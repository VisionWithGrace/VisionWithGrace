                                
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.Kinect;
using System.Media;
using System.Runtime.InteropServices;
using System.Timers;

using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using Emgu.CV.UI;

using DatabaseModule;


/**
 * Handles the Kinect feed and recognizing objects from it
 * */
namespace VisionWithGrace
{
    class ComputerVision
    {
        //Kinect sensor
        KinectSensor sensor;
        public bool isUsingKinect;
        private const DepthImageFormat DepthFormat = DepthImageFormat.Resolution320x240Fps30;
        private const ColorImageFormat ColorFormat = ColorImageFormat.RgbResolution640x480Fps30;

        //Collecting frames
        private int frameCounter;
        private int frameLimit;
        private bool FramesReady;

        //Depth processing
        private DepthImagePixel[] depthPixels;
        private Image<Bgra, byte> emguOverlayedDepth;
        private Image<Gray, byte> emguDepthWithBoxes;
        private Image<Gray, byte> emguProcessedGrayDepth;

        //Color processing 
        private ColorImagePoint[] colorCoordinates;
        private Image<Bgra, byte> emguRawColor;
        private Image<Bgra, byte> emguProcessedColor;

        //Rectangles 
        public int num_objects;
        List<Tuple<Rectangle,int>> objects;
        
        //Object Recognition
        //public DrawMatches matcher;
        List<Image<Bgra, byte>> subimages;
        Image<Bgr, byte> matchResult;

        //Debug window 
        private cvDebug debugWindow;

        //Simulation (for when Kinect is not present)
        private Bitmap simulationImage;
        
        public ComputerVision()
        {
            // Start kinect sensor
            try
            {
                //Sensor parameter
                sensor = KinectSensor.KinectSensors.First();
                isUsingKinect = true;

                //Set formats
                sensor.ColorStream.Enable(ColorFormat);
                sensor.DepthStream.Enable(DepthFormat);

                //Set depth mapping data
                this.colorCoordinates = new ColorImagePoint[this.sensor.DepthStream.FramePixelDataLength];
                this.depthPixels = new DepthImagePixel[this.sensor.DepthStream.FramePixelDataLength];

                //Set handler to read image when depth rolls in
                sensor.AllFramesReady += this.GetFrames;
                this.frameCounter = 4;
                this.frameLimit = 3;
                this.FramesReady = false;

                //Attempt to start Kinect
                try
                {
                    sensor.Start();
                }
                catch
                {
                    this.sensor = null;
                }

                this.subimages = new List<Image<Bgra,byte>>();

                this.debugWindow = new cvDebug();
                this.debugWindow.Show();
            }
            // Open file dialog if no kinect is found
            catch
            {
                isUsingKinect = false;
                simulationImage = new Bitmap("../../SampleImages/room.jpeg");
            }

            this.objects = new List<Tuple<Rectangle,int>>();
        }
        
        // Get depth and color frames from Kinect
        private void GetFrames(object sender, AllFramesReadyEventArgs e)
        {
            if (null == this.sensor)
            {
                //Kinect is shutting down or not connected
                return;
            }

            bool depthReceived = false;

            using (DepthImageFrame depthFrame = e.OpenDepthImageFrame())
            {
                if (null != depthFrame && (this.frameCounter++ >= this.frameLimit))
                {
                    // Copy the pixel data from the image to a temporary array
                    depthFrame.CopyDepthImagePixelDataTo(this.depthPixels);
                    depthReceived = true;
                    this.frameCounter = 0;
                }
            }

            using (ColorImageFrame colorFrame = e.OpenColorImageFrame())
            {
                if (null != colorFrame && depthReceived)
                {
                    //Copy color immediately to two EMGU images
                    Bitmap colorBitmap = new Bitmap(this.ColorImageFrameToBitmap(colorFrame));
                    this.emguRawColor = new Image<Bgra, byte>(colorBitmap);
                    this.emguProcessedColor = new Image<Bgra, byte>(colorBitmap);
                    this.FramesReady = true;
                }
            }
        }

        private void DetectObjects()
        {             
            //************ Map depth data to color pixels *************//                
            // Microsoft-provided magic
            this.sensor.CoordinateMapper.MapDepthFrameToColorFrame(
                DepthFormat, this.depthPixels,
                ColorFormat, colorCoordinates);
            //*********************************************************//

            //******* Create grayscale image to represent depth *******//
            this.emguOverlayedDepth = new Image<Bgra, byte>(this.sensor.ColorStream.FrameWidth, this.sensor.ColorStream.FrameHeight, new Bgra(0, 0, 0, 255));
            //*********************************************************//

            //**********  Loop through colorCoordinates  **************//
            for (int i = 0; i < colorCoordinates.Length; i++)
            {
                if (this.depthPixels[i].IsKnownDepth)
                {
                    // Scale color coordinates to depth resolution
                    int colorInDepthX = colorCoordinates[i].X / (this.sensor.ColorStream.FrameWidth / this.sensor.DepthStream.FrameWidth);
                    int colorInDepthY = colorCoordinates[i].Y / (this.sensor.ColorStream.FrameWidth / this.sensor.DepthStream.FrameWidth);

                    //Check bounds on picture size
                    if (colorInDepthX > 0 && colorInDepthX < this.sensor.DepthStream.FrameWidth &&
                        colorInDepthY >= 0 && colorInDepthY < this.sensor.DepthStream.FrameHeight)
                    {
                        //Check bounds on depth data
                        if (this.depthPixels[i].Depth < 4000)
                        {
                            //Calculate intensity and populate Image object
                            var intensity = CalculateIntensityFromDistance(this.depthPixels[i].Depth);
                            this.emguOverlayedDepth[colorInDepthY, colorInDepthX] = new Bgra(intensity, intensity, intensity, 255);
                        }
                    }
                }
            }

            //Resize Depth images 
            this.emguOverlayedDepth = this.emguOverlayedDepth.Resize(2.0, INTER.CV_INTER_NN).Copy();
            //this.emguProcessedColor = this.emguProcessedColor.Resize(0.5, INTER.CV_INTER_NN);


            this.debugWindow.emguDepthImageBox.Image = this.emguOverlayedDepth;
            //*********************************************************//

            //****************** Draw Depth Lines *********************//
            //Effects:  Converts depth info to canny edges
            Image<Gray, byte> emguOverlayedGrayDepth = this.emguOverlayedDepth.Convert<Gray, byte>();
            this.emguProcessedGrayDepth = new Image<Gray, byte>(this.emguOverlayedDepth.Width, this.emguOverlayedDepth.Height);

            CvInvoke.cvSmooth(emguOverlayedGrayDepth, emguOverlayedGrayDepth, Emgu.CV.CvEnum.SMOOTH_TYPE.CV_MEDIAN, 5, 5, 9, 9);
            CvInvoke.cvCanny(emguOverlayedGrayDepth, this.emguProcessedGrayDepth, 100, 50, 3);

            //*********************************************************//

            //***************** Fatten Up Depth Lines *****************//
            //Effects: Closes gaps in depth lines
            Point seedPoint;
            emguOverlayedGrayDepth = this.emguProcessedGrayDepth.Copy();
            for (int fillY = emguOverlayedGrayDepth.Height - 1; fillY > 0; fillY--)
            {
                for (int fillX = emguOverlayedGrayDepth.Width - 1; fillX > 0; fillX--)
                {
                    seedPoint = new Point(fillX, fillY);
                    if (emguOverlayedGrayDepth[fillY, fillX].Intensity >= 200)
                    {
                        if (fillX < emguOverlayedGrayDepth.Width - 1)
                        {
                            this.emguProcessedGrayDepth[fillY, fillX + 1] = new Emgu.CV.Structure.Gray(255);
                        }
                        if (fillX > 0)
                        {
                            this.emguProcessedGrayDepth[fillY, fillX - 1] = new Emgu.CV.Structure.Gray(255);
                        }
                        if (fillY < emguOverlayedGrayDepth.Height - 1)
                        {
                            this.emguProcessedGrayDepth[fillY + 1, fillX] = new Emgu.CV.Structure.Gray(255);
                        }
                        if (fillY > 0)
                        {
                            this.emguProcessedGrayDepth[fillY - 1, fillX] = new Emgu.CV.Structure.Gray(255);
                        }
                    }
                }
            }
            //Horizon Handling
            //NOTE: Must be implemented!!!
            //**********************************************************//


            //****************** Draw Boxes ****************************//
            //Effects:  Floods canny edges with color at arbitrary points
            //          Group points by color
            //          Draw boxes around groups of pixels

            // Flood-fill method for boxes
            IntPtr src = this.emguProcessedGrayDepth;
            MCvScalar newVal;
            MCvScalar loDiff = new MCvScalar(0);
            MCvScalar upDiff = new MCvScalar(0);
            MCvConnectedComp comp = new MCvConnectedComp();

            //Create array of point-clouds
            List<PointF>[] pointList = new List<PointF>[256];
            for (int i = 0; i < 256; i++)
            {
                pointList[i] = new List<PointF>();
            }

            //****** Iterate through picture, filling in arbitrary pixels ********//
            int flags = 4;
            IntPtr mask = new Image<Gray, byte>(this.emguProcessedGrayDepth.Width + 2, this.emguProcessedGrayDepth.Height + 2, new Gray(0));
            int whatColor = 0;
            for (int fillY = this.emguProcessedGrayDepth.Height - 1; fillY > 0; fillY -= this.emguProcessedGrayDepth.Height / 256)
            {
                for (int fillX = this.emguProcessedGrayDepth.Width - 1; fillX > 0; fillX -= this.emguProcessedGrayDepth.Width / 256)
                {
                    seedPoint = new Point(fillX, fillY);
                    if (this.emguProcessedGrayDepth[seedPoint].Intensity == 0)
                    {
                        //Fill a region of the picture with a new color
                        whatColor++;
                        newVal = new MCvScalar(whatColor);
                        CvInvoke.cvFloodFill(src, seedPoint, newVal, loDiff, upDiff, out comp, flags, mask);
                        pointList[whatColor].Add(new PointF(fillX, fillY));
                    }
                    else
                    {
                        //Hash point into proper point-cloud entry
                        pointList[(int)this.emguProcessedGrayDepth[seedPoint].Intensity].Add(new PointF(fillX, fillY));
                    }
                }
            }
            //****************************************************************//

            //***************** Generate boxes *******************************//
            HashSet<int> GoodColors = new HashSet<int>();
            this.emguDepthWithBoxes = this.emguProcessedGrayDepth.Copy();
            this.num_objects = 0;
            this.objects.Clear();

            for (int i = 1; i < 255; i++)
            {
                if (pointList[i].Count > 100)
                {
                    Rectangle temp = Emgu.CV.PointCollection.BoundingRectangle(pointList[i].ToArray());
                    if ((temp.Height < 0.6 * this.emguProcessedGrayDepth.Height)
                        && (temp.Width < 0.6 * this.emguProcessedGrayDepth.Width)
                        && (pointList[i].Count < 4000))
                    {
                        //Draw rectangle around object
                        this.emguDepthWithBoxes.Draw(temp, new Gray(127), 2);

                        //Add to rectangle list
                        this.objects.Add(new Tuple<Rectangle, int>(temp,i));
                        this.num_objects++;

                        //Add Color to be drawn
                        GoodColors.Add(i);
                    }

                    /*if (pointList[i].Count < 4000)
                    {
                        GoodColors.Add(i); 
                    }*/
                }
            }

            this.debugWindow.emguDepthProcessedImageBox.Image = this.emguDepthWithBoxes;
            //***************************************************************//

            //Resize color images to match depth resolution
            //this.emguRawColor = this.emguRawColor.Resize(.5, INTER.CV_INTER_NN).Copy();
            //this.emguProcessedColor = this.emguProcessedColor.Resize(0.5, INTER.CV_INTER_NN);


            //Assign colored pixels
            for (int fillY = this.emguProcessedColor.Height - 1; fillY > 0; fillY--)
            {
                for (int fillX = this.emguProcessedColor.Width - 1; fillX > 0; fillX--)
                {
                    seedPoint = new Point(fillX, fillY);
                    if (!GoodColors.Contains((int)this.emguProcessedGrayDepth[seedPoint].Intensity))
                    {              
                        /*
                        double b = this.emguProcessedColor[seedPoint].Blue;
                        double g = this.emguProcessedColor[seedPoint].Green;
                        double r = this.emguProcessedColor[seedPoint].Red;
                        double a = this.emguProcessedColor[seedPoint].Alpha;
                        this.emguProcessedColor[seedPoint] = new Bgra(b, g, r, a / 8);
                            */
                        this.emguProcessedColor[seedPoint] = new Bgra(0, 0, 0, 255);
                    }
                }
            }

            //Sort boxes, left to right, then top to bottom
            this.objects.Sort((a, b) => a.Item1.Left.CompareTo(b.Item1.Left));

            //Get sub-images
            this.subimages = new List<Image<Bgra,byte>>();
            foreach(Tuple<Rectangle,int> tuple in this.objects)
            {
                int exceptionsThrown = 0;
                try
                {
                    Image<Bgra, byte> subimage = this.emguRawColor.GetSubRect(tuple.Item1).Copy();

                    for (int fillY = tuple.Item1.Top; fillY < tuple.Item1.Bottom; fillY++)
                    {
                        for (int fillX = tuple.Item1.Left; fillX < tuple.Item1.Right; fillX++)
                        {
                            seedPoint = new Point(fillX, fillY);
                            if ((int)this.emguProcessedGrayDepth[seedPoint].Intensity != tuple.Item2)
                            {
                                Point subPoint = new Point(fillX - tuple.Item1.Left, fillY - tuple.Item1.Top);
                                subimage[subPoint] = new Bgra(0, 0, 0, 255);
                            }
                        }
                    }
                    this.subimages.Add(subimage);
                }
                catch
                {
                    exceptionsThrown++;
                }
            }

            //Assign processed color
            //this.debugWindow.emguColorImageBox.Image = this.emguRawColor;
            int windowWidth = this.debugWindow.emguColorProcessedImageBox.Width;
            int windowHeight = this.debugWindow.emguColorProcessedImageBox.Height;
            this.debugWindow.emguColorProcessedImageBox.Image = this.emguProcessedColor.Resize(windowWidth, windowHeight, INTER.CV_INTER_NN);
        }

        public static byte CalculateIntensityFromDistance(short distance)
        {
            // This will map a distance value to a 0 - 255 range
            // for the purposes of applying the resulting value
            // to RGB pixels.
            const int MaxDepthDistance = 4000;
            const int MinDepthDistance = 800;
            const int MaxDepthDistanceOffset = MaxDepthDistance - MinDepthDistance;

            int newDistance = distance - MinDepthDistance;
            if (newDistance >= 0)
                return (byte)(255 - (255 * newDistance
                / (MaxDepthDistanceOffset)));
            else
                return (byte)255;
        }
           
        private static void getFrameTimeout(object source, ElapsedEventArgs e)
        {
            Console.WriteLine("The kinect did not serve a frame for 3 seconds. Exiting.");
            throw new Exception();
        } 
        public Bitmap getFrame()
        {
            if (isUsingKinect)
            {
                if (this.FramesReady)
                {
                    return this.emguRawColor.ToBitmap();
                }
                else
                {
                    ColorImageFrame cFrame = this.sensor.ColorStream.OpenNextFrame(4000);
                    if (cFrame != null)
                    {
                        return this.ColorImageFrameToBitmap(cFrame);
                    }
                    else
                    {
                        Console.WriteLine("The kinect did not serve a frame for 4 seconds. Exiting.");
                        throw new Exception();
                    }
                }
                /*System.Timers.Timer t = new System.Timers.Timer(10000);
                t.Elapsed += getFrameTimeout;
                t.Start();

                int counter = 0;
                while (!this.FramesReady)
                {
                    System.Threading.Thread.Sleep(500);
                    if (counter++ > 8)
                    {
                        Console.WriteLine("The kinect did not serve a frame for 4 seconds. Exiting.");
                        throw new Exception();
                    }
                }
                else
                {
                    return new Bitmap(this.sensor.ColorStream.FrameWidth, this.sensor.ColorStream.FrameHeight);
                }*/
            }
            else
            {
                return this.simulationImage;
            }
        }


        // Return boxes scaled to color resolution
        public List<Rectangle> getBoxes()
        {
            //If Kinect sensor is being used
            if (isUsingKinect)
            {
                //Look for objects in latest image
                if (this.FramesReady == true)
                {
                    this.DetectObjects();
                }

                List<Rectangle> unscaled_boxes = new List<Rectangle>();
                foreach (Tuple<Rectangle, int> tuple in this.objects)
                {
                    unscaled_boxes.Add(tuple.Item1);
                }
                return unscaled_boxes;
            }

            else
            {

                Random rand = new Random();
                this.num_objects = (rand.Next() % 4) + 2;
                List<Rectangle> rand_objects = new List<Rectangle>();
                int x, y, z, w;
                int max_width;
                int max_height;

                // Get image height and width
                if (isUsingKinect)
                {
                    max_width = sensor.ColorStream.FrameWidth;
                    max_height = sensor.ColorStream.FrameHeight;
                }
                else
                {
                    max_width = simulationImage.Width;
                    max_height = simulationImage.Height;
                }

                // Create N randomly sized and located rectangles
                for (int i = 0; i < num_objects; i++)
                {
                    x = rand.Next() % (max_width - 160);
                    y = rand.Next() % (max_height - 160);
                    z = (rand.Next() % 120) + 40;
                    w = (rand.Next() % 120) + 40;
                    rand_objects.Add(new Rectangle(x, y, z, w));                   
                }

                //Sort boxes, left to right
                rand_objects.Sort((a, b) => a.Left.CompareTo(b.Left));
                return rand_objects;
            }           
        }

        /*
         * Given the index in the List<Rectangle> just passed, match that pic
         * against entries in the database. If no suitable match is found, pass
         * back null reference
         */
        public VObject RecognizeObject(int index)
        {
            if (!this.isUsingKinect)
            {
                return null;
            }
            //Check for bad index
            if (index >= this.subimages.Count || index < 0)
            {
                //The index is out of range
                throw new IndexOutOfRangeException();
            }
          
            //Convert selected subimage to larger grayscale
            Image<Gray,byte> target = this.subimages[index].Convert<Gray, byte>().Resize(5.0, INTER.CV_INTER_NN);

            //Begin to iterate through objects in the database, matching each in scene
            DatabaseInterface DbInterface = new DatabaseInterface();
            List<VObject> entries = DbInterface.getAllObjects();

            int maxMatches = 0;
            VObject bestMatch = null;

            foreach( VObject entry in entries)
            {
                //Convert bitmap to Emgu image
                Image<Gray,byte> img = new Image<Gray,byte>(entry.image as Bitmap).Resize(5.0, INTER.CV_INTER_NN);

                long matchTime = new long();
                int numMatches = new int();
                this.matchResult = DrawMatches.Draw(target, img, out matchTime, out numMatches);
      
                this.debugWindow.emguColorImageBox.Image = this.matchResult;
                this.debugWindow.Text = numMatches.ToString() + " matches.";

                //Record best match
                if ((numMatches >= 4) && (numMatches > maxMatches))
                {
                    maxMatches = numMatches;
                    bestMatch = entry;
                }
            }

            if(bestMatch != null)
            {
                this.debugWindow.Text = "Object recognized! " + bestMatch.name + "("+maxMatches.ToString()+" matches)";
            }

            return bestMatch;
        }



        // Don't touch this function, I didn't write it. It came from the interwebs.
        public Bitmap ColorImageFrameToBitmap(ColorImageFrame colorFrame)
        {
            byte[] pixelBuffer = new byte[colorFrame.PixelDataLength];
            colorFrame.CopyPixelDataTo(pixelBuffer);

            Bitmap bitmapFrame = new Bitmap(colorFrame.Width, colorFrame.Height,
                System.Drawing.Imaging.PixelFormat.Format32bppRgb);

            System.Drawing.Imaging.BitmapData bitmapData = bitmapFrame.LockBits(new Rectangle(0, 0,
                                             colorFrame.Width, colorFrame.Height),
            System.Drawing.Imaging.ImageLockMode.WriteOnly, bitmapFrame.PixelFormat);

            IntPtr intPointer = bitmapData.Scan0;
            Marshal.Copy(pixelBuffer, 0, intPointer, colorFrame.PixelDataLength);

            bitmapFrame.UnlockBits(bitmapData);
            return bitmapFrame;
        }
    }
}
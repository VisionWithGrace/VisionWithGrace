                                
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.Kinect;

using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using Emgu.CV.UI;


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
        Timer timer = new Timer();
        
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
                this.frameCounter = 0;
                this.frameLimit = 30;

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

                OpenFileDialog ofd = new OpenFileDialog();
                ofd.Title = "Select a picture file";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    simulationImage = new Bitmap(ofd.FileName);
                }
                else
                {
                    simulationImage = new Bitmap(400, 400);
                }

                // Initialize timer to simulate Kinect frames
                // 33 ms ~= 1/30 sec ~= 30 FPS
                timer.Interval = 33;
            }

            this.objects = new List<Tuple<Rectangle,int>>();
        }

        public void set_handler(EventHandler handler)
        {
            if (isUsingKinect)
                throw new InvalidOperationException();

            timer.Tick += handler;
            timer.Start();
        }
        public void set_handler(EventHandler<ColorImageFrameReadyEventArgs> handler)
        {
            if (!isUsingKinect)
                throw new InvalidOperationException();

            sensor.ColorFrameReady += handler;
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
                    Bitmap colorBitmap = new Bitmap(MainForm.ColorImageFrameToBitmap(colorFrame));
                    this.emguRawColor = new Image<Bgra, byte>(colorBitmap);
                    this.emguProcessedColor = new Image<Bgra, byte>(colorBitmap);
                }
            }
        
        
            // do our processing outside of the using block
            // so that we return resources to the kinect as soon as possible
            if (true == depthReceived)
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
                            && (temp.Width < 0.6 * this.emguProcessedGrayDepth.Width))
                        {
                            //Draw rectangle around object
                            this.emguDepthWithBoxes.Draw(temp, new Gray(127), 2);

                            //Add to rectangle list
                            this.objects.Add(new Tuple<Rectangle, int>(temp,i));
                            this.num_objects++;
                        }

                        if (pointList[i].Count < 4000)
                        {
                            GoodColors.Add(i);
                        }
                    }
                }

                this.debugWindow.emguDepthProcessedImageBox.Image = this.emguDepthWithBoxes;
                //***************************************************************//

                //Resize color images to match depth resolution
                this.emguRawColor = this.emguRawColor.Resize(.5, INTER.CV_INTER_NN).Copy();
                this.emguProcessedColor = this.emguProcessedColor.Resize(0.5, INTER.CV_INTER_NN);


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
                this.debugWindow.emguColorProcessedImageBox.Image = this.emguProcessedColor;
            }
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

        // Return boxes scaled to color resolution
        public List<Rectangle> getBoxes()
        {
            //If Kinect sensor is being used
            if (isUsingKinect)
            {
                List<Rectangle> scaled = new List<Rectangle>();
                foreach (Tuple<Rectangle,int> tuple in this.objects)
                {
                    scaled.Add(new Rectangle(tuple.Item1.X * 2, tuple.Item1.Y * 2, tuple.Item1.Width * 2, tuple.Item1.Height * 2)); 
                }

                if (this.subimages.Count > 0)
                {
                    long matchTime = new long();
                    this.matchResult = DrawMatches.Draw(this.subimages[0].Convert<Gray, byte>().Resize(5.0, INTER.CV_INTER_NN), this.emguProcessedColor.Convert<Gray, byte>().Resize(5.0,INTER.CV_INTER_NN), out matchTime);
                    this.debugWindow.emguColorImageBox.Image = this.matchResult;
                }

                return scaled;
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
                return rand_objects;
            }
            
        }

        public Bitmap getSimulationImage()
        {
            return simulationImage;
        }
    }
}
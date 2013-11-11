using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.Kinect;

namespace VisionWithGrace
{
    class ComputerVision
    {
        KinectSensor sensor;
        private Bitmap simulationImage;
        Rectangle[] objects;
        public bool isUsingKinect;
        Timer timer = new Timer();
        public int num_objects;
        public ComputerVision()
        {
            // Start kinect sensor
            try
            {
                sensor = KinectSensor.KinectSensors.First();
                isUsingKinect = true;
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
            sensor.Start();
            sensor.ColorStream.Enable();
        }

        // Draw Rectangles at random locations
        // Need to receive x,y coordinates from Kinect
        public Rectangle[] getBoxes()
        {
            Random rand = new Random();
            num_objects = (rand.Next() % 4) + 2;
            objects = new Rectangle[num_objects];
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
                objects[i] = new Rectangle(x, y, z, w);
            }

            return objects;
        }

        public Bitmap getSimulationImage()
        {
            return simulationImage;
        }
    }
}
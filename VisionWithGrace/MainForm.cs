using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Kinect;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace VisionWithGrace
{
    public partial class MainForm : Form
    {
        Bitmap plainView;
        Bitmap boxedView;
        Rectangle[] rectangles;
        int selected;
        int frame_count;
        ComputerVision cv;
        Database db;
        Pen redPen = new Pen(Color.Red, 3);
        Pen yellowPen = new Pen(Color.Yellow, 5);

        // Scanning Interface
        private bool isScanning;
        private int framesPerScan = 50;

        public MainForm()
        {
            InitializeComponent();
        }


        // Initialize Computer Vision and Kinect upon Load
        // Draw mockup GUI if no kinect is found
        private void Form1_Shown(object sender, EventArgs e)
        {
            this.mainDisplay.Focus();
            
            // Start CV and set handler
            cv = new ComputerVision();

            // Get first set of boxes
            rectangles = cv.getBoxes();
            this.objectDetectedLabel.Text = rectangles.Length.ToString() + " objects detected";

            // Set kinect handler
            if (cv.isUsingKinect)
            {
                cv.set_handler(new EventHandler<ColorImageFrameReadyEventArgs>(this.colorFrameReady));
            }
            else
            {
                cv.set_handler(new EventHandler(this.colorFrameReady));
            }
            frame_count = 0;
            isScanning = false;
        }

        public void colorFrameReady(object sender, EventArgs e)
        {
            processFrame(cv.getSimulationImage());
        }
        public void colorFrameReady(object sender, ColorImageFrameReadyEventArgs e)
        {
            ColorImageFrame colorFrame = e.OpenColorImageFrame();
            if (colorFrame == null)
            {
                return;
            }
            processFrame(ColorImageFrameToBitmap(colorFrame));
        }

        private void processFrame(Bitmap bitmap)
        {
            // delete previous values
            if (plainView != null)
                plainView.Dispose();
            if (boxedView != null)
                boxedView.Dispose();

            // Track number of frames if currently scanning
            if (isScanning)
                frame_count++;
            else
                frame_count = 0;

            plainView = new Bitmap(bitmap);
            boxedView = new Bitmap(plainView);

            // Add boxes
            using (var graphics = Graphics.FromImage(boxedView))
            {
                for (int i = 0; i < rectangles.Length; i++)
                {
                    if (i == selected)
                        graphics.DrawRectangle(yellowPen, rectangles[i]);
                    else
                        graphics.DrawRectangle(redPen, rectangles[i]);
                }
            }

            if (frame_count > framesPerScan)
            {
                selected = (selected + 1) % rectangles.Length;
                frame_count = 0;
            }

            if (isScanning)
                this.labelTimeRemaining.Text = Convert.ToString(framesPerScan - frame_count);

            this.mainDisplay.Image = boxedView;
        }

        // Don't touch this function, I didn't write it. It came from the interwebs.
        private static Bitmap ColorImageFrameToBitmap(ColorImageFrame colorFrame)
        {
            byte[] pixelBuffer = new byte[colorFrame.PixelDataLength];
            colorFrame.CopyPixelDataTo(pixelBuffer);

            Bitmap bitmapFrame = new Bitmap(colorFrame.Width, colorFrame.Height,
                PixelFormat.Format32bppRgb);

            BitmapData bitmapData = bitmapFrame.LockBits(new Rectangle(0, 0,
                                             colorFrame.Width, colorFrame.Height),
            ImageLockMode.WriteOnly, bitmapFrame.PixelFormat);

            IntPtr intPointer = bitmapData.Scan0;
            Marshal.Copy(pixelBuffer, 0, intPointer, colorFrame.PixelDataLength);

            bitmapFrame.UnlockBits(bitmapData);
            return bitmapFrame;
        }

        private void selectButton_Click(object sender, EventArgs e)
        {
            show_selected_object();
        }

        // Display selected object in closeUpDisplay
        private void show_selected_object()
        {
            try
            {
                Bitmap zoomView = new Bitmap(rectangles[selected].Width, rectangles[selected].Height);

                using (var graphics = Graphics.FromImage(zoomView))
                {
                    graphics.DrawImage(plainView, new Rectangle(0, 0, zoomView.Width, zoomView.Height), rectangles[selected], GraphicsUnit.Pixel);
                }

                this.closeUpDisplay.Image = zoomView;
            }
            catch (Exception e)
            {
                // Do something
            }
        }

        // Rotate and highlight the rectangles, also display the cropped image in the closeupView
        private void scanButton_Click_1(object sender, EventArgs e)
        {
            Bitmap tempBitmap = new Bitmap(boxedView);

            // Highlight rectangle in yellow
            using (var graphics = Graphics.FromImage(tempBitmap))
            {
                graphics.DrawRectangle(yellowPen, rectangles[selected]);
            }

            mainDisplay.Image = tempBitmap;

            // Draw cropped image in closeupView
            Bitmap zoomView = new Bitmap(rectangles[selected].Width, rectangles[selected].Height);
            using (var graphics = Graphics.FromImage(zoomView))
            {
                graphics.DrawImage(plainView, new Rectangle(0, 0, zoomView.Width, zoomView.Height), rectangles[selected], GraphicsUnit.Pixel);
            }
            closeUpDisplay.Image = zoomView;

            selected++;
            if (selected >= cv.num_objects) selected = 0;
        }

        // Re-generate rectangles
        private void refreshButton_Click(object sender, EventArgs e)
        {
            rectangles = cv.getBoxes();
            this.objectDetectedLabel.Text = rectangles.Length.ToString() + " objects detected";
            boxedView = new Bitmap(plainView);
            drawRectangles();
            mainDisplay.Image = boxedView;
        }

        private void drawRectangles()
        {
            using (var graphics = Graphics.FromImage(boxedView))
            {
                for (int i = 0; i < cv.num_objects; i++)
                {
                    graphics.DrawRectangle(redPen, rectangles[i]);
                }
            }
            selected = 0;
        }

        private void startScanning(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Space)
                return;
            e.SuppressKeyPress = true;
            isScanning = true;
        }
        private void stopScanning(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Space)
                return;
            e.SuppressKeyPress = true;
            isScanning = false;
            this.labelTimeRemaining.Text = "";
            show_selected_object();
        }

        private void objectNameText_Enter(object sender, EventArgs e)
        {
            this.objectNameText.Text = "";
        }

        private void objectNameText_Leave(object sender, EventArgs e)
        {
            this.objectNameText.Text = "Enter object name...";
        }

        private void objectNameText_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                this.objectNameText.Text = "";
        }

        // Opens the admin panel for editing tags
        private void adminPanelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AdminPanel adminPanel = new AdminPanel();
            adminPanel.Show();
        }
    }
}
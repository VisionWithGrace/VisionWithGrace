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
        Bitmap boxesView;
        List<Rectangle> rectangles;
        ComputerVision cv;
        Database db;

        Scanner scanner = new Scanner();

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

            // Set kinect handler
            if (cv.isUsingKinect)
            {
                cv.set_handler(new EventHandler<ColorImageFrameReadyEventArgs>(this.colorFrameReady));
            }
            else
            {
                cv.set_handler(new EventHandler(this.colorFrameReady));
            }

            scanner.OnChange = highlightNextBox;
        }

        public void colorFrameReady(object sender, EventArgs e)
        {
            processFrame(cv.getSimulationImage());
        }
        public void colorFrameReady(object sender, ColorImageFrameReadyEventArgs e)
        {
            ColorImageFrame colorFrame = e.OpenColorImageFrame();
            if (colorFrame != null)
            {
                processFrame(ColorImageFrameToBitmap(colorFrame));
            }
        }

        public void highlightNextBox(object sender, EventArgs e)
        {
            int nextHighlighted = scanner.CurObject;

            if (boxesView == null)
                return;

            drawBoxes(nextHighlighted);
        }

        private void processFrame(Bitmap bitmap)
        {
            // delete previous values
            if (plainView != null)
                plainView.Dispose();
            plainView = new Bitmap(bitmap);

            if (boxesView == null)
                getNewBoxes();

            // Redraw views on main display
            if (mainDisplay.Image != null)
                this.mainDisplay.Image.Dispose();
            mainDisplay.Image = new Bitmap(plainView.Size.Width, plainView.Size.Height);
            using (Graphics g = Graphics.FromImage(mainDisplay.Image))
            {
                // overlay the boxedView on the plainView
                g.DrawImage(plainView, new Rectangle(0, 0, plainView.Width, plainView.Height));
                g.DrawImage(boxesView, new Rectangle(0, 0, boxesView.Width, boxesView.Height));
            }
        }

        // Don't touch this function, I didn't write it. It came from the interwebs.
        public static Bitmap ColorImageFrameToBitmap(ColorImageFrame colorFrame)
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

        // Display selected object in closeUpDisplay
        private void show_selected_object()
        {
            try
            {
                Bitmap zoomView = new Bitmap(rectangles[scanner.CurObject].Width, rectangles[scanner.CurObject].Height);

                using (var graphics = Graphics.FromImage(zoomView))
                {
                    graphics.DrawImage(plainView, new Rectangle(0, 0, zoomView.Width, zoomView.Height), rectangles[scanner.CurObject], GraphicsUnit.Pixel);
                }

                this.closeUpDisplay.Image = zoomView;
            }
            catch (Exception e)
            {
                // Do something
            }
        }

        // Re-generate rectangles
        private void refreshButton_Click(object sender, EventArgs e)
        {
            getNewBoxes();
        }

        private void getNewBoxes()
        {
            rectangles = cv.getBoxes();

            scanner.NumObjects = rectangles.Count;
            this.objectDetectedLabel.Text = rectangles.Count.ToString() + " objects detected";

            drawBoxes();
        }

        private void drawBoxes(int selected = -1)
        {
            if (boxesView != null)
                boxesView.Dispose();
            boxesView = new Bitmap(plainView.Size.Width, plainView.Size.Height);


            Pen redPen = new Pen(Color.Red, 3);
            Pen yellowPen = new Pen(Color.Yellow, 5);

            using (var graphics = Graphics.FromImage(boxesView))
            {
                for (int i = 0; i < rectangles.Count; i++)
                {
                    if (selected != -1 && i == selected)
                        graphics.DrawRectangle(yellowPen, rectangles[i]);
                    else
                        graphics.DrawRectangle(redPen, rectangles[i]);
                }
            }
        }

        private void startScanning(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Space)
                return;
            
            e.SuppressKeyPress = true;
            scanner.start();
        }
        private void stopScanning(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Space)
                return;
            e.SuppressKeyPress = true;

            scanner.stop();

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
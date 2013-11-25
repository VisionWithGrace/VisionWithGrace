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
using System.Media;

using DatabaseModule;

/**
 * Main GUI from which everything is displayed and controlled
 * */
namespace VisionWithGrace
{
    public partial class MainForm : Form
    {
        Bitmap plainView;
        Bitmap boxesView;
        List<Rectangle> rectangles;
        ComputerVision cv;

        Scanner scanner = new Scanner();
        Timer refreshTimer = new Timer();
        bool readyForNewFrame = new bool();

        int x, x0, x1, y, y0, y1, Mstep, diff, scale;
        bool isManual = false;

        public MainForm()
        {
            InitializeComponent();

            refreshTimer.Interval = 5000;
            refreshTimer.Tick += refreshView;
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
                this.readyForNewFrame = true;
            }
            else
            {
                cv.set_handler(new EventHandler(this.colorFrameReady));
            }

            scanner.OnChange = highlightNextBox;            
            refreshTimer.Start();
        }

        private void refreshView(object sender, EventArgs e)
        {
            this.readyForNewFrame = true;
            getNewBoxes();
        }

        public void colorFrameReady(object sender, EventArgs e)
        {
            processFrame(cv.getSimulationImage());
        }
        public void colorFrameReady(object sender, ColorImageFrameReadyEventArgs e)
        {
            ColorImageFrame colorFrame = e.OpenColorImageFrame();
            if (this.readyForNewFrame && colorFrame != null)
            {
                processFrame(ColorImageFrameToBitmap(colorFrame));
                this.readyForNewFrame = false;
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
        private void showSelectedObject(VObject vobj)
        {
            SoundPlayer player = new SoundPlayer(@"C:\WINDOWS\Media\notify.wav");
            player.Play();

            SelectedObjectForm selectedObjectForm;
            selectedObjectForm = new SelectedObjectForm(vobj);
            selectedObjectForm.ShowDialog();
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
            refreshTimer.Stop();
            this.readyForNewFrame = false;

            scanner.start();
        }
        private void stopScanning(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Space)
                return;
            e.SuppressKeyPress = true;

            scanner.stop();

            this.labelTimeRemaining.Text = "";
            if (isManual) manualStep();
            else
            {
                VObject vobj = cv.RecognizeObject(scanner.CurObject);
                if (vobj != null)
                {
                    this.Text = vobj.name + " found.";
                }
                else
                {
                    vobj = new VObject();
                    vobj.image = getImageInBox(rectangles[scanner.CurObject]);
                }
                showSelectedObject(vobj);
                refreshTimer.Start();
            }
        }

        private Bitmap getImageInBox(Rectangle rectangle)
        {
            Bitmap image = new Bitmap(rectangle.Width, rectangle.Height);

            using (var graphics = Graphics.FromImage(image))
            {
                graphics.DrawImage(plainView, new Rectangle(0, 0, image.Width, image.Height), rectangle, GraphicsUnit.Pixel);
            }

            return image;
        }

        // Opens the admin panel for editing tags
        private void adminPanelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AdminPanel adminPanel = new AdminPanel();
            adminPanel.Show();
        }

        private void manualStep()
        {
            if (Mstep == 0)
            {
                x0 = x;
                x1 = x;
                diff = 6;
            }
            else if (Mstep == 1)
            {
                y = y0;
                diff = 10;
            }
            else if (Mstep == 2)
            {
                y0 = y;
                y1 = y;
                diff = 6;
            }
            else if (Mstep == 3)
            {
                VObject vObject = new VObject();
                vObject.image = getImageInBox(new Rectangle(x0, y0, x1 - x0, y1 - y0));
                showSelectedObject(vObject);
                y0 = 0;
                y1 = plainView.Size.Height;
                x0 = 0;
                x1 = plainView.Size.Width;
                x = x0;
                diff = 10;
                Mstep = -1;
            }
            Mstep++;
        }
        public void manualScanNextBox(object sender, EventArgs e)
        {
            if (boxesView == null)
                return;

            manualScan();
        }
        private void manualScan()
        {
            if (boxesView != null)
                boxesView.Dispose();
            boxesView = new Bitmap(plainView.Size.Width, plainView.Size.Height);


            Pen redPen = new Pen(Color.Red, 5);
            //Pen yellowPen = new Pen(Color.Yellow, 5);
            if (Mstep == 0)
            {
                using (var graphics = Graphics.FromImage(boxesView))
                {
                    graphics.DrawLine(redPen, new Point(x, y0), new Point(x, y1));
                }
                x = x + diff;
                if (x < x0 || x > x1)
                {
                    diff = diff * -1;
                    x = x + diff + diff;
                }
            }
            else if (Mstep == 1)
            {
                using (var graphics = Graphics.FromImage(boxesView))
                {
                    graphics.DrawRectangle(redPen, new Rectangle(x0, y0, x1 - x0, y1 - y0));
                }
                x0 = x0 - diff;
                x1 = x1 + diff;
                if (x0 < 0 || x1 > plainView.Size.Width || x0 > x1)
                {
                    diff = diff * -1;
                    x0 = x0 - diff - diff;
                    x1 = x1 + diff + diff;
                }
            }
            else if (Mstep == 2)
            {
                using (var graphics = Graphics.FromImage(boxesView))
                {
                    graphics.DrawLine(redPen, new Point(x0, y), new Point(x1, y));
                    graphics.DrawLine(redPen, new Point(x0, y0), new Point(x0, y1));
                    graphics.DrawLine(redPen, new Point(x1, y0), new Point(x1, y1));
                }
                y = y + diff;
                if (y < y0 || y > y1)
                {
                    diff = diff * -1;
                    y = y + diff + diff;
                }
            }
            else if (Mstep == 3)
            {
                using (var graphics = Graphics.FromImage(boxesView))
                {
                    graphics.DrawRectangle(redPen, new Rectangle(x0, y0, x1 - x0, y1 - y0));
                }
                y0 = y0 - diff;
                y1 = y1 + diff;
                if (y0 < 0 || y1 > plainView.Size.Height || y0 > y1)
                {
                    diff = diff * -1;
                    y0 = y0 - diff - diff;
                    y1 = y1 + diff + diff;
                }
            }
        }

        private void manualScanToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!isManual)
            {
                isManual = true;
                scanner = new Scanner(13);
                Mstep = 0;
                y0 = 0;
                y1 = plainView.Size.Height;
                x0 = 0;
                x1 = plainView.Size.Width;
                diff = 10;
                x = x0;
                this.objectDetectedLabel.Text = "Manually Scanning View";
                scale = (plainView.Size.Width - 1) / 600 + 1;
                scanner.NumObjects = 2;
                scanner.OnChange = manualScanNextBox;
            }
            else
            {
                scanner = new Scanner();
                scanner.OnChange = highlightNextBox;
                scanner.NumObjects = rectangles.Count;
                this.objectDetectedLabel.Text = rectangles.Count.ToString() + " objects detected";
                refreshTimer.Start();
                isManual = false;
            }
        }
    }
}
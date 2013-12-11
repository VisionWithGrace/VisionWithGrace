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
    enum ScanningMode{
        FREE_FORM,
        AUTO_DETECTION,
        MENU,
        STORED,
    }

    public partial class MainForm : Form
    {
        AdminPanel adminPanel = new AdminPanel();

        Bitmap plainView;
        Bitmap boxesView;
        Bitmap dullView;
        List<Rectangle> rectangles;
        ComputerVision cv;

        Scanner scanner = new Scanner();
        Timer refreshTimer = new Timer();

        int x, x0, x1, y, y0, y1, Mstep, diff, scale;
        ScanningMode scanningMode = ScanningMode.MENU;

        HelpForm helpForm;
        Color buttonColor;

        public MainForm()
        {
            InitializeComponent();
            buttonColor = buttonPanel.BackColor;


            // Initialize CV manager
            cv = new ComputerVision();

            // Set refresh defaults
            refreshTimer.Interval = 5000;
            refreshTimer.Interval = Properties.Settings.Default.refreshRate;
            refreshTimer.Tick += refreshView;

            // Set scanner defaults
            scanner.OnChange = MenuScan;

        }

        // Initialize Computer Vision and Kinect upon Load
        // Draw mockup GUI if no kinect is found
        private void Form1_Shown(object sender, EventArgs e)
        {
            this.mainDisplay.Focus();

            plainView = cv.getFrame();
            mainDisplay.Image = new Bitmap(plainView.Size.Width, plainView.Size.Height);
            using (Graphics g = Graphics.FromImage(mainDisplay.Image))
            {
                // overlay the boxedView on the plainView
                g.DrawImage(plainView, new Rectangle(0, 0, plainView.Width, plainView.Height));
            }
            // Open help splash screen
            if (Properties.Settings.Default.helpFlag)
            {
                helpForm = new HelpForm();
                helpForm.ShowDialog();
            }

            // refresh once at the beginning and then start the timer
            refreshView(sender, e);
            buttonPanel.BackColor = Color.Yellow;
            scanner.NumObjects = 3;


        }

        private void refreshView(object sender, EventArgs e)
        {
            plainView = cv.getFrame();
            rectangles = cv.getBoxes();

            scanner.NumObjects = rectangles.Count + 1;
            this.objectDetectedLabel.Text = rectangles.Count.ToString() + " objects detected";

            drawBoxes();
            drawViews();
            dullView = reduceSaturation(plainView);

        }

        public void highlightNextBox(object sender, EventArgs e)
        {
            int nextHighlighted = scanner.CurObject;

            if (boxesView == null)
                return;

            drawBoxes(nextHighlighted);
            drawViews();
        }
        public void MenuScan(object sender, EventArgs e)
        {
            button1.BackColor = buttonColor;
            button2.BackColor = buttonColor;
            button3.BackColor = buttonColor;
            if(scanner.CurObject == 0)
            {
                button1.BackColor = Color.LightBlue;
            }
            else if (scanner.CurObject == 1)
            {
                button2.BackColor = Color.LightBlue;
            }
            else if (scanner.CurObject == 2)
            {
                button3.BackColor = Color.LightBlue;
            }
        }
        
        // Display selected object in closeUpDisplay
        private void showSelectedObject(VObject detected)
        {
            SoundPlayer player = new SoundPlayer(@"C:\WINDOWS\Media\notify.wav");
            player.Play();

            VObject recognized;
            if (scanningMode == ScanningMode.AUTO_DETECTION)
            {
                recognized = cv.RecognizeObject(scanner.CurObject - 1);
            }
            else
            {
                recognized = cv.RecognizeObject(new Rectangle(x0, y0, x1 - x0, y1 - y0));
            }

            SelectedObjectForm selectedObjectForm;
            selectedObjectForm = new SelectedObjectForm(detected, recognized);
            selectedObjectForm.ShowDialog();
        }
        
        private void drawBoxes(int selected = -1)
        {
            if (boxesView != null)
                boxesView.Dispose();
            boxesView = new Bitmap(plainView.Size.Width, plainView.Size.Height);

            Pen redPen = new Pen(Properties.Settings.Default.boxColor, 3);
            Pen yellowPen = new Pen(Color.Yellow, 5);

            buttonPanel.BackColor = buttonColor;

            using (var graphics = Graphics.FromImage(boxesView))
            {
                if(selected != -1)
                {
                    graphics.DrawImage(dullView, new Rectangle(0, 0, plainView.Width, plainView.Height));
                }
                for (int i = 0; i < rectangles.Count; i++)
                {
                    //if (selected != -1 && i == selected)
                     //   graphics.DrawRectangle(yellowPen, rectangles[i]);
                    //else
                        graphics.DrawRectangle(redPen, rectangles[i]);
                }
                if (selected > 0)
                {
                    graphics.DrawImage(plainView, rectangles[selected - 1], rectangles[selected - 1], GraphicsUnit.Pixel);
                    graphics.DrawRectangle(yellowPen, rectangles[selected - 1]);
                }
                else if(selected == 0)
                {
                    buttonPanel.BackColor = Color.Yellow;
                }
            }
        }

        private void drawViews()
        {
            // Dispose of previously drawn image
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

        private void startScanning(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Space)
                return;

            // don't scan if there are no objects to scan through
            //if (rectangles.Count == 0 && scanningMode == ScanningMode.AUTO_DETECTION)
            //    return;

            e.SuppressKeyPress = true;
            refreshTimer.Stop();
            if (scanningMode == ScanningMode.AUTO_DETECTION) scanner.start(1);
            else scanner.start();
        }
        private void stopScanning(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Space)
                return;
            e.SuppressKeyPress = true;

            if (!scanner.isRunning)
                return;

            scanner.stop();

            this.labelTimeRemaining.Text = "";
            if (scanningMode == ScanningMode.MENU)
            {
                if (scanner.CurObject == 0) switchToAuto();
                else if (scanner.CurObject == 1) switchToManual();
                else if (scanner.CurObject == 2)
                {
                    StoredObjects View = new StoredObjects();
                    View.ShowDialog();
                }
            }
            else if (scanningMode == ScanningMode.FREE_FORM)
            {
                manualStep();
            }
            else if (scanner.CurObject == 0)
            {
                scanningMode = ScanningMode.MENU;
                scanner = new Scanner();
                scanner.NumObjects = 3;
                scanner.OnChange = MenuScan;
            }
            else
            {
                VObject vobj = new VObject();
                vobj.image = getImageInBox(rectangles[scanner.CurObject - 1]);
                showSelectedObject(vobj);
                refreshTimer.Start();
            }
        }
        private Bitmap reduceSaturation(Bitmap image)
        {
            Bitmap dull = new Bitmap(image.Width, image.Height);
            for(int x = 0; x < image.Width; x++)
            {
                for(int y = 0; y < image.Height; y++)
                {
                    Color pixelColor = image.GetPixel(x, y);
                    int G = (pixelColor.R + pixelColor.G + pixelColor.B) / 3;
                    Color newColor = Color.FromArgb((4 * G + 2 * pixelColor.R) / 9, (4 * G + 2 * pixelColor.G) / 9, (4 * G + 2 * pixelColor.B) / 9);
                    dull.SetPixel(x, y, newColor);
                }
            }

            return dull;
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
            if (adminPanel.Visible)
            {
                adminPanel.BringToFront();
            }
            else
            {
                if (adminPanel.IsDisposed)
                    adminPanel = new AdminPanel();
                adminPanel.Show();
            }
        }

        private void manualStep()
        {
            if (Mstep == 0)
            {
                x0 = x;
                x1 = x;
                diff = 1;
            }
            else if (Mstep == 1)
            {
                y = y0;
                diff = 2;
            }
            else if (Mstep == 2)
            {
                y0 = y;
                y1 = y;
                diff = 1;
            }
            else if (Mstep == 3)
            {
                VObject vObject = new VObject();
                vObject.image = getImageInBox(new Rectangle(x0, y0, x1 - x0, y1 - y0));
                showSelectedObject(vObject);
                scanningMode = ScanningMode.MENU;
                scanner = new Scanner();
                scanner.NumObjects = 3;
                scanner.OnChange = MenuScan;
                buttonPanel.BackColor = Color.Yellow;
                mainDisplay.Image = new Bitmap(plainView.Size.Width, plainView.Size.Height);
                using (Graphics g = Graphics.FromImage(mainDisplay.Image))
                {
                    // overlay the boxedView on the plainView
                    g.DrawImage(plainView, new Rectangle(0, 0, plainView.Width, plainView.Height));
                }
                //y0 = 0;
                //y1 = plainView.Size.Height;
                //x0 = 0;
                //x1 = plainView.Size.Width;
                //x = x0;
                //diff = 2;
                //Mstep = -1;
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

            Pen redPen = new Pen(Properties.Settings.Default.boxColor, 4);
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
                    graphics.DrawImage(dullView, new Rectangle(0, 0, plainView.Width, plainView.Height));
                    graphics.DrawImage(plainView, new Rectangle(x0, y0, x1 - x0, y1 - y0), new Rectangle(x0, y0, x1 - x0, y1 - y0), GraphicsUnit.Pixel);
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
                    graphics.DrawImage(dullView, new Rectangle(0, 0, plainView.Width, plainView.Height));
                    graphics.DrawImage(plainView, new Rectangle(x0, y0, x1 - x0, y1 - y0), new Rectangle(x0, y0, x1 - x0, y1 - y0), GraphicsUnit.Pixel);
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
                    graphics.DrawImage(dullView, new Rectangle(0, 0, plainView.Width, plainView.Height));
                    graphics.DrawImage(plainView, new Rectangle(x0, y0, x1 - x0, y1 - y0), new Rectangle(x0, y0, x1 - x0, y1 - y0), GraphicsUnit.Pixel);
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

            drawViews();
        }

        private void manualScanToolStripMenuItem_Click(object sender, EventArgs e)
        {
            switchToManual();
        }

        private void switchToManual()
        {
            scanningMode = ScanningMode.FREE_FORM;
            buttonPanel.BackColor = buttonColor;
            refreshTimer.Stop();
            if (boxesView != null)
                boxesView.Dispose();
            boxesView = new Bitmap(plainView.Size.Width, plainView.Size.Height);
            drawViews();

            scanner = new Scanner(13);
            Mstep = 0;
            y0 = 0;
            y1 = plainView.Size.Height;
            x0 = 0;
            x1 = plainView.Size.Width;
            diff = 2;
            x = x0;
            this.objectDetectedLabel.Text = "Free Form Scanning";
            scale = (plainView.Size.Width - 1) / 600 + 1;
            scanner.NumObjects = 3;
            scanner.OnChange = manualScanNextBox;
        }

        private void objectDetectionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            switchToAuto();
            refreshView(sender, e);
        }

        private void switchToAuto()
        {
            scanningMode = ScanningMode.AUTO_DETECTION;
            scanner = new Scanner();
            scanner.OnChange = highlightNextBox;
            scanner.NumObjects = rectangles.Count + 1;
            refreshTimer.Start();
        }

        // Listener for the settings button, updates refreshrate, scanrate, and box color
        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            settingsPanel settings = new settingsPanel();
            settings.ShowDialog();
            if (settings.DialogResult == DialogResult.OK)
            {
                refreshTimer.Interval = Properties.Settings.Default.refreshRate;
                drawBoxes();
                scanner.update_interval();
            }
        }

        private void computerVisionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.cv.OpenDebugWindow();
        }

        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            helpForm = new HelpForm();
            helpForm.Show();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

/**
 * This class does the automated image scanning from which Grace can select
 * objects by releasing the button
 * */
namespace VisionWithGrace
{
    class Scanner
    {
        Timer timer = new Timer();
        bool isHandlerSet = false;
        private int numObjects;
        private EventHandler onChange;
        private int curObject;

        public Scanner()
        {
            // Initialize timer to simulate 50 Kinect frames
            // 50 frames/tick * 30 FPS != 1667 ms/tick
            timer.Interval = Properties.Settings.Default.scanSpeed;
            timer.Tick += new EventHandler(on_tick);
            curObject = 0;
        }
        public Scanner(int interval)
        {
            timer.Interval = interval;
            timer.Tick += new EventHandler(on_tick);
            curObject = 0;
        }

        public void on_tick(object sender, EventArgs e)
        {
            if (numObjects == 0)
                return;

            curObject = (curObject + 1) % numObjects;

            if (isHandlerSet)
                onChange.Invoke(sender, e);
        }

        public void update_interval()
        {
            timer.Interval = Properties.Settings.Default.scanSpeed;
        }

        public EventHandler OnChange
        {
            set
            {
                onChange = value;
                if (value != null)
                    isHandlerSet = true;
            }
        }

        public int NumObjects
        {
            get
            {
                return numObjects;
            }
            set
            {
                numObjects = value;
                curObject = 0;
            }
        }

        public int CurObject
        {
            get
            {
                return curObject;
            }
        }

        public bool isRunning
        {
            get
            {
                return timer.Enabled;
            }
        }

        public void start(int index = 0)
        {
            if (timer.Enabled)
                return;
            if (index > numObjects) index = 0;
            curObject = index;
            timer.Start();
            onChange(null, new EventArgs());
        }
        public void stop()
        {
            timer.Stop();
        }
    }
}

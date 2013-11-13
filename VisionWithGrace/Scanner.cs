using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VisionWithGrace
{
    class Scanner
    {
        Timer timer = new Timer();
        bool isHandlerSet = false;

        public Scanner()
        {
            // Initialize timer to simulate 50 Kinect frames
            // 50 frames/tick * 30 FPS != 1667 ms/tick
            timer.Interval = 1667;
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

        private EventHandler onChange;
        public EventHandler OnChange
        {
            set
            {
                onChange = value;
                if (value != null)
                    isHandlerSet = true;
            }
        }

        private int numObjects;
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

        private int curObject;
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

        public void start()
        {
            if (timer.Enabled)
                return;

            timer.Start();
            onChange(null, new EventArgs());
        }
        public void stop()
        {
            timer.Stop();
        }
    }
}

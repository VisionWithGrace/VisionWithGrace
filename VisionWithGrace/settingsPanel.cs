using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VisionWithGrace
{
    public partial class settingsPanel : Form
    {
        public settingsPanel()
        {
            InitializeComponent();
        }

        private void refreshTrackBar_Scroll(object sender, EventArgs e)
        {
            Properties.Settings.Default.refreshRate = this.refreshTrackBar.Value;
            Properties.Settings.Default.Save();
        }

        private void scanTrackBar_Scroll(object sender, EventArgs e)
        {
            Properties.Settings.Default.scanSpeed = this.scanTrackBar.Value;
            Properties.Settings.Default.Save();
        }
    }
}

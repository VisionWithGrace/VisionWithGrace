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
            this.colorDropDown.SelectedItem = Properties.Settings.Default.boxColor.Name;
            string blah = Properties.Settings.Default.boxColor.Name;
        }

        private void refreshTrackBar_Scroll(object sender, EventArgs e)
        {
            Properties.Settings.Default.refreshRate = this.refreshTrackBar.Value;
        }

        private void scanTrackBar_Scroll(object sender, EventArgs e)
        {
            Properties.Settings.Default.scanSpeed = this.scanTrackBar.Value;
        }

        private void colorDropDown_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.colorDropDown.SelectedIndex == 0)
            {
                Properties.Settings.Default.boxColor = Color.Red;
            }
            else if (this.colorDropDown.SelectedIndex == 1)
            {
                Properties.Settings.Default.boxColor = Color.Blue;
            }
            else if (this.colorDropDown.SelectedIndex == 2)
            {
                Properties.Settings.Default.boxColor = Color.Green;
            }
            else if (this.colorDropDown.SelectedIndex == 3)
            {
                Properties.Settings.Default.boxColor = Color.Purple;
            }
            else if (this.colorDropDown.SelectedIndex == 4)
            {
                Properties.Settings.Default.boxColor = Color.Pink;
            }
            else if (this.colorDropDown.SelectedIndex == 5)
            {
                Properties.Settings.Default.boxColor = Color.White;
            }
            else if (this.colorDropDown.SelectedIndex == 6)
            {
                Properties.Settings.Default.boxColor = Color.Black;
            }
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            Properties.Settings.Default.Save();
            Close();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}

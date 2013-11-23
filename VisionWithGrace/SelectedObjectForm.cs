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
    public partial class SelectedObjectForm : Form
    {
        public SelectedObjectForm(Bitmap image)
        {
            InitializeComponent();

            this.pictureBox1.Image = image;
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            VObject obj = new VObject();

            obj.image = this.pictureBox1.Image as Bitmap;
            
            // Get name, default to "Unnamed Object"
            obj.name = this.textBoxName.Text;
            if (obj.name == "")
                obj.name = "Unnamed Object";

            // Parse tags
            string tagString = this.textBoxTags.Text;
            obj.tags = tagString.Split(' ').ToList<string>();

            obj.save();

            // close form
            this.Close();
        }

        private void buttonDiscard_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

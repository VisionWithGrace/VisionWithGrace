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
        public SelectedObjectForm(Bitmap image, string name, List<string> tags)
        {
            InitializeComponent();

            this.pictureBox1.Image = image;
            if (name != null)
            {
                this.Text = name + " selected!";
                this.textBoxName.Text = name;

                //Serialize tags
                string tags_string = "";
                foreach(string tag in tags)
                {
                    tags_string += (tag + ", ");
                }
                this.textBoxTags.Text = tags_string;
            }
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

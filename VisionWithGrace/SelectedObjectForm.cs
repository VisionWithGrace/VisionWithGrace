using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using DatabaseModule;

namespace VisionWithGrace
{
    public partial class SelectedObjectForm : Form
    {
        DatabaseInterface dbInterface = new DatabaseInterface();

        public SelectedObjectForm(Bitmap image, string name, List<string> tags)
        {
            InitializeComponent();

            this.pictureBox1.Image = image;
            if (name != null)
            {
                this.Text = name + " selected!";
                this.vObjectForm1.VObjectName = name;
                this.vObjectForm1.VObjectTags = tags;
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            VObject obj = new VObject();

            obj.image = this.pictureBox1.Image as Bitmap;
            obj.name  = this.vObjectForm1.VObjectName;
            obj.tags  = this.vObjectForm1.VObjectTags;
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

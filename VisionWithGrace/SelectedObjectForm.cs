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
        VObject selectedVObject;

        public SelectedObjectForm(VObject obj)
        {
            InitializeComponent();
            selectedVObject = obj;

            this.pictureBox1.Image = obj.image;
            if (obj.name != "")
            {
                this.Text = obj.name + " selected!";
            }
            this.vObjectForm1.setVObject(obj);
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            selectedVObject.name = this.vObjectForm1.VObjectName;
            selectedVObject.tags = this.vObjectForm1.VObjectTags;
            selectedVObject.save();

            // close form
            this.Close();
        }

        private void buttonDiscard_Click(object sender, EventArgs e)
        {
            this.Close();
        }





    }
}

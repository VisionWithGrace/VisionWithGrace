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
        VObject recognizedObject;
        VObject detectedObject;

        public SelectedObjectForm(VObject detectedObj, VObject recognizedObj)
        {
            InitializeComponent();

            detectedObject = detectedObj;
            recognizedObject = recognizedObj;

            if (recognizedObject != null)
            {
                radioButtonDetectedObject.Visible = true;
                radioButtonRecognizedObject.Visible = true;
                radioButtonRecognizedObject.Checked = true;
                this.labelTitle.Text = "Did you select a '" + recognizedObj.name + "'?";
                showObject(recognizedObject);
            }
            else
            {
                this.labelTitle.Text = "I don't recognize this object.";
                showObject(detectedObject);
            }
        }

        private void showObject(VObject obj)
        {
            this.vObjectForm1.setVObject(obj);
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (radioButtonRecognizedObject.Checked && recognizedObject != null)
            {
                recognizedObject.name = vObjectForm1.VObjectName;
                recognizedObject.tags = vObjectForm1.VObjectTags;
                recognizedObject.save();
            }
            else
            {
                detectedObject.name = this.vObjectForm1.VObjectName;
                detectedObject.tags = this.vObjectForm1.VObjectTags;
                detectedObject.save();
            }

            // close form
            this.Close();
        }

        private void buttonDiscard_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void radioButtonDetectedObject_CheckedChanged(object sender, EventArgs e)
        {
            this.labelTitle.Text = "This is an object I haven't seen before.";
            showObject(detectedObject);
        }

        private void radioButtonRecognizedObject_CheckedChanged(object sender, EventArgs e)
        {
            this.labelTitle.Text = "This is a '" + recognizedObject.name + "'.";
            showObject(recognizedObject);
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using DatabaseModule;

namespace VisionWithGrace
{
    public partial class VObjectForm : UserControl
    {
        DatabaseInterface dbInterface = new DatabaseInterface();

        public VObjectForm()
        {
            InitializeComponent();
            loadAutocompleteOptions();
            this.listBoxTags.Items.Clear();
        }

        public void setVObject(VObject obj)
        {
            this.textBoxName.Text = obj.name;
            this.listBoxTags.Items.Clear();
            this.listBoxTags.Items.AddRange(obj.tags.ToArray());
        }

        public string VObjectName
        {
            get
            {
                return this.textBoxName.Text;
            }
            set
            {
                this.textBoxName.Text = value;
            }
        }

        public List<string> VObjectTags
        {
            get
            {
                List<string> tags = new List<string>();
                foreach (string tag in this.listBoxTags.Items)
                {
                    tags.Add(tag);
                }
                return tags;
            }
            set
            {
                foreach (string tag in value)
                {
                    listBoxTags.Items.Add(tag);
                }
            }
        }

        private void loadAutocompleteOptions()
        {
            List<string> allTags = dbInterface.getAllTags();
            var source = new AutoCompleteStringCollection();
            source.AddRange(allTags.ToArray());
            this.textBoxTag.AutoCompleteCustomSource = source;
        }

        private void addTag(string tag)
        {
            if (tag.Trim().Length == 0)
                return;

            this.listBoxTags.Items.Add(tag);
        }

        private void buttonAddTag_Click(object sender, EventArgs e)
        {
            addTag(this.textBoxTag.Text);
            this.textBoxTag.Text = "";
        }

        private void buttonRemoveTag_Click(object sender, EventArgs e)
        {
            if (this.listBoxTags.SelectedIndex != -1)
                this.listBoxTags.Items.RemoveAt(this.listBoxTags.SelectedIndex);
        }

        private void textBoxTag_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                addTag(this.textBoxTag.Text);
                this.textBoxTag.Text = "";
                e.SuppressKeyPress = true;
            }
        }
    }
}

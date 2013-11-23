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
    public partial class AdminPanel : Form
    {
        DatabaseInterface dbInterface = new DatabaseInterface("VObjects");
        Database db = new Database();

        List<VObject> objects;

        public AdminPanel()
        {
            InitializeComponent();
        }

        // Do something upon click on object list
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int selectedIndex = this.listBoxObjects.SelectedIndex;
            VObject selectedVObject = objects[selectedIndex];

            this.textBoxName.Text = selectedVObject.name;

            this.listBoxTags.Items.Clear();
            if (selectedVObject.tags != null)
            {
                this.listBoxTags.Items.AddRange(selectedVObject.tags.ToArray());
            }
        }

        private void textBoxTags_TextChanged(object sender, EventArgs e)
        {
            if (!textBoxTag.Focused)
                return;

            // Check if what the user is typing is already in the set of used tags

            // get last word
            TextBox tb = sender as TextBox;

            if (tb == null)
                return;

            int caret_pos = tb.SelectionStart;
            int word_start = tb.Text.LastIndexOf(" ", caret_pos) + 1;

            if (word_start == caret_pos)
                return;

            string cur_word = tb.Text.Substring(word_start, caret_pos - word_start);

            // query DB for cur_word
            var source = new AutoCompleteStringCollection();
            source.AddRange(db.getSimilarTags(cur_word));
            tb.AutoCompleteCustomSource = source;
        }

        private void textBoxTag_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                this.listBoxTags.Items.Add(this.textBoxTag.Text);
                this.textBoxTag.Text = "";
                e.SuppressKeyPress = true;
            }
        }

        private void buttonAddTag_Click(object sender, EventArgs e)
        {
            this.listBoxTags.Items.Add(this.textBoxTag.Text);
            this.textBoxTag.Text = "";
        }

        private void buttonRemoveTag_Click(object sender, EventArgs e)
        {
            if (this.listBoxTags.SelectedIndex != -1)
                this.listBoxTags.Items.RemoveAt(this.listBoxTags.SelectedIndex);
        }

        private void refreshObjectsInView()
        {
            listBoxObjects.Items.Clear();
            foreach (VObject item in objects)
            {
                this.listBoxObjects.Items.Add(item.name);
            }
        }

        private List<VObject> convertDictionariesToVObjects(List<Dictionary<string, object>> objects)
        {
            List<VObject> vObjects = new List<VObject>();
            foreach (Dictionary<string, object> item in objects)
            {
                vObjects.Add(new VObject(item));
            }
            return vObjects;
        }

        private void AdminPanel_Load(object sender, EventArgs e)
        {
            objects = convertDictionariesToVObjects(dbInterface.getUnnamedObjects());
            refreshObjectsInView();
        }

        private void recentObjectsButton_Click(object sender, EventArgs e)
        {
            //objects = dbInterface.getRecentObjects();
            refreshObjectsInView();
        }

        private void allObjectsButton_Click(object sender, EventArgs e)
        {
        }
    }
}

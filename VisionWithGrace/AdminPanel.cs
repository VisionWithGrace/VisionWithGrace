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

/**
 * Admin Panel form from which database entries can be read or written
 * */
namespace VisionWithGrace
{
    public partial class AdminPanel : Form
    {
        DatabaseInterface dbInterface = new DatabaseInterface();
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
            List<string> similarTags = dbInterface.getAllTags();
            source.AddRange(similarTags.ToArray());
            tb.AutoCompleteCustomSource = source;
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

        private void buttonAddTag_Click(object sender, EventArgs e)
        {
            addTag(this.textBoxTag.Text);
            this.textBoxTag.Text = "";
        }

        private void addTag(string tag)
        {
            if (tag.Trim().Length == 0)
                return;

            this.listBoxTags.Items.Add(tag);
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
            objects = convertDictionariesToVObjects(dbInterface.getAllObjects());
            refreshObjectsInView();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            // get currently selected object
            int index = this.listBoxTags.SelectedIndex;
            VObject current = objects[index];
            current.name = this.textBoxName.Text;
            current.tags.Clear();
            current.tags.AddRange(this.listBoxTags.Items as IEnumerable<string>);
        }
    }
}

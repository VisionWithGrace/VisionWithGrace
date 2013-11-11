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
    public partial class AdminPanel : Form
    {
        Database db = new Database();

        public AdminPanel()
        {
            InitializeComponent();
        }

        // Do something upon click on object list
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBoxTags_TextChanged(object sender, EventArgs e)
        {
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
    }
}

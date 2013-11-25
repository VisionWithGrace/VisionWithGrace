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

            if (selectedIndex == -1)
                return;

            VObject selectedVObject = objects[selectedIndex];

            this.vObjectForm1.setVObject(selectedVObject);
        }

        private void refreshObjectsInView()
        {
            listBoxObjects.Items.Clear();
            foreach (VObject item in objects)
            {
                if (item.name == "")
                    this.listBoxObjects.Items.Add("Unnamed Object");
                else
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
            objects = dbInterface.getUnnamedObjects();
            refreshObjectsInView();
        }

        private void recentObjectsButton_Click(object sender, EventArgs e)
        {
            //objects = dbInterface.getRecentObjects();
            refreshObjectsInView();
        }

        private void allObjectsButton_Click(object sender, EventArgs e)
        {
            objects = dbInterface.getAllObjects();
            refreshObjectsInView();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            // get currently selected object
            int index = this.listBoxObjects.SelectedIndex;
            VObject current = objects[index];
            current.name = this.vObjectForm1.VObjectName;
            current.tags = this.vObjectForm1.VObjectTags;
            current.save();
        }
    }
}

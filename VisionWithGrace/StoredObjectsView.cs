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

    enum ScannerMode
    {
        TAGS,
        IMAGES
    }

    public partial class StoredObjects : Form
    {
        DatabaseInterface db = new DatabaseInterface();
        List<VObject> AllObjects;
        List<String> AllTags;
        List<List<int>> TagObjectIndexes;
        List<TableLayoutPanel> tables;
        List<Label> tagLabels;
        Scanner scanner;
        ScannerMode scanMode = ScannerMode.TAGS;
        int currentTag;
        List<int> currentObject;
        //TableLayoutPanel tPanel;

        public StoredObjects()
        {
            InitializeComponent();
            AllObjects = db.getAllUniqueObjects();
            AllTags = db.getAllTags();
            tables = new List<TableLayoutPanel>();
            tagLabels = new List<Label>();
            //tPanel = new TableLayoutPanel();
            tableLayoutPanel1.RowCount = 1;
            tableLayoutPanel1.ColumnCount = 1;
            tableLayoutPanel1.Dock = DockStyle.Fill;
            label1.Text = AllObjects.Count.ToString();
            TagObjectIndexes = new List<List<int>>();
            currentObject = new List<int>();

            currentTag = 0;
            for (int i = 0; i < AllTags.Count + 2; i++) currentObject.Add(0);



                for (int i = 0; i < AllTags.Count; i++)
                {
                    List<int> tempList = new List<int>();
                    for (int j = 0; j < AllObjects.Count; j++)
                    {
                        for (int k = 0; k < AllObjects[j].tags.Count; k++)
                        {
                            if (AllObjects[j].tags[k] == AllTags[i]) tempList.Add(j);
                        }
                    }
                    TagObjectIndexes.Add(tempList);
                }

            tableLayoutPanel4.RowCount = 1;
            tableLayoutPanel4.ColumnCount = 1;
            tableLayoutPanel4.Dock = DockStyle.Fill;
            Label exitlabel = new Label();
            Label allLabel = new Label();
            exitlabel.Text = "Exit";
            allLabel.Text = "All Objects";
            exitlabel.Font = new Font("Arial", 16);
            allLabel.Font = new Font("Arial", 16);
            exitlabel.AutoSize = true;
            exitlabel.Dock = DockStyle.Fill;
            allLabel.AutoSize = true;
            allLabel.Dock = DockStyle.Fill;
            tagLabels.Add(exitlabel);
            tagLabels.Add(allLabel);
            tableLayoutPanel4.Controls.Add(exitlabel);
            tableLayoutPanel4.Controls.Add(allLabel);
            for (int i = 0; i < AllTags.Count; i++)
            {
                Label tBox = new Label();
                tBox.Text = AllTags[i];
                tBox.Font = new Font("Arial", 16);
                tBox.AutoSize = true;
                tBox.Dock = DockStyle.Fill;
                tableLayoutPanel4.Controls.Add(tBox);
                tagLabels.Add(tBox);
            }
            tableLayoutPanel4.Controls.Add(new Label());

            scanner = new Scanner();
            scanner.NumObjects = AllTags.Count + 2;
            scanner.OnChange = focusTag;


        }
        private void focusObject(object sender, EventArgs e)
        {
            if (scanner.NumObjects > 0)
            {
                tableLayoutPanel1.ScrollControlIntoView(tables[scanner.CurObject]);
                tables[scanner.CurObject].BackColor = Color.Red;
                if (scanner.CurObject > 0) tables[scanner.CurObject - 1].BackColor = tableLayoutPanel1.BackColor;
                else if (scanner.NumObjects > 1) tables[AllObjects.Count - 1].BackColor = tableLayoutPanel1.BackColor;
            }
        }
        private void focusTag(object sender, EventArgs e)
        {
            tableLayoutPanel4.ScrollControlIntoView(tagLabels[scanner.CurObject]);
            tagLabels[scanner.CurObject].BackColor = Color.Red;
            if (scanner.CurObject > 0) tagLabels[scanner.CurObject - 1].BackColor = tableLayoutPanel4.BackColor;
            else if (scanner.NumObjects > 1) tagLabels[AllTags.Count + 1].BackColor = tableLayoutPanel4.BackColor;
            currentTag = scanner.CurObject;
            tables.Clear();
            tableLayoutPanel1.Controls.Clear();
            if (currentTag == 1)
            {
                for (int i = 0; (i < AllObjects.Count)&&(i < 3); i++)
                {
                    TableLayoutPanel tPanel = new TableLayoutPanel();
                    tPanel.Dock = DockStyle.Fill;
                    tPanel.Size = new System.Drawing.Size(410, 500);
                    tPanel.RowCount = 2;
                    tPanel.ColumnCount = 1;
                    PictureBox pBox = new PictureBox();
                    pBox.Size = new System.Drawing.Size(400, 400);
                    pBox.BorderStyle = BorderStyle.Fixed3D;
                    pBox.BackColor = Color.Gray;
                    pBox.SizeMode = PictureBoxSizeMode.Zoom;
                    pBox.Image = AllObjects[i].image;
                    tPanel.Controls.Add(pBox, 0, 0);
                    Label tBox = new Label();
                    tBox.Text = AllObjects[i].name;
                    tBox.AutoSize = true;
                    tBox.Font = new Font("Arial", 28);
                    tPanel.Controls.Add(tBox, 0, 1);
                    tableLayoutPanel1.Controls.Add(tPanel);
                    tables.Add(tPanel);
                }
                tableLayoutPanel1.Controls.Add(new Label());
            }
            else if (currentTag > 1)
            {
                for (int i = 0; (i < TagObjectIndexes[currentTag - 2].Count)&&(i < 3); i++)
                {
                    TableLayoutPanel tPanel = new TableLayoutPanel();
                    tPanel.Dock = DockStyle.Fill;
                    tPanel.Size = new System.Drawing.Size(410, 500);
                    tPanel.RowCount = 2;
                    tPanel.ColumnCount = 1;
                    PictureBox pBox = new PictureBox();
                    pBox.Size = new System.Drawing.Size(400, 400);
                    pBox.BorderStyle = BorderStyle.Fixed3D;
                    pBox.BackColor = Color.Gray;
                    pBox.SizeMode = PictureBoxSizeMode.Zoom;
                    pBox.Image = AllObjects[TagObjectIndexes[currentTag - 2][i]].image;
                    tPanel.Controls.Add(pBox, 0, 0);
                    Label tBox = new Label();
                    tBox.Text = AllObjects[TagObjectIndexes[currentTag - 2][i]].name;
                    tBox.AutoSize = true;
                    tBox.Font = new Font("Arial", 28);
                    tPanel.Controls.Add(tBox, 0, 1);
                    tableLayoutPanel1.Controls.Add(tPanel);
                    tables.Add(tPanel);
                }
                tableLayoutPanel1.Controls.Add(new Label());
            }
        }
        private void startScanning(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Space)
                return;

            // don't scan if there are no objects to scan through
            //if (rectangles.Count == 0 && scanningMode == ScanningMode.AUTO_DETECTION)
            //    return;

            e.SuppressKeyPress = true;
            if (scanMode == ScannerMode.IMAGES)
                scanner.start(currentObject[currentTag]);
            else scanner.start(currentTag);
        }
        private void stopScanning(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Space)
                return;
            e.SuppressKeyPress = true;

            if (!scanner.isRunning)
                return;

            scanner.stop();
            if (scanMode == ScannerMode.IMAGES)
            {
                SelectedObjectForm selectedObjectForm;
                if (currentTag == 1) selectedObjectForm = new SelectedObjectForm(AllObjects[scanner.CurObject], AllObjects[scanner.CurObject]);
                else selectedObjectForm = new SelectedObjectForm(AllObjects[TagObjectIndexes[currentTag - 2][scanner.CurObject]], AllObjects[TagObjectIndexes[currentTag - 2][scanner.CurObject]]);
                selectedObjectForm.ShowDialog();
                tables[scanner.CurObject].BackColor = tableLayoutPanel1.BackColor;
                currentObject[currentTag] = scanner.CurObject;
                scanMode = ScannerMode.TAGS;
                scanner.NumObjects = AllTags.Count + 2;
                scanner.OnChange = focusTag;
            }
            else
            {
                currentTag = scanner.CurObject;
                if (currentTag == 0)
                {
                    this.Close();
                    return;
                }
                scanMode = ScannerMode.IMAGES;
                scanner.OnChange = focusObject;
                tables.Clear();
                tableLayoutPanel1.Controls.Clear();
                if (currentTag == 1)
                {
                    for (int i = 0; i < AllObjects.Count; i++)
                    {
                        TableLayoutPanel tPanel = new TableLayoutPanel();
                        tPanel.Dock = DockStyle.Fill;
                        tPanel.Size = new System.Drawing.Size(410, 500);
                        tPanel.RowCount = 2;
                        tPanel.ColumnCount = 1;
                        PictureBox pBox = new PictureBox();
                        pBox.Size = new System.Drawing.Size(400, 400);
                        pBox.BorderStyle = BorderStyle.Fixed3D;
                        pBox.BackColor = Color.Gray;
                        pBox.SizeMode = PictureBoxSizeMode.Zoom;
                        pBox.Image = AllObjects[i].image;
                        tPanel.Controls.Add(pBox, 0, 0);
                        Label tBox = new Label();
                        tBox.Text = AllObjects[i].name;
                        tBox.AutoSize = true;
                        tBox.Font = new Font("Arial", 28);
                        tPanel.Controls.Add(tBox, 0, 1);
                        tableLayoutPanel1.Controls.Add(tPanel);
                        tables.Add(tPanel);
                    }
                    tableLayoutPanel1.Controls.Add(new Label());
                    scanner.NumObjects = AllObjects.Count;
                }
                else
                {
                    for (int i = 0; i < TagObjectIndexes[currentTag - 2].Count; i++)
                    {
                        TableLayoutPanel tPanel = new TableLayoutPanel();
                        tPanel.Dock = DockStyle.Fill;
                        tPanel.Size = new System.Drawing.Size(410, 500);
                        tPanel.RowCount = 2;
                        tPanel.ColumnCount = 1;
                        PictureBox pBox = new PictureBox();
                        pBox.Size = new System.Drawing.Size(400, 400);
                        pBox.BorderStyle = BorderStyle.Fixed3D;
                        pBox.BackColor = Color.Gray;
                        pBox.SizeMode = PictureBoxSizeMode.Zoom;
                        pBox.Image = AllObjects[TagObjectIndexes[currentTag - 2][i]].image;
                        tPanel.Controls.Add(pBox, 0, 0);
                        Label tBox = new Label();
                        tBox.Text = AllObjects[TagObjectIndexes[currentTag - 2][i]].name;
                        tBox.AutoSize = true;
                        tBox.Font = new Font("Arial", 28);
                        tPanel.Controls.Add(tBox, 0, 1);
                        tableLayoutPanel1.Controls.Add(tPanel);
                        tables.Add(tPanel);
                    }
                    tableLayoutPanel1.Controls.Add(new Label());
                    scanner.NumObjects = TagObjectIndexes[currentTag - 2].Count;
                }
            }

            
        }
    }
}

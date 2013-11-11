namespace VisionWithGrace
{
    partial class AdminPanel
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.listBoxObjects = new System.Windows.Forms.ListBox();
            this.recentObjectsButton = new System.Windows.Forms.Button();
            this.allObjectsButton = new System.Windows.Forms.Button();
            this.textBoxName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxTag = new System.Windows.Forms.TextBox();
            this.listBoxTags = new System.Windows.Forms.ListBox();
            this.buttonAddTag = new System.Windows.Forms.Button();
            this.buttonRemoveTag = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // listBoxObjects
            // 
            this.listBoxObjects.FormattingEnabled = true;
            this.listBoxObjects.Items.AddRange(new object[] {
            "Book",
            "Table",
            "Backpack",
            "Ball",
            "Dinosaur",
            "Mom"});
            this.listBoxObjects.Location = new System.Drawing.Point(12, 12);
            this.listBoxObjects.Name = "listBoxObjects";
            this.listBoxObjects.Size = new System.Drawing.Size(316, 290);
            this.listBoxObjects.TabIndex = 0;
            this.listBoxObjects.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
            // 
            // recentObjectsButton
            // 
            this.recentObjectsButton.Location = new System.Drawing.Point(114, 308);
            this.recentObjectsButton.Name = "recentObjectsButton";
            this.recentObjectsButton.Size = new System.Drawing.Size(140, 58);
            this.recentObjectsButton.TabIndex = 1;
            this.recentObjectsButton.Text = "Get Recent Objects";
            this.recentObjectsButton.UseVisualStyleBackColor = true;
            // 
            // allObjectsButton
            // 
            this.allObjectsButton.Location = new System.Drawing.Point(329, 308);
            this.allObjectsButton.Name = "allObjectsButton";
            this.allObjectsButton.Size = new System.Drawing.Size(150, 58);
            this.allObjectsButton.TabIndex = 2;
            this.allObjectsButton.Text = "Get All Objects";
            this.allObjectsButton.UseVisualStyleBackColor = true;
            // 
            // textBoxName
            // 
            this.textBoxName.Location = new System.Drawing.Point(375, 12);
            this.textBoxName.Name = "textBoxName";
            this.textBoxName.Size = new System.Drawing.Size(202, 20);
            this.textBoxName.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(334, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Name:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(338, 42);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(34, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Tags:";
            // 
            // textBoxTag
            // 
            this.textBoxTag.AcceptsTab = true;
            this.textBoxTag.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.textBoxTag.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.textBoxTag.Location = new System.Drawing.Point(375, 39);
            this.textBoxTag.Name = "textBoxTag";
            this.textBoxTag.Size = new System.Drawing.Size(202, 20);
            this.textBoxTag.TabIndex = 6;
            this.textBoxTag.TextChanged += new System.EventHandler(this.textBoxTags_TextChanged);
            this.textBoxTag.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxTag_KeyDown);
            // 
            // listBoxTags
            // 
            this.listBoxTags.FormattingEnabled = true;
            this.listBoxTags.Items.AddRange(new object[] {
            ""});
            this.listBoxTags.Location = new System.Drawing.Point(375, 65);
            this.listBoxTags.Name = "listBoxTags";
            this.listBoxTags.Size = new System.Drawing.Size(202, 108);
            this.listBoxTags.TabIndex = 7;
            // 
            // buttonAddTag
            // 
            this.buttonAddTag.Location = new System.Drawing.Point(351, 65);
            this.buttonAddTag.Name = "buttonAddTag";
            this.buttonAddTag.Size = new System.Drawing.Size(18, 23);
            this.buttonAddTag.TabIndex = 8;
            this.buttonAddTag.Text = "+";
            this.buttonAddTag.UseVisualStyleBackColor = true;
            this.buttonAddTag.Click += new System.EventHandler(this.buttonAddTag_Click);
            // 
            // buttonRemoveTag
            // 
            this.buttonRemoveTag.Location = new System.Drawing.Point(351, 94);
            this.buttonRemoveTag.Name = "buttonRemoveTag";
            this.buttonRemoveTag.Size = new System.Drawing.Size(18, 23);
            this.buttonRemoveTag.TabIndex = 9;
            this.buttonRemoveTag.Text = "-";
            this.buttonRemoveTag.UseVisualStyleBackColor = true;
            this.buttonRemoveTag.Click += new System.EventHandler(this.buttonRemoveTag_Click);
            // 
            // AdminPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(589, 378);
            this.Controls.Add(this.buttonRemoveTag);
            this.Controls.Add(this.buttonAddTag);
            this.Controls.Add(this.listBoxTags);
            this.Controls.Add(this.textBoxTag);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBoxName);
            this.Controls.Add(this.allObjectsButton);
            this.Controls.Add(this.recentObjectsButton);
            this.Controls.Add(this.listBoxObjects);
            this.Name = "AdminPanel";
            this.Text = "Form2";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox listBoxObjects;
        private System.Windows.Forms.Button recentObjectsButton;
        private System.Windows.Forms.Button allObjectsButton;
        private System.Windows.Forms.TextBox textBoxName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxTag;
        private System.Windows.Forms.ListBox listBoxTags;
        private System.Windows.Forms.Button buttonAddTag;
        private System.Windows.Forms.Button buttonRemoveTag;


    }
}
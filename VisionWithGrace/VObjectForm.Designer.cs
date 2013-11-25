namespace VisionWithGrace
{
    partial class VObjectForm
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.buttonRemoveTag = new System.Windows.Forms.Button();
            this.buttonAddTag = new System.Windows.Forms.Button();
            this.listBoxTags = new System.Windows.Forms.ListBox();
            this.textBoxTag = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.labelName = new System.Windows.Forms.Label();
            this.textBoxName = new System.Windows.Forms.TextBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonRemoveTag
            // 
            this.buttonRemoveTag.Location = new System.Drawing.Point(423, 81);
            this.buttonRemoveTag.Name = "buttonRemoveTag";
            this.buttonRemoveTag.Size = new System.Drawing.Size(18, 23);
            this.buttonRemoveTag.TabIndex = 23;
            this.buttonRemoveTag.Text = "-";
            this.buttonRemoveTag.UseVisualStyleBackColor = true;
            this.buttonRemoveTag.Click += new System.EventHandler(this.buttonRemoveTag_Click);
            // 
            // buttonAddTag
            // 
            this.buttonAddTag.Location = new System.Drawing.Point(423, 52);
            this.buttonAddTag.Name = "buttonAddTag";
            this.buttonAddTag.Size = new System.Drawing.Size(18, 23);
            this.buttonAddTag.TabIndex = 22;
            this.buttonAddTag.Text = "+";
            this.buttonAddTag.UseVisualStyleBackColor = true;
            this.buttonAddTag.Click += new System.EventHandler(this.buttonAddTag_Click);
            // 
            // listBoxTags
            // 
            this.listBoxTags.FormattingEnabled = true;
            this.listBoxTags.Items.AddRange(new object[] {
            ""});
            this.listBoxTags.Location = new System.Drawing.Point(447, 52);
            this.listBoxTags.Name = "listBoxTags";
            this.listBoxTags.Size = new System.Drawing.Size(202, 108);
            this.listBoxTags.TabIndex = 21;
            // 
            // textBoxTag
            // 
            this.textBoxTag.AcceptsTab = true;
            this.textBoxTag.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.textBoxTag.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.textBoxTag.Location = new System.Drawing.Point(447, 26);
            this.textBoxTag.Name = "textBoxTag";
            this.textBoxTag.Size = new System.Drawing.Size(202, 20);
            this.textBoxTag.TabIndex = 20;
            this.textBoxTag.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxTag_KeyDown);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(410, 29);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(34, 13);
            this.label2.TabIndex = 19;
            this.label2.Text = "Tags:";
            // 
            // labelName
            // 
            this.labelName.AutoSize = true;
            this.labelName.Location = new System.Drawing.Point(403, 3);
            this.labelName.Name = "labelName";
            this.labelName.Size = new System.Drawing.Size(38, 13);
            this.labelName.TabIndex = 18;
            this.labelName.Text = "Name:";
            // 
            // textBoxName
            // 
            this.textBoxName.Location = new System.Drawing.Point(447, 0);
            this.textBoxName.Name = "textBoxName";
            this.textBoxName.Size = new System.Drawing.Size(202, 20);
            this.textBoxName.TabIndex = 17;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(400, 400);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 24;
            this.pictureBox1.TabStop = false;
            // 
            // VObjectForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.buttonRemoveTag);
            this.Controls.Add(this.buttonAddTag);
            this.Controls.Add(this.listBoxTags);
            this.Controls.Add(this.textBoxTag);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.labelName);
            this.Controls.Add(this.textBoxName);
            this.Controls.Add(this.pictureBox1);
            this.Name = "VObjectForm";
            this.Size = new System.Drawing.Size(650, 401);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonRemoveTag;
        private System.Windows.Forms.Button buttonAddTag;
        private System.Windows.Forms.ListBox listBoxTags;
        private System.Windows.Forms.TextBox textBoxTag;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label labelName;
        private System.Windows.Forms.TextBox textBoxName;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}

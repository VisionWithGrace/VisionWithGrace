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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AdminPanel));
            this.listBoxObjects = new System.Windows.Forms.ListBox();
            this.recentObjectsButton = new System.Windows.Forms.Button();
            this.allObjectsButton = new System.Windows.Forms.Button();
            this.buttonSave = new System.Windows.Forms.Button();
            this.buttonDelete = new System.Windows.Forms.Button();
            this.vObjectForm1 = new VisionWithGrace.VObjectForm();
            this.SuspendLayout();
            // 
            // listBoxObjects
            // 
            this.listBoxObjects.FormattingEnabled = true;
            this.listBoxObjects.Location = new System.Drawing.Point(12, 12);
            this.listBoxObjects.Name = "listBoxObjects";
            this.listBoxObjects.Size = new System.Drawing.Size(316, 290);
            this.listBoxObjects.TabIndex = 0;
            this.listBoxObjects.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
            // 
            // recentObjectsButton
            // 
            this.recentObjectsButton.Location = new System.Drawing.Point(12, 308);
            this.recentObjectsButton.Name = "recentObjectsButton";
            this.recentObjectsButton.Size = new System.Drawing.Size(140, 58);
            this.recentObjectsButton.TabIndex = 1;
            this.recentObjectsButton.Text = "Get Unnamed Objects";
            this.recentObjectsButton.UseVisualStyleBackColor = true;
            this.recentObjectsButton.Click += new System.EventHandler(this.unnamedObjectsButton_Click);
            // 
            // allObjectsButton
            // 
            this.allObjectsButton.Location = new System.Drawing.Point(178, 308);
            this.allObjectsButton.Name = "allObjectsButton";
            this.allObjectsButton.Size = new System.Drawing.Size(150, 58);
            this.allObjectsButton.TabIndex = 2;
            this.allObjectsButton.Text = "Get All Objects";
            this.allObjectsButton.UseVisualStyleBackColor = true;
            this.allObjectsButton.Click += new System.EventHandler(this.allObjectsButton_Click);
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(885, 176);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(100, 40);
            this.buttonSave.TabIndex = 10;
            this.buttonSave.Text = "Update Object";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // buttonDelete
            // 
            this.buttonDelete.Location = new System.Drawing.Point(779, 176);
            this.buttonDelete.Name = "buttonDelete";
            this.buttonDelete.Size = new System.Drawing.Size(100, 40);
            this.buttonDelete.TabIndex = 12;
            this.buttonDelete.Text = "Delete Object";
            this.buttonDelete.UseVisualStyleBackColor = true;
            this.buttonDelete.Click += new System.EventHandler(this.buttonDelete_Click);
            // 
            // vObjectForm1
            // 
            this.vObjectForm1.Location = new System.Drawing.Point(334, 12);
            this.vObjectForm1.Name = "vObjectForm1";
            this.vObjectForm1.Size = new System.Drawing.Size(651, 402);
            this.vObjectForm1.TabIndex = 11;
            this.vObjectForm1.VObjectName = "";
            this.vObjectForm1.VObjectTags = ((System.Collections.Generic.List<string>)(resources.GetObject("vObjectForm1.VObjectTags")));
            // 
            // AdminPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(990, 418);
            this.Controls.Add(this.buttonDelete);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.allObjectsButton);
            this.Controls.Add(this.recentObjectsButton);
            this.Controls.Add(this.listBoxObjects);
            this.Controls.Add(this.vObjectForm1);
            this.Name = "AdminPanel";
            this.Text = "Form2";
            this.Load += new System.EventHandler(this.AdminPanel_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox listBoxObjects;
        private System.Windows.Forms.Button recentObjectsButton;
        private System.Windows.Forms.Button allObjectsButton;
        private System.Windows.Forms.Button buttonSave;
        private VObjectForm vObjectForm1;
        private System.Windows.Forms.Button buttonDelete;


    }
}
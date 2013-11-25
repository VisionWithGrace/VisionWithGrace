namespace VisionWithGrace
{
    partial class SelectedObjectForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SelectedObjectForm));
            this.buttonDiscard = new System.Windows.Forms.Button();
            this.buttonSave = new System.Windows.Forms.Button();
            this.vObjectForm1 = new VisionWithGrace.VObjectForm();
            this.SuspendLayout();
            // 
            // buttonDiscard
            // 
            this.buttonDiscard.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonDiscard.Location = new System.Drawing.Point(543, 341);
            this.buttonDiscard.Name = "buttonDiscard";
            this.buttonDiscard.Size = new System.Drawing.Size(120, 73);
            this.buttonDiscard.TabIndex = 3;
            this.buttonDiscard.Text = "Discard";
            this.buttonDiscard.UseVisualStyleBackColor = true;
            this.buttonDiscard.Click += new System.EventHandler(this.buttonDiscard_Click);
            // 
            // buttonSave
            // 
            this.buttonSave.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonSave.Location = new System.Drawing.Point(417, 341);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(120, 73);
            this.buttonSave.TabIndex = 4;
            this.buttonSave.Text = "Save";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // vObjectForm1
            // 
            this.vObjectForm1.Location = new System.Drawing.Point(12, 12);
            this.vObjectForm1.Name = "vObjectForm1";
            this.vObjectForm1.Size = new System.Drawing.Size(650, 401);
            this.vObjectForm1.TabIndex = 5;
            this.vObjectForm1.VObjectName = "";
            this.vObjectForm1.VObjectTags = ((System.Collections.Generic.List<string>)(resources.GetObject("vObjectForm1.VObjectTags")));
            // 
            // SelectedObjectForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(675, 426);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.buttonDiscard);
            this.Controls.Add(this.vObjectForm1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SelectedObjectForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Edit Selected Object";
            this.TopMost = true;
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonDiscard;
        private System.Windows.Forms.Button buttonSave;
        private VObjectForm vObjectForm1;
    }
}
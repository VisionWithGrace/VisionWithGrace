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
            this.radioButtonRecognizedObject = new System.Windows.Forms.RadioButton();
            this.radioButtonDetectedObject = new System.Windows.Forms.RadioButton();
            this.vObjectForm1 = new VisionWithGrace.VObjectForm();
            this.labelTitle = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // buttonDiscard
            // 
            this.buttonDiscard.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonDiscard.Location = new System.Drawing.Point(543, 370);
            this.buttonDiscard.Name = "buttonDiscard";
            this.buttonDiscard.Size = new System.Drawing.Size(120, 73);
            this.buttonDiscard.TabIndex = 0;
            this.buttonDiscard.Text = "Discard";
            this.buttonDiscard.UseVisualStyleBackColor = true;
            this.buttonDiscard.Click += new System.EventHandler(this.buttonDiscard_Click);
            // 
            // buttonSave
            // 
            this.buttonSave.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonSave.Location = new System.Drawing.Point(417, 370);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(120, 73);
            this.buttonSave.TabIndex = 1;
            this.buttonSave.Text = "Save";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // radioButtonRecognizedObject
            // 
            this.radioButtonRecognizedObject.AutoSize = true;
            this.radioButtonRecognizedObject.Location = new System.Drawing.Point(417, 255);
            this.radioButtonRecognizedObject.Name = "radioButtonRecognizedObject";
            this.radioButtonRecognizedObject.Size = new System.Drawing.Size(147, 17);
            this.radioButtonRecognizedObject.TabIndex = 3;
            this.radioButtonRecognizedObject.TabStop = true;
            this.radioButtonRecognizedObject.Text = "Update recognized object";
            this.radioButtonRecognizedObject.UseVisualStyleBackColor = true;
            this.radioButtonRecognizedObject.Visible = false;
            this.radioButtonRecognizedObject.CheckedChanged += new System.EventHandler(this.radioButtonRecognizedObject_CheckedChanged);
            // 
            // radioButtonDetectedObject
            // 
            this.radioButtonDetectedObject.AutoSize = true;
            this.radioButtonDetectedObject.Location = new System.Drawing.Point(417, 278);
            this.radioButtonDetectedObject.Name = "radioButtonDetectedObject";
            this.radioButtonDetectedObject.Size = new System.Drawing.Size(119, 17);
            this.radioButtonDetectedObject.TabIndex = 4;
            this.radioButtonDetectedObject.TabStop = true;
            this.radioButtonDetectedObject.Text = "Save as new object";
            this.radioButtonDetectedObject.UseVisualStyleBackColor = true;
            this.radioButtonDetectedObject.Visible = false;
            this.radioButtonDetectedObject.CheckedChanged += new System.EventHandler(this.radioButtonDetectedObject_CheckedChanged);
            // 
            // vObjectForm1
            // 
            this.vObjectForm1.Location = new System.Drawing.Point(12, 41);
            this.vObjectForm1.Name = "vObjectForm1";
            this.vObjectForm1.Size = new System.Drawing.Size(650, 401);
            this.vObjectForm1.TabIndex = 2;
            this.vObjectForm1.VObjectName = "";
            this.vObjectForm1.VObjectTags = ((System.Collections.Generic.List<string>)(resources.GetObject("vObjectForm1.VObjectTags")));
            // 
            // labelTitle
            // 
            this.labelTitle.AutoSize = true;
            this.labelTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTitle.Location = new System.Drawing.Point(12, 9);
            this.labelTitle.Name = "labelTitle";
            this.labelTitle.Size = new System.Drawing.Size(64, 25);
            this.labelTitle.TabIndex = 5;
            this.labelTitle.Text = "label1";
            // 
            // SelectedObjectForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(675, 454);
            this.Controls.Add(this.labelTitle);
            this.Controls.Add(this.radioButtonDetectedObject);
            this.Controls.Add(this.radioButtonRecognizedObject);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.buttonDiscard);
            this.Controls.Add(this.vObjectForm1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SelectedObjectForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Selected Object";
            this.TopMost = true;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonDiscard;
        private System.Windows.Forms.Button buttonSave;
        private VObjectForm vObjectForm1;
        private System.Windows.Forms.RadioButton radioButtonRecognizedObject;
        private System.Windows.Forms.RadioButton radioButtonDetectedObject;
        private System.Windows.Forms.Label labelTitle;
    }
}
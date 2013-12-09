namespace VisionWithGrace
{
    partial class settingsPanel
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
            this.refreshTrackBar = new System.Windows.Forms.TrackBar();
            this.refreshLabel = new System.Windows.Forms.Label();
            this.scanLabel = new System.Windows.Forms.Label();
            this.scanTrackBar = new System.Windows.Forms.TrackBar();
            this.colorDropDown = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.okButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.label6 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.refreshTrackBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.scanTrackBar)).BeginInit();
            this.SuspendLayout();
            // 
            // refreshTrackBar
            // 
            this.refreshTrackBar.Location = new System.Drawing.Point(123, 28);
            this.refreshTrackBar.Maximum = 10000;
            this.refreshTrackBar.Minimum = 1000;
            this.refreshTrackBar.Name = "refreshTrackBar";
            this.refreshTrackBar.Size = new System.Drawing.Size(339, 45);
            this.refreshTrackBar.TabIndex = 0;
            this.refreshTrackBar.Value = global::VisionWithGrace.Properties.Settings.Default.refreshRate;
            this.refreshTrackBar.Scroll += new System.EventHandler(this.refreshTrackBar_Scroll);
            // 
            // refreshLabel
            // 
            this.refreshLabel.AutoSize = true;
            this.refreshLabel.Location = new System.Drawing.Point(27, 28);
            this.refreshLabel.Name = "refreshLabel";
            this.refreshLabel.Size = new System.Drawing.Size(70, 13);
            this.refreshLabel.TabIndex = 1;
            this.refreshLabel.Text = "Refresh Rate";
            // 
            // scanLabel
            // 
            this.scanLabel.AutoSize = true;
            this.scanLabel.Location = new System.Drawing.Point(27, 99);
            this.scanLabel.Name = "scanLabel";
            this.scanLabel.Size = new System.Drawing.Size(66, 13);
            this.scanLabel.TabIndex = 3;
            this.scanLabel.Text = "Scan Speed";
            // 
            // scanTrackBar
            // 
            this.scanTrackBar.Location = new System.Drawing.Point(123, 99);
            this.scanTrackBar.Maximum = 5000;
            this.scanTrackBar.Minimum = 100;
            this.scanTrackBar.Name = "scanTrackBar";
            this.scanTrackBar.Size = new System.Drawing.Size(339, 45);
            this.scanTrackBar.TabIndex = 2;
            this.scanTrackBar.Value = global::VisionWithGrace.Properties.Settings.Default.scanSpeed;
            this.scanTrackBar.Scroll += new System.EventHandler(this.scanTrackBar_Scroll);
            // 
            // colorDropDown
            // 
            this.colorDropDown.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.colorDropDown.FormattingEnabled = true;
            this.colorDropDown.Items.AddRange(new object[] {
            "Red",
            "Blue",
            "Green",
            "Purple",
            "Pink",
            "White",
            "Black"});
            this.colorDropDown.Location = new System.Drawing.Point(123, 150);
            this.colorDropDown.Name = "colorDropDown";
            this.colorDropDown.Size = new System.Drawing.Size(121, 21);
            this.colorDropDown.TabIndex = 4;
            this.colorDropDown.SelectedIndexChanged += new System.EventHandler(this.colorDropDown_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(27, 153);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Highlight Color";
            // 
            // okButton
            // 
            this.okButton.Location = new System.Drawing.Point(103, 213);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 23);
            this.okButton.TabIndex = 6;
            this.okButton.Text = "Ok";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(304, 213);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 7;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(123, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(27, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Fast";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(123, 83);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(27, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "Fast";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(435, 13);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(30, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "Slow";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(432, 83);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(30, 13);
            this.label5.TabIndex = 11;
            this.label5.Text = "Slow";
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Checked = global::VisionWithGrace.Properties.Settings.Default.helpFlag;
            this.checkBox1.Location = new System.Drawing.Point(126, 190);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(15, 14);
            this.checkBox1.TabIndex = 12;
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(27, 190);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(83, 13);
            this.label6.TabIndex = 13;
            this.label6.Text = "Help On Startup";
            // 
            // settingsPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(474, 262);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.colorDropDown);
            this.Controls.Add(this.scanLabel);
            this.Controls.Add(this.scanTrackBar);
            this.Controls.Add(this.refreshLabel);
            this.Controls.Add(this.refreshTrackBar);
            this.Name = "settingsPanel";
            this.Text = "settingsPanel";
            ((System.ComponentModel.ISupportInitialize)(this.refreshTrackBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.scanTrackBar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TrackBar refreshTrackBar;
        private System.Windows.Forms.Label refreshLabel;
        private System.Windows.Forms.Label scanLabel;
        private System.Windows.Forms.TrackBar scanTrackBar;
        private System.Windows.Forms.ComboBox colorDropDown;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Label label6;
    }
}
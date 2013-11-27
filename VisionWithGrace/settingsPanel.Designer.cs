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
            this.colorDropDown.FormattingEnabled = true;
            this.colorDropDown.Location = new System.Drawing.Point(123, 150);
            this.colorDropDown.Name = "colorDropDown";
            this.colorDropDown.Size = new System.Drawing.Size(121, 21);
            this.colorDropDown.TabIndex = 4;
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
            // settingsPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(474, 262);
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
    }
}
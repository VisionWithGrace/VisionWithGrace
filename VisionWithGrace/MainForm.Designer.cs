namespace VisionWithGrace
{
    partial class MainForm
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
            this.mainDisplay.Image.Dispose();
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
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.objectDetectedLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.labelTimeRemaining = new System.Windows.Forms.ToolStripStatusLabel();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.adminToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.adminPanelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.manualToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.manualScanToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mainDisplay = new System.Windows.Forms.PictureBox();
            this.mainDisplayLayout = new System.Windows.Forms.TableLayoutPanel();
            this.statusStrip1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mainDisplay)).BeginInit();
            this.mainDisplayLayout.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.mainDisplayLayout.SetColumnSpan(this.statusStrip1, 2);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.objectDetectedLabel,
            this.labelTimeRemaining});
            this.statusStrip1.Location = new System.Drawing.Point(0, 534);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.statusStrip1.Size = new System.Drawing.Size(1062, 42);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // objectDetectedLabel
            // 
            this.objectDetectedLabel.Font = new System.Drawing.Font("Segoe UI", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.objectDetectedLabel.Name = "objectDetectedLabel";
            this.objectDetectedLabel.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.objectDetectedLabel.Size = new System.Drawing.Size(260, 37);
            this.objectDetectedLabel.Text = "4 Objects Detected";
            // 
            // labelTimeRemaining
            // 
            this.labelTimeRemaining.Name = "labelTimeRemaining";
            this.labelTimeRemaining.Size = new System.Drawing.Size(0, 37);
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.mainDisplayLayout.SetColumnSpan(this.menuStrip1, 2);
            this.menuStrip1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.adminToolStripMenuItem,
            this.manualToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 377);
            this.menuStrip1.MaximumSize = new System.Drawing.Size(0, 24);
            this.menuStrip1.MinimumSize = new System.Drawing.Size(0, 24);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1062, 24);
            this.menuStrip1.TabIndex = 8;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // adminToolStripMenuItem
            // 
            this.adminToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.adminPanelToolStripMenuItem,
            this.settingsToolStripMenuItem});
            this.adminToolStripMenuItem.Name = "adminToolStripMenuItem";
            this.adminToolStripMenuItem.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.adminToolStripMenuItem.Size = new System.Drawing.Size(108, 20);
            this.adminToolStripMenuItem.Text = "Admininistration";
            // 
            // adminPanelToolStripMenuItem
            // 
            this.adminPanelToolStripMenuItem.Name = "adminPanelToolStripMenuItem";
            this.adminPanelToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.adminPanelToolStripMenuItem.Text = "Edit Objects";
            this.adminPanelToolStripMenuItem.Click += new System.EventHandler(this.adminPanelToolStripMenuItem_Click);
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.settingsToolStripMenuItem.Text = "Settings";
            this.settingsToolStripMenuItem.Click += new System.EventHandler(this.settingsToolStripMenuItem_Click);
            // 
            // manualToolStripMenuItem
            // 
            this.manualToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.manualScanToolStripMenuItem});
            this.manualToolStripMenuItem.Name = "manualToolStripMenuItem";
            this.manualToolStripMenuItem.Size = new System.Drawing.Size(102, 20);
            this.manualToolStripMenuItem.Text = "Scanning Mode";
            // 
            // manualScanToolStripMenuItem
            // 
            this.manualScanToolStripMenuItem.Name = "manualScanToolStripMenuItem";
            this.manualScanToolStripMenuItem.Size = new System.Drawing.Size(127, 22);
            this.manualScanToolStripMenuItem.Text = "Free Form";
            this.manualScanToolStripMenuItem.Click += new System.EventHandler(this.manualScanToolStripMenuItem_Click);
            // 
            // mainDisplay
            // 
            this.mainDisplay.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.mainDisplay.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.mainDisplayLayout.SetColumnSpan(this.mainDisplay, 2);
            this.mainDisplay.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainDisplay.Location = new System.Drawing.Point(10, 10);
            this.mainDisplay.Margin = new System.Windows.Forms.Padding(10);
            this.mainDisplay.Name = "mainDisplay";
            this.mainDisplayLayout.SetRowSpan(this.mainDisplay, 4);
            this.mainDisplay.Size = new System.Drawing.Size(1042, 357);
            this.mainDisplay.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.mainDisplay.TabIndex = 0;
            this.mainDisplay.TabStop = false;
            // 
            // mainDisplayLayout
            // 
            this.mainDisplayLayout.AutoSize = true;
            this.mainDisplayLayout.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.mainDisplayLayout.ColumnCount = 2;
            this.mainDisplayLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.47834F));
            this.mainDisplayLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 83.52166F));
            this.mainDisplayLayout.Controls.Add(this.mainDisplay, 0, 0);
            this.mainDisplayLayout.Controls.Add(this.menuStrip1, 0, 0);
            this.mainDisplayLayout.Controls.Add(this.statusStrip1, 0, 5);
            this.mainDisplayLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainDisplayLayout.Location = new System.Drawing.Point(0, 0);
            this.mainDisplayLayout.Name = "mainDisplayLayout";
            this.mainDisplayLayout.RowCount = 6;
            this.mainDisplayLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.mainDisplayLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.mainDisplayLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 51.45631F));
            this.mainDisplayLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 48.54369F));
            this.mainDisplayLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 156F));
            this.mainDisplayLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 42F));
            this.mainDisplayLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.mainDisplayLayout.Size = new System.Drawing.Size(1062, 576);
            this.mainDisplayLayout.TabIndex = 0;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1062, 576);
            this.Controls.Add(this.mainDisplayLayout);
            this.KeyPreview = true;
            this.MainMenuStrip = this.menuStrip1;
            this.MinimumSize = new System.Drawing.Size(500, 300);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Vision With Grace";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Shown += new System.EventHandler(this.Form1_Shown);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.startScanning);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.stopScanning);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mainDisplay)).EndInit();
            this.mainDisplayLayout.ResumeLayout(false);
            this.mainDisplayLayout.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.TableLayoutPanel mainDisplayLayout;
        private System.Windows.Forms.PictureBox mainDisplay;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem adminToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem adminPanelToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripStatusLabel objectDetectedLabel;
        private System.Windows.Forms.ToolStripStatusLabel labelTimeRemaining;
        private System.Windows.Forms.ToolStripMenuItem manualToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem manualScanToolStripMenuItem;

    }
}


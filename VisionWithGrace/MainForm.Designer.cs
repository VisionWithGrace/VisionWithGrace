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
            this.computerVisionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.manualToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.manualScanToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.objectDetectionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mainDisplayLayout = new System.Windows.Forms.TableLayoutPanel();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.buttonPanel = new System.Windows.Forms.TableLayoutPanel();
            this.button3 = new System.Windows.Forms.Button();
            this.mainDisplay = new System.Windows.Forms.PictureBox();
            this.statusStrip1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.mainDisplayLayout.SuspendLayout();
            this.buttonPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mainDisplay)).BeginInit();
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
            this.statusStrip1.Size = new System.Drawing.Size(1163, 42);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // objectDetectedLabel
            // 
            this.objectDetectedLabel.Font = new System.Drawing.Font("Segoe UI", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.objectDetectedLabel.Name = "objectDetectedLabel";
            this.objectDetectedLabel.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.objectDetectedLabel.Size = new System.Drawing.Size(262, 37);
            this.objectDetectedLabel.Text = "Waiting for picture";
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
            this.manualToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.MaximumSize = new System.Drawing.Size(0, 24);
            this.menuStrip1.MinimumSize = new System.Drawing.Size(0, 24);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1163, 24);
            this.menuStrip1.TabIndex = 8;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // adminToolStripMenuItem
            // 
            this.adminToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.adminPanelToolStripMenuItem,
            this.settingsToolStripMenuItem,
            this.computerVisionToolStripMenuItem});
            this.adminToolStripMenuItem.Name = "adminToolStripMenuItem";
            this.adminToolStripMenuItem.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.adminToolStripMenuItem.Size = new System.Drawing.Size(108, 20);
            this.adminToolStripMenuItem.Text = "Admininistration";
            // 
            // adminPanelToolStripMenuItem
            // 
            this.adminPanelToolStripMenuItem.Name = "adminPanelToolStripMenuItem";
            this.adminPanelToolStripMenuItem.Size = new System.Drawing.Size(163, 22);
            this.adminPanelToolStripMenuItem.Text = "Edit Objects";
            this.adminPanelToolStripMenuItem.Click += new System.EventHandler(this.adminPanelToolStripMenuItem_Click);
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(163, 22);
            this.settingsToolStripMenuItem.Text = "Settings";
            this.settingsToolStripMenuItem.Click += new System.EventHandler(this.settingsToolStripMenuItem_Click);
            // 
            // computerVisionToolStripMenuItem
            // 
            this.computerVisionToolStripMenuItem.Name = "computerVisionToolStripMenuItem";
            this.computerVisionToolStripMenuItem.Size = new System.Drawing.Size(163, 22);
            this.computerVisionToolStripMenuItem.Text = "Computer Vision";
            this.computerVisionToolStripMenuItem.Click += new System.EventHandler(this.computerVisionToolStripMenuItem_Click);
            // 
            // manualToolStripMenuItem
            // 
            this.manualToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.manualScanToolStripMenuItem,
            this.objectDetectionToolStripMenuItem});
            this.manualToolStripMenuItem.Name = "manualToolStripMenuItem";
            this.manualToolStripMenuItem.Size = new System.Drawing.Size(102, 20);
            this.manualToolStripMenuItem.Text = "Scanning Mode";
            // 
            // manualScanToolStripMenuItem
            // 
            this.manualScanToolStripMenuItem.Name = "manualScanToolStripMenuItem";
            this.manualScanToolStripMenuItem.Size = new System.Drawing.Size(163, 22);
            this.manualScanToolStripMenuItem.Text = "Free Form";
            this.manualScanToolStripMenuItem.Click += new System.EventHandler(this.manualScanToolStripMenuItem_Click);
            // 
            // objectDetectionToolStripMenuItem
            // 
            this.objectDetectionToolStripMenuItem.Name = "objectDetectionToolStripMenuItem";
            this.objectDetectionToolStripMenuItem.Size = new System.Drawing.Size(163, 22);
            this.objectDetectionToolStripMenuItem.Text = "Object Detection";
            this.objectDetectionToolStripMenuItem.Click += new System.EventHandler(this.objectDetectionToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "Help";
            this.helpToolStripMenuItem.Click += new System.EventHandler(this.helpToolStripMenuItem_Click);
            // 
            // mainDisplayLayout
            // 
            this.mainDisplayLayout.AutoSize = true;
            this.mainDisplayLayout.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.mainDisplayLayout.BackColor = System.Drawing.SystemColors.Control;
            this.mainDisplayLayout.ColumnCount = 2;
            this.mainDisplayLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 143F));
            this.mainDisplayLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.mainDisplayLayout.Controls.Add(this.mainDisplay, 1, 1);
            this.mainDisplayLayout.Controls.Add(this.menuStrip1, 0, 0);
            this.mainDisplayLayout.Controls.Add(this.statusStrip1, 0, 4);
            this.mainDisplayLayout.Controls.Add(this.buttonPanel, 0, 1);
            this.mainDisplayLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainDisplayLayout.Location = new System.Drawing.Point(0, 0);
            this.mainDisplayLayout.Name = "mainDisplayLayout";
            this.mainDisplayLayout.RowCount = 5;
            this.mainDisplayLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.mainDisplayLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.mainDisplayLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.mainDisplayLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.mainDisplayLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 0F));
            this.mainDisplayLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 42F));
            this.mainDisplayLayout.Size = new System.Drawing.Size(1163, 576);
            this.mainDisplayLayout.TabIndex = 0;
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.button1.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button1.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(10, 10);
            this.button1.Margin = new System.Windows.Forms.Padding(10);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(113, 142);
            this.button1.TabIndex = 9;
            this.button1.Text = "Auto Detect";
            this.button1.UseVisualStyleBackColor = false;
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.button2.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.button2.FlatAppearance.BorderSize = 0;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button2.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.Location = new System.Drawing.Point(10, 172);
            this.button2.Margin = new System.Windows.Forms.Padding(10);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(113, 142);
            this.button2.TabIndex = 10;
            this.button2.Text = "Free Form Crop";
            this.button2.UseVisualStyleBackColor = false;
            // 
            // buttonPanel
            // 
            this.buttonPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonPanel.BackColor = System.Drawing.SystemColors.Control;
            this.buttonPanel.ColumnCount = 1;
            this.buttonPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.buttonPanel.Controls.Add(this.button3, 0, 2);
            this.buttonPanel.Controls.Add(this.button1, 0, 0);
            this.buttonPanel.Controls.Add(this.button2, 0, 1);
            this.buttonPanel.Location = new System.Drawing.Point(10, 34);
            this.buttonPanel.Margin = new System.Windows.Forms.Padding(10, 10, 0, 10);
            this.buttonPanel.Name = "buttonPanel";
            this.buttonPanel.RowCount = 3;
            this.mainDisplayLayout.SetRowSpan(this.buttonPanel, 3);
            this.buttonPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.buttonPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.buttonPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.buttonPanel.Size = new System.Drawing.Size(133, 487);
            this.buttonPanel.TabIndex = 11;
            // 
            // button3
            // 
            this.button3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.button3.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.button3.FlatAppearance.BorderSize = 0;
            this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button3.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button3.Location = new System.Drawing.Point(10, 334);
            this.button3.Margin = new System.Windows.Forms.Padding(10);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(113, 143);
            this.button3.TabIndex = 11;
            this.button3.Text = "Stored Objects";
            this.button3.UseVisualStyleBackColor = false;
            // 
            // mainDisplay
            // 
            this.mainDisplay.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.mainDisplay.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.mainDisplay.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainDisplay.Location = new System.Drawing.Point(153, 34);
            this.mainDisplay.Margin = new System.Windows.Forms.Padding(10);
            this.mainDisplay.Name = "mainDisplay";
            this.mainDisplayLayout.SetRowSpan(this.mainDisplay, 4);
            this.mainDisplay.Size = new System.Drawing.Size(1000, 487);
            this.mainDisplay.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.mainDisplay.TabIndex = 0;
            this.mainDisplay.TabStop = false;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1163, 576);
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
            this.mainDisplayLayout.ResumeLayout(false);
            this.mainDisplayLayout.PerformLayout();
            this.buttonPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.mainDisplay)).EndInit();
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
        private System.Windows.Forms.ToolStripMenuItem objectDetectionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem computerVisionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TableLayoutPanel buttonPanel;
        private System.Windows.Forms.Button button3;

    }
}


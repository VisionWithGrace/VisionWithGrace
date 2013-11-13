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
            this.mainDisplayLayout = new System.Windows.Forms.TableLayoutPanel();
            this.objectNameText = new System.Windows.Forms.TextBox();
            this.mainDisplay = new System.Windows.Forms.PictureBox();
            this.closeUpDisplay = new System.Windows.Forms.PictureBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.adminToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.adminPanelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.objectDetectedLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.labelTimeRemaining = new System.Windows.Forms.ToolStripStatusLabel();
            this.leftButtonMenuLayout = new System.Windows.Forms.TableLayoutPanel();
            this.refreshButton = new System.Windows.Forms.Button();
            this.undoButton = new System.Windows.Forms.Button();
            this.scanButton = new System.Windows.Forms.Button();
            this.selectButton = new System.Windows.Forms.Button();
            this.mainDisplayLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mainDisplay)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.closeUpDisplay)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.leftButtonMenuLayout.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainDisplayLayout
            // 
            this.mainDisplayLayout.AutoSize = true;
            this.mainDisplayLayout.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.mainDisplayLayout.ColumnCount = 2;
            this.mainDisplayLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.47834F));
            this.mainDisplayLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 83.52166F));
            this.mainDisplayLayout.Controls.Add(this.objectNameText, 0, 1);
            this.mainDisplayLayout.Controls.Add(this.mainDisplay, 2, 1);
            this.mainDisplayLayout.Controls.Add(this.closeUpDisplay, 0, 2);
            this.mainDisplayLayout.Controls.Add(this.menuStrip1, 0, 0);
            this.mainDisplayLayout.Controls.Add(this.statusStrip1, 0, 5);
            this.mainDisplayLayout.Controls.Add(this.leftButtonMenuLayout, 0, 3);
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
            this.mainDisplayLayout.Size = new System.Drawing.Size(1062, 576);
            this.mainDisplayLayout.TabIndex = 0;
            // 
            // objectNameText
            // 
            this.objectNameText.Dock = System.Windows.Forms.DockStyle.Top;
            this.objectNameText.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.objectNameText.Location = new System.Drawing.Point(3, 27);
            this.objectNameText.Name = "objectNameText";
            this.objectNameText.Size = new System.Drawing.Size(168, 26);
            this.objectNameText.TabIndex = 6;
            this.objectNameText.Text = "Enter object name...";
            this.objectNameText.Enter += new System.EventHandler(this.objectNameText_Enter);
            this.objectNameText.KeyDown += new System.Windows.Forms.KeyEventHandler(this.objectNameText_KeyDown);
            this.objectNameText.Leave += new System.EventHandler(this.objectNameText_Leave);
            // 
            // mainDisplay
            // 
            this.mainDisplay.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.mainDisplay.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.mainDisplay.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainDisplay.Location = new System.Drawing.Point(184, 34);
            this.mainDisplay.Margin = new System.Windows.Forms.Padding(10);
            this.mainDisplay.Name = "mainDisplay";
            this.mainDisplayLayout.SetRowSpan(this.mainDisplay, 4);
            this.mainDisplay.Size = new System.Drawing.Size(868, 489);
            this.mainDisplay.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.mainDisplay.TabIndex = 0;
            this.mainDisplay.TabStop = false;
            // 
            // closeUpDisplay
            // 
            this.closeUpDisplay.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.closeUpDisplay.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.closeUpDisplay.Dock = System.Windows.Forms.DockStyle.Fill;
            this.closeUpDisplay.InitialImage = null;
            this.closeUpDisplay.Location = new System.Drawing.Point(10, 66);
            this.closeUpDisplay.Margin = new System.Windows.Forms.Padding(10);
            this.closeUpDisplay.Name = "closeUpDisplay";
            this.closeUpDisplay.Size = new System.Drawing.Size(154, 145);
            this.closeUpDisplay.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.closeUpDisplay.TabIndex = 1;
            this.closeUpDisplay.TabStop = false;
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.mainDisplayLayout.SetColumnSpan(this.menuStrip1, 2);
            this.menuStrip1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.editToolStripMenuItem,
            this.adminToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.MaximumSize = new System.Drawing.Size(0, 24);
            this.menuStrip1.MinimumSize = new System.Drawing.Size(0, 24);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1062, 24);
            this.menuStrip1.TabIndex = 8;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
            this.editToolStripMenuItem.Text = "Edit";
            // 
            // adminToolStripMenuItem
            // 
            this.adminToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.adminPanelToolStripMenuItem});
            this.adminToolStripMenuItem.Name = "adminToolStripMenuItem";
            this.adminToolStripMenuItem.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.adminToolStripMenuItem.Size = new System.Drawing.Size(55, 20);
            this.adminToolStripMenuItem.Text = "Admin";
            // 
            // adminPanelToolStripMenuItem
            // 
            this.adminPanelToolStripMenuItem.Name = "adminPanelToolStripMenuItem";
            this.adminPanelToolStripMenuItem.Size = new System.Drawing.Size(142, 22);
            this.adminPanelToolStripMenuItem.Text = "Admin Panel";
            this.adminPanelToolStripMenuItem.Click += new System.EventHandler(this.adminPanelToolStripMenuItem_Click);
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
            // leftButtonMenuLayout
            // 
            this.leftButtonMenuLayout.ColumnCount = 1;
            this.leftButtonMenuLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.leftButtonMenuLayout.Controls.Add(this.refreshButton, 0, 3);
            this.leftButtonMenuLayout.Controls.Add(this.undoButton, 0, 2);
            this.leftButtonMenuLayout.Controls.Add(this.scanButton, 0, 1);
            this.leftButtonMenuLayout.Controls.Add(this.selectButton, 0, 0);
            this.leftButtonMenuLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.leftButtonMenuLayout.Location = new System.Drawing.Point(3, 224);
            this.leftButtonMenuLayout.Name = "leftButtonMenuLayout";
            this.leftButtonMenuLayout.Padding = new System.Windows.Forms.Padding(10);
            this.leftButtonMenuLayout.RowCount = 4;
            this.mainDisplayLayout.SetRowSpan(this.leftButtonMenuLayout, 2);
            this.leftButtonMenuLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.leftButtonMenuLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.leftButtonMenuLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.leftButtonMenuLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.leftButtonMenuLayout.Size = new System.Drawing.Size(168, 306);
            this.leftButtonMenuLayout.TabIndex = 7;
            // 
            // refreshButton
            // 
            this.refreshButton.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.refreshButton.BackColor = System.Drawing.SystemColors.Control;
            this.refreshButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.refreshButton.Location = new System.Drawing.Point(13, 226);
            this.refreshButton.Name = "refreshButton";
            this.refreshButton.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.refreshButton.Size = new System.Drawing.Size(142, 67);
            this.refreshButton.TabIndex = 3;
            this.refreshButton.Text = "Refresh";
            this.refreshButton.UseVisualStyleBackColor = false;
            this.refreshButton.Click += new System.EventHandler(this.refreshButton_Click);
            // 
            // undoButton
            // 
            this.undoButton.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.undoButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.undoButton.Enabled = false;
            this.undoButton.Location = new System.Drawing.Point(13, 155);
            this.undoButton.Name = "undoButton";
            this.undoButton.Size = new System.Drawing.Size(142, 65);
            this.undoButton.TabIndex = 4;
            this.undoButton.Text = "Undo";
            this.undoButton.UseVisualStyleBackColor = false;
            // 
            // scanButton
            // 
            this.scanButton.BackColor = System.Drawing.SystemColors.Control;
            this.scanButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scanButton.Enabled = false;
            this.scanButton.Location = new System.Drawing.Point(13, 84);
            this.scanButton.Name = "scanButton";
            this.scanButton.Size = new System.Drawing.Size(142, 65);
            this.scanButton.TabIndex = 5;
            this.scanButton.Text = "Scan";
            this.scanButton.UseVisualStyleBackColor = false;
            // 
            // selectButton
            // 
            this.selectButton.BackColor = System.Drawing.SystemColors.Control;
            this.selectButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.selectButton.Enabled = false;
            this.selectButton.Location = new System.Drawing.Point(13, 13);
            this.selectButton.Name = "selectButton";
            this.selectButton.Size = new System.Drawing.Size(142, 65);
            this.selectButton.TabIndex = 6;
            this.selectButton.Text = "Select";
            this.selectButton.UseVisualStyleBackColor = false;
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
            this.Shown += new System.EventHandler(this.Form1_Shown);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.startScanning);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.stopScanning);
            this.mainDisplayLayout.ResumeLayout(false);
            this.mainDisplayLayout.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mainDisplay)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.closeUpDisplay)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.leftButtonMenuLayout.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel mainDisplayLayout;
        private System.Windows.Forms.PictureBox mainDisplay;
        private System.Windows.Forms.PictureBox closeUpDisplay;
        private System.Windows.Forms.StatusStrip statusStrip1;

        private System.Windows.Forms.ToolStripStatusLabel objectDetectedLabel;
        private System.Windows.Forms.Button refreshButton;
        private System.Windows.Forms.TextBox objectNameText;
        private System.Windows.Forms.TableLayoutPanel leftButtonMenuLayout;
        private System.Windows.Forms.Button undoButton;
        private System.Windows.Forms.Button scanButton;
        private System.Windows.Forms.Button selectButton;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem adminToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem adminPanelToolStripMenuItem;
        private System.Windows.Forms.ToolStripStatusLabel labelTimeRemaining;
    }
}


namespace VisionWithGrace
{
    partial class cvDebug
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
            this.components = new System.ComponentModel.Container();
            this.emguColorImageBox = new Emgu.CV.UI.ImageBox();
            this.emguDepthImageBox = new Emgu.CV.UI.ImageBox();
            this.emguColorProcessedImageBox = new Emgu.CV.UI.ImageBox();
            this.emguDepthProcessedImageBox = new Emgu.CV.UI.ImageBox();
            this.recognitionOutput = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.emguColorImageBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emguDepthImageBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emguColorProcessedImageBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emguDepthProcessedImageBox)).BeginInit();
            this.SuspendLayout();
            // 
            // emguColorImageBox
            // 
            this.emguColorImageBox.Location = new System.Drawing.Point(9, 5);
            this.emguColorImageBox.Name = "emguColorImageBox";
            this.emguColorImageBox.Size = new System.Drawing.Size(480, 340);
            this.emguColorImageBox.TabIndex = 2;
            this.emguColorImageBox.TabStop = false;
            // 
            // emguDepthImageBox
            // 
            this.emguDepthImageBox.Location = new System.Drawing.Point(495, 5);
            this.emguDepthImageBox.Name = "emguDepthImageBox";
            this.emguDepthImageBox.Size = new System.Drawing.Size(480, 340);
            this.emguDepthImageBox.TabIndex = 3;
            this.emguDepthImageBox.TabStop = false;
            // 
            // emguColorProcessedImageBox
            // 
            this.emguColorProcessedImageBox.Location = new System.Drawing.Point(9, 351);
            this.emguColorProcessedImageBox.Name = "emguColorProcessedImageBox";
            this.emguColorProcessedImageBox.Size = new System.Drawing.Size(480, 340);
            this.emguColorProcessedImageBox.TabIndex = 4;
            this.emguColorProcessedImageBox.TabStop = false;
            // 
            // emguDepthProcessedImageBox
            // 
            this.emguDepthProcessedImageBox.Location = new System.Drawing.Point(495, 351);
            this.emguDepthProcessedImageBox.Name = "emguDepthProcessedImageBox";
            this.emguDepthProcessedImageBox.Size = new System.Drawing.Size(480, 340);
            this.emguDepthProcessedImageBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.emguDepthProcessedImageBox.TabIndex = 5;
            this.emguDepthProcessedImageBox.TabStop = false;
            // 
            // recognitionOutput
            // 
            this.recognitionOutput.Location = new System.Drawing.Point(9, 703);
            this.recognitionOutput.Multiline = true;
            this.recognitionOutput.Name = "recognitionOutput";
            this.recognitionOutput.ReadOnly = true;
            this.recognitionOutput.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.recognitionOutput.Size = new System.Drawing.Size(656, 36);
            this.recognitionOutput.TabIndex = 6;
            // 
            // cvDebug
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 741);
            this.Controls.Add(this.recognitionOutput);
            this.Controls.Add(this.emguDepthProcessedImageBox);
            this.Controls.Add(this.emguColorProcessedImageBox);
            this.Controls.Add(this.emguDepthImageBox);
            this.Controls.Add(this.emguColorImageBox);
            this.Name = "cvDebug";
            this.Text = "CV Debug";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.cvDebug_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.emguColorImageBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emguDepthImageBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emguColorProcessedImageBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emguDepthProcessedImageBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public Emgu.CV.UI.ImageBox emguColorImageBox;
        public Emgu.CV.UI.ImageBox emguDepthImageBox;
        public Emgu.CV.UI.ImageBox emguColorProcessedImageBox;
        public Emgu.CV.UI.ImageBox emguDepthProcessedImageBox;
        public System.Windows.Forms.TextBox recognitionOutput;
    }
}
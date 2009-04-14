namespace ZSS.Forms
{
    partial class AutoScreenshots
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
            this.cbScreenshotTypes = new System.Windows.Forms.ComboBox();
            this.nudDelay = new System.Windows.Forms.NumericUpDown();
            this.ssBar = new System.Windows.Forms.StatusStrip();
            this.tspbBar = new System.Windows.Forms.ToolStripProgressBar();
            this.btnExecute = new System.Windows.Forms.Button();
            this.cbWaitUploads = new System.Windows.Forms.CheckBox();
            this.lblScreenshotType = new System.Windows.Forms.Label();
            this.lblDelay = new System.Windows.Forms.Label();
            this.lblSeconds = new System.Windows.Forms.Label();
            this.tsslStatus = new System.Windows.Forms.ToolStripStatusLabel();
            ((System.ComponentModel.ISupportInitialize)(this.nudDelay)).BeginInit();
            this.ssBar.SuspendLayout();
            this.SuspendLayout();
            // 
            // cbScreenshotTypes
            // 
            this.cbScreenshotTypes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbScreenshotTypes.FormattingEnabled = true;
            this.cbScreenshotTypes.Items.AddRange(new object[] {
            "Entire Screen",
            "Active Window",
            "Last Crop Shot"});
            this.cbScreenshotTypes.Location = new System.Drawing.Point(104, 12);
            this.cbScreenshotTypes.Name = "cbScreenshotTypes";
            this.cbScreenshotTypes.Size = new System.Drawing.Size(104, 21);
            this.cbScreenshotTypes.TabIndex = 0;
            // 
            // nudDelay
            // 
            this.nudDelay.DecimalPlaces = 1;
            this.nudDelay.Location = new System.Drawing.Point(56, 40);
            this.nudDelay.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.nudDelay.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudDelay.Name = "nudDelay";
            this.nudDelay.Size = new System.Drawing.Size(96, 20);
            this.nudDelay.TabIndex = 1;
            this.nudDelay.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // ssBar
            // 
            this.ssBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tspbBar,
            this.tsslStatus});
            this.ssBar.Location = new System.Drawing.Point(0, 131);
            this.ssBar.Name = "ssBar";
            this.ssBar.Size = new System.Drawing.Size(233, 22);
            this.ssBar.TabIndex = 2;
            this.ssBar.Text = "statusStrip1";
            // 
            // tspbBar
            // 
            this.tspbBar.Name = "tspbBar";
            this.tspbBar.Size = new System.Drawing.Size(100, 16);
            // 
            // btnExecute
            // 
            this.btnExecute.Location = new System.Drawing.Point(16, 96);
            this.btnExecute.Name = "btnExecute";
            this.btnExecute.Size = new System.Drawing.Size(200, 24);
            this.btnExecute.TabIndex = 3;
            this.btnExecute.Text = "Start";
            this.btnExecute.UseVisualStyleBackColor = true;
            this.btnExecute.Click += new System.EventHandler(this.btnExecute_Click);
            // 
            // cbWaitUploads
            // 
            this.cbWaitUploads.AutoSize = true;
            this.cbWaitUploads.Location = new System.Drawing.Point(16, 72);
            this.cbWaitUploads.Name = "cbWaitUploads";
            this.cbWaitUploads.Size = new System.Drawing.Size(156, 17);
            this.cbWaitUploads.TabIndex = 4;
            this.cbWaitUploads.Text = "Wait until uploads complete";
            this.cbWaitUploads.UseVisualStyleBackColor = true;
            // 
            // lblScreenshotType
            // 
            this.lblScreenshotType.AutoSize = true;
            this.lblScreenshotType.Location = new System.Drawing.Point(16, 16);
            this.lblScreenshotType.Name = "lblScreenshotType";
            this.lblScreenshotType.Size = new System.Drawing.Size(87, 13);
            this.lblScreenshotType.TabIndex = 5;
            this.lblScreenshotType.Text = "Screenshot type:";
            // 
            // lblDelay
            // 
            this.lblDelay.AutoSize = true;
            this.lblDelay.Location = new System.Drawing.Point(16, 48);
            this.lblDelay.Name = "lblDelay";
            this.lblDelay.Size = new System.Drawing.Size(37, 13);
            this.lblDelay.TabIndex = 6;
            this.lblDelay.Text = "Delay:";
            // 
            // lblSeconds
            // 
            this.lblSeconds.AutoSize = true;
            this.lblSeconds.Location = new System.Drawing.Point(160, 48);
            this.lblSeconds.Name = "lblSeconds";
            this.lblSeconds.Size = new System.Drawing.Size(47, 13);
            this.lblSeconds.TabIndex = 7;
            this.lblSeconds.Text = "seconds";
            // 
            // tsslStatus
            // 
            this.tsslStatus.Name = "tsslStatus";
            this.tsslStatus.Size = new System.Drawing.Size(0, 17);
            // 
            // AutoScreenshots
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(233, 153);
            this.Controls.Add(this.lblSeconds);
            this.Controls.Add(this.lblDelay);
            this.Controls.Add(this.lblScreenshotType);
            this.Controls.Add(this.cbWaitUploads);
            this.Controls.Add(this.btnExecute);
            this.Controls.Add(this.ssBar);
            this.Controls.Add(this.nudDelay);
            this.Controls.Add(this.cbScreenshotTypes);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "AutoScreenshots";
            this.Text = "AutoScreenshots";
            ((System.ComponentModel.ISupportInitialize)(this.nudDelay)).EndInit();
            this.ssBar.ResumeLayout(false);
            this.ssBar.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cbScreenshotTypes;
        private System.Windows.Forms.NumericUpDown nudDelay;
        private System.Windows.Forms.StatusStrip ssBar;
        private System.Windows.Forms.ToolStripProgressBar tspbBar;
        private System.Windows.Forms.Button btnExecute;
        private System.Windows.Forms.CheckBox cbWaitUploads;
        private System.Windows.Forms.Label lblScreenshotType;
        private System.Windows.Forms.Label lblDelay;
        private System.Windows.Forms.Label lblSeconds;
        private System.Windows.Forms.ToolStripStatusLabel tsslStatus;
    }
}
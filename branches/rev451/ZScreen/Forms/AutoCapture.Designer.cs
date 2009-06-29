namespace ZSS.Forms
{
    partial class AutoCapture
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
            this.ssBar = new System.Windows.Forms.StatusStrip();
            this.tspbBar = new System.Windows.Forms.ToolStripProgressBar();
            this.tsslStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.btnExecute = new System.Windows.Forms.Button();
            this.cbWaitUploads = new System.Windows.Forms.CheckBox();
            this.lblScreenshotType = new System.Windows.Forms.Label();
            this.cbAutoMinimize = new System.Windows.Forms.CheckBox();
            this.nudDelay = new ZSS.NumericUpDownTimer();
            this.ssBar.SuspendLayout();
            this.SuspendLayout();
            // 
            // cbScreenshotTypes
            // 
            this.cbScreenshotTypes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbScreenshotTypes.FormattingEnabled = true;
            this.cbScreenshotTypes.Location = new System.Drawing.Point(104, 10);
            this.cbScreenshotTypes.Name = "cbScreenshotTypes";
            this.cbScreenshotTypes.Size = new System.Drawing.Size(104, 21);
            this.cbScreenshotTypes.TabIndex = 0;
            this.cbScreenshotTypes.SelectedIndexChanged += new System.EventHandler(this.cbScreenshotTypes_SelectedIndexChanged);
            // 
            // ssBar
            // 
            this.ssBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tspbBar,
            this.tsslStatus});
            this.ssBar.Location = new System.Drawing.Point(0, 126);
            this.ssBar.Name = "ssBar";
            this.ssBar.Size = new System.Drawing.Size(319, 22);
            this.ssBar.SizingGrip = false;
            this.ssBar.TabIndex = 2;
            this.ssBar.Text = "statusStrip1";
            // 
            // tspbBar
            // 
            this.tspbBar.Name = "tspbBar";
            this.tspbBar.Size = new System.Drawing.Size(125, 16);
            // 
            // tsslStatus
            // 
            this.tsslStatus.Name = "tsslStatus";
            this.tsslStatus.Size = new System.Drawing.Size(0, 17);
            // 
            // btnExecute
            // 
            this.btnExecute.Location = new System.Drawing.Point(192, 72);
            this.btnExecute.Name = "btnExecute";
            this.btnExecute.Size = new System.Drawing.Size(112, 40);
            this.btnExecute.TabIndex = 3;
            this.btnExecute.Text = "Start";
            this.btnExecute.UseVisualStyleBackColor = true;
            this.btnExecute.Click += new System.EventHandler(this.btnExecute_Click);
            // 
            // cbWaitUploads
            // 
            this.cbWaitUploads.AutoSize = true;
            this.cbWaitUploads.Location = new System.Drawing.Point(16, 96);
            this.cbWaitUploads.Name = "cbWaitUploads";
            this.cbWaitUploads.Size = new System.Drawing.Size(156, 17);
            this.cbWaitUploads.TabIndex = 4;
            this.cbWaitUploads.Text = "Wait until uploads complete";
            this.cbWaitUploads.UseVisualStyleBackColor = true;
            this.cbWaitUploads.CheckedChanged += new System.EventHandler(this.cbWaitUploads_CheckedChanged);
            // 
            // lblScreenshotType
            // 
            this.lblScreenshotType.AutoSize = true;
            this.lblScreenshotType.Location = new System.Drawing.Point(16, 14);
            this.lblScreenshotType.Name = "lblScreenshotType";
            this.lblScreenshotType.Size = new System.Drawing.Size(87, 13);
            this.lblScreenshotType.TabIndex = 5;
            this.lblScreenshotType.Text = "Screenshot type:";
            // 
            // cbAutoMinimize
            // 
            this.cbAutoMinimize.AutoSize = true;
            this.cbAutoMinimize.Location = new System.Drawing.Point(16, 72);
            this.cbAutoMinimize.Name = "cbAutoMinimize";
            this.cbAutoMinimize.Size = new System.Drawing.Size(148, 17);
            this.cbAutoMinimize.TabIndex = 8;
            this.cbAutoMinimize.Text = "Auto minimize this window";
            this.cbAutoMinimize.UseVisualStyleBackColor = true;
            this.cbAutoMinimize.CheckedChanged += new System.EventHandler(this.cbAutoMinimize_CheckedChanged);
            // 
            // nudDelay
            // 
            this.nudDelay.Location = new System.Drawing.Point(13, 38);
            this.nudDelay.Name = "nudDelay";
            this.nudDelay.RealValue = ((long)(0));
            this.nudDelay.Size = new System.Drawing.Size(297, 27);
            this.nudDelay.TabIndex = 9;
            this.nudDelay.Time = ZSS.Times.Milliseconds;
            this.nudDelay.Value = ((long)(0));
            this.nudDelay.ValueChanged += new System.EventHandler(this.nudDelay_ValueChanged);
            this.nudDelay.SelectedIndexChanged += new System.EventHandler(this.nudDelay_SelectedIndexChanged);
            // 
            // AutoCapture
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(319, 148);
            this.Controls.Add(this.nudDelay);
            this.Controls.Add(this.cbAutoMinimize);
            this.Controls.Add(this.lblScreenshotType);
            this.Controls.Add(this.cbWaitUploads);
            this.Controls.Add(this.btnExecute);
            this.Controls.Add(this.ssBar);
            this.Controls.Add(this.cbScreenshotTypes);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "AutoCapture";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Auto Capture";
            this.ssBar.ResumeLayout(false);
            this.ssBar.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cbScreenshotTypes;
        private System.Windows.Forms.StatusStrip ssBar;
        private System.Windows.Forms.ToolStripProgressBar tspbBar;
        private System.Windows.Forms.Button btnExecute;
        private System.Windows.Forms.CheckBox cbWaitUploads;
        private System.Windows.Forms.Label lblScreenshotType;
        private System.Windows.Forms.ToolStripStatusLabel tsslStatus;
        private System.Windows.Forms.CheckBox cbAutoMinimize;
        private NumericUpDownTimer nudDelay;
    }
}
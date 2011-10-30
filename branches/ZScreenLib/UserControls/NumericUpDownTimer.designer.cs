namespace ZScreenGUI
{
    partial class NumericUpDownTimer
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

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.nudDelay = new System.Windows.Forms.NumericUpDown();
            this.cbDelay = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.nudDelay)).BeginInit();
            this.SuspendLayout();
            // 
            // nudDelay
            // 
            this.nudDelay.Location = new System.Drawing.Point(2, 3);
            this.nudDelay.Maximum = new decimal(new int[] {
            1000000000,
            0,
            0,
            0});
            this.nudDelay.Name = "nudDelay";
            this.nudDelay.Size = new System.Drawing.Size(68, 20);
            this.nudDelay.TabIndex = 1;
            this.nudDelay.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.nudDelay.ValueChanged += new System.EventHandler(this.nudDelay_ValueChanged);
            // 
            // cbDelay
            // 
            this.cbDelay.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.cbDelay.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbDelay.FormattingEnabled = true;
            this.cbDelay.ItemHeight = 13;
            this.cbDelay.Location = new System.Drawing.Point(74, 3);
            this.cbDelay.Name = "cbDelay";
            this.cbDelay.Size = new System.Drawing.Size(159, 21);
            this.cbDelay.TabIndex = 2;
            this.cbDelay.SelectedIndexChanged += new System.EventHandler(this.cbDelay_SelectedIndexChanged);
            // 
            // NumericUpDownTimer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.cbDelay);
            this.Controls.Add(this.nudDelay);
            this.Name = "NumericUpDownTimer";
            this.Size = new System.Drawing.Size(233, 27);
            ((System.ComponentModel.ISupportInitialize)(this.nudDelay)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion Component Designer generated code

        private System.Windows.Forms.NumericUpDown nudDelay;
        private System.Windows.Forms.ComboBox cbDelay;
    }
}
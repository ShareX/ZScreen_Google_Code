namespace ZSS
{
    partial class Crop
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
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
            this.SuspendLayout();
            // 
            // Crop
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.CausesValidation = false;
            this.ClientSize = new System.Drawing.Size(400, 400);
            this.ControlBox = false;
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Crop";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.TopMost = true;
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Crop_MouseUp);
            this.Shown += new System.EventHandler(this.Crop_Shown);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Crop_FormClosed);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Crop_MouseDown);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Crop_KeyPress);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Crop_FormClosing);
            this.ResumeLayout(false);

        }

        #endregion

    }
}
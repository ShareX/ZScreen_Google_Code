namespace ZSS.Forms
{
    partial class ShowScreenshot
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
            this.SuspendLayout();
            // 
            // ShowScreenshot
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 264);
            this.Name = "ShowScreenshot";
            this.Text = "ShowScreenshot";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.ShowScreenshot_Load);
            this.Shown += new System.EventHandler(this.ShowScreenshot_Shown);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ShowScreenshot_MouseDown);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ShowScreenshot_KeyDown);
            this.ResumeLayout(false);

        }

        #endregion
    }
}
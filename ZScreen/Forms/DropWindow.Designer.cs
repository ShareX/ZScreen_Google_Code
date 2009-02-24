namespace ZSS.Forms
{
    partial class DropWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DropWindow));
            this.SuspendLayout();
            // 
            // DropWindow
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(96, 96);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "DropWindow";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "DropWindow";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.DropWindow_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.DropWindow_Paint);
            this.DoubleClick += new System.EventHandler(this.DropWindow_DoubleClick);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.DropWindow_DragDrop);
            this.Click += new System.EventHandler(this.DropWindow_Click);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.DropWindow_DragEnter);
            this.ResumeLayout(false);

        }

        #endregion
    }
}
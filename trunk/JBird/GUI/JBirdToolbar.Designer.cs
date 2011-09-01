namespace JBirdGUI
{
    partial class JBirdToolbar
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
            this.tsApp = new System.Windows.Forms.ToolStrip();
            this.tsbCaptureScreen = new System.Windows.Forms.ToolStripButton();
            this.tsbCaptureActiveWindow = new System.Windows.Forms.ToolStripButton();
            this.tsbCaptureWindow = new System.Windows.Forms.ToolStripButton();
            this.tsApp.SuspendLayout();
            this.SuspendLayout();
            // 
            // tsApp
            // 
            this.tsApp.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbCaptureScreen,
            this.tsbCaptureActiveWindow,
            this.tsbCaptureWindow});
            this.tsApp.Location = new System.Drawing.Point(0, 0);
            this.tsApp.MinimumSize = new System.Drawing.Size(128, 32);
            this.tsApp.Name = "tsApp";
            this.tsApp.Size = new System.Drawing.Size(128, 32);
            this.tsApp.TabIndex = 1;
            this.tsApp.Text = "JBird";
            // 
            // tsbCaptureScreen
            // 
            this.tsbCaptureScreen.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbCaptureScreen.Image = global::JBirdGUI.Properties.Resources.monitor;
            this.tsbCaptureScreen.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbCaptureScreen.Name = "tsbCaptureScreen";
            this.tsbCaptureScreen.Size = new System.Drawing.Size(23, 29);
            this.tsbCaptureScreen.Text = "Capture Screen";
            this.tsbCaptureScreen.Click += new System.EventHandler(this.tsbCaptureScreen_Click);
            // 
            // tsbCaptureActiveWindow
            // 
            this.tsbCaptureActiveWindow.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbCaptureActiveWindow.Image = global::JBirdGUI.Properties.Resources.application_go;
            this.tsbCaptureActiveWindow.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbCaptureActiveWindow.Name = "tsbCaptureActiveWindow";
            this.tsbCaptureActiveWindow.Size = new System.Drawing.Size(23, 29);
            this.tsbCaptureActiveWindow.Text = "Capture Active Window";
            this.tsbCaptureActiveWindow.Click += new System.EventHandler(this.tsbCaptureActiveWindow_Click);
            // 
            // tsbCaptureWindow
            // 
            this.tsbCaptureWindow.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbCaptureWindow.Image = global::JBirdGUI.Properties.Resources.application_double;
            this.tsbCaptureWindow.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbCaptureWindow.Name = "tsbCaptureWindow";
            this.tsbCaptureWindow.Size = new System.Drawing.Size(23, 29);
            this.tsbCaptureWindow.Text = "Capture Window";
            this.tsbCaptureWindow.Click += new System.EventHandler(this.tsbCaptureWindow_Click);
            // 
            // JBirdToolbar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(122, 32);
            this.Controls.Add(this.tsApp);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximumSize = new System.Drawing.Size(128, 56);
            this.MinimumSize = new System.Drawing.Size(128, 56);
            this.Name = "JBirdToolbar";
            this.Text = "JBird Toolbar";
            this.tsApp.ResumeLayout(false);
            this.tsApp.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip tsApp;
        private System.Windows.Forms.ToolStripButton tsbCaptureScreen;
        private System.Windows.Forms.ToolStripButton tsbCaptureActiveWindow;
        private System.Windows.Forms.ToolStripButton tsbCaptureWindow;
    }
}

namespace ZScreenLib
{
    partial class DestSelector
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DestSelector));
            this.miniToolStrip = new System.Windows.Forms.ToolStrip();
            this.tsDest = new System.Windows.Forms.ToolStrip();
            this.tsddbClipboardContent = new System.Windows.Forms.ToolStripDropDownButton();
            this.tsddbDestImage = new System.Windows.Forms.ToolStripDropDownButton();
            this.tsddDestText = new System.Windows.Forms.ToolStripDropDownButton();
            this.tsddDestFile = new System.Windows.Forms.ToolStripDropDownButton();
            this.tsddbDestLink = new System.Windows.Forms.ToolStripDropDownButton();
            this.tsbDestConfig = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsDest.SuspendLayout();
            this.SuspendLayout();
            // 
            // miniToolStrip
            // 
            this.miniToolStrip.AutoSize = false;
            this.miniToolStrip.BackColor = System.Drawing.Color.White;
            this.miniToolStrip.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.miniToolStrip.CanOverflow = false;
            this.miniToolStrip.Dock = System.Windows.Forms.DockStyle.None;
            this.miniToolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.miniToolStrip.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Table;
            this.miniToolStrip.Location = new System.Drawing.Point(0, 138);
            this.miniToolStrip.Name = "miniToolStrip";
            this.miniToolStrip.Size = new System.Drawing.Size(346, 149);
            this.miniToolStrip.TabIndex = 128;
            // 
            // tsDest
            // 
            this.tsDest.AutoSize = false;
            this.tsDest.BackColor = System.Drawing.Color.White;
            this.tsDest.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.tsDest.CanOverflow = false;
            this.tsDest.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tsDest.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.tsDest.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsddbClipboardContent,
            this.toolStripSeparator1,
            this.tsddbDestImage,
            this.tsddDestText,
            this.tsddDestFile,
            this.tsddbDestLink,
            this.toolStripSeparator2,
            this.tsbDestConfig});
            this.tsDest.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.VerticalStackWithOverflow;
            this.tsDest.Location = new System.Drawing.Point(0, 0);
            this.tsDest.Name = "tsDest";
            this.tsDest.Size = new System.Drawing.Size(352, 168);
            this.tsDest.TabIndex = 128;
            this.tsDest.Text = "Destinations";
            // 
            // tsddbClipboardContent
            // 
            this.tsddbClipboardContent.Image = ((System.Drawing.Image)(resources.GetObject("tsddbClipboardContent.Image")));
            this.tsddbClipboardContent.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.tsddbClipboardContent.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsddbClipboardContent.Name = "tsddbClipboardContent";
            this.tsddbClipboardContent.Size = new System.Drawing.Size(350, 20);
            this.tsddbClipboardContent.Text = "Clipboard Content:";
            this.tsddbClipboardContent.DropDownItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.tsddbClipboardContent_DropDownItemClicked);
            // 
            // tsddbDestImage
            // 
            this.tsddbDestImage.Image = ((System.Drawing.Image)(resources.GetObject("tsddbDestImage.Image")));
            this.tsddbDestImage.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.tsddbDestImage.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsddbDestImage.Name = "tsddbDestImage";
            this.tsddbDestImage.Overflow = System.Windows.Forms.ToolStripItemOverflow.Never;
            this.tsddbDestImage.Size = new System.Drawing.Size(350, 20);
            this.tsddbDestImage.Text = "Image output:";
            // 
            // tsddDestText
            // 
            this.tsddDestText.Image = global::ZScreenLib.Properties.Resources.pencil_go;
            this.tsddDestText.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.tsddDestText.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsddDestText.Name = "tsddDestText";
            this.tsddDestText.Overflow = System.Windows.Forms.ToolStripItemOverflow.Never;
            this.tsddDestText.Size = new System.Drawing.Size(350, 20);
            this.tsddDestText.Text = "Text output:";
            // 
            // tsddDestFile
            // 
            this.tsddDestFile.Image = global::ZScreenLib.Properties.Resources.page_go;
            this.tsddDestFile.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.tsddDestFile.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsddDestFile.Name = "tsddDestFile";
            this.tsddDestFile.Overflow = System.Windows.Forms.ToolStripItemOverflow.Never;
            this.tsddDestFile.Size = new System.Drawing.Size(350, 20);
            this.tsddDestFile.Text = "File output:";
            // 
            // tsddbDestLink
            // 
            this.tsddbDestLink.Image = global::ZScreenLib.Properties.Resources.link_go;
            this.tsddbDestLink.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.tsddbDestLink.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsddbDestLink.Name = "tsddbDestLink";
            this.tsddbDestLink.Overflow = System.Windows.Forms.ToolStripItemOverflow.Never;
            this.tsddbDestLink.Size = new System.Drawing.Size(350, 20);
            this.tsddbDestLink.Text = "URL Shortener";
            this.tsddbDestLink.DropDownItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.tsddDestLinks_DropDownItemClicked);
            // 
            // tsbDestConfig
            // 
            this.tsbDestConfig.Image = global::ZScreenLib.Properties.Resources.server_edit;
            this.tsbDestConfig.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.tsbDestConfig.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbDestConfig.Name = "tsbDestConfig";
            this.tsbDestConfig.Size = new System.Drawing.Size(350, 20);
            this.tsbDestConfig.Text = "Open destinations configuration..";
            this.tsbDestConfig.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.tsbDestConfig.Click += new System.EventHandler(this.tsbDestConfig_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(350, 6);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(350, 6);
            // 
            // DestSelector
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tsDest);
            this.Name = "DestSelector";
            this.Size = new System.Drawing.Size(352, 168);
            this.tsDest.ResumeLayout(false);
            this.tsDest.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion Component Designer generated code

        private System.Windows.Forms.ToolStrip miniToolStrip;
        private System.Windows.Forms.ToolStrip tsDest;
        public System.Windows.Forms.ToolStripDropDownButton tsddbClipboardContent;
        public System.Windows.Forms.ToolStripDropDownButton tsddbDestImage;
        public System.Windows.Forms.ToolStripDropDownButton tsddDestText;
        public System.Windows.Forms.ToolStripDropDownButton tsddDestFile;
        public System.Windows.Forms.ToolStripDropDownButton tsddbDestLink;
        private System.Windows.Forms.ToolStripButton tsbDestConfig;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;

    }
}
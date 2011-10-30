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
            this.tsddbOutputs = new System.Windows.Forms.ToolStripDropDownButton();
            this.tsddbClipboardContent = new System.Windows.Forms.ToolStripDropDownButton();
            this.tsddbLinkFormat = new System.Windows.Forms.ToolStripDropDownButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.tsddbDestImage = new System.Windows.Forms.ToolStripDropDownButton();
            this.tsddbDestText = new System.Windows.Forms.ToolStripDropDownButton();
            this.tsddbDestFile = new System.Windows.Forms.ToolStripDropDownButton();
            this.tsddbDestLink = new System.Windows.Forms.ToolStripDropDownButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbDestConfig = new System.Windows.Forms.ToolStripButton();
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
            this.tsbDestConfig,
            this.tsddbOutputs,
            this.toolStripSeparator2,
            this.tsddbClipboardContent,
            this.tsddbLinkFormat,
            this.toolStripSeparator3,
            this.tsddbDestImage,
            this.tsddbDestText,
            this.tsddbDestFile,
            this.tsddbDestLink});
            this.tsDest.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.VerticalStackWithOverflow;
            this.tsDest.Location = new System.Drawing.Point(0, 0);
            this.tsDest.Name = "tsDest";
            this.tsDest.ShowItemToolTips = false;
            this.tsDest.Size = new System.Drawing.Size(352, 200);
            this.tsDest.TabIndex = 128;
            this.tsDest.Text = "Destinations";
            // 
            // tsddbOutputs
            // 
            this.tsddbOutputs.Image = ((System.Drawing.Image)(resources.GetObject("tsddbOutputs.Image")));
            this.tsddbOutputs.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.tsddbOutputs.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsddbOutputs.Name = "tsddbOutputs";
            this.tsddbOutputs.Size = new System.Drawing.Size(350, 20);
            this.tsddbOutputs.Tag = "Outputs";
            this.tsddbOutputs.Text = "Outputs: loading...";
            // 
            // tsddbClipboardContent
            // 
            this.tsddbClipboardContent.Image = ((System.Drawing.Image)(resources.GetObject("tsddbClipboardContent.Image")));
            this.tsddbClipboardContent.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.tsddbClipboardContent.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsddbClipboardContent.Name = "tsddbClipboardContent";
            this.tsddbClipboardContent.Size = new System.Drawing.Size(350, 20);
            this.tsddbClipboardContent.Tag = "Clipboard content";
            this.tsddbClipboardContent.Text = "Clipboard content: loading...";
            this.tsddbClipboardContent.DropDownItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.tsddbClipboardContent_DropDownItemClicked);
            // 
            // tsddbLinkFormat
            // 
            this.tsddbLinkFormat.Image = ((System.Drawing.Image)(resources.GetObject("tsddbLinkFormat.Image")));
            this.tsddbLinkFormat.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.tsddbLinkFormat.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsddbLinkFormat.Name = "tsddbLinkFormat";
            this.tsddbLinkFormat.Size = new System.Drawing.Size(350, 20);
            this.tsddbLinkFormat.Tag = "URL format";
            this.tsddbLinkFormat.Text = "URL format: loading...";
            this.tsddbLinkFormat.DropDownItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.tsddbLinkFormat_DropDownItemClicked);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(350, 6);
            // 
            // tsddbDestImage
            // 
            this.tsddbDestImage.Image = ((System.Drawing.Image)(resources.GetObject("tsddbDestImage.Image")));
            this.tsddbDestImage.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.tsddbDestImage.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsddbDestImage.Name = "tsddbDestImage";
            this.tsddbDestImage.Overflow = System.Windows.Forms.ToolStripItemOverflow.Never;
            this.tsddbDestImage.Size = new System.Drawing.Size(350, 20);
            this.tsddbDestImage.Tag = "Upload image to";
            this.tsddbDestImage.Text = "Upload image to: loading...";
            // 
            // tsddbDestText
            // 
            this.tsddbDestText.Image = global::ZScreenLib.Properties.Resources.pencil_go;
            this.tsddbDestText.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.tsddbDestText.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsddbDestText.Name = "tsddbDestText";
            this.tsddbDestText.Overflow = System.Windows.Forms.ToolStripItemOverflow.Never;
            this.tsddbDestText.Size = new System.Drawing.Size(350, 20);
            this.tsddbDestText.Tag = "Upload text to";
            this.tsddbDestText.Text = "Upload text to: loading...";
            // 
            // tsddDestFile
            // 
            this.tsddbDestFile.Image = global::ZScreenLib.Properties.Resources.page_go;
            this.tsddbDestFile.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.tsddbDestFile.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsddbDestFile.Name = "tsddDestFile";
            this.tsddbDestFile.Overflow = System.Windows.Forms.ToolStripItemOverflow.Never;
            this.tsddbDestFile.Size = new System.Drawing.Size(350, 20);
            this.tsddbDestFile.Tag = "Upload file to";
            this.tsddbDestFile.Text = "Upload file to: loading...";
            // 
            // tsddbDestLink
            // 
            this.tsddbDestLink.Image = global::ZScreenLib.Properties.Resources.link_go;
            this.tsddbDestLink.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.tsddbDestLink.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsddbDestLink.Name = "tsddbDestLink";
            this.tsddbDestLink.Overflow = System.Windows.Forms.ToolStripItemOverflow.Never;
            this.tsddbDestLink.Size = new System.Drawing.Size(350, 20);
            this.tsddbDestLink.Tag = "URL shortener";
            this.tsddbDestLink.Text = "URL shortener: loading...";
            this.tsddbDestLink.DropDownItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.tsddDestLinks_DropDownItemClicked);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(350, 6);
            // 
            // tsbDestConfig
            // 
            this.tsbDestConfig.Image = global::ZScreenLib.Properties.Resources.server_edit;
            this.tsbDestConfig.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.tsbDestConfig.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbDestConfig.Name = "tsbDestConfig";
            this.tsbDestConfig.Size = new System.Drawing.Size(350, 20);
            this.tsbDestConfig.Text = "Open outputs configuration...";
            this.tsbDestConfig.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.tsbDestConfig.Click += new System.EventHandler(this.tsbDestConfig_Click);
            // 
            // DestSelector
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tsDest);
            this.Name = "DestSelector";
            this.Size = new System.Drawing.Size(352, 200);
            this.Load += new System.EventHandler(this.DestSelector_Load);
            this.tsDest.ResumeLayout(false);
            this.tsDest.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion Component Designer generated code

        private System.Windows.Forms.ToolStrip miniToolStrip;
        private System.Windows.Forms.ToolStrip tsDest;
        public System.Windows.Forms.ToolStripDropDownButton tsddbClipboardContent;
        public System.Windows.Forms.ToolStripDropDownButton tsddbDestImage;
        public System.Windows.Forms.ToolStripDropDownButton tsddbDestText;
        public System.Windows.Forms.ToolStripDropDownButton tsddbDestFile;
        public System.Windows.Forms.ToolStripDropDownButton tsddbDestLink;
        private System.Windows.Forms.ToolStripButton tsbDestConfig;
        public System.Windows.Forms.ToolStripDropDownButton tsddbLinkFormat;
        public System.Windows.Forms.ToolStripDropDownButton tsddbOutputs;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;

    }
}
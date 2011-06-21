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
            this.gbMainOptions = new System.Windows.Forms.GroupBox();
            this.tsDest = new System.Windows.Forms.ToolStrip();
            this.tsddbDestImage = new System.Windows.Forms.ToolStripDropDownButton();
            this.tsddDestText = new System.Windows.Forms.ToolStripDropDownButton();
            this.tsddDestFile = new System.Windows.Forms.ToolStripDropDownButton();
            this.tsddDestLink = new System.Windows.Forms.ToolStripDropDownButton();
            this.tsbDestConfig = new System.Windows.Forms.ToolStripButton();
            this.gbMainOptions.SuspendLayout();
            this.tsDest.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbMainOptions
            // 
            this.gbMainOptions.BackColor = System.Drawing.Color.Transparent;
            this.gbMainOptions.Controls.Add(this.tsDest);
            this.gbMainOptions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbMainOptions.Location = new System.Drawing.Point(0, 0);
            this.gbMainOptions.Name = "gbMainOptions";
            this.gbMainOptions.Size = new System.Drawing.Size(352, 144);
            this.gbMainOptions.TabIndex = 80;
            this.gbMainOptions.TabStop = false;
            this.gbMainOptions.Text = "Active Destinations";
            // 
            // tsDest
            // 
            this.tsDest.AutoSize = false;
            this.tsDest.BackColor = System.Drawing.Color.White;
            this.tsDest.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.tsDest.CanOverflow = false;
            this.tsDest.Dock = System.Windows.Forms.DockStyle.None;
            this.tsDest.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.tsDest.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsddbDestImage,
            this.tsddDestText,
            this.tsddDestFile,
            this.tsddDestLink,
            this.tsbDestConfig});
            this.tsDest.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Table;
            this.tsDest.Location = new System.Drawing.Point(8, 16);
            this.tsDest.Name = "tsDest";
            this.tsDest.Size = new System.Drawing.Size(336, 120);
            this.tsDest.TabIndex = 128;
            this.tsDest.Text = "Destinations";
            // 
            // tsddDestImage
            // 
            this.tsddbDestImage.Image = ((System.Drawing.Image)(resources.GetObject("tsddDestImage.Image")));
            this.tsddbDestImage.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.tsddbDestImage.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsddbDestImage.Name = "tsddDestImage";
            this.tsddbDestImage.Overflow = System.Windows.Forms.ToolStripItemOverflow.Never;
            this.tsddbDestImage.Size = new System.Drawing.Size(105, 20);
            this.tsddbDestImage.Text = "Image output:";
            // 
            // tsddDestText
            // 
            this.tsddDestText.Image = global::ZScreenLib.Properties.Resources.pencil_go;
            this.tsddDestText.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.tsddDestText.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsddDestText.Name = "tsddDestText";
            this.tsddDestText.Overflow = System.Windows.Forms.ToolStripItemOverflow.Never;
            this.tsddDestText.Size = new System.Drawing.Size(97, 20);
            this.tsddDestText.Text = "Text output:";
            // 
            // tsddDestFile
            // 
            this.tsddDestFile.Image = global::ZScreenLib.Properties.Resources.page_go;
            this.tsddDestFile.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.tsddDestFile.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsddDestFile.Name = "tsddDestFile";
            this.tsddDestFile.Overflow = System.Windows.Forms.ToolStripItemOverflow.Never;
            this.tsddDestFile.Size = new System.Drawing.Size(91, 20);
            this.tsddDestFile.Text = "File output:";
            // 
            // tsddDestLinks
            // 
            this.tsddDestLink.Image = global::ZScreenLib.Properties.Resources.link_go;
            this.tsddDestLink.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.tsddDestLink.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsddDestLink.Name = "tsddDestLinks";
            this.tsddDestLink.Overflow = System.Windows.Forms.ToolStripItemOverflow.Never;
            this.tsddDestLink.Size = new System.Drawing.Size(106, 20);
            this.tsddDestLink.Text = "URL Shortener";
            this.tsddDestLink.DropDownItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.tsddDestLinks_DropDownItemClicked);
            // 
            // tsbDestConfig
            // 
            this.tsbDestConfig.Image = global::ZScreenLib.Properties.Resources.server_edit;
            this.tsbDestConfig.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.tsbDestConfig.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbDestConfig.Name = "tsbDestConfig";
            this.tsbDestConfig.Size = new System.Drawing.Size(188, 20);
            this.tsbDestConfig.Text = "Open destinations configuration..";
            this.tsbDestConfig.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.tsbDestConfig.Click += new System.EventHandler(this.tsbDestConfig_Click);
            // 
            // DestSelector
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gbMainOptions);
            this.Name = "DestSelector";
            this.Size = new System.Drawing.Size(352, 144);
            this.gbMainOptions.ResumeLayout(false);
            this.tsDest.ResumeLayout(false);
            this.tsDest.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion Component Designer generated code

        internal System.Windows.Forms.GroupBox gbMainOptions;
        public System.Windows.Forms.ToolStripDropDownButton tsddbDestImage;
        public System.Windows.Forms.ToolStripDropDownButton tsddDestText;
        public System.Windows.Forms.ToolStripDropDownButton tsddDestFile;
        private System.Windows.Forms.ToolStrip tsDest;
        private System.Windows.Forms.ToolStripButton tsbDestConfig;
        public System.Windows.Forms.ToolStripDropDownButton tsddDestLink;
    }
}
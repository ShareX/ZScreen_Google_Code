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
            this.tsddDestImage = new System.Windows.Forms.ToolStripDropDownButton();
            this.tsddDestText = new System.Windows.Forms.ToolStripDropDownButton();
            this.tsddDestFile = new System.Windows.Forms.ToolStripDropDownButton();
            this.tsddLinkShortener = new System.Windows.Forms.ToolStripDropDownButton();
            this.tscbURLShorteners = new System.Windows.Forms.ToolStripComboBox();
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
            this.tsddDestImage,
            this.tsddDestText,
            this.tsddDestFile,
            this.tsddLinkShortener,
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
            this.tsddDestImage.Image = ((System.Drawing.Image)(resources.GetObject("tsddDestImage.Image")));
            this.tsddDestImage.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.tsddDestImage.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsddDestImage.Name = "tsddDestImage";
            this.tsddDestImage.Overflow = System.Windows.Forms.ToolStripItemOverflow.Never;
            this.tsddDestImage.Size = new System.Drawing.Size(105, 20);
            this.tsddDestImage.Text = "Image output:";
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
            // tsddLinkShortener
            // 
            this.tsddLinkShortener.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tscbURLShorteners});
            this.tsddLinkShortener.Image = global::ZScreenLib.Properties.Resources.link_go;
            this.tsddLinkShortener.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.tsddLinkShortener.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsddLinkShortener.Name = "tsddLinkShortener";
            this.tsddLinkShortener.Overflow = System.Windows.Forms.ToolStripItemOverflow.Never;
            this.tsddLinkShortener.Size = new System.Drawing.Size(106, 20);
            this.tsddLinkShortener.Text = "URL Shortener";
            // 
            // tscbURLShorteners
            // 
            this.tscbURLShorteners.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.tscbURLShorteners.Name = "tscbURLShorteners";
            this.tscbURLShorteners.Size = new System.Drawing.Size(121, 21);
            this.tscbURLShorteners.SelectedIndexChanged += new System.EventHandler(this.tscbURLShortener_SelectedIndexChanged);
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
        public System.Windows.Forms.ToolStripDropDownButton tsddDestImage;
        public System.Windows.Forms.ToolStripDropDownButton tsddDestText;
        public System.Windows.Forms.ToolStripDropDownButton tsddDestFile;
        private System.Windows.Forms.ToolStrip tsDest;
        private System.Windows.Forms.ToolStripDropDownButton tsddLinkShortener;
        private System.Windows.Forms.ToolStripButton tsbDestConfig;
        public System.Windows.Forms.ToolStripComboBox tscbURLShorteners;
    }
}
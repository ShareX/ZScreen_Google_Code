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
            this.tsddDestImages = new System.Windows.Forms.ToolStripDropDownButton();
            this.tsddDestText = new System.Windows.Forms.ToolStripDropDownButton();
            this.tsddDestFiles = new System.Windows.Forms.ToolStripDropDownButton();
            this.btnOpenUploadersConfig = new System.Windows.Forms.Button();
            this.cboFileUploaders = new System.Windows.Forms.ComboBox();
            this.cboURLShorteners = new System.Windows.Forms.ComboBox();
            this.lblURLShortener = new System.Windows.Forms.Label();
            this.cboTextUploaders = new System.Windows.Forms.ComboBox();
            this.gbMainOptions.SuspendLayout();
            this.tsDest.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbMainOptions
            // 
            this.gbMainOptions.BackColor = System.Drawing.Color.Transparent;
            this.gbMainOptions.Controls.Add(this.tsDest);
            this.gbMainOptions.Controls.Add(this.btnOpenUploadersConfig);
            this.gbMainOptions.Controls.Add(this.cboFileUploaders);
            this.gbMainOptions.Controls.Add(this.cboURLShorteners);
            this.gbMainOptions.Controls.Add(this.lblURLShortener);
            this.gbMainOptions.Controls.Add(this.cboTextUploaders);
            this.gbMainOptions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbMainOptions.Location = new System.Drawing.Point(0, 0);
            this.gbMainOptions.Name = "gbMainOptions";
            this.gbMainOptions.Size = new System.Drawing.Size(347, 153);
            this.gbMainOptions.TabIndex = 80;
            this.gbMainOptions.TabStop = false;
            this.gbMainOptions.Text = "Active Destinations";
            // 
            // tsDest
            // 
            this.tsDest.BackColor = System.Drawing.Color.White;
            this.tsDest.Dock = System.Windows.Forms.DockStyle.None;
            this.tsDest.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.tsDest.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsddDestImages,
            this.tsddDestText,
            this.tsddDestFiles});
            this.tsDest.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.VerticalStackWithOverflow;
            this.tsDest.Location = new System.Drawing.Point(8, 24);
            this.tsDest.Name = "tsDest";
            this.tsDest.Size = new System.Drawing.Size(118, 90);
            this.tsDest.TabIndex = 128;
            this.tsDest.Text = "Destinations";
            // 
            // tsddDestImages
            // 
            this.tsddDestImages.Image = ((System.Drawing.Image)(resources.GetObject("tsddDestImages.Image")));
            this.tsddDestImages.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.tsddDestImages.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsddDestImages.Name = "tsddDestImages";
            this.tsddDestImages.Size = new System.Drawing.Size(116, 20);
            this.tsddDestImages.Text = "Send images to";
            // 
            // tsddDestText
            // 
            this.tsddDestText.Image = global::ZScreenLib.Properties.Resources.pencil_go;
            this.tsddDestText.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.tsddDestText.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsddDestText.Name = "tsddDestText";
            this.tsddDestText.Size = new System.Drawing.Size(116, 20);
            this.tsddDestText.Text = "Send text to";
            // 
            // tsddDestFiles
            // 
            this.tsddDestFiles.Image = global::ZScreenLib.Properties.Resources.page_go;
            this.tsddDestFiles.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.tsddDestFiles.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsddDestFiles.Name = "tsddDestFiles";
            this.tsddDestFiles.Size = new System.Drawing.Size(116, 20);
            this.tsddDestFiles.Text = "Send files to";
            // 
            // btnOpenUploadersConfig
            // 
            this.btnOpenUploadersConfig.Location = new System.Drawing.Point(128, 120);
            this.btnOpenUploadersConfig.Name = "btnOpenUploadersConfig";
            this.btnOpenUploadersConfig.Size = new System.Drawing.Size(208, 23);
            this.btnOpenUploadersConfig.TabIndex = 127;
            this.btnOpenUploadersConfig.Text = "Open destinations configuration...";
            this.btnOpenUploadersConfig.UseVisualStyleBackColor = true;
            this.btnOpenUploadersConfig.Click += new System.EventHandler(this.btnOpenUploadersConfig_Click);
            // 
            // cboFileUploaders
            // 
            this.cboFileUploaders.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboFileUploaders.FormattingEnabled = true;
            this.cboFileUploaders.Location = new System.Drawing.Point(128, 72);
            this.cboFileUploaders.Name = "cboFileUploaders";
            this.cboFileUploaders.Size = new System.Drawing.Size(208, 21);
            this.cboFileUploaders.TabIndex = 126;
            // 
            // cboURLShorteners
            // 
            this.cboURLShorteners.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboURLShorteners.FormattingEnabled = true;
            this.cboURLShorteners.Location = new System.Drawing.Point(128, 96);
            this.cboURLShorteners.Name = "cboURLShorteners";
            this.cboURLShorteners.Size = new System.Drawing.Size(208, 21);
            this.cboURLShorteners.TabIndex = 124;
            // 
            // lblURLShortener
            // 
            this.lblURLShortener.Location = new System.Drawing.Point(16, 96);
            this.lblURLShortener.Name = "lblURLShortener";
            this.lblURLShortener.Size = new System.Drawing.Size(96, 21);
            this.lblURLShortener.TabIndex = 123;
            this.lblURLShortener.Text = "URL Shortener:";
            this.lblURLShortener.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cboTextUploaders
            // 
            this.cboTextUploaders.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTextUploaders.FormattingEnabled = true;
            this.cboTextUploaders.Location = new System.Drawing.Point(128, 48);
            this.cboTextUploaders.Name = "cboTextUploaders";
            this.cboTextUploaders.Size = new System.Drawing.Size(208, 21);
            this.cboTextUploaders.TabIndex = 121;
            // 
            // DestSelector
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gbMainOptions);
            this.Name = "DestSelector";
            this.Size = new System.Drawing.Size(347, 153);
            this.gbMainOptions.ResumeLayout(false);
            this.gbMainOptions.PerformLayout();
            this.tsDest.ResumeLayout(false);
            this.tsDest.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion Component Designer generated code

        internal System.Windows.Forms.GroupBox gbMainOptions;
        public System.Windows.Forms.ComboBox cboFileUploaders;
        public System.Windows.Forms.ComboBox cboURLShorteners;
        private System.Windows.Forms.Label lblURLShortener;
        public System.Windows.Forms.ComboBox cboTextUploaders;
        private System.Windows.Forms.Button btnOpenUploadersConfig;
        public System.Windows.Forms.ToolStripDropDownButton tsddDestImages;
        public System.Windows.Forms.ToolStripDropDownButton tsddDestText;
        public System.Windows.Forms.ToolStripDropDownButton tsddDestFiles;
        private System.Windows.Forms.ToolStrip tsDest;
    }
}
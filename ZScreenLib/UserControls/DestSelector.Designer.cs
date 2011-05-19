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
            this.gbMainOptions = new System.Windows.Forms.GroupBox();
            this.cboFileUploaders = new System.Windows.Forms.ComboBox();
            this.lblFileUploader = new System.Windows.Forms.Label();
            this.cboURLShorteners = new System.Windows.Forms.ComboBox();
            this.lblURLShortener = new System.Windows.Forms.Label();
            this.lblImageUploader = new System.Windows.Forms.Label();
            this.lblTextUploader = new System.Windows.Forms.Label();
            this.cboImageUploaders = new System.Windows.Forms.ComboBox();
            this.cboTextUploaders = new System.Windows.Forms.ComboBox();
            this.btnOpenUploadersConfig = new System.Windows.Forms.Button();
            this.gbMainOptions.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbMainOptions
            // 
            this.gbMainOptions.BackColor = System.Drawing.Color.Transparent;
            this.gbMainOptions.Controls.Add(this.btnOpenUploadersConfig);
            this.gbMainOptions.Controls.Add(this.cboFileUploaders);
            this.gbMainOptions.Controls.Add(this.lblFileUploader);
            this.gbMainOptions.Controls.Add(this.cboURLShorteners);
            this.gbMainOptions.Controls.Add(this.lblURLShortener);
            this.gbMainOptions.Controls.Add(this.lblImageUploader);
            this.gbMainOptions.Controls.Add(this.lblTextUploader);
            this.gbMainOptions.Controls.Add(this.cboImageUploaders);
            this.gbMainOptions.Controls.Add(this.cboTextUploaders);
            this.gbMainOptions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbMainOptions.Location = new System.Drawing.Point(0, 0);
            this.gbMainOptions.Name = "gbMainOptions";
            this.gbMainOptions.Size = new System.Drawing.Size(347, 153);
            this.gbMainOptions.TabIndex = 80;
            this.gbMainOptions.TabStop = false;
            this.gbMainOptions.Text = "Active Destinations";
            // 
            // cboFileUploaders
            // 
            this.cboFileUploaders.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboFileUploaders.FormattingEnabled = true;
            this.cboFileUploaders.Location = new System.Drawing.Point(120, 70);
            this.cboFileUploaders.Name = "cboFileUploaders";
            this.cboFileUploaders.Size = new System.Drawing.Size(208, 21);
            this.cboFileUploaders.TabIndex = 126;
            // 
            // lblFileUploader
            // 
            this.lblFileUploader.Location = new System.Drawing.Point(16, 72);
            this.lblFileUploader.Name = "lblFileUploader";
            this.lblFileUploader.Size = new System.Drawing.Size(96, 16);
            this.lblFileUploader.TabIndex = 125;
            this.lblFileUploader.Text = "File destination:";
            this.lblFileUploader.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cboURLShorteners
            // 
            this.cboURLShorteners.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboURLShorteners.FormattingEnabled = true;
            this.cboURLShorteners.Location = new System.Drawing.Point(120, 94);
            this.cboURLShorteners.Name = "cboURLShorteners";
            this.cboURLShorteners.Size = new System.Drawing.Size(208, 21);
            this.cboURLShorteners.TabIndex = 124;
            // 
            // lblURLShortener
            // 
            this.lblURLShortener.Location = new System.Drawing.Point(16, 96);
            this.lblURLShortener.Name = "lblURLShortener";
            this.lblURLShortener.Size = new System.Drawing.Size(96, 16);
            this.lblURLShortener.TabIndex = 123;
            this.lblURLShortener.Text = "URL Shortener:";
            this.lblURLShortener.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblImageUploader
            // 
            this.lblImageUploader.Location = new System.Drawing.Point(16, 24);
            this.lblImageUploader.Name = "lblImageUploader";
            this.lblImageUploader.Size = new System.Drawing.Size(96, 16);
            this.lblImageUploader.TabIndex = 1;
            this.lblImageUploader.Text = "Image destination:";
            this.lblImageUploader.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblTextUploader
            // 
            this.lblTextUploader.Location = new System.Drawing.Point(16, 48);
            this.lblTextUploader.Name = "lblTextUploader";
            this.lblTextUploader.Size = new System.Drawing.Size(96, 16);
            this.lblTextUploader.TabIndex = 122;
            this.lblTextUploader.Text = "Text destination:";
            this.lblTextUploader.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cboImageUploaders
            // 
            this.cboImageUploaders.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboImageUploaders.FormattingEnabled = true;
            this.cboImageUploaders.Location = new System.Drawing.Point(120, 24);
            this.cboImageUploaders.Name = "cboImageUploaders";
            this.cboImageUploaders.Size = new System.Drawing.Size(208, 21);
            this.cboImageUploaders.TabIndex = 0;
            // 
            // cboTextUploaders
            // 
            this.cboTextUploaders.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTextUploaders.FormattingEnabled = true;
            this.cboTextUploaders.Location = new System.Drawing.Point(120, 46);
            this.cboTextUploaders.Name = "cboTextUploaders";
            this.cboTextUploaders.Size = new System.Drawing.Size(208, 21);
            this.cboTextUploaders.TabIndex = 121;
            // 
            // btnOpenUploadersConfig
            // 
            this.btnOpenUploadersConfig.Location = new System.Drawing.Point(120, 118);
            this.btnOpenUploadersConfig.Name = "btnOpenUploadersConfig";
            this.btnOpenUploadersConfig.Size = new System.Drawing.Size(208, 23);
            this.btnOpenUploadersConfig.TabIndex = 127;
            this.btnOpenUploadersConfig.Text = "Open destinations config...";
            this.btnOpenUploadersConfig.UseVisualStyleBackColor = true;
            this.btnOpenUploadersConfig.Click += new System.EventHandler(this.btnOpenUploadersConfig_Click);
            // 
            // DestSelector
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gbMainOptions);
            this.Name = "DestSelector";
            this.Size = new System.Drawing.Size(347, 153);
            this.gbMainOptions.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion Component Designer generated code

        internal System.Windows.Forms.GroupBox gbMainOptions;
        public System.Windows.Forms.ComboBox cboFileUploaders;
        private System.Windows.Forms.Label lblFileUploader;
        public System.Windows.Forms.ComboBox cboURLShorteners;
        private System.Windows.Forms.Label lblURLShortener;
        internal System.Windows.Forms.Label lblImageUploader;
        internal System.Windows.Forms.Label lblTextUploader;
        public System.Windows.Forms.ComboBox cboImageUploaders;
        public System.Windows.Forms.ComboBox cboTextUploaders;
        private System.Windows.Forms.Button btnOpenUploadersConfig;
    }
}
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
            this.chkTextUploaderEnabled = new System.Windows.Forms.CheckBox();
            this.chkImageUploaderEnabled = new System.Windows.Forms.CheckBox();
            this.cboFileUploaders = new System.Windows.Forms.ComboBox();
            this.lblFileUploader = new System.Windows.Forms.Label();
            this.cboURLShorteners = new System.Windows.Forms.ComboBox();
            this.lblURLShortener = new System.Windows.Forms.Label();
            this.lblImageUploader = new System.Windows.Forms.Label();
            this.lblTextUploader = new System.Windows.Forms.Label();
            this.cboImageUploaders = new System.Windows.Forms.ComboBox();
            this.cboTextUploaders = new System.Windows.Forms.ComboBox();
            this.gbMainOptions.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbMainOptions
            // 
            this.gbMainOptions.BackColor = System.Drawing.Color.Transparent;
            this.gbMainOptions.Controls.Add(this.chkTextUploaderEnabled);
            this.gbMainOptions.Controls.Add(this.chkImageUploaderEnabled);
            this.gbMainOptions.Controls.Add(this.cboFileUploaders);
            this.gbMainOptions.Controls.Add(this.lblFileUploader);
            this.gbMainOptions.Controls.Add(this.cboURLShorteners);
            this.gbMainOptions.Controls.Add(this.lblURLShortener);
            this.gbMainOptions.Controls.Add(this.lblImageUploader);
            this.gbMainOptions.Controls.Add(this.lblTextUploader);
            this.gbMainOptions.Controls.Add(this.cboImageUploaders);
            this.gbMainOptions.Controls.Add(this.cboTextUploaders);
            this.gbMainOptions.Location = new System.Drawing.Point(0, 0);
            this.gbMainOptions.Name = "gbMainOptions";
            this.gbMainOptions.Size = new System.Drawing.Size(376, 128);
            this.gbMainOptions.TabIndex = 80;
            this.gbMainOptions.TabStop = false;
            this.gbMainOptions.Text = "Upload Destinations";
            // 
            // chkTextUploaderEnabled
            // 
            this.chkTextUploaderEnabled.AutoSize = true;
            this.chkTextUploaderEnabled.Checked = true;
            this.chkTextUploaderEnabled.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkTextUploaderEnabled.Location = new System.Drawing.Point(348, 50);
            this.chkTextUploaderEnabled.Name = "chkTextUploaderEnabled";
            this.chkTextUploaderEnabled.Size = new System.Drawing.Size(15, 14);
            this.chkTextUploaderEnabled.TabIndex = 128;
            this.chkTextUploaderEnabled.UseVisualStyleBackColor = true;
            this.chkTextUploaderEnabled.CheckedChanged += new System.EventHandler(this.cbTextUploaderUseFile_CheckedChanged);
            // 
            // chkImageUploaderEnabled
            // 
            this.chkImageUploaderEnabled.AutoSize = true;
            this.chkImageUploaderEnabled.Checked = true;
            this.chkImageUploaderEnabled.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkImageUploaderEnabled.Location = new System.Drawing.Point(348, 26);
            this.chkImageUploaderEnabled.Name = "chkImageUploaderEnabled";
            this.chkImageUploaderEnabled.Size = new System.Drawing.Size(15, 14);
            this.chkImageUploaderEnabled.TabIndex = 127;
            this.chkImageUploaderEnabled.UseVisualStyleBackColor = true;
            this.chkImageUploaderEnabled.CheckedChanged += new System.EventHandler(this.cbImageUploaderUseFile_CheckedChanged);
            // 
            // cboFileUploaders
            // 
            this.cboFileUploaders.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboFileUploaders.FormattingEnabled = true;
            this.cboFileUploaders.Location = new System.Drawing.Point(109, 70);
            this.cboFileUploaders.Name = "cboFileUploaders";
            this.cboFileUploaders.Size = new System.Drawing.Size(232, 21);
            this.cboFileUploaders.TabIndex = 126;
            // 
            // lblFileUploader
            // 
            this.lblFileUploader.AutoSize = true;
            this.lblFileUploader.Location = new System.Drawing.Point(29, 75);
            this.lblFileUploader.Name = "lblFileUploader";
            this.lblFileUploader.Size = new System.Drawing.Size(72, 13);
            this.lblFileUploader.TabIndex = 125;
            this.lblFileUploader.Text = "File Uploader:";
            // 
            // cboURLShorteners
            // 
            this.cboURLShorteners.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboURLShorteners.FormattingEnabled = true;
            this.cboURLShorteners.Location = new System.Drawing.Point(109, 94);
            this.cboURLShorteners.Name = "cboURLShorteners";
            this.cboURLShorteners.Size = new System.Drawing.Size(232, 21);
            this.cboURLShorteners.TabIndex = 124;
            // 
            // lblURLShortener
            // 
            this.lblURLShortener.AutoSize = true;
            this.lblURLShortener.Location = new System.Drawing.Point(20, 98);
            this.lblURLShortener.Name = "lblURLShortener";
            this.lblURLShortener.Size = new System.Drawing.Size(81, 13);
            this.lblURLShortener.TabIndex = 123;
            this.lblURLShortener.Text = "URL Shortener:";
            // 
            // lblImageUploader
            // 
            this.lblImageUploader.AutoSize = true;
            this.lblImageUploader.Location = new System.Drawing.Point(16, 25);
            this.lblImageUploader.Name = "lblImageUploader";
            this.lblImageUploader.Size = new System.Drawing.Size(85, 13);
            this.lblImageUploader.TabIndex = 1;
            this.lblImageUploader.Text = "Image Uploader:";
            // 
            // lblTextUploader
            // 
            this.lblTextUploader.AutoSize = true;
            this.lblTextUploader.Location = new System.Drawing.Point(24, 50);
            this.lblTextUploader.Name = "lblTextUploader";
            this.lblTextUploader.Size = new System.Drawing.Size(77, 13);
            this.lblTextUploader.TabIndex = 122;
            this.lblTextUploader.Text = "Text Uploader:";
            // 
            // cboImageUploaders
            // 
            this.cboImageUploaders.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboImageUploaders.FormattingEnabled = true;
            this.cboImageUploaders.Location = new System.Drawing.Point(109, 22);
            this.cboImageUploaders.Name = "cboImageUploaders";
            this.cboImageUploaders.Size = new System.Drawing.Size(232, 21);
            this.cboImageUploaders.TabIndex = 0;
            // 
            // cboTextUploaders
            // 
            this.cboTextUploaders.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTextUploaders.FormattingEnabled = true;
            this.cboTextUploaders.Location = new System.Drawing.Point(109, 46);
            this.cboTextUploaders.Name = "cboTextUploaders";
            this.cboTextUploaders.Size = new System.Drawing.Size(232, 21);
            this.cboTextUploaders.TabIndex = 121;
            // 
            // DestSelector
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gbMainOptions);
            this.MaximumSize = new System.Drawing.Size(378, 145);
            this.Name = "DestSelector";
            this.Size = new System.Drawing.Size(378, 129);
            this.gbMainOptions.ResumeLayout(false);
            this.gbMainOptions.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.GroupBox gbMainOptions;
        public System.Windows.Forms.ComboBox cboFileUploaders;
        private System.Windows.Forms.Label lblFileUploader;
        public System.Windows.Forms.ComboBox cboURLShorteners;
        private System.Windows.Forms.Label lblURLShortener;
        internal System.Windows.Forms.Label lblImageUploader;
        internal System.Windows.Forms.Label lblTextUploader;
        public System.Windows.Forms.ComboBox cboImageUploaders;
        public System.Windows.Forms.ComboBox cboTextUploaders;
        public System.Windows.Forms.CheckBox chkTextUploaderEnabled;
        public System.Windows.Forms.CheckBox chkImageUploaderEnabled;
    }
}

namespace ZScreenLib
{
    partial class ConfigWizard
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConfigWizard));
            this.lblScreenshotDestination = new System.Windows.Forms.Label();
            this.cboScreenshotDest = new System.Windows.Forms.ComboBox();
            this.gbRoot = new System.Windows.Forms.GroupBox();
            this.btnViewRootDir = new System.Windows.Forms.Button();
            this.btnBrowseRootDir = new System.Windows.Forms.Button();
            this.txtRootFolder = new System.Windows.Forms.TextBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.gbPublishOptions = new System.Windows.Forms.GroupBox();
            this.lblWatermarkType = new System.Windows.Forms.Label();
            this.cboWatermarkType = new System.Windows.Forms.ComboBox();
            this.txtWatermarkText = new System.Windows.Forms.TextBox();
            this.lblWatermarkText = new System.Windows.Forms.Label();
            this.txtWatermarkImageLocation = new System.Windows.Forms.TextBox();
            this.btwWatermarkBrowseImage = new System.Windows.Forms.Button();
            this.lblWatermarkImage = new System.Windows.Forms.Label();
            this.gbWatermark = new System.Windows.Forms.GroupBox();
            this.chkPreferSystemFolders = new System.Windows.Forms.CheckBox();
            this.gbRoot.SuspendLayout();
            this.gbPublishOptions.SuspendLayout();
            this.gbWatermark.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblScreenshotDestination
            // 
            this.lblScreenshotDestination.AutoSize = true;
            this.lblScreenshotDestination.Location = new System.Drawing.Point(16, 32);
            this.lblScreenshotDestination.Name = "lblScreenshotDestination";
            this.lblScreenshotDestination.Size = new System.Drawing.Size(95, 13);
            this.lblScreenshotDestination.TabIndex = 3;
            this.lblScreenshotDestination.Text = "Image Destination:";
            // 
            // cboScreenshotDest
            // 
            this.cboScreenshotDest.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboScreenshotDest.FormattingEnabled = true;
            this.cboScreenshotDest.Location = new System.Drawing.Point(120, 28);
            this.cboScreenshotDest.Name = "cboScreenshotDest";
            this.cboScreenshotDest.Size = new System.Drawing.Size(232, 21);
            this.cboScreenshotDest.TabIndex = 2;
            this.cboScreenshotDest.SelectedIndexChanged += new System.EventHandler(this.cboScreenshotDest_SelectedIndexChanged);
            // 
            // gbRoot
            // 
            this.gbRoot.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gbRoot.Controls.Add(this.btnViewRootDir);
            this.gbRoot.Controls.Add(this.btnBrowseRootDir);
            this.gbRoot.Controls.Add(this.txtRootFolder);
            this.gbRoot.Location = new System.Drawing.Point(8, 48);
            this.gbRoot.Name = "gbRoot";
            this.gbRoot.Size = new System.Drawing.Size(600, 64);
            this.gbRoot.TabIndex = 118;
            this.gbRoot.TabStop = false;
            this.gbRoot.Text = "Root folder for Settings and Data";
            // 
            // btnViewRootDir
            // 
            this.btnViewRootDir.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnViewRootDir.Location = new System.Drawing.Point(480, 24);
            this.btnViewRootDir.Name = "btnViewRootDir";
            this.btnViewRootDir.Size = new System.Drawing.Size(104, 24);
            this.btnViewRootDir.TabIndex = 116;
            this.btnViewRootDir.Text = "View Directory...";
            this.btnViewRootDir.UseVisualStyleBackColor = true;
            this.btnViewRootDir.Click += new System.EventHandler(this.btnViewRootDir_Click);
            // 
            // btnBrowseRootDir
            // 
            this.btnBrowseRootDir.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBrowseRootDir.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnBrowseRootDir.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnBrowseRootDir.Location = new System.Drawing.Point(392, 24);
            this.btnBrowseRootDir.Name = "btnBrowseRootDir";
            this.btnBrowseRootDir.Size = new System.Drawing.Size(80, 24);
            this.btnBrowseRootDir.TabIndex = 115;
            this.btnBrowseRootDir.Text = "Relocate...";
            this.btnBrowseRootDir.UseVisualStyleBackColor = true;
            this.btnBrowseRootDir.Click += new System.EventHandler(this.btnBrowseRootDir_Click);
            // 
            // txtRootFolder
            // 
            this.txtRootFolder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtRootFolder.Location = new System.Drawing.Point(16, 24);
            this.txtRootFolder.Name = "txtRootFolder";
            this.txtRootFolder.ReadOnly = true;
            this.txtRootFolder.Size = new System.Drawing.Size(368, 20);
            this.txtRootFolder.TabIndex = 114;
            this.txtRootFolder.Tag = "Path of the Root folder that holds Images, Text, Cache, Settings and Temp folders" +
                "";
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(520, 192);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 117;
            this.btnOK.Text = "&OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // gbPublishOptions
            // 
            this.gbPublishOptions.Controls.Add(this.lblScreenshotDestination);
            this.gbPublishOptions.Controls.Add(this.cboScreenshotDest);
            this.gbPublishOptions.Location = new System.Drawing.Point(8, 120);
            this.gbPublishOptions.Name = "gbPublishOptions";
            this.gbPublishOptions.Size = new System.Drawing.Size(600, 64);
            this.gbPublishOptions.TabIndex = 120;
            this.gbPublishOptions.TabStop = false;
            this.gbPublishOptions.Text = "Publish Options";
            // 
            // lblWatermarkType
            // 
            this.lblWatermarkType.AutoSize = true;
            this.lblWatermarkType.Location = new System.Drawing.Point(61, 32);
            this.lblWatermarkType.Name = "lblWatermarkType";
            this.lblWatermarkType.Size = new System.Drawing.Size(34, 13);
            this.lblWatermarkType.TabIndex = 34;
            this.lblWatermarkType.Text = "Type:";
            // 
            // cboWatermarkType
            // 
            this.cboWatermarkType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboWatermarkType.FormattingEnabled = true;
            this.cboWatermarkType.Location = new System.Drawing.Point(120, 24);
            this.cboWatermarkType.Name = "cboWatermarkType";
            this.cboWatermarkType.Size = new System.Drawing.Size(120, 21);
            this.cboWatermarkType.TabIndex = 35;
            // 
            // txtWatermarkText
            // 
            this.txtWatermarkText.Location = new System.Drawing.Point(120, 56);
            this.txtWatermarkText.Name = "txtWatermarkText";
            this.txtWatermarkText.Size = new System.Drawing.Size(248, 20);
            this.txtWatermarkText.TabIndex = 36;
            // 
            // lblWatermarkText
            // 
            this.lblWatermarkText.AutoSize = true;
            this.lblWatermarkText.Location = new System.Drawing.Point(64, 60);
            this.lblWatermarkText.Name = "lblWatermarkText";
            this.lblWatermarkText.Size = new System.Drawing.Size(31, 13);
            this.lblWatermarkText.TabIndex = 37;
            this.lblWatermarkText.Text = "Text:";
            // 
            // txtWatermarkImageLocation
            // 
            this.txtWatermarkImageLocation.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtWatermarkImageLocation.Location = new System.Drawing.Point(120, 88);
            this.txtWatermarkImageLocation.Name = "txtWatermarkImageLocation";
            this.txtWatermarkImageLocation.Size = new System.Drawing.Size(339, 20);
            this.txtWatermarkImageLocation.TabIndex = 38;
            // 
            // btwWatermarkBrowseImage
            // 
            this.btwWatermarkBrowseImage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btwWatermarkBrowseImage.Location = new System.Drawing.Point(464, 88);
            this.btwWatermarkBrowseImage.Name = "btwWatermarkBrowseImage";
            this.btwWatermarkBrowseImage.Size = new System.Drawing.Size(64, 24);
            this.btwWatermarkBrowseImage.TabIndex = 39;
            this.btwWatermarkBrowseImage.Tag = "Browse for a Watermark Image";
            this.btwWatermarkBrowseImage.Text = "Browse...";
            this.btwWatermarkBrowseImage.UseVisualStyleBackColor = true;
            // 
            // lblWatermarkImage
            // 
            this.lblWatermarkImage.AutoSize = true;
            this.lblWatermarkImage.Location = new System.Drawing.Point(56, 88);
            this.lblWatermarkImage.Name = "lblWatermarkImage";
            this.lblWatermarkImage.Size = new System.Drawing.Size(39, 13);
            this.lblWatermarkImage.TabIndex = 40;
            this.lblWatermarkImage.Text = "Image:";
            // 
            // gbWatermark
            // 
            this.gbWatermark.Controls.Add(this.lblWatermarkImage);
            this.gbWatermark.Controls.Add(this.btwWatermarkBrowseImage);
            this.gbWatermark.Controls.Add(this.txtWatermarkImageLocation);
            this.gbWatermark.Controls.Add(this.lblWatermarkText);
            this.gbWatermark.Controls.Add(this.txtWatermarkText);
            this.gbWatermark.Controls.Add(this.cboWatermarkType);
            this.gbWatermark.Controls.Add(this.lblWatermarkType);
            this.gbWatermark.Location = new System.Drawing.Point(8, 328);
            this.gbWatermark.Name = "gbWatermark";
            this.gbWatermark.Size = new System.Drawing.Size(600, 120);
            this.gbWatermark.TabIndex = 119;
            this.gbWatermark.TabStop = false;
            this.gbWatermark.Text = "Watermark Options";
            // 
            // chkPreferSystemFolders
            // 
            this.chkPreferSystemFolders.AutoSize = true;
            this.chkPreferSystemFolders.Location = new System.Drawing.Point(16, 16);
            this.chkPreferSystemFolders.Name = "chkPreferSystemFolders";
            this.chkPreferSystemFolders.Size = new System.Drawing.Size(231, 17);
            this.chkPreferSystemFolders.TabIndex = 121;
            this.chkPreferSystemFolders.Text = "&Prefer System Folders for Settings and Data";
            this.chkPreferSystemFolders.UseVisualStyleBackColor = true;
            this.chkPreferSystemFolders.CheckedChanged += new System.EventHandler(this.chkPreferSystemFolders_CheckedChanged);
            // 
            // ConfigWizard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(624, 222);
            this.Controls.Add(this.chkPreferSystemFolders);
            this.Controls.Add(this.gbPublishOptions);
            this.Controls.Add(this.gbWatermark);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.gbRoot);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "ConfigWizard";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ZScreen - Configuration Wizard";
            this.gbRoot.ResumeLayout(false);
            this.gbRoot.PerformLayout();
            this.gbPublishOptions.ResumeLayout(false);
            this.gbPublishOptions.PerformLayout();
            this.gbWatermark.ResumeLayout(false);
            this.gbWatermark.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblScreenshotDestination;
        private System.Windows.Forms.ComboBox cboScreenshotDest;
        private System.Windows.Forms.GroupBox gbRoot;
        private System.Windows.Forms.Button btnViewRootDir;
        private System.Windows.Forms.Button btnBrowseRootDir;
        private System.Windows.Forms.TextBox txtRootFolder;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.GroupBox gbPublishOptions;
        private System.Windows.Forms.Label lblWatermarkType;
        private System.Windows.Forms.ComboBox cboWatermarkType;
        private System.Windows.Forms.TextBox txtWatermarkText;
        private System.Windows.Forms.Label lblWatermarkText;
        private System.Windows.Forms.TextBox txtWatermarkImageLocation;
        private System.Windows.Forms.Button btwWatermarkBrowseImage;
        private System.Windows.Forms.Label lblWatermarkImage;
        private System.Windows.Forms.GroupBox gbWatermark;
        private System.Windows.Forms.CheckBox chkPreferSystemFolders;
    }
}
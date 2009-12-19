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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConfigWizard));
            this.lblScreenshotDestination = new System.Windows.Forms.Label();
            this.cboScreenshotDest = new System.Windows.Forms.ComboBox();
            this.gbRoot = new System.Windows.Forms.GroupBox();
            this.btnViewRootDir = new System.Windows.Forms.Button();
            this.btnBrowseRootDir = new System.Windows.Forms.Button();
            this.txtRootFolder = new System.Windows.Forms.TextBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.gbPublishOptions = new System.Windows.Forms.GroupBox();
            this.chkPreferSystemFolders = new System.Windows.Forms.CheckBox();
            this.ttApp = new System.Windows.Forms.ToolTip(this.components);
            this.btnExit = new System.Windows.Forms.Button();
            this.gbRoot.SuspendLayout();
            this.gbPublishOptions.SuspendLayout();
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
            this.gbRoot.Controls.Add(this.btnViewRootDir);
            this.gbRoot.Controls.Add(this.btnBrowseRootDir);
            this.gbRoot.Controls.Add(this.txtRootFolder);
            this.gbRoot.Location = new System.Drawing.Point(8, 33);
            this.gbRoot.Name = "gbRoot";
            this.gbRoot.Size = new System.Drawing.Size(608, 64);
            this.gbRoot.TabIndex = 118;
            this.gbRoot.TabStop = false;
            this.gbRoot.Text = "Root folder for Settings and Data";
            this.gbRoot.UseCompatibleTextRendering = true;
            // 
            // btnViewRootDir
            // 
            this.btnViewRootDir.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnViewRootDir.Location = new System.Drawing.Point(490, 24);
            this.btnViewRootDir.Name = "btnViewRootDir";
            this.btnViewRootDir.Size = new System.Drawing.Size(104, 24);
            this.btnViewRootDir.TabIndex = 116;
            this.btnViewRootDir.Text = "View Directory...";
            this.btnViewRootDir.UseCompatibleTextRendering = true;
            this.btnViewRootDir.UseVisualStyleBackColor = true;
            this.btnViewRootDir.Click += new System.EventHandler(this.btnViewRootDir_Click);
            // 
            // btnBrowseRootDir
            // 
            this.btnBrowseRootDir.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBrowseRootDir.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnBrowseRootDir.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnBrowseRootDir.Location = new System.Drawing.Point(402, 24);
            this.btnBrowseRootDir.Name = "btnBrowseRootDir";
            this.btnBrowseRootDir.Size = new System.Drawing.Size(80, 24);
            this.btnBrowseRootDir.TabIndex = 115;
            this.btnBrowseRootDir.Text = "Relocate...";
            this.btnBrowseRootDir.UseCompatibleTextRendering = true;
            this.btnBrowseRootDir.UseVisualStyleBackColor = true;
            this.btnBrowseRootDir.Click += new System.EventHandler(this.btnBrowseRootDir_Click);
            // 
            // txtRootFolder
            // 
            this.txtRootFolder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtRootFolder.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.txtRootFolder.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.FileSystemDirectories;
            this.txtRootFolder.Location = new System.Drawing.Point(16, 26);
            this.txtRootFolder.Name = "txtRootFolder";
            this.txtRootFolder.ReadOnly = true;
            this.txtRootFolder.Size = new System.Drawing.Size(376, 20);
            this.txtRootFolder.TabIndex = 114;
            this.txtRootFolder.Tag = "Path of the Root folder that holds Images, Text, Cache, Settings and Temp folders" +
                "";
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnOK.Location = new System.Drawing.Point(543, 191);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 28);
            this.btnOK.TabIndex = 117;
            this.btnOK.Text = "&OK";
            this.btnOK.UseCompatibleTextRendering = true;
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // gbPublishOptions
            // 
            this.gbPublishOptions.Controls.Add(this.lblScreenshotDestination);
            this.gbPublishOptions.Controls.Add(this.cboScreenshotDest);
            this.gbPublishOptions.Location = new System.Drawing.Point(8, 105);
            this.gbPublishOptions.Name = "gbPublishOptions";
            this.gbPublishOptions.Size = new System.Drawing.Size(608, 64);
            this.gbPublishOptions.TabIndex = 120;
            this.gbPublishOptions.TabStop = false;
            this.gbPublishOptions.Text = "Publish Options";
            this.gbPublishOptions.UseCompatibleTextRendering = true;
            // 
            // chkPreferSystemFolders
            // 
            this.chkPreferSystemFolders.AutoSize = true;
            this.chkPreferSystemFolders.Location = new System.Drawing.Point(8, 8);
            this.chkPreferSystemFolders.Name = "chkPreferSystemFolders";
            this.chkPreferSystemFolders.Size = new System.Drawing.Size(230, 17);
            this.chkPreferSystemFolders.TabIndex = 121;
            this.chkPreferSystemFolders.Text = "&Prefer Known Folders for Settings and Data";
            this.chkPreferSystemFolders.UseVisualStyleBackColor = true;
            this.chkPreferSystemFolders.CheckedChanged += new System.EventHandler(this.chkPreferSystemFolders_CheckedChanged);
            // 
            // ttApp
            // 
            this.ttApp.AutoPopDelay = 15000;
            this.ttApp.InitialDelay = 100;
            this.ttApp.ReshowDelay = 100;
            // 
            // btnExit
            // 
            this.btnExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnExit.Location = new System.Drawing.Point(462, 191);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(75, 28);
            this.btnExit.TabIndex = 122;
            this.btnExit.Text = "E&xit";
            this.btnExit.UseCompatibleTextRendering = true;
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // ConfigWizard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(625, 226);
            this.Controls.Add(this.chkPreferSystemFolders);
            this.Controls.Add(this.gbRoot);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.gbPublishOptions);
            this.Controls.Add(this.btnOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.GlassArea = new System.Windows.Forms.Padding(0, 0, 0, 45);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "ConfigWizard";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ZScreen - Configuration Wizard";
            this.Load += new System.EventHandler(this.ConfigWizard_Load);
            this.gbRoot.ResumeLayout(false);
            this.gbRoot.PerformLayout();
            this.gbPublishOptions.ResumeLayout(false);
            this.gbPublishOptions.PerformLayout();
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
        private System.Windows.Forms.CheckBox chkPreferSystemFolders;
        private System.Windows.Forms.ToolTip ttApp;
        private System.Windows.Forms.Button btnExit;
    }
}
namespace ZUploader
{
    partial class SettingsForm
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
            this.pgFTPSettings = new System.Windows.Forms.PropertyGrid();
            this.cbClipboardAutoCopy = new System.Windows.Forms.CheckBox();
            this.cbAutoPlaySound = new System.Windows.Forms.CheckBox();
            this.pgProxy = new System.Windows.Forms.PropertyGrid();
            this.tcSettings = new System.Windows.Forms.TabControl();
            this.tpGeneral = new System.Windows.Forms.TabPage();
            this.label1 = new System.Windows.Forms.Label();
            this.btnOpenZUploaderPath = new System.Windows.Forms.Button();
            this.lblGeneralInfo = new System.Windows.Forms.Label();
            this.cbShellContextMenu = new System.Windows.Forms.CheckBox();
            this.tpUpload = new System.Windows.Forms.TabPage();
            this.lblUploadLimitHint = new System.Windows.Forms.Label();
            this.nudUploadLimit = new System.Windows.Forms.NumericUpDown();
            this.lblUploadLimit = new System.Windows.Forms.Label();
            this.lblBufferSize = new System.Windows.Forms.Label();
            this.lblBufferSizeInfo = new System.Windows.Forms.Label();
            this.cbBufferSize = new System.Windows.Forms.ComboBox();
            this.tpImage = new System.Windows.Forms.TabPage();
            this.lblImageInfo = new System.Windows.Forms.Label();
            this.lblUseImageFormat2AfterHint = new System.Windows.Forms.Label();
            this.lblImageJPEGQualityHint = new System.Windows.Forms.Label();
            this.cbImageGIFQuality = new System.Windows.Forms.ComboBox();
            this.cbImageFormat2 = new System.Windows.Forms.ComboBox();
            this.lblImageFormat2 = new System.Windows.Forms.Label();
            this.lblUseImageFormat2After = new System.Windows.Forms.Label();
            this.nudUseImageFormat2After = new System.Windows.Forms.NumericUpDown();
            this.nudImageJPEGQuality = new System.Windows.Forms.NumericUpDown();
            this.lblImageGIFQuality = new System.Windows.Forms.Label();
            this.lblImageJPEGQuality = new System.Windows.Forms.Label();
            this.cbImageFormat = new System.Windows.Forms.ComboBox();
            this.lblImageFormat = new System.Windows.Forms.Label();
            this.tpClipboardUpload = new System.Windows.Forms.TabPage();
            this.lblClipboardUploadInfo = new System.Windows.Forms.Label();
            this.lblNameFormatPatternPreview = new System.Windows.Forms.Label();
            this.lblNameFormatPattern = new System.Windows.Forms.Label();
            this.btnNameFormatPatternHelp = new System.Windows.Forms.Button();
            this.txtNameFormatPattern = new System.Windows.Forms.TextBox();
            this.tpHistory = new System.Windows.Forms.TabPage();
            this.lblHistoryInfo = new System.Windows.Forms.Label();
            this.nudHistoryMaxItemCount = new System.Windows.Forms.NumericUpDown();
            this.lblHistoryMaxItemCount = new System.Windows.Forms.Label();
            this.btnBrowseCustomHistoryPath = new System.Windows.Forms.Button();
            this.txtCustomHistoryPath = new System.Windows.Forms.TextBox();
            this.cbUseCustomHistoryPath = new System.Windows.Forms.CheckBox();
            this.cbHistorySave = new System.Windows.Forms.CheckBox();
            this.tpFTP = new System.Windows.Forms.TabPage();
            this.tpProxy = new System.Windows.Forms.TabPage();
            this.tpDebug = new System.Windows.Forms.TabPage();
            this.txtDebugLog = new System.Windows.Forms.TextBox();
            this.tcSettings.SuspendLayout();
            this.tpGeneral.SuspendLayout();
            this.tpUpload.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudUploadLimit)).BeginInit();
            this.tpImage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudUseImageFormat2After)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudImageJPEGQuality)).BeginInit();
            this.tpClipboardUpload.SuspendLayout();
            this.tpHistory.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudHistoryMaxItemCount)).BeginInit();
            this.tpFTP.SuspendLayout();
            this.tpProxy.SuspendLayout();
            this.tpDebug.SuspendLayout();
            this.SuspendLayout();
            // 
            // pgFTPSettings
            // 
            this.pgFTPSettings.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pgFTPSettings.Location = new System.Drawing.Point(4, 4);
            this.pgFTPSettings.Name = "pgFTPSettings";
            this.pgFTPSettings.PropertySort = System.Windows.Forms.PropertySort.NoSort;
            this.pgFTPSettings.Size = new System.Drawing.Size(474, 251);
            this.pgFTPSettings.TabIndex = 0;
            this.pgFTPSettings.ToolbarVisible = false;
            this.pgFTPSettings.SelectedObjectsChanged += new System.EventHandler(this.pgFTPSettings_SelectedObjectsChanged);
            // 
            // cbClipboardAutoCopy
            // 
            this.cbClipboardAutoCopy.AutoSize = true;
            this.cbClipboardAutoCopy.Location = new System.Drawing.Point(16, 16);
            this.cbClipboardAutoCopy.Name = "cbClipboardAutoCopy";
            this.cbClipboardAutoCopy.Size = new System.Drawing.Size(254, 17);
            this.cbClipboardAutoCopy.TabIndex = 2;
            this.cbClipboardAutoCopy.Text = "Copy URL to clipboard after upload is completed";
            this.cbClipboardAutoCopy.UseVisualStyleBackColor = true;
            this.cbClipboardAutoCopy.CheckedChanged += new System.EventHandler(this.cbClipboardAutoCopy_CheckedChanged);
            // 
            // cbAutoPlaySound
            // 
            this.cbAutoPlaySound.AutoSize = true;
            this.cbAutoPlaySound.Location = new System.Drawing.Point(16, 40);
            this.cbAutoPlaySound.Name = "cbAutoPlaySound";
            this.cbAutoPlaySound.Size = new System.Drawing.Size(199, 17);
            this.cbAutoPlaySound.TabIndex = 3;
            this.cbAutoPlaySound.Text = "Play sound after upload is completed";
            this.cbAutoPlaySound.UseVisualStyleBackColor = true;
            this.cbAutoPlaySound.CheckedChanged += new System.EventHandler(this.cbAutoPlaySound_CheckedChanged);
            // 
            // pgProxy
            // 
            this.pgProxy.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pgProxy.Location = new System.Drawing.Point(4, 4);
            this.pgProxy.Name = "pgProxy";
            this.pgProxy.PropertySort = System.Windows.Forms.PropertySort.NoSort;
            this.pgProxy.Size = new System.Drawing.Size(474, 251);
            this.pgProxy.TabIndex = 1;
            this.pgProxy.ToolbarVisible = false;
            // 
            // tcSettings
            // 
            this.tcSettings.Controls.Add(this.tpGeneral);
            this.tcSettings.Controls.Add(this.tpUpload);
            this.tcSettings.Controls.Add(this.tpImage);
            this.tcSettings.Controls.Add(this.tpClipboardUpload);
            this.tcSettings.Controls.Add(this.tpHistory);
            this.tcSettings.Controls.Add(this.tpFTP);
            this.tcSettings.Controls.Add(this.tpProxy);
            this.tcSettings.Controls.Add(this.tpDebug);
            this.tcSettings.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcSettings.Location = new System.Drawing.Point(3, 3);
            this.tcSettings.Name = "tcSettings";
            this.tcSettings.SelectedIndex = 0;
            this.tcSettings.Size = new System.Drawing.Size(490, 285);
            this.tcSettings.TabIndex = 5;
            // 
            // tpGeneral
            // 
            this.tpGeneral.Controls.Add(this.label1);
            this.tpGeneral.Controls.Add(this.btnOpenZUploaderPath);
            this.tpGeneral.Controls.Add(this.lblGeneralInfo);
            this.tpGeneral.Controls.Add(this.cbShellContextMenu);
            this.tpGeneral.Controls.Add(this.cbClipboardAutoCopy);
            this.tpGeneral.Controls.Add(this.cbAutoPlaySound);
            this.tpGeneral.Location = new System.Drawing.Point(4, 22);
            this.tpGeneral.Name = "tpGeneral";
            this.tpGeneral.Padding = new System.Windows.Forms.Padding(3);
            this.tpGeneral.Size = new System.Drawing.Size(482, 259);
            this.tpGeneral.TabIndex = 0;
            this.tpGeneral.Text = "General";
            this.tpGeneral.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 128);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(264, 13);
            this.label1.TabIndex = 18;
            this.label1.Text = "This folder have settings, history database and log files";
            // 
            // btnOpenZUploaderPath
            // 
            this.btnOpenZUploaderPath.Location = new System.Drawing.Point(16, 96);
            this.btnOpenZUploaderPath.Name = "btnOpenZUploaderPath";
            this.btnOpenZUploaderPath.Size = new System.Drawing.Size(176, 23);
            this.btnOpenZUploaderPath.TabIndex = 17;
            this.btnOpenZUploaderPath.Text = "Open ZUploader personal folder";
            this.btnOpenZUploaderPath.UseVisualStyleBackColor = true;
            this.btnOpenZUploaderPath.Click += new System.EventHandler(this.btnOpenZUploaderPath_Click);
            // 
            // lblGeneralInfo
            // 
            this.lblGeneralInfo.BackColor = System.Drawing.Color.DimGray;
            this.lblGeneralInfo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblGeneralInfo.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblGeneralInfo.ForeColor = System.Drawing.Color.White;
            this.lblGeneralInfo.Location = new System.Drawing.Point(3, 227);
            this.lblGeneralInfo.Name = "lblGeneralInfo";
            this.lblGeneralInfo.Size = new System.Drawing.Size(476, 29);
            this.lblGeneralInfo.TabIndex = 16;
            this.lblGeneralInfo.Text = "Shell context menu is Windows Explorer right click menu for files and folders.";
            this.lblGeneralInfo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cbShellContextMenu
            // 
            this.cbShellContextMenu.AutoSize = true;
            this.cbShellContextMenu.Location = new System.Drawing.Point(16, 64);
            this.cbShellContextMenu.Name = "cbShellContextMenu";
            this.cbShellContextMenu.Size = new System.Drawing.Size(285, 17);
            this.cbShellContextMenu.TabIndex = 4;
            this.cbShellContextMenu.Text = "Show \"Upload using ZUploader\" in Shell context menu";
            this.cbShellContextMenu.UseVisualStyleBackColor = true;
            this.cbShellContextMenu.CheckedChanged += new System.EventHandler(this.cbShellContextMenu_CheckedChanged);
            // 
            // tpUpload
            // 
            this.tpUpload.Controls.Add(this.lblUploadLimitHint);
            this.tpUpload.Controls.Add(this.nudUploadLimit);
            this.tpUpload.Controls.Add(this.lblUploadLimit);
            this.tpUpload.Controls.Add(this.lblBufferSize);
            this.tpUpload.Controls.Add(this.lblBufferSizeInfo);
            this.tpUpload.Controls.Add(this.cbBufferSize);
            this.tpUpload.Location = new System.Drawing.Point(4, 22);
            this.tpUpload.Name = "tpUpload";
            this.tpUpload.Size = new System.Drawing.Size(482, 259);
            this.tpUpload.TabIndex = 7;
            this.tpUpload.Text = "Upload";
            this.tpUpload.UseVisualStyleBackColor = true;
            // 
            // lblUploadLimitHint
            // 
            this.lblUploadLimitHint.AutoSize = true;
            this.lblUploadLimitHint.Location = new System.Drawing.Point(216, 16);
            this.lblUploadLimitHint.Name = "lblUploadLimitHint";
            this.lblUploadLimitHint.Size = new System.Drawing.Size(90, 13);
            this.lblUploadLimitHint.TabIndex = 5;
            this.lblUploadLimitHint.Text = "0 - 25 (0 disables)";
            // 
            // nudUploadLimit
            // 
            this.nudUploadLimit.Location = new System.Drawing.Point(152, 12);
            this.nudUploadLimit.Maximum = new decimal(new int[] {
            25,
            0,
            0,
            0});
            this.nudUploadLimit.Name = "nudUploadLimit";
            this.nudUploadLimit.Size = new System.Drawing.Size(56, 20);
            this.nudUploadLimit.TabIndex = 4;
            this.nudUploadLimit.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.nudUploadLimit.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.nudUploadLimit.ValueChanged += new System.EventHandler(this.nudUploadLimit_ValueChanged);
            // 
            // lblUploadLimit
            // 
            this.lblUploadLimit.AutoSize = true;
            this.lblUploadLimit.Location = new System.Drawing.Point(16, 16);
            this.lblUploadLimit.Name = "lblUploadLimit";
            this.lblUploadLimit.Size = new System.Drawing.Size(128, 13);
            this.lblUploadLimit.TabIndex = 3;
            this.lblUploadLimit.Text = "Simultaneous upload limit:";
            // 
            // lblBufferSize
            // 
            this.lblBufferSize.AutoSize = true;
            this.lblBufferSize.Location = new System.Drawing.Point(16, 48);
            this.lblBufferSize.Name = "lblBufferSize";
            this.lblBufferSize.Size = new System.Drawing.Size(59, 13);
            this.lblBufferSize.TabIndex = 2;
            this.lblBufferSize.Text = "Buffer size:";
            // 
            // lblBufferSizeInfo
            // 
            this.lblBufferSizeInfo.AutoSize = true;
            this.lblBufferSizeInfo.Location = new System.Drawing.Point(152, 48);
            this.lblBufferSizeInfo.Name = "lblBufferSizeInfo";
            this.lblBufferSizeInfo.Size = new System.Drawing.Size(23, 13);
            this.lblBufferSizeInfo.TabIndex = 1;
            this.lblBufferSizeInfo.Text = "KiB";
            // 
            // cbBufferSize
            // 
            this.cbBufferSize.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbBufferSize.FormattingEnabled = true;
            this.cbBufferSize.Location = new System.Drawing.Point(80, 44);
            this.cbBufferSize.Name = "cbBufferSize";
            this.cbBufferSize.Size = new System.Drawing.Size(64, 21);
            this.cbBufferSize.TabIndex = 0;
            this.cbBufferSize.SelectedIndexChanged += new System.EventHandler(this.cbBufferSize_SelectedIndexChanged);
            // 
            // tpImage
            // 
            this.tpImage.Controls.Add(this.lblImageInfo);
            this.tpImage.Controls.Add(this.lblUseImageFormat2AfterHint);
            this.tpImage.Controls.Add(this.lblImageJPEGQualityHint);
            this.tpImage.Controls.Add(this.cbImageGIFQuality);
            this.tpImage.Controls.Add(this.cbImageFormat2);
            this.tpImage.Controls.Add(this.lblImageFormat2);
            this.tpImage.Controls.Add(this.lblUseImageFormat2After);
            this.tpImage.Controls.Add(this.nudUseImageFormat2After);
            this.tpImage.Controls.Add(this.nudImageJPEGQuality);
            this.tpImage.Controls.Add(this.lblImageGIFQuality);
            this.tpImage.Controls.Add(this.lblImageJPEGQuality);
            this.tpImage.Controls.Add(this.cbImageFormat);
            this.tpImage.Controls.Add(this.lblImageFormat);
            this.tpImage.Location = new System.Drawing.Point(4, 22);
            this.tpImage.Name = "tpImage";
            this.tpImage.Padding = new System.Windows.Forms.Padding(3);
            this.tpImage.Size = new System.Drawing.Size(482, 259);
            this.tpImage.TabIndex = 4;
            this.tpImage.Text = "Image";
            this.tpImage.UseVisualStyleBackColor = true;
            // 
            // lblImageInfo
            // 
            this.lblImageInfo.BackColor = System.Drawing.Color.DimGray;
            this.lblImageInfo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblImageInfo.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblImageInfo.ForeColor = System.Drawing.Color.White;
            this.lblImageInfo.Location = new System.Drawing.Point(3, 227);
            this.lblImageInfo.Name = "lblImageInfo";
            this.lblImageInfo.Size = new System.Drawing.Size(476, 29);
            this.lblImageInfo.TabIndex = 14;
            this.lblImageInfo.Text = "These settings are for clipboard upload. Images that are stored in clipboard are " +
                "added as bitmap.";
            this.lblImageInfo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblUseImageFormat2AfterHint
            // 
            this.lblUseImageFormat2AfterHint.AutoSize = true;
            this.lblUseImageFormat2AfterHint.Location = new System.Drawing.Point(288, 112);
            this.lblUseImageFormat2AfterHint.Name = "lblUseImageFormat2AfterHint";
            this.lblUseImageFormat2AfterHint.Size = new System.Drawing.Size(124, 13);
            this.lblUseImageFormat2AfterHint.TabIndex = 13;
            this.lblUseImageFormat2AfterHint.Text = "KiB  0 - 5000 (0 disables)";
            // 
            // lblImageJPEGQualityHint
            // 
            this.lblImageJPEGQualityHint.AutoSize = true;
            this.lblImageJPEGQualityHint.Location = new System.Drawing.Point(168, 48);
            this.lblImageJPEGQualityHint.Name = "lblImageJPEGQualityHint";
            this.lblImageJPEGQualityHint.Size = new System.Drawing.Size(40, 13);
            this.lblImageJPEGQualityHint.TabIndex = 12;
            this.lblImageJPEGQualityHint.Text = "0 - 100";
            // 
            // cbImageGIFQuality
            // 
            this.cbImageGIFQuality.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbImageGIFQuality.FormattingEnabled = true;
            this.cbImageGIFQuality.Items.AddRange(new object[] {
            "Default (Fast)",
            "256 colors (8 bit)",
            "16 colors (4 bit)",
            "Grayscale"});
            this.cbImageGIFQuality.Location = new System.Drawing.Point(104, 76);
            this.cbImageGIFQuality.Name = "cbImageGIFQuality";
            this.cbImageGIFQuality.Size = new System.Drawing.Size(120, 21);
            this.cbImageGIFQuality.TabIndex = 10;
            this.cbImageGIFQuality.SelectedIndexChanged += new System.EventHandler(this.cbImageGIFQuality_SelectedIndexChanged);
            // 
            // cbImageFormat2
            // 
            this.cbImageFormat2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbImageFormat2.FormattingEnabled = true;
            this.cbImageFormat2.Items.AddRange(new object[] {
            "PNG",
            "JPEG",
            "GIF",
            "BMP",
            "TIFF"});
            this.cbImageFormat2.Location = new System.Drawing.Point(104, 140);
            this.cbImageFormat2.Name = "cbImageFormat2";
            this.cbImageFormat2.Size = new System.Drawing.Size(56, 21);
            this.cbImageFormat2.TabIndex = 9;
            this.cbImageFormat2.SelectedIndexChanged += new System.EventHandler(this.cbImageFormat2_SelectedIndexChanged);
            // 
            // lblImageFormat2
            // 
            this.lblImageFormat2.AutoSize = true;
            this.lblImageFormat2.Location = new System.Drawing.Point(16, 144);
            this.lblImageFormat2.Name = "lblImageFormat2";
            this.lblImageFormat2.Size = new System.Drawing.Size(80, 13);
            this.lblImageFormat2.TabIndex = 8;
            this.lblImageFormat2.Text = "Image format 2:";
            // 
            // lblUseImageFormat2After
            // 
            this.lblUseImageFormat2After.AutoSize = true;
            this.lblUseImageFormat2After.Location = new System.Drawing.Point(16, 112);
            this.lblUseImageFormat2After.Name = "lblUseImageFormat2After";
            this.lblUseImageFormat2After.Size = new System.Drawing.Size(198, 13);
            this.lblUseImageFormat2After.TabIndex = 7;
            this.lblUseImageFormat2After.Text = "Image size limit for use \"Image format 2\":";
            // 
            // nudUseImageFormat2After
            // 
            this.nudUseImageFormat2After.Location = new System.Drawing.Point(224, 108);
            this.nudUseImageFormat2After.Maximum = new decimal(new int[] {
            5000,
            0,
            0,
            0});
            this.nudUseImageFormat2After.Name = "nudUseImageFormat2After";
            this.nudUseImageFormat2After.Size = new System.Drawing.Size(56, 20);
            this.nudUseImageFormat2After.TabIndex = 6;
            this.nudUseImageFormat2After.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.nudUseImageFormat2After.Value = new decimal(new int[] {
            5000,
            0,
            0,
            0});
            this.nudUseImageFormat2After.ValueChanged += new System.EventHandler(this.nudUseImageFormat2After_ValueChanged);
            // 
            // nudImageJPEGQuality
            // 
            this.nudImageJPEGQuality.Location = new System.Drawing.Point(104, 44);
            this.nudImageJPEGQuality.Name = "nudImageJPEGQuality";
            this.nudImageJPEGQuality.Size = new System.Drawing.Size(56, 20);
            this.nudImageJPEGQuality.TabIndex = 4;
            this.nudImageJPEGQuality.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.nudImageJPEGQuality.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.nudImageJPEGQuality.ValueChanged += new System.EventHandler(this.nudImageJPEGQuality_ValueChanged);
            // 
            // lblImageGIFQuality
            // 
            this.lblImageGIFQuality.AutoSize = true;
            this.lblImageGIFQuality.Location = new System.Drawing.Point(16, 80);
            this.lblImageGIFQuality.Name = "lblImageGIFQuality";
            this.lblImageGIFQuality.Size = new System.Drawing.Size(60, 13);
            this.lblImageGIFQuality.TabIndex = 3;
            this.lblImageGIFQuality.Text = "GIF quality:";
            // 
            // lblImageJPEGQuality
            // 
            this.lblImageJPEGQuality.AutoSize = true;
            this.lblImageJPEGQuality.Location = new System.Drawing.Point(16, 48);
            this.lblImageJPEGQuality.Name = "lblImageJPEGQuality";
            this.lblImageJPEGQuality.Size = new System.Drawing.Size(70, 13);
            this.lblImageJPEGQuality.TabIndex = 2;
            this.lblImageJPEGQuality.Text = "JPEG quality:";
            // 
            // cbImageFormat
            // 
            this.cbImageFormat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbImageFormat.FormattingEnabled = true;
            this.cbImageFormat.Items.AddRange(new object[] {
            "PNG",
            "JPEG",
            "GIF",
            "BMP",
            "TIFF"});
            this.cbImageFormat.Location = new System.Drawing.Point(104, 12);
            this.cbImageFormat.Name = "cbImageFormat";
            this.cbImageFormat.Size = new System.Drawing.Size(56, 21);
            this.cbImageFormat.TabIndex = 1;
            this.cbImageFormat.SelectedIndexChanged += new System.EventHandler(this.cbImageFormat_SelectedIndexChanged);
            // 
            // lblImageFormat
            // 
            this.lblImageFormat.AutoSize = true;
            this.lblImageFormat.Location = new System.Drawing.Point(16, 16);
            this.lblImageFormat.Name = "lblImageFormat";
            this.lblImageFormat.Size = new System.Drawing.Size(71, 13);
            this.lblImageFormat.TabIndex = 0;
            this.lblImageFormat.Text = "Image format:";
            // 
            // tpClipboardUpload
            // 
            this.tpClipboardUpload.Controls.Add(this.lblClipboardUploadInfo);
            this.tpClipboardUpload.Controls.Add(this.lblNameFormatPatternPreview);
            this.tpClipboardUpload.Controls.Add(this.lblNameFormatPattern);
            this.tpClipboardUpload.Controls.Add(this.btnNameFormatPatternHelp);
            this.tpClipboardUpload.Controls.Add(this.txtNameFormatPattern);
            this.tpClipboardUpload.Location = new System.Drawing.Point(4, 22);
            this.tpClipboardUpload.Name = "tpClipboardUpload";
            this.tpClipboardUpload.Padding = new System.Windows.Forms.Padding(3);
            this.tpClipboardUpload.Size = new System.Drawing.Size(482, 259);
            this.tpClipboardUpload.TabIndex = 6;
            this.tpClipboardUpload.Text = "Clipboard upload";
            this.tpClipboardUpload.UseVisualStyleBackColor = true;
            // 
            // lblClipboardUploadInfo
            // 
            this.lblClipboardUploadInfo.BackColor = System.Drawing.Color.DimGray;
            this.lblClipboardUploadInfo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblClipboardUploadInfo.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblClipboardUploadInfo.ForeColor = System.Drawing.Color.White;
            this.lblClipboardUploadInfo.Location = new System.Drawing.Point(3, 227);
            this.lblClipboardUploadInfo.Name = "lblClipboardUploadInfo";
            this.lblClipboardUploadInfo.Size = new System.Drawing.Size(476, 29);
            this.lblClipboardUploadInfo.TabIndex = 19;
            this.lblClipboardUploadInfo.Text = "Clipboard upload automatically detects the data type and selects the upload servi" +
                "ce accordingly.";
            this.lblClipboardUploadInfo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblNameFormatPatternPreview
            // 
            this.lblNameFormatPatternPreview.AutoSize = true;
            this.lblNameFormatPatternPreview.Location = new System.Drawing.Point(16, 72);
            this.lblNameFormatPatternPreview.Name = "lblNameFormatPatternPreview";
            this.lblNameFormatPatternPreview.Size = new System.Drawing.Size(48, 13);
            this.lblNameFormatPatternPreview.TabIndex = 18;
            this.lblNameFormatPatternPreview.Text = "Preview:";
            // 
            // lblNameFormatPattern
            // 
            this.lblNameFormatPattern.AutoSize = true;
            this.lblNameFormatPattern.Location = new System.Drawing.Point(16, 16);
            this.lblNameFormatPattern.Name = "lblNameFormatPattern";
            this.lblNameFormatPattern.Size = new System.Drawing.Size(362, 13);
            this.lblNameFormatPattern.TabIndex = 15;
            this.lblNameFormatPattern.Text = "Clipboard upload name pattern for image and text (Not image file or text file):";
            // 
            // btnNameFormatPatternHelp
            // 
            this.btnNameFormatPatternHelp.Location = new System.Drawing.Point(440, 39);
            this.btnNameFormatPatternHelp.Name = "btnNameFormatPatternHelp";
            this.btnNameFormatPatternHelp.Size = new System.Drawing.Size(24, 23);
            this.btnNameFormatPatternHelp.TabIndex = 17;
            this.btnNameFormatPatternHelp.Text = "?";
            this.btnNameFormatPatternHelp.UseVisualStyleBackColor = true;
            this.btnNameFormatPatternHelp.Click += new System.EventHandler(this.btnNameFormatPatternHelp_Click);
            // 
            // txtNameFormatPattern
            // 
            this.txtNameFormatPattern.Location = new System.Drawing.Point(16, 40);
            this.txtNameFormatPattern.Name = "txtNameFormatPattern";
            this.txtNameFormatPattern.Size = new System.Drawing.Size(416, 20);
            this.txtNameFormatPattern.TabIndex = 16;
            this.txtNameFormatPattern.TextChanged += new System.EventHandler(this.txtNameFormatPattern_TextChanged);
            // 
            // tpHistory
            // 
            this.tpHistory.Controls.Add(this.lblHistoryInfo);
            this.tpHistory.Controls.Add(this.nudHistoryMaxItemCount);
            this.tpHistory.Controls.Add(this.lblHistoryMaxItemCount);
            this.tpHistory.Controls.Add(this.btnBrowseCustomHistoryPath);
            this.tpHistory.Controls.Add(this.txtCustomHistoryPath);
            this.tpHistory.Controls.Add(this.cbUseCustomHistoryPath);
            this.tpHistory.Controls.Add(this.cbHistorySave);
            this.tpHistory.Location = new System.Drawing.Point(4, 22);
            this.tpHistory.Name = "tpHistory";
            this.tpHistory.Padding = new System.Windows.Forms.Padding(3);
            this.tpHistory.Size = new System.Drawing.Size(482, 259);
            this.tpHistory.TabIndex = 3;
            this.tpHistory.Text = "History";
            this.tpHistory.UseVisualStyleBackColor = true;
            // 
            // lblHistoryInfo
            // 
            this.lblHistoryInfo.BackColor = System.Drawing.Color.DimGray;
            this.lblHistoryInfo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblHistoryInfo.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblHistoryInfo.ForeColor = System.Drawing.Color.White;
            this.lblHistoryInfo.Location = new System.Drawing.Point(3, 227);
            this.lblHistoryInfo.Name = "lblHistoryInfo";
            this.lblHistoryInfo.Size = new System.Drawing.Size(476, 29);
            this.lblHistoryInfo.TabIndex = 15;
            this.lblHistoryInfo.Text = "ZUploader uses SQLite to store history items.";
            this.lblHistoryInfo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // nudHistoryMaxItemCount
            // 
            this.nudHistoryMaxItemCount.Location = new System.Drawing.Point(216, 92);
            this.nudHistoryMaxItemCount.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.nudHistoryMaxItemCount.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
            this.nudHistoryMaxItemCount.Name = "nudHistoryMaxItemCount";
            this.nudHistoryMaxItemCount.Size = new System.Drawing.Size(80, 20);
            this.nudHistoryMaxItemCount.TabIndex = 11;
            this.nudHistoryMaxItemCount.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.nudHistoryMaxItemCount.Value = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
            this.nudHistoryMaxItemCount.ValueChanged += new System.EventHandler(this.nudHistoryMaxItemCount_ValueChanged);
            // 
            // lblHistoryMaxItemCount
            // 
            this.lblHistoryMaxItemCount.AutoSize = true;
            this.lblHistoryMaxItemCount.Location = new System.Drawing.Point(16, 96);
            this.lblHistoryMaxItemCount.Name = "lblHistoryMaxItemCount";
            this.lblHistoryMaxItemCount.Size = new System.Drawing.Size(192, 13);
            this.lblHistoryMaxItemCount.TabIndex = 10;
            this.lblHistoryMaxItemCount.Text = "Max item count for filtering (-1 disables):";
            // 
            // btnBrowseCustomHistoryPath
            // 
            this.btnBrowseCustomHistoryPath.Location = new System.Drawing.Point(392, 62);
            this.btnBrowseCustomHistoryPath.Name = "btnBrowseCustomHistoryPath";
            this.btnBrowseCustomHistoryPath.Size = new System.Drawing.Size(75, 23);
            this.btnBrowseCustomHistoryPath.TabIndex = 9;
            this.btnBrowseCustomHistoryPath.Text = "Browse...";
            this.btnBrowseCustomHistoryPath.UseVisualStyleBackColor = true;
            this.btnBrowseCustomHistoryPath.Click += new System.EventHandler(this.btnBrowseCustomHistoryPath_Click);
            // 
            // txtCustomHistoryPath
            // 
            this.txtCustomHistoryPath.Location = new System.Drawing.Point(16, 64);
            this.txtCustomHistoryPath.Name = "txtCustomHistoryPath";
            this.txtCustomHistoryPath.Size = new System.Drawing.Size(368, 20);
            this.txtCustomHistoryPath.TabIndex = 8;
            this.txtCustomHistoryPath.TextChanged += new System.EventHandler(this.txtCustomHistoryPath_TextChanged);
            // 
            // cbUseCustomHistoryPath
            // 
            this.cbUseCustomHistoryPath.AutoSize = true;
            this.cbUseCustomHistoryPath.Location = new System.Drawing.Point(16, 40);
            this.cbUseCustomHistoryPath.Name = "cbUseCustomHistoryPath";
            this.cbUseCustomHistoryPath.Size = new System.Drawing.Size(158, 17);
            this.cbUseCustomHistoryPath.TabIndex = 6;
            this.cbUseCustomHistoryPath.Text = "Use custom history file path:";
            this.cbUseCustomHistoryPath.UseVisualStyleBackColor = true;
            this.cbUseCustomHistoryPath.CheckedChanged += new System.EventHandler(this.cbUseCustomHistoryPath_CheckedChanged);
            // 
            // cbHistorySave
            // 
            this.cbHistorySave.AutoSize = true;
            this.cbHistorySave.Location = new System.Drawing.Point(16, 16);
            this.cbHistorySave.Name = "cbHistorySave";
            this.cbHistorySave.Size = new System.Drawing.Size(118, 17);
            this.cbHistorySave.TabIndex = 5;
            this.cbHistorySave.Text = "Enable history save";
            this.cbHistorySave.UseVisualStyleBackColor = true;
            this.cbHistorySave.CheckedChanged += new System.EventHandler(this.cbHistorySave_CheckedChanged);
            // 
            // tpFTP
            // 
            this.tpFTP.Controls.Add(this.pgFTPSettings);
            this.tpFTP.Location = new System.Drawing.Point(4, 22);
            this.tpFTP.Name = "tpFTP";
            this.tpFTP.Padding = new System.Windows.Forms.Padding(4);
            this.tpFTP.Size = new System.Drawing.Size(482, 259);
            this.tpFTP.TabIndex = 1;
            this.tpFTP.Text = "FTP";
            this.tpFTP.UseVisualStyleBackColor = true;
            // 
            // tpProxy
            // 
            this.tpProxy.Controls.Add(this.pgProxy);
            this.tpProxy.Location = new System.Drawing.Point(4, 22);
            this.tpProxy.Name = "tpProxy";
            this.tpProxy.Padding = new System.Windows.Forms.Padding(4);
            this.tpProxy.Size = new System.Drawing.Size(482, 259);
            this.tpProxy.TabIndex = 2;
            this.tpProxy.Text = "Proxy";
            this.tpProxy.UseVisualStyleBackColor = true;
            // 
            // tpDebug
            // 
            this.tpDebug.Controls.Add(this.txtDebugLog);
            this.tpDebug.Location = new System.Drawing.Point(4, 22);
            this.tpDebug.Name = "tpDebug";
            this.tpDebug.Padding = new System.Windows.Forms.Padding(3);
            this.tpDebug.Size = new System.Drawing.Size(482, 259);
            this.tpDebug.TabIndex = 5;
            this.tpDebug.Text = "Debug";
            this.tpDebug.UseVisualStyleBackColor = true;
            // 
            // txtDebugLog
            // 
            this.txtDebugLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtDebugLog.Location = new System.Drawing.Point(3, 3);
            this.txtDebugLog.Multiline = true;
            this.txtDebugLog.Name = "txtDebugLog";
            this.txtDebugLog.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtDebugLog.Size = new System.Drawing.Size(476, 253);
            this.txtDebugLog.TabIndex = 0;
            this.txtDebugLog.WordWrap = false;
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(496, 291);
            this.Controls.Add(this.tcSettings);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "SettingsForm";
            this.Padding = new System.Windows.Forms.Padding(3);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ZUploader - Settings";
            this.Shown += new System.EventHandler(this.SettingsForm_Shown);
            this.Resize += new System.EventHandler(this.SettingsForm_Resize);
            this.tcSettings.ResumeLayout(false);
            this.tpGeneral.ResumeLayout(false);
            this.tpGeneral.PerformLayout();
            this.tpUpload.ResumeLayout(false);
            this.tpUpload.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudUploadLimit)).EndInit();
            this.tpImage.ResumeLayout(false);
            this.tpImage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudUseImageFormat2After)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudImageJPEGQuality)).EndInit();
            this.tpClipboardUpload.ResumeLayout(false);
            this.tpClipboardUpload.PerformLayout();
            this.tpHistory.ResumeLayout(false);
            this.tpHistory.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudHistoryMaxItemCount)).EndInit();
            this.tpFTP.ResumeLayout(false);
            this.tpProxy.ResumeLayout(false);
            this.tpDebug.ResumeLayout(false);
            this.tpDebug.PerformLayout();
            this.ResumeLayout(false);

        }

        private System.Windows.Forms.PropertyGrid pgProxy;

        #endregion Windows Form Designer generated code

        private System.Windows.Forms.PropertyGrid pgFTPSettings;
        private System.Windows.Forms.CheckBox cbClipboardAutoCopy;
        private System.Windows.Forms.CheckBox cbAutoPlaySound;
        private System.Windows.Forms.TabControl tcSettings;
        private System.Windows.Forms.TabPage tpGeneral;
        private System.Windows.Forms.TabPage tpFTP;
        private System.Windows.Forms.TabPage tpProxy;
        private System.Windows.Forms.CheckBox cbShellContextMenu;
        private System.Windows.Forms.TabPage tpHistory;
        private System.Windows.Forms.TextBox txtCustomHistoryPath;
        private System.Windows.Forms.CheckBox cbUseCustomHistoryPath;
        private System.Windows.Forms.CheckBox cbHistorySave;
        private System.Windows.Forms.Button btnBrowseCustomHistoryPath;
        private System.Windows.Forms.TabPage tpImage;
        private System.Windows.Forms.ComboBox cbImageFormat;
        private System.Windows.Forms.Label lblImageFormat;
        private System.Windows.Forms.ComboBox cbImageGIFQuality;
        private System.Windows.Forms.ComboBox cbImageFormat2;
        private System.Windows.Forms.Label lblImageFormat2;
        private System.Windows.Forms.Label lblUseImageFormat2After;
        private System.Windows.Forms.NumericUpDown nudUseImageFormat2After;
        private System.Windows.Forms.NumericUpDown nudImageJPEGQuality;
        private System.Windows.Forms.Label lblImageGIFQuality;
        private System.Windows.Forms.Label lblImageJPEGQuality;
        private System.Windows.Forms.Label lblUseImageFormat2AfterHint;
        private System.Windows.Forms.Label lblImageJPEGQualityHint;
        private System.Windows.Forms.Label lblHistoryMaxItemCount;
        private System.Windows.Forms.NumericUpDown nudHistoryMaxItemCount;
        private System.Windows.Forms.Label lblImageInfo;
        private System.Windows.Forms.Label lblHistoryInfo;
        private System.Windows.Forms.Label lblGeneralInfo;
        private System.Windows.Forms.TabPage tpDebug;
        private System.Windows.Forms.TextBox txtDebugLog;
        private System.Windows.Forms.Button btnNameFormatPatternHelp;
        private System.Windows.Forms.TextBox txtNameFormatPattern;
        private System.Windows.Forms.Label lblNameFormatPattern;
        private System.Windows.Forms.Label lblNameFormatPatternPreview;
        private System.Windows.Forms.TabPage tpClipboardUpload;
        private System.Windows.Forms.Label lblClipboardUploadInfo;
        private System.Windows.Forms.TabPage tpUpload;
        private System.Windows.Forms.ComboBox cbBufferSize;
        private System.Windows.Forms.Label lblBufferSize;
        private System.Windows.Forms.Label lblBufferSizeInfo;
        private System.Windows.Forms.Button btnOpenZUploaderPath;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblUploadLimitHint;
        private System.Windows.Forms.NumericUpDown nudUploadLimit;
        private System.Windows.Forms.Label lblUploadLimit;
    }
}
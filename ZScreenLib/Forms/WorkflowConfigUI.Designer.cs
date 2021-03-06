﻿namespace ZScreenLib
{
    partial class WorkflowWizard
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
        protected virtual void InitializeComponent()
        {
            this.tcMain = new System.Windows.Forms.TabControl();
            this.tpJob = new System.Windows.Forms.TabPage();
            this.gbTask = new System.Windows.Forms.GroupBox();
            this.cboTask = new System.Windows.Forms.ComboBox();
            this.gbName = new System.Windows.Forms.GroupBox();
            this.txtName = new System.Windows.Forms.TextBox();
            this.chkUseHotkey = new System.Windows.Forms.CheckBox();
            this.hmcHotkeys = new HelpersLib.Hotkey.HotkeyManagerControl();
            this.tpImagePreview = new System.Windows.Forms.TabPage();
            this.pbImage = new HelpersLib.MyPictureBox();
            this.tpImageQuality = new System.Windows.Forms.TabPage();
            this.gbPictureQuality = new System.Windows.Forms.GroupBox();
            this.tcQuality = new System.Windows.Forms.TabControl();
            this.tpQualityPng = new System.Windows.Forms.TabPage();
            this.chkPngQualityInterlaced = new System.Windows.Forms.CheckBox();
            this.cboPngQuality = new System.Windows.Forms.ComboBox();
            this.tpQualityJpeg = new System.Windows.Forms.TabPage();
            this.lblQuality = new System.Windows.Forms.Label();
            this.cboJpgSubSampling = new System.Windows.Forms.ComboBox();
            this.cboJpgQuality = new System.Windows.Forms.ComboBox();
            this.tpQualityGif = new System.Windows.Forms.TabPage();
            this.lblGIFQuality = new System.Windows.Forms.Label();
            this.cboGIFQuality = new System.Windows.Forms.ComboBox();
            this.tpQualityTiff = new System.Windows.Forms.TabPage();
            this.cboTiffQuality = new System.Windows.Forms.ComboBox();
            this.nudSwitchAfter = new System.Windows.Forms.NumericUpDown();
            this.cboSwitchFormat = new System.Windows.Forms.ComboBox();
            this.lblFileFormat = new System.Windows.Forms.Label();
            this.cboFileFormat = new System.Windows.Forms.ComboBox();
            this.lblKB = new System.Windows.Forms.Label();
            this.lblAfter = new System.Windows.Forms.Label();
            this.lblSwitchTo = new System.Windows.Forms.Label();
            this.tpImageResize = new System.Windows.Forms.TabPage();
            this.gbImageSize = new System.Windows.Forms.GroupBox();
            this.nudImageSizeFixedWidth = new System.Windows.Forms.NumericUpDown();
            this.nudImageSizeFixedHeight = new System.Windows.Forms.NumericUpDown();
            this.nudImageSizeRatio = new System.Windows.Forms.NumericUpDown();
            this.lblImageSizeFixedAutoScale = new System.Windows.Forms.Label();
            this.rbImageSizeDefault = new System.Windows.Forms.RadioButton();
            this.lblImageSizeFixedHeight = new System.Windows.Forms.Label();
            this.rbImageSizeFixed = new System.Windows.Forms.RadioButton();
            this.lblImageSizeFixedWidth = new System.Windows.Forms.Label();
            this.lblImageSizeRatioPercentage = new System.Windows.Forms.Label();
            this.rbImageSizeRatio = new System.Windows.Forms.RadioButton();
            this.tpOutputs = new System.Windows.Forms.TabPage();
            this.gbRemoteLocations = new System.Windows.Forms.GroupBox();
            this.flpTextUploaders = new System.Windows.Forms.FlowLayoutPanel();
            this.flpImageUploaders = new System.Windows.Forms.FlowLayoutPanel();
            this.flpFileUploaders = new System.Windows.Forms.FlowLayoutPanel();
            this.gbSaveToFile = new System.Windows.Forms.GroupBox();
            this.txtFileNameWithoutExt = new System.Windows.Forms.TextBox();
            this.txtSaveFolder = new System.Windows.Forms.TextBox();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.btnOutputsConfig = new System.Windows.Forms.Button();
            this.chkSaveFile = new System.Windows.Forms.CheckBox();
            this.chkPrinter = new System.Windows.Forms.CheckBox();
            this.chkUpload = new System.Windows.Forms.CheckBox();
            this.chkClipboard = new System.Windows.Forms.CheckBox();
            this.gbTasks = new System.Windows.Forms.GroupBox();
            this.flpTasks = new System.Windows.Forms.FlowLayoutPanel();
            this.btnTaskAnnotate = new System.Windows.Forms.Button();
            this.chkTaskImageFileFormat = new System.Windows.Forms.CheckBox();
            this.chkTaskImageResize = new System.Windows.Forms.CheckBox();
            this.btnCopyImageClose = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.tcMain.SuspendLayout();
            this.tpJob.SuspendLayout();
            this.gbTask.SuspendLayout();
            this.gbName.SuspendLayout();
            this.tpImagePreview.SuspendLayout();
            this.tpImageQuality.SuspendLayout();
            this.gbPictureQuality.SuspendLayout();
            this.tcQuality.SuspendLayout();
            this.tpQualityPng.SuspendLayout();
            this.tpQualityJpeg.SuspendLayout();
            this.tpQualityGif.SuspendLayout();
            this.tpQualityTiff.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudSwitchAfter)).BeginInit();
            this.tpImageResize.SuspendLayout();
            this.gbImageSize.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudImageSizeFixedWidth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudImageSizeFixedHeight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudImageSizeRatio)).BeginInit();
            this.tpOutputs.SuspendLayout();
            this.gbRemoteLocations.SuspendLayout();
            this.gbSaveToFile.SuspendLayout();
            this.gbTasks.SuspendLayout();
            this.flpTasks.SuspendLayout();
            this.SuspendLayout();
            // 
            // tcMain
            // 
            this.tcMain.Controls.Add(this.tpJob);
            this.tcMain.Controls.Add(this.tpImagePreview);
            this.tcMain.Controls.Add(this.tpImageQuality);
            this.tcMain.Controls.Add(this.tpImageResize);
            this.tcMain.Controls.Add(this.tpOutputs);
            this.tcMain.Location = new System.Drawing.Point(208, 8);
            this.tcMain.Name = "tcMain";
            this.tcMain.SelectedIndex = 0;
            this.tcMain.Size = new System.Drawing.Size(640, 400);
            this.tcMain.TabIndex = 1;
            // 
            // tpJob
            // 
            this.tpJob.Controls.Add(this.gbTask);
            this.tpJob.Controls.Add(this.gbName);
            this.tpJob.Controls.Add(this.chkUseHotkey);
            this.tpJob.Controls.Add(this.hmcHotkeys);
            this.tpJob.Location = new System.Drawing.Point(4, 22);
            this.tpJob.Name = "tpJob";
            this.tpJob.Padding = new System.Windows.Forms.Padding(3);
            this.tpJob.Size = new System.Drawing.Size(632, 374);
            this.tpJob.TabIndex = 0;
            this.tpJob.Text = "Job";
            this.tpJob.UseVisualStyleBackColor = true;
            // 
            // gbTask
            // 
            this.gbTask.Controls.Add(this.cboTask);
            this.gbTask.Location = new System.Drawing.Point(8, 72);
            this.gbTask.Name = "gbTask";
            this.gbTask.Size = new System.Drawing.Size(608, 56);
            this.gbTask.TabIndex = 1;
            this.gbTask.TabStop = false;
            this.gbTask.Text = "Task";
            // 
            // cboTask
            // 
            this.cboTask.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTask.FormattingEnabled = true;
            this.cboTask.Location = new System.Drawing.Point(8, 24);
            this.cboTask.Name = "cboTask";
            this.cboTask.Size = new System.Drawing.Size(360, 21);
            this.cboTask.TabIndex = 0;
            this.cboTask.SelectedIndexChanged += new System.EventHandler(this.cboTask_SelectedIndexChanged);
            // 
            // gbName
            // 
            this.gbName.Controls.Add(this.txtName);
            this.gbName.Location = new System.Drawing.Point(8, 8);
            this.gbName.Name = "gbName";
            this.gbName.Size = new System.Drawing.Size(608, 56);
            this.gbName.TabIndex = 0;
            this.gbName.TabStop = false;
            this.gbName.Text = "Description";
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(8, 24);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(560, 20);
            this.txtName.TabIndex = 0;
            // 
            // chkUseHotkey
            // 
            this.chkUseHotkey.AutoSize = true;
            this.chkUseHotkey.Location = new System.Drawing.Point(24, 160);
            this.chkUseHotkey.Name = "chkUseHotkey";
            this.chkUseHotkey.Size = new System.Drawing.Size(183, 17);
            this.chkUseHotkey.TabIndex = 3;
            this.chkUseHotkey.Text = "Enable a hotkey to run this profile";
            this.chkUseHotkey.UseVisualStyleBackColor = true;
            // 
            // hmcHotkeys
            // 
            this.hmcHotkeys.AutoScroll = true;
            this.hmcHotkeys.Location = new System.Drawing.Point(16, 144);
            this.hmcHotkeys.Name = "hmcHotkeys";
            this.hmcHotkeys.Size = new System.Drawing.Size(600, 32);
            this.hmcHotkeys.TabIndex = 2;
            // 
            // tpImagePreview
            // 
            this.tpImagePreview.Controls.Add(this.pbImage);
            this.tpImagePreview.Location = new System.Drawing.Point(4, 22);
            this.tpImagePreview.Name = "tpImagePreview";
            this.tpImagePreview.Padding = new System.Windows.Forms.Padding(3);
            this.tpImagePreview.Size = new System.Drawing.Size(632, 374);
            this.tpImagePreview.TabIndex = 1;
            this.tpImagePreview.Text = "Preview";
            this.tpImagePreview.UseVisualStyleBackColor = true;
            // 
            // pbImage
            // 
            this.pbImage.BackColor = System.Drawing.Color.White;
            this.pbImage.DisableViewer = false;
            this.pbImage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pbImage.Location = new System.Drawing.Point(3, 3);
            this.pbImage.Name = "pbImage";
            this.pbImage.Size = new System.Drawing.Size(626, 368);
            this.pbImage.TabIndex = 0;
            // 
            // tpImageQuality
            // 
            this.tpImageQuality.Controls.Add(this.gbPictureQuality);
            this.tpImageQuality.Location = new System.Drawing.Point(4, 22);
            this.tpImageQuality.Name = "tpImageQuality";
            this.tpImageQuality.Size = new System.Drawing.Size(632, 374);
            this.tpImageQuality.TabIndex = 2;
            this.tpImageQuality.Text = "Quality";
            this.tpImageQuality.UseVisualStyleBackColor = true;
            // 
            // gbPictureQuality
            // 
            this.gbPictureQuality.BackColor = System.Drawing.Color.Transparent;
            this.gbPictureQuality.Controls.Add(this.tcQuality);
            this.gbPictureQuality.Controls.Add(this.nudSwitchAfter);
            this.gbPictureQuality.Controls.Add(this.cboSwitchFormat);
            this.gbPictureQuality.Controls.Add(this.lblFileFormat);
            this.gbPictureQuality.Controls.Add(this.cboFileFormat);
            this.gbPictureQuality.Controls.Add(this.lblKB);
            this.gbPictureQuality.Controls.Add(this.lblAfter);
            this.gbPictureQuality.Controls.Add(this.lblSwitchTo);
            this.gbPictureQuality.Location = new System.Drawing.Point(8, 8);
            this.gbPictureQuality.Name = "gbPictureQuality";
            this.gbPictureQuality.Size = new System.Drawing.Size(608, 352);
            this.gbPictureQuality.TabIndex = 0;
            this.gbPictureQuality.TabStop = false;
            this.gbPictureQuality.Text = "Picture Quality";
            // 
            // tcQuality
            // 
            this.tcQuality.Controls.Add(this.tpQualityPng);
            this.tcQuality.Controls.Add(this.tpQualityJpeg);
            this.tcQuality.Controls.Add(this.tpQualityGif);
            this.tcQuality.Controls.Add(this.tpQualityTiff);
            this.tcQuality.Location = new System.Drawing.Point(16, 80);
            this.tcQuality.Name = "tcQuality";
            this.tcQuality.SelectedIndex = 0;
            this.tcQuality.Size = new System.Drawing.Size(584, 120);
            this.tcQuality.TabIndex = 7;
            // 
            // tpQualityPng
            // 
            this.tpQualityPng.Controls.Add(this.chkPngQualityInterlaced);
            this.tpQualityPng.Controls.Add(this.cboPngQuality);
            this.tpQualityPng.Location = new System.Drawing.Point(4, 22);
            this.tpQualityPng.Name = "tpQualityPng";
            this.tpQualityPng.Padding = new System.Windows.Forms.Padding(3);
            this.tpQualityPng.Size = new System.Drawing.Size(576, 94);
            this.tpQualityPng.TabIndex = 0;
            this.tpQualityPng.Text = "PNG";
            this.tpQualityPng.UseVisualStyleBackColor = true;
            // 
            // chkPngQualityInterlaced
            // 
            this.chkPngQualityInterlaced.AutoSize = true;
            this.chkPngQualityInterlaced.Location = new System.Drawing.Point(416, 16);
            this.chkPngQualityInterlaced.Name = "chkPngQualityInterlaced";
            this.chkPngQualityInterlaced.Size = new System.Drawing.Size(73, 17);
            this.chkPngQualityInterlaced.TabIndex = 1;
            this.chkPngQualityInterlaced.Text = "Interlaced";
            this.chkPngQualityInterlaced.UseVisualStyleBackColor = true;
            // 
            // cboPngQuality
            // 
            this.cboPngQuality.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboPngQuality.FormattingEnabled = true;
            this.cboPngQuality.Location = new System.Drawing.Point(8, 16);
            this.cboPngQuality.Name = "cboPngQuality";
            this.cboPngQuality.Size = new System.Drawing.Size(392, 21);
            this.cboPngQuality.TabIndex = 0;
            // 
            // tpQualityJpeg
            // 
            this.tpQualityJpeg.Controls.Add(this.lblQuality);
            this.tpQualityJpeg.Controls.Add(this.cboJpgSubSampling);
            this.tpQualityJpeg.Controls.Add(this.cboJpgQuality);
            this.tpQualityJpeg.Location = new System.Drawing.Point(4, 22);
            this.tpQualityJpeg.Name = "tpQualityJpeg";
            this.tpQualityJpeg.Padding = new System.Windows.Forms.Padding(3);
            this.tpQualityJpeg.Size = new System.Drawing.Size(576, 94);
            this.tpQualityJpeg.TabIndex = 1;
            this.tpQualityJpeg.Text = "JPEG";
            this.tpQualityJpeg.UseVisualStyleBackColor = true;
            // 
            // lblQuality
            // 
            this.lblQuality.AutoSize = true;
            this.lblQuality.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblQuality.Location = new System.Drawing.Point(12, 10);
            this.lblQuality.Name = "lblQuality";
            this.lblQuality.Size = new System.Drawing.Size(72, 13);
            this.lblQuality.TabIndex = 0;
            this.lblQuality.Text = "JPEG Quality:";
            // 
            // cboJpgSubSampling
            // 
            this.cboJpgSubSampling.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboJpgSubSampling.FormattingEnabled = true;
            this.cboJpgSubSampling.Location = new System.Drawing.Point(12, 58);
            this.cboJpgSubSampling.Name = "cboJpgSubSampling";
            this.cboJpgSubSampling.Size = new System.Drawing.Size(416, 21);
            this.cboJpgSubSampling.TabIndex = 2;
            // 
            // cboJpgQuality
            // 
            this.cboJpgQuality.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboJpgQuality.FormattingEnabled = true;
            this.cboJpgQuality.Location = new System.Drawing.Point(12, 26);
            this.cboJpgQuality.Name = "cboJpgQuality";
            this.cboJpgQuality.Size = new System.Drawing.Size(416, 21);
            this.cboJpgQuality.TabIndex = 1;
            // 
            // tpQualityGif
            // 
            this.tpQualityGif.Controls.Add(this.lblGIFQuality);
            this.tpQualityGif.Controls.Add(this.cboGIFQuality);
            this.tpQualityGif.Location = new System.Drawing.Point(4, 22);
            this.tpQualityGif.Name = "tpQualityGif";
            this.tpQualityGif.Size = new System.Drawing.Size(576, 94);
            this.tpQualityGif.TabIndex = 2;
            this.tpQualityGif.Text = "GIF";
            this.tpQualityGif.UseVisualStyleBackColor = true;
            // 
            // lblGIFQuality
            // 
            this.lblGIFQuality.AutoSize = true;
            this.lblGIFQuality.Location = new System.Drawing.Point(12, 10);
            this.lblGIFQuality.Name = "lblGIFQuality";
            this.lblGIFQuality.Size = new System.Drawing.Size(62, 13);
            this.lblGIFQuality.TabIndex = 0;
            this.lblGIFQuality.Text = "GIF Quality:";
            // 
            // cboGIFQuality
            // 
            this.cboGIFQuality.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboGIFQuality.FormattingEnabled = true;
            this.cboGIFQuality.Items.AddRange(new object[] {
            "Grayscale",
            "4 bit (16 colors)",
            "8 bit (256 colors)"});
            this.cboGIFQuality.Location = new System.Drawing.Point(12, 26);
            this.cboGIFQuality.Name = "cboGIFQuality";
            this.cboGIFQuality.Size = new System.Drawing.Size(98, 21);
            this.cboGIFQuality.TabIndex = 1;
            // 
            // tpQualityTiff
            // 
            this.tpQualityTiff.Controls.Add(this.cboTiffQuality);
            this.tpQualityTiff.Location = new System.Drawing.Point(4, 22);
            this.tpQualityTiff.Name = "tpQualityTiff";
            this.tpQualityTiff.Padding = new System.Windows.Forms.Padding(3);
            this.tpQualityTiff.Size = new System.Drawing.Size(576, 94);
            this.tpQualityTiff.TabIndex = 3;
            this.tpQualityTiff.Text = "TIFF";
            this.tpQualityTiff.UseVisualStyleBackColor = true;
            // 
            // cboTiffQuality
            // 
            this.cboTiffQuality.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTiffQuality.FormattingEnabled = true;
            this.cboTiffQuality.Location = new System.Drawing.Point(8, 16);
            this.cboTiffQuality.Name = "cboTiffQuality";
            this.cboTiffQuality.Size = new System.Drawing.Size(392, 21);
            this.cboTiffQuality.TabIndex = 0;
            // 
            // nudSwitchAfter
            // 
            this.nudSwitchAfter.Location = new System.Drawing.Point(125, 40);
            this.nudSwitchAfter.Maximum = new decimal(new int[] {
            1024,
            0,
            0,
            0});
            this.nudSwitchAfter.Name = "nudSwitchAfter";
            this.nudSwitchAfter.Size = new System.Drawing.Size(72, 20);
            this.nudSwitchAfter.TabIndex = 4;
            this.nudSwitchAfter.Value = new decimal(new int[] {
            350,
            0,
            0,
            0});
            this.nudSwitchAfter.ValueChanged += new System.EventHandler(this.nudSwitchAfter_ValueChanged);
            this.nudSwitchAfter.LostFocus += new System.EventHandler(this.nudSwitchAfter_LostFocus);
            // 
            // cboSwitchFormat
            // 
            this.cboSwitchFormat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboSwitchFormat.FormattingEnabled = true;
            this.cboSwitchFormat.Location = new System.Drawing.Point(232, 40);
            this.cboSwitchFormat.Name = "cboSwitchFormat";
            this.cboSwitchFormat.Size = new System.Drawing.Size(98, 21);
            this.cboSwitchFormat.TabIndex = 6;
            this.cboSwitchFormat.SelectedIndexChanged += new System.EventHandler(this.cboSwitchFormat_SelectedIndexChanged);
            // 
            // lblFileFormat
            // 
            this.lblFileFormat.AutoSize = true;
            this.lblFileFormat.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblFileFormat.Location = new System.Drawing.Point(16, 24);
            this.lblFileFormat.Name = "lblFileFormat";
            this.lblFileFormat.Size = new System.Drawing.Size(61, 13);
            this.lblFileFormat.TabIndex = 1;
            this.lblFileFormat.Text = "File Format:";
            // 
            // cboFileFormat
            // 
            this.cboFileFormat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboFileFormat.FormattingEnabled = true;
            this.cboFileFormat.Location = new System.Drawing.Point(16, 40);
            this.cboFileFormat.Name = "cboFileFormat";
            this.cboFileFormat.Size = new System.Drawing.Size(98, 21);
            this.cboFileFormat.TabIndex = 3;
            this.cboFileFormat.SelectedIndexChanged += new System.EventHandler(this.cboFileFormat_SelectedIndexChanged);
            // 
            // lblKB
            // 
            this.lblKB.AutoSize = true;
            this.lblKB.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblKB.Location = new System.Drawing.Point(197, 44);
            this.lblKB.Name = "lblKB";
            this.lblKB.Size = new System.Drawing.Size(23, 13);
            this.lblKB.TabIndex = 5;
            this.lblKB.Text = "KiB";
            // 
            // lblAfter
            // 
            this.lblAfter.AutoSize = true;
            this.lblAfter.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblAfter.Location = new System.Drawing.Point(125, 24);
            this.lblAfter.Name = "lblAfter";
            this.lblAfter.Size = new System.Drawing.Size(88, 13);
            this.lblAfter.TabIndex = 2;
            this.lblAfter.Text = "After: (0 disables)";
            // 
            // lblSwitchTo
            // 
            this.lblSwitchTo.AutoSize = true;
            this.lblSwitchTo.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblSwitchTo.Location = new System.Drawing.Point(235, 23);
            this.lblSwitchTo.Name = "lblSwitchTo";
            this.lblSwitchTo.Size = new System.Drawing.Size(54, 13);
            this.lblSwitchTo.TabIndex = 0;
            this.lblSwitchTo.Text = "Switch to:";
            // 
            // tpImageResize
            // 
            this.tpImageResize.Controls.Add(this.gbImageSize);
            this.tpImageResize.Location = new System.Drawing.Point(4, 22);
            this.tpImageResize.Name = "tpImageResize";
            this.tpImageResize.Padding = new System.Windows.Forms.Padding(3);
            this.tpImageResize.Size = new System.Drawing.Size(632, 374);
            this.tpImageResize.TabIndex = 3;
            this.tpImageResize.Text = "Resize";
            this.tpImageResize.UseVisualStyleBackColor = true;
            // 
            // gbImageSize
            // 
            this.gbImageSize.Controls.Add(this.nudImageSizeFixedWidth);
            this.gbImageSize.Controls.Add(this.nudImageSizeFixedHeight);
            this.gbImageSize.Controls.Add(this.nudImageSizeRatio);
            this.gbImageSize.Controls.Add(this.lblImageSizeFixedAutoScale);
            this.gbImageSize.Controls.Add(this.rbImageSizeDefault);
            this.gbImageSize.Controls.Add(this.lblImageSizeFixedHeight);
            this.gbImageSize.Controls.Add(this.rbImageSizeFixed);
            this.gbImageSize.Controls.Add(this.lblImageSizeFixedWidth);
            this.gbImageSize.Controls.Add(this.lblImageSizeRatioPercentage);
            this.gbImageSize.Controls.Add(this.rbImageSizeRatio);
            this.gbImageSize.Location = new System.Drawing.Point(8, 8);
            this.gbImageSize.Name = "gbImageSize";
            this.gbImageSize.Size = new System.Drawing.Size(608, 120);
            this.gbImageSize.TabIndex = 0;
            this.gbImageSize.TabStop = false;
            this.gbImageSize.Text = "Image Size";
            // 
            // nudImageSizeFixedWidth
            // 
            this.nudImageSizeFixedWidth.Location = new System.Drawing.Point(176, 56);
            this.nudImageSizeFixedWidth.Maximum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.nudImageSizeFixedWidth.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudImageSizeFixedWidth.Name = "nudImageSizeFixedWidth";
            this.nudImageSizeFixedWidth.Size = new System.Drawing.Size(64, 20);
            this.nudImageSizeFixedWidth.TabIndex = 2;
            this.nudImageSizeFixedWidth.Value = new decimal(new int[] {
            480,
            0,
            0,
            0});
            this.nudImageSizeFixedWidth.ValueChanged += new System.EventHandler(this.nudImageSizeFixedWidth_ValueChanged);
            // 
            // nudImageSizeFixedHeight
            // 
            this.nudImageSizeFixedHeight.Location = new System.Drawing.Point(312, 56);
            this.nudImageSizeFixedHeight.Maximum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.nudImageSizeFixedHeight.Name = "nudImageSizeFixedHeight";
            this.nudImageSizeFixedHeight.Size = new System.Drawing.Size(64, 20);
            this.nudImageSizeFixedHeight.TabIndex = 5;
            this.nudImageSizeFixedHeight.ValueChanged += new System.EventHandler(this.nudImageSizeFixedHeight_ValueChanged);
            // 
            // nudImageSizeRatio
            // 
            this.nudImageSizeRatio.Location = new System.Drawing.Point(120, 88);
            this.nudImageSizeRatio.Name = "nudImageSizeRatio";
            this.nudImageSizeRatio.Size = new System.Drawing.Size(40, 20);
            this.nudImageSizeRatio.TabIndex = 8;
            this.nudImageSizeRatio.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.nudImageSizeRatio.ValueChanged += new System.EventHandler(this.nudImageSizeRatio_ValueChanged);
            // 
            // lblImageSizeFixedAutoScale
            // 
            this.lblImageSizeFixedAutoScale.AutoSize = true;
            this.lblImageSizeFixedAutoScale.Location = new System.Drawing.Point(384, 60);
            this.lblImageSizeFixedAutoScale.Name = "lblImageSizeFixedAutoScale";
            this.lblImageSizeFixedAutoScale.Size = new System.Drawing.Size(152, 13);
            this.lblImageSizeFixedAutoScale.TabIndex = 6;
            this.lblImageSizeFixedAutoScale.Text = "0 height or width for auto scale";
            // 
            // rbImageSizeDefault
            // 
            this.rbImageSizeDefault.AutoSize = true;
            this.rbImageSizeDefault.Location = new System.Drawing.Point(16, 24);
            this.rbImageSizeDefault.Name = "rbImageSizeDefault";
            this.rbImageSizeDefault.Size = new System.Drawing.Size(110, 17);
            this.rbImageSizeDefault.TabIndex = 0;
            this.rbImageSizeDefault.TabStop = true;
            this.rbImageSizeDefault.Text = "Image size default";
            this.rbImageSizeDefault.UseVisualStyleBackColor = true;
            this.rbImageSizeDefault.CheckedChanged += new System.EventHandler(this.rbImageSizeDefault_CheckedChanged);
            // 
            // lblImageSizeFixedHeight
            // 
            this.lblImageSizeFixedHeight.AutoSize = true;
            this.lblImageSizeFixedHeight.Location = new System.Drawing.Point(248, 59);
            this.lblImageSizeFixedHeight.Name = "lblImageSizeFixedHeight";
            this.lblImageSizeFixedHeight.Size = new System.Drawing.Size(61, 13);
            this.lblImageSizeFixedHeight.TabIndex = 4;
            this.lblImageSizeFixedHeight.Text = "Height (px):";
            // 
            // rbImageSizeFixed
            // 
            this.rbImageSizeFixed.AutoSize = true;
            this.rbImageSizeFixed.Location = new System.Drawing.Point(16, 56);
            this.rbImageSizeFixed.Name = "rbImageSizeFixed";
            this.rbImageSizeFixed.Size = new System.Drawing.Size(103, 17);
            this.rbImageSizeFixed.TabIndex = 1;
            this.rbImageSizeFixed.TabStop = true;
            this.rbImageSizeFixed.Text = "Image size fixed:";
            this.rbImageSizeFixed.UseVisualStyleBackColor = true;
            this.rbImageSizeFixed.CheckedChanged += new System.EventHandler(this.rbImageSizeFixed_CheckedChanged);
            // 
            // lblImageSizeFixedWidth
            // 
            this.lblImageSizeFixedWidth.AutoSize = true;
            this.lblImageSizeFixedWidth.Location = new System.Drawing.Point(120, 59);
            this.lblImageSizeFixedWidth.Name = "lblImageSizeFixedWidth";
            this.lblImageSizeFixedWidth.Size = new System.Drawing.Size(58, 13);
            this.lblImageSizeFixedWidth.TabIndex = 3;
            this.lblImageSizeFixedWidth.Text = "Width (px):";
            // 
            // lblImageSizeRatioPercentage
            // 
            this.lblImageSizeRatioPercentage.AutoSize = true;
            this.lblImageSizeRatioPercentage.Location = new System.Drawing.Point(159, 91);
            this.lblImageSizeRatioPercentage.Name = "lblImageSizeRatioPercentage";
            this.lblImageSizeRatioPercentage.Size = new System.Drawing.Size(15, 13);
            this.lblImageSizeRatioPercentage.TabIndex = 9;
            this.lblImageSizeRatioPercentage.Text = "%";
            // 
            // rbImageSizeRatio
            // 
            this.rbImageSizeRatio.AutoSize = true;
            this.rbImageSizeRatio.Location = new System.Drawing.Point(16, 88);
            this.rbImageSizeRatio.Name = "rbImageSizeRatio";
            this.rbImageSizeRatio.Size = new System.Drawing.Size(101, 17);
            this.rbImageSizeRatio.TabIndex = 7;
            this.rbImageSizeRatio.TabStop = true;
            this.rbImageSizeRatio.Text = "Image size ratio:";
            this.rbImageSizeRatio.UseVisualStyleBackColor = true;
            this.rbImageSizeRatio.CheckedChanged += new System.EventHandler(this.rbImageSizeRatio_CheckedChanged);
            // 
            // tpOutputs
            // 
            this.tpOutputs.Controls.Add(this.gbRemoteLocations);
            this.tpOutputs.Controls.Add(this.gbSaveToFile);
            this.tpOutputs.Controls.Add(this.btnOutputsConfig);
            this.tpOutputs.Location = new System.Drawing.Point(4, 22);
            this.tpOutputs.Name = "tpOutputs";
            this.tpOutputs.Padding = new System.Windows.Forms.Padding(3);
            this.tpOutputs.Size = new System.Drawing.Size(632, 374);
            this.tpOutputs.TabIndex = 4;
            this.tpOutputs.Text = "Outputs";
            this.tpOutputs.UseVisualStyleBackColor = true;
            // 
            // gbRemoteLocations
            // 
            this.gbRemoteLocations.Controls.Add(this.flpTextUploaders);
            this.gbRemoteLocations.Controls.Add(this.flpImageUploaders);
            this.gbRemoteLocations.Controls.Add(this.flpFileUploaders);
            this.gbRemoteLocations.Location = new System.Drawing.Point(8, 136);
            this.gbRemoteLocations.Name = "gbRemoteLocations";
            this.gbRemoteLocations.Size = new System.Drawing.Size(608, 216);
            this.gbRemoteLocations.TabIndex = 2;
            this.gbRemoteLocations.TabStop = false;
            this.gbRemoteLocations.Text = "Upload to Remote Locations";
            // 
            // flpTextUploaders
            // 
            this.flpTextUploaders.AutoScroll = true;
            this.flpTextUploaders.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.flpTextUploaders.Location = new System.Drawing.Point(16, 24);
            this.flpTextUploaders.Name = "flpTextUploaders";
            this.flpTextUploaders.Size = new System.Drawing.Size(280, 176);
            this.flpTextUploaders.TabIndex = 0;
            this.flpTextUploaders.Visible = false;
            // 
            // flpImageUploaders
            // 
            this.flpImageUploaders.AutoScroll = true;
            this.flpImageUploaders.Location = new System.Drawing.Point(16, 24);
            this.flpImageUploaders.Name = "flpImageUploaders";
            this.flpImageUploaders.Size = new System.Drawing.Size(280, 176);
            this.flpImageUploaders.TabIndex = 1;
            this.flpImageUploaders.Visible = false;
            // 
            // flpFileUploaders
            // 
            this.flpFileUploaders.AutoScroll = true;
            this.flpFileUploaders.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.flpFileUploaders.Location = new System.Drawing.Point(312, 24);
            this.flpFileUploaders.Name = "flpFileUploaders";
            this.flpFileUploaders.Size = new System.Drawing.Size(280, 176);
            this.flpFileUploaders.TabIndex = 2;
            // 
            // gbSaveToFile
            // 
            this.gbSaveToFile.Controls.Add(this.txtFileNameWithoutExt);
            this.gbSaveToFile.Controls.Add(this.txtSaveFolder);
            this.gbSaveToFile.Controls.Add(this.btnBrowse);
            this.gbSaveToFile.Location = new System.Drawing.Point(8, 40);
            this.gbSaveToFile.Name = "gbSaveToFile";
            this.gbSaveToFile.Size = new System.Drawing.Size(608, 88);
            this.gbSaveToFile.TabIndex = 1;
            this.gbSaveToFile.TabStop = false;
            this.gbSaveToFile.Text = "When taking a screenshot, save the file to a preconfigured location";
            // 
            // txtFileNameWithoutExt
            // 
            this.txtFileNameWithoutExt.Location = new System.Drawing.Point(16, 24);
            this.txtFileNameWithoutExt.Name = "txtFileNameWithoutExt";
            this.txtFileNameWithoutExt.Size = new System.Drawing.Size(456, 20);
            this.txtFileNameWithoutExt.TabIndex = 0;
            // 
            // txtSaveFolder
            // 
            this.txtSaveFolder.Location = new System.Drawing.Point(16, 56);
            this.txtSaveFolder.Name = "txtSaveFolder";
            this.txtSaveFolder.Size = new System.Drawing.Size(456, 20);
            this.txtSaveFolder.TabIndex = 1;
            // 
            // btnBrowse
            // 
            this.btnBrowse.Location = new System.Drawing.Point(480, 52);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(80, 24);
            this.btnBrowse.TabIndex = 2;
            this.btnBrowse.Text = "&Browse...";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // btnOutputsConfig
            // 
            this.btnOutputsConfig.Location = new System.Drawing.Point(8, 8);
            this.btnOutputsConfig.Name = "btnOutputsConfig";
            this.btnOutputsConfig.Size = new System.Drawing.Size(336, 24);
            this.btnOutputsConfig.TabIndex = 0;
            this.btnOutputsConfig.Text = "Outputs Configuration... ( Dropbox, FTP, SendSpace etc. )";
            this.btnOutputsConfig.UseVisualStyleBackColor = true;
            this.btnOutputsConfig.Click += new System.EventHandler(this.btnOutputsConfig_Click);
            // 
            // chkSaveFile
            // 
            this.chkSaveFile.AutoSize = true;
            this.chkSaveFile.Location = new System.Drawing.Point(3, 101);
            this.chkSaveFile.Name = "chkSaveFile";
            this.chkSaveFile.Size = new System.Drawing.Size(79, 17);
            this.chkSaveFile.TabIndex = 4;
            this.chkSaveFile.Text = "Save to file";
            this.chkSaveFile.UseVisualStyleBackColor = true;
            this.chkSaveFile.CheckedChanged += new System.EventHandler(this.chkSaveFile_CheckedChanged);
            // 
            // chkPrinter
            // 
            this.chkPrinter.AutoSize = true;
            this.chkPrinter.Location = new System.Drawing.Point(3, 147);
            this.chkPrinter.Name = "chkPrinter";
            this.chkPrinter.Size = new System.Drawing.Size(96, 17);
            this.chkPrinter.TabIndex = 6;
            this.chkPrinter.Text = "Send to Printer";
            this.chkPrinter.UseVisualStyleBackColor = true;
            // 
            // chkUpload
            // 
            this.chkUpload.AutoSize = true;
            this.chkUpload.Location = new System.Drawing.Point(3, 124);
            this.chkUpload.Name = "chkUpload";
            this.chkUpload.Size = new System.Drawing.Size(161, 17);
            this.chkUpload.TabIndex = 5;
            this.chkUpload.Text = "Upload to Remote Locations";
            this.chkUpload.UseVisualStyleBackColor = true;
            // 
            // chkClipboard
            // 
            this.chkClipboard.AutoSize = true;
            this.chkClipboard.Location = new System.Drawing.Point(3, 78);
            this.chkClipboard.Name = "chkClipboard";
            this.chkClipboard.Size = new System.Drawing.Size(177, 17);
            this.chkClipboard.TabIndex = 3;
            this.chkClipboard.Text = "Copy Image or Text to Clipboard";
            this.chkClipboard.UseVisualStyleBackColor = true;
            // 
            // gbTasks
            // 
            this.gbTasks.Controls.Add(this.flpTasks);
            this.gbTasks.Dock = System.Windows.Forms.DockStyle.Left;
            this.gbTasks.Location = new System.Drawing.Point(0, 0);
            this.gbTasks.Name = "gbTasks";
            this.gbTasks.Size = new System.Drawing.Size(200, 460);
            this.gbTasks.TabIndex = 0;
            this.gbTasks.TabStop = false;
            this.gbTasks.Text = "I want to...";
            // 
            // flpTasks
            // 
            this.flpTasks.Controls.Add(this.btnTaskAnnotate);
            this.flpTasks.Controls.Add(this.chkTaskImageFileFormat);
            this.flpTasks.Controls.Add(this.chkTaskImageResize);
            this.flpTasks.Controls.Add(this.chkClipboard);
            this.flpTasks.Controls.Add(this.chkSaveFile);
            this.flpTasks.Controls.Add(this.chkUpload);
            this.flpTasks.Controls.Add(this.chkPrinter);
            this.flpTasks.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpTasks.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flpTasks.Location = new System.Drawing.Point(3, 16);
            this.flpTasks.Name = "flpTasks";
            this.flpTasks.Size = new System.Drawing.Size(194, 441);
            this.flpTasks.TabIndex = 0;
            // 
            // btnTaskAnnotate
            // 
            this.btnTaskAnnotate.Location = new System.Drawing.Point(3, 3);
            this.btnTaskAnnotate.Name = "btnTaskAnnotate";
            this.btnTaskAnnotate.Size = new System.Drawing.Size(117, 23);
            this.btnTaskAnnotate.TabIndex = 0;
            this.btnTaskAnnotate.Text = "Annotate Image...";
            this.btnTaskAnnotate.UseVisualStyleBackColor = true;
            this.btnTaskAnnotate.Click += new System.EventHandler(this.btnTaskAnnotate_Click);
            // 
            // chkTaskImageFileFormat
            // 
            this.chkTaskImageFileFormat.AutoSize = true;
            this.chkTaskImageFileFormat.Location = new System.Drawing.Point(3, 32);
            this.chkTaskImageFileFormat.Name = "chkTaskImageFileFormat";
            this.chkTaskImageFileFormat.Size = new System.Drawing.Size(127, 17);
            this.chkTaskImageFileFormat.TabIndex = 1;
            this.chkTaskImageFileFormat.Text = "Change image quality";
            this.chkTaskImageFileFormat.UseVisualStyleBackColor = true;
            this.chkTaskImageFileFormat.CheckedChanged += new System.EventHandler(this.chkTaskImageFileFormat_CheckedChanged);
            // 
            // chkTaskImageResize
            // 
            this.chkTaskImageResize.AutoSize = true;
            this.chkTaskImageResize.Location = new System.Drawing.Point(3, 55);
            this.chkTaskImageResize.Name = "chkTaskImageResize";
            this.chkTaskImageResize.Size = new System.Drawing.Size(89, 17);
            this.chkTaskImageResize.TabIndex = 2;
            this.chkTaskImageResize.Text = "Resize image";
            this.chkTaskImageResize.UseVisualStyleBackColor = true;
            this.chkTaskImageResize.CheckedChanged += new System.EventHandler(this.chkTaskImageResize_CheckedChanged);
            // 
            // btnCopyImageClose
            // 
            this.btnCopyImageClose.Location = new System.Drawing.Point(211, 424);
            this.btnCopyImageClose.Name = "btnCopyImageClose";
            this.btnCopyImageClose.Size = new System.Drawing.Size(117, 23);
            this.btnCopyImageClose.TabIndex = 2;
            this.btnCopyImageClose.Text = "Copy Image && Close";
            this.btnCopyImageClose.UseVisualStyleBackColor = true;
            this.btnCopyImageClose.Click += new System.EventHandler(this.btnCopyImageClose_Click);
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.Location = new System.Drawing.Point(599, 424);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(104, 24);
            this.btnOK.TabIndex = 3;
            this.btnOK.Text = "&Save && Continue";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(711, 424);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(72, 24);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // WorkflowWizard
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(857, 460);
            this.Controls.Add(this.gbTasks);
            this.Controls.Add(this.tcMain);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnCopyImageClose);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimumSize = new System.Drawing.Size(856, 488);
            this.Name = "WorkflowWizard";
            this.Text = "Configure Workflow";
            this.Load += new System.EventHandler(this.WorkflowWizard_Load);
            this.tcMain.ResumeLayout(false);
            this.tpJob.ResumeLayout(false);
            this.tpJob.PerformLayout();
            this.gbTask.ResumeLayout(false);
            this.gbName.ResumeLayout(false);
            this.gbName.PerformLayout();
            this.tpImagePreview.ResumeLayout(false);
            this.tpImageQuality.ResumeLayout(false);
            this.gbPictureQuality.ResumeLayout(false);
            this.gbPictureQuality.PerformLayout();
            this.tcQuality.ResumeLayout(false);
            this.tpQualityPng.ResumeLayout(false);
            this.tpQualityPng.PerformLayout();
            this.tpQualityJpeg.ResumeLayout(false);
            this.tpQualityJpeg.PerformLayout();
            this.tpQualityGif.ResumeLayout(false);
            this.tpQualityGif.PerformLayout();
            this.tpQualityTiff.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.nudSwitchAfter)).EndInit();
            this.tpImageResize.ResumeLayout(false);
            this.gbImageSize.ResumeLayout(false);
            this.gbImageSize.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudImageSizeFixedWidth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudImageSizeFixedHeight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudImageSizeRatio)).EndInit();
            this.tpOutputs.ResumeLayout(false);
            this.gbRemoteLocations.ResumeLayout(false);
            this.gbSaveToFile.ResumeLayout(false);
            this.gbSaveToFile.PerformLayout();
            this.gbTasks.ResumeLayout(false);
            this.flpTasks.ResumeLayout(false);
            this.flpTasks.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion


        internal System.Windows.Forms.ComboBox cboFileFormat;
        internal System.Windows.Forms.ComboBox cboSwitchFormat;
        internal System.Windows.Forms.GroupBox gbPictureQuality;
        internal System.Windows.Forms.Label lblAfter;
        internal System.Windows.Forms.Label lblFileFormat;
        internal System.Windows.Forms.Label lblKB;
        internal System.Windows.Forms.Label lblQuality;
        internal System.Windows.Forms.Label lblSwitchTo;
        internal System.Windows.Forms.NumericUpDown nudSwitchAfter;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnOutputsConfig;
        private System.Windows.Forms.CheckBox chkClipboard;
        private System.Windows.Forms.CheckBox chkPrinter;
        private System.Windows.Forms.CheckBox chkSaveFile;
        private System.Windows.Forms.CheckBox chkTaskImageFileFormat;
        private System.Windows.Forms.CheckBox chkTaskImageResize;
        private System.Windows.Forms.CheckBox chkUpload;
        private System.Windows.Forms.CheckBox chkUseHotkey;
        private System.Windows.Forms.ComboBox cboGIFQuality;
        private System.Windows.Forms.ComboBox cboJpgQuality;
        private System.Windows.Forms.ComboBox cboJpgSubSampling;
        private System.Windows.Forms.ComboBox cboTask;
        private System.Windows.Forms.GroupBox gbImageSize;
        private System.Windows.Forms.GroupBox gbRemoteLocations;
        private System.Windows.Forms.GroupBox gbSaveToFile;
        private System.Windows.Forms.GroupBox gbTasks;
        private System.Windows.Forms.Label lblGIFQuality;
        private System.Windows.Forms.Label lblImageSizeFixedAutoScale;
        private System.Windows.Forms.Label lblImageSizeFixedHeight;
        private System.Windows.Forms.Label lblImageSizeFixedWidth;
        private System.Windows.Forms.Label lblImageSizeRatioPercentage;
        private System.Windows.Forms.RadioButton rbImageSizeDefault;
        private System.Windows.Forms.RadioButton rbImageSizeFixed;
        private System.Windows.Forms.RadioButton rbImageSizeRatio;
        private System.Windows.Forms.TabPage tpImageQuality;
        private System.Windows.Forms.TabPage tpImageResize;
        private System.Windows.Forms.TabPage tpJob;
        private System.Windows.Forms.TabPage tpOutputs;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.TextBox txtSaveFolder;
        protected HelpersLib.Hotkey.HotkeyManagerControl hmcHotkeys;
        private System.Windows.Forms.GroupBox gbName;
        private System.Windows.Forms.GroupBox gbTask;
        private System.Windows.Forms.TabControl tcMain;
        private System.Windows.Forms.TextBox txtFileNameWithoutExt;
        private System.Windows.Forms.NumericUpDown nudImageSizeRatio;
        private System.Windows.Forms.NumericUpDown nudImageSizeFixedWidth;
        private System.Windows.Forms.NumericUpDown nudImageSizeFixedHeight;
        private System.Windows.Forms.FlowLayoutPanel flpFileUploaders;
        private System.Windows.Forms.FlowLayoutPanel flpTextUploaders;
        private System.Windows.Forms.FlowLayoutPanel flpImageUploaders;
        private System.Windows.Forms.FlowLayoutPanel flpTasks;
        private System.Windows.Forms.TabControl tcQuality;
        private System.Windows.Forms.TabPage tpQualityPng;
        private System.Windows.Forms.CheckBox chkPngQualityInterlaced;
        private System.Windows.Forms.ComboBox cboPngQuality;
        private System.Windows.Forms.TabPage tpQualityJpeg;
        private System.Windows.Forms.TabPage tpQualityGif;
        private System.Windows.Forms.TabPage tpQualityTiff;
        private System.Windows.Forms.ComboBox cboTiffQuality;
        private System.Windows.Forms.Button btnTaskAnnotate;
        private System.Windows.Forms.TabPage tpImagePreview;
        private HelpersLib.MyPictureBox pbImage;
        private System.Windows.Forms.Button btnCopyImageClose;

    }
}
using ZScreenLib;
using ZScreenGUI.UserControls;

namespace ZScreenGUI
{
    partial class ZScreen
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ZScreen));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.niTray = new System.Windows.Forms.NotifyIcon(this.components);
            this.cmTray = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiTabs = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmImageDest = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmEditinImageSoftware = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmCopytoClipboardMode = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmFTPClient = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmViewRemoteDirectory = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmViewLocalDirectory = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmActions = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmEntireScreen = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmSelectedWindow = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmCropShot = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmLastCropShot = new System.Windows.Forms.ToolStripMenuItem();
            this.autoScreenshotsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmClipboardUpload = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmDragDropWindow = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmLanguageTranslator = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmScreenColorPicker = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmQuickActions = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmQuickOptions = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmLicense = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmVersionHistory = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmExitZScreen = new System.Windows.Forms.ToolStripMenuItem();
            this.cmsHistory = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmCopyCbHistory = new System.Windows.Forms.ToolStripMenuItem();
            this.copyImageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.openLocalFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.browseURLToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openSourceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openSourceInDefaultWebBrowserHTMLToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copySourceToClipboardStringToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
            this.cmsRetryUpload = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ilApp = new System.Windows.Forms.ImageList(this.components);
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.tmrApp = new System.Windows.Forms.Timer(this.components);
            this.tcApp = new System.Windows.Forms.TabControl();
            this.tpMain = new System.Windows.Forms.TabPage();
            this.gbImageSettings = new System.Windows.Forms.GroupBox();
            this.lblScreenshotDelay = new System.Windows.Forms.Label();
            this.nudtScreenshotDelay = new ZScreenGUI.NumericUpDownTimer();
            this.lblCopytoClipboard = new System.Windows.Forms.Label();
            this.cboClipboardTextMode = new System.Windows.Forms.ComboBox();
            this.cbShowCursor = new System.Windows.Forms.CheckBox();
            this.chkManualNaming = new System.Windows.Forms.CheckBox();
            this.llProjectPage = new System.Windows.Forms.LinkLabel();
            this.llWebsite = new System.Windows.Forms.LinkLabel();
            this.llblBugReports = new System.Windows.Forms.LinkLabel();
            this.gbMainOptions = new System.Windows.Forms.GroupBox();
            this.cboFileUploaders = new System.Windows.Forms.ComboBox();
            this.lblFileUploader = new System.Windows.Forms.Label();
            this.cboURLShorteners = new System.Windows.Forms.ComboBox();
            this.lblURLShortener = new System.Windows.Forms.Label();
            this.lblImageUploader = new System.Windows.Forms.Label();
            this.lblTextUploader = new System.Windows.Forms.Label();
            this.cboImageUploaders = new System.Windows.Forms.ComboBox();
            this.cboTextUploaders = new System.Windows.Forms.ComboBox();
            this.lblLogo = new System.Windows.Forms.Label();
            this.pbLogo = new System.Windows.Forms.PictureBox();
            this.tpDestinations = new System.Windows.Forms.TabPage();
            this.tcAccounts = new System.Windows.Forms.TabControl();
            this.tpFTP = new System.Windows.Forms.TabPage();
            this.ucFTPAccounts = new ZScreenGUI.UserControls.AccountsControl();
            this.gbFTPSettings = new System.Windows.Forms.GroupBox();
            this.cbFTPThumbnailCheckSize = new System.Windows.Forms.CheckBox();
            this.lblFTPThumbHeight = new System.Windows.Forms.Label();
            this.lblFTPThumbWidth = new System.Windows.Forms.Label();
            this.txtFTPThumbWidth = new System.Windows.Forms.TextBox();
            this.txtFTPThumbHeight = new System.Windows.Forms.TextBox();
            this.chkEnableThumbnail = new System.Windows.Forms.CheckBox();
            this.tpTinyPic = new System.Windows.Forms.TabPage();
            this.gbTinyPic = new System.Windows.Forms.GroupBox();
            this.btnGalleryTinyPic = new System.Windows.Forms.Button();
            this.btnRegCodeTinyPic = new System.Windows.Forms.Button();
            this.lblRegistrationCode = new System.Windows.Forms.Label();
            this.txtTinyPicShuk = new System.Windows.Forms.TextBox();
            this.chkRememberTinyPicUserPass = new System.Windows.Forms.CheckBox();
            this.tpImageShack = new System.Windows.Forms.TabPage();
            this.chkPublicImageShack = new System.Windows.Forms.CheckBox();
            this.gbImageShack = new System.Windows.Forms.GroupBox();
            this.btnImageShackProfile = new System.Windows.Forms.Button();
            this.lblImageShackUsername = new System.Windows.Forms.Label();
            this.txtUserNameImageShack = new System.Windows.Forms.TextBox();
            this.btnGalleryImageShack = new System.Windows.Forms.Button();
            this.btnRegCodeImageShack = new System.Windows.Forms.Button();
            this.lblImageShackRegistrationCode = new System.Windows.Forms.Label();
            this.txtImageShackRegistrationCode = new System.Windows.Forms.TextBox();
            this.tpTwitter = new System.Windows.Forms.TabPage();
            this.tcTwitter = new System.Windows.Forms.TabControl();
            this.tpTwitPic = new System.Windows.Forms.TabPage();
            this.cboTwitPicUploadMode = new System.Windows.Forms.ComboBox();
            this.lblTwitPicThumbnailMode = new System.Windows.Forms.Label();
            this.lblTwitPicUploadMode = new System.Windows.Forms.Label();
            this.cboTwitPicThumbnailMode = new System.Windows.Forms.ComboBox();
            this.cbTwitPicShowFull = new System.Windows.Forms.CheckBox();
            this.tpYfrog = new System.Windows.Forms.TabPage();
            this.cboYfrogUploadMode = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.gbTwitterAccount = new System.Windows.Forms.GroupBox();
            this.lblTwitterPassword = new System.Windows.Forms.Label();
            this.txtTwitPicPassword = new System.Windows.Forms.TextBox();
            this.lblTwitterUsername = new System.Windows.Forms.Label();
            this.txtTwitPicUserName = new System.Windows.Forms.TextBox();
            this.tpImageBam = new System.Windows.Forms.TabPage();
            this.gbImageBamGalleries = new System.Windows.Forms.GroupBox();
            this.lbImageBamGalleries = new System.Windows.Forms.ListBox();
            this.gbImageBamLinks = new System.Windows.Forms.GroupBox();
            this.chkImageBamContentNSFW = new System.Windows.Forms.CheckBox();
            this.btnImageBamRemoveGallery = new System.Windows.Forms.Button();
            this.btnImageBamCreateGallery = new System.Windows.Forms.Button();
            this.btnImageBamRegister = new System.Windows.Forms.Button();
            this.btnImageBamApiKeysUrl = new System.Windows.Forms.Button();
            this.gbImageBamApiKeys = new System.Windows.Forms.GroupBox();
            this.lblImageBamSecret = new System.Windows.Forms.Label();
            this.txtImageBamSecret = new System.Windows.Forms.TextBox();
            this.lblImageBamKey = new System.Windows.Forms.Label();
            this.txtImageBamApiKey = new System.Windows.Forms.TextBox();
            this.tpRapidShare = new System.Windows.Forms.TabPage();
            this.lblRapidSharePassword = new System.Windows.Forms.Label();
            this.lblRapidSharePremiumUsername = new System.Windows.Forms.Label();
            this.lblRapidShareCollectorsID = new System.Windows.Forms.Label();
            this.txtRapidSharePassword = new System.Windows.Forms.TextBox();
            this.txtRapidSharePremiumUserName = new System.Windows.Forms.TextBox();
            this.txtRapidShareCollectorID = new System.Windows.Forms.TextBox();
            this.cboRapidShareAcctType = new System.Windows.Forms.ComboBox();
            this.lblRapidShareAccountType = new System.Windows.Forms.Label();
            this.tpSendSpace = new System.Windows.Forms.TabPage();
            this.btnSendSpaceRegister = new System.Windows.Forms.Button();
            this.lblSendSpacePassword = new System.Windows.Forms.Label();
            this.lblSendSpaceUsername = new System.Windows.Forms.Label();
            this.txtSendSpacePassword = new System.Windows.Forms.TextBox();
            this.txtSendSpaceUserName = new System.Windows.Forms.TextBox();
            this.cboSendSpaceAcctType = new System.Windows.Forms.ComboBox();
            this.lblSendSpaceAccountType = new System.Windows.Forms.Label();
            this.tpMindTouch = new System.Windows.Forms.TabPage();
            this.gbMindTouchOptions = new System.Windows.Forms.GroupBox();
            this.chkDekiWikiForcePath = new System.Windows.Forms.CheckBox();
            this.ucMindTouchAccounts = new ZScreenGUI.UserControls.AccountsControl();
            this.tpFlickr = new System.Windows.Forms.TabPage();
            this.btnFlickrOpenImages = new System.Windows.Forms.Button();
            this.pgFlickrAuthInfo = new System.Windows.Forms.PropertyGrid();
            this.pgFlickrSettings = new System.Windows.Forms.PropertyGrid();
            this.btnFlickrCheckToken = new System.Windows.Forms.Button();
            this.btnFlickrGetToken = new System.Windows.Forms.Button();
            this.btnFlickrGetFrob = new System.Windows.Forms.Button();
            this.tpHotkeys = new System.Windows.Forms.TabPage();
            this.lblHotkeyStatus = new System.Windows.Forms.Label();
            this.dgvHotkeys = new System.Windows.Forms.DataGridView();
            this.chHotkeys_Description = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.chHotkeys_Keys = new System.Windows.Forms.DataGridViewButtonColumn();
            this.tpScreenshots = new System.Windows.Forms.TabPage();
            this.tcScreenshots = new System.Windows.Forms.TabControl();
            this.tpCropShot = new System.Windows.Forms.TabPage();
            this.gbDynamicRegionBorderColorSettings = new System.Windows.Forms.GroupBox();
            this.nudCropRegionStep = new System.Windows.Forms.NumericUpDown();
            this.nudCropHueRange = new System.Windows.Forms.NumericUpDown();
            this.cbCropDynamicBorderColor = new System.Windows.Forms.CheckBox();
            this.lblCropRegionInterval = new System.Windows.Forms.Label();
            this.lblCropHueRange = new System.Windows.Forms.Label();
            this.lblCropRegionStep = new System.Windows.Forms.Label();
            this.nudCropRegionInterval = new System.Windows.Forms.NumericUpDown();
            this.gbDynamicCrosshair = new System.Windows.Forms.GroupBox();
            this.cbCropDynamicCrosshair = new System.Windows.Forms.CheckBox();
            this.lblCropCrosshairStep = new System.Windows.Forms.Label();
            this.lblCropCrosshairInterval = new System.Windows.Forms.Label();
            this.nudCropCrosshairInterval = new System.Windows.Forms.NumericUpDown();
            this.nudCropCrosshairStep = new System.Windows.Forms.NumericUpDown();
            this.gpCropRegion = new System.Windows.Forms.GroupBox();
            this.lblCropRegionStyle = new System.Windows.Forms.Label();
            this.cbRegionHotkeyInfo = new System.Windows.Forms.CheckBox();
            this.cbCropStyle = new System.Windows.Forms.ComboBox();
            this.cbRegionRectangleInfo = new System.Windows.Forms.CheckBox();
            this.gbCropRegionSettings = new System.Windows.Forms.GroupBox();
            this.lblCropBorderSize = new System.Windows.Forms.Label();
            this.cbShowCropRuler = new System.Windows.Forms.CheckBox();
            this.cbCropShowGrids = new System.Windows.Forms.CheckBox();
            this.lblCropBorderColor = new System.Windows.Forms.Label();
            this.pbCropBorderColor = new System.Windows.Forms.PictureBox();
            this.nudCropBorderSize = new System.Windows.Forms.NumericUpDown();
            this.gbCrosshairSettings = new System.Windows.Forms.GroupBox();
            this.cbCropShowMagnifyingGlass = new System.Windows.Forms.CheckBox();
            this.cbCropShowBigCross = new System.Windows.Forms.CheckBox();
            this.pbCropCrosshairColor = new System.Windows.Forms.PictureBox();
            this.lblCropCrosshairColor = new System.Windows.Forms.Label();
            this.nudCrosshairLineCount = new System.Windows.Forms.NumericUpDown();
            this.nudCrosshairLineSize = new System.Windows.Forms.NumericUpDown();
            this.lblCrosshairLineSize = new System.Windows.Forms.Label();
            this.lblCrosshairLineCount = new System.Windows.Forms.Label();
            this.gbGridMode = new System.Windows.Forms.GroupBox();
            this.cboCropGridMode = new System.Windows.Forms.CheckBox();
            this.nudCropGridHeight = new System.Windows.Forms.NumericUpDown();
            this.lblGridSizeWidth = new System.Windows.Forms.Label();
            this.lblGridSize = new System.Windows.Forms.Label();
            this.lblGridSizeHeight = new System.Windows.Forms.Label();
            this.nudCropGridWidth = new System.Windows.Forms.NumericUpDown();
            this.tpSelectedWindow = new System.Windows.Forms.TabPage();
            this.cbSelectedWindowCaptureObjects = new System.Windows.Forms.CheckBox();
            this.nudSelectedWindowHueRange = new System.Windows.Forms.NumericUpDown();
            this.lblSelectedWindowHueRange = new System.Windows.Forms.Label();
            this.nudSelectedWindowRegionStep = new System.Windows.Forms.NumericUpDown();
            this.nudSelectedWindowRegionInterval = new System.Windows.Forms.NumericUpDown();
            this.lblSelectedWindowRegionStep = new System.Windows.Forms.Label();
            this.lblSelectedWindowRegionInterval = new System.Windows.Forms.Label();
            this.cbSelectedWindowDynamicBorderColor = new System.Windows.Forms.CheckBox();
            this.cbSelectedWindowRuler = new System.Windows.Forms.CheckBox();
            this.lblSelectedWindowRegionStyle = new System.Windows.Forms.Label();
            this.cbSelectedWindowStyle = new System.Windows.Forms.ComboBox();
            this.cbSelectedWindowRectangleInfo = new System.Windows.Forms.CheckBox();
            this.lblSelectedWindowBorderColor = new System.Windows.Forms.Label();
            this.nudSelectedWindowBorderSize = new System.Windows.Forms.NumericUpDown();
            this.lblSelectedWindowBorderSize = new System.Windows.Forms.Label();
            this.pbSelectedWindowBorderColor = new System.Windows.Forms.PictureBox();
            this.tpWatermark = new System.Windows.Forms.TabPage();
            this.pbWatermarkShow = new System.Windows.Forms.PictureBox();
            this.gbWatermarkGeneral = new System.Windows.Forms.GroupBox();
            this.lblWatermarkOffsetPixel = new System.Windows.Forms.Label();
            this.cboWatermarkType = new System.Windows.Forms.ComboBox();
            this.cbWatermarkAutoHide = new System.Windows.Forms.CheckBox();
            this.cbWatermarkAddReflection = new System.Windows.Forms.CheckBox();
            this.lblWatermarkType = new System.Windows.Forms.Label();
            this.cbWatermarkPosition = new System.Windows.Forms.ComboBox();
            this.lblWatermarkPosition = new System.Windows.Forms.Label();
            this.nudWatermarkOffset = new System.Windows.Forms.NumericUpDown();
            this.lblWatermarkOffset = new System.Windows.Forms.Label();
            this.tcWatermark = new System.Windows.Forms.TabControl();
            this.tpWatermarkText = new System.Windows.Forms.TabPage();
            this.gbWatermarkBackground = new System.Windows.Forms.GroupBox();
            this.trackWatermarkBackgroundTrans = new System.Windows.Forms.TrackBar();
            this.cbWatermarkGradientType = new System.Windows.Forms.ComboBox();
            this.lblWatermarkGradientType = new System.Windows.Forms.Label();
            this.lblWatermarkCornerRadiusTip = new System.Windows.Forms.Label();
            this.nudWatermarkBackTrans = new System.Windows.Forms.NumericUpDown();
            this.nudWatermarkCornerRadius = new System.Windows.Forms.NumericUpDown();
            this.lblRectangleCornerRadius = new System.Windows.Forms.Label();
            this.lblWatermarkBackColorsTip = new System.Windows.Forms.Label();
            this.lblWatermarkBackTrans = new System.Windows.Forms.Label();
            this.pbWatermarkGradient1 = new System.Windows.Forms.PictureBox();
            this.pbWatermarkBorderColor = new System.Windows.Forms.PictureBox();
            this.pbWatermarkGradient2 = new System.Windows.Forms.PictureBox();
            this.lblWatermarkBackColors = new System.Windows.Forms.Label();
            this.gbWatermarkText = new System.Windows.Forms.GroupBox();
            this.trackWatermarkFontTrans = new System.Windows.Forms.TrackBar();
            this.lblWatermarkText = new System.Windows.Forms.Label();
            this.nudWatermarkFontTrans = new System.Windows.Forms.NumericUpDown();
            this.lblWatermarkFont = new System.Windows.Forms.Label();
            this.btnWatermarkFont = new System.Windows.Forms.Button();
            this.lblWatermarkFontTrans = new System.Windows.Forms.Label();
            this.txtWatermarkText = new System.Windows.Forms.TextBox();
            this.pbWatermarkFontColor = new System.Windows.Forms.PictureBox();
            this.tpWatermarkImage = new System.Windows.Forms.TabPage();
            this.lblWatermarkImageScale = new System.Windows.Forms.Label();
            this.nudWatermarkImageScale = new System.Windows.Forms.NumericUpDown();
            this.cbWatermarkUseBorder = new System.Windows.Forms.CheckBox();
            this.btwWatermarkBrowseImage = new System.Windows.Forms.Button();
            this.txtWatermarkImageLocation = new System.Windows.Forms.TextBox();
            this.tpFileNaming = new System.Windows.Forms.TabPage();
            this.btnResetIncrement = new System.Windows.Forms.Button();
            this.gbOthersNaming = new System.Windows.Forms.GroupBox();
            this.lblEntireScreenPreview = new System.Windows.Forms.Label();
            this.txtEntireScreen = new System.Windows.Forms.TextBox();
            this.gbCodeTitle = new System.Windows.Forms.GroupBox();
            this.btnCodesI = new System.Windows.Forms.Button();
            this.btnCodesPm = new System.Windows.Forms.Button();
            this.btnCodesS = new System.Windows.Forms.Button();
            this.btnCodesMi = new System.Windows.Forms.Button();
            this.btnCodesH = new System.Windows.Forms.Button();
            this.btnCodesY = new System.Windows.Forms.Button();
            this.btnCodesD = new System.Windows.Forms.Button();
            this.btnCodesMo = new System.Windows.Forms.Button();
            this.btnCodesT = new System.Windows.Forms.Button();
            this.lblCodeI = new System.Windows.Forms.Label();
            this.lblCodeT = new System.Windows.Forms.Label();
            this.lblCodeMo = new System.Windows.Forms.Label();
            this.lblCodePm = new System.Windows.Forms.Label();
            this.lblCodeD = new System.Windows.Forms.Label();
            this.lblCodeS = new System.Windows.Forms.Label();
            this.lblCodeMi = new System.Windows.Forms.Label();
            this.lblCodeY = new System.Windows.Forms.Label();
            this.lblCodeH = new System.Windows.Forms.Label();
            this.gbActiveWindowNaming = new System.Windows.Forms.GroupBox();
            this.lblActiveWindowPreview = new System.Windows.Forms.Label();
            this.txtActiveWindow = new System.Windows.Forms.TextBox();
            this.tpCaptureQuality = new System.Windows.Forms.TabPage();
            this.gbImageSize = new System.Windows.Forms.GroupBox();
            this.rbImageSizeDefault = new System.Windows.Forms.RadioButton();
            this.lblImageSizeFixedHeight = new System.Windows.Forms.Label();
            this.rbImageSizeFixed = new System.Windows.Forms.RadioButton();
            this.lblImageSizeFixedWidth = new System.Windows.Forms.Label();
            this.txtImageSizeRatio = new System.Windows.Forms.TextBox();
            this.lblImageSizeRatioPercentage = new System.Windows.Forms.Label();
            this.txtImageSizeFixedWidth = new System.Windows.Forms.TextBox();
            this.rbImageSizeRatio = new System.Windows.Forms.RadioButton();
            this.txtImageSizeFixedHeight = new System.Windows.Forms.TextBox();
            this.gbPictureQuality = new System.Windows.Forms.GroupBox();
            this.nudSwitchAfter = new System.Windows.Forms.NumericUpDown();
            this.nudImageQuality = new System.Windows.Forms.NumericUpDown();
            this.lblJPEGQualityPercentage = new System.Windows.Forms.Label();
            this.lblQuality = new System.Windows.Forms.Label();
            this.cbSwitchFormat = new System.Windows.Forms.ComboBox();
            this.lblFileFormat = new System.Windows.Forms.Label();
            this.cbFileFormat = new System.Windows.Forms.ComboBox();
            this.lblKB = new System.Windows.Forms.Label();
            this.lblAfter = new System.Windows.Forms.Label();
            this.lblSwitchTo = new System.Windows.Forms.Label();
            this.tpEditors = new System.Windows.Forms.TabPage();
            this.tcEditors = new System.Windows.Forms.TabControl();
            this.tpEditorsImages = new System.Windows.Forms.TabPage();
            this.gbImageEditorSettings = new System.Windows.Forms.GroupBox();
            this.chkImageEditorAutoSave = new System.Windows.Forms.CheckBox();
            this.pgEditorsImage = new System.Windows.Forms.PropertyGrid();
            this.btnRemoveImageEditor = new System.Windows.Forms.Button();
            this.btnBrowseImageEditor = new System.Windows.Forms.Button();
            this.lbImageSoftware = new System.Windows.Forms.ListBox();
            this.btnAddImageSoftware = new System.Windows.Forms.Button();
            this.tpImageHosting = new System.Windows.Forms.TabPage();
            this.tcImages = new System.Windows.Forms.TabControl();
            this.tpImageUploaders = new System.Windows.Forms.TabPage();
            this.gbImageUploadRetry = new System.Windows.Forms.GroupBox();
            this.lblErrorRetry = new System.Windows.Forms.Label();
            this.lblUploadDurationLimit = new System.Windows.Forms.Label();
            this.chkImageUploadRetryOnFail = new System.Windows.Forms.CheckBox();
            this.cboImageUploadRetryOnTimeout = new System.Windows.Forms.CheckBox();
            this.nudUploadDurationLimit = new System.Windows.Forms.NumericUpDown();
            this.nudErrorRetry = new System.Windows.Forms.NumericUpDown();
            this.gbImageUploaderOptions = new System.Windows.Forms.GroupBox();
            this.cbAutoSwitchFileUploader = new System.Windows.Forms.CheckBox();
            this.cbTinyPicSizeCheck = new System.Windows.Forms.CheckBox();
            this.cbAddFailedScreenshot = new System.Windows.Forms.CheckBox();
            this.cboUploadMode = new System.Windows.Forms.ComboBox();
            this.lblUploadAs = new System.Windows.Forms.Label();
            this.tpCustomUploaders = new System.Windows.Forms.TabPage();
            this.txtUploadersLog = new System.Windows.Forms.RichTextBox();
            this.btnUploadersTest = new System.Windows.Forms.Button();
            this.txtFullImage = new System.Windows.Forms.TextBox();
            this.txtThumbnail = new System.Windows.Forms.TextBox();
            this.lblFullImage = new System.Windows.Forms.Label();
            this.lblThumbnail = new System.Windows.Forms.Label();
            this.gbImageUploaders = new System.Windows.Forms.GroupBox();
            this.lbUploader = new System.Windows.Forms.ListBox();
            this.btnUploadersClear = new System.Windows.Forms.Button();
            this.btnUploaderExport = new System.Windows.Forms.Button();
            this.btnUploaderRemove = new System.Windows.Forms.Button();
            this.btnUploaderImport = new System.Windows.Forms.Button();
            this.btnUploaderUpdate = new System.Windows.Forms.Button();
            this.txtUploader = new System.Windows.Forms.TextBox();
            this.btnUploaderAdd = new System.Windows.Forms.Button();
            this.gbRegexp = new System.Windows.Forms.GroupBox();
            this.btnRegexpEdit = new System.Windows.Forms.Button();
            this.txtRegexp = new System.Windows.Forms.TextBox();
            this.lvRegexps = new System.Windows.Forms.ListView();
            this.lvRegexpsColumn = new System.Windows.Forms.ColumnHeader();
            this.btnRegexpRemove = new System.Windows.Forms.Button();
            this.btnRegexpAdd = new System.Windows.Forms.Button();
            this.txtFileForm = new System.Windows.Forms.TextBox();
            this.lblFileForm = new System.Windows.Forms.Label();
            this.lblUploadURL = new System.Windows.Forms.Label();
            this.txtUploadURL = new System.Windows.Forms.TextBox();
            this.gbArguments = new System.Windows.Forms.GroupBox();
            this.btnArgEdit = new System.Windows.Forms.Button();
            this.txtArg2 = new System.Windows.Forms.TextBox();
            this.btnArgRemove = new System.Windows.Forms.Button();
            this.lvArguments = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.btnArgAdd = new System.Windows.Forms.Button();
            this.txtArg1 = new System.Windows.Forms.TextBox();
            this.tpWebPageUpload = new System.Windows.Forms.TabPage();
            this.cbWebPageAutoUpload = new System.Windows.Forms.CheckBox();
            this.lblWebPageHeight = new System.Windows.Forms.Label();
            this.lblWebPageWidth = new System.Windows.Forms.Label();
            this.txtWebPageHeight = new System.Windows.Forms.TextBox();
            this.txtWebPageWidth = new System.Windows.Forms.TextBox();
            this.cbWebPageUseCustomSize = new System.Windows.Forms.CheckBox();
            this.btnWebPageImageUpload = new System.Windows.Forms.Button();
            this.pWebPageImage = new System.Windows.Forms.Panel();
            this.pbWebPageImage = new System.Windows.Forms.PictureBox();
            this.btnWebPageCaptureImage = new System.Windows.Forms.Button();
            this.txtWebPageURL = new System.Windows.Forms.TextBox();
            this.tpTextServices = new System.Windows.Forms.TabPage();
            this.tcTextUploaders = new System.Windows.Forms.TabControl();
            this.tpTextUploaders = new System.Windows.Forms.TabPage();
            this.ucTextUploaders = new ZScreenGUI.UserControls.TextUploadersControl();
            this.tpURLShorteners = new System.Windows.Forms.TabPage();
            this.ucUrlShorteners = new ZScreenGUI.UserControls.TextUploadersControl();
            this.tpTreeGUI = new System.Windows.Forms.TabPage();
            this.pgIndexer = new System.Windows.Forms.PropertyGrid();
            this.tpTranslator = new System.Windows.Forms.TabPage();
            this.txtAutoTranslate = new System.Windows.Forms.TextBox();
            this.cbAutoTranslate = new System.Windows.Forms.CheckBox();
            this.btnTranslateTo1 = new System.Windows.Forms.Button();
            this.lblDictionary = new System.Windows.Forms.Label();
            this.txtDictionary = new System.Windows.Forms.TextBox();
            this.cbClipboardTranslate = new System.Windows.Forms.CheckBox();
            this.txtTranslateResult = new System.Windows.Forms.TextBox();
            this.txtLanguages = new System.Windows.Forms.TextBox();
            this.btnTranslate = new System.Windows.Forms.Button();
            this.txtTranslateText = new System.Windows.Forms.TextBox();
            this.lblToLanguage = new System.Windows.Forms.Label();
            this.lblFromLanguage = new System.Windows.Forms.Label();
            this.cbToLanguage = new System.Windows.Forms.ComboBox();
            this.cbFromLanguage = new System.Windows.Forms.ComboBox();
            this.tpHistory = new System.Windows.Forms.TabPage();
            this.tcHistory = new System.Windows.Forms.TabControl();
            this.tpHistoryList = new System.Windows.Forms.TabPage();
            this.tlpHistory = new System.Windows.Forms.TableLayoutPanel();
            this.tlpHistoryControls = new System.Windows.Forms.TableLayoutPanel();
            this.lblHistoryScreenshot = new System.Windows.Forms.Label();
            this.panelControls = new System.Windows.Forms.Panel();
            this.btnHistoryOpenLocalFile = new System.Windows.Forms.Button();
            this.txtHistoryLocalPath = new System.Windows.Forms.TextBox();
            this.btnHistoryCopyLink = new System.Windows.Forms.Button();
            this.lblHistoryRemotePath = new System.Windows.Forms.Label();
            this.btnHistoryCopyImage = new System.Windows.Forms.Button();
            this.txtHistoryRemotePath = new System.Windows.Forms.TextBox();
            this.btnHistoryBrowseURL = new System.Windows.Forms.Button();
            this.lblHistoryLocalPath = new System.Windows.Forms.Label();
            this.panelPreview = new System.Windows.Forms.Panel();
            this.pbPreview = new System.Windows.Forms.PictureBox();
            this.txtPreview = new System.Windows.Forms.TextBox();
            this.historyBrowser = new System.Windows.Forms.WebBrowser();
            this.lbHistory = new System.Windows.Forms.ListBox();
            this.tpHistorySettings = new System.Windows.Forms.TabPage();
            this.cbHistorySave = new System.Windows.Forms.CheckBox();
            this.cbShowHistoryTooltip = new System.Windows.Forms.CheckBox();
            this.btnHistoryClear = new System.Windows.Forms.Button();
            this.cbHistoryListFormat = new System.Windows.Forms.ComboBox();
            this.lblHistoryMaxItems = new System.Windows.Forms.Label();
            this.lblHistoryListFormat = new System.Windows.Forms.Label();
            this.nudHistoryMaxItems = new System.Windows.Forms.NumericUpDown();
            this.cbHistoryAddSpace = new System.Windows.Forms.CheckBox();
            this.cbHistoryReverseList = new System.Windows.Forms.CheckBox();
            this.tpOptions = new System.Windows.Forms.TabPage();
            this.tcOptions = new System.Windows.Forms.TabControl();
            this.tpGeneral = new System.Windows.Forms.TabPage();
            this.gbUpdates = new System.Windows.Forms.GroupBox();
            this.cbCheckUpdatesBeta = new System.Windows.Forms.CheckBox();
            this.lblUpdateInfo = new System.Windows.Forms.Label();
            this.btnCheckUpdate = new System.Windows.Forms.Button();
            this.cbCheckUpdates = new System.Windows.Forms.CheckBox();
            this.gbMisc = new System.Windows.Forms.GroupBox();
            this.chkWindows7TaskbarIntegration = new System.Windows.Forms.CheckBox();
            this.cbAutoSaveSettings = new System.Windows.Forms.CheckBox();
            this.cbSaveFormSizePosition = new System.Windows.Forms.CheckBox();
            this.cbShowHelpBalloonTips = new System.Windows.Forms.CheckBox();
            this.cbLockFormSize = new System.Windows.Forms.CheckBox();
            this.chkShowTaskbar = new System.Windows.Forms.CheckBox();
            this.chkOpenMainWindow = new System.Windows.Forms.CheckBox();
            this.chkStartWin = new System.Windows.Forms.CheckBox();
            this.tpProxy = new System.Windows.Forms.TabPage();
            this.gpProxySettings = new System.Windows.Forms.GroupBox();
            this.chkProxyEnable = new System.Windows.Forms.CheckBox();
            this.ucProxyAccounts = new ZScreenGUI.UserControls.AccountsControl();
            this.tpInteraction = new System.Windows.Forms.TabPage();
            this.gbActionsToolbarSettings = new System.Windows.Forms.GroupBox();
            this.cbCloseQuickActions = new System.Windows.Forms.CheckBox();
            this.gbDropBox = new System.Windows.Forms.GroupBox();
            this.cbCloseDropBox = new System.Windows.Forms.CheckBox();
            this.gbAppearance = new System.Windows.Forms.GroupBox();
            this.cbCompleteSound = new System.Windows.Forms.CheckBox();
            this.chkCaptureFallback = new System.Windows.Forms.CheckBox();
            this.cbShowUploadDuration = new System.Windows.Forms.CheckBox();
            this.chkBalloonTipOpenLink = new System.Windows.Forms.CheckBox();
            this.cbShowPopup = new System.Windows.Forms.CheckBox();
            this.lblTrayFlash = new System.Windows.Forms.Label();
            this.nudFlashIconCount = new System.Windows.Forms.NumericUpDown();
            this.tpAdvPaths = new System.Windows.Forms.TabPage();
            this.gbRoot = new System.Windows.Forms.GroupBox();
            this.btnViewRootDir = new System.Windows.Forms.Button();
            this.btnBrowseRootDir = new System.Windows.Forms.Button();
            this.txtRootFolder = new System.Windows.Forms.TextBox();
            this.gbSaveLoc = new System.Windows.Forms.GroupBox();
            this.btnMoveImageFiles = new System.Windows.Forms.Button();
            this.lblImagesFolderPattern = new System.Windows.Forms.Label();
            this.lblImagesFolderPatternPreview = new System.Windows.Forms.Label();
            this.txtImagesFolderPattern = new System.Windows.Forms.TextBox();
            this.cbDeleteLocal = new System.Windows.Forms.CheckBox();
            this.btnViewImagesDir = new System.Windows.Forms.Button();
            this.txtImagesDir = new System.Windows.Forms.TextBox();
            this.gbSettingsExportImport = new System.Windows.Forms.GroupBox();
            this.txtSettingsDir = new System.Windows.Forms.TextBox();
            this.btnSettingsDefault = new System.Windows.Forms.Button();
            this.btnSettingsExport = new System.Windows.Forms.Button();
            this.btnFTPExport = new System.Windows.Forms.Button();
            this.btnFTPImport = new System.Windows.Forms.Button();
            this.btnViewSettingsDir = new System.Windows.Forms.Button();
            this.btnSettingsImport = new System.Windows.Forms.Button();
            this.gbRemoteDirCache = new System.Windows.Forms.GroupBox();
            this.btnViewCacheDir = new System.Windows.Forms.Button();
            this.lblCacheSize = new System.Windows.Forms.Label();
            this.lblMebibytes = new System.Windows.Forms.Label();
            this.nudCacheSize = new System.Windows.Forms.NumericUpDown();
            this.txtCacheDir = new System.Windows.Forms.TextBox();
            this.tpAdvDebug = new System.Windows.Forms.TabPage();
            this.gbStatistics = new System.Windows.Forms.GroupBox();
            this.cboDebugAutoScroll = new System.Windows.Forms.CheckBox();
            this.btnDebugStart = new System.Windows.Forms.Button();
            this.btnCopyStats = new System.Windows.Forms.Button();
            this.txtDebugInfo = new System.Windows.Forms.TextBox();
            this.gbLastSource = new System.Windows.Forms.GroupBox();
            this.btnOpenSourceString = new System.Windows.Forms.Button();
            this.btnOpenSourceText = new System.Windows.Forms.Button();
            this.btnOpenSourceBrowser = new System.Windows.Forms.Button();
            this.tpOptionsAdv = new System.Windows.Forms.TabPage();
            this.pgApp = new System.Windows.Forms.PropertyGrid();
            this.tpUploadText = new System.Windows.Forms.TabPage();
            this.txtTextUploaderContent = new System.Windows.Forms.TextBox();
            this.btnUploadText = new System.Windows.Forms.Button();
            this.btnUploadTextClipboard = new System.Windows.Forms.Button();
            this.btnUploadTextClipboardFile = new System.Windows.Forms.Button();
            this.ttZScreen = new System.Windows.Forms.ToolTip(this.components);
            this.cmTray.SuspendLayout();
            this.cmsHistory.SuspendLayout();
            this.tcApp.SuspendLayout();
            this.tpMain.SuspendLayout();
            this.gbImageSettings.SuspendLayout();
            this.gbMainOptions.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbLogo)).BeginInit();
            this.tpDestinations.SuspendLayout();
            this.tcAccounts.SuspendLayout();
            this.tpFTP.SuspendLayout();
            this.gbFTPSettings.SuspendLayout();
            this.tpTinyPic.SuspendLayout();
            this.gbTinyPic.SuspendLayout();
            this.tpImageShack.SuspendLayout();
            this.gbImageShack.SuspendLayout();
            this.tpTwitter.SuspendLayout();
            this.tcTwitter.SuspendLayout();
            this.tpTwitPic.SuspendLayout();
            this.tpYfrog.SuspendLayout();
            this.gbTwitterAccount.SuspendLayout();
            this.tpImageBam.SuspendLayout();
            this.gbImageBamGalleries.SuspendLayout();
            this.gbImageBamLinks.SuspendLayout();
            this.gbImageBamApiKeys.SuspendLayout();
            this.tpRapidShare.SuspendLayout();
            this.tpSendSpace.SuspendLayout();
            this.tpMindTouch.SuspendLayout();
            this.gbMindTouchOptions.SuspendLayout();
            this.tpFlickr.SuspendLayout();
            this.tpHotkeys.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvHotkeys)).BeginInit();
            this.tpScreenshots.SuspendLayout();
            this.tcScreenshots.SuspendLayout();
            this.tpCropShot.SuspendLayout();
            this.gbDynamicRegionBorderColorSettings.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudCropRegionStep)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudCropHueRange)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudCropRegionInterval)).BeginInit();
            this.gbDynamicCrosshair.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudCropCrosshairInterval)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudCropCrosshairStep)).BeginInit();
            this.gpCropRegion.SuspendLayout();
            this.gbCropRegionSettings.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbCropBorderColor)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudCropBorderSize)).BeginInit();
            this.gbCrosshairSettings.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbCropCrosshairColor)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudCrosshairLineCount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudCrosshairLineSize)).BeginInit();
            this.gbGridMode.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudCropGridHeight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudCropGridWidth)).BeginInit();
            this.tpSelectedWindow.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudSelectedWindowHueRange)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudSelectedWindowRegionStep)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudSelectedWindowRegionInterval)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudSelectedWindowBorderSize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbSelectedWindowBorderColor)).BeginInit();
            this.tpWatermark.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbWatermarkShow)).BeginInit();
            this.gbWatermarkGeneral.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudWatermarkOffset)).BeginInit();
            this.tcWatermark.SuspendLayout();
            this.tpWatermarkText.SuspendLayout();
            this.gbWatermarkBackground.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackWatermarkBackgroundTrans)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudWatermarkBackTrans)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudWatermarkCornerRadius)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbWatermarkGradient1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbWatermarkBorderColor)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbWatermarkGradient2)).BeginInit();
            this.gbWatermarkText.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackWatermarkFontTrans)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudWatermarkFontTrans)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbWatermarkFontColor)).BeginInit();
            this.tpWatermarkImage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudWatermarkImageScale)).BeginInit();
            this.tpFileNaming.SuspendLayout();
            this.gbOthersNaming.SuspendLayout();
            this.gbCodeTitle.SuspendLayout();
            this.gbActiveWindowNaming.SuspendLayout();
            this.tpCaptureQuality.SuspendLayout();
            this.gbImageSize.SuspendLayout();
            this.gbPictureQuality.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudSwitchAfter)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudImageQuality)).BeginInit();
            this.tpEditors.SuspendLayout();
            this.tcEditors.SuspendLayout();
            this.tpEditorsImages.SuspendLayout();
            this.gbImageEditorSettings.SuspendLayout();
            this.tpImageHosting.SuspendLayout();
            this.tcImages.SuspendLayout();
            this.tpImageUploaders.SuspendLayout();
            this.gbImageUploadRetry.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudUploadDurationLimit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudErrorRetry)).BeginInit();
            this.gbImageUploaderOptions.SuspendLayout();
            this.tpCustomUploaders.SuspendLayout();
            this.gbImageUploaders.SuspendLayout();
            this.gbRegexp.SuspendLayout();
            this.gbArguments.SuspendLayout();
            this.tpWebPageUpload.SuspendLayout();
            this.pWebPageImage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbWebPageImage)).BeginInit();
            this.tpTextServices.SuspendLayout();
            this.tcTextUploaders.SuspendLayout();
            this.tpTextUploaders.SuspendLayout();
            this.tpURLShorteners.SuspendLayout();
            this.tpTreeGUI.SuspendLayout();
            this.tpTranslator.SuspendLayout();
            this.tpHistory.SuspendLayout();
            this.tcHistory.SuspendLayout();
            this.tpHistoryList.SuspendLayout();
            this.tlpHistory.SuspendLayout();
            this.tlpHistoryControls.SuspendLayout();
            this.panelControls.SuspendLayout();
            this.panelPreview.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbPreview)).BeginInit();
            this.tpHistorySettings.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudHistoryMaxItems)).BeginInit();
            this.tpOptions.SuspendLayout();
            this.tcOptions.SuspendLayout();
            this.tpGeneral.SuspendLayout();
            this.gbUpdates.SuspendLayout();
            this.gbMisc.SuspendLayout();
            this.tpProxy.SuspendLayout();
            this.gpProxySettings.SuspendLayout();
            this.tpInteraction.SuspendLayout();
            this.gbActionsToolbarSettings.SuspendLayout();
            this.gbDropBox.SuspendLayout();
            this.gbAppearance.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudFlashIconCount)).BeginInit();
            this.tpAdvPaths.SuspendLayout();
            this.gbRoot.SuspendLayout();
            this.gbSaveLoc.SuspendLayout();
            this.gbSettingsExportImport.SuspendLayout();
            this.gbRemoteDirCache.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudCacheSize)).BeginInit();
            this.tpAdvDebug.SuspendLayout();
            this.gbStatistics.SuspendLayout();
            this.gbLastSource.SuspendLayout();
            this.tpOptionsAdv.SuspendLayout();
            this.SuspendLayout();
            // 
            // niTray
            // 
            this.niTray.ContextMenuStrip = this.cmTray;
            this.niTray.Icon = ((System.Drawing.Icon)(resources.GetObject("niTray.Icon")));
            this.niTray.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.niTray_MouseDoubleClick);
            // 
            // cmTray
            // 
            this.cmTray.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiTabs,
            this.toolStripSeparator4,
            this.tsmImageDest,
            this.tsmEditinImageSoftware,
            this.tsmCopytoClipboardMode,
            this.toolStripSeparator3,
            this.tsmFTPClient,
            this.tsmViewRemoteDirectory,
            this.tsmViewLocalDirectory,
            this.toolStripSeparator1,
            this.tsmActions,
            this.tsmQuickActions,
            this.tsmQuickOptions,
            this.toolStripSeparator7,
            this.tsmHelp,
            this.tsmExitZScreen});
            this.cmTray.Name = "cmTray";
            this.cmTray.Size = new System.Drawing.Size(206, 292);
            // 
            // tsmiTabs
            // 
            this.tsmiTabs.DoubleClickEnabled = true;
            this.tsmiTabs.Image = global::ZSS.Properties.Resources.wrench;
            this.tsmiTabs.Name = "tsmiTabs";
            this.tsmiTabs.Size = new System.Drawing.Size(205, 22);
            this.tsmiTabs.Text = "View Settings Menu...";
            this.tsmiTabs.Click += new System.EventHandler(this.tsmSettings_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(202, 6);
            // 
            // tsmImageDest
            // 
            this.tsmImageDest.Image = global::ZSS.Properties.Resources.picture_go;
            this.tsmImageDest.Name = "tsmImageDest";
            this.tsmImageDest.Size = new System.Drawing.Size(205, 22);
            this.tsmImageDest.Text = "Send Image To";
            // 
            // tsmEditinImageSoftware
            // 
            this.tsmEditinImageSoftware.Image = global::ZSS.Properties.Resources.picture_edit;
            this.tsmEditinImageSoftware.Name = "tsmEditinImageSoftware";
            this.tsmEditinImageSoftware.Size = new System.Drawing.Size(205, 22);
            this.tsmEditinImageSoftware.Text = "Edit in Image Software";
            // 
            // tsmCopytoClipboardMode
            // 
            this.tsmCopytoClipboardMode.Image = global::ZSS.Properties.Resources.page_copy;
            this.tsmCopytoClipboardMode.Name = "tsmCopytoClipboardMode";
            this.tsmCopytoClipboardMode.Size = new System.Drawing.Size(205, 22);
            this.tsmCopytoClipboardMode.Text = "Copy to Clipboard Mode";
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(202, 6);
            // 
            // tsmFTPClient
            // 
            this.tsmFTPClient.Image = global::ZSS.Properties.Resources.server_edit;
            this.tsmFTPClient.Name = "tsmFTPClient";
            this.tsmFTPClient.Size = new System.Drawing.Size(205, 22);
            this.tsmFTPClient.Text = "FTP &Client...";
            this.tsmFTPClient.Click += new System.EventHandler(this.tsmFTPClient_Click);
            // 
            // tsmViewRemoteDirectory
            // 
            this.tsmViewRemoteDirectory.Image = global::ZSS.Properties.Resources.server;
            this.tsmViewRemoteDirectory.Name = "tsmViewRemoteDirectory";
            this.tsmViewRemoteDirectory.Size = new System.Drawing.Size(205, 22);
            this.tsmViewRemoteDirectory.Text = "&FTP Viewer...";
            this.tsmViewRemoteDirectory.Click += new System.EventHandler(this.tsmViewRemote_Click);
            // 
            // tsmViewLocalDirectory
            // 
            this.tsmViewLocalDirectory.Image = global::ZSS.Properties.Resources.folder_picture;
            this.tsmViewLocalDirectory.Name = "tsmViewLocalDirectory";
            this.tsmViewLocalDirectory.Size = new System.Drawing.Size(205, 22);
            this.tsmViewLocalDirectory.Text = "Images Directory...";
            this.tsmViewLocalDirectory.Click += new System.EventHandler(this.tsmViewDirectory_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(202, 6);
            // 
            // tsmActions
            // 
            this.tsmActions.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmEntireScreen,
            this.tsmSelectedWindow,
            this.tsmCropShot,
            this.tsmLastCropShot,
            this.autoScreenshotsToolStripMenuItem,
            this.toolStripSeparator6,
            this.tsmClipboardUpload,
            this.tsmDragDropWindow,
            this.tsmLanguageTranslator,
            this.tsmScreenColorPicker});
            this.tsmActions.Image = global::ZSS.Properties.Resources.lightning;
            this.tsmActions.Name = "tsmActions";
            this.tsmActions.Size = new System.Drawing.Size(205, 22);
            this.tsmActions.Text = "Quick &Actions";
            // 
            // tsmEntireScreen
            // 
            this.tsmEntireScreen.Image = global::ZSS.Properties.Resources.monitor;
            this.tsmEntireScreen.Name = "tsmEntireScreen";
            this.tsmEntireScreen.Size = new System.Drawing.Size(197, 22);
            this.tsmEntireScreen.Text = "Entire Screen";
            this.tsmEntireScreen.Click += new System.EventHandler(this.entireScreenToolStripMenuItem_Click);
            // 
            // tsmSelectedWindow
            // 
            this.tsmSelectedWindow.Image = global::ZSS.Properties.Resources.application_double;
            this.tsmSelectedWindow.Name = "tsmSelectedWindow";
            this.tsmSelectedWindow.Size = new System.Drawing.Size(197, 22);
            this.tsmSelectedWindow.Text = "Selected Window...";
            this.tsmSelectedWindow.Click += new System.EventHandler(this.selectedWindowToolStripMenuItem_Click);
            // 
            // tsmCropShot
            // 
            this.tsmCropShot.Image = global::ZSS.Properties.Resources.shape_square;
            this.tsmCropShot.Name = "tsmCropShot";
            this.tsmCropShot.Size = new System.Drawing.Size(197, 22);
            this.tsmCropShot.Text = "Crop Shot...";
            this.tsmCropShot.Click += new System.EventHandler(this.rectangularRegionToolStripMenuItem_Click);
            // 
            // tsmLastCropShot
            // 
            this.tsmLastCropShot.Image = global::ZSS.Properties.Resources.shape_square_go;
            this.tsmLastCropShot.Name = "tsmLastCropShot";
            this.tsmLastCropShot.Size = new System.Drawing.Size(197, 22);
            this.tsmLastCropShot.Text = "Last Crop Shot";
            this.tsmLastCropShot.Click += new System.EventHandler(this.lastRectangularRegionToolStripMenuItem_Click);
            // 
            // autoScreenshotsToolStripMenuItem
            // 
            this.autoScreenshotsToolStripMenuItem.Image = global::ZSS.Properties.Resources.images_stack;
            this.autoScreenshotsToolStripMenuItem.Name = "autoScreenshotsToolStripMenuItem";
            this.autoScreenshotsToolStripMenuItem.Size = new System.Drawing.Size(197, 22);
            this.autoScreenshotsToolStripMenuItem.Text = "Auto Capture...";
            this.autoScreenshotsToolStripMenuItem.Click += new System.EventHandler(this.autoScreenshotsToolStripMenuItem_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(194, 6);
            // 
            // tsmClipboardUpload
            // 
            this.tsmClipboardUpload.Image = global::ZSS.Properties.Resources.images;
            this.tsmClipboardUpload.Name = "tsmClipboardUpload";
            this.tsmClipboardUpload.Size = new System.Drawing.Size(197, 22);
            this.tsmClipboardUpload.Text = "Clipboard Upload";
            this.tsmClipboardUpload.Click += new System.EventHandler(this.tsmUploadFromClipboard_Click);
            // 
            // tsmDragDropWindow
            // 
            this.tsmDragDropWindow.Image = global::ZSS.Properties.Resources.shape_move_backwards;
            this.tsmDragDropWindow.Name = "tsmDragDropWindow";
            this.tsmDragDropWindow.Size = new System.Drawing.Size(197, 22);
            this.tsmDragDropWindow.Text = "Drag && Drop Window...";
            this.tsmDragDropWindow.Click += new System.EventHandler(this.tsmDropWindow_Click);
            // 
            // tsmLanguageTranslator
            // 
            this.tsmLanguageTranslator.Image = global::ZSS.Properties.Resources.comments;
            this.tsmLanguageTranslator.Name = "tsmLanguageTranslator";
            this.tsmLanguageTranslator.Size = new System.Drawing.Size(197, 22);
            this.tsmLanguageTranslator.Text = "Language Translator";
            this.tsmLanguageTranslator.Click += new System.EventHandler(this.languageTranslatorToolStripMenuItem_Click);
            // 
            // tsmScreenColorPicker
            // 
            this.tsmScreenColorPicker.Image = global::ZSS.Properties.Resources.color_wheel;
            this.tsmScreenColorPicker.Name = "tsmScreenColorPicker";
            this.tsmScreenColorPicker.Size = new System.Drawing.Size(197, 22);
            this.tsmScreenColorPicker.Text = "Screen Color Picker...";
            this.tsmScreenColorPicker.Click += new System.EventHandler(this.screenColorPickerToolStripMenuItem_Click);
            // 
            // tsmQuickActions
            // 
            this.tsmQuickActions.Image = global::ZSS.Properties.Resources.application_lightning;
            this.tsmQuickActions.Name = "tsmQuickActions";
            this.tsmQuickActions.Size = new System.Drawing.Size(205, 22);
            this.tsmQuickActions.Text = "Actions Toolbar...";
            this.tsmQuickActions.Click += new System.EventHandler(this.tsmQuickActions_Click);
            // 
            // tsmQuickOptions
            // 
            this.tsmQuickOptions.Image = global::ZSS.Properties.Resources.application_edit;
            this.tsmQuickOptions.Name = "tsmQuickOptions";
            this.tsmQuickOptions.Size = new System.Drawing.Size(205, 22);
            this.tsmQuickOptions.Text = "&Quick Options...";
            this.tsmQuickOptions.Click += new System.EventHandler(this.tsmQuickOptions_Click);
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(202, 6);
            // 
            // tsmHelp
            // 
            this.tsmHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmLicense,
            this.tsmVersionHistory,
            this.tsmAbout});
            this.tsmHelp.Image = global::ZSS.Properties.Resources.help;
            this.tsmHelp.Name = "tsmHelp";
            this.tsmHelp.Size = new System.Drawing.Size(205, 22);
            this.tsmHelp.Text = "&Help";
            // 
            // tsmLicense
            // 
            this.tsmLicense.Image = global::ZSS.Properties.Resources.note_error;
            this.tsmLicense.Name = "tsmLicense";
            this.tsmLicense.Size = new System.Drawing.Size(163, 22);
            this.tsmLicense.Text = "License...";
            this.tsmLicense.Click += new System.EventHandler(this.tsmLic_Click);
            // 
            // tsmVersionHistory
            // 
            this.tsmVersionHistory.Image = global::ZSS.Properties.Resources.page_white_text;
            this.tsmVersionHistory.Name = "tsmVersionHistory";
            this.tsmVersionHistory.Size = new System.Drawing.Size(163, 22);
            this.tsmVersionHistory.Text = "&Version History...";
            this.tsmVersionHistory.Click += new System.EventHandler(this.cmVersionHistory_Click);
            // 
            // tsmAbout
            // 
            this.tsmAbout.Image = global::ZSS.Properties.Resources.information;
            this.tsmAbout.Name = "tsmAbout";
            this.tsmAbout.Size = new System.Drawing.Size(163, 22);
            this.tsmAbout.Text = "About...";
            this.tsmAbout.Click += new System.EventHandler(this.tsmAboutMain_Click);
            // 
            // tsmExitZScreen
            // 
            this.tsmExitZScreen.Image = global::ZSS.Properties.Resources.cross;
            this.tsmExitZScreen.Name = "tsmExitZScreen";
            this.tsmExitZScreen.Size = new System.Drawing.Size(205, 22);
            this.tsmExitZScreen.Text = "Exit ZScreen";
            this.tsmExitZScreen.Click += new System.EventHandler(this.exitZScreenToolStripMenuItem_Click);
            // 
            // cmsHistory
            // 
            this.cmsHistory.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmCopyCbHistory,
            this.copyImageToolStripMenuItem,
            this.toolStripSeparator2,
            this.openLocalFileToolStripMenuItem,
            this.browseURLToolStripMenuItem,
            this.openSourceToolStripMenuItem,
            this.toolStripSeparator8,
            this.cmsRetryUpload,
            this.deleteToolStripMenuItem});
            this.cmsHistory.Name = "cmsHistory";
            this.cmsHistory.Size = new System.Drawing.Size(165, 170);
            // 
            // tsmCopyCbHistory
            // 
            this.tsmCopyCbHistory.Name = "tsmCopyCbHistory";
            this.tsmCopyCbHistory.Size = new System.Drawing.Size(164, 22);
            this.tsmCopyCbHistory.Text = "&Copy Link";
            // 
            // copyImageToolStripMenuItem
            // 
            this.copyImageToolStripMenuItem.Name = "copyImageToolStripMenuItem";
            this.copyImageToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
            this.copyImageToolStripMenuItem.Text = "Copy &Image";
            this.copyImageToolStripMenuItem.Click += new System.EventHandler(this.copyImageToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(161, 6);
            // 
            // openLocalFileToolStripMenuItem
            // 
            this.openLocalFileToolStripMenuItem.Name = "openLocalFileToolStripMenuItem";
            this.openLocalFileToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
            this.openLocalFileToolStripMenuItem.Text = "Open Local File";
            this.openLocalFileToolStripMenuItem.Click += new System.EventHandler(this.openLocalFileToolStripMenuItem_Click);
            // 
            // browseURLToolStripMenuItem
            // 
            this.browseURLToolStripMenuItem.Name = "browseURLToolStripMenuItem";
            this.browseURLToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
            this.browseURLToolStripMenuItem.Text = "Browse URL";
            this.browseURLToolStripMenuItem.Click += new System.EventHandler(this.browseURLToolStripMenuItem_Click);
            // 
            // openSourceToolStripMenuItem
            // 
            this.openSourceToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.openSourceInDefaultWebBrowserHTMLToolStripMenuItem,
            this.copySourceToClipboardStringToolStripMenuItem});
            this.openSourceToolStripMenuItem.Name = "openSourceToolStripMenuItem";
            this.openSourceToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
            this.openSourceToolStripMenuItem.Text = "Open Source";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(214, 22);
            this.openToolStripMenuItem.Text = "Open Source in Text Editor";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // openSourceInDefaultWebBrowserHTMLToolStripMenuItem
            // 
            this.openSourceInDefaultWebBrowserHTMLToolStripMenuItem.Name = "openSourceInDefaultWebBrowserHTMLToolStripMenuItem";
            this.openSourceInDefaultWebBrowserHTMLToolStripMenuItem.Size = new System.Drawing.Size(214, 22);
            this.openSourceInDefaultWebBrowserHTMLToolStripMenuItem.Text = "Open Source in Browser";
            this.openSourceInDefaultWebBrowserHTMLToolStripMenuItem.Click += new System.EventHandler(this.openSourceInDefaultWebBrowserHTMLToolStripMenuItem_Click);
            // 
            // copySourceToClipboardStringToolStripMenuItem
            // 
            this.copySourceToClipboardStringToolStripMenuItem.Name = "copySourceToClipboardStringToolStripMenuItem";
            this.copySourceToClipboardStringToolStripMenuItem.Size = new System.Drawing.Size(214, 22);
            this.copySourceToClipboardStringToolStripMenuItem.Text = "Copy Source to Clipboard";
            this.copySourceToClipboardStringToolStripMenuItem.Click += new System.EventHandler(this.copySourceToClipboardStringToolStripMenuItem_Click);
            // 
            // toolStripSeparator8
            // 
            this.toolStripSeparator8.Name = "toolStripSeparator8";
            this.toolStripSeparator8.Size = new System.Drawing.Size(161, 6);
            // 
            // cmsRetryUpload
            // 
            this.cmsRetryUpload.Name = "cmsRetryUpload";
            this.cmsRetryUpload.Size = new System.Drawing.Size(164, 22);
            this.cmsRetryUpload.Text = "Retry Upload";
            this.cmsRetryUpload.Click += new System.EventHandler(this.cmsRetryUpload_Click);
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
            this.deleteToolStripMenuItem.Text = "&Delete Local Files";
            this.deleteToolStripMenuItem.Click += new System.EventHandler(this.deleteToolStripMenuItem_Click);
            // 
            // ilApp
            // 
            this.ilApp.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ilApp.ImageStream")));
            this.ilApp.TransparentColor = System.Drawing.Color.Transparent;
            this.ilApp.Images.SetKeyName(0, "application_form.png");
            this.ilApp.Images.SetKeyName(1, "server.png");
            this.ilApp.Images.SetKeyName(2, "keyboard.png");
            this.ilApp.Images.SetKeyName(3, "monitor.png");
            this.ilApp.Images.SetKeyName(4, "picture_edit.png");
            this.ilApp.Images.SetKeyName(5, "picture_go.png");
            this.ilApp.Images.SetKeyName(6, "text_signature.png");
            this.ilApp.Images.SetKeyName(7, "comments.png");
            this.ilApp.Images.SetKeyName(8, "pictures.png");
            this.ilApp.Images.SetKeyName(9, "application_edit.png");
            this.ilApp.Images.SetKeyName(10, "shape_square.png");
            this.ilApp.Images.SetKeyName(11, "application_double.png");
            this.ilApp.Images.SetKeyName(12, "tag_blue_edit.png");
            this.ilApp.Images.SetKeyName(13, "Twitter.ico");
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(179, 6);
            // 
            // tmrApp
            // 
            this.tmrApp.Enabled = true;
            this.tmrApp.Interval = 21600000;
            this.tmrApp.Tick += new System.EventHandler(this.tmrApp_Tick);
            // 
            // tcApp
            // 
            this.tcApp.Controls.Add(this.tpMain);
            this.tcApp.Controls.Add(this.tpDestinations);
            this.tcApp.Controls.Add(this.tpHotkeys);
            this.tcApp.Controls.Add(this.tpScreenshots);
            this.tcApp.Controls.Add(this.tpEditors);
            this.tcApp.Controls.Add(this.tpImageHosting);
            this.tcApp.Controls.Add(this.tpTextServices);
            this.tcApp.Controls.Add(this.tpTranslator);
            this.tcApp.Controls.Add(this.tpHistory);
            this.tcApp.Controls.Add(this.tpOptions);
            this.tcApp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcApp.ImageList = this.ilApp;
            this.tcApp.Location = new System.Drawing.Point(2, 2);
            this.tcApp.Name = "tcApp";
            this.tcApp.SelectedIndex = 0;
            this.tcApp.Size = new System.Drawing.Size(813, 469);
            this.tcApp.TabIndex = 74;
            this.tcApp.SelectedIndexChanged += new System.EventHandler(this.tcApp_SelectedIndexChanged);
            // 
            // tpMain
            // 
            this.tpMain.AllowDrop = true;
            this.tpMain.Controls.Add(this.gbImageSettings);
            this.tpMain.Controls.Add(this.llProjectPage);
            this.tpMain.Controls.Add(this.llWebsite);
            this.tpMain.Controls.Add(this.llblBugReports);
            this.tpMain.Controls.Add(this.gbMainOptions);
            this.tpMain.Controls.Add(this.lblLogo);
            this.tpMain.Controls.Add(this.pbLogo);
            this.tpMain.ImageKey = "application_form.png";
            this.tpMain.Location = new System.Drawing.Point(4, 23);
            this.tpMain.Name = "tpMain";
            this.tpMain.Padding = new System.Windows.Forms.Padding(3);
            this.tpMain.Size = new System.Drawing.Size(805, 442);
            this.tpMain.TabIndex = 0;
            this.tpMain.Text = "Main";
            this.tpMain.UseVisualStyleBackColor = true;
            // 
            // gbImageSettings
            // 
            this.gbImageSettings.Controls.Add(this.lblScreenshotDelay);
            this.gbImageSettings.Controls.Add(this.nudtScreenshotDelay);
            this.gbImageSettings.Controls.Add(this.lblCopytoClipboard);
            this.gbImageSettings.Controls.Add(this.cboClipboardTextMode);
            this.gbImageSettings.Controls.Add(this.cbShowCursor);
            this.gbImageSettings.Controls.Add(this.chkManualNaming);
            this.gbImageSettings.Location = new System.Drawing.Point(48, 200);
            this.gbImageSettings.Name = "gbImageSettings";
            this.gbImageSettings.Size = new System.Drawing.Size(360, 144);
            this.gbImageSettings.TabIndex = 123;
            this.gbImageSettings.TabStop = false;
            this.gbImageSettings.Text = "Image Settings";
            // 
            // lblScreenshotDelay
            // 
            this.lblScreenshotDelay.AutoSize = true;
            this.lblScreenshotDelay.Location = new System.Drawing.Point(16, 24);
            this.lblScreenshotDelay.Name = "lblScreenshotDelay";
            this.lblScreenshotDelay.Size = new System.Drawing.Size(94, 13);
            this.lblScreenshotDelay.TabIndex = 122;
            this.lblScreenshotDelay.Text = "Screenshot Delay:";
            // 
            // nudtScreenshotDelay
            // 
            this.nudtScreenshotDelay.Location = new System.Drawing.Point(112, 18);
            this.nudtScreenshotDelay.Name = "nudtScreenshotDelay";
            this.nudtScreenshotDelay.RealValue = ((long)(0));
            this.nudtScreenshotDelay.Size = new System.Drawing.Size(234, 24);
            this.nudtScreenshotDelay.TabIndex = 121;
            this.nudtScreenshotDelay.Tag = "Test";
            this.nudtScreenshotDelay.Time = ZScreenLib.Times.Milliseconds;
            this.ttZScreen.SetToolTip(this.nudtScreenshotDelay, "Specify the amount of time to wait before taking a screenshot.");
            this.nudtScreenshotDelay.Value = ((long)(0));
            this.nudtScreenshotDelay.ValueChanged += new System.EventHandler(this.numericUpDownTimer1_ValueChanged);
            this.nudtScreenshotDelay.MouseHover += new System.EventHandler(this.nudtScreenshotDelay_MouseHover);
            this.nudtScreenshotDelay.SelectedIndexChanged += new System.EventHandler(this.nudtScreenshotDelay_SelectedIndexChanged);
            // 
            // lblCopytoClipboard
            // 
            this.lblCopytoClipboard.AutoSize = true;
            this.lblCopytoClipboard.Location = new System.Drawing.Point(16, 52);
            this.lblCopytoClipboard.Name = "lblCopytoClipboard";
            this.lblCopytoClipboard.Size = new System.Drawing.Size(93, 13);
            this.lblCopytoClipboard.TabIndex = 117;
            this.lblCopytoClipboard.Text = "Copy to Clipboard:";
            // 
            // cboClipboardTextMode
            // 
            this.cboClipboardTextMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboClipboardTextMode.FormattingEnabled = true;
            this.cboClipboardTextMode.Location = new System.Drawing.Point(114, 48);
            this.cboClipboardTextMode.Name = "cboClipboardTextMode";
            this.cboClipboardTextMode.Size = new System.Drawing.Size(232, 21);
            this.cboClipboardTextMode.TabIndex = 116;
            this.ttZScreen.SetToolTip(this.cboClipboardTextMode, "Specify the way in which screenshot links\r\nshould be added to your clipboard.\r\nDe" +
                    "fault: Full Image.");
            this.cboClipboardTextMode.SelectedIndexChanged += new System.EventHandler(this.cboClipboardTextMode_SelectedIndexChanged);
            // 
            // cbShowCursor
            // 
            this.cbShowCursor.AutoSize = true;
            this.cbShowCursor.Location = new System.Drawing.Point(16, 111);
            this.cbShowCursor.Name = "cbShowCursor";
            this.cbShowCursor.Size = new System.Drawing.Size(159, 17);
            this.cbShowCursor.TabIndex = 8;
            this.cbShowCursor.Text = "Show Cursor in Screenshots";
            this.ttZScreen.SetToolTip(this.cbShowCursor, "When enabled your mouse cursor icon will be captured \r\nas it appeared when the sc" +
                    "reenshot was taken.");
            this.cbShowCursor.UseVisualStyleBackColor = true;
            this.cbShowCursor.CheckedChanged += new System.EventHandler(this.cbShowCursor_CheckedChanged);
            // 
            // chkManualNaming
            // 
            this.chkManualNaming.AutoSize = true;
            this.chkManualNaming.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.chkManualNaming.Location = new System.Drawing.Point(16, 88);
            this.chkManualNaming.Name = "chkManualNaming";
            this.chkManualNaming.Size = new System.Drawing.Size(124, 17);
            this.chkManualNaming.TabIndex = 112;
            this.chkManualNaming.Text = "Prompt for File Name";
            this.ttZScreen.SetToolTip(this.chkManualNaming, "When enabled a prompt will be displayed when each\r\nscreenshot is taken allowing y" +
                    "ou to manually specify a filename.");
            this.chkManualNaming.UseVisualStyleBackColor = true;
            this.chkManualNaming.CheckedChanged += new System.EventHandler(this.chkManualNaming_CheckedChanged);
            // 
            // llProjectPage
            // 
            this.llProjectPage.AutoSize = true;
            this.llProjectPage.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline;
            this.llProjectPage.Location = new System.Drawing.Point(440, 360);
            this.llProjectPage.Name = "llProjectPage";
            this.llProjectPage.Size = new System.Drawing.Size(68, 13);
            this.llProjectPage.TabIndex = 83;
            this.llProjectPage.TabStop = true;
            this.llProjectPage.Text = "Project Page";
            this.ttZScreen.SetToolTip(this.llProjectPage, "View ZScreen\'s project page on the web.");
            this.llProjectPage.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llProjectPage_LinkClicked);
            // 
            // llWebsite
            // 
            this.llWebsite.AutoSize = true;
            this.llWebsite.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline;
            this.llWebsite.Location = new System.Drawing.Point(688, 360);
            this.llWebsite.Name = "llWebsite";
            this.llWebsite.Size = new System.Drawing.Size(72, 13);
            this.llWebsite.TabIndex = 82;
            this.llWebsite.TabStop = true;
            this.llWebsite.Text = "BrandonZ.net";
            this.ttZScreen.SetToolTip(this.llWebsite, "Visit the home of ZScreen.");
            this.llWebsite.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llWebsite_LinkClicked);
            // 
            // llblBugReports
            // 
            this.llblBugReports.AutoSize = true;
            this.llblBugReports.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline;
            this.llblBugReports.Location = new System.Drawing.Point(552, 360);
            this.llblBugReports.Name = "llblBugReports";
            this.llblBugReports.Size = new System.Drawing.Size(100, 13);
            this.llblBugReports.TabIndex = 81;
            this.llblBugReports.TabStop = true;
            this.llblBugReports.Text = "Bugs/Suggestions?";
            this.ttZScreen.SetToolTip(this.llblBugReports, "Have a bug report or a suggestion for us?\r\nCome visit our website and create an i" +
                    "ssue.");
            this.llblBugReports.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llblBugReports_LinkClicked);
            // 
            // gbMainOptions
            // 
            this.gbMainOptions.BackColor = System.Drawing.Color.Transparent;
            this.gbMainOptions.Controls.Add(this.cboFileUploaders);
            this.gbMainOptions.Controls.Add(this.lblFileUploader);
            this.gbMainOptions.Controls.Add(this.cboURLShorteners);
            this.gbMainOptions.Controls.Add(this.lblURLShortener);
            this.gbMainOptions.Controls.Add(this.lblImageUploader);
            this.gbMainOptions.Controls.Add(this.lblTextUploader);
            this.gbMainOptions.Controls.Add(this.cboImageUploaders);
            this.gbMainOptions.Controls.Add(this.cboTextUploaders);
            this.gbMainOptions.Location = new System.Drawing.Point(48, 64);
            this.gbMainOptions.Name = "gbMainOptions";
            this.gbMainOptions.Size = new System.Drawing.Size(360, 128);
            this.gbMainOptions.TabIndex = 79;
            this.gbMainOptions.TabStop = false;
            this.gbMainOptions.Text = "Upload Destinations";
            // 
            // cboFileUploaders
            // 
            this.cboFileUploaders.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboFileUploaders.FormattingEnabled = true;
            this.cboFileUploaders.Location = new System.Drawing.Point(112, 72);
            this.cboFileUploaders.Name = "cboFileUploaders";
            this.cboFileUploaders.Size = new System.Drawing.Size(232, 21);
            this.cboFileUploaders.TabIndex = 126;
            this.ttZScreen.SetToolTip(this.cboFileUploaders, "Specify which File Uploader to use.\r\nTo change them see the Destinations tab.\r\nTh" +
                    "is option specifies the destination where files will be sent.");
            this.cboFileUploaders.SelectedIndexChanged += new System.EventHandler(this.cboFileUploaders_SelectedIndexChanged);
            // 
            // lblFileUploader
            // 
            this.lblFileUploader.AutoSize = true;
            this.lblFileUploader.Location = new System.Drawing.Point(32, 76);
            this.lblFileUploader.Name = "lblFileUploader";
            this.lblFileUploader.Size = new System.Drawing.Size(72, 13);
            this.lblFileUploader.TabIndex = 125;
            this.lblFileUploader.Text = "File Uploader:";
            // 
            // cboURLShorteners
            // 
            this.cboURLShorteners.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboURLShorteners.FormattingEnabled = true;
            this.cboURLShorteners.Location = new System.Drawing.Point(112, 96);
            this.cboURLShorteners.Name = "cboURLShorteners";
            this.cboURLShorteners.Size = new System.Drawing.Size(232, 21);
            this.cboURLShorteners.TabIndex = 124;
            this.ttZScreen.SetToolTip(this.cboURLShorteners, "Specify which URL Shortener to use.\r\nTo add/remove/change them see Text Services " +
                    "-> URL Shorteners tab.\r\nThis setting relies on Clipboard Upload which can be set" +
                    " in the Hotkeys tab.");
            this.cboURLShorteners.SelectedIndexChanged += new System.EventHandler(this.cboURLShorteners_SelectedIndexChanged);
            // 
            // lblURLShortener
            // 
            this.lblURLShortener.AutoSize = true;
            this.lblURLShortener.Location = new System.Drawing.Point(23, 99);
            this.lblURLShortener.Name = "lblURLShortener";
            this.lblURLShortener.Size = new System.Drawing.Size(81, 13);
            this.lblURLShortener.TabIndex = 123;
            this.lblURLShortener.Text = "URL Shortener:";
            // 
            // lblImageUploader
            // 
            this.lblImageUploader.AutoSize = true;
            this.lblImageUploader.Location = new System.Drawing.Point(19, 26);
            this.lblImageUploader.Name = "lblImageUploader";
            this.lblImageUploader.Size = new System.Drawing.Size(85, 13);
            this.lblImageUploader.TabIndex = 1;
            this.lblImageUploader.Text = "Image Uploader:";
            // 
            // lblTextUploader
            // 
            this.lblTextUploader.AutoSize = true;
            this.lblTextUploader.Location = new System.Drawing.Point(27, 51);
            this.lblTextUploader.Name = "lblTextUploader";
            this.lblTextUploader.Size = new System.Drawing.Size(77, 13);
            this.lblTextUploader.TabIndex = 122;
            this.lblTextUploader.Text = "Text Uploader:";
            // 
            // cboImageUploaders
            // 
            this.cboImageUploaders.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboImageUploaders.FormattingEnabled = true;
            this.cboImageUploaders.Location = new System.Drawing.Point(112, 24);
            this.cboImageUploaders.Name = "cboImageUploaders";
            this.cboImageUploaders.Size = new System.Drawing.Size(232, 21);
            this.cboImageUploaders.TabIndex = 0;
            this.ttZScreen.SetToolTip(this.cboImageUploaders, "Specify which Image Uploader to use.\r\nTo change them see the Destinations tab.\r\nT" +
                    "his option specifies the destination where images/screenshots will be sent.");
            this.cboImageUploaders.SelectedIndexChanged += new System.EventHandler(this.cboScreenshotDest_SelectedIndexChanged);
            // 
            // cboTextUploaders
            // 
            this.cboTextUploaders.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTextUploaders.FormattingEnabled = true;
            this.cboTextUploaders.Location = new System.Drawing.Point(112, 48);
            this.cboTextUploaders.Name = "cboTextUploaders";
            this.cboTextUploaders.Size = new System.Drawing.Size(232, 21);
            this.cboTextUploaders.TabIndex = 121;
            this.ttZScreen.SetToolTip(this.cboTextUploaders, "Specify which Text Uploader to use.\r\nTo add/remove/change them see Text Services " +
                    "-> Text Uploaders tab.\r\nThis setting relies on Clipboard Upload which can be set" +
                    " in the Hotkeys tab.");
            this.cboTextUploaders.SelectedIndexChanged += new System.EventHandler(this.cboTextDest_SelectedIndexChanged);
            // 
            // lblLogo
            // 
            this.lblLogo.BackColor = System.Drawing.Color.White;
            this.lblLogo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblLogo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblLogo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblLogo.ForeColor = System.Drawing.Color.Black;
            this.lblLogo.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblLogo.Location = new System.Drawing.Point(440, 312);
            this.lblLogo.Name = "lblLogo";
            this.lblLogo.Size = new System.Drawing.Size(326, 40);
            this.lblLogo.TabIndex = 74;
            this.lblLogo.Text = "ZScreen vW.X.Y.Z";
            this.lblLogo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblLogo.MouseLeave += new System.EventHandler(this.lblLogo_MouseLeave);
            this.lblLogo.Click += new System.EventHandler(this.lblLogo_Click);
            this.lblLogo.MouseEnter += new System.EventHandler(this.lblLogo_MouseEnter);
            // 
            // pbLogo
            // 
            this.pbLogo.Cursor = System.Windows.Forms.Cursors.Default;
            this.pbLogo.Image = ((System.Drawing.Image)(resources.GetObject("pbLogo.Image")));
            this.pbLogo.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.pbLogo.Location = new System.Drawing.Point(440, 56);
            this.pbLogo.Name = "pbLogo";
            this.pbLogo.Size = new System.Drawing.Size(324, 254);
            this.pbLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pbLogo.TabIndex = 72;
            this.pbLogo.TabStop = false;
            this.pbLogo.MouseLeave += new System.EventHandler(this.pbLogo_MouseLeave);
            this.pbLogo.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pbLogo_MouseClick);
            this.pbLogo.MouseEnter += new System.EventHandler(this.pbLogo_MouseEnter);
            // 
            // tpDestinations
            // 
            this.tpDestinations.Controls.Add(this.tcAccounts);
            this.tpDestinations.ImageKey = "server.png";
            this.tpDestinations.Location = new System.Drawing.Point(4, 23);
            this.tpDestinations.Name = "tpDestinations";
            this.tpDestinations.Padding = new System.Windows.Forms.Padding(3);
            this.tpDestinations.Size = new System.Drawing.Size(805, 442);
            this.tpDestinations.TabIndex = 12;
            this.tpDestinations.Text = "Destinations";
            this.tpDestinations.UseVisualStyleBackColor = true;
            // 
            // tcAccounts
            // 
            this.tcAccounts.Controls.Add(this.tpFTP);
            this.tcAccounts.Controls.Add(this.tpRapidShare);
            this.tcAccounts.Controls.Add(this.tpSendSpace);
            this.tcAccounts.Controls.Add(this.tpFlickr);
            this.tcAccounts.Controls.Add(this.tpImageShack);
            this.tcAccounts.Controls.Add(this.tpTinyPic);
            this.tcAccounts.Controls.Add(this.tpTwitter);
            this.tcAccounts.Controls.Add(this.tpImageBam);
            this.tcAccounts.Controls.Add(this.tpMindTouch);
            this.tcAccounts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcAccounts.ImageList = this.ilApp;
            this.tcAccounts.Location = new System.Drawing.Point(3, 3);
            this.tcAccounts.Name = "tcAccounts";
            this.tcAccounts.SelectedIndex = 0;
            this.tcAccounts.Size = new System.Drawing.Size(799, 436);
            this.tcAccounts.TabIndex = 0;
            // 
            // tpFTP
            // 
            this.tpFTP.Controls.Add(this.ucFTPAccounts);
            this.tpFTP.Controls.Add(this.gbFTPSettings);
            this.tpFTP.Location = new System.Drawing.Point(4, 23);
            this.tpFTP.Name = "tpFTP";
            this.tpFTP.Padding = new System.Windows.Forms.Padding(3);
            this.tpFTP.Size = new System.Drawing.Size(791, 409);
            this.tpFTP.TabIndex = 5;
            this.tpFTP.Text = "FTP";
            this.tpFTP.UseVisualStyleBackColor = true;
            // 
            // ucFTPAccounts
            // 
            this.ucFTPAccounts.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.ucFTPAccounts.Location = new System.Drawing.Point(3, 3);
            this.ucFTPAccounts.Name = "ucFTPAccounts";
            this.ucFTPAccounts.Size = new System.Drawing.Size(785, 319);
            this.ucFTPAccounts.TabIndex = 0;
            // 
            // gbFTPSettings
            // 
            this.gbFTPSettings.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gbFTPSettings.Controls.Add(this.cbFTPThumbnailCheckSize);
            this.gbFTPSettings.Controls.Add(this.lblFTPThumbHeight);
            this.gbFTPSettings.Controls.Add(this.lblFTPThumbWidth);
            this.gbFTPSettings.Controls.Add(this.txtFTPThumbWidth);
            this.gbFTPSettings.Controls.Add(this.txtFTPThumbHeight);
            this.gbFTPSettings.Controls.Add(this.chkEnableThumbnail);
            this.gbFTPSettings.Location = new System.Drawing.Point(16, 322);
            this.gbFTPSettings.Name = "gbFTPSettings";
            this.gbFTPSettings.Size = new System.Drawing.Size(759, 75);
            this.gbFTPSettings.TabIndex = 115;
            this.gbFTPSettings.TabStop = false;
            this.gbFTPSettings.Text = "FTP Settings";
            // 
            // cbFTPThumbnailCheckSize
            // 
            this.cbFTPThumbnailCheckSize.AutoSize = true;
            this.cbFTPThumbnailCheckSize.Location = new System.Drawing.Point(16, 48);
            this.cbFTPThumbnailCheckSize.Name = "cbFTPThumbnailCheckSize";
            this.cbFTPThumbnailCheckSize.Size = new System.Drawing.Size(331, 17);
            this.cbFTPThumbnailCheckSize.TabIndex = 131;
            this.cbFTPThumbnailCheckSize.Text = "If image size smaller than thumbnail size then not make thumbnail";
            this.cbFTPThumbnailCheckSize.UseVisualStyleBackColor = true;
            this.cbFTPThumbnailCheckSize.CheckedChanged += new System.EventHandler(this.cbFTPThumbnailCheckSize_CheckedChanged);
            // 
            // lblFTPThumbHeight
            // 
            this.lblFTPThumbHeight.AutoSize = true;
            this.lblFTPThumbHeight.Location = new System.Drawing.Point(312, 25);
            this.lblFTPThumbHeight.Name = "lblFTPThumbHeight";
            this.lblFTPThumbHeight.Size = new System.Drawing.Size(61, 13);
            this.lblFTPThumbHeight.TabIndex = 130;
            this.lblFTPThumbHeight.Text = "Height (px):";
            // 
            // lblFTPThumbWidth
            // 
            this.lblFTPThumbWidth.AutoSize = true;
            this.lblFTPThumbWidth.Location = new System.Drawing.Point(200, 25);
            this.lblFTPThumbWidth.Name = "lblFTPThumbWidth";
            this.lblFTPThumbWidth.Size = new System.Drawing.Size(58, 13);
            this.lblFTPThumbWidth.TabIndex = 129;
            this.lblFTPThumbWidth.Text = "Width (px):";
            // 
            // txtFTPThumbWidth
            // 
            this.txtFTPThumbWidth.Location = new System.Drawing.Point(264, 22);
            this.txtFTPThumbWidth.Name = "txtFTPThumbWidth";
            this.txtFTPThumbWidth.Size = new System.Drawing.Size(40, 20);
            this.txtFTPThumbWidth.TabIndex = 127;
            this.txtFTPThumbWidth.Text = "2500";
            this.txtFTPThumbWidth.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtFTPThumbWidth.TextChanged += new System.EventHandler(this.txtFTPThumbWidth_TextChanged);
            // 
            // txtFTPThumbHeight
            // 
            this.txtFTPThumbHeight.Location = new System.Drawing.Point(376, 22);
            this.txtFTPThumbHeight.Name = "txtFTPThumbHeight";
            this.txtFTPThumbHeight.Size = new System.Drawing.Size(40, 20);
            this.txtFTPThumbHeight.TabIndex = 128;
            this.txtFTPThumbHeight.Text = "2500";
            this.txtFTPThumbHeight.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtFTPThumbHeight.TextChanged += new System.EventHandler(this.txtFTPThumbHeight_TextChanged);
            // 
            // chkEnableThumbnail
            // 
            this.chkEnableThumbnail.AutoSize = true;
            this.chkEnableThumbnail.BackColor = System.Drawing.Color.Transparent;
            this.chkEnableThumbnail.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.chkEnableThumbnail.Location = new System.Drawing.Point(16, 24);
            this.chkEnableThumbnail.Name = "chkEnableThumbnail";
            this.chkEnableThumbnail.Size = new System.Drawing.Size(187, 17);
            this.chkEnableThumbnail.TabIndex = 113;
            this.chkEnableThumbnail.Text = "Create thumbnail. Thumbnail size: ";
            this.chkEnableThumbnail.UseVisualStyleBackColor = false;
            this.chkEnableThumbnail.CheckedChanged += new System.EventHandler(this.chkEnableThumbnail_CheckedChanged);
            // 
            // tpTinyPic
            // 
            this.tpTinyPic.Controls.Add(this.gbTinyPic);
            this.tpTinyPic.Controls.Add(this.chkRememberTinyPicUserPass);
            this.tpTinyPic.Location = new System.Drawing.Point(4, 23);
            this.tpTinyPic.Name = "tpTinyPic";
            this.tpTinyPic.Padding = new System.Windows.Forms.Padding(3);
            this.tpTinyPic.Size = new System.Drawing.Size(791, 409);
            this.tpTinyPic.TabIndex = 0;
            this.tpTinyPic.Text = "TinyPic";
            this.tpTinyPic.UseVisualStyleBackColor = true;
            // 
            // gbTinyPic
            // 
            this.gbTinyPic.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gbTinyPic.Controls.Add(this.btnGalleryTinyPic);
            this.gbTinyPic.Controls.Add(this.btnRegCodeTinyPic);
            this.gbTinyPic.Controls.Add(this.lblRegistrationCode);
            this.gbTinyPic.Controls.Add(this.txtTinyPicShuk);
            this.gbTinyPic.Location = new System.Drawing.Point(16, 16);
            this.gbTinyPic.Name = "gbTinyPic";
            this.gbTinyPic.Size = new System.Drawing.Size(760, 64);
            this.gbTinyPic.TabIndex = 4;
            this.gbTinyPic.TabStop = false;
            this.gbTinyPic.Text = "Account";
            // 
            // btnGalleryTinyPic
            // 
            this.btnGalleryTinyPic.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGalleryTinyPic.Location = new System.Drawing.Point(672, 24);
            this.btnGalleryTinyPic.Name = "btnGalleryTinyPic";
            this.btnGalleryTinyPic.Size = new System.Drawing.Size(75, 23);
            this.btnGalleryTinyPic.TabIndex = 8;
            this.btnGalleryTinyPic.Text = "&MyImages...";
            this.btnGalleryTinyPic.UseVisualStyleBackColor = true;
            this.btnGalleryTinyPic.Click += new System.EventHandler(this.btnGalleryTinyPic_Click);
            // 
            // btnRegCodeTinyPic
            // 
            this.btnRegCodeTinyPic.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRegCodeTinyPic.Location = new System.Drawing.Point(592, 24);
            this.btnRegCodeTinyPic.Name = "btnRegCodeTinyPic";
            this.btnRegCodeTinyPic.Size = new System.Drawing.Size(75, 23);
            this.btnRegCodeTinyPic.TabIndex = 5;
            this.btnRegCodeTinyPic.Text = "&RegCode...";
            this.btnRegCodeTinyPic.UseVisualStyleBackColor = true;
            this.btnRegCodeTinyPic.Click += new System.EventHandler(this.btnRegCodeTinyPic_Click);
            // 
            // lblRegistrationCode
            // 
            this.lblRegistrationCode.AutoSize = true;
            this.lblRegistrationCode.Location = new System.Drawing.Point(8, 28);
            this.lblRegistrationCode.Name = "lblRegistrationCode";
            this.lblRegistrationCode.Size = new System.Drawing.Size(93, 13);
            this.lblRegistrationCode.TabIndex = 4;
            this.lblRegistrationCode.Text = "Registration code:";
            // 
            // txtTinyPicShuk
            // 
            this.txtTinyPicShuk.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTinyPicShuk.Location = new System.Drawing.Point(112, 24);
            this.txtTinyPicShuk.Name = "txtTinyPicShuk";
            this.txtTinyPicShuk.Size = new System.Drawing.Size(472, 20);
            this.txtTinyPicShuk.TabIndex = 3;
            this.txtTinyPicShuk.TextChanged += new System.EventHandler(this.txtTinyPicShuk_TextChanged);
            // 
            // chkRememberTinyPicUserPass
            // 
            this.chkRememberTinyPicUserPass.AutoSize = true;
            this.chkRememberTinyPicUserPass.Location = new System.Drawing.Point(24, 88);
            this.chkRememberTinyPicUserPass.Name = "chkRememberTinyPicUserPass";
            this.chkRememberTinyPicUserPass.Size = new System.Drawing.Size(241, 17);
            this.chkRememberTinyPicUserPass.TabIndex = 6;
            this.chkRememberTinyPicUserPass.Text = "Remember TinyPic User Name and Password";
            this.chkRememberTinyPicUserPass.UseVisualStyleBackColor = true;
            this.chkRememberTinyPicUserPass.CheckedChanged += new System.EventHandler(this.chkRememberTinyPicUserPass_CheckedChanged);
            // 
            // tpImageShack
            // 
            this.tpImageShack.Controls.Add(this.chkPublicImageShack);
            this.tpImageShack.Controls.Add(this.gbImageShack);
            this.tpImageShack.Location = new System.Drawing.Point(4, 23);
            this.tpImageShack.Name = "tpImageShack";
            this.tpImageShack.Padding = new System.Windows.Forms.Padding(3);
            this.tpImageShack.Size = new System.Drawing.Size(791, 409);
            this.tpImageShack.TabIndex = 1;
            this.tpImageShack.Text = "ImageShack";
            this.tpImageShack.UseVisualStyleBackColor = true;
            // 
            // chkPublicImageShack
            // 
            this.chkPublicImageShack.AutoSize = true;
            this.chkPublicImageShack.Location = new System.Drawing.Point(24, 120);
            this.chkPublicImageShack.Name = "chkPublicImageShack";
            this.chkPublicImageShack.Size = new System.Drawing.Size(309, 17);
            this.chkPublicImageShack.TabIndex = 1;
            this.chkPublicImageShack.Text = "Show images uploaded to ImageShack in your Public Profile";
            this.chkPublicImageShack.UseVisualStyleBackColor = true;
            this.chkPublicImageShack.CheckedChanged += new System.EventHandler(this.chkPublicImageShack_CheckedChanged);
            // 
            // gbImageShack
            // 
            this.gbImageShack.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gbImageShack.BackColor = System.Drawing.Color.Transparent;
            this.gbImageShack.Controls.Add(this.btnImageShackProfile);
            this.gbImageShack.Controls.Add(this.lblImageShackUsername);
            this.gbImageShack.Controls.Add(this.txtUserNameImageShack);
            this.gbImageShack.Controls.Add(this.btnGalleryImageShack);
            this.gbImageShack.Controls.Add(this.btnRegCodeImageShack);
            this.gbImageShack.Controls.Add(this.lblImageShackRegistrationCode);
            this.gbImageShack.Controls.Add(this.txtImageShackRegistrationCode);
            this.gbImageShack.Location = new System.Drawing.Point(16, 16);
            this.gbImageShack.Name = "gbImageShack";
            this.gbImageShack.Size = new System.Drawing.Size(760, 96);
            this.gbImageShack.TabIndex = 0;
            this.gbImageShack.TabStop = false;
            this.gbImageShack.Text = "Account";
            // 
            // btnImageShackProfile
            // 
            this.btnImageShackProfile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnImageShackProfile.Location = new System.Drawing.Point(456, 56);
            this.btnImageShackProfile.Name = "btnImageShackProfile";
            this.btnImageShackProfile.Size = new System.Drawing.Size(184, 23);
            this.btnImageShackProfile.TabIndex = 6;
            this.btnImageShackProfile.Text = "&Public Profile...";
            this.btnImageShackProfile.UseVisualStyleBackColor = true;
            this.btnImageShackProfile.Click += new System.EventHandler(this.btnImageShackProfile_Click);
            // 
            // lblImageShackUsername
            // 
            this.lblImageShackUsername.AutoSize = true;
            this.lblImageShackUsername.Location = new System.Drawing.Point(40, 56);
            this.lblImageShackUsername.Name = "lblImageShackUsername";
            this.lblImageShackUsername.Size = new System.Drawing.Size(63, 13);
            this.lblImageShackUsername.TabIndex = 5;
            this.lblImageShackUsername.Text = "User Name:";
            // 
            // txtUserNameImageShack
            // 
            this.txtUserNameImageShack.Location = new System.Drawing.Point(112, 56);
            this.txtUserNameImageShack.Name = "txtUserNameImageShack";
            this.txtUserNameImageShack.Size = new System.Drawing.Size(328, 20);
            this.txtUserNameImageShack.TabIndex = 4;
            this.txtUserNameImageShack.TextChanged += new System.EventHandler(this.txtUserNameImageShack_TextChanged);
            // 
            // btnGalleryImageShack
            // 
            this.btnGalleryImageShack.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGalleryImageShack.Location = new System.Drawing.Point(552, 24);
            this.btnGalleryImageShack.Name = "btnGalleryImageShack";
            this.btnGalleryImageShack.Size = new System.Drawing.Size(88, 23);
            this.btnGalleryImageShack.TabIndex = 3;
            this.btnGalleryImageShack.Text = "&MyImages...";
            this.btnGalleryImageShack.UseVisualStyleBackColor = true;
            this.btnGalleryImageShack.Click += new System.EventHandler(this.btnGalleryImageShack_Click);
            // 
            // btnRegCodeImageShack
            // 
            this.btnRegCodeImageShack.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRegCodeImageShack.Location = new System.Drawing.Point(456, 24);
            this.btnRegCodeImageShack.Name = "btnRegCodeImageShack";
            this.btnRegCodeImageShack.Size = new System.Drawing.Size(88, 23);
            this.btnRegCodeImageShack.TabIndex = 2;
            this.btnRegCodeImageShack.Text = "&RegCode...";
            this.btnRegCodeImageShack.UseVisualStyleBackColor = true;
            this.btnRegCodeImageShack.Click += new System.EventHandler(this.btnRegCodeImageShack_Click);
            // 
            // lblImageShackRegistrationCode
            // 
            this.lblImageShackRegistrationCode.AutoSize = true;
            this.lblImageShackRegistrationCode.Location = new System.Drawing.Point(8, 28);
            this.lblImageShackRegistrationCode.Name = "lblImageShackRegistrationCode";
            this.lblImageShackRegistrationCode.Size = new System.Drawing.Size(93, 13);
            this.lblImageShackRegistrationCode.TabIndex = 1;
            this.lblImageShackRegistrationCode.Text = "Registration code:";
            // 
            // txtImageShackRegistrationCode
            // 
            this.txtImageShackRegistrationCode.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtImageShackRegistrationCode.Location = new System.Drawing.Point(112, 24);
            this.txtImageShackRegistrationCode.Name = "txtImageShackRegistrationCode";
            this.txtImageShackRegistrationCode.Size = new System.Drawing.Size(336, 20);
            this.txtImageShackRegistrationCode.TabIndex = 0;
            this.txtImageShackRegistrationCode.TextChanged += new System.EventHandler(this.txtImageShackRegistrationCode_TextChanged);
            // 
            // tpTwitter
            // 
            this.tpTwitter.Controls.Add(this.tcTwitter);
            this.tpTwitter.Controls.Add(this.gbTwitterAccount);
            this.tpTwitter.ImageKey = "Twitter.ico";
            this.tpTwitter.Location = new System.Drawing.Point(4, 23);
            this.tpTwitter.Name = "tpTwitter";
            this.tpTwitter.Padding = new System.Windows.Forms.Padding(3);
            this.tpTwitter.Size = new System.Drawing.Size(791, 409);
            this.tpTwitter.TabIndex = 6;
            this.tpTwitter.Text = "Twitter";
            this.tpTwitter.UseVisualStyleBackColor = true;
            // 
            // tcTwitter
            // 
            this.tcTwitter.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tcTwitter.Controls.Add(this.tpTwitPic);
            this.tcTwitter.Controls.Add(this.tpYfrog);
            this.tcTwitter.Location = new System.Drawing.Point(8, 112);
            this.tcTwitter.Name = "tcTwitter";
            this.tcTwitter.SelectedIndex = 0;
            this.tcTwitter.Size = new System.Drawing.Size(776, 294);
            this.tcTwitter.TabIndex = 16;
            // 
            // tpTwitPic
            // 
            this.tpTwitPic.Controls.Add(this.cboTwitPicUploadMode);
            this.tpTwitPic.Controls.Add(this.lblTwitPicThumbnailMode);
            this.tpTwitPic.Controls.Add(this.lblTwitPicUploadMode);
            this.tpTwitPic.Controls.Add(this.cboTwitPicThumbnailMode);
            this.tpTwitPic.Controls.Add(this.cbTwitPicShowFull);
            this.tpTwitPic.Location = new System.Drawing.Point(4, 22);
            this.tpTwitPic.Name = "tpTwitPic";
            this.tpTwitPic.Padding = new System.Windows.Forms.Padding(3);
            this.tpTwitPic.Size = new System.Drawing.Size(768, 268);
            this.tpTwitPic.TabIndex = 0;
            this.tpTwitPic.Text = "TwitPic";
            this.tpTwitPic.UseVisualStyleBackColor = true;
            // 
            // cboTwitPicUploadMode
            // 
            this.cboTwitPicUploadMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTwitPicUploadMode.FormattingEnabled = true;
            this.cboTwitPicUploadMode.Location = new System.Drawing.Point(112, 16);
            this.cboTwitPicUploadMode.Name = "cboTwitPicUploadMode";
            this.cboTwitPicUploadMode.Size = new System.Drawing.Size(312, 21);
            this.cboTwitPicUploadMode.TabIndex = 11;
            this.cboTwitPicUploadMode.SelectedIndexChanged += new System.EventHandler(this.cboTwitPicUploadMode_SelectedIndexChanged);
            // 
            // lblTwitPicThumbnailMode
            // 
            this.lblTwitPicThumbnailMode.AutoSize = true;
            this.lblTwitPicThumbnailMode.Location = new System.Drawing.Point(16, 77);
            this.lblTwitPicThumbnailMode.Name = "lblTwitPicThumbnailMode";
            this.lblTwitPicThumbnailMode.Size = new System.Drawing.Size(89, 13);
            this.lblTwitPicThumbnailMode.TabIndex = 15;
            this.lblTwitPicThumbnailMode.Text = "Thumbnail Mode:";
            // 
            // lblTwitPicUploadMode
            // 
            this.lblTwitPicUploadMode.AutoSize = true;
            this.lblTwitPicUploadMode.Location = new System.Drawing.Point(16, 21);
            this.lblTwitPicUploadMode.Name = "lblTwitPicUploadMode";
            this.lblTwitPicUploadMode.Size = new System.Drawing.Size(80, 13);
            this.lblTwitPicUploadMode.TabIndex = 12;
            this.lblTwitPicUploadMode.Text = "Upload Method";
            // 
            // cboTwitPicThumbnailMode
            // 
            this.cboTwitPicThumbnailMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTwitPicThumbnailMode.FormattingEnabled = true;
            this.cboTwitPicThumbnailMode.Location = new System.Drawing.Point(112, 72);
            this.cboTwitPicThumbnailMode.Name = "cboTwitPicThumbnailMode";
            this.cboTwitPicThumbnailMode.Size = new System.Drawing.Size(144, 21);
            this.cboTwitPicThumbnailMode.TabIndex = 14;
            this.cboTwitPicThumbnailMode.SelectedIndexChanged += new System.EventHandler(this.cbTwitPicThumbnailMode_SelectedIndexChanged);
            // 
            // cbTwitPicShowFull
            // 
            this.cbTwitPicShowFull.AutoSize = true;
            this.cbTwitPicShowFull.Location = new System.Drawing.Point(20, 48);
            this.cbTwitPicShowFull.Name = "cbTwitPicShowFull";
            this.cbTwitPicShowFull.Size = new System.Drawing.Size(94, 17);
            this.cbTwitPicShowFull.TabIndex = 13;
            this.cbTwitPicShowFull.Text = "Show full URL";
            this.ttZScreen.SetToolTip(this.cbTwitPicShowFull, "Append /full to the url to show the image in full size");
            this.cbTwitPicShowFull.UseVisualStyleBackColor = true;
            this.cbTwitPicShowFull.CheckedChanged += new System.EventHandler(this.cbTwitPicShowFull_CheckedChanged);
            // 
            // tpYfrog
            // 
            this.tpYfrog.Controls.Add(this.cboYfrogUploadMode);
            this.tpYfrog.Controls.Add(this.label1);
            this.tpYfrog.Location = new System.Drawing.Point(4, 22);
            this.tpYfrog.Name = "tpYfrog";
            this.tpYfrog.Padding = new System.Windows.Forms.Padding(3);
            this.tpYfrog.Size = new System.Drawing.Size(768, 268);
            this.tpYfrog.TabIndex = 2;
            this.tpYfrog.Text = "yFrog";
            this.tpYfrog.UseVisualStyleBackColor = true;
            // 
            // cboYfrogUploadMode
            // 
            this.cboYfrogUploadMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboYfrogUploadMode.FormattingEnabled = true;
            this.cboYfrogUploadMode.Location = new System.Drawing.Point(112, 16);
            this.cboYfrogUploadMode.Name = "cboYfrogUploadMode";
            this.cboYfrogUploadMode.Size = new System.Drawing.Size(312, 21);
            this.cboYfrogUploadMode.TabIndex = 13;
            this.cboYfrogUploadMode.SelectedIndexChanged += new System.EventHandler(this.cboYfrogUploadMode_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 13);
            this.label1.TabIndex = 14;
            this.label1.Text = "Upload Method";
            // 
            // gbTwitterAccount
            // 
            this.gbTwitterAccount.Controls.Add(this.lblTwitterPassword);
            this.gbTwitterAccount.Controls.Add(this.txtTwitPicPassword);
            this.gbTwitterAccount.Controls.Add(this.lblTwitterUsername);
            this.gbTwitterAccount.Controls.Add(this.txtTwitPicUserName);
            this.gbTwitterAccount.Location = new System.Drawing.Point(8, 8);
            this.gbTwitterAccount.Name = "gbTwitterAccount";
            this.gbTwitterAccount.Size = new System.Drawing.Size(416, 88);
            this.gbTwitterAccount.TabIndex = 10;
            this.gbTwitterAccount.TabStop = false;
            this.gbTwitterAccount.Text = "Twitter Account";
            // 
            // lblTwitterPassword
            // 
            this.lblTwitterPassword.AutoSize = true;
            this.lblTwitterPassword.Location = new System.Drawing.Point(16, 51);
            this.lblTwitterPassword.Name = "lblTwitterPassword";
            this.lblTwitterPassword.Size = new System.Drawing.Size(56, 13);
            this.lblTwitterPassword.TabIndex = 9;
            this.lblTwitterPassword.Text = "Password:";
            // 
            // txtTwitPicPassword
            // 
            this.txtTwitPicPassword.Location = new System.Drawing.Point(88, 48);
            this.txtTwitPicPassword.Name = "txtTwitPicPassword";
            this.txtTwitPicPassword.PasswordChar = '*';
            this.txtTwitPicPassword.Size = new System.Drawing.Size(312, 20);
            this.txtTwitPicPassword.TabIndex = 8;
            this.txtTwitPicPassword.TextChanged += new System.EventHandler(this.txtTwitPicPassword_TextChanged);
            // 
            // lblTwitterUsername
            // 
            this.lblTwitterUsername.AutoSize = true;
            this.lblTwitterUsername.Location = new System.Drawing.Point(16, 27);
            this.lblTwitterUsername.Name = "lblTwitterUsername";
            this.lblTwitterUsername.Size = new System.Drawing.Size(58, 13);
            this.lblTwitterUsername.TabIndex = 7;
            this.lblTwitterUsername.Text = "Username:";
            // 
            // txtTwitPicUserName
            // 
            this.txtTwitPicUserName.Location = new System.Drawing.Point(88, 24);
            this.txtTwitPicUserName.Name = "txtTwitPicUserName";
            this.txtTwitPicUserName.Size = new System.Drawing.Size(312, 20);
            this.txtTwitPicUserName.TabIndex = 6;
            this.txtTwitPicUserName.TextChanged += new System.EventHandler(this.txtTwitPicUserName_TextChanged);
            // 
            // tpImageBam
            // 
            this.tpImageBam.Controls.Add(this.gbImageBamGalleries);
            this.tpImageBam.Controls.Add(this.gbImageBamLinks);
            this.tpImageBam.Controls.Add(this.gbImageBamApiKeys);
            this.tpImageBam.Location = new System.Drawing.Point(4, 23);
            this.tpImageBam.Name = "tpImageBam";
            this.tpImageBam.Padding = new System.Windows.Forms.Padding(3);
            this.tpImageBam.Size = new System.Drawing.Size(791, 409);
            this.tpImageBam.TabIndex = 7;
            this.tpImageBam.Text = "ImageBam";
            this.tpImageBam.UseVisualStyleBackColor = true;
            // 
            // gbImageBamGalleries
            // 
            this.gbImageBamGalleries.Controls.Add(this.lbImageBamGalleries);
            this.gbImageBamGalleries.Location = new System.Drawing.Point(8, 112);
            this.gbImageBamGalleries.Name = "gbImageBamGalleries";
            this.gbImageBamGalleries.Size = new System.Drawing.Size(480, 152);
            this.gbImageBamGalleries.TabIndex = 10;
            this.gbImageBamGalleries.TabStop = false;
            this.gbImageBamGalleries.Text = "Galleries";
            // 
            // lbImageBamGalleries
            // 
            this.lbImageBamGalleries.FormattingEnabled = true;
            this.lbImageBamGalleries.Location = new System.Drawing.Point(16, 24);
            this.lbImageBamGalleries.Name = "lbImageBamGalleries";
            this.lbImageBamGalleries.Size = new System.Drawing.Size(440, 108);
            this.lbImageBamGalleries.TabIndex = 0;
            this.lbImageBamGalleries.SelectedIndexChanged += new System.EventHandler(this.lbImageBamGalleries_SelectedIndexChanged);
            // 
            // gbImageBamLinks
            // 
            this.gbImageBamLinks.Controls.Add(this.chkImageBamContentNSFW);
            this.gbImageBamLinks.Controls.Add(this.btnImageBamRemoveGallery);
            this.gbImageBamLinks.Controls.Add(this.btnImageBamCreateGallery);
            this.gbImageBamLinks.Controls.Add(this.btnImageBamRegister);
            this.gbImageBamLinks.Controls.Add(this.btnImageBamApiKeysUrl);
            this.gbImageBamLinks.Location = new System.Drawing.Point(496, 8);
            this.gbImageBamLinks.Name = "gbImageBamLinks";
            this.gbImageBamLinks.Size = new System.Drawing.Size(160, 256);
            this.gbImageBamLinks.TabIndex = 9;
            this.gbImageBamLinks.TabStop = false;
            this.gbImageBamLinks.Text = "Tasks";
            // 
            // chkImageBamContentNSFW
            // 
            this.chkImageBamContentNSFW.AutoSize = true;
            this.chkImageBamContentNSFW.Location = new System.Drawing.Point(16, 152);
            this.chkImageBamContentNSFW.Name = "chkImageBamContentNSFW";
            this.chkImageBamContentNSFW.Size = new System.Drawing.Size(98, 17);
            this.chkImageBamContentNSFW.TabIndex = 10;
            this.chkImageBamContentNSFW.Text = "NSFW Content";
            this.ttZScreen.SetToolTip(this.chkImageBamContentNSFW, "If you are uploading NSFW (Not Safe for Work) content then tick this checkbox");
            this.chkImageBamContentNSFW.UseVisualStyleBackColor = true;
            this.chkImageBamContentNSFW.CheckedChanged += new System.EventHandler(this.chkImageBamContentNSFW_CheckedChanged);
            // 
            // btnImageBamRemoveGallery
            // 
            this.btnImageBamRemoveGallery.Location = new System.Drawing.Point(16, 120);
            this.btnImageBamRemoveGallery.Name = "btnImageBamRemoveGallery";
            this.btnImageBamRemoveGallery.Size = new System.Drawing.Size(128, 23);
            this.btnImageBamRemoveGallery.TabIndex = 9;
            this.btnImageBamRemoveGallery.Text = "Remove &Gallery";
            this.btnImageBamRemoveGallery.UseVisualStyleBackColor = true;
            this.btnImageBamRemoveGallery.Click += new System.EventHandler(this.btnImageBamRemoveGallery_Click);
            // 
            // btnImageBamCreateGallery
            // 
            this.btnImageBamCreateGallery.Location = new System.Drawing.Point(16, 88);
            this.btnImageBamCreateGallery.Name = "btnImageBamCreateGallery";
            this.btnImageBamCreateGallery.Size = new System.Drawing.Size(128, 23);
            this.btnImageBamCreateGallery.TabIndex = 8;
            this.btnImageBamCreateGallery.Text = "Create &Gallery";
            this.btnImageBamCreateGallery.UseVisualStyleBackColor = true;
            this.btnImageBamCreateGallery.Click += new System.EventHandler(this.btnImageBamCreateGallery_Click);
            // 
            // btnImageBamRegister
            // 
            this.btnImageBamRegister.AutoSize = true;
            this.btnImageBamRegister.Location = new System.Drawing.Point(16, 24);
            this.btnImageBamRegister.Name = "btnImageBamRegister";
            this.btnImageBamRegister.Size = new System.Drawing.Size(130, 23);
            this.btnImageBamRegister.TabIndex = 7;
            this.btnImageBamRegister.Text = "Register at ImageBam...";
            this.btnImageBamRegister.UseVisualStyleBackColor = true;
            this.btnImageBamRegister.Click += new System.EventHandler(this.btnImageBamRegister_Click);
            // 
            // btnImageBamApiKeysUrl
            // 
            this.btnImageBamApiKeysUrl.AutoSize = true;
            this.btnImageBamApiKeysUrl.Location = new System.Drawing.Point(16, 56);
            this.btnImageBamApiKeysUrl.Name = "btnImageBamApiKeysUrl";
            this.btnImageBamApiKeysUrl.Size = new System.Drawing.Size(128, 23);
            this.btnImageBamApiKeysUrl.TabIndex = 6;
            this.btnImageBamApiKeysUrl.Text = "View your API Keys...";
            this.btnImageBamApiKeysUrl.UseVisualStyleBackColor = true;
            this.btnImageBamApiKeysUrl.Click += new System.EventHandler(this.btnImageBamApiKeysUrl_Click);
            // 
            // gbImageBamApiKeys
            // 
            this.gbImageBamApiKeys.Controls.Add(this.lblImageBamSecret);
            this.gbImageBamApiKeys.Controls.Add(this.txtImageBamSecret);
            this.gbImageBamApiKeys.Controls.Add(this.lblImageBamKey);
            this.gbImageBamApiKeys.Controls.Add(this.txtImageBamApiKey);
            this.gbImageBamApiKeys.Location = new System.Drawing.Point(8, 8);
            this.gbImageBamApiKeys.Name = "gbImageBamApiKeys";
            this.gbImageBamApiKeys.Size = new System.Drawing.Size(480, 96);
            this.gbImageBamApiKeys.TabIndex = 8;
            this.gbImageBamApiKeys.TabStop = false;
            this.gbImageBamApiKeys.Text = "API-Keys";
            // 
            // lblImageBamSecret
            // 
            this.lblImageBamSecret.AutoSize = true;
            this.lblImageBamSecret.Location = new System.Drawing.Point(16, 56);
            this.lblImageBamSecret.Name = "lblImageBamSecret";
            this.lblImageBamSecret.Size = new System.Drawing.Size(41, 13);
            this.lblImageBamSecret.TabIndex = 5;
            this.lblImageBamSecret.Text = "Secret:";
            // 
            // txtImageBamSecret
            // 
            this.txtImageBamSecret.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtImageBamSecret.Location = new System.Drawing.Point(63, 52);
            this.txtImageBamSecret.Name = "txtImageBamSecret";
            this.txtImageBamSecret.Size = new System.Drawing.Size(393, 20);
            this.txtImageBamSecret.TabIndex = 4;
            this.txtImageBamSecret.TextChanged += new System.EventHandler(this.txtImageBamSecret_TextChanged);
            // 
            // lblImageBamKey
            // 
            this.lblImageBamKey.AutoSize = true;
            this.lblImageBamKey.Location = new System.Drawing.Point(29, 26);
            this.lblImageBamKey.Name = "lblImageBamKey";
            this.lblImageBamKey.Size = new System.Drawing.Size(28, 13);
            this.lblImageBamKey.TabIndex = 3;
            this.lblImageBamKey.Text = "Key:";
            // 
            // txtImageBamApiKey
            // 
            this.txtImageBamApiKey.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtImageBamApiKey.Location = new System.Drawing.Point(62, 22);
            this.txtImageBamApiKey.Name = "txtImageBamApiKey";
            this.txtImageBamApiKey.Size = new System.Drawing.Size(394, 20);
            this.txtImageBamApiKey.TabIndex = 2;
            this.txtImageBamApiKey.TextChanged += new System.EventHandler(this.txtImageBamApiKey_TextChanged);
            // 
            // tpRapidShare
            // 
            this.tpRapidShare.Controls.Add(this.lblRapidSharePassword);
            this.tpRapidShare.Controls.Add(this.lblRapidSharePremiumUsername);
            this.tpRapidShare.Controls.Add(this.lblRapidShareCollectorsID);
            this.tpRapidShare.Controls.Add(this.txtRapidSharePassword);
            this.tpRapidShare.Controls.Add(this.txtRapidSharePremiumUserName);
            this.tpRapidShare.Controls.Add(this.txtRapidShareCollectorID);
            this.tpRapidShare.Controls.Add(this.cboRapidShareAcctType);
            this.tpRapidShare.Controls.Add(this.lblRapidShareAccountType);
            this.tpRapidShare.ImageKey = "(none)";
            this.tpRapidShare.Location = new System.Drawing.Point(4, 23);
            this.tpRapidShare.Name = "tpRapidShare";
            this.tpRapidShare.Padding = new System.Windows.Forms.Padding(3);
            this.tpRapidShare.Size = new System.Drawing.Size(791, 409);
            this.tpRapidShare.TabIndex = 8;
            this.tpRapidShare.Text = "RapidShare";
            this.tpRapidShare.UseVisualStyleBackColor = true;
            // 
            // lblRapidSharePassword
            // 
            this.lblRapidSharePassword.AutoSize = true;
            this.lblRapidSharePassword.Location = new System.Drawing.Point(72, 120);
            this.lblRapidSharePassword.Name = "lblRapidSharePassword";
            this.lblRapidSharePassword.Size = new System.Drawing.Size(53, 13);
            this.lblRapidSharePassword.TabIndex = 7;
            this.lblRapidSharePassword.Text = "Password";
            // 
            // lblRapidSharePremiumUsername
            // 
            this.lblRapidSharePremiumUsername.AutoSize = true;
            this.lblRapidSharePremiumUsername.Location = new System.Drawing.Point(22, 88);
            this.lblRapidSharePremiumUsername.Name = "lblRapidSharePremiumUsername";
            this.lblRapidSharePremiumUsername.Size = new System.Drawing.Size(103, 13);
            this.lblRapidSharePremiumUsername.TabIndex = 6;
            this.lblRapidSharePremiumUsername.Text = "Premium User Name";
            // 
            // lblRapidShareCollectorsID
            // 
            this.lblRapidShareCollectorsID.AutoSize = true;
            this.lblRapidShareCollectorsID.Location = new System.Drawing.Point(56, 56);
            this.lblRapidShareCollectorsID.Name = "lblRapidShareCollectorsID";
            this.lblRapidShareCollectorsID.Size = new System.Drawing.Size(69, 13);
            this.lblRapidShareCollectorsID.TabIndex = 5;
            this.lblRapidShareCollectorsID.Text = "Collector\'s ID";
            // 
            // txtRapidSharePassword
            // 
            this.txtRapidSharePassword.Location = new System.Drawing.Point(136, 117);
            this.txtRapidSharePassword.Name = "txtRapidSharePassword";
            this.txtRapidSharePassword.PasswordChar = '*';
            this.txtRapidSharePassword.Size = new System.Drawing.Size(120, 20);
            this.txtRapidSharePassword.TabIndex = 4;
            this.txtRapidSharePassword.TextChanged += new System.EventHandler(this.txtRapidSharePassword_TextChanged);
            // 
            // txtRapidSharePremiumUserName
            // 
            this.txtRapidSharePremiumUserName.Location = new System.Drawing.Point(136, 85);
            this.txtRapidSharePremiumUserName.Name = "txtRapidSharePremiumUserName";
            this.txtRapidSharePremiumUserName.Size = new System.Drawing.Size(120, 20);
            this.txtRapidSharePremiumUserName.TabIndex = 3;
            this.txtRapidSharePremiumUserName.TextChanged += new System.EventHandler(this.txtRapidSharePremiumUserName_TextChanged);
            // 
            // txtRapidShareCollectorID
            // 
            this.txtRapidShareCollectorID.Location = new System.Drawing.Point(136, 53);
            this.txtRapidShareCollectorID.Name = "txtRapidShareCollectorID";
            this.txtRapidShareCollectorID.Size = new System.Drawing.Size(120, 20);
            this.txtRapidShareCollectorID.TabIndex = 2;
            this.txtRapidShareCollectorID.TextChanged += new System.EventHandler(this.txtRapidShareCollectorID_TextChanged);
            // 
            // cboRapidShareAcctType
            // 
            this.cboRapidShareAcctType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboRapidShareAcctType.FormattingEnabled = true;
            this.cboRapidShareAcctType.Location = new System.Drawing.Point(136, 21);
            this.cboRapidShareAcctType.Name = "cboRapidShareAcctType";
            this.cboRapidShareAcctType.Size = new System.Drawing.Size(121, 21);
            this.cboRapidShareAcctType.TabIndex = 1;
            this.cboRapidShareAcctType.SelectedIndexChanged += new System.EventHandler(this.cboRapidShareAcctType_SelectedIndexChanged);
            // 
            // lblRapidShareAccountType
            // 
            this.lblRapidShareAccountType.AutoSize = true;
            this.lblRapidShareAccountType.Location = new System.Drawing.Point(51, 24);
            this.lblRapidShareAccountType.Name = "lblRapidShareAccountType";
            this.lblRapidShareAccountType.Size = new System.Drawing.Size(74, 13);
            this.lblRapidShareAccountType.TabIndex = 0;
            this.lblRapidShareAccountType.Text = "Account Type";
            // 
            // tpSendSpace
            // 
            this.tpSendSpace.Controls.Add(this.btnSendSpaceRegister);
            this.tpSendSpace.Controls.Add(this.lblSendSpacePassword);
            this.tpSendSpace.Controls.Add(this.lblSendSpaceUsername);
            this.tpSendSpace.Controls.Add(this.txtSendSpacePassword);
            this.tpSendSpace.Controls.Add(this.txtSendSpaceUserName);
            this.tpSendSpace.Controls.Add(this.cboSendSpaceAcctType);
            this.tpSendSpace.Controls.Add(this.lblSendSpaceAccountType);
            this.tpSendSpace.Location = new System.Drawing.Point(4, 23);
            this.tpSendSpace.Name = "tpSendSpace";
            this.tpSendSpace.Padding = new System.Windows.Forms.Padding(3);
            this.tpSendSpace.Size = new System.Drawing.Size(791, 409);
            this.tpSendSpace.TabIndex = 9;
            this.tpSendSpace.Text = "SendSpace";
            this.tpSendSpace.UseVisualStyleBackColor = true;
            // 
            // btnSendSpaceRegister
            // 
            this.btnSendSpaceRegister.Location = new System.Drawing.Point(256, 24);
            this.btnSendSpaceRegister.Name = "btnSendSpaceRegister";
            this.btnSendSpaceRegister.Size = new System.Drawing.Size(75, 23);
            this.btnSendSpaceRegister.TabIndex = 16;
            this.btnSendSpaceRegister.Text = "&Register...";
            this.btnSendSpaceRegister.UseVisualStyleBackColor = true;
            this.btnSendSpaceRegister.Click += new System.EventHandler(this.btnSendSpaceRegister_Click);
            // 
            // lblSendSpacePassword
            // 
            this.lblSendSpacePassword.AutoSize = true;
            this.lblSendSpacePassword.Location = new System.Drawing.Point(64, 92);
            this.lblSendSpacePassword.Name = "lblSendSpacePassword";
            this.lblSendSpacePassword.Size = new System.Drawing.Size(53, 13);
            this.lblSendSpacePassword.TabIndex = 15;
            this.lblSendSpacePassword.Text = "Password";
            // 
            // lblSendSpaceUsername
            // 
            this.lblSendSpaceUsername.AutoSize = true;
            this.lblSendSpaceUsername.Location = new System.Drawing.Point(56, 60);
            this.lblSendSpaceUsername.Name = "lblSendSpaceUsername";
            this.lblSendSpaceUsername.Size = new System.Drawing.Size(60, 13);
            this.lblSendSpaceUsername.TabIndex = 14;
            this.lblSendSpaceUsername.Text = "User Name";
            // 
            // txtSendSpacePassword
            // 
            this.txtSendSpacePassword.Location = new System.Drawing.Point(128, 89);
            this.txtSendSpacePassword.Name = "txtSendSpacePassword";
            this.txtSendSpacePassword.PasswordChar = '*';
            this.txtSendSpacePassword.Size = new System.Drawing.Size(120, 20);
            this.txtSendSpacePassword.TabIndex = 12;
            this.txtSendSpacePassword.TextChanged += new System.EventHandler(this.txtSendSpacePassword_TextChanged);
            // 
            // txtSendSpaceUserName
            // 
            this.txtSendSpaceUserName.Location = new System.Drawing.Point(128, 57);
            this.txtSendSpaceUserName.Name = "txtSendSpaceUserName";
            this.txtSendSpaceUserName.Size = new System.Drawing.Size(120, 20);
            this.txtSendSpaceUserName.TabIndex = 11;
            this.txtSendSpaceUserName.TextChanged += new System.EventHandler(this.txtSendSpaceUserName_TextChanged);
            // 
            // cboSendSpaceAcctType
            // 
            this.cboSendSpaceAcctType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboSendSpaceAcctType.FormattingEnabled = true;
            this.cboSendSpaceAcctType.Location = new System.Drawing.Point(128, 25);
            this.cboSendSpaceAcctType.Name = "cboSendSpaceAcctType";
            this.cboSendSpaceAcctType.Size = new System.Drawing.Size(121, 21);
            this.cboSendSpaceAcctType.TabIndex = 9;
            this.cboSendSpaceAcctType.SelectedIndexChanged += new System.EventHandler(this.cboSendSpaceAcctType_SelectedIndexChanged);
            // 
            // lblSendSpaceAccountType
            // 
            this.lblSendSpaceAccountType.AutoSize = true;
            this.lblSendSpaceAccountType.Location = new System.Drawing.Point(43, 28);
            this.lblSendSpaceAccountType.Name = "lblSendSpaceAccountType";
            this.lblSendSpaceAccountType.Size = new System.Drawing.Size(74, 13);
            this.lblSendSpaceAccountType.TabIndex = 8;
            this.lblSendSpaceAccountType.Text = "Account Type";
            // 
            // tpMindTouch
            // 
            this.tpMindTouch.Controls.Add(this.gbMindTouchOptions);
            this.tpMindTouch.Controls.Add(this.ucMindTouchAccounts);
            this.tpMindTouch.Location = new System.Drawing.Point(4, 23);
            this.tpMindTouch.Name = "tpMindTouch";
            this.tpMindTouch.Padding = new System.Windows.Forms.Padding(3);
            this.tpMindTouch.Size = new System.Drawing.Size(791, 409);
            this.tpMindTouch.TabIndex = 4;
            this.tpMindTouch.Text = "MindTouch";
            this.tpMindTouch.UseVisualStyleBackColor = true;
            // 
            // gbMindTouchOptions
            // 
            this.gbMindTouchOptions.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gbMindTouchOptions.Controls.Add(this.chkDekiWikiForcePath);
            this.gbMindTouchOptions.Location = new System.Drawing.Point(16, 322);
            this.gbMindTouchOptions.Name = "gbMindTouchOptions";
            this.gbMindTouchOptions.Size = new System.Drawing.Size(759, 72);
            this.gbMindTouchOptions.TabIndex = 116;
            this.gbMindTouchOptions.TabStop = false;
            this.gbMindTouchOptions.Text = "MindTouch Deki Wiki Settings";
            // 
            // chkDekiWikiForcePath
            // 
            this.chkDekiWikiForcePath.AutoSize = true;
            this.chkDekiWikiForcePath.BackColor = System.Drawing.Color.Transparent;
            this.chkDekiWikiForcePath.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.chkDekiWikiForcePath.Location = new System.Drawing.Point(16, 24);
            this.chkDekiWikiForcePath.Name = "chkDekiWikiForcePath";
            this.chkDekiWikiForcePath.Size = new System.Drawing.Size(295, 17);
            this.chkDekiWikiForcePath.TabIndex = 113;
            this.chkDekiWikiForcePath.Text = "Ask where to save everytime when a screenshot is taken";
            this.chkDekiWikiForcePath.UseVisualStyleBackColor = false;
            this.chkDekiWikiForcePath.CheckedChanged += new System.EventHandler(this.chkDekiWikiForcePath_CheckedChanged);
            // 
            // ucMindTouchAccounts
            // 
            this.ucMindTouchAccounts.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.ucMindTouchAccounts.Location = new System.Drawing.Point(3, 3);
            this.ucMindTouchAccounts.Name = "ucMindTouchAccounts";
            this.ucMindTouchAccounts.Size = new System.Drawing.Size(785, 319);
            this.ucMindTouchAccounts.TabIndex = 0;
            // 
            // tpFlickr
            // 
            this.tpFlickr.Controls.Add(this.btnFlickrOpenImages);
            this.tpFlickr.Controls.Add(this.pgFlickrAuthInfo);
            this.tpFlickr.Controls.Add(this.pgFlickrSettings);
            this.tpFlickr.Controls.Add(this.btnFlickrCheckToken);
            this.tpFlickr.Controls.Add(this.btnFlickrGetToken);
            this.tpFlickr.Controls.Add(this.btnFlickrGetFrob);
            this.tpFlickr.Location = new System.Drawing.Point(4, 23);
            this.tpFlickr.Name = "tpFlickr";
            this.tpFlickr.Padding = new System.Windows.Forms.Padding(3);
            this.tpFlickr.Size = new System.Drawing.Size(791, 409);
            this.tpFlickr.TabIndex = 10;
            this.tpFlickr.Text = "Flickr";
            this.tpFlickr.UseVisualStyleBackColor = true;
            // 
            // btnFlickrOpenImages
            // 
            this.btnFlickrOpenImages.Location = new System.Drawing.Point(472, 16);
            this.btnFlickrOpenImages.Name = "btnFlickrOpenImages";
            this.btnFlickrOpenImages.Size = new System.Drawing.Size(128, 23);
            this.btnFlickrOpenImages.TabIndex = 7;
            this.btnFlickrOpenImages.Text = "Open images page";
            this.btnFlickrOpenImages.UseVisualStyleBackColor = true;
            this.btnFlickrOpenImages.Click += new System.EventHandler(this.btnFlickrOpenImages_Click);
            // 
            // pgFlickrAuthInfo
            // 
            this.pgFlickrAuthInfo.CommandsVisibleIfAvailable = false;
            this.pgFlickrAuthInfo.HelpVisible = false;
            this.pgFlickrAuthInfo.Location = new System.Drawing.Point(16, 48);
            this.pgFlickrAuthInfo.Name = "pgFlickrAuthInfo";
            this.pgFlickrAuthInfo.PropertySort = System.Windows.Forms.PropertySort.Alphabetical;
            this.pgFlickrAuthInfo.Size = new System.Drawing.Size(584, 104);
            this.pgFlickrAuthInfo.TabIndex = 6;
            this.pgFlickrAuthInfo.ToolbarVisible = false;
            // 
            // pgFlickrSettings
            // 
            this.pgFlickrSettings.CommandsVisibleIfAvailable = false;
            this.pgFlickrSettings.Location = new System.Drawing.Point(16, 160);
            this.pgFlickrSettings.Name = "pgFlickrSettings";
            this.pgFlickrSettings.PropertySort = System.Windows.Forms.PropertySort.Alphabetical;
            this.pgFlickrSettings.Size = new System.Drawing.Size(584, 240);
            this.pgFlickrSettings.TabIndex = 5;
            this.pgFlickrSettings.ToolbarVisible = false;
            // 
            // btnFlickrCheckToken
            // 
            this.btnFlickrCheckToken.Location = new System.Drawing.Point(368, 16);
            this.btnFlickrCheckToken.Name = "btnFlickrCheckToken";
            this.btnFlickrCheckToken.Size = new System.Drawing.Size(96, 23);
            this.btnFlickrCheckToken.TabIndex = 4;
            this.btnFlickrCheckToken.Text = "Check token";
            this.ttZScreen.SetToolTip(this.btnFlickrCheckToken, "Returns the credentials attached to an authentication token.");
            this.btnFlickrCheckToken.UseVisualStyleBackColor = true;
            this.btnFlickrCheckToken.Click += new System.EventHandler(this.btnFlickrCheckToken_Click);
            // 
            // btnFlickrGetToken
            // 
            this.btnFlickrGetToken.Enabled = false;
            this.btnFlickrGetToken.Location = new System.Drawing.Point(192, 16);
            this.btnFlickrGetToken.Name = "btnFlickrGetToken";
            this.btnFlickrGetToken.Size = new System.Drawing.Size(168, 24);
            this.btnFlickrGetToken.TabIndex = 1;
            this.btnFlickrGetToken.Text = "Complete authentication";
            this.ttZScreen.SetToolTip(this.btnFlickrGetToken, "Returns the auth token for the given frob, if one has been attached.");
            this.btnFlickrGetToken.UseVisualStyleBackColor = true;
            this.btnFlickrGetToken.Click += new System.EventHandler(this.btnFlickrGetToken_Click);
            // 
            // btnFlickrGetFrob
            // 
            this.btnFlickrGetFrob.Location = new System.Drawing.Point(16, 16);
            this.btnFlickrGetFrob.Name = "btnFlickrGetFrob";
            this.btnFlickrGetFrob.Size = new System.Drawing.Size(168, 23);
            this.btnFlickrGetFrob.TabIndex = 0;
            this.btnFlickrGetFrob.Text = "Open authentication page";
            this.ttZScreen.SetToolTip(this.btnFlickrGetFrob, "Returns a frob to be used during authentication.");
            this.btnFlickrGetFrob.UseVisualStyleBackColor = true;
            this.btnFlickrGetFrob.Click += new System.EventHandler(this.btnFlickrGetFrob_Click);
            // 
            // tpHotkeys
            // 
            this.tpHotkeys.Controls.Add(this.lblHotkeyStatus);
            this.tpHotkeys.Controls.Add(this.dgvHotkeys);
            this.tpHotkeys.ImageKey = "keyboard.png";
            this.tpHotkeys.Location = new System.Drawing.Point(4, 23);
            this.tpHotkeys.Name = "tpHotkeys";
            this.tpHotkeys.Padding = new System.Windows.Forms.Padding(3);
            this.tpHotkeys.Size = new System.Drawing.Size(805, 442);
            this.tpHotkeys.TabIndex = 1;
            this.tpHotkeys.Text = "Hotkeys";
            this.tpHotkeys.UseVisualStyleBackColor = true;
            // 
            // lblHotkeyStatus
            // 
            this.lblHotkeyStatus.AutoSize = true;
            this.lblHotkeyStatus.BackColor = System.Drawing.Color.Transparent;
            this.lblHotkeyStatus.Location = new System.Drawing.Point(29, 24);
            this.lblHotkeyStatus.Name = "lblHotkeyStatus";
            this.lblHotkeyStatus.Size = new System.Drawing.Size(120, 13);
            this.lblHotkeyStatus.TabIndex = 68;
            this.lblHotkeyStatus.Text = "Click on a Hotkey to set";
            // 
            // dgvHotkeys
            // 
            this.dgvHotkeys.AllowUserToAddRows = false;
            this.dgvHotkeys.AllowUserToDeleteRows = false;
            this.dgvHotkeys.AllowUserToResizeColumns = false;
            this.dgvHotkeys.AllowUserToResizeRows = false;
            this.dgvHotkeys.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvHotkeys.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgvHotkeys.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvHotkeys.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.Disable;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvHotkeys.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvHotkeys.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvHotkeys.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.chHotkeys_Description,
            this.chHotkeys_Keys});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvHotkeys.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvHotkeys.Location = new System.Drawing.Point(26, 50);
            this.dgvHotkeys.MultiSelect = false;
            this.dgvHotkeys.Name = "dgvHotkeys";
            this.dgvHotkeys.ReadOnly = true;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvHotkeys.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvHotkeys.RowHeadersVisible = false;
            this.dgvHotkeys.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgvHotkeys.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dgvHotkeys.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgvHotkeys.Size = new System.Drawing.Size(462, 377);
            this.dgvHotkeys.TabIndex = 67;
            this.dgvHotkeys.Leave += new System.EventHandler(this.dgvHotkeys_Leave);
            this.dgvHotkeys.CellMouseEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvHotkeys_CellMouseEnter);
            this.dgvHotkeys.MouseLeave += new System.EventHandler(this.dgvHotkeys_MouseLeave);
            this.dgvHotkeys.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvHotkeys_CellClick);
            this.dgvHotkeys.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgvHotkeys_KeyDown);
            // 
            // chHotkeys_Description
            // 
            this.chHotkeys_Description.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.chHotkeys_Description.HeaderText = "Description";
            this.chHotkeys_Description.Name = "chHotkeys_Description";
            this.chHotkeys_Description.ReadOnly = true;
            this.chHotkeys_Description.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.chHotkeys_Description.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // chHotkeys_Keys
            // 
            this.chHotkeys_Keys.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.chHotkeys_Keys.HeaderText = "Keys";
            this.chHotkeys_Keys.Name = "chHotkeys_Keys";
            this.chHotkeys_Keys.ReadOnly = true;
            this.chHotkeys_Keys.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // tpScreenshots
            // 
            this.tpScreenshots.Controls.Add(this.tcScreenshots);
            this.tpScreenshots.ImageKey = "monitor.png";
            this.tpScreenshots.Location = new System.Drawing.Point(4, 23);
            this.tpScreenshots.Name = "tpScreenshots";
            this.tpScreenshots.Padding = new System.Windows.Forms.Padding(3);
            this.tpScreenshots.Size = new System.Drawing.Size(805, 442);
            this.tpScreenshots.TabIndex = 4;
            this.tpScreenshots.Text = "Screenshots";
            this.tpScreenshots.UseVisualStyleBackColor = true;
            // 
            // tcScreenshots
            // 
            this.tcScreenshots.Controls.Add(this.tpCropShot);
            this.tcScreenshots.Controls.Add(this.tpSelectedWindow);
            this.tcScreenshots.Controls.Add(this.tpWatermark);
            this.tcScreenshots.Controls.Add(this.tpFileNaming);
            this.tcScreenshots.Controls.Add(this.tpCaptureQuality);
            this.tcScreenshots.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcScreenshots.ImageList = this.ilApp;
            this.tcScreenshots.Location = new System.Drawing.Point(3, 3);
            this.tcScreenshots.Name = "tcScreenshots";
            this.tcScreenshots.SelectedIndex = 0;
            this.tcScreenshots.Size = new System.Drawing.Size(799, 436);
            this.tcScreenshots.TabIndex = 77;
            // 
            // tpCropShot
            // 
            this.tpCropShot.Controls.Add(this.gbDynamicRegionBorderColorSettings);
            this.tpCropShot.Controls.Add(this.gbDynamicCrosshair);
            this.tpCropShot.Controls.Add(this.gpCropRegion);
            this.tpCropShot.Controls.Add(this.gbCropRegionSettings);
            this.tpCropShot.Controls.Add(this.gbCrosshairSettings);
            this.tpCropShot.Controls.Add(this.gbGridMode);
            this.tpCropShot.ImageKey = "shape_square.png";
            this.tpCropShot.Location = new System.Drawing.Point(4, 23);
            this.tpCropShot.Name = "tpCropShot";
            this.tpCropShot.Padding = new System.Windows.Forms.Padding(3);
            this.tpCropShot.Size = new System.Drawing.Size(791, 409);
            this.tpCropShot.TabIndex = 7;
            this.tpCropShot.Text = "Crop Shot";
            this.tpCropShot.UseVisualStyleBackColor = true;
            // 
            // gbDynamicRegionBorderColorSettings
            // 
            this.gbDynamicRegionBorderColorSettings.Controls.Add(this.nudCropRegionStep);
            this.gbDynamicRegionBorderColorSettings.Controls.Add(this.nudCropHueRange);
            this.gbDynamicRegionBorderColorSettings.Controls.Add(this.cbCropDynamicBorderColor);
            this.gbDynamicRegionBorderColorSettings.Controls.Add(this.lblCropRegionInterval);
            this.gbDynamicRegionBorderColorSettings.Controls.Add(this.lblCropHueRange);
            this.gbDynamicRegionBorderColorSettings.Controls.Add(this.lblCropRegionStep);
            this.gbDynamicRegionBorderColorSettings.Controls.Add(this.nudCropRegionInterval);
            this.gbDynamicRegionBorderColorSettings.Location = new System.Drawing.Point(368, 264);
            this.gbDynamicRegionBorderColorSettings.Name = "gbDynamicRegionBorderColorSettings";
            this.gbDynamicRegionBorderColorSettings.Size = new System.Drawing.Size(392, 112);
            this.gbDynamicRegionBorderColorSettings.TabIndex = 123;
            this.gbDynamicRegionBorderColorSettings.TabStop = false;
            this.gbDynamicRegionBorderColorSettings.Text = "Dynamic Region Border Color Settings";
            // 
            // nudCropRegionStep
            // 
            this.nudCropRegionStep.Location = new System.Drawing.Point(320, 44);
            this.nudCropRegionStep.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudCropRegionStep.Name = "nudCropRegionStep";
            this.nudCropRegionStep.Size = new System.Drawing.Size(56, 20);
            this.nudCropRegionStep.TabIndex = 31;
            this.nudCropRegionStep.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudCropRegionStep.ValueChanged += new System.EventHandler(this.nudCropRegionStep_ValueChanged);
            // 
            // nudCropHueRange
            // 
            this.nudCropHueRange.Location = new System.Drawing.Point(320, 76);
            this.nudCropHueRange.Maximum = new decimal(new int[] {
            360,
            0,
            0,
            0});
            this.nudCropHueRange.Name = "nudCropHueRange";
            this.nudCropHueRange.Size = new System.Drawing.Size(56, 20);
            this.nudCropHueRange.TabIndex = 33;
            this.nudCropHueRange.ValueChanged += new System.EventHandler(this.nudCropHueRange_ValueChanged);
            // 
            // cbCropDynamicBorderColor
            // 
            this.cbCropDynamicBorderColor.AutoSize = true;
            this.cbCropDynamicBorderColor.Location = new System.Drawing.Point(16, 24);
            this.cbCropDynamicBorderColor.Name = "cbCropDynamicBorderColor";
            this.cbCropDynamicBorderColor.Size = new System.Drawing.Size(65, 17);
            this.cbCropDynamicBorderColor.TabIndex = 27;
            this.cbCropDynamicBorderColor.Text = "Enabled";
            this.cbCropDynamicBorderColor.UseVisualStyleBackColor = true;
            this.cbCropDynamicBorderColor.CheckedChanged += new System.EventHandler(this.cbCropDynamicBorderColor_CheckedChanged);
            // 
            // lblCropRegionInterval
            // 
            this.lblCropRegionInterval.AutoSize = true;
            this.lblCropRegionInterval.Location = new System.Drawing.Point(176, 48);
            this.lblCropRegionInterval.Name = "lblCropRegionInterval";
            this.lblCropRegionInterval.Size = new System.Drawing.Size(45, 13);
            this.lblCropRegionInterval.TabIndex = 28;
            this.lblCropRegionInterval.Text = "Interval:";
            // 
            // lblCropHueRange
            // 
            this.lblCropHueRange.AutoSize = true;
            this.lblCropHueRange.Location = new System.Drawing.Point(256, 80);
            this.lblCropHueRange.Name = "lblCropHueRange";
            this.lblCropHueRange.Size = new System.Drawing.Size(60, 13);
            this.lblCropHueRange.TabIndex = 32;
            this.lblCropHueRange.Text = "Hue range:";
            // 
            // lblCropRegionStep
            // 
            this.lblCropRegionStep.AutoSize = true;
            this.lblCropRegionStep.Location = new System.Drawing.Point(286, 48);
            this.lblCropRegionStep.Name = "lblCropRegionStep";
            this.lblCropRegionStep.Size = new System.Drawing.Size(32, 13);
            this.lblCropRegionStep.TabIndex = 29;
            this.lblCropRegionStep.Text = "Step:";
            // 
            // nudCropRegionInterval
            // 
            this.nudCropRegionInterval.Location = new System.Drawing.Point(224, 44);
            this.nudCropRegionInterval.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.nudCropRegionInterval.Name = "nudCropRegionInterval";
            this.nudCropRegionInterval.Size = new System.Drawing.Size(56, 20);
            this.nudCropRegionInterval.TabIndex = 30;
            this.nudCropRegionInterval.ValueChanged += new System.EventHandler(this.nudCropRegionInterval_ValueChanged);
            // 
            // gbDynamicCrosshair
            // 
            this.gbDynamicCrosshair.Controls.Add(this.cbCropDynamicCrosshair);
            this.gbDynamicCrosshair.Controls.Add(this.lblCropCrosshairStep);
            this.gbDynamicCrosshair.Controls.Add(this.lblCropCrosshairInterval);
            this.gbDynamicCrosshair.Controls.Add(this.nudCropCrosshairInterval);
            this.gbDynamicCrosshair.Controls.Add(this.nudCropCrosshairStep);
            this.gbDynamicCrosshair.Location = new System.Drawing.Point(8, 264);
            this.gbDynamicCrosshair.Name = "gbDynamicCrosshair";
            this.gbDynamicCrosshair.Size = new System.Drawing.Size(352, 112);
            this.gbDynamicCrosshair.TabIndex = 122;
            this.gbDynamicCrosshair.TabStop = false;
            this.gbDynamicCrosshair.Text = "Dynamic Crosshair Settings";
            // 
            // cbCropDynamicCrosshair
            // 
            this.cbCropDynamicCrosshair.AutoSize = true;
            this.cbCropDynamicCrosshair.Location = new System.Drawing.Point(16, 24);
            this.cbCropDynamicCrosshair.Name = "cbCropDynamicCrosshair";
            this.cbCropDynamicCrosshair.Size = new System.Drawing.Size(65, 17);
            this.cbCropDynamicCrosshair.TabIndex = 16;
            this.cbCropDynamicCrosshair.Text = "Enabled";
            this.cbCropDynamicCrosshair.UseVisualStyleBackColor = true;
            this.cbCropDynamicCrosshair.CheckedChanged += new System.EventHandler(this.cbCropDynamicCrosshair_CheckedChanged);
            // 
            // lblCropCrosshairStep
            // 
            this.lblCropCrosshairStep.AutoSize = true;
            this.lblCropCrosshairStep.Location = new System.Drawing.Point(248, 52);
            this.lblCropCrosshairStep.Name = "lblCropCrosshairStep";
            this.lblCropCrosshairStep.Size = new System.Drawing.Size(32, 13);
            this.lblCropCrosshairStep.TabIndex = 22;
            this.lblCropCrosshairStep.Text = "Step:";
            // 
            // lblCropCrosshairInterval
            // 
            this.lblCropCrosshairInterval.AutoSize = true;
            this.lblCropCrosshairInterval.Location = new System.Drawing.Point(136, 52);
            this.lblCropCrosshairInterval.Name = "lblCropCrosshairInterval";
            this.lblCropCrosshairInterval.Size = new System.Drawing.Size(45, 13);
            this.lblCropCrosshairInterval.TabIndex = 21;
            this.lblCropCrosshairInterval.Text = "Interval:";
            // 
            // nudCropCrosshairInterval
            // 
            this.nudCropCrosshairInterval.Location = new System.Drawing.Point(184, 48);
            this.nudCropCrosshairInterval.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.nudCropCrosshairInterval.Name = "nudCropCrosshairInterval";
            this.nudCropCrosshairInterval.Size = new System.Drawing.Size(56, 20);
            this.nudCropCrosshairInterval.TabIndex = 23;
            this.nudCropCrosshairInterval.ValueChanged += new System.EventHandler(this.nudCropInterval_ValueChanged);
            // 
            // nudCropCrosshairStep
            // 
            this.nudCropCrosshairStep.Location = new System.Drawing.Point(280, 48);
            this.nudCropCrosshairStep.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudCropCrosshairStep.Name = "nudCropCrosshairStep";
            this.nudCropCrosshairStep.Size = new System.Drawing.Size(56, 20);
            this.nudCropCrosshairStep.TabIndex = 24;
            this.nudCropCrosshairStep.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudCropCrosshairStep.ValueChanged += new System.EventHandler(this.nudCropStep_ValueChanged);
            // 
            // gpCropRegion
            // 
            this.gpCropRegion.Controls.Add(this.lblCropRegionStyle);
            this.gpCropRegion.Controls.Add(this.cbRegionHotkeyInfo);
            this.gpCropRegion.Controls.Add(this.cbCropStyle);
            this.gpCropRegion.Controls.Add(this.cbRegionRectangleInfo);
            this.gpCropRegion.Location = new System.Drawing.Point(8, 16);
            this.gpCropRegion.Name = "gpCropRegion";
            this.gpCropRegion.Size = new System.Drawing.Size(352, 120);
            this.gpCropRegion.TabIndex = 121;
            this.gpCropRegion.TabStop = false;
            this.gpCropRegion.Text = "Crop Region Settings";
            // 
            // lblCropRegionStyle
            // 
            this.lblCropRegionStyle.AutoSize = true;
            this.lblCropRegionStyle.Location = new System.Drawing.Point(16, 28);
            this.lblCropRegionStyle.Name = "lblCropRegionStyle";
            this.lblCropRegionStyle.Size = new System.Drawing.Size(88, 13);
            this.lblCropRegionStyle.TabIndex = 9;
            this.lblCropRegionStyle.Text = "Crop region style:";
            // 
            // cbRegionHotkeyInfo
            // 
            this.cbRegionHotkeyInfo.AutoSize = true;
            this.cbRegionHotkeyInfo.Location = new System.Drawing.Point(16, 88);
            this.cbRegionHotkeyInfo.Name = "cbRegionHotkeyInfo";
            this.cbRegionHotkeyInfo.Size = new System.Drawing.Size(200, 17);
            this.cbRegionHotkeyInfo.TabIndex = 6;
            this.cbRegionHotkeyInfo.Text = "Show crop region hotkey instructions";
            this.cbRegionHotkeyInfo.UseVisualStyleBackColor = true;
            this.cbRegionHotkeyInfo.CheckedChanged += new System.EventHandler(this.cbRegionHotkeyInfo_CheckedChanged);
            // 
            // cbCropStyle
            // 
            this.cbCropStyle.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbCropStyle.FormattingEnabled = true;
            this.cbCropStyle.Location = new System.Drawing.Point(120, 24);
            this.cbCropStyle.Name = "cbCropStyle";
            this.cbCropStyle.Size = new System.Drawing.Size(216, 21);
            this.cbCropStyle.TabIndex = 8;
            this.cbCropStyle.SelectedIndexChanged += new System.EventHandler(this.cbCropStyle_SelectedIndexChanged);
            // 
            // cbRegionRectangleInfo
            // 
            this.cbRegionRectangleInfo.AutoSize = true;
            this.cbRegionRectangleInfo.Location = new System.Drawing.Point(16, 64);
            this.cbRegionRectangleInfo.Name = "cbRegionRectangleInfo";
            this.cbRegionRectangleInfo.Size = new System.Drawing.Size(209, 17);
            this.cbRegionRectangleInfo.TabIndex = 5;
            this.cbRegionRectangleInfo.Text = "Show crop region coordinates and size";
            this.cbRegionRectangleInfo.UseVisualStyleBackColor = true;
            this.cbRegionRectangleInfo.CheckedChanged += new System.EventHandler(this.cbRegionRectangleInfo_CheckedChanged);
            // 
            // gbCropRegionSettings
            // 
            this.gbCropRegionSettings.Controls.Add(this.lblCropBorderSize);
            this.gbCropRegionSettings.Controls.Add(this.cbShowCropRuler);
            this.gbCropRegionSettings.Controls.Add(this.cbCropShowGrids);
            this.gbCropRegionSettings.Controls.Add(this.lblCropBorderColor);
            this.gbCropRegionSettings.Controls.Add(this.pbCropBorderColor);
            this.gbCropRegionSettings.Controls.Add(this.nudCropBorderSize);
            this.gbCropRegionSettings.Location = new System.Drawing.Point(368, 144);
            this.gbCropRegionSettings.Name = "gbCropRegionSettings";
            this.gbCropRegionSettings.Size = new System.Drawing.Size(392, 112);
            this.gbCropRegionSettings.TabIndex = 27;
            this.gbCropRegionSettings.TabStop = false;
            this.gbCropRegionSettings.Text = "Region Settings";
            // 
            // lblCropBorderSize
            // 
            this.lblCropBorderSize.AutoSize = true;
            this.lblCropBorderSize.Location = new System.Drawing.Point(248, 28);
            this.lblCropBorderSize.Name = "lblCropBorderSize";
            this.lblCropBorderSize.Size = new System.Drawing.Size(62, 13);
            this.lblCropBorderSize.TabIndex = 11;
            this.lblCropBorderSize.Text = "Border size:";
            // 
            // cbShowCropRuler
            // 
            this.cbShowCropRuler.AutoSize = true;
            this.cbShowCropRuler.Location = new System.Drawing.Point(16, 24);
            this.cbShowCropRuler.Name = "cbShowCropRuler";
            this.cbShowCropRuler.Size = new System.Drawing.Size(76, 17);
            this.cbShowCropRuler.TabIndex = 26;
            this.cbShowCropRuler.Text = "Show ruler";
            this.cbShowCropRuler.UseVisualStyleBackColor = true;
            this.cbShowCropRuler.CheckedChanged += new System.EventHandler(this.cbShowCropRuler_CheckedChanged);
            // 
            // cbCropShowGrids
            // 
            this.cbCropShowGrids.AutoSize = true;
            this.cbCropShowGrids.Location = new System.Drawing.Point(16, 48);
            this.cbCropShowGrids.Name = "cbCropShowGrids";
            this.cbCropShowGrids.Size = new System.Drawing.Size(206, 17);
            this.cbCropShowGrids.TabIndex = 13;
            this.cbCropShowGrids.Text = "Show grid when possible in Grid Mode";
            this.cbCropShowGrids.UseVisualStyleBackColor = true;
            this.cbCropShowGrids.CheckedChanged += new System.EventHandler(this.cbCropShowGrids_CheckedChanged);
            // 
            // lblCropBorderColor
            // 
            this.lblCropBorderColor.AutoSize = true;
            this.lblCropBorderColor.Location = new System.Drawing.Point(248, 56);
            this.lblCropBorderColor.Name = "lblCropBorderColor";
            this.lblCropBorderColor.Size = new System.Drawing.Size(67, 13);
            this.lblCropBorderColor.TabIndex = 10;
            this.lblCropBorderColor.Text = "Border color:";
            // 
            // pbCropBorderColor
            // 
            this.pbCropBorderColor.BackColor = System.Drawing.Color.White;
            this.pbCropBorderColor.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pbCropBorderColor.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pbCropBorderColor.Location = new System.Drawing.Point(320, 52);
            this.pbCropBorderColor.Name = "pbCropBorderColor";
            this.pbCropBorderColor.Size = new System.Drawing.Size(56, 20);
            this.pbCropBorderColor.TabIndex = 9;
            this.pbCropBorderColor.TabStop = false;
            this.pbCropBorderColor.Click += new System.EventHandler(this.pbCropBorderColor_Click);
            // 
            // nudCropBorderSize
            // 
            this.nudCropBorderSize.Location = new System.Drawing.Point(320, 24);
            this.nudCropBorderSize.Name = "nudCropBorderSize";
            this.nudCropBorderSize.Size = new System.Drawing.Size(56, 20);
            this.nudCropBorderSize.TabIndex = 12;
            this.nudCropBorderSize.ValueChanged += new System.EventHandler(this.nudCropBorderSize_ValueChanged);
            // 
            // gbCrosshairSettings
            // 
            this.gbCrosshairSettings.Controls.Add(this.cbCropShowMagnifyingGlass);
            this.gbCrosshairSettings.Controls.Add(this.cbCropShowBigCross);
            this.gbCrosshairSettings.Controls.Add(this.pbCropCrosshairColor);
            this.gbCrosshairSettings.Controls.Add(this.lblCropCrosshairColor);
            this.gbCrosshairSettings.Controls.Add(this.nudCrosshairLineCount);
            this.gbCrosshairSettings.Controls.Add(this.nudCrosshairLineSize);
            this.gbCrosshairSettings.Controls.Add(this.lblCrosshairLineSize);
            this.gbCrosshairSettings.Controls.Add(this.lblCrosshairLineCount);
            this.gbCrosshairSettings.Location = new System.Drawing.Point(8, 144);
            this.gbCrosshairSettings.Name = "gbCrosshairSettings";
            this.gbCrosshairSettings.Size = new System.Drawing.Size(352, 112);
            this.gbCrosshairSettings.TabIndex = 25;
            this.gbCrosshairSettings.TabStop = false;
            this.gbCrosshairSettings.Text = "Crosshair Settings";
            // 
            // cbCropShowMagnifyingGlass
            // 
            this.cbCropShowMagnifyingGlass.AutoSize = true;
            this.cbCropShowMagnifyingGlass.Location = new System.Drawing.Point(16, 48);
            this.cbCropShowMagnifyingGlass.Name = "cbCropShowMagnifyingGlass";
            this.cbCropShowMagnifyingGlass.Size = new System.Drawing.Size(133, 17);
            this.cbCropShowMagnifyingGlass.TabIndex = 26;
            this.cbCropShowMagnifyingGlass.Text = "Show magnifying glass";
            this.cbCropShowMagnifyingGlass.UseVisualStyleBackColor = true;
            this.cbCropShowMagnifyingGlass.CheckedChanged += new System.EventHandler(this.cbCropShowMagnifyingGlass_CheckedChanged);
            // 
            // cbCropShowBigCross
            // 
            this.cbCropShowBigCross.AutoSize = true;
            this.cbCropShowBigCross.Location = new System.Drawing.Point(16, 24);
            this.cbCropShowBigCross.Name = "cbCropShowBigCross";
            this.cbCropShowBigCross.Size = new System.Drawing.Size(194, 17);
            this.cbCropShowBigCross.TabIndex = 25;
            this.cbCropShowBigCross.Text = "Show second crosshair ( Big cross )";
            this.cbCropShowBigCross.UseVisualStyleBackColor = true;
            this.cbCropShowBigCross.CheckedChanged += new System.EventHandler(this.cbCropShowBigCross_CheckedChanged);
            // 
            // pbCropCrosshairColor
            // 
            this.pbCropCrosshairColor.BackColor = System.Drawing.Color.White;
            this.pbCropCrosshairColor.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pbCropCrosshairColor.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pbCropCrosshairColor.Location = new System.Drawing.Point(280, 80);
            this.pbCropCrosshairColor.Name = "pbCropCrosshairColor";
            this.pbCropCrosshairColor.Size = new System.Drawing.Size(56, 20);
            this.pbCropCrosshairColor.TabIndex = 14;
            this.pbCropCrosshairColor.TabStop = false;
            this.pbCropCrosshairColor.Click += new System.EventHandler(this.pbCropCrosshairColor_Click);
            // 
            // lblCropCrosshairColor
            // 
            this.lblCropCrosshairColor.AutoSize = true;
            this.lblCropCrosshairColor.Location = new System.Drawing.Point(240, 84);
            this.lblCropCrosshairColor.Name = "lblCropCrosshairColor";
            this.lblCropCrosshairColor.Size = new System.Drawing.Size(34, 13);
            this.lblCropCrosshairColor.TabIndex = 15;
            this.lblCropCrosshairColor.Text = "Color:";
            // 
            // nudCrosshairLineCount
            // 
            this.nudCrosshairLineCount.Location = new System.Drawing.Point(280, 24);
            this.nudCrosshairLineCount.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nudCrosshairLineCount.Name = "nudCrosshairLineCount";
            this.nudCrosshairLineCount.Size = new System.Drawing.Size(56, 20);
            this.nudCrosshairLineCount.TabIndex = 17;
            this.nudCrosshairLineCount.ValueChanged += new System.EventHandler(this.nudCrosshairLineCount_ValueChanged);
            // 
            // nudCrosshairLineSize
            // 
            this.nudCrosshairLineSize.Location = new System.Drawing.Point(280, 52);
            this.nudCrosshairLineSize.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nudCrosshairLineSize.Name = "nudCrosshairLineSize";
            this.nudCrosshairLineSize.Size = new System.Drawing.Size(56, 20);
            this.nudCrosshairLineSize.TabIndex = 18;
            this.nudCrosshairLineSize.ValueChanged += new System.EventHandler(this.nudCrosshairLineSize_ValueChanged);
            // 
            // lblCrosshairLineSize
            // 
            this.lblCrosshairLineSize.AutoSize = true;
            this.lblCrosshairLineSize.Location = new System.Drawing.Point(224, 56);
            this.lblCrosshairLineSize.Name = "lblCrosshairLineSize";
            this.lblCrosshairLineSize.Size = new System.Drawing.Size(51, 13);
            this.lblCrosshairLineSize.TabIndex = 20;
            this.lblCrosshairLineSize.Text = "Line size:";
            // 
            // lblCrosshairLineCount
            // 
            this.lblCrosshairLineCount.AutoSize = true;
            this.lblCrosshairLineCount.Location = new System.Drawing.Point(216, 28);
            this.lblCrosshairLineCount.Name = "lblCrosshairLineCount";
            this.lblCrosshairLineCount.Size = new System.Drawing.Size(60, 13);
            this.lblCrosshairLineCount.TabIndex = 19;
            this.lblCrosshairLineCount.Text = "Line count:";
            // 
            // gbGridMode
            // 
            this.gbGridMode.Controls.Add(this.cboCropGridMode);
            this.gbGridMode.Controls.Add(this.nudCropGridHeight);
            this.gbGridMode.Controls.Add(this.lblGridSizeWidth);
            this.gbGridMode.Controls.Add(this.lblGridSize);
            this.gbGridMode.Controls.Add(this.lblGridSizeHeight);
            this.gbGridMode.Controls.Add(this.nudCropGridWidth);
            this.gbGridMode.Location = new System.Drawing.Point(368, 16);
            this.gbGridMode.Name = "gbGridMode";
            this.gbGridMode.Size = new System.Drawing.Size(392, 120);
            this.gbGridMode.TabIndex = 120;
            this.gbGridMode.TabStop = false;
            this.gbGridMode.Tag = "With Grid Mode you can take screenshots of preset portions of the Screen";
            this.gbGridMode.Text = "Grid Mode Settings";
            // 
            // cboCropGridMode
            // 
            this.cboCropGridMode.AutoSize = true;
            this.cboCropGridMode.Location = new System.Drawing.Point(16, 24);
            this.cboCropGridMode.Name = "cboCropGridMode";
            this.cboCropGridMode.Size = new System.Drawing.Size(178, 17);
            this.cboCropGridMode.TabIndex = 119;
            this.cboCropGridMode.Text = "Activate Grid Mode in Crop Shot";
            this.cboCropGridMode.UseVisualStyleBackColor = true;
            this.cboCropGridMode.CheckedChanged += new System.EventHandler(this.cbCropGridMode_CheckedChanged);
            // 
            // nudCropGridHeight
            // 
            this.nudCropGridHeight.Location = new System.Drawing.Point(320, 64);
            this.nudCropGridHeight.Maximum = new decimal(new int[] {
            5000,
            0,
            0,
            0});
            this.nudCropGridHeight.Name = "nudCropGridHeight";
            this.nudCropGridHeight.Size = new System.Drawing.Size(56, 20);
            this.nudCropGridHeight.TabIndex = 15;
            this.nudCropGridHeight.ValueChanged += new System.EventHandler(this.nudCropGridHeight_ValueChanged);
            // 
            // lblGridSizeWidth
            // 
            this.lblGridSizeWidth.AutoSize = true;
            this.lblGridSizeWidth.Location = new System.Drawing.Point(176, 68);
            this.lblGridSizeWidth.Name = "lblGridSizeWidth";
            this.lblGridSizeWidth.Size = new System.Drawing.Size(35, 13);
            this.lblGridSizeWidth.TabIndex = 14;
            this.lblGridSizeWidth.Text = "Width";
            // 
            // lblGridSize
            // 
            this.lblGridSize.AutoSize = true;
            this.lblGridSize.Location = new System.Drawing.Point(48, 68);
            this.lblGridSize.Name = "lblGridSize";
            this.lblGridSize.Size = new System.Drawing.Size(117, 13);
            this.lblGridSize.TabIndex = 118;
            this.lblGridSize.Text = "Grid Size ( 0 = Disable )";
            // 
            // lblGridSizeHeight
            // 
            this.lblGridSizeHeight.AutoSize = true;
            this.lblGridSizeHeight.Location = new System.Drawing.Point(280, 68);
            this.lblGridSizeHeight.Name = "lblGridSizeHeight";
            this.lblGridSizeHeight.Size = new System.Drawing.Size(38, 13);
            this.lblGridSizeHeight.TabIndex = 16;
            this.lblGridSizeHeight.Text = "Height";
            // 
            // nudCropGridWidth
            // 
            this.nudCropGridWidth.Location = new System.Drawing.Point(216, 64);
            this.nudCropGridWidth.Maximum = new decimal(new int[] {
            5000,
            0,
            0,
            0});
            this.nudCropGridWidth.Name = "nudCropGridWidth";
            this.nudCropGridWidth.Size = new System.Drawing.Size(56, 20);
            this.nudCropGridWidth.TabIndex = 13;
            this.nudCropGridWidth.ValueChanged += new System.EventHandler(this.nudCropGridSize_ValueChanged);
            // 
            // tpSelectedWindow
            // 
            this.tpSelectedWindow.Controls.Add(this.cbSelectedWindowCaptureObjects);
            this.tpSelectedWindow.Controls.Add(this.nudSelectedWindowHueRange);
            this.tpSelectedWindow.Controls.Add(this.lblSelectedWindowHueRange);
            this.tpSelectedWindow.Controls.Add(this.nudSelectedWindowRegionStep);
            this.tpSelectedWindow.Controls.Add(this.nudSelectedWindowRegionInterval);
            this.tpSelectedWindow.Controls.Add(this.lblSelectedWindowRegionStep);
            this.tpSelectedWindow.Controls.Add(this.lblSelectedWindowRegionInterval);
            this.tpSelectedWindow.Controls.Add(this.cbSelectedWindowDynamicBorderColor);
            this.tpSelectedWindow.Controls.Add(this.cbSelectedWindowRuler);
            this.tpSelectedWindow.Controls.Add(this.lblSelectedWindowRegionStyle);
            this.tpSelectedWindow.Controls.Add(this.cbSelectedWindowStyle);
            this.tpSelectedWindow.Controls.Add(this.cbSelectedWindowRectangleInfo);
            this.tpSelectedWindow.Controls.Add(this.lblSelectedWindowBorderColor);
            this.tpSelectedWindow.Controls.Add(this.nudSelectedWindowBorderSize);
            this.tpSelectedWindow.Controls.Add(this.lblSelectedWindowBorderSize);
            this.tpSelectedWindow.Controls.Add(this.pbSelectedWindowBorderColor);
            this.tpSelectedWindow.ImageKey = "application_double.png";
            this.tpSelectedWindow.Location = new System.Drawing.Point(4, 23);
            this.tpSelectedWindow.Name = "tpSelectedWindow";
            this.tpSelectedWindow.Size = new System.Drawing.Size(791, 409);
            this.tpSelectedWindow.TabIndex = 6;
            this.tpSelectedWindow.Text = "Selected Window";
            this.tpSelectedWindow.UseVisualStyleBackColor = true;
            // 
            // cbSelectedWindowCaptureObjects
            // 
            this.cbSelectedWindowCaptureObjects.AutoSize = true;
            this.cbSelectedWindowCaptureObjects.Location = new System.Drawing.Point(16, 272);
            this.cbSelectedWindowCaptureObjects.Name = "cbSelectedWindowCaptureObjects";
            this.cbSelectedWindowCaptureObjects.Size = new System.Drawing.Size(231, 17);
            this.cbSelectedWindowCaptureObjects.TabIndex = 42;
            this.cbSelectedWindowCaptureObjects.Text = "Capture control objects within each window";
            this.cbSelectedWindowCaptureObjects.UseVisualStyleBackColor = true;
            this.cbSelectedWindowCaptureObjects.CheckedChanged += new System.EventHandler(this.cbSelectedWindowCaptureObjects_CheckedChanged);
            // 
            // nudSelectedWindowHueRange
            // 
            this.nudSelectedWindowHueRange.Location = new System.Drawing.Point(216, 232);
            this.nudSelectedWindowHueRange.Maximum = new decimal(new int[] {
            360,
            0,
            0,
            0});
            this.nudSelectedWindowHueRange.Name = "nudSelectedWindowHueRange";
            this.nudSelectedWindowHueRange.Size = new System.Drawing.Size(56, 20);
            this.nudSelectedWindowHueRange.TabIndex = 40;
            this.nudSelectedWindowHueRange.ValueChanged += new System.EventHandler(this.nudSelectedWindowHueRange_ValueChanged);
            // 
            // lblSelectedWindowHueRange
            // 
            this.lblSelectedWindowHueRange.AutoSize = true;
            this.lblSelectedWindowHueRange.Location = new System.Drawing.Point(16, 236);
            this.lblSelectedWindowHueRange.Name = "lblSelectedWindowHueRange";
            this.lblSelectedWindowHueRange.Size = new System.Drawing.Size(193, 13);
            this.lblSelectedWindowHueRange.TabIndex = 39;
            this.lblSelectedWindowHueRange.Text = "Dynamic region border color hue range:";
            // 
            // nudSelectedWindowRegionStep
            // 
            this.nudSelectedWindowRegionStep.Location = new System.Drawing.Point(163, 200);
            this.nudSelectedWindowRegionStep.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudSelectedWindowRegionStep.Name = "nudSelectedWindowRegionStep";
            this.nudSelectedWindowRegionStep.Size = new System.Drawing.Size(56, 20);
            this.nudSelectedWindowRegionStep.TabIndex = 38;
            this.nudSelectedWindowRegionStep.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudSelectedWindowRegionStep.ValueChanged += new System.EventHandler(this.nudSelectedWindowRegionStep_ValueChanged);
            // 
            // nudSelectedWindowRegionInterval
            // 
            this.nudSelectedWindowRegionInterval.Location = new System.Drawing.Point(64, 200);
            this.nudSelectedWindowRegionInterval.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.nudSelectedWindowRegionInterval.Name = "nudSelectedWindowRegionInterval";
            this.nudSelectedWindowRegionInterval.Size = new System.Drawing.Size(56, 20);
            this.nudSelectedWindowRegionInterval.TabIndex = 37;
            this.nudSelectedWindowRegionInterval.ValueChanged += new System.EventHandler(this.nudSelectedWindowRegionInterval_ValueChanged);
            // 
            // lblSelectedWindowRegionStep
            // 
            this.lblSelectedWindowRegionStep.AutoSize = true;
            this.lblSelectedWindowRegionStep.Location = new System.Drawing.Point(128, 203);
            this.lblSelectedWindowRegionStep.Name = "lblSelectedWindowRegionStep";
            this.lblSelectedWindowRegionStep.Size = new System.Drawing.Size(32, 13);
            this.lblSelectedWindowRegionStep.TabIndex = 36;
            this.lblSelectedWindowRegionStep.Text = "Step:";
            // 
            // lblSelectedWindowRegionInterval
            // 
            this.lblSelectedWindowRegionInterval.AutoSize = true;
            this.lblSelectedWindowRegionInterval.Location = new System.Drawing.Point(16, 203);
            this.lblSelectedWindowRegionInterval.Name = "lblSelectedWindowRegionInterval";
            this.lblSelectedWindowRegionInterval.Size = new System.Drawing.Size(45, 13);
            this.lblSelectedWindowRegionInterval.TabIndex = 35;
            this.lblSelectedWindowRegionInterval.Text = "Interval:";
            // 
            // cbSelectedWindowDynamicBorderColor
            // 
            this.cbSelectedWindowDynamicBorderColor.AutoSize = true;
            this.cbSelectedWindowDynamicBorderColor.Location = new System.Drawing.Point(16, 168);
            this.cbSelectedWindowDynamicBorderColor.Name = "cbSelectedWindowDynamicBorderColor";
            this.cbSelectedWindowDynamicBorderColor.Size = new System.Drawing.Size(158, 17);
            this.cbSelectedWindowDynamicBorderColor.TabIndex = 34;
            this.cbSelectedWindowDynamicBorderColor.Text = "Dynamic region border color";
            this.cbSelectedWindowDynamicBorderColor.UseVisualStyleBackColor = true;
            this.cbSelectedWindowDynamicBorderColor.CheckedChanged += new System.EventHandler(this.cbSelectedWindowDynamicBorderColor_CheckedChanged);
            // 
            // cbSelectedWindowRuler
            // 
            this.cbSelectedWindowRuler.AutoSize = true;
            this.cbSelectedWindowRuler.Location = new System.Drawing.Point(16, 72);
            this.cbSelectedWindowRuler.Name = "cbSelectedWindowRuler";
            this.cbSelectedWindowRuler.Size = new System.Drawing.Size(76, 17);
            this.cbSelectedWindowRuler.TabIndex = 12;
            this.cbSelectedWindowRuler.Text = "Show ruler";
            this.cbSelectedWindowRuler.UseVisualStyleBackColor = true;
            this.cbSelectedWindowRuler.CheckedChanged += new System.EventHandler(this.cbSelectedWindowRuler_CheckedChanged);
            // 
            // lblSelectedWindowRegionStyle
            // 
            this.lblSelectedWindowRegionStyle.AutoSize = true;
            this.lblSelectedWindowRegionStyle.Location = new System.Drawing.Point(16, 20);
            this.lblSelectedWindowRegionStyle.Name = "lblSelectedWindowRegionStyle";
            this.lblSelectedWindowRegionStyle.Size = new System.Drawing.Size(147, 13);
            this.lblSelectedWindowRegionStyle.TabIndex = 11;
            this.lblSelectedWindowRegionStyle.Text = "Selected window region style:";
            // 
            // cbSelectedWindowStyle
            // 
            this.cbSelectedWindowStyle.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbSelectedWindowStyle.FormattingEnabled = true;
            this.cbSelectedWindowStyle.Location = new System.Drawing.Point(168, 16);
            this.cbSelectedWindowStyle.Name = "cbSelectedWindowStyle";
            this.cbSelectedWindowStyle.Size = new System.Drawing.Size(208, 21);
            this.cbSelectedWindowStyle.TabIndex = 10;
            this.cbSelectedWindowStyle.SelectedIndexChanged += new System.EventHandler(this.cbSelectedWindowStyle_SelectedIndexChanged);
            // 
            // cbSelectedWindowRectangleInfo
            // 
            this.cbSelectedWindowRectangleInfo.AutoSize = true;
            this.cbSelectedWindowRectangleInfo.Location = new System.Drawing.Point(16, 48);
            this.cbSelectedWindowRectangleInfo.Name = "cbSelectedWindowRectangleInfo";
            this.cbSelectedWindowRectangleInfo.Size = new System.Drawing.Size(267, 17);
            this.cbSelectedWindowRectangleInfo.TabIndex = 5;
            this.cbSelectedWindowRectangleInfo.Text = "Show selected window region coordinates and size";
            this.cbSelectedWindowRectangleInfo.UseVisualStyleBackColor = true;
            this.cbSelectedWindowRectangleInfo.CheckedChanged += new System.EventHandler(this.cbSelectedWindowRectangleInfo_CheckedChanged);
            // 
            // lblSelectedWindowBorderColor
            // 
            this.lblSelectedWindowBorderColor.AutoSize = true;
            this.lblSelectedWindowBorderColor.Location = new System.Drawing.Point(16, 104);
            this.lblSelectedWindowBorderColor.Name = "lblSelectedWindowBorderColor";
            this.lblSelectedWindowBorderColor.Size = new System.Drawing.Size(103, 13);
            this.lblSelectedWindowBorderColor.TabIndex = 1;
            this.lblSelectedWindowBorderColor.Text = "Region border color:";
            // 
            // nudSelectedWindowBorderSize
            // 
            this.nudSelectedWindowBorderSize.Location = new System.Drawing.Point(200, 136);
            this.nudSelectedWindowBorderSize.Name = "nudSelectedWindowBorderSize";
            this.nudSelectedWindowBorderSize.Size = new System.Drawing.Size(56, 20);
            this.nudSelectedWindowBorderSize.TabIndex = 4;
            this.nudSelectedWindowBorderSize.ValueChanged += new System.EventHandler(this.nudSelectedWindowBorderSize_ValueChanged);
            // 
            // lblSelectedWindowBorderSize
            // 
            this.lblSelectedWindowBorderSize.AutoSize = true;
            this.lblSelectedWindowBorderSize.Location = new System.Drawing.Point(16, 139);
            this.lblSelectedWindowBorderSize.Name = "lblSelectedWindowBorderSize";
            this.lblSelectedWindowBorderSize.Size = new System.Drawing.Size(175, 13);
            this.lblSelectedWindowBorderSize.TabIndex = 2;
            this.lblSelectedWindowBorderSize.Text = "Region border size ( 0 = No border )";
            // 
            // pbSelectedWindowBorderColor
            // 
            this.pbSelectedWindowBorderColor.BackColor = System.Drawing.Color.White;
            this.pbSelectedWindowBorderColor.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pbSelectedWindowBorderColor.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pbSelectedWindowBorderColor.Location = new System.Drawing.Point(128, 101);
            this.pbSelectedWindowBorderColor.Name = "pbSelectedWindowBorderColor";
            this.pbSelectedWindowBorderColor.Size = new System.Drawing.Size(56, 20);
            this.pbSelectedWindowBorderColor.TabIndex = 3;
            this.pbSelectedWindowBorderColor.TabStop = false;
            this.pbSelectedWindowBorderColor.Click += new System.EventHandler(this.pbSelectedWindowBorderColor_Click);
            // 
            // tpWatermark
            // 
            this.tpWatermark.Controls.Add(this.pbWatermarkShow);
            this.tpWatermark.Controls.Add(this.gbWatermarkGeneral);
            this.tpWatermark.Controls.Add(this.tcWatermark);
            this.tpWatermark.ImageKey = "tag_blue_edit.png";
            this.tpWatermark.Location = new System.Drawing.Point(4, 23);
            this.tpWatermark.Name = "tpWatermark";
            this.tpWatermark.Padding = new System.Windows.Forms.Padding(3);
            this.tpWatermark.Size = new System.Drawing.Size(791, 409);
            this.tpWatermark.TabIndex = 11;
            this.tpWatermark.Text = "Watermark";
            this.tpWatermark.UseVisualStyleBackColor = true;
            // 
            // pbWatermarkShow
            // 
            this.pbWatermarkShow.BackColor = System.Drawing.Color.White;
            this.pbWatermarkShow.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbWatermarkShow.Location = new System.Drawing.Point(8, 184);
            this.pbWatermarkShow.Name = "pbWatermarkShow";
            this.pbWatermarkShow.Size = new System.Drawing.Size(272, 200);
            this.pbWatermarkShow.TabIndex = 13;
            this.pbWatermarkShow.TabStop = false;
            // 
            // gbWatermarkGeneral
            // 
            this.gbWatermarkGeneral.Controls.Add(this.lblWatermarkOffsetPixel);
            this.gbWatermarkGeneral.Controls.Add(this.cboWatermarkType);
            this.gbWatermarkGeneral.Controls.Add(this.cbWatermarkAutoHide);
            this.gbWatermarkGeneral.Controls.Add(this.cbWatermarkAddReflection);
            this.gbWatermarkGeneral.Controls.Add(this.lblWatermarkType);
            this.gbWatermarkGeneral.Controls.Add(this.cbWatermarkPosition);
            this.gbWatermarkGeneral.Controls.Add(this.lblWatermarkPosition);
            this.gbWatermarkGeneral.Controls.Add(this.nudWatermarkOffset);
            this.gbWatermarkGeneral.Controls.Add(this.lblWatermarkOffset);
            this.gbWatermarkGeneral.Location = new System.Drawing.Point(8, 8);
            this.gbWatermarkGeneral.Name = "gbWatermarkGeneral";
            this.gbWatermarkGeneral.Size = new System.Drawing.Size(272, 168);
            this.gbWatermarkGeneral.TabIndex = 26;
            this.gbWatermarkGeneral.TabStop = false;
            this.gbWatermarkGeneral.Text = "Watermark Settings";
            // 
            // lblWatermarkOffsetPixel
            // 
            this.lblWatermarkOffsetPixel.AutoSize = true;
            this.lblWatermarkOffsetPixel.Location = new System.Drawing.Point(152, 88);
            this.lblWatermarkOffsetPixel.Name = "lblWatermarkOffsetPixel";
            this.lblWatermarkOffsetPixel.Size = new System.Drawing.Size(18, 13);
            this.lblWatermarkOffsetPixel.TabIndex = 34;
            this.lblWatermarkOffsetPixel.Text = "px";
            // 
            // cboWatermarkType
            // 
            this.cboWatermarkType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboWatermarkType.FormattingEnabled = true;
            this.cboWatermarkType.Location = new System.Drawing.Point(88, 20);
            this.cboWatermarkType.Name = "cboWatermarkType";
            this.cboWatermarkType.Size = new System.Drawing.Size(120, 21);
            this.cboWatermarkType.TabIndex = 33;
            this.cboWatermarkType.SelectedIndexChanged += new System.EventHandler(this.cboWatermarkType_SelectedIndexChanged);
            // 
            // cbWatermarkAutoHide
            // 
            this.cbWatermarkAutoHide.AutoSize = true;
            this.cbWatermarkAutoHide.Location = new System.Drawing.Point(16, 136);
            this.cbWatermarkAutoHide.Name = "cbWatermarkAutoHide";
            this.cbWatermarkAutoHide.Size = new System.Drawing.Size(188, 17);
            this.cbWatermarkAutoHide.TabIndex = 32;
            this.cbWatermarkAutoHide.Text = "Hide Watermark if Image is smaller";
            this.cbWatermarkAutoHide.UseVisualStyleBackColor = true;
            this.cbWatermarkAutoHide.CheckedChanged += new System.EventHandler(this.cbWatermarkAutoHide_CheckedChanged);
            // 
            // cbWatermarkAddReflection
            // 
            this.cbWatermarkAddReflection.AutoSize = true;
            this.cbWatermarkAddReflection.Location = new System.Drawing.Point(16, 112);
            this.cbWatermarkAddReflection.Name = "cbWatermarkAddReflection";
            this.cbWatermarkAddReflection.Size = new System.Drawing.Size(96, 17);
            this.cbWatermarkAddReflection.TabIndex = 24;
            this.cbWatermarkAddReflection.Text = "Add Reflection";
            this.cbWatermarkAddReflection.UseVisualStyleBackColor = true;
            this.cbWatermarkAddReflection.CheckedChanged += new System.EventHandler(this.cbWatermarkAddReflection_CheckedChanged);
            // 
            // lblWatermarkType
            // 
            this.lblWatermarkType.AutoSize = true;
            this.lblWatermarkType.Location = new System.Drawing.Point(16, 24);
            this.lblWatermarkType.Name = "lblWatermarkType";
            this.lblWatermarkType.Size = new System.Drawing.Size(34, 13);
            this.lblWatermarkType.TabIndex = 31;
            this.lblWatermarkType.Text = "Type:";
            // 
            // cbWatermarkPosition
            // 
            this.cbWatermarkPosition.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbWatermarkPosition.FormattingEnabled = true;
            this.cbWatermarkPosition.Location = new System.Drawing.Point(88, 52);
            this.cbWatermarkPosition.Name = "cbWatermarkPosition";
            this.cbWatermarkPosition.Size = new System.Drawing.Size(121, 21);
            this.cbWatermarkPosition.TabIndex = 18;
            this.cbWatermarkPosition.SelectedIndexChanged += new System.EventHandler(this.cbWatermarkPosition_SelectedIndexChanged);
            // 
            // lblWatermarkPosition
            // 
            this.lblWatermarkPosition.AutoSize = true;
            this.lblWatermarkPosition.Location = new System.Drawing.Point(16, 56);
            this.lblWatermarkPosition.Name = "lblWatermarkPosition";
            this.lblWatermarkPosition.Size = new System.Drawing.Size(60, 13);
            this.lblWatermarkPosition.TabIndex = 19;
            this.lblWatermarkPosition.Text = "Placement:";
            // 
            // nudWatermarkOffset
            // 
            this.nudWatermarkOffset.Location = new System.Drawing.Point(88, 84);
            this.nudWatermarkOffset.Maximum = new decimal(new int[] {
            2000,
            0,
            0,
            0});
            this.nudWatermarkOffset.Name = "nudWatermarkOffset";
            this.nudWatermarkOffset.Size = new System.Drawing.Size(56, 20);
            this.nudWatermarkOffset.TabIndex = 6;
            this.nudWatermarkOffset.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.nudWatermarkOffset.ValueChanged += new System.EventHandler(this.nudWatermarkOffset_ValueChanged);
            // 
            // lblWatermarkOffset
            // 
            this.lblWatermarkOffset.AutoSize = true;
            this.lblWatermarkOffset.Location = new System.Drawing.Point(16, 88);
            this.lblWatermarkOffset.Name = "lblWatermarkOffset";
            this.lblWatermarkOffset.Size = new System.Drawing.Size(38, 13);
            this.lblWatermarkOffset.TabIndex = 5;
            this.lblWatermarkOffset.Text = "Offset:";
            // 
            // tcWatermark
            // 
            this.tcWatermark.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tcWatermark.Controls.Add(this.tpWatermarkText);
            this.tcWatermark.Controls.Add(this.tpWatermarkImage);
            this.tcWatermark.ImageList = this.ilApp;
            this.tcWatermark.Location = new System.Drawing.Point(288, 4);
            this.tcWatermark.Name = "tcWatermark";
            this.tcWatermark.SelectedIndex = 0;
            this.tcWatermark.Size = new System.Drawing.Size(485, 383);
            this.tcWatermark.TabIndex = 29;
            // 
            // tpWatermarkText
            // 
            this.tpWatermarkText.Controls.Add(this.gbWatermarkBackground);
            this.tpWatermarkText.Controls.Add(this.gbWatermarkText);
            this.tpWatermarkText.ImageKey = "textfield_rename.png";
            this.tpWatermarkText.Location = new System.Drawing.Point(4, 23);
            this.tpWatermarkText.Name = "tpWatermarkText";
            this.tpWatermarkText.Padding = new System.Windows.Forms.Padding(3);
            this.tpWatermarkText.Size = new System.Drawing.Size(477, 356);
            this.tpWatermarkText.TabIndex = 0;
            this.tpWatermarkText.Text = "Text";
            this.tpWatermarkText.UseVisualStyleBackColor = true;
            // 
            // gbWatermarkBackground
            // 
            this.gbWatermarkBackground.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gbWatermarkBackground.Controls.Add(this.trackWatermarkBackgroundTrans);
            this.gbWatermarkBackground.Controls.Add(this.cbWatermarkGradientType);
            this.gbWatermarkBackground.Controls.Add(this.lblWatermarkGradientType);
            this.gbWatermarkBackground.Controls.Add(this.lblWatermarkCornerRadiusTip);
            this.gbWatermarkBackground.Controls.Add(this.nudWatermarkBackTrans);
            this.gbWatermarkBackground.Controls.Add(this.nudWatermarkCornerRadius);
            this.gbWatermarkBackground.Controls.Add(this.lblRectangleCornerRadius);
            this.gbWatermarkBackground.Controls.Add(this.lblWatermarkBackColorsTip);
            this.gbWatermarkBackground.Controls.Add(this.lblWatermarkBackTrans);
            this.gbWatermarkBackground.Controls.Add(this.pbWatermarkGradient1);
            this.gbWatermarkBackground.Controls.Add(this.pbWatermarkBorderColor);
            this.gbWatermarkBackground.Controls.Add(this.pbWatermarkGradient2);
            this.gbWatermarkBackground.Controls.Add(this.lblWatermarkBackColors);
            this.gbWatermarkBackground.Location = new System.Drawing.Point(8, 136);
            this.gbWatermarkBackground.Name = "gbWatermarkBackground";
            this.gbWatermarkBackground.Size = new System.Drawing.Size(456, 160);
            this.gbWatermarkBackground.TabIndex = 25;
            this.gbWatermarkBackground.TabStop = false;
            this.gbWatermarkBackground.Text = "Text Background Settings";
            // 
            // trackWatermarkBackgroundTrans
            // 
            this.trackWatermarkBackgroundTrans.AutoSize = false;
            this.trackWatermarkBackgroundTrans.BackColor = System.Drawing.SystemColors.Window;
            this.trackWatermarkBackgroundTrans.Location = new System.Drawing.Point(152, 85);
            this.trackWatermarkBackgroundTrans.Maximum = 255;
            this.trackWatermarkBackgroundTrans.Name = "trackWatermarkBackgroundTrans";
            this.trackWatermarkBackgroundTrans.Size = new System.Drawing.Size(200, 24);
            this.trackWatermarkBackgroundTrans.TabIndex = 31;
            this.trackWatermarkBackgroundTrans.Tag = "Adjust Background Transparency. 0 = Invisible. ";
            this.trackWatermarkBackgroundTrans.TickFrequency = 5;
            this.trackWatermarkBackgroundTrans.TickStyle = System.Windows.Forms.TickStyle.None;
            this.trackWatermarkBackgroundTrans.Scroll += new System.EventHandler(this.trackWatermarkBackgroundTrans_Scroll);
            // 
            // cbWatermarkGradientType
            // 
            this.cbWatermarkGradientType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbWatermarkGradientType.FormattingEnabled = true;
            this.cbWatermarkGradientType.Location = new System.Drawing.Point(88, 120);
            this.cbWatermarkGradientType.Name = "cbWatermarkGradientType";
            this.cbWatermarkGradientType.Size = new System.Drawing.Size(121, 21);
            this.cbWatermarkGradientType.TabIndex = 25;
            this.cbWatermarkGradientType.SelectedIndexChanged += new System.EventHandler(this.cbWatermarkGradientType_SelectedIndexChanged);
            // 
            // lblWatermarkGradientType
            // 
            this.lblWatermarkGradientType.AutoSize = true;
            this.lblWatermarkGradientType.Location = new System.Drawing.Point(8, 125);
            this.lblWatermarkGradientType.Name = "lblWatermarkGradientType";
            this.lblWatermarkGradientType.Size = new System.Drawing.Size(73, 13);
            this.lblWatermarkGradientType.TabIndex = 24;
            this.lblWatermarkGradientType.Text = "Gradient type:";
            // 
            // lblWatermarkCornerRadiusTip
            // 
            this.lblWatermarkCornerRadiusTip.AutoSize = true;
            this.lblWatermarkCornerRadiusTip.Location = new System.Drawing.Point(200, 24);
            this.lblWatermarkCornerRadiusTip.Name = "lblWatermarkCornerRadiusTip";
            this.lblWatermarkCornerRadiusTip.Size = new System.Drawing.Size(146, 13);
            this.lblWatermarkCornerRadiusTip.TabIndex = 23;
            this.lblWatermarkCornerRadiusTip.Text = "(0 - 15) 0 = Normal Rectangle";
            // 
            // nudWatermarkBackTrans
            // 
            this.nudWatermarkBackTrans.BackColor = System.Drawing.SystemColors.Window;
            this.nudWatermarkBackTrans.Location = new System.Drawing.Point(360, 85);
            this.nudWatermarkBackTrans.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.nudWatermarkBackTrans.Name = "nudWatermarkBackTrans";
            this.nudWatermarkBackTrans.ReadOnly = true;
            this.nudWatermarkBackTrans.Size = new System.Drawing.Size(48, 20);
            this.nudWatermarkBackTrans.TabIndex = 8;
            this.nudWatermarkBackTrans.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.nudWatermarkBackTrans.ValueChanged += new System.EventHandler(this.nudWatermarkBackTrans_ValueChanged);
            // 
            // nudWatermarkCornerRadius
            // 
            this.nudWatermarkCornerRadius.Location = new System.Drawing.Point(144, 19);
            this.nudWatermarkCornerRadius.Maximum = new decimal(new int[] {
            15,
            0,
            0,
            0});
            this.nudWatermarkCornerRadius.Name = "nudWatermarkCornerRadius";
            this.nudWatermarkCornerRadius.Size = new System.Drawing.Size(48, 20);
            this.nudWatermarkCornerRadius.TabIndex = 22;
            this.nudWatermarkCornerRadius.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.nudWatermarkCornerRadius.ValueChanged += new System.EventHandler(this.nudWatermarkCornerRadius_ValueChanged);
            // 
            // lblRectangleCornerRadius
            // 
            this.lblRectangleCornerRadius.AutoSize = true;
            this.lblRectangleCornerRadius.Location = new System.Drawing.Point(8, 24);
            this.lblRectangleCornerRadius.Name = "lblRectangleCornerRadius";
            this.lblRectangleCornerRadius.Size = new System.Drawing.Size(123, 13);
            this.lblRectangleCornerRadius.TabIndex = 21;
            this.lblRectangleCornerRadius.Text = "Rectangle corner radius:";
            // 
            // lblWatermarkBackColorsTip
            // 
            this.lblWatermarkBackColorsTip.AutoSize = true;
            this.lblWatermarkBackColorsTip.Location = new System.Drawing.Point(208, 56);
            this.lblWatermarkBackColorsTip.Name = "lblWatermarkBackColorsTip";
            this.lblWatermarkBackColorsTip.Size = new System.Drawing.Size(195, 13);
            this.lblWatermarkBackColorsTip.TabIndex = 20;
            this.lblWatermarkBackColorsTip.Text = "1 && 2 = Gradient colors, 3 = Border color";
            // 
            // lblWatermarkBackTrans
            // 
            this.lblWatermarkBackTrans.AutoSize = true;
            this.lblWatermarkBackTrans.Location = new System.Drawing.Point(8, 88);
            this.lblWatermarkBackTrans.Name = "lblWatermarkBackTrans";
            this.lblWatermarkBackTrans.Size = new System.Drawing.Size(136, 13);
            this.lblWatermarkBackTrans.TabIndex = 7;
            this.lblWatermarkBackTrans.Text = "Background Transparency:";
            // 
            // pbWatermarkGradient1
            // 
            this.pbWatermarkGradient1.BackColor = System.Drawing.Color.White;
            this.pbWatermarkGradient1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pbWatermarkGradient1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pbWatermarkGradient1.Location = new System.Drawing.Point(112, 51);
            this.pbWatermarkGradient1.Name = "pbWatermarkGradient1";
            this.pbWatermarkGradient1.Size = new System.Drawing.Size(24, 24);
            this.pbWatermarkGradient1.TabIndex = 10;
            this.pbWatermarkGradient1.TabStop = false;
            this.pbWatermarkGradient1.Click += new System.EventHandler(this.pbWatermarkGradient1_Click);
            // 
            // pbWatermarkBorderColor
            // 
            this.pbWatermarkBorderColor.BackColor = System.Drawing.Color.Black;
            this.pbWatermarkBorderColor.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pbWatermarkBorderColor.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pbWatermarkBorderColor.Location = new System.Drawing.Point(176, 51);
            this.pbWatermarkBorderColor.Name = "pbWatermarkBorderColor";
            this.pbWatermarkBorderColor.Size = new System.Drawing.Size(24, 24);
            this.pbWatermarkBorderColor.TabIndex = 14;
            this.pbWatermarkBorderColor.TabStop = false;
            this.pbWatermarkBorderColor.Click += new System.EventHandler(this.pbWatermarkBorderColor_Click);
            // 
            // pbWatermarkGradient2
            // 
            this.pbWatermarkGradient2.BackColor = System.Drawing.Color.Gray;
            this.pbWatermarkGradient2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pbWatermarkGradient2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pbWatermarkGradient2.Location = new System.Drawing.Point(144, 51);
            this.pbWatermarkGradient2.Name = "pbWatermarkGradient2";
            this.pbWatermarkGradient2.Size = new System.Drawing.Size(24, 24);
            this.pbWatermarkGradient2.TabIndex = 11;
            this.pbWatermarkGradient2.TabStop = false;
            this.pbWatermarkGradient2.Click += new System.EventHandler(this.pbWatermarkGradient2_Click);
            // 
            // lblWatermarkBackColors
            // 
            this.lblWatermarkBackColors.AutoSize = true;
            this.lblWatermarkBackColors.Location = new System.Drawing.Point(8, 56);
            this.lblWatermarkBackColors.Name = "lblWatermarkBackColors";
            this.lblWatermarkBackColors.Size = new System.Drawing.Size(99, 13);
            this.lblWatermarkBackColors.TabIndex = 12;
            this.lblWatermarkBackColors.Text = "Background colors:";
            // 
            // gbWatermarkText
            // 
            this.gbWatermarkText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gbWatermarkText.Controls.Add(this.trackWatermarkFontTrans);
            this.gbWatermarkText.Controls.Add(this.lblWatermarkText);
            this.gbWatermarkText.Controls.Add(this.nudWatermarkFontTrans);
            this.gbWatermarkText.Controls.Add(this.lblWatermarkFont);
            this.gbWatermarkText.Controls.Add(this.btnWatermarkFont);
            this.gbWatermarkText.Controls.Add(this.lblWatermarkFontTrans);
            this.gbWatermarkText.Controls.Add(this.txtWatermarkText);
            this.gbWatermarkText.Controls.Add(this.pbWatermarkFontColor);
            this.gbWatermarkText.Location = new System.Drawing.Point(8, 8);
            this.gbWatermarkText.Name = "gbWatermarkText";
            this.gbWatermarkText.Size = new System.Drawing.Size(456, 120);
            this.gbWatermarkText.TabIndex = 24;
            this.gbWatermarkText.TabStop = false;
            this.gbWatermarkText.Text = "Text Settings";
            // 
            // trackWatermarkFontTrans
            // 
            this.trackWatermarkFontTrans.AutoSize = false;
            this.trackWatermarkFontTrans.BackColor = System.Drawing.SystemColors.Window;
            this.trackWatermarkFontTrans.Location = new System.Drawing.Point(152, 85);
            this.trackWatermarkFontTrans.Maximum = 255;
            this.trackWatermarkFontTrans.Name = "trackWatermarkFontTrans";
            this.trackWatermarkFontTrans.Size = new System.Drawing.Size(200, 24);
            this.trackWatermarkFontTrans.TabIndex = 30;
            this.trackWatermarkFontTrans.Tag = "Adjust Font Transparency. 0 = Invisible. ";
            this.trackWatermarkFontTrans.TickFrequency = 5;
            this.trackWatermarkFontTrans.TickStyle = System.Windows.Forms.TickStyle.None;
            this.trackWatermarkFontTrans.Scroll += new System.EventHandler(this.trackWatermarkFontTrans_Scroll);
            // 
            // lblWatermarkText
            // 
            this.lblWatermarkText.AutoSize = true;
            this.lblWatermarkText.Location = new System.Drawing.Point(8, 24);
            this.lblWatermarkText.Name = "lblWatermarkText";
            this.lblWatermarkText.Size = new System.Drawing.Size(86, 13);
            this.lblWatermarkText.TabIndex = 16;
            this.lblWatermarkText.Text = "Watermark Text:";
            // 
            // nudWatermarkFontTrans
            // 
            this.nudWatermarkFontTrans.BackColor = System.Drawing.SystemColors.Window;
            this.nudWatermarkFontTrans.Location = new System.Drawing.Point(360, 85);
            this.nudWatermarkFontTrans.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.nudWatermarkFontTrans.Name = "nudWatermarkFontTrans";
            this.nudWatermarkFontTrans.ReadOnly = true;
            this.nudWatermarkFontTrans.Size = new System.Drawing.Size(48, 20);
            this.nudWatermarkFontTrans.TabIndex = 22;
            this.nudWatermarkFontTrans.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.nudWatermarkFontTrans.ValueChanged += new System.EventHandler(this.nudWatermarkFontTrans_ValueChanged);
            // 
            // lblWatermarkFont
            // 
            this.lblWatermarkFont.AutoSize = true;
            this.lblWatermarkFont.Location = new System.Drawing.Point(136, 56);
            this.lblWatermarkFont.Name = "lblWatermarkFont";
            this.lblWatermarkFont.Size = new System.Drawing.Size(83, 13);
            this.lblWatermarkFont.TabIndex = 4;
            this.lblWatermarkFont.Text = "Font Information";
            // 
            // btnWatermarkFont
            // 
            this.btnWatermarkFont.Location = new System.Drawing.Point(8, 48);
            this.btnWatermarkFont.Name = "btnWatermarkFont";
            this.btnWatermarkFont.Size = new System.Drawing.Size(88, 24);
            this.btnWatermarkFont.TabIndex = 3;
            this.btnWatermarkFont.Text = "Change Font...";
            this.btnWatermarkFont.UseVisualStyleBackColor = true;
            this.btnWatermarkFont.Click += new System.EventHandler(this.btnWatermarkFont_Click);
            // 
            // lblWatermarkFontTrans
            // 
            this.lblWatermarkFontTrans.AutoSize = true;
            this.lblWatermarkFontTrans.Location = new System.Drawing.Point(8, 88);
            this.lblWatermarkFontTrans.Name = "lblWatermarkFontTrans";
            this.lblWatermarkFontTrans.Size = new System.Drawing.Size(99, 13);
            this.lblWatermarkFontTrans.TabIndex = 21;
            this.lblWatermarkFontTrans.Text = "Font Transparency:";
            // 
            // txtWatermarkText
            // 
            this.txtWatermarkText.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtWatermarkText.Location = new System.Drawing.Point(104, 19);
            this.txtWatermarkText.Name = "txtWatermarkText";
            this.txtWatermarkText.Size = new System.Drawing.Size(336, 20);
            this.txtWatermarkText.TabIndex = 15;
            this.txtWatermarkText.TextChanged += new System.EventHandler(this.txtWatermarkText_TextChanged);
            this.txtWatermarkText.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtWatermarkText_KeyDown);
            this.txtWatermarkText.Leave += new System.EventHandler(this.txtWatermarkText_Leave);
            this.txtWatermarkText.MouseDown += new System.Windows.Forms.MouseEventHandler(this.txtWatermarkText_MouseDown);
            // 
            // pbWatermarkFontColor
            // 
            this.pbWatermarkFontColor.BackColor = System.Drawing.Color.Black;
            this.pbWatermarkFontColor.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pbWatermarkFontColor.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pbWatermarkFontColor.Location = new System.Drawing.Point(104, 48);
            this.pbWatermarkFontColor.Name = "pbWatermarkFontColor";
            this.pbWatermarkFontColor.Size = new System.Drawing.Size(24, 24);
            this.pbWatermarkFontColor.TabIndex = 17;
            this.pbWatermarkFontColor.TabStop = false;
            this.pbWatermarkFontColor.Click += new System.EventHandler(this.pbWatermarkFontColor_Click);
            // 
            // tpWatermarkImage
            // 
            this.tpWatermarkImage.Controls.Add(this.lblWatermarkImageScale);
            this.tpWatermarkImage.Controls.Add(this.nudWatermarkImageScale);
            this.tpWatermarkImage.Controls.Add(this.cbWatermarkUseBorder);
            this.tpWatermarkImage.Controls.Add(this.btwWatermarkBrowseImage);
            this.tpWatermarkImage.Controls.Add(this.txtWatermarkImageLocation);
            this.tpWatermarkImage.ImageKey = "image_edit.png";
            this.tpWatermarkImage.Location = new System.Drawing.Point(4, 23);
            this.tpWatermarkImage.Name = "tpWatermarkImage";
            this.tpWatermarkImage.Padding = new System.Windows.Forms.Padding(3);
            this.tpWatermarkImage.Size = new System.Drawing.Size(477, 356);
            this.tpWatermarkImage.TabIndex = 1;
            this.tpWatermarkImage.Text = "Image";
            this.tpWatermarkImage.UseVisualStyleBackColor = true;
            // 
            // lblWatermarkImageScale
            // 
            this.lblWatermarkImageScale.AutoSize = true;
            this.lblWatermarkImageScale.Location = new System.Drawing.Point(16, 76);
            this.lblWatermarkImageScale.Name = "lblWatermarkImageScale";
            this.lblWatermarkImageScale.Size = new System.Drawing.Size(117, 13);
            this.lblWatermarkImageScale.TabIndex = 25;
            this.lblWatermarkImageScale.Text = "Image size percentage:";
            // 
            // nudWatermarkImageScale
            // 
            this.nudWatermarkImageScale.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.nudWatermarkImageScale.Location = new System.Drawing.Point(136, 72);
            this.nudWatermarkImageScale.Maximum = new decimal(new int[] {
            2000,
            0,
            0,
            0});
            this.nudWatermarkImageScale.Name = "nudWatermarkImageScale";
            this.nudWatermarkImageScale.Size = new System.Drawing.Size(56, 20);
            this.nudWatermarkImageScale.TabIndex = 24;
            this.nudWatermarkImageScale.ValueChanged += new System.EventHandler(this.nudWatermarkImageScale_ValueChanged);
            // 
            // cbWatermarkUseBorder
            // 
            this.cbWatermarkUseBorder.AutoSize = true;
            this.cbWatermarkUseBorder.Location = new System.Drawing.Point(16, 48);
            this.cbWatermarkUseBorder.Name = "cbWatermarkUseBorder";
            this.cbWatermarkUseBorder.Size = new System.Drawing.Size(79, 17);
            this.cbWatermarkUseBorder.TabIndex = 23;
            this.cbWatermarkUseBorder.Text = "Add Border";
            this.cbWatermarkUseBorder.UseVisualStyleBackColor = true;
            this.cbWatermarkUseBorder.CheckedChanged += new System.EventHandler(this.cbWatermarkUseBorder_CheckedChanged);
            // 
            // btwWatermarkBrowseImage
            // 
            this.btwWatermarkBrowseImage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btwWatermarkBrowseImage.Location = new System.Drawing.Point(400, 13);
            this.btwWatermarkBrowseImage.Name = "btwWatermarkBrowseImage";
            this.btwWatermarkBrowseImage.Size = new System.Drawing.Size(64, 24);
            this.btwWatermarkBrowseImage.TabIndex = 22;
            this.btwWatermarkBrowseImage.Tag = "Browse for a Watermark Image";
            this.btwWatermarkBrowseImage.Text = "Browse...";
            this.btwWatermarkBrowseImage.UseVisualStyleBackColor = true;
            this.btwWatermarkBrowseImage.Click += new System.EventHandler(this.btwWatermarkBrowseImage_Click);
            // 
            // txtWatermarkImageLocation
            // 
            this.txtWatermarkImageLocation.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtWatermarkImageLocation.Location = new System.Drawing.Point(16, 16);
            this.txtWatermarkImageLocation.Name = "txtWatermarkImageLocation";
            this.txtWatermarkImageLocation.Size = new System.Drawing.Size(376, 20);
            this.txtWatermarkImageLocation.TabIndex = 21;
            this.txtWatermarkImageLocation.TextChanged += new System.EventHandler(this.txtWatermarkImageLocation_TextChanged);
            // 
            // tpFileNaming
            // 
            this.tpFileNaming.Controls.Add(this.btnResetIncrement);
            this.tpFileNaming.Controls.Add(this.gbOthersNaming);
            this.tpFileNaming.Controls.Add(this.gbCodeTitle);
            this.tpFileNaming.Controls.Add(this.gbActiveWindowNaming);
            this.tpFileNaming.Location = new System.Drawing.Point(4, 23);
            this.tpFileNaming.Name = "tpFileNaming";
            this.tpFileNaming.Size = new System.Drawing.Size(791, 409);
            this.tpFileNaming.TabIndex = 3;
            this.tpFileNaming.Text = "Naming Conventions";
            this.tpFileNaming.UseVisualStyleBackColor = true;
            // 
            // btnResetIncrement
            // 
            this.btnResetIncrement.Location = new System.Drawing.Point(240, 184);
            this.btnResetIncrement.Name = "btnResetIncrement";
            this.btnResetIncrement.Size = new System.Drawing.Size(184, 23);
            this.btnResetIncrement.TabIndex = 116;
            this.btnResetIncrement.Text = "Reset Auto-Increment Number";
            this.btnResetIncrement.UseVisualStyleBackColor = true;
            this.btnResetIncrement.Click += new System.EventHandler(this.btnResetIncrement_Click);
            // 
            // gbOthersNaming
            // 
            this.gbOthersNaming.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gbOthersNaming.Controls.Add(this.lblEntireScreenPreview);
            this.gbOthersNaming.Controls.Add(this.txtEntireScreen);
            this.gbOthersNaming.Location = new System.Drawing.Point(240, 96);
            this.gbOthersNaming.Name = "gbOthersNaming";
            this.gbOthersNaming.Size = new System.Drawing.Size(384, 80);
            this.gbOthersNaming.TabIndex = 115;
            this.gbOthersNaming.TabStop = false;
            this.gbOthersNaming.Text = "Other Capture Types";
            // 
            // lblEntireScreenPreview
            // 
            this.lblEntireScreenPreview.AutoSize = true;
            this.lblEntireScreenPreview.Location = new System.Drawing.Point(16, 56);
            this.lblEntireScreenPreview.Name = "lblEntireScreenPreview";
            this.lblEntireScreenPreview.Size = new System.Drawing.Size(112, 13);
            this.lblEntireScreenPreview.TabIndex = 6;
            this.lblEntireScreenPreview.Text = "Entire Screen Preview";
            // 
            // txtEntireScreen
            // 
            this.txtEntireScreen.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtEntireScreen.Location = new System.Drawing.Point(16, 24);
            this.txtEntireScreen.Name = "txtEntireScreen";
            this.txtEntireScreen.Size = new System.Drawing.Size(352, 20);
            this.txtEntireScreen.TabIndex = 4;
            this.txtEntireScreen.TextChanged += new System.EventHandler(this.txtEntireScreen_TextChanged);
            this.txtEntireScreen.Leave += new System.EventHandler(this.txtEntireScreen_Leave);
            // 
            // gbCodeTitle
            // 
            this.gbCodeTitle.BackColor = System.Drawing.Color.Transparent;
            this.gbCodeTitle.Controls.Add(this.btnCodesI);
            this.gbCodeTitle.Controls.Add(this.btnCodesPm);
            this.gbCodeTitle.Controls.Add(this.btnCodesS);
            this.gbCodeTitle.Controls.Add(this.btnCodesMi);
            this.gbCodeTitle.Controls.Add(this.btnCodesH);
            this.gbCodeTitle.Controls.Add(this.btnCodesY);
            this.gbCodeTitle.Controls.Add(this.btnCodesD);
            this.gbCodeTitle.Controls.Add(this.btnCodesMo);
            this.gbCodeTitle.Controls.Add(this.btnCodesT);
            this.gbCodeTitle.Controls.Add(this.lblCodeI);
            this.gbCodeTitle.Controls.Add(this.lblCodeT);
            this.gbCodeTitle.Controls.Add(this.lblCodeMo);
            this.gbCodeTitle.Controls.Add(this.lblCodePm);
            this.gbCodeTitle.Controls.Add(this.lblCodeD);
            this.gbCodeTitle.Controls.Add(this.lblCodeS);
            this.gbCodeTitle.Controls.Add(this.lblCodeMi);
            this.gbCodeTitle.Controls.Add(this.lblCodeY);
            this.gbCodeTitle.Controls.Add(this.lblCodeH);
            this.gbCodeTitle.Location = new System.Drawing.Point(8, 8);
            this.gbCodeTitle.Name = "gbCodeTitle";
            this.gbCodeTitle.Size = new System.Drawing.Size(224, 264);
            this.gbCodeTitle.TabIndex = 111;
            this.gbCodeTitle.TabStop = false;
            this.gbCodeTitle.Text = "Codes";
            // 
            // btnCodesI
            // 
            this.btnCodesI.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCodesI.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnCodesI.Location = new System.Drawing.Point(59, 201);
            this.btnCodesI.Margin = new System.Windows.Forms.Padding(2);
            this.btnCodesI.Name = "btnCodesI";
            this.btnCodesI.Size = new System.Drawing.Size(152, 22);
            this.btnCodesI.TabIndex = 107;
            this.btnCodesI.TabStop = false;
            this.btnCodesI.Text = "Auto-Increment";
            this.btnCodesI.UseVisualStyleBackColor = true;
            this.btnCodesI.Click += new System.EventHandler(this.btnCodes_Click);
            // 
            // btnCodesPm
            // 
            this.btnCodesPm.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCodesPm.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnCodesPm.Location = new System.Drawing.Point(59, 228);
            this.btnCodesPm.Margin = new System.Windows.Forms.Padding(2);
            this.btnCodesPm.Name = "btnCodesPm";
            this.btnCodesPm.Size = new System.Drawing.Size(152, 22);
            this.btnCodesPm.TabIndex = 106;
            this.btnCodesPm.TabStop = false;
            this.btnCodesPm.Text = "Gets AM/PM";
            this.btnCodesPm.UseVisualStyleBackColor = true;
            this.btnCodesPm.Click += new System.EventHandler(this.btnCodes_Click);
            // 
            // btnCodesS
            // 
            this.btnCodesS.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCodesS.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnCodesS.Location = new System.Drawing.Point(59, 174);
            this.btnCodesS.Margin = new System.Windows.Forms.Padding(2);
            this.btnCodesS.Name = "btnCodesS";
            this.btnCodesS.Size = new System.Drawing.Size(152, 22);
            this.btnCodesS.TabIndex = 105;
            this.btnCodesS.TabStop = false;
            this.btnCodesS.Text = "Gets the current second";
            this.btnCodesS.UseVisualStyleBackColor = true;
            this.btnCodesS.Click += new System.EventHandler(this.btnCodes_Click);
            // 
            // btnCodesMi
            // 
            this.btnCodesMi.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCodesMi.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnCodesMi.Location = new System.Drawing.Point(59, 148);
            this.btnCodesMi.Margin = new System.Windows.Forms.Padding(2);
            this.btnCodesMi.Name = "btnCodesMi";
            this.btnCodesMi.Size = new System.Drawing.Size(152, 22);
            this.btnCodesMi.TabIndex = 104;
            this.btnCodesMi.TabStop = false;
            this.btnCodesMi.Text = "Gets the current minute";
            this.btnCodesMi.UseVisualStyleBackColor = true;
            this.btnCodesMi.Click += new System.EventHandler(this.btnCodes_Click);
            // 
            // btnCodesH
            // 
            this.btnCodesH.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCodesH.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnCodesH.Location = new System.Drawing.Point(59, 122);
            this.btnCodesH.Margin = new System.Windows.Forms.Padding(2);
            this.btnCodesH.Name = "btnCodesH";
            this.btnCodesH.Size = new System.Drawing.Size(152, 22);
            this.btnCodesH.TabIndex = 103;
            this.btnCodesH.TabStop = false;
            this.btnCodesH.Text = "Gets the current hour";
            this.btnCodesH.UseVisualStyleBackColor = true;
            this.btnCodesH.Click += new System.EventHandler(this.btnCodes_Click);
            // 
            // btnCodesY
            // 
            this.btnCodesY.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCodesY.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnCodesY.Location = new System.Drawing.Point(59, 96);
            this.btnCodesY.Margin = new System.Windows.Forms.Padding(2);
            this.btnCodesY.Name = "btnCodesY";
            this.btnCodesY.Size = new System.Drawing.Size(152, 22);
            this.btnCodesY.TabIndex = 102;
            this.btnCodesY.TabStop = false;
            this.btnCodesY.Text = "Gets the current year";
            this.btnCodesY.UseVisualStyleBackColor = true;
            this.btnCodesY.Click += new System.EventHandler(this.btnCodes_Click);
            // 
            // btnCodesD
            // 
            this.btnCodesD.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCodesD.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnCodesD.Location = new System.Drawing.Point(59, 70);
            this.btnCodesD.Margin = new System.Windows.Forms.Padding(2);
            this.btnCodesD.Name = "btnCodesD";
            this.btnCodesD.Size = new System.Drawing.Size(152, 22);
            this.btnCodesD.TabIndex = 101;
            this.btnCodesD.TabStop = false;
            this.btnCodesD.Text = "Gets the current day";
            this.btnCodesD.UseVisualStyleBackColor = true;
            this.btnCodesD.Click += new System.EventHandler(this.btnCodes_Click);
            // 
            // btnCodesMo
            // 
            this.btnCodesMo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCodesMo.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnCodesMo.Location = new System.Drawing.Point(59, 44);
            this.btnCodesMo.Margin = new System.Windows.Forms.Padding(2);
            this.btnCodesMo.Name = "btnCodesMo";
            this.btnCodesMo.Size = new System.Drawing.Size(152, 22);
            this.btnCodesMo.TabIndex = 100;
            this.btnCodesMo.TabStop = false;
            this.btnCodesMo.Text = "Gets the current month";
            this.btnCodesMo.UseVisualStyleBackColor = true;
            this.btnCodesMo.Click += new System.EventHandler(this.btnCodes_Click);
            // 
            // btnCodesT
            // 
            this.btnCodesT.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCodesT.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnCodesT.Location = new System.Drawing.Point(59, 19);
            this.btnCodesT.Margin = new System.Windows.Forms.Padding(2);
            this.btnCodesT.Name = "btnCodesT";
            this.btnCodesT.Size = new System.Drawing.Size(152, 22);
            this.btnCodesT.TabIndex = 99;
            this.btnCodesT.TabStop = false;
            this.btnCodesT.Text = "Title of Active Window";
            this.btnCodesT.UseVisualStyleBackColor = true;
            this.btnCodesT.Click += new System.EventHandler(this.btnCodes_Click);
            // 
            // lblCodeI
            // 
            this.lblCodeI.AutoSize = true;
            this.lblCodeI.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblCodeI.Location = new System.Drawing.Point(20, 205);
            this.lblCodeI.Name = "lblCodeI";
            this.lblCodeI.Size = new System.Drawing.Size(17, 13);
            this.lblCodeI.TabIndex = 90;
            this.lblCodeI.Text = "%i";
            // 
            // lblCodeT
            // 
            this.lblCodeT.AutoSize = true;
            this.lblCodeT.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblCodeT.Location = new System.Drawing.Point(20, 22);
            this.lblCodeT.Name = "lblCodeT";
            this.lblCodeT.Size = new System.Drawing.Size(18, 13);
            this.lblCodeT.TabIndex = 74;
            this.lblCodeT.Text = "%t";
            // 
            // lblCodeMo
            // 
            this.lblCodeMo.AutoSize = true;
            this.lblCodeMo.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblCodeMo.Location = new System.Drawing.Point(20, 48);
            this.lblCodeMo.Name = "lblCodeMo";
            this.lblCodeMo.Size = new System.Drawing.Size(29, 13);
            this.lblCodeMo.TabIndex = 75;
            this.lblCodeMo.Text = "%mo";
            // 
            // lblCodePm
            // 
            this.lblCodePm.AutoSize = true;
            this.lblCodePm.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblCodePm.Location = new System.Drawing.Point(20, 231);
            this.lblCodePm.Name = "lblCodePm";
            this.lblCodePm.Size = new System.Drawing.Size(29, 13);
            this.lblCodePm.TabIndex = 88;
            this.lblCodePm.Text = "%pm";
            // 
            // lblCodeD
            // 
            this.lblCodeD.AutoSize = true;
            this.lblCodeD.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblCodeD.Location = new System.Drawing.Point(20, 73);
            this.lblCodeD.Name = "lblCodeD";
            this.lblCodeD.Size = new System.Drawing.Size(21, 13);
            this.lblCodeD.TabIndex = 76;
            this.lblCodeD.Text = "%d";
            // 
            // lblCodeS
            // 
            this.lblCodeS.AutoSize = true;
            this.lblCodeS.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblCodeS.Location = new System.Drawing.Point(20, 177);
            this.lblCodeS.Name = "lblCodeS";
            this.lblCodeS.Size = new System.Drawing.Size(20, 13);
            this.lblCodeS.TabIndex = 86;
            this.lblCodeS.Text = "%s";
            // 
            // lblCodeMi
            // 
            this.lblCodeMi.AutoSize = true;
            this.lblCodeMi.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblCodeMi.Location = new System.Drawing.Point(20, 152);
            this.lblCodeMi.Name = "lblCodeMi";
            this.lblCodeMi.Size = new System.Drawing.Size(25, 13);
            this.lblCodeMi.TabIndex = 84;
            this.lblCodeMi.Text = "%mi";
            // 
            // lblCodeY
            // 
            this.lblCodeY.AutoSize = true;
            this.lblCodeY.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblCodeY.Location = new System.Drawing.Point(20, 99);
            this.lblCodeY.Name = "lblCodeY";
            this.lblCodeY.Size = new System.Drawing.Size(20, 13);
            this.lblCodeY.TabIndex = 80;
            this.lblCodeY.Text = "%y";
            // 
            // lblCodeH
            // 
            this.lblCodeH.AutoSize = true;
            this.lblCodeH.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblCodeH.Location = new System.Drawing.Point(20, 126);
            this.lblCodeH.Name = "lblCodeH";
            this.lblCodeH.Size = new System.Drawing.Size(21, 13);
            this.lblCodeH.TabIndex = 82;
            this.lblCodeH.Text = "%h";
            // 
            // gbActiveWindowNaming
            // 
            this.gbActiveWindowNaming.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gbActiveWindowNaming.BackColor = System.Drawing.Color.Transparent;
            this.gbActiveWindowNaming.Controls.Add(this.lblActiveWindowPreview);
            this.gbActiveWindowNaming.Controls.Add(this.txtActiveWindow);
            this.gbActiveWindowNaming.Location = new System.Drawing.Point(240, 8);
            this.gbActiveWindowNaming.Name = "gbActiveWindowNaming";
            this.gbActiveWindowNaming.Size = new System.Drawing.Size(384, 80);
            this.gbActiveWindowNaming.TabIndex = 113;
            this.gbActiveWindowNaming.TabStop = false;
            this.gbActiveWindowNaming.Text = "Active Window";
            // 
            // lblActiveWindowPreview
            // 
            this.lblActiveWindowPreview.AutoSize = true;
            this.lblActiveWindowPreview.Location = new System.Drawing.Point(16, 56);
            this.lblActiveWindowPreview.Name = "lblActiveWindowPreview";
            this.lblActiveWindowPreview.Size = new System.Drawing.Size(120, 13);
            this.lblActiveWindowPreview.TabIndex = 4;
            this.lblActiveWindowPreview.Text = "Active Window Preview";
            // 
            // txtActiveWindow
            // 
            this.txtActiveWindow.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtActiveWindow.Location = new System.Drawing.Point(16, 24);
            this.txtActiveWindow.Name = "txtActiveWindow";
            this.txtActiveWindow.Size = new System.Drawing.Size(352, 20);
            this.txtActiveWindow.TabIndex = 2;
            this.txtActiveWindow.TextChanged += new System.EventHandler(this.txtActiveWindow_TextChanged);
            this.txtActiveWindow.Leave += new System.EventHandler(this.txtActiveWindow_Leave);
            // 
            // tpCaptureQuality
            // 
            this.tpCaptureQuality.Controls.Add(this.gbImageSize);
            this.tpCaptureQuality.Controls.Add(this.gbPictureQuality);
            this.tpCaptureQuality.Location = new System.Drawing.Point(4, 23);
            this.tpCaptureQuality.Name = "tpCaptureQuality";
            this.tpCaptureQuality.Padding = new System.Windows.Forms.Padding(3);
            this.tpCaptureQuality.Size = new System.Drawing.Size(791, 409);
            this.tpCaptureQuality.TabIndex = 0;
            this.tpCaptureQuality.Text = "Image Settings";
            this.tpCaptureQuality.UseVisualStyleBackColor = true;
            // 
            // gbImageSize
            // 
            this.gbImageSize.Controls.Add(this.rbImageSizeDefault);
            this.gbImageSize.Controls.Add(this.lblImageSizeFixedHeight);
            this.gbImageSize.Controls.Add(this.rbImageSizeFixed);
            this.gbImageSize.Controls.Add(this.lblImageSizeFixedWidth);
            this.gbImageSize.Controls.Add(this.txtImageSizeRatio);
            this.gbImageSize.Controls.Add(this.lblImageSizeRatioPercentage);
            this.gbImageSize.Controls.Add(this.txtImageSizeFixedWidth);
            this.gbImageSize.Controls.Add(this.rbImageSizeRatio);
            this.gbImageSize.Controls.Add(this.txtImageSizeFixedHeight);
            this.gbImageSize.Location = new System.Drawing.Point(8, 152);
            this.gbImageSize.Name = "gbImageSize";
            this.gbImageSize.Size = new System.Drawing.Size(768, 128);
            this.gbImageSize.TabIndex = 124;
            this.gbImageSize.TabStop = false;
            this.gbImageSize.Text = "Image Size";
            // 
            // rbImageSizeDefault
            // 
            this.rbImageSizeDefault.AutoSize = true;
            this.rbImageSizeDefault.Location = new System.Drawing.Point(16, 24);
            this.rbImageSizeDefault.Name = "rbImageSizeDefault";
            this.rbImageSizeDefault.Size = new System.Drawing.Size(110, 17);
            this.rbImageSizeDefault.TabIndex = 127;
            this.rbImageSizeDefault.TabStop = true;
            this.rbImageSizeDefault.Text = "Image size default";
            this.rbImageSizeDefault.UseVisualStyleBackColor = true;
            this.rbImageSizeDefault.CheckedChanged += new System.EventHandler(this.rbImageSize_CheckedChanged);
            // 
            // lblImageSizeFixedHeight
            // 
            this.lblImageSizeFixedHeight.AutoSize = true;
            this.lblImageSizeFixedHeight.Location = new System.Drawing.Point(232, 59);
            this.lblImageSizeFixedHeight.Name = "lblImageSizeFixedHeight";
            this.lblImageSizeFixedHeight.Size = new System.Drawing.Size(61, 13);
            this.lblImageSizeFixedHeight.TabIndex = 126;
            this.lblImageSizeFixedHeight.Text = "Height (px):";
            // 
            // rbImageSizeFixed
            // 
            this.rbImageSizeFixed.AutoSize = true;
            this.rbImageSizeFixed.Location = new System.Drawing.Point(16, 56);
            this.rbImageSizeFixed.Name = "rbImageSizeFixed";
            this.rbImageSizeFixed.Size = new System.Drawing.Size(103, 17);
            this.rbImageSizeFixed.TabIndex = 123;
            this.rbImageSizeFixed.TabStop = true;
            this.rbImageSizeFixed.Text = "Image size fixed:";
            this.rbImageSizeFixed.UseVisualStyleBackColor = true;
            this.rbImageSizeFixed.CheckedChanged += new System.EventHandler(this.rbImageSize_CheckedChanged);
            // 
            // lblImageSizeFixedWidth
            // 
            this.lblImageSizeFixedWidth.AutoSize = true;
            this.lblImageSizeFixedWidth.Location = new System.Drawing.Point(120, 59);
            this.lblImageSizeFixedWidth.Name = "lblImageSizeFixedWidth";
            this.lblImageSizeFixedWidth.Size = new System.Drawing.Size(58, 13);
            this.lblImageSizeFixedWidth.TabIndex = 125;
            this.lblImageSizeFixedWidth.Text = "Width (px):";
            // 
            // txtImageSizeRatio
            // 
            this.txtImageSizeRatio.Location = new System.Drawing.Point(120, 87);
            this.txtImageSizeRatio.Name = "txtImageSizeRatio";
            this.txtImageSizeRatio.Size = new System.Drawing.Size(32, 20);
            this.txtImageSizeRatio.TabIndex = 116;
            this.txtImageSizeRatio.Text = "100";
            this.txtImageSizeRatio.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtImageSizeRatio.TextChanged += new System.EventHandler(this.txtImageSizeRatio_TextChanged);
            // 
            // lblImageSizeRatioPercentage
            // 
            this.lblImageSizeRatioPercentage.AutoSize = true;
            this.lblImageSizeRatioPercentage.Location = new System.Drawing.Point(159, 91);
            this.lblImageSizeRatioPercentage.Name = "lblImageSizeRatioPercentage";
            this.lblImageSizeRatioPercentage.Size = new System.Drawing.Size(15, 13);
            this.lblImageSizeRatioPercentage.TabIndex = 118;
            this.lblImageSizeRatioPercentage.Text = "%";
            // 
            // txtImageSizeFixedWidth
            // 
            this.txtImageSizeFixedWidth.Location = new System.Drawing.Point(184, 56);
            this.txtImageSizeFixedWidth.Name = "txtImageSizeFixedWidth";
            this.txtImageSizeFixedWidth.Size = new System.Drawing.Size(40, 20);
            this.txtImageSizeFixedWidth.TabIndex = 119;
            this.txtImageSizeFixedWidth.Text = "2500";
            this.txtImageSizeFixedWidth.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtImageSizeFixedWidth.TextChanged += new System.EventHandler(this.txtImageSizeFixedWidth_TextChanged);
            // 
            // rbImageSizeRatio
            // 
            this.rbImageSizeRatio.AutoSize = true;
            this.rbImageSizeRatio.Location = new System.Drawing.Point(16, 88);
            this.rbImageSizeRatio.Name = "rbImageSizeRatio";
            this.rbImageSizeRatio.Size = new System.Drawing.Size(101, 17);
            this.rbImageSizeRatio.TabIndex = 122;
            this.rbImageSizeRatio.TabStop = true;
            this.rbImageSizeRatio.Text = "Image size ratio:";
            this.rbImageSizeRatio.UseVisualStyleBackColor = true;
            this.rbImageSizeRatio.CheckedChanged += new System.EventHandler(this.rbImageSize_CheckedChanged);
            // 
            // txtImageSizeFixedHeight
            // 
            this.txtImageSizeFixedHeight.Location = new System.Drawing.Point(296, 56);
            this.txtImageSizeFixedHeight.Name = "txtImageSizeFixedHeight";
            this.txtImageSizeFixedHeight.Size = new System.Drawing.Size(40, 20);
            this.txtImageSizeFixedHeight.TabIndex = 120;
            this.txtImageSizeFixedHeight.Text = "2500";
            this.txtImageSizeFixedHeight.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtImageSizeFixedHeight.TextChanged += new System.EventHandler(this.txtImageSizeFixedHeight_TextChanged);
            // 
            // gbPictureQuality
            // 
            this.gbPictureQuality.BackColor = System.Drawing.Color.Transparent;
            this.gbPictureQuality.Controls.Add(this.nudSwitchAfter);
            this.gbPictureQuality.Controls.Add(this.nudImageQuality);
            this.gbPictureQuality.Controls.Add(this.lblJPEGQualityPercentage);
            this.gbPictureQuality.Controls.Add(this.lblQuality);
            this.gbPictureQuality.Controls.Add(this.cbSwitchFormat);
            this.gbPictureQuality.Controls.Add(this.lblFileFormat);
            this.gbPictureQuality.Controls.Add(this.cbFileFormat);
            this.gbPictureQuality.Controls.Add(this.lblKB);
            this.gbPictureQuality.Controls.Add(this.lblAfter);
            this.gbPictureQuality.Controls.Add(this.lblSwitchTo);
            this.gbPictureQuality.Location = new System.Drawing.Point(8, 8);
            this.gbPictureQuality.Name = "gbPictureQuality";
            this.gbPictureQuality.Size = new System.Drawing.Size(768, 136);
            this.gbPictureQuality.TabIndex = 115;
            this.gbPictureQuality.TabStop = false;
            this.gbPictureQuality.Text = "Picture Quality";
            // 
            // nudSwitchAfter
            // 
            this.nudSwitchAfter.Location = new System.Drawing.Point(16, 96);
            this.nudSwitchAfter.Maximum = new decimal(new int[] {
            1024,
            0,
            0,
            0});
            this.nudSwitchAfter.Name = "nudSwitchAfter";
            this.nudSwitchAfter.Size = new System.Drawing.Size(72, 20);
            this.nudSwitchAfter.TabIndex = 116;
            this.nudSwitchAfter.Value = new decimal(new int[] {
            350,
            0,
            0,
            0});
            this.nudSwitchAfter.ValueChanged += new System.EventHandler(this.nudSwitchAfter_ValueChanged);
            // 
            // nudImageQuality
            // 
            this.nudImageQuality.Location = new System.Drawing.Point(128, 40);
            this.nudImageQuality.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudImageQuality.Name = "nudImageQuality";
            this.nudImageQuality.Size = new System.Drawing.Size(72, 20);
            this.nudImageQuality.TabIndex = 111;
            this.nudImageQuality.Value = new decimal(new int[] {
            90,
            0,
            0,
            0});
            this.nudImageQuality.ValueChanged += new System.EventHandler(this.txtImageQuality_ValueChanged);
            // 
            // lblJPEGQualityPercentage
            // 
            this.lblJPEGQualityPercentage.AutoSize = true;
            this.lblJPEGQualityPercentage.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblJPEGQualityPercentage.Location = new System.Drawing.Point(208, 44);
            this.lblJPEGQualityPercentage.Name = "lblJPEGQualityPercentage";
            this.lblJPEGQualityPercentage.Size = new System.Drawing.Size(15, 13);
            this.lblJPEGQualityPercentage.TabIndex = 110;
            this.lblJPEGQualityPercentage.Text = "%";
            // 
            // lblQuality
            // 
            this.lblQuality.AutoSize = true;
            this.lblQuality.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblQuality.Location = new System.Drawing.Point(128, 24);
            this.lblQuality.Name = "lblQuality";
            this.lblQuality.Size = new System.Drawing.Size(69, 13);
            this.lblQuality.TabIndex = 108;
            this.lblQuality.Text = "JPEG Quality";
            // 
            // cbSwitchFormat
            // 
            this.cbSwitchFormat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbSwitchFormat.FormattingEnabled = true;
            this.cbSwitchFormat.Location = new System.Drawing.Point(128, 96);
            this.cbSwitchFormat.Name = "cbSwitchFormat";
            this.cbSwitchFormat.Size = new System.Drawing.Size(98, 21);
            this.cbSwitchFormat.TabIndex = 9;
            this.cbSwitchFormat.SelectedIndexChanged += new System.EventHandler(this.cmbSwitchFormat_SelectedIndexChanged);
            // 
            // lblFileFormat
            // 
            this.lblFileFormat.AutoSize = true;
            this.lblFileFormat.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblFileFormat.Location = new System.Drawing.Point(16, 24);
            this.lblFileFormat.Name = "lblFileFormat";
            this.lblFileFormat.Size = new System.Drawing.Size(61, 13);
            this.lblFileFormat.TabIndex = 97;
            this.lblFileFormat.Text = "File Format:";
            // 
            // cbFileFormat
            // 
            this.cbFileFormat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbFileFormat.FormattingEnabled = true;
            this.cbFileFormat.Location = new System.Drawing.Point(16, 40);
            this.cbFileFormat.Name = "cbFileFormat";
            this.cbFileFormat.Size = new System.Drawing.Size(98, 21);
            this.cbFileFormat.TabIndex = 6;
            this.cbFileFormat.SelectedIndexChanged += new System.EventHandler(this.cmbFileFormat_SelectedIndexChanged);
            // 
            // lblKB
            // 
            this.lblKB.AutoSize = true;
            this.lblKB.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblKB.Location = new System.Drawing.Point(88, 100);
            this.lblKB.Name = "lblKB";
            this.lblKB.Size = new System.Drawing.Size(23, 13);
            this.lblKB.TabIndex = 95;
            this.lblKB.Text = "KiB";
            // 
            // lblAfter
            // 
            this.lblAfter.AutoSize = true;
            this.lblAfter.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblAfter.Location = new System.Drawing.Point(16, 80);
            this.lblAfter.Name = "lblAfter";
            this.lblAfter.Size = new System.Drawing.Size(88, 13);
            this.lblAfter.TabIndex = 93;
            this.lblAfter.Text = "After: (0 disables)";
            // 
            // lblSwitchTo
            // 
            this.lblSwitchTo.AutoSize = true;
            this.lblSwitchTo.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblSwitchTo.Location = new System.Drawing.Point(128, 80);
            this.lblSwitchTo.Name = "lblSwitchTo";
            this.lblSwitchTo.Size = new System.Drawing.Size(54, 13);
            this.lblSwitchTo.TabIndex = 92;
            this.lblSwitchTo.Text = "Switch to:";
            // 
            // tpEditors
            // 
            this.tpEditors.Controls.Add(this.tcEditors);
            this.tpEditors.ImageKey = "picture_edit.png";
            this.tpEditors.Location = new System.Drawing.Point(4, 23);
            this.tpEditors.Name = "tpEditors";
            this.tpEditors.Padding = new System.Windows.Forms.Padding(3);
            this.tpEditors.Size = new System.Drawing.Size(805, 442);
            this.tpEditors.TabIndex = 2;
            this.tpEditors.Text = "Editors";
            this.tpEditors.UseVisualStyleBackColor = true;
            // 
            // tcEditors
            // 
            this.tcEditors.Controls.Add(this.tpEditorsImages);
            this.tcEditors.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcEditors.Location = new System.Drawing.Point(3, 3);
            this.tcEditors.Name = "tcEditors";
            this.tcEditors.SelectedIndex = 0;
            this.tcEditors.Size = new System.Drawing.Size(799, 436);
            this.tcEditors.TabIndex = 64;
            // 
            // tpEditorsImages
            // 
            this.tpEditorsImages.Controls.Add(this.gbImageEditorSettings);
            this.tpEditorsImages.Controls.Add(this.pgEditorsImage);
            this.tpEditorsImages.Controls.Add(this.btnRemoveImageEditor);
            this.tpEditorsImages.Controls.Add(this.btnBrowseImageEditor);
            this.tpEditorsImages.Controls.Add(this.lbImageSoftware);
            this.tpEditorsImages.Controls.Add(this.btnAddImageSoftware);
            this.tpEditorsImages.Location = new System.Drawing.Point(4, 22);
            this.tpEditorsImages.Name = "tpEditorsImages";
            this.tpEditorsImages.Padding = new System.Windows.Forms.Padding(3);
            this.tpEditorsImages.Size = new System.Drawing.Size(791, 410);
            this.tpEditorsImages.TabIndex = 0;
            this.tpEditorsImages.Text = "Image Editors";
            this.tpEditorsImages.UseVisualStyleBackColor = true;
            // 
            // gbImageEditorSettings
            // 
            this.gbImageEditorSettings.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gbImageEditorSettings.Controls.Add(this.chkImageEditorAutoSave);
            this.gbImageEditorSettings.Location = new System.Drawing.Point(296, 120);
            this.gbImageEditorSettings.Name = "gbImageEditorSettings";
            this.gbImageEditorSettings.Size = new System.Drawing.Size(479, 56);
            this.gbImageEditorSettings.TabIndex = 67;
            this.gbImageEditorSettings.TabStop = false;
            this.gbImageEditorSettings.Text = "ZScreen Image Editor Settings";
            // 
            // chkImageEditorAutoSave
            // 
            this.chkImageEditorAutoSave.AutoSize = true;
            this.chkImageEditorAutoSave.Location = new System.Drawing.Point(16, 24);
            this.chkImageEditorAutoSave.Name = "chkImageEditorAutoSave";
            this.chkImageEditorAutoSave.Size = new System.Drawing.Size(193, 17);
            this.chkImageEditorAutoSave.TabIndex = 0;
            this.chkImageEditorAutoSave.Text = "&Automatically save changes on Exit";
            this.chkImageEditorAutoSave.UseVisualStyleBackColor = true;
            this.chkImageEditorAutoSave.CheckedChanged += new System.EventHandler(this.chkImageEditorAutoSave_CheckedChanged);
            // 
            // pgEditorsImage
            // 
            this.pgEditorsImage.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pgEditorsImage.HelpVisible = false;
            this.pgEditorsImage.Location = new System.Drawing.Point(296, 40);
            this.pgEditorsImage.Name = "pgEditorsImage";
            this.pgEditorsImage.PropertySort = System.Windows.Forms.PropertySort.NoSort;
            this.pgEditorsImage.Size = new System.Drawing.Size(479, 72);
            this.pgEditorsImage.TabIndex = 64;
            this.pgEditorsImage.ToolbarVisible = false;
            this.pgEditorsImage.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.pgEditorsImage_PropertyValueChanged);
            // 
            // btnRemoveImageEditor
            // 
            this.btnRemoveImageEditor.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnRemoveImageEditor.Location = new System.Drawing.Point(392, 8);
            this.btnRemoveImageEditor.Name = "btnRemoveImageEditor";
            this.btnRemoveImageEditor.Size = new System.Drawing.Size(88, 24);
            this.btnRemoveImageEditor.TabIndex = 58;
            this.btnRemoveImageEditor.Text = "&Remove";
            this.btnRemoveImageEditor.UseVisualStyleBackColor = true;
            this.btnRemoveImageEditor.Click += new System.EventHandler(this.btnDeleteImageSoftware_Click);
            // 
            // btnBrowseImageEditor
            // 
            this.btnBrowseImageEditor.AutoSize = true;
            this.btnBrowseImageEditor.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnBrowseImageEditor.Location = new System.Drawing.Point(488, 8);
            this.btnBrowseImageEditor.Name = "btnBrowseImageEditor";
            this.btnBrowseImageEditor.Size = new System.Drawing.Size(88, 23);
            this.btnBrowseImageEditor.TabIndex = 6;
            this.btnBrowseImageEditor.Text = "Browse...";
            this.btnBrowseImageEditor.UseVisualStyleBackColor = true;
            this.btnBrowseImageEditor.Click += new System.EventHandler(this.btnBrowseImageSoftware_Click);
            // 
            // lbImageSoftware
            // 
            this.lbImageSoftware.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbImageSoftware.FormattingEnabled = true;
            this.lbImageSoftware.IntegralHeight = false;
            this.lbImageSoftware.Location = new System.Drawing.Point(3, 3);
            this.lbImageSoftware.Name = "lbImageSoftware";
            this.lbImageSoftware.Size = new System.Drawing.Size(280, 404);
            this.lbImageSoftware.TabIndex = 59;
            this.lbImageSoftware.SelectedIndexChanged += new System.EventHandler(this.lbImageSoftware_SelectedIndexChanged);
            // 
            // btnAddImageSoftware
            // 
            this.btnAddImageSoftware.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnAddImageSoftware.BackColor = System.Drawing.Color.Transparent;
            this.btnAddImageSoftware.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnAddImageSoftware.Location = new System.Drawing.Point(296, 8);
            this.btnAddImageSoftware.Name = "btnAddImageSoftware";
            this.btnAddImageSoftware.Size = new System.Drawing.Size(88, 24);
            this.btnAddImageSoftware.TabIndex = 59;
            this.btnAddImageSoftware.Text = "Add...";
            this.btnAddImageSoftware.UseVisualStyleBackColor = false;
            this.btnAddImageSoftware.Click += new System.EventHandler(this.btnAddImageSoftware_Click);
            // 
            // tpImageHosting
            // 
            this.tpImageHosting.Controls.Add(this.tcImages);
            this.tpImageHosting.ImageKey = "picture_go.png";
            this.tpImageHosting.Location = new System.Drawing.Point(4, 23);
            this.tpImageHosting.Name = "tpImageHosting";
            this.tpImageHosting.Padding = new System.Windows.Forms.Padding(3);
            this.tpImageHosting.Size = new System.Drawing.Size(805, 442);
            this.tpImageHosting.TabIndex = 10;
            this.tpImageHosting.Text = "Image Hosting";
            this.tpImageHosting.UseVisualStyleBackColor = true;
            // 
            // tcImages
            // 
            this.tcImages.Controls.Add(this.tpImageUploaders);
            this.tcImages.Controls.Add(this.tpCustomUploaders);
            this.tcImages.Controls.Add(this.tpWebPageUpload);
            this.tcImages.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcImages.ImageList = this.ilApp;
            this.tcImages.Location = new System.Drawing.Point(3, 3);
            this.tcImages.Name = "tcImages";
            this.tcImages.SelectedIndex = 0;
            this.tcImages.Size = new System.Drawing.Size(799, 436);
            this.tcImages.TabIndex = 5;
            // 
            // tpImageUploaders
            // 
            this.tpImageUploaders.Controls.Add(this.gbImageUploadRetry);
            this.tpImageUploaders.Controls.Add(this.gbImageUploaderOptions);
            this.tpImageUploaders.Location = new System.Drawing.Point(4, 23);
            this.tpImageUploaders.Name = "tpImageUploaders";
            this.tpImageUploaders.Padding = new System.Windows.Forms.Padding(3);
            this.tpImageUploaders.Size = new System.Drawing.Size(791, 409);
            this.tpImageUploaders.TabIndex = 0;
            this.tpImageUploaders.Text = "Image Uploaders";
            this.tpImageUploaders.UseVisualStyleBackColor = true;
            // 
            // gbImageUploadRetry
            // 
            this.gbImageUploadRetry.Controls.Add(this.lblErrorRetry);
            this.gbImageUploadRetry.Controls.Add(this.lblUploadDurationLimit);
            this.gbImageUploadRetry.Controls.Add(this.chkImageUploadRetryOnFail);
            this.gbImageUploadRetry.Controls.Add(this.cboImageUploadRetryOnTimeout);
            this.gbImageUploadRetry.Controls.Add(this.nudUploadDurationLimit);
            this.gbImageUploadRetry.Controls.Add(this.nudErrorRetry);
            this.gbImageUploadRetry.Location = new System.Drawing.Point(8, 152);
            this.gbImageUploadRetry.Name = "gbImageUploadRetry";
            this.gbImageUploadRetry.Size = new System.Drawing.Size(776, 104);
            this.gbImageUploadRetry.TabIndex = 8;
            this.gbImageUploadRetry.TabStop = false;
            this.gbImageUploadRetry.Text = "Retry Options";
            // 
            // lblErrorRetry
            // 
            this.lblErrorRetry.AutoSize = true;
            this.lblErrorRetry.Location = new System.Drawing.Point(16, 24);
            this.lblErrorRetry.Name = "lblErrorRetry";
            this.lblErrorRetry.Size = new System.Drawing.Size(95, 13);
            this.lblErrorRetry.TabIndex = 11;
            this.lblErrorRetry.Text = "Number of Retries:";
            // 
            // lblUploadDurationLimit
            // 
            this.lblUploadDurationLimit.AutoSize = true;
            this.lblUploadDurationLimit.Location = new System.Drawing.Point(368, 72);
            this.lblUploadDurationLimit.Name = "lblUploadDurationLimit";
            this.lblUploadDurationLimit.Size = new System.Drawing.Size(61, 13);
            this.lblUploadDurationLimit.TabIndex = 10;
            this.lblUploadDurationLimit.Text = "miliseconds";
            // 
            // chkImageUploadRetryOnFail
            // 
            this.chkImageUploadRetryOnFail.AutoSize = true;
            this.chkImageUploadRetryOnFail.Location = new System.Drawing.Point(16, 48);
            this.chkImageUploadRetryOnFail.Name = "chkImageUploadRetryOnFail";
            this.chkImageUploadRetryOnFail.Size = new System.Drawing.Size(390, 17);
            this.chkImageUploadRetryOnFail.TabIndex = 6;
            this.chkImageUploadRetryOnFail.Text = "Retry with another Image Uploader if the Image Uploader fails the first attempt";
            this.ttZScreen.SetToolTip(this.chkImageUploadRetryOnFail, "This setting ignores Retry Count. Only happens between ImageShack and TinyPic.");
            this.chkImageUploadRetryOnFail.UseVisualStyleBackColor = true;
            this.chkImageUploadRetryOnFail.CheckedChanged += new System.EventHandler(this.cbImageUploadRetry_CheckedChanged);
            // 
            // cboImageUploadRetryOnTimeout
            // 
            this.cboImageUploadRetryOnTimeout.AutoSize = true;
            this.cboImageUploadRetryOnTimeout.Location = new System.Drawing.Point(16, 72);
            this.cboImageUploadRetryOnTimeout.Name = "cboImageUploadRetryOnTimeout";
            this.cboImageUploadRetryOnTimeout.Size = new System.Drawing.Size(282, 17);
            this.cboImageUploadRetryOnTimeout.TabIndex = 8;
            this.cboImageUploadRetryOnTimeout.Text = "Change the Image Uploader if the upload times out by ";
            this.ttZScreen.SetToolTip(this.cboImageUploadRetryOnTimeout, "This setting ignores Retry Count. Only happens between ImageShack and TinyPic.");
            this.cboImageUploadRetryOnTimeout.UseVisualStyleBackColor = true;
            this.cboImageUploadRetryOnTimeout.CheckedChanged += new System.EventHandler(this.cbAutoChangeUploadDestination_CheckedChanged);
            // 
            // nudUploadDurationLimit
            // 
            this.nudUploadDurationLimit.Location = new System.Drawing.Point(299, 70);
            this.nudUploadDurationLimit.Maximum = new decimal(new int[] {
            300000,
            0,
            0,
            0});
            this.nudUploadDurationLimit.Name = "nudUploadDurationLimit";
            this.nudUploadDurationLimit.Size = new System.Drawing.Size(64, 20);
            this.nudUploadDurationLimit.TabIndex = 9;
            this.nudUploadDurationLimit.ValueChanged += new System.EventHandler(this.nudUploadDurationLimit_ValueChanged);
            // 
            // nudErrorRetry
            // 
            this.nudErrorRetry.Location = new System.Drawing.Point(120, 22);
            this.nudErrorRetry.Name = "nudErrorRetry";
            this.nudErrorRetry.Size = new System.Drawing.Size(40, 20);
            this.nudErrorRetry.TabIndex = 3;
            this.ttZScreen.SetToolTip(this.nudErrorRetry, "Choose 0 if you do not wish to retry uploading");
            this.nudErrorRetry.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.nudErrorRetry.ValueChanged += new System.EventHandler(this.nErrorRetry_ValueChanged);
            // 
            // gbImageUploaderOptions
            // 
            this.gbImageUploaderOptions.Controls.Add(this.cbAutoSwitchFileUploader);
            this.gbImageUploaderOptions.Controls.Add(this.cbTinyPicSizeCheck);
            this.gbImageUploaderOptions.Controls.Add(this.cbAddFailedScreenshot);
            this.gbImageUploaderOptions.Controls.Add(this.cboUploadMode);
            this.gbImageUploaderOptions.Controls.Add(this.lblUploadAs);
            this.gbImageUploaderOptions.Location = new System.Drawing.Point(8, 8);
            this.gbImageUploaderOptions.Name = "gbImageUploaderOptions";
            this.gbImageUploaderOptions.Size = new System.Drawing.Size(776, 136);
            this.gbImageUploaderOptions.TabIndex = 7;
            this.gbImageUploaderOptions.TabStop = false;
            this.gbImageUploaderOptions.Text = "General Options";
            // 
            // cbAutoSwitchFileUploader
            // 
            this.cbAutoSwitchFileUploader.AutoSize = true;
            this.cbAutoSwitchFileUploader.Location = new System.Drawing.Point(16, 104);
            this.cbAutoSwitchFileUploader.Name = "cbAutoSwitchFileUploader";
            this.cbAutoSwitchFileUploader.Size = new System.Drawing.Size(465, 17);
            this.cbAutoSwitchFileUploader.TabIndex = 114;
            this.cbAutoSwitchFileUploader.Text = "Automatically switch to File Uploader if a user copies (Clipboard Upload) or drag" +
                "s a non-Image";
            this.cbAutoSwitchFileUploader.UseVisualStyleBackColor = true;
            this.cbAutoSwitchFileUploader.CheckedChanged += new System.EventHandler(this.chkAutoSwitchFTP_CheckedChanged);
            // 
            // cbTinyPicSizeCheck
            // 
            this.cbTinyPicSizeCheck.AutoSize = true;
            this.cbTinyPicSizeCheck.Location = new System.Drawing.Point(16, 80);
            this.cbTinyPicSizeCheck.Name = "cbTinyPicSizeCheck";
            this.cbTinyPicSizeCheck.Size = new System.Drawing.Size(440, 17);
            this.cbTinyPicSizeCheck.TabIndex = 7;
            this.cbTinyPicSizeCheck.Text = "Switch from TinyPic to ImageShack if the image dimensions are greater than 1600 p" +
                "ixels";
            this.cbTinyPicSizeCheck.UseVisualStyleBackColor = true;
            this.cbTinyPicSizeCheck.CheckedChanged += new System.EventHandler(this.cbTinyPicSizeCheck_CheckedChanged);
            // 
            // cbAddFailedScreenshot
            // 
            this.cbAddFailedScreenshot.AutoSize = true;
            this.cbAddFailedScreenshot.Location = new System.Drawing.Point(16, 56);
            this.cbAddFailedScreenshot.Name = "cbAddFailedScreenshot";
            this.cbAddFailedScreenshot.Size = new System.Drawing.Size(143, 17);
            this.cbAddFailedScreenshot.TabIndex = 7;
            this.cbAddFailedScreenshot.Text = "Add failed task to History";
            this.cbAddFailedScreenshot.UseVisualStyleBackColor = true;
            this.cbAddFailedScreenshot.CheckedChanged += new System.EventHandler(this.cbAddFailedScreenshot_CheckedChanged);
            // 
            // cboUploadMode
            // 
            this.cboUploadMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboUploadMode.FormattingEnabled = true;
            this.cboUploadMode.Location = new System.Drawing.Point(80, 24);
            this.cboUploadMode.Name = "cboUploadMode";
            this.cboUploadMode.Size = new System.Drawing.Size(121, 21);
            this.cboUploadMode.TabIndex = 5;
            this.cboUploadMode.SelectedIndexChanged += new System.EventHandler(this.cboUploadMode_SelectedIndexChanged);
            // 
            // lblUploadAs
            // 
            this.lblUploadAs.AutoSize = true;
            this.lblUploadAs.Location = new System.Drawing.Point(16, 28);
            this.lblUploadAs.Name = "lblUploadAs";
            this.lblUploadAs.Size = new System.Drawing.Size(58, 13);
            this.lblUploadAs.TabIndex = 4;
            this.lblUploadAs.Text = "Upload as:";
            // 
            // tpCustomUploaders
            // 
            this.tpCustomUploaders.Controls.Add(this.txtUploadersLog);
            this.tpCustomUploaders.Controls.Add(this.btnUploadersTest);
            this.tpCustomUploaders.Controls.Add(this.txtFullImage);
            this.tpCustomUploaders.Controls.Add(this.txtThumbnail);
            this.tpCustomUploaders.Controls.Add(this.lblFullImage);
            this.tpCustomUploaders.Controls.Add(this.lblThumbnail);
            this.tpCustomUploaders.Controls.Add(this.gbImageUploaders);
            this.tpCustomUploaders.Controls.Add(this.gbRegexp);
            this.tpCustomUploaders.Controls.Add(this.txtFileForm);
            this.tpCustomUploaders.Controls.Add(this.lblFileForm);
            this.tpCustomUploaders.Controls.Add(this.lblUploadURL);
            this.tpCustomUploaders.Controls.Add(this.txtUploadURL);
            this.tpCustomUploaders.Controls.Add(this.gbArguments);
            this.tpCustomUploaders.ImageKey = "world_add.png";
            this.tpCustomUploaders.Location = new System.Drawing.Point(4, 23);
            this.tpCustomUploaders.Name = "tpCustomUploaders";
            this.tpCustomUploaders.Padding = new System.Windows.Forms.Padding(3);
            this.tpCustomUploaders.Size = new System.Drawing.Size(791, 409);
            this.tpCustomUploaders.TabIndex = 11;
            this.tpCustomUploaders.Text = "Custom Image Uploaders";
            this.tpCustomUploaders.UseVisualStyleBackColor = true;
            // 
            // txtUploadersLog
            // 
            this.txtUploadersLog.Location = new System.Drawing.Point(8, 280);
            this.txtUploadersLog.Name = "txtUploadersLog";
            this.txtUploadersLog.Size = new System.Drawing.Size(416, 104);
            this.txtUploadersLog.TabIndex = 18;
            this.txtUploadersLog.Text = "";
            this.txtUploadersLog.LinkClicked += new System.Windows.Forms.LinkClickedEventHandler(this.txtUploadersLog_LinkClicked);
            // 
            // btnUploadersTest
            // 
            this.btnUploadersTest.Location = new System.Drawing.Point(432, 360);
            this.btnUploadersTest.Name = "btnUploadersTest";
            this.btnUploadersTest.Size = new System.Drawing.Size(328, 24);
            this.btnUploadersTest.TabIndex = 9;
            this.btnUploadersTest.Text = "Test Upload";
            this.btnUploadersTest.UseVisualStyleBackColor = true;
            this.btnUploadersTest.Click += new System.EventHandler(this.btUploadersTest_Click);
            // 
            // txtFullImage
            // 
            this.txtFullImage.Location = new System.Drawing.Point(432, 296);
            this.txtFullImage.Name = "txtFullImage";
            this.txtFullImage.Size = new System.Drawing.Size(328, 20);
            this.txtFullImage.TabIndex = 5;
            // 
            // txtThumbnail
            // 
            this.txtThumbnail.Location = new System.Drawing.Point(432, 336);
            this.txtThumbnail.Name = "txtThumbnail";
            this.txtThumbnail.Size = new System.Drawing.Size(328, 20);
            this.txtThumbnail.TabIndex = 6;
            // 
            // lblFullImage
            // 
            this.lblFullImage.AutoSize = true;
            this.lblFullImage.Location = new System.Drawing.Point(432, 280);
            this.lblFullImage.Name = "lblFullImage";
            this.lblFullImage.Size = new System.Drawing.Size(55, 13);
            this.lblFullImage.TabIndex = 17;
            this.lblFullImage.Text = "Full Image";
            // 
            // lblThumbnail
            // 
            this.lblThumbnail.AutoSize = true;
            this.lblThumbnail.Location = new System.Drawing.Point(432, 320);
            this.lblThumbnail.Name = "lblThumbnail";
            this.lblThumbnail.Size = new System.Drawing.Size(56, 13);
            this.lblThumbnail.TabIndex = 16;
            this.lblThumbnail.Text = "Thumbnail";
            // 
            // gbImageUploaders
            // 
            this.gbImageUploaders.Controls.Add(this.lbUploader);
            this.gbImageUploaders.Controls.Add(this.btnUploadersClear);
            this.gbImageUploaders.Controls.Add(this.btnUploaderExport);
            this.gbImageUploaders.Controls.Add(this.btnUploaderRemove);
            this.gbImageUploaders.Controls.Add(this.btnUploaderImport);
            this.gbImageUploaders.Controls.Add(this.btnUploaderUpdate);
            this.gbImageUploaders.Controls.Add(this.txtUploader);
            this.gbImageUploaders.Controls.Add(this.btnUploaderAdd);
            this.gbImageUploaders.Location = new System.Drawing.Point(8, 8);
            this.gbImageUploaders.Name = "gbImageUploaders";
            this.gbImageUploaders.Size = new System.Drawing.Size(248, 264);
            this.gbImageUploaders.TabIndex = 0;
            this.gbImageUploaders.TabStop = false;
            this.gbImageUploaders.Text = "Image Hosting Services";
            // 
            // lbUploader
            // 
            this.lbUploader.FormattingEnabled = true;
            this.lbUploader.Location = new System.Drawing.Point(8, 72);
            this.lbUploader.Name = "lbUploader";
            this.lbUploader.Size = new System.Drawing.Size(232, 147);
            this.lbUploader.TabIndex = 3;
            this.lbUploader.SelectedIndexChanged += new System.EventHandler(this.lbUploader_SelectedIndexChanged);
            // 
            // btnUploadersClear
            // 
            this.btnUploadersClear.Location = new System.Drawing.Point(168, 232);
            this.btnUploadersClear.Name = "btnUploadersClear";
            this.btnUploadersClear.Size = new System.Drawing.Size(72, 24);
            this.btnUploadersClear.TabIndex = 8;
            this.btnUploadersClear.Text = "Clear";
            this.btnUploadersClear.UseVisualStyleBackColor = true;
            this.btnUploadersClear.Click += new System.EventHandler(this.btnUploadersClear_Click);
            // 
            // btnUploaderExport
            // 
            this.btnUploaderExport.Location = new System.Drawing.Point(88, 232);
            this.btnUploaderExport.Name = "btnUploaderExport";
            this.btnUploaderExport.Size = new System.Drawing.Size(72, 24);
            this.btnUploaderExport.TabIndex = 5;
            this.btnUploaderExport.Text = "Export...";
            this.btnUploaderExport.UseVisualStyleBackColor = true;
            this.btnUploaderExport.Click += new System.EventHandler(this.btnUploaderExport_Click);
            // 
            // btnUploaderRemove
            // 
            this.btnUploaderRemove.Location = new System.Drawing.Point(88, 40);
            this.btnUploaderRemove.Name = "btnUploaderRemove";
            this.btnUploaderRemove.Size = new System.Drawing.Size(72, 24);
            this.btnUploaderRemove.TabIndex = 2;
            this.btnUploaderRemove.Text = "Remove";
            this.btnUploaderRemove.UseVisualStyleBackColor = true;
            this.btnUploaderRemove.Click += new System.EventHandler(this.btnUploaderRemove_Click);
            // 
            // btnUploaderImport
            // 
            this.btnUploaderImport.Location = new System.Drawing.Point(8, 232);
            this.btnUploaderImport.Name = "btnUploaderImport";
            this.btnUploaderImport.Size = new System.Drawing.Size(72, 24);
            this.btnUploaderImport.TabIndex = 4;
            this.btnUploaderImport.Text = "Import...";
            this.btnUploaderImport.UseVisualStyleBackColor = true;
            this.btnUploaderImport.Click += new System.EventHandler(this.btnUploaderImport_Click);
            // 
            // btnUploaderUpdate
            // 
            this.btnUploaderUpdate.Location = new System.Drawing.Point(168, 40);
            this.btnUploaderUpdate.Name = "btnUploaderUpdate";
            this.btnUploaderUpdate.Size = new System.Drawing.Size(72, 24);
            this.btnUploaderUpdate.TabIndex = 7;
            this.btnUploaderUpdate.Text = "Update";
            this.btnUploaderUpdate.UseVisualStyleBackColor = true;
            this.btnUploaderUpdate.Click += new System.EventHandler(this.btnUploadersUpdate_Click);
            // 
            // txtUploader
            // 
            this.txtUploader.Location = new System.Drawing.Point(8, 16);
            this.txtUploader.Name = "txtUploader";
            this.txtUploader.Size = new System.Drawing.Size(232, 20);
            this.txtUploader.TabIndex = 0;
            // 
            // btnUploaderAdd
            // 
            this.btnUploaderAdd.Location = new System.Drawing.Point(8, 40);
            this.btnUploaderAdd.Name = "btnUploaderAdd";
            this.btnUploaderAdd.Size = new System.Drawing.Size(72, 24);
            this.btnUploaderAdd.TabIndex = 1;
            this.btnUploaderAdd.Text = "Add";
            this.btnUploaderAdd.UseVisualStyleBackColor = true;
            this.btnUploaderAdd.Click += new System.EventHandler(this.btnUploaderAdd_Click);
            // 
            // gbRegexp
            // 
            this.gbRegexp.Controls.Add(this.btnRegexpEdit);
            this.gbRegexp.Controls.Add(this.txtRegexp);
            this.gbRegexp.Controls.Add(this.lvRegexps);
            this.gbRegexp.Controls.Add(this.btnRegexpRemove);
            this.gbRegexp.Controls.Add(this.btnRegexpAdd);
            this.gbRegexp.Location = new System.Drawing.Point(520, 88);
            this.gbRegexp.Name = "gbRegexp";
            this.gbRegexp.Size = new System.Drawing.Size(240, 184);
            this.gbRegexp.TabIndex = 4;
            this.gbRegexp.TabStop = false;
            this.gbRegexp.Text = "Regexp from Source";
            // 
            // btnRegexpEdit
            // 
            this.btnRegexpEdit.Location = new System.Drawing.Point(160, 40);
            this.btnRegexpEdit.Name = "btnRegexpEdit";
            this.btnRegexpEdit.Size = new System.Drawing.Size(72, 24);
            this.btnRegexpEdit.TabIndex = 4;
            this.btnRegexpEdit.Text = "Edit";
            this.btnRegexpEdit.UseVisualStyleBackColor = true;
            this.btnRegexpEdit.Click += new System.EventHandler(this.btnRegexpEdit_Click);
            // 
            // txtRegexp
            // 
            this.txtRegexp.Location = new System.Drawing.Point(8, 16);
            this.txtRegexp.Name = "txtRegexp";
            this.txtRegexp.Size = new System.Drawing.Size(224, 20);
            this.txtRegexp.TabIndex = 0;
            // 
            // lvRegexps
            // 
            this.lvRegexps.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.lvRegexpsColumn});
            this.lvRegexps.FullRowSelect = true;
            this.lvRegexps.GridLines = true;
            this.lvRegexps.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.lvRegexps.HideSelection = false;
            this.lvRegexps.Location = new System.Drawing.Point(8, 72);
            this.lvRegexps.MultiSelect = false;
            this.lvRegexps.Name = "lvRegexps";
            this.lvRegexps.Scrollable = false;
            this.lvRegexps.Size = new System.Drawing.Size(226, 104);
            this.lvRegexps.TabIndex = 3;
            this.lvRegexps.UseCompatibleStateImageBehavior = false;
            this.lvRegexps.View = System.Windows.Forms.View.Details;
            // 
            // lvRegexpsColumn
            // 
            this.lvRegexpsColumn.Width = 227;
            // 
            // btnRegexpRemove
            // 
            this.btnRegexpRemove.Location = new System.Drawing.Point(84, 40);
            this.btnRegexpRemove.Name = "btnRegexpRemove";
            this.btnRegexpRemove.Size = new System.Drawing.Size(72, 24);
            this.btnRegexpRemove.TabIndex = 2;
            this.btnRegexpRemove.Text = "Remove";
            this.btnRegexpRemove.UseVisualStyleBackColor = true;
            this.btnRegexpRemove.Click += new System.EventHandler(this.btnRegexpRemove_Click);
            // 
            // btnRegexpAdd
            // 
            this.btnRegexpAdd.Location = new System.Drawing.Point(8, 40);
            this.btnRegexpAdd.Name = "btnRegexpAdd";
            this.btnRegexpAdd.Size = new System.Drawing.Size(72, 24);
            this.btnRegexpAdd.TabIndex = 1;
            this.btnRegexpAdd.Text = "Add";
            this.btnRegexpAdd.UseVisualStyleBackColor = true;
            this.btnRegexpAdd.Click += new System.EventHandler(this.btnRegexpAdd_Click);
            // 
            // txtFileForm
            // 
            this.txtFileForm.Location = new System.Drawing.Point(528, 64);
            this.txtFileForm.Name = "txtFileForm";
            this.txtFileForm.Size = new System.Drawing.Size(224, 20);
            this.txtFileForm.TabIndex = 3;
            // 
            // lblFileForm
            // 
            this.lblFileForm.AutoSize = true;
            this.lblFileForm.Location = new System.Drawing.Point(528, 48);
            this.lblFileForm.Name = "lblFileForm";
            this.lblFileForm.Size = new System.Drawing.Size(83, 13);
            this.lblFileForm.TabIndex = 9;
            this.lblFileForm.Text = "File Form Name:";
            // 
            // lblUploadURL
            // 
            this.lblUploadURL.AutoSize = true;
            this.lblUploadURL.Location = new System.Drawing.Point(528, 8);
            this.lblUploadURL.Name = "lblUploadURL";
            this.lblUploadURL.Size = new System.Drawing.Size(69, 13);
            this.lblUploadURL.TabIndex = 8;
            this.lblUploadURL.Text = "Upload URL:";
            // 
            // txtUploadURL
            // 
            this.txtUploadURL.Location = new System.Drawing.Point(528, 24);
            this.txtUploadURL.Name = "txtUploadURL";
            this.txtUploadURL.Size = new System.Drawing.Size(224, 20);
            this.txtUploadURL.TabIndex = 2;
            // 
            // gbArguments
            // 
            this.gbArguments.Controls.Add(this.btnArgEdit);
            this.gbArguments.Controls.Add(this.txtArg2);
            this.gbArguments.Controls.Add(this.btnArgRemove);
            this.gbArguments.Controls.Add(this.lvArguments);
            this.gbArguments.Controls.Add(this.btnArgAdd);
            this.gbArguments.Controls.Add(this.txtArg1);
            this.gbArguments.Location = new System.Drawing.Point(264, 8);
            this.gbArguments.Name = "gbArguments";
            this.gbArguments.Size = new System.Drawing.Size(248, 264);
            this.gbArguments.TabIndex = 1;
            this.gbArguments.TabStop = false;
            this.gbArguments.Text = "Arguments";
            // 
            // btnArgEdit
            // 
            this.btnArgEdit.Location = new System.Drawing.Point(168, 40);
            this.btnArgEdit.Name = "btnArgEdit";
            this.btnArgEdit.Size = new System.Drawing.Size(72, 24);
            this.btnArgEdit.TabIndex = 5;
            this.btnArgEdit.Text = "Edit";
            this.btnArgEdit.UseVisualStyleBackColor = true;
            this.btnArgEdit.Click += new System.EventHandler(this.btnArgEdit_Click);
            // 
            // txtArg2
            // 
            this.txtArg2.Location = new System.Drawing.Point(128, 16);
            this.txtArg2.Name = "txtArg2";
            this.txtArg2.Size = new System.Drawing.Size(112, 20);
            this.txtArg2.TabIndex = 1;
            // 
            // btnArgRemove
            // 
            this.btnArgRemove.Location = new System.Drawing.Point(88, 40);
            this.btnArgRemove.Name = "btnArgRemove";
            this.btnArgRemove.Size = new System.Drawing.Size(72, 24);
            this.btnArgRemove.TabIndex = 3;
            this.btnArgRemove.Text = "Remove";
            this.btnArgRemove.UseVisualStyleBackColor = true;
            this.btnArgRemove.Click += new System.EventHandler(this.btnArgRemove_Click);
            // 
            // lvArguments
            // 
            this.lvArguments.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.lvArguments.FullRowSelect = true;
            this.lvArguments.GridLines = true;
            this.lvArguments.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.lvArguments.HideSelection = false;
            this.lvArguments.Location = new System.Drawing.Point(8, 72);
            this.lvArguments.MultiSelect = false;
            this.lvArguments.Name = "lvArguments";
            this.lvArguments.Size = new System.Drawing.Size(232, 184);
            this.lvArguments.TabIndex = 4;
            this.lvArguments.UseCompatibleStateImageBehavior = false;
            this.lvArguments.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Width = 113;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Width = 113;
            // 
            // btnArgAdd
            // 
            this.btnArgAdd.Location = new System.Drawing.Point(8, 40);
            this.btnArgAdd.Name = "btnArgAdd";
            this.btnArgAdd.Size = new System.Drawing.Size(72, 24);
            this.btnArgAdd.TabIndex = 2;
            this.btnArgAdd.Text = "Add";
            this.btnArgAdd.UseVisualStyleBackColor = true;
            this.btnArgAdd.Click += new System.EventHandler(this.btnArgAdd_Click);
            // 
            // txtArg1
            // 
            this.txtArg1.Location = new System.Drawing.Point(8, 16);
            this.txtArg1.Name = "txtArg1";
            this.txtArg1.Size = new System.Drawing.Size(112, 20);
            this.txtArg1.TabIndex = 0;
            // 
            // tpWebPageUpload
            // 
            this.tpWebPageUpload.Controls.Add(this.cbWebPageAutoUpload);
            this.tpWebPageUpload.Controls.Add(this.lblWebPageHeight);
            this.tpWebPageUpload.Controls.Add(this.lblWebPageWidth);
            this.tpWebPageUpload.Controls.Add(this.txtWebPageHeight);
            this.tpWebPageUpload.Controls.Add(this.txtWebPageWidth);
            this.tpWebPageUpload.Controls.Add(this.cbWebPageUseCustomSize);
            this.tpWebPageUpload.Controls.Add(this.btnWebPageImageUpload);
            this.tpWebPageUpload.Controls.Add(this.pWebPageImage);
            this.tpWebPageUpload.Controls.Add(this.btnWebPageCaptureImage);
            this.tpWebPageUpload.Controls.Add(this.txtWebPageURL);
            this.tpWebPageUpload.Location = new System.Drawing.Point(4, 23);
            this.tpWebPageUpload.Name = "tpWebPageUpload";
            this.tpWebPageUpload.Padding = new System.Windows.Forms.Padding(3);
            this.tpWebPageUpload.Size = new System.Drawing.Size(791, 409);
            this.tpWebPageUpload.TabIndex = 12;
            this.tpWebPageUpload.Text = "Webpage Uploader";
            this.tpWebPageUpload.UseVisualStyleBackColor = true;
            // 
            // cbWebPageAutoUpload
            // 
            this.cbWebPageAutoUpload.AutoSize = true;
            this.cbWebPageAutoUpload.Location = new System.Drawing.Point(592, 48);
            this.cbWebPageAutoUpload.Name = "cbWebPageAutoUpload";
            this.cbWebPageAutoUpload.Size = new System.Drawing.Size(83, 17);
            this.cbWebPageAutoUpload.TabIndex = 8;
            this.cbWebPageAutoUpload.Text = "Auto upload";
            this.cbWebPageAutoUpload.UseVisualStyleBackColor = true;
            this.cbWebPageAutoUpload.CheckedChanged += new System.EventHandler(this.cbWebPageAutoUpload_CheckedChanged);
            // 
            // lblWebPageHeight
            // 
            this.lblWebPageHeight.AutoSize = true;
            this.lblWebPageHeight.Location = new System.Drawing.Point(256, 48);
            this.lblWebPageHeight.Name = "lblWebPageHeight";
            this.lblWebPageHeight.Size = new System.Drawing.Size(41, 13);
            this.lblWebPageHeight.TabIndex = 7;
            this.lblWebPageHeight.Text = "Height:";
            // 
            // lblWebPageWidth
            // 
            this.lblWebPageWidth.AutoSize = true;
            this.lblWebPageWidth.Location = new System.Drawing.Point(168, 48);
            this.lblWebPageWidth.Name = "lblWebPageWidth";
            this.lblWebPageWidth.Size = new System.Drawing.Size(38, 13);
            this.lblWebPageWidth.TabIndex = 6;
            this.lblWebPageWidth.Text = "Width:";
            // 
            // txtWebPageHeight
            // 
            this.txtWebPageHeight.Location = new System.Drawing.Point(304, 43);
            this.txtWebPageHeight.Name = "txtWebPageHeight";
            this.txtWebPageHeight.Size = new System.Drawing.Size(40, 20);
            this.txtWebPageHeight.TabIndex = 5;
            this.txtWebPageHeight.TextChanged += new System.EventHandler(this.txtWebPageHeight_TextChanged);
            // 
            // txtWebPageWidth
            // 
            this.txtWebPageWidth.Location = new System.Drawing.Point(208, 43);
            this.txtWebPageWidth.Name = "txtWebPageWidth";
            this.txtWebPageWidth.Size = new System.Drawing.Size(40, 20);
            this.txtWebPageWidth.TabIndex = 4;
            this.txtWebPageWidth.TextChanged += new System.EventHandler(this.txtWebPageWidth_TextChanged);
            // 
            // cbWebPageUseCustomSize
            // 
            this.cbWebPageUseCustomSize.AutoSize = true;
            this.cbWebPageUseCustomSize.Location = new System.Drawing.Point(16, 48);
            this.cbWebPageUseCustomSize.Name = "cbWebPageUseCustomSize";
            this.cbWebPageUseCustomSize.Size = new System.Drawing.Size(146, 17);
            this.cbWebPageUseCustomSize.TabIndex = 3;
            this.cbWebPageUseCustomSize.Text = "Use custom browser size:";
            this.ttZScreen.SetToolTip(this.cbWebPageUseCustomSize, "Default size is primary monitor size");
            this.cbWebPageUseCustomSize.UseVisualStyleBackColor = true;
            this.cbWebPageUseCustomSize.CheckedChanged += new System.EventHandler(this.cbWebPageUseCustomSize_CheckedChanged);
            // 
            // btnWebPageImageUpload
            // 
            this.btnWebPageImageUpload.Enabled = false;
            this.btnWebPageImageUpload.Location = new System.Drawing.Point(680, 40);
            this.btnWebPageImageUpload.Name = "btnWebPageImageUpload";
            this.btnWebPageImageUpload.Size = new System.Drawing.Size(96, 24);
            this.btnWebPageImageUpload.TabIndex = 1;
            this.btnWebPageImageUpload.Text = "Upload";
            this.btnWebPageImageUpload.UseVisualStyleBackColor = true;
            this.btnWebPageImageUpload.Click += new System.EventHandler(this.btnWebPageImageUpload_Click);
            // 
            // pWebPageImage
            // 
            this.pWebPageImage.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pWebPageImage.AutoScroll = true;
            this.pWebPageImage.BackColor = System.Drawing.Color.White;
            this.pWebPageImage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pWebPageImage.Controls.Add(this.pbWebPageImage);
            this.pWebPageImage.Location = new System.Drawing.Point(16, 72);
            this.pWebPageImage.Name = "pWebPageImage";
            this.pWebPageImage.Size = new System.Drawing.Size(760, 320);
            this.pWebPageImage.TabIndex = 2;
            // 
            // pbWebPageImage
            // 
            this.pbWebPageImage.BackColor = System.Drawing.Color.White;
            this.pbWebPageImage.Location = new System.Drawing.Point(0, 0);
            this.pbWebPageImage.Name = "pbWebPageImage";
            this.pbWebPageImage.Size = new System.Drawing.Size(100, 50);
            this.pbWebPageImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pbWebPageImage.TabIndex = 0;
            this.pbWebPageImage.TabStop = false;
            // 
            // btnWebPageCaptureImage
            // 
            this.btnWebPageCaptureImage.Location = new System.Drawing.Point(680, 13);
            this.btnWebPageCaptureImage.Name = "btnWebPageCaptureImage";
            this.btnWebPageCaptureImage.Size = new System.Drawing.Size(96, 24);
            this.btnWebPageCaptureImage.TabIndex = 1;
            this.btnWebPageCaptureImage.Text = "Capture Image";
            this.btnWebPageCaptureImage.UseVisualStyleBackColor = true;
            this.btnWebPageCaptureImage.Click += new System.EventHandler(this.btnWebPageUploadImage_Click);
            // 
            // txtWebPageURL
            // 
            this.txtWebPageURL.Location = new System.Drawing.Point(16, 16);
            this.txtWebPageURL.Name = "txtWebPageURL";
            this.txtWebPageURL.Size = new System.Drawing.Size(656, 20);
            this.txtWebPageURL.TabIndex = 0;
            // 
            // tpTextServices
            // 
            this.tpTextServices.Controls.Add(this.tcTextUploaders);
            this.tpTextServices.ImageKey = "text_signature.png";
            this.tpTextServices.Location = new System.Drawing.Point(4, 23);
            this.tpTextServices.Name = "tpTextServices";
            this.tpTextServices.Padding = new System.Windows.Forms.Padding(3);
            this.tpTextServices.Size = new System.Drawing.Size(805, 442);
            this.tpTextServices.TabIndex = 13;
            this.tpTextServices.Text = "Text Services";
            this.tpTextServices.UseVisualStyleBackColor = true;
            // 
            // tcTextUploaders
            // 
            this.tcTextUploaders.Controls.Add(this.tpTextUploaders);
            this.tcTextUploaders.Controls.Add(this.tpURLShorteners);
            this.tcTextUploaders.Controls.Add(this.tpTreeGUI);
            this.tcTextUploaders.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcTextUploaders.Location = new System.Drawing.Point(3, 3);
            this.tcTextUploaders.Name = "tcTextUploaders";
            this.tcTextUploaders.SelectedIndex = 0;
            this.tcTextUploaders.Size = new System.Drawing.Size(799, 436);
            this.tcTextUploaders.TabIndex = 0;
            // 
            // tpTextUploaders
            // 
            this.tpTextUploaders.Controls.Add(this.ucTextUploaders);
            this.tpTextUploaders.Location = new System.Drawing.Point(4, 22);
            this.tpTextUploaders.Name = "tpTextUploaders";
            this.tpTextUploaders.Padding = new System.Windows.Forms.Padding(3);
            this.tpTextUploaders.Size = new System.Drawing.Size(791, 410);
            this.tpTextUploaders.TabIndex = 14;
            this.tpTextUploaders.Text = "Text Uploaders";
            this.tpTextUploaders.UseVisualStyleBackColor = true;
            // 
            // ucTextUploaders
            // 
            this.ucTextUploaders.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucTextUploaders.Location = new System.Drawing.Point(3, 3);
            this.ucTextUploaders.Name = "ucTextUploaders";
            this.ucTextUploaders.Size = new System.Drawing.Size(785, 404);
            this.ucTextUploaders.TabIndex = 0;
            // 
            // tpURLShorteners
            // 
            this.tpURLShorteners.Controls.Add(this.ucUrlShorteners);
            this.tpURLShorteners.Location = new System.Drawing.Point(4, 22);
            this.tpURLShorteners.Name = "tpURLShorteners";
            this.tpURLShorteners.Padding = new System.Windows.Forms.Padding(3);
            this.tpURLShorteners.Size = new System.Drawing.Size(791, 410);
            this.tpURLShorteners.TabIndex = 13;
            this.tpURLShorteners.Text = "URL Shorteners";
            this.tpURLShorteners.UseVisualStyleBackColor = true;
            // 
            // ucUrlShorteners
            // 
            this.ucUrlShorteners.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucUrlShorteners.Location = new System.Drawing.Point(3, 3);
            this.ucUrlShorteners.Name = "ucUrlShorteners";
            this.ucUrlShorteners.Size = new System.Drawing.Size(785, 404);
            this.ucUrlShorteners.TabIndex = 0;
            // 
            // tpTreeGUI
            // 
            this.tpTreeGUI.Controls.Add(this.pgIndexer);
            this.tpTreeGUI.Location = new System.Drawing.Point(4, 22);
            this.tpTreeGUI.Name = "tpTreeGUI";
            this.tpTreeGUI.Padding = new System.Windows.Forms.Padding(3);
            this.tpTreeGUI.Size = new System.Drawing.Size(791, 410);
            this.tpTreeGUI.TabIndex = 15;
            this.tpTreeGUI.Text = "Directory Indexer";
            this.tpTreeGUI.UseVisualStyleBackColor = true;
            // 
            // pgIndexer
            // 
            this.pgIndexer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pgIndexer.Location = new System.Drawing.Point(3, 3);
            this.pgIndexer.Name = "pgIndexer";
            this.pgIndexer.Size = new System.Drawing.Size(785, 404);
            this.pgIndexer.TabIndex = 0;
            // 
            // tpTranslator
            // 
            this.tpTranslator.Controls.Add(this.txtAutoTranslate);
            this.tpTranslator.Controls.Add(this.cbAutoTranslate);
            this.tpTranslator.Controls.Add(this.btnTranslateTo1);
            this.tpTranslator.Controls.Add(this.lblDictionary);
            this.tpTranslator.Controls.Add(this.txtDictionary);
            this.tpTranslator.Controls.Add(this.cbClipboardTranslate);
            this.tpTranslator.Controls.Add(this.txtTranslateResult);
            this.tpTranslator.Controls.Add(this.txtLanguages);
            this.tpTranslator.Controls.Add(this.btnTranslate);
            this.tpTranslator.Controls.Add(this.txtTranslateText);
            this.tpTranslator.Controls.Add(this.lblToLanguage);
            this.tpTranslator.Controls.Add(this.lblFromLanguage);
            this.tpTranslator.Controls.Add(this.cbToLanguage);
            this.tpTranslator.Controls.Add(this.cbFromLanguage);
            this.tpTranslator.ImageKey = "comments.png";
            this.tpTranslator.Location = new System.Drawing.Point(4, 23);
            this.tpTranslator.Name = "tpTranslator";
            this.tpTranslator.Padding = new System.Windows.Forms.Padding(3);
            this.tpTranslator.Size = new System.Drawing.Size(805, 442);
            this.tpTranslator.TabIndex = 1;
            this.tpTranslator.Text = "Translator";
            this.tpTranslator.UseVisualStyleBackColor = true;
            // 
            // txtAutoTranslate
            // 
            this.txtAutoTranslate.Location = new System.Drawing.Point(440, 392);
            this.txtAutoTranslate.Name = "txtAutoTranslate";
            this.txtAutoTranslate.Size = new System.Drawing.Size(56, 20);
            this.txtAutoTranslate.TabIndex = 11;
            this.txtAutoTranslate.TextChanged += new System.EventHandler(this.txtAutoTranslate_TextChanged);
            // 
            // cbAutoTranslate
            // 
            this.cbAutoTranslate.AutoSize = true;
            this.cbAutoTranslate.Location = new System.Drawing.Point(16, 394);
            this.cbAutoTranslate.Name = "cbAutoTranslate";
            this.cbAutoTranslate.Size = new System.Drawing.Size(418, 17);
            this.cbAutoTranslate.TabIndex = 10;
            this.cbAutoTranslate.Text = "If clipboard text length is smaller than this number then instead text upload, tr" +
                "anslate";
            this.ttZScreen.SetToolTip(this.cbAutoTranslate, "Maximum number of characters before Clipboard Upload switches from Translate to T" +
                    "ext Upload.");
            this.cbAutoTranslate.UseVisualStyleBackColor = true;
            this.cbAutoTranslate.CheckedChanged += new System.EventHandler(this.cbAutoTranslate_CheckedChanged);
            // 
            // btnTranslateTo1
            // 
            this.btnTranslateTo1.AllowDrop = true;
            this.btnTranslateTo1.Location = new System.Drawing.Point(216, 176);
            this.btnTranslateTo1.Name = "btnTranslateTo1";
            this.btnTranslateTo1.Size = new System.Drawing.Size(136, 24);
            this.btnTranslateTo1.TabIndex = 9;
            this.btnTranslateTo1.Text = "???";
            this.btnTranslateTo1.UseVisualStyleBackColor = true;
            this.btnTranslateTo1.Click += new System.EventHandler(this.btnTranslateTo1_Click);
            this.btnTranslateTo1.DragDrop += new System.Windows.Forms.DragEventHandler(this.btnTranslateTo1_DragDrop);
            this.btnTranslateTo1.DragEnter += new System.Windows.Forms.DragEventHandler(this.btnTranslateTo1_DragEnter);
            // 
            // lblDictionary
            // 
            this.lblDictionary.AutoSize = true;
            this.lblDictionary.Location = new System.Drawing.Point(368, 24);
            this.lblDictionary.Name = "lblDictionary";
            this.lblDictionary.Size = new System.Drawing.Size(54, 13);
            this.lblDictionary.TabIndex = 8;
            this.lblDictionary.Text = "Dictionary";
            // 
            // txtDictionary
            // 
            this.txtDictionary.BackColor = System.Drawing.SystemColors.Info;
            this.txtDictionary.Location = new System.Drawing.Point(368, 48);
            this.txtDictionary.Multiline = true;
            this.txtDictionary.Name = "txtDictionary";
            this.txtDictionary.ReadOnly = true;
            this.txtDictionary.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtDictionary.Size = new System.Drawing.Size(392, 304);
            this.txtDictionary.TabIndex = 7;
            // 
            // cbClipboardTranslate
            // 
            this.cbClipboardTranslate.AutoSize = true;
            this.cbClipboardTranslate.Location = new System.Drawing.Point(16, 370);
            this.cbClipboardTranslate.Name = "cbClipboardTranslate";
            this.cbClipboardTranslate.Size = new System.Drawing.Size(230, 17);
            this.cbClipboardTranslate.TabIndex = 6;
            this.cbClipboardTranslate.Text = "Auto paste result to clipboard after translate";
            this.cbClipboardTranslate.UseVisualStyleBackColor = true;
            this.cbClipboardTranslate.CheckedChanged += new System.EventHandler(this.cbClipboardTranslate_CheckedChanged);
            // 
            // txtTranslateResult
            // 
            this.txtTranslateResult.Location = new System.Drawing.Point(16, 232);
            this.txtTranslateResult.Multiline = true;
            this.txtTranslateResult.Name = "txtTranslateResult";
            this.txtTranslateResult.ReadOnly = true;
            this.txtTranslateResult.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtTranslateResult.Size = new System.Drawing.Size(336, 120);
            this.txtTranslateResult.TabIndex = 5;
            // 
            // txtLanguages
            // 
            this.txtLanguages.Location = new System.Drawing.Point(16, 208);
            this.txtLanguages.Name = "txtLanguages";
            this.txtLanguages.ReadOnly = true;
            this.txtLanguages.Size = new System.Drawing.Size(336, 20);
            this.txtLanguages.TabIndex = 4;
            // 
            // btnTranslate
            // 
            this.btnTranslate.Location = new System.Drawing.Point(16, 176);
            this.btnTranslate.Name = "btnTranslate";
            this.btnTranslate.Size = new System.Drawing.Size(192, 24);
            this.btnTranslate.TabIndex = 3;
            this.btnTranslate.Text = "Translate ( Ctrl + Enter )";
            this.btnTranslate.UseVisualStyleBackColor = true;
            this.btnTranslate.Click += new System.EventHandler(this.btnTranslate_Click);
            // 
            // txtTranslateText
            // 
            this.txtTranslateText.Location = new System.Drawing.Point(16, 48);
            this.txtTranslateText.Multiline = true;
            this.txtTranslateText.Name = "txtTranslateText";
            this.txtTranslateText.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtTranslateText.Size = new System.Drawing.Size(336, 120);
            this.txtTranslateText.TabIndex = 2;
            this.txtTranslateText.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtTranslateText_KeyDown);
            // 
            // lblToLanguage
            // 
            this.lblToLanguage.Cursor = System.Windows.Forms.Cursors.SizeAll;
            this.lblToLanguage.Location = new System.Drawing.Point(192, 10);
            this.lblToLanguage.Name = "lblToLanguage";
            this.lblToLanguage.Size = new System.Drawing.Size(28, 32);
            this.lblToLanguage.TabIndex = 3;
            this.lblToLanguage.Text = "To:";
            this.lblToLanguage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblToLanguage.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lblToLanguage_MouseDown);
            // 
            // lblFromLanguage
            // 
            this.lblFromLanguage.Location = new System.Drawing.Point(16, 10);
            this.lblFromLanguage.Name = "lblFromLanguage";
            this.lblFromLanguage.Size = new System.Drawing.Size(33, 32);
            this.lblFromLanguage.TabIndex = 2;
            this.lblFromLanguage.Text = "From:";
            this.lblFromLanguage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cbToLanguage
            // 
            this.cbToLanguage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbToLanguage.Enabled = false;
            this.cbToLanguage.FormattingEnabled = true;
            this.cbToLanguage.Location = new System.Drawing.Point(224, 16);
            this.cbToLanguage.MaxDropDownItems = 20;
            this.cbToLanguage.Name = "cbToLanguage";
            this.cbToLanguage.Size = new System.Drawing.Size(128, 21);
            this.cbToLanguage.TabIndex = 1;
            this.cbToLanguage.SelectedIndexChanged += new System.EventHandler(this.cbToLanguage_SelectedIndexChanged);
            // 
            // cbFromLanguage
            // 
            this.cbFromLanguage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbFromLanguage.Enabled = false;
            this.cbFromLanguage.FormattingEnabled = true;
            this.cbFromLanguage.Location = new System.Drawing.Point(56, 16);
            this.cbFromLanguage.MaxDropDownItems = 20;
            this.cbFromLanguage.Name = "cbFromLanguage";
            this.cbFromLanguage.Size = new System.Drawing.Size(128, 21);
            this.cbFromLanguage.TabIndex = 0;
            this.cbFromLanguage.SelectedIndexChanged += new System.EventHandler(this.cbFromLanguage_SelectedIndexChanged);
            // 
            // tpHistory
            // 
            this.tpHistory.Controls.Add(this.tcHistory);
            this.tpHistory.ImageKey = "pictures.png";
            this.tpHistory.Location = new System.Drawing.Point(4, 23);
            this.tpHistory.Name = "tpHistory";
            this.tpHistory.Padding = new System.Windows.Forms.Padding(3);
            this.tpHistory.Size = new System.Drawing.Size(805, 442);
            this.tpHistory.TabIndex = 8;
            this.tpHistory.Text = "History";
            this.tpHistory.UseVisualStyleBackColor = true;
            // 
            // tcHistory
            // 
            this.tcHistory.Controls.Add(this.tpHistoryList);
            this.tcHistory.Controls.Add(this.tpHistorySettings);
            this.tcHistory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcHistory.Location = new System.Drawing.Point(3, 3);
            this.tcHistory.Name = "tcHistory";
            this.tcHistory.SelectedIndex = 0;
            this.tcHistory.Size = new System.Drawing.Size(799, 436);
            this.tcHistory.TabIndex = 3;
            // 
            // tpHistoryList
            // 
            this.tpHistoryList.Controls.Add(this.tlpHistory);
            this.tpHistoryList.Location = new System.Drawing.Point(4, 22);
            this.tpHistoryList.Name = "tpHistoryList";
            this.tpHistoryList.Padding = new System.Windows.Forms.Padding(3);
            this.tpHistoryList.Size = new System.Drawing.Size(791, 410);
            this.tpHistoryList.TabIndex = 0;
            this.tpHistoryList.Text = "History List";
            this.tpHistoryList.UseVisualStyleBackColor = true;
            // 
            // tlpHistory
            // 
            this.tlpHistory.ColumnCount = 2;
            this.tlpHistory.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tlpHistory.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 60F));
            this.tlpHistory.Controls.Add(this.tlpHistoryControls, 1, 0);
            this.tlpHistory.Controls.Add(this.lbHistory, 0, 0);
            this.tlpHistory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpHistory.Location = new System.Drawing.Point(3, 3);
            this.tlpHistory.Name = "tlpHistory";
            this.tlpHistory.RowCount = 1;
            this.tlpHistory.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpHistory.Size = new System.Drawing.Size(785, 404);
            this.tlpHistory.TabIndex = 17;
            // 
            // tlpHistoryControls
            // 
            this.tlpHistoryControls.ColumnCount = 1;
            this.tlpHistoryControls.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpHistoryControls.Controls.Add(this.lblHistoryScreenshot, 0, 0);
            this.tlpHistoryControls.Controls.Add(this.panelControls, 0, 2);
            this.tlpHistoryControls.Controls.Add(this.panelPreview, 0, 1);
            this.tlpHistoryControls.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpHistoryControls.Location = new System.Drawing.Point(317, 3);
            this.tlpHistoryControls.Name = "tlpHistoryControls";
            this.tlpHistoryControls.RowCount = 3;
            this.tlpHistoryControls.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpHistoryControls.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpHistoryControls.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 130F));
            this.tlpHistoryControls.Size = new System.Drawing.Size(465, 398);
            this.tlpHistoryControls.TabIndex = 0;
            // 
            // lblHistoryScreenshot
            // 
            this.lblHistoryScreenshot.AutoSize = true;
            this.lblHistoryScreenshot.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblHistoryScreenshot.Location = new System.Drawing.Point(3, 0);
            this.lblHistoryScreenshot.Name = "lblHistoryScreenshot";
            this.lblHistoryScreenshot.Size = new System.Drawing.Size(459, 20);
            this.lblHistoryScreenshot.TabIndex = 13;
            this.lblHistoryScreenshot.Text = "Screenshot";
            // 
            // panelControls
            // 
            this.panelControls.Controls.Add(this.btnHistoryOpenLocalFile);
            this.panelControls.Controls.Add(this.txtHistoryLocalPath);
            this.panelControls.Controls.Add(this.btnHistoryCopyLink);
            this.panelControls.Controls.Add(this.lblHistoryRemotePath);
            this.panelControls.Controls.Add(this.btnHistoryCopyImage);
            this.panelControls.Controls.Add(this.txtHistoryRemotePath);
            this.panelControls.Controls.Add(this.btnHistoryBrowseURL);
            this.panelControls.Controls.Add(this.lblHistoryLocalPath);
            this.panelControls.Location = new System.Drawing.Point(3, 271);
            this.panelControls.Name = "panelControls";
            this.panelControls.Size = new System.Drawing.Size(455, 123);
            this.panelControls.TabIndex = 15;
            // 
            // btnHistoryOpenLocalFile
            // 
            this.btnHistoryOpenLocalFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnHistoryOpenLocalFile.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnHistoryOpenLocalFile.Enabled = false;
            this.btnHistoryOpenLocalFile.Location = new System.Drawing.Point(312, 8);
            this.btnHistoryOpenLocalFile.Name = "btnHistoryOpenLocalFile";
            this.btnHistoryOpenLocalFile.Size = new System.Drawing.Size(96, 24);
            this.btnHistoryOpenLocalFile.TabIndex = 9;
            this.btnHistoryOpenLocalFile.Text = "&Open Local File";
            this.btnHistoryOpenLocalFile.UseVisualStyleBackColor = true;
            this.btnHistoryOpenLocalFile.Click += new System.EventHandler(this.btnScreenshotOpen_Click);
            // 
            // txtHistoryLocalPath
            // 
            this.txtHistoryLocalPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtHistoryLocalPath.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtHistoryLocalPath.Location = new System.Drawing.Point(8, 56);
            this.txtHistoryLocalPath.Name = "txtHistoryLocalPath";
            this.txtHistoryLocalPath.ReadOnly = true;
            this.txtHistoryLocalPath.Size = new System.Drawing.Size(440, 20);
            this.txtHistoryLocalPath.TabIndex = 7;
            // 
            // btnHistoryCopyLink
            // 
            this.btnHistoryCopyLink.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnHistoryCopyLink.Enabled = false;
            this.btnHistoryCopyLink.Location = new System.Drawing.Point(48, 8);
            this.btnHistoryCopyLink.Name = "btnHistoryCopyLink";
            this.btnHistoryCopyLink.Size = new System.Drawing.Size(80, 24);
            this.btnHistoryCopyLink.TabIndex = 12;
            this.btnHistoryCopyLink.Text = "Copy Link";
            this.btnHistoryCopyLink.UseVisualStyleBackColor = true;
            this.btnHistoryCopyLink.Click += new System.EventHandler(this.btnCopyLink_Click);
            // 
            // lblHistoryRemotePath
            // 
            this.lblHistoryRemotePath.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblHistoryRemotePath.AutoSize = true;
            this.lblHistoryRemotePath.Location = new System.Drawing.Point(8, 79);
            this.lblHistoryRemotePath.Name = "lblHistoryRemotePath";
            this.lblHistoryRemotePath.Size = new System.Drawing.Size(69, 13);
            this.lblHistoryRemotePath.TabIndex = 6;
            this.lblHistoryRemotePath.Text = "Remote Path";
            // 
            // btnHistoryCopyImage
            // 
            this.btnHistoryCopyImage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnHistoryCopyImage.Enabled = false;
            this.btnHistoryCopyImage.Location = new System.Drawing.Point(136, 8);
            this.btnHistoryCopyImage.Name = "btnHistoryCopyImage";
            this.btnHistoryCopyImage.Size = new System.Drawing.Size(80, 24);
            this.btnHistoryCopyImage.TabIndex = 11;
            this.btnHistoryCopyImage.Text = "Copy &Image";
            this.btnHistoryCopyImage.UseVisualStyleBackColor = true;
            this.btnHistoryCopyImage.Click += new System.EventHandler(this.btnImageCopy_Click);
            // 
            // txtHistoryRemotePath
            // 
            this.txtHistoryRemotePath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtHistoryRemotePath.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtHistoryRemotePath.Location = new System.Drawing.Point(8, 96);
            this.txtHistoryRemotePath.Name = "txtHistoryRemotePath";
            this.txtHistoryRemotePath.ReadOnly = true;
            this.txtHistoryRemotePath.Size = new System.Drawing.Size(440, 20);
            this.txtHistoryRemotePath.TabIndex = 8;
            // 
            // btnHistoryBrowseURL
            // 
            this.btnHistoryBrowseURL.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnHistoryBrowseURL.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnHistoryBrowseURL.Enabled = false;
            this.btnHistoryBrowseURL.Location = new System.Drawing.Point(224, 8);
            this.btnHistoryBrowseURL.Name = "btnHistoryBrowseURL";
            this.btnHistoryBrowseURL.Size = new System.Drawing.Size(80, 24);
            this.btnHistoryBrowseURL.TabIndex = 10;
            this.btnHistoryBrowseURL.Text = "Browse &URL";
            this.btnHistoryBrowseURL.UseVisualStyleBackColor = true;
            this.btnHistoryBrowseURL.Click += new System.EventHandler(this.btnScreenshotBrowse_Click);
            // 
            // lblHistoryLocalPath
            // 
            this.lblHistoryLocalPath.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblHistoryLocalPath.AutoSize = true;
            this.lblHistoryLocalPath.Location = new System.Drawing.Point(8, 39);
            this.lblHistoryLocalPath.Name = "lblHistoryLocalPath";
            this.lblHistoryLocalPath.Size = new System.Drawing.Size(58, 13);
            this.lblHistoryLocalPath.TabIndex = 5;
            this.lblHistoryLocalPath.Text = "Local Path";
            // 
            // panelPreview
            // 
            this.panelPreview.Controls.Add(this.pbPreview);
            this.panelPreview.Controls.Add(this.txtPreview);
            this.panelPreview.Controls.Add(this.historyBrowser);
            this.panelPreview.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelPreview.Location = new System.Drawing.Point(3, 23);
            this.panelPreview.Name = "panelPreview";
            this.panelPreview.Size = new System.Drawing.Size(459, 242);
            this.panelPreview.TabIndex = 16;
            // 
            // pbPreview
            // 
            this.pbPreview.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbPreview.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pbPreview.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pbPreview.InitialImage = ((System.Drawing.Image)(resources.GetObject("pbPreview.InitialImage")));
            this.pbPreview.Location = new System.Drawing.Point(0, 0);
            this.pbPreview.Name = "pbPreview";
            this.pbPreview.Size = new System.Drawing.Size(459, 242);
            this.pbPreview.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbPreview.TabIndex = 4;
            this.pbPreview.TabStop = false;
            this.pbPreview.Click += new System.EventHandler(this.pbHistoryThumb_Click);
            // 
            // txtPreview
            // 
            this.txtPreview.BackColor = System.Drawing.SystemColors.Info;
            this.txtPreview.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtPreview.Location = new System.Drawing.Point(0, 0);
            this.txtPreview.Multiline = true;
            this.txtPreview.Name = "txtPreview";
            this.txtPreview.ReadOnly = true;
            this.txtPreview.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtPreview.Size = new System.Drawing.Size(459, 242);
            this.txtPreview.TabIndex = 14;
            // 
            // historyBrowser
            // 
            this.historyBrowser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.historyBrowser.Location = new System.Drawing.Point(0, 0);
            this.historyBrowser.MinimumSize = new System.Drawing.Size(20, 20);
            this.historyBrowser.Name = "historyBrowser";
            this.historyBrowser.Size = new System.Drawing.Size(459, 242);
            this.historyBrowser.TabIndex = 15;
            // 
            // lbHistory
            // 
            this.lbHistory.ContextMenuStrip = this.cmsHistory;
            this.lbHistory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbHistory.FormattingEnabled = true;
            this.lbHistory.HorizontalScrollbar = true;
            this.lbHistory.IntegralHeight = false;
            this.lbHistory.Location = new System.Drawing.Point(3, 3);
            this.lbHistory.Name = "lbHistory";
            this.lbHistory.ScrollAlwaysVisible = true;
            this.lbHistory.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lbHistory.Size = new System.Drawing.Size(308, 398);
            this.lbHistory.TabIndex = 2;
            this.lbHistory.SelectedIndexChanged += new System.EventHandler(this.lbHistory_SelectedIndexChanged);
            this.lbHistory.DoubleClick += new System.EventHandler(this.lbHistory_DoubleClick);
            this.lbHistory.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lbHistory_KeyDown);
            // 
            // tpHistorySettings
            // 
            this.tpHistorySettings.Controls.Add(this.cbHistorySave);
            this.tpHistorySettings.Controls.Add(this.cbShowHistoryTooltip);
            this.tpHistorySettings.Controls.Add(this.btnHistoryClear);
            this.tpHistorySettings.Controls.Add(this.cbHistoryListFormat);
            this.tpHistorySettings.Controls.Add(this.lblHistoryMaxItems);
            this.tpHistorySettings.Controls.Add(this.lblHistoryListFormat);
            this.tpHistorySettings.Controls.Add(this.nudHistoryMaxItems);
            this.tpHistorySettings.Controls.Add(this.cbHistoryAddSpace);
            this.tpHistorySettings.Controls.Add(this.cbHistoryReverseList);
            this.tpHistorySettings.Location = new System.Drawing.Point(4, 22);
            this.tpHistorySettings.Name = "tpHistorySettings";
            this.tpHistorySettings.Padding = new System.Windows.Forms.Padding(3);
            this.tpHistorySettings.Size = new System.Drawing.Size(791, 410);
            this.tpHistorySettings.TabIndex = 1;
            this.tpHistorySettings.Text = "History Settings";
            this.tpHistorySettings.UseVisualStyleBackColor = true;
            // 
            // cbHistorySave
            // 
            this.cbHistorySave.AutoSize = true;
            this.cbHistorySave.Location = new System.Drawing.Point(16, 72);
            this.cbHistorySave.Name = "cbHistorySave";
            this.cbHistorySave.Size = new System.Drawing.Size(173, 17);
            this.cbHistorySave.TabIndex = 10;
            this.cbHistorySave.Text = "Save History List to an XML file";
            this.cbHistorySave.UseVisualStyleBackColor = true;
            this.cbHistorySave.CheckedChanged += new System.EventHandler(this.cbHistorySave_CheckedChanged);
            // 
            // cbShowHistoryTooltip
            // 
            this.cbShowHistoryTooltip.AutoSize = true;
            this.cbShowHistoryTooltip.Location = new System.Drawing.Point(16, 96);
            this.cbShowHistoryTooltip.Name = "cbShowHistoryTooltip";
            this.cbShowHistoryTooltip.Size = new System.Drawing.Size(211, 17);
            this.cbShowHistoryTooltip.TabIndex = 9;
            this.cbShowHistoryTooltip.Text = "Show Screenshot Information in Tooltip";
            this.cbShowHistoryTooltip.UseVisualStyleBackColor = true;
            this.cbShowHistoryTooltip.CheckedChanged += new System.EventHandler(this.cbShowHistoryTooltip_CheckedChanged);
            // 
            // btnHistoryClear
            // 
            this.btnHistoryClear.AutoSize = true;
            this.btnHistoryClear.Location = new System.Drawing.Point(16, 168);
            this.btnHistoryClear.Name = "btnHistoryClear";
            this.btnHistoryClear.Size = new System.Drawing.Size(112, 24);
            this.btnHistoryClear.TabIndex = 6;
            this.btnHistoryClear.Text = "Clear History List...";
            this.btnHistoryClear.UseVisualStyleBackColor = true;
            this.btnHistoryClear.Click += new System.EventHandler(this.btnHistoryClear_Click);
            // 
            // cbHistoryListFormat
            // 
            this.cbHistoryListFormat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbHistoryListFormat.FormattingEnabled = true;
            this.cbHistoryListFormat.Location = new System.Drawing.Point(112, 16);
            this.cbHistoryListFormat.Name = "cbHistoryListFormat";
            this.cbHistoryListFormat.Size = new System.Drawing.Size(176, 21);
            this.cbHistoryListFormat.TabIndex = 7;
            this.cbHistoryListFormat.SelectedIndexChanged += new System.EventHandler(this.cbHistoryListFormat_SelectedIndexChanged);
            // 
            // lblHistoryMaxItems
            // 
            this.lblHistoryMaxItems.AutoSize = true;
            this.lblHistoryMaxItems.Location = new System.Drawing.Point(16, 48);
            this.lblHistoryMaxItems.Name = "lblHistoryMaxItems";
            this.lblHistoryMaxItems.Size = new System.Drawing.Size(208, 13);
            this.lblHistoryMaxItems.TabIndex = 5;
            this.lblHistoryMaxItems.Text = "Maximum number of screenshots in history:";
            // 
            // lblHistoryListFormat
            // 
            this.lblHistoryListFormat.AutoSize = true;
            this.lblHistoryListFormat.Location = new System.Drawing.Point(16, 20);
            this.lblHistoryListFormat.Name = "lblHistoryListFormat";
            this.lblHistoryListFormat.Size = new System.Drawing.Size(89, 13);
            this.lblHistoryListFormat.TabIndex = 8;
            this.lblHistoryListFormat.Text = "History list format:";
            // 
            // nudHistoryMaxItems
            // 
            this.nudHistoryMaxItems.Location = new System.Drawing.Point(232, 44);
            this.nudHistoryMaxItems.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nudHistoryMaxItems.Name = "nudHistoryMaxItems";
            this.nudHistoryMaxItems.Size = new System.Drawing.Size(72, 20);
            this.nudHistoryMaxItems.TabIndex = 4;
            this.nudHistoryMaxItems.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.nudHistoryMaxItems.ValueChanged += new System.EventHandler(this.nudHistoryMaxItems_ValueChanged);
            // 
            // cbHistoryAddSpace
            // 
            this.cbHistoryAddSpace.AutoSize = true;
            this.cbHistoryAddSpace.BackColor = System.Drawing.Color.Transparent;
            this.cbHistoryAddSpace.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.cbHistoryAddSpace.Location = new System.Drawing.Point(16, 144);
            this.cbHistoryAddSpace.Name = "cbHistoryAddSpace";
            this.cbHistoryAddSpace.Size = new System.Drawing.Size(234, 17);
            this.cbHistoryAddSpace.TabIndex = 0;
            this.cbHistoryAddSpace.Tag = "Adding a New Line before the URLs makes it look nicer when you copy a URL List in" +
                " IM such as Pidgin";
            this.cbHistoryAddSpace.Text = "Add a new line before the URLs in clipboard";
            this.cbHistoryAddSpace.UseVisualStyleBackColor = false;
            this.cbHistoryAddSpace.CheckedChanged += new System.EventHandler(this.cbAddSpace_CheckedChanged);
            // 
            // cbHistoryReverseList
            // 
            this.cbHistoryReverseList.AutoSize = true;
            this.cbHistoryReverseList.BackColor = System.Drawing.Color.Transparent;
            this.cbHistoryReverseList.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.cbHistoryReverseList.Location = new System.Drawing.Point(16, 120);
            this.cbHistoryReverseList.Name = "cbHistoryReverseList";
            this.cbHistoryReverseList.Size = new System.Drawing.Size(143, 17);
            this.cbHistoryReverseList.TabIndex = 1;
            this.cbHistoryReverseList.Text = "Reverse List in Clipboard";
            this.cbHistoryReverseList.UseVisualStyleBackColor = false;
            this.cbHistoryReverseList.CheckedChanged += new System.EventHandler(this.cbReverse_CheckedChanged);
            // 
            // tpOptions
            // 
            this.tpOptions.Controls.Add(this.tcOptions);
            this.tpOptions.ImageKey = "application_edit.png";
            this.tpOptions.Location = new System.Drawing.Point(4, 23);
            this.tpOptions.Name = "tpOptions";
            this.tpOptions.Padding = new System.Windows.Forms.Padding(3);
            this.tpOptions.Size = new System.Drawing.Size(805, 442);
            this.tpOptions.TabIndex = 9;
            this.tpOptions.Text = "Options";
            this.tpOptions.UseVisualStyleBackColor = true;
            // 
            // tcOptions
            // 
            this.tcOptions.Controls.Add(this.tpGeneral);
            this.tcOptions.Controls.Add(this.tpProxy);
            this.tcOptions.Controls.Add(this.tpInteraction);
            this.tcOptions.Controls.Add(this.tpAdvPaths);
            this.tcOptions.Controls.Add(this.tpAdvDebug);
            this.tcOptions.Controls.Add(this.tpOptionsAdv);
            this.tcOptions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcOptions.Location = new System.Drawing.Point(3, 3);
            this.tcOptions.Name = "tcOptions";
            this.tcOptions.SelectedIndex = 0;
            this.tcOptions.Size = new System.Drawing.Size(799, 436);
            this.tcOptions.TabIndex = 8;
            // 
            // tpGeneral
            // 
            this.tpGeneral.Controls.Add(this.gbUpdates);
            this.tpGeneral.Controls.Add(this.gbMisc);
            this.tpGeneral.Location = new System.Drawing.Point(4, 22);
            this.tpGeneral.Name = "tpGeneral";
            this.tpGeneral.Padding = new System.Windows.Forms.Padding(3);
            this.tpGeneral.Size = new System.Drawing.Size(791, 410);
            this.tpGeneral.TabIndex = 0;
            this.tpGeneral.Text = "General";
            this.tpGeneral.UseVisualStyleBackColor = true;
            // 
            // gbUpdates
            // 
            this.gbUpdates.Controls.Add(this.cbCheckUpdatesBeta);
            this.gbUpdates.Controls.Add(this.lblUpdateInfo);
            this.gbUpdates.Controls.Add(this.btnCheckUpdate);
            this.gbUpdates.Controls.Add(this.cbCheckUpdates);
            this.gbUpdates.Location = new System.Drawing.Point(8, 248);
            this.gbUpdates.Name = "gbUpdates";
            this.gbUpdates.Size = new System.Drawing.Size(752, 96);
            this.gbUpdates.TabIndex = 8;
            this.gbUpdates.TabStop = false;
            this.gbUpdates.Text = "Check Updates";
            // 
            // cbCheckUpdatesBeta
            // 
            this.cbCheckUpdatesBeta.AutoSize = true;
            this.cbCheckUpdatesBeta.Location = new System.Drawing.Point(192, 24);
            this.cbCheckUpdatesBeta.Name = "cbCheckUpdatesBeta";
            this.cbCheckUpdatesBeta.Size = new System.Drawing.Size(126, 17);
            this.cbCheckUpdatesBeta.TabIndex = 7;
            this.cbCheckUpdatesBeta.Text = "Include beta updates";
            this.cbCheckUpdatesBeta.UseVisualStyleBackColor = true;
            this.cbCheckUpdatesBeta.CheckedChanged += new System.EventHandler(this.cbCheckUpdatesBeta_CheckedChanged);
            // 
            // lblUpdateInfo
            // 
            this.lblUpdateInfo.AutoSize = true;
            this.lblUpdateInfo.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUpdateInfo.Location = new System.Drawing.Point(552, 24);
            this.lblUpdateInfo.Name = "lblUpdateInfo";
            this.lblUpdateInfo.Size = new System.Drawing.Size(131, 11);
            this.lblUpdateInfo.TabIndex = 6;
            this.lblUpdateInfo.Text = "Update information";
            // 
            // btnCheckUpdate
            // 
            this.btnCheckUpdate.Location = new System.Drawing.Point(16, 56);
            this.btnCheckUpdate.Name = "btnCheckUpdate";
            this.btnCheckUpdate.Size = new System.Drawing.Size(104, 24);
            this.btnCheckUpdate.TabIndex = 5;
            this.btnCheckUpdate.Text = "Check Update";
            this.btnCheckUpdate.UseVisualStyleBackColor = true;
            this.btnCheckUpdate.Click += new System.EventHandler(this.btnCheckUpdate_Click);
            // 
            // cbCheckUpdates
            // 
            this.cbCheckUpdates.AutoSize = true;
            this.cbCheckUpdates.Location = new System.Drawing.Point(16, 24);
            this.cbCheckUpdates.Name = "cbCheckUpdates";
            this.cbCheckUpdates.Size = new System.Drawing.Size(162, 17);
            this.cbCheckUpdates.TabIndex = 1;
            this.cbCheckUpdates.Text = "Automatically check updates";
            this.cbCheckUpdates.UseVisualStyleBackColor = true;
            this.cbCheckUpdates.CheckedChanged += new System.EventHandler(this.cbCheckUpdates_CheckedChanged);
            // 
            // gbMisc
            // 
            this.gbMisc.BackColor = System.Drawing.Color.Transparent;
            this.gbMisc.Controls.Add(this.chkWindows7TaskbarIntegration);
            this.gbMisc.Controls.Add(this.cbAutoSaveSettings);
            this.gbMisc.Controls.Add(this.cbSaveFormSizePosition);
            this.gbMisc.Controls.Add(this.cbShowHelpBalloonTips);
            this.gbMisc.Controls.Add(this.cbLockFormSize);
            this.gbMisc.Controls.Add(this.chkShowTaskbar);
            this.gbMisc.Controls.Add(this.chkOpenMainWindow);
            this.gbMisc.Controls.Add(this.chkStartWin);
            this.gbMisc.Location = new System.Drawing.Point(8, 8);
            this.gbMisc.Name = "gbMisc";
            this.gbMisc.Size = new System.Drawing.Size(752, 224);
            this.gbMisc.TabIndex = 7;
            this.gbMisc.TabStop = false;
            this.gbMisc.Text = "Program";
            // 
            // chkWindows7TaskbarIntegration
            // 
            this.chkWindows7TaskbarIntegration.AutoSize = true;
            this.chkWindows7TaskbarIntegration.Location = new System.Drawing.Point(16, 192);
            this.chkWindows7TaskbarIntegration.Name = "chkWindows7TaskbarIntegration";
            this.chkWindows7TaskbarIntegration.Size = new System.Drawing.Size(174, 17);
            this.chkWindows7TaskbarIntegration.TabIndex = 8;
            this.chkWindows7TaskbarIntegration.Text = "Windows 7 &Taskbar Integration";
            this.chkWindows7TaskbarIntegration.UseVisualStyleBackColor = true;
            this.chkWindows7TaskbarIntegration.CheckedChanged += new System.EventHandler(this.chkWindows7TaskbarIntegration_CheckedChanged);
            // 
            // cbAutoSaveSettings
            // 
            this.cbAutoSaveSettings.AutoSize = true;
            this.cbAutoSaveSettings.Location = new System.Drawing.Point(16, 168);
            this.cbAutoSaveSettings.Name = "cbAutoSaveSettings";
            this.cbAutoSaveSettings.Size = new System.Drawing.Size(267, 17);
            this.cbAutoSaveSettings.TabIndex = 7;
            this.cbAutoSaveSettings.Text = "Auto save settings on resize or while switching tabs";
            this.ttZScreen.SetToolTip(this.cbAutoSaveSettings, "ZScreen still saves settings before close");
            this.cbAutoSaveSettings.UseVisualStyleBackColor = true;
            this.cbAutoSaveSettings.CheckedChanged += new System.EventHandler(this.cbAutoSaveSettings_CheckedChanged);
            // 
            // cbSaveFormSizePosition
            // 
            this.cbSaveFormSizePosition.AutoSize = true;
            this.cbSaveFormSizePosition.Location = new System.Drawing.Point(16, 144);
            this.cbSaveFormSizePosition.Name = "cbSaveFormSizePosition";
            this.cbSaveFormSizePosition.Size = new System.Drawing.Size(222, 17);
            this.cbSaveFormSizePosition.TabIndex = 6;
            this.cbSaveFormSizePosition.Text = "Remember main window size and position";
            this.cbSaveFormSizePosition.UseVisualStyleBackColor = true;
            this.cbSaveFormSizePosition.CheckedChanged += new System.EventHandler(this.cbSaveFormSizePosition_CheckedChanged);
            // 
            // cbShowHelpBalloonTips
            // 
            this.cbShowHelpBalloonTips.AutoSize = true;
            this.cbShowHelpBalloonTips.Location = new System.Drawing.Point(16, 96);
            this.cbShowHelpBalloonTips.Name = "cbShowHelpBalloonTips";
            this.cbShowHelpBalloonTips.Size = new System.Drawing.Size(132, 17);
            this.cbShowHelpBalloonTips.TabIndex = 5;
            this.cbShowHelpBalloonTips.Text = "Show help balloon tips";
            this.cbShowHelpBalloonTips.UseVisualStyleBackColor = true;
            this.cbShowHelpBalloonTips.CheckedChanged += new System.EventHandler(this.cbShowHelpBalloonTips_CheckedChanged);
            // 
            // cbLockFormSize
            // 
            this.cbLockFormSize.AutoSize = true;
            this.cbLockFormSize.Location = new System.Drawing.Point(16, 120);
            this.cbLockFormSize.Name = "cbLockFormSize";
            this.cbLockFormSize.Size = new System.Drawing.Size(348, 17);
            this.cbLockFormSize.TabIndex = 4;
            this.cbLockFormSize.Text = "Lock main window size to minimum possible size and not allow resize";
            this.cbLockFormSize.UseVisualStyleBackColor = true;
            this.cbLockFormSize.CheckedChanged += new System.EventHandler(this.cbLockFormSize_CheckedChanged);
            // 
            // chkShowTaskbar
            // 
            this.chkShowTaskbar.AutoSize = true;
            this.chkShowTaskbar.Location = new System.Drawing.Point(16, 72);
            this.chkShowTaskbar.Name = "chkShowTaskbar";
            this.chkShowTaskbar.Size = new System.Drawing.Size(166, 17);
            this.chkShowTaskbar.TabIndex = 3;
            this.chkShowTaskbar.Text = "Show main window in taskbar";
            this.chkShowTaskbar.UseVisualStyleBackColor = true;
            this.chkShowTaskbar.CheckedChanged += new System.EventHandler(this.cbShowTaskbar_CheckedChanged);
            // 
            // chkOpenMainWindow
            // 
            this.chkOpenMainWindow.AutoSize = true;
            this.chkOpenMainWindow.Location = new System.Drawing.Point(16, 48);
            this.chkOpenMainWindow.Name = "chkOpenMainWindow";
            this.chkOpenMainWindow.Size = new System.Drawing.Size(154, 17);
            this.chkOpenMainWindow.TabIndex = 2;
            this.chkOpenMainWindow.Text = "Open main window on load";
            this.chkOpenMainWindow.UseVisualStyleBackColor = true;
            this.chkOpenMainWindow.CheckedChanged += new System.EventHandler(this.cbOpenMainWindow_CheckedChanged);
            // 
            // chkStartWin
            // 
            this.chkStartWin.AutoSize = true;
            this.chkStartWin.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.chkStartWin.Location = new System.Drawing.Point(16, 24);
            this.chkStartWin.Name = "chkStartWin";
            this.chkStartWin.Size = new System.Drawing.Size(117, 17);
            this.chkStartWin.TabIndex = 0;
            this.chkStartWin.Text = "Start with Windows";
            this.chkStartWin.UseVisualStyleBackColor = true;
            this.chkStartWin.CheckedChanged += new System.EventHandler(this.cbStartWin_CheckedChanged);
            // 
            // tpProxy
            // 
            this.tpProxy.Controls.Add(this.gpProxySettings);
            this.tpProxy.Controls.Add(this.ucProxyAccounts);
            this.tpProxy.Location = new System.Drawing.Point(4, 22);
            this.tpProxy.Name = "tpProxy";
            this.tpProxy.Padding = new System.Windows.Forms.Padding(3);
            this.tpProxy.Size = new System.Drawing.Size(791, 410);
            this.tpProxy.TabIndex = 6;
            this.tpProxy.Text = "Proxy";
            this.tpProxy.UseVisualStyleBackColor = true;
            // 
            // gpProxySettings
            // 
            this.gpProxySettings.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gpProxySettings.Controls.Add(this.chkProxyEnable);
            this.gpProxySettings.Location = new System.Drawing.Point(16, 323);
            this.gpProxySettings.Name = "gpProxySettings";
            this.gpProxySettings.Size = new System.Drawing.Size(759, 72);
            this.gpProxySettings.TabIndex = 117;
            this.gpProxySettings.TabStop = false;
            this.gpProxySettings.Text = "Proxy Settings";
            // 
            // chkProxyEnable
            // 
            this.chkProxyEnable.AutoSize = true;
            this.chkProxyEnable.BackColor = System.Drawing.Color.Transparent;
            this.chkProxyEnable.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.chkProxyEnable.Location = new System.Drawing.Point(16, 24);
            this.chkProxyEnable.Name = "chkProxyEnable";
            this.chkProxyEnable.Size = new System.Drawing.Size(88, 17);
            this.chkProxyEnable.TabIndex = 113;
            this.chkProxyEnable.Text = "Enable Proxy";
            this.chkProxyEnable.UseVisualStyleBackColor = false;
            this.chkProxyEnable.CheckedChanged += new System.EventHandler(this.chkProxyEnable_CheckedChanged);
            // 
            // ucProxyAccounts
            // 
            this.ucProxyAccounts.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.ucProxyAccounts.Location = new System.Drawing.Point(3, 3);
            this.ucProxyAccounts.Name = "ucProxyAccounts";
            this.ucProxyAccounts.Size = new System.Drawing.Size(785, 320);
            this.ucProxyAccounts.TabIndex = 0;
            // 
            // tpInteraction
            // 
            this.tpInteraction.Controls.Add(this.gbActionsToolbarSettings);
            this.tpInteraction.Controls.Add(this.gbDropBox);
            this.tpInteraction.Controls.Add(this.gbAppearance);
            this.tpInteraction.Location = new System.Drawing.Point(4, 22);
            this.tpInteraction.Name = "tpInteraction";
            this.tpInteraction.Padding = new System.Windows.Forms.Padding(3);
            this.tpInteraction.Size = new System.Drawing.Size(791, 410);
            this.tpInteraction.TabIndex = 5;
            this.tpInteraction.Text = "Interaction";
            this.tpInteraction.UseVisualStyleBackColor = true;
            // 
            // gbActionsToolbarSettings
            // 
            this.gbActionsToolbarSettings.Controls.Add(this.cbCloseQuickActions);
            this.gbActionsToolbarSettings.Location = new System.Drawing.Point(8, 264);
            this.gbActionsToolbarSettings.Name = "gbActionsToolbarSettings";
            this.gbActionsToolbarSettings.Size = new System.Drawing.Size(752, 56);
            this.gbActionsToolbarSettings.TabIndex = 7;
            this.gbActionsToolbarSettings.TabStop = false;
            this.gbActionsToolbarSettings.Text = "Actions Toolbar Settings";
            // 
            // cbCloseQuickActions
            // 
            this.cbCloseQuickActions.AutoSize = true;
            this.cbCloseQuickActions.Location = new System.Drawing.Point(16, 24);
            this.cbCloseQuickActions.Name = "cbCloseQuickActions";
            this.cbCloseQuickActions.Size = new System.Drawing.Size(214, 17);
            this.cbCloseQuickActions.TabIndex = 0;
            this.cbCloseQuickActions.Text = "Close Actions Toolbar after Mouse Click";
            this.cbCloseQuickActions.UseVisualStyleBackColor = true;
            this.cbCloseQuickActions.CheckedChanged += new System.EventHandler(this.cbCloseQuickActions_CheckedChanged);
            // 
            // gbDropBox
            // 
            this.gbDropBox.Controls.Add(this.cbCloseDropBox);
            this.gbDropBox.Location = new System.Drawing.Point(8, 200);
            this.gbDropBox.Name = "gbDropBox";
            this.gbDropBox.Size = new System.Drawing.Size(752, 56);
            this.gbDropBox.TabIndex = 6;
            this.gbDropBox.TabStop = false;
            this.gbDropBox.Text = "Drop Window Settings";
            // 
            // cbCloseDropBox
            // 
            this.cbCloseDropBox.AutoSize = true;
            this.cbCloseDropBox.Location = new System.Drawing.Point(16, 24);
            this.cbCloseDropBox.Name = "cbCloseDropBox";
            this.cbCloseDropBox.Size = new System.Drawing.Size(205, 17);
            this.cbCloseDropBox.TabIndex = 0;
            this.cbCloseDropBox.Text = "Close Drop Window after Drag n Drop";
            this.cbCloseDropBox.UseVisualStyleBackColor = true;
            this.cbCloseDropBox.CheckedChanged += new System.EventHandler(this.cbCloseDropBox_CheckedChanged);
            // 
            // gbAppearance
            // 
            this.gbAppearance.BackColor = System.Drawing.Color.Transparent;
            this.gbAppearance.Controls.Add(this.cbCompleteSound);
            this.gbAppearance.Controls.Add(this.chkCaptureFallback);
            this.gbAppearance.Controls.Add(this.cbShowUploadDuration);
            this.gbAppearance.Controls.Add(this.chkBalloonTipOpenLink);
            this.gbAppearance.Controls.Add(this.cbShowPopup);
            this.gbAppearance.Controls.Add(this.lblTrayFlash);
            this.gbAppearance.Controls.Add(this.nudFlashIconCount);
            this.gbAppearance.Location = new System.Drawing.Point(8, 8);
            this.gbAppearance.Name = "gbAppearance";
            this.gbAppearance.Size = new System.Drawing.Size(752, 184);
            this.gbAppearance.TabIndex = 5;
            this.gbAppearance.TabStop = false;
            this.gbAppearance.Text = "After completing a task";
            // 
            // cbCompleteSound
            // 
            this.cbCompleteSound.AutoSize = true;
            this.cbCompleteSound.Location = new System.Drawing.Point(16, 152);
            this.cbCompleteSound.Name = "cbCompleteSound";
            this.cbCompleteSound.Size = new System.Drawing.Size(228, 17);
            this.cbCompleteSound.TabIndex = 5;
            this.cbCompleteSound.Text = "Play sound after image reaches destination";
            this.cbCompleteSound.UseVisualStyleBackColor = true;
            this.cbCompleteSound.CheckedChanged += new System.EventHandler(this.cbCompleteSound_CheckedChanged);
            // 
            // chkCaptureFallback
            // 
            this.chkCaptureFallback.AutoSize = true;
            this.chkCaptureFallback.Location = new System.Drawing.Point(16, 56);
            this.chkCaptureFallback.Name = "chkCaptureFallback";
            this.chkCaptureFallback.Size = new System.Drawing.Size(302, 17);
            this.chkCaptureFallback.TabIndex = 7;
            this.chkCaptureFallback.Text = "Capture entire screen if active window capture or crop fails";
            this.chkCaptureFallback.UseVisualStyleBackColor = true;
            this.chkCaptureFallback.CheckedChanged += new System.EventHandler(this.chkCaptureFallback_CheckedChanged);
            // 
            // cbShowUploadDuration
            // 
            this.cbShowUploadDuration.AutoSize = true;
            this.cbShowUploadDuration.Location = new System.Drawing.Point(16, 128);
            this.cbShowUploadDuration.Name = "cbShowUploadDuration";
            this.cbShowUploadDuration.Size = new System.Drawing.Size(196, 17);
            this.cbShowUploadDuration.TabIndex = 8;
            this.cbShowUploadDuration.Text = "Show upload duration in Balloon Tip";
            this.cbShowUploadDuration.UseVisualStyleBackColor = true;
            this.cbShowUploadDuration.CheckedChanged += new System.EventHandler(this.cbShowUploadDuration_CheckedChanged);
            // 
            // chkBalloonTipOpenLink
            // 
            this.chkBalloonTipOpenLink.AutoSize = true;
            this.chkBalloonTipOpenLink.Location = new System.Drawing.Point(16, 104);
            this.chkBalloonTipOpenLink.Name = "chkBalloonTipOpenLink";
            this.chkBalloonTipOpenLink.Size = new System.Drawing.Size(250, 17);
            this.chkBalloonTipOpenLink.TabIndex = 6;
            this.chkBalloonTipOpenLink.Text = "Open screenshot URL/File on Balloon Tip Click";
            this.chkBalloonTipOpenLink.UseVisualStyleBackColor = true;
            this.chkBalloonTipOpenLink.CheckedChanged += new System.EventHandler(this.chkBalloonTipOpenLink_CheckedChanged);
            // 
            // cbShowPopup
            // 
            this.cbShowPopup.AutoSize = true;
            this.cbShowPopup.Location = new System.Drawing.Point(16, 80);
            this.cbShowPopup.Name = "cbShowPopup";
            this.cbShowPopup.Size = new System.Drawing.Size(230, 17);
            this.cbShowPopup.TabIndex = 5;
            this.cbShowPopup.Text = "Show Balloon Tip after upload is completed";
            this.cbShowPopup.UseVisualStyleBackColor = true;
            this.cbShowPopup.CheckedChanged += new System.EventHandler(this.cbShowPopup_CheckedChanged);
            // 
            // lblTrayFlash
            // 
            this.lblTrayFlash.AutoSize = true;
            this.lblTrayFlash.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblTrayFlash.Location = new System.Drawing.Point(14, 26);
            this.lblTrayFlash.Name = "lblTrayFlash";
            this.lblTrayFlash.Size = new System.Drawing.Size(315, 13);
            this.lblTrayFlash.TabIndex = 3;
            this.lblTrayFlash.Text = "Number of times tray icon should flash after an upload is complete";
            // 
            // nudFlashIconCount
            // 
            this.nudFlashIconCount.Location = new System.Drawing.Point(336, 22);
            this.nudFlashIconCount.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.nudFlashIconCount.Name = "nudFlashIconCount";
            this.nudFlashIconCount.Size = new System.Drawing.Size(58, 20);
            this.nudFlashIconCount.TabIndex = 4;
            this.nudFlashIconCount.ValueChanged += new System.EventHandler(this.nudFlashIconCount_ValueChanged);
            // 
            // tpAdvPaths
            // 
            this.tpAdvPaths.Controls.Add(this.gbRoot);
            this.tpAdvPaths.Controls.Add(this.gbSaveLoc);
            this.tpAdvPaths.Controls.Add(this.gbSettingsExportImport);
            this.tpAdvPaths.Controls.Add(this.gbRemoteDirCache);
            this.tpAdvPaths.Location = new System.Drawing.Point(4, 22);
            this.tpAdvPaths.Name = "tpAdvPaths";
            this.tpAdvPaths.Size = new System.Drawing.Size(791, 410);
            this.tpAdvPaths.TabIndex = 2;
            this.tpAdvPaths.Text = "Paths";
            this.tpAdvPaths.UseVisualStyleBackColor = true;
            // 
            // gbRoot
            // 
            this.gbRoot.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gbRoot.Controls.Add(this.btnViewRootDir);
            this.gbRoot.Controls.Add(this.btnBrowseRootDir);
            this.gbRoot.Controls.Add(this.txtRootFolder);
            this.gbRoot.Location = new System.Drawing.Point(8, 8);
            this.gbRoot.Name = "gbRoot";
            this.gbRoot.Size = new System.Drawing.Size(765, 64);
            this.gbRoot.TabIndex = 117;
            this.gbRoot.TabStop = false;
            this.gbRoot.Text = "Root";
            // 
            // btnViewRootDir
            // 
            this.btnViewRootDir.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnViewRootDir.Location = new System.Drawing.Point(645, 22);
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
            this.btnBrowseRootDir.Location = new System.Drawing.Point(557, 22);
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
            this.txtRootFolder.Size = new System.Drawing.Size(533, 20);
            this.txtRootFolder.TabIndex = 114;
            this.txtRootFolder.Tag = "Path of the Root folder that holds Images, Text, Cache, Settings and Temp folders" +
                "";
            // 
            // gbSaveLoc
            // 
            this.gbSaveLoc.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gbSaveLoc.BackColor = System.Drawing.Color.Transparent;
            this.gbSaveLoc.Controls.Add(this.btnMoveImageFiles);
            this.gbSaveLoc.Controls.Add(this.lblImagesFolderPattern);
            this.gbSaveLoc.Controls.Add(this.lblImagesFolderPatternPreview);
            this.gbSaveLoc.Controls.Add(this.txtImagesFolderPattern);
            this.gbSaveLoc.Controls.Add(this.cbDeleteLocal);
            this.gbSaveLoc.Controls.Add(this.btnViewImagesDir);
            this.gbSaveLoc.Controls.Add(this.txtImagesDir);
            this.gbSaveLoc.Location = new System.Drawing.Point(8, 80);
            this.gbSaveLoc.Name = "gbSaveLoc";
            this.gbSaveLoc.Size = new System.Drawing.Size(765, 120);
            this.gbSaveLoc.TabIndex = 114;
            this.gbSaveLoc.TabStop = false;
            this.gbSaveLoc.Text = "Images";
            // 
            // btnMoveImageFiles
            // 
            this.btnMoveImageFiles.Location = new System.Drawing.Point(576, 56);
            this.btnMoveImageFiles.Name = "btnMoveImageFiles";
            this.btnMoveImageFiles.Size = new System.Drawing.Size(176, 23);
            this.btnMoveImageFiles.TabIndex = 117;
            this.btnMoveImageFiles.Text = "Move Images to Sub-folders...";
            this.btnMoveImageFiles.UseVisualStyleBackColor = true;
            this.btnMoveImageFiles.Click += new System.EventHandler(this.btnMoveImageFiles_Click);
            // 
            // lblImagesFolderPattern
            // 
            this.lblImagesFolderPattern.AutoSize = true;
            this.lblImagesFolderPattern.Location = new System.Drawing.Point(16, 59);
            this.lblImagesFolderPattern.Name = "lblImagesFolderPattern";
            this.lblImagesFolderPattern.Size = new System.Drawing.Size(95, 13);
            this.lblImagesFolderPattern.TabIndex = 116;
            this.lblImagesFolderPattern.Text = "Sub-folder Pattern:";
            // 
            // lblImagesFolderPatternPreview
            // 
            this.lblImagesFolderPatternPreview.AutoSize = true;
            this.lblImagesFolderPatternPreview.Location = new System.Drawing.Point(232, 59);
            this.lblImagesFolderPatternPreview.Name = "lblImagesFolderPatternPreview";
            this.lblImagesFolderPatternPreview.Size = new System.Drawing.Size(81, 13);
            this.lblImagesFolderPatternPreview.TabIndex = 115;
            this.lblImagesFolderPatternPreview.Text = "Pattern preview";
            // 
            // txtImagesFolderPattern
            // 
            this.txtImagesFolderPattern.Location = new System.Drawing.Point(120, 56);
            this.txtImagesFolderPattern.Name = "txtImagesFolderPattern";
            this.txtImagesFolderPattern.Size = new System.Drawing.Size(100, 20);
            this.txtImagesFolderPattern.TabIndex = 114;
            this.ttZScreen.SetToolTip(this.txtImagesFolderPattern, "%y = Year\r\n%mo = Month\r\n%mon = Month Name\r\n%d = Day");
            this.txtImagesFolderPattern.TextChanged += new System.EventHandler(this.txtImagesFolderPattern_TextChanged);
            // 
            // cbDeleteLocal
            // 
            this.cbDeleteLocal.AutoSize = true;
            this.cbDeleteLocal.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.cbDeleteLocal.Location = new System.Drawing.Point(16, 88);
            this.cbDeleteLocal.Name = "cbDeleteLocal";
            this.cbDeleteLocal.Size = new System.Drawing.Size(283, 17);
            this.cbDeleteLocal.TabIndex = 0;
            this.cbDeleteLocal.Text = "Delete captured screenshots after upload is completed";
            this.cbDeleteLocal.UseVisualStyleBackColor = true;
            this.cbDeleteLocal.CheckedChanged += new System.EventHandler(this.cbDeleteLocal_CheckedChanged);
            // 
            // btnViewImagesDir
            // 
            this.btnViewImagesDir.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnViewImagesDir.Location = new System.Drawing.Point(648, 22);
            this.btnViewImagesDir.Name = "btnViewImagesDir";
            this.btnViewImagesDir.Size = new System.Drawing.Size(104, 24);
            this.btnViewImagesDir.TabIndex = 113;
            this.btnViewImagesDir.Text = "View Directory...";
            this.btnViewImagesDir.UseVisualStyleBackColor = true;
            this.btnViewImagesDir.Click += new System.EventHandler(this.btnViewLocalDirectory_Click);
            // 
            // txtImagesDir
            // 
            this.txtImagesDir.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtImagesDir.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.txtImagesDir.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.FileSystemDirectories;
            this.txtImagesDir.Location = new System.Drawing.Point(16, 24);
            this.txtImagesDir.Name = "txtImagesDir";
            this.txtImagesDir.ReadOnly = true;
            this.txtImagesDir.Size = new System.Drawing.Size(621, 20);
            this.txtImagesDir.TabIndex = 1;
            this.txtImagesDir.TextChanged += new System.EventHandler(this.txtFileDirectory_TextChanged);
            // 
            // gbSettingsExportImport
            // 
            this.gbSettingsExportImport.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gbSettingsExportImport.BackColor = System.Drawing.Color.Transparent;
            this.gbSettingsExportImport.Controls.Add(this.txtSettingsDir);
            this.gbSettingsExportImport.Controls.Add(this.btnSettingsDefault);
            this.gbSettingsExportImport.Controls.Add(this.btnSettingsExport);
            this.gbSettingsExportImport.Controls.Add(this.btnFTPExport);
            this.gbSettingsExportImport.Controls.Add(this.btnFTPImport);
            this.gbSettingsExportImport.Controls.Add(this.btnViewSettingsDir);
            this.gbSettingsExportImport.Controls.Add(this.btnSettingsImport);
            this.gbSettingsExportImport.Location = new System.Drawing.Point(8, 304);
            this.gbSettingsExportImport.Name = "gbSettingsExportImport";
            this.gbSettingsExportImport.Size = new System.Drawing.Size(765, 96);
            this.gbSettingsExportImport.TabIndex = 6;
            this.gbSettingsExportImport.TabStop = false;
            this.gbSettingsExportImport.Text = "Settings";
            // 
            // txtSettingsDir
            // 
            this.txtSettingsDir.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSettingsDir.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.txtSettingsDir.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.FileSystemDirectories;
            this.txtSettingsDir.Location = new System.Drawing.Point(16, 24);
            this.txtSettingsDir.Name = "txtSettingsDir";
            this.txtSettingsDir.ReadOnly = true;
            this.txtSettingsDir.Size = new System.Drawing.Size(621, 20);
            this.txtSettingsDir.TabIndex = 2;
            // 
            // btnSettingsDefault
            // 
            this.btnSettingsDefault.AutoSize = true;
            this.btnSettingsDefault.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnSettingsDefault.Location = new System.Drawing.Point(224, 56);
            this.btnSettingsDefault.Name = "btnSettingsDefault";
            this.btnSettingsDefault.Size = new System.Drawing.Size(101, 23);
            this.btnSettingsDefault.TabIndex = 1;
            this.btnSettingsDefault.Text = "Default Settings...";
            this.btnSettingsDefault.UseVisualStyleBackColor = true;
            this.btnSettingsDefault.Click += new System.EventHandler(this.btnDeleteSettings_Click);
            // 
            // btnSettingsExport
            // 
            this.btnSettingsExport.AutoSize = true;
            this.btnSettingsExport.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnSettingsExport.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnSettingsExport.Location = new System.Drawing.Point(120, 56);
            this.btnSettingsExport.Name = "btnSettingsExport";
            this.btnSettingsExport.Size = new System.Drawing.Size(97, 23);
            this.btnSettingsExport.TabIndex = 1;
            this.btnSettingsExport.Text = "Export Settings...";
            this.btnSettingsExport.UseVisualStyleBackColor = true;
            this.btnSettingsExport.Click += new System.EventHandler(this.btnSettingsExport_Click);
            // 
            // btnFTPExport
            // 
            this.btnFTPExport.AutoSize = true;
            this.btnFTPExport.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnFTPExport.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnFTPExport.Location = new System.Drawing.Point(496, 56);
            this.btnFTPExport.Name = "btnFTPExport";
            this.btnFTPExport.Size = new System.Drawing.Size(127, 23);
            this.btnFTPExport.TabIndex = 38;
            this.btnFTPExport.Text = "Export FTP Accounts...";
            this.btnFTPExport.UseVisualStyleBackColor = true;
            this.btnFTPExport.Click += new System.EventHandler(this.btnExportAccounts_Click);
            // 
            // btnFTPImport
            // 
            this.btnFTPImport.AutoSize = true;
            this.btnFTPImport.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnFTPImport.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnFTPImport.Location = new System.Drawing.Point(360, 56);
            this.btnFTPImport.Name = "btnFTPImport";
            this.btnFTPImport.Size = new System.Drawing.Size(126, 23);
            this.btnFTPImport.TabIndex = 39;
            this.btnFTPImport.Text = "Import FTP Accounts...";
            this.btnFTPImport.UseVisualStyleBackColor = true;
            this.btnFTPImport.Click += new System.EventHandler(this.btnAccsImport_Click);
            // 
            // btnViewSettingsDir
            // 
            this.btnViewSettingsDir.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnViewSettingsDir.AutoSize = true;
            this.btnViewSettingsDir.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnViewSettingsDir.Location = new System.Drawing.Point(645, 23);
            this.btnViewSettingsDir.Name = "btnViewSettingsDir";
            this.btnViewSettingsDir.Size = new System.Drawing.Size(104, 23);
            this.btnViewSettingsDir.TabIndex = 0;
            this.btnViewSettingsDir.Text = "View Directory...";
            this.btnViewSettingsDir.UseVisualStyleBackColor = true;
            this.btnViewSettingsDir.Click += new System.EventHandler(this.btnBrowseConfig_Click);
            // 
            // btnSettingsImport
            // 
            this.btnSettingsImport.AutoSize = true;
            this.btnSettingsImport.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnSettingsImport.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnSettingsImport.Location = new System.Drawing.Point(16, 56);
            this.btnSettingsImport.Name = "btnSettingsImport";
            this.btnSettingsImport.Size = new System.Drawing.Size(96, 23);
            this.btnSettingsImport.TabIndex = 0;
            this.btnSettingsImport.Text = "Import Settings...";
            this.btnSettingsImport.UseVisualStyleBackColor = true;
            this.btnSettingsImport.Click += new System.EventHandler(this.btnSettingsImport_Click);
            // 
            // gbRemoteDirCache
            // 
            this.gbRemoteDirCache.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gbRemoteDirCache.BackColor = System.Drawing.Color.Transparent;
            this.gbRemoteDirCache.Controls.Add(this.btnViewCacheDir);
            this.gbRemoteDirCache.Controls.Add(this.lblCacheSize);
            this.gbRemoteDirCache.Controls.Add(this.lblMebibytes);
            this.gbRemoteDirCache.Controls.Add(this.nudCacheSize);
            this.gbRemoteDirCache.Controls.Add(this.txtCacheDir);
            this.gbRemoteDirCache.Location = new System.Drawing.Point(8, 208);
            this.gbRemoteDirCache.Name = "gbRemoteDirCache";
            this.gbRemoteDirCache.Size = new System.Drawing.Size(765, 88);
            this.gbRemoteDirCache.TabIndex = 1;
            this.gbRemoteDirCache.TabStop = false;
            this.gbRemoteDirCache.Text = "Cache";
            // 
            // btnViewCacheDir
            // 
            this.btnViewCacheDir.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnViewCacheDir.Location = new System.Drawing.Point(645, 22);
            this.btnViewCacheDir.Name = "btnViewCacheDir";
            this.btnViewCacheDir.Size = new System.Drawing.Size(104, 24);
            this.btnViewCacheDir.TabIndex = 7;
            this.btnViewCacheDir.Text = "View Directory...";
            this.btnViewCacheDir.UseVisualStyleBackColor = true;
            this.btnViewCacheDir.Click += new System.EventHandler(this.btnViewRemoteDirectory_Click);
            // 
            // lblCacheSize
            // 
            this.lblCacheSize.AutoSize = true;
            this.lblCacheSize.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblCacheSize.Location = new System.Drawing.Point(14, 60);
            this.lblCacheSize.Name = "lblCacheSize";
            this.lblCacheSize.Size = new System.Drawing.Size(62, 13);
            this.lblCacheSize.TabIndex = 5;
            this.lblCacheSize.Text = "Cache size:";
            // 
            // lblMebibytes
            // 
            this.lblMebibytes.AutoSize = true;
            this.lblMebibytes.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblMebibytes.Location = new System.Drawing.Point(200, 60);
            this.lblMebibytes.Name = "lblMebibytes";
            this.lblMebibytes.Size = new System.Drawing.Size(25, 13);
            this.lblMebibytes.TabIndex = 4;
            this.lblMebibytes.Text = "MiB";
            // 
            // nudCacheSize
            // 
            this.nudCacheSize.Location = new System.Drawing.Point(80, 56);
            this.nudCacheSize.Maximum = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.nudCacheSize.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudCacheSize.Name = "nudCacheSize";
            this.nudCacheSize.Size = new System.Drawing.Size(112, 20);
            this.nudCacheSize.TabIndex = 3;
            this.nudCacheSize.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudCacheSize.ValueChanged += new System.EventHandler(this.nudCacheSize_ValueChanged);
            // 
            // txtCacheDir
            // 
            this.txtCacheDir.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCacheDir.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.txtCacheDir.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.FileSystemDirectories;
            this.txtCacheDir.Location = new System.Drawing.Point(16, 24);
            this.txtCacheDir.Name = "txtCacheDir";
            this.txtCacheDir.ReadOnly = true;
            this.txtCacheDir.Size = new System.Drawing.Size(621, 20);
            this.txtCacheDir.TabIndex = 0;
            this.txtCacheDir.TextChanged += new System.EventHandler(this.txtCacheDir_TextChanged);
            // 
            // tpAdvDebug
            // 
            this.tpAdvDebug.Controls.Add(this.gbStatistics);
            this.tpAdvDebug.Controls.Add(this.gbLastSource);
            this.tpAdvDebug.Location = new System.Drawing.Point(4, 22);
            this.tpAdvDebug.Name = "tpAdvDebug";
            this.tpAdvDebug.Padding = new System.Windows.Forms.Padding(3);
            this.tpAdvDebug.Size = new System.Drawing.Size(791, 410);
            this.tpAdvDebug.TabIndex = 1;
            this.tpAdvDebug.Text = "Debug";
            this.tpAdvDebug.UseVisualStyleBackColor = true;
            // 
            // gbStatistics
            // 
            this.gbStatistics.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gbStatistics.Controls.Add(this.cboDebugAutoScroll);
            this.gbStatistics.Controls.Add(this.btnDebugStart);
            this.gbStatistics.Controls.Add(this.btnCopyStats);
            this.gbStatistics.Controls.Add(this.txtDebugInfo);
            this.gbStatistics.Location = new System.Drawing.Point(8, 8);
            this.gbStatistics.Name = "gbStatistics";
            this.gbStatistics.Size = new System.Drawing.Size(775, 304);
            this.gbStatistics.TabIndex = 28;
            this.gbStatistics.TabStop = false;
            this.gbStatistics.Text = "Statistics";
            // 
            // cboDebugAutoScroll
            // 
            this.cboDebugAutoScroll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cboDebugAutoScroll.AutoSize = true;
            this.cboDebugAutoScroll.Location = new System.Drawing.Point(680, 24);
            this.cboDebugAutoScroll.Name = "cboDebugAutoScroll";
            this.cboDebugAutoScroll.Size = new System.Drawing.Size(77, 17);
            this.cboDebugAutoScroll.TabIndex = 29;
            this.cboDebugAutoScroll.Text = "Auto &Scroll";
            this.cboDebugAutoScroll.UseVisualStyleBackColor = true;
            // 
            // btnDebugStart
            // 
            this.btnDebugStart.Location = new System.Drawing.Point(16, 24);
            this.btnDebugStart.Name = "btnDebugStart";
            this.btnDebugStart.Size = new System.Drawing.Size(64, 24);
            this.btnDebugStart.TabIndex = 30;
            this.btnDebugStart.Text = "Start";
            this.btnDebugStart.UseVisualStyleBackColor = true;
            this.btnDebugStart.Click += new System.EventHandler(this.btnDebugStart_Click);
            // 
            // btnCopyStats
            // 
            this.btnCopyStats.Location = new System.Drawing.Point(88, 24);
            this.btnCopyStats.Name = "btnCopyStats";
            this.btnCopyStats.Size = new System.Drawing.Size(120, 24);
            this.btnCopyStats.TabIndex = 29;
            this.btnCopyStats.Text = "Copy to Clipboard";
            this.btnCopyStats.UseVisualStyleBackColor = true;
            this.btnCopyStats.Click += new System.EventHandler(this.btnCopyStats_Click);
            // 
            // txtDebugInfo
            // 
            this.txtDebugInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDebugInfo.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDebugInfo.Location = new System.Drawing.Point(14, 60);
            this.txtDebugInfo.Multiline = true;
            this.txtDebugInfo.Name = "txtDebugInfo";
            this.txtDebugInfo.ReadOnly = true;
            this.txtDebugInfo.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtDebugInfo.Size = new System.Drawing.Size(745, 228);
            this.txtDebugInfo.TabIndex = 27;
            // 
            // gbLastSource
            // 
            this.gbLastSource.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gbLastSource.Controls.Add(this.btnOpenSourceString);
            this.gbLastSource.Controls.Add(this.btnOpenSourceText);
            this.gbLastSource.Controls.Add(this.btnOpenSourceBrowser);
            this.gbLastSource.Location = new System.Drawing.Point(8, 320);
            this.gbLastSource.Name = "gbLastSource";
            this.gbLastSource.Size = new System.Drawing.Size(775, 64);
            this.gbLastSource.TabIndex = 26;
            this.gbLastSource.TabStop = false;
            this.gbLastSource.Text = "Last Source";
            // 
            // btnOpenSourceString
            // 
            this.btnOpenSourceString.Enabled = false;
            this.btnOpenSourceString.Location = new System.Drawing.Point(16, 24);
            this.btnOpenSourceString.Name = "btnOpenSourceString";
            this.btnOpenSourceString.Size = new System.Drawing.Size(120, 23);
            this.btnOpenSourceString.TabIndex = 25;
            this.btnOpenSourceString.Text = "Copy to Clipboard";
            this.btnOpenSourceString.UseVisualStyleBackColor = true;
            this.btnOpenSourceString.Click += new System.EventHandler(this.btnOpenSourceString_Click);
            // 
            // btnOpenSourceText
            // 
            this.btnOpenSourceText.Enabled = false;
            this.btnOpenSourceText.Location = new System.Drawing.Point(142, 24);
            this.btnOpenSourceText.Name = "btnOpenSourceText";
            this.btnOpenSourceText.Size = new System.Drawing.Size(120, 23);
            this.btnOpenSourceText.TabIndex = 24;
            this.btnOpenSourceText.Text = "Open in Text Editor";
            this.btnOpenSourceText.UseVisualStyleBackColor = true;
            this.btnOpenSourceText.Click += new System.EventHandler(this.btnOpenSourceText_Click);
            // 
            // btnOpenSourceBrowser
            // 
            this.btnOpenSourceBrowser.Enabled = false;
            this.btnOpenSourceBrowser.Location = new System.Drawing.Point(268, 24);
            this.btnOpenSourceBrowser.Name = "btnOpenSourceBrowser";
            this.btnOpenSourceBrowser.Size = new System.Drawing.Size(120, 23);
            this.btnOpenSourceBrowser.TabIndex = 22;
            this.btnOpenSourceBrowser.Text = "Open in Browser";
            this.btnOpenSourceBrowser.UseVisualStyleBackColor = true;
            this.btnOpenSourceBrowser.Click += new System.EventHandler(this.btnOpenSourceBrowser_Click);
            // 
            // tpOptionsAdv
            // 
            this.tpOptionsAdv.Controls.Add(this.pgApp);
            this.tpOptionsAdv.Location = new System.Drawing.Point(4, 22);
            this.tpOptionsAdv.Name = "tpOptionsAdv";
            this.tpOptionsAdv.Padding = new System.Windows.Forms.Padding(3);
            this.tpOptionsAdv.Size = new System.Drawing.Size(791, 410);
            this.tpOptionsAdv.TabIndex = 3;
            this.tpOptionsAdv.Text = "Advanced";
            this.tpOptionsAdv.UseVisualStyleBackColor = true;
            // 
            // pgApp
            // 
            this.pgApp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pgApp.Location = new System.Drawing.Point(3, 3);
            this.pgApp.Name = "pgApp";
            this.pgApp.Size = new System.Drawing.Size(785, 404);
            this.pgApp.TabIndex = 0;
            this.pgApp.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.confApp_PropertyValueChanged);
            // 
            // tpUploadText
            // 
            this.tpUploadText.Location = new System.Drawing.Point(4, 22);
            this.tpUploadText.Name = "tpUploadText";
            this.tpUploadText.Padding = new System.Windows.Forms.Padding(3);
            this.tpUploadText.Size = new System.Drawing.Size(766, 402);
            this.tpUploadText.TabIndex = 0;
            this.tpUploadText.Text = "Upload text";
            this.tpUploadText.UseVisualStyleBackColor = true;
            // 
            // txtTextUploaderContent
            // 
            this.txtTextUploaderContent.Dock = System.Windows.Forms.DockStyle.Top;
            this.txtTextUploaderContent.Location = new System.Drawing.Point(3, 3);
            this.txtTextUploaderContent.Multiline = true;
            this.txtTextUploaderContent.Name = "txtTextUploaderContent";
            this.txtTextUploaderContent.Size = new System.Drawing.Size(760, 357);
            this.txtTextUploaderContent.TabIndex = 1;
            // 
            // btnUploadText
            // 
            this.btnUploadText.Location = new System.Drawing.Point(0, 0);
            this.btnUploadText.Name = "btnUploadText";
            this.btnUploadText.Size = new System.Drawing.Size(75, 23);
            this.btnUploadText.TabIndex = 0;
            // 
            // btnUploadTextClipboard
            // 
            this.btnUploadTextClipboard.Location = new System.Drawing.Point(0, 0);
            this.btnUploadTextClipboard.Name = "btnUploadTextClipboard";
            this.btnUploadTextClipboard.Size = new System.Drawing.Size(75, 23);
            this.btnUploadTextClipboard.TabIndex = 0;
            // 
            // btnUploadTextClipboardFile
            // 
            this.btnUploadTextClipboardFile.Location = new System.Drawing.Point(0, 0);
            this.btnUploadTextClipboardFile.Name = "btnUploadTextClipboardFile";
            this.btnUploadTextClipboardFile.Size = new System.Drawing.Size(75, 23);
            this.btnUploadTextClipboardFile.TabIndex = 0;
            // 
            // ttZScreen
            // 
            this.ttZScreen.AutomaticDelay = 1000;
            this.ttZScreen.AutoPopDelay = 60000;
            this.ttZScreen.InitialDelay = 1000;
            this.ttZScreen.IsBalloon = true;
            this.ttZScreen.ReshowDelay = 200;
            this.ttZScreen.ShowAlways = true;
            // 
            // ZScreen
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(817, 473);
            this.Controls.Add(this.tcApp);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(825, 500);
            this.Name = "ZScreen";
            this.Padding = new System.Windows.Forms.Padding(2);
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ZScreen";
            this.WindowState = System.Windows.Forms.FormWindowState.Minimized;
            this.Deactivate += new System.EventHandler(this.ZScreen_Deactivate);
            this.Load += new System.EventHandler(this.ZScreen_Load);
            this.Shown += new System.EventHandler(this.ZScreen_Shown);
            this.Leave += new System.EventHandler(this.ZScreen_Leave);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ZScreen_FormClosing);
            this.Resize += new System.EventHandler(this.ZScreen_Resize);
            this.cmTray.ResumeLayout(false);
            this.cmsHistory.ResumeLayout(false);
            this.tcApp.ResumeLayout(false);
            this.tpMain.ResumeLayout(false);
            this.tpMain.PerformLayout();
            this.gbImageSettings.ResumeLayout(false);
            this.gbImageSettings.PerformLayout();
            this.gbMainOptions.ResumeLayout(false);
            this.gbMainOptions.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbLogo)).EndInit();
            this.tpDestinations.ResumeLayout(false);
            this.tcAccounts.ResumeLayout(false);
            this.tpFTP.ResumeLayout(false);
            this.gbFTPSettings.ResumeLayout(false);
            this.gbFTPSettings.PerformLayout();
            this.tpTinyPic.ResumeLayout(false);
            this.tpTinyPic.PerformLayout();
            this.gbTinyPic.ResumeLayout(false);
            this.gbTinyPic.PerformLayout();
            this.tpImageShack.ResumeLayout(false);
            this.tpImageShack.PerformLayout();
            this.gbImageShack.ResumeLayout(false);
            this.gbImageShack.PerformLayout();
            this.tpTwitter.ResumeLayout(false);
            this.tcTwitter.ResumeLayout(false);
            this.tpTwitPic.ResumeLayout(false);
            this.tpTwitPic.PerformLayout();
            this.tpYfrog.ResumeLayout(false);
            this.tpYfrog.PerformLayout();
            this.gbTwitterAccount.ResumeLayout(false);
            this.gbTwitterAccount.PerformLayout();
            this.tpImageBam.ResumeLayout(false);
            this.gbImageBamGalleries.ResumeLayout(false);
            this.gbImageBamLinks.ResumeLayout(false);
            this.gbImageBamLinks.PerformLayout();
            this.gbImageBamApiKeys.ResumeLayout(false);
            this.gbImageBamApiKeys.PerformLayout();
            this.tpRapidShare.ResumeLayout(false);
            this.tpRapidShare.PerformLayout();
            this.tpSendSpace.ResumeLayout(false);
            this.tpSendSpace.PerformLayout();
            this.tpMindTouch.ResumeLayout(false);
            this.gbMindTouchOptions.ResumeLayout(false);
            this.gbMindTouchOptions.PerformLayout();
            this.tpFlickr.ResumeLayout(false);
            this.tpHotkeys.ResumeLayout(false);
            this.tpHotkeys.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvHotkeys)).EndInit();
            this.tpScreenshots.ResumeLayout(false);
            this.tcScreenshots.ResumeLayout(false);
            this.tpCropShot.ResumeLayout(false);
            this.gbDynamicRegionBorderColorSettings.ResumeLayout(false);
            this.gbDynamicRegionBorderColorSettings.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudCropRegionStep)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudCropHueRange)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudCropRegionInterval)).EndInit();
            this.gbDynamicCrosshair.ResumeLayout(false);
            this.gbDynamicCrosshair.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudCropCrosshairInterval)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudCropCrosshairStep)).EndInit();
            this.gpCropRegion.ResumeLayout(false);
            this.gpCropRegion.PerformLayout();
            this.gbCropRegionSettings.ResumeLayout(false);
            this.gbCropRegionSettings.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbCropBorderColor)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudCropBorderSize)).EndInit();
            this.gbCrosshairSettings.ResumeLayout(false);
            this.gbCrosshairSettings.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbCropCrosshairColor)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudCrosshairLineCount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudCrosshairLineSize)).EndInit();
            this.gbGridMode.ResumeLayout(false);
            this.gbGridMode.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudCropGridHeight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudCropGridWidth)).EndInit();
            this.tpSelectedWindow.ResumeLayout(false);
            this.tpSelectedWindow.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudSelectedWindowHueRange)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudSelectedWindowRegionStep)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudSelectedWindowRegionInterval)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudSelectedWindowBorderSize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbSelectedWindowBorderColor)).EndInit();
            this.tpWatermark.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbWatermarkShow)).EndInit();
            this.gbWatermarkGeneral.ResumeLayout(false);
            this.gbWatermarkGeneral.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudWatermarkOffset)).EndInit();
            this.tcWatermark.ResumeLayout(false);
            this.tpWatermarkText.ResumeLayout(false);
            this.gbWatermarkBackground.ResumeLayout(false);
            this.gbWatermarkBackground.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackWatermarkBackgroundTrans)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudWatermarkBackTrans)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudWatermarkCornerRadius)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbWatermarkGradient1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbWatermarkBorderColor)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbWatermarkGradient2)).EndInit();
            this.gbWatermarkText.ResumeLayout(false);
            this.gbWatermarkText.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackWatermarkFontTrans)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudWatermarkFontTrans)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbWatermarkFontColor)).EndInit();
            this.tpWatermarkImage.ResumeLayout(false);
            this.tpWatermarkImage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudWatermarkImageScale)).EndInit();
            this.tpFileNaming.ResumeLayout(false);
            this.gbOthersNaming.ResumeLayout(false);
            this.gbOthersNaming.PerformLayout();
            this.gbCodeTitle.ResumeLayout(false);
            this.gbCodeTitle.PerformLayout();
            this.gbActiveWindowNaming.ResumeLayout(false);
            this.gbActiveWindowNaming.PerformLayout();
            this.tpCaptureQuality.ResumeLayout(false);
            this.gbImageSize.ResumeLayout(false);
            this.gbImageSize.PerformLayout();
            this.gbPictureQuality.ResumeLayout(false);
            this.gbPictureQuality.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudSwitchAfter)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudImageQuality)).EndInit();
            this.tpEditors.ResumeLayout(false);
            this.tcEditors.ResumeLayout(false);
            this.tpEditorsImages.ResumeLayout(false);
            this.tpEditorsImages.PerformLayout();
            this.gbImageEditorSettings.ResumeLayout(false);
            this.gbImageEditorSettings.PerformLayout();
            this.tpImageHosting.ResumeLayout(false);
            this.tcImages.ResumeLayout(false);
            this.tpImageUploaders.ResumeLayout(false);
            this.gbImageUploadRetry.ResumeLayout(false);
            this.gbImageUploadRetry.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudUploadDurationLimit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudErrorRetry)).EndInit();
            this.gbImageUploaderOptions.ResumeLayout(false);
            this.gbImageUploaderOptions.PerformLayout();
            this.tpCustomUploaders.ResumeLayout(false);
            this.tpCustomUploaders.PerformLayout();
            this.gbImageUploaders.ResumeLayout(false);
            this.gbImageUploaders.PerformLayout();
            this.gbRegexp.ResumeLayout(false);
            this.gbRegexp.PerformLayout();
            this.gbArguments.ResumeLayout(false);
            this.gbArguments.PerformLayout();
            this.tpWebPageUpload.ResumeLayout(false);
            this.tpWebPageUpload.PerformLayout();
            this.pWebPageImage.ResumeLayout(false);
            this.pWebPageImage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbWebPageImage)).EndInit();
            this.tpTextServices.ResumeLayout(false);
            this.tcTextUploaders.ResumeLayout(false);
            this.tpTextUploaders.ResumeLayout(false);
            this.tpURLShorteners.ResumeLayout(false);
            this.tpTreeGUI.ResumeLayout(false);
            this.tpTranslator.ResumeLayout(false);
            this.tpTranslator.PerformLayout();
            this.tpHistory.ResumeLayout(false);
            this.tcHistory.ResumeLayout(false);
            this.tpHistoryList.ResumeLayout(false);
            this.tlpHistory.ResumeLayout(false);
            this.tlpHistoryControls.ResumeLayout(false);
            this.tlpHistoryControls.PerformLayout();
            this.panelControls.ResumeLayout(false);
            this.panelControls.PerformLayout();
            this.panelPreview.ResumeLayout(false);
            this.panelPreview.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbPreview)).EndInit();
            this.tpHistorySettings.ResumeLayout(false);
            this.tpHistorySettings.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudHistoryMaxItems)).EndInit();
            this.tpOptions.ResumeLayout(false);
            this.tcOptions.ResumeLayout(false);
            this.tpGeneral.ResumeLayout(false);
            this.gbUpdates.ResumeLayout(false);
            this.gbUpdates.PerformLayout();
            this.gbMisc.ResumeLayout(false);
            this.gbMisc.PerformLayout();
            this.tpProxy.ResumeLayout(false);
            this.gpProxySettings.ResumeLayout(false);
            this.gpProxySettings.PerformLayout();
            this.tpInteraction.ResumeLayout(false);
            this.gbActionsToolbarSettings.ResumeLayout(false);
            this.gbActionsToolbarSettings.PerformLayout();
            this.gbDropBox.ResumeLayout(false);
            this.gbDropBox.PerformLayout();
            this.gbAppearance.ResumeLayout(false);
            this.gbAppearance.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudFlashIconCount)).EndInit();
            this.tpAdvPaths.ResumeLayout(false);
            this.gbRoot.ResumeLayout(false);
            this.gbRoot.PerformLayout();
            this.gbSaveLoc.ResumeLayout(false);
            this.gbSaveLoc.PerformLayout();
            this.gbSettingsExportImport.ResumeLayout(false);
            this.gbSettingsExportImport.PerformLayout();
            this.gbRemoteDirCache.ResumeLayout(false);
            this.gbRemoteDirCache.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudCacheSize)).EndInit();
            this.tpAdvDebug.ResumeLayout(false);
            this.gbStatistics.ResumeLayout(false);
            this.gbStatistics.PerformLayout();
            this.gbLastSource.ResumeLayout(false);
            this.tpOptionsAdv.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.NotifyIcon niTray;
        internal System.Windows.Forms.ContextMenuStrip cmTray;
        internal System.Windows.Forms.ToolStripMenuItem tsmExitZScreen;
        internal System.Windows.Forms.ToolStripMenuItem tsmEditinImageSoftware;
        internal System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        internal System.Windows.Forms.ToolStripMenuItem tsmiTabs;
        internal System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        internal System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        internal System.Windows.Forms.ToolStripMenuItem tsmViewLocalDirectory;
        internal System.Windows.Forms.ToolStripMenuItem tsmViewRemoteDirectory;
        internal System.Windows.Forms.ToolStripMenuItem tsmImageDest;
        internal System.Windows.Forms.ToolStripMenuItem tsmCopytoClipboardMode;
        internal System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        internal System.Windows.Forms.ToolStripMenuItem tsmQuickOptions;
        internal System.Windows.Forms.ContextMenuStrip cmsHistory;
        internal System.Windows.Forms.ToolStripMenuItem tsmCopyCbHistory;
        internal System.Windows.Forms.ImageList ilApp;
        internal System.Windows.Forms.ToolStripMenuItem tsmActions;
        internal System.Windows.Forms.ToolStripMenuItem tsmCropShot;
        internal System.Windows.Forms.ToolStripMenuItem tsmClipboardUpload;
        internal System.Windows.Forms.ToolStripMenuItem tsmEntireScreen;
        internal System.Windows.Forms.ToolStripMenuItem tsmLastCropShot;
        internal System.Windows.Forms.ToolStripMenuItem copyImageToolStripMenuItem;
        internal System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        internal System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
        internal System.Windows.Forms.ToolStripMenuItem openLocalFileToolStripMenuItem;
        internal System.Windows.Forms.ToolStripMenuItem browseURLToolStripMenuItem;
        internal System.Windows.Forms.ToolStripSeparator toolStripSeparator8;
        internal System.Windows.Forms.ToolStripMenuItem openSourceToolStripMenuItem;
        internal System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        internal System.Windows.Forms.ToolStripMenuItem copySourceToClipboardStringToolStripMenuItem;
        internal System.Windows.Forms.ToolStripMenuItem openSourceInDefaultWebBrowserHTMLToolStripMenuItem;
        internal System.Windows.Forms.ToolStripMenuItem cmsRetryUpload;
        internal System.Windows.Forms.ToolStripMenuItem tsmHelp;
        internal System.Windows.Forms.ToolStripMenuItem tsmLicense;
        internal System.Windows.Forms.ToolStripMenuItem tsmVersionHistory;
        internal System.Windows.Forms.ToolStripMenuItem tsmAbout;
        internal System.Windows.Forms.ToolStripMenuItem tsmLanguageTranslator;
        internal System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        internal System.Windows.Forms.ToolStripMenuItem tsmSelectedWindow;
        internal System.Windows.Forms.ToolStripMenuItem tsmScreenColorPicker;
        internal System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        internal System.Windows.Forms.ToolStripMenuItem tsmDragDropWindow;
        internal System.Windows.Forms.ToolStripMenuItem tsmQuickActions;
        internal System.Windows.Forms.ToolStripMenuItem autoScreenshotsToolStripMenuItem;
        internal System.Windows.Forms.Timer tmrApp;
        internal System.Windows.Forms.TabControl tcApp;
        internal System.Windows.Forms.TabPage tpMain;
        internal System.Windows.Forms.GroupBox gbGridMode;
        internal System.Windows.Forms.CheckBox cboCropGridMode;
        internal System.Windows.Forms.NumericUpDown nudCropGridHeight;
        internal System.Windows.Forms.Label lblGridSizeWidth;
        internal System.Windows.Forms.Label lblGridSize;
        internal System.Windows.Forms.Label lblGridSizeHeight;
        internal System.Windows.Forms.NumericUpDown nudCropGridWidth;
        internal System.Windows.Forms.LinkLabel llProjectPage;
        internal System.Windows.Forms.LinkLabel llWebsite;
        internal System.Windows.Forms.LinkLabel llblBugReports;
        internal System.Windows.Forms.GroupBox gbMainOptions;
        private NumericUpDownTimer nudtScreenshotDelay;
        internal System.Windows.Forms.Label lblCopytoClipboard;
        internal System.Windows.Forms.ComboBox cboClipboardTextMode;
        internal System.Windows.Forms.CheckBox chkManualNaming;
        internal System.Windows.Forms.CheckBox cbShowCursor;
        internal System.Windows.Forms.Label lblImageUploader;
        internal System.Windows.Forms.ComboBox cboImageUploaders;
        internal System.Windows.Forms.Label lblLogo;
        internal System.Windows.Forms.PictureBox pbLogo;
        internal System.Windows.Forms.TabPage tpHotkeys;
        internal System.Windows.Forms.Label lblHotkeyStatus;
        internal System.Windows.Forms.DataGridView dgvHotkeys;
        internal System.Windows.Forms.TabPage tpScreenshots;
        internal System.Windows.Forms.TabControl tcScreenshots;
        internal System.Windows.Forms.TabPage tpCropShot;
        internal System.Windows.Forms.GroupBox gbCropRegionSettings;
        internal System.Windows.Forms.NumericUpDown nudCropHueRange;
        internal System.Windows.Forms.Label lblCropHueRange;
        internal System.Windows.Forms.NumericUpDown nudCropRegionStep;
        internal System.Windows.Forms.NumericUpDown nudCropRegionInterval;
        internal System.Windows.Forms.Label lblCropRegionStep;
        internal System.Windows.Forms.Label lblCropRegionInterval;
        internal System.Windows.Forms.CheckBox cbCropDynamicBorderColor;
        internal System.Windows.Forms.Label lblCropBorderSize;
        internal System.Windows.Forms.CheckBox cbShowCropRuler;
        internal System.Windows.Forms.CheckBox cbCropShowGrids;
        internal System.Windows.Forms.Label lblCropBorderColor;
        internal System.Windows.Forms.PictureBox pbCropBorderColor;
        internal System.Windows.Forms.NumericUpDown nudCropBorderSize;
        internal System.Windows.Forms.GroupBox gbCrosshairSettings;
        internal System.Windows.Forms.CheckBox cbCropShowMagnifyingGlass;
        internal System.Windows.Forms.CheckBox cbCropShowBigCross;
        internal System.Windows.Forms.CheckBox cbCropDynamicCrosshair;
        internal System.Windows.Forms.NumericUpDown nudCropCrosshairStep;
        internal System.Windows.Forms.PictureBox pbCropCrosshairColor;
        internal System.Windows.Forms.NumericUpDown nudCropCrosshairInterval;
        internal System.Windows.Forms.Label lblCropCrosshairColor;
        internal System.Windows.Forms.Label lblCropCrosshairStep;
        internal System.Windows.Forms.NumericUpDown nudCrosshairLineCount;
        internal System.Windows.Forms.Label lblCropCrosshairInterval;
        internal System.Windows.Forms.NumericUpDown nudCrosshairLineSize;
        internal System.Windows.Forms.Label lblCrosshairLineSize;
        internal System.Windows.Forms.Label lblCrosshairLineCount;
        internal System.Windows.Forms.Label lblCropRegionStyle;
        internal System.Windows.Forms.CheckBox cbRegionHotkeyInfo;
        internal System.Windows.Forms.ComboBox cbCropStyle;
        internal System.Windows.Forms.CheckBox cbRegionRectangleInfo;
        internal System.Windows.Forms.TabPage tpSelectedWindow;
        internal System.Windows.Forms.NumericUpDown nudSelectedWindowHueRange;
        internal System.Windows.Forms.Label lblSelectedWindowHueRange;
        internal System.Windows.Forms.NumericUpDown nudSelectedWindowRegionStep;
        internal System.Windows.Forms.NumericUpDown nudSelectedWindowRegionInterval;
        internal System.Windows.Forms.Label lblSelectedWindowRegionStep;
        internal System.Windows.Forms.Label lblSelectedWindowRegionInterval;
        internal System.Windows.Forms.CheckBox cbSelectedWindowDynamicBorderColor;
        internal System.Windows.Forms.CheckBox cbSelectedWindowRuler;
        internal System.Windows.Forms.Label lblSelectedWindowRegionStyle;
        internal System.Windows.Forms.ComboBox cbSelectedWindowStyle;
        internal System.Windows.Forms.CheckBox cbSelectedWindowRectangleInfo;
        internal System.Windows.Forms.Label lblSelectedWindowBorderColor;
        internal System.Windows.Forms.NumericUpDown nudSelectedWindowBorderSize;
        internal System.Windows.Forms.Label lblSelectedWindowBorderSize;
        internal System.Windows.Forms.PictureBox pbSelectedWindowBorderColor;
        internal System.Windows.Forms.TabPage tpInteraction;
        internal System.Windows.Forms.GroupBox gbActionsToolbarSettings;
        internal System.Windows.Forms.CheckBox cbCloseQuickActions;
        internal System.Windows.Forms.GroupBox gbDropBox;
        internal System.Windows.Forms.CheckBox cbCloseDropBox;
        internal System.Windows.Forms.GroupBox gbAppearance;
        internal System.Windows.Forms.CheckBox cbCompleteSound;
        internal System.Windows.Forms.CheckBox chkCaptureFallback;
        internal System.Windows.Forms.CheckBox cbShowUploadDuration;
        internal System.Windows.Forms.CheckBox chkBalloonTipOpenLink;
        internal System.Windows.Forms.CheckBox cbShowPopup;
        internal System.Windows.Forms.Label lblTrayFlash;
        internal System.Windows.Forms.NumericUpDown nudFlashIconCount;
        internal System.Windows.Forms.TabPage tpFileNaming;
        internal System.Windows.Forms.Button btnResetIncrement;
        internal System.Windows.Forms.GroupBox gbOthersNaming;
        internal System.Windows.Forms.Label lblEntireScreenPreview;
        internal System.Windows.Forms.TextBox txtEntireScreen;
        internal System.Windows.Forms.GroupBox gbCodeTitle;
        internal System.Windows.Forms.Button btnCodesI;
        internal System.Windows.Forms.Button btnCodesPm;
        internal System.Windows.Forms.Button btnCodesS;
        internal System.Windows.Forms.Button btnCodesMi;
        internal System.Windows.Forms.Button btnCodesH;
        internal System.Windows.Forms.Button btnCodesY;
        internal System.Windows.Forms.Button btnCodesD;
        internal System.Windows.Forms.Button btnCodesMo;
        internal System.Windows.Forms.Button btnCodesT;
        internal System.Windows.Forms.Label lblCodeI;
        internal System.Windows.Forms.Label lblCodeT;
        internal System.Windows.Forms.Label lblCodeMo;
        internal System.Windows.Forms.Label lblCodePm;
        internal System.Windows.Forms.Label lblCodeD;
        internal System.Windows.Forms.Label lblCodeS;
        internal System.Windows.Forms.Label lblCodeMi;
        internal System.Windows.Forms.Label lblCodeY;
        internal System.Windows.Forms.Label lblCodeH;
        internal System.Windows.Forms.GroupBox gbActiveWindowNaming;
        internal System.Windows.Forms.Label lblActiveWindowPreview;
        internal System.Windows.Forms.TextBox txtActiveWindow;
        internal System.Windows.Forms.TabPage tpCaptureQuality;
        internal System.Windows.Forms.GroupBox gbPictureQuality;
        internal System.Windows.Forms.NumericUpDown nudSwitchAfter;
        internal System.Windows.Forms.NumericUpDown nudImageQuality;
        internal System.Windows.Forms.Label lblJPEGQualityPercentage;
        internal System.Windows.Forms.Label lblQuality;
        internal System.Windows.Forms.ComboBox cbSwitchFormat;
        internal System.Windows.Forms.Label lblFileFormat;
        internal System.Windows.Forms.ComboBox cbFileFormat;
        internal System.Windows.Forms.Label lblKB;
        internal System.Windows.Forms.Label lblAfter;
        internal System.Windows.Forms.Label lblSwitchTo;
        internal System.Windows.Forms.TabPage tpWatermark;
        internal System.Windows.Forms.PictureBox pbWatermarkShow;
        internal System.Windows.Forms.GroupBox gbWatermarkGeneral;
        internal System.Windows.Forms.ComboBox cboWatermarkType;
        internal System.Windows.Forms.CheckBox cbWatermarkAutoHide;
        internal System.Windows.Forms.CheckBox cbWatermarkAddReflection;
        internal System.Windows.Forms.Label lblWatermarkType;
        internal System.Windows.Forms.ComboBox cbWatermarkPosition;
        internal System.Windows.Forms.Label lblWatermarkPosition;
        internal System.Windows.Forms.NumericUpDown nudWatermarkOffset;
        internal System.Windows.Forms.Label lblWatermarkOffset;
        internal System.Windows.Forms.TabControl tcWatermark;
        internal System.Windows.Forms.TabPage tpWatermarkText;
        internal System.Windows.Forms.GroupBox gbWatermarkBackground;
        internal System.Windows.Forms.TrackBar trackWatermarkBackgroundTrans;
        internal System.Windows.Forms.ComboBox cbWatermarkGradientType;
        internal System.Windows.Forms.Label lblWatermarkGradientType;
        internal System.Windows.Forms.Label lblWatermarkCornerRadiusTip;
        internal System.Windows.Forms.NumericUpDown nudWatermarkBackTrans;
        internal System.Windows.Forms.NumericUpDown nudWatermarkCornerRadius;
        internal System.Windows.Forms.Label lblRectangleCornerRadius;
        internal System.Windows.Forms.Label lblWatermarkBackColorsTip;
        internal System.Windows.Forms.Label lblWatermarkBackTrans;
        internal System.Windows.Forms.PictureBox pbWatermarkGradient1;
        internal System.Windows.Forms.PictureBox pbWatermarkBorderColor;
        internal System.Windows.Forms.PictureBox pbWatermarkGradient2;
        internal System.Windows.Forms.Label lblWatermarkBackColors;
        internal System.Windows.Forms.GroupBox gbWatermarkText;
        internal System.Windows.Forms.TrackBar trackWatermarkFontTrans;
        internal System.Windows.Forms.Label lblWatermarkText;
        internal System.Windows.Forms.NumericUpDown nudWatermarkFontTrans;
        internal System.Windows.Forms.Label lblWatermarkFont;
        internal System.Windows.Forms.Button btnWatermarkFont;
        internal System.Windows.Forms.Label lblWatermarkFontTrans;
        internal System.Windows.Forms.TextBox txtWatermarkText;
        internal System.Windows.Forms.PictureBox pbWatermarkFontColor;
        internal System.Windows.Forms.TabPage tpWatermarkImage;
        internal System.Windows.Forms.Label lblWatermarkImageScale;
        internal System.Windows.Forms.NumericUpDown nudWatermarkImageScale;
        internal System.Windows.Forms.CheckBox cbWatermarkUseBorder;
        internal System.Windows.Forms.Button btwWatermarkBrowseImage;
        internal System.Windows.Forms.TextBox txtWatermarkImageLocation;
        internal System.Windows.Forms.TabPage tpEditors;
        internal System.Windows.Forms.TabControl tcEditors;
        internal System.Windows.Forms.TabPage tpEditorsImages;
        internal System.Windows.Forms.Button btnRemoveImageEditor;
        internal System.Windows.Forms.Button btnBrowseImageEditor;
        internal System.Windows.Forms.ListBox lbImageSoftware;
        internal System.Windows.Forms.Button btnAddImageSoftware;
        internal System.Windows.Forms.GroupBox gbFTPSettings;
        internal System.Windows.Forms.CheckBox cbAutoSwitchFileUploader;
        internal System.Windows.Forms.CheckBox chkEnableThumbnail;
        internal System.Windows.Forms.Button btnFTPImport;
        internal System.Windows.Forms.Button btnFTPExport;
        internal System.Windows.Forms.TabPage tpImageHosting;
        internal System.Windows.Forms.TabControl tcImages;
        internal System.Windows.Forms.TabPage tpImageUploaders;
        internal System.Windows.Forms.GroupBox gbImageUploaderOptions;
        internal System.Windows.Forms.Label lblUploadDurationLimit;
        internal System.Windows.Forms.CheckBox cbTinyPicSizeCheck;
        internal System.Windows.Forms.NumericUpDown nudUploadDurationLimit;
        internal System.Windows.Forms.CheckBox cboImageUploadRetryOnTimeout;
        internal System.Windows.Forms.CheckBox cbAddFailedScreenshot;
        internal System.Windows.Forms.CheckBox chkImageUploadRetryOnFail;
        internal System.Windows.Forms.ComboBox cboUploadMode;
        internal System.Windows.Forms.Label lblUploadAs;
        internal System.Windows.Forms.NumericUpDown nudErrorRetry;
        internal System.Windows.Forms.GroupBox gbImageShack;
        internal System.Windows.Forms.Button btnGalleryImageShack;
        internal System.Windows.Forms.Button btnRegCodeImageShack;
        internal System.Windows.Forms.Label lblImageShackRegistrationCode;
        internal System.Windows.Forms.TextBox txtImageShackRegistrationCode;
        internal System.Windows.Forms.GroupBox gbTinyPic;
        internal System.Windows.Forms.Button btnGalleryTinyPic;
        internal System.Windows.Forms.CheckBox chkRememberTinyPicUserPass;
        internal System.Windows.Forms.Button btnRegCodeTinyPic;
        internal System.Windows.Forms.Label lblRegistrationCode;
        internal System.Windows.Forms.TextBox txtTinyPicShuk;
        internal System.Windows.Forms.TabPage tpCustomUploaders;
        internal System.Windows.Forms.RichTextBox txtUploadersLog;
        internal System.Windows.Forms.Button btnUploadersTest;
        internal System.Windows.Forms.TextBox txtFullImage;
        internal System.Windows.Forms.TextBox txtThumbnail;
        internal System.Windows.Forms.Label lblFullImage;
        internal System.Windows.Forms.Label lblThumbnail;
        internal System.Windows.Forms.GroupBox gbImageUploaders;
        internal System.Windows.Forms.ListBox lbUploader;
        internal System.Windows.Forms.Button btnUploadersClear;
        internal System.Windows.Forms.Button btnUploaderExport;
        internal System.Windows.Forms.Button btnUploaderRemove;
        internal System.Windows.Forms.Button btnUploaderImport;
        internal System.Windows.Forms.Button btnUploaderUpdate;
        internal System.Windows.Forms.TextBox txtUploader;
        internal System.Windows.Forms.Button btnUploaderAdd;
        internal System.Windows.Forms.GroupBox gbRegexp;
        internal System.Windows.Forms.Button btnRegexpEdit;
        internal System.Windows.Forms.TextBox txtRegexp;
        internal System.Windows.Forms.ListView lvRegexps;
        internal System.Windows.Forms.ColumnHeader lvRegexpsColumn;
        internal System.Windows.Forms.Button btnRegexpRemove;
        internal System.Windows.Forms.Button btnRegexpAdd;
        internal System.Windows.Forms.TextBox txtFileForm;
        internal System.Windows.Forms.Label lblFileForm;
        internal System.Windows.Forms.Label lblUploadURL;
        internal System.Windows.Forms.TextBox txtUploadURL;
        internal System.Windows.Forms.GroupBox gbArguments;
        internal System.Windows.Forms.Button btnArgEdit;
        internal System.Windows.Forms.TextBox txtArg2;
        internal System.Windows.Forms.Button btnArgRemove;
        internal System.Windows.Forms.ListView lvArguments;
        internal System.Windows.Forms.ColumnHeader columnHeader1;
        internal System.Windows.Forms.ColumnHeader columnHeader2;
        internal System.Windows.Forms.Button btnArgAdd;
        internal System.Windows.Forms.TextBox txtArg1;
        internal System.Windows.Forms.TabPage tpTranslator;
        internal System.Windows.Forms.Button btnTranslateTo1;
        internal System.Windows.Forms.Label lblDictionary;
        internal System.Windows.Forms.TextBox txtDictionary;
        internal System.Windows.Forms.CheckBox cbClipboardTranslate;
        internal System.Windows.Forms.TextBox txtTranslateResult;
        internal System.Windows.Forms.TextBox txtLanguages;
        internal System.Windows.Forms.Button btnTranslate;
        internal System.Windows.Forms.TextBox txtTranslateText;
        internal System.Windows.Forms.Label lblToLanguage;
        internal System.Windows.Forms.Label lblFromLanguage;
        internal System.Windows.Forms.ComboBox cbToLanguage;
        internal System.Windows.Forms.ComboBox cbFromLanguage;
        internal System.Windows.Forms.TabPage tpHistory;
        internal System.Windows.Forms.TabControl tcHistory;
        internal System.Windows.Forms.TabPage tpHistoryList;
        internal System.Windows.Forms.Label lblHistoryScreenshot;
        internal System.Windows.Forms.Button btnHistoryCopyLink;
        internal System.Windows.Forms.Button btnHistoryCopyImage;
        internal System.Windows.Forms.Button btnHistoryBrowseURL;
        internal System.Windows.Forms.Button btnHistoryOpenLocalFile;
        internal System.Windows.Forms.Label lblHistoryLocalPath;
        internal System.Windows.Forms.TextBox txtHistoryRemotePath;
        internal System.Windows.Forms.Label lblHistoryRemotePath;
        internal System.Windows.Forms.TextBox txtHistoryLocalPath;
        internal System.Windows.Forms.ListBox lbHistory;
        internal System.Windows.Forms.TextBox txtPreview;
        internal System.Windows.Forms.PictureBox pbPreview;
        internal System.Windows.Forms.TabPage tpHistorySettings;
        internal System.Windows.Forms.CheckBox cbHistorySave;
        internal System.Windows.Forms.CheckBox cbShowHistoryTooltip;
        internal System.Windows.Forms.Button btnHistoryClear;
        internal System.Windows.Forms.ComboBox cbHistoryListFormat;
        internal System.Windows.Forms.Label lblHistoryMaxItems;
        internal System.Windows.Forms.Label lblHistoryListFormat;
        internal System.Windows.Forms.NumericUpDown nudHistoryMaxItems;
        internal System.Windows.Forms.CheckBox cbHistoryAddSpace;
        internal System.Windows.Forms.CheckBox cbHistoryReverseList;
        internal System.Windows.Forms.TabPage tpOptions;
        internal System.Windows.Forms.TabControl tcOptions;
        internal System.Windows.Forms.TabPage tpGeneral;
        internal System.Windows.Forms.GroupBox gbUpdates;
        internal System.Windows.Forms.Label lblUpdateInfo;
        internal System.Windows.Forms.Button btnCheckUpdate;
        internal System.Windows.Forms.CheckBox cbCheckUpdates;
        internal System.Windows.Forms.GroupBox gbMisc;
        internal System.Windows.Forms.CheckBox cbLockFormSize;
        internal System.Windows.Forms.CheckBox chkShowTaskbar;
        internal System.Windows.Forms.CheckBox chkOpenMainWindow;
        internal System.Windows.Forms.CheckBox chkStartWin;
        internal System.Windows.Forms.TabPage tpAdvPaths;
        internal System.Windows.Forms.GroupBox gbRoot;
        internal System.Windows.Forms.Button btnViewRootDir;
        internal System.Windows.Forms.Button btnBrowseRootDir;
        internal System.Windows.Forms.TextBox txtRootFolder;
        internal System.Windows.Forms.GroupBox gbSaveLoc;
        internal System.Windows.Forms.CheckBox cbDeleteLocal;
        internal System.Windows.Forms.Button btnViewImagesDir;
        internal System.Windows.Forms.TextBox txtImagesDir;
        internal System.Windows.Forms.GroupBox gbSettingsExportImport;
        internal System.Windows.Forms.TextBox txtSettingsDir;
        internal System.Windows.Forms.Button btnSettingsDefault;
        internal System.Windows.Forms.Button btnSettingsExport;
        internal System.Windows.Forms.Button btnViewSettingsDir;
        internal System.Windows.Forms.Button btnSettingsImport;
        internal System.Windows.Forms.GroupBox gbRemoteDirCache;
        internal System.Windows.Forms.Button btnViewCacheDir;
        internal System.Windows.Forms.Label lblCacheSize;
        internal System.Windows.Forms.Label lblMebibytes;
        internal System.Windows.Forms.NumericUpDown nudCacheSize;
        internal System.Windows.Forms.TextBox txtCacheDir;
        internal System.Windows.Forms.TabPage tpAdvDebug;
        internal System.Windows.Forms.GroupBox gbStatistics;
        internal System.Windows.Forms.Button btnDebugStart;
        internal System.Windows.Forms.Button btnCopyStats;
        internal System.Windows.Forms.GroupBox gbLastSource;
        internal System.Windows.Forms.Button btnOpenSourceString;
        internal System.Windows.Forms.Button btnOpenSourceText;
        internal System.Windows.Forms.Button btnOpenSourceBrowser;
        internal System.Windows.Forms.TabPage tpOptionsAdv;
        internal System.Windows.Forms.PropertyGrid pgApp;
        internal System.Windows.Forms.TabPage tpUploadText;
        internal System.Windows.Forms.TextBox txtTextUploaderContent;
        internal System.Windows.Forms.Button btnUploadText;
        internal System.Windows.Forms.Button btnUploadTextClipboard;
        internal System.Windows.Forms.Button btnUploadTextClipboardFile;
        internal System.Windows.Forms.PropertyGrid pgEditorsImage;
        internal System.Windows.Forms.TabPage tpDestinations;
        internal System.Windows.Forms.TabControl tcAccounts;
        internal System.Windows.Forms.TabPage tpTinyPic;
        internal System.Windows.Forms.TabPage tpImageShack;
        internal System.Windows.Forms.TabPage tpTextServices;
        internal System.Windows.Forms.TabControl tcTextUploaders;
        internal System.Windows.Forms.ComboBox cboTextUploaders;
        internal System.Windows.Forms.Label lblTextUploader;
        internal System.Windows.Forms.GroupBox gbImageSettings;
        internal System.Windows.Forms.GroupBox gpCropRegion;
        internal System.Windows.Forms.TextBox txtAutoTranslate;
        internal System.Windows.Forms.CheckBox cbAutoTranslate;
        internal System.Windows.Forms.TabPage tpURLShorteners;
        internal TextUploadersControl ucUrlShorteners;
        internal System.Windows.Forms.TabPage tpTextUploaders;
        internal TextUploadersControl ucTextUploaders;
        internal System.Windows.Forms.ToolTip ttZScreen;
        internal System.Windows.Forms.CheckBox cbShowHelpBalloonTips;
        internal System.Windows.Forms.TabPage tpMindTouch;
        internal UserControls.AccountsControl ucMindTouchAccounts;
        internal System.Windows.Forms.GroupBox gbImageEditorSettings;
        internal System.Windows.Forms.CheckBox chkImageEditorAutoSave;
        internal System.Windows.Forms.TabPage tpFTP;
        internal UserControls.AccountsControl ucFTPAccounts;
        internal System.Windows.Forms.CheckBox chkPublicImageShack;
        internal System.Windows.Forms.Button btnImageShackProfile;
        internal System.Windows.Forms.Label lblImageShackUsername;
        internal System.Windows.Forms.TextBox txtUserNameImageShack;
        internal System.Windows.Forms.Label lblScreenshotDelay;
        internal System.Windows.Forms.GroupBox gbDynamicCrosshair;
        internal System.Windows.Forms.GroupBox gbDynamicRegionBorderColorSettings;
        internal System.Windows.Forms.DataGridViewTextBoxColumn chHotkeys_Description;
        internal System.Windows.Forms.DataGridViewButtonColumn chHotkeys_Keys;
        internal System.Windows.Forms.TabPage tpTwitter;
        internal System.Windows.Forms.Label lblTwitterPassword;
        internal System.Windows.Forms.TextBox txtTwitPicPassword;
        internal System.Windows.Forms.Label lblTwitterUsername;
        internal System.Windows.Forms.TextBox txtTwitPicUserName;
        internal System.Windows.Forms.GroupBox gbTwitterAccount;
        internal System.Windows.Forms.Label lblTwitPicUploadMode;
        internal System.Windows.Forms.ComboBox cboTwitPicUploadMode;
        internal System.Windows.Forms.GroupBox gbMindTouchOptions;
        internal System.Windows.Forms.CheckBox chkDekiWikiForcePath;
        private System.Windows.Forms.TabPage tpProxy;
        private UserControls.AccountsControl ucProxyAccounts;
        internal System.Windows.Forms.GroupBox gpProxySettings;
        internal System.Windows.Forms.CheckBox chkProxyEnable;
        private System.Windows.Forms.ToolStripMenuItem tsmFTPClient;
        private System.Windows.Forms.Label lblURLShortener;
        private System.Windows.Forms.ComboBox cboURLShorteners;
        private System.Windows.Forms.TabPage tpTreeGUI;
        private System.Windows.Forms.PropertyGrid pgIndexer;
        private System.Windows.Forms.CheckBox cbSelectedWindowCaptureObjects;
        private System.Windows.Forms.Label lblWatermarkOffsetPixel;
        private System.Windows.Forms.CheckBox cbSaveFormSizePosition;
        private System.Windows.Forms.CheckBox cbAutoSaveSettings;
        private System.Windows.Forms.CheckBox cbTwitPicShowFull;
        private System.Windows.Forms.ComboBox cboTwitPicThumbnailMode;
        private System.Windows.Forms.Label lblTwitPicThumbnailMode;
        private System.Windows.Forms.TableLayoutPanel tlpHistory;
        private System.Windows.Forms.TableLayoutPanel tlpHistoryControls;
        private System.Windows.Forms.Panel panelControls;
        private System.Windows.Forms.Panel panelPreview;
        private System.Windows.Forms.WebBrowser historyBrowser;
        private System.Windows.Forms.TextBox txtImagesFolderPattern;
        private System.Windows.Forms.Label lblImagesFolderPatternPreview;
        private System.Windows.Forms.Label lblImagesFolderPattern;
        private System.Windows.Forms.Button btnMoveImageFiles;
        private System.Windows.Forms.CheckBox cbCheckUpdatesBeta;
        private System.Windows.Forms.RadioButton rbImageSizeFixed;
        private System.Windows.Forms.RadioButton rbImageSizeRatio;
        private System.Windows.Forms.TextBox txtImageSizeFixedHeight;
        private System.Windows.Forms.TextBox txtImageSizeFixedWidth;
        private System.Windows.Forms.Label lblImageSizeRatioPercentage;
        private System.Windows.Forms.TextBox txtImageSizeRatio;
        private System.Windows.Forms.GroupBox gbImageSize;
        private System.Windows.Forms.RadioButton rbImageSizeDefault;
        private System.Windows.Forms.Label lblImageSizeFixedHeight;
        private System.Windows.Forms.Label lblImageSizeFixedWidth;
        private System.Windows.Forms.TabPage tpWebPageUpload;
        private System.Windows.Forms.TextBox txtWebPageURL;
        private System.Windows.Forms.Button btnWebPageCaptureImage;
        private System.Windows.Forms.Panel pWebPageImage;
        private System.Windows.Forms.PictureBox pbWebPageImage;
        private System.Windows.Forms.Button btnWebPageImageUpload;
        private System.Windows.Forms.Label lblWebPageHeight;
        private System.Windows.Forms.Label lblWebPageWidth;
        private System.Windows.Forms.TextBox txtWebPageHeight;
        private System.Windows.Forms.TextBox txtWebPageWidth;
        private System.Windows.Forms.CheckBox cbWebPageUseCustomSize;
        private System.Windows.Forms.CheckBox cbWebPageAutoUpload;
        private System.Windows.Forms.TabControl tcTwitter;
        private System.Windows.Forms.TabPage tpTwitPic;
        private System.Windows.Forms.TabPage tpYfrog;
        private System.Windows.Forms.TabPage tpImageBam;
        internal System.Windows.Forms.Label lblImageBamSecret;
        internal System.Windows.Forms.TextBox txtImageBamSecret;
        internal System.Windows.Forms.Label lblImageBamKey;
        internal System.Windows.Forms.TextBox txtImageBamApiKey;
        private System.Windows.Forms.Button btnImageBamApiKeysUrl;
        private System.Windows.Forms.GroupBox gbImageBamLinks;
        private System.Windows.Forms.Button btnImageBamRegister;
        private System.Windows.Forms.GroupBox gbImageBamApiKeys;
        private System.Windows.Forms.GroupBox gbImageBamGalleries;
        private System.Windows.Forms.ListBox lbImageBamGalleries;
        private System.Windows.Forms.Button btnImageBamCreateGallery;
        private System.Windows.Forms.Button btnImageBamRemoveGallery;
        private System.Windows.Forms.CheckBox chkImageBamContentNSFW;
        private System.Windows.Forms.GroupBox gbImageUploadRetry;
        private System.Windows.Forms.TabPage tpRapidShare;
        private System.Windows.Forms.Label lblRapidSharePassword;
        private System.Windows.Forms.Label lblRapidSharePremiumUsername;
        private System.Windows.Forms.Label lblRapidShareCollectorsID;
        private System.Windows.Forms.TextBox txtRapidSharePassword;
        private System.Windows.Forms.TextBox txtRapidSharePremiumUserName;
        private System.Windows.Forms.TextBox txtRapidShareCollectorID;
        private System.Windows.Forms.ComboBox cboRapidShareAcctType;
        private System.Windows.Forms.Label lblRapidShareAccountType;
        private System.Windows.Forms.ComboBox cboFileUploaders;
        private System.Windows.Forms.Label lblFileUploader;
        private System.Windows.Forms.TabPage tpSendSpace;
        private System.Windows.Forms.Label lblSendSpacePassword;
        private System.Windows.Forms.Label lblSendSpaceUsername;
        private System.Windows.Forms.TextBox txtSendSpacePassword;
        private System.Windows.Forms.TextBox txtSendSpaceUserName;
        private System.Windows.Forms.ComboBox cboSendSpaceAcctType;
        private System.Windows.Forms.Label lblSendSpaceAccountType;
        private System.Windows.Forms.Button btnSendSpaceRegister;
        internal System.Windows.Forms.TextBox txtDebugInfo;
        private System.Windows.Forms.CheckBox cboDebugAutoScroll;
        private System.Windows.Forms.Label lblErrorRetry;
        private System.Windows.Forms.Label lblFTPThumbHeight;
        private System.Windows.Forms.Label lblFTPThumbWidth;
        private System.Windows.Forms.TextBox txtFTPThumbWidth;
        private System.Windows.Forms.TextBox txtFTPThumbHeight;
        private System.Windows.Forms.CheckBox cbFTPThumbnailCheckSize;
        internal System.Windows.Forms.ComboBox cboYfrogUploadMode;
        internal System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox chkWindows7TaskbarIntegration;
        private System.Windows.Forms.TabPage tpFlickr;
        private System.Windows.Forms.Button btnFlickrGetToken;
        private System.Windows.Forms.Button btnFlickrGetFrob;
        private System.Windows.Forms.Button btnFlickrCheckToken;
        private System.Windows.Forms.PropertyGrid pgFlickrAuthInfo;
        private System.Windows.Forms.PropertyGrid pgFlickrSettings;
        private System.Windows.Forms.Button btnFlickrOpenImages;

    }
}
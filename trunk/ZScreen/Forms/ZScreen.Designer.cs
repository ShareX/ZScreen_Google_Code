using ZScreenLib;

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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.niTray = new System.Windows.Forms.NotifyIcon(this.components);
            this.cmTray = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiTabs = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmImageDest = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmFileDest = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmEditinImageSoftware = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmCopytoClipboardMode = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.historyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmFTPClient = new System.Windows.Forms.ToolStripMenuItem();
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
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.tmrApp = new System.Windows.Forms.Timer(this.components);
            this.tcApp = new System.Windows.Forms.TabControl();
            this.tpMain = new System.Windows.Forms.TabPage();
            this.ucDestOptions = new ZScreenLib.DestSelector();
            this.gbImageSettings = new System.Windows.Forms.GroupBox();
            this.lblScreenshotDelay = new System.Windows.Forms.Label();
            this.nudScreenshotDelay = new ZScreenGUI.NumericUpDownTimer();
            this.lblCopytoClipboard = new System.Windows.Forms.Label();
            this.cboClipboardTextMode = new System.Windows.Forms.ComboBox();
            this.chkShowCursor = new System.Windows.Forms.CheckBox();
            this.chkManualNaming = new System.Windows.Forms.CheckBox();
            this.llProjectPage = new System.Windows.Forms.LinkLabel();
            this.llWebsite = new System.Windows.Forms.LinkLabel();
            this.llblBugReports = new System.Windows.Forms.LinkLabel();
            this.lblLogo = new System.Windows.Forms.Label();
            this.pbLogo = new System.Windows.Forms.PictureBox();
            this.tpDestinations = new System.Windows.Forms.TabPage();
            this.tcDestinations = new System.Windows.Forms.TabControl();
            this.tpDestFTP = new System.Windows.Forms.TabPage();
            this.btnFtpHelp = new System.Windows.Forms.Button();
            this.btnFTPOpenClient = new System.Windows.Forms.Button();
            this.ucFTPAccounts = new ZScreenGUI.AccountsControl();
            this.gbFTPSettings = new System.Windows.Forms.GroupBox();
            this.lblFtpFiles = new System.Windows.Forms.Label();
            this.lblFtpText = new System.Windows.Forms.Label();
            this.lblFtpImages = new System.Windows.Forms.Label();
            this.cboFtpFiles = new System.Windows.Forms.ComboBox();
            this.cboFtpText = new System.Windows.Forms.ComboBox();
            this.cboFtpImages = new System.Windows.Forms.ComboBox();
            this.cbFTPThumbnailCheckSize = new System.Windows.Forms.CheckBox();
            this.lblFTPThumbWidth = new System.Windows.Forms.Label();
            this.txtFTPThumbWidth = new System.Windows.Forms.TextBox();
            this.tpDestDropbox = new System.Windows.Forms.TabPage();
            this.lblDropboxPasswordTip = new System.Windows.Forms.Label();
            this.pbDropboxLogo = new System.Windows.Forms.PictureBox();
            this.lblDropboxLoginTip = new System.Windows.Forms.Label();
            this.btnDropboxRegister = new System.Windows.Forms.Button();
            this.lblDropboxStatus = new System.Windows.Forms.Label();
            this.lblDropboxPathTip = new System.Windows.Forms.Label();
            this.lblDropboxPath = new System.Windows.Forms.Label();
            this.lblDropboxPassword = new System.Windows.Forms.Label();
            this.lblDropboxEmail = new System.Windows.Forms.Label();
            this.btnDropboxLogin = new System.Windows.Forms.Button();
            this.txtDropboxPath = new System.Windows.Forms.TextBox();
            this.txtDropboxPassword = new System.Windows.Forms.TextBox();
            this.txtDropboxEmail = new System.Windows.Forms.TextBox();
            this.tpDestLocalhost = new System.Windows.Forms.TabPage();
            this.ucLocalhostAccounts = new ZScreenGUI.AccountsControl();
            this.tpDestRapidShare = new System.Windows.Forms.TabPage();
            this.lblRapidSharePassword = new System.Windows.Forms.Label();
            this.lblRapidSharePremiumUsername = new System.Windows.Forms.Label();
            this.lblRapidShareCollectorsID = new System.Windows.Forms.Label();
            this.txtRapidSharePassword = new System.Windows.Forms.TextBox();
            this.txtRapidSharePremiumUserName = new System.Windows.Forms.TextBox();
            this.txtRapidShareCollectorID = new System.Windows.Forms.TextBox();
            this.cboRapidShareAcctType = new System.Windows.Forms.ComboBox();
            this.lblRapidShareAccountType = new System.Windows.Forms.Label();
            this.tpDestSendSpace = new System.Windows.Forms.TabPage();
            this.btnSendSpaceRegister = new System.Windows.Forms.Button();
            this.lblSendSpacePassword = new System.Windows.Forms.Label();
            this.lblSendSpaceUsername = new System.Windows.Forms.Label();
            this.txtSendSpacePassword = new System.Windows.Forms.TextBox();
            this.txtSendSpaceUserName = new System.Windows.Forms.TextBox();
            this.cboSendSpaceAcctType = new System.Windows.Forms.ComboBox();
            this.lblSendSpaceAccountType = new System.Windows.Forms.Label();
            this.tpDestFlickr = new System.Windows.Forms.TabPage();
            this.btnFlickrOpenImages = new System.Windows.Forms.Button();
            this.pgFlickrAuthInfo = new System.Windows.Forms.PropertyGrid();
            this.pgFlickrSettings = new System.Windows.Forms.PropertyGrid();
            this.btnFlickrCheckToken = new System.Windows.Forms.Button();
            this.btnFlickrGetToken = new System.Windows.Forms.Button();
            this.btnFlickrGetFrob = new System.Windows.Forms.Button();
            this.tpDestImageShack = new System.Windows.Forms.TabPage();
            this.chkPublicImageShack = new System.Windows.Forms.CheckBox();
            this.gbImageShack = new System.Windows.Forms.GroupBox();
            this.btnImageShackProfile = new System.Windows.Forms.Button();
            this.lblImageShackUsername = new System.Windows.Forms.Label();
            this.txtUserNameImageShack = new System.Windows.Forms.TextBox();
            this.btnGalleryImageShack = new System.Windows.Forms.Button();
            this.btnRegCodeImageShack = new System.Windows.Forms.Button();
            this.lblImageShackRegistrationCode = new System.Windows.Forms.Label();
            this.txtImageShackRegistrationCode = new System.Windows.Forms.TextBox();
            this.tpDestImgur = new System.Windows.Forms.TabPage();
            this.gbImgurUserAccount = new System.Windows.Forms.GroupBox();
            this.btnImgurOpenAuthorizePage = new System.Windows.Forms.Button();
            this.btnImgurLogin = new System.Windows.Forms.Button();
            this.lblImgurStatus = new System.Windows.Forms.Label();
            this.chkImgurUserAccount = new System.Windows.Forms.CheckBox();
            this.tpDestImageBam = new System.Windows.Forms.TabPage();
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
            this.tpDestTinyPic = new System.Windows.Forms.TabPage();
            this.gbTinyPic = new System.Windows.Forms.GroupBox();
            this.btnGalleryTinyPic = new System.Windows.Forms.Button();
            this.btnRegCodeTinyPic = new System.Windows.Forms.Button();
            this.lblRegistrationCode = new System.Windows.Forms.Label();
            this.txtTinyPicShuk = new System.Windows.Forms.TextBox();
            this.chkRememberTinyPicUserPass = new System.Windows.Forms.CheckBox();
            this.tpDestTwitter = new System.Windows.Forms.TabPage();
            this.tlpTwitter = new System.Windows.Forms.TableLayoutPanel();
            this.panelTwitter = new System.Windows.Forms.Panel();
            this.btnTwitterLogin = new System.Windows.Forms.Button();
            this.ucTwitterAccounts = new ZScreenGUI.AccountsControl();
            this.gbTwitterOthers = new System.Windows.Forms.GroupBox();
            this.cbTwitPicShowFull = new System.Windows.Forms.CheckBox();
            this.cboTwitPicThumbnailMode = new System.Windows.Forms.ComboBox();
            this.lblTwitPicThumbnailMode = new System.Windows.Forms.Label();
            this.tpDestMindTouch = new System.Windows.Forms.TabPage();
            this.gbMindTouchOptions = new System.Windows.Forms.GroupBox();
            this.chkDekiWikiForcePath = new System.Windows.Forms.CheckBox();
            this.ucMindTouchAccounts = new ZScreenGUI.AccountsControl();
            this.tpDestMediaWiki = new System.Windows.Forms.TabPage();
            this.ucMediaWikiAccounts = new ZScreenGUI.AccountsControl();
            this.tpDestCustom = new System.Windows.Forms.TabPage();
            this.txtUploadersLog = new System.Windows.Forms.RichTextBox();
            this.btnUploadersTest = new System.Windows.Forms.Button();
            this.txtFullImage = new System.Windows.Forms.TextBox();
            this.txtThumbnail = new System.Windows.Forms.TextBox();
            this.lblFullImage = new System.Windows.Forms.Label();
            this.lblThumbnail = new System.Windows.Forms.Label();
            this.gbImageUploaders = new System.Windows.Forms.GroupBox();
            this.lbImageUploader = new System.Windows.Forms.ListBox();
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
            this.lvRegexpsColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
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
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btnArgAdd = new System.Windows.Forms.Button();
            this.txtArg1 = new System.Windows.Forms.TextBox();
            this.tpHotkeys = new System.Windows.Forms.TabPage();
            this.btnResetHotkeys = new System.Windows.Forms.Button();
            this.lblHotkeyStatus = new System.Windows.Forms.Label();
            this.dgvHotkeys = new System.Windows.Forms.DataGridView();
            this.chHotkeys_Description = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.chHotkeys_Keys = new System.Windows.Forms.DataGridViewButtonColumn();
            this.DefaultKeys = new System.Windows.Forms.DataGridViewTextBoxColumn();
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
            this.chkCropDynamicCrosshair = new System.Windows.Forms.CheckBox();
            this.lblCropCrosshairStep = new System.Windows.Forms.Label();
            this.lblCropCrosshairInterval = new System.Windows.Forms.Label();
            this.nudCropCrosshairInterval = new System.Windows.Forms.NumericUpDown();
            this.nudCropCrosshairStep = new System.Windows.Forms.NumericUpDown();
            this.gpCropRegion = new System.Windows.Forms.GroupBox();
            this.lblCropRegionStyle = new System.Windows.Forms.Label();
            this.chkRegionHotkeyInfo = new System.Windows.Forms.CheckBox();
            this.chkCropStyle = new System.Windows.Forms.ComboBox();
            this.chkRegionRectangleInfo = new System.Windows.Forms.CheckBox();
            this.gbCropRegionSettings = new System.Windows.Forms.GroupBox();
            this.lblCropBorderSize = new System.Windows.Forms.Label();
            this.cbShowCropRuler = new System.Windows.Forms.CheckBox();
            this.cbCropShowGrids = new System.Windows.Forms.CheckBox();
            this.lblCropBorderColor = new System.Windows.Forms.Label();
            this.pbCropBorderColor = new System.Windows.Forms.PictureBox();
            this.nudCropBorderSize = new System.Windows.Forms.NumericUpDown();
            this.gbCrosshairSettings = new System.Windows.Forms.GroupBox();
            this.chkCropShowMagnifyingGlass = new System.Windows.Forms.CheckBox();
            this.chkCropShowBigCross = new System.Windows.Forms.CheckBox();
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
            this.chkSelectedWindowCaptureObjects = new System.Windows.Forms.CheckBox();
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
            this.tpActivewindow = new System.Windows.Forms.TabPage();
            this.chkActiveWindowTryCaptureChildren = new System.Windows.Forms.CheckBox();
            this.cbActiveWindowGDIFreezeWindow = new System.Windows.Forms.CheckBox();
            this.chkSelectedWindowCleanTransparentCorners = new System.Windows.Forms.CheckBox();
            this.chkSelectedWindowShowCheckers = new System.Windows.Forms.CheckBox();
            this.chkSelectedWindowIncludeShadow = new System.Windows.Forms.CheckBox();
            this.chkActiveWindowPreferDWM = new System.Windows.Forms.CheckBox();
            this.chkSelectedWindowCleanBackground = new System.Windows.Forms.CheckBox();
            this.tpFreehandCropShot = new System.Windows.Forms.TabPage();
            this.cbFreehandCropShowRectangleBorder = new System.Windows.Forms.CheckBox();
            this.cbFreehandCropAutoClose = new System.Windows.Forms.CheckBox();
            this.cbFreehandCropAutoUpload = new System.Windows.Forms.CheckBox();
            this.cbFreehandCropShowHelpText = new System.Windows.Forms.CheckBox();
            this.tpWatermark = new System.Windows.Forms.TabPage();
            this.pbWatermarkShow = new System.Windows.Forms.PictureBox();
            this.gbWatermarkGeneral = new System.Windows.Forms.GroupBox();
            this.lblWatermarkOffsetPixel = new System.Windows.Forms.Label();
            this.cboWatermarkType = new System.Windows.Forms.ComboBox();
            this.cbWatermarkAutoHide = new System.Windows.Forms.CheckBox();
            this.cbWatermarkAddReflection = new System.Windows.Forms.CheckBox();
            this.lblWatermarkType = new System.Windows.Forms.Label();
            this.chkWatermarkPosition = new System.Windows.Forms.ComboBox();
            this.lblWatermarkPosition = new System.Windows.Forms.Label();
            this.nudWatermarkOffset = new System.Windows.Forms.NumericUpDown();
            this.lblWatermarkOffset = new System.Windows.Forms.Label();
            this.tcWatermark = new System.Windows.Forms.TabControl();
            this.tpWatermarkText = new System.Windows.Forms.TabPage();
            this.gbWatermarkBackground = new System.Windows.Forms.GroupBox();
            this.lblRectangleCornerRadius = new System.Windows.Forms.Label();
            this.gbGradientMakerBasic = new System.Windows.Forms.GroupBox();
            this.lblWatermarkBackColors = new System.Windows.Forms.Label();
            this.trackWatermarkBackgroundTrans = new System.Windows.Forms.TrackBar();
            this.pbWatermarkGradient2 = new System.Windows.Forms.PictureBox();
            this.cbWatermarkGradientType = new System.Windows.Forms.ComboBox();
            this.pbWatermarkBorderColor = new System.Windows.Forms.PictureBox();
            this.lblWatermarkGradientType = new System.Windows.Forms.Label();
            this.pbWatermarkGradient1 = new System.Windows.Forms.PictureBox();
            this.lblWatermarkBackTrans = new System.Windows.Forms.Label();
            this.nudWatermarkBackTrans = new System.Windows.Forms.NumericUpDown();
            this.lblWatermarkBackColorsTip = new System.Windows.Forms.Label();
            this.btnSelectGradient = new System.Windows.Forms.Button();
            this.cboUseCustomGradient = new System.Windows.Forms.CheckBox();
            this.nudWatermarkCornerRadius = new System.Windows.Forms.NumericUpDown();
            this.lblWatermarkCornerRadiusTip = new System.Windows.Forms.Label();
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
            this.lblMaxNameLength = new System.Windows.Forms.Label();
            this.nudMaxNameLength = new System.Windows.Forms.NumericUpDown();
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
            this.cbGIFQuality = new System.Windows.Forms.ComboBox();
            this.lblGIFQuality = new System.Windows.Forms.Label();
            this.nudSwitchAfter = new System.Windows.Forms.NumericUpDown();
            this.nudImageQuality = new System.Windows.Forms.NumericUpDown();
            this.lblJPEGQualityPercentage = new System.Windows.Forms.Label();
            this.lblQuality = new System.Windows.Forms.Label();
            this.cboSwitchFormat = new System.Windows.Forms.ComboBox();
            this.lblFileFormat = new System.Windows.Forms.Label();
            this.cboFileFormat = new System.Windows.Forms.ComboBox();
            this.lblKB = new System.Windows.Forms.Label();
            this.lblAfter = new System.Windows.Forms.Label();
            this.lblSwitchTo = new System.Windows.Forms.Label();
            this.tpMainActions = new System.Windows.Forms.TabPage();
            this.chkPerformActions = new System.Windows.Forms.CheckBox();
            this.gbImageEditorSettings = new System.Windows.Forms.GroupBox();
            this.chkImageEditorAutoSave = new System.Windows.Forms.CheckBox();
            this.lbSoftware = new System.Windows.Forms.CheckedListBox();
            this.pgEditorsImage = new System.Windows.Forms.PropertyGrid();
            this.btnRemoveImageEditor = new System.Windows.Forms.Button();
            this.btnAddImageSoftware = new System.Windows.Forms.Button();
            this.tpImageHosting = new System.Windows.Forms.TabPage();
            this.tcImages = new System.Windows.Forms.TabControl();
            this.tpImageUploaders = new System.Windows.Forms.TabPage();
            this.gbImageUploadRetry = new System.Windows.Forms.GroupBox();
            this.chkImageUploadRandomRetryOnFail = new System.Windows.Forms.CheckBox();
            this.lblErrorRetry = new System.Windows.Forms.Label();
            this.lblUploadDurationLimit = new System.Windows.Forms.Label();
            this.chkImageUploadRetryOnFail = new System.Windows.Forms.CheckBox();
            this.cboImageUploadRetryOnTimeout = new System.Windows.Forms.CheckBox();
            this.nudUploadDurationLimit = new System.Windows.Forms.NumericUpDown();
            this.nudErrorRetry = new System.Windows.Forms.NumericUpDown();
            this.gbImageUploaderOptions = new System.Windows.Forms.GroupBox();
            this.chkAutoSwitchFileUploader = new System.Windows.Forms.CheckBox();
            this.cbTinyPicSizeCheck = new System.Windows.Forms.CheckBox();
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
            this.tpTreeGUI = new System.Windows.Forms.TabPage();
            this.pgIndexer = new System.Windows.Forms.PropertyGrid();
            this.tpTranslator = new System.Windows.Forms.TabPage();
            this.cbLanguageAutoDetect = new System.Windows.Forms.CheckBox();
            this.txtAutoTranslate = new System.Windows.Forms.TextBox();
            this.cbAutoTranslate = new System.Windows.Forms.CheckBox();
            this.btnTranslateTo1 = new System.Windows.Forms.Button();
            this.cbClipboardTranslate = new System.Windows.Forms.CheckBox();
            this.txtTranslateResult = new System.Windows.Forms.TextBox();
            this.txtLanguages = new System.Windows.Forms.TextBox();
            this.btnTranslate = new System.Windows.Forms.Button();
            this.txtTranslateText = new System.Windows.Forms.TextBox();
            this.lblToLanguage = new System.Windows.Forms.Label();
            this.lblFromLanguage = new System.Windows.Forms.Label();
            this.cbToLanguage = new System.Windows.Forms.ComboBox();
            this.cbFromLanguage = new System.Windows.Forms.ComboBox();
            this.tpOptions = new System.Windows.Forms.TabPage();
            this.tcOptions = new System.Windows.Forms.TabControl();
            this.tpGeneral = new System.Windows.Forms.TabPage();
            this.gbMonitorClipboard = new System.Windows.Forms.GroupBox();
            this.chkMonUrls = new System.Windows.Forms.CheckBox();
            this.chkMonFiles = new System.Windows.Forms.CheckBox();
            this.chkMonImages = new System.Windows.Forms.CheckBox();
            this.chkMonText = new System.Windows.Forms.CheckBox();
            this.gbUpdates = new System.Windows.Forms.GroupBox();
            this.chkCheckUpdatesBeta = new System.Windows.Forms.CheckBox();
            this.lblUpdateInfo = new System.Windows.Forms.Label();
            this.btnCheckUpdate = new System.Windows.Forms.Button();
            this.chkCheckUpdates = new System.Windows.Forms.CheckBox();
            this.gbMisc = new System.Windows.Forms.GroupBox();
            this.chkHotkeys = new System.Windows.Forms.CheckBox();
            this.chkShellExt = new System.Windows.Forms.CheckBox();
            this.chkWindows7TaskbarIntegration = new System.Windows.Forms.CheckBox();
            this.cbAutoSaveSettings = new System.Windows.Forms.CheckBox();
            this.cbShowHelpBalloonTips = new System.Windows.Forms.CheckBox();
            this.chkShowTaskbar = new System.Windows.Forms.CheckBox();
            this.chkOpenMainWindow = new System.Windows.Forms.CheckBox();
            this.chkStartWin = new System.Windows.Forms.CheckBox();
            this.tpProxy = new System.Windows.Forms.TabPage();
            this.gpProxySettings = new System.Windows.Forms.GroupBox();
            this.cboProxyConfig = new System.Windows.Forms.ComboBox();
            this.ucProxyAccounts = new ZScreenGUI.AccountsControl();
            this.tpInteraction = new System.Windows.Forms.TabPage();
            this.gbWindowButtons = new System.Windows.Forms.GroupBox();
            this.cboCloseButtonAction = new System.Windows.Forms.ComboBox();
            this.cboMinimizeButtonAction = new System.Windows.Forms.ComboBox();
            this.lblCloseButtonAction = new System.Windows.Forms.Label();
            this.lblMinimizeButtonAction = new System.Windows.Forms.Label();
            this.gbActionsToolbarSettings = new System.Windows.Forms.GroupBox();
            this.cbCloseQuickActions = new System.Windows.Forms.CheckBox();
            this.gbDropBox = new System.Windows.Forms.GroupBox();
            this.cbCloseDropBox = new System.Windows.Forms.CheckBox();
            this.gbAppearance = new System.Windows.Forms.GroupBox();
            this.cbCopyClipboardAfterTask = new System.Windows.Forms.CheckBox();
            this.chkTwitterEnable = new System.Windows.Forms.CheckBox();
            this.cbCompleteSound = new System.Windows.Forms.CheckBox();
            this.chkCaptureFallback = new System.Windows.Forms.CheckBox();
            this.cbShowUploadDuration = new System.Windows.Forms.CheckBox();
            this.chkBalloonTipOpenLink = new System.Windows.Forms.CheckBox();
            this.cbShowPopup = new System.Windows.Forms.CheckBox();
            this.lblTrayFlash = new System.Windows.Forms.Label();
            this.nudFlashIconCount = new System.Windows.Forms.NumericUpDown();
            this.tpAdvPaths = new System.Windows.Forms.TabPage();
            this.chkPreferSystemFolders = new System.Windows.Forms.CheckBox();
            this.gbRoot = new System.Windows.Forms.GroupBox();
            this.btnViewRootDir = new System.Windows.Forms.Button();
            this.btnBrowseRootDir = new System.Windows.Forms.Button();
            this.txtRootFolder = new System.Windows.Forms.TextBox();
            this.gbImages = new System.Windows.Forms.GroupBox();
            this.btnBrowseImagesDir = new System.Windows.Forms.Button();
            this.btnMoveImageFiles = new System.Windows.Forms.Button();
            this.lblImagesFolderPattern = new System.Windows.Forms.Label();
            this.lblImagesFolderPatternPreview = new System.Windows.Forms.Label();
            this.txtImagesFolderPattern = new System.Windows.Forms.TextBox();
            this.chkDeleteLocal = new System.Windows.Forms.CheckBox();
            this.btnViewImagesDir = new System.Windows.Forms.Button();
            this.txtImagesDir = new System.Windows.Forms.TextBox();
            this.gbSettingsExportImport = new System.Windows.Forms.GroupBox();
            this.btnSettingsDefault = new System.Windows.Forms.Button();
            this.btnSettingsExport = new System.Windows.Forms.Button();
            this.btnFTPExport = new System.Windows.Forms.Button();
            this.btnFTPImport = new System.Windows.Forms.Button();
            this.btnSettingsImport = new System.Windows.Forms.Button();
            this.gbCache = new System.Windows.Forms.GroupBox();
            this.btnViewCacheDir = new System.Windows.Forms.Button();
            this.lblCacheSize = new System.Windows.Forms.Label();
            this.lblMebibytes = new System.Windows.Forms.Label();
            this.nudCacheSize = new System.Windows.Forms.NumericUpDown();
            this.txtCacheDir = new System.Windows.Forms.TextBox();
            this.tpStats = new System.Windows.Forms.TabPage();
            this.btnOpenZScreenTester = new System.Windows.Forms.Button();
            this.gbStatistics = new System.Windows.Forms.GroupBox();
            this.btnDebugStart = new System.Windows.Forms.Button();
            this.rtbDebugInfo = new System.Windows.Forms.RichTextBox();
            this.gbLastSource = new System.Windows.Forms.GroupBox();
            this.btnOpenSourceString = new System.Windows.Forms.Button();
            this.btnOpenSourceText = new System.Windows.Forms.Button();
            this.btnOpenSourceBrowser = new System.Windows.Forms.Button();
            this.tpDebugLog = new System.Windows.Forms.TabPage();
            this.rtbDebugLog = new System.Windows.Forms.RichTextBox();
            this.tpOptionsAdv = new System.Windows.Forms.TabPage();
            this.pgApp = new System.Windows.Forms.PropertyGrid();
            this.tpHistory = new System.Windows.Forms.TabPage();
            this.label1 = new System.Windows.Forms.Label();
            this.btnHistoryOpen = new System.Windows.Forms.Button();
            this.cbHistorySave = new System.Windows.Forms.CheckBox();
            this.nudHistoryMaxItems = new System.Windows.Forms.NumericUpDown();
            this.cbAddFailedScreenshot = new System.Windows.Forms.CheckBox();
            this.tpUploadText = new System.Windows.Forms.TabPage();
            this.txtTextUploaderContent = new System.Windows.Forms.TextBox();
            this.btnUploadText = new System.Windows.Forms.Button();
            this.btnUploadTextClipboard = new System.Windows.Forms.Button();
            this.btnUploadTextClipboardFile = new System.Windows.Forms.Button();
            this.ttZScreen = new System.Windows.Forms.ToolTip(this.components);
            this.cmTray.SuspendLayout();
            this.tcApp.SuspendLayout();
            this.tpMain.SuspendLayout();
            this.gbImageSettings.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbLogo)).BeginInit();
            this.tpDestinations.SuspendLayout();
            this.tcDestinations.SuspendLayout();
            this.tpDestFTP.SuspendLayout();
            this.gbFTPSettings.SuspendLayout();
            this.tpDestDropbox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbDropboxLogo)).BeginInit();
            this.tpDestLocalhost.SuspendLayout();
            this.tpDestRapidShare.SuspendLayout();
            this.tpDestSendSpace.SuspendLayout();
            this.tpDestFlickr.SuspendLayout();
            this.tpDestImageShack.SuspendLayout();
            this.gbImageShack.SuspendLayout();
            this.tpDestImgur.SuspendLayout();
            this.gbImgurUserAccount.SuspendLayout();
            this.tpDestImageBam.SuspendLayout();
            this.gbImageBamGalleries.SuspendLayout();
            this.gbImageBamLinks.SuspendLayout();
            this.gbImageBamApiKeys.SuspendLayout();
            this.tpDestTinyPic.SuspendLayout();
            this.gbTinyPic.SuspendLayout();
            this.tpDestTwitter.SuspendLayout();
            this.tlpTwitter.SuspendLayout();
            this.panelTwitter.SuspendLayout();
            this.gbTwitterOthers.SuspendLayout();
            this.tpDestMindTouch.SuspendLayout();
            this.gbMindTouchOptions.SuspendLayout();
            this.tpDestMediaWiki.SuspendLayout();
            this.tpDestCustom.SuspendLayout();
            this.gbImageUploaders.SuspendLayout();
            this.gbRegexp.SuspendLayout();
            this.gbArguments.SuspendLayout();
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
            this.tpActivewindow.SuspendLayout();
            this.tpFreehandCropShot.SuspendLayout();
            this.tpWatermark.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbWatermarkShow)).BeginInit();
            this.gbWatermarkGeneral.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudWatermarkOffset)).BeginInit();
            this.tcWatermark.SuspendLayout();
            this.tpWatermarkText.SuspendLayout();
            this.gbWatermarkBackground.SuspendLayout();
            this.gbGradientMakerBasic.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackWatermarkBackgroundTrans)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbWatermarkGradient2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbWatermarkBorderColor)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbWatermarkGradient1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudWatermarkBackTrans)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudWatermarkCornerRadius)).BeginInit();
            this.gbWatermarkText.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackWatermarkFontTrans)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudWatermarkFontTrans)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbWatermarkFontColor)).BeginInit();
            this.tpWatermarkImage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudWatermarkImageScale)).BeginInit();
            this.tpFileNaming.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudMaxNameLength)).BeginInit();
            this.gbOthersNaming.SuspendLayout();
            this.gbCodeTitle.SuspendLayout();
            this.gbActiveWindowNaming.SuspendLayout();
            this.tpCaptureQuality.SuspendLayout();
            this.gbImageSize.SuspendLayout();
            this.gbPictureQuality.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudSwitchAfter)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudImageQuality)).BeginInit();
            this.tpMainActions.SuspendLayout();
            this.gbImageEditorSettings.SuspendLayout();
            this.tpImageHosting.SuspendLayout();
            this.tcImages.SuspendLayout();
            this.tpImageUploaders.SuspendLayout();
            this.gbImageUploadRetry.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudUploadDurationLimit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudErrorRetry)).BeginInit();
            this.gbImageUploaderOptions.SuspendLayout();
            this.tpWebPageUpload.SuspendLayout();
            this.pWebPageImage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbWebPageImage)).BeginInit();
            this.tpTextServices.SuspendLayout();
            this.tcTextUploaders.SuspendLayout();
            this.tpTreeGUI.SuspendLayout();
            this.tpTranslator.SuspendLayout();
            this.tpOptions.SuspendLayout();
            this.tcOptions.SuspendLayout();
            this.tpGeneral.SuspendLayout();
            this.gbMonitorClipboard.SuspendLayout();
            this.gbUpdates.SuspendLayout();
            this.gbMisc.SuspendLayout();
            this.tpProxy.SuspendLayout();
            this.gpProxySettings.SuspendLayout();
            this.tpInteraction.SuspendLayout();
            this.gbWindowButtons.SuspendLayout();
            this.gbActionsToolbarSettings.SuspendLayout();
            this.gbDropBox.SuspendLayout();
            this.gbAppearance.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudFlashIconCount)).BeginInit();
            this.tpAdvPaths.SuspendLayout();
            this.gbRoot.SuspendLayout();
            this.gbImages.SuspendLayout();
            this.gbSettingsExportImport.SuspendLayout();
            this.gbCache.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudCacheSize)).BeginInit();
            this.tpStats.SuspendLayout();
            this.gbStatistics.SuspendLayout();
            this.gbLastSource.SuspendLayout();
            this.tpDebugLog.SuspendLayout();
            this.tpOptionsAdv.SuspendLayout();
            this.tpHistory.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudHistoryMaxItems)).BeginInit();
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
            this.tsmFileDest,
            this.tsmEditinImageSoftware,
            this.tsmCopytoClipboardMode,
            this.toolStripSeparator3,
            this.historyToolStripMenuItem,
            this.tsmFTPClient,
            this.tsmViewLocalDirectory,
            this.toolStripSeparator1,
            this.tsmActions,
            this.tsmQuickActions,
            this.tsmQuickOptions,
            this.toolStripSeparator7,
            this.tsmHelp,
            this.tsmExitZScreen});
            this.cmTray.Name = "cmTray";
            this.cmTray.Size = new System.Drawing.Size(206, 314);
            // 
            // tsmiTabs
            // 
            this.tsmiTabs.DoubleClickEnabled = true;
            this.tsmiTabs.Image = global::ZScreenGUI.Properties.Resources.wrench;
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
            this.tsmImageDest.Image = global::ZScreenGUI.Properties.Resources.picture_go;
            this.tsmImageDest.Name = "tsmImageDest";
            this.tsmImageDest.Size = new System.Drawing.Size(205, 22);
            this.tsmImageDest.Text = "Send Image To";
            // 
            // tsmFileDest
            // 
            this.tsmFileDest.Image = global::ZScreenGUI.Properties.Resources.application_go;
            this.tsmFileDest.Name = "tsmFileDest";
            this.tsmFileDest.Size = new System.Drawing.Size(205, 22);
            this.tsmFileDest.Text = "Send File To";
            // 
            // tsmEditinImageSoftware
            // 
            this.tsmEditinImageSoftware.CheckOnClick = true;
            this.tsmEditinImageSoftware.Image = global::ZScreenGUI.Properties.Resources.picture_edit;
            this.tsmEditinImageSoftware.Name = "tsmEditinImageSoftware";
            this.tsmEditinImageSoftware.Size = new System.Drawing.Size(205, 22);
            this.tsmEditinImageSoftware.Text = "Perform Custom Actions";
            this.tsmEditinImageSoftware.CheckedChanged += new System.EventHandler(this.tsmEditinImageSoftware_CheckedChanged);
            // 
            // tsmCopytoClipboardMode
            // 
            this.tsmCopytoClipboardMode.Image = global::ZScreenGUI.Properties.Resources.page_copy;
            this.tsmCopytoClipboardMode.Name = "tsmCopytoClipboardMode";
            this.tsmCopytoClipboardMode.Size = new System.Drawing.Size(205, 22);
            this.tsmCopytoClipboardMode.Text = "URL Format";
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(202, 6);
            // 
            // historyToolStripMenuItem
            // 
            this.historyToolStripMenuItem.Image = global::ZScreenGUI.Properties.Resources.pictures;
            this.historyToolStripMenuItem.Name = "historyToolStripMenuItem";
            this.historyToolStripMenuItem.Size = new System.Drawing.Size(205, 22);
            this.historyToolStripMenuItem.Text = "&History...";
            this.historyToolStripMenuItem.Click += new System.EventHandler(this.historyToolStripMenuItem_Click);
            // 
            // tsmFTPClient
            // 
            this.tsmFTPClient.Image = global::ZScreenGUI.Properties.Resources.server_edit;
            this.tsmFTPClient.Name = "tsmFTPClient";
            this.tsmFTPClient.Size = new System.Drawing.Size(205, 22);
            this.tsmFTPClient.Text = "FTP &Client...";
            this.tsmFTPClient.Click += new System.EventHandler(this.tsmFTPClient_Click);
            // 
            // tsmViewLocalDirectory
            // 
            this.tsmViewLocalDirectory.Image = global::ZScreenGUI.Properties.Resources.folder_picture;
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
            this.tsmActions.Image = global::ZScreenGUI.Properties.Resources.lightning;
            this.tsmActions.Name = "tsmActions";
            this.tsmActions.Size = new System.Drawing.Size(205, 22);
            this.tsmActions.Text = "Quick &Actions";
            // 
            // tsmEntireScreen
            // 
            this.tsmEntireScreen.Image = global::ZScreenGUI.Properties.Resources.monitor;
            this.tsmEntireScreen.Name = "tsmEntireScreen";
            this.tsmEntireScreen.Size = new System.Drawing.Size(197, 22);
            this.tsmEntireScreen.Text = "Entire Screen";
            this.tsmEntireScreen.Click += new System.EventHandler(this.entireScreenToolStripMenuItem_Click);
            // 
            // tsmSelectedWindow
            // 
            this.tsmSelectedWindow.Image = global::ZScreenGUI.Properties.Resources.application_double;
            this.tsmSelectedWindow.Name = "tsmSelectedWindow";
            this.tsmSelectedWindow.Size = new System.Drawing.Size(197, 22);
            this.tsmSelectedWindow.Text = "Selected Window...";
            this.tsmSelectedWindow.Click += new System.EventHandler(this.selectedWindowToolStripMenuItem_Click);
            // 
            // tsmCropShot
            // 
            this.tsmCropShot.Image = global::ZScreenGUI.Properties.Resources.shape_square;
            this.tsmCropShot.Name = "tsmCropShot";
            this.tsmCropShot.Size = new System.Drawing.Size(197, 22);
            this.tsmCropShot.Text = "Crop Shot...";
            this.tsmCropShot.Click += new System.EventHandler(this.rectangularRegionToolStripMenuItem_Click);
            // 
            // tsmLastCropShot
            // 
            this.tsmLastCropShot.Image = global::ZScreenGUI.Properties.Resources.shape_square_go;
            this.tsmLastCropShot.Name = "tsmLastCropShot";
            this.tsmLastCropShot.Size = new System.Drawing.Size(197, 22);
            this.tsmLastCropShot.Text = "Last Crop Shot";
            this.tsmLastCropShot.Click += new System.EventHandler(this.lastRectangularRegionToolStripMenuItem_Click);
            // 
            // autoScreenshotsToolStripMenuItem
            // 
            this.autoScreenshotsToolStripMenuItem.Image = global::ZScreenGUI.Properties.Resources.images_stack;
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
            this.tsmClipboardUpload.Image = global::ZScreenGUI.Properties.Resources.images;
            this.tsmClipboardUpload.Name = "tsmClipboardUpload";
            this.tsmClipboardUpload.Size = new System.Drawing.Size(197, 22);
            this.tsmClipboardUpload.Text = "Clipboard Upload";
            this.tsmClipboardUpload.Click += new System.EventHandler(this.tsmUploadFromClipboard_Click);
            // 
            // tsmDragDropWindow
            // 
            this.tsmDragDropWindow.Image = global::ZScreenGUI.Properties.Resources.shape_move_backwards;
            this.tsmDragDropWindow.Name = "tsmDragDropWindow";
            this.tsmDragDropWindow.Size = new System.Drawing.Size(197, 22);
            this.tsmDragDropWindow.Text = "Drag && Drop Window...";
            this.tsmDragDropWindow.Click += new System.EventHandler(this.tsmDropWindow_Click);
            // 
            // tsmLanguageTranslator
            // 
            this.tsmLanguageTranslator.Image = global::ZScreenGUI.Properties.Resources.comments;
            this.tsmLanguageTranslator.Name = "tsmLanguageTranslator";
            this.tsmLanguageTranslator.Size = new System.Drawing.Size(197, 22);
            this.tsmLanguageTranslator.Text = "Language Translator";
            this.tsmLanguageTranslator.Click += new System.EventHandler(this.languageTranslatorToolStripMenuItem_Click);
            // 
            // tsmScreenColorPicker
            // 
            this.tsmScreenColorPicker.Image = global::ZScreenGUI.Properties.Resources.color_wheel;
            this.tsmScreenColorPicker.Name = "tsmScreenColorPicker";
            this.tsmScreenColorPicker.Size = new System.Drawing.Size(197, 22);
            this.tsmScreenColorPicker.Text = "Screen Color Picker...";
            this.tsmScreenColorPicker.Click += new System.EventHandler(this.screenColorPickerToolStripMenuItem_Click);
            // 
            // tsmQuickActions
            // 
            this.tsmQuickActions.Image = global::ZScreenGUI.Properties.Resources.application_lightning;
            this.tsmQuickActions.Name = "tsmQuickActions";
            this.tsmQuickActions.Size = new System.Drawing.Size(205, 22);
            this.tsmQuickActions.Text = "Actions Toolbar...";
            this.tsmQuickActions.Click += new System.EventHandler(this.tsmQuickActions_Click);
            // 
            // tsmQuickOptions
            // 
            this.tsmQuickOptions.Image = global::ZScreenGUI.Properties.Resources.application_edit;
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
            this.tsmHelp.Image = global::ZScreenGUI.Properties.Resources.help;
            this.tsmHelp.Name = "tsmHelp";
            this.tsmHelp.Size = new System.Drawing.Size(205, 22);
            this.tsmHelp.Text = "&Help";
            // 
            // tsmLicense
            // 
            this.tsmLicense.Image = global::ZScreenGUI.Properties.Resources.note_error;
            this.tsmLicense.Name = "tsmLicense";
            this.tsmLicense.Size = new System.Drawing.Size(163, 22);
            this.tsmLicense.Text = "License...";
            this.tsmLicense.Click += new System.EventHandler(this.tsmLic_Click);
            // 
            // tsmVersionHistory
            // 
            this.tsmVersionHistory.Image = global::ZScreenGUI.Properties.Resources.page_white_text;
            this.tsmVersionHistory.Name = "tsmVersionHistory";
            this.tsmVersionHistory.Size = new System.Drawing.Size(163, 22);
            this.tsmVersionHistory.Text = "&Version History...";
            this.tsmVersionHistory.Click += new System.EventHandler(this.cmVersionHistory_Click);
            // 
            // tsmAbout
            // 
            this.tsmAbout.Image = global::ZScreenGUI.Properties.Resources.information;
            this.tsmAbout.Name = "tsmAbout";
            this.tsmAbout.Size = new System.Drawing.Size(163, 22);
            this.tsmAbout.Text = "About...";
            this.tsmAbout.Click += new System.EventHandler(this.tsmAboutMain_Click);
            // 
            // tsmExitZScreen
            // 
            this.tsmExitZScreen.Image = global::ZScreenGUI.Properties.Resources.cross;
            this.tsmExitZScreen.Name = "tsmExitZScreen";
            this.tsmExitZScreen.Size = new System.Drawing.Size(205, 22);
            this.tsmExitZScreen.Text = "Exit ZScreen";
            this.tsmExitZScreen.Click += new System.EventHandler(this.exitZScreenToolStripMenuItem_Click);
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
            this.tcApp.Controls.Add(this.tpMainActions);
            this.tcApp.Controls.Add(this.tpImageHosting);
            this.tcApp.Controls.Add(this.tpTextServices);
            this.tcApp.Controls.Add(this.tpTranslator);
            this.tcApp.Controls.Add(this.tpHistory);
            this.tcApp.Controls.Add(this.tpOptions);
            this.tcApp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcApp.Location = new System.Drawing.Point(2, 2);
            this.tcApp.Name = "tcApp";
            this.tcApp.SelectedIndex = 0;
            this.tcApp.Size = new System.Drawing.Size(813, 462);
            this.tcApp.TabIndex = 74;
            this.tcApp.SelectedIndexChanged += new System.EventHandler(this.tcApp_SelectedIndexChanged);
            // 
            // tpMain
            // 
            this.tpMain.AllowDrop = true;
            this.tpMain.Controls.Add(this.ucDestOptions);
            this.tpMain.Controls.Add(this.gbImageSettings);
            this.tpMain.Controls.Add(this.llProjectPage);
            this.tpMain.Controls.Add(this.llWebsite);
            this.tpMain.Controls.Add(this.llblBugReports);
            this.tpMain.Controls.Add(this.lblLogo);
            this.tpMain.Controls.Add(this.pbLogo);
            this.tpMain.ImageKey = "(none)";
            this.tpMain.Location = new System.Drawing.Point(4, 22);
            this.tpMain.Name = "tpMain";
            this.tpMain.Padding = new System.Windows.Forms.Padding(3);
            this.tpMain.Size = new System.Drawing.Size(805, 436);
            this.tpMain.TabIndex = 0;
            this.tpMain.Text = "Main";
            this.tpMain.UseVisualStyleBackColor = true;
            // 
            // ucDestOptions
            // 
            this.ucDestOptions.Location = new System.Drawing.Point(40, 56);
            this.ucDestOptions.Margin = new System.Windows.Forms.Padding(4);
            this.ucDestOptions.MaximumSize = new System.Drawing.Size(378, 145);
            this.ucDestOptions.Name = "ucDestOptions";
            this.ucDestOptions.Size = new System.Drawing.Size(378, 145);
            this.ucDestOptions.TabIndex = 124;
            this.ttZScreen.SetToolTip(this.ucDestOptions, "To configure destination options go to Destinations tab");
            // 
            // gbImageSettings
            // 
            this.gbImageSettings.Controls.Add(this.lblScreenshotDelay);
            this.gbImageSettings.Controls.Add(this.nudScreenshotDelay);
            this.gbImageSettings.Controls.Add(this.lblCopytoClipboard);
            this.gbImageSettings.Controls.Add(this.cboClipboardTextMode);
            this.gbImageSettings.Controls.Add(this.chkShowCursor);
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
            // nudScreenshotDelay
            // 
            this.nudScreenshotDelay.Location = new System.Drawing.Point(112, 18);
            this.nudScreenshotDelay.Margin = new System.Windows.Forms.Padding(4);
            this.nudScreenshotDelay.Name = "nudScreenshotDelay";
            this.nudScreenshotDelay.RealValue = ((long)(0));
            this.nudScreenshotDelay.Size = new System.Drawing.Size(234, 24);
            this.nudScreenshotDelay.TabIndex = 121;
            this.nudScreenshotDelay.Tag = "Test";
            this.nudScreenshotDelay.Time = ZScreenLib.Times.Milliseconds;
            this.ttZScreen.SetToolTip(this.nudScreenshotDelay, "Specify the amount of time to wait before taking a screenshot.");
            this.nudScreenshotDelay.Value = ((long)(0));
            this.nudScreenshotDelay.ValueChanged += new System.EventHandler(this.numericUpDownTimer1_ValueChanged);
            this.nudScreenshotDelay.SelectedIndexChanged += new System.EventHandler(this.nudtScreenshotDelay_SelectedIndexChanged);
            this.nudScreenshotDelay.MouseHover += new System.EventHandler(this.nudtScreenshotDelay_MouseHover);
            // 
            // lblCopytoClipboard
            // 
            this.lblCopytoClipboard.AutoSize = true;
            this.lblCopytoClipboard.Location = new System.Drawing.Point(43, 52);
            this.lblCopytoClipboard.Name = "lblCopytoClipboard";
            this.lblCopytoClipboard.Size = new System.Drawing.Size(67, 13);
            this.lblCopytoClipboard.TabIndex = 117;
            this.lblCopytoClipboard.Text = "URL Format:";
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
            // chkShowCursor
            // 
            this.chkShowCursor.AutoSize = true;
            this.chkShowCursor.Location = new System.Drawing.Point(16, 111);
            this.chkShowCursor.Name = "chkShowCursor";
            this.chkShowCursor.Size = new System.Drawing.Size(159, 17);
            this.chkShowCursor.TabIndex = 8;
            this.chkShowCursor.Text = "Show Cursor in Screenshots";
            this.ttZScreen.SetToolTip(this.chkShowCursor, "When enabled your mouse cursor icon will be captured \r\nas it appeared when the sc" +
        "reenshot was taken.");
            this.chkShowCursor.UseVisualStyleBackColor = true;
            this.chkShowCursor.CheckedChanged += new System.EventHandler(this.cbShowCursor_CheckedChanged);
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
            this.llProjectPage.Size = new System.Drawing.Size(61, 13);
            this.llProjectPage.TabIndex = 83;
            this.llProjectPage.TabStop = true;
            this.llProjectPage.Text = "Wiki Pages";
            this.ttZScreen.SetToolTip(this.llProjectPage, "View ZScreen\'s project page on the web.");
            this.llProjectPage.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llProjectPage_LinkClicked);
            // 
            // llWebsite
            // 
            this.llWebsite.AutoSize = true;
            this.llWebsite.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline;
            this.llWebsite.Location = new System.Drawing.Point(696, 360);
            this.llWebsite.Name = "llWebsite";
            this.llWebsite.Size = new System.Drawing.Size(66, 13);
            this.llWebsite.TabIndex = 82;
            this.llWebsite.TabStop = true;
            this.llWebsite.Text = "ZScreen.net";
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
            this.lblLogo.Click += new System.EventHandler(this.lblLogo_Click);
            this.lblLogo.MouseEnter += new System.EventHandler(this.lblLogo_MouseEnter);
            this.lblLogo.MouseLeave += new System.EventHandler(this.lblLogo_MouseLeave);
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
            this.pbLogo.MouseEnter += new System.EventHandler(this.pbLogo_MouseEnter);
            this.pbLogo.MouseLeave += new System.EventHandler(this.pbLogo_MouseLeave);
            // 
            // tpDestinations
            // 
            this.tpDestinations.Controls.Add(this.tcDestinations);
            this.tpDestinations.ImageKey = "(none)";
            this.tpDestinations.Location = new System.Drawing.Point(4, 22);
            this.tpDestinations.Name = "tpDestinations";
            this.tpDestinations.Padding = new System.Windows.Forms.Padding(3);
            this.tpDestinations.Size = new System.Drawing.Size(805, 436);
            this.tpDestinations.TabIndex = 12;
            this.tpDestinations.Text = "Destinations";
            this.tpDestinations.UseVisualStyleBackColor = true;
            // 
            // tcDestinations
            // 
            this.tcDestinations.Controls.Add(this.tpDestFTP);
            this.tcDestinations.Controls.Add(this.tpDestDropbox);
            this.tcDestinations.Controls.Add(this.tpDestLocalhost);
            this.tcDestinations.Controls.Add(this.tpDestRapidShare);
            this.tcDestinations.Controls.Add(this.tpDestSendSpace);
            this.tcDestinations.Controls.Add(this.tpDestFlickr);
            this.tcDestinations.Controls.Add(this.tpDestImageShack);
            this.tcDestinations.Controls.Add(this.tpDestImgur);
            this.tcDestinations.Controls.Add(this.tpDestImageBam);
            this.tcDestinations.Controls.Add(this.tpDestTinyPic);
            this.tcDestinations.Controls.Add(this.tpDestTwitter);
            this.tcDestinations.Controls.Add(this.tpDestMindTouch);
            this.tcDestinations.Controls.Add(this.tpDestMediaWiki);
            this.tcDestinations.Controls.Add(this.tpDestCustom);
            this.tcDestinations.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcDestinations.Location = new System.Drawing.Point(3, 3);
            this.tcDestinations.Name = "tcDestinations";
            this.tcDestinations.SelectedIndex = 0;
            this.tcDestinations.Size = new System.Drawing.Size(799, 430);
            this.tcDestinations.TabIndex = 0;
            // 
            // tpDestFTP
            // 
            this.tpDestFTP.BackColor = System.Drawing.SystemColors.Window;
            this.tpDestFTP.Controls.Add(this.btnFtpHelp);
            this.tpDestFTP.Controls.Add(this.btnFTPOpenClient);
            this.tpDestFTP.Controls.Add(this.ucFTPAccounts);
            this.tpDestFTP.Controls.Add(this.gbFTPSettings);
            this.tpDestFTP.Location = new System.Drawing.Point(4, 22);
            this.tpDestFTP.Name = "tpDestFTP";
            this.tpDestFTP.Padding = new System.Windows.Forms.Padding(3);
            this.tpDestFTP.Size = new System.Drawing.Size(791, 404);
            this.tpDestFTP.TabIndex = 5;
            this.tpDestFTP.Text = "FTP";
            // 
            // btnFtpHelp
            // 
            this.btnFtpHelp.Location = new System.Drawing.Point(312, 11);
            this.btnFtpHelp.Name = "btnFtpHelp";
            this.btnFtpHelp.Size = new System.Drawing.Size(64, 24);
            this.btnFtpHelp.TabIndex = 75;
            this.btnFtpHelp.Text = "&Help...";
            this.btnFtpHelp.UseVisualStyleBackColor = true;
            this.btnFtpHelp.Click += new System.EventHandler(this.btnFtpHelp_Click);
            // 
            // btnFTPOpenClient
            // 
            this.btnFTPOpenClient.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnFTPOpenClient.Location = new System.Drawing.Point(649, 11);
            this.btnFTPOpenClient.Name = "btnFTPOpenClient";
            this.btnFTPOpenClient.Size = new System.Drawing.Size(128, 24);
            this.btnFTPOpenClient.TabIndex = 116;
            this.btnFTPOpenClient.Text = "Open FTP &Client...";
            this.btnFTPOpenClient.UseVisualStyleBackColor = true;
            this.btnFTPOpenClient.Click += new System.EventHandler(this.btnFTPOpenClient_Click);
            // 
            // ucFTPAccounts
            // 
            this.ucFTPAccounts.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ucFTPAccounts.Location = new System.Drawing.Point(3, 3);
            this.ucFTPAccounts.Margin = new System.Windows.Forms.Padding(4);
            this.ucFTPAccounts.Name = "ucFTPAccounts";
            this.ucFTPAccounts.Size = new System.Drawing.Size(787, 303);
            this.ucFTPAccounts.TabIndex = 0;
            // 
            // gbFTPSettings
            // 
            this.gbFTPSettings.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbFTPSettings.Controls.Add(this.lblFtpFiles);
            this.gbFTPSettings.Controls.Add(this.lblFtpText);
            this.gbFTPSettings.Controls.Add(this.lblFtpImages);
            this.gbFTPSettings.Controls.Add(this.cboFtpFiles);
            this.gbFTPSettings.Controls.Add(this.cboFtpText);
            this.gbFTPSettings.Controls.Add(this.cboFtpImages);
            this.gbFTPSettings.Controls.Add(this.cbFTPThumbnailCheckSize);
            this.gbFTPSettings.Controls.Add(this.lblFTPThumbWidth);
            this.gbFTPSettings.Controls.Add(this.txtFTPThumbWidth);
            this.gbFTPSettings.Location = new System.Drawing.Point(8, 304);
            this.gbFTPSettings.Name = "gbFTPSettings";
            this.gbFTPSettings.Size = new System.Drawing.Size(759, 94);
            this.gbFTPSettings.TabIndex = 115;
            this.gbFTPSettings.TabStop = false;
            this.gbFTPSettings.Text = "FTP Settings";
            // 
            // lblFtpFiles
            // 
            this.lblFtpFiles.AutoSize = true;
            this.lblFtpFiles.Location = new System.Drawing.Point(432, 70);
            this.lblFtpFiles.Name = "lblFtpFiles";
            this.lblFtpFiles.Size = new System.Drawing.Size(28, 13);
            this.lblFtpFiles.TabIndex = 136;
            this.lblFtpFiles.Text = "Files";
            // 
            // lblFtpText
            // 
            this.lblFtpText.AutoSize = true;
            this.lblFtpText.Location = new System.Drawing.Point(432, 44);
            this.lblFtpText.Name = "lblFtpText";
            this.lblFtpText.Size = new System.Drawing.Size(28, 13);
            this.lblFtpText.TabIndex = 135;
            this.lblFtpText.Text = "Text";
            // 
            // lblFtpImages
            // 
            this.lblFtpImages.AutoSize = true;
            this.lblFtpImages.Location = new System.Drawing.Point(419, 19);
            this.lblFtpImages.Name = "lblFtpImages";
            this.lblFtpImages.Size = new System.Drawing.Size(41, 13);
            this.lblFtpImages.TabIndex = 134;
            this.lblFtpImages.Text = "Images";
            // 
            // cboFtpFiles
            // 
            this.cboFtpFiles.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboFtpFiles.FormattingEnabled = true;
            this.cboFtpFiles.Location = new System.Drawing.Point(472, 64);
            this.cboFtpFiles.Name = "cboFtpFiles";
            this.cboFtpFiles.Size = new System.Drawing.Size(272, 21);
            this.cboFtpFiles.TabIndex = 133;
            this.cboFtpFiles.SelectedIndexChanged += new System.EventHandler(this.cboFtpFiles_SelectedIndexChanged);
            // 
            // cboFtpText
            // 
            this.cboFtpText.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboFtpText.FormattingEnabled = true;
            this.cboFtpText.Location = new System.Drawing.Point(472, 40);
            this.cboFtpText.Name = "cboFtpText";
            this.cboFtpText.Size = new System.Drawing.Size(272, 21);
            this.cboFtpText.TabIndex = 132;
            this.cboFtpText.SelectedIndexChanged += new System.EventHandler(this.cboFtpText_SelectedIndexChanged);
            // 
            // cboFtpImages
            // 
            this.cboFtpImages.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboFtpImages.FormattingEnabled = true;
            this.cboFtpImages.Location = new System.Drawing.Point(472, 16);
            this.cboFtpImages.Name = "cboFtpImages";
            this.cboFtpImages.Size = new System.Drawing.Size(272, 21);
            this.cboFtpImages.TabIndex = 117;
            this.cboFtpImages.SelectedIndexChanged += new System.EventHandler(this.cboFtpImages_SelectedIndexChanged);
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
            // lblFTPThumbWidth
            // 
            this.lblFTPThumbWidth.AutoSize = true;
            this.lblFTPThumbWidth.Location = new System.Drawing.Point(16, 25);
            this.lblFTPThumbWidth.Name = "lblFTPThumbWidth";
            this.lblFTPThumbWidth.Size = new System.Drawing.Size(107, 13);
            this.lblFTPThumbWidth.TabIndex = 129;
            this.lblFTPThumbWidth.Text = "Thumbnail width (px):";
            // 
            // txtFTPThumbWidth
            // 
            this.txtFTPThumbWidth.Location = new System.Drawing.Point(128, 22);
            this.txtFTPThumbWidth.Name = "txtFTPThumbWidth";
            this.txtFTPThumbWidth.Size = new System.Drawing.Size(40, 20);
            this.txtFTPThumbWidth.TabIndex = 127;
            this.txtFTPThumbWidth.Text = "2500";
            this.txtFTPThumbWidth.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtFTPThumbWidth.TextChanged += new System.EventHandler(this.txtFTPThumbWidth_TextChanged);
            // 
            // tpDestDropbox
            // 
            this.tpDestDropbox.BackColor = System.Drawing.SystemColors.Window;
            this.tpDestDropbox.Controls.Add(this.lblDropboxPasswordTip);
            this.tpDestDropbox.Controls.Add(this.pbDropboxLogo);
            this.tpDestDropbox.Controls.Add(this.lblDropboxLoginTip);
            this.tpDestDropbox.Controls.Add(this.btnDropboxRegister);
            this.tpDestDropbox.Controls.Add(this.lblDropboxStatus);
            this.tpDestDropbox.Controls.Add(this.lblDropboxPathTip);
            this.tpDestDropbox.Controls.Add(this.lblDropboxPath);
            this.tpDestDropbox.Controls.Add(this.lblDropboxPassword);
            this.tpDestDropbox.Controls.Add(this.lblDropboxEmail);
            this.tpDestDropbox.Controls.Add(this.btnDropboxLogin);
            this.tpDestDropbox.Controls.Add(this.txtDropboxPath);
            this.tpDestDropbox.Controls.Add(this.txtDropboxPassword);
            this.tpDestDropbox.Controls.Add(this.txtDropboxEmail);
            this.tpDestDropbox.Location = new System.Drawing.Point(4, 23);
            this.tpDestDropbox.Name = "tpDestDropbox";
            this.tpDestDropbox.Padding = new System.Windows.Forms.Padding(3);
            this.tpDestDropbox.Size = new System.Drawing.Size(791, 402);
            this.tpDestDropbox.TabIndex = 14;
            this.tpDestDropbox.Text = "Dropbox";
            // 
            // lblDropboxPasswordTip
            // 
            this.lblDropboxPasswordTip.AutoSize = true;
            this.lblDropboxPasswordTip.Location = new System.Drawing.Point(344, 128);
            this.lblDropboxPasswordTip.Name = "lblDropboxPasswordTip";
            this.lblDropboxPasswordTip.Size = new System.Drawing.Size(131, 13);
            this.lblDropboxPasswordTip.TabIndex = 12;
            this.lblDropboxPasswordTip.Text = "Password won\'t be saved.";
            // 
            // pbDropboxLogo
            // 
            this.pbDropboxLogo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pbDropboxLogo.Image = global::ZScreenGUI.Properties.Resources.DropboxLogo;
            this.pbDropboxLogo.Location = new System.Drawing.Point(16, 16);
            this.pbDropboxLogo.Name = "pbDropboxLogo";
            this.pbDropboxLogo.Size = new System.Drawing.Size(248, 64);
            this.pbDropboxLogo.TabIndex = 11;
            this.pbDropboxLogo.TabStop = false;
            this.pbDropboxLogo.Click += new System.EventHandler(this.pbDropboxLogo_Click);
            // 
            // lblDropboxLoginTip
            // 
            this.lblDropboxLoginTip.AutoSize = true;
            this.lblDropboxLoginTip.Location = new System.Drawing.Point(200, 196);
            this.lblDropboxLoginTip.Name = "lblDropboxLoginTip";
            this.lblDropboxLoginTip.Size = new System.Drawing.Size(152, 13);
            this.lblDropboxLoginTip.TabIndex = 10;
            this.lblDropboxLoginTip.Text = "Login is only one time required.";
            // 
            // btnDropboxRegister
            // 
            this.btnDropboxRegister.Location = new System.Drawing.Point(104, 190);
            this.btnDropboxRegister.Name = "btnDropboxRegister";
            this.btnDropboxRegister.Size = new System.Drawing.Size(80, 24);
            this.btnDropboxRegister.TabIndex = 4;
            this.btnDropboxRegister.Text = "Register...";
            this.btnDropboxRegister.UseVisualStyleBackColor = true;
            this.btnDropboxRegister.Click += new System.EventHandler(this.btnDropboxRegister_Click);
            // 
            // lblDropboxStatus
            // 
            this.lblDropboxStatus.AutoSize = true;
            this.lblDropboxStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblDropboxStatus.Location = new System.Drawing.Point(16, 228);
            this.lblDropboxStatus.Name = "lblDropboxStatus";
            this.lblDropboxStatus.Size = new System.Drawing.Size(82, 16);
            this.lblDropboxStatus.TabIndex = 8;
            this.lblDropboxStatus.Text = "Login status:";
            // 
            // lblDropboxPathTip
            // 
            this.lblDropboxPathTip.AutoSize = true;
            this.lblDropboxPathTip.Location = new System.Drawing.Point(344, 160);
            this.lblDropboxPathTip.Name = "lblDropboxPathTip";
            this.lblDropboxPathTip.Size = new System.Drawing.Size(208, 13);
            this.lblDropboxPathTip.TabIndex = 7;
            this.lblDropboxPathTip.Text = "Use \"Public\" folder for be able to get URL.";
            // 
            // lblDropboxPath
            // 
            this.lblDropboxPath.AutoSize = true;
            this.lblDropboxPath.Location = new System.Drawing.Point(16, 160);
            this.lblDropboxPath.Name = "lblDropboxPath";
            this.lblDropboxPath.Size = new System.Drawing.Size(68, 13);
            this.lblDropboxPath.TabIndex = 6;
            this.lblDropboxPath.Text = "Upload path:";
            // 
            // lblDropboxPassword
            // 
            this.lblDropboxPassword.AutoSize = true;
            this.lblDropboxPassword.Location = new System.Drawing.Point(16, 128);
            this.lblDropboxPassword.Name = "lblDropboxPassword";
            this.lblDropboxPassword.Size = new System.Drawing.Size(56, 13);
            this.lblDropboxPassword.TabIndex = 5;
            this.lblDropboxPassword.Text = "Password:";
            // 
            // lblDropboxEmail
            // 
            this.lblDropboxEmail.AutoSize = true;
            this.lblDropboxEmail.Location = new System.Drawing.Point(16, 96);
            this.lblDropboxEmail.Name = "lblDropboxEmail";
            this.lblDropboxEmail.Size = new System.Drawing.Size(35, 13);
            this.lblDropboxEmail.TabIndex = 4;
            this.lblDropboxEmail.Text = "Email:";
            // 
            // btnDropboxLogin
            // 
            this.btnDropboxLogin.Location = new System.Drawing.Point(16, 190);
            this.btnDropboxLogin.Name = "btnDropboxLogin";
            this.btnDropboxLogin.Size = new System.Drawing.Size(80, 24);
            this.btnDropboxLogin.TabIndex = 3;
            this.btnDropboxLogin.Text = "Login";
            this.btnDropboxLogin.UseVisualStyleBackColor = true;
            this.btnDropboxLogin.Click += new System.EventHandler(this.btnDropboxLogin_Click);
            // 
            // txtDropboxPath
            // 
            this.txtDropboxPath.Location = new System.Drawing.Point(88, 156);
            this.txtDropboxPath.Name = "txtDropboxPath";
            this.txtDropboxPath.Size = new System.Drawing.Size(248, 20);
            this.txtDropboxPath.TabIndex = 2;
            this.txtDropboxPath.TextChanged += new System.EventHandler(this.txtDropboxPath_TextChanged);
            // 
            // txtDropboxPassword
            // 
            this.txtDropboxPassword.Location = new System.Drawing.Point(88, 124);
            this.txtDropboxPassword.Name = "txtDropboxPassword";
            this.txtDropboxPassword.PasswordChar = '*';
            this.txtDropboxPassword.Size = new System.Drawing.Size(248, 20);
            this.txtDropboxPassword.TabIndex = 1;
            // 
            // txtDropboxEmail
            // 
            this.txtDropboxEmail.Location = new System.Drawing.Point(88, 92);
            this.txtDropboxEmail.Name = "txtDropboxEmail";
            this.txtDropboxEmail.Size = new System.Drawing.Size(248, 20);
            this.txtDropboxEmail.TabIndex = 0;
            // 
            // tpDestLocalhost
            // 
            this.tpDestLocalhost.BackColor = System.Drawing.SystemColors.Window;
            this.tpDestLocalhost.Controls.Add(this.ucLocalhostAccounts);
            this.tpDestLocalhost.Location = new System.Drawing.Point(4, 23);
            this.tpDestLocalhost.Name = "tpDestLocalhost";
            this.tpDestLocalhost.Padding = new System.Windows.Forms.Padding(3);
            this.tpDestLocalhost.Size = new System.Drawing.Size(791, 402);
            this.tpDestLocalhost.TabIndex = 11;
            this.tpDestLocalhost.Text = "Localhost";
            // 
            // ucLocalhostAccounts
            // 
            this.ucLocalhostAccounts.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ucLocalhostAccounts.Location = new System.Drawing.Point(3, 3);
            this.ucLocalhostAccounts.Margin = new System.Windows.Forms.Padding(4);
            this.ucLocalhostAccounts.Name = "ucLocalhostAccounts";
            this.ucLocalhostAccounts.Size = new System.Drawing.Size(787, 312);
            this.ucLocalhostAccounts.TabIndex = 1;
            // 
            // tpDestRapidShare
            // 
            this.tpDestRapidShare.BackColor = System.Drawing.SystemColors.Window;
            this.tpDestRapidShare.Controls.Add(this.lblRapidSharePassword);
            this.tpDestRapidShare.Controls.Add(this.lblRapidSharePremiumUsername);
            this.tpDestRapidShare.Controls.Add(this.lblRapidShareCollectorsID);
            this.tpDestRapidShare.Controls.Add(this.txtRapidSharePassword);
            this.tpDestRapidShare.Controls.Add(this.txtRapidSharePremiumUserName);
            this.tpDestRapidShare.Controls.Add(this.txtRapidShareCollectorID);
            this.tpDestRapidShare.Controls.Add(this.cboRapidShareAcctType);
            this.tpDestRapidShare.Controls.Add(this.lblRapidShareAccountType);
            this.tpDestRapidShare.ImageKey = "(none)";
            this.tpDestRapidShare.Location = new System.Drawing.Point(4, 23);
            this.tpDestRapidShare.Name = "tpDestRapidShare";
            this.tpDestRapidShare.Padding = new System.Windows.Forms.Padding(3);
            this.tpDestRapidShare.Size = new System.Drawing.Size(791, 402);
            this.tpDestRapidShare.TabIndex = 8;
            this.tpDestRapidShare.Text = "RapidShare";
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
            // tpDestSendSpace
            // 
            this.tpDestSendSpace.BackColor = System.Drawing.SystemColors.Window;
            this.tpDestSendSpace.Controls.Add(this.btnSendSpaceRegister);
            this.tpDestSendSpace.Controls.Add(this.lblSendSpacePassword);
            this.tpDestSendSpace.Controls.Add(this.lblSendSpaceUsername);
            this.tpDestSendSpace.Controls.Add(this.txtSendSpacePassword);
            this.tpDestSendSpace.Controls.Add(this.txtSendSpaceUserName);
            this.tpDestSendSpace.Controls.Add(this.cboSendSpaceAcctType);
            this.tpDestSendSpace.Controls.Add(this.lblSendSpaceAccountType);
            this.tpDestSendSpace.Location = new System.Drawing.Point(4, 23);
            this.tpDestSendSpace.Name = "tpDestSendSpace";
            this.tpDestSendSpace.Padding = new System.Windows.Forms.Padding(3);
            this.tpDestSendSpace.Size = new System.Drawing.Size(791, 402);
            this.tpDestSendSpace.TabIndex = 9;
            this.tpDestSendSpace.Text = "SendSpace";
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
            // tpDestFlickr
            // 
            this.tpDestFlickr.BackColor = System.Drawing.SystemColors.Window;
            this.tpDestFlickr.Controls.Add(this.btnFlickrOpenImages);
            this.tpDestFlickr.Controls.Add(this.pgFlickrAuthInfo);
            this.tpDestFlickr.Controls.Add(this.pgFlickrSettings);
            this.tpDestFlickr.Controls.Add(this.btnFlickrCheckToken);
            this.tpDestFlickr.Controls.Add(this.btnFlickrGetToken);
            this.tpDestFlickr.Controls.Add(this.btnFlickrGetFrob);
            this.tpDestFlickr.Location = new System.Drawing.Point(4, 23);
            this.tpDestFlickr.Name = "tpDestFlickr";
            this.tpDestFlickr.Padding = new System.Windows.Forms.Padding(3);
            this.tpDestFlickr.Size = new System.Drawing.Size(791, 402);
            this.tpDestFlickr.TabIndex = 10;
            this.tpDestFlickr.Text = "Flickr";
            // 
            // btnFlickrOpenImages
            // 
            this.btnFlickrOpenImages.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnFlickrOpenImages.Location = new System.Drawing.Point(607, 184);
            this.btnFlickrOpenImages.Name = "btnFlickrOpenImages";
            this.btnFlickrOpenImages.Size = new System.Drawing.Size(168, 23);
            this.btnFlickrOpenImages.TabIndex = 7;
            this.btnFlickrOpenImages.Text = "Your photostream...";
            this.ttZScreen.SetToolTip(this.btnFlickrOpenImages, "Opens http://www.flickr.com/photos/<UserID>");
            this.btnFlickrOpenImages.UseVisualStyleBackColor = true;
            this.btnFlickrOpenImages.Click += new System.EventHandler(this.btnFlickrOpenImages_Click);
            // 
            // pgFlickrAuthInfo
            // 
            this.pgFlickrAuthInfo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pgFlickrAuthInfo.CommandsVisibleIfAvailable = false;
            this.pgFlickrAuthInfo.Location = new System.Drawing.Point(16, 18);
            this.pgFlickrAuthInfo.Name = "pgFlickrAuthInfo";
            this.pgFlickrAuthInfo.PropertySort = System.Windows.Forms.PropertySort.NoSort;
            this.pgFlickrAuthInfo.Size = new System.Drawing.Size(585, 160);
            this.pgFlickrAuthInfo.TabIndex = 6;
            this.pgFlickrAuthInfo.ToolbarVisible = false;
            // 
            // pgFlickrSettings
            // 
            this.pgFlickrSettings.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pgFlickrSettings.CommandsVisibleIfAvailable = false;
            this.pgFlickrSettings.Location = new System.Drawing.Point(16, 184);
            this.pgFlickrSettings.Name = "pgFlickrSettings";
            this.pgFlickrSettings.PropertySort = System.Windows.Forms.PropertySort.NoSort;
            this.pgFlickrSettings.Size = new System.Drawing.Size(585, 209);
            this.pgFlickrSettings.TabIndex = 5;
            this.pgFlickrSettings.ToolbarVisible = false;
            // 
            // btnFlickrCheckToken
            // 
            this.btnFlickrCheckToken.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnFlickrCheckToken.Location = new System.Drawing.Point(607, 155);
            this.btnFlickrCheckToken.Name = "btnFlickrCheckToken";
            this.btnFlickrCheckToken.Size = new System.Drawing.Size(168, 23);
            this.btnFlickrCheckToken.TabIndex = 4;
            this.btnFlickrCheckToken.Text = "Check Token...";
            this.ttZScreen.SetToolTip(this.btnFlickrCheckToken, "Returns the credentials attached to an authentication token.");
            this.btnFlickrCheckToken.UseVisualStyleBackColor = true;
            this.btnFlickrCheckToken.Click += new System.EventHandler(this.btnFlickrCheckToken_Click);
            // 
            // btnFlickrGetToken
            // 
            this.btnFlickrGetToken.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnFlickrGetToken.Enabled = false;
            this.btnFlickrGetToken.Location = new System.Drawing.Point(607, 47);
            this.btnFlickrGetToken.Name = "btnFlickrGetToken";
            this.btnFlickrGetToken.Size = new System.Drawing.Size(168, 24);
            this.btnFlickrGetToken.TabIndex = 1;
            this.btnFlickrGetToken.Text = "Step 2. Finalize Authentication...";
            this.btnFlickrGetToken.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.ttZScreen.SetToolTip(this.btnFlickrGetToken, "Returns the auth token for the given frob, if one has been attached.");
            this.btnFlickrGetToken.UseVisualStyleBackColor = true;
            this.btnFlickrGetToken.Click += new System.EventHandler(this.btnFlickrGetToken_Click);
            // 
            // btnFlickrGetFrob
            // 
            this.btnFlickrGetFrob.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnFlickrGetFrob.Location = new System.Drawing.Point(607, 18);
            this.btnFlickrGetFrob.Name = "btnFlickrGetFrob";
            this.btnFlickrGetFrob.Size = new System.Drawing.Size(168, 23);
            this.btnFlickrGetFrob.TabIndex = 0;
            this.btnFlickrGetFrob.Text = "Step 1. Authenticate ZScreen...";
            this.btnFlickrGetFrob.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.ttZScreen.SetToolTip(this.btnFlickrGetFrob, "Returns a frob to be used during authentication.");
            this.btnFlickrGetFrob.UseVisualStyleBackColor = true;
            this.btnFlickrGetFrob.Click += new System.EventHandler(this.btnFlickrGetFrob_Click);
            // 
            // tpDestImageShack
            // 
            this.tpDestImageShack.BackColor = System.Drawing.SystemColors.Window;
            this.tpDestImageShack.Controls.Add(this.chkPublicImageShack);
            this.tpDestImageShack.Controls.Add(this.gbImageShack);
            this.tpDestImageShack.Location = new System.Drawing.Point(4, 23);
            this.tpDestImageShack.Name = "tpDestImageShack";
            this.tpDestImageShack.Padding = new System.Windows.Forms.Padding(3);
            this.tpDestImageShack.Size = new System.Drawing.Size(791, 402);
            this.tpDestImageShack.TabIndex = 1;
            this.tpDestImageShack.Text = "ImageShack";
            // 
            // chkPublicImageShack
            // 
            this.chkPublicImageShack.AutoSize = true;
            this.chkPublicImageShack.Location = new System.Drawing.Point(16, 104);
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
            this.gbImageShack.Location = new System.Drawing.Point(8, 8);
            this.gbImageShack.Name = "gbImageShack";
            this.gbImageShack.Size = new System.Drawing.Size(769, 88);
            this.gbImageShack.TabIndex = 0;
            this.gbImageShack.TabStop = false;
            this.gbImageShack.Text = "Account";
            // 
            // btnImageShackProfile
            // 
            this.btnImageShackProfile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnImageShackProfile.Location = new System.Drawing.Point(472, 52);
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
            this.lblImageShackUsername.Location = new System.Drawing.Point(48, 52);
            this.lblImageShackUsername.Name = "lblImageShackUsername";
            this.lblImageShackUsername.Size = new System.Drawing.Size(63, 13);
            this.lblImageShackUsername.TabIndex = 5;
            this.lblImageShackUsername.Text = "User Name:";
            // 
            // txtUserNameImageShack
            // 
            this.txtUserNameImageShack.Location = new System.Drawing.Point(120, 52);
            this.txtUserNameImageShack.Name = "txtUserNameImageShack";
            this.txtUserNameImageShack.Size = new System.Drawing.Size(344, 20);
            this.txtUserNameImageShack.TabIndex = 4;
            this.txtUserNameImageShack.TextChanged += new System.EventHandler(this.txtUserNameImageShack_TextChanged);
            // 
            // btnGalleryImageShack
            // 
            this.btnGalleryImageShack.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGalleryImageShack.Location = new System.Drawing.Point(568, 20);
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
            this.btnRegCodeImageShack.Location = new System.Drawing.Point(472, 20);
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
            this.lblImageShackRegistrationCode.Location = new System.Drawing.Point(16, 24);
            this.lblImageShackRegistrationCode.Name = "lblImageShackRegistrationCode";
            this.lblImageShackRegistrationCode.Size = new System.Drawing.Size(93, 13);
            this.lblImageShackRegistrationCode.TabIndex = 1;
            this.lblImageShackRegistrationCode.Text = "Registration code:";
            // 
            // txtImageShackRegistrationCode
            // 
            this.txtImageShackRegistrationCode.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtImageShackRegistrationCode.Location = new System.Drawing.Point(120, 20);
            this.txtImageShackRegistrationCode.Name = "txtImageShackRegistrationCode";
            this.txtImageShackRegistrationCode.Size = new System.Drawing.Size(344, 20);
            this.txtImageShackRegistrationCode.TabIndex = 0;
            this.txtImageShackRegistrationCode.TextChanged += new System.EventHandler(this.txtImageShackRegistrationCode_TextChanged);
            // 
            // tpDestImgur
            // 
            this.tpDestImgur.BackColor = System.Drawing.SystemColors.Window;
            this.tpDestImgur.Controls.Add(this.gbImgurUserAccount);
            this.tpDestImgur.Controls.Add(this.chkImgurUserAccount);
            this.tpDestImgur.Location = new System.Drawing.Point(4, 23);
            this.tpDestImgur.Name = "tpDestImgur";
            this.tpDestImgur.Padding = new System.Windows.Forms.Padding(3);
            this.tpDestImgur.Size = new System.Drawing.Size(791, 402);
            this.tpDestImgur.TabIndex = 15;
            this.tpDestImgur.Text = "Imgur";
            // 
            // gbImgurUserAccount
            // 
            this.gbImgurUserAccount.Controls.Add(this.btnImgurOpenAuthorizePage);
            this.gbImgurUserAccount.Controls.Add(this.btnImgurLogin);
            this.gbImgurUserAccount.Controls.Add(this.lblImgurStatus);
            this.gbImgurUserAccount.Location = new System.Drawing.Point(16, 48);
            this.gbImgurUserAccount.Name = "gbImgurUserAccount";
            this.gbImgurUserAccount.Size = new System.Drawing.Size(376, 104);
            this.gbImgurUserAccount.TabIndex = 7;
            this.gbImgurUserAccount.TabStop = false;
            this.gbImgurUserAccount.Text = "User account";
            // 
            // btnImgurOpenAuthorizePage
            // 
            this.btnImgurOpenAuthorizePage.Location = new System.Drawing.Point(16, 24);
            this.btnImgurOpenAuthorizePage.Name = "btnImgurOpenAuthorizePage";
            this.btnImgurOpenAuthorizePage.Size = new System.Drawing.Size(168, 32);
            this.btnImgurOpenAuthorizePage.TabIndex = 0;
            this.btnImgurOpenAuthorizePage.Text = "Open authorize page...";
            this.btnImgurOpenAuthorizePage.UseVisualStyleBackColor = true;
            this.btnImgurOpenAuthorizePage.Click += new System.EventHandler(this.btnImgurOpenAuthorizePage_Click);
            // 
            // btnImgurLogin
            // 
            this.btnImgurLogin.Location = new System.Drawing.Point(192, 24);
            this.btnImgurLogin.Name = "btnImgurLogin";
            this.btnImgurLogin.Size = new System.Drawing.Size(168, 32);
            this.btnImgurLogin.TabIndex = 3;
            this.btnImgurLogin.Text = "Enter verification code...";
            this.btnImgurLogin.UseVisualStyleBackColor = true;
            this.btnImgurLogin.Click += new System.EventHandler(this.btnImgurLogin_Click);
            // 
            // lblImgurStatus
            // 
            this.lblImgurStatus.AutoSize = true;
            this.lblImgurStatus.Location = new System.Drawing.Point(16, 72);
            this.lblImgurStatus.Name = "lblImgurStatus";
            this.lblImgurStatus.Size = new System.Drawing.Size(84, 13);
            this.lblImgurStatus.TabIndex = 5;
            this.lblImgurStatus.Text = "Login is required";
            // 
            // chkImgurUserAccount
            // 
            this.chkImgurUserAccount.AutoSize = true;
            this.chkImgurUserAccount.Location = new System.Drawing.Point(16, 16);
            this.chkImgurUserAccount.Name = "chkImgurUserAccount";
            this.chkImgurUserAccount.Size = new System.Drawing.Size(145, 17);
            this.chkImgurUserAccount.TabIndex = 6;
            this.chkImgurUserAccount.Text = "Upload via User Account";
            this.chkImgurUserAccount.UseVisualStyleBackColor = true;
            this.chkImgurUserAccount.CheckedChanged += new System.EventHandler(this.cbImgurUseAccount_CheckedChanged);
            // 
            // tpDestImageBam
            // 
            this.tpDestImageBam.BackColor = System.Drawing.SystemColors.Window;
            this.tpDestImageBam.Controls.Add(this.gbImageBamGalleries);
            this.tpDestImageBam.Controls.Add(this.gbImageBamLinks);
            this.tpDestImageBam.Controls.Add(this.gbImageBamApiKeys);
            this.tpDestImageBam.Location = new System.Drawing.Point(4, 23);
            this.tpDestImageBam.Name = "tpDestImageBam";
            this.tpDestImageBam.Padding = new System.Windows.Forms.Padding(3);
            this.tpDestImageBam.Size = new System.Drawing.Size(791, 402);
            this.tpDestImageBam.TabIndex = 7;
            this.tpDestImageBam.Text = "ImageBam";
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
            this.gbImageBamLinks.Size = new System.Drawing.Size(206, 256);
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
            this.btnImageBamRegister.Size = new System.Drawing.Size(169, 27);
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
            this.btnImageBamApiKeysUrl.Size = new System.Drawing.Size(151, 27);
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
            // tpDestTinyPic
            // 
            this.tpDestTinyPic.BackColor = System.Drawing.SystemColors.Window;
            this.tpDestTinyPic.Controls.Add(this.gbTinyPic);
            this.tpDestTinyPic.Controls.Add(this.chkRememberTinyPicUserPass);
            this.tpDestTinyPic.Location = new System.Drawing.Point(4, 23);
            this.tpDestTinyPic.Name = "tpDestTinyPic";
            this.tpDestTinyPic.Padding = new System.Windows.Forms.Padding(3);
            this.tpDestTinyPic.Size = new System.Drawing.Size(791, 402);
            this.tpDestTinyPic.TabIndex = 0;
            this.tpDestTinyPic.Text = "TinyPic";
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
            this.btnGalleryTinyPic.Location = new System.Drawing.Point(673, 24);
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
            this.txtTinyPicShuk.Size = new System.Drawing.Size(473, 20);
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
            // tpDestTwitter
            // 
            this.tpDestTwitter.BackColor = System.Drawing.SystemColors.Window;
            this.tpDestTwitter.Controls.Add(this.tlpTwitter);
            this.tpDestTwitter.ImageKey = "(none)";
            this.tpDestTwitter.Location = new System.Drawing.Point(4, 23);
            this.tpDestTwitter.Name = "tpDestTwitter";
            this.tpDestTwitter.Padding = new System.Windows.Forms.Padding(3);
            this.tpDestTwitter.Size = new System.Drawing.Size(791, 402);
            this.tpDestTwitter.TabIndex = 6;
            this.tpDestTwitter.Text = "Twitter";
            // 
            // tlpTwitter
            // 
            this.tlpTwitter.ColumnCount = 1;
            this.tlpTwitter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpTwitter.Controls.Add(this.panelTwitter, 0, 0);
            this.tlpTwitter.Controls.Add(this.gbTwitterOthers, 0, 1);
            this.tlpTwitter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpTwitter.Location = new System.Drawing.Point(3, 3);
            this.tlpTwitter.Name = "tlpTwitter";
            this.tlpTwitter.RowCount = 2;
            this.tlpTwitter.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 75F));
            this.tlpTwitter.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tlpTwitter.Size = new System.Drawing.Size(785, 396);
            this.tlpTwitter.TabIndex = 23;
            // 
            // panelTwitter
            // 
            this.panelTwitter.Controls.Add(this.btnTwitterLogin);
            this.panelTwitter.Controls.Add(this.ucTwitterAccounts);
            this.panelTwitter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelTwitter.Location = new System.Drawing.Point(3, 3);
            this.panelTwitter.Name = "panelTwitter";
            this.panelTwitter.Size = new System.Drawing.Size(779, 291);
            this.panelTwitter.TabIndex = 24;
            // 
            // btnTwitterLogin
            // 
            this.btnTwitterLogin.Location = new System.Drawing.Point(226, 8);
            this.btnTwitterLogin.Name = "btnTwitterLogin";
            this.btnTwitterLogin.Size = new System.Drawing.Size(60, 24);
            this.btnTwitterLogin.TabIndex = 19;
            this.btnTwitterLogin.Text = "Login";
            this.btnTwitterLogin.UseVisualStyleBackColor = true;
            this.btnTwitterLogin.Click += new System.EventHandler(this.btnTwitterLogin_Click);
            // 
            // ucTwitterAccounts
            // 
            this.ucTwitterAccounts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucTwitterAccounts.Location = new System.Drawing.Point(0, 0);
            this.ucTwitterAccounts.Name = "ucTwitterAccounts";
            this.ucTwitterAccounts.Size = new System.Drawing.Size(779, 291);
            this.ucTwitterAccounts.TabIndex = 22;
            // 
            // gbTwitterOthers
            // 
            this.gbTwitterOthers.Controls.Add(this.cbTwitPicShowFull);
            this.gbTwitterOthers.Controls.Add(this.cboTwitPicThumbnailMode);
            this.gbTwitterOthers.Controls.Add(this.lblTwitPicThumbnailMode);
            this.gbTwitterOthers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbTwitterOthers.Location = new System.Drawing.Point(3, 300);
            this.gbTwitterOthers.Name = "gbTwitterOthers";
            this.gbTwitterOthers.Size = new System.Drawing.Size(779, 93);
            this.gbTwitterOthers.TabIndex = 20;
            this.gbTwitterOthers.TabStop = false;
            this.gbTwitterOthers.Text = "Other Twitter services (yfrog, twitsnaps etc.)";
            // 
            // cbTwitPicShowFull
            // 
            this.cbTwitPicShowFull.AutoSize = true;
            this.cbTwitPicShowFull.Location = new System.Drawing.Point(16, 24);
            this.cbTwitPicShowFull.Name = "cbTwitPicShowFull";
            this.cbTwitPicShowFull.Size = new System.Drawing.Size(94, 17);
            this.cbTwitPicShowFull.TabIndex = 13;
            this.cbTwitPicShowFull.Text = "Show full URL";
            this.ttZScreen.SetToolTip(this.cbTwitPicShowFull, "Append /full to the url to show the image in full size");
            this.cbTwitPicShowFull.UseVisualStyleBackColor = true;
            this.cbTwitPicShowFull.CheckedChanged += new System.EventHandler(this.cbTwitPicShowFull_CheckedChanged);
            // 
            // cboTwitPicThumbnailMode
            // 
            this.cboTwitPicThumbnailMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTwitPicThumbnailMode.FormattingEnabled = true;
            this.cboTwitPicThumbnailMode.Location = new System.Drawing.Point(112, 48);
            this.cboTwitPicThumbnailMode.Name = "cboTwitPicThumbnailMode";
            this.cboTwitPicThumbnailMode.Size = new System.Drawing.Size(144, 21);
            this.cboTwitPicThumbnailMode.TabIndex = 14;
            this.cboTwitPicThumbnailMode.SelectedIndexChanged += new System.EventHandler(this.cbTwitPicThumbnailMode_SelectedIndexChanged);
            // 
            // lblTwitPicThumbnailMode
            // 
            this.lblTwitPicThumbnailMode.AutoSize = true;
            this.lblTwitPicThumbnailMode.Location = new System.Drawing.Point(16, 53);
            this.lblTwitPicThumbnailMode.Name = "lblTwitPicThumbnailMode";
            this.lblTwitPicThumbnailMode.Size = new System.Drawing.Size(89, 13);
            this.lblTwitPicThumbnailMode.TabIndex = 15;
            this.lblTwitPicThumbnailMode.Text = "Thumbnail Mode:";
            // 
            // tpDestMindTouch
            // 
            this.tpDestMindTouch.BackColor = System.Drawing.SystemColors.Window;
            this.tpDestMindTouch.Controls.Add(this.gbMindTouchOptions);
            this.tpDestMindTouch.Controls.Add(this.ucMindTouchAccounts);
            this.tpDestMindTouch.Location = new System.Drawing.Point(4, 23);
            this.tpDestMindTouch.Name = "tpDestMindTouch";
            this.tpDestMindTouch.Padding = new System.Windows.Forms.Padding(3);
            this.tpDestMindTouch.Size = new System.Drawing.Size(791, 402);
            this.tpDestMindTouch.TabIndex = 4;
            this.tpDestMindTouch.Text = "MindTouch";
            // 
            // gbMindTouchOptions
            // 
            this.gbMindTouchOptions.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbMindTouchOptions.Controls.Add(this.chkDekiWikiForcePath);
            this.gbMindTouchOptions.Location = new System.Drawing.Point(16, 314);
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
            this.ucMindTouchAccounts.Margin = new System.Windows.Forms.Padding(4);
            this.ucMindTouchAccounts.Name = "ucMindTouchAccounts";
            this.ucMindTouchAccounts.Size = new System.Drawing.Size(787, 312);
            this.ucMindTouchAccounts.TabIndex = 0;
            // 
            // tpDestMediaWiki
            // 
            this.tpDestMediaWiki.BackColor = System.Drawing.SystemColors.Window;
            this.tpDestMediaWiki.Controls.Add(this.ucMediaWikiAccounts);
            this.tpDestMediaWiki.Location = new System.Drawing.Point(4, 23);
            this.tpDestMediaWiki.Name = "tpDestMediaWiki";
            this.tpDestMediaWiki.Padding = new System.Windows.Forms.Padding(3);
            this.tpDestMediaWiki.Size = new System.Drawing.Size(791, 402);
            this.tpDestMediaWiki.TabIndex = 13;
            this.tpDestMediaWiki.Text = "MediaWiki";
            // 
            // ucMediaWikiAccounts
            // 
            this.ucMediaWikiAccounts.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ucMediaWikiAccounts.BackColor = System.Drawing.Color.Transparent;
            this.ucMediaWikiAccounts.Location = new System.Drawing.Point(3, 3);
            this.ucMediaWikiAccounts.Margin = new System.Windows.Forms.Padding(4);
            this.ucMediaWikiAccounts.Name = "ucMediaWikiAccounts";
            this.ucMediaWikiAccounts.Size = new System.Drawing.Size(787, 392);
            this.ucMediaWikiAccounts.TabIndex = 0;
            // 
            // tpDestCustom
            // 
            this.tpDestCustom.BackColor = System.Drawing.SystemColors.Window;
            this.tpDestCustom.Controls.Add(this.txtUploadersLog);
            this.tpDestCustom.Controls.Add(this.btnUploadersTest);
            this.tpDestCustom.Controls.Add(this.txtFullImage);
            this.tpDestCustom.Controls.Add(this.txtThumbnail);
            this.tpDestCustom.Controls.Add(this.lblFullImage);
            this.tpDestCustom.Controls.Add(this.lblThumbnail);
            this.tpDestCustom.Controls.Add(this.gbImageUploaders);
            this.tpDestCustom.Controls.Add(this.gbRegexp);
            this.tpDestCustom.Controls.Add(this.txtFileForm);
            this.tpDestCustom.Controls.Add(this.lblFileForm);
            this.tpDestCustom.Controls.Add(this.lblUploadURL);
            this.tpDestCustom.Controls.Add(this.txtUploadURL);
            this.tpDestCustom.Controls.Add(this.gbArguments);
            this.tpDestCustom.ImageKey = "world_add.png";
            this.tpDestCustom.Location = new System.Drawing.Point(4, 23);
            this.tpDestCustom.Name = "tpDestCustom";
            this.tpDestCustom.Padding = new System.Windows.Forms.Padding(3);
            this.tpDestCustom.Size = new System.Drawing.Size(791, 402);
            this.tpDestCustom.TabIndex = 11;
            this.tpDestCustom.Text = "Custom";
            // 
            // txtUploadersLog
            // 
            this.txtUploadersLog.Location = new System.Drawing.Point(8, 280);
            this.txtUploadersLog.Name = "txtUploadersLog";
            this.txtUploadersLog.Size = new System.Drawing.Size(424, 104);
            this.txtUploadersLog.TabIndex = 18;
            this.txtUploadersLog.Text = "";
            this.txtUploadersLog.LinkClicked += new System.Windows.Forms.LinkClickedEventHandler(this.txtUploadersLog_LinkClicked);
            // 
            // btnUploadersTest
            // 
            this.btnUploadersTest.Location = new System.Drawing.Point(448, 360);
            this.btnUploadersTest.Name = "btnUploadersTest";
            this.btnUploadersTest.Size = new System.Drawing.Size(328, 24);
            this.btnUploadersTest.TabIndex = 9;
            this.btnUploadersTest.Text = "Test Upload";
            this.btnUploadersTest.UseVisualStyleBackColor = true;
            this.btnUploadersTest.Click += new System.EventHandler(this.btUploadersTest_Click);
            // 
            // txtFullImage
            // 
            this.txtFullImage.Location = new System.Drawing.Point(448, 296);
            this.txtFullImage.Name = "txtFullImage";
            this.txtFullImage.Size = new System.Drawing.Size(328, 20);
            this.txtFullImage.TabIndex = 5;
            // 
            // txtThumbnail
            // 
            this.txtThumbnail.Location = new System.Drawing.Point(448, 336);
            this.txtThumbnail.Name = "txtThumbnail";
            this.txtThumbnail.Size = new System.Drawing.Size(328, 20);
            this.txtThumbnail.TabIndex = 6;
            // 
            // lblFullImage
            // 
            this.lblFullImage.AutoSize = true;
            this.lblFullImage.Location = new System.Drawing.Point(448, 280);
            this.lblFullImage.Name = "lblFullImage";
            this.lblFullImage.Size = new System.Drawing.Size(55, 13);
            this.lblFullImage.TabIndex = 17;
            this.lblFullImage.Text = "Full Image";
            // 
            // lblThumbnail
            // 
            this.lblThumbnail.AutoSize = true;
            this.lblThumbnail.Location = new System.Drawing.Point(448, 320);
            this.lblThumbnail.Name = "lblThumbnail";
            this.lblThumbnail.Size = new System.Drawing.Size(56, 13);
            this.lblThumbnail.TabIndex = 16;
            this.lblThumbnail.Text = "Thumbnail";
            // 
            // gbImageUploaders
            // 
            this.gbImageUploaders.Controls.Add(this.lbImageUploader);
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
            // lbImageUploader
            // 
            this.lbImageUploader.FormattingEnabled = true;
            this.lbImageUploader.Location = new System.Drawing.Point(8, 72);
            this.lbImageUploader.Name = "lbImageUploader";
            this.lbImageUploader.Size = new System.Drawing.Size(232, 147);
            this.lbImageUploader.TabIndex = 3;
            this.lbImageUploader.SelectedIndexChanged += new System.EventHandler(this.lbImageUploader_SelectedIndexChanged);
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
            this.gbRegexp.Location = new System.Drawing.Point(272, 88);
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
            this.txtFileForm.Location = new System.Drawing.Point(280, 64);
            this.txtFileForm.Name = "txtFileForm";
            this.txtFileForm.Size = new System.Drawing.Size(224, 20);
            this.txtFileForm.TabIndex = 3;
            // 
            // lblFileForm
            // 
            this.lblFileForm.AutoSize = true;
            this.lblFileForm.Location = new System.Drawing.Point(280, 48);
            this.lblFileForm.Name = "lblFileForm";
            this.lblFileForm.Size = new System.Drawing.Size(83, 13);
            this.lblFileForm.TabIndex = 9;
            this.lblFileForm.Text = "File Form Name:";
            // 
            // lblUploadURL
            // 
            this.lblUploadURL.AutoSize = true;
            this.lblUploadURL.Location = new System.Drawing.Point(280, 8);
            this.lblUploadURL.Name = "lblUploadURL";
            this.lblUploadURL.Size = new System.Drawing.Size(69, 13);
            this.lblUploadURL.TabIndex = 8;
            this.lblUploadURL.Text = "Upload URL:";
            // 
            // txtUploadURL
            // 
            this.txtUploadURL.Location = new System.Drawing.Point(280, 24);
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
            this.gbArguments.Location = new System.Drawing.Point(528, 8);
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
            // tpHotkeys
            // 
            this.tpHotkeys.Controls.Add(this.btnResetHotkeys);
            this.tpHotkeys.Controls.Add(this.lblHotkeyStatus);
            this.tpHotkeys.Controls.Add(this.dgvHotkeys);
            this.tpHotkeys.ImageKey = "(none)";
            this.tpHotkeys.Location = new System.Drawing.Point(4, 22);
            this.tpHotkeys.Name = "tpHotkeys";
            this.tpHotkeys.Padding = new System.Windows.Forms.Padding(3);
            this.tpHotkeys.Size = new System.Drawing.Size(805, 436);
            this.tpHotkeys.TabIndex = 1;
            this.tpHotkeys.Text = "Hotkeys";
            this.tpHotkeys.UseVisualStyleBackColor = true;
            // 
            // btnResetHotkeys
            // 
            this.btnResetHotkeys.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnResetHotkeys.AutoSize = true;
            this.btnResetHotkeys.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnResetHotkeys.Location = new System.Drawing.Point(688, 16);
            this.btnResetHotkeys.Name = "btnResetHotkeys";
            this.btnResetHotkeys.Size = new System.Drawing.Size(101, 23);
            this.btnResetHotkeys.TabIndex = 69;
            this.btnResetHotkeys.Text = "Reset &All Hotkeys";
            this.btnResetHotkeys.UseVisualStyleBackColor = true;
            this.btnResetHotkeys.Click += new System.EventHandler(this.btnResetHotkeys_Click);
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
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvHotkeys.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.dgvHotkeys.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvHotkeys.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.chHotkeys_Description,
            this.chHotkeys_Keys,
            this.DefaultKeys});
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvHotkeys.DefaultCellStyle = dataGridViewCellStyle7;
            this.dgvHotkeys.Location = new System.Drawing.Point(26, 50);
            this.dgvHotkeys.MultiSelect = false;
            this.dgvHotkeys.Name = "dgvHotkeys";
            this.dgvHotkeys.ReadOnly = true;
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvHotkeys.RowHeadersDefaultCellStyle = dataGridViewCellStyle8;
            this.dgvHotkeys.RowHeadersVisible = false;
            this.dgvHotkeys.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgvHotkeys.RowTemplate.Height = 24;
            this.dgvHotkeys.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dgvHotkeys.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgvHotkeys.Size = new System.Drawing.Size(550, 378);
            this.dgvHotkeys.TabIndex = 67;
            this.dgvHotkeys.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvHotkeys_CellClick);
            this.dgvHotkeys.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgvHotkeys_KeyDown);
            this.dgvHotkeys.Leave += new System.EventHandler(this.dgvHotkeys_Leave);
            this.dgvHotkeys.MouseLeave += new System.EventHandler(this.dgvHotkeys_MouseLeave);
            // 
            // chHotkeys_Description
            // 
            this.chHotkeys_Description.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.chHotkeys_Description.HeaderText = "Description";
            this.chHotkeys_Description.Name = "chHotkeys_Description";
            this.chHotkeys_Description.ReadOnly = true;
            this.chHotkeys_Description.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // chHotkeys_Keys
            // 
            this.chHotkeys_Keys.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.chHotkeys_Keys.HeaderText = "Hotkey";
            this.chHotkeys_Keys.Name = "chHotkeys_Keys";
            this.chHotkeys_Keys.ReadOnly = true;
            this.chHotkeys_Keys.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // DefaultKeys
            // 
            this.DefaultKeys.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.DefaultKeys.DefaultCellStyle = dataGridViewCellStyle6;
            this.DefaultKeys.HeaderText = "Default Hotkey";
            this.DefaultKeys.Name = "DefaultKeys";
            this.DefaultKeys.ReadOnly = true;
            this.DefaultKeys.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.DefaultKeys.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // tpScreenshots
            // 
            this.tpScreenshots.Controls.Add(this.tcScreenshots);
            this.tpScreenshots.ImageKey = "(none)";
            this.tpScreenshots.Location = new System.Drawing.Point(4, 22);
            this.tpScreenshots.Name = "tpScreenshots";
            this.tpScreenshots.Padding = new System.Windows.Forms.Padding(3);
            this.tpScreenshots.Size = new System.Drawing.Size(805, 436);
            this.tpScreenshots.TabIndex = 4;
            this.tpScreenshots.Text = "Screenshots";
            this.tpScreenshots.UseVisualStyleBackColor = true;
            // 
            // tcScreenshots
            // 
            this.tcScreenshots.Controls.Add(this.tpCropShot);
            this.tcScreenshots.Controls.Add(this.tpSelectedWindow);
            this.tcScreenshots.Controls.Add(this.tpActivewindow);
            this.tcScreenshots.Controls.Add(this.tpFreehandCropShot);
            this.tcScreenshots.Controls.Add(this.tpWatermark);
            this.tcScreenshots.Controls.Add(this.tpFileNaming);
            this.tcScreenshots.Controls.Add(this.tpCaptureQuality);
            this.tcScreenshots.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcScreenshots.Location = new System.Drawing.Point(3, 3);
            this.tcScreenshots.Name = "tcScreenshots";
            this.tcScreenshots.SelectedIndex = 0;
            this.tcScreenshots.Size = new System.Drawing.Size(799, 430);
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
            this.tpCropShot.Location = new System.Drawing.Point(4, 22);
            this.tpCropShot.Name = "tpCropShot";
            this.tpCropShot.Padding = new System.Windows.Forms.Padding(3);
            this.tpCropShot.Size = new System.Drawing.Size(791, 404);
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
            this.gbDynamicCrosshair.Controls.Add(this.chkCropDynamicCrosshair);
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
            // chkCropDynamicCrosshair
            // 
            this.chkCropDynamicCrosshair.AutoSize = true;
            this.chkCropDynamicCrosshair.Location = new System.Drawing.Point(16, 24);
            this.chkCropDynamicCrosshair.Name = "chkCropDynamicCrosshair";
            this.chkCropDynamicCrosshair.Size = new System.Drawing.Size(65, 17);
            this.chkCropDynamicCrosshair.TabIndex = 16;
            this.chkCropDynamicCrosshair.Text = "Enabled";
            this.chkCropDynamicCrosshair.UseVisualStyleBackColor = true;
            this.chkCropDynamicCrosshair.CheckedChanged += new System.EventHandler(this.cbCropDynamicCrosshair_CheckedChanged);
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
            this.gpCropRegion.Controls.Add(this.chkRegionHotkeyInfo);
            this.gpCropRegion.Controls.Add(this.chkCropStyle);
            this.gpCropRegion.Controls.Add(this.chkRegionRectangleInfo);
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
            // chkRegionHotkeyInfo
            // 
            this.chkRegionHotkeyInfo.AutoSize = true;
            this.chkRegionHotkeyInfo.Location = new System.Drawing.Point(16, 88);
            this.chkRegionHotkeyInfo.Name = "chkRegionHotkeyInfo";
            this.chkRegionHotkeyInfo.Size = new System.Drawing.Size(200, 17);
            this.chkRegionHotkeyInfo.TabIndex = 6;
            this.chkRegionHotkeyInfo.Text = "Show crop region hotkey instructions";
            this.chkRegionHotkeyInfo.UseVisualStyleBackColor = true;
            this.chkRegionHotkeyInfo.CheckedChanged += new System.EventHandler(this.cbRegionHotkeyInfo_CheckedChanged);
            // 
            // chkCropStyle
            // 
            this.chkCropStyle.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.chkCropStyle.FormattingEnabled = true;
            this.chkCropStyle.Location = new System.Drawing.Point(120, 24);
            this.chkCropStyle.Name = "chkCropStyle";
            this.chkCropStyle.Size = new System.Drawing.Size(216, 21);
            this.chkCropStyle.TabIndex = 8;
            this.chkCropStyle.SelectedIndexChanged += new System.EventHandler(this.cbCropStyle_SelectedIndexChanged);
            // 
            // chkRegionRectangleInfo
            // 
            this.chkRegionRectangleInfo.AutoSize = true;
            this.chkRegionRectangleInfo.Location = new System.Drawing.Point(16, 64);
            this.chkRegionRectangleInfo.Name = "chkRegionRectangleInfo";
            this.chkRegionRectangleInfo.Size = new System.Drawing.Size(209, 17);
            this.chkRegionRectangleInfo.TabIndex = 5;
            this.chkRegionRectangleInfo.Text = "Show crop region coordinates and size";
            this.chkRegionRectangleInfo.UseVisualStyleBackColor = true;
            this.chkRegionRectangleInfo.CheckedChanged += new System.EventHandler(this.cbRegionRectangleInfo_CheckedChanged);
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
            this.gbCrosshairSettings.Controls.Add(this.chkCropShowMagnifyingGlass);
            this.gbCrosshairSettings.Controls.Add(this.chkCropShowBigCross);
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
            // chkCropShowMagnifyingGlass
            // 
            this.chkCropShowMagnifyingGlass.AutoSize = true;
            this.chkCropShowMagnifyingGlass.Location = new System.Drawing.Point(16, 48);
            this.chkCropShowMagnifyingGlass.Name = "chkCropShowMagnifyingGlass";
            this.chkCropShowMagnifyingGlass.Size = new System.Drawing.Size(133, 17);
            this.chkCropShowMagnifyingGlass.TabIndex = 26;
            this.chkCropShowMagnifyingGlass.Text = "Show magnifying glass";
            this.chkCropShowMagnifyingGlass.UseVisualStyleBackColor = true;
            this.chkCropShowMagnifyingGlass.CheckedChanged += new System.EventHandler(this.cbCropShowMagnifyingGlass_CheckedChanged);
            // 
            // chkCropShowBigCross
            // 
            this.chkCropShowBigCross.AutoSize = true;
            this.chkCropShowBigCross.Location = new System.Drawing.Point(16, 24);
            this.chkCropShowBigCross.Name = "chkCropShowBigCross";
            this.chkCropShowBigCross.Size = new System.Drawing.Size(194, 17);
            this.chkCropShowBigCross.TabIndex = 25;
            this.chkCropShowBigCross.Text = "Show second crosshair ( Big cross )";
            this.chkCropShowBigCross.UseVisualStyleBackColor = true;
            this.chkCropShowBigCross.CheckedChanged += new System.EventHandler(this.cbCropShowBigCross_CheckedChanged);
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
            this.tpSelectedWindow.Controls.Add(this.chkSelectedWindowCaptureObjects);
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
            this.tpSelectedWindow.Size = new System.Drawing.Size(791, 402);
            this.tpSelectedWindow.TabIndex = 6;
            this.tpSelectedWindow.Text = "Selected Window";
            this.tpSelectedWindow.UseVisualStyleBackColor = true;
            // 
            // chkSelectedWindowCaptureObjects
            // 
            this.chkSelectedWindowCaptureObjects.AutoSize = true;
            this.chkSelectedWindowCaptureObjects.Location = new System.Drawing.Point(16, 272);
            this.chkSelectedWindowCaptureObjects.Name = "chkSelectedWindowCaptureObjects";
            this.chkSelectedWindowCaptureObjects.Size = new System.Drawing.Size(231, 17);
            this.chkSelectedWindowCaptureObjects.TabIndex = 42;
            this.chkSelectedWindowCaptureObjects.Text = "Capture control objects within each window";
            this.chkSelectedWindowCaptureObjects.UseVisualStyleBackColor = true;
            this.chkSelectedWindowCaptureObjects.CheckedChanged += new System.EventHandler(this.cbSelectedWindowCaptureObjects_CheckedChanged);
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
            // tpActivewindow
            // 
            this.tpActivewindow.Controls.Add(this.chkActiveWindowTryCaptureChildren);
            this.tpActivewindow.Controls.Add(this.cbActiveWindowGDIFreezeWindow);
            this.tpActivewindow.Controls.Add(this.chkSelectedWindowCleanTransparentCorners);
            this.tpActivewindow.Controls.Add(this.chkSelectedWindowShowCheckers);
            this.tpActivewindow.Controls.Add(this.chkSelectedWindowIncludeShadow);
            this.tpActivewindow.Controls.Add(this.chkActiveWindowPreferDWM);
            this.tpActivewindow.Controls.Add(this.chkSelectedWindowCleanBackground);
            this.tpActivewindow.ImageKey = "application.png";
            this.tpActivewindow.Location = new System.Drawing.Point(4, 23);
            this.tpActivewindow.Name = "tpActivewindow";
            this.tpActivewindow.Padding = new System.Windows.Forms.Padding(3);
            this.tpActivewindow.Size = new System.Drawing.Size(791, 402);
            this.tpActivewindow.TabIndex = 12;
            this.tpActivewindow.Text = "Active Window";
            this.tpActivewindow.UseVisualStyleBackColor = true;
            // 
            // chkActiveWindowTryCaptureChildren
            // 
            this.chkActiveWindowTryCaptureChildren.AutoSize = true;
            this.chkActiveWindowTryCaptureChildren.Location = new System.Drawing.Point(16, 156);
            this.chkActiveWindowTryCaptureChildren.Name = "chkActiveWindowTryCaptureChildren";
            this.chkActiveWindowTryCaptureChildren.Size = new System.Drawing.Size(235, 17);
            this.chkActiveWindowTryCaptureChildren.TabIndex = 48;
            this.chkActiveWindowTryCaptureChildren.Text = "Capture Child Windows, Tooltips and Menus";
            this.ttZScreen.SetToolTip(this.chkActiveWindowTryCaptureChildren, "Only works when DWM is disabled");
            this.chkActiveWindowTryCaptureChildren.UseVisualStyleBackColor = true;
            this.chkActiveWindowTryCaptureChildren.CheckedChanged += new System.EventHandler(this.chkActiveWindowTryCaptureChilds_CheckedChanged);
            // 
            // cbActiveWindowGDIFreezeWindow
            // 
            this.cbActiveWindowGDIFreezeWindow.AutoSize = true;
            this.cbActiveWindowGDIFreezeWindow.Location = new System.Drawing.Point(16, 133);
            this.cbActiveWindowGDIFreezeWindow.Name = "cbActiveWindowGDIFreezeWindow";
            this.cbActiveWindowGDIFreezeWindow.Size = new System.Drawing.Size(168, 17);
            this.cbActiveWindowGDIFreezeWindow.TabIndex = 49;
            this.cbActiveWindowGDIFreezeWindow.Text = "Freeze window during capture";
            this.ttZScreen.SetToolTip(this.cbActiveWindowGDIFreezeWindow, "Avoids artifacts with moving images");
            this.cbActiveWindowGDIFreezeWindow.UseVisualStyleBackColor = true;
            this.cbActiveWindowGDIFreezeWindow.CheckedChanged += new System.EventHandler(this.chkActiveWindowGDIFreezeWindow_CheckedChanged);
            // 
            // chkSelectedWindowCleanTransparentCorners
            // 
            this.chkSelectedWindowCleanTransparentCorners.AutoSize = true;
            this.chkSelectedWindowCleanTransparentCorners.Location = new System.Drawing.Point(16, 110);
            this.chkSelectedWindowCleanTransparentCorners.Name = "chkSelectedWindowCleanTransparentCorners";
            this.chkSelectedWindowCleanTransparentCorners.Size = new System.Drawing.Size(147, 17);
            this.chkSelectedWindowCleanTransparentCorners.TabIndex = 44;
            this.chkSelectedWindowCleanTransparentCorners.Text = "Clean transparent corners";
            this.ttZScreen.SetToolTip(this.chkSelectedWindowCleanTransparentCorners, "Remove the background behind the transparent window corners");
            this.chkSelectedWindowCleanTransparentCorners.UseVisualStyleBackColor = true;
            this.chkSelectedWindowCleanTransparentCorners.CheckedChanged += new System.EventHandler(this.cbSelectedWindowCleanTransparentCorners_CheckedChanged);
            // 
            // chkSelectedWindowShowCheckers
            // 
            this.chkSelectedWindowShowCheckers.AutoSize = true;
            this.chkSelectedWindowShowCheckers.Location = new System.Drawing.Point(16, 40);
            this.chkSelectedWindowShowCheckers.Name = "chkSelectedWindowShowCheckers";
            this.chkSelectedWindowShowCheckers.Size = new System.Drawing.Size(242, 17);
            this.chkSelectedWindowShowCheckers.TabIndex = 46;
            this.chkSelectedWindowShowCheckers.Text = "Show checkerboard pattern behind the image";
            this.ttZScreen.SetToolTip(this.chkSelectedWindowShowCheckers, "Useful to visualize transparency");
            this.chkSelectedWindowShowCheckers.UseVisualStyleBackColor = true;
            this.chkSelectedWindowShowCheckers.CheckedChanged += new System.EventHandler(this.cbSelectedWindowShowCheckers_CheckedChanged);
            // 
            // chkSelectedWindowIncludeShadow
            // 
            this.chkSelectedWindowIncludeShadow.AutoSize = true;
            this.chkSelectedWindowIncludeShadow.Location = new System.Drawing.Point(16, 87);
            this.chkSelectedWindowIncludeShadow.Name = "chkSelectedWindowIncludeShadow";
            this.chkSelectedWindowIncludeShadow.Size = new System.Drawing.Size(131, 17);
            this.chkSelectedWindowIncludeShadow.TabIndex = 45;
            this.chkSelectedWindowIncludeShadow.Text = "Include shadow effect";
            this.ttZScreen.SetToolTip(this.chkSelectedWindowIncludeShadow, "Captures the real window shadow (GDI on Vista & 7), or fake it (DWM, XP)");
            this.chkSelectedWindowIncludeShadow.UseVisualStyleBackColor = true;
            this.chkSelectedWindowIncludeShadow.CheckedChanged += new System.EventHandler(this.cbSelectedWindowIncludeShadow_CheckedChanged);
            // 
            // chkActiveWindowPreferDWM
            // 
            this.chkActiveWindowPreferDWM.AutoSize = true;
            this.chkActiveWindowPreferDWM.Location = new System.Drawing.Point(16, 64);
            this.chkActiveWindowPreferDWM.Name = "chkActiveWindowPreferDWM";
            this.chkActiveWindowPreferDWM.Size = new System.Drawing.Size(184, 17);
            this.chkActiveWindowPreferDWM.TabIndex = 49;
            this.chkActiveWindowPreferDWM.Text = "Prefer Desktop Window Manager";
            this.ttZScreen.SetToolTip(this.chkActiveWindowPreferDWM, "Make use of DWM to capture the window");
            this.chkActiveWindowPreferDWM.UseVisualStyleBackColor = true;
            this.chkActiveWindowPreferDWM.CheckedChanged += new System.EventHandler(this.chkActiveWindowPreferDWM_CheckedChanged);
            // 
            // chkSelectedWindowCleanBackground
            // 
            this.chkSelectedWindowCleanBackground.AutoSize = true;
            this.chkSelectedWindowCleanBackground.Location = new System.Drawing.Point(16, 16);
            this.chkSelectedWindowCleanBackground.Name = "chkSelectedWindowCleanBackground";
            this.chkSelectedWindowCleanBackground.Size = new System.Drawing.Size(110, 17);
            this.chkSelectedWindowCleanBackground.TabIndex = 43;
            this.chkSelectedWindowCleanBackground.Text = "Clear background";
            this.ttZScreen.SetToolTip(this.chkSelectedWindowCleanBackground, "Clears background area that does not belong to the Active Window");
            this.chkSelectedWindowCleanBackground.UseVisualStyleBackColor = true;
            this.chkSelectedWindowCleanBackground.CheckedChanged += new System.EventHandler(this.cbSelectedWindowCleanBackground_CheckedChanged);
            // 
            // tpFreehandCropShot
            // 
            this.tpFreehandCropShot.Controls.Add(this.cbFreehandCropShowRectangleBorder);
            this.tpFreehandCropShot.Controls.Add(this.cbFreehandCropAutoClose);
            this.tpFreehandCropShot.Controls.Add(this.cbFreehandCropAutoUpload);
            this.tpFreehandCropShot.Controls.Add(this.cbFreehandCropShowHelpText);
            this.tpFreehandCropShot.ImageKey = "shape_square_edit.png";
            this.tpFreehandCropShot.Location = new System.Drawing.Point(4, 23);
            this.tpFreehandCropShot.Name = "tpFreehandCropShot";
            this.tpFreehandCropShot.Size = new System.Drawing.Size(791, 402);
            this.tpFreehandCropShot.TabIndex = 13;
            this.tpFreehandCropShot.Text = "Freehand Crop Shot";
            this.tpFreehandCropShot.UseVisualStyleBackColor = true;
            // 
            // cbFreehandCropShowRectangleBorder
            // 
            this.cbFreehandCropShowRectangleBorder.AutoSize = true;
            this.cbFreehandCropShowRectangleBorder.Location = new System.Drawing.Point(16, 88);
            this.cbFreehandCropShowRectangleBorder.Name = "cbFreehandCropShowRectangleBorder";
            this.cbFreehandCropShowRectangleBorder.Size = new System.Drawing.Size(229, 17);
            this.cbFreehandCropShowRectangleBorder.TabIndex = 3;
            this.cbFreehandCropShowRectangleBorder.Text = "Show rectangle border and size information";
            this.cbFreehandCropShowRectangleBorder.UseVisualStyleBackColor = true;
            this.cbFreehandCropShowRectangleBorder.CheckedChanged += new System.EventHandler(this.cbFreehandCropShowRectangleBorder_CheckedChanged);
            // 
            // cbFreehandCropAutoClose
            // 
            this.cbFreehandCropAutoClose.AutoSize = true;
            this.cbFreehandCropAutoClose.Location = new System.Drawing.Point(16, 64);
            this.cbFreehandCropAutoClose.Name = "cbFreehandCropAutoClose";
            this.cbFreehandCropAutoClose.Size = new System.Drawing.Size(336, 17);
            this.cbFreehandCropAutoClose.TabIndex = 2;
            this.cbFreehandCropAutoClose.Text = "Use right click to cancel upload instead of cleaning drawn regions";
            this.cbFreehandCropAutoClose.UseVisualStyleBackColor = true;
            this.cbFreehandCropAutoClose.CheckedChanged += new System.EventHandler(this.cbFreehandCropAutoClose_CheckedChanged);
            // 
            // cbFreehandCropAutoUpload
            // 
            this.cbFreehandCropAutoUpload.AutoSize = true;
            this.cbFreehandCropAutoUpload.Location = new System.Drawing.Point(16, 40);
            this.cbFreehandCropAutoUpload.Name = "cbFreehandCropAutoUpload";
            this.cbFreehandCropAutoUpload.Size = new System.Drawing.Size(221, 17);
            this.cbFreehandCropAutoUpload.TabIndex = 1;
            this.cbFreehandCropAutoUpload.Text = "Automatically upload after region is drawn";
            this.cbFreehandCropAutoUpload.UseVisualStyleBackColor = true;
            this.cbFreehandCropAutoUpload.CheckedChanged += new System.EventHandler(this.cbFreehandCropAutoUpload_CheckedChanged);
            // 
            // cbFreehandCropShowHelpText
            // 
            this.cbFreehandCropShowHelpText.AutoSize = true;
            this.cbFreehandCropShowHelpText.Location = new System.Drawing.Point(16, 16);
            this.cbFreehandCropShowHelpText.Name = "cbFreehandCropShowHelpText";
            this.cbFreehandCropShowHelpText.Size = new System.Drawing.Size(96, 17);
            this.cbFreehandCropShowHelpText.TabIndex = 0;
            this.cbFreehandCropShowHelpText.Text = "Show help text";
            this.cbFreehandCropShowHelpText.UseVisualStyleBackColor = true;
            this.cbFreehandCropShowHelpText.CheckedChanged += new System.EventHandler(this.cbFreehandCropShowHelpText_CheckedChanged);
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
            this.tpWatermark.Size = new System.Drawing.Size(791, 402);
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
            this.gbWatermarkGeneral.Controls.Add(this.chkWatermarkPosition);
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
            // chkWatermarkPosition
            // 
            this.chkWatermarkPosition.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.chkWatermarkPosition.FormattingEnabled = true;
            this.chkWatermarkPosition.Location = new System.Drawing.Point(88, 52);
            this.chkWatermarkPosition.Name = "chkWatermarkPosition";
            this.chkWatermarkPosition.Size = new System.Drawing.Size(121, 21);
            this.chkWatermarkPosition.TabIndex = 18;
            this.chkWatermarkPosition.SelectedIndexChanged += new System.EventHandler(this.cbWatermarkPosition_SelectedIndexChanged);
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
            this.tcWatermark.Location = new System.Drawing.Point(288, 4);
            this.tcWatermark.Name = "tcWatermark";
            this.tcWatermark.SelectedIndex = 0;
            this.tcWatermark.Size = new System.Drawing.Size(486, 389);
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
            this.tpWatermarkText.Size = new System.Drawing.Size(478, 362);
            this.tpWatermarkText.TabIndex = 0;
            this.tpWatermarkText.Text = "Text";
            this.tpWatermarkText.UseVisualStyleBackColor = true;
            // 
            // gbWatermarkBackground
            // 
            this.gbWatermarkBackground.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbWatermarkBackground.Controls.Add(this.lblRectangleCornerRadius);
            this.gbWatermarkBackground.Controls.Add(this.gbGradientMakerBasic);
            this.gbWatermarkBackground.Controls.Add(this.btnSelectGradient);
            this.gbWatermarkBackground.Controls.Add(this.cboUseCustomGradient);
            this.gbWatermarkBackground.Controls.Add(this.nudWatermarkCornerRadius);
            this.gbWatermarkBackground.Controls.Add(this.lblWatermarkCornerRadiusTip);
            this.gbWatermarkBackground.Location = new System.Drawing.Point(8, 136);
            this.gbWatermarkBackground.Name = "gbWatermarkBackground";
            this.gbWatermarkBackground.Size = new System.Drawing.Size(456, 216);
            this.gbWatermarkBackground.TabIndex = 25;
            this.gbWatermarkBackground.TabStop = false;
            this.gbWatermarkBackground.Text = "Text Background Settings";
            // 
            // lblRectangleCornerRadius
            // 
            this.lblRectangleCornerRadius.AutoSize = true;
            this.lblRectangleCornerRadius.Location = new System.Drawing.Point(12, 25);
            this.lblRectangleCornerRadius.Name = "lblRectangleCornerRadius";
            this.lblRectangleCornerRadius.Size = new System.Drawing.Size(128, 13);
            this.lblRectangleCornerRadius.TabIndex = 21;
            this.lblRectangleCornerRadius.Text = "Rectangle corner Radius:";
            // 
            // gbGradientMakerBasic
            // 
            this.gbGradientMakerBasic.Controls.Add(this.lblWatermarkBackColors);
            this.gbGradientMakerBasic.Controls.Add(this.trackWatermarkBackgroundTrans);
            this.gbGradientMakerBasic.Controls.Add(this.pbWatermarkGradient2);
            this.gbGradientMakerBasic.Controls.Add(this.cbWatermarkGradientType);
            this.gbGradientMakerBasic.Controls.Add(this.pbWatermarkBorderColor);
            this.gbGradientMakerBasic.Controls.Add(this.lblWatermarkGradientType);
            this.gbGradientMakerBasic.Controls.Add(this.pbWatermarkGradient1);
            this.gbGradientMakerBasic.Controls.Add(this.lblWatermarkBackTrans);
            this.gbGradientMakerBasic.Controls.Add(this.nudWatermarkBackTrans);
            this.gbGradientMakerBasic.Controls.Add(this.lblWatermarkBackColorsTip);
            this.gbGradientMakerBasic.Location = new System.Drawing.Point(12, 48);
            this.gbGradientMakerBasic.Name = "gbGradientMakerBasic";
            this.gbGradientMakerBasic.Size = new System.Drawing.Size(431, 122);
            this.gbGradientMakerBasic.TabIndex = 34;
            this.gbGradientMakerBasic.TabStop = false;
            this.gbGradientMakerBasic.Text = "Gradient Maker (Basic)";
            // 
            // lblWatermarkBackColors
            // 
            this.lblWatermarkBackColors.AutoSize = true;
            this.lblWatermarkBackColors.Location = new System.Drawing.Point(8, 25);
            this.lblWatermarkBackColors.Name = "lblWatermarkBackColors";
            this.lblWatermarkBackColors.Size = new System.Drawing.Size(100, 13);
            this.lblWatermarkBackColors.TabIndex = 12;
            this.lblWatermarkBackColors.Text = "Background Colors:";
            // 
            // trackWatermarkBackgroundTrans
            // 
            this.trackWatermarkBackgroundTrans.AutoSize = false;
            this.trackWatermarkBackgroundTrans.BackColor = System.Drawing.SystemColors.Window;
            this.trackWatermarkBackgroundTrans.Location = new System.Drawing.Point(152, 54);
            this.trackWatermarkBackgroundTrans.Maximum = 255;
            this.trackWatermarkBackgroundTrans.Name = "trackWatermarkBackgroundTrans";
            this.trackWatermarkBackgroundTrans.Size = new System.Drawing.Size(200, 24);
            this.trackWatermarkBackgroundTrans.TabIndex = 31;
            this.trackWatermarkBackgroundTrans.Tag = "Adjust Background Transparency. 0 = Invisible. ";
            this.trackWatermarkBackgroundTrans.TickFrequency = 5;
            this.trackWatermarkBackgroundTrans.TickStyle = System.Windows.Forms.TickStyle.None;
            this.trackWatermarkBackgroundTrans.Scroll += new System.EventHandler(this.trackWatermarkBackgroundTrans_Scroll);
            // 
            // pbWatermarkGradient2
            // 
            this.pbWatermarkGradient2.BackColor = System.Drawing.Color.Gray;
            this.pbWatermarkGradient2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pbWatermarkGradient2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pbWatermarkGradient2.Location = new System.Drawing.Point(144, 20);
            this.pbWatermarkGradient2.Name = "pbWatermarkGradient2";
            this.pbWatermarkGradient2.Size = new System.Drawing.Size(24, 24);
            this.pbWatermarkGradient2.TabIndex = 11;
            this.pbWatermarkGradient2.TabStop = false;
            this.pbWatermarkGradient2.Click += new System.EventHandler(this.pbWatermarkGradient2_Click);
            // 
            // cbWatermarkGradientType
            // 
            this.cbWatermarkGradientType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbWatermarkGradientType.FormattingEnabled = true;
            this.cbWatermarkGradientType.Location = new System.Drawing.Point(88, 89);
            this.cbWatermarkGradientType.Name = "cbWatermarkGradientType";
            this.cbWatermarkGradientType.Size = new System.Drawing.Size(121, 21);
            this.cbWatermarkGradientType.TabIndex = 25;
            this.cbWatermarkGradientType.SelectedIndexChanged += new System.EventHandler(this.cbWatermarkGradientType_SelectedIndexChanged);
            // 
            // pbWatermarkBorderColor
            // 
            this.pbWatermarkBorderColor.BackColor = System.Drawing.Color.Black;
            this.pbWatermarkBorderColor.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pbWatermarkBorderColor.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pbWatermarkBorderColor.Location = new System.Drawing.Point(176, 20);
            this.pbWatermarkBorderColor.Name = "pbWatermarkBorderColor";
            this.pbWatermarkBorderColor.Size = new System.Drawing.Size(24, 24);
            this.pbWatermarkBorderColor.TabIndex = 14;
            this.pbWatermarkBorderColor.TabStop = false;
            this.pbWatermarkBorderColor.Click += new System.EventHandler(this.pbWatermarkBorderColor_Click);
            // 
            // lblWatermarkGradientType
            // 
            this.lblWatermarkGradientType.AutoSize = true;
            this.lblWatermarkGradientType.Location = new System.Drawing.Point(8, 94);
            this.lblWatermarkGradientType.Name = "lblWatermarkGradientType";
            this.lblWatermarkGradientType.Size = new System.Drawing.Size(77, 13);
            this.lblWatermarkGradientType.TabIndex = 24;
            this.lblWatermarkGradientType.Text = "Gradient Type:";
            // 
            // pbWatermarkGradient1
            // 
            this.pbWatermarkGradient1.BackColor = System.Drawing.Color.White;
            this.pbWatermarkGradient1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pbWatermarkGradient1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pbWatermarkGradient1.Location = new System.Drawing.Point(112, 20);
            this.pbWatermarkGradient1.Name = "pbWatermarkGradient1";
            this.pbWatermarkGradient1.Size = new System.Drawing.Size(24, 24);
            this.pbWatermarkGradient1.TabIndex = 10;
            this.pbWatermarkGradient1.TabStop = false;
            this.pbWatermarkGradient1.Click += new System.EventHandler(this.pbWatermarkGradient1_Click);
            // 
            // lblWatermarkBackTrans
            // 
            this.lblWatermarkBackTrans.AutoSize = true;
            this.lblWatermarkBackTrans.Location = new System.Drawing.Point(8, 57);
            this.lblWatermarkBackTrans.Name = "lblWatermarkBackTrans";
            this.lblWatermarkBackTrans.Size = new System.Drawing.Size(136, 13);
            this.lblWatermarkBackTrans.TabIndex = 7;
            this.lblWatermarkBackTrans.Text = "Background Transparency:";
            // 
            // nudWatermarkBackTrans
            // 
            this.nudWatermarkBackTrans.BackColor = System.Drawing.SystemColors.Window;
            this.nudWatermarkBackTrans.Location = new System.Drawing.Point(352, 54);
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
            // lblWatermarkBackColorsTip
            // 
            this.lblWatermarkBackColorsTip.AutoSize = true;
            this.lblWatermarkBackColorsTip.Location = new System.Drawing.Point(208, 25);
            this.lblWatermarkBackColorsTip.Name = "lblWatermarkBackColorsTip";
            this.lblWatermarkBackColorsTip.Size = new System.Drawing.Size(195, 13);
            this.lblWatermarkBackColorsTip.TabIndex = 20;
            this.lblWatermarkBackColorsTip.Text = "1 && 2 = Gradient colors, 3 = Border color";
            // 
            // btnSelectGradient
            // 
            this.btnSelectGradient.Location = new System.Drawing.Point(196, 178);
            this.btnSelectGradient.Name = "btnSelectGradient";
            this.btnSelectGradient.Size = new System.Drawing.Size(112, 23);
            this.btnSelectGradient.TabIndex = 33;
            this.btnSelectGradient.Text = "Gradient Maker...";
            this.btnSelectGradient.UseVisualStyleBackColor = true;
            this.btnSelectGradient.Click += new System.EventHandler(this.btnSelectGradient_Click);
            // 
            // cboUseCustomGradient
            // 
            this.cboUseCustomGradient.AutoSize = true;
            this.cboUseCustomGradient.Location = new System.Drawing.Point(12, 180);
            this.cboUseCustomGradient.Name = "cboUseCustomGradient";
            this.cboUseCustomGradient.Size = new System.Drawing.Size(179, 17);
            this.cboUseCustomGradient.TabIndex = 32;
            this.cboUseCustomGradient.Text = "Use Gradient Maker (Advanced)";
            this.cboUseCustomGradient.UseVisualStyleBackColor = true;
            this.cboUseCustomGradient.CheckedChanged += new System.EventHandler(this.cbUseCustomGradient_CheckedChanged);
            // 
            // nudWatermarkCornerRadius
            // 
            this.nudWatermarkCornerRadius.Location = new System.Drawing.Point(148, 20);
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
            // lblWatermarkCornerRadiusTip
            // 
            this.lblWatermarkCornerRadiusTip.AutoSize = true;
            this.lblWatermarkCornerRadiusTip.Location = new System.Drawing.Point(204, 25);
            this.lblWatermarkCornerRadiusTip.Name = "lblWatermarkCornerRadiusTip";
            this.lblWatermarkCornerRadiusTip.Size = new System.Drawing.Size(146, 13);
            this.lblWatermarkCornerRadiusTip.TabIndex = 23;
            this.lblWatermarkCornerRadiusTip.Text = "(0 - 15) 0 = Normal Rectangle";
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
            this.tpWatermarkImage.Size = new System.Drawing.Size(478, 362);
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
            this.tpFileNaming.Controls.Add(this.lblMaxNameLength);
            this.tpFileNaming.Controls.Add(this.nudMaxNameLength);
            this.tpFileNaming.Controls.Add(this.btnResetIncrement);
            this.tpFileNaming.Controls.Add(this.gbOthersNaming);
            this.tpFileNaming.Controls.Add(this.gbCodeTitle);
            this.tpFileNaming.Controls.Add(this.gbActiveWindowNaming);
            this.tpFileNaming.Location = new System.Drawing.Point(4, 23);
            this.tpFileNaming.Name = "tpFileNaming";
            this.tpFileNaming.Size = new System.Drawing.Size(791, 402);
            this.tpFileNaming.TabIndex = 3;
            this.tpFileNaming.Text = "Naming Conventions";
            this.tpFileNaming.UseVisualStyleBackColor = true;
            // 
            // lblMaxNameLength
            // 
            this.lblMaxNameLength.AutoSize = true;
            this.lblMaxNameLength.Location = new System.Drawing.Point(248, 192);
            this.lblMaxNameLength.Name = "lblMaxNameLength";
            this.lblMaxNameLength.Size = new System.Drawing.Size(179, 13);
            this.lblMaxNameLength.TabIndex = 118;
            this.lblMaxNameLength.Text = "Maximum name length (0 = No limit) :";
            // 
            // nudMaxNameLength
            // 
            this.nudMaxNameLength.Location = new System.Drawing.Point(432, 189);
            this.nudMaxNameLength.Name = "nudMaxNameLength";
            this.nudMaxNameLength.Size = new System.Drawing.Size(56, 20);
            this.nudMaxNameLength.TabIndex = 117;
            this.nudMaxNameLength.ValueChanged += new System.EventHandler(this.nudMaxNameLength_ValueChanged);
            // 
            // btnResetIncrement
            // 
            this.btnResetIncrement.Location = new System.Drawing.Point(8, 280);
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
            this.gbCodeTitle.Text = "Environment Varables";
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
            this.tpCaptureQuality.Size = new System.Drawing.Size(791, 402);
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
            this.gbImageSize.Size = new System.Drawing.Size(768, 120);
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
            this.gbPictureQuality.Controls.Add(this.cbGIFQuality);
            this.gbPictureQuality.Controls.Add(this.lblGIFQuality);
            this.gbPictureQuality.Controls.Add(this.nudSwitchAfter);
            this.gbPictureQuality.Controls.Add(this.nudImageQuality);
            this.gbPictureQuality.Controls.Add(this.lblJPEGQualityPercentage);
            this.gbPictureQuality.Controls.Add(this.lblQuality);
            this.gbPictureQuality.Controls.Add(this.cboSwitchFormat);
            this.gbPictureQuality.Controls.Add(this.lblFileFormat);
            this.gbPictureQuality.Controls.Add(this.cboFileFormat);
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
            // cbGIFQuality
            // 
            this.cbGIFQuality.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbGIFQuality.FormattingEnabled = true;
            this.cbGIFQuality.Items.AddRange(new object[] {
            "Grayscale",
            "4 bit (16 colors)",
            "8 bit (256 colors)"});
            this.cbGIFQuality.Location = new System.Drawing.Point(240, 40);
            this.cbGIFQuality.Name = "cbGIFQuality";
            this.cbGIFQuality.Size = new System.Drawing.Size(121, 21);
            this.cbGIFQuality.TabIndex = 118;
            this.cbGIFQuality.SelectedIndexChanged += new System.EventHandler(this.cbGIFQuality_SelectedIndexChanged);
            // 
            // lblGIFQuality
            // 
            this.lblGIFQuality.AutoSize = true;
            this.lblGIFQuality.Location = new System.Drawing.Point(240, 24);
            this.lblGIFQuality.Name = "lblGIFQuality";
            this.lblGIFQuality.Size = new System.Drawing.Size(62, 13);
            this.lblGIFQuality.TabIndex = 117;
            this.lblGIFQuality.Text = "GIF Quality:";
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
            this.lblQuality.Size = new System.Drawing.Size(72, 13);
            this.lblQuality.TabIndex = 108;
            this.lblQuality.Text = "JPEG Quality:";
            // 
            // cboSwitchFormat
            // 
            this.cboSwitchFormat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboSwitchFormat.FormattingEnabled = true;
            this.cboSwitchFormat.Location = new System.Drawing.Point(128, 96);
            this.cboSwitchFormat.Name = "cboSwitchFormat";
            this.cboSwitchFormat.Size = new System.Drawing.Size(98, 21);
            this.cboSwitchFormat.TabIndex = 9;
            this.cboSwitchFormat.SelectedIndexChanged += new System.EventHandler(this.cboSwitchFormat_SelectedIndexChanged);
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
            // cboFileFormat
            // 
            this.cboFileFormat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboFileFormat.FormattingEnabled = true;
            this.cboFileFormat.Location = new System.Drawing.Point(16, 40);
            this.cboFileFormat.Name = "cboFileFormat";
            this.cboFileFormat.Size = new System.Drawing.Size(98, 21);
            this.cboFileFormat.TabIndex = 6;
            this.cboFileFormat.SelectedIndexChanged += new System.EventHandler(this.cboFileFormat_SelectedIndexChanged);
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
            // tpMainActions
            // 
            this.tpMainActions.Controls.Add(this.chkPerformActions);
            this.tpMainActions.Controls.Add(this.gbImageEditorSettings);
            this.tpMainActions.Controls.Add(this.lbSoftware);
            this.tpMainActions.Controls.Add(this.pgEditorsImage);
            this.tpMainActions.Controls.Add(this.btnRemoveImageEditor);
            this.tpMainActions.Controls.Add(this.btnAddImageSoftware);
            this.tpMainActions.ImageKey = "(none)";
            this.tpMainActions.Location = new System.Drawing.Point(4, 22);
            this.tpMainActions.Name = "tpMainActions";
            this.tpMainActions.Padding = new System.Windows.Forms.Padding(3);
            this.tpMainActions.Size = new System.Drawing.Size(805, 436);
            this.tpMainActions.TabIndex = 2;
            this.tpMainActions.Text = "Actions";
            this.tpMainActions.UseVisualStyleBackColor = true;
            // 
            // chkPerformActions
            // 
            this.chkPerformActions.AutoSize = true;
            this.chkPerformActions.Checked = true;
            this.chkPerformActions.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkPerformActions.Location = new System.Drawing.Point(590, 18);
            this.chkPerformActions.Name = "chkPerformActions";
            this.chkPerformActions.Size = new System.Drawing.Size(203, 17);
            this.chkPerformActions.TabIndex = 68;
            this.chkPerformActions.Text = "Perform &Actions after capturing Image";
            this.chkPerformActions.UseVisualStyleBackColor = true;
            this.chkPerformActions.CheckedChanged += new System.EventHandler(this.ChkEditorsEnableCheckedChanged);
            // 
            // gbImageEditorSettings
            // 
            this.gbImageEditorSettings.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbImageEditorSettings.Controls.Add(this.chkImageEditorAutoSave);
            this.gbImageEditorSettings.Location = new System.Drawing.Point(312, 296);
            this.gbImageEditorSettings.Name = "gbImageEditorSettings";
            this.gbImageEditorSettings.Size = new System.Drawing.Size(481, 56);
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
            // lbSoftware
            // 
            this.lbSoftware.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbSoftware.FormattingEnabled = true;
            this.lbSoftware.IntegralHeight = false;
            this.lbSoftware.Location = new System.Drawing.Point(3, 3);
            this.lbSoftware.Name = "lbSoftware";
            this.lbSoftware.Size = new System.Drawing.Size(293, 430);
            this.lbSoftware.TabIndex = 59;
            this.lbSoftware.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.LbSoftwareItemCheck);
            this.lbSoftware.MouseClick += new System.Windows.Forms.MouseEventHandler(this.LbSoftwareMouseClick);
            this.lbSoftware.SelectedIndexChanged += new System.EventHandler(this.lbSoftware_SelectedIndexChanged);
            // 
            // pgEditorsImage
            // 
            this.pgEditorsImage.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pgEditorsImage.Location = new System.Drawing.Point(312, 45);
            this.pgEditorsImage.Name = "pgEditorsImage";
            this.pgEditorsImage.PropertySort = System.Windows.Forms.PropertySort.NoSort;
            this.pgEditorsImage.Size = new System.Drawing.Size(481, 233);
            this.pgEditorsImage.TabIndex = 64;
            this.pgEditorsImage.ToolbarVisible = false;
            this.pgEditorsImage.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.pgEditorsImage_PropertyValueChanged);
            // 
            // btnRemoveImageEditor
            // 
            this.btnRemoveImageEditor.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnRemoveImageEditor.Location = new System.Drawing.Point(408, 13);
            this.btnRemoveImageEditor.Name = "btnRemoveImageEditor";
            this.btnRemoveImageEditor.Size = new System.Drawing.Size(88, 24);
            this.btnRemoveImageEditor.TabIndex = 58;
            this.btnRemoveImageEditor.Text = "&Remove";
            this.btnRemoveImageEditor.UseVisualStyleBackColor = true;
            this.btnRemoveImageEditor.Click += new System.EventHandler(this.btnDeleteImageSoftware_Click);
            // 
            // btnAddImageSoftware
            // 
            this.btnAddImageSoftware.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnAddImageSoftware.BackColor = System.Drawing.Color.Transparent;
            this.btnAddImageSoftware.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnAddImageSoftware.Location = new System.Drawing.Point(312, 13);
            this.btnAddImageSoftware.Name = "btnAddImageSoftware";
            this.btnAddImageSoftware.Size = new System.Drawing.Size(88, 24);
            this.btnAddImageSoftware.TabIndex = 0;
            this.btnAddImageSoftware.Text = "Add...";
            this.btnAddImageSoftware.UseVisualStyleBackColor = false;
            this.btnAddImageSoftware.Click += new System.EventHandler(this.btnAddImageSoftware_Click);
            // 
            // tpImageHosting
            // 
            this.tpImageHosting.Controls.Add(this.tcImages);
            this.tpImageHosting.ImageKey = "(none)";
            this.tpImageHosting.Location = new System.Drawing.Point(4, 22);
            this.tpImageHosting.Name = "tpImageHosting";
            this.tpImageHosting.Padding = new System.Windows.Forms.Padding(3);
            this.tpImageHosting.Size = new System.Drawing.Size(805, 436);
            this.tpImageHosting.TabIndex = 10;
            this.tpImageHosting.Text = "Image Hosting";
            this.tpImageHosting.UseVisualStyleBackColor = true;
            // 
            // tcImages
            // 
            this.tcImages.Controls.Add(this.tpImageUploaders);
            this.tcImages.Controls.Add(this.tpWebPageUpload);
            this.tcImages.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcImages.Location = new System.Drawing.Point(3, 3);
            this.tcImages.Name = "tcImages";
            this.tcImages.SelectedIndex = 0;
            this.tcImages.Size = new System.Drawing.Size(799, 430);
            this.tcImages.TabIndex = 5;
            // 
            // tpImageUploaders
            // 
            this.tpImageUploaders.Controls.Add(this.gbImageUploadRetry);
            this.tpImageUploaders.Controls.Add(this.gbImageUploaderOptions);
            this.tpImageUploaders.Location = new System.Drawing.Point(4, 22);
            this.tpImageUploaders.Name = "tpImageUploaders";
            this.tpImageUploaders.Padding = new System.Windows.Forms.Padding(3);
            this.tpImageUploaders.Size = new System.Drawing.Size(791, 404);
            this.tpImageUploaders.TabIndex = 0;
            this.tpImageUploaders.Text = "Image Uploaders";
            this.tpImageUploaders.UseVisualStyleBackColor = true;
            // 
            // gbImageUploadRetry
            // 
            this.gbImageUploadRetry.Controls.Add(this.chkImageUploadRandomRetryOnFail);
            this.gbImageUploadRetry.Controls.Add(this.lblErrorRetry);
            this.gbImageUploadRetry.Controls.Add(this.lblUploadDurationLimit);
            this.gbImageUploadRetry.Controls.Add(this.chkImageUploadRetryOnFail);
            this.gbImageUploadRetry.Controls.Add(this.cboImageUploadRetryOnTimeout);
            this.gbImageUploadRetry.Controls.Add(this.nudUploadDurationLimit);
            this.gbImageUploadRetry.Controls.Add(this.nudErrorRetry);
            this.gbImageUploadRetry.Location = new System.Drawing.Point(8, 120);
            this.gbImageUploadRetry.Name = "gbImageUploadRetry";
            this.gbImageUploadRetry.Size = new System.Drawing.Size(776, 128);
            this.gbImageUploadRetry.TabIndex = 8;
            this.gbImageUploadRetry.TabStop = false;
            this.gbImageUploadRetry.Text = "Retry Options";
            // 
            // chkImageUploadRandomRetryOnFail
            // 
            this.chkImageUploadRandomRetryOnFail.AutoSize = true;
            this.chkImageUploadRandomRetryOnFail.Location = new System.Drawing.Point(32, 72);
            this.chkImageUploadRandomRetryOnFail.Name = "chkImageUploadRandomRetryOnFail";
            this.chkImageUploadRandomRetryOnFail.Size = new System.Drawing.Size(192, 17);
            this.chkImageUploadRandomRetryOnFail.TabIndex = 12;
            this.chkImageUploadRandomRetryOnFail.Text = "Randomly select a valid destination";
            this.chkImageUploadRandomRetryOnFail.UseVisualStyleBackColor = true;
            this.chkImageUploadRandomRetryOnFail.CheckedChanged += new System.EventHandler(this.chkImageUploadRandomRetryOnFail_CheckedChanged);
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
            this.lblUploadDurationLimit.Location = new System.Drawing.Point(368, 96);
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
            this.ttZScreen.SetToolTip(this.chkImageUploadRetryOnFail, "This setting ignores Retry Count. Only happens between ImageShack and TinyPic unl" +
        "ess Random Destinations are enabled");
            this.chkImageUploadRetryOnFail.UseVisualStyleBackColor = true;
            this.chkImageUploadRetryOnFail.CheckedChanged += new System.EventHandler(this.cbImageUploadRetry_CheckedChanged);
            // 
            // cboImageUploadRetryOnTimeout
            // 
            this.cboImageUploadRetryOnTimeout.AutoSize = true;
            this.cboImageUploadRetryOnTimeout.Location = new System.Drawing.Point(16, 96);
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
            this.nudUploadDurationLimit.Location = new System.Drawing.Point(299, 94);
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
            this.gbImageUploaderOptions.Controls.Add(this.chkAutoSwitchFileUploader);
            this.gbImageUploaderOptions.Controls.Add(this.cbTinyPicSizeCheck);
            this.gbImageUploaderOptions.Location = new System.Drawing.Point(8, 8);
            this.gbImageUploaderOptions.Name = "gbImageUploaderOptions";
            this.gbImageUploaderOptions.Size = new System.Drawing.Size(776, 104);
            this.gbImageUploaderOptions.TabIndex = 7;
            this.gbImageUploaderOptions.TabStop = false;
            this.gbImageUploaderOptions.Text = "General Options";
            // 
            // chkAutoSwitchFileUploader
            // 
            this.chkAutoSwitchFileUploader.AutoSize = true;
            this.chkAutoSwitchFileUploader.Location = new System.Drawing.Point(16, 72);
            this.chkAutoSwitchFileUploader.Name = "chkAutoSwitchFileUploader";
            this.chkAutoSwitchFileUploader.Size = new System.Drawing.Size(465, 17);
            this.chkAutoSwitchFileUploader.TabIndex = 114;
            this.chkAutoSwitchFileUploader.Text = "Automatically switch to File Uploader if a user copies (Clipboard Upload) or drag" +
    "s a non-Image";
            this.chkAutoSwitchFileUploader.UseVisualStyleBackColor = true;
            this.chkAutoSwitchFileUploader.CheckedChanged += new System.EventHandler(this.chkAutoSwitchFTP_CheckedChanged);
            // 
            // cbTinyPicSizeCheck
            // 
            this.cbTinyPicSizeCheck.AutoSize = true;
            this.cbTinyPicSizeCheck.Location = new System.Drawing.Point(16, 48);
            this.cbTinyPicSizeCheck.Name = "cbTinyPicSizeCheck";
            this.cbTinyPicSizeCheck.Size = new System.Drawing.Size(440, 17);
            this.cbTinyPicSizeCheck.TabIndex = 7;
            this.cbTinyPicSizeCheck.Text = "Switch from TinyPic to ImageShack if the image dimensions are greater than 1600 p" +
    "ixels";
            this.cbTinyPicSizeCheck.UseVisualStyleBackColor = true;
            this.cbTinyPicSizeCheck.CheckedChanged += new System.EventHandler(this.cbTinyPicSizeCheck_CheckedChanged);
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
            this.tpWebPageUpload.Size = new System.Drawing.Size(791, 402);
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
            this.pWebPageImage.Size = new System.Drawing.Size(760, 313);
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
            this.tpTextServices.ImageKey = "(none)";
            this.tpTextServices.Location = new System.Drawing.Point(4, 22);
            this.tpTextServices.Name = "tpTextServices";
            this.tpTextServices.Padding = new System.Windows.Forms.Padding(3);
            this.tpTextServices.Size = new System.Drawing.Size(805, 436);
            this.tpTextServices.TabIndex = 13;
            this.tpTextServices.Text = "Text Services";
            this.tpTextServices.UseVisualStyleBackColor = true;
            // 
            // tcTextUploaders
            // 
            this.tcTextUploaders.Controls.Add(this.tpTreeGUI);
            this.tcTextUploaders.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcTextUploaders.Location = new System.Drawing.Point(3, 3);
            this.tcTextUploaders.Name = "tcTextUploaders";
            this.tcTextUploaders.SelectedIndex = 0;
            this.tcTextUploaders.Size = new System.Drawing.Size(799, 430);
            this.tcTextUploaders.TabIndex = 0;
            // 
            // tpTreeGUI
            // 
            this.tpTreeGUI.Controls.Add(this.pgIndexer);
            this.tpTreeGUI.Location = new System.Drawing.Point(4, 22);
            this.tpTreeGUI.Name = "tpTreeGUI";
            this.tpTreeGUI.Padding = new System.Windows.Forms.Padding(3);
            this.tpTreeGUI.Size = new System.Drawing.Size(791, 404);
            this.tpTreeGUI.TabIndex = 15;
            this.tpTreeGUI.Text = "Directory Indexer";
            this.tpTreeGUI.UseVisualStyleBackColor = true;
            // 
            // pgIndexer
            // 
            this.pgIndexer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pgIndexer.Location = new System.Drawing.Point(3, 3);
            this.pgIndexer.Name = "pgIndexer";
            this.pgIndexer.Size = new System.Drawing.Size(785, 398);
            this.pgIndexer.TabIndex = 0;
            // 
            // tpTranslator
            // 
            this.tpTranslator.Controls.Add(this.cbLanguageAutoDetect);
            this.tpTranslator.Controls.Add(this.txtAutoTranslate);
            this.tpTranslator.Controls.Add(this.cbAutoTranslate);
            this.tpTranslator.Controls.Add(this.btnTranslateTo1);
            this.tpTranslator.Controls.Add(this.cbClipboardTranslate);
            this.tpTranslator.Controls.Add(this.txtTranslateResult);
            this.tpTranslator.Controls.Add(this.txtLanguages);
            this.tpTranslator.Controls.Add(this.btnTranslate);
            this.tpTranslator.Controls.Add(this.txtTranslateText);
            this.tpTranslator.Controls.Add(this.lblToLanguage);
            this.tpTranslator.Controls.Add(this.lblFromLanguage);
            this.tpTranslator.Controls.Add(this.cbToLanguage);
            this.tpTranslator.Controls.Add(this.cbFromLanguage);
            this.tpTranslator.ImageKey = "(none)";
            this.tpTranslator.Location = new System.Drawing.Point(4, 22);
            this.tpTranslator.Name = "tpTranslator";
            this.tpTranslator.Padding = new System.Windows.Forms.Padding(3);
            this.tpTranslator.Size = new System.Drawing.Size(805, 436);
            this.tpTranslator.TabIndex = 1;
            this.tpTranslator.Text = "Translator";
            this.tpTranslator.UseVisualStyleBackColor = true;
            // 
            // cbLanguageAutoDetect
            // 
            this.cbLanguageAutoDetect.AutoSize = true;
            this.cbLanguageAutoDetect.Location = new System.Drawing.Point(224, 18);
            this.cbLanguageAutoDetect.Name = "cbLanguageAutoDetect";
            this.cbLanguageAutoDetect.Size = new System.Drawing.Size(128, 17);
            this.cbLanguageAutoDetect.TabIndex = 13;
            this.cbLanguageAutoDetect.Text = "Auto detect language";
            this.cbLanguageAutoDetect.UseVisualStyleBackColor = true;
            this.cbLanguageAutoDetect.CheckedChanged += new System.EventHandler(this.cbLanguageAutoDetect_CheckedChanged);
            // 
            // txtAutoTranslate
            // 
            this.txtAutoTranslate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtAutoTranslate.Location = new System.Drawing.Point(496, 273);
            this.txtAutoTranslate.Name = "txtAutoTranslate";
            this.txtAutoTranslate.Size = new System.Drawing.Size(56, 20);
            this.txtAutoTranslate.TabIndex = 11;
            this.txtAutoTranslate.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtAutoTranslate.TextChanged += new System.EventHandler(this.txtAutoTranslate_TextChanged);
            // 
            // cbAutoTranslate
            // 
            this.cbAutoTranslate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cbAutoTranslate.AutoSize = true;
            this.cbAutoTranslate.Location = new System.Drawing.Point(16, 275);
            this.cbAutoTranslate.Name = "cbAutoTranslate";
            this.cbAutoTranslate.Size = new System.Drawing.Size(477, 17);
            this.cbAutoTranslate.TabIndex = 10;
            this.cbAutoTranslate.Text = "If clipboard text length is smaller than this number then instead text upload, au" +
    "tomaticly translate:";
            this.ttZScreen.SetToolTip(this.cbAutoTranslate, "Maximum number of characters before Clipboard Upload switches from Translate to T" +
        "ext Upload.");
            this.cbAutoTranslate.UseVisualStyleBackColor = true;
            this.cbAutoTranslate.CheckedChanged += new System.EventHandler(this.cbAutoTranslate_CheckedChanged);
            // 
            // btnTranslateTo1
            // 
            this.btnTranslateTo1.AllowDrop = true;
            this.btnTranslateTo1.Location = new System.Drawing.Point(216, 208);
            this.btnTranslateTo1.Name = "btnTranslateTo1";
            this.btnTranslateTo1.Size = new System.Drawing.Size(136, 24);
            this.btnTranslateTo1.TabIndex = 9;
            this.btnTranslateTo1.Text = "???";
            this.btnTranslateTo1.UseVisualStyleBackColor = true;
            this.btnTranslateTo1.Click += new System.EventHandler(this.btnTranslateTo1_Click);
            this.btnTranslateTo1.DragDrop += new System.Windows.Forms.DragEventHandler(this.btnTranslateTo1_DragDrop);
            this.btnTranslateTo1.DragEnter += new System.Windows.Forms.DragEventHandler(this.btnTranslateTo1_DragEnter);
            // 
            // cbClipboardTranslate
            // 
            this.cbClipboardTranslate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cbClipboardTranslate.AutoSize = true;
            this.cbClipboardTranslate.Location = new System.Drawing.Point(16, 251);
            this.cbClipboardTranslate.Name = "cbClipboardTranslate";
            this.cbClipboardTranslate.Size = new System.Drawing.Size(230, 17);
            this.cbClipboardTranslate.TabIndex = 6;
            this.cbClipboardTranslate.Text = "Auto paste result to clipboard after translate";
            this.cbClipboardTranslate.UseVisualStyleBackColor = true;
            this.cbClipboardTranslate.CheckedChanged += new System.EventHandler(this.cbClipboardTranslate_CheckedChanged);
            // 
            // txtTranslateResult
            // 
            this.txtTranslateResult.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.txtTranslateResult.Location = new System.Drawing.Point(360, 104);
            this.txtTranslateResult.Multiline = true;
            this.txtTranslateResult.Name = "txtTranslateResult";
            this.txtTranslateResult.ReadOnly = true;
            this.txtTranslateResult.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtTranslateResult.Size = new System.Drawing.Size(336, 129);
            this.txtTranslateResult.TabIndex = 5;
            // 
            // txtLanguages
            // 
            this.txtLanguages.Location = new System.Drawing.Point(360, 80);
            this.txtLanguages.Name = "txtLanguages";
            this.txtLanguages.ReadOnly = true;
            this.txtLanguages.Size = new System.Drawing.Size(336, 20);
            this.txtLanguages.TabIndex = 4;
            // 
            // btnTranslate
            // 
            this.btnTranslate.Location = new System.Drawing.Point(16, 208);
            this.btnTranslate.Name = "btnTranslate";
            this.btnTranslate.Size = new System.Drawing.Size(192, 24);
            this.btnTranslate.TabIndex = 3;
            this.btnTranslate.Text = "Translate ( Ctrl + Enter )";
            this.btnTranslate.UseVisualStyleBackColor = true;
            this.btnTranslate.Click += new System.EventHandler(this.btnTranslate_Click);
            // 
            // txtTranslateText
            // 
            this.txtTranslateText.Location = new System.Drawing.Point(16, 80);
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
            this.lblToLanguage.Location = new System.Drawing.Point(16, 40);
            this.lblToLanguage.Name = "lblToLanguage";
            this.lblToLanguage.Size = new System.Drawing.Size(48, 32);
            this.lblToLanguage.TabIndex = 3;
            this.lblToLanguage.Text = "Target:";
            this.lblToLanguage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblToLanguage.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lblToLanguage_MouseDown);
            // 
            // lblFromLanguage
            // 
            this.lblFromLanguage.Location = new System.Drawing.Point(16, 10);
            this.lblFromLanguage.Name = "lblFromLanguage";
            this.lblFromLanguage.Size = new System.Drawing.Size(48, 32);
            this.lblFromLanguage.TabIndex = 2;
            this.lblFromLanguage.Text = "Source:";
            this.lblFromLanguage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cbToLanguage
            // 
            this.cbToLanguage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbToLanguage.Enabled = false;
            this.cbToLanguage.FormattingEnabled = true;
            this.cbToLanguage.Location = new System.Drawing.Point(72, 48);
            this.cbToLanguage.MaxDropDownItems = 20;
            this.cbToLanguage.Name = "cbToLanguage";
            this.cbToLanguage.Size = new System.Drawing.Size(144, 21);
            this.cbToLanguage.TabIndex = 1;
            this.cbToLanguage.SelectedIndexChanged += new System.EventHandler(this.cbToLanguage_SelectedIndexChanged);
            // 
            // cbFromLanguage
            // 
            this.cbFromLanguage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbFromLanguage.Enabled = false;
            this.cbFromLanguage.FormattingEnabled = true;
            this.cbFromLanguage.Location = new System.Drawing.Point(72, 16);
            this.cbFromLanguage.MaxDropDownItems = 20;
            this.cbFromLanguage.Name = "cbFromLanguage";
            this.cbFromLanguage.Size = new System.Drawing.Size(144, 21);
            this.cbFromLanguage.TabIndex = 0;
            this.cbFromLanguage.SelectedIndexChanged += new System.EventHandler(this.cbFromLanguage_SelectedIndexChanged);
            // 
            // tpOptions
            // 
            this.tpOptions.Controls.Add(this.tcOptions);
            this.tpOptions.ImageKey = "(none)";
            this.tpOptions.Location = new System.Drawing.Point(4, 22);
            this.tpOptions.Name = "tpOptions";
            this.tpOptions.Padding = new System.Windows.Forms.Padding(3);
            this.tpOptions.Size = new System.Drawing.Size(805, 436);
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
            this.tcOptions.Controls.Add(this.tpStats);
            this.tcOptions.Controls.Add(this.tpDebugLog);
            this.tcOptions.Controls.Add(this.tpOptionsAdv);
            this.tcOptions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcOptions.Location = new System.Drawing.Point(3, 3);
            this.tcOptions.Name = "tcOptions";
            this.tcOptions.SelectedIndex = 0;
            this.tcOptions.Size = new System.Drawing.Size(799, 430);
            this.tcOptions.TabIndex = 8;
            this.tcOptions.SelectedIndexChanged += new System.EventHandler(this.tcOptions_SelectedIndexChanged);
            // 
            // tpGeneral
            // 
            this.tpGeneral.Controls.Add(this.gbMonitorClipboard);
            this.tpGeneral.Controls.Add(this.gbUpdates);
            this.tpGeneral.Controls.Add(this.gbMisc);
            this.tpGeneral.Location = new System.Drawing.Point(4, 22);
            this.tpGeneral.Name = "tpGeneral";
            this.tpGeneral.Padding = new System.Windows.Forms.Padding(3);
            this.tpGeneral.Size = new System.Drawing.Size(791, 404);
            this.tpGeneral.TabIndex = 0;
            this.tpGeneral.Text = "General";
            this.tpGeneral.UseVisualStyleBackColor = true;
            // 
            // gbMonitorClipboard
            // 
            this.gbMonitorClipboard.Controls.Add(this.chkMonUrls);
            this.gbMonitorClipboard.Controls.Add(this.chkMonFiles);
            this.gbMonitorClipboard.Controls.Add(this.chkMonImages);
            this.gbMonitorClipboard.Controls.Add(this.chkMonText);
            this.gbMonitorClipboard.Location = new System.Drawing.Point(8, 144);
            this.gbMonitorClipboard.Name = "gbMonitorClipboard";
            this.gbMonitorClipboard.Size = new System.Drawing.Size(760, 56);
            this.gbMonitorClipboard.TabIndex = 9;
            this.gbMonitorClipboard.TabStop = false;
            this.gbMonitorClipboard.Text = "Monitor Clipboard";
            // 
            // chkMonUrls
            // 
            this.chkMonUrls.AutoSize = true;
            this.chkMonUrls.Location = new System.Drawing.Point(592, 24);
            this.chkMonUrls.Name = "chkMonUrls";
            this.chkMonUrls.Size = new System.Drawing.Size(53, 17);
            this.chkMonUrls.TabIndex = 3;
            this.chkMonUrls.Text = "URLs";
            this.chkMonUrls.UseVisualStyleBackColor = true;
            this.chkMonUrls.CheckedChanged += new System.EventHandler(this.chkMonUrls_CheckedChanged);
            // 
            // chkMonFiles
            // 
            this.chkMonFiles.AutoSize = true;
            this.chkMonFiles.Location = new System.Drawing.Point(424, 24);
            this.chkMonFiles.Name = "chkMonFiles";
            this.chkMonFiles.Size = new System.Drawing.Size(47, 17);
            this.chkMonFiles.TabIndex = 2;
            this.chkMonFiles.Text = "Files";
            this.chkMonFiles.UseVisualStyleBackColor = true;
            this.chkMonFiles.CheckedChanged += new System.EventHandler(this.chkMonFiles_CheckedChanged);
            // 
            // chkMonImages
            // 
            this.chkMonImages.AutoSize = true;
            this.chkMonImages.Location = new System.Drawing.Point(16, 24);
            this.chkMonImages.Name = "chkMonImages";
            this.chkMonImages.Size = new System.Drawing.Size(60, 17);
            this.chkMonImages.TabIndex = 1;
            this.chkMonImages.Text = "Images";
            this.chkMonImages.UseVisualStyleBackColor = true;
            this.chkMonImages.CheckedChanged += new System.EventHandler(this.chkMonImages_CheckedChanged);
            // 
            // chkMonText
            // 
            this.chkMonText.AutoSize = true;
            this.chkMonText.Location = new System.Drawing.Point(200, 24);
            this.chkMonText.Name = "chkMonText";
            this.chkMonText.Size = new System.Drawing.Size(47, 17);
            this.chkMonText.TabIndex = 0;
            this.chkMonText.Text = "Text";
            this.chkMonText.UseVisualStyleBackColor = true;
            this.chkMonText.CheckedChanged += new System.EventHandler(this.chkMonText_CheckedChanged);
            // 
            // gbUpdates
            // 
            this.gbUpdates.Controls.Add(this.chkCheckUpdatesBeta);
            this.gbUpdates.Controls.Add(this.lblUpdateInfo);
            this.gbUpdates.Controls.Add(this.btnCheckUpdate);
            this.gbUpdates.Controls.Add(this.chkCheckUpdates);
            this.gbUpdates.Location = new System.Drawing.Point(8, 208);
            this.gbUpdates.Name = "gbUpdates";
            this.gbUpdates.Size = new System.Drawing.Size(760, 96);
            this.gbUpdates.TabIndex = 8;
            this.gbUpdates.TabStop = false;
            this.gbUpdates.Text = "Check Updates";
            // 
            // chkCheckUpdatesBeta
            // 
            this.chkCheckUpdatesBeta.AutoSize = true;
            this.chkCheckUpdatesBeta.Location = new System.Drawing.Point(200, 24);
            this.chkCheckUpdatesBeta.Name = "chkCheckUpdatesBeta";
            this.chkCheckUpdatesBeta.Size = new System.Drawing.Size(126, 17);
            this.chkCheckUpdatesBeta.TabIndex = 7;
            this.chkCheckUpdatesBeta.Text = "Include beta updates";
            this.chkCheckUpdatesBeta.UseVisualStyleBackColor = true;
            this.chkCheckUpdatesBeta.CheckedChanged += new System.EventHandler(this.cbCheckUpdatesBeta_CheckedChanged);
            // 
            // lblUpdateInfo
            // 
            this.lblUpdateInfo.AutoSize = true;
            this.lblUpdateInfo.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblUpdateInfo.Location = new System.Drawing.Point(392, 24);
            this.lblUpdateInfo.Name = "lblUpdateInfo";
            this.lblUpdateInfo.Size = new System.Drawing.Size(116, 16);
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
            // chkCheckUpdates
            // 
            this.chkCheckUpdates.AutoSize = true;
            this.chkCheckUpdates.Location = new System.Drawing.Point(16, 24);
            this.chkCheckUpdates.Name = "chkCheckUpdates";
            this.chkCheckUpdates.Size = new System.Drawing.Size(162, 17);
            this.chkCheckUpdates.TabIndex = 1;
            this.chkCheckUpdates.Text = "Automatically check updates";
            this.chkCheckUpdates.UseVisualStyleBackColor = true;
            this.chkCheckUpdates.CheckedChanged += new System.EventHandler(this.cbCheckUpdates_CheckedChanged);
            // 
            // gbMisc
            // 
            this.gbMisc.BackColor = System.Drawing.Color.Transparent;
            this.gbMisc.Controls.Add(this.chkHotkeys);
            this.gbMisc.Controls.Add(this.chkShellExt);
            this.gbMisc.Controls.Add(this.chkWindows7TaskbarIntegration);
            this.gbMisc.Controls.Add(this.cbAutoSaveSettings);
            this.gbMisc.Controls.Add(this.cbShowHelpBalloonTips);
            this.gbMisc.Controls.Add(this.chkShowTaskbar);
            this.gbMisc.Controls.Add(this.chkOpenMainWindow);
            this.gbMisc.Controls.Add(this.chkStartWin);
            this.gbMisc.Location = new System.Drawing.Point(8, 8);
            this.gbMisc.Name = "gbMisc";
            this.gbMisc.Size = new System.Drawing.Size(760, 128);
            this.gbMisc.TabIndex = 7;
            this.gbMisc.TabStop = false;
            this.gbMisc.Text = "Program";
            // 
            // chkHotkeys
            // 
            this.chkHotkeys.AutoSize = true;
            this.chkHotkeys.Checked = true;
            this.chkHotkeys.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkHotkeys.Location = new System.Drawing.Point(424, 96);
            this.chkHotkeys.Name = "chkHotkeys";
            this.chkHotkeys.Size = new System.Drawing.Size(165, 17);
            this.chkHotkeys.TabIndex = 9;
            this.chkHotkeys.Text = "Keyboard Hotkeys integration";
            this.chkHotkeys.UseVisualStyleBackColor = true;
            this.chkHotkeys.CheckedChanged += new System.EventHandler(this.chkHotkeys_CheckedChanged);
            // 
            // chkShellExt
            // 
            this.chkShellExt.AutoSize = true;
            this.chkShellExt.Location = new System.Drawing.Point(424, 72);
            this.chkShellExt.Name = "chkShellExt";
            this.chkShellExt.Size = new System.Drawing.Size(278, 17);
            this.chkShellExt.TabIndex = 9;
            this.chkShellExt.Text = "Show \"Upload using ZScreen\" in Shell Context Menu";
            this.chkShellExt.UseVisualStyleBackColor = true;
            this.chkShellExt.CheckedChanged += new System.EventHandler(this.chkShellExt_CheckedChanged);
            // 
            // chkWindows7TaskbarIntegration
            // 
            this.chkWindows7TaskbarIntegration.AutoSize = true;
            this.chkWindows7TaskbarIntegration.Location = new System.Drawing.Point(424, 48);
            this.chkWindows7TaskbarIntegration.Name = "chkWindows7TaskbarIntegration";
            this.chkWindows7TaskbarIntegration.Size = new System.Drawing.Size(173, 17);
            this.chkWindows7TaskbarIntegration.TabIndex = 8;
            this.chkWindows7TaskbarIntegration.Text = "Windows 7 &Taskbar integration";
            this.chkWindows7TaskbarIntegration.UseVisualStyleBackColor = true;
            this.chkWindows7TaskbarIntegration.CheckedChanged += new System.EventHandler(this.chkWindows7TaskbarIntegration_CheckedChanged);
            // 
            // cbAutoSaveSettings
            // 
            this.cbAutoSaveSettings.AutoSize = true;
            this.cbAutoSaveSettings.Location = new System.Drawing.Point(16, 72);
            this.cbAutoSaveSettings.Name = "cbAutoSaveSettings";
            this.cbAutoSaveSettings.Size = new System.Drawing.Size(244, 17);
            this.cbAutoSaveSettings.TabIndex = 7;
            this.cbAutoSaveSettings.Text = "Save settings on resize or while switching tabs";
            this.ttZScreen.SetToolTip(this.cbAutoSaveSettings, "ZScreen still saves settings before close");
            this.cbAutoSaveSettings.UseVisualStyleBackColor = true;
            this.cbAutoSaveSettings.CheckedChanged += new System.EventHandler(this.cbAutoSaveSettings_CheckedChanged);
            // 
            // cbShowHelpBalloonTips
            // 
            this.cbShowHelpBalloonTips.AutoSize = true;
            this.cbShowHelpBalloonTips.Location = new System.Drawing.Point(16, 96);
            this.cbShowHelpBalloonTips.Name = "cbShowHelpBalloonTips";
            this.cbShowHelpBalloonTips.Size = new System.Drawing.Size(156, 17);
            this.cbShowHelpBalloonTips.TabIndex = 5;
            this.cbShowHelpBalloonTips.Text = "Show Help via Balloon Tips";
            this.cbShowHelpBalloonTips.UseVisualStyleBackColor = true;
            this.cbShowHelpBalloonTips.CheckedChanged += new System.EventHandler(this.cbShowHelpBalloonTips_CheckedChanged);
            // 
            // chkShowTaskbar
            // 
            this.chkShowTaskbar.AutoSize = true;
            this.chkShowTaskbar.Location = new System.Drawing.Point(16, 48);
            this.chkShowTaskbar.Name = "chkShowTaskbar";
            this.chkShowTaskbar.Size = new System.Drawing.Size(174, 17);
            this.chkShowTaskbar.TabIndex = 3;
            this.chkShowTaskbar.Text = "Show Main Window in Taskbar";
            this.chkShowTaskbar.UseVisualStyleBackColor = true;
            this.chkShowTaskbar.CheckedChanged += new System.EventHandler(this.cbShowTaskbar_CheckedChanged);
            // 
            // chkOpenMainWindow
            // 
            this.chkOpenMainWindow.AutoSize = true;
            this.chkOpenMainWindow.Location = new System.Drawing.Point(16, 24);
            this.chkOpenMainWindow.Name = "chkOpenMainWindow";
            this.chkOpenMainWindow.Size = new System.Drawing.Size(158, 17);
            this.chkOpenMainWindow.TabIndex = 2;
            this.chkOpenMainWindow.Text = "Open Main Window on load";
            this.chkOpenMainWindow.UseVisualStyleBackColor = true;
            this.chkOpenMainWindow.CheckedChanged += new System.EventHandler(this.cbOpenMainWindow_CheckedChanged);
            // 
            // chkStartWin
            // 
            this.chkStartWin.AutoSize = true;
            this.chkStartWin.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.chkStartWin.Location = new System.Drawing.Point(424, 24);
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
            this.tpProxy.Size = new System.Drawing.Size(791, 403);
            this.tpProxy.TabIndex = 6;
            this.tpProxy.Text = "Proxy";
            this.tpProxy.UseVisualStyleBackColor = true;
            // 
            // gpProxySettings
            // 
            this.gpProxySettings.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gpProxySettings.Controls.Add(this.cboProxyConfig);
            this.gpProxySettings.Location = new System.Drawing.Point(16, 314);
            this.gpProxySettings.Name = "gpProxySettings";
            this.gpProxySettings.Size = new System.Drawing.Size(759, 72);
            this.gpProxySettings.TabIndex = 117;
            this.gpProxySettings.TabStop = false;
            this.gpProxySettings.Text = "Proxy Settings";
            // 
            // cboProxyConfig
            // 
            this.cboProxyConfig.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboProxyConfig.FormattingEnabled = true;
            this.cboProxyConfig.Location = new System.Drawing.Point(16, 24);
            this.cboProxyConfig.Name = "cboProxyConfig";
            this.cboProxyConfig.Size = new System.Drawing.Size(264, 21);
            this.cboProxyConfig.TabIndex = 114;
            this.cboProxyConfig.SelectedIndexChanged += new System.EventHandler(this.cboProxyConfig_SelectedIndexChanged);
            // 
            // ucProxyAccounts
            // 
            this.ucProxyAccounts.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ucProxyAccounts.Location = new System.Drawing.Point(3, 3);
            this.ucProxyAccounts.Margin = new System.Windows.Forms.Padding(4);
            this.ucProxyAccounts.Name = "ucProxyAccounts";
            this.ucProxyAccounts.Size = new System.Drawing.Size(787, 313);
            this.ucProxyAccounts.TabIndex = 0;
            // 
            // tpInteraction
            // 
            this.tpInteraction.Controls.Add(this.gbWindowButtons);
            this.tpInteraction.Controls.Add(this.gbActionsToolbarSettings);
            this.tpInteraction.Controls.Add(this.gbDropBox);
            this.tpInteraction.Controls.Add(this.gbAppearance);
            this.tpInteraction.Location = new System.Drawing.Point(4, 22);
            this.tpInteraction.Name = "tpInteraction";
            this.tpInteraction.Padding = new System.Windows.Forms.Padding(3);
            this.tpInteraction.Size = new System.Drawing.Size(791, 403);
            this.tpInteraction.TabIndex = 5;
            this.tpInteraction.Text = "Interaction";
            this.tpInteraction.UseVisualStyleBackColor = true;
            // 
            // gbWindowButtons
            // 
            this.gbWindowButtons.Controls.Add(this.cboCloseButtonAction);
            this.gbWindowButtons.Controls.Add(this.cboMinimizeButtonAction);
            this.gbWindowButtons.Controls.Add(this.lblCloseButtonAction);
            this.gbWindowButtons.Controls.Add(this.lblMinimizeButtonAction);
            this.gbWindowButtons.Location = new System.Drawing.Point(8, 168);
            this.gbWindowButtons.Name = "gbWindowButtons";
            this.gbWindowButtons.Size = new System.Drawing.Size(752, 56);
            this.gbWindowButtons.TabIndex = 14;
            this.gbWindowButtons.TabStop = false;
            this.gbWindowButtons.Text = "Windows Buttons Behavior";
            // 
            // cboCloseButtonAction
            // 
            this.cboCloseButtonAction.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboCloseButtonAction.FormattingEnabled = true;
            this.cboCloseButtonAction.Location = new System.Drawing.Point(544, 20);
            this.cboCloseButtonAction.Name = "cboCloseButtonAction";
            this.cboCloseButtonAction.Size = new System.Drawing.Size(144, 21);
            this.cboCloseButtonAction.TabIndex = 13;
            this.cboCloseButtonAction.SelectedIndexChanged += new System.EventHandler(this.cbCloseButtonAction_SelectedIndexChanged);
            // 
            // cboMinimizeButtonAction
            // 
            this.cboMinimizeButtonAction.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboMinimizeButtonAction.FormattingEnabled = true;
            this.cboMinimizeButtonAction.Location = new System.Drawing.Point(136, 20);
            this.cboMinimizeButtonAction.Name = "cboMinimizeButtonAction";
            this.cboMinimizeButtonAction.Size = new System.Drawing.Size(144, 21);
            this.cboMinimizeButtonAction.TabIndex = 12;
            this.cboMinimizeButtonAction.SelectedIndexChanged += new System.EventHandler(this.cbMinimizeButtonAction_SelectedIndexChanged);
            // 
            // lblCloseButtonAction
            // 
            this.lblCloseButtonAction.AutoSize = true;
            this.lblCloseButtonAction.Location = new System.Drawing.Point(440, 24);
            this.lblCloseButtonAction.Name = "lblCloseButtonAction";
            this.lblCloseButtonAction.Size = new System.Drawing.Size(101, 13);
            this.lblCloseButtonAction.TabIndex = 10;
            this.lblCloseButtonAction.Text = "Close button action:";
            // 
            // lblMinimizeButtonAction
            // 
            this.lblMinimizeButtonAction.AutoSize = true;
            this.lblMinimizeButtonAction.Location = new System.Drawing.Point(16, 24);
            this.lblMinimizeButtonAction.Name = "lblMinimizeButtonAction";
            this.lblMinimizeButtonAction.Size = new System.Drawing.Size(115, 13);
            this.lblMinimizeButtonAction.TabIndex = 11;
            this.lblMinimizeButtonAction.Text = "Minimize button action:";
            // 
            // gbActionsToolbarSettings
            // 
            this.gbActionsToolbarSettings.Controls.Add(this.cbCloseQuickActions);
            this.gbActionsToolbarSettings.Location = new System.Drawing.Point(8, 296);
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
            this.gbDropBox.Location = new System.Drawing.Point(8, 232);
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
            this.gbAppearance.Controls.Add(this.cbCopyClipboardAfterTask);
            this.gbAppearance.Controls.Add(this.chkTwitterEnable);
            this.gbAppearance.Controls.Add(this.cbCompleteSound);
            this.gbAppearance.Controls.Add(this.chkCaptureFallback);
            this.gbAppearance.Controls.Add(this.cbShowUploadDuration);
            this.gbAppearance.Controls.Add(this.chkBalloonTipOpenLink);
            this.gbAppearance.Controls.Add(this.cbShowPopup);
            this.gbAppearance.Controls.Add(this.lblTrayFlash);
            this.gbAppearance.Controls.Add(this.nudFlashIconCount);
            this.gbAppearance.Location = new System.Drawing.Point(8, 8);
            this.gbAppearance.Name = "gbAppearance";
            this.gbAppearance.Size = new System.Drawing.Size(752, 152);
            this.gbAppearance.TabIndex = 5;
            this.gbAppearance.TabStop = false;
            this.gbAppearance.Text = "After completing a task";
            // 
            // cbCopyClipboardAfterTask
            // 
            this.cbCopyClipboardAfterTask.AutoSize = true;
            this.cbCopyClipboardAfterTask.Location = new System.Drawing.Point(16, 24);
            this.cbCopyClipboardAfterTask.Name = "cbCopyClipboardAfterTask";
            this.cbCopyClipboardAfterTask.Size = new System.Drawing.Size(279, 17);
            this.cbCopyClipboardAfterTask.TabIndex = 10;
            this.cbCopyClipboardAfterTask.Text = "Copy URL to clipboard after upload/task is completed";
            this.cbCopyClipboardAfterTask.UseVisualStyleBackColor = true;
            this.cbCopyClipboardAfterTask.CheckedChanged += new System.EventHandler(this.cbCopyClipboardAfterTask_CheckedChanged);
            // 
            // chkTwitterEnable
            // 
            this.chkTwitterEnable.AutoSize = true;
            this.chkTwitterEnable.Location = new System.Drawing.Point(352, 48);
            this.chkTwitterEnable.Name = "chkTwitterEnable";
            this.chkTwitterEnable.Size = new System.Drawing.Size(202, 17);
            this.chkTwitterEnable.TabIndex = 9;
            this.chkTwitterEnable.Text = "Update status in Twitter automatically";
            this.chkTwitterEnable.UseVisualStyleBackColor = true;
            this.chkTwitterEnable.CheckedChanged += new System.EventHandler(this.chkTwitterEnable_CheckedChanged);
            // 
            // cbCompleteSound
            // 
            this.cbCompleteSound.AutoSize = true;
            this.cbCompleteSound.Location = new System.Drawing.Point(16, 48);
            this.cbCompleteSound.Name = "cbCompleteSound";
            this.cbCompleteSound.Size = new System.Drawing.Size(224, 17);
            this.cbCompleteSound.TabIndex = 5;
            this.cbCompleteSound.Text = "Play sound after upload/task is completed";
            this.cbCompleteSound.UseVisualStyleBackColor = true;
            this.cbCompleteSound.CheckedChanged += new System.EventHandler(this.cbCompleteSound_CheckedChanged);
            // 
            // chkCaptureFallback
            // 
            this.chkCaptureFallback.AutoSize = true;
            this.chkCaptureFallback.Location = new System.Drawing.Point(352, 72);
            this.chkCaptureFallback.Name = "chkCaptureFallback";
            this.chkCaptureFallback.Size = new System.Drawing.Size(332, 17);
            this.chkCaptureFallback.TabIndex = 7;
            this.chkCaptureFallback.Text = "Capture entire screen if Active Window capture or Crop Shot fails";
            this.chkCaptureFallback.UseVisualStyleBackColor = true;
            this.chkCaptureFallback.CheckedChanged += new System.EventHandler(this.chkCaptureFallback_CheckedChanged);
            // 
            // cbShowUploadDuration
            // 
            this.cbShowUploadDuration.AutoSize = true;
            this.cbShowUploadDuration.Location = new System.Drawing.Point(16, 120);
            this.cbShowUploadDuration.Name = "cbShowUploadDuration";
            this.cbShowUploadDuration.Size = new System.Drawing.Size(191, 17);
            this.cbShowUploadDuration.TabIndex = 8;
            this.cbShowUploadDuration.Text = "Show upload duration in balloon tip";
            this.cbShowUploadDuration.UseVisualStyleBackColor = true;
            this.cbShowUploadDuration.CheckedChanged += new System.EventHandler(this.cbShowUploadDuration_CheckedChanged);
            // 
            // chkBalloonTipOpenLink
            // 
            this.chkBalloonTipOpenLink.AutoSize = true;
            this.chkBalloonTipOpenLink.Location = new System.Drawing.Point(16, 96);
            this.chkBalloonTipOpenLink.Name = "chkBalloonTipOpenLink";
            this.chkBalloonTipOpenLink.Size = new System.Drawing.Size(189, 17);
            this.chkBalloonTipOpenLink.TabIndex = 6;
            this.chkBalloonTipOpenLink.Text = "Open URL/File on balloon tip click";
            this.chkBalloonTipOpenLink.UseVisualStyleBackColor = true;
            this.chkBalloonTipOpenLink.CheckedChanged += new System.EventHandler(this.chkBalloonTipOpenLink_CheckedChanged);
            // 
            // cbShowPopup
            // 
            this.cbShowPopup.AutoSize = true;
            this.cbShowPopup.Location = new System.Drawing.Point(16, 72);
            this.cbShowPopup.Name = "cbShowPopup";
            this.cbShowPopup.Size = new System.Drawing.Size(250, 17);
            this.cbShowPopup.TabIndex = 5;
            this.cbShowPopup.Text = "Show balloon tip after upload/task is completed";
            this.cbShowPopup.UseVisualStyleBackColor = true;
            this.cbShowPopup.CheckedChanged += new System.EventHandler(this.cbShowPopup_CheckedChanged);
            // 
            // lblTrayFlash
            // 
            this.lblTrayFlash.AutoSize = true;
            this.lblTrayFlash.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblTrayFlash.Location = new System.Drawing.Point(350, 24);
            this.lblTrayFlash.Name = "lblTrayFlash";
            this.lblTrayFlash.Size = new System.Drawing.Size(315, 13);
            this.lblTrayFlash.TabIndex = 3;
            this.lblTrayFlash.Text = "Number of times tray icon should flash after an upload is complete";
            // 
            // nudFlashIconCount
            // 
            this.nudFlashIconCount.Location = new System.Drawing.Point(672, 20);
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
            this.tpAdvPaths.Controls.Add(this.chkPreferSystemFolders);
            this.tpAdvPaths.Controls.Add(this.gbRoot);
            this.tpAdvPaths.Controls.Add(this.gbImages);
            this.tpAdvPaths.Controls.Add(this.gbSettingsExportImport);
            this.tpAdvPaths.Controls.Add(this.gbCache);
            this.tpAdvPaths.Location = new System.Drawing.Point(4, 22);
            this.tpAdvPaths.Name = "tpAdvPaths";
            this.tpAdvPaths.Size = new System.Drawing.Size(791, 403);
            this.tpAdvPaths.TabIndex = 2;
            this.tpAdvPaths.Text = "Paths";
            this.tpAdvPaths.UseVisualStyleBackColor = true;
            // 
            // chkPreferSystemFolders
            // 
            this.chkPreferSystemFolders.AutoSize = true;
            this.chkPreferSystemFolders.Location = new System.Drawing.Point(16, 16);
            this.chkPreferSystemFolders.Name = "chkPreferSystemFolders";
            this.chkPreferSystemFolders.Size = new System.Drawing.Size(254, 17);
            this.chkPreferSystemFolders.TabIndex = 117;
            this.chkPreferSystemFolders.Text = "&Prefer System Folders to store Settings and Data";
            this.chkPreferSystemFolders.UseVisualStyleBackColor = true;
            this.chkPreferSystemFolders.CheckedChanged += new System.EventHandler(this.chkPreferSystemFolders_CheckedChanged);
            // 
            // gbRoot
            // 
            this.gbRoot.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbRoot.Controls.Add(this.btnViewRootDir);
            this.gbRoot.Controls.Add(this.btnBrowseRootDir);
            this.gbRoot.Controls.Add(this.txtRootFolder);
            this.gbRoot.Location = new System.Drawing.Point(8, 40);
            this.gbRoot.Name = "gbRoot";
            this.gbRoot.Size = new System.Drawing.Size(765, 64);
            this.gbRoot.TabIndex = 117;
            this.gbRoot.TabStop = false;
            this.gbRoot.Text = "Root";
            // 
            // btnViewRootDir
            // 
            this.btnViewRootDir.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnViewRootDir.Location = new System.Drawing.Point(649, 22);
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
            this.btnBrowseRootDir.Location = new System.Drawing.Point(560, 22);
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
            this.txtRootFolder.Size = new System.Drawing.Size(528, 20);
            this.txtRootFolder.TabIndex = 0;
            this.txtRootFolder.Tag = "Path of the Root folder that holds Images, Text, Cache, Settings and Temp folders" +
    "";
            // 
            // gbImages
            // 
            this.gbImages.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbImages.BackColor = System.Drawing.Color.Transparent;
            this.gbImages.Controls.Add(this.btnBrowseImagesDir);
            this.gbImages.Controls.Add(this.btnMoveImageFiles);
            this.gbImages.Controls.Add(this.lblImagesFolderPattern);
            this.gbImages.Controls.Add(this.lblImagesFolderPatternPreview);
            this.gbImages.Controls.Add(this.txtImagesFolderPattern);
            this.gbImages.Controls.Add(this.chkDeleteLocal);
            this.gbImages.Controls.Add(this.btnViewImagesDir);
            this.gbImages.Controls.Add(this.txtImagesDir);
            this.gbImages.Location = new System.Drawing.Point(8, 112);
            this.gbImages.Name = "gbImages";
            this.gbImages.Size = new System.Drawing.Size(765, 120);
            this.gbImages.TabIndex = 114;
            this.gbImages.TabStop = false;
            this.gbImages.Text = "Images";
            // 
            // btnBrowseImagesDir
            // 
            this.btnBrowseImagesDir.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBrowseImagesDir.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnBrowseImagesDir.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnBrowseImagesDir.Location = new System.Drawing.Point(560, 21);
            this.btnBrowseImagesDir.Name = "btnBrowseImagesDir";
            this.btnBrowseImagesDir.Size = new System.Drawing.Size(80, 24);
            this.btnBrowseImagesDir.TabIndex = 117;
            this.btnBrowseImagesDir.Text = "Relocate...";
            this.btnBrowseImagesDir.UseVisualStyleBackColor = true;
            this.btnBrowseImagesDir.Click += new System.EventHandler(this.BtnBrowseImagesDirClick);
            // 
            // btnMoveImageFiles
            // 
            this.btnMoveImageFiles.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMoveImageFiles.Location = new System.Drawing.Point(577, 56);
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
            // chkDeleteLocal
            // 
            this.chkDeleteLocal.AutoSize = true;
            this.chkDeleteLocal.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.chkDeleteLocal.Location = new System.Drawing.Point(16, 88);
            this.chkDeleteLocal.Name = "chkDeleteLocal";
            this.chkDeleteLocal.Size = new System.Drawing.Size(283, 17);
            this.chkDeleteLocal.TabIndex = 0;
            this.chkDeleteLocal.Text = "Delete captured screenshots after upload is completed";
            this.chkDeleteLocal.UseVisualStyleBackColor = true;
            this.chkDeleteLocal.CheckedChanged += new System.EventHandler(this.cbDeleteLocal_CheckedChanged);
            // 
            // btnViewImagesDir
            // 
            this.btnViewImagesDir.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnViewImagesDir.Location = new System.Drawing.Point(649, 21);
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
            this.txtImagesDir.Size = new System.Drawing.Size(528, 20);
            this.txtImagesDir.TabIndex = 1;
            this.txtImagesDir.TextChanged += new System.EventHandler(this.txtFileDirectory_TextChanged);
            // 
            // gbSettingsExportImport
            // 
            this.gbSettingsExportImport.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbSettingsExportImport.BackColor = System.Drawing.Color.Transparent;
            this.gbSettingsExportImport.Controls.Add(this.btnSettingsDefault);
            this.gbSettingsExportImport.Controls.Add(this.btnSettingsExport);
            this.gbSettingsExportImport.Controls.Add(this.btnFTPExport);
            this.gbSettingsExportImport.Controls.Add(this.btnFTPImport);
            this.gbSettingsExportImport.Controls.Add(this.btnSettingsImport);
            this.gbSettingsExportImport.Location = new System.Drawing.Point(8, 336);
            this.gbSettingsExportImport.Name = "gbSettingsExportImport";
            this.gbSettingsExportImport.Size = new System.Drawing.Size(765, 56);
            this.gbSettingsExportImport.TabIndex = 6;
            this.gbSettingsExportImport.TabStop = false;
            this.gbSettingsExportImport.Text = "Backup and Restore";
            // 
            // btnSettingsDefault
            // 
            this.btnSettingsDefault.AutoSize = true;
            this.btnSettingsDefault.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnSettingsDefault.Location = new System.Drawing.Point(224, 21);
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
            this.btnSettingsExport.Location = new System.Drawing.Point(120, 21);
            this.btnSettingsExport.Name = "btnSettingsExport";
            this.btnSettingsExport.Size = new System.Drawing.Size(97, 23);
            this.btnSettingsExport.TabIndex = 1;
            this.btnSettingsExport.Text = "Export Settings...";
            this.btnSettingsExport.UseVisualStyleBackColor = true;
            this.btnSettingsExport.Click += new System.EventHandler(this.btnSettingsExport_Click);
            // 
            // btnFTPExport
            // 
            this.btnFTPExport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnFTPExport.AutoSize = true;
            this.btnFTPExport.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnFTPExport.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnFTPExport.Location = new System.Drawing.Point(624, 21);
            this.btnFTPExport.Name = "btnFTPExport";
            this.btnFTPExport.Size = new System.Drawing.Size(127, 23);
            this.btnFTPExport.TabIndex = 38;
            this.btnFTPExport.Text = "Export FTP Accounts...";
            this.btnFTPExport.UseVisualStyleBackColor = true;
            this.btnFTPExport.Click += new System.EventHandler(this.btnExportAccounts_Click);
            // 
            // btnFTPImport
            // 
            this.btnFTPImport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnFTPImport.AutoSize = true;
            this.btnFTPImport.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnFTPImport.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnFTPImport.Location = new System.Drawing.Point(488, 21);
            this.btnFTPImport.Name = "btnFTPImport";
            this.btnFTPImport.Size = new System.Drawing.Size(126, 23);
            this.btnFTPImport.TabIndex = 39;
            this.btnFTPImport.Text = "Import FTP Accounts...";
            this.btnFTPImport.UseVisualStyleBackColor = true;
            this.btnFTPImport.Click += new System.EventHandler(this.btnAccsImport_Click);
            // 
            // btnSettingsImport
            // 
            this.btnSettingsImport.AutoSize = true;
            this.btnSettingsImport.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnSettingsImport.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnSettingsImport.Location = new System.Drawing.Point(16, 21);
            this.btnSettingsImport.Name = "btnSettingsImport";
            this.btnSettingsImport.Size = new System.Drawing.Size(96, 23);
            this.btnSettingsImport.TabIndex = 0;
            this.btnSettingsImport.Text = "Import Settings...";
            this.btnSettingsImport.UseVisualStyleBackColor = true;
            this.btnSettingsImport.Click += new System.EventHandler(this.btnSettingsImport_Click);
            // 
            // gbCache
            // 
            this.gbCache.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbCache.BackColor = System.Drawing.Color.Transparent;
            this.gbCache.Controls.Add(this.btnViewCacheDir);
            this.gbCache.Controls.Add(this.lblCacheSize);
            this.gbCache.Controls.Add(this.lblMebibytes);
            this.gbCache.Controls.Add(this.nudCacheSize);
            this.gbCache.Controls.Add(this.txtCacheDir);
            this.gbCache.Location = new System.Drawing.Point(8, 240);
            this.gbCache.Name = "gbCache";
            this.gbCache.Size = new System.Drawing.Size(765, 88);
            this.gbCache.TabIndex = 1;
            this.gbCache.TabStop = false;
            this.gbCache.Text = "Cache";
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
            this.txtCacheDir.Size = new System.Drawing.Size(623, 20);
            this.txtCacheDir.TabIndex = 0;
            // 
            // tpStats
            // 
            this.tpStats.Controls.Add(this.btnOpenZScreenTester);
            this.tpStats.Controls.Add(this.gbStatistics);
            this.tpStats.Controls.Add(this.gbLastSource);
            this.tpStats.Location = new System.Drawing.Point(4, 22);
            this.tpStats.Name = "tpStats";
            this.tpStats.Padding = new System.Windows.Forms.Padding(3);
            this.tpStats.Size = new System.Drawing.Size(791, 403);
            this.tpStats.TabIndex = 1;
            this.tpStats.Text = "Statistics";
            this.tpStats.UseVisualStyleBackColor = true;
            // 
            // btnOpenZScreenTester
            // 
            this.btnOpenZScreenTester.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOpenZScreenTester.Location = new System.Drawing.Point(608, 336);
            this.btnOpenZScreenTester.Name = "btnOpenZScreenTester";
            this.btnOpenZScreenTester.Size = new System.Drawing.Size(160, 23);
            this.btnOpenZScreenTester.TabIndex = 9;
            this.btnOpenZScreenTester.Text = "Open ZScreen Tester...";
            this.btnOpenZScreenTester.UseVisualStyleBackColor = true;
            this.btnOpenZScreenTester.Click += new System.EventHandler(this.btnOpenZScreenTester_Click);
            // 
            // gbStatistics
            // 
            this.gbStatistics.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbStatistics.Controls.Add(this.btnDebugStart);
            this.gbStatistics.Controls.Add(this.rtbDebugInfo);
            this.gbStatistics.Location = new System.Drawing.Point(8, 8);
            this.gbStatistics.Name = "gbStatistics";
            this.gbStatistics.Size = new System.Drawing.Size(775, 310);
            this.gbStatistics.TabIndex = 28;
            this.gbStatistics.TabStop = false;
            this.gbStatistics.Text = "Statistics";
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
            // rtbDebugInfo
            // 
            this.rtbDebugInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rtbDebugInfo.BackColor = System.Drawing.Color.WhiteSmoke;
            this.rtbDebugInfo.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.rtbDebugInfo.Location = new System.Drawing.Point(16, 56);
            this.rtbDebugInfo.Name = "rtbDebugInfo";
            this.rtbDebugInfo.ReadOnly = true;
            this.rtbDebugInfo.Size = new System.Drawing.Size(745, 244);
            this.rtbDebugInfo.TabIndex = 27;
            this.rtbDebugInfo.Text = "";
            this.rtbDebugInfo.WordWrap = false;
            // 
            // gbLastSource
            // 
            this.gbLastSource.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbLastSource.Controls.Add(this.btnOpenSourceString);
            this.gbLastSource.Controls.Add(this.btnOpenSourceText);
            this.gbLastSource.Controls.Add(this.btnOpenSourceBrowser);
            this.gbLastSource.Location = new System.Drawing.Point(8, 328);
            this.gbLastSource.Name = "gbLastSource";
            this.gbLastSource.Size = new System.Drawing.Size(409, 64);
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
            this.btnOpenSourceText.Location = new System.Drawing.Point(144, 24);
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
            this.btnOpenSourceBrowser.Location = new System.Drawing.Point(272, 24);
            this.btnOpenSourceBrowser.Name = "btnOpenSourceBrowser";
            this.btnOpenSourceBrowser.Size = new System.Drawing.Size(120, 23);
            this.btnOpenSourceBrowser.TabIndex = 22;
            this.btnOpenSourceBrowser.Text = "Open in Browser";
            this.btnOpenSourceBrowser.UseVisualStyleBackColor = true;
            this.btnOpenSourceBrowser.Click += new System.EventHandler(this.btnOpenSourceBrowser_Click);
            // 
            // tpDebugLog
            // 
            this.tpDebugLog.Controls.Add(this.rtbDebugLog);
            this.tpDebugLog.Location = new System.Drawing.Point(4, 22);
            this.tpDebugLog.Name = "tpDebugLog";
            this.tpDebugLog.Padding = new System.Windows.Forms.Padding(3);
            this.tpDebugLog.Size = new System.Drawing.Size(791, 403);
            this.tpDebugLog.TabIndex = 7;
            this.tpDebugLog.Text = "Debug";
            this.tpDebugLog.UseVisualStyleBackColor = true;
            // 
            // rtbDebugLog
            // 
            this.rtbDebugLog.BackColor = System.Drawing.Color.WhiteSmoke;
            this.rtbDebugLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbDebugLog.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.rtbDebugLog.Location = new System.Drawing.Point(3, 3);
            this.rtbDebugLog.Name = "rtbDebugLog";
            this.rtbDebugLog.ReadOnly = true;
            this.rtbDebugLog.Size = new System.Drawing.Size(785, 397);
            this.rtbDebugLog.TabIndex = 0;
            this.rtbDebugLog.Text = "";
            this.rtbDebugLog.WordWrap = false;
            // 
            // tpOptionsAdv
            // 
            this.tpOptionsAdv.Controls.Add(this.pgApp);
            this.tpOptionsAdv.Location = new System.Drawing.Point(4, 22);
            this.tpOptionsAdv.Name = "tpOptionsAdv";
            this.tpOptionsAdv.Padding = new System.Windows.Forms.Padding(3);
            this.tpOptionsAdv.Size = new System.Drawing.Size(791, 403);
            this.tpOptionsAdv.TabIndex = 3;
            this.tpOptionsAdv.Text = "Advanced";
            this.tpOptionsAdv.UseVisualStyleBackColor = true;
            // 
            // pgApp
            // 
            this.pgApp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pgApp.Location = new System.Drawing.Point(3, 3);
            this.pgApp.Name = "pgApp";
            this.pgApp.Size = new System.Drawing.Size(785, 397);
            this.pgApp.TabIndex = 0;
            this.pgApp.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.pgApp_PropertyValueChanged);
            // 
            // tpHistory
            // 
            this.tpHistory.Controls.Add(this.label1);
            this.tpHistory.Controls.Add(this.btnHistoryOpen);
            this.tpHistory.Controls.Add(this.cbHistorySave);
            this.tpHistory.Controls.Add(this.nudHistoryMaxItems);
            this.tpHistory.Controls.Add(this.cbAddFailedScreenshot);
            this.tpHistory.Location = new System.Drawing.Point(4, 22);
            this.tpHistory.Name = "tpHistory";
            this.tpHistory.Padding = new System.Windows.Forms.Padding(3);
            this.tpHistory.Size = new System.Drawing.Size(805, 436);
            this.tpHistory.TabIndex = 14;
            this.tpHistory.Text = "History";
            this.tpHistory.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 64);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(172, 13);
            this.label1.TabIndex = 11;
            this.label1.Text = "Maximum number of items in history";
            // 
            // btnHistoryOpen
            // 
            this.btnHistoryOpen.Location = new System.Drawing.Point(16, 16);
            this.btnHistoryOpen.Name = "btnHistoryOpen";
            this.btnHistoryOpen.Size = new System.Drawing.Size(128, 23);
            this.btnHistoryOpen.TabIndex = 0;
            this.btnHistoryOpen.Text = "&Open History...";
            this.btnHistoryOpen.UseVisualStyleBackColor = true;
            this.btnHistoryOpen.Click += new System.EventHandler(this.btnHistoryOpen_Click);
            // 
            // cbHistorySave
            // 
            this.cbHistorySave.AutoSize = true;
            this.cbHistorySave.Location = new System.Drawing.Point(16, 101);
            this.cbHistorySave.Name = "cbHistorySave";
            this.cbHistorySave.Size = new System.Drawing.Size(105, 17);
            this.cbHistorySave.TabIndex = 10;
            this.cbHistorySave.Text = "Save History List";
            this.cbHistorySave.UseVisualStyleBackColor = true;
            this.cbHistorySave.CheckedChanged += new System.EventHandler(this.cbHistorySave_CheckedChanged);
            // 
            // nudHistoryMaxItems
            // 
            this.nudHistoryMaxItems.Location = new System.Drawing.Point(192, 61);
            this.nudHistoryMaxItems.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.nudHistoryMaxItems.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
            this.nudHistoryMaxItems.Name = "nudHistoryMaxItems";
            this.nudHistoryMaxItems.Size = new System.Drawing.Size(72, 20);
            this.nudHistoryMaxItems.TabIndex = 4;
            this.nudHistoryMaxItems.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.nudHistoryMaxItems.ValueChanged += new System.EventHandler(this.nudHistoryMaxItems_ValueChanged);
            // 
            // cbAddFailedScreenshot
            // 
            this.cbAddFailedScreenshot.AutoSize = true;
            this.cbAddFailedScreenshot.Location = new System.Drawing.Point(16, 128);
            this.cbAddFailedScreenshot.Name = "cbAddFailedScreenshot";
            this.cbAddFailedScreenshot.Size = new System.Drawing.Size(143, 17);
            this.cbAddFailedScreenshot.TabIndex = 7;
            this.cbAddFailedScreenshot.Text = "Add failed task to History";
            this.cbAddFailedScreenshot.UseVisualStyleBackColor = true;
            this.cbAddFailedScreenshot.CheckedChanged += new System.EventHandler(this.cbAddFailedScreenshot_CheckedChanged);
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
            this.ClientSize = new System.Drawing.Size(817, 466);
            this.Controls.Add(this.tcApp);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(822, 497);
            this.Name = "ZScreen";
            this.Padding = new System.Windows.Forms.Padding(2);
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ZScreen";
            this.WindowState = System.Windows.Forms.FormWindowState.Minimized;
            this.Deactivate += new System.EventHandler(this.ZScreen_Deactivate);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ZScreen_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.ZScreen_FormClosed);
            this.Load += new System.EventHandler(this.ZScreen_Load);
            this.Shown += new System.EventHandler(this.ZScreen_Shown);
            this.Leave += new System.EventHandler(this.ZScreen_Leave);
            this.Resize += new System.EventHandler(this.ZScreen_Resize);
            this.cmTray.ResumeLayout(false);
            this.tcApp.ResumeLayout(false);
            this.tpMain.ResumeLayout(false);
            this.tpMain.PerformLayout();
            this.gbImageSettings.ResumeLayout(false);
            this.gbImageSettings.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbLogo)).EndInit();
            this.tpDestinations.ResumeLayout(false);
            this.tcDestinations.ResumeLayout(false);
            this.tpDestFTP.ResumeLayout(false);
            this.gbFTPSettings.ResumeLayout(false);
            this.gbFTPSettings.PerformLayout();
            this.tpDestDropbox.ResumeLayout(false);
            this.tpDestDropbox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbDropboxLogo)).EndInit();
            this.tpDestLocalhost.ResumeLayout(false);
            this.tpDestRapidShare.ResumeLayout(false);
            this.tpDestRapidShare.PerformLayout();
            this.tpDestSendSpace.ResumeLayout(false);
            this.tpDestSendSpace.PerformLayout();
            this.tpDestFlickr.ResumeLayout(false);
            this.tpDestImageShack.ResumeLayout(false);
            this.tpDestImageShack.PerformLayout();
            this.gbImageShack.ResumeLayout(false);
            this.gbImageShack.PerformLayout();
            this.tpDestImgur.ResumeLayout(false);
            this.tpDestImgur.PerformLayout();
            this.gbImgurUserAccount.ResumeLayout(false);
            this.gbImgurUserAccount.PerformLayout();
            this.tpDestImageBam.ResumeLayout(false);
            this.gbImageBamGalleries.ResumeLayout(false);
            this.gbImageBamLinks.ResumeLayout(false);
            this.gbImageBamLinks.PerformLayout();
            this.gbImageBamApiKeys.ResumeLayout(false);
            this.gbImageBamApiKeys.PerformLayout();
            this.tpDestTinyPic.ResumeLayout(false);
            this.tpDestTinyPic.PerformLayout();
            this.gbTinyPic.ResumeLayout(false);
            this.gbTinyPic.PerformLayout();
            this.tpDestTwitter.ResumeLayout(false);
            this.tlpTwitter.ResumeLayout(false);
            this.panelTwitter.ResumeLayout(false);
            this.gbTwitterOthers.ResumeLayout(false);
            this.gbTwitterOthers.PerformLayout();
            this.tpDestMindTouch.ResumeLayout(false);
            this.gbMindTouchOptions.ResumeLayout(false);
            this.gbMindTouchOptions.PerformLayout();
            this.tpDestMediaWiki.ResumeLayout(false);
            this.tpDestCustom.ResumeLayout(false);
            this.tpDestCustom.PerformLayout();
            this.gbImageUploaders.ResumeLayout(false);
            this.gbImageUploaders.PerformLayout();
            this.gbRegexp.ResumeLayout(false);
            this.gbRegexp.PerformLayout();
            this.gbArguments.ResumeLayout(false);
            this.gbArguments.PerformLayout();
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
            this.tpActivewindow.ResumeLayout(false);
            this.tpActivewindow.PerformLayout();
            this.tpFreehandCropShot.ResumeLayout(false);
            this.tpFreehandCropShot.PerformLayout();
            this.tpWatermark.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbWatermarkShow)).EndInit();
            this.gbWatermarkGeneral.ResumeLayout(false);
            this.gbWatermarkGeneral.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudWatermarkOffset)).EndInit();
            this.tcWatermark.ResumeLayout(false);
            this.tpWatermarkText.ResumeLayout(false);
            this.gbWatermarkBackground.ResumeLayout(false);
            this.gbWatermarkBackground.PerformLayout();
            this.gbGradientMakerBasic.ResumeLayout(false);
            this.gbGradientMakerBasic.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackWatermarkBackgroundTrans)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbWatermarkGradient2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbWatermarkBorderColor)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbWatermarkGradient1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudWatermarkBackTrans)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudWatermarkCornerRadius)).EndInit();
            this.gbWatermarkText.ResumeLayout(false);
            this.gbWatermarkText.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackWatermarkFontTrans)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudWatermarkFontTrans)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbWatermarkFontColor)).EndInit();
            this.tpWatermarkImage.ResumeLayout(false);
            this.tpWatermarkImage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudWatermarkImageScale)).EndInit();
            this.tpFileNaming.ResumeLayout(false);
            this.tpFileNaming.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudMaxNameLength)).EndInit();
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
            this.tpMainActions.ResumeLayout(false);
            this.tpMainActions.PerformLayout();
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
            this.tpWebPageUpload.ResumeLayout(false);
            this.tpWebPageUpload.PerformLayout();
            this.pWebPageImage.ResumeLayout(false);
            this.pWebPageImage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbWebPageImage)).EndInit();
            this.tpTextServices.ResumeLayout(false);
            this.tcTextUploaders.ResumeLayout(false);
            this.tpTreeGUI.ResumeLayout(false);
            this.tpTranslator.ResumeLayout(false);
            this.tpTranslator.PerformLayout();
            this.tpOptions.ResumeLayout(false);
            this.tcOptions.ResumeLayout(false);
            this.tpGeneral.ResumeLayout(false);
            this.gbMonitorClipboard.ResumeLayout(false);
            this.gbMonitorClipboard.PerformLayout();
            this.gbUpdates.ResumeLayout(false);
            this.gbUpdates.PerformLayout();
            this.gbMisc.ResumeLayout(false);
            this.gbMisc.PerformLayout();
            this.tpProxy.ResumeLayout(false);
            this.gpProxySettings.ResumeLayout(false);
            this.tpInteraction.ResumeLayout(false);
            this.gbWindowButtons.ResumeLayout(false);
            this.gbWindowButtons.PerformLayout();
            this.gbActionsToolbarSettings.ResumeLayout(false);
            this.gbActionsToolbarSettings.PerformLayout();
            this.gbDropBox.ResumeLayout(false);
            this.gbDropBox.PerformLayout();
            this.gbAppearance.ResumeLayout(false);
            this.gbAppearance.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudFlashIconCount)).EndInit();
            this.tpAdvPaths.ResumeLayout(false);
            this.tpAdvPaths.PerformLayout();
            this.gbRoot.ResumeLayout(false);
            this.gbRoot.PerformLayout();
            this.gbImages.ResumeLayout(false);
            this.gbImages.PerformLayout();
            this.gbSettingsExportImport.ResumeLayout(false);
            this.gbSettingsExportImport.PerformLayout();
            this.gbCache.ResumeLayout(false);
            this.gbCache.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudCacheSize)).EndInit();
            this.tpStats.ResumeLayout(false);
            this.gbStatistics.ResumeLayout(false);
            this.gbLastSource.ResumeLayout(false);
            this.tpDebugLog.ResumeLayout(false);
            this.tpOptionsAdv.ResumeLayout(false);
            this.tpHistory.ResumeLayout(false);
            this.tpHistory.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudHistoryMaxItems)).EndInit();
            this.ResumeLayout(false);

        }

        internal System.Windows.Forms.GroupBox gbCache;
        internal System.Windows.Forms.GroupBox gbImages;
        internal System.Windows.Forms.Button btnBrowseImagesDir;
        private System.Windows.Forms.GroupBox gbWindowButtons;
        private System.Windows.Forms.CheckBox chkPerformActions;

        #endregion Windows Form Designer generated code

        internal System.Windows.Forms.NotifyIcon niTray;
        internal System.Windows.Forms.ContextMenuStrip cmTray;
        internal System.Windows.Forms.ToolStripMenuItem tsmExitZScreen;
        internal System.Windows.Forms.ToolStripMenuItem tsmEditinImageSoftware;
        internal System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        internal System.Windows.Forms.ToolStripMenuItem tsmiTabs;
        internal System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        internal System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        internal System.Windows.Forms.ToolStripMenuItem tsmViewLocalDirectory;
        internal System.Windows.Forms.ToolStripMenuItem tsmImageDest;
        internal System.Windows.Forms.ToolStripMenuItem tsmCopytoClipboardMode;
        internal System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        internal System.Windows.Forms.ToolStripMenuItem tsmQuickOptions;
        internal System.Windows.Forms.ToolStripMenuItem tsmActions;
        internal System.Windows.Forms.ToolStripMenuItem tsmCropShot;
        internal System.Windows.Forms.ToolStripMenuItem tsmClipboardUpload;
        internal System.Windows.Forms.ToolStripMenuItem tsmEntireScreen;
        internal System.Windows.Forms.ToolStripMenuItem tsmLastCropShot;
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
        private NumericUpDownTimer nudScreenshotDelay;
        internal System.Windows.Forms.Label lblCopytoClipboard;
        internal System.Windows.Forms.ComboBox cboClipboardTextMode;
        internal System.Windows.Forms.CheckBox chkManualNaming;
        internal System.Windows.Forms.CheckBox chkShowCursor;
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
        internal System.Windows.Forms.CheckBox chkCropShowMagnifyingGlass;
        internal System.Windows.Forms.CheckBox chkCropShowBigCross;
        internal System.Windows.Forms.CheckBox chkCropDynamicCrosshair;
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
        internal System.Windows.Forms.CheckBox chkRegionHotkeyInfo;
        internal System.Windows.Forms.ComboBox chkCropStyle;
        internal System.Windows.Forms.CheckBox chkRegionRectangleInfo;
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
        internal System.Windows.Forms.ComboBox cboSwitchFormat;
        internal System.Windows.Forms.Label lblFileFormat;
        internal System.Windows.Forms.ComboBox cboFileFormat;
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
        internal System.Windows.Forms.ComboBox chkWatermarkPosition;
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
        internal System.Windows.Forms.TabPage tpMainActions;
        internal System.Windows.Forms.Button btnRemoveImageEditor;
        internal System.Windows.Forms.CheckedListBox lbSoftware;
        internal System.Windows.Forms.Button btnAddImageSoftware;
        internal System.Windows.Forms.GroupBox gbFTPSettings;
        internal System.Windows.Forms.CheckBox chkAutoSwitchFileUploader;
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
        internal System.Windows.Forms.CheckBox chkImageUploadRetryOnFail;
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
        internal System.Windows.Forms.TabPage tpDestCustom;
        internal System.Windows.Forms.RichTextBox txtUploadersLog;
        internal System.Windows.Forms.Button btnUploadersTest;
        internal System.Windows.Forms.TextBox txtFullImage;
        internal System.Windows.Forms.TextBox txtThumbnail;
        internal System.Windows.Forms.Label lblFullImage;
        internal System.Windows.Forms.Label lblThumbnail;
        internal System.Windows.Forms.GroupBox gbImageUploaders;
        internal System.Windows.Forms.ListBox lbImageUploader;
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
        internal System.Windows.Forms.CheckBox cbClipboardTranslate;
        internal System.Windows.Forms.TextBox txtTranslateResult;
        internal System.Windows.Forms.TextBox txtLanguages;
        internal System.Windows.Forms.Button btnTranslate;
        internal System.Windows.Forms.TextBox txtTranslateText;
        internal System.Windows.Forms.Label lblToLanguage;
        internal System.Windows.Forms.Label lblFromLanguage;
        internal System.Windows.Forms.ComboBox cbToLanguage;
        internal System.Windows.Forms.ComboBox cbFromLanguage;
        internal System.Windows.Forms.TabPage tpOptions;
        internal System.Windows.Forms.TabControl tcOptions;
        internal System.Windows.Forms.TabPage tpGeneral;
        internal System.Windows.Forms.GroupBox gbUpdates;
        internal System.Windows.Forms.Label lblUpdateInfo;
        internal System.Windows.Forms.Button btnCheckUpdate;
        internal System.Windows.Forms.CheckBox chkCheckUpdates;
        internal System.Windows.Forms.GroupBox gbMisc;
        internal System.Windows.Forms.CheckBox chkShowTaskbar;
        internal System.Windows.Forms.CheckBox chkOpenMainWindow;
        internal System.Windows.Forms.CheckBox chkStartWin;
        internal System.Windows.Forms.TabPage tpAdvPaths;
        internal System.Windows.Forms.GroupBox gbRoot;
        internal System.Windows.Forms.Button btnViewRootDir;
        internal System.Windows.Forms.Button btnBrowseRootDir;
        internal System.Windows.Forms.TextBox txtRootFolder;
        internal System.Windows.Forms.CheckBox chkDeleteLocal;
        internal System.Windows.Forms.Button btnViewImagesDir;
        internal System.Windows.Forms.TextBox txtImagesDir;
        internal System.Windows.Forms.Button btnSettingsDefault;
        internal System.Windows.Forms.Button btnSettingsExport;
        internal System.Windows.Forms.Button btnSettingsImport;
        internal System.Windows.Forms.Button btnViewCacheDir;
        internal System.Windows.Forms.Label lblCacheSize;
        internal System.Windows.Forms.Label lblMebibytes;
        internal System.Windows.Forms.NumericUpDown nudCacheSize;
        internal System.Windows.Forms.TextBox txtCacheDir;
        internal System.Windows.Forms.TabPage tpStats;
        internal System.Windows.Forms.GroupBox gbStatistics;
        internal System.Windows.Forms.Button btnDebugStart;
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
        internal System.Windows.Forms.TabControl tcDestinations;
        internal System.Windows.Forms.TabPage tpDestTinyPic;
        internal System.Windows.Forms.TabPage tpDestImageShack;
        internal System.Windows.Forms.TabPage tpTextServices;
        internal System.Windows.Forms.TabControl tcTextUploaders;
        internal System.Windows.Forms.GroupBox gbImageSettings;
        internal System.Windows.Forms.GroupBox gpCropRegion;
        internal System.Windows.Forms.TextBox txtAutoTranslate;
        internal System.Windows.Forms.CheckBox cbAutoTranslate;
        internal System.Windows.Forms.ToolTip ttZScreen;
        internal System.Windows.Forms.CheckBox cbShowHelpBalloonTips;
        internal System.Windows.Forms.TabPage tpDestMindTouch;
        internal AccountsControl ucMindTouchAccounts;
        internal System.Windows.Forms.GroupBox gbImageEditorSettings;
        internal System.Windows.Forms.CheckBox chkImageEditorAutoSave;
        internal System.Windows.Forms.TabPage tpDestFTP;
        internal AccountsControl ucFTPAccounts;
        internal System.Windows.Forms.CheckBox chkPublicImageShack;
        internal System.Windows.Forms.Button btnImageShackProfile;
        internal System.Windows.Forms.Label lblImageShackUsername;
        internal System.Windows.Forms.TextBox txtUserNameImageShack;
        internal System.Windows.Forms.Label lblScreenshotDelay;
        internal System.Windows.Forms.GroupBox gbDynamicCrosshair;
        internal System.Windows.Forms.GroupBox gbDynamicRegionBorderColorSettings;
        internal System.Windows.Forms.TabPage tpDestTwitter;
        internal System.Windows.Forms.GroupBox gbMindTouchOptions;
        internal System.Windows.Forms.CheckBox chkDekiWikiForcePath;
        private System.Windows.Forms.TabPage tpProxy;
        internal System.Windows.Forms.GroupBox gpProxySettings;
        private System.Windows.Forms.ToolStripMenuItem tsmFTPClient;
        private System.Windows.Forms.TabPage tpTreeGUI;
        private System.Windows.Forms.PropertyGrid pgIndexer;
        private System.Windows.Forms.CheckBox chkSelectedWindowCaptureObjects;
        private System.Windows.Forms.Label lblWatermarkOffsetPixel;
        private System.Windows.Forms.CheckBox cbAutoSaveSettings;
        private System.Windows.Forms.CheckBox cbTwitPicShowFull;
        private System.Windows.Forms.ComboBox cboTwitPicThumbnailMode;
        private System.Windows.Forms.Label lblTwitPicThumbnailMode;
        private System.Windows.Forms.TextBox txtImagesFolderPattern;
        private System.Windows.Forms.Label lblImagesFolderPatternPreview;
        private System.Windows.Forms.Label lblImagesFolderPattern;
        private System.Windows.Forms.Button btnMoveImageFiles;
        private System.Windows.Forms.CheckBox chkCheckUpdatesBeta;
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
        private System.Windows.Forms.TabPage tpDestImageBam;
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
        private System.Windows.Forms.TabPage tpDestRapidShare;
        private System.Windows.Forms.Label lblRapidSharePassword;
        private System.Windows.Forms.Label lblRapidSharePremiumUsername;
        private System.Windows.Forms.Label lblRapidShareCollectorsID;
        private System.Windows.Forms.TextBox txtRapidSharePassword;
        private System.Windows.Forms.TextBox txtRapidSharePremiumUserName;
        private System.Windows.Forms.TextBox txtRapidShareCollectorID;
        private System.Windows.Forms.ComboBox cboRapidShareAcctType;
        private System.Windows.Forms.Label lblRapidShareAccountType;
        private System.Windows.Forms.TabPage tpDestSendSpace;
        private System.Windows.Forms.Label lblSendSpacePassword;
        private System.Windows.Forms.Label lblSendSpaceUsername;
        private System.Windows.Forms.TextBox txtSendSpacePassword;
        private System.Windows.Forms.TextBox txtSendSpaceUserName;
        private System.Windows.Forms.ComboBox cboSendSpaceAcctType;
        private System.Windows.Forms.Label lblSendSpaceAccountType;
        private System.Windows.Forms.Button btnSendSpaceRegister;
        internal System.Windows.Forms.RichTextBox rtbDebugInfo;
        private System.Windows.Forms.Label lblErrorRetry;
        private System.Windows.Forms.Label lblFTPThumbWidth;
        private System.Windows.Forms.TextBox txtFTPThumbWidth;
        private System.Windows.Forms.CheckBox cbFTPThumbnailCheckSize;
        private System.Windows.Forms.CheckBox chkWindows7TaskbarIntegration;
        private System.Windows.Forms.TabPage tpDestFlickr;
        private System.Windows.Forms.Button btnFlickrGetToken;
        private System.Windows.Forms.Button btnFlickrGetFrob;
        private System.Windows.Forms.Button btnFlickrCheckToken;
        private System.Windows.Forms.PropertyGrid pgFlickrAuthInfo;
        private System.Windows.Forms.PropertyGrid pgFlickrSettings;
        private System.Windows.Forms.Button btnFlickrOpenImages;
        internal DestSelector ucDestOptions;
        private System.Windows.Forms.Button btnFTPOpenClient;
        private System.Windows.Forms.CheckBox chkShellExt;
        private System.Windows.Forms.ToolStripMenuItem tsmFileDest;
        private System.Windows.Forms.CheckBox chkHotkeys;
        private System.Windows.Forms.CheckBox chkTwitterEnable;
        private System.Windows.Forms.Button btnFtpHelp;
        private System.Windows.Forms.Button btnOpenZScreenTester;
        private System.Windows.Forms.Label lblMaxNameLength;
        private System.Windows.Forms.NumericUpDown nudMaxNameLength;
        private System.Windows.Forms.Button btnSelectGradient;
        private System.Windows.Forms.CheckBox cboUseCustomGradient;
        private System.Windows.Forms.GroupBox gbGradientMakerBasic;
        private System.Windows.Forms.DataGridViewTextBoxColumn chHotkeys_Description;
        private System.Windows.Forms.DataGridViewButtonColumn chHotkeys_Keys;
        private System.Windows.Forms.DataGridViewTextBoxColumn DefaultKeys;
        private System.Windows.Forms.CheckBox chkImageUploadRandomRetryOnFail;
        private System.Windows.Forms.CheckBox chkSelectedWindowCleanBackground;
        private System.Windows.Forms.CheckBox chkSelectedWindowCleanTransparentCorners;
        private System.Windows.Forms.CheckBox chkSelectedWindowShowCheckers;
        private System.Windows.Forms.CheckBox chkSelectedWindowIncludeShadow;
        private System.Windows.Forms.GroupBox gbMonitorClipboard;
        private System.Windows.Forms.CheckBox chkMonText;
        private System.Windows.Forms.CheckBox chkMonUrls;
        private System.Windows.Forms.CheckBox chkMonFiles;
        private System.Windows.Forms.CheckBox chkMonImages;
        private System.Windows.Forms.CheckBox chkActiveWindowTryCaptureChildren;
        private System.Windows.Forms.CheckBox chkActiveWindowPreferDWM;
        private System.Windows.Forms.TabPage tpActivewindow;
        private System.Windows.Forms.CheckBox cbActiveWindowGDIFreezeWindow;
        private System.Windows.Forms.ComboBox cbGIFQuality;
        private System.Windows.Forms.Label lblGIFQuality;
        private System.Windows.Forms.TabPage tpDebugLog;
        private System.Windows.Forms.RichTextBox rtbDebugLog;
        private System.Windows.Forms.ComboBox cboCloseButtonAction;
        private System.Windows.Forms.ComboBox cboMinimizeButtonAction;
        private System.Windows.Forms.Label lblMinimizeButtonAction;
        private System.Windows.Forms.Label lblCloseButtonAction;
        private System.Windows.Forms.CheckBox chkPreferSystemFolders;
        internal System.Windows.Forms.GroupBox gbSettingsExportImport;
        private System.Windows.Forms.Button btnResetHotkeys;
        private System.Windows.Forms.TabPage tpFreehandCropShot;
        private System.Windows.Forms.CheckBox cbFreehandCropShowHelpText;
        private System.Windows.Forms.CheckBox cbFreehandCropAutoUpload;
        private System.Windows.Forms.CheckBox cbFreehandCropAutoClose;
        private System.Windows.Forms.CheckBox cbFreehandCropShowRectangleBorder;
        private System.Windows.Forms.TabPage tpDestLocalhost;
        internal AccountsControl ucLocalhostAccounts;
        private System.Windows.Forms.Label lblFtpFiles;
        private System.Windows.Forms.Label lblFtpText;
        private System.Windows.Forms.Label lblFtpImages;
        private System.Windows.Forms.ComboBox cboFtpFiles;
        private System.Windows.Forms.ComboBox cboFtpText;
        private System.Windows.Forms.ComboBox cboFtpImages;
        private System.Windows.Forms.TabPage tpDestMediaWiki;
        internal AccountsControl ucMediaWikiAccounts;
        private System.Windows.Forms.TabPage tpDestDropbox;
        private System.Windows.Forms.Label lblDropboxPathTip;
        private System.Windows.Forms.Label lblDropboxPath;
        private System.Windows.Forms.Label lblDropboxPassword;
        private System.Windows.Forms.Label lblDropboxEmail;
        private System.Windows.Forms.Button btnDropboxLogin;
        private System.Windows.Forms.TextBox txtDropboxPath;
        private System.Windows.Forms.TextBox txtDropboxPassword;
        private System.Windows.Forms.TextBox txtDropboxEmail;
        private System.Windows.Forms.Label lblDropboxStatus;
        private System.Windows.Forms.Button btnDropboxRegister;
        private System.Windows.Forms.Label lblDropboxLoginTip;
        private System.Windows.Forms.Label lblDropboxPasswordTip;
        private System.Windows.Forms.PictureBox pbDropboxLogo;
        private System.Windows.Forms.TabPage tpDestImgur;
        private System.Windows.Forms.Button btnImgurOpenAuthorizePage;
        private System.Windows.Forms.Button btnImgurLogin;
        private System.Windows.Forms.Label lblImgurStatus;
        private System.Windows.Forms.CheckBox chkImgurUserAccount;
        private System.Windows.Forms.GroupBox gbTwitterOthers;
        private System.Windows.Forms.Button btnTwitterLogin;
        private AccountsControl ucTwitterAccounts;
        private System.Windows.Forms.GroupBox gbImgurUserAccount;
        private System.Windows.Forms.TableLayoutPanel tlpTwitter;
        private System.Windows.Forms.Panel panelTwitter;
        private System.Windows.Forms.CheckBox cbCopyClipboardAfterTask;
        internal AccountsControl ucProxyAccounts;
        internal System.Windows.Forms.ComboBox cboProxyConfig;
        private System.Windows.Forms.Button btnHistoryOpen;
        internal System.Windows.Forms.CheckBox cbAddFailedScreenshot;
        internal System.Windows.Forms.CheckBox cbHistorySave;
        internal System.Windows.Forms.NumericUpDown nudHistoryMaxItems;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolStripMenuItem historyToolStripMenuItem;
        private System.Windows.Forms.CheckBox cbLanguageAutoDetect;
        private System.Windows.Forms.TabPage tpHistory;
    }
}
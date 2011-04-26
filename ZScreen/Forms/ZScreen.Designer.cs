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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.niTray = new System.Windows.Forms.NotifyIcon(this.components);
            this.cmTray = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiTabs = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmImageDest = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmFileDest = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmEditinImageSoftware = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmCopytoClipboardMode = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
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
            this.toolStripSeparator9 = new System.Windows.Forms.ToolStripSeparator();
            this.editInPicnikToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiTwitter = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
            this.cmsRetryUpload = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ilApp = new System.Windows.Forms.ImageList(this.components);
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
            this.tpFTP = new System.Windows.Forms.TabPage();
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
            this.tpLocalhost = new System.Windows.Forms.TabPage();
            this.ucLocalhostAccounts = new ZScreenGUI.AccountsControl();
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
            this.tpDropbox = new System.Windows.Forms.TabPage();
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
            this.tpTinyPic = new System.Windows.Forms.TabPage();
            this.gbTinyPic = new System.Windows.Forms.GroupBox();
            this.btnGalleryTinyPic = new System.Windows.Forms.Button();
            this.btnRegCodeTinyPic = new System.Windows.Forms.Button();
            this.lblRegistrationCode = new System.Windows.Forms.Label();
            this.txtTinyPicShuk = new System.Windows.Forms.TextBox();
            this.chkRememberTinyPicUserPass = new System.Windows.Forms.CheckBox();
            this.tpImgur = new System.Windows.Forms.TabPage();
            this.cbImgurUseAccount = new System.Windows.Forms.CheckBox();
            this.lblImgurStatus = new System.Windows.Forms.Label();
            this.lblImgurHowTo = new System.Windows.Forms.Label();
            this.btnImgurLogin = new System.Windows.Forms.Button();
            this.lblImgurVerificationCode = new System.Windows.Forms.Label();
            this.tbImgurVerificationCode = new System.Windows.Forms.TextBox();
            this.btnImgurOpenAuthorizePage = new System.Windows.Forms.Button();
            this.tpFlickr = new System.Windows.Forms.TabPage();
            this.btnFlickrOpenImages = new System.Windows.Forms.Button();
            this.pgFlickrAuthInfo = new System.Windows.Forms.PropertyGrid();
            this.pgFlickrSettings = new System.Windows.Forms.PropertyGrid();
            this.btnFlickrCheckToken = new System.Windows.Forms.Button();
            this.btnFlickrGetToken = new System.Windows.Forms.Button();
            this.btnFlickrGetFrob = new System.Windows.Forms.Button();
            this.tpTwitter = new System.Windows.Forms.TabPage();
            this.lblTwitterStatus = new System.Windows.Forms.Label();
            this.gbTwitterOthers = new System.Windows.Forms.GroupBox();
            this.cbTwitPicShowFull = new System.Windows.Forms.CheckBox();
            this.cboTwitPicThumbnailMode = new System.Windows.Forms.ComboBox();
            this.lblTwitPicThumbnailMode = new System.Windows.Forms.Label();
            this.btnTwitterLogin = new System.Windows.Forms.Button();
            this.lblTwitterVerificationCode = new System.Windows.Forms.Label();
            this.tbTwitterVerificationCode = new System.Windows.Forms.TextBox();
            this.btnTwitterOpenAuthorizePage = new System.Windows.Forms.Button();
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
            this.tpMindTouch = new System.Windows.Forms.TabPage();
            this.gbMindTouchOptions = new System.Windows.Forms.GroupBox();
            this.chkDekiWikiForcePath = new System.Windows.Forms.CheckBox();
            this.ucMindTouchAccounts = new ZScreenGUI.AccountsControl();
            this.tpMediaWiki = new System.Windows.Forms.TabPage();
            this.ucMediaWikiAccounts = new ZScreenGUI.AccountsControl();
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
            this.tpEditors = new System.Windows.Forms.TabPage();
            this.tcEditors = new System.Windows.Forms.TabControl();
            this.tpEditorsImages = new System.Windows.Forms.TabPage();
            this.chkEditorsEnabled = new System.Windows.Forms.CheckBox();
            this.gbImageEditorSettings = new System.Windows.Forms.GroupBox();
            this.chkImageEditorAutoSave = new System.Windows.Forms.CheckBox();
            this.pgEditorsImage = new System.Windows.Forms.PropertyGrid();
            this.btnRemoveImageEditor = new System.Windows.Forms.Button();
            this.lbSoftware = new System.Windows.Forms.CheckedListBox();
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
            this.cbAddFailedScreenshot = new System.Windows.Forms.CheckBox();
            this.tpCustomUploaders = new System.Windows.Forms.TabPage();
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
            this.lvDictionary = new System.Windows.Forms.ListView();
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.txtAutoTranslate = new System.Windows.Forms.TextBox();
            this.cbAutoTranslate = new System.Windows.Forms.CheckBox();
            this.btnTranslateTo1 = new System.Windows.Forms.Button();
            this.lblDictionary = new System.Windows.Forms.Label();
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
            this.txtPreview = new System.Windows.Forms.RichTextBox();
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
            this.tpUploadText = new System.Windows.Forms.TabPage();
            this.txtTextUploaderContent = new System.Windows.Forms.TextBox();
            this.btnUploadText = new System.Windows.Forms.Button();
            this.btnUploadTextClipboard = new System.Windows.Forms.Button();
            this.btnUploadTextClipboardFile = new System.Windows.Forms.Button();
            this.ttZScreen = new System.Windows.Forms.ToolTip(this.components);
            this.ucTwitterAccounts = new ZScreenGUI.AccountsControl();
            this.cmTray.SuspendLayout();
            this.cmsHistory.SuspendLayout();
            this.tcApp.SuspendLayout();
            this.tpMain.SuspendLayout();
            this.gbImageSettings.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbLogo)).BeginInit();
            this.tpDestinations.SuspendLayout();
            this.tcDestinations.SuspendLayout();
            this.tpFTP.SuspendLayout();
            this.gbFTPSettings.SuspendLayout();
            this.tpLocalhost.SuspendLayout();
            this.tpRapidShare.SuspendLayout();
            this.tpSendSpace.SuspendLayout();
            this.tpDropbox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbDropboxLogo)).BeginInit();
            this.tpImageShack.SuspendLayout();
            this.gbImageShack.SuspendLayout();
            this.tpTinyPic.SuspendLayout();
            this.gbTinyPic.SuspendLayout();
            this.tpImgur.SuspendLayout();
            this.tpFlickr.SuspendLayout();
            this.tpTwitter.SuspendLayout();
            this.gbTwitterOthers.SuspendLayout();
            this.tpImageBam.SuspendLayout();
            this.gbImageBamGalleries.SuspendLayout();
            this.gbImageBamLinks.SuspendLayout();
            this.gbImageBamApiKeys.SuspendLayout();
            this.tpMindTouch.SuspendLayout();
            this.gbMindTouchOptions.SuspendLayout();
            this.tpMediaWiki.SuspendLayout();
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
            this.cmTray.Size = new System.Drawing.Size(244, 316);
            // 
            // tsmiTabs
            // 
            this.tsmiTabs.DoubleClickEnabled = true;
            this.tsmiTabs.Image = global::ZScreenGUI.Properties.Resources.wrench;
            this.tsmiTabs.Name = "tsmiTabs";
            this.tsmiTabs.Size = new System.Drawing.Size(243, 24);
            this.tsmiTabs.Text = "View Settings Menu...";
            this.tsmiTabs.Click += new System.EventHandler(this.tsmSettings_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(240, 6);
            // 
            // tsmImageDest
            // 
            this.tsmImageDest.Image = global::ZScreenGUI.Properties.Resources.picture_go;
            this.tsmImageDest.Name = "tsmImageDest";
            this.tsmImageDest.Size = new System.Drawing.Size(243, 24);
            this.tsmImageDest.Text = "Send Image To";
            // 
            // tsmFileDest
            // 
            this.tsmFileDest.Image = global::ZScreenGUI.Properties.Resources.application_go;
            this.tsmFileDest.Name = "tsmFileDest";
            this.tsmFileDest.Size = new System.Drawing.Size(243, 24);
            this.tsmFileDest.Text = "Send File To";
            // 
            // tsmEditinImageSoftware
            // 
            this.tsmEditinImageSoftware.CheckOnClick = true;
            this.tsmEditinImageSoftware.Image = global::ZScreenGUI.Properties.Resources.picture_edit;
            this.tsmEditinImageSoftware.Name = "tsmEditinImageSoftware";
            this.tsmEditinImageSoftware.Size = new System.Drawing.Size(243, 24);
            this.tsmEditinImageSoftware.Text = "Edit in Image Software";
            this.tsmEditinImageSoftware.CheckedChanged += new System.EventHandler(this.tsmEditinImageSoftware_CheckedChanged);
            // 
            // tsmCopytoClipboardMode
            // 
            this.tsmCopytoClipboardMode.Image = global::ZScreenGUI.Properties.Resources.page_copy;
            this.tsmCopytoClipboardMode.Name = "tsmCopytoClipboardMode";
            this.tsmCopytoClipboardMode.Size = new System.Drawing.Size(243, 24);
            this.tsmCopytoClipboardMode.Text = "Copy to Clipboard Mode";
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(240, 6);
            // 
            // tsmFTPClient
            // 
            this.tsmFTPClient.Image = global::ZScreenGUI.Properties.Resources.server_edit;
            this.tsmFTPClient.Name = "tsmFTPClient";
            this.tsmFTPClient.Size = new System.Drawing.Size(243, 24);
            this.tsmFTPClient.Text = "FTP &Client...";
            this.tsmFTPClient.Click += new System.EventHandler(this.tsmFTPClient_Click);
            // 
            // tsmViewLocalDirectory
            // 
            this.tsmViewLocalDirectory.Image = global::ZScreenGUI.Properties.Resources.folder_picture;
            this.tsmViewLocalDirectory.Name = "tsmViewLocalDirectory";
            this.tsmViewLocalDirectory.Size = new System.Drawing.Size(243, 24);
            this.tsmViewLocalDirectory.Text = "Images Directory...";
            this.tsmViewLocalDirectory.Click += new System.EventHandler(this.tsmViewDirectory_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(240, 6);
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
            this.tsmActions.Size = new System.Drawing.Size(243, 24);
            this.tsmActions.Text = "Quick &Actions";
            // 
            // tsmEntireScreen
            // 
            this.tsmEntireScreen.Image = global::ZScreenGUI.Properties.Resources.monitor;
            this.tsmEntireScreen.Name = "tsmEntireScreen";
            this.tsmEntireScreen.Size = new System.Drawing.Size(233, 24);
            this.tsmEntireScreen.Text = "Entire Screen";
            this.tsmEntireScreen.Click += new System.EventHandler(this.entireScreenToolStripMenuItem_Click);
            // 
            // tsmSelectedWindow
            // 
            this.tsmSelectedWindow.Image = global::ZScreenGUI.Properties.Resources.application_double;
            this.tsmSelectedWindow.Name = "tsmSelectedWindow";
            this.tsmSelectedWindow.Size = new System.Drawing.Size(233, 24);
            this.tsmSelectedWindow.Text = "Selected Window...";
            this.tsmSelectedWindow.Click += new System.EventHandler(this.selectedWindowToolStripMenuItem_Click);
            // 
            // tsmCropShot
            // 
            this.tsmCropShot.Image = global::ZScreenGUI.Properties.Resources.shape_square;
            this.tsmCropShot.Name = "tsmCropShot";
            this.tsmCropShot.Size = new System.Drawing.Size(233, 24);
            this.tsmCropShot.Text = "Crop Shot...";
            this.tsmCropShot.Click += new System.EventHandler(this.rectangularRegionToolStripMenuItem_Click);
            // 
            // tsmLastCropShot
            // 
            this.tsmLastCropShot.Image = global::ZScreenGUI.Properties.Resources.shape_square_go;
            this.tsmLastCropShot.Name = "tsmLastCropShot";
            this.tsmLastCropShot.Size = new System.Drawing.Size(233, 24);
            this.tsmLastCropShot.Text = "Last Crop Shot";
            this.tsmLastCropShot.Click += new System.EventHandler(this.lastRectangularRegionToolStripMenuItem_Click);
            // 
            // autoScreenshotsToolStripMenuItem
            // 
            this.autoScreenshotsToolStripMenuItem.Image = global::ZScreenGUI.Properties.Resources.images_stack;
            this.autoScreenshotsToolStripMenuItem.Name = "autoScreenshotsToolStripMenuItem";
            this.autoScreenshotsToolStripMenuItem.Size = new System.Drawing.Size(233, 24);
            this.autoScreenshotsToolStripMenuItem.Text = "Auto Capture...";
            this.autoScreenshotsToolStripMenuItem.Click += new System.EventHandler(this.autoScreenshotsToolStripMenuItem_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(230, 6);
            // 
            // tsmClipboardUpload
            // 
            this.tsmClipboardUpload.Image = global::ZScreenGUI.Properties.Resources.images;
            this.tsmClipboardUpload.Name = "tsmClipboardUpload";
            this.tsmClipboardUpload.Size = new System.Drawing.Size(233, 24);
            this.tsmClipboardUpload.Text = "Clipboard Upload";
            this.tsmClipboardUpload.Click += new System.EventHandler(this.tsmUploadFromClipboard_Click);
            // 
            // tsmDragDropWindow
            // 
            this.tsmDragDropWindow.Image = global::ZScreenGUI.Properties.Resources.shape_move_backwards;
            this.tsmDragDropWindow.Name = "tsmDragDropWindow";
            this.tsmDragDropWindow.Size = new System.Drawing.Size(233, 24);
            this.tsmDragDropWindow.Text = "Drag && Drop Window...";
            this.tsmDragDropWindow.Click += new System.EventHandler(this.tsmDropWindow_Click);
            // 
            // tsmLanguageTranslator
            // 
            this.tsmLanguageTranslator.Image = global::ZScreenGUI.Properties.Resources.comments;
            this.tsmLanguageTranslator.Name = "tsmLanguageTranslator";
            this.tsmLanguageTranslator.Size = new System.Drawing.Size(233, 24);
            this.tsmLanguageTranslator.Text = "Language Translator";
            this.tsmLanguageTranslator.Click += new System.EventHandler(this.languageTranslatorToolStripMenuItem_Click);
            // 
            // tsmScreenColorPicker
            // 
            this.tsmScreenColorPicker.Image = global::ZScreenGUI.Properties.Resources.color_wheel;
            this.tsmScreenColorPicker.Name = "tsmScreenColorPicker";
            this.tsmScreenColorPicker.Size = new System.Drawing.Size(233, 24);
            this.tsmScreenColorPicker.Text = "Screen Color Picker...";
            this.tsmScreenColorPicker.Click += new System.EventHandler(this.screenColorPickerToolStripMenuItem_Click);
            // 
            // tsmQuickActions
            // 
            this.tsmQuickActions.Image = global::ZScreenGUI.Properties.Resources.application_lightning;
            this.tsmQuickActions.Name = "tsmQuickActions";
            this.tsmQuickActions.Size = new System.Drawing.Size(243, 24);
            this.tsmQuickActions.Text = "Actions Toolbar...";
            this.tsmQuickActions.Click += new System.EventHandler(this.tsmQuickActions_Click);
            // 
            // tsmQuickOptions
            // 
            this.tsmQuickOptions.Image = global::ZScreenGUI.Properties.Resources.application_edit;
            this.tsmQuickOptions.Name = "tsmQuickOptions";
            this.tsmQuickOptions.Size = new System.Drawing.Size(243, 24);
            this.tsmQuickOptions.Text = "&Quick Options...";
            this.tsmQuickOptions.Click += new System.EventHandler(this.tsmQuickOptions_Click);
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(240, 6);
            // 
            // tsmHelp
            // 
            this.tsmHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmLicense,
            this.tsmVersionHistory,
            this.tsmAbout});
            this.tsmHelp.Image = global::ZScreenGUI.Properties.Resources.help;
            this.tsmHelp.Name = "tsmHelp";
            this.tsmHelp.Size = new System.Drawing.Size(243, 24);
            this.tsmHelp.Text = "&Help";
            // 
            // tsmLicense
            // 
            this.tsmLicense.Image = global::ZScreenGUI.Properties.Resources.note_error;
            this.tsmLicense.Name = "tsmLicense";
            this.tsmLicense.Size = new System.Drawing.Size(187, 24);
            this.tsmLicense.Text = "License...";
            this.tsmLicense.Click += new System.EventHandler(this.tsmLic_Click);
            // 
            // tsmVersionHistory
            // 
            this.tsmVersionHistory.Image = global::ZScreenGUI.Properties.Resources.page_white_text;
            this.tsmVersionHistory.Name = "tsmVersionHistory";
            this.tsmVersionHistory.Size = new System.Drawing.Size(187, 24);
            this.tsmVersionHistory.Text = "&Version History...";
            this.tsmVersionHistory.Click += new System.EventHandler(this.cmVersionHistory_Click);
            // 
            // tsmAbout
            // 
            this.tsmAbout.Image = global::ZScreenGUI.Properties.Resources.information;
            this.tsmAbout.Name = "tsmAbout";
            this.tsmAbout.Size = new System.Drawing.Size(187, 24);
            this.tsmAbout.Text = "About...";
            this.tsmAbout.Click += new System.EventHandler(this.tsmAboutMain_Click);
            // 
            // tsmExitZScreen
            // 
            this.tsmExitZScreen.Image = global::ZScreenGUI.Properties.Resources.cross;
            this.tsmExitZScreen.Name = "tsmExitZScreen";
            this.tsmExitZScreen.Size = new System.Drawing.Size(243, 24);
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
            this.toolStripSeparator9,
            this.editInPicnikToolStripMenuItem,
            this.tsmiTwitter,
            this.toolStripSeparator8,
            this.cmsRetryUpload,
            this.deleteToolStripMenuItem});
            this.cmsHistory.Name = "cmsHistory";
            this.cmsHistory.Size = new System.Drawing.Size(196, 238);
            // 
            // tsmCopyCbHistory
            // 
            this.tsmCopyCbHistory.Name = "tsmCopyCbHistory";
            this.tsmCopyCbHistory.Size = new System.Drawing.Size(195, 24);
            this.tsmCopyCbHistory.Text = "&Copy Link";
            // 
            // copyImageToolStripMenuItem
            // 
            this.copyImageToolStripMenuItem.Name = "copyImageToolStripMenuItem";
            this.copyImageToolStripMenuItem.Size = new System.Drawing.Size(195, 24);
            this.copyImageToolStripMenuItem.Text = "Copy &Image";
            this.copyImageToolStripMenuItem.Click += new System.EventHandler(this.copyImageToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(192, 6);
            // 
            // openLocalFileToolStripMenuItem
            // 
            this.openLocalFileToolStripMenuItem.Name = "openLocalFileToolStripMenuItem";
            this.openLocalFileToolStripMenuItem.Size = new System.Drawing.Size(195, 24);
            this.openLocalFileToolStripMenuItem.Text = "Open Local File";
            this.openLocalFileToolStripMenuItem.Click += new System.EventHandler(this.openLocalFileToolStripMenuItem_Click);
            // 
            // browseURLToolStripMenuItem
            // 
            this.browseURLToolStripMenuItem.Name = "browseURLToolStripMenuItem";
            this.browseURLToolStripMenuItem.Size = new System.Drawing.Size(195, 24);
            this.browseURLToolStripMenuItem.Text = "Browse &URL...";
            this.browseURLToolStripMenuItem.Click += new System.EventHandler(this.browseURLToolStripMenuItem_Click);
            // 
            // openSourceToolStripMenuItem
            // 
            this.openSourceToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.openSourceInDefaultWebBrowserHTMLToolStripMenuItem,
            this.copySourceToClipboardStringToolStripMenuItem});
            this.openSourceToolStripMenuItem.Name = "openSourceToolStripMenuItem";
            this.openSourceToolStripMenuItem.Size = new System.Drawing.Size(195, 24);
            this.openSourceToolStripMenuItem.Text = "Open Source";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(255, 24);
            this.openToolStripMenuItem.Text = "Open Source in Text Editor";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // openSourceInDefaultWebBrowserHTMLToolStripMenuItem
            // 
            this.openSourceInDefaultWebBrowserHTMLToolStripMenuItem.Name = "openSourceInDefaultWebBrowserHTMLToolStripMenuItem";
            this.openSourceInDefaultWebBrowserHTMLToolStripMenuItem.Size = new System.Drawing.Size(255, 24);
            this.openSourceInDefaultWebBrowserHTMLToolStripMenuItem.Text = "Open Source in Browser";
            this.openSourceInDefaultWebBrowserHTMLToolStripMenuItem.Click += new System.EventHandler(this.openSourceInDefaultWebBrowserHTMLToolStripMenuItem_Click);
            // 
            // copySourceToClipboardStringToolStripMenuItem
            // 
            this.copySourceToClipboardStringToolStripMenuItem.Name = "copySourceToClipboardStringToolStripMenuItem";
            this.copySourceToClipboardStringToolStripMenuItem.Size = new System.Drawing.Size(255, 24);
            this.copySourceToClipboardStringToolStripMenuItem.Text = "Copy Source to Clipboard";
            this.copySourceToClipboardStringToolStripMenuItem.Click += new System.EventHandler(this.copySourceToClipboardStringToolStripMenuItem_Click);
            // 
            // toolStripSeparator9
            // 
            this.toolStripSeparator9.Name = "toolStripSeparator9";
            this.toolStripSeparator9.Size = new System.Drawing.Size(192, 6);
            // 
            // editInPicnikToolStripMenuItem
            // 
            this.editInPicnikToolStripMenuItem.Name = "editInPicnikToolStripMenuItem";
            this.editInPicnikToolStripMenuItem.Size = new System.Drawing.Size(195, 24);
            this.editInPicnikToolStripMenuItem.Text = "Edit in Picnik...";
            this.editInPicnikToolStripMenuItem.Click += new System.EventHandler(this.editInPicnikToolStripMenuItem_Click);
            // 
            // tsmiTwitter
            // 
            this.tsmiTwitter.Name = "tsmiTwitter";
            this.tsmiTwitter.Size = new System.Drawing.Size(195, 24);
            this.tsmiTwitter.Text = "Share on &Twitter...";
            this.tsmiTwitter.Click += new System.EventHandler(this.tsmiTwitter_Click);
            // 
            // toolStripSeparator8
            // 
            this.toolStripSeparator8.Name = "toolStripSeparator8";
            this.toolStripSeparator8.Size = new System.Drawing.Size(192, 6);
            // 
            // cmsRetryUpload
            // 
            this.cmsRetryUpload.Name = "cmsRetryUpload";
            this.cmsRetryUpload.Size = new System.Drawing.Size(195, 24);
            this.cmsRetryUpload.Text = "Retry Upload";
            this.cmsRetryUpload.Click += new System.EventHandler(this.cmsRetryUpload_Click);
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(195, 24);
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
            this.ilApp.Images.SetKeyName(14, "application.png");
            this.ilApp.Images.SetKeyName(15, "shape_square_edit.png");
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
            this.tcApp.Location = new System.Drawing.Point(3, 2);
            this.tcApp.Margin = new System.Windows.Forms.Padding(4);
            this.tcApp.Name = "tcApp";
            this.tcApp.SelectedIndex = 0;
            this.tcApp.Size = new System.Drawing.Size(1083, 570);
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
            this.tpMain.ImageKey = "application_form.png";
            this.tpMain.Location = new System.Drawing.Point(4, 25);
            this.tpMain.Margin = new System.Windows.Forms.Padding(4);
            this.tpMain.Name = "tpMain";
            this.tpMain.Padding = new System.Windows.Forms.Padding(4);
            this.tpMain.Size = new System.Drawing.Size(1075, 541);
            this.tpMain.TabIndex = 0;
            this.tpMain.Text = "Main";
            this.tpMain.UseVisualStyleBackColor = true;
            // 
            // ucDestOptions
            // 
            this.ucDestOptions.Location = new System.Drawing.Point(53, 69);
            this.ucDestOptions.Margin = new System.Windows.Forms.Padding(5);
            this.ucDestOptions.MaximumSize = new System.Drawing.Size(504, 178);
            this.ucDestOptions.Name = "ucDestOptions";
            this.ucDestOptions.Size = new System.Drawing.Size(504, 178);
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
            this.gbImageSettings.Location = new System.Drawing.Point(64, 246);
            this.gbImageSettings.Margin = new System.Windows.Forms.Padding(4);
            this.gbImageSettings.Name = "gbImageSettings";
            this.gbImageSettings.Padding = new System.Windows.Forms.Padding(4);
            this.gbImageSettings.Size = new System.Drawing.Size(480, 177);
            this.gbImageSettings.TabIndex = 123;
            this.gbImageSettings.TabStop = false;
            this.gbImageSettings.Text = "Image Settings";
            // 
            // lblScreenshotDelay
            // 
            this.lblScreenshotDelay.AutoSize = true;
            this.lblScreenshotDelay.Location = new System.Drawing.Point(21, 30);
            this.lblScreenshotDelay.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblScreenshotDelay.Name = "lblScreenshotDelay";
            this.lblScreenshotDelay.Size = new System.Drawing.Size(124, 17);
            this.lblScreenshotDelay.TabIndex = 122;
            this.lblScreenshotDelay.Text = "Screenshot Delay:";
            // 
            // nudScreenshotDelay
            // 
            this.nudScreenshotDelay.Location = new System.Drawing.Point(149, 22);
            this.nudScreenshotDelay.Margin = new System.Windows.Forms.Padding(5);
            this.nudScreenshotDelay.Name = "nudScreenshotDelay";
            this.nudScreenshotDelay.RealValue = ((long)(0));
            this.nudScreenshotDelay.Size = new System.Drawing.Size(312, 30);
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
            this.lblCopytoClipboard.Location = new System.Drawing.Point(21, 64);
            this.lblCopytoClipboard.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCopytoClipboard.Name = "lblCopytoClipboard";
            this.lblCopytoClipboard.Size = new System.Drawing.Size(124, 17);
            this.lblCopytoClipboard.TabIndex = 117;
            this.lblCopytoClipboard.Text = "Copy to Clipboard:";
            // 
            // cboClipboardTextMode
            // 
            this.cboClipboardTextMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboClipboardTextMode.FormattingEnabled = true;
            this.cboClipboardTextMode.Location = new System.Drawing.Point(152, 59);
            this.cboClipboardTextMode.Margin = new System.Windows.Forms.Padding(4);
            this.cboClipboardTextMode.Name = "cboClipboardTextMode";
            this.cboClipboardTextMode.Size = new System.Drawing.Size(308, 24);
            this.cboClipboardTextMode.TabIndex = 116;
            this.ttZScreen.SetToolTip(this.cboClipboardTextMode, "Specify the way in which screenshot links\r\nshould be added to your clipboard.\r\nDe" +
                    "fault: Full Image.");
            this.cboClipboardTextMode.SelectedIndexChanged += new System.EventHandler(this.cboClipboardTextMode_SelectedIndexChanged);
            // 
            // chkShowCursor
            // 
            this.chkShowCursor.AutoSize = true;
            this.chkShowCursor.Location = new System.Drawing.Point(21, 137);
            this.chkShowCursor.Margin = new System.Windows.Forms.Padding(4);
            this.chkShowCursor.Name = "chkShowCursor";
            this.chkShowCursor.Size = new System.Drawing.Size(208, 21);
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
            this.chkManualNaming.Location = new System.Drawing.Point(21, 108);
            this.chkManualNaming.Margin = new System.Windows.Forms.Padding(4);
            this.chkManualNaming.Name = "chkManualNaming";
            this.chkManualNaming.Size = new System.Drawing.Size(163, 21);
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
            this.llProjectPage.Location = new System.Drawing.Point(587, 443);
            this.llProjectPage.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.llProjectPage.Name = "llProjectPage";
            this.llProjectPage.Size = new System.Drawing.Size(78, 17);
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
            this.llWebsite.Location = new System.Drawing.Point(928, 443);
            this.llWebsite.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.llWebsite.Name = "llWebsite";
            this.llWebsite.Size = new System.Drawing.Size(86, 17);
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
            this.llblBugReports.Location = new System.Drawing.Point(736, 443);
            this.llblBugReports.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.llblBugReports.Name = "llblBugReports";
            this.llblBugReports.Size = new System.Drawing.Size(130, 17);
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
            this.lblLogo.Location = new System.Drawing.Point(587, 384);
            this.lblLogo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblLogo.Name = "lblLogo";
            this.lblLogo.Size = new System.Drawing.Size(434, 49);
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
            this.pbLogo.Location = new System.Drawing.Point(587, 69);
            this.pbLogo.Margin = new System.Windows.Forms.Padding(4);
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
            this.tpDestinations.ImageKey = "server.png";
            this.tpDestinations.Location = new System.Drawing.Point(4, 25);
            this.tpDestinations.Margin = new System.Windows.Forms.Padding(4);
            this.tpDestinations.Name = "tpDestinations";
            this.tpDestinations.Padding = new System.Windows.Forms.Padding(4);
            this.tpDestinations.Size = new System.Drawing.Size(1075, 541);
            this.tpDestinations.TabIndex = 12;
            this.tpDestinations.Text = "Destinations";
            this.tpDestinations.UseVisualStyleBackColor = true;
            // 
            // tcDestinations
            // 
            this.tcDestinations.Controls.Add(this.tpFTP);
            this.tcDestinations.Controls.Add(this.tpLocalhost);
            this.tcDestinations.Controls.Add(this.tpRapidShare);
            this.tcDestinations.Controls.Add(this.tpSendSpace);
            this.tcDestinations.Controls.Add(this.tpDropbox);
            this.tcDestinations.Controls.Add(this.tpImageShack);
            this.tcDestinations.Controls.Add(this.tpTinyPic);
            this.tcDestinations.Controls.Add(this.tpImgur);
            this.tcDestinations.Controls.Add(this.tpFlickr);
            this.tcDestinations.Controls.Add(this.tpTwitter);
            this.tcDestinations.Controls.Add(this.tpImageBam);
            this.tcDestinations.Controls.Add(this.tpMindTouch);
            this.tcDestinations.Controls.Add(this.tpMediaWiki);
            this.tcDestinations.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcDestinations.ImageList = this.ilApp;
            this.tcDestinations.Location = new System.Drawing.Point(4, 4);
            this.tcDestinations.Margin = new System.Windows.Forms.Padding(4);
            this.tcDestinations.Name = "tcDestinations";
            this.tcDestinations.SelectedIndex = 0;
            this.tcDestinations.Size = new System.Drawing.Size(1067, 533);
            this.tcDestinations.TabIndex = 0;
            // 
            // tpFTP
            // 
            this.tpFTP.Controls.Add(this.btnFtpHelp);
            this.tpFTP.Controls.Add(this.btnFTPOpenClient);
            this.tpFTP.Controls.Add(this.ucFTPAccounts);
            this.tpFTP.Controls.Add(this.gbFTPSettings);
            this.tpFTP.Location = new System.Drawing.Point(4, 25);
            this.tpFTP.Margin = new System.Windows.Forms.Padding(4);
            this.tpFTP.Name = "tpFTP";
            this.tpFTP.Padding = new System.Windows.Forms.Padding(4);
            this.tpFTP.Size = new System.Drawing.Size(1059, 504);
            this.tpFTP.TabIndex = 5;
            this.tpFTP.Text = "FTP";
            this.tpFTP.UseVisualStyleBackColor = true;
            // 
            // btnFtpHelp
            // 
            this.btnFtpHelp.Location = new System.Drawing.Point(416, 14);
            this.btnFtpHelp.Margin = new System.Windows.Forms.Padding(4);
            this.btnFtpHelp.Name = "btnFtpHelp";
            this.btnFtpHelp.Size = new System.Drawing.Size(85, 30);
            this.btnFtpHelp.TabIndex = 75;
            this.btnFtpHelp.Text = "&Help...";
            this.btnFtpHelp.UseVisualStyleBackColor = true;
            this.btnFtpHelp.Click += new System.EventHandler(this.btnFtpHelp_Click);
            // 
            // btnFTPOpenClient
            // 
            this.btnFTPOpenClient.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnFTPOpenClient.Location = new System.Drawing.Point(866, 14);
            this.btnFTPOpenClient.Margin = new System.Windows.Forms.Padding(4);
            this.btnFTPOpenClient.Name = "btnFTPOpenClient";
            this.btnFTPOpenClient.Size = new System.Drawing.Size(171, 30);
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
            this.ucFTPAccounts.Location = new System.Drawing.Point(4, 4);
            this.ucFTPAccounts.Margin = new System.Windows.Forms.Padding(5);
            this.ucFTPAccounts.Name = "ucFTPAccounts";
            this.ucFTPAccounts.Size = new System.Drawing.Size(1050, 373);
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
            this.gbFTPSettings.Location = new System.Drawing.Point(11, 374);
            this.gbFTPSettings.Margin = new System.Windows.Forms.Padding(4);
            this.gbFTPSettings.Name = "gbFTPSettings";
            this.gbFTPSettings.Padding = new System.Windows.Forms.Padding(4);
            this.gbFTPSettings.Size = new System.Drawing.Size(1014, 116);
            this.gbFTPSettings.TabIndex = 115;
            this.gbFTPSettings.TabStop = false;
            this.gbFTPSettings.Text = "FTP Settings";
            // 
            // lblFtpFiles
            // 
            this.lblFtpFiles.AutoSize = true;
            this.lblFtpFiles.Location = new System.Drawing.Point(576, 86);
            this.lblFtpFiles.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblFtpFiles.Name = "lblFtpFiles";
            this.lblFtpFiles.Size = new System.Drawing.Size(37, 17);
            this.lblFtpFiles.TabIndex = 136;
            this.lblFtpFiles.Text = "Files";
            // 
            // lblFtpText
            // 
            this.lblFtpText.AutoSize = true;
            this.lblFtpText.Location = new System.Drawing.Point(576, 54);
            this.lblFtpText.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblFtpText.Name = "lblFtpText";
            this.lblFtpText.Size = new System.Drawing.Size(35, 17);
            this.lblFtpText.TabIndex = 135;
            this.lblFtpText.Text = "Text";
            // 
            // lblFtpImages
            // 
            this.lblFtpImages.AutoSize = true;
            this.lblFtpImages.Location = new System.Drawing.Point(559, 23);
            this.lblFtpImages.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblFtpImages.Name = "lblFtpImages";
            this.lblFtpImages.Size = new System.Drawing.Size(53, 17);
            this.lblFtpImages.TabIndex = 134;
            this.lblFtpImages.Text = "Images";
            // 
            // cboFtpFiles
            // 
            this.cboFtpFiles.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboFtpFiles.FormattingEnabled = true;
            this.cboFtpFiles.Location = new System.Drawing.Point(629, 79);
            this.cboFtpFiles.Margin = new System.Windows.Forms.Padding(4);
            this.cboFtpFiles.Name = "cboFtpFiles";
            this.cboFtpFiles.Size = new System.Drawing.Size(361, 24);
            this.cboFtpFiles.TabIndex = 133;
            this.cboFtpFiles.SelectedIndexChanged += new System.EventHandler(this.cboFtpFiles_SelectedIndexChanged);
            // 
            // cboFtpText
            // 
            this.cboFtpText.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboFtpText.FormattingEnabled = true;
            this.cboFtpText.Location = new System.Drawing.Point(629, 49);
            this.cboFtpText.Margin = new System.Windows.Forms.Padding(4);
            this.cboFtpText.Name = "cboFtpText";
            this.cboFtpText.Size = new System.Drawing.Size(361, 24);
            this.cboFtpText.TabIndex = 132;
            this.cboFtpText.SelectedIndexChanged += new System.EventHandler(this.cboFtpText_SelectedIndexChanged);
            // 
            // cboFtpImages
            // 
            this.cboFtpImages.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboFtpImages.FormattingEnabled = true;
            this.cboFtpImages.Location = new System.Drawing.Point(629, 20);
            this.cboFtpImages.Margin = new System.Windows.Forms.Padding(4);
            this.cboFtpImages.Name = "cboFtpImages";
            this.cboFtpImages.Size = new System.Drawing.Size(361, 24);
            this.cboFtpImages.TabIndex = 117;
            this.cboFtpImages.SelectedIndexChanged += new System.EventHandler(this.cboFtpImages_SelectedIndexChanged);
            // 
            // cbFTPThumbnailCheckSize
            // 
            this.cbFTPThumbnailCheckSize.AutoSize = true;
            this.cbFTPThumbnailCheckSize.Location = new System.Drawing.Point(21, 59);
            this.cbFTPThumbnailCheckSize.Margin = new System.Windows.Forms.Padding(4);
            this.cbFTPThumbnailCheckSize.Name = "cbFTPThumbnailCheckSize";
            this.cbFTPThumbnailCheckSize.Size = new System.Drawing.Size(442, 21);
            this.cbFTPThumbnailCheckSize.TabIndex = 131;
            this.cbFTPThumbnailCheckSize.Text = "If image size smaller than thumbnail size then not make thumbnail";
            this.cbFTPThumbnailCheckSize.UseVisualStyleBackColor = true;
            this.cbFTPThumbnailCheckSize.CheckedChanged += new System.EventHandler(this.cbFTPThumbnailCheckSize_CheckedChanged);
            // 
            // lblFTPThumbWidth
            // 
            this.lblFTPThumbWidth.AutoSize = true;
            this.lblFTPThumbWidth.Location = new System.Drawing.Point(21, 31);
            this.lblFTPThumbWidth.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblFTPThumbWidth.Name = "lblFTPThumbWidth";
            this.lblFTPThumbWidth.Size = new System.Drawing.Size(142, 17);
            this.lblFTPThumbWidth.TabIndex = 129;
            this.lblFTPThumbWidth.Text = "Thumbnail width (px):";
            // 
            // txtFTPThumbWidth
            // 
            this.txtFTPThumbWidth.Location = new System.Drawing.Point(171, 27);
            this.txtFTPThumbWidth.Margin = new System.Windows.Forms.Padding(4);
            this.txtFTPThumbWidth.Name = "txtFTPThumbWidth";
            this.txtFTPThumbWidth.Size = new System.Drawing.Size(52, 22);
            this.txtFTPThumbWidth.TabIndex = 127;
            this.txtFTPThumbWidth.Text = "2500";
            this.txtFTPThumbWidth.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtFTPThumbWidth.TextChanged += new System.EventHandler(this.txtFTPThumbWidth_TextChanged);
            // 
            // tpLocalhost
            // 
            this.tpLocalhost.Controls.Add(this.ucLocalhostAccounts);
            this.tpLocalhost.Location = new System.Drawing.Point(4, 25);
            this.tpLocalhost.Margin = new System.Windows.Forms.Padding(4);
            this.tpLocalhost.Name = "tpLocalhost";
            this.tpLocalhost.Padding = new System.Windows.Forms.Padding(4);
            this.tpLocalhost.Size = new System.Drawing.Size(1059, 504);
            this.tpLocalhost.TabIndex = 11;
            this.tpLocalhost.Text = "Localhost";
            this.tpLocalhost.UseVisualStyleBackColor = true;
            // 
            // ucLocalhostAccounts
            // 
            this.ucLocalhostAccounts.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.ucLocalhostAccounts.Location = new System.Drawing.Point(4, 4);
            this.ucLocalhostAccounts.Margin = new System.Windows.Forms.Padding(5);
            this.ucLocalhostAccounts.Name = "ucLocalhostAccounts";
            this.ucLocalhostAccounts.Size = new System.Drawing.Size(1050, 387);
            this.ucLocalhostAccounts.TabIndex = 1;
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
            this.tpRapidShare.Location = new System.Drawing.Point(4, 25);
            this.tpRapidShare.Margin = new System.Windows.Forms.Padding(4);
            this.tpRapidShare.Name = "tpRapidShare";
            this.tpRapidShare.Padding = new System.Windows.Forms.Padding(4);
            this.tpRapidShare.Size = new System.Drawing.Size(1059, 504);
            this.tpRapidShare.TabIndex = 8;
            this.tpRapidShare.Text = "RapidShare";
            this.tpRapidShare.UseVisualStyleBackColor = true;
            // 
            // lblRapidSharePassword
            // 
            this.lblRapidSharePassword.AutoSize = true;
            this.lblRapidSharePassword.Location = new System.Drawing.Point(96, 148);
            this.lblRapidSharePassword.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblRapidSharePassword.Name = "lblRapidSharePassword";
            this.lblRapidSharePassword.Size = new System.Drawing.Size(69, 17);
            this.lblRapidSharePassword.TabIndex = 7;
            this.lblRapidSharePassword.Text = "Password";
            // 
            // lblRapidSharePremiumUsername
            // 
            this.lblRapidSharePremiumUsername.AutoSize = true;
            this.lblRapidSharePremiumUsername.Location = new System.Drawing.Point(29, 108);
            this.lblRapidSharePremiumUsername.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblRapidSharePremiumUsername.Name = "lblRapidSharePremiumUsername";
            this.lblRapidSharePremiumUsername.Size = new System.Drawing.Size(138, 17);
            this.lblRapidSharePremiumUsername.TabIndex = 6;
            this.lblRapidSharePremiumUsername.Text = "Premium User Name";
            // 
            // lblRapidShareCollectorsID
            // 
            this.lblRapidShareCollectorsID.AutoSize = true;
            this.lblRapidShareCollectorsID.Location = new System.Drawing.Point(75, 69);
            this.lblRapidShareCollectorsID.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblRapidShareCollectorsID.Name = "lblRapidShareCollectorsID";
            this.lblRapidShareCollectorsID.Size = new System.Drawing.Size(90, 17);
            this.lblRapidShareCollectorsID.TabIndex = 5;
            this.lblRapidShareCollectorsID.Text = "Collector\'s ID";
            // 
            // txtRapidSharePassword
            // 
            this.txtRapidSharePassword.Location = new System.Drawing.Point(181, 144);
            this.txtRapidSharePassword.Margin = new System.Windows.Forms.Padding(4);
            this.txtRapidSharePassword.Name = "txtRapidSharePassword";
            this.txtRapidSharePassword.PasswordChar = '*';
            this.txtRapidSharePassword.Size = new System.Drawing.Size(159, 22);
            this.txtRapidSharePassword.TabIndex = 4;
            this.txtRapidSharePassword.TextChanged += new System.EventHandler(this.txtRapidSharePassword_TextChanged);
            // 
            // txtRapidSharePremiumUserName
            // 
            this.txtRapidSharePremiumUserName.Location = new System.Drawing.Point(181, 105);
            this.txtRapidSharePremiumUserName.Margin = new System.Windows.Forms.Padding(4);
            this.txtRapidSharePremiumUserName.Name = "txtRapidSharePremiumUserName";
            this.txtRapidSharePremiumUserName.Size = new System.Drawing.Size(159, 22);
            this.txtRapidSharePremiumUserName.TabIndex = 3;
            this.txtRapidSharePremiumUserName.TextChanged += new System.EventHandler(this.txtRapidSharePremiumUserName_TextChanged);
            // 
            // txtRapidShareCollectorID
            // 
            this.txtRapidShareCollectorID.Location = new System.Drawing.Point(181, 65);
            this.txtRapidShareCollectorID.Margin = new System.Windows.Forms.Padding(4);
            this.txtRapidShareCollectorID.Name = "txtRapidShareCollectorID";
            this.txtRapidShareCollectorID.Size = new System.Drawing.Size(159, 22);
            this.txtRapidShareCollectorID.TabIndex = 2;
            this.txtRapidShareCollectorID.TextChanged += new System.EventHandler(this.txtRapidShareCollectorID_TextChanged);
            // 
            // cboRapidShareAcctType
            // 
            this.cboRapidShareAcctType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboRapidShareAcctType.FormattingEnabled = true;
            this.cboRapidShareAcctType.Location = new System.Drawing.Point(181, 26);
            this.cboRapidShareAcctType.Margin = new System.Windows.Forms.Padding(4);
            this.cboRapidShareAcctType.Name = "cboRapidShareAcctType";
            this.cboRapidShareAcctType.Size = new System.Drawing.Size(160, 24);
            this.cboRapidShareAcctType.TabIndex = 1;
            this.cboRapidShareAcctType.SelectedIndexChanged += new System.EventHandler(this.cboRapidShareAcctType_SelectedIndexChanged);
            // 
            // lblRapidShareAccountType
            // 
            this.lblRapidShareAccountType.AutoSize = true;
            this.lblRapidShareAccountType.Location = new System.Drawing.Point(68, 30);
            this.lblRapidShareAccountType.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblRapidShareAccountType.Name = "lblRapidShareAccountType";
            this.lblRapidShareAccountType.Size = new System.Drawing.Size(95, 17);
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
            this.tpSendSpace.Location = new System.Drawing.Point(4, 25);
            this.tpSendSpace.Margin = new System.Windows.Forms.Padding(4);
            this.tpSendSpace.Name = "tpSendSpace";
            this.tpSendSpace.Padding = new System.Windows.Forms.Padding(4);
            this.tpSendSpace.Size = new System.Drawing.Size(1059, 504);
            this.tpSendSpace.TabIndex = 9;
            this.tpSendSpace.Text = "SendSpace";
            this.tpSendSpace.UseVisualStyleBackColor = true;
            // 
            // btnSendSpaceRegister
            // 
            this.btnSendSpaceRegister.Location = new System.Drawing.Point(341, 30);
            this.btnSendSpaceRegister.Margin = new System.Windows.Forms.Padding(4);
            this.btnSendSpaceRegister.Name = "btnSendSpaceRegister";
            this.btnSendSpaceRegister.Size = new System.Drawing.Size(100, 28);
            this.btnSendSpaceRegister.TabIndex = 16;
            this.btnSendSpaceRegister.Text = "&Register...";
            this.btnSendSpaceRegister.UseVisualStyleBackColor = true;
            this.btnSendSpaceRegister.Click += new System.EventHandler(this.btnSendSpaceRegister_Click);
            // 
            // lblSendSpacePassword
            // 
            this.lblSendSpacePassword.AutoSize = true;
            this.lblSendSpacePassword.Location = new System.Drawing.Point(85, 113);
            this.lblSendSpacePassword.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSendSpacePassword.Name = "lblSendSpacePassword";
            this.lblSendSpacePassword.Size = new System.Drawing.Size(69, 17);
            this.lblSendSpacePassword.TabIndex = 15;
            this.lblSendSpacePassword.Text = "Password";
            // 
            // lblSendSpaceUsername
            // 
            this.lblSendSpaceUsername.AutoSize = true;
            this.lblSendSpaceUsername.Location = new System.Drawing.Point(75, 74);
            this.lblSendSpaceUsername.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSendSpaceUsername.Name = "lblSendSpaceUsername";
            this.lblSendSpaceUsername.Size = new System.Drawing.Size(79, 17);
            this.lblSendSpaceUsername.TabIndex = 14;
            this.lblSendSpaceUsername.Text = "User Name";
            // 
            // txtSendSpacePassword
            // 
            this.txtSendSpacePassword.Location = new System.Drawing.Point(171, 110);
            this.txtSendSpacePassword.Margin = new System.Windows.Forms.Padding(4);
            this.txtSendSpacePassword.Name = "txtSendSpacePassword";
            this.txtSendSpacePassword.PasswordChar = '*';
            this.txtSendSpacePassword.Size = new System.Drawing.Size(159, 22);
            this.txtSendSpacePassword.TabIndex = 12;
            this.txtSendSpacePassword.TextChanged += new System.EventHandler(this.txtSendSpacePassword_TextChanged);
            // 
            // txtSendSpaceUserName
            // 
            this.txtSendSpaceUserName.Location = new System.Drawing.Point(171, 70);
            this.txtSendSpaceUserName.Margin = new System.Windows.Forms.Padding(4);
            this.txtSendSpaceUserName.Name = "txtSendSpaceUserName";
            this.txtSendSpaceUserName.Size = new System.Drawing.Size(159, 22);
            this.txtSendSpaceUserName.TabIndex = 11;
            this.txtSendSpaceUserName.TextChanged += new System.EventHandler(this.txtSendSpaceUserName_TextChanged);
            // 
            // cboSendSpaceAcctType
            // 
            this.cboSendSpaceAcctType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboSendSpaceAcctType.FormattingEnabled = true;
            this.cboSendSpaceAcctType.Location = new System.Drawing.Point(171, 31);
            this.cboSendSpaceAcctType.Margin = new System.Windows.Forms.Padding(4);
            this.cboSendSpaceAcctType.Name = "cboSendSpaceAcctType";
            this.cboSendSpaceAcctType.Size = new System.Drawing.Size(160, 24);
            this.cboSendSpaceAcctType.TabIndex = 9;
            this.cboSendSpaceAcctType.SelectedIndexChanged += new System.EventHandler(this.cboSendSpaceAcctType_SelectedIndexChanged);
            // 
            // lblSendSpaceAccountType
            // 
            this.lblSendSpaceAccountType.AutoSize = true;
            this.lblSendSpaceAccountType.Location = new System.Drawing.Point(57, 34);
            this.lblSendSpaceAccountType.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSendSpaceAccountType.Name = "lblSendSpaceAccountType";
            this.lblSendSpaceAccountType.Size = new System.Drawing.Size(95, 17);
            this.lblSendSpaceAccountType.TabIndex = 8;
            this.lblSendSpaceAccountType.Text = "Account Type";
            // 
            // tpDropbox
            // 
            this.tpDropbox.Controls.Add(this.lblDropboxPasswordTip);
            this.tpDropbox.Controls.Add(this.pbDropboxLogo);
            this.tpDropbox.Controls.Add(this.lblDropboxLoginTip);
            this.tpDropbox.Controls.Add(this.btnDropboxRegister);
            this.tpDropbox.Controls.Add(this.lblDropboxStatus);
            this.tpDropbox.Controls.Add(this.lblDropboxPathTip);
            this.tpDropbox.Controls.Add(this.lblDropboxPath);
            this.tpDropbox.Controls.Add(this.lblDropboxPassword);
            this.tpDropbox.Controls.Add(this.lblDropboxEmail);
            this.tpDropbox.Controls.Add(this.btnDropboxLogin);
            this.tpDropbox.Controls.Add(this.txtDropboxPath);
            this.tpDropbox.Controls.Add(this.txtDropboxPassword);
            this.tpDropbox.Controls.Add(this.txtDropboxEmail);
            this.tpDropbox.Location = new System.Drawing.Point(4, 25);
            this.tpDropbox.Margin = new System.Windows.Forms.Padding(4);
            this.tpDropbox.Name = "tpDropbox";
            this.tpDropbox.Padding = new System.Windows.Forms.Padding(4);
            this.tpDropbox.Size = new System.Drawing.Size(1059, 504);
            this.tpDropbox.TabIndex = 14;
            this.tpDropbox.Text = "Dropbox";
            this.tpDropbox.UseVisualStyleBackColor = true;
            // 
            // lblDropboxPasswordTip
            // 
            this.lblDropboxPasswordTip.AutoSize = true;
            this.lblDropboxPasswordTip.Location = new System.Drawing.Point(459, 158);
            this.lblDropboxPasswordTip.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblDropboxPasswordTip.Name = "lblDropboxPasswordTip";
            this.lblDropboxPasswordTip.Size = new System.Drawing.Size(171, 17);
            this.lblDropboxPasswordTip.TabIndex = 12;
            this.lblDropboxPasswordTip.Text = "Password won\'t be saved.";
            // 
            // pbDropboxLogo
            // 
            this.pbDropboxLogo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pbDropboxLogo.Image = global::ZScreenGUI.Properties.Resources.DropboxLogo;
            this.pbDropboxLogo.Location = new System.Drawing.Point(21, 20);
            this.pbDropboxLogo.Margin = new System.Windows.Forms.Padding(4);
            this.pbDropboxLogo.Name = "pbDropboxLogo";
            this.pbDropboxLogo.Size = new System.Drawing.Size(331, 79);
            this.pbDropboxLogo.TabIndex = 11;
            this.pbDropboxLogo.TabStop = false;
            this.pbDropboxLogo.Click += new System.EventHandler(this.pbDropboxLogo_Click);
            // 
            // lblDropboxLoginTip
            // 
            this.lblDropboxLoginTip.AutoSize = true;
            this.lblDropboxLoginTip.Location = new System.Drawing.Point(256, 238);
            this.lblDropboxLoginTip.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblDropboxLoginTip.Name = "lblDropboxLoginTip";
            this.lblDropboxLoginTip.Size = new System.Drawing.Size(206, 17);
            this.lblDropboxLoginTip.TabIndex = 10;
            this.lblDropboxLoginTip.Text = "Login is only one time required.";
            // 
            // btnDropboxRegister
            // 
            this.btnDropboxRegister.Location = new System.Drawing.Point(139, 231);
            this.btnDropboxRegister.Margin = new System.Windows.Forms.Padding(4);
            this.btnDropboxRegister.Name = "btnDropboxRegister";
            this.btnDropboxRegister.Size = new System.Drawing.Size(107, 28);
            this.btnDropboxRegister.TabIndex = 4;
            this.btnDropboxRegister.Text = "Register...";
            this.btnDropboxRegister.UseVisualStyleBackColor = true;
            this.btnDropboxRegister.Click += new System.EventHandler(this.btnDropboxRegister_Click);
            // 
            // lblDropboxStatus
            // 
            this.lblDropboxStatus.AutoSize = true;
            this.lblDropboxStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblDropboxStatus.Location = new System.Drawing.Point(21, 281);
            this.lblDropboxStatus.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblDropboxStatus.Name = "lblDropboxStatus";
            this.lblDropboxStatus.Size = new System.Drawing.Size(106, 20);
            this.lblDropboxStatus.TabIndex = 8;
            this.lblDropboxStatus.Text = "Login status:";
            // 
            // lblDropboxPathTip
            // 
            this.lblDropboxPathTip.AutoSize = true;
            this.lblDropboxPathTip.Location = new System.Drawing.Point(459, 197);
            this.lblDropboxPathTip.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblDropboxPathTip.Name = "lblDropboxPathTip";
            this.lblDropboxPathTip.Size = new System.Drawing.Size(273, 17);
            this.lblDropboxPathTip.TabIndex = 7;
            this.lblDropboxPathTip.Text = "Use \"Public\" folder for be able to get URL.";
            // 
            // lblDropboxPath
            // 
            this.lblDropboxPath.AutoSize = true;
            this.lblDropboxPath.Location = new System.Drawing.Point(21, 197);
            this.lblDropboxPath.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblDropboxPath.Name = "lblDropboxPath";
            this.lblDropboxPath.Size = new System.Drawing.Size(89, 17);
            this.lblDropboxPath.TabIndex = 6;
            this.lblDropboxPath.Text = "Upload path:";
            // 
            // lblDropboxPassword
            // 
            this.lblDropboxPassword.AutoSize = true;
            this.lblDropboxPassword.Location = new System.Drawing.Point(21, 158);
            this.lblDropboxPassword.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblDropboxPassword.Name = "lblDropboxPassword";
            this.lblDropboxPassword.Size = new System.Drawing.Size(73, 17);
            this.lblDropboxPassword.TabIndex = 5;
            this.lblDropboxPassword.Text = "Password:";
            // 
            // lblDropboxEmail
            // 
            this.lblDropboxEmail.AutoSize = true;
            this.lblDropboxEmail.Location = new System.Drawing.Point(21, 118);
            this.lblDropboxEmail.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblDropboxEmail.Name = "lblDropboxEmail";
            this.lblDropboxEmail.Size = new System.Drawing.Size(46, 17);
            this.lblDropboxEmail.TabIndex = 4;
            this.lblDropboxEmail.Text = "Email:";
            // 
            // btnDropboxLogin
            // 
            this.btnDropboxLogin.Location = new System.Drawing.Point(21, 231);
            this.btnDropboxLogin.Margin = new System.Windows.Forms.Padding(4);
            this.btnDropboxLogin.Name = "btnDropboxLogin";
            this.btnDropboxLogin.Size = new System.Drawing.Size(107, 30);
            this.btnDropboxLogin.TabIndex = 3;
            this.btnDropboxLogin.Text = "Login";
            this.btnDropboxLogin.UseVisualStyleBackColor = true;
            this.btnDropboxLogin.Click += new System.EventHandler(this.btnDropboxLogin_Click);
            // 
            // txtDropboxPath
            // 
            this.txtDropboxPath.Location = new System.Drawing.Point(117, 192);
            this.txtDropboxPath.Margin = new System.Windows.Forms.Padding(4);
            this.txtDropboxPath.Name = "txtDropboxPath";
            this.txtDropboxPath.Size = new System.Drawing.Size(329, 22);
            this.txtDropboxPath.TabIndex = 2;
            this.txtDropboxPath.TextChanged += new System.EventHandler(this.txtDropboxPath_TextChanged);
            // 
            // txtDropboxPassword
            // 
            this.txtDropboxPassword.Location = new System.Drawing.Point(117, 153);
            this.txtDropboxPassword.Margin = new System.Windows.Forms.Padding(4);
            this.txtDropboxPassword.Name = "txtDropboxPassword";
            this.txtDropboxPassword.PasswordChar = '*';
            this.txtDropboxPassword.Size = new System.Drawing.Size(329, 22);
            this.txtDropboxPassword.TabIndex = 1;
            // 
            // txtDropboxEmail
            // 
            this.txtDropboxEmail.Location = new System.Drawing.Point(117, 113);
            this.txtDropboxEmail.Margin = new System.Windows.Forms.Padding(4);
            this.txtDropboxEmail.Name = "txtDropboxEmail";
            this.txtDropboxEmail.Size = new System.Drawing.Size(329, 22);
            this.txtDropboxEmail.TabIndex = 0;
            // 
            // tpImageShack
            // 
            this.tpImageShack.Controls.Add(this.chkPublicImageShack);
            this.tpImageShack.Controls.Add(this.gbImageShack);
            this.tpImageShack.Location = new System.Drawing.Point(4, 25);
            this.tpImageShack.Margin = new System.Windows.Forms.Padding(4);
            this.tpImageShack.Name = "tpImageShack";
            this.tpImageShack.Padding = new System.Windows.Forms.Padding(4);
            this.tpImageShack.Size = new System.Drawing.Size(1059, 504);
            this.tpImageShack.TabIndex = 1;
            this.tpImageShack.Text = "ImageShack";
            this.tpImageShack.UseVisualStyleBackColor = true;
            // 
            // chkPublicImageShack
            // 
            this.chkPublicImageShack.AutoSize = true;
            this.chkPublicImageShack.Location = new System.Drawing.Point(21, 128);
            this.chkPublicImageShack.Margin = new System.Windows.Forms.Padding(4);
            this.chkPublicImageShack.Name = "chkPublicImageShack";
            this.chkPublicImageShack.Size = new System.Drawing.Size(406, 21);
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
            this.gbImageShack.Location = new System.Drawing.Point(11, 10);
            this.gbImageShack.Margin = new System.Windows.Forms.Padding(4);
            this.gbImageShack.Name = "gbImageShack";
            this.gbImageShack.Padding = new System.Windows.Forms.Padding(4);
            this.gbImageShack.Size = new System.Drawing.Size(1026, 108);
            this.gbImageShack.TabIndex = 0;
            this.gbImageShack.TabStop = false;
            this.gbImageShack.Text = "Account";
            // 
            // btnImageShackProfile
            // 
            this.btnImageShackProfile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnImageShackProfile.Location = new System.Drawing.Point(631, 64);
            this.btnImageShackProfile.Margin = new System.Windows.Forms.Padding(4);
            this.btnImageShackProfile.Name = "btnImageShackProfile";
            this.btnImageShackProfile.Size = new System.Drawing.Size(245, 28);
            this.btnImageShackProfile.TabIndex = 6;
            this.btnImageShackProfile.Text = "&Public Profile...";
            this.btnImageShackProfile.UseVisualStyleBackColor = true;
            this.btnImageShackProfile.Click += new System.EventHandler(this.btnImageShackProfile_Click);
            // 
            // lblImageShackUsername
            // 
            this.lblImageShackUsername.AutoSize = true;
            this.lblImageShackUsername.Location = new System.Drawing.Point(64, 64);
            this.lblImageShackUsername.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblImageShackUsername.Name = "lblImageShackUsername";
            this.lblImageShackUsername.Size = new System.Drawing.Size(83, 17);
            this.lblImageShackUsername.TabIndex = 5;
            this.lblImageShackUsername.Text = "User Name:";
            // 
            // txtUserNameImageShack
            // 
            this.txtUserNameImageShack.Location = new System.Drawing.Point(160, 64);
            this.txtUserNameImageShack.Margin = new System.Windows.Forms.Padding(4);
            this.txtUserNameImageShack.Name = "txtUserNameImageShack";
            this.txtUserNameImageShack.Size = new System.Drawing.Size(457, 22);
            this.txtUserNameImageShack.TabIndex = 4;
            this.txtUserNameImageShack.TextChanged += new System.EventHandler(this.txtUserNameImageShack_TextChanged);
            // 
            // btnGalleryImageShack
            // 
            this.btnGalleryImageShack.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGalleryImageShack.Location = new System.Drawing.Point(759, 25);
            this.btnGalleryImageShack.Margin = new System.Windows.Forms.Padding(4);
            this.btnGalleryImageShack.Name = "btnGalleryImageShack";
            this.btnGalleryImageShack.Size = new System.Drawing.Size(117, 28);
            this.btnGalleryImageShack.TabIndex = 3;
            this.btnGalleryImageShack.Text = "&MyImages...";
            this.btnGalleryImageShack.UseVisualStyleBackColor = true;
            this.btnGalleryImageShack.Click += new System.EventHandler(this.btnGalleryImageShack_Click);
            // 
            // btnRegCodeImageShack
            // 
            this.btnRegCodeImageShack.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRegCodeImageShack.Location = new System.Drawing.Point(631, 25);
            this.btnRegCodeImageShack.Margin = new System.Windows.Forms.Padding(4);
            this.btnRegCodeImageShack.Name = "btnRegCodeImageShack";
            this.btnRegCodeImageShack.Size = new System.Drawing.Size(117, 28);
            this.btnRegCodeImageShack.TabIndex = 2;
            this.btnRegCodeImageShack.Text = "&RegCode...";
            this.btnRegCodeImageShack.UseVisualStyleBackColor = true;
            this.btnRegCodeImageShack.Click += new System.EventHandler(this.btnRegCodeImageShack_Click);
            // 
            // lblImageShackRegistrationCode
            // 
            this.lblImageShackRegistrationCode.AutoSize = true;
            this.lblImageShackRegistrationCode.Location = new System.Drawing.Point(21, 30);
            this.lblImageShackRegistrationCode.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblImageShackRegistrationCode.Name = "lblImageShackRegistrationCode";
            this.lblImageShackRegistrationCode.Size = new System.Drawing.Size(123, 17);
            this.lblImageShackRegistrationCode.TabIndex = 1;
            this.lblImageShackRegistrationCode.Text = "Registration code:";
            // 
            // txtImageShackRegistrationCode
            // 
            this.txtImageShackRegistrationCode.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtImageShackRegistrationCode.Location = new System.Drawing.Point(160, 25);
            this.txtImageShackRegistrationCode.Margin = new System.Windows.Forms.Padding(4);
            this.txtImageShackRegistrationCode.Name = "txtImageShackRegistrationCode";
            this.txtImageShackRegistrationCode.Size = new System.Drawing.Size(459, 22);
            this.txtImageShackRegistrationCode.TabIndex = 0;
            this.txtImageShackRegistrationCode.TextChanged += new System.EventHandler(this.txtImageShackRegistrationCode_TextChanged);
            // 
            // tpTinyPic
            // 
            this.tpTinyPic.Controls.Add(this.gbTinyPic);
            this.tpTinyPic.Controls.Add(this.chkRememberTinyPicUserPass);
            this.tpTinyPic.Location = new System.Drawing.Point(4, 25);
            this.tpTinyPic.Margin = new System.Windows.Forms.Padding(4);
            this.tpTinyPic.Name = "tpTinyPic";
            this.tpTinyPic.Padding = new System.Windows.Forms.Padding(4);
            this.tpTinyPic.Size = new System.Drawing.Size(1059, 504);
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
            this.gbTinyPic.Location = new System.Drawing.Point(21, 20);
            this.gbTinyPic.Margin = new System.Windows.Forms.Padding(4);
            this.gbTinyPic.Name = "gbTinyPic";
            this.gbTinyPic.Padding = new System.Windows.Forms.Padding(4);
            this.gbTinyPic.Size = new System.Drawing.Size(1015, 79);
            this.gbTinyPic.TabIndex = 4;
            this.gbTinyPic.TabStop = false;
            this.gbTinyPic.Text = "Account";
            // 
            // btnGalleryTinyPic
            // 
            this.btnGalleryTinyPic.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGalleryTinyPic.Location = new System.Drawing.Point(898, 30);
            this.btnGalleryTinyPic.Margin = new System.Windows.Forms.Padding(4);
            this.btnGalleryTinyPic.Name = "btnGalleryTinyPic";
            this.btnGalleryTinyPic.Size = new System.Drawing.Size(100, 28);
            this.btnGalleryTinyPic.TabIndex = 8;
            this.btnGalleryTinyPic.Text = "&MyImages...";
            this.btnGalleryTinyPic.UseVisualStyleBackColor = true;
            this.btnGalleryTinyPic.Click += new System.EventHandler(this.btnGalleryTinyPic_Click);
            // 
            // btnRegCodeTinyPic
            // 
            this.btnRegCodeTinyPic.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRegCodeTinyPic.Location = new System.Drawing.Point(791, 30);
            this.btnRegCodeTinyPic.Margin = new System.Windows.Forms.Padding(4);
            this.btnRegCodeTinyPic.Name = "btnRegCodeTinyPic";
            this.btnRegCodeTinyPic.Size = new System.Drawing.Size(100, 28);
            this.btnRegCodeTinyPic.TabIndex = 5;
            this.btnRegCodeTinyPic.Text = "&RegCode...";
            this.btnRegCodeTinyPic.UseVisualStyleBackColor = true;
            this.btnRegCodeTinyPic.Click += new System.EventHandler(this.btnRegCodeTinyPic_Click);
            // 
            // lblRegistrationCode
            // 
            this.lblRegistrationCode.AutoSize = true;
            this.lblRegistrationCode.Location = new System.Drawing.Point(11, 34);
            this.lblRegistrationCode.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblRegistrationCode.Name = "lblRegistrationCode";
            this.lblRegistrationCode.Size = new System.Drawing.Size(123, 17);
            this.lblRegistrationCode.TabIndex = 4;
            this.lblRegistrationCode.Text = "Registration code:";
            // 
            // txtTinyPicShuk
            // 
            this.txtTinyPicShuk.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTinyPicShuk.Location = new System.Drawing.Point(149, 30);
            this.txtTinyPicShuk.Margin = new System.Windows.Forms.Padding(4);
            this.txtTinyPicShuk.Name = "txtTinyPicShuk";
            this.txtTinyPicShuk.Size = new System.Drawing.Size(630, 22);
            this.txtTinyPicShuk.TabIndex = 3;
            this.txtTinyPicShuk.TextChanged += new System.EventHandler(this.txtTinyPicShuk_TextChanged);
            // 
            // chkRememberTinyPicUserPass
            // 
            this.chkRememberTinyPicUserPass.AutoSize = true;
            this.chkRememberTinyPicUserPass.Location = new System.Drawing.Point(32, 108);
            this.chkRememberTinyPicUserPass.Margin = new System.Windows.Forms.Padding(4);
            this.chkRememberTinyPicUserPass.Name = "chkRememberTinyPicUserPass";
            this.chkRememberTinyPicUserPass.Size = new System.Drawing.Size(317, 21);
            this.chkRememberTinyPicUserPass.TabIndex = 6;
            this.chkRememberTinyPicUserPass.Text = "Remember TinyPic User Name and Password";
            this.chkRememberTinyPicUserPass.UseVisualStyleBackColor = true;
            this.chkRememberTinyPicUserPass.CheckedChanged += new System.EventHandler(this.chkRememberTinyPicUserPass_CheckedChanged);
            // 
            // tpImgur
            // 
            this.tpImgur.Controls.Add(this.cbImgurUseAccount);
            this.tpImgur.Controls.Add(this.lblImgurStatus);
            this.tpImgur.Controls.Add(this.lblImgurHowTo);
            this.tpImgur.Controls.Add(this.btnImgurLogin);
            this.tpImgur.Controls.Add(this.lblImgurVerificationCode);
            this.tpImgur.Controls.Add(this.tbImgurVerificationCode);
            this.tpImgur.Controls.Add(this.btnImgurOpenAuthorizePage);
            this.tpImgur.Location = new System.Drawing.Point(4, 25);
            this.tpImgur.Margin = new System.Windows.Forms.Padding(4);
            this.tpImgur.Name = "tpImgur";
            this.tpImgur.Padding = new System.Windows.Forms.Padding(4);
            this.tpImgur.Size = new System.Drawing.Size(1059, 504);
            this.tpImgur.TabIndex = 15;
            this.tpImgur.Text = "Imgur";
            this.tpImgur.UseVisualStyleBackColor = true;
            // 
            // cbImgurUseAccount
            // 
            this.cbImgurUseAccount.AutoSize = true;
            this.cbImgurUseAccount.Location = new System.Drawing.Point(21, 20);
            this.cbImgurUseAccount.Margin = new System.Windows.Forms.Padding(4);
            this.cbImgurUseAccount.Name = "cbImgurUseAccount";
            this.cbImgurUseAccount.Size = new System.Drawing.Size(141, 21);
            this.cbImgurUseAccount.TabIndex = 6;
            this.cbImgurUseAccount.Text = "Use user account";
            this.cbImgurUseAccount.UseVisualStyleBackColor = true;
            this.cbImgurUseAccount.CheckedChanged += new System.EventHandler(this.cbImgurUseAccount_CheckedChanged);
            // 
            // lblImgurStatus
            // 
            this.lblImgurStatus.AutoSize = true;
            this.lblImgurStatus.Location = new System.Drawing.Point(21, 177);
            this.lblImgurStatus.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblImgurStatus.Name = "lblImgurStatus";
            this.lblImgurStatus.Size = new System.Drawing.Size(114, 17);
            this.lblImgurStatus.TabIndex = 5;
            this.lblImgurStatus.Text = "Login is required";
            // 
            // lblImgurHowTo
            // 
            this.lblImgurHowTo.AutoSize = true;
            this.lblImgurHowTo.Location = new System.Drawing.Point(21, 49);
            this.lblImgurHowTo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblImgurHowTo.Name = "lblImgurHowTo";
            this.lblImgurHowTo.Size = new System.Drawing.Size(397, 34);
            this.lblImgurHowTo.TabIndex = 4;
            this.lblImgurHowTo.Text = "Open authorize page for web based secure login.\r\nAfter login copy verification co" +
                "de to ZScreen and press Login.";
            // 
            // btnImgurLogin
            // 
            this.btnImgurLogin.Location = new System.Drawing.Point(427, 137);
            this.btnImgurLogin.Margin = new System.Windows.Forms.Padding(4);
            this.btnImgurLogin.Name = "btnImgurLogin";
            this.btnImgurLogin.Size = new System.Drawing.Size(100, 28);
            this.btnImgurLogin.TabIndex = 3;
            this.btnImgurLogin.Text = "Login";
            this.btnImgurLogin.UseVisualStyleBackColor = true;
            this.btnImgurLogin.Click += new System.EventHandler(this.btnImgurLogin_Click);
            // 
            // lblImgurVerificationCode
            // 
            this.lblImgurVerificationCode.AutoSize = true;
            this.lblImgurVerificationCode.Location = new System.Drawing.Point(21, 142);
            this.lblImgurVerificationCode.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblImgurVerificationCode.Name = "lblImgurVerificationCode";
            this.lblImgurVerificationCode.Size = new System.Drawing.Size(117, 17);
            this.lblImgurVerificationCode.TabIndex = 2;
            this.lblImgurVerificationCode.Text = "Verification code:";
            // 
            // tbImgurVerificationCode
            // 
            this.tbImgurVerificationCode.Location = new System.Drawing.Point(149, 138);
            this.tbImgurVerificationCode.Margin = new System.Windows.Forms.Padding(4);
            this.tbImgurVerificationCode.Name = "tbImgurVerificationCode";
            this.tbImgurVerificationCode.Size = new System.Drawing.Size(265, 22);
            this.tbImgurVerificationCode.TabIndex = 1;
            // 
            // btnImgurOpenAuthorizePage
            // 
            this.btnImgurOpenAuthorizePage.Location = new System.Drawing.Point(21, 98);
            this.btnImgurOpenAuthorizePage.Margin = new System.Windows.Forms.Padding(4);
            this.btnImgurOpenAuthorizePage.Name = "btnImgurOpenAuthorizePage";
            this.btnImgurOpenAuthorizePage.Size = new System.Drawing.Size(181, 28);
            this.btnImgurOpenAuthorizePage.TabIndex = 0;
            this.btnImgurOpenAuthorizePage.Text = "Open authorize page";
            this.btnImgurOpenAuthorizePage.UseVisualStyleBackColor = true;
            this.btnImgurOpenAuthorizePage.Click += new System.EventHandler(this.btnImgurOpenAuthorizePage_Click);
            // 
            // tpFlickr
            // 
            this.tpFlickr.Controls.Add(this.btnFlickrOpenImages);
            this.tpFlickr.Controls.Add(this.pgFlickrAuthInfo);
            this.tpFlickr.Controls.Add(this.pgFlickrSettings);
            this.tpFlickr.Controls.Add(this.btnFlickrCheckToken);
            this.tpFlickr.Controls.Add(this.btnFlickrGetToken);
            this.tpFlickr.Controls.Add(this.btnFlickrGetFrob);
            this.tpFlickr.Location = new System.Drawing.Point(4, 25);
            this.tpFlickr.Margin = new System.Windows.Forms.Padding(4);
            this.tpFlickr.Name = "tpFlickr";
            this.tpFlickr.Padding = new System.Windows.Forms.Padding(4);
            this.tpFlickr.Size = new System.Drawing.Size(1059, 504);
            this.tpFlickr.TabIndex = 10;
            this.tpFlickr.Text = "Flickr";
            this.tpFlickr.UseVisualStyleBackColor = true;
            // 
            // btnFlickrOpenImages
            // 
            this.btnFlickrOpenImages.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnFlickrOpenImages.Location = new System.Drawing.Point(810, 226);
            this.btnFlickrOpenImages.Margin = new System.Windows.Forms.Padding(4);
            this.btnFlickrOpenImages.Name = "btnFlickrOpenImages";
            this.btnFlickrOpenImages.Size = new System.Drawing.Size(224, 28);
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
            this.pgFlickrAuthInfo.Location = new System.Drawing.Point(21, 22);
            this.pgFlickrAuthInfo.Margin = new System.Windows.Forms.Padding(4);
            this.pgFlickrAuthInfo.Name = "pgFlickrAuthInfo";
            this.pgFlickrAuthInfo.PropertySort = System.Windows.Forms.PropertySort.NoSort;
            this.pgFlickrAuthInfo.Size = new System.Drawing.Size(781, 197);
            this.pgFlickrAuthInfo.TabIndex = 6;
            this.pgFlickrAuthInfo.ToolbarVisible = false;
            // 
            // pgFlickrSettings
            // 
            this.pgFlickrSettings.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pgFlickrSettings.CommandsVisibleIfAvailable = false;
            this.pgFlickrSettings.Location = new System.Drawing.Point(21, 226);
            this.pgFlickrSettings.Margin = new System.Windows.Forms.Padding(4);
            this.pgFlickrSettings.Name = "pgFlickrSettings";
            this.pgFlickrSettings.PropertySort = System.Windows.Forms.PropertySort.NoSort;
            this.pgFlickrSettings.Size = new System.Drawing.Size(781, 260);
            this.pgFlickrSettings.TabIndex = 5;
            this.pgFlickrSettings.ToolbarVisible = false;
            // 
            // btnFlickrCheckToken
            // 
            this.btnFlickrCheckToken.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnFlickrCheckToken.Location = new System.Drawing.Point(810, 191);
            this.btnFlickrCheckToken.Margin = new System.Windows.Forms.Padding(4);
            this.btnFlickrCheckToken.Name = "btnFlickrCheckToken";
            this.btnFlickrCheckToken.Size = new System.Drawing.Size(224, 28);
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
            this.btnFlickrGetToken.Location = new System.Drawing.Point(810, 58);
            this.btnFlickrGetToken.Margin = new System.Windows.Forms.Padding(4);
            this.btnFlickrGetToken.Name = "btnFlickrGetToken";
            this.btnFlickrGetToken.Size = new System.Drawing.Size(224, 30);
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
            this.btnFlickrGetFrob.Location = new System.Drawing.Point(810, 22);
            this.btnFlickrGetFrob.Margin = new System.Windows.Forms.Padding(4);
            this.btnFlickrGetFrob.Name = "btnFlickrGetFrob";
            this.btnFlickrGetFrob.Size = new System.Drawing.Size(224, 28);
            this.btnFlickrGetFrob.TabIndex = 0;
            this.btnFlickrGetFrob.Text = "Step 1. Authenticate ZScreen...";
            this.btnFlickrGetFrob.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.ttZScreen.SetToolTip(this.btnFlickrGetFrob, "Returns a frob to be used during authentication.");
            this.btnFlickrGetFrob.UseVisualStyleBackColor = true;
            this.btnFlickrGetFrob.Click += new System.EventHandler(this.btnFlickrGetFrob_Click);
            // 
            // tpTwitter
            // 
            this.tpTwitter.Controls.Add(this.ucTwitterAccounts);
            this.tpTwitter.Controls.Add(this.lblTwitterStatus);
            this.tpTwitter.Controls.Add(this.gbTwitterOthers);
            this.tpTwitter.Controls.Add(this.btnTwitterLogin);
            this.tpTwitter.Controls.Add(this.lblTwitterVerificationCode);
            this.tpTwitter.Controls.Add(this.tbTwitterVerificationCode);
            this.tpTwitter.Controls.Add(this.btnTwitterOpenAuthorizePage);
            this.tpTwitter.ImageKey = "(none)";
            this.tpTwitter.Location = new System.Drawing.Point(4, 25);
            this.tpTwitter.Margin = new System.Windows.Forms.Padding(4);
            this.tpTwitter.Name = "tpTwitter";
            this.tpTwitter.Padding = new System.Windows.Forms.Padding(4);
            this.tpTwitter.Size = new System.Drawing.Size(1059, 504);
            this.tpTwitter.TabIndex = 6;
            this.tpTwitter.Text = "Twitter";
            this.tpTwitter.UseVisualStyleBackColor = true;
            // 
            // lblTwitterStatus
            // 
            this.lblTwitterStatus.AutoSize = true;
            this.lblTwitterStatus.Location = new System.Drawing.Point(21, 98);
            this.lblTwitterStatus.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTwitterStatus.Name = "lblTwitterStatus";
            this.lblTwitterStatus.Size = new System.Drawing.Size(114, 17);
            this.lblTwitterStatus.TabIndex = 21;
            this.lblTwitterStatus.Text = "Login is required";
            // 
            // gbTwitterOthers
            // 
            this.gbTwitterOthers.Controls.Add(this.cbTwitPicShowFull);
            this.gbTwitterOthers.Controls.Add(this.cboTwitPicThumbnailMode);
            this.gbTwitterOthers.Controls.Add(this.lblTwitPicThumbnailMode);
            this.gbTwitterOthers.Location = new System.Drawing.Point(21, 138);
            this.gbTwitterOthers.Margin = new System.Windows.Forms.Padding(4);
            this.gbTwitterOthers.Name = "gbTwitterOthers";
            this.gbTwitterOthers.Padding = new System.Windows.Forms.Padding(4);
            this.gbTwitterOthers.Size = new System.Drawing.Size(384, 108);
            this.gbTwitterOthers.TabIndex = 20;
            this.gbTwitterOthers.TabStop = false;
            this.gbTwitterOthers.Text = "Other Twitter services (yfrog, twitsnaps etc.)";
            // 
            // cbTwitPicShowFull
            // 
            this.cbTwitPicShowFull.AutoSize = true;
            this.cbTwitPicShowFull.Location = new System.Drawing.Point(21, 30);
            this.cbTwitPicShowFull.Margin = new System.Windows.Forms.Padding(4);
            this.cbTwitPicShowFull.Name = "cbTwitPicShowFull";
            this.cbTwitPicShowFull.Size = new System.Drawing.Size(118, 21);
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
            this.cboTwitPicThumbnailMode.Location = new System.Drawing.Point(149, 59);
            this.cboTwitPicThumbnailMode.Margin = new System.Windows.Forms.Padding(4);
            this.cboTwitPicThumbnailMode.Name = "cboTwitPicThumbnailMode";
            this.cboTwitPicThumbnailMode.Size = new System.Drawing.Size(191, 24);
            this.cboTwitPicThumbnailMode.TabIndex = 14;
            this.cboTwitPicThumbnailMode.SelectedIndexChanged += new System.EventHandler(this.cbTwitPicThumbnailMode_SelectedIndexChanged);
            // 
            // lblTwitPicThumbnailMode
            // 
            this.lblTwitPicThumbnailMode.AutoSize = true;
            this.lblTwitPicThumbnailMode.Location = new System.Drawing.Point(21, 65);
            this.lblTwitPicThumbnailMode.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTwitPicThumbnailMode.Name = "lblTwitPicThumbnailMode";
            this.lblTwitPicThumbnailMode.Size = new System.Drawing.Size(117, 17);
            this.lblTwitPicThumbnailMode.TabIndex = 15;
            this.lblTwitPicThumbnailMode.Text = "Thumbnail Mode:";
            // 
            // btnTwitterLogin
            // 
            this.btnTwitterLogin.Location = new System.Drawing.Point(427, 58);
            this.btnTwitterLogin.Margin = new System.Windows.Forms.Padding(4);
            this.btnTwitterLogin.Name = "btnTwitterLogin";
            this.btnTwitterLogin.Size = new System.Drawing.Size(100, 28);
            this.btnTwitterLogin.TabIndex = 19;
            this.btnTwitterLogin.Text = "Login";
            this.btnTwitterLogin.UseVisualStyleBackColor = true;
            // 
            // lblTwitterVerificationCode
            // 
            this.lblTwitterVerificationCode.AutoSize = true;
            this.lblTwitterVerificationCode.Location = new System.Drawing.Point(21, 63);
            this.lblTwitterVerificationCode.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTwitterVerificationCode.Name = "lblTwitterVerificationCode";
            this.lblTwitterVerificationCode.Size = new System.Drawing.Size(117, 17);
            this.lblTwitterVerificationCode.TabIndex = 18;
            this.lblTwitterVerificationCode.Text = "Verification code:";
            // 
            // tbTwitterVerificationCode
            // 
            this.tbTwitterVerificationCode.Location = new System.Drawing.Point(149, 59);
            this.tbTwitterVerificationCode.Margin = new System.Windows.Forms.Padding(4);
            this.tbTwitterVerificationCode.Name = "tbTwitterVerificationCode";
            this.tbTwitterVerificationCode.Size = new System.Drawing.Size(265, 22);
            this.tbTwitterVerificationCode.TabIndex = 17;
            // 
            // btnTwitterOpenAuthorizePage
            // 
            this.btnTwitterOpenAuthorizePage.Location = new System.Drawing.Point(21, 20);
            this.btnTwitterOpenAuthorizePage.Margin = new System.Windows.Forms.Padding(4);
            this.btnTwitterOpenAuthorizePage.Name = "btnTwitterOpenAuthorizePage";
            this.btnTwitterOpenAuthorizePage.Size = new System.Drawing.Size(181, 28);
            this.btnTwitterOpenAuthorizePage.TabIndex = 16;
            this.btnTwitterOpenAuthorizePage.Text = "Open authorize page";
            this.btnTwitterOpenAuthorizePage.UseVisualStyleBackColor = true;
            // 
            // tpImageBam
            // 
            this.tpImageBam.Controls.Add(this.gbImageBamGalleries);
            this.tpImageBam.Controls.Add(this.gbImageBamLinks);
            this.tpImageBam.Controls.Add(this.gbImageBamApiKeys);
            this.tpImageBam.Location = new System.Drawing.Point(4, 25);
            this.tpImageBam.Margin = new System.Windows.Forms.Padding(4);
            this.tpImageBam.Name = "tpImageBam";
            this.tpImageBam.Padding = new System.Windows.Forms.Padding(4);
            this.tpImageBam.Size = new System.Drawing.Size(1059, 504);
            this.tpImageBam.TabIndex = 7;
            this.tpImageBam.Text = "ImageBam";
            this.tpImageBam.UseVisualStyleBackColor = true;
            // 
            // gbImageBamGalleries
            // 
            this.gbImageBamGalleries.Controls.Add(this.lbImageBamGalleries);
            this.gbImageBamGalleries.Location = new System.Drawing.Point(11, 138);
            this.gbImageBamGalleries.Margin = new System.Windows.Forms.Padding(4);
            this.gbImageBamGalleries.Name = "gbImageBamGalleries";
            this.gbImageBamGalleries.Padding = new System.Windows.Forms.Padding(4);
            this.gbImageBamGalleries.Size = new System.Drawing.Size(640, 187);
            this.gbImageBamGalleries.TabIndex = 10;
            this.gbImageBamGalleries.TabStop = false;
            this.gbImageBamGalleries.Text = "Galleries";
            // 
            // lbImageBamGalleries
            // 
            this.lbImageBamGalleries.FormattingEnabled = true;
            this.lbImageBamGalleries.ItemHeight = 16;
            this.lbImageBamGalleries.Location = new System.Drawing.Point(21, 30);
            this.lbImageBamGalleries.Margin = new System.Windows.Forms.Padding(4);
            this.lbImageBamGalleries.Name = "lbImageBamGalleries";
            this.lbImageBamGalleries.Size = new System.Drawing.Size(585, 132);
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
            this.gbImageBamLinks.Location = new System.Drawing.Point(661, 10);
            this.gbImageBamLinks.Margin = new System.Windows.Forms.Padding(4);
            this.gbImageBamLinks.Name = "gbImageBamLinks";
            this.gbImageBamLinks.Padding = new System.Windows.Forms.Padding(4);
            this.gbImageBamLinks.Size = new System.Drawing.Size(275, 315);
            this.gbImageBamLinks.TabIndex = 9;
            this.gbImageBamLinks.TabStop = false;
            this.gbImageBamLinks.Text = "Tasks";
            // 
            // chkImageBamContentNSFW
            // 
            this.chkImageBamContentNSFW.AutoSize = true;
            this.chkImageBamContentNSFW.Location = new System.Drawing.Point(21, 187);
            this.chkImageBamContentNSFW.Margin = new System.Windows.Forms.Padding(4);
            this.chkImageBamContentNSFW.Name = "chkImageBamContentNSFW";
            this.chkImageBamContentNSFW.Size = new System.Drawing.Size(123, 21);
            this.chkImageBamContentNSFW.TabIndex = 10;
            this.chkImageBamContentNSFW.Text = "NSFW Content";
            this.ttZScreen.SetToolTip(this.chkImageBamContentNSFW, "If you are uploading NSFW (Not Safe for Work) content then tick this checkbox");
            this.chkImageBamContentNSFW.UseVisualStyleBackColor = true;
            this.chkImageBamContentNSFW.CheckedChanged += new System.EventHandler(this.chkImageBamContentNSFW_CheckedChanged);
            // 
            // btnImageBamRemoveGallery
            // 
            this.btnImageBamRemoveGallery.Location = new System.Drawing.Point(21, 148);
            this.btnImageBamRemoveGallery.Margin = new System.Windows.Forms.Padding(4);
            this.btnImageBamRemoveGallery.Name = "btnImageBamRemoveGallery";
            this.btnImageBamRemoveGallery.Size = new System.Drawing.Size(171, 28);
            this.btnImageBamRemoveGallery.TabIndex = 9;
            this.btnImageBamRemoveGallery.Text = "Remove &Gallery";
            this.btnImageBamRemoveGallery.UseVisualStyleBackColor = true;
            this.btnImageBamRemoveGallery.Click += new System.EventHandler(this.btnImageBamRemoveGallery_Click);
            // 
            // btnImageBamCreateGallery
            // 
            this.btnImageBamCreateGallery.Location = new System.Drawing.Point(21, 108);
            this.btnImageBamCreateGallery.Margin = new System.Windows.Forms.Padding(4);
            this.btnImageBamCreateGallery.Name = "btnImageBamCreateGallery";
            this.btnImageBamCreateGallery.Size = new System.Drawing.Size(171, 28);
            this.btnImageBamCreateGallery.TabIndex = 8;
            this.btnImageBamCreateGallery.Text = "Create &Gallery";
            this.btnImageBamCreateGallery.UseVisualStyleBackColor = true;
            this.btnImageBamCreateGallery.Click += new System.EventHandler(this.btnImageBamCreateGallery_Click);
            // 
            // btnImageBamRegister
            // 
            this.btnImageBamRegister.AutoSize = true;
            this.btnImageBamRegister.Location = new System.Drawing.Point(21, 30);
            this.btnImageBamRegister.Margin = new System.Windows.Forms.Padding(4);
            this.btnImageBamRegister.Name = "btnImageBamRegister";
            this.btnImageBamRegister.Size = new System.Drawing.Size(225, 33);
            this.btnImageBamRegister.TabIndex = 7;
            this.btnImageBamRegister.Text = "Register at ImageBam...";
            this.btnImageBamRegister.UseVisualStyleBackColor = true;
            this.btnImageBamRegister.Click += new System.EventHandler(this.btnImageBamRegister_Click);
            // 
            // btnImageBamApiKeysUrl
            // 
            this.btnImageBamApiKeysUrl.AutoSize = true;
            this.btnImageBamApiKeysUrl.Location = new System.Drawing.Point(21, 69);
            this.btnImageBamApiKeysUrl.Margin = new System.Windows.Forms.Padding(4);
            this.btnImageBamApiKeysUrl.Name = "btnImageBamApiKeysUrl";
            this.btnImageBamApiKeysUrl.Size = new System.Drawing.Size(201, 33);
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
            this.gbImageBamApiKeys.Location = new System.Drawing.Point(11, 10);
            this.gbImageBamApiKeys.Margin = new System.Windows.Forms.Padding(4);
            this.gbImageBamApiKeys.Name = "gbImageBamApiKeys";
            this.gbImageBamApiKeys.Padding = new System.Windows.Forms.Padding(4);
            this.gbImageBamApiKeys.Size = new System.Drawing.Size(640, 118);
            this.gbImageBamApiKeys.TabIndex = 8;
            this.gbImageBamApiKeys.TabStop = false;
            this.gbImageBamApiKeys.Text = "API-Keys";
            // 
            // lblImageBamSecret
            // 
            this.lblImageBamSecret.AutoSize = true;
            this.lblImageBamSecret.Location = new System.Drawing.Point(21, 69);
            this.lblImageBamSecret.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblImageBamSecret.Name = "lblImageBamSecret";
            this.lblImageBamSecret.Size = new System.Drawing.Size(53, 17);
            this.lblImageBamSecret.TabIndex = 5;
            this.lblImageBamSecret.Text = "Secret:";
            // 
            // txtImageBamSecret
            // 
            this.txtImageBamSecret.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtImageBamSecret.Location = new System.Drawing.Point(84, 64);
            this.txtImageBamSecret.Margin = new System.Windows.Forms.Padding(4);
            this.txtImageBamSecret.Name = "txtImageBamSecret";
            this.txtImageBamSecret.Size = new System.Drawing.Size(523, 22);
            this.txtImageBamSecret.TabIndex = 4;
            this.txtImageBamSecret.TextChanged += new System.EventHandler(this.txtImageBamSecret_TextChanged);
            // 
            // lblImageBamKey
            // 
            this.lblImageBamKey.AutoSize = true;
            this.lblImageBamKey.Location = new System.Drawing.Point(39, 32);
            this.lblImageBamKey.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblImageBamKey.Name = "lblImageBamKey";
            this.lblImageBamKey.Size = new System.Drawing.Size(36, 17);
            this.lblImageBamKey.TabIndex = 3;
            this.lblImageBamKey.Text = "Key:";
            // 
            // txtImageBamApiKey
            // 
            this.txtImageBamApiKey.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtImageBamApiKey.Location = new System.Drawing.Point(83, 27);
            this.txtImageBamApiKey.Margin = new System.Windows.Forms.Padding(4);
            this.txtImageBamApiKey.Name = "txtImageBamApiKey";
            this.txtImageBamApiKey.Size = new System.Drawing.Size(524, 22);
            this.txtImageBamApiKey.TabIndex = 2;
            this.txtImageBamApiKey.TextChanged += new System.EventHandler(this.txtImageBamApiKey_TextChanged);
            // 
            // tpMindTouch
            // 
            this.tpMindTouch.Controls.Add(this.gbMindTouchOptions);
            this.tpMindTouch.Controls.Add(this.ucMindTouchAccounts);
            this.tpMindTouch.Location = new System.Drawing.Point(4, 25);
            this.tpMindTouch.Margin = new System.Windows.Forms.Padding(4);
            this.tpMindTouch.Name = "tpMindTouch";
            this.tpMindTouch.Padding = new System.Windows.Forms.Padding(4);
            this.tpMindTouch.Size = new System.Drawing.Size(1059, 504);
            this.tpMindTouch.TabIndex = 4;
            this.tpMindTouch.Text = "MindTouch";
            this.tpMindTouch.UseVisualStyleBackColor = true;
            // 
            // gbMindTouchOptions
            // 
            this.gbMindTouchOptions.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gbMindTouchOptions.Controls.Add(this.chkDekiWikiForcePath);
            this.gbMindTouchOptions.Location = new System.Drawing.Point(21, 390);
            this.gbMindTouchOptions.Margin = new System.Windows.Forms.Padding(4);
            this.gbMindTouchOptions.Name = "gbMindTouchOptions";
            this.gbMindTouchOptions.Padding = new System.Windows.Forms.Padding(4);
            this.gbMindTouchOptions.Size = new System.Drawing.Size(1014, 89);
            this.gbMindTouchOptions.TabIndex = 116;
            this.gbMindTouchOptions.TabStop = false;
            this.gbMindTouchOptions.Text = "MindTouch Deki Wiki Settings";
            // 
            // chkDekiWikiForcePath
            // 
            this.chkDekiWikiForcePath.AutoSize = true;
            this.chkDekiWikiForcePath.BackColor = System.Drawing.Color.Transparent;
            this.chkDekiWikiForcePath.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.chkDekiWikiForcePath.Location = new System.Drawing.Point(21, 30);
            this.chkDekiWikiForcePath.Margin = new System.Windows.Forms.Padding(4);
            this.chkDekiWikiForcePath.Name = "chkDekiWikiForcePath";
            this.chkDekiWikiForcePath.Size = new System.Drawing.Size(386, 21);
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
            this.ucMindTouchAccounts.Location = new System.Drawing.Point(4, 4);
            this.ucMindTouchAccounts.Margin = new System.Windows.Forms.Padding(5);
            this.ucMindTouchAccounts.Name = "ucMindTouchAccounts";
            this.ucMindTouchAccounts.Size = new System.Drawing.Size(1050, 387);
            this.ucMindTouchAccounts.TabIndex = 0;
            // 
            // tpMediaWiki
            // 
            this.tpMediaWiki.Controls.Add(this.ucMediaWikiAccounts);
            this.tpMediaWiki.Location = new System.Drawing.Point(4, 25);
            this.tpMediaWiki.Margin = new System.Windows.Forms.Padding(4);
            this.tpMediaWiki.Name = "tpMediaWiki";
            this.tpMediaWiki.Padding = new System.Windows.Forms.Padding(4);
            this.tpMediaWiki.Size = new System.Drawing.Size(1059, 504);
            this.tpMediaWiki.TabIndex = 13;
            this.tpMediaWiki.Text = "MediaWiki";
            this.tpMediaWiki.UseVisualStyleBackColor = true;
            // 
            // ucMediaWikiAccounts
            // 
            this.ucMediaWikiAccounts.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.ucMediaWikiAccounts.Location = new System.Drawing.Point(4, 4);
            this.ucMediaWikiAccounts.Margin = new System.Windows.Forms.Padding(5);
            this.ucMediaWikiAccounts.Name = "ucMediaWikiAccounts";
            this.ucMediaWikiAccounts.Size = new System.Drawing.Size(1050, 486);
            this.ucMediaWikiAccounts.TabIndex = 0;
            // 
            // tpHotkeys
            // 
            this.tpHotkeys.Controls.Add(this.btnResetHotkeys);
            this.tpHotkeys.Controls.Add(this.lblHotkeyStatus);
            this.tpHotkeys.Controls.Add(this.dgvHotkeys);
            this.tpHotkeys.ImageKey = "keyboard.png";
            this.tpHotkeys.Location = new System.Drawing.Point(4, 25);
            this.tpHotkeys.Margin = new System.Windows.Forms.Padding(4);
            this.tpHotkeys.Name = "tpHotkeys";
            this.tpHotkeys.Padding = new System.Windows.Forms.Padding(4);
            this.tpHotkeys.Size = new System.Drawing.Size(1075, 541);
            this.tpHotkeys.TabIndex = 1;
            this.tpHotkeys.Text = "Hotkeys";
            this.tpHotkeys.UseVisualStyleBackColor = true;
            // 
            // btnResetHotkeys
            // 
            this.btnResetHotkeys.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnResetHotkeys.AutoSize = true;
            this.btnResetHotkeys.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnResetHotkeys.Location = new System.Drawing.Point(923, 20);
            this.btnResetHotkeys.Margin = new System.Windows.Forms.Padding(4);
            this.btnResetHotkeys.Name = "btnResetHotkeys";
            this.btnResetHotkeys.Size = new System.Drawing.Size(129, 27);
            this.btnResetHotkeys.TabIndex = 69;
            this.btnResetHotkeys.Text = "Reset &All Hotkeys";
            this.btnResetHotkeys.UseVisualStyleBackColor = true;
            this.btnResetHotkeys.Click += new System.EventHandler(this.btnResetHotkeys_Click);
            // 
            // lblHotkeyStatus
            // 
            this.lblHotkeyStatus.AutoSize = true;
            this.lblHotkeyStatus.BackColor = System.Drawing.Color.Transparent;
            this.lblHotkeyStatus.Location = new System.Drawing.Point(39, 30);
            this.lblHotkeyStatus.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblHotkeyStatus.Name = "lblHotkeyStatus";
            this.lblHotkeyStatus.Size = new System.Drawing.Size(156, 17);
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
            this.chHotkeys_Keys,
            this.DefaultKeys});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvHotkeys.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgvHotkeys.Location = new System.Drawing.Point(35, 62);
            this.dgvHotkeys.Margin = new System.Windows.Forms.Padding(4);
            this.dgvHotkeys.MultiSelect = false;
            this.dgvHotkeys.Name = "dgvHotkeys";
            this.dgvHotkeys.ReadOnly = true;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvHotkeys.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dgvHotkeys.RowHeadersVisible = false;
            this.dgvHotkeys.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgvHotkeys.RowTemplate.Height = 24;
            this.dgvHotkeys.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dgvHotkeys.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgvHotkeys.Size = new System.Drawing.Size(733, 464);
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
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.DefaultKeys.DefaultCellStyle = dataGridViewCellStyle2;
            this.DefaultKeys.HeaderText = "Default Hotkey";
            this.DefaultKeys.Name = "DefaultKeys";
            this.DefaultKeys.ReadOnly = true;
            this.DefaultKeys.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.DefaultKeys.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // tpScreenshots
            // 
            this.tpScreenshots.Controls.Add(this.tcScreenshots);
            this.tpScreenshots.ImageKey = "monitor.png";
            this.tpScreenshots.Location = new System.Drawing.Point(4, 25);
            this.tpScreenshots.Margin = new System.Windows.Forms.Padding(4);
            this.tpScreenshots.Name = "tpScreenshots";
            this.tpScreenshots.Padding = new System.Windows.Forms.Padding(4);
            this.tpScreenshots.Size = new System.Drawing.Size(1075, 541);
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
            this.tcScreenshots.ImageList = this.ilApp;
            this.tcScreenshots.Location = new System.Drawing.Point(4, 4);
            this.tcScreenshots.Margin = new System.Windows.Forms.Padding(4);
            this.tcScreenshots.Name = "tcScreenshots";
            this.tcScreenshots.SelectedIndex = 0;
            this.tcScreenshots.Size = new System.Drawing.Size(1068, 532);
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
            this.tpCropShot.Location = new System.Drawing.Point(4, 25);
            this.tpCropShot.Margin = new System.Windows.Forms.Padding(4);
            this.tpCropShot.Name = "tpCropShot";
            this.tpCropShot.Padding = new System.Windows.Forms.Padding(4);
            this.tpCropShot.Size = new System.Drawing.Size(1060, 503);
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
            this.gbDynamicRegionBorderColorSettings.Location = new System.Drawing.Point(491, 325);
            this.gbDynamicRegionBorderColorSettings.Margin = new System.Windows.Forms.Padding(4);
            this.gbDynamicRegionBorderColorSettings.Name = "gbDynamicRegionBorderColorSettings";
            this.gbDynamicRegionBorderColorSettings.Padding = new System.Windows.Forms.Padding(4);
            this.gbDynamicRegionBorderColorSettings.Size = new System.Drawing.Size(523, 138);
            this.gbDynamicRegionBorderColorSettings.TabIndex = 123;
            this.gbDynamicRegionBorderColorSettings.TabStop = false;
            this.gbDynamicRegionBorderColorSettings.Text = "Dynamic Region Border Color Settings";
            // 
            // nudCropRegionStep
            // 
            this.nudCropRegionStep.Location = new System.Drawing.Point(427, 54);
            this.nudCropRegionStep.Margin = new System.Windows.Forms.Padding(4);
            this.nudCropRegionStep.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudCropRegionStep.Name = "nudCropRegionStep";
            this.nudCropRegionStep.Size = new System.Drawing.Size(75, 22);
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
            this.nudCropHueRange.Location = new System.Drawing.Point(427, 94);
            this.nudCropHueRange.Margin = new System.Windows.Forms.Padding(4);
            this.nudCropHueRange.Maximum = new decimal(new int[] {
            360,
            0,
            0,
            0});
            this.nudCropHueRange.Name = "nudCropHueRange";
            this.nudCropHueRange.Size = new System.Drawing.Size(75, 22);
            this.nudCropHueRange.TabIndex = 33;
            this.nudCropHueRange.ValueChanged += new System.EventHandler(this.nudCropHueRange_ValueChanged);
            // 
            // cbCropDynamicBorderColor
            // 
            this.cbCropDynamicBorderColor.AutoSize = true;
            this.cbCropDynamicBorderColor.Location = new System.Drawing.Point(21, 30);
            this.cbCropDynamicBorderColor.Margin = new System.Windows.Forms.Padding(4);
            this.cbCropDynamicBorderColor.Name = "cbCropDynamicBorderColor";
            this.cbCropDynamicBorderColor.Size = new System.Drawing.Size(82, 21);
            this.cbCropDynamicBorderColor.TabIndex = 27;
            this.cbCropDynamicBorderColor.Text = "Enabled";
            this.cbCropDynamicBorderColor.UseVisualStyleBackColor = true;
            this.cbCropDynamicBorderColor.CheckedChanged += new System.EventHandler(this.cbCropDynamicBorderColor_CheckedChanged);
            // 
            // lblCropRegionInterval
            // 
            this.lblCropRegionInterval.AutoSize = true;
            this.lblCropRegionInterval.Location = new System.Drawing.Point(235, 59);
            this.lblCropRegionInterval.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCropRegionInterval.Name = "lblCropRegionInterval";
            this.lblCropRegionInterval.Size = new System.Drawing.Size(58, 17);
            this.lblCropRegionInterval.TabIndex = 28;
            this.lblCropRegionInterval.Text = "Interval:";
            // 
            // lblCropHueRange
            // 
            this.lblCropHueRange.AutoSize = true;
            this.lblCropHueRange.Location = new System.Drawing.Point(341, 98);
            this.lblCropHueRange.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCropHueRange.Name = "lblCropHueRange";
            this.lblCropHueRange.Size = new System.Drawing.Size(79, 17);
            this.lblCropHueRange.TabIndex = 32;
            this.lblCropHueRange.Text = "Hue range:";
            // 
            // lblCropRegionStep
            // 
            this.lblCropRegionStep.AutoSize = true;
            this.lblCropRegionStep.Location = new System.Drawing.Point(381, 59);
            this.lblCropRegionStep.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCropRegionStep.Name = "lblCropRegionStep";
            this.lblCropRegionStep.Size = new System.Drawing.Size(41, 17);
            this.lblCropRegionStep.TabIndex = 29;
            this.lblCropRegionStep.Text = "Step:";
            // 
            // nudCropRegionInterval
            // 
            this.nudCropRegionInterval.Location = new System.Drawing.Point(299, 54);
            this.nudCropRegionInterval.Margin = new System.Windows.Forms.Padding(4);
            this.nudCropRegionInterval.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.nudCropRegionInterval.Name = "nudCropRegionInterval";
            this.nudCropRegionInterval.Size = new System.Drawing.Size(75, 22);
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
            this.gbDynamicCrosshair.Location = new System.Drawing.Point(11, 325);
            this.gbDynamicCrosshair.Margin = new System.Windows.Forms.Padding(4);
            this.gbDynamicCrosshair.Name = "gbDynamicCrosshair";
            this.gbDynamicCrosshair.Padding = new System.Windows.Forms.Padding(4);
            this.gbDynamicCrosshair.Size = new System.Drawing.Size(469, 138);
            this.gbDynamicCrosshair.TabIndex = 122;
            this.gbDynamicCrosshair.TabStop = false;
            this.gbDynamicCrosshair.Text = "Dynamic Crosshair Settings";
            // 
            // chkCropDynamicCrosshair
            // 
            this.chkCropDynamicCrosshair.AutoSize = true;
            this.chkCropDynamicCrosshair.Location = new System.Drawing.Point(21, 30);
            this.chkCropDynamicCrosshair.Margin = new System.Windows.Forms.Padding(4);
            this.chkCropDynamicCrosshair.Name = "chkCropDynamicCrosshair";
            this.chkCropDynamicCrosshair.Size = new System.Drawing.Size(82, 21);
            this.chkCropDynamicCrosshair.TabIndex = 16;
            this.chkCropDynamicCrosshair.Text = "Enabled";
            this.chkCropDynamicCrosshair.UseVisualStyleBackColor = true;
            this.chkCropDynamicCrosshair.CheckedChanged += new System.EventHandler(this.cbCropDynamicCrosshair_CheckedChanged);
            // 
            // lblCropCrosshairStep
            // 
            this.lblCropCrosshairStep.AutoSize = true;
            this.lblCropCrosshairStep.Location = new System.Drawing.Point(331, 64);
            this.lblCropCrosshairStep.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCropCrosshairStep.Name = "lblCropCrosshairStep";
            this.lblCropCrosshairStep.Size = new System.Drawing.Size(41, 17);
            this.lblCropCrosshairStep.TabIndex = 22;
            this.lblCropCrosshairStep.Text = "Step:";
            // 
            // lblCropCrosshairInterval
            // 
            this.lblCropCrosshairInterval.AutoSize = true;
            this.lblCropCrosshairInterval.Location = new System.Drawing.Point(181, 64);
            this.lblCropCrosshairInterval.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCropCrosshairInterval.Name = "lblCropCrosshairInterval";
            this.lblCropCrosshairInterval.Size = new System.Drawing.Size(58, 17);
            this.lblCropCrosshairInterval.TabIndex = 21;
            this.lblCropCrosshairInterval.Text = "Interval:";
            // 
            // nudCropCrosshairInterval
            // 
            this.nudCropCrosshairInterval.Location = new System.Drawing.Point(245, 59);
            this.nudCropCrosshairInterval.Margin = new System.Windows.Forms.Padding(4);
            this.nudCropCrosshairInterval.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.nudCropCrosshairInterval.Name = "nudCropCrosshairInterval";
            this.nudCropCrosshairInterval.Size = new System.Drawing.Size(75, 22);
            this.nudCropCrosshairInterval.TabIndex = 23;
            this.nudCropCrosshairInterval.ValueChanged += new System.EventHandler(this.nudCropInterval_ValueChanged);
            // 
            // nudCropCrosshairStep
            // 
            this.nudCropCrosshairStep.Location = new System.Drawing.Point(373, 59);
            this.nudCropCrosshairStep.Margin = new System.Windows.Forms.Padding(4);
            this.nudCropCrosshairStep.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudCropCrosshairStep.Name = "nudCropCrosshairStep";
            this.nudCropCrosshairStep.Size = new System.Drawing.Size(75, 22);
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
            this.gpCropRegion.Location = new System.Drawing.Point(11, 20);
            this.gpCropRegion.Margin = new System.Windows.Forms.Padding(4);
            this.gpCropRegion.Name = "gpCropRegion";
            this.gpCropRegion.Padding = new System.Windows.Forms.Padding(4);
            this.gpCropRegion.Size = new System.Drawing.Size(469, 148);
            this.gpCropRegion.TabIndex = 121;
            this.gpCropRegion.TabStop = false;
            this.gpCropRegion.Text = "Crop Region Settings";
            // 
            // lblCropRegionStyle
            // 
            this.lblCropRegionStyle.AutoSize = true;
            this.lblCropRegionStyle.Location = new System.Drawing.Point(21, 34);
            this.lblCropRegionStyle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCropRegionStyle.Name = "lblCropRegionStyle";
            this.lblCropRegionStyle.Size = new System.Drawing.Size(119, 17);
            this.lblCropRegionStyle.TabIndex = 9;
            this.lblCropRegionStyle.Text = "Crop region style:";
            // 
            // chkRegionHotkeyInfo
            // 
            this.chkRegionHotkeyInfo.AutoSize = true;
            this.chkRegionHotkeyInfo.Location = new System.Drawing.Point(21, 108);
            this.chkRegionHotkeyInfo.Margin = new System.Windows.Forms.Padding(4);
            this.chkRegionHotkeyInfo.Name = "chkRegionHotkeyInfo";
            this.chkRegionHotkeyInfo.Size = new System.Drawing.Size(262, 21);
            this.chkRegionHotkeyInfo.TabIndex = 6;
            this.chkRegionHotkeyInfo.Text = "Show crop region hotkey instructions";
            this.chkRegionHotkeyInfo.UseVisualStyleBackColor = true;
            this.chkRegionHotkeyInfo.CheckedChanged += new System.EventHandler(this.cbRegionHotkeyInfo_CheckedChanged);
            // 
            // chkCropStyle
            // 
            this.chkCropStyle.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.chkCropStyle.FormattingEnabled = true;
            this.chkCropStyle.Location = new System.Drawing.Point(160, 30);
            this.chkCropStyle.Margin = new System.Windows.Forms.Padding(4);
            this.chkCropStyle.Name = "chkCropStyle";
            this.chkCropStyle.Size = new System.Drawing.Size(287, 24);
            this.chkCropStyle.TabIndex = 8;
            this.chkCropStyle.SelectedIndexChanged += new System.EventHandler(this.cbCropStyle_SelectedIndexChanged);
            // 
            // chkRegionRectangleInfo
            // 
            this.chkRegionRectangleInfo.AutoSize = true;
            this.chkRegionRectangleInfo.Location = new System.Drawing.Point(21, 79);
            this.chkRegionRectangleInfo.Margin = new System.Windows.Forms.Padding(4);
            this.chkRegionRectangleInfo.Name = "chkRegionRectangleInfo";
            this.chkRegionRectangleInfo.Size = new System.Drawing.Size(275, 21);
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
            this.gbCropRegionSettings.Location = new System.Drawing.Point(491, 177);
            this.gbCropRegionSettings.Margin = new System.Windows.Forms.Padding(4);
            this.gbCropRegionSettings.Name = "gbCropRegionSettings";
            this.gbCropRegionSettings.Padding = new System.Windows.Forms.Padding(4);
            this.gbCropRegionSettings.Size = new System.Drawing.Size(523, 138);
            this.gbCropRegionSettings.TabIndex = 27;
            this.gbCropRegionSettings.TabStop = false;
            this.gbCropRegionSettings.Text = "Region Settings";
            // 
            // lblCropBorderSize
            // 
            this.lblCropBorderSize.AutoSize = true;
            this.lblCropBorderSize.Location = new System.Drawing.Point(331, 34);
            this.lblCropBorderSize.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCropBorderSize.Name = "lblCropBorderSize";
            this.lblCropBorderSize.Size = new System.Drawing.Size(84, 17);
            this.lblCropBorderSize.TabIndex = 11;
            this.lblCropBorderSize.Text = "Border size:";
            // 
            // cbShowCropRuler
            // 
            this.cbShowCropRuler.AutoSize = true;
            this.cbShowCropRuler.Location = new System.Drawing.Point(21, 30);
            this.cbShowCropRuler.Margin = new System.Windows.Forms.Padding(4);
            this.cbShowCropRuler.Name = "cbShowCropRuler";
            this.cbShowCropRuler.Size = new System.Drawing.Size(97, 21);
            this.cbShowCropRuler.TabIndex = 26;
            this.cbShowCropRuler.Text = "Show ruler";
            this.cbShowCropRuler.UseVisualStyleBackColor = true;
            this.cbShowCropRuler.CheckedChanged += new System.EventHandler(this.cbShowCropRuler_CheckedChanged);
            // 
            // cbCropShowGrids
            // 
            this.cbCropShowGrids.AutoSize = true;
            this.cbCropShowGrids.Location = new System.Drawing.Point(21, 59);
            this.cbCropShowGrids.Margin = new System.Windows.Forms.Padding(4);
            this.cbCropShowGrids.Name = "cbCropShowGrids";
            this.cbCropShowGrids.Size = new System.Drawing.Size(270, 21);
            this.cbCropShowGrids.TabIndex = 13;
            this.cbCropShowGrids.Text = "Show grid when possible in Grid Mode";
            this.cbCropShowGrids.UseVisualStyleBackColor = true;
            this.cbCropShowGrids.CheckedChanged += new System.EventHandler(this.cbCropShowGrids_CheckedChanged);
            // 
            // lblCropBorderColor
            // 
            this.lblCropBorderColor.AutoSize = true;
            this.lblCropBorderColor.Location = new System.Drawing.Point(331, 69);
            this.lblCropBorderColor.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCropBorderColor.Name = "lblCropBorderColor";
            this.lblCropBorderColor.Size = new System.Drawing.Size(90, 17);
            this.lblCropBorderColor.TabIndex = 10;
            this.lblCropBorderColor.Text = "Border color:";
            // 
            // pbCropBorderColor
            // 
            this.pbCropBorderColor.BackColor = System.Drawing.Color.White;
            this.pbCropBorderColor.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pbCropBorderColor.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pbCropBorderColor.Location = new System.Drawing.Point(427, 64);
            this.pbCropBorderColor.Margin = new System.Windows.Forms.Padding(4);
            this.pbCropBorderColor.Name = "pbCropBorderColor";
            this.pbCropBorderColor.Size = new System.Drawing.Size(73, 24);
            this.pbCropBorderColor.TabIndex = 9;
            this.pbCropBorderColor.TabStop = false;
            this.pbCropBorderColor.Click += new System.EventHandler(this.pbCropBorderColor_Click);
            // 
            // nudCropBorderSize
            // 
            this.nudCropBorderSize.Location = new System.Drawing.Point(427, 30);
            this.nudCropBorderSize.Margin = new System.Windows.Forms.Padding(4);
            this.nudCropBorderSize.Name = "nudCropBorderSize";
            this.nudCropBorderSize.Size = new System.Drawing.Size(75, 22);
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
            this.gbCrosshairSettings.Location = new System.Drawing.Point(11, 177);
            this.gbCrosshairSettings.Margin = new System.Windows.Forms.Padding(4);
            this.gbCrosshairSettings.Name = "gbCrosshairSettings";
            this.gbCrosshairSettings.Padding = new System.Windows.Forms.Padding(4);
            this.gbCrosshairSettings.Size = new System.Drawing.Size(469, 138);
            this.gbCrosshairSettings.TabIndex = 25;
            this.gbCrosshairSettings.TabStop = false;
            this.gbCrosshairSettings.Text = "Crosshair Settings";
            // 
            // chkCropShowMagnifyingGlass
            // 
            this.chkCropShowMagnifyingGlass.AutoSize = true;
            this.chkCropShowMagnifyingGlass.Location = new System.Drawing.Point(21, 59);
            this.chkCropShowMagnifyingGlass.Margin = new System.Windows.Forms.Padding(4);
            this.chkCropShowMagnifyingGlass.Name = "chkCropShowMagnifyingGlass";
            this.chkCropShowMagnifyingGlass.Size = new System.Drawing.Size(173, 21);
            this.chkCropShowMagnifyingGlass.TabIndex = 26;
            this.chkCropShowMagnifyingGlass.Text = "Show magnifying glass";
            this.chkCropShowMagnifyingGlass.UseVisualStyleBackColor = true;
            this.chkCropShowMagnifyingGlass.CheckedChanged += new System.EventHandler(this.cbCropShowMagnifyingGlass_CheckedChanged);
            // 
            // chkCropShowBigCross
            // 
            this.chkCropShowBigCross.AutoSize = true;
            this.chkCropShowBigCross.Location = new System.Drawing.Point(21, 30);
            this.chkCropShowBigCross.Margin = new System.Windows.Forms.Padding(4);
            this.chkCropShowBigCross.Name = "chkCropShowBigCross";
            this.chkCropShowBigCross.Size = new System.Drawing.Size(256, 21);
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
            this.pbCropCrosshairColor.Location = new System.Drawing.Point(373, 98);
            this.pbCropCrosshairColor.Margin = new System.Windows.Forms.Padding(4);
            this.pbCropCrosshairColor.Name = "pbCropCrosshairColor";
            this.pbCropCrosshairColor.Size = new System.Drawing.Size(73, 24);
            this.pbCropCrosshairColor.TabIndex = 14;
            this.pbCropCrosshairColor.TabStop = false;
            this.pbCropCrosshairColor.Click += new System.EventHandler(this.pbCropCrosshairColor_Click);
            // 
            // lblCropCrosshairColor
            // 
            this.lblCropCrosshairColor.AutoSize = true;
            this.lblCropCrosshairColor.Location = new System.Drawing.Point(320, 103);
            this.lblCropCrosshairColor.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCropCrosshairColor.Name = "lblCropCrosshairColor";
            this.lblCropCrosshairColor.Size = new System.Drawing.Size(45, 17);
            this.lblCropCrosshairColor.TabIndex = 15;
            this.lblCropCrosshairColor.Text = "Color:";
            // 
            // nudCrosshairLineCount
            // 
            this.nudCrosshairLineCount.Location = new System.Drawing.Point(373, 30);
            this.nudCrosshairLineCount.Margin = new System.Windows.Forms.Padding(4);
            this.nudCrosshairLineCount.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nudCrosshairLineCount.Name = "nudCrosshairLineCount";
            this.nudCrosshairLineCount.Size = new System.Drawing.Size(75, 22);
            this.nudCrosshairLineCount.TabIndex = 17;
            this.nudCrosshairLineCount.ValueChanged += new System.EventHandler(this.nudCrosshairLineCount_ValueChanged);
            // 
            // nudCrosshairLineSize
            // 
            this.nudCrosshairLineSize.Location = new System.Drawing.Point(373, 64);
            this.nudCrosshairLineSize.Margin = new System.Windows.Forms.Padding(4);
            this.nudCrosshairLineSize.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nudCrosshairLineSize.Name = "nudCrosshairLineSize";
            this.nudCrosshairLineSize.Size = new System.Drawing.Size(75, 22);
            this.nudCrosshairLineSize.TabIndex = 18;
            this.nudCrosshairLineSize.ValueChanged += new System.EventHandler(this.nudCrosshairLineSize_ValueChanged);
            // 
            // lblCrosshairLineSize
            // 
            this.lblCrosshairLineSize.AutoSize = true;
            this.lblCrosshairLineSize.Location = new System.Drawing.Point(299, 69);
            this.lblCrosshairLineSize.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCrosshairLineSize.Name = "lblCrosshairLineSize";
            this.lblCrosshairLineSize.Size = new System.Drawing.Size(68, 17);
            this.lblCrosshairLineSize.TabIndex = 20;
            this.lblCrosshairLineSize.Text = "Line size:";
            // 
            // lblCrosshairLineCount
            // 
            this.lblCrosshairLineCount.AutoSize = true;
            this.lblCrosshairLineCount.Location = new System.Drawing.Point(288, 34);
            this.lblCrosshairLineCount.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCrosshairLineCount.Name = "lblCrosshairLineCount";
            this.lblCrosshairLineCount.Size = new System.Drawing.Size(78, 17);
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
            this.gbGridMode.Location = new System.Drawing.Point(491, 20);
            this.gbGridMode.Margin = new System.Windows.Forms.Padding(4);
            this.gbGridMode.Name = "gbGridMode";
            this.gbGridMode.Padding = new System.Windows.Forms.Padding(4);
            this.gbGridMode.Size = new System.Drawing.Size(523, 148);
            this.gbGridMode.TabIndex = 120;
            this.gbGridMode.TabStop = false;
            this.gbGridMode.Tag = "With Grid Mode you can take screenshots of preset portions of the Screen";
            this.gbGridMode.Text = "Grid Mode Settings";
            // 
            // cboCropGridMode
            // 
            this.cboCropGridMode.AutoSize = true;
            this.cboCropGridMode.Location = new System.Drawing.Point(21, 30);
            this.cboCropGridMode.Margin = new System.Windows.Forms.Padding(4);
            this.cboCropGridMode.Name = "cboCropGridMode";
            this.cboCropGridMode.Size = new System.Drawing.Size(232, 21);
            this.cboCropGridMode.TabIndex = 119;
            this.cboCropGridMode.Text = "Activate Grid Mode in Crop Shot";
            this.cboCropGridMode.UseVisualStyleBackColor = true;
            this.cboCropGridMode.CheckedChanged += new System.EventHandler(this.cbCropGridMode_CheckedChanged);
            // 
            // nudCropGridHeight
            // 
            this.nudCropGridHeight.Location = new System.Drawing.Point(427, 79);
            this.nudCropGridHeight.Margin = new System.Windows.Forms.Padding(4);
            this.nudCropGridHeight.Maximum = new decimal(new int[] {
            5000,
            0,
            0,
            0});
            this.nudCropGridHeight.Name = "nudCropGridHeight";
            this.nudCropGridHeight.Size = new System.Drawing.Size(75, 22);
            this.nudCropGridHeight.TabIndex = 15;
            this.nudCropGridHeight.ValueChanged += new System.EventHandler(this.nudCropGridHeight_ValueChanged);
            // 
            // lblGridSizeWidth
            // 
            this.lblGridSizeWidth.AutoSize = true;
            this.lblGridSizeWidth.Location = new System.Drawing.Point(235, 84);
            this.lblGridSizeWidth.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblGridSizeWidth.Name = "lblGridSizeWidth";
            this.lblGridSizeWidth.Size = new System.Drawing.Size(44, 17);
            this.lblGridSizeWidth.TabIndex = 14;
            this.lblGridSizeWidth.Text = "Width";
            // 
            // lblGridSize
            // 
            this.lblGridSize.AutoSize = true;
            this.lblGridSize.Location = new System.Drawing.Point(64, 84);
            this.lblGridSize.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblGridSize.Name = "lblGridSize";
            this.lblGridSize.Size = new System.Drawing.Size(159, 17);
            this.lblGridSize.TabIndex = 118;
            this.lblGridSize.Text = "Grid Size ( 0 = Disable )";
            // 
            // lblGridSizeHeight
            // 
            this.lblGridSizeHeight.AutoSize = true;
            this.lblGridSizeHeight.Location = new System.Drawing.Point(373, 84);
            this.lblGridSizeHeight.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblGridSizeHeight.Name = "lblGridSizeHeight";
            this.lblGridSizeHeight.Size = new System.Drawing.Size(49, 17);
            this.lblGridSizeHeight.TabIndex = 16;
            this.lblGridSizeHeight.Text = "Height";
            // 
            // nudCropGridWidth
            // 
            this.nudCropGridWidth.Location = new System.Drawing.Point(288, 79);
            this.nudCropGridWidth.Margin = new System.Windows.Forms.Padding(4);
            this.nudCropGridWidth.Maximum = new decimal(new int[] {
            5000,
            0,
            0,
            0});
            this.nudCropGridWidth.Name = "nudCropGridWidth";
            this.nudCropGridWidth.Size = new System.Drawing.Size(75, 22);
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
            this.tpSelectedWindow.Location = new System.Drawing.Point(4, 25);
            this.tpSelectedWindow.Margin = new System.Windows.Forms.Padding(4);
            this.tpSelectedWindow.Name = "tpSelectedWindow";
            this.tpSelectedWindow.Size = new System.Drawing.Size(1060, 503);
            this.tpSelectedWindow.TabIndex = 6;
            this.tpSelectedWindow.Text = "Selected Window";
            this.tpSelectedWindow.UseVisualStyleBackColor = true;
            // 
            // chkSelectedWindowCaptureObjects
            // 
            this.chkSelectedWindowCaptureObjects.AutoSize = true;
            this.chkSelectedWindowCaptureObjects.Location = new System.Drawing.Point(21, 335);
            this.chkSelectedWindowCaptureObjects.Margin = new System.Windows.Forms.Padding(4);
            this.chkSelectedWindowCaptureObjects.Name = "chkSelectedWindowCaptureObjects";
            this.chkSelectedWindowCaptureObjects.Size = new System.Drawing.Size(299, 21);
            this.chkSelectedWindowCaptureObjects.TabIndex = 42;
            this.chkSelectedWindowCaptureObjects.Text = "Capture control objects within each window";
            this.chkSelectedWindowCaptureObjects.UseVisualStyleBackColor = true;
            this.chkSelectedWindowCaptureObjects.CheckedChanged += new System.EventHandler(this.cbSelectedWindowCaptureObjects_CheckedChanged);
            // 
            // nudSelectedWindowHueRange
            // 
            this.nudSelectedWindowHueRange.Location = new System.Drawing.Point(288, 286);
            this.nudSelectedWindowHueRange.Margin = new System.Windows.Forms.Padding(4);
            this.nudSelectedWindowHueRange.Maximum = new decimal(new int[] {
            360,
            0,
            0,
            0});
            this.nudSelectedWindowHueRange.Name = "nudSelectedWindowHueRange";
            this.nudSelectedWindowHueRange.Size = new System.Drawing.Size(75, 22);
            this.nudSelectedWindowHueRange.TabIndex = 40;
            this.nudSelectedWindowHueRange.ValueChanged += new System.EventHandler(this.nudSelectedWindowHueRange_ValueChanged);
            // 
            // lblSelectedWindowHueRange
            // 
            this.lblSelectedWindowHueRange.AutoSize = true;
            this.lblSelectedWindowHueRange.Location = new System.Drawing.Point(21, 290);
            this.lblSelectedWindowHueRange.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSelectedWindowHueRange.Name = "lblSelectedWindowHueRange";
            this.lblSelectedWindowHueRange.Size = new System.Drawing.Size(260, 17);
            this.lblSelectedWindowHueRange.TabIndex = 39;
            this.lblSelectedWindowHueRange.Text = "Dynamic region border color hue range:";
            // 
            // nudSelectedWindowRegionStep
            // 
            this.nudSelectedWindowRegionStep.Location = new System.Drawing.Point(217, 246);
            this.nudSelectedWindowRegionStep.Margin = new System.Windows.Forms.Padding(4);
            this.nudSelectedWindowRegionStep.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudSelectedWindowRegionStep.Name = "nudSelectedWindowRegionStep";
            this.nudSelectedWindowRegionStep.Size = new System.Drawing.Size(75, 22);
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
            this.nudSelectedWindowRegionInterval.Location = new System.Drawing.Point(85, 246);
            this.nudSelectedWindowRegionInterval.Margin = new System.Windows.Forms.Padding(4);
            this.nudSelectedWindowRegionInterval.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.nudSelectedWindowRegionInterval.Name = "nudSelectedWindowRegionInterval";
            this.nudSelectedWindowRegionInterval.Size = new System.Drawing.Size(75, 22);
            this.nudSelectedWindowRegionInterval.TabIndex = 37;
            this.nudSelectedWindowRegionInterval.ValueChanged += new System.EventHandler(this.nudSelectedWindowRegionInterval_ValueChanged);
            // 
            // lblSelectedWindowRegionStep
            // 
            this.lblSelectedWindowRegionStep.AutoSize = true;
            this.lblSelectedWindowRegionStep.Location = new System.Drawing.Point(171, 250);
            this.lblSelectedWindowRegionStep.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSelectedWindowRegionStep.Name = "lblSelectedWindowRegionStep";
            this.lblSelectedWindowRegionStep.Size = new System.Drawing.Size(41, 17);
            this.lblSelectedWindowRegionStep.TabIndex = 36;
            this.lblSelectedWindowRegionStep.Text = "Step:";
            // 
            // lblSelectedWindowRegionInterval
            // 
            this.lblSelectedWindowRegionInterval.AutoSize = true;
            this.lblSelectedWindowRegionInterval.Location = new System.Drawing.Point(21, 250);
            this.lblSelectedWindowRegionInterval.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSelectedWindowRegionInterval.Name = "lblSelectedWindowRegionInterval";
            this.lblSelectedWindowRegionInterval.Size = new System.Drawing.Size(58, 17);
            this.lblSelectedWindowRegionInterval.TabIndex = 35;
            this.lblSelectedWindowRegionInterval.Text = "Interval:";
            // 
            // cbSelectedWindowDynamicBorderColor
            // 
            this.cbSelectedWindowDynamicBorderColor.AutoSize = true;
            this.cbSelectedWindowDynamicBorderColor.Location = new System.Drawing.Point(21, 207);
            this.cbSelectedWindowDynamicBorderColor.Margin = new System.Windows.Forms.Padding(4);
            this.cbSelectedWindowDynamicBorderColor.Name = "cbSelectedWindowDynamicBorderColor";
            this.cbSelectedWindowDynamicBorderColor.Size = new System.Drawing.Size(209, 21);
            this.cbSelectedWindowDynamicBorderColor.TabIndex = 34;
            this.cbSelectedWindowDynamicBorderColor.Text = "Dynamic region border color";
            this.cbSelectedWindowDynamicBorderColor.UseVisualStyleBackColor = true;
            this.cbSelectedWindowDynamicBorderColor.CheckedChanged += new System.EventHandler(this.cbSelectedWindowDynamicBorderColor_CheckedChanged);
            // 
            // cbSelectedWindowRuler
            // 
            this.cbSelectedWindowRuler.AutoSize = true;
            this.cbSelectedWindowRuler.Location = new System.Drawing.Point(21, 89);
            this.cbSelectedWindowRuler.Margin = new System.Windows.Forms.Padding(4);
            this.cbSelectedWindowRuler.Name = "cbSelectedWindowRuler";
            this.cbSelectedWindowRuler.Size = new System.Drawing.Size(97, 21);
            this.cbSelectedWindowRuler.TabIndex = 12;
            this.cbSelectedWindowRuler.Text = "Show ruler";
            this.cbSelectedWindowRuler.UseVisualStyleBackColor = true;
            this.cbSelectedWindowRuler.CheckedChanged += new System.EventHandler(this.cbSelectedWindowRuler_CheckedChanged);
            // 
            // lblSelectedWindowRegionStyle
            // 
            this.lblSelectedWindowRegionStyle.AutoSize = true;
            this.lblSelectedWindowRegionStyle.Location = new System.Drawing.Point(21, 25);
            this.lblSelectedWindowRegionStyle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSelectedWindowRegionStyle.Name = "lblSelectedWindowRegionStyle";
            this.lblSelectedWindowRegionStyle.Size = new System.Drawing.Size(193, 17);
            this.lblSelectedWindowRegionStyle.TabIndex = 11;
            this.lblSelectedWindowRegionStyle.Text = "Selected window region style:";
            // 
            // cbSelectedWindowStyle
            // 
            this.cbSelectedWindowStyle.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbSelectedWindowStyle.FormattingEnabled = true;
            this.cbSelectedWindowStyle.Location = new System.Drawing.Point(224, 20);
            this.cbSelectedWindowStyle.Margin = new System.Windows.Forms.Padding(4);
            this.cbSelectedWindowStyle.Name = "cbSelectedWindowStyle";
            this.cbSelectedWindowStyle.Size = new System.Drawing.Size(276, 24);
            this.cbSelectedWindowStyle.TabIndex = 10;
            this.cbSelectedWindowStyle.SelectedIndexChanged += new System.EventHandler(this.cbSelectedWindowStyle_SelectedIndexChanged);
            // 
            // cbSelectedWindowRectangleInfo
            // 
            this.cbSelectedWindowRectangleInfo.AutoSize = true;
            this.cbSelectedWindowRectangleInfo.Location = new System.Drawing.Point(21, 59);
            this.cbSelectedWindowRectangleInfo.Margin = new System.Windows.Forms.Padding(4);
            this.cbSelectedWindowRectangleInfo.Name = "cbSelectedWindowRectangleInfo";
            this.cbSelectedWindowRectangleInfo.Size = new System.Drawing.Size(349, 21);
            this.cbSelectedWindowRectangleInfo.TabIndex = 5;
            this.cbSelectedWindowRectangleInfo.Text = "Show selected window region coordinates and size";
            this.cbSelectedWindowRectangleInfo.UseVisualStyleBackColor = true;
            this.cbSelectedWindowRectangleInfo.CheckedChanged += new System.EventHandler(this.cbSelectedWindowRectangleInfo_CheckedChanged);
            // 
            // lblSelectedWindowBorderColor
            // 
            this.lblSelectedWindowBorderColor.AutoSize = true;
            this.lblSelectedWindowBorderColor.Location = new System.Drawing.Point(21, 128);
            this.lblSelectedWindowBorderColor.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSelectedWindowBorderColor.Name = "lblSelectedWindowBorderColor";
            this.lblSelectedWindowBorderColor.Size = new System.Drawing.Size(138, 17);
            this.lblSelectedWindowBorderColor.TabIndex = 1;
            this.lblSelectedWindowBorderColor.Text = "Region border color:";
            // 
            // nudSelectedWindowBorderSize
            // 
            this.nudSelectedWindowBorderSize.Location = new System.Drawing.Point(267, 167);
            this.nudSelectedWindowBorderSize.Margin = new System.Windows.Forms.Padding(4);
            this.nudSelectedWindowBorderSize.Name = "nudSelectedWindowBorderSize";
            this.nudSelectedWindowBorderSize.Size = new System.Drawing.Size(75, 22);
            this.nudSelectedWindowBorderSize.TabIndex = 4;
            this.nudSelectedWindowBorderSize.ValueChanged += new System.EventHandler(this.nudSelectedWindowBorderSize_ValueChanged);
            // 
            // lblSelectedWindowBorderSize
            // 
            this.lblSelectedWindowBorderSize.AutoSize = true;
            this.lblSelectedWindowBorderSize.Location = new System.Drawing.Point(21, 171);
            this.lblSelectedWindowBorderSize.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSelectedWindowBorderSize.Name = "lblSelectedWindowBorderSize";
            this.lblSelectedWindowBorderSize.Size = new System.Drawing.Size(238, 17);
            this.lblSelectedWindowBorderSize.TabIndex = 2;
            this.lblSelectedWindowBorderSize.Text = "Region border size ( 0 = No border )";
            // 
            // pbSelectedWindowBorderColor
            // 
            this.pbSelectedWindowBorderColor.BackColor = System.Drawing.Color.White;
            this.pbSelectedWindowBorderColor.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pbSelectedWindowBorderColor.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pbSelectedWindowBorderColor.Location = new System.Drawing.Point(171, 124);
            this.pbSelectedWindowBorderColor.Margin = new System.Windows.Forms.Padding(4);
            this.pbSelectedWindowBorderColor.Name = "pbSelectedWindowBorderColor";
            this.pbSelectedWindowBorderColor.Size = new System.Drawing.Size(73, 24);
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
            this.tpActivewindow.Location = new System.Drawing.Point(4, 25);
            this.tpActivewindow.Margin = new System.Windows.Forms.Padding(4);
            this.tpActivewindow.Name = "tpActivewindow";
            this.tpActivewindow.Padding = new System.Windows.Forms.Padding(4);
            this.tpActivewindow.Size = new System.Drawing.Size(1060, 503);
            this.tpActivewindow.TabIndex = 12;
            this.tpActivewindow.Text = "Active Window";
            this.tpActivewindow.UseVisualStyleBackColor = true;
            // 
            // chkActiveWindowTryCaptureChildren
            // 
            this.chkActiveWindowTryCaptureChildren.AutoSize = true;
            this.chkActiveWindowTryCaptureChildren.Location = new System.Drawing.Point(21, 192);
            this.chkActiveWindowTryCaptureChildren.Margin = new System.Windows.Forms.Padding(4);
            this.chkActiveWindowTryCaptureChildren.Name = "chkActiveWindowTryCaptureChildren";
            this.chkActiveWindowTryCaptureChildren.Size = new System.Drawing.Size(307, 21);
            this.chkActiveWindowTryCaptureChildren.TabIndex = 48;
            this.chkActiveWindowTryCaptureChildren.Text = "Capture Child Windows, Tooltips and Menus";
            this.ttZScreen.SetToolTip(this.chkActiveWindowTryCaptureChildren, "Only works when DWM is disabled");
            this.chkActiveWindowTryCaptureChildren.UseVisualStyleBackColor = true;
            this.chkActiveWindowTryCaptureChildren.CheckedChanged += new System.EventHandler(this.chkActiveWindowTryCaptureChilds_CheckedChanged);
            // 
            // cbActiveWindowGDIFreezeWindow
            // 
            this.cbActiveWindowGDIFreezeWindow.AutoSize = true;
            this.cbActiveWindowGDIFreezeWindow.Location = new System.Drawing.Point(21, 164);
            this.cbActiveWindowGDIFreezeWindow.Margin = new System.Windows.Forms.Padding(4);
            this.cbActiveWindowGDIFreezeWindow.Name = "cbActiveWindowGDIFreezeWindow";
            this.cbActiveWindowGDIFreezeWindow.Size = new System.Drawing.Size(219, 21);
            this.cbActiveWindowGDIFreezeWindow.TabIndex = 49;
            this.cbActiveWindowGDIFreezeWindow.Text = "Freeze window during capture";
            this.ttZScreen.SetToolTip(this.cbActiveWindowGDIFreezeWindow, "Avoids artifacts with moving images");
            this.cbActiveWindowGDIFreezeWindow.UseVisualStyleBackColor = true;
            this.cbActiveWindowGDIFreezeWindow.CheckedChanged += new System.EventHandler(this.chkActiveWindowGDIFreezeWindow_CheckedChanged);
            // 
            // chkSelectedWindowCleanTransparentCorners
            // 
            this.chkSelectedWindowCleanTransparentCorners.AutoSize = true;
            this.chkSelectedWindowCleanTransparentCorners.Location = new System.Drawing.Point(21, 135);
            this.chkSelectedWindowCleanTransparentCorners.Margin = new System.Windows.Forms.Padding(4);
            this.chkSelectedWindowCleanTransparentCorners.Name = "chkSelectedWindowCleanTransparentCorners";
            this.chkSelectedWindowCleanTransparentCorners.Size = new System.Drawing.Size(195, 21);
            this.chkSelectedWindowCleanTransparentCorners.TabIndex = 44;
            this.chkSelectedWindowCleanTransparentCorners.Text = "Clean transparent corners";
            this.ttZScreen.SetToolTip(this.chkSelectedWindowCleanTransparentCorners, "Remove the background behind the transparent window corners");
            this.chkSelectedWindowCleanTransparentCorners.UseVisualStyleBackColor = true;
            this.chkSelectedWindowCleanTransparentCorners.CheckedChanged += new System.EventHandler(this.cbSelectedWindowCleanTransparentCorners_CheckedChanged);
            // 
            // chkSelectedWindowShowCheckers
            // 
            this.chkSelectedWindowShowCheckers.AutoSize = true;
            this.chkSelectedWindowShowCheckers.Location = new System.Drawing.Point(21, 49);
            this.chkSelectedWindowShowCheckers.Margin = new System.Windows.Forms.Padding(4);
            this.chkSelectedWindowShowCheckers.Name = "chkSelectedWindowShowCheckers";
            this.chkSelectedWindowShowCheckers.Size = new System.Drawing.Size(317, 21);
            this.chkSelectedWindowShowCheckers.TabIndex = 46;
            this.chkSelectedWindowShowCheckers.Text = "Show checkerboard pattern behind the image";
            this.ttZScreen.SetToolTip(this.chkSelectedWindowShowCheckers, "Useful to visualize transparency");
            this.chkSelectedWindowShowCheckers.UseVisualStyleBackColor = true;
            this.chkSelectedWindowShowCheckers.CheckedChanged += new System.EventHandler(this.cbSelectedWindowShowCheckers_CheckedChanged);
            // 
            // chkSelectedWindowIncludeShadow
            // 
            this.chkSelectedWindowIncludeShadow.AutoSize = true;
            this.chkSelectedWindowIncludeShadow.Location = new System.Drawing.Point(21, 107);
            this.chkSelectedWindowIncludeShadow.Margin = new System.Windows.Forms.Padding(4);
            this.chkSelectedWindowIncludeShadow.Name = "chkSelectedWindowIncludeShadow";
            this.chkSelectedWindowIncludeShadow.Size = new System.Drawing.Size(166, 21);
            this.chkSelectedWindowIncludeShadow.TabIndex = 45;
            this.chkSelectedWindowIncludeShadow.Text = "Include shadow effect";
            this.ttZScreen.SetToolTip(this.chkSelectedWindowIncludeShadow, "Captures the real window shadow (GDI on Vista & 7), or fake it (DWM, XP)");
            this.chkSelectedWindowIncludeShadow.UseVisualStyleBackColor = true;
            this.chkSelectedWindowIncludeShadow.CheckedChanged += new System.EventHandler(this.cbSelectedWindowIncludeShadow_CheckedChanged);
            // 
            // chkActiveWindowPreferDWM
            // 
            this.chkActiveWindowPreferDWM.AutoSize = true;
            this.chkActiveWindowPreferDWM.Location = new System.Drawing.Point(21, 79);
            this.chkActiveWindowPreferDWM.Margin = new System.Windows.Forms.Padding(4);
            this.chkActiveWindowPreferDWM.Name = "chkActiveWindowPreferDWM";
            this.chkActiveWindowPreferDWM.Size = new System.Drawing.Size(238, 21);
            this.chkActiveWindowPreferDWM.TabIndex = 49;
            this.chkActiveWindowPreferDWM.Text = "Prefer Desktop Window Manager";
            this.ttZScreen.SetToolTip(this.chkActiveWindowPreferDWM, "Make use of DWM to capture the window");
            this.chkActiveWindowPreferDWM.UseVisualStyleBackColor = true;
            this.chkActiveWindowPreferDWM.CheckedChanged += new System.EventHandler(this.chkActiveWindowPreferDWM_CheckedChanged);
            // 
            // chkSelectedWindowCleanBackground
            // 
            this.chkSelectedWindowCleanBackground.AutoSize = true;
            this.chkSelectedWindowCleanBackground.Location = new System.Drawing.Point(21, 20);
            this.chkSelectedWindowCleanBackground.Margin = new System.Windows.Forms.Padding(4);
            this.chkSelectedWindowCleanBackground.Name = "chkSelectedWindowCleanBackground";
            this.chkSelectedWindowCleanBackground.Size = new System.Drawing.Size(142, 21);
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
            this.tpFreehandCropShot.Location = new System.Drawing.Point(4, 25);
            this.tpFreehandCropShot.Margin = new System.Windows.Forms.Padding(4);
            this.tpFreehandCropShot.Name = "tpFreehandCropShot";
            this.tpFreehandCropShot.Size = new System.Drawing.Size(1060, 503);
            this.tpFreehandCropShot.TabIndex = 13;
            this.tpFreehandCropShot.Text = "Freehand Crop Shot";
            this.tpFreehandCropShot.UseVisualStyleBackColor = true;
            // 
            // cbFreehandCropShowRectangleBorder
            // 
            this.cbFreehandCropShowRectangleBorder.AutoSize = true;
            this.cbFreehandCropShowRectangleBorder.Location = new System.Drawing.Point(21, 108);
            this.cbFreehandCropShowRectangleBorder.Margin = new System.Windows.Forms.Padding(4);
            this.cbFreehandCropShowRectangleBorder.Name = "cbFreehandCropShowRectangleBorder";
            this.cbFreehandCropShowRectangleBorder.Size = new System.Drawing.Size(304, 21);
            this.cbFreehandCropShowRectangleBorder.TabIndex = 3;
            this.cbFreehandCropShowRectangleBorder.Text = "Show rectangle border and size information";
            this.cbFreehandCropShowRectangleBorder.UseVisualStyleBackColor = true;
            this.cbFreehandCropShowRectangleBorder.CheckedChanged += new System.EventHandler(this.cbFreehandCropShowRectangleBorder_CheckedChanged);
            // 
            // cbFreehandCropAutoClose
            // 
            this.cbFreehandCropAutoClose.AutoSize = true;
            this.cbFreehandCropAutoClose.Location = new System.Drawing.Point(21, 79);
            this.cbFreehandCropAutoClose.Margin = new System.Windows.Forms.Padding(4);
            this.cbFreehandCropAutoClose.Name = "cbFreehandCropAutoClose";
            this.cbFreehandCropAutoClose.Size = new System.Drawing.Size(442, 21);
            this.cbFreehandCropAutoClose.TabIndex = 2;
            this.cbFreehandCropAutoClose.Text = "Use right click to cancel upload instead of cleaning drawn regions";
            this.cbFreehandCropAutoClose.UseVisualStyleBackColor = true;
            this.cbFreehandCropAutoClose.CheckedChanged += new System.EventHandler(this.cbFreehandCropAutoClose_CheckedChanged);
            // 
            // cbFreehandCropAutoUpload
            // 
            this.cbFreehandCropAutoUpload.AutoSize = true;
            this.cbFreehandCropAutoUpload.Location = new System.Drawing.Point(21, 49);
            this.cbFreehandCropAutoUpload.Margin = new System.Windows.Forms.Padding(4);
            this.cbFreehandCropAutoUpload.Name = "cbFreehandCropAutoUpload";
            this.cbFreehandCropAutoUpload.Size = new System.Drawing.Size(293, 21);
            this.cbFreehandCropAutoUpload.TabIndex = 1;
            this.cbFreehandCropAutoUpload.Text = "Automatically upload after region is drawn";
            this.cbFreehandCropAutoUpload.UseVisualStyleBackColor = true;
            this.cbFreehandCropAutoUpload.CheckedChanged += new System.EventHandler(this.cbFreehandCropAutoUpload_CheckedChanged);
            // 
            // cbFreehandCropShowHelpText
            // 
            this.cbFreehandCropShowHelpText.AutoSize = true;
            this.cbFreehandCropShowHelpText.Location = new System.Drawing.Point(21, 20);
            this.cbFreehandCropShowHelpText.Margin = new System.Windows.Forms.Padding(4);
            this.cbFreehandCropShowHelpText.Name = "cbFreehandCropShowHelpText";
            this.cbFreehandCropShowHelpText.Size = new System.Drawing.Size(121, 21);
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
            this.tpWatermark.Location = new System.Drawing.Point(4, 25);
            this.tpWatermark.Margin = new System.Windows.Forms.Padding(4);
            this.tpWatermark.Name = "tpWatermark";
            this.tpWatermark.Padding = new System.Windows.Forms.Padding(4);
            this.tpWatermark.Size = new System.Drawing.Size(1060, 503);
            this.tpWatermark.TabIndex = 11;
            this.tpWatermark.Text = "Watermark";
            this.tpWatermark.UseVisualStyleBackColor = true;
            // 
            // pbWatermarkShow
            // 
            this.pbWatermarkShow.BackColor = System.Drawing.Color.White;
            this.pbWatermarkShow.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbWatermarkShow.Location = new System.Drawing.Point(11, 226);
            this.pbWatermarkShow.Margin = new System.Windows.Forms.Padding(4);
            this.pbWatermarkShow.Name = "pbWatermarkShow";
            this.pbWatermarkShow.Size = new System.Drawing.Size(362, 246);
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
            this.gbWatermarkGeneral.Location = new System.Drawing.Point(11, 10);
            this.gbWatermarkGeneral.Margin = new System.Windows.Forms.Padding(4);
            this.gbWatermarkGeneral.Name = "gbWatermarkGeneral";
            this.gbWatermarkGeneral.Padding = new System.Windows.Forms.Padding(4);
            this.gbWatermarkGeneral.Size = new System.Drawing.Size(363, 207);
            this.gbWatermarkGeneral.TabIndex = 26;
            this.gbWatermarkGeneral.TabStop = false;
            this.gbWatermarkGeneral.Text = "Watermark Settings";
            // 
            // lblWatermarkOffsetPixel
            // 
            this.lblWatermarkOffsetPixel.AutoSize = true;
            this.lblWatermarkOffsetPixel.Location = new System.Drawing.Point(203, 108);
            this.lblWatermarkOffsetPixel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblWatermarkOffsetPixel.Name = "lblWatermarkOffsetPixel";
            this.lblWatermarkOffsetPixel.Size = new System.Drawing.Size(22, 17);
            this.lblWatermarkOffsetPixel.TabIndex = 34;
            this.lblWatermarkOffsetPixel.Text = "px";
            // 
            // cboWatermarkType
            // 
            this.cboWatermarkType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboWatermarkType.FormattingEnabled = true;
            this.cboWatermarkType.Location = new System.Drawing.Point(117, 25);
            this.cboWatermarkType.Margin = new System.Windows.Forms.Padding(4);
            this.cboWatermarkType.Name = "cboWatermarkType";
            this.cboWatermarkType.Size = new System.Drawing.Size(159, 24);
            this.cboWatermarkType.TabIndex = 33;
            this.cboWatermarkType.SelectedIndexChanged += new System.EventHandler(this.cboWatermarkType_SelectedIndexChanged);
            // 
            // cbWatermarkAutoHide
            // 
            this.cbWatermarkAutoHide.AutoSize = true;
            this.cbWatermarkAutoHide.Location = new System.Drawing.Point(21, 167);
            this.cbWatermarkAutoHide.Margin = new System.Windows.Forms.Padding(4);
            this.cbWatermarkAutoHide.Name = "cbWatermarkAutoHide";
            this.cbWatermarkAutoHide.Size = new System.Drawing.Size(248, 21);
            this.cbWatermarkAutoHide.TabIndex = 32;
            this.cbWatermarkAutoHide.Text = "Hide Watermark if Image is smaller";
            this.cbWatermarkAutoHide.UseVisualStyleBackColor = true;
            this.cbWatermarkAutoHide.CheckedChanged += new System.EventHandler(this.cbWatermarkAutoHide_CheckedChanged);
            // 
            // cbWatermarkAddReflection
            // 
            this.cbWatermarkAddReflection.AutoSize = true;
            this.cbWatermarkAddReflection.Location = new System.Drawing.Point(21, 138);
            this.cbWatermarkAddReflection.Margin = new System.Windows.Forms.Padding(4);
            this.cbWatermarkAddReflection.Name = "cbWatermarkAddReflection";
            this.cbWatermarkAddReflection.Size = new System.Drawing.Size(122, 21);
            this.cbWatermarkAddReflection.TabIndex = 24;
            this.cbWatermarkAddReflection.Text = "Add Reflection";
            this.cbWatermarkAddReflection.UseVisualStyleBackColor = true;
            this.cbWatermarkAddReflection.CheckedChanged += new System.EventHandler(this.cbWatermarkAddReflection_CheckedChanged);
            // 
            // lblWatermarkType
            // 
            this.lblWatermarkType.AutoSize = true;
            this.lblWatermarkType.Location = new System.Drawing.Point(21, 30);
            this.lblWatermarkType.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblWatermarkType.Name = "lblWatermarkType";
            this.lblWatermarkType.Size = new System.Drawing.Size(44, 17);
            this.lblWatermarkType.TabIndex = 31;
            this.lblWatermarkType.Text = "Type:";
            // 
            // chkWatermarkPosition
            // 
            this.chkWatermarkPosition.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.chkWatermarkPosition.FormattingEnabled = true;
            this.chkWatermarkPosition.Location = new System.Drawing.Point(117, 64);
            this.chkWatermarkPosition.Margin = new System.Windows.Forms.Padding(4);
            this.chkWatermarkPosition.Name = "chkWatermarkPosition";
            this.chkWatermarkPosition.Size = new System.Drawing.Size(160, 24);
            this.chkWatermarkPosition.TabIndex = 18;
            this.chkWatermarkPosition.SelectedIndexChanged += new System.EventHandler(this.cbWatermarkPosition_SelectedIndexChanged);
            // 
            // lblWatermarkPosition
            // 
            this.lblWatermarkPosition.AutoSize = true;
            this.lblWatermarkPosition.Location = new System.Drawing.Point(21, 69);
            this.lblWatermarkPosition.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblWatermarkPosition.Name = "lblWatermarkPosition";
            this.lblWatermarkPosition.Size = new System.Drawing.Size(78, 17);
            this.lblWatermarkPosition.TabIndex = 19;
            this.lblWatermarkPosition.Text = "Placement:";
            // 
            // nudWatermarkOffset
            // 
            this.nudWatermarkOffset.Location = new System.Drawing.Point(117, 103);
            this.nudWatermarkOffset.Margin = new System.Windows.Forms.Padding(4);
            this.nudWatermarkOffset.Maximum = new decimal(new int[] {
            2000,
            0,
            0,
            0});
            this.nudWatermarkOffset.Name = "nudWatermarkOffset";
            this.nudWatermarkOffset.Size = new System.Drawing.Size(75, 22);
            this.nudWatermarkOffset.TabIndex = 6;
            this.nudWatermarkOffset.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.nudWatermarkOffset.ValueChanged += new System.EventHandler(this.nudWatermarkOffset_ValueChanged);
            // 
            // lblWatermarkOffset
            // 
            this.lblWatermarkOffset.AutoSize = true;
            this.lblWatermarkOffset.Location = new System.Drawing.Point(21, 108);
            this.lblWatermarkOffset.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblWatermarkOffset.Name = "lblWatermarkOffset";
            this.lblWatermarkOffset.Size = new System.Drawing.Size(50, 17);
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
            this.tcWatermark.Location = new System.Drawing.Point(384, 5);
            this.tcWatermark.Margin = new System.Windows.Forms.Padding(4);
            this.tcWatermark.Name = "tcWatermark";
            this.tcWatermark.SelectedIndex = 0;
            this.tcWatermark.Size = new System.Drawing.Size(650, 480);
            this.tcWatermark.TabIndex = 29;
            // 
            // tpWatermarkText
            // 
            this.tpWatermarkText.Controls.Add(this.gbWatermarkBackground);
            this.tpWatermarkText.Controls.Add(this.gbWatermarkText);
            this.tpWatermarkText.ImageKey = "textfield_rename.png";
            this.tpWatermarkText.Location = new System.Drawing.Point(4, 25);
            this.tpWatermarkText.Margin = new System.Windows.Forms.Padding(4);
            this.tpWatermarkText.Name = "tpWatermarkText";
            this.tpWatermarkText.Padding = new System.Windows.Forms.Padding(4);
            this.tpWatermarkText.Size = new System.Drawing.Size(642, 451);
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
            this.gbWatermarkBackground.Location = new System.Drawing.Point(11, 167);
            this.gbWatermarkBackground.Margin = new System.Windows.Forms.Padding(4);
            this.gbWatermarkBackground.Name = "gbWatermarkBackground";
            this.gbWatermarkBackground.Padding = new System.Windows.Forms.Padding(4);
            this.gbWatermarkBackground.Size = new System.Drawing.Size(611, 266);
            this.gbWatermarkBackground.TabIndex = 25;
            this.gbWatermarkBackground.TabStop = false;
            this.gbWatermarkBackground.Text = "Text Background Settings";
            // 
            // lblRectangleCornerRadius
            // 
            this.lblRectangleCornerRadius.AutoSize = true;
            this.lblRectangleCornerRadius.Location = new System.Drawing.Point(16, 31);
            this.lblRectangleCornerRadius.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblRectangleCornerRadius.Name = "lblRectangleCornerRadius";
            this.lblRectangleCornerRadius.Size = new System.Drawing.Size(169, 17);
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
            this.gbGradientMakerBasic.Location = new System.Drawing.Point(16, 59);
            this.gbGradientMakerBasic.Margin = new System.Windows.Forms.Padding(4);
            this.gbGradientMakerBasic.Name = "gbGradientMakerBasic";
            this.gbGradientMakerBasic.Padding = new System.Windows.Forms.Padding(4);
            this.gbGradientMakerBasic.Size = new System.Drawing.Size(575, 150);
            this.gbGradientMakerBasic.TabIndex = 34;
            this.gbGradientMakerBasic.TabStop = false;
            this.gbGradientMakerBasic.Text = "Gradient Maker (Basic)";
            // 
            // lblWatermarkBackColors
            // 
            this.lblWatermarkBackColors.AutoSize = true;
            this.lblWatermarkBackColors.Location = new System.Drawing.Point(11, 31);
            this.lblWatermarkBackColors.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblWatermarkBackColors.Name = "lblWatermarkBackColors";
            this.lblWatermarkBackColors.Size = new System.Drawing.Size(132, 17);
            this.lblWatermarkBackColors.TabIndex = 12;
            this.lblWatermarkBackColors.Text = "Background Colors:";
            // 
            // trackWatermarkBackgroundTrans
            // 
            this.trackWatermarkBackgroundTrans.AutoSize = false;
            this.trackWatermarkBackgroundTrans.BackColor = System.Drawing.SystemColors.Window;
            this.trackWatermarkBackgroundTrans.Location = new System.Drawing.Point(203, 66);
            this.trackWatermarkBackgroundTrans.Margin = new System.Windows.Forms.Padding(4);
            this.trackWatermarkBackgroundTrans.Maximum = 255;
            this.trackWatermarkBackgroundTrans.Name = "trackWatermarkBackgroundTrans";
            this.trackWatermarkBackgroundTrans.Size = new System.Drawing.Size(267, 30);
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
            this.pbWatermarkGradient2.Location = new System.Drawing.Point(192, 25);
            this.pbWatermarkGradient2.Margin = new System.Windows.Forms.Padding(4);
            this.pbWatermarkGradient2.Name = "pbWatermarkGradient2";
            this.pbWatermarkGradient2.Size = new System.Drawing.Size(31, 29);
            this.pbWatermarkGradient2.TabIndex = 11;
            this.pbWatermarkGradient2.TabStop = false;
            this.pbWatermarkGradient2.Click += new System.EventHandler(this.pbWatermarkGradient2_Click);
            // 
            // cbWatermarkGradientType
            // 
            this.cbWatermarkGradientType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbWatermarkGradientType.FormattingEnabled = true;
            this.cbWatermarkGradientType.Location = new System.Drawing.Point(117, 110);
            this.cbWatermarkGradientType.Margin = new System.Windows.Forms.Padding(4);
            this.cbWatermarkGradientType.Name = "cbWatermarkGradientType";
            this.cbWatermarkGradientType.Size = new System.Drawing.Size(160, 24);
            this.cbWatermarkGradientType.TabIndex = 25;
            this.cbWatermarkGradientType.SelectedIndexChanged += new System.EventHandler(this.cbWatermarkGradientType_SelectedIndexChanged);
            // 
            // pbWatermarkBorderColor
            // 
            this.pbWatermarkBorderColor.BackColor = System.Drawing.Color.Black;
            this.pbWatermarkBorderColor.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pbWatermarkBorderColor.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pbWatermarkBorderColor.Location = new System.Drawing.Point(235, 25);
            this.pbWatermarkBorderColor.Margin = new System.Windows.Forms.Padding(4);
            this.pbWatermarkBorderColor.Name = "pbWatermarkBorderColor";
            this.pbWatermarkBorderColor.Size = new System.Drawing.Size(31, 29);
            this.pbWatermarkBorderColor.TabIndex = 14;
            this.pbWatermarkBorderColor.TabStop = false;
            this.pbWatermarkBorderColor.Click += new System.EventHandler(this.pbWatermarkBorderColor_Click);
            // 
            // lblWatermarkGradientType
            // 
            this.lblWatermarkGradientType.AutoSize = true;
            this.lblWatermarkGradientType.Location = new System.Drawing.Point(11, 116);
            this.lblWatermarkGradientType.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblWatermarkGradientType.Name = "lblWatermarkGradientType";
            this.lblWatermarkGradientType.Size = new System.Drawing.Size(103, 17);
            this.lblWatermarkGradientType.TabIndex = 24;
            this.lblWatermarkGradientType.Text = "Gradient Type:";
            // 
            // pbWatermarkGradient1
            // 
            this.pbWatermarkGradient1.BackColor = System.Drawing.Color.White;
            this.pbWatermarkGradient1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pbWatermarkGradient1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pbWatermarkGradient1.Location = new System.Drawing.Point(149, 25);
            this.pbWatermarkGradient1.Margin = new System.Windows.Forms.Padding(4);
            this.pbWatermarkGradient1.Name = "pbWatermarkGradient1";
            this.pbWatermarkGradient1.Size = new System.Drawing.Size(31, 29);
            this.pbWatermarkGradient1.TabIndex = 10;
            this.pbWatermarkGradient1.TabStop = false;
            this.pbWatermarkGradient1.Click += new System.EventHandler(this.pbWatermarkGradient1_Click);
            // 
            // lblWatermarkBackTrans
            // 
            this.lblWatermarkBackTrans.AutoSize = true;
            this.lblWatermarkBackTrans.Location = new System.Drawing.Point(11, 70);
            this.lblWatermarkBackTrans.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblWatermarkBackTrans.Name = "lblWatermarkBackTrans";
            this.lblWatermarkBackTrans.Size = new System.Drawing.Size(180, 17);
            this.lblWatermarkBackTrans.TabIndex = 7;
            this.lblWatermarkBackTrans.Text = "Background Transparency:";
            // 
            // nudWatermarkBackTrans
            // 
            this.nudWatermarkBackTrans.BackColor = System.Drawing.SystemColors.Window;
            this.nudWatermarkBackTrans.Location = new System.Drawing.Point(469, 66);
            this.nudWatermarkBackTrans.Margin = new System.Windows.Forms.Padding(4);
            this.nudWatermarkBackTrans.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.nudWatermarkBackTrans.Name = "nudWatermarkBackTrans";
            this.nudWatermarkBackTrans.ReadOnly = true;
            this.nudWatermarkBackTrans.Size = new System.Drawing.Size(64, 22);
            this.nudWatermarkBackTrans.TabIndex = 8;
            this.nudWatermarkBackTrans.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.nudWatermarkBackTrans.ValueChanged += new System.EventHandler(this.nudWatermarkBackTrans_ValueChanged);
            // 
            // lblWatermarkBackColorsTip
            // 
            this.lblWatermarkBackColorsTip.AutoSize = true;
            this.lblWatermarkBackColorsTip.Location = new System.Drawing.Point(277, 31);
            this.lblWatermarkBackColorsTip.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblWatermarkBackColorsTip.Name = "lblWatermarkBackColorsTip";
            this.lblWatermarkBackColorsTip.Size = new System.Drawing.Size(264, 17);
            this.lblWatermarkBackColorsTip.TabIndex = 20;
            this.lblWatermarkBackColorsTip.Text = "1 && 2 = Gradient colors, 3 = Border color";
            // 
            // btnSelectGradient
            // 
            this.btnSelectGradient.Location = new System.Drawing.Point(261, 219);
            this.btnSelectGradient.Margin = new System.Windows.Forms.Padding(4);
            this.btnSelectGradient.Name = "btnSelectGradient";
            this.btnSelectGradient.Size = new System.Drawing.Size(149, 28);
            this.btnSelectGradient.TabIndex = 33;
            this.btnSelectGradient.Text = "Gradient Maker...";
            this.btnSelectGradient.UseVisualStyleBackColor = true;
            this.btnSelectGradient.Click += new System.EventHandler(this.btnSelectGradient_Click);
            // 
            // cboUseCustomGradient
            // 
            this.cboUseCustomGradient.AutoSize = true;
            this.cboUseCustomGradient.Location = new System.Drawing.Point(16, 222);
            this.cboUseCustomGradient.Margin = new System.Windows.Forms.Padding(4);
            this.cboUseCustomGradient.Name = "cboUseCustomGradient";
            this.cboUseCustomGradient.Size = new System.Drawing.Size(234, 21);
            this.cboUseCustomGradient.TabIndex = 32;
            this.cboUseCustomGradient.Text = "Use Gradient Maker (Advanced)";
            this.cboUseCustomGradient.UseVisualStyleBackColor = true;
            this.cboUseCustomGradient.CheckedChanged += new System.EventHandler(this.cbUseCustomGradient_CheckedChanged);
            // 
            // nudWatermarkCornerRadius
            // 
            this.nudWatermarkCornerRadius.Location = new System.Drawing.Point(197, 25);
            this.nudWatermarkCornerRadius.Margin = new System.Windows.Forms.Padding(4);
            this.nudWatermarkCornerRadius.Maximum = new decimal(new int[] {
            15,
            0,
            0,
            0});
            this.nudWatermarkCornerRadius.Name = "nudWatermarkCornerRadius";
            this.nudWatermarkCornerRadius.Size = new System.Drawing.Size(64, 22);
            this.nudWatermarkCornerRadius.TabIndex = 22;
            this.nudWatermarkCornerRadius.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.nudWatermarkCornerRadius.ValueChanged += new System.EventHandler(this.nudWatermarkCornerRadius_ValueChanged);
            // 
            // lblWatermarkCornerRadiusTip
            // 
            this.lblWatermarkCornerRadiusTip.AutoSize = true;
            this.lblWatermarkCornerRadiusTip.Location = new System.Drawing.Point(272, 31);
            this.lblWatermarkCornerRadiusTip.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblWatermarkCornerRadiusTip.Name = "lblWatermarkCornerRadiusTip";
            this.lblWatermarkCornerRadiusTip.Size = new System.Drawing.Size(196, 17);
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
            this.gbWatermarkText.Location = new System.Drawing.Point(11, 10);
            this.gbWatermarkText.Margin = new System.Windows.Forms.Padding(4);
            this.gbWatermarkText.Name = "gbWatermarkText";
            this.gbWatermarkText.Padding = new System.Windows.Forms.Padding(4);
            this.gbWatermarkText.Size = new System.Drawing.Size(611, 148);
            this.gbWatermarkText.TabIndex = 24;
            this.gbWatermarkText.TabStop = false;
            this.gbWatermarkText.Text = "Text Settings";
            // 
            // trackWatermarkFontTrans
            // 
            this.trackWatermarkFontTrans.AutoSize = false;
            this.trackWatermarkFontTrans.BackColor = System.Drawing.SystemColors.Window;
            this.trackWatermarkFontTrans.Location = new System.Drawing.Point(203, 105);
            this.trackWatermarkFontTrans.Margin = new System.Windows.Forms.Padding(4);
            this.trackWatermarkFontTrans.Maximum = 255;
            this.trackWatermarkFontTrans.Name = "trackWatermarkFontTrans";
            this.trackWatermarkFontTrans.Size = new System.Drawing.Size(267, 30);
            this.trackWatermarkFontTrans.TabIndex = 30;
            this.trackWatermarkFontTrans.Tag = "Adjust Font Transparency. 0 = Invisible. ";
            this.trackWatermarkFontTrans.TickFrequency = 5;
            this.trackWatermarkFontTrans.TickStyle = System.Windows.Forms.TickStyle.None;
            this.trackWatermarkFontTrans.Scroll += new System.EventHandler(this.trackWatermarkFontTrans_Scroll);
            // 
            // lblWatermarkText
            // 
            this.lblWatermarkText.AutoSize = true;
            this.lblWatermarkText.Location = new System.Drawing.Point(11, 30);
            this.lblWatermarkText.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblWatermarkText.Name = "lblWatermarkText";
            this.lblWatermarkText.Size = new System.Drawing.Size(112, 17);
            this.lblWatermarkText.TabIndex = 16;
            this.lblWatermarkText.Text = "Watermark Text:";
            // 
            // nudWatermarkFontTrans
            // 
            this.nudWatermarkFontTrans.BackColor = System.Drawing.SystemColors.Window;
            this.nudWatermarkFontTrans.Location = new System.Drawing.Point(480, 105);
            this.nudWatermarkFontTrans.Margin = new System.Windows.Forms.Padding(4);
            this.nudWatermarkFontTrans.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.nudWatermarkFontTrans.Name = "nudWatermarkFontTrans";
            this.nudWatermarkFontTrans.ReadOnly = true;
            this.nudWatermarkFontTrans.Size = new System.Drawing.Size(64, 22);
            this.nudWatermarkFontTrans.TabIndex = 22;
            this.nudWatermarkFontTrans.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.nudWatermarkFontTrans.ValueChanged += new System.EventHandler(this.nudWatermarkFontTrans_ValueChanged);
            // 
            // lblWatermarkFont
            // 
            this.lblWatermarkFont.AutoSize = true;
            this.lblWatermarkFont.Location = new System.Drawing.Point(181, 69);
            this.lblWatermarkFont.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblWatermarkFont.Name = "lblWatermarkFont";
            this.lblWatermarkFont.Size = new System.Drawing.Size(110, 17);
            this.lblWatermarkFont.TabIndex = 4;
            this.lblWatermarkFont.Text = "Font Information";
            // 
            // btnWatermarkFont
            // 
            this.btnWatermarkFont.Location = new System.Drawing.Point(11, 59);
            this.btnWatermarkFont.Margin = new System.Windows.Forms.Padding(4);
            this.btnWatermarkFont.Name = "btnWatermarkFont";
            this.btnWatermarkFont.Size = new System.Drawing.Size(117, 30);
            this.btnWatermarkFont.TabIndex = 3;
            this.btnWatermarkFont.Text = "Change Font...";
            this.btnWatermarkFont.UseVisualStyleBackColor = true;
            this.btnWatermarkFont.Click += new System.EventHandler(this.btnWatermarkFont_Click);
            // 
            // lblWatermarkFontTrans
            // 
            this.lblWatermarkFontTrans.AutoSize = true;
            this.lblWatermarkFontTrans.Location = new System.Drawing.Point(11, 108);
            this.lblWatermarkFontTrans.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblWatermarkFontTrans.Name = "lblWatermarkFontTrans";
            this.lblWatermarkFontTrans.Size = new System.Drawing.Size(132, 17);
            this.lblWatermarkFontTrans.TabIndex = 21;
            this.lblWatermarkFontTrans.Text = "Font Transparency:";
            // 
            // txtWatermarkText
            // 
            this.txtWatermarkText.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtWatermarkText.Location = new System.Drawing.Point(139, 23);
            this.txtWatermarkText.Margin = new System.Windows.Forms.Padding(4);
            this.txtWatermarkText.Name = "txtWatermarkText";
            this.txtWatermarkText.Size = new System.Drawing.Size(447, 22);
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
            this.pbWatermarkFontColor.Location = new System.Drawing.Point(139, 59);
            this.pbWatermarkFontColor.Margin = new System.Windows.Forms.Padding(4);
            this.pbWatermarkFontColor.Name = "pbWatermarkFontColor";
            this.pbWatermarkFontColor.Size = new System.Drawing.Size(31, 29);
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
            this.tpWatermarkImage.Location = new System.Drawing.Point(4, 25);
            this.tpWatermarkImage.Margin = new System.Windows.Forms.Padding(4);
            this.tpWatermarkImage.Name = "tpWatermarkImage";
            this.tpWatermarkImage.Padding = new System.Windows.Forms.Padding(4);
            this.tpWatermarkImage.Size = new System.Drawing.Size(642, 451);
            this.tpWatermarkImage.TabIndex = 1;
            this.tpWatermarkImage.Text = "Image";
            this.tpWatermarkImage.UseVisualStyleBackColor = true;
            // 
            // lblWatermarkImageScale
            // 
            this.lblWatermarkImageScale.AutoSize = true;
            this.lblWatermarkImageScale.Location = new System.Drawing.Point(21, 94);
            this.lblWatermarkImageScale.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblWatermarkImageScale.Name = "lblWatermarkImageScale";
            this.lblWatermarkImageScale.Size = new System.Drawing.Size(155, 17);
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
            this.nudWatermarkImageScale.Location = new System.Drawing.Point(181, 89);
            this.nudWatermarkImageScale.Margin = new System.Windows.Forms.Padding(4);
            this.nudWatermarkImageScale.Maximum = new decimal(new int[] {
            2000,
            0,
            0,
            0});
            this.nudWatermarkImageScale.Name = "nudWatermarkImageScale";
            this.nudWatermarkImageScale.Size = new System.Drawing.Size(75, 22);
            this.nudWatermarkImageScale.TabIndex = 24;
            this.nudWatermarkImageScale.ValueChanged += new System.EventHandler(this.nudWatermarkImageScale_ValueChanged);
            // 
            // cbWatermarkUseBorder
            // 
            this.cbWatermarkUseBorder.AutoSize = true;
            this.cbWatermarkUseBorder.Location = new System.Drawing.Point(21, 59);
            this.cbWatermarkUseBorder.Margin = new System.Windows.Forms.Padding(4);
            this.cbWatermarkUseBorder.Name = "cbWatermarkUseBorder";
            this.cbWatermarkUseBorder.Size = new System.Drawing.Size(102, 21);
            this.cbWatermarkUseBorder.TabIndex = 23;
            this.cbWatermarkUseBorder.Text = "Add Border";
            this.cbWatermarkUseBorder.UseVisualStyleBackColor = true;
            this.cbWatermarkUseBorder.CheckedChanged += new System.EventHandler(this.cbWatermarkUseBorder_CheckedChanged);
            // 
            // btwWatermarkBrowseImage
            // 
            this.btwWatermarkBrowseImage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btwWatermarkBrowseImage.Location = new System.Drawing.Point(533, 16);
            this.btwWatermarkBrowseImage.Margin = new System.Windows.Forms.Padding(4);
            this.btwWatermarkBrowseImage.Name = "btwWatermarkBrowseImage";
            this.btwWatermarkBrowseImage.Size = new System.Drawing.Size(85, 30);
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
            this.txtWatermarkImageLocation.Location = new System.Drawing.Point(21, 20);
            this.txtWatermarkImageLocation.Margin = new System.Windows.Forms.Padding(4);
            this.txtWatermarkImageLocation.Name = "txtWatermarkImageLocation";
            this.txtWatermarkImageLocation.Size = new System.Drawing.Size(500, 22);
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
            this.tpFileNaming.Location = new System.Drawing.Point(4, 25);
            this.tpFileNaming.Margin = new System.Windows.Forms.Padding(4);
            this.tpFileNaming.Name = "tpFileNaming";
            this.tpFileNaming.Size = new System.Drawing.Size(1060, 503);
            this.tpFileNaming.TabIndex = 3;
            this.tpFileNaming.Text = "Naming Conventions";
            this.tpFileNaming.UseVisualStyleBackColor = true;
            // 
            // lblMaxNameLength
            // 
            this.lblMaxNameLength.AutoSize = true;
            this.lblMaxNameLength.Location = new System.Drawing.Point(331, 236);
            this.lblMaxNameLength.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblMaxNameLength.Name = "lblMaxNameLength";
            this.lblMaxNameLength.Size = new System.Drawing.Size(240, 17);
            this.lblMaxNameLength.TabIndex = 118;
            this.lblMaxNameLength.Text = "Maximum name length (0 = No limit) :";
            // 
            // nudMaxNameLength
            // 
            this.nudMaxNameLength.Location = new System.Drawing.Point(576, 233);
            this.nudMaxNameLength.Margin = new System.Windows.Forms.Padding(4);
            this.nudMaxNameLength.Name = "nudMaxNameLength";
            this.nudMaxNameLength.Size = new System.Drawing.Size(75, 22);
            this.nudMaxNameLength.TabIndex = 117;
            this.nudMaxNameLength.ValueChanged += new System.EventHandler(this.nudMaxNameLength_ValueChanged);
            // 
            // btnResetIncrement
            // 
            this.btnResetIncrement.Location = new System.Drawing.Point(11, 345);
            this.btnResetIncrement.Margin = new System.Windows.Forms.Padding(4);
            this.btnResetIncrement.Name = "btnResetIncrement";
            this.btnResetIncrement.Size = new System.Drawing.Size(245, 28);
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
            this.gbOthersNaming.Location = new System.Drawing.Point(320, 118);
            this.gbOthersNaming.Margin = new System.Windows.Forms.Padding(4);
            this.gbOthersNaming.Name = "gbOthersNaming";
            this.gbOthersNaming.Padding = new System.Windows.Forms.Padding(4);
            this.gbOthersNaming.Size = new System.Drawing.Size(515, 98);
            this.gbOthersNaming.TabIndex = 115;
            this.gbOthersNaming.TabStop = false;
            this.gbOthersNaming.Text = "Other Capture Types";
            // 
            // lblEntireScreenPreview
            // 
            this.lblEntireScreenPreview.AutoSize = true;
            this.lblEntireScreenPreview.Location = new System.Drawing.Point(21, 69);
            this.lblEntireScreenPreview.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblEntireScreenPreview.Name = "lblEntireScreenPreview";
            this.lblEntireScreenPreview.Size = new System.Drawing.Size(147, 17);
            this.lblEntireScreenPreview.TabIndex = 6;
            this.lblEntireScreenPreview.Text = "Entire Screen Preview";
            // 
            // txtEntireScreen
            // 
            this.txtEntireScreen.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtEntireScreen.Location = new System.Drawing.Point(21, 30);
            this.txtEntireScreen.Margin = new System.Windows.Forms.Padding(4);
            this.txtEntireScreen.Name = "txtEntireScreen";
            this.txtEntireScreen.Size = new System.Drawing.Size(471, 22);
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
            this.gbCodeTitle.Location = new System.Drawing.Point(11, 10);
            this.gbCodeTitle.Margin = new System.Windows.Forms.Padding(4);
            this.gbCodeTitle.Name = "gbCodeTitle";
            this.gbCodeTitle.Padding = new System.Windows.Forms.Padding(4);
            this.gbCodeTitle.Size = new System.Drawing.Size(299, 325);
            this.gbCodeTitle.TabIndex = 111;
            this.gbCodeTitle.TabStop = false;
            this.gbCodeTitle.Text = "Environment Varables";
            // 
            // btnCodesI
            // 
            this.btnCodesI.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCodesI.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnCodesI.Location = new System.Drawing.Point(79, 247);
            this.btnCodesI.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnCodesI.Name = "btnCodesI";
            this.btnCodesI.Size = new System.Drawing.Size(203, 27);
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
            this.btnCodesPm.Location = new System.Drawing.Point(79, 281);
            this.btnCodesPm.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnCodesPm.Name = "btnCodesPm";
            this.btnCodesPm.Size = new System.Drawing.Size(203, 27);
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
            this.btnCodesS.Location = new System.Drawing.Point(79, 214);
            this.btnCodesS.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnCodesS.Name = "btnCodesS";
            this.btnCodesS.Size = new System.Drawing.Size(203, 27);
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
            this.btnCodesMi.Location = new System.Drawing.Point(79, 182);
            this.btnCodesMi.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnCodesMi.Name = "btnCodesMi";
            this.btnCodesMi.Size = new System.Drawing.Size(203, 27);
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
            this.btnCodesH.Location = new System.Drawing.Point(79, 150);
            this.btnCodesH.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnCodesH.Name = "btnCodesH";
            this.btnCodesH.Size = new System.Drawing.Size(203, 27);
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
            this.btnCodesY.Location = new System.Drawing.Point(79, 118);
            this.btnCodesY.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnCodesY.Name = "btnCodesY";
            this.btnCodesY.Size = new System.Drawing.Size(203, 27);
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
            this.btnCodesD.Location = new System.Drawing.Point(79, 86);
            this.btnCodesD.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnCodesD.Name = "btnCodesD";
            this.btnCodesD.Size = new System.Drawing.Size(203, 27);
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
            this.btnCodesMo.Location = new System.Drawing.Point(79, 54);
            this.btnCodesMo.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnCodesMo.Name = "btnCodesMo";
            this.btnCodesMo.Size = new System.Drawing.Size(203, 27);
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
            this.btnCodesT.Location = new System.Drawing.Point(79, 23);
            this.btnCodesT.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnCodesT.Name = "btnCodesT";
            this.btnCodesT.Size = new System.Drawing.Size(203, 27);
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
            this.lblCodeI.Location = new System.Drawing.Point(27, 252);
            this.lblCodeI.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCodeI.Name = "lblCodeI";
            this.lblCodeI.Size = new System.Drawing.Size(23, 17);
            this.lblCodeI.TabIndex = 90;
            this.lblCodeI.Text = "%i";
            // 
            // lblCodeT
            // 
            this.lblCodeT.AutoSize = true;
            this.lblCodeT.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblCodeT.Location = new System.Drawing.Point(27, 27);
            this.lblCodeT.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCodeT.Name = "lblCodeT";
            this.lblCodeT.Size = new System.Drawing.Size(24, 17);
            this.lblCodeT.TabIndex = 74;
            this.lblCodeT.Text = "%t";
            // 
            // lblCodeMo
            // 
            this.lblCodeMo.AutoSize = true;
            this.lblCodeMo.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblCodeMo.Location = new System.Drawing.Point(27, 59);
            this.lblCodeMo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCodeMo.Name = "lblCodeMo";
            this.lblCodeMo.Size = new System.Drawing.Size(39, 17);
            this.lblCodeMo.TabIndex = 75;
            this.lblCodeMo.Text = "%mo";
            // 
            // lblCodePm
            // 
            this.lblCodePm.AutoSize = true;
            this.lblCodePm.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblCodePm.Location = new System.Drawing.Point(27, 284);
            this.lblCodePm.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCodePm.Name = "lblCodePm";
            this.lblCodePm.Size = new System.Drawing.Size(39, 17);
            this.lblCodePm.TabIndex = 88;
            this.lblCodePm.Text = "%pm";
            // 
            // lblCodeD
            // 
            this.lblCodeD.AutoSize = true;
            this.lblCodeD.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblCodeD.Location = new System.Drawing.Point(27, 90);
            this.lblCodeD.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCodeD.Name = "lblCodeD";
            this.lblCodeD.Size = new System.Drawing.Size(28, 17);
            this.lblCodeD.TabIndex = 76;
            this.lblCodeD.Text = "%d";
            // 
            // lblCodeS
            // 
            this.lblCodeS.AutoSize = true;
            this.lblCodeS.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblCodeS.Location = new System.Drawing.Point(27, 218);
            this.lblCodeS.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCodeS.Name = "lblCodeS";
            this.lblCodeS.Size = new System.Drawing.Size(27, 17);
            this.lblCodeS.TabIndex = 86;
            this.lblCodeS.Text = "%s";
            // 
            // lblCodeMi
            // 
            this.lblCodeMi.AutoSize = true;
            this.lblCodeMi.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblCodeMi.Location = new System.Drawing.Point(27, 187);
            this.lblCodeMi.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCodeMi.Name = "lblCodeMi";
            this.lblCodeMi.Size = new System.Drawing.Size(34, 17);
            this.lblCodeMi.TabIndex = 84;
            this.lblCodeMi.Text = "%mi";
            // 
            // lblCodeY
            // 
            this.lblCodeY.AutoSize = true;
            this.lblCodeY.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblCodeY.Location = new System.Drawing.Point(27, 122);
            this.lblCodeY.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCodeY.Name = "lblCodeY";
            this.lblCodeY.Size = new System.Drawing.Size(27, 17);
            this.lblCodeY.TabIndex = 80;
            this.lblCodeY.Text = "%y";
            // 
            // lblCodeH
            // 
            this.lblCodeH.AutoSize = true;
            this.lblCodeH.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblCodeH.Location = new System.Drawing.Point(27, 155);
            this.lblCodeH.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCodeH.Name = "lblCodeH";
            this.lblCodeH.Size = new System.Drawing.Size(28, 17);
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
            this.gbActiveWindowNaming.Location = new System.Drawing.Point(320, 10);
            this.gbActiveWindowNaming.Margin = new System.Windows.Forms.Padding(4);
            this.gbActiveWindowNaming.Name = "gbActiveWindowNaming";
            this.gbActiveWindowNaming.Padding = new System.Windows.Forms.Padding(4);
            this.gbActiveWindowNaming.Size = new System.Drawing.Size(515, 98);
            this.gbActiveWindowNaming.TabIndex = 113;
            this.gbActiveWindowNaming.TabStop = false;
            this.gbActiveWindowNaming.Text = "Active Window";
            // 
            // lblActiveWindowPreview
            // 
            this.lblActiveWindowPreview.AutoSize = true;
            this.lblActiveWindowPreview.Location = new System.Drawing.Point(21, 69);
            this.lblActiveWindowPreview.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblActiveWindowPreview.Name = "lblActiveWindowPreview";
            this.lblActiveWindowPreview.Size = new System.Drawing.Size(152, 17);
            this.lblActiveWindowPreview.TabIndex = 4;
            this.lblActiveWindowPreview.Text = "Active Window Preview";
            // 
            // txtActiveWindow
            // 
            this.txtActiveWindow.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtActiveWindow.Location = new System.Drawing.Point(21, 30);
            this.txtActiveWindow.Margin = new System.Windows.Forms.Padding(4);
            this.txtActiveWindow.Name = "txtActiveWindow";
            this.txtActiveWindow.Size = new System.Drawing.Size(471, 22);
            this.txtActiveWindow.TabIndex = 2;
            this.txtActiveWindow.TextChanged += new System.EventHandler(this.txtActiveWindow_TextChanged);
            this.txtActiveWindow.Leave += new System.EventHandler(this.txtActiveWindow_Leave);
            // 
            // tpCaptureQuality
            // 
            this.tpCaptureQuality.Controls.Add(this.gbImageSize);
            this.tpCaptureQuality.Controls.Add(this.gbPictureQuality);
            this.tpCaptureQuality.Location = new System.Drawing.Point(4, 25);
            this.tpCaptureQuality.Margin = new System.Windows.Forms.Padding(4);
            this.tpCaptureQuality.Name = "tpCaptureQuality";
            this.tpCaptureQuality.Padding = new System.Windows.Forms.Padding(4);
            this.tpCaptureQuality.Size = new System.Drawing.Size(1060, 503);
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
            this.gbImageSize.Location = new System.Drawing.Point(11, 187);
            this.gbImageSize.Margin = new System.Windows.Forms.Padding(4);
            this.gbImageSize.Name = "gbImageSize";
            this.gbImageSize.Padding = new System.Windows.Forms.Padding(4);
            this.gbImageSize.Size = new System.Drawing.Size(1024, 148);
            this.gbImageSize.TabIndex = 124;
            this.gbImageSize.TabStop = false;
            this.gbImageSize.Text = "Image Size";
            // 
            // rbImageSizeDefault
            // 
            this.rbImageSizeDefault.AutoSize = true;
            this.rbImageSizeDefault.Location = new System.Drawing.Point(21, 30);
            this.rbImageSizeDefault.Margin = new System.Windows.Forms.Padding(4);
            this.rbImageSizeDefault.Name = "rbImageSizeDefault";
            this.rbImageSizeDefault.Size = new System.Drawing.Size(143, 21);
            this.rbImageSizeDefault.TabIndex = 127;
            this.rbImageSizeDefault.TabStop = true;
            this.rbImageSizeDefault.Text = "Image size default";
            this.rbImageSizeDefault.UseVisualStyleBackColor = true;
            this.rbImageSizeDefault.CheckedChanged += new System.EventHandler(this.rbImageSize_CheckedChanged);
            // 
            // lblImageSizeFixedHeight
            // 
            this.lblImageSizeFixedHeight.AutoSize = true;
            this.lblImageSizeFixedHeight.Location = new System.Drawing.Point(309, 73);
            this.lblImageSizeFixedHeight.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblImageSizeFixedHeight.Name = "lblImageSizeFixedHeight";
            this.lblImageSizeFixedHeight.Size = new System.Drawing.Size(81, 17);
            this.lblImageSizeFixedHeight.TabIndex = 126;
            this.lblImageSizeFixedHeight.Text = "Height (px):";
            // 
            // rbImageSizeFixed
            // 
            this.rbImageSizeFixed.AutoSize = true;
            this.rbImageSizeFixed.Location = new System.Drawing.Point(21, 69);
            this.rbImageSizeFixed.Margin = new System.Windows.Forms.Padding(4);
            this.rbImageSizeFixed.Name = "rbImageSizeFixed";
            this.rbImageSizeFixed.Size = new System.Drawing.Size(133, 21);
            this.rbImageSizeFixed.TabIndex = 123;
            this.rbImageSizeFixed.TabStop = true;
            this.rbImageSizeFixed.Text = "Image size fixed:";
            this.rbImageSizeFixed.UseVisualStyleBackColor = true;
            this.rbImageSizeFixed.CheckedChanged += new System.EventHandler(this.rbImageSize_CheckedChanged);
            // 
            // lblImageSizeFixedWidth
            // 
            this.lblImageSizeFixedWidth.AutoSize = true;
            this.lblImageSizeFixedWidth.Location = new System.Drawing.Point(160, 73);
            this.lblImageSizeFixedWidth.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblImageSizeFixedWidth.Name = "lblImageSizeFixedWidth";
            this.lblImageSizeFixedWidth.Size = new System.Drawing.Size(76, 17);
            this.lblImageSizeFixedWidth.TabIndex = 125;
            this.lblImageSizeFixedWidth.Text = "Width (px):";
            // 
            // txtImageSizeRatio
            // 
            this.txtImageSizeRatio.Location = new System.Drawing.Point(160, 107);
            this.txtImageSizeRatio.Margin = new System.Windows.Forms.Padding(4);
            this.txtImageSizeRatio.Name = "txtImageSizeRatio";
            this.txtImageSizeRatio.Size = new System.Drawing.Size(41, 22);
            this.txtImageSizeRatio.TabIndex = 116;
            this.txtImageSizeRatio.Text = "100";
            this.txtImageSizeRatio.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtImageSizeRatio.TextChanged += new System.EventHandler(this.txtImageSizeRatio_TextChanged);
            // 
            // lblImageSizeRatioPercentage
            // 
            this.lblImageSizeRatioPercentage.AutoSize = true;
            this.lblImageSizeRatioPercentage.Location = new System.Drawing.Point(212, 112);
            this.lblImageSizeRatioPercentage.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblImageSizeRatioPercentage.Name = "lblImageSizeRatioPercentage";
            this.lblImageSizeRatioPercentage.Size = new System.Drawing.Size(20, 17);
            this.lblImageSizeRatioPercentage.TabIndex = 118;
            this.lblImageSizeRatioPercentage.Text = "%";
            // 
            // txtImageSizeFixedWidth
            // 
            this.txtImageSizeFixedWidth.Location = new System.Drawing.Point(245, 69);
            this.txtImageSizeFixedWidth.Margin = new System.Windows.Forms.Padding(4);
            this.txtImageSizeFixedWidth.Name = "txtImageSizeFixedWidth";
            this.txtImageSizeFixedWidth.Size = new System.Drawing.Size(52, 22);
            this.txtImageSizeFixedWidth.TabIndex = 119;
            this.txtImageSizeFixedWidth.Text = "2500";
            this.txtImageSizeFixedWidth.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtImageSizeFixedWidth.TextChanged += new System.EventHandler(this.txtImageSizeFixedWidth_TextChanged);
            // 
            // rbImageSizeRatio
            // 
            this.rbImageSizeRatio.AutoSize = true;
            this.rbImageSizeRatio.Location = new System.Drawing.Point(21, 108);
            this.rbImageSizeRatio.Margin = new System.Windows.Forms.Padding(4);
            this.rbImageSizeRatio.Name = "rbImageSizeRatio";
            this.rbImageSizeRatio.Size = new System.Drawing.Size(132, 21);
            this.rbImageSizeRatio.TabIndex = 122;
            this.rbImageSizeRatio.TabStop = true;
            this.rbImageSizeRatio.Text = "Image size ratio:";
            this.rbImageSizeRatio.UseVisualStyleBackColor = true;
            this.rbImageSizeRatio.CheckedChanged += new System.EventHandler(this.rbImageSize_CheckedChanged);
            // 
            // txtImageSizeFixedHeight
            // 
            this.txtImageSizeFixedHeight.Location = new System.Drawing.Point(395, 69);
            this.txtImageSizeFixedHeight.Margin = new System.Windows.Forms.Padding(4);
            this.txtImageSizeFixedHeight.Name = "txtImageSizeFixedHeight";
            this.txtImageSizeFixedHeight.Size = new System.Drawing.Size(52, 22);
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
            this.gbPictureQuality.Location = new System.Drawing.Point(11, 10);
            this.gbPictureQuality.Margin = new System.Windows.Forms.Padding(4);
            this.gbPictureQuality.Name = "gbPictureQuality";
            this.gbPictureQuality.Padding = new System.Windows.Forms.Padding(4);
            this.gbPictureQuality.Size = new System.Drawing.Size(1024, 167);
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
            this.cbGIFQuality.Location = new System.Drawing.Point(320, 49);
            this.cbGIFQuality.Margin = new System.Windows.Forms.Padding(4);
            this.cbGIFQuality.Name = "cbGIFQuality";
            this.cbGIFQuality.Size = new System.Drawing.Size(160, 24);
            this.cbGIFQuality.TabIndex = 118;
            this.cbGIFQuality.SelectedIndexChanged += new System.EventHandler(this.cbGIFQuality_SelectedIndexChanged);
            // 
            // lblGIFQuality
            // 
            this.lblGIFQuality.AutoSize = true;
            this.lblGIFQuality.Location = new System.Drawing.Point(320, 30);
            this.lblGIFQuality.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblGIFQuality.Name = "lblGIFQuality";
            this.lblGIFQuality.Size = new System.Drawing.Size(82, 17);
            this.lblGIFQuality.TabIndex = 117;
            this.lblGIFQuality.Text = "GIF Quality:";
            // 
            // nudSwitchAfter
            // 
            this.nudSwitchAfter.Location = new System.Drawing.Point(21, 118);
            this.nudSwitchAfter.Margin = new System.Windows.Forms.Padding(4);
            this.nudSwitchAfter.Maximum = new decimal(new int[] {
            1024,
            0,
            0,
            0});
            this.nudSwitchAfter.Name = "nudSwitchAfter";
            this.nudSwitchAfter.Size = new System.Drawing.Size(96, 22);
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
            this.nudImageQuality.Location = new System.Drawing.Point(171, 49);
            this.nudImageQuality.Margin = new System.Windows.Forms.Padding(4);
            this.nudImageQuality.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudImageQuality.Name = "nudImageQuality";
            this.nudImageQuality.Size = new System.Drawing.Size(96, 22);
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
            this.lblJPEGQualityPercentage.Location = new System.Drawing.Point(277, 54);
            this.lblJPEGQualityPercentage.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblJPEGQualityPercentage.Name = "lblJPEGQualityPercentage";
            this.lblJPEGQualityPercentage.Size = new System.Drawing.Size(20, 17);
            this.lblJPEGQualityPercentage.TabIndex = 110;
            this.lblJPEGQualityPercentage.Text = "%";
            // 
            // lblQuality
            // 
            this.lblQuality.AutoSize = true;
            this.lblQuality.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblQuality.Location = new System.Drawing.Point(171, 30);
            this.lblQuality.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblQuality.Name = "lblQuality";
            this.lblQuality.Size = new System.Drawing.Size(96, 17);
            this.lblQuality.TabIndex = 108;
            this.lblQuality.Text = "JPEG Quality:";
            // 
            // cboSwitchFormat
            // 
            this.cboSwitchFormat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboSwitchFormat.FormattingEnabled = true;
            this.cboSwitchFormat.Location = new System.Drawing.Point(171, 118);
            this.cboSwitchFormat.Margin = new System.Windows.Forms.Padding(4);
            this.cboSwitchFormat.Name = "cboSwitchFormat";
            this.cboSwitchFormat.Size = new System.Drawing.Size(129, 24);
            this.cboSwitchFormat.TabIndex = 9;
            this.cboSwitchFormat.SelectedIndexChanged += new System.EventHandler(this.cboSwitchFormat_SelectedIndexChanged);
            // 
            // lblFileFormat
            // 
            this.lblFileFormat.AutoSize = true;
            this.lblFileFormat.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblFileFormat.Location = new System.Drawing.Point(21, 30);
            this.lblFileFormat.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblFileFormat.Name = "lblFileFormat";
            this.lblFileFormat.Size = new System.Drawing.Size(82, 17);
            this.lblFileFormat.TabIndex = 97;
            this.lblFileFormat.Text = "File Format:";
            // 
            // cboFileFormat
            // 
            this.cboFileFormat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboFileFormat.FormattingEnabled = true;
            this.cboFileFormat.Location = new System.Drawing.Point(21, 49);
            this.cboFileFormat.Margin = new System.Windows.Forms.Padding(4);
            this.cboFileFormat.Name = "cboFileFormat";
            this.cboFileFormat.Size = new System.Drawing.Size(129, 24);
            this.cboFileFormat.TabIndex = 6;
            this.cboFileFormat.SelectedIndexChanged += new System.EventHandler(this.cboFileFormat_SelectedIndexChanged);
            // 
            // lblKB
            // 
            this.lblKB.AutoSize = true;
            this.lblKB.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblKB.Location = new System.Drawing.Point(117, 123);
            this.lblKB.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblKB.Name = "lblKB";
            this.lblKB.Size = new System.Drawing.Size(29, 17);
            this.lblKB.TabIndex = 95;
            this.lblKB.Text = "KiB";
            // 
            // lblAfter
            // 
            this.lblAfter.AutoSize = true;
            this.lblAfter.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblAfter.Location = new System.Drawing.Point(21, 98);
            this.lblAfter.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblAfter.Name = "lblAfter";
            this.lblAfter.Size = new System.Drawing.Size(120, 17);
            this.lblAfter.TabIndex = 93;
            this.lblAfter.Text = "After: (0 disables)";
            // 
            // lblSwitchTo
            // 
            this.lblSwitchTo.AutoSize = true;
            this.lblSwitchTo.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblSwitchTo.Location = new System.Drawing.Point(171, 98);
            this.lblSwitchTo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSwitchTo.Name = "lblSwitchTo";
            this.lblSwitchTo.Size = new System.Drawing.Size(68, 17);
            this.lblSwitchTo.TabIndex = 92;
            this.lblSwitchTo.Text = "Switch to:";
            // 
            // tpEditors
            // 
            this.tpEditors.Controls.Add(this.tcEditors);
            this.tpEditors.ImageKey = "picture_edit.png";
            this.tpEditors.Location = new System.Drawing.Point(4, 25);
            this.tpEditors.Margin = new System.Windows.Forms.Padding(4);
            this.tpEditors.Name = "tpEditors";
            this.tpEditors.Padding = new System.Windows.Forms.Padding(4);
            this.tpEditors.Size = new System.Drawing.Size(1075, 541);
            this.tpEditors.TabIndex = 2;
            this.tpEditors.Text = "Editors";
            this.tpEditors.UseVisualStyleBackColor = true;
            // 
            // tcEditors
            // 
            this.tcEditors.Controls.Add(this.tpEditorsImages);
            this.tcEditors.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcEditors.Location = new System.Drawing.Point(4, 4);
            this.tcEditors.Margin = new System.Windows.Forms.Padding(4);
            this.tcEditors.Name = "tcEditors";
            this.tcEditors.SelectedIndex = 0;
            this.tcEditors.Size = new System.Drawing.Size(1068, 532);
            this.tcEditors.TabIndex = 64;
            // 
            // tpEditorsImages
            // 
            this.tpEditorsImages.Controls.Add(this.chkEditorsEnabled);
            this.tpEditorsImages.Controls.Add(this.gbImageEditorSettings);
            this.tpEditorsImages.Controls.Add(this.pgEditorsImage);
            this.tpEditorsImages.Controls.Add(this.btnRemoveImageEditor);
            this.tpEditorsImages.Controls.Add(this.lbSoftware);
            this.tpEditorsImages.Controls.Add(this.btnAddImageSoftware);
            this.tpEditorsImages.Location = new System.Drawing.Point(4, 25);
            this.tpEditorsImages.Margin = new System.Windows.Forms.Padding(4);
            this.tpEditorsImages.Name = "tpEditorsImages";
            this.tpEditorsImages.Padding = new System.Windows.Forms.Padding(4);
            this.tpEditorsImages.Size = new System.Drawing.Size(1060, 503);
            this.tpEditorsImages.TabIndex = 0;
            this.tpEditorsImages.Text = "Image Editors";
            this.tpEditorsImages.UseVisualStyleBackColor = true;
            // 
            // chkEditorsEnabled
            // 
            this.chkEditorsEnabled.AutoSize = true;
            this.chkEditorsEnabled.Checked = true;
            this.chkEditorsEnabled.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkEditorsEnabled.Location = new System.Drawing.Point(864, 14);
            this.chkEditorsEnabled.Margin = new System.Windows.Forms.Padding(4);
            this.chkEditorsEnabled.Name = "chkEditorsEnabled";
            this.chkEditorsEnabled.Size = new System.Drawing.Size(164, 21);
            this.chkEditorsEnabled.TabIndex = 68;
            this.chkEditorsEnabled.Text = "&Enable Image Editors";
            this.chkEditorsEnabled.UseVisualStyleBackColor = true;
            this.chkEditorsEnabled.CheckedChanged += new System.EventHandler(this.ChkEditorsEnableCheckedChanged);
            // 
            // gbImageEditorSettings
            // 
            this.gbImageEditorSettings.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gbImageEditorSettings.Controls.Add(this.chkImageEditorAutoSave);
            this.gbImageEditorSettings.Location = new System.Drawing.Point(395, 256);
            this.gbImageEditorSettings.Margin = new System.Windows.Forms.Padding(4);
            this.gbImageEditorSettings.Name = "gbImageEditorSettings";
            this.gbImageEditorSettings.Padding = new System.Windows.Forms.Padding(4);
            this.gbImageEditorSettings.Size = new System.Drawing.Size(643, 69);
            this.gbImageEditorSettings.TabIndex = 67;
            this.gbImageEditorSettings.TabStop = false;
            this.gbImageEditorSettings.Text = "ZScreen Image Editor Settings";
            // 
            // chkImageEditorAutoSave
            // 
            this.chkImageEditorAutoSave.AutoSize = true;
            this.chkImageEditorAutoSave.Location = new System.Drawing.Point(21, 30);
            this.chkImageEditorAutoSave.Margin = new System.Windows.Forms.Padding(4);
            this.chkImageEditorAutoSave.Name = "chkImageEditorAutoSave";
            this.chkImageEditorAutoSave.Size = new System.Drawing.Size(251, 21);
            this.chkImageEditorAutoSave.TabIndex = 0;
            this.chkImageEditorAutoSave.Text = "&Automatically save changes on Exit";
            this.chkImageEditorAutoSave.UseVisualStyleBackColor = true;
            this.chkImageEditorAutoSave.CheckedChanged += new System.EventHandler(this.chkImageEditorAutoSave_CheckedChanged);
            // 
            // pgEditorsImage
            // 
            this.pgEditorsImage.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pgEditorsImage.Location = new System.Drawing.Point(395, 49);
            this.pgEditorsImage.Margin = new System.Windows.Forms.Padding(4);
            this.pgEditorsImage.Name = "pgEditorsImage";
            this.pgEditorsImage.PropertySort = System.Windows.Forms.PropertySort.NoSort;
            this.pgEditorsImage.Size = new System.Drawing.Size(643, 187);
            this.pgEditorsImage.TabIndex = 64;
            this.pgEditorsImage.ToolbarVisible = false;
            this.pgEditorsImage.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.pgEditorsImage_PropertyValueChanged);
            // 
            // btnRemoveImageEditor
            // 
            this.btnRemoveImageEditor.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnRemoveImageEditor.Location = new System.Drawing.Point(523, 10);
            this.btnRemoveImageEditor.Margin = new System.Windows.Forms.Padding(4);
            this.btnRemoveImageEditor.Name = "btnRemoveImageEditor";
            this.btnRemoveImageEditor.Size = new System.Drawing.Size(117, 30);
            this.btnRemoveImageEditor.TabIndex = 58;
            this.btnRemoveImageEditor.Text = "&Remove";
            this.btnRemoveImageEditor.UseVisualStyleBackColor = true;
            this.btnRemoveImageEditor.Click += new System.EventHandler(this.btnDeleteImageSoftware_Click);
            // 
            // lbSoftware
            // 
            this.lbSoftware.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbSoftware.FormattingEnabled = true;
            this.lbSoftware.IntegralHeight = false;
            this.lbSoftware.Location = new System.Drawing.Point(4, 4);
            this.lbSoftware.Margin = new System.Windows.Forms.Padding(4);
            this.lbSoftware.Name = "lbSoftware";
            this.lbSoftware.Size = new System.Drawing.Size(372, 495);
            this.lbSoftware.TabIndex = 59;
            this.lbSoftware.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.LbSoftwareItemCheck);
            this.lbSoftware.MouseClick += new System.Windows.Forms.MouseEventHandler(this.LbSoftwareMouseClick);
            this.lbSoftware.SelectedIndexChanged += new System.EventHandler(this.lbSoftware_SelectedIndexChanged);
            // 
            // btnAddImageSoftware
            // 
            this.btnAddImageSoftware.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnAddImageSoftware.BackColor = System.Drawing.Color.Transparent;
            this.btnAddImageSoftware.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnAddImageSoftware.Location = new System.Drawing.Point(395, 10);
            this.btnAddImageSoftware.Margin = new System.Windows.Forms.Padding(4);
            this.btnAddImageSoftware.Name = "btnAddImageSoftware";
            this.btnAddImageSoftware.Size = new System.Drawing.Size(117, 30);
            this.btnAddImageSoftware.TabIndex = 59;
            this.btnAddImageSoftware.Text = "Add...";
            this.btnAddImageSoftware.UseVisualStyleBackColor = false;
            this.btnAddImageSoftware.Click += new System.EventHandler(this.btnAddImageSoftware_Click);
            // 
            // tpImageHosting
            // 
            this.tpImageHosting.Controls.Add(this.tcImages);
            this.tpImageHosting.ImageKey = "picture_go.png";
            this.tpImageHosting.Location = new System.Drawing.Point(4, 25);
            this.tpImageHosting.Margin = new System.Windows.Forms.Padding(4);
            this.tpImageHosting.Name = "tpImageHosting";
            this.tpImageHosting.Padding = new System.Windows.Forms.Padding(4);
            this.tpImageHosting.Size = new System.Drawing.Size(1075, 541);
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
            this.tcImages.Location = new System.Drawing.Point(4, 4);
            this.tcImages.Margin = new System.Windows.Forms.Padding(4);
            this.tcImages.Name = "tcImages";
            this.tcImages.SelectedIndex = 0;
            this.tcImages.Size = new System.Drawing.Size(1068, 532);
            this.tcImages.TabIndex = 5;
            // 
            // tpImageUploaders
            // 
            this.tpImageUploaders.Controls.Add(this.gbImageUploadRetry);
            this.tpImageUploaders.Controls.Add(this.gbImageUploaderOptions);
            this.tpImageUploaders.Location = new System.Drawing.Point(4, 25);
            this.tpImageUploaders.Margin = new System.Windows.Forms.Padding(4);
            this.tpImageUploaders.Name = "tpImageUploaders";
            this.tpImageUploaders.Padding = new System.Windows.Forms.Padding(4);
            this.tpImageUploaders.Size = new System.Drawing.Size(1060, 503);
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
            this.gbImageUploadRetry.Location = new System.Drawing.Point(11, 148);
            this.gbImageUploadRetry.Margin = new System.Windows.Forms.Padding(4);
            this.gbImageUploadRetry.Name = "gbImageUploadRetry";
            this.gbImageUploadRetry.Padding = new System.Windows.Forms.Padding(4);
            this.gbImageUploadRetry.Size = new System.Drawing.Size(1035, 158);
            this.gbImageUploadRetry.TabIndex = 8;
            this.gbImageUploadRetry.TabStop = false;
            this.gbImageUploadRetry.Text = "Retry Options";
            // 
            // chkImageUploadRandomRetryOnFail
            // 
            this.chkImageUploadRandomRetryOnFail.AutoSize = true;
            this.chkImageUploadRandomRetryOnFail.Location = new System.Drawing.Point(43, 89);
            this.chkImageUploadRandomRetryOnFail.Margin = new System.Windows.Forms.Padding(4);
            this.chkImageUploadRandomRetryOnFail.Name = "chkImageUploadRandomRetryOnFail";
            this.chkImageUploadRandomRetryOnFail.Size = new System.Drawing.Size(252, 21);
            this.chkImageUploadRandomRetryOnFail.TabIndex = 12;
            this.chkImageUploadRandomRetryOnFail.Text = "Randomly select a valid destination";
            this.chkImageUploadRandomRetryOnFail.UseVisualStyleBackColor = true;
            this.chkImageUploadRandomRetryOnFail.CheckedChanged += new System.EventHandler(this.chkImageUploadRandomRetryOnFail_CheckedChanged);
            // 
            // lblErrorRetry
            // 
            this.lblErrorRetry.AutoSize = true;
            this.lblErrorRetry.Location = new System.Drawing.Point(21, 30);
            this.lblErrorRetry.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblErrorRetry.Name = "lblErrorRetry";
            this.lblErrorRetry.Size = new System.Drawing.Size(127, 17);
            this.lblErrorRetry.TabIndex = 11;
            this.lblErrorRetry.Text = "Number of Retries:";
            // 
            // lblUploadDurationLimit
            // 
            this.lblUploadDurationLimit.AutoSize = true;
            this.lblUploadDurationLimit.Location = new System.Drawing.Point(491, 118);
            this.lblUploadDurationLimit.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblUploadDurationLimit.Name = "lblUploadDurationLimit";
            this.lblUploadDurationLimit.Size = new System.Drawing.Size(81, 17);
            this.lblUploadDurationLimit.TabIndex = 10;
            this.lblUploadDurationLimit.Text = "miliseconds";
            // 
            // chkImageUploadRetryOnFail
            // 
            this.chkImageUploadRetryOnFail.AutoSize = true;
            this.chkImageUploadRetryOnFail.Location = new System.Drawing.Point(21, 59);
            this.chkImageUploadRetryOnFail.Margin = new System.Windows.Forms.Padding(4);
            this.chkImageUploadRetryOnFail.Name = "chkImageUploadRetryOnFail";
            this.chkImageUploadRetryOnFail.Size = new System.Drawing.Size(519, 21);
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
            this.cboImageUploadRetryOnTimeout.Location = new System.Drawing.Point(21, 118);
            this.cboImageUploadRetryOnTimeout.Margin = new System.Windows.Forms.Padding(4);
            this.cboImageUploadRetryOnTimeout.Name = "cboImageUploadRetryOnTimeout";
            this.cboImageUploadRetryOnTimeout.Size = new System.Drawing.Size(373, 21);
            this.cboImageUploadRetryOnTimeout.TabIndex = 8;
            this.cboImageUploadRetryOnTimeout.Text = "Change the Image Uploader if the upload times out by ";
            this.ttZScreen.SetToolTip(this.cboImageUploadRetryOnTimeout, "This setting ignores Retry Count. Only happens between ImageShack and TinyPic.");
            this.cboImageUploadRetryOnTimeout.UseVisualStyleBackColor = true;
            this.cboImageUploadRetryOnTimeout.CheckedChanged += new System.EventHandler(this.cbAutoChangeUploadDestination_CheckedChanged);
            // 
            // nudUploadDurationLimit
            // 
            this.nudUploadDurationLimit.Location = new System.Drawing.Point(399, 116);
            this.nudUploadDurationLimit.Margin = new System.Windows.Forms.Padding(4);
            this.nudUploadDurationLimit.Maximum = new decimal(new int[] {
            300000,
            0,
            0,
            0});
            this.nudUploadDurationLimit.Name = "nudUploadDurationLimit";
            this.nudUploadDurationLimit.Size = new System.Drawing.Size(85, 22);
            this.nudUploadDurationLimit.TabIndex = 9;
            this.nudUploadDurationLimit.ValueChanged += new System.EventHandler(this.nudUploadDurationLimit_ValueChanged);
            // 
            // nudErrorRetry
            // 
            this.nudErrorRetry.Location = new System.Drawing.Point(160, 27);
            this.nudErrorRetry.Margin = new System.Windows.Forms.Padding(4);
            this.nudErrorRetry.Name = "nudErrorRetry";
            this.nudErrorRetry.Size = new System.Drawing.Size(53, 22);
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
            this.gbImageUploaderOptions.Controls.Add(this.cbAddFailedScreenshot);
            this.gbImageUploaderOptions.Location = new System.Drawing.Point(11, 10);
            this.gbImageUploaderOptions.Margin = new System.Windows.Forms.Padding(4);
            this.gbImageUploaderOptions.Name = "gbImageUploaderOptions";
            this.gbImageUploaderOptions.Padding = new System.Windows.Forms.Padding(4);
            this.gbImageUploaderOptions.Size = new System.Drawing.Size(1035, 128);
            this.gbImageUploaderOptions.TabIndex = 7;
            this.gbImageUploaderOptions.TabStop = false;
            this.gbImageUploaderOptions.Text = "General Options";
            // 
            // chkAutoSwitchFileUploader
            // 
            this.chkAutoSwitchFileUploader.AutoSize = true;
            this.chkAutoSwitchFileUploader.Location = new System.Drawing.Point(21, 89);
            this.chkAutoSwitchFileUploader.Margin = new System.Windows.Forms.Padding(4);
            this.chkAutoSwitchFileUploader.Name = "chkAutoSwitchFileUploader";
            this.chkAutoSwitchFileUploader.Size = new System.Drawing.Size(622, 21);
            this.chkAutoSwitchFileUploader.TabIndex = 114;
            this.chkAutoSwitchFileUploader.Text = "Automatically switch to File Uploader if a user copies (Clipboard Upload) or drag" +
                "s a non-Image";
            this.chkAutoSwitchFileUploader.UseVisualStyleBackColor = true;
            this.chkAutoSwitchFileUploader.CheckedChanged += new System.EventHandler(this.chkAutoSwitchFTP_CheckedChanged);
            // 
            // cbTinyPicSizeCheck
            // 
            this.cbTinyPicSizeCheck.AutoSize = true;
            this.cbTinyPicSizeCheck.Location = new System.Drawing.Point(21, 59);
            this.cbTinyPicSizeCheck.Margin = new System.Windows.Forms.Padding(4);
            this.cbTinyPicSizeCheck.Name = "cbTinyPicSizeCheck";
            this.cbTinyPicSizeCheck.Size = new System.Drawing.Size(583, 21);
            this.cbTinyPicSizeCheck.TabIndex = 7;
            this.cbTinyPicSizeCheck.Text = "Switch from TinyPic to ImageShack if the image dimensions are greater than 1600 p" +
                "ixels";
            this.cbTinyPicSizeCheck.UseVisualStyleBackColor = true;
            this.cbTinyPicSizeCheck.CheckedChanged += new System.EventHandler(this.cbTinyPicSizeCheck_CheckedChanged);
            // 
            // cbAddFailedScreenshot
            // 
            this.cbAddFailedScreenshot.AutoSize = true;
            this.cbAddFailedScreenshot.Location = new System.Drawing.Point(21, 30);
            this.cbAddFailedScreenshot.Margin = new System.Windows.Forms.Padding(4);
            this.cbAddFailedScreenshot.Name = "cbAddFailedScreenshot";
            this.cbAddFailedScreenshot.Size = new System.Drawing.Size(187, 21);
            this.cbAddFailedScreenshot.TabIndex = 7;
            this.cbAddFailedScreenshot.Text = "Add failed task to History";
            this.cbAddFailedScreenshot.UseVisualStyleBackColor = true;
            this.cbAddFailedScreenshot.CheckedChanged += new System.EventHandler(this.cbAddFailedScreenshot_CheckedChanged);
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
            this.tpCustomUploaders.Location = new System.Drawing.Point(4, 25);
            this.tpCustomUploaders.Margin = new System.Windows.Forms.Padding(4);
            this.tpCustomUploaders.Name = "tpCustomUploaders";
            this.tpCustomUploaders.Padding = new System.Windows.Forms.Padding(4);
            this.tpCustomUploaders.Size = new System.Drawing.Size(1060, 503);
            this.tpCustomUploaders.TabIndex = 11;
            this.tpCustomUploaders.Text = "Custom Image Uploaders";
            this.tpCustomUploaders.UseVisualStyleBackColor = true;
            // 
            // txtUploadersLog
            // 
            this.txtUploadersLog.Location = new System.Drawing.Point(11, 345);
            this.txtUploadersLog.Margin = new System.Windows.Forms.Padding(4);
            this.txtUploadersLog.Name = "txtUploadersLog";
            this.txtUploadersLog.Size = new System.Drawing.Size(564, 127);
            this.txtUploadersLog.TabIndex = 18;
            this.txtUploadersLog.Text = "";
            this.txtUploadersLog.LinkClicked += new System.Windows.Forms.LinkClickedEventHandler(this.txtUploadersLog_LinkClicked);
            // 
            // btnUploadersTest
            // 
            this.btnUploadersTest.Location = new System.Drawing.Point(597, 443);
            this.btnUploadersTest.Margin = new System.Windows.Forms.Padding(4);
            this.btnUploadersTest.Name = "btnUploadersTest";
            this.btnUploadersTest.Size = new System.Drawing.Size(437, 30);
            this.btnUploadersTest.TabIndex = 9;
            this.btnUploadersTest.Text = "Test Upload";
            this.btnUploadersTest.UseVisualStyleBackColor = true;
            this.btnUploadersTest.Click += new System.EventHandler(this.btUploadersTest_Click);
            // 
            // txtFullImage
            // 
            this.txtFullImage.Location = new System.Drawing.Point(597, 364);
            this.txtFullImage.Margin = new System.Windows.Forms.Padding(4);
            this.txtFullImage.Name = "txtFullImage";
            this.txtFullImage.Size = new System.Drawing.Size(436, 22);
            this.txtFullImage.TabIndex = 5;
            // 
            // txtThumbnail
            // 
            this.txtThumbnail.Location = new System.Drawing.Point(597, 414);
            this.txtThumbnail.Margin = new System.Windows.Forms.Padding(4);
            this.txtThumbnail.Name = "txtThumbnail";
            this.txtThumbnail.Size = new System.Drawing.Size(436, 22);
            this.txtThumbnail.TabIndex = 6;
            // 
            // lblFullImage
            // 
            this.lblFullImage.AutoSize = true;
            this.lblFullImage.Location = new System.Drawing.Point(597, 345);
            this.lblFullImage.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblFullImage.Name = "lblFullImage";
            this.lblFullImage.Size = new System.Drawing.Size(72, 17);
            this.lblFullImage.TabIndex = 17;
            this.lblFullImage.Text = "Full Image";
            // 
            // lblThumbnail
            // 
            this.lblThumbnail.AutoSize = true;
            this.lblThumbnail.Location = new System.Drawing.Point(597, 394);
            this.lblThumbnail.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblThumbnail.Name = "lblThumbnail";
            this.lblThumbnail.Size = new System.Drawing.Size(74, 17);
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
            this.gbImageUploaders.Location = new System.Drawing.Point(11, 10);
            this.gbImageUploaders.Margin = new System.Windows.Forms.Padding(4);
            this.gbImageUploaders.Name = "gbImageUploaders";
            this.gbImageUploaders.Padding = new System.Windows.Forms.Padding(4);
            this.gbImageUploaders.Size = new System.Drawing.Size(331, 325);
            this.gbImageUploaders.TabIndex = 0;
            this.gbImageUploaders.TabStop = false;
            this.gbImageUploaders.Text = "Image Hosting Services";
            // 
            // lbImageUploader
            // 
            this.lbImageUploader.FormattingEnabled = true;
            this.lbImageUploader.ItemHeight = 16;
            this.lbImageUploader.Location = new System.Drawing.Point(11, 89);
            this.lbImageUploader.Margin = new System.Windows.Forms.Padding(4);
            this.lbImageUploader.Name = "lbImageUploader";
            this.lbImageUploader.Size = new System.Drawing.Size(308, 180);
            this.lbImageUploader.TabIndex = 3;
            this.lbImageUploader.SelectedIndexChanged += new System.EventHandler(this.lbImageUploader_SelectedIndexChanged);
            // 
            // btnUploadersClear
            // 
            this.btnUploadersClear.Location = new System.Drawing.Point(224, 286);
            this.btnUploadersClear.Margin = new System.Windows.Forms.Padding(4);
            this.btnUploadersClear.Name = "btnUploadersClear";
            this.btnUploadersClear.Size = new System.Drawing.Size(96, 30);
            this.btnUploadersClear.TabIndex = 8;
            this.btnUploadersClear.Text = "Clear";
            this.btnUploadersClear.UseVisualStyleBackColor = true;
            this.btnUploadersClear.Click += new System.EventHandler(this.btnUploadersClear_Click);
            // 
            // btnUploaderExport
            // 
            this.btnUploaderExport.Location = new System.Drawing.Point(117, 286);
            this.btnUploaderExport.Margin = new System.Windows.Forms.Padding(4);
            this.btnUploaderExport.Name = "btnUploaderExport";
            this.btnUploaderExport.Size = new System.Drawing.Size(96, 30);
            this.btnUploaderExport.TabIndex = 5;
            this.btnUploaderExport.Text = "Export...";
            this.btnUploaderExport.UseVisualStyleBackColor = true;
            this.btnUploaderExport.Click += new System.EventHandler(this.btnUploaderExport_Click);
            // 
            // btnUploaderRemove
            // 
            this.btnUploaderRemove.Location = new System.Drawing.Point(117, 49);
            this.btnUploaderRemove.Margin = new System.Windows.Forms.Padding(4);
            this.btnUploaderRemove.Name = "btnUploaderRemove";
            this.btnUploaderRemove.Size = new System.Drawing.Size(96, 30);
            this.btnUploaderRemove.TabIndex = 2;
            this.btnUploaderRemove.Text = "Remove";
            this.btnUploaderRemove.UseVisualStyleBackColor = true;
            this.btnUploaderRemove.Click += new System.EventHandler(this.btnUploaderRemove_Click);
            // 
            // btnUploaderImport
            // 
            this.btnUploaderImport.Location = new System.Drawing.Point(11, 286);
            this.btnUploaderImport.Margin = new System.Windows.Forms.Padding(4);
            this.btnUploaderImport.Name = "btnUploaderImport";
            this.btnUploaderImport.Size = new System.Drawing.Size(96, 30);
            this.btnUploaderImport.TabIndex = 4;
            this.btnUploaderImport.Text = "Import...";
            this.btnUploaderImport.UseVisualStyleBackColor = true;
            this.btnUploaderImport.Click += new System.EventHandler(this.btnUploaderImport_Click);
            // 
            // btnUploaderUpdate
            // 
            this.btnUploaderUpdate.Location = new System.Drawing.Point(224, 49);
            this.btnUploaderUpdate.Margin = new System.Windows.Forms.Padding(4);
            this.btnUploaderUpdate.Name = "btnUploaderUpdate";
            this.btnUploaderUpdate.Size = new System.Drawing.Size(96, 30);
            this.btnUploaderUpdate.TabIndex = 7;
            this.btnUploaderUpdate.Text = "Update";
            this.btnUploaderUpdate.UseVisualStyleBackColor = true;
            this.btnUploaderUpdate.Click += new System.EventHandler(this.btnUploadersUpdate_Click);
            // 
            // txtUploader
            // 
            this.txtUploader.Location = new System.Drawing.Point(11, 20);
            this.txtUploader.Margin = new System.Windows.Forms.Padding(4);
            this.txtUploader.Name = "txtUploader";
            this.txtUploader.Size = new System.Drawing.Size(308, 22);
            this.txtUploader.TabIndex = 0;
            // 
            // btnUploaderAdd
            // 
            this.btnUploaderAdd.Location = new System.Drawing.Point(11, 49);
            this.btnUploaderAdd.Margin = new System.Windows.Forms.Padding(4);
            this.btnUploaderAdd.Name = "btnUploaderAdd";
            this.btnUploaderAdd.Size = new System.Drawing.Size(96, 30);
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
            this.gbRegexp.Location = new System.Drawing.Point(363, 108);
            this.gbRegexp.Margin = new System.Windows.Forms.Padding(4);
            this.gbRegexp.Name = "gbRegexp";
            this.gbRegexp.Padding = new System.Windows.Forms.Padding(4);
            this.gbRegexp.Size = new System.Drawing.Size(320, 226);
            this.gbRegexp.TabIndex = 4;
            this.gbRegexp.TabStop = false;
            this.gbRegexp.Text = "Regexp from Source";
            // 
            // btnRegexpEdit
            // 
            this.btnRegexpEdit.Location = new System.Drawing.Point(213, 49);
            this.btnRegexpEdit.Margin = new System.Windows.Forms.Padding(4);
            this.btnRegexpEdit.Name = "btnRegexpEdit";
            this.btnRegexpEdit.Size = new System.Drawing.Size(96, 30);
            this.btnRegexpEdit.TabIndex = 4;
            this.btnRegexpEdit.Text = "Edit";
            this.btnRegexpEdit.UseVisualStyleBackColor = true;
            this.btnRegexpEdit.Click += new System.EventHandler(this.btnRegexpEdit_Click);
            // 
            // txtRegexp
            // 
            this.txtRegexp.Location = new System.Drawing.Point(11, 20);
            this.txtRegexp.Margin = new System.Windows.Forms.Padding(4);
            this.txtRegexp.Name = "txtRegexp";
            this.txtRegexp.Size = new System.Drawing.Size(297, 22);
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
            this.lvRegexps.Location = new System.Drawing.Point(11, 89);
            this.lvRegexps.Margin = new System.Windows.Forms.Padding(4);
            this.lvRegexps.MultiSelect = false;
            this.lvRegexps.Name = "lvRegexps";
            this.lvRegexps.Scrollable = false;
            this.lvRegexps.Size = new System.Drawing.Size(300, 127);
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
            this.btnRegexpRemove.Location = new System.Drawing.Point(112, 49);
            this.btnRegexpRemove.Margin = new System.Windows.Forms.Padding(4);
            this.btnRegexpRemove.Name = "btnRegexpRemove";
            this.btnRegexpRemove.Size = new System.Drawing.Size(96, 30);
            this.btnRegexpRemove.TabIndex = 2;
            this.btnRegexpRemove.Text = "Remove";
            this.btnRegexpRemove.UseVisualStyleBackColor = true;
            this.btnRegexpRemove.Click += new System.EventHandler(this.btnRegexpRemove_Click);
            // 
            // btnRegexpAdd
            // 
            this.btnRegexpAdd.Location = new System.Drawing.Point(11, 49);
            this.btnRegexpAdd.Margin = new System.Windows.Forms.Padding(4);
            this.btnRegexpAdd.Name = "btnRegexpAdd";
            this.btnRegexpAdd.Size = new System.Drawing.Size(96, 30);
            this.btnRegexpAdd.TabIndex = 1;
            this.btnRegexpAdd.Text = "Add";
            this.btnRegexpAdd.UseVisualStyleBackColor = true;
            this.btnRegexpAdd.Click += new System.EventHandler(this.btnRegexpAdd_Click);
            // 
            // txtFileForm
            // 
            this.txtFileForm.Location = new System.Drawing.Point(373, 79);
            this.txtFileForm.Margin = new System.Windows.Forms.Padding(4);
            this.txtFileForm.Name = "txtFileForm";
            this.txtFileForm.Size = new System.Drawing.Size(297, 22);
            this.txtFileForm.TabIndex = 3;
            // 
            // lblFileForm
            // 
            this.lblFileForm.AutoSize = true;
            this.lblFileForm.Location = new System.Drawing.Point(373, 59);
            this.lblFileForm.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblFileForm.Name = "lblFileForm";
            this.lblFileForm.Size = new System.Drawing.Size(111, 17);
            this.lblFileForm.TabIndex = 9;
            this.lblFileForm.Text = "File Form Name:";
            // 
            // lblUploadURL
            // 
            this.lblUploadURL.AutoSize = true;
            this.lblUploadURL.Location = new System.Drawing.Point(373, 10);
            this.lblUploadURL.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblUploadURL.Name = "lblUploadURL";
            this.lblUploadURL.Size = new System.Drawing.Size(89, 17);
            this.lblUploadURL.TabIndex = 8;
            this.lblUploadURL.Text = "Upload URL:";
            // 
            // txtUploadURL
            // 
            this.txtUploadURL.Location = new System.Drawing.Point(373, 30);
            this.txtUploadURL.Margin = new System.Windows.Forms.Padding(4);
            this.txtUploadURL.Name = "txtUploadURL";
            this.txtUploadURL.Size = new System.Drawing.Size(297, 22);
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
            this.gbArguments.Location = new System.Drawing.Point(704, 10);
            this.gbArguments.Margin = new System.Windows.Forms.Padding(4);
            this.gbArguments.Name = "gbArguments";
            this.gbArguments.Padding = new System.Windows.Forms.Padding(4);
            this.gbArguments.Size = new System.Drawing.Size(331, 325);
            this.gbArguments.TabIndex = 1;
            this.gbArguments.TabStop = false;
            this.gbArguments.Text = "Arguments";
            // 
            // btnArgEdit
            // 
            this.btnArgEdit.Location = new System.Drawing.Point(224, 49);
            this.btnArgEdit.Margin = new System.Windows.Forms.Padding(4);
            this.btnArgEdit.Name = "btnArgEdit";
            this.btnArgEdit.Size = new System.Drawing.Size(96, 30);
            this.btnArgEdit.TabIndex = 5;
            this.btnArgEdit.Text = "Edit";
            this.btnArgEdit.UseVisualStyleBackColor = true;
            this.btnArgEdit.Click += new System.EventHandler(this.btnArgEdit_Click);
            // 
            // txtArg2
            // 
            this.txtArg2.Location = new System.Drawing.Point(171, 20);
            this.txtArg2.Margin = new System.Windows.Forms.Padding(4);
            this.txtArg2.Name = "txtArg2";
            this.txtArg2.Size = new System.Drawing.Size(148, 22);
            this.txtArg2.TabIndex = 1;
            // 
            // btnArgRemove
            // 
            this.btnArgRemove.Location = new System.Drawing.Point(117, 49);
            this.btnArgRemove.Margin = new System.Windows.Forms.Padding(4);
            this.btnArgRemove.Name = "btnArgRemove";
            this.btnArgRemove.Size = new System.Drawing.Size(96, 30);
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
            this.lvArguments.Location = new System.Drawing.Point(11, 89);
            this.lvArguments.Margin = new System.Windows.Forms.Padding(4);
            this.lvArguments.MultiSelect = false;
            this.lvArguments.Name = "lvArguments";
            this.lvArguments.Size = new System.Drawing.Size(308, 226);
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
            this.btnArgAdd.Location = new System.Drawing.Point(11, 49);
            this.btnArgAdd.Margin = new System.Windows.Forms.Padding(4);
            this.btnArgAdd.Name = "btnArgAdd";
            this.btnArgAdd.Size = new System.Drawing.Size(96, 30);
            this.btnArgAdd.TabIndex = 2;
            this.btnArgAdd.Text = "Add";
            this.btnArgAdd.UseVisualStyleBackColor = true;
            this.btnArgAdd.Click += new System.EventHandler(this.btnArgAdd_Click);
            // 
            // txtArg1
            // 
            this.txtArg1.Location = new System.Drawing.Point(11, 20);
            this.txtArg1.Margin = new System.Windows.Forms.Padding(4);
            this.txtArg1.Name = "txtArg1";
            this.txtArg1.Size = new System.Drawing.Size(148, 22);
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
            this.tpWebPageUpload.Location = new System.Drawing.Point(4, 25);
            this.tpWebPageUpload.Margin = new System.Windows.Forms.Padding(4);
            this.tpWebPageUpload.Name = "tpWebPageUpload";
            this.tpWebPageUpload.Padding = new System.Windows.Forms.Padding(4);
            this.tpWebPageUpload.Size = new System.Drawing.Size(1060, 503);
            this.tpWebPageUpload.TabIndex = 12;
            this.tpWebPageUpload.Text = "Webpage Uploader";
            this.tpWebPageUpload.UseVisualStyleBackColor = true;
            // 
            // cbWebPageAutoUpload
            // 
            this.cbWebPageAutoUpload.AutoSize = true;
            this.cbWebPageAutoUpload.Location = new System.Drawing.Point(789, 59);
            this.cbWebPageAutoUpload.Margin = new System.Windows.Forms.Padding(4);
            this.cbWebPageAutoUpload.Name = "cbWebPageAutoUpload";
            this.cbWebPageAutoUpload.Size = new System.Drawing.Size(106, 21);
            this.cbWebPageAutoUpload.TabIndex = 8;
            this.cbWebPageAutoUpload.Text = "Auto upload";
            this.cbWebPageAutoUpload.UseVisualStyleBackColor = true;
            this.cbWebPageAutoUpload.CheckedChanged += new System.EventHandler(this.cbWebPageAutoUpload_CheckedChanged);
            // 
            // lblWebPageHeight
            // 
            this.lblWebPageHeight.AutoSize = true;
            this.lblWebPageHeight.Location = new System.Drawing.Point(341, 59);
            this.lblWebPageHeight.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblWebPageHeight.Name = "lblWebPageHeight";
            this.lblWebPageHeight.Size = new System.Drawing.Size(53, 17);
            this.lblWebPageHeight.TabIndex = 7;
            this.lblWebPageHeight.Text = "Height:";
            // 
            // lblWebPageWidth
            // 
            this.lblWebPageWidth.AutoSize = true;
            this.lblWebPageWidth.Location = new System.Drawing.Point(224, 59);
            this.lblWebPageWidth.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblWebPageWidth.Name = "lblWebPageWidth";
            this.lblWebPageWidth.Size = new System.Drawing.Size(48, 17);
            this.lblWebPageWidth.TabIndex = 6;
            this.lblWebPageWidth.Text = "Width:";
            // 
            // txtWebPageHeight
            // 
            this.txtWebPageHeight.Location = new System.Drawing.Point(405, 53);
            this.txtWebPageHeight.Margin = new System.Windows.Forms.Padding(4);
            this.txtWebPageHeight.Name = "txtWebPageHeight";
            this.txtWebPageHeight.Size = new System.Drawing.Size(52, 22);
            this.txtWebPageHeight.TabIndex = 5;
            this.txtWebPageHeight.TextChanged += new System.EventHandler(this.txtWebPageHeight_TextChanged);
            // 
            // txtWebPageWidth
            // 
            this.txtWebPageWidth.Location = new System.Drawing.Point(277, 53);
            this.txtWebPageWidth.Margin = new System.Windows.Forms.Padding(4);
            this.txtWebPageWidth.Name = "txtWebPageWidth";
            this.txtWebPageWidth.Size = new System.Drawing.Size(52, 22);
            this.txtWebPageWidth.TabIndex = 4;
            this.txtWebPageWidth.TextChanged += new System.EventHandler(this.txtWebPageWidth_TextChanged);
            // 
            // cbWebPageUseCustomSize
            // 
            this.cbWebPageUseCustomSize.AutoSize = true;
            this.cbWebPageUseCustomSize.Location = new System.Drawing.Point(21, 59);
            this.cbWebPageUseCustomSize.Margin = new System.Windows.Forms.Padding(4);
            this.cbWebPageUseCustomSize.Name = "cbWebPageUseCustomSize";
            this.cbWebPageUseCustomSize.Size = new System.Drawing.Size(191, 21);
            this.cbWebPageUseCustomSize.TabIndex = 3;
            this.cbWebPageUseCustomSize.Text = "Use custom browser size:";
            this.ttZScreen.SetToolTip(this.cbWebPageUseCustomSize, "Default size is primary monitor size");
            this.cbWebPageUseCustomSize.UseVisualStyleBackColor = true;
            this.cbWebPageUseCustomSize.CheckedChanged += new System.EventHandler(this.cbWebPageUseCustomSize_CheckedChanged);
            // 
            // btnWebPageImageUpload
            // 
            this.btnWebPageImageUpload.Enabled = false;
            this.btnWebPageImageUpload.Location = new System.Drawing.Point(907, 49);
            this.btnWebPageImageUpload.Margin = new System.Windows.Forms.Padding(4);
            this.btnWebPageImageUpload.Name = "btnWebPageImageUpload";
            this.btnWebPageImageUpload.Size = new System.Drawing.Size(128, 30);
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
            this.pWebPageImage.Location = new System.Drawing.Point(21, 89);
            this.pWebPageImage.Margin = new System.Windows.Forms.Padding(4);
            this.pWebPageImage.Name = "pWebPageImage";
            this.pWebPageImage.Size = new System.Drawing.Size(1016, 386);
            this.pWebPageImage.TabIndex = 2;
            // 
            // pbWebPageImage
            // 
            this.pbWebPageImage.BackColor = System.Drawing.Color.White;
            this.pbWebPageImage.Location = new System.Drawing.Point(0, 0);
            this.pbWebPageImage.Margin = new System.Windows.Forms.Padding(4);
            this.pbWebPageImage.Name = "pbWebPageImage";
            this.pbWebPageImage.Size = new System.Drawing.Size(100, 50);
            this.pbWebPageImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pbWebPageImage.TabIndex = 0;
            this.pbWebPageImage.TabStop = false;
            // 
            // btnWebPageCaptureImage
            // 
            this.btnWebPageCaptureImage.Location = new System.Drawing.Point(907, 16);
            this.btnWebPageCaptureImage.Margin = new System.Windows.Forms.Padding(4);
            this.btnWebPageCaptureImage.Name = "btnWebPageCaptureImage";
            this.btnWebPageCaptureImage.Size = new System.Drawing.Size(128, 30);
            this.btnWebPageCaptureImage.TabIndex = 1;
            this.btnWebPageCaptureImage.Text = "Capture Image";
            this.btnWebPageCaptureImage.UseVisualStyleBackColor = true;
            this.btnWebPageCaptureImage.Click += new System.EventHandler(this.btnWebPageUploadImage_Click);
            // 
            // txtWebPageURL
            // 
            this.txtWebPageURL.Location = new System.Drawing.Point(21, 20);
            this.txtWebPageURL.Margin = new System.Windows.Forms.Padding(4);
            this.txtWebPageURL.Name = "txtWebPageURL";
            this.txtWebPageURL.Size = new System.Drawing.Size(873, 22);
            this.txtWebPageURL.TabIndex = 0;
            // 
            // tpTextServices
            // 
            this.tpTextServices.Controls.Add(this.tcTextUploaders);
            this.tpTextServices.ImageKey = "text_signature.png";
            this.tpTextServices.Location = new System.Drawing.Point(4, 25);
            this.tpTextServices.Margin = new System.Windows.Forms.Padding(4);
            this.tpTextServices.Name = "tpTextServices";
            this.tpTextServices.Padding = new System.Windows.Forms.Padding(4);
            this.tpTextServices.Size = new System.Drawing.Size(1075, 541);
            this.tpTextServices.TabIndex = 13;
            this.tpTextServices.Text = "Text Services";
            this.tpTextServices.UseVisualStyleBackColor = true;
            // 
            // tcTextUploaders
            // 
            this.tcTextUploaders.Controls.Add(this.tpTreeGUI);
            this.tcTextUploaders.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcTextUploaders.Location = new System.Drawing.Point(4, 4);
            this.tcTextUploaders.Margin = new System.Windows.Forms.Padding(4);
            this.tcTextUploaders.Name = "tcTextUploaders";
            this.tcTextUploaders.SelectedIndex = 0;
            this.tcTextUploaders.Size = new System.Drawing.Size(1068, 532);
            this.tcTextUploaders.TabIndex = 0;
            // 
            // tpTreeGUI
            // 
            this.tpTreeGUI.Controls.Add(this.pgIndexer);
            this.tpTreeGUI.Location = new System.Drawing.Point(4, 25);
            this.tpTreeGUI.Margin = new System.Windows.Forms.Padding(4);
            this.tpTreeGUI.Name = "tpTreeGUI";
            this.tpTreeGUI.Padding = new System.Windows.Forms.Padding(4);
            this.tpTreeGUI.Size = new System.Drawing.Size(1060, 503);
            this.tpTreeGUI.TabIndex = 15;
            this.tpTreeGUI.Text = "Directory Indexer";
            this.tpTreeGUI.UseVisualStyleBackColor = true;
            // 
            // pgIndexer
            // 
            this.pgIndexer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pgIndexer.Location = new System.Drawing.Point(4, 4);
            this.pgIndexer.Margin = new System.Windows.Forms.Padding(4);
            this.pgIndexer.Name = "pgIndexer";
            this.pgIndexer.Size = new System.Drawing.Size(1052, 495);
            this.pgIndexer.TabIndex = 0;
            // 
            // tpTranslator
            // 
            this.tpTranslator.Controls.Add(this.lvDictionary);
            this.tpTranslator.Controls.Add(this.txtAutoTranslate);
            this.tpTranslator.Controls.Add(this.cbAutoTranslate);
            this.tpTranslator.Controls.Add(this.btnTranslateTo1);
            this.tpTranslator.Controls.Add(this.lblDictionary);
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
            this.tpTranslator.Location = new System.Drawing.Point(4, 25);
            this.tpTranslator.Margin = new System.Windows.Forms.Padding(4);
            this.tpTranslator.Name = "tpTranslator";
            this.tpTranslator.Padding = new System.Windows.Forms.Padding(4);
            this.tpTranslator.Size = new System.Drawing.Size(1075, 541);
            this.tpTranslator.TabIndex = 1;
            this.tpTranslator.Text = "Translator";
            this.tpTranslator.UseVisualStyleBackColor = true;
            // 
            // lvDictionary
            // 
            this.lvDictionary.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lvDictionary.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader3});
            this.lvDictionary.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lvDictionary.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.lvDictionary.Location = new System.Drawing.Point(491, 59);
            this.lvDictionary.Margin = new System.Windows.Forms.Padding(4);
            this.lvDictionary.Name = "lvDictionary";
            this.lvDictionary.Size = new System.Drawing.Size(553, 373);
            this.lvDictionary.TabIndex = 12;
            this.lvDictionary.UseCompatibleStateImageBehavior = false;
            this.lvDictionary.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Width = 412;
            // 
            // txtAutoTranslate
            // 
            this.txtAutoTranslate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtAutoTranslate.Location = new System.Drawing.Point(587, 482);
            this.txtAutoTranslate.Margin = new System.Windows.Forms.Padding(4);
            this.txtAutoTranslate.Name = "txtAutoTranslate";
            this.txtAutoTranslate.Size = new System.Drawing.Size(73, 22);
            this.txtAutoTranslate.TabIndex = 11;
            this.txtAutoTranslate.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtAutoTranslate.TextChanged += new System.EventHandler(this.txtAutoTranslate_TextChanged);
            // 
            // cbAutoTranslate
            // 
            this.cbAutoTranslate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cbAutoTranslate.AutoSize = true;
            this.cbAutoTranslate.Location = new System.Drawing.Point(21, 485);
            this.cbAutoTranslate.Margin = new System.Windows.Forms.Padding(4);
            this.cbAutoTranslate.Name = "cbAutoTranslate";
            this.cbAutoTranslate.Size = new System.Drawing.Size(559, 21);
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
            this.btnTranslateTo1.Location = new System.Drawing.Point(288, 217);
            this.btnTranslateTo1.Margin = new System.Windows.Forms.Padding(4);
            this.btnTranslateTo1.Name = "btnTranslateTo1";
            this.btnTranslateTo1.Size = new System.Drawing.Size(181, 30);
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
            this.lblDictionary.Location = new System.Drawing.Point(491, 30);
            this.lblDictionary.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblDictionary.Name = "lblDictionary";
            this.lblDictionary.Size = new System.Drawing.Size(71, 17);
            this.lblDictionary.TabIndex = 8;
            this.lblDictionary.Text = "Dictionary";
            // 
            // cbClipboardTranslate
            // 
            this.cbClipboardTranslate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cbClipboardTranslate.AutoSize = true;
            this.cbClipboardTranslate.Location = new System.Drawing.Point(21, 455);
            this.cbClipboardTranslate.Margin = new System.Windows.Forms.Padding(4);
            this.cbClipboardTranslate.Name = "cbClipboardTranslate";
            this.cbClipboardTranslate.Size = new System.Drawing.Size(307, 21);
            this.cbClipboardTranslate.TabIndex = 6;
            this.cbClipboardTranslate.Text = "Auto paste result to clipboard after translate";
            this.cbClipboardTranslate.UseVisualStyleBackColor = true;
            this.cbClipboardTranslate.CheckedChanged += new System.EventHandler(this.cbClipboardTranslate_CheckedChanged);
            // 
            // txtTranslateResult
            // 
            this.txtTranslateResult.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.txtTranslateResult.Location = new System.Drawing.Point(21, 286);
            this.txtTranslateResult.Margin = new System.Windows.Forms.Padding(4);
            this.txtTranslateResult.Multiline = true;
            this.txtTranslateResult.Name = "txtTranslateResult";
            this.txtTranslateResult.ReadOnly = true;
            this.txtTranslateResult.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtTranslateResult.Size = new System.Drawing.Size(447, 147);
            this.txtTranslateResult.TabIndex = 5;
            // 
            // txtLanguages
            // 
            this.txtLanguages.Location = new System.Drawing.Point(21, 256);
            this.txtLanguages.Margin = new System.Windows.Forms.Padding(4);
            this.txtLanguages.Name = "txtLanguages";
            this.txtLanguages.ReadOnly = true;
            this.txtLanguages.Size = new System.Drawing.Size(447, 22);
            this.txtLanguages.TabIndex = 4;
            // 
            // btnTranslate
            // 
            this.btnTranslate.Location = new System.Drawing.Point(21, 217);
            this.btnTranslate.Margin = new System.Windows.Forms.Padding(4);
            this.btnTranslate.Name = "btnTranslate";
            this.btnTranslate.Size = new System.Drawing.Size(256, 30);
            this.btnTranslate.TabIndex = 3;
            this.btnTranslate.Text = "Translate ( Ctrl + Enter )";
            this.btnTranslate.UseVisualStyleBackColor = true;
            this.btnTranslate.Click += new System.EventHandler(this.btnTranslate_Click);
            // 
            // txtTranslateText
            // 
            this.txtTranslateText.Location = new System.Drawing.Point(21, 59);
            this.txtTranslateText.Margin = new System.Windows.Forms.Padding(4);
            this.txtTranslateText.Multiline = true;
            this.txtTranslateText.Name = "txtTranslateText";
            this.txtTranslateText.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtTranslateText.Size = new System.Drawing.Size(447, 147);
            this.txtTranslateText.TabIndex = 2;
            this.txtTranslateText.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtTranslateText_KeyDown);
            // 
            // lblToLanguage
            // 
            this.lblToLanguage.Cursor = System.Windows.Forms.Cursors.SizeAll;
            this.lblToLanguage.Location = new System.Drawing.Point(256, 12);
            this.lblToLanguage.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblToLanguage.Name = "lblToLanguage";
            this.lblToLanguage.Size = new System.Drawing.Size(37, 39);
            this.lblToLanguage.TabIndex = 3;
            this.lblToLanguage.Text = "To:";
            this.lblToLanguage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblToLanguage.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lblToLanguage_MouseDown);
            // 
            // lblFromLanguage
            // 
            this.lblFromLanguage.Location = new System.Drawing.Point(21, 12);
            this.lblFromLanguage.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblFromLanguage.Name = "lblFromLanguage";
            this.lblFromLanguage.Size = new System.Drawing.Size(44, 39);
            this.lblFromLanguage.TabIndex = 2;
            this.lblFromLanguage.Text = "From:";
            this.lblFromLanguage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cbToLanguage
            // 
            this.cbToLanguage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbToLanguage.Enabled = false;
            this.cbToLanguage.FormattingEnabled = true;
            this.cbToLanguage.Location = new System.Drawing.Point(299, 20);
            this.cbToLanguage.Margin = new System.Windows.Forms.Padding(4);
            this.cbToLanguage.MaxDropDownItems = 20;
            this.cbToLanguage.Name = "cbToLanguage";
            this.cbToLanguage.Size = new System.Drawing.Size(169, 24);
            this.cbToLanguage.TabIndex = 1;
            this.cbToLanguage.SelectedIndexChanged += new System.EventHandler(this.cbToLanguage_SelectedIndexChanged);
            // 
            // cbFromLanguage
            // 
            this.cbFromLanguage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbFromLanguage.Enabled = false;
            this.cbFromLanguage.FormattingEnabled = true;
            this.cbFromLanguage.Location = new System.Drawing.Point(75, 20);
            this.cbFromLanguage.Margin = new System.Windows.Forms.Padding(4);
            this.cbFromLanguage.MaxDropDownItems = 20;
            this.cbFromLanguage.Name = "cbFromLanguage";
            this.cbFromLanguage.Size = new System.Drawing.Size(169, 24);
            this.cbFromLanguage.TabIndex = 0;
            this.cbFromLanguage.SelectedIndexChanged += new System.EventHandler(this.cbFromLanguage_SelectedIndexChanged);
            // 
            // tpHistory
            // 
            this.tpHistory.Controls.Add(this.tcHistory);
            this.tpHistory.ImageKey = "pictures.png";
            this.tpHistory.Location = new System.Drawing.Point(4, 25);
            this.tpHistory.Margin = new System.Windows.Forms.Padding(4);
            this.tpHistory.Name = "tpHistory";
            this.tpHistory.Padding = new System.Windows.Forms.Padding(4);
            this.tpHistory.Size = new System.Drawing.Size(1075, 541);
            this.tpHistory.TabIndex = 8;
            this.tpHistory.Text = "History";
            this.tpHistory.UseVisualStyleBackColor = true;
            // 
            // tcHistory
            // 
            this.tcHistory.Controls.Add(this.tpHistoryList);
            this.tcHistory.Controls.Add(this.tpHistorySettings);
            this.tcHistory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcHistory.Location = new System.Drawing.Point(4, 4);
            this.tcHistory.Margin = new System.Windows.Forms.Padding(4);
            this.tcHistory.Name = "tcHistory";
            this.tcHistory.SelectedIndex = 0;
            this.tcHistory.Size = new System.Drawing.Size(1068, 532);
            this.tcHistory.TabIndex = 3;
            // 
            // tpHistoryList
            // 
            this.tpHistoryList.Controls.Add(this.tlpHistory);
            this.tpHistoryList.Location = new System.Drawing.Point(4, 25);
            this.tpHistoryList.Margin = new System.Windows.Forms.Padding(4);
            this.tpHistoryList.Name = "tpHistoryList";
            this.tpHistoryList.Padding = new System.Windows.Forms.Padding(4);
            this.tpHistoryList.Size = new System.Drawing.Size(1060, 503);
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
            this.tlpHistory.Location = new System.Drawing.Point(4, 4);
            this.tlpHistory.Margin = new System.Windows.Forms.Padding(4);
            this.tlpHistory.Name = "tlpHistory";
            this.tlpHistory.RowCount = 1;
            this.tlpHistory.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpHistory.Size = new System.Drawing.Size(1052, 495);
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
            this.tlpHistoryControls.Location = new System.Drawing.Point(424, 4);
            this.tlpHistoryControls.Margin = new System.Windows.Forms.Padding(4);
            this.tlpHistoryControls.Name = "tlpHistoryControls";
            this.tlpHistoryControls.RowCount = 3;
            this.tlpHistoryControls.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tlpHistoryControls.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpHistoryControls.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 160F));
            this.tlpHistoryControls.Size = new System.Drawing.Size(624, 487);
            this.tlpHistoryControls.TabIndex = 0;
            // 
            // lblHistoryScreenshot
            // 
            this.lblHistoryScreenshot.AutoSize = true;
            this.lblHistoryScreenshot.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblHistoryScreenshot.Location = new System.Drawing.Point(4, 0);
            this.lblHistoryScreenshot.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblHistoryScreenshot.Name = "lblHistoryScreenshot";
            this.lblHistoryScreenshot.Size = new System.Drawing.Size(616, 25);
            this.lblHistoryScreenshot.TabIndex = 13;
            this.lblHistoryScreenshot.Text = "Screenshot";
            // 
            // panelControls
            // 
            this.panelControls.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panelControls.Controls.Add(this.btnHistoryOpenLocalFile);
            this.panelControls.Controls.Add(this.txtHistoryLocalPath);
            this.panelControls.Controls.Add(this.btnHistoryCopyLink);
            this.panelControls.Controls.Add(this.lblHistoryRemotePath);
            this.panelControls.Controls.Add(this.btnHistoryCopyImage);
            this.panelControls.Controls.Add(this.txtHistoryRemotePath);
            this.panelControls.Controls.Add(this.btnHistoryBrowseURL);
            this.panelControls.Controls.Add(this.lblHistoryLocalPath);
            this.panelControls.Location = new System.Drawing.Point(4, 331);
            this.panelControls.Margin = new System.Windows.Forms.Padding(4);
            this.panelControls.Name = "panelControls";
            this.panelControls.Size = new System.Drawing.Size(616, 151);
            this.panelControls.TabIndex = 15;
            // 
            // btnHistoryOpenLocalFile
            // 
            this.btnHistoryOpenLocalFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnHistoryOpenLocalFile.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnHistoryOpenLocalFile.Enabled = false;
            this.btnHistoryOpenLocalFile.Location = new System.Drawing.Point(421, 10);
            this.btnHistoryOpenLocalFile.Margin = new System.Windows.Forms.Padding(4);
            this.btnHistoryOpenLocalFile.Name = "btnHistoryOpenLocalFile";
            this.btnHistoryOpenLocalFile.Size = new System.Drawing.Size(128, 30);
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
            this.txtHistoryLocalPath.Location = new System.Drawing.Point(11, 69);
            this.txtHistoryLocalPath.Margin = new System.Windows.Forms.Padding(4);
            this.txtHistoryLocalPath.Name = "txtHistoryLocalPath";
            this.txtHistoryLocalPath.ReadOnly = true;
            this.txtHistoryLocalPath.Size = new System.Drawing.Size(595, 22);
            this.txtHistoryLocalPath.TabIndex = 7;
            // 
            // btnHistoryCopyLink
            // 
            this.btnHistoryCopyLink.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnHistoryCopyLink.Enabled = false;
            this.btnHistoryCopyLink.Location = new System.Drawing.Point(69, 10);
            this.btnHistoryCopyLink.Margin = new System.Windows.Forms.Padding(4);
            this.btnHistoryCopyLink.Name = "btnHistoryCopyLink";
            this.btnHistoryCopyLink.Size = new System.Drawing.Size(107, 30);
            this.btnHistoryCopyLink.TabIndex = 12;
            this.btnHistoryCopyLink.Text = "Copy Link";
            this.btnHistoryCopyLink.UseVisualStyleBackColor = true;
            this.btnHistoryCopyLink.Click += new System.EventHandler(this.btnCopyLink_Click);
            // 
            // lblHistoryRemotePath
            // 
            this.lblHistoryRemotePath.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblHistoryRemotePath.AutoSize = true;
            this.lblHistoryRemotePath.Location = new System.Drawing.Point(16, 97);
            this.lblHistoryRemotePath.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblHistoryRemotePath.Name = "lblHistoryRemotePath";
            this.lblHistoryRemotePath.Size = new System.Drawing.Size(90, 17);
            this.lblHistoryRemotePath.TabIndex = 6;
            this.lblHistoryRemotePath.Text = "Remote Path";
            // 
            // btnHistoryCopyImage
            // 
            this.btnHistoryCopyImage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnHistoryCopyImage.Enabled = false;
            this.btnHistoryCopyImage.Location = new System.Drawing.Point(187, 10);
            this.btnHistoryCopyImage.Margin = new System.Windows.Forms.Padding(4);
            this.btnHistoryCopyImage.Name = "btnHistoryCopyImage";
            this.btnHistoryCopyImage.Size = new System.Drawing.Size(107, 30);
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
            this.txtHistoryRemotePath.Location = new System.Drawing.Point(11, 118);
            this.txtHistoryRemotePath.Margin = new System.Windows.Forms.Padding(4);
            this.txtHistoryRemotePath.Name = "txtHistoryRemotePath";
            this.txtHistoryRemotePath.ReadOnly = true;
            this.txtHistoryRemotePath.Size = new System.Drawing.Size(595, 22);
            this.txtHistoryRemotePath.TabIndex = 8;
            // 
            // btnHistoryBrowseURL
            // 
            this.btnHistoryBrowseURL.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnHistoryBrowseURL.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnHistoryBrowseURL.Enabled = false;
            this.btnHistoryBrowseURL.Location = new System.Drawing.Point(304, 10);
            this.btnHistoryBrowseURL.Margin = new System.Windows.Forms.Padding(4);
            this.btnHistoryBrowseURL.Name = "btnHistoryBrowseURL";
            this.btnHistoryBrowseURL.Size = new System.Drawing.Size(107, 30);
            this.btnHistoryBrowseURL.TabIndex = 10;
            this.btnHistoryBrowseURL.Text = "Browse &URL";
            this.btnHistoryBrowseURL.UseVisualStyleBackColor = true;
            this.btnHistoryBrowseURL.Click += new System.EventHandler(this.btnScreenshotBrowse_Click);
            // 
            // lblHistoryLocalPath
            // 
            this.lblHistoryLocalPath.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblHistoryLocalPath.AutoSize = true;
            this.lblHistoryLocalPath.Location = new System.Drawing.Point(16, 48);
            this.lblHistoryLocalPath.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblHistoryLocalPath.Name = "lblHistoryLocalPath";
            this.lblHistoryLocalPath.Size = new System.Drawing.Size(75, 17);
            this.lblHistoryLocalPath.TabIndex = 5;
            this.lblHistoryLocalPath.Text = "Local Path";
            // 
            // panelPreview
            // 
            this.panelPreview.Controls.Add(this.pbPreview);
            this.panelPreview.Controls.Add(this.txtPreview);
            this.panelPreview.Controls.Add(this.historyBrowser);
            this.panelPreview.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelPreview.Location = new System.Drawing.Point(4, 29);
            this.panelPreview.Margin = new System.Windows.Forms.Padding(4);
            this.panelPreview.Name = "panelPreview";
            this.panelPreview.Size = new System.Drawing.Size(616, 294);
            this.panelPreview.TabIndex = 16;
            // 
            // pbPreview
            // 
            this.pbPreview.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pbPreview.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbPreview.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pbPreview.InitialImage = ((System.Drawing.Image)(resources.GetObject("pbPreview.InitialImage")));
            this.pbPreview.Location = new System.Drawing.Point(0, 0);
            this.pbPreview.Margin = new System.Windows.Forms.Padding(4);
            this.pbPreview.Name = "pbPreview";
            this.pbPreview.Size = new System.Drawing.Size(615, 293);
            this.pbPreview.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbPreview.TabIndex = 4;
            this.pbPreview.TabStop = false;
            this.pbPreview.Click += new System.EventHandler(this.pbHistoryThumb_Click);
            // 
            // txtPreview
            // 
            this.txtPreview.BackColor = System.Drawing.SystemColors.Info;
            this.txtPreview.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtPreview.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPreview.Location = new System.Drawing.Point(0, 0);
            this.txtPreview.Margin = new System.Windows.Forms.Padding(4);
            this.txtPreview.Name = "txtPreview";
            this.txtPreview.ReadOnly = true;
            this.txtPreview.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedBoth;
            this.txtPreview.Size = new System.Drawing.Size(616, 294);
            this.txtPreview.TabIndex = 14;
            this.txtPreview.Text = "";
            // 
            // historyBrowser
            // 
            this.historyBrowser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.historyBrowser.Location = new System.Drawing.Point(0, 0);
            this.historyBrowser.Margin = new System.Windows.Forms.Padding(4);
            this.historyBrowser.MinimumSize = new System.Drawing.Size(27, 25);
            this.historyBrowser.Name = "historyBrowser";
            this.historyBrowser.Size = new System.Drawing.Size(616, 294);
            this.historyBrowser.TabIndex = 15;
            // 
            // lbHistory
            // 
            this.lbHistory.AllowDrop = true;
            this.lbHistory.ContextMenuStrip = this.cmsHistory;
            this.lbHistory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbHistory.FormattingEnabled = true;
            this.lbHistory.HorizontalScrollbar = true;
            this.lbHistory.IntegralHeight = false;
            this.lbHistory.ItemHeight = 16;
            this.lbHistory.Location = new System.Drawing.Point(4, 4);
            this.lbHistory.Margin = new System.Windows.Forms.Padding(4);
            this.lbHistory.Name = "lbHistory";
            this.lbHistory.ScrollAlwaysVisible = true;
            this.lbHistory.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lbHistory.Size = new System.Drawing.Size(412, 487);
            this.lbHistory.TabIndex = 2;
            this.lbHistory.SelectedIndexChanged += new System.EventHandler(this.lbHistory_SelectedIndexChanged);
            this.lbHistory.DragDrop += new System.Windows.Forms.DragEventHandler(this.lbHistory_DragDrop);
            this.lbHistory.DragEnter += new System.Windows.Forms.DragEventHandler(this.lbHistory_DragEnter);
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
            this.tpHistorySettings.Location = new System.Drawing.Point(4, 25);
            this.tpHistorySettings.Margin = new System.Windows.Forms.Padding(4);
            this.tpHistorySettings.Name = "tpHistorySettings";
            this.tpHistorySettings.Padding = new System.Windows.Forms.Padding(4);
            this.tpHistorySettings.Size = new System.Drawing.Size(1060, 503);
            this.tpHistorySettings.TabIndex = 1;
            this.tpHistorySettings.Text = "History Settings";
            this.tpHistorySettings.UseVisualStyleBackColor = true;
            // 
            // cbHistorySave
            // 
            this.cbHistorySave.AutoSize = true;
            this.cbHistorySave.Location = new System.Drawing.Point(21, 89);
            this.cbHistorySave.Margin = new System.Windows.Forms.Padding(4);
            this.cbHistorySave.Name = "cbHistorySave";
            this.cbHistorySave.Size = new System.Drawing.Size(226, 21);
            this.cbHistorySave.TabIndex = 10;
            this.cbHistorySave.Text = "Save History List to an XML file";
            this.cbHistorySave.UseVisualStyleBackColor = true;
            this.cbHistorySave.CheckedChanged += new System.EventHandler(this.cbHistorySave_CheckedChanged);
            // 
            // cbShowHistoryTooltip
            // 
            this.cbShowHistoryTooltip.AutoSize = true;
            this.cbShowHistoryTooltip.Location = new System.Drawing.Point(21, 118);
            this.cbShowHistoryTooltip.Margin = new System.Windows.Forms.Padding(4);
            this.cbShowHistoryTooltip.Name = "cbShowHistoryTooltip";
            this.cbShowHistoryTooltip.Size = new System.Drawing.Size(276, 21);
            this.cbShowHistoryTooltip.TabIndex = 9;
            this.cbShowHistoryTooltip.Text = "Show Screenshot Information in Tooltip";
            this.cbShowHistoryTooltip.UseVisualStyleBackColor = true;
            this.cbShowHistoryTooltip.CheckedChanged += new System.EventHandler(this.cbShowHistoryTooltip_CheckedChanged);
            // 
            // btnHistoryClear
            // 
            this.btnHistoryClear.AutoSize = true;
            this.btnHistoryClear.Location = new System.Drawing.Point(21, 207);
            this.btnHistoryClear.Margin = new System.Windows.Forms.Padding(4);
            this.btnHistoryClear.Name = "btnHistoryClear";
            this.btnHistoryClear.Size = new System.Drawing.Size(183, 33);
            this.btnHistoryClear.TabIndex = 6;
            this.btnHistoryClear.Text = "Clear History List...";
            this.btnHistoryClear.UseVisualStyleBackColor = true;
            this.btnHistoryClear.Click += new System.EventHandler(this.btnHistoryClear_Click);
            // 
            // cbHistoryListFormat
            // 
            this.cbHistoryListFormat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbHistoryListFormat.FormattingEnabled = true;
            this.cbHistoryListFormat.Location = new System.Drawing.Point(149, 20);
            this.cbHistoryListFormat.Margin = new System.Windows.Forms.Padding(4);
            this.cbHistoryListFormat.Name = "cbHistoryListFormat";
            this.cbHistoryListFormat.Size = new System.Drawing.Size(233, 24);
            this.cbHistoryListFormat.TabIndex = 7;
            this.cbHistoryListFormat.SelectedIndexChanged += new System.EventHandler(this.cbHistoryListFormat_SelectedIndexChanged);
            // 
            // lblHistoryMaxItems
            // 
            this.lblHistoryMaxItems.AutoSize = true;
            this.lblHistoryMaxItems.Location = new System.Drawing.Point(21, 59);
            this.lblHistoryMaxItems.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblHistoryMaxItems.Name = "lblHistoryMaxItems";
            this.lblHistoryMaxItems.Size = new System.Drawing.Size(280, 17);
            this.lblHistoryMaxItems.TabIndex = 5;
            this.lblHistoryMaxItems.Text = "Maximum number of screenshots in history:";
            // 
            // lblHistoryListFormat
            // 
            this.lblHistoryListFormat.AutoSize = true;
            this.lblHistoryListFormat.Location = new System.Drawing.Point(21, 25);
            this.lblHistoryListFormat.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblHistoryListFormat.Name = "lblHistoryListFormat";
            this.lblHistoryListFormat.Size = new System.Drawing.Size(121, 17);
            this.lblHistoryListFormat.TabIndex = 8;
            this.lblHistoryListFormat.Text = "History list format:";
            // 
            // nudHistoryMaxItems
            // 
            this.nudHistoryMaxItems.Location = new System.Drawing.Point(309, 54);
            this.nudHistoryMaxItems.Margin = new System.Windows.Forms.Padding(4);
            this.nudHistoryMaxItems.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nudHistoryMaxItems.Name = "nudHistoryMaxItems";
            this.nudHistoryMaxItems.Size = new System.Drawing.Size(96, 22);
            this.nudHistoryMaxItems.TabIndex = 4;
            this.nudHistoryMaxItems.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.nudHistoryMaxItems.ValueChanged += new System.EventHandler(this.nudHistoryMaxItems_ValueChanged);
            // 
            // cbHistoryAddSpace
            // 
            this.cbHistoryAddSpace.AutoSize = true;
            this.cbHistoryAddSpace.BackColor = System.Drawing.Color.Transparent;
            this.cbHistoryAddSpace.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.cbHistoryAddSpace.Location = new System.Drawing.Point(21, 177);
            this.cbHistoryAddSpace.Margin = new System.Windows.Forms.Padding(4);
            this.cbHistoryAddSpace.Name = "cbHistoryAddSpace";
            this.cbHistoryAddSpace.Size = new System.Drawing.Size(307, 21);
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
            this.cbHistoryReverseList.Location = new System.Drawing.Point(21, 148);
            this.cbHistoryReverseList.Margin = new System.Windows.Forms.Padding(4);
            this.cbHistoryReverseList.Name = "cbHistoryReverseList";
            this.cbHistoryReverseList.Size = new System.Drawing.Size(188, 21);
            this.cbHistoryReverseList.TabIndex = 1;
            this.cbHistoryReverseList.Text = "Reverse List in Clipboard";
            this.cbHistoryReverseList.UseVisualStyleBackColor = false;
            this.cbHistoryReverseList.CheckedChanged += new System.EventHandler(this.cbReverse_CheckedChanged);
            // 
            // tpOptions
            // 
            this.tpOptions.Controls.Add(this.tcOptions);
            this.tpOptions.ImageKey = "application_edit.png";
            this.tpOptions.Location = new System.Drawing.Point(4, 25);
            this.tpOptions.Margin = new System.Windows.Forms.Padding(4);
            this.tpOptions.Name = "tpOptions";
            this.tpOptions.Padding = new System.Windows.Forms.Padding(4);
            this.tpOptions.Size = new System.Drawing.Size(1075, 541);
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
            this.tcOptions.Location = new System.Drawing.Point(4, 4);
            this.tcOptions.Margin = new System.Windows.Forms.Padding(4);
            this.tcOptions.Name = "tcOptions";
            this.tcOptions.SelectedIndex = 0;
            this.tcOptions.Size = new System.Drawing.Size(1068, 532);
            this.tcOptions.TabIndex = 8;
            this.tcOptions.SelectedIndexChanged += new System.EventHandler(this.tcOptions_SelectedIndexChanged);
            // 
            // tpGeneral
            // 
            this.tpGeneral.Controls.Add(this.gbMonitorClipboard);
            this.tpGeneral.Controls.Add(this.gbUpdates);
            this.tpGeneral.Controls.Add(this.gbMisc);
            this.tpGeneral.Location = new System.Drawing.Point(4, 25);
            this.tpGeneral.Margin = new System.Windows.Forms.Padding(4);
            this.tpGeneral.Name = "tpGeneral";
            this.tpGeneral.Padding = new System.Windows.Forms.Padding(4);
            this.tpGeneral.Size = new System.Drawing.Size(1060, 503);
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
            this.gbMonitorClipboard.Location = new System.Drawing.Point(11, 177);
            this.gbMonitorClipboard.Margin = new System.Windows.Forms.Padding(4);
            this.gbMonitorClipboard.Name = "gbMonitorClipboard";
            this.gbMonitorClipboard.Padding = new System.Windows.Forms.Padding(4);
            this.gbMonitorClipboard.Size = new System.Drawing.Size(1013, 69);
            this.gbMonitorClipboard.TabIndex = 9;
            this.gbMonitorClipboard.TabStop = false;
            this.gbMonitorClipboard.Text = "Monitor Clipboard";
            // 
            // chkMonUrls
            // 
            this.chkMonUrls.AutoSize = true;
            this.chkMonUrls.Location = new System.Drawing.Point(789, 30);
            this.chkMonUrls.Margin = new System.Windows.Forms.Padding(4);
            this.chkMonUrls.Name = "chkMonUrls";
            this.chkMonUrls.Size = new System.Drawing.Size(65, 21);
            this.chkMonUrls.TabIndex = 3;
            this.chkMonUrls.Text = "URLs";
            this.chkMonUrls.UseVisualStyleBackColor = true;
            this.chkMonUrls.CheckedChanged += new System.EventHandler(this.chkMonUrls_CheckedChanged);
            // 
            // chkMonFiles
            // 
            this.chkMonFiles.AutoSize = true;
            this.chkMonFiles.Location = new System.Drawing.Point(565, 30);
            this.chkMonFiles.Margin = new System.Windows.Forms.Padding(4);
            this.chkMonFiles.Name = "chkMonFiles";
            this.chkMonFiles.Size = new System.Drawing.Size(59, 21);
            this.chkMonFiles.TabIndex = 2;
            this.chkMonFiles.Text = "Files";
            this.chkMonFiles.UseVisualStyleBackColor = true;
            this.chkMonFiles.CheckedChanged += new System.EventHandler(this.chkMonFiles_CheckedChanged);
            // 
            // chkMonImages
            // 
            this.chkMonImages.AutoSize = true;
            this.chkMonImages.Location = new System.Drawing.Point(21, 30);
            this.chkMonImages.Margin = new System.Windows.Forms.Padding(4);
            this.chkMonImages.Name = "chkMonImages";
            this.chkMonImages.Size = new System.Drawing.Size(75, 21);
            this.chkMonImages.TabIndex = 1;
            this.chkMonImages.Text = "Images";
            this.chkMonImages.UseVisualStyleBackColor = true;
            this.chkMonImages.CheckedChanged += new System.EventHandler(this.chkMonImages_CheckedChanged);
            // 
            // chkMonText
            // 
            this.chkMonText.AutoSize = true;
            this.chkMonText.Location = new System.Drawing.Point(267, 30);
            this.chkMonText.Margin = new System.Windows.Forms.Padding(4);
            this.chkMonText.Name = "chkMonText";
            this.chkMonText.Size = new System.Drawing.Size(57, 21);
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
            this.gbUpdates.Location = new System.Drawing.Point(11, 256);
            this.gbUpdates.Margin = new System.Windows.Forms.Padding(4);
            this.gbUpdates.Name = "gbUpdates";
            this.gbUpdates.Padding = new System.Windows.Forms.Padding(4);
            this.gbUpdates.Size = new System.Drawing.Size(1013, 118);
            this.gbUpdates.TabIndex = 8;
            this.gbUpdates.TabStop = false;
            this.gbUpdates.Text = "Check Updates";
            // 
            // chkCheckUpdatesBeta
            // 
            this.chkCheckUpdatesBeta.AutoSize = true;
            this.chkCheckUpdatesBeta.Location = new System.Drawing.Point(267, 30);
            this.chkCheckUpdatesBeta.Margin = new System.Windows.Forms.Padding(4);
            this.chkCheckUpdatesBeta.Name = "chkCheckUpdatesBeta";
            this.chkCheckUpdatesBeta.Size = new System.Drawing.Size(162, 21);
            this.chkCheckUpdatesBeta.TabIndex = 7;
            this.chkCheckUpdatesBeta.Text = "Include beta updates";
            this.chkCheckUpdatesBeta.UseVisualStyleBackColor = true;
            this.chkCheckUpdatesBeta.CheckedChanged += new System.EventHandler(this.cbCheckUpdatesBeta_CheckedChanged);
            // 
            // lblUpdateInfo
            // 
            this.lblUpdateInfo.AutoSize = true;
            this.lblUpdateInfo.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblUpdateInfo.Location = new System.Drawing.Point(523, 30);
            this.lblUpdateInfo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblUpdateInfo.Name = "lblUpdateInfo";
            this.lblUpdateInfo.Size = new System.Drawing.Size(146, 19);
            this.lblUpdateInfo.TabIndex = 6;
            this.lblUpdateInfo.Text = "Update information";
            // 
            // btnCheckUpdate
            // 
            this.btnCheckUpdate.Location = new System.Drawing.Point(21, 69);
            this.btnCheckUpdate.Margin = new System.Windows.Forms.Padding(4);
            this.btnCheckUpdate.Name = "btnCheckUpdate";
            this.btnCheckUpdate.Size = new System.Drawing.Size(139, 30);
            this.btnCheckUpdate.TabIndex = 5;
            this.btnCheckUpdate.Text = "Check Update";
            this.btnCheckUpdate.UseVisualStyleBackColor = true;
            this.btnCheckUpdate.Click += new System.EventHandler(this.btnCheckUpdate_Click);
            // 
            // chkCheckUpdates
            // 
            this.chkCheckUpdates.AutoSize = true;
            this.chkCheckUpdates.Location = new System.Drawing.Point(21, 30);
            this.chkCheckUpdates.Margin = new System.Windows.Forms.Padding(4);
            this.chkCheckUpdates.Name = "chkCheckUpdates";
            this.chkCheckUpdates.Size = new System.Drawing.Size(209, 21);
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
            this.gbMisc.Location = new System.Drawing.Point(11, 10);
            this.gbMisc.Margin = new System.Windows.Forms.Padding(4);
            this.gbMisc.Name = "gbMisc";
            this.gbMisc.Padding = new System.Windows.Forms.Padding(4);
            this.gbMisc.Size = new System.Drawing.Size(1013, 158);
            this.gbMisc.TabIndex = 7;
            this.gbMisc.TabStop = false;
            this.gbMisc.Text = "Program";
            // 
            // chkHotkeys
            // 
            this.chkHotkeys.AutoSize = true;
            this.chkHotkeys.Checked = true;
            this.chkHotkeys.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkHotkeys.Location = new System.Drawing.Point(565, 118);
            this.chkHotkeys.Margin = new System.Windows.Forms.Padding(4);
            this.chkHotkeys.Name = "chkHotkeys";
            this.chkHotkeys.Size = new System.Drawing.Size(217, 21);
            this.chkHotkeys.TabIndex = 9;
            this.chkHotkeys.Text = "Keyboard Hotkeys integration";
            this.chkHotkeys.UseVisualStyleBackColor = true;
            this.chkHotkeys.CheckedChanged += new System.EventHandler(this.chkHotkeys_CheckedChanged);
            // 
            // chkShellExt
            // 
            this.chkShellExt.AutoSize = true;
            this.chkShellExt.Location = new System.Drawing.Point(565, 89);
            this.chkShellExt.Margin = new System.Windows.Forms.Padding(4);
            this.chkShellExt.Name = "chkShellExt";
            this.chkShellExt.Size = new System.Drawing.Size(359, 21);
            this.chkShellExt.TabIndex = 9;
            this.chkShellExt.Text = "Show \"Upload using ZScreen\" in Shell Context Menu";
            this.chkShellExt.UseVisualStyleBackColor = true;
            this.chkShellExt.CheckedChanged += new System.EventHandler(this.chkShellExt_CheckedChanged);
            // 
            // chkWindows7TaskbarIntegration
            // 
            this.chkWindows7TaskbarIntegration.AutoSize = true;
            this.chkWindows7TaskbarIntegration.Location = new System.Drawing.Point(565, 59);
            this.chkWindows7TaskbarIntegration.Margin = new System.Windows.Forms.Padding(4);
            this.chkWindows7TaskbarIntegration.Name = "chkWindows7TaskbarIntegration";
            this.chkWindows7TaskbarIntegration.Size = new System.Drawing.Size(225, 21);
            this.chkWindows7TaskbarIntegration.TabIndex = 8;
            this.chkWindows7TaskbarIntegration.Text = "Windows 7 &Taskbar integration";
            this.chkWindows7TaskbarIntegration.UseVisualStyleBackColor = true;
            this.chkWindows7TaskbarIntegration.CheckedChanged += new System.EventHandler(this.chkWindows7TaskbarIntegration_CheckedChanged);
            // 
            // cbAutoSaveSettings
            // 
            this.cbAutoSaveSettings.AutoSize = true;
            this.cbAutoSaveSettings.Location = new System.Drawing.Point(21, 89);
            this.cbAutoSaveSettings.Margin = new System.Windows.Forms.Padding(4);
            this.cbAutoSaveSettings.Name = "cbAutoSaveSettings";
            this.cbAutoSaveSettings.Size = new System.Drawing.Size(321, 21);
            this.cbAutoSaveSettings.TabIndex = 7;
            this.cbAutoSaveSettings.Text = "Save settings on resize or while switching tabs";
            this.ttZScreen.SetToolTip(this.cbAutoSaveSettings, "ZScreen still saves settings before close");
            this.cbAutoSaveSettings.UseVisualStyleBackColor = true;
            this.cbAutoSaveSettings.CheckedChanged += new System.EventHandler(this.cbAutoSaveSettings_CheckedChanged);
            // 
            // cbShowHelpBalloonTips
            // 
            this.cbShowHelpBalloonTips.AutoSize = true;
            this.cbShowHelpBalloonTips.Location = new System.Drawing.Point(21, 118);
            this.cbShowHelpBalloonTips.Margin = new System.Windows.Forms.Padding(4);
            this.cbShowHelpBalloonTips.Name = "cbShowHelpBalloonTips";
            this.cbShowHelpBalloonTips.Size = new System.Drawing.Size(201, 21);
            this.cbShowHelpBalloonTips.TabIndex = 5;
            this.cbShowHelpBalloonTips.Text = "Show Help via Balloon Tips";
            this.cbShowHelpBalloonTips.UseVisualStyleBackColor = true;
            this.cbShowHelpBalloonTips.CheckedChanged += new System.EventHandler(this.cbShowHelpBalloonTips_CheckedChanged);
            // 
            // chkShowTaskbar
            // 
            this.chkShowTaskbar.AutoSize = true;
            this.chkShowTaskbar.Location = new System.Drawing.Point(21, 59);
            this.chkShowTaskbar.Margin = new System.Windows.Forms.Padding(4);
            this.chkShowTaskbar.Name = "chkShowTaskbar";
            this.chkShowTaskbar.Size = new System.Drawing.Size(222, 21);
            this.chkShowTaskbar.TabIndex = 3;
            this.chkShowTaskbar.Text = "Show Main Window in Taskbar";
            this.chkShowTaskbar.UseVisualStyleBackColor = true;
            this.chkShowTaskbar.CheckedChanged += new System.EventHandler(this.cbShowTaskbar_CheckedChanged);
            // 
            // chkOpenMainWindow
            // 
            this.chkOpenMainWindow.AutoSize = true;
            this.chkOpenMainWindow.Location = new System.Drawing.Point(21, 30);
            this.chkOpenMainWindow.Margin = new System.Windows.Forms.Padding(4);
            this.chkOpenMainWindow.Name = "chkOpenMainWindow";
            this.chkOpenMainWindow.Size = new System.Drawing.Size(203, 21);
            this.chkOpenMainWindow.TabIndex = 2;
            this.chkOpenMainWindow.Text = "Open Main Window on load";
            this.chkOpenMainWindow.UseVisualStyleBackColor = true;
            this.chkOpenMainWindow.CheckedChanged += new System.EventHandler(this.cbOpenMainWindow_CheckedChanged);
            // 
            // chkStartWin
            // 
            this.chkStartWin.AutoSize = true;
            this.chkStartWin.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.chkStartWin.Location = new System.Drawing.Point(565, 30);
            this.chkStartWin.Margin = new System.Windows.Forms.Padding(4);
            this.chkStartWin.Name = "chkStartWin";
            this.chkStartWin.Size = new System.Drawing.Size(148, 21);
            this.chkStartWin.TabIndex = 0;
            this.chkStartWin.Text = "Start with Windows";
            this.chkStartWin.UseVisualStyleBackColor = true;
            this.chkStartWin.CheckedChanged += new System.EventHandler(this.cbStartWin_CheckedChanged);
            // 
            // tpProxy
            // 
            this.tpProxy.Controls.Add(this.gpProxySettings);
            this.tpProxy.Controls.Add(this.ucProxyAccounts);
            this.tpProxy.Location = new System.Drawing.Point(4, 25);
            this.tpProxy.Margin = new System.Windows.Forms.Padding(4);
            this.tpProxy.Name = "tpProxy";
            this.tpProxy.Padding = new System.Windows.Forms.Padding(4);
            this.tpProxy.Size = new System.Drawing.Size(1060, 503);
            this.tpProxy.TabIndex = 6;
            this.tpProxy.Text = "Proxy";
            this.tpProxy.UseVisualStyleBackColor = true;
            // 
            // gpProxySettings
            // 
            this.gpProxySettings.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gpProxySettings.Controls.Add(this.cboProxyConfig);
            this.gpProxySettings.Location = new System.Drawing.Point(21, 389);
            this.gpProxySettings.Margin = new System.Windows.Forms.Padding(4);
            this.gpProxySettings.Name = "gpProxySettings";
            this.gpProxySettings.Padding = new System.Windows.Forms.Padding(4);
            this.gpProxySettings.Size = new System.Drawing.Size(1014, 89);
            this.gpProxySettings.TabIndex = 117;
            this.gpProxySettings.TabStop = false;
            this.gpProxySettings.Text = "Proxy Settings";
            // 
            // cboProxyConfig
            // 
            this.cboProxyConfig.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboProxyConfig.FormattingEnabled = true;
            this.cboProxyConfig.Location = new System.Drawing.Point(21, 30);
            this.cboProxyConfig.Margin = new System.Windows.Forms.Padding(4);
            this.cboProxyConfig.Name = "cboProxyConfig";
            this.cboProxyConfig.Size = new System.Drawing.Size(351, 24);
            this.cboProxyConfig.TabIndex = 114;
            this.cboProxyConfig.SelectedIndexChanged += new System.EventHandler(this.cboProxyConfig_SelectedIndexChanged);
            // 
            // ucProxyAccounts
            // 
            this.ucProxyAccounts.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.ucProxyAccounts.Location = new System.Drawing.Point(4, 4);
            this.ucProxyAccounts.Margin = new System.Windows.Forms.Padding(5);
            this.ucProxyAccounts.Name = "ucProxyAccounts";
            this.ucProxyAccounts.Size = new System.Drawing.Size(1050, 387);
            this.ucProxyAccounts.TabIndex = 0;
            // 
            // tpInteraction
            // 
            this.tpInteraction.Controls.Add(this.gbWindowButtons);
            this.tpInteraction.Controls.Add(this.gbActionsToolbarSettings);
            this.tpInteraction.Controls.Add(this.gbDropBox);
            this.tpInteraction.Controls.Add(this.gbAppearance);
            this.tpInteraction.Location = new System.Drawing.Point(4, 25);
            this.tpInteraction.Margin = new System.Windows.Forms.Padding(4);
            this.tpInteraction.Name = "tpInteraction";
            this.tpInteraction.Padding = new System.Windows.Forms.Padding(4);
            this.tpInteraction.Size = new System.Drawing.Size(1060, 503);
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
            this.gbWindowButtons.Location = new System.Drawing.Point(11, 197);
            this.gbWindowButtons.Margin = new System.Windows.Forms.Padding(4);
            this.gbWindowButtons.Name = "gbWindowButtons";
            this.gbWindowButtons.Padding = new System.Windows.Forms.Padding(4);
            this.gbWindowButtons.Size = new System.Drawing.Size(1003, 69);
            this.gbWindowButtons.TabIndex = 14;
            this.gbWindowButtons.TabStop = false;
            this.gbWindowButtons.Text = "Windows Buttons Behavior";
            // 
            // cboCloseButtonAction
            // 
            this.cboCloseButtonAction.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboCloseButtonAction.FormattingEnabled = true;
            this.cboCloseButtonAction.Location = new System.Drawing.Point(725, 25);
            this.cboCloseButtonAction.Margin = new System.Windows.Forms.Padding(4);
            this.cboCloseButtonAction.Name = "cboCloseButtonAction";
            this.cboCloseButtonAction.Size = new System.Drawing.Size(191, 24);
            this.cboCloseButtonAction.TabIndex = 13;
            this.cboCloseButtonAction.SelectedIndexChanged += new System.EventHandler(this.cbCloseButtonAction_SelectedIndexChanged);
            // 
            // cboMinimizeButtonAction
            // 
            this.cboMinimizeButtonAction.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboMinimizeButtonAction.FormattingEnabled = true;
            this.cboMinimizeButtonAction.Location = new System.Drawing.Point(181, 25);
            this.cboMinimizeButtonAction.Margin = new System.Windows.Forms.Padding(4);
            this.cboMinimizeButtonAction.Name = "cboMinimizeButtonAction";
            this.cboMinimizeButtonAction.Size = new System.Drawing.Size(191, 24);
            this.cboMinimizeButtonAction.TabIndex = 12;
            this.cboMinimizeButtonAction.SelectedIndexChanged += new System.EventHandler(this.cbMinimizeButtonAction_SelectedIndexChanged);
            // 
            // lblCloseButtonAction
            // 
            this.lblCloseButtonAction.AutoSize = true;
            this.lblCloseButtonAction.Location = new System.Drawing.Point(587, 30);
            this.lblCloseButtonAction.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCloseButtonAction.Name = "lblCloseButtonAction";
            this.lblCloseButtonAction.Size = new System.Drawing.Size(133, 17);
            this.lblCloseButtonAction.TabIndex = 10;
            this.lblCloseButtonAction.Text = "Close button action:";
            // 
            // lblMinimizeButtonAction
            // 
            this.lblMinimizeButtonAction.AutoSize = true;
            this.lblMinimizeButtonAction.Location = new System.Drawing.Point(21, 30);
            this.lblMinimizeButtonAction.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblMinimizeButtonAction.Name = "lblMinimizeButtonAction";
            this.lblMinimizeButtonAction.Size = new System.Drawing.Size(152, 17);
            this.lblMinimizeButtonAction.TabIndex = 11;
            this.lblMinimizeButtonAction.Text = "Minimize button action:";
            // 
            // gbActionsToolbarSettings
            // 
            this.gbActionsToolbarSettings.Controls.Add(this.cbCloseQuickActions);
            this.gbActionsToolbarSettings.Location = new System.Drawing.Point(11, 354);
            this.gbActionsToolbarSettings.Margin = new System.Windows.Forms.Padding(4);
            this.gbActionsToolbarSettings.Name = "gbActionsToolbarSettings";
            this.gbActionsToolbarSettings.Padding = new System.Windows.Forms.Padding(4);
            this.gbActionsToolbarSettings.Size = new System.Drawing.Size(1003, 69);
            this.gbActionsToolbarSettings.TabIndex = 7;
            this.gbActionsToolbarSettings.TabStop = false;
            this.gbActionsToolbarSettings.Text = "Actions Toolbar Settings";
            // 
            // cbCloseQuickActions
            // 
            this.cbCloseQuickActions.AutoSize = true;
            this.cbCloseQuickActions.Location = new System.Drawing.Point(21, 30);
            this.cbCloseQuickActions.Margin = new System.Windows.Forms.Padding(4);
            this.cbCloseQuickActions.Name = "cbCloseQuickActions";
            this.cbCloseQuickActions.Size = new System.Drawing.Size(280, 21);
            this.cbCloseQuickActions.TabIndex = 0;
            this.cbCloseQuickActions.Text = "Close Actions Toolbar after Mouse Click";
            this.cbCloseQuickActions.UseVisualStyleBackColor = true;
            this.cbCloseQuickActions.CheckedChanged += new System.EventHandler(this.cbCloseQuickActions_CheckedChanged);
            // 
            // gbDropBox
            // 
            this.gbDropBox.Controls.Add(this.cbCloseDropBox);
            this.gbDropBox.Location = new System.Drawing.Point(11, 276);
            this.gbDropBox.Margin = new System.Windows.Forms.Padding(4);
            this.gbDropBox.Name = "gbDropBox";
            this.gbDropBox.Padding = new System.Windows.Forms.Padding(4);
            this.gbDropBox.Size = new System.Drawing.Size(1003, 69);
            this.gbDropBox.TabIndex = 6;
            this.gbDropBox.TabStop = false;
            this.gbDropBox.Text = "Drop Window Settings";
            // 
            // cbCloseDropBox
            // 
            this.cbCloseDropBox.AutoSize = true;
            this.cbCloseDropBox.Location = new System.Drawing.Point(21, 30);
            this.cbCloseDropBox.Margin = new System.Windows.Forms.Padding(4);
            this.cbCloseDropBox.Name = "cbCloseDropBox";
            this.cbCloseDropBox.Size = new System.Drawing.Size(268, 21);
            this.cbCloseDropBox.TabIndex = 0;
            this.cbCloseDropBox.Text = "Close Drop Window after Drag n Drop";
            this.cbCloseDropBox.UseVisualStyleBackColor = true;
            this.cbCloseDropBox.CheckedChanged += new System.EventHandler(this.cbCloseDropBox_CheckedChanged);
            // 
            // gbAppearance
            // 
            this.gbAppearance.BackColor = System.Drawing.Color.Transparent;
            this.gbAppearance.Controls.Add(this.chkTwitterEnable);
            this.gbAppearance.Controls.Add(this.cbCompleteSound);
            this.gbAppearance.Controls.Add(this.chkCaptureFallback);
            this.gbAppearance.Controls.Add(this.cbShowUploadDuration);
            this.gbAppearance.Controls.Add(this.chkBalloonTipOpenLink);
            this.gbAppearance.Controls.Add(this.cbShowPopup);
            this.gbAppearance.Controls.Add(this.lblTrayFlash);
            this.gbAppearance.Controls.Add(this.nudFlashIconCount);
            this.gbAppearance.Location = new System.Drawing.Point(11, 10);
            this.gbAppearance.Margin = new System.Windows.Forms.Padding(4);
            this.gbAppearance.Name = "gbAppearance";
            this.gbAppearance.Padding = new System.Windows.Forms.Padding(4);
            this.gbAppearance.Size = new System.Drawing.Size(1003, 177);
            this.gbAppearance.TabIndex = 5;
            this.gbAppearance.TabStop = false;
            this.gbAppearance.Text = "After completing a task";
            // 
            // chkTwitterEnable
            // 
            this.chkTwitterEnable.AutoSize = true;
            this.chkTwitterEnable.Location = new System.Drawing.Point(21, 69);
            this.chkTwitterEnable.Margin = new System.Windows.Forms.Padding(4);
            this.chkTwitterEnable.Name = "chkTwitterEnable";
            this.chkTwitterEnable.Size = new System.Drawing.Size(193, 21);
            this.chkTwitterEnable.TabIndex = 9;
            this.chkTwitterEnable.Text = "Update Status in Twitter...";
            this.chkTwitterEnable.UseVisualStyleBackColor = true;
            this.chkTwitterEnable.CheckedChanged += new System.EventHandler(this.chkTwitterEnable_CheckedChanged);
            // 
            // cbCompleteSound
            // 
            this.cbCompleteSound.AutoSize = true;
            this.cbCompleteSound.Location = new System.Drawing.Point(21, 128);
            this.cbCompleteSound.Margin = new System.Windows.Forms.Padding(4);
            this.cbCompleteSound.Name = "cbCompleteSound";
            this.cbCompleteSound.Size = new System.Drawing.Size(303, 21);
            this.cbCompleteSound.TabIndex = 5;
            this.cbCompleteSound.Text = "Play sound after image reaches destination";
            this.cbCompleteSound.UseVisualStyleBackColor = true;
            this.cbCompleteSound.CheckedChanged += new System.EventHandler(this.cbCompleteSound_CheckedChanged);
            // 
            // chkCaptureFallback
            // 
            this.chkCaptureFallback.AutoSize = true;
            this.chkCaptureFallback.Location = new System.Drawing.Point(21, 98);
            this.chkCaptureFallback.Margin = new System.Windows.Forms.Padding(4);
            this.chkCaptureFallback.Name = "chkCaptureFallback";
            this.chkCaptureFallback.Size = new System.Drawing.Size(438, 21);
            this.chkCaptureFallback.TabIndex = 7;
            this.chkCaptureFallback.Text = "Capture entire screen if Active Window capture or Crop Shot fails";
            this.chkCaptureFallback.UseVisualStyleBackColor = true;
            this.chkCaptureFallback.CheckedChanged += new System.EventHandler(this.chkCaptureFallback_CheckedChanged);
            // 
            // cbShowUploadDuration
            // 
            this.cbShowUploadDuration.AutoSize = true;
            this.cbShowUploadDuration.Location = new System.Drawing.Point(587, 128);
            this.cbShowUploadDuration.Margin = new System.Windows.Forms.Padding(4);
            this.cbShowUploadDuration.Name = "cbShowUploadDuration";
            this.cbShowUploadDuration.Size = new System.Drawing.Size(257, 21);
            this.cbShowUploadDuration.TabIndex = 8;
            this.cbShowUploadDuration.Text = "Show upload duration in Balloon Tip";
            this.cbShowUploadDuration.UseVisualStyleBackColor = true;
            this.cbShowUploadDuration.CheckedChanged += new System.EventHandler(this.cbShowUploadDuration_CheckedChanged);
            // 
            // chkBalloonTipOpenLink
            // 
            this.chkBalloonTipOpenLink.AutoSize = true;
            this.chkBalloonTipOpenLink.Location = new System.Drawing.Point(587, 98);
            this.chkBalloonTipOpenLink.Margin = new System.Windows.Forms.Padding(4);
            this.chkBalloonTipOpenLink.Name = "chkBalloonTipOpenLink";
            this.chkBalloonTipOpenLink.Size = new System.Drawing.Size(251, 21);
            this.chkBalloonTipOpenLink.TabIndex = 6;
            this.chkBalloonTipOpenLink.Text = "Open URL/File on Balloon Tip Click";
            this.chkBalloonTipOpenLink.UseVisualStyleBackColor = true;
            this.chkBalloonTipOpenLink.CheckedChanged += new System.EventHandler(this.chkBalloonTipOpenLink_CheckedChanged);
            // 
            // cbShowPopup
            // 
            this.cbShowPopup.AutoSize = true;
            this.cbShowPopup.Location = new System.Drawing.Point(587, 69);
            this.cbShowPopup.Margin = new System.Windows.Forms.Padding(4);
            this.cbShowPopup.Name = "cbShowPopup";
            this.cbShowPopup.Size = new System.Drawing.Size(302, 21);
            this.cbShowPopup.TabIndex = 5;
            this.cbShowPopup.Text = "Show Balloon Tip after upload is completed";
            this.cbShowPopup.UseVisualStyleBackColor = true;
            this.cbShowPopup.CheckedChanged += new System.EventHandler(this.cbShowPopup_CheckedChanged);
            // 
            // lblTrayFlash
            // 
            this.lblTrayFlash.AutoSize = true;
            this.lblTrayFlash.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblTrayFlash.Location = new System.Drawing.Point(19, 32);
            this.lblTrayFlash.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTrayFlash.Name = "lblTrayFlash";
            this.lblTrayFlash.Size = new System.Drawing.Size(424, 17);
            this.lblTrayFlash.TabIndex = 3;
            this.lblTrayFlash.Text = "Number of times tray icon should flash after an upload is complete";
            // 
            // nudFlashIconCount
            // 
            this.nudFlashIconCount.Location = new System.Drawing.Point(448, 27);
            this.nudFlashIconCount.Margin = new System.Windows.Forms.Padding(4);
            this.nudFlashIconCount.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.nudFlashIconCount.Name = "nudFlashIconCount";
            this.nudFlashIconCount.Size = new System.Drawing.Size(77, 22);
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
            this.tpAdvPaths.Location = new System.Drawing.Point(4, 25);
            this.tpAdvPaths.Margin = new System.Windows.Forms.Padding(4);
            this.tpAdvPaths.Name = "tpAdvPaths";
            this.tpAdvPaths.Size = new System.Drawing.Size(1060, 503);
            this.tpAdvPaths.TabIndex = 2;
            this.tpAdvPaths.Text = "Paths";
            this.tpAdvPaths.UseVisualStyleBackColor = true;
            // 
            // chkPreferSystemFolders
            // 
            this.chkPreferSystemFolders.AutoSize = true;
            this.chkPreferSystemFolders.Location = new System.Drawing.Point(21, 20);
            this.chkPreferSystemFolders.Margin = new System.Windows.Forms.Padding(4);
            this.chkPreferSystemFolders.Name = "chkPreferSystemFolders";
            this.chkPreferSystemFolders.Size = new System.Drawing.Size(339, 21);
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
            this.gbRoot.Location = new System.Drawing.Point(11, 49);
            this.gbRoot.Margin = new System.Windows.Forms.Padding(4);
            this.gbRoot.Name = "gbRoot";
            this.gbRoot.Padding = new System.Windows.Forms.Padding(4);
            this.gbRoot.Size = new System.Drawing.Size(1022, 79);
            this.gbRoot.TabIndex = 117;
            this.gbRoot.TabStop = false;
            this.gbRoot.Text = "Root";
            // 
            // btnViewRootDir
            // 
            this.btnViewRootDir.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnViewRootDir.Location = new System.Drawing.Point(866, 27);
            this.btnViewRootDir.Margin = new System.Windows.Forms.Padding(4);
            this.btnViewRootDir.Name = "btnViewRootDir";
            this.btnViewRootDir.Size = new System.Drawing.Size(139, 30);
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
            this.btnBrowseRootDir.Location = new System.Drawing.Point(748, 27);
            this.btnBrowseRootDir.Margin = new System.Windows.Forms.Padding(4);
            this.btnBrowseRootDir.Name = "btnBrowseRootDir";
            this.btnBrowseRootDir.Size = new System.Drawing.Size(107, 30);
            this.btnBrowseRootDir.TabIndex = 115;
            this.btnBrowseRootDir.Text = "Relocate...";
            this.btnBrowseRootDir.UseVisualStyleBackColor = true;
            this.btnBrowseRootDir.Click += new System.EventHandler(this.btnBrowseRootDir_Click);
            // 
            // txtRootFolder
            // 
            this.txtRootFolder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtRootFolder.Location = new System.Drawing.Point(21, 30);
            this.txtRootFolder.Margin = new System.Windows.Forms.Padding(4);
            this.txtRootFolder.Name = "txtRootFolder";
            this.txtRootFolder.ReadOnly = true;
            this.txtRootFolder.Size = new System.Drawing.Size(704, 22);
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
            this.gbImages.Location = new System.Drawing.Point(11, 138);
            this.gbImages.Margin = new System.Windows.Forms.Padding(4);
            this.gbImages.Name = "gbImages";
            this.gbImages.Padding = new System.Windows.Forms.Padding(4);
            this.gbImages.Size = new System.Drawing.Size(1022, 148);
            this.gbImages.TabIndex = 114;
            this.gbImages.TabStop = false;
            this.gbImages.Text = "Images";
            // 
            // btnBrowseImagesDir
            // 
            this.btnBrowseImagesDir.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBrowseImagesDir.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnBrowseImagesDir.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnBrowseImagesDir.Location = new System.Drawing.Point(748, 26);
            this.btnBrowseImagesDir.Margin = new System.Windows.Forms.Padding(4);
            this.btnBrowseImagesDir.Name = "btnBrowseImagesDir";
            this.btnBrowseImagesDir.Size = new System.Drawing.Size(107, 30);
            this.btnBrowseImagesDir.TabIndex = 117;
            this.btnBrowseImagesDir.Text = "Relocate...";
            this.btnBrowseImagesDir.UseVisualStyleBackColor = true;
            this.btnBrowseImagesDir.Click += new System.EventHandler(this.BtnBrowseImagesDirClick);
            // 
            // btnMoveImageFiles
            // 
            this.btnMoveImageFiles.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMoveImageFiles.Location = new System.Drawing.Point(770, 69);
            this.btnMoveImageFiles.Margin = new System.Windows.Forms.Padding(4);
            this.btnMoveImageFiles.Name = "btnMoveImageFiles";
            this.btnMoveImageFiles.Size = new System.Drawing.Size(235, 28);
            this.btnMoveImageFiles.TabIndex = 117;
            this.btnMoveImageFiles.Text = "Move Images to Sub-folders...";
            this.btnMoveImageFiles.UseVisualStyleBackColor = true;
            this.btnMoveImageFiles.Click += new System.EventHandler(this.btnMoveImageFiles_Click);
            // 
            // lblImagesFolderPattern
            // 
            this.lblImagesFolderPattern.AutoSize = true;
            this.lblImagesFolderPattern.Location = new System.Drawing.Point(21, 73);
            this.lblImagesFolderPattern.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblImagesFolderPattern.Name = "lblImagesFolderPattern";
            this.lblImagesFolderPattern.Size = new System.Drawing.Size(128, 17);
            this.lblImagesFolderPattern.TabIndex = 116;
            this.lblImagesFolderPattern.Text = "Sub-folder Pattern:";
            // 
            // lblImagesFolderPatternPreview
            // 
            this.lblImagesFolderPatternPreview.AutoSize = true;
            this.lblImagesFolderPatternPreview.Location = new System.Drawing.Point(309, 73);
            this.lblImagesFolderPatternPreview.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblImagesFolderPatternPreview.Name = "lblImagesFolderPatternPreview";
            this.lblImagesFolderPatternPreview.Size = new System.Drawing.Size(106, 17);
            this.lblImagesFolderPatternPreview.TabIndex = 115;
            this.lblImagesFolderPatternPreview.Text = "Pattern preview";
            // 
            // txtImagesFolderPattern
            // 
            this.txtImagesFolderPattern.Location = new System.Drawing.Point(160, 69);
            this.txtImagesFolderPattern.Margin = new System.Windows.Forms.Padding(4);
            this.txtImagesFolderPattern.Name = "txtImagesFolderPattern";
            this.txtImagesFolderPattern.Size = new System.Drawing.Size(132, 22);
            this.txtImagesFolderPattern.TabIndex = 114;
            this.ttZScreen.SetToolTip(this.txtImagesFolderPattern, "%y = Year\r\n%mo = Month\r\n%mon = Month Name\r\n%d = Day");
            this.txtImagesFolderPattern.TextChanged += new System.EventHandler(this.txtImagesFolderPattern_TextChanged);
            // 
            // chkDeleteLocal
            // 
            this.chkDeleteLocal.AutoSize = true;
            this.chkDeleteLocal.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.chkDeleteLocal.Location = new System.Drawing.Point(21, 108);
            this.chkDeleteLocal.Margin = new System.Windows.Forms.Padding(4);
            this.chkDeleteLocal.Name = "chkDeleteLocal";
            this.chkDeleteLocal.Size = new System.Drawing.Size(375, 21);
            this.chkDeleteLocal.TabIndex = 0;
            this.chkDeleteLocal.Text = "Delete captured screenshots after upload is completed";
            this.chkDeleteLocal.UseVisualStyleBackColor = true;
            this.chkDeleteLocal.CheckedChanged += new System.EventHandler(this.cbDeleteLocal_CheckedChanged);
            // 
            // btnViewImagesDir
            // 
            this.btnViewImagesDir.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnViewImagesDir.Location = new System.Drawing.Point(866, 26);
            this.btnViewImagesDir.Margin = new System.Windows.Forms.Padding(4);
            this.btnViewImagesDir.Name = "btnViewImagesDir";
            this.btnViewImagesDir.Size = new System.Drawing.Size(139, 30);
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
            this.txtImagesDir.Location = new System.Drawing.Point(21, 30);
            this.txtImagesDir.Margin = new System.Windows.Forms.Padding(4);
            this.txtImagesDir.Name = "txtImagesDir";
            this.txtImagesDir.ReadOnly = true;
            this.txtImagesDir.Size = new System.Drawing.Size(704, 22);
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
            this.gbSettingsExportImport.Location = new System.Drawing.Point(11, 414);
            this.gbSettingsExportImport.Margin = new System.Windows.Forms.Padding(4);
            this.gbSettingsExportImport.Name = "gbSettingsExportImport";
            this.gbSettingsExportImport.Padding = new System.Windows.Forms.Padding(4);
            this.gbSettingsExportImport.Size = new System.Drawing.Size(1022, 69);
            this.gbSettingsExportImport.TabIndex = 6;
            this.gbSettingsExportImport.TabStop = false;
            this.gbSettingsExportImport.Text = "Backup and Restore";
            // 
            // btnSettingsDefault
            // 
            this.btnSettingsDefault.AutoSize = true;
            this.btnSettingsDefault.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnSettingsDefault.Location = new System.Drawing.Point(299, 26);
            this.btnSettingsDefault.Margin = new System.Windows.Forms.Padding(4);
            this.btnSettingsDefault.Name = "btnSettingsDefault";
            this.btnSettingsDefault.Size = new System.Drawing.Size(130, 27);
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
            this.btnSettingsExport.Location = new System.Drawing.Point(160, 26);
            this.btnSettingsExport.Margin = new System.Windows.Forms.Padding(4);
            this.btnSettingsExport.Name = "btnSettingsExport";
            this.btnSettingsExport.Size = new System.Drawing.Size(125, 27);
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
            this.btnFTPExport.Location = new System.Drawing.Point(841, 26);
            this.btnFTPExport.Margin = new System.Windows.Forms.Padding(4);
            this.btnFTPExport.Name = "btnFTPExport";
            this.btnFTPExport.Size = new System.Drawing.Size(162, 27);
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
            this.btnFTPImport.Location = new System.Drawing.Point(660, 26);
            this.btnFTPImport.Margin = new System.Windows.Forms.Padding(4);
            this.btnFTPImport.Name = "btnFTPImport";
            this.btnFTPImport.Size = new System.Drawing.Size(161, 27);
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
            this.btnSettingsImport.Location = new System.Drawing.Point(21, 26);
            this.btnSettingsImport.Margin = new System.Windows.Forms.Padding(4);
            this.btnSettingsImport.Name = "btnSettingsImport";
            this.btnSettingsImport.Size = new System.Drawing.Size(124, 27);
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
            this.gbCache.Location = new System.Drawing.Point(11, 295);
            this.gbCache.Margin = new System.Windows.Forms.Padding(4);
            this.gbCache.Name = "gbCache";
            this.gbCache.Padding = new System.Windows.Forms.Padding(4);
            this.gbCache.Size = new System.Drawing.Size(1022, 108);
            this.gbCache.TabIndex = 1;
            this.gbCache.TabStop = false;
            this.gbCache.Text = "Cache";
            // 
            // btnViewCacheDir
            // 
            this.btnViewCacheDir.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnViewCacheDir.Location = new System.Drawing.Point(862, 27);
            this.btnViewCacheDir.Margin = new System.Windows.Forms.Padding(4);
            this.btnViewCacheDir.Name = "btnViewCacheDir";
            this.btnViewCacheDir.Size = new System.Drawing.Size(139, 30);
            this.btnViewCacheDir.TabIndex = 7;
            this.btnViewCacheDir.Text = "View Directory...";
            this.btnViewCacheDir.UseVisualStyleBackColor = true;
            this.btnViewCacheDir.Click += new System.EventHandler(this.btnViewRemoteDirectory_Click);
            // 
            // lblCacheSize
            // 
            this.lblCacheSize.AutoSize = true;
            this.lblCacheSize.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblCacheSize.Location = new System.Drawing.Point(19, 74);
            this.lblCacheSize.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCacheSize.Name = "lblCacheSize";
            this.lblCacheSize.Size = new System.Drawing.Size(81, 17);
            this.lblCacheSize.TabIndex = 5;
            this.lblCacheSize.Text = "Cache size:";
            // 
            // lblMebibytes
            // 
            this.lblMebibytes.AutoSize = true;
            this.lblMebibytes.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblMebibytes.Location = new System.Drawing.Point(267, 74);
            this.lblMebibytes.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblMebibytes.Name = "lblMebibytes";
            this.lblMebibytes.Size = new System.Drawing.Size(31, 17);
            this.lblMebibytes.TabIndex = 4;
            this.lblMebibytes.Text = "MiB";
            // 
            // nudCacheSize
            // 
            this.nudCacheSize.Location = new System.Drawing.Point(107, 69);
            this.nudCacheSize.Margin = new System.Windows.Forms.Padding(4);
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
            this.nudCacheSize.Size = new System.Drawing.Size(149, 22);
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
            this.txtCacheDir.Location = new System.Drawing.Point(21, 30);
            this.txtCacheDir.Margin = new System.Windows.Forms.Padding(4);
            this.txtCacheDir.Name = "txtCacheDir";
            this.txtCacheDir.ReadOnly = true;
            this.txtCacheDir.Size = new System.Drawing.Size(830, 22);
            this.txtCacheDir.TabIndex = 0;
            // 
            // tpStats
            // 
            this.tpStats.Controls.Add(this.btnOpenZScreenTester);
            this.tpStats.Controls.Add(this.gbStatistics);
            this.tpStats.Controls.Add(this.gbLastSource);
            this.tpStats.Location = new System.Drawing.Point(4, 25);
            this.tpStats.Margin = new System.Windows.Forms.Padding(4);
            this.tpStats.Name = "tpStats";
            this.tpStats.Padding = new System.Windows.Forms.Padding(4);
            this.tpStats.Size = new System.Drawing.Size(1060, 503);
            this.tpStats.TabIndex = 1;
            this.tpStats.Text = "Statistics";
            this.tpStats.UseVisualStyleBackColor = true;
            // 
            // btnOpenZScreenTester
            // 
            this.btnOpenZScreenTester.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOpenZScreenTester.Location = new System.Drawing.Point(812, 415);
            this.btnOpenZScreenTester.Margin = new System.Windows.Forms.Padding(4);
            this.btnOpenZScreenTester.Name = "btnOpenZScreenTester";
            this.btnOpenZScreenTester.Size = new System.Drawing.Size(213, 28);
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
            this.gbStatistics.Location = new System.Drawing.Point(11, 10);
            this.gbStatistics.Margin = new System.Windows.Forms.Padding(4);
            this.gbStatistics.Name = "gbStatistics";
            this.gbStatistics.Padding = new System.Windows.Forms.Padding(4);
            this.gbStatistics.Size = new System.Drawing.Size(1035, 384);
            this.gbStatistics.TabIndex = 28;
            this.gbStatistics.TabStop = false;
            this.gbStatistics.Text = "Statistics";
            // 
            // btnDebugStart
            // 
            this.btnDebugStart.Location = new System.Drawing.Point(21, 30);
            this.btnDebugStart.Margin = new System.Windows.Forms.Padding(4);
            this.btnDebugStart.Name = "btnDebugStart";
            this.btnDebugStart.Size = new System.Drawing.Size(85, 30);
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
            this.rtbDebugInfo.Location = new System.Drawing.Point(21, 69);
            this.rtbDebugInfo.Margin = new System.Windows.Forms.Padding(4);
            this.rtbDebugInfo.Name = "rtbDebugInfo";
            this.rtbDebugInfo.ReadOnly = true;
            this.rtbDebugInfo.Size = new System.Drawing.Size(994, 301);
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
            this.gbLastSource.Location = new System.Drawing.Point(11, 405);
            this.gbLastSource.Margin = new System.Windows.Forms.Padding(4);
            this.gbLastSource.Name = "gbLastSource";
            this.gbLastSource.Padding = new System.Windows.Forms.Padding(4);
            this.gbLastSource.Size = new System.Drawing.Size(546, 79);
            this.gbLastSource.TabIndex = 26;
            this.gbLastSource.TabStop = false;
            this.gbLastSource.Text = "Last Source";
            // 
            // btnOpenSourceString
            // 
            this.btnOpenSourceString.Enabled = false;
            this.btnOpenSourceString.Location = new System.Drawing.Point(21, 30);
            this.btnOpenSourceString.Margin = new System.Windows.Forms.Padding(4);
            this.btnOpenSourceString.Name = "btnOpenSourceString";
            this.btnOpenSourceString.Size = new System.Drawing.Size(160, 28);
            this.btnOpenSourceString.TabIndex = 25;
            this.btnOpenSourceString.Text = "Copy to Clipboard";
            this.btnOpenSourceString.UseVisualStyleBackColor = true;
            this.btnOpenSourceString.Click += new System.EventHandler(this.btnOpenSourceString_Click);
            // 
            // btnOpenSourceText
            // 
            this.btnOpenSourceText.Enabled = false;
            this.btnOpenSourceText.Location = new System.Drawing.Point(192, 30);
            this.btnOpenSourceText.Margin = new System.Windows.Forms.Padding(4);
            this.btnOpenSourceText.Name = "btnOpenSourceText";
            this.btnOpenSourceText.Size = new System.Drawing.Size(160, 28);
            this.btnOpenSourceText.TabIndex = 24;
            this.btnOpenSourceText.Text = "Open in Text Editor";
            this.btnOpenSourceText.UseVisualStyleBackColor = true;
            this.btnOpenSourceText.Click += new System.EventHandler(this.btnOpenSourceText_Click);
            // 
            // btnOpenSourceBrowser
            // 
            this.btnOpenSourceBrowser.Enabled = false;
            this.btnOpenSourceBrowser.Location = new System.Drawing.Point(363, 30);
            this.btnOpenSourceBrowser.Margin = new System.Windows.Forms.Padding(4);
            this.btnOpenSourceBrowser.Name = "btnOpenSourceBrowser";
            this.btnOpenSourceBrowser.Size = new System.Drawing.Size(160, 28);
            this.btnOpenSourceBrowser.TabIndex = 22;
            this.btnOpenSourceBrowser.Text = "Open in Browser";
            this.btnOpenSourceBrowser.UseVisualStyleBackColor = true;
            this.btnOpenSourceBrowser.Click += new System.EventHandler(this.btnOpenSourceBrowser_Click);
            // 
            // tpDebugLog
            // 
            this.tpDebugLog.Controls.Add(this.rtbDebugLog);
            this.tpDebugLog.Location = new System.Drawing.Point(4, 25);
            this.tpDebugLog.Margin = new System.Windows.Forms.Padding(4);
            this.tpDebugLog.Name = "tpDebugLog";
            this.tpDebugLog.Padding = new System.Windows.Forms.Padding(4);
            this.tpDebugLog.Size = new System.Drawing.Size(1060, 503);
            this.tpDebugLog.TabIndex = 7;
            this.tpDebugLog.Text = "Debug";
            this.tpDebugLog.UseVisualStyleBackColor = true;
            // 
            // rtbDebugLog
            // 
            this.rtbDebugLog.BackColor = System.Drawing.Color.WhiteSmoke;
            this.rtbDebugLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbDebugLog.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.rtbDebugLog.Location = new System.Drawing.Point(4, 4);
            this.rtbDebugLog.Margin = new System.Windows.Forms.Padding(4);
            this.rtbDebugLog.Name = "rtbDebugLog";
            this.rtbDebugLog.ReadOnly = true;
            this.rtbDebugLog.Size = new System.Drawing.Size(1052, 495);
            this.rtbDebugLog.TabIndex = 0;
            this.rtbDebugLog.Text = "";
            this.rtbDebugLog.WordWrap = false;
            // 
            // tpOptionsAdv
            // 
            this.tpOptionsAdv.Controls.Add(this.pgApp);
            this.tpOptionsAdv.Location = new System.Drawing.Point(4, 25);
            this.tpOptionsAdv.Margin = new System.Windows.Forms.Padding(4);
            this.tpOptionsAdv.Name = "tpOptionsAdv";
            this.tpOptionsAdv.Padding = new System.Windows.Forms.Padding(4);
            this.tpOptionsAdv.Size = new System.Drawing.Size(1060, 503);
            this.tpOptionsAdv.TabIndex = 3;
            this.tpOptionsAdv.Text = "Advanced";
            this.tpOptionsAdv.UseVisualStyleBackColor = true;
            // 
            // pgApp
            // 
            this.pgApp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pgApp.Location = new System.Drawing.Point(4, 4);
            this.pgApp.Margin = new System.Windows.Forms.Padding(4);
            this.pgApp.Name = "pgApp";
            this.pgApp.Size = new System.Drawing.Size(1052, 495);
            this.pgApp.TabIndex = 0;
            this.pgApp.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.pgApp_PropertyValueChanged);
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
            // ucTwitterAccounts
            // 
            this.ucTwitterAccounts.Location = new System.Drawing.Point(432, 112);
            this.ucTwitterAccounts.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.ucTwitterAccounts.Name = "ucTwitterAccounts";
            this.ucTwitterAccounts.Size = new System.Drawing.Size(544, 332);
            this.ucTwitterAccounts.TabIndex = 22;
            // 
            // ZScreen
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1089, 574);
            this.Controls.Add(this.tcApp);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MinimumSize = new System.Drawing.Size(1093, 604);
            this.Name = "ZScreen";
            this.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
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
            this.cmsHistory.ResumeLayout(false);
            this.tcApp.ResumeLayout(false);
            this.tpMain.ResumeLayout(false);
            this.tpMain.PerformLayout();
            this.gbImageSettings.ResumeLayout(false);
            this.gbImageSettings.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbLogo)).EndInit();
            this.tpDestinations.ResumeLayout(false);
            this.tcDestinations.ResumeLayout(false);
            this.tpFTP.ResumeLayout(false);
            this.gbFTPSettings.ResumeLayout(false);
            this.gbFTPSettings.PerformLayout();
            this.tpLocalhost.ResumeLayout(false);
            this.tpRapidShare.ResumeLayout(false);
            this.tpRapidShare.PerformLayout();
            this.tpSendSpace.ResumeLayout(false);
            this.tpSendSpace.PerformLayout();
            this.tpDropbox.ResumeLayout(false);
            this.tpDropbox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbDropboxLogo)).EndInit();
            this.tpImageShack.ResumeLayout(false);
            this.tpImageShack.PerformLayout();
            this.gbImageShack.ResumeLayout(false);
            this.gbImageShack.PerformLayout();
            this.tpTinyPic.ResumeLayout(false);
            this.tpTinyPic.PerformLayout();
            this.gbTinyPic.ResumeLayout(false);
            this.gbTinyPic.PerformLayout();
            this.tpImgur.ResumeLayout(false);
            this.tpImgur.PerformLayout();
            this.tpFlickr.ResumeLayout(false);
            this.tpTwitter.ResumeLayout(false);
            this.tpTwitter.PerformLayout();
            this.gbTwitterOthers.ResumeLayout(false);
            this.gbTwitterOthers.PerformLayout();
            this.tpImageBam.ResumeLayout(false);
            this.gbImageBamGalleries.ResumeLayout(false);
            this.gbImageBamLinks.ResumeLayout(false);
            this.gbImageBamLinks.PerformLayout();
            this.gbImageBamApiKeys.ResumeLayout(false);
            this.gbImageBamApiKeys.PerformLayout();
            this.tpMindTouch.ResumeLayout(false);
            this.gbMindTouchOptions.ResumeLayout(false);
            this.gbMindTouchOptions.PerformLayout();
            this.tpMediaWiki.ResumeLayout(false);
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
            ((System.ComponentModel.ISupportInitialize)(this.pbPreview)).EndInit();
            this.tpHistorySettings.ResumeLayout(false);
            this.tpHistorySettings.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudHistoryMaxItems)).EndInit();
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
            this.ResumeLayout(false);

        }

        internal System.Windows.Forms.GroupBox gbCache;
        internal System.Windows.Forms.GroupBox gbImages;
        internal System.Windows.Forms.Button btnBrowseImagesDir;
        private System.Windows.Forms.GroupBox gbWindowButtons;
        private System.Windows.Forms.CheckBox chkEditorsEnabled;

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
        internal System.Windows.Forms.TabPage tpEditors;
        internal System.Windows.Forms.TabControl tcEditors;
        internal System.Windows.Forms.TabPage tpEditorsImages;
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
        internal System.Windows.Forms.CheckBox cbAddFailedScreenshot;
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
        internal System.Windows.Forms.TabPage tpCustomUploaders;
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
        internal System.Windows.Forms.Label lblDictionary;
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
        internal System.Windows.Forms.RichTextBox txtPreview;
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
        internal System.Windows.Forms.TabPage tpTinyPic;
        internal System.Windows.Forms.TabPage tpImageShack;
        internal System.Windows.Forms.TabPage tpTextServices;
        internal System.Windows.Forms.TabControl tcTextUploaders;
        internal System.Windows.Forms.GroupBox gbImageSettings;
        internal System.Windows.Forms.GroupBox gpCropRegion;
        internal System.Windows.Forms.TextBox txtAutoTranslate;
        internal System.Windows.Forms.CheckBox cbAutoTranslate;
        internal System.Windows.Forms.ToolTip ttZScreen;
        internal System.Windows.Forms.CheckBox cbShowHelpBalloonTips;
        internal System.Windows.Forms.TabPage tpMindTouch;
        internal AccountsControl ucMindTouchAccounts;
        internal System.Windows.Forms.GroupBox gbImageEditorSettings;
        internal System.Windows.Forms.CheckBox chkImageEditorAutoSave;
        internal System.Windows.Forms.TabPage tpFTP;
        internal AccountsControl ucFTPAccounts;
        internal System.Windows.Forms.CheckBox chkPublicImageShack;
        internal System.Windows.Forms.Button btnImageShackProfile;
        internal System.Windows.Forms.Label lblImageShackUsername;
        internal System.Windows.Forms.TextBox txtUserNameImageShack;
        internal System.Windows.Forms.Label lblScreenshotDelay;
        internal System.Windows.Forms.GroupBox gbDynamicCrosshair;
        internal System.Windows.Forms.GroupBox gbDynamicRegionBorderColorSettings;
        internal System.Windows.Forms.TabPage tpTwitter;
        internal System.Windows.Forms.GroupBox gbMindTouchOptions;
        internal System.Windows.Forms.CheckBox chkDekiWikiForcePath;
        private System.Windows.Forms.TabPage tpProxy;
        private AccountsControl ucProxyAccounts;
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
        private System.Windows.Forms.TableLayoutPanel tlpHistory;
        private System.Windows.Forms.TableLayoutPanel tlpHistoryControls;
        private System.Windows.Forms.Panel panelControls;
        private System.Windows.Forms.Panel panelPreview;
        private System.Windows.Forms.WebBrowser historyBrowser;
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
        private System.Windows.Forms.TabPage tpSendSpace;
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
        private System.Windows.Forms.TabPage tpFlickr;
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
        private System.Windows.Forms.ToolStripMenuItem tsmiTwitter;
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
        internal System.Windows.Forms.ListView lvDictionary;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.TabPage tpDebugLog;
        private System.Windows.Forms.RichTextBox rtbDebugLog;
        private System.Windows.Forms.ComboBox cboCloseButtonAction;
        private System.Windows.Forms.ComboBox cboMinimizeButtonAction;
        private System.Windows.Forms.Label lblMinimizeButtonAction;
        private System.Windows.Forms.Label lblCloseButtonAction;
        private System.Windows.Forms.CheckBox chkPreferSystemFolders;
        internal System.Windows.Forms.GroupBox gbSettingsExportImport;
        private System.Windows.Forms.Button btnResetHotkeys;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator9;
        private System.Windows.Forms.ToolStripMenuItem editInPicnikToolStripMenuItem;
        private System.Windows.Forms.TabPage tpFreehandCropShot;
        private System.Windows.Forms.CheckBox cbFreehandCropShowHelpText;
        private System.Windows.Forms.CheckBox cbFreehandCropAutoUpload;
        private System.Windows.Forms.CheckBox cbFreehandCropAutoClose;
        private System.Windows.Forms.CheckBox cbFreehandCropShowRectangleBorder;
        private System.Windows.Forms.TabPage tpLocalhost;
        internal AccountsControl ucLocalhostAccounts;
        private System.Windows.Forms.Label lblFtpFiles;
        private System.Windows.Forms.Label lblFtpText;
        private System.Windows.Forms.Label lblFtpImages;
        private System.Windows.Forms.ComboBox cboFtpFiles;
        private System.Windows.Forms.ComboBox cboFtpText;
        private System.Windows.Forms.ComboBox cboFtpImages;
        private System.Windows.Forms.ComboBox cboProxyConfig;
        private System.Windows.Forms.TabPage tpMediaWiki;
        internal AccountsControl ucMediaWikiAccounts;
        private System.Windows.Forms.TabPage tpDropbox;
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
        private System.Windows.Forms.TabPage tpImgur;
        private System.Windows.Forms.Button btnImgurOpenAuthorizePage;
        private System.Windows.Forms.Button btnImgurLogin;
        private System.Windows.Forms.Label lblImgurVerificationCode;
        private System.Windows.Forms.TextBox tbImgurVerificationCode;
        private System.Windows.Forms.Label lblImgurStatus;
        private System.Windows.Forms.Label lblImgurHowTo;
        private System.Windows.Forms.CheckBox cbImgurUseAccount;
        private System.Windows.Forms.GroupBox gbTwitterOthers;
        private System.Windows.Forms.Button btnTwitterLogin;
        private System.Windows.Forms.Label lblTwitterVerificationCode;
        private System.Windows.Forms.TextBox tbTwitterVerificationCode;
        private System.Windows.Forms.Button btnTwitterOpenAuthorizePage;
        private System.Windows.Forms.Label lblTwitterStatus;
        private AccountsControl ucTwitterAccounts;
    }
}
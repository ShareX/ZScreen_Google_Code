namespace ZSS
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle15 = new System.Windows.Forms.DataGridViewCellStyle();
            this.niTray = new System.Windows.Forms.NotifyIcon(this.components);
            this.cmTray = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmSettings = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmHotkeys = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmFileSettings = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmImageSoftwareSettings = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmFTPSettings = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmHTTPSettings = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmAdvanced = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmHistory = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmLic = new System.Windows.Forms.ToolStripMenuItem();
            this.cmVersionHistory = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmAboutMain = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmSendTo = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmDestClipboard = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmDestFile = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmDestFTP = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmDestImageShack = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmDestTinyPic = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmDestCustomHTTP = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmImageSoftware = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmCbCopy = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmPromptFileName = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmViewRemote = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmViewDirectory = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.captureToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.entireScreenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rectangularRegionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lastRectangularRegionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmScrenshotFromClipboard = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmDropWindow = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmQuickOptions = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.exitZScreenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tpScreenshots = new System.Windows.Forms.TabPage();
            this.gbScreenshotPreview = new System.Windows.Forms.GroupBox();
            this.btnScreenshotBrowse = new System.Windows.Forms.Button();
            this.btnScreenshotOpen = new System.Windows.Forms.Button();
            this.txtHistoryRemotePath = new System.Windows.Forms.TextBox();
            this.txtHistoryLocalPath = new System.Windows.Forms.TextBox();
            this.lblHistoryRemotePath = new System.Windows.Forms.Label();
            this.lblHistoryLocalPath = new System.Windows.Forms.Label();
            this.pbHistoryThumb = new System.Windows.Forms.PictureBox();
            this.cbReverse = new System.Windows.Forms.CheckBox();
            this.lbHistory = new System.Windows.Forms.ListBox();
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
            this.cbAddSpace = new System.Windows.Forms.CheckBox();
            this.btnCopyToClipboard = new System.Windows.Forms.Button();
            this.tpFile = new System.Windows.Forms.TabPage();
            this.tcFileSettings = new System.Windows.Forms.TabControl();
            this.tpCaptureCrop = new System.Windows.Forms.TabPage();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.nudCropBorderSize = new System.Windows.Forms.NumericUpDown();
            this.lblCropBorderSize = new System.Windows.Forms.Label();
            this.lblCropBorderColor = new System.Windows.Forms.Label();
            this.cbRegionRectangleInfo = new System.Windows.Forms.CheckBox();
            this.pbCropBorderColor = new System.Windows.Forms.PictureBox();
            this.cbRegionHotkeyInfo = new System.Windows.Forms.CheckBox();
            this.cbCropStyle = new System.Windows.Forms.ComboBox();
            this.tpFileNaming = new System.Windows.Forms.TabPage();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.lblEntireScreenPreview = new System.Windows.Forms.Label();
            this.txtEntireScreen = new System.Windows.Forms.TextBox();
            this.btnResetEntireScreen = new System.Windows.Forms.Button();
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
            this.label20 = new System.Windows.Forms.Label();
            this.lblCodeMi = new System.Windows.Forms.Label();
            this.lblCodeY = new System.Windows.Forms.Label();
            this.lblCodeH = new System.Windows.Forms.Label();
            this.gbAutoFileName = new System.Windows.Forms.GroupBox();
            this.lblActiveWindowPreview = new System.Windows.Forms.Label();
            this.txtActiveWindow = new System.Windows.Forms.TextBox();
            this.btnResetActiveWindow = new System.Windows.Forms.Button();
            this.tpFileSettingsWatermark = new System.Windows.Forms.TabPage();
            this.gbWatermarkPreview = new System.Windows.Forms.GroupBox();
            this.pbWatermarkShow = new System.Windows.Forms.PictureBox();
            this.gbWatermarkGeneral = new System.Windows.Forms.GroupBox();
            this.cbWatermarkPosition = new System.Windows.Forms.ComboBox();
            this.lblWatermarkPosition = new System.Windows.Forms.Label();
            this.nudWatermarkOffset = new System.Windows.Forms.NumericUpDown();
            this.lblWatermarkOffset = new System.Windows.Forms.Label();
            this.gbWatermarkBackground = new System.Windows.Forms.GroupBox();
            this.cbWatermarkGradientType = new System.Windows.Forms.ComboBox();
            this.lblWatermarkGradientType = new System.Windows.Forms.Label();
            this.lblWatermarkCornerRadiusTip = new System.Windows.Forms.Label();
            this.nudWatermarkCornerRadius = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.lblWatermarkBackColorsTip = new System.Windows.Forms.Label();
            this.nudWatermarkBackTrans = new System.Windows.Forms.NumericUpDown();
            this.lblWatermarkBackTransTip = new System.Windows.Forms.Label();
            this.lblWatermarkBackTrans = new System.Windows.Forms.Label();
            this.pbWatermarkGradient1 = new System.Windows.Forms.PictureBox();
            this.pbWatermarkBorderColor = new System.Windows.Forms.PictureBox();
            this.pbWatermarkGradient2 = new System.Windows.Forms.PictureBox();
            this.lblWatermarkBackColors = new System.Windows.Forms.Label();
            this.gbWatermarkText = new System.Windows.Forms.GroupBox();
            this.lblWatermarkTextTip = new System.Windows.Forms.Label();
            this.lblWatermarkText = new System.Windows.Forms.Label();
            this.lblWatermarkFontTransTip = new System.Windows.Forms.Label();
            this.lblWatermarkFont = new System.Windows.Forms.Label();
            this.nudWatermarkFontTrans = new System.Windows.Forms.NumericUpDown();
            this.btnWatermarkFont = new System.Windows.Forms.Button();
            this.lblWatermarkFontTrans = new System.Windows.Forms.Label();
            this.txtWatermarkText = new System.Windows.Forms.TextBox();
            this.pbWatermarkFontColor = new System.Windows.Forms.PictureBox();
            this.tpCaptureQuality = new System.Windows.Forms.TabPage();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtImageQuality = new System.Windows.Forms.NumericUpDown();
            this.label21 = new System.Windows.Forms.Label();
            this.lblQuality = new System.Windows.Forms.Label();
            this.cmbSwitchFormat = new System.Windows.Forms.ComboBox();
            this.lblFileFormat = new System.Windows.Forms.Label();
            this.cmbFileFormat = new System.Windows.Forms.ComboBox();
            this.lblKB = new System.Windows.Forms.Label();
            this.txtSwitchAfter = new System.Windows.Forms.TextBox();
            this.lblAfter = new System.Windows.Forms.Label();
            this.lblSwitchTo = new System.Windows.Forms.Label();
            this.cbDeleteLocal = new System.Windows.Forms.CheckBox();
            this.gbSaveLoc = new System.Windows.Forms.GroupBox();
            this.btnViewLocalDirectory = new System.Windows.Forms.Button();
            this.txtFileDirectory = new System.Windows.Forms.TextBox();
            this.btnBrowseDirectory = new System.Windows.Forms.Button();
            this.cbShowWatermark = new System.Windows.Forms.CheckBox();
            this.chkManualNaming = new System.Windows.Forms.CheckBox();
            this.cboClipboardTextMode = new System.Windows.Forms.ComboBox();
            this.chkEnableThumbnail = new System.Windows.Forms.CheckBox();
            this.tpImageSoftware = new System.Windows.Forms.TabPage();
            this.btnAddImageSoftware = new System.Windows.Forms.Button();
            this.gbImageSoftwareList = new System.Windows.Forms.GroupBox();
            this.lbImageSoftware = new System.Windows.Forms.ListBox();
            this.btnDeleteImageSoftware = new System.Windows.Forms.Button();
            this.gbImageSoftwareActive = new System.Windows.Forms.GroupBox();
            this.lblImageSoftwarePath = new System.Windows.Forms.Label();
            this.lblImageSoftwareName = new System.Windows.Forms.Label();
            this.btnUpdateImageSoftware = new System.Windows.Forms.Button();
            this.txtImageSoftwareName = new System.Windows.Forms.TextBox();
            this.btnBrowseImageSoftware = new System.Windows.Forms.Button();
            this.txtImageSoftwarePath = new System.Windows.Forms.TextBox();
            this.cbStartWin = new System.Windows.Forms.CheckBox();
            this.tpFTP = new System.Windows.Forms.TabPage();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.lbFTPAccounts = new System.Windows.Forms.ListBox();
            this.btnAccsImport = new System.Windows.Forms.Button();
            this.btnAccsExport = new System.Windows.Forms.Button();
            this.btnDeleteFTP = new System.Windows.Forms.Button();
            this.gbFTPAccountActive = new System.Windows.Forms.GroupBox();
            this.txtServerPort = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.gbFTPMode = new System.Windows.Forms.GroupBox();
            this.rbFTPActive = new System.Windows.Forms.RadioButton();
            this.rbFTPPassive = new System.Windows.Forms.RadioButton();
            this.lblPort = new System.Windows.Forms.Label();
            this.txtServer = new System.Windows.Forms.TextBox();
            this.lblServer = new System.Windows.Forms.Label();
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.lblUsername = new System.Windows.Forms.Label();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.lblHttpPath = new System.Windows.Forms.Label();
            this.lblPassword = new System.Windows.Forms.Label();
            this.txtHttpPath = new System.Windows.Forms.TextBox();
            this.txtPath = new System.Windows.Forms.TextBox();
            this.lblFtpPath = new System.Windows.Forms.Label();
            this.txtErrorFTP = new System.Windows.Forms.TextBox();
            this.btnTestConnection = new System.Windows.Forms.Button();
            this.btnUpdateFTP = new System.Windows.Forms.Button();
            this.btnAddAccount = new System.Windows.Forms.Button();
            this.tpHotKeys = new System.Windows.Forms.TabPage();
            this.lblHotkeyStatus = new System.Windows.Forms.Label();
            this.dgvHotkeys = new System.Windows.Forms.DataGridView();
            this.chHotkeys_Description = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.chHotkeys_Keys = new System.Windows.Forms.DataGridViewButtonColumn();
            this.tpMain = new System.Windows.Forms.TabPage();
            this.gbActiveHelp = new System.Windows.Forms.GroupBox();
            this.cbHelpToLanguage = new System.Windows.Forms.ComboBox();
            this.chkGTActiveHelp = new System.Windows.Forms.CheckBox();
            this.cbActiveHelp = new System.Windows.Forms.CheckBox();
            this.llProjectPage = new System.Windows.Forms.LinkLabel();
            this.llWebsite = new System.Windows.Forms.LinkLabel();
            this.llblBugReports = new System.Windows.Forms.LinkLabel();
            this.lblFirstRun = new System.Windows.Forms.Label();
            this.gbMainOptions = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.lblScreenshotDelay = new System.Windows.Forms.Label();
            this.cbShowCursor = new System.Windows.Forms.CheckBox();
            this.nScreenshotDelay = new System.Windows.Forms.NumericUpDown();
            this.lblScreenshotDestination = new System.Windows.Forms.Label();
            this.lblDelaySeconds = new System.Windows.Forms.Label();
            this.cbCompleteSound = new System.Windows.Forms.CheckBox();
            this.cboScreenshotDest = new System.Windows.Forms.ComboBox();
            this.lblLogo = new System.Windows.Forms.Label();
            this.pbLogo = new System.Windows.Forms.PictureBox();
            this.tcApp = new System.Windows.Forms.TabControl();
            this.tpHTTP = new System.Windows.Forms.TabPage();
            this.tcHTTP = new System.Windows.Forms.TabControl();
            this.tpImageUploaders = new System.Windows.Forms.TabPage();
            this.cboUploadMode = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.gbImageShack = new System.Windows.Forms.GroupBox();
            this.btnGalleryImageShack = new System.Windows.Forms.Button();
            this.btnRegCodeImageShack = new System.Windows.Forms.Button();
            this.lblImageShackRegistrationCode = new System.Windows.Forms.Label();
            this.txtImageShackRegistrationCode = new System.Windows.Forms.TextBox();
            this.gbTinyPic = new System.Windows.Forms.GroupBox();
            this.btnRegCodeTinyPic = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.txtTinyPicShuk = new System.Windows.Forms.TextBox();
            this.lblErrorRetry = new System.Windows.Forms.Label();
            this.nErrorRetry = new System.Windows.Forms.NumericUpDown();
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
            this.tpLanguageTranslator = new System.Windows.Forms.TabPage();
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
            this.tpAdvanced = new System.Windows.Forms.TabPage();
            this.tcAdvanced = new System.Windows.Forms.TabControl();
            this.tpAdvAppearance = new System.Windows.Forms.TabPage();
            this.gbAppearance = new System.Windows.Forms.GroupBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.chkBalloonTipOpenLink = new System.Windows.Forms.CheckBox();
            this.cbShowPopup = new System.Windows.Forms.CheckBox();
            this.lblTrayFlash = new System.Windows.Forms.Label();
            this.nudFlashIconCount = new System.Windows.Forms.NumericUpDown();
            this.gbMisc = new System.Windows.Forms.GroupBox();
            this.cbShowTaskbar = new System.Windows.Forms.CheckBox();
            this.cbOpenMainWindow = new System.Windows.Forms.CheckBox();
            this.cbCheckUpdates = new System.Windows.Forms.CheckBox();
            this.tpAdvPaths = new System.Windows.Forms.TabPage();
            this.gbSettingsExportImport = new System.Windows.Forms.GroupBox();
            this.btnDeleteSettings = new System.Windows.Forms.Button();
            this.btnSettingsExport = new System.Windows.Forms.Button();
            this.btnBrowseConfig = new System.Windows.Forms.Button();
            this.btnSettingsImport = new System.Windows.Forms.Button();
            this.gbRemoteDirCache = new System.Windows.Forms.GroupBox();
            this.btnViewRemoteDirectory = new System.Windows.Forms.Button();
            this.btnBrowseCacheLocation = new System.Windows.Forms.Button();
            this.lblCacheSize = new System.Windows.Forms.Label();
            this.lblMebibytes = new System.Windows.Forms.Label();
            this.nudCacheSize = new System.Windows.Forms.NumericUpDown();
            this.txtCacheDir = new System.Windows.Forms.TextBox();
            this.tpAdvDebug = new System.Windows.Forms.TabPage();
            this.lblDebugInfo = new System.Windows.Forms.Label();
            this.gbLastSource = new System.Windows.Forms.GroupBox();
            this.btnOpenSourceString = new System.Windows.Forms.Button();
            this.btnOpenSourceText = new System.Windows.Forms.Button();
            this.btnOpenSourceBrowser = new System.Windows.Forms.Button();
            this.ilApp = new System.Windows.Forms.ImageList(this.components);
            this.txtActiveHelp = new System.Windows.Forms.RichTextBox();
            this.splitContainerApp = new System.Windows.Forms.SplitContainer();
            this.debugTimer = new System.Windows.Forms.Timer(this.components);
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnCopyStats = new System.Windows.Forms.Button();
            this.cmTray.SuspendLayout();
            this.tpScreenshots.SuspendLayout();
            this.gbScreenshotPreview.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbHistoryThumb)).BeginInit();
            this.cmsHistory.SuspendLayout();
            this.tpFile.SuspendLayout();
            this.tcFileSettings.SuspendLayout();
            this.tpCaptureCrop.SuspendLayout();
            this.groupBox6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudCropBorderSize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbCropBorderColor)).BeginInit();
            this.tpFileNaming.SuspendLayout();
            this.groupBox8.SuspendLayout();
            this.gbCodeTitle.SuspendLayout();
            this.gbAutoFileName.SuspendLayout();
            this.tpFileSettingsWatermark.SuspendLayout();
            this.gbWatermarkPreview.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbWatermarkShow)).BeginInit();
            this.gbWatermarkGeneral.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudWatermarkOffset)).BeginInit();
            this.gbWatermarkBackground.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudWatermarkCornerRadius)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudWatermarkBackTrans)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbWatermarkGradient1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbWatermarkBorderColor)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbWatermarkGradient2)).BeginInit();
            this.gbWatermarkText.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudWatermarkFontTrans)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbWatermarkFontColor)).BeginInit();
            this.tpCaptureQuality.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtImageQuality)).BeginInit();
            this.gbSaveLoc.SuspendLayout();
            this.tpImageSoftware.SuspendLayout();
            this.gbImageSoftwareList.SuspendLayout();
            this.gbImageSoftwareActive.SuspendLayout();
            this.tpFTP.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.gbFTPAccountActive.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtServerPort)).BeginInit();
            this.gbFTPMode.SuspendLayout();
            this.tpHotKeys.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvHotkeys)).BeginInit();
            this.tpMain.SuspendLayout();
            this.gbActiveHelp.SuspendLayout();
            this.gbMainOptions.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nScreenshotDelay)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbLogo)).BeginInit();
            this.tcApp.SuspendLayout();
            this.tpHTTP.SuspendLayout();
            this.tcHTTP.SuspendLayout();
            this.tpImageUploaders.SuspendLayout();
            this.gbImageShack.SuspendLayout();
            this.gbTinyPic.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nErrorRetry)).BeginInit();
            this.tpCustomUploaders.SuspendLayout();
            this.gbImageUploaders.SuspendLayout();
            this.gbRegexp.SuspendLayout();
            this.gbArguments.SuspendLayout();
            this.tpLanguageTranslator.SuspendLayout();
            this.tpAdvanced.SuspendLayout();
            this.tcAdvanced.SuspendLayout();
            this.tpAdvAppearance.SuspendLayout();
            this.gbAppearance.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudFlashIconCount)).BeginInit();
            this.gbMisc.SuspendLayout();
            this.tpAdvPaths.SuspendLayout();
            this.gbSettingsExportImport.SuspendLayout();
            this.gbRemoteDirCache.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudCacheSize)).BeginInit();
            this.tpAdvDebug.SuspendLayout();
            this.gbLastSource.SuspendLayout();
            this.splitContainerApp.Panel1.SuspendLayout();
            this.splitContainerApp.Panel2.SuspendLayout();
            this.splitContainerApp.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // niTray
            // 
            this.niTray.ContextMenuStrip = this.cmTray;
            this.niTray.Icon = ((System.Drawing.Icon)(resources.GetObject("niTray.Icon")));
            this.niTray.Visible = true;
            this.niTray.MouseClick += new System.Windows.Forms.MouseEventHandler(this.niTray_MouseClick);
            this.niTray.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.niTray_MouseDoubleClick);
            // 
            // cmTray
            // 
            this.cmTray.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmSettings,
            this.toolStripSeparator4,
            this.tsmSendTo,
            this.tsmImageSoftware,
            this.tsmCbCopy,
            this.tsmPromptFileName,
            this.toolStripSeparator3,
            this.tsmViewRemote,
            this.tsmViewDirectory,
            this.toolStripSeparator1,
            this.captureToolStripMenuItem,
            this.tsmDropWindow,
            this.tsmQuickOptions,
            this.toolStripSeparator7,
            this.exitZScreenToolStripMenuItem});
            this.cmTray.Name = "cmTray";
            this.cmTray.Size = new System.Drawing.Size(206, 270);
            // 
            // tsmSettings
            // 
            this.tsmSettings.DoubleClickEnabled = true;
            this.tsmSettings.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmHotkeys,
            this.tsmFileSettings,
            this.tsmImageSoftwareSettings,
            this.tsmFTPSettings,
            this.tsmHTTPSettings,
            this.tsmAdvanced,
            this.tsmHistory,
            this.toolStripSeparator6,
            this.tsmLic,
            this.cmVersionHistory,
            this.tsmAboutMain});
            this.tsmSettings.Image = global::ZSS.Properties.Resources.wrench;
            this.tsmSettings.Name = "tsmSettings";
            this.tsmSettings.Size = new System.Drawing.Size(205, 22);
            this.tsmSettings.Text = "View Settings Menu";
            this.tsmSettings.Click += new System.EventHandler(this.tsmSettings_Click);
            // 
            // tsmHotkeys
            // 
            this.tsmHotkeys.Image = global::ZSS.Properties.Resources.keyboard;
            this.tsmHotkeys.Name = "tsmHotkeys";
            this.tsmHotkeys.Size = new System.Drawing.Size(165, 22);
            this.tsmHotkeys.Text = "Hotkeys...";
            this.tsmHotkeys.Click += new System.EventHandler(this.tsm_Click);
            // 
            // tsmFileSettings
            // 
            this.tsmFileSettings.Image = global::ZSS.Properties.Resources.camera_edit;
            this.tsmFileSettings.Name = "tsmFileSettings";
            this.tsmFileSettings.Size = new System.Drawing.Size(165, 22);
            this.tsmFileSettings.Text = "Capture...";
            this.tsmFileSettings.Click += new System.EventHandler(this.tsm_Click);
            // 
            // tsmImageSoftwareSettings
            // 
            this.tsmImageSoftwareSettings.Image = global::ZSS.Properties.Resources.picture_edit;
            this.tsmImageSoftwareSettings.Name = "tsmImageSoftwareSettings";
            this.tsmImageSoftwareSettings.Size = new System.Drawing.Size(165, 22);
            this.tsmImageSoftwareSettings.Text = "Image Software...";
            this.tsmImageSoftwareSettings.Click += new System.EventHandler(this.tsm_Click);
            // 
            // tsmFTPSettings
            // 
            this.tsmFTPSettings.Image = global::ZSS.Properties.Resources.server_edit;
            this.tsmFTPSettings.Name = "tsmFTPSettings";
            this.tsmFTPSettings.Size = new System.Drawing.Size(165, 22);
            this.tsmFTPSettings.Text = "FTP...";
            this.tsmFTPSettings.Click += new System.EventHandler(this.tsm_Click);
            // 
            // tsmHTTPSettings
            // 
            this.tsmHTTPSettings.Image = global::ZSS.Properties.Resources.world_edit;
            this.tsmHTTPSettings.Name = "tsmHTTPSettings";
            this.tsmHTTPSettings.Size = new System.Drawing.Size(165, 22);
            this.tsmHTTPSettings.Text = "HTTP...";
            this.tsmHTTPSettings.Click += new System.EventHandler(this.tsm_Click);
            // 
            // tsmAdvanced
            // 
            this.tsmAdvanced.Image = global::ZSS.Properties.Resources.application_edit;
            this.tsmAdvanced.Name = "tsmAdvanced";
            this.tsmAdvanced.Size = new System.Drawing.Size(165, 22);
            this.tsmAdvanced.Text = "Advanced...";
            this.tsmAdvanced.Click += new System.EventHandler(this.tsm_Click);
            // 
            // tsmHistory
            // 
            this.tsmHistory.Image = global::ZSS.Properties.Resources.pictures;
            this.tsmHistory.Name = "tsmHistory";
            this.tsmHistory.Size = new System.Drawing.Size(165, 22);
            this.tsmHistory.Text = "History...";
            this.tsmHistory.Click += new System.EventHandler(this.tsm_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(162, 6);
            // 
            // tsmLic
            // 
            this.tsmLic.Name = "tsmLic";
            this.tsmLic.Size = new System.Drawing.Size(165, 22);
            this.tsmLic.Text = "License...";
            this.tsmLic.Click += new System.EventHandler(this.tsmLic_Click);
            // 
            // cmVersionHistory
            // 
            this.cmVersionHistory.Name = "cmVersionHistory";
            this.cmVersionHistory.Size = new System.Drawing.Size(165, 22);
            this.cmVersionHistory.Text = "&Version History...";
            this.cmVersionHistory.Click += new System.EventHandler(this.cmVersionHistory_Click);
            // 
            // tsmAboutMain
            // 
            this.tsmAboutMain.Image = global::ZSS.Properties.Resources.info;
            this.tsmAboutMain.Name = "tsmAboutMain";
            this.tsmAboutMain.Size = new System.Drawing.Size(165, 22);
            this.tsmAboutMain.Text = "About...";
            this.tsmAboutMain.Click += new System.EventHandler(this.tsmAboutMain_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(202, 6);
            // 
            // tsmSendTo
            // 
            this.tsmSendTo.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmDestClipboard,
            this.tsmDestFile,
            this.tsmDestFTP,
            this.tsmDestImageShack,
            this.tsmDestTinyPic,
            this.tsmDestCustomHTTP});
            this.tsmSendTo.Image = global::ZSS.Properties.Resources.picture_go;
            this.tsmSendTo.Name = "tsmSendTo";
            this.tsmSendTo.Size = new System.Drawing.Size(205, 22);
            this.tsmSendTo.Text = "Send Image To";
            // 
            // tsmDestClipboard
            // 
            this.tsmDestClipboard.Name = "tsmDestClipboard";
            this.tsmDestClipboard.Size = new System.Drawing.Size(167, 22);
            this.tsmDestClipboard.Text = "Clipboard";
            this.tsmDestClipboard.Click += new System.EventHandler(this.tsmDestClipboard_Click);
            // 
            // tsmDestFile
            // 
            this.tsmDestFile.Name = "tsmDestFile";
            this.tsmDestFile.Size = new System.Drawing.Size(167, 22);
            this.tsmDestFile.Text = "File";
            this.tsmDestFile.Click += new System.EventHandler(this.tsmDestFile_Click);
            // 
            // tsmDestFTP
            // 
            this.tsmDestFTP.Name = "tsmDestFTP";
            this.tsmDestFTP.Size = new System.Drawing.Size(167, 22);
            this.tsmDestFTP.Text = "FTP";
            this.tsmDestFTP.Click += new System.EventHandler(this.tsmDestFTP_Click);
            // 
            // tsmDestImageShack
            // 
            this.tsmDestImageShack.Name = "tsmDestImageShack";
            this.tsmDestImageShack.Size = new System.Drawing.Size(167, 22);
            this.tsmDestImageShack.Text = "ImageShack";
            this.tsmDestImageShack.Click += new System.EventHandler(this.tsmDestImageShack_Click);
            // 
            // tsmDestTinyPic
            // 
            this.tsmDestTinyPic.Name = "tsmDestTinyPic";
            this.tsmDestTinyPic.Size = new System.Drawing.Size(167, 22);
            this.tsmDestTinyPic.Text = "TinyPic";
            this.tsmDestTinyPic.Click += new System.EventHandler(this.tsmDestTinyPic_Click);
            // 
            // tsmDestCustomHTTP
            // 
            this.tsmDestCustomHTTP.Name = "tsmDestCustomHTTP";
            this.tsmDestCustomHTTP.Size = new System.Drawing.Size(167, 22);
            this.tsmDestCustomHTTP.Text = "&Custom Uploader";
            this.tsmDestCustomHTTP.Click += new System.EventHandler(this.tsmDestCustomHTTP_Click);
            // 
            // tsmImageSoftware
            // 
            this.tsmImageSoftware.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.tsmImageSoftware.Image = global::ZSS.Properties.Resources.picture_edit;
            this.tsmImageSoftware.Name = "tsmImageSoftware";
            this.tsmImageSoftware.Size = new System.Drawing.Size(205, 22);
            this.tsmImageSoftware.Text = "Edit in Image Software";
            // 
            // tsmCbCopy
            // 
            this.tsmCbCopy.Image = global::ZSS.Properties.Resources.page_copy;
            this.tsmCbCopy.Name = "tsmCbCopy";
            this.tsmCbCopy.Size = new System.Drawing.Size(205, 22);
            this.tsmCbCopy.Text = "Copy to Clipboard Mode";
            // 
            // tsmPromptFileName
            // 
            this.tsmPromptFileName.Name = "tsmPromptFileName";
            this.tsmPromptFileName.Size = new System.Drawing.Size(205, 22);
            this.tsmPromptFileName.Text = "Prompt for File Name";
            this.tsmPromptFileName.Click += new System.EventHandler(this.tsmPromptFileName_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(202, 6);
            // 
            // tsmViewRemote
            // 
            this.tsmViewRemote.Name = "tsmViewRemote";
            this.tsmViewRemote.Size = new System.Drawing.Size(205, 22);
            this.tsmViewRemote.Text = "View Remote Directory...";
            this.tsmViewRemote.Click += new System.EventHandler(this.tsmViewRemote_Click);
            // 
            // tsmViewDirectory
            // 
            this.tsmViewDirectory.Name = "tsmViewDirectory";
            this.tsmViewDirectory.Size = new System.Drawing.Size(205, 22);
            this.tsmViewDirectory.Text = "View Local Directory...";
            this.tsmViewDirectory.Click += new System.EventHandler(this.tsmViewDirectory_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(202, 6);
            // 
            // captureToolStripMenuItem
            // 
            this.captureToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.entireScreenToolStripMenuItem,
            this.rectangularRegionToolStripMenuItem,
            this.lastRectangularRegionToolStripMenuItem,
            this.tsmScrenshotFromClipboard});
            this.captureToolStripMenuItem.Image = global::ZSS.Properties.Resources.camera;
            this.captureToolStripMenuItem.Name = "captureToolStripMenuItem";
            this.captureToolStripMenuItem.Size = new System.Drawing.Size(205, 22);
            this.captureToolStripMenuItem.Text = "&Capture";
            // 
            // entireScreenToolStripMenuItem
            // 
            this.entireScreenToolStripMenuItem.Name = "entireScreenToolStripMenuItem";
            this.entireScreenToolStripMenuItem.Size = new System.Drawing.Size(232, 22);
            this.entireScreenToolStripMenuItem.Text = "&Entire Screen";
            this.entireScreenToolStripMenuItem.Click += new System.EventHandler(this.entireScreenToolStripMenuItem_Click);
            // 
            // rectangularRegionToolStripMenuItem
            // 
            this.rectangularRegionToolStripMenuItem.Name = "rectangularRegionToolStripMenuItem";
            this.rectangularRegionToolStripMenuItem.Size = new System.Drawing.Size(232, 22);
            this.rectangularRegionToolStripMenuItem.Text = "&Crop Shot";
            this.rectangularRegionToolStripMenuItem.Click += new System.EventHandler(this.rectangularRegionToolStripMenuItem_Click);
            // 
            // lastRectangularRegionToolStripMenuItem
            // 
            this.lastRectangularRegionToolStripMenuItem.Name = "lastRectangularRegionToolStripMenuItem";
            this.lastRectangularRegionToolStripMenuItem.Size = new System.Drawing.Size(232, 22);
            this.lastRectangularRegionToolStripMenuItem.Text = "Last Crop Shot";
            this.lastRectangularRegionToolStripMenuItem.Click += new System.EventHandler(this.lastRectangularRegionToolStripMenuItem_Click);
            // 
            // tsmScrenshotFromClipboard
            // 
            this.tsmScrenshotFromClipboard.Name = "tsmScrenshotFromClipboard";
            this.tsmScrenshotFromClipboard.Size = new System.Drawing.Size(232, 22);
            this.tsmScrenshotFromClipboard.Text = "Image Upload from &Clipboard";
            this.tsmScrenshotFromClipboard.Click += new System.EventHandler(this.tsmScrenshotFromClipboard_Click);
            // 
            // tsmDropWindow
            // 
            this.tsmDropWindow.Name = "tsmDropWindow";
            this.tsmDropWindow.Size = new System.Drawing.Size(205, 22);
            this.tsmDropWindow.Text = "&Drag && Drop Window...";
            this.tsmDropWindow.Click += new System.EventHandler(this.tsmDropWindow_Click);
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
            // exitZScreenToolStripMenuItem
            // 
            this.exitZScreenToolStripMenuItem.Image = global::ZSS.Properties.Resources.cross;
            this.exitZScreenToolStripMenuItem.Name = "exitZScreenToolStripMenuItem";
            this.exitZScreenToolStripMenuItem.Size = new System.Drawing.Size(205, 22);
            this.exitZScreenToolStripMenuItem.Text = "Exit ZScreen";
            this.exitZScreenToolStripMenuItem.Click += new System.EventHandler(this.exitZScreenToolStripMenuItem_Click);
            // 
            // tpScreenshots
            // 
            this.tpScreenshots.Controls.Add(this.gbScreenshotPreview);
            this.tpScreenshots.Controls.Add(this.cbReverse);
            this.tpScreenshots.Controls.Add(this.lbHistory);
            this.tpScreenshots.Controls.Add(this.cbAddSpace);
            this.tpScreenshots.Controls.Add(this.btnCopyToClipboard);
            this.tpScreenshots.ImageKey = "pictures.png";
            this.tpScreenshots.Location = new System.Drawing.Point(4, 23);
            this.tpScreenshots.Name = "tpScreenshots";
            this.tpScreenshots.Size = new System.Drawing.Size(786, 428);
            this.tpScreenshots.TabIndex = 8;
            this.tpScreenshots.Text = "History";
            this.tpScreenshots.UseVisualStyleBackColor = true;
            // 
            // gbScreenshotPreview
            // 
            this.gbScreenshotPreview.Controls.Add(this.btnScreenshotBrowse);
            this.gbScreenshotPreview.Controls.Add(this.btnScreenshotOpen);
            this.gbScreenshotPreview.Controls.Add(this.txtHistoryRemotePath);
            this.gbScreenshotPreview.Controls.Add(this.txtHistoryLocalPath);
            this.gbScreenshotPreview.Controls.Add(this.lblHistoryRemotePath);
            this.gbScreenshotPreview.Controls.Add(this.lblHistoryLocalPath);
            this.gbScreenshotPreview.Controls.Add(this.pbHistoryThumb);
            this.gbScreenshotPreview.Location = new System.Drawing.Point(400, 8);
            this.gbScreenshotPreview.Name = "gbScreenshotPreview";
            this.gbScreenshotPreview.Size = new System.Drawing.Size(376, 408);
            this.gbScreenshotPreview.TabIndex = 0;
            this.gbScreenshotPreview.TabStop = false;
            this.gbScreenshotPreview.Text = "Screenshot";
            // 
            // btnScreenshotBrowse
            // 
            this.btnScreenshotBrowse.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnScreenshotBrowse.Enabled = false;
            this.btnScreenshotBrowse.Location = new System.Drawing.Point(192, 272);
            this.btnScreenshotBrowse.Name = "btnScreenshotBrowse";
            this.btnScreenshotBrowse.Size = new System.Drawing.Size(104, 23);
            this.btnScreenshotBrowse.TabIndex = 10;
            this.btnScreenshotBrowse.Text = "Browse &URL";
            this.btnScreenshotBrowse.UseVisualStyleBackColor = true;
            this.btnScreenshotBrowse.Click += new System.EventHandler(this.btnScreenshotBrowse_Click);
            // 
            // btnScreenshotOpen
            // 
            this.btnScreenshotOpen.AutoSize = true;
            this.btnScreenshotOpen.Enabled = false;
            this.btnScreenshotOpen.Location = new System.Drawing.Point(80, 272);
            this.btnScreenshotOpen.Name = "btnScreenshotOpen";
            this.btnScreenshotOpen.Size = new System.Drawing.Size(104, 23);
            this.btnScreenshotOpen.TabIndex = 9;
            this.btnScreenshotOpen.Text = "&Open Local File";
            this.btnScreenshotOpen.UseVisualStyleBackColor = true;
            this.btnScreenshotOpen.Click += new System.EventHandler(this.btnScreenshotOpen_Click);
            // 
            // txtHistoryRemotePath
            // 
            this.txtHistoryRemotePath.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtHistoryRemotePath.Location = new System.Drawing.Point(8, 376);
            this.txtHistoryRemotePath.Name = "txtHistoryRemotePath";
            this.txtHistoryRemotePath.ReadOnly = true;
            this.txtHistoryRemotePath.Size = new System.Drawing.Size(360, 20);
            this.txtHistoryRemotePath.TabIndex = 8;
            // 
            // txtHistoryLocalPath
            // 
            this.txtHistoryLocalPath.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtHistoryLocalPath.Location = new System.Drawing.Point(8, 321);
            this.txtHistoryLocalPath.Name = "txtHistoryLocalPath";
            this.txtHistoryLocalPath.ReadOnly = true;
            this.txtHistoryLocalPath.Size = new System.Drawing.Size(360, 20);
            this.txtHistoryLocalPath.TabIndex = 7;
            // 
            // lblHistoryRemotePath
            // 
            this.lblHistoryRemotePath.AutoSize = true;
            this.lblHistoryRemotePath.Location = new System.Drawing.Point(16, 355);
            this.lblHistoryRemotePath.Name = "lblHistoryRemotePath";
            this.lblHistoryRemotePath.Size = new System.Drawing.Size(69, 13);
            this.lblHistoryRemotePath.TabIndex = 6;
            this.lblHistoryRemotePath.Text = "Remote Path";
            // 
            // lblHistoryLocalPath
            // 
            this.lblHistoryLocalPath.AutoSize = true;
            this.lblHistoryLocalPath.Location = new System.Drawing.Point(16, 300);
            this.lblHistoryLocalPath.Name = "lblHistoryLocalPath";
            this.lblHistoryLocalPath.Size = new System.Drawing.Size(58, 13);
            this.lblHistoryLocalPath.TabIndex = 5;
            this.lblHistoryLocalPath.Text = "Local Path";
            // 
            // pbHistoryThumb
            // 
            this.pbHistoryThumb.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbHistoryThumb.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pbHistoryThumb.Location = new System.Drawing.Point(8, 16);
            this.pbHistoryThumb.Name = "pbHistoryThumb";
            this.pbHistoryThumb.Size = new System.Drawing.Size(360, 248);
            this.pbHistoryThumb.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbHistoryThumb.TabIndex = 4;
            this.pbHistoryThumb.TabStop = false;
            this.pbHistoryThumb.Click += new System.EventHandler(this.pbHistoryThumb_Click);
            // 
            // cbReverse
            // 
            this.cbReverse.AutoSize = true;
            this.cbReverse.BackColor = System.Drawing.Color.Transparent;
            this.cbReverse.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.cbReverse.Location = new System.Drawing.Point(8, 32);
            this.cbReverse.Name = "cbReverse";
            this.cbReverse.Size = new System.Drawing.Size(85, 17);
            this.cbReverse.TabIndex = 1;
            this.cbReverse.Text = "Reverse List";
            this.cbReverse.UseVisualStyleBackColor = false;
            // 
            // lbHistory
            // 
            this.lbHistory.ContextMenuStrip = this.cmsHistory;
            this.lbHistory.FormattingEnabled = true;
            this.lbHistory.IntegralHeight = false;
            this.lbHistory.Location = new System.Drawing.Point(8, 64);
            this.lbHistory.Name = "lbHistory";
            this.lbHistory.ScrollAlwaysVisible = true;
            this.lbHistory.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lbHistory.Size = new System.Drawing.Size(384, 352);
            this.lbHistory.TabIndex = 2;
            this.lbHistory.SelectedIndexChanged += new System.EventHandler(this.lbHistory_SelectedIndexChanged);
            this.lbHistory.DoubleClick += new System.EventHandler(this.lbHistory_DoubleClick);
            this.lbHistory.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lbHistory_MouseDown);
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
            // cbAddSpace
            // 
            this.cbAddSpace.AutoSize = true;
            this.cbAddSpace.BackColor = System.Drawing.Color.Transparent;
            this.cbAddSpace.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.cbAddSpace.Location = new System.Drawing.Point(8, 13);
            this.cbAddSpace.Name = "cbAddSpace";
            this.cbAddSpace.Size = new System.Drawing.Size(183, 17);
            this.cbAddSpace.TabIndex = 0;
            this.cbAddSpace.Tag = "Adding a New Line before the URLs makes it look nicer when you copy a URL List in" +
                " IM such as Pidgin";
            this.cbAddSpace.Text = "Add a New Line before the URLs";
            this.cbAddSpace.UseVisualStyleBackColor = false;
            // 
            // btnCopyToClipboard
            // 
            this.btnCopyToClipboard.BackColor = System.Drawing.Color.Transparent;
            this.btnCopyToClipboard.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.btnCopyToClipboard.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnCopyToClipboard.Location = new System.Drawing.Point(280, 16);
            this.btnCopyToClipboard.Name = "btnCopyToClipboard";
            this.btnCopyToClipboard.Size = new System.Drawing.Size(113, 39);
            this.btnCopyToClipboard.TabIndex = 3;
            this.btnCopyToClipboard.Text = "Copy to Clipboard";
            this.btnCopyToClipboard.UseVisualStyleBackColor = false;
            this.btnCopyToClipboard.Click += new System.EventHandler(this.btnCopyToClipboard_Click);
            // 
            // tpFile
            // 
            this.tpFile.Controls.Add(this.tcFileSettings);
            this.tpFile.ImageKey = "camera_edit.png";
            this.tpFile.Location = new System.Drawing.Point(4, 23);
            this.tpFile.Name = "tpFile";
            this.tpFile.Size = new System.Drawing.Size(786, 428);
            this.tpFile.TabIndex = 4;
            this.tpFile.Text = "Capture";
            this.tpFile.UseVisualStyleBackColor = true;
            // 
            // tcFileSettings
            // 
            this.tcFileSettings.Controls.Add(this.tpCaptureCrop);
            this.tcFileSettings.Controls.Add(this.tpFileNaming);
            this.tcFileSettings.Controls.Add(this.tpFileSettingsWatermark);
            this.tcFileSettings.Controls.Add(this.tpCaptureQuality);
            this.tcFileSettings.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcFileSettings.Location = new System.Drawing.Point(0, 0);
            this.tcFileSettings.Name = "tcFileSettings";
            this.tcFileSettings.SelectedIndex = 0;
            this.tcFileSettings.Size = new System.Drawing.Size(786, 428);
            this.tcFileSettings.TabIndex = 77;
            // 
            // tpCaptureCrop
            // 
            this.tpCaptureCrop.Controls.Add(this.groupBox6);
            this.tpCaptureCrop.Location = new System.Drawing.Point(4, 22);
            this.tpCaptureCrop.Name = "tpCaptureCrop";
            this.tpCaptureCrop.Size = new System.Drawing.Size(778, 402);
            this.tpCaptureCrop.TabIndex = 4;
            this.tpCaptureCrop.Text = "Crop Shots";
            this.tpCaptureCrop.UseVisualStyleBackColor = true;
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.nudCropBorderSize);
            this.groupBox6.Controls.Add(this.lblCropBorderSize);
            this.groupBox6.Controls.Add(this.lblCropBorderColor);
            this.groupBox6.Controls.Add(this.cbRegionRectangleInfo);
            this.groupBox6.Controls.Add(this.pbCropBorderColor);
            this.groupBox6.Controls.Add(this.cbRegionHotkeyInfo);
            this.groupBox6.Controls.Add(this.cbCropStyle);
            this.groupBox6.Location = new System.Drawing.Point(8, 8);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(760, 176);
            this.groupBox6.TabIndex = 13;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Options";
            // 
            // nudCropBorderSize
            // 
            this.nudCropBorderSize.Location = new System.Drawing.Point(224, 136);
            this.nudCropBorderSize.Name = "nudCropBorderSize";
            this.nudCropBorderSize.Size = new System.Drawing.Size(40, 20);
            this.nudCropBorderSize.TabIndex = 12;
            this.nudCropBorderSize.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudCropBorderSize.ValueChanged += new System.EventHandler(this.nudCropBorderSize_ValueChanged);
            // 
            // lblCropBorderSize
            // 
            this.lblCropBorderSize.AutoSize = true;
            this.lblCropBorderSize.Location = new System.Drawing.Point(19, 144);
            this.lblCropBorderSize.Name = "lblCropBorderSize";
            this.lblCropBorderSize.Size = new System.Drawing.Size(195, 13);
            this.lblCropBorderSize.TabIndex = 11;
            this.lblCropBorderSize.Text = "Crop region border size ( 0 = No border )";
            // 
            // lblCropBorderColor
            // 
            this.lblCropBorderColor.AutoSize = true;
            this.lblCropBorderColor.Location = new System.Drawing.Point(19, 120);
            this.lblCropBorderColor.Name = "lblCropBorderColor";
            this.lblCropBorderColor.Size = new System.Drawing.Size(120, 13);
            this.lblCropBorderColor.TabIndex = 10;
            this.lblCropBorderColor.Text = "Crop region border color";
            // 
            // cbRegionRectangleInfo
            // 
            this.cbRegionRectangleInfo.AutoSize = true;
            this.cbRegionRectangleInfo.Checked = true;
            this.cbRegionRectangleInfo.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbRegionRectangleInfo.Location = new System.Drawing.Point(19, 24);
            this.cbRegionRectangleInfo.Name = "cbRegionRectangleInfo";
            this.cbRegionRectangleInfo.Size = new System.Drawing.Size(210, 17);
            this.cbRegionRectangleInfo.TabIndex = 5;
            this.cbRegionRectangleInfo.Text = "Show Crop region coordinates and size";
            this.cbRegionRectangleInfo.UseVisualStyleBackColor = true;
            this.cbRegionRectangleInfo.CheckedChanged += new System.EventHandler(this.cbRegionRectangleInfo_CheckedChanged);
            // 
            // pbCropBorderColor
            // 
            this.pbCropBorderColor.BackColor = System.Drawing.Color.White;
            this.pbCropBorderColor.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pbCropBorderColor.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pbCropBorderColor.Location = new System.Drawing.Point(152, 112);
            this.pbCropBorderColor.Name = "pbCropBorderColor";
            this.pbCropBorderColor.Size = new System.Drawing.Size(24, 24);
            this.pbCropBorderColor.TabIndex = 9;
            this.pbCropBorderColor.TabStop = false;
            this.pbCropBorderColor.Click += new System.EventHandler(this.pbCropBorderColor_Click);
            // 
            // cbRegionHotkeyInfo
            // 
            this.cbRegionHotkeyInfo.AutoSize = true;
            this.cbRegionHotkeyInfo.Checked = true;
            this.cbRegionHotkeyInfo.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbRegionHotkeyInfo.Location = new System.Drawing.Point(19, 50);
            this.cbRegionHotkeyInfo.Name = "cbRegionHotkeyInfo";
            this.cbRegionHotkeyInfo.Size = new System.Drawing.Size(196, 17);
            this.cbRegionHotkeyInfo.TabIndex = 6;
            this.cbRegionHotkeyInfo.Text = "Show Crop region hotkey instruction";
            this.cbRegionHotkeyInfo.UseVisualStyleBackColor = true;
            this.cbRegionHotkeyInfo.CheckedChanged += new System.EventHandler(this.cbRegionHotkeyInfo_CheckedChanged);
            // 
            // cbCropStyle
            // 
            this.cbCropStyle.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbCropStyle.FormattingEnabled = true;
            this.cbCropStyle.Items.AddRange(new object[] {
            "No Transparency",
            "Crop Region Transparent",
            "Background Region Transparent"});
            this.cbCropStyle.Location = new System.Drawing.Point(19, 80);
            this.cbCropStyle.Name = "cbCropStyle";
            this.cbCropStyle.Size = new System.Drawing.Size(240, 21);
            this.cbCropStyle.TabIndex = 8;
            this.cbCropStyle.SelectedIndexChanged += new System.EventHandler(this.cbCropStyle_SelectedIndexChanged);
            // 
            // tpFileNaming
            // 
            this.tpFileNaming.Controls.Add(this.groupBox8);
            this.tpFileNaming.Controls.Add(this.gbCodeTitle);
            this.tpFileNaming.Controls.Add(this.gbAutoFileName);
            this.tpFileNaming.Location = new System.Drawing.Point(4, 22);
            this.tpFileNaming.Name = "tpFileNaming";
            this.tpFileNaming.Size = new System.Drawing.Size(778, 402);
            this.tpFileNaming.TabIndex = 3;
            this.tpFileNaming.Text = "Naming Conventions";
            this.tpFileNaming.UseVisualStyleBackColor = true;
            // 
            // groupBox8
            // 
            this.groupBox8.Controls.Add(this.lblEntireScreenPreview);
            this.groupBox8.Controls.Add(this.txtEntireScreen);
            this.groupBox8.Controls.Add(this.btnResetEntireScreen);
            this.groupBox8.Location = new System.Drawing.Point(240, 8);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Size = new System.Drawing.Size(376, 80);
            this.groupBox8.TabIndex = 115;
            this.groupBox8.TabStop = false;
            this.groupBox8.Text = "Clipboard / Crop / Full Screen:";
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
            this.txtEntireScreen.Location = new System.Drawing.Point(16, 24);
            this.txtEntireScreen.Name = "txtEntireScreen";
            this.txtEntireScreen.Size = new System.Drawing.Size(257, 20);
            this.txtEntireScreen.TabIndex = 4;
            this.txtEntireScreen.TextChanged += new System.EventHandler(this.txtEntireScreen_TextChanged);
            this.txtEntireScreen.Leave += new System.EventHandler(this.txtEntireScreen_Leave);
            // 
            // btnResetEntireScreen
            // 
            this.btnResetEntireScreen.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnResetEntireScreen.Location = new System.Drawing.Point(280, 24);
            this.btnResetEntireScreen.Name = "btnResetEntireScreen";
            this.btnResetEntireScreen.Size = new System.Drawing.Size(80, 23);
            this.btnResetEntireScreen.TabIndex = 5;
            this.btnResetEntireScreen.Text = "Reset";
            this.btnResetEntireScreen.UseVisualStyleBackColor = true;
            this.btnResetEntireScreen.Click += new System.EventHandler(this.btnResetEntireScreen_Click);
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
            this.gbCodeTitle.Controls.Add(this.label20);
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
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label20.Location = new System.Drawing.Point(83, 7);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(0, 13);
            this.label20.TabIndex = 78;
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
            // gbAutoFileName
            // 
            this.gbAutoFileName.BackColor = System.Drawing.Color.Transparent;
            this.gbAutoFileName.Controls.Add(this.lblActiveWindowPreview);
            this.gbAutoFileName.Controls.Add(this.txtActiveWindow);
            this.gbAutoFileName.Controls.Add(this.btnResetActiveWindow);
            this.gbAutoFileName.Location = new System.Drawing.Point(240, 96);
            this.gbAutoFileName.Name = "gbAutoFileName";
            this.gbAutoFileName.Size = new System.Drawing.Size(376, 80);
            this.gbAutoFileName.TabIndex = 113;
            this.gbAutoFileName.TabStop = false;
            this.gbAutoFileName.Text = "Active Window";
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
            this.txtActiveWindow.Location = new System.Drawing.Point(16, 24);
            this.txtActiveWindow.Name = "txtActiveWindow";
            this.txtActiveWindow.Size = new System.Drawing.Size(257, 20);
            this.txtActiveWindow.TabIndex = 2;
            this.txtActiveWindow.TextChanged += new System.EventHandler(this.txtActiveWindow_TextChanged);
            this.txtActiveWindow.Leave += new System.EventHandler(this.txtActiveWindow_Leave);
            // 
            // btnResetActiveWindow
            // 
            this.btnResetActiveWindow.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnResetActiveWindow.Location = new System.Drawing.Point(280, 24);
            this.btnResetActiveWindow.Name = "btnResetActiveWindow";
            this.btnResetActiveWindow.Size = new System.Drawing.Size(80, 23);
            this.btnResetActiveWindow.TabIndex = 3;
            this.btnResetActiveWindow.Text = "Reset";
            this.btnResetActiveWindow.UseVisualStyleBackColor = true;
            this.btnResetActiveWindow.Click += new System.EventHandler(this.btnResetActiveWindow_Click);
            // 
            // tpFileSettingsWatermark
            // 
            this.tpFileSettingsWatermark.Controls.Add(this.gbWatermarkPreview);
            this.tpFileSettingsWatermark.Controls.Add(this.gbWatermarkGeneral);
            this.tpFileSettingsWatermark.Controls.Add(this.gbWatermarkBackground);
            this.tpFileSettingsWatermark.Controls.Add(this.gbWatermarkText);
            this.tpFileSettingsWatermark.Location = new System.Drawing.Point(4, 22);
            this.tpFileSettingsWatermark.Name = "tpFileSettingsWatermark";
            this.tpFileSettingsWatermark.Padding = new System.Windows.Forms.Padding(3);
            this.tpFileSettingsWatermark.Size = new System.Drawing.Size(778, 402);
            this.tpFileSettingsWatermark.TabIndex = 2;
            this.tpFileSettingsWatermark.Text = "Watermark";
            this.tpFileSettingsWatermark.UseVisualStyleBackColor = true;
            // 
            // gbWatermarkPreview
            // 
            this.gbWatermarkPreview.Controls.Add(this.pbWatermarkShow);
            this.gbWatermarkPreview.Location = new System.Drawing.Point(536, 8);
            this.gbWatermarkPreview.Name = "gbWatermarkPreview";
            this.gbWatermarkPreview.Size = new System.Drawing.Size(216, 176);
            this.gbWatermarkPreview.TabIndex = 28;
            this.gbWatermarkPreview.TabStop = false;
            this.gbWatermarkPreview.Text = "Watermark Preview";
            // 
            // pbWatermarkShow
            // 
            this.pbWatermarkShow.BackColor = System.Drawing.Color.White;
            this.pbWatermarkShow.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbWatermarkShow.Location = new System.Drawing.Point(8, 24);
            this.pbWatermarkShow.Name = "pbWatermarkShow";
            this.pbWatermarkShow.Size = new System.Drawing.Size(200, 136);
            this.pbWatermarkShow.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pbWatermarkShow.TabIndex = 13;
            this.pbWatermarkShow.TabStop = false;
            // 
            // gbWatermarkGeneral
            // 
            this.gbWatermarkGeneral.Controls.Add(this.cbWatermarkPosition);
            this.gbWatermarkGeneral.Controls.Add(this.lblWatermarkPosition);
            this.gbWatermarkGeneral.Controls.Add(this.nudWatermarkOffset);
            this.gbWatermarkGeneral.Controls.Add(this.lblWatermarkOffset);
            this.gbWatermarkGeneral.Location = new System.Drawing.Point(8, 8);
            this.gbWatermarkGeneral.Name = "gbWatermarkGeneral";
            this.gbWatermarkGeneral.Size = new System.Drawing.Size(520, 88);
            this.gbWatermarkGeneral.TabIndex = 26;
            this.gbWatermarkGeneral.TabStop = false;
            this.gbWatermarkGeneral.Text = "General Settings";
            // 
            // cbWatermarkPosition
            // 
            this.cbWatermarkPosition.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbWatermarkPosition.FormattingEnabled = true;
            this.cbWatermarkPosition.Location = new System.Drawing.Point(120, 16);
            this.cbWatermarkPosition.Name = "cbWatermarkPosition";
            this.cbWatermarkPosition.Size = new System.Drawing.Size(121, 21);
            this.cbWatermarkPosition.TabIndex = 18;
            this.cbWatermarkPosition.SelectedIndexChanged += new System.EventHandler(this.cbWatermarkPosition_SelectedIndexChanged);
            // 
            // lblWatermarkPosition
            // 
            this.lblWatermarkPosition.AutoSize = true;
            this.lblWatermarkPosition.Location = new System.Drawing.Point(16, 24);
            this.lblWatermarkPosition.Name = "lblWatermarkPosition";
            this.lblWatermarkPosition.Size = new System.Drawing.Size(101, 13);
            this.lblWatermarkPosition.TabIndex = 19;
            this.lblWatermarkPosition.Text = "Watermark position:";
            // 
            // nudWatermarkOffset
            // 
            this.nudWatermarkOffset.Location = new System.Drawing.Point(64, 48);
            this.nudWatermarkOffset.Maximum = new decimal(new int[] {
            2000,
            0,
            0,
            0});
            this.nudWatermarkOffset.Name = "nudWatermarkOffset";
            this.nudWatermarkOffset.Size = new System.Drawing.Size(64, 20);
            this.nudWatermarkOffset.TabIndex = 6;
            this.nudWatermarkOffset.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.nudWatermarkOffset.ValueChanged += new System.EventHandler(this.nudWatermarkOffset_ValueChanged);
            // 
            // lblWatermarkOffset
            // 
            this.lblWatermarkOffset.AutoSize = true;
            this.lblWatermarkOffset.Location = new System.Drawing.Point(16, 56);
            this.lblWatermarkOffset.Name = "lblWatermarkOffset";
            this.lblWatermarkOffset.Size = new System.Drawing.Size(38, 13);
            this.lblWatermarkOffset.TabIndex = 5;
            this.lblWatermarkOffset.Text = "Offset:";
            // 
            // gbWatermarkBackground
            // 
            this.gbWatermarkBackground.Controls.Add(this.cbWatermarkGradientType);
            this.gbWatermarkBackground.Controls.Add(this.lblWatermarkGradientType);
            this.gbWatermarkBackground.Controls.Add(this.lblWatermarkCornerRadiusTip);
            this.gbWatermarkBackground.Controls.Add(this.nudWatermarkCornerRadius);
            this.gbWatermarkBackground.Controls.Add(this.label5);
            this.gbWatermarkBackground.Controls.Add(this.lblWatermarkBackColorsTip);
            this.gbWatermarkBackground.Controls.Add(this.nudWatermarkBackTrans);
            this.gbWatermarkBackground.Controls.Add(this.lblWatermarkBackTransTip);
            this.gbWatermarkBackground.Controls.Add(this.lblWatermarkBackTrans);
            this.gbWatermarkBackground.Controls.Add(this.pbWatermarkGradient1);
            this.gbWatermarkBackground.Controls.Add(this.pbWatermarkBorderColor);
            this.gbWatermarkBackground.Controls.Add(this.pbWatermarkGradient2);
            this.gbWatermarkBackground.Controls.Add(this.lblWatermarkBackColors);
            this.gbWatermarkBackground.Location = new System.Drawing.Point(8, 224);
            this.gbWatermarkBackground.Name = "gbWatermarkBackground";
            this.gbWatermarkBackground.Size = new System.Drawing.Size(520, 144);
            this.gbWatermarkBackground.TabIndex = 25;
            this.gbWatermarkBackground.TabStop = false;
            this.gbWatermarkBackground.Text = "Background Settings";
            // 
            // cbWatermarkGradientType
            // 
            this.cbWatermarkGradientType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbWatermarkGradientType.FormattingEnabled = true;
            this.cbWatermarkGradientType.Location = new System.Drawing.Point(88, 112);
            this.cbWatermarkGradientType.Name = "cbWatermarkGradientType";
            this.cbWatermarkGradientType.Size = new System.Drawing.Size(121, 21);
            this.cbWatermarkGradientType.TabIndex = 25;
            this.cbWatermarkGradientType.SelectedIndexChanged += new System.EventHandler(this.cbWatermarkGradientType_SelectedIndexChanged);
            // 
            // lblWatermarkGradientType
            // 
            this.lblWatermarkGradientType.AutoSize = true;
            this.lblWatermarkGradientType.Location = new System.Drawing.Point(8, 120);
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
            this.lblWatermarkCornerRadiusTip.Size = new System.Drawing.Size(141, 13);
            this.lblWatermarkCornerRadiusTip.TabIndex = 23;
            this.lblWatermarkCornerRadiusTip.Text = "(0 - 10) 0 = Normal rectangle";
            // 
            // nudWatermarkCornerRadius
            // 
            this.nudWatermarkCornerRadius.Location = new System.Drawing.Point(144, 16);
            this.nudWatermarkCornerRadius.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.nudWatermarkCornerRadius.Name = "nudWatermarkCornerRadius";
            this.nudWatermarkCornerRadius.Size = new System.Drawing.Size(48, 20);
            this.nudWatermarkCornerRadius.TabIndex = 22;
            this.nudWatermarkCornerRadius.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.nudWatermarkCornerRadius.ValueChanged += new System.EventHandler(this.nudWatermarkCornerRadius_ValueChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(8, 24);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(123, 13);
            this.label5.TabIndex = 21;
            this.label5.Text = "Rectangle corner radius:";
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
            // nudWatermarkBackTrans
            // 
            this.nudWatermarkBackTrans.Location = new System.Drawing.Point(144, 80);
            this.nudWatermarkBackTrans.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.nudWatermarkBackTrans.Name = "nudWatermarkBackTrans";
            this.nudWatermarkBackTrans.Size = new System.Drawing.Size(48, 20);
            this.nudWatermarkBackTrans.TabIndex = 8;
            this.nudWatermarkBackTrans.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.nudWatermarkBackTrans.ValueChanged += new System.EventHandler(this.nudWatermarkBackTrans_ValueChanged);
            // 
            // lblWatermarkBackTransTip
            // 
            this.lblWatermarkBackTransTip.AutoSize = true;
            this.lblWatermarkBackTransTip.Location = new System.Drawing.Point(200, 88);
            this.lblWatermarkBackTransTip.Name = "lblWatermarkBackTransTip";
            this.lblWatermarkBackTransTip.Size = new System.Drawing.Size(105, 13);
            this.lblWatermarkBackTransTip.TabIndex = 9;
            this.lblWatermarkBackTransTip.Text = "(0 - 255) 0 = Invisible";
            // 
            // lblWatermarkBackTrans
            // 
            this.lblWatermarkBackTrans.AutoSize = true;
            this.lblWatermarkBackTrans.Location = new System.Drawing.Point(8, 88);
            this.lblWatermarkBackTrans.Name = "lblWatermarkBackTrans";
            this.lblWatermarkBackTrans.Size = new System.Drawing.Size(132, 13);
            this.lblWatermarkBackTrans.TabIndex = 7;
            this.lblWatermarkBackTrans.Text = "Background transparency:";
            // 
            // pbWatermarkGradient1
            // 
            this.pbWatermarkGradient1.BackColor = System.Drawing.Color.White;
            this.pbWatermarkGradient1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pbWatermarkGradient1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pbWatermarkGradient1.Location = new System.Drawing.Point(112, 48);
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
            this.pbWatermarkBorderColor.Location = new System.Drawing.Point(176, 48);
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
            this.pbWatermarkGradient2.Location = new System.Drawing.Point(144, 48);
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
            this.gbWatermarkText.Controls.Add(this.lblWatermarkTextTip);
            this.gbWatermarkText.Controls.Add(this.lblWatermarkText);
            this.gbWatermarkText.Controls.Add(this.lblWatermarkFontTransTip);
            this.gbWatermarkText.Controls.Add(this.lblWatermarkFont);
            this.gbWatermarkText.Controls.Add(this.nudWatermarkFontTrans);
            this.gbWatermarkText.Controls.Add(this.btnWatermarkFont);
            this.gbWatermarkText.Controls.Add(this.lblWatermarkFontTrans);
            this.gbWatermarkText.Controls.Add(this.txtWatermarkText);
            this.gbWatermarkText.Controls.Add(this.pbWatermarkFontColor);
            this.gbWatermarkText.Location = new System.Drawing.Point(8, 104);
            this.gbWatermarkText.Name = "gbWatermarkText";
            this.gbWatermarkText.Size = new System.Drawing.Size(520, 112);
            this.gbWatermarkText.TabIndex = 24;
            this.gbWatermarkText.TabStop = false;
            this.gbWatermarkText.Text = "Text Settings";
            // 
            // lblWatermarkTextTip
            // 
            this.lblWatermarkTextTip.AutoSize = true;
            this.lblWatermarkTextTip.Location = new System.Drawing.Point(304, 24);
            this.lblWatermarkTextTip.Name = "lblWatermarkTextTip";
            this.lblWatermarkTextTip.Size = new System.Drawing.Size(95, 13);
            this.lblWatermarkTextTip.TabIndex = 24;
            this.lblWatermarkTextTip.Text = "\\n = New line char";
            // 
            // lblWatermarkText
            // 
            this.lblWatermarkText.AutoSize = true;
            this.lblWatermarkText.Location = new System.Drawing.Point(8, 24);
            this.lblWatermarkText.Name = "lblWatermarkText";
            this.lblWatermarkText.Size = new System.Drawing.Size(82, 13);
            this.lblWatermarkText.TabIndex = 16;
            this.lblWatermarkText.Text = "Watermark text:";
            // 
            // lblWatermarkFontTransTip
            // 
            this.lblWatermarkFontTransTip.AutoSize = true;
            this.lblWatermarkFontTransTip.Location = new System.Drawing.Point(168, 88);
            this.lblWatermarkFontTransTip.Name = "lblWatermarkFontTransTip";
            this.lblWatermarkFontTransTip.Size = new System.Drawing.Size(105, 13);
            this.lblWatermarkFontTransTip.TabIndex = 23;
            this.lblWatermarkFontTransTip.Text = "(0 - 255) 0 = Invisible";
            // 
            // lblWatermarkFont
            // 
            this.lblWatermarkFont.AutoSize = true;
            this.lblWatermarkFont.Location = new System.Drawing.Point(136, 56);
            this.lblWatermarkFont.Name = "lblWatermarkFont";
            this.lblWatermarkFont.Size = new System.Drawing.Size(82, 13);
            this.lblWatermarkFont.TabIndex = 4;
            this.lblWatermarkFont.Text = "Font information";
            // 
            // nudWatermarkFontTrans
            // 
            this.nudWatermarkFontTrans.Location = new System.Drawing.Point(112, 80);
            this.nudWatermarkFontTrans.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.nudWatermarkFontTrans.Name = "nudWatermarkFontTrans";
            this.nudWatermarkFontTrans.Size = new System.Drawing.Size(48, 20);
            this.nudWatermarkFontTrans.TabIndex = 22;
            this.nudWatermarkFontTrans.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.nudWatermarkFontTrans.ValueChanged += new System.EventHandler(this.nudWatermarkFontTrans_ValueChanged);
            // 
            // btnWatermarkFont
            // 
            this.btnWatermarkFont.Location = new System.Drawing.Point(8, 48);
            this.btnWatermarkFont.Name = "btnWatermarkFont";
            this.btnWatermarkFont.Size = new System.Drawing.Size(88, 24);
            this.btnWatermarkFont.TabIndex = 3;
            this.btnWatermarkFont.Text = "Change Font";
            this.btnWatermarkFont.UseVisualStyleBackColor = true;
            this.btnWatermarkFont.Click += new System.EventHandler(this.btnWatermarkFont_Click);
            // 
            // lblWatermarkFontTrans
            // 
            this.lblWatermarkFontTrans.AutoSize = true;
            this.lblWatermarkFontTrans.Location = new System.Drawing.Point(8, 88);
            this.lblWatermarkFontTrans.Name = "lblWatermarkFontTrans";
            this.lblWatermarkFontTrans.Size = new System.Drawing.Size(95, 13);
            this.lblWatermarkFontTrans.TabIndex = 21;
            this.lblWatermarkFontTrans.Text = "Font transparency:";
            // 
            // txtWatermarkText
            // 
            this.txtWatermarkText.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtWatermarkText.Location = new System.Drawing.Point(96, 16);
            this.txtWatermarkText.Name = "txtWatermarkText";
            this.txtWatermarkText.Size = new System.Drawing.Size(200, 20);
            this.txtWatermarkText.TabIndex = 15;
            this.txtWatermarkText.TextChanged += new System.EventHandler(this.txtWatermarkText_TextChanged);
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
            // tpCaptureQuality
            // 
            this.tpCaptureQuality.Controls.Add(this.groupBox2);
            this.tpCaptureQuality.Location = new System.Drawing.Point(4, 22);
            this.tpCaptureQuality.Name = "tpCaptureQuality";
            this.tpCaptureQuality.Padding = new System.Windows.Forms.Padding(3);
            this.tpCaptureQuality.Size = new System.Drawing.Size(778, 402);
            this.tpCaptureQuality.TabIndex = 0;
            this.tpCaptureQuality.Text = "Quality";
            this.tpCaptureQuality.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.Transparent;
            this.groupBox2.Controls.Add(this.txtImageQuality);
            this.groupBox2.Controls.Add(this.label21);
            this.groupBox2.Controls.Add(this.lblQuality);
            this.groupBox2.Controls.Add(this.cmbSwitchFormat);
            this.groupBox2.Controls.Add(this.lblFileFormat);
            this.groupBox2.Controls.Add(this.cmbFileFormat);
            this.groupBox2.Controls.Add(this.lblKB);
            this.groupBox2.Controls.Add(this.txtSwitchAfter);
            this.groupBox2.Controls.Add(this.lblAfter);
            this.groupBox2.Controls.Add(this.lblSwitchTo);
            this.groupBox2.Location = new System.Drawing.Point(16, 16);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(760, 88);
            this.groupBox2.TabIndex = 115;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Picture Quality";
            // 
            // txtImageQuality
            // 
            this.txtImageQuality.Location = new System.Drawing.Point(136, 40);
            this.txtImageQuality.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.txtImageQuality.Name = "txtImageQuality";
            this.txtImageQuality.Size = new System.Drawing.Size(65, 20);
            this.txtImageQuality.TabIndex = 111;
            this.txtImageQuality.Value = new decimal(new int[] {
            90,
            0,
            0,
            0});
            this.txtImageQuality.ValueChanged += new System.EventHandler(this.txtImageQuality_ValueChanged);
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label21.Location = new System.Drawing.Point(208, 40);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(15, 13);
            this.label21.TabIndex = 110;
            this.label21.Text = "%";
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
            // cmbSwitchFormat
            // 
            this.cmbSwitchFormat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSwitchFormat.FormattingEnabled = true;
            this.cmbSwitchFormat.Location = new System.Drawing.Point(491, 40);
            this.cmbSwitchFormat.Name = "cmbSwitchFormat";
            this.cmbSwitchFormat.Size = new System.Drawing.Size(98, 21);
            this.cmbSwitchFormat.TabIndex = 9;
            this.cmbSwitchFormat.SelectedIndexChanged += new System.EventHandler(this.cmbSwitchFormat_SelectedIndexChanged);
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
            // cmbFileFormat
            // 
            this.cmbFileFormat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFileFormat.FormattingEnabled = true;
            this.cmbFileFormat.Location = new System.Drawing.Point(16, 40);
            this.cmbFileFormat.Name = "cmbFileFormat";
            this.cmbFileFormat.Size = new System.Drawing.Size(98, 21);
            this.cmbFileFormat.TabIndex = 6;
            this.cmbFileFormat.SelectedIndexChanged += new System.EventHandler(this.cmbFileFormat_SelectedIndexChanged);
            // 
            // lblKB
            // 
            this.lblKB.AutoSize = true;
            this.lblKB.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblKB.Location = new System.Drawing.Point(376, 40);
            this.lblKB.Name = "lblKB";
            this.lblKB.Size = new System.Drawing.Size(79, 13);
            this.lblKB.TabIndex = 95;
            this.lblKB.Text = "KiB (0 disables)";
            // 
            // txtSwitchAfter
            // 
            this.txtSwitchAfter.Location = new System.Drawing.Point(304, 40);
            this.txtSwitchAfter.Name = "txtSwitchAfter";
            this.txtSwitchAfter.Size = new System.Drawing.Size(69, 20);
            this.txtSwitchAfter.TabIndex = 8;
            this.txtSwitchAfter.TextChanged += new System.EventHandler(this.txtSwitchAfter_TextChanged);
            // 
            // lblAfter
            // 
            this.lblAfter.AutoSize = true;
            this.lblAfter.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblAfter.Location = new System.Drawing.Point(304, 24);
            this.lblAfter.Name = "lblAfter";
            this.lblAfter.Size = new System.Drawing.Size(32, 13);
            this.lblAfter.TabIndex = 93;
            this.lblAfter.Text = "After:";
            // 
            // lblSwitchTo
            // 
            this.lblSwitchTo.AutoSize = true;
            this.lblSwitchTo.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblSwitchTo.Location = new System.Drawing.Point(488, 24);
            this.lblSwitchTo.Name = "lblSwitchTo";
            this.lblSwitchTo.Size = new System.Drawing.Size(54, 13);
            this.lblSwitchTo.TabIndex = 92;
            this.lblSwitchTo.Text = "Switch to:";
            // 
            // cbDeleteLocal
            // 
            this.cbDeleteLocal.AutoSize = true;
            this.cbDeleteLocal.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.cbDeleteLocal.Location = new System.Drawing.Point(16, 56);
            this.cbDeleteLocal.Name = "cbDeleteLocal";
            this.cbDeleteLocal.Size = new System.Drawing.Size(239, 17);
            this.cbDeleteLocal.TabIndex = 0;
            this.cbDeleteLocal.Text = "Delete Captured Screenshot after Completion";
            this.cbDeleteLocal.UseVisualStyleBackColor = true;
            this.cbDeleteLocal.CheckedChanged += new System.EventHandler(this.cbDeleteLocal_CheckedChanged);
            // 
            // gbSaveLoc
            // 
            this.gbSaveLoc.BackColor = System.Drawing.Color.Transparent;
            this.gbSaveLoc.Controls.Add(this.cbDeleteLocal);
            this.gbSaveLoc.Controls.Add(this.btnViewLocalDirectory);
            this.gbSaveLoc.Controls.Add(this.txtFileDirectory);
            this.gbSaveLoc.Controls.Add(this.btnBrowseDirectory);
            this.gbSaveLoc.Location = new System.Drawing.Point(16, 16);
            this.gbSaveLoc.Name = "gbSaveLoc";
            this.gbSaveLoc.Size = new System.Drawing.Size(744, 88);
            this.gbSaveLoc.TabIndex = 114;
            this.gbSaveLoc.TabStop = false;
            this.gbSaveLoc.Text = "Images";
            // 
            // btnViewLocalDirectory
            // 
            this.btnViewLocalDirectory.Location = new System.Drawing.Point(496, 24);
            this.btnViewLocalDirectory.Name = "btnViewLocalDirectory";
            this.btnViewLocalDirectory.Size = new System.Drawing.Size(104, 24);
            this.btnViewLocalDirectory.TabIndex = 113;
            this.btnViewLocalDirectory.Text = "View Directory...";
            this.btnViewLocalDirectory.UseVisualStyleBackColor = true;
            this.btnViewLocalDirectory.Click += new System.EventHandler(this.btnViewLocalDirectory_Click);
            // 
            // txtFileDirectory
            // 
            this.txtFileDirectory.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.txtFileDirectory.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.FileSystemDirectories;
            this.txtFileDirectory.Location = new System.Drawing.Point(16, 24);
            this.txtFileDirectory.Name = "txtFileDirectory";
            this.txtFileDirectory.Size = new System.Drawing.Size(386, 20);
            this.txtFileDirectory.TabIndex = 1;
            this.txtFileDirectory.TextChanged += new System.EventHandler(this.txtFileDirectory_TextChanged);
            // 
            // btnBrowseDirectory
            // 
            this.btnBrowseDirectory.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnBrowseDirectory.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnBrowseDirectory.Location = new System.Drawing.Point(408, 24);
            this.btnBrowseDirectory.Name = "btnBrowseDirectory";
            this.btnBrowseDirectory.Size = new System.Drawing.Size(80, 24);
            this.btnBrowseDirectory.TabIndex = 1;
            this.btnBrowseDirectory.Text = "Browse...";
            this.btnBrowseDirectory.UseVisualStyleBackColor = true;
            this.btnBrowseDirectory.Click += new System.EventHandler(this.btnBrowseDirectory_Click);
            // 
            // cbShowWatermark
            // 
            this.cbShowWatermark.AutoSize = true;
            this.cbShowWatermark.Location = new System.Drawing.Point(24, 208);
            this.cbShowWatermark.Name = "cbShowWatermark";
            this.cbShowWatermark.Size = new System.Drawing.Size(185, 17);
            this.cbShowWatermark.TabIndex = 0;
            this.cbShowWatermark.Text = "Show Watermark on Screenshots";
            this.cbShowWatermark.UseVisualStyleBackColor = true;
            this.cbShowWatermark.CheckedChanged += new System.EventHandler(this.cbShowWatermark_CheckedChanged);
            // 
            // chkManualNaming
            // 
            this.chkManualNaming.AutoSize = true;
            this.chkManualNaming.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.chkManualNaming.Location = new System.Drawing.Point(24, 160);
            this.chkManualNaming.Name = "chkManualNaming";
            this.chkManualNaming.Size = new System.Drawing.Size(124, 17);
            this.chkManualNaming.TabIndex = 112;
            this.chkManualNaming.Text = "Prompt for File Name";
            this.chkManualNaming.UseVisualStyleBackColor = true;
            this.chkManualNaming.CheckedChanged += new System.EventHandler(this.chkManualNaming_CheckedChanged);
            // 
            // cboClipboardTextMode
            // 
            this.cboClipboardTextMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboClipboardTextMode.FormattingEnabled = true;
            this.cboClipboardTextMode.Location = new System.Drawing.Point(128, 56);
            this.cboClipboardTextMode.Name = "cboClipboardTextMode";
            this.cboClipboardTextMode.Size = new System.Drawing.Size(232, 21);
            this.cboClipboardTextMode.TabIndex = 116;
            this.cboClipboardTextMode.SelectedIndexChanged += new System.EventHandler(this.cboClipboardTextMode_SelectedIndexChanged);
            // 
            // chkEnableThumbnail
            // 
            this.chkEnableThumbnail.AutoSize = true;
            this.chkEnableThumbnail.BackColor = System.Drawing.Color.Transparent;
            this.chkEnableThumbnail.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.chkEnableThumbnail.Location = new System.Drawing.Point(438, 334);
            this.chkEnableThumbnail.Name = "chkEnableThumbnail";
            this.chkEnableThumbnail.Size = new System.Drawing.Size(109, 17);
            this.chkEnableThumbnail.TabIndex = 113;
            this.chkEnableThumbnail.Text = "Create Thumbnail";
            this.chkEnableThumbnail.UseVisualStyleBackColor = false;
            this.chkEnableThumbnail.CheckedChanged += new System.EventHandler(this.chkEnableThumbnail_CheckedChanged);
            // 
            // tpImageSoftware
            // 
            this.tpImageSoftware.Controls.Add(this.btnAddImageSoftware);
            this.tpImageSoftware.Controls.Add(this.gbImageSoftwareList);
            this.tpImageSoftware.Controls.Add(this.gbImageSoftwareActive);
            this.tpImageSoftware.ImageKey = "picture_edit.png";
            this.tpImageSoftware.Location = new System.Drawing.Point(4, 23);
            this.tpImageSoftware.Name = "tpImageSoftware";
            this.tpImageSoftware.Padding = new System.Windows.Forms.Padding(3);
            this.tpImageSoftware.Size = new System.Drawing.Size(786, 428);
            this.tpImageSoftware.TabIndex = 2;
            this.tpImageSoftware.Text = "Image Software";
            this.tpImageSoftware.UseVisualStyleBackColor = true;
            // 
            // btnAddImageSoftware
            // 
            this.btnAddImageSoftware.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnAddImageSoftware.BackColor = System.Drawing.Color.Transparent;
            this.btnAddImageSoftware.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnAddImageSoftware.Location = new System.Drawing.Point(312, 60);
            this.btnAddImageSoftware.Name = "btnAddImageSoftware";
            this.btnAddImageSoftware.Size = new System.Drawing.Size(85, 24);
            this.btnAddImageSoftware.TabIndex = 59;
            this.btnAddImageSoftware.Text = "Add -->";
            this.btnAddImageSoftware.UseVisualStyleBackColor = false;
            this.btnAddImageSoftware.Click += new System.EventHandler(this.btnAddImageSoftware_Click);
            // 
            // gbImageSoftwareList
            // 
            this.gbImageSoftwareList.BackColor = System.Drawing.Color.Transparent;
            this.gbImageSoftwareList.Controls.Add(this.lbImageSoftware);
            this.gbImageSoftwareList.Controls.Add(this.btnDeleteImageSoftware);
            this.gbImageSoftwareList.Location = new System.Drawing.Point(411, 18);
            this.gbImageSoftwareList.Name = "gbImageSoftwareList";
            this.gbImageSoftwareList.Size = new System.Drawing.Size(309, 191);
            this.gbImageSoftwareList.TabIndex = 64;
            this.gbImageSoftwareList.TabStop = false;
            this.gbImageSoftwareList.Text = "Image Software List";
            // 
            // lbImageSoftware
            // 
            this.lbImageSoftware.FormattingEnabled = true;
            this.lbImageSoftware.Location = new System.Drawing.Point(8, 24);
            this.lbImageSoftware.Name = "lbImageSoftware";
            this.lbImageSoftware.Size = new System.Drawing.Size(192, 160);
            this.lbImageSoftware.TabIndex = 59;
            this.lbImageSoftware.SelectedIndexChanged += new System.EventHandler(this.lbImageSoftware_SelectedIndexChanged);
            // 
            // btnDeleteImageSoftware
            // 
            this.btnDeleteImageSoftware.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnDeleteImageSoftware.Location = new System.Drawing.Point(214, 28);
            this.btnDeleteImageSoftware.Name = "btnDeleteImageSoftware";
            this.btnDeleteImageSoftware.Size = new System.Drawing.Size(80, 24);
            this.btnDeleteImageSoftware.TabIndex = 58;
            this.btnDeleteImageSoftware.Text = "Delete";
            this.btnDeleteImageSoftware.UseVisualStyleBackColor = true;
            this.btnDeleteImageSoftware.Click += new System.EventHandler(this.btnDeleteImageSoftware_Click);
            // 
            // gbImageSoftwareActive
            // 
            this.gbImageSoftwareActive.BackColor = System.Drawing.Color.Transparent;
            this.gbImageSoftwareActive.Controls.Add(this.lblImageSoftwarePath);
            this.gbImageSoftwareActive.Controls.Add(this.lblImageSoftwareName);
            this.gbImageSoftwareActive.Controls.Add(this.btnUpdateImageSoftware);
            this.gbImageSoftwareActive.Controls.Add(this.txtImageSoftwareName);
            this.gbImageSoftwareActive.Controls.Add(this.btnBrowseImageSoftware);
            this.gbImageSoftwareActive.Controls.Add(this.txtImageSoftwarePath);
            this.gbImageSoftwareActive.Location = new System.Drawing.Point(22, 18);
            this.gbImageSoftwareActive.Name = "gbImageSoftwareActive";
            this.gbImageSoftwareActive.Size = new System.Drawing.Size(277, 191);
            this.gbImageSoftwareActive.TabIndex = 63;
            this.gbImageSoftwareActive.TabStop = false;
            this.gbImageSoftwareActive.Text = "Add/Update Image Software";
            // 
            // lblImageSoftwarePath
            // 
            this.lblImageSoftwarePath.AutoSize = true;
            this.lblImageSoftwarePath.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblImageSoftwarePath.Location = new System.Drawing.Point(17, 75);
            this.lblImageSoftwarePath.Name = "lblImageSoftwarePath";
            this.lblImageSoftwarePath.Size = new System.Drawing.Size(32, 13);
            this.lblImageSoftwarePath.TabIndex = 62;
            this.lblImageSoftwarePath.Text = "Path:";
            // 
            // lblImageSoftwareName
            // 
            this.lblImageSoftwareName.AutoSize = true;
            this.lblImageSoftwareName.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblImageSoftwareName.Location = new System.Drawing.Point(17, 28);
            this.lblImageSoftwareName.Name = "lblImageSoftwareName";
            this.lblImageSoftwareName.Size = new System.Drawing.Size(38, 13);
            this.lblImageSoftwareName.TabIndex = 61;
            this.lblImageSoftwareName.Text = "Name:";
            // 
            // btnUpdateImageSoftware
            // 
            this.btnUpdateImageSoftware.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnUpdateImageSoftware.Location = new System.Drawing.Point(174, 122);
            this.btnUpdateImageSoftware.Name = "btnUpdateImageSoftware";
            this.btnUpdateImageSoftware.Size = new System.Drawing.Size(80, 24);
            this.btnUpdateImageSoftware.TabIndex = 7;
            this.btnUpdateImageSoftware.Text = "Update";
            this.btnUpdateImageSoftware.UseVisualStyleBackColor = true;
            this.btnUpdateImageSoftware.Click += new System.EventHandler(this.btnUpdateImageSoftware_Click);
            // 
            // txtImageSoftwareName
            // 
            this.txtImageSoftwareName.Location = new System.Drawing.Point(18, 44);
            this.txtImageSoftwareName.Name = "txtImageSoftwareName";
            this.txtImageSoftwareName.Size = new System.Drawing.Size(236, 20);
            this.txtImageSoftwareName.TabIndex = 60;
            // 
            // btnBrowseImageSoftware
            // 
            this.btnBrowseImageSoftware.AutoSize = true;
            this.btnBrowseImageSoftware.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnBrowseImageSoftware.Location = new System.Drawing.Point(18, 122);
            this.btnBrowseImageSoftware.Name = "btnBrowseImageSoftware";
            this.btnBrowseImageSoftware.Size = new System.Drawing.Size(86, 23);
            this.btnBrowseImageSoftware.TabIndex = 6;
            this.btnBrowseImageSoftware.Text = "Browse...";
            this.btnBrowseImageSoftware.UseVisualStyleBackColor = true;
            this.btnBrowseImageSoftware.Click += new System.EventHandler(this.btnBrowseImageSoftware_Click);
            // 
            // txtImageSoftwarePath
            // 
            this.txtImageSoftwarePath.Enabled = false;
            this.txtImageSoftwarePath.Location = new System.Drawing.Point(18, 91);
            this.txtImageSoftwarePath.Name = "txtImageSoftwarePath";
            this.txtImageSoftwarePath.Size = new System.Drawing.Size(236, 20);
            this.txtImageSoftwarePath.TabIndex = 5;
            // 
            // cbStartWin
            // 
            this.cbStartWin.AutoSize = true;
            this.cbStartWin.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.cbStartWin.Location = new System.Drawing.Point(16, 24);
            this.cbStartWin.Name = "cbStartWin";
            this.cbStartWin.Size = new System.Drawing.Size(117, 17);
            this.cbStartWin.TabIndex = 0;
            this.cbStartWin.Text = "Start with Windows";
            this.cbStartWin.UseVisualStyleBackColor = true;
            this.cbStartWin.CheckedChanged += new System.EventHandler(this.cbStartWin_CheckedChanged);
            // 
            // tpFTP
            // 
            this.tpFTP.Controls.Add(this.groupBox3);
            this.tpFTP.Controls.Add(this.gbFTPAccountActive);
            this.tpFTP.Controls.Add(this.chkEnableThumbnail);
            this.tpFTP.Controls.Add(this.txtErrorFTP);
            this.tpFTP.Controls.Add(this.btnTestConnection);
            this.tpFTP.Controls.Add(this.btnUpdateFTP);
            this.tpFTP.Controls.Add(this.btnAddAccount);
            this.tpFTP.ImageKey = "server_edit.png";
            this.tpFTP.Location = new System.Drawing.Point(4, 23);
            this.tpFTP.Name = "tpFTP";
            this.tpFTP.Padding = new System.Windows.Forms.Padding(3);
            this.tpFTP.Size = new System.Drawing.Size(786, 428);
            this.tpFTP.TabIndex = 3;
            this.tpFTP.Text = "FTP";
            this.tpFTP.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.BackColor = System.Drawing.Color.Transparent;
            this.groupBox3.Controls.Add(this.lbFTPAccounts);
            this.groupBox3.Controls.Add(this.btnAccsImport);
            this.groupBox3.Controls.Add(this.btnAccsExport);
            this.groupBox3.Controls.Add(this.btnDeleteFTP);
            this.groupBox3.Location = new System.Drawing.Point(438, 18);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(312, 297);
            this.groupBox3.TabIndex = 41;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "FTP Accounts List";
            // 
            // lbFTPAccounts
            // 
            this.lbFTPAccounts.FormattingEnabled = true;
            this.lbFTPAccounts.Location = new System.Drawing.Point(8, 24);
            this.lbFTPAccounts.Name = "lbFTPAccounts";
            this.lbFTPAccounts.Size = new System.Drawing.Size(200, 264);
            this.lbFTPAccounts.TabIndex = 40;
            this.lbFTPAccounts.SelectedIndexChanged += new System.EventHandler(this.lbFTPAccounts_SelectedIndexChanged);
            // 
            // btnAccsImport
            // 
            this.btnAccsImport.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnAccsImport.Location = new System.Drawing.Point(220, 215);
            this.btnAccsImport.Name = "btnAccsImport";
            this.btnAccsImport.Size = new System.Drawing.Size(77, 23);
            this.btnAccsImport.TabIndex = 39;
            this.btnAccsImport.Text = "Import...";
            this.btnAccsImport.UseVisualStyleBackColor = true;
            this.btnAccsImport.Click += new System.EventHandler(this.btnAccsImport_Click);
            // 
            // btnAccsExport
            // 
            this.btnAccsExport.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnAccsExport.Location = new System.Drawing.Point(220, 249);
            this.btnAccsExport.Name = "btnAccsExport";
            this.btnAccsExport.Size = new System.Drawing.Size(77, 23);
            this.btnAccsExport.TabIndex = 38;
            this.btnAccsExport.Text = "Export...";
            this.btnAccsExport.UseVisualStyleBackColor = true;
            this.btnAccsExport.Click += new System.EventHandler(this.btnExportAccounts_Click);
            // 
            // btnDeleteFTP
            // 
            this.btnDeleteFTP.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnDeleteFTP.Location = new System.Drawing.Point(220, 25);
            this.btnDeleteFTP.Name = "btnDeleteFTP";
            this.btnDeleteFTP.Size = new System.Drawing.Size(77, 24);
            this.btnDeleteFTP.TabIndex = 12;
            this.btnDeleteFTP.Text = "Delete";
            this.btnDeleteFTP.UseVisualStyleBackColor = true;
            this.btnDeleteFTP.Click += new System.EventHandler(this.btnDeleteFTP_Click);
            // 
            // gbFTPAccountActive
            // 
            this.gbFTPAccountActive.BackColor = System.Drawing.Color.Transparent;
            this.gbFTPAccountActive.Controls.Add(this.txtServerPort);
            this.gbFTPAccountActive.Controls.Add(this.label1);
            this.gbFTPAccountActive.Controls.Add(this.txtName);
            this.gbFTPAccountActive.Controls.Add(this.gbFTPMode);
            this.gbFTPAccountActive.Controls.Add(this.lblPort);
            this.gbFTPAccountActive.Controls.Add(this.txtServer);
            this.gbFTPAccountActive.Controls.Add(this.lblServer);
            this.gbFTPAccountActive.Controls.Add(this.txtUsername);
            this.gbFTPAccountActive.Controls.Add(this.lblUsername);
            this.gbFTPAccountActive.Controls.Add(this.txtPassword);
            this.gbFTPAccountActive.Controls.Add(this.lblHttpPath);
            this.gbFTPAccountActive.Controls.Add(this.lblPassword);
            this.gbFTPAccountActive.Controls.Add(this.txtHttpPath);
            this.gbFTPAccountActive.Controls.Add(this.txtPath);
            this.gbFTPAccountActive.Controls.Add(this.lblFtpPath);
            this.gbFTPAccountActive.Location = new System.Drawing.Point(15, 18);
            this.gbFTPAccountActive.Name = "gbFTPAccountActive";
            this.gbFTPAccountActive.Size = new System.Drawing.Size(329, 297);
            this.gbFTPAccountActive.TabIndex = 40;
            this.gbFTPAccountActive.TabStop = false;
            this.gbFTPAccountActive.Text = "Add/Update FTP Account";
            // 
            // txtServerPort
            // 
            this.txtServerPort.Location = new System.Drawing.Point(259, 115);
            this.txtServerPort.Maximum = new decimal(new int[] {
            65536,
            0,
            0,
            0});
            this.txtServerPort.Minimum = new decimal(new int[] {
            21,
            0,
            0,
            0});
            this.txtServerPort.Name = "txtServerPort";
            this.txtServerPort.Size = new System.Drawing.Size(58, 20);
            this.txtServerPort.TabIndex = 37;
            this.txtServerPort.Value = new decimal(new int[] {
            21,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label1.Location = new System.Drawing.Point(26, 57);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 36;
            this.label1.Text = "Name:";
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(29, 74);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(224, 20);
            this.txtName.TabIndex = 0;
            this.txtName.TextChanged += new System.EventHandler(this.txtName_TextChanged);
            // 
            // gbFTPMode
            // 
            this.gbFTPMode.Controls.Add(this.rbFTPActive);
            this.gbFTPMode.Controls.Add(this.rbFTPPassive);
            this.gbFTPMode.Location = new System.Drawing.Point(26, 18);
            this.gbFTPMode.Name = "gbFTPMode";
            this.gbFTPMode.Size = new System.Drawing.Size(227, 36);
            this.gbFTPMode.TabIndex = 33;
            this.gbFTPMode.TabStop = false;
            // 
            // rbFTPActive
            // 
            this.rbFTPActive.AutoSize = true;
            this.rbFTPActive.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.rbFTPActive.Location = new System.Drawing.Point(113, 13);
            this.rbFTPActive.Name = "rbFTPActive";
            this.rbFTPActive.Size = new System.Drawing.Size(85, 17);
            this.rbFTPActive.TabIndex = 1;
            this.rbFTPActive.Text = "Active Mode";
            this.rbFTPActive.UseVisualStyleBackColor = true;
            // 
            // rbFTPPassive
            // 
            this.rbFTPPassive.AutoSize = true;
            this.rbFTPPassive.Checked = true;
            this.rbFTPPassive.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.rbFTPPassive.Location = new System.Drawing.Point(7, 13);
            this.rbFTPPassive.Name = "rbFTPPassive";
            this.rbFTPPassive.Size = new System.Drawing.Size(92, 17);
            this.rbFTPPassive.TabIndex = 0;
            this.rbFTPPassive.TabStop = true;
            this.rbFTPPassive.Text = "Passive Mode";
            this.rbFTPPassive.UseVisualStyleBackColor = true;
            // 
            // lblPort
            // 
            this.lblPort.AutoSize = true;
            this.lblPort.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblPort.Location = new System.Drawing.Point(256, 98);
            this.lblPort.Name = "lblPort";
            this.lblPort.Size = new System.Drawing.Size(29, 13);
            this.lblPort.TabIndex = 29;
            this.lblPort.Text = "Port:";
            // 
            // txtServer
            // 
            this.txtServer.Location = new System.Drawing.Point(29, 114);
            this.txtServer.Name = "txtServer";
            this.txtServer.Size = new System.Drawing.Size(224, 20);
            this.txtServer.TabIndex = 1;
            // 
            // lblServer
            // 
            this.lblServer.AutoSize = true;
            this.lblServer.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblServer.Location = new System.Drawing.Point(26, 98);
            this.lblServer.Name = "lblServer";
            this.lblServer.Size = new System.Drawing.Size(64, 13);
            this.lblServer.TabIndex = 25;
            this.lblServer.Text = "FTP Server:";
            // 
            // txtUsername
            // 
            this.txtUsername.Location = new System.Drawing.Point(29, 153);
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Size = new System.Drawing.Size(224, 20);
            this.txtUsername.TabIndex = 3;
            // 
            // lblUsername
            // 
            this.lblUsername.AutoSize = true;
            this.lblUsername.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblUsername.Location = new System.Drawing.Point(26, 137);
            this.lblUsername.Name = "lblUsername";
            this.lblUsername.Size = new System.Drawing.Size(81, 13);
            this.lblUsername.TabIndex = 24;
            this.lblUsername.Text = "FTP Username:";
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(29, 192);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(224, 20);
            this.txtPassword.TabIndex = 4;
            this.txtPassword.UseSystemPasswordChar = true;
            // 
            // lblHttpPath
            // 
            this.lblHttpPath.AutoSize = true;
            this.lblHttpPath.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblHttpPath.Location = new System.Drawing.Point(26, 252);
            this.lblHttpPath.Name = "lblHttpPath";
            this.lblHttpPath.Size = new System.Drawing.Size(214, 13);
            this.lblHttpPath.TabIndex = 20;
            this.lblHttpPath.Text = "HTTP Path: (ex: brandonz.net/screenshots)";
            // 
            // lblPassword
            // 
            this.lblPassword.AutoSize = true;
            this.lblPassword.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblPassword.Location = new System.Drawing.Point(26, 176);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(79, 13);
            this.lblPassword.TabIndex = 23;
            this.lblPassword.Text = "FTP Password:";
            // 
            // txtHttpPath
            // 
            this.txtHttpPath.Location = new System.Drawing.Point(29, 268);
            this.txtHttpPath.Name = "txtHttpPath";
            this.txtHttpPath.Size = new System.Drawing.Size(224, 20);
            this.txtHttpPath.TabIndex = 6;
            // 
            // txtPath
            // 
            this.txtPath.Location = new System.Drawing.Point(29, 231);
            this.txtPath.Name = "txtPath";
            this.txtPath.Size = new System.Drawing.Size(224, 20);
            this.txtPath.TabIndex = 5;
            // 
            // lblFtpPath
            // 
            this.lblFtpPath.AutoSize = true;
            this.lblFtpPath.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblFtpPath.Location = new System.Drawing.Point(26, 215);
            this.lblFtpPath.Name = "lblFtpPath";
            this.lblFtpPath.Size = new System.Drawing.Size(200, 13);
            this.lblFtpPath.TabIndex = 22;
            this.lblFtpPath.Text = "FTP Path: (ex: / or /htdocs/screenshots)";
            // 
            // txtErrorFTP
            // 
            this.txtErrorFTP.CausesValidation = false;
            this.txtErrorFTP.Location = new System.Drawing.Point(42, 356);
            this.txtErrorFTP.Multiline = true;
            this.txtErrorFTP.Name = "txtErrorFTP";
            this.txtErrorFTP.ReadOnly = true;
            this.txtErrorFTP.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtErrorFTP.Size = new System.Drawing.Size(270, 47);
            this.txtErrorFTP.TabIndex = 8;
            // 
            // btnTestConnection
            // 
            this.btnTestConnection.BackColor = System.Drawing.Color.Transparent;
            this.btnTestConnection.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnTestConnection.Location = new System.Drawing.Point(43, 327);
            this.btnTestConnection.Name = "btnTestConnection";
            this.btnTestConnection.Size = new System.Drawing.Size(135, 23);
            this.btnTestConnection.TabIndex = 7;
            this.btnTestConnection.Text = "Test Connection";
            this.btnTestConnection.UseVisualStyleBackColor = false;
            this.btnTestConnection.Click += new System.EventHandler(this.btnTestConnection_Click);
            // 
            // btnUpdateFTP
            // 
            this.btnUpdateFTP.BackColor = System.Drawing.Color.Transparent;
            this.btnUpdateFTP.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnUpdateFTP.Location = new System.Drawing.Point(184, 327);
            this.btnUpdateFTP.Name = "btnUpdateFTP";
            this.btnUpdateFTP.Size = new System.Drawing.Size(128, 24);
            this.btnUpdateFTP.TabIndex = 9;
            this.btnUpdateFTP.Text = "Update";
            this.btnUpdateFTP.UseVisualStyleBackColor = false;
            this.btnUpdateFTP.Click += new System.EventHandler(this.btnUpdateFTP_Click);
            // 
            // btnAddAccount
            // 
            this.btnAddAccount.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnAddAccount.BackColor = System.Drawing.Color.Transparent;
            this.btnAddAccount.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnAddAccount.Location = new System.Drawing.Point(352, 130);
            this.btnAddAccount.Name = "btnAddAccount";
            this.btnAddAccount.Size = new System.Drawing.Size(79, 23);
            this.btnAddAccount.TabIndex = 14;
            this.btnAddAccount.Text = "Add -->";
            this.btnAddAccount.UseVisualStyleBackColor = false;
            this.btnAddAccount.Click += new System.EventHandler(this.btnAddAccount_Click);
            // 
            // tpHotKeys
            // 
            this.tpHotKeys.Controls.Add(this.lblHotkeyStatus);
            this.tpHotKeys.Controls.Add(this.dgvHotkeys);
            this.tpHotKeys.ImageKey = "keyboard.png";
            this.tpHotKeys.Location = new System.Drawing.Point(4, 23);
            this.tpHotKeys.Name = "tpHotKeys";
            this.tpHotKeys.Padding = new System.Windows.Forms.Padding(3);
            this.tpHotKeys.Size = new System.Drawing.Size(786, 428);
            this.tpHotKeys.TabIndex = 1;
            this.tpHotKeys.Text = "Hotkeys";
            this.tpHotKeys.UseVisualStyleBackColor = true;
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
            this.dgvHotkeys.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgvHotkeys.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvHotkeys.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.Disable;
            dataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle13.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle13.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            dataGridViewCellStyle13.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle13.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle13.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle13.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvHotkeys.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle13;
            this.dgvHotkeys.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvHotkeys.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.chHotkeys_Description,
            this.chHotkeys_Keys});
            dataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle14.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle14.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            dataGridViewCellStyle14.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle14.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle14.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle14.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvHotkeys.DefaultCellStyle = dataGridViewCellStyle14;
            this.dgvHotkeys.Location = new System.Drawing.Point(26, 50);
            this.dgvHotkeys.MultiSelect = false;
            this.dgvHotkeys.Name = "dgvHotkeys";
            this.dgvHotkeys.ReadOnly = true;
            dataGridViewCellStyle15.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle15.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle15.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            dataGridViewCellStyle15.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle15.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle15.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle15.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvHotkeys.RowHeadersDefaultCellStyle = dataGridViewCellStyle15;
            this.dgvHotkeys.RowHeadersVisible = false;
            this.dgvHotkeys.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgvHotkeys.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dgvHotkeys.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgvHotkeys.Size = new System.Drawing.Size(503, 198);
            this.dgvHotkeys.TabIndex = 67;
            this.dgvHotkeys.Leave += new System.EventHandler(this.dgvHotkeys_Leave);
            this.dgvHotkeys.CellMouseEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvHotkeys_CellMouseEnter);
            this.dgvHotkeys.MouseLeave += new System.EventHandler(this.dgvHotkeys_MouseLeave);
            this.dgvHotkeys.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvHotkeys_CellClick);
            // 
            // chHotkeys_Description
            // 
            this.chHotkeys_Description.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.chHotkeys_Description.HeaderText = "Description";
            this.chHotkeys_Description.Name = "chHotkeys_Description";
            this.chHotkeys_Description.ReadOnly = true;
            this.chHotkeys_Description.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.chHotkeys_Description.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.chHotkeys_Description.Width = 250;
            // 
            // chHotkeys_Keys
            // 
            this.chHotkeys_Keys.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.chHotkeys_Keys.HeaderText = "Keys";
            this.chHotkeys_Keys.Name = "chHotkeys_Keys";
            this.chHotkeys_Keys.ReadOnly = true;
            this.chHotkeys_Keys.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.chHotkeys_Keys.Width = 250;
            // 
            // tpMain
            // 
            this.tpMain.AllowDrop = true;
            this.tpMain.Controls.Add(this.gbActiveHelp);
            this.tpMain.Controls.Add(this.llProjectPage);
            this.tpMain.Controls.Add(this.llWebsite);
            this.tpMain.Controls.Add(this.llblBugReports);
            this.tpMain.Controls.Add(this.lblFirstRun);
            this.tpMain.Controls.Add(this.gbMainOptions);
            this.tpMain.Controls.Add(this.lblLogo);
            this.tpMain.Controls.Add(this.pbLogo);
            this.tpMain.ImageKey = "application_form.png";
            this.tpMain.Location = new System.Drawing.Point(4, 23);
            this.tpMain.Name = "tpMain";
            this.tpMain.Padding = new System.Windows.Forms.Padding(3);
            this.tpMain.Size = new System.Drawing.Size(786, 428);
            this.tpMain.TabIndex = 0;
            this.tpMain.Text = "Main";
            this.tpMain.UseVisualStyleBackColor = true;
            this.tpMain.DragDrop += new System.Windows.Forms.DragEventHandler(this.tpMain_DragDrop);
            this.tpMain.DragEnter += new System.Windows.Forms.DragEventHandler(this.tpMain_DragEnter);
            // 
            // gbActiveHelp
            // 
            this.gbActiveHelp.Controls.Add(this.cbHelpToLanguage);
            this.gbActiveHelp.Controls.Add(this.chkGTActiveHelp);
            this.gbActiveHelp.Controls.Add(this.cbActiveHelp);
            this.gbActiveHelp.Location = new System.Drawing.Point(16, 304);
            this.gbActiveHelp.Name = "gbActiveHelp";
            this.gbActiveHelp.Size = new System.Drawing.Size(376, 104);
            this.gbActiveHelp.TabIndex = 84;
            this.gbActiveHelp.TabStop = false;
            this.gbActiveHelp.Text = "Active Help";
            // 
            // cbHelpToLanguage
            // 
            this.cbHelpToLanguage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbHelpToLanguage.Enabled = false;
            this.cbHelpToLanguage.FormattingEnabled = true;
            this.cbHelpToLanguage.Location = new System.Drawing.Point(184, 40);
            this.cbHelpToLanguage.MaxDropDownItems = 20;
            this.cbHelpToLanguage.Name = "cbHelpToLanguage";
            this.cbHelpToLanguage.Size = new System.Drawing.Size(128, 21);
            this.cbHelpToLanguage.TabIndex = 9;
            this.cbHelpToLanguage.SelectedIndexChanged += new System.EventHandler(this.cbHelpToLanguage_SelectedIndexChanged);
            // 
            // chkGTActiveHelp
            // 
            this.chkGTActiveHelp.AutoSize = true;
            this.chkGTActiveHelp.Location = new System.Drawing.Point(24, 48);
            this.chkGTActiveHelp.Name = "chkGTActiveHelp";
            this.chkGTActiveHelp.Size = new System.Drawing.Size(156, 17);
            this.chkGTActiveHelp.TabIndex = 8;
            this.chkGTActiveHelp.Text = "Translate Active Help using";
            this.chkGTActiveHelp.UseVisualStyleBackColor = true;
            this.chkGTActiveHelp.CheckedChanged += new System.EventHandler(this.chkGTActiveHelp_CheckedChanged);
            // 
            // cbActiveHelp
            // 
            this.cbActiveHelp.AutoSize = true;
            this.cbActiveHelp.Checked = true;
            this.cbActiveHelp.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbActiveHelp.Location = new System.Drawing.Point(24, 24);
            this.cbActiveHelp.Name = "cbActiveHelp";
            this.cbActiveHelp.Size = new System.Drawing.Size(111, 17);
            this.cbActiveHelp.TabIndex = 7;
            this.cbActiveHelp.Text = "Show Active Help";
            this.cbActiveHelp.UseVisualStyleBackColor = true;
            this.cbActiveHelp.CheckedChanged += new System.EventHandler(this.cbActiveHelp_CheckedChanged);
            // 
            // llProjectPage
            // 
            this.llProjectPage.AutoSize = true;
            this.llProjectPage.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline;
            this.llProjectPage.Location = new System.Drawing.Point(544, 360);
            this.llProjectPage.Name = "llProjectPage";
            this.llProjectPage.Size = new System.Drawing.Size(68, 13);
            this.llProjectPage.TabIndex = 83;
            this.llProjectPage.TabStop = true;
            this.llProjectPage.Text = "Project Page";
            this.llProjectPage.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llProjectPage_LinkClicked);
            // 
            // llWebsite
            // 
            this.llWebsite.AutoSize = true;
            this.llWebsite.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline;
            this.llWebsite.Location = new System.Drawing.Point(424, 360);
            this.llWebsite.Name = "llWebsite";
            this.llWebsite.Size = new System.Drawing.Size(72, 13);
            this.llWebsite.TabIndex = 82;
            this.llWebsite.TabStop = true;
            this.llWebsite.Text = "BrandonZ.net";
            this.llWebsite.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llWebsite_LinkClicked);
            // 
            // llblBugReports
            // 
            this.llblBugReports.AutoSize = true;
            this.llblBugReports.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline;
            this.llblBugReports.Location = new System.Drawing.Point(648, 360);
            this.llblBugReports.Name = "llblBugReports";
            this.llblBugReports.Size = new System.Drawing.Size(100, 13);
            this.llblBugReports.TabIndex = 81;
            this.llblBugReports.TabStop = true;
            this.llblBugReports.Text = "Bugs/Suggestions?";
            this.llblBugReports.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llblBugReports_LinkClicked);
            // 
            // lblFirstRun
            // 
            this.lblFirstRun.BackColor = System.Drawing.Color.White;
            this.lblFirstRun.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblFirstRun.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblFirstRun.Location = new System.Drawing.Point(424, 16);
            this.lblFirstRun.Name = "lblFirstRun";
            this.lblFirstRun.Size = new System.Drawing.Size(326, 27);
            this.lblFirstRun.TabIndex = 80;
            this.lblFirstRun.Text = "First Run: Please see all Settings sections";
            this.lblFirstRun.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblFirstRun.Visible = false;
            // 
            // gbMainOptions
            // 
            this.gbMainOptions.BackColor = System.Drawing.Color.Transparent;
            this.gbMainOptions.Controls.Add(this.cbShowWatermark);
            this.gbMainOptions.Controls.Add(this.label4);
            this.gbMainOptions.Controls.Add(this.cboClipboardTextMode);
            this.gbMainOptions.Controls.Add(this.chkManualNaming);
            this.gbMainOptions.Controls.Add(this.lblScreenshotDelay);
            this.gbMainOptions.Controls.Add(this.cbShowCursor);
            this.gbMainOptions.Controls.Add(this.nScreenshotDelay);
            this.gbMainOptions.Controls.Add(this.lblScreenshotDestination);
            this.gbMainOptions.Controls.Add(this.lblDelaySeconds);
            this.gbMainOptions.Controls.Add(this.cbCompleteSound);
            this.gbMainOptions.Controls.Add(this.cboScreenshotDest);
            this.gbMainOptions.Location = new System.Drawing.Point(16, 16);
            this.gbMainOptions.Name = "gbMainOptions";
            this.gbMainOptions.Size = new System.Drawing.Size(376, 280);
            this.gbMainOptions.TabIndex = 79;
            this.gbMainOptions.TabStop = false;
            this.gbMainOptions.Text = "General Settings";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(24, 56);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(93, 13);
            this.label4.TabIndex = 117;
            this.label4.Text = "Copy to Clipboard:";
            // 
            // lblScreenshotDelay
            // 
            this.lblScreenshotDelay.AutoSize = true;
            this.lblScreenshotDelay.Location = new System.Drawing.Point(24, 96);
            this.lblScreenshotDelay.Name = "lblScreenshotDelay";
            this.lblScreenshotDelay.Size = new System.Drawing.Size(269, 13);
            this.lblScreenshotDelay.TabIndex = 2;
            this.lblScreenshotDelay.Text = "Delay for Entire Screen and Active Window Screenshot";
            // 
            // cbShowCursor
            // 
            this.cbShowCursor.AutoSize = true;
            this.cbShowCursor.Location = new System.Drawing.Point(24, 184);
            this.cbShowCursor.Name = "cbShowCursor";
            this.cbShowCursor.Size = new System.Drawing.Size(159, 17);
            this.cbShowCursor.TabIndex = 8;
            this.cbShowCursor.Text = "Show Cursor in Screenshots";
            this.cbShowCursor.UseVisualStyleBackColor = true;
            this.cbShowCursor.CheckedChanged += new System.EventHandler(this.cbShowCursor_CheckedChanged);
            // 
            // nScreenshotDelay
            // 
            this.nScreenshotDelay.DecimalPlaces = 1;
            this.nScreenshotDelay.Increment = new decimal(new int[] {
            5,
            0,
            0,
            65536});
            this.nScreenshotDelay.Location = new System.Drawing.Point(24, 120);
            this.nScreenshotDelay.Name = "nScreenshotDelay";
            this.nScreenshotDelay.Size = new System.Drawing.Size(80, 20);
            this.nScreenshotDelay.TabIndex = 3;
            this.nScreenshotDelay.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.nScreenshotDelay.ValueChanged += new System.EventHandler(this.nScreenshotDelay_ValueChanged);
            // 
            // lblScreenshotDestination
            // 
            this.lblScreenshotDestination.AutoSize = true;
            this.lblScreenshotDestination.Location = new System.Drawing.Point(54, 24);
            this.lblScreenshotDestination.Name = "lblScreenshotDestination";
            this.lblScreenshotDestination.Size = new System.Drawing.Size(63, 13);
            this.lblScreenshotDestination.TabIndex = 1;
            this.lblScreenshotDestination.Text = "Destination:";
            // 
            // lblDelaySeconds
            // 
            this.lblDelaySeconds.AutoSize = true;
            this.lblDelaySeconds.Location = new System.Drawing.Point(112, 124);
            this.lblDelaySeconds.Name = "lblDelaySeconds";
            this.lblDelaySeconds.Size = new System.Drawing.Size(47, 13);
            this.lblDelaySeconds.TabIndex = 4;
            this.lblDelaySeconds.Text = "seconds";
            // 
            // cbCompleteSound
            // 
            this.cbCompleteSound.AutoSize = true;
            this.cbCompleteSound.Location = new System.Drawing.Point(24, 232);
            this.cbCompleteSound.Name = "cbCompleteSound";
            this.cbCompleteSound.Size = new System.Drawing.Size(230, 17);
            this.cbCompleteSound.TabIndex = 5;
            this.cbCompleteSound.Text = "Play Sound after image reaches destination";
            this.cbCompleteSound.UseVisualStyleBackColor = true;
            this.cbCompleteSound.CheckedChanged += new System.EventHandler(this.cbCompleteSound_CheckedChanged);
            // 
            // cboScreenshotDest
            // 
            this.cboScreenshotDest.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboScreenshotDest.FormattingEnabled = true;
            this.cboScreenshotDest.Location = new System.Drawing.Point(128, 24);
            this.cboScreenshotDest.Name = "cboScreenshotDest";
            this.cboScreenshotDest.Size = new System.Drawing.Size(232, 21);
            this.cboScreenshotDest.TabIndex = 0;
            this.cboScreenshotDest.SelectedIndexChanged += new System.EventHandler(this.cboScreenshotDest_SelectedIndexChanged);
            // 
            // lblLogo
            // 
            this.lblLogo.BackColor = System.Drawing.Color.White;
            this.lblLogo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblLogo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblLogo.ForeColor = System.Drawing.Color.Black;
            this.lblLogo.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblLogo.Location = new System.Drawing.Point(424, 312);
            this.lblLogo.Name = "lblLogo";
            this.lblLogo.Size = new System.Drawing.Size(326, 40);
            this.lblLogo.TabIndex = 74;
            this.lblLogo.Text = "ZScreen vW.X.Y.Z";
            this.lblLogo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pbLogo
            // 
            this.pbLogo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbLogo.Cursor = System.Windows.Forms.Cursors.Default;
            this.pbLogo.Image = ((System.Drawing.Image)(resources.GetObject("pbLogo.Image")));
            this.pbLogo.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.pbLogo.Location = new System.Drawing.Point(424, 48);
            this.pbLogo.Name = "pbLogo";
            this.pbLogo.Size = new System.Drawing.Size(326, 256);
            this.pbLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pbLogo.TabIndex = 72;
            this.pbLogo.TabStop = false;
            // 
            // tcApp
            // 
            this.tcApp.AllowDrop = true;
            this.tcApp.Controls.Add(this.tpMain);
            this.tcApp.Controls.Add(this.tpHotKeys);
            this.tcApp.Controls.Add(this.tpFile);
            this.tcApp.Controls.Add(this.tpImageSoftware);
            this.tcApp.Controls.Add(this.tpFTP);
            this.tcApp.Controls.Add(this.tpHTTP);
            this.tcApp.Controls.Add(this.tpAdvanced);
            this.tcApp.Controls.Add(this.tpScreenshots);
            this.tcApp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcApp.ImageList = this.ilApp;
            this.tcApp.Location = new System.Drawing.Point(0, 0);
            this.tcApp.Name = "tcApp";
            this.tcApp.SelectedIndex = 0;
            this.tcApp.Size = new System.Drawing.Size(794, 455);
            this.tcApp.TabIndex = 74;
            this.tcApp.Selected += new System.Windows.Forms.TabControlEventHandler(this.tabControl_Selected);
            // 
            // tpHTTP
            // 
            this.tpHTTP.Controls.Add(this.tcHTTP);
            this.tpHTTP.ImageKey = "world_edit.png";
            this.tpHTTP.Location = new System.Drawing.Point(4, 23);
            this.tpHTTP.Name = "tpHTTP";
            this.tpHTTP.Padding = new System.Windows.Forms.Padding(3);
            this.tpHTTP.Size = new System.Drawing.Size(786, 428);
            this.tpHTTP.TabIndex = 10;
            this.tpHTTP.Text = "HTTP";
            this.tpHTTP.UseVisualStyleBackColor = true;
            // 
            // tcHTTP
            // 
            this.tcHTTP.Controls.Add(this.tpImageUploaders);
            this.tcHTTP.Controls.Add(this.tpCustomUploaders);
            this.tcHTTP.Controls.Add(this.tpLanguageTranslator);
            this.tcHTTP.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcHTTP.Location = new System.Drawing.Point(3, 3);
            this.tcHTTP.Name = "tcHTTP";
            this.tcHTTP.SelectedIndex = 0;
            this.tcHTTP.Size = new System.Drawing.Size(780, 422);
            this.tcHTTP.TabIndex = 5;
            // 
            // tpImageUploaders
            // 
            this.tpImageUploaders.Controls.Add(this.cboUploadMode);
            this.tpImageUploaders.Controls.Add(this.label6);
            this.tpImageUploaders.Controls.Add(this.gbImageShack);
            this.tpImageUploaders.Controls.Add(this.gbTinyPic);
            this.tpImageUploaders.Controls.Add(this.lblErrorRetry);
            this.tpImageUploaders.Controls.Add(this.nErrorRetry);
            this.tpImageUploaders.Location = new System.Drawing.Point(4, 22);
            this.tpImageUploaders.Name = "tpImageUploaders";
            this.tpImageUploaders.Padding = new System.Windows.Forms.Padding(3);
            this.tpImageUploaders.Size = new System.Drawing.Size(772, 396);
            this.tpImageUploaders.TabIndex = 0;
            this.tpImageUploaders.Text = "Image Uploaders";
            this.tpImageUploaders.UseVisualStyleBackColor = true;
            // 
            // cboUploadMode
            // 
            this.cboUploadMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboUploadMode.FormattingEnabled = true;
            this.cboUploadMode.Location = new System.Drawing.Point(88, 16);
            this.cboUploadMode.Name = "cboUploadMode";
            this.cboUploadMode.Size = new System.Drawing.Size(121, 21);
            this.cboUploadMode.TabIndex = 5;
            this.cboUploadMode.SelectedIndexChanged += new System.EventHandler(this.cboUploadMode_SelectedIndexChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(24, 24);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(58, 13);
            this.label6.TabIndex = 4;
            this.label6.Text = "Upload as:";
            // 
            // gbImageShack
            // 
            this.gbImageShack.BackColor = System.Drawing.Color.Transparent;
            this.gbImageShack.Controls.Add(this.btnGalleryImageShack);
            this.gbImageShack.Controls.Add(this.btnRegCodeImageShack);
            this.gbImageShack.Controls.Add(this.lblImageShackRegistrationCode);
            this.gbImageShack.Controls.Add(this.txtImageShackRegistrationCode);
            this.gbImageShack.Location = new System.Drawing.Point(16, 56);
            this.gbImageShack.Name = "gbImageShack";
            this.gbImageShack.Size = new System.Drawing.Size(465, 97);
            this.gbImageShack.TabIndex = 0;
            this.gbImageShack.TabStop = false;
            this.gbImageShack.Text = "ImageShack";
            // 
            // btnGalleryImageShack
            // 
            this.btnGalleryImageShack.Location = new System.Drawing.Point(363, 44);
            this.btnGalleryImageShack.Name = "btnGalleryImageShack";
            this.btnGalleryImageShack.Size = new System.Drawing.Size(75, 23);
            this.btnGalleryImageShack.TabIndex = 3;
            this.btnGalleryImageShack.Text = "&MyImages...";
            this.btnGalleryImageShack.UseVisualStyleBackColor = true;
            this.btnGalleryImageShack.Click += new System.EventHandler(this.btnGalleryImageShack_Click);
            // 
            // btnRegCodeImageShack
            // 
            this.btnRegCodeImageShack.Location = new System.Drawing.Point(282, 44);
            this.btnRegCodeImageShack.Name = "btnRegCodeImageShack";
            this.btnRegCodeImageShack.Size = new System.Drawing.Size(75, 23);
            this.btnRegCodeImageShack.TabIndex = 2;
            this.btnRegCodeImageShack.Text = "&RegCode...";
            this.btnRegCodeImageShack.UseVisualStyleBackColor = true;
            this.btnRegCodeImageShack.Click += new System.EventHandler(this.btnRegCodeImageShack_Click);
            // 
            // lblImageShackRegistrationCode
            // 
            this.lblImageShackRegistrationCode.AutoSize = true;
            this.lblImageShackRegistrationCode.Location = new System.Drawing.Point(7, 26);
            this.lblImageShackRegistrationCode.Name = "lblImageShackRegistrationCode";
            this.lblImageShackRegistrationCode.Size = new System.Drawing.Size(94, 13);
            this.lblImageShackRegistrationCode.TabIndex = 1;
            this.lblImageShackRegistrationCode.Text = "Registration Code:";
            // 
            // txtImageShackRegistrationCode
            // 
            this.txtImageShackRegistrationCode.Location = new System.Drawing.Point(9, 46);
            this.txtImageShackRegistrationCode.Name = "txtImageShackRegistrationCode";
            this.txtImageShackRegistrationCode.Size = new System.Drawing.Size(267, 20);
            this.txtImageShackRegistrationCode.TabIndex = 0;
            this.txtImageShackRegistrationCode.TextChanged += new System.EventHandler(this.txtImageShackRegistrationCode_TextChanged);
            // 
            // gbTinyPic
            // 
            this.gbTinyPic.Controls.Add(this.btnRegCodeTinyPic);
            this.gbTinyPic.Controls.Add(this.label2);
            this.gbTinyPic.Controls.Add(this.txtTinyPicShuk);
            this.gbTinyPic.Location = new System.Drawing.Point(16, 160);
            this.gbTinyPic.Name = "gbTinyPic";
            this.gbTinyPic.Size = new System.Drawing.Size(465, 91);
            this.gbTinyPic.TabIndex = 4;
            this.gbTinyPic.TabStop = false;
            this.gbTinyPic.Text = "TinyPic";
            // 
            // btnRegCodeTinyPic
            // 
            this.btnRegCodeTinyPic.Location = new System.Drawing.Point(282, 44);
            this.btnRegCodeTinyPic.Name = "btnRegCodeTinyPic";
            this.btnRegCodeTinyPic.Size = new System.Drawing.Size(75, 23);
            this.btnRegCodeTinyPic.TabIndex = 5;
            this.btnRegCodeTinyPic.Text = "&RegCode...";
            this.btnRegCodeTinyPic.UseVisualStyleBackColor = true;
            this.btnRegCodeTinyPic.Click += new System.EventHandler(this.btnRegCodeTinyPic_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 23);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(94, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Registration Code:";
            // 
            // txtTinyPicShuk
            // 
            this.txtTinyPicShuk.Location = new System.Drawing.Point(9, 46);
            this.txtTinyPicShuk.Name = "txtTinyPicShuk";
            this.txtTinyPicShuk.ReadOnly = true;
            this.txtTinyPicShuk.Size = new System.Drawing.Size(267, 20);
            this.txtTinyPicShuk.TabIndex = 3;
            this.txtTinyPicShuk.TextChanged += new System.EventHandler(this.txtTinyPicShuk_TextChanged);
            // 
            // lblErrorRetry
            // 
            this.lblErrorRetry.AutoSize = true;
            this.lblErrorRetry.Location = new System.Drawing.Point(264, 24);
            this.lblErrorRetry.Name = "lblErrorRetry";
            this.lblErrorRetry.Size = new System.Drawing.Size(95, 13);
            this.lblErrorRetry.TabIndex = 1;
            this.lblErrorRetry.Text = "Number of Retries:";
            // 
            // nErrorRetry
            // 
            this.nErrorRetry.Location = new System.Drawing.Point(368, 16);
            this.nErrorRetry.Name = "nErrorRetry";
            this.nErrorRetry.Size = new System.Drawing.Size(80, 20);
            this.nErrorRetry.TabIndex = 3;
            this.nErrorRetry.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.nErrorRetry.ValueChanged += new System.EventHandler(this.nErrorRetry_ValueChanged);
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
            this.tpCustomUploaders.Location = new System.Drawing.Point(4, 22);
            this.tpCustomUploaders.Name = "tpCustomUploaders";
            this.tpCustomUploaders.Padding = new System.Windows.Forms.Padding(3);
            this.tpCustomUploaders.Size = new System.Drawing.Size(772, 396);
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
            // tpLanguageTranslator
            // 
            this.tpLanguageTranslator.Controls.Add(this.lblDictionary);
            this.tpLanguageTranslator.Controls.Add(this.txtDictionary);
            this.tpLanguageTranslator.Controls.Add(this.cbClipboardTranslate);
            this.tpLanguageTranslator.Controls.Add(this.txtTranslateResult);
            this.tpLanguageTranslator.Controls.Add(this.txtLanguages);
            this.tpLanguageTranslator.Controls.Add(this.btnTranslate);
            this.tpLanguageTranslator.Controls.Add(this.txtTranslateText);
            this.tpLanguageTranslator.Controls.Add(this.lblToLanguage);
            this.tpLanguageTranslator.Controls.Add(this.lblFromLanguage);
            this.tpLanguageTranslator.Controls.Add(this.cbToLanguage);
            this.tpLanguageTranslator.Controls.Add(this.cbFromLanguage);
            this.tpLanguageTranslator.Location = new System.Drawing.Point(4, 22);
            this.tpLanguageTranslator.Name = "tpLanguageTranslator";
            this.tpLanguageTranslator.Padding = new System.Windows.Forms.Padding(3);
            this.tpLanguageTranslator.Size = new System.Drawing.Size(772, 396);
            this.tpLanguageTranslator.TabIndex = 1;
            this.tpLanguageTranslator.Text = "Language Translator";
            this.tpLanguageTranslator.UseVisualStyleBackColor = true;
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
            this.txtDictionary.Location = new System.Drawing.Point(368, 48);
            this.txtDictionary.Multiline = true;
            this.txtDictionary.Name = "txtDictionary";
            this.txtDictionary.ReadOnly = true;
            this.txtDictionary.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtDictionary.Size = new System.Drawing.Size(336, 304);
            this.txtDictionary.TabIndex = 7;
            // 
            // cbClipboardTranslate
            // 
            this.cbClipboardTranslate.AutoSize = true;
            this.cbClipboardTranslate.Location = new System.Drawing.Point(16, 360);
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
            this.btnTranslate.Size = new System.Drawing.Size(336, 24);
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
            this.lblToLanguage.AutoSize = true;
            this.lblToLanguage.Location = new System.Drawing.Point(192, 24);
            this.lblToLanguage.Name = "lblToLanguage";
            this.lblToLanguage.Size = new System.Drawing.Size(23, 13);
            this.lblToLanguage.TabIndex = 3;
            this.lblToLanguage.Text = "To:";
            // 
            // lblFromLanguage
            // 
            this.lblFromLanguage.AutoSize = true;
            this.lblFromLanguage.Location = new System.Drawing.Point(16, 24);
            this.lblFromLanguage.Name = "lblFromLanguage";
            this.lblFromLanguage.Size = new System.Drawing.Size(33, 13);
            this.lblFromLanguage.TabIndex = 2;
            this.lblFromLanguage.Text = "From:";
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
            // tpAdvanced
            // 
            this.tpAdvanced.Controls.Add(this.tcAdvanced);
            this.tpAdvanced.ImageKey = "application_edit.png";
            this.tpAdvanced.Location = new System.Drawing.Point(4, 23);
            this.tpAdvanced.Name = "tpAdvanced";
            this.tpAdvanced.Padding = new System.Windows.Forms.Padding(3);
            this.tpAdvanced.Size = new System.Drawing.Size(786, 428);
            this.tpAdvanced.TabIndex = 9;
            this.tpAdvanced.Text = "Advanced";
            this.tpAdvanced.UseVisualStyleBackColor = true;
            // 
            // tcAdvanced
            // 
            this.tcAdvanced.Controls.Add(this.tpAdvAppearance);
            this.tcAdvanced.Controls.Add(this.tpAdvPaths);
            this.tcAdvanced.Controls.Add(this.tpAdvDebug);
            this.tcAdvanced.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcAdvanced.Location = new System.Drawing.Point(3, 3);
            this.tcAdvanced.Name = "tcAdvanced";
            this.tcAdvanced.SelectedIndex = 0;
            this.tcAdvanced.Size = new System.Drawing.Size(780, 422);
            this.tcAdvanced.TabIndex = 8;
            // 
            // tpAdvAppearance
            // 
            this.tpAdvAppearance.Controls.Add(this.gbAppearance);
            this.tpAdvAppearance.Controls.Add(this.gbMisc);
            this.tpAdvAppearance.Location = new System.Drawing.Point(4, 22);
            this.tpAdvAppearance.Name = "tpAdvAppearance";
            this.tpAdvAppearance.Padding = new System.Windows.Forms.Padding(3);
            this.tpAdvAppearance.Size = new System.Drawing.Size(772, 396);
            this.tpAdvAppearance.TabIndex = 0;
            this.tpAdvAppearance.Text = "Appearance";
            this.tpAdvAppearance.UseVisualStyleBackColor = true;
            // 
            // gbAppearance
            // 
            this.gbAppearance.BackColor = System.Drawing.Color.Transparent;
            this.gbAppearance.Controls.Add(this.checkBox1);
            this.gbAppearance.Controls.Add(this.chkBalloonTipOpenLink);
            this.gbAppearance.Controls.Add(this.cbShowPopup);
            this.gbAppearance.Controls.Add(this.lblTrayFlash);
            this.gbAppearance.Controls.Add(this.nudFlashIconCount);
            this.gbAppearance.Location = new System.Drawing.Point(8, 8);
            this.gbAppearance.Name = "gbAppearance";
            this.gbAppearance.Size = new System.Drawing.Size(752, 136);
            this.gbAppearance.TabIndex = 5;
            this.gbAppearance.TabStop = false;
            this.gbAppearance.Text = "After taking a Screenshot";
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(16, 104);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(311, 17);
            this.checkBox1.TabIndex = 7;
            this.checkBox1.Text = "&Capture Entire Screen if Active Window Capture or Crop fails";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // chkBalloonTipOpenLink
            // 
            this.chkBalloonTipOpenLink.AutoSize = true;
            this.chkBalloonTipOpenLink.Location = new System.Drawing.Point(16, 80);
            this.chkBalloonTipOpenLink.Name = "chkBalloonTipOpenLink";
            this.chkBalloonTipOpenLink.Size = new System.Drawing.Size(225, 17);
            this.chkBalloonTipOpenLink.TabIndex = 6;
            this.chkBalloonTipOpenLink.Text = "Open Screenshot URL/File  when Clicked";
            this.chkBalloonTipOpenLink.UseVisualStyleBackColor = true;
            this.chkBalloonTipOpenLink.CheckedChanged += new System.EventHandler(this.chkBalloonTipOpenLink_CheckedChanged);
            // 
            // cbShowPopup
            // 
            this.cbShowPopup.AutoSize = true;
            this.cbShowPopup.Location = new System.Drawing.Point(16, 56);
            this.cbShowPopup.Name = "cbShowPopup";
            this.cbShowPopup.Size = new System.Drawing.Size(231, 17);
            this.cbShowPopup.TabIndex = 5;
            this.cbShowPopup.Text = "Show Balloon Tip after upload is Completed";
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
            this.nudFlashIconCount.Location = new System.Drawing.Point(336, 24);
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
            // gbMisc
            // 
            this.gbMisc.BackColor = System.Drawing.Color.Transparent;
            this.gbMisc.Controls.Add(this.cbShowTaskbar);
            this.gbMisc.Controls.Add(this.cbOpenMainWindow);
            this.gbMisc.Controls.Add(this.cbCheckUpdates);
            this.gbMisc.Controls.Add(this.cbStartWin);
            this.gbMisc.Location = new System.Drawing.Point(8, 152);
            this.gbMisc.Name = "gbMisc";
            this.gbMisc.Size = new System.Drawing.Size(752, 128);
            this.gbMisc.TabIndex = 7;
            this.gbMisc.TabStop = false;
            this.gbMisc.Text = "Interaction";
            // 
            // cbShowTaskbar
            // 
            this.cbShowTaskbar.AutoSize = true;
            this.cbShowTaskbar.Location = new System.Drawing.Point(16, 72);
            this.cbShowTaskbar.Name = "cbShowTaskbar";
            this.cbShowTaskbar.Size = new System.Drawing.Size(150, 17);
            this.cbShowTaskbar.TabIndex = 3;
            this.cbShowTaskbar.Text = "Show ZScreen in Taskbar";
            this.cbShowTaskbar.UseVisualStyleBackColor = true;
            this.cbShowTaskbar.CheckedChanged += new System.EventHandler(this.cbShowTaskbar_CheckedChanged);
            // 
            // cbOpenMainWindow
            // 
            this.cbOpenMainWindow.AutoSize = true;
            this.cbOpenMainWindow.Location = new System.Drawing.Point(16, 48);
            this.cbOpenMainWindow.Name = "cbOpenMainWindow";
            this.cbOpenMainWindow.Size = new System.Drawing.Size(162, 17);
            this.cbOpenMainWindow.TabIndex = 2;
            this.cbOpenMainWindow.Text = "Open Main Window on Load";
            this.cbOpenMainWindow.UseVisualStyleBackColor = true;
            this.cbOpenMainWindow.CheckedChanged += new System.EventHandler(this.cbOpenMainWindow_CheckedChanged);
            // 
            // cbCheckUpdates
            // 
            this.cbCheckUpdates.AutoSize = true;
            this.cbCheckUpdates.Location = new System.Drawing.Point(16, 96);
            this.cbCheckUpdates.Name = "cbCheckUpdates";
            this.cbCheckUpdates.Size = new System.Drawing.Size(180, 17);
            this.cbCheckUpdates.TabIndex = 1;
            this.cbCheckUpdates.Text = "Automatically Check for Updates";
            this.cbCheckUpdates.UseVisualStyleBackColor = true;
            this.cbCheckUpdates.CheckedChanged += new System.EventHandler(this.cbCheckUpdates_CheckedChanged);
            // 
            // tpAdvPaths
            // 
            this.tpAdvPaths.Controls.Add(this.gbSaveLoc);
            this.tpAdvPaths.Controls.Add(this.gbSettingsExportImport);
            this.tpAdvPaths.Controls.Add(this.gbRemoteDirCache);
            this.tpAdvPaths.Location = new System.Drawing.Point(4, 22);
            this.tpAdvPaths.Name = "tpAdvPaths";
            this.tpAdvPaths.Size = new System.Drawing.Size(772, 396);
            this.tpAdvPaths.TabIndex = 2;
            this.tpAdvPaths.Text = "Paths";
            this.tpAdvPaths.UseVisualStyleBackColor = true;
            // 
            // gbSettingsExportImport
            // 
            this.gbSettingsExportImport.BackColor = System.Drawing.Color.Transparent;
            this.gbSettingsExportImport.Controls.Add(this.btnDeleteSettings);
            this.gbSettingsExportImport.Controls.Add(this.btnSettingsExport);
            this.gbSettingsExportImport.Controls.Add(this.btnBrowseConfig);
            this.gbSettingsExportImport.Controls.Add(this.btnSettingsImport);
            this.gbSettingsExportImport.Location = new System.Drawing.Point(16, 320);
            this.gbSettingsExportImport.Name = "gbSettingsExportImport";
            this.gbSettingsExportImport.Size = new System.Drawing.Size(744, 64);
            this.gbSettingsExportImport.TabIndex = 6;
            this.gbSettingsExportImport.TabStop = false;
            this.gbSettingsExportImport.Text = "Settings";
            // 
            // btnDeleteSettings
            // 
            this.btnDeleteSettings.AutoSize = true;
            this.btnDeleteSettings.Location = new System.Drawing.Point(384, 24);
            this.btnDeleteSettings.Name = "btnDeleteSettings";
            this.btnDeleteSettings.Size = new System.Drawing.Size(104, 23);
            this.btnDeleteSettings.TabIndex = 1;
            this.btnDeleteSettings.Text = "Default Settings...";
            this.btnDeleteSettings.UseVisualStyleBackColor = true;
            this.btnDeleteSettings.Click += new System.EventHandler(this.btnDeleteSettings_Click);
            // 
            // btnSettingsExport
            // 
            this.btnSettingsExport.AutoSize = true;
            this.btnSettingsExport.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnSettingsExport.Location = new System.Drawing.Point(128, 24);
            this.btnSettingsExport.Name = "btnSettingsExport";
            this.btnSettingsExport.Size = new System.Drawing.Size(104, 23);
            this.btnSettingsExport.TabIndex = 1;
            this.btnSettingsExport.Text = "Export Settings...";
            this.btnSettingsExport.UseVisualStyleBackColor = true;
            this.btnSettingsExport.Click += new System.EventHandler(this.btnSettingsExport_Click);
            // 
            // btnBrowseConfig
            // 
            this.btnBrowseConfig.AutoSize = true;
            this.btnBrowseConfig.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnBrowseConfig.Location = new System.Drawing.Point(240, 24);
            this.btnBrowseConfig.Name = "btnBrowseConfig";
            this.btnBrowseConfig.Size = new System.Drawing.Size(136, 23);
            this.btnBrowseConfig.TabIndex = 0;
            this.btnBrowseConfig.Text = "Browse Settings folder...";
            this.btnBrowseConfig.UseVisualStyleBackColor = true;
            this.btnBrowseConfig.Click += new System.EventHandler(this.btnBrowseConfig_Click);
            // 
            // btnSettingsImport
            // 
            this.btnSettingsImport.AutoSize = true;
            this.btnSettingsImport.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnSettingsImport.Location = new System.Drawing.Point(16, 24);
            this.btnSettingsImport.Name = "btnSettingsImport";
            this.btnSettingsImport.Size = new System.Drawing.Size(104, 23);
            this.btnSettingsImport.TabIndex = 0;
            this.btnSettingsImport.Text = "Import Settings...";
            this.btnSettingsImport.UseVisualStyleBackColor = true;
            this.btnSettingsImport.Click += new System.EventHandler(this.btnSettingsImport_Click);
            // 
            // gbRemoteDirCache
            // 
            this.gbRemoteDirCache.BackColor = System.Drawing.Color.Transparent;
            this.gbRemoteDirCache.Controls.Add(this.btnViewRemoteDirectory);
            this.gbRemoteDirCache.Controls.Add(this.btnBrowseCacheLocation);
            this.gbRemoteDirCache.Controls.Add(this.lblCacheSize);
            this.gbRemoteDirCache.Controls.Add(this.lblMebibytes);
            this.gbRemoteDirCache.Controls.Add(this.nudCacheSize);
            this.gbRemoteDirCache.Controls.Add(this.txtCacheDir);
            this.gbRemoteDirCache.Location = new System.Drawing.Point(16, 112);
            this.gbRemoteDirCache.Name = "gbRemoteDirCache";
            this.gbRemoteDirCache.Size = new System.Drawing.Size(744, 96);
            this.gbRemoteDirCache.TabIndex = 1;
            this.gbRemoteDirCache.TabStop = false;
            this.gbRemoteDirCache.Text = "Cache";
            // 
            // btnViewRemoteDirectory
            // 
            this.btnViewRemoteDirectory.Location = new System.Drawing.Point(496, 24);
            this.btnViewRemoteDirectory.Name = "btnViewRemoteDirectory";
            this.btnViewRemoteDirectory.Size = new System.Drawing.Size(104, 24);
            this.btnViewRemoteDirectory.TabIndex = 7;
            this.btnViewRemoteDirectory.Text = "View Directory...";
            this.btnViewRemoteDirectory.UseVisualStyleBackColor = true;
            this.btnViewRemoteDirectory.Click += new System.EventHandler(this.btnViewRemoteDirectory_Click);
            // 
            // btnBrowseCacheLocation
            // 
            this.btnBrowseCacheLocation.Location = new System.Drawing.Point(408, 24);
            this.btnBrowseCacheLocation.Name = "btnBrowseCacheLocation";
            this.btnBrowseCacheLocation.Size = new System.Drawing.Size(80, 24);
            this.btnBrowseCacheLocation.TabIndex = 6;
            this.btnBrowseCacheLocation.Text = "Browse...";
            this.btnBrowseCacheLocation.UseVisualStyleBackColor = true;
            this.btnBrowseCacheLocation.Click += new System.EventHandler(this.btnBrowseCacheLocation_Click);
            // 
            // lblCacheSize
            // 
            this.lblCacheSize.AutoSize = true;
            this.lblCacheSize.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblCacheSize.Location = new System.Drawing.Point(14, 56);
            this.lblCacheSize.Name = "lblCacheSize";
            this.lblCacheSize.Size = new System.Drawing.Size(61, 13);
            this.lblCacheSize.TabIndex = 5;
            this.lblCacheSize.Text = "Cache Size";
            // 
            // lblMebibytes
            // 
            this.lblMebibytes.AutoSize = true;
            this.lblMebibytes.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblMebibytes.Location = new System.Drawing.Point(232, 56);
            this.lblMebibytes.Name = "lblMebibytes";
            this.lblMebibytes.Size = new System.Drawing.Size(25, 13);
            this.lblMebibytes.TabIndex = 4;
            this.lblMebibytes.Text = "MiB";
            // 
            // nudCacheSize
            // 
            this.nudCacheSize.Location = new System.Drawing.Point(108, 54);
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
            this.nudCacheSize.Size = new System.Drawing.Size(120, 20);
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
            this.txtCacheDir.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.txtCacheDir.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.FileSystemDirectories;
            this.txtCacheDir.Location = new System.Drawing.Point(16, 24);
            this.txtCacheDir.Name = "txtCacheDir";
            this.txtCacheDir.Size = new System.Drawing.Size(384, 20);
            this.txtCacheDir.TabIndex = 0;
            this.txtCacheDir.TextChanged += new System.EventHandler(this.txtCacheDir_TextChanged);
            // 
            // tpAdvDebug
            // 
            this.tpAdvDebug.Controls.Add(this.groupBox1);
            this.tpAdvDebug.Controls.Add(this.gbLastSource);
            this.tpAdvDebug.Location = new System.Drawing.Point(4, 22);
            this.tpAdvDebug.Name = "tpAdvDebug";
            this.tpAdvDebug.Padding = new System.Windows.Forms.Padding(3);
            this.tpAdvDebug.Size = new System.Drawing.Size(772, 396);
            this.tpAdvDebug.TabIndex = 1;
            this.tpAdvDebug.Text = "Debug";
            this.tpAdvDebug.UseVisualStyleBackColor = true;
            // 
            // lblDebugInfo
            // 
            this.lblDebugInfo.AutoSize = true;
            this.lblDebugInfo.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDebugInfo.Location = new System.Drawing.Point(24, 56);
            this.lblDebugInfo.Name = "lblDebugInfo";
            this.lblDebugInfo.Size = new System.Drawing.Size(194, 11);
            this.lblDebugInfo.TabIndex = 27;
            this.lblDebugInfo.Text = "Debug Info - Wait 3 seconds";
            // 
            // gbLastSource
            // 
            this.gbLastSource.Controls.Add(this.btnOpenSourceString);
            this.gbLastSource.Controls.Add(this.btnOpenSourceText);
            this.gbLastSource.Controls.Add(this.btnOpenSourceBrowser);
            this.gbLastSource.Location = new System.Drawing.Point(16, 176);
            this.gbLastSource.Name = "gbLastSource";
            this.gbLastSource.Size = new System.Drawing.Size(152, 128);
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
            this.btnOpenSourceText.Location = new System.Drawing.Point(16, 56);
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
            this.btnOpenSourceBrowser.Location = new System.Drawing.Point(16, 88);
            this.btnOpenSourceBrowser.Name = "btnOpenSourceBrowser";
            this.btnOpenSourceBrowser.Size = new System.Drawing.Size(120, 23);
            this.btnOpenSourceBrowser.TabIndex = 22;
            this.btnOpenSourceBrowser.Text = "Open in Browser";
            this.btnOpenSourceBrowser.UseVisualStyleBackColor = true;
            this.btnOpenSourceBrowser.Click += new System.EventHandler(this.btnOpenSourceBrowser_Click);
            // 
            // ilApp
            // 
            this.ilApp.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ilApp.ImageStream")));
            this.ilApp.TransparentColor = System.Drawing.Color.Transparent;
            this.ilApp.Images.SetKeyName(0, "keyboard.png");
            this.ilApp.Images.SetKeyName(1, "application_edit.png");
            this.ilApp.Images.SetKeyName(2, "picture.png");
            this.ilApp.Images.SetKeyName(3, "pictures.png");
            this.ilApp.Images.SetKeyName(4, "picture_edit.png");
            this.ilApp.Images.SetKeyName(5, "world_edit.png");
            this.ilApp.Images.SetKeyName(6, "server.png");
            this.ilApp.Images.SetKeyName(7, "folder_edit.png");
            this.ilApp.Images.SetKeyName(8, "world_add.png");
            this.ilApp.Images.SetKeyName(9, "application.png");
            this.ilApp.Images.SetKeyName(10, "server_edit.png");
            this.ilApp.Images.SetKeyName(11, "application_form.png");
            this.ilApp.Images.SetKeyName(12, "camera_edit.png");
            // 
            // txtActiveHelp
            // 
            this.txtActiveHelp.BackColor = System.Drawing.SystemColors.Info;
            this.txtActiveHelp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtActiveHelp.Location = new System.Drawing.Point(0, 0);
            this.txtActiveHelp.Name = "txtActiveHelp";
            this.txtActiveHelp.ReadOnly = true;
            this.txtActiveHelp.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.txtActiveHelp.Size = new System.Drawing.Size(794, 58);
            this.txtActiveHelp.TabIndex = 75;
            this.txtActiveHelp.Text = "";
            this.txtActiveHelp.LinkClicked += new System.Windows.Forms.LinkClickedEventHandler(this.txtActiveHelp_LinkClicked);
            // 
            // splitContainerApp
            // 
            this.splitContainerApp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerApp.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainerApp.IsSplitterFixed = true;
            this.splitContainerApp.Location = new System.Drawing.Point(0, 0);
            this.splitContainerApp.Name = "splitContainerApp";
            this.splitContainerApp.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainerApp.Panel1
            // 
            this.splitContainerApp.Panel1.Controls.Add(this.tcApp);
            // 
            // splitContainerApp.Panel2
            // 
            this.splitContainerApp.Panel2.Controls.Add(this.txtActiveHelp);
            this.splitContainerApp.Size = new System.Drawing.Size(794, 514);
            this.splitContainerApp.SplitterDistance = 455;
            this.splitContainerApp.SplitterWidth = 1;
            this.splitContainerApp.TabIndex = 76;
            // 
            // debugTimer
            // 
            this.debugTimer.Interval = 1000;
            this.debugTimer.Tick += new System.EventHandler(this.debugTimer_Tick);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnCopyStats);
            this.groupBox1.Controls.Add(this.lblDebugInfo);
            this.groupBox1.Location = new System.Drawing.Point(16, 16);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(280, 152);
            this.groupBox1.TabIndex = 28;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Statistics";
            // 
            // btnCopyStats
            // 
            this.btnCopyStats.Location = new System.Drawing.Point(16, 24);
            this.btnCopyStats.Name = "btnCopyStats";
            this.btnCopyStats.Size = new System.Drawing.Size(120, 23);
            this.btnCopyStats.TabIndex = 29;
            this.btnCopyStats.Text = "Copy to Clipboard";
            this.btnCopyStats.UseVisualStyleBackColor = true;
            this.btnCopyStats.Click += new System.EventHandler(this.btnCopyStats_Click);
            // 
            // ZScreen
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(794, 514);
            this.Controls.Add(this.splitContainerApp);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "ZScreen";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ZScreen";
            this.WindowState = System.Windows.Forms.FormWindowState.Minimized;
            this.Deactivate += new System.EventHandler(this.ZScreen_Deactivate);
            this.Load += new System.EventHandler(this.ZScreen_Load);
            this.Shown += new System.EventHandler(this.ZScreen_Shown);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.ZScreen_DragDrop);
            this.Leave += new System.EventHandler(this.ZScreen_Leave);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.ZScreen_DragEnter);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ZScreen_FormClosing);
            this.Resize += new System.EventHandler(this.ZScreen_Resize);
            this.cmTray.ResumeLayout(false);
            this.tpScreenshots.ResumeLayout(false);
            this.tpScreenshots.PerformLayout();
            this.gbScreenshotPreview.ResumeLayout(false);
            this.gbScreenshotPreview.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbHistoryThumb)).EndInit();
            this.cmsHistory.ResumeLayout(false);
            this.tpFile.ResumeLayout(false);
            this.tcFileSettings.ResumeLayout(false);
            this.tpCaptureCrop.ResumeLayout(false);
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudCropBorderSize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbCropBorderColor)).EndInit();
            this.tpFileNaming.ResumeLayout(false);
            this.groupBox8.ResumeLayout(false);
            this.groupBox8.PerformLayout();
            this.gbCodeTitle.ResumeLayout(false);
            this.gbCodeTitle.PerformLayout();
            this.gbAutoFileName.ResumeLayout(false);
            this.gbAutoFileName.PerformLayout();
            this.tpFileSettingsWatermark.ResumeLayout(false);
            this.gbWatermarkPreview.ResumeLayout(false);
            this.gbWatermarkPreview.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbWatermarkShow)).EndInit();
            this.gbWatermarkGeneral.ResumeLayout(false);
            this.gbWatermarkGeneral.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudWatermarkOffset)).EndInit();
            this.gbWatermarkBackground.ResumeLayout(false);
            this.gbWatermarkBackground.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudWatermarkCornerRadius)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudWatermarkBackTrans)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbWatermarkGradient1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbWatermarkBorderColor)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbWatermarkGradient2)).EndInit();
            this.gbWatermarkText.ResumeLayout(false);
            this.gbWatermarkText.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudWatermarkFontTrans)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbWatermarkFontColor)).EndInit();
            this.tpCaptureQuality.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtImageQuality)).EndInit();
            this.gbSaveLoc.ResumeLayout(false);
            this.gbSaveLoc.PerformLayout();
            this.tpImageSoftware.ResumeLayout(false);
            this.gbImageSoftwareList.ResumeLayout(false);
            this.gbImageSoftwareActive.ResumeLayout(false);
            this.gbImageSoftwareActive.PerformLayout();
            this.tpFTP.ResumeLayout(false);
            this.tpFTP.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.gbFTPAccountActive.ResumeLayout(false);
            this.gbFTPAccountActive.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtServerPort)).EndInit();
            this.gbFTPMode.ResumeLayout(false);
            this.gbFTPMode.PerformLayout();
            this.tpHotKeys.ResumeLayout(false);
            this.tpHotKeys.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvHotkeys)).EndInit();
            this.tpMain.ResumeLayout(false);
            this.tpMain.PerformLayout();
            this.gbActiveHelp.ResumeLayout(false);
            this.gbActiveHelp.PerformLayout();
            this.gbMainOptions.ResumeLayout(false);
            this.gbMainOptions.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nScreenshotDelay)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbLogo)).EndInit();
            this.tcApp.ResumeLayout(false);
            this.tpHTTP.ResumeLayout(false);
            this.tcHTTP.ResumeLayout(false);
            this.tpImageUploaders.ResumeLayout(false);
            this.tpImageUploaders.PerformLayout();
            this.gbImageShack.ResumeLayout(false);
            this.gbImageShack.PerformLayout();
            this.gbTinyPic.ResumeLayout(false);
            this.gbTinyPic.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nErrorRetry)).EndInit();
            this.tpCustomUploaders.ResumeLayout(false);
            this.tpCustomUploaders.PerformLayout();
            this.gbImageUploaders.ResumeLayout(false);
            this.gbImageUploaders.PerformLayout();
            this.gbRegexp.ResumeLayout(false);
            this.gbRegexp.PerformLayout();
            this.gbArguments.ResumeLayout(false);
            this.gbArguments.PerformLayout();
            this.tpLanguageTranslator.ResumeLayout(false);
            this.tpLanguageTranslator.PerformLayout();
            this.tpAdvanced.ResumeLayout(false);
            this.tcAdvanced.ResumeLayout(false);
            this.tpAdvAppearance.ResumeLayout(false);
            this.gbAppearance.ResumeLayout(false);
            this.gbAppearance.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudFlashIconCount)).EndInit();
            this.gbMisc.ResumeLayout(false);
            this.gbMisc.PerformLayout();
            this.tpAdvPaths.ResumeLayout(false);
            this.gbSettingsExportImport.ResumeLayout(false);
            this.gbSettingsExportImport.PerformLayout();
            this.gbRemoteDirCache.ResumeLayout(false);
            this.gbRemoteDirCache.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudCacheSize)).EndInit();
            this.tpAdvDebug.ResumeLayout(false);
            this.gbLastSource.ResumeLayout(false);
            this.splitContainerApp.Panel1.ResumeLayout(false);
            this.splitContainerApp.Panel2.ResumeLayout(false);
            this.splitContainerApp.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.NotifyIcon niTray;
        private System.Windows.Forms.ContextMenuStrip cmTray;
        private System.Windows.Forms.ToolStripMenuItem exitZScreenToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tsmImageSoftware;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem tsmSettings;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem tsmViewDirectory;
        private System.Windows.Forms.TabPage tpScreenshots;
        private System.Windows.Forms.CheckBox cbReverse;
        private System.Windows.Forms.CheckBox cbAddSpace;
        private System.Windows.Forms.Button btnCopyToClipboard;
        private System.Windows.Forms.TabPage tpFile;
        private System.Windows.Forms.Label lblCodeI;
        private System.Windows.Forms.Label lblCodeT;
        private System.Windows.Forms.Label lblCodeMo;
        private System.Windows.Forms.Label lblCodePm;
        private System.Windows.Forms.Label lblCodeD;
        private System.Windows.Forms.Label lblCodeS;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label lblCodeMi;
        private System.Windows.Forms.Label lblCodeY;
        private System.Windows.Forms.Label lblCodeH;
        private System.Windows.Forms.Button btnResetEntireScreen;
        private System.Windows.Forms.TextBox txtFileDirectory;
        private System.Windows.Forms.TextBox txtEntireScreen;
        private System.Windows.Forms.TextBox txtActiveWindow;
        private System.Windows.Forms.Button btnResetActiveWindow;
        private System.Windows.Forms.Button btnBrowseDirectory;
        private System.Windows.Forms.TabPage tpImageSoftware;
        private System.Windows.Forms.CheckBox cbStartWin;
        private System.Windows.Forms.Button btnUpdateImageSoftware;
        private System.Windows.Forms.Button btnBrowseImageSoftware;
        private System.Windows.Forms.TextBox txtImageSoftwarePath;
        private System.Windows.Forms.TabPage tpFTP;
        private System.Windows.Forms.Label lblPort;
        private System.Windows.Forms.TextBox txtServer;
        private System.Windows.Forms.Label lblServer;
        private System.Windows.Forms.TextBox txtErrorFTP;
        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.Button btnTestConnection;
        private System.Windows.Forms.Label lblUsername;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Label lblHttpPath;
        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.TextBox txtHttpPath;
        private System.Windows.Forms.Button btnUpdateFTP;
        private System.Windows.Forms.TextBox txtPath;
        private System.Windows.Forms.Label lblFtpPath;
        private System.Windows.Forms.TabPage tpHotKeys;
        private System.Windows.Forms.TabPage tpMain;
        private System.Windows.Forms.Label lblLogo;
        private System.Windows.Forms.PictureBox pbLogo;
        private System.Windows.Forms.TabControl tcApp;
        private System.Windows.Forms.ComboBox cmbFileFormat;
        private System.Windows.Forms.Label lblKB;
        private System.Windows.Forms.TextBox txtSwitchAfter;
        private System.Windows.Forms.Label lblAfter;
        private System.Windows.Forms.Label lblSwitchTo;
        private System.Windows.Forms.ComboBox cmbSwitchFormat;
        private System.Windows.Forms.Label lblFileFormat;
        private System.Windows.Forms.ToolStripMenuItem tsmViewRemote;
        private System.Windows.Forms.Button btnCodesT;
        private System.Windows.Forms.Button btnCodesMo;
        private System.Windows.Forms.Button btnCodesI;
        private System.Windows.Forms.Button btnCodesPm;
        private System.Windows.Forms.Button btnCodesS;
        private System.Windows.Forms.Button btnCodesMi;
        private System.Windows.Forms.Button btnCodesH;
        private System.Windows.Forms.Button btnCodesY;
        private System.Windows.Forms.Button btnCodesD;
        private System.Windows.Forms.Label lblQuality;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.ToolStripMenuItem tsmHotkeys;
        private System.Windows.Forms.ToolStripMenuItem tsmFTPSettings;
        private System.Windows.Forms.ToolStripMenuItem tsmImageSoftwareSettings;
        private System.Windows.Forms.ToolStripMenuItem tsmFileSettings;
        private System.Windows.Forms.ToolStripMenuItem tsmHistory;
        private System.Windows.Forms.CheckBox cbDeleteLocal;
        private System.Windows.Forms.GroupBox gbFTPMode;
        private System.Windows.Forms.RadioButton rbFTPActive;
        private System.Windows.Forms.RadioButton rbFTPPassive;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Button btnAddAccount;
        private System.Windows.Forms.Button btnDeleteFTP;
        private System.Windows.Forms.ListBox lbHistory;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.TabPage tpAdvanced;
        private System.Windows.Forms.Button btnBrowseConfig;
        private System.Windows.Forms.ToolStripMenuItem tsmLic;
        private System.Windows.Forms.GroupBox gbRemoteDirCache;
        private System.Windows.Forms.TextBox txtCacheDir;
        private System.Windows.Forms.Label lblMebibytes;
        private System.Windows.Forms.NumericUpDown nudCacheSize;
        private System.Windows.Forms.Label lblCacheSize;
        private System.Windows.Forms.GroupBox gbCodeTitle;
        private System.Windows.Forms.CheckBox chkManualNaming;
        private System.Windows.Forms.GroupBox gbAutoFileName;
        private System.Windows.Forms.GroupBox gbSaveLoc;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ToolStripMenuItem tsmPromptFileName;
        private System.Windows.Forms.NumericUpDown txtImageQuality;
        private System.Windows.Forms.NumericUpDown txtServerPort;
        private System.Windows.Forms.Label lblTrayFlash;
        private System.Windows.Forms.NumericUpDown nudFlashIconCount;
        private System.Windows.Forms.ToolStripMenuItem tsmAboutMain;
        private System.Windows.Forms.Button btnAddImageSoftware;
        private System.Windows.Forms.Button btnDeleteImageSoftware;
        private System.Windows.Forms.TextBox txtImageSoftwareName;
        private System.Windows.Forms.Label lblImageSoftwarePath;
        private System.Windows.Forms.Label lblImageSoftwareName;
        private System.Windows.Forms.Button btnAccsExport;
        private System.Windows.Forms.Button btnAccsImport;
        private System.Windows.Forms.GroupBox gbAppearance;
        private System.Windows.Forms.GroupBox gbSettingsExportImport;
        private System.Windows.Forms.Button btnSettingsExport;
        private System.Windows.Forms.Button btnSettingsImport;
        private System.Windows.Forms.ToolStripMenuItem tsmAdvanced;
        private System.Windows.Forms.GroupBox gbImageSoftwareActive;
        private System.Windows.Forms.GroupBox gbImageSoftwareList;
        private System.Windows.Forms.GroupBox gbFTPAccountActive;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.ComboBox cboClipboardTextMode;
        private System.Windows.Forms.CheckBox chkEnableThumbnail;
        private System.Windows.Forms.GroupBox gbMainOptions;
        private System.Windows.Forms.ComboBox cboScreenshotDest;
        private System.Windows.Forms.ToolStripMenuItem tsmSendTo;
        private System.Windows.Forms.ToolStripMenuItem tsmDestClipboard;
        private System.Windows.Forms.ToolStripMenuItem tsmDestFile;
        private System.Windows.Forms.ToolStripMenuItem tsmDestFTP;
        private System.Windows.Forms.ToolStripMenuItem tsmDestImageShack;
        private System.Windows.Forms.ToolStripMenuItem tsmCbCopy;
        private System.Windows.Forms.Label lblFirstRun;
        private System.Windows.Forms.GroupBox gbMisc;
        private System.Windows.Forms.TabPage tpHTTP;
        private System.Windows.Forms.GroupBox gbImageShack;
        private System.Windows.Forms.Label lblImageShackRegistrationCode;
        private System.Windows.Forms.TextBox txtImageShackRegistrationCode;
        private System.Windows.Forms.ToolStripMenuItem tsmHTTPSettings;
        private System.Windows.Forms.ToolStripMenuItem tsmDestTinyPic;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        private System.Windows.Forms.CheckBox cbShowPopup;
        private System.Windows.Forms.Button btnDeleteSettings;
        private System.Windows.Forms.Label lblDelaySeconds;
        private System.Windows.Forms.NumericUpDown nScreenshotDelay;
        private System.Windows.Forms.Label lblScreenshotDelay;
        private System.Windows.Forms.Label lblScreenshotDestination;
        private System.Windows.Forms.CheckBox cbRegionHotkeyInfo;
        private System.Windows.Forms.CheckBox cbRegionRectangleInfo;
        private System.Windows.Forms.ToolStripMenuItem tsmQuickOptions;
        private System.Windows.Forms.Button btnRegCodeImageShack;
        private System.Windows.Forms.Label lblErrorRetry;
        private System.Windows.Forms.NumericUpDown nErrorRetry;
        private System.Windows.Forms.TabPage tpCustomUploaders;
        private System.Windows.Forms.ListView lvArguments;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.Button btnArgRemove;
        private System.Windows.Forms.Button btnArgAdd;
        private System.Windows.Forms.TextBox txtArg2;
        private System.Windows.Forms.TextBox txtArg1;
        private System.Windows.Forms.GroupBox gbArguments;
        private System.Windows.Forms.TextBox txtFileForm;
        private System.Windows.Forms.Label lblFileForm;
        private System.Windows.Forms.Label lblUploadURL;
        private System.Windows.Forms.TextBox txtUploadURL;
        private System.Windows.Forms.GroupBox gbImageUploaders;
        private System.Windows.Forms.GroupBox gbRegexp;
        private System.Windows.Forms.TextBox txtRegexp;
        private System.Windows.Forms.ListView lvRegexps;
        private System.Windows.Forms.Button btnRegexpRemove;
        private System.Windows.Forms.Button btnRegexpAdd;
        private System.Windows.Forms.Button btnUploaderRemove;
        private System.Windows.Forms.TextBox txtUploader;
        private System.Windows.Forms.Button btnUploaderAdd;
        private System.Windows.Forms.TextBox txtFullImage;
        private System.Windows.Forms.TextBox txtThumbnail;
        private System.Windows.Forms.Label lblFullImage;
        private System.Windows.Forms.Label lblThumbnail;
        private System.Windows.Forms.Button btnUploadersTest;
        private System.Windows.Forms.Button btnUploaderExport;
        private System.Windows.Forms.Button btnUploaderImport;
        private System.Windows.Forms.ColumnHeader lvRegexpsColumn;
        private System.Windows.Forms.Button btnGalleryImageShack;
        private System.Windows.Forms.Button btnUploadersClear;
        private System.Windows.Forms.Button btnUploaderUpdate;
        private System.Windows.Forms.ListBox lbUploader;
        private System.Windows.Forms.DataGridView dgvHotkeys;
        private System.Windows.Forms.Label lblHotkeyStatus;
        private System.Windows.Forms.Button btnArgEdit;
        private System.Windows.Forms.Button btnRegexpEdit;
        private System.Windows.Forms.RichTextBox txtUploadersLog;
        private System.Windows.Forms.DataGridViewTextBoxColumn chHotkeys_Description;
        private System.Windows.Forms.DataGridViewButtonColumn chHotkeys_Keys;
        private System.Windows.Forms.ToolStripMenuItem tsmDestCustomHTTP;
        private System.Windows.Forms.RichTextBox txtActiveHelp;
        private System.Windows.Forms.CheckBox cbActiveHelp;
        private System.Windows.Forms.GroupBox gbTinyPic;
        private System.Windows.Forms.Button btnRegCodeTinyPic;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtTinyPicShuk;
        private System.Windows.Forms.ComboBox cbCropStyle;
        private System.Windows.Forms.Label lblCropBorderColor;
        private System.Windows.Forms.PictureBox pbCropBorderColor;
        private System.Windows.Forms.Label lblCropBorderSize;
        private System.Windows.Forms.NumericUpDown nudCropBorderSize;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.LinkLabel llblBugReports;
        private System.Windows.Forms.ListBox lbImageSoftware;
        private System.Windows.Forms.ListBox lbFTPAccounts;
        private System.Windows.Forms.CheckBox cbCompleteSound;
        private System.Windows.Forms.CheckBox cbCheckUpdates;
        private System.Windows.Forms.ContextMenuStrip cmsHistory;
        private System.Windows.Forms.ToolStripMenuItem tsmCopyCbHistory;
        private System.Windows.Forms.PictureBox pbHistoryThumb;
        private System.Windows.Forms.TextBox txtHistoryRemotePath;
        private System.Windows.Forms.TextBox txtHistoryLocalPath;
        private System.Windows.Forms.Label lblHistoryRemotePath;
        private System.Windows.Forms.Label lblHistoryLocalPath;
        private System.Windows.Forms.Button btnScreenshotBrowse;
        private System.Windows.Forms.Button btnScreenshotOpen;
        private System.Windows.Forms.GroupBox gbScreenshotPreview;
        private System.Windows.Forms.ImageList ilApp;
        private System.Windows.Forms.CheckBox cbShowWatermark;
        private System.Windows.Forms.CheckBox cbShowCursor;
        private System.Windows.Forms.Label lblWatermarkFont;
        private System.Windows.Forms.Button btnWatermarkFont;
        private System.Windows.Forms.NumericUpDown nudWatermarkOffset;
        private System.Windows.Forms.Label lblWatermarkOffset;
        private System.Windows.Forms.NumericUpDown nudWatermarkBackTrans;
        private System.Windows.Forms.Label lblWatermarkBackTrans;
        private System.Windows.Forms.Label lblWatermarkBackTransTip;
        private System.Windows.Forms.ToolStripMenuItem captureToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem rectangularRegionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tsmScrenshotFromClipboard;
        private System.Windows.Forms.ToolStripMenuItem tsmDropWindow;
        private System.Windows.Forms.ToolStripMenuItem entireScreenToolStripMenuItem;
        private System.Windows.Forms.PictureBox pbWatermarkGradient2;
        private System.Windows.Forms.PictureBox pbWatermarkGradient1;
        private System.Windows.Forms.Label lblWatermarkBackColors;
        private System.Windows.Forms.PictureBox pbWatermarkShow;
        private System.Windows.Forms.PictureBox pbWatermarkBorderColor;
        private System.Windows.Forms.TabControl tcFileSettings;
        private System.Windows.Forms.TabPage tpCaptureQuality;
        private System.Windows.Forms.TabPage tpFileSettingsWatermark;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TabPage tpFileNaming;
        private System.Windows.Forms.GroupBox groupBox8;
        private System.Windows.Forms.Label lblWatermarkText;
        private System.Windows.Forms.TextBox txtWatermarkText;
        private System.Windows.Forms.PictureBox pbWatermarkFontColor;
        private System.Windows.Forms.Label lblWatermarkPosition;
        private System.Windows.Forms.ComboBox cbWatermarkPosition;
        private System.Windows.Forms.Label lblWatermarkBackColorsTip;
        private System.Windows.Forms.Label lblWatermarkFontTransTip;
        private System.Windows.Forms.NumericUpDown nudWatermarkFontTrans;
        private System.Windows.Forms.Label lblWatermarkFontTrans;
        private System.Windows.Forms.Label lblEntireScreenPreview;
        private System.Windows.Forms.Label lblActiveWindowPreview;
        private System.Windows.Forms.GroupBox gbWatermarkGeneral;
        private System.Windows.Forms.GroupBox gbWatermarkBackground;
        private System.Windows.Forms.GroupBox gbWatermarkText;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown nudWatermarkCornerRadius;
        private System.Windows.Forms.Label lblWatermarkCornerRadiusTip;
        private System.Windows.Forms.ToolStripMenuItem lastRectangularRegionToolStripMenuItem;
        private System.Windows.Forms.Label lblWatermarkTextTip;
        private System.Windows.Forms.ComboBox cbWatermarkGradientType;
        private System.Windows.Forms.Label lblWatermarkGradientType;
        private System.Windows.Forms.SplitContainer splitContainerApp;
        private System.Windows.Forms.ToolStripMenuItem copyImageToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
        private System.Windows.Forms.Button btnViewLocalDirectory;
        private System.Windows.Forms.Button btnBrowseCacheLocation;
        private System.Windows.Forms.Button btnViewRemoteDirectory;
        private System.Windows.Forms.CheckBox cbOpenMainWindow;
        private System.Windows.Forms.CheckBox cbShowTaskbar;
        private System.Windows.Forms.TabPage tpCaptureCrop;
        private System.Windows.Forms.LinkLabel llProjectPage;
        private System.Windows.Forms.LinkLabel llWebsite;
        private System.Windows.Forms.ToolStripMenuItem openLocalFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem browseURLToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator8;
        private System.Windows.Forms.CheckBox chkBalloonTipOpenLink;
        private System.Windows.Forms.TabControl tcHTTP;
        private System.Windows.Forms.TabPage tpImageUploaders;
        private System.Windows.Forms.TabPage tpLanguageTranslator;
        private System.Windows.Forms.ComboBox cbToLanguage;
        private System.Windows.Forms.ComboBox cbFromLanguage;
        private System.Windows.Forms.Label lblFromLanguage;
        private System.Windows.Forms.TextBox txtTranslateText;
        private System.Windows.Forms.Label lblToLanguage;
        private System.Windows.Forms.Button btnTranslate;
        private System.Windows.Forms.TextBox txtTranslateResult;
        private System.Windows.Forms.TextBox txtLanguages;
        private System.Windows.Forms.CheckBox cbClipboardTranslate;
        private System.Windows.Forms.GroupBox gbWatermarkPreview;
        private System.Windows.Forms.Label lblDictionary;
        private System.Windows.Forms.TextBox txtDictionary;
        private System.Windows.Forms.ToolStripMenuItem cmVersionHistory;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cboUploadMode;
        private System.Windows.Forms.ToolStripMenuItem openSourceToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copySourceToClipboardStringToolStripMenuItem;
        private System.Windows.Forms.Button btnOpenSourceText;
        private System.Windows.Forms.Button btnOpenSourceBrowser;
        private System.Windows.Forms.Button btnOpenSourceString;
        private System.Windows.Forms.ToolStripMenuItem openSourceInDefaultWebBrowserHTMLToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cmsRetryUpload;
        private System.Windows.Forms.GroupBox gbActiveHelp;
        private System.Windows.Forms.CheckBox chkGTActiveHelp;
        private System.Windows.Forms.ComboBox cbHelpToLanguage;
        private System.Windows.Forms.TabControl tcAdvanced;
        private System.Windows.Forms.TabPage tpAdvAppearance;
        private System.Windows.Forms.TabPage tpAdvDebug;
        private System.Windows.Forms.TabPage tpAdvPaths;
        private System.Windows.Forms.GroupBox gbLastSource;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Label lblDebugInfo;
        private System.Windows.Forms.Timer debugTimer;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnCopyStats;

    }
}
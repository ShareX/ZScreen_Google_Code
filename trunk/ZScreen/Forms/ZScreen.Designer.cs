using ZScreenLib;
using UploadersLib;
using System.Windows.Forms;
using System.Diagnostics;

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
            this.niTray = new System.Windows.Forms.NotifyIcon(this.components);
            this.cmTray = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmEntireScreen = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmSelectedWindow = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmCropShot = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmLastCropShot = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmCaptureShape = new System.Windows.Forms.ToolStripMenuItem();
            this.autoScreenshotsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmEditinImageSoftware = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmFileUpload = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmClipboardUpload = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmDragDropWindow = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmLanguageTranslator = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmScreenColorPicker = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiTabs = new System.Windows.Forms.ToolStripMenuItem();
            this.historyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmViewLocalDirectory = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmFTPClient = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmLicense = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmVersionHistory = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmExitZScreen = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmActions = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.tmrApp = new System.Windows.Forms.Timer(this.components);
            this.tcMain = new System.Windows.Forms.TabControl();
            this.tpMain = new System.Windows.Forms.TabPage();
            this.tsLinks = new System.Windows.Forms.ToolStrip();
            this.tsbLinkHome = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton3 = new System.Windows.Forms.ToolStripButton();
            this.tsMainTab = new System.Windows.Forms.ToolStrip();
            this.tsbFullscreenCapture = new System.Windows.Forms.ToolStripButton();
            this.tsbActiveWindow = new System.Windows.Forms.ToolStripButton();
            this.tsbSelectedWindow = new System.Windows.Forms.ToolStripButton();
            this.tsbCropShot = new System.Windows.Forms.ToolStripButton();
            this.tsbLastCropShot = new System.Windows.Forms.ToolStripButton();
            this.tsbFreehandCropShot = new System.Windows.Forms.ToolStripButton();
            this.tsbAutoCapture = new System.Windows.Forms.ToolStripButton();
            this.tssMaintoolbar1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbFileUpload = new System.Windows.Forms.ToolStripButton();
            this.tsbClipboardUpload = new System.Windows.Forms.ToolStripButton();
            this.tsbDragDropWindow = new System.Windows.Forms.ToolStripButton();
            this.tsbLanguageTranslator = new System.Windows.Forms.ToolStripButton();
            this.tsbScreenColorPicker = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbOpenHistory = new System.Windows.Forms.ToolStripButton();
            this.tsbImageDirectory = new System.Windows.Forms.ToolStripButton();
            this.tsbAbout = new System.Windows.Forms.ToolStripButton();
            this.tsbDonate = new System.Windows.Forms.ToolStripLabel();
            this.pbLogo = new System.Windows.Forms.PictureBox();
            this.lblFileSystemNote = new System.Windows.Forms.Label();
            this.gbImageSettings = new System.Windows.Forms.GroupBox();
            this.chkShowUploadResults = new System.Windows.Forms.CheckBox();
            this.chkShortenURL = new System.Windows.Forms.CheckBox();
            this.chkPerformActions = new System.Windows.Forms.CheckBox();
            this.lblScreenshotDelay = new System.Windows.Forms.Label();
            this.nudScreenshotDelay = new ZScreenGUI.NumericUpDownTimer();
            this.chkShowCursor = new System.Windows.Forms.CheckBox();
            this.chkManualNaming = new System.Windows.Forms.CheckBox();
            this.ucDestOptions = new ZScreenLib.DestSelector();
            this.tpHotkeys = new System.Windows.Forms.TabPage();
            this.btnResetHotkeys = new System.Windows.Forms.Button();
            this.lblHotkeyStatus = new System.Windows.Forms.Label();
            this.dgvHotkeys = new System.Windows.Forms.DataGridView();
            this.chHotkeys_Description = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.chHotkeys_Keys = new System.Windows.Forms.DataGridViewButtonColumn();
            this.DefaultKeys = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tpMainInput = new System.Windows.Forms.TabPage();
            this.tcCapture = new System.Windows.Forms.TabControl();
            this.tpActivewindow = new System.Windows.Forms.TabPage();
            this.chkActiveWindowTryCaptureChildren = new System.Windows.Forms.CheckBox();
            this.chkSelectedWindowCleanTransparentCorners = new System.Windows.Forms.CheckBox();
            this.chkSelectedWindowShowCheckers = new System.Windows.Forms.CheckBox();
            this.chkSelectedWindowIncludeShadow = new System.Windows.Forms.CheckBox();
            this.chkActiveWindowPreferDWM = new System.Windows.Forms.CheckBox();
            this.chkSelectedWindowCleanBackground = new System.Windows.Forms.CheckBox();
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
            this.tpCropShot = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cboCropEngine = new System.Windows.Forms.ComboBox();
            this.gbCropShotMagnifyingGlass = new System.Windows.Forms.GroupBox();
            this.chkCropShowMagnifyingGlass = new System.Windows.Forms.CheckBox();
            this.gbCropDynamicRegionBorderColorSettings = new System.Windows.Forms.GroupBox();
            this.nudCropRegionStep = new System.Windows.Forms.NumericUpDown();
            this.nudCropHueRange = new System.Windows.Forms.NumericUpDown();
            this.cbCropDynamicBorderColor = new System.Windows.Forms.CheckBox();
            this.lblCropRegionInterval = new System.Windows.Forms.Label();
            this.lblCropHueRange = new System.Windows.Forms.Label();
            this.lblCropRegionStep = new System.Windows.Forms.Label();
            this.nudCropRegionInterval = new System.Windows.Forms.NumericUpDown();
            this.gbCropRegion = new System.Windows.Forms.GroupBox();
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
            this.gbCropCrosshairSettings = new System.Windows.Forms.GroupBox();
            this.chkCropDynamicCrosshair = new System.Windows.Forms.CheckBox();
            this.lblCropCrosshairStep = new System.Windows.Forms.Label();
            this.chkCropShowBigCross = new System.Windows.Forms.CheckBox();
            this.pbCropCrosshairColor = new System.Windows.Forms.PictureBox();
            this.lblCropCrosshairInterval = new System.Windows.Forms.Label();
            this.lblCropCrosshairColor = new System.Windows.Forms.Label();
            this.nudCrosshairLineCount = new System.Windows.Forms.NumericUpDown();
            this.nudCropCrosshairInterval = new System.Windows.Forms.NumericUpDown();
            this.nudCrosshairLineSize = new System.Windows.Forms.NumericUpDown();
            this.nudCropCrosshairStep = new System.Windows.Forms.NumericUpDown();
            this.lblCrosshairLineSize = new System.Windows.Forms.Label();
            this.lblCrosshairLineCount = new System.Windows.Forms.Label();
            this.gbCropGridMode = new System.Windows.Forms.GroupBox();
            this.cboCropGridMode = new System.Windows.Forms.CheckBox();
            this.nudCropGridHeight = new System.Windows.Forms.NumericUpDown();
            this.lblGridSizeWidth = new System.Windows.Forms.Label();
            this.lblGridSize = new System.Windows.Forms.Label();
            this.lblGridSizeHeight = new System.Windows.Forms.Label();
            this.nudCropGridWidth = new System.Windows.Forms.NumericUpDown();
            this.tpCropShotLast = new System.Windows.Forms.TabPage();
            this.btnLastCropShotReset = new System.Windows.Forms.Button();
            this.tpCaptureShape = new System.Windows.Forms.TabPage();
            this.pgSurfaceConfig = new System.Windows.Forms.PropertyGrid();
            this.tpFreehandCropShot = new System.Windows.Forms.TabPage();
            this.cbFreehandCropShowRectangleBorder = new System.Windows.Forms.CheckBox();
            this.cbFreehandCropAutoClose = new System.Windows.Forms.CheckBox();
            this.cbFreehandCropAutoUpload = new System.Windows.Forms.CheckBox();
            this.cbFreehandCropShowHelpText = new System.Windows.Forms.CheckBox();
            this.tpCaptureClipboard = new System.Windows.Forms.TabPage();
            this.gbMonitorClipboard = new System.Windows.Forms.GroupBox();
            this.chkMonUrls = new System.Windows.Forms.CheckBox();
            this.chkMonFiles = new System.Windows.Forms.CheckBox();
            this.chkMonImages = new System.Windows.Forms.CheckBox();
            this.chkMonText = new System.Windows.Forms.CheckBox();
            this.tpMainActions = new System.Windows.Forms.TabPage();
            this.lblNoteActions = new System.Windows.Forms.Label();
            this.lbSoftware = new System.Windows.Forms.CheckedListBox();
            this.pgEditorsImage = new System.Windows.Forms.PropertyGrid();
            this.btnActionsRemove = new System.Windows.Forms.Button();
            this.btnAddImageSoftware = new System.Windows.Forms.Button();
            this.tpOptions = new System.Windows.Forms.TabPage();
            this.tcOptions = new System.Windows.Forms.TabControl();
            this.tpOptionsGeneral = new System.Windows.Forms.TabPage();
            this.gbUpdates = new System.Windows.Forms.GroupBox();
            this.cboReleaseChannel = new System.Windows.Forms.ComboBox();
            this.lblUpdateInfo = new System.Windows.Forms.Label();
            this.btnCheckUpdate = new System.Windows.Forms.Button();
            this.chkCheckUpdates = new System.Windows.Forms.CheckBox();
            this.gbMisc = new System.Windows.Forms.GroupBox();
            this.chkShellExt = new System.Windows.Forms.CheckBox();
            this.chkWindows7TaskbarIntegration = new System.Windows.Forms.CheckBox();
            this.cbAutoSaveSettings = new System.Windows.Forms.CheckBox();
            this.cbShowHelpBalloonTips = new System.Windows.Forms.CheckBox();
            this.chkShowTaskbar = new System.Windows.Forms.CheckBox();
            this.chkOpenMainWindow = new System.Windows.Forms.CheckBox();
            this.chkStartWin = new System.Windows.Forms.CheckBox();
            this.gbWindowButtons = new System.Windows.Forms.GroupBox();
            this.cboCloseButtonAction = new System.Windows.Forms.ComboBox();
            this.cboMinimizeButtonAction = new System.Windows.Forms.ComboBox();
            this.lblCloseButtonAction = new System.Windows.Forms.Label();
            this.lblMinimizeButtonAction = new System.Windows.Forms.Label();
            this.tpCaptureQuality = new System.Windows.Forms.TabPage();
            this.gbImageSize = new System.Windows.Forms.GroupBox();
            this.lblImageSizeFixedAutoScale = new System.Windows.Forms.Label();
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
            this.cboJpgSubSampling = new System.Windows.Forms.ComboBox();
            this.cboJpgQuality = new System.Windows.Forms.ComboBox();
            this.cbGIFQuality = new System.Windows.Forms.ComboBox();
            this.lblGIFQuality = new System.Windows.Forms.Label();
            this.nudSwitchAfter = new System.Windows.Forms.NumericUpDown();
            this.lblQuality = new System.Windows.Forms.Label();
            this.cboSwitchFormat = new System.Windows.Forms.ComboBox();
            this.lblFileFormat = new System.Windows.Forms.Label();
            this.cboFileFormat = new System.Windows.Forms.ComboBox();
            this.lblKB = new System.Windows.Forms.Label();
            this.lblAfter = new System.Windows.Forms.Label();
            this.lblSwitchTo = new System.Windows.Forms.Label();
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
            this.tpPaths = new System.Windows.Forms.TabPage();
            this.gbRoot = new System.Windows.Forms.GroupBox();
            this.btnViewRootDir = new System.Windows.Forms.Button();
            this.btnRelocateRootDir = new System.Windows.Forms.Button();
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
            this.gbLogs = new System.Windows.Forms.GroupBox();
            this.btnViewCacheDir = new System.Windows.Forms.Button();
            this.txtLogsDir = new System.Windows.Forms.TextBox();
            this.tpFileNaming = new System.Windows.Forms.TabPage();
            this.chkOverwriteFiles = new System.Windows.Forms.CheckBox();
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
            this.tpTreeGUI = new System.Windows.Forms.TabPage();
            this.pgIndexer = new System.Windows.Forms.PropertyGrid();
            this.tpInteraction = new System.Windows.Forms.TabPage();
            this.btnOptionsBalloonTip = new System.Windows.Forms.GroupBox();
            this.cbShowPopup = new System.Windows.Forms.CheckBox();
            this.chkBalloonTipOpenLink = new System.Windows.Forms.CheckBox();
            this.cbShowUploadDuration = new System.Windows.Forms.CheckBox();
            this.gbDropBox = new System.Windows.Forms.GroupBox();
            this.cbCloseDropBox = new System.Windows.Forms.CheckBox();
            this.gbAppearance = new System.Windows.Forms.GroupBox();
            this.chkTwitterEnable = new System.Windows.Forms.CheckBox();
            this.cbCompleteSound = new System.Windows.Forms.CheckBox();
            this.chkCaptureFallback = new System.Windows.Forms.CheckBox();
            this.lblTrayFlash = new System.Windows.Forms.Label();
            this.nudFlashIconCount = new System.Windows.Forms.NumericUpDown();
            this.tpProxy = new System.Windows.Forms.TabPage();
            this.gpProxySettings = new System.Windows.Forms.GroupBox();
            this.cboProxyConfig = new System.Windows.Forms.ComboBox();
            this.ucProxyAccounts = new UploadersLib.AccountsControl();
            this.tpHistoryOptions = new System.Windows.Forms.TabPage();
            this.btnClearHistory = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.cbHistorySave = new System.Windows.Forms.CheckBox();
            this.nudHistoryMaxItems = new System.Windows.Forms.NumericUpDown();
            this.tpBackupRestore = new System.Windows.Forms.TabPage();
            this.gbBackupRestoreOutputs = new System.Windows.Forms.GroupBox();
            this.btnOutputsConfigExport = new System.Windows.Forms.Button();
            this.btnOutputsConfigImport = new System.Windows.Forms.Button();
            this.gbBackupRestoreFTP = new System.Windows.Forms.GroupBox();
            this.btnFTPImport = new System.Windows.Forms.Button();
            this.btnFTPExport = new System.Windows.Forms.Button();
            this.gbSettingsExportImport = new System.Windows.Forms.GroupBox();
            this.btnSettingsDefault = new System.Windows.Forms.Button();
            this.btnSettingsExport = new System.Windows.Forms.Button();
            this.btnSettingsImport = new System.Windows.Forms.Button();
            this.tpAdvanced = new System.Windows.Forms.TabPage();
            this.tcAdvanced = new System.Windows.Forms.TabControl();
            this.tpAdvancedSettings = new System.Windows.Forms.TabPage();
            this.pgAppConfig = new System.Windows.Forms.PropertyGrid();
            this.tpAdvancedWorkflow = new System.Windows.Forms.TabPage();
            this.pgWorkflow = new System.Windows.Forms.PropertyGrid();
            this.tpAdvancedCore = new System.Windows.Forms.TabPage();
            this.pgAppSettings = new System.Windows.Forms.PropertyGrid();
            this.tpAdvancedDebug = new System.Windows.Forms.TabPage();
            this.rtbDebugLog = new System.Windows.Forms.RichTextBox();
            this.tpAdvancedStats = new System.Windows.Forms.TabPage();
            this.btnOpenZScreenTester = new System.Windows.Forms.Button();
            this.gbStatistics = new System.Windows.Forms.GroupBox();
            this.btnDebugStart = new System.Windows.Forms.Button();
            this.rtbStats = new System.Windows.Forms.RichTextBox();
            this.gbLastSource = new System.Windows.Forms.GroupBox();
            this.btnOpenSourceString = new System.Windows.Forms.Button();
            this.btnOpenSourceText = new System.Windows.Forms.Button();
            this.btnOpenSourceBrowser = new System.Windows.Forms.Button();
            this.tpQueue = new System.Windows.Forms.TabPage();
            this.lvUploads = new HelpersLib.MyListView();
            this.chFilename = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chStatus = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chProgress = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chSpeed = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chElapsed = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chRemaining = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chUploaderType = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chHost = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chURL = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
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
            this.tpUploadText = new System.Windows.Forms.TabPage();
            this.txtTextUploaderContent = new System.Windows.Forms.TextBox();
            this.btnUploadText = new System.Windows.Forms.Button();
            this.btnUploadTextClipboard = new System.Windows.Forms.Button();
            this.btnUploadTextClipboardFile = new System.Windows.Forms.Button();
            this.ttZScreen = new System.Windows.Forms.ToolTip(this.components);
            this.cmTray.SuspendLayout();
            this.tcMain.SuspendLayout();
            this.tpMain.SuspendLayout();
            this.tsLinks.SuspendLayout();
            this.tsMainTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbLogo)).BeginInit();
            this.gbImageSettings.SuspendLayout();
            this.tpHotkeys.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvHotkeys)).BeginInit();
            this.tpMainInput.SuspendLayout();
            this.tcCapture.SuspendLayout();
            this.tpActivewindow.SuspendLayout();
            this.tpSelectedWindow.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudSelectedWindowHueRange)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudSelectedWindowRegionStep)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudSelectedWindowRegionInterval)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudSelectedWindowBorderSize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbSelectedWindowBorderColor)).BeginInit();
            this.tpCropShot.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.gbCropShotMagnifyingGlass.SuspendLayout();
            this.gbCropDynamicRegionBorderColorSettings.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudCropRegionStep)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudCropHueRange)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudCropRegionInterval)).BeginInit();
            this.gbCropRegion.SuspendLayout();
            this.gbCropRegionSettings.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbCropBorderColor)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudCropBorderSize)).BeginInit();
            this.gbCropCrosshairSettings.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbCropCrosshairColor)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudCrosshairLineCount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudCropCrosshairInterval)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudCrosshairLineSize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudCropCrosshairStep)).BeginInit();
            this.gbCropGridMode.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudCropGridHeight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudCropGridWidth)).BeginInit();
            this.tpCropShotLast.SuspendLayout();
            this.tpCaptureShape.SuspendLayout();
            this.tpFreehandCropShot.SuspendLayout();
            this.tpCaptureClipboard.SuspendLayout();
            this.gbMonitorClipboard.SuspendLayout();
            this.tpMainActions.SuspendLayout();
            this.tpOptions.SuspendLayout();
            this.tcOptions.SuspendLayout();
            this.tpOptionsGeneral.SuspendLayout();
            this.gbUpdates.SuspendLayout();
            this.gbMisc.SuspendLayout();
            this.gbWindowButtons.SuspendLayout();
            this.tpCaptureQuality.SuspendLayout();
            this.gbImageSize.SuspendLayout();
            this.gbPictureQuality.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudSwitchAfter)).BeginInit();
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
            this.tpPaths.SuspendLayout();
            this.gbRoot.SuspendLayout();
            this.gbImages.SuspendLayout();
            this.gbLogs.SuspendLayout();
            this.tpFileNaming.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudMaxNameLength)).BeginInit();
            this.gbOthersNaming.SuspendLayout();
            this.gbCodeTitle.SuspendLayout();
            this.gbActiveWindowNaming.SuspendLayout();
            this.tpTreeGUI.SuspendLayout();
            this.tpInteraction.SuspendLayout();
            this.btnOptionsBalloonTip.SuspendLayout();
            this.gbDropBox.SuspendLayout();
            this.gbAppearance.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudFlashIconCount)).BeginInit();
            this.tpProxy.SuspendLayout();
            this.gpProxySettings.SuspendLayout();
            this.tpHistoryOptions.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudHistoryMaxItems)).BeginInit();
            this.tpBackupRestore.SuspendLayout();
            this.gbBackupRestoreOutputs.SuspendLayout();
            this.gbBackupRestoreFTP.SuspendLayout();
            this.gbSettingsExportImport.SuspendLayout();
            this.tpAdvanced.SuspendLayout();
            this.tcAdvanced.SuspendLayout();
            this.tpAdvancedSettings.SuspendLayout();
            this.tpAdvancedWorkflow.SuspendLayout();
            this.tpAdvancedCore.SuspendLayout();
            this.tpAdvancedDebug.SuspendLayout();
            this.tpAdvancedStats.SuspendLayout();
            this.gbStatistics.SuspendLayout();
            this.gbLastSource.SuspendLayout();
            this.tpQueue.SuspendLayout();
            this.tpDestImageBam.SuspendLayout();
            this.gbImageBamGalleries.SuspendLayout();
            this.gbImageBamLinks.SuspendLayout();
            this.gbImageBamApiKeys.SuspendLayout();
            this.SuspendLayout();
            // 
            // niTray
            // 
            this.niTray.ContextMenuStrip = this.cmTray;
            this.niTray.Icon = ((System.Drawing.Icon)(resources.GetObject("niTray.Icon")));
            this.niTray.Text = "ZScreen";
            this.niTray.Visible = true;
            this.niTray.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.niTray_MouseDoubleClick);
            // 
            // cmTray
            // 
            this.cmTray.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmEntireScreen,
            this.tsmSelectedWindow,
            this.tsmCropShot,
            this.tsmLastCropShot,
            this.tsmCaptureShape,
            this.autoScreenshotsToolStripMenuItem,
            this.toolStripSeparator1,
            this.tsmEditinImageSoftware,
            this.toolStripSeparator6,
            this.tsmFileUpload,
            this.tsmClipboardUpload,
            this.tsmDragDropWindow,
            this.tsmLanguageTranslator,
            this.tsmScreenColorPicker,
            this.toolStripSeparator4,
            this.tsmiTabs,
            this.historyToolStripMenuItem,
            this.tsmViewLocalDirectory,
            this.tsmFTPClient,
            this.tsmHelp,
            this.toolStripSeparator3,
            this.tsmExitZScreen});
            this.cmTray.Name = "cmTray";
            this.cmTray.Size = new System.Drawing.Size(206, 424);
            // 
            // tsmEntireScreen
            // 
            this.tsmEntireScreen.Image = global::ZScreenGUI.Properties.Resources.monitor;
            this.tsmEntireScreen.Name = "tsmEntireScreen";
            this.tsmEntireScreen.Size = new System.Drawing.Size(205, 22);
            this.tsmEntireScreen.Text = "Entire Screen";
            this.tsmEntireScreen.Click += new System.EventHandler(this.entireScreenToolStripMenuItem_Click);
            // 
            // tsmSelectedWindow
            // 
            this.tsmSelectedWindow.Image = global::ZScreenGUI.Properties.Resources.application_double;
            this.tsmSelectedWindow.Name = "tsmSelectedWindow";
            this.tsmSelectedWindow.Size = new System.Drawing.Size(205, 22);
            this.tsmSelectedWindow.Text = "Selected Window...";
            this.tsmSelectedWindow.Click += new System.EventHandler(this.selectedWindowToolStripMenuItem_Click);
            // 
            // tsmCropShot
            // 
            this.tsmCropShot.Image = global::ZScreenGUI.Properties.Resources.shape_square;
            this.tsmCropShot.Name = "tsmCropShot";
            this.tsmCropShot.Size = new System.Drawing.Size(205, 22);
            this.tsmCropShot.Text = "Crop Shot...";
            this.tsmCropShot.Click += new System.EventHandler(this.rectangularRegionToolStripMenuItem_Click);
            // 
            // tsmLastCropShot
            // 
            this.tsmLastCropShot.Image = global::ZScreenGUI.Properties.Resources.shape_square_go;
            this.tsmLastCropShot.Name = "tsmLastCropShot";
            this.tsmLastCropShot.Size = new System.Drawing.Size(205, 22);
            this.tsmLastCropShot.Text = "Last Crop Shot";
            this.tsmLastCropShot.Click += new System.EventHandler(this.lastRectangularRegionToolStripMenuItem_Click);
            // 
            // tsmCaptureShape
            // 
            this.tsmCaptureShape.Image = global::ZScreenGUI.Properties.Resources.shape_square_edit;
            this.tsmCaptureShape.Name = "tsmCaptureShape";
            this.tsmCaptureShape.Size = new System.Drawing.Size(205, 22);
            this.tsmCaptureShape.Text = "Capture Shape";
            this.tsmCaptureShape.Click += new System.EventHandler(this.tsmFreehandCropShot_Click);
            // 
            // autoScreenshotsToolStripMenuItem
            // 
            this.autoScreenshotsToolStripMenuItem.Image = global::ZScreenGUI.Properties.Resources.images_stack;
            this.autoScreenshotsToolStripMenuItem.Name = "autoScreenshotsToolStripMenuItem";
            this.autoScreenshotsToolStripMenuItem.Size = new System.Drawing.Size(205, 22);
            this.autoScreenshotsToolStripMenuItem.Text = "Auto Capture...";
            this.autoScreenshotsToolStripMenuItem.Click += new System.EventHandler(this.autoScreenshotsToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(202, 6);
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
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(202, 6);
            // 
            // tsmFileUpload
            // 
            this.tsmFileUpload.Image = global::ZScreenGUI.Properties.Resources.drive_network;
            this.tsmFileUpload.Name = "tsmFileUpload";
            this.tsmFileUpload.Size = new System.Drawing.Size(205, 22);
            this.tsmFileUpload.Text = "File Upload...";
            this.tsmFileUpload.Click += new System.EventHandler(this.tsmFileUpload_Click);
            // 
            // tsmClipboardUpload
            // 
            this.tsmClipboardUpload.Image = global::ZScreenGUI.Properties.Resources.images;
            this.tsmClipboardUpload.Name = "tsmClipboardUpload";
            this.tsmClipboardUpload.Size = new System.Drawing.Size(205, 22);
            this.tsmClipboardUpload.Text = "Clipboard Upload...";
            this.tsmClipboardUpload.Click += new System.EventHandler(this.tsmUploadFromClipboard_Click);
            // 
            // tsmDragDropWindow
            // 
            this.tsmDragDropWindow.Image = global::ZScreenGUI.Properties.Resources.shape_move_backwards;
            this.tsmDragDropWindow.Name = "tsmDragDropWindow";
            this.tsmDragDropWindow.Size = new System.Drawing.Size(205, 22);
            this.tsmDragDropWindow.Text = "Drag && Drop Window...";
            this.tsmDragDropWindow.Click += new System.EventHandler(this.tsmDropWindow_Click);
            // 
            // tsmLanguageTranslator
            // 
            this.tsmLanguageTranslator.Image = global::ZScreenGUI.Properties.Resources.comments;
            this.tsmLanguageTranslator.Name = "tsmLanguageTranslator";
            this.tsmLanguageTranslator.Size = new System.Drawing.Size(205, 22);
            this.tsmLanguageTranslator.Text = "Language Translator";
            this.tsmLanguageTranslator.Click += new System.EventHandler(this.languageTranslatorToolStripMenuItem_Click);
            // 
            // tsmScreenColorPicker
            // 
            this.tsmScreenColorPicker.Image = global::ZScreenGUI.Properties.Resources.color_wheel;
            this.tsmScreenColorPicker.Name = "tsmScreenColorPicker";
            this.tsmScreenColorPicker.Size = new System.Drawing.Size(205, 22);
            this.tsmScreenColorPicker.Text = "Screen Color Picker...";
            this.tsmScreenColorPicker.Click += new System.EventHandler(this.screenColorPickerToolStripMenuItem_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(202, 6);
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
            // historyToolStripMenuItem
            // 
            this.historyToolStripMenuItem.Image = global::ZScreenGUI.Properties.Resources.pictures;
            this.historyToolStripMenuItem.Name = "historyToolStripMenuItem";
            this.historyToolStripMenuItem.Size = new System.Drawing.Size(205, 22);
            this.historyToolStripMenuItem.Text = "&History...";
            this.historyToolStripMenuItem.Click += new System.EventHandler(this.historyToolStripMenuItem_Click);
            // 
            // tsmViewLocalDirectory
            // 
            this.tsmViewLocalDirectory.Image = global::ZScreenGUI.Properties.Resources.folder_picture;
            this.tsmViewLocalDirectory.Name = "tsmViewLocalDirectory";
            this.tsmViewLocalDirectory.Size = new System.Drawing.Size(205, 22);
            this.tsmViewLocalDirectory.Text = "Images Directory...";
            this.tsmViewLocalDirectory.Click += new System.EventHandler(this.tsmViewDirectory_Click);
            // 
            // tsmFTPClient
            // 
            this.tsmFTPClient.Image = global::ZScreenGUI.Properties.Resources.server_edit;
            this.tsmFTPClient.Name = "tsmFTPClient";
            this.tsmFTPClient.Size = new System.Drawing.Size(205, 22);
            this.tsmFTPClient.Text = "FTP &Client...";
            this.tsmFTPClient.Click += new System.EventHandler(this.tsmFTPClient_Click);
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
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(202, 6);
            // 
            // tsmExitZScreen
            // 
            this.tsmExitZScreen.Image = global::ZScreenGUI.Properties.Resources.cross;
            this.tsmExitZScreen.Name = "tsmExitZScreen";
            this.tsmExitZScreen.Size = new System.Drawing.Size(205, 22);
            this.tsmExitZScreen.Text = "Exit ZScreen";
            this.tsmExitZScreen.Click += new System.EventHandler(this.tsmExitZScreen_Click);
            // 
            // tsmActions
            // 
            this.tsmActions.Name = "tsmActions";
            this.tsmActions.Size = new System.Drawing.Size(32, 19);
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
            // tcMain
            // 
            this.tcMain.Controls.Add(this.tpMain);
            this.tcMain.Controls.Add(this.tpHotkeys);
            this.tcMain.Controls.Add(this.tpMainInput);
            this.tcMain.Controls.Add(this.tpMainActions);
            this.tcMain.Controls.Add(this.tpOptions);
            this.tcMain.Controls.Add(this.tpAdvanced);
            this.tcMain.Controls.Add(this.tpQueue);
            this.tcMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcMain.Location = new System.Drawing.Point(2, 2);
            this.tcMain.Multiline = true;
            this.tcMain.Name = "tcMain";
            this.tcMain.SelectedIndex = 0;
            this.tcMain.Size = new System.Drawing.Size(821, 466);
            this.tcMain.TabIndex = 74;
            this.tcMain.Selecting += new System.Windows.Forms.TabControlCancelEventHandler(this.tcMain_Selecting);
            // 
            // tpMain
            // 
            this.tpMain.AllowDrop = true;
            this.tpMain.BackColor = System.Drawing.Color.White;
            this.tpMain.Controls.Add(this.tsLinks);
            this.tpMain.Controls.Add(this.tsMainTab);
            this.tpMain.Controls.Add(this.pbLogo);
            this.tpMain.Controls.Add(this.lblFileSystemNote);
            this.tpMain.Controls.Add(this.gbImageSettings);
            this.tpMain.Controls.Add(this.ucDestOptions);
            this.tpMain.ImageKey = "(none)";
            this.tpMain.Location = new System.Drawing.Point(4, 22);
            this.tpMain.Name = "tpMain";
            this.tpMain.Padding = new System.Windows.Forms.Padding(3);
            this.tpMain.Size = new System.Drawing.Size(813, 440);
            this.tpMain.TabIndex = 0;
            this.tpMain.Text = "Main";
            this.tpMain.DragDrop += new System.Windows.Forms.DragEventHandler(this.tpMain_DragDrop);
            this.tpMain.DragEnter += new System.Windows.Forms.DragEventHandler(this.tpMain_DragEnter);
            this.tpMain.MouseClick += new System.Windows.Forms.MouseEventHandler(this.tpMain_MouseClick);
            this.tpMain.MouseLeave += new System.EventHandler(this.tpMain_MouseLeave);
            // 
            // tsLinks
            // 
            this.tsLinks.AutoSize = false;
            this.tsLinks.BackColor = System.Drawing.Color.White;
            this.tsLinks.Dock = System.Windows.Forms.DockStyle.None;
            this.tsLinks.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.tsLinks.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbLinkHome,
            this.toolStripButton2,
            this.toolStripButton3});
            this.tsLinks.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.VerticalStackWithOverflow;
            this.tsLinks.Location = new System.Drawing.Point(384, 312);
            this.tsLinks.Name = "tsLinks";
            this.tsLinks.Size = new System.Drawing.Size(256, 90);
            this.tsLinks.TabIndex = 127;
            this.tsLinks.Text = "toolStrip1";
            // 
            // tsbLinkHome
            // 
            this.tsbLinkHome.Image = global::ZScreenGUI.Properties.Resources.world_go;
            this.tsbLinkHome.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.tsbLinkHome.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbLinkHome.Name = "tsbLinkHome";
            this.tsbLinkHome.Size = new System.Drawing.Size(254, 20);
            this.tsbLinkHome.Text = "Home Page";
            this.tsbLinkHome.Click += new System.EventHandler(this.tsbLinkHome_Click);
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.Image = global::ZScreenGUI.Properties.Resources.bug;
            this.toolStripButton2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new System.Drawing.Size(254, 20);
            this.toolStripButton2.Text = "Bugs/Suggestions?";
            this.toolStripButton2.Click += new System.EventHandler(this.tsbLinkIssues_Click);
            // 
            // toolStripButton3
            // 
            this.toolStripButton3.Image = global::ZScreenGUI.Properties.Resources.help;
            this.toolStripButton3.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.toolStripButton3.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton3.Name = "toolStripButton3";
            this.toolStripButton3.Size = new System.Drawing.Size(254, 20);
            this.toolStripButton3.Text = "Tutorials";
            this.toolStripButton3.Click += new System.EventHandler(this.tsbLinkHelp_Click);
            // 
            // tsMainTab
            // 
            this.tsMainTab.BackColor = System.Drawing.Color.White;
            this.tsMainTab.CanOverflow = false;
            this.tsMainTab.Dock = System.Windows.Forms.DockStyle.Right;
            this.tsMainTab.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.tsMainTab.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbFullscreenCapture,
            this.tsbActiveWindow,
            this.tsbSelectedWindow,
            this.tsbCropShot,
            this.tsbLastCropShot,
            this.tsbFreehandCropShot,
            this.tsbAutoCapture,
            this.tssMaintoolbar1,
            this.tsbFileUpload,
            this.tsbClipboardUpload,
            this.tsbDragDropWindow,
            this.tsbLanguageTranslator,
            this.tsbScreenColorPicker,
            this.toolStripSeparator8,
            this.tsbOpenHistory,
            this.tsbImageDirectory,
            this.tsbAbout,
            this.tsbDonate});
            this.tsMainTab.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.VerticalStackWithOverflow;
            this.tsMainTab.Location = new System.Drawing.Point(652, 3);
            this.tsMainTab.Name = "tsMainTab";
            this.tsMainTab.Size = new System.Drawing.Size(158, 413);
            this.tsMainTab.TabIndex = 126;
            // 
            // tsbFullscreenCapture
            // 
            this.tsbFullscreenCapture.Image = global::ZScreenGUI.Properties.Resources.monitor;
            this.tsbFullscreenCapture.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.tsbFullscreenCapture.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbFullscreenCapture.Name = "tsbFullscreenCapture";
            this.tsbFullscreenCapture.Size = new System.Drawing.Size(155, 20);
            this.tsbFullscreenCapture.Text = "Capture Fullscreen";
            this.tsbFullscreenCapture.Click += new System.EventHandler(this.tsbFullscreenCapture_Click);
            // 
            // tsbActiveWindow
            // 
            this.tsbActiveWindow.Image = global::ZScreenGUI.Properties.Resources.application_go;
            this.tsbActiveWindow.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.tsbActiveWindow.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbActiveWindow.Name = "tsbActiveWindow";
            this.tsbActiveWindow.Size = new System.Drawing.Size(155, 20);
            this.tsbActiveWindow.Text = "Active Window (3 sec)";
            this.tsbActiveWindow.ToolTipText = "Active Window will capture after 3 seconds";
            this.tsbActiveWindow.Click += new System.EventHandler(this.tsbActiveWindow_Click);
            // 
            // tsbSelectedWindow
            // 
            this.tsbSelectedWindow.Image = global::ZScreenGUI.Properties.Resources.application_double;
            this.tsbSelectedWindow.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.tsbSelectedWindow.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbSelectedWindow.Name = "tsbSelectedWindow";
            this.tsbSelectedWindow.Size = new System.Drawing.Size(155, 20);
            this.tsbSelectedWindow.Text = "Capture Window...";
            this.tsbSelectedWindow.Click += new System.EventHandler(this.tsbSelectedWindow_Click);
            // 
            // tsbCropShot
            // 
            this.tsbCropShot.Image = global::ZScreenGUI.Properties.Resources.shape_square;
            this.tsbCropShot.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.tsbCropShot.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbCropShot.Name = "tsbCropShot";
            this.tsbCropShot.Size = new System.Drawing.Size(155, 20);
            this.tsbCropShot.Text = "Capture Rectangle...";
            this.tsbCropShot.Click += new System.EventHandler(this.tsbCropShot_Click);
            // 
            // tsbLastCropShot
            // 
            this.tsbLastCropShot.Image = global::ZScreenGUI.Properties.Resources.shape_square_go;
            this.tsbLastCropShot.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.tsbLastCropShot.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbLastCropShot.Name = "tsbLastCropShot";
            this.tsbLastCropShot.Size = new System.Drawing.Size(155, 20);
            this.tsbLastCropShot.Text = "Capture Last Rectangle...";
            this.tsbLastCropShot.Click += new System.EventHandler(this.tsbLastCropShot_Click);
            // 
            // tsbFreehandCropShot
            // 
            this.tsbFreehandCropShot.Image = global::ZScreenGUI.Properties.Resources.shape_square_edit;
            this.tsbFreehandCropShot.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.tsbFreehandCropShot.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbFreehandCropShot.Name = "tsbFreehandCropShot";
            this.tsbFreehandCropShot.Size = new System.Drawing.Size(155, 20);
            this.tsbFreehandCropShot.Text = "Capture Shape...";
            this.tsbFreehandCropShot.Click += new System.EventHandler(this.tsbFreehandCropShot_Click);
            // 
            // tsbAutoCapture
            // 
            this.tsbAutoCapture.Image = global::ZScreenGUI.Properties.Resources.images_stack;
            this.tsbAutoCapture.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.tsbAutoCapture.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbAutoCapture.Name = "tsbAutoCapture";
            this.tsbAutoCapture.Size = new System.Drawing.Size(155, 20);
            this.tsbAutoCapture.Text = "Auto Capture...";
            this.tsbAutoCapture.Click += new System.EventHandler(this.tsbAutoCapture_Click);
            // 
            // tssMaintoolbar1
            // 
            this.tssMaintoolbar1.Name = "tssMaintoolbar1";
            this.tssMaintoolbar1.Size = new System.Drawing.Size(155, 6);
            // 
            // tsbFileUpload
            // 
            this.tsbFileUpload.Image = global::ZScreenGUI.Properties.Resources.drive_network;
            this.tsbFileUpload.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.tsbFileUpload.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbFileUpload.Name = "tsbFileUpload";
            this.tsbFileUpload.Size = new System.Drawing.Size(155, 20);
            this.tsbFileUpload.Text = "File Upload...";
            this.tsbFileUpload.Click += new System.EventHandler(this.tsbFileUpload_Click);
            // 
            // tsbClipboardUpload
            // 
            this.tsbClipboardUpload.Image = global::ZScreenGUI.Properties.Resources.images;
            this.tsbClipboardUpload.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.tsbClipboardUpload.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbClipboardUpload.Name = "tsbClipboardUpload";
            this.tsbClipboardUpload.Size = new System.Drawing.Size(155, 20);
            this.tsbClipboardUpload.Text = "Clipboard Upload...";
            this.tsbClipboardUpload.Click += new System.EventHandler(this.tsbClipboardUpload_Click);
            // 
            // tsbDragDropWindow
            // 
            this.tsbDragDropWindow.Image = global::ZScreenGUI.Properties.Resources.shape_move_backwards;
            this.tsbDragDropWindow.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.tsbDragDropWindow.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbDragDropWindow.Name = "tsbDragDropWindow";
            this.tsbDragDropWindow.Size = new System.Drawing.Size(155, 20);
            this.tsbDragDropWindow.Text = "Drag && Drop Window...";
            this.tsbDragDropWindow.Click += new System.EventHandler(this.tsbDragDropWindow_Click);
            // 
            // tsbLanguageTranslator
            // 
            this.tsbLanguageTranslator.Image = global::ZScreenGUI.Properties.Resources.comments;
            this.tsbLanguageTranslator.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.tsbLanguageTranslator.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbLanguageTranslator.Name = "tsbLanguageTranslator";
            this.tsbLanguageTranslator.Size = new System.Drawing.Size(155, 20);
            this.tsbLanguageTranslator.Text = "Google Translate...";
            this.tsbLanguageTranslator.Visible = false;
            this.tsbLanguageTranslator.Click += new System.EventHandler(this.tsbLanguageTranslate_Click);
            // 
            // tsbScreenColorPicker
            // 
            this.tsbScreenColorPicker.Image = global::ZScreenGUI.Properties.Resources.color_wheel;
            this.tsbScreenColorPicker.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.tsbScreenColorPicker.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbScreenColorPicker.Name = "tsbScreenColorPicker";
            this.tsbScreenColorPicker.Size = new System.Drawing.Size(155, 20);
            this.tsbScreenColorPicker.Text = "Screen Color Picker...";
            this.tsbScreenColorPicker.Click += new System.EventHandler(this.tsbScreenColorPicker_Click);
            // 
            // toolStripSeparator8
            // 
            this.toolStripSeparator8.Name = "toolStripSeparator8";
            this.toolStripSeparator8.Size = new System.Drawing.Size(155, 6);
            // 
            // tsbOpenHistory
            // 
            this.tsbOpenHistory.Image = global::ZScreenGUI.Properties.Resources.pictures;
            this.tsbOpenHistory.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.tsbOpenHistory.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbOpenHistory.Name = "tsbOpenHistory";
            this.tsbOpenHistory.Size = new System.Drawing.Size(155, 20);
            this.tsbOpenHistory.Text = "History...";
            this.tsbOpenHistory.Click += new System.EventHandler(this.tsbOpenHistory_Click);
            // 
            // tsbImageDirectory
            // 
            this.tsbImageDirectory.Image = global::ZScreenGUI.Properties.Resources.folder_picture;
            this.tsbImageDirectory.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.tsbImageDirectory.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbImageDirectory.Name = "tsbImageDirectory";
            this.tsbImageDirectory.Size = new System.Drawing.Size(155, 20);
            this.tsbImageDirectory.Text = "Images Directory...";
            this.tsbImageDirectory.Click += new System.EventHandler(this.tsbImageDirectory_Click);
            // 
            // tsbAbout
            // 
            this.tsbAbout.Image = global::ZScreenGUI.Properties.Resources.information;
            this.tsbAbout.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.tsbAbout.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbAbout.Name = "tsbAbout";
            this.tsbAbout.Size = new System.Drawing.Size(155, 20);
            this.tsbAbout.Text = "About...";
            this.tsbAbout.Click += new System.EventHandler(this.tsbAbout_Click);
            // 
            // tsbDonate
            // 
            this.tsbDonate.AutoSize = false;
            this.tsbDonate.BackgroundImage = global::ZScreenGUI.Properties.Resources.paypal;
            this.tsbDonate.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.tsbDonate.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.None;
            this.tsbDonate.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbDonate.ImageTransparentColor = System.Drawing.Color.Transparent;
            this.tsbDonate.Margin = new System.Windows.Forms.Padding(0, 8, 0, 0);
            this.tsbDonate.Name = "tsbDonate";
            this.tsbDonate.Size = new System.Drawing.Size(100, 35);
            this.tsbDonate.Text = "Donate";
            this.tsbDonate.ToolTipText = "Thanks!";
            this.tsbDonate.Click += new System.EventHandler(this.tsbDonate_Click);
            this.tsbDonate.MouseEnter += new System.EventHandler(this.tsbDonate_MouseEnter);
            this.tsbDonate.MouseLeave += new System.EventHandler(this.tsbDonate_MouseLeave);
            // 
            // pbLogo
            // 
            this.pbLogo.Cursor = System.Windows.Forms.Cursors.Default;
            this.pbLogo.Image = global::ZScreenGUI.Properties.Resources.ZScreen_256;
            this.pbLogo.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.pbLogo.Location = new System.Drawing.Point(382, 8);
            this.pbLogo.Name = "pbLogo";
            this.pbLogo.Size = new System.Drawing.Size(256, 256);
            this.pbLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pbLogo.TabIndex = 72;
            this.pbLogo.TabStop = false;
            // 
            // lblFileSystemNote
            // 
            this.lblFileSystemNote.BackColor = System.Drawing.Color.WhiteSmoke;
            this.lblFileSystemNote.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblFileSystemNote.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblFileSystemNote.Location = new System.Drawing.Point(3, 416);
            this.lblFileSystemNote.Name = "lblFileSystemNote";
            this.lblFileSystemNote.Size = new System.Drawing.Size(807, 21);
            this.lblFileSystemNote.TabIndex = 117;
            this.lblFileSystemNote.Text = "You can also Drag n Drop files or a directory on to anywhere in this page";
            this.lblFileSystemNote.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // gbImageSettings
            // 
            this.gbImageSettings.Controls.Add(this.chkShowUploadResults);
            this.gbImageSettings.Controls.Add(this.chkShortenURL);
            this.gbImageSettings.Controls.Add(this.chkPerformActions);
            this.gbImageSettings.Controls.Add(this.lblScreenshotDelay);
            this.gbImageSettings.Controls.Add(this.nudScreenshotDelay);
            this.gbImageSettings.Controls.Add(this.chkShowCursor);
            this.gbImageSettings.Controls.Add(this.chkManualNaming);
            this.gbImageSettings.Location = new System.Drawing.Point(16, 224);
            this.gbImageSettings.Name = "gbImageSettings";
            this.gbImageSettings.Size = new System.Drawing.Size(352, 178);
            this.gbImageSettings.TabIndex = 123;
            this.gbImageSettings.TabStop = false;
            this.gbImageSettings.Text = "Quick Settings";
            // 
            // chkShowUploadResults
            // 
            this.chkShowUploadResults.AutoSize = true;
            this.chkShowUploadResults.Location = new System.Drawing.Point(16, 152);
            this.chkShowUploadResults.Name = "chkShowUploadResults";
            this.chkShowUploadResults.Size = new System.Drawing.Size(245, 17);
            this.chkShowUploadResults.TabIndex = 123;
            this.chkShowUploadResults.Text = "Show Upload Results window after completion";
            this.chkShowUploadResults.UseVisualStyleBackColor = true;
            this.chkShowUploadResults.CheckedChanged += new System.EventHandler(this.chkShowUploadResults_CheckedChanged);
            // 
            // chkShortenURL
            // 
            this.chkShortenURL.AutoSize = true;
            this.chkShortenURL.Location = new System.Drawing.Point(16, 128);
            this.chkShortenURL.Name = "chkShortenURL";
            this.chkShortenURL.Size = new System.Drawing.Size(190, 17);
            this.chkShortenURL.TabIndex = 11;
            this.chkShortenURL.Text = "Shorten URL if the URL is too long";
            this.chkShortenURL.UseVisualStyleBackColor = true;
            this.chkShortenURL.CheckedChanged += new System.EventHandler(this.chkShortenURL_CheckedChanged);
            // 
            // chkPerformActions
            // 
            this.chkPerformActions.AutoSize = true;
            this.chkPerformActions.Checked = true;
            this.chkPerformActions.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkPerformActions.Location = new System.Drawing.Point(16, 80);
            this.chkPerformActions.Name = "chkPerformActions";
            this.chkPerformActions.Size = new System.Drawing.Size(231, 17);
            this.chkPerformActions.TabIndex = 68;
            this.chkPerformActions.Text = "Perform &Actions before reaching destination";
            this.chkPerformActions.UseVisualStyleBackColor = true;
            this.chkPerformActions.CheckedChanged += new System.EventHandler(this.ChkEditorsEnableCheckedChanged);
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
            this.nudScreenshotDelay.Location = new System.Drawing.Point(120, 18);
            this.nudScreenshotDelay.Margin = new System.Windows.Forms.Padding(4);
            this.nudScreenshotDelay.Name = "nudScreenshotDelay";
            this.nudScreenshotDelay.RealValue = ((long)(0));
            this.nudScreenshotDelay.Size = new System.Drawing.Size(208, 24);
            this.nudScreenshotDelay.TabIndex = 121;
            this.nudScreenshotDelay.Tag = "Test";
            this.nudScreenshotDelay.Time = ZScreenLib.Times.Milliseconds;
            this.ttZScreen.SetToolTip(this.nudScreenshotDelay, "Specify the amount of time to wait before taking a screenshot.");
            this.nudScreenshotDelay.Value = ((long)(0));
            this.nudScreenshotDelay.ValueChanged += new System.EventHandler(this.numericUpDownTimer1_ValueChanged);
            this.nudScreenshotDelay.SelectedIndexChanged += new System.EventHandler(this.nudtScreenshotDelay_SelectedIndexChanged);
            this.nudScreenshotDelay.MouseHover += new System.EventHandler(this.nudtScreenshotDelay_MouseHover);
            // 
            // chkShowCursor
            // 
            this.chkShowCursor.AutoSize = true;
            this.chkShowCursor.Location = new System.Drawing.Point(16, 56);
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
            this.chkManualNaming.Location = new System.Drawing.Point(16, 104);
            this.chkManualNaming.Name = "chkManualNaming";
            this.chkManualNaming.Size = new System.Drawing.Size(206, 17);
            this.chkManualNaming.TabIndex = 112;
            this.chkManualNaming.Text = "Prompt for File Name and Destinations";
            this.ttZScreen.SetToolTip(this.chkManualNaming, "When enabled a prompt will be displayed when each\r\nscreenshot is taken allowing y" +
        "ou to manually specify a filename.");
            this.chkManualNaming.UseVisualStyleBackColor = true;
            this.chkManualNaming.CheckedChanged += new System.EventHandler(this.chkManualNaming_CheckedChanged);
            // 
            // ucDestOptions
            // 
            this.ucDestOptions.Location = new System.Drawing.Point(16, 16);
            this.ucDestOptions.Margin = new System.Windows.Forms.Padding(4);
            this.ucDestOptions.Name = "ucDestOptions";
            this.ucDestOptions.Size = new System.Drawing.Size(352, 200);
            this.ucDestOptions.TabIndex = 124;
            this.ttZScreen.SetToolTip(this.ucDestOptions, "To configure destination options go to Destinations tab");
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
            this.tpHotkeys.Size = new System.Drawing.Size(813, 440);
            this.tpHotkeys.TabIndex = 1;
            this.tpHotkeys.Text = "Hotkeys";
            this.tpHotkeys.UseVisualStyleBackColor = true;
            // 
            // btnResetHotkeys
            // 
            this.btnResetHotkeys.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnResetHotkeys.AutoSize = true;
            this.btnResetHotkeys.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnResetHotkeys.Location = new System.Drawing.Point(696, 380);
            this.btnResetHotkeys.Name = "btnResetHotkeys";
            this.btnResetHotkeys.Size = new System.Drawing.Size(101, 23);
            this.btnResetHotkeys.TabIndex = 69;
            this.btnResetHotkeys.Text = "Reset &All Hotkeys";
            this.btnResetHotkeys.UseVisualStyleBackColor = true;
            this.btnResetHotkeys.Click += new System.EventHandler(this.btnResetHotkeys_Click);
            // 
            // lblHotkeyStatus
            // 
            this.lblHotkeyStatus.BackColor = System.Drawing.Color.WhiteSmoke;
            this.lblHotkeyStatus.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblHotkeyStatus.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblHotkeyStatus.Location = new System.Drawing.Point(3, 414);
            this.lblHotkeyStatus.Name = "lblHotkeyStatus";
            this.lblHotkeyStatus.Size = new System.Drawing.Size(807, 23);
            this.lblHotkeyStatus.TabIndex = 68;
            this.lblHotkeyStatus.Text = "Click on a Hotkey to set";
            this.lblHotkeyStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
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
            this.dgvHotkeys.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvHotkeys.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.chHotkeys_Description,
            this.chHotkeys_Keys,
            this.DefaultKeys});
            this.dgvHotkeys.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvHotkeys.Location = new System.Drawing.Point(3, 3);
            this.dgvHotkeys.MultiSelect = false;
            this.dgvHotkeys.Name = "dgvHotkeys";
            this.dgvHotkeys.ReadOnly = true;
            this.dgvHotkeys.RowHeadersVisible = false;
            this.dgvHotkeys.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgvHotkeys.RowTemplate.Height = 24;
            this.dgvHotkeys.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dgvHotkeys.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgvHotkeys.Size = new System.Drawing.Size(807, 434);
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
            this.chHotkeys_Keys.DefaultCellStyle = dataGridViewCellStyle1;
            this.chHotkeys_Keys.HeaderText = "Hotkey";
            this.chHotkeys_Keys.Name = "chHotkeys_Keys";
            this.chHotkeys_Keys.ReadOnly = true;
            this.chHotkeys_Keys.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // DefaultKeys
            // 
            this.DefaultKeys.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.DefaultKeys.HeaderText = "Default Hotkey";
            this.DefaultKeys.Name = "DefaultKeys";
            this.DefaultKeys.ReadOnly = true;
            this.DefaultKeys.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.DefaultKeys.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // tpMainInput
            // 
            this.tpMainInput.Controls.Add(this.tcCapture);
            this.tpMainInput.ImageKey = "(none)";
            this.tpMainInput.Location = new System.Drawing.Point(4, 22);
            this.tpMainInput.Name = "tpMainInput";
            this.tpMainInput.Padding = new System.Windows.Forms.Padding(3);
            this.tpMainInput.Size = new System.Drawing.Size(813, 440);
            this.tpMainInput.TabIndex = 4;
            this.tpMainInput.Text = "Capture";
            this.tpMainInput.UseVisualStyleBackColor = true;
            // 
            // tcCapture
            // 
            this.tcCapture.Controls.Add(this.tpActivewindow);
            this.tcCapture.Controls.Add(this.tpSelectedWindow);
            this.tcCapture.Controls.Add(this.tpCropShot);
            this.tcCapture.Controls.Add(this.tpCropShotLast);
            this.tcCapture.Controls.Add(this.tpCaptureShape);
            this.tcCapture.Controls.Add(this.tpFreehandCropShot);
            this.tcCapture.Controls.Add(this.tpCaptureClipboard);
            this.tcCapture.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcCapture.Location = new System.Drawing.Point(3, 3);
            this.tcCapture.Name = "tcCapture";
            this.tcCapture.SelectedIndex = 0;
            this.tcCapture.Size = new System.Drawing.Size(807, 434);
            this.tcCapture.TabIndex = 77;
            // 
            // tpActivewindow
            // 
            this.tpActivewindow.Controls.Add(this.chkActiveWindowTryCaptureChildren);
            this.tpActivewindow.Controls.Add(this.chkSelectedWindowCleanTransparentCorners);
            this.tpActivewindow.Controls.Add(this.chkSelectedWindowShowCheckers);
            this.tpActivewindow.Controls.Add(this.chkSelectedWindowIncludeShadow);
            this.tpActivewindow.Controls.Add(this.chkActiveWindowPreferDWM);
            this.tpActivewindow.Controls.Add(this.chkSelectedWindowCleanBackground);
            this.tpActivewindow.ImageKey = "application.png";
            this.tpActivewindow.Location = new System.Drawing.Point(4, 22);
            this.tpActivewindow.Name = "tpActivewindow";
            this.tpActivewindow.Padding = new System.Windows.Forms.Padding(3);
            this.tpActivewindow.Size = new System.Drawing.Size(799, 408);
            this.tpActivewindow.TabIndex = 12;
            this.tpActivewindow.Text = "Active Window";
            this.tpActivewindow.UseVisualStyleBackColor = true;
            // 
            // chkActiveWindowTryCaptureChildren
            // 
            this.chkActiveWindowTryCaptureChildren.AutoSize = true;
            this.chkActiveWindowTryCaptureChildren.Location = new System.Drawing.Point(16, 136);
            this.chkActiveWindowTryCaptureChildren.Name = "chkActiveWindowTryCaptureChildren";
            this.chkActiveWindowTryCaptureChildren.Size = new System.Drawing.Size(235, 17);
            this.chkActiveWindowTryCaptureChildren.TabIndex = 48;
            this.chkActiveWindowTryCaptureChildren.Text = "Capture Child Windows, Tooltips and Menus";
            this.ttZScreen.SetToolTip(this.chkActiveWindowTryCaptureChildren, "Only works when DWM is disabled");
            this.chkActiveWindowTryCaptureChildren.UseVisualStyleBackColor = true;
            this.chkActiveWindowTryCaptureChildren.CheckedChanged += new System.EventHandler(this.chkActiveWindowTryCaptureChilds_CheckedChanged);
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
            this.tpSelectedWindow.Location = new System.Drawing.Point(4, 22);
            this.tpSelectedWindow.Name = "tpSelectedWindow";
            this.tpSelectedWindow.Size = new System.Drawing.Size(799, 408);
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
            // tpCropShot
            // 
            this.tpCropShot.Controls.Add(this.groupBox1);
            this.tpCropShot.Controls.Add(this.gbCropShotMagnifyingGlass);
            this.tpCropShot.Controls.Add(this.gbCropDynamicRegionBorderColorSettings);
            this.tpCropShot.Controls.Add(this.gbCropRegion);
            this.tpCropShot.Controls.Add(this.gbCropRegionSettings);
            this.tpCropShot.Controls.Add(this.gbCropCrosshairSettings);
            this.tpCropShot.Controls.Add(this.gbCropGridMode);
            this.tpCropShot.ImageKey = "shape_square.png";
            this.tpCropShot.Location = new System.Drawing.Point(4, 22);
            this.tpCropShot.Name = "tpCropShot";
            this.tpCropShot.Padding = new System.Windows.Forms.Padding(3);
            this.tpCropShot.Size = new System.Drawing.Size(799, 408);
            this.tpCropShot.TabIndex = 7;
            this.tpCropShot.Text = "Crop Shot";
            this.tpCropShot.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cboCropEngine);
            this.groupBox1.Location = new System.Drawing.Point(8, 8);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(352, 56);
            this.groupBox1.TabIndex = 125;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Crop Engine of choice";
            // 
            // cboCropEngine
            // 
            this.cboCropEngine.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboCropEngine.FormattingEnabled = true;
            this.cboCropEngine.Location = new System.Drawing.Point(8, 24);
            this.cboCropEngine.Name = "cboCropEngine";
            this.cboCropEngine.Size = new System.Drawing.Size(336, 21);
            this.cboCropEngine.TabIndex = 0;
            this.cboCropEngine.SelectedIndexChanged += new System.EventHandler(this.cboCropEngine_SelectedIndexChanged);
            // 
            // gbCropShotMagnifyingGlass
            // 
            this.gbCropShotMagnifyingGlass.Controls.Add(this.chkCropShowMagnifyingGlass);
            this.gbCropShotMagnifyingGlass.Location = new System.Drawing.Point(368, 280);
            this.gbCropShotMagnifyingGlass.Name = "gbCropShotMagnifyingGlass";
            this.gbCropShotMagnifyingGlass.Size = new System.Drawing.Size(392, 56);
            this.gbCropShotMagnifyingGlass.TabIndex = 124;
            this.gbCropShotMagnifyingGlass.TabStop = false;
            this.gbCropShotMagnifyingGlass.Text = "Ease of Access";
            // 
            // chkCropShowMagnifyingGlass
            // 
            this.chkCropShowMagnifyingGlass.AutoSize = true;
            this.chkCropShowMagnifyingGlass.Location = new System.Drawing.Point(16, 24);
            this.chkCropShowMagnifyingGlass.Name = "chkCropShowMagnifyingGlass";
            this.chkCropShowMagnifyingGlass.Size = new System.Drawing.Size(133, 17);
            this.chkCropShowMagnifyingGlass.TabIndex = 26;
            this.chkCropShowMagnifyingGlass.Text = "Show magnifying glass";
            this.chkCropShowMagnifyingGlass.UseVisualStyleBackColor = true;
            this.chkCropShowMagnifyingGlass.CheckedChanged += new System.EventHandler(this.cbCropShowMagnifyingGlass_CheckedChanged);
            // 
            // gbCropDynamicRegionBorderColorSettings
            // 
            this.gbCropDynamicRegionBorderColorSettings.Controls.Add(this.nudCropRegionStep);
            this.gbCropDynamicRegionBorderColorSettings.Controls.Add(this.nudCropHueRange);
            this.gbCropDynamicRegionBorderColorSettings.Controls.Add(this.cbCropDynamicBorderColor);
            this.gbCropDynamicRegionBorderColorSettings.Controls.Add(this.lblCropRegionInterval);
            this.gbCropDynamicRegionBorderColorSettings.Controls.Add(this.lblCropHueRange);
            this.gbCropDynamicRegionBorderColorSettings.Controls.Add(this.lblCropRegionStep);
            this.gbCropDynamicRegionBorderColorSettings.Controls.Add(this.nudCropRegionInterval);
            this.gbCropDynamicRegionBorderColorSettings.Location = new System.Drawing.Point(368, 192);
            this.gbCropDynamicRegionBorderColorSettings.Name = "gbCropDynamicRegionBorderColorSettings";
            this.gbCropDynamicRegionBorderColorSettings.Size = new System.Drawing.Size(392, 80);
            this.gbCropDynamicRegionBorderColorSettings.TabIndex = 123;
            this.gbCropDynamicRegionBorderColorSettings.TabStop = false;
            this.gbCropDynamicRegionBorderColorSettings.Text = "Dynamic Region Border Color Settings";
            // 
            // nudCropRegionStep
            // 
            this.nudCropRegionStep.Location = new System.Drawing.Point(320, 20);
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
            this.nudCropHueRange.Location = new System.Drawing.Point(320, 52);
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
            this.lblCropRegionInterval.Location = new System.Drawing.Point(176, 24);
            this.lblCropRegionInterval.Name = "lblCropRegionInterval";
            this.lblCropRegionInterval.Size = new System.Drawing.Size(45, 13);
            this.lblCropRegionInterval.TabIndex = 28;
            this.lblCropRegionInterval.Text = "Interval:";
            // 
            // lblCropHueRange
            // 
            this.lblCropHueRange.AutoSize = true;
            this.lblCropHueRange.Location = new System.Drawing.Point(256, 56);
            this.lblCropHueRange.Name = "lblCropHueRange";
            this.lblCropHueRange.Size = new System.Drawing.Size(60, 13);
            this.lblCropHueRange.TabIndex = 32;
            this.lblCropHueRange.Text = "Hue range:";
            // 
            // lblCropRegionStep
            // 
            this.lblCropRegionStep.AutoSize = true;
            this.lblCropRegionStep.Location = new System.Drawing.Point(286, 24);
            this.lblCropRegionStep.Name = "lblCropRegionStep";
            this.lblCropRegionStep.Size = new System.Drawing.Size(32, 13);
            this.lblCropRegionStep.TabIndex = 29;
            this.lblCropRegionStep.Text = "Step:";
            // 
            // nudCropRegionInterval
            // 
            this.nudCropRegionInterval.Location = new System.Drawing.Point(224, 20);
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
            // gbCropRegion
            // 
            this.gbCropRegion.Controls.Add(this.lblCropRegionStyle);
            this.gbCropRegion.Controls.Add(this.chkRegionHotkeyInfo);
            this.gbCropRegion.Controls.Add(this.chkCropStyle);
            this.gbCropRegion.Controls.Add(this.chkRegionRectangleInfo);
            this.gbCropRegion.Location = new System.Drawing.Point(8, 72);
            this.gbCropRegion.Name = "gbCropRegion";
            this.gbCropRegion.Size = new System.Drawing.Size(352, 120);
            this.gbCropRegion.TabIndex = 121;
            this.gbCropRegion.TabStop = false;
            this.gbCropRegion.Text = "Crop Region Settings";
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
            this.gbCropRegionSettings.Location = new System.Drawing.Point(368, 96);
            this.gbCropRegionSettings.Name = "gbCropRegionSettings";
            this.gbCropRegionSettings.Size = new System.Drawing.Size(392, 88);
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
            // gbCropCrosshairSettings
            // 
            this.gbCropCrosshairSettings.Controls.Add(this.chkCropDynamicCrosshair);
            this.gbCropCrosshairSettings.Controls.Add(this.lblCropCrosshairStep);
            this.gbCropCrosshairSettings.Controls.Add(this.chkCropShowBigCross);
            this.gbCropCrosshairSettings.Controls.Add(this.pbCropCrosshairColor);
            this.gbCropCrosshairSettings.Controls.Add(this.lblCropCrosshairInterval);
            this.gbCropCrosshairSettings.Controls.Add(this.lblCropCrosshairColor);
            this.gbCropCrosshairSettings.Controls.Add(this.nudCrosshairLineCount);
            this.gbCropCrosshairSettings.Controls.Add(this.nudCropCrosshairInterval);
            this.gbCropCrosshairSettings.Controls.Add(this.nudCrosshairLineSize);
            this.gbCropCrosshairSettings.Controls.Add(this.nudCropCrosshairStep);
            this.gbCropCrosshairSettings.Controls.Add(this.lblCrosshairLineSize);
            this.gbCropCrosshairSettings.Controls.Add(this.lblCrosshairLineCount);
            this.gbCropCrosshairSettings.Location = new System.Drawing.Point(8, 200);
            this.gbCropCrosshairSettings.Name = "gbCropCrosshairSettings";
            this.gbCropCrosshairSettings.Size = new System.Drawing.Size(352, 144);
            this.gbCropCrosshairSettings.TabIndex = 25;
            this.gbCropCrosshairSettings.TabStop = false;
            this.gbCropCrosshairSettings.Text = "Crosshair Settings";
            // 
            // chkCropDynamicCrosshair
            // 
            this.chkCropDynamicCrosshair.AutoSize = true;
            this.chkCropDynamicCrosshair.Location = new System.Drawing.Point(16, 48);
            this.chkCropDynamicCrosshair.Name = "chkCropDynamicCrosshair";
            this.chkCropDynamicCrosshair.Size = new System.Drawing.Size(118, 17);
            this.chkCropDynamicCrosshair.TabIndex = 16;
            this.chkCropDynamicCrosshair.Text = "Animated cross-hair";
            this.chkCropDynamicCrosshair.UseVisualStyleBackColor = true;
            this.chkCropDynamicCrosshair.CheckedChanged += new System.EventHandler(this.cbCropDynamicCrosshair_CheckedChanged);
            // 
            // lblCropCrosshairStep
            // 
            this.lblCropCrosshairStep.AutoSize = true;
            this.lblCropCrosshairStep.Location = new System.Drawing.Point(248, 49);
            this.lblCropCrosshairStep.Name = "lblCropCrosshairStep";
            this.lblCropCrosshairStep.Size = new System.Drawing.Size(32, 13);
            this.lblCropCrosshairStep.TabIndex = 22;
            this.lblCropCrosshairStep.Text = "Step:";
            // 
            // chkCropShowBigCross
            // 
            this.chkCropShowBigCross.AutoSize = true;
            this.chkCropShowBigCross.Location = new System.Drawing.Point(16, 24);
            this.chkCropShowBigCross.Name = "chkCropShowBigCross";
            this.chkCropShowBigCross.Size = new System.Drawing.Size(212, 17);
            this.chkCropShowBigCross.TabIndex = 25;
            this.chkCropShowBigCross.Text = "Extend the cross-hair across the screen";
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
            // lblCropCrosshairInterval
            // 
            this.lblCropCrosshairInterval.AutoSize = true;
            this.lblCropCrosshairInterval.Location = new System.Drawing.Point(136, 49);
            this.lblCropCrosshairInterval.Name = "lblCropCrosshairInterval";
            this.lblCropCrosshairInterval.Size = new System.Drawing.Size(45, 13);
            this.lblCropCrosshairInterval.TabIndex = 21;
            this.lblCropCrosshairInterval.Text = "Interval:";
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
            this.nudCrosshairLineCount.Location = new System.Drawing.Point(165, 78);
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
            // nudCropCrosshairInterval
            // 
            this.nudCropCrosshairInterval.Location = new System.Drawing.Point(184, 47);
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
            // nudCrosshairLineSize
            // 
            this.nudCrosshairLineSize.Location = new System.Drawing.Point(96, 109);
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
            // nudCropCrosshairStep
            // 
            this.nudCropCrosshairStep.Location = new System.Drawing.Point(280, 46);
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
            // lblCrosshairLineSize
            // 
            this.lblCrosshairLineSize.AutoSize = true;
            this.lblCrosshairLineSize.Location = new System.Drawing.Point(16, 110);
            this.lblCrosshairLineSize.Name = "lblCrosshairLineSize";
            this.lblCrosshairLineSize.Size = new System.Drawing.Size(77, 13);
            this.lblCrosshairLineSize.TabIndex = 20;
            this.lblCrosshairLineSize.Text = "Cross-hair size:";
            // 
            // lblCrosshairLineCount
            // 
            this.lblCrosshairLineCount.AutoSize = true;
            this.lblCrosshairLineCount.Location = new System.Drawing.Point(16, 80);
            this.lblCrosshairLineCount.Name = "lblCrosshairLineCount";
            this.lblCrosshairLineCount.Size = new System.Drawing.Size(145, 13);
            this.lblCrosshairLineCount.TabIndex = 19;
            this.lblCrosshairLineCount.Text = "Number of concentric circles:";
            // 
            // gbCropGridMode
            // 
            this.gbCropGridMode.Controls.Add(this.cboCropGridMode);
            this.gbCropGridMode.Controls.Add(this.nudCropGridHeight);
            this.gbCropGridMode.Controls.Add(this.lblGridSizeWidth);
            this.gbCropGridMode.Controls.Add(this.lblGridSize);
            this.gbCropGridMode.Controls.Add(this.lblGridSizeHeight);
            this.gbCropGridMode.Controls.Add(this.nudCropGridWidth);
            this.gbCropGridMode.Location = new System.Drawing.Point(368, 8);
            this.gbCropGridMode.Name = "gbCropGridMode";
            this.gbCropGridMode.Size = new System.Drawing.Size(392, 80);
            this.gbCropGridMode.TabIndex = 120;
            this.gbCropGridMode.TabStop = false;
            this.gbCropGridMode.Tag = "With Grid Mode you can take screenshots of preset portions of the Screen";
            this.gbCropGridMode.Text = "Grid Mode Settings";
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
            this.nudCropGridHeight.Location = new System.Drawing.Point(320, 48);
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
            this.lblGridSizeWidth.Location = new System.Drawing.Point(176, 52);
            this.lblGridSizeWidth.Name = "lblGridSizeWidth";
            this.lblGridSizeWidth.Size = new System.Drawing.Size(35, 13);
            this.lblGridSizeWidth.TabIndex = 14;
            this.lblGridSizeWidth.Text = "Width";
            // 
            // lblGridSize
            // 
            this.lblGridSize.AutoSize = true;
            this.lblGridSize.Location = new System.Drawing.Point(48, 52);
            this.lblGridSize.Name = "lblGridSize";
            this.lblGridSize.Size = new System.Drawing.Size(117, 13);
            this.lblGridSize.TabIndex = 118;
            this.lblGridSize.Text = "Grid Size ( 0 = Disable )";
            // 
            // lblGridSizeHeight
            // 
            this.lblGridSizeHeight.AutoSize = true;
            this.lblGridSizeHeight.Location = new System.Drawing.Point(280, 52);
            this.lblGridSizeHeight.Name = "lblGridSizeHeight";
            this.lblGridSizeHeight.Size = new System.Drawing.Size(38, 13);
            this.lblGridSizeHeight.TabIndex = 16;
            this.lblGridSizeHeight.Text = "Height";
            // 
            // nudCropGridWidth
            // 
            this.nudCropGridWidth.Location = new System.Drawing.Point(216, 48);
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
            // tpCropShotLast
            // 
            this.tpCropShotLast.Controls.Add(this.btnLastCropShotReset);
            this.tpCropShotLast.Location = new System.Drawing.Point(4, 22);
            this.tpCropShotLast.Name = "tpCropShotLast";
            this.tpCropShotLast.Padding = new System.Windows.Forms.Padding(3);
            this.tpCropShotLast.Size = new System.Drawing.Size(799, 408);
            this.tpCropShotLast.TabIndex = 14;
            this.tpCropShotLast.Text = "Last Crop Shot";
            this.tpCropShotLast.UseVisualStyleBackColor = true;
            // 
            // btnLastCropShotReset
            // 
            this.btnLastCropShotReset.AutoSize = true;
            this.btnLastCropShotReset.Location = new System.Drawing.Point(16, 16);
            this.btnLastCropShotReset.Name = "btnLastCropShotReset";
            this.btnLastCropShotReset.Size = new System.Drawing.Size(165, 23);
            this.btnLastCropShotReset.TabIndex = 0;
            this.btnLastCropShotReset.Text = "Reset Last Crop Shot rectangle";
            this.btnLastCropShotReset.UseVisualStyleBackColor = true;
            this.btnLastCropShotReset.Click += new System.EventHandler(this.btnLastCropShotReset_Click);
            // 
            // tpCaptureShape
            // 
            this.tpCaptureShape.Controls.Add(this.pgSurfaceConfig);
            this.tpCaptureShape.Location = new System.Drawing.Point(4, 22);
            this.tpCaptureShape.Name = "tpCaptureShape";
            this.tpCaptureShape.Padding = new System.Windows.Forms.Padding(3);
            this.tpCaptureShape.Size = new System.Drawing.Size(799, 408);
            this.tpCaptureShape.TabIndex = 15;
            this.tpCaptureShape.Text = "Shape";
            this.tpCaptureShape.UseVisualStyleBackColor = true;
            // 
            // pgSurfaceConfig
            // 
            this.pgSurfaceConfig.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pgSurfaceConfig.Location = new System.Drawing.Point(3, 3);
            this.pgSurfaceConfig.Name = "pgSurfaceConfig";
            this.pgSurfaceConfig.Size = new System.Drawing.Size(793, 402);
            this.pgSurfaceConfig.TabIndex = 1;
            // 
            // tpFreehandCropShot
            // 
            this.tpFreehandCropShot.Controls.Add(this.cbFreehandCropShowRectangleBorder);
            this.tpFreehandCropShot.Controls.Add(this.cbFreehandCropAutoClose);
            this.tpFreehandCropShot.Controls.Add(this.cbFreehandCropAutoUpload);
            this.tpFreehandCropShot.Controls.Add(this.cbFreehandCropShowHelpText);
            this.tpFreehandCropShot.ImageKey = "shape_square_edit.png";
            this.tpFreehandCropShot.Location = new System.Drawing.Point(4, 22);
            this.tpFreehandCropShot.Name = "tpFreehandCropShot";
            this.tpFreehandCropShot.Size = new System.Drawing.Size(799, 408);
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
            // tpCaptureClipboard
            // 
            this.tpCaptureClipboard.Controls.Add(this.gbMonitorClipboard);
            this.tpCaptureClipboard.Location = new System.Drawing.Point(4, 22);
            this.tpCaptureClipboard.Name = "tpCaptureClipboard";
            this.tpCaptureClipboard.Padding = new System.Windows.Forms.Padding(3);
            this.tpCaptureClipboard.Size = new System.Drawing.Size(799, 408);
            this.tpCaptureClipboard.TabIndex = 8;
            this.tpCaptureClipboard.Text = "Clipboard";
            this.tpCaptureClipboard.UseVisualStyleBackColor = true;
            // 
            // gbMonitorClipboard
            // 
            this.gbMonitorClipboard.Controls.Add(this.chkMonUrls);
            this.gbMonitorClipboard.Controls.Add(this.chkMonFiles);
            this.gbMonitorClipboard.Controls.Add(this.chkMonImages);
            this.gbMonitorClipboard.Controls.Add(this.chkMonText);
            this.gbMonitorClipboard.Location = new System.Drawing.Point(8, 8);
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
            // tpMainActions
            // 
            this.tpMainActions.Controls.Add(this.lblNoteActions);
            this.tpMainActions.Controls.Add(this.lbSoftware);
            this.tpMainActions.Controls.Add(this.pgEditorsImage);
            this.tpMainActions.Controls.Add(this.btnActionsRemove);
            this.tpMainActions.Controls.Add(this.btnAddImageSoftware);
            this.tpMainActions.ImageKey = "(none)";
            this.tpMainActions.Location = new System.Drawing.Point(4, 22);
            this.tpMainActions.Name = "tpMainActions";
            this.tpMainActions.Padding = new System.Windows.Forms.Padding(3);
            this.tpMainActions.Size = new System.Drawing.Size(813, 440);
            this.tpMainActions.TabIndex = 2;
            this.tpMainActions.Text = "Actions";
            this.tpMainActions.UseVisualStyleBackColor = true;
            // 
            // lblNoteActions
            // 
            this.lblNoteActions.BackColor = System.Drawing.Color.WhiteSmoke;
            this.lblNoteActions.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblNoteActions.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblNoteActions.Location = new System.Drawing.Point(296, 416);
            this.lblNoteActions.Name = "lblNoteActions";
            this.lblNoteActions.Size = new System.Drawing.Size(514, 21);
            this.lblNoteActions.TabIndex = 118;
            this.lblNoteActions.Text = "UPDATE ME";
            this.lblNoteActions.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbSoftware
            // 
            this.lbSoftware.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbSoftware.FormattingEnabled = true;
            this.lbSoftware.IntegralHeight = false;
            this.lbSoftware.Location = new System.Drawing.Point(3, 3);
            this.lbSoftware.Name = "lbSoftware";
            this.lbSoftware.Size = new System.Drawing.Size(293, 434);
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
            this.pgEditorsImage.Size = new System.Drawing.Size(489, 233);
            this.pgEditorsImage.TabIndex = 64;
            this.pgEditorsImage.ToolbarVisible = false;
            this.pgEditorsImage.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.pgEditorsImage_PropertyValueChanged);
            // 
            // btnActionsRemove
            // 
            this.btnActionsRemove.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnActionsRemove.Location = new System.Drawing.Point(408, 13);
            this.btnActionsRemove.Name = "btnActionsRemove";
            this.btnActionsRemove.Size = new System.Drawing.Size(88, 24);
            this.btnActionsRemove.TabIndex = 58;
            this.btnActionsRemove.Text = "&Remove";
            this.btnActionsRemove.UseVisualStyleBackColor = true;
            this.btnActionsRemove.Click += new System.EventHandler(this.btnDeleteImageSoftware_Click);
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
            // tpOptions
            // 
            this.tpOptions.Controls.Add(this.tcOptions);
            this.tpOptions.ImageKey = "(none)";
            this.tpOptions.Location = new System.Drawing.Point(4, 22);
            this.tpOptions.Name = "tpOptions";
            this.tpOptions.Padding = new System.Windows.Forms.Padding(3);
            this.tpOptions.Size = new System.Drawing.Size(813, 440);
            this.tpOptions.TabIndex = 9;
            this.tpOptions.Text = "Options";
            this.tpOptions.UseVisualStyleBackColor = true;
            // 
            // tcOptions
            // 
            this.tcOptions.Controls.Add(this.tpOptionsGeneral);
            this.tcOptions.Controls.Add(this.tpCaptureQuality);
            this.tcOptions.Controls.Add(this.tpWatermark);
            this.tcOptions.Controls.Add(this.tpPaths);
            this.tcOptions.Controls.Add(this.tpFileNaming);
            this.tcOptions.Controls.Add(this.tpTreeGUI);
            this.tcOptions.Controls.Add(this.tpInteraction);
            this.tcOptions.Controls.Add(this.tpProxy);
            this.tcOptions.Controls.Add(this.tpHistoryOptions);
            this.tcOptions.Controls.Add(this.tpBackupRestore);
            this.tcOptions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcOptions.Location = new System.Drawing.Point(3, 3);
            this.tcOptions.Name = "tcOptions";
            this.tcOptions.SelectedIndex = 0;
            this.tcOptions.Size = new System.Drawing.Size(807, 434);
            this.tcOptions.TabIndex = 8;
            // 
            // tpOptionsGeneral
            // 
            this.tpOptionsGeneral.Controls.Add(this.gbUpdates);
            this.tpOptionsGeneral.Controls.Add(this.gbMisc);
            this.tpOptionsGeneral.Controls.Add(this.gbWindowButtons);
            this.tpOptionsGeneral.Location = new System.Drawing.Point(4, 22);
            this.tpOptionsGeneral.Name = "tpOptionsGeneral";
            this.tpOptionsGeneral.Padding = new System.Windows.Forms.Padding(3);
            this.tpOptionsGeneral.Size = new System.Drawing.Size(799, 408);
            this.tpOptionsGeneral.TabIndex = 0;
            this.tpOptionsGeneral.Text = "General";
            this.tpOptionsGeneral.UseVisualStyleBackColor = true;
            // 
            // gbUpdates
            // 
            this.gbUpdates.Controls.Add(this.cboReleaseChannel);
            this.gbUpdates.Controls.Add(this.lblUpdateInfo);
            this.gbUpdates.Controls.Add(this.btnCheckUpdate);
            this.gbUpdates.Controls.Add(this.chkCheckUpdates);
            this.gbUpdates.Location = new System.Drawing.Point(8, 208);
            this.gbUpdates.Name = "gbUpdates";
            this.gbUpdates.Size = new System.Drawing.Size(760, 128);
            this.gbUpdates.TabIndex = 8;
            this.gbUpdates.TabStop = false;
            this.gbUpdates.Text = "Check Updates";
            // 
            // cboReleaseChannel
            // 
            this.cboReleaseChannel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboReleaseChannel.FormattingEnabled = true;
            this.cboReleaseChannel.Location = new System.Drawing.Point(240, 24);
            this.cboReleaseChannel.Name = "cboReleaseChannel";
            this.cboReleaseChannel.Size = new System.Drawing.Size(121, 21);
            this.cboReleaseChannel.TabIndex = 8;
            this.cboReleaseChannel.SelectedIndexChanged += new System.EventHandler(this.cboReleaseChannel_SelectedIndexChanged);
            // 
            // lblUpdateInfo
            // 
            this.lblUpdateInfo.AutoSize = true;
            this.lblUpdateInfo.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblUpdateInfo.Location = new System.Drawing.Point(24, 56);
            this.lblUpdateInfo.Name = "lblUpdateInfo";
            this.lblUpdateInfo.Size = new System.Drawing.Size(116, 16);
            this.lblUpdateInfo.TabIndex = 6;
            this.lblUpdateInfo.Text = "Update information";
            // 
            // btnCheckUpdate
            // 
            this.btnCheckUpdate.Location = new System.Drawing.Point(368, 24);
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
            this.chkCheckUpdates.Size = new System.Drawing.Size(224, 17);
            this.chkCheckUpdates.TabIndex = 1;
            this.chkCheckUpdates.Text = "Automatically check updates at startup for";
            this.chkCheckUpdates.UseVisualStyleBackColor = true;
            this.chkCheckUpdates.CheckedChanged += new System.EventHandler(this.cbCheckUpdates_CheckedChanged);
            // 
            // gbMisc
            // 
            this.gbMisc.BackColor = System.Drawing.Color.Transparent;
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
            // chkShellExt
            // 
            this.chkShellExt.AutoSize = true;
            this.chkShellExt.Location = new System.Drawing.Point(424, 72);
            this.chkShellExt.Name = "chkShellExt";
            this.chkShellExt.Size = new System.Drawing.Size(278, 17);
            this.chkShellExt.TabIndex = 9;
            this.chkShellExt.Text = "Show \"Upload using ZScreen\" in Shell Context Menu";
            this.ttZScreen.SetToolTip(this.chkShellExt, "Use ZUploader for context menu uploads.\r\nZScreen is not recommended for this.");
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
            this.cbAutoSaveSettings.Location = new System.Drawing.Point(16, 96);
            this.cbAutoSaveSettings.Name = "cbAutoSaveSettings";
            this.cbAutoSaveSettings.Size = new System.Drawing.Size(245, 17);
            this.cbAutoSaveSettings.TabIndex = 7;
            this.cbAutoSaveSettings.Text = "Save settings when ZScreen minimizing to tray";
            this.ttZScreen.SetToolTip(this.cbAutoSaveSettings, resources.GetString("cbAutoSaveSettings.ToolTip"));
            this.cbAutoSaveSettings.UseVisualStyleBackColor = true;
            this.cbAutoSaveSettings.CheckedChanged += new System.EventHandler(this.cbAutoSaveSettings_CheckedChanged);
            // 
            // cbShowHelpBalloonTips
            // 
            this.cbShowHelpBalloonTips.AutoSize = true;
            this.cbShowHelpBalloonTips.Location = new System.Drawing.Point(16, 72);
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
            this.chkShowTaskbar.Location = new System.Drawing.Point(424, 24);
            this.chkShowTaskbar.Name = "chkShowTaskbar";
            this.chkShowTaskbar.Size = new System.Drawing.Size(161, 17);
            this.chkShowTaskbar.TabIndex = 3;
            this.chkShowTaskbar.Text = "Show Application in Taskbar";
            this.chkShowTaskbar.UseVisualStyleBackColor = true;
            this.chkShowTaskbar.CheckedChanged += new System.EventHandler(this.cbShowTaskbar_CheckedChanged);
            // 
            // chkOpenMainWindow
            // 
            this.chkOpenMainWindow.AutoSize = true;
            this.chkOpenMainWindow.Location = new System.Drawing.Point(16, 48);
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
            this.chkStartWin.Location = new System.Drawing.Point(16, 24);
            this.chkStartWin.Name = "chkStartWin";
            this.chkStartWin.Size = new System.Drawing.Size(117, 17);
            this.chkStartWin.TabIndex = 0;
            this.chkStartWin.Text = "Start with Windows";
            this.chkStartWin.UseVisualStyleBackColor = true;
            this.chkStartWin.CheckedChanged += new System.EventHandler(this.cbStartWin_CheckedChanged);
            // 
            // gbWindowButtons
            // 
            this.gbWindowButtons.Controls.Add(this.cboCloseButtonAction);
            this.gbWindowButtons.Controls.Add(this.cboMinimizeButtonAction);
            this.gbWindowButtons.Controls.Add(this.lblCloseButtonAction);
            this.gbWindowButtons.Controls.Add(this.lblMinimizeButtonAction);
            this.gbWindowButtons.Location = new System.Drawing.Point(8, 144);
            this.gbWindowButtons.Name = "gbWindowButtons";
            this.gbWindowButtons.Size = new System.Drawing.Size(760, 56);
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
            // tpCaptureQuality
            // 
            this.tpCaptureQuality.Controls.Add(this.gbImageSize);
            this.tpCaptureQuality.Controls.Add(this.gbPictureQuality);
            this.tpCaptureQuality.Location = new System.Drawing.Point(4, 22);
            this.tpCaptureQuality.Name = "tpCaptureQuality";
            this.tpCaptureQuality.Padding = new System.Windows.Forms.Padding(3);
            this.tpCaptureQuality.Size = new System.Drawing.Size(799, 408);
            this.tpCaptureQuality.TabIndex = 0;
            this.tpCaptureQuality.Text = "Image Settings";
            this.tpCaptureQuality.UseVisualStyleBackColor = true;
            // 
            // gbImageSize
            // 
            this.gbImageSize.Controls.Add(this.lblImageSizeFixedAutoScale);
            this.gbImageSize.Controls.Add(this.rbImageSizeDefault);
            this.gbImageSize.Controls.Add(this.lblImageSizeFixedHeight);
            this.gbImageSize.Controls.Add(this.rbImageSizeFixed);
            this.gbImageSize.Controls.Add(this.lblImageSizeFixedWidth);
            this.gbImageSize.Controls.Add(this.txtImageSizeRatio);
            this.gbImageSize.Controls.Add(this.lblImageSizeRatioPercentage);
            this.gbImageSize.Controls.Add(this.txtImageSizeFixedWidth);
            this.gbImageSize.Controls.Add(this.rbImageSizeRatio);
            this.gbImageSize.Controls.Add(this.txtImageSizeFixedHeight);
            this.gbImageSize.Location = new System.Drawing.Point(8, 208);
            this.gbImageSize.Name = "gbImageSize";
            this.gbImageSize.Size = new System.Drawing.Size(768, 120);
            this.gbImageSize.TabIndex = 124;
            this.gbImageSize.TabStop = false;
            this.gbImageSize.Text = "Image Size";
            // 
            // lblImageSizeFixedAutoScale
            // 
            this.lblImageSizeFixedAutoScale.AutoSize = true;
            this.lblImageSizeFixedAutoScale.Location = new System.Drawing.Point(352, 60);
            this.lblImageSizeFixedAutoScale.Name = "lblImageSizeFixedAutoScale";
            this.lblImageSizeFixedAutoScale.Size = new System.Drawing.Size(152, 13);
            this.lblImageSizeFixedAutoScale.TabIndex = 128;
            this.lblImageSizeFixedAutoScale.Text = "0 height or width for auto scale";
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
            this.gbPictureQuality.Controls.Add(this.cboJpgSubSampling);
            this.gbPictureQuality.Controls.Add(this.cboJpgQuality);
            this.gbPictureQuality.Controls.Add(this.cbGIFQuality);
            this.gbPictureQuality.Controls.Add(this.lblGIFQuality);
            this.gbPictureQuality.Controls.Add(this.nudSwitchAfter);
            this.gbPictureQuality.Controls.Add(this.lblQuality);
            this.gbPictureQuality.Controls.Add(this.cboSwitchFormat);
            this.gbPictureQuality.Controls.Add(this.lblFileFormat);
            this.gbPictureQuality.Controls.Add(this.cboFileFormat);
            this.gbPictureQuality.Controls.Add(this.lblKB);
            this.gbPictureQuality.Controls.Add(this.lblAfter);
            this.gbPictureQuality.Controls.Add(this.lblSwitchTo);
            this.gbPictureQuality.Location = new System.Drawing.Point(8, 8);
            this.gbPictureQuality.Name = "gbPictureQuality";
            this.gbPictureQuality.Size = new System.Drawing.Size(768, 184);
            this.gbPictureQuality.TabIndex = 115;
            this.gbPictureQuality.TabStop = false;
            this.gbPictureQuality.Text = "Picture Quality";
            // 
            // cboJpgSubSampling
            // 
            this.cboJpgSubSampling.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboJpgSubSampling.FormattingEnabled = true;
            this.cboJpgSubSampling.Location = new System.Drawing.Point(336, 96);
            this.cboJpgSubSampling.Name = "cboJpgSubSampling";
            this.cboJpgSubSampling.Size = new System.Drawing.Size(416, 21);
            this.cboJpgSubSampling.TabIndex = 120;
            this.cboJpgSubSampling.SelectedIndexChanged += new System.EventHandler(this.cboJpgSubSampling_SelectedIndexChanged);
            // 
            // cboJpgQuality
            // 
            this.cboJpgQuality.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboJpgQuality.FormattingEnabled = true;
            this.cboJpgQuality.Location = new System.Drawing.Point(16, 96);
            this.cboJpgQuality.Name = "cboJpgQuality";
            this.cboJpgQuality.Size = new System.Drawing.Size(312, 21);
            this.cboJpgQuality.TabIndex = 119;
            this.cboJpgQuality.SelectedIndexChanged += new System.EventHandler(this.cboJpgQuality_SelectedIndexChanged);
            // 
            // cbGIFQuality
            // 
            this.cbGIFQuality.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbGIFQuality.FormattingEnabled = true;
            this.cbGIFQuality.Items.AddRange(new object[] {
            "Grayscale",
            "4 bit (16 colors)",
            "8 bit (256 colors)"});
            this.cbGIFQuality.Location = new System.Drawing.Point(16, 144);
            this.cbGIFQuality.Name = "cbGIFQuality";
            this.cbGIFQuality.Size = new System.Drawing.Size(98, 21);
            this.cbGIFQuality.TabIndex = 118;
            this.cbGIFQuality.SelectedIndexChanged += new System.EventHandler(this.cbGIFQuality_SelectedIndexChanged);
            // 
            // lblGIFQuality
            // 
            this.lblGIFQuality.AutoSize = true;
            this.lblGIFQuality.Location = new System.Drawing.Point(16, 128);
            this.lblGIFQuality.Name = "lblGIFQuality";
            this.lblGIFQuality.Size = new System.Drawing.Size(62, 13);
            this.lblGIFQuality.TabIndex = 117;
            this.lblGIFQuality.Text = "GIF Quality:";
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
            this.nudSwitchAfter.TabIndex = 116;
            this.nudSwitchAfter.Value = new decimal(new int[] {
            350,
            0,
            0,
            0});
            this.nudSwitchAfter.ValueChanged += new System.EventHandler(this.nudSwitchAfter_ValueChanged);
            // 
            // lblQuality
            // 
            this.lblQuality.AutoSize = true;
            this.lblQuality.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblQuality.Location = new System.Drawing.Point(16, 80);
            this.lblQuality.Name = "lblQuality";
            this.lblQuality.Size = new System.Drawing.Size(72, 13);
            this.lblQuality.TabIndex = 108;
            this.lblQuality.Text = "JPEG Quality:";
            // 
            // cboSwitchFormat
            // 
            this.cboSwitchFormat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboSwitchFormat.FormattingEnabled = true;
            this.cboSwitchFormat.Location = new System.Drawing.Point(232, 40);
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
            this.lblKB.Location = new System.Drawing.Point(197, 44);
            this.lblKB.Name = "lblKB";
            this.lblKB.Size = new System.Drawing.Size(23, 13);
            this.lblKB.TabIndex = 95;
            this.lblKB.Text = "KiB";
            // 
            // lblAfter
            // 
            this.lblAfter.AutoSize = true;
            this.lblAfter.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblAfter.Location = new System.Drawing.Point(125, 24);
            this.lblAfter.Name = "lblAfter";
            this.lblAfter.Size = new System.Drawing.Size(88, 13);
            this.lblAfter.TabIndex = 93;
            this.lblAfter.Text = "After: (0 disables)";
            // 
            // lblSwitchTo
            // 
            this.lblSwitchTo.AutoSize = true;
            this.lblSwitchTo.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblSwitchTo.Location = new System.Drawing.Point(235, 23);
            this.lblSwitchTo.Name = "lblSwitchTo";
            this.lblSwitchTo.Size = new System.Drawing.Size(54, 13);
            this.lblSwitchTo.TabIndex = 92;
            this.lblSwitchTo.Text = "Switch to:";
            // 
            // tpWatermark
            // 
            this.tpWatermark.Controls.Add(this.pbWatermarkShow);
            this.tpWatermark.Controls.Add(this.gbWatermarkGeneral);
            this.tpWatermark.Controls.Add(this.tcWatermark);
            this.tpWatermark.ImageKey = "tag_blue_edit.png";
            this.tpWatermark.Location = new System.Drawing.Point(4, 22);
            this.tpWatermark.Name = "tpWatermark";
            this.tpWatermark.Padding = new System.Windows.Forms.Padding(3);
            this.tpWatermark.Size = new System.Drawing.Size(799, 408);
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
            this.tcWatermark.Size = new System.Drawing.Size(486, 391);
            this.tcWatermark.TabIndex = 29;
            // 
            // tpWatermarkText
            // 
            this.tpWatermarkText.Controls.Add(this.gbWatermarkBackground);
            this.tpWatermarkText.Controls.Add(this.gbWatermarkText);
            this.tpWatermarkText.ImageKey = "textfield_rename.png";
            this.tpWatermarkText.Location = new System.Drawing.Point(4, 22);
            this.tpWatermarkText.Name = "tpWatermarkText";
            this.tpWatermarkText.Padding = new System.Windows.Forms.Padding(3);
            this.tpWatermarkText.Size = new System.Drawing.Size(478, 365);
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
            this.tpWatermarkImage.Location = new System.Drawing.Point(4, 22);
            this.tpWatermarkImage.Name = "tpWatermarkImage";
            this.tpWatermarkImage.Padding = new System.Windows.Forms.Padding(3);
            this.tpWatermarkImage.Size = new System.Drawing.Size(478, 365);
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
            // tpPaths
            // 
            this.tpPaths.Controls.Add(this.gbRoot);
            this.tpPaths.Controls.Add(this.gbImages);
            this.tpPaths.Controls.Add(this.gbLogs);
            this.tpPaths.Location = new System.Drawing.Point(4, 22);
            this.tpPaths.Name = "tpPaths";
            this.tpPaths.Size = new System.Drawing.Size(799, 408);
            this.tpPaths.TabIndex = 2;
            this.tpPaths.Text = "Paths";
            this.tpPaths.UseVisualStyleBackColor = true;
            // 
            // gbRoot
            // 
            this.gbRoot.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbRoot.Controls.Add(this.btnViewRootDir);
            this.gbRoot.Controls.Add(this.btnRelocateRootDir);
            this.gbRoot.Controls.Add(this.txtRootFolder);
            this.gbRoot.Location = new System.Drawing.Point(8, 8);
            this.gbRoot.Name = "gbRoot";
            this.gbRoot.Size = new System.Drawing.Size(776, 64);
            this.gbRoot.TabIndex = 117;
            this.gbRoot.TabStop = false;
            this.gbRoot.Text = "Root";
            // 
            // btnViewRootDir
            // 
            this.btnViewRootDir.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnViewRootDir.Location = new System.Drawing.Point(660, 22);
            this.btnViewRootDir.Name = "btnViewRootDir";
            this.btnViewRootDir.Size = new System.Drawing.Size(104, 24);
            this.btnViewRootDir.TabIndex = 116;
            this.btnViewRootDir.Text = "View Directory...";
            this.btnViewRootDir.UseVisualStyleBackColor = true;
            this.btnViewRootDir.Click += new System.EventHandler(this.btnViewRootDir_Click);
            // 
            // btnRelocateRootDir
            // 
            this.btnRelocateRootDir.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRelocateRootDir.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnRelocateRootDir.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnRelocateRootDir.Location = new System.Drawing.Point(571, 22);
            this.btnRelocateRootDir.Name = "btnRelocateRootDir";
            this.btnRelocateRootDir.Size = new System.Drawing.Size(80, 24);
            this.btnRelocateRootDir.TabIndex = 115;
            this.btnRelocateRootDir.Text = "Relocate...";
            this.btnRelocateRootDir.UseVisualStyleBackColor = true;
            this.btnRelocateRootDir.Click += new System.EventHandler(this.btnBrowseRootDir_Click);
            // 
            // txtRootFolder
            // 
            this.txtRootFolder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtRootFolder.Location = new System.Drawing.Point(16, 24);
            this.txtRootFolder.Name = "txtRootFolder";
            this.txtRootFolder.ReadOnly = true;
            this.txtRootFolder.Size = new System.Drawing.Size(539, 20);
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
            this.gbImages.Location = new System.Drawing.Point(8, 80);
            this.gbImages.Name = "gbImages";
            this.gbImages.Size = new System.Drawing.Size(776, 120);
            this.gbImages.TabIndex = 114;
            this.gbImages.TabStop = false;
            this.gbImages.Text = "Images";
            // 
            // btnBrowseImagesDir
            // 
            this.btnBrowseImagesDir.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBrowseImagesDir.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnBrowseImagesDir.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnBrowseImagesDir.Location = new System.Drawing.Point(571, 21);
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
            this.btnMoveImageFiles.Location = new System.Drawing.Point(588, 56);
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
            this.btnViewImagesDir.Location = new System.Drawing.Point(660, 21);
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
            this.txtImagesDir.Size = new System.Drawing.Size(539, 20);
            this.txtImagesDir.TabIndex = 1;
            // 
            // gbLogs
            // 
            this.gbLogs.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbLogs.BackColor = System.Drawing.Color.Transparent;
            this.gbLogs.Controls.Add(this.btnViewCacheDir);
            this.gbLogs.Controls.Add(this.txtLogsDir);
            this.gbLogs.Location = new System.Drawing.Point(8, 208);
            this.gbLogs.Name = "gbLogs";
            this.gbLogs.Size = new System.Drawing.Size(776, 64);
            this.gbLogs.TabIndex = 1;
            this.gbLogs.TabStop = false;
            this.gbLogs.Text = "Logs";
            // 
            // btnViewCacheDir
            // 
            this.btnViewCacheDir.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnViewCacheDir.Location = new System.Drawing.Point(656, 22);
            this.btnViewCacheDir.Name = "btnViewCacheDir";
            this.btnViewCacheDir.Size = new System.Drawing.Size(104, 24);
            this.btnViewCacheDir.TabIndex = 7;
            this.btnViewCacheDir.Text = "View Directory...";
            this.btnViewCacheDir.UseVisualStyleBackColor = true;
            this.btnViewCacheDir.Click += new System.EventHandler(this.btnViewRemoteDirectory_Click);
            // 
            // txtLogsDir
            // 
            this.txtLogsDir.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtLogsDir.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.txtLogsDir.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.FileSystemDirectories;
            this.txtLogsDir.Location = new System.Drawing.Point(16, 24);
            this.txtLogsDir.Name = "txtLogsDir";
            this.txtLogsDir.ReadOnly = true;
            this.txtLogsDir.Size = new System.Drawing.Size(634, 20);
            this.txtLogsDir.TabIndex = 0;
            // 
            // tpFileNaming
            // 
            this.tpFileNaming.Controls.Add(this.chkOverwriteFiles);
            this.tpFileNaming.Controls.Add(this.lblMaxNameLength);
            this.tpFileNaming.Controls.Add(this.nudMaxNameLength);
            this.tpFileNaming.Controls.Add(this.btnResetIncrement);
            this.tpFileNaming.Controls.Add(this.gbOthersNaming);
            this.tpFileNaming.Controls.Add(this.gbCodeTitle);
            this.tpFileNaming.Controls.Add(this.gbActiveWindowNaming);
            this.tpFileNaming.Location = new System.Drawing.Point(4, 22);
            this.tpFileNaming.Name = "tpFileNaming";
            this.tpFileNaming.Size = new System.Drawing.Size(799, 408);
            this.tpFileNaming.TabIndex = 3;
            this.tpFileNaming.Text = "File Naming";
            this.tpFileNaming.UseVisualStyleBackColor = true;
            // 
            // chkOverwriteFiles
            // 
            this.chkOverwriteFiles.AutoSize = true;
            this.chkOverwriteFiles.Location = new System.Drawing.Point(248, 224);
            this.chkOverwriteFiles.Name = "chkOverwriteFiles";
            this.chkOverwriteFiles.Size = new System.Drawing.Size(95, 17);
            this.chkOverwriteFiles.TabIndex = 119;
            this.chkOverwriteFiles.Text = "Overwrite Files";
            this.chkOverwriteFiles.UseVisualStyleBackColor = true;
            this.chkOverwriteFiles.CheckedChanged += new System.EventHandler(this.chkOverwriteFiles_CheckedChanged);
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
            // tpTreeGUI
            // 
            this.tpTreeGUI.Controls.Add(this.pgIndexer);
            this.tpTreeGUI.Location = new System.Drawing.Point(4, 22);
            this.tpTreeGUI.Name = "tpTreeGUI";
            this.tpTreeGUI.Padding = new System.Windows.Forms.Padding(3);
            this.tpTreeGUI.Size = new System.Drawing.Size(799, 408);
            this.tpTreeGUI.TabIndex = 15;
            this.tpTreeGUI.Text = "Directory Indexer";
            this.tpTreeGUI.UseVisualStyleBackColor = true;
            // 
            // pgIndexer
            // 
            this.pgIndexer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pgIndexer.Location = new System.Drawing.Point(3, 3);
            this.pgIndexer.Name = "pgIndexer";
            this.pgIndexer.Size = new System.Drawing.Size(793, 402);
            this.pgIndexer.TabIndex = 0;
            // 
            // tpInteraction
            // 
            this.tpInteraction.Controls.Add(this.btnOptionsBalloonTip);
            this.tpInteraction.Controls.Add(this.gbDropBox);
            this.tpInteraction.Controls.Add(this.gbAppearance);
            this.tpInteraction.Location = new System.Drawing.Point(4, 22);
            this.tpInteraction.Name = "tpInteraction";
            this.tpInteraction.Padding = new System.Windows.Forms.Padding(3);
            this.tpInteraction.Size = new System.Drawing.Size(799, 408);
            this.tpInteraction.TabIndex = 5;
            this.tpInteraction.Text = "Interaction";
            this.tpInteraction.UseVisualStyleBackColor = true;
            // 
            // btnOptionsBalloonTip
            // 
            this.btnOptionsBalloonTip.Controls.Add(this.cbShowPopup);
            this.btnOptionsBalloonTip.Controls.Add(this.chkBalloonTipOpenLink);
            this.btnOptionsBalloonTip.Controls.Add(this.cbShowUploadDuration);
            this.btnOptionsBalloonTip.Location = new System.Drawing.Point(8, 144);
            this.btnOptionsBalloonTip.Name = "btnOptionsBalloonTip";
            this.btnOptionsBalloonTip.Size = new System.Drawing.Size(752, 104);
            this.btnOptionsBalloonTip.TabIndex = 8;
            this.btnOptionsBalloonTip.TabStop = false;
            this.btnOptionsBalloonTip.Text = "Balloon Tip Options";
            // 
            // cbShowPopup
            // 
            this.cbShowPopup.AutoSize = true;
            this.cbShowPopup.Location = new System.Drawing.Point(16, 24);
            this.cbShowPopup.Name = "cbShowPopup";
            this.cbShowPopup.Size = new System.Drawing.Size(250, 17);
            this.cbShowPopup.TabIndex = 5;
            this.cbShowPopup.Text = "Show balloon tip after upload/task is completed";
            this.cbShowPopup.UseVisualStyleBackColor = true;
            this.cbShowPopup.CheckedChanged += new System.EventHandler(this.cbShowPopup_CheckedChanged);
            // 
            // chkBalloonTipOpenLink
            // 
            this.chkBalloonTipOpenLink.AutoSize = true;
            this.chkBalloonTipOpenLink.Location = new System.Drawing.Point(16, 48);
            this.chkBalloonTipOpenLink.Name = "chkBalloonTipOpenLink";
            this.chkBalloonTipOpenLink.Size = new System.Drawing.Size(189, 17);
            this.chkBalloonTipOpenLink.TabIndex = 6;
            this.chkBalloonTipOpenLink.Text = "Open URL/File on balloon tip click";
            this.chkBalloonTipOpenLink.UseVisualStyleBackColor = true;
            this.chkBalloonTipOpenLink.CheckedChanged += new System.EventHandler(this.chkBalloonTipOpenLink_CheckedChanged);
            // 
            // cbShowUploadDuration
            // 
            this.cbShowUploadDuration.AutoSize = true;
            this.cbShowUploadDuration.Location = new System.Drawing.Point(16, 72);
            this.cbShowUploadDuration.Name = "cbShowUploadDuration";
            this.cbShowUploadDuration.Size = new System.Drawing.Size(191, 17);
            this.cbShowUploadDuration.TabIndex = 8;
            this.cbShowUploadDuration.Text = "Show upload duration in balloon tip";
            this.cbShowUploadDuration.UseVisualStyleBackColor = true;
            this.cbShowUploadDuration.CheckedChanged += new System.EventHandler(this.cbShowUploadDuration_CheckedChanged);
            // 
            // gbDropBox
            // 
            this.gbDropBox.Controls.Add(this.cbCloseDropBox);
            this.gbDropBox.Location = new System.Drawing.Point(8, 256);
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
            this.gbAppearance.Controls.Add(this.chkTwitterEnable);
            this.gbAppearance.Controls.Add(this.cbCompleteSound);
            this.gbAppearance.Controls.Add(this.chkCaptureFallback);
            this.gbAppearance.Controls.Add(this.lblTrayFlash);
            this.gbAppearance.Controls.Add(this.nudFlashIconCount);
            this.gbAppearance.Location = new System.Drawing.Point(8, 8);
            this.gbAppearance.Name = "gbAppearance";
            this.gbAppearance.Size = new System.Drawing.Size(752, 128);
            this.gbAppearance.TabIndex = 5;
            this.gbAppearance.TabStop = false;
            this.gbAppearance.Text = "After completing a task";
            // 
            // chkTwitterEnable
            // 
            this.chkTwitterEnable.AutoSize = true;
            this.chkTwitterEnable.Location = new System.Drawing.Point(16, 72);
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
            this.cbCompleteSound.Location = new System.Drawing.Point(16, 24);
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
            this.chkCaptureFallback.Location = new System.Drawing.Point(16, 48);
            this.chkCaptureFallback.Name = "chkCaptureFallback";
            this.chkCaptureFallback.Size = new System.Drawing.Size(206, 17);
            this.chkCaptureFallback.TabIndex = 7;
            this.chkCaptureFallback.Text = "Capture entire screen if Crop Shot fails";
            this.chkCaptureFallback.UseVisualStyleBackColor = true;
            this.chkCaptureFallback.CheckedChanged += new System.EventHandler(this.chkCaptureFallback_CheckedChanged);
            // 
            // lblTrayFlash
            // 
            this.lblTrayFlash.AutoSize = true;
            this.lblTrayFlash.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblTrayFlash.Location = new System.Drawing.Point(16, 96);
            this.lblTrayFlash.Name = "lblTrayFlash";
            this.lblTrayFlash.Size = new System.Drawing.Size(315, 13);
            this.lblTrayFlash.TabIndex = 3;
            this.lblTrayFlash.Text = "Number of times tray icon should flash after an upload is complete";
            // 
            // nudFlashIconCount
            // 
            this.nudFlashIconCount.Location = new System.Drawing.Point(333, 95);
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
            // tpProxy
            // 
            this.tpProxy.Controls.Add(this.gpProxySettings);
            this.tpProxy.Controls.Add(this.ucProxyAccounts);
            this.tpProxy.Location = new System.Drawing.Point(4, 22);
            this.tpProxy.Name = "tpProxy";
            this.tpProxy.Padding = new System.Windows.Forms.Padding(3);
            this.tpProxy.Size = new System.Drawing.Size(799, 408);
            this.tpProxy.TabIndex = 6;
            this.tpProxy.Text = "Proxy";
            this.tpProxy.UseVisualStyleBackColor = true;
            // 
            // gpProxySettings
            // 
            this.gpProxySettings.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gpProxySettings.Controls.Add(this.cboProxyConfig);
            this.gpProxySettings.Location = new System.Drawing.Point(16, 315);
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
            this.ucProxyAccounts.Size = new System.Drawing.Size(787, 314);
            this.ucProxyAccounts.TabIndex = 0;
            // 
            // tpHistoryOptions
            // 
            this.tpHistoryOptions.Controls.Add(this.btnClearHistory);
            this.tpHistoryOptions.Controls.Add(this.label1);
            this.tpHistoryOptions.Controls.Add(this.cbHistorySave);
            this.tpHistoryOptions.Controls.Add(this.nudHistoryMaxItems);
            this.tpHistoryOptions.Location = new System.Drawing.Point(4, 22);
            this.tpHistoryOptions.Name = "tpHistoryOptions";
            this.tpHistoryOptions.Padding = new System.Windows.Forms.Padding(3);
            this.tpHistoryOptions.Size = new System.Drawing.Size(799, 408);
            this.tpHistoryOptions.TabIndex = 16;
            this.tpHistoryOptions.Text = "History";
            this.tpHistoryOptions.UseVisualStyleBackColor = true;
            // 
            // btnClearHistory
            // 
            this.btnClearHistory.Location = new System.Drawing.Point(16, 77);
            this.btnClearHistory.Name = "btnClearHistory";
            this.btnClearHistory.Size = new System.Drawing.Size(136, 23);
            this.btnClearHistory.TabIndex = 12;
            this.btnClearHistory.Text = "Clear History...";
            this.btnClearHistory.UseVisualStyleBackColor = true;
            this.btnClearHistory.Click += new System.EventHandler(this.btnClearHistory_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(172, 13);
            this.label1.TabIndex = 11;
            this.label1.Text = "Maximum number of items in history";
            // 
            // cbHistorySave
            // 
            this.cbHistorySave.AutoSize = true;
            this.cbHistorySave.Location = new System.Drawing.Point(16, 45);
            this.cbHistorySave.Name = "cbHistorySave";
            this.cbHistorySave.Size = new System.Drawing.Size(232, 17);
            this.cbHistorySave.TabIndex = 10;
            this.cbHistorySave.Text = "Save successfully uploaded items to History";
            this.cbHistorySave.UseVisualStyleBackColor = true;
            this.cbHistorySave.CheckedChanged += new System.EventHandler(this.cbHistorySave_CheckedChanged);
            // 
            // nudHistoryMaxItems
            // 
            this.nudHistoryMaxItems.Location = new System.Drawing.Point(192, 13);
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
            // tpBackupRestore
            // 
            this.tpBackupRestore.Controls.Add(this.gbBackupRestoreOutputs);
            this.tpBackupRestore.Controls.Add(this.gbBackupRestoreFTP);
            this.tpBackupRestore.Controls.Add(this.gbSettingsExportImport);
            this.tpBackupRestore.Location = new System.Drawing.Point(4, 22);
            this.tpBackupRestore.Name = "tpBackupRestore";
            this.tpBackupRestore.Padding = new System.Windows.Forms.Padding(3);
            this.tpBackupRestore.Size = new System.Drawing.Size(799, 408);
            this.tpBackupRestore.TabIndex = 17;
            this.tpBackupRestore.Text = "Backup & Restore";
            this.tpBackupRestore.UseVisualStyleBackColor = true;
            // 
            // gbBackupRestoreOutputs
            // 
            this.gbBackupRestoreOutputs.Controls.Add(this.btnOutputsConfigExport);
            this.gbBackupRestoreOutputs.Controls.Add(this.btnOutputsConfigImport);
            this.gbBackupRestoreOutputs.Location = new System.Drawing.Point(168, 16);
            this.gbBackupRestoreOutputs.Name = "gbBackupRestoreOutputs";
            this.gbBackupRestoreOutputs.Size = new System.Drawing.Size(200, 136);
            this.gbBackupRestoreOutputs.TabIndex = 41;
            this.gbBackupRestoreOutputs.TabStop = false;
            this.gbBackupRestoreOutputs.Text = "Outputs";
            // 
            // btnOutputsConfigExport
            // 
            this.btnOutputsConfigExport.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnOutputsConfigExport.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnOutputsConfigExport.Location = new System.Drawing.Point(16, 56);
            this.btnOutputsConfigExport.Name = "btnOutputsConfigExport";
            this.btnOutputsConfigExport.Size = new System.Drawing.Size(168, 24);
            this.btnOutputsConfigExport.TabIndex = 3;
            this.btnOutputsConfigExport.Text = "Export Outputs Configuration...";
            this.btnOutputsConfigExport.UseVisualStyleBackColor = true;
            this.btnOutputsConfigExport.Click += new System.EventHandler(this.btnOutputsConfigExport_Click);
            // 
            // btnOutputsConfigImport
            // 
            this.btnOutputsConfigImport.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnOutputsConfigImport.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnOutputsConfigImport.Location = new System.Drawing.Point(16, 24);
            this.btnOutputsConfigImport.Name = "btnOutputsConfigImport";
            this.btnOutputsConfigImport.Size = new System.Drawing.Size(168, 24);
            this.btnOutputsConfigImport.TabIndex = 2;
            this.btnOutputsConfigImport.Text = "Import Outputs Configuration...";
            this.btnOutputsConfigImport.UseVisualStyleBackColor = true;
            this.btnOutputsConfigImport.Click += new System.EventHandler(this.btnOutputsConfigImport_Click);
            // 
            // gbBackupRestoreFTP
            // 
            this.gbBackupRestoreFTP.Controls.Add(this.btnFTPImport);
            this.gbBackupRestoreFTP.Controls.Add(this.btnFTPExport);
            this.gbBackupRestoreFTP.Location = new System.Drawing.Point(392, 16);
            this.gbBackupRestoreFTP.Name = "gbBackupRestoreFTP";
            this.gbBackupRestoreFTP.Size = new System.Drawing.Size(144, 136);
            this.gbBackupRestoreFTP.TabIndex = 40;
            this.gbBackupRestoreFTP.TabStop = false;
            this.gbBackupRestoreFTP.Text = "FTP";
            // 
            // btnFTPImport
            // 
            this.btnFTPImport.AutoSize = true;
            this.btnFTPImport.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnFTPImport.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnFTPImport.Location = new System.Drawing.Point(8, 24);
            this.btnFTPImport.Name = "btnFTPImport";
            this.btnFTPImport.Size = new System.Drawing.Size(126, 23);
            this.btnFTPImport.TabIndex = 39;
            this.btnFTPImport.Text = "Import FTP Accounts...";
            this.btnFTPImport.UseVisualStyleBackColor = true;
            // 
            // btnFTPExport
            // 
            this.btnFTPExport.AutoSize = true;
            this.btnFTPExport.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnFTPExport.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnFTPExport.Location = new System.Drawing.Point(8, 56);
            this.btnFTPExport.Name = "btnFTPExport";
            this.btnFTPExport.Size = new System.Drawing.Size(127, 23);
            this.btnFTPExport.TabIndex = 38;
            this.btnFTPExport.Text = "Export FTP Accounts...";
            this.btnFTPExport.UseVisualStyleBackColor = true;
            // 
            // gbSettingsExportImport
            // 
            this.gbSettingsExportImport.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbSettingsExportImport.BackColor = System.Drawing.Color.Transparent;
            this.gbSettingsExportImport.Controls.Add(this.btnSettingsDefault);
            this.gbSettingsExportImport.Controls.Add(this.btnSettingsExport);
            this.gbSettingsExportImport.Controls.Add(this.btnSettingsImport);
            this.gbSettingsExportImport.Location = new System.Drawing.Point(16, 16);
            this.gbSettingsExportImport.Name = "gbSettingsExportImport";
            this.gbSettingsExportImport.Size = new System.Drawing.Size(136, 136);
            this.gbSettingsExportImport.TabIndex = 6;
            this.gbSettingsExportImport.TabStop = false;
            this.gbSettingsExportImport.Text = "Application Settings";
            // 
            // btnSettingsDefault
            // 
            this.btnSettingsDefault.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnSettingsDefault.Location = new System.Drawing.Point(16, 88);
            this.btnSettingsDefault.Name = "btnSettingsDefault";
            this.btnSettingsDefault.Size = new System.Drawing.Size(104, 24);
            this.btnSettingsDefault.TabIndex = 1;
            this.btnSettingsDefault.Text = "Default Settings...";
            this.btnSettingsDefault.UseVisualStyleBackColor = true;
            this.btnSettingsDefault.Click += new System.EventHandler(this.btnDeleteSettings_Click);
            // 
            // btnSettingsExport
            // 
            this.btnSettingsExport.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnSettingsExport.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnSettingsExport.Location = new System.Drawing.Point(16, 56);
            this.btnSettingsExport.Name = "btnSettingsExport";
            this.btnSettingsExport.Size = new System.Drawing.Size(104, 24);
            this.btnSettingsExport.TabIndex = 1;
            this.btnSettingsExport.Text = "Export Settings...";
            this.btnSettingsExport.UseVisualStyleBackColor = true;
            this.btnSettingsExport.Click += new System.EventHandler(this.btnSettingsExport_Click);
            // 
            // btnSettingsImport
            // 
            this.btnSettingsImport.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnSettingsImport.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnSettingsImport.Location = new System.Drawing.Point(16, 24);
            this.btnSettingsImport.Name = "btnSettingsImport";
            this.btnSettingsImport.Size = new System.Drawing.Size(104, 24);
            this.btnSettingsImport.TabIndex = 0;
            this.btnSettingsImport.Text = "Import Settings...";
            this.btnSettingsImport.UseVisualStyleBackColor = true;
            this.btnSettingsImport.Click += new System.EventHandler(this.btnSettingsImport_Click);
            // 
            // tpAdvanced
            // 
            this.tpAdvanced.Controls.Add(this.tcAdvanced);
            this.tpAdvanced.Location = new System.Drawing.Point(4, 22);
            this.tpAdvanced.Name = "tpAdvanced";
            this.tpAdvanced.Padding = new System.Windows.Forms.Padding(3);
            this.tpAdvanced.Size = new System.Drawing.Size(813, 440);
            this.tpAdvanced.TabIndex = 3;
            this.tpAdvanced.Text = "Advanced";
            this.tpAdvanced.UseVisualStyleBackColor = true;
            // 
            // tcAdvanced
            // 
            this.tcAdvanced.Controls.Add(this.tpAdvancedSettings);
            this.tcAdvanced.Controls.Add(this.tpAdvancedWorkflow);
            this.tcAdvanced.Controls.Add(this.tpAdvancedCore);
            this.tcAdvanced.Controls.Add(this.tpAdvancedDebug);
            this.tcAdvanced.Controls.Add(this.tpAdvancedStats);
            this.tcAdvanced.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcAdvanced.Location = new System.Drawing.Point(3, 3);
            this.tcAdvanced.Name = "tcAdvanced";
            this.tcAdvanced.SelectedIndex = 0;
            this.tcAdvanced.Size = new System.Drawing.Size(807, 434);
            this.tcAdvanced.TabIndex = 1;
            this.tcAdvanced.Selected += new System.Windows.Forms.TabControlEventHandler(this.tcAdvanced_Selected);
            // 
            // tpAdvancedSettings
            // 
            this.tpAdvancedSettings.Controls.Add(this.pgAppConfig);
            this.tpAdvancedSettings.Location = new System.Drawing.Point(4, 22);
            this.tpAdvancedSettings.Name = "tpAdvancedSettings";
            this.tpAdvancedSettings.Padding = new System.Windows.Forms.Padding(3);
            this.tpAdvancedSettings.Size = new System.Drawing.Size(799, 408);
            this.tpAdvancedSettings.TabIndex = 0;
            this.tpAdvancedSettings.Text = "Settings";
            this.tpAdvancedSettings.UseVisualStyleBackColor = true;
            // 
            // pgAppConfig
            // 
            this.pgAppConfig.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pgAppConfig.Location = new System.Drawing.Point(3, 3);
            this.pgAppConfig.Name = "pgAppConfig";
            this.pgAppConfig.Size = new System.Drawing.Size(793, 402);
            this.pgAppConfig.TabIndex = 0;
            this.pgAppConfig.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.pgApp_PropertyValueChanged);
            // 
            // tpAdvancedWorkflow
            // 
            this.tpAdvancedWorkflow.Controls.Add(this.pgWorkflow);
            this.tpAdvancedWorkflow.Location = new System.Drawing.Point(4, 22);
            this.tpAdvancedWorkflow.Name = "tpAdvancedWorkflow";
            this.tpAdvancedWorkflow.Padding = new System.Windows.Forms.Padding(3);
            this.tpAdvancedWorkflow.Size = new System.Drawing.Size(799, 408);
            this.tpAdvancedWorkflow.TabIndex = 8;
            this.tpAdvancedWorkflow.Text = "Workflow";
            this.tpAdvancedWorkflow.UseVisualStyleBackColor = true;
            // 
            // pgWorkflow
            // 
            this.pgWorkflow.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pgWorkflow.Location = new System.Drawing.Point(3, 3);
            this.pgWorkflow.Name = "pgWorkflow";
            this.pgWorkflow.Size = new System.Drawing.Size(793, 402);
            this.pgWorkflow.TabIndex = 1;
            // 
            // tpAdvancedCore
            // 
            this.tpAdvancedCore.Controls.Add(this.pgAppSettings);
            this.tpAdvancedCore.Location = new System.Drawing.Point(4, 22);
            this.tpAdvancedCore.Name = "tpAdvancedCore";
            this.tpAdvancedCore.Padding = new System.Windows.Forms.Padding(3);
            this.tpAdvancedCore.Size = new System.Drawing.Size(799, 408);
            this.tpAdvancedCore.TabIndex = 9;
            this.tpAdvancedCore.Text = "Core";
            this.tpAdvancedCore.UseVisualStyleBackColor = true;
            // 
            // pgAppSettings
            // 
            this.pgAppSettings.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pgAppSettings.HelpVisible = false;
            this.pgAppSettings.Location = new System.Drawing.Point(3, 3);
            this.pgAppSettings.Name = "pgAppSettings";
            this.pgAppSettings.Size = new System.Drawing.Size(793, 402);
            this.pgAppSettings.TabIndex = 118;
            this.pgAppSettings.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.pgAppSettings_PropertyValueChanged);
            // 
            // tpAdvancedDebug
            // 
            this.tpAdvancedDebug.Controls.Add(this.rtbDebugLog);
            this.tpAdvancedDebug.Location = new System.Drawing.Point(4, 22);
            this.tpAdvancedDebug.Name = "tpAdvancedDebug";
            this.tpAdvancedDebug.Padding = new System.Windows.Forms.Padding(3);
            this.tpAdvancedDebug.Size = new System.Drawing.Size(799, 408);
            this.tpAdvancedDebug.TabIndex = 7;
            this.tpAdvancedDebug.Text = "Debug";
            this.tpAdvancedDebug.UseVisualStyleBackColor = true;
            // 
            // rtbDebugLog
            // 
            this.rtbDebugLog.BackColor = System.Drawing.Color.WhiteSmoke;
            this.rtbDebugLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbDebugLog.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.rtbDebugLog.Location = new System.Drawing.Point(3, 3);
            this.rtbDebugLog.Name = "rtbDebugLog";
            this.rtbDebugLog.ReadOnly = true;
            this.rtbDebugLog.Size = new System.Drawing.Size(793, 402);
            this.rtbDebugLog.TabIndex = 0;
            this.rtbDebugLog.Text = "";
            this.rtbDebugLog.WordWrap = false;
            // 
            // tpAdvancedStats
            // 
            this.tpAdvancedStats.Controls.Add(this.btnOpenZScreenTester);
            this.tpAdvancedStats.Controls.Add(this.gbStatistics);
            this.tpAdvancedStats.Controls.Add(this.gbLastSource);
            this.tpAdvancedStats.Location = new System.Drawing.Point(4, 22);
            this.tpAdvancedStats.Name = "tpAdvancedStats";
            this.tpAdvancedStats.Padding = new System.Windows.Forms.Padding(3);
            this.tpAdvancedStats.Size = new System.Drawing.Size(799, 408);
            this.tpAdvancedStats.TabIndex = 1;
            this.tpAdvancedStats.Text = "Statistics";
            this.tpAdvancedStats.UseVisualStyleBackColor = true;
            // 
            // btnOpenZScreenTester
            // 
            this.btnOpenZScreenTester.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOpenZScreenTester.Location = new System.Drawing.Point(608, 337);
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
            this.gbStatistics.Controls.Add(this.rtbStats);
            this.gbStatistics.Location = new System.Drawing.Point(8, 8);
            this.gbStatistics.Name = "gbStatistics";
            this.gbStatistics.Size = new System.Drawing.Size(775, 311);
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
            // rtbStats
            // 
            this.rtbStats.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rtbStats.BackColor = System.Drawing.Color.WhiteSmoke;
            this.rtbStats.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.rtbStats.Location = new System.Drawing.Point(16, 56);
            this.rtbStats.Name = "rtbStats";
            this.rtbStats.ReadOnly = true;
            this.rtbStats.Size = new System.Drawing.Size(745, 245);
            this.rtbStats.TabIndex = 27;
            this.rtbStats.Text = "";
            this.rtbStats.WordWrap = false;
            // 
            // gbLastSource
            // 
            this.gbLastSource.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbLastSource.Controls.Add(this.btnOpenSourceString);
            this.gbLastSource.Controls.Add(this.btnOpenSourceText);
            this.gbLastSource.Controls.Add(this.btnOpenSourceBrowser);
            this.gbLastSource.Location = new System.Drawing.Point(8, 329);
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
            // 
            // tpQueue
            // 
            this.tpQueue.Controls.Add(this.lvUploads);
            this.tpQueue.Location = new System.Drawing.Point(4, 22);
            this.tpQueue.Name = "tpQueue";
            this.tpQueue.Padding = new System.Windows.Forms.Padding(3);
            this.tpQueue.Size = new System.Drawing.Size(813, 440);
            this.tpQueue.TabIndex = 10;
            this.tpQueue.Text = "Queue";
            this.tpQueue.UseVisualStyleBackColor = true;
            // 
            // lvUploads
            // 
            this.lvUploads.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chFilename,
            this.chStatus,
            this.chProgress,
            this.chSpeed,
            this.chElapsed,
            this.chRemaining,
            this.chUploaderType,
            this.chHost,
            this.chURL});
            this.lvUploads.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvUploads.FullRowSelect = true;
            this.lvUploads.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lvUploads.HideSelection = false;
            this.lvUploads.Location = new System.Drawing.Point(3, 3);
            this.lvUploads.Name = "lvUploads";
            this.lvUploads.ShowItemToolTips = true;
            this.lvUploads.Size = new System.Drawing.Size(807, 434);
            this.lvUploads.TabIndex = 4;
            this.lvUploads.UseCompatibleStateImageBehavior = false;
            this.lvUploads.View = System.Windows.Forms.View.Details;
            // 
            // chFilename
            // 
            this.chFilename.Text = "Filename";
            this.chFilename.Width = 150;
            // 
            // chStatus
            // 
            this.chStatus.Text = "Status";
            this.chStatus.Width = 75;
            // 
            // chProgress
            // 
            this.chProgress.Text = "Progress";
            this.chProgress.Width = 149;
            // 
            // chSpeed
            // 
            this.chSpeed.Text = "Speed";
            this.chSpeed.Width = 65;
            // 
            // chElapsed
            // 
            this.chElapsed.Text = "Elapsed";
            this.chElapsed.Width = 50;
            // 
            // chRemaining
            // 
            this.chRemaining.Text = "Remaining";
            this.chRemaining.Width = 50;
            // 
            // chUploaderType
            // 
            this.chUploaderType.Text = "Type";
            this.chUploaderType.Width = 50;
            // 
            // chHost
            // 
            this.chHost.Text = "Host";
            this.chHost.Width = 100;
            // 
            // chURL
            // 
            this.chURL.Text = "URL";
            this.chURL.Width = 225;
            // 
            // tpDestImageBam
            // 
            this.tpDestImageBam.BackColor = System.Drawing.SystemColors.Window;
            this.tpDestImageBam.Controls.Add(this.gbImageBamGalleries);
            this.tpDestImageBam.Controls.Add(this.gbImageBamLinks);
            this.tpDestImageBam.Controls.Add(this.gbImageBamApiKeys);
            this.tpDestImageBam.Location = new System.Drawing.Point(4, 22);
            this.tpDestImageBam.Name = "tpDestImageBam";
            this.tpDestImageBam.Padding = new System.Windows.Forms.Padding(3);
            this.tpDestImageBam.Size = new System.Drawing.Size(791, 404);
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
            // 
            // btnImageBamRemoveGallery
            // 
            this.btnImageBamRemoveGallery.Location = new System.Drawing.Point(16, 120);
            this.btnImageBamRemoveGallery.Name = "btnImageBamRemoveGallery";
            this.btnImageBamRemoveGallery.Size = new System.Drawing.Size(128, 23);
            this.btnImageBamRemoveGallery.TabIndex = 9;
            this.btnImageBamRemoveGallery.Text = "Remove &Gallery";
            this.btnImageBamRemoveGallery.UseVisualStyleBackColor = true;
            // 
            // btnImageBamCreateGallery
            // 
            this.btnImageBamCreateGallery.Location = new System.Drawing.Point(16, 88);
            this.btnImageBamCreateGallery.Name = "btnImageBamCreateGallery";
            this.btnImageBamCreateGallery.Size = new System.Drawing.Size(128, 23);
            this.btnImageBamCreateGallery.TabIndex = 8;
            this.btnImageBamCreateGallery.Text = "Create &Gallery";
            this.btnImageBamCreateGallery.UseVisualStyleBackColor = true;
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
            this.ClientSize = new System.Drawing.Size(825, 470);
            this.Controls.Add(this.tcMain);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(833, 504);
            this.Name = "ZScreen";
            this.Padding = new System.Windows.Forms.Padding(2);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ZScreen";
            this.Deactivate += new System.EventHandler(this.ZScreen_Deactivate);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ZScreen_FormClosing);
            this.Load += new System.EventHandler(this.ZScreen_Load);
            this.Leave += new System.EventHandler(this.ZScreen_Leave);
            this.Resize += new System.EventHandler(this.ZScreen_Resize);
            this.cmTray.ResumeLayout(false);
            this.tcMain.ResumeLayout(false);
            this.tpMain.ResumeLayout(false);
            this.tpMain.PerformLayout();
            this.tsLinks.ResumeLayout(false);
            this.tsLinks.PerformLayout();
            this.tsMainTab.ResumeLayout(false);
            this.tsMainTab.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbLogo)).EndInit();
            this.gbImageSettings.ResumeLayout(false);
            this.gbImageSettings.PerformLayout();
            this.tpHotkeys.ResumeLayout(false);
            this.tpHotkeys.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvHotkeys)).EndInit();
            this.tpMainInput.ResumeLayout(false);
            this.tcCapture.ResumeLayout(false);
            this.tpActivewindow.ResumeLayout(false);
            this.tpActivewindow.PerformLayout();
            this.tpSelectedWindow.ResumeLayout(false);
            this.tpSelectedWindow.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudSelectedWindowHueRange)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudSelectedWindowRegionStep)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudSelectedWindowRegionInterval)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudSelectedWindowBorderSize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbSelectedWindowBorderColor)).EndInit();
            this.tpCropShot.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.gbCropShotMagnifyingGlass.ResumeLayout(false);
            this.gbCropShotMagnifyingGlass.PerformLayout();
            this.gbCropDynamicRegionBorderColorSettings.ResumeLayout(false);
            this.gbCropDynamicRegionBorderColorSettings.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudCropRegionStep)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudCropHueRange)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudCropRegionInterval)).EndInit();
            this.gbCropRegion.ResumeLayout(false);
            this.gbCropRegion.PerformLayout();
            this.gbCropRegionSettings.ResumeLayout(false);
            this.gbCropRegionSettings.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbCropBorderColor)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudCropBorderSize)).EndInit();
            this.gbCropCrosshairSettings.ResumeLayout(false);
            this.gbCropCrosshairSettings.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbCropCrosshairColor)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudCrosshairLineCount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudCropCrosshairInterval)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudCrosshairLineSize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudCropCrosshairStep)).EndInit();
            this.gbCropGridMode.ResumeLayout(false);
            this.gbCropGridMode.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudCropGridHeight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudCropGridWidth)).EndInit();
            this.tpCropShotLast.ResumeLayout(false);
            this.tpCropShotLast.PerformLayout();
            this.tpCaptureShape.ResumeLayout(false);
            this.tpFreehandCropShot.ResumeLayout(false);
            this.tpFreehandCropShot.PerformLayout();
            this.tpCaptureClipboard.ResumeLayout(false);
            this.gbMonitorClipboard.ResumeLayout(false);
            this.gbMonitorClipboard.PerformLayout();
            this.tpMainActions.ResumeLayout(false);
            this.tpOptions.ResumeLayout(false);
            this.tcOptions.ResumeLayout(false);
            this.tpOptionsGeneral.ResumeLayout(false);
            this.gbUpdates.ResumeLayout(false);
            this.gbUpdates.PerformLayout();
            this.gbMisc.ResumeLayout(false);
            this.gbMisc.PerformLayout();
            this.gbWindowButtons.ResumeLayout(false);
            this.gbWindowButtons.PerformLayout();
            this.tpCaptureQuality.ResumeLayout(false);
            this.gbImageSize.ResumeLayout(false);
            this.gbImageSize.PerformLayout();
            this.gbPictureQuality.ResumeLayout(false);
            this.gbPictureQuality.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudSwitchAfter)).EndInit();
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
            this.tpPaths.ResumeLayout(false);
            this.gbRoot.ResumeLayout(false);
            this.gbRoot.PerformLayout();
            this.gbImages.ResumeLayout(false);
            this.gbImages.PerformLayout();
            this.gbLogs.ResumeLayout(false);
            this.gbLogs.PerformLayout();
            this.tpFileNaming.ResumeLayout(false);
            this.tpFileNaming.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudMaxNameLength)).EndInit();
            this.gbOthersNaming.ResumeLayout(false);
            this.gbOthersNaming.PerformLayout();
            this.gbCodeTitle.ResumeLayout(false);
            this.gbCodeTitle.PerformLayout();
            this.gbActiveWindowNaming.ResumeLayout(false);
            this.gbActiveWindowNaming.PerformLayout();
            this.tpTreeGUI.ResumeLayout(false);
            this.tpInteraction.ResumeLayout(false);
            this.btnOptionsBalloonTip.ResumeLayout(false);
            this.btnOptionsBalloonTip.PerformLayout();
            this.gbDropBox.ResumeLayout(false);
            this.gbDropBox.PerformLayout();
            this.gbAppearance.ResumeLayout(false);
            this.gbAppearance.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudFlashIconCount)).EndInit();
            this.tpProxy.ResumeLayout(false);
            this.gpProxySettings.ResumeLayout(false);
            this.tpHistoryOptions.ResumeLayout(false);
            this.tpHistoryOptions.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudHistoryMaxItems)).EndInit();
            this.tpBackupRestore.ResumeLayout(false);
            this.gbBackupRestoreOutputs.ResumeLayout(false);
            this.gbBackupRestoreFTP.ResumeLayout(false);
            this.gbBackupRestoreFTP.PerformLayout();
            this.gbSettingsExportImport.ResumeLayout(false);
            this.tpAdvanced.ResumeLayout(false);
            this.tcAdvanced.ResumeLayout(false);
            this.tpAdvancedSettings.ResumeLayout(false);
            this.tpAdvancedWorkflow.ResumeLayout(false);
            this.tpAdvancedCore.ResumeLayout(false);
            this.tpAdvancedDebug.ResumeLayout(false);
            this.tpAdvancedStats.ResumeLayout(false);
            this.gbStatistics.ResumeLayout(false);
            this.gbLastSource.ResumeLayout(false);
            this.tpQueue.ResumeLayout(false);
            this.tpDestImageBam.ResumeLayout(false);
            this.gbImageBamGalleries.ResumeLayout(false);
            this.gbImageBamLinks.ResumeLayout(false);
            this.gbImageBamLinks.PerformLayout();
            this.gbImageBamApiKeys.ResumeLayout(false);
            this.gbImageBamApiKeys.PerformLayout();
            this.ResumeLayout(false);

        }

        internal System.Windows.Forms.GroupBox gbLogs;
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
        internal System.Windows.Forms.ToolStripMenuItem autoScreenshotsToolStripMenuItem;
        internal System.Windows.Forms.Timer tmrApp;
        internal System.Windows.Forms.TabControl tcMain;
        internal System.Windows.Forms.TabPage tpMain;
        internal System.Windows.Forms.GroupBox gbCropGridMode;
        internal System.Windows.Forms.CheckBox cboCropGridMode;
        internal System.Windows.Forms.NumericUpDown nudCropGridHeight;
        internal System.Windows.Forms.Label lblGridSizeWidth;
        internal System.Windows.Forms.Label lblGridSize;
        internal System.Windows.Forms.Label lblGridSizeHeight;
        internal System.Windows.Forms.NumericUpDown nudCropGridWidth;
        internal NumericUpDownTimer nudScreenshotDelay;
        internal System.Windows.Forms.CheckBox chkManualNaming;
        internal System.Windows.Forms.CheckBox chkShowCursor;
        internal System.Windows.Forms.PictureBox pbLogo;
        internal System.Windows.Forms.TabPage tpHotkeys;
        internal System.Windows.Forms.Label lblHotkeyStatus;
        internal System.Windows.Forms.DataGridView dgvHotkeys;
        internal System.Windows.Forms.TabPage tpMainInput;
        internal System.Windows.Forms.TabControl tcCapture;
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
        internal System.Windows.Forms.GroupBox gbCropCrosshairSettings;
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
        internal System.Windows.Forms.Button btnActionsRemove;
        internal System.Windows.Forms.CheckedListBox lbSoftware;
        internal System.Windows.Forms.Button btnAddImageSoftware;
        internal System.Windows.Forms.Button btnFTPImport;
        internal System.Windows.Forms.Button btnFTPExport;
        internal System.Windows.Forms.TabPage tpOptions;
        internal System.Windows.Forms.TabControl tcOptions;
        internal System.Windows.Forms.TabPage tpOptionsGeneral;
        internal System.Windows.Forms.GroupBox gbUpdates;
        internal System.Windows.Forms.Label lblUpdateInfo;
        internal System.Windows.Forms.Button btnCheckUpdate;
        internal System.Windows.Forms.CheckBox chkCheckUpdates;
        internal System.Windows.Forms.GroupBox gbMisc;
        internal System.Windows.Forms.CheckBox chkShowTaskbar;
        internal System.Windows.Forms.CheckBox chkOpenMainWindow;
        internal System.Windows.Forms.CheckBox chkStartWin;
        internal System.Windows.Forms.TabPage tpPaths;
        internal System.Windows.Forms.GroupBox gbRoot;
        internal System.Windows.Forms.Button btnViewRootDir;
        internal System.Windows.Forms.Button btnRelocateRootDir;
        internal System.Windows.Forms.TextBox txtRootFolder;
        internal System.Windows.Forms.CheckBox chkDeleteLocal;
        internal System.Windows.Forms.Button btnViewImagesDir;
        internal System.Windows.Forms.TextBox txtImagesDir;
        internal System.Windows.Forms.Button btnSettingsDefault;
        internal System.Windows.Forms.Button btnSettingsExport;
        internal System.Windows.Forms.Button btnSettingsImport;
        internal System.Windows.Forms.Button btnViewCacheDir;
        internal System.Windows.Forms.TextBox txtLogsDir;
        internal System.Windows.Forms.TabPage tpAdvancedStats;
        internal System.Windows.Forms.GroupBox gbStatistics;
        internal System.Windows.Forms.Button btnDebugStart;
        internal System.Windows.Forms.GroupBox gbLastSource;
        internal System.Windows.Forms.Button btnOpenSourceString;
        internal System.Windows.Forms.Button btnOpenSourceText;
        internal System.Windows.Forms.Button btnOpenSourceBrowser;
        internal System.Windows.Forms.TabPage tpAdvanced;
        internal System.Windows.Forms.PropertyGrid pgAppConfig;
        internal System.Windows.Forms.TabPage tpUploadText;
        internal System.Windows.Forms.TextBox txtTextUploaderContent;
        internal System.Windows.Forms.Button btnUploadText;
        internal System.Windows.Forms.Button btnUploadTextClipboard;
        internal System.Windows.Forms.Button btnUploadTextClipboardFile;
        internal System.Windows.Forms.PropertyGrid pgEditorsImage;
        internal System.Windows.Forms.GroupBox gbImageSettings;
        internal System.Windows.Forms.GroupBox gbCropRegion;
        internal System.Windows.Forms.ToolTip ttZScreen;
        internal System.Windows.Forms.CheckBox cbShowHelpBalloonTips;
        internal System.Windows.Forms.Label lblScreenshotDelay;
        internal System.Windows.Forms.GroupBox gbCropDynamicRegionBorderColorSettings;
        private System.Windows.Forms.TabPage tpProxy;
        internal System.Windows.Forms.GroupBox gpProxySettings;
        private System.Windows.Forms.ToolStripMenuItem tsmFTPClient;
        private System.Windows.Forms.TabPage tpTreeGUI;
        private System.Windows.Forms.PropertyGrid pgIndexer;
        private System.Windows.Forms.CheckBox chkSelectedWindowCaptureObjects;
        private System.Windows.Forms.Label lblWatermarkOffsetPixel;
        private System.Windows.Forms.CheckBox cbAutoSaveSettings;
        private System.Windows.Forms.TextBox txtImagesFolderPattern;
        private System.Windows.Forms.Label lblImagesFolderPatternPreview;
        private System.Windows.Forms.Label lblImagesFolderPattern;
        private System.Windows.Forms.Button btnMoveImageFiles;
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
        internal System.Windows.Forms.RichTextBox rtbStats;
        private System.Windows.Forms.CheckBox chkWindows7TaskbarIntegration;
        private System.Windows.Forms.CheckBox chkShellExt;
        private System.Windows.Forms.CheckBox chkTwitterEnable;
        private System.Windows.Forms.Button btnOpenZScreenTester;
        private System.Windows.Forms.Label lblMaxNameLength;
        private System.Windows.Forms.NumericUpDown nudMaxNameLength;
        private System.Windows.Forms.Button btnSelectGradient;
        private System.Windows.Forms.CheckBox cboUseCustomGradient;
        private System.Windows.Forms.GroupBox gbGradientMakerBasic;
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
        private System.Windows.Forms.ComboBox cbGIFQuality;
        private System.Windows.Forms.Label lblGIFQuality;
        private System.Windows.Forms.TabPage tpAdvancedDebug;
        private System.Windows.Forms.RichTextBox rtbDebugLog;
        private System.Windows.Forms.ComboBox cboCloseButtonAction;
        private System.Windows.Forms.ComboBox cboMinimizeButtonAction;
        private System.Windows.Forms.Label lblMinimizeButtonAction;
        private System.Windows.Forms.Label lblCloseButtonAction;
        internal System.Windows.Forms.GroupBox gbSettingsExportImport;
        private System.Windows.Forms.Button btnResetHotkeys;
        private System.Windows.Forms.TabPage tpFreehandCropShot;
        private System.Windows.Forms.CheckBox cbFreehandCropShowHelpText;
        private System.Windows.Forms.CheckBox cbFreehandCropAutoUpload;
        private System.Windows.Forms.CheckBox cbFreehandCropAutoClose;
        private System.Windows.Forms.CheckBox cbFreehandCropShowRectangleBorder;
        internal AccountsControl ucProxyAccounts;
        internal System.Windows.Forms.ComboBox cboProxyConfig;
        internal System.Windows.Forms.CheckBox cbHistorySave;
        internal System.Windows.Forms.NumericUpDown nudHistoryMaxItems;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolStripMenuItem historyToolStripMenuItem;
        private System.Windows.Forms.TabPage tpCaptureClipboard;
        private System.Windows.Forms.Label lblFileSystemNote;
        private System.Windows.Forms.GroupBox btnOptionsBalloonTip;
        private System.Windows.Forms.CheckBox chkShortenURL;
        private System.Windows.Forms.ComboBox cboReleaseChannel;
        private System.Windows.Forms.Button btnClearHistory;
        private System.Windows.Forms.DataGridViewTextBoxColumn chHotkeys_Description;
        private System.Windows.Forms.DataGridViewButtonColumn chHotkeys_Keys;
        private System.Windows.Forms.DataGridViewTextBoxColumn DefaultKeys;
        private System.Windows.Forms.ToolStripMenuItem tsmCaptureShape;
        private System.Windows.Forms.ToolStripMenuItem tsmFileUpload;
        private System.Windows.Forms.ToolStrip tsMainTab;
        private System.Windows.Forms.ToolStripButton tsbFullscreenCapture;
        private System.Windows.Forms.ToolStripButton tsbSelectedWindow;
        private System.Windows.Forms.ToolStripButton tsbCropShot;
        private System.Windows.Forms.ToolStripButton tsbLastCropShot;
        private System.Windows.Forms.ToolStripButton tsbFreehandCropShot;
        private System.Windows.Forms.ToolStripButton tsbAutoCapture;
        private System.Windows.Forms.ToolStripSeparator tssMaintoolbar1;
        private System.Windows.Forms.ToolStripButton tsbFileUpload;
        private System.Windows.Forms.ToolStripButton tsbClipboardUpload;
        private System.Windows.Forms.ToolStripButton tsbDragDropWindow;
        private System.Windows.Forms.ToolStripButton tsbLanguageTranslator;
        private System.Windows.Forms.ToolStripButton tsbScreenColorPicker;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator8;
        private System.Windows.Forms.ToolStripButton tsbOpenHistory;
        private System.Windows.Forms.TabPage tpHistoryOptions;
        private System.Windows.Forms.ToolStripButton tsbActiveWindow;
        private ToolStripButton tsbAbout;
        private ToolStripButton tsbImageDirectory;
        private TabControl tcAdvanced;
        private TabPage tpAdvancedSettings;
        private TabPage tpBackupRestore;
        private PropertyGrid pgAppSettings;
        internal DestSelector ucDestOptions;
        private Label lblImageSizeFixedAutoScale;
        private CheckBox chkShowUploadResults;
        private ToolStripLabel tsbDonate;
        private TabPage tpCropShotLast;
        private Button btnLastCropShotReset;
        internal GroupBox gbCropShotMagnifyingGlass;
        private CheckBox chkOverwriteFiles;
        private TabPage tpAdvancedWorkflow;
        internal PropertyGrid pgWorkflow;
        private Label lblNoteActions;
        private TabPage tpAdvancedCore;
        internal Button btnOutputsConfigExport;
        private GroupBox gbBackupRestoreFTP;
        internal Button btnOutputsConfigImport;
        private GroupBox gbBackupRestoreOutputs;
        private TabPage tpCaptureShape;
        internal PropertyGrid pgSurfaceConfig;
        internal GroupBox groupBox1;
        private ComboBox cboCropEngine;
        private ToolStrip tsLinks;
        private ToolStripButton tsbLinkHome;
        private ToolStripButton toolStripButton2;
        private ToolStripButton toolStripButton3;
        private ComboBox cboJpgSubSampling;
        private ComboBox cboJpgQuality;
        private TabPage tpQueue;
        private HelpersLib.MyListView lvUploads;
        private ColumnHeader chFilename;
        private ColumnHeader chStatus;
        private ColumnHeader chProgress;
        private ColumnHeader chSpeed;
        private ColumnHeader chElapsed;
        private ColumnHeader chRemaining;
        private ColumnHeader chUploaderType;
        private ColumnHeader chHost;
        private ColumnHeader chURL;
    }
}
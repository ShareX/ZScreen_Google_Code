namespace ZScreenGUI
{
    partial class ZScreenOptionsUI
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
            System.Windows.Forms.TreeNode treeNode9 = new System.Windows.Forms.TreeNode("General");
            System.Windows.Forms.TreeNode treeNode10 = new System.Windows.Forms.TreeNode("Workflow");
            System.Windows.Forms.TreeNode treeNode11 = new System.Windows.Forms.TreeNode("Folder Listing");
            System.Windows.Forms.TreeNode treeNode12 = new System.Windows.Forms.TreeNode("Capture", new System.Windows.Forms.TreeNode[] {
            treeNode11});
            System.Windows.Forms.TreeNode treeNode13 = new System.Windows.Forms.TreeNode("Effects");
            System.Windows.Forms.TreeNode treeNode14 = new System.Windows.Forms.TreeNode("History");
            System.Windows.Forms.TreeNode treeNode15 = new System.Windows.Forms.TreeNode("Saving", new System.Windows.Forms.TreeNode[] {
            treeNode14});
            System.Windows.Forms.TreeNode treeNode16 = new System.Windows.Forms.TreeNode("Backup & Restore");
            this.tlpMain = new System.Windows.Forms.TableLayoutPanel();
            this.tvOptions = new System.Windows.Forms.TreeView();
            this.tcMain = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.gbMisc = new System.Windows.Forms.GroupBox();
            this.chkShellExt = new System.Windows.Forms.CheckBox();
            this.chkWindows7TaskbarIntegration = new System.Windows.Forms.CheckBox();
            this.cbAutoSaveSettings = new System.Windows.Forms.CheckBox();
            this.cbShowHelpBalloonTips = new System.Windows.Forms.CheckBox();
            this.chkOpenMainWindow = new System.Windows.Forms.CheckBox();
            this.chkStartWin = new System.Windows.Forms.CheckBox();
            this.gbUpdates = new System.Windows.Forms.GroupBox();
            this.cboReleaseChannel = new System.Windows.Forms.ComboBox();
            this.lblUpdateInfo = new System.Windows.Forms.Label();
            this.btnCheckUpdate = new System.Windows.Forms.Button();
            this.chkCheckUpdates = new System.Windows.Forms.CheckBox();
            this.gbWindowButtons = new System.Windows.Forms.GroupBox();
            this.cboCloseButtonAction = new System.Windows.Forms.ComboBox();
            this.cboMinimizeButtonAction = new System.Windows.Forms.ComboBox();
            this.lblCloseButtonAction = new System.Windows.Forms.Label();
            this.lblMinimizeButtonAction = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.gbOptionsBalloonTip = new System.Windows.Forms.GroupBox();
            this.chkBalloonTipOpenLink = new System.Windows.Forms.CheckBox();
            this.cbShowUploadDuration = new System.Windows.Forms.CheckBox();
            this.gbAppearance = new System.Windows.Forms.GroupBox();
            this.chkShowPopup = new System.Windows.Forms.CheckBox();
            this.chkTwitterEnable = new System.Windows.Forms.CheckBox();
            this.cbCompleteSound = new System.Windows.Forms.CheckBox();
            this.chkCaptureFallback = new System.Windows.Forms.CheckBox();
            this.lblTrayFlash = new System.Windows.Forms.Label();
            this.nudFlashIconCount = new System.Windows.Forms.NumericUpDown();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.tabPage6 = new System.Windows.Forms.TabPage();
            this.pgIndexer = new System.Windows.Forms.PropertyGrid();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.tabPage7 = new System.Windows.Forms.TabPage();
            this.btnClearHistory = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.cbHistorySave = new System.Windows.Forms.CheckBox();
            this.nudHistoryMaxItems = new System.Windows.Forms.NumericUpDown();
            this.tabPage8 = new System.Windows.Forms.TabPage();
            this.gbBackupRestoreUploaders = new System.Windows.Forms.GroupBox();
            this.btnUploadersConfigExport = new System.Windows.Forms.Button();
            this.btnUploadersConfigImport = new System.Windows.Forms.Button();
            this.gbBackupRestoreOutputs = new System.Windows.Forms.GroupBox();
            this.btnOutputsConfigExport = new System.Windows.Forms.Button();
            this.btnOutputsConfigImport = new System.Windows.Forms.Button();
            this.gbSettingsExportImport = new System.Windows.Forms.GroupBox();
            this.btnSettingsDefault = new System.Windows.Forms.Button();
            this.btnSettingsExport = new System.Windows.Forms.Button();
            this.btnSettingsImport = new System.Windows.Forms.Button();
            this.pgWorkflowImageEffects = new System.Windows.Forms.PropertyGrid();
            this.tlpMain.SuspendLayout();
            this.tcMain.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.gbMisc.SuspendLayout();
            this.gbUpdates.SuspendLayout();
            this.gbWindowButtons.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.gbOptionsBalloonTip.SuspendLayout();
            this.gbAppearance.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudFlashIconCount)).BeginInit();
            this.tabPage6.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.tabPage7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudHistoryMaxItems)).BeginInit();
            this.tabPage8.SuspendLayout();
            this.gbBackupRestoreUploaders.SuspendLayout();
            this.gbBackupRestoreOutputs.SuspendLayout();
            this.gbSettingsExportImport.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlpMain
            // 
            this.tlpMain.ColumnCount = 2;
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 166F));
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMain.Controls.Add(this.tvOptions, 0, 0);
            this.tlpMain.Controls.Add(this.tcMain, 1, 0);
            this.tlpMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpMain.Location = new System.Drawing.Point(0, 0);
            this.tlpMain.Name = "tlpMain";
            this.tlpMain.RowCount = 1;
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMain.Size = new System.Drawing.Size(792, 526);
            this.tlpMain.TabIndex = 0;
            // 
            // tvOptions
            // 
            this.tvOptions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvOptions.Location = new System.Drawing.Point(3, 3);
            this.tvOptions.Name = "tvOptions";
            treeNode9.Name = "tnGeneral";
            treeNode9.Text = "General";
            treeNode10.Name = "tnWorkflow";
            treeNode10.Text = "Workflow";
            treeNode11.Name = "Node1";
            treeNode11.Text = "Folder Listing";
            treeNode12.Name = "tnCapture";
            treeNode12.Text = "Capture";
            treeNode13.Name = "tnEffects";
            treeNode13.Text = "Effects";
            treeNode14.Name = "Node0";
            treeNode14.Text = "History";
            treeNode15.Name = "tnSaving";
            treeNode15.Text = "Saving";
            treeNode16.Name = "Node0";
            treeNode16.Text = "Backup & Restore";
            this.tvOptions.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode9,
            treeNode10,
            treeNode12,
            treeNode13,
            treeNode15,
            treeNode16});
            this.tvOptions.Size = new System.Drawing.Size(160, 520);
            this.tvOptions.TabIndex = 0;
            // 
            // tcMain
            // 
            this.tcMain.Controls.Add(this.tabPage1);
            this.tcMain.Controls.Add(this.tabPage2);
            this.tcMain.Controls.Add(this.tabPage3);
            this.tcMain.Controls.Add(this.tabPage6);
            this.tcMain.Controls.Add(this.tabPage4);
            this.tcMain.Controls.Add(this.tabPage5);
            this.tcMain.Controls.Add(this.tabPage7);
            this.tcMain.Controls.Add(this.tabPage8);
            this.tcMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcMain.Location = new System.Drawing.Point(169, 3);
            this.tcMain.Name = "tcMain";
            this.tcMain.SelectedIndex = 0;
            this.tcMain.Size = new System.Drawing.Size(620, 520);
            this.tcMain.TabIndex = 1;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.gbMisc);
            this.tabPage1.Controls.Add(this.gbUpdates);
            this.tabPage1.Controls.Add(this.gbWindowButtons);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(612, 494);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "General";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // gbMisc
            // 
            this.gbMisc.BackColor = System.Drawing.Color.Transparent;
            this.gbMisc.Controls.Add(this.chkShellExt);
            this.gbMisc.Controls.Add(this.chkWindows7TaskbarIntegration);
            this.gbMisc.Controls.Add(this.cbAutoSaveSettings);
            this.gbMisc.Controls.Add(this.cbShowHelpBalloonTips);
            this.gbMisc.Controls.Add(this.chkOpenMainWindow);
            this.gbMisc.Controls.Add(this.chkStartWin);
            this.gbMisc.Location = new System.Drawing.Point(11, 15);
            this.gbMisc.Name = "gbMisc";
            this.gbMisc.Size = new System.Drawing.Size(589, 104);
            this.gbMisc.TabIndex = 3;
            this.gbMisc.TabStop = false;
            this.gbMisc.Text = "Program";
            // 
            // chkShellExt
            // 
            this.chkShellExt.AutoSize = true;
            this.chkShellExt.Location = new System.Drawing.Point(304, 48);
            this.chkShellExt.Name = "chkShellExt";
            this.chkShellExt.Size = new System.Drawing.Size(270, 17);
            this.chkShellExt.TabIndex = 3;
            this.chkShellExt.Text = "Show \"Open using ZScreen\" in Shell Context Menu";
            this.chkShellExt.UseVisualStyleBackColor = true;
            this.chkShellExt.CheckedChanged += new System.EventHandler(this.chkShellExt_CheckedChanged);
            // 
            // chkWindows7TaskbarIntegration
            // 
            this.chkWindows7TaskbarIntegration.AutoSize = true;
            this.chkWindows7TaskbarIntegration.Location = new System.Drawing.Point(304, 24);
            this.chkWindows7TaskbarIntegration.Name = "chkWindows7TaskbarIntegration";
            this.chkWindows7TaskbarIntegration.Size = new System.Drawing.Size(173, 17);
            this.chkWindows7TaskbarIntegration.TabIndex = 1;
            this.chkWindows7TaskbarIntegration.Text = "Windows 7 &Taskbar integration";
            this.chkWindows7TaskbarIntegration.UseVisualStyleBackColor = true;
            this.chkWindows7TaskbarIntegration.CheckedChanged += new System.EventHandler(this.chkWindows7TaskbarIntegration_CheckedChanged);
            // 
            // cbAutoSaveSettings
            // 
            this.cbAutoSaveSettings.AutoSize = true;
            this.cbAutoSaveSettings.Location = new System.Drawing.Point(304, 72);
            this.cbAutoSaveSettings.Name = "cbAutoSaveSettings";
            this.cbAutoSaveSettings.Size = new System.Drawing.Size(245, 17);
            this.cbAutoSaveSettings.TabIndex = 5;
            this.cbAutoSaveSettings.Text = "Save settings when ZScreen minimizing to tray";
            this.cbAutoSaveSettings.UseVisualStyleBackColor = true;
            this.cbAutoSaveSettings.CheckedChanged += new System.EventHandler(this.cbAutoSaveSettings_CheckedChanged);
            // 
            // cbShowHelpBalloonTips
            // 
            this.cbShowHelpBalloonTips.AutoSize = true;
            this.cbShowHelpBalloonTips.Location = new System.Drawing.Point(16, 72);
            this.cbShowHelpBalloonTips.Name = "cbShowHelpBalloonTips";
            this.cbShowHelpBalloonTips.Size = new System.Drawing.Size(156, 17);
            this.cbShowHelpBalloonTips.TabIndex = 4;
            this.cbShowHelpBalloonTips.Text = "Show Help via Balloon Tips";
            this.cbShowHelpBalloonTips.UseVisualStyleBackColor = true;
            this.cbShowHelpBalloonTips.CheckedChanged += new System.EventHandler(this.cbShowHelpBalloonTips_CheckedChanged);
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
            // gbUpdates
            // 
            this.gbUpdates.Controls.Add(this.cboReleaseChannel);
            this.gbUpdates.Controls.Add(this.lblUpdateInfo);
            this.gbUpdates.Controls.Add(this.btnCheckUpdate);
            this.gbUpdates.Controls.Add(this.chkCheckUpdates);
            this.gbUpdates.Location = new System.Drawing.Point(11, 232);
            this.gbUpdates.Name = "gbUpdates";
            this.gbUpdates.Size = new System.Drawing.Size(589, 128);
            this.gbUpdates.TabIndex = 5;
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
            this.cboReleaseChannel.TabIndex = 1;
            this.cboReleaseChannel.SelectedIndexChanged += new System.EventHandler(this.cboReleaseChannel_SelectedIndexChanged);
            // 
            // lblUpdateInfo
            // 
            this.lblUpdateInfo.AutoSize = true;
            this.lblUpdateInfo.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblUpdateInfo.Location = new System.Drawing.Point(24, 56);
            this.lblUpdateInfo.Name = "lblUpdateInfo";
            this.lblUpdateInfo.Size = new System.Drawing.Size(116, 16);
            this.lblUpdateInfo.TabIndex = 3;
            this.lblUpdateInfo.Text = "Update information";
            // 
            // btnCheckUpdate
            // 
            this.btnCheckUpdate.Location = new System.Drawing.Point(368, 24);
            this.btnCheckUpdate.Name = "btnCheckUpdate";
            this.btnCheckUpdate.Size = new System.Drawing.Size(104, 24);
            this.btnCheckUpdate.TabIndex = 2;
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
            this.chkCheckUpdates.TabIndex = 0;
            this.chkCheckUpdates.Text = "Automatically check updates at startup for";
            this.chkCheckUpdates.UseVisualStyleBackColor = true;
            this.chkCheckUpdates.CheckedChanged += new System.EventHandler(this.cbCheckUpdates_CheckedChanged);
            // 
            // gbWindowButtons
            // 
            this.gbWindowButtons.Controls.Add(this.cboCloseButtonAction);
            this.gbWindowButtons.Controls.Add(this.cboMinimizeButtonAction);
            this.gbWindowButtons.Controls.Add(this.lblCloseButtonAction);
            this.gbWindowButtons.Controls.Add(this.lblMinimizeButtonAction);
            this.gbWindowButtons.Location = new System.Drawing.Point(8, 128);
            this.gbWindowButtons.Name = "gbWindowButtons";
            this.gbWindowButtons.Size = new System.Drawing.Size(592, 89);
            this.gbWindowButtons.TabIndex = 4;
            this.gbWindowButtons.TabStop = false;
            this.gbWindowButtons.Text = "Windows Buttons Behavior";
            // 
            // cboCloseButtonAction
            // 
            this.cboCloseButtonAction.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboCloseButtonAction.FormattingEnabled = true;
            this.cboCloseButtonAction.Location = new System.Drawing.Point(136, 48);
            this.cboCloseButtonAction.Name = "cboCloseButtonAction";
            this.cboCloseButtonAction.Size = new System.Drawing.Size(144, 21);
            this.cboCloseButtonAction.TabIndex = 3;
            this.cboCloseButtonAction.SelectedIndexChanged += new System.EventHandler(this.cbCloseButtonAction_SelectedIndexChanged);
            // 
            // cboMinimizeButtonAction
            // 
            this.cboMinimizeButtonAction.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboMinimizeButtonAction.FormattingEnabled = true;
            this.cboMinimizeButtonAction.Location = new System.Drawing.Point(136, 20);
            this.cboMinimizeButtonAction.Name = "cboMinimizeButtonAction";
            this.cboMinimizeButtonAction.Size = new System.Drawing.Size(144, 21);
            this.cboMinimizeButtonAction.TabIndex = 1;
            this.cboMinimizeButtonAction.SelectedIndexChanged += new System.EventHandler(this.cbMinimizeButtonAction_SelectedIndexChanged);
            // 
            // lblCloseButtonAction
            // 
            this.lblCloseButtonAction.AutoSize = true;
            this.lblCloseButtonAction.Location = new System.Drawing.Point(32, 52);
            this.lblCloseButtonAction.Name = "lblCloseButtonAction";
            this.lblCloseButtonAction.Size = new System.Drawing.Size(101, 13);
            this.lblCloseButtonAction.TabIndex = 2;
            this.lblCloseButtonAction.Text = "Close button action:";
            // 
            // lblMinimizeButtonAction
            // 
            this.lblMinimizeButtonAction.AutoSize = true;
            this.lblMinimizeButtonAction.Location = new System.Drawing.Point(16, 24);
            this.lblMinimizeButtonAction.Name = "lblMinimizeButtonAction";
            this.lblMinimizeButtonAction.Size = new System.Drawing.Size(115, 13);
            this.lblMinimizeButtonAction.TabIndex = 0;
            this.lblMinimizeButtonAction.Text = "Minimize button action:";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.gbOptionsBalloonTip);
            this.tabPage2.Controls.Add(this.gbAppearance);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(612, 494);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Workflow";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // gbOptionsBalloonTip
            // 
            this.gbOptionsBalloonTip.Controls.Add(this.chkBalloonTipOpenLink);
            this.gbOptionsBalloonTip.Controls.Add(this.cbShowUploadDuration);
            this.gbOptionsBalloonTip.Location = new System.Drawing.Point(11, 167);
            this.gbOptionsBalloonTip.Name = "gbOptionsBalloonTip";
            this.gbOptionsBalloonTip.Size = new System.Drawing.Size(581, 80);
            this.gbOptionsBalloonTip.TabIndex = 3;
            this.gbOptionsBalloonTip.TabStop = false;
            this.gbOptionsBalloonTip.Text = "Balloon Tip Options";
            // 
            // chkBalloonTipOpenLink
            // 
            this.chkBalloonTipOpenLink.AutoSize = true;
            this.chkBalloonTipOpenLink.Location = new System.Drawing.Point(16, 24);
            this.chkBalloonTipOpenLink.Name = "chkBalloonTipOpenLink";
            this.chkBalloonTipOpenLink.Size = new System.Drawing.Size(189, 17);
            this.chkBalloonTipOpenLink.TabIndex = 0;
            this.chkBalloonTipOpenLink.Text = "Open URL/File on balloon tip click";
            this.chkBalloonTipOpenLink.UseVisualStyleBackColor = true;
            this.chkBalloonTipOpenLink.CheckedChanged += new System.EventHandler(this.chkBalloonTipOpenLink_CheckedChanged);
            // 
            // cbShowUploadDuration
            // 
            this.cbShowUploadDuration.AutoSize = true;
            this.cbShowUploadDuration.Location = new System.Drawing.Point(16, 48);
            this.cbShowUploadDuration.Name = "cbShowUploadDuration";
            this.cbShowUploadDuration.Size = new System.Drawing.Size(191, 17);
            this.cbShowUploadDuration.TabIndex = 1;
            this.cbShowUploadDuration.Text = "Show upload duration in balloon tip";
            this.cbShowUploadDuration.UseVisualStyleBackColor = true;
            this.cbShowUploadDuration.CheckedChanged += new System.EventHandler(this.cbShowUploadDuration_CheckedChanged);
            // 
            // gbAppearance
            // 
            this.gbAppearance.BackColor = System.Drawing.Color.Transparent;
            this.gbAppearance.Controls.Add(this.chkShowPopup);
            this.gbAppearance.Controls.Add(this.chkTwitterEnable);
            this.gbAppearance.Controls.Add(this.cbCompleteSound);
            this.gbAppearance.Controls.Add(this.chkCaptureFallback);
            this.gbAppearance.Controls.Add(this.lblTrayFlash);
            this.gbAppearance.Controls.Add(this.nudFlashIconCount);
            this.gbAppearance.Location = new System.Drawing.Point(11, 7);
            this.gbAppearance.Name = "gbAppearance";
            this.gbAppearance.Size = new System.Drawing.Size(581, 152);
            this.gbAppearance.TabIndex = 2;
            this.gbAppearance.TabStop = false;
            this.gbAppearance.Text = "After completing a task";
            // 
            // chkShowPopup
            // 
            this.chkShowPopup.AutoSize = true;
            this.chkShowPopup.Location = new System.Drawing.Point(16, 120);
            this.chkShowPopup.Name = "chkShowPopup";
            this.chkShowPopup.Size = new System.Drawing.Size(250, 17);
            this.chkShowPopup.TabIndex = 5;
            this.chkShowPopup.Text = "Show balloon tip after upload/task is completed";
            this.chkShowPopup.UseVisualStyleBackColor = true;
            this.chkShowPopup.CheckedChanged += new System.EventHandler(this.cbShowPopup_CheckedChanged);
            // 
            // chkTwitterEnable
            // 
            this.chkTwitterEnable.AutoSize = true;
            this.chkTwitterEnable.Location = new System.Drawing.Point(16, 72);
            this.chkTwitterEnable.Name = "chkTwitterEnable";
            this.chkTwitterEnable.Size = new System.Drawing.Size(202, 17);
            this.chkTwitterEnable.TabIndex = 2;
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
            this.cbCompleteSound.TabIndex = 0;
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
            this.chkCaptureFallback.TabIndex = 1;
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
            // tabPage3
            // 
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(612, 494);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "tabPage3";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // tabPage6
            // 
            this.tabPage6.Controls.Add(this.pgIndexer);
            this.tabPage6.Location = new System.Drawing.Point(4, 22);
            this.tabPage6.Name = "tabPage6";
            this.tabPage6.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage6.Size = new System.Drawing.Size(612, 494);
            this.tabPage6.TabIndex = 5;
            this.tabPage6.Text = "Directory Indexer";
            this.tabPage6.UseVisualStyleBackColor = true;
            // 
            // pgIndexer
            // 
            this.pgIndexer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pgIndexer.Location = new System.Drawing.Point(3, 3);
            this.pgIndexer.Name = "pgIndexer";
            this.pgIndexer.Size = new System.Drawing.Size(606, 488);
            this.pgIndexer.TabIndex = 0;
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.pgWorkflowImageEffects);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(612, 494);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "Effects";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // tabPage5
            // 
            this.tabPage5.Location = new System.Drawing.Point(4, 22);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage5.Size = new System.Drawing.Size(612, 494);
            this.tabPage5.TabIndex = 4;
            this.tabPage5.Text = "Saving";
            this.tabPage5.UseVisualStyleBackColor = true;
            // 
            // tabPage7
            // 
            this.tabPage7.Controls.Add(this.btnClearHistory);
            this.tabPage7.Controls.Add(this.label1);
            this.tabPage7.Controls.Add(this.cbHistorySave);
            this.tabPage7.Controls.Add(this.nudHistoryMaxItems);
            this.tabPage7.Location = new System.Drawing.Point(4, 22);
            this.tabPage7.Name = "tabPage7";
            this.tabPage7.Size = new System.Drawing.Size(612, 494);
            this.tabPage7.TabIndex = 6;
            this.tabPage7.Text = "History";
            this.tabPage7.UseVisualStyleBackColor = true;
            // 
            // btnClearHistory
            // 
            this.btnClearHistory.Location = new System.Drawing.Point(16, 77);
            this.btnClearHistory.Name = "btnClearHistory";
            this.btnClearHistory.Size = new System.Drawing.Size(136, 23);
            this.btnClearHistory.TabIndex = 7;
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
            this.label1.TabIndex = 4;
            this.label1.Text = "Maximum number of items in history";
            // 
            // cbHistorySave
            // 
            this.cbHistorySave.AutoSize = true;
            this.cbHistorySave.Location = new System.Drawing.Point(16, 45);
            this.cbHistorySave.Name = "cbHistorySave";
            this.cbHistorySave.Size = new System.Drawing.Size(232, 17);
            this.cbHistorySave.TabIndex = 6;
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
            this.nudHistoryMaxItems.TabIndex = 5;
            this.nudHistoryMaxItems.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.nudHistoryMaxItems.ValueChanged += new System.EventHandler(this.nudHistoryMaxItems_ValueChanged);
            // 
            // tabPage8
            // 
            this.tabPage8.Controls.Add(this.gbBackupRestoreUploaders);
            this.tabPage8.Controls.Add(this.gbBackupRestoreOutputs);
            this.tabPage8.Controls.Add(this.gbSettingsExportImport);
            this.tabPage8.Location = new System.Drawing.Point(4, 22);
            this.tabPage8.Name = "tabPage8";
            this.tabPage8.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage8.Size = new System.Drawing.Size(612, 494);
            this.tabPage8.TabIndex = 7;
            this.tabPage8.Text = "Backup && Restore";
            this.tabPage8.UseVisualStyleBackColor = true;
            // 
            // gbBackupRestoreUploaders
            // 
            this.gbBackupRestoreUploaders.Controls.Add(this.btnUploadersConfigExport);
            this.gbBackupRestoreUploaders.Controls.Add(this.btnUploadersConfigImport);
            this.gbBackupRestoreUploaders.Location = new System.Drawing.Point(16, 280);
            this.gbBackupRestoreUploaders.Name = "gbBackupRestoreUploaders";
            this.gbBackupRestoreUploaders.Size = new System.Drawing.Size(216, 96);
            this.gbBackupRestoreUploaders.TabIndex = 5;
            this.gbBackupRestoreUploaders.TabStop = false;
            this.gbBackupRestoreUploaders.Text = "Uploaders Config";
            // 
            // btnUploadersConfigExport
            // 
            this.btnUploadersConfigExport.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnUploadersConfigExport.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnUploadersConfigExport.Location = new System.Drawing.Point(16, 56);
            this.btnUploadersConfigExport.Name = "btnUploadersConfigExport";
            this.btnUploadersConfigExport.Size = new System.Drawing.Size(184, 24);
            this.btnUploadersConfigExport.TabIndex = 1;
            this.btnUploadersConfigExport.Text = "Export Uploaders Configuration...";
            this.btnUploadersConfigExport.UseVisualStyleBackColor = true;
            this.btnUploadersConfigExport.Click += new System.EventHandler(this.btnUploadersConfigExport_Click);
            // 
            // btnUploadersConfigImport
            // 
            this.btnUploadersConfigImport.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnUploadersConfigImport.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnUploadersConfigImport.Location = new System.Drawing.Point(16, 24);
            this.btnUploadersConfigImport.Name = "btnUploadersConfigImport";
            this.btnUploadersConfigImport.Size = new System.Drawing.Size(184, 24);
            this.btnUploadersConfigImport.TabIndex = 0;
            this.btnUploadersConfigImport.Text = "Import Uploaders Configuration...";
            this.btnUploadersConfigImport.UseVisualStyleBackColor = true;
            this.btnUploadersConfigImport.Click += new System.EventHandler(this.btnUploadersConfigImport_Click);
            // 
            // gbBackupRestoreOutputs
            // 
            this.gbBackupRestoreOutputs.Controls.Add(this.btnOutputsConfigExport);
            this.gbBackupRestoreOutputs.Controls.Add(this.btnOutputsConfigImport);
            this.gbBackupRestoreOutputs.Location = new System.Drawing.Point(16, 168);
            this.gbBackupRestoreOutputs.Name = "gbBackupRestoreOutputs";
            this.gbBackupRestoreOutputs.Size = new System.Drawing.Size(200, 96);
            this.gbBackupRestoreOutputs.TabIndex = 4;
            this.gbBackupRestoreOutputs.TabStop = false;
            this.gbBackupRestoreOutputs.Text = "Workflow Settings";
            // 
            // btnOutputsConfigExport
            // 
            this.btnOutputsConfigExport.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnOutputsConfigExport.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnOutputsConfigExport.Location = new System.Drawing.Point(16, 56);
            this.btnOutputsConfigExport.Name = "btnOutputsConfigExport";
            this.btnOutputsConfigExport.Size = new System.Drawing.Size(168, 24);
            this.btnOutputsConfigExport.TabIndex = 1;
            this.btnOutputsConfigExport.Text = "Export Workflow Configuration...";
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
            this.btnOutputsConfigImport.TabIndex = 0;
            this.btnOutputsConfigImport.Text = "Import Workflow Configuration...";
            this.btnOutputsConfigImport.UseVisualStyleBackColor = true;
            this.btnOutputsConfigImport.Click += new System.EventHandler(this.btnOutputsConfigImport_Click);
            // 
            // gbSettingsExportImport
            // 
            this.gbSettingsExportImport.BackColor = System.Drawing.Color.Transparent;
            this.gbSettingsExportImport.Controls.Add(this.btnSettingsDefault);
            this.gbSettingsExportImport.Controls.Add(this.btnSettingsExport);
            this.gbSettingsExportImport.Controls.Add(this.btnSettingsImport);
            this.gbSettingsExportImport.Location = new System.Drawing.Point(16, 16);
            this.gbSettingsExportImport.Name = "gbSettingsExportImport";
            this.gbSettingsExportImport.Size = new System.Drawing.Size(136, 136);
            this.gbSettingsExportImport.TabIndex = 3;
            this.gbSettingsExportImport.TabStop = false;
            this.gbSettingsExportImport.Text = "Application Settings";
            // 
            // btnSettingsDefault
            // 
            this.btnSettingsDefault.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnSettingsDefault.Location = new System.Drawing.Point(16, 88);
            this.btnSettingsDefault.Name = "btnSettingsDefault";
            this.btnSettingsDefault.Size = new System.Drawing.Size(104, 24);
            this.btnSettingsDefault.TabIndex = 2;
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
            // pgWorkflowImageEffects
            // 
            this.pgWorkflowImageEffects.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pgWorkflowImageEffects.Location = new System.Drawing.Point(3, 3);
            this.pgWorkflowImageEffects.Name = "pgWorkflowImageEffects";
            this.pgWorkflowImageEffects.Size = new System.Drawing.Size(606, 488);
            this.pgWorkflowImageEffects.TabIndex = 1;
            // 
            // ZScreenOptionsUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(792, 526);
            this.Controls.Add(this.tlpMain);
            this.MinimumSize = new System.Drawing.Size(800, 560);
            this.Name = "ZScreenOptionsUI";
            this.Text = "Options";
            this.Load += new System.EventHandler(this.ZScreenOptionsUI_Load);
            this.Shown += new System.EventHandler(this.ZScreenOptionsUI_Shown);
            this.Resize += new System.EventHandler(this.ZScreenOptionsUI_Resize);
            this.tlpMain.ResumeLayout(false);
            this.tcMain.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.gbMisc.ResumeLayout(false);
            this.gbMisc.PerformLayout();
            this.gbUpdates.ResumeLayout(false);
            this.gbUpdates.PerformLayout();
            this.gbWindowButtons.ResumeLayout(false);
            this.gbWindowButtons.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.gbOptionsBalloonTip.ResumeLayout(false);
            this.gbOptionsBalloonTip.PerformLayout();
            this.gbAppearance.ResumeLayout(false);
            this.gbAppearance.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudFlashIconCount)).EndInit();
            this.tabPage6.ResumeLayout(false);
            this.tabPage4.ResumeLayout(false);
            this.tabPage7.ResumeLayout(false);
            this.tabPage7.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudHistoryMaxItems)).EndInit();
            this.tabPage8.ResumeLayout(false);
            this.gbBackupRestoreUploaders.ResumeLayout(false);
            this.gbBackupRestoreOutputs.ResumeLayout(false);
            this.gbSettingsExportImport.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        protected System.Windows.Forms.TableLayoutPanel tlpMain;
        protected System.Windows.Forms.TreeView tvOptions;
        protected System.Windows.Forms.TabControl tcMain;
        protected System.Windows.Forms.TabPage tabPage1;
        protected System.Windows.Forms.TabPage tabPage2;
        protected System.Windows.Forms.TabPage tabPage3;
        protected System.Windows.Forms.TabPage tabPage4;
        protected System.Windows.Forms.TabPage tabPage5;
        private System.Windows.Forms.TabPage tabPage6;
        private System.Windows.Forms.PropertyGrid pgIndexer;
        private System.Windows.Forms.TabPage tabPage7;
        private System.Windows.Forms.Button btnClearHistory;
        private System.Windows.Forms.Label label1;
        internal System.Windows.Forms.CheckBox cbHistorySave;
        internal System.Windows.Forms.NumericUpDown nudHistoryMaxItems;
        private System.Windows.Forms.TabPage tabPage8;
        private System.Windows.Forms.GroupBox gbBackupRestoreUploaders;
        internal System.Windows.Forms.Button btnUploadersConfigExport;
        internal System.Windows.Forms.Button btnUploadersConfigImport;
        private System.Windows.Forms.GroupBox gbBackupRestoreOutputs;
        internal System.Windows.Forms.Button btnOutputsConfigExport;
        internal System.Windows.Forms.Button btnOutputsConfigImport;
        internal System.Windows.Forms.GroupBox gbSettingsExportImport;
        internal System.Windows.Forms.Button btnSettingsDefault;
        internal System.Windows.Forms.Button btnSettingsExport;
        internal System.Windows.Forms.Button btnSettingsImport;
        internal System.Windows.Forms.GroupBox gbMisc;
        private System.Windows.Forms.CheckBox chkShellExt;
        private System.Windows.Forms.CheckBox chkWindows7TaskbarIntegration;
        private System.Windows.Forms.CheckBox cbAutoSaveSettings;
        internal System.Windows.Forms.CheckBox cbShowHelpBalloonTips;
        internal System.Windows.Forms.CheckBox chkOpenMainWindow;
        internal System.Windows.Forms.CheckBox chkStartWin;
        internal System.Windows.Forms.GroupBox gbUpdates;
        private System.Windows.Forms.ComboBox cboReleaseChannel;
        internal System.Windows.Forms.Label lblUpdateInfo;
        internal System.Windows.Forms.Button btnCheckUpdate;
        internal System.Windows.Forms.CheckBox chkCheckUpdates;
        private System.Windows.Forms.GroupBox gbWindowButtons;
        private System.Windows.Forms.ComboBox cboCloseButtonAction;
        private System.Windows.Forms.ComboBox cboMinimizeButtonAction;
        private System.Windows.Forms.Label lblCloseButtonAction;
        private System.Windows.Forms.Label lblMinimizeButtonAction;
        private System.Windows.Forms.GroupBox gbOptionsBalloonTip;
        internal System.Windows.Forms.CheckBox chkBalloonTipOpenLink;
        internal System.Windows.Forms.CheckBox cbShowUploadDuration;
        internal System.Windows.Forms.GroupBox gbAppearance;
        internal System.Windows.Forms.CheckBox chkShowPopup;
        private System.Windows.Forms.CheckBox chkTwitterEnable;
        internal System.Windows.Forms.CheckBox cbCompleteSound;
        internal System.Windows.Forms.CheckBox chkCaptureFallback;
        internal System.Windows.Forms.Label lblTrayFlash;
        internal System.Windows.Forms.NumericUpDown nudFlashIconCount;
        internal System.Windows.Forms.PropertyGrid pgWorkflowImageEffects;

    }
}
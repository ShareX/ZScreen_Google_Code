using System;
using System.Diagnostics;
using System.Drawing.Drawing2D;
using System.IO;
using System.Windows.Forms;
using HelpersLib;
using Microsoft.WindowsAPICodePack.Shell;
using Microsoft.WindowsAPICodePack.Taskbar;
using UploadersLib.HelperClasses;
using ZScreenGUI.Properties;
using ZScreenLib;
using ZSS.UpdateCheckerLib;

namespace ZScreenGUI
{
    public partial class ZScreen : HotkeyForm
    {
        private void ZScreen_Preconfig()
        {
            // Tab Image List
            tabImageList.ColorDepth = ColorDepth.Depth32Bit;
            tabImageList.Images.Add("application_form", Resources.application_form);
            tabImageList.Images.Add("server", Resources.server);
            tabImageList.Images.Add("keyboard", Resources.keyboard);
            tabImageList.Images.Add("monitor", Resources.monitor);
            tabImageList.Images.Add("picture_edit", Resources.picture_edit);
            tabImageList.Images.Add("comments", Resources.comments);
            tabImageList.Images.Add("application_edit", Resources.application_edit);
            tabImageList.Images.Add("wrench", Resources.wrench);
            tabImageList.Images.Add("info", Resources.info);
            tcMain.ImageList = tabImageList;
            tpMain.ImageKey = "application_form";
            tpHotkeys.ImageKey = "keyboard";
            tpMainInput.ImageKey = "monitor";
            tpMainActions.ImageKey = "picture_edit";
            tpOptions.ImageKey = "application_edit";
            tpAdvanced.ImageKey = "wrench";

            // Notes
            lblNoteActions.Text = string.Format("Enable \"{0}\" in the {1} tab for functionality.", chkPerformActions.Text, tpMain.Text);

            // Options - Proxy
            ucProxyAccounts.btnAdd.Click += new EventHandler(ProxyAccountsAddButton_Click);
            ucProxyAccounts.btnRemove.Click += new EventHandler(ProxyAccountsRemoveButton_Click);
            ucProxyAccounts.btnTest.Click += new EventHandler(ProxyAccountTestButton_Click);
            ucProxyAccounts.AccountsList.SelectedIndexChanged += new EventHandler(ProxyAccountsList_SelectedIndexChanged);

            // Watermark Codes Menu
            codesMenu.AutoClose = false;
            codesMenu.Font = new XFont("Lucida Console", 8);
            codesMenu.Opacity = 0.8;
            codesMenu.ShowImageMargin = false;

            niTray.BalloonTipClicked += new EventHandler(niTray_BalloonTipClicked);
        }

        private void ZScreen_ConfigGUI()
        {
            Engine.MyLogger.WriteLine("Configuring ZScreen GUI via " + new StackFrame(1).GetMethod().Name);

            DisableFeatures();

            pgAppSettings.SelectedObject = Engine.AppConf;
            pgAppConfig.SelectedObject = Engine.conf;
            pgWorkflow.SelectedObject = Engine.MyWorkflow;
            pgIndexer.SelectedObject = Engine.conf.IndexerConfig;

            ZScreen_ConfigGUI_Form();
            ZScreen_ConfigGUI_TrayMenu();

            ZScreen_ConfigGUI_Main();
            ZScreen_ConfigGUI_Hotkeys();
            ZScreen_ConfigGUI_Capture();
            ZScreen_ConfigGUI_Actions();
            ZScreen_ConfigGUI_Options();
            ZScreen_ConfigGUI_Options_Paths();
            ZScreen_ConfigGUI_CaptureWebpage();
            ZScreen_ConfigGUI_Options_History();
        }

        private void ZScreen_ConfigGUI_Form()
        {
            if (Engine.conf.LockFormSize)
            {
                if (this.FormBorderStyle != FormBorderStyle.FixedSingle)
                {
                    this.FormBorderStyle = FormBorderStyle.FixedSingle;
                    this.Size = this.MinimumSize;
                }
            }
            else
            {
                if (this.FormBorderStyle != FormBorderStyle.Sizable)
                {
                    this.FormBorderStyle = FormBorderStyle.Sizable;
                    this.Size = this.MinimumSize;
                }
            }
        }

        private void ZScreen_ConfigGUI_Main()
        {
            DestSelectorHelper dsh = new DestSelectorHelper(ucDestOptions);
            dsh.AddEnumDestToMenuWithConfigSettings();

            chkManualNaming.Checked = Engine.conf.PromptForOutputs;
            chkShowCursor.Checked = Engine.CoreConf.ShowCursor;
            chkShowUploadResults.Checked = Engine.conf.ShowUploadResultsWindow;
        }

        private void ZScreen_ConfigGUI_Hotkeys()
        {
            UpdateHotkeysDGV();
        }

        private void ZScreen_ConfigGUI_Capture()
        {
            ZScreen_ConfigGUI_Capture_CropShot();

            // Selected Window
            if (cbSelectedWindowStyle.Items.Count == 0)
            {
                cbSelectedWindowStyle.Items.AddRange(typeof(RegionStyles).GetDescriptions());
            }

            cbSelectedWindowStyle.SelectedIndex = (int)Engine.conf.SelectedWindowRegionStyles;
            cbSelectedWindowRectangleInfo.Checked = Engine.conf.SelectedWindowRectangleInfo;
            cbSelectedWindowRuler.Checked = Engine.conf.SelectedWindowRuler;
            pbSelectedWindowBorderColor.BackColor = Engine.conf.SelectedWindowBorderArgb;
            nudSelectedWindowBorderSize.Value = Engine.conf.SelectedWindowBorderSize;
            cbSelectedWindowDynamicBorderColor.Checked = Engine.conf.SelectedWindowDynamicBorderColor;
            nudSelectedWindowRegionInterval.Value = Engine.conf.SelectedWindowRegionInterval;
            nudSelectedWindowRegionStep.Value = Engine.conf.SelectedWindowRegionStep;
            nudSelectedWindowHueRange.Value = Engine.conf.SelectedWindowHueRange;
            chkSelectedWindowCaptureObjects.Checked = Engine.conf.SelectedWindowCaptureObjects;

            // Active Window
            chkActiveWindowPreferDWM.Checked = Engine.CoreConf.ActiveWindowPreferDWM;
            chkSelectedWindowCleanBackground.Checked = Engine.CoreConf.ActiveWindowClearBackground;
            chkSelectedWindowCleanTransparentCorners.Checked = Engine.CoreConf.ActiveWindowCleanTransparentCorners;
            chkSelectedWindowIncludeShadow.Checked = Engine.CoreConf.ActiveWindowIncludeShadows;
            chkActiveWindowTryCaptureChildren.Checked = Engine.CoreConf.ActiveWindowTryCaptureChildren;
            chkSelectedWindowShowCheckers.Checked = Engine.CoreConf.ActiveWindowShowCheckers;

            // Freehand Crop Shot
            cbFreehandCropShowHelpText.Checked = Engine.conf.FreehandCropShowHelpText;
            cbFreehandCropAutoUpload.Checked = Engine.conf.FreehandCropAutoUpload;
            cbFreehandCropAutoClose.Checked = Engine.conf.FreehandCropAutoClose;
            cbFreehandCropShowRectangleBorder.Checked = Engine.conf.FreehandCropShowRectangleBorder;
            pgSurfaceConfig.SelectedObject = Engine.conf.SurfaceConfig;

            // Naming Conventions
            txtActiveWindow.Text = Engine.CoreConf.ActiveWindowPattern;
            txtEntireScreen.Text = Engine.CoreConf.EntireScreenPattern;
            txtImagesFolderPattern.Text = Engine.CoreConf.SaveFolderPattern;
            nudMaxNameLength.Value = Engine.CoreConf.MaxNameLength;

            ZScreen_ConfigGUI_Options_Watermark();
            ZScreen_ConfigGUI_Options_ImageSettings();
        }

        private void ZScreen_ConfigGUI_Capture_CropShot()
        {
            // Crop Region Settings
            if (chkCropStyle.Items.Count == 0)
            {
                chkCropStyle.Items.AddRange(typeof(RegionStyles).GetDescriptions());
            }
            chkCropStyle.SelectedIndex = (int)Engine.conf.CropRegionStyles;
            chkRegionRectangleInfo.Checked = Engine.conf.CropRegionRectangleInfo;
            chkRegionHotkeyInfo.Checked = Engine.conf.CropRegionHotkeyInfo;

            // Crosshair Settings
            chkCropDynamicCrosshair.Checked = Engine.conf.CropDynamicCrosshair;
            nudCropCrosshairInterval.Value = Engine.conf.CropInterval;
            nudCropCrosshairStep.Value = Engine.conf.CropStep;
            nudCrosshairLineCount.Value = Engine.conf.CrosshairLineCount;
            nudCrosshairLineSize.Value = Engine.conf.CrosshairLineSize;
            pbCropCrosshairColor.BackColor = Engine.conf.CropCrosshairArgb;
            chkCropShowBigCross.Checked = Engine.conf.CropShowBigCross;
            chkCropShowMagnifyingGlass.Checked = Engine.conf.CropShowMagnifyingGlass;

            // Region Settings
            cbShowCropRuler.Checked = Engine.conf.CropShowRuler;
            cbCropDynamicBorderColor.Checked = Engine.conf.CropDynamicBorderColor;
            nudCropRegionInterval.Value = Engine.conf.CropRegionInterval;
            nudCropRegionStep.Value = Engine.conf.CropRegionStep;
            nudCropHueRange.Value = Engine.conf.CropHueRange;
            pbCropBorderColor.BackColor = Engine.conf.CropBorderArgb;
            nudCropBorderSize.Value = Engine.conf.CropBorderSize;
            cbCropShowGrids.Checked = Engine.conf.CropShowGrids;

            // Grid Mode Settings
            nudScreenshotDelay.Time = Engine.conf.ScreenshotDelayTimes;
            nudScreenshotDelay.Value = Engine.conf.ScreenshotDelayTime;
            cboCropGridMode.Checked = Engine.conf.CropGridToggle;
            nudCropGridWidth.Value = Engine.conf.CropGridSize.Width;
            nudCropGridHeight.Value = Engine.conf.CropGridSize.Height;
        }

        private void ZScreen_ConfigGUI_CaptureWebpage()
        {
            // Web Page Upload

            cbWebPageUseCustomSize.Checked = Engine.conf.WebPageUseCustomSize;
            txtWebPageWidth.Text = Engine.conf.WebPageWidth.ToString();
            txtWebPageHeight.Text = Engine.conf.WebPageHeight.ToString();
            cbWebPageAutoUpload.Checked = Engine.conf.WebPageAutoUpload;
        }

        private void ZScreen_ConfigGUI_Actions()
        {
            chkPerformActions.Checked = Engine.conf.PerformActions;
            tsmEditinImageSoftware.Checked = Engine.conf.PerformActions;

            if (Engine.conf.ActionsAppList.Count == 0)
            {
                Software editor = new Software(Engine.zImageAnnotator, Application.ExecutablePath, true, true);
                Engine.conf.ActionsAppList.Insert(0, editor);
            }
            else
            {
                Engine.conf.ActionsAppList.RemoveAll(x => string.IsNullOrEmpty(x.Path) || !File.Exists(x.Path));
            }

            ImageEditorHelper.FindImageEditors();

            lbSoftware.Items.Clear();

            foreach (Software app in Engine.conf.ActionsAppList)
            {
                if (!String.IsNullOrEmpty(app.Name))
                {
                    lbSoftware.Items.Add(app.Name, app.Enabled);
                }
            }

            RewriteImageEditorsRightClickMenu();

            int i;
            if (Engine.conf.ImageEditor != null && (i = lbSoftware.Items.IndexOf(Engine.conf.ImageEditor.Name)) != -1)
            {
                lbSoftware.SelectedIndex = i;
            }
            else if (lbSoftware.Items.Count > 0)
            {
                lbSoftware.SelectedIndex = 0;
            }

            // Text Editors

            if (Engine.conf.TextEditors.Count == 0)
            {
                Engine.conf.TextEditors.Add(new Software("Notepad", Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.System), "notepad.exe"), true));
            }
        }

        private void ZScreen_ConfigGUI_Options()
        {
            // General
            chkStartWin.Checked = RegistryHelper.CheckStartWithWindows();
            chkShellExt.Checked = RegistryHelper.CheckShellContextMenu();
            chkOpenMainWindow.Checked = Engine.AppConf.ShowMainWindow;
            chkShowTaskbar.Checked = Engine.AppConf.ShowInTaskbar;
            cbShowHelpBalloonTips.Checked = Engine.conf.ShowHelpBalloonTips;
            cbAutoSaveSettings.Checked = Engine.conf.AutoSaveSettings;
            chkWindows7TaskbarIntegration.Checked = TaskbarManager.IsPlatformSupported && Engine.AppConf.Windows7TaskbarIntegration;
            chkWindows7TaskbarIntegration.Enabled = TaskbarManager.IsPlatformSupported;
            // chkShowTaskbar.Enabled = !Engine.conf.Windows7TaskbarIntegration || !CoreHelpers.RunningOnWin7;
            chkTwitterEnable.Checked = Engine.conf.TwitterEnabled;

            // Interaction
            chkShortenURL.Checked = Engine.conf.ShortenUrlAfterUpload;
            nudFlashIconCount.Value = Engine.conf.FlashTrayCount;
            chkCaptureFallback.Checked = Engine.conf.CaptureEntireScreenOnError;
            cbShowPopup.Checked = Engine.conf.ShowBalloonTip;
            chkBalloonTipOpenLink.Checked = Engine.conf.BalloonTipOpenLink;
            cbShowUploadDuration.Checked = Engine.conf.ShowUploadDuration;
            cbCompleteSound.Checked = Engine.conf.CompleteSound;
            cbCloseDropBox.Checked = Engine.conf.CloseDropBox;

            // Proxy
            if (cboProxyConfig.Items.Count == 0)
            {
                cboProxyConfig.Items.AddRange(typeof(ProxyConfigType).GetDescriptions());
            }
            cboProxyConfig.SelectedIndex = (int)Engine.conf.ProxyConfig;

            ProxySetup(Engine.conf.ProxyList);
            if (ucProxyAccounts.AccountsList.Items.Count > 0)
            {
                ucProxyAccounts.AccountsList.SelectedIndex = Engine.conf.ProxySelected;
            }

            if (cboCloseButtonAction.Items.Count == 0)
            {
                cboMinimizeButtonAction.Items.AddRange(typeof(WindowButtonAction).GetDescriptions());
            }
            if (cboCloseButtonAction.Items.Count == 0)
            {
                cboCloseButtonAction.Items.AddRange(typeof(WindowButtonAction).GetDescriptions());
            }
            cboCloseButtonAction.SelectedIndex = (int)Engine.AppConf.WindowButtonActionClose;
            cboMinimizeButtonAction.SelectedIndex = (int)Engine.AppConf.WindowButtonActionMinimize;

            ttZScreen.Active = Engine.conf.ShowHelpBalloonTips;

            chkCheckUpdates.Checked = Engine.conf.CheckUpdates;
            if (cboReleaseChannel.Items.Count == 0)
            {
                cboReleaseChannel.Items.AddRange(typeof(ReleaseChannelType).GetDescriptions());
                cboReleaseChannel.SelectedIndex = (int)Engine.conf.ReleaseChannel;
            }
            chkDeleteLocal.Checked = Engine.conf.DeleteLocal;

            FolderWatcher zWatcher = new FolderWatcher(this);
            zWatcher.FolderPath = Engine.conf.FolderMonitorPath;
            if (Engine.conf.FolderMonitoring)
            {
                zWatcher.StartWatching();
            }
            else
            {
                zWatcher.StopWatching();
            }

            // Monitor Clipboard
            chkMonImages.Checked = Engine.conf.MonitorImages;
            chkMonText.Checked = Engine.conf.MonitorText;
            chkMonFiles.Checked = Engine.conf.MonitorFiles;
            chkMonUrls.Checked = Engine.conf.MonitorUrls;

            chkOverwriteFiles.Checked = Engine.CoreConf.OverwriteFiles;
        }

        private void ZScreen_ConfigGUI_Options_Paths()
        {
            Engine.InitializeDefaultFolderPaths(dirCreation: false);

            txtImagesDir.Text = Engine.ImagesDir;
            txtLogsDir.Text = Engine.LogsDir;

            if (Engine.AppConf.PreferSystemFolders)
            {
                txtRootFolder.Text = Engine.SettingsDir;
                gbRoot.Text = "Settings";
            }
            else
            {
                txtRootFolder.Text = Engine.AppConf.RootDir;
                gbRoot.Text = "Root";
            }

            btnRelocateRootDir.Enabled = !Engine.AppConf.PreferSystemFolders;
            gbRoot.Enabled = !Engine.IsPortable;
            gbImages.Enabled = !Engine.IsPortable;
            gbLogs.Enabled = !Engine.IsPortable;
        }

        private void ZScreen_ConfigGUI_Options_ImageSettings()
        {
            if (cboFileFormat.Items.Count == 0)
            {
                cboFileFormat.Items.AddRange(typeof(EImageFormat).GetDescriptions());
            }

            cboFileFormat.SelectedIndex = (int)Engine.MyWorkflow.ImageFormat;
            nudImageQuality.Value = Engine.MyWorkflow.ImageJPEGQuality;
            cbGIFQuality.SelectedIndex = (int)Engine.MyWorkflow.ImageGIFQuality;
            nudSwitchAfter.Value = Engine.MyWorkflow.ImageSizeLimit;
            if (cboSwitchFormat.Items.Count == 0)
            {
                cboSwitchFormat.Items.AddRange(typeof(EImageFormat).GetDescriptions());
            }
            cboSwitchFormat.SelectedIndex = (int)Engine.MyWorkflow.ImageFormat2;

            switch (Engine.conf.ImageSizeType)
            {
                case ImageSizeType.DEFAULT:
                    rbImageSizeDefault.Checked = true;
                    break;
                case ImageSizeType.FIXED:
                    rbImageSizeFixed.Checked = true;
                    break;
                case ImageSizeType.RATIO:
                    rbImageSizeRatio.Checked = true;
                    break;
            }

            txtImageSizeFixedWidth.Text = Engine.conf.ImageSizeFixedWidth.ToString();
            txtImageSizeFixedHeight.Text = Engine.conf.ImageSizeFixedHeight.ToString();
            txtImageSizeRatio.Text = Engine.conf.ImageSizeRatioPercentage.ToString();
        }

        private void ZScreen_ConfigGUI_Options_Watermark()
        {
            if (cboWatermarkType.Items.Count == 0)
            {
                cboWatermarkType.Items.AddRange(typeof(WatermarkType).GetDescriptions());
            }

            cboWatermarkType.SelectedIndex = (int)Engine.conf.WatermarkMode;
            if (chkWatermarkPosition.Items.Count == 0)
            {
                chkWatermarkPosition.Items.AddRange(typeof(WatermarkPositionType).GetDescriptions());
            }

            chkWatermarkPosition.SelectedIndex = (int)Engine.conf.WatermarkPositionMode;
            nudWatermarkOffset.Value = Engine.conf.WatermarkOffset;
            cbWatermarkAddReflection.Checked = Engine.conf.WatermarkAddReflection;
            cbWatermarkAutoHide.Checked = Engine.conf.WatermarkAutoHide;

            txtWatermarkText.Text = Engine.conf.WatermarkText;
            pbWatermarkFontColor.BackColor = Engine.conf.WatermarkFontArgb;
            lblWatermarkFont.Text = FontToString();
            nudWatermarkFontTrans.Value = Engine.conf.WatermarkFontTrans;
            trackWatermarkFontTrans.Value = (int)Engine.conf.WatermarkFontTrans;
            nudWatermarkCornerRadius.Value = Engine.conf.WatermarkCornerRadius;
            pbWatermarkGradient1.BackColor = Engine.conf.WatermarkGradient1Argb;
            pbWatermarkGradient2.BackColor = Engine.conf.WatermarkGradient2Argb;
            pbWatermarkBorderColor.BackColor = Engine.conf.WatermarkBorderArgb;
            nudWatermarkBackTrans.Value = Engine.conf.WatermarkBackTrans;
            trackWatermarkBackgroundTrans.Value = (int)Engine.conf.WatermarkBackTrans;
            if (cbWatermarkGradientType.Items.Count == 0)
            {
                cbWatermarkGradientType.Items.AddRange(Enum.GetNames(typeof(LinearGradientMode)));
            }

            cbWatermarkGradientType.SelectedIndex = (int)Engine.conf.WatermarkGradientType;
            cboUseCustomGradient.Checked = Engine.conf.WatermarkUseCustomGradient;

            txtWatermarkImageLocation.Text = Engine.conf.WatermarkImageLocation;
            cbWatermarkUseBorder.Checked = Engine.conf.WatermarkUseBorder;
            nudWatermarkImageScale.Value = Engine.conf.WatermarkImageScale;

            TestWatermark();
        }

        private void ZScreen_ConfigGUI_Options_History()
        {
            nudHistoryMaxItems.Value = Engine.conf.HistoryMaxNumber;
            cbHistorySave.Checked = Engine.conf.HistorySave;
        }

        private void ZScreen_ConfigGUI_TrayMenu()
        {
            if (tsmiTabs.DropDownItems.Count == 0)
            {
                foreach (TabPage tp in tcMain.TabPages)
                {
                    ToolStripMenuItem tsmi = new ToolStripMenuItem(tp.Text + "...");
                    tsmi.Click += new EventHandler(tsmiTab_Click);
                    tsmi.Image = tabImageList.Images[tp.ImageKey];
                    tsmi.Tag = tp.Name;
                    tsmiTabs.DropDownItems.Add(tsmi);
                }
            }
        }

        internal void ZScreen_Windows7onlyTasks()
        {
            if (!Engine.AppConf.Windows7TaskbarIntegration)
            {
                if (Engine.zJumpList != null)
                {
                    Engine.zJumpList.ClearAllUserTasks();
                    Engine.zJumpList.Refresh();
                }
            }
            else if (Engine.AppConf.Windows7TaskbarIntegration && this.Handle != IntPtr.Zero && TaskbarManager.IsPlatformSupported
                && this.ShowInTaskbar && this.WindowState == FormWindowState.Normal)
            {
                try
                {
                    Engine.CheckFileRegistration();

                    Engine.zWindowsTaskbar = TaskbarManager.Instance;
                    Engine.zWindowsTaskbar.ApplicationId = Engine.appId;

                    Engine.zJumpList = JumpList.CreateJumpList();

                    // User Tasks
                    JumpListLink jlCropShot = new JumpListLink(Adapter.ZScreenCliPath(), "Crop Shot");
                    jlCropShot.Arguments = "-cc";
                    jlCropShot.IconReference = new IconReference(Adapter.ResourcePath, 1);
                    Engine.zJumpList.AddUserTasks(jlCropShot);

                    JumpListLink jlSelectedWindow = new JumpListLink(Adapter.ZScreenCliPath(), "Selected Window");
                    jlSelectedWindow.Arguments = "-sw";
                    jlSelectedWindow.IconReference = new IconReference(Adapter.ResourcePath, 2);
                    Engine.zJumpList.AddUserTasks(jlSelectedWindow);

                    JumpListLink jlClipboardUpload = new JumpListLink(Adapter.ZScreenCliPath(), "Clipboard Upload");
                    jlClipboardUpload.Arguments = "-cu";
                    jlClipboardUpload.IconReference = new IconReference(Adapter.ResourcePath, 3);
                    Engine.zJumpList.AddUserTasks(jlClipboardUpload);

                    JumpListLink jlHistory = new JumpListLink(Application.ExecutablePath, "Open History");
                    jlHistory.Arguments = "-hi";
                    jlHistory.IconReference = new IconReference(Adapter.ResourcePath, 4);
                    Engine.zJumpList.AddUserTasks(jlHistory);

                    // Recent Items
                    Engine.zJumpList.KnownCategoryToDisplay = JumpListKnownCategoryType.Recent;

                    // Custom Categories
                    JumpListCustomCategory paths = new JumpListCustomCategory("Paths");

                    JumpListLink imagesJumpListLink = new JumpListLink(FileSystem.GetImagesDir(), "Images");
                    imagesJumpListLink.IconReference = new IconReference(Path.Combine("%windir%", "explorer.exe"), 0);

                    JumpListLink settingsJumpListLink = new JumpListLink(Engine.SettingsDir, "Settings");
                    settingsJumpListLink.IconReference = new IconReference(Path.Combine("%windir%", "explorer.exe"), 0);

                    JumpListLink logsJumpListLink = new JumpListLink(Engine.LogsDir, "Logs");
                    logsJumpListLink.IconReference = new IconReference(Path.Combine("%windir%", "explorer.exe"), 0);

                    paths.AddJumpListItems(imagesJumpListLink, settingsJumpListLink, logsJumpListLink);
                    Engine.zJumpList.AddCustomCategories(paths);

                    // Taskbar Buttons
                    ThumbnailToolBarButton cropShot = new ThumbnailToolBarButton(Resources.shape_square_ico, "Crop Shot");
                    cropShot.Click += new EventHandler<ThumbnailButtonClickedEventArgs>(cropShot_Click);

                    ThumbnailToolBarButton selWindow = new ThumbnailToolBarButton(Resources.application_double_ico, "Selected Window");
                    selWindow.Click += new EventHandler<ThumbnailButtonClickedEventArgs>(selWindow_Click);

                    ThumbnailToolBarButton clipboardUpload = new ThumbnailToolBarButton(Resources.clipboard_upload_ico, "Clipboard Upload");
                    clipboardUpload.Click += new EventHandler<ThumbnailButtonClickedEventArgs>(clipboardUpload_Click);

                    ThumbnailToolBarButton openHistory = new ThumbnailToolBarButton(Resources.pictures_ico, "History");
                    openHistory.Click += new EventHandler<ThumbnailButtonClickedEventArgs>(tsbOpenHistory_Click);

                    Engine.zWindowsTaskbar.ThumbnailToolBars.AddButtons(this.Handle, cropShot, selWindow, clipboardUpload, openHistory);

                    Engine.zJumpList.Refresh();

                    Engine.MyLogger.WriteLine("Integrated into Windows 7 Taskbar");
                }
                catch (Exception ex)
                {
                    Engine.MyLogger.WriteException(ex, "Error while configuring Windows 7 Taskbar");
                }
            }

            Engine.MyLogger.WriteLine(new StackFrame().GetMethod().Name);
        }
    }
}
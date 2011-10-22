using System;
using System.Diagnostics;
using System.Drawing;
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
    public partial class ZScreen : ZScreenCoreUI
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

            UploadManager.ListViewControl = lvUploads;
        }

        private void ZScreen_ConfigGUI()
        {
            StaticHelper.WriteLine("Configuring ZScreen GUI via " + new StackFrame(1).GetMethod().Name);

            if (Engine.ConfigWorkflow.PasswordsSecureUsingEncryption)
            {
                Engine.ConfigWorkflow.CryptPasswords(bEncrypt: false);
            }

            DisableFeatures();

            pgAppSettings.SelectedObject = Engine.ConfigApp;
            pgAppConfig.SelectedObject = Engine.ConfigUI;
            pgWorkflow.SelectedObject = Engine.ConfigWorkflow;
            pgIndexer.SelectedObject = Engine.ConfigUI.IndexerConfig;

            ZScreen_ConfigGUI_Form();
            ZScreen_ConfigGUI_TrayMenu();

            ZScreen_ConfigGUI_Main();
            ZScreen_ConfigGUI_Hotkeys();
            ZScreen_ConfigGUI_Capture();
            ZScreen_ConfigGUI_Actions();
            ZScreen_ConfigGUI_Options();
            ZScreen_ConfigGUI_Options_Paths();
            ZScreen_ConfigGUI_Options_History();
        }

        private void ZScreen_ConfigGUI_Form()
        {
            if (Engine.ConfigUI.LockFormSize)
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
            ucDestOptions.ReconfigOutputsUI();

            chkShowWorkflowWizard.Checked = Engine.ConfigUI.PromptForOutputs;
            chkShowCursor.Checked = Engine.ConfigWorkflow.DrawCursor;
            chkShowUploadResults.Checked = Engine.ConfigUI.ShowUploadResultsWindow;
        }

        private void ZScreen_ConfigGUI_Hotkeys()
        {
            InitHotkeys();
            hmHotkeys.PrepareHotkeys(HotkeyManager);
        }

        private void ZScreen_ConfigGUI_Capture()
        {
            ZScreen_ConfigGUI_Capture_CropShot();

            // Selected Window
            if (cbSelectedWindowStyle.Items.Count == 0)
            {
                cbSelectedWindowStyle.Items.AddRange(typeof(RegionStyles).GetDescriptions());
            }

            cbSelectedWindowStyle.SelectedIndex = (int)Engine.ConfigUI.SelectedWindowRegionStyles;
            cbSelectedWindowRectangleInfo.Checked = Engine.ConfigUI.SelectedWindowRectangleInfo;
            cbSelectedWindowRuler.Checked = Engine.ConfigUI.SelectedWindowRuler;
            pbSelectedWindowBorderColor.BackColor = Engine.ConfigUI.SelectedWindowBorderArgb;
            nudSelectedWindowBorderSize.Value = Engine.ConfigUI.SelectedWindowBorderSize;
            cbSelectedWindowDynamicBorderColor.Checked = Engine.ConfigUI.SelectedWindowDynamicBorderColor;
            nudSelectedWindowRegionInterval.Value = Engine.ConfigUI.SelectedWindowRegionInterval;
            nudSelectedWindowRegionStep.Value = Engine.ConfigUI.SelectedWindowRegionStep;
            nudSelectedWindowHueRange.Value = Engine.ConfigUI.SelectedWindowHueRange;
            chkSelectedWindowCaptureObjects.Checked = Engine.ConfigUI.SelectedWindowCaptureObjects;

            // Active Window
            if (cboCaptureEngine.Items.Count == 0)
            {
                cboCaptureEngine.Items.AddRange(typeof(CaptureEngineType).GetDescriptions());
                cboCaptureEngine.SelectedIndex = (int)Engine.ConfigWorkflow.CaptureEngineMode2;
            }

            chkActiveWindowCleanBackground.Checked = Engine.ConfigWorkflow.ActiveWindowClearBackground;
            chkSelectedWindowCleanTransparentCorners.Checked = Engine.ConfigWorkflow.ActiveWindowCleanTransparentCorners;
            chkSelectedWindowIncludeShadow.Checked = Engine.ConfigWorkflow.ActiveWindowIncludeShadows;
            chkActiveWindowTryCaptureChildren.Checked = Engine.ConfigWorkflow.ActiveWindowTryCaptureChildren;
            chkSelectedWindowShowCheckers.Checked = Engine.ConfigWorkflow.ActiveWindowShowCheckers;
            pbActiveWindowDwmBackColor.BackColor = Engine.ConfigWorkflow.ActiveWindowDwmBackColor;
            chkActiveWindowDwmCustomColor.Checked = Engine.ConfigWorkflow.ActiveWindowDwmUseCustomBackground;

            // Freehand Crop Shot
            cbFreehandCropShowHelpText.Checked = Engine.ConfigUI.FreehandCropShowHelpText;
            cbFreehandCropAutoUpload.Checked = Engine.ConfigUI.FreehandCropAutoUpload;
            cbFreehandCropAutoClose.Checked = Engine.ConfigUI.FreehandCropAutoClose;
            cbFreehandCropShowRectangleBorder.Checked = Engine.ConfigUI.FreehandCropShowRectangleBorder;
            pgSurfaceConfig.SelectedObject = Engine.ConfigUI.SurfaceConfig;

            // Naming Conventions
            txtActiveWindow.Text = Engine.ConfigWorkflow.ActiveWindowPattern;
            txtEntireScreen.Text = Engine.ConfigWorkflow.EntireScreenPattern;
            txtImagesFolderPattern.Text = Engine.ConfigWorkflow.SaveFolderPattern;
            nudMaxNameLength.Value = Engine.ConfigWorkflow.MaxNameLength;

            ZScreen_ConfigGUI_Options_Watermark();
        }

        private void ZScreen_ConfigGUI_Capture_CropShot()
        {
            if (cboCropEngine.Items.Count == 0)
            {
                cboCropEngine.Items.AddRange(typeof(CropEngineType).GetDescriptions());
                cboCropEngine.SelectedIndex = (int)Engine.ConfigUI.CropEngineMode;
            }

            // Crop Region Settings
            if (chkCropStyle.Items.Count == 0)
            {
                chkCropStyle.Items.AddRange(typeof(RegionStyles).GetDescriptions());
            }
            chkCropStyle.SelectedIndex = (int)Engine.ConfigUI.CropRegionStyles;
            chkRegionRectangleInfo.Checked = Engine.ConfigUI.CropRegionRectangleInfo;
            chkRegionHotkeyInfo.Checked = Engine.ConfigUI.CropRegionHotkeyInfo;

            // Crosshair Settings
            chkCropDynamicCrosshair.Checked = Engine.ConfigUI.CropDynamicCrosshair;
            nudCropCrosshairInterval.Value = Engine.ConfigUI.CropInterval;
            nudCropCrosshairStep.Value = Engine.ConfigUI.CropStep;
            nudCrosshairLineCount.Value = Engine.ConfigUI.CrosshairLineCount;
            nudCrosshairLineSize.Value = Engine.ConfigUI.CrosshairLineSize;
            pbCropCrosshairColor.BackColor = Engine.ConfigUI.CropCrosshairArgb;
            chkCropShowBigCross.Checked = Engine.ConfigUI.CropShowBigCross;
            chkCropShowMagnifyingGlass.Checked = Engine.ConfigUI.CropShowMagnifyingGlass;

            // Region Settings
            cbShowCropRuler.Checked = Engine.ConfigUI.CropShowRuler;
            cbCropDynamicBorderColor.Checked = Engine.ConfigUI.CropDynamicBorderColor;
            nudCropRegionInterval.Value = Engine.ConfigUI.CropRegionInterval;
            nudCropRegionStep.Value = Engine.ConfigUI.CropRegionStep;
            nudCropHueRange.Value = Engine.ConfigUI.CropHueRange;
            pbCropBorderColor.BackColor = Engine.ConfigUI.CropBorderArgb;
            nudCropBorderSize.Value = Engine.ConfigUI.CropBorderSize;
            cbCropShowGrids.Checked = Engine.ConfigUI.CropShowGrids;

            // Grid Mode Settings
            nudScreenshotDelay.Time = Engine.ConfigUI.ScreenshotDelayTimes;
            nudScreenshotDelay.Value = Engine.ConfigUI.ScreenshotDelayTime;
            cboCropGridMode.Checked = Engine.ConfigUI.CropGridToggle;
            nudCropGridWidth.Value = Engine.ConfigUI.CropGridSize.Width;
            nudCropGridHeight.Value = Engine.ConfigUI.CropGridSize.Height;
        }

        private void ZScreen_ConfigGUI_Actions()
        {
            chkPerformActions.Checked = Engine.ConfigWorkflow.PerformActions && Engine.ConfigUI.PromptForOutputs;
            chkPerformActions.Enabled = !Engine.ConfigUI.PromptForOutputs;
            tsmEditinImageSoftware.Checked = Engine.ConfigWorkflow.PerformActions;

            if (Engine.ConfigUI.ActionsApps.Count == 0)
            {
                Software editor = new Software(Engine.zImageAnnotator, Application.ExecutablePath, true, true);
                Engine.ConfigUI.ActionsApps.Add(editor);
                Software effects = new Software(Engine.zImageEffects, Application.ExecutablePath, true, false);
                Engine.ConfigUI.ActionsApps.Add(effects);
            }
            else
            {
                Engine.ConfigUI.ActionsApps.RemoveAll(x => string.IsNullOrEmpty(x.Path) || !File.Exists(x.Path));
            }

            ImageEditorHelper.FindImageEditors();

            lbSoftware.Items.Clear();

            foreach (Software app in Engine.ConfigUI.ActionsApps)
            {
                if (!String.IsNullOrEmpty(app.Name))
                {
                    lbSoftware.Items.Add(app.Name, app.Enabled);
                }
            }

            RewriteImageEditorsRightClickMenu();

            int i;
            if (Engine.ConfigUI.ImageEditor != null && (i = lbSoftware.Items.IndexOf(Engine.ConfigUI.ImageEditor.Name)) != -1)
            {
                lbSoftware.SelectedIndex = i;
            }
            else if (lbSoftware.Items.Count > 0)
            {
                lbSoftware.SelectedIndex = 0;
            }
        }

        private void ZScreen_ConfigGUI_Options()
        {
            // General
            chkStartWin.Checked = RegistryHelper.CheckStartWithWindows();
            chkShellExt.Checked = RegistryHelper.CheckShellContextMenu();
            chkOpenMainWindow.Checked = Engine.ConfigApp.ShowMainWindow;

            if (IsReady && !Engine.ConfigApp.ShowInTaskbar)
            {
                this.chkWindows7TaskbarIntegration.Checked = false; // Windows 7 Taskbar Integration cannot work without showing in Taskbar
                this.ShowInTaskbar = Engine.ConfigApp.ShowInTaskbar;
            }

            cbShowHelpBalloonTips.Checked = Engine.ConfigUI.ShowHelpBalloonTips;
            cbAutoSaveSettings.Checked = Engine.ConfigUI.AutoSaveSettings;
            chkWindows7TaskbarIntegration.Checked = TaskbarManager.IsPlatformSupported && Engine.ConfigApp.Windows7TaskbarIntegration;
            chkWindows7TaskbarIntegration.Enabled = TaskbarManager.IsPlatformSupported;

            chkTwitterEnable.Checked = Engine.ConfigUI.TwitterEnabled;

            // Interaction
            chkShortenURL.Checked = Engine.ConfigUI.ShortenUrlAfterUpload;
            nudFlashIconCount.Value = Engine.ConfigUI.FlashTrayCount;
            chkCaptureFallback.Checked = Engine.ConfigUI.CaptureEntireScreenOnError;
            cbShowPopup.Checked = Engine.ConfigUI.ShowBalloonTip;
            chkBalloonTipOpenLink.Checked = Engine.ConfigUI.BalloonTipOpenLink;
            cbShowUploadDuration.Checked = Engine.ConfigUI.ShowUploadDuration;
            cbCompleteSound.Checked = Engine.ConfigUI.CompleteSound;
            cbCloseDropBox.Checked = Engine.ConfigUI.CloseDropBox;

            // Proxy
            if (cboProxyConfig.Items.Count == 0)
            {
                cboProxyConfig.Items.AddRange(typeof(ProxyConfigType).GetDescriptions());
            }
            cboProxyConfig.SelectedIndex = (int)Engine.ConfigUI.ProxyConfig;

            ProxySetup(Engine.ConfigUI.ProxyList);
            if (ucProxyAccounts.AccountsList.Items.Count > 0)
            {
                ucProxyAccounts.AccountsList.SelectedIndex = Engine.ConfigUI.ProxySelected;
            }

            if (cboCloseButtonAction.Items.Count == 0)
            {
                cboMinimizeButtonAction.Items.AddRange(typeof(WindowButtonAction).GetDescriptions());
            }
            if (cboCloseButtonAction.Items.Count == 0)
            {
                cboCloseButtonAction.Items.AddRange(typeof(WindowButtonAction).GetDescriptions());
            }
            cboCloseButtonAction.SelectedIndex = (int)Engine.ConfigApp.WindowButtonActionClose;
            cboMinimizeButtonAction.SelectedIndex = (int)Engine.ConfigApp.WindowButtonActionMinimize;

            ttZScreen.Active = Engine.ConfigUI.ShowHelpBalloonTips;

            chkCheckUpdates.Checked = Engine.ConfigUI.CheckUpdates;
            if (cboReleaseChannel.Items.Count == 0)
            {
                cboReleaseChannel.Items.AddRange(typeof(ReleaseChannelType).GetDescriptions());
                cboReleaseChannel.SelectedIndex = (int)Engine.ConfigUI.ReleaseChannel;
            }
            chkDeleteLocal.Checked = Engine.ConfigUI.DeleteLocal;

            FolderWatcher zWatcher = new FolderWatcher(this);
            zWatcher.FolderPath = Engine.ConfigUI.FolderMonitorPath;
            if (Engine.ConfigUI.FolderMonitoring)
            {
                zWatcher.StartWatching();
            }
            else
            {
                zWatcher.StopWatching();
            }

            // Monitor Clipboard
            chkMonImages.Checked = Engine.ConfigUI.MonitorImages;
            chkMonText.Checked = Engine.ConfigUI.MonitorText;
            chkMonFiles.Checked = Engine.ConfigUI.MonitorFiles;
            chkMonUrls.Checked = Engine.ConfigUI.MonitorUrls;

            chkOverwriteFiles.Checked = Engine.ConfigWorkflow.OverwriteFiles;
        }

        private void ZScreen_ConfigGUI_Options_Paths()
        {
            Engine.InitializeDefaultFolderPaths(dirCreation: false);

            txtImagesDir.Text = Engine.ImagesDir;
            txtLogsDir.Text = Engine.LogsDir;

            if (Engine.ConfigApp.PreferSystemFolders)
            {
                txtRootFolder.Text = Engine.SettingsDir;
                gbRoot.Text = "Settings";
            }
            else
            {
                txtRootFolder.Text = Engine.ConfigApp.RootDir;
                gbRoot.Text = "Root";
            }

            btnRelocateRootDir.Enabled = !Engine.ConfigApp.PreferSystemFolders;
            gbRoot.Enabled = !Engine.IsPortable;
            gbImages.Enabled = !Engine.IsPortable;
            gbLogs.Enabled = !Engine.IsPortable;
        }

        private void ZScreen_ConfigGUI_Options_Watermark()
        {
            if (cboWatermarkType.Items.Count == 0)
            {
                cboWatermarkType.Items.AddRange(typeof(WatermarkType).GetDescriptions());
            }

            cboWatermarkType.SelectedIndex = (int)Engine.ConfigWorkflow.WatermarkMode;
            if (chkWatermarkPosition.Items.Count == 0)
            {
                chkWatermarkPosition.Items.AddRange(typeof(WatermarkPositionType).GetDescriptions());
            }

            chkWatermarkPosition.SelectedIndex = (int)Engine.ConfigWorkflow.WatermarkPositionMode;
            nudWatermarkOffset.Value = Engine.ConfigWorkflow.WatermarkOffset;
            cbWatermarkAddReflection.Checked = Engine.ConfigWorkflow.WatermarkAddReflection;
            cbWatermarkAutoHide.Checked = Engine.ConfigWorkflow.WatermarkAutoHide;

            txtWatermarkText.Text = Engine.ConfigWorkflow.WatermarkText;
            pbWatermarkFontColor.BackColor = Engine.ConfigWorkflow.WatermarkFontArgb;
            lblWatermarkFont.Text = FontToString();
            nudWatermarkFontTrans.Value = Engine.ConfigWorkflow.WatermarkFontTrans;
            trackWatermarkFontTrans.Value = (int)Engine.ConfigWorkflow.WatermarkFontTrans;
            nudWatermarkCornerRadius.Value = Engine.ConfigWorkflow.WatermarkCornerRadius;
            pbWatermarkGradient1.BackColor = Engine.ConfigWorkflow.WatermarkGradient1Argb;
            pbWatermarkGradient2.BackColor = Engine.ConfigWorkflow.WatermarkGradient2Argb;
            pbWatermarkBorderColor.BackColor = Engine.ConfigWorkflow.WatermarkBorderArgb;
            nudWatermarkBackTrans.Value = Engine.ConfigWorkflow.WatermarkBackTrans;
            trackWatermarkBackgroundTrans.Value = (int)Engine.ConfigWorkflow.WatermarkBackTrans;
            if (cbWatermarkGradientType.Items.Count == 0)
            {
                cbWatermarkGradientType.Items.AddRange(Enum.GetNames(typeof(LinearGradientMode)));
            }

            cbWatermarkGradientType.SelectedIndex = (int)Engine.ConfigWorkflow.WatermarkGradientType;
            cboUseCustomGradient.Checked = Engine.ConfigWorkflow.WatermarkUseCustomGradient;

            txtWatermarkImageLocation.Text = Engine.ConfigWorkflow.WatermarkImageLocation;
            cbWatermarkUseBorder.Checked = Engine.ConfigWorkflow.WatermarkUseBorder;
            nudWatermarkImageScale.Value = Engine.ConfigWorkflow.WatermarkImageScale;

            TestWatermark();
        }

        private void ZScreen_ConfigGUI_Options_History()
        {
            nudHistoryMaxItems.Value = Engine.ConfigUI.HistoryMaxNumber;
            cbHistorySave.Checked = Engine.ConfigUI.HistorySave;
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
            if (!Engine.ConfigApp.Windows7TaskbarIntegration)
            {
                if (Engine.zJumpList != null)
                {
                    Engine.zJumpList.ClearAllUserTasks();
                    Engine.zJumpList.Refresh();
                }
            }
            else if (!IsDisposed && Engine.ConfigApp.Windows7TaskbarIntegration && this.Handle != IntPtr.Zero && TaskbarManager.IsPlatformSupported
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
                    openHistory.Click += new EventHandler<ThumbnailButtonClickedEventArgs>(OpenHistory);

                    if (!IsDisposed)
                    {
                        Engine.zWindowsTaskbar.ThumbnailToolBars.AddButtons(this.Handle, cropShot, selWindow, clipboardUpload, openHistory);
                        Engine.zJumpList.Refresh();
                    }

                    StaticHelper.WriteLine("Integrated into Windows 7 Taskbar");
                }
                catch (Exception ex)
                {
                    StaticHelper.WriteException(ex, "Error while configuring Windows 7 Taskbar");
                }
            }

            StaticHelper.WriteLine(new StackFrame().GetMethod().Name);
        }
    }
}
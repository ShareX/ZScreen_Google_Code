#region License Information (GPL v2)

/*
    ZScreen - A program that allows you to upload screenshots in one keystroke.
    Copyright (C) 2008-2011 ZScreen Developers

    This program is free software; you can redistribute it and/or
    modify it under the terms of the GNU General Public License
    as published by the Free Software Foundation; either version 2
    of the License, or (at your option) any later version.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with this program; if not, write to the Free Software
    Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301, USA.

    Optionally you can also view the license at <http://www.gnu.org/licenses/>.
*/

#endregion License Information (GPL v2)

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Text;
using System.Threading;
using System.Timers;
using System.Windows.Forms;
using HelpersLib;
using HelpersLib.CLI;
using ScreenCapture;
using UploadersLib;
using UploadersLib.HelperClasses;
using ZScreenGUI.Properties;
using ZScreenGUI.UserControls;
using ZScreenLib;
using ZScreenTesterGUI;
using ZSS.ColorsLib;
using ZSS.FTPClientLib;
using Timer = System.Timers.Timer;

namespace ZScreenGUI
{
    public partial class ZScreen : ZScreenCoreUI
    {
        #region Trim memory

        private readonly Object trimMemoryLock = new Object();
        private Timer timerTrimMemory;

        /// <summary>
        /// Trim memory working set after a few seconds, unless this method is called again in the mean time (optimization)
        /// </summary>
        private void DelayedTrimMemoryUse()
        {
            if (Engine.ConfigUI != null && Engine.ConfigUI.EnableAutoMemoryTrim)
            {
                try
                {
                    lock (trimMemoryLock)
                    {
                        if (timerTrimMemory == null)
                        {
                            timerTrimMemory = new Timer();
                            timerTrimMemory.AutoReset = false;
                            timerTrimMemory.Interval = 10000;
                            timerTrimMemory.Elapsed += timerTrimMemory_Elapsed;
                        }
                        else
                        {
                            timerTrimMemory.Stop();
                        }

                        timerTrimMemory.Start();
                    }
                }
                catch (Exception ex)
                {
                    StaticHelper.WriteException(ex, "Error in DelayedTrimMemoryUse");
                }
            }
        }

        private void timerTrimMemory_Elapsed(object sender, ElapsedEventArgs e)
        {
            lock (trimMemoryLock)
            {
                if (timerTrimMemory != null)
                {
                    timerTrimMemory.Stop();
                    timerTrimMemory.Close();
                }

                NativeMethods.TrimMemoryUse();
            }
        }

        #endregion Trim memory

        #region Variables

        private readonly ContextMenuStrip codesMenu = new ContextMenuStrip();
        private readonly ImageList tabImageList = new ImageList();
        public CloseMethod CloseMethod;
        private DebugHelper mDebug;

        #endregion Variables

        #region ZScreen Form Events

        public ZScreen()
        {
            InitializeComponent();
            base.tsCoreMainTab.Visible = true;

            pbPreview.DisableViewer = true;
            pbPreview.LoadImage(Resources.ZScreen_256, PictureBoxSizeMode.CenterImage);
            pbPreview.SetNote("You can also Drag n Drop files or a directory on to anywhere in this page.");

            Icon = Resources.zss_main;
            WindowState = Engine.ConfigApp.ShowMainWindow ? FormWindowState.Normal : FormWindowState.Minimized;

            var bwConfig = new BackgroundWorker();
            bwConfig.DoWork += bwConfig_DoWork;
            bwConfig.RunWorkerCompleted += bwConfig_RunWorkerCompleted;
            bwConfig.RunWorkerAsync();
        }

        private void bwConfig_DoWork(object sender, DoWorkEventArgs e)
        {
            Engine.LoadSettings();
        }

        private void bwConfig_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            LoggerTimer timer = Engine.EngineLogger.StartTimer(new StackFrame().GetMethod().Name + " started");

            Text = Engine.GetProductName();
            niTray.Text = Text;

            Uploader.ProxySettings = Adapter.CheckProxySettings();

            if (Engine.ConfigUploaders.PasswordsSecureUsingEncryption)
            {
                Engine.ConfigUploaders.CryptPasswords(bEncrypt: false);
            }

            ZScreen_ConfigGUI();

            if (Engine.ConfigUI.CheckUpdates)
            {
                FormsMgr.OptionsUI.CheckUpdates();
            }

            PerformOnlineTasks();

            CleanCache();

            if (Engine.ConfigUI.ConfigProxy.ProxyConfigType != EProxyConfigType.NoProxy && Uploader.ProxySettings.ProxyActive != null)
            {
                StaticHelper.WriteLine("Proxy Settings: " + Uploader.ProxySettings.ProxyActive);
            }

            if (Engine.ConfigUI.BackupApplicationSettings)
            {
                FileSystem.BackupSettings();
            }

            if (Engine.ConfigUI.FirstRun)
            {
                if (Engine.HasVista)
                {
                    cboCaptureEngine.SelectedIndex = (int)CaptureEngineType.DWM;
                }

                ShowWindow();

                Engine.ConfigUI.FirstRun = false;
            }

            timer.WriteLineTime(new StackFrame().GetMethod().Name + " finished");
            StaticHelper.WriteLine("ZScreen startup time: {0} ms", Engine.StartTimer.ElapsedMilliseconds);

            UseCommandLineArg(Loader.CommandLineArg);

            IsReady = true;

            Engine.IsClipboardUploading = false;
            tmrClipboardMonitor.Tick += tmrClipboardMonitor_Tick;
        }

        private void ZScreen_Deactivate(object sender, EventArgs e)
        {
            codesMenu.Close();
            ucDestOptions.DropDownMenusClose();
        }

        private void ZScreen_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Save Destinations
            if (Engine.ConfigUI != null)
            {
                Adapter.SaveMenuConfigToList(ucDestOptions.tsddbOutputs, Engine.ConfigUI.ConfOutputs);
                Adapter.SaveMenuConfigToList(ucDestOptions.tsddbLinkFormat, Engine.ConfigUI.ConfLinkFormat);
                Adapter.SaveMenuConfigToList(ucDestOptions.tsddbClipboardContent, Engine.ConfigUI.ConfClipboardContent);
                Adapter.SaveMenuConfigToList(ucDestOptions.tsddbDestImage, Engine.ConfigUI.MyImageUploaders);
                Adapter.SaveMenuConfigToList(ucDestOptions.tsddbDestFile, Engine.ConfigUI.MyFileUploaders);
                Adapter.SaveMenuConfigToList(ucDestOptions.tsddbDestText, Engine.ConfigUI.MyTextUploaders);
                Adapter.SaveMenuConfigToList(ucDestOptions.tsddbDestLink, Engine.ConfigUI.MyURLShorteners);
            }

            // If UserClosing && ZScreenCloseReason.None then this means close button pressed in title bar
            if (e.CloseReason == CloseReason.UserClosing && CloseMethod == CloseMethod.None)
            {
                if (Engine.ConfigApp.WindowButtonActionClose == WindowButtonAction.ExitApplication)
                {
                    CloseMethod = CloseMethod.CloseButton;
                }
                else if (Engine.ConfigApp.WindowButtonActionClose == WindowButtonAction.MinimizeToTaskbar)
                {
                    WindowState = FormWindowState.Minimized;
                    e.Cancel = true;
                }
                else if (Engine.ConfigApp.WindowButtonActionClose == WindowButtonAction.MinimizeToTray)
                {
                    Hide();
                    DelayedTrimMemoryUse();
                    if (Engine.ConfigOptions.AutoSaveSettings) Engine.WriteSettingsAsync();
                    e.Cancel = true;
                }
            }

            // If really ZScreen is closing
            if (!e.Cancel)
            {
                StaticHelper.WriteLine("ZScreen_FormClosing - CloseReason: {0}, CloseMethod: {1}", e.CloseReason,
                                       CloseMethod);
                Engine.WriteSettings();
                Engine.TurnOff();
            }
        }

        private void ZScreen_Load(object sender, EventArgs e)
        {
            Engine.zHandle = Handle;

            #region Window Size/Location

            if (Engine.ConfigApp.WindowLocation.IsEmpty)
            {
                Engine.ConfigApp.WindowLocation = Location;
            }

            if (Engine.ConfigApp.WindowSize.IsEmpty)
            {
                Engine.ConfigApp.WindowSize = Size;
            }

            if (Engine.ConfigApp.SaveFormSizePosition)
            {
                Rectangle screenRect = CaptureHelpers.GetScreenBounds();
                screenRect.Inflate(-100, -100);

                if (
                    screenRect.IntersectsWith(new Rectangle(Engine.ConfigApp.WindowLocation, Engine.ConfigApp.WindowSize)))
                {
                    Size = Engine.ConfigApp.WindowSize;
                    Location = Engine.ConfigApp.WindowLocation;
                }
            }

            #endregion Window Size/Location

            #region Window Show/Hide

            bool bHideWindow = false;
            if (Engine.ConfigApp.ShowMainWindow)
            {
                if (Engine.ConfigApp.WindowState == FormWindowState.Maximized)
                {
                    WindowState = FormWindowState.Maximized;
                }
                else
                {
                    WindowState = FormWindowState.Normal;
                }
                ShowInTaskbar = Engine.ConfigApp.ShowInTaskbar;
            }
            else if (Engine.ConfigApp.ShowInTaskbar &&
                     Engine.ConfigApp.WindowButtonActionClose == WindowButtonAction.MinimizeToTaskbar)
            {
                WindowState = FormWindowState.Minimized;
            }
            else
            {
                bHideWindow = true;
            }

            if (Engine.ConfigApp.Windows7TaskbarIntegration && Engine.HasWindows7)
            {
                ZScreen_Windows7onlyTasks();
            }

            if (bHideWindow)
            {
                Hide(); // this should happen after windows 7 taskbar integration
            }

            #endregion Window Show/Hide

            LoggerTimer timer = Engine.EngineLogger.StartTimer(new StackFrame().GetMethod().Name + " started");

            ZScreen_Preconfig();

            mDebug = new DebugHelper();
            mDebug.GetDebugInfo += debug_GetDebugInfo;

            SetToolTip(nudScreenshotDelay);

            new RichTextBoxMenu(rtbDebugLog, true);
            new RichTextBoxMenu(rtbStats, true);

            if (Engine.IsMultiInstance)
            {
                niTray.ShowBalloonTip(2000, Engine.GetProductName(),
                                      string.Format("Another instance of {0} is already running...",
                                                    Application.ProductName), ToolTipIcon.Warning);
                niTray.BalloonTipClicked += niTray2_BalloonTipClicked;
            }

            timer.WriteLineTime(new StackFrame().GetMethod().Name + " finished");

            Application.Idle += Application_Idle;
        }

        private void ZScreen_Move(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Normal && Engine.ConfigApp.SaveFormSizePosition)
            {
                Engine.ConfigApp.WindowLocation = Location;
                Engine.ConfigApp.WindowSize = Size;
            }
        }

        private void ZScreen_Resize(object sender, EventArgs e)
        {
            if (IsReady)
            {
                Engine.ConfigApp.WindowState = WindowState;

                if (WindowState == FormWindowState.Normal && Engine.ConfigApp.SaveFormSizePosition)
                {
                    Engine.ConfigApp.WindowLocation = Location;
                    Engine.ConfigApp.WindowSize = Size;
                }

                Refresh();
            }
        }

        #endregion ZScreen Form Events

        private FTPAccount GetSelectedFTPforImages()
        {
            FTPAccount acc = null;
            if (Adapter.CheckFTPAccounts(Engine.ConfigUploaders.FTPSelectedImage))
            {
                acc = Engine.ConfigUploaders.FTPAccountList2[Engine.ConfigUploaders.FTPSelectedImage];
            }

            return acc;
        }

        public bool UseCommandLineArg(string arg)
        {
            if (!string.IsNullOrEmpty(arg))
            {
                StaticHelper.WriteLine("CommandLine: " + arg);
                var cli = new CLIManagerRegex();

                cli.Commands = new List<CLICommandRegex>
                                   {
                                       new CLICommandRegex("fu|fileupload", filePath => UploadUsingFileSystem(filePath)),
                                       new CLICommandRegex("cu|clipboardupload", () => UploadUsingClipboard()),
                                       new CLICommandRegex("fs|fullscreen", () => CaptureEntireScreen()),
                                       new CLICommandRegex("cc|crop", () => CaptureRectRegion()),
                                       new CLICommandRegex("sw|selectedwindow", () => CaptureSelectedWindow()),
                                       new CLICommandRegex("hi|history", () => OpenHistory()),
                                       new CLICommandRegex("ac|autocapture", () => ShowAutoCapture())
                                   };

                cli.FilePathAction = filePath => UploadUsingFileSystem(filePath);

                return cli.Parse(arg);
            }

            return false;
        }

        private void Application_Idle(object sender, EventArgs e)
        {
            DelayedTrimMemoryUse();
        }

        private void autoScreenshotsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowAutoCapture();
        }

        private void btnClearHistory_Click(object sender, EventArgs e)
        {
            if (File.Exists(Engine.HistoryPath))
            {
                if (MessageBox.Show(
                    "Do you really want to delete History?\r\nHistory file path: " + Engine.HistoryPath,
                    "ZScreen - History",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    File.Delete(Engine.HistoryPath);
                }
            }
        }

        private void btnDebugStart_Click(object sender, EventArgs e)
        {
            if (mDebug.DebugTimer.Enabled)
            {
                btnDebugStart.Text = "Start";
            }
            else
            {
                btnDebugStart.Text = "Pause";
            }

            mDebug.DebugTimer.Enabled = !mDebug.DebugTimer.Enabled;
        }

        private void btnFtpHelp_Click(object sender, EventArgs e)
        {
            ZAppHelper.LoadBrowserAsync("http://code.google.com/p/zscreen/wiki/FTPAccounts");
        }

        private void btnFTPOpenClient_Click(object sender, EventArgs e)
        {
            OpenFTPClient();
        }

        private void btnGalleryImageShack_Click(object sender, EventArgs e)
        {
            ZAppHelper.LoadBrowserAsync("http://my.imageshack.us/v_images.php");
        }

        private void btnGalleryTinyPic_Click(object sender, EventArgs e)
        {
            ZAppHelper.LoadBrowserAsync("http://tinypic.com/yourstuff.php");
        }

        private void btnLastCropShotReset_Click(object sender, EventArgs e)
        {
            Engine.ConfigUI.LastCapture = Rectangle.Empty;
            Engine.ConfigUI.LastRegion = Rectangle.Empty;
        }

        private void btnMoveImageFiles_Click(object sender, EventArgs e)
        {
            try
            {
                if (FileSystem.ManageImageFolders(Engine.RootImagesDir))
                {
                    MessageBox.Show("Files successfully moved to save folders.");
                }
            }
            catch (Exception ex)
            {
                StaticHelper.WriteException(ex, "Error while moving image files");
                MessageBox.Show(ex.Message);
            }
        }

        private void btnOpenZScreenTester_Click(object sender, EventArgs e)
        {
            new TesterGUI().ShowDialog();
        }

        private void btnRegCodeImageShack_Click(object sender, EventArgs e)
        {
            ZAppHelper.LoadBrowserAsync("http://profile.imageshack.us/prefs");
        }

        private void btnResetIncrement_Click(object sender, EventArgs e)
        {
            Engine.ConfigWorkflow.ConfigFileNaming.AutoIncrement = 0;
        }

        private void btnViewLocalDirectory_Click(object sender, EventArgs e)
        {
            ShowDirectory(FileSystem.GetImagesDir());
        }

        private void btnViewRemoteDirectory_Click(object sender, EventArgs e)
        {
            ShowDirectory(Engine.LogsDir);
        }

        private void btnWorkflowConfig_Click(object sender, EventArgs e)
        {
            ShowImageFormatUI();
        }

        public override void CaptureSelectedWindowGetList()
        {
            tsddbCoreSelectedWindow.DropDownItems.Clear();

            var windowsList = new WindowsList();
            List<WindowInfo> windows = windowsList.GetVisibleWindowsList();

            foreach (WindowInfo window in windows)
            {
                string title = window.Text.Truncate(50);
                ToolStripItem tsiSelectedWindow = tsddbCoreSelectedWindow.DropDownItems.Add(title);
                tsiSelectedWindow.Click += tsiSelectedWindow_Click;

                using (Icon icon = window.Icon)
                {
                    if (icon != null)
                    {
                        tsiSelectedWindow.Image = icon.ToBitmap();
                    }
                }

                tsiSelectedWindow.Tag = window;
            }
        }

        private void cbCropDynamicBorderColor_CheckedChanged(object sender, EventArgs e)
        {
            Engine.ConfigUI.CropDynamicBorderColor = cbCropDynamicBorderColor.Checked;
        }

        private void cbCropDynamicCrosshair_CheckedChanged(object sender, EventArgs e)
        {
            Engine.ConfigUI.CropDynamicCrosshair = chkCropDynamicCrosshair.Checked;
        }

        private void cbCropGridMode_CheckedChanged(object sender, EventArgs e)
        {
            Engine.ConfigUI.CropGridToggle = cboCropGridMode.Checked;
        }

        private void cbCropShowBigCross_CheckedChanged(object sender, EventArgs e)
        {
            Engine.ConfigUI.CropShowBigCross = chkCropShowBigCross.Checked;
        }

        private void cbCropShowGrids_CheckedChanged(object sender, EventArgs e)
        {
            Engine.ConfigUI.CropShowGrids = cbCropShowGrids.Checked;
        }

        private void cbCropShowMagnifyingGlass_CheckedChanged(object sender, EventArgs e)
        {
            Engine.ConfigUI.CropShowMagnifyingGlass = chkCropShowMagnifyingGlass.Checked;
        }

        private void cbCropStyle_SelectedIndexChanged(object sender, EventArgs e)
        {
            Engine.ConfigUI.CropRegionStyles = (RegionStyles)chkCropStyle.SelectedIndex;
        }

        private void cbFreehandCropAutoClose_CheckedChanged(object sender, EventArgs e)
        {
            Engine.ConfigUI.FreehandCropAutoClose = cbFreehandCropAutoClose.Checked;
        }

        private void cbFreehandCropAutoUpload_CheckedChanged(object sender, EventArgs e)
        {
            Engine.ConfigUI.FreehandCropAutoUpload = cbFreehandCropAutoUpload.Checked;
        }

        private void cbFreehandCropShowHelpText_CheckedChanged(object sender, EventArgs e)
        {
            Engine.ConfigUI.FreehandCropShowHelpText = cbFreehandCropShowHelpText.Checked;
        }

        private void cbFreehandCropShowRectangleBorder_CheckedChanged(object sender, EventArgs e)
        {
            Engine.ConfigUI.FreehandCropShowRectangleBorder = cbFreehandCropShowRectangleBorder.Checked;
        }

        private void cboCaptureEngine_SelectedIndexChanged(object sender, EventArgs e)
        {
            Engine.ConfigWorkflow.CaptureEngineMode2 = (CaptureEngineType)cboCaptureEngine.SelectedIndex;
            UpdateAeroGlassConfig();
        }

        private void cboCropEngine_SelectedIndexChanged(object sender, EventArgs e)
        {
            Engine.ConfigUI.CropEngineMode = (CropEngineType)cboCropEngine.SelectedIndex;
            gbCropRegion.Visible = Engine.ConfigUI.CropEngineMode == CropEngineType.Cropv1;
            gbCropCrosshairSettings.Visible = Engine.ConfigUI.CropEngineMode == CropEngineType.Cropv1;
            gbCropDynamicRegionBorderColorSettings.Visible = Engine.ConfigUI.CropEngineMode == CropEngineType.Cropv1;
            gbCropGridMode.Visible = Engine.ConfigUI.CropEngineMode == CropEngineType.Cropv1;
            gbCropRegionSettings.Visible = Engine.ConfigUI.CropEngineMode == CropEngineType.Cropv1;
            gbCropShotMagnifyingGlass.Visible = Engine.ConfigUI.CropEngineMode == CropEngineType.Cropv1;
        }

        private void cbRegionHotkeyInfo_CheckedChanged(object sender, EventArgs e)
        {
            Engine.ConfigUI.CropRegionHotkeyInfo = chkRegionHotkeyInfo.Checked;
        }

        private void cbRegionRectangleInfo_CheckedChanged(object sender, EventArgs e)
        {
            Engine.ConfigUI.CropRegionRectangleInfo = chkRegionRectangleInfo.Checked;
            chkCropShowMagnifyingGlass.Enabled = chkRegionRectangleInfo.Checked;
        }

        private void cbSelectedWindowCaptureObjects_CheckedChanged(object sender, EventArgs e)
        {
            Engine.ConfigUI.SelectedWindowCaptureObjects = chkSelectedWindowCaptureObjects.Checked;
        }

        private void cbSelectedWindowCleanBackground_CheckedChanged(object sender, EventArgs e)
        {
            Engine.ConfigWorkflow.ActiveWindowClearBackground = chkActiveWindowCleanBackground.Checked;
            UpdateAeroGlassConfig();
        }

        private void cbSelectedWindowDynamicBorderColor_CheckedChanged(object sender, EventArgs e)
        {
            Engine.ConfigUI.SelectedWindowDynamicBorderColor = cbSelectedWindowDynamicBorderColor.Checked;
        }

        private void cbSelectedWindowIncludeShadow_CheckedChanged(object sender, EventArgs e)
        {
            Engine.ConfigWorkflow.ActiveWindowIncludeShadows = chkSelectedWindowIncludeShadow.Checked;
            UpdateAeroGlassConfig();
        }

        private void cbSelectedWindowRectangleInfo_CheckedChanged(object sender, EventArgs e)
        {
            Engine.ConfigUI.SelectedWindowRectangleInfo = cbSelectedWindowRectangleInfo.Checked;
        }

        private void cbSelectedWindowRuler_CheckedChanged(object sender, EventArgs e)
        {
            Engine.ConfigUI.SelectedWindowRuler = cbSelectedWindowRuler.Checked;
        }

        private void cbSelectedWindowShowCheckers_CheckedChanged(object sender, EventArgs e)
        {
            Engine.ConfigWorkflow.ActiveWindowShowCheckers = chkSelectedWindowShowCheckers.Checked;
        }

        private void cbSelectedWindowStyle_SelectedIndexChanged(object sender, EventArgs e)
        {
            Engine.ConfigUI.SelectedWindowRegionStyles = (RegionStyles)cbSelectedWindowStyle.SelectedIndex;
        }

        private void cbShowCropRuler_CheckedChanged(object sender, EventArgs e)
        {
            Engine.ConfigUI.CropShowRuler = cbShowCropRuler.Checked;
        }

        private void cbShowCursor_CheckedChanged(object sender, EventArgs e)
        {
            Engine.ConfigWorkflow.DrawCursor = chkShowCursor.Checked;
        }

        private void chkActiveWindowDwmCustomColor_CheckedChanged(object sender, EventArgs e)
        {
            Engine.ConfigWorkflow.ActiveWindowDwmUseCustomBackground = chkActiveWindowDwmCustomColor.Checked;
        }

        private void chkActiveWindowTryCaptureChilds_CheckedChanged(object sender, EventArgs e)
        {
            Engine.ConfigWorkflow.ActiveWindowTryCaptureChildren = chkActiveWindowTryCaptureChildren.Checked;
        }

        private void ChkEditorsEnableCheckedChanged(object sender, EventArgs e)
        {
            Engine.ConfigWorkflow.PerformActions = chkPerformActions.Checked;
        }

        private void chkManualNaming_CheckedChanged(object sender, EventArgs e)
        {
            Engine.ConfigUI.PromptForOutputs = chkShowWorkflowWizard.Checked;
            if (chkShowWorkflowWizard.Checked)
            {
                chkPerformActions.Checked = false;
            }
            chkPerformActions.Enabled = !chkShowWorkflowWizard.Checked;
        }

        private void chkMonFiles_CheckedChanged(object sender, EventArgs e)
        {
            Engine.ConfigUI.MonitorFiles = chkMonFiles.Checked;
        }

        private void chkMonImages_CheckedChanged(object sender, EventArgs e)
        {
            Engine.ConfigUI.MonitorImages = chkMonImages.Checked;
        }

        private void chkMonText_CheckedChanged(object sender, EventArgs e)
        {
            Engine.ConfigUI.MonitorText = chkMonText.Checked;
        }

        private void chkMonUrls_CheckedChanged(object sender, EventArgs e)
        {
            Engine.ConfigUI.MonitorUrls = chkMonUrls.Checked;
        }

        private void chkShortenURL_CheckedChanged(object sender, EventArgs e)
        {
            Engine.ConfigUI.ShortenUrlAfterUpload = chkShortenURL.Checked;
        }

        private void chkShowUploadResults_CheckedChanged(object sender, EventArgs e)
        {
            Engine.ConfigUI.ShowUploadResultsWindow = chkShowUploadResults.Checked;
        }

        public override void ClipboardUpload()
        {
            if (Engine.ConfigUI.ShowClipboardContentViewer)
            {
                using (var ccv = new ClipboardContentViewer())
                {
                    if (ccv.ShowDialog() == DialogResult.OK && !ccv.IsClipboardEmpty)
                    {
                        UploadUsingClipboard();
                    }

                    Engine.ConfigUI.ShowClipboardContentViewer = !ccv.DontShowThisWindow;
                }
            }
            else
            {
                UploadUsingClipboard();
            }
        }

        private void clipboardUpload_Click(object sender, EventArgs e)
        {
            UploadUsingClipboard();
        }

        private void cmVersionHistory_Click(object sender, EventArgs e)
        {
            FormsMgr.ShowVersionHistory();
        }

        private void cropShot_Click(object sender, EventArgs e)
        {
            CaptureRectRegion();
        }

        private void debug_GetDebugInfo(object sender, string e)
        {
            if (Visible)
            {
                var sb = new StringBuilder();
                sb.Append(e);
                rtbStats.Text = sb.ToString();
            }
        }

        private void editInPicnikToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // TODO: Edit in Picnik in the new History
            //Process.Start(string.Format("http://www.picnik.com/service/?_import={0}&_apikey={1}",
            //                    HttpUtility.UrlEncode(hi.RemotePath), Engine.PicnikKey));
        }

        private void entireScreenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Thread.Sleep(300);
            CaptureEntireScreen();
        }

        public override void FileUpload()
        {
            using (var ofd = new OpenFileDialog())
            {
                ofd.Multiselect = true;
                ofd.Title = "Upload files...";
                ofd.Filter = "All Files (*.*)|*.*";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    UploadUsingFileSystem(ofd.FileNames);
                }
            }
        }

        private void historyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenHistory(sender, e);
        }

        private void languageTranslatorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StartWorkerTranslator();
        }

        private void lastRectangularRegionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Thread.Sleep(300);
            CaptureRectRegionLast();
        }

        private void llblBugReports_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ZAppHelper.LoadBrowserAsync(ZLinks.URL_ISSUES);
        }

        private void llProjectPage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ZAppHelper.LoadBrowserAsync(ZLinks.URL_WIKIPAGES);
        }

        private void nudCropBorderSize_ValueChanged(object sender, EventArgs e)
        {
            Engine.ConfigUI.CropBorderSize = nudCropBorderSize.Value;
        }

        private void nudCropGridHeight_ValueChanged(object sender, EventArgs e)
        {
            Engine.ConfigUI.CropGridSize.Height = (int)nudCropGridHeight.Value;
        }

        private void nudCropGridSize_ValueChanged(object sender, EventArgs e)
        {
            Engine.ConfigUI.CropGridSize.Width = (int)nudCropGridWidth.Value;
        }

        private void nudCropHueRange_ValueChanged(object sender, EventArgs e)
        {
            Engine.ConfigUI.CropHueRange = nudCropHueRange.Value;
        }

        private void nudCropInterval_ValueChanged(object sender, EventArgs e)
        {
            Engine.ConfigUI.CropInterval = (int)nudCropCrosshairInterval.Value;
        }

        private void nudCropRegionInterval_ValueChanged(object sender, EventArgs e)
        {
            Engine.ConfigUI.CropRegionInterval = nudCropRegionInterval.Value;
        }

        private void nudCropRegionStep_ValueChanged(object sender, EventArgs e)
        {
            Engine.ConfigUI.CropRegionStep = nudCropRegionStep.Value;
        }

        private void nudCropStep_ValueChanged(object sender, EventArgs e)
        {
            Engine.ConfigUI.CropStep = (int)nudCropCrosshairStep.Value;
        }

        private void nudCrosshairLineCount_ValueChanged(object sender, EventArgs e)
        {
            Engine.ConfigUI.CrosshairLineCount = (int)nudCrosshairLineCount.Value;
        }

        private void nudCrosshairLineSize_ValueChanged(object sender, EventArgs e)
        {
            Engine.ConfigUI.CrosshairLineSize = (int)nudCrosshairLineSize.Value;
        }

        private void nudSelectedWindowBorderSize_ValueChanged(object sender, EventArgs e)
        {
            Engine.ConfigUI.SelectedWindowBorderSize = nudSelectedWindowBorderSize.Value;
        }

        private void nudSelectedWindowHueRange_ValueChanged(object sender, EventArgs e)
        {
            Engine.ConfigUI.SelectedWindowHueRange = nudSelectedWindowHueRange.Value;
        }

        private void nudSelectedWindowRegionInterval_ValueChanged(object sender, EventArgs e)
        {
            Engine.ConfigUI.SelectedWindowRegionInterval = nudSelectedWindowRegionInterval.Value;
        }

        private void nudSelectedWindowRegionStep_ValueChanged(object sender, EventArgs e)
        {
            Engine.ConfigUI.SelectedWindowRegionStep = nudSelectedWindowRegionStep.Value;
        }

        private void nudtScreenshotDelay_MouseHover(object sender, EventArgs e)
        {
            ttZScreen.Show(ttZScreen.GetToolTip(nudScreenshotDelay), this);
        }

        private void nudtScreenshotDelay_SelectedIndexChanged(object sender, EventArgs e)
        {
            Engine.ConfigUI.ScreenshotDelayTimes = nudScreenshotDelay.Time;
        }

        private void numericUpDownTimer1_ValueChanged(object sender, EventArgs e)
        {
            Engine.ConfigUI.ScreenshotDelayTime = nudScreenshotDelay.Value;
        }

        public void OpenFTPClient()
        {
            if (Engine.ConfigUploaders.FTPAccountList2.Count > 0)
            {
                FTPAccount acc = Engine.ConfigUploaders.FTPAccountList2[Engine.ConfigUploaders.FTPSelectedImage];
                if (acc != null)
                {
                    if (acc.Protocol == FTPProtocol.SFTP)
                    {
                        MessageBox.Show("Sorry, this doesn't support SFTP.", "Sorry!", MessageBoxButtons.OK,
                                        MessageBoxIcon.Error);
                        return;
                    }
                    var ftpClient = new FTPClient2(acc) { Icon = Icon };
                    ftpClient.Show();
                }
            }
        }

        private void pbActiveWindowDwmBackColor_Click(object sender, EventArgs e)
        {
            SelectColor((PictureBox)sender, ref Engine.ConfigWorkflow.ActiveWindowDwmBackColor);
        }

        private void pbCropBorderColor_Click(object sender, EventArgs e)
        {
            SelectColor((PictureBox)sender, ref Engine.ConfigUI.CropBorderArgb);
        }

        private void pbCropCrosshairColor_Click(object sender, EventArgs e)
        {
            SelectColor((PictureBox)sender, ref Engine.ConfigUI.CropCrosshairArgb);
        }

        private void pbSelectedWindowBorderColor_Click(object sender, EventArgs e)
        {
            SelectColor((PictureBox)sender, ref Engine.ConfigUI.SelectedWindowBorderArgb);
        }

        private void pgAppConfig_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            ZScreen_ConfigGUI();
        }

        private void pgAppSettings_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            if (IsReady)
            {
                FormsMgr.OptionsUI.ConfigurePaths();

                if (!string.IsNullOrEmpty(Engine.ConfigApp.UploadersConfigCustomPath))
                {
                    Engine.ConfigUploaders = UploadersConfig.Load(Engine.ConfigApp.UploadersConfigCustomPath);
                }

                if (!string.IsNullOrEmpty(Engine.ConfigApp.WorkflowConfigCustomPath))
                {
                    Engine.ConfigWorkflow = Workflow.Read(Engine.ConfigApp.WorkflowConfigCustomPath);
                }
            }
        }

        private void rectangularRegionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Thread.Sleep(300);
            CaptureRectRegion();
        }

        private void screenColorPickerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowScreenColorPicker();
        }

        private void SelectColor(Control pb, ref XColor color)
        {
            var dColor = new DialogColor(pb.BackColor);
            if (dColor.ShowDialog() == DialogResult.OK)
            {
                pb.BackColor = dColor.Color;
                color = dColor.Color;
            }
        }

        private void selectedWindowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Thread.Sleep(300);
            CaptureSelectedWindow();
        }

        private void selWindow_Click(object sender, EventArgs e)
        {
            CaptureSelectedWindow();
        }

        private void SetToolTip(Control original)
        {
            SetToolTip(original, original);
        }

        private void SetToolTip(Control original, Control next)
        {
            ttZScreen.SetToolTip(next, ttZScreen.GetToolTip(original));
            foreach (Control c in next.Controls)
            {
                SetToolTip(original, c);
            }
        }

        private void ShowMainWindow()
        {
            if (IsHandleCreated)
            {
                Show();
                WindowState = FormWindowState.Normal;
                NativeMethods.ActivateWindow(Handle);
            }
        }

        private void ShowWindow()
        {
            Show();
            WindowState = FormWindowState.Normal;
            Activate();
            BringToFront();
        }

        private void tcAdvanced_Selected(object sender, TabControlEventArgs e)
        {
            if (e.TabPage == tpAdvancedDebug)
            {
                rtbDebugLog.Text = Engine.EngineLogger.ToString();
            }
        }

        private void tcMain_Selecting(object sender, TabControlCancelEventArgs e)
        {
            if (e.TabPage == tpAdvanced && tcAdvanced.SelectedTab == tpAdvancedDebug)
            {
                rtbDebugLog.Text = Engine.EngineLogger.ToString();
            }
        }

        /// <summary>
        /// Method to periodically (every 6 hours) perform online tasks
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tmrApp_Tick(object sender, EventArgs e)
        {
            PerformOnlineTasks();
        }

        private void tpMain_DragDrop(object sender, DragEventArgs e)
        {
            var fileDirPaths = (string[])e.Data.GetData(DataFormats.FileDrop, true);
            var ddMainGUIfiles = new List<string>();
            foreach (string fdp in fileDirPaths)
            {
                if (File.Exists(fdp))
                {
                    ddMainGUIfiles.Add(fdp);
                }
                else if (Directory.Exists(fdp))
                {
                    ddMainGUIfiles.AddRange(Directory.GetFiles(fdp, "*.*", SearchOption.AllDirectories));
                }
            }
            UploadUsingFileSystem(ddMainGUIfiles.ToArray());
        }

        private void tpMain_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.All;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private void tpMain_MouseClick(object sender, MouseEventArgs e)
        {
            ucDestOptions.DropDownMenusClose();
        }

        private void tpMain_MouseLeave(object sender, EventArgs e)
        {
            ucDestOptions.DropDownMenusClose();
        }

        private void tpSourceFileSystem_DragDrop(object sender, DragEventArgs e)
        {
            var ddfilePaths = (string[])e.Data.GetData(DataFormats.FileDrop, true);
            UploadUsingFileSystem(ddfilePaths);
        }

        private void tpSourceFileSystem_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.All;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private void tsbDonate_Click(object sender, EventArgs e)
        {
            ZAppHelper.LoadBrowserAsync(ZLinks.URL_DONATE_ZS);
        }

        private void tsbDonate_MouseEnter(object sender, EventArgs e)
        {
            Cursor = Cursors.Hand;
        }

        private void tsbLinkHelp_Click(object sender, EventArgs e)
        {
            ZAppHelper.LoadBrowserAsync(ZLinks.URL_WIKIPAGES);
        }

        private void tsbLinkHome_Click(object sender, EventArgs e)
        {
            ZAppHelper.LoadBrowserAsync(ZLinks.URL_WEBSITE);
        }

        private void tsbLinkIssues_Click(object sender, EventArgs e)
        {
            ZAppHelper.LoadBrowserAsync(ZLinks.URL_ISSUES);
        }

        public void tsddbSelectedWindow_DropDownOpening(object sender, EventArgs e)
        {
            CaptureSelectedWindowGetList();
        }

        private void tsiSelectedWindow_Click(object sender, EventArgs e)
        {
            var tsi = (ToolStripItem)sender;
            var wi = tsi.Tag as WindowInfo;
            if (wi != null)
            {
                CaptureSelectedWindowFromList(wi.Handle);
            }
        }

        private void tsmAboutMain_Click(object sender, EventArgs e)
        {
            FormsMgr.ShowAboutWindow();
        }

        private void tsmDropWindow_Click(object sender, EventArgs e)
        {
            ShowDropWindow();
        }

        private void tsmFileUpload_Click(object sender, EventArgs e)
        {
            FileUpload();
        }

        private void tsmFreehandCropShot_Click(object sender, EventArgs e)
        {
            Thread.Sleep(300);
            CaptureFreeHandRegion();
        }

        private void tsmFTPClient_Click(object sender, EventArgs e)
        {
            OpenFTPClient();
        }

        private void tsmLic_Click(object sender, EventArgs e)
        {
            FormsMgr.ShowLicense();
        }

        private void tsmMain_Click(object sender, EventArgs e)
        {
            ShowWindow();
        }

        private void tsmSettings_Click(object sender, EventArgs e)
        {
            FormsMgr.ShowOptionsUI();
        }

        private void tsmUploadFromClipboard_Click(object sender, EventArgs e)
        {
            ClipboardUpload();
        }

        private void tsmViewDirectory_Click(object sender, EventArgs e)
        {
            ShowDirectory(FileSystem.GetImagesDir());
        }

        private void txtDebugLog_LinkClicked(object sender, LinkClickedEventArgs e)
        {
            ZAppHelper.LoadBrowserAsync(e.LinkText);
        }

        private void UpdateAeroGlassConfig()
        {
            gbCaptureDwm.Enabled = Engine.ConfigWorkflow.CaptureEngineMode2 == CaptureEngineType.DWM;
            gbCaptureGdi.Enabled = Engine.ConfigWorkflow.CaptureEngineMode2 == CaptureEngineType.GDI;

            // Disable Show Checkers option if Clean Background is disabled
            if (!chkActiveWindowCleanBackground.Checked)
            {
                chkSelectedWindowShowCheckers.Checked = false;
            }
            chkSelectedWindowShowCheckers.Enabled = chkActiveWindowCleanBackground.Checked;

            // Disable Capture children option if DWM is enabled
            if (cboCaptureEngine.SelectedIndex == (int)CaptureEngineType.DWM)
            {
                chkActiveWindowTryCaptureChildren.Checked = false;
            }
            chkActiveWindowTryCaptureChildren.Enabled = cboCaptureEngine.SelectedIndex != (int)CaptureEngineType.DWM;
        }

        private void tsmEditinImageSoftware_Click(object sender, EventArgs e)
        {
            ShowConfigureActionsUI();
        }

        private void btnActionsUI_Click(object sender, EventArgs e)
        {
            ShowConfigureActionsUI();
        }

        private void btnConfigWatermark_Click(object sender, EventArgs e)
        {
            ShowWatermarkUI();
        }

        private void btnFileNamingUI_Click(object sender, EventArgs e)
        {
            ShowFileNamingUI();
        }

        private void tsmiImageSettings_Click(object sender, EventArgs e)
        {
            ShowImageFormatUI();
        }

        private void tsmiWatermark_Click(object sender, EventArgs e)
        {
            ShowWatermarkUI();
        }

        private void tsmiConfigureFileNaming_Click(object sender, EventArgs e)
        {
            ShowFileNamingUI();
        }

        private void tsmiFileUpload_Click(object sender, EventArgs e)
        {
            FileUpload();
        }

        private void tsmiExit_Click(object sender, EventArgs e)
        {
            tsmExitZScreen_Click(sender, e);
        }

        private void tsmiVersionHistory_Click(object sender, EventArgs e)
        {
            FormsMgr.ShowVersionHistory();
        }

        private void tsmiConfigureActions_Click(object sender, EventArgs e)
        {
            ShowConfigureActionsUI();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormsMgr.ShowAboutWindow();
        }

        private void tsmiOutputs_Click(object sender, EventArgs e)
        {
            ucDestOptions.tsbDestConfig_Click(sender, e);
        }

        private void tsmiOptions_Click(object sender, EventArgs e)
        {
            ShowOptions();
        }

        private void tsmiProxy_Click(object sender, EventArgs e)
        {
            if (FormsMgr.ShowDialogProxyConfig() == System.Windows.Forms.DialogResult.OK)
            {
                Uploader.ProxySettings = Adapter.CheckProxySettings();
            }
        }

        private void tsmiApiKeys_Click(object sender, EventArgs e)
        {
            FormsMgr.ShowApiKeysUI();
        }
    }
}
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
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Timers;
using System.Windows.Forms;
using GradientTester;
using HelpersLib;
using HelpersLib.CLI;
using ScreenCapture;
using UploadersAPILib;
using UploadersLib;
using UploadersLib.HelperClasses;
using ZSS.ColorsLib;
using ZSS.FTPClientLib;
using ZSS.UpdateCheckerLib;
using ZScreenGUI.Properties;
using ZScreenGUI.UserControls;
using ZScreenLib;
using ZScreenTesterGUI;
using Timer = System.Timers.Timer;

namespace ZScreenGUI
{
    public partial class ZScreen : ZScreenCoreUI
    {
        #region Codes Menu

        private void CodesMenuCloseEvent(object sender, MouseEventArgs e)
        {
            codesMenu.Close();
        }

        private void CodesMenuCloseEvents()
        {
            tpWatermark.MouseClick += CodesMenuCloseEvent;
            foreach (Control cntrl in tpWatermark.Controls)
            {
                if (cntrl.GetType() == typeof (GroupBox))
                {
                    cntrl.MouseClick += CodesMenuCloseEvent;
                }
            }
        }

        private void CreateCodesMenu()
        {
            var variables = Enum.GetValues(typeof (ReplacementVariables)).Cast<ReplacementVariables>().
                Select(
                    x =>
                    new
                        {
                            Name = ReplacementExtension.Prefix + Enum.GetName(typeof (ReplacementVariables), x),
                            Description = x.GetDescription()
                        });

            foreach (var variable in variables)
            {
                var tsi = new ToolStripMenuItem
                              {
                                  Text = string.Format("{0} - {1}", variable.Name, variable.Description),
                                  Tag = variable.Name
                              };
                tsi.Click += watermarkCodeMenu_Click;
                codesMenu.Items.Add(tsi);
            }

            CodesMenuCloseEvents();
        }

        #endregion Codes Menu

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
        private TextBox mHadFocus;

        private int mHadFocusAt;

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
                CheckUpdates();
            }

            PerformOnlineTasks();

            CleanCache();

            if (Engine.ConfigUI.ProxyConfig != ProxyConfigType.NoProxy && Uploader.ProxySettings.ProxyActive != null)
            {
                StaticHelper.WriteLine("Proxy Settings: " + Uploader.ProxySettings.ProxyActive);
            }

            if (Engine.ConfigUI.BackupApplicationSettings)
            {
                FileSystem.BackupSettings();
            }

            UpdateHotkeys(false);

            if (Engine.ConfigUI.FirstRun)
            {
                if (Engine.HasVista)
                {
                    cboCaptureEngine.SelectedIndex = (int) CaptureEngineType.DWM;
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
                    if (Engine.ConfigUI.AutoSaveSettings) Engine.WriteSettingsAsync();
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

            if (Engine.ConfigApp.Windows7TaskbarIntegration && Engine.HasWindows7)
            {
                ZScreen_Windows7onlyTasks();
            }

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

            #endregion Windows Size/Location

            #region Window Show/Hide 

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
                Hide(); // this should happen after windows 7 taskbar integration
            }

            #endregion Window Show/Hide

            LoggerTimer timer = Engine.EngineLogger.StartTimer(new StackFrame().GetMethod().Name + " started");

            ZScreen_Preconfig();

            mDebug = new DebugHelper();
            mDebug.GetDebugInfo += debug_GetDebugInfo;

            SetToolTip(nudScreenshotDelay);

            CreateCodesMenu();

            new RichTextBoxMenu(rtbDebugLog, true);
            new RichTextBoxMenu(rtbStats, true);

            if (Engine.IsMultipleInstance)
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
            }
        }

        #endregion ZScreen Form Events

        /// <summary>
        /// Browse for an applicatoin
        /// </summary>
        /// <returns>Software</returns>
        private Software BrowseApplication()
        {
            Software temp = null;

            using (var dlg = new OpenFileDialog())
            {
                dlg.Filter = Resources.FilterAllFiles;

                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    temp = new Software();
                    temp.Name = Path.GetFileNameWithoutExtension(dlg.FileName);
                    temp.Path = dlg.FileName;
                }
            }

            return temp;
        }

        private string FontToString()
        {
            return FontToString(Engine.ConfigWorkflow.WatermarkFont, Engine.ConfigWorkflow.WatermarkFontArgb);
        }

        private string FontToString(Font font, Color color)
        {
            return "Name: " + font.Name + " - Size: " + font.Size + " - Style: " + font.Style + " - Color: " +
                   color.R + "," + color.G + "," + color.B;
        }

        /// <summary>
        /// Searches for an Image Software in settings and returns it
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        private static Software GetImageSoftware(string name)
        {
            foreach (Software app in Engine.ConfigUI.ActionsApps)
            {
                if (app != null && app.Name != null)
                {
                    if (app.Name.Equals(name))
                    {
                        return app;
                    }
                }
            }

            return null;
        }

        private FTPAccount GetSelectedFTPforImages()
        {
            FTPAccount acc = null;
            if (Adapter.CheckFTPAccounts(Engine.ConfigUploaders.FTPSelectedImage))
            {
                acc = Engine.ConfigUploaders.FTPAccountList2[Engine.ConfigUploaders.FTPSelectedImage];
            }

            return acc;
        }

        private ProxyInfo GetSelectedProxy()
        {
            ProxyInfo acc = null;
            if (ucProxyAccounts.AccountsList.SelectedIndex != -1 &&
                Engine.ConfigUI.ProxyList.Count >= ucProxyAccounts.AccountsList.Items.Count)
            {
                acc = Engine.ConfigUI.ProxyList[ucProxyAccounts.AccountsList.SelectedIndex];
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

        private void AddImageSoftwareToList(Software temp)
        {
            if (temp != null)
            {
                Engine.ConfigUI.ActionsApps.Add(temp);
                lbSoftware.Items.Add(temp);
                lbSoftware.SelectedIndex = lbSoftware.Items.Count - 1;
                RewriteImageEditorsRightClickMenu();
            }
        }

        private void Application_Idle(object sender, EventArgs e)
        {
            DelayedTrimMemoryUse();
        }

        private void autoScreenshotsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowAutoCapture();
        }

        private void btnAddImageSoftware_Click(object sender, EventArgs e)
        {
            Software temp = BrowseApplication();
            if (temp != null)
            {
                AddImageSoftwareToList(temp);
            }
        }

        private void BtnBrowseImagesDirClick(object sender, EventArgs e)
        {
            string oldDir = txtImagesDir.Text;
            string dirNew = Path.Combine(Adapter.GetDirPathUsingFolderBrowser("Configure Custom Images Directory..."),
                                         "Images");

            if (!string.IsNullOrEmpty(dirNew))
            {
                Engine.ConfigUI.UseCustomImagesDir = true;
                Engine.ConfigUI.CustomImagesDir = dirNew;
                FileSystem.MoveDirectory(oldDir, txtImagesDir.Text);
                ZScreen_ConfigGUI_Options_Paths();
            }
        }

        private void btnBrowseRootDir_Click(object sender, EventArgs e)
        {
            string oldRootDir = txtRootFolder.Text;
            string dirNew = Adapter.GetDirPathUsingFolderBrowser("Configure Root directory...");

            if (!string.IsNullOrEmpty(dirNew))
            {
                Engine.SetRootFolder(dirNew);
                txtRootFolder.Text = Engine.ConfigApp.RootDir;
                FileSystem.MoveDirectory(oldRootDir, txtRootFolder.Text);
                ZScreen_ConfigGUI_Options_Paths();
                Engine.ConfigUI = XMLSettings.Read();
                ZScreen_ConfigGUI();
            }
        }

        private void btnCheckUpdate_Click(object sender, EventArgs e)
        {
            CheckUpdates();
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

        private void btnCodes_Click(object sender, EventArgs e)
        {
            var b = (Button) sender;

            const string beginning = "btnCodes";
            string name = b.Name, code;

            if (name.Contains(beginning))
            {
                name = name.Replace(beginning, string.Empty);
                code = "%" + name.ToLower();

                if (mHadFocus != null)
                {
                    mHadFocus.Text = mHadFocus.Text.Insert(mHadFocusAt, code);
                    mHadFocus.Focus();
                    mHadFocus.Select(mHadFocusAt + code.Length, 0);
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

        private void btnDeleteImageSoftware_Click(object sender, EventArgs e)
        {
            int sel = lbSoftware.SelectedIndex;

            if (sel != -1)
            {
                Engine.ConfigUI.ActionsApps.RemoveAt(sel);

                lbSoftware.Items.RemoveAt(sel);

                if (lbSoftware.Items.Count > 0)
                {
                    lbSoftware.SelectedIndex = (sel > 0) ? (sel - 1) : 0;
                }
            }

            RewriteImageEditorsRightClickMenu();
        }

        private void btnDeleteSettings_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you really want to revert settings to default values?", Application.ProductName,
                                MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                LoadSettingsDefault();
            }
        }

        private void btnFtpHelp_Click(object sender, EventArgs e)
        {
            StaticHelper.LoadBrowser("http://code.google.com/p/zscreen/wiki/FTPAccounts");
        }

        private void btnFTPOpenClient_Click(object sender, EventArgs e)
        {
            OpenFTPClient();
        }

        private void btnGalleryImageShack_Click(object sender, EventArgs e)
        {
            StaticHelper.LoadBrowser("http://my.imageshack.us/v_images.php");
        }

        private void btnGalleryTinyPic_Click(object sender, EventArgs e)
        {
            StaticHelper.LoadBrowser("http://tinypic.com/yourstuff.php");
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

        private void btnOutputsConfigExport_Click(object sender, EventArgs e)
        {
            OutputsConfigExport();
        }

        private void btnOutputsConfigImport_Click(object sender, EventArgs e)
        {
            OutputsConfigImport();
        }

        private void btnRegCodeImageShack_Click(object sender, EventArgs e)
        {
            StaticHelper.LoadBrowser("http://profile.imageshack.us/prefs");
        }

        private void btnResetIncrement_Click(object sender, EventArgs e)
        {
            Engine.ConfigWorkflow.AutoIncrement = 0;
        }

        private void btnSelectGradient_Click(object sender, EventArgs e)
        {
            using (var gradient = new GradientMaker(Engine.ConfigWorkflow.GradientMakerOptions))
            {
                gradient.Icon = Icon;
                if (gradient.ShowDialog() == DialogResult.OK)
                {
                    Engine.ConfigWorkflow.GradientMakerOptions = gradient.Options;
                    TestWatermark();
                }
            }
        }

        private void btnSettingsExport_Click(object sender, EventArgs e)
        {
            AppSettingsExport();
        }

        private void btnSettingsImport_Click(object sender, EventArgs e)
        {
            AppSettingsImport();
        }

        private void btnUploadersConfigExport_Click(object sender, EventArgs e)
        {
            UploadersConfigExport();
        }

        private void btnUploadersConfigImport_Click(object sender, EventArgs e)
        {
            UploadersConfigImport();
        }

        private void btnViewLocalDirectory_Click(object sender, EventArgs e)
        {
            ShowDirectory(FileSystem.GetImagesDir());
        }

        private void btnViewRemoteDirectory_Click(object sender, EventArgs e)
        {
            ShowDirectory(Engine.LogsDir);
        }

        private void btnViewRootDir_Click(object sender, EventArgs e)
        {
            ShowDirectory(txtRootFolder.Text);
        }

        private void btnWatermarkFont_Click(object sender, EventArgs e)
        {
            DialogResult result = Adapter.ShowFontDialog();
            if (result == DialogResult.OK)
            {
                pbWatermarkFontColor.BackColor = Engine.ConfigWorkflow.WatermarkFontArgb;
                lblWatermarkFont.Text = FontToString();
                TestWatermark();
            }
        }

        private void btnWorkflowConfig_Click(object sender, EventArgs e)
        {
            var wfwgui = new WorkflowWizardGUIOptions
                             {
                                 ShowQualityTab = true,
                                 ShowResizeTab = true
                             };
            var wfw = new WorkflowWizard(new WorkerTask(Engine.ConfigWorkflow, false), wfwgui) {Icon = Icon};
            wfw.Show();
        }

        private void btwWatermarkBrowseImage_Click(object sender, EventArgs e)
        {
            var fd = new OpenFileDialog
                         {
                             InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop)
                         };
            if (fd.ShowDialog() == DialogResult.OK)
            {
                txtWatermarkImageLocation.Text = fd.FileName;
            }
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

        private void cbAutoSaveSettings_CheckedChanged(object sender, EventArgs e)
        {
            Engine.ConfigUI.AutoSaveSettings = cbAutoSaveSettings.Checked;
        }

        private void cbCheckUpdates_CheckedChanged(object sender, EventArgs e)
        {
            Engine.ConfigUI.CheckUpdates = chkCheckUpdates.Checked;
        }

        private void cbCloseButtonAction_SelectedIndexChanged(object sender, EventArgs e)
        {
            Engine.ConfigApp.WindowButtonActionClose = (WindowButtonAction) cboCloseButtonAction.SelectedIndex;
        }

        private void cbCloseDropBox_CheckedChanged(object sender, EventArgs e)
        {
            Engine.ConfigUI.CloseDropBox = cbCloseDropBox.Checked;
        }

        private void cbCompleteSound_CheckedChanged(object sender, EventArgs e)
        {
            Engine.ConfigUI.CompleteSound = cbCompleteSound.Checked;
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
            Engine.ConfigUI.CropRegionStyles = (RegionStyles) chkCropStyle.SelectedIndex;
        }

        private void cbDeleteLocal_CheckedChanged(object sender, EventArgs e)
        {
            Engine.ConfigUI.DeleteLocal = chkDeleteLocal.Checked;
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

        private void cbHistorySave_CheckedChanged(object sender, EventArgs e)
        {
            Engine.ConfigUI.HistorySave = cbHistorySave.Checked;
        }

        private void cbMinimizeButtonAction_SelectedIndexChanged(object sender, EventArgs e)
        {
            Engine.ConfigApp.WindowButtonActionMinimize = (WindowButtonAction) cboMinimizeButtonAction.SelectedIndex;
        }

        private void cboCaptureEngine_SelectedIndexChanged(object sender, EventArgs e)
        {
            Engine.ConfigWorkflow.CaptureEngineMode2 = (CaptureEngineType) cboCaptureEngine.SelectedIndex;
            UpdateAeroGlassConfig();
        }

        private void cboCropEngine_SelectedIndexChanged(object sender, EventArgs e)
        {
            Engine.ConfigUI.CropEngineMode = (CropEngineType) cboCropEngine.SelectedIndex;
            gbCropRegion.Visible = Engine.ConfigUI.CropEngineMode == CropEngineType.Cropv1;
            gbCropCrosshairSettings.Visible = Engine.ConfigUI.CropEngineMode == CropEngineType.Cropv1;
            gbCropDynamicRegionBorderColorSettings.Visible = Engine.ConfigUI.CropEngineMode == CropEngineType.Cropv1;
            gbCropGridMode.Visible = Engine.ConfigUI.CropEngineMode == CropEngineType.Cropv1;
            gbCropRegionSettings.Visible = Engine.ConfigUI.CropEngineMode == CropEngineType.Cropv1;
            gbCropShotMagnifyingGlass.Visible = Engine.ConfigUI.CropEngineMode == CropEngineType.Cropv1;
        }

        private void cbOpenMainWindow_CheckedChanged(object sender, EventArgs e)
        {
            Engine.ConfigApp.ShowMainWindow = chkOpenMainWindow.Checked;
        }

        private void cboProxyConfig_SelectedIndexChanged(object sender, EventArgs e)
        {
            Engine.ConfigUI.ProxyConfig = (ProxyConfigType) cboProxyConfig.SelectedIndex;
            if (IsReady)
            {
                Uploader.ProxySettings = Adapter.CheckProxySettings();
            }
        }

        private void cboReleaseChannel_SelectedIndexChanged(object sender, EventArgs e)
        {
            Engine.ConfigUI.ReleaseChannel = (ReleaseChannelType) cboReleaseChannel.SelectedIndex;
        }

        private void cboWatermarkType_SelectedIndexChanged(object sender, EventArgs e)
        {
            Engine.ConfigWorkflow.WatermarkMode = (WatermarkType) cboWatermarkType.SelectedIndex;
            TestWatermark();
            tcWatermark.Enabled = Engine.ConfigWorkflow.WatermarkMode != WatermarkType.NONE;
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
            Engine.ConfigUI.SelectedWindowRegionStyles = (RegionStyles) cbSelectedWindowStyle.SelectedIndex;
        }

        private void cbShowCropRuler_CheckedChanged(object sender, EventArgs e)
        {
            Engine.ConfigUI.CropShowRuler = cbShowCropRuler.Checked;
        }

        private void cbShowCursor_CheckedChanged(object sender, EventArgs e)
        {
            Engine.ConfigWorkflow.DrawCursor = chkShowCursor.Checked;
        }

        private void cbShowHelpBalloonTips_CheckedChanged(object sender, EventArgs e)
        {
            Engine.ConfigUI.ShowHelpBalloonTips = cbShowHelpBalloonTips.Checked;
            ttZScreen.Active = Engine.ConfigUI.ShowHelpBalloonTips;
        }

        private void cbShowPopup_CheckedChanged(object sender, EventArgs e)
        {
            Engine.ConfigUI.ShowBalloonTip = cbShowPopup.Checked;
        }

        private void cbShowUploadDuration_CheckedChanged(object sender, EventArgs e)
        {
            Engine.ConfigUI.ShowUploadDuration = cbShowUploadDuration.Checked;
        }

        public void cbStartWin_CheckedChanged(object sender, EventArgs e)
        {
            RegistryHelper.SetStartWithWindows(chkStartWin.Checked);
        }

        private void cbUseCustomGradient_CheckedChanged(object sender, EventArgs e)
        {
            Engine.ConfigWorkflow.WatermarkUseCustomGradient = cboUseCustomGradient.Checked;
            gbGradientMakerBasic.Enabled = !cboUseCustomGradient.Checked;
            TestWatermark();
        }

        private void cbWatermarkAddReflection_CheckedChanged(object sender, EventArgs e)
        {
            Engine.ConfigWorkflow.WatermarkAddReflection = cbWatermarkAddReflection.Checked;
            TestWatermark();
        }

        private void cbWatermarkAutoHide_CheckedChanged(object sender, EventArgs e)
        {
            Engine.ConfigWorkflow.WatermarkAutoHide = cbWatermarkAutoHide.Checked;
            TestWatermark();
        }

        private void cbWatermarkGradientType_SelectedIndexChanged(object sender, EventArgs e)
        {
            Engine.ConfigWorkflow.WatermarkGradientType = (LinearGradientMode) cbWatermarkGradientType.SelectedIndex;
            TestWatermark();
        }

        private void cbWatermarkPosition_SelectedIndexChanged(object sender, EventArgs e)
        {
            Engine.ConfigWorkflow.WatermarkPositionMode = (WatermarkPositionType) chkWatermarkPosition.SelectedIndex;
            TestWatermark();
        }

        private void cbWatermarkUseBorder_CheckedChanged(object sender, EventArgs e)
        {
            Engine.ConfigWorkflow.WatermarkUseBorder = cbWatermarkUseBorder.Checked;
            TestWatermark();
        }

        private void CheckForCodes(object checkObject)
        {
            var textBox = (TextBox) checkObject;
            if (codesMenu.Items.Count > 0)
            {
                codesMenu.Show(textBox, new Point(textBox.Width + 1, 0));
            }
        }

        private void chkActiveWindowDwmCustomColor_CheckedChanged(object sender, EventArgs e)
        {
            Engine.ConfigWorkflow.ActiveWindowDwmUseCustomBackground = chkActiveWindowDwmCustomColor.Checked;
        }

        private void chkActiveWindowTryCaptureChilds_CheckedChanged(object sender, EventArgs e)
        {
            Engine.ConfigWorkflow.ActiveWindowTryCaptureChildren = chkActiveWindowTryCaptureChildren.Checked;
        }

        private void chkBalloonTipOpenLink_CheckedChanged(object sender, EventArgs e)
        {
            Engine.ConfigUI.BalloonTipOpenLink = chkBalloonTipOpenLink.Checked;
        }

        private void chkCaptureFallback_CheckedChanged(object sender, EventArgs e)
        {
            Engine.ConfigUI.CaptureEntireScreenOnError = chkCaptureFallback.Checked;
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

        private void chkOverwriteFiles_CheckedChanged(object sender, EventArgs e)
        {
            Engine.ConfigWorkflow.OverwriteFiles = chkOverwriteFiles.Checked;
        }

        private void chkShellExt_CheckedChanged(object sender, EventArgs e)
        {
            if (chkShellExt.Checked)
            {
                RegistryHelper.RegisterShellContextMenu();
            }
            else
            {
                RegistryHelper.UnregisterShellContextMenu();
            }
        }

        private void chkShortenURL_CheckedChanged(object sender, EventArgs e)
        {
            Engine.ConfigUI.ShortenUrlAfterUpload = chkShortenURL.Checked;
        }

        private void chkShowUploadResults_CheckedChanged(object sender, EventArgs e)
        {
            Engine.ConfigUI.ShowUploadResultsWindow = chkShowUploadResults.Checked;
        }

        private void chkTwitterEnable_CheckedChanged(object sender, EventArgs e)
        {
            Engine.ConfigUI.TwitterEnabled = chkTwitterEnable.Checked;
        }

        private void chkWindows7TaskbarIntegration_CheckedChanged(object sender, EventArgs e)
        {
            if (IsReady)
            {
                if (chkWindows7TaskbarIntegration.Checked)
                {
                    Engine.ConfigApp.ShowInTaskbar = true;
                        // Application requires to be shown in Taskbar for Windows 7 Integration
                }
                Engine.ConfigApp.Windows7TaskbarIntegration = chkWindows7TaskbarIntegration.Checked;
                ZScreen_Windows7onlyTasks();
            }
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

        private void lbSoftware_SelectedIndexChanged(object sender, EventArgs e)
        {
            ShowImageEditorsSettings();
        }

        private void LbSoftwareItemCheck(object sender, ItemCheckEventArgs e)
        {
            UpdateGuiEditors(sender);
        }

        private void LbSoftwareMouseClick(object sender, MouseEventArgs e)
        {
            int sel = lbSoftware.IndexFromPoint(e.X, e.Y);
            if (sel != -1)
            {
                // The following lines have been commented out because of unusual check/uncheck behavior
                // MessageBox.Show(lbSoftware.GetItemChecked(sel).ToString());
                // lbSoftware.SetItemChecked(sel, !lbSoftware.GetItemChecked(sel));
            }
        }

        private void llblBugReports_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            StaticHelper.LoadBrowser(ZLinks.URL_ISSUES);
        }

        private void llProjectPage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            StaticHelper.LoadBrowser(ZLinks.URL_WIKIPAGES);
        }

        private void LoadSettingsDefault()
        {
            Engine.ConfigUI = new XMLSettings();
            ZScreen_ConfigGUI();
            Engine.ConfigUI.FirstRun = false;
        }

        private void nudCropBorderSize_ValueChanged(object sender, EventArgs e)
        {
            Engine.ConfigUI.CropBorderSize = nudCropBorderSize.Value;
        }

        private void nudCropGridHeight_ValueChanged(object sender, EventArgs e)
        {
            Engine.ConfigUI.CropGridSize.Height = (int) nudCropGridHeight.Value;
        }

        private void nudCropGridSize_ValueChanged(object sender, EventArgs e)
        {
            Engine.ConfigUI.CropGridSize.Width = (int) nudCropGridWidth.Value;
        }

        private void nudCropHueRange_ValueChanged(object sender, EventArgs e)
        {
            Engine.ConfigUI.CropHueRange = nudCropHueRange.Value;
        }

        private void nudCropInterval_ValueChanged(object sender, EventArgs e)
        {
            Engine.ConfigUI.CropInterval = (int) nudCropCrosshairInterval.Value;
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
            Engine.ConfigUI.CropStep = (int) nudCropCrosshairStep.Value;
        }

        private void nudCrosshairLineCount_ValueChanged(object sender, EventArgs e)
        {
            Engine.ConfigUI.CrosshairLineCount = (int) nudCrosshairLineCount.Value;
        }

        private void nudCrosshairLineSize_ValueChanged(object sender, EventArgs e)
        {
            Engine.ConfigUI.CrosshairLineSize = (int) nudCrosshairLineSize.Value;
        }

        private void nudFlashIconCount_ValueChanged(object sender, EventArgs e)
        {
            Engine.ConfigUI.FlashTrayCount = nudFlashIconCount.Value;
        }

        private void nudHistoryMaxItems_ValueChanged(object sender, EventArgs e)
        {
            Engine.ConfigUI.HistoryMaxNumber = (int) nudHistoryMaxItems.Value;
        }

        private void nudMaxNameLength_ValueChanged(object sender, EventArgs e)
        {
            Engine.ConfigWorkflow.MaxNameLength = (int) nudMaxNameLength.Value;
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

        private void nudWatermarkBackTrans_ValueChanged(object sender, EventArgs e)
        {
            Engine.ConfigWorkflow.WatermarkBackTrans = nudWatermarkBackTrans.Value;
            trackWatermarkBackgroundTrans.Value = (int) nudWatermarkBackTrans.Value;
        }

        private void nudWatermarkCornerRadius_ValueChanged(object sender, EventArgs e)
        {
            Engine.ConfigWorkflow.WatermarkCornerRadius = nudWatermarkCornerRadius.Value;
            TestWatermark();
        }

        private void nudWatermarkFontTrans_ValueChanged(object sender, EventArgs e)
        {
            Engine.ConfigWorkflow.WatermarkFontTrans = nudWatermarkFontTrans.Value;
            trackWatermarkFontTrans.Value = (int) nudWatermarkFontTrans.Value;
        }

        private void nudWatermarkImageScale_ValueChanged(object sender, EventArgs e)
        {
            Engine.ConfigWorkflow.WatermarkImageScale = nudWatermarkImageScale.Value;
            TestWatermark();
        }

        private void nudWatermarkOffset_ValueChanged(object sender, EventArgs e)
        {
            Engine.ConfigWorkflow.WatermarkOffset = nudWatermarkOffset.Value;
            TestWatermark();
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
                    var ftpClient = new FTPClient2(acc) {Icon = Icon};
                    ftpClient.Show();
                }
            }
        }

        private void pbActiveWindowDwmBackColor_Click(object sender, EventArgs e)
        {
            SelectColor((PictureBox) sender, ref Engine.ConfigWorkflow.ActiveWindowDwmBackColor);
        }

        private void pbCropBorderColor_Click(object sender, EventArgs e)
        {
            SelectColor((PictureBox) sender, ref Engine.ConfigUI.CropBorderArgb);
        }

        private void pbCropCrosshairColor_Click(object sender, EventArgs e)
        {
            SelectColor((PictureBox) sender, ref Engine.ConfigUI.CropCrosshairArgb);
        }

        private void pbSelectedWindowBorderColor_Click(object sender, EventArgs e)
        {
            SelectColor((PictureBox) sender, ref Engine.ConfigUI.SelectedWindowBorderArgb);
        }

        private void pbWatermarkBorderColor_Click(object sender, EventArgs e)
        {
            SelectColor((PictureBox) sender, ref Engine.ConfigWorkflow.WatermarkBorderArgb);
            TestWatermark();
        }

        private void pbWatermarkFontColor_Click(object sender, EventArgs e)
        {
            SelectColor((PictureBox) sender, ref Engine.ConfigWorkflow.WatermarkFontArgb);
            lblWatermarkFont.Text = FontToString();
            TestWatermark();
        }

        private void pbWatermarkGradient1_Click(object sender, EventArgs e)
        {
            SelectColor((PictureBox) sender, ref Engine.ConfigWorkflow.WatermarkGradient1Argb);
            TestWatermark();
        }

        private void pbWatermarkGradient2_Click(object sender, EventArgs e)
        {
            SelectColor((PictureBox) sender, ref Engine.ConfigWorkflow.WatermarkGradient2Argb);
            TestWatermark();
        }

        private void pgAppConfig_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            ZScreen_ConfigGUI();
        }

        private void pgAppSettings_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            if (IsReady)
            {
                ZScreen_ConfigGUI_Options_Paths();

                if (!string.IsNullOrEmpty(Engine.ConfigApp.UploadersConfigCustomPath))
                {
                    Engine.ConfigUploaders = UploadersConfig.Read(Engine.ConfigApp.UploadersConfigCustomPath);
                }

                if (!string.IsNullOrEmpty(Engine.ConfigApp.WorkflowConfigCustomPath))
                {
                    Engine.ConfigWorkflow = Workflow.Read(Engine.ConfigApp.WorkflowConfigCustomPath);
                }
            }
        }

        private void pgEditorsImage_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            Software temp = Engine.ConfigUI.ActionsApps[lbSoftware.SelectedIndex];
            lbSoftware.Items[lbSoftware.SelectedIndex] = temp;
            Engine.ConfigUI.ActionsApps[lbSoftware.SelectedIndex] = temp;
            RewriteImageEditorsRightClickMenu();
        }

        private void ProxySetup(IEnumerable<ProxyInfo> accs)
        {
            if (accs != null)
            {
                ucProxyAccounts.AccountsList.Items.Clear();
                Engine.ConfigUI.ProxyList = new List<ProxyInfo>();
                Engine.ConfigUI.ProxyList.AddRange(accs);
                foreach (ProxyInfo acc in Engine.ConfigUI.ProxyList)
                {
                    ucProxyAccounts.AccountsList.Items.Add(acc);
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

        private void SetActiveImageSoftware()
        {
            Engine.ConfigUI.ImageEditor = Engine.ConfigUI.ActionsApps[lbSoftware.SelectedIndex];
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

        private void ShowImageEditorsSettings()
        {
            if (lbSoftware.SelectedItem != null)
            {
                Software app = GetImageSoftware(lbSoftware.SelectedItem.ToString());
                if (app != null)
                {
                    Engine.ConfigUI.ActionsApps[lbSoftware.SelectedIndex].Enabled =
                        lbSoftware.GetItemChecked(lbSoftware.SelectedIndex);
                    pgEditorsImage.SelectedObject = app;
                    pgEditorsImage.Enabled = !app.Protected;
                    btnActionsRemove.Enabled = !app.Protected;
                    SetActiveImageSoftware();
                }
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

        private void TestWatermark()
        {
            using (Bitmap bmp = Resources.main.Clone(new Rectangle(62, 33, 199, 140), PixelFormat.Format32bppArgb))
            {
                var bmp2 = new Bitmap(pbWatermarkShow.ClientRectangle.Width, pbWatermarkShow.ClientRectangle.Height);
                Graphics g = Graphics.FromImage(bmp2);
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.DrawImage(bmp,
                            new Rectangle(0, 0, pbWatermarkShow.ClientRectangle.Width,
                                          pbWatermarkShow.ClientRectangle.Height));
                pbWatermarkShow.Image = new ImageEffects(Engine.ConfigWorkflow).ApplyWatermark(bmp2);
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
            var fileDirPaths = (string[]) e.Data.GetData(DataFormats.FileDrop, true);
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
            var ddfilePaths = (string[]) e.Data.GetData(DataFormats.FileDrop, true);
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

        private void trackWatermarkBackgroundTrans_Scroll(object sender, EventArgs e)
        {
            Engine.ConfigWorkflow.WatermarkBackTrans = trackWatermarkBackgroundTrans.Value;
            nudWatermarkBackTrans.Value = Engine.ConfigWorkflow.WatermarkBackTrans;
            TestWatermark();
        }

        private void trackWatermarkFontTrans_Scroll(object sender, EventArgs e)
        {
            Engine.ConfigWorkflow.WatermarkFontTrans = trackWatermarkFontTrans.Value;
            nudWatermarkFontTrans.Value = Engine.ConfigWorkflow.WatermarkFontTrans;
            TestWatermark();
        }

        private void tsbDonate_Click(object sender, EventArgs e)
        {
            StaticHelper.LoadBrowser(ZLinks.URL_DONATE_ZS);
        }

        private void tsbDonate_MouseEnter(object sender, EventArgs e)
        {
            Cursor = Cursors.Hand;
        }

        private void tsbLinkHelp_Click(object sender, EventArgs e)
        {
            StaticHelper.LoadBrowser(ZLinks.URL_WIKIPAGES);
        }

        private void tsbLinkHome_Click(object sender, EventArgs e)
        {
            StaticHelper.LoadBrowser(ZLinks.URL_WEBSITE);
        }

        private void tsbLinkIssues_Click(object sender, EventArgs e)
        {
            StaticHelper.LoadBrowser(ZLinks.URL_ISSUES);
        }

        public void tsddbSelectedWindow_DropDownOpening(object sender, EventArgs e)
        {
            CaptureSelectedWindowGetList();
        }

        private void tsiSelectedWindow_Click(object sender, EventArgs e)
        {
            var tsi = (ToolStripItem) sender;
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

        private void tsmEditinImageSoftware_CheckedChanged(object sender, EventArgs e)
        {
            chkPerformActions.Checked = tsmEditinImageSoftware.Checked;
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
            ShowWindow();
        }

        private void tsmUploadFromClipboard_Click(object sender, EventArgs e)
        {
            ClipboardUpload();
        }

        private void tsmViewDirectory_Click(object sender, EventArgs e)
        {
            ShowDirectory(FileSystem.GetImagesDir());
        }

        private void txtActiveWindow_Leave(object sender, EventArgs e)
        {
            mHadFocus = (TextBox) sender;
            mHadFocusAt = ((TextBox) sender).SelectionStart;
        }

        private void txtActiveWindow_TextChanged(object sender, EventArgs e)
        {
            Engine.ConfigWorkflow.ActiveWindowPattern = txtActiveWindow.Text;
            var parser = new NameParser(NameParserType.ActiveWindow)
                             {
                                 CustomProductName = Engine.GetProductName(),
                                 IsPreview = true,
                                 MaxNameLength = Engine.ConfigWorkflow.MaxNameLength
                             };
            lblActiveWindowPreview.Text = parser.Convert(Engine.ConfigWorkflow.ActiveWindowPattern);
        }

        private void txtDebugLog_LinkClicked(object sender, LinkClickedEventArgs e)
        {
            StaticHelper.LoadBrowser(e.LinkText);
        }

        private void txtEntireScreen_Leave(object sender, EventArgs e)
        {
            mHadFocus = (TextBox) sender;
            mHadFocusAt = ((TextBox) sender).SelectionStart;
        }

        private void txtEntireScreen_TextChanged(object sender, EventArgs e)
        {
            Engine.ConfigWorkflow.EntireScreenPattern = txtEntireScreen.Text;
            var parser = new NameParser(NameParserType.EntireScreen)
                             {
                                 CustomProductName = Engine.GetProductName(),
                                 IsPreview = true,
                                 MaxNameLength = Engine.ConfigWorkflow.MaxNameLength
                             };
            lblEntireScreenPreview.Text = parser.Convert(Engine.ConfigWorkflow.EntireScreenPattern);
        }

        private void txtImagesFolderPattern_TextChanged(object sender, EventArgs e)
        {
            Engine.ConfigWorkflow.SaveFolderPattern = txtImagesFolderPattern.Text;
            lblImagesFolderPatternPreview.Text =
                new NameParser(NameParserType.SaveFolder).Convert(Engine.ConfigWorkflow.SaveFolderPattern);
            txtImagesDir.Text = Engine.ImagesDir;
        }

        private void txtWatermarkImageLocation_TextChanged(object sender, EventArgs e)
        {
            if (File.Exists(txtWatermarkImageLocation.Text))
            {
                Engine.ConfigWorkflow.WatermarkImageLocation = txtWatermarkImageLocation.Text;
                TestWatermark();
            }
        }

        private void txtWatermarkText_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && codesMenu.Visible)
            {
                codesMenu.Close();
            }
        }

        private void txtWatermarkText_Leave(object sender, EventArgs e)
        {
            if (codesMenu.Visible)
            {
                codesMenu.Close();
            }
        }

        private void txtWatermarkText_MouseDown(object sender, MouseEventArgs e)
        {
            CheckForCodes(sender);
        }

        private void txtWatermarkText_TextChanged(object sender, EventArgs e)
        {
            Engine.ConfigWorkflow.WatermarkText = txtWatermarkText.Text;
            TestWatermark();
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
            if (cboCaptureEngine.SelectedIndex == (int) CaptureEngineType.DWM)
            {
                chkActiveWindowTryCaptureChildren.Checked = false;
            }
            chkActiveWindowTryCaptureChildren.Enabled = cboCaptureEngine.SelectedIndex != (int) CaptureEngineType.DWM;
        }

        private void UpdateGuiEditors(object sender)
        {
            if (IsReady)
            {
                if (sender.GetType() == lbSoftware.GetType())
                {
                    // the checked state needs to be inversed for some weird reason to get it working properly
                    if (Engine.ConfigUI.ActionsApps.HasValidIndex(lbSoftware.SelectedIndex))
                    {
                        Engine.ConfigUI.ActionsApps[lbSoftware.SelectedIndex].Enabled =
                            !lbSoftware.GetItemChecked(lbSoftware.SelectedIndex);
                        var tsm = tsmEditinImageSoftware.DropDownItems[lbSoftware.SelectedIndex] as ToolStripMenuItem;
                        tsm.Checked = Engine.ConfigUI.ActionsApps[lbSoftware.SelectedIndex].Enabled;
                    }
                }
                else if (sender.GetType() == typeof (ToolStripMenuItem))
                {
                    var tsm = sender as ToolStripMenuItem;
                    var sel = (int) tsm.Tag;
                    if (Engine.ConfigUI.ActionsApps.HasValidIndex(sel))
                    {
                        Engine.ConfigUI.ActionsApps[sel].Enabled = tsm.Checked;
                        lbSoftware.SetItemChecked(lbSoftware.SelectedIndex, tsm.Checked);
                    }
                }
            }
        }

        private void watermarkCodeMenu_Click(object sender, EventArgs e)
        {
            var tsi = (ToolStripMenuItem) sender;
            int oldPos = txtWatermarkText.SelectionStart;
            string appendText;
            if (oldPos > 0 && txtWatermarkText.Text[txtWatermarkText.SelectionStart - 1] == ReplacementExtension.Prefix)
            {
                appendText = tsi.Tag.ToString().TrimStart('%');
                txtWatermarkText.Text =
                    txtWatermarkText.Text.Insert(txtWatermarkText.SelectionStart, appendText);
                txtWatermarkText.Select(oldPos + appendText.Length, 0);
            }
            else
            {
                appendText = tsi.Tag.ToString();
                txtWatermarkText.Text =
                    txtWatermarkText.Text.Insert(txtWatermarkText.SelectionStart, appendText);
                txtWatermarkText.Select(oldPos + appendText.Length, 0);
            }
        }
    }
}
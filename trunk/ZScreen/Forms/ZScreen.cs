#region License Information (GPL v2)
/*
    ZScreen - A program that allows you to upload screenshots in one keystroke.
    Copyright (C) 2008-2009  Brandon Zimmerman

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
#endregion

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Microsoft.Win32;
using ZSS.Forms;
using ZSS.Helpers;
using ZSS.ImageUploader;
using ZSS.ImageUploader.Helpers;
using ZSS.Properties;
using ZSS.Tasks;
using ZSS.Colors;

namespace ZSS
{
    public partial class ZScreen : Form
    {
        #region Global Variables

        private bool mGuiIsReady = false;
        private bool mClose = false;
        private bool mDoingNameUpdate = false;
        private bool mTakingScreenShot = false;
        private bool mSetHotkeys = false;
        private int mHKSelectedRow = -1;
        private HKcombo mHKSetcombo;
        //Used for appending Screenshot URL when taking more than on Screenshot in a short time
        //private uint mNumWorkers = 0;
        //private List<string> mClipboardURLs = new List<string>();
        //Used for Click-to-insert Codes (Naming Conventions)
        private TextBox mHadFocus;
        private int mHadFocusAt;
        public IntPtr m_hID = (IntPtr)1; //Used for the keyboard hook
        private const int mWM_KEYDOWN = 0x0100;
        private const int mWM_SYSKEYDOWN = 0x0104;
        private bool bQuickOptionsOpened = false;
        private bool bDropWindowOpened = false;
        public int startHeight;
        //public List<Control> ZScreenControls;
        public ContextMenuStrip codesMenu = new ContextMenuStrip();
        private GoogleTranslate mGTranslator = null;
        private BackgroundWorker bwActiveHelp = new BackgroundWorker();
        public Debug debug;

        #endregion

        public ZScreen()
        {
            InitializeComponent();

            // Set height when program is launched
            startHeight = this.Height;

            // Set Icon
            this.Icon = Properties.Resources.zss_main;

            // Set Name            
            lblLogo.Text = Program.mAppInfo.GetApplicationTitle(McoreSystem.AppInfo.VersionDepth.MajorMinorBuildRevision);
            this.Text = Program.mAppInfo.GetApplicationTitle(McoreSystem.AppInfo.VersionDepth.MajorMinorBuild);
            this.niTray.Text = this.Text;

            // Update GUI Controls
            SetupScreen();

            CleanCache();

            //show settings if never ran before
            if (Program.conf.RunOnce == false)
            {
                Show();
                WindowState = FormWindowState.Normal;
                this.Activate();
                this.BringToFront();
                //Form.ActiveForm.BringToFront();

                lblFirstRun.Visible = true;

                Program.conf.activeWindow = Properties.Resources.activeWindowDefault;
                Program.conf.entireScreen = Properties.Resources.entireScreenDefault;

                Program.conf.RunOnce = true;
            }

            if (Program.conf.CheckUpdates) UpdateChecker.CheckUpdates();

            debug = new Debug();
            debugTimer.Start();
        }

        private void ZScreen_Load(object sender, EventArgs e)
        {
            if (Program.conf.OpenMainWindow)
            {
                this.WindowState = FormWindowState.Normal;
                this.ShowInTaskbar = Program.conf.ShowInTaskbar;
            }
            else
            {
                this.Hide();
            }

            niTray.BalloonTipClicked += new EventHandler(niTray_BalloonTipClicked);
            AddToClipboardByDoubleClick(tpScreenshots);

            // Set Active Help Tags
            ActiveHelpTagsConfig();
            AddMouseHoverEventHandlerHelp(Controls);

            //LoadTranslations("English");

            FillClipboardCopyMenu();
            FillClipboardMenu();

            CreateCodesMenu();

            //Need better solution for this
            dgvHotkeys.BackgroundColor = Color.FromArgb(tpHotKeys.BackColor.R, tpHotKeys.BackColor.G, tpHotKeys.BackColor.B);
        }

        private void SetupScreen()
        {
            //////////////////////////////
            // Global
            /////////////////////////////            
            Program.ConfigureDirs();

            //////////////////////////////
            // Configure Settings
            /////////////////////////////
            // Configure FTP Accounts List
            //if (Program.conf.FTPAccountList == null)
            //{
            //    Program.conf.FTPAccountList = new List<FTPAccount>();
            //}
            //if (Program.conf.FTPAccountList.Count == 0)
            //{
            //    Program.conf.FTPAccountList.Add(new FTPAccount(Resources.NewAccount));
            //}
            // Configure ImageSoftware List
            //if (Program.conf.ImageSoftwareList == null)
            //{
            //    Program.conf.ImageSoftwareList = new List<ImageSoftware>();
            //}
            if (Program.conf.ImageSoftwareActive == null)
            {
                Program.conf.ImageSoftwareActive = new ImageSoftware();
                Program.conf.ImageSoftwareActive.Name = "MS Paint";
                Program.conf.ImageSoftwareActive.Path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.System), "mspaint.exe");
            }
            if (Program.conf.ImageSoftwareList.Count == 0)
            {
                Program.conf.ImageSoftwareList.Add(Program.conf.ImageSoftwareActive);
            }
            FindImageSoftwares();

            ///////////////////////////////////
            // Main Tab
            //////////////////////////////////

            txtActiveHelp.Text = String.Format("Welcome to {0}. To begin using Active Help all you need to do is hover over any control and this textbox will be updated with information about the control.", this.ProductName);

            cboScreenshotDest.Items.Clear();
            foreach (ImageDestType sdt in Enum.GetValues(typeof(ImageDestType)))
            {
                cboScreenshotDest.Items.Add(sdt.ToDescriptionString());
            }

            cbWatermarkPosition.Items.Clear();
            foreach (WatermarkPositionType wmt in Enum.GetValues(typeof(WatermarkPositionType)))
            {
                cbWatermarkPosition.Items.Add(wmt.ToDescriptionString());
            }

            cboClipboardTextMode.Items.Clear();
            foreach (ClipboardUriType cut in Enum.GetValues(typeof(ClipboardUriType)))
            {
                cboClipboardTextMode.Items.Add(cut.ToDescriptionString());
            }
            cboScreenshotDest.SelectedIndex = (int)Program.conf.ScreenshotDestMode;

            nScreenshotDelay.Value = Program.conf.ScreenshotDelay;
            cbRegionRectangleInfo.Checked = Program.conf.RegionRectangleInfo;
            cbRegionHotkeyInfo.Checked = Program.conf.RegionHotkeyInfo;
            cbActiveHelp.Checked = Program.conf.ActiveHelp;
            cbCropStyle.SelectedIndex = Program.conf.CropStyle;
            pbCropBorderColor.BackColor = XMLSettings.DeserializeColor(Program.conf.CropBorderColor);
            nudCropBorderSize.Value = Program.conf.CropBorderSize;
            cbCompleteSound.Checked = Program.conf.CompleteSound;
            cbShowCursor.Checked = Program.conf.ShowCursor;
            chkGTActiveHelp.Checked = Program.conf.GTActiveHelp;
            cbSelectedWindowFront.Checked = Program.conf.SelectedWindowFront;
            cbSelectedWindowRectangleInfo.Checked = Program.conf.SelectedWindowRectangleInfo;
            pbSelectedWindowBorderColor.BackColor = XMLSettings.DeserializeColor(Program.conf.SelectedWindowBorderColor);
            nudSelectedWindowBorderSize.Value = Program.conf.SelectedWindowBorderSize;

            ///////////////////////////////////
            // Hotkeys Settings
            ///////////////////////////////////

            UpdateHotkeysDGV();

            ///////////////////////////////////
            // FTP Settings
            ///////////////////////////////////

            if (Program.conf.FTPAccountList == null || Program.conf.FTPAccountList.Count == 0)
            {
                FTPSetup(new List<FTPAccount>());
                FTPLoad(new FTPAccount());
            }
            else
            {
                FTPSetup(Program.conf.FTPAccountList);
                if (lbFTPAccounts.Items.Count > 0)
                {
                    lbFTPAccounts.SelectedIndex = Program.conf.FTPselected;
                }
            }

            ///////////////////////////////////
            // HTTP Settings
            ///////////////////////////////////
            txtImageShackRegistrationCode.Text = Program.conf.ImageShackRegistrationCode;
            txtTinyPicShuk.Text = Program.conf.TinyPicShuk;
            nErrorRetry.Value = Program.conf.ErrorRetryCount;
            cboUploadMode.Items.Clear();
            foreach (UploadMode um in Enum.GetValues(typeof(UploadMode)))
            {
                cboUploadMode.Items.Add(um.ToDescriptionString());
            }
            cboUploadMode.SelectedIndex = (int)Program.conf.UploadMode;
            chkImageUploadRetry.Checked = Program.conf.ImageUploadRetry;
            cbAutoSwitchFTP.Checked = Program.conf.AutoSwitchFTP;

            if (cbFromLanguage.Items.Count == 0 || cbToLanguage.Items.Count == 0 || cbHelpToLanguage.Items.Count == 0)
                DownloadLanguagesList();
            cbClipboardTranslate.Checked = Program.conf.ClipboardTranslate;

            ///////////////////////////////////
            // Image Software Settings
            ///////////////////////////////////
            //Add "Disabled" to the top of the Image Software List
            lbImageSoftware.Items.Clear();
            lbImageSoftware.Items.Add(Properties.Resources.Disabled);
            foreach (ImageSoftware app in Program.conf.ImageSoftwareList)
            {
                lbImageSoftware.Items.Add(app.Name);
            }

            int i;
            if (Program.conf.ISenabled)
            {
                if ((i = lbImageSoftware.Items.IndexOf(Program.conf.ImageSoftwareActive.Name)) != -1)
                    lbImageSoftware.SelectedIndex = i;
            }
            else
            {
                //Set to disabled
                lbImageSoftware.SelectedIndex = 0;
            }
            txtImageSoftwarePath.Enabled = false;
            //cbRunImageSoftware.Checked = Program.conf.ISenabled;


            ///////////////////////////////////
            // Main/File Settings
            ///////////////////////////////////  
            chkEnableThumbnail.Checked = Program.conf.EnableThumbnail;
            cboClipboardTextMode.SelectedIndex = (int)Program.conf.ClipboardUriMode;
            //updateClipboardTextTrayMenu();

            // 
            //check start with windows
            cbStartWin.Checked = CheckStartWithWindows();

            ///////////////////////////////////
            // localFilePath Settings
            ///////////////////////////////////
            //gbAutoFileName.Enabled = !chkManualNaming.Checked;
            //gbCodeTitle.Enabled = !chkManualNaming.Checked;
            txtFileDirectory.Text = Program.conf.ImagesDir;

            //file settings
            txtActiveWindow.Text = Program.conf.activeWindow;
            txtEntireScreen.Text = Program.conf.entireScreen;

            cmbFileFormat.Items.AddRange(Program.mFileTypes);
            cmbSwitchFormat.Items.AddRange(Program.mFileTypes);

            cmbFileFormat.SelectedIndex = Program.conf.FileFormat;
            nudSwitchAfter.Text = Program.conf.SwitchAfter.ToString();
            cmbSwitchFormat.SelectedIndex = Program.conf.SwitchFormat;
            txtImageQuality.Text = Program.conf.ImageQuality.ToString();
            chkManualNaming.Checked = Program.conf.ManualNaming;

            //tsmSaveToClip.Checked = Program.conf.ScreenshotDestMode == ScreenshotDestType.CLIPBOARD;
            cbShowWatermark.Checked = Program.conf.ShowWatermark;

            txtWatermarkText.Text = Program.conf.WatermarkText;
            pbWatermarkFontColor.BackColor = XMLSettings.DeserializeColor(Program.conf.WatermarkFontColor);
            lblWatermarkFont.Text = FontToString();
            nudWatermarkFontTrans.Value = Program.conf.WatermarkFontTrans;
            nudWatermarkOffset.Value = Program.conf.WatermarkOffset;
            nudWatermarkBackTrans.Value = Program.conf.WatermarkBackTrans;
            pbWatermarkGradient1.BackColor = XMLSettings.DeserializeColor(Program.conf.WatermarkGradient1);
            pbWatermarkGradient2.BackColor = XMLSettings.DeserializeColor(Program.conf.WatermarkGradient2);
            pbWatermarkBorderColor.BackColor = XMLSettings.DeserializeColor(Program.conf.WatermarkBorderColor);
            cbWatermarkPosition.SelectedIndex = (int)Program.conf.WatermarkPositionMode;
            nudWatermarkCornerRadius.Value = Program.conf.WatermarkCornerRadius;
            if (cbWatermarkGradientType.Items.Count == 0)
            {
                cbWatermarkGradientType.Items.AddRange(Enum.GetNames(typeof(LinearGradientMode)));
            }
            cbWatermarkGradientType.SelectedIndex = cbWatermarkGradientType.FindStringExact(Program.conf.WatermarkGradientType);
            TestWatermark();

            ///////////////////////////////////
            // Advanced Settings
            ///////////////////////////////////

            txtCacheDir.Text = Program.conf.CacheDir;
            //advanced
            txtCacheDir.Text = Program.conf.CacheDir;
            nudCacheSize.Value = Program.conf.ScreenshotCacheSize;
            nudFlashIconCount.Value = Program.conf.FlashTrayCount;
            cbShowPopup.Checked = Program.conf.ShowBalloonTip;
            chkBalloonTipOpenLink.Checked = Program.conf.BalloonTipOpenLink;
            cbCheckUpdates.Checked = Program.conf.CheckUpdates;
            cbCheckExperimental.Enabled = Program.conf.CheckUpdates;
            cbOpenMainWindow.Checked = Program.conf.OpenMainWindow;
            cbShowTaskbar.Checked = Program.conf.ShowInTaskbar;
            cbDeleteLocal.Checked = Program.conf.DeleteLocal;
            cbCheckExperimental.Checked = Program.conf.CheckExperimental;

            ///////////////////////////////////
            // Image Uploaders
            ///////////////////////////////////

            lbUploader.Items.Clear();

            if (Program.conf.ImageUploadersList == null)
            {
                Program.conf.ImageUploadersList = new List<ImageHostingService>();
                LoadImageUploaders(new ImageHostingService());
            }
            else
            {
                List<ImageHostingService> iUploaders = Program.conf.ImageUploadersList;

                foreach (ImageHostingService iUploader in iUploaders)
                {
                    lbUploader.Items.Add(iUploader.Name);
                }
                if (lbUploader.Items.Count > 0)
                {
                    lbUploader.SelectedIndex = Program.conf.ImageUploaderSelected;
                }
                if (lbUploader.SelectedIndex != -1)
                {
                    LoadImageUploaders(Program.conf.ImageUploadersList[lbUploader.SelectedIndex]);
                }
            }
        }

        private bool CheckKeys(HKcombo hkc, IntPtr lParam)
        {
            if (hkc.Mods == null) //0 mods
            {
                if (Control.ModifierKeys == Keys.None && (Keys)Marshal.ReadInt32(lParam) == hkc.Key)
                    return true;
            }
            else // if(hkc.Mods.Length > 0)
            {
                if (hkc.Mods.Length == 1)
                {
                    if (Control.ModifierKeys == hkc.Mods[0] && (Keys)Marshal.ReadInt32(lParam) == hkc.Key)
                        return true;
                }
                else //if (hkc.Mods.Length == 2)
                {
                    if (Control.ModifierKeys == (hkc.Mods[0] | hkc.Mods[1]) && (Keys)Marshal.ReadInt32(lParam) == hkc.Key)
                        return true;
                }
            }

            return false;
        }

        private HKcombo GetHKcombo(IntPtr lParam)
        {
            try
            {
                string[] mods = Control.ModifierKeys.ToString().Split(',');

                if (mods == null || Control.ModifierKeys == Keys.None)
                {
                    return new HKcombo((Keys)Marshal.ReadInt32(lParam));
                }
                else if (mods.Length == 1)
                {
                    return new HKcombo((Keys)Enum.Parse(typeof(Keys), mods[0], true), (Keys)Marshal.ReadInt32(lParam));
                }
                else if (mods.Length == 2)
                {
                    return new HKcombo((Keys)Enum.Parse(typeof(Keys), mods[0], true), (Keys)Enum.Parse(typeof(Keys), mods[1], true), (Keys)Marshal.ReadInt32(lParam));
                }
            }
            catch { }

            return null;
            //(Keys)Marshal.ReadInt32(lParam)
        }

        public IntPtr ScreenshotUsingHotkeys(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode >= 0 && (wParam == (IntPtr)mWM_KEYDOWN || wParam == (IntPtr)mWM_SYSKEYDOWN))
            {
                if (mSetHotkeys)
                {
                    if ((Keys)Marshal.ReadInt32(lParam) == Keys.Enter)
                    {
                        QuitSettingHotkeys();
                    }
                    else if ((Keys)Marshal.ReadInt32(lParam) == Keys.Escape)
                    {
                        HKcombo hkc = new HKcombo(Keys.None);
                        mHKSetcombo = hkc;
                        dgvHotkeys.Rows[mHKSelectedRow].Cells[1].Value = hkc;
                        SetHotkey(mHKSelectedRow, hkc);
                    }
                    else
                    {
                        mHKSetcombo = GetHKcombo(lParam);
                        dgvHotkeys.Rows[mHKSelectedRow].Cells[1].Value = mHKSetcombo;
                        SetHotkey(mHKSelectedRow, mHKSetcombo);
                    }
                }
                else
                {
                    if (CheckKeys(Program.conf.HKActiveWindow, lParam))
                    {
                        //Active window
                        StartWorkerScreenshots(MainAppTask.Jobs.TAKE_SCREENSHOT_WINDOW_ACTIVE);
                        return m_hID;
                    }
                    else if (CheckKeys(Program.conf.HKSelectedWindow, lParam))
                    {
                        //Selected Window
                        StartBW_SelectedWindow();
                        return m_hID;
                    }
                    else if (CheckKeys(Program.conf.HKCropShot, lParam))
                    {
                        //Crop Shot
                        StartBW_CropShot();
                        return m_hID;
                    }
                    else if (CheckKeys(Program.conf.HKLastCropShot, lParam))
                    {
                        //Last Crop Shot
                        StartBW_LastCropShot();
                        return m_hID;
                    }
                    else if (CheckKeys(Program.conf.HKEntireScreen, lParam))
                    {
                        //Entire Screen
                        StartBW_EntireScreen();
                        return m_hID;
                    }
                    else if (CheckKeys(Program.conf.HKClipboardUpload, lParam))
                    {
                        //Clipboard Upload
                        ScreenshotUsingClipboard();
                        return m_hID;
                    }
                    else if (CheckKeys(Program.conf.HKDropWindow, lParam))
                    {
                        //Drop Window
                        ShowDropWindow();
                        return m_hID;
                    }
                    else if (CheckKeys(Program.conf.HKQuickOptions, lParam))
                    {
                        //ShowQuickOptions();
                        Thread thr = new Thread(new ThreadStart(ShowQuickOptions));
                        thr.Start();
                        return m_hID;
                    }
                    else if (CheckKeys(Program.conf.HKLanguageTranslator, lParam))
                    {
                        //Language Translator
                        if (Clipboard.ContainsText())
                        {
                            StartBW_LanguageTranslator(Clipboard.GetText());
                            return m_hID;
                        }
                    }
                }
            }
            return User32.CallNextHookEx(m_hID, nCode, wParam, lParam);
        }

        private void StartBW_EntireScreen()
        {
            StartWorkerScreenshots(MainAppTask.Jobs.TAKE_SCREENSHOT_SCREEN);
        }

        private void StartBW_SelectedWindow()
        {
            if (!mTakingScreenShot)
            {
                StartWorkerScreenshots(MainAppTask.Jobs.TAKE_SCREENSHOT_WINDOW_SELECTED);
            }
        }

        private void StartBW_CropShot()
        {
            if (!mTakingScreenShot)
            {
                StartWorkerScreenshots(MainAppTask.Jobs.TAKE_SCREENSHOT_CROPPED);
            }
        }

        private void StartBW_LastCropShot()
        {
            StartWorkerScreenshots(MainAppTask.Jobs.TAKE_SCREENSHOT_LAST_CROPPED);
        }

        private void StartBW_LanguageTranslator()
        {
            StartBW_LanguageTranslator("");
        }

        private void StartBW_LanguageTranslator(string clipboard)
        {
            StartWorkerText(MainAppTask.Jobs.LANGUAGE_TRANSLATOR, clipboard);
        }

        #region "Cache Cleaner Methods"

        private void CleanCache()
        {
            System.ComponentModel.BackgroundWorker bw = new System.ComponentModel.BackgroundWorker();
            bw.DoWork += new System.ComponentModel.DoWorkEventHandler(BwCache_DoWork);
            bw.RunWorkerAsync();
        }

        private void BwCache_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            ZSS.Tasks.CacheCleanerTask t = new ZSS.Tasks.CacheCleanerTask(Program.conf.CacheDir, Program.conf.ScreenshotCacheSize);
        }

        #endregion

        #region "Background Worker Safe Methods"

        private void CaptureActiveWindow(ref MainAppTask task)
        {
            try
            {
                task.CaptureActiveWindow();
                SaveImage(task);
                ImageSoftwareAndOrWeb(ref task);
            }
            catch (System.ArgumentOutOfRangeException aor)
            {
                task.Errors.Add("Invalid FTP Account Selection");
                Console.WriteLine(aor.ToString());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                if (Program.conf.CaptureEntireScreenOnError)
                {
                    CaptureRegionOrWindow(ref task);
                }
            }
        }

        private string CaptureRegionOrWindow(ref MainAppTask task)
        {
            string filePath = "";
            Image imgSS = null;

            try
            {
                mTakingScreenShot = true;
                imgSS = User32.CaptureScreen(Program.conf.ShowCursor);

                if (task.Job == MainAppTask.Jobs.TAKE_SCREENSHOT_LAST_CROPPED && !Program.LastRegion.IsEmpty)
                {
                    task.SetImage(CropImage(imgSS, Program.LastRegion));
                }
                else
                {
                    Crop c = new Crop(imgSS, task.Job == MainAppTask.Jobs.TAKE_SCREENSHOT_WINDOW_SELECTED);
                    if (c.ShowDialog() == DialogResult.OK)
                    {
                        if (task.Job == MainAppTask.Jobs.TAKE_SCREENSHOT_CROPPED && !Program.LastRegion.IsEmpty)
                        {
                            task.SetImage(CropImage(imgSS, Program.LastRegion));
                        }
                        else if (task.Job == MainAppTask.Jobs.TAKE_SCREENSHOT_WINDOW_SELECTED && !Program.LastCapture.IsEmpty)
                        {
                            task.SetImage(CropImage(imgSS, Program.LastCapture));
                        }
                    }
                }

                mTakingScreenShot = false;
                if (task.MyImage != null)
                {
                    SaveImage(task);
                    ImageSoftwareAndOrWeb(ref task);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                task.Errors.Add(ex.Message);
                if (Program.conf.CaptureEntireScreenOnError)
                {
                    CaptureScreen(ref task);
                }
            }
            finally
            {
                mTakingScreenShot = false;
                if (imgSS != null) imgSS.Dispose();
            }

            return filePath;
        }

        private void CaptureScreen(ref MainAppTask task)
        {
            task.CaptureScreen();
            SaveImage(task);
            ImageSoftwareAndOrWeb(ref task);
        }

        private void FlashIcon(MainAppTask task)
        {
            for (int i = 0; i < (int)Program.conf.FlashTrayCount; i++)
            {
                task.MyWorker.ReportProgress((int)MainAppTask.ProgressType.FLASH_ICON, Resources.zss_uploaded);
                Thread.Sleep(275);
                task.MyWorker.ReportProgress((int)MainAppTask.ProgressType.FLASH_ICON, Resources.zss_green);
                Thread.Sleep(275);
            }
        }

        private string GetFilePath(MainAppTask.Jobs job)
        {
            switch (job)
            {
                case MainAppTask.Jobs.TAKE_SCREENSHOT_CROPPED:
                case MainAppTask.Jobs.TAKE_SCREENSHOT_LAST_CROPPED:
                case MainAppTask.Jobs.TAKE_SCREENSHOT_SCREEN:
                    return FileSystem.GetFilePath(NameParser.Convert(NameParser.NameType.EntireScreen), Program.conf.ManualNaming);
                case MainAppTask.Jobs.TAKE_SCREENSHOT_WINDOW_ACTIVE:
                    return FileSystem.GetFilePath(NameParser.Convert(Program.conf.activeWindow, NameParser.NameType.ActiveWindow), Program.conf.ManualNaming);
            }
            throw new Exception("Unsupported Job for getting File Path.");
        }

        private void ImageSoftware(ref MainAppTask task)
        {
            if (File.Exists(task.ImageLocalPath))
            {
                Process p = new Process();
                ProcessStartInfo psi = new ProcessStartInfo(Program.conf.ImageSoftwareActive.Path);
                psi.Arguments = string.Format("{0}{1}{0}", "\"", task.ImageLocalPath);
                p.StartInfo = psi;
                p.Start();
                // Wait till user quits the ScreenshotEditApp
                p.WaitForExit();

                // upload to ftpUpload or save to clipboard
                UploadScreenshot(ref task);
            }
        }

        private void ImageSoftwareAndOrWeb(ref MainAppTask task)
        {
            if (task.MyImage != null && Program.conf.ISenabled)
            {
                //if (Program.conf.ISpath != "")
                ImageSoftware(ref task);
            }

            if (task.SafeToUpload())
            {
                Console.WriteLine("File for HDD: " + task.ImageLocalPath);
                UploadScreenshot(ref task);
            }
        }

        /// <summary>
        /// Funtion to FTP the Screenshot
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="fullFilePath"></param>
        /// <returns>Retuns a List of Screenshots</returns>
        private bool UploadFtp(ref MainAppTask task)
        {
            bool succ = true;
            string httpPath = "";
            string fullFilePath = task.ImageLocalPath;

            if (File.Exists(fullFilePath))
            {
                //there may be bugs from this line
                FTPAccount acc = Program.conf.FTPAccountList[Program.conf.FTPselected];
                task.ImageDestinationName = acc.Name;
                httpPath = acc.getUriPath(Path.GetFileName(task.ImageLocalPath));
                task.ImageRemotePath = httpPath;

                FileSystem.appendDebug(string.Format("Uploading {0} to FTP: {1}", task.ImageName, acc.Server));

                // ftpUploadFile(ref acc, task.ScreenshotName, fullFilePath);
                ImageUploader.FTPUploader fu = new ZSS.ImageUploader.FTPUploader(acc);
                fu.EnableThumbnail = (Program.conf.ClipboardUriMode != ClipboardUriType.FULL) || Program.conf.EnableThumbnail; // = true; // ideally this shold be true
                fu.WorkingDir = Program.conf.CacheDir;
                task.ImageManager = fu.UploadImage(fullFilePath);
            }

            return succ;
        }

        private void UploadScreenshot(ref MainAppTask task)
        {
            HTTPUploader imageUploader = null;

            switch (task.ImageDestCategory)
            {
                case ImageDestType.CLIPBOARD:
                    task.MyWorker.ReportProgress((int)MainAppTask.ProgressType.COPY_TO_CLIPBOARD_IMAGE, task.ImageLocalPath);
                    break;
                case ImageDestType.CUSTOM_UPLOADER:
                    if (Program.conf.ImageUploadersList != null && Program.conf.ImageUploaderSelected != -1)
                    {
                        imageUploader = new CustomUploader(Program.conf.ImageUploadersList[Program.conf.ImageUploaderSelected]);
                    }
                    break;
                case ImageDestType.FTP:
                    UploadFtp(ref task);
                    break;
                case ImageDestType.IMAGESHACK:
                    imageUploader = new ImageShackUploader(Program.IMAGESHACK_KEY, Program.conf.ImageShackRegistrationCode, Program.conf.UploadMode);
                    break;
                case ImageDestType.TINYPIC:
                    imageUploader = new TinyPicUploader(Program.TINYPIC_ID, Program.TINYPIC_KEY, Program.conf.UploadMode);
                    ((TinyPicUploader)imageUploader).Shuk = Program.conf.TinyPicShuk;
                    break;
            }


            if (imageUploader != null)
            {
                task.ImageDestinationName = imageUploader.Name;
                string fullFilePath = task.ImageLocalPath;
                if (File.Exists(fullFilePath))
                {
                    for (int i = 1; i <= (int)Program.conf.ErrorRetryCount &&
                        (task.ImageManager == null || (task.ImageManager != null && task.ImageManager.FileCount < 1)); i++)
                    {
                        task.ImageManager = imageUploader.UploadImage(fullFilePath);
                        task.Errors = imageUploader.Errors;
                        if (Program.conf.ImageUploadRetry && (task.ImageDestCategory ==
                            ImageDestType.IMAGESHACK || task.ImageDestCategory == ImageDestType.TINYPIC))
                        {
                            break;
                        }
                    }

                    //Set remote path for Screenshots history
                    task.ImageRemotePath = task.ImageManager.GetFullImageUrl();

                }
            }

            task.MyWorker.ReportProgress((int)Tasks.MainAppTask.ProgressType.ADD_FILE_TO_LISTBOX, new HistoryItem(task));

            if (task.ImageManager != null)
            {
                FlashIcon(task);
            }
        }

        #endregion

        #region "GUI Methods"

        private string ShowBalloonTip(MainAppTask t)
        {
            StringBuilder sbMsg = new StringBuilder();
            ToolTipIcon tti = ToolTipIcon.Info;

            niTray.Tag = t;

            if (t.Job == MainAppTask.Jobs.LANGUAGE_TRANSLATOR)
            {
                //sbMsg.AppendLine("Languages: " + t.TranslationInfo.Result.TranslationType);
                sbMsg.AppendLine(t.TranslationInfo.Result.TranslationType);
                sbMsg.AppendLine("Source: " + t.TranslationInfo.SourceText);
                sbMsg.AppendLine("Result: " + t.TranslationInfo.Result.TranslatedText);
                //sbMsg.AppendLine(string.Format("{0} >> {1}", t.TranslationInfo.Result.SourceText, t.TranslationInfo.Result.TranslatedText));
            }
            else
            {
                switch (t.ImageDestCategory)
                {
                    case ImageDestType.FTP:
                        sbMsg.AppendLine(string.Format("Destination: {0} ({1})", t.ImageDestCategory.ToDescriptionString(), t.ImageDestinationName));
                        break;
                    case ImageDestType.CUSTOM_UPLOADER:
                        sbMsg.AppendLine(string.Format("Destination: {0} ({1})", t.ImageDestCategory.ToDescriptionString(), t.ImageDestinationName));
                        break;
                    default:
                        sbMsg.AppendLine(string.Format("Destination: {0}", t.ImageDestCategory.ToDescriptionString()));
                        break;
                }

                string fileOrUrl = "";

                if (t.ImageDestCategory == ImageDestType.CLIPBOARD || t.ImageDestCategory == ImageDestType.FILE)
                {
                    // just local file 
                    if (!string.IsNullOrEmpty(t.ImageName.ToString()))
                    {
                        sbMsg.AppendLine("Name: " + t.ImageName.ToString());
                    }
                    fileOrUrl = string.Format("{0}: {1}", t.ImageDestCategory.ToDescriptionString(), t.ImageLocalPath);
                }
                else
                {
                    // remote file
                    if (!string.IsNullOrEmpty(t.ImageRemotePath))
                    {
                        if (!string.IsNullOrEmpty(t.ImageName.ToString()))
                        {
                            sbMsg.AppendLine("Name: " + t.ImageName.ToString());
                        }
                        fileOrUrl = string.Format("URL: {0}", t.ImageRemotePath);

                        if (string.IsNullOrEmpty(t.ImageRemotePath) && t.Errors.Count > 0)
                        {
                            tti = ToolTipIcon.Warning;
                            sbMsg.AppendLine("Warnings: ");
                            foreach (string err in t.Errors)
                            {
                                sbMsg.AppendLine(err);
                            }
                        }
                    }
                    else
                    {
                        if (t.Errors.Count > 0)
                        {
                            tti = ToolTipIcon.Error;
                            // this is not good
                            fileOrUrl = "Warning: " + t.Errors[t.Errors.Count - 1];
                        }
                    }
                }

                if (!string.IsNullOrEmpty(fileOrUrl))
                {
                    sbMsg.AppendLine(fileOrUrl);
                }
            }

            niTray.ShowBalloonTip(1000, Application.ProductName, sbMsg.ToString(), tti);

            return sbMsg.ToString();
        }

        private void SaveImage(MainAppTask t)
        {
            if (t.MyImage != null)
            {
                switch (t.Job)
                {
                    case MainAppTask.Jobs.TAKE_SCREENSHOT_CROPPED:
                        t.SetLocalFilePath(this.GetFilePath(MainAppTask.Jobs.TAKE_SCREENSHOT_CROPPED));
                        break;
                    case MainAppTask.Jobs.TAKE_SCREENSHOT_LAST_CROPPED:
                        t.SetLocalFilePath(this.GetFilePath(MainAppTask.Jobs.TAKE_SCREENSHOT_LAST_CROPPED));
                        break;
                    case MainAppTask.Jobs.TAKE_SCREENSHOT_SCREEN:
                        t.SetLocalFilePath(this.GetFilePath(MainAppTask.Jobs.TAKE_SCREENSHOT_SCREEN));
                        break;
                    case MainAppTask.Jobs.TAKE_SCREENSHOT_WINDOW_ACTIVE:
                    case MainAppTask.Jobs.TAKE_SCREENSHOT_WINDOW_SELECTED:
                        t.SetLocalFilePath(this.GetFilePath(MainAppTask.Jobs.TAKE_SCREENSHOT_WINDOW_ACTIVE));
                        break;
                }
                // Update LocalFilePath again, due to possible PNG to JPG changes
                t.SetLocalFilePath(FileSystem.SaveImage(t.MyImage, t.ImageLocalPath));

            }
        }

        private void SaveImageToClipboard(string fullFile)
        {
            if (File.Exists(fullFile))
            {
                Image img = Image.FromFile(fullFile);
                Clipboard.SetImage(img);

                img.Dispose();

                if (Program.conf.DeleteLocal)
                    File.Delete(fullFile);
            }
        }

        private void ShowQuickOptions()
        {
            if (!bQuickOptionsOpened)
            {
                bQuickOptionsOpened = true;
                Forms.QuickOptions fDes = new ZSS.Forms.QuickOptions();
                fDes.Icon = Properties.Resources.zss_main;
                fDes.ShowDialog();
                if (fDes.DialogResult == DialogResult.OK)
                {
                    BeginInvoke(new SetIndexes(SetIndexesMethod),
                        new object[] { (int)fDes.Result.Destination, 
                                       (int)fDes.Result.ClipboardMode }
                                );
                }
                bQuickOptionsOpened = false;
            }
        }

        public delegate void SetIndexes(int x, int y);

        private void SetIndexesMethod(int destination, int clipboardMode)
        {
            cboScreenshotDest.SelectedIndex = destination;
            cboClipboardTextMode.SelectedIndex = clipboardMode;
        }

        private BackgroundWorker CreateWorker()
        {
            BackgroundWorker bwApp = new BackgroundWorker();
            bwApp.WorkerReportsProgress = true;
            bwApp.DoWork += new System.ComponentModel.DoWorkEventHandler(BwApp_DoWork);
            bwApp.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(BwApp_ProgressChanged);
            bwApp.RunWorkerCompleted += new RunWorkerCompletedEventHandler(BwApp_RunWorkerCompleted);
            return bwApp;
        }

        private MainAppTask CreateTask(MainAppTask.Jobs job)
        {
            BackgroundWorker bwApp = CreateWorker();
            MainAppTask task = new MainAppTask(bwApp, job);
            if (task.Job != MainAppTask.Jobs.CUSTOM_UPLOADER_TEST)
            {
                task.ImageDestCategory = Program.conf.ScreenshotDestMode;
            }
            else
            {
                task.ImageDestCategory = ImageDestType.CUSTOM_UPLOADER;
            }
            return task;
        }

        /// <summary>
        /// Worker for Screenshots: Active Window, Crop, Entire Screen
        /// </summary>
        /// <param name="job">Job Type</param>
        private void StartWorkerScreenshots(MainAppTask.Jobs job)
        {
            MainAppTask t = CreateTask(job);
            t.JobCategory = JobCategoryType.SCREENSHOTS;
            t.MyWorker.RunWorkerAsync(t);
        }

        /// <summary>
        /// Worker for Images: Drag n Drop, Image from Clipboard, Custom Uploader
        /// </summary>
        /// <param name="job">Job Type</param>
        /// <param name="localFilePath">Local file path of the image</param>
        private void StartWorkerImages(MainAppTask.Jobs job, string localFilePath)
        {
            MainAppTask t = CreateTask(job);
            t.JobCategory = JobCategoryType.PICTURES;
            t.SetImage(localFilePath);
            t.SetLocalFilePath(localFilePath);
            t.MyWorker.RunWorkerAsync(t);
        }

        /// <summary>
        /// Worker for Text: Google Translate, Paste2, Pastebin
        /// </summary>
        /// <param name="job"></param>
        /// <param name="clipboard"></param>
        /// <returns></returns>
        private bool StartWorkerText(MainAppTask.Jobs job, string clipboard)
        {
            btnTranslate.Enabled = false;
            MainAppTask t = CreateTask(job);
            t.JobCategory = JobCategoryType.TEXT;
            if (clipboard == "")
            {
                clipboard = txtTranslateText.Text;
            }
            t.TranslationInfo = new GoogleTranslate.TranslationInfo(clipboard, mGTranslator.LanguageOptions.SourceLangList[cbFromLanguage.SelectedIndex],
                mGTranslator.LanguageOptions.TargetLangList[cbToLanguage.SelectedIndex]);
            if (t.TranslationInfo.IsEmpty())
            {
                btnTranslate.Enabled = true;
                return false;
            }
            t.MyWorker.RunWorkerAsync(t);
            return true;
        }

        private void UpdateUI()
        {
            //manual changes
            if (lbImageSoftware.Items.Count > 0) //It must contain one object
                lbImageSoftware.Items[0] = Properties.Resources.Disabled;

            if (tsmImageSoftware.DropDownItems.Count > 0) //It also should contain at least one object
            {
                tsmImageSoftware.DropDownItems[0].Text = Properties.Resources.Disabled;
            }

            //automatic changes
            /*
            foreach (ToolStripItem t in cmTray.Items)
            {
                if (t is ToolStripMenuItem)
                {
                    ToolStripMenuItem tm = (ToolStripMenuItem)t;

                    string txt = mResourceMan.GetString(tm.Name + ".Text");

                    if (txt != null)
                        tm.Text = txt;

                    foreach (ToolStripItem ti in tm.DropDownItems)
                    {
                        if (ti is ToolStripMenuItem)
                        {
                            ToolStripMenuItem tmi = (ToolStripMenuItem)ti;
                            txt = mResourceMan.GetString(tmi.Name + ".Text");

                            if (txt != null)
                                tmi.Text = txt;
                        }
                    }
                }
            }
            */
            SetNamingConventions();

        }

        #endregion

        #region "Event Handlers"

        private void BwApp_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            MainAppTask task = (MainAppTask)e.Argument;
            task.MyWorker.ReportProgress((int)MainAppTask.ProgressType.SET_ICON_BUSY, task);
            ClipboardManager.Queue();

            if (task.Job == MainAppTask.Jobs.PROCESS_DRAG_N_DROP || task.Job == MainAppTask.Jobs.IMAGEUPLOAD_FROM_CLIPBOARD)
            {
                if (task.ImageDestCategory != ImageDestType.FTP)
                {
                    if (!IsValidImage(ref task))
                    {
                        if (Program.conf.AutoSwitchFTP)
                        {
                            task.ImageDestCategory = ImageDestType.FTP;
                        }
                        else
                        {
                            e.Result = task;
                            return;
                        }
                    }
                }
            }
            if (task.JobCategory == JobCategoryType.SCREENSHOTS)
            {
                if (Program.conf.ScreenshotDelay != 0)
                    Thread.Sleep((int)(Program.conf.ScreenshotDelay * 1000));
            }

            FileSystem.appendDebug(".");
            FileSystem.appendDebug(string.Format("Job started: {0}", task.Job.ToString()));

            switch (task.Job)
            {
                case MainAppTask.Jobs.CUSTOM_UPLOADER_TEST:
                    CaptureActiveWindow(ref task);
                    break;
                case MainAppTask.Jobs.TAKE_SCREENSHOT_WINDOW_SELECTED:
                    CaptureRegionOrWindow(ref task);
                    break;
                case MainAppTask.Jobs.TAKE_SCREENSHOT_CROPPED:
                    CaptureRegionOrWindow(ref task);
                    break;
                case MainAppTask.Jobs.TAKE_SCREENSHOT_LAST_CROPPED:
                    CaptureRegionOrWindow(ref task);
                    break;
                case MainAppTask.Jobs.TAKE_SCREENSHOT_SCREEN:
                    CaptureScreen(ref task);
                    break;
                case MainAppTask.Jobs.TAKE_SCREENSHOT_WINDOW_ACTIVE:
                    CaptureActiveWindow(ref task);
                    break;
                case MainAppTask.Jobs.IMAGEUPLOAD_FROM_CLIPBOARD:
                    ImageSoftwareAndOrWeb(ref task);
                    break;
                case MainAppTask.Jobs.PROCESS_DRAG_N_DROP:
                    ImageSoftwareAndOrWeb(ref task);
                    break;
                case MainAppTask.Jobs.LANGUAGE_TRANSLATOR:
                    LanguageTranslator(ref task);
                    break;
                case MainAppTask.Jobs.UPLOAD_IMAGE:
                    ImageSoftwareAndOrWeb(ref task);
                    break;
            }

            e.Result = task;
        }

        private void BwApp_ProgressChanged(object sender, System.ComponentModel.ProgressChangedEventArgs e)
        {
            ZSS.Tasks.MainAppTask.ProgressType p = (ZSS.Tasks.MainAppTask.ProgressType)e.ProgressPercentage;
            switch (p)
            {
                case MainAppTask.ProgressType.ADD_FILE_TO_LISTBOX:
                    HistoryItem hi = (HistoryItem)e.UserState;
                    lbHistory.Items.Insert(0, hi);
                    break;

                case MainAppTask.ProgressType.COPY_TO_CLIPBOARD_IMAGE:
                    string f = e.UserState.ToString();
                    if (File.Exists(f))
                    {
                        SaveImageToClipboard(f);
                        FileSystem.appendDebug(string.Format("Saved {0} as an Image to Clipboard...", f));
                    }
                    break;

                case MainAppTask.ProgressType.FLASH_ICON:
                    niTray.Icon = (Icon)e.UserState;
                    break;

                case MainAppTask.ProgressType.SET_ICON_BUSY:
                    MainAppTask task = (MainAppTask)e.UserState;
                    niTray.Text = this.Text + " - " + task.Job.ToDescriptionString();
                    niTray.Icon = Properties.Resources.zss_busy;
                    break;
            }
        }

        private void BwApp_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                MainAppTask t = (MainAppTask)e.Result;

                FileSystem.appendDebug(string.Format("Job completed: {0}", t.Job.ToString()));

                if (!RetryUpload(t))
                {
                    if (t != null)
                    {
                        switch (t.JobCategory)
                        {
                            case JobCategoryType.TEXT:
                                switch (t.Job)
                                {
                                    case MainAppTask.Jobs.LANGUAGE_TRANSLATOR:
                                        txtTranslateText.Text = t.TranslationInfo.SourceText;
                                        txtTranslateResult.Text = t.TranslationInfo.Result.TranslatedText;
                                        txtLanguages.Text = t.TranslationInfo.Result.TranslationType;
                                        txtDictionary.Text = t.TranslationInfo.Result.Dictionary;
                                        if (Program.conf.ClipboardTranslate)
                                        {
                                            Clipboard.SetText(t.TranslationInfo.Result.TranslatedText);
                                        }
                                        btnTranslate.Enabled = true;
                                        break;
                                }
                                break;
                            case JobCategoryType.PICTURES:
                                switch (t.Job)
                                {
                                    case MainAppTask.Jobs.CUSTOM_UPLOADER_TEST:
                                        if (t.ImageManager != null & t.ImageManager.FileCount > 0)
                                        {
                                            if (t.ImageManager.GetFullImageUrl() != "")
                                            {
                                                txtUploadersLog.AppendText(t.ImageDestinationName + " full image: " +
                                                    t.ImageManager.GetFullImageUrl() + "\r\n");
                                            }
                                            if (t.ImageManager.GetThumbnailUrl() != "")
                                            {
                                                txtUploadersLog.AppendText(t.ImageDestinationName + " thumbnail: " +
                                                    t.ImageManager.GetThumbnailUrl() + "\r\n");
                                            }
                                        }
                                        btnUploadersTest.Enabled = true;
                                        break;
                                }
                                break;
                            case JobCategoryType.SCREENSHOTS:
                                if (Program.conf.DeleteLocal)
                                {
                                    if (File.Exists(t.ImageLocalPath))
                                    {
                                        File.Delete(t.ImageLocalPath);
                                    }
                                }
                                break;
                        }

                        if (t.JobCategory == JobCategoryType.SCREENSHOTS || t.JobCategory == JobCategoryType.PICTURES)
                        {
                            ClipboardManager.AddScreenshotList(t.ImageManager);
                            ClipboardManager.SetClipboardText();
                        }

                        if (t.ImageManager != null && !string.IsNullOrEmpty(t.ImageManager.Source))
                        {
                            btnOpenSourceText.Enabled = true;
                            btnOpenSourceBrowser.Enabled = true;
                            btnOpenSourceString.Enabled = true;
                        }
                    } // Task is not null

                    niTray.Text = this.Text;
                    if (ClipboardManager.Workers > 1)
                    {
                        niTray.Icon = Resources.zss_busy;
                    }
                    else
                    {
                        niTray.Icon = Resources.zss_tray;
                    }

                    if (t.Job == MainAppTask.Jobs.LANGUAGE_TRANSLATOR || File.Exists(t.ImageLocalPath))
                    {
                        if (Program.conf.CompleteSound)
                        {
                            System.Media.SystemSounds.Exclamation.Play();
                        }
                        if (Program.conf.ShowBalloonTip)
                        {
                            ShowBalloonTip(t);
                        }
                    }

                    if (t.Errors.Count > 0)
                    {
                        Console.WriteLine(t.Errors[t.Errors.Count - 1]);
                    }
                }

                if (t.MyImage != null) t.MyImage.Dispose(); // For fix memory leak

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                ClipboardManager.Commit();
            }
        }

        private bool RetryUpload(MainAppTask t)
        {
            if (Program.conf.ImageUploadRetry && t.Errors.Count > 0 && !t.Retry &&
                (t.ImageDestCategory == ImageDestType.IMAGESHACK || t.ImageDestCategory == ImageDestType.TINYPIC))
            {
                MainAppTask task = CreateTask(MainAppTask.Jobs.UPLOAD_IMAGE);
                task.JobCategory = t.JobCategory;
                task.SetImage(t.ImageLocalPath);
                task.SetLocalFilePath(t.ImageLocalPath);
                if (t.ImageDestCategory == ImageDestType.IMAGESHACK)
                {
                    task.ImageDestCategory = ImageDestType.TINYPIC;
                }
                else
                {
                    task.ImageDestCategory = ImageDestType.IMAGESHACK;
                }
                task.Retry = true;
                task.MyWorker.RunWorkerAsync(task);
                return true;
            }
            else
            {
                return false;
            }
        }

        private void exitZScreenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            mClose = true;
            Close();
        }

        private void cbRegionRectangleInfo_CheckedChanged(object sender, EventArgs e)
        {
            Program.conf.RegionRectangleInfo = cbRegionRectangleInfo.Checked;
        }

        private void cbRegionHotkeyInfo_CheckedChanged(object sender, EventArgs e)
        {
            Program.conf.RegionHotkeyInfo = cbRegionHotkeyInfo.Checked;
        }

        private void niTray_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ShowMainWindow();
        }

        private void tsmQuickOptions_Click(object sender, EventArgs e)
        {
            ShowQuickOptions();
        }

        private void btnRegCodeImageShack_Click(object sender, EventArgs e)
        {
            Process.Start("http://my.imageshack.us/registration/");
        }

        private void nErrorRetry_ValueChanged(object sender, EventArgs e)
        {
            Program.conf.ErrorRetryCount = nErrorRetry.Value;
        }

        private void btnGalleryImageShack_Click(object sender, EventArgs e)
        {
            Process.Start("http://my.imageshack.us/v_images.php");
        }

        private void ZScreen_Resize(object sender, EventArgs e)
        {
            if (mGuiIsReady)
            {
                if (this.WindowState == FormWindowState.Minimized)
                {
                    Program.conf.Save();
                    if (!Program.conf.ShowInTaskbar)
                    {
                        this.Hide();
                    }
                }
                else if (this.WindowState == FormWindowState.Normal)
                {
                    this.ShowInTaskbar = Program.conf.ShowInTaskbar;
                }
            }
        }

        private void ZScreen_FormClosing(object sender, FormClosingEventArgs e)
        {
            Program.conf.Save();
            if (!mClose && e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                Hide();
            }
            FileSystem.appendDebug("Closed " + Application.ProductName + "\n");
            //FileSystem.writeDebugFile();
        }

        #endregion

        private void RewriteISRightClickMenu()
        {
            if (Program.conf.ImageSoftwareList != null)
            {
                tsmImageSoftware.DropDownDirection = ToolStripDropDownDirection.Right;

                tsmImageSoftware.DropDownItems.Clear();

                List<ImageSoftware> imgs = Program.conf.ImageSoftwareList;

                ToolStripMenuItem tsm;

                //tsm.TextDirection = ToolStripTextDirection.Horizontal;
                tsmImageSoftware.DropDownDirection = ToolStripDropDownDirection.Right;

                tsm = new ToolStripMenuItem();
                tsm.Text = Properties.Resources.Disabled;
                tsm.CheckOnClick = true;
                tsm.Click += new EventHandler(disableImageSoftware_Click);

                tsmImageSoftware.DropDownItems.Add(tsm);

                tsmImageSoftware.DropDownItems.Add(new ToolStripSeparator());

                for (int x = 0; x < imgs.Count; x++)
                {
                    tsm = new ToolStripMenuItem();
                    //tsm.Tag = x;
                    tsm.CheckOnClick = true;

                    //add event handler
                    tsm.Click += new EventHandler(rightClickISItem_Click);

                    tsm.Text = imgs[x].Name;
                    tsmImageSoftware.DropDownItems.Add(tsm);
                }

                //check the active ftpUpload account

                if (Program.conf.ISenabled)
                    CheckCorrectISRightClickMenu(Program.conf.ImageSoftwareActive.Name);
                else
                    CheckCorrectISRightClickMenu(tsmImageSoftware.DropDownItems[0].Text);

                tsmImageSoftware.DropDownDirection = ToolStripDropDownDirection.Right;

                //show drop down menu in the correct place if menu is selected
                if (tsmImageSoftware.Selected == true)
                {
                    tsmImageSoftware.DropDown.Hide();
                    tsmImageSoftware.DropDown.Show();
                }
            }
        }

        private void disableImageSoftware_Click(object sender, EventArgs e)
        {
            //cbRunImageSoftware.Checked = false;

            //select "Disabled"
            lbImageSoftware.SelectedIndex = 0;

            CheckCorrectISRightClickMenu(tsmImageSoftware.DropDownItems[0].Text); //disabled
            //rewriteISRightClickMenu();
        }

        private void rightClickISItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem tsm = (ToolStripMenuItem)sender;

            Program.conf.ImageSoftwareActive = Program.GetImageSoftware(tsm.Text); //Program.conf.ImageSoftwareList[(int)tsm.Tag];

            if (lbImageSoftware.Items.IndexOf(tsm.Text) >= 0)
                lbImageSoftware.SelectedItem = tsm.Text;

            //Turn on Image Software
            //cbRunImageSoftware.Checked = true;

            //rewriteISRightClickMenu();
        }

        private void CheckCorrectISRightClickMenu(string txt)
        {
            ToolStripMenuItem tsm;

            for (int x = 0; x < tsmImageSoftware.DropDownItems.Count; x++)
            {
                //if (tsmImageSoftware.DropDownItems[x].GetType() == typeof(ToolStripMenuItem))
                if (tsmImageSoftware.DropDownItems[x] is ToolStripMenuItem)
                {
                    tsm = (ToolStripMenuItem)tsmImageSoftware.DropDownItems[x];

                    if (tsm.Text == txt)
                    {
                        tsm.CheckState = CheckState.Checked;
                    }
                    else
                    {
                        tsm.CheckState = CheckState.Unchecked;
                    }
                }
            }
        }

        private void RewriteCustomUploaderRightClickMenu()
        {
            if (Program.conf.ImageUploadersList != null)
            {
                List<ImageHostingService> lUploaders = Program.conf.ImageUploadersList;
                ToolStripMenuItem tsm;
                tsmDestCustomHTTP.DropDownDirection = ToolStripDropDownDirection.Right;
                tsmDestCustomHTTP.DropDownItems.Clear();

                for (int i = 0; i < lUploaders.Count; i++)
                {
                    tsm = new ToolStripMenuItem();
                    tsm.CheckOnClick = true;
                    tsm.Click += new EventHandler(rightClickIHS_Click);
                    tsm.Tag = i;
                    tsm.Text = lUploaders[i].Name;
                    tsmDestCustomHTTP.DropDownItems.Add(tsm);
                }

                CheckCorrectMenuItemClicked(ref tsmDestCustomHTTP, Program.conf.ImageUploaderSelected);

                tsmDestCustomHTTP.DropDownDirection = ToolStripDropDownDirection.Right;

                //show drop down menu in the correct place if menu is selected
                if (tsmDestCustomHTTP.Selected == true)
                {
                    tsmDestCustomHTTP.DropDown.Hide();
                    tsmDestCustomHTTP.DropDown.Show();
                }

            }
        }

        private void rightClickIHS_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem tsm = (ToolStripMenuItem)sender;
            lbUploader.SelectedIndex = (int)tsm.Tag;
        }

        private void FillClipboardCopyMenu()
        {
            tsmCopyCbHistory.DropDownDirection = ToolStripDropDownDirection.Right;
            tsmCopyCbHistory.DropDownItems.Clear();

            ToolStripMenuItem tsm;
            int x = 0;
            foreach (ClipboardUriType cui in Enum.GetValues(typeof(ClipboardUriType)))
            {
                tsm = new ToolStripMenuItem();
                tsm.Tag = x++;
                tsm.Text = cui.ToDescriptionString();
                tsm.Click += new EventHandler(clipboardCopyHistory_Click);
                tsmCopyCbHistory.DropDownItems.Add(tsm);
            }
        }

        void clipboardCopyHistory_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem tsm = (ToolStripMenuItem)sender;
            SetClipboardFromHistory((ClipboardUriType)tsm.Tag);
        }

        private void FillClipboardMenu()
        {

            tsmCbCopy.DropDownDirection = ToolStripDropDownDirection.Right;
            tsmCbCopy.DropDownItems.Clear();

            ToolStripMenuItem tsm;
            int x = 0;
            foreach (ClipboardUriType cui in Enum.GetValues(typeof(ClipboardUriType)))
            {
                tsm = new ToolStripMenuItem();
                tsm.Tag = x++;
                tsm.CheckOnClick = true;
                tsm.Text = cui.ToDescriptionString();
                tsm.Click += new EventHandler(clipboardMode_Click);
                tsmCbCopy.DropDownItems.Add(tsm);
            }

            CheckCorrectMenuItemClicked(ref tsmCbCopy, (int)Program.conf.ClipboardUriMode);
            tsmCbCopy.DropDownDirection = ToolStripDropDownDirection.Right;

        }

        void clipboardMode_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem tsm = (ToolStripMenuItem)sender;
            Program.conf.ClipboardUriMode = (ClipboardUriType)tsm.Tag;
            CheckCorrectMenuItemClicked(ref tsmCbCopy, (int)Program.conf.ClipboardUriMode);
            cboClipboardTextMode.SelectedIndex = (int)Program.conf.ClipboardUriMode;
        }

        private void RewriteFTPRightClickMenu()
        {
            if (Program.conf.FTPAccountList != null)
            {
                tsmDestFTP.DropDownDirection = ToolStripDropDownDirection.Right;

                tsmDestFTP.DropDownItems.Clear();

                List<FTPAccount> accs = Program.conf.FTPAccountList;

                ToolStripMenuItem tsm;

                //tsm.TextDirection = ToolStripTextDirection.Horizontal;
                tsmDestFTP.DropDownDirection = ToolStripDropDownDirection.Right;


                for (int x = 0; x < accs.Count; x++)
                {
                    tsm = new ToolStripMenuItem();
                    tsm.Tag = x;
                    tsm.CheckOnClick = true;

                    //add event handler
                    tsm.Click += new EventHandler(rightClickFTPItem_Click);

                    tsm.Text = accs[x].Name;
                    tsmDestFTP.DropDownItems.Add(tsm);

                }

                //check the active ftpUpload account

                CheckCorrectMenuItemClicked(ref tsmDestFTP, Program.conf.FTPselected);

                tsmDestFTP.DropDownDirection = ToolStripDropDownDirection.Right;

                //show drop down menu in the correct place if menu is selected
                if (tsmDestFTP.Selected == true)
                {
                    tsmDestFTP.DropDown.Hide();
                    tsmDestFTP.DropDown.Show();
                }
            }
        }

        private void rightClickFTPItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem tsm = (ToolStripMenuItem)sender;

            //Program.conf.FTPselected = (int)tsm.Tag;

            lbFTPAccounts.SelectedIndex = (int)tsm.Tag;

            //rewriteFTPRightClickMenu();
        }

        private void CheckCorrectMenuItemClicked(ref ToolStripMenuItem mi, int index)
        {
            ToolStripMenuItem tsm;

            for (int x = 0; x < mi.DropDownItems.Count; x++)
            {
                tsm = (ToolStripMenuItem)mi.DropDownItems[x];

                if (index == x)
                {
                    tsm.CheckState = CheckState.Checked;
                }
                else
                {
                    tsm.CheckState = CheckState.Unchecked;
                }
            }
        }

        private bool CheckStartWithWindows()
        {
            RegistryKey regkey = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run");
            if ((string)regkey.GetValue(Application.ProductName, "null", RegistryValueOptions.None) != "null")
            {
                Registry.CurrentUser.Flush();
                return true;
            }
            Registry.CurrentUser.Flush();
            return false;
        }

        private void ShowLicense()
        {
            string lic = FileSystem.getTextFromFile(Path.Combine(Application.StartupPath, "License.txt"));
            lic = lic != string.Empty ? lic : FileSystem.GetText("License.txt");
            if (lic != string.Empty)
            {
                frmTextViewer v = new frmTextViewer(string.Format("{0} - {1}",
                    Application.ProductName, "License"), lic);
                v.Icon = this.Icon;
                v.ShowDialog();
            }
        }

        private void ShowVersionHistory()
        {
            string h = FileSystem.getTextFromFile(Path.Combine(Application.StartupPath, "VersionHistory.txt"));
            if (h == string.Empty)
            {
                h = FileSystem.GetText("VersionHistory.txt");
            }
            if (h != string.Empty)
            {
                frmTextViewer v = new frmTextViewer(string.Format("{0} - {1}",
                    Application.ProductName, "Version History"), h);
                v.Icon = this.Icon;
                v.ShowDialog();
            }
        }

        private void FindImageSoftwares()
        {
            //Adobe Photoshop - HKEY_CLASSES_ROOT\Applications\Photoshop.exe\shell\open\command
            SoftwareCheck(@"Applications\Photoshop.exe\shell\open\command", "Adobe Photoshop");
            //Irfan View - HKEY_CLASSES_ROOT\Applications\i_view32.exe\shell\open\command - HKEY_LOCAL_MACHINE\SOFTWARE\Classes\IrfanView\shell\open\command
            SoftwareCheck(@"Applications\i_view32.exe\shell\open\command", "Irfan View");
            //Paint.NET - HKEY_CLASSES_ROOT\Paint.NET.1\shell\open\command
            SoftwareCheck(@"Paint.NET.1\shell\open\command", "Paint.NET");
        }

        private bool SoftwareCheck(string regPath, string sName)
        {
            RegistryKey regKey = Registry.ClassesRoot.OpenSubKey(regPath, false);
            if (regKey != null) //If registry found
            {
                string stringReg = regKey.GetValue("").ToString();
                if (!string.IsNullOrEmpty(stringReg)) //If registry value not empty
                {
                    string filePath = stringReg.Substring(1, stringReg.LastIndexOf("%") - 4);
                    if (File.Exists(filePath)) //If found path exist
                    {
                        if (!SoftwareExist(sName)) //If not added to Software list before
                        {
                            Program.conf.ImageSoftwareList.Add(new ImageSoftware(sName, filePath));
                        }
                        return true;
                    }
                }
            }
            SoftwareRemove(sName);
            return false;
        }

        private bool SoftwareExist(string sName)
        {
            foreach (ImageSoftware iS in Program.conf.ImageSoftwareList)
            {
                if (iS.Name == sName) return true;
            }
            return false;
        }

        private bool SoftwareRemove(string sName)
        {
            if (SoftwareExist(sName))
            {
                foreach (ImageSoftware iS in Program.conf.ImageSoftwareList)
                {
                    if (iS.Name == sName)
                    {
                        Program.conf.ImageSoftwareList.Remove(iS);
                        return true;
                    }
                }
            }
            return false;
        }

        private void btnBrowseDirectory_Click(object sender, EventArgs e)
        {
            Program.conf.ImagesDir = BrowseDirectory(ref txtFileDirectory);
        }

        private string BrowseDirectory(ref TextBox textBoxDirectory)
        {
            string settingDir = "";
            FolderBrowserDialog dlg = new FolderBrowserDialog();
            dlg.SelectedPath = textBoxDirectory.Text;
            dlg.ShowNewFolderButton = true;
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                if (dlg.SelectedPath != "")
                {
                    settingDir = dlg.SelectedPath;
                    textBoxDirectory.Text = dlg.SelectedPath;
                }
            }
            return settingDir;
        }

        private void btnResetActiveWindow_Click(object sender, EventArgs e)
        {
            Program.conf.activeWindow = Properties.Resources.activeWindowDefault;
            txtActiveWindow.Text = Program.conf.activeWindow;
        }

        private void btnResetEntireScreen_Click(object sender, EventArgs e)
        {
            Program.conf.entireScreen = Properties.Resources.entireScreenDefault;
            txtEntireScreen.Text = Program.conf.entireScreen;
        }

        private void btnCopyToClipboard_Click(object sender, EventArgs e)
        {
            string str = "";

            ArrayList items = new ArrayList();

            foreach (int i in lbHistory.SelectedIndices)
            {
                items.Add(((HistoryItem)lbHistory.Items[i]).ScreenshotManager.GetFullImageUrl());
            }

            //Changed it back to the way it was. (reversing the list is unreliable when you do it more than one time [when new screenshots are dropping in])
            if (cbReverse.Checked)
                items.Reverse();

            foreach (string url in items)
            {
                str += url + System.Environment.NewLine;
            }

            if (str != "")
            {
                if (cbAddSpace.Checked)
                    str = str.Insert(0, System.Environment.NewLine);
                str = str.TrimEnd(System.Environment.NewLine.ToCharArray()); ;

                //Set clipboard data
                Clipboard.SetDataObject(str);
            }
        }

        private void btnUpdateImageSoftware_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtImageSoftwareName.Text) &&
                Program.GetImageSoftware(txtImageSoftwareName.Text) == null &&
                !string.IsNullOrEmpty(txtImageSoftwarePath.Text))
            {
                int sel = 0;
                bool isActiveImageSoftware = false;

                if ((sel = lbImageSoftware.SelectedIndex) > 0)
                {
                    if (Program.conf.ImageSoftwareActive.Name == (string)lbImageSoftware.SelectedItem)
                        isActiveImageSoftware = true;
                    ImageSoftware temp = new ImageSoftware();
                    temp.Name = txtImageSoftwareName.Text;
                    temp.Path = txtImageSoftwarePath.Text;
                    Program.conf.ImageSoftwareList[sel - 1] = temp;
                    lbImageSoftware.Items[sel] = temp.Name;

                    if (isActiveImageSoftware)
                    {
                        if (Program.conf.ISenabled)
                        {
                            Program.conf.ImageSoftwareActive = temp;
                            CheckCorrectISRightClickMenu(temp.Name);
                        }
                    }
                }
            }

            RewriteISRightClickMenu();
        }

        private void btnBrowseImageSoftware_Click(object sender, EventArgs e)
        {
            BrowseImageSoftware();
        }

        private void BrowseImageSoftware()
        {
            //get old path
            string old = txtImageSoftwarePath.Text;

            txtImageSoftwarePath.Enabled = true;

            //choose path / create path popup
            //remember old path and display it

            OpenFileDialog dlg = new OpenFileDialog();
            if (txtImageSoftwarePath.Text != "")
                dlg.FileName = txtImageSoftwarePath.Text;

            dlg.Filter = "Executable files (*.exe)|*.exe";

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                txtImageSoftwarePath.Text = dlg.FileName;
                if (String.IsNullOrEmpty(txtImageSoftwareName.Text))
                {
                    txtImageSoftwareName.Text =
                        Path.GetFileNameWithoutExtension(dlg.FileName);
                }
            }

            txtImageSoftwarePath.Enabled = false;


        }

        private void tsmSettings_Click(object sender, EventArgs e)
        {
            BringUpMenu();
        }

        private void TrimFTPControls()
        {
            TextBox[] arr = { txtFTPServer, txtFTPPath, txtFTPHTTPPath };
            foreach (TextBox tb in arr)
            {
                if (tb.Text != "/")
                {
                    tb.Text = tb.Text.TrimEnd("/".ToCharArray()).TrimEnd("\\".ToCharArray());
                    tb.Update();
                }
            }
        }

        private void tsmViewDirectory_Click(object sender, EventArgs e)
        {
            ShowDirectory(Program.conf.ImagesDir);
        }

        private void ShowDirectory(string argDir)
        {
            Process proc = new Process();
            proc.StartInfo.FileName = "explorer";
            proc.StartInfo.Arguments = argDir;
            proc.Start();
        }

        private void ShowHelp()
        {
            Process.Start("http://www.brandonz.net/projects/zscreen/documentation.html");
        }

        private void tpDocumentation_Enter(object sender, EventArgs e)
        {
            ShowHelp();
        }

        private void tsmViewRemote_Click(object sender, EventArgs e)
        {
            if (Program.conf.FTPAccountList.Count > 0)
            {
                ViewRemote vr = new ViewRemote();
                vr.Show();
            }
        }

        private void txtActiveWindow_Leave(object sender, EventArgs e)
        {
            mHadFocus = (TextBox)sender;
            mHadFocusAt = ((TextBox)sender).SelectionStart;
        }

        private void txtEntireScreen_Leave(object sender, EventArgs e)
        {
            mHadFocus = (TextBox)sender;
            mHadFocusAt = ((TextBox)sender).SelectionStart;
        }

        private void btnCodes_Click(object sender, EventArgs e)
        {
            Button b = (Button)sender;

            const string beginning = "btnCodes";
            string name = b.Name, code;

            if (name.Contains(beginning))
            {
                name = name.Replace(beginning, "");
                code = "%" + name.ToLower();

                if (mHadFocus != null)
                {
                    mHadFocus.Text = mHadFocus.Text.Insert(mHadFocusAt, code);
                    mHadFocus.Focus();
                    mHadFocus.Select(mHadFocusAt + code.Length, 0);
                }
            }
        }

        private void BringUpMenu()
        {
            Show();
            WindowState = FormWindowState.Normal;
            this.Activate();
            Form.ActiveForm.BringToFront();
        }

        private void tsm_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem tsm = (ToolStripMenuItem)sender;

            TabPage sel = tpMain;

            if (tsm == tsmHotkeys)
                sel = tpHotKeys;
            else if (tsm == tsmFTPSettings)
                sel = tpFTP;
            else if (tsm == tsmHTTPSettings)
                sel = tpHTTP;
            else if (tsm == tsmImageSoftwareSettings)
                sel = tpImageSoftware;
            else if (tsm == tsmFileSettings)
                sel = tpFile;
            else if (tsm == tsmFTPSettings)
                sel = tpFTP;
            else if (tsm == tsmHistory)
                sel = tpScreenshots;
            else if (tsm == tsmAdvanced)
                sel = tpAdvanced;

            tcApp.SelectedTab = sel;

            BringUpMenu();

            tcApp.Focus();
        }

        private void SetNamingConventions()
        {
            Program.conf.activeWindow = Properties.Resources.activeWindowDefault;
            Program.conf.entireScreen = Properties.Resources.entireScreenDefault;

            txtActiveWindow.Text = Program.conf.activeWindow;
            txtEntireScreen.Text = Program.conf.entireScreen;
        }

        private void tsmHelp_Click(object sender, EventArgs e)
        {
            ShowHelp();
        }

        private void tsmLicense_Click(object sender, EventArgs e)
        {
            ShowLicense();
        }

        private void tsmVersionHistory_Click(object sender, EventArgs e)
        {
            ShowVersionHistory();
        }

        private void sShowAbout()
        {
            ZSS.Forms.AboutBox ab = new ZSS.Forms.AboutBox();
            ab.ShowDialog();
        }

        private void tsmAbout_Click(object sender, EventArgs e)
        {
            sShowAbout();
        }

        private void tsmDocumentation_Click(object sender, EventArgs e)
        {
            ShowHelp();
        }

        private void btnBrowseConfig_Click(object sender, EventArgs e)
        {
            Process.Start(Path.GetDirectoryName(Program.XMLSettingsFile));
        }

        private void tsmLic_Click(object sender, EventArgs e)
        {
            ShowLicense();
        }

        private void chkManualNaming_CheckedChanged(object sender, EventArgs e)
        {
            UpdatePromptFileNameCheck();
        }

        private void UpdatePromptFileNameCheck()
        {
            Program.conf.ManualNaming = chkManualNaming.Checked;
        }

        private void ZScreen_Shown(object sender, EventArgs e)
        {
            mGuiIsReady = true;
            // this.ShowInTaskbar = Program.conf.ShowInTaskbar;
            //if (Program.MultipleInstance)
            //{
            //    //MessageBox.Show(string.Format("Another instance of {0} is already running. {0} will continue to work in Muliple Instance mode.", Application.ProductName), 
            //    //    Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
            //}
        }

        private void AddToClipboardByDoubleClick(TabPage tp)
        {
            Control ctl = tp.GetNextControl(tp, true);
            while (ctl != null)
            {
                if (ctl.GetType() == typeof(TextBox))
                {
                    ((TextBox)ctl).DoubleClick += new EventHandler(TextBox_DoubleClick);
                }
                ctl = tp.GetNextControl(ctl, true);
            }
        }

        void TextBox_DoubleClick(object sender, EventArgs e)
        {
            TextBox tb = ((TextBox)sender);
            if (!string.IsNullOrEmpty(tb.Text))
            {
                Clipboard.SetText(tb.Text);
            }
        }

        //private void ShowDebug()
        //{
        //    if (File.Exists(FileSystem.DebugFilePath))
        //    {
        //        Process.Start(FileSystem.DebugFilePath);
        //    }
        //}

        private void tsmAboutMain_Click(object sender, EventArgs e)
        {
            sShowAbout();
        }

        private void cbStartWin_CheckedChanged(object sender, EventArgs e)
        {
            RegistryKey regkey = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run", true);

            if (cbStartWin.Checked)
            {
                regkey.SetValue(Application.ProductName, Application.ExecutablePath, RegistryValueKind.String);
            }
            else
            {
                regkey.DeleteValue(Application.ProductName, false);
            }

            Registry.CurrentUser.Flush();
        }

        private void nudFlashIconCount_ValueChanged(object sender, EventArgs e)
        {
            Program.conf.FlashTrayCount = nudFlashIconCount.Value;
        }

        private void nudCacheSize_ValueChanged(object sender, System.EventArgs e)
        {
            Program.conf.ScreenshotCacheSize = nudCacheSize.Value;
        }

        private void txtCacheDir_TextChanged(object sender, System.EventArgs e)
        {
            Program.conf.CacheDir = txtCacheDir.Text;
        }

        private void btnSettingsExport_Click(object sender, EventArgs e)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Filter = Program.FILTER_SETTINGS;

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                Program.conf.Save(dlg.FileName);
            }
        }

        private void btnSettingsImport_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = Program.FILTER_SETTINGS;
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                XMLSettings temp = XMLSettings.Read(dlg.FileName);
                if (temp.RunOnce == true)
                {
                    Program.conf = temp;
                    SetupScreen();
                }
            }
        }

        private void AddImageSoftwareToList()
        {
            if (!string.IsNullOrEmpty(txtImageSoftwareName.Text))
            {
                if (Program.GetImageSoftware(txtImageSoftwareName.Text) == null)
                {
                    if (!string.IsNullOrEmpty(txtImageSoftwarePath.Text))
                    {
                        ImageSoftware temp = new ImageSoftware();
                        temp.Name = txtImageSoftwareName.Text;
                        temp.Path = txtImageSoftwarePath.Text;
                        Program.conf.ImageSoftwareList.Add(temp);
                        lbImageSoftware.Items.Add(temp.Name);

                        lbImageSoftware.SelectedItem = temp.Name;

                        //rewriteISRightClickMenu();
                    }
                }
            }
        }

        private void btnAddImageSoftware_Click(object sender, EventArgs e)
        {
            AddImageSoftwareToList();
        }

        private void btnDeleteImageSoftware_Click(object sender, EventArgs e)
        {
            int wasActiveSetLower = -1;

            if (lbImageSoftware.SelectedIndex > 0)
            {
                //If Active then set one step lower in the list
                if (Program.conf.ImageSoftwareActive.Name == lbImageSoftware.SelectedItem.ToString())
                {
                    wasActiveSetLower = lbImageSoftware.SelectedIndex - 1;
                }

                ImageSoftware temp = Program.GetImageSoftware(lbImageSoftware.SelectedItem.ToString());
                if (temp != null)
                {
                    Program.conf.ImageSoftwareList.Remove(temp);
                    lbImageSoftware.Items.Remove(lbImageSoftware.SelectedItem.ToString());
                }

                if (wasActiveSetLower != -1)
                {
                    lbImageSoftware.SelectedIndex = wasActiveSetLower;
                }

                RewriteISRightClickMenu();
            }
        }

        private void cboScreenshotDest_SelectedIndexChanged(object sender, EventArgs e)
        {
            ImageDestType sdt = (ImageDestType)cboScreenshotDest.SelectedIndex;
            Program.conf.ScreenshotDestMode = sdt;
            cboClipboardTextMode.Enabled = sdt != ImageDestType.CLIPBOARD && sdt != ImageDestType.FILE;

            switch (sdt)
            {
                case ImageDestType.CLIPBOARD:
                    CheckSendToMenu(tsmDestClipboard);
                    break;
                case ImageDestType.FILE:
                    CheckSendToMenu(tsmDestFile);
                    break;
                case ImageDestType.FTP:
                    CheckSendToMenu(tsmDestFTP);
                    break;
                case ImageDestType.IMAGESHACK:
                    CheckSendToMenu(tsmDestImageShack);
                    break;
                case ImageDestType.TINYPIC:
                    CheckSendToMenu(tsmDestTinyPic);
                    break;
                case ImageDestType.CUSTOM_UPLOADER:
                    CheckSendToMenu(tsmDestCustomHTTP);
                    break;
            }
        }

        private void CheckSendToMenu(ToolStripMenuItem item)
        {
            CheckToolStripMenuItem(tsmSendTo, item);
        }

        private void CheckToolStripMenuItem(ToolStripMenuItem parent, ToolStripMenuItem item)
        {
            foreach (ToolStripMenuItem tsmi in parent.DropDownItems)
            {
                if (tsmi == item)
                    tsmi.Checked = true;
                else
                    tsmi.Checked = false;
            }

            tsmCbCopy.Enabled = cboScreenshotDest.SelectedIndex != (int)ImageDestType.CLIPBOARD &&
                                cboScreenshotDest.SelectedIndex != (int)ImageDestType.FILE;
        }

        private void tsmDestClipboard_Click(object sender, EventArgs e)
        {
            cboScreenshotDest.SelectedIndex = (int)ImageDestType.CLIPBOARD;
        }

        private void tsmDestFile_Click(object sender, EventArgs e)
        {
            cboScreenshotDest.SelectedIndex = (int)ImageDestType.FILE;
        }

        private void tsmDestFTP_Click(object sender, EventArgs e)
        {
            cboScreenshotDest.SelectedIndex = (int)ImageDestType.FTP;
        }

        private void tsmDestImageShack_Click(object sender, EventArgs e)
        {
            cboScreenshotDest.SelectedIndex = (int)ImageDestType.IMAGESHACK;
        }

        private void tsmDestTinyPic_Click(object sender, EventArgs e)
        {
            cboScreenshotDest.SelectedIndex = (int)ImageDestType.TINYPIC;
        }

        private void tsmDestCustomHTTP_Click(object sender, EventArgs e)
        {
            cboScreenshotDest.SelectedIndex = (int)ImageDestType.CUSTOM_UPLOADER;
        }

        private void SetActiveImageSoftware()
        {
            int sel = 0;

            if ((sel = lbImageSoftware.SelectedIndex) > 0)
            {
                Program.conf.ISenabled = true;

                Program.conf.ImageSoftwareActive = Program.conf.ImageSoftwareList[sel - 1];
                RewriteISRightClickMenu();
                //checkCorrectISRightClickMenu(Program.conf.ImageSoftwareActive.Name);
                //cbRunImageSoftware.Checked = true;
            }
        }

        private void lbImageSoftware_SelectedIndexChanged(object sender, EventArgs e)
        {
            int sel = lbImageSoftware.SelectedIndex;

            bool b = sel > 0;

            if (sel == 0)
            {
                txtImageSoftwareName.Text = "";
                txtImageSoftwarePath.Text = "";

                Program.conf.ISenabled = false;
                RewriteISRightClickMenu();
            }
            else if (b)
            {
                ImageSoftware temp = Program.GetImageSoftware(lbImageSoftware.SelectedItem.ToString());
                if (temp != null)
                {
                    txtImageSoftwareName.Text = temp.Name;
                    txtImageSoftwarePath.Text = temp.Path;
                }

                SetActiveImageSoftware();
            }

            btnUpdateImageSoftware.Enabled = b;
            btnDeleteImageSoftware.Enabled = b;
        }

        private void cboClipboardTextMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            Program.conf.ClipboardUriMode = (ClipboardUriType)cboClipboardTextMode.SelectedIndex;
            UpdateClipboardTextTrayMenu();
        }

        private void UpdateClipboardTextTrayMenu()
        {

            foreach (ToolStripMenuItem tsmi in tsmCbCopy.DropDownItems)
            {
                tsmi.Checked = false;
            }
            CheckCorrectMenuItemClicked(ref tsmCbCopy, (int)Program.conf.ClipboardUriMode);
        }

        private void txtFileDirectory_TextChanged(object sender, EventArgs e)
        {
            Program.conf.ImagesDir = txtFileDirectory.Text;
        }

        private void cbDeleteLocal_CheckedChanged(object sender, EventArgs e)
        {
            Program.conf.DeleteLocal = cbDeleteLocal.Checked;
        }

        private void txtActiveWindow_TextChanged(object sender, EventArgs e)
        {
            Program.conf.activeWindow = txtActiveWindow.Text;
            lblActiveWindowPreview.Text = NameParser.Convert(NameParser.NameType.ActiveWindow);
        }

        private void txtEntireScreen_TextChanged(object sender, EventArgs e)
        {
            Program.conf.entireScreen = txtEntireScreen.Text;
            lblEntireScreenPreview.Text = NameParser.Convert(NameParser.NameType.EntireScreen);
        }

        private void cmbFileFormat_SelectedIndexChanged(object sender, EventArgs e)
        {
            Program.conf.FileFormat = cmbFileFormat.SelectedIndex;
        }

        private void txtImageQuality_ValueChanged(object sender, EventArgs e)
        {
            long quality = 100L;

            try { quality = long.Parse(txtImageQuality.Text); }
            catch { }

            Program.conf.ImageQuality = quality;
        }

        private void txtSwitchAfter_TextChanged(object sender, EventArgs e)
        {
            int switchAfter = 350;

            try { switchAfter = Int32.Parse(nudSwitchAfter.Text); }
            catch { }

            Program.conf.SwitchAfter = switchAfter;
        }

        private void cmbSwitchFormat_SelectedIndexChanged(object sender, EventArgs e)
        {
            Program.conf.SwitchFormat = cmbSwitchFormat.SelectedIndex;
        }

        private void txtImageShackRegistrationCode_TextChanged(object sender, EventArgs e)
        {
            Program.conf.ImageShackRegistrationCode = txtImageShackRegistrationCode.Text;
        }

        private void ZScreen_DragEnter(object sender, DragEventArgs e)
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

        public void ScreenshotUsingDragDrop(string fp)
        {
            StartWorkerImages(MainAppTask.Jobs.PROCESS_DRAG_N_DROP, fp);
        }

        public void ScreenshotUsingDragDrop(string[] paths)
        {
            foreach (string fp in FileSystem.GetExplorerFileList(paths))
            {
                ScreenshotUsingDragDrop(fp);
            }
        }

        private void ZScreen_DragDrop(object sender, DragEventArgs e)
        {
            string[] paths = (string[])e.Data.GetData(DataFormats.FileDrop, true);
            ScreenshotUsingDragDrop(paths);
        }

        private void tpMain_DragDrop(object sender, DragEventArgs e)
        {
            string[] paths = (string[])e.Data.GetData(DataFormats.FileDrop, true);
            ScreenshotUsingDragDrop(paths);
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

        private void ScreenshotUsingClipboard()
        {
            List<string> files = GetClipboardImagePaths();
            foreach (string fp in files)
            {
                StartWorkerImages(MainAppTask.Jobs.IMAGEUPLOAD_FROM_CLIPBOARD, fp);
            }
        }

        private List<string> GetClipboardImagePaths()
        {
            List<string> cbListFilePath = new List<string>();

            string cbFilePath = null;
            Image cImage = Clipboard.GetImage();

            string fileName = NameParser.Convert(Program.conf.entireScreen, NameParser.NameType.EntireScreen);

            if (cImage != null)
            {
                cbFilePath = FileSystem.GetFilePath(fileName, false); // Path.Combine(Program.conf.path, Path.Combine("ClipboardImage.", mFileTypes[Program.conf.FileFormat]));
                cbFilePath = FileSystem.SaveImage(cImage, cbFilePath);
                cbListFilePath.Add(cbFilePath);
            }
            else
            {
                if (Clipboard.ContainsFileDropList())
                {
                    foreach (string fp in FileSystem.GetExplorerFileList(Clipboard.GetFileDropList()))
                    {
                        cbFilePath = Path.Combine(Program.conf.ImagesDir, Path.GetFileName(fp)); //fileName.ToString() + Path.GetExtension(filePath[0]));
                        System.IO.File.Copy(fp, cbFilePath, true);
                        cbListFilePath.Add(cbFilePath);
                    }
                }
            }
            return cbListFilePath;
        }

        private void cbShowPopup_CheckedChanged(object sender, EventArgs e)
        {
            Program.conf.ShowBalloonTip = cbShowPopup.Checked;
        }

        private void btnDeleteSettings_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you really want to revert settings to default values?", Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                Program.conf = new XMLSettings();
                SetupScreen();
                Program.conf.RunOnce = true;
                Program.conf.Save();
            }
        }

        private void ShowMainWindow()
        {
            if (!this.Visible)
            {
                this.Show();
                this.WindowState = FormWindowState.Normal;
                User32.ActivateWindow(this.Handle);
            }
        }

        private void niTray_BalloonTipClicked(object sender, EventArgs e)
        {
            if (Program.conf.BalloonTipOpenLink)
            {
                try
                {
                    NotifyIcon ni = (NotifyIcon)sender;
                    if (ni.Tag != null)
                    {
                        MainAppTask t = (MainAppTask)niTray.Tag;
                        string cbString;
                        switch (t.Job)
                        {
                            case MainAppTask.Jobs.LANGUAGE_TRANSLATOR:
                                cbString = t.TranslationInfo.Result.TranslatedText;
                                if (!string.IsNullOrEmpty(cbString))
                                {
                                    Clipboard.SetText(cbString);
                                }
                                break;
                            default:
                                switch (t.ImageDestCategory)
                                {
                                    case ImageDestType.FILE:
                                    case ImageDestType.CLIPBOARD:
                                        cbString = t.ImageLocalPath;
                                        if (!string.IsNullOrEmpty(cbString))
                                        {
                                            Process.Start(cbString);
                                        }
                                        break;
                                    default:
                                        cbString = t.ImageRemotePath;
                                        if (!string.IsNullOrEmpty(cbString))
                                        {
                                            Process.Start(cbString);
                                        }
                                        break;
                                }
                                break;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
        }



        private void nScreenshotDelay_ValueChanged(object sender, EventArgs e)
        {
            Program.conf.ScreenshotDelay = nScreenshotDelay.Value;
        }

        private void tsmDropWindow_Click(object sender, EventArgs e)
        {
            ShowDropWindow();
        }


        private void ShowDropWindow()
        {

            if (!bDropWindowOpened)
            {
                bDropWindowOpened = true;
                DropWindow dw = Program.MyDropWindow;
                dw.Location = new Point(SystemInformation.PrimaryMonitorSize.Width - dw.Width * 2, SystemInformation.PrimaryMonitorSize.Height - dw.Height * 2);
                dw.ShowDialog();
                if (dw.DialogResult == DialogResult.OK)
                {
                    ScreenshotUsingDragDrop(dw.FilePaths);
                }
                bDropWindowOpened = false;
            }
        }

        #region Image Uploaders

        private void btnUploaderAdd_Click(object sender, EventArgs e)
        {
            if (txtUploader.Text != "")
            {
                ImageHostingService iUploader = GetUploaderFromFields();
                Program.conf.ImageUploadersList.Add(iUploader);
                lbUploader.Items.Add(iUploader.Name);
                lbUploader.SelectedIndex = lbUploader.Items.Count - 1;
            }
        }

        private void btnUploaderRemove_Click(object sender, EventArgs e)
        {
            if (lbUploader.SelectedIndex != -1)
            {
                int selected = lbUploader.SelectedIndex;
                Program.conf.ImageUploadersList.RemoveAt(selected);
                lbUploader.Items.RemoveAt(selected);
                LoadImageUploaders(new ImageHostingService());
            }
        }

        private ImageHostingService GetUploaderFromFields()
        {
            ImageHostingService iUploader = new ImageHostingService(txtUploader.Text);
            foreach (ListViewItem lvItem in lvArguments.Items)
            {
                iUploader.Arguments.Add(new string[] { lvItem.Text, lvItem.SubItems[1].Text });
            }
            iUploader.UploadURL = txtUploadURL.Text;
            iUploader.FileForm = txtFileForm.Text;
            foreach (ListViewItem lvItem in lvRegexps.Items)
            {
                iUploader.RegexpList.Add(lvItem.Text);
            }
            iUploader.Fullimage = txtFullImage.Text;
            iUploader.Thumbnail = txtThumbnail.Text;
            return iUploader;
        }

        private void btnArgAdd_Click(object sender, EventArgs e)
        {
            if (txtArg1.Text != "")
            {
                lvArguments.Items.Add(txtArg1.Text).SubItems.Add(txtArg2.Text);
                txtArg1.Text = "";
                txtArg2.Text = "";
                txtArg1.Focus();
            }
        }

        private void btnArgEdit_Click(object sender, EventArgs e)
        {
            if (lvArguments.SelectedItems.Count > 0 && txtArg1.Text != "")
            {
                lvArguments.SelectedItems[0].Text = txtArg1.Text;
                lvArguments.SelectedItems[0].SubItems[1].Text = txtArg2.Text;
            }
        }

        private void btnArgRemove_Click(object sender, EventArgs e)
        {
            if (lvArguments.SelectedItems.Count > 0)
            {
                lvArguments.SelectedItems[0].Remove();
            }
        }

        private void btnRegexpAdd_Click(object sender, EventArgs e)
        {
            if (txtRegexp.Text != "")
            {
                if (txtRegexp.Text.StartsWith("!tag"))
                {
                    lvRegexps.Items.Add(String.Format("(?<={0}>).*(?=</{0})",
                        txtRegexp.Text.Substring(4, txtRegexp.Text.Length - 4).Trim()));
                }
                else
                {
                    lvRegexps.Items.Add(txtRegexp.Text);
                }
                txtRegexp.Text = "";
                txtRegexp.Focus();
            }
        }

        private void btnRegexpEdit_Click(object sender, EventArgs e)
        {
            if (lvRegexps.SelectedItems.Count > 0 && txtRegexp.Text != "")
            {
                lvRegexps.SelectedItems[0].Text = txtRegexp.Text;
            }
        }

        private void btnRegexpRemove_Click(object sender, EventArgs e)
        {
            if (lvRegexps.SelectedItems.Count > 0)
            {
                lvRegexps.SelectedItems[0].Remove();
            }
        }

        private void lbUploader_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lbUploader.SelectedIndex != -1)
            {
                LoadImageUploaders(Program.conf.ImageUploadersList[lbUploader.SelectedIndex]);
                Program.conf.ImageUploaderSelected = lbUploader.SelectedIndex;
                RewriteCustomUploaderRightClickMenu();
            }
        }

        private void LoadImageUploaders(ImageHostingService imageUploader)
        {
            txtArg1.Text = "";
            txtArg2.Text = "";
            lvArguments.Items.Clear();
            foreach (string[] args in imageUploader.Arguments)
            {
                lvArguments.Items.Add(args[0]).SubItems.Add(args[1]);
            }
            txtUploadURL.Text = imageUploader.UploadURL;
            txtFileForm.Text = imageUploader.FileForm;
            txtRegexp.Text = "";
            lvRegexps.Items.Clear();
            foreach (string regexp in imageUploader.RegexpList)
            {
                lvRegexps.Items.Add(regexp);
            }
            txtFullImage.Text = imageUploader.Fullimage;
            txtThumbnail.Text = imageUploader.Thumbnail;
        }

        private void btnUploadersUpdate_Click(object sender, EventArgs e)
        {
            if (lbUploader.SelectedIndex != -1)
            {
                ImageHostingService iUploader = GetUploaderFromFields();
                iUploader.Name = lbUploader.SelectedItem.ToString();
                Program.conf.ImageUploadersList[lbUploader.SelectedIndex] = iUploader;
            }
            RewriteCustomUploaderRightClickMenu();
        }

        private void btnUploadersClear_Click(object sender, EventArgs e)
        {
            LoadImageUploaders(new ImageHostingService());
        }

        private void btUploadersTest_Click(object sender, EventArgs e)
        {
            if (lbUploader.SelectedIndex != -1)
            {
                btnUploadersTest.Enabled = false;
                CustomUploader cUploader = new CustomUploader(Program.conf.ImageUploadersList[lbUploader.SelectedIndex]);
                string fp = Path.Combine(Program.conf.TempDir, "CustomUploaderText.png");
                StartWorkerImages(MainAppTask.Jobs.CUSTOM_UPLOADER_TEST, fp);
            }
        }

        private void btnUploaderExport_Click(object sender, EventArgs e)
        {
            if (Program.conf.ImageUploadersList != null)
            {
                SaveFileDialog dlg = new SaveFileDialog();
                dlg.FileName = string.Format("{0}-{1}-uploaders", Application.ProductName, DateTime.Now.ToString("yyyyMMdd"));
                dlg.Filter = Program.FILTER_IMAGE_HOSTING_SERVICES;
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    ImageHostingServiceManager ihsm = new ImageHostingServiceManager();
                    ihsm.ImageHostingServices = Program.conf.ImageUploadersList;
                    ihsm.Save(dlg.FileName);
                }
            }
        }

        private void ImportImageUploaders(string fp)
        {
            ImageHostingServiceManager tmp = new ImageHostingServiceManager();
            tmp = ImageHostingServiceManager.Read(fp);
            if (tmp != null)
            {
                lbUploader.Items.Clear();
                Program.conf.ImageUploadersList = new List<ImageHostingService>();
                Program.conf.ImageUploadersList.AddRange(tmp.ImageHostingServices);
                foreach (ImageHostingService iHostingService in Program.conf.ImageUploadersList)
                {
                    lbUploader.Items.Add(iHostingService.Name);
                }
            }
        }

        private void btnUploaderImport_Click(object sender, EventArgs e)
        {
            if (Program.conf.ImageUploadersList == null)
                Program.conf.ImageUploadersList = new List<ImageHostingService>();

            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = Program.FILTER_IMAGE_HOSTING_SERVICES;
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                ImportImageUploaders(dlg.FileName);
            }
        }

        private void btnOpenSourceText_Click(object sender, EventArgs e)
        {
            OpenLastSource(ImageFileManager.SourceType.TEXT);
        }

        private void btnOpenSourceBrowser_Click(object sender, EventArgs e)
        {
            OpenLastSource(ImageFileManager.SourceType.HTML);
        }

        private void btnOpenSourceString_Click(object sender, EventArgs e)
        {
            OpenLastSource(ImageFileManager.SourceType.STRING);
        }

        public bool OpenLastSource(ImageFileManager.SourceType sType)
        {
            return OpenSource(ClipboardManager.GetLastImageUpload(), sType);
        }

        public bool OpenSource(ImageFileManager ifm, ImageFileManager.SourceType sType)
        {
            string path = ifm.GetSource(Program.conf.TempDir, sType);
            if (!string.IsNullOrEmpty(path))
            {
                if (sType == ImageFileManager.SourceType.TEXT || sType == ImageFileManager.SourceType.HTML)
                {
                    Process.Start(path);
                    return true;
                }
                else if (sType == ImageFileManager.SourceType.STRING)
                {
                    Clipboard.SetText(path);
                    return true;
                }
            }
            return false;
        }

        private void txtUploadersLog_LinkClicked(object sender, LinkClickedEventArgs e)
        {
            Process.Start(e.LinkText);
        }

        #endregion

        private void dgvHotkeys_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Ignore clicks
            if (e.RowIndex < 0 || e.ColumnIndex != dgvHotkeys.Columns[1].Index)
            {
                return;
            }

            mSetHotkeys = true;
            mHKSelectedRow = e.RowIndex;

            lblHotkeyStatus.Text = "Press the keys you would like to use... Press enter when done setting all desired Hotkeys.";
        }

        private void UpdateHotkeysDGV()
        {
            dgvHotkeys.Rows.Clear();

            dgvHotkeys.Rows.Add(new object[] { "Active Window", Program.conf.HKActiveWindow });
            dgvHotkeys.Rows.Add(new object[] { "Selected Window", Program.conf.HKSelectedWindow });
            dgvHotkeys.Rows.Add(new object[] { "Entire Screen", Program.conf.HKEntireScreen });
            dgvHotkeys.Rows.Add(new object[] { "Crop Shot", Program.conf.HKCropShot });
            dgvHotkeys.Rows.Add(new object[] { "Last Crop Shot", Program.conf.HKLastCropShot });
            dgvHotkeys.Rows.Add(new object[] { "Clipboard Upload", Program.conf.HKClipboardUpload });
            dgvHotkeys.Rows.Add(new object[] { "Quick Options", Program.conf.HKQuickOptions });
            dgvHotkeys.Rows.Add(new object[] { "Drop Window", Program.conf.HKDropWindow });
            dgvHotkeys.Rows.Add(new object[] { "Language Translator", Program.conf.HKLanguageTranslator });

            dgvHotkeys.Refresh();
        }

        private void SetHotkey(int row, HKcombo hkc)
        {
            switch (row)
            {
                case 0: //active window
                    Program.conf.HKActiveWindow = hkc;
                    break;
                case 1: //selected window
                    Program.conf.HKSelectedWindow = hkc;
                    break;
                case 2: //entire screen
                    Program.conf.HKEntireScreen = hkc;
                    break;
                case 3: //crop shot
                    Program.conf.HKCropShot = hkc;
                    break;
                case 4: //last crop shot
                    Program.conf.HKLastCropShot = hkc;
                    break;
                case 5: //clipboard upload
                    Program.conf.HKClipboardUpload = hkc;
                    break;
                case 6: //select quick options
                    Program.conf.HKQuickOptions = hkc;
                    break;
                case 7: //drop window
                    Program.conf.HKDropWindow = hkc;
                    break;
                case 8: //language translator
                    Program.conf.HKLanguageTranslator = hkc;
                    break;
            }

            lblHotkeyStatus.Text = dgvHotkeys.Rows[mHKSelectedRow].Cells[0].Value + " Hotkey set to: " + mHKSetcombo.ToString() + ". Press enter when done setting all desired Hotkeys.";
        }

        private void dgvHotkeys_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                txtActiveHelp.Text = dgvHotkeys.Rows[e.RowIndex].Cells[0].Value + ": allows you to ";

                switch (e.RowIndex)
                {
                    case 0: //active window
                        txtActiveHelp.Text += "capture a window that is currently highlighted and send it your selected destination.";
                        break;
                    case 1: //selected window
                        txtActiveHelp.Text += "capture a window by selecting a window from the mouse and send it your selected destination.";
                        break;
                    case 2: //entire screen
                        txtActiveHelp.Text += "capture everything present on your screen including taskbar, start menu, etc and send it your selected destination";
                        break;
                    case 3: //crop shot
                        txtActiveHelp.Text += "capture a specified region of your screen and send it to your selected destination";
                        break;
                    case 4: //last crop shot
                        txtActiveHelp.Text += "capture the specified region from crop shot another time";
                        break;
                    case 5: //clipboard upload
                        txtActiveHelp.Text += "send files from your file system to your selected destination.";
                        break;
                    case 6: // quick options
                        txtActiveHelp.Text += "quickly select the destination you would like to send images via a small pop up form.";
                        break;
                    case 7: // drop window
                        txtActiveHelp.Text += "display a Drop Window so can drag and drop image files from Windows Explorer to upload.";
                        break;
                    case 8: // language translator
                        txtActiveHelp.Text += "translate the text that is in your clipboard from one language to another. See HTTP -> Language Translator for settings.";
                        break;

                }
            }
        }

        private void dgvHotkeys_Leave(object sender, EventArgs e)
        {
            QuitSettingHotkeys();
        }

        private void QuitSettingHotkeys()
        {
            if (mSetHotkeys)
            {
                mSetHotkeys = false;


                if (mHKSetcombo != null)
                {

                    lblHotkeyStatus.Text = dgvHotkeys.Rows[mHKSelectedRow].Cells[0].Value + " Hotkey Updated.";
                }
                else
                {
                    lblHotkeyStatus.Text = "Aborted Hotkey selection. Click on a Hotkey to set.";
                }

                mHKSetcombo = null;
                mHKSelectedRow = -1;
            }
        }

        private void ZScreen_Leave(object sender, EventArgs e)
        {
            QuitSettingHotkeys();
        }

        private void dgvHotkeys_MouseLeave(object sender, EventArgs e)
        {
            QuitSettingHotkeys();
        }

        private void cbActiveHelp_CheckedChanged(object sender, EventArgs e)
        {
            this.Height = (cbActiveHelp.Checked ? startHeight : startHeight - txtActiveHelp.Height);
            splitContainerApp.Panel2Collapsed = !cbActiveHelp.Checked;
            Program.conf.ActiveHelp = cbActiveHelp.Checked;
        }

        private void AddMouseHoverEventHandlerHelp(Control.ControlCollection col)
        {
            foreach (Control c in col)
            {
                if (c.Tag != null && c.Tag.ToString() != "")
                {
                    c.MouseHover += new EventHandler(HelpMouseHoverEvent);
                }

                AddMouseHoverEventHandlerHelp(c.Controls);
            }
        }

        private void HelpMouseHoverEvent(object sender, EventArgs e)
        {
            if (Program.conf.ActiveHelp)
            {
                string help = ((Control)sender).Tag.ToString();
                if (mGTranslator != null && Program.conf.GTActiveHelp)
                {
                    StartGTActiveHelp(help);
                }
                else
                {
                    txtActiveHelp.Text = help;
                }
            }
        }

        private void StartGTActiveHelp(string help)
        {
            bwActiveHelp.DoWork += new DoWorkEventHandler(bwActiveHelp_DoWork);
            ZSS.GoogleTranslate.TranslationInfo ti = new GoogleTranslate.TranslationInfo(help, new GoogleTranslate.GTLanguage("en", "English"), mGTranslator.LanguageOptions.TargetLangList[cbHelpToLanguage.SelectedIndex]);
            bwActiveHelp.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bwActiveHelp_RunWorkerCompleted);
            if (!bwActiveHelp.IsBusy)
            {
                bwActiveHelp.RunWorkerAsync(ti);
            }
        }

        private void bwActiveHelp_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Result != null)
            {
                ZSS.GoogleTranslate.ResultPacket grp = (ZSS.GoogleTranslate.ResultPacket)e.Result;
                txtActiveHelp.Text = grp.TranslatedText;
            }
        }

        private void bwActiveHelp_DoWork(object sender, DoWorkEventArgs e)
        {
            ZSS.GoogleTranslate.TranslationInfo ti = (ZSS.GoogleTranslate.TranslationInfo)e.Argument;
            ZSS.GoogleTranslate.ResultPacket grp = mGTranslator.TranslateText(ti);
            e.Result = grp;
        }

        private void btnRegCodeTinyPic_Click(object sender, EventArgs e)
        {
            UserPassBox ub = new UserPassBox("Enter TinyPic Email Address and Password", "someone@gmail.com");
            ub.Icon = this.Icon;
            ub.ShowDialog();
            if (ub.DialogResult == DialogResult.OK)
            {
                TinyPicUploader tpu = new TinyPicUploader(Program.TINYPIC_ID, Program.TINYPIC_KEY, UploadMode.API);
                txtTinyPicShuk.Text = tpu.UserAuth(ub.UserName, ub.Password);
            }
            this.BringToFront();
        }

        private void txtTinyPicShuk_TextChanged(object sender, EventArgs e)
        {
            Program.conf.TinyPicShuk = txtTinyPicShuk.Text;
        }

        private void tabControl_Selected(object sender, TabControlEventArgs e)
        {
            // What the tab control will display when you change from one section to another

            string tabDesc = "In this section you can ";

            if (e.TabPage == tpMain)
            {
                txtActiveHelp.Text = tabDesc + "select the destination that images are uploaded to, enable/disable crop settings, and turn Active Help on and off.";
            }
            else if (e.TabPage == tpHotKeys)
            {
                txtActiveHelp.Text = tabDesc + "customize hotkeys that you would like to use. To set a Hotkey click on a button and follow the directions provided above.";
            }
            else if (e.TabPage == tpFTP)
            {
                txtActiveHelp.Text = tabDesc + "add/remove FTP accounts that you use to upload screenshots. You can also drag and drop any other non-image file to the Drop Window to upload it to FTP.";
            }
            else if (e.TabPage == tpHTTP)
            {
                txtActiveHelp.Text = tabDesc + "configure the Image Hosting Service you prefer to upload the screenshot.";
            }
            else if (e.TabPage == tpImageSoftware)
            {
                txtActiveHelp.Text = tabDesc + string.Format("configure the Image Editing application you wish to run after taking the screenshot. {0} will automatically load this application and enable you to edit the image before uploading.", Application.ProductName);
            }
            else if (e.TabPage == tpFile)
            {
                txtActiveHelp.Text = tabDesc + string.Format("customize file naming patterns for the screenshot you are taking.");
            }
            else if (e.TabPage == tpScreenshots)
            {
                txtActiveHelp.Text = tabDesc + "copy screenshot URLs to Clipboard under diffent modes and preview the screenshots. To access Copy to Clipboard options, right click on one or more screenshot entries in the Screenshots list box.";
            }
            else if (e.TabPage == tpAdvanced)
            {
                txtActiveHelp.Text = tabDesc + "access the folder where settings are saved and import/export/revert settings";
            }
            else if (e.TabPage == tpCustomUploaders)
            {
                txtActiveHelp.Text = "Wiki: http://code.google.com/p/zscreen/wiki/CustomUploadersHelp";
            }
        }

        private void ActiveHelpTagsConfig()
        {
            //////////////////////////////////
            // Main Tab
            //////////////////////////////////
            cboScreenshotDest.Tag = "Select destination for the Screenshot. Destination can also be changed using the Tray menu.";

            cboClipboardTextMode.Tag = "Copy to Clipboard Mode specifies what kind of URL you would like to be added to your clipboard.\n\"Full Image\" returns a normal full-size image URL.\n\"Full Image for Forums\" returns BBcode for embedding images into forum posts.\n\"Thumbnail\" returns the thumbnail (a small image) of your uploaded image.\n\"Linked Thumbnail\" is the same as \"Thumbnail\" except that it links the thumbnail to the full-size image.";

            //active help inconsistency (uses label because numeric up/down doesn't support mousehover event
            lblScreenshotDelay.Tag = "The amount of time in second (half-second intervals) that the program will pause before taking a Full Screen or Active Window Screenshot.";

            cbCompleteSound.Tag = "When checked a sound will be played after an image successfully reaches the destination you have set (Clipboard, FTP, etc).";

            cbShowCursor.Tag = "When checked your mouse cursor will be captured in screenshots. This is useful for quickly pointing to things.";

            cbActiveHelp.Tag = String.Format("When checked a friendly set of information inside of this textbox will be displayed about each control that you hover. You can use the {0} check box to toggle it on or off. The form will automatically resize itself after checking/unchecking the checkbox", cbActiveHelp.Text);

            llblBugReports.Tag = String.Format("Send the developers of {0} a bug report or a suggestion so that we can improve the program.", Application.ProductName);

            //////////////////////////////////
            // Capture Tab
            //////////////////////////////////
            chkManualNaming.Tag = "When checked automatic naming conventions will be ignored and instead you can specify your own name for a screenshot manually.";

            txtEntireScreen.Tag = "The automatic naming convention used for all types of screenshots besides Active Window.";

            txtActiveWindow.Tag = "The automatic naming convention used for active window screenshots.";

            nudSwitchAfter.Tag = string.Format("After {0} KiB, {1} will switch format from {2} to JPG", nudSwitchAfter.Text, Application.ProductName, cmbFileFormat.Text.ToUpper());

            nudWatermarkOffset.Tag = string.Format("Move Watermark {0} pixels leftwards and {0} pixels upwards from the Bottom Right corner of the Screenshot.", nudWatermarkOffset.Value);

            txtWatermarkText.Tag = "The naming pattern that watermarks follow. To close this context menu just click in another textbox.";

            //Paths
            txtFileDirectory.Tag = "The directory where all screenshots will be placed (unless deleted with the option below).";

            cmbFileFormat.Tag = "The format that screenshots will be saved as.";

            //active help inconsistency (uses label because numeric up/down doesn't support mousehover event
            lblQuality.Tag = "The quality (1-100%) of JPEG screenshots. This quality setting does not effect any other type of Image Format.";

            cmbSwitchFormat.Tag = "The secondary format that the program will switch to after a user-specified limit has been reached.";

            nudSwitchAfter.Tag = "At this limit File Format will switch from the original format to the secondary format.";

            cbDeleteLocal.Tag = "When checked files that you upload will be deleted locally to save hard disk space.";

            //////////////////////////////////
            // Custom Uploaders Tab
            //////////////////////////////////

            //////////////////////////////////
            // Screenshots
            //////////////////////////////////
            lbHistory.Tag = "Right click to access Copy to Clipboard options.";
            txtHistoryLocalPath.Tag = "Double click to copy File Path to Clipboard.";
            txtHistoryRemotePath.Tag = "Double click to copy URL to Clipboard.";
        }

        /// <summary>
        /// Check for valid image and update task.Errors with the error message
        /// </summary>
        /// <param name="task"></param>
        /// <returns></returns>
        private bool IsValidImage(ref MainAppTask task)
        {
            bool isImage = true;

            try
            {
                Image.FromFile(task.ImageLocalPath).Dispose();
            }
            catch (OutOfMemoryException ex)
            {
                task.Errors.Add("Unsupported image. " + ex.Message);
                isImage = false;
            }
            return isImage;
        }

        private void cbCropStyle_SelectedIndexChanged(object sender, EventArgs e)
        {
            Program.conf.CropStyle = cbCropStyle.SelectedIndex;
        }

        private void pbCropBorderColor_Click(object sender, EventArgs e)
        {
            SelectColor((PictureBox)sender, ref Program.conf.CropBorderColor);
        }

        private void nudCropBorderSize_ValueChanged(object sender, EventArgs e)
        {
            Program.conf.CropBorderSize = nudCropBorderSize.Value;
        }

        private void llblBugReports_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(Program.URL_ISSUES);
        }

        private void cbCompleteSound_CheckedChanged(object sender, EventArgs e)
        {
            Program.conf.CompleteSound = cbCompleteSound.Checked;
        }

        private void cbCheckUpdates_CheckedChanged(object sender, EventArgs e)
        {
            Program.conf.CheckUpdates = cbCheckUpdates.Checked;
            cbCheckExperimental.Enabled = Program.conf.CheckUpdates;
        }

        public Image CropImage(Image img, Rectangle rect)
        {
            Image Cropped = new Bitmap(rect.Width, rect.Height);
            Graphics e = Graphics.FromImage(Cropped);
            e.CompositingQuality = CompositingQuality.HighQuality;
            e.SmoothingMode = SmoothingMode.HighQuality;
            e.InterpolationMode = InterpolationMode.HighQualityBicubic;
            e.DrawImage(img, new Rectangle(0, 0, rect.Width, rect.Height), rect, GraphicsUnit.Pixel);
            return Cropped;
        }

        private void txtActiveHelp_LinkClicked(object sender, LinkClickedEventArgs e)
        {
            Process.Start(e.LinkText);
        }

        private void SetClipboardFromHistory(ZSS.ClipboardUriType type)
        {
            if (lbHistory.SelectedIndex != -1)
            {
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < lbHistory.SelectedItems.Count; i++)
                {
                    HistoryItem hi = (HistoryItem)lbHistory.SelectedItems[i];
                    if (hi.ScreenshotManager != null)
                    {
                        sb.Append(hi.ScreenshotManager.GetUrlByType(type));
                        if (i < lbHistory.SelectedItems.Count - 1)
                        {
                            sb.AppendLine();
                        }
                    }
                }
                string temp = sb.ToString();
                if (!string.IsNullOrEmpty(temp))
                {
                    Clipboard.SetText(temp);
                }
            }
        }

        private void lbHistory_MouseDown(object sender, MouseEventArgs e)
        {
            cmsHistory.Enabled = lbHistory.Items.Count > 0;

            int i = lbHistory.IndexFromPoint(e.X, e.Y);

            if (i >= 0 && i < lbHistory.Items.Count)
            {
                lbHistory.SelectedIndex = i;
            }

            lbHistory.Refresh();

            HistoryItem hi = ((HistoryItem)lbHistory.SelectedItem);
            if (hi != null)
            {
                tsmCopyCbHistory.Enabled = hi.ScreenshotManager != null && !string.IsNullOrEmpty(hi.ScreenshotManager.URL);
                browseURLToolStripMenuItem.Enabled = tsmCopyCbHistory.Enabled;
                btnScreenshotBrowse.Enabled = tsmCopyCbHistory.Enabled;
                btnScreenshotOpen.Enabled = hi.ScreenshotManager != null && File.Exists(hi.ScreenshotManager.LocalFilePath);
            }

        }

        private void lbHistory_DoubleClick(object sender, EventArgs e)
        {
            if (lbHistory.SelectedIndex > -1)
            {
                HistoryItem hi = (HistoryItem)lbHistory.SelectedItem;
                if (!string.IsNullOrEmpty(hi.RemotePath)) Process.Start(((HistoryItem)lbHistory.SelectedItem).RemotePath);
            }
        }

        private void lbHistory_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lbHistory.SelectedIndex > -1)
            {
                HistoryItem hi = (HistoryItem)lbHistory.SelectedItem;
                if (!string.IsNullOrEmpty(hi.LocalPath) && File.Exists(hi.LocalPath))
                {
                    pbHistoryThumb.ImageLocation = hi.LocalPath;
                }
                else if (!string.IsNullOrEmpty(hi.RemotePath))
                {
                    pbHistoryThumb.ImageLocation = hi.RemotePath;
                }
                txtHistoryLocalPath.Text = hi.LocalPath;
                txtHistoryRemotePath.Text = hi.RemotePath;
                gbScreenshotPreview.Text = string.Format("{0} ({1})", hi.JobName, this.GetDestinationName(hi.MyTask));
            }
        }

        private string GetDestinationName(MainAppTask t)
        {
            string name = "";

            switch (t.ImageDestCategory)
            {
                case ImageDestType.FTP:
                case ImageDestType.CUSTOM_UPLOADER:
                    name = string.Format("{0}: {1}", t.ImageDestCategory.ToDescriptionString(), t.ImageDestinationName);
                    break;
                default:
                    name = string.Format("{0}", t.ImageDestCategory.ToDescriptionString());
                    break;
            }

            return name;
        }

        private void btnScreenshotOpen_Click(object sender, EventArgs e)
        {
            OpenLocalFile();
        }

        private void OpenLocalFile()
        {
            if (lbHistory.SelectedItem != null)
            {
                HistoryItem hi = (HistoryItem)lbHistory.SelectedItem;
                if (!string.IsNullOrEmpty(hi.LocalPath))
                {
                    Process.Start(hi.LocalPath);
                }
            }
        }

        private void OpenRemoteFile()
        {
            if (lbHistory.SelectedItem != null)
            {
                HistoryItem hi = (HistoryItem)lbHistory.SelectedItem;
                if (!string.IsNullOrEmpty(hi.RemotePath))
                {
                    Process.Start(hi.RemotePath);
                }
            }
        }

        private void btnScreenshotBrowse_Click(object sender, EventArgs e)
        {
            OpenRemoteFile();
        }

        private void cbShowWatermark_CheckedChanged(object sender, EventArgs e)
        {
            Program.conf.ShowWatermark = cbShowWatermark.Checked;
            TestWatermark();
        }

        private void txtWatermarkText_TextChanged(object sender, EventArgs e)
        {
            Program.conf.WatermarkText = txtWatermarkText.Text;
            TestWatermark();
        }

        private void CheckForCodes(object checkObject)
        {
            TextBox textBox = (TextBox)checkObject;
            if (codesMenu.Items.Count > 0)
            {
                codesMenu.Show(textBox, new Point(textBox.Width + 1, 0));
            }
        }

        private void CreateCodesMenu()
        {
            codesMenu.AutoClose = false;
            codesMenu.Font = new Font("Lucida Console", 8);
            codesMenu.Opacity = 0.8;
            codesMenu.ShowImageMargin = false;
            for (int i = 0; i < NameParser.replacementVars.Length; i++)
            {
                ToolStripMenuItem tsi = new ToolStripMenuItem();
                tsi.Text = NameParser.replacementVars[i].PadRight(3, ' ') + " - " + NameParser.replacementDescriptions[i];
                tsi.Tag = NameParser.replacementVars[i];
                tsi.Click += new EventHandler(watermarkCodeMenu_Click);
                codesMenu.Items.Add(tsi);
            }
            CodesMenuCloseEvents();
        }

        void watermarkCodeMenu_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem tsi = (ToolStripMenuItem)sender;
            int oldPos = txtWatermarkText.SelectionStart;
            string appendText;
            if (txtWatermarkText.Text[txtWatermarkText.SelectionStart - 1] == NameParser.prefix[0])
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

        private void cbShowCursor_CheckedChanged(object sender, EventArgs e)
        {
            Program.conf.ShowCursor = cbShowCursor.Checked;
        }

        private void btnWatermarkFont_Click(object sender, EventArgs e)
        {
            FontDialog fDialog = new FontDialog();
            fDialog.ShowColor = true;
            fDialog.Font = XMLSettings.DeserializeFont(Program.conf.WatermarkFont);
            fDialog.Color = XMLSettings.DeserializeColor(Program.conf.WatermarkFontColor);
            if (fDialog.ShowDialog() == DialogResult.OK)
            {
                Program.conf.WatermarkFont = XMLSettings.SerializeFont(fDialog.Font);
                Program.conf.WatermarkFontColor = XMLSettings.SerializeColor(fDialog.Color);
                pbWatermarkFontColor.BackColor = XMLSettings.DeserializeColor(Program.conf.WatermarkFontColor);
                lblWatermarkFont.Text = FontToString();
            }
            TestWatermark();
        }

        public string FontToString()
        {
            return FontToString(XMLSettings.DeserializeFont(Program.conf.WatermarkFont),
                 XMLSettings.DeserializeColor(Program.conf.WatermarkFontColor));
        }

        public string FontToString(Font font, Color color)
        {
            return "Name: " + font.Name + " - Size: " + font.Size.ToString() + " - Style: " + font.Style.ToString() + " - Color: " +
                (color.IsNamedColor ? color.Name : "(A:" + color.A + " R:" + color.R + " G:" + color.G + " B:" + color.B + ")");
        }

        private void nudWatermarkOffset_ValueChanged(object sender, EventArgs e)
        {
            Program.conf.WatermarkOffset = nudWatermarkOffset.Value;
            TestWatermark();
        }

        private void nudWatermarkBackTrans_ValueChanged(object sender, EventArgs e)
        {
            Program.conf.WatermarkBackTrans = nudWatermarkBackTrans.Value;
            TestWatermark();
        }

        private void entireScreenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Thread.Sleep(300);
            StartBW_EntireScreen();
        }

        private void selectedWindowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Thread.Sleep(300);
            StartBW_SelectedWindow();
        }

        private void rectangularRegionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Thread.Sleep(300);
            StartBW_CropShot();
        }

        private void lastRectangularRegionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Thread.Sleep(300);
            StartBW_LastCropShot();
        }

        private void tsmScrenshotFromClipboard_Click(object sender, EventArgs e)
        {
            ScreenshotUsingClipboard();
        }

        private void languageTranslatorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Clipboard.ContainsText()) StartBW_LanguageTranslator(Clipboard.GetText());
        }

        private void pbWatermarkGradient1_Click(object sender, EventArgs e)
        {
            SelectColor((PictureBox)sender, ref Program.conf.WatermarkGradient1);
            TestWatermark();
        }

        private void pbWatermarkGradient2_Click(object sender, EventArgs e)
        {
            SelectColor((PictureBox)sender, ref Program.conf.WatermarkGradient2);
            TestWatermark();
        }

        private void pbWatermarkBorderColor_Click(object sender, EventArgs e)
        {
            SelectColor((PictureBox)sender, ref Program.conf.WatermarkBorderColor);
            TestWatermark();
        }

        private void TestWatermark()
        {
            ComponentResourceManager resources = new ComponentResourceManager(typeof(ZScreen));
            Bitmap bmp = new Bitmap((Image)(resources.GetObject("pbLogo.Image")));
            bmp = bmp.Clone(new Rectangle(61, 32, 201, 141), PixelFormat.Format32bppArgb);
            pbWatermarkShow.Image = bmp;
            Graphics g = Graphics.FromImage(bmp);
            pbWatermarkShow.Image = WatermarkMaker.GetImage(bmp);
        }

        private void txtWatermarkText_Leave(object sender, EventArgs e)
        {
            if (codesMenu.Visible)
            {
                codesMenu.Close();
            }
        }

        private void pbWatermarkFontColor_Click(object sender, EventArgs e)
        {
            SelectColor((PictureBox)sender, ref Program.conf.WatermarkFontColor);
            lblWatermarkFont.Text = FontToString();
            TestWatermark();
        }

        private void SelectColor(PictureBox pb, ref string setting)
        {
            DialogColor dColor = new DialogColor(pb.BackColor);
            if (dColor.ShowDialog() == DialogResult.OK)
            {
                pb.BackColor = dColor.NewColor;
                setting = XMLSettings.SerializeColor(dColor.NewColor);
            }
        }

        private void cbWatermarkPosition_SelectedIndexChanged(object sender, EventArgs e)
        {
            Program.conf.WatermarkPositionMode = (WatermarkPositionType)cbWatermarkPosition.SelectedIndex;
            TestWatermark();
        }

        private void nudWatermarkFontTrans_ValueChanged(object sender, EventArgs e)
        {
            Program.conf.WatermarkFontTrans = nudWatermarkFontTrans.Value;
            TestWatermark();
        }

        private void nudWatermarkCornerRadius_ValueChanged(object sender, EventArgs e)
        {
            Program.conf.WatermarkCornerRadius = nudWatermarkCornerRadius.Value;
            TestWatermark();
        }

        private void cbWatermarkGradientType_SelectedIndexChanged(object sender, EventArgs e)
        {
            Program.conf.WatermarkGradientType = cbWatermarkGradientType.Text;
            TestWatermark();
        }

        private void copyImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (lbHistory.SelectedIndex != -1)
            {
                HistoryItem hi = (HistoryItem)lbHistory.SelectedItem;
                Clipboard.SetImage(MyGraphics.GetImageSafely(hi.LocalPath));
            }
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (lbHistory.SelectedIndex != -1)
            {
                List<HistoryItem> temp = new List<HistoryItem>();
                foreach (HistoryItem hi in lbHistory.SelectedItems)
                {
                    temp.Add(hi);
                }
                foreach (HistoryItem hi in temp)
                {
                    lbHistory.Items.Remove(hi);
                    if (File.Exists(hi.LocalPath))
                    {
                        File.Delete(hi.LocalPath);
                    }
                }
            }
        }

        private void btnViewLocalDirectory_Click(object sender, EventArgs e)
        {
            ShowDirectory(Program.conf.ImagesDir);
        }

        private void btnBrowseCacheLocation_Click(object sender, EventArgs e)
        {
            Program.conf.CacheDir = BrowseDirectory(ref txtCacheDir);
        }

        private void btnViewRemoteDirectory_Click(object sender, EventArgs e)
        {
            ShowDirectory(Program.conf.CacheDir);
        }

        private void cbOpenMainWindow_CheckedChanged(object sender, EventArgs e)
        {
            Program.conf.OpenMainWindow = cbOpenMainWindow.Checked;
        }

        private void cbShowTaskbar_CheckedChanged(object sender, EventArgs e)
        {
            Program.conf.ShowInTaskbar = cbShowTaskbar.Checked;
            if (mGuiIsReady)
            {
                this.ShowInTaskbar = Program.conf.ShowInTaskbar;
            }
        }

        private void llWebsite_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(Program.URL_WEBSITE);
        }

        private void llProjectPage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(Program.URL_PROJECTPAGE);
        }

        private void ZScreen_Deactivate(object sender, EventArgs e)
        {
            codesMenu.Close();
        }

        private void txtWatermarkText_MouseDown(object sender, MouseEventArgs e)
        {
            CheckForCodes(sender);
        }

        private void CodesMenuCloseEvents()
        {
            tpFileSettingsWatermark.MouseClick += new MouseEventHandler(CodesMenuCloseEvent);
            foreach (Control cntrl in tpFileSettingsWatermark.Controls)
            {
                if (cntrl.GetType() == typeof(GroupBox))
                {
                    cntrl.MouseClick += new MouseEventHandler(CodesMenuCloseEvent);
                }
            }
        }

        private void CodesMenuCloseEvent(object sender, MouseEventArgs e)
        {
            codesMenu.Close();
        }

        private void openLocalFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenLocalFile();
        }

        private void browseURLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenRemoteFile();
        }

        private void chkBalloonTipOpenLink_CheckedChanged(object sender, EventArgs e)
        {
            Program.conf.BalloonTipOpenLink = chkBalloonTipOpenLink.Checked;
        }

        private void cmVersionHistory_Click(object sender, EventArgs e)
        {
            ShowVersionHistory();
        }

        #region Language Translator

        private void DownloadLanguagesList()
        {
            BackgroundWorker bwLanguages = new BackgroundWorker();
            bwLanguages.DoWork += new DoWorkEventHandler(bwLanguages_DoWork);
            bwLanguages.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bwLanguages_RunWorkerCompleted);
            bwLanguages.RunWorkerAsync();
        }

        private void bwLanguages_DoWork(object sender, DoWorkEventArgs e)
        {
            mGTranslator = new GoogleTranslate();
        }

        private void bwLanguages_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (mGTranslator != null)
            {
                cbFromLanguage.Items.Clear();
                cbToLanguage.Items.Clear();
                cbHelpToLanguage.Items.Clear();
                foreach (ZSS.GoogleTranslate.GTLanguage gtLang in mGTranslator.LanguageOptions.SourceLangList)
                {
                    cbFromLanguage.Items.Add(gtLang.Name);
                }
                foreach (ZSS.GoogleTranslate.GTLanguage gtLang in mGTranslator.LanguageOptions.TargetLangList)
                {
                    cbToLanguage.Items.Add(gtLang.Name);
                    cbHelpToLanguage.Items.Add(gtLang.Name);
                }
                SelectLanguage(Program.conf.FromLanguage, Program.conf.ToLanguage, Program.conf.HelpToLanguage);
                cbFromLanguage.Enabled = true;
                cbToLanguage.Enabled = true;
                cbHelpToLanguage.Enabled = true;
            }
        }

        private void SelectLanguage(string srcLangValue, string targetLangValue, string helpTargetLangValue)
        {
            for (int i = 0; i < mGTranslator.LanguageOptions.SourceLangList.Count; i++)
            {
                if (mGTranslator.LanguageOptions.SourceLangList[i].Value == srcLangValue)
                {
                    if (cbFromLanguage.Items.Count > i) cbFromLanguage.SelectedIndex = i;
                    break;
                }
            }
            for (int i = 0; i < mGTranslator.LanguageOptions.TargetLangList.Count; i++)
            {
                if (mGTranslator.LanguageOptions.TargetLangList[i].Value == targetLangValue)
                {
                    if (cbToLanguage.Items.Count > i) cbToLanguage.SelectedIndex = i;
                }
                if (mGTranslator.LanguageOptions.TargetLangList[i].Value == helpTargetLangValue)
                {
                    if (cbHelpToLanguage.Items.Count > i) cbHelpToLanguage.SelectedIndex = i;
                }
            }
        }

        private void btnTranslate_Click(object sender, EventArgs e)
        {
            StartBW_LanguageTranslator();
        }

        private void LanguageTranslator(ref MainAppTask t)
        {
            t.TranslationInfo.Result = mGTranslator.TranslateText(t.TranslationInfo);
        }

        private void cbFromLanguage_SelectedIndexChanged(object sender, EventArgs e)
        {
            Program.conf.FromLanguage = mGTranslator.LanguageOptions.SourceLangList[cbFromLanguage.SelectedIndex].Value;
        }

        private void cbToLanguage_SelectedIndexChanged(object sender, EventArgs e)
        {
            Program.conf.ToLanguage = mGTranslator.LanguageOptions.TargetLangList[cbToLanguage.SelectedIndex].Value;
        }

        private void cbClipboardTranslate_CheckedChanged(object sender, EventArgs e)
        {
            Program.conf.ClipboardTranslate = cbClipboardTranslate.Checked;
        }

        private void txtTranslateText_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                StartBW_LanguageTranslator();
            }
        }

        #endregion

        private void cboUploadMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            Program.conf.UploadMode = (UploadMode)cboUploadMode.SelectedIndex;
            gbImageShack.Enabled = Program.conf.UploadMode == UploadMode.API;
            gbTinyPic.Enabled = Program.conf.UploadMode == UploadMode.API;
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HistoryItem hi = (HistoryItem)lbHistory.SelectedItem;
            OpenSource(hi.ScreenshotManager, ImageFileManager.SourceType.TEXT);
        }

        private void openSourceInDefaultWebBrowserHTMLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HistoryItem hi = (HistoryItem)lbHistory.SelectedItem;
            OpenSource(hi.ScreenshotManager, ImageFileManager.SourceType.HTML);
        }

        private void copySourceToClipboardStringToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HistoryItem hi = (HistoryItem)lbHistory.SelectedItem;
            OpenSource(hi.ScreenshotManager, ImageFileManager.SourceType.STRING);
        }

        private void cmsRetryUpload_Click(object sender, EventArgs e)
        {
            HistoryItem hi = (HistoryItem)lbHistory.SelectedItem;
            if (hi != null && File.Exists(hi.LocalPath))
            {
                hi.RetryUpload();
                // ScreenshotUsingDragDrop(hi.LocalPath);
            }
        }

        private void pbHistoryThumb_Click(object sender, EventArgs e)
        {
            HistoryItem hi = (HistoryItem)lbHistory.SelectedItem;
            if (hi != null && File.Exists(hi.LocalPath))
            {
                if (hi.ScreenshotManager != null)
                {
                    ShowScreenshot sc = new ShowScreenshot();
                    if (hi.ScreenshotManager.GetImage() != null)
                    {
                        sc.BackgroundImage = Image.FromFile(hi.LocalPath);
                        sc.ShowDialog();
                    }
                }
                else
                {
                    Process.Start(hi.LocalPath);
                }
            }
        }

        private void chkGTActiveHelp_CheckedChanged(object sender, EventArgs e)
        {
            Program.conf.GTActiveHelp = chkGTActiveHelp.Checked;
        }

        private void cbHelpToLanguage_SelectedIndexChanged(object sender, EventArgs e)
        {
            Program.conf.HelpToLanguage = mGTranslator.LanguageOptions.TargetLangList[cbHelpToLanguage.SelectedIndex].Value;
        }

        private void debugTimer_Tick(object sender, EventArgs e)
        {
            if (this.Visible && debug.IsReady)
            {
                lblDebugInfo.Text = debug.DebugInfo();
            }
        }

        private void btnCopyStats_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(lblDebugInfo.Text);
        }

        private void cbImageUploadRetry_CheckedChanged(object sender, EventArgs e)
        {
            Program.conf.ImageUploadRetry = chkImageUploadRetry.Checked;
        }

        #region FTP

        private string FTPAdd(FTPAccount acc)
        {
            return acc.Name + " - " + acc.Server + ":" + acc.Port;
        }

        private void FTPLoad(FTPAccount acc)
        {
            txtFTPName.Text = acc.Name;
            txtFTPServer.Text = acc.Server;
            nudFTPServerPort.Value = acc.Port;
            txtFTPUsername.Text = acc.Username;
            txtFTPPassword.Text = acc.Password;
            txtFTPPath.Text = acc.Path;
            txtFTPHTTPPath.Text = acc.HttpPath;
            rbFTPActive.Checked = acc.IsActive;
            rbFTPPassive.Checked = !acc.IsActive;
            gbFTPAccount.Text = string.Format("Settings: {0} - {1}", acc.Name, acc.Server);
        }

        private void FTPSetup(List<FTPAccount> accs)
        {
            if (accs != null)
            {
                lbFTPAccounts.Items.Clear();
                Program.conf.FTPAccountList = new List<FTPAccount>();
                Program.conf.FTPAccountList.AddRange(accs);
                foreach (FTPAccount acc in Program.conf.FTPAccountList)
                {
                    lbFTPAccounts.Items.Add(FTPAdd(acc));
                }
            }
        }

        private FTPAccount GetFTPAccountFromFields()
        {
            TrimFTPControls();

            FTPAccount acc = new FTPAccount();
            acc.Name = txtFTPName.Text;
            acc.Server = txtFTPServer.Text;
            acc.Port = (int)nudFTPServerPort.Value;
            acc.Username = txtFTPUsername.Text;
            acc.Password = txtFTPPassword.Text;
            if (!txtFTPPath.Text.StartsWith("/"))
                txtFTPPath.Text = string.Concat("/", txtFTPPath.Text);
            acc.Path = txtFTPPath.Text;
            acc.HttpPath = txtFTPHTTPPath.Text;
            acc.IsActive = rbFTPActive.Checked;

            return acc;
        }

        private bool ValidateFTP()
        {
            Control[] controls = { txtFTPServer, txtFTPUsername, txtFTPPassword, txtFTPPath };

            foreach (Control c in controls)
            {
                if (String.IsNullOrEmpty(c.Text))
                    return false;
            }

            return true;
        }

        private void UpdateFTP()
        {
            if (ValidateFTP() //txtServer.Text != "" && txtUsername.Text != "" && txtPassword.Text != "" && txtPath.Text != ""
                && lbFTPAccounts.SelectedIndices.Count == 1 && lbFTPAccounts.SelectedIndex != -1)
            {
                txtFTPStatus.Text = Properties.Resources.FTPupdated;

                FTPAccount acc = GetFTPAccountFromFields();

                if (Program.conf.FTPAccountList != null)
                {
                    Program.conf.FTPAccountList[lbFTPAccounts.SelectedIndex] = acc; //use selected index instead of 0
                }
                else
                {
                    Program.conf.FTPAccountList = new List<FTPAccount>();
                    Program.conf.FTPAccountList.Add(acc);
                }

                lbFTPAccounts.Items[lbFTPAccounts.SelectedIndex] = FTPAdd(acc);

                RewriteFTPRightClickMenu();
            }
            else
            {
                txtFTPStatus.Text = Properties.Resources.FTPnotUpdated;
            }
        }

        private void btnCloneAccount_Click(object sender, EventArgs e)
        {
            FTPAccount tmp = new FTPAccount();

            int src = lbFTPAccounts.SelectedIndex;

            if (Program.conf.FTPAccountList != null && Program.conf.FTPAccountList.Count > 0 && lbFTPAccounts.SelectedIndices.Count == 1 && src != -1)
            {
                int len = Program.conf.FTPAccountList.Count;

                tmp = Program.conf.FTPAccountList[src];

                //mDoingNameUpdate = true;
                lbFTPAccounts.Items.Add(tmp.Name);

                txtFTPServer.Text = tmp.Server;
                nudFTPServerPort.Text = tmp.Port.ToString();
                txtFTPUsername.Text = tmp.Username;
                txtFTPPassword.Text = tmp.Password;
                txtFTPPath.Text = tmp.Path;
                txtFTPHTTPPath.Text = tmp.HttpPath;
                rbFTPActive.Checked = tmp.IsActive;
                rbFTPPassive.Checked = !tmp.IsActive;

                Program.conf.FTPAccountList.Add(tmp);

                lbFTPAccounts.SelectedIndex = lbFTPAccounts.Items.Count - 1;
                txtFTPName.Text = tmp.Name;
                txtFTPName.Focus();
                txtFTPName.SelectAll();
                //mDoingNameUpdate = false;

                //rewriteFTPRightClickMenu();
            }
        }

        private void btnDeleteFTP_Click(object sender, EventArgs e)
        {
            int sel = lbFTPAccounts.SelectedIndex;

            if (sel != -1)
            {
                Program.conf.FTPAccountList.RemoveAt(sel);

                lbFTPAccounts.Items.RemoveAt(sel);

                if (lbFTPAccounts.Items.Count > 0)
                {
                    lbFTPAccounts.SelectedIndex = (sel > 0) ? (sel - 1) : 0;
                }
            }
        }

        private void lbFTPAccounts_SelectedIndexChanged(object sender, EventArgs e)
        {
            int sel = lbFTPAccounts.SelectedIndex;
            if (!mDoingNameUpdate && Program.conf.FTPAccountList != null && sel != -1 && sel < Program.conf.FTPAccountList.Count && Program.conf.FTPAccountList[sel] != null)
            {
                FTPLoad(Program.conf.FTPAccountList[sel]);
                Program.conf.FTPselected = lbFTPAccounts.SelectedIndex;
                RewriteFTPRightClickMenu();
            }
        }

        private void btnAddAccount_Click(object sender, EventArgs e)
        {
            if (ValidateFTP())
            {
                FTPAccount acc = GetFTPAccountFromFields();
                Program.conf.FTPAccountList.Add(acc);
                lbFTPAccounts.Items.Add(FTPAdd(acc));
                lbFTPAccounts.SelectedIndex = lbFTPAccounts.Items.Count - 1;
            }
            else
            {
                txtFTPStatus.Text = Resources.FTPnotUpdated; //change to FTP Account not added?
            }
        }

        private void btnUpdateFTP_Click(object sender, EventArgs e)
        {
            UpdateFTP();
        }

        private void btnClearFTP_Click(object sender, EventArgs e)
        {
            FTPLoad(new FTPAccount());
        }

        private void btnExportAccounts_Click(object sender, EventArgs e)
        {
            if (Program.conf.FTPAccountList != null)
            {
                SaveFileDialog dlg = new SaveFileDialog();
                dlg.FileName = string.Format("{0}-{1}-accounts", Application.ProductName, DateTime.Now.ToString("yyyyMMdd"));
                dlg.Filter = Program.FILTER_ACCOUNTS;
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    FTPAccountManager fam = new FTPAccountManager(Program.conf.FTPAccountList);
                    fam.Save(dlg.FileName);
                }
            }
        }

        private void btnAccsImport_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = Program.FILTER_ACCOUNTS;
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                FTPAccountManager fam = FTPAccountManager.Read(dlg.FileName);
                FTPSetup(fam.FTPAccounts);
            }
        }

        private void btnTestConnection_Click(object sender, EventArgs e)
        {
            txtFTPStatus.Text = Properties.Resources.FTPtest; //Testing
            txtFTPStatus.Update();
            TrimFTPControls();
            int port = (int)nudFTPServerPort.Value;

            try
            {
                FTPAccount acc = new FTPAccount();
                acc.Server = txtFTPServer.Text;
                acc.Port = port;
                acc.Username = txtFTPUsername.Text;
                acc.Password = txtFTPPassword.Text;
                acc.IsActive = rbFTPActive.Checked;
                acc.Path = txtFTPPath.Text;

                FTP ftp = new FTP(ref acc);
                if (ftp.ListDirectory() != null)
                {
                    txtFTPStatus.Text = Properties.Resources.FTPsuccess; //Success
                }
                else
                {
                    txtFTPStatus.Text = "FTP Settings are not set correctly. Make sure your FTP Path exists.";
                }
            }
            catch (Exception t)
            {
                txtFTPStatus.Text = t.Message;
            }
        }

        private void chkEnableThumbnail_CheckedChanged(object sender, EventArgs e)
        {
            Program.conf.EnableThumbnail = chkEnableThumbnail.Checked;
        }

        private void cbAutoSwitchFTP_CheckedChanged(object sender, EventArgs e)
        {
            Program.conf.AutoSwitchFTP = cbAutoSwitchFTP.Checked;
        }

        #endregion

        private void cbSelectedWindowFront_CheckedChanged(object sender, EventArgs e)
        {
            Program.conf.SelectedWindowFront = cbSelectedWindowFront.Checked;
        }

        private void cbSelectedWindowRectangleInfo_CheckedChanged(object sender, EventArgs e)
        {
            Program.conf.SelectedWindowRectangleInfo = cbSelectedWindowRectangleInfo.Checked;
        }

        private void pbSelectedWindowBorderColor_Click(object sender, EventArgs e)
        {
            SelectColor((PictureBox)sender, ref Program.conf.SelectedWindowBorderColor);
        }

        private void nudSelectedWindowBorderSize_ValueChanged(object sender, EventArgs e)
        {
            Program.conf.SelectedWindowBorderSize = nudSelectedWindowBorderSize.Value;
        }

        private void cbCheckExperimental_CheckedChanged(object sender, EventArgs e)
        {
            Program.conf.CheckExperimental = cbCheckExperimental.Checked;
        }

        private void btnCheckUpdate_Click(object sender, EventArgs e)
        {
            UpdateChecker.CheckUpdates();
        }

        private void screenColorPickerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogColor dialogColor = new DialogColor();
            dialogColor.ScreenPicker = true;
            dialogColor.ShowDialog();
            dialogColor.Dispose();
        }
    }
}
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
using ZSS.UpdateCheckerLib;

namespace ZSS
{
    public partial class ZScreen : Form
    {
        #region Variables

        public IntPtr KeyboardHookHandle = (IntPtr)1; //Used for the keyboard hook

        private bool mGuiIsReady;
        private bool mClose;
        private bool mTakingScreenShot;
        private bool mSetHotkeys;
        private int mHKSelectedRow = -1;
        private HKcombo mHKSetcombo;
        private TextBox mHadFocus;
        private int mHadFocusAt;
        private const int mWM_KEYDOWN = 0x0100;
        private const int mWM_SYSKEYDOWN = 0x0104;
        private bool bQuickOptionsOpened;
        private bool bDropWindowOpened;
        private bool bQuickActionsOpened;
        private ContextMenuStrip codesMenu = new ContextMenuStrip();
        private GoogleTranslate mGTranslator;
        private BackgroundWorker bwActiveHelp = new BackgroundWorker();
        private Debug debug;

        #endregion

        public ZScreen()
        {
            InitializeComponent();

            this.Icon = Resources.zss_main;
            this.Text = Program.mAppInfo.GetApplicationTitle(McoreSystem.AppInfo.VersionDepth.MajorMinorBuildRevision);
            this.niTray.Text = this.Text;
            lblLogo.Text = this.Text;

            SetupScreen();

            if (Program.conf.CheckUpdates) CheckUpdates();
        }

        private void ZScreen_Load(object sender, EventArgs e)
        {
            if (Program.conf.OpenMainWindow)
            {
                WindowState = FormWindowState.Normal;
                Size = Program.conf.WindowSize;
                ShowInTaskbar = Program.conf.ShowInTaskbar;
            }
            else
            {
                Hide();
            }

            CleanCache();
            StartDebug();

            niTray.BalloonTipClicked += new EventHandler(niTray_BalloonTipClicked);
            AddToClipboardByDoubleClick(tpHistory);

            ActiveHelpTagsConfig();
            AddMouseHoverEventHandlerHelp(Controls);

            FillClipboardCopyMenu();
            FillClipboardMenu();

            CreateCodesMenu();

            dgvHotkeys.BackgroundColor = Color.FromArgb(tpHotkeys.BackColor.R, tpHotkeys.BackColor.G, tpHotkeys.BackColor.B);
        }

        private void SetupScreen()
        {
            #region Global

            //~~~~~~~~~~~~~~~~~~~~~
            //  Global
            //~~~~~~~~~~~~~~~~~~~~~

            confApp.SelectedObject = Program.conf;
            txtRootFolder.Text = Program.RootAppFolder;

            UpdateGuiControlsPaths();
            txtActiveHelp.Text = String.Format("Welcome to {0}. To begin using Active Help all you need to do is hover over any control and this textbox will be updated with information about the control.", ProductName);

            #endregion

            #region Main

            //~~~~~~~~~~~~~~~~~~~~~
            //  Main
            //~~~~~~~~~~~~~~~~~~~~~

            if (cboScreenshotDest.Items.Count == 0)
            {
                cboScreenshotDest.Items.AddRange(typeof(ImageDestType).GetDescriptions());
            }
            cboScreenshotDest.SelectedIndex = (int)Program.conf.ScreenshotDestMode;
            if (cboClipboardTextMode.Items.Count == 0)
            {
                cboClipboardTextMode.Items.AddRange(typeof(ClipboardUriType).GetDescriptions());
            }
            cboClipboardTextMode.SelectedIndex = (int)Program.conf.ClipboardUriMode;
            nudScreenshotDelay.Value = Program.conf.ScreenshotDelay;
            cbPromptforUpload.Checked = Program.conf.PromptforUpload;
            chkManualNaming.Checked = Program.conf.ManualNaming;
            cbShowCursor.Checked = Program.conf.ShowCursor;
            cboShowWatermark.Checked = Program.conf.ShowWatermark;
            cboCropGridMode.Checked = Program.conf.CropGridToggle;
            nudCropGridWidth.Value = Program.conf.CropGridSize.Width;
            nudCropGridHeight.Value = Program.conf.CropGridSize.Height;
            // CheckActiveHelp();
            // cbActiveHelp.Checked = Program.conf.ActiveHelp;
            chkGTActiveHelp.Checked = Program.conf.GTActiveHelp;

            #endregion

            #region Hotkeys

            //~~~~~~~~~~~~~~~~~~~~~
            //  Hotkeys
            //~~~~~~~~~~~~~~~~~~~~~

            UpdateHotkeysDGV();

            #endregion

            #region Capture

            //~~~~~~~~~~~~~~~~~~~~~
            //  Capture
            //~~~~~~~~~~~~~~~~~~~~~

            // Crop Shot
            cbCropStyle.SelectedIndex = Program.conf.CropRegionStyle;
            cbRegionRectangleInfo.Checked = Program.conf.CropRegionRectangleInfo;
            cbCropDynamicCrosshair.Checked = Program.conf.CropDynamicCrosshair;
            nudCropCrosshairInterval.Value = Program.conf.CropInterval;
            nudCropCrosshairStep.Value = Program.conf.CropStep;
            nudCrosshairLineCount.Value = Program.conf.CrosshairLineCount;
            nudCrosshairLineSize.Value = Program.conf.CrosshairLineSize;
            pbCropCrosshairColor.BackColor = XMLSettings.DeserializeColor(Program.conf.CropCrosshairColor);
            cbCropShowBigCross.Checked = Program.conf.CropShowBigCross;
            cbShowCropRuler.Checked = Program.conf.CropShowRuler;
            cbCropDynamicBorderColor.Checked = Program.conf.CropDynamicBorderColor;
            nudCropRegionInterval.Value = Program.conf.CropRegionInterval;
            nudCropRegionStep.Value = Program.conf.CropRegionStep;
            nudCropHueRange.Value = Program.conf.CropHueRange;
            pbCropBorderColor.BackColor = XMLSettings.DeserializeColor(Program.conf.CropBorderColor);
            nudCropBorderSize.Value = Program.conf.CropBorderSize;
            cbCropShowGrids.Checked = Program.conf.CropShowGrids;
            cbRegionHotkeyInfo.Checked = Program.conf.CropRegionHotkeyInfo;

            // Selected Window
            cbSelectedWindowStyle.SelectedIndex = Program.conf.SelectedWindowRegionStyle;
            cbSelectedWindowFront.Checked = Program.conf.SelectedWindowFront;
            cbSelectedWindowRectangleInfo.Checked = Program.conf.SelectedWindowRectangleInfo;
            cbSelectedWindowRuler.Checked = Program.conf.SelectedWindowRuler;
            pbSelectedWindowBorderColor.BackColor = XMLSettings.DeserializeColor(Program.conf.SelectedWindowBorderColor);
            nudSelectedWindowBorderSize.Value = Program.conf.SelectedWindowBorderSize;
            cbSelectedWindowDynamicBorderColor.Checked = Program.conf.SelectedWindowDynamicBorderColor;
            nudSelectedWindowRegionInterval.Value = Program.conf.SelectedWindowRegionInterval;
            nudSelectedWindowRegionStep.Value = Program.conf.SelectedWindowRegionStep;
            nudSelectedWindowHueRange.Value = Program.conf.SelectedWindowHueRange;
            cbSelectedWindowAddBorder.Checked = Program.conf.SelectedWindowAddBorder;

            // Interaction
            nudFlashIconCount.Value = Program.conf.FlashTrayCount;
            chkCaptureFallback.Checked = Program.conf.CaptureEntireScreenOnError;
            cbShowPopup.Checked = Program.conf.ShowBalloonTip;
            chkBalloonTipOpenLink.Checked = Program.conf.BalloonTipOpenLink;
            cbShowUploadDuration.Checked = Program.conf.ShowUploadDuration;
            cbCompleteSound.Checked = Program.conf.CompleteSound;
            cbCloseDropBox.Checked = Program.conf.CloseDropBox;
            cbCloseQuickActions.Checked = Program.conf.CloseQuickActions;

            // Naming Conventions
            txtActiveWindow.Text = Program.conf.activeWindow;
            txtEntireScreen.Text = Program.conf.entireScreen;

            // Watermark
            if (cbWatermarkPosition.Items.Count == 0)
            {
                cbWatermarkPosition.Items.AddRange(typeof(WatermarkPositionType).GetDescriptions());
            }
            if (cboWatermarkType.Items.Count == 0)
            {
                cboWatermarkType.Items.AddRange(typeof(WatermarkType).GetDescriptions());
            }
            cboWatermarkType.SelectedIndex = (int)Program.conf.WatermarkMode;
            cbWatermarkPosition.SelectedIndex = (int)Program.conf.WatermarkPositionMode;
            nudWatermarkOffset.Value = Program.conf.WatermarkOffset;
            cbWatermarkAddReflection.Checked = Program.conf.WatermarkAddReflection;
            cbWatermarkAutoHide.Checked = Program.conf.WatermarkAutoHide;

            txtWatermarkText.Text = Program.conf.WatermarkText;
            pbWatermarkFontColor.BackColor = XMLSettings.DeserializeColor(Program.conf.WatermarkFontColor);
            lblWatermarkFont.Text = FontToString();
            nudWatermarkFontTrans.Value = Program.conf.WatermarkFontTrans;
            trackWatermarkFontTrans.Value = (int)Program.conf.WatermarkFontTrans;
            nudWatermarkCornerRadius.Value = Program.conf.WatermarkCornerRadius;
            pbWatermarkGradient1.BackColor = XMLSettings.DeserializeColor(Program.conf.WatermarkGradient1);
            pbWatermarkGradient2.BackColor = XMLSettings.DeserializeColor(Program.conf.WatermarkGradient2);
            pbWatermarkBorderColor.BackColor = XMLSettings.DeserializeColor(Program.conf.WatermarkBorderColor);
            nudWatermarkBackTrans.Value = Program.conf.WatermarkBackTrans;
            trackWatermarkFontTrans.Value = (int)Program.conf.WatermarkBackTrans;
            if (cbWatermarkGradientType.Items.Count == 0)
            {
                cbWatermarkGradientType.Items.AddRange(Enum.GetNames(typeof(LinearGradientMode)));
            }
            cbWatermarkGradientType.SelectedIndex = (int)Program.conf.WatermarkGradientType;

            txtWatermarkImageLocation.Text = Program.conf.WatermarkImageLocation;
            cbWatermarkUseBorder.Checked = Program.conf.WatermarkUseBorder;
            nudWatermarkImageScale.Value = Program.conf.WatermarkImageScale;

            TestWatermark();

            // Quality
            if (cbFileFormat.Items.Count == 0) cbFileFormat.Items.AddRange(Program.zImageFileTypes);
            cbFileFormat.SelectedIndex = Program.conf.FileFormat;
            nudImageQuality.Value = Program.conf.ImageQuality;
            nudSwitchAfter.Value = Program.conf.SwitchAfter;
            if (cbSwitchFormat.Items.Count == 0) cbSwitchFormat.Items.AddRange(Program.zImageFileTypes);
            cbSwitchFormat.SelectedIndex = Program.conf.SwitchFormat;

            #endregion

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
            chkEnableThumbnail.Checked = Program.conf.FTPCreateThumbnail;
            cbAutoSwitchFTP.Checked = Program.conf.AutoSwitchFTP;

            ///////////////////////////////////
            // HTTP Settings
            ///////////////////////////////////

            chkRememberTinyPicUserPass.Checked = Program.conf.RememberTinyPicUserPass;
            txtImageShackRegistrationCode.Text = Program.conf.ImageShackRegistrationCode;
            txtTinyPicShuk.Text = Program.conf.TinyPicShuk;
            nudErrorRetry.Value = Program.conf.ErrorRetryCount;
            cboAutoChangeUploadDestination.Checked = Program.conf.AutoChangeUploadDestination;
            nudUploadDurationLimit.Value = Program.conf.UploadDurationLimit;
            if (cboUploadMode.Items.Count == 0)
            {
                cboUploadMode.Items.AddRange(typeof(UploadMode).GetDescriptions());
            }
            cboUploadMode.SelectedIndex = (int)Program.conf.UploadMode;
            chkImageUploadRetry.Checked = Program.conf.ImageUploadRetry;
            DownloadLanguagesList();
            cbClipboardTranslate.Checked = Program.conf.ClipboardTranslate;
            cbAddFailedScreenshot.Checked = Program.conf.AddFailedScreenshot;
            cbTinyPicSizeCheck.Checked = Program.conf.TinyPicSizeCheck;

            ///////////////////////////////////
            // Image Software Settings
            ///////////////////////////////////

            if (Program.conf.ImageSoftwareActive == null)
            {
                Program.conf.ImageSoftwareActive = new Software
                {
                    Name = "Paint",
                    Path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.System), "mspaint.exe")
                };
            }
            if (Program.conf.ImageSoftwareList.Count == 0)
            {
                Program.conf.ImageSoftwareList.Add(Program.conf.ImageSoftwareActive);
            }
            FindImageEditors();
            if (Program.conf.TextEditors.Count == 0)
            {
                Program.conf.TextEditors.Add(new Software("Notepad", Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.System), "notepad.exe")));
            }
            lbImageSoftware.Items.Clear();
            lbImageSoftware.Items.Add("Disabled");
            foreach (Software app in Program.conf.ImageSoftwareList)
            {
                if (!String.IsNullOrEmpty(app.Name))
                    lbImageSoftware.Items.Add(app.Name);
            }
            if (Program.conf.ImageSoftwareEnabled)
            {
                int i;
                if ((i = lbImageSoftware.Items.IndexOf(Program.conf.ImageSoftwareActive.Name)) != -1)
                    lbImageSoftware.SelectedIndex = i;
            }
            else
            {
                lbImageSoftware.SelectedIndex = 0; //Set to disabled
            }
            txtImageSoftwarePath.Enabled = false;

            ///////////////////////////////////
            // Advanced Settings
            ///////////////////////////////////

            cbStartWin.Checked = CheckStartWithWindows();

            nudCacheSize.Value = Program.conf.ScreenshotCacheSize;
            if (cboUpdateCheckType.Items.Count == 0)
            {
                cboUpdateCheckType.Items.AddRange(typeof(UpdateCheckType).GetDescriptions());
            }
            cboUpdateCheckType.SelectedIndex = (int)Program.conf.UpdateCheckType;
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

            ///////////////////////////////////
            // History
            ///////////////////////////////////

            cbHistorySave.Checked = Program.conf.HistorySave;
            if (cbHistoryListFormat.Items.Count == 0)
            {
                cbHistoryListFormat.Items.AddRange(typeof(HistoryListFormat).GetDescriptions());
            }
            cbHistoryListFormat.SelectedIndex = (int)Program.conf.HistoryListFormat;
            cbShowHistoryTooltip.Checked = Program.conf.HistoryShowTooltips;
            cbHistoryAddSpace.Checked = Program.conf.HistoryAddSpace;
            cbHistoryReverseList.Checked = Program.conf.HistoryReverseList;
            LoadHistoryItems();
            nudHistoryMaxItems.Value = Program.conf.HistoryMaxNumber;

            UpdateGuiControlsHistory();
        }

        private void UpdateGuiControlsPaths()
        {
            Program.InitializeDefaultFolderPaths();
            Program.ConfigureDirs();
            txtImagesDir.Text = Program.conf.ImagesDir;
            txtCacheDir.Text = Program.conf.CacheDir;
            txtSettingsDir.Text = Program.conf.SettingsDir;
        }

        private void UpdateGuiControlsHistory()
        {
            tpHistoryList.Text = string.Format("History List ({0}/{1})", lbHistory.Items.Count, Program.conf.HistoryMaxNumber);
        }

        private bool CheckKeys(HKcombo hkc, IntPtr lParam)
        {
            if (hkc.Mods == null) //0 mods
            {
                if (ModifierKeys == Keys.None && (Keys)Marshal.ReadInt32(lParam) == hkc.Key)
                    return true;
            }
            else // if(hkc.Mods.Length > 0)
            {
                if (hkc.Mods.Length == 1)
                {
                    if (ModifierKeys == hkc.Mods[0] && (Keys)Marshal.ReadInt32(lParam) == hkc.Key)
                        return true;
                }
                else //if (hkc.Mods.Length == 2)
                {
                    if (ModifierKeys == (hkc.Mods[0] | hkc.Mods[1]) && (Keys)Marshal.ReadInt32(lParam) == hkc.Key)
                        return true;
                }
            }

            return false;
        }

        private HKcombo GetHKcombo(IntPtr lParam)
        {
            try
            {
                string[] mods = ModifierKeys.ToString().Split(',');

                if (ModifierKeys == Keys.None)
                {
                    return new HKcombo((Keys)Marshal.ReadInt32(lParam));
                }
                if (mods.Length == 1)
                {
                    return new HKcombo((Keys)Enum.Parse(typeof(Keys), mods[0], true), (Keys)Marshal.ReadInt32(lParam));
                }
                if (mods.Length == 2)
                {
                    return new HKcombo((Keys)Enum.Parse(typeof(Keys), mods[0], true), (Keys)Enum.Parse(typeof(Keys), mods[1], true), (Keys)Marshal.ReadInt32(lParam));
                }
            }
            catch { }

            return null;
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
                        return KeyboardHookHandle;
                    }
                    if (CheckKeys(Program.conf.HKSelectedWindow, lParam))
                    {
                        //Selected Window
                        StartBW_SelectedWindow();
                        return KeyboardHookHandle;
                    }
                    if (CheckKeys(Program.conf.HKCropShot, lParam))
                    {
                        //Crop Shot
                        StartBW_CropShot();
                        return KeyboardHookHandle;
                    }
                    if (CheckKeys(Program.conf.HKLastCropShot, lParam))
                    {
                        //Last Crop Shot
                        StartBW_LastCropShot();
                        return KeyboardHookHandle;
                    }
                    if (CheckKeys(Program.conf.HKEntireScreen, lParam))
                    {
                        //Entire Screen
                        StartBW_EntireScreen();
                        return KeyboardHookHandle;
                    }
                    if (CheckKeys(Program.conf.HKClipboardUpload, lParam))
                    {
                        //Clipboard Upload
                        UploadUsingClipboard();
                        return KeyboardHookHandle;
                    }
                    if (CheckKeys(Program.conf.HKDropWindow, lParam))
                    {
                        //Drop Window
                        ShowDropWindow();
                        return KeyboardHookHandle;
                    }
                    if (CheckKeys(Program.conf.HKActionsToolbar, lParam))
                    {
                        //Quick Actions
                        ShowQuickActions();
                        return KeyboardHookHandle;
                    }
                    if (CheckKeys(Program.conf.HKQuickOptions, lParam))
                    {
                        //Quick Options
                        ShowQuickOptions();
                        return KeyboardHookHandle;
                    }
                    if (CheckKeys(Program.conf.HKLanguageTranslator, lParam))
                    {
                        //Language Translator
                        if (Clipboard.ContainsText())
                        {
                            StartBW_LanguageTranslator(Clipboard.GetText());
                        }
                        return KeyboardHookHandle;
                    }
                    if (CheckKeys(Program.conf.HKScreenColorPicker, lParam))
                    {
                        //Screen Color Picker
                        ScreenColorPicker();
                        return KeyboardHookHandle;
                    }
                }
            }
            return User32.CallNextHookEx(KeyboardHookHandle, nCode, wParam, lParam);
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
            if (cbFromLanguage.Items.Count > 0 && cbToLanguage.Items.Count > 0)
            {
                StartWorkerText(MainAppTask.Jobs.LANGUAGE_TRANSLATOR, clipboard, "");
            }
        }

        private void ScreenColorPicker()
        {
            DialogColor dialogColor = new DialogColor { ScreenPicker = true };
            dialogColor.Show();
        }

        #region "Cache Cleaner Methods"

        private void CleanCache()
        {
            BackgroundWorker bw = new BackgroundWorker();
            bw.DoWork += new System.ComponentModel.DoWorkEventHandler(BwCache_DoWork);
            bw.RunWorkerAsync();
        }

        private void BwCache_DoWork(object sender, DoWorkEventArgs e)
        {
            CacheCleanerTask t = new CacheCleanerTask(Program.conf.CacheDir, Program.conf.ScreenshotCacheSize);
            t.CleanCache();
        }

        #endregion

        #region "Background Worker Safe Methods"

        private void CaptureActiveWindow(ref MainAppTask task)
        {
            try
            {
                task.CaptureActiveWindow();
                WriteImage(task);
                PublishImage(ref task);
            }
            catch (ArgumentOutOfRangeException aor)
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
                    CropOptions co = new CropOptions
                    {
                        MyImage = imgSS,
                        SelectedWindowMode = task.Job == MainAppTask.Jobs.TAKE_SCREENSHOT_WINDOW_SELECTED
                    };
                    Crop c = new Crop(co);
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
                    WriteImage(task);
                    PublishImage(ref task);
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
                task.MyWorker.ReportProgress((int)MainAppTask.ProgressType.UPDATE_CROP_MODE);
                mTakingScreenShot = false;
                if (imgSS != null) imgSS.Dispose();
            }

            return filePath;
        }

        private void CaptureScreen(ref MainAppTask task)
        {
            task.CaptureScreen();
            WriteImage(task);
            PublishImage(ref task);
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

        /// <summary>
        /// Function to edit Image (Screenshot or Picture) in an Image Editor and Upload
        /// </summary>
        /// <param name="task"></param>
        private void PublishImage(ref MainAppTask task)
        {
            TaskManager tm = new TaskManager(ref task);
            if (task.MyImage != null && Program.conf.ImageSoftwareEnabled)
            {
                tm.ImageEdit();
            }

            if (task.SafeToUpload())
            {
                Console.WriteLine("File for HDD: " + task.LocalFilePath);
                tm.UploadImage();
            }
        }

        /// <summary>
        /// Function to edit Text in a Text Editor and Upload
        /// </summary>
        /// <param name="task"></param>
        private void PublishText(ref MainAppTask task)
        {
            TaskManager tm = new TaskManager(ref task);
            tm.UploadText();
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
                        sbMsg.AppendLine(string.Format("Destination: {0} ({1})", t.ImageDestCategory.GetDescription(), t.DestinationName));
                        break;
                    case ImageDestType.CUSTOM_UPLOADER:
                        sbMsg.AppendLine(string.Format("Destination: {0} ({1})", t.ImageDestCategory.GetDescription(), t.DestinationName));
                        break;
                    default:
                        sbMsg.AppendLine(string.Format("Destination: {0}", t.ImageDestCategory.GetDescription()));
                        break;
                }

                string fileOrUrl = "";

                if (t.ImageDestCategory == ImageDestType.CLIPBOARD || t.ImageDestCategory == ImageDestType.FILE)
                {
                    // just local file 
                    if (!string.IsNullOrEmpty(t.FileName.ToString()))
                    {
                        sbMsg.AppendLine("Name: " + t.FileName);
                    }
                    fileOrUrl = string.Format("{0}: {1}", t.ImageDestCategory.GetDescription(), t.LocalFilePath);
                }
                else
                {
                    // remote file
                    if (!string.IsNullOrEmpty(t.RemoteFilePath))
                    {
                        if (!string.IsNullOrEmpty(t.FileName.ToString()))
                        {
                            sbMsg.AppendLine("Name: " + t.FileName);
                        }
                        fileOrUrl = string.Format("URL: {0}", t.RemoteFilePath);

                        if (string.IsNullOrEmpty(t.RemoteFilePath) && t.Errors.Count > 0)
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
                            fileOrUrl = "Warning: " + t.Errors[t.Errors.Count - 1];
                        }
                    }
                }

                if (!string.IsNullOrEmpty(fileOrUrl))
                {
                    sbMsg.AppendLine(fileOrUrl);
                }

                if (Program.conf.ShowUploadDuration && t.UploadDuration > 0)
                {
                    sbMsg.AppendLine("Upload duration: " + t.UploadDuration + " ms");
                }
            }

            niTray.ShowBalloonTip(1000, Application.ProductName, sbMsg.ToString(), tti);

            return sbMsg.ToString();
        }

        private void WriteImage(MainAppTask t)
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
                    case MainAppTask.Jobs.CUSTOM_UPLOADER_TEST:
                        t.SetLocalFilePath(this.GetFilePath(MainAppTask.Jobs.TAKE_SCREENSHOT_WINDOW_ACTIVE));
                        break;
                }
                // Update LocalFilePath again, due to possible PNG to JPG changes
                t.SetLocalFilePath(FileSystem.SaveImage(t.MyImage, t.LocalFilePath));
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
                QuickOptions quickOptions = new QuickOptions { Icon = Resources.zss_main };
                quickOptions.FormClosed += new FormClosedEventHandler(QuickOptionsFormClosed);
                quickOptions.ApplySettings += new EventHandler(QuickOptionsApplySettings);
                quickOptions.Show();
                Rectangle taskbar = User32.GetTaskbarRectangle();
                quickOptions.Location = new Point(SystemInformation.PrimaryMonitorSize.Width - quickOptions.Width - 100,
                    SystemInformation.PrimaryMonitorSize.Height - taskbar.Height - quickOptions.Height - 10);
            }
        }

        private void QuickOptionsApplySettings(object sender, EventArgs e)
        {
            cboScreenshotDest.SelectedIndex = (int)Program.conf.ScreenshotDestMode;
            cboClipboardTextMode.SelectedIndex = (int)Program.conf.ClipboardUriMode;
        }

        private void QuickOptionsFormClosed(object sender, FormClosedEventArgs e)
        {
            bQuickOptionsOpened = false;
        }

        private BackgroundWorker CreateWorker()
        {
            BackgroundWorker bwApp = new BackgroundWorker { WorkerReportsProgress = true };
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
                task.TextDestCategory = Program.conf.TextDestMode;
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
        /// <returns></returns>
        private bool StartWorkerText(MainAppTask.Jobs job, string txt, string localFilePath)
        {
            MainAppTask t = CreateTask(job);
            t.JobCategory = JobCategoryType.TEXT;
            t.SetLocalFilePath(localFilePath);

            switch (job)
            {
                case MainAppTask.Jobs.LANGUAGE_TRANSLATOR:
                    btnTranslate.Enabled = false;

                    if (txt == "")
                    {
                        txt = txtTranslateText.Text;
                    }
                    t.TranslationInfo = new GoogleTranslate.TranslationInfo(txt, mGTranslator.LanguageOptions.SourceLangList[cbFromLanguage.SelectedIndex],
                        mGTranslator.LanguageOptions.TargetLangList[cbToLanguage.SelectedIndex]);
                    if (t.TranslationInfo.IsEmpty())
                    {
                        btnTranslate.Enabled = true;
                        return false;
                    }
                    break;
            }

            t.MyWorker.RunWorkerAsync(t);
            return true;
        }

        #endregion

        #region "Event Handlers"

        private void BwApp_DoWork(object sender, DoWorkEventArgs e)
        {
            MainAppTask task = (MainAppTask)e.Argument;
            task.MyWorker.ReportProgress((int)MainAppTask.ProgressType.SET_ICON_BUSY, task);
            ClipboardManager.Queue();

            if (Program.conf.PromptforUpload && task.ImageDestCategory != ImageDestType.CLIPBOARD &
                task.ImageDestCategory != ImageDestType.FILE &&
                (task.Job == MainAppTask.Jobs.TAKE_SCREENSHOT_SCREEN ||
                task.Job == MainAppTask.Jobs.TAKE_SCREENSHOT_WINDOW_ACTIVE) &&
                MessageBox.Show("Are you really want upload to " + task.ImageDestCategory.GetDescription() + " ?",
                "ZScreen", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
            {
                e.Result = task;
                return;
            }

            if ((task.Job == MainAppTask.Jobs.PROCESS_DRAG_N_DROP || task.Job == MainAppTask.Jobs.UPLOAD_FROM_CLIPBOARD) &&
                task.ImageDestCategory != ImageDestType.FTP && !task.IsValidImage())
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

            if (task.JobCategory == JobCategoryType.SCREENSHOTS)
            {
                if (Program.conf.ScreenshotDelay != 0)
                {
                    Thread.Sleep((int)(Program.conf.ScreenshotDelay));
                }
            }

            FileSystem.AppendDebug(".");
            FileSystem.AppendDebug(string.Format("Job started: {0}", task.Job));

            switch (task.JobCategory)
            {
                case JobCategoryType.PICTURES:
                    PublishImage(ref task);
                    break;
                case JobCategoryType.SCREENSHOTS:
                    switch (task.Job)
                    {
                        case MainAppTask.Jobs.TAKE_SCREENSHOT_SCREEN:
                            CaptureScreen(ref task);
                            break;
                        case MainAppTask.Jobs.TAKE_SCREENSHOT_WINDOW_SELECTED:
                        case MainAppTask.Jobs.TAKE_SCREENSHOT_CROPPED:
                        case MainAppTask.Jobs.TAKE_SCREENSHOT_LAST_CROPPED:
                            CaptureRegionOrWindow(ref task);
                            break;
                        case MainAppTask.Jobs.CUSTOM_UPLOADER_TEST:
                        case MainAppTask.Jobs.TAKE_SCREENSHOT_WINDOW_ACTIVE:
                            CaptureActiveWindow(ref task);
                            break;
                        case MainAppTask.Jobs.UPLOAD_IMAGE:
                            PublishImage(ref task);
                            break;
                    }
                    break;
                case JobCategoryType.TEXT:
                    switch (task.Job)
                    {
                        case MainAppTask.Jobs.UPLOAD_FROM_CLIPBOARD:
                            PublishText(ref task);
                            break;
                        case MainAppTask.Jobs.LANGUAGE_TRANSLATOR:
                            LanguageTranslator(ref task);
                            break;
                    }
                    break;
            }

            if (!string.IsNullOrEmpty(task.LocalFilePath))
            {
                if (Program.conf.AddFailedScreenshot ||
                    (!Program.conf.AddFailedScreenshot && task.Errors.Count == 0 ||
                    task.JobCategory == JobCategoryType.TEXT))
                {
                    task.MyWorker.ReportProgress((int)MainAppTask.ProgressType.ADD_FILE_TO_LISTBOX, new HistoryItem(task));
                }
            }

            e.Result = task;
        }

        private void BwApp_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            switch ((MainAppTask.ProgressType)e.ProgressPercentage)
            {
                case MainAppTask.ProgressType.ADD_FILE_TO_LISTBOX:
                    AddHistoryItem((HistoryItem)e.UserState);
                    break;
                case MainAppTask.ProgressType.COPY_TO_CLIPBOARD_IMAGE:
                    if (e.UserState.GetType() == typeof(string))
                    {
                        string f = e.UserState.ToString();
                        if (File.Exists(f))
                        {
                            SaveImageToClipboard(f);
                            FileSystem.AppendDebug(string.Format("Saved {0} as an Image to Clipboard...", f));
                        }
                    }
                    else if (e.UserState.GetType() == typeof(Bitmap))
                    {
                        Clipboard.SetImage((Image)e.UserState);
                    }
                    break;
                case MainAppTask.ProgressType.FLASH_ICON:
                    niTray.Icon = (Icon)e.UserState;
                    break;
                case MainAppTask.ProgressType.SET_ICON_BUSY:
                    MainAppTask task = (MainAppTask)e.UserState;
                    niTray.Text = this.Text + " - " + task.Job.GetDescription();
                    niTray.Icon = Resources.zss_busy;
                    break;
                case MainAppTask.ProgressType.UPDATE_CROP_MODE:
                    cboCropGridMode.Checked = Program.conf.CropGridToggle;
                    break;
                case MainAppTask.ProgressType.UPDATE_UPLOAD_DESTINATION:
                    cboScreenshotDest.SelectedIndex = (int)Program.conf.ScreenshotDestMode;
                    break;
            }
        }

        private void BwApp_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                MainAppTask task = (MainAppTask)e.Result;

                FileSystem.AppendDebug(string.Format("Job completed: {0}", task.Job));

                if (!RetryUpload(task))
                {
                    switch (task.JobCategory)
                    {
                        case JobCategoryType.TEXT:
                            switch (task.Job)
                            {
                                case MainAppTask.Jobs.LANGUAGE_TRANSLATOR:
                                    txtTranslateText.Text = task.TranslationInfo.SourceText;
                                    txtTranslateResult.Text = task.TranslationInfo.Result.TranslatedText;
                                    txtLanguages.Text = task.TranslationInfo.Result.TranslationType;
                                    txtDictionary.Text = task.TranslationInfo.Result.Dictionary;
                                    if (Program.conf.ClipboardTranslate)
                                    {
                                        Clipboard.SetText(task.TranslationInfo.Result.TranslatedText);
                                    }
                                    btnTranslate.Enabled = true;
                                    break;
                                case MainAppTask.Jobs.UPLOAD_FROM_CLIPBOARD:
                                    if (!string.IsNullOrEmpty(task.RemoteFilePath))
                                    {
                                        Clipboard.SetText(task.RemoteFilePath);
                                    }
                                    break;
                            }
                            break;
                        case JobCategoryType.SCREENSHOTS:
                            switch (task.Job)
                            {
                                case MainAppTask.Jobs.CUSTOM_UPLOADER_TEST:
                                    if (task.ImageManager != null && task.ImageManager.FileCount > 0)
                                    {
                                        if (task.ImageManager.GetFullImageUrl() != "")
                                        {
                                            txtUploadersLog.AppendText(task.DestinationName + " full image: " +
                                                task.ImageManager.GetFullImageUrl() + "\r\n");
                                        }
                                        if (task.ImageManager.GetThumbnailUrl() != "")
                                        {
                                            txtUploadersLog.AppendText(task.DestinationName + " thumbnail: " +
                                                task.ImageManager.GetThumbnailUrl() + "\r\n");
                                        }
                                    }
                                    btnUploadersTest.Enabled = true;
                                    break;
                            }
                            if (Program.conf.DeleteLocal)
                            {
                                if (File.Exists(task.LocalFilePath))
                                {
                                    File.Delete(task.LocalFilePath);
                                }
                            }
                            break;
                    }

                    if (task.JobCategory == JobCategoryType.SCREENSHOTS || task.JobCategory == JobCategoryType.PICTURES)
                    {
                        ClipboardManager.AddScreenshotList(task.ImageManager);
                        ClipboardManager.SetClipboardText();
                    }

                    if (task.ImageManager != null && !string.IsNullOrEmpty(task.ImageManager.Source))
                    {
                        btnOpenSourceText.Enabled = true;
                        btnOpenSourceBrowser.Enabled = true;
                        btnOpenSourceString.Enabled = true;
                    }

                    niTray.Text = this.Text;
                    if (ClipboardManager.Workers > 1)
                    {
                        niTray.Icon = Resources.zss_busy;
                    }
                    else
                    {
                        niTray.Icon = Resources.zss_tray;
                    }

                    if (task.Job == MainAppTask.Jobs.LANGUAGE_TRANSLATOR || File.Exists(task.LocalFilePath))
                    {
                        if (Program.conf.CompleteSound)
                        {
                            System.Media.SystemSounds.Exclamation.Play();
                        }
                        if (Program.conf.ShowBalloonTip)
                        {
                            ShowBalloonTip(task);
                        }
                    }

                    if (task.Errors.Count > 0)
                    {
                        Console.WriteLine(task.Errors[task.Errors.Count - 1]);
                    }
                }

                if (task.MyImage != null) task.MyImage.Dispose(); // For fix memory leak

                UpdateGuiControlsHistory();

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
            if (Program.conf.ImageUploadRetry && t.IsImage && t.Errors.Count > 0 && !t.Retry &&
                (t.ImageDestCategory == ImageDestType.IMAGESHACK || t.ImageDestCategory == ImageDestType.TINYPIC))
            {
                MainAppTask task = CreateTask(MainAppTask.Jobs.UPLOAD_IMAGE);
                task.JobCategory = t.JobCategory;
                task.SetImage(t.LocalFilePath);
                task.SetLocalFilePath(t.LocalFilePath);
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
            return false;
        }

        private void LoadHistoryItems()
        {
            lbHistory.Items.Clear();
            HistoryManager history = HistoryManager.Read();
            for (int i = 0; i < history.HistoryItems.Count && i < Program.conf.HistoryMaxNumber; i++)
            {
                lbHistory.Items.Add(history.HistoryItems[i]);
            }
            if (lbHistory.Items.Count > 0)
            {
                lbHistory.SelectedIndex = 0;
            }
        }

        private void AddHistoryItem(HistoryItem hi)
        {
            lbHistory.Items.Insert(0, hi);
            CheckHistoryItems();
            SaveHistoryItems();
            if (lbHistory.Items.Count > 0)
            {
                lbHistory.ClearSelected();
                lbHistory.SelectedIndex = 0;
            }
        }

        private void CheckHistoryItems()
        {
            if (lbHistory.Items.Count > Program.conf.HistoryMaxNumber)
            {
                for (int i = lbHistory.Items.Count - 1; i >= Program.conf.HistoryMaxNumber; i--)
                {
                    lbHistory.Items.RemoveAt(i);
                }
            }
        }

        private void SaveHistoryItems()
        {
            if (Program.conf.HistorySave)
            {
                List<HistoryItem> historyItems = new List<HistoryItem>();
                foreach (HistoryItem item in lbHistory.Items)
                {
                    historyItems.Add(item);
                }
                HistoryManager hm = new HistoryManager(historyItems);
                hm.Save();
            }
        }

        private void exitZScreenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            mClose = true;
            Close();
        }

        private void cbRegionRectangleInfo_CheckedChanged(object sender, EventArgs e)
        {
            Program.conf.CropRegionRectangleInfo = cbRegionRectangleInfo.Checked;
        }

        private void cbRegionHotkeyInfo_CheckedChanged(object sender, EventArgs e)
        {
            Program.conf.CropRegionHotkeyInfo = cbRegionHotkeyInfo.Checked;
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
            Program.conf.ErrorRetryCount = nudErrorRetry.Value;
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
                    Program.conf.WindowSize = this.Size;
                    this.ShowInTaskbar = Program.conf.ShowInTaskbar;
                }
            }
        }

        private void ZScreen_FormClosing(object sender, FormClosingEventArgs e)
        {
            /* 
             * Sometimes Settings.xml write delays cause a small pause when user press the close button
             * Noticing this is avoided by this.WindowState = FormWindowState.Minimized; 
            */
            this.WindowState = FormWindowState.Minimized;
            SaveSettings();
            if (!mClose && e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                Hide();
            }
            FileSystem.AppendDebug("Closed " + Application.ProductName + "\n");
        }

        #endregion

        private void SaveSettings()
        {
            Program.conf.Save();
            SaveHistoryItems();
            Settings.Default.Save();
        }

        private void RewriteIsRightClickMenu()
        {
            if (Program.conf.ImageSoftwareList != null)
            {
                tsmEditinImageSoftware.DropDownDirection = ToolStripDropDownDirection.Right;

                tsmEditinImageSoftware.DropDownItems.Clear();

                List<Software> imgs = Program.conf.ImageSoftwareList;

                //tsm.TextDirection = ToolStripTextDirection.Horizontal;
                tsmEditinImageSoftware.DropDownDirection = ToolStripDropDownDirection.Right;

                ToolStripMenuItem tsm = new ToolStripMenuItem { Text = "Disabled", CheckOnClick = true };
                tsm.Click += DisableImageSoftwareClick;

                tsmEditinImageSoftware.DropDownItems.Add(tsm);

                tsmEditinImageSoftware.DropDownItems.Add(new ToolStripSeparator());

                for (int x = 0; x < imgs.Count; x++)
                {
                    tsm = new ToolStripMenuItem { Text = imgs[x].Name, CheckOnClick = true };
                    //tsm.Tag = x;
                    tsm.Click += new EventHandler(RightClickIsItemClick);
                    tsmEditinImageSoftware.DropDownItems.Add(tsm);
                }

                //check the active ftpUpload account

                if (Program.conf.ImageSoftwareEnabled)
                    CheckCorrectIsRightClickMenu(Program.conf.ImageSoftwareActive.Name);
                else
                    CheckCorrectIsRightClickMenu(tsmEditinImageSoftware.DropDownItems[0].Text);

                tsmEditinImageSoftware.DropDownDirection = ToolStripDropDownDirection.Right;

                //show drop down menu in the correct place if menu is selected
                if (tsmEditinImageSoftware.Selected)
                {
                    tsmEditinImageSoftware.DropDown.Hide();
                    tsmEditinImageSoftware.DropDown.Show();
                }
            }
        }

        private void DisableImageSoftwareClick(object sender, EventArgs e)
        {
            //cbRunImageSoftware.Checked = false;

            //select "Disabled"
            lbImageSoftware.SelectedIndex = 0;

            CheckCorrectIsRightClickMenu(tsmEditinImageSoftware.DropDownItems[0].Text); //disabled
            //rewriteISRightClickMenu();
        }

        private void RightClickIsItemClick(object sender, EventArgs e)
        {
            ToolStripMenuItem tsm = (ToolStripMenuItem)sender;

            Program.conf.ImageSoftwareActive = GetImageSoftware(tsm.Text); //Program.conf.ImageSoftwareList[(int)tsm.Tag];

            if (lbImageSoftware.Items.IndexOf(tsm.Text) >= 0)
                lbImageSoftware.SelectedItem = tsm.Text;

            //Turn on Image Software
            //cbRunImageSoftware.Checked = true;

            //rewriteISRightClickMenu();
        }

        private void CheckCorrectIsRightClickMenu(string txt)
        {
            ToolStripMenuItem tsm;

            for (int x = 0; x < tsmEditinImageSoftware.DropDownItems.Count; x++)
            {
                //if (tsmImageSoftware.DropDownItems[x].GetType() == typeof(ToolStripMenuItem))
                if (tsmEditinImageSoftware.DropDownItems[x] is ToolStripMenuItem)
                {
                    tsm = (ToolStripMenuItem)tsmEditinImageSoftware.DropDownItems[x];

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
                    tsm = new ToolStripMenuItem { CheckOnClick = true, Tag = i, Text = lUploaders[i].Name };
                    tsm.Click += rightClickIHS_Click;
                    tsmDestCustomHTTP.DropDownItems.Add(tsm);
                }

                CheckCorrectMenuItemClicked(ref tsmDestCustomHTTP, Program.conf.ImageUploaderSelected);

                tsmDestCustomHTTP.DropDownDirection = ToolStripDropDownDirection.Right;

                //show drop down menu in the correct place if menu is selected
                if (tsmDestCustomHTTP.Selected)
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
                tsm = new ToolStripMenuItem { Tag = x++, Text = cui.GetDescription() };
                tsm.Click += clipboardCopyHistory_Click;
                tsmCopyCbHistory.DropDownItems.Add(tsm);
            }
        }

        private void clipboardCopyHistory_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem tsm = (ToolStripMenuItem)sender;
            SetClipboardFromHistory((ClipboardUriType)tsm.Tag);
        }

        private void FillClipboardMenu()
        {
            tsmCopytoClipboardMode.DropDownDirection = ToolStripDropDownDirection.Right;
            tsmCopytoClipboardMode.DropDownItems.Clear();

            ToolStripMenuItem tsm;
            int x = 0;
            foreach (ClipboardUriType cui in Enum.GetValues(typeof(ClipboardUriType)))
            {
                tsm = new ToolStripMenuItem { Tag = x++, CheckOnClick = true, Text = cui.GetDescription() };
                tsm.Click += new EventHandler(ClipboardModeClick);
                tsmCopytoClipboardMode.DropDownItems.Add(tsm);
            }

            CheckCorrectMenuItemClicked(ref tsmCopytoClipboardMode, (int)Program.conf.ClipboardUriMode);
            tsmCopytoClipboardMode.DropDownDirection = ToolStripDropDownDirection.Right;
        }

        private void ClipboardModeClick(object sender, EventArgs e)
        {
            ToolStripMenuItem tsm = (ToolStripMenuItem)sender;
            Program.conf.ClipboardUriMode = (ClipboardUriType)tsm.Tag;
            CheckCorrectMenuItemClicked(ref tsmCopytoClipboardMode, (int)Program.conf.ClipboardUriMode);
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
                    tsm = new ToolStripMenuItem { Tag = x, CheckOnClick = true, Text = accs[x].Name };
                    tsm.Click += rightClickFTPItem_Click;
                    tsmDestFTP.DropDownItems.Add(tsm);
                }

                //check the active ftpUpload account
                CheckCorrectMenuItemClicked(ref tsmDestFTP, Program.conf.FTPselected);
                tsmDestFTP.DropDownDirection = ToolStripDropDownDirection.Right;

                //show drop down menu in the correct place if menu is selected
                if (tsmDestFTP.Selected)
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
            if (regkey != null && (string)regkey.GetValue(Application.ProductName,
                "null", RegistryValueOptions.None) != "null")
            {
                Registry.CurrentUser.Flush();
                return true;
            }
            Registry.CurrentUser.Flush();
            return false;
        }

        private void ShowLicense()
        {
            string lic = FileSystem.GetTextFromFile(Path.Combine(Application.StartupPath, "License.txt"));
            lic = lic != string.Empty ? lic : FileSystem.GetText("License.txt");
            if (lic != string.Empty)
            {
                frmTextViewer v = new frmTextViewer(string.Format("{0} - {1}",
                    Application.ProductName, "License"), lic) { Icon = this.Icon };
                v.ShowDialog();
            }
        }

        private void ShowVersionHistory()
        {
            string h = FileSystem.GetTextFromFile(Path.Combine(Application.StartupPath, "VersionHistory.txt"));
            if (h == string.Empty)
            {
                h = FileSystem.GetText("VersionHistory.txt");
            }
            if (h != string.Empty)
            {
                frmTextViewer v = new frmTextViewer(string.Format("{0} - {1}",
                    Application.ProductName, "Version History"), h) { Icon = this.Icon };
                v.ShowDialog();
            }
        }

        private void FindImageEditors()
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
                            Program.conf.ImageSoftwareList.Add(new Software(sName, filePath));
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
            foreach (Software iS in Program.conf.ImageSoftwareList)
            {
                if (iS.Name == sName) return true;
            }
            return false;
        }

        private bool SoftwareRemove(string sName)
        {
            if (SoftwareExist(sName))
            {
                foreach (Software iS in Program.conf.ImageSoftwareList)
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
            Program.conf.ImagesDir = BrowseDirectory(ref txtImagesDir);
        }

        private string BrowseDirectory(ref TextBox textBoxDirectory)
        {
            string settingDir = textBoxDirectory.Text;
            FolderBrowserDialog dlg = new FolderBrowserDialog
            {
                SelectedPath = textBoxDirectory.Text,
                ShowNewFolderButton = true
            };
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

        private void btnUpdateImageSoftware_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtImageSoftwareName.Text) &&
                GetImageSoftware(txtImageSoftwareName.Text) == null &&
                !string.IsNullOrEmpty(txtImageSoftwarePath.Text))
            {
                int sel;
                bool isActiveImageSoftware = false;

                if ((sel = lbImageSoftware.SelectedIndex) > 0)
                {
                    if (Program.conf.ImageSoftwareActive.Name == (string)lbImageSoftware.SelectedItem)
                        isActiveImageSoftware = true;
                    Software temp = new Software { Name = txtImageSoftwareName.Text, Path = txtImageSoftwarePath.Text };
                    Program.conf.ImageSoftwareList[sel - 1] = temp;
                    lbImageSoftware.Items[sel] = temp.Name;

                    if (isActiveImageSoftware)
                    {
                        if (Program.conf.ImageSoftwareEnabled)
                        {
                            Program.conf.ImageSoftwareActive = temp;
                            CheckCorrectIsRightClickMenu(temp.Name);
                        }
                    }
                }
            }

            RewriteIsRightClickMenu();
        }

        private void btnBrowseImageSoftware_Click(object sender, EventArgs e)
        {
            BrowseImageSoftware();
        }

        private void BrowseImageSoftware()
        {
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

        private void ShowDirectory(string dir)
        {
            Process.Start("explorer.exe", dir);
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
            this.BringToFront();
        }

        private void tsm_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem tsm = (ToolStripMenuItem)sender;

            TabPage sel = tpMain;

            if (tsm == tsmHotkeys)
                sel = tpHotkeys;
            else if (tsm == tsmCapture)
                sel = tpCapture;
            else if (tsm == tsmWatermark)
                sel = tpWatermark;
            else if (tsm == tsmEditors)
                sel = tpEditors;
            else if (tsm == tsmFTP)
                sel = tpFTP;
            else if (tsm == tsmHTTP)
                sel = tpHTTP;
            else if (tsm == tsmHistory)
                sel = tpHistory;
            else if (tsm == tsmOptions)
                sel = tpOptions;

            tcApp.SelectedTab = sel;

            BringUpMenu();
            tcApp.Focus();
        }

        private void sShowAbout()
        {
            AboutBox ab = new AboutBox();
            ab.ShowDialog();
        }

        private void btnBrowseConfig_Click(object sender, EventArgs e)
        {
            ShowDirectory(Program.conf.SettingsDir);
        }

        private void tsmLic_Click(object sender, EventArgs e)
        {
            ShowLicense();
        }

        private void chkManualNaming_CheckedChanged(object sender, EventArgs e)
        {
            Program.conf.ManualNaming = chkManualNaming.Checked;
        }

        private void ZScreen_Shown(object sender, EventArgs e)
        {
            mGuiIsReady = true;

            // Show settings if never ran before
            if (!Program.conf.RunOnce)
            {
                Show();
                WindowState = FormWindowState.Normal;
                this.Activate();
                this.BringToFront();
                lblFirstRun.Visible = true;
                Program.conf.RunOnce = true;
            }

            if (Program.conf.FTPSettingsBackup)
            {
                FileSystem.BackupFTPSettings();
            }
        }

        private void AddToClipboardByDoubleClick(Control tp)
        {
            Control ctl = tp.GetNextControl(tp, true);
            while (ctl != null)
            {
                if (ctl.GetType() == typeof(TextBox))
                {
                    ctl.DoubleClick += TextBox_DoubleClick;
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
            if (regkey != null)
            {
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
        }

        private void nudFlashIconCount_ValueChanged(object sender, EventArgs e)
        {
            Program.conf.FlashTrayCount = nudFlashIconCount.Value;
        }

        private void nudCacheSize_ValueChanged(object sender, EventArgs e)
        {
            Program.conf.ScreenshotCacheSize = nudCacheSize.Value;
        }

        private void txtCacheDir_TextChanged(object sender, EventArgs e)
        {
            Program.conf.CacheDir = txtCacheDir.Text;
        }

        private void btnSettingsExport_Click(object sender, EventArgs e)
        {
            SaveFileDialog dlg = new SaveFileDialog { Filter = Program.FILTER_SETTINGS };
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                Program.conf.Save(dlg.FileName);
            }
        }

        private void btnSettingsImport_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog { Filter = Program.FILTER_SETTINGS };
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                XMLSettings temp = XMLSettings.Read(dlg.FileName);
                if (temp.RunOnce)
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
                if (GetImageSoftware(txtImageSoftwareName.Text) == null)
                {
                    if (!string.IsNullOrEmpty(txtImageSoftwarePath.Text))
                    {
                        Software temp = new Software
                        {
                            Name = txtImageSoftwareName.Text,
                            Path = txtImageSoftwarePath.Text
                        };
                        Program.conf.ImageSoftwareList.Add(temp);
                        lbImageSoftware.Items.Add(temp.Name);
                        lbImageSoftware.SelectedItem = temp.Name;
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

                Software temp = GetImageSoftware(lbImageSoftware.SelectedItem.ToString());
                if (temp != null)
                {
                    Program.conf.ImageSoftwareList.Remove(temp);
                    lbImageSoftware.Items.Remove(lbImageSoftware.SelectedItem.ToString());
                }

                if (wasActiveSetLower != -1)
                {
                    lbImageSoftware.SelectedIndex = wasActiveSetLower;
                }

                RewriteIsRightClickMenu();
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
            CheckToolStripMenuItem(tsmSendImageTo, item);
        }

        private void CheckToolStripMenuItem(ToolStripDropDownItem parent, ToolStripMenuItem item)
        {
            foreach (ToolStripMenuItem tsmi in parent.DropDownItems)
            {
                tsmi.Checked = tsmi == item;
            }

            tsmCopytoClipboardMode.Enabled = cboScreenshotDest.SelectedIndex != (int)ImageDestType.CLIPBOARD &&
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
            int sel;

            if ((sel = lbImageSoftware.SelectedIndex) > 0)
            {
                Program.conf.ImageSoftwareEnabled = true;

                Program.conf.ImageSoftwareActive = Program.conf.ImageSoftwareList[sel - 1];
                RewriteIsRightClickMenu();
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

                Program.conf.ImageSoftwareEnabled = false;
                RewriteIsRightClickMenu();
            }
            else if (b)
            {
                Software temp = GetImageSoftware(lbImageSoftware.SelectedItem.ToString());
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

            foreach (ToolStripMenuItem tsmi in tsmCopytoClipboardMode.DropDownItems)
            {
                tsmi.Checked = false;
            }
            CheckCorrectMenuItemClicked(ref tsmCopytoClipboardMode, (int)Program.conf.ClipboardUriMode);
        }

        private void txtFileDirectory_TextChanged(object sender, EventArgs e)
        {
            Program.conf.ImagesDir = txtImagesDir.Text;
        }

        private void cbDeleteLocal_CheckedChanged(object sender, EventArgs e)
        {
            Program.conf.DeleteLocal = cbDeleteLocal.Checked;
        }

        private void txtActiveWindow_TextChanged(object sender, EventArgs e)
        {
            Program.conf.activeWindow = txtActiveWindow.Text;
            lblActiveWindowPreview.Text = NameParser.Convert(NameParser.NameType.ActiveWindow, true);
        }

        private void txtEntireScreen_TextChanged(object sender, EventArgs e)
        {
            Program.conf.entireScreen = txtEntireScreen.Text;
            lblEntireScreenPreview.Text = NameParser.Convert(NameParser.NameType.EntireScreen, true);
        }

        private void cmbFileFormat_SelectedIndexChanged(object sender, EventArgs e)
        {
            Program.conf.FileFormat = cbFileFormat.SelectedIndex;
        }

        private void txtImageQuality_ValueChanged(object sender, EventArgs e)
        {
            Program.conf.ImageQuality = (int)nudImageQuality.Value;
        }

        private void cmbSwitchFormat_SelectedIndexChanged(object sender, EventArgs e)
        {
            Program.conf.SwitchFormat = cbSwitchFormat.SelectedIndex;
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

        private void ScreenshotUsingDragDrop(string fp)
        {
            StartWorkerImages(MainAppTask.Jobs.PROCESS_DRAG_N_DROP, fp);
        }

        private void ScreenshotUsingDragDrop(string[] paths)
        {
            foreach (string filePath in FileSystem.GetExplorerFileList(paths))
            {
                File.Copy(filePath, FileSystem.GetUniqueFilePath(Path.Combine(
                    Program.conf.ImagesDir, Path.GetFileName(filePath))), true);
                ScreenshotUsingDragDrop(filePath);
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

        private void UploadUsingClipboard()
        {
            foreach (string filePath in GetClipboardFilePaths())
            {
                if (FileSystem.IsValidTextFile(filePath))
                {
                    StartWorkerText(MainAppTask.Jobs.UPLOAD_FROM_CLIPBOARD, "", filePath);
                }
                else
                {
                    StartWorkerImages(MainAppTask.Jobs.UPLOAD_FROM_CLIPBOARD, filePath);
                }
            }
        }

        private List<string> GetClipboardFilePaths()
        {
            List<string> cbListFilePath = new List<string>();

            try
            {
                string cbFilePath;

                if (Clipboard.ContainsImage())
                {
                    Image cImage = Clipboard.GetImage();
                    cbFilePath = FileSystem.GetFilePath(NameParser.Convert(NameParser.NameType.EntireScreen), false);
                    cbFilePath = FileSystem.SaveImage(cImage, cbFilePath);
                    cbListFilePath.Add(cbFilePath);
                }
                else if (Clipboard.ContainsText())
                {
                    cbFilePath = FileSystem.GetUniqueFilePath(Path.Combine(Program.conf.TextDir,
                        NameParser.Convert("%y.%mo.%d-%h.%mi.%s") + ".txt"));
                    File.WriteAllText(cbFilePath, Clipboard.GetText());
                    cbListFilePath.Add(cbFilePath);
                }
                else if (Clipboard.ContainsFileDropList())
                {
                    foreach (string fp in FileSystem.GetExplorerFileList(Clipboard.GetFileDropList()))
                    {
                        cbFilePath = FileSystem.GetUniqueFilePath(Path.Combine(MyGraphics.IsValidImage(fp) ? Program.ImagesDir : Program.FilesDir, Path.GetFileName(fp)));
                        File.Copy(fp, cbFilePath, true);
                        cbListFilePath.Add(cbFilePath);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
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
            if (this.IsHandleCreated)
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
                                        cbString = t.LocalFilePath;
                                        if (!string.IsNullOrEmpty(cbString))
                                        {
                                            Process.Start(cbString);
                                        }
                                        break;
                                    default:
                                        cbString = t.RemoteFilePath;
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
            Program.conf.ScreenshotDelay = nudScreenshotDelay.Value;
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
                iUploader.Arguments.Add(new[] { lvItem.Text, lvItem.SubItems[1].Text });
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
                StartWorkerScreenshots(MainAppTask.Jobs.CUSTOM_UPLOADER_TEST);
            }
        }

        private void btnUploaderExport_Click(object sender, EventArgs e)
        {
            if (Program.conf.ImageUploadersList != null)
            {
                SaveFileDialog dlg = new SaveFileDialog
                {
                    FileName = string.Format("{0}-{1}-uploaders", Application.ProductName, DateTime.Now.ToString("yyyyMMdd")),
                    Filter = Program.FILTER_IMAGE_HOSTING_SERVICES
                };
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    ImageHostingServiceManager ihsm = new ImageHostingServiceManager
                    {
                        ImageHostingServices = Program.conf.ImageUploadersList
                    };
                    ihsm.Save(dlg.FileName);
                }
            }
        }

        private void ImportImageUploaders(string fp)
        {
            ImageHostingServiceManager tmp = ImageHostingServiceManager.Read(fp);
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

            OpenFileDialog dlg = new OpenFileDialog { Filter = Program.FILTER_IMAGE_HOSTING_SERVICES };
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

        private void OpenLastSource(ImageFileManager.SourceType sType)
        {
            OpenSource(ClipboardManager.GetLastImageUpload(), sType);
        }

        private bool OpenSource(ImageFileManager ifm, ImageFileManager.SourceType sType)
        {
            string path = ifm.GetSource(Program.conf.TempDir, sType);
            if (!string.IsNullOrEmpty(path))
            {
                if (sType == ImageFileManager.SourceType.TEXT || sType == ImageFileManager.SourceType.HTML)
                {
                    Process.Start(path);
                    return true;
                }
                if (sType == ImageFileManager.SourceType.STRING)
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
            dgvHotkeys.Rows.Add(new object[] { "Actions Toolbar", Program.conf.HKActionsToolbar });
            dgvHotkeys.Rows.Add(new object[] { "Quick Options", Program.conf.HKQuickOptions });
            dgvHotkeys.Rows.Add(new object[] { "Drop Window", Program.conf.HKDropWindow });
            dgvHotkeys.Rows.Add(new object[] { "Language Translator", Program.conf.HKLanguageTranslator });
            dgvHotkeys.Rows.Add(new object[] { "Screen Color Picker", Program.conf.HKScreenColorPicker });

            dgvHotkeys.Refresh();
        }

        private void SetHotkey(int row, HKcombo hkc)
        {
            switch (row)
            {
                case 0: //Active Window
                    Program.conf.HKActiveWindow = hkc;
                    break;
                case 1: //Selected Window
                    Program.conf.HKSelectedWindow = hkc;
                    break;
                case 2: //Entire Screen
                    Program.conf.HKEntireScreen = hkc;
                    break;
                case 3: //Crop Shot
                    Program.conf.HKCropShot = hkc;
                    break;
                case 4: //Last Crop Shot
                    Program.conf.HKLastCropShot = hkc;
                    break;
                case 5: //Clipboard Upload
                    Program.conf.HKClipboardUpload = hkc;
                    break;
                case 6: //Actions Toolbar
                    Program.conf.HKActionsToolbar = hkc;
                    break;
                case 7: //Quick Options
                    Program.conf.HKQuickOptions = hkc;
                    break;
                case 8: //Drag & Drop Window
                    Program.conf.HKDropWindow = hkc;
                    break;
                case 9: //Language Translator
                    Program.conf.HKLanguageTranslator = hkc;
                    break;
                case 10: //Screen Color Picker
                    Program.conf.HKScreenColorPicker = hkc;
                    break;
            }

            lblHotkeyStatus.Text = dgvHotkeys.Rows[mHKSelectedRow].Cells[0].Value + " Hotkey set to: " + mHKSetcombo + ". Press enter when done setting all desired Hotkeys.";
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
                    case 7: // quick options
                        txtActiveHelp.Text += "quickly select the destination you would like to send images via a small pop up form.";
                        break;
                    case 8: // drop window
                        txtActiveHelp.Text += "display a Drop Window so can drag and drop image files from Windows Explorer to upload.";
                        break;
                    case 9: // language translator
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

        //private void cbActiveHelp_CheckedChanged(object sender, EventArgs e)
        //{
        //    Program.conf.ActiveHelp = cbActiveHelp.Checked;
        //    CheckActiveHelp();
        //}

        //private void CheckActiveHelp()
        //{
        //    splitContainerApp.Panel2Collapsed = !Program.conf.ActiveHelp;
        //    this.Height = (Program.conf.ActiveHelp ? Program.conf.WindowSize.Height : Program.conf.WindowSize.Height - txtActiveHelp.Height);
        //    this.Refresh();
        //}

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
            //if (Program.conf.ActiveHelp)
            //{
            string help = ((Control)sender).Tag.ToString();
            if (mGTranslator != null && Program.conf.GTActiveHelp && cbHelpToLanguage.Items.Count > 0)
            {
                StartGTActiveHelp(help);
            }
            else
            {
                txtActiveHelp.Text = help;
            }
            // }
        }

        private void StartGTActiveHelp(string help)
        {
            bwActiveHelp.DoWork += new DoWorkEventHandler(bwActiveHelp_DoWork);
            GoogleTranslate.TranslationInfo ti =
                new GoogleTranslate.TranslationInfo(help, new GoogleTranslate.GTLanguage("en", "English"),
                    mGTranslator.LanguageOptions.TargetLangList[cbHelpToLanguage.SelectedIndex]);
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
                GoogleTranslate.ResultPacket grp = (GoogleTranslate.ResultPacket)e.Result;
                txtActiveHelp.Text = grp.TranslatedText;
            }
        }

        private void bwActiveHelp_DoWork(object sender, DoWorkEventArgs e)
        {
            GoogleTranslate.TranslationInfo ti = (GoogleTranslate.TranslationInfo)e.Argument;
            GoogleTranslate.ResultPacket grp = mGTranslator.TranslateText(ti);
            e.Result = grp;
        }

        private void btnRegCodeTinyPic_Click(object sender, EventArgs e)
        {
            UserPassBox ub = new UserPassBox("Enter TinyPic Email Address and Password",
                string.IsNullOrEmpty(Program.conf.TinyPicUserName) ? "someone@gmail.com" :
                Program.conf.TinyPicUserName, Program.conf.TinyPicPassword) { Icon = this.Icon };
            ub.ShowDialog();
            if (ub.DialogResult == DialogResult.OK)
            {
                TinyPicUploader tpu = new TinyPicUploader(Program.TINYPIC_ID, Program.TINYPIC_KEY, UploadMode.API);
                txtTinyPicShuk.Text = tpu.UserAuth(ub.UserName, ub.Password);
                if (Program.conf.RememberTinyPicUserPass)
                {
                    Program.conf.TinyPicUserName = ub.UserName;
                    Program.conf.TinyPicPassword = ub.Password;
                }
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
            else if (e.TabPage == tpHotkeys)
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
            else if (e.TabPage == tpEditors)
            {
                txtActiveHelp.Text = tabDesc + string.Format("configure the Image Editing application you wish to run after taking the screenshot. {0} will automatically load this application and enable you to edit the image before uploading.", Application.ProductName);
            }
            else if (e.TabPage == tpCapture)
            {
                txtActiveHelp.Text = tabDesc + string.Format("customize file naming patterns for the screenshot you are taking.");
            }
            else if (e.TabPage == tpHistory)
            {
                txtActiveHelp.Text = tabDesc + "copy screenshot URLs to Clipboard under diffent modes and preview the screenshots. To access Copy to Clipboard options, right click on one or more screenshot entries in the Screenshots list box.";
            }
            else if (e.TabPage == tpOptions)
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

            nudSwitchAfter.Tag = string.Format("After {0} KiB, {1} will switch format from {2} to JPG", nudSwitchAfter.Text, Application.ProductName, cbFileFormat.Text.ToUpper());

            nudWatermarkOffset.Tag = string.Format("Move Watermark {0} pixels leftwards and {0} pixels upwards from the Bottom Right corner of the Screenshot.", nudWatermarkOffset.Value);

            txtWatermarkText.Tag = "The naming pattern that watermarks follow. To close this context menu just click in another textbox.";

            //Paths
            txtImagesDir.Tag = "The directory where all screenshots will be placed (unless deleted with the option below).";

            cbFileFormat.Tag = "The format that screenshots will be saved as.";

            //active help inconsistency (uses label because numeric up/down doesn't support mousehover event
            lblQuality.Tag = "The quality (1-100%) of JPEG screenshots. This quality setting does not effect any other type of Image Format.";

            cbSwitchFormat.Tag = "The secondary format that the program will switch to after a user-specified limit has been reached.";

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

        private void cbCropStyle_SelectedIndexChanged(object sender, EventArgs e)
        {
            Program.conf.CropRegionStyle = cbCropStyle.SelectedIndex;
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

        private Image CropImage(Image img, Rectangle rect)
        {
            Image cropped = new Bitmap(rect.Width, rect.Height);
            Graphics e = Graphics.FromImage(cropped);
            e.CompositingQuality = CompositingQuality.HighQuality;
            e.SmoothingMode = SmoothingMode.HighQuality;
            e.InterpolationMode = InterpolationMode.HighQualityBicubic;
            e.DrawImage(img, new Rectangle(0, 0, rect.Width, rect.Height), rect, GraphicsUnit.Pixel);
            return cropped;
        }

        private void txtActiveHelp_LinkClicked(object sender, LinkClickedEventArgs e)
        {
            Process.Start(e.LinkText);
        }

        private void SetClipboardFromHistory(ClipboardUriType type)
        {
            if (lbHistory.SelectedIndex != -1)
            {
                List<string> screenshots = new List<string>();
                for (int i = 0; i < lbHistory.SelectedItems.Count; i++)
                {
                    HistoryItem hi = (HistoryItem)lbHistory.SelectedItems[i];
                    if (hi.ScreenshotManager != null)
                    {
                        screenshots.Add(hi.ScreenshotManager.GetUrlByType(type));
                    }
                }
                if (screenshots.Count > 0)
                {
                    if (Program.conf.HistoryReverseList)
                    {
                        screenshots.Reverse();
                    }
                    StringBuilder sb = new StringBuilder();
                    if (Program.conf.HistoryAddSpace)
                    {
                        sb.AppendLine();
                    }
                    for (int i = 0; i < screenshots.Count; i++)
                    {
                        sb.Append(screenshots[i]);
                        if (i < lbHistory.SelectedItems.Count - 1)
                        {
                            sb.AppendLine();
                        }
                    }
                    string result = sb.ToString();
                    if (!string.IsNullOrEmpty(result))
                    {
                        Clipboard.SetText(result);
                    }
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

                if (hi != null)
                {
                    bool checkLocal = !string.IsNullOrEmpty(hi.LocalPath) && File.Exists(hi.LocalPath);
                    bool checkRemote = !string.IsNullOrEmpty(hi.RemotePath);
                    tsmCopyCbHistory.Enabled = checkRemote;
                    browseURLToolStripMenuItem.Enabled = checkRemote;
                    btnHistoryCopyLink.Enabled = checkRemote;
                    btnHistoryBrowseURL.Enabled = checkRemote;
                    btnHistoryOpenLocalFile.Enabled = checkLocal;

                    if (FileSystem.IsValidImageFile(hi.LocalPath))
                    {
                        pbPreview.Visible = true;
                        txtPreview.Visible = false;
                        btnHistoryCopyImage.Enabled = true;
                        if (checkLocal)
                        {
                            pbPreview.ImageLocation = hi.LocalPath;
                        }
                        else if (checkRemote)
                        {
                            pbPreview.ImageLocation = hi.RemotePath;
                        }
                    }
                    else if (FileSystem.IsValidTextFile(hi.LocalPath))
                    {
                        pbPreview.Visible = false;
                        txtPreview.Visible = true;
                        txtPreview.Text = File.ReadAllText(hi.LocalPath);
                    }
                    txtHistoryLocalPath.Text = hi.LocalPath;
                    txtHistoryRemotePath.Text = hi.RemotePath;
                    lblHistoryScreenshot.Text = string.Format("{0} ({1})", hi.JobName, hi.DestinationName);
                }

                if (Program.conf.HistoryShowTooltips && hi != null)
                {
                    ttApp.SetToolTip(lbHistory, hi.GetStatistics());
                    ttApp.SetToolTip(pbPreview, hi.GetStatistics());
                }
            }
        }

        private void HistoryRetryUpload(HistoryItem hi)
        {
            if (hi != null && File.Exists(hi.LocalPath))
            {
                MainAppTask task = CreateTask(MainAppTask.Jobs.UPLOAD_IMAGE);
                task.JobCategory = hi.JobCategory;
                task.SetImage(hi.LocalPath);
                task.SetLocalFilePath(hi.LocalPath);
                task.ImageDestCategory = hi.ImageDestCategory;
                task.MyWorker.RunWorkerAsync(task);
            }
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
            Program.conf.ShowWatermark = cboShowWatermark.Checked;
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
                ToolStripMenuItem tsi = new ToolStripMenuItem
                {
                    Text = NameParser.replacementVars[i].PadRight(3, ' ') + " - " + NameParser.replacementDescriptions[i],
                    Tag = NameParser.replacementVars[i]
                };
                tsi.Click += watermarkCodeMenu_Click;
                codesMenu.Items.Add(tsi);
            }
            CodesMenuCloseEvents();
        }

        private void watermarkCodeMenu_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem tsi = (ToolStripMenuItem)sender;
            int oldPos = txtWatermarkText.SelectionStart;
            string appendText;
            if (oldPos > 0 && txtWatermarkText.Text[txtWatermarkText.SelectionStart - 1] == NameParser.prefix[0])
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
            FontDialog fDialog = new FontDialog
            {
                ShowColor = true,
                Font = XMLSettings.DeserializeFont(Program.conf.WatermarkFont),
                Color = XMLSettings.DeserializeColor(Program.conf.WatermarkFontColor)
            };
            if (fDialog.ShowDialog() == DialogResult.OK)
            {
                Program.conf.WatermarkFont = XMLSettings.SerializeFont(fDialog.Font);
                Program.conf.WatermarkFontColor = XMLSettings.SerializeColor(fDialog.Color);
                pbWatermarkFontColor.BackColor = XMLSettings.DeserializeColor(Program.conf.WatermarkFontColor);
                lblWatermarkFont.Text = FontToString();
            }
            TestWatermark();
        }

        private string FontToString()
        {
            return FontToString(XMLSettings.DeserializeFont(Program.conf.WatermarkFont),
                 XMLSettings.DeserializeColor(Program.conf.WatermarkFontColor));
        }

        private string FontToString(Font font, Color color)
        {
            return "Name: " + font.Name + " - Size: " + font.Size + " - Style: " + font.Style + " - Color: " +
                color.R + "," + color.G + "," + color.B;
            //+ " - Color: " + (color.IsNamedColor ? color.Name : "(R:" + color.R + " G:" + color.G + " B:" + color.B + ")");
        }

        private void nudWatermarkOffset_ValueChanged(object sender, EventArgs e)
        {
            Program.conf.WatermarkOffset = nudWatermarkOffset.Value;
            TestWatermark();
        }

        private void nudWatermarkBackTrans_ValueChanged(object sender, EventArgs e)
        {
            Program.conf.WatermarkBackTrans = nudWatermarkBackTrans.Value;
            trackWatermarkBackgroundTrans.Value = (int)nudWatermarkBackTrans.Value;
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

        private void tsmDropWindow_Click(object sender, EventArgs e)
        {
            ShowDropWindow();
        }

        private void tsmUploadFromClipboard_Click(object sender, EventArgs e)
        {
            UploadUsingClipboard();
        }

        private void languageTranslatorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Clipboard.ContainsText()) StartBW_LanguageTranslator(Clipboard.GetText());
        }

        private void screenColorPickerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ScreenColorPicker();
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
            using (Bitmap bmp = new Bitmap((Image)(resources.GetObject("pbLogo.Image"))).
                Clone(new Rectangle(62, 33, 199, 140), PixelFormat.Format32bppArgb))
            {
                Bitmap bmp2 = new Bitmap(pbWatermarkShow.ClientRectangle.Width, pbWatermarkShow.ClientRectangle.Height);
                Graphics g = Graphics.FromImage(bmp2);
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.DrawImage(bmp, new Rectangle(0, 0, pbWatermarkShow.ClientRectangle.Width, pbWatermarkShow.ClientRectangle.Height));
                pbWatermarkShow.Image = WatermarkMaker.GetImage(bmp2);
            }
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

        private void SelectColor(Control pb, ref string setting)
        {
            DialogColor dColor = new DialogColor(pb.BackColor);
            if (dColor.ShowDialog() == DialogResult.OK)
            {
                pb.BackColor = dColor.Color;
                setting = XMLSettings.SerializeColor(dColor.Color);
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
            trackWatermarkFontTrans.Value = (int)nudWatermarkFontTrans.Value;
        }

        private void nudWatermarkCornerRadius_ValueChanged(object sender, EventArgs e)
        {
            Program.conf.WatermarkCornerRadius = nudWatermarkCornerRadius.Value;
            TestWatermark();
        }

        private void cbWatermarkGradientType_SelectedIndexChanged(object sender, EventArgs e)
        {
            Program.conf.WatermarkGradientType = (LinearGradientMode)cbWatermarkGradientType.SelectedIndex;
            TestWatermark();
        }

        private void CopyImageFromHistory()
        {
            if (lbHistory.SelectedIndex != -1)
            {
                HistoryItem hi = (HistoryItem)lbHistory.SelectedItem;
                if (!string.IsNullOrEmpty(hi.LocalPath))
                {
                    using (Image img = MyGraphics.GetImageSafely(hi.LocalPath))
                    {
                        if (img != null)
                        {
                            Clipboard.SetImage(img);
                        }
                    }
                }
            }
        }

        private void CopyLinkFromHistory()
        {
            if (lbHistory.SelectedIndex != -1)
            {
                HistoryItem hi = (HistoryItem)lbHistory.SelectedItem;
                if (!string.IsNullOrEmpty(hi.RemotePath))
                {
                    Clipboard.SetText(hi.RemotePath);
                }
            }
        }

        private void copyImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CopyImageFromHistory();
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
                        Microsoft.VisualBasic.FileIO.FileSystem.DeleteFile(hi.LocalPath, Microsoft.VisualBasic.FileIO.UIOption.OnlyErrorDialogs, Microsoft.VisualBasic.FileIO.RecycleOption.SendToRecycleBin);
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
            tpWatermark.MouseClick += new MouseEventHandler(CodesMenuCloseEvent);
            foreach (Control cntrl in tpWatermark.Controls)
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
            bwLanguages.DoWork += new DoWorkEventHandler(bwOnlineTasks_DoWork);
            bwLanguages.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bwOnlineTasks_RunWorkerCompleted);
            bwLanguages.RunWorkerAsync();
        }

        private void bwOnlineTasks_DoWork(object sender, DoWorkEventArgs e)
        {
            mGTranslator = new GoogleTranslate();
            if (Program.conf.RememberTinyPicUserPass && !string.IsNullOrEmpty(Program.conf.TinyPicUserName) && !string.IsNullOrEmpty(Program.conf.TinyPicPassword))
            {
                TinyPicUploader tpu = new TinyPicUploader(Program.TINYPIC_ID, Program.TINYPIC_KEY, UploadMode.API);
                Program.conf.TinyPicShuk = tpu.UserAuth(Program.conf.TinyPicUserName, Program.conf.TinyPicPassword);
            }
        }

        private void bwOnlineTasks_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (mGTranslator != null)
            {
                cbFromLanguage.Items.Clear();
                cbToLanguage.Items.Clear();
                cbHelpToLanguage.Items.Clear();
                foreach (GoogleTranslate.GTLanguage gtLang in mGTranslator.LanguageOptions.SourceLangList)
                {
                    cbFromLanguage.Items.Add(gtLang.Name);
                }
                foreach (GoogleTranslate.GTLanguage gtLang in mGTranslator.LanguageOptions.TargetLangList)
                {
                    cbToLanguage.Items.Add(gtLang.Name);
                    cbHelpToLanguage.Items.Add(gtLang.Name);
                }
                SelectLanguage(Program.conf.FromLanguage, Program.conf.ToLanguage, Program.conf.HelpToLanguage);
                if (cbFromLanguage.Items.Count > 0) cbFromLanguage.Enabled = true;
                if (cbToLanguage.Items.Count > 0) cbToLanguage.Enabled = true;
                if (cbHelpToLanguage.Items.Count > 0) cbHelpToLanguage.Enabled = true;
            }
            if (!string.IsNullOrEmpty(Program.conf.TinyPicShuk) && Program.conf.TinyPicShuk != txtTinyPicShuk.Text)
            {
                txtTinyPicShuk.Text = Program.conf.TinyPicShuk;
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
            HistoryRetryUpload((HistoryItem)lbHistory.SelectedItem);
        }

        private void pbHistoryThumb_Click(object sender, EventArgs e)
        {
            HistoryItem hi = (HistoryItem)lbHistory.SelectedItem;
            if (hi != null && File.Exists(hi.LocalPath))
            {
                if (hi.ScreenshotManager != null)
                {
                    if (FileSystem.IsValidImageFile(hi.LocalPath))
                    {
                        ShowScreenshot sc = new ShowScreenshot();
                        if (hi.ScreenshotManager.GetImage() != null)
                        {
                            sc.BackgroundImage = Image.FromFile(hi.LocalPath);
                            sc.ShowDialog();
                        }
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

        private void FTPSetup(IEnumerable<FTPAccount> accs)
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

            FTPAccount acc = new FTPAccount
            {
                Name = txtFTPName.Text,
                Server = txtFTPServer.Text,
                Port = (int)nudFTPServerPort.Value,
                Username = txtFTPUsername.Text,
                Password = txtFTPPassword.Text
            };
            if (!txtFTPPath.Text.StartsWith("/"))
            {
                txtFTPPath.Text = string.Concat("/", txtFTPPath.Text);
            }
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
                txtFTPStatus.Text = "Updated.";

                FTPAccount acc = GetFTPAccountFromFields();

                if (Program.conf.FTPAccountList != null)
                {
                    Program.conf.FTPAccountList[lbFTPAccounts.SelectedIndex] = acc; //use selected index instead of 0
                }
                else
                {
                    Program.conf.FTPAccountList = new List<FTPAccount> { acc };
                }

                lbFTPAccounts.Items[lbFTPAccounts.SelectedIndex] = FTPAdd(acc);

                RewriteFTPRightClickMenu();
            }
            else
            {
                txtFTPStatus.Text = "Not Updated.";
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
            if (Program.conf.FTPAccountList != null && sel != -1 && sel < Program.conf.FTPAccountList.Count && Program.conf.FTPAccountList[sel] != null)
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
                txtFTPStatus.Text = "Not Updated."; //change to FTP Account not added?
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
                SaveFileDialog dlg = new SaveFileDialog
                {
                    FileName = string.Format("{0}-{1}-accounts", Application.ProductName, DateTime.Now.ToString("yyyyMMdd")),
                    Filter = Program.FILTER_ACCOUNTS
                };
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    FTPAccountManager fam = new FTPAccountManager(Program.conf.FTPAccountList);
                    fam.Save(dlg.FileName);
                }
            }
        }

        private void btnAccsImport_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog { Filter = Program.FILTER_ACCOUNTS };
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                FTPAccountManager fam = FTPAccountManager.Read(dlg.FileName);
                FTPSetup(fam.FTPAccounts);
            }
        }

        private void btnTestConnection_Click(object sender, EventArgs e)
        {
            txtFTPStatus.Text = "Testing..."; //Testing
            txtFTPStatus.Update();
            TrimFTPControls();
            int port = (int)nudFTPServerPort.Value;

            try
            {
                FTPAccount acc = new FTPAccount
                {
                    Server = txtFTPServer.Text,
                    Port = port,
                    Username = txtFTPUsername.Text,
                    Password = txtFTPPassword.Text,
                    IsActive = rbFTPActive.Checked,
                    Path = txtFTPPath.Text
                };

                FTP ftp = new FTP(ref acc);
                if (ftp.ListDirectory() != null)
                {
                    txtFTPStatus.Text = "Success"; //Success
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
            Program.conf.FTPCreateThumbnail = chkEnableThumbnail.Checked;
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
            CheckUpdates();
        }

        private void cbAddFailedScreenshot_CheckedChanged(object sender, EventArgs e)
        {
            Program.conf.AddFailedScreenshot = cbAddFailedScreenshot.Checked;
        }

        private void ShowDropWindow()
        {
            if (!bDropWindowOpened)
            {
                bDropWindowOpened = true;
                DropWindow dw = new DropWindow();
                dw.Result += new StringsEventHandler(dw_Result);
                dw.FormClosed += new FormClosedEventHandler(dw_FormClosed);
                dw.Show();
                Rectangle taskbar = User32.GetTaskbarRectangle();
                if (Program.conf.LastDropBoxPosition == Point.Empty)
                {
                    dw.Location = new Point(SystemInformation.PrimaryMonitorSize.Width - dw.Width - 100,
                        SystemInformation.PrimaryMonitorSize.Height - taskbar.Height - dw.Height - 10);
                }
                else
                {
                    dw.Location = Program.conf.LastDropBoxPosition;
                }
            }
        }

        private void dw_Result(object sender, string[] strings)
        {
            if (strings != null) ScreenshotUsingDragDrop(strings);
        }

        private void dw_FormClosed(object sender, FormClosedEventArgs e)
        {
            bDropWindowOpened = false;
        }

        private void cbShowUploadDuration_CheckedChanged(object sender, EventArgs e)
        {
            Program.conf.ShowUploadDuration = cbShowUploadDuration.Checked;
        }

        /// <summary>
        /// Searches for an Image Software in settings and returns it
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        private static Software GetImageSoftware(string name)
        {
            foreach (Software app in Program.conf.ImageSoftwareList)
            {
                if (app != null && app.Name != null)
                {
                    if (app.Name.Equals(name))
                        return app;
                }

            }
            return null;
        }

        private void CheckUpdates()
        {
            btnCheckUpdate.Enabled = false;
            lblUpdateInfo.Text = "Checking for Updates...";
            BackgroundWorker updateThread = new BackgroundWorker { WorkerReportsProgress = true };
            updateThread.DoWork += new DoWorkEventHandler(updateThread_DoWork);
            updateThread.ProgressChanged += new ProgressChangedEventHandler(updateThread_ProgressChanged);
            updateThread.RunWorkerCompleted += new RunWorkerCompletedEventHandler(updateThread_RunWorkerCompleted);
            updateThread.RunWorkerAsync(Application.ProductName);
        }

        private void updateThread_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            switch (e.ProgressPercentage)
            {
                case 1:
                    lblUpdateInfo.Text = (string)e.UserState;
                    break;
            }
        }

        private void updateThread_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = (BackgroundWorker)sender;
            NewVersionWindowOptions nvwo = new NewVersionWindowOptions { MyIcon = this.Icon, MyImage = Resources.main };

            UpdateCheckerOptions uco = new UpdateCheckerOptions
            {
                CheckExperimental = Program.conf.CheckExperimental,
                UpdateCheckType = Program.conf.UpdateCheckType,
                MyNewVersionWindowOptions = nvwo
            };

            UpdateChecker updateChecker = new UpdateChecker((string)e.Argument, uco);
            worker.ReportProgress(1, updateChecker.StartCheckUpdate());
            updateChecker.ShowPrompt();
        }

        private void updateThread_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            btnCheckUpdate.Enabled = true;
        }

        private void cbSelectedWindowStyle_SelectedIndexChanged(object sender, EventArgs e)
        {
            Program.conf.SelectedWindowRegionStyle = cbSelectedWindowStyle.SelectedIndex;
        }

        private void nudCropGridSize_ValueChanged(object sender, EventArgs e)
        {
            Program.conf.CropGridSize.Width = (int)nudCropGridWidth.Value;
        }

        private void nudCropGridHeight_ValueChanged(object sender, EventArgs e)
        {
            Program.conf.CropGridSize.Height = (int)nudCropGridHeight.Value;
        }

        private void cbCropShowGrids_CheckedChanged(object sender, EventArgs e)
        {
            Program.conf.CropShowGrids = cbCropShowGrids.Checked;
        }

        private void cboUpdateCheckType_SelectedIndexChanged(object sender, EventArgs e)
        {
            Program.conf.UpdateCheckType = (UpdateCheckType)cboUpdateCheckType.SelectedIndex;
        }

        private void cbAddSpace_CheckedChanged(object sender, EventArgs e)
        {
            Program.conf.HistoryAddSpace = cbHistoryAddSpace.Checked;
        }

        private void cbReverse_CheckedChanged(object sender, EventArgs e)
        {
            Program.conf.HistoryReverseList = cbHistoryReverseList.Checked;
        }

        private void nudHistoryMaxItems_ValueChanged(object sender, EventArgs e)
        {
            Program.conf.HistoryMaxNumber = (int)nudHistoryMaxItems.Value;
            CheckHistoryItems();
            SaveHistoryItems();
        }

        private void cbCloseDropBox_CheckedChanged(object sender, EventArgs e)
        {
            Program.conf.CloseDropBox = cbCloseDropBox.Checked;
        }

        private void btnHistoryClear_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you really want to clear the History List?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                lbHistory.Items.Clear();
                CheckHistoryItems();
                SaveHistoryItems();
            }
        }

        private void tsmQuickActions_Click(object sender, EventArgs e)
        {
            ShowQuickActions();
        }

        #region Quick Actions

        private void ShowQuickActions()
        {
            if (!bQuickActionsOpened)
            {
                bQuickActionsOpened = true;
                ToolbarWindow quickActions = new ToolbarWindow { Icon = Resources.zss_main };
                quickActions.EventJob += new JobsEventHandler(EventJobs);
                quickActions.FormClosed += new FormClosedEventHandler(quickActions_FormClosed);
                quickActions.Show();
                Rectangle taskbar = User32.GetTaskbarRectangle();
                quickActions.Location = new Point(SystemInformation.PrimaryMonitorSize.Width - quickActions.Width - 100,
                    SystemInformation.PrimaryMonitorSize.Height - taskbar.Height - quickActions.Height - 10);
            }
        }

        private void quickActions_FormClosed(object sender, FormClosedEventArgs e)
        {
            bQuickActionsOpened = false;
        }

        private void EventJobs(object sender, MainAppTask.Jobs jobs)
        {
            switch (jobs)
            {
                case MainAppTask.Jobs.TAKE_SCREENSHOT_SCREEN:
                    StartBW_EntireScreen();
                    break;
                case MainAppTask.Jobs.TAKE_SCREENSHOT_WINDOW_SELECTED:
                    StartBW_SelectedWindow();
                    break;
                case MainAppTask.Jobs.TAKE_SCREENSHOT_CROPPED:
                    StartBW_CropShot();
                    break;
                case MainAppTask.Jobs.TAKE_SCREENSHOT_LAST_CROPPED:
                    StartBW_LastCropShot();
                    break;
                case MainAppTask.Jobs.UPLOAD_FROM_CLIPBOARD:
                    UploadUsingClipboard();
                    break;
                case MainAppTask.Jobs.PROCESS_DRAG_N_DROP:
                    ShowDropWindow();
                    break;
                case MainAppTask.Jobs.LANGUAGE_TRANSLATOR:
                    if (Clipboard.ContainsText()) StartBW_LanguageTranslator(Clipboard.GetText());
                    break;
                case MainAppTask.Jobs.SCREEN_COLOR_PICKER:
                    ScreenColorPicker();
                    break;
            }
        }

        private void cbCloseQuickActions_CheckedChanged(object sender, EventArgs e)
        {
            Program.conf.CloseQuickActions = cbCloseQuickActions.Checked;
        }

        #endregion

        private void chkRememberTinyPicUserPass_CheckedChanged(object sender, EventArgs e)
        {
            Program.conf.RememberTinyPicUserPass = chkRememberTinyPicUserPass.Checked;
        }

        private void btnResetIncrement_Click(object sender, EventArgs e)
        {
            Program.conf.AutoIncrement = 0;
        }

        private void btnImageCopy_Click(object sender, EventArgs e)
        {
            CopyImageFromHistory();
        }

        private void lbHistory_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.A)
            {
                for (int i = lbHistory.Items.Count - 1; i >= 0; i--)
                {
                    lbHistory.SetSelected(i, true);
                }
            }
        }

        private void btnCopyLink_Click(object sender, EventArgs e)
        {
            CopyLinkFromHistory();
        }

        private void cbHistoryListFormat_SelectedIndexChanged(object sender, EventArgs e)
        {
            Program.conf.HistoryListFormat = (HistoryListFormat)cbHistoryListFormat.SelectedIndex;
            LoadHistoryItems();
        }

        private void cbShowHistoryTooltip_CheckedChanged(object sender, EventArgs e)
        {
            Program.conf.HistoryShowTooltips = cbShowHistoryTooltip.Checked;
        }

        private void cbHistorySave_CheckedChanged(object sender, EventArgs e)
        {
            Program.conf.HistorySave = cbHistorySave.Checked;
        }

        private void pbCropCrosshairColor_Click(object sender, EventArgs e)
        {
            SelectColor((PictureBox)sender, ref Program.conf.CropCrosshairColor);
        }

        private void chkCaptureFallback_CheckedChanged(object sender, EventArgs e)
        {
            Program.conf.CaptureEntireScreenOnError = chkCaptureFallback.Checked;
        }

        private void nudSwitchAfter_ValueChanged(object sender, EventArgs e)
        {
            Program.conf.SwitchAfter = (int)nudSwitchAfter.Value;
        }

        private void cbCropDynamicCrosshair_CheckedChanged(object sender, EventArgs e)
        {
            Program.conf.CropDynamicCrosshair = cbCropDynamicCrosshair.Checked;
        }

        private void nudCrosshairLineCount_ValueChanged(object sender, EventArgs e)
        {
            Program.conf.CrosshairLineCount = (int)nudCrosshairLineCount.Value;
        }

        private void nudCrosshairLineSize_ValueChanged(object sender, EventArgs e)
        {
            Program.conf.CrosshairLineSize = (int)nudCrosshairLineSize.Value;
        }

        private void nudCropInterval_ValueChanged(object sender, EventArgs e)
        {
            Program.conf.CropInterval = (int)nudCropCrosshairInterval.Value;
        }

        private void nudCropStep_ValueChanged(object sender, EventArgs e)
        {
            Program.conf.CropStep = (int)nudCropCrosshairStep.Value;
        }

        private void cbCropShowBigCross_CheckedChanged(object sender, EventArgs e)
        {
            Program.conf.CropShowBigCross = cbCropShowBigCross.Checked;
        }

        private void cbShowCropRuler_CheckedChanged(object sender, EventArgs e)
        {
            Program.conf.CropShowRuler = cbShowCropRuler.Checked;
        }

        private void cbSelectedWindowRuler_CheckedChanged(object sender, EventArgs e)
        {
            Program.conf.SelectedWindowRuler = cbSelectedWindowRuler.Checked;
        }

        private void cbCropDynamicBorderColor_CheckedChanged(object sender, EventArgs e)
        {
            Program.conf.CropDynamicBorderColor = cbCropDynamicBorderColor.Checked;
        }

        private void nudCropRegionInterval_ValueChanged(object sender, EventArgs e)
        {
            Program.conf.CropRegionInterval = nudCropRegionInterval.Value;
        }

        private void nudCropRegionStep_ValueChanged(object sender, EventArgs e)
        {
            Program.conf.CropRegionStep = nudCropRegionStep.Value;
        }

        private void nudCropHueRange_ValueChanged(object sender, EventArgs e)
        {
            Program.conf.CropHueRange = nudCropHueRange.Value;
        }

        private void cbSelectedWindowDynamicBorderColor_CheckedChanged(object sender, EventArgs e)
        {
            Program.conf.SelectedWindowDynamicBorderColor = cbSelectedWindowDynamicBorderColor.Checked;
        }

        private void nudSelectedWindowRegionInterval_ValueChanged(object sender, EventArgs e)
        {
            Program.conf.SelectedWindowRegionInterval = nudSelectedWindowRegionInterval.Value;
        }

        private void nudSelectedWindowRegionStep_ValueChanged(object sender, EventArgs e)
        {
            Program.conf.SelectedWindowRegionStep = nudSelectedWindowRegionStep.Value;
        }

        private void nudSelectedWindowHueRange_ValueChanged(object sender, EventArgs e)
        {
            Program.conf.SelectedWindowHueRange = nudSelectedWindowHueRange.Value;
        }

        private void cbCropGridMode_CheckedChanged(object sender, EventArgs e)
        {
            Program.conf.CropGridToggle = cboCropGridMode.Checked;
        }

        private void cbTinyPicSizeCheck_CheckedChanged(object sender, EventArgs e)
        {
            Program.conf.TinyPicSizeCheck = cbTinyPicSizeCheck.Checked;
        }


        private void PreviewWatermark()
        {
            Program.conf.ShowWatermark = (Program.conf.WatermarkMode != WatermarkType.NONE);
            Program.conf.WatermarkUseImage = (Program.conf.WatermarkMode == WatermarkType.IMAGE);

            if (Program.conf.ShowWatermark)
            {
                TestWatermark();
            }
        }

        private void txtWatermarkImageLocation_TextChanged(object sender, EventArgs e)
        {
            if (File.Exists(txtWatermarkImageLocation.Text))
            {
                Program.conf.WatermarkImageLocation = txtWatermarkImageLocation.Text;
                TestWatermark();
            }
        }

        private void btwWatermarkBrowseImage_Click(object sender, EventArgs e)
        {
            OpenFileDialog fd = new OpenFileDialog
            {
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop)
            };
            if (fd.ShowDialog() == DialogResult.OK)
            {
                txtWatermarkImageLocation.Text = fd.FileName;
            }
        }

        private void cbPromptforUpload_CheckedChanged(object sender, EventArgs e)
        {
            Program.conf.PromptforUpload = cbPromptforUpload.Checked;
        }

        private void cbAutoChangeUploadDestination_CheckedChanged(object sender, EventArgs e)
        {
            Program.conf.AutoChangeUploadDestination = cboAutoChangeUploadDestination.Checked;
        }

        private void nudUploadDurationLimit_ValueChanged(object sender, EventArgs e)
        {
            Program.conf.UploadDurationLimit = nudUploadDurationLimit.Value;
        }

        private void StartDebug()
        {
            debug = new Debug();
            debug.GetDebugInfo += new StringEventHandler(debug_GetDebugInfo);
        }

        private void debug_GetDebugInfo(object sender, string e)
        {
            if (this.Visible)
            {
                lblDebugInfo.Text = e;
            }
        }

        private void btnDebugStart_Click(object sender, EventArgs e)
        {
            if (debug.DebugTimer.Enabled)
            {
                btnDebugStart.Text = "Start";
            }
            else
            {
                btnDebugStart.Text = "Pause";
            }
            debug.DebugTimer.Enabled = !debug.DebugTimer.Enabled;
        }

        private void tsmMain_Click(object sender, EventArgs e)
        {
            BringUpMenu();
        }

        private void btnGalleryTinyPic_Click(object sender, EventArgs e)
        {
            Process.Start("http://tinypic.com/yourstuff.php");
        }

        private void cbWatermarkUseBorder_CheckedChanged(object sender, EventArgs e)
        {
            Program.conf.WatermarkUseBorder = cbWatermarkUseBorder.Checked;
            TestWatermark();
        }



        private void cbWatermarkAddReflection_CheckedChanged(object sender, EventArgs e)
        {
            Program.conf.WatermarkAddReflection = cbWatermarkAddReflection.Checked;
            TestWatermark();
        }

        private void btnBrowseRootDir_Click(object sender, EventArgs e)
        {
            string oldRootDir = txtRootFolder.Text;
            FolderBrowserDialog dlg = new FolderBrowserDialog { ShowNewFolderButton = true };
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                Program.SetRootFolder(dlg.SelectedPath);
                txtRootFolder.Text = Settings.Default.RootDir;
            }
            FileSystem.MoveDirectory(oldRootDir, txtRootFolder.Text);
            UpdateGuiControlsPaths();
            Program.conf = XMLSettings.Read();
            SetupScreen();
        }

        private void btnViewRootDir_Click(object sender, EventArgs e)
        {
            ShowDirectory(txtRootFolder.Text);
        }

        private void nudWatermarkImageScale_ValueChanged(object sender, EventArgs e)
        {
            Program.conf.WatermarkImageScale = nudWatermarkImageScale.Value;
            TestWatermark();
        }

        private void trackWatermarkFontTrans_Scroll(object sender, EventArgs e)
        {
            Program.conf.WatermarkFontTrans = trackWatermarkFontTrans.Value;
            nudWatermarkFontTrans.Value = Program.conf.WatermarkFontTrans;
            TestWatermark();
        }

        private void trackWatermarkBackgroundTrans_Scroll(object sender, EventArgs e)
        {
            Program.conf.WatermarkBackTrans = trackWatermarkBackgroundTrans.Value;
            nudWatermarkBackTrans.Value = Program.conf.WatermarkBackTrans;
            TestWatermark();
        }

        private void cbWatermarkAutoHide_CheckedChanged(object sender, EventArgs e)
        {
            Program.conf.WatermarkAutoHide = cbWatermarkAutoHide.Checked;
            TestWatermark();
        }

        private void btnSaveSettings_Click(object sender, EventArgs e)
        {
            SaveSettings();
        }

        private void cboWatermarkType_SelectedIndexChanged(object sender, EventArgs e)
        {
            Program.conf.WatermarkMode = (WatermarkType)cboWatermarkType.SelectedIndex;
            cboShowWatermark.Checked = (Program.conf.WatermarkMode != WatermarkType.NONE);
            PreviewWatermark();
        }

        private void cbSelectedWindowAddBorder_CheckedChanged(object sender, EventArgs e)
        {
            Program.conf.SelectedWindowAddBorder = cbSelectedWindowAddBorder.Checked;
        }
    }
}
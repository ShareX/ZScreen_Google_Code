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
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Microsoft.WindowsAPICodePack.Shell;
using Microsoft.WindowsAPICodePack.Taskbar;
using MS.WindowsAPICodePack.Internal;
using UploadersLib;
using UploadersLib.Helpers;
using UploadersLib.ImageUploaders;
using UploadersLib.TextServices;
using UploadersLib.TextUploaders;
using UploadersLib.URLShorteners;
using ZScreenGUI.Properties;
using ZScreenLib;
using ZSS.ColorsLib;
using ZSS.FTPClientLib;
using ZScreenTesterGUI;
using GradientTester;
using ZScreenLib.Helpers;

namespace ZScreenGUI
{
    public partial class ZScreen : Form
    {
        #region Private Variables

        private bool mGuiIsReady, mClose;
        private int mHadFocusAt;
        private TextBox mHadFocus;
        private ContextMenuStrip codesMenu = new ContextMenuStrip();
        private DebugHelper mDebug = null;
        private ZScreenLib.ImageEffects.TurnImage turnLogo;
        private ThumbnailCacher thumbnailCacher;
        internal static GoogleTranslate mGTranslator = null;
        private System.Windows.Forms.Timer mTimerImageEditorMenuClose = new System.Windows.Forms.Timer()
        {
            Interval = 5000,
        };

        #endregion

        public ZScreen()
        {
            InitializeComponent();
            Uploader.ProxySettings = Adapter.CheckProxySettings();
            ZScreen_SetFormSettings();
            Loader.Worker = new WorkerPrimary(this);
            Loader.Worker2 = new WorkerSecondary(this);
            ZScreen_ConfigGUI();

            Loader.Worker2.PerformOnlineTasks();
            if (Engine.conf.CheckUpdates)
            {
                Loader.Worker2.CheckUpdates();
            }

            tsmEditinImageSoftware.MouseEnter += new EventHandler(tsmEditinImageSoftware_MouseEnter);
            tsmEditinImageSoftware.MouseLeave += new EventHandler(tsmEditinImageSoftware_MouseLeave);
            mTimerImageEditorMenuClose.Tick += new EventHandler(mImageEditorMenuClose_Tick);
            Application.Idle += new EventHandler(Application_Idle);
        }

        void tsmEditinImageSoftware_MouseEnter(object sender, EventArgs e)
        {
            tsmEditinImageSoftware.DropDown.AutoClose = false;
            mTimerImageEditorMenuClose.Enabled = false;
        }

        void tsmEditinImageSoftware_MouseLeave(object sender, EventArgs e)
        {
            mTimerImageEditorMenuClose.Enabled = true;
        }

        void mImageEditorMenuClose_Tick(object sender, EventArgs e)
        {
            tsmEditinImageSoftware.DropDown.Close();
        }

        void Application_Idle(object sender, EventArgs e)
        {
            DelayedTrimMemoryUse();
        }

        internal void ZScreen_Windows7onlyTasks()
        {
            this.ShowInTaskbar = Engine.conf.Windows7TaskbarIntegration && CoreHelpers.RunningOnWin7;
            Engine.conf.MinimizeOnClose = Engine.conf.Windows7TaskbarIntegration && CoreHelpers.RunningOnWin7;

            if (this.Handle != IntPtr.Zero && CoreHelpers.RunningOnWin7)
            {
                try
                {
                    Engine.CheckFileRegistration();

                    Engine.zWindowsTaskbar = TaskbarManager.Instance;
                    Engine.zWindowsTaskbar.ApplicationId = Engine.appId;

                    Engine.zJumpList = JumpList.CreateJumpList();

                    // User Tasks
                    JumpListLink jlCropShot = new JumpListLink(Adapter.ZScreenCliPath(), "Crop Shot");
                    jlCropShot.Arguments = "crop_shot";
                    jlCropShot.IconReference = new IconReference(Adapter.ResourcePath, 1);
                    Engine.zJumpList.AddUserTasks(jlCropShot);

                    JumpListLink jlSelectedWindow = new JumpListLink(Adapter.ZScreenCliPath(), "Selected Window");
                    jlSelectedWindow.Arguments = "selected_window";
                    jlSelectedWindow.IconReference = new IconReference(Adapter.ResourcePath, 2);
                    Engine.zJumpList.AddUserTasks(jlSelectedWindow);

                    JumpListLink jlClipboardUpload = new JumpListLink(Adapter.ZScreenCliPath(), "Clipboard Upload");
                    jlClipboardUpload.Arguments = "clipboard_upload";
                    jlClipboardUpload.IconReference = new IconReference(Adapter.ResourcePath, 3);
                    Engine.zJumpList.AddUserTasks(jlClipboardUpload);

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
                    ThumbnailToolbarButton cropShot = new ThumbnailToolbarButton(Resources.shape_square_ico, "Crop Shot");
                    cropShot.Click += new EventHandler<ThumbnailButtonClickedEventArgs>(cropShot_Click);

                    ThumbnailToolbarButton selWindow = new ThumbnailToolbarButton(Resources.application_double_ico, "Selected Window");
                    selWindow.Click += new EventHandler<ThumbnailButtonClickedEventArgs>(selWindow_Click);

                    ThumbnailToolbarButton clipboardUpload = new ThumbnailToolbarButton(Resources.clipboard_upload_ico, "Clipboard Upload");
                    clipboardUpload.Click += new EventHandler<ThumbnailButtonClickedEventArgs>(clipboardUpload_Click);

                    Engine.zWindowsTaskbar.ThumbnailToolbars.AddButtons(this.Handle, cropShot, selWindow, clipboardUpload);
                    Engine.zJumpList.Refresh();
                    FileSystem.AppendDebug("Integrated into Windows 7 Taskbar");
                }
                catch (Exception ex)
                {
                    FileSystem.AppendDebug("Error while configuring Windows 7 Taskbar", ex);
                }
            }
        }

        private void ZScreen_SetFormSettings()
        {
            this.Icon = Resources.zss_main;
            this.Text = Engine.GetProductName();
            this.niTray.Text = this.Text;
            this.lblLogo.Text = this.Text;
            chkWindows7TaskbarIntegration.Enabled = CoreHelpers.RunningOnWin7;

            if (this.WindowState == FormWindowState.Normal)
            {
                if (Engine.conf.WindowLocation.IsEmpty)
                {
                    Engine.conf.WindowLocation = this.Location;
                }

                if (Engine.conf.WindowSize.IsEmpty)
                {
                    Engine.conf.WindowSize = this.Size;
                }
            }

            // Accounts - FTP
            ucFTPAccounts.btnAdd.Click += new EventHandler(FTPAccountAddButton_Click);
            ucFTPAccounts.btnRemove.Click += new EventHandler(FTPAccountRemoveButton_Click);
            ucFTPAccounts.btnTest.Click += new EventHandler(FTPAccountTestButton_Click);
            ucFTPAccounts.AccountsList.SelectedIndexChanged += new EventHandler(FTPAccountsList_SelectedIndexChanged);

            // Accounts - MindTouch
            ucMindTouchAccounts.btnAdd.Click += new EventHandler(MindTouchAccountAddButton_Click);
            ucMindTouchAccounts.btnRemove.Click += new EventHandler(MindTouchAccountRemoveButton_Click);
            ucMindTouchAccounts.btnTest.Click += new EventHandler(MindTouchAccountTestButton_Click);
            ucMindTouchAccounts.AccountsList.SelectedIndexChanged += new EventHandler(MindTouchAccountsList_SelectedIndexChanged);

            // Accounts - Twitter
            ucTwitterAccounts.btnAdd.Text = "Add...";
            ucTwitterAccounts.btnAdd.Click += new EventHandler(TwitterAccountAddButton_Click);
            ucTwitterAccounts.btnRemove.Click += new EventHandler(TwitterAccountRemoveButton_Click);
            ucTwitterAccounts.btnTest.Text = "Authorize";
            ucTwitterAccounts.btnTest.Click += new EventHandler(TwitterAccountAuthButton_Click);
            ucTwitterAccounts.SettingsGrid.PropertySort = PropertySort.Categorized;
            ucTwitterAccounts.AccountsList.SelectedIndexChanged += new EventHandler(TwitterAccountList_SelectedIndexChanged);

            // Options - Proxy
            ucProxyAccounts.btnAdd.Click += new EventHandler(ProxyAccountsAddButton_Click);
            ucProxyAccounts.btnRemove.Click += new EventHandler(ProxyAccountsRemoveButton_Click);
            ucProxyAccounts.btnTest.Click += new EventHandler(ProxyAccountTestButton_Click);
            ucProxyAccounts.AccountsList.SelectedIndexChanged += new EventHandler(ProxyAccountsList_SelectedIndexChanged);

            // Text Services - Text Uploaders
            ucTextUploaders.MyComboBox = ucDestOptions.cboTextUploaders;
            ucTextUploaders.btnItemAdd.Click += new EventHandler(TextUploadersAddButton_Click);
            ucTextUploaders.btnItemRemove.Click += new EventHandler(TextUploadersRemoveButton_Click);
            ucTextUploaders.MyCollection.SelectedIndexChanged += new EventHandler(cboTextUploaders_SelectedIndexChanged);
            ucTextUploaders.btnItemTest.Click += new EventHandler(TextUploaderTestButton_Click);

            // Text Services - URL Shorteners
            ucUrlShorteners.MyComboBox = ucDestOptions.cboURLShorteners;
            ucUrlShorteners.btnItemAdd.Click += new EventHandler(UrlShortenersAddButton_Click);
            ucUrlShorteners.btnItemRemove.Click += new EventHandler(UrlShortenersRemoveButton_Click);
            ucUrlShorteners.MyCollection.SelectedIndexChanged += new EventHandler(UrlShorteners_SelectedIndexChanged);
            ucUrlShorteners.btnItemTest.Click += new EventHandler(UrlShortenerTestButton_Click);

            // Watermark Codes Menu
            codesMenu.AutoClose = false;
            codesMenu.Font = new Font("Lucida Console", 8);
            codesMenu.Opacity = 0.8;
            codesMenu.ShowImageMargin = false;

            // Dest Selectors
            ucDestOptions.cboFileUploaders.SelectedIndexChanged += new EventHandler(cboFileUploaders_SelectedIndexChanged);
            ucDestOptions.cboImageUploaders.SelectedIndexChanged += new EventHandler(cboImageUploaders_SelectedIndexChanged);
            ucDestOptions.cboTextUploaders.SelectedIndexChanged += new EventHandler(cboTextUploaders_SelectedIndexChanged);
            ucDestOptions.cboURLShorteners.SelectedIndexChanged += new EventHandler(cboURLShorteners_SelectedIndexChanged);

            niTray.BalloonTipClicked += new EventHandler(niTray_BalloonTipClicked);

            DrawZScreenLabel(false);
        }

        private void ZScreen_Load(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Normal)
            {
                Rectangle screenRect = GraphicsMgr.GetScreenBounds();
                screenRect.Inflate(-100, -100);
                if (screenRect.IntersectsWith(new Rectangle(Engine.conf.WindowLocation, Engine.conf.WindowSize)))
                {
                    this.Size = Engine.conf.WindowSize;
                    this.Location = Engine.conf.WindowLocation;
                }
            }

            if (Engine.conf.ActionsToolbarMode)
            {
                this.Hide();
                Loader.Worker.ShowActionsToolbar(false);
            }
            else
            {
                if (Engine.conf.ShowMainWindow)
                {
                    this.WindowState = Engine.conf.WindowState;
                    ShowInTaskbar = Engine.conf.ShowInTaskbar;
                }
                else if (Engine.conf.ShowInTaskbar && Engine.conf.MinimizeOnClose)
                {
                    this.WindowState = FormWindowState.Minimized;
                    ShowInTaskbar = true;
                }
                else
                {
                    Hide();
                }
            }

            Adapter.AddToClipboardByDoubleClick(tpHistory);

            Loader.Worker2.CleanCache();
            StartDebug();

            SetToolTip(nudtScreenshotDelay);
            FillClipboardCopyMenu();
            FillClipboardMenu();

            CreateCodesMenu();

            dgvHotkeys.BackgroundColor = Color.FromArgb(tpHotkeys.BackColor.R, tpHotkeys.BackColor.G, tpHotkeys.BackColor.B);

            turnLogo = new ImageEffects.TurnImage((Image)new ComponentResourceManager(typeof(ZScreen)).GetObject(("pbLogo.Image")));
            turnLogo.ImageTurned += new ImageEffects.TurnImage.ImageEventHandler(x => pbLogo.Image = x);

            thumbnailCacher = new ThumbnailCacher(pbPreview, new Size(450, 230), 10)
            {
                LoadingImage = Resources.ajax_loader
            };

            niTray.Visible = true;
            // Loader.Splash.Close();
            // FileSystem.AppendDebug("Closed Splash Screen");

            txtDebugLog.Text = FileSystem.DebugLog.ToString();
            FileSystem.DebugLogChanged += new FileSystem.DebugLogEventHandler(FileSystem_DebugLogChanged);

            FileSystem.AppendDebug("Loaded ZScreen GUI...");
        }

        private void FileSystem_DebugLogChanged(string line)
        {
            MethodInvoker method = delegate
            {
                txtDebugLog.AppendText(line + Environment.NewLine);
            };

            if (this.InvokeRequired)
            {
                this.Invoke(method);
            }
            else
            {
                method.Invoke();
            }
        }

        protected override void WndProc(ref Message m)
        {
            if (mGuiIsReady)
            {
                switch ((ClipboardHook.Msgs)m.Msg)
                {
                    case ClipboardHook.Msgs.WM_DRAWCLIPBOARD:
                        try
                        {
                            string cbText = Clipboard.GetText();
                            bool uploadImage = Clipboard.ContainsImage() && Engine.conf.MonitorImages;
                            bool uploadText = Clipboard.ContainsText() && Engine.conf.MonitorText;
                            bool uploadFile = Clipboard.ContainsFileDropList() && Engine.conf.MonitorFiles;
                            bool shortenUrl = Clipboard.ContainsText() && FileSystem.IsValidLink(cbText) && cbText.Length > Engine.conf.ShortenUrlAfterUploadAfter && Engine.conf.MonitorUrls;
                            if (uploadImage || uploadText || uploadFile || shortenUrl)
                            {
                                if (cbText != Engine.zClipboardText || string.IsNullOrEmpty(cbText))
                                {
                                    Loader.Worker.UploadUsingClipboard();
                                }
                            }
                        }
                        catch (System.Runtime.InteropServices.ExternalException externEx)
                        {
                            // Copying a field definition in Access 2002 causes this sometimes?
                            Debug.WriteLine("InteropServices.ExternalException: {0}", externEx.Message);
                            return;
                        }
                        catch (Exception ex)
                        {
                            FileSystem.AppendDebug("Error monitoring clipboard", ex);
                            return;
                        }
                        break;
                    case ClipboardHook.Msgs.WM_CHANGECBCHAIN:
                        if (m.WParam == ClipboardHook.mClipboardViewerNext)
                        {
                            ClipboardHook.mClipboardViewerNext = m.LParam;
                        }
                        else
                        {
                            ClipboardHook.SendMessage(m.Msg, m.WParam, m.LParam);
                        }
                        break;
                    default:
                        base.WndProc(ref m);
                        break;
                }
            }
            else
            {
                base.WndProc(ref m);
            }
        }

        private void ZScreen_ConfigGUI()
        {
            FileSystem.AppendDebug("Configuring ZScreen GUI..");
            pgApp.SelectedObject = Engine.conf;
            pgIndexer.SelectedObject = Engine.conf.IndexerConfig;

            #region Global

            //~~~~~~~~~~~~~~~~~~~~~
            //  Global
            //~~~~~~~~~~~~~~~~~~~~~
            Engine.mAppSettings.PreferSystemFolders = Engine.conf.PreferSystemFolders;
            txtRootFolder.Text = Engine.RootAppFolder;
            UpdateGuiControlsPaths();

            #endregion

            #region Main

            //~~~~~~~~~~~~~~~~~~~~~
            //  Main
            //~~~~~~~~~~~~~~~~~~~~~

            if (tsmiTabs.DropDownItems.Count == 0)
            {
                foreach (TabPage tp in tcApp.TabPages)
                {
                    ToolStripMenuItem tsmi = new ToolStripMenuItem(tp.Text + "...");
                    tsmi.Click += new EventHandler(tsmiTab_Click);
                    tsmi.Image = ilApp.Images[tp.ImageKey];
                    tsmi.Tag = tp.Name;
                    tsmiTabs.DropDownItems.Add(tsmi);
                }
            }

            if (ucDestOptions.cboImageUploaders.Items.Count == 0)
            {
                ucDestOptions.cboImageUploaders.Items.AddRange(typeof(ImageDestType).GetDescriptions());
            }

            ucDestOptions.cboImageUploaders.SelectedIndex = (int)Engine.conf.ImageUploaderType;

            if (tsmImageDest.DropDownItems.Count == 0)
            {
                foreach (ImageDestType idt in Enum.GetValues(typeof(ImageDestType)))
                {
                    ToolStripMenuItem tsmi = new ToolStripMenuItem(idt.GetDescription());
                    tsmi.Click += new EventHandler(tsmiDestImages_Click);
                    tsmi.Tag = idt;
                    tsmImageDest.DropDownItems.Add(tsmi);
                }
            }

            CheckToolStripMenuItem(tsmImageDest, GetImageDestMenuItem(Engine.conf.ImageUploaderType));

            if (tsmFileDest.DropDownItems.Count == 0)
            {
                foreach (FileUploaderType fileUploader in Enum.GetValues(typeof(FileUploaderType)))
                {
                    ToolStripMenuItem tsmi = new ToolStripMenuItem(fileUploader.GetDescription());
                    tsmi.Click += new EventHandler(tsmiDestFiles_Click);
                    tsmi.Tag = fileUploader;
                    tsmFileDest.DropDownItems.Add(tsmi);
                }
            }

            CheckToolStripMenuItem(tsmFileDest, GetFileDestMenuItem(Engine.conf.FileDestMode));

            if (cboClipboardTextMode.Items.Count == 0)
            {
                cboClipboardTextMode.Items.AddRange(typeof(ClipboardUriType).GetDescriptions());
            }

            cboClipboardTextMode.SelectedIndex = (int)Engine.conf.ClipboardUriMode;
            nudtScreenshotDelay.Time = Engine.conf.ScreenshotDelayTimes;
            nudtScreenshotDelay.Value = Engine.conf.ScreenshotDelayTime;
            chkManualNaming.Checked = Engine.conf.ManualNaming;
            cbShowCursor.Checked = Engine.conf.ShowCursor;
            cboCropGridMode.Checked = Engine.conf.CropGridToggle;
            nudCropGridWidth.Value = Engine.conf.CropGridSize.Width;
            nudCropGridHeight.Value = Engine.conf.CropGridSize.Height;

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
            if (cbCropStyle.Items.Count == 0)
            {
                cbCropStyle.Items.AddRange(typeof(RegionStyles).GetDescriptions());
            }

            cbCropStyle.SelectedIndex = (int)Engine.conf.CropRegionStyles;
            cbRegionRectangleInfo.Checked = Engine.conf.CropRegionRectangleInfo;
            cbRegionHotkeyInfo.Checked = Engine.conf.CropRegionHotkeyInfo;

            cbCropDynamicCrosshair.Checked = Engine.conf.CropDynamicCrosshair;
            nudCropCrosshairInterval.Value = Engine.conf.CropInterval;
            nudCropCrosshairStep.Value = Engine.conf.CropStep;
            nudCrosshairLineCount.Value = Engine.conf.CrosshairLineCount;
            nudCrosshairLineSize.Value = Engine.conf.CrosshairLineSize;
            pbCropCrosshairColor.BackColor = XMLSettings.DeserializeColor(Engine.conf.CropCrosshairColor);
            cbCropShowBigCross.Checked = Engine.conf.CropShowBigCross;
            cbCropShowMagnifyingGlass.Checked = Engine.conf.CropShowMagnifyingGlass;

            cbShowCropRuler.Checked = Engine.conf.CropShowRuler;
            cbCropDynamicBorderColor.Checked = Engine.conf.CropDynamicBorderColor;
            nudCropRegionInterval.Value = Engine.conf.CropRegionInterval;
            nudCropRegionStep.Value = Engine.conf.CropRegionStep;
            nudCropHueRange.Value = Engine.conf.CropHueRange;
            pbCropBorderColor.BackColor = XMLSettings.DeserializeColor(Engine.conf.CropBorderColor);
            nudCropBorderSize.Value = Engine.conf.CropBorderSize;
            cbCropShowGrids.Checked = Engine.conf.CropShowGrids;

            // Selected Window
            if (cbSelectedWindowStyle.Items.Count == 0)
            {
                cbSelectedWindowStyle.Items.AddRange(typeof(RegionStyles).GetDescriptions());
            }

            cbSelectedWindowStyle.SelectedIndex = (int)Engine.conf.SelectedWindowRegionStyles;
            cbSelectedWindowRectangleInfo.Checked = Engine.conf.SelectedWindowRectangleInfo;
            cbSelectedWindowRuler.Checked = Engine.conf.SelectedWindowRuler;
            pbSelectedWindowBorderColor.BackColor = XMLSettings.DeserializeColor(Engine.conf.SelectedWindowBorderColor);
            nudSelectedWindowBorderSize.Value = Engine.conf.SelectedWindowBorderSize;
            cbSelectedWindowDynamicBorderColor.Checked = Engine.conf.SelectedWindowDynamicBorderColor;
            nudSelectedWindowRegionInterval.Value = Engine.conf.SelectedWindowRegionInterval;
            nudSelectedWindowRegionStep.Value = Engine.conf.SelectedWindowRegionStep;
            nudSelectedWindowHueRange.Value = Engine.conf.SelectedWindowHueRange;
            chkSelectedWindowCaptureObjects.Checked = Engine.conf.SelectedWindowCaptureObjects;

            // Active Window        
            chkActiveWindowPreferDWM.Checked = Engine.conf.ActiveWindowPreferDWM;
            chkSelectedWindowCleanBackground.Checked = Engine.conf.ActiveWindowClearBackground;
            chkSelectedWindowCleanTransparentCorners.Checked = Engine.conf.ActiveWindowCleanTransparentCorners;
            chkSelectedWindowIncludeShadow.Checked = Engine.conf.ActiveWindowIncludeShadows;
            chkActiveWindowTryCaptureChilds.Checked = Engine.conf.ActiveWindowTryCaptureChilds;
            chkSelectedWindowShowCheckers.Checked = Engine.conf.ActiveWindowShowCheckers;
            cbActiveWindowGDIFreezeWindow.Checked = Engine.conf.ActiveWindowGDIFreezeWindow;

            // Interaction
            nudFlashIconCount.Value = Engine.conf.FlashTrayCount;
            chkCaptureFallback.Checked = Engine.conf.CaptureEntireScreenOnError;
            cbShowPopup.Checked = Engine.conf.ShowBalloonTip;
            chkBalloonTipOpenLink.Checked = Engine.conf.BalloonTipOpenLink;
            cbShowUploadDuration.Checked = Engine.conf.ShowUploadDuration;
            cbCompleteSound.Checked = Engine.conf.CompleteSound;
            cbCloseDropBox.Checked = Engine.conf.CloseDropBox;
            cbCloseQuickActions.Checked = Engine.conf.CloseQuickActions;

            // Watermark
            if (cboWatermarkType.Items.Count == 0)
            {
                cboWatermarkType.Items.AddRange(typeof(WatermarkType).GetDescriptions());
            }

            cboWatermarkType.SelectedIndex = (int)Engine.conf.WatermarkMode;
            if (cbWatermarkPosition.Items.Count == 0)
            {
                cbWatermarkPosition.Items.AddRange(typeof(WatermarkPositionType).GetDescriptions());
            }

            cbWatermarkPosition.SelectedIndex = (int)Engine.conf.WatermarkPositionMode;
            nudWatermarkOffset.Value = Engine.conf.WatermarkOffset;
            cbWatermarkAddReflection.Checked = Engine.conf.WatermarkAddReflection;
            cbWatermarkAutoHide.Checked = Engine.conf.WatermarkAutoHide;

            txtWatermarkText.Text = Engine.conf.WatermarkText;
            pbWatermarkFontColor.BackColor = XMLSettings.DeserializeColor(Engine.conf.WatermarkFontColor);
            lblWatermarkFont.Text = FontToString();
            nudWatermarkFontTrans.Value = Engine.conf.WatermarkFontTrans;
            trackWatermarkFontTrans.Value = (int)Engine.conf.WatermarkFontTrans;
            nudWatermarkCornerRadius.Value = Engine.conf.WatermarkCornerRadius;
            pbWatermarkGradient1.BackColor = XMLSettings.DeserializeColor(Engine.conf.WatermarkGradient1);
            pbWatermarkGradient2.BackColor = XMLSettings.DeserializeColor(Engine.conf.WatermarkGradient2);
            pbWatermarkBorderColor.BackColor = XMLSettings.DeserializeColor(Engine.conf.WatermarkBorderColor);
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

            // Image Settings

            if (cboFileFormat.Items.Count == 0)
            {
                cboFileFormat.Items.AddRange(typeof(ImageFileFormatType).GetDescriptions());
            }

            cboFileFormat.SelectedIndex = (int)Engine.conf.ImageFileFormat;
            nudImageQuality.Value = Engine.conf.JpgQuality;
            cbGIFQuality.SelectedIndex = (int)Engine.conf.GIFQuality;
            nudSwitchAfter.Value = Engine.conf.SwitchAfter;
            if (cboSwitchFormat.Items.Count == 0)
            {
                cboSwitchFormat.Items.AddRange(typeof(ImageFileFormatType).GetDescriptions());
            }
            cboSwitchFormat.SelectedIndex = (int)Engine.conf.ImageFormatSwitch;

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

            #endregion

            #region Text Uploaders & URL Shorteners

            ///////////////////////////////////
            // Text Uploader Settings
            ///////////////////////////////////

            //            if (Engine.conf.TextUploadersList.Count == 0)
            //            {
            //                Engine.conf.TextUploadersList = new List<TextUploader> { new PastebinUploader(), new Paste2Uploader(), new SlexyUploader() };
            //            }
            foreach (TextDestType etu in Enum.GetValues(typeof(TextDestType)))
            {
                TextUploader tu = Adapter.FindTextUploader(etu.GetDescription());
                if (null != tu)
                {
                    if (!Adapter.FindItemInList(Engine.conf.TextUploadersList, tu.ToString()))
                    {
                        Engine.conf.TextUploadersList.Add(tu);
                    }
                }
            }

            ucTextUploaders.MyCollection.Items.Clear();
            ucDestOptions.cboTextUploaders.Items.Clear();
            foreach (TextUploader textUploader in Engine.conf.TextUploadersList)
            {
                if (textUploader != null)
                {
                    ucTextUploaders.MyCollection.Items.Add(textUploader);
                    ucDestOptions.cboTextUploaders.Items.Add(textUploader);
                }
            }

            if (Adapter.CheckTextUploaders())
            {
                ucTextUploaders.MyCollection.SelectedIndex = Engine.conf.TextUploaderSelected;
                ucDestOptions.cboTextUploaders.SelectedIndex = Engine.conf.TextUploaderSelected;
            }
            else
            {
                ucTextUploaders.MyCollection.SelectedIndex = 0;
                ucDestOptions.cboTextUploaders.SelectedIndex = 0;
            }

            ucDestOptions.cboTextUploaders.Enabled = !Engine.conf.PreferFileUploaderForText;

            ucTextUploaders.Templates.Items.Clear();
            ucTextUploaders.Templates.Items.AddRange(typeof(TextDestType).GetDescriptions());
            ucTextUploaders.Templates.SelectedIndex = 1;

            ///////////////////////////////////
            // URL Shorteners Settings
            ///////////////////////////////////

            //            if (Engine.conf.UrlShortenersList.Count == 0)
            //            {
            //                Engine.conf.UrlShortenersList = new List<TextUploader> { new ThreelyUploader(), new BitlyUploader(),
            //                    new IsgdUploader(), new KlamUploader(), new TinyURLUploader() };
            //            }
            foreach (UrlShortenerType etu in Enum.GetValues(typeof(UrlShortenerType)))
            {
                TextUploader tu = Adapter.FindUrlShortener(etu.GetDescription());
                if (null != tu)
                {
                    if (!Adapter.FindItemInList(Engine.conf.UrlShortenersList, tu.ToString()))
                    {
                        Engine.conf.UrlShortenersList.Add(tu);
                    }
                }
            }

            ucUrlShorteners.MyCollection.Items.Clear();
            ucDestOptions.cboURLShorteners.Items.Clear();
            foreach (TextUploader textUploader in Engine.conf.UrlShortenersList)
            {
                if (textUploader != null)
                {
                    ucUrlShorteners.MyCollection.Items.Add(textUploader);
                    ucDestOptions.cboURLShorteners.Items.Add(textUploader);
                }
            }

            if (Engine.conf.UrlShortenerSelected > -1 && Engine.conf.UrlShortenerSelected < ucUrlShorteners.MyCollection.Items.Count)
            {
                ucUrlShorteners.MyCollection.SelectedIndex = Engine.conf.UrlShortenerSelected;
                ucDestOptions.cboURLShorteners.SelectedIndex = Engine.conf.UrlShortenerSelected;
            }

            ucUrlShorteners.Templates.Items.Clear();
            ucUrlShorteners.Templates.Items.AddRange(typeof(UrlShortenerType).GetDescriptions());
            ucUrlShorteners.Templates.SelectedIndex = 0;

            #endregion

            #region FTP Settings

            if (Engine.conf.FTPAccountList == null || Engine.conf.FTPAccountList.Count == 0)
            {
                FTPSetup(new List<FTPAccount>());
            }
            else
            {
                FTPSetup(Engine.conf.FTPAccountList);
                if (ucFTPAccounts.AccountsList.Items.Count > 0)
                {
                    ucFTPAccounts.AccountsList.SelectedIndex = Engine.conf.FTPSelected;
                }
            }

            chkEnableThumbnail.Checked = Engine.conf.FTPCreateThumbnail;
            txtFTPThumbWidth.Text = Engine.conf.FTPThumbnailWidth.ToString();
            txtFTPThumbHeight.Text = Engine.conf.FTPThumbnailHeight.ToString();
            cbFTPThumbnailCheckSize.Checked = Engine.conf.FTPThumbnailCheckSize;

            #endregion

            #region MindTouch Settings

            ///////////////////////////////////
            // MindTouch Deki Wiki Settings
            ///////////////////////////////////

            if (Engine.conf.DekiWikiAccountList == null || Engine.conf.DekiWikiAccountList.Count == 0)
            {
                DekiWikiSetup(new List<DekiWikiAccount>());
            }
            else
            {
                DekiWikiSetup(Engine.conf.DekiWikiAccountList);
                if (ucMindTouchAccounts.AccountsList.Items.Count > 0)
                {
                    ucMindTouchAccounts.AccountsList.SelectedIndex = Engine.conf.DekiWikiSelected;
                }
            }

            chkDekiWikiForcePath.Checked = Engine.conf.DekiWikiForcePath;

            #endregion

            #region Image Uploaders

            ///////////////////////////////////
            // Image Uploader Settings
            ///////////////////////////////////

            // Twitter 
            ucTwitterAccounts.AccountsList.Items.Clear();
            foreach (TwitterAuthInfo acc in Engine.conf.TwitterAccountsList)
            {
                ucTwitterAccounts.AccountsList.Items.Add(acc);
            }
            if (ucTwitterAccounts.AccountsList.Items.Count > 0)
            {
                ucTwitterAccounts.AccountsList.SelectedIndex = Engine.conf.TwitterAcctSelected;
            }

            // TinyPic

            txtTinyPicShuk.Text = Engine.conf.TinyPicShuk;
            chkRememberTinyPicUserPass.Checked = Engine.conf.RememberTinyPicUserPass;

            // ImageShack

            txtImageShackRegistrationCode.Text = Engine.conf.ImageShackRegistrationCode;
            txtUserNameImageShack.Text = Engine.conf.ImageShackUserName;
            chkPublicImageShack.Checked = Engine.conf.ImageShackShowImagesInPublic;

            // cboTwitPicUploadMode.SelectedIndex = (int)Engine.conf.TwitPicUploadMode;
            cbTwitPicShowFull.Checked = Engine.conf.TwitPicShowFull;
            if (cboTwitPicThumbnailMode.Items.Count == 0)
            {
                cboTwitPicThumbnailMode.Items.AddRange(typeof(TwitPicThumbnailType).GetDescriptions());
            }

            cboTwitPicThumbnailMode.SelectedIndex = (int)Engine.conf.TwitPicThumbnailMode;

            // ImageBam

            txtImageBamApiKey.Text = Engine.conf.ImageBamApiKey;
            txtImageBamSecret.Text = Engine.conf.ImageBamSecret;
            chkImageBamContentNSFW.Checked = Engine.conf.ImageBamContentNSFW;
            if (Engine.conf.ImageBamGallery.Count == 0)
            {
                Engine.conf.ImageBamGallery.Add(string.Empty);
            }

            foreach (string id in Engine.conf.ImageBamGallery)
            {
                lbImageBamGalleries.Items.Add(id);
            }

            if (lbImageBamGalleries.Items.Count > Engine.conf.ImageBamGalleryActive)
            {
                lbImageBamGalleries.SelectedIndex = Engine.conf.ImageBamGalleryActive;
            }
            else
            {
                lbImageBamGalleries.SelectedIndex = 0;
            }

            // Flickr

            pgFlickrAuthInfo.SelectedObject = Engine.conf.FlickrAuthInfo;
            pgFlickrSettings.SelectedObject = Engine.conf.FlickrSettings;
            // btnFlickrOpenImages.Text = string.Format("{0}'s photostream", Engine.conf.FlickrAuthInfo.Username);

            #endregion

            #region File Uploaders

            if (ucDestOptions.cboFileUploaders.Items.Count == 0)
            {
                ucDestOptions.cboFileUploaders.Items.AddRange(typeof(FileUploaderType).GetDescriptions());
            }

            ucDestOptions.cboFileUploaders.SelectedIndex = (int)Engine.conf.FileDestMode;

            // RapidShare

            if (cboRapidShareAcctType.Items.Count == 0)
            {
                cboRapidShareAcctType.Items.AddRange(typeof(RapidShareAcctType).GetDescriptions());
            }

            cboRapidShareAcctType.SelectedIndex = (int)Engine.conf.RapidShareAccountType;
            txtRapidShareCollectorID.Text = Engine.conf.RapidShareCollectorsID;
            txtRapidSharePassword.Text = Engine.conf.RapidSharePassword;
            txtRapidSharePremiumUserName.Text = Engine.conf.RapidSharePremiumUserName;

            // SendSpace

            if (cboSendSpaceAcctType.Items.Count == 0)
            {
                cboSendSpaceAcctType.Items.AddRange(typeof(AcctType).GetDescriptions());
            }

            cboSendSpaceAcctType.SelectedIndex = (int)Engine.conf.SendSpaceAccountType;
            txtSendSpacePassword.Text = Engine.conf.SendSpacePassword;
            txtSendSpaceUserName.Text = Engine.conf.SendSpaceUserName;
            #endregion

            // Others

            cbAutoSwitchFileUploader.Checked = Engine.conf.AutoSwitchFileUploader;
            nudErrorRetry.Value = Engine.conf.ErrorRetryCount;
            cboImageUploadRetryOnTimeout.Checked = Engine.conf.ImageUploadRetryOnTimeout;
            nudUploadDurationLimit.Value = Engine.conf.UploadDurationLimit;

            if (cboUploadMode.Items.Count == 0)
            {
                cboUploadMode.Items.AddRange(typeof(UploadMode).GetDescriptions());
            }

            cboUploadMode.SelectedIndex = (int)Engine.conf.UploadMode;
            chkImageUploadRetryOnFail.Checked = Engine.conf.ImageUploadRetryOnFail;
            chkImageUploadRandomRetryOnFail.Checked = Engine.conf.ImageUploadRandomRetryOnFail;
            cbClipboardTranslate.Checked = Engine.conf.ClipboardTranslate;
            cbAutoTranslate.Checked = Engine.conf.AutoTranslate;
            txtAutoTranslate.Text = Engine.conf.AutoTranslateLength.ToString();
            cbAddFailedScreenshot.Checked = Engine.conf.AddFailedScreenshot;
            cbTinyPicSizeCheck.Checked = Engine.conf.TinyPicSizeCheck;

            // Web Page Upload

            cbWebPageUseCustomSize.Checked = Engine.conf.WebPageUseCustomSize;
            txtWebPageWidth.Text = Engine.conf.WebPageWidth.ToString();
            txtWebPageHeight.Text = Engine.conf.WebPageHeight.ToString();
            cbWebPageAutoUpload.Checked = Engine.conf.WebPageAutoUpload;

            #region Image Editors

            ///////////////////////////////////
            // Image Editors Settings
            ///////////////////////////////////

            chkEditorsEnabled.Checked = Engine.conf.ImageEditorsEnabled;
            tsmEditinImageSoftware.Checked = Engine.conf.ImageEditorsEnabled;

            string mspaint = "Paint";
            Software editor = new Software(Engine.ZSCREEN_IMAGE_EDITOR, string.Empty, false);
            if (Software.Exist(Engine.ZSCREEN_IMAGE_EDITOR))
            {
                editor = Software.GetByName(Engine.ZSCREEN_IMAGE_EDITOR);
            }
            Software paint = new Software(mspaint, Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.System), "mspaint.exe"), false);
            if (Software.Exist(mspaint))
            {
                paint = Software.GetByName(mspaint);
            }

            Engine.conf.ImageEditors.RemoveAll(x => x.Path == string.Empty || x.Name == Engine.ZSCREEN_IMAGE_EDITOR || x.Name == mspaint || !File.Exists(x.Path));

            Engine.conf.ImageEditors.Insert(0, editor);
            if (File.Exists(paint.Path))
            {
                Engine.conf.ImageEditors.Insert(1, paint);
            }

            RegistryMgr.FindImageEditors();

            lbSoftware.Items.Clear();

            foreach (Software app in Engine.conf.ImageEditors)
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

            chkImageEditorAutoSave.Checked = Engine.conf.ImageEditorAutoSave;

            // Text Editors

            if (Engine.conf.TextEditors.Count == 0)
            {
                Engine.conf.TextEditors.Add(new Software("Notepad", Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.System), "notepad.exe"), true));
            }

            #endregion

            #region Options

            chkStartWin.Checked = RegistryMgr.CheckStartWithWindows();
            chkShellExt.Checked = RegistryMgr.CheckShellExt();
            chkOpenMainWindow.Checked = Engine.conf.ShowMainWindow;
            chkShowTaskbar.Checked = Engine.conf.ShowInTaskbar;
            chkShowTaskbar.Enabled = !Engine.conf.Windows7TaskbarIntegration;
            cbShowHelpBalloonTips.Checked = Engine.conf.ShowHelpBalloonTips;
            chkSaveFormSizePosition.Checked = Engine.conf.SaveFormSizePosition;
            cbLockFormSize.Checked = Engine.conf.LockFormSize;
            cbAutoSaveSettings.Checked = Engine.conf.AutoSaveSettings;
            chkWindows7TaskbarIntegration.Checked = CoreHelpers.RunningOnWin7 && Engine.conf.Windows7TaskbarIntegration;
            chkTwitterEnable.Checked = Engine.conf.TwitterEnabled;

            // Monitor Clipboard

            chkMonImages.Checked = Engine.conf.MonitorImages;
            chkMonText.Checked = Engine.conf.MonitorText;
            chkMonFiles.Checked = Engine.conf.MonitorFiles;
            chkMonUrls.Checked = Engine.conf.MonitorUrls;

            #endregion

            gbRoot.Enabled = !Engine.mAppSettings.PreferSystemFolders;
            chkProxyEnable.Checked = Engine.conf.ProxyEnabled;
            ttZScreen.Active = Engine.conf.ShowHelpBalloonTips;

            chkCheckUpdates.Checked = Engine.conf.CheckUpdates;
            chkCheckUpdatesBeta.Checked = Engine.conf.CheckUpdatesBeta;
            nudCacheSize.Value = Engine.conf.ScreenshotCacheSize;
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

            // Naming Conventions
            txtActiveWindow.Text = Engine.conf.ActiveWindowPattern;
            txtEntireScreen.Text = Engine.conf.EntireScreenPattern;
            txtImagesFolderPattern.Text = Engine.conf.SaveFolderPattern;
            nudMaxNameLength.Value = Engine.conf.MaxNameLength;

            // Proxy Settings
            ProxySetup(Engine.conf.ProxyList);
            if (ucProxyAccounts.AccountsList.Items.Count > 0)
            {
                ucProxyAccounts.AccountsList.SelectedIndex = Engine.conf.ProxySelected;
            }

            ///////////////////////////////////
            // Image Uploaders
            ///////////////////////////////////

            lbImageUploader.Items.Clear();
            if (Engine.conf.ImageUploadersList == null)
            {
                Engine.conf.ImageUploadersList = new List<ImageHostingService>();
                LoadImageUploaders(new ImageHostingService());
            }
            else
            {
                List<ImageHostingService> iUploaders = Engine.conf.ImageUploadersList;
                foreach (ImageHostingService iUploader in iUploaders)
                {
                    lbImageUploader.Items.Add(iUploader.Name);
                }

                if (lbImageUploader.Items.Count > 0)
                {
                    lbImageUploader.SelectedIndex = Engine.conf.ImageUploaderSelected;
                }

                if (lbImageUploader.SelectedIndex != -1)
                {
                    LoadImageUploaders(Engine.conf.ImageUploadersList[lbImageUploader.SelectedIndex]);
                }
            }

            #region History

            if (mGuiIsReady)
            {
                nudHistoryMaxItems.Value = Engine.conf.HistoryMaxNumber;
                Loader.Worker.UpdateGuiControlsHistory();
            }
            else
            {
                Loader.Worker2.LoadHistoryItems();
            }

            #endregion

            CheckFormSettings();
        }

        private void tsmiTab_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem tsmi = sender as ToolStripMenuItem;
            tcApp.SelectedTab = tcApp.TabPages[(string)tsmi.Tag];

            BringUpMenu();
            tcApp.Focus();
        }

        private void tsmiDestImages_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem tsmi = sender as ToolStripMenuItem;
            ucDestOptions.cboImageUploaders.SelectedIndex = (int)tsmi.Tag;
        }

        private void tsmiDestFiles_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem tsmi = sender as ToolStripMenuItem;
            ucDestOptions.cboFileUploaders.SelectedIndex = (int)tsmi.Tag;
        }

        private void UpdateGuiControlsPaths()
        {
            Engine.InitializeDefaultFolderPaths();
            txtImagesDir.Text = Engine.ImagesDir;
            txtCacheDir.Text = Engine.CacheDir;
            txtSettingsDir.Text = Engine.SettingsDir;
        }

        private void cbCloseQuickActions_CheckedChanged(object sender, EventArgs e)
        {
            Engine.conf.CloseQuickActions = cbCloseQuickActions.Checked;
        }

        private void exitZScreenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            mClose = true;
            Close();
        }

        private void cbRegionRectangleInfo_CheckedChanged(object sender, EventArgs e)
        {
            Engine.conf.CropRegionRectangleInfo = cbRegionRectangleInfo.Checked;
        }

        private void cbRegionHotkeyInfo_CheckedChanged(object sender, EventArgs e)
        {
            Engine.conf.CropRegionHotkeyInfo = cbRegionHotkeyInfo.Checked;
        }

        private void niTray_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ShowMainWindow();
        }

        private void tsmQuickOptions_Click(object sender, EventArgs e)
        {
            Loader.Worker.ShowQuickOptions();
        }

        private void btnRegCodeImageShack_Click(object sender, EventArgs e)
        {
            Process.Start("http://my.imageshack.us/registration/");
        }

        private void nErrorRetry_ValueChanged(object sender, EventArgs e)
        {
            Engine.conf.ErrorRetryCount = nudErrorRetry.Value;
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
                    if (!Engine.conf.ShowInTaskbar)
                    {
                        this.Hide();
                    }

                    if (Engine.conf.AutoSaveSettings)
                    {
                        WriteSettings();
                    }
                }
                else if (this.WindowState == FormWindowState.Normal)
                {
                    this.ShowInTaskbar = Engine.conf.ShowInTaskbar;
                    this.Refresh();
                }
            }
        }

        private void ZScreen_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!mClose)
            {
                mClose = e.CloseReason != CloseReason.UserClosing; // if Windows shuts down then close by setting mClose = true
            }

            WriteSettings();

            if (!mClose)
            {
                e.Cancel = true; // cancel the Close
                if (Engine.conf.MinimizeOnClose)
                {
                    this.WindowState = FormWindowState.Minimized;
                }
                else if (e.CloseReason == CloseReason.UserClosing)
                {
                    Hide();
                }

                DelayedTrimMemoryUse();
            }
        }

        #region Trim memory
        private System.Timers.Timer timerTrimMemory;
        Object trimMemoryLock = new Object();
        /// <summary>
        /// Trim memory working set after a few seconds, unless this method is called again in the mean time (optimization)
        /// </summary>
        private void DelayedTrimMemoryUse()
        {
            try
            {
                lock (trimMemoryLock)
                {
                    //System.Console.WriteLine("DelayedTrimMemoryUse");
                    if (timerTrimMemory == null)
                    {
                        timerTrimMemory = new System.Timers.Timer();
                        timerTrimMemory.AutoReset = false;
                        timerTrimMemory.Interval = 10000;
                        timerTrimMemory.Elapsed += new System.Timers.ElapsedEventHandler(timerTrimMemory_Elapsed);
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
                FileSystem.AppendDebug("Error in DelayedTrimMemoryUse", ex);
            }
        }

        void timerTrimMemory_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
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
        #endregion

        private void WriteSettings()
        {
            if (mGuiIsReady && Engine.conf.SaveFormSizePosition && this.WindowState == FormWindowState.Normal)
            {
                Engine.conf.WindowLocation = this.Location;
                Engine.conf.WindowSize = this.Size;
            }

            Engine.conf.WindowState = this.WindowState;
            Engine.conf.Write();
            FileSystem.AppendDebug("Settings written to file: " + Engine.mAppSettings.GetSettingsFilePath());
            Loader.Worker.SaveHistoryItems();
        }

        private void RewriteImageEditorsRightClickMenu()
        {
            if (Engine.conf.ImageEditors != null)
            {
                tsmEditinImageSoftware.DropDownDirection = ToolStripDropDownDirection.Right;
                tsmEditinImageSoftware.DropDownItems.Clear();

                List<Software> imgs = Engine.conf.ImageEditors;

                //tsm.TextDirection = ToolStripTextDirection.Horizontal;
                tsmEditinImageSoftware.DropDownDirection = ToolStripDropDownDirection.Right;

                for (int x = 0; x < imgs.Count; x++)
                {
                    ToolStripMenuItem tsm = new ToolStripMenuItem
                    {
                        Tag = x,
                        Text = imgs[x].Name,
                        CheckOnClick = true,
                        Checked = imgs[x].Enabled
                    };
                    tsm.Click += new EventHandler(TrayImageEditorClick);
                    tsmEditinImageSoftware.DropDownItems.Add(tsm);
                }

                tsmEditinImageSoftware.DropDownDirection = ToolStripDropDownDirection.Right;

                //show drop down menu in the correct place if menu is selected
                if (tsmEditinImageSoftware.Selected)
                {
                    tsmEditinImageSoftware.DropDown.Hide();
                    tsmEditinImageSoftware.DropDown.Show();
                }
            }
        }

        private void UpdateGuiEditors(object sender)
        {
            if (mGuiIsReady)
            {
                if (sender.GetType() == lbSoftware.GetType())
                {
                    // the checked state needs to be inversed for some weird reason to get it working properly
                    Engine.conf.ImageEditors[lbSoftware.SelectedIndex].Enabled = !lbSoftware.GetItemChecked(lbSoftware.SelectedIndex);
                    ToolStripMenuItem tsm = tsmEditinImageSoftware.DropDownItems[lbSoftware.SelectedIndex] as ToolStripMenuItem;
                    tsm.Checked = Engine.conf.ImageEditors[lbSoftware.SelectedIndex].Enabled;
                }
                else if (sender.GetType() == typeof(ToolStripMenuItem))
                {
                    ToolStripMenuItem tsm = sender as ToolStripMenuItem;
                    Engine.conf.ImageEditors[(int)tsm.Tag].Enabled = tsm.Checked;
                    lbSoftware.SetItemChecked(lbSoftware.SelectedIndex, tsm.Checked);
                }
            }
        }

        private void TrayImageEditorClick(object sender, EventArgs e)
        {
            ToolStripMenuItem tsm = (ToolStripMenuItem)sender;

            Engine.conf.ImageEditor = GetImageSoftware(tsm.Text);

            if (lbSoftware.Items.IndexOf(tsm.Text) >= 0)
            {
                tsmEditinImageSoftware.DropDown.AutoClose = false;
                mTimerImageEditorMenuClose.Enabled = true;
                lbSoftware.SelectedItem = tsm.Text;
                UpdateGuiEditors(sender);
                mTimerImageEditorMenuClose.Stop();
                mTimerImageEditorMenuClose.Start();
            }
        }

        private void RewriteCustomUploaderRightClickMenu()
        {
            if (Engine.conf.ImageUploadersList != null)
            {
                List<ImageHostingService> lUploaders = Engine.conf.ImageUploadersList;

                ToolStripMenuItem tsmDestCustomHTTP = GetImageDestMenuItem(ImageDestType.CUSTOM_UPLOADER);
                tsmDestCustomHTTP.DropDownDirection = ToolStripDropDownDirection.Right;
                tsmDestCustomHTTP.DropDownItems.Clear();

                ToolStripMenuItem tsm;
                for (int i = 0; i < lUploaders.Count; i++)
                {
                    tsm = new ToolStripMenuItem { CheckOnClick = true, Tag = i, Text = lUploaders[i].Name };
                    tsm.Click += rightClickIHS_Click;
                    tsmDestCustomHTTP.DropDownItems.Add(tsm);
                }

                CheckCorrectMenuItemClicked(ref tsmDestCustomHTTP, Engine.conf.ImageUploaderSelected);

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
            lbImageUploader.SelectedIndex = (int)tsm.Tag;
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

            CheckCorrectMenuItemClicked(ref tsmCopytoClipboardMode, (int)Engine.conf.ClipboardUriMode);
            tsmCopytoClipboardMode.DropDownDirection = ToolStripDropDownDirection.Right;
        }

        private void ClipboardModeClick(object sender, EventArgs e)
        {
            ToolStripMenuItem tsm = (ToolStripMenuItem)sender;
            Engine.conf.ClipboardUriMode = (ClipboardUriType)tsm.Tag;
            CheckCorrectMenuItemClicked(ref tsmCopytoClipboardMode, (int)Engine.conf.ClipboardUriMode);
            cboClipboardTextMode.SelectedIndex = (int)Engine.conf.ClipboardUriMode;
        }

        private void RewriteFTPRightClickMenu()
        {
            if (Engine.conf.FTPAccountList != null)
            {
                List<ToolStripMenuItem> tsmList = new List<ToolStripMenuItem>();
                tsmList.Add(GetImageDestMenuItem(ImageDestType.FTP));
                tsmList.Add(GetFileDestMenuItem(FileUploaderType.FTP));

                foreach (ToolStripMenuItem tsmi in tsmList)
                {
                    tsmi.DropDownDirection = ToolStripDropDownDirection.Right;
                    tsmi.DropDownItems.Clear();
                    List<FTPAccount> accs = Engine.conf.FTPAccountList;
                    ToolStripMenuItem temp;

                    for (int x = 0; x < accs.Count; x++)
                    {
                        temp = new ToolStripMenuItem { Tag = x, CheckOnClick = true, Text = accs[x].Name };
                        temp.Click += rightClickFTPItem_Click;
                        tsmi.DropDownItems.Add(temp);
                    }

                    temp = tsmi;

                    // Check the active ftpUpload account
                    CheckCorrectMenuItemClicked(ref temp, Engine.conf.FTPSelected);
                    tsmi.DropDownDirection = ToolStripDropDownDirection.Right;

                    // Show drop down menu in the correct place if menu is selected
                    if (tsmi.Selected)
                    {
                        tsmi.DropDown.Hide();
                        tsmi.DropDown.Show();
                    }
                }
            }
        }

        private void rightClickFTPItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem tsm = (ToolStripMenuItem)sender;
            ucFTPAccounts.AccountsList.SelectedIndex = (int)tsm.Tag;
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

        /// <summary>
        /// Browse for an Image Editor
        /// </summary>
        /// <returns>Image Editor</returns>
        private Software BrowseImageSoftware()
        {
            Software temp = null;

            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "Executable files (*.exe)|*.exe";

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                temp = new Software();
                temp.Name = Path.GetFileNameWithoutExtension(dlg.FileName);
                temp.Path = dlg.FileName;
            }

            return temp;
        }

        private void tsmSettings_Click(object sender, EventArgs e)
        {
            BringUpMenu();
        }

        private void tsmViewDirectory_Click(object sender, EventArgs e)
        {
            ShowDirectory(FileSystem.GetImagesDir());
        }

        private void ShowDirectory(string dir)
        {
            Process.Start("explorer.exe", dir);
        }

        private void tsmViewRemote_Click(object sender, EventArgs e)
        {
            if (Engine.conf.FTPAccountList.Count > 0)
            {
                new ViewRemote().Show();
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

        private void BringUpMenu()
        {
            Show();
            WindowState = FormWindowState.Normal;
            this.Activate();
            this.BringToFront();
        }

        private void btnBrowseConfig_Click(object sender, EventArgs e)
        {
            ShowDirectory(Engine.SettingsDir);
        }

        private void tsmLic_Click(object sender, EventArgs e)
        {
            FormsMgr.ShowLicense();
        }

        private void chkManualNaming_CheckedChanged(object sender, EventArgs e)
        {
            Engine.conf.ManualNaming = chkManualNaming.Checked;
        }

        private void ZScreen_Shown(object sender, EventArgs e)
        {
            mGuiIsReady = true;
            Engine.zHandle = this.Handle;
            Engine.ClipboardHook();

            if (Engine.conf.ProxyEnabled)
            {
                FileSystem.AppendDebug("Proxy Settings: " + Uploader.ProxySettings.ProxyActive.ToString());
            }

            if (Engine.conf.FirstRun)
            {
                this.Show();
                this.WindowState = FormWindowState.Normal;
                this.Activate();
                this.BringToFront();
            }

            if (Engine.conf.BackupFTPSettings)
            {
                FileSystem.BackupFTPSettings();
            }

            if (Engine.conf.BackupApplicationSettings)
            {
                FileSystem.BackupAppSettings();
            }

            if (Engine.conf.Windows7TaskbarIntegration)
            {
                ZScreen_Windows7onlyTasks();
                if (!Engine.conf.ShowMainWindow && !Engine.conf.FirstRun)
                {
                    this.WindowState = FormWindowState.Minimized;
                    this.ShowInTaskbar = true;
                }
            }

            Engine.ZScreenKeyboardHook = new KeyboardHook();
            Engine.ZScreenKeyboardHook.KeyDownEvent += new KeyEventHandler(Loader.Worker.CheckHotkeys);
            FileSystem.AppendDebug("Keyboard Hook initiated");
            Engine.conf.FirstRun = false;
        }

        private void clipboardUpload_Click(object sender, EventArgs e)
        {
            Loader.Worker.UploadUsingClipboard();
        }

        private void selWindow_Click(object sender, EventArgs e)
        {
            Loader.Worker.StartBw_SelectedWindow();
        }

        private void tsmAboutMain_Click(object sender, EventArgs e)
        {
            FormsMgr.ShowAboutWindow();
        }

        public void cbStartWin_CheckedChanged(object sender, EventArgs e)
        {
            RegistryMgr.SetStartWithWindows(chkStartWin.Checked);
        }

        private void nudFlashIconCount_ValueChanged(object sender, EventArgs e)
        {
            Engine.conf.FlashTrayCount = nudFlashIconCount.Value;
        }

        private void nudCacheSize_ValueChanged(object sender, EventArgs e)
        {
            Engine.conf.ScreenshotCacheSize = nudCacheSize.Value;
        }

        private void btnSettingsExport_Click(object sender, EventArgs e)
        {
            SaveFileDialog dlg = new SaveFileDialog { Filter = Engine.FILTER_SETTINGS };
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                Engine.conf.Write(dlg.FileName);
            }
        }

        private void btnSettingsImport_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog { Filter = Engine.FILTER_SETTINGS };
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                XMLSettings temp = XMLSettings.Read(dlg.FileName);
                temp.FirstRun = false;
                Engine.conf = temp;
                ZScreen_ConfigGUI();
            }
        }

        private void AddImageSoftwareToList(Software temp)
        {
            if (temp != null)
            {
                Engine.conf.ImageEditors.Add(temp);
                lbSoftware.Items.Add(temp);
                lbSoftware.SelectedIndex = lbSoftware.Items.Count - 1;
                RewriteImageEditorsRightClickMenu();
            }
        }

        private void btnAddImageSoftware_Click(object sender, EventArgs e)
        {
            Software temp = BrowseImageSoftware();
            if (temp != null)
            {
                AddImageSoftwareToList(temp);
            }
        }

        private void btnDeleteImageSoftware_Click(object sender, EventArgs e)
        {
            int sel = lbSoftware.SelectedIndex;

            if (sel != -1)
            {
                Engine.conf.ImageEditors.RemoveAt(sel);

                lbSoftware.Items.RemoveAt(sel);

                if (lbSoftware.Items.Count > 0)
                {
                    lbSoftware.SelectedIndex = (sel > 0) ? (sel - 1) : 0;
                }
            }

            RewriteImageEditorsRightClickMenu();
        }

        private ToolStripMenuItem GetImageDestMenuItem(ImageDestType idt)
        {
            foreach (ToolStripMenuItem tsmi in tsmImageDest.DropDownItems)
            {
                if ((ImageDestType)tsmi.Tag == idt)
                {
                    return tsmi;
                }
            }

            return null;
        }

        private ToolStripMenuItem GetFileDestMenuItem(FileUploaderType fut)
        {
            foreach (ToolStripMenuItem tsmi in tsmFileDest.DropDownItems)
            {
                if ((FileUploaderType)tsmi.Tag == fut)
                {
                    return tsmi;
                }
            }

            return null;
        }

        private void cboImageUploaders_SelectedIndexChanged(object sender, EventArgs e)
        {
            ImageDestType sdt = (ImageDestType)ucDestOptions.cboImageUploaders.SelectedIndex;
            Engine.conf.ImageUploaderType = sdt;
            Engine.conf.PreferFileUploaderForImages = (sdt == ImageDestType.FileUploader);
            cboClipboardTextMode.Enabled = sdt != ImageDestType.CLIPBOARD && sdt != ImageDestType.FILE;

            CheckToolStripMenuItem(tsmImageDest, GetImageDestMenuItem(sdt));
        }

        private void CheckToolStripMenuItem(ToolStripDropDownItem parent, ToolStripMenuItem item)
        {
            foreach (ToolStripMenuItem tsmi in parent.DropDownItems)
            {
                tsmi.Checked = tsmi == item;
            }

            tsmCopytoClipboardMode.Enabled = ucDestOptions.cboImageUploaders.SelectedIndex != (int)ImageDestType.CLIPBOARD &&
                ucDestOptions.cboImageUploaders.SelectedIndex != (int)ImageDestType.FILE;
        }

        private void SetActiveImageSoftware()
        {
            Engine.conf.ImageEditor = Engine.conf.ImageEditors[lbSoftware.SelectedIndex];
        }

        private void ShowImageEditorsSettings()
        {
            if (lbSoftware.SelectedItem != null)
            {
                Software app = GetImageSoftware(lbSoftware.SelectedItem.ToString());
                if (app != null)
                {
                    Engine.conf.ImageEditors[lbSoftware.SelectedIndex].Enabled = lbSoftware.GetItemChecked(lbSoftware.SelectedIndex);
                    pgEditorsImage.SelectedObject = app;
                    pgEditorsImage.Enabled = !app.Protected;
                    btnRemoveImageEditor.Enabled = !app.Protected;
                    gbImageEditorSettings.Visible = app.Name == Engine.ZSCREEN_IMAGE_EDITOR;

                    SetActiveImageSoftware();
                }
            }
        }

        private void lbSoftware_SelectedIndexChanged(object sender, EventArgs e)
        {
            ShowImageEditorsSettings();
        }

        private void cboClipboardTextMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            Engine.conf.ClipboardUriMode = (ClipboardUriType)cboClipboardTextMode.SelectedIndex;
            UpdateClipboardTextTrayMenu();
        }

        private void UpdateClipboardTextTrayMenu()
        {
            foreach (ToolStripMenuItem tsmi in tsmCopytoClipboardMode.DropDownItems)
            {
                tsmi.Checked = false;
            }

            CheckCorrectMenuItemClicked(ref tsmCopytoClipboardMode, (int)Engine.conf.ClipboardUriMode);
        }

        private void txtFileDirectory_TextChanged(object sender, EventArgs e)
        {
            Engine.ImagesDir = txtImagesDir.Text;
        }

        private void cbDeleteLocal_CheckedChanged(object sender, EventArgs e)
        {
            Engine.conf.DeleteLocal = chkDeleteLocal.Checked;
        }

        private void txtActiveWindow_TextChanged(object sender, EventArgs e)
        {
            Engine.conf.ActiveWindowPattern = txtActiveWindow.Text;
            lblActiveWindowPreview.Text = NameParser.Convert(
                new NameParserInfo(NameParserType.ActiveWindow) { IsPreview = true, MaxNameLength = Engine.conf.MaxNameLength });
        }

        private void txtEntireScreen_TextChanged(object sender, EventArgs e)
        {
            Engine.conf.EntireScreenPattern = txtEntireScreen.Text;
            lblEntireScreenPreview.Text = NameParser.Convert(
                new NameParserInfo(NameParserType.EntireScreen) { IsPreview = true, MaxNameLength = Engine.conf.MaxNameLength });
        }

        private void cboFileFormat_SelectedIndexChanged(object sender, EventArgs e)
        {
            Engine.conf.ImageFileFormat = (ImageFileFormatType)cboFileFormat.SelectedIndex;
            Engine.SetImageFormat(ref Engine.zImageFileFormat, Engine.conf.ImageFileFormat);
        }

        private void txtImageQuality_ValueChanged(object sender, EventArgs e)
        {
            Engine.conf.JpgQuality = nudImageQuality.Value;
        }

        private void cboSwitchFormat_SelectedIndexChanged(object sender, EventArgs e)
        {
            Engine.conf.ImageFormatSwitch = (ImageFileFormatType)cboSwitchFormat.SelectedIndex;
            Engine.SetImageFormat(ref Engine.zImageFileFormatSwitch, Engine.conf.ImageFormatSwitch);
        }

        private void txtImageShackRegistrationCode_TextChanged(object sender, EventArgs e)
        {
            if (mGuiIsReady)
            {
                Engine.conf.ImageShackRegistrationCode = txtImageShackRegistrationCode.Text;
            }
        }

        private void cbShowPopup_CheckedChanged(object sender, EventArgs e)
        {
            Engine.conf.ShowBalloonTip = cbShowPopup.Checked;
        }

        private void LoadSettingsDefault()
        {
            Engine.conf = new XMLSettings();
            ZScreen_ConfigGUI();
            Engine.conf.FirstRun = false;
            Engine.conf.Write();
        }

        private void btnDeleteSettings_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you really want to revert settings to default values?", Application.ProductName,
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                LoadSettingsDefault();
            }
        }

        private void cropShot_Click(object sender, EventArgs e)
        {
            Loader.Worker.StartBw_CropShot();
        }

        private void ShowMainWindow()
        {
            if (this.IsHandleCreated)
            {
                this.Show();
                this.WindowState = FormWindowState.Normal;
                NativeMethods.ActivateWindow(this.Handle);
            }
        }

        private void niTray_BalloonTipClicked(object sender, EventArgs e)
        {
            if (Engine.conf.BalloonTipOpenLink)
            {
                try
                {
                    NotifyIcon ni = (NotifyIcon)sender;
                    if (ni.Tag != null)
                    {
                        WorkerTask t = (WorkerTask)niTray.Tag;
                        string cbString;
                        switch (t.Job)
                        {
                            case WorkerTask.Jobs.LANGUAGE_TRANSLATOR:
                                cbString = t.TranslationInfo.Result.TranslatedText;
                                if (!string.IsNullOrEmpty(cbString))
                                {
                                    Clipboard.SetText(cbString);
                                }

                                break;
                            default:
                                switch (t.MyImageUploader)
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
                    FileSystem.AppendDebug("Error while clicking Balloon Tip", ex);
                }
            }
        }

        #region Image MyCollection

        private void btnUploaderAdd_Click(object sender, EventArgs e)
        {
            if (txtUploader.Text != string.Empty)
            {
                ImageHostingService iUploader = GetUploaderFromFields();
                Engine.conf.ImageUploadersList.Add(iUploader);
                lbImageUploader.Items.Add(iUploader.Name);
                lbImageUploader.SelectedIndex = lbImageUploader.Items.Count - 1;
            }
        }

        private void btnUploaderRemove_Click(object sender, EventArgs e)
        {
            if (lbImageUploader.SelectedIndex != -1)
            {
                int selected = lbImageUploader.SelectedIndex;
                Engine.conf.ImageUploadersList.RemoveAt(selected);
                lbImageUploader.Items.RemoveAt(selected);
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
            if (txtArg1.Text != string.Empty)
            {
                lvArguments.Items.Add(txtArg1.Text).SubItems.Add(txtArg2.Text);
                txtArg1.Text = string.Empty;
                txtArg2.Text = string.Empty;
                txtArg1.Focus();
            }
        }

        private void btnArgEdit_Click(object sender, EventArgs e)
        {
            if (lvArguments.SelectedItems.Count > 0 && txtArg1.Text != string.Empty)
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
            if (txtRegexp.Text != string.Empty)
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

                txtRegexp.Text = string.Empty;
                txtRegexp.Focus();
            }
        }

        private void btnRegexpEdit_Click(object sender, EventArgs e)
        {
            if (lvRegexps.SelectedItems.Count > 0 && txtRegexp.Text != string.Empty)
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

        private void lbImageUploader_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lbImageUploader.SelectedIndex != -1)
            {
                LoadImageUploaders(Engine.conf.ImageUploadersList[lbImageUploader.SelectedIndex]);
                Engine.conf.ImageUploaderSelected = lbImageUploader.SelectedIndex;
                RewriteCustomUploaderRightClickMenu();
            }
        }

        private void LoadImageUploaders(ImageHostingService imageUploader)
        {
            txtArg1.Text = string.Empty;
            txtArg2.Text = string.Empty;
            lvArguments.Items.Clear();
            foreach (string[] args in imageUploader.Arguments)
            {
                lvArguments.Items.Add(args[0]).SubItems.Add(args[1]);
            }

            txtUploadURL.Text = imageUploader.UploadURL;
            txtFileForm.Text = imageUploader.FileForm;
            txtRegexp.Text = string.Empty;
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
            if (lbImageUploader.SelectedIndex != -1)
            {
                ImageHostingService iUploader = GetUploaderFromFields();
                iUploader.Name = lbImageUploader.SelectedItem.ToString();
                Engine.conf.ImageUploadersList[lbImageUploader.SelectedIndex] = iUploader;
            }

            RewriteCustomUploaderRightClickMenu();
        }

        private void btnUploadersClear_Click(object sender, EventArgs e)
        {
            LoadImageUploaders(new ImageHostingService());
        }

        private void btUploadersTest_Click(object sender, EventArgs e)
        {
            if (lbImageUploader.SelectedIndex != -1)
            {
                btnUploadersTest.Enabled = false;
                Loader.Worker.StartWorkerScreenshots(WorkerTask.Jobs.CustomUploaderTest);
            }
        }

        private void btnUploaderExport_Click(object sender, EventArgs e)
        {
            if (Engine.conf.ImageUploadersList != null)
            {
                SaveFileDialog dlg = new SaveFileDialog
                {
                    FileName = string.Format("{0}-{1}-uploaders", Application.ProductName, DateTime.Now.ToString("yyyyMMdd")),
                    Filter = Engine.FILTER_IMAGE_HOSTING_SERVICES
                };
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    ImageHostingServiceManager ihsm = new ImageHostingServiceManager
                    {
                        ImageHostingServices = Engine.conf.ImageUploadersList
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
                lbImageUploader.Items.Clear();
                Engine.conf.ImageUploadersList = new List<ImageHostingService>();
                Engine.conf.ImageUploadersList.AddRange(tmp.ImageHostingServices);
                foreach (ImageHostingService iHostingService in Engine.conf.ImageUploadersList)
                {
                    lbImageUploader.Items.Add(iHostingService.Name);
                }
            }
        }

        private void btnUploaderImport_Click(object sender, EventArgs e)
        {
            if (Engine.conf.ImageUploadersList == null)
            {
                Engine.conf.ImageUploadersList = new List<ImageHostingService>();
            }

            OpenFileDialog dlg = new OpenFileDialog { Filter = Engine.FILTER_IMAGE_HOSTING_SERVICES };
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
            OpenSource(UploadManager.GetLastImageUpload(), sType);
        }

        private bool OpenSource(ImageFileManager ifm, ImageFileManager.SourceType sType)
        {
            if (ifm != null)
            {
                string path = ifm.GetSource(Engine.TempDir, sType);
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

            Loader.Worker.mSetHotkeys = true;
            Loader.Worker.mHKSelectedRow = e.RowIndex;

            lblHotkeyStatus.Text = "Press the keys you would like to use... Press enter when done setting all desired Hotkeys.";

            dgvHotkeys.Rows[e.RowIndex].Cells[1].Value = Loader.Worker.GetSelectedHotkeySpecialString() + " <Set Keys>";
        }

        private void UpdateHotkeysDGV()
        {
            dgvHotkeys.Rows.Clear();

            AddHotkey("Entire Screen");
            AddHotkey("Active Window");
            AddHotkey("Crop Shot");
            AddHotkey("Selected Window");
            AddHotkey("Clipboard Upload");
            AddHotkey("Last Crop Shot");
            AddHotkey("Auto Capture");
            AddHotkey("Actions Toolbar");
            AddHotkey("Quick Options");
            AddHotkey("Drop Window");
            AddHotkey("Language Translator");
            AddHotkey("Screen Color Picker");
            AddHotkey("Twitter Client");

            dgvHotkeys.Refresh();
        }

        private void AddHotkey(string descr)
        {
            object userHotKey = Engine.conf.GetFieldValue("Hotkey" + descr.Replace(" ", string.Empty));
            object dfltHotkey = Engine.conf.GetFieldValue("DefaultHotkey" + descr.Replace(" ", string.Empty));

            if (userHotKey != null && userHotKey.GetType() == typeof(Keys))
            {
                dgvHotkeys.Rows.Add(descr, ((Keys)userHotKey).ToSpecialString(), ((Keys)dfltHotkey).ToSpecialString());
            }
        }

        private void dgvHotkeys_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                //txtActiveHelp.Text = dgvHotkeys.Rows[e.RowIndex].Cells[0].Value + ": allows you to ";

                switch (e.RowIndex)
                {
                    case 0: //active window
                        //txtActiveHelp.Text += "capture a window that is currently highlighted and send it your selected destination.";
                        break;
                    case 1: //selected window
                        //txtActiveHelp.Text += "capture a window by selecting a window from the mouse and send it your selected destination.";
                        break;
                    case 2: //entire screen
                        //txtActiveHelp.Text += "capture everything present on your screen including taskbar, start menu, etc and send it your selected destination";
                        break;
                    case 3: //crop shot
                        //txtActiveHelp.Text += "capture a specified region of your screen and send it to your selected destination";
                        break;
                    case 4: //last crop shot
                        //txtActiveHelp.Text += "capture the specified region from crop shot another time";
                        break;
                    case 5: //clipboard upload
                        //txtActiveHelp.Text += "send files from your file system to your selected destination.";
                        break;
                    case 7: // quick options
                        //txtActiveHelp.Text += "quickly select the destination you would like to send images via a small pop up form.";
                        break;
                    case 8: // drop window
                        //txtActiveHelp.Text += "display a Drop Window so can drag and drop image files from Windows Explorer to upload.";
                        break;
                    case 9: // language translator
                        //txtActiveHelp.Text += "translate the text that is in your clipboard from one language to another. See HTTP -> Language Translator for settings.";
                        break;
                }
            }
        }

        private void dgvHotkeys_Leave(object sender, EventArgs e)
        {
            Loader.Worker.QuitSettingHotkeys();
        }

        private void ZScreen_Leave(object sender, EventArgs e)
        {
            Loader.Worker.QuitSettingHotkeys();
        }

        private void dgvHotkeys_MouseLeave(object sender, EventArgs e)
        {
            Loader.Worker.QuitSettingHotkeys();
        }

        private void btnRegCodeTinyPic_Click(object sender, EventArgs e)
        {
            string shuk = Adapter.GetTinyPicShuk();
            if (null != shuk)
            {
                txtTinyPicShuk.Text = shuk;
            }
            this.BringToFront();
        }

        private void txtTinyPicShuk_TextChanged(object sender, EventArgs e)
        {
            Engine.conf.TinyPicShuk = txtTinyPicShuk.Text;
        }

        private void CheckFormSettings()
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

        private void cbCropStyle_SelectedIndexChanged(object sender, EventArgs e)
        {
            Engine.conf.CropRegionStyles = (RegionStyles)cbCropStyle.SelectedIndex;
        }

        private void pbCropBorderColor_Click(object sender, EventArgs e)
        {
            SelectColor((PictureBox)sender, ref Engine.conf.CropBorderColor);
        }

        private void nudCropBorderSize_ValueChanged(object sender, EventArgs e)
        {
            Engine.conf.CropBorderSize = nudCropBorderSize.Value;
        }

        private void llblBugReports_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(Engine.URL_ISSUES);
        }

        private void cbCompleteSound_CheckedChanged(object sender, EventArgs e)
        {
            Engine.conf.CompleteSound = cbCompleteSound.Checked;
        }

        private void cbCheckUpdates_CheckedChanged(object sender, EventArgs e)
        {
            Engine.conf.CheckUpdates = chkCheckUpdates.Checked;
        }

        private void txtActiveHelp_LinkClicked(object sender, LinkClickedEventArgs e)
        {
            Process.Start(e.LinkText);
        }

        private void SetClipboardFromHistory(ClipboardUriType type)
        {
            if (lbHistory.SelectedIndex != -1)
            {
                List<string> listUrls = new List<string>();
                for (int i = 0; i < lbHistory.SelectedItems.Count; i++)
                {
                    HistoryItem hi = (HistoryItem)lbHistory.SelectedItems[i];
                    string url = string.Empty;
                    if (hi.ScreenshotManager != null)
                    {
                        url = hi.ScreenshotManager.GetUrlByType(type);
                        if (!string.IsNullOrEmpty(url))
                        {
                            listUrls.Add(url);
                        }
                    }
                    if (0 == listUrls.Count && type == ClipboardUriType.FULL_TINYURL)
                    {
                        url = Adapter.ShortenURL(hi.RemotePath);
                        if (!string.IsNullOrEmpty(url))
                        {
                            listUrls.Add(url);
                        }
                    }
                }

                if (listUrls.Count > 0)
                {
                    if (Engine.conf.HistoryReverseList)
                    {
                        listUrls.Reverse();
                    }

                    StringBuilder sb = new StringBuilder();
                    if (Engine.conf.HistoryAddSpace)
                    {
                        sb.AppendLine();
                    }

                    for (int i = 0; i < listUrls.Count; i++)
                    {
                        sb.Append(listUrls[i]);
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

        private void lbHistory_DoubleClick(object sender, EventArgs e)
        {
            if (lbHistory.SelectedIndex > -1)
            {
                HistoryItem hi = (HistoryItem)lbHistory.SelectedItem;
                if (!string.IsNullOrEmpty(hi.RemotePath))
                {
                    Process.Start(((HistoryItem)lbHistory.SelectedItem).RemotePath);
                }
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
                    bool checkImage = FileSystem.IsValidImageFile(hi.LocalPath); //GraphicsMgr.IsValidImage(hi.LocalPath);
                    bool checkText = FileSystem.IsValidTextFile(hi.LocalPath);
                    bool checkWebpage = FileSystem.IsValidWebpageFile(hi.LocalPath) || (checkImage && Engine.conf.PreferBrowserForImages) ||
                        (checkText && Engine.conf.PreferBrowserForText);
                    bool checkBinary = !checkImage && !checkText && !checkWebpage;

                    historyBrowser.Visible = checkWebpage;
                    pbPreview.Visible = checkImage || (!checkText && checkRemote) && !checkWebpage || checkBinary;
                    txtPreview.Visible = checkText && !checkWebpage;

                    tsmCopyCbHistory.Enabled = checkRemote;
                    cmsHistory.Enabled = checkLocal;

                    tsmCopyCbHistory.Enabled = browseURLToolStripMenuItem.Enabled = checkRemote;
                    copyImageToolStripMenuItem.Enabled = openLocalFileToolStripMenuItem.Enabled = deleteToolStripMenuItem.Enabled = checkLocal;
                    openSourceToolStripMenuItem.Enabled = hi.ScreenshotManager != null;

                    btnHistoryCopyLink.Enabled = checkRemote;
                    btnHistoryBrowseURL.Enabled = checkRemote;
                    btnHistoryOpenLocalFile.Enabled = checkLocal;
                    btnHistoryCopyImage.Enabled = checkImage;

                    if (checkWebpage)
                    {
                        // preview text from remote path because otherwise Notepad is gonna open
                        string url = (checkText ? (checkRemote ? hi.RemotePath : hi.LocalPath) : hi.LocalPath);
                        historyBrowser.Navigate(url);
                    }
                    else if (checkImage)
                    {
                        if (checkLocal)
                        {
                            thumbnailCacher.LoadImage(hi.LocalPath);
                        }
                        else if (checkRemote)
                        {
                            thumbnailCacher.LoadImage(hi.RemotePath);
                        }
                    }
                    else if (checkText)
                    {
                        txtPreview.Text = File.ReadAllText(hi.LocalPath);
                    }
                    else if (checkBinary)
                    {
                        pbPreview.Image = Resources.explorer;
                    }

                    txtHistoryLocalPath.Text = hi.LocalPath;
                    txtHistoryRemotePath.Text = hi.RemotePath;
                    lblHistoryScreenshot.Text = hi.Description;
                }

                if (Engine.conf.HistoryShowTooltips && hi != null)
                {
                    ttZScreen.SetToolTip(lbHistory, hi.GetStatistics());
                    ttZScreen.SetToolTip(pbPreview, hi.GetStatistics());
                }
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
                if (File.Exists(hi.LocalPath))
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

        private void txtWatermarkText_TextChanged(object sender, EventArgs e)
        {
            Engine.conf.WatermarkText = txtWatermarkText.Text;
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
            var variables = Enum.GetValues(typeof(ReplacementVariables)).Cast<ReplacementVariables>().
                Select(x => new { Name = NameParser.Prefix + Enum.GetName(typeof(ReplacementVariables), x), Description = x.GetDescription() });

            foreach (var variable in variables)
            {
                ToolStripMenuItem tsi = new ToolStripMenuItem { Text = string.Format("{0} - {1}", variable.Name, variable.Description), Tag = variable.Name };
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
            if (oldPos > 0 && txtWatermarkText.Text[txtWatermarkText.SelectionStart - 1] == NameParser.Prefix[0])
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
            Engine.conf.ShowCursor = cbShowCursor.Checked;
        }

        private void btnWatermarkFont_Click(object sender, EventArgs e)
        {
            DialogResult result = Adapter.ShowFontDialog();
            if (result == DialogResult.OK)
            {
                pbWatermarkFontColor.BackColor = XMLSettings.DeserializeColor(Engine.conf.WatermarkFontColor);
                lblWatermarkFont.Text = FontToString();
                TestWatermark();
            }
        }

        private string FontToString()
        {
            return FontToString(XMLSettings.DeserializeFont(Engine.conf.WatermarkFont),
                 XMLSettings.DeserializeColor(Engine.conf.WatermarkFontColor));
        }

        private string FontToString(Font font, Color color)
        {
            return "Name: " + font.Name + " - Size: " + font.Size + " - Style: " + font.Style + " - Color: " +
                color.R + "," + color.G + "," + color.B;
        }

        private void nudWatermarkOffset_ValueChanged(object sender, EventArgs e)
        {
            Engine.conf.WatermarkOffset = nudWatermarkOffset.Value;
            TestWatermark();
        }

        private void nudWatermarkBackTrans_ValueChanged(object sender, EventArgs e)
        {
            Engine.conf.WatermarkBackTrans = nudWatermarkBackTrans.Value;
            trackWatermarkBackgroundTrans.Value = (int)nudWatermarkBackTrans.Value;
        }

        private void entireScreenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Thread.Sleep(300);
            Loader.Worker.StartBW_EntireScreen();
        }

        private void selectedWindowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Thread.Sleep(300);
            Loader.Worker.StartBw_SelectedWindow();
        }

        private void rectangularRegionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Thread.Sleep(300);
            Loader.Worker.StartBw_CropShot();
        }

        private void lastRectangularRegionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Thread.Sleep(300);
            Loader.Worker.StartBW_LastCropShot();
        }

        private void tsmDropWindow_Click(object sender, EventArgs e)
        {
            Loader.Worker.ShowDropWindow();
        }

        private void tsmUploadFromClipboard_Click(object sender, EventArgs e)
        {
            Loader.Worker.UploadUsingClipboard();
        }

        private void languageTranslatorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Loader.Worker.StartWorkerTranslator();
        }

        private void screenColorPickerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Loader.Worker.ScreenColorPicker();
        }

        private void pbWatermarkGradient1_Click(object sender, EventArgs e)
        {
            SelectColor((PictureBox)sender, ref Engine.conf.WatermarkGradient1);
            TestWatermark();
        }

        private void pbWatermarkGradient2_Click(object sender, EventArgs e)
        {
            SelectColor((PictureBox)sender, ref Engine.conf.WatermarkGradient2);
            TestWatermark();
        }

        private void pbWatermarkBorderColor_Click(object sender, EventArgs e)
        {
            SelectColor((PictureBox)sender, ref Engine.conf.WatermarkBorderColor);
            TestWatermark();
        }

        private void TestWatermark()
        {
            ComponentResourceManager resources = new ComponentResourceManager(typeof(ZScreen));
            using (Bitmap bmp = new Bitmap((Image)resources.GetObject("pbLogo.Image")).
                Clone(new Rectangle(62, 33, 199, 140), PixelFormat.Format32bppArgb))
            {
                Bitmap bmp2 = new Bitmap(pbWatermarkShow.ClientRectangle.Width, pbWatermarkShow.ClientRectangle.Height);
                Graphics g = Graphics.FromImage(bmp2);
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.DrawImage(bmp, new Rectangle(0, 0, pbWatermarkShow.ClientRectangle.Width, pbWatermarkShow.ClientRectangle.Height));
                pbWatermarkShow.Image = ImageEffects.ApplyWatermark(bmp2);
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
            SelectColor((PictureBox)sender, ref Engine.conf.WatermarkFontColor);
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
            Engine.conf.WatermarkPositionMode = (WatermarkPositionType)cbWatermarkPosition.SelectedIndex;
            TestWatermark();
        }

        private void nudWatermarkFontTrans_ValueChanged(object sender, EventArgs e)
        {
            Engine.conf.WatermarkFontTrans = nudWatermarkFontTrans.Value;
            trackWatermarkFontTrans.Value = (int)nudWatermarkFontTrans.Value;
        }

        private void nudWatermarkCornerRadius_ValueChanged(object sender, EventArgs e)
        {
            Engine.conf.WatermarkCornerRadius = nudWatermarkCornerRadius.Value;
            TestWatermark();
        }

        private void cbWatermarkGradientType_SelectedIndexChanged(object sender, EventArgs e)
        {
            Engine.conf.WatermarkGradientType = (LinearGradientMode)cbWatermarkGradientType.SelectedIndex;
            TestWatermark();
        }

        private void CopyImageFromHistory()
        {
            if (lbHistory.SelectedIndex != -1)
            {
                HistoryItem hi = (HistoryItem)lbHistory.SelectedItem;
                if (!string.IsNullOrEmpty(hi.LocalPath))
                {
                    using (Image img = GraphicsMgr.GetImageSafely(hi.LocalPath))
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
            DeleteHistoryFiles();
        }

        private void DeleteHistoryFiles()
        {
            if (lbHistory.SelectedIndex != -1)
            {
                StringBuilder sbFiles = new StringBuilder();
                List<HistoryItem> temp = new List<HistoryItem>();
                foreach (HistoryItem hi in lbHistory.SelectedItems)
                {
                    temp.Add(hi);
                    sbFiles.AppendLine(hi.LocalPath);
                }

                string msg = "Are you sure you want to delete ";
                if (temp.Count > 10)
                {
                    msg += temp.Count + " files?";
                }
                else
                {
                    msg += "the following files:\n\n" + sbFiles.ToString();
                }

                DialogResult strAns = MessageBox.Show(msg, Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                foreach (HistoryItem hi in temp)
                {
                    lbHistory.Items.Remove(hi);
                    if (strAns == DialogResult.Yes)
                    {
                        Adapter.DeleteFile(hi.LocalPath);
                    }
                }

                if (lbHistory.Items.Count > 0)
                {
                    lbHistory.SelectedIndex = 0;
                }
            }
        }

        private void btnViewLocalDirectory_Click(object sender, EventArgs e)
        {
            ShowDirectory(FileSystem.GetImagesDir());
        }

        private void btnViewRemoteDirectory_Click(object sender, EventArgs e)
        {
            ShowDirectory(Engine.CacheDir);
        }

        private void cbOpenMainWindow_CheckedChanged(object sender, EventArgs e)
        {
            Engine.conf.ShowMainWindow = chkOpenMainWindow.Checked;
        }

        private void cbShowTaskbar_CheckedChanged(object sender, EventArgs e)
        {
            Engine.conf.ShowInTaskbar = chkShowTaskbar.Checked;
            if (mGuiIsReady)
            {
                this.ShowInTaskbar = Engine.conf.ShowInTaskbar;
            }
        }

        private void llWebsite_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(Engine.URL_WEBSITE);
        }

        private void llProjectPage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(Engine.URL_PROJECTPAGE);
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
            Engine.conf.BalloonTipOpenLink = chkBalloonTipOpenLink.Checked;
        }

        private void cmVersionHistory_Click(object sender, EventArgs e)
        {
            FormsMgr.ShowVersionHistory();
        }

        #region Language Translator

        private void btnTranslate_Click(object sender, EventArgs e)
        {
            btnTranslateMethod();
        }

        private void btnTranslateMethod()
        {
            Loader.Worker.StartBW_LanguageTranslator(new GoogleTranslate.TranslationInfo(txtTranslateText.Text,
                GoogleTranslate.FindLanguage(Engine.conf.FromLanguage, ZScreen.mGTranslator.LanguageOptions.SourceLangList),
                GoogleTranslate.FindLanguage(Engine.conf.ToLanguage, ZScreen.mGTranslator.LanguageOptions.TargetLangList)));
        }

        private void cbFromLanguage_SelectedIndexChanged(object sender, EventArgs e)
        {
            Engine.conf.FromLanguage = ZScreen.mGTranslator.LanguageOptions.SourceLangList[cbFromLanguage.SelectedIndex].Value;
        }

        private void cbToLanguage_SelectedIndexChanged(object sender, EventArgs e)
        {
            Engine.conf.ToLanguage = ZScreen.mGTranslator.LanguageOptions.TargetLangList[cbToLanguage.SelectedIndex].Value;
        }

        private void cbClipboardTranslate_CheckedChanged(object sender, EventArgs e)
        {
            Engine.conf.ClipboardTranslate = cbClipboardTranslate.Checked;
        }

        private void txtTranslateText_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                btnTranslateMethod();
            }
        }

        #endregion

        private void cboUploadMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            Engine.conf.UploadMode = (UploadMode)cboUploadMode.SelectedIndex;
            gbImageShack.Enabled = Engine.conf.UploadMode == UploadMode.API;
            gbTinyPic.Enabled = Engine.conf.UploadMode == UploadMode.API;
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (lbHistory.SelectedItems.Count > 0)
            {
                HistoryItem hi = lbHistory.SelectedItem as HistoryItem;
                if (hi != null)
                {
                    OpenSource(hi.ScreenshotManager, ImageFileManager.SourceType.TEXT);
                }
            }
        }

        private void openSourceInDefaultWebBrowserHTMLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (lbHistory.SelectedItems.Count > 0)
            {
                HistoryItem hi = lbHistory.SelectedItem as HistoryItem;
                if (hi != null)
                {
                    OpenSource(hi.ScreenshotManager, ImageFileManager.SourceType.HTML);
                }
            }
        }

        private void copySourceToClipboardStringToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (lbHistory.SelectedItems.Count > 0)
            {
                HistoryItem hi = lbHistory.SelectedItem as HistoryItem;
                if (hi != null)
                {
                    OpenSource(hi.ScreenshotManager, ImageFileManager.SourceType.STRING);
                }
            }
        }

        private void cmsRetryUpload_Click(object sender, EventArgs e)
        {
            Loader.Worker.HistoryRetryUpload((HistoryItem)lbHistory.SelectedItem);
        }

        private void pbHistoryThumb_Click(object sender, EventArgs e)
        {
            HistoryItem hi = (HistoryItem)lbHistory.SelectedItem;
            if (hi != null && File.Exists(hi.LocalPath) && GraphicsMgr.IsValidImage(hi.LocalPath))
            {
                using (ShowScreenshot sc = new ShowScreenshot(Image.FromFile(hi.LocalPath)))
                {
                    sc.ShowDialog();
                }
            }
        }

        private void btnCopyStats_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtDebugInfo.Text))
            {
                Clipboard.SetText(txtDebugInfo.Text);
            }
        }

        private void cbImageUploadRetry_CheckedChanged(object sender, EventArgs e)
        {
            Engine.conf.ImageUploadRetryOnFail = chkImageUploadRetryOnFail.Checked;
        }

        private void DekiWikiSetup(IEnumerable<DekiWikiAccount> accs)
        {
            if (accs != null)
            {
                ucMindTouchAccounts.AccountsList.Items.Clear();
                Engine.conf.DekiWikiAccountList = new List<DekiWikiAccount>();
                Engine.conf.DekiWikiAccountList.AddRange(accs);
                foreach (DekiWikiAccount acc in Engine.conf.DekiWikiAccountList)
                {
                    ucMindTouchAccounts.AccountsList.Items.Add(acc);
                }
            }
        }

        private void ProxySetup(IEnumerable<ProxyInfo> accs)
        {
            if (accs != null)
            {
                ucProxyAccounts.AccountsList.Items.Clear();
                Engine.conf.ProxyList = new List<ProxyInfo>();
                Engine.conf.ProxyList.AddRange(accs);
                foreach (ProxyInfo acc in Engine.conf.ProxyList)
                {
                    ucProxyAccounts.AccountsList.Items.Add(acc);
                }
            }
        }

        #region FTP Accounts

        private void FTPSetup(IEnumerable<FTPAccount> accs)
        {
            if (accs != null)
            {
                ucFTPAccounts.AccountsList.Items.Clear();
                Engine.conf.FTPAccountList = new List<FTPAccount>();
                Engine.conf.FTPAccountList.AddRange(accs);
                foreach (FTPAccount acc in Engine.conf.FTPAccountList)
                {
                    ucFTPAccounts.AccountsList.Items.Add(acc);
                }
            }
        }

        private void FTPAccountRemoveButton_Click(object sender, EventArgs e)
        {
            int sel = ucFTPAccounts.AccountsList.SelectedIndex;
            if (ucFTPAccounts.RemoveItem(sel) == true)
            {
                Engine.conf.FTPAccountList.RemoveAt(sel);
            }
        }

        private void MindTouchAccountRemoveButton_Click(object sender, EventArgs e)
        {
            int sel = ucMindTouchAccounts.AccountsList.SelectedIndex;
            if (ucMindTouchAccounts.RemoveItem(sel) == true)
            {
                Engine.conf.DekiWikiAccountList.RemoveAt(sel);
            }
        }

        private void FTPAccountsList_SelectedIndexChanged(object sender, EventArgs e)
        {
            int sel = ucFTPAccounts.AccountsList.SelectedIndex;
            Engine.conf.FTPSelected = sel;

            if (Adapter.CheckFTPAccounts())
            {
                FTPAccount acc = Engine.conf.FTPAccountList[sel];
                ucFTPAccounts.SettingsGrid.SelectedObject = acc;
                RewriteFTPRightClickMenu();
            }
        }

        private void MindTouchAccountsList_SelectedIndexChanged(object sender, EventArgs e)
        {
            int sel = ucMindTouchAccounts.AccountsList.SelectedIndex;
            Engine.conf.DekiWikiSelected = sel;
            if (Engine.conf.DekiWikiAccountList != null && sel != -1 && sel < Engine.conf.DekiWikiAccountList.Count && Engine.conf.DekiWikiAccountList[sel] != null)
            {
                DekiWikiAccount acc = Engine.conf.DekiWikiAccountList[sel];
                ucMindTouchAccounts.SettingsGrid.SelectedObject = acc;
                // RewriteFTPRightClickMenu();
            }
        }

        private void FTPAccountAddButton_Click(object sender, EventArgs e)
        {
            FTPAccount acc = new FTPAccount("New Account");
            Engine.conf.FTPAccountList.Add(acc);
            ucFTPAccounts.AccountsList.Items.Add(acc);
            ucFTPAccounts.AccountsList.SelectedIndex = ucFTPAccounts.AccountsList.Items.Count - 1;
        }

        private void MindTouchAccountAddButton_Click(object sender, EventArgs e)
        {
            DekiWikiAccount acc = new DekiWikiAccount("New Account");
            Engine.conf.DekiWikiAccountList.Add(acc);
            ucMindTouchAccounts.AccountsList.Items.Add(acc);
            ucMindTouchAccounts.AccountsList.SelectedIndex = ucMindTouchAccounts.AccountsList.Items.Count - 1;
        }

        private void btnExportAccounts_Click(object sender, EventArgs e)
        {
            if (Engine.conf.FTPAccountList != null)
            {
                SaveFileDialog dlg = new SaveFileDialog
                {
                    FileName = string.Format("{0}-{1}-accounts", Application.ProductName, DateTime.Now.ToString("yyyyMMdd")),
                    Filter = Engine.FILTER_ACCOUNTS
                };
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    FTPAccountManager fam = new FTPAccountManager(Engine.conf.FTPAccountList);
                    fam.Save(dlg.FileName);
                }
            }
        }

        private void btnAccsImport_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog { Filter = Engine.FILTER_ACCOUNTS };
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                FTPAccountManager fam = FTPAccountManager.Read(dlg.FileName);
                FTPSetup(fam.FTPAccounts);
            }
        }

        private ProxyInfo GetSelectedProxy()
        {
            ProxyInfo acc = null;
            if (ucProxyAccounts.AccountsList.SelectedIndex != -1 && Engine.conf.ProxyList.Count >= ucProxyAccounts.AccountsList.Items.Count)
            {
                acc = Engine.conf.ProxyList[ucProxyAccounts.AccountsList.SelectedIndex];
            }

            return acc;
        }

        private FTPAccount GetSelectedFTP()
        {
            FTPAccount acc = null;
            if (Adapter.CheckFTPAccounts())
            {
                acc = Engine.conf.FTPAccountList[Engine.conf.FTPSelected];
            }

            return acc;
        }

        private DekiWikiAccount GetSelectedDekiWiki()
        {
            DekiWikiAccount acc = null;
            if (Adapter.CheckDekiWikiAccounts())
            {
                acc = Engine.conf.DekiWikiAccountList[Engine.conf.DekiWikiSelected];
            }

            return acc;
        }

        private void FTPAccountTestButton_Click(object sender, EventArgs e)
        {
            Loader.Worker2.TestFTPAccountAsync(GetSelectedFTP());
        }

        private void MindTouchAccountTestButton_Click(object sender, EventArgs e)
        {
            DekiWikiAccount acc = GetSelectedDekiWiki();
            if (acc != null)
            {
                Adapter.TestDekiWikiAccount(acc);
            }
        }

        private void chkEnableThumbnail_CheckedChanged(object sender, EventArgs e)
        {
            Engine.conf.FTPCreateThumbnail = chkEnableThumbnail.Checked;
        }

        private void chkAutoSwitchFTP_CheckedChanged(object sender, EventArgs e)
        {
            Engine.conf.AutoSwitchFileUploader = cbAutoSwitchFileUploader.Checked;
        }

        #endregion

        private void cbSelectedWindowRectangleInfo_CheckedChanged(object sender, EventArgs e)
        {
            Engine.conf.SelectedWindowRectangleInfo = cbSelectedWindowRectangleInfo.Checked;
        }

        private void pbSelectedWindowBorderColor_Click(object sender, EventArgs e)
        {
            SelectColor((PictureBox)sender, ref Engine.conf.SelectedWindowBorderColor);
        }

        private void nudSelectedWindowBorderSize_ValueChanged(object sender, EventArgs e)
        {
            Engine.conf.SelectedWindowBorderSize = nudSelectedWindowBorderSize.Value;
        }

        private void btnCheckUpdate_Click(object sender, EventArgs e)
        {
            Loader.Worker2.CheckUpdates();
        }

        private void cbAddFailedScreenshot_CheckedChanged(object sender, EventArgs e)
        {
            Engine.conf.AddFailedScreenshot = cbAddFailedScreenshot.Checked;
        }

        private void cbShowUploadDuration_CheckedChanged(object sender, EventArgs e)
        {
            Engine.conf.ShowUploadDuration = cbShowUploadDuration.Checked;
        }

        /// <summary>
        /// Searches for an Image Software in settings and returns it
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        private static Software GetImageSoftware(string name)
        {
            foreach (Software app in Engine.conf.ImageEditors)
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

        private void cbSelectedWindowStyle_SelectedIndexChanged(object sender, EventArgs e)
        {
            Engine.conf.SelectedWindowRegionStyles = (RegionStyles)cbSelectedWindowStyle.SelectedIndex;
        }

        private void nudCropGridSize_ValueChanged(object sender, EventArgs e)
        {
            Engine.conf.CropGridSize.Width = (int)nudCropGridWidth.Value;
        }

        private void nudCropGridHeight_ValueChanged(object sender, EventArgs e)
        {
            Engine.conf.CropGridSize.Height = (int)nudCropGridHeight.Value;
        }

        private void cbCropShowGrids_CheckedChanged(object sender, EventArgs e)
        {
            Engine.conf.CropShowGrids = cbCropShowGrids.Checked;
        }

        private void cbAddSpace_CheckedChanged(object sender, EventArgs e)
        {
            Engine.conf.HistoryAddSpace = cbHistoryAddSpace.Checked;
        }

        private void cbReverse_CheckedChanged(object sender, EventArgs e)
        {
            Engine.conf.HistoryReverseList = cbHistoryReverseList.Checked;
        }

        private void nudHistoryMaxItems_ValueChanged(object sender, EventArgs e)
        {
            Engine.conf.HistoryMaxNumber = (int)nudHistoryMaxItems.Value;
            if (mGuiIsReady)
            {
                Loader.Worker.CheckHistoryItems();
                Loader.Worker.SaveHistoryItems();
            }
        }

        private void cbCloseDropBox_CheckedChanged(object sender, EventArgs e)
        {
            Engine.conf.CloseDropBox = cbCloseDropBox.Checked;
        }

        private void btnHistoryClear_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you really want to clear the History List?", this.Text, MessageBoxButtons.YesNo,
                MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                lbHistory.Items.Clear();
                Loader.Worker.CheckHistoryItems();
                Loader.Worker.SaveHistoryItems();
            }
        }

        private void tsmQuickActions_Click(object sender, EventArgs e)
        {
            Loader.Worker.ShowActionsToolbar(true);
        }

        private void chkRememberTinyPicUserPass_CheckedChanged(object sender, EventArgs e)
        {
            Engine.conf.RememberTinyPicUserPass = chkRememberTinyPicUserPass.Checked;
        }

        private void btnResetIncrement_Click(object sender, EventArgs e)
        {
            Engine.conf.AutoIncrement = 0;
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
            else if (e.KeyCode == Keys.Delete)
            {
                this.DeleteHistoryFiles();
            }
        }

        private void btnCopyLink_Click(object sender, EventArgs e)
        {
            CopyLinkFromHistory();
        }

        private void cbHistoryListFormat_SelectedIndexChanged(object sender, EventArgs e)
        {
            Engine.conf.HistoryListFormat = (HistoryListFormat)cbHistoryListFormat.SelectedIndex;
            // LoadHistoryItems();
        }

        private void cbShowHistoryTooltip_CheckedChanged(object sender, EventArgs e)
        {
            Engine.conf.HistoryShowTooltips = cbShowHistoryTooltip.Checked;
        }

        private void cbHistorySave_CheckedChanged(object sender, EventArgs e)
        {
            Engine.conf.HistorySave = cbHistorySave.Checked;
        }

        private void pbCropCrosshairColor_Click(object sender, EventArgs e)
        {
            SelectColor((PictureBox)sender, ref Engine.conf.CropCrosshairColor);
        }

        private void chkCaptureFallback_CheckedChanged(object sender, EventArgs e)
        {
            Engine.conf.CaptureEntireScreenOnError = chkCaptureFallback.Checked;
        }

        private void nudSwitchAfter_ValueChanged(object sender, EventArgs e)
        {
            Engine.conf.SwitchAfter = nudSwitchAfter.Value;
        }

        private void cbCropDynamicCrosshair_CheckedChanged(object sender, EventArgs e)
        {
            Engine.conf.CropDynamicCrosshair = cbCropDynamicCrosshair.Checked;
        }

        private void nudCrosshairLineCount_ValueChanged(object sender, EventArgs e)
        {
            Engine.conf.CrosshairLineCount = (int)nudCrosshairLineCount.Value;
        }

        private void nudCrosshairLineSize_ValueChanged(object sender, EventArgs e)
        {
            Engine.conf.CrosshairLineSize = (int)nudCrosshairLineSize.Value;
        }

        private void nudCropInterval_ValueChanged(object sender, EventArgs e)
        {
            Engine.conf.CropInterval = (int)nudCropCrosshairInterval.Value;
        }

        private void nudCropStep_ValueChanged(object sender, EventArgs e)
        {
            Engine.conf.CropStep = (int)nudCropCrosshairStep.Value;
        }

        private void cbCropShowBigCross_CheckedChanged(object sender, EventArgs e)
        {
            Engine.conf.CropShowBigCross = cbCropShowBigCross.Checked;
        }

        private void cbShowCropRuler_CheckedChanged(object sender, EventArgs e)
        {
            Engine.conf.CropShowRuler = cbShowCropRuler.Checked;
        }

        private void cbSelectedWindowRuler_CheckedChanged(object sender, EventArgs e)
        {
            Engine.conf.SelectedWindowRuler = cbSelectedWindowRuler.Checked;
        }

        private void cbCropDynamicBorderColor_CheckedChanged(object sender, EventArgs e)
        {
            Engine.conf.CropDynamicBorderColor = cbCropDynamicBorderColor.Checked;
        }

        private void nudCropRegionInterval_ValueChanged(object sender, EventArgs e)
        {
            Engine.conf.CropRegionInterval = nudCropRegionInterval.Value;
        }

        private void nudCropRegionStep_ValueChanged(object sender, EventArgs e)
        {
            Engine.conf.CropRegionStep = nudCropRegionStep.Value;
        }

        private void nudCropHueRange_ValueChanged(object sender, EventArgs e)
        {
            Engine.conf.CropHueRange = nudCropHueRange.Value;
        }

        private void cbSelectedWindowDynamicBorderColor_CheckedChanged(object sender, EventArgs e)
        {
            Engine.conf.SelectedWindowDynamicBorderColor = cbSelectedWindowDynamicBorderColor.Checked;
        }

        private void nudSelectedWindowRegionInterval_ValueChanged(object sender, EventArgs e)
        {
            Engine.conf.SelectedWindowRegionInterval = nudSelectedWindowRegionInterval.Value;
        }

        private void nudSelectedWindowRegionStep_ValueChanged(object sender, EventArgs e)
        {
            Engine.conf.SelectedWindowRegionStep = nudSelectedWindowRegionStep.Value;
        }

        private void nudSelectedWindowHueRange_ValueChanged(object sender, EventArgs e)
        {
            Engine.conf.SelectedWindowHueRange = nudSelectedWindowHueRange.Value;
        }

        private void cbCropGridMode_CheckedChanged(object sender, EventArgs e)
        {
            Engine.conf.CropGridToggle = cboCropGridMode.Checked;
        }

        private void cbTinyPicSizeCheck_CheckedChanged(object sender, EventArgs e)
        {
            Engine.conf.TinyPicSizeCheck = cbTinyPicSizeCheck.Checked;
        }

        private void txtWatermarkImageLocation_TextChanged(object sender, EventArgs e)
        {
            if (File.Exists(txtWatermarkImageLocation.Text))
            {
                Engine.conf.WatermarkImageLocation = txtWatermarkImageLocation.Text;
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

        private void cbAutoChangeUploadDestination_CheckedChanged(object sender, EventArgs e)
        {
            Engine.conf.ImageUploadRetryOnTimeout = cboImageUploadRetryOnTimeout.Checked;
        }

        private void nudUploadDurationLimit_ValueChanged(object sender, EventArgs e)
        {
            Engine.conf.UploadDurationLimit = nudUploadDurationLimit.Value;
        }

        private void StartDebug()
        {
            mDebug = new DebugHelper();
            mDebug.GetDebugInfo += new StringEventHandler(debug_GetDebugInfo);
        }

        private void debug_GetDebugInfo(object sender, string e)
        {
            if (this.Visible)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(e);
                txtDebugInfo.Text = sb.ToString();
                if (cboDebugAutoScroll.Checked)
                {
                    txtDebugInfo.SelectionStart = txtDebugInfo.Text.Length;
                    txtDebugInfo.ScrollToCaret();
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
            Engine.conf.WatermarkUseBorder = cbWatermarkUseBorder.Checked;
            TestWatermark();
        }

        private void cbWatermarkAddReflection_CheckedChanged(object sender, EventArgs e)
        {
            Engine.conf.WatermarkAddReflection = cbWatermarkAddReflection.Checked;
            TestWatermark();
        }

        private void btnBrowseRootDir_Click(object sender, EventArgs e)
        {
            string oldRootDir = txtRootFolder.Text;
            FolderBrowserDialog dlg = new FolderBrowserDialog { ShowNewFolderButton = true };
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                Engine.SetRootFolder(dlg.SelectedPath);
                txtRootFolder.Text = Engine.mAppSettings.RootDir;
            }

            FileSystem.MoveDirectory(oldRootDir, txtRootFolder.Text);
            UpdateGuiControlsPaths();
            Engine.conf = XMLSettings.Read();
            ZScreen_ConfigGUI();
        }

        private void btnViewRootDir_Click(object sender, EventArgs e)
        {
            ShowDirectory(txtRootFolder.Text);
        }

        private void nudWatermarkImageScale_ValueChanged(object sender, EventArgs e)
        {
            Engine.conf.WatermarkImageScale = nudWatermarkImageScale.Value;
            TestWatermark();
        }

        private void trackWatermarkFontTrans_Scroll(object sender, EventArgs e)
        {
            Engine.conf.WatermarkFontTrans = trackWatermarkFontTrans.Value;
            nudWatermarkFontTrans.Value = Engine.conf.WatermarkFontTrans;
            TestWatermark();
        }

        private void trackWatermarkBackgroundTrans_Scroll(object sender, EventArgs e)
        {
            Engine.conf.WatermarkBackTrans = trackWatermarkBackgroundTrans.Value;
            nudWatermarkBackTrans.Value = Engine.conf.WatermarkBackTrans;
            TestWatermark();
        }

        private void cbWatermarkAutoHide_CheckedChanged(object sender, EventArgs e)
        {
            Engine.conf.WatermarkAutoHide = cbWatermarkAutoHide.Checked;
            TestWatermark();
        }

        private void cboWatermarkType_SelectedIndexChanged(object sender, EventArgs e)
        {
            Engine.conf.WatermarkMode = (WatermarkType)cboWatermarkType.SelectedIndex;
            TestWatermark();
        }

        private void cbCropShowMagnifyingGlass_CheckedChanged(object sender, EventArgs e)
        {
            Engine.conf.CropShowMagnifyingGlass = cbCropShowMagnifyingGlass.Checked;
        }

        private void pbLogo_MouseEnter(object sender, EventArgs e)
        {
            if (!turnLogo.IsTurning)
            {
                pbLogo.Image = ImageEffects.GetRandomLogo(Resources.main);
            }
        }

        private void pbLogo_MouseLeave(object sender, EventArgs e)
        {
            if (!turnLogo.IsTurning)
            {
                pbLogo.Image = new Bitmap((Image)new ComponentResourceManager(typeof(ZScreen)).GetObject("pbLogo.Image"));
            }
        }

        private void autoScreenshotsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Loader.Worker.ShowAutoCapture();
        }

        private void numericUpDownTimer1_ValueChanged(object sender, EventArgs e)
        {
            Engine.conf.ScreenshotDelayTime = nudtScreenshotDelay.Value;
        }

        private void nudtScreenshotDelay_SelectedIndexChanged(object sender, EventArgs e)
        {
            Engine.conf.ScreenshotDelayTimes = nudtScreenshotDelay.Time;
        }

        private void lblToLanguage_MouseDown(object sender, MouseEventArgs e)
        {
            if (cbToLanguage.SelectedIndex > -1)
            {
                cbToLanguage.DoDragDrop(Engine.conf.ToLanguage, DragDropEffects.Move);
            }
        }

        private void btnTranslateTo1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.Text) && e.AllowedEffect == DragDropEffects.Move)
            {
                e.Effect = DragDropEffects.Move;
            }
        }

        private void btnTranslateTo1_DragDrop(object sender, DragEventArgs e)
        {
            GoogleTranslate.GTLanguage lang = GoogleTranslate.FindLanguage(e.Data.GetData(DataFormats.Text).ToString(),
               ZScreen.mGTranslator.LanguageOptions.TargetLangList);
            Engine.conf.ToLanguage2 = lang.Value;
            btnTranslateTo1.Text = "To " + lang.Name;
        }

        private void btnTranslateTo1_Click(object sender, EventArgs e)
        {
            Loader.Worker.TranslateTo1();
        }

        private void cbLockFormSize_CheckedChanged(object sender, EventArgs e)
        {
            Engine.conf.LockFormSize = cbLockFormSize.Checked;
            CheckFormSettings();
        }

        /// <summary>
        /// Method to periodically (every 6 hours) perform online tasks
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tmrApp_Tick(object sender, EventArgs e)
        {
            Loader.Worker2.PerformOnlineTasks();
        }

        private void pgApp_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            ZScreen_ConfigGUI();
        }

        private void TextUploadersAddButton_Click(object sender, EventArgs e)
        {
            if (ucTextUploaders.Templates.SelectedIndex > -1)
            {
                string name = ucTextUploaders.Templates.SelectedItem.ToString();
                if (!string.IsNullOrEmpty(name))
                {
                    TextUploader textUploader = Adapter.FindTextUploader(name);
                    if (textUploader != null)
                    {
                        Engine.conf.TextUploadersList.Add(textUploader);
                        ucTextUploaders.MyCollection.Items.Add(textUploader);
                        ucDestOptions.cboTextUploaders.Items.Add(textUploader);
                    }

                    ucTextUploaders.MyCollection.SelectedIndex = ucTextUploaders.MyCollection.Items.Count - 1;
                }
            }
        }

        private void TextUploadersRemoveButton_Click(object sender, EventArgs e)
        {
            if (ucTextUploaders.MyCollection.Items.Count > 0)
            {
                List<TextUploader> selectedUploaders = new List<TextUploader>();
                foreach (TextUploader uploader in ucTextUploaders.MyCollection.SelectedItems)
                {
                    selectedUploaders.Add(uploader);
                }
                foreach (TextUploader uploader in selectedUploaders)
                {
                    Engine.conf.TextUploadersList.Remove(uploader);
                    ucTextUploaders.MyCollection.Items.Remove(uploader);
                    ucDestOptions.cboTextUploaders.Items.Remove(uploader);
                }
                ucTextUploaders.MyCollection.SelectedIndex = ucTextUploaders.MyCollection.Items.Count - 1;
            }
        }

        private void TestUploaderText(TextUploader uploader)
        {
            if (uploader != null)
            {
                string name = uploader.ToString();
                string testString = uploader.TesterString;

                if (!string.IsNullOrEmpty(name))
                {
                    WorkerTask task = Loader.Worker.GetWorkerText(WorkerTask.Jobs.UploadFromClipboard);
                    task.MyText = TextInfo.FromString(testString);
                    task.MakeTinyURL = false; // preventing Error: TinyURL redirects to a TinyURL.
                    task.MyTextUploader = uploader;
                    task.RunWorker();
                }
            }
            else
            {
                MessageBox.Show("Select a Text Uploader.");
            }
        }

        private void TextUploaderTestButton_Click(object sender, EventArgs e)
        {
            TextUploader uploader = (TextUploader)ucTextUploaders.MyCollection.SelectedItem;
            TestUploaderText(uploader);
        }

        private void pgFTPSettings_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            RewriteFTPRightClickMenu();
        }

        private void pgEditorsImage_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            Software temp = Engine.conf.ImageEditors[lbSoftware.SelectedIndex];
            lbSoftware.Items[lbSoftware.SelectedIndex] = temp;
            Engine.conf.ImageEditors[lbSoftware.SelectedIndex] = temp;
            RewriteImageEditorsRightClickMenu();
        }

        private void cboTextUploaders_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool bComboBox = sender.GetType() == typeof(ComboBox);
            int sel = (bComboBox ? ucDestOptions.cboTextUploaders.SelectedIndex : ucTextUploaders.MyCollection.SelectedIndex);

            if (ucTextUploaders.MyCollection.SelectedItems.Count > 0)
            {
                TextUploader textUploader = (TextUploader)ucTextUploaders.MyCollection.SelectedItem;

                if (mGuiIsReady)
                {
                    Engine.conf.TextUploaderSelected = sel;
                    if (bComboBox)
                    {
                        ucTextUploaders.MyCollection.SelectedIndex = sel;
                    }
                    else
                    {
                        ucDestOptions.cboTextUploaders.SelectedIndex = sel;
                    }
                }

                bool hasOptions = textUploader != null;
                ucTextUploaders.SettingsGrid.Visible = hasOptions;

                if (hasOptions)
                {
                    ucTextUploaders.SettingsGrid.SelectedObject = textUploader.Settings;
                }
            }
        }

        private void cboURLShorteners_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (mGuiIsReady)
            {
                ucUrlShorteners.MyCollection.SelectedIndex = ucDestOptions.cboURLShorteners.SelectedIndex;
                Engine.conf.UrlShortenerSelected = ucDestOptions.cboURLShorteners.SelectedIndex;
            }
        }

        private void cbAutoTranslate_CheckedChanged(object sender, EventArgs e)
        {
            Engine.conf.AutoTranslate = cbAutoTranslate.Checked;
        }

        private void txtAutoTranslate_TextChanged(object sender, EventArgs e)
        {
            int number;
            if (int.TryParse(txtAutoTranslate.Text, out number))
            {
                Engine.conf.AutoTranslateLength = number;
            }
        }

        private void UrlShortenerTestButton_Click(object sender, EventArgs e)
        {
            this.TestUploaderText((TextUploader)ucUrlShorteners.MyCollection.SelectedItem);
        }

        private void UrlShorteners_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ucUrlShorteners.MyCollection.SelectedItems.Count > 0)
            {
                TextUploader textUploader = (TextUploader)ucUrlShorteners.MyCollection.SelectedItem;

                if (mGuiIsReady)
                {
                    Engine.conf.UrlShortenerSelected = ucUrlShorteners.MyCollection.SelectedIndex;
                    ucDestOptions.cboURLShorteners.SelectedIndex = ucUrlShorteners.MyCollection.SelectedIndex;
                }

                bool hasOptions = textUploader != null;
                ucUrlShorteners.SettingsGrid.Visible = hasOptions;

                if (hasOptions)
                {
                    ucUrlShorteners.SettingsGrid.SelectedObject = textUploader.Settings;
                }
            }
        }

        /// <summary>
        /// Method to add a Link Shorteners to the List of Link Shorteners
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UrlShortenersAddButton_Click(object sender, EventArgs e)
        {
            if (ucUrlShorteners.Templates.SelectedIndex > -1)
            {
                string name = ucUrlShorteners.Templates.SelectedItem.ToString();
                if (!string.IsNullOrEmpty(name))
                {
                    TextUploader textUploader = Adapter.FindUrlShortener(name);
                    if (textUploader != null)
                    {
                        Engine.conf.UrlShortenersList.Add(textUploader);
                        ucUrlShorteners.MyCollection.Items.Add(textUploader);
                        ucDestOptions.cboURLShorteners.Items.Add(textUploader);
                    }

                    ucUrlShorteners.MyCollection.SelectedIndex = ucUrlShorteners.MyCollection.Items.Count - 1;
                }
            }
        }

        /// <summary>
        /// Method to remove a Link Shorteners from the List of Link Shorteners
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UrlShortenersRemoveButton_Click(object sender, EventArgs e)
        {
            if (ucUrlShorteners.MyCollection.Items.Count > 0)
            {
                List<TextUploader> selectedUploaders = new List<TextUploader>();
                foreach (TextUploader uploader in ucUrlShorteners.MyCollection.SelectedItems)
                {
                    selectedUploaders.Add(uploader);
                }
                foreach (TextUploader uploader in selectedUploaders)
                {
                    Engine.conf.UrlShortenersList.Remove(uploader);
                    ucUrlShorteners.MyCollection.Items.Remove(uploader);
                    ucDestOptions.cboURLShorteners.Items.Remove(uploader);
                }
                ucUrlShorteners.MyCollection.SelectedIndex = ucUrlShorteners.MyCollection.Items.Count - 1;
            }
        }

        private void cbShowHelpBalloonTips_CheckedChanged(object sender, EventArgs e)
        {
            Engine.conf.ShowHelpBalloonTips = cbShowHelpBalloonTips.Checked;
            ttZScreen.Active = Engine.conf.ShowHelpBalloonTips;
        }

        private void chkImageEditorAutoSave_CheckedChanged(object sender, EventArgs e)
        {
            Engine.conf.ImageEditorAutoSave = chkImageEditorAutoSave.Checked;
        }

        private void btnImageShackProfile_Click(object sender, EventArgs e)
        {
            Process.Start("http://profile.imageshack.us/user/" + txtUserNameImageShack.Text);
        }

        private void chkPublicImageShack_CheckedChanged(object sender, EventArgs e)
        {
            Engine.conf.ImageShackShowImagesInPublic = chkPublicImageShack.Checked;
        }

        private void txtUserNameImageShack_TextChanged(object sender, EventArgs e)
        {
            Engine.conf.ImageShackUserName = txtUserNameImageShack.Text;
        }

        private void ucTextUploaders_Load(object sender, EventArgs e)
        {
            cboTextUploaders_SelectedIndexChanged(sender, e);
        }

        private void ucUrlShorteners_Load(object sender, EventArgs e)
        {
            UrlShorteners_SelectedIndexChanged(sender, e);
        }

        private void tcApp_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Engine.conf.AutoSaveSettings)
            {
                WriteSettings();
            }
        }

        private void chkDekiWikiForcePath_CheckedChanged(object sender, EventArgs e)
        {
            Engine.conf.DekiWikiForcePath = chkDekiWikiForcePath.Checked;
        }

        private void lblLogo_Click(object sender, EventArgs e)
        {
            FormsMgr.ShowAboutWindow();
        }

        private void DrawZScreenLabel(bool hover)
        {
            Color color = hover ? Color.LightGray : Color.WhiteSmoke;
            Bitmap bmpVersion = new Bitmap(lblLogo.Width, lblLogo.Height);
            Graphics g = Graphics.FromImage(bmpVersion);
            g.SmoothingMode = SmoothingMode.HighQuality;
            LinearGradientBrush brush = new LinearGradientBrush(new Rectangle(0, 0, lblLogo.Width, lblLogo.Height), color,
               lblLogo.BackColor, LinearGradientMode.Horizontal);
            brush.SetSigmaBellShape(0.50f);
            g.FillRectangle(brush, new Rectangle(0, 0, lblLogo.Width, lblLogo.Height));
            lblLogo.Image = bmpVersion;
        }

        private void lblLogo_MouseEnter(object sender, EventArgs e)
        {
            DrawZScreenLabel(true);
        }

        private void lblLogo_MouseLeave(object sender, EventArgs e)
        {
            DrawZScreenLabel(false);
        }

        private void chkProxyEnable_CheckedChanged(object sender, EventArgs e)
        {
            Engine.conf.ProxyEnabled = chkProxyEnable.Checked;
            if (mGuiIsReady)
            {
                Uploader.ProxySettings = Adapter.CheckProxySettings();
            }
        }

        private void tsmFTPClient_Click(object sender, EventArgs e)
        {
            OpenFTPClient();
        }

        private void ProxyAccountTestButton_Click(object sender, EventArgs e)
        {
            ProxyInfo proxy = GetSelectedProxy();
            if (proxy != null)
            {
                Adapter.TestProxyAccount(proxy);
            }
        }

        private void ProxyAccountsRemoveButton_Click(object sender, EventArgs e)
        {
            int sel = ucProxyAccounts.AccountsList.SelectedIndex;
            if (ucProxyAccounts.RemoveItem(sel) == true)
            {
                Engine.conf.ProxyList.RemoveAt(sel);
            }
        }

        private void ProxyAccountsList_SelectedIndexChanged(object sender, EventArgs e)
        {
            int sel = ucProxyAccounts.AccountsList.SelectedIndex;
            if (Engine.conf.ProxyList != null && sel != -1 && sel < Engine.conf.ProxyList.Count && Engine.conf.ProxyList[sel] != null)
            {
                ProxyInfo acc = Engine.conf.ProxyList[sel];
                ucProxyAccounts.SettingsGrid.SelectedObject = acc;
                Engine.conf.ProxyActive = acc;
                Engine.conf.ProxySelected = ucProxyAccounts.AccountsList.SelectedIndex;
            }
            if (mGuiIsReady)
            {
                Uploader.ProxySettings = Adapter.CheckProxySettings();
            }
        }

        private void ProxyAccountsAddButton_Click(object sender, EventArgs e)
        {
            ProxyInfo acc = new ProxyInfo("username", "password", "host", 8080);
            Engine.conf.ProxyList.Add(acc);
            ucProxyAccounts.AccountsList.Items.Add(acc);
            ucProxyAccounts.AccountsList.SelectedIndex = ucProxyAccounts.AccountsList.Items.Count - 1;
        }

        private void dgvHotkeys_KeyDown(object sender, KeyEventArgs e)
        {
            if (Loader.Worker.mSetHotkeys)
            {
                if (e.KeyValue == (int)Keys.Up || e.KeyValue == (int)Keys.Down || e.KeyValue == (int)Keys.Left || e.KeyValue == (int)Keys.Right)
                {
                    e.SuppressKeyPress = true;
                    e.Handled = true;
                }
            }
        }

        private void cbSelectedWindowCaptureObjects_CheckedChanged(object sender, EventArgs e)
        {
            Engine.conf.SelectedWindowCaptureObjects = chkSelectedWindowCaptureObjects.Checked;
        }

        private void cbSelectedWindowCleanBackground_CheckedChanged(object sender, EventArgs e)
        {
            Engine.conf.ActiveWindowClearBackground = chkSelectedWindowCleanBackground.Checked;
            UpdateAeroGlassConfig();
        }

        private void UpdateAeroGlassConfig()
        {
            // Disable Show Checkers option if Clean Background is disabled
            if (!chkSelectedWindowCleanBackground.Checked)
            {
                chkSelectedWindowShowCheckers.Checked = false;
            }
            chkSelectedWindowShowCheckers.Enabled = chkSelectedWindowCleanBackground.Checked;
            gbCaptureGdi.Enabled = !chkActiveWindowPreferDWM.Checked;
            gbCaptureDwm.Enabled = !gbCaptureGdi.Enabled;
        }

        private void cbSelectedWindowCleanTransparentCorners_CheckedChanged(object sender, EventArgs e)
        {
            Engine.conf.ActiveWindowCleanTransparentCorners = chkSelectedWindowCleanTransparentCorners.Checked;
            UpdateAeroGlassConfig();
        }

        private void cbSaveFormSizePosition_CheckedChanged(object sender, EventArgs e)
        {
            Engine.conf.SaveFormSizePosition = chkSaveFormSizePosition.Checked;

            if (mGuiIsReady)
            {
                if (Engine.conf.SaveFormSizePosition)
                {
                    Engine.conf.WindowLocation = this.Location;
                    Engine.conf.WindowSize = this.Size;
                }
                else
                {
                    Engine.conf.WindowLocation = Point.Empty;
                    Engine.conf.WindowSize = Size.Empty;
                }
            }
        }

        private void cbAutoSaveSettings_CheckedChanged(object sender, EventArgs e)
        {
            Engine.conf.AutoSaveSettings = cbAutoSaveSettings.Checked;
        }

        private void cbTwitPicShowFull_CheckedChanged(object sender, EventArgs e)
        {
            Engine.conf.TwitPicShowFull = cbTwitPicShowFull.Checked;
        }

        private void cbTwitPicThumbnailMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            Engine.conf.TwitPicThumbnailMode = (TwitPicThumbnailType)cboTwitPicThumbnailMode.SelectedIndex;
        }

        private void nudtScreenshotDelay_MouseHover(object sender, EventArgs e)
        {
            ttZScreen.Show(ttZScreen.GetToolTip(nudtScreenshotDelay), this);
        }

        private void txtImagesFolderPattern_TextChanged(object sender, EventArgs e)
        {
            Engine.conf.SaveFolderPattern = txtImagesFolderPattern.Text;
            lblImagesFolderPatternPreview.Text = NameParser.Convert(NameParserType.SaveFolder);
            txtImagesDir.Text = Engine.ImagesDir;
        }

        private void txtWatermarkText_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && codesMenu.Visible)
            {
                codesMenu.Close();
            }
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
                FileSystem.AppendDebug("Error while moving image files", ex);
                MessageBox.Show(ex.Message);
            }
        }

        private void cbCheckUpdatesBeta_CheckedChanged(object sender, EventArgs e)
        {
            Engine.conf.CheckUpdatesBeta = chkCheckUpdatesBeta.Checked;
        }

        private void rbImageSize_CheckedChanged(object sender, EventArgs e)
        {
            if (rbImageSizeDefault.Checked)
            {
                Engine.conf.ImageSizeType = ImageSizeType.DEFAULT;
            }
            else if (rbImageSizeFixed.Checked)
            {
                Engine.conf.ImageSizeType = ImageSizeType.FIXED;
            }
            else if (rbImageSizeRatio.Checked)
            {
                Engine.conf.ImageSizeType = ImageSizeType.RATIO;
            }
        }

        private void txtImageSizeFixedWidth_TextChanged(object sender, EventArgs e)
        {
            int width;
            if (int.TryParse(txtImageSizeFixedWidth.Text, out width))
            {
                Engine.conf.ImageSizeFixedWidth = width;
            }
        }

        private void txtImageSizeFixedHeight_TextChanged(object sender, EventArgs e)
        {
            int height;
            if (int.TryParse(txtImageSizeFixedHeight.Text, out height))
            {
                Engine.conf.ImageSizeFixedHeight = height;
            }
        }

        private void txtImageSizeRatio_TextChanged(object sender, EventArgs e)
        {
            float percentage;
            if (float.TryParse(txtImageSizeRatio.Text, out percentage))
            {
                Engine.conf.ImageSizeRatioPercentage = percentage;
            }
        }

        private void btnWebPageUploadImage_Click(object sender, EventArgs e)
        {
            string url = txtWebPageURL.Text;

            if (!string.IsNullOrEmpty(url))
            {
                url = url.StartsWith("http://", StringComparison.InvariantCultureIgnoreCase) ? url : "http://" + url;
                btnWebPageCaptureImage.Enabled = false;

                IECapt capture;
                if (Engine.conf.WebPageUseCustomSize)
                {
                    capture = new IECapt(Engine.conf.WebPageWidth, Engine.conf.WebPageHeight, 1);
                }
                else
                {
                    capture = new IECapt(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height, 1);
                }

                capture.ImageCaptured += new IECapt.ImageEventHandler(capture_ImageCaptured);
                capture.CapturePage(txtWebPageURL.Text);
            }

            /*
            WebPageCapture webPageCapture;
            if (Program.conf.WebPageUseCustomSize)
            {
                webPageCapture = new WebPageCapture(Program.conf.WebPageWidth, Program.conf.WebPageHeight);
            }
            else
            {
                webPageCapture = new WebPageCapture();
            }
            
            webPageCapture.DownloadCompleted += new WebPageCapture.ImageEventHandler(webPageCapture_DownloadCompleted);
            webPageCapture.DownloadPage(txtWebPageURL.Text);
            */
        }

        private void capture_ImageCaptured(Image img)
        {
            pbWebPageImage.Image = img;
            btnWebPageCaptureImage.Enabled = true;
            btnWebPageImageUpload.Enabled = img != null;

            if (Engine.conf.WebPageAutoUpload)
            {
                WebPageUpload();
            }
        }

        private void cbWebPageUseCustomSize_CheckedChanged(object sender, EventArgs e)
        {
            Engine.conf.WebPageUseCustomSize = cbWebPageUseCustomSize.Checked;
        }

        private void txtWebPageWidth_TextChanged(object sender, EventArgs e)
        {
            int width;
            if (int.TryParse(txtWebPageWidth.Text, out width))
            {
                Engine.conf.WebPageWidth = width;
            }
        }

        private void txtWebPageHeight_TextChanged(object sender, EventArgs e)
        {
            int height;
            if (int.TryParse(txtWebPageHeight.Text, out height))
            {
                Engine.conf.WebPageHeight = height;
            }
        }

        private void btnWebPageImageUpload_Click(object sender, EventArgs e)
        {
            WebPageUpload();
        }

        private void WebPageUpload()
        {
            if (pbWebPageImage.Image != null)
            {
                Bitmap bmp = new Bitmap(pbWebPageImage.Image);
                Loader.Worker.StartWorkerPictures(WorkerTask.Jobs.UPLOAD_IMAGE, bmp);
            }
        }

        private void cbWebPageAutoUpload_CheckedChanged(object sender, EventArgs e)
        {
            Engine.conf.WebPageAutoUpload = cbWebPageAutoUpload.Checked;
        }

        private void txtImageBamApiKey_TextChanged(object sender, EventArgs e)
        {
            Engine.conf.ImageBamApiKey = txtImageBamApiKey.Text;
        }

        private void txtImageBamSecret_TextChanged(object sender, EventArgs e)
        {
            Engine.conf.ImageBamSecret = txtImageBamSecret.Text;
        }

        private void btnImageBamApiKeysUrl_Click(object sender, EventArgs e)
        {
            Process.Start("http://www.imagebam.com/nav/API_Keys");
        }

        private void btnImageBamRegister_Click(object sender, EventArgs e)
        {
            Process.Start("http://www.imagebam.com/register");
        }

        private void btnImageBamCreateGallery_Click(object sender, EventArgs e)
        {
            lbImageBamGalleries.Items.Add(Adapter.CreateImageBamGallery());
        }

        private void lbImageBamGalleries_SelectedIndexChanged(object sender, EventArgs e)
        {
            Engine.conf.ImageBamGalleryActive = lbImageBamGalleries.SelectedIndex;
        }

        private void btnImageBamRemoveGallery_Click(object sender, EventArgs e)
        {
            if (lbImageBamGalleries.SelectedIndex > -1)
            {
                if (!string.IsNullOrEmpty(Adapter.GetImageBamGalleryActive()))
                {
                    lbImageBamGalleries.Items.RemoveAt(lbImageBamGalleries.SelectedIndex);
                    Engine.conf.ImageBamGallery.RemoveAt(lbImageBamGalleries.SelectedIndex);
                }
            }
        }

        private void chkImageBamContentNSFW_CheckedChanged(object sender, EventArgs e)
        {
            Engine.conf.ImageBamContentNSFW = chkImageBamContentNSFW.Checked;
        }

        private void cboRapidShareAcctType_SelectedIndexChanged(object sender, EventArgs e)
        {
            Engine.conf.RapidShareAccountType = (RapidShareAcctType)cboRapidShareAcctType.SelectedIndex;
            txtRapidSharePremiumUserName.Enabled = Engine.conf.RapidShareAccountType == RapidShareAcctType.Premium;
            txtRapidShareCollectorID.Enabled = Engine.conf.RapidShareAccountType != RapidShareAcctType.Free && !txtRapidSharePremiumUserName.Enabled;
            txtRapidSharePassword.Enabled = Engine.conf.RapidShareAccountType != RapidShareAcctType.Free;
        }

        private void txtRapidShareCollectorID_TextChanged(object sender, EventArgs e)
        {
            Engine.conf.RapidShareCollectorsID = txtRapidShareCollectorID.Text;
        }

        private void txtRapidSharePremiumUserName_TextChanged(object sender, EventArgs e)
        {
            Engine.conf.RapidSharePremiumUserName = txtRapidSharePremiumUserName.Text;
        }

        private void txtRapidSharePassword_TextChanged(object sender, EventArgs e)
        {
            Engine.conf.RapidSharePassword = txtRapidSharePassword.Text;
        }

        private void txtSendSpaceUserName_TextChanged(object sender, EventArgs e)
        {
            Engine.conf.SendSpaceUserName = txtSendSpaceUserName.Text;
        }

        private void txtSendSpacePassword_TextChanged(object sender, EventArgs e)
        {
            Engine.conf.SendSpacePassword = txtSendSpacePassword.Text;
        }

        private void cboSendSpaceAcctType_SelectedIndexChanged(object sender, EventArgs e)
        {
            Engine.conf.SendSpaceAccountType = (AcctType)cboSendSpaceAcctType.SelectedIndex;
            txtSendSpacePassword.Enabled = Engine.conf.SendSpaceAccountType == AcctType.User;
            txtSendSpaceUserName.Enabled = Engine.conf.SendSpaceAccountType == AcctType.User;
        }

        private void btnSendSpaceRegister_Click(object sender, EventArgs e)
        {
            using (UserPassBox upb = Adapter.SendSpaceRegister())
            {
                if (upb.Success)
                {
                    txtSendSpaceUserName.Text = upb.UserName;
                    txtSendSpacePassword.Text = upb.Password;
                    cboSendSpaceAcctType.SelectedIndex = (int)AcctType.User;
                }
            }
        }

        private void cboFileUploaders_SelectedIndexChanged(object sender, EventArgs e)
        {
            Engine.conf.FileDestMode = (FileUploaderType)ucDestOptions.cboFileUploaders.SelectedIndex;

            CheckToolStripMenuItem(tsmFileDest, GetFileDestMenuItem(Engine.conf.FileDestMode));
        }

        private void txtFTPThumbWidth_TextChanged(object sender, EventArgs e)
        {
            int width;
            if (int.TryParse(txtFTPThumbWidth.Text, out width))
            {
                Engine.conf.FTPThumbnailWidth = width;
            }
        }

        private void txtFTPThumbHeight_TextChanged(object sender, EventArgs e)
        {
            int height;
            if (int.TryParse(txtFTPThumbHeight.Text, out height))
            {
                Engine.conf.FTPThumbnailHeight = height;
            }
        }

        private void cbFTPThumbnailCheckSize_CheckedChanged(object sender, EventArgs e)
        {
            Engine.conf.FTPThumbnailCheckSize = cbFTPThumbnailCheckSize.Checked;
        }

        private void chkWindows7TaskbarIntegration_CheckedChanged(object sender, EventArgs e)
        {
            if (mGuiIsReady)
            {
                Engine.conf.Windows7TaskbarIntegration = chkWindows7TaskbarIntegration.Checked;
                chkShowTaskbar.Enabled = !Engine.conf.Windows7TaskbarIntegration;
                ZScreen_Windows7onlyTasks();
            }
        }

        public void OpenFTPClient()
        {
            if (Adapter.CheckFTPAccounts())
            {
                FTPAccount acc = Engine.conf.FTPAccountList[Engine.conf.FTPSelected];
                FTPClient2 ftpClient = new FTPClient2(acc) { Icon = this.Icon };
                ftpClient.Show();
            }
        }

        #region Flickr

        private void btnFlickrGetFrob_Click(object sender, EventArgs e)
        {
            try
            {
                FlickrUploader flickr = new FlickrUploader();
                btnFlickrGetFrob.Tag = flickr.GetFrob();
                string url = flickr.GetAuthLink(FlickrUploader.Permission.Write);
                Process.Start(url);
                btnFlickrGetToken.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnFlickrGetToken_Click(object sender, EventArgs e)
        {
            try
            {
                string token = btnFlickrGetFrob.Tag as string;
                if (!string.IsNullOrEmpty(token))
                {
                    FlickrUploader flickr = new FlickrUploader();
                    Engine.conf.FlickrAuthInfo = flickr.GetToken(token);
                    pgFlickrAuthInfo.SelectedObject = Engine.conf.FlickrAuthInfo;
                    // btnFlickrOpenImages.Text = string.Format("{0}'s photostream", Engine.conf.FlickrAuthInfo.Username);
                    MessageBox.Show("Success.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnFlickrCheckToken_Click(object sender, EventArgs e)
        {
            try
            {
                if (Engine.conf.FlickrAuthInfo != null)
                {
                    string token = Engine.conf.FlickrAuthInfo.Token;
                    if (!string.IsNullOrEmpty(token))
                    {
                        FlickrUploader flickr = new FlickrUploader();
                        Engine.conf.FlickrAuthInfo = flickr.CheckToken(token);
                        pgFlickrAuthInfo.SelectedObject = Engine.conf.FlickrAuthInfo;

                        MessageBox.Show("Success.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnFlickrOpenImages_Click(object sender, EventArgs e)
        {
            if (Engine.conf.FlickrAuthInfo != null)
            {
                string userID = Engine.conf.FlickrAuthInfo.UserID;
                if (!string.IsNullOrEmpty(userID))
                {
                    FlickrUploader flickr = new FlickrUploader();
                    string url = flickr.GetPhotosLink(userID);
                    Process.Start(url);
                }
            }
        }

        #endregion

        private void btnFTPOpenClient_Click(object sender, EventArgs e)
        {
            OpenFTPClient();
        }

        private void chkShellExt_CheckedChanged(object sender, EventArgs e)
        {
            if (chkShellExt.Checked)
            {
                RegistryMgr.ShellExtRegister();
            }
            else
            {
                RegistryMgr.ShellExtUnregister();
            }
        }

        private void lbHistory_DragDrop(object sender, DragEventArgs e)
        {
            string[] FilePaths = (string[])e.Data.GetData(DataFormats.FileDrop, true);
            List<string> files = new List<string>();
            files.AddRange(FilePaths);
            Loader.Worker.UploadUsingFileSystem(files);
        }

        private void lbHistory_DragEnter(object sender, DragEventArgs e)
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

        private void chkHotkeys_CheckedChanged(object sender, EventArgs e)
        {
            if (mGuiIsReady)
            {
                if (chkHotkeys.Checked)
                {
                    Engine.ZScreenKeyboardHook = new KeyboardHook();
                    Engine.ZScreenKeyboardHook.KeyDownEvent += new KeyEventHandler(Loader.Worker.CheckHotkeys);
                }
                else
                {
                    Engine.ZScreenKeyboardHook.Dispose();
                }
            }
        }

        private void chkTwitterEnable_CheckedChanged(object sender, EventArgs e)
        {
            if (!Adapter.CheckTwitterAccounts())
            {
                MessageBox.Show("Configure your Twitter accounts in Destinations tab", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                tcApp.SelectedTab = tpDestinations;
                tcDestinations.SelectedTab = tpTwitter;
            }
            Engine.conf.TwitterEnabled = chkTwitterEnable.Checked;
        }

        private void tsmiTwitter_Click(object sender, EventArgs e)
        {
            if (lbHistory.SelectedItem != null)
            {
                HistoryItem hi = (HistoryItem)lbHistory.SelectedItem;
                if (!string.IsNullOrEmpty(hi.RemotePath))
                {
                    string url = Adapter.ShortenURL(hi.RemotePath);
                    Adapter.TwitterMsg(string.IsNullOrEmpty(url) ? hi.RemotePath : url);
                }
            }
        }

        private void btnFtpHelp_Click(object sender, EventArgs e)
        {
            Process.Start("http://code.google.com/p/zscreen/wiki/FTPAccounts");
        }

        private void btnOpenZScreenTester_Click(object sender, EventArgs e)
        {
            TesterGUI testerGUI = new TesterGUI
            { // TODO
                TestFileBinaryPath = "",
                TestFilePicturePath = "",
                TestFileTextPath = ""
            };

            testerGUI.Show();
        }

        private void nudMaxNameLength_ValueChanged(object sender, EventArgs e)
        {
            Engine.conf.MaxNameLength = (int)nudMaxNameLength.Value;
        }

        private void TwitterAccountAuthButton_Click(object sender, EventArgs e)
        {
            if (Adapter.CheckTwitterAccounts())
            {
                TwitterAuthInfo acc = Adapter.TwitterGetActiveAcct();
                if (!string.IsNullOrEmpty(acc.PIN))
                {
                    acc = Adapter.TwitterAuthSetPin(ref acc);
                    if (null != acc)
                    {
                        ucTwitterAccounts.SettingsGrid.SelectedObject = acc;
                    }
                }
            }
        }

        private void TwitterAccountRemoveButton_Click(object sender, EventArgs e)
        {
            int sel = ucTwitterAccounts.AccountsList.SelectedIndex;
            if (ucTwitterAccounts.RemoveItem(sel) == true)
            {
                Engine.conf.TwitterAccountsList.RemoveAt(sel);
            }
        }

        private void TwitterAccountList_SelectedIndexChanged(object sender, EventArgs e)
        {
            int sel = ucTwitterAccounts.AccountsList.SelectedIndex;
            Engine.conf.TwitterAcctSelected = sel;

            if (Adapter.CheckList(Engine.conf.TwitterAccountsList, Engine.conf.TwitterAcctSelected))
            {
                TwitterAuthInfo acc = Engine.conf.TwitterAccountsList[sel];
                ucTwitterAccounts.SettingsGrid.SelectedObject = acc;
            }
        }

        private void TwitterAccountAddButton_Click(object sender, EventArgs e)
        {
            TwitterAuthInfo acc = new TwitterAuthInfo();
            Engine.conf.TwitterAccountsList.Add(acc);
            ucTwitterAccounts.AccountsList.Items.Add(acc);
            ucTwitterAccounts.AccountsList.SelectedIndex = ucTwitterAccounts.AccountsList.Items.Count - 1;
            if (Adapter.CheckTwitterAccounts())
            {
                ucTwitterAccounts.SettingsGrid.SelectedObject = Adapter.TwitterAuthGetPin();
            }
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

        private void btnSelectGradient_Click(object sender, EventArgs e)
        {
            using (GradientMaker gradient = new GradientMaker(Engine.conf.GradientMakerOptions))
            {
                gradient.Icon = this.Icon;
                if (gradient.ShowDialog() == DialogResult.OK)
                {
                    Engine.conf.GradientMakerOptions = gradient.Options;
                    TestWatermark();
                }
            }
        }

        private void cbUseCustomGradient_CheckedChanged(object sender, EventArgs e)
        {
            Engine.conf.WatermarkUseCustomGradient = cboUseCustomGradient.Checked;
            gbGradientMakerBasic.Enabled = !cboUseCustomGradient.Checked;
            TestWatermark();
        }

        private void chkImageUploadRandomRetryOnFail_CheckedChanged(object sender, EventArgs e)
        {
            Engine.conf.ImageUploadRandomRetryOnFail = chkImageUploadRandomRetryOnFail.Checked;
        }

        private void cbSelectedWindowIncludeShadow_CheckedChanged(object sender, EventArgs e)
        {
            Engine.conf.ActiveWindowIncludeShadows = chkSelectedWindowIncludeShadow.Checked;
            UpdateAeroGlassConfig();
        }

        private void cbSelectedWindowShowCheckers_CheckedChanged(object sender, EventArgs e)
        {
            Engine.conf.ActiveWindowShowCheckers = chkSelectedWindowShowCheckers.Checked;
        }

        private void chkMonImages_CheckedChanged(object sender, EventArgs e)
        {
            Engine.conf.MonitorImages = chkMonImages.Checked;
        }

        private void chkMonText_CheckedChanged(object sender, EventArgs e)
        {
            Engine.conf.MonitorText = chkMonText.Checked;
        }

        private void chkMonFiles_CheckedChanged(object sender, EventArgs e)
        {
            Engine.conf.MonitorFiles = chkMonFiles.Checked;
        }

        private void chkMonUrls_CheckedChanged(object sender, EventArgs e)
        {
            Engine.conf.MonitorUrls = chkMonUrls.Checked;
        }

        private void chkActiveWindowTryCaptureChilds_CheckedChanged(object sender, EventArgs e)
        {
            Engine.conf.ActiveWindowTryCaptureChilds = chkActiveWindowTryCaptureChilds.Checked;
        }

        private void chkActiveWindowPreferDWM_CheckedChanged(object sender, EventArgs e)
        {
            Engine.conf.ActiveWindowPreferDWM = chkActiveWindowPreferDWM.Checked;
            chkActiveWindowTryCaptureChilds.Enabled = !chkActiveWindowPreferDWM.Checked;
            UpdateAeroGlassConfig();
        }

        private void chkActiveWindowGDIFreezeWindow_CheckedChanged(object sender, EventArgs e)
        {
            Engine.conf.ActiveWindowGDIFreezeWindow = cbActiveWindowGDIFreezeWindow.Checked;
        }

        private void ChkEditorsEnableCheckedChanged(object sender, EventArgs e)
        {
            Engine.conf.ImageEditorsEnabled = chkEditorsEnabled.Checked;
            lbSoftware.Enabled = chkEditorsEnabled.Checked;
        }

        private void cbGIFQuality_SelectedIndexChanged(object sender, EventArgs e)
        {
            Engine.conf.GIFQuality = (GIFQuality)cbGIFQuality.SelectedIndex;
        }

        private void tsmEditinImageSoftware_CheckedChanged(object sender, EventArgs e)
        {
            chkEditorsEnabled.Checked = tsmEditinImageSoftware.Checked;
        }

        void LbSoftwareItemCheck(object sender, ItemCheckEventArgs e)
        {
            UpdateGuiEditors(sender);
        }
    }
}
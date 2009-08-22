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
using ZSS;
using ZSS.ColorsLib;
using ZSS.FTPClientLib;
using ZSS.Properties;

namespace ZScreenLib
{
    public partial class ZScreen : Form
    {
        #region Private Variables

        private bool mGuiIsReady, mClose;
        private int mHadFocusAt;
        private TextBox mHadFocus;
        private ContextMenuStrip codesMenu = new ContextMenuStrip();
        private Debug debug = null;
        private ImageEffects.TurnImage turnLogo;
        internal static GoogleTranslate mGTranslator = null;

        #endregion

        public ZScreen()
        {
            InitializeComponent();
            ZScreen_SetFormSettings();
            Loader.Worker = new WorkerPrimary(this);
            Loader.Worker2 = new WorkerSecondary(this);
            ZScreen_ConfigGUI();

            Loader.Worker2.PerformOnlineTasks();
            Program.ZScreenKeyboardHook.KeyDownEvent += new KeyEventHandler(Loader.Worker.ScreenshotUsingHotkeys);
            if (Program.conf.CheckUpdates) Loader.Worker2.CheckUpdates();
        }

        internal void ZScreen_Windows7onlyTasks()
        {
            if (Program.conf.Windows7TaskbarIntegration)
            {
                this.ShowInTaskbar = true;
                Program.conf.MinimizeOnClose = true;
            }

            if (CoreHelpers.RunningOnWin7)
            {
                try
                {
                    Program.CheckFileRegistration();

                    Program.zWindowsTaskbar = TaskbarManager.Instance;
                    Program.zWindowsTaskbar.ApplicationId = Program.appId;

                    Program.zJumpList = JumpList.CreateJumpList();

                    // User Tasks - these are only added once
                    if (!Program.conf.UserTasksAdded)
                    {
                        JumpListLink jlCropShot = new JumpListLink(Path.Combine(Application.StartupPath, Loader.ZScreenCLI), "Crop Shot");
                        jlCropShot.Arguments = "crop_shot";
                        jlCropShot.IconReference = new IconReference(Application.ExecutablePath, 0);
                        Program.zJumpList.AddUserTasks(jlCropShot);

                        JumpListLink jlSelectedWindow = new JumpListLink(Path.Combine(Application.StartupPath, Loader.ZScreenCLI), "Selected Window");
                        jlSelectedWindow.Arguments = "selected_window";
                        jlSelectedWindow.IconReference = new IconReference(Application.ExecutablePath, 0);
                        Program.zJumpList.AddUserTasks(jlSelectedWindow);

                        Program.zJumpList.Refresh();
                        Program.conf.UserTasksAdded = true;
                    }

                    // Custom Categories
                    JumpListCustomCategory paths = new JumpListCustomCategory("Paths");

                    JumpListLink imagesJumpListLink = new JumpListLink(FileSystem.GetImagesDir(), "Images");
                    imagesJumpListLink.IconReference = new IconReference(Path.Combine("%windir%", "explorer.exe"), 0);

                    JumpListLink settingsJumpListLink = new JumpListLink(Program.SettingsDir, "Settings");
                    settingsJumpListLink.IconReference = new IconReference(Path.Combine("%windir%", "explorer.exe"), 0);

                    paths.AddJumpListItems(imagesJumpListLink, settingsJumpListLink);
                    Program.zJumpList.AddCustomCategories(paths);

                    // Taskbar Buttons
                    ThumbnailToolbarButton cropShot = new ThumbnailToolbarButton(Resources.shape_square_ico, "Crop Shot");
                    cropShot.Click += new EventHandler<ThumbnailButtonClickedEventArgs>(cropShot_Click);

                    ThumbnailToolbarButton selWindow = new ThumbnailToolbarButton(Resources.application_double_ico, "Selected Window");
                    selWindow.Click += new EventHandler<ThumbnailButtonClickedEventArgs>(selWindow_Click);

                    ThumbnailToolbarButton clipboardUpload = new ThumbnailToolbarButton(Resources.clipboard_upload_ico, "Clipboard Upload");
                    clipboardUpload.Click += new EventHandler<ThumbnailButtonClickedEventArgs>(clipboardUpload_Click);

                    Program.zWindowsTaskbar.ThumbnailToolbars.AddButtons(this.Handle, cropShot, selWindow, clipboardUpload);
                }
                catch (Exception ex)
                {
                    FileSystem.AppendDebug(ex);
                }
            }
        }

        private void ZScreen_SetFormSettings()
        {
            this.Icon = Resources.zss_main;
            this.Text = Program.GetProductName();
            this.niTray.Text = this.Text;
            this.lblLogo.Text = this.Text;
            chkWindows7TaskbarIntegration.Enabled = CoreHelpers.RunningOnWin7;

            if (this.WindowState == FormWindowState.Normal)
            {
                if (Program.conf.WindowLocation.IsEmpty)
                {
                    Program.conf.WindowLocation = this.Location;
                }
                if (Program.conf.WindowSize.IsEmpty)
                {
                    Program.conf.WindowSize = this.Size;
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

            // Options - Proxy
            ucProxyAccounts.btnAdd.Click += new EventHandler(ProxyAccountsAddButton_Click);
            ucProxyAccounts.btnRemove.Click += new EventHandler(ProxyAccountsRemoveButton_Click);
            ucProxyAccounts.btnTest.Click += new EventHandler(ProxyAccountTestButton_Click);
            ucProxyAccounts.AccountsList.SelectedIndexChanged += new EventHandler(ProxyAccountsList_SelectedIndexChanged);

            // Text Services - Text Uploaders
            ucTextUploaders.MyComboBox = cboTextUploaders;
            ucTextUploaders.btnItemAdd.Click += new EventHandler(TextUploadersAddButton_Click);
            ucTextUploaders.btnItemRemove.Click += new EventHandler(TextUploadersRemoveButton_Click);
            ucTextUploaders.MyCollection.SelectedIndexChanged += new EventHandler(TextUploaders_SelectedIndexChanged);
            ucTextUploaders.btnItemTest.Click += new EventHandler(TextUploaderTestButton_Click);

            // Text Services - URL Shorteners
            ucUrlShorteners.MyComboBox = cboURLShorteners;
            ucUrlShorteners.btnItemAdd.Click += new EventHandler(UrlShortenersAddButton_Click);
            ucUrlShorteners.btnItemRemove.Click += new EventHandler(UrlShortenersRemoveButton_Click);
            ucUrlShorteners.MyCollection.SelectedIndexChanged += new EventHandler(UrlShorteners_SelectedIndexChanged);
            ucUrlShorteners.btnItemTest.Click += new EventHandler(UrlShortenerTestButton_Click);

            // Watermark Codes Menu
            codesMenu.AutoClose = false;
            codesMenu.Font = new Font("Lucida Console", 8);
            codesMenu.Opacity = 0.8;
            codesMenu.ShowImageMargin = false;

            niTray.BalloonTipClicked += new EventHandler(niTray_BalloonTipClicked);

            DrawZScreenLabel(false);
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

        private void ZScreen_Load(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Normal)
            {
                Rectangle screenRect = GraphicsMgr.GetScreenBounds();
                screenRect.Inflate(-100, -100);
                if (screenRect.IntersectsWith(new Rectangle(Program.conf.WindowLocation, Program.conf.WindowSize)))
                {
                    this.Size = Program.conf.WindowSize;
                    this.Location = Program.conf.WindowLocation;
                }
            }

            if (Program.conf.ActionsToolbarMode)
            {
                this.Hide();
                Loader.Worker.ShowActionsToolbar(false);
            }
            else
            {
                if (Program.conf.OpenMainWindow)
                {
                    this.WindowState = Program.conf.WindowState;
                    ShowInTaskbar = Program.conf.ShowInTaskbar;
                }
                else if (Program.conf.ShowInTaskbar && Program.conf.MinimizeOnClose)
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

            niTray.Visible = true;
            FileSystem.AppendDebug("Loaded ZScreen GUI...");
        }

        private void ZScreen_ConfigGUI()
        {
            FileSystem.AppendDebug("Configuring ZScreen GUI..");

            #region Global

            //~~~~~~~~~~~~~~~~~~~~~
            //  Global
            //~~~~~~~~~~~~~~~~~~~~~

            pgApp.SelectedObject = Program.conf;
            pgIndexer.SelectedObject = Program.conf.IndexerConfig;
            txtRootFolder.Text = Program.RootAppFolder;
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

            if (cboImageUploaders.Items.Count == 0)
            {
                cboImageUploaders.Items.AddRange(typeof(ImageDestType).GetDescriptions());
            }
            cboImageUploaders.SelectedIndex = (int)Program.conf.ScreenshotDestMode;
            cboImageUploaders.Enabled = !Program.conf.PreferFileUploaderForImages;

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
            
            if (cboClipboardTextMode.Items.Count == 0)
            {
                cboClipboardTextMode.Items.AddRange(typeof(ClipboardUriType).GetDescriptions());
            }
            cboClipboardTextMode.SelectedIndex = (int)Program.conf.ClipboardUriMode;
            nudtScreenshotDelay.Time = Program.conf.ScreenshotDelayTimes;
            nudtScreenshotDelay.Value = Program.conf.ScreenshotDelayTime;
            chkManualNaming.Checked = Program.conf.ManualNaming;
            cbShowCursor.Checked = Program.conf.ShowCursor;
            cboCropGridMode.Checked = Program.conf.CropGridToggle;
            nudCropGridWidth.Value = Program.conf.CropGridSize.Width;
            nudCropGridHeight.Value = Program.conf.CropGridSize.Height;

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
            cbCropStyle.SelectedIndex = (int)Program.conf.CropRegionStyles;
            cbRegionRectangleInfo.Checked = Program.conf.CropRegionRectangleInfo;
            cbRegionHotkeyInfo.Checked = Program.conf.CropRegionHotkeyInfo;

            cbCropDynamicCrosshair.Checked = Program.conf.CropDynamicCrosshair;
            nudCropCrosshairInterval.Value = Program.conf.CropInterval;
            nudCropCrosshairStep.Value = Program.conf.CropStep;
            nudCrosshairLineCount.Value = Program.conf.CrosshairLineCount;
            nudCrosshairLineSize.Value = Program.conf.CrosshairLineSize;
            pbCropCrosshairColor.BackColor = XMLSettings.DeserializeColor(Program.conf.CropCrosshairColor);
            cbCropShowBigCross.Checked = Program.conf.CropShowBigCross;
            cbCropShowMagnifyingGlass.Checked = Program.conf.CropShowMagnifyingGlass;

            cbShowCropRuler.Checked = Program.conf.CropShowRuler;
            cbCropDynamicBorderColor.Checked = Program.conf.CropDynamicBorderColor;
            nudCropRegionInterval.Value = Program.conf.CropRegionInterval;
            nudCropRegionStep.Value = Program.conf.CropRegionStep;
            nudCropHueRange.Value = Program.conf.CropHueRange;
            pbCropBorderColor.BackColor = XMLSettings.DeserializeColor(Program.conf.CropBorderColor);
            nudCropBorderSize.Value = Program.conf.CropBorderSize;
            cbCropShowGrids.Checked = Program.conf.CropShowGrids;

            // Selected Window
            if (cbSelectedWindowStyle.Items.Count == 0)
            {
                cbSelectedWindowStyle.Items.AddRange(typeof(RegionStyles).GetDescriptions());
            }
            cbSelectedWindowStyle.SelectedIndex = (int)Program.conf.SelectedWindowRegionStyles;
            cbSelectedWindowRectangleInfo.Checked = Program.conf.SelectedWindowRectangleInfo;
            cbSelectedWindowRuler.Checked = Program.conf.SelectedWindowRuler;
            pbSelectedWindowBorderColor.BackColor = XMLSettings.DeserializeColor(Program.conf.SelectedWindowBorderColor);
            nudSelectedWindowBorderSize.Value = Program.conf.SelectedWindowBorderSize;
            cbSelectedWindowDynamicBorderColor.Checked = Program.conf.SelectedWindowDynamicBorderColor;
            nudSelectedWindowRegionInterval.Value = Program.conf.SelectedWindowRegionInterval;
            nudSelectedWindowRegionStep.Value = Program.conf.SelectedWindowRegionStep;
            nudSelectedWindowHueRange.Value = Program.conf.SelectedWindowHueRange;
            cbSelectedWindowCaptureObjects.Checked = Program.conf.SelectedWindowCaptureObjects;

            // Interaction
            nudFlashIconCount.Value = Program.conf.FlashTrayCount;
            chkCaptureFallback.Checked = Program.conf.CaptureEntireScreenOnError;
            cbShowPopup.Checked = Program.conf.ShowBalloonTip;
            chkBalloonTipOpenLink.Checked = Program.conf.BalloonTipOpenLink;
            cbShowUploadDuration.Checked = Program.conf.ShowUploadDuration;
            cbCompleteSound.Checked = Program.conf.CompleteSound;
            cbCloseDropBox.Checked = Program.conf.CloseDropBox;
            cbCloseQuickActions.Checked = Program.conf.CloseQuickActions;

            // Watermark
            if (cboWatermarkType.Items.Count == 0)
            {
                cboWatermarkType.Items.AddRange(typeof(WatermarkType).GetDescriptions());
            }
            cboWatermarkType.SelectedIndex = (int)Program.conf.WatermarkMode;
            if (cbWatermarkPosition.Items.Count == 0)
            {
                cbWatermarkPosition.Items.AddRange(typeof(WatermarkPositionType).GetDescriptions());
            }
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
            trackWatermarkBackgroundTrans.Value = (int)Program.conf.WatermarkBackTrans;
            if (cbWatermarkGradientType.Items.Count == 0)
            {
                cbWatermarkGradientType.Items.AddRange(Enum.GetNames(typeof(LinearGradientMode)));
            }
            cbWatermarkGradientType.SelectedIndex = (int)Program.conf.WatermarkGradientType;

            txtWatermarkImageLocation.Text = Program.conf.WatermarkImageLocation;
            cbWatermarkUseBorder.Checked = Program.conf.WatermarkUseBorder;
            nudWatermarkImageScale.Value = Program.conf.WatermarkImageScale;

            TestWatermark();

            // Image Settings

            if (cbFileFormat.Items.Count == 0) cbFileFormat.Items.AddRange(Program.zImageFileTypes);
            cbFileFormat.SelectedIndex = Program.conf.FileFormat;
            nudImageQuality.Value = Program.conf.ImageQuality;
            nudSwitchAfter.Value = Program.conf.SwitchAfter;
            if (cbSwitchFormat.Items.Count == 0) cbSwitchFormat.Items.AddRange(Program.zImageFileTypes);
            cbSwitchFormat.SelectedIndex = Program.conf.SwitchFormat;

            switch (Program.conf.ImageSizeType)
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
            txtImageSizeFixedWidth.Text = Program.conf.ImageSizeFixedWidth.ToString();
            txtImageSizeFixedHeight.Text = Program.conf.ImageSizeFixedHeight.ToString();
            txtImageSizeRatio.Text = Program.conf.ImageSizeRatioPercentage.ToString();

            #endregion

            #region Text Uploaders & URL Shorteners

            ///////////////////////////////////
            // Text Uploader Settings
            ///////////////////////////////////

            if (Program.conf.TextUploadersList.Count == 0)
            {
                Program.conf.TextUploadersList = new List<TextUploader> { new PastebinUploader(), new Paste2Uploader(), new SlexyUploader() };
            }
            ucTextUploaders.MyCollection.Items.Clear();
            cboTextUploaders.Items.Clear();
            foreach (TextUploader textUploader in Program.conf.TextUploadersList)
            {
                if (textUploader != null)
                {
                    ucTextUploaders.MyCollection.Items.Add(textUploader);
                    cboTextUploaders.Items.Add(textUploader);
                }
            }
            if (Program.conf.TextUploaderSelected > -1 && Program.conf.TextUploaderSelected < ucTextUploaders.MyCollection.Items.Count)
            {
                ucTextUploaders.MyCollection.SelectedIndex = Program.conf.TextUploaderSelected;
                cboTextUploaders.SelectedIndex = Program.conf.TextUploaderSelected;
            }
            cboTextUploaders.Enabled = !Program.conf.PreferFileUploaderForText;

            ucTextUploaders.Templates.Items.Clear();
            ucTextUploaders.Templates.Items.AddRange(typeof(TextDestType).GetDescriptions());
            ucTextUploaders.Templates.SelectedIndex = 1;

            ///////////////////////////////////
            // URL Shorteners Settings
            ///////////////////////////////////

            if (Program.conf.UrlShortenersList.Count == 0)
            {
                Program.conf.UrlShortenersList = new List<TextUploader> { new ThreelyUploader(), new BitlyUploader(),
                    new IsgdUploader(), new KlamUploader(), new TinyURLUploader() };
            }

            ucUrlShorteners.MyCollection.Items.Clear();
            cboURLShorteners.Items.Clear();
            foreach (TextUploader textUploader in Program.conf.UrlShortenersList)
            {
                if (textUploader != null)
                {
                    ucUrlShorteners.MyCollection.Items.Add(textUploader);
                    cboURLShorteners.Items.Add(textUploader);
                }
            }

            if (Program.conf.UrlShortenerSelected > -1 && Program.conf.UrlShortenerSelected < ucUrlShorteners.MyCollection.Items.Count)
            {
                ucUrlShorteners.MyCollection.SelectedIndex = Program.conf.UrlShortenerSelected;
                cboURLShorteners.SelectedIndex = Program.conf.UrlShortenerSelected;
            }

            ucUrlShorteners.Templates.Items.Clear();
            ucUrlShorteners.Templates.Items.AddRange(typeof(UrlShortenerType).GetDescriptions());
            ucUrlShorteners.Templates.SelectedIndex = 0;

            #endregion

            #region FTP Settings

            if (Program.conf.FTPAccountList == null || Program.conf.FTPAccountList.Count == 0)
            {
                FTPSetup(new List<FTPAccount>());
            }
            else
            {
                FTPSetup(Program.conf.FTPAccountList);
                if (ucFTPAccounts.AccountsList.Items.Count > 0)
                {
                    ucFTPAccounts.AccountsList.SelectedIndex = Program.conf.FTPSelected;
                }
            }
            chkEnableThumbnail.Checked = Program.conf.FTPCreateThumbnail;
            txtFTPThumbWidth.Text = Program.conf.FTPThumbnailWidth.ToString();
            txtFTPThumbHeight.Text = Program.conf.FTPThumbnailHeight.ToString();
            cbFTPThumbnailCheckSize.Checked = Program.conf.FTPThumbnailCheckSize;

            #endregion

            #region MindTouch Settings

            ///////////////////////////////////
            // MindTouch Deki Wiki Settings
            ///////////////////////////////////

            if (Program.conf.DekiWikiAccountList == null || Program.conf.DekiWikiAccountList.Count == 0)
            {
                DekiWikiSetup(new List<DekiWikiAccount>());
            }
            else
            {
                DekiWikiSetup(Program.conf.DekiWikiAccountList);
                if (ucMindTouchAccounts.AccountsList.Items.Count > 0)
                {
                    ucMindTouchAccounts.AccountsList.SelectedIndex = Program.conf.DekiWikiSelected;
                }
            }
            chkDekiWikiForcePath.Checked = Program.conf.DekiWikiForcePath;

            #endregion

            #region Image Uploaders

            ///////////////////////////////////
            // Image Uploader Settings
            ///////////////////////////////////

            // TinyPic

            txtTinyPicShuk.Text = Program.conf.TinyPicShuk;
            chkRememberTinyPicUserPass.Checked = Program.conf.RememberTinyPicUserPass;

            // ImageShack

            txtImageShackRegistrationCode.Text = Program.conf.ImageShackRegistrationCode;
            txtUserNameImageShack.Text = Program.conf.ImageShackUserName;
            chkPublicImageShack.Checked = Program.conf.ImageShackShowImagesInPublic;

            // TwitPic

            txtTwitPicUserName.Text = Program.conf.TwitterUserName;
            txtTwitPicPassword.Text = Program.conf.TwitterPassword;
            if (cboTwitPicUploadMode.Items.Count == 0)
            {
                cboTwitPicUploadMode.Items.AddRange(typeof(TwitPicUploadType).GetDescriptions());
            }
            cboTwitPicUploadMode.SelectedIndex = (int)Program.conf.TwitPicUploadMode;
            cbTwitPicShowFull.Checked = Program.conf.TwitPicShowFull;
            if (cboTwitPicThumbnailMode.Items.Count == 0)
            {
                cboTwitPicThumbnailMode.Items.AddRange(typeof(TwitPicThumbnailType).GetDescriptions());
            }
            cboTwitPicThumbnailMode.SelectedIndex = (int)Program.conf.TwitPicThumbnailMode;

            // yFrog
            if (cboYfrogUploadMode.Items.Count == 0)
            {
                cboYfrogUploadMode.Items.AddRange(typeof(YfrogUploadType).GetDescriptions());
            }
            cboYfrogUploadMode.SelectedIndex = (int)Program.conf.YfrogUploadMode;

            // ImageBam

            txtImageBamApiKey.Text = Program.conf.ImageBamApiKey;
            txtImageBamSecret.Text = Program.conf.ImageBamSecret;
            chkImageBamContentNSFW.Checked = Program.conf.ImageBamContentNSFW;
            if (Program.conf.ImageBamGallery.Count == 0)
            {
                Program.conf.ImageBamGallery.Add("");
            }
            foreach (string id in Program.conf.ImageBamGallery)
            {
                lbImageBamGalleries.Items.Add(id);
            }
            if (lbImageBamGalleries.Items.Count > Program.conf.ImageBamGalleryActive)
            {
                lbImageBamGalleries.SelectedIndex = Program.conf.ImageBamGalleryActive;
            }
            else
            {
                lbImageBamGalleries.SelectedIndex = 0;
            }

            #endregion

            #region File Uploaders

            if (cboFileUploaders.Items.Count == 0)
            {
                cboFileUploaders.Items.AddRange(typeof(FileUploaderType).GetDescriptions());
            }
            cboFileUploaders.SelectedIndex = (int)Program.conf.FileDestMode;

            // RapidShare
            if (cboRapidShareAcctType.Items.Count == 0)
            {
                cboRapidShareAcctType.Items.AddRange(typeof(RapidShareAcctType).GetDescriptions());
            }
            cboRapidShareAcctType.SelectedIndex = (int)Program.conf.RapidShareAccountType;
            txtRapidShareCollectorID.Text = Program.conf.RapidShareCollectorsID;
            txtRapidSharePassword.Text = Program.conf.RapidSharePassword;
            txtRapidSharePremiumUserName.Text = Program.conf.RapidSharePremiumUserName;

            // SendSpace
            if (cboSendSpaceAcctType.Items.Count == 0)
            {
                cboSendSpaceAcctType.Items.AddRange(typeof(AcctType).GetDescriptions());
            }
            cboSendSpaceAcctType.SelectedIndex = (int)Program.conf.SendSpaceAccountType;
            txtSendSpacePassword.Text = Program.conf.SendSpacePassword;
            txtSendSpaceUserName.Text = Program.conf.SendSpaceUserName;
            #endregion

            // Others

            cbAutoSwitchFileUploader.Checked = Program.conf.AutoSwitchFileUploader;
            nudErrorRetry.Value = Program.conf.ErrorRetryCount;
            cboImageUploadRetryOnTimeout.Checked = Program.conf.ImageUploadRetryOnTimeout;
            nudUploadDurationLimit.Value = Program.conf.UploadDurationLimit;

            if (cboUploadMode.Items.Count == 0)
            {
                cboUploadMode.Items.AddRange(typeof(UploadMode).GetDescriptions());
            }
            cboUploadMode.SelectedIndex = (int)Program.conf.UploadMode;
            chkImageUploadRetryOnFail.Checked = Program.conf.ImageUploadRetryOnFail;
            cbClipboardTranslate.Checked = Program.conf.ClipboardTranslate;
            cbAutoTranslate.Checked = Program.conf.AutoTranslate;
            txtAutoTranslate.Text = Program.conf.AutoTranslateLength.ToString();
            cbAddFailedScreenshot.Checked = Program.conf.AddFailedScreenshot;
            cbTinyPicSizeCheck.Checked = Program.conf.TinyPicSizeCheck;

            // Web Page Upload

            cbWebPageUseCustomSize.Checked = Program.conf.WebPageUseCustomSize;
            txtWebPageWidth.Text = Program.conf.WebPageWidth.ToString();
            txtWebPageHeight.Text = Program.conf.WebPageHeight.ToString();
            cbWebPageAutoUpload.Checked = Program.conf.WebPageAutoUpload;

            #region Image Editors

            ///////////////////////////////////
            // Image Editors Settings
            ///////////////////////////////////

            Software disabled = new Software(Program.DISABLED_IMAGE_EDITOR, "", true);
            Software editor = new Software(Program.ZSCREEN_IMAGE_EDITOR, "", true);
            Software paint = new Software("Paint", Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.System), "mspaint.exe"), true);

            Program.conf.ImageEditors.RemoveAll(x => x.Path == "" || x.Name == Program.DISABLED_IMAGE_EDITOR ||
                x.Name == Program.ZSCREEN_IMAGE_EDITOR || x.Name == "Paint" || !File.Exists(x.Path));

            Program.conf.ImageEditors.Insert(0, disabled);
            Program.conf.ImageEditors.Insert(1, editor);
            if (File.Exists(paint.Path)) Program.conf.ImageEditors.Insert(2, paint);

            RegistryMgr.FindImageEditors();

            lbImageSoftware.Items.Clear();

            foreach (Software app in Program.conf.ImageEditors)
            {
                if (!String.IsNullOrEmpty(app.Name))
                {
                    lbImageSoftware.Items.Add(app.Name);
                }
            }

            int i;
            if (Program.conf.ImageEditor != null && (i = lbImageSoftware.Items.IndexOf(Program.conf.ImageEditor.Name)) != -1)
            {
                lbImageSoftware.SelectedIndex = i;
            }
            else if (lbImageSoftware.Items.Count > 0)
            {
                lbImageSoftware.SelectedIndex = 0;
            }

            chkImageEditorAutoSave.Checked = Program.conf.ImageEditorAutoSave;

            // Text Editors

            if (Program.conf.TextEditors.Count == 0)
            {
                Program.conf.TextEditors.Add(new Software("Notepad", Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.System), "notepad.exe"), true));
            }

            #endregion

            #region Options

            chkStartWin.Checked = RegistryMgr.CheckStartWithWindows();
            chkOpenMainWindow.Checked = Program.conf.OpenMainWindow;
            chkShowTaskbar.Checked = Program.conf.ShowInTaskbar;
            chkShowTaskbar.Enabled = !Program.conf.Windows7TaskbarIntegration;
            cbShowHelpBalloonTips.Checked = Program.conf.ShowHelpBalloonTips;
            cbSaveFormSizePosition.Checked = Program.conf.SaveFormSizePosition;
            cbLockFormSize.Checked = Program.conf.LockFormSize;
            cbAutoSaveSettings.Checked = Program.conf.AutoSaveSettings;
            chkWindows7TaskbarIntegration.Checked = Program.conf.Windows7TaskbarIntegration;

            #endregion



            chkProxyEnable.Checked = Program.conf.ProxyEnabled;
            ttZScreen.Active = Program.conf.ShowHelpBalloonTips;

            cbCheckUpdates.Checked = Program.conf.CheckUpdates;
            cbCheckUpdatesBeta.Checked = Program.conf.CheckUpdatesBeta;
            nudCacheSize.Value = Program.conf.ScreenshotCacheSize;
            cbDeleteLocal.Checked = Program.conf.DeleteLocal;

            FolderWatcher zWatcher = new FolderWatcher(this);
            zWatcher.FolderPath = Program.conf.FolderMonitorPath;
            if (Program.conf.FolderMonitoring)
            {
                zWatcher.StartWatching();
            }
            else
            {
                zWatcher.StopWatching();
            }

            // Naming Conventions
            txtActiveWindow.Text = Program.conf.ActiveWindowPattern;
            txtEntireScreen.Text = Program.conf.EntireScreenPattern;
            txtImagesFolderPattern.Text = Program.conf.SaveFolderPattern;

            // Proxy Settings
            Proxyetup(Program.conf.ProxyList);
            if (ucProxyAccounts.AccountsList.Items.Count > 0)
            {
                ucProxyAccounts.AccountsList.SelectedIndex = Program.conf.ProxySelected;
            }

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

            #region History

            if (mGuiIsReady)
            {
                nudHistoryMaxItems.Value = Program.conf.HistoryMaxNumber;
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
            cboImageUploaders.SelectedIndex = (int)tsmi.Tag;
        }

        private void UpdateGuiControlsPaths()
        {
            Program.InitializeDefaultFolderPaths();
            txtImagesDir.Text = Program.ImagesDir;
            txtCacheDir.Text = Program.CacheDir;
            txtSettingsDir.Text = Program.SettingsDir;
        }

        private void cbCloseQuickActions_CheckedChanged(object sender, EventArgs e)
        {
            Program.conf.CloseQuickActions = cbCloseQuickActions.Checked;
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
            Loader.Worker.ShowQuickOptions();
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
                    if (!Program.conf.ShowInTaskbar) this.Hide();

                    if (Program.conf.AutoSaveSettings) WriteSettings();
                }
                else if (this.WindowState == FormWindowState.Normal)
                {
                    this.ShowInTaskbar = Program.conf.ShowInTaskbar;
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
                if (Program.conf.MinimizeOnClose)
                {
                    this.WindowState = FormWindowState.Minimized;
                }
                else if (e.CloseReason == CloseReason.UserClosing)
                {
                    Hide();
                }
            }
        }

        private void WriteSettings()
        {
            if (mGuiIsReady && Program.conf.SaveFormSizePosition && this.WindowState == FormWindowState.Normal)
            {
                Program.conf.WindowLocation = this.Location;
                Program.conf.WindowSize = this.Size;
            }
            Program.conf.WindowState = this.WindowState;

            Program.conf.Write();
            Loader.Worker.SaveHistoryItems();

            FileSystem.AppendDebug("Settings written to file.");
        }

        private void RewriteImageEditorsRightClickMenu()
        {
            if (Program.conf.ImageEditors != null)
            {
                tsmEditinImageSoftware.DropDownDirection = ToolStripDropDownDirection.Right;

                tsmEditinImageSoftware.DropDownItems.Clear();

                List<Software> imgs = Program.conf.ImageEditors;

                //tsm.TextDirection = ToolStripTextDirection.Horizontal;
                tsmEditinImageSoftware.DropDownDirection = ToolStripDropDownDirection.Right;

                for (int x = 0; x < imgs.Count; x++)
                {
                    ToolStripMenuItem tsm = new ToolStripMenuItem { Text = imgs[x].Name, CheckOnClick = true };
                    tsm.Click += new EventHandler(TrayImageEditorClick);
                    tsmEditinImageSoftware.DropDownItems.Add(tsm);
                    if (imgs[x].Name == Program.DISABLED_IMAGE_EDITOR)
                    {
                        tsmEditinImageSoftware.DropDownItems.Add(new ToolStripSeparator());
                    }
                }

                //check the active ftpUpload account

                if (Adapter.ImageSoftwareEnabled())
                {
                    CheckCorrectIsRightClickMenu(Program.conf.ImageEditor.Name);
                }
                else
                {
                    CheckCorrectIsRightClickMenu(Program.DISABLED_IMAGE_EDITOR);
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

        private void DisableImageSoftwareClick(object sender, EventArgs e)
        {
            //cbRunImageSoftware.Checked = false;

            //select "Disabled"
            lbImageSoftware.SelectedIndex = 0;

            CheckCorrectIsRightClickMenu(tsmEditinImageSoftware.DropDownItems[0].Text); //disabled
            //rewriteISRightClickMenu();
        }

        private void TrayImageEditorClick(object sender, EventArgs e)
        {
            ToolStripMenuItem tsm = (ToolStripMenuItem)sender;

            Program.conf.ImageEditor = GetImageSoftware(tsm.Text); //Program.conf.ImageSoftwareList[(int)tsm.Tag];

            if (lbImageSoftware.Items.IndexOf(tsm.Text) >= 0)
                lbImageSoftware.SelectedItem = tsm.Text;

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
                ToolStripMenuItem tsmDestFTP = GetImageDestMenuItem(ImageDestType.FTP);
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
                CheckCorrectMenuItemClicked(ref tsmDestFTP, Program.conf.FTPSelected);
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

        private void btnBrowseImageSoftware_Click(object sender, EventArgs e)
        {
            if (lbImageSoftware.SelectedIndex > -1)
            {
                Software temp = BrowseImageSoftware();
                if (temp != null)
                {
                    lbImageSoftware.Items[lbImageSoftware.SelectedIndex] = temp;
                    Program.conf.ImageEditors[lbImageSoftware.SelectedIndex] = temp;
                    ShowImageEditorsSettings();
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
            if (Program.conf.FTPAccountList.Count > 0)
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

        private void btnBrowseConfig_Click(object sender, EventArgs e)
        {
            ShowDirectory(Program.SettingsDir);
        }

        private void tsmLic_Click(object sender, EventArgs e)
        {
            FormsMgr.ShowLicense();
        }

        private void chkManualNaming_CheckedChanged(object sender, EventArgs e)
        {
            Program.conf.ManualNaming = chkManualNaming.Checked;
        }

        private void ZScreen_Shown(object sender, EventArgs e)
        {
            mGuiIsReady = true;

            if (!Program.conf.RunOnce)
            {
                Show();
                WindowState = FormWindowState.Normal;
                this.Activate();
                this.BringToFront();
                Program.conf.RunOnce = true;
            }

            if (Program.conf.BackupFTPSettings)
            {
                FileSystem.BackupFTPSettings();
            }

            if (Program.conf.BackupApplicationSettings)
            {
                FileSystem.BackupAppSettings();
            }

            // Loader.Splash.Close();
            if (Program.conf.Windows7TaskbarIntegration)
            {
                ZScreen_Windows7onlyTasks();
                if (!Program.conf.OpenMainWindow)
                {
                    this.WindowState = FormWindowState.Minimized;
                    this.ShowInTaskbar = true;
                }
            }
        }

        private void clipboardUpload_Click(object sender, EventArgs e)
        {
            Loader.Worker.UploadUsingClipboard();
        }

        private void selWindow_Click(object sender, EventArgs e)
        {
            Loader.Worker.StartBW_SelectedWindow();
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
            Program.conf.FlashTrayCount = nudFlashIconCount.Value;
        }

        private void nudCacheSize_ValueChanged(object sender, EventArgs e)
        {
            Program.conf.ScreenshotCacheSize = nudCacheSize.Value;
        }

        private void txtCacheDir_TextChanged(object sender, EventArgs e)
        {
            Program.CacheDir = txtCacheDir.Text;
        }

        private void btnSettingsExport_Click(object sender, EventArgs e)
        {
            SaveFileDialog dlg = new SaveFileDialog { Filter = Program.FILTER_SETTINGS };
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                Program.conf.Write(dlg.FileName);
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
                    ZScreen_ConfigGUI();
                }
            }
        }

        private void AddImageSoftwareToList(Software temp)
        {
            if (temp != null)
            {
                Program.conf.ImageEditors.Add(temp);
                lbImageSoftware.Items.Add(temp);
                lbImageSoftware.SelectedIndex = lbImageSoftware.Items.Count - 1;
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
            int sel = lbImageSoftware.SelectedIndex;

            if (sel != -1)
            {
                Program.conf.ImageEditors.RemoveAt(sel);

                lbImageSoftware.Items.RemoveAt(sel);

                if (lbImageSoftware.Items.Count > 0)
                {
                    lbImageSoftware.SelectedIndex = (sel > 0) ? (sel - 1) : 0;
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

        private void cboScreenshotDest_SelectedIndexChanged(object sender, EventArgs e)
        {
            ImageDestType sdt = (ImageDestType)cboImageUploaders.SelectedIndex;
            Program.conf.ScreenshotDestMode = sdt;
            cboClipboardTextMode.Enabled = sdt != ImageDestType.CLIPBOARD && sdt != ImageDestType.FILE;

            CheckSendToMenu(GetImageDestMenuItem(sdt));
        }

        private void CheckSendToMenu(ToolStripMenuItem item)
        {
            CheckToolStripMenuItem(tsmImageDest, item);
        }

        private void CheckToolStripMenuItem(ToolStripDropDownItem parent, ToolStripMenuItem item)
        {
            foreach (ToolStripMenuItem tsmi in parent.DropDownItems)
            {
                tsmi.Checked = tsmi == item;
            }

            tsmCopytoClipboardMode.Enabled = cboImageUploaders.SelectedIndex != (int)ImageDestType.CLIPBOARD &&
                cboImageUploaders.SelectedIndex != (int)ImageDestType.FILE;
        }

        private void SetActiveImageSoftware()
        {
            Program.conf.ImageEditor = Program.conf.ImageEditors[lbImageSoftware.SelectedIndex];
            RewriteImageEditorsRightClickMenu();
        }

        private void ShowImageEditorsSettings()
        {
            if (lbImageSoftware.SelectedItem != null)
            {
                Software app = GetImageSoftware(lbImageSoftware.SelectedItem.ToString());
                if (app != null)
                {
                    btnBrowseImageEditor.Enabled = !app.Protected;
                    pgEditorsImage.SelectedObject = app;
                    pgEditorsImage.Enabled = !app.Protected;
                    btnRemoveImageEditor.Enabled = !app.Protected;

                    gbImageEditorSettings.Visible = app.Name == Program.ZSCREEN_IMAGE_EDITOR;

                    SetActiveImageSoftware();
                }
            }
        }

        private void lbImageSoftware_SelectedIndexChanged(object sender, EventArgs e)
        {
            ShowImageEditorsSettings();
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
            Program.ImagesDir = txtImagesDir.Text;
        }

        private void cbDeleteLocal_CheckedChanged(object sender, EventArgs e)
        {
            Program.conf.DeleteLocal = cbDeleteLocal.Checked;
        }

        private void txtActiveWindow_TextChanged(object sender, EventArgs e)
        {
            Program.conf.ActiveWindowPattern = txtActiveWindow.Text;
            lblActiveWindowPreview.Text = NameParser.Convert(new NameParserInfo(NameParserType.ActiveWindow) { IsPreview = true });
        }

        private void txtEntireScreen_TextChanged(object sender, EventArgs e)
        {
            Program.conf.EntireScreenPattern = txtEntireScreen.Text;
            lblEntireScreenPreview.Text = NameParser.Convert(new NameParserInfo(NameParserType.EntireScreen) { IsPreview = true });
        }

        private void cmbFileFormat_SelectedIndexChanged(object sender, EventArgs e)
        {
            Program.conf.FileFormat = cbFileFormat.SelectedIndex;
        }

        private void txtImageQuality_ValueChanged(object sender, EventArgs e)
        {
            Program.conf.ImageQuality = nudImageQuality.Value;
        }

        private void cmbSwitchFormat_SelectedIndexChanged(object sender, EventArgs e)
        {
            Program.conf.SwitchFormat = cbSwitchFormat.SelectedIndex;
        }

        private void txtImageShackRegistrationCode_TextChanged(object sender, EventArgs e)
        {
            if (mGuiIsReady)
            {
                Program.conf.ImageShackRegistrationCode = txtImageShackRegistrationCode.Text;
            }
        }

        private void cbShowPopup_CheckedChanged(object sender, EventArgs e)
        {
            Program.conf.ShowBalloonTip = cbShowPopup.Checked;
        }

        private void LoadSettingsDefault()
        {
            Program.conf = new XMLSettings();
            ZScreen_ConfigGUI();
            Program.conf.RunOnce = true;
            Program.conf.Write();
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
            Loader.Worker.StartBW_CropShot();
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
                    FileSystem.AppendDebug(ex.ToString());
                }
            }
        }

        #region Image MyCollection

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
                Loader.Worker.StartWorkerScreenshots(WorkerTask.Jobs.CustomUploaderTest);
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
            OpenSource(UploadManager.GetLastImageUpload(), sType);
        }

        private bool OpenSource(ImageFileManager ifm, ImageFileManager.SourceType sType)
        {
            string path = ifm.GetSource(Program.TempDir, sType);
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

            dgvHotkeys.Refresh();
        }

        private void AddHotkey(string name)
        {
            object obj = Program.conf.GetFieldValue("Hotkey" + name.Replace(" ", ""));
            if (obj != null && obj.GetType() == typeof(Keys))
            {
                dgvHotkeys.Rows.Add(name, ((Keys)obj).ToSpecialString());
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
            txtTinyPicShuk.Text = Adapter.GetTinyPicShuk();
            this.BringToFront();
        }

        private void txtTinyPicShuk_TextChanged(object sender, EventArgs e)
        {
            Program.conf.TinyPicShuk = txtTinyPicShuk.Text;
        }

        private void CheckFormSettings()
        {
            if (Program.conf.LockFormSize)
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
            Program.conf.CropRegionStyles = (RegionStyles)cbCropStyle.SelectedIndex;
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
                    bool checkImage = GraphicsMgr.IsValidImage(hi.LocalPath);
                    bool checkText = FileSystem.IsValidTextFile(hi.LocalPath);
                    bool checkWebpage = FileSystem.IsValidWebpageFile(hi.LocalPath) || (checkImage && Program.conf.PreferBrowserForImages) || (checkText && Program.conf.PreferBrowserForText);
                    bool checkBinary = !checkImage && !checkText && !checkWebpage;

                    historyBrowser.Visible = checkWebpage;
                    pbPreview.Visible = checkImage || (!checkText && checkRemote) && !checkWebpage || checkBinary;
                    txtPreview.Visible = checkText && !checkWebpage;

                    tsmCopyCbHistory.Enabled = checkRemote;
                    cmsHistory.Enabled = checkLocal;
                    browseURLToolStripMenuItem.Enabled = checkRemote;
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
                            pbPreview.LoadAsync(hi.LocalPath);
                            pbPreview.LoadCompleted += new AsyncCompletedEventHandler(pbPreview_LoadCompleted);
                        }
                        else if (checkRemote)
                        {
                            pbPreview.Image = Resources.ajax_loader;
                            pbPreview.SizeMode = PictureBoxSizeMode.CenterImage;
                            pbPreview.LoadAsync(hi.RemotePath);
                            pbPreview.LoadCompleted += new AsyncCompletedEventHandler(pbPreview_LoadCompleted);
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

                if (Program.conf.HistoryShowTooltips && hi != null)
                {
                    ttZScreen.SetToolTip(lbHistory, hi.GetStatistics());
                    ttZScreen.SetToolTip(pbPreview, hi.GetStatistics());
                }
            }
        }

        private void pbPreview_LoadCompleted(object sender, AsyncCompletedEventArgs e)
        {
            if (!e.Cancelled && e.Error == null)
            {
                pbPreview.SizeMode = PictureBoxSizeMode.Zoom;
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
            Program.conf.ShowCursor = cbShowCursor.Checked;
        }

        private void btnWatermarkFont_Click(object sender, EventArgs e)
        {
            try
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
            catch (Exception ex)
            {
                FileSystem.AppendDebug(ex.ToString());
            }
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
            Loader.Worker.StartBW_EntireScreen();
        }

        private void selectedWindowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Thread.Sleep(300);
            Loader.Worker.StartBW_SelectedWindow();
        }

        private void rectangularRegionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Thread.Sleep(300);
            Loader.Worker.StartBW_CropShot();
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
            ShowDirectory(Program.CacheDir);
        }

        private void cbOpenMainWindow_CheckedChanged(object sender, EventArgs e)
        {
            Program.conf.OpenMainWindow = chkOpenMainWindow.Checked;
        }

        private void cbShowTaskbar_CheckedChanged(object sender, EventArgs e)
        {
            Program.conf.ShowInTaskbar = chkShowTaskbar.Checked;
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
                GoogleTranslate.FindLanguage(Program.conf.FromLanguage, ZScreen.mGTranslator.LanguageOptions.SourceLangList),
                GoogleTranslate.FindLanguage(Program.conf.ToLanguage, ZScreen.mGTranslator.LanguageOptions.TargetLangList)));
        }


        private void cbFromLanguage_SelectedIndexChanged(object sender, EventArgs e)
        {
            Program.conf.FromLanguage = ZScreen.mGTranslator.LanguageOptions.SourceLangList[cbFromLanguage.SelectedIndex].Value;
        }

        private void cbToLanguage_SelectedIndexChanged(object sender, EventArgs e)
        {
            Program.conf.ToLanguage = ZScreen.mGTranslator.LanguageOptions.TargetLangList[cbToLanguage.SelectedIndex].Value;
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
                btnTranslateMethod();
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
            Loader.Worker.HistoryRetryUpload((HistoryItem)lbHistory.SelectedItem);
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

        private void btnCopyStats_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(txtDebugInfo.Text);
        }

        private void cbImageUploadRetry_CheckedChanged(object sender, EventArgs e)
        {
            Program.conf.ImageUploadRetryOnFail = chkImageUploadRetryOnFail.Checked;
        }

        private void DekiWikiSetup(IEnumerable<DekiWikiAccount> accs)
        {
            if (accs != null)
            {
                ucMindTouchAccounts.AccountsList.Items.Clear();
                Program.conf.DekiWikiAccountList = new List<DekiWikiAccount>();
                Program.conf.DekiWikiAccountList.AddRange(accs);
                foreach (DekiWikiAccount acc in Program.conf.DekiWikiAccountList)
                {
                    ucMindTouchAccounts.AccountsList.Items.Add(acc);
                }
            }
        }

        private void Proxyetup(IEnumerable<ProxyInfo> accs)
        {
            if (accs != null)
            {
                ucProxyAccounts.AccountsList.Items.Clear();
                Program.conf.ProxyList = new List<ProxyInfo>();
                Program.conf.ProxyList.AddRange(accs);
                foreach (ProxyInfo acc in Program.conf.ProxyList)
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
                Program.conf.FTPAccountList = new List<FTPAccount>();
                Program.conf.FTPAccountList.AddRange(accs);
                foreach (FTPAccount acc in Program.conf.FTPAccountList)
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
                Program.conf.FTPAccountList.RemoveAt(sel);
            }
        }

        private void MindTouchAccountRemoveButton_Click(object sender, EventArgs e)
        {
            int sel = ucMindTouchAccounts.AccountsList.SelectedIndex;
            if (ucMindTouchAccounts.RemoveItem(sel) == true)
            {
                Program.conf.DekiWikiAccountList.RemoveAt(sel);
            }
        }

        private void FTPAccountsList_SelectedIndexChanged(object sender, EventArgs e)
        {
            int sel = ucFTPAccounts.AccountsList.SelectedIndex;
            Program.conf.FTPSelected = sel;
            if (Program.conf.FTPAccountList != null && sel != -1 && sel < Program.conf.FTPAccountList.Count && Program.conf.FTPAccountList[sel] != null)
            {
                FTPAccount acc = Program.conf.FTPAccountList[sel];
                ucFTPAccounts.SettingsGrid.SelectedObject = acc;
                RewriteFTPRightClickMenu();
            }
        }

        private void MindTouchAccountsList_SelectedIndexChanged(object sender, EventArgs e)
        {
            int sel = ucMindTouchAccounts.AccountsList.SelectedIndex;
            Program.conf.DekiWikiSelected = sel;
            if (Program.conf.DekiWikiAccountList != null && sel != -1 && sel < Program.conf.DekiWikiAccountList.Count && Program.conf.DekiWikiAccountList[sel] != null)
            {
                DekiWikiAccount acc = Program.conf.DekiWikiAccountList[sel];
                ucMindTouchAccounts.SettingsGrid.SelectedObject = acc;
                // RewriteFTPRightClickMenu();
            }
        }

        private void FTPAccountAddButton_Click(object sender, EventArgs e)
        {
            FTPAccount acc = new FTPAccount("New Account");
            Program.conf.FTPAccountList.Add(acc);
            ucFTPAccounts.AccountsList.Items.Add(acc);
            ucFTPAccounts.AccountsList.SelectedIndex = ucFTPAccounts.AccountsList.Items.Count - 1;
        }

        private void MindTouchAccountAddButton_Click(object sender, EventArgs e)
        {
            DekiWikiAccount acc = new DekiWikiAccount("New Account");
            Program.conf.DekiWikiAccountList.Add(acc);
            ucMindTouchAccounts.AccountsList.Items.Add(acc);
            ucMindTouchAccounts.AccountsList.SelectedIndex = ucMindTouchAccounts.AccountsList.Items.Count - 1;
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

        private ProxyInfo GetSelectedProxy()
        {
            ProxyInfo acc = null;
            if (ucProxyAccounts.AccountsList.SelectedIndex != -1 && Program.conf.ProxyList.Count >= ucProxyAccounts.AccountsList.Items.Count)
            {
                acc = Program.conf.ProxyList[ucProxyAccounts.AccountsList.SelectedIndex];
            }
            return acc;
        }

        private FTPAccount GetSelectedFTP()
        {
            FTPAccount acc = null;
            if (Adapter.CheckFTPAccounts())
            {
                acc = Program.conf.FTPAccountList[Program.conf.FTPSelected];
            }
            return acc;
        }

        private DekiWikiAccount GetSelectedDekiWiki()
        {
            DekiWikiAccount acc = null;
            if (Adapter.CheckDekiWikiAccounts())
            {
                acc = Program.conf.DekiWikiAccountList[Program.conf.DekiWikiSelected];
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
            if (acc != null) Adapter.TestDekiWikiAccount(acc);
        }

        private void chkEnableThumbnail_CheckedChanged(object sender, EventArgs e)
        {
            Program.conf.FTPCreateThumbnail = chkEnableThumbnail.Checked;
        }

        private void chkAutoSwitchFTP_CheckedChanged(object sender, EventArgs e)
        {
            Program.conf.AutoSwitchFileUploader = cbAutoSwitchFileUploader.Checked;
        }

        #endregion

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

        private void btnCheckUpdate_Click(object sender, EventArgs e)
        {
            Loader.Worker2.CheckUpdates();
        }

        private void cbAddFailedScreenshot_CheckedChanged(object sender, EventArgs e)
        {
            Program.conf.AddFailedScreenshot = cbAddFailedScreenshot.Checked;
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
            foreach (Software app in Program.conf.ImageEditors)
            {
                if (app != null && app.Name != null)
                {
                    if (app.Name.Equals(name))
                        return app;
                }

            }
            return null;
        }

        private void cbSelectedWindowStyle_SelectedIndexChanged(object sender, EventArgs e)
        {
            Program.conf.SelectedWindowRegionStyles = (RegionStyles)cbSelectedWindowStyle.SelectedIndex;
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
            if (mGuiIsReady)
            {
                Loader.Worker.CheckHistoryItems();
                Loader.Worker.SaveHistoryItems();
            }
        }

        private void cbCloseDropBox_CheckedChanged(object sender, EventArgs e)
        {
            Program.conf.CloseDropBox = cbCloseDropBox.Checked;
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
            Program.conf.HistoryListFormat = (HistoryListFormat)cbHistoryListFormat.SelectedIndex;
            // LoadHistoryItems();
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
            Program.conf.SwitchAfter = nudSwitchAfter.Value;
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

        private void cbAutoChangeUploadDestination_CheckedChanged(object sender, EventArgs e)
        {
            Program.conf.ImageUploadRetryOnTimeout = cboImageUploadRetryOnTimeout.Checked;
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
                StringBuilder sb = new StringBuilder();
                sb.Append(e);
                sb.AppendLine();
                sb.Append(FileSystem.mDebug.ToString());
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
                txtRootFolder.Text = Program.appSettings.RootDir;
            }
            FileSystem.MoveDirectory(oldRootDir, txtRootFolder.Text);
            UpdateGuiControlsPaths();
            Program.conf = XMLSettings.Read();
            ZScreen_ConfigGUI();
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

        private void cboWatermarkType_SelectedIndexChanged(object sender, EventArgs e)
        {
            Program.conf.WatermarkMode = (WatermarkType)cboWatermarkType.SelectedIndex;
            TestWatermark();
        }

        private void cbCropShowMagnifyingGlass_CheckedChanged(object sender, EventArgs e)
        {
            Program.conf.CropShowMagnifyingGlass = cbCropShowMagnifyingGlass.Checked;
        }

        private void pbLogo_MouseEnter(object sender, EventArgs e)
        {
            if (turnLogo.IsTurning) return;
            pbLogo.Image = ImageEffects.GetRandomLogo(Resources.main);
        }

        private void pbLogo_MouseLeave(object sender, EventArgs e)
        {
            if (turnLogo.IsTurning) return;

            pbLogo.Image = new Bitmap((Image)new ComponentResourceManager(typeof(ZScreen)).GetObject(("pbLogo.Image")));
        }

        private void autoScreenshotsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Loader.Worker.ShowAutoCapture();
        }

        private void numericUpDownTimer1_ValueChanged(object sender, EventArgs e)
        {
            Program.conf.ScreenshotDelayTime = nudtScreenshotDelay.Value;
        }

        private void nudtScreenshotDelay_SelectedIndexChanged(object sender, EventArgs e)
        {
            Program.conf.ScreenshotDelayTimes = nudtScreenshotDelay.Time;
        }

        private void lblToLanguage_MouseDown(object sender, MouseEventArgs e)
        {
            if (cbToLanguage.SelectedIndex > -1)
            {
                cbToLanguage.DoDragDrop(Program.conf.ToLanguage, DragDropEffects.Move);
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
            Program.conf.ToLanguage2 = lang.Value;
            btnTranslateTo1.Text = "To " + lang.Name;
        }

        private void btnTranslateTo1_Click(object sender, EventArgs e)
        {
            Loader.Worker.TranslateTo1();
        }

        private void cbLockFormSize_CheckedChanged(object sender, EventArgs e)
        {
            Program.conf.LockFormSize = cbLockFormSize.Checked;
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

        private void confApp_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
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
                        Program.conf.TextUploadersList.Add(textUploader);
                        ucTextUploaders.MyCollection.Items.Add(textUploader);
                        cboTextUploaders.Items.Add(textUploader);
                    }
                    ucTextUploaders.MyCollection.SelectedIndex = ucTextUploaders.MyCollection.Items.Count - 1;
                }
            }
        }

        private void TextUploadersRemoveButton_Click(object sender, EventArgs e)
        {
            if (ucTextUploaders.MyCollection.Items.Count > 0)
            {
                int index = ucTextUploaders.MyCollection.SelectedIndex;
                Program.conf.TextUploadersList.RemoveAt(index);
                ucTextUploaders.MyCollection.Items.RemoveAt(index);
                cboTextUploaders.Items.RemoveAt(index);
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
            Software temp = Program.conf.ImageEditors[lbImageSoftware.SelectedIndex];
            lbImageSoftware.Items[lbImageSoftware.SelectedIndex] = temp;
            Program.conf.ImageEditors[lbImageSoftware.SelectedIndex] = temp;
            CheckCorrectIsRightClickMenu(temp.Name);
            RewriteImageEditorsRightClickMenu();
        }

        private void TextUploaders_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ucTextUploaders.MyCollection.SelectedItems.Count > 0)
            {
                TextUploader textUploader = (TextUploader)ucTextUploaders.MyCollection.SelectedItem;

                if (mGuiIsReady)
                {
                    Program.conf.TextUploaderSelected = ucTextUploaders.MyCollection.SelectedIndex;
                    cboTextUploaders.SelectedIndex = ucTextUploaders.MyCollection.SelectedIndex;
                }

                bool hasOptions = textUploader != null;
                ucTextUploaders.SettingsGrid.Visible = hasOptions;

                if (hasOptions)
                {
                    ucTextUploaders.SettingsGrid.SelectedObject = textUploader.Settings;
                }
            }
        }

        private void cboTextDest_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (mGuiIsReady)
            {
                ucTextUploaders.MyCollection.SelectedIndex = cboTextUploaders.SelectedIndex;
                Program.conf.TextUploaderSelected = cboTextUploaders.SelectedIndex;
            }
        }

        private void cboURLShorteners_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (mGuiIsReady)
            {
                ucUrlShorteners.MyCollection.SelectedIndex = cboURLShorteners.SelectedIndex;
                Program.conf.UrlShortenerSelected = cboURLShorteners.SelectedIndex;
            }
        }

        private void cbAutoTranslate_CheckedChanged(object sender, EventArgs e)
        {
            Program.conf.AutoTranslate = cbAutoTranslate.Checked;
        }

        private void txtAutoTranslate_TextChanged(object sender, EventArgs e)
        {
            int number;
            if (int.TryParse(txtAutoTranslate.Text, out number))
            {
                Program.conf.AutoTranslateLength = number;
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
                TextUploader textUploader = (TextUploader)ucUrlShorteners.MyCollection.SelectedItem; ;

                if (mGuiIsReady)
                {
                    Program.conf.UrlShortenerSelected = ucUrlShorteners.MyCollection.SelectedIndex;
                    cboURLShorteners.SelectedIndex = ucUrlShorteners.MyCollection.SelectedIndex;
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
                        Program.conf.UrlShortenersList.Add(textUploader);
                        ucUrlShorteners.MyCollection.Items.Add(textUploader);
                        cboURLShorteners.Items.Add(textUploader);
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
                int index = ucUrlShorteners.MyCollection.SelectedIndex;
                Program.conf.UrlShortenersList.RemoveAt(index);
                ucUrlShorteners.MyCollection.Items.RemoveAt(index);
                cboURLShorteners.Items.RemoveAt(index);
                ucUrlShorteners.MyCollection.SelectedIndex = ucUrlShorteners.MyCollection.Items.Count - 1;
            }
        }

        private void cbShowHelpBalloonTips_CheckedChanged(object sender, EventArgs e)
        {
            Program.conf.ShowHelpBalloonTips = cbShowHelpBalloonTips.Checked;
            ttZScreen.Active = Program.conf.ShowHelpBalloonTips;
        }

        private void chkImageEditorAutoSave_CheckedChanged(object sender, EventArgs e)
        {
            Program.conf.ImageEditorAutoSave = chkImageEditorAutoSave.Checked;
        }

        private void btnImageShackProfile_Click(object sender, EventArgs e)
        {
            Process.Start("http://profile.imageshack.us/user/" + txtUserNameImageShack.Text);
        }

        private void chkPublicImageShack_CheckedChanged(object sender, EventArgs e)
        {
            Program.conf.ImageShackShowImagesInPublic = chkPublicImageShack.Checked;
        }

        private void txtUserNameImageShack_TextChanged(object sender, EventArgs e)
        {
            Program.conf.ImageShackUserName = txtUserNameImageShack.Text;
        }

        private void ucTextUploaders_Load(object sender, EventArgs e)
        {
            TextUploaders_SelectedIndexChanged(sender, e);
        }

        private void ucUrlShorteners_Load(object sender, EventArgs e)
        {
            UrlShorteners_SelectedIndexChanged(sender, e);
        }

        private void txtTwitPicUserName_TextChanged(object sender, EventArgs e)
        {
            if (mGuiIsReady)
            {
                Program.conf.TwitterUserName = txtTwitPicUserName.Text;
            }
        }

        private void txtTwitPicPassword_TextChanged(object sender, EventArgs e)
        {
            if (mGuiIsReady)
            {
                Program.conf.TwitterPassword = txtTwitPicPassword.Text;
            }
        }

        private void cboTwitPicUploadMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            Program.conf.TwitPicUploadMode = (TwitPicUploadType)cboTwitPicUploadMode.SelectedIndex;
        }

        private void tcApp_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Program.conf.AutoSaveSettings) WriteSettings();
        }

        private void chkDekiWikiForcePath_CheckedChanged(object sender, EventArgs e)
        {
            Program.conf.DekiWikiForcePath = chkDekiWikiForcePath.Checked;
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
            Program.conf.ProxyEnabled = chkProxyEnable.Checked;
        }

        private void tsmFTPClient_Click(object sender, EventArgs e)
        {
            if (Adapter.CheckFTPAccounts())
            {
                FTPAccount acc = Program.conf.FTPAccountList[Program.conf.FTPSelected];
                FTPOptions opt = new FTPOptions(acc, Adapter.GetProxySettings());
                FTPClient ftpClient = new FTPClient(opt) { Icon = this.Icon };
                ftpClient.Show();
            }
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
                Program.conf.ProxyList.RemoveAt(sel);
            }
        }

        private void ProxyAccountsList_SelectedIndexChanged(object sender, EventArgs e)
        {
            int sel = ucProxyAccounts.AccountsList.SelectedIndex;
            if (Program.conf.ProxyList != null && sel != -1 && sel < Program.conf.ProxyList.Count && Program.conf.ProxyList[sel] != null)
            {
                ProxyInfo acc = Program.conf.ProxyList[sel];
                ucProxyAccounts.SettingsGrid.SelectedObject = acc;
                Program.conf.ProxyActive = acc;
            }
        }

        private void ProxyAccountsAddButton_Click(object sender, EventArgs e)
        {
            ProxyInfo acc = new ProxyInfo("userName", "password", "domain", 8080);
            Program.conf.ProxyList.Add(acc);
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
            Program.conf.SelectedWindowCaptureObjects = cbSelectedWindowCaptureObjects.Checked;
        }

        private void cbSaveFormSizePosition_CheckedChanged(object sender, EventArgs e)
        {
            Program.conf.SaveFormSizePosition = cbSaveFormSizePosition.Checked;

            if (mGuiIsReady)
            {
                if (Program.conf.SaveFormSizePosition)
                {
                    Program.conf.WindowLocation = this.Location;
                    Program.conf.WindowSize = this.Size;
                }
                else
                {
                    Program.conf.WindowLocation = Point.Empty;
                    Program.conf.WindowSize = Size.Empty;
                }
            }
        }

        private void cbAutoSaveSettings_CheckedChanged(object sender, EventArgs e)
        {
            Program.conf.AutoSaveSettings = cbAutoSaveSettings.Checked;
        }

        private void cbTwitPicShowFull_CheckedChanged(object sender, EventArgs e)
        {
            Program.conf.TwitPicShowFull = cbTwitPicShowFull.Checked;
        }

        private void cbTwitPicThumbnailMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            Program.conf.TwitPicThumbnailMode = (TwitPicThumbnailType)cboTwitPicThumbnailMode.SelectedIndex;
        }

        private void nudtScreenshotDelay_MouseHover(object sender, EventArgs e)
        {
            ttZScreen.Show(ttZScreen.GetToolTip(nudtScreenshotDelay), this);
        }

        private void txtImagesFolderPattern_TextChanged(object sender, EventArgs e)
        {
            Program.conf.SaveFolderPattern = txtImagesFolderPattern.Text;
            lblImagesFolderPatternPreview.Text = NameParser.Convert(NameParserType.SaveFolder);
            txtImagesDir.Text = Program.ImagesDir;
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
                if (FileSystem.ManageImageFolders(Program.RootImagesDir))
                {
                    MessageBox.Show("Files successfully moved to save folders.");
                }
            }
            catch (Exception ex)
            {
                FileSystem.AppendDebug(ex);
                MessageBox.Show(ex.Message);
            }
        }

        private void cbCheckUpdatesBeta_CheckedChanged(object sender, EventArgs e)
        {
            Program.conf.CheckUpdatesBeta = cbCheckUpdatesBeta.Checked;
        }

        private void rbImageSize_CheckedChanged(object sender, EventArgs e)
        {
            if (rbImageSizeDefault.Checked)
            {
                Program.conf.ImageSizeType = ImageSizeType.DEFAULT;
            }
            else if (rbImageSizeFixed.Checked)
            {
                Program.conf.ImageSizeType = ImageSizeType.FIXED;
            }
            else if (rbImageSizeRatio.Checked)
            {
                Program.conf.ImageSizeType = ImageSizeType.RATIO;
            }
        }

        private void txtImageSizeFixedWidth_TextChanged(object sender, EventArgs e)
        {
            int width;
            if (int.TryParse(txtImageSizeFixedWidth.Text, out width))
            {
                Program.conf.ImageSizeFixedWidth = width;
            }
        }

        private void txtImageSizeFixedHeight_TextChanged(object sender, EventArgs e)
        {
            int height;
            if (int.TryParse(txtImageSizeFixedHeight.Text, out height))
            {
                Program.conf.ImageSizeFixedHeight = height;
            }
        }

        private void txtImageSizeRatio_TextChanged(object sender, EventArgs e)
        {
            float percentage;
            if (float.TryParse(txtImageSizeRatio.Text, out percentage))
            {
                Program.conf.ImageSizeRatioPercentage = percentage;
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
                if (Program.conf.WebPageUseCustomSize)
                {
                    capture = new IECapt(Program.conf.WebPageWidth, Program.conf.WebPageHeight, 1);
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

            if (Program.conf.WebPageAutoUpload)
            {
                WebPageUpload();
            }
        }

        private void cbWebPageUseCustomSize_CheckedChanged(object sender, EventArgs e)
        {
            Program.conf.WebPageUseCustomSize = cbWebPageUseCustomSize.Checked;
        }

        private void txtWebPageWidth_TextChanged(object sender, EventArgs e)
        {
            int width;
            if (int.TryParse(txtWebPageWidth.Text, out width))
            {
                Program.conf.WebPageWidth = width;
            }
        }

        private void txtWebPageHeight_TextChanged(object sender, EventArgs e)
        {
            int height;
            if (int.TryParse(txtWebPageHeight.Text, out height))
            {
                Program.conf.WebPageHeight = height;
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
            Program.conf.WebPageAutoUpload = cbWebPageAutoUpload.Checked;
        }

        private void pbLogo_MouseClick(object sender, MouseEventArgs e)
        {
            turnLogo.StartTurn();
        }

        private void txtImageBamApiKey_TextChanged(object sender, EventArgs e)
        {
            Program.conf.ImageBamApiKey = txtImageBamApiKey.Text;
        }

        private void txtImageBamSecret_TextChanged(object sender, EventArgs e)
        {
            Program.conf.ImageBamSecret = txtImageBamSecret.Text;
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
            Program.conf.ImageBamGalleryActive = lbImageBamGalleries.SelectedIndex;
        }

        private void btnImageBamRemoveGallery_Click(object sender, EventArgs e)
        {
            if (lbImageBamGalleries.SelectedIndex > -1)
            {
                if (!string.IsNullOrEmpty(Adapter.GetImageBamGalleryActive()))
                {
                    lbImageBamGalleries.Items.RemoveAt(lbImageBamGalleries.SelectedIndex);
                    Program.conf.ImageBamGallery.RemoveAt(lbImageBamGalleries.SelectedIndex);
                }
            }
        }

        private void chkImageBamContentNSFW_CheckedChanged(object sender, EventArgs e)
        {
            Program.conf.ImageBamContentNSFW = chkImageBamContentNSFW.Checked;
        }

        private void cboRapidShareAcctType_SelectedIndexChanged(object sender, EventArgs e)
        {
            Program.conf.RapidShareAccountType = (RapidShareAcctType)cboRapidShareAcctType.SelectedIndex;
            txtRapidSharePremiumUserName.Enabled = Program.conf.RapidShareAccountType == RapidShareAcctType.Premium;
            txtRapidShareCollectorID.Enabled = Program.conf.RapidShareAccountType != RapidShareAcctType.Free && !txtRapidSharePremiumUserName.Enabled;
            txtRapidSharePassword.Enabled = Program.conf.RapidShareAccountType != RapidShareAcctType.Free;
        }

        private void txtRapidShareCollectorID_TextChanged(object sender, EventArgs e)
        {
            Program.conf.RapidShareCollectorsID = txtRapidShareCollectorID.Text;
        }

        private void txtRapidSharePremiumUserName_TextChanged(object sender, EventArgs e)
        {
            Program.conf.RapidSharePremiumUserName = txtRapidSharePremiumUserName.Text;
        }

        private void txtRapidSharePassword_TextChanged(object sender, EventArgs e)
        {
            Program.conf.RapidSharePassword = txtRapidSharePassword.Text;
        }

        private void txtSendSpaceUserName_TextChanged(object sender, EventArgs e)
        {
            Program.conf.SendSpaceUserName = txtSendSpaceUserName.Text;
        }

        private void txtSendSpacePassword_TextChanged(object sender, EventArgs e)
        {
            Program.conf.SendSpacePassword = txtSendSpacePassword.Text;
        }

        private void cboSendSpaceAcctType_SelectedIndexChanged(object sender, EventArgs e)
        {
            Program.conf.SendSpaceAccountType = (AcctType)cboSendSpaceAcctType.SelectedIndex;
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
            Program.conf.FileDestMode = (FileUploaderType)cboFileUploaders.SelectedIndex;
        }

        private void txtFTPThumbWidth_TextChanged(object sender, EventArgs e)
        {
            int width;
            if (int.TryParse(txtFTPThumbWidth.Text, out width))
            {
                Program.conf.FTPThumbnailWidth = width;
            }
        }

        private void txtFTPThumbHeight_TextChanged(object sender, EventArgs e)
        {
            int height;
            if (int.TryParse(txtFTPThumbHeight.Text, out height))
            {
                Program.conf.FTPThumbnailHeight = height;
            }
        }

        private void cbFTPThumbnailCheckSize_CheckedChanged(object sender, EventArgs e)
        {
            Program.conf.FTPThumbnailCheckSize = cbFTPThumbnailCheckSize.Checked;
        }

        private void cboYfrogUploadMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            Program.conf.YfrogUploadMode = (YfrogUploadType)cboYfrogUploadMode.SelectedIndex;
        }

        private void chkWindows7TaskbarIntegration_CheckedChanged(object sender, EventArgs e)
        {
            if (mGuiIsReady)
            {
                Program.conf.Windows7TaskbarIntegration = chkWindows7TaskbarIntegration.Checked;
                chkShowTaskbar.Enabled = !Program.conf.Windows7TaskbarIntegration;
                ZScreen_Windows7onlyTasks();
            }
        }
    }
}
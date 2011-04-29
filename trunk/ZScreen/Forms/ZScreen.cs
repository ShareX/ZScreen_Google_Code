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
using System.Media;
using System.Text;
using System.Threading;
using System.Web;
using System.Windows.Forms;
using GradientTester;
using GraphicsMgrLib;
using HelpersLib;
using Microsoft.WindowsAPICodePack.Shell;
using Microsoft.WindowsAPICodePack.Taskbar;
using MS.WindowsAPICodePack.Internal;
using UploadersLib;
using UploadersLib.FileUploaders;
using UploadersLib.HelperClasses;
using UploadersLib.ImageUploaders;
using UploadersLib.TextServices;
using ZScreenGUI.Properties;
using ZScreenGUI.UserControls;
using ZScreenLib;
using ZScreenLib.Helpers;
using ZScreenTesterGUI;
using ZSS.ColorsLib;
using ZSS.FTPClientLib;

namespace ZScreenGUI
{
    public partial class ZScreen : Form
    {
        #region Variables

        private bool mGuiIsReady, mClose;
        private int mHadFocusAt;
        private TextBox mHadFocus;
        private ContextMenuStrip codesMenu = new ContextMenuStrip();
        private DebugHelper mDebug = null;
        private ZScreenLib.ImageEffects.TurnImage turnLogo;
        private ThumbnailCacher thumbnailCacher;

        #endregion Variables

        public ZScreen()
        {
            InitializeComponent();
            Uploader.ProxySettings = Adapter.CheckProxySettings();
            ZScreen_SetFormSettings();
            Loader.Worker = new WorkerPrimary(this);
            Loader.Worker2 = new WorkerSecondary(this);
            Loader.Worker.mHotkeyMgr = new HotkeyMgr(ref dgvHotkeys, ref lblHotkeyStatus);
            ZScreen_ConfigGUI();

            Loader.Worker2.PerformOnlineTasks();
            if (Engine.conf.CheckUpdates)
            {
                Loader.Worker2.CheckUpdates();
            }
            Application.Idle += new EventHandler(Application_Idle);
        }

        internal void ZScreen_Windows7onlyTasks()
        {
            this.ShowInTaskbar = Engine.conf.Windows7TaskbarIntegration && CoreHelpers.RunningOnWin7;

            if (!Engine.conf.Windows7TaskbarIntegration)
            {
                if (Engine.zJumpList != null)
                {
                    Engine.zJumpList.ClearAllUserTasks();
                }
            }
            else if (this.Handle != IntPtr.Zero && CoreHelpers.RunningOnWin7)
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
                    ThumbnailToolBarButton cropShot = new ThumbnailToolBarButton(Resources.shape_square_ico, "Crop Shot");
                    cropShot.Click += new EventHandler<ThumbnailButtonClickedEventArgs>(cropShot_Click);

                    ThumbnailToolBarButton selWindow = new ThumbnailToolBarButton(Resources.application_double_ico, "Selected Window");
                    selWindow.Click += new EventHandler<ThumbnailButtonClickedEventArgs>(selWindow_Click);

                    ThumbnailToolBarButton clipboardUpload = new ThumbnailToolBarButton(Resources.clipboard_upload_ico, "Clipboard Upload");
                    clipboardUpload.Click += new EventHandler<ThumbnailButtonClickedEventArgs>(clipboardUpload_Click);

                    Engine.zWindowsTaskbar.ThumbnailToolBars.AddButtons(this.Handle, cropShot, selWindow, clipboardUpload);
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
            ucFTPAccounts.btnClone.Visible = true;
            ucFTPAccounts.btnClone.Click += new EventHandler(FTPAccountCloneButton_Click);
            ucFTPAccounts.AccountsList.SelectedIndexChanged += new EventHandler(FTPAccountsList_SelectedIndexChanged);
            ucFTPAccounts.SettingsGrid.PropertyValueChanged += new PropertyValueChangedEventHandler(FtpAccountSettingsGrid_PropertyValueChanged);

            // Accounts - Localhost
            ucLocalhostAccounts.btnAdd.Click += new EventHandler(LocalhostAccountAddButton_Click);
            ucLocalhostAccounts.btnRemove.Click += new EventHandler(LocalhostAccountRemoveButton_Click);
            ucLocalhostAccounts.btnTest.Visible = false;
            ucLocalhostAccounts.AccountsList.SelectedIndexChanged += new EventHandler(LocalhostAccountsList_SelectedIndexChanged);

            // Accounts - MindTouch
            ucMindTouchAccounts.btnAdd.Click += new EventHandler(MindTouchAccountAddButton_Click);
            ucMindTouchAccounts.btnRemove.Click += new EventHandler(MindTouchAccountRemoveButton_Click);
            ucMindTouchAccounts.btnTest.Click += new EventHandler(MindTouchAccountTestButton_Click);
            ucMindTouchAccounts.AccountsList.SelectedIndexChanged += new EventHandler(MindTouchAccountsList_SelectedIndexChanged);

            // Accounts - MediaWiki
            ucMediaWikiAccounts.btnAdd.Click += new EventHandler(MediawikiAccountAddButton_Click);
            ucMediaWikiAccounts.btnRemove.Click += new EventHandler(MediawikiAccountRemoveButton_Click);
            ucMediaWikiAccounts.btnTest.Click += new EventHandler(MediawikiAccountTestButton_Click);
            ucMediaWikiAccounts.AccountsList.SelectedIndexChanged += new EventHandler(MediaWikiAccountsList_SelectedIndexChanged);

            // Accounts - Twitter
            ucTwitterAccounts.btnAdd.Text = "Add";
            ucTwitterAccounts.btnAdd.Click += new EventHandler(TwitterAccountAddButton_Click);
            ucTwitterAccounts.btnRemove.Click += new EventHandler(TwitterAccountRemoveButton_Click);
            ucTwitterAccounts.btnTest.Text = "Authorize";
            ucTwitterAccounts.btnTest.Click += new EventHandler(TwitterAccountAuthButton_Click);
            ucTwitterAccounts.SettingsGrid.PropertySort = PropertySort.NoSort;
            ucTwitterAccounts.AccountsList.SelectedIndexChanged += new EventHandler(TwitterAccountList_SelectedIndexChanged);

            // Options - Proxy
            ucProxyAccounts.btnAdd.Click += new EventHandler(ProxyAccountsAddButton_Click);
            ucProxyAccounts.btnRemove.Click += new EventHandler(ProxyAccountsRemoveButton_Click);
            ucProxyAccounts.btnTest.Click += new EventHandler(ProxyAccountTestButton_Click);
            ucProxyAccounts.AccountsList.SelectedIndexChanged += new EventHandler(ProxyAccountsList_SelectedIndexChanged);

            // Watermark Codes Menu
            codesMenu.AutoClose = false;
            codesMenu.Font = new Font("Lucida Console", 8);
            codesMenu.Opacity = 0.8;
            codesMenu.ShowImageMargin = false;

            // Dest Selectors
            ucDestOptions.cboImageUploaders.SelectedIndexChanged += new EventHandler(cboImageUploaders_SelectedIndexChanged);
            ucDestOptions.cboTextUploaders.SelectedIndexChanged += new EventHandler(cboTextUploaders_SelectedIndexChanged);
            ucDestOptions.cboFileUploaders.SelectedIndexChanged += new EventHandler(cboFileUploaders_SelectedIndexChanged);
            ucDestOptions.cboURLShorteners.SelectedIndexChanged += new EventHandler(cboURLShorteners_SelectedIndexChanged);

            niTray.BalloonTipClicked += new EventHandler(niTray_BalloonTipClicked);

            DrawZScreenLabel(false);
        }

        private void FtpAccountSettingsGrid_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            FTPSetup(Engine.conf.FTPAccountList);
        }

        private void FTPAccountCloneButton_Click(object sender, EventArgs e)
        {
            FTPAccount src = ucFTPAccounts.AccountsList.Items[ucFTPAccounts.AccountsList.SelectedIndex] as FTPAccount;
            Engine.conf.FTPAccountList.Add(src.Clone());
            ucFTPAccounts.AccountsList.SelectedIndex = ucFTPAccounts.AccountsList.Items.Count - 1;
            FTPSetup(Engine.conf.FTPAccountList);
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
                else if (Engine.conf.ShowInTaskbar && Engine.conf.CloseButtonAction == WindowButtonAction.MinimizeToTaskbar)
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

            SetToolTip(nudScreenshotDelay);
            FillClipboardCopyMenu();
            FillClipboardMenu();

            CreateCodesMenu();

            dgvHotkeys.BackgroundColor = Color.FromArgb(tpHotkeys.BackColor.R, tpHotkeys.BackColor.G, tpHotkeys.BackColor.B);

            turnLogo = new ZScreenLib.ImageEffects.TurnImage((Image)new ComponentResourceManager(typeof(ZScreen)).GetObject(("pbLogo.Image")));
            turnLogo.ImageTurned += new ZScreenLib.ImageEffects.TurnImage.ImageEventHandler(x => pbLogo.Image = x);

            thumbnailCacher = new ThumbnailCacher(pbPreview, new Size(450, 230), 10)
            {
                LoadingImage = Resources.ajax_loader
            };

            niTray.Visible = true;
            // Loader.Splash.Close();
            // FileSystem.AppendDebug("Closed Splash Screen");

            rtbDebugLog.Text = FileSystem.DebugLog.ToString();
            FileSystem.DebugLogChanged += new FileSystem.DebugLogEventHandler(FileSystem_DebugLogChanged);

            new RichTextBoxMenu(rtbDebugLog, true);
            new RichTextBoxMenu(rtbDebugInfo, true);

            FileSystem.AppendDebug("Loaded ZScreen GUI...");
        }

        private void FileSystem_DebugLogChanged(string line)
        {
            if (!rtbDebugLog.IsDisposed)
            {
                MethodInvoker method = delegate
                {
                    rtbDebugLog.AppendText(line + Environment.NewLine);
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
        }

        protected override void WndProc(ref Message m)
        {
            if (mGuiIsReady)
            {
                switch (m.Msg)
                {
                    case 992: // nfi but this is the only way it works for XP
                    case (int)ClipboardHook.Msgs.WM_DRAWCLIPBOARD:
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
                        // pass message on to next clipboard listener
                        ClipboardHook.SendMessage(m.Msg, m.WParam, m.LParam);
                        break;
                    case (int)ClipboardHook.Msgs.WM_CHANGECBCHAIN:
                        if (m.WParam == ClipboardHook.mClipboardViewerNext)
                        {
                            ClipboardHook.mClipboardViewerNext = m.LParam;
                        }
                        else
                        {
                            ClipboardHook.SendMessage(m.Msg, m.WParam, m.LParam);
                        }
                        break;
                    case ZScreenLib.NativeMethods.WM_SYSCOMMAND:
                        int command = m.WParam.ToInt32() & 0xfff0;
                        if (command == NativeMethods.SC_MINIMIZE)
                        {
                            switch (Engine.conf.MinimizeButtonAction)
                            {
                                case WindowButtonAction.CloseApplication:
                                    mClose = true;
                                    this.Close();
                                    break;
                                case WindowButtonAction.MinimizeToTaskbar:
                                    this.WindowState = FormWindowState.Minimized;
                                    break;
                                case WindowButtonAction.MinimizeToTray:
                                    this.Hide();
                                    break;
                            }
                        }
                        else
                        {
                            base.WndProc(ref m);
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

        private void UpdateFtpControls()
        {
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
            if (Engine.conf.PreferSystemFolders)
            {
                txtRootFolder.Text = Engine.SettingsDir;
                gbRoot.Text = "Settings";
            }
            else
            {
                txtRootFolder.Text = Engine.RootAppFolder;
                gbRoot.Text = "Root";
            }
            gbRoot.Enabled = !Engine.Portable;
            gbImages.Enabled = !Engine.Portable;
            gbCache.Enabled = !Engine.Portable;
            chkPreferSystemFolders.Enabled = !Engine.Portable;
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
            Engine.conf.CropRegionRectangleInfo = chkRegionRectangleInfo.Checked;
        }

        private void cbRegionHotkeyInfo_CheckedChanged(object sender, EventArgs e)
        {
            Engine.conf.CropRegionHotkeyInfo = chkRegionHotkeyInfo.Checked;
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
                }

                this.Refresh();
            }
        }

        private void ZScreen_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason != CloseReason.WindowsShutDown)
            {
                WriteSettings();
            }

            if (e.CloseReason == CloseReason.UserClosing && Engine.conf.CloseButtonAction != WindowButtonAction.CloseApplication && !mClose)
            {
                e.Cancel = true;

                if (Engine.conf.CloseButtonAction == WindowButtonAction.MinimizeToTaskbar)
                {
                    this.WindowState = FormWindowState.Minimized;
                }
                else if (Engine.conf.CloseButtonAction == WindowButtonAction.MinimizeToTray)
                {
                    this.Hide();
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

        private void timerTrimMemory_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
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
                    tsm.MouseEnter += new EventHandler(TrayImageEditor_MouseEnter);
                    tsm.MouseLeave += new EventHandler(TrayImageEditor_MouseLeave);
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

        private void TrayImageEditor_MouseLeave(object sender, EventArgs e)
        {
            tsmEditinImageSoftware.DropDown.AutoClose = true;
        }

        private void TrayImageEditor_MouseEnter(object sender, EventArgs e)
        {
            tsmEditinImageSoftware.DropDown.AutoClose = false;
        }

        private void UpdateGuiEditors(object sender)
        {
            if (mGuiIsReady)
            {
                if (sender.GetType() == lbSoftware.GetType())
                {
                    // the checked state needs to be inversed for some weird reason to get it working properly
                    if (Adapter.CheckList(Engine.conf.ImageEditors, lbSoftware.SelectedIndex))
                    {
                        Engine.conf.ImageEditors[lbSoftware.SelectedIndex].Enabled = !lbSoftware.GetItemChecked(lbSoftware.SelectedIndex);
                        ToolStripMenuItem tsm = tsmEditinImageSoftware.DropDownItems[lbSoftware.SelectedIndex] as ToolStripMenuItem;
                        tsm.Checked = Engine.conf.ImageEditors[lbSoftware.SelectedIndex].Enabled;
                    }
                }
                else if (sender.GetType() == typeof(ToolStripMenuItem))
                {
                    ToolStripMenuItem tsm = sender as ToolStripMenuItem;
                    int sel = (int)tsm.Tag;
                    if (Adapter.CheckList(Engine.conf.ImageEditors, sel))
                    {
                        Engine.conf.ImageEditors[sel].Enabled = tsm.Checked;
                        lbSoftware.SetItemChecked(lbSoftware.SelectedIndex, tsm.Checked);
                    }
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
                lbSoftware.SelectedItem = tsm.Text;
                UpdateGuiEditors(sender);
            }
        }

        private void RewriteCustomUploaderRightClickMenu()
        {
            if (Engine.conf.CustomUploadersList != null)
            {
                List<CustomUploaderInfo> lUploaders = Engine.conf.CustomUploadersList;

                ToolStripMenuItem tsmDestCustomHTTP = GetFileDestMenuItem(FileUploaderType.CustomUploader);
                tsmDestCustomHTTP.DropDownDirection = ToolStripDropDownDirection.Right;
                tsmDestCustomHTTP.DropDownItems.Clear();

                ToolStripMenuItem tsm;
                for (int i = 0; i < lUploaders.Count; i++)
                {
                    tsm = new ToolStripMenuItem { CheckOnClick = true, Tag = i, Text = lUploaders[i].Name };
                    tsm.Click += rightClickIHS_Click;
                    tsmDestCustomHTTP.DropDownItems.Add(tsm);
                }

                CheckCorrectMenuItemClicked(ref tsmDestCustomHTTP, Engine.conf.CustomUploaderSelected);

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

        /// <summary>
        /// Annoying method until somebody fixes it
        /// </summary>
        private void RewriteFTPRightClickMenu()
        {
            //if (Engine.conf.FTPAccountList != null)
            //{
            //    List<ToolStripMenuItem> tsmList = new List<ToolStripMenuItem>();
            //    tsmList.Add(GetImageDestMenuItem(ImageDestType.FTP));
            //    tsmList.Add(GetFileDestMenuItem(FileUploaderType.FTP));

            //    foreach (ToolStripMenuItem tsmi in tsmList)
            //    {
            //        tsmi.DropDownDirection = ToolStripDropDownDirection.Right;
            //        tsmi.DropDownItems.Clear();
            //        List<FTPAccount> accs = Engine.conf.FTPAccountList;
            //        ToolStripMenuItem temp;

            //        for (int x = 0; x < accs.Count; x++)
            //        {
            //            temp = new ToolStripMenuItem { Tag = x, CheckOnClick = true, Text = accs[x].Name };
            //            temp.Click += rightClickFTPItem_Click;
            //            tsmi.DropDownItems.Add(temp);
            //        }

            //        temp = tsmi;

            //        // Check the active ftpUpload account
            //        CheckCorrectMenuItemClicked(ref temp, Engine.conf.FtpImages);
            //        tsmi.DropDownDirection = ToolStripDropDownDirection.Right;

            //        // Show drop down menu in the correct place if menu is selected
            //        if (tsmi.Selected)
            //        {
            //            tsmi.DropDown.Hide();
            //            tsmi.DropDown.Show();
            //        }
            //    }
            //}
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

            using (OpenFileDialog dlg = new OpenFileDialog())
            {
                dlg.Filter = "Executable Files (*.exe)|*.exe|All Files (*.*)|*.*";

                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    temp = new Software();
                    temp.Name = Path.GetFileNameWithoutExtension(dlg.FileName);
                    temp.Path = dlg.FileName;
                }
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

            if (Engine.conf.ProxyConfig != ProxyConfigType.NoProxy)
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

            if (Engine.conf.Windows7TaskbarIntegration && Engine.HasWindows7)
            {
                if (Engine.conf.FirstRun)
                {
                    Engine.conf.CloseButtonAction = WindowButtonAction.MinimizeToTaskbar;
                }
                ZScreen_Windows7onlyTasks();
                if (Engine.conf.CloseButtonAction == WindowButtonAction.MinimizeToTaskbar)
                {
                    this.ShowInTaskbar = true;
                    if (!Engine.conf.ShowMainWindow)
                    {
                        this.WindowState = FormWindowState.Minimized;
                    }
                }
            }

            Loader.KeyboardHook();
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

        private ToolStripMenuItem GetImageDestMenuItem(ImageUploaderType idt)
        {
            foreach (ToolStripMenuItem tsmi in tsmImageDest.DropDownItems)
            {
                if ((ImageUploaderType)tsmi.Tag == idt)
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
            ImageUploaderType uploader = (ImageUploaderType)ucDestOptions.cboImageUploaders.SelectedIndex;
            Engine.conf.ImageUploaderType = uploader;
            cboClipboardTextMode.Enabled = uploader != ImageUploaderType.CLIPBOARD;

            CheckToolStripMenuItem(tsmImageDest, GetImageDestMenuItem(uploader));
        }

        private void cboTextUploaders_SelectedIndexChanged(object sender, EventArgs e)
        {
            Engine.conf.TextUploaderType = (TextUploaderType)ucDestOptions.cboTextUploaders.SelectedIndex;

            // TODO: CheckToolStripMenuItem(tsmTextDest?, GetFileDestMenuItem(uploader));
        }

        private void cboFileUploaders_SelectedIndexChanged(object sender, EventArgs e)
        {
            Engine.conf.FileUploaderType = (FileUploaderType)ucDestOptions.cboFileUploaders.SelectedIndex;

            CheckToolStripMenuItem(tsmFileDest, GetFileDestMenuItem(Engine.conf.FileUploaderType));
        }

        private void cboURLShorteners_SelectedIndexChanged(object sender, EventArgs e)
        {
            Engine.conf.URLShortenerType = (UrlShortenerType)ucDestOptions.cboURLShorteners.SelectedIndex;

            // TODO: CheckToolStripMenuItem(tsmURLShortenerDest?, GetFileDestMenuItem(Engine.conf.TextUploaderType));
        }

        private void CheckToolStripMenuItem(ToolStripDropDownItem parent, ToolStripMenuItem item)
        {
            foreach (ToolStripMenuItem tsmi in parent.DropDownItems)
            {
                tsmi.Checked = tsmi == item;
            }

            tsmCopytoClipboardMode.Enabled = ucDestOptions.cboImageUploaders.SelectedIndex != (int)ImageUploaderType.CLIPBOARD &&
                ucDestOptions.cboImageUploaders.SelectedIndex != (int)ImageUploaderType.FILE;
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

        private void mImageEditorMenuClose_Tick(object sender, EventArgs e)
        {
            tsmEditinImageSoftware.DropDown.Close();
        }

        private void Application_Idle(object sender, EventArgs e)
        {
            DelayedTrimMemoryUse();
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
            NameParser parser = new NameParser(NameParserType.ActiveWindow)
            {
                CustomProductName = Engine.GetProductName(),
                IsPreview = true,
                MaxNameLength = Engine.conf.MaxNameLength
            };
            lblActiveWindowPreview.Text = parser.Convert(Engine.conf.ActiveWindowPattern);
        }

        private void txtEntireScreen_TextChanged(object sender, EventArgs e)
        {
            Engine.conf.EntireScreenPattern = txtEntireScreen.Text;
            NameParser parser = new NameParser(NameParserType.EntireScreen)
            {
                CustomProductName = Engine.GetProductName(),
                IsPreview = true,
                MaxNameLength = Engine.conf.MaxNameLength
            };
            lblEntireScreenPreview.Text = parser.Convert(Engine.conf.EntireScreenPattern);
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
                        switch (t.Job2)
                        {
                            case WorkerTask.JobLevel2.LANGUAGE_TRANSLATOR:
                                cbString = t.TranslationInfo.Result;
                                if (!string.IsNullOrEmpty(cbString))
                                {
                                    Clipboard.SetText(cbString); // ok
                                }
                                break;
                            default:
                                switch (t.MyImageUploader)
                                {
                                    case ImageUploaderType.FILE:
                                    case ImageUploaderType.CLIPBOARD:
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
                CustomUploaderInfo iUploader = GetUploaderFromFields();
                Engine.conf.CustomUploadersList.Add(iUploader);
                lbImageUploader.Items.Add(iUploader.Name);
                lbImageUploader.SelectedIndex = lbImageUploader.Items.Count - 1;
            }
        }

        private void btnUploaderRemove_Click(object sender, EventArgs e)
        {
            if (lbImageUploader.SelectedIndex != -1)
            {
                int selected = lbImageUploader.SelectedIndex;
                Engine.conf.CustomUploadersList.RemoveAt(selected);
                lbImageUploader.Items.RemoveAt(selected);
                LoadImageUploaders(new CustomUploaderInfo());
            }
        }

        private CustomUploaderInfo GetUploaderFromFields()
        {
            CustomUploaderInfo iUploader = new CustomUploaderInfo(txtUploader.Text);
            foreach (ListViewItem lvItem in lvArguments.Items)
            {
                iUploader.Arguments.Add(new Argument(lvItem.Text, lvItem.SubItems[1].Text));
            }

            iUploader.UploadURL = txtUploadURL.Text;
            iUploader.FileFormName = txtFileForm.Text;
            foreach (ListViewItem lvItem in lvRegexps.Items)
            {
                iUploader.RegexpList.Add(lvItem.Text);
            }

            iUploader.URL = txtFullImage.Text;
            iUploader.ThumbnailURL = txtThumbnail.Text;
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
                LoadImageUploaders(Engine.conf.CustomUploadersList[lbImageUploader.SelectedIndex]);
                Engine.conf.CustomUploaderSelected = lbImageUploader.SelectedIndex;
                RewriteCustomUploaderRightClickMenu();
            }
        }

        private void LoadImageUploaders(CustomUploaderInfo imageUploader)
        {
            txtArg1.Text = string.Empty;
            txtArg2.Text = string.Empty;
            lvArguments.Items.Clear();
            foreach (Argument arg in imageUploader.Arguments)
            {
                lvArguments.Items.Add(arg.Name).SubItems.Add(arg.Value);
            }

            txtUploadURL.Text = imageUploader.UploadURL;
            txtFileForm.Text = imageUploader.FileFormName;
            txtRegexp.Text = string.Empty;
            lvRegexps.Items.Clear();
            foreach (string regexp in imageUploader.RegexpList)
            {
                lvRegexps.Items.Add(regexp);
            }

            txtFullImage.Text = imageUploader.URL;
            txtThumbnail.Text = imageUploader.ThumbnailURL;
        }

        private void btnUploadersUpdate_Click(object sender, EventArgs e)
        {
            if (lbImageUploader.SelectedIndex != -1)
            {
                CustomUploaderInfo iUploader = GetUploaderFromFields();
                iUploader.Name = lbImageUploader.SelectedItem.ToString();
                Engine.conf.CustomUploadersList[lbImageUploader.SelectedIndex] = iUploader;
            }

            RewriteCustomUploaderRightClickMenu();
        }

        private void btnUploadersClear_Click(object sender, EventArgs e)
        {
            LoadImageUploaders(new CustomUploaderInfo());
        }

        private void btUploadersTest_Click(object sender, EventArgs e)
        {
            if (lbImageUploader.SelectedIndex != -1)
            {
                btnUploadersTest.Enabled = false;
                Loader.Worker.StartWorkerScreenshots(WorkerTask.JobLevel2.CustomUploaderTest);
            }
        }

        private void btnUploaderExport_Click(object sender, EventArgs e)
        {
            if (Engine.conf.CustomUploadersList != null)
            {
                SaveFileDialog dlg = new SaveFileDialog
                {
                    FileName = string.Format("{0}-{1}-uploaders", Application.ProductName, DateTime.Now.ToString("yyyyMMdd")),
                    Filter = Engine.FILTER_IMAGE_HOSTING_SERVICES
                };
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    CustomUploaderManager ihsm = new CustomUploaderManager
                    {
                        ImageHostingServices = Engine.conf.CustomUploadersList
                    };
                    ihsm.Save(dlg.FileName);
                }
            }
        }

        private void ImportImageUploaders(string fp)
        {
            CustomUploaderManager tmp = CustomUploaderManager.Read(fp);
            if (tmp != null)
            {
                Engine.conf.CustomUploadersList = new List<CustomUploaderInfo>();
                Engine.conf.CustomUploadersList.AddRange(tmp.ImageHostingServices);
                foreach (CustomUploaderInfo iHostingService in Engine.conf.CustomUploadersList)
                {
                    lbImageUploader.Items.Add(iHostingService.Name);
                }
            }
        }

        private void btnUploaderImport_Click(object sender, EventArgs e)
        {
            if (Engine.conf.CustomUploadersList == null)
            {
                Engine.conf.CustomUploadersList = new List<CustomUploaderInfo>();
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
            OpenSource(ClipboardManager.GetLastImageUpload(), sType);
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
                        Clipboard.SetText(path); // ok
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

        #endregion Image MyCollection

        private void dgvHotkeys_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Ignore clicks
            if (e.RowIndex < 0 || e.ColumnIndex != dgvHotkeys.Columns[1].Index)
            {
                return;
            }

            Loader.Worker.mSetHotkeys = true;
            HotkeyMgr.mHKSelectedRow = e.RowIndex;

            lblHotkeyStatus.Text = "Press the keys you would like to use... Press enter when done setting all desired Hotkeys.";

            dgvHotkeys.Rows[e.RowIndex].Cells[1].Value = Loader.Worker.GetSelectedHotkeySpecialString() + " <Set Keys>";
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

        private void cbCropStyle_SelectedIndexChanged(object sender, EventArgs e)
        {
            Engine.conf.CropRegionStyles = (RegionStyles)chkCropStyle.SelectedIndex;
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

        private void cbCopyClipboardAfterTask_CheckedChanged(object sender, EventArgs e)
        {
            Engine.conf.CopyClipboardAfterTask = cbCopyClipboardAfterTask.Checked;
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
                        Clipboard.SetText(result); // ok - user
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
                Select(x => new { Name = ReplacementExtension.Prefix + Enum.GetName(typeof(ReplacementVariables), x), Description = x.GetDescription() });

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

        private void cbShowCursor_CheckedChanged(object sender, EventArgs e)
        {
            Engine.conf.ShowCursor = chkShowCursor.Checked;
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
                pbWatermarkShow.Image = ZScreenLib.ImageEffects.ApplyWatermark(bmp2);
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
            Engine.conf.WatermarkPositionMode = (WatermarkPositionType)chkWatermarkPosition.SelectedIndex;
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
                            using (Image img2 = ImageEffects.FillBackground(img, Engine.conf.ClipboardBackgroundColor))
                            {
                                try
                                {
                                    Clipboard.SetImage(img2); // ok
                                }
                                catch (Exception ex)
                                {
                                    FileSystem.AppendDebug("Error while copying image to clipboard", ex);
                                }
                            }
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
                    Clipboard.SetText(hi.RemotePath); // ok
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

                if (MessageBox.Show(msg, Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    foreach (HistoryItem hi in temp)
                    {
                        Adapter.DeleteFile(hi.LocalPath);
                        lbHistory.Items.Remove(hi);
                    }

                    if (lbHistory.Items.Count > 0)
                    {
                        lbHistory.SelectedIndex = 0;
                    }

                    Loader.Worker.UpdateGuiControlsHistory();
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
            Process.Start(Engine.URL_WIKIPAGES);
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
            /*Loader.Worker.StartBW_LanguageTranslator(new GoogleTranslate.TranslationInfo(txtTranslateText.Text,
                GoogleTranslate.FindLanguage(Engine.conf.GoogleSourceLanguage, ZScreen.mGTranslator.LanguageOptions.SourceLangList),
                GoogleTranslate.FindLanguage(Engine.conf.GoogleTargetLanguage, ZScreen.mGTranslator.LanguageOptions.TargetLangList)));*/
        }

        private void cbFromLanguage_SelectedIndexChanged(object sender, EventArgs e)
        {
            Engine.conf.GoogleSourceLanguage = GoogleTranslate.Languages[cbFromLanguage.SelectedIndex].Name;
        }

        private void cbToLanguage_SelectedIndexChanged(object sender, EventArgs e)
        {
            Engine.conf.GoogleTargetLanguage = GoogleTranslate.Languages[cbToLanguage.SelectedIndex].Name;
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

        #endregion Language Translator

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

        private void MediaWikiSetup(IEnumerable<MediaWikiAccount> accs)
        {
            if (accs != null)
            {
                ucMediaWikiAccounts.AccountsList.Items.Clear();
                Engine.conf.MediaWikiAccountList = new List<MediaWikiAccount>();
                Engine.conf.MediaWikiAccountList.AddRange(accs);
                foreach (MediaWikiAccount acc in Engine.conf.MediaWikiAccountList)
                {
                    ucMediaWikiAccounts.AccountsList.Items.Add(acc);
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

        private void FTPSetup(IEnumerable<FTPAccount> accs)
        {
            if (accs != null)
            {
                int selFtpList = ucFTPAccounts.AccountsList.SelectedIndex;
                int selFtpImages = cboFtpImages.SelectedIndex;
                int selFtpText = cboFtpText.SelectedIndex;
                int selFtpFiles = cboFtpFiles.SelectedIndex;

                ucFTPAccounts.AccountsList.Items.Clear();
                cboFtpImages.Items.Clear();
                cboFtpText.Items.Clear();
                cboFtpFiles.Items.Clear();
                Engine.conf.FTPAccountList = new List<FTPAccount>();
                Engine.conf.FTPAccountList.AddRange(accs);
                foreach (FTPAccount acc in Engine.conf.FTPAccountList)
                {
                    ucFTPAccounts.AccountsList.Items.Add(acc);
                    cboFtpImages.Items.Add(acc);
                    cboFtpText.Items.Add(acc);
                    cboFtpFiles.Items.Add(acc);
                }
                if (ucFTPAccounts.AccountsList.Items.Count > 0)
                {
                    ucFTPAccounts.AccountsList.SelectedIndex = Math.Max(Math.Min(selFtpList, ucFTPAccounts.AccountsList.Items.Count - 1), 0);
                    cboFtpImages.SelectedIndex = Math.Max(Math.Min(Engine.conf.FtpImages, ucFTPAccounts.AccountsList.Items.Count - 1), 0);
                    cboFtpText.SelectedIndex = Math.Max(Math.Min(Engine.conf.FtpText, ucFTPAccounts.AccountsList.Items.Count - 1), 0);
                    cboFtpFiles.SelectedIndex = Math.Max(Math.Min(Engine.conf.FtpFiles, ucFTPAccounts.AccountsList.Items.Count - 1), 0);
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
            FTPSetup(Engine.conf.FTPAccountList);
        }

        private void LocalhostAccountsList_SelectedIndexChanged(object sender, EventArgs e)
        {
            int sel = ucLocalhostAccounts.AccountsList.SelectedIndex;
            Engine.conf.LocalhostSelected = sel;
            if (Adapter.CheckList(Engine.conf.LocalhostAccountList, sel))
            {
                LocalhostAccount acc = Engine.conf.LocalhostAccountList[sel];
                ucLocalhostAccounts.SettingsGrid.SelectedObject = acc;
            }
        }

        private void LocalhostAccountRemoveButton_Click(object sender, EventArgs e)
        {
            int sel = ucLocalhostAccounts.AccountsList.SelectedIndex;
            if (ucLocalhostAccounts.RemoveItem(sel) == true)
            {
                Engine.conf.LocalhostAccountList.RemoveAt(sel);
            }
        }

        private void LocalhostAccountsSetup(IEnumerable<LocalhostAccount> accs)
        {
            if (accs != null)
            {
                ucLocalhostAccounts.AccountsList.Items.Clear();
                Engine.conf.LocalhostAccountList = new List<LocalhostAccount>();
                Engine.conf.LocalhostAccountList.AddRange(accs);
                foreach (LocalhostAccount acc in Engine.conf.LocalhostAccountList)
                {
                    ucLocalhostAccounts.AccountsList.Items.Add(acc);
                }
            }
        }

        private void LocalhostAccountAddButton_Click(object sender, EventArgs e)
        {
            LocalhostAccount acc = new LocalhostAccount("New Account");
            Engine.conf.LocalhostAccountList.Add(acc);
            ucLocalhostAccounts.AccountsList.Items.Add(acc);
            ucLocalhostAccounts.AccountsList.SelectedIndex = ucLocalhostAccounts.AccountsList.Items.Count - 1;
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

            if (Adapter.CheckList(Engine.conf.FTPAccountList, sel))
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
            FTPSetup(Engine.conf.FTPAccountList);
        }

        private void MindTouchAccountAddButton_Click(object sender, EventArgs e)
        {
            DekiWikiAccount acc = new DekiWikiAccount("New Account");
            Engine.conf.DekiWikiAccountList.Add(acc);
            ucMindTouchAccounts.AccountsList.Items.Add(acc);
            ucMindTouchAccounts.AccountsList.SelectedIndex = ucMindTouchAccounts.AccountsList.Items.Count - 1;
        }

        private void MediawikiAccountAddButton_Click(object sender, EventArgs e)
        {
            MediaWikiAccount acc = new MediaWikiAccount("New Account");
            Engine.conf.MediaWikiAccountList.Add(acc);
            ucMediaWikiAccounts.AccountsList.Items.Add(acc);
            ucMediaWikiAccounts.AccountsList.SelectedIndex = ucMediaWikiAccounts.AccountsList.Items.Count - 1;
        }

        private void MediaWikiAccountsList_SelectedIndexChanged(object sender, EventArgs e)
        {
            int sel = ucMediaWikiAccounts.AccountsList.SelectedIndex;
            Engine.conf.MediaWikiAccountSelected = sel;
            if (Engine.conf.MediaWikiAccountList != null && sel != -1 && sel < Engine.conf.MediaWikiAccountList.Count && Engine.conf.MediaWikiAccountList[sel] != null)
            {
                MediaWikiAccount acc = Engine.conf.MediaWikiAccountList[sel];
                ucMediaWikiAccounts.SettingsGrid.SelectedObject = acc;
            }
        }

        private void MediawikiAccountTestButton_Click(object sender, EventArgs e)
        {
            string text = ucMediaWikiAccounts.btnTest.Text;
            ucMediaWikiAccounts.btnTest.Text = "Testing...";
            ucMediaWikiAccounts.btnTest.Enabled = false;
            MediaWikiAccount acc = GetSelectedMediaWiki();
            if (acc != null)
            {
                Adapter.TestMediaWikiAccount(acc,
                    // callback for success
                    delegate()
                    {
                        // invoke on UI thread
                        Invoke((Action)delegate()
                        {
                            ucMediaWikiAccounts.btnTest.Enabled = true;
                            ucMediaWikiAccounts.btnTest.Text = text;
                            MessageBox.Show("Login successful!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        });
                    },
                    // callback for failure
                    delegate(string message)
                    {
                        // invoke on UI thread
                        Invoke((Action)delegate()
                        {
                            ucMediaWikiAccounts.btnTest.Enabled = true;
                            ucMediaWikiAccounts.btnTest.Text = text;
                            MessageBox.Show(message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        });
                    });
            }
        }

        private void MediawikiAccountRemoveButton_Click(object sender, EventArgs e)
        {
            int sel = ucMediaWikiAccounts.AccountsList.SelectedIndex;
            if (ucMediaWikiAccounts.RemoveItem(sel))
            {
                Engine.conf.MediaWikiAccountList.RemoveAt(sel);
            }
        }

        private MediaWikiAccount GetSelectedMediaWiki()
        {
            MediaWikiAccount account = null;
            if (Adapter.CheckMediaWikiAccounts())
            {
                account = Engine.conf.MediaWikiAccountList[Engine.conf.MediaWikiAccountSelected];
            }

            return account;
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
                acc = Engine.conf.FTPAccountList[Engine.conf.FtpImages];
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

        private void chkAutoSwitchFTP_CheckedChanged(object sender, EventArgs e)
        {
            Engine.conf.AutoSwitchFileUploader = chkAutoSwitchFileUploader.Checked;
        }

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
                DeleteHistoryFiles();
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
            Engine.conf.CropDynamicCrosshair = chkCropDynamicCrosshair.Checked;
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
            Engine.conf.CropShowBigCross = chkCropShowBigCross.Checked;
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
                rtbDebugInfo.Text = sb.ToString();
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
            string dirNew = Adapter.GetDirPathUsingFolderBrowser("Configure Root directory...");

            if (!string.IsNullOrEmpty(dirNew))
            {
                Engine.SetRootFolder(dirNew);
                txtRootFolder.Text = Engine.mAppSettings.RootDir;
                FileSystem.MoveDirectory(oldRootDir, txtRootFolder.Text);
                UpdateGuiControlsPaths();
                Engine.conf = XMLSettings.Read();
                ZScreen_ConfigGUI();
            }
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
            Engine.conf.CropShowMagnifyingGlass = chkCropShowMagnifyingGlass.Checked;
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
            Engine.conf.ScreenshotDelayTime = nudScreenshotDelay.Value;
        }

        private void nudtScreenshotDelay_SelectedIndexChanged(object sender, EventArgs e)
        {
            Engine.conf.ScreenshotDelayTimes = nudScreenshotDelay.Time;
        }

        private void lblToLanguage_MouseDown(object sender, MouseEventArgs e)
        {
            if (cbToLanguage.SelectedIndex > -1)
            {
                cbToLanguage.DoDragDrop(Engine.conf.GoogleTargetLanguage, DragDropEffects.Move);
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
            /*GoogleTranslate.GTLanguage lang = GoogleTranslate.FindLanguage(e.Data.GetData(DataFormats.Text).ToString(),
               ZScreen.mGTranslator.LanguageOptions.TargetLangList);
            Engine.conf.GoogleTargetLanguage2 = lang.Value;
            btnTranslateTo1.Text = "To " + lang.Name;*/
        }

        private void btnTranslateTo1_Click(object sender, EventArgs e)
        {
            Loader.Worker.TranslateTo1();
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
            ProxyAdd(new ProxyInfo(Environment.UserName, "", Adapter.GetDefaultWebProxyHost(), Adapter.GetDefaultWebProxyPort()));
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

            // Disable Capture children option if DWM is enabled
            if (chkActiveWindowPreferDWM.Checked)
            {
                chkActiveWindowTryCaptureChildren.Checked = false;
            }
            chkActiveWindowTryCaptureChildren.Enabled = !chkActiveWindowPreferDWM.Checked;

            // With GDI, corner-clearing cannot be disabled when both "clean background" and "include shadow" are enabled
            if (chkActiveWindowPreferDWM.Checked || !chkSelectedWindowCleanBackground.Checked || !chkSelectedWindowIncludeShadow.Checked)
            {
                chkSelectedWindowCleanTransparentCorners.Enabled = true;
            }
            else
            {
                chkSelectedWindowCleanTransparentCorners.Enabled = false;
                chkSelectedWindowCleanTransparentCorners.Checked = true;
            }
        }

        private void cbSelectedWindowCleanTransparentCorners_CheckedChanged(object sender, EventArgs e)
        {
            Engine.conf.ActiveWindowCleanTransparentCorners = chkSelectedWindowCleanTransparentCorners.Checked;
            UpdateAeroGlassConfig();
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
            ttZScreen.Show(ttZScreen.GetToolTip(nudScreenshotDelay), this);
        }

        private void txtImagesFolderPattern_TextChanged(object sender, EventArgs e)
        {
            Engine.conf.SaveFolderPattern = txtImagesFolderPattern.Text;
            lblImagesFolderPatternPreview.Text = new NameParser(NameParserType.SaveFolder).Convert(Engine.conf.SaveFolderPattern);
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
                Loader.Worker.StartWorkerPictures(WorkerTask.JobLevel2.UPLOAD_IMAGE, bmp);
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
            Engine.conf.SendSpaceAccountType = (AccountType)cboSendSpaceAcctType.SelectedIndex;
            txtSendSpacePassword.Enabled = Engine.conf.SendSpaceAccountType == AccountType.User;
            txtSendSpaceUserName.Enabled = Engine.conf.SendSpaceAccountType == AccountType.User;
        }

        private void btnSendSpaceRegister_Click(object sender, EventArgs e)
        {
            using (UserPassBox upb = Adapter.SendSpaceRegister())
            {
                if (upb.Success)
                {
                    txtSendSpaceUserName.Text = upb.UserName;
                    txtSendSpacePassword.Text = upb.Password;
                    cboSendSpaceAcctType.SelectedIndex = (int)AccountType.User;
                }
            }
        }

        private void txtFTPThumbWidth_TextChanged(object sender, EventArgs e)
        {
            int width;
            if (int.TryParse(txtFTPThumbWidth.Text, out width))
            {
                Engine.conf.FTPThumbnailWidth = width;
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
            if (ucFTPAccounts.AccountsList.SelectedIndex > -1)
            {
                FTPAccount acc = ucFTPAccounts.AccountsList.Items[ucFTPAccounts.AccountsList.SelectedIndex] as FTPAccount;
                if (acc != null)
                {
                    FTPClient2 ftpClient = new FTPClient2(acc) { Icon = this.Icon };
                    ftpClient.Show();
                }
            }
        }

        #region Flickr

        private void btnFlickrGetFrob_Click(object sender, EventArgs e)
        {
            try
            {
                FlickrUploader flickr = new FlickrUploader(Engine.FlickrKey, Engine.FlickrSecret);
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
                    FlickrUploader flickr = new FlickrUploader(Engine.FlickrKey, Engine.FlickrSecret);
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
                        FlickrUploader flickr = new FlickrUploader(Engine.FlickrKey, Engine.FlickrSecret);
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
                    FlickrUploader flickr = new FlickrUploader(Engine.FlickrKey, Engine.FlickrSecret);
                    string url = flickr.GetPhotosLink(userID);
                    Process.Start(url);
                }
            }
        }

        #endregion Flickr

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
                    Engine.ZScreenKeyboardHook.KeyDown += new KeyEventHandler(Loader.Worker.CheckHotkeys);
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
                HistoryItem hi = lbHistory.SelectedItem as HistoryItem;
                if (hi != null && !string.IsNullOrEmpty(hi.RemotePath))
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
            { // TODO: TesterGUI paths
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
                OAuthInfo acc = Adapter.TwitterGetActiveAcct();
                Twitter twitter = new Twitter(acc);
                string url = twitter.GetAuthorizationURL();

                if (!string.IsNullOrEmpty(url))
                {
                    Engine.conf.TwitterOAuthInfoList[Engine.conf.TwitterAcctSelected] = acc;
                    Process.Start(url);
                    ucTwitterAccounts.SettingsGrid.SelectedObject = acc;
                }
            }
        }

        private void TwitterAccountRemoveButton_Click(object sender, EventArgs e)
        {
            int sel = ucTwitterAccounts.AccountsList.SelectedIndex;
            if (ucTwitterAccounts.RemoveItem(sel) == true)
            {
                Engine.conf.TwitterOAuthInfoList.RemoveAt(sel);
            }
        }

        private void TwitterAccountList_SelectedIndexChanged(object sender, EventArgs e)
        {
            int sel = ucTwitterAccounts.AccountsList.SelectedIndex;
            Engine.conf.TwitterAcctSelected = sel;

            if (Adapter.CheckList(Engine.conf.TwitterOAuthInfoList, Engine.conf.TwitterAcctSelected))
            {
                OAuthInfo acc = Engine.conf.TwitterOAuthInfoList[sel];
                ucTwitterAccounts.SettingsGrid.SelectedObject = acc;
            }
        }

        private void TwitterAccountAddButton_Click(object sender, EventArgs e)
        {
            OAuthInfo acc = new OAuthInfo(Engine.TwitterConsumerKey, Engine.TwitterConsumerSecret);
            Engine.conf.TwitterOAuthInfoList.Add(acc);
            ucTwitterAccounts.AccountsList.Items.Add(acc);
            ucTwitterAccounts.AccountsList.SelectedIndex = ucTwitterAccounts.AccountsList.Items.Count - 1;
            if (Adapter.CheckTwitterAccounts())
            {
                ucTwitterAccounts.SettingsGrid.SelectedObject = acc;
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
            Engine.conf.ActiveWindowTryCaptureChildren = chkActiveWindowTryCaptureChildren.Checked;
        }

        private void chkActiveWindowPreferDWM_CheckedChanged(object sender, EventArgs e)
        {
            Engine.conf.ActiveWindowPreferDWM = chkActiveWindowPreferDWM.Checked;
            chkActiveWindowTryCaptureChildren.Enabled = !chkActiveWindowPreferDWM.Checked;
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

        private void LbSoftwareItemCheck(object sender, ItemCheckEventArgs e)
        {
            UpdateGuiEditors(sender);
        }

        private void tcOptions_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tcOptions.SelectedTab == tpStats && !mDebug.DebugTimer.Enabled)
            {
                btnDebugStart_Click(sender, e);
            }
        }

        private void txtDebugLog_LinkClicked(object sender, LinkClickedEventArgs e)
        {
            Process.Start(e.LinkText);
        }

        private void cbCloseButtonAction_SelectedIndexChanged(object sender, EventArgs e)
        {
            Engine.conf.CloseButtonAction = (WindowButtonAction)cboCloseButtonAction.SelectedIndex;
        }

        private void cbMinimizeButtonAction_SelectedIndexChanged(object sender, EventArgs e)
        {
            Engine.conf.MinimizeButtonAction = (WindowButtonAction)cboMinimizeButtonAction.SelectedIndex;
        }

        private void LbSoftwareMouseClick(object sender, MouseEventArgs e)
        {
            int sel = lbSoftware.IndexFromPoint(e.X, e.Y);
            if (sel != -1)
            {
                lbSoftware.SetItemChecked(sel, !lbSoftware.GetItemChecked(sel));
            }
        }

        private void BtnBrowseImagesDirClick(object sender, EventArgs e)
        {
            string oldDir = txtImagesDir.Text;
            string dirNew = Adapter.GetDirPathUsingFolderBrowser("Configure Custom Images Directory...");

            if (!string.IsNullOrEmpty(dirNew))
            {
                Engine.conf.UseCustomImagesDir = true;
                Engine.conf.CustomImagesDir = dirNew;
                FileSystem.MoveDirectory(oldDir, txtImagesDir.Text);
                UpdateGuiControlsPaths();
            }
        }

        private void chkPreferSystemFolders_CheckedChanged(object sender, EventArgs e)
        {
            if (mGuiIsReady)
            {
                Engine.conf.PreferSystemFolders = chkPreferSystemFolders.Checked;
                ZScreen_ConfigGUI();
            }
        }

        private void btnResetHotkeys_Click(object sender, EventArgs e)
        {
            Loader.Worker.mHotkeyMgr.ResetHotkeys();
        }

        private void editInPicnikToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (lbHistory.SelectedItem != null)
            {
                HistoryItem hi = (HistoryItem)lbHistory.SelectedItem;
                if (!string.IsNullOrEmpty(hi.RemotePath))
                {
                    Process.Start(string.Format("http://www.picnik.com/service/?_import={0}&_apikey={1}",
                        HttpUtility.UrlEncode(hi.RemotePath), Engine.PicnikKey));
                }
            }
        }

        private void cbFreehandCropShowHelpText_CheckedChanged(object sender, EventArgs e)
        {
            Engine.conf.FreehandCropShowHelpText = cbFreehandCropShowHelpText.Checked;
        }

        private void cbFreehandCropAutoUpload_CheckedChanged(object sender, EventArgs e)
        {
            Engine.conf.FreehandCropAutoUpload = cbFreehandCropAutoUpload.Checked;
        }

        private void cbFreehandCropAutoClose_CheckedChanged(object sender, EventArgs e)
        {
            Engine.conf.FreehandCropAutoClose = cbFreehandCropAutoClose.Checked;
        }

        private void cbFreehandCropShowRectangleBorder_CheckedChanged(object sender, EventArgs e)
        {
            Engine.conf.FreehandCropShowRectangleBorder = cbFreehandCropShowRectangleBorder.Checked;
        }

        private void ZScreen_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (e.CloseReason == CloseReason.WindowsShutDown)
            {
                WriteSettings();
            }
        }

        private void cboFtpImages_SelectedIndexChanged(object sender, EventArgs e)
        {
            Engine.conf.FtpImages = cboFtpImages.SelectedIndex;
        }

        private void cboFtpText_SelectedIndexChanged(object sender, EventArgs e)
        {
            Engine.conf.FtpText = cboFtpText.SelectedIndex;
        }

        private void cboFtpFiles_SelectedIndexChanged(object sender, EventArgs e)
        {
            Engine.conf.FtpFiles = cboFtpFiles.SelectedIndex;
        }

        private void cboProxyConfig_SelectedIndexChanged(object sender, EventArgs e)
        {
            Engine.conf.ProxyConfig = (ProxyConfigType)cboProxyConfig.SelectedIndex;
            if (mGuiIsReady)
            {
                Uploader.ProxySettings = Adapter.CheckProxySettings();
            }
        }

        private void btnDropboxLogin_Click(object sender, EventArgs e)
        {
            string email = txtDropboxEmail.Text;
            string password = txtDropboxPassword.Text;

            if (!string.IsNullOrEmpty(email) && !string.IsNullOrEmpty(password))
            {
                try
                {
                    Dropbox dropbox = new Dropbox(new OAuthInfo(Engine.DropboxConsumerKey, Engine.DropboxConsumerSecret));
                    DropboxUserLogin login = dropbox.Login(email, password);

                    if (login != null)
                    {
                        DropboxAccountInfo account = dropbox.GetAccountInfo();

                        if (account != null)
                        {
                            Engine.conf.DropboxUserToken = login.token;
                            Engine.conf.DropboxUserSecret = login.secret;
                            Engine.conf.DropboxEmail = account.email;
                            Engine.conf.DropboxName = account.display_name;
                            Engine.conf.DropboxUserID = account.uid.ToString();
                            Engine.conf.DropboxUploadPath = txtDropboxPath.Text;
                            UpdateDropboxStatus();
                            SystemSounds.Exclamation.Play();
                            return;
                        }
                        else
                        {
                            MessageBox.Show("GetAccountInfo failed.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Login failed.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                Engine.conf.DropboxUserToken = Engine.conf.DropboxUserSecret = string.Empty;
                UpdateDropboxStatus();
            }
        }

        private void UpdateDropboxStatus()
        {
            if (!string.IsNullOrEmpty(Engine.conf.DropboxUserToken) && !string.IsNullOrEmpty(Engine.conf.DropboxUserSecret))
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendLine("Login status: Success");
                sb.AppendLine("Email: " + Engine.conf.DropboxEmail);
                sb.AppendLine("Name: " + Engine.conf.DropboxName);
                sb.AppendLine("User ID: " + Engine.conf.DropboxUserID);
                if (!string.IsNullOrEmpty(Engine.conf.DropboxUploadPath))
                {
                    string uploadPath = new NameParser { IsFolderPath = true }.Convert(Engine.conf.DropboxUploadPath);
                    sb.AppendLine("Upload path: " + uploadPath);
                    sb.AppendLine("Download path: " + Dropbox.GetDropboxURL(Engine.conf.DropboxUserID, uploadPath, "{Filename}"));
                }
                lblDropboxStatus.Text = sb.ToString();
            }
            else
            {
                lblDropboxStatus.Text = "Login status: Login is required";
            }
        }

        private void txtDropboxPath_TextChanged(object sender, EventArgs e)
        {
            Engine.conf.DropboxUploadPath = txtDropboxPath.Text;
            UpdateDropboxStatus();
        }

        private void pbDropboxLogo_Click(object sender, EventArgs e)
        {
            Process.Start("https://www.dropbox.com");
        }

        private void btnDropboxRegister_Click(object sender, EventArgs e)
        {
            Process.Start("https://www.dropbox.com/register");
        }

        private void btnImgurOpenAuthorizePage_Click(object sender, EventArgs e)
        {
            try
            {
                OAuthInfo oauth = new OAuthInfo(Engine.ImgurConsumerKey, Engine.ImgurConsumerSecret);

                string url = new Imgur(oauth).GetAuthorizationURL();

                if (!string.IsNullOrEmpty(url))
                {
                    Engine.conf.ImgurOAuthInfo = oauth;
                    Process.Start(url);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnImgurLogin_Click(object sender, EventArgs e)
        {
            try
            {
                using (ZScreenLib.InputBox inputBox = new ZScreenLib.InputBox("Imgur user account", "Enter verification code:"))
                {
                    if (inputBox.ShowDialog() == DialogResult.OK)
                    {
                        string verification = inputBox.InputText;

                        if (!string.IsNullOrEmpty(verification) && Engine.conf.ImgurOAuthInfo != null &&
                            !string.IsNullOrEmpty(Engine.conf.ImgurOAuthInfo.AuthToken) && !string.IsNullOrEmpty(Engine.conf.ImgurOAuthInfo.AuthSecret))
                        {
                            bool result = new Imgur(Engine.conf.ImgurOAuthInfo).GetAccessToken(verification);

                            if (result)
                            {
                                lblImgurStatus.Text = "User token: " + Engine.conf.ImgurOAuthInfo.UserToken;
                                MessageBox.Show("Login success.", "ZScreen", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                lblImgurStatus.Text = "Login is required";
                                MessageBox.Show("Login failed.", "ZScreen", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cbImgurUseAccount_CheckedChanged(object sender, EventArgs e)
        {
            if (chkImgurUserAccount.Checked)
            {
                Engine.conf.ImgurAccountType = AccountType.User;
            }
            else
            {
                Engine.conf.ImgurAccountType = AccountType.Anonymous;
            }
        }

        private void btnTwitterLogin_Click(object sender, EventArgs e)
        {
            OAuthInfo acc = Adapter.TwitterGetActiveAcct();
            string verification = acc.AuthVerifier;

            if (!string.IsNullOrEmpty(verification) && acc != null &&
                !string.IsNullOrEmpty(acc.AuthToken) && !string.IsNullOrEmpty(acc.AuthSecret))
            {
                Twitter twitter = new Twitter(acc);
                bool result = twitter.GetAccessToken(acc.AuthVerifier);

                if (result)
                {
                    MessageBox.Show("Login success.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Login failed.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
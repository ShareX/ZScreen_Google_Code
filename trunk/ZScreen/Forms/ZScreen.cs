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
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using GradientTester;
using GraphicsMgrLib;
using HelpersLib;
using UploadersAPILib;
using UploadersLib;
using UploadersLib.HelperClasses;
using ZScreenGUI.Properties;
using ZScreenGUI.UserControls;
using ZScreenLib;
using ZSS.ColorsLib;
using ZSS.FTPClientLib;
using ZSS.UpdateCheckerLib;

namespace ZScreenGUI
{
    public partial class ZScreen : HotkeyForm
    {
        #region Variables

        public bool IsReady;
        public CloseMethod CloseMethod;

        private int mHadFocusAt;
        private TextBox mHadFocus;
        private ContextMenuStrip codesMenu = new ContextMenuStrip();
        private DebugHelper mDebug = null;
        private ImageList tabImageList = new ImageList();

        #endregion Variables

        public ZScreen()
        {
            InitializeComponent();
            ZScreen_Preconfig();
            this.WindowState = Engine.conf.ShowMainWindow ? FormWindowState.Normal : FormWindowState.Minimized;
        }

        private void ZScreen_Load(object sender, EventArgs e)
        {
            Uploader.ProxySettings = Adapter.CheckProxySettings();
            ZScreen_ConfigGUI();

            PerformOnlineTasks();
            if (Engine.conf.CheckUpdates)
            {
                CheckUpdates();
            }

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

                Rectangle screenRect = GraphicsMgr.GetScreenBounds();
                screenRect.Inflate(-100, -100);
                if (screenRect.IntersectsWith(new Rectangle(Engine.conf.WindowLocation, Engine.conf.WindowSize)))
                {
                    this.Size = Engine.conf.WindowSize;
                    this.Location = Engine.conf.WindowLocation;
                }
            }

            CleanCache();
            StartStatistics();

            SetToolTip(nudScreenshotDelay);

            CreateCodesMenu();

            dgvHotkeys.BackgroundColor = Color.FromArgb(tpHotkeys.BackColor.R, tpHotkeys.BackColor.G, tpHotkeys.BackColor.B);

            niTray.Visible = true;

            new RichTextBoxMenu(rtbDebugLog, true);
            new RichTextBoxMenu(rtbStats, true);

            Application.Idle += new EventHandler(Application_Idle);

            Engine.MyLogger.WriteLine(new StackFrame().GetMethod().Name);
        }

        public bool UseCommandLineArg(string arg)
        {
            if (!string.IsNullOrEmpty(arg))
            {
                CLIManager cli = new CLIManager();

                cli.Commands = new List<Command>()
                {
                    new Command("fu|fileupload", filePath => UploadUsingFileSystem(filePath)),
                    new Command("cu|clipboardupload", () => UploadUsingClipboard()),
                    new Command("fs|fullscreen", () => CaptureEntireScreen()),
                    new Command("cc|crop", () => CaptureRectRegion()),
                    new Command("sw|selectedwindow", () => CaptureSelectedWindow()),
                    new Command("hi|history", () => OpenHistory())
                };

                cli.FilePathAction = filePath => UploadUsingFileSystem(filePath);

                return cli.Parse(arg);
            }

            return false;
        }

        protected override void WndProc(ref Message m)
        {
            if (IsReady)
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
                                    UploadUsingClipboard();
                                }
                            }
                        }
                        catch (ExternalException ex)
                        {
                            // Copying a field definition in Access 2002 causes this sometimes?
                            Engine.MyLogger.WriteException(ex, "InteropServices.ExternalException in ZScreen.WndProc");
                            return;
                        }
                        catch (Exception ex)
                        {
                            Engine.MyLogger.WriteException(ex, "Error monitoring clipboard");
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
                    case NativeMethods.WM_SYSCOMMAND:
                        if (m.WParam.ToInt32() == NativeMethods.SC_MINIMIZE) // Minimize button handling
                        {
                            switch (Engine.conf.WindowButtonActionMinimize)
                            {
                                case WindowButtonAction.ExitApplication:
                                    CloseMethod = CloseMethod.MinimizeButton;
                                    Close();
                                    break;
                                case WindowButtonAction.MinimizeToTaskbar:
                                    WindowState = FormWindowState.Minimized;
                                    break;
                                case WindowButtonAction.MinimizeToTray:
                                    Hide();
                                    break;
                            }

                            m.Result = IntPtr.Zero;
                            return;
                        }
                        break;
                }
            }

            base.WndProc(ref m);
        }

        private void tsmiTab_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem tsmi = sender as ToolStripMenuItem;
            tcMain.SelectedTab = tcMain.TabPages[(string)tsmi.Tag];

            ShowWindow();
            tcMain.Focus();
        }

        private void exitZScreenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CloseMethod = CloseMethod.TrayButton;
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

        private void btnRegCodeImageShack_Click(object sender, EventArgs e)
        {
            Process.Start("http://profile.imageshack.us/prefs");
        }

        private void btnGalleryImageShack_Click(object sender, EventArgs e)
        {
            Process.Start("http://my.imageshack.us/v_images.php");
        }

        private void ZScreen_Resize(object sender, EventArgs e)
        {
            if (IsReady)
            {
                Engine.conf.WindowState = WindowState;

                if (WindowState == FormWindowState.Normal)
                {
                    if (Engine.conf.SaveFormSizePosition)
                    {
                        Engine.conf.WindowLocation = Location;
                        Engine.conf.WindowSize = Size;
                    }
                }

                Refresh();
            }
        }

        private void ZScreen_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Save Destinations

            Adapter.SaveMenuConfigToList(ucDestOptions.tsddbOutputs, Engine.conf.ConfOutputs);
            Adapter.SaveMenuConfigToList(ucDestOptions.tsddbLinkFormat, Engine.conf.ConfLinkFormat);
            Adapter.SaveMenuConfigToList(ucDestOptions.tsddbClipboardContent, Engine.conf.ConfClipboardContent);
            Adapter.SaveMenuConfigToList(ucDestOptions.tsddbDestImage, Engine.conf.MyImageUploaders);
            Adapter.SaveMenuConfigToList(ucDestOptions.tsddDestFile, Engine.conf.MyFileUploaders);
            Adapter.SaveMenuConfigToList(ucDestOptions.tsddDestText, Engine.conf.MyTextUploaders);
            Adapter.SaveMenuConfigToList(ucDestOptions.tsddbDestLink, Engine.conf.MyURLShorteners);

            // If UserClosing && ZScreenCloseReason.None then this means close button pressed in title bar
            if (e.CloseReason == CloseReason.UserClosing && CloseMethod == CloseMethod.None)
            {
                if (Engine.conf.WindowButtonActionClose == WindowButtonAction.ExitApplication)
                {
                    CloseMethod = CloseMethod.CloseButton;
                }
                else if (Engine.conf.WindowButtonActionClose == WindowButtonAction.MinimizeToTaskbar)
                {
                    WindowState = FormWindowState.Minimized;
                    e.Cancel = true;
                }
                else if (Engine.conf.WindowButtonActionClose == WindowButtonAction.MinimizeToTray)
                {
                    Hide();
                    DelayedTrimMemoryUse();
                    if (Engine.conf.AutoSaveSettings) Engine.WriteSettingsAsync();
                    e.Cancel = true;
                }
            }

            // If really ZScreen is closing
            if (!e.Cancel)
            {
                Engine.MyLogger.WriteLine("ZScreen_FormClosing - CloseReason: {0}, CloseMethod: {1}", e.CloseReason, CloseMethod);
                Engine.WriteSettings();
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
            if (Engine.conf.EnableAutoMemoryTrim)
            {
                try
                {
                    lock (trimMemoryLock)
                    {
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
                    Engine.MyLogger.WriteException(ex, "Error in DelayedTrimMemoryUse");
                }
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

        private void RewriteImageEditorsRightClickMenu()
        {
            if (Engine.conf.ActionsList != null)
            {
                tsmEditinImageSoftware.DropDownDirection = ToolStripDropDownDirection.Right;
                tsmEditinImageSoftware.DropDownItems.Clear();

                List<Software> imgs = Engine.conf.ActionsList;

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
            if (IsReady)
            {
                if (sender.GetType() == lbSoftware.GetType())
                {
                    // the checked state needs to be inversed for some weird reason to get it working properly
                    if (Engine.conf.ActionsList.CheckSelected(lbSoftware.SelectedIndex))
                    {
                        Engine.conf.ActionsList[lbSoftware.SelectedIndex].Enabled = !lbSoftware.GetItemChecked(lbSoftware.SelectedIndex);
                        ToolStripMenuItem tsm = tsmEditinImageSoftware.DropDownItems[lbSoftware.SelectedIndex] as ToolStripMenuItem;
                        tsm.Checked = Engine.conf.ActionsList[lbSoftware.SelectedIndex].Enabled;
                    }
                }
                else if (sender.GetType() == typeof(ToolStripMenuItem))
                {
                    ToolStripMenuItem tsm = sender as ToolStripMenuItem;
                    int sel = (int)tsm.Tag;
                    if (Engine.conf.ActionsList.CheckSelected(sel))
                    {
                        Engine.conf.ActionsList[sel].Enabled = tsm.Checked;
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

        /// <summary>
        /// Browse for an applicatoin
        /// </summary>
        /// <returns>Software</returns>
        private Software BrowseApplication()
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
            ShowWindow();
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

        private void ShowWindow()
        {
            this.Show();
            this.WindowState = FormWindowState.Normal;
            this.Activate();
            this.BringToFront();
        }

        private void tsmLic_Click(object sender, EventArgs e)
        {
            FormsMgr.ShowLicense();
        }

        private void chkManualNaming_CheckedChanged(object sender, EventArgs e)
        {
            Engine.conf.PromptForOutputs = chkManualNaming.Checked;
        }

        private void ZScreen_Shown(object sender, EventArgs e)
        {
            Engine.zHandle = this.Handle;
            Engine.ClipboardHook();

            if (Engine.conf.ProxyConfig != ProxyConfigType.NoProxy)
            {
                Engine.MyLogger.WriteLine("Proxy Settings: " + Uploader.ProxySettings.ProxyActive.ToString());
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
                ZScreen_Windows7onlyTasks();
            }

            if (Engine.conf.ShowMainWindow)
            {
                if (Engine.conf.WindowState == FormWindowState.Maximized)
                {
                    this.WindowState = FormWindowState.Maximized;
                }
                else
                {
                    this.WindowState = FormWindowState.Normal;
                }
                ShowInTaskbar = Engine.conf.ShowInTaskbar;
            }
            else if (Engine.conf.ShowInTaskbar && Engine.conf.WindowButtonActionClose == WindowButtonAction.MinimizeToTaskbar)
            {
                this.WindowState = FormWindowState.Minimized;
            }
            else
            {
                Hide();
            }

            UpdateHotkeys(false);
            InitKeyboardHook();

            Engine.conf.FirstRun = false;

            if (Engine.IsMultipleInstance)
            {
                niTray.ShowBalloonTip(2000, Engine.GetProductName(), string.Format("Another instance of {0} is already running...", Application.ProductName), ToolTipIcon.Warning);
                niTray.BalloonTipClicked += new EventHandler(niTray2_BalloonTipClicked);
            }

            if (Engine.conf.FirstRun)
            {
                ShowWindow();
            }

            Engine.MyLogger.WriteLine("ZScreen_Shown. Startup time: {0} ms", Engine.StartTimer.ElapsedMilliseconds);

            UseCommandLineArg(Loader.CommandLineArg);
            IsReady = true;
        }

        private void niTray2_BalloonTipClicked(object sender, EventArgs e)
        {
            this.Show();
            this.WindowState = FormWindowState.Normal;
        }

        private void clipboardUpload_Click(object sender, EventArgs e)
        {
            UploadUsingClipboard();
        }

        private void selWindow_Click(object sender, EventArgs e)
        {
            CaptureSelectedWindow();
        }

        private void tsmAboutMain_Click(object sender, EventArgs e)
        {
            FormsMgr.ShowAboutWindow();
        }

        public void cbStartWin_CheckedChanged(object sender, EventArgs e)
        {
            RegistryHelper.SetStartWithWindows(chkStartWin.Checked);
        }

        private void nudFlashIconCount_ValueChanged(object sender, EventArgs e)
        {
            Engine.conf.FlashTrayCount = nudFlashIconCount.Value;
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
                Engine.conf.ActionsList.Add(temp);
                lbSoftware.Items.Add(temp);
                lbSoftware.SelectedIndex = lbSoftware.Items.Count - 1;
                RewriteImageEditorsRightClickMenu();
            }
        }

        private void btnAddImageSoftware_Click(object sender, EventArgs e)
        {
            Software temp = BrowseApplication();
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
                Engine.conf.ActionsList.RemoveAt(sel);

                lbSoftware.Items.RemoveAt(sel);

                if (lbSoftware.Items.Count > 0)
                {
                    lbSoftware.SelectedIndex = (sel > 0) ? (sel - 1) : 0;
                }
            }

            RewriteImageEditorsRightClickMenu();
        }

        private void SetActiveImageSoftware()
        {
            Engine.conf.ImageEditor = Engine.conf.ActionsList[lbSoftware.SelectedIndex];
        }

        private void ShowImageEditorsSettings()
        {
            if (lbSoftware.SelectedItem != null)
            {
                Software app = GetImageSoftware(lbSoftware.SelectedItem.ToString());
                if (app != null)
                {
                    Engine.conf.ActionsList[lbSoftware.SelectedIndex].Enabled = lbSoftware.GetItemChecked(lbSoftware.SelectedIndex);
                    pgEditorsImage.SelectedObject = app;
                    pgEditorsImage.Enabled = !app.Protected;
                    btnRemoveImageEditor.Enabled = !app.Protected;

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
            Engine.conf.ImageFormat = (EImageFormat)cboFileFormat.SelectedIndex;
            Engine.SetImageFormat(ref Engine.zImageFileFormat, Engine.conf.ImageFormat);
        }

        private void txtImageQuality_ValueChanged(object sender, EventArgs e)
        {
            Engine.conf.ImageJPEGQuality = (int)nudImageQuality.Value;
        }

        private void cboSwitchFormat_SelectedIndexChanged(object sender, EventArgs e)
        {
            Engine.conf.ImageFormat2 = (EImageFormat)cboSwitchFormat.SelectedIndex;
            Engine.SetImageFormat(ref Engine.zImageFileFormatSwitch, Engine.conf.ImageFormat2);
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
            CaptureRectRegion();
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
                    new BalloonTipHelper(ni).ClickBalloonTip();
                }
                catch (Exception ex)
                {
                    Engine.MyLogger.WriteException(ex, "Error while clicking Balloon Tip");
                }
            }
        }

        private void ZScreen_Leave(object sender, EventArgs e)
        {
            QuitSettingHotkeys();
        }

        private void cbCropStyle_SelectedIndexChanged(object sender, EventArgs e)
        {
            Engine.conf.CropRegionStyles = (RegionStyles)chkCropStyle.SelectedIndex;
        }

        private void pbCropBorderColor_Click(object sender, EventArgs e)
        {
            SelectColor((PictureBox)sender, ref Engine.conf.CropBorderArgb);
        }

        private void nudCropBorderSize_ValueChanged(object sender, EventArgs e)
        {
            Engine.conf.CropBorderSize = nudCropBorderSize.Value;
        }

        private void llblBugReports_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(ZLinks.URL_ISSUES);
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
                pbWatermarkFontColor.BackColor = Engine.conf.WatermarkFontArgb;
                lblWatermarkFont.Text = FontToString();
                TestWatermark();
            }
        }

        private string FontToString()
        {
            return FontToString(Engine.conf.WatermarkFont, Engine.conf.WatermarkFontArgb);
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
            CaptureEntireScreen();
        }

        private void selectedWindowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Thread.Sleep(300);
            CaptureSelectedWindow();
        }

        private void rectangularRegionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Thread.Sleep(300);
            CaptureRectRegion();
        }

        private void lastRectangularRegionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Thread.Sleep(300);
            CaptureRectRegionLast();
        }

        private void tsmFreehandCropShot_Click(object sender, EventArgs e)
        {
            Thread.Sleep(300);
            CaptureFreeHandRegion();
        }

        private void autoScreenshotsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowAutoCapture();
        }

        private void tsmFileUpload_Click(object sender, EventArgs e)
        {
            FileUpload();
        }

        private void tsmUploadFromClipboard_Click(object sender, EventArgs e)
        {
            ClipboardUpload();
        }

        private void tsmDropWindow_Click(object sender, EventArgs e)
        {
            ShowDropWindow();
        }

        private void languageTranslatorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StartWorkerTranslator();
        }

        private void screenColorPickerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ScreenColorPicker();
        }

        private void pbWatermarkGradient1_Click(object sender, EventArgs e)
        {
            SelectColor((PictureBox)sender, ref Engine.conf.WatermarkGradient1Argb);
            TestWatermark();
        }

        private void pbWatermarkGradient2_Click(object sender, EventArgs e)
        {
            SelectColor((PictureBox)sender, ref Engine.conf.WatermarkGradient2Argb);
            TestWatermark();
        }

        private void pbWatermarkBorderColor_Click(object sender, EventArgs e)
        {
            SelectColor((PictureBox)sender, ref Engine.conf.WatermarkBorderArgb);
            TestWatermark();
        }

        private void TestWatermark()
        {
            using (Bitmap bmp = Resources.main.Clone(new Rectangle(62, 33, 199, 140), PixelFormat.Format32bppArgb))
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
            SelectColor((PictureBox)sender, ref Engine.conf.WatermarkFontArgb);
            lblWatermarkFont.Text = FontToString();
            TestWatermark();
        }

        private void SelectColor(Control pb, ref XColor color)
        {
            DialogColor dColor = new DialogColor(pb.BackColor);
            if (dColor.ShowDialog() == DialogResult.OK)
            {
                pb.BackColor = dColor.Color;
                color = dColor.Color;
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

        private void btnViewLocalDirectory_Click(object sender, EventArgs e)
        {
            ShowDirectory(FileSystem.GetImagesDir());
        }

        private void btnViewRemoteDirectory_Click(object sender, EventArgs e)
        {
            ShowDirectory(Engine.LogsDir);
        }

        private void cbOpenMainWindow_CheckedChanged(object sender, EventArgs e)
        {
            Engine.conf.ShowMainWindow = chkOpenMainWindow.Checked;
        }

        private void cbShowTaskbar_CheckedChanged(object sender, EventArgs e)
        {
            Engine.conf.ShowInTaskbar = chkShowTaskbar.Checked;
            if (IsReady)
            {
                if (!chkShowTaskbar.Checked)
                {
                    this.chkWindows7TaskbarIntegration.Checked = false; // Windows 7 Taskbar Integration cannot work without showing in Taskbar
                }
                this.ShowInTaskbar = Engine.conf.ShowInTaskbar;
            }
        }

        private void llWebsite_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(ZLinks.URL_WEBSITE);
        }

        private void llProjectPage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(ZLinks.URL_WIKIPAGES);
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

        private void chkBalloonTipOpenLink_CheckedChanged(object sender, EventArgs e)
        {
            Engine.conf.BalloonTipOpenLink = chkBalloonTipOpenLink.Checked;
        }

        private void cmVersionHistory_Click(object sender, EventArgs e)
        {
            FormsMgr.ShowVersionHistory();
        }

        #region Language Translator

        #endregion Language Translator

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
                acc = Engine.MyUploadersConfig.FTPAccountList[Engine.MyUploadersConfig.FTPSelectedImage];
            }

            return acc;
        }

        private void cbSelectedWindowRectangleInfo_CheckedChanged(object sender, EventArgs e)
        {
            Engine.conf.SelectedWindowRectangleInfo = cbSelectedWindowRectangleInfo.Checked;
        }

        private void pbSelectedWindowBorderColor_Click(object sender, EventArgs e)
        {
            SelectColor((PictureBox)sender, ref Engine.conf.SelectedWindowBorderArgb);
        }

        private void nudSelectedWindowBorderSize_ValueChanged(object sender, EventArgs e)
        {
            Engine.conf.SelectedWindowBorderSize = nudSelectedWindowBorderSize.Value;
        }

        private void btnCheckUpdate_Click(object sender, EventArgs e)
        {
            CheckUpdates();
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
            foreach (Software app in Engine.conf.ActionsList)
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

        private void nudHistoryMaxItems_ValueChanged(object sender, EventArgs e)
        {
            Engine.conf.HistoryMaxNumber = (int)nudHistoryMaxItems.Value;
        }

        private void cbCloseDropBox_CheckedChanged(object sender, EventArgs e)
        {
            Engine.conf.CloseDropBox = cbCloseDropBox.Checked;
        }

        private void btnResetIncrement_Click(object sender, EventArgs e)
        {
            Engine.conf.AutoIncrement = 0;
        }

        private void cbHistorySave_CheckedChanged(object sender, EventArgs e)
        {
            Engine.conf.HistorySave = cbHistorySave.Checked;
        }

        private void pbCropCrosshairColor_Click(object sender, EventArgs e)
        {
            SelectColor((PictureBox)sender, ref Engine.conf.CropCrosshairArgb);
        }

        private void chkCaptureFallback_CheckedChanged(object sender, EventArgs e)
        {
            Engine.conf.CaptureEntireScreenOnError = chkCaptureFallback.Checked;
        }

        private void nudSwitchAfter_ValueChanged(object sender, EventArgs e)
        {
            Engine.conf.ImageSizeLimit = (int)nudSwitchAfter.Value;
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

        private void StartStatistics()
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
                rtbStats.Text = sb.ToString();
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
            ShowWindow();
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
                txtRootFolder.Text = Engine.AppConf.RootDir;
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

        private void numericUpDownTimer1_ValueChanged(object sender, EventArgs e)
        {
            Engine.conf.ScreenshotDelayTime = nudScreenshotDelay.Value;
        }

        private void nudtScreenshotDelay_SelectedIndexChanged(object sender, EventArgs e)
        {
            Engine.conf.ScreenshotDelayTimes = nudScreenshotDelay.Time;
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

        private void pgApp_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            ZScreen_ConfigGUI();
        }

        private void pgEditorsImage_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            Software temp = Engine.conf.ActionsList[lbSoftware.SelectedIndex];
            lbSoftware.Items[lbSoftware.SelectedIndex] = temp;
            Engine.conf.ActionsList[lbSoftware.SelectedIndex] = temp;
            RewriteImageEditorsRightClickMenu();
        }

        private void cbShowHelpBalloonTips_CheckedChanged(object sender, EventArgs e)
        {
            Engine.conf.ShowHelpBalloonTips = cbShowHelpBalloonTips.Checked;
            ttZScreen.Active = Engine.conf.ShowHelpBalloonTips;
        }

        private void tsmFTPClient_Click(object sender, EventArgs e)
        {
            OpenFTPClient();
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
                Engine.MyLogger.WriteException(ex, "Error while moving image files");
                MessageBox.Show(ex.Message);
            }
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
                WorkerTask wpTask = CreateTask(WorkerTask.JobLevel2.UploadImage);
                Bitmap bmp = new Bitmap(pbWebPageImage.Image);
                wpTask.SetImage(bmp);
                wpTask.RunWorker();
            }
        }

        private void cbWebPageAutoUpload_CheckedChanged(object sender, EventArgs e)
        {
            Engine.conf.WebPageAutoUpload = cbWebPageAutoUpload.Checked;
        }

        private void chkWindows7TaskbarIntegration_CheckedChanged(object sender, EventArgs e)
        {
            if (IsReady)
            {
                if (chkWindows7TaskbarIntegration.Checked)
                {
                    chkShowTaskbar.Checked = true; // Application requires to be shown in Taskbar for Windows 7 Integration
                }
                Engine.conf.Windows7TaskbarIntegration = chkWindows7TaskbarIntegration.Checked;
                ZScreen_Windows7onlyTasks();
            }
        }

        public void OpenFTPClient()
        {
            if (Engine.MyUploadersConfig.FTPAccountList.Count > 0)
            {
                FTPAccount acc = Engine.MyUploadersConfig.FTPAccountList[Engine.MyUploadersConfig.FTPSelectedImage] as FTPAccount;
                if (acc != null)
                {
                    FTPClient2 ftpClient = new FTPClient2(acc) { Icon = this.Icon };
                    ftpClient.Show();
                }
            }
        }

        private void btnFTPOpenClient_Click(object sender, EventArgs e)
        {
            OpenFTPClient();
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

        private void chkHotkeys_CheckedChanged(object sender, EventArgs e)
        {
            if (IsReady)
            {
                if (chkHotkeys.Checked)
                {
                    UpdateHotkeys(false);
                }
                else
                {
                    UnregisterAllHotkeys();
                }
            }
        }

        private void chkTwitterEnable_CheckedChanged(object sender, EventArgs e)
        {
            Engine.conf.TwitterEnabled = chkTwitterEnable.Checked;
        }

        private void btnFtpHelp_Click(object sender, EventArgs e)
        {
            Process.Start("http://code.google.com/p/zscreen/wiki/FTPAccounts");
        }

        private void btnOpenZScreenTester_Click(object sender, EventArgs e)
        {
            new ZScreenTesterGUI.TesterGUI().ShowDialog();
        }

        private void nudMaxNameLength_ValueChanged(object sender, EventArgs e)
        {
            Engine.conf.MaxNameLength = (int)nudMaxNameLength.Value;
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
            if (chkMonUrls.Checked)
            {
                Engine.ClipboardHook();
            }
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
            Engine.conf.PerformActions = chkPerformActions.Checked;
            lbSoftware.Enabled = chkPerformActions.Checked;
        }

        private void cbGIFQuality_SelectedIndexChanged(object sender, EventArgs e)
        {
            Engine.conf.ImageGIFQuality = (GIFQuality)cbGIFQuality.SelectedIndex;
        }

        private void tsmEditinImageSoftware_CheckedChanged(object sender, EventArgs e)
        {
            chkPerformActions.Checked = tsmEditinImageSoftware.Checked;
        }

        private void LbSoftwareItemCheck(object sender, ItemCheckEventArgs e)
        {
            UpdateGuiEditors(sender);
        }

        private void txtDebugLog_LinkClicked(object sender, LinkClickedEventArgs e)
        {
            Process.Start(e.LinkText);
        }

        private void cbCloseButtonAction_SelectedIndexChanged(object sender, EventArgs e)
        {
            Engine.conf.WindowButtonActionClose = (WindowButtonAction)cboCloseButtonAction.SelectedIndex;
        }

        private void cbMinimizeButtonAction_SelectedIndexChanged(object sender, EventArgs e)
        {
            Engine.conf.WindowButtonActionMinimize = (WindowButtonAction)cboMinimizeButtonAction.SelectedIndex;
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

        private void btnResetHotkeys_Click(object sender, EventArgs e)
        {
            UnregisterAllHotkeys();
            ResetHotkeys();
        }

        private void editInPicnikToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // TODO: Edit in Picnik in the new History
            //Process.Start(string.Format("http://www.picnik.com/service/?_import={0}&_apikey={1}",
            //                    HttpUtility.UrlEncode(hi.RemotePath), Engine.PicnikKey));
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

        private void cboProxyConfig_SelectedIndexChanged(object sender, EventArgs e)
        {
            Engine.conf.ProxyConfig = (ProxyConfigType)cboProxyConfig.SelectedIndex;
            if (IsReady)
            {
                Uploader.ProxySettings = Adapter.CheckProxySettings();
            }
        }

        private void historyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenHistory();
        }

        private void tpSourceFileSystem_DragDrop(object sender, DragEventArgs e)
        {
            string[] filePaths = (string[])e.Data.GetData(DataFormats.FileDrop, true);
            UploadUsingFileSystem(filePaths);
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

        private void FileUpload()
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
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

        private void ClipboardUpload()
        {
            if (Engine.conf.ShowClipboardContentViewer)
            {
                using (ClipboardContentViewer ccv = new ClipboardContentViewer())
                {
                    if (ccv.ShowDialog() == DialogResult.OK && !ccv.IsClipboardEmpty)
                    {
                        UploadUsingClipboard();
                    }

                    Engine.conf.ShowClipboardContentViewer = !ccv.DontShowThisWindow;
                }
            }
            else
            {
                UploadUsingClipboard();
            }
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

        private void tpMain_DragDrop(object sender, DragEventArgs e)
        {
            string[] fileDirPaths = (string[])e.Data.GetData(DataFormats.FileDrop, true);
            List<string> files = new List<string>();
            foreach (string fdp in fileDirPaths)
            {
                if (File.Exists(fdp))
                {
                    files.Add(fdp);
                }
                else if (Directory.Exists(fdp))
                {
                    files.AddRange(Directory.GetFiles(fdp, "*.*", SearchOption.AllDirectories));
                }
            }
            UploadUsingFileSystem(files.ToArray());
        }

        private void chkShortenURL_CheckedChanged(object sender, EventArgs e)
        {
            Engine.conf.ShortenUrlAfterUpload = chkShortenURL.Checked;
        }

        private void cboReleaseChannel_SelectedIndexChanged(object sender, EventArgs e)
        {
            Engine.conf.ReleaseChannel = (ReleaseChannelType)cboReleaseChannel.SelectedIndex;
        }

        private void btnClearHistory_Click(object sender, EventArgs e)
        {
            if (File.Exists(Engine.HistoryPath))
            {
                if (MessageBox.Show("Do you really want to delete History?\r\nHistory file path: " + Engine.HistoryPath, "ZScreen - History",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    File.Delete(Engine.HistoryPath);
                }
            }
        }

        private void HideFormTemporary(MethodInvoker method, int executeTime = 500, int showTime = 2000)
        {
            var timer = new System.Windows.Forms.Timer { Interval = executeTime };
            var timer2 = new System.Windows.Forms.Timer { Interval = showTime };

            timer.Tick += (sender, e) =>
            {
                timer.Stop();
                method();
                timer2.Start();
            };

            timer2.Tick += (sender, e) =>
            {
                timer2.Stop();
                NativeMethods.ShowWindow(Handle, (int)NativeMethods.WindowShowStyle.ShowNormalNoActivate);
            };

            Hide();
            timer.Start();
        }

        private void ExecuteTimer(MethodInvoker method, ToolStripItem control, int executeTime = 3000)
        {
            var timer = new System.Windows.Forms.Timer { Interval = executeTime };

            timer.Tick += (sender, e) =>
            {
                timer.Stop();
                method();
                control.Enabled = true;
            };

            control.Enabled = false;
            timer.Start();
        }

        #region Main tab toolbar

        private void tsbFullscreenCapture_Click(object sender, EventArgs e)
        {
            HideFormTemporary(() => CaptureEntireScreen());
        }

        private void tsbActiveWindow_Click(object sender, EventArgs e)
        {
            ExecuteTimer(() => CaptureActiveWindow(), tsbActiveWindow);
        }

        private void tsbSelectedWindow_Click(object sender, EventArgs e)
        {
            HideFormTemporary(() => CaptureSelectedWindow());
        }

        private void tsbCropShot_Click(object sender, EventArgs e)
        {
            HideFormTemporary(() => CaptureRectRegion());
        }

        private void tsbLastCropShot_Click(object sender, EventArgs e)
        {
            HideFormTemporary(() => CaptureRectRegionLast());
        }

        private void tsbFreehandCropShot_Click(object sender, EventArgs e)
        {
            HideFormTemporary(() => CaptureFreeHandRegion());
        }

        private void tsbAutoCapture_Click(object sender, EventArgs e)
        {
            ShowAutoCapture();
        }

        private void tsbFileUpload_Click(object sender, EventArgs e)
        {
            FileUpload();
        }

        private void tsbClipboardUpload_Click(object sender, EventArgs e)
        {
            ClipboardUpload();
        }

        private void tsbDragDropWindow_Click(object sender, EventArgs e)
        {
            ShowDropWindow();
        }

        private void tsbLanguageTranslate_Click(object sender, EventArgs e)
        {
            GetGTGUI().Show();
        }

        private void tsbScreenColorPicker_Click(object sender, EventArgs e)
        {
            ScreenColorPicker();
        }

        private void tsbOpenHistory_Click(object sender, EventArgs e)
        {
            OpenHistory();
        }

        private void tsbImageDirectory_Click(object sender, EventArgs e)
        {
            ShowDirectory(FileSystem.GetImagesDir());
        }

        private void tsbAbout_Click(object sender, EventArgs e)
        {
            FormsMgr.ShowAboutWindow();
        }

        #endregion Main tab toolbar

        private void pbDonate_Click(object sender, EventArgs e)
        {
            Process.Start(ZLinks.URL_DONATE);
        }

        private void tcMain_Selecting(object sender, TabControlCancelEventArgs e)
        {
            if (e.TabPage == tpAdvanced && tcAdvanced.SelectedTab == tpDebugLog)
            {
                rtbDebugLog.Text = Engine.MyLogger.ToString();
            }
        }

        private void tcAdvanced_Selected(object sender, TabControlEventArgs e)
        {
            if (e.TabPage == tpDebugLog)
            {
                rtbDebugLog.Text = Engine.MyLogger.ToString();
            }
        }

        private void pgAppSettings_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            if (IsReady)
            {
                UpdateGuiControlsPaths();

                if (!string.IsNullOrEmpty(Engine.AppConf.UploadersConfigCustomPath))
                {
                    Engine.MyUploadersConfig = UploadersConfig.Load(Engine.AppConf.UploadersConfigCustomPath);
                }
            }
        }

        private void chkShowUploadResults_CheckedChanged(object sender, EventArgs e)
        {
            Engine.conf.ShowUploadResultsWindow = chkShowUploadResults.Checked;
        }

        private void tsbDonate_Click(object sender, EventArgs e)
        {
            Process.Start(ZLinks.URL_DONATE);
        }

        private void tsbDonate_MouseEnter(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Hand;
        }

        private void tsbDonate_MouseLeave(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Arrow;
        }

        private void btnLastCropShotReset_Click(object sender, EventArgs e)
        {
            Engine.conf.LastCapture = Rectangle.Empty;
            Engine.conf.LastRegion = Rectangle.Empty;
        }
    }
}
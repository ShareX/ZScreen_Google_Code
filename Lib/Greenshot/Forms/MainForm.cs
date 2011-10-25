/*
 * Greenshot - a free and open source screenshot tool
 * Copyright (C) 2007-2011  Thomas Braun, Jens Klingen, Robin Krom
 *
 * For more information see: http://getgreenshot.org/
 * The Greenshot project is hosted on Sourceforge: http://sourceforge.net/projects/greenshot/
 *
 * This program is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 1 of the License, or
 * (at your option) any later version.
 *
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 *
 * You should have received a copy of the GNU General Public License
 * along with this program.  If not, see <http://www.gnu.org/licenses/>.
 */

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Printing;
using System.IO;
using System.Reflection;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Windows.Forms;

using Greenshot.Configuration;
using Greenshot.Drawing;
using Greenshot.Experimental;
using Greenshot.Forms;
using Greenshot.Help;
using Greenshot.Helpers;
using Greenshot.Plugin;
using Greenshot.UnmanagedHelpers;
using GreenshotPlugin.Controls;
using GreenshotPlugin.Core;
using IniFile;

namespace Greenshot
{
    /// <summary>
    /// Description of MainForm.
    /// </summary>
    public partial class MainForm : Form
    {
        private const string LOG4NET_FILE = "log4net.xml";
        private static log4net.ILog LOG = null;
        private static Mutex applicationMutex = null;
        private static CoreConfiguration conf;

        public static void InitializeLog4NET()
        {
            // Setup log4j, currently the file is called log4net.xml
            string pafLog4NetFilename = Path.Combine(Application.StartupPath, @"App\Greenshot\" + LOG4NET_FILE);
            string log4netFilename = Path.Combine(Application.StartupPath, LOG4NET_FILE);
            if (File.Exists(log4netFilename))
            {
                log4net.Config.XmlConfigurator.Configure(new FileInfo(log4netFilename));
            }
            else if (File.Exists(pafLog4NetFilename))
            {
                log4net.Config.XmlConfigurator.Configure(new FileInfo(pafLog4NetFilename));
            }
            else
            {
                MessageBox.Show("Can't find file " + LOG4NET_FILE);
            }

            // Setup the LOG
            LOG = log4net.LogManager.GetLogger(typeof(MainForm));
        }

        /// <summary>
        /// Send DataTransport Object via Window-messages
        /// </summary>
        /// <param name="dataTransport">DataTransport with data for a running instance</param>
        private static void SendData(CopyDataTransport dataTransport)
        {
            string appName = Application.ProductName;
            CopyData copyData = new CopyData();
            copyData.Channels.Add(appName);
            copyData.Channels[appName].Send(dataTransport);
        }

        private static void FreeMutex()
        {
            // Remove the application mutex
            if (applicationMutex != null)
            {
                try
                {
                    applicationMutex.ReleaseMutex();
                    applicationMutex = null;
                }
                catch (Exception ex)
                {
                    LOG.Error("Error releasing Mutex!", ex);
                }
            }
        }

        public static MainForm instance = null;

        private ILanguage lang;
        private ToolTip tooltip;
        private CaptureForm captureForm = null;

        // Thumbnail preview
        private FormWithoutActivation thumbnailForm = null;
        private IntPtr thumbnailHandle = IntPtr.Zero;
        private Rectangle parentMenuBounds = Rectangle.Empty;
        private int resizeFactor = 4;

        public MainForm(CopyDataTransport dataTransport)
        {
            instance = this;
            conf = IniConfig.GetIniSection<CoreConfiguration>();
            //
            // The InitializeComponent() call is required for Windows Forms designer support.
            //
            this.Hide();
            InitializeComponent();
            lang = Language.GetInstance();
            IniConfig.IniChanged += new FileSystemEventHandler(ReloadConfiguration);

            tooltip = new ToolTip();

            UpdateUI();
            InitializeQuickSettingsMenu();

            // captureForm = new CaptureForm();
        }

        /// <summary>
        /// DataReceivedEventHandler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="dataReceivedEventArgs"></param>
        private void CopyDataDataReceived(object sender, CopyDataReceivedEventArgs copyDataReceivedEventArgs)
        {
            // Cast the data to the type of object we sent:
            CopyDataTransport dataTransport = (CopyDataTransport)copyDataReceivedEventArgs.Data;
            HandleDataTransport(dataTransport);
        }

        private void HandleDataTransport(CopyDataTransport dataTransport)
        {
            foreach (KeyValuePair<CommandEnum, string> command in dataTransport.Commands)
            {
                LOG.Debug("Data received, Command = " + command.Key + ", Data: " + command.Value);
                switch (command.Key)
                {
                    case CommandEnum.Exit:
                        exit();
                        break;
                    case CommandEnum.FirstLaunch:
                        LOG.Info("FirstLaunch: Created new configuration.");
                        break;
                    case CommandEnum.ReloadConfig:
                        IniConfig.Reload();
                        ReloadConfiguration(null, null);
                        break;
                    case CommandEnum.OpenFile:
                        string filename = command.Value;
                        if (File.Exists(filename))
                        {
                            captureForm.MakeCapture(filename);
                        }
                        else
                        {
                            LOG.Warn("No such file: " + filename);
                        }
                        break;
                    default:
                        LOG.Error("Unknown command!");
                        break;
                }
            }
        }

        private void ReloadConfiguration(object source, FileSystemEventArgs e)
        {
            lang.SetLanguage(conf.Language);
            this.Invoke((MethodInvoker)delegate
            {
                // Even update language when needed
                UpdateUI();
                // Update the hotkey
                // Make sure the current hotkeys are disabled
                HotkeyControl.UnregisterHotkeys();
            });
        }

        public ContextMenuStrip MainMenu
        {
            get { return contextMenu; }
        }

        #region hotkeys

        protected override void WndProc(ref Message m)
        {
            if (ClipboardHelper.HandleClipboardMessages(ref m))
            {
                return;
            }
            if (HotkeyControl.HandleMessages(ref m))
            {
                return;
            }
            base.WndProc(ref m);
        }

        #endregion hotkeys

        public void UpdateUI()
        {
            this.Text = lang.GetString(LangKey.application_title);
            this.contextmenu_settings.Text = lang.GetString(LangKey.contextmenu_settings);
            this.contextmenu_capturearea.Text = lang.GetString(LangKey.contextmenu_capturearea);
            this.contextmenu_capturelastregion.Text = lang.GetString(LangKey.contextmenu_capturelastregion);
            this.contextmenu_capturewindow.Text = lang.GetString(LangKey.contextmenu_capturewindow);
            this.contextmenu_capturefullscreen.Text = lang.GetString(LangKey.contextmenu_capturefullscreen);
            this.contextmenu_captureclipboard.Text = lang.GetString(LangKey.contextmenu_captureclipboard);
            this.contextmenu_openfile.Text = lang.GetString(LangKey.contextmenu_openfile);
            this.contextmenu_quicksettings.Text = lang.GetString(LangKey.contextmenu_quicksettings);
            this.contextmenu_help.Text = lang.GetString(LangKey.contextmenu_help);
            this.contextmenu_about.Text = lang.GetString(LangKey.contextmenu_about);
            this.contextmenu_donate.Text = lang.GetString(LangKey.contextmenu_donate);
            this.contextmenu_exit.Text = lang.GetString(LangKey.contextmenu_exit);
            this.contextmenu_captureie.Text = lang.GetString(LangKey.contextmenu_captureie);
            this.contextmenu_openrecentcapture.Text = lang.GetString(LangKey.contextmenu_openrecentcapture);
        }

        #region mainform events

        private void MainFormFormClosing(object sender, FormClosingEventArgs e)
        {
            instance = null;
            exit();
        }

        private void MainFormActivated(object sender, EventArgs e)
        {
            Hide();
            ShowInTaskbar = false;
        }

        #endregion mainform events

        #region key handlers

        private void CaptureRegion()
        {
            captureForm.MakeCapture(CaptureMode.Region, true);
        }

        private void CaptureClipboard()
        {
            captureForm.MakeCapture(CaptureMode.Clipboard, false);
        }

        private void CaptureFile()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.png, *.jpg, *.gif, *.bmp, *.ico)|*.png; *.jpg; *.jpeg; *.gif; *.bmp; *.ico";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                if (File.Exists(openFileDialog.FileName))
                {
                    captureForm.MakeCapture(openFileDialog.FileName);
                }
            }
        }

        private void CaptureFullScreen()
        {
            captureForm.MakeCapture(CaptureMode.FullScreen, true);
        }

        private void CaptureLastRegion()
        {
            captureForm.MakeCapture(CaptureMode.LastRegion, true);
        }

        private void CaptureIE()
        {
            captureForm.MakeCapture(CaptureMode.IE, true);
        }

        private void CaptureWindow()
        {
            CaptureMode captureMode = CaptureMode.None;
            if (conf.CaptureWindowsInteractive)
            {
                captureMode = CaptureMode.Window;
            }
            else
            {
                captureMode = CaptureMode.ActiveWindow;
            }
            captureForm.MakeCapture(captureMode, true);
        }

        #endregion key handlers

        #region contextmenu

        private void ContextMenuOpening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (Clipboard.ContainsImage())
            {
                contextmenu_captureclipboard.Enabled = true;
            }
            else
            {
                contextmenu_captureclipboard.Enabled = false;
            }
            contextmenu_capturelastregion.Enabled = RuntimeConfig.LastCapturedRegion != Rectangle.Empty;

            // IE context menu code
            if (conf.IECapture && IECaptureHelper.IsIERunning())
            {
                this.contextmenu_captureie.Enabled = true;
            }
            else
            {
                this.contextmenu_captureie.Enabled = false;
            }
        }

        private void ContextMenuClosing(object sender, EventArgs e)
        {
            this.contextmenu_captureie.DropDownItems.Clear();
            this.contextmenu_capturewindow.DropDownItems.Clear();
            cleanupThumbnail();
        }

        /// <summary>
        /// Build a selectable list of IE tabs when we enter the menu item
        /// </summary>
        private void EnterCaptureIEMenuItem(object sender, EventArgs e)
        {
            List<KeyValuePair<WindowDetails, string>> tabs = IECaptureHelper.GetTabList();
            this.contextmenu_captureie.DropDownItems.Clear();
            if (tabs.Count > 0)
            {
                this.contextmenu_captureie.Enabled = true;
                Dictionary<WindowDetails, int> counter = new Dictionary<WindowDetails, int>();

                foreach (KeyValuePair<WindowDetails, string> tabData in tabs)
                {
                    ToolStripMenuItem captureIETabItem = new ToolStripMenuItem(tabData.Value);
                    int index;
                    if (counter.ContainsKey(tabData.Key))
                    {
                        index = counter[tabData.Key];
                    }
                    else
                    {
                        index = 0;
                    }
                    captureIETabItem.Tag = new KeyValuePair<WindowDetails, int>(tabData.Key, index++);
                    captureIETabItem.Click += new System.EventHandler(Contextmenu_captureIE_Click);
                    this.contextmenu_captureie.DropDownItems.Add(captureIETabItem);
                    if (counter.ContainsKey(tabData.Key))
                    {
                        counter[tabData.Key] = index;
                    }
                    else
                    {
                        counter.Add(tabData.Key, index);
                    }
                }
            }
            else
            {
                this.contextmenu_captureie.Enabled = false;
            }
        }

        /// <summary>
        /// Build a selectable list of windows when we enter the menu item
        /// </summary>
        private void EnterCaptureWindowMenuItem(object sender, EventArgs e)
        {
            // The Capture window context menu item used to go to the following code:
            // captureForm.MakeCapture(CaptureMode.Window, false);
            // Now we check which windows are there to capture
            ToolStripMenuItem captureWindowMenuItem = (ToolStripMenuItem)sender;
            AddCaptureWindowMenuItems(captureWindowMenuItem, Contextmenu_window_Click);
        }

        private void LeaveCaptureWindowMenuItem(object sender, EventArgs e)
        {
            cleanupThumbnail();
        }

        private void ShowThumbnailOnEnter(object sender, EventArgs e)
        {
            ToolStripMenuItem captureWindowItem = sender as ToolStripMenuItem;
            WindowDetails window = captureWindowItem.Tag as WindowDetails;
            parentMenuBounds = captureWindowItem.GetCurrentParent().TopLevelControl.Bounds;
            if (thumbnailForm == null)
            {
                thumbnailForm = new FormWithoutActivation();
                thumbnailForm.ShowInTaskbar = false;
                thumbnailForm.FormBorderStyle = FormBorderStyle.None;
                thumbnailForm.TopMost = false;
                thumbnailForm.Enabled = false;
                if (conf.WindowCaptureMode == WindowCaptureMode.Auto || conf.WindowCaptureMode == WindowCaptureMode.Aero)
                {
                    thumbnailForm.BackColor = Color.FromArgb(255, conf.DWMBackgroundColor.R, conf.DWMBackgroundColor.G, conf.DWMBackgroundColor.B);
                }
                else
                {
                    thumbnailForm.BackColor = Color.White;
                }
            }
            if (thumbnailHandle != IntPtr.Zero)
            {
                DWM.DwmUnregisterThumbnail(thumbnailHandle);
                thumbnailHandle = IntPtr.Zero;
            }
            DWM.DwmRegisterThumbnail(thumbnailForm.Handle, window.Handle, out thumbnailHandle);
            if (thumbnailHandle != IntPtr.Zero)
            {
                Rectangle windowRectangle = window.ClientRectangle;
                int thumbnailWidth = (windowRectangle.Width) / resizeFactor;
                int thumbnailHeight = (windowRectangle.Height) / resizeFactor;
                thumbnailForm.Width = thumbnailWidth;
                thumbnailForm.Height = thumbnailHeight;
                // Prepare the displaying of the Thumbnail
                DWM_THUMBNAIL_PROPERTIES props = new DWM_THUMBNAIL_PROPERTIES();
                props.Opacity = (byte)255;
                props.Visible = true;
                props.SourceClientAreaOnly = false;
                props.Destination = new RECT(0, 0, thumbnailWidth, thumbnailHeight);
                DWM.DwmUpdateThumbnailProperties(thumbnailHandle, ref props);
                if (!thumbnailForm.Visible)
                {
                    thumbnailForm.Show();
                }
                // Make sure it's on "top"!
                User32.SetWindowPos(thumbnailForm.Handle, captureWindowItem.GetCurrentParent().TopLevelControl.Handle, 0, 0, 0, 0, WindowPos.SWP_NOMOVE | WindowPos.SWP_NOSIZE | WindowPos.SWP_NOACTIVATE);

                // Align to menu
                Rectangle screenBounds = WindowCapture.GetScreenBounds();
                if (screenBounds.Contains(parentMenuBounds.Left, parentMenuBounds.Top - thumbnailHeight))
                {
                    thumbnailForm.Location = new Point(parentMenuBounds.Left + (parentMenuBounds.Width / 2) - (thumbnailWidth / 2), parentMenuBounds.Top - thumbnailHeight);
                }
                else
                {
                    thumbnailForm.Location = new Point(parentMenuBounds.Left + (parentMenuBounds.Width / 2) - (thumbnailWidth / 2), parentMenuBounds.Bottom);
                }
            }
        }

        private void HideThumbnailOnLeave(object sender, EventArgs e)
        {
            hideThumbnail();
        }

        private void hideThumbnail()
        {
            if (thumbnailHandle != IntPtr.Zero)
            {
                DWM.DwmUnregisterThumbnail(thumbnailHandle);
                thumbnailHandle = IntPtr.Zero;
                thumbnailForm.Hide();
            }
        }

        private void cleanupThumbnail()
        {
            hideThumbnail();

            if (thumbnailForm != null)
            {
                thumbnailForm.Close();
                thumbnailForm = null;
            }
        }

        public void AddCaptureWindowMenuItems(ToolStripMenuItem menuItem, EventHandler eventHandler)
        {
            ILanguage lang = Language.GetInstance();
            menuItem.DropDownItems.Clear();
            // check if thumbnailPreview is enabled and DWM is enabled
            bool thumbnailPreview = conf.ThumnailPreview && DWM.isDWMEnabled();

            List<WindowDetails> windows = WindowDetails.GetVisibleWindows();
            foreach (WindowDetails window in windows)
            {
                ToolStripMenuItem captureWindowItem = new ToolStripMenuItem(window.Text);
                captureWindowItem.Tag = window;
                captureWindowItem.Click += new System.EventHandler(eventHandler);
                // Only show preview when enabled
                if (thumbnailPreview)
                {
                    captureWindowItem.MouseEnter += new System.EventHandler(ShowThumbnailOnEnter);
                    captureWindowItem.MouseLeave += new System.EventHandler(HideThumbnailOnLeave);
                }
                menuItem.DropDownItems.Add(captureWindowItem);
            }
        }

        private void CaptureAreaToolStripMenuItemClick(object sender, EventArgs e)
        {
            captureForm.MakeCapture(CaptureMode.Region, false);
        }

        private void CaptureClipboardToolStripMenuItemClick(object sender, EventArgs e)
        {
            CaptureClipboard();
        }

        private void OpenFileToolStripMenuItemClick(object sender, EventArgs e)
        {
            CaptureFile();
        }

        private void CaptureFullScreenToolStripMenuItemClick(object sender, EventArgs e)
        {
            captureForm.MakeCapture(CaptureMode.FullScreen, false);
        }

        private void Contextmenu_capturelastregionClick(object sender, EventArgs e)
        {
            captureForm.MakeCapture(CaptureMode.LastRegion, false);
        }

        private void Contextmenu_window_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem clickedItem = (ToolStripMenuItem)sender;
            try
            {
                WindowDetails windowToCapture = (WindowDetails)clickedItem.Tag;
                captureForm.MakeCapture(windowToCapture);
            }
            catch (Exception exception)
            {
                LOG.Error(exception);
            }
        }

        private void Contextmenu_captureIE_Click(object sender, EventArgs e)
        {
            if (!conf.IECapture)
            {
                LOG.InfoFormat("IE Capture is disabled.");
                return;
            }
            ToolStripMenuItem clickedItem = (ToolStripMenuItem)sender;
            KeyValuePair<WindowDetails, int> tabData = (KeyValuePair<WindowDetails, int>)clickedItem.Tag;
            try
            {
                IECaptureHelper.ActivateIETab(tabData.Key, tabData.Value);
            }
            catch (Exception exception)
            {
                LOG.Error(exception);
            }
            try
            {
                captureForm.MakeCapture(CaptureMode.IE, false);
            }
            catch (Exception exception)
            {
                LOG.Error(exception);
            }
        }

        private void Contextmenu_donateClick(object sender, EventArgs e)
        {
            Process.Start("http://getgreenshot.org/support/");
        }

        private void Contextmenu_settingsClick(object sender, EventArgs e)
        {
            SettingsForm settings = new SettingsForm();
            settings.ShowDialog();
            InitializeQuickSettingsMenu();
            this.Hide();
        }

        private void Contextmenu_aboutClick(object sender, EventArgs e)
        {
            new AboutForm().Show();
        }

        private void Contextmenu_helpClick(object sender, EventArgs e)
        {
            HelpBrowserForm hpf = new HelpBrowserForm(conf.Language);
            hpf.Show();
        }

        private void Contextmenu_exitClick(object sender, EventArgs e)
        {
            exit();
        }

        private void InitializeQuickSettingsMenu()
        {
            this.contextmenu_quicksettings.DropDownItems.Clear();
            // screenshot destination
            ToolStripMenuSelectList selectList = new ToolStripMenuSelectList("destination", true);
            selectList.Text = lang.GetString(LangKey.settings_destination);
            selectList.AddItem(lang.GetString(LangKey.settings_destination_editor), Destination.Editor, conf.OutputDestinations.Contains(Destination.Editor));
            selectList.AddItem(lang.GetString(LangKey.settings_destination_clipboard), Destination.Clipboard, conf.OutputDestinations.Contains(Destination.Clipboard));
            selectList.AddItem(lang.GetString(LangKey.quicksettings_destination_file), Destination.FileDefault, conf.OutputDestinations.Contains(Destination.FileDefault));
            selectList.AddItem(lang.GetString(LangKey.settings_destination_fileas), Destination.FileWithDialog, conf.OutputDestinations.Contains(Destination.FileWithDialog));
            selectList.AddItem(lang.GetString(LangKey.settings_destination_printer), Destination.Printer, conf.OutputDestinations.Contains(Destination.Printer));
            selectList.AddItem(lang.GetString(LangKey.settings_destination_email), Destination.EMail, conf.OutputDestinations.Contains(Destination.EMail));
            selectList.CheckedChanged += new EventHandler(this.QuickSettingItemChanged);
            this.contextmenu_quicksettings.DropDownItems.Add(selectList);
            // print options
            selectList = new ToolStripMenuSelectList("printoptions", true);
            selectList.Text = lang.GetString(LangKey.settings_printoptions);
            selectList.AddItem(lang.GetString(LangKey.printoptions_allowshrink), "AllowPrintShrink", conf.OutputPrintAllowShrink);
            selectList.AddItem(lang.GetString(LangKey.printoptions_allowenlarge), "AllowPrintEnlarge", conf.OutputPrintAllowEnlarge);
            selectList.AddItem(lang.GetString(LangKey.printoptions_allowrotate), "AllowPrintRotate", conf.OutputPrintAllowRotate);
            selectList.AddItem(lang.GetString(LangKey.printoptions_allowcenter), "AllowPrintCenter", conf.OutputPrintCenter);
            selectList.CheckedChanged += new EventHandler(this.QuickSettingItemChanged);
            this.contextmenu_quicksettings.DropDownItems.Add(selectList);
            // effects
            selectList = new ToolStripMenuSelectList("effects", true);
            selectList.Text = lang.GetString(LangKey.settings_visualization);
            selectList.AddItem(lang.GetString(LangKey.settings_playsound), "PlaySound", conf.PlayCameraSound);
            selectList.CheckedChanged += new EventHandler(this.QuickSettingItemChanged);
            this.contextmenu_quicksettings.DropDownItems.Add(selectList);
        }

        private void QuickSettingItemChanged(object sender, EventArgs e)
        {
            ToolStripMenuSelectList selectList = (ToolStripMenuSelectList)sender;
            ToolStripMenuSelectListItem item = ((ItemCheckedChangedEventArgs)e).Item;
            if (selectList.Identifier.Equals("destination"))
            {
                Destination selectedDestination = (Destination)item.Data;
                if (item.Checked && !conf.OutputDestinations.Contains(selectedDestination))
                {
                    conf.OutputDestinations.Add(selectedDestination);
                }
                if (!item.Checked && conf.OutputDestinations.Contains(selectedDestination))
                {
                    conf.OutputDestinations.Remove(selectedDestination);
                }
                IniConfig.Save();
            }
            else if (selectList.Identifier.Equals("printoptions"))
            {
                if (item.Data.Equals("AllowPrintShrink")) conf.OutputPrintAllowShrink = item.Checked;
                else if (item.Data.Equals("AllowPrintEnlarge")) conf.OutputPrintAllowEnlarge = item.Checked;
                else if (item.Data.Equals("AllowPrintRotate")) conf.OutputPrintAllowRotate = item.Checked;
                else if (item.Data.Equals("AllowPrintCenter")) conf.OutputPrintCenter = item.Checked;
                IniConfig.Save();
            }
            else if (selectList.Identifier.Equals("effects"))
            {
                if (item.Data.Equals("PlaySound"))
                {
                    conf.PlayCameraSound = item.Checked;
                }
                IniConfig.Save();
            }
        }

        #endregion contextmenu

        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            string exceptionText = EnvironmentInfo.BuildReport(e.ExceptionObject as Exception);
            LOG.Error(exceptionText);
            new BugReportForm(exceptionText).ShowDialog();
        }

        private static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            string exceptionText = EnvironmentInfo.BuildReport(e.Exception);
            LOG.Error(exceptionText);
            new BugReportForm(exceptionText).ShowDialog();
        }

        private void NotifyIconClick(object sender, EventArgs eventArgs)
        {
            MethodInfo oMethodInfo = typeof(NotifyIcon).GetMethod("ShowContextMenu", BindingFlags.Instance | BindingFlags.NonPublic);
            oMethodInfo.Invoke(notifyIcon, null);
        }

        /// <summary>
        /// The Contextmenu_OpenRecent currently opens the last know save location
        /// </summary>
        private void Contextmenu_OpenRecent(object sender, EventArgs eventArgs)
        {
            string path;
            string configPath = FilenameHelper.FillVariables(conf.OutputFilePath, false);
            string lastFilePath = Path.GetDirectoryName(conf.OutputFileAsFullpath);
            if (Directory.Exists(lastFilePath))
            {
                path = lastFilePath;
            }
            else if (Directory.Exists(configPath))
            {
                path = configPath;
            }
            else
            {
                // What do I open when nothing can be found? Right, nothing...
                return;
            }
            LOG.Debug("DoubleClick was called! Starting: " + path);
            try
            {
                System.Diagnostics.Process.Start(path);
            }
            catch (Exception e)
            {
                // Make sure we show what we tried to open in the exception
                e.Data.Add("path", path);
                throw e;
            }
        }

        /// <summary>
        /// Shutdown / cleanup
        /// </summary>
        public void exit()
        {
            // Close all open forms (except this), use a separate List to make sure we don't get a "InvalidOperationException: Collection was modified"
            List<Form> formsToClose = new List<Form>();
            foreach (Form form in Application.OpenForms)
            {
                if (form.Handle != this.Handle && !form.GetType().Equals(typeof(Greenshot.ImageEditorForm)))
                {
                    formsToClose.Add(form);
                }
            }
            foreach (Form form in formsToClose)
            {
                try
                {
                    LOG.InfoFormat("Closing form: {0}", form.Name);
                    this.Invoke((MethodInvoker)delegate { form.Close(); });
                }
                catch (Exception e)
                {
                    LOG.Error("Error closing form!", e);
                }
            }

            // Make sure hotkeys are disabled
            try
            {
                HotkeyControl.UnregisterHotkeys();
            }
            catch (Exception e)
            {
                LOG.Error("Error unregistering hotkeys!", e);
            }

            // Now the sound isn't needed anymore
            try
            {
                SoundHelper.Deinitialize();
            }
            catch (Exception e)
            {
                LOG.Error("Error deinitializing sound!", e);
            }

            // Inform all registed plugins
            try
            {
                PluginHelper.instance.Shutdown();
            }
            catch (Exception e)
            {
                LOG.Error("Error shutting down plugins!", e);
            }

            // Gracefull shutdown
            try
            {
                Application.DoEvents();
                Application.Exit();
            }
            catch (Exception e)
            {
                LOG.Error("Error closing application!", e);
            }

            // Store any open configuration changes
            try
            {
                IniConfig.Save();
            }
            catch (Exception e)
            {
                LOG.Error("Error storing configuration!", e);
            }

            // Remove the application mutex
            FreeMutex();

            // make the icon invisible otherwise it stays even after exit!!
            if (notifyIcon != null)
            {
                notifyIcon.Visible = false;
                notifyIcon.Dispose();
                notifyIcon = null;
            }
        }
    }
}
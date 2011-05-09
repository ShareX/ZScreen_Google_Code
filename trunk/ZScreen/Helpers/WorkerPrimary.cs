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
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Media;
using System.Threading;
using System.Windows.Forms;
using HelpersLib;
using HistoryLib;
using Microsoft.WindowsAPICodePack.Taskbar;
using MS.WindowsAPICodePack.Internal;
using UploadersLib;
using UploadersLib.HelperClasses;
using UploadersLib.TextServices;
using ZScreenGUI.Properties;
using ZScreenLib;
using ZSS.ColorsLib;
using ZUploader.HelperClasses;

namespace ZScreenGUI
{
    public class WorkerPrimary : Worker
    {
        private ZScreen mZScreen;
        internal bool mSetHotkeys, bAutoScreenshotsOpened, bDropWindowOpened, bQuickActionsOpened, bQuickOptionsOpened;
        internal HotkeyMgr mHotkeyMgr = null;

        public WorkerPrimary(ZScreen myZScreen)
        {
            this.mZScreen = myZScreen;
        }

        public override BackgroundWorker CreateWorker()
        {
            BackgroundWorker bwApp = new BackgroundWorker { WorkerReportsProgress = true };
            bwApp.DoWork += new System.ComponentModel.DoWorkEventHandler(BwApp_DoWork);
            bwApp.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(BwApp_ProgressChanged);
            bwApp.RunWorkerCompleted += new RunWorkerCompletedEventHandler(BwApp_RunWorkerCompleted);
            return bwApp;
        }

        #region Worker Events

        public override void BwApp_DoWork(object sender, DoWorkEventArgs e)
        {
            WorkerTask task = (WorkerTask)e.Argument;
            task.MyWorker.ReportProgress((int)WorkerTask.ProgressType.SET_ICON_BUSY, task);
            task.UniqueNumber = ClipboardManager.Queue();

            if (Engine.conf.PromptForUpload && task.MyImageUploader != ImageUploaderType.CLIPBOARD &
                task.MyImageUploader != ImageUploaderType.FILE &&
                (task.Job2 == WorkerTask.JobLevel2.TAKE_SCREENSHOT_SCREEN ||
                task.Job2 == WorkerTask.JobLevel2.TAKE_SCREENSHOT_WINDOW_ACTIVE) &&
                MessageBox.Show("Do you really want to upload to " + task.MyImageUploader.GetDescription() + "?",
                Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
            {
                e.Result = task;
                return;
            }

            if (task.Job1 == JobLevel1.Image)
            {
                if (Engine.conf.ScreenshotDelayTime != 0)
                {
                    Thread.Sleep((int)Engine.conf.ScreenshotDelayTime);
                }
            }

            FileSystem.AppendDebug(string.Format("Job started: {0}", task.Job2));

            switch (task.Job1)
            {
                case JobLevel1.Image:
                case JobLevel1.File:
                    switch (task.Job2)
                    {
                        case WorkerTask.JobLevel2.TAKE_SCREENSHOT_SCREEN:
                            new TaskManager(task).CaptureScreen();
                            break;
                        case WorkerTask.JobLevel2.TakeScreenshotWindowSelected:
                        case WorkerTask.JobLevel2.TakeScreenshotCropped:
                        case WorkerTask.JobLevel2.TAKE_SCREENSHOT_LAST_CROPPED:
                            new TaskManager(task).CaptureRegionOrWindow();
                            break;
                        case WorkerTask.JobLevel2.CustomUploaderTest:
                        case WorkerTask.JobLevel2.TAKE_SCREENSHOT_WINDOW_ACTIVE:
                            new TaskManager(task).CaptureActiveWindow();
                            break;
                        case WorkerTask.JobLevel2.FREEHAND_CROP_SHOT:
                            new TaskManager(task).CaptureFreehandCrop();
                            break;
                        case WorkerTask.JobLevel2.UPLOAD_IMAGE:
                        case WorkerTask.JobLevel2.UploadFromClipboard:
                        case WorkerTask.JobLevel2.PROCESS_DRAG_N_DROP:
                            new TaskManager(task).PublishData();
                            break;
                    }

                    break;
                case JobLevel1.Text:
                    switch (task.Job2)
                    {
                        case WorkerTask.JobLevel2.UploadFromClipboard:
                            PublishText(task);
                            break;
                        case WorkerTask.JobLevel2.LANGUAGE_TRANSLATOR:
                            LanguageTranslator(task);
                            break;
                    }
                    break;
            }

            if (task.LinkManager != null && task.MyImageUploader != ImageUploaderType.FILE  && task.ShouldShortenURL(task.RemoteFilePath))
            {
                task.ShortenURL(task.RemoteFilePath);
            }

            e.Result = task;
        }

        private void BwApp_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (mZScreen == null)
            {
                return;
            }

            switch ((WorkerTask.ProgressType)e.ProgressPercentage)
            {
                case (WorkerTask.ProgressType)101:
                    Adapter.PrintImage(e.UserState as Image);
                    break;
                case (WorkerTask.ProgressType)102:
                    Adapter.CopyImageToClipboard(e.UserState as Image);
                    break;
                case (WorkerTask.ProgressType)103:
                    Adapter.SaveImage(e.UserState as Image);
                    break;
                case (WorkerTask.ProgressType)104:
                    Adapter.CopyDataToClipboard(e.UserState);
                    break;
                case WorkerTask.ProgressType.COPY_TO_CLIPBOARD_IMAGE:
                    if (e.UserState.GetType() == typeof(string))
                    {
                        Adapter.CopyImageToClipboard(e.UserState as string);
                    }
                    else if (e.UserState is Image)
                    {
                        Adapter.CopyImageToClipboard((Image)e.UserState);
                    }

                    break;
                case WorkerTask.ProgressType.FLASH_ICON:
                    Adapter.FlashNotifyIcon(mZScreen.niTray, e.UserState as Icon);
                    break;
                case WorkerTask.ProgressType.SET_ICON_BUSY:
                    Adapter.SetNotifyIconStatus(e.UserState as WorkerTask, mZScreen.niTray, Resources.zss_busy);
                    break;
                case WorkerTask.ProgressType.UpdateCropMode:
                    mZScreen.cboCropGridMode.Checked = Engine.conf.CropGridToggle;
                    break;
                case WorkerTask.ProgressType.CHANGE_UPLOAD_DESTINATION:
                    mZScreen.ucDestOptions.cboImageUploaders.SelectedIndex = (int)Engine.conf.MyImageUploader;
                    Adapter.SetNotifyIconBalloonTip(mZScreen.niTray, mZScreen.Text, string.Format("Images Destination was updated to {0}", ((ImageUploaderType)Engine.conf.MyImageUploader).GetDescription()), ToolTipIcon.Warning);
                    break;
                case WorkerTask.ProgressType.CHANGE_TRAY_ICON_PROGRESS:
                    int progress = (int)((ProgressManager)e.UserState).Percentage;
                    Adapter.UpdateNotifyIconProgress(mZScreen.niTray, progress);
                    Adapter.TaskbarSetProgressValue(mZScreen, progress);
                    mZScreen.Text = string.Format("{0}% - {1}", ClipboardManager.GetAverageProgress(), Engine.GetProductName());
                    break;
                case WorkerTask.ProgressType.UPDATE_PROGRESS_MAX:
                    TaskbarProgressBarState tbps = (TaskbarProgressBarState)e.UserState;
                    Adapter.TaskbarSetProgressState(mZScreen, tbps);
                    break;
                case WorkerTask.ProgressType.ShowTrayWarning:
                    Adapter.TaskbarSetProgressValue(mZScreen, 33);
                    Adapter.TaskbarSetProgressState(mZScreen, TaskbarProgressBarState.Error);
                    Adapter.SetNotifyIconBalloonTip(mZScreen.niTray, mZScreen.Text, e.UserState as string, ToolTipIcon.Warning);
                    break;
            }
        }

        private void BwApp_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            WorkerTask task = (WorkerTask)e.Result;
            mZScreen.Text = Engine.GetProductName();

            try
            {
                if (task.Errors.Count > 0 && task.Errors[0].Contains(ExceptionMessage.ProxyAuthenticationRequired))
                {
                    ProxyConfig pc = new ProxyConfig();
                    if (pc.ShowDialog() == DialogResult.OK)
                    {
                        mZScreen.ProxyAdd(pc.Proxy);
                        mZScreen.cboProxyConfig.SelectedIndex = (int)ProxyConfigType.ManualProxy;
                        Uploader.ProxySettings = Adapter.CheckProxySettings();
                    }
                    RetryTask(task);
                }

                WorkerTask checkTask = RetryUpload(task);

                if (checkTask.RetryPending)
                {
                    string message = string.Format("{0}\r\n\r\nAutomatically starting upload with {1}.", string.Join("\r\n", task.Errors.ToArray()), checkTask.MyImageUploader.GetDescription());
                    mZScreen.niTray.ShowBalloonTip(5000, Application.ProductName, message, ToolTipIcon.Warning);
                }

                else
                {
                    FileSystem.AppendDebug(string.Format("Job completed: {0}", task.Job2));

                    if (task.MyImageUploader == ImageUploaderType.FILE && Engine.conf.ShowSaveFileDialogImages)
                    {
                        string fp = Adapter.SaveImage(task.MyImage);
                        if (!string.IsNullOrEmpty(fp))
                        {
                            task.UpdateLocalFilePath(fp);
                        }
                    }

                    switch (task.Job1)
                    {
                        case JobLevel1.Text:
                            if (task.Job2 == WorkerTask.JobLevel2.LANGUAGE_TRANSLATOR && mZScreen != null)
                            {
                                FillGoogleTranslateInfo(task.TranslationInfo);

                                mZScreen.btnTranslate.Enabled = true;
                                mZScreen.btnTranslateTo1.Enabled = true;
                            }
                            break;
                        case JobLevel1.Image:
                            if (task.Job2 == WorkerTask.JobLevel2.CustomUploaderTest && task.LinkManager != null && !string.IsNullOrEmpty(task.LinkManager.UploadResult.URL))
                            {
                                if (!string.IsNullOrEmpty(task.LinkManager.GetFullImageUrl()))
                                {
                                    this.mZScreen.txtUploadersLog.AppendText(task.DestinationName + " full image: " +
                                                task.LinkManager.GetFullImageUrl() + "\r\n");
                                }

                                if (!string.IsNullOrEmpty(task.LinkManager.GetThumbnailUrl()))
                                {
                                    this.mZScreen.txtUploadersLog.AppendText(task.DestinationName + " thumbnail: " +
                                                task.LinkManager.GetThumbnailUrl() + "\r\n");
                                }
                            }

                            if (task.MyImageUploader != ImageUploaderType.FILE && Engine.conf.DeleteLocal && File.Exists(task.LocalFilePath))
                            {
                                try
                                {
                                    File.Delete(task.LocalFilePath);
                                }
                                catch (Exception ex) // TODO: sometimes file is still locked... delete those files sometime
                                {
                                    FileSystem.AppendDebug("Error while finalizing job", ex);
                                }
                            }
                            break;
                    }

                    if (Engine.conf.CopyClipboardAfterTask)
                    {
                        ClipboardManager.SetClipboard(task, false);
                    }

                    if (Engine.conf.TwitterEnabled)
                    {
                        Adapter.TwitterMsg(task);
                    }

                    if (task.LinkManager != null && !string.IsNullOrEmpty(task.LinkManager.UploadResult.Source))
                    {
                        mZScreen.btnOpenSourceText.Enabled = true;
                        mZScreen.btnOpenSourceBrowser.Enabled = true;
                        mZScreen.btnOpenSourceString.Enabled = true;
                    }

                    if (ClipboardManager.UploadInfoList.Count > 1)
                    {
                        mZScreen.niTray.Icon = Resources.zss_busy;
                    }
                    else
                    {
                        mZScreen.niTray.Text = this.mZScreen.Text; // do not update notifyIcon text if there are other jobs active
                        mZScreen.niTray.Icon = Resources.zss_tray;
                    }

                    if (task.LinkManager != null && !string.IsNullOrEmpty(task.RemoteFilePath) || File.Exists(task.LocalFilePath) || task.Job2 == WorkerTask.JobLevel2.LANGUAGE_TRANSLATOR)
                    {
                        if (Engine.conf.CompleteSound)
                        {
                            SystemSounds.Exclamation.Play();
                        }

                        if (Engine.conf.ShowBalloonTip)
                        {
                            new BalloonTipHelper(mZScreen.niTray, task).ShowBalloonTip();
                        }
                    }

                    if (task.Errors.Count > 0)
                    {
                        foreach (var error in task.Errors)
                        {
                            FileSystem.AppendDebug(error);
                        }

                        MessageBox.Show(task.Errors[task.Errors.Count - 1], "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

                if (task.MyImage != null)
                {
                    task.MyImage.Dispose(); // For fix memory leak
                }

                if (Engine.conf.AddFailedScreenshot || (!Engine.conf.AddFailedScreenshot && task.Errors.Count == 0))
                {
                    AddHistoryItem(task);
                }
            }

            catch (Exception ex)
            {
                FileSystem.AppendDebug("Job Completed with errors: ", ex);
            }

            finally
            {
                ClipboardManager.Commit(task.UniqueNumber);

                if (CoreHelpers.RunningOnWin7)
                {
                    Adapter.TaskbarSetProgressState(mZScreen, TaskbarProgressBarState.NoProgress);
                }

                mZScreen.btnUploadersTest.Enabled = true;
            }
        }

        #endregion Worker Events

        /// <summary>
        /// Worker for Text: Paste2, Pastebin
        /// </summary>
        /// <returns></returns>
        public override WorkerTask GetWorkerText(WorkerTask.JobLevel2 job, string localFilePath)
        {
            WorkerTask task = base.GetWorkerText(job, localFilePath);

            switch (job)
            {
                case WorkerTask.JobLevel2.LANGUAGE_TRANSLATOR:
                    mZScreen.btnTranslate.Enabled = false;
                    task.TranslationInfo = new GoogleTranslateInfo()
                    {
                        Text = mZScreen.txtTranslateText.Text,
                        SourceLanguage = Engine.conf.GoogleSourceLanguage,
                        TargetLanguage = Engine.conf.GoogleTargetLanguage
                    };

                    break;
            }

            return task;
        }

        public void CaptureWebpage(WorkerTask task)
        {
            if (task != null && FileSystem.IsValidLink(task.MyText))
            {
                WebPageCapture webPageCapture;
                if (Engine.conf.WebPageUseCustomSize)
                {
                    webPageCapture = new WebPageCapture(Engine.conf.WebPageWidth, Engine.conf.WebPageHeight);
                }
                else
                {
                    webPageCapture = new WebPageCapture();
                }

                webPageCapture.DownloadCompleted += new WebPageCapture.ImageEventHandler(webPageCapture_DownloadCompleted);
                webPageCapture.DownloadPage(task.MyText);
            }
        }

        private void webPageCapture_DownloadCompleted(Image img)
        {
            if (img != null)
            {
                Bitmap bmp = new Bitmap(img);
            }
        }

        internal void EventJobs(object sender, WorkerTask.JobLevel2 jobs)
        {
            switch (jobs)
            {
                case WorkerTask.JobLevel2.TAKE_SCREENSHOT_SCREEN:
                    StartBW_EntireScreen();
                    break;
                case WorkerTask.JobLevel2.TAKE_SCREENSHOT_WINDOW_ACTIVE:
                    StartBW_ActiveWindow();
                    break;
                case WorkerTask.JobLevel2.TakeScreenshotWindowSelected:
                    StartBw_SelectedWindow();
                    break;
                case WorkerTask.JobLevel2.TakeScreenshotCropped:
                    StartBw_CropShot();
                    break;
                case WorkerTask.JobLevel2.TAKE_SCREENSHOT_LAST_CROPPED:
                    StartBW_LastCropShot();
                    break;
                case WorkerTask.JobLevel2.AUTO_CAPTURE:
                    ShowAutoCapture();
                    break;
                case WorkerTask.JobLevel2.UploadFromClipboard:
                    UploadUsingClipboard();
                    break;
                case WorkerTask.JobLevel2.PROCESS_DRAG_N_DROP:
                    ShowDropWindow();
                    break;
                case WorkerTask.JobLevel2.LANGUAGE_TRANSLATOR:
                    StartWorkerTranslator();
                    break;
                case WorkerTask.JobLevel2.SCREEN_COLOR_PICKER:
                    ScreenColorPicker();
                    break;
            }
        }

        public void LanguageTranslator(WorkerTask task)
        {
            task.TranslationInfo = new GoogleTranslate(Engine.GoogleTranslateKey).TranslateText(task.TranslationInfo);
            task.SetText(task.TranslationInfo.Result);
        }

        public void StartWorkerTranslator()
        {
            if (Clipboard.ContainsText())
            {
                StartBW_LanguageTranslator(new GoogleTranslateInfo()
                {
                    Text = Clipboard.GetText(),
                    SourceLanguage = Engine.conf.GoogleSourceLanguage,
                    TargetLanguage = Engine.conf.GoogleTargetLanguage
                });
            }
        }

        public void CheckHotkeys(object sender, KeyEventArgs e)
        {
            if (mSetHotkeys)
            {
                if (e.KeyData == Keys.Enter)
                {
                    QuitSettingHotkeys();
                }
                else if (e.KeyData == Keys.Escape)
                {
                    mHotkeyMgr.SetHotkey(Keys.None);
                }
                else
                {
                    mHotkeyMgr.SetHotkey(e.KeyData);
                }
            }
            else
            {
                e.Handled = UploadUsingHotkeys(e.KeyData);
            }
        }

        private bool UploadUsingHotkeys(Keys key)
        {
            // Fix for Issue 23 - Media Center was triggering Keys.None
            if (key != Keys.None)
            {
                if (Engine.conf.HotkeyEntireScreen == key) // Entire Screen
                {
                    StartBW_EntireScreen();
                    return true;
                }

                if (Engine.conf.HotkeyActiveWindow == key) // Active Window
                {
                    StartBW_ActiveWindow();
                    return true;
                }

                if (Engine.conf.HotkeySelectedWindow == key) // Selected Window
                {
                    StartBw_SelectedWindow();
                    return true;
                }

                if (Engine.conf.HotkeyCropShot == key) // Crop Shot
                {
                    FileSystem.AppendDebug("Crop Shot Hotkey triggered: " + key.ToSpecialString());
                    StartBw_CropShot();
                    return true;
                }

                if (Engine.conf.HotkeyLastCropShot == key) // Last Crop Shot
                {
                    StartBW_LastCropShot();
                    return true;
                }

                if (Engine.conf.HotkeyFreehandCropShot == key) // Freehand Crop Shot
                {
                    StartBw_FreehandCropShot();
                    return true;
                }

                if (Engine.conf.HotkeyAutoCapture == key) // Auto Capture
                {
                    ShowAutoCapture();
                    return true;
                }

                if (Engine.conf.HotkeyClipboardUpload == key) // Clipboard Upload
                {
                    UploadUsingClipboard();
                    return true;
                }

                if (Engine.conf.HotkeyDropWindow == key) // Drag & Drop Window
                {
                    ShowDropWindow();
                    return true;
                }

                if (Engine.conf.HotkeyActionsToolbar == key) // Actions Toolbar
                {
                    ShowActionsToolbar(true);
                    return true;
                }

                if (Engine.conf.HotkeyQuickOptions == key) // Quick Options
                {
                    ShowQuickOptions();
                    return true;
                }

                if (Engine.conf.HotkeyLanguageTranslator == key) // Language Translator
                {
                    StartWorkerTranslator();
                    return true;
                }

                if (Engine.conf.HotkeyScreenColorPicker == key) // Screen Color Picker
                {
                    ScreenColorPicker();
                    return true;
                }

                if (Engine.conf.HotkeyTwitterClient == key)
                {
                    Adapter.TwitterMsg("");
                    return true;
                }
            }

            return false;
        }

        public void QuitSettingHotkeys()
        {
            if (mSetHotkeys)
            {
                mSetHotkeys = false;

                if (HotkeyMgr.mHKSelectedRow > -1)
                {
                    mZScreen.lblHotkeyStatus.Text = GetSelectedHotkeyName() + " Hotkey Updated.";
                    //reset hotkey text from <set keys> back to normal
                    mZScreen.dgvHotkeys.Rows[HotkeyMgr.mHKSelectedRow].Cells[1].Value = GetSelectedHotkeySpecialString();
                }
                HotkeyMgr.mHKSelectedRow = -1;
            }
        }

        public string GetSelectedHotkeyName()
        {
            return mZScreen.dgvHotkeys.Rows[HotkeyMgr.mHKSelectedRow].Cells[0].Value.ToString();
        }

        public string GetSelectedHotkeySpecialString()
        {
            object obj = Engine.conf.GetFieldValue("Hotkey" + GetSelectedHotkeyName().Replace(" ", string.Empty));
            if (obj != null && obj.GetType() == typeof(Keys))
            {
                return ((Keys)obj).ToSpecialString();
            }

            return "Error getting hotkey";
        }

        public void UploadUsingClipboard()
        {
            Engine.ClipboardUnhook();
            if (Clipboard.ContainsText() && Engine.conf.AutoTranslate && Clipboard.GetText().Length <= Engine.conf.AutoTranslateLength)
            {
                StartWorkerTranslator();
            }
            else
            {
                StartBw_ClipboardUpload();
            }
        }

        public void AddHistoryItem(WorkerTask task)
        {
            if (Engine.conf.HistorySave)
            {
                HistoryManager.AutomaticlyAddHistoryItemAsync(Engine.HistoryDbPath, task.GenerateHistoryItem());
            }

            Adapter.AddRecentItem(task.LocalFilePath);
        }

        private void FillGoogleTranslateInfo(GoogleTranslateInfo info)
        {
            mZScreen.txtTranslateText.Text = info.Text;
            mZScreen.txtLanguages.Text = info.SourceLanguage + " -> " + info.TargetLanguage;
            mZScreen.txtTranslateResult.Text = info.Result;
        }

        #region Start Workers

        public void StartBW_LanguageTranslator(GoogleTranslateInfo translationInfo)
        {
            WorkerTask t = CreateTask(WorkerTask.JobLevel2.LANGUAGE_TRANSLATOR);
            mZScreen.btnTranslate.Enabled = false;
            mZScreen.btnTranslateTo1.Enabled = false;
            t.TranslationInfo = translationInfo;
            t.MyWorker.RunWorkerAsync(t);
        }

        public void StartBW_EntireScreen()
        {
            StartWorkerScreenshots(WorkerTask.JobLevel2.TAKE_SCREENSHOT_SCREEN);
        }

        public void StartBW_ActiveWindow()
        {
            StartWorkerScreenshots(WorkerTask.JobLevel2.TAKE_SCREENSHOT_WINDOW_ACTIVE);
        }

        public void StartBW_LastCropShot()
        {
            StartWorkerScreenshots(WorkerTask.JobLevel2.TAKE_SCREENSHOT_LAST_CROPPED);
        }

        #endregion Start Workers

        #region Quick Actions

        /// <summary>
        /// Show Actions Toolbar
        /// </summary>
        /// <param name="manual">If user clicks from Tray Menu then Manual is set to true.</param>
        public void ShowActionsToolbar(bool manual)
        {
            if (!bQuickActionsOpened)
            {
                bQuickActionsOpened = true;
                ToolbarWindow actionsToolbar = new ToolbarWindow { Icon = Resources.zss_main };
                actionsToolbar.Location = Engine.conf.ActionToolbarLocation;
                actionsToolbar.EventJob += new JobsEventHandler(EventJobs);
                actionsToolbar.FormClosed += new FormClosedEventHandler(quickActions_FormClosed);
                actionsToolbar.Show();
                if (manual)
                {
                    actionsToolbar.Show();
                    Rectangle taskbar = NativeMethods.GetTaskbarRectangle();
                    actionsToolbar.Location = new Point(SystemInformation.PrimaryMonitorSize.Width - actionsToolbar.Width - 100,
                        SystemInformation.PrimaryMonitorSize.Height - taskbar.Height - actionsToolbar.Height - 10);
                }
            }
        }

        private void quickActions_FormClosed(object sender, FormClosedEventArgs e)
        {
            bQuickActionsOpened = false;
        }

        #endregion Quick Actions

        #region "Show Quick Actions"

        public void ShowQuickOptions()
        {
            if (!bQuickOptionsOpened)
            {
                bQuickOptionsOpened = true;
                QuickOptions quickOptions = new QuickOptions { Icon = Resources.zss_main };
                quickOptions.FormClosed += new FormClosedEventHandler(QuickOptionsFormClosed);
                quickOptions.ApplySettings += new EventHandler(QuickOptionsApplySettings);
                quickOptions.Show();
                Rectangle taskbar = NativeMethods.GetTaskbarRectangle();
                quickOptions.Location = new Point(SystemInformation.PrimaryMonitorSize.Width - quickOptions.Width - 100,
                    SystemInformation.PrimaryMonitorSize.Height - taskbar.Height - quickOptions.Height - 10);
            }
        }

        private void QuickOptionsApplySettings(object sender, EventArgs e)
        {
            mZScreen.ucDestOptions.cboImageUploaders.SelectedIndex = Engine.conf.MyImageUploader;
            mZScreen.cboClipboardTextMode.SelectedIndex = Engine.conf.MyClipboardUriMode;
        }

        private void QuickOptionsFormClosed(object sender, FormClosedEventArgs e)
        {
            bQuickOptionsOpened = false;
        }

        #endregion "Show Quick Actions"

        public void ShowAutoCapture()
        {
            if (!bAutoScreenshotsOpened)
            {
                bAutoScreenshotsOpened = true;
                AutoCapture autoScreenshots = new AutoCapture { Icon = Resources.zss_main };
                autoScreenshots.EventJob += new JobsEventHandler(EventJobs);
                autoScreenshots.FormClosed += new FormClosedEventHandler(autoScreenshots_FormClosed);
                autoScreenshots.Show();
            }
        }

        private void autoScreenshots_FormClosed(object sender, FormClosedEventArgs e)
        {
            bAutoScreenshotsOpened = false;
        }

        public void ShowDropWindow()
        {
            if (!bDropWindowOpened)
            {
                bDropWindowOpened = true;
                DropWindow dw = new DropWindow();
                dw.Result += new StringsEventHandler(dw_Result);
                dw.FormClosed += new FormClosedEventHandler(dw_FormClosed);
                dw.Show();
                Rectangle taskbar = NativeMethods.GetTaskbarRectangle();
                if (Engine.conf.LastDropBoxPosition == Point.Empty)
                {
                    dw.Location = new Point(SystemInformation.PrimaryMonitorSize.Width - dw.Width - 100,
                        SystemInformation.PrimaryMonitorSize.Height - taskbar.Height - dw.Height - 10);
                }
                else
                {
                    dw.Location = Engine.conf.LastDropBoxPosition;
                }
            }
        }

        private void dw_Result(object sender, string[] strings)
        {
            if (strings != null)
            {
                UploadUsingDragDrop(strings);
            }
        }

        private void dw_FormClosed(object sender, FormClosedEventArgs e)
        {
            bDropWindowOpened = false;
        }

        public void ScreenColorPicker()
        {
            DialogColor dialogColor = new DialogColor { ScreenPicker = true };
            dialogColor.Show();
        }

        public void RetryTask(WorkerTask task)
        {
            task.Errors.Clear();
            task.MyWorker = CreateWorker();
            task.LinkManager = new ImageFileManager(task.LocalFilePath);
            task.UpdateRemoteFilePath(new UploadResult());
            new TaskManager(task).PublishData();
        }

        #region Translate

        public void Translate()
        {
            StartBW_LanguageTranslator(new GoogleTranslateInfo
            {
                Text = mZScreen.txtTranslateText.Text,
                SourceLanguage = Engine.conf.GoogleAutoDetectSource ? null : Engine.conf.GoogleSourceLanguage,
                TargetLanguage = Engine.conf.GoogleTargetLanguage
            });
        }

        public void TranslateTo1()
        {
            if (Engine.conf.GoogleTargetLanguage2 == "?")
            {
                mZScreen.lblToLanguage.BorderStyle = BorderStyle.FixedSingle;
                MessageBox.Show("Drag n drop 'To:' label to this button for be able to set button language.", mZScreen.Text,
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                mZScreen.lblToLanguage.BorderStyle = BorderStyle.None;
            }
            else
            {
                StartBW_LanguageTranslator(new GoogleTranslateInfo()
                {
                    Text = mZScreen.txtTranslateText.Text,
                    SourceLanguage = Engine.conf.GoogleAutoDetectSource ? null : Engine.conf.GoogleSourceLanguage,
                    TargetLanguage = Engine.conf.GoogleTargetLanguage2
                });
            }
        }

        #endregion Translate
    }
}
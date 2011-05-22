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
using UploadersAPILib;
using UploadersLib;
using UploadersLib.HelperClasses;
using UploadersLib.OtherServices;
using ZScreenGUI.Properties;
using ZScreenLib;
using ZSS.ColorsLib;
using ZUploader.HelperClasses;

namespace ZScreenGUI
{
    public class WorkerPrimary : Worker
    {
        private ZScreen mZScreen;
        internal bool mSetHotkeys, bAutoScreenshotsOpened, bDropWindowOpened, bQuickOptionsOpened;
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

        private bool CanStartWork(WorkerTask task)
        {
            bool can = task.WasToTakeScreenshot;
            if (!task.WasToTakeScreenshot)
            {
                switch (task.Job1)
                {
                    case JobLevel1.Image:
                        can = task.MyImageUploader != ImageUploaderType.NONE || task.MyImageUploader == ImageUploaderType.FileUploader && task.MyFileUploader != FileUploaderType.NONE;
                        break;
                    case JobLevel1.Text:
                        can = task.MyTextUploader != TextUploaderType.NONE || task.MyTextUploader == TextUploaderType.FileUploader && task.MyFileUploader != FileUploaderType.NONE;
                        break;
                    case JobLevel1.File:
                        can = task.MyFileUploader != FileUploaderType.NONE;
                        break;
                }
            }
            return can;
        }

        public override void BwApp_DoWork(object sender, DoWorkEventArgs e)
        {
            WorkerTask task = (WorkerTask)e.Argument;
            if (!CanStartWork(task))
            {
                e.Result = null; // Pass a null object because there is nothing else to do
            }
            else
            {
                task.MyWorker.ReportProgress((int)WorkerTask.ProgressType.SET_ICON_BUSY, task);
                task.UniqueNumber = UploadManager.Queue();

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

                if (task.WasToTakeScreenshot)
                {
                    if (Engine.conf.ScreenshotDelayTime > 0)
                    {
                        Thread.Sleep((int)Engine.conf.ScreenshotDelayTime);
                    }
                }

                Engine.MyLogger.WriteLine(string.Format("Job started: {0}", task.Job2));

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
                                task.UploadText();
                                break;
                            case WorkerTask.JobLevel2.LANGUAGE_TRANSLATOR:
                                LanguageTranslator(task);
                                break;
                        }
                        break;
                }

                if (task.LinkManager != null && task.MyImageUploader != ImageUploaderType.FILE && task.ShouldShortenURL(task.RemoteFilePath))
                {
                    task.ShortenURL(task.RemoteFilePath);
                }

                e.Result = task;
            }
        }

        private void BwApp_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (mZScreen == null) return;

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
                    mZScreen.Text = string.Format("{0}% - {1}", UploadManager.GetAverageProgress(), Engine.GetProductName());
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
            if (task == null) return;

            mZScreen.Text = Engine.GetProductName();
            UploadManager.LinkManagerLast = task.LinkManager;

            try
            {
                if (task.IsError && task.Errors[0].Contains(ExceptionMessage.ProxyAuthenticationRequired))
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
                    Engine.MyLogger.WriteLine(string.Format("Job completed: {0}", task.Job2));

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

                                Loader.MyGTGUI.btnTranslate.Enabled = true;
                                Loader.MyGTGUI.btnTranslateTo1.Enabled = true;
                            }
                            break;
                        case JobLevel1.Image:
                            if (task.Job2 == WorkerTask.JobLevel2.CustomUploaderTest && task.LinkManager != null && !string.IsNullOrEmpty(task.LinkManager.UploadResult.URL))
                            {
                                // TODO: What to do?
                            }

                            if (task.MyImageUploader != ImageUploaderType.FILE && Engine.conf.DeleteLocal && File.Exists(task.LocalFilePath))
                            {
                                try
                                {
                                    File.Delete(task.LocalFilePath);
                                }
                                catch (Exception ex) // TODO: sometimes file is still locked... delete those files sometime
                                {
                                    Engine.MyLogger.WriteException(ex, "Error while finalizing job");
                                }
                            }
                            break;
                    }

                    if (Engine.conf.CopyClipboardAfterTask)
                    {
                        if (!Engine.conf.ClipboardOverwrite && !Clipboard.ContainsFileDropList() && !Clipboard.ContainsImage() && !Clipboard.ContainsText() || Engine.conf.ClipboardOverwrite)
                        {
                            UploadManager.SetClipboard(mZScreen.Handle, task, false);
                        }
                    }

                    if (Engine.conf.TwitterEnabled)
                    {
                        Adapter.TwitterMsg(task);
                    }

                    bool bLastSourceButtonsEnabled = task.LinkManager != null && !string.IsNullOrEmpty(task.LinkManager.UploadResult.Source);
                    mZScreen.btnOpenSourceText.Enabled = bLastSourceButtonsEnabled;
                    mZScreen.btnOpenSourceBrowser.Enabled = bLastSourceButtonsEnabled;
                    mZScreen.btnOpenSourceString.Enabled = bLastSourceButtonsEnabled;

                    if (UploadManager.UploadInfoList.Count > 1)
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

                    if (task.IsError)
                    {
                        foreach (string error in task.Errors)
                        {
                            Engine.MyLogger.WriteLine(error);
                        }

                        MessageBox.Show(task.Errors[task.Errors.Count - 1], "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

                if (task.MyImage != null)
                {
                    task.MyImage.Dispose(); // For fix memory leak
                }

                if (!task.IsError)
                {
                    AddHistoryItem(task);
                }
            }
            catch (Exception ex)
            {
                Engine.MyLogger.WriteException(ex, "Job Completed with errors: ");
            }
            finally
            {
                UploadManager.Commit(task.UniqueNumber);

                if (TaskbarManager.IsPlatformSupported)
                {
                    Adapter.TaskbarSetProgressState(mZScreen, TaskbarProgressBarState.NoProgress);
                }
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
                    Loader.MyGTGUI.btnTranslate.Enabled = false;
                    task.TranslationInfo = new GoogleTranslateInfo()
                    {
                        Text = Loader.MyGTGUI.txtTranslateText.Text,
                        SourceLanguage = Engine.MyGTConfig.GoogleSourceLanguage,
                        TargetLanguage = Engine.MyGTConfig.GoogleTargetLanguage
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
            task.TranslationInfo = new GoogleTranslate(ZKeys.GoogleTranslateKey).TranslateText(task.TranslationInfo);
            task.SetText(task.TranslationInfo.Result);
        }

        public void StartWorkerTranslator()
        {
            if (Clipboard.ContainsText())
            {
                StartBW_LanguageTranslator(new GoogleTranslateInfo()
                {
                    Text = Clipboard.GetText(),
                    SourceLanguage = Engine.MyGTConfig.GoogleSourceLanguage,
                    TargetLanguage = Engine.MyGTConfig.GoogleTargetLanguage
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
                    Engine.MyLogger.WriteLine("Crop Shot Hotkey triggered: " + key.ToSpecialString());
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
            if (Clipboard.ContainsText() && Engine.MyGTConfig.AutoTranslate && Clipboard.GetText().Length <= Engine.MyGTConfig.AutoTranslateLength)
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
                HistoryManager.AddHistoryItemAsync(Engine.HistoryPath, task.GenerateHistoryItem());
            }

            Adapter.AddRecentItem(task.LocalFilePath);
        }

        private void FillGoogleTranslateInfo(GoogleTranslateInfo info)
        {
            Loader.MyGTGUI.txtTranslateText.Text = info.Text;
            Loader.MyGTGUI.txtLanguages.Text = info.SourceLanguage + " -> " + info.TargetLanguage;
            Loader.MyGTGUI.txtTranslateResult.Text = info.Result;
        }

        #region Start Workers

        public void StartBW_LanguageTranslator(GoogleTranslateInfo translationInfo)
        {
            WorkerTask t = CreateTask(WorkerTask.JobLevel2.LANGUAGE_TRANSLATOR);
            Loader.MyGTGUI.btnTranslate.Enabled = false;
            Loader.MyGTGUI.btnTranslateTo1.Enabled = false;
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
            mZScreen.cboURLFormat.SelectedIndex = Engine.conf.MyClipboardUriMode;
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



        #endregion Translate
    }
}
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
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using HelpersLib;
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
            task.UniqueNumber = UploadManager.Queue();

            if ((Engine.conf.PreferFileUploaderForImages && (task.JobCategory == JobCategoryType.PICTURES || task.JobCategory == JobCategoryType.SCREENSHOTS)) ||
                (Engine.conf.PreferFileUploaderForText && task.JobCategory == JobCategoryType.TEXT && task.Job != WorkerTask.Jobs.LANGUAGE_TRANSLATOR) ||
                task.Job == WorkerTask.Jobs.CustomUploaderTest)
            {
                task.JobCategory = JobCategoryType.BINARY;
                if (!Engine.conf.PreferFtpServerForIndex)
                {
                    task.MyFileUploader = Engine.conf.FileUploaderType;
                }
            }

            if (Engine.conf.PromptForUpload && task.MyImageUploader != ImageDestType.CLIPBOARD &
                task.MyImageUploader != ImageDestType.FILE &&
                (task.Job == WorkerTask.Jobs.TAKE_SCREENSHOT_SCREEN ||
                task.Job == WorkerTask.Jobs.TAKE_SCREENSHOT_WINDOW_ACTIVE) &&
                MessageBox.Show("Do you really want to upload to " + task.MyImageUploader.GetDescription() + "?",
                Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
            {
                e.Result = task;
                return;
            }

            if (task.JobCategory == JobCategoryType.SCREENSHOTS)
            {
                if (Engine.conf.ScreenshotDelayTime != 0)
                {
                    Thread.Sleep((int)Engine.conf.ScreenshotDelayTime);
                }
            }

            FileSystem.AppendDebug(string.Format("Job started: {0}", task.Job));

            switch (task.JobCategory)
            {
                case JobCategoryType.PICTURES:
                case JobCategoryType.SCREENSHOTS:
                case JobCategoryType.BINARY:
                    switch (task.Job)
                    {
                        case WorkerTask.Jobs.TAKE_SCREENSHOT_SCREEN:
                            new TaskManager(ref task).CaptureScreen();
                            break;
                        case WorkerTask.Jobs.TakeScreenshotWindowSelected:
                        case WorkerTask.Jobs.TakeScreenshotCropped:
                        case WorkerTask.Jobs.TAKE_SCREENSHOT_LAST_CROPPED:
                            new TaskManager(ref task).CaptureRegionOrWindow();
                            break;
                        case WorkerTask.Jobs.CustomUploaderTest:
                        case WorkerTask.Jobs.TAKE_SCREENSHOT_WINDOW_ACTIVE:
                            new TaskManager(ref task).CaptureActiveWindow();
                            break;
                        case WorkerTask.Jobs.FREEHAND_CROP_SHOT:
                            new TaskManager(ref task).CaptureFreehandCrop();
                            break;
                        case WorkerTask.Jobs.UPLOAD_IMAGE:
                        case WorkerTask.Jobs.UploadFromClipboard:
                        case WorkerTask.Jobs.PROCESS_DRAG_N_DROP:
                            new TaskManager(ref task).PublishData();
                            break;
                    }

                    break;
                case JobCategoryType.TEXT:
                    switch (task.Job)
                    {
                        case WorkerTask.Jobs.UploadFromClipboard:
                            PublishText(ref task);
                            break;
                        case WorkerTask.Jobs.LANGUAGE_TRANSLATOR:
                            LanguageTranslator(ref task);
                            break;
                    }

                    break;
            }

            if (task.MakeTinyURL)
            {
                string url = task.RemoteFilePath;
                if (!string.IsNullOrEmpty(url))
                {
                    url = Adapter.TryShortenURL(url);
                    if (!string.IsNullOrEmpty(url))
                    {
                        FileSystem.AppendDebug("Shortened URL: " + url);
                        task.RemoteFilePath = url;
                        if (null != task.LinkManager && null != task.LinkManager.ImageFileList)
                        {
                            task.LinkManager.ImageFileList.Add(new ImageFile(url, LinkType.FULLIMAGE_TINYURL));
                        }
                    }
                }
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
                case WorkerTask.ProgressType.ADD_FILE_TO_LISTBOX:
                    AddHistoryItem(e.UserState as HistoryItem);
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
                    mZScreen.ucDestOptions.cboImageUploaders.SelectedIndex = (int)Engine.conf.ImageUploaderType;
                    Adapter.SetNotifyIconBalloonTip(mZScreen.niTray, mZScreen.Text, string.Format("Images Destination was updated to {0}", Engine.conf.ImageUploaderType.GetDescription()), ToolTipIcon.Warning);
                    break;
                case WorkerTask.ProgressType.CHANGE_TRAY_ICON_PROGRESS:
                    int progress = (int)((ProgressManager)e.UserState).Percentage;
                    Adapter.UpdateNotifyIconProgress(mZScreen.niTray, progress);
                    Adapter.TaskbarSetProgressValue(progress);
                    mZScreen.Text = string.Format("{0}% - {1}", UploadManager.GetAverageProgress(), Engine.GetProductName());
                    break;
                case WorkerTask.ProgressType.UPDATE_PROGRESS_MAX:
                    TaskbarProgressBarState tbps = (TaskbarProgressBarState)e.UserState;
                    Adapter.TaskbarSetProgressState(tbps);
                    break;
                case WorkerTask.ProgressType.ShowTrayWarning:
                    Adapter.TaskbarSetProgressValue(33);
                    Adapter.TaskbarSetProgressState(TaskbarProgressBarState.Error);
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
                WorkerTask checkTask = RetryUpload(task);
                if (checkTask.RetryPending)
                {
                    string message = string.Format("{0}\r\n\r\nAutomatically starting upload with {1}.", string.Join("\r\n", task.Errors.ToArray()), checkTask.MyImageUploader.GetDescription());
                    mZScreen.niTray.ShowBalloonTip(5000, Application.ProductName, message, ToolTipIcon.Warning);
                }
                else
                {
                    FileSystem.AppendDebug(string.Format("Job completed: {0}", task.Job));
                    if (task.MyImageUploader == ImageDestType.FILE && Engine.conf.ShowSaveFileDialogImages)
                    {
                        string fp = Adapter.SaveImage(task.MyImage);
                        if (!string.IsNullOrEmpty(fp))
                        {
                            task.UpdateLocalFilePath(fp);
                        }
                    }

                    if (!string.IsNullOrEmpty(task.LocalFilePath) && File.Exists(task.LocalFilePath))
                    {
                        if (Engine.conf.AddFailedScreenshot ||
                            (!Engine.conf.AddFailedScreenshot && task.Errors.Count == 0 || task.JobCategory == JobCategoryType.TEXT))
                        {
                            AddHistoryItem(new HistoryItem(task));
                        }
                    }

                    switch (task.JobCategory)
                    {
                        case JobCategoryType.BINARY:
                            if (!string.IsNullOrEmpty(task.RemoteFilePath))
                            {
                                Clipboard.SetText(task.RemoteFilePath);
                            }
                            break;
                        case JobCategoryType.TEXT:
                            switch (task.Job)
                            {
                                case WorkerTask.Jobs.LANGUAGE_TRANSLATOR:
                                    if (mZScreen != null)
                                    {
                                        FillGoogleTranslateInfo(task.TranslationInfo);

                                        this.mZScreen.btnTranslate.Enabled = true;
                                        this.mZScreen.btnTranslateTo1.Enabled = true;
                                    }
                                    break;
                            }
                            break;
                        case JobCategoryType.SCREENSHOTS:
                            switch (task.Job)
                            {
                                case WorkerTask.Jobs.CustomUploaderTest:
                                    if (task.LinkManager != null && task.LinkManager.ImageFileList.Count > 0)
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
                                    break;
                            }

                            if (task.MyImageUploader != ImageDestType.FILE && Engine.conf.DeleteLocal && File.Exists(task.LocalFilePath))
                            {
                                try
                                {
                                    File.Delete(task.LocalFilePath);
                                }
                                catch (Exception ex) // sometimes file is still locked... ToDo: delete those files sometime
                                {
                                    FileSystem.AppendDebug("Error while finalizing job", ex);
                                }
                            }
                            break;
                    }

                    UploadManager.SetClipboardText(task, false);

                    if (Engine.conf.TwitterEnabled)
                    {
                        Adapter.TwitterMsg(ref task);
                    }

                    if (task.LinkManager != null && !string.IsNullOrEmpty(task.LinkManager.Source))
                    {
                        this.mZScreen.btnOpenSourceText.Enabled = true;
                        this.mZScreen.btnOpenSourceBrowser.Enabled = true;
                        this.mZScreen.btnOpenSourceString.Enabled = true;
                    }

                    if (UploadManager.UploadInfoList.Count > 1)
                    {
                        this.mZScreen.niTray.Icon = Resources.zss_busy;
                    }
                    else
                    {
                        this.mZScreen.niTray.Text = this.mZScreen.Text; // do not update notifyIcon text if there are other jobs active
                        this.mZScreen.niTray.Icon = Resources.zss_tray;
                    }

                    if (task.Job == WorkerTask.Jobs.LANGUAGE_TRANSLATOR || File.Exists(task.LocalFilePath) || !string.IsNullOrEmpty(task.RemoteFilePath))
                    {
                        if (Engine.conf.CompleteSound)
                        {
                            System.Media.SystemSounds.Exclamation.Play();
                        }

                        if (Engine.conf.ShowBalloonTip)
                        {
                            new BalloonTipHelper(this.mZScreen.niTray, task).ShowBalloonTip();
                        }
                    }

                    if (task.Errors.Count > 0)
                    {
                        foreach (var error in task.Errors)
                            FileSystem.AppendDebug(error);
                        MessageBox.Show(task.Errors[task.Errors.Count - 1], "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

                if (task.MyImage != null)
                {
                    task.MyImage.Dispose(); // For fix memory leak
                }
            }
            catch (Exception ex)
            {
                FileSystem.AppendDebug("Job Completed with errors: ", ex);
            }
            finally
            {
                UploadManager.Commit(task.UniqueNumber);
                if (CoreHelpers.RunningOnWin7)
                {
                    Adapter.TaskbarSetProgressState(TaskbarProgressBarState.NoProgress);
                }
                this.mZScreen.btnUploadersTest.Enabled = true;
            }
        }

        #endregion Worker Events

        /// <summary>
        /// Worker for Text: Paste2, Pastebin
        /// </summary>
        /// <returns></returns>
        public override WorkerTask GetWorkerText(WorkerTask.Jobs job, string localFilePath)
        {
            WorkerTask task = base.GetWorkerText(job, localFilePath);

            switch (job)
            {
                case WorkerTask.Jobs.LANGUAGE_TRANSLATOR:
                    mZScreen.btnTranslate.Enabled = false;
                    task.TranslationInfo = new GoogleTranslate.TranslationInfo(mZScreen.txtTranslateText.Text, ZScreen.mGTranslator.LanguageOptions.SourceLangList[mZScreen.cbFromLanguage.SelectedIndex],
                        ZScreen.mGTranslator.LanguageOptions.TargetLangList[mZScreen.cbToLanguage.SelectedIndex]);
                    if (task.TranslationInfo.IsEmpty())
                    {
                        mZScreen.btnTranslate.Enabled = true;
                    }

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

        public void HistoryRetryUpload(HistoryItem hi)
        {
            if (hi != null && File.Exists(hi.LocalPath))
            {
                WorkerTask task = CreateTask(WorkerTask.Jobs.UPLOAD_IMAGE);
                task.JobCategory = hi.JobCategory;
                task.SetImage(hi.LocalPath);
                task.UpdateLocalFilePath(hi.LocalPath);
                task.MyImageUploader = hi.ImageDestCategory;
                task.MyWorker.RunWorkerAsync(task);
            }
        }

        internal void EventJobs(object sender, WorkerTask.Jobs jobs)
        {
            switch (jobs)
            {
                case WorkerTask.Jobs.TAKE_SCREENSHOT_SCREEN:
                    StartBW_EntireScreen();
                    break;
                case WorkerTask.Jobs.TAKE_SCREENSHOT_WINDOW_ACTIVE:
                    StartBW_ActiveWindow();
                    break;
                case WorkerTask.Jobs.TakeScreenshotWindowSelected:
                    StartBw_SelectedWindow();
                    break;
                case WorkerTask.Jobs.TakeScreenshotCropped:
                    StartBw_CropShot();
                    break;
                case WorkerTask.Jobs.TAKE_SCREENSHOT_LAST_CROPPED:
                    StartBW_LastCropShot();
                    break;
                case WorkerTask.Jobs.AUTO_CAPTURE:
                    ShowAutoCapture();
                    break;
                case WorkerTask.Jobs.UploadFromClipboard:
                    UploadUsingClipboard();
                    break;
                case WorkerTask.Jobs.PROCESS_DRAG_N_DROP:
                    ShowDropWindow();
                    break;
                case WorkerTask.Jobs.LANGUAGE_TRANSLATOR:
                    StartWorkerTranslator();
                    break;
                case WorkerTask.Jobs.SCREEN_COLOR_PICKER:
                    ScreenColorPicker();
                    break;
            }
        }

        public void LanguageTranslator(ref WorkerTask task)
        {
            task.TranslationInfo.Result = ZScreen.mGTranslator.TranslateText(task.TranslationInfo);
            task.MyText = TextInfo.FromString(task.TranslationInfo.Result.TranslatedText);
        }

        public void StartWorkerTranslator()
        {
            if (Clipboard.ContainsText())
            {
                StartBW_LanguageTranslator(new GoogleTranslate.TranslationInfo(Clipboard.GetText(),
                    GoogleTranslate.FindLanguage(Engine.conf.FromLanguage, ZScreen.mGTranslator.LanguageOptions.SourceLangList),
                    GoogleTranslate.FindLanguage(Engine.conf.ToLanguage, ZScreen.mGTranslator.LanguageOptions.TargetLangList)));
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

        public void AddHistoryItem(HistoryItem hi)
        {
            mZScreen.lbHistory.Items.Insert(0, hi);
            CheckHistoryItems();
            SaveHistoryItems();
            if (mZScreen.lbHistory.Items.Count > 0)
            {
                mZScreen.lbHistory.ClearSelected();
                mZScreen.lbHistory.SelectedIndex = 0;
            }

            Adapter.AddRecentItem(hi.LocalPath);
        }

        private void FillGoogleTranslateInfo(GoogleTranslate.TranslationInfo info)
        {
            mZScreen.txtTranslateText.Text = info.SourceText;
            mZScreen.txtTranslateResult.Text = info.Result.TranslatedText;
            mZScreen.txtLanguages.Text = info.Result.TranslationType;
            List<GoogleTranslate.Vocabulary> dictionary = info.Result.Dictionary;
            if (dictionary.Count > 0)
            {
                foreach (GoogleTranslate.Vocabulary vocab in dictionary)
                {
                    ListViewGroup group = new ListViewGroup(vocab.Name, HorizontalAlignment.Left);
                    mZScreen.lvDictionary.Groups.Add(group);

                    foreach (string word in vocab.Words)
                    {
                        ListViewItem lvi = new ListViewItem(word) { Group = group };
                        mZScreen.lvDictionary.Items.Add(lvi);
                    }
                }
            }
            else
            {
                mZScreen.lvDictionary.Groups.Clear();
                mZScreen.lvDictionary.Items.Clear();
            }
        }

        #region Start Workers

        public void StartBW_LanguageTranslator(GoogleTranslate.TranslationInfo translationInfo)
        {
            if (mZScreen.cbFromLanguage.Items.Count > 0 && mZScreen.cbToLanguage.Items.Count > 0 && !translationInfo.IsEmpty())
            {
                WorkerTask t = CreateTask(WorkerTask.Jobs.LANGUAGE_TRANSLATOR);
                t.JobCategory = JobCategoryType.TEXT;
                mZScreen.btnTranslate.Enabled = false;
                mZScreen.btnTranslateTo1.Enabled = false;
                t.TranslationInfo = translationInfo;
                t.MyWorker.RunWorkerAsync(t);
            }
        }

        public void StartBW_EntireScreen()
        {
            StartWorkerScreenshots(WorkerTask.Jobs.TAKE_SCREENSHOT_SCREEN);
        }

        public void StartBW_ActiveWindow()
        {
            StartWorkerScreenshots(WorkerTask.Jobs.TAKE_SCREENSHOT_WINDOW_ACTIVE);
        }

        public void StartBW_LastCropShot()
        {
            StartWorkerScreenshots(WorkerTask.Jobs.TAKE_SCREENSHOT_LAST_CROPPED);
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
            mZScreen.ucDestOptions.cboImageUploaders.SelectedIndex = (int)Engine.conf.ImageUploaderType;
            mZScreen.cboClipboardTextMode.SelectedIndex = (int)Engine.conf.ClipboardUriMode;
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

        #region Translate

        public void TranslateTo1()
        {
            if (Engine.conf.ToLanguage2 == "?")
            {
                mZScreen.lblToLanguage.BorderStyle = BorderStyle.FixedSingle;
                MessageBox.Show("Drag n drop 'To:' label to this button for be able to set button language.", mZScreen.Text,
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                mZScreen.lblToLanguage.BorderStyle = BorderStyle.None;
            }
            else
            {
                StartBW_LanguageTranslator(new GoogleTranslate.TranslationInfo(mZScreen.txtTranslateText.Text,
                     GoogleTranslate.FindLanguage(Engine.conf.FromLanguage, ZScreen.mGTranslator.LanguageOptions.SourceLangList),
                     GoogleTranslate.FindLanguage(Engine.conf.ToLanguage2, ZScreen.mGTranslator.LanguageOptions.TargetLangList)));
            }
        }

        #endregion Translate

        #region ZScreen GUI

        public void CheckHistoryItems()
        {
            if (mZScreen.lbHistory.Items.Count > Engine.conf.HistoryMaxNumber)
            {
                for (int i = mZScreen.lbHistory.Items.Count - 1; i >= Engine.conf.HistoryMaxNumber; i--)
                {
                    mZScreen.lbHistory.Items.RemoveAt(i);
                }
            }

            UpdateGuiControlsHistory();
        }

        public void SaveHistoryItems()
        {
            if (Engine.conf.HistorySave)
            {
                List<HistoryItem> historyItems = new List<HistoryItem>();
                foreach (HistoryItem item in mZScreen.lbHistory.Items)
                {
                    historyItems.Add(item);
                }

                HistoryManager hm = new HistoryManager(historyItems);
                hm.Save();
            }
        }

        public void UpdateGuiControlsHistory()
        {
            mZScreen.tpHistoryList.Text = string.Format("History List ({0}/{1})", mZScreen.lbHistory.Items.Count, Engine.conf.HistoryMaxNumber);
        }

        #endregion ZScreen GUI
    }
}
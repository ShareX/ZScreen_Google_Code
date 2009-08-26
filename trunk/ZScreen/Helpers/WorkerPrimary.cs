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
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using Microsoft.WindowsAPICodePack.Taskbar;
using MS.WindowsAPICodePack.Internal;
using UploadersLib;
using UploadersLib.Helpers;
using UploadersLib.TextServices;
using ZScreenLib;
using ZSS.ColorsLib;
using ZSS.IndexersLib;
using ZSS.Properties;

namespace ZScreenGUI
{
    public class WorkerPrimary
    {
        private ZScreen mZScreen;
        internal bool mSetHotkeys, bAutoScreenshotsOpened, bDropWindowOpened, bQuickActionsOpened, bQuickOptionsOpened;
        internal int mHKSelectedRow = -1;

        public WorkerPrimary(ZScreen myZScreen)
        {
            this.mZScreen = myZScreen;
        }

        public BackgroundWorker CreateWorker()
        {
            BackgroundWorker bwApp = new BackgroundWorker { WorkerReportsProgress = true };
            bwApp.DoWork += new System.ComponentModel.DoWorkEventHandler(BwApp_DoWork);
            bwApp.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(BwApp_ProgressChanged);
            bwApp.RunWorkerCompleted += new RunWorkerCompletedEventHandler(BwApp_RunWorkerCompleted);
            return bwApp;
        }

        #region Worker Events

        private void BwApp_DoWork(object sender, DoWorkEventArgs e)
        {
            WorkerTask task = (WorkerTask)e.Argument;
            task.MyWorker.ReportProgress((int)WorkerTask.ProgressType.SET_ICON_BUSY, task);
            task.UniqueNumber = UploadManager.Queue();

            if ((Program.conf.PreferFileUploaderForImages && (task.JobCategory == JobCategoryType.PICTURES || task.JobCategory == JobCategoryType.SCREENSHOTS)) ||
                (Program.conf.PreferFileUploaderForText && task.JobCategory == JobCategoryType.TEXT))
            {
                task.JobCategory = JobCategoryType.BINARY;
                task.MyFileUploader = Program.conf.FileDestMode;
            }

            if (Program.conf.PromptForUpload && task.MyImageUploader != ImageDestType.CLIPBOARD &
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
                if (Program.conf.ScreenshotDelayTime != 0)
                {
                    Thread.Sleep((int)(Program.conf.ScreenshotDelayTime));
                }
            }

            FileSystem.AppendDebug("."); // add a gap to distinguish between jobs
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
                        case WorkerTask.Jobs.UPLOAD_IMAGE:
                        case WorkerTask.Jobs.UploadFromClipboard:
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
                //case JobCategoryType.BINARY:
                //    switch (task.Job)
                //    {
                //        case WorkerTask.Jobs.UploadFromClipboard:
                //            if (Program.conf.AutoSwitchFileUploader)
                //            {
                //                task.MyImageUploader = ImageDestType.FTP;
                //                PublishBinary(ref task);
                //            }
                //            break;
                //    }
                //    break;
            }

            if (!string.IsNullOrEmpty(task.LocalFilePath) && File.Exists(task.LocalFilePath))
            {
                if (Program.conf.AddFailedScreenshot ||
                    (!Program.conf.AddFailedScreenshot && task.Errors.Count == 0 ||
                    task.JobCategory == JobCategoryType.TEXT))
                {
                    task.MyWorker.ReportProgress((int)WorkerTask.ProgressType.ADD_FILE_TO_LISTBOX, new HistoryItem(task));
                }
            }

            e.Result = task;
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
                case WorkerTask.ProgressType.ADD_FILE_TO_LISTBOX:
                    AddHistoryItem(e.UserState as HistoryItem);
                    break;
                case WorkerTask.ProgressType.COPY_TO_CLIPBOARD_IMAGE:
                    if (e.UserState.GetType() == typeof(string))
                    {
                        Adapter.CopyImageToClipboard(e.UserState as string);
                    }
                    else if (e.UserState.GetType() == typeof(Bitmap))
                    {
                        Adapter.CopyImageToClipboard(e.UserState as Image);
                    }
                    break;
                case WorkerTask.ProgressType.COPY_TO_CLIPBOARD_URL:
                    string url = e.UserState as string;
                    if (!string.IsNullOrEmpty(url))
                    {
                        Clipboard.SetText(url);
                    }
                    break;
                case WorkerTask.ProgressType.FLASH_ICON:
                    Adapter.FlashNotifyIcon(mZScreen.niTray, e.UserState as Icon);
                    break;
                case WorkerTask.ProgressType.SET_ICON_BUSY:
                    Adapter.SetNotifyIconStatus(e.UserState as WorkerTask, mZScreen.niTray, Resources.zss_busy);
                    break;
                case WorkerTask.ProgressType.UpdateCropMode:
                    mZScreen.cboCropGridMode.Checked = Program.conf.CropGridToggle;
                    break;
                case WorkerTask.ProgressType.CHANGE_UPLOAD_DESTINATION:
                    mZScreen.ucDestOptions.cboImageUploaders.SelectedIndex = (int)Program.conf.ScreenshotDestMode;
                    Adapter.SetNotifyIconBalloonTip(mZScreen.niTray, mZScreen.Text, string.Format("Images Destination was updated to {0}", Program.conf.ScreenshotDestMode.GetDescription()), ToolTipIcon.Warning);
                    break;
                case WorkerTask.ProgressType.CHANGE_TRAY_ICON_PROGRESS:
                    int progress = (int)e.UserState;
                    Adapter.UpdateNotifyIconProgress(mZScreen.niTray, progress);
                    Adapter.TaskbarSetProgressValue(progress);
                    mZScreen.Text = string.Format("{0}% - {1}", progress, Program.GetProductName());
                    break;
                case WorkerTask.ProgressType.UPDATE_PROGRESS_MAX:
                    TaskbarProgressBarState tbps = (TaskbarProgressBarState)e.UserState;
                    Adapter.TaskbarSetProgressState(tbps);
                    break;
                case WorkerTask.ProgressType.SHOW_TRAY_MESSAGE:
                    Adapter.TaskbarSetProgressState(TaskbarProgressBarState.Error);
                    Adapter.SetNotifyIconBalloonTip(mZScreen.niTray, mZScreen.Text, e.UserState as string, ToolTipIcon.Warning);
                    break;
            }
        }

        private void BwApp_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            WorkerTask task = (WorkerTask)e.Result;
            mZScreen.Text = Program.GetProductName();

            try
            {
                FileSystem.AppendDebug(string.Format("Job completed: {0}", task.Job));

                if (!RetryUpload(task))
                {
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
                                        this.mZScreen.txtTranslateText.Text = task.TranslationInfo.SourceText;
                                        this.mZScreen.txtTranslateResult.Text = task.TranslationInfo.Result.TranslatedText;
                                        this.mZScreen.txtLanguages.Text = task.TranslationInfo.Result.TranslationType;
                                        this.mZScreen.txtDictionary.Text = task.TranslationInfo.Result.Dictionary;
                                    }
                                    if (Program.conf.ClipboardTranslate)
                                    {
                                        Clipboard.SetText(task.TranslationInfo.Result.TranslatedText);
                                    }
                                    if (mZScreen != null)
                                    {
                                        this.mZScreen.btnTranslate.Enabled = true;
                                        this.mZScreen.btnTranslateTo1.Enabled = true;
                                    }

                                    break;
                                case WorkerTask.Jobs.UploadFromClipboard:
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
                                case WorkerTask.Jobs.CustomUploaderTest:
                                    if (task.ImageManager != null && task.ImageManager.ImageFileList.Count > 0)
                                    {
                                        if (task.ImageManager.GetFullImageUrl() != "")
                                        {
                                            this.mZScreen.txtUploadersLog.AppendText(task.DestinationName + " full image: " +
                                                task.ImageManager.GetFullImageUrl() + "\r\n");
                                        }
                                        if (task.ImageManager.GetThumbnailUrl() != "")
                                        {
                                            this.mZScreen.txtUploadersLog.AppendText(task.DestinationName + " thumbnail: " +
                                                task.ImageManager.GetThumbnailUrl() + "\r\n");
                                        }
                                    }
                                    this.mZScreen.btnUploadersTest.Enabled = true;
                                    break;
                            }
                            if (task.MyImageUploader != ImageDestType.FILE && Program.conf.DeleteLocal && File.Exists(task.LocalFilePath))
                            {
                                try
                                {
                                    File.Delete(task.LocalFilePath);
                                }
                                //sometimes file is still locked... ToDo: delte those files sometime
                                catch (Exception ex)
                                {
                                    FileSystem.AppendDebug(ex.ToString());
                                }
                            }
                            break;
                    }

                    if (task.JobCategory != JobCategoryType.TEXT)
                    {
                        UploadManager.SetClipboardText(task, false);
                    }

                    if (task.ImageManager != null && !string.IsNullOrEmpty(task.ImageManager.Source))
                    {
                        this.mZScreen.btnOpenSourceText.Enabled = true;
                        this.mZScreen.btnOpenSourceBrowser.Enabled = true;
                        this.mZScreen.btnOpenSourceString.Enabled = true;
                    }

                    this.mZScreen.niTray.Text = this.mZScreen.Text;
                    if (UploadManager.UploadInfoList.Count > 1)
                    {
                        this.mZScreen.niTray.Icon = Resources.zss_busy;
                    }
                    else
                    {
                        this.mZScreen.niTray.Icon = Resources.zss_tray;
                    }

                    if (task.Job == WorkerTask.Jobs.LANGUAGE_TRANSLATOR || File.Exists(task.LocalFilePath) || !string.IsNullOrEmpty(task.RemoteFilePath))
                    {
                        if (Program.conf.CompleteSound)
                        {
                            System.Media.SystemSounds.Exclamation.Play();
                        }
                        if (Program.conf.ShowBalloonTip)
                        {
                            new BalloonTipHelper(this.mZScreen.niTray, task).ShowBalloonTip();
                        }
                    }

                    if (task.Errors.Count > 0)
                    {
                        FileSystem.AppendDebug(task.Errors[task.Errors.Count - 1]);
                    }
                }

                if (CoreHelpers.RunningOnWin7)
                {
                    Adapter.TaskbarSetProgressState(TaskbarProgressBarState.NoProgress);
                }


                if (task.MyImage != null) task.MyImage.Dispose(); // For fix memory leak
            }
            catch (Exception ex)
            {
                FileSystem.AppendDebug(ex.ToString());
            }
            finally
            {
                UploadManager.Commit(task.UniqueNumber);
            }
        }

        #endregion

        public WorkerTask CreateTask(WorkerTask.Jobs job)
        {
            BackgroundWorker bwApp = CreateWorker();
            WorkerTask task = new WorkerTask(bwApp, job);
            if (task.Job != WorkerTask.Jobs.CustomUploaderTest)
            {
                task.MyImageUploader = Program.conf.ScreenshotDestMode;
                task.MyFileUploader = Program.conf.FileDestMode;
            }
            else
            {
                task.MyImageUploader = ImageDestType.CUSTOM_UPLOADER;
            }
            return task;
        }

        public WorkerTask GetWorkerText(WorkerTask.Jobs job)
        {
            return GetWorkerText(job, "");
        }

        /// <summary>
        /// Worker for Text: Paste2, Pastebin
        /// </summary>
        /// <returns></returns>
        public WorkerTask GetWorkerText(WorkerTask.Jobs job, string localFilePath)
        {
            WorkerTask t = CreateTask(job);
            t.JobCategory = JobCategoryType.TEXT;
            // t.MakeTinyURL = Program.MakeTinyURL();
            t.MyTextUploader = (TextUploader)mZScreen.ucTextUploaders.MyCollection.SelectedItem;
            if (!string.IsNullOrEmpty(localFilePath))
            {
                t.SetLocalFilePath(localFilePath);
            }

            switch (job)
            {
                case WorkerTask.Jobs.LANGUAGE_TRANSLATOR:
                    mZScreen.btnTranslate.Enabled = false;
                    t.TranslationInfo = new GoogleTranslate.TranslationInfo(mZScreen.txtTranslateText.Text, ZScreen.mGTranslator.LanguageOptions.SourceLangList[mZScreen.cbFromLanguage.SelectedIndex],
                        ZScreen.mGTranslator.LanguageOptions.TargetLangList[mZScreen.cbToLanguage.SelectedIndex]);
                    if (t.TranslationInfo.IsEmpty())
                    {
                        mZScreen.btnTranslate.Enabled = true;
                    }
                    break;
            }
            return t;
        }

        public void CaptureWebpage(WorkerTask task)
        {
            if (task != null && FileSystem.IsValidLink(task.MyText.LocalString))
            {
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
                webPageCapture.DownloadPage(task.MyText.LocalString);
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
                task.SetLocalFilePath(hi.LocalPath);
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
                    StartBW_SelectedWindow();
                    break;
                case WorkerTask.Jobs.TakeScreenshotCropped:
                    StartBW_CropShot();
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

        public void LanguageTranslator(ref WorkerTask t)
        {
            t.TranslationInfo.Result = ZScreen.mGTranslator.TranslateText(t.TranslationInfo);
        }

        public void StartWorkerTranslator()
        {
            if (Clipboard.ContainsText())
            {
                StartBW_LanguageTranslator(new GoogleTranslate.TranslationInfo(Clipboard.GetText(),
                    GoogleTranslate.FindLanguage(Program.conf.FromLanguage, ZScreen.mGTranslator.LanguageOptions.SourceLangList),
                    GoogleTranslate.FindLanguage(Program.conf.ToLanguage, ZScreen.mGTranslator.LanguageOptions.TargetLangList)));
            }
        }

        /// <summary>
        /// Worker for Images: Drag n Drop, Image from Clipboard, Custom Uploader
        /// </summary>
        /// <param name="job">Job Type</param>
        /// <param name="localFilePath">Local file path of the image</param>
        public void StartWorkerPictures(WorkerTask t, string localFilePath)
        {
            t.JobCategory = JobCategoryType.PICTURES;
            t.MakeTinyURL = Adapter.MakeTinyURL();
            t.SetImage(localFilePath);
            t.MyWorker.RunWorkerAsync(t);
        }

        public void StartWorkerPictures(WorkerTask.Jobs job, Image img)
        {
            WorkerTask t = CreateTask(job);
            t.JobCategory = JobCategoryType.PICTURES;
            t.MakeTinyURL = Adapter.MakeTinyURL();
            t.SetImage(img);
            new TaskManager(ref t).WriteImage();
            t.MyWorker.RunWorkerAsync(t);
        }

        /// <summary>
        /// Worker for Screenshots: Active Window, Crop, Entire Screen
        /// </summary>
        /// <param name="job">Job Type</param>
        public void StartWorkerScreenshots(WorkerTask.Jobs job)
        {
            WorkerTask t = CreateTask(job);
            t.JobCategory = JobCategoryType.SCREENSHOTS;
            t.MakeTinyURL = Adapter.MakeTinyURL();
            t.MyWorker.RunWorkerAsync(t);
        }

        /// <summary>
        /// Worker for Binary: Drag n Drop, Clipboard Upload files from Explorer
        /// </summary>
        /// <param name="job">Job Type</param>
        /// <param name="localFilePath">Local file path of the file</param>
        private void StartWorkerBinary(WorkerTask.Jobs job, string localFilePath)
        {
            WorkerTask t = CreateTask(job);
            t.JobCategory = JobCategoryType.BINARY;
            t.MakeTinyURL = Adapter.MakeTinyURL();
            t.SetLocalFilePath(localFilePath);
            t.MyWorker.RunWorkerAsync(t);
        }

        private void PublishBinary(ref WorkerTask task)
        {
            task.StartTime = DateTime.Now;
            TaskManager tm = new TaskManager(ref task);
            tm.UploadFile();
            task.EndTime = DateTime.Now;
        }


        /// <summary>
        /// Function to edit Text in a Text Editor and Upload
        /// </summary>
        /// <param name="task"></param>
        private void PublishText(ref WorkerTask task)
        {
            TaskManager tm = new TaskManager(ref task);
            tm.UploadText();
        }

        private void ScreenshotUsingDragDrop(string fp)
        {
            StartWorkerPictures(CreateTask(WorkerTask.Jobs.PROCESS_DRAG_N_DROP), fp);
        }

        private void ScreenshotUsingDragDrop(string[] paths)
        {
            foreach (string filePath in FileSystem.GetExplorerFileList(paths))
            {
                File.Copy(filePath, FileSystem.GetUniqueFilePath(Path.Combine(
                    Program.ImagesDir, Path.GetFileName(filePath))), true);
                ScreenshotUsingDragDrop(filePath);
            }
        }

        public void ScreenshotUsingHotkeys(object sender, KeyEventArgs e)
        {
            if (mSetHotkeys)
            {
                if (e.KeyData == Keys.Enter)
                {
                    QuitSettingHotkeys();
                }
                else if (e.KeyData == Keys.Escape)
                {
                    mZScreen.dgvHotkeys.Rows[mHKSelectedRow].Cells[1].Value = Keys.None;
                    SetHotkey(mHKSelectedRow, Keys.None);
                }
                else
                {
                    mZScreen.dgvHotkeys.Rows[mHKSelectedRow].Cells[1].Value = e.KeyData.ToSpecialString();
                    SetHotkey(mHKSelectedRow, e.KeyData);
                }
            }
            else
            {
                e.Handled = CheckHotkeys(e.KeyData);
            }
        }

        private bool CheckHotkeys(Keys key)
        {
            if (Program.conf.HotkeyEntireScreen == key) //Entire Screen
            {
                StartBW_EntireScreen();
                return true;
            }
            if (Program.conf.HotkeyActiveWindow == key) //Active Window
            {
                StartBW_ActiveWindow();
                return true;
            }
            if (Program.conf.HotkeySelectedWindow == key) //Selected Window
            {
                StartBW_SelectedWindow();
                return true;
            }
            if (Program.conf.HotkeyCropShot == key) //Crop Shot
            {
                StartBW_CropShot();
                return true;
            }
            if (Program.conf.HotkeyLastCropShot == key) //Last Crop Shot
            {
                StartBW_LastCropShot();
                return true;
            }
            if (Program.conf.HotkeyAutoCapture == key) //Auto Capture
            {
                ShowAutoCapture();
                return true;
            }
            if (Program.conf.HotkeyClipboardUpload == key) //Clipboard Upload
            {
                UploadUsingClipboard();
                return true;
            }
            if (Program.conf.HotkeyDropWindow == key) //Drag & Drop Window
            {
                ShowDropWindow();
                return true;
            }
            if (Program.conf.HotkeyActionsToolbar == key) //Actions Toolbar
            {
                ShowActionsToolbar(true);
                return true;
            }
            if (Program.conf.HotkeyQuickOptions == key) //Quick Options
            {
                ShowQuickOptions();
                return true;
            }
            if (Program.conf.HotkeyLanguageTranslator == key) //Language Translator
            {
                StartWorkerTranslator();
                return true;
            }
            if (Program.conf.HotkeyScreenColorPicker == key) //Screen Color Picker
            {
                ScreenColorPicker();
                return true;
            }
            return false;
        }

        public void QuitSettingHotkeys()
        {
            if (mSetHotkeys)
            {
                mSetHotkeys = false;

                mZScreen.lblHotkeyStatus.Text = GetSelectedHotkeyName() + " Hotkey Updated.";

                //reset hotkey text from <set keys> back to normal
                mZScreen.dgvHotkeys.Rows[mHKSelectedRow].Cells[1].Value = GetSelectedHotkeySpecialString();

                mHKSelectedRow = -1;
            }
        }

        public string GetSelectedHotkeyName()
        {
            return mZScreen.dgvHotkeys.Rows[mHKSelectedRow].Cells[0].Value.ToString();
        }

        public string GetSelectedHotkeySpecialString()
        {
            object obj = Program.conf.GetFieldValue("Hotkey" + GetSelectedHotkeyName().Replace(" ", ""));
            if (obj != null && obj.GetType() == typeof(Keys))
            {
                return ((Keys)obj).ToSpecialString();
            }

            return "Error getting hotkey";
        }

        private void SetHotkey(int row, Keys key)
        {
            SetHotkey(mZScreen.dgvHotkeys.Rows[mHKSelectedRow].Cells[0].Value.ToString(), key);

            mZScreen.lblHotkeyStatus.Text = mZScreen.dgvHotkeys.Rows[mHKSelectedRow].Cells[0].Value + " Hotkey set to: " + key.ToSpecialString() +
                ". Press enter when done setting all desired Hotkeys.";
        }

        private bool SetHotkey(string name, Keys key)
        {
            return Program.conf.SetFieldValue("Hotkey" + name.Replace(" ", ""), key);
        }

        public bool UploadUsingFileSystem(List<string> fileList)
        {
            List<string> strListFilePath = new List<string>();
            bool succ = true;
            foreach (string fp in fileList)
            {
                try
                {
                    if (GraphicsMgr.IsValidImage(fp))
                    {
                        string cbFilePath = FileSystem.GetUniqueFilePath(Path.Combine(Program.ImagesDir, Path.GetFileName(fp)));
                        if (fp != cbFilePath)
                        {
                            File.Copy(fp, cbFilePath, true);
                        }
                        strListFilePath.Add(cbFilePath);
                    }
                    else
                    {
                        strListFilePath.Add(fp); // yes we use the orignal file path
                    }
                }
                catch (Exception ex)
                {
                    FileSystem.AppendDebug(ex.ToString());
                    succ = false;
                }
            }

            List<WorkerTask> textWorkers = new List<WorkerTask>();

            foreach (string fp in strListFilePath)
            {
                if (GraphicsMgr.IsValidImage(fp))
                {
                    StartWorkerPictures(CreateTask(WorkerTask.Jobs.UploadFromClipboard), fp);
                }
                else if (FileSystem.IsValidTextFile(fp))
                {
                    WorkerTask temp = GetWorkerText(WorkerTask.Jobs.UploadFromClipboard);
                    temp.SetLocalFilePath(fp);
                    temp.MyText = TextInfo.FromFile(fp);
                    textWorkers.Add(temp);
                }
                else
                {
                    StartWorkerBinary(WorkerTask.Jobs.UploadFromClipboard, fp);
                }
            }

            StartTextWorkers(textWorkers);
            return succ;
        }

        public void UploadUsingClipboard()
        {
            if (Clipboard.ContainsText() && Program.conf.AutoTranslate && Clipboard.GetText().Length <= Program.conf.AutoTranslateLength)
            {
                StartWorkerTranslator();
            }
            else
            {
                List<WorkerTask> textWorkers = new List<WorkerTask>();

                if (Clipboard.ContainsImage())
                {
                    Image cImage = Clipboard.GetImage();
                    WorkerTask task = CreateTask(WorkerTask.Jobs.UploadFromClipboard);
                    task.Settings.ManualNaming = false;
                    task.SetFilePath(NameParser.Convert(new NameParserInfo(NameParserType.EntireScreen)));
                    FileSystem.SaveImage(cImage, task.LocalFilePath);
                    StartWorkerPictures(task, task.LocalFilePath);
                }
                else if (Clipboard.ContainsText())
                {
                    WorkerTask temp = GetWorkerText(WorkerTask.Jobs.UploadFromClipboard);
                    string fp = FileSystem.GetUniqueFilePath(Path.Combine(Program.TextDir,
                        NameParser.Convert(new NameParserInfo("%y.%mo.%d-%h.%mi.%s")) + ".txt"));
                    File.WriteAllText(fp, Clipboard.GetText());
                    temp.SetLocalFilePath(fp);
                    temp.MyText = TextInfo.FromFile(fp);
                    textWorkers.Add(temp);
                }
                else if (Clipboard.ContainsFileDropList())
                {
                    this.UploadUsingFileSystem(FileSystem.GetExplorerFileList(Clipboard.GetFileDropList()));
                }

                this.StartTextWorkers(textWorkers);
            }
        }

        private void StartTextWorkers(List<WorkerTask> textWorkers)
        {
            foreach (WorkerTask task in textWorkers)
            {
                if (FileSystem.IsValidLink(task.MyText.LocalString) && Program.conf.AutoShortenURL && Adapter.CheckURLShorteners())
                {
                    task.MyTextUploader = Program.conf.UrlShortenersList[Program.conf.UrlShortenerSelected];
                    task.RunWorker();
                }
                else if (Directory.Exists(task.MyText.LocalString)) // McoreD: can make this an option later
                {
                    IndexerAdapter settings = new IndexerAdapter();
                    settings.LoadConfig(Program.conf.IndexerConfig);
                    Program.conf.IndexerConfig.FolderList.Clear();
                    string ext = (task.MyTextUploader.GetType() == typeof(FTPUploader)) ? ".html" : ".log";
                    string fileName = Path.GetFileName(task.MyText.LocalString) + ext;
                    settings.GetConfig().SetSingleIndexPath(Path.Combine(Program.TextDir, fileName));
                    settings.GetConfig().FolderList.Add(task.MyText.LocalString);

                    Indexer indexer = null;
                    switch (settings.GetConfig().IndexingEngineType)
                    {
                        case IndexingEngine.TreeLib:
                            indexer = new TreeWalkIndexer(settings);
                            break;
                        case IndexingEngine.TreeNetLib:
                            indexer = new TreeNetIndexer(settings);
                            break;
                    }
                    if (indexer != null)
                    {
                        indexer.IndexNow(IndexingMode.IN_ONE_FOLDER_MERGED);
                        task.MyText = null; // force to upload from file
                        task.SetLocalFilePath(settings.GetConfig().GetIndexFilePath());
                        task.RunWorker();
                    }
                }
                else if (Adapter.CheckTextUploaders())
                {
                    task.RunWorker();
                }
            }
        }

        private bool RetryUpload(WorkerTask t)
        {
            if (Program.conf.ImageUploadRetryOnFail && t.IsImage && t.Errors.Count > 0 && !t.Retry &&
                (t.MyImageUploader == ImageDestType.IMAGESHACK || t.MyImageUploader == ImageDestType.TINYPIC))
            {
                WorkerTask task = CreateTask(WorkerTask.Jobs.UPLOAD_IMAGE);
                task.JobCategory = t.JobCategory;
                task.SetImage(t.LocalFilePath);
                task.SetLocalFilePath(t.LocalFilePath);
                if (t.MyImageUploader == ImageDestType.IMAGESHACK)
                {
                    task.MyImageUploader = ImageDestType.TINYPIC;
                }
                else
                {
                    task.MyImageUploader = ImageDestType.IMAGESHACK;
                }
                task.Retry = true;

                string message = string.Format("{0}\r\n\r\nAutomatically starting upload with {1}.", string.Join("\r\n", t.Errors.ToArray()),
                    task.MyImageUploader.GetDescription());
                mZScreen.niTray.ShowBalloonTip(5000, Application.ProductName, message, ToolTipIcon.Warning);
                task.MyWorker.RunWorkerAsync(task);
                return true;
            }
            return false;
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

        public void StartBW_SelectedWindow()
        {
            if (!TaskManager.mTakingScreenShot)
            {
                StartWorkerScreenshots(WorkerTask.Jobs.TakeScreenshotWindowSelected);
            }
        }

        public void StartBW_CropShot()
        {
            if (!TaskManager.mTakingScreenShot)
            {
                StartWorkerScreenshots(WorkerTask.Jobs.TakeScreenshotCropped);
            }
        }

        public void StartBW_LastCropShot()
        {
            StartWorkerScreenshots(WorkerTask.Jobs.TAKE_SCREENSHOT_LAST_CROPPED);
        }

        #endregion

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
                actionsToolbar.Location = Program.conf.ActionToolbarLocation;
                actionsToolbar.EventJob += new JobsEventHandler(EventJobs);
                actionsToolbar.FormClosed += new FormClosedEventHandler(quickActions_FormClosed);
                actionsToolbar.Show();
                if (manual)
                {
                    actionsToolbar.Show();
                    Rectangle taskbar = User32.GetTaskbarRectangle();
                    actionsToolbar.Location = new Point(SystemInformation.PrimaryMonitorSize.Width - actionsToolbar.Width - 100,
                        SystemInformation.PrimaryMonitorSize.Height - taskbar.Height - actionsToolbar.Height - 10);
                }
            }
        }

        private void quickActions_FormClosed(object sender, FormClosedEventArgs e)
        {
            bQuickActionsOpened = false;
        }

        #endregion

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
                Rectangle taskbar = User32.GetTaskbarRectangle();
                quickOptions.Location = new Point(SystemInformation.PrimaryMonitorSize.Width - quickOptions.Width - 100,
                    SystemInformation.PrimaryMonitorSize.Height - taskbar.Height - quickOptions.Height - 10);
            }
        }

        private void QuickOptionsApplySettings(object sender, EventArgs e)
        {
            mZScreen.ucDestOptions.cboImageUploaders.SelectedIndex = (int)Program.conf.ScreenshotDestMode;
            mZScreen.cboClipboardTextMode.SelectedIndex = (int)Program.conf.ClipboardUriMode;
        }

        private void QuickOptionsFormClosed(object sender, FormClosedEventArgs e)
        {
            bQuickOptionsOpened = false;
        }

        #endregion

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

        public void ScreenColorPicker()
        {
            DialogColor dialogColor = new DialogColor { ScreenPicker = true };
            dialogColor.Show();
        }

        #region "Translate"

        public void TranslateTo1()
        {
            if (Program.conf.ToLanguage2 == "?")
            {
                mZScreen.lblToLanguage.BorderStyle = BorderStyle.FixedSingle;
                MessageBox.Show("Drag n drop 'To:' label to this button for be able to set button language.", mZScreen.Text,
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                mZScreen.lblToLanguage.BorderStyle = BorderStyle.None;
            }
            else
            {
                StartBW_LanguageTranslator(new GoogleTranslate.TranslationInfo(mZScreen.txtTranslateText.Text,
                     GoogleTranslate.FindLanguage(Program.conf.FromLanguage, ZScreen.mGTranslator.LanguageOptions.SourceLangList),
                     GoogleTranslate.FindLanguage(Program.conf.ToLanguage2, ZScreen.mGTranslator.LanguageOptions.TargetLangList)));
            }
        }

        #endregion

        #region "ZScreen GUI"

        public void CheckHistoryItems()
        {
            if (mZScreen.lbHistory.Items.Count > Program.conf.HistoryMaxNumber)
            {
                for (int i = mZScreen.lbHistory.Items.Count - 1; i >= Program.conf.HistoryMaxNumber; i--)
                {
                    mZScreen.lbHistory.Items.RemoveAt(i);
                }
            }
            UpdateGuiControlsHistory();
        }

        public void SaveHistoryItems()
        {
            if (Program.conf.HistorySave)
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
            mZScreen.tpHistoryList.Text = string.Format("History List ({0}/{1})", mZScreen.lbHistory.Items.Count, Program.conf.HistoryMaxNumber);
        }

        #endregion
    }
}
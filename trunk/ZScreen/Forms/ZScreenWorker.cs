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
using System.Media;
using System.Threading;
using System.Windows.Forms;
using GraphicsMgrLib;
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
using ZSS.IndexersLib;
using ZUploader.HelperClasses;

namespace ZScreenGUI
{
    public partial class ZScreen : Form
    {
        internal bool mSetHotkeys, bAutoScreenshotsOpened, bDropWindowOpened;
        internal HotkeyMgr mHotkeyMgr = null;

        #region Worker Events

        public BackgroundWorker CreateWorker()
        {
            BackgroundWorker bwApp = new BackgroundWorker { WorkerReportsProgress = true };
            bwApp.DoWork += new System.ComponentModel.DoWorkEventHandler(BwApp_DoWork);
            bwApp.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(BwApp_ProgressChanged);
            bwApp.RunWorkerCompleted += new RunWorkerCompletedEventHandler(BwApp_RunWorkerCompleted);
            return bwApp;
        }

        public void BwApp_DoWork(object sender, DoWorkEventArgs e)
        {
            WorkerTask bwTask = (WorkerTask)e.Argument;
            if (bwTask.Status != WorkerTask.TaskStatus.Prepared)
            {
                bwTask.PrepareTask(ucDestOptions);
            }
            bwTask.Status = WorkerTask.TaskStatus.ThreadMode;

            if (!bwTask.CanStartWork())
            {
                e.Result = null; // Pass a null object because there is nothing else to do
            }
            else
            {
                bwTask.MyWorker.ReportProgress((int)WorkerTask.ProgressType.SET_ICON_BUSY, bwTask);
                bwTask.UniqueNumber = UploadManager.Queue();

                if (Engine.conf.PromptForUpload && !bwTask.MyClipboardContent.Contains(ClipboardContentEnum.Data) &&
                    !bwTask.MyClipboardContent.Contains(ClipboardContentEnum.Local) &&
                    (bwTask.Job2 == WorkerTask.JobLevel2.CaptureEntireScreen ||
                    bwTask.Job2 == WorkerTask.JobLevel2.CaptureActiveWindow) &&
                    MessageBox.Show("Do you really want to upload to " + bwTask.GetActiveImageUploadersDescription() + "?",
                    Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                {
                    e.Result = bwTask;
                    return;
                }

                if (bwTask.WasToTakeScreenshot)
                {
                    if (Engine.conf.ScreenshotDelayTime > 0)
                    {
                        Thread.Sleep((int)Engine.conf.ScreenshotDelayTime);
                    }
                }

                Engine.MyLogger.WriteLine(string.Format("Job started: {0}", bwTask.Job2));

                switch (bwTask.Job1)
                {
                    case JobLevel1.Image:
                    case JobLevel1.File:
                        switch (bwTask.Job2)
                        {
                            case WorkerTask.JobLevel2.CaptureEntireScreen:
                                if (bwTask.tempImage == null)
                                {
                                    bwTask.CaptureScreen();
                                }
                                break;
                            case WorkerTask.JobLevel2.CaptureActiveWindow:
                                if (bwTask.tempImage == null)
                                {
                                    bwTask.CaptureActiveWindow();
                                }
                                break;
                            case WorkerTask.JobLevel2.CaptureSelectedWindow:
                            case WorkerTask.JobLevel2.CaptureRectRegion:
                                bwTask.BwCaptureRegionOrWindow();
                                bwTask.WriteImage();
                                break;
                            case WorkerTask.JobLevel2.CaptureFreeHandRegion:
                                bwTask.BwCaptureFreehandCrop();
                                bwTask.WriteImage();
                                break;
                        }
                        bwTask.PublishData();
                        break;
                    case JobLevel1.Text:
                        switch (bwTask.Job2)
                        {
                            case WorkerTask.JobLevel2.UploadFromClipboard:
                                bwTask.UploadText();
                                break;
                            case WorkerTask.JobLevel2.LANGUAGE_TRANSLATOR:
                                bwTask.SetTranslationInfo(new GoogleTranslate(ZKeys.GoogleTranslateKey).TranslateText(bwTask.TranslationInfo));
                                bwTask.SetText(bwTask.TranslationInfo.Result);
                                break;
                        }
                        break;
                }

                if (bwTask.UploadResults.Count > 0)
                {
                    foreach (UploadResult ur in bwTask.UploadResults)
                    {
                        if (bwTask.ShouldShortenURL(ur.URL))
                        {
                            bwTask.ShortenURL(ur, ur.URL);
                        }
                    }
                }

                e.Result = bwTask;
            }
        }

        private void BwApp_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            switch ((WorkerTask.ProgressType)e.ProgressPercentage)
            {
                case (WorkerTask.ProgressType)101:
                    Adapter.PrintImage(e.UserState as Image);
                    break;
                case WorkerTask.ProgressType.PrintText:
                    Adapter.PrintText(e.UserState as string);
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
                    Adapter.FlashNotifyIcon(this.niTray, e.UserState as Icon);
                    break;
                case WorkerTask.ProgressType.SET_ICON_BUSY:
                    Adapter.SetNotifyIconStatus(e.UserState as WorkerTask, this.niTray, Resources.zss_busy);
                    break;
                case WorkerTask.ProgressType.UpdateCropMode:
                    this.cboCropGridMode.Checked = Engine.conf.CropGridToggle;
                    break;
                case WorkerTask.ProgressType.CHANGE_TRAY_ICON_PROGRESS:
                    UploadManager.SetCumulativePercentatge((int)((ProgressManager)e.UserState).Percentage);
                    Adapter.UpdateNotifyIconProgress(this.niTray, UploadManager.CumulativePercentage);
                    Adapter.TaskbarSetProgressValue(this, UploadManager.CumulativePercentage);
                    this.Text = string.Format("{0}% - {1}", UploadManager.CumulativePercentage, Engine.GetProductName());
                    break;
                case WorkerTask.ProgressType.UPDATE_PROGRESS_MAX:
                    TaskbarProgressBarState tbps = (TaskbarProgressBarState)e.UserState;
                    Adapter.TaskbarSetProgressState(this, tbps);
                    break;
                case WorkerTask.ProgressType.ShowTrayWarning:
                    Adapter.TaskbarSetProgressValue(this, 33);
                    Adapter.TaskbarSetProgressState(this, TaskbarProgressBarState.Error);
                    Adapter.SetNotifyIconBalloonTip(this.niTray, this.Text, e.UserState as string, ToolTipIcon.Warning);
                    break;
            }

            rtbDebugLog.Text = Engine.MyLogger.ToString();
        }

        private void BwApp_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            WorkerTask task = (WorkerTask)e.Result;
            if (task == null) return;

            this.Text = Engine.GetProductName();
            if (task.UploadResults.Count > 0)
            {
                UploadManager.UploadResultLast = task.UploadResults[task.UploadResults.Count - 1];
                UploadManager.ResetCumulativePercentage();
            }

            try
            {
                if (task.IsError && task.Errors[0].Contains(ExceptionMessage.ProxyAuthenticationRequired))
                {
                    ProxyConfig pc = new ProxyConfig();
                    if (pc.ShowDialog() == DialogResult.OK)
                    {
                        this.ProxyAdd(pc.Proxy);
                        this.cboProxyConfig.SelectedIndex = (int)ProxyConfigType.ManualProxy;
                        Uploader.ProxySettings = Adapter.CheckProxySettings();
                    }
                    RetryTask(task);
                }

                WorkerTask checkTask = RetryUpload(task);

                if (checkTask.Status == WorkerTask.TaskStatus.RetryPending)
                {
                    string message = string.Format("{0}\r\n\r\nAutomatically retrying upload for {1}.", string.Join("\r\n", task.Errors.ToArray()), checkTask.GetActiveImageUploadersDescription());
                    this.niTray.ShowBalloonTip(5000, Application.ProductName, message, ToolTipIcon.Warning);
                }
                else
                {
                    task.Status = WorkerTask.TaskStatus.Finished;
                    Engine.MyLogger.WriteLine(string.Format("Job completed: {0}", task.Job2));

                    if (task.MyClipboardContent.Contains(ClipboardContentEnum.Local) && Engine.conf.ShowSaveFileDialogImages)
                    {
                        string fp = Adapter.SaveImage(task.tempImage);
                        if (!string.IsNullOrEmpty(fp))
                        {
                            task.UpdateLocalFilePath(fp);
                        }
                    }

                    switch (task.Job1)
                    {
                        case JobLevel1.Text:
                            if (task.Job2 == WorkerTask.JobLevel2.LANGUAGE_TRANSLATOR)
                            {
                                UpdateGoogleTranslateGUI(task.TranslationInfo);

                                Loader.MyGTGUI.btnTranslate.Enabled = true;
                                Loader.MyGTGUI.btnTranslateTo.Enabled = true;
                            }
                            break;
                        case JobLevel1.Image:
                            if (!task.MyClipboardContent.Contains(ClipboardContentEnum.Local) && Engine.conf.DeleteLocal && File.Exists(task.LocalFilePath))
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

                    if (!Engine.conf.ClipboardOverwrite && !Clipboard.ContainsFileDropList() && !Clipboard.ContainsImage() && !Clipboard.ContainsText() || Engine.conf.ClipboardOverwrite)
                    {
                        UploadManager.ShowUploadResults(this.Handle, task, false);
                    }

                    if (Engine.conf.TwitterEnabled)
                    {
                        Adapter.TwitterMsg(task);
                    }

                    bool bLastSourceButtonsEnabled = task.UploadResults.Count > 0 && !string.IsNullOrEmpty(task.UploadResults[task.UploadResults.Count - 1].Source);
                    this.btnOpenSourceText.Enabled = bLastSourceButtonsEnabled;
                    this.btnOpenSourceBrowser.Enabled = bLastSourceButtonsEnabled;
                    this.btnOpenSourceString.Enabled = bLastSourceButtonsEnabled;

                    if (UploadManager.UploadInfoList.Count > 1)
                    {
                        this.niTray.Icon = Resources.zss_busy;
                    }
                    else
                    {
                        this.niTray.Text = this.Text; // do not update notifyIcon text if there are other jobs active
                        this.niTray.Icon = Resources.zss_tray;
                    }

                    if (task.UploadResults.Count > 0 || File.Exists(task.LocalFilePath) || task.Job2 == WorkerTask.JobLevel2.LANGUAGE_TRANSLATOR)
                    {
                        if (Engine.conf.CompleteSound)
                        {
                            SystemSounds.Exclamation.Play();
                        }

                        if (Engine.conf.ShowBalloonTip)
                        {
                            new BalloonTipHelper(this.niTray, task).ShowBalloonTip();
                        }
                    }

                    if (task.IsError)
                    {
                        foreach (string error in task.Errors)
                        {
                            Engine.MyLogger.WriteLine(error);
                        }
                        niTray.ShowBalloonTip(5000, Application.ProductName, niTray.BalloonTipText + Environment.NewLine + task.Errors[task.Errors.Count - 1], ToolTipIcon.Error);
                    }
                }

                if (task.tempImage != null)
                {
                    task.tempImage.Dispose(); // For fix memory leak
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
                    Adapter.TaskbarSetProgressState(this, TaskbarProgressBarState.NoProgress);
                }
            }

            rtbDebugLog.Text = Engine.MyLogger.ToString();
        }

        #endregion Worker Events

        #region Create Worker

        public WorkerTask CreateTask(WorkerTask.JobLevel2 job)
        {
            return new WorkerTask(CreateWorker(), job);
        }

        /// <summary>
        /// Worker for Text: Paste2, Pastebin
        /// </summary>
        /// <returns></returns>
        public WorkerTask CreateTaskText(WorkerTask.JobLevel2 job, string localFilePath)
        {
            WorkerTask task = CreateTask(job);
            foreach (TextUploaderType ut in Engine.conf.MyTextUploaders)
            {
                task.MyTextUploaders.Add((TextUploaderType)ut);
            }
            if (!string.IsNullOrEmpty(localFilePath))
            {
                task.UpdateLocalFilePath(localFilePath);
            }

            switch (job)
            {
                case WorkerTask.JobLevel2.LANGUAGE_TRANSLATOR:
                    Loader.MyGTGUI.btnTranslate.Enabled = false;
                    task.SetTranslationInfo(new GoogleTranslateInfo()
                    {
                        Text = Loader.MyGTGUI.txtTranslateText.Text,
                        SourceLanguage = Engine.MyGTConfig.GoogleSourceLanguage,
                        TargetLanguage = Engine.MyGTConfig.GoogleTargetLanguage
                    });

                    break;
            }

            return task;
        }

        #endregion Create Worker

        #region Hotkeys

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

        public string GetSelectedHotkeyName()
        {
            return this.dgvHotkeys.Rows[HotkeyMgr.mHKSelectedRow].Cells[0].Value.ToString();
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

        private bool UploadUsingHotkeys(Keys key)
        {
            // Fix for Issue 23 - Media Center was triggering Keys.None
            if (key != Keys.None)
            {
                WorkerTask hkTask = new WorkerTask(CreateWorker());
                hkTask.PrepareTask(ucDestOptions);

                if (Engine.conf.HotkeyEntireScreen == key) // Entire Screen
                {
                    hkTask.AssignJob(WorkerTask.JobLevel2.CaptureEntireScreen);
                    hkTask.CaptureScreen();
                    hkTask.WriteImage();
                    RunWorkerAsync_EntireScreen(hkTask);
                    return true;
                }

                if (Engine.conf.HotkeyActiveWindow == key) // Active Window
                {
                    hkTask.AssignJob(WorkerTask.JobLevel2.CaptureActiveWindow);
                    hkTask.CaptureActiveWindow();
                    hkTask.WriteImage();
                    RunWorkerAsync_ActiveWindow(hkTask);
                    return true;
                }

                if (Engine.conf.HotkeySelectedWindow == key) // Selected Window
                {
                    hkTask.AssignJob(WorkerTask.JobLevel2.CaptureSelectedWindow);
                    RunWorkerAsync_SelectedWindow(hkTask);
                    return true;
                }

                if (Engine.conf.HotkeyCropShot == key) // Crop Shot
                {
                    hkTask.AssignJob(WorkerTask.JobLevel2.CaptureRectRegion);
                    RunWorkerAsync_CropShot(hkTask);
                    return true;
                }

                if (Engine.conf.HotkeyLastCropShot == key) // Last Crop Shot
                {
                    hkTask.AssignJob(WorkerTask.JobLevel2.CaptureLastCroppedWindow);
                    RunWorkerAsync_LastCropShot(hkTask);
                    return true;
                }

                if (Engine.conf.HotkeyFreehandCropShot == key) // Freehand Crop Shot
                {
                    hkTask.AssignJob(WorkerTask.JobLevel2.CaptureFreeHandRegion);
                    RunWorkerAsync_FreehandCropShot(hkTask);
                    return true;
                }

                if (Engine.conf.HotkeyAutoCapture == key) // Auto Capture
                {
                    ShowAutoCapture();
                    return true;
                }

                if (Engine.conf.HotkeyClipboardUpload == key) // Clipboard Upload
                {
                    UploadUsingClipboardOrGoogleTranslate();
                    return true;
                }

                if (Engine.conf.HotkeyDropWindow == key) // Drag & Drop Window
                {
                    ShowDropWindow();
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
                    this.lblHotkeyStatus.Text = GetSelectedHotkeyName() + " Hotkey Updated.";
                    //reset hotkey text from <set keys> back to normal
                    this.dgvHotkeys.Rows[HotkeyMgr.mHKSelectedRow].Cells[1].Value = GetSelectedHotkeySpecialString();
                }
                HotkeyMgr.mHKSelectedRow = -1;
            }
        }

        #endregion Hotkeys

        #region Google Translate

        public void StartWorkerTranslator()
        {
            if (Clipboard.ContainsText())
            {
                RunWorkerAsync_LanguageTranslator(new GoogleTranslateInfo()
                {
                    Text = Clipboard.GetText(),
                    SourceLanguage = Engine.MyGTConfig.GoogleSourceLanguage,
                    TargetLanguage = Engine.MyGTConfig.GoogleTargetLanguage
                });
            }
        }

        private void UpdateGoogleTranslateGUI(GoogleTranslateInfo info)
        {
            Loader.MyGTGUI.txtTranslateText.Text = info.Text;
            Loader.MyGTGUI.txtLanguages.Text = info.SourceLanguage + " -> " + info.TargetLanguage;
            Loader.MyGTGUI.txtTranslateResult.Text = info.Result;
        }

        #endregion Google Translate

        #region RunWorkerAsync Job 1

        /// <summary>
        /// Worker for Screenshots: Active Window, Crop, Entire Screen
        /// </summary>
        /// <param name="job">Job Type</param>
        public void RunWorkerAsync_Screenshots(WorkerTask task)
        {
            Engine.ClipboardUnhook();
            task.WasToTakeScreenshot = true;
            task.MyWorker.RunWorkerAsync(task);
        }

        /// <summary>
        /// Worker for Images: Drag n Drop, Image from Clipboard, Custom Uploader
        /// </summary>
        /// <param name="job">Job Type</param>
        /// <param name="localFilePath">Local file path of the image</param>
        public void RunWorkerAsync_Pictures(WorkerTask task)
        {
            Engine.ClipboardUnhook();
            task.UpdateLocalFilePath(task.LocalFilePath);
            task.SetImage(task.LocalFilePath);
            task.MyWorker.RunWorkerAsync(task);
        }

        /// <summary>
        /// Worker for Binary: Drag n Drop, Clipboard Upload files from Explorer
        /// </summary>
        /// <param name="job">Job Type</param>
        /// <param name="localFilePath">Local file path of the file</param>
        public void RunWorkerAsync_Files(WorkerTask.JobLevel2 job, string localFilePath)
        {
            WorkerTask t = CreateTask(job);
            t.UpdateLocalFilePath(localFilePath);
            t.MyWorker.RunWorkerAsync(t);
        }

        public void RunWorkerAsync_Text(WorkerTask.JobLevel2 job, string text)
        {
            WorkerTask temp = CreateTaskText(job, string.Empty);
            string fp = FileSystem.GetUniqueFilePath(Path.Combine(Engine.TextDir, new NameParser().Convert("%y.%mo.%d-%h.%mi.%s") + ".txt"));
            FileSystem.WriteText(fp, text);
            temp.UpdateLocalFilePath(fp);
            temp.SetText(text);
            temp.RunWorker();
        }

        public void RunWorkerAsync_Text_Batch(List<WorkerTask> textWorkers)
        {
            Engine.ClipboardUnhook();
            foreach (WorkerTask task in textWorkers)
            {
                if (Directory.Exists(task.TempText))
                {
                    IndexerAdapter settings = new IndexerAdapter();
                    settings.LoadConfig(Engine.conf.IndexerConfig);
                    Engine.conf.IndexerConfig.FolderList.Clear();
                    string ext = ".log";
                    if (task.MyTextUploaders.Contains(TextUploaderType.FileUploader))
                    {
                        ext = ".html";
                    }
                    string fileName = Path.GetFileName(task.TempText) + ext;
                    settings.GetConfig().SetSingleIndexPath(Path.Combine(Engine.TextDir, fileName));
                    settings.GetConfig().FolderList.Add(task.TempText);

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
                        task.SetText(null); // force to upload from file
                        task.UpdateLocalFilePath(settings.GetConfig().GetIndexFilePath());
                        task.RunWorker();
                    }
                }
                else
                {
                    task.RunWorker();
                }
            }
        }

        #endregion RunWorkerAsync Job 1

        #region RunWorkerAsync Job 2

        public void RunWorkerAsync_LanguageTranslator(GoogleTranslateInfo translationInfo)
        {
            WorkerTask t = CreateTask(WorkerTask.JobLevel2.LANGUAGE_TRANSLATOR);
            if (Loader.MyGTGUI == null)
            {
                Loader.MyGTGUI = new GoogleTranslateGUI(Engine.MyGTConfig, ZKeys.GetAPIKeys());
            }
            Loader.MyGTGUI.btnTranslate.Enabled = false;
            Loader.MyGTGUI.btnTranslateTo.Enabled = false;
            t.SetTranslationInfo(translationInfo);
            t.MyWorker.RunWorkerAsync(t);
        }

        public void RunWorkerAsync_EntireScreen(WorkerTask task)
        {
            RunWorkerAsync_Screenshots(task);
        }

        public void RunWorkerAsync_ActiveWindow(WorkerTask task)
        {
            RunWorkerAsync_Screenshots(task);
        }

        public void RunWorkerAsync_SelectedWindow(WorkerTask task)
        {
            if (!task.IsTakingScreenShot)
            {
                RunWorkerAsync_Screenshots(task);
            }
        }

        public void RunWorkerAsync_LastCropShot(WorkerTask task)
        {
            RunWorkerAsync_Screenshots(task);
        }

        public void RunWorkerAsync_CropShot(WorkerTask task)
        {
            if (!task.IsTakingScreenShot)
            {
                RunWorkerAsync_Screenshots(task);
            }
        }

        public void RunWorkerAsync_FreehandCropShot(WorkerTask task)
        {
            if (!task.IsTakingScreenShot)
            {
                RunWorkerAsync_Screenshots(task);
            }
        }

        public void UploadUsingClipboardOrGoogleTranslate()
        {
            Engine.ClipboardUnhook();
            if (Clipboard.ContainsText() && Engine.MyGTConfig.AutoTranslate && Clipboard.GetText().Length <= Engine.MyGTConfig.AutoTranslateLength)
            {
                StartWorkerTranslator();
            }
            else
            {
                UploadUsingClipboard();
            }
        }

        public void UploadUsingClipboard()
        {
            WorkerTask cbTask = CreateTask(WorkerTask.JobLevel2.UploadFromClipboard);

            if (Clipboard.ContainsImage())
            {
                cbTask.SetImage(Clipboard.GetImage());
                if (cbTask.SetFilePathFromPattern(new NameParser(NameParserType.EntireScreen).Convert(Engine.conf.EntireScreenPattern)))
                {
                    FileSystem.WriteImage(cbTask);
                }
                RunWorkerAsync_Pictures(cbTask);
            }
            else if (Clipboard.ContainsText())
            {
                RunWorkerAsync_Text(WorkerTask.JobLevel2.UploadFromClipboard, Clipboard.GetText());
            }
            else if (Clipboard.ContainsFileDropList())
            {
                UploadUsingFileSystem(FileSystem.GetExplorerFileList(Clipboard.GetFileDropList()));
            }
        }

        public void UploadUsingDragDrop(string fp)
        {
            WorkerTask ddTask = new WorkerTask(CreateWorker(), WorkerTask.JobLevel2.UploadFromDragDrop);
            ddTask.UpdateLocalFilePath(fp);
            RunWorkerAsync_Pictures(ddTask);
        }

        public void UploadUsingDragDrop(string[] paths)
        {
            List<string> pathsList = new List<string>(paths);
            UploadUsingFileSystem(pathsList);
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
                        string cbFilePath = FileSystem.GetUniqueFilePath(Path.Combine(Engine.ImagesDir, Path.GetFileName(fp)));
                        if (fp != cbFilePath)
                        {
                            File.Copy(fp, cbFilePath, true);
                        }
                        if (Path.GetDirectoryName(fp) == Engine.conf.FolderMonitorPath)
                        {
                            File.Delete(fp);
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
                    Engine.MyLogger.WriteException(ex, "Error while uploading using file system");
                    succ = false;
                }
            }

            List<WorkerTask> textWorkers = new List<WorkerTask>();

            foreach (string fp in strListFilePath)
            {
                if (GraphicsMgr.IsValidImage(fp))
                {
                    WorkerTask cbTask = new WorkerTask(CreateWorker(), WorkerTask.JobLevel2.UploadFromClipboard);
                    cbTask.UpdateLocalFilePath(fp);
                    RunWorkerAsync_Pictures(cbTask);
                }
                else if (FileSystem.IsValidTextFile(fp))
                {
                    WorkerTask temp = CreateTaskText(WorkerTask.JobLevel2.UploadFromClipboard, fp);
                    temp.SetText(File.ReadAllText(fp));
                    textWorkers.Add(temp);
                }
                else
                {
                    RunWorkerAsync_Files(WorkerTask.JobLevel2.UploadFromClipboard, fp);
                }
            }

            RunWorkerAsync_Text_Batch(textWorkers);

            return succ;
        }

        #endregion RunWorkerAsync Job 2

        #region Webpage Capture

        public void CaptureWebpage(WorkerTask task)
        {
            if (task != null && FileSystem.IsValidLink(task.TempText))
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
                webPageCapture.DownloadPage(task.TempText);
            }
        }

        private void webPageCapture_DownloadCompleted(Image img)
        {
            if (img != null)
            {
                Bitmap bmp = new Bitmap(img);
            }
        }

        #endregion Webpage Capture

        #region Auto Capture

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

        internal void EventJobs(object sender, WorkerTask.JobLevel2 job)
        {
            WorkerTask task = new WorkerTask(CreateWorker(), job);
            switch (job)
            {
                case WorkerTask.JobLevel2.CaptureEntireScreen:
                    RunWorkerAsync_EntireScreen(task);
                    break;
                case WorkerTask.JobLevel2.CaptureActiveWindow:
                    RunWorkerAsync_ActiveWindow(task);
                    break;
                case WorkerTask.JobLevel2.CaptureSelectedWindow:
                    RunWorkerAsync_SelectedWindow(task);
                    break;
                case WorkerTask.JobLevel2.CaptureRectRegion:
                    RunWorkerAsync_CropShot(task);
                    break;
                case WorkerTask.JobLevel2.CaptureLastCroppedWindow:
                    RunWorkerAsync_LastCropShot(task);
                    break;
                case WorkerTask.JobLevel2.AUTO_CAPTURE:
                    ShowAutoCapture();
                    break;
                case WorkerTask.JobLevel2.UploadFromClipboard:
                    UploadUsingClipboardOrGoogleTranslate();
                    break;
                case WorkerTask.JobLevel2.UploadFromDragDrop:
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

        #endregion Auto Capture

        #region Drop Window

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

        #endregion Drop Window

        #region Screen Color Picker

        public void ScreenColorPicker()
        {
            DialogColor dialogColor = new DialogColor { ScreenPicker = true };
            dialogColor.Show();
        }

        #endregion Screen Color Picker

        #region Retry Methods

        public void RetryTask(WorkerTask task)
        {
            task.Errors.Clear();
            task.MyWorker = CreateWorker();
            task.PublishData();
        }

        public WorkerTask RetryUpload(WorkerTask task)
        {
            if (task.UploadResults.Count > 0 && task.Job2 != WorkerTask.JobLevel2.LANGUAGE_TRANSLATOR)
            {
                if (!task.MyClipboardContent.Contains(ClipboardContentEnum.Data) && !task.MyClipboardContent.Contains(ClipboardContentEnum.Local) &&
                    string.IsNullOrEmpty(task.UploadResults[0].URL) && Engine.conf.ImageUploadRetryOnFail && task.Status == WorkerTask.TaskStatus.RetryPending && File.Exists(task.LocalFilePath))
                {
                    WorkerTask task2 = CreateTask(WorkerTask.JobLevel2.UploadImage);
                    task2.SetImage(task.LocalFilePath);
                    task2.UpdateLocalFilePath(task.LocalFilePath);
                    task2.Status = WorkerTask.TaskStatus.Finished; // we do not retry again

                    if (task.Job1 == JobLevel1.Image)
                    {
                        if (Engine.conf.ImageUploadRandomRetryOnFail)
                        {
                            List<ImageUploaderType> randomDest = new List<ImageUploaderType>() { ImageUploaderType.IMAGESHACK, ImageUploaderType.TINYPIC, ImageUploaderType.IMGUR };
                            int r = Adapter.RandomNumber(3, 3 + randomDest.Count - 1);
                            while (task.MyImageUploaders.Contains((ImageUploaderType)r))
                            {
                                r = Adapter.RandomNumber(3, 3 + randomDest.Count - 1);
                            }
                            if (!task.MyImageUploaders.Contains((ImageUploaderType)r))
                            {
                                task.MyImageUploaders.Add((ImageUploaderType)r);
                            }
                        }
                        else
                        {
                            if (!task.MyImageUploaders.Contains(ImageUploaderType.TINYPIC))
                            {
                                task.MyImageUploaders.Add((ImageUploaderType.TINYPIC));
                            }
                            else if (!task.MyImageUploaders.Contains(ImageUploaderType.IMAGESHACK))
                            {
                                task.MyImageUploaders.Add((ImageUploaderType.IMAGESHACK));
                            }
                        }
                    }

                    task2.MyWorker.RunWorkerAsync(task2);
                    return task2;
                }
            }
            task.Status = WorkerTask.TaskStatus.Finished;
            return task;
        }

        #endregion Retry Methods

        #region History

        public void AddHistoryItem(WorkerTask task)
        {
            if (Engine.conf.HistorySave)
            {
                foreach (UploadResult ur in task.UploadResults)
                {
                    HistoryManager.AddHistoryItemAsync(Engine.HistoryPath, task.GenerateHistoryItem(ur));
                }
            }

            Adapter.AddRecentItem(task.LocalFilePath);
        }

        #endregion History
    }
}
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
using UploadersLib;
using UploadersLib.HelperClasses;
using UploadersLib.OtherServices;
using ZScreenGUI.Properties;
using ZScreenLib;
using ZSS.ColorsLib;
using ZUploader.HelperClasses;

namespace ZScreenGUI
{
    public partial class ZScreen : ZScreenCoreUI
    {
        internal bool bAutoScreenshotsOpened, bDropWindowOpened;

        #region Worker Events

        public override void BwApp_DoWork(object sender, DoWorkEventArgs e)
        {
            WorkerTask bwTask = (WorkerTask)e.Argument;
            if (bwTask.States.Contains(WorkerTask.TaskState.CancellationPending))
            {
                return;
            }

            bwTask.States.Add(WorkerTask.TaskState.ThreadMode);

            bwTask.Id = UploadManager.Queue();

            if (bwTask.WasToTakeScreenshot)
            {
                if (Engine.ConfigUI.ScreenshotDelayTime > 0)
                {
                    Thread.Sleep((int)Engine.ConfigUI.ScreenshotDelayTime);
                }
            }

            bwTask.PublishData();

            if (bwTask.WorkflowConfig.DestConfig.Outputs.Contains(OutputEnum.RemoteHost) && bwTask.UploadResults.Count > 0)
            {
                foreach (UploadResult ur in bwTask.UploadResults)
                {
                    if (bwTask.ShouldShortenURL(ur.URL) && string.IsNullOrEmpty(ur.ShortenedURL))
                    {
                        bwTask.ShortenURL(ur, ur.URL);
                    }
                }
            }

            e.Result = bwTask;
        }

        public override void BwApp_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            switch ((WorkerTask.ProgressType)e.ProgressPercentage)
            {
                case WorkerTask.ProgressType.PrintImage:
                    Adapter.PrintImage(e.UserState as Image);
                    break;
                case WorkerTask.ProgressType.PrintText:
                    Adapter.PrintText(e.UserState as string);
                    break;
                case (WorkerTask.ProgressType)102:
                    throw new Exception("Unsupported progress");
                case (WorkerTask.ProgressType)103:
                    throw new Exception("Unsupported progress");
                case (WorkerTask.ProgressType)104:
                    Adapter.CopyDataToClipboard(e.UserState);
                    break;
                case WorkerTask.ProgressType.CopyToClipboardImage:
                    if (e.UserState.GetType() == typeof(string))
                    {
                        Adapter.CopyImageToClipboard(e.UserState as string);
                    }
                    else if (e.UserState is Image)
                    {
                        Adapter.CopyImageToClipboard((Image)e.UserState, false);
                    }

                    break;
                case WorkerTask.ProgressType.FlashIcon:
                    Adapter.FlashNotifyIcon(this.niTray, e.UserState as Icon);
                    break;
                case WorkerTask.ProgressType.UpdateCropMode:
                    this.cboCropGridMode.Checked = Engine.ConfigUI.CropGridToggle;
                    break;
                case WorkerTask.ProgressType.ChangeTrayIconProgress:
                    UploadManager.SetCumulativePercentatge((int)((ProgressManager)e.UserState).Percentage);
                    Adapter.UpdateNotifyIconProgress(this.niTray, UploadManager.CumulativePercentage);
                    Adapter.TaskbarSetProgressValue(this, UploadManager.CumulativePercentage);
                    this.Text = string.Format("{0}% - {1}", UploadManager.CumulativePercentage, Engine.GetProductName());
                    break;
                case WorkerTask.ProgressType.UpdateProgressMax:
                    TaskbarProgressBarState tbps = (TaskbarProgressBarState)e.UserState;
                    Adapter.TaskbarSetProgressState(this, tbps);
                    break;
                case WorkerTask.ProgressType.ShowBalloonTip:
                    WorkerTask task = e.UserState as WorkerTask;
                    if (Engine.ConfigOptions.ShowBalloonTip)
                    {
                        ShowBalloonTip(task);
                    }
                    break;
                case WorkerTask.ProgressType.ShowTrayWarning:
                    Adapter.TaskbarSetProgressValue(this, 33);
                    Adapter.TaskbarSetProgressState(this, TaskbarProgressBarState.Error);
                    Adapter.SetNotifyIconBalloonTip(this.niTray, this.Text, e.UserState as string, ToolTipIcon.Warning);
                    break;
            }

            rtbDebugLog.Text = Engine.EngineLogger.ToString();
        }

        public override void BwApp_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            WorkerTask task = (WorkerTask)e.Result;
            PostWorkerTasks();
            if (task == null) return;

            this.Text = Engine.GetProductName();
            niTray.Tag = task;

            if (task.UploadResults.Count > 0)
            {
                UploadManager.UploadResultLast = task.UploadResults[task.UploadResults.Count - 1];
                UploadManager.ResetCumulativePercentage();
            }

            try
            {
                if (task.IsError && task.Errors[0].Contains(ExceptionMessage.ProxyAuthenticationRequired))
                {
                    ProxyUI ui = new ProxyUI();
                    if (ui.ShowDialog() == DialogResult.OK)
                    {
                        Engine.ConfigUI.ConfigProxy.ProxyList.Add(ui.Proxy);
                        Engine.ConfigUI.ConfigProxy.ProxyConfigType = EProxyConfigType.ManualProxy;
                        Uploader.ProxySettings = Adapter.CheckProxySettings();
                    }
                    RetryTask(task);
                }

                WorkerTask checkTask = RetryUpload(task);

                if (checkTask.States.Contains(WorkerTask.TaskState.RetryPending))
                {
                    string message = string.Format("{0}\r\n\r\nAutomatically retrying upload for {1}.", string.Join("\r\n", task.Errors.ToArray()), checkTask.WorkflowConfig.DestConfig.ToStringImageUploaders());
                    niTray.ShowBalloonTip(5000, Application.ProductName, message, ToolTipIcon.Warning);
                }
                else
                {
                    task.States.Add(WorkerTask.TaskState.Finished);

                    if (task.WorkflowConfig.DestConfig.TaskClipboardContent.Contains(ClipboardContentEnum.Local) && Engine.ConfigUI.ShowSaveFileDialogImages)
                    {
                        string fp = task.WriteImageAs();
                        if (!string.IsNullOrEmpty(fp))
                        {
                            task.UpdateLocalFilePath(fp);
                        }
                    }

                    switch (task.Job1)
                    {
                        case EDataType.Text:
                            if (task.Job2 == WorkerTask.JobLevel2.Translate)
                            {
                                UpdateGoogleTranslateGUI(task.TranslationInfo);

                                Loader.MyGTGUI.btnTranslate.Enabled = true;
                                Loader.MyGTGUI.btnTranslateTo.Enabled = true;
                            }
                            break;
                        case EDataType.Image:
                            if (!task.WorkflowConfig.DestConfig.TaskClipboardContent.Contains(ClipboardContentEnum.Local) && Engine.ConfigOptions.DeleteLocal && File.Exists(task.Info.LocalFilePath))
                            {
                                try
                                {
                                    File.Delete(task.Info.LocalFilePath);
                                }
                                catch (Exception ex) // TODO: sometimes file is still locked... delete those files sometime
                                {
                                    DebugHelper.WriteException(ex, "Error while finalizing job");
                                }
                            }
                            break;
                    }

                    if (Engine.ConfigOptions.TwitterEnabled)
                    {
                        Adapter.TwitterMsg(task);
                    }

                    bool bLastSourceButtonsEnabled = task.UploadResults.Count > 0 && !string.IsNullOrEmpty(task.UploadResults[task.UploadResults.Count - 1].Source);
                    this.btnOpenSourceText.Enabled = bLastSourceButtonsEnabled;
                    this.btnOpenSourceBrowser.Enabled = bLastSourceButtonsEnabled;
                    this.btnOpenSourceString.Enabled = bLastSourceButtonsEnabled;

                    if (task.UploadResults.Count > 0 || File.Exists(task.Info.LocalFilePath) || task.Job2 == WorkerTask.JobLevel2.Translate)
                    {
                        if (Engine.ConfigOptions.CompleteSound)
                        {
                            if (Engine.ConfigWorkflow.EnableSoundTaskCompleted && !string.IsNullOrEmpty(Engine.ConfigWorkflow.SoundPath) && File.Exists(Engine.ConfigWorkflow.SoundPath))
                                new SoundPlayer(Engine.ConfigWorkflow.SoundPath).Play();
                            else
                                SystemSounds.Exclamation.Play();
                        }

                        if (Engine.ConfigOptions.ShowBalloonTip)
                        {
                            ShowBalloonTip(task);
                        }
                    }

                    if (task.IsError)
                    {
                        foreach (string error in task.Errors)
                        {
                            DebugHelper.WriteLine(error);
                        }
                        niTray.ShowBalloonTip(5000, Application.ProductName, niTray.BalloonTipText + Environment.NewLine + task.Errors[task.Errors.Count - 1], ToolTipIcon.Error);
                    }
                }

                if (!task.IsError)
                {
                    AddHistoryItem(task);
                }

                // do this last
                UploadManager.ShowUploadResults(task, false);
            }
            catch (Exception ex)
            {
                DebugHelper.WriteException(ex, "Job Completed with errors: ");
            }
            finally
            {
                UploadManager.Commit(task.Id);

                if (TaskbarManager.IsPlatformSupported)
                {
                    Adapter.TaskbarSetProgressState(this, TaskbarProgressBarState.NoProgress);
                }
            }

            DebugHelper.WriteLine(string.Format("Job completed: {0}", task.Job2));
            DebugHelper.WriteLine(string.Format("Task duration: {0} ms", task.UploadDuration));
            PostWorkerTasks();
        }

        private void PostWorkerTasks()
        {
            pbPreview.LoadImage(Resources.ZScreen_256, PictureBoxSizeMode.CenterImage);
            rtbDebugLog.Text = Engine.EngineLogger.ToString();

            this.niTray.Text = this.Text; // do not update notifyIcon text if there are other jobs active
            this.niTray.Icon = Resources.zss_tray;
            Engine.IsClipboardUploading = false;
        }

        #endregion Worker Events

        #region Create Worker

        public DestConfig GetDestConfig(DestSelector ucDestOptions)
        {
            DestConfig DestConfig = new UploadersLib.DestConfig();

            Adapter.SaveMenuConfigToList<OutputEnum>(ucDestOptions.tsddbOutputs, DestConfig.Outputs);
            Adapter.SaveMenuConfigToList<ClipboardContentEnum>(ucDestOptions.tsddbClipboardContent, DestConfig.TaskClipboardContent);
            Adapter.SaveMenuConfigToList<LinkFormatEnum>(ucDestOptions.tsddbLinkFormat, DestConfig.LinkFormat);
            Adapter.SaveMenuConfigToList<ImageDestination>(ucDestOptions.tsddbDestImage, DestConfig.ImageUploaders);
            Adapter.SaveMenuConfigToList<TextDestination>(ucDestOptions.tsddbDestText, DestConfig.TextUploaders);
            Adapter.SaveMenuConfigToList<FileDestination>(ucDestOptions.tsddbDestFile, DestConfig.FileUploaders);
            Adapter.SaveMenuConfigToList<UrlShortenerType>(ucDestOptions.tsddbDestLink, DestConfig.LinkUploaders);

            return DestConfig;
        }

        public override WorkerTask CreateTask(WorkerTask.JobLevel2 job, TaskInfo tiCreateTask = null)
        {
            DebugHelper.WriteLine(string.Format("Creating job: {0}", job));
            if (tiCreateTask == null) tiCreateTask = new TaskInfo();

            tiCreateTask.Job = job;
            tiCreateTask.DestConfig = GetDestConfig(ucDestOptions);
            if (job == WorkerTask.JobLevel2.CaptureRectRegionClipboard)
            {
                tiCreateTask.DestConfig.TaskClipboardContent.Clear();
                tiCreateTask.DestConfig.ImageUploaders.Clear();
                tiCreateTask.DestConfig.TaskClipboardContent.Add(ClipboardContentEnum.Data);
            }
            tiCreateTask.TrayIcon = this.niTray;

            WorkerTask createTask = new WorkerTask(CreateWorker(), tiCreateTask);

            switch (job)
            {
                case WorkerTask.JobLevel2.Translate:
                    Loader.MyGTGUI.btnTranslate.Enabled = false;
                    createTask.SetTranslationInfo(new GoogleTranslateInfo()
                    {
                        Text = Loader.MyGTGUI.txtTranslateText.Text,
                        SourceLanguage = Engine.ConfigGT.GoogleSourceLanguage,
                        TargetLanguage = Engine.ConfigGT.GoogleTargetLanguage
                    });

                    break;
            }

            return createTask;
        }

        #endregion Create Worker

        #region Google Translate

        public void StartWorkerTranslator()
        {
            if (Clipboard.ContainsText())
            {
                RunWorkerAsync_LanguageTranslator(new GoogleTranslateInfo()
                {
                    Text = Clipboard.GetText(),
                    SourceLanguage = Engine.ConfigGT.GoogleSourceLanguage,
                    TargetLanguage = Engine.ConfigGT.GoogleTargetLanguage
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
        public void RunWorkerAsync(WorkerTask screenshotTask)
        {
            if (screenshotTask == null) return;
            if (screenshotTask.States.Contains(WorkerTask.TaskState.CancellationPending))
            {
                PostWorkerTasks();
                return;
            }

            screenshotTask.WasToTakeScreenshot = true;
            // the last point before the task enters background
            if (screenshotTask.TempImage != null)
            {
                pbPreview.LoadImage(screenshotTask.TempImage);
                screenshotTask.RunWorker();
            }
            else
            {
                screenshotTask.States.Add(WorkerTask.TaskState.CancellationPending);
                PostWorkerTasks();
            }
        }

        public override void CaptureActiveWindow()
        {
            WorkerTask hkawTask = CreateTask(WorkerTask.JobLevel2.CaptureActiveWindow);
#if DEBUG
            UploadManager.UploadImage(hkawTask);
#endif
            RunWorkerAsync(hkawTask);
        }

        public override void CaptureEntireScreen()
        {
            WorkerTask hkesTask = CreateTask(WorkerTask.JobLevel2.CaptureEntireScreen);
            RunWorkerAsync(hkesTask);
        }

        public override void CaptureActiveMonitor()
        {
            WorkerTask hkesTask = CreateTask(WorkerTask.JobLevel2.CaptureActiveMonitor);
            RunWorkerAsync(hkesTask);
        }

        public override void CaptureSelectedWindow()
        {
            WorkerTask hkswTask = CreateTask(WorkerTask.JobLevel2.CaptureSelectedWindow);
            RunWorkerAsync(hkswTask);
        }

        public void CaptureSelectedWindowFromList(IntPtr handle)
        {
            TaskInfo tiCaptureWindowFromList = new TaskInfo() { Handle = handle };
            WorkerTask hkswTask = CreateTask(WorkerTask.JobLevel2.CaptureSelectedWindowFromList, tiCaptureWindowFromList);
            RunWorkerAsync(hkswTask);
        }

        public override void CaptureRectRegion()
        {
            WorkerTask hkrcTask = CreateTask(WorkerTask.JobLevel2.CaptureRectRegion);
            RunWorkerAsync(hkrcTask);
        }

        public override void CaptureRectRegionClipboard()
        {
            WorkerTask hkrcTask = CreateTask(WorkerTask.JobLevel2.CaptureRectRegionClipboard);
            RunWorkerAsync(hkrcTask);
        }

        public override void CaptureRectRegionLast()
        {
            WorkerTask hkrclTask = CreateTask(WorkerTask.JobLevel2.CaptureLastCroppedWindow);
            RunWorkerAsync(hkrclTask);
        }

        public override void CaptureFreeHandRegion()
        {
            WorkerTask hkfhrTask = CreateTask(WorkerTask.JobLevel2.CaptureFreeHandRegion);
            RunWorkerAsync(hkfhrTask);
        }

        #endregion RunWorkerAsync Job 1

        #region RunWorkerAsync Job 2

        public void RunWorkerAsync_LanguageTranslator(GoogleTranslateInfo translationInfo)
        {
            WorkerTask gtTask = CreateTask(WorkerTask.JobLevel2.Translate);
            if (Loader.MyGTGUI == null)
            {
                Loader.MyGTGUI = new GoogleTranslateGUI(Engine.ConfigGT);
            }
            Loader.MyGTGUI.btnTranslate.Enabled = false;
            Loader.MyGTGUI.btnTranslateTo.Enabled = false;
            gtTask.SetTranslationInfo(translationInfo);
            gtTask.RunWorker();
        }

        public void UploadUsingClipboardOrGoogleTranslate()
        {
            if (Clipboard.ContainsText() && Engine.ConfigGT.AutoTranslate && Clipboard.GetText().Length <= Engine.ConfigGT.AutoTranslateLength)
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
            if (!Engine.IsClipboardUploading)
            {
                Engine.IsClipboardUploading = true;

                if (Clipboard.ContainsFileDropList())
                {
                    UploadUsingFileSystem(FileSystem.GetExplorerFileList(Clipboard.GetFileDropList()).ToArray());
                }
                else
                {
                    WorkerTask cbTask = CreateTask(WorkerTask.JobLevel2.UploadFromClipboard);

                    if (Clipboard.ContainsImage())
                    {
                        cbTask.RunWorker();
                    }
                    else if (Clipboard.ContainsText())
                    {
                        cbTask.RunWorker();
                        Engine.IsClipboardUploading = true;
                    }
                }
            }
        }

        public bool UploadUsingFileSystem(params string[] fileList)
        {
            List<string> strListFilePath = new List<string>();
            bool succ = true;
            foreach (string fp in fileList)
            {
                try
                {
                    if (!string.IsNullOrEmpty(fp) && File.Exists(fp))
                    {
                        if (GraphicsMgr.IsValidImage(fp))
                        {
                            string dirfp = Path.GetDirectoryName(fp);
                            string fsfp = FileSystem.GetUniqueFilePath(Engine.ConfigWorkflow, Engine.ImagesDir, Path.GetFileName(fp));
                            if (fp != fsfp && dirfp != Engine.ImagesDir)
                            {
                                string dir = Path.GetDirectoryName(fsfp);
                                if (!Directory.Exists(dir))
                                {
                                    Directory.CreateDirectory(dir);
                                }
                                try
                                {
                                    File.Copy(fp, fsfp, true);
                                    if (Path.GetDirectoryName(fp) == Engine.ConfigUI.FolderMonitorPath)
                                    {
                                        File.Delete(fp);
                                    }
                                }
                                catch (Exception e)
                                {
                                    fsfp = fp;
                                    DebugHelper.WriteException(e);
                                }
                                strListFilePath.Add(fsfp);
                            }
                            else
                            {
                                strListFilePath.Add(fp);
                            }
                        }
                        else
                        {
                            strListFilePath.Add(fp); // yes we use the orignal file path
                        }
                    }
                }
                catch (Exception ex)
                {
                    DebugHelper.WriteException(ex, "Error while uploading using file system");
                    succ = false;
                }
            }

            strListFilePath.Sort();
            List<Image> tempImages = new List<Image>();
            bool bCreateAni = strListFilePath.Count > 1 && strListFilePath.Count < Engine.ConfigWorkflow.ImageAnimatedFramesMax && GraphicsMgr.HasIdenticalImages(strListFilePath, out tempImages);

            if (bCreateAni)
            {
                WorkerTask agifTask = CreateTask(WorkerTask.JobLevel2.UploadFromExplorer);
                agifTask.SetImages(tempImages);
                agifTask.RunWorker();
            }
            else
            {
                foreach (string fp in strListFilePath)
                {
                    var tifs = new TaskInfo() { ExistingFilePath = fp };
                    var fpdataTask = CreateTask(WorkerTask.JobLevel2.UploadFromExplorer, tifs);
                    fpdataTask.UpdateLocalFilePath(fp);
                    fpdataTask.RunWorker();
                }
            }
            return succ;
        }

        #endregion RunWorkerAsync Job 2

        #region Auto Capture

        public override void ShowAutoCapture()
        {
            if (!bAutoScreenshotsOpened)
            {
                bAutoScreenshotsOpened = true;
                AutoCapture autoScreenshots = new AutoCapture { Icon = Resources.zss_main };
                autoScreenshots.EventJob += new JobsEventHandler(EventJobs);
                autoScreenshots.FormClosed += new FormClosedEventHandler(autoScreenshots_FormClosed);
                autoScreenshots.Show();
                if (Engine.ConfigUI.AutoCaptureExecute) autoScreenshots.Execute();
            }
        }

        private void autoScreenshots_FormClosed(object sender, FormClosedEventArgs e)
        {
            bAutoScreenshotsOpened = false;
        }

        internal void EventJobs(object sender, WorkerTask.JobLevel2 job)
        {
            WorkerTask task = CreateTask(job);
            switch (job)
            {
                case WorkerTask.JobLevel2.CaptureEntireScreen:
                    CaptureEntireScreen();
                    break;
                case WorkerTask.JobLevel2.CaptureActiveWindow:
                    CaptureActiveWindow();
                    break;
                case WorkerTask.JobLevel2.CaptureSelectedWindow:
                    CaptureSelectedWindow();
                    break;
                case WorkerTask.JobLevel2.CaptureRectRegion:
                    CaptureRectRegion();
                    break;
                case WorkerTask.JobLevel2.CaptureLastCroppedWindow:
                    CaptureRectRegionLast();
                    break;
                case WorkerTask.JobLevel2.AutoCapture:
                    ShowAutoCapture();
                    break;
                case WorkerTask.JobLevel2.UploadFromClipboard:
                    UploadUsingClipboardOrGoogleTranslate();
                    break;
                case WorkerTask.JobLevel2.UploadFromExplorer:
                    ShowDropWindow();
                    break;
                case WorkerTask.JobLevel2.Translate:
                    StartWorkerTranslator();
                    break;
                case WorkerTask.JobLevel2.ScreenColorPicker:
                    ShowScreenColorPicker();
                    break;
            }
        }

        #endregion Auto Capture

        #region Drop Window

        public override void ShowDropWindow()
        {
            if (!bDropWindowOpened)
            {
                bDropWindowOpened = true;
                DropWindow dw = new DropWindow();
                dw.Result += new StringsEventHandler(dw_Result);
                dw.FormClosed += new FormClosedEventHandler(dw_FormClosed);
                dw.Show();
                Rectangle taskbar = NativeMethods.GetTaskbarRectangle();
                if (Engine.ConfigUI.LastDropBoxPosition == Point.Empty)
                {
                    dw.Location = new Point(SystemInformation.PrimaryMonitorSize.Width - dw.Width - 100,
                        SystemInformation.PrimaryMonitorSize.Height - taskbar.Height - dw.Height - 10);
                }
                else
                {
                    dw.Location = Engine.ConfigUI.LastDropBoxPosition;
                }
            }
        }

        private void dw_Result(object sender, string[] dwFiles)
        {
            if (dwFiles != null)
            {
                UploadUsingFileSystem(dwFiles);
            }
        }

        private void dw_FormClosed(object sender, FormClosedEventArgs e)
        {
            bDropWindowOpened = false;
        }

        #endregion Drop Window

        #region Screen Color Picker

        public override void ShowScreenColorPicker()
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
            if (task.UploadResults.Count > 0 && task.Job2 != WorkerTask.JobLevel2.Translate)
            {
                if (!task.WorkflowConfig.DestConfig.TaskClipboardContent.Contains(ClipboardContentEnum.Data) &&
                    !task.WorkflowConfig.DestConfig.TaskClipboardContent.Contains(ClipboardContentEnum.Local) &&
                    string.IsNullOrEmpty(task.UploadResults[0].URL) && Engine.ConfigUI.ImageUploadRetryOnFail &&
                    task.States.Contains(WorkerTask.TaskState.RetryPending) && File.Exists(task.Info.LocalFilePath))
                {
                    WorkerTask task2 = CreateTask(WorkerTask.JobLevel2.UploadImage);
                    task2.SetImage(task.Info.LocalFilePath);
                    task2.States.Add(WorkerTask.TaskState.Finished); // we do not retry again

                    if (task.Job1 == EDataType.Image)
                    {
                        if (Engine.ConfigUI.ImageUploadRandomRetryOnFail)
                        {
                            List<ImageDestination> randomDest = new List<ImageDestination>() { ImageDestination.ImageShack, ImageDestination.TinyPic, ImageDestination.Imgur };
                            int r = Adapter.RandomNumber(3, 3 + randomDest.Count - 1);
                            while (task.WorkflowConfig.DestConfig.ImageUploaders.Contains((ImageDestination)r))
                            {
                                r = Adapter.RandomNumber(3, 3 + randomDest.Count - 1);
                            }
                            if (!task.WorkflowConfig.DestConfig.ImageUploaders.Contains((ImageDestination)r))
                            {
                                task.WorkflowConfig.DestConfig.ImageUploaders.Add((ImageDestination)r);
                            }
                        }
                        else
                        {
                            if (!task.WorkflowConfig.DestConfig.ImageUploaders.Contains(ImageDestination.TinyPic))
                            {
                                task.WorkflowConfig.DestConfig.ImageUploaders.Add((ImageDestination.TinyPic));
                            }
                            else if (!task.WorkflowConfig.DestConfig.ImageUploaders.Contains(ImageDestination.ImageShack))
                            {
                                task.WorkflowConfig.DestConfig.ImageUploaders.Add((ImageDestination.ImageShack));
                            }
                        }
                    }

                    task2.MyWorker.RunWorkerAsync(task2);
                    return task2;
                }
            }
            task.States.Add(WorkerTask.TaskState.Finished);
            return task;
        }

        #endregion Retry Methods

        #region History

        public void AddHistoryItem(WorkerTask task)
        {
            if (Engine.ConfigOptions.HistorySave)
            {
                foreach (UploadResult ur in task.UploadResults)
                {
                    HistoryManager.AddHistoryItemAsync(Engine.HistoryPath, task.GenerateHistoryItem(ur));
                }
            }

            Adapter.AddRecentItem(task.Info.LocalFilePath);
        }

        #endregion History
    }
}
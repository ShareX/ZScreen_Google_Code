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
using System.Windows.Forms;
using GraphicsMgrLib;
using HelpersLib;
using UploadersLib;
using UploadersLib.HelperClasses;
using ZScreenLib.Properties;
using ZSS.IndexersLib;
using ZUploader.HelperClasses;

namespace ZScreenLib
{
    public class Worker
    {
        private GenericMainWindow GUI = null;
        public bool IsBusy { get; private set; }

        public Worker() { }

        public Worker(GenericMainWindow gui)
        {
            this.GUI = gui;
            this.IsBusy = true;
        }

        #region Background Worker

        public virtual BackgroundWorker CreateWorker()
        {
            BackgroundWorker bwApp = new BackgroundWorker { WorkerReportsProgress = true };
            bwApp.DoWork += new DoWorkEventHandler(BwApp_DoWork);
            bwApp.ProgressChanged += new ProgressChangedEventHandler(bwApp_ProgressChanged);
            bwApp.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bwApp_RunWorkerCompleted);
            return bwApp;
        }

        public virtual void BwApp_DoWork(object sender, DoWorkEventArgs e)
        {
            WorkerTask task = (WorkerTask)e.Argument;
            task.UniqueNumber = UploadManager.Queue();
            task.MyWorker.ReportProgress((int)WorkerTask.ProgressType.SET_ICON_BUSY, task);

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
                            // LanguageTranslator(ref task);
                            break;
                    }

                    break;
            }

            if (!string.IsNullOrEmpty(task.LocalFilePath) && File.Exists(task.LocalFilePath))
            {
                if (Engine.conf.AddFailedScreenshot ||
                    (!Engine.conf.AddFailedScreenshot && task.Errors.Count == 0 || task.JobCategory == JobCategoryType.TEXT))
                {
                    task.MyWorker.ReportProgress((int)WorkerTask.ProgressType.ADD_FILE_TO_LISTBOX, new HistoryItem(task));
                }
            }

            e.Result = task;
        }

        void bwApp_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            switch ((WorkerTask.ProgressType)e.ProgressPercentage)
            {
                case WorkerTask.ProgressType.SET_ICON_BUSY:
                    Adapter.SetNotifyIconStatus(e.UserState as WorkerTask, GUI.niTray, Resources.zss_busy);
                    break;
                case WorkerTask.ProgressType.CHANGE_TRAY_ICON_PROGRESS:
                    //Issue 331
                    int progress = (int)((ProgressManager)e.UserState).Percentage;
                    Adapter.UpdateNotifyIconProgress(GUI.niTray, progress);
                    Adapter.TaskbarSetProgressValue(progress);
                    GUI.Text = string.Format("{0}% - {1}", progress, Engine.GetProductName());
                    break;
            }
        }

        void bwApp_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            WorkerTask task = (WorkerTask)e.Result;

            try
            {
                FileSystem.AppendDebug(string.Format("Job completed: {0}", task.Job));
                WorkerTask checkTask = RetryUpload(task);

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
                            case WorkerTask.Jobs.UploadFromClipboard:
                                if (!string.IsNullOrEmpty(task.RemoteFilePath))
                                {
                                    Clipboard.SetText(task.RemoteFilePath);
                                }

                                break;
                        }

                        break;
                    case JobCategoryType.SCREENSHOTS:
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

                if (task.JobCategory != JobCategoryType.TEXT)
                {
                    UploadManager.SetClipboardText(task, false);
                }

                this.GUI.niTray.Text = this.GUI.Text;
                if (UploadManager.UploadInfoList.Count > 1)
                {
                    this.GUI.niTray.Icon = Resources.zss_busy;
                }
                else
                {
                    this.GUI.niTray.Icon = Resources.zss_tray;
                }

                if (task.Job == WorkerTask.Jobs.LANGUAGE_TRANSLATOR || File.Exists(task.LocalFilePath) || !string.IsNullOrEmpty(task.RemoteFilePath))
                {
                    if (Engine.conf.CompleteSound)
                    {
                        System.Media.SystemSounds.Exclamation.Play();
                    }

                    if (Engine.conf.ShowBalloonTip)
                    {
                        new BalloonTipHelper(this.GUI.niTray, task).ShowBalloonTip();
                    }
                }

                if (task.Errors.Count > 0)
                {
                    FileSystem.AppendDebug(task.Errors[task.Errors.Count - 1]);
                }

                if (task.MyImage != null)
                {
                    task.MyImage.Dispose(); // For fix memory leak
                }
            }
            catch (Exception ex)
            {
                FileSystem.AppendDebug("Error while finalizing Worker job", ex);
            }
            finally
            {
                UploadManager.Commit(task.UniqueNumber);
                this.IsBusy = false;
            }
        }

        #endregion Background Worker

        #region Create Tasks

        public WorkerTask CreateTask(WorkerTask.Jobs job)
        {
            BackgroundWorker bwApp = CreateWorker();
            WorkerTask task = new WorkerTask(bwApp, job);
            if (task.Job != WorkerTask.Jobs.CustomUploaderTest)
            {
                task.MyImageUploader = Engine.conf.ImageUploaderType;
                if (Adapter.CheckList(Engine.conf.TextUploadersList, Engine.conf.TextUploaderSelected))
                {
                    task.MyTextUploader = Engine.conf.TextUploadersList[Engine.conf.TextUploaderSelected];
                }
                task.MyFileUploader = Engine.conf.FileDestMode;
            }
            else
            {
                task.MyImageUploader = ImageDestType.CUSTOM_UPLOADER;
            }

            return task;
        }

        public WorkerTask GetWorkerText(WorkerTask.Jobs job)
        {
            return GetWorkerText(job, string.Empty);
        }

        /// <summary>
        /// Worker for Text: Paste2, Pastebin
        /// </summary>
        /// <returns></returns>
        public virtual WorkerTask GetWorkerText(WorkerTask.Jobs job, string localFilePath)
        {
            WorkerTask t = CreateTask(job);
            t.JobCategory = JobCategoryType.TEXT;
            // t.MakeTinyURL = Program.MakeTinyURL();
            t.MyTextUploader = Adapter.GetTextUploaderActive();
            if (!string.IsNullOrEmpty(localFilePath))
            {
                t.UpdateLocalFilePath(localFilePath);
            }

            return t;
        }

        #endregion Create Tasks

        #region User Tasks

        public void StartBw_SelectedWindow()
        {
            if (!TaskManager.mTakingScreenShot)
            {
                StartWorkerScreenshots(WorkerTask.Jobs.TakeScreenshotWindowSelected);
            }
        }

        public void StartBw_CropShot()
        {
            if (!TaskManager.mTakingScreenShot)
            {
                StartWorkerScreenshots(WorkerTask.Jobs.TakeScreenshotCropped);
            }
        }

        public void StartBw_FreehandCropShot()
        {
            if (!TaskManager.mTakingScreenShot)
            {
                StartWorkerScreenshots(WorkerTask.Jobs.FREEHAND_CROP_SHOT);
            }
        }

        public void StartBw_ClipboardUpload()
        {
            WorkerTask task = CreateTask(WorkerTask.Jobs.UploadFromClipboard);

            List<WorkerTask> textWorkers = new List<WorkerTask>();

            if (Clipboard.ContainsImage())
            {
                task.SetImage(Clipboard.GetImage());
                if (task.SetFilePathFromPattern(new NameParser(NameParserType.EntireScreen).Convert(Engine.conf.EntireScreenPattern)))
                {
                    FileSystem.SaveImage(ref task);
                    StartWorkerPictures(task, task.LocalFilePath);
                }
            }
            else if (Clipboard.ContainsText())
            {
                WorkerTask temp = GetWorkerText(WorkerTask.Jobs.UploadFromClipboard);
                string fp = FileSystem.GetUniqueFilePath(Path.Combine(Engine.TextDir, new NameParser().Convert("%y.%mo.%d-%h.%mi.%s") + ".txt"));
                File.WriteAllText(fp, Clipboard.GetText());
                temp.UpdateLocalFilePath(fp);
                temp.MyText = TextInfo.FromFile(fp);
                textWorkers.Add(temp);
            }
            else if (Clipboard.ContainsFileDropList())
            {
                UploadUsingFileSystem(FileSystem.GetExplorerFileList(Clipboard.GetFileDropList()));
            }

            StartTextWorkers(textWorkers);
        }

        protected void UploadUsingDragDrop(string fp)
        {
            StartWorkerPictures(CreateTask(WorkerTask.Jobs.PROCESS_DRAG_N_DROP), fp);
        }

        protected void UploadUsingDragDrop(string[] paths)
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
                    FileSystem.AppendDebug("Error while uploading using file system", ex);
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
                    temp.UpdateLocalFilePath(fp);
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

        /// <summary>
        /// Worker for Screenshots: Active Window, Crop, Entire Screen
        /// </summary>
        /// <param name="job">Job Type</param>
        public void StartWorkerScreenshots(WorkerTask.Jobs job)
        {
            Engine.ClipboardUnhook();
            WorkerTask t = CreateTask(job);
            t.JobCategory = JobCategoryType.SCREENSHOTS;
            t.MakeTinyURL = Adapter.MakeTinyURL();
            t.MyWorker.RunWorkerAsync(t);
        }

        /// <summary>
        /// Worker for Images: Drag n Drop, Image from Clipboard, Custom Uploader
        /// </summary>
        /// <param name="job">Job Type</param>
        /// <param name="localFilePath">Local file path of the image</param>
        public void StartWorkerPictures(WorkerTask task, string localFilePath)
        {
            Engine.ClipboardUnhook();
            task.JobCategory = JobCategoryType.PICTURES;
            task.MakeTinyURL = Adapter.MakeTinyURL();
            task.UpdateLocalFilePath(localFilePath);
            task.SetImage(localFilePath);
            task.MyWorker.RunWorkerAsync(task);
        }

        public void StartWorkerPictures(WorkerTask.Jobs job, Image img)
        {
            Engine.ClipboardUnhook();
            WorkerTask t = CreateTask(job);
            t.JobCategory = JobCategoryType.PICTURES;
            t.MakeTinyURL = Adapter.MakeTinyURL();
            t.SetImage(img);
            new TaskManager(ref t).WriteImage();
            t.MyWorker.RunWorkerAsync(t);
        }

        protected void StartTextWorkers(List<WorkerTask> textWorkers)
        {
            Engine.ClipboardUnhook();
            foreach (WorkerTask task in textWorkers)
            {
                if (FileSystem.IsValidLink(task.MyText.LocalString) && Engine.conf.ShortenUrlUsingClipboardUpload && Adapter.CheckURLShorteners())
                {
                    FileSystem.AppendDebug(string.Format("URL: {0}; Length {1}; Shortening after {2}", task.MyText.LocalString, task.MyText.LocalString.Length, Engine.conf.ShortenUrlAfterUploadAfter));
                    task.MyTextUploader = Engine.conf.UrlShortenersList[Engine.conf.UrlShortenerSelected];
                    task.RunWorker();
                }
                else if (Directory.Exists(task.MyText.LocalString))
                {
                    IndexerAdapter settings = new IndexerAdapter();
                    settings.LoadConfig(Engine.conf.IndexerConfig);
                    Engine.conf.IndexerConfig.FolderList.Clear();
                    string ext = ".log";
                    if (Engine.conf.PreferFileUploaderForText || Adapter.CheckFTPAccounts())
                    {
                        if (Engine.conf.PreferFtpServerForIndex)
                        {
                            task.MyFileUploader = FileUploaderType.FTP;
                            task.JobCategory = JobCategoryType.BINARY;
                        }
                        if (task.MyFileUploader == FileUploaderType.FTP)
                        {
                            ext = ".html";
                        }
                    }
                    string fileName = Path.GetFileName(task.MyText.LocalString) + ext;
                    settings.GetConfig().SetSingleIndexPath(Path.Combine(Engine.TextDir, fileName));
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
                        task.UpdateLocalFilePath(settings.GetConfig().GetIndexFilePath());
                        task.RunWorker();
                    }
                }
                else if (Adapter.CheckTextUploaders())
                {
                    task.RunWorker();
                }
            }
        }

        /// <summary>
        /// Worker for Binary: Drag n Drop, Clipboard Upload files from Explorer
        /// </summary>
        /// <param name="job">Job Type</param>
        /// <param name="localFilePath">Local file path of the file</param>
        protected void StartWorkerBinary(WorkerTask.Jobs job, string localFilePath)
        {
            WorkerTask t = CreateTask(job);
            t.JobCategory = JobCategoryType.BINARY;
            t.MakeTinyURL = Adapter.MakeTinyURL();
            t.UpdateLocalFilePath(localFilePath);
            t.MyWorker.RunWorkerAsync(t);
        }

        #endregion User Tasks

        #region Publish Workers

        /// <summary>
        /// Function to edit Text in a Text Editor and Upload
        /// </summary>
        /// <param name="task"></param>
        protected void PublishText(ref WorkerTask task)
        {
            TaskManager tm = new TaskManager(ref task);
            tm.UploadText();
        }

        protected void PublishBinary(ref WorkerTask task)
        {
            TaskManager tm = new TaskManager(ref task);
            tm.UploadFile();
        }

        #endregion Publish Workers

        public WorkerTask RetryUpload(WorkerTask task)
        {
            if (task.Job != WorkerTask.Jobs.LANGUAGE_TRANSLATOR && task.MyImageUploader != ImageDestType.PRINTER)
            {
                if (task.MyImageUploader != ImageDestType.CLIPBOARD && task.MyImageUploader != ImageDestType.FILE &&
                    string.IsNullOrEmpty(task.RemoteFilePath) && Engine.conf.ImageUploadRetryOnFail && !task.RetryPending && File.Exists(task.LocalFilePath))
                {
                    WorkerTask task2 = CreateTask(WorkerTask.Jobs.UPLOAD_IMAGE);
                    task2.JobCategory = task.JobCategory;
                    task2.SetImage(task.LocalFilePath);
                    task2.UpdateLocalFilePath(task.LocalFilePath);
                    task2.RetryPending = true; // we do not retry again

                    if (task.JobCategory == JobCategoryType.PICTURES || task.JobCategory == JobCategoryType.SCREENSHOTS)
                    {
                        if (Engine.conf.ImageUploadRandomRetryOnFail)
                        {
                            List<ImageDestType> randomDest = new List<ImageDestType>() { ImageDestType.IMAGESHACK, ImageDestType.TINYPIC };
                            if (!string.IsNullOrEmpty(Engine.conf.ImageBamApiKey))
                            {
                                randomDest.Add(ImageDestType.IMAGEBAM);
                            }
                            if (null != Engine.conf.FlickrAuthInfo)
                            {
                                randomDest.Add(ImageDestType.FLICKR);
                            }
                            if (Adapter.CheckFTPAccounts() && null != Adapter.GetFtpAcctActive())
                            {
                                randomDest.Add(ImageDestType.FTP);
                            }
                            int r = Adapter.RandomNumber(3, 3 + randomDest.Count - 1);
                            while ((ImageDestType)r == task2.MyImageUploader || (ImageDestType)r == ImageDestType.FILE || (ImageDestType)r == ImageDestType.CLIPBOARD)
                            {
                                r = Adapter.RandomNumber(3, 3 + randomDest.Count - 1);
                            }
                            task2.MyImageUploader = (ImageDestType)r;
                        }
                        else
                        {
                            if (task.MyImageUploader == ImageDestType.IMAGESHACK)
                            {
                                task2.MyImageUploader = ImageDestType.TINYPIC;
                            }
                            else
                            {
                                task2.MyImageUploader = ImageDestType.IMAGESHACK;
                            }
                        }
                    }

                    task2.MyWorker.RunWorkerAsync(task2);
                    return task2;
                }
            }
            task.RetryPending = false;
            return task;
        }
    }
}
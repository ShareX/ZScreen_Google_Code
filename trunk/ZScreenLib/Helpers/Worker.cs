using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using ZScreenLib;
using UploadersLib;
using System.IO;
using System.Drawing;
using ZSS.IndexersLib;
using System.Windows.Forms;
using UploadersLib.Helpers;
using ZScreenLib.Properties;

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
            bwApp.DoWork += new DoWorkEventHandler(bwApp_DoWork);
            bwApp.ProgressChanged += new ProgressChangedEventHandler(bwApp_ProgressChanged);
            bwApp.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bwApp_RunWorkerCompleted);
            return bwApp;
        }

        void bwApp_DoWork(object sender, DoWorkEventArgs e)
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
                    int progress = (int)e.UserState;
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
                                FileSystem.AppendDebug(ex.ToString());
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
                FileSystem.AppendDebug(ex.ToString());
            }
            finally
            {
                UploadManager.Commit(task.UniqueNumber);
                this.IsBusy = false;
            }
        }

        #endregion

        #region Create Tasks

        public WorkerTask CreateTask(WorkerTask.Jobs job)
        {
            BackgroundWorker bwApp = CreateWorker();
            WorkerTask task = new WorkerTask(bwApp, job);
            if (task.Job != WorkerTask.Jobs.CustomUploaderTest)
            {
                task.MyImageUploader = Engine.conf.ScreenshotDestMode;
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
                t.SetLocalFilePath(localFilePath);
            }

            return t;
        }

        #endregion

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

        public void StartBw_ClipboardUpload()
        {
            WorkerTask task = CreateTask(WorkerTask.Jobs.UploadFromClipboard);

            List<WorkerTask> textWorkers = new List<WorkerTask>();

            if (Clipboard.ContainsImage())
            {
                Image cImage = Clipboard.GetImage();
                task.Settings.ManualNaming = false;
                task.SetFilePath(NameParser.Convert(new NameParserInfo(NameParserType.EntireScreen)));
                FileSystem.SaveImage(cImage, task.LocalFilePath);
                StartWorkerPictures(task, task.LocalFilePath);
            }
            else if (Clipboard.ContainsText())
            {
                WorkerTask temp = GetWorkerText(WorkerTask.Jobs.UploadFromClipboard);
                string fp = FileSystem.GetUniqueFilePath(Path.Combine(Engine.TextDir,
                    NameParser.Convert(new NameParserInfo("%y.%mo.%d-%h.%mi.%s")) + ".txt"));
                File.WriteAllText(fp, Clipboard.GetText());
                temp.SetLocalFilePath(fp);
                temp.MyText = TextInfo.FromFile(fp);
                textWorkers.Add(temp);
            }
            else if (Clipboard.ContainsFileDropList())
            {
                UploadUsingFileSystem(FileSystem.GetExplorerFileList(Clipboard.GetFileDropList()));
            }

            StartTextWorkers(textWorkers);
        }

        protected void ScreenshotUsingDragDrop(string fp)
        {
            StartWorkerPictures(CreateTask(WorkerTask.Jobs.PROCESS_DRAG_N_DROP), fp);
        }

        protected void ScreenshotUsingDragDrop(string[] paths)
        {
            foreach (string filePath in FileSystem.GetExplorerFileList(paths))
            {
                File.Copy(filePath, FileSystem.GetUniqueFilePath(Path.Combine(Engine.ImagesDir, Path.GetFileName(filePath))), true);
                ScreenshotUsingDragDrop(filePath);
            }
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
        /// Worker for Images: Drag n Drop, Image from Clipboard, Custom Uploader
        /// </summary>
        /// <param name="job">Job Type</param>
        /// <param name="localFilePath">Local file path of the image</param>
        public void StartWorkerPictures(WorkerTask task, string localFilePath)
        {
            task.JobCategory = JobCategoryType.PICTURES;
            task.MakeTinyURL = Adapter.MakeTinyURL();
            task.SetLocalFilePath(localFilePath);
            task.SetImage(localFilePath);
            task.MyWorker.RunWorkerAsync(task);
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

        protected void StartTextWorkers(List<WorkerTask> textWorkers)
        {
            foreach (WorkerTask task in textWorkers)
            {
                if (FileSystem.IsValidLink(task.MyText.LocalString) && Engine.conf.AutoShortenURL && Adapter.CheckURLShorteners())
                {
                    task.MyTextUploader = Engine.conf.UrlShortenersList[Engine.conf.UrlShortenerSelected];
                    task.RunWorker();
                }
                else if (Directory.Exists(task.MyText.LocalString)) // McoreD: can make this an option later
                {
                    IndexerAdapter settings = new IndexerAdapter();
                    settings.LoadConfig(Engine.conf.IndexerConfig);
                    Engine.conf.IndexerConfig.FolderList.Clear();
                    string ext = (task.MyTextUploader.GetType() == typeof(FTPUploader)) ? ".html" : ".log";
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
            t.SetLocalFilePath(localFilePath);
            t.MyWorker.RunWorkerAsync(t);
        }

        #endregion

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

        #endregion


    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using ZScreenLib;
using UploadersLib;
using System.IO;
using System.Drawing;

namespace ZScreenLib
{
    public class Worker
    {
        private GenericMainWindow GUI = null;

        public Worker() { }

        public Worker(GenericMainWindow gui)
        {
            this.GUI = gui;
        }

        #region Create Tasks

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

        #endregion

        #region Public Start Worker Methods

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
            task.StartTime = DateTime.Now;
            TaskManager tm = new TaskManager(ref task);
            tm.UploadFile();
            task.EndTime = DateTime.Now;
        }

        #endregion

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
            task.MyWorker.ReportProgress((int)WorkerTask.ProgressType.SET_ICON_BUSY, task);

        }

        void bwApp_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            //  throw new NotImplementedException();
        }

        void bwApp_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.GUI.Close();
        }
    }
}

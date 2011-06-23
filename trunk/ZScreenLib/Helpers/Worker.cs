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

            switch (task.Job1)
            {
                case JobLevel1.Image:
                case JobLevel1.File:
                    switch (task.Job2)
                    {
                        case WorkerTask.JobLevel2.CaptureEntireScreen:
                            task.CaptureScreen();
                            break;
                        case WorkerTask.JobLevel2.CaptureSelectedWindow:
                        case WorkerTask.JobLevel2.CaptureRectRegion:
                        case WorkerTask.JobLevel2.CaptureLastCroppedWindow:
                            task.CaptureRegionOrWindow();
                            break;
                        case WorkerTask.JobLevel2.CaptureActiveWindow:
                            task.CaptureActiveWindow();
                            break;
                        case WorkerTask.JobLevel2.CaptureFreeHandRegion:
                            task.CaptureFreehandCrop();
                            break;
                        case WorkerTask.JobLevel2.UPLOAD_IMAGE:
                        case WorkerTask.JobLevel2.UploadFromClipboard:
                            task.PublishData();
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
                            // LanguageTranslator(ref task);
                            break;
                    }
                    break;
            }

            e.Result = task;
        }

        private void bwApp_ProgressChanged(object sender, ProgressChangedEventArgs e)
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
                    Adapter.TaskbarSetProgressValue(null, progress);
                    GUI.Text = string.Format("{0}% - {1}", progress, Engine.GetProductName());
                    break;
            }
        }

        private void bwApp_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            WorkerTask task = (WorkerTask)e.Result;

            try
            {
                Engine.MyLogger.WriteLine(string.Format("Job completed: {0}", task.Job2));

                if (task.WasToTakeScreenshot)
                {
                    if (!task.MyOutputs.Contains(OutputTypeEnum.Local) && Engine.conf.DeleteLocal && File.Exists(task.LocalFilePath))
                    {
                        try
                        {
                            File.Delete(task.LocalFilePath);
                        }
                        catch (Exception ex) // sometimes file is still locked... ToDo: delete those files sometime
                        {
                            Engine.MyLogger.WriteException(ex, "Error while finalizing job");
                        }
                    }
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

                if (task.Job2 == WorkerTask.JobLevel2.LANGUAGE_TRANSLATOR || File.Exists(task.LocalFilePath) || task.UploadResults.Count > 0)
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

                if (Engine.conf.CopyClipboardAfterTask)
                {
                    UploadManager.SetClipboard(IntPtr.Zero, task, false);
                }

                if (task.IsError)
                {
                    foreach (string error in task.Errors)
                    {
                        Engine.MyLogger.WriteLine(error);
                    }

                    MessageBox.Show(task.Errors[task.Errors.Count - 1], "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                if (task.TempImage != null)
                {
                    task.TempImage.Dispose(); // For fix memory leak
                }
            }
            catch (Exception ex)
            {
                Engine.MyLogger.WriteException(ex, "Error while finalizing Worker job");
            }
            finally
            {
                UploadManager.Commit(task.UniqueNumber);
                this.IsBusy = false;
            }
        }

        #endregion Background Worker

        #region Create Tasks

        public WorkerTask CreateTask(WorkerTask.JobLevel2 job)
        {
            return new WorkerTask(CreateWorker(), job);
        }

        public WorkerTask GetWorkerText(WorkerTask.JobLevel2 job)
        {
            return GetWorkerText(job, string.Empty);
        }

        /// <summary>
        /// Worker for Text: Paste2, Pastebin
        /// </summary>
        /// <returns></returns>
        public virtual WorkerTask GetWorkerText(WorkerTask.JobLevel2 job, string localFilePath)
        {
            WorkerTask task = CreateTask(job);
            foreach (int i in Engine.conf.MyTextUploaders)
            {
                task.MyTextUploaders.Add((TextUploaderType)i);
            }
            if (!string.IsNullOrEmpty(localFilePath))
            {
                task.UpdateLocalFilePath(localFilePath);
            }

            return task;
        }

        #endregion Create Tasks

        #region User Tasks

        public void StartBw_EntireScreen()
        {
            StartWorkerScreenshots(WorkerTask.JobLevel2.CaptureEntireScreen);
        }

        public void StartBw_SelectedWindow()
        {
            StartWorkerScreenshots(WorkerTask.JobLevel2.CaptureSelectedWindow);
        }

        public void StartBw_CropShot()
        {
            StartWorkerScreenshots(WorkerTask.JobLevel2.CaptureRectRegion);
        }

        public void StartBw_FreehandCropShot()
        {
            StartWorkerScreenshots(WorkerTask.JobLevel2.CaptureFreeHandRegion);
        }

        public void StartBw_ClipboardUpload()
        {
            WorkerTask task = CreateTask(WorkerTask.JobLevel2.UploadFromClipboard);

            List<WorkerTask> textWorkers = new List<WorkerTask>();

            if (Clipboard.ContainsImage())
            {
                task.SetImage(Clipboard.GetImage());
                if (task.SetFilePathFromPattern(new NameParser(NameParserType.EntireScreen).Convert(Engine.conf.EntireScreenPattern)))
                {
                    FileSystem.WriteImage(task);
                    StartWorkerPictures(task, task.LocalFilePath);
                }
            }
            else if (Clipboard.ContainsText())
            {
                WorkerTask temp = GetWorkerText(WorkerTask.JobLevel2.UploadFromClipboard);
                string fp = FileSystem.GetUniqueFilePath(Path.Combine(Engine.TextDir, new NameParser().Convert("%y.%mo.%d-%h.%mi.%s") + ".txt"));
                string cbText = Clipboard.GetText();
                FileSystem.WriteText(fp, cbText);
                temp.UpdateLocalFilePath(fp);
                temp.SetText(cbText);
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
            StartWorkerPictures(CreateTask(WorkerTask.JobLevel2.PROCESS_DRAG_N_DROP), fp);
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
                    Engine.MyLogger.WriteException(ex, "Error while uploading using file system");
                    succ = false;
                }
            }

            List<WorkerTask> textWorkers = new List<WorkerTask>();

            foreach (string fp in strListFilePath)
            {
                if (GraphicsMgr.IsValidImage(fp))
                {
                    StartWorkerPictures(CreateTask(WorkerTask.JobLevel2.UploadFromClipboard), fp);
                }
                else if (FileSystem.IsValidTextFile(fp))
                {
                    WorkerTask temp = GetWorkerText(WorkerTask.JobLevel2.UploadFromClipboard);
                    temp.UpdateLocalFilePath(fp);
                    temp.SetText(File.ReadAllText(fp));
                    textWorkers.Add(temp);
                }
                else
                {
                    StartWorkerBinary(WorkerTask.JobLevel2.UploadFromClipboard, fp);
                }
            }

            StartTextWorkers(textWorkers);
            return succ;
        }

        /// <summary>
        /// Worker for Screenshots: Active Window, Crop, Entire Screen
        /// </summary>
        /// <param name="job">Job Type</param>
        public void StartWorkerScreenshots(WorkerTask.JobLevel2 job)
        {
            Engine.ClipboardUnhook();
            WorkerTask task = CreateTask(job);
            task.WasToTakeScreenshot = true;
            task.MyWorker.RunWorkerAsync(task);
        }

        /// <summary>
        /// Worker for Images: Drag n Drop, Image from Clipboard, Custom Uploader
        /// </summary>
        /// <param name="job">Job Type</param>
        /// <param name="localFilePath">Local file path of the image</param>
        public void StartWorkerPictures(WorkerTask task, string localFilePath)
        {
            Engine.ClipboardUnhook();
            task.UpdateLocalFilePath(localFilePath);
            task.SetImage(localFilePath);
            task.MyWorker.RunWorkerAsync(task);
        }

        public void StartWorkerPictures(WorkerTask.JobLevel2 job, Image img)
        {
            Engine.ClipboardUnhook();
            WorkerTask task = CreateTask(job);
            task.SetImage(img);
            task.MyWorker.RunWorkerAsync(task);
        }

        protected void StartTextWorkers(List<WorkerTask> textWorkers)
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

        /// <summary>
        /// Worker for Binary: Drag n Drop, Clipboard Upload files from Explorer
        /// </summary>
        /// <param name="job">Job Type</param>
        /// <param name="localFilePath">Local file path of the file</param>
        protected void StartWorkerBinary(WorkerTask.JobLevel2 job, string localFilePath)
        {
            WorkerTask t = CreateTask(job);
            t.UpdateLocalFilePath(localFilePath);
            t.MyWorker.RunWorkerAsync(t);
        }

        #endregion User Tasks
    }
}
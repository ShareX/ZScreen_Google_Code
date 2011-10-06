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
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Crop;
using Gif.Components;
using GraphicsMgrLib;
using HelpersLib;
using HistoryLib;
using ImageQueue;
using Microsoft.WindowsAPICodePack.Taskbar;
using ScreenCapture;
using UploadersAPILib;
using UploadersLib;
using UploadersLib.FileUploaders;
using UploadersLib.GUI;
using UploadersLib.HelperClasses;
using UploadersLib.ImageUploaders;
using UploadersLib.OtherServices;
using UploadersLib.TextUploaders;
using UploadersLib.URLShorteners;
using ZScreenLib.Properties;
using ZSS.IndexersLib;
using ZUploader.HelperClasses;

namespace ZScreenLib
{
    public class WorkerTask : IDisposable
    {
        public delegate void TaskEventHandler(WorkerTask wt);

        public event TaskEventHandler UploadStarted;
        public event TaskEventHandler UploadPreparing;
        public event TaskEventHandler UploadProgressChanged2;
        public event TaskEventHandler UploadCompleted;
        public TaskStatus Status { get; private set; }
        public bool IsWorking { get { return Status == TaskStatus.Preparing || Status == TaskStatus.Uploading; } }
        public bool IsStopped { get; private set; }

        #region Enums

        public enum TaskState
        {
            Created,
            Prepared,
            Started,
            RetryPending,
            ThreadMode,
            ImageProcessed,
            ImageEdited,
            ImageWritten,
            CancellationPending,
            Finished
        }

        public enum JobLevel2
        {
            [Description("Capture Entire Screen")]
            CaptureEntireScreen,
            [Description("Capture Active Window")]
            CaptureActiveWindow,
            [Description("Capture Window")]
            CaptureSelectedWindow,
            [Description("Capture Rectangular Region")]
            CaptureRectRegion,
            [Description("Capture Previous Rectangular Region")]
            CaptureLastCroppedWindow,
            [Description("Auto Capture")]
            AutoCapture,
            [Description("Clipboard Upload")]
            UploadFromClipboard,
            [Description("Upload from Explorer")]
            UploadFromExplorer,
            [Description("Language Translator")]
            Translate,
            [Description("Screen Color Picker")]
            SCREEN_COLOR_PICKER,
            [Description("Upload Image")]
            UploadImage,
            [Description("Webpage Capture")]
            WEBPAGE_CAPTURE,
            [Description("Capture Shape")]
            CaptureFreeHandRegion
        }

        public enum JobLevel3
        {
            [Description("None")]
            None,
            [Description("Upload Text")]
            UploadText,
            [Description("Shorten URL")]
            ShortenURL,
            [Description("Index Folder")]
            IndexFolder,
            [Description("Created Animated Image")]
            CreateAnimatedImage
        }

        public enum ProgressType : int
        {
            COPY_TO_CLIPBOARD_IMAGE, // needed only for the feature CopyImageUntilURL
            FLASH_ICON,
            INCREMENT_PROGRESS,
            UPDATE_STATUS_BAR_TEXT,
            UPDATE_PROGRESS_MAX,
            UPDATE_TRAY_TITLE,
            UpdateCropMode,
            CHANGE_TRAY_ICON_PROGRESS,
            ShowBalloonTip,
            ShowTrayWarning,
            PrintText,
            PrintImage
        }

        #endregion Enums

        #region Properties

        public int ID { get; set; }
        public ProgressManager Progress { get; set; }

        public BackgroundWorker MyWorker { get; set; }
        public Workflow WorkflowConfig { get; private set; }

        public bool WasToTakeScreenshot { get; set; }

        public JobLevel1 Job1 { get; private set; }  // Image, File, Text
        public JobLevel2 Job2 { get; private set; }  // Entire Screen, Active Window, Selected Window, Crop Shot, etc.
        public JobLevel3 Job3 { get; private set; }  // Shorten URL, Upload Text, Index Folder, etc.

        public List<string> Errors { get; set; }

        public bool IsError
        {
            get { return Errors != null && Errors.Count > 0; }
        }

        public List<TaskState> States = new List<TaskState>();

        public DateTime StartTime { get; set; }

        private DateTime mEndTime;

        public DateTime EndTime
        {
            get
            {
                return mEndTime;
            }
            set
            {
                mEndTime = value;
                UploadDuration = (int)Math.Round((mEndTime - StartTime).TotalMilliseconds);
            }
        }

        public int UploadDuration { get; set; }

        public bool IsImage { get; private set; }

        public List<Image> tempImages;
        public Image tempImage { get; private set; }
        public string TempText { get; private set; }
        private Stream Data;

        public GoogleTranslateInfo TranslationInfo { get; private set; }

        public string FileName { get; private set; }
        public string FileSize { get; private set; }

        public string LocalFilePath { get; private set; }

        private string DestinationName = string.Empty;

        public List<ClipboardContentEnum> TaskClipboardContent = new List<ClipboardContentEnum>();
        public List<LinkFormatEnum> MyLinkFormat = new List<LinkFormatEnum>();
        public List<ImageUploaderType> MyImageUploaders = new List<ImageUploaderType>();
        public List<TextUploaderType> MyTextUploaders = new List<TextUploaderType>();
        public List<UrlShortenerType> MyLinkUploaders = new List<UrlShortenerType>();
        public List<FileUploaderType> MyFileUploaders = new List<FileUploaderType>();

        public List<UploadResult> UploadResults { get; private set; }
        public UploadResult Result
        {
            get
            {
                return GetResult();
            }
        }

        #endregion Properties

        #region Constructors

        public WorkerTask(Workflow wf)
        {
            UploadResults = new List<UploadResult>();
            Errors = new List<string>();
            States.Add(TaskState.Created);
            MyWorker = new BackgroundWorker() { WorkerReportsProgress = true };

            IClone cm = new CloneManager();
            WorkflowConfig = cm.Clone<Workflow>(wf);
        }

        public WorkerTask(BackgroundWorker worker, Workflow wf)
            : this(wf)
        {
            MyWorker = worker;

            if (wf.Outputs.Contains(OutputEnum.Clipboard) && TaskClipboardContent.Count == 0)
            {
                TaskClipboardContent.Add(ClipboardContentEnum.Data);
            }

            StartWork(wf.Job);

            if (wf.Outputs.Contains(OutputEnum.LocalDisk))
            {
                // write image
            }
        }

        public WorkerTask(BackgroundWorker worker, JobLevel2 job, DestSelector ucDestOptions, string fp = "")
            : this(Engine.Workflow)
        {
            if (!string.IsNullOrEmpty(fp))
            {
                UpdateLocalFilePath(fp);
            }

            MyWorker = worker;
            StartWork(job);

            if (PrepareOutputs(ucDestOptions) == DialogResult.Cancel)
            {
                this.States.Add(TaskState.CancellationPending);
            }
        }

        public void StartWork(JobLevel2 job)
        {
            Job2 = job;

            switch (job)
            {
                case JobLevel2.UploadFromExplorer:
                case JobLevel2.UploadFromClipboard:
                    Job1 = JobLevel1.File;
                    break;
                case JobLevel2.Translate:
                    Job1 = JobLevel1.Text;
                    break;
                default:
                    Job1 = JobLevel1.Image;
                    break;
            }

            bool success = true;

            switch (job)
            {
                case JobLevel2.CaptureActiveWindow:
                    success = CaptureActiveWindow();
                    break;
                case JobLevel2.CaptureEntireScreen:
                    success = CaptureScreen();
                    break;
                case JobLevel2.CaptureSelectedWindow:
                case JobLevel2.CaptureRectRegion:
                case JobLevel2.CaptureLastCroppedWindow:
                    success = CaptureRegionOrWindow();
                    break;
                case JobLevel2.CaptureFreeHandRegion:
                    success = CaptureShape();
                    break;
            }

            if (!success)
            {
                this.States.Add(TaskState.CancellationPending);
            }
        }

        private DialogResult PrepareOutputs(DestSelector ucDestOptions)
        {
            DialogResult dlgResult = DialogResult.OK;

            if (!States.Contains(TaskState.Prepared) && !States.Contains(TaskState.CancellationPending))
            {
                Adapter.SaveMenuConfigToList<OutputEnum>(ucDestOptions.tsddbOutputs, WorkflowConfig.Outputs);
                Adapter.SaveMenuConfigToList<ClipboardContentEnum>(ucDestOptions.tsddbClipboardContent, TaskClipboardContent);
                Adapter.SaveMenuConfigToList<LinkFormatEnum>(ucDestOptions.tsddbLinkFormat, MyLinkFormat);
                Adapter.SaveMenuConfigToList<ImageUploaderType>(ucDestOptions.tsddbDestImage, MyImageUploaders);
                Adapter.SaveMenuConfigToList<TextUploaderType>(ucDestOptions.tsddbDestText, MyTextUploaders);
                Adapter.SaveMenuConfigToList<FileUploaderType>(ucDestOptions.tsddbDestFile, MyFileUploaders);
                Adapter.SaveMenuConfigToList<UrlShortenerType>(ucDestOptions.tsddbDestLink, MyLinkUploaders);

                if (Engine.conf.PromptForOutputs)
                {
                    dlgResult = SetManualOutputs(LocalFilePath);
                }
                States.Add(TaskState.Prepared);
            }

            return dlgResult;
        }

        #endregion Constructors

        public void Start()
        {
            if (Status == TaskStatus.InQueue && !IsStopped)
            {
                OnUploadPreparing();

                //  UploadManager.UpdateProxySettings();

                MyWorker = new BackgroundWorker();
                MyWorker.WorkerReportsProgress = true;
                MyWorker.DoWork += new DoWorkEventHandler(MyWorker_DoWork);
                MyWorker.ProgressChanged += new ProgressChangedEventHandler(MyWorker_ProgressChanged);
                MyWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(MyWorker_RunWorkerCompleted);
                MyWorker.RunWorkerAsync();
            }
        }

        private void MyWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            //  throw new NotImplementedException();
        }

        private void MyWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            switch ((TaskProgress)e.ProgressPercentage)
            {
                case TaskProgress.ReportStarted:
                    OnUploadStarted();
                    break;
                case TaskProgress.ReportProgress:
                    ProgressManager progress = e.UserState as ProgressManager;
                    if (progress != null)
                    {
                        Progress = progress;
                        // OnUploadProgressChanged2();
                    }
                    break;
            }
        }

        private void MyWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            // OnUploadCompleted();
        }

        #region Task Events

        private void OnUploadPreparing()
        {
            Status = TaskStatus.Preparing;

            switch (Job1)
            {
                case JobLevel1.Image:
                case JobLevel1.Text:
                    Status = TaskStatus.Preparing;
                    break;
                default:
                    Status = TaskStatus.InQueue;
                    break;
            }

            if (UploadPreparing != null)
            {
                UploadPreparing(this);
            }
        }

        private void OnUploadStarted()
        {
            if (UploadStarted != null)
            {
                UploadStarted(this);
            }
        }

        private void OnUploadProgressChanged()
        {
            if (UploadProgressChanged2 != null)
            {
                UploadProgressChanged2(this);
            }
        }

        private void OnUploadCompleted()
        {
            Status = TaskStatus.Completed;

            if (!IsStopped)
            {
                Status = TaskStatus.Completed;
            }
            else
            {
                Status = TaskStatus.Stopped;
            }

            if (UploadCompleted != null)
            {
                UploadCompleted(this);
            }

            Dispose();
        }

        #endregion Task Events

        #region Populating Task

        public void SetImages(List<Image> tempImages)
        {
            Job3 = JobLevel3.CreateAnimatedImage;
            this.tempImages = tempImages;
        }

        public bool SetImage(string savePath)
        {
            return SetImage(GraphicsMgr.GetImageSafely(savePath), savePath);
        }

        public bool SetImage(Image img, string savePath = "")
        {
            if (img != null)
            {
                tempImage = img;
                Job1 = JobLevel1.Image;

                StaticHelper.WriteLine(string.Format("Setting Image {0}x{1} to WorkerTask", img.Width, img.Height));

                if (Engine.conf != null && Engine.conf.ShowOutputsAsap)
                {
                    // IF (Bitmap)img.Clone() IS NOT USED THEN WE ARE GONNA GET CROSS THREAD OPERATION ERRORS! - McoreD
                    MyWorker.ReportProgress((int)WorkerTask.ProgressType.COPY_TO_CLIPBOARD_IMAGE, (Bitmap)img.Clone());
                }

                // UpdateLocalFilePath needs to happen before Image is processed
                EImageFormat imageFormat = WorkflowConfig.ImageFormat;
                if (!string.IsNullOrEmpty(savePath))
                {
                    UpdateLocalFilePath(savePath);
                    Data = PrepareData(savePath);
                }
                else
                {
                    // Prepare data so that we have the correct file extension for Image Editor
                    Data = WorkerTaskHelper.PrepareImage(WorkflowConfig, tempImage, out imageFormat, bTargetFileSize: false);
                    string fn = WorkerTaskHelper.PrepareFilename(WorkflowConfig, tempImage, imageFormat, GetPatternType());
                    string imgfp = FileSystem.GetUniqueFilePath(WorkflowConfig, Engine.ImagesDir, fn);
                    UpdateLocalFilePath(imgfp);
                }

                if (!States.Contains(TaskState.ImageProcessed))
                {
                    States.Add(TaskState.ImageProcessed);
                    ProcessImage(tempImage);
                }

                // PerformActions should happen in main thread
                if (string.IsNullOrEmpty(savePath) && tempImage != null && !States.Contains(TaskState.ImageEdited))
                {
                    States.Add(TaskState.ImageEdited);
                    if (Adapter.ActionsEnabled() && Job2 != WorkerTask.JobLevel2.UploadImage)
                    {
                        PerformActions();
                    }
                }
            }

            return tempImage != null;
        }

        public void SetText(string text)
        {
            Job1 = JobLevel1.Text;
            TempText = text;

            string fptxt = FileSystem.GetUniqueFilePath(Engine.Workflow, Engine.TextDir, new NameParser().Convert("%y.%mo.%d-%h.%mi.%s") + ".txt");
            UpdateLocalFilePath(fptxt);

            if (Directory.Exists(text))
            {
                Job3 = WorkerTask.JobLevel3.IndexFolder;

                IndexerAdapter settings = new IndexerAdapter();
                settings.LoadConfig(Engine.conf.IndexerConfig);
                Engine.conf.IndexerConfig.FolderList.Clear();
                string ext = ".log";
                if (MyTextUploaders.Contains(TextUploaderType.FileUploader))
                {
                    ext = ".html";
                }
                FileName = Path.GetFileName(TempText) + ext;
                settings.GetConfig().SetSingleIndexPath(Path.Combine(Engine.TextDir, FileName));
                settings.GetConfig().FolderList.Add(TempText);

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
                    UpdateLocalFilePath(settings.GetConfig().GetIndexFilePath());
                }
            }
            else if (FileSystem.IsValidLink(text))
            {
                Job3 = WorkerTask.JobLevel3.ShortenURL;
            }
            else
            {
                Job3 = WorkerTask.JobLevel3.UploadText;
            }
        }

        /// <summary>
        /// Sets the file to save the image to.
        /// If the user activated the "prompt for filename" option, then opens a dialog box.
        /// </summary>
        /// <param name="pattern">the base name</param>
        /// <returns>true if the screenshot should be saved, or false if the user canceled</returns>
        public DialogResult SetManualOutputs(string filePath)
        {
            DialogResult dlgResult = DialogResult.OK;
            if (Engine.conf.PromptForOutputs)
            {
                DestOptions dialog = new DestOptions(this)
                {
                    Title = "Configure Outputs...",
                    FilePath = filePath,
                    Icon = Resources.zss_main
                };
                NativeMethods.SetForegroundWindow(dialog.Handle);
                dlgResult = dialog.ShowDialog();
                if (dlgResult == DialogResult.OK)
                {
                    if (!string.IsNullOrEmpty(dialog.FilePath))
                    {
                        UpdateLocalFilePath(dialog.FilePath);
                    }
                }
            }
            return dlgResult;
        }

        public void AddUploadResult(UploadResult ur)
        {
            if (ur != null && !ExistsUploadResult(ur))
            {
                string fp = LocalFilePath;
                if (Engine.IsPortable)
                {
                    fp = Path.Combine(Application.StartupPath, fp);
                    UpdateLocalFilePath(fp);
                }
                if (File.Exists(fp))
                {
                    ur.LocalFilePath = fp;
                }
                if (!string.IsNullOrEmpty(ur.LocalFilePath) || !string.IsNullOrEmpty(ur.URL))
                {
                    UploadResults.Add(ur);
                    if (Engine.conf.ShowOutputsAsap)
                    {
                        MyWorker.ReportProgress((int)ProgressType.ShowBalloonTip, this);
                    }
                }
            }
        }

        private bool ExistsUploadResult(UploadResult ur2)
        {
            foreach (UploadResult ur1 in UploadResults)
            {
                if (ur2.Host == ur1.Host)
                {
                    return true;
                }
            }

            return false;
        }

        public void UpdateLocalFilePath(string fp)
        {
            if (!string.IsNullOrEmpty(fp))
            {
                LocalFilePath = Engine.IsPortable ? Path.Combine(Application.StartupPath, fp) : fp;
                FileName = Path.GetFileName(fp);

                if (ZAppHelper.IsTextFile(fp))
                {
                    Job1 = JobLevel1.Text;
                }
                else if (ZAppHelper.IsImageFile(fp))
                {
                    Job1 = JobLevel1.Image;
                    IsImage = true;
                    if (tempImage == null && GraphicsMgr.IsValidImage(fp))
                    {
                        tempImage = FileSystem.ImageFromFile(fp);
                    }
                }
                else
                {
                    Job1 = JobLevel1.File;
                }
            }
        }

        #endregion Populating Task

        #region Capture

        private bool CaptureRegionOrWindow(Image imgSS, bool windowMode)
        {
            using (Crop c = new Crop(imgSS, windowMode))
            {
                if (c.ShowDialog() == DialogResult.OK)
                {
                    if (Job2 == WorkerTask.JobLevel2.CaptureRectRegion && !Engine.conf.LastRegion.IsEmpty)
                    {
                        return SetImage(GraphicsMgr.CropImage(imgSS, Engine.conf.LastRegion));
                    }
                    else if (windowMode && !Engine.conf.LastCapture.IsEmpty)
                    {
                        return SetImage(GraphicsMgr.CropImage(imgSS, Engine.conf.LastCapture));
                    }
                }
            }

            return false;
        }

        private bool CaptureRectangle(Image imgSS)
        {
            return CaptureRegionOrWindow(imgSS, false);
        }

        private bool CaptureWindow(Image imgSS)
        {
            return CaptureRegionOrWindow(imgSS, true);
        }

        public bool CaptureRegionOrWindow()
        {
            if (!Engine.IsTakingScreenShot)
            {
                Engine.IsTakingScreenShot = true;

                bool windowMode = Job2 == WorkerTask.JobLevel2.CaptureSelectedWindow;

                if (Engine.conf == null) Engine.conf = new XMLSettings();

                try
                {
                    using (Image imgSS = Capture.CaptureScreen(Engine.Workflow.DrawCursor))
                    {
                        if (Job2 == WorkerTask.JobLevel2.CaptureLastCroppedWindow && !Engine.conf.LastRegion.IsEmpty)
                        {
                            SetImage(GraphicsMgr.CropImage(imgSS, Engine.conf.LastRegion));
                        }

                        else if (Job2 == WorkerTask.JobLevel2.CaptureSelectedWindow)
                        {
                            CaptureWindow(imgSS);
                        }

                        else
                        {
                            switch (Engine.conf.CropEngineMode)
                            {
                                case CropEngineType.CropLite:
                                    using (CropLight crop = new CropLight(imgSS))
                                    {
                                        if (crop.ShowDialog() == DialogResult.OK)
                                        {
                                            SetImage(GraphicsMgr.CropImage(imgSS, crop.SelectionRectangle));
                                        }
                                    }
                                    break;
                                case CropEngineType.Cropv1:
                                    CaptureRectangle(imgSS);
                                    break;
                                case CropEngineType.Cropv2:
                                    using (Crop2 crop = new Crop2(imgSS))
                                    {
                                        if (crop.ShowDialog() == DialogResult.OK)
                                        {
                                            SetImage(crop.GetCroppedScreenshot());
                                        }
                                    }
                                    break;
                                case CropEngineType.Cropv3:
                                    Surface surface = new RectangleRegion(imgSS);
                                    surface.Config = Engine.conf.SurfaceConfig;
                                    if (surface.ShowDialog() == DialogResult.OK)
                                    {
                                        SetImage(surface.GetRegionImage());
                                    }
                                    break;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    StaticHelper.WriteException(ex, "Error while capturing region");
                    Errors.Add(ex.Message);
                    if (Engine.conf.CaptureEntireScreenOnError)
                    {
                        CaptureScreen();
                    }
                }
                finally
                {
                    MyWorker.ReportProgress((int)WorkerTask.ProgressType.UpdateCropMode);
                    Engine.IsTakingScreenShot = false;
                }
            }

            return tempImage != null;
        }

        public bool CaptureShape()
        {
            Screenshot.DrawCursor = Engine.Workflow.DrawCursor;
            RegionCapturePreview rcp = new RegionCapturePreview(Engine.conf.SurfaceConfig);

            if (rcp.ShowDialog() == DialogResult.OK)
            {
                SetImage(rcp.Result);
            }

            return tempImage != null;
        }

        /// <summary>
        /// Function to Capture Active Window
        /// </summary>
        public bool CaptureActiveWindow()
        {
            if (tempImage == null)
            {
                SetImage(Capture.CaptureActiveWindow(this.WorkflowConfig));
            }
            return tempImage != null;
        }

        /// <summary>
        /// Function to Capture Entire Screen
        /// </summary>
        public bool CaptureScreen()
        {
            if (tempImage == null)
            {
                SetImage(Capture.CaptureScreen(Engine.conf != null && Engine.Workflow.DrawCursor));
            }

            return tempImage != null;
        }

        #endregion Capture

        #region Google Translate

        public void SetTranslationInfo(GoogleTranslateInfo gti)
        {
            Job1 = JobLevel1.Text;
            TranslationInfo = gti;
        }

        #endregion Google Translate

        #region Edit Image

        private void ProcessImage(Image img)
        {
            bool window = Job2 == JobLevel2.CaptureActiveWindow || Job2 == JobLevel2.CaptureSelectedWindow || Job2 == JobLevel2.CaptureEntireScreen;

            if (img != null)
            {
                if (!window)
                {
                    // Add Rounded corners
                    bool roundedShadowCorners = false;
                    if (Engine.conf.ImageAddRoundedCorners)
                    {
                        img = GraphicsMgr.RemoveCorners(img, null);
                        roundedShadowCorners = true;
                    }

                    // Add shadows
                    if (Engine.conf.ImageAddShadow)
                    {
                        img = GraphicsMgr.AddBorderShadow(img, roundedShadowCorners);
                    }
                }

                // Watermark
                img = ImageEffects.ApplySizeChanges(img);
                img = ImageEffects.ApplyScreenshotEffects(img);
                if (Job2 != WorkerTask.JobLevel2.UploadFromClipboard || !Engine.conf.WatermarkExcludeClipboardUpload)
                {
                    img = ImageEffects.ApplyWatermark(img);
                }

                tempImage = img;
            }
        }

        /// <summary>
        /// Perform Actions after capturing image/text/file objects
        /// </summary>
        public void PerformActions()
        {
            foreach (Software app in Engine.conf.ActionsAppsUser)
            {
                if (app.Enabled)
                {
                    if (Job1 == JobLevel1.File && app.TriggerForFiles ||
                        Job1 == JobLevel1.Image && app.TriggerForImages && !WasToTakeScreenshot ||
                        Job1 == JobLevel1.Image && app.TriggerForScreenshots && WasToTakeScreenshot ||
                        Job1 == JobLevel1.Text && app.TriggerForText)
                    {
                        if (app.Name == Engine.zImageAnnotator)
                        {
                            try
                            {
                                Greenshot.Helpers.Capture capture = new Greenshot.Helpers.Capture(tempImage);
                                capture.CaptureDetails.Filename = LocalFilePath;
                                capture.CaptureDetails.Title = Path.GetFileNameWithoutExtension(capture.CaptureDetails.Filename);
                                capture.CaptureDetails.AddMetaData("file", capture.CaptureDetails.Filename);
                                capture.CaptureDetails.AddMetaData("source", "file");
                                Greenshot.Drawing.Surface surface = new Greenshot.Drawing.Surface(capture);
                                Greenshot.ImageEditorForm editor = new Greenshot.ImageEditorForm(surface, WorkflowConfig.Outputs.Contains(OutputEnum.LocalDisk)) { Icon = Resources.zss_main };
                                editor.SetImagePath(LocalFilePath);
                                editor.Visible = false;
                                editor.ShowDialog();
                                if (!editor.surface.Modified)
                                {
                                    tempImage = editor.GetImageForExport();
                                }
                            }
                            catch (Exception ex)
                            {
                                StaticHelper.WriteException(ex, "ImageEdit");
                            }
                        }
                        else if (app.Name == Engine.zImageEffects)
                        {
                            ImageEffectsGUI effects = new ImageEffectsGUI(tempImage);
                            effects.ShowDialog();
                            tempImage = effects.GetImageForExport();
                        }
                        else if (File.Exists(app.Path))
                        {
                            app.OpenFile(LocalFilePath);
                        }
                    }
                    StaticHelper.WriteLine(string.Format("Performed Actions using {0}.", app.Name));
                }
            }
        }

        #endregion Edit Image

        #region Publish Data

        /// <summary>
        /// Beginining of the background worker tasks
        /// </summary>
        public void PublishData()
        {
            StartTime = DateTime.Now;

            Data = PrepareData();

            StaticHelper.WriteLine(string.Format("Job started: {0}", Job2));

            if (File.Exists(LocalFilePath) || tempImage != null || !string.IsNullOrEmpty(TempText))
            {
                if (WorkflowConfig.Outputs.Contains(OutputEnum.LocalDisk))
                {
                    switch (Job1)
                    {
                        case JobLevel1.Text:
                            FileSystem.WriteText(LocalFilePath, TempText);
                            break;
                        default:
                            WriteImage();
                            break;
                    }
                }

                if (WorkflowConfig.Outputs.Contains(OutputEnum.Clipboard))
                {
                    SetClipboardContent();
                }

                if (WorkflowConfig.Outputs.Contains(OutputEnum.Printer))
                {
                    Print();
                }

                if (WorkflowConfig.Outputs.Contains(OutputEnum.RemoteHost))
                {
                    switch (Job1)
                    {
                        case JobLevel1.Image:
                            UploadImage();
                            break;

                        case JobLevel1.Text:
                            switch (Job2)
                            {
                                case WorkerTask.JobLevel2.Translate:
                                    SetTranslationInfo(new GoogleTranslate(ZKeys.GoogleApiKey).TranslateText(TranslationInfo));
                                    SetText(TranslationInfo.Result);
                                    break;
                                default:
                                    UploadText();
                                    break;
                            }
                            break;

                        case JobLevel1.File:
                            UploadFile();
                            break;
                    }
                }

                if (WorkflowConfig.Outputs.Contains(OutputEnum.Email))
                {
                    SendEmail();
                }

                if (WorkflowConfig.Outputs.Contains(OutputEnum.SharedFolder))
                {
                    UploadToSharedFolder();
                }

                if (UploadResults.Count > 0)
                {
                    FlashIcon();
                }
            }

            EndTime = DateTime.Now;
        }

        private Stream PrepareData(string fp)
        {
            Stream data = null;
            using (FileStream fs = new FileStream(fp, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                data = new MemoryStream();
                fs.CopyStreamTo(data);
            }
            return data;
        }

        private Stream PrepareData()
        {
            Stream data = null;

            if (Job3 == JobLevel3.CreateAnimatedImage)
            {
                WriteImageAnimated();
            }

            if (File.Exists(LocalFilePath))
            {
                StaticHelper.WriteLine("Preparing data from " + LocalFilePath);
                data = PrepareData(LocalFilePath);
            }
            else if (tempImage != null)
            {
                StaticHelper.WriteLine("Preparing data from image");
                EImageFormat imageFormat;
                data = WorkerTaskHelper.PrepareImage(WorkflowConfig, tempImage, out imageFormat, bTargetFileSize: true);
            }
            else if (!string.IsNullOrEmpty(TempText))
            {
                StaticHelper.WriteLine("Preparing data from text");
                data = new MemoryStream(Encoding.UTF8.GetBytes(TempText));
            }

            SetFileSize(data.Length);

            return data;
        }

        private void SetFileSize(long sz)
        {
            FileSize = string.Format("{0} KiB", (sz / 1024.0).ToString("0"));
        }

        /// <summary>
        /// Writes MyImage object in a WorkerTask into a file
        /// </summary>
        /// <param name="t">WorkerTask</param>
        public void WriteImage()
        {
            if (WorkflowConfig.Outputs.Contains(OutputEnum.LocalDisk) && tempImage != null && !States.Contains(TaskState.ImageWritten))
            {
                string fp = LocalFilePath;
                Image img = tempImage;
                FileInfo fi = FileSystem.WriteImage(fp, PrepareData()); // PrepareData instead of using Data
                fp = fi.FullName;
                this.SetFileSize(fi.Length);

                States.Add(TaskState.ImageWritten);
                UploadResult ur = new UploadResult()
                {
                    Host = OutputEnum.LocalDisk.GetDescription(),
                    LocalFilePath = LocalFilePath,
                };
                if (!WorkflowConfig.Outputs.Contains(OutputEnum.RemoteHost))
                {
                    ur.URL = ur.GetLocalFilePathAsUri(LocalFilePath);
                    AddUploadResult(ur);
                }

                if (!File.Exists(LocalFilePath))
                {
                    Errors.Add(string.Format("{0} does not exist", LocalFilePath));
                }
            }
        }

        private void WriteImageAnimated()
        {
            if (tempImages != null && tempImages.Count > 0)
            {
                String outputFilePath = FileSystem.GetUniqueFilePath(WorkflowConfig, Engine.ImagesDir,
                    new NameParser(NameParserType.EntireScreen).Convert(WorkflowConfig.EntireScreenPattern));

                switch (WorkflowConfig.ImageFormatAnimated)
                {
                    case AnimatedImageFormat.PNG:
                        outputFilePath += ".png";
                        SharpApng.Apng apng = new SharpApng.Apng();
                        foreach (Image img in tempImages)
                        {
                            apng.AddFrame(new Bitmap(img), 500, 1);
                        }

                        apng.WriteApng(outputFilePath);
                        break;

                    default:
                        outputFilePath += ".gif";
                        AnimatedGifEncoder enc = new AnimatedGifEncoder();
                        enc.Start(outputFilePath);
                        enc.SetDelay(WorkflowConfig.ImageAnimatedFramesDelay * 100);
                        enc.SetRepeat(0);
                        foreach (Image img in tempImages)
                        {
                            enc.AddFrame(img);
                        }
                        enc.Finish();
                        break;
                }

                UpdateLocalFilePath(outputFilePath);
                StaticHelper.WriteLine("Wrote animated image: " + outputFilePath);
                tempImages.Clear();
            }
        }

        public void UploadImage()
        {
            if (WorkflowConfig.Outputs.Contains(OutputEnum.RemoteHost))
            {
                if (Engine.conf != null && Engine.conf.TinyPicSizeCheck && MyImageUploaders.Contains(ImageUploaderType.TINYPIC) && File.Exists(LocalFilePath))
                {
                    SizeF size = Image.FromFile(LocalFilePath).PhysicalDimension;
                    if (size.Width > 1600 || size.Height > 1600)
                    {
                        StaticHelper.WriteLine("Changing from TinyPic to ImageShack due to large image size");
                        if (!MyImageUploaders.Contains(ImageUploaderType.IMAGESHACK))
                        {
                            MyImageUploaders.Add(ImageUploaderType.IMAGESHACK);
                            MyImageUploaders.Remove(ImageUploaderType.TINYPIC);
                        }
                    }
                }

                for (int i = 0; i < MyImageUploaders.Count; i++)
                {
                    UploadImage(MyImageUploaders[i], Data);
                }
            }

            if (Engine.conf != null && Engine.conf.ImageUploadRetryOnTimeout && UploadDuration > (int)Engine.conf.UploadDurationLimit)
            {
                if (!MyImageUploaders.Contains(ImageUploaderType.TINYPIC))
                {
                    MyImageUploaders.Add(ImageUploaderType.TINYPIC);
                }
                else if (!MyImageUploaders.Contains(ImageUploaderType.TINYPIC))
                {
                    MyImageUploaders.Add(ImageUploaderType.IMAGESHACK);
                }
            }
        }

        private void UploadImage(ImageUploaderType imageUploaderType, Stream data)
        {
            ImageUploader imageUploader = null;

            switch (imageUploaderType)
            {
                case ImageUploaderType.IMAGESHACK:
                    imageUploader = new ImageShackUploader(ZKeys.ImageShackKey, Engine.Workflow.OutputsConfig.ImageShackAccountType,
                        Engine.Workflow.OutputsConfig.ImageShackRegistrationCode)
                    {
                        IsPublic = Engine.Workflow.OutputsConfig.ImageShackShowImagesInPublic
                    };
                    break;
                case ImageUploaderType.TINYPIC:
                    imageUploader = new TinyPicUploader(ZKeys.TinyPicID, ZKeys.TinyPicKey, Engine.Workflow.OutputsConfig.TinyPicAccountType,
                        Engine.Workflow.OutputsConfig.TinyPicRegistrationCode);
                    break;
                case ImageUploaderType.IMGUR:
                    imageUploader = new Imgur(Engine.Workflow.OutputsConfig.ImgurAccountType, ZKeys.ImgurAnonymousKey, Engine.Workflow.OutputsConfig.ImgurOAuthInfo)
                    {
                        ThumbnailType = Engine.Workflow.OutputsConfig.ImgurThumbnailType
                    };
                    break;
                case ImageUploaderType.FLICKR:
                    imageUploader = new FlickrUploader(ZKeys.FlickrKey, ZKeys.FlickrSecret,
                        Engine.Workflow.OutputsConfig.FlickrAuthInfo, Engine.Workflow.OutputsConfig.FlickrSettings);
                    break;
                case ImageUploaderType.Photobucket:
                    imageUploader = new Photobucket(Engine.Workflow.OutputsConfig.PhotobucketOAuthInfo, Engine.Workflow.OutputsConfig.PhotobucketAccountInfo);
                    break;
                case ImageUploaderType.UPLOADSCREENSHOT:
                    imageUploader = new UploadScreenshot(ZKeys.UploadScreenshotKey);
                    break;
                case ImageUploaderType.MEDIAWIKI:
                    UploadToMediaWiki();
                    break;
                case ImageUploaderType.TWITPIC:
                    TwitPicOptions twitpicOpt = new TwitPicOptions();
                    twitpicOpt.Username = Engine.Workflow.OutputsConfig.TwitPicUsername;
                    twitpicOpt.Password = Engine.Workflow.OutputsConfig.TwitPicPassword;
                    // twitpicOpt.TwitPicUploadType = Engine.conf.TwitPicUploadMode;
                    twitpicOpt.TwitPicThumbnailMode = Engine.Workflow.OutputsConfig.TwitPicThumbnailMode;
                    twitpicOpt.ShowFull = Engine.Workflow.OutputsConfig.TwitPicShowFull;
                    imageUploader = new TwitPicUploader(twitpicOpt);
                    break;
                case ImageUploaderType.YFROG:
                    YfrogOptions yfrogOp = new YfrogOptions(ZKeys.ImageShackKey);
                    yfrogOp.Username = Engine.Workflow.OutputsConfig.YFrogUsername;
                    yfrogOp.Password = Engine.Workflow.OutputsConfig.YFrogPassword;
                    yfrogOp.Source = Application.ProductName;
                    // yfrogOp.UploadType = Engine.conf.YfrogUploadMode;
                    imageUploader = new YfrogUploader(yfrogOp);
                    break;
                case ImageUploaderType.TWITSNAPS:
                    imageUploader = new TwitSnapsUploader(ZKeys.TwitsnapsKey, Adapter.TwitterGetActiveAccount());
                    break;
                case ImageUploaderType.FileUploader:
                    foreach (FileUploaderType ft in MyFileUploaders)
                    {
                        UploadFile(ft, data);
                    }
                    break;
            }

            if (imageUploader != null)
            {
                imageUploader.ProgressChanged += (x) => UploadProgressChanged(x);
                DestinationName = GetActiveImageUploadersDescription();
                StaticHelper.WriteLine("Initialized " + DestinationName);

                if (data != null)
                {
                    if (Engine.conf == null)
                    {
                        Engine.conf = new XMLSettings();
                    }

                    for (int i = 0; i <= (int)Engine.conf.ErrorRetryCount; i++)
                    {
                        UploadResult ur = new UploadResult();
                        ur = imageUploader.Upload(data, FileName);
                        ur.Host = imageUploaderType.GetDescription();
                        AddUploadResult(ur);
                        Errors = imageUploader.Errors;

                        if (UploadResults.Count > 0 && string.IsNullOrEmpty(UploadResults[UploadResults.Count - 1].URL))
                        {
                            MyWorker.ReportProgress((int)ZScreenLib.WorkerTask.ProgressType.ShowTrayWarning,
                                string.Format("Retrying... Attempt {1}", imageUploaderType.GetDescription(), i + 1));
                        }
                        else
                        {
                            break;
                        }
                    }
                }
            }
        }

        public void UploadText()
        {
            MyWorker.ReportProgress((int)WorkerTask.ProgressType.UPDATE_PROGRESS_MAX, TaskbarProgressBarState.Indeterminate);

            if (ShouldShortenURL(TempText))
            {
                // Need this for shortening URL using Clipboard Upload
                ShortenURL(TempText);
            }
            else if (WorkflowConfig.Outputs.Contains(OutputEnum.RemoteHost))
            {
                foreach (TextUploaderType textUploaderType in MyTextUploaders)
                {
                    UploadText(textUploaderType);
                }
            }
        }

        private void UploadText(TextUploaderType textUploaderType)
        {
            TextUploader textUploader = null;

            switch (textUploaderType)
            {
                case TextUploaderType.PASTEBIN:
                    textUploader = new PastebinUploader(ZKeys.PastebinKey, Engine.Workflow.OutputsConfig.PastebinSettings);
                    break;
                case TextUploaderType.PASTEBIN_CA:
                    textUploader = new PastebinCaUploader(ZKeys.PastebinCaKey);
                    break;
                case TextUploaderType.PASTE2:
                    textUploader = new Paste2Uploader();
                    break;
                case TextUploaderType.SLEXY:
                    textUploader = new SlexyUploader();
                    break;
                case TextUploaderType.FileUploader:
                    UploadFile();
                    break;
            }

            if (textUploader != null)
            {
                DestinationName = textUploaderType.GetDescription();
                StaticHelper.WriteLine("Uploading to " + DestinationName);

                string url = string.Empty;

                if (!string.IsNullOrEmpty(TempText))
                {
                    url = textUploader.UploadText(TempText);
                }
                else
                {
                    url = textUploader.UploadTextFile(LocalFilePath);
                }

                AddUploadResult(new UploadResult { Host = textUploaderType.GetDescription(), URL = url });
                Errors = textUploader.Errors;
            }
        }

        /// <summary>
        /// Funtion to FTP the Screenshot
        /// </summary>
        /// <returns>Retuns a List of Screenshots</returns>
        public UploadResult UploadToFTP(int FtpAccountId, Stream data)
        {
            UploadResult ur = null;

            try
            {
                MyWorker.ReportProgress((int)WorkerTask.ProgressType.UPDATE_PROGRESS_MAX, TaskbarProgressBarState.Indeterminate);

                if (Adapter.CheckFTPAccounts(this))
                {
                    FTPAccount acc = Engine.Workflow.OutputsConfig.FTPAccountList[FtpAccountId];
                    DestinationName = string.Format("FTP - {0}", acc.Name);
                    StaticHelper.WriteLine(string.Format("Uploading {0} to FTP: {1}", FileName, acc.Host));
                    string url;
                    FTPUploader fu;
                    SFTPUploader sftp;
                    MyWorker.ReportProgress((int)WorkerTask.ProgressType.UPDATE_PROGRESS_MAX, TaskbarProgressBarState.Normal);
                    switch (acc.Protocol)
                    {
                        case FTPProtocol.SFTP:
                            sftp = new SFTPUploader(acc);
                            if (!sftp.isInstantiated)
                            {
                                Errors.Add("An SFTP client couldn't be instantiated, not enough information.\nCould be a missing key file.");
                                return ur;
                            }
                            sftp.ProgressChanged += new Uploader.ProgressEventHandler(UploadProgressChanged);
                            url = File.Exists(LocalFilePath) ? sftp.Upload(LocalFilePath).URL : sftp.Upload(data, FileName).URL;
                            ur = CreateThumbnail(url, sftp);
                            break;
                        default:
                            fu = new FTPUploader(acc);
                            fu.ProgressChanged += new Uploader.ProgressEventHandler(UploadProgressChanged);
                            url = File.Exists(LocalFilePath) ? fu.Upload(LocalFilePath).URL : fu.Upload(data, FileName).URL;
                            ur = CreateThumbnail(url, fu);
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                StaticHelper.WriteException(ex, "Error while uploading to FTP Server");
                Errors.Add("FTP upload failed.\r\n" + ex.Message);
            }
            return ur;
        }

        private UploadResult CreateThumbnail(string url, FTPUploader fu)
        {
            if (!string.IsNullOrEmpty(url))
            {
                AddUploadResult(new UploadResult() { Host = FileUploaderType.FTP.GetDescription(), URL = url });

                if (CreateThumbnail())
                {
                    double thar = (double)Engine.Workflow.OutputsConfig.FTPThumbnailWidthLimit / (double)tempImage.Width;
                    using (Image img = GraphicsMgr.ChangeImageSize(tempImage, Engine.Workflow.OutputsConfig.FTPThumbnailWidthLimit,
                        (int)(thar * tempImage.Height)))
                    {
                        StringBuilder sb = new StringBuilder(Path.GetFileNameWithoutExtension(LocalFilePath));
                        sb.Append(".th");
                        sb.Append(Path.GetExtension(LocalFilePath));
                        string thPath = Path.Combine(Path.GetDirectoryName(LocalFilePath), sb.ToString());
                        img.Save(thPath);
                        if (File.Exists(thPath))
                        {
                            string thumb = fu.Upload(thPath).URL;

                            if (!string.IsNullOrEmpty(thumb))
                            {
                                UploadResult ur = new UploadResult();
                                ur.Host = FileUploaderType.FTP.GetDescription();
                                ur.ThumbnailURL = thumb;
                                AddUploadResult(ur);
                                return ur;
                            }
                        }
                    }
                }
                return null;
            }
            return null;
        }

        private UploadResult CreateThumbnail(string url, SFTPUploader fu)
        {
            if (!string.IsNullOrEmpty(url))
            {
                AddUploadResult(new UploadResult() { Host = FileUploaderType.FTP.GetDescription(), URL = url });

                if (CreateThumbnail())
                {
                    double thar = (double)Engine.Workflow.OutputsConfig.FTPThumbnailWidthLimit / (double)tempImage.Width;
                    using (Image img = GraphicsMgr.ChangeImageSize(tempImage, Engine.Workflow.OutputsConfig.FTPThumbnailWidthLimit,
                        (int)(thar * tempImage.Height)))
                    {
                        StringBuilder sb = new StringBuilder(Path.GetFileNameWithoutExtension(LocalFilePath));
                        sb.Append(".th");
                        sb.Append(Path.GetExtension(LocalFilePath));
                        string thPath = Path.Combine(Path.GetDirectoryName(LocalFilePath), sb.ToString());
                        img.Save(thPath);

                        if (File.Exists(thPath))
                        {
                            string thumb = fu.Upload(thPath).URL;

                            if (!string.IsNullOrEmpty(thumb))
                            {
                                UploadResult ur = new UploadResult();
                                ur.Host = FileUploaderType.FTP.GetDescription();
                                ur.ThumbnailURL = thumb;
                                AddUploadResult(ur);
                                return ur;
                            }
                        }
                    }
                }
                return null;
            }
            return null;
        }

        private void UploadFile(FileUploaderType fileUploaderType, Stream data)
        {
            FileUploader fileUploader = null;

            switch (fileUploaderType)
            {
                case FileUploaderType.FTP:
                    if (Engine.conf.ShowFTPSettingsBeforeUploading)
                    {
                        UploadersConfigForm ucf = new UploadersConfigForm(Engine.Workflow.OutputsConfig, ZKeys.GetAPIKeys());
                        ucf.Icon = Resources.zss_main;
                        ucf.tcUploaders.SelectedTab = ucf.tpFileUploaders;
                        ucf.tcFileUploaders.SelectedTab = ucf.tpFTP;
                        ucf.ShowDialog();
                    }
                    switch (Job1)
                    {
                        case JobLevel1.Text:
                            UploadToFTP(Engine.Workflow.OutputsConfig.FTPSelectedText, data);
                            break;
                        case JobLevel1.Image:
                            UploadToFTP(Engine.Workflow.OutputsConfig.FTPSelectedImage, data);
                            break;
                        default:
                        case JobLevel1.File:
                            UploadToFTP(Engine.Workflow.OutputsConfig.FTPSelectedFile, data);
                            break;
                    }
                    break;
                case FileUploaderType.Minus:
                    fileUploader = new Minus(Engine.Workflow.OutputsConfig.MinusConfig, new OAuthInfo(ZKeys.MinusConsumerKey, ZKeys.MinusConsumerSecret));
                    break;
                case FileUploaderType.Dropbox:
                    string uploadPath = new NameParser { IsFolderPath = true }.Convert(Dropbox.TidyUploadPath(Engine.Workflow.OutputsConfig.DropboxUploadPath));
                    fileUploader = new Dropbox(Engine.Workflow.OutputsConfig.DropboxOAuthInfo, uploadPath, Engine.Workflow.OutputsConfig.DropboxAccountInfo);
                    break;
                case FileUploaderType.SendSpace:
                    fileUploader = new SendSpace(ZKeys.SendSpaceKey);
                    switch (Engine.Workflow.OutputsConfig.SendSpaceAccountType)
                    {
                        case AccountType.Anonymous:
                            SendSpaceManager.PrepareUploadInfo(ZKeys.SendSpaceKey);
                            break;
                        case AccountType.User:
                            SendSpaceManager.PrepareUploadInfo(ZKeys.SendSpaceKey, Engine.Workflow.OutputsConfig.SendSpaceUsername,
                                Engine.Workflow.OutputsConfig.SendSpacePassword);
                            break;
                    }
                    break;
                case FileUploaderType.RapidShare:
                    fileUploader = new RapidShare(Engine.Workflow.OutputsConfig.RapidShareUserAccountType, Engine.Workflow.OutputsConfig.RapidShareUsername,
                        Engine.Workflow.OutputsConfig.RapidSharePassword);
                    break;
                case FileUploaderType.CustomUploader:
                    fileUploader = new CustomUploader(Engine.Workflow.OutputsConfig.CustomUploadersList[Engine.Workflow.OutputsConfig.CustomUploaderSelected]);
                    break;
            }

            if (fileUploader != null)
            {
                MyWorker.ReportProgress((int)WorkerTask.ProgressType.UPDATE_PROGRESS_MAX, TaskbarProgressBarState.Indeterminate);
                DestinationName = fileUploaderType.GetDescription();
                StaticHelper.WriteLine("Initialized " + DestinationName);
                fileUploader.ProgressChanged += UploadProgressChanged;
                UploadResult ur = new UploadResult();
                ur = fileUploader.Upload(data, FileName);
                ur.Host = fileUploaderType.GetDescription();
                AddUploadResult(ur);
                Errors = fileUploader.Errors;
            }
        }

        public void UploadFile()
        {
            StaticHelper.WriteLine("Uploading File: " + LocalFilePath);

            foreach (FileUploaderType fileUploaderType in MyFileUploaders)
            {
                UploadFile(fileUploaderType, Data);
            }
        }

        public bool ShortenURL(UploadResult ur, string fullUrl)
        {
            if (!string.IsNullOrEmpty(fullUrl))
            {
                Job3 = WorkerTask.JobLevel3.ShortenURL;
                URLShortener us = null;

                if (MyLinkUploaders.Contains(UrlShortenerType.BITLY))
                {
                    us = new BitlyURLShortener(ZKeys.BitlyLogin, ZKeys.BitlyKey);
                }
                /*
                else if (MyLinkUploaders.Contains(UrlShortenerType.Debli))
                {
                    us = new DebliURLShortener();
                }
                */
                else if (MyLinkUploaders.Contains(UrlShortenerType.Google))
                {
                    us = new GoogleURLShortener(Engine.Workflow.OutputsConfig.GoogleURLShortenerAccountType, ZKeys.GoogleApiKey,
                        Engine.Workflow.OutputsConfig.GoogleURLShortenerOAuthInfo);
                }
                else if (MyLinkUploaders.Contains(UrlShortenerType.ISGD))
                {
                    us = new IsgdURLShortener();
                }
                else if (MyLinkUploaders.Contains(UrlShortenerType.Jmp))
                {
                    us = new JmpURLShortener(ZKeys.BitlyLogin, ZKeys.BitlyKey);
                }
                else if (MyLinkUploaders.Contains(UrlShortenerType.TINYURL))
                {
                    us = new TinyURLShortener();
                }
                else if (MyLinkUploaders.Contains(UrlShortenerType.TURL))
                {
                    us = new TurlURLShortener();
                }

                if (us != null)
                {
                    string shortenUrl = us.ShortenURL(fullUrl);

                    if (!string.IsNullOrEmpty(shortenUrl))
                    {
                        StaticHelper.WriteLine(string.Format("Shortened URL: {0}", shortenUrl));
                        ur.Host = us.Host;
                        ur.URL = fullUrl;
                        ur.ShortenedURL = shortenUrl;
                        return true;
                    }
                }
            }

            return false;
        }

        public void SetClipboardContent()
        {
            if (TaskClipboardContent.Contains(ClipboardContentEnum.Local))
            {
                AddUploadResult(new UploadResult()
                {
                    LocalFilePath = this.LocalFilePath,
                    Host = ClipboardContentEnum.Local.GetDescription()
                });
            }
        }

        public void Print()
        {
            if (WorkflowConfig.Outputs.Contains(OutputEnum.Printer))
            {
                if (tempImage != null)
                {
                    MyWorker.ReportProgress((int)ProgressType.PrintImage, (Image)tempImage.Clone());
                }
                else if (!string.IsNullOrEmpty(TempText))
                {
                    MyWorker.ReportProgress((int)ProgressType.PrintText, TempText);
                }
            }
        }

        public void SendEmail()
        {
            EmailForm emailForm = new EmailForm(Engine.Workflow.OutputsConfig.EmailRememberLastTo ? Engine.Workflow.OutputsConfig.EmailLastTo : string.Empty,
                Engine.Workflow.OutputsConfig.EmailDefaultSubject, Engine.Workflow.OutputsConfig.EmailDefaultBody);

            if (emailForm.ShowDialog() == DialogResult.OK)
            {
                if (Engine.Workflow.OutputsConfig.EmailRememberLastTo)
                {
                    Engine.Workflow.OutputsConfig.EmailLastTo = emailForm.ToEmail;
                }

                Email email = new Email
                {
                    SmtpServer = Engine.Workflow.OutputsConfig.EmailSmtpServer,
                    SmtpPort = Engine.Workflow.OutputsConfig.EmailSmtpPort,
                    FromEmail = Engine.Workflow.OutputsConfig.EmailFrom,
                    Password = Engine.Workflow.OutputsConfig.EmailPassword
                };

                Stream emailData = null;
                try
                {
                    emailData = Data;

                    if (emailData != null && emailData.Length > 0)
                    {
                        email.Send(emailForm.ToEmail, emailForm.Subject, emailForm.Body, emailData, FileName);
                    }
                }
                finally
                {
                    if (emailData != null) emailData.Dispose();
                }
            }
        }

        public void UploadToSharedFolder()
        {
            if (Engine.Workflow.OutputsConfig.LocalhostAccountList.CheckSelected(Engine.Workflow.OutputsConfig.LocalhostSelected))
            {
                LocalhostAccount acc = Engine.Workflow.OutputsConfig.LocalhostAccountList[Engine.Workflow.OutputsConfig.LocalhostSelected];
                string fn = string.Empty;
                if (File.Exists(LocalFilePath))
                {
                    fn = Path.GetFileName(LocalFilePath);
                    string destFile = acc.GetLocalhostPath(fn);
                    string destDir = Path.GetDirectoryName(destFile);
                    if (!Directory.Exists(destDir))
                    {
                        Directory.CreateDirectory(destDir);
                    }
                    File.Copy(LocalFilePath, destFile);
                    UpdateLocalFilePath(destFile);
                }
                else if (tempImage != null)
                {
                    EImageFormat imageFormat;
                    Data = WorkerTaskHelper.PrepareImage(WorkflowConfig, tempImage, out imageFormat);
                    fn = WorkerTaskHelper.PrepareFilename(WorkflowConfig, tempImage, imageFormat, GetPatternType());
                    string fp = acc.GetLocalhostPath(fn);
                    FileSystem.WriteImage(fp, Data);
                }
                else if (!string.IsNullOrEmpty(TempText))
                {
                    fn = new NameParser(NameParserType.EntireScreen).Convert(WorkflowConfig.EntireScreenPattern) + ".txt";
                    string destFile = acc.GetLocalhostPath(fn);
                    FileSystem.WriteText(destFile, TempText);
                }

                UploadResult ur = new UploadResult()
                {
                    Host = OutputEnum.SharedFolder.GetDescription(),
                    URL = acc.GetUriPath(fn),
                    LocalFilePath = LocalFilePath
                };
                UploadResults.Add(ur);
            }
        }

        public bool UploadToMediaWiki()
        {
            string fullFilePath = LocalFilePath;

            if (Engine.Workflow.OutputsConfig.MediaWikiAccountList.CheckSelected(Engine.Workflow.OutputsConfig.MediaWikiAccountSelected) && File.Exists(fullFilePath))
            {
                MediaWikiAccount acc = Engine.Workflow.OutputsConfig.MediaWikiAccountList[Engine.Workflow.OutputsConfig.MediaWikiAccountSelected];
                System.Net.IWebProxy proxy = Adapter.CheckProxySettings().GetWebProxy;
                DestinationName = acc.Name;
                StaticHelper.WriteLine(string.Format("Uploading {0} to MediaWiki: {1}", FileName, acc.Url));
                MediaWikiUploader uploader = new MediaWikiUploader(new MediaWikiOptions(acc, proxy));
                AddUploadResult(uploader.UploadImage(LocalFilePath));

                return true;
            }
            return false;
        }

        private void FlashIcon()
        {
            for (int i = 0; i < (int)Engine.conf.FlashTrayCount; i++)
            {
                MyWorker.ReportProgress((int)WorkerTask.ProgressType.FLASH_ICON, Resources.zss_uploaded);
                Thread.Sleep(250);
                MyWorker.ReportProgress((int)WorkerTask.ProgressType.FLASH_ICON, Resources.zss_green);
                Thread.Sleep(250);
            }
            MyWorker.ReportProgress((int)WorkerTask.ProgressType.FLASH_ICON, Resources.zss_tray);
        }

        private void UploadProgressChanged(ProgressManager progress)
        {
            if (Engine.conf.ShowTrayUploadProgress)
            {
                UploadInfo uploadInfo = UploadManager.GetInfo(ID);
                if (uploadInfo != null)
                {
                    uploadInfo.UploadPercentage = (int)progress.Percentage;
                    MyWorker.ReportProgress((int)WorkerTask.ProgressType.CHANGE_TRAY_ICON_PROGRESS, progress);
                }
            }
        }

        #endregion Publish Data

        #region Upload Methods

        public void LoadClipboardContent()
        {
            if (Clipboard.ContainsImage())
            {
                SetImage(Clipboard.GetImage());
            }
            else if (Clipboard.ContainsText())
            {
                SetText(Clipboard.GetText());
            }
            else if (Clipboard.ContainsFileDropList())
            {
                List<string> listFiles = new List<string>();
                listFiles = FileSystem.GetExplorerFileList(Clipboard.GetFileDropList());
                if (listFiles.Count > 0)
                {
                    UpdateLocalFilePath(listFiles[0]);
                }
            }
        }

        #endregion Upload Methods

        #region Checks

        /// <summary>
        /// Function to test if the URL should or could shorten
        /// </summary>
        /// <param name="url">Long URL</param>
        /// <returns>true/false whether URL should or could shorten</returns>
        public bool ShouldShortenURL(string url)
        {
            if (FileSystem.IsValidLink(url) && MyLinkUploaders.Count > 0)
            {
                bool bShortenUrlJob = Engine.conf.ShortenUrlUsingClipboardUpload && Job2 == JobLevel2.UploadFromClipboard && FileSystem.IsValidLink(TempText);
                bool bLongUrl = Engine.conf.ShortenUrlAfterUpload && url.Length > Engine.conf.ShortenUrlAfterUploadAfter;

                if (bShortenUrlJob || bLongUrl)
                {
                    StaticHelper.WriteLine(string.Format("URL Length: {0}; Shortening after {1}", url.Length.ToString(), Engine.conf.ShortenUrlAfterUploadAfter));
                }
                return Engine.conf.TwitterEnabled || bShortenUrlJob || bLongUrl ||
                Engine.conf.ConfLinkFormat.Contains((int)LinkFormatEnum.FULL_TINYURL);
            }

            return false;
        }

        public bool ShortenURL(string fullUrl)
        {
            UploadResult ur = new UploadResult();
            bool success = ShortenURL(ur, fullUrl);
            AddUploadResult(ur);
            return success;
        }

        public bool JobIsImageToClipboard()
        {
            return WorkflowConfig.Outputs.Contains(OutputEnum.Clipboard) && TaskClipboardContent.Contains(ClipboardContentEnum.Data) && tempImage != null;
        }

        private bool CreateThumbnail()
        {
            return GraphicsMgr.IsValidImage(LocalFilePath) && tempImage != null &&
                (Engine.conf.ConfLinkFormat.Contains((int)LinkFormatEnum.LINKED_THUMBNAIL) ||
                  Engine.conf.ConfLinkFormat.Contains((int)LinkFormatEnum.LINKED_THUMBNAIL_WIKI) ||
                  Engine.conf.ConfLinkFormat.Contains((int)LinkFormatEnum.LinkedThumbnailHtml) ||
                  Engine.conf.ConfLinkFormat.Contains((int)LinkFormatEnum.THUMBNAIL)) &&
                (!Engine.Workflow.OutputsConfig.FTPThumbnailCheckSize || (Engine.Workflow.OutputsConfig.FTPThumbnailCheckSize &&
                (tempImage.Width > Engine.Workflow.OutputsConfig.FTPThumbnailWidthLimit)));
        }

        public bool IsNotCanceled()
        {
            return !States.Contains(TaskState.CancellationPending);
        }

        public void SetNotifyIconStatus(NotifyIcon ni, Icon ico)
        {
            if (ni != null && ico != null)
            {
                ni.Icon = ico;
                // Text length must be less than 64 characters long
                StringBuilder sbMsg = new StringBuilder();
                sbMsg.Append(Job2.GetDescription());
                sbMsg.Append(" to ");
                switch (Job1)
                {
                    case JobLevel1.Image:
                        sbMsg.Append(GetActiveImageUploadersDescription());
                        break;
                    case JobLevel1.Text:
                        if (Job3 == WorkerTask.JobLevel3.ShortenURL)
                        {
                            sbMsg.Append(GetActiveLinkUploadersDescription());
                        }
                        else
                        {
                            sbMsg.Append(GetActiveTextUploadersDescription());
                        }
                        break;
                    case JobLevel1.File:
                        sbMsg.Append(GetActiveUploadersDescription<FileUploaderType>(MyFileUploaders));
                        break;
                }
                ni.Text = sbMsg.ToString().Substring(0, Math.Min(sbMsg.Length, 63));
            }
        }

        #endregion Checks

        #region Descriptions

        public NameParserType GetPatternType()
        {
            if (Job2 == JobLevel2.CaptureActiveWindow)
            {
                return NameParserType.ActiveWindow;
            }
            else
            {
                return NameParserType.EntireScreen;
            }
        }

        public string GetDestinationName()
        {
            string destName = DestinationName;
            if (string.IsNullOrEmpty(destName))
            {
                switch (Job1)
                {
                    case JobLevel1.Image:
                        destName = GetActiveImageUploadersDescription();
                        break;
                    case JobLevel1.Text:
                        switch (Job3)
                        {
                            case WorkerTask.JobLevel3.ShortenURL:
                                destName = GetActiveLinkUploadersDescription();
                                break;
                            default:
                                destName = GetActiveTextUploadersDescription();
                                break;
                        }
                        break;
                    case JobLevel1.File:
                        destName = GetActiveUploadersDescription<FileUploaderType>(MyFileUploaders);
                        break;
                }
            }
            return destName;
        }

        public string GetDescription()
        {
            StringBuilder sb = new StringBuilder();

            if (!string.IsNullOrEmpty(FileName))
            {
                sb.Append(FileName);
            }

            if (tempImage != null)
            {
                sb.Append(string.Format(" ({0}x{1})", tempImage.Width, tempImage.Height));
                if (!string.IsNullOrEmpty(FileSize))
                {
                    sb.Append(" " + FileSize);
                }
            }

            if (sb.Length == 0)
            {
                sb.Append(Application.ProductName);
            }

            return sb.ToString();
        }

        public string GetOutputsDescription()
        {
            StringBuilder sb = new StringBuilder();
            foreach (OutputEnum ut in WorkflowConfig.Outputs)
            {
                sb.Append(ut.GetDescription());
                sb.Append(", ");
            }
            if (sb.Length > 2)
            {
                sb.Remove(sb.Length - 2, 2);
            }
            return sb.ToString();
        }

        public string GetActiveImageUploadersDescription()
        {
            StringBuilder sb = new StringBuilder();
            foreach (ImageUploaderType ut in MyImageUploaders)
            {
                sb.Append(ut.GetDescription());
                sb.Append(", ");
            }
            if (sb.Length < 3)
            {
                foreach (OutputEnum ut in WorkflowConfig.Outputs)
                {
                    sb.Append(ut.GetDescription());
                    sb.Append(", ");
                }
            }
            if (sb.Length > 2)
            {
                sb.Remove(sb.Length - 2, 2);
            }
            return sb.ToString();
        }

        public string GetActiveTextUploadersDescription()
        {
            return GetActiveUploadersDescription<TextUploaderType>(MyTextUploaders);
        }

        public string GetActiveLinkUploadersDescription()
        {
            return GetActiveUploadersDescription<UrlShortenerType>(MyLinkUploaders);
        }

        public string GetActiveUploadersDescription<T>(List<T> list)
        {
            StringBuilder sb = new StringBuilder();
            foreach (T ut in list)
            {
                Enum en = (Enum)Convert.ChangeType(ut, typeof(Enum));
                sb.Append(en.GetDescription());
                sb.Append(", ");
            }
            if (sb.Length > 2)
            {
                sb.Remove(sb.Length - 2, 2);
            }
            return sb.ToString();
        }

        public string ToErrorString()
        {
            return string.Join("\r\n", Errors.ToArray());
        }

        public override string ToString()
        {
            StringBuilder sbDebug = new StringBuilder();
            sbDebug.AppendLine(string.Format("Image Uploaders: {0}", GetActiveImageUploadersDescription()));
            sbDebug.AppendLine(string.Format(" File Uploader: {0}", GetActiveUploadersDescription<FileUploaderType>(MyFileUploaders)));
            return sbDebug.ToString();
        }

        #endregion Descriptions

        #region Helper Methods

        /// <summary>
        /// Runs BwApp_DoWork
        /// </summary>
        public void RunWorker()
        {
            MyWorker.RunWorkerAsync(this);
        }

        public UploadResult GetResult()
        {
            foreach (UploadResult ur in this.UploadResults)
            {
                if (!string.IsNullOrEmpty(ur.URL))
                {
                    return ur;
                }
            }

            return null;
        }

        public HistoryItem GenerateHistoryItem()
        {
            return GenerateHistoryItem(Result);
        }

        public HistoryItem GenerateHistoryItem(UploadResult ur)
        {
            HistoryLib.HistoryItem hi = new HistoryLib.HistoryItem();
            hi.DateTimeUtc = EndTime;

            hi.DeletionURL = ur.DeletionURL;
            hi.ThumbnailURL = ur.ThumbnailURL;
            hi.ShortenedURL = ur.ShortenedURL;
            hi.URL = ur.URL;

            hi.Filename = FileName;
            hi.Filepath = LocalFilePath;
            hi.Host = ur.Host;
            hi.Type = Job1.GetDescription();

            return hi;
        }

        public void Dispose()
        {
            if (tempImage != null) tempImage.Dispose();
            if (Data != null) Data.Dispose();
            if (MyWorker != null) MyWorker.Dispose();
        }

        #endregion Helper Methods
    }
}
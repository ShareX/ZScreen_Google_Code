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
using System.Diagnostics;
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
            [Description("Capture Window from a List")]
            CaptureSelectedWindowFromList,
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

        public DateTime StartTime { get; private set; }

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

        public Image tempImage;

        public string tempText { get; private set; }

        public string OCRText { get; private set; }

        public string OCRFilePath { get; private set; }

        private Stream Data;

        public GoogleTranslateInfo TranslationInfo { get; private set; }

        private string DestinationName = string.Empty;

        public TaskInfo Info { get; private set; }

        public List<ClipboardContentEnum> TaskClipboardContent = new List<ClipboardContentEnum>();
        public List<LinkFormatEnum> MyLinkFormat = new List<LinkFormatEnum>();
        public List<UrlShortenerType> MyLinkUploaders = new List<UrlShortenerType>();

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
            Info = new TaskInfo();
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

            if (wf.DestConfig.Outputs.Contains(OutputEnum.Clipboard) && TaskClipboardContent.Count == 0)
            {
                TaskClipboardContent.Add(ClipboardContentEnum.Data);
            }

            StartWork(wf.Job);
        }

        public WorkerTask(BackgroundWorker worker, TaskInfo info)
            : this(Engine.ConfigWorkflow)
        {
            this.Info = info;

            if (!string.IsNullOrEmpty(info.ExistingFilePath))
            {
                UpdateLocalFilePath(info.ExistingFilePath);
            }

            MyWorker = worker;
            PrepareOutputs(info.DestConfig);                                                                        // step 1

            DialogResult result = StartWork(info.Job) ? DialogResult.OK : DialogResult.Cancel;                     // step 2

            if (result == DialogResult.OK && Engine.ConfigUI.PromptForOutputs)                                      // step 3
            {
                WorkflowWizard wfw = new WorkflowWizard(this) { Icon = Resources.zss_tray };
                result = wfw.ShowDialog();
            }

            if (result == DialogResult.OK)
            {
                if (!States.Contains(TaskState.ImageProcessed))
                {
                    ProcessImage(tempImage);
                }
            }

            if (result == DialogResult.Cancel)
            {
                this.States.Add(TaskState.CancellationPending);
            }

            if (States.Contains(TaskState.CancellationPending))
            {
                SetNotifyIconStatus(Info.TrayIcon, ready: true);
            }
        }

        public bool StartWork(JobLevel2 job)
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

            SetNotifyIconStatus(Info.TrayIcon, ready: false);

            Info.WindowTitleText = NativeMethods.GetForegroundWindowText();

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
                case JobLevel2.CaptureSelectedWindowFromList:
                    success = CaptureSelectedWindow();
                    break;
                case JobLevel2.CaptureFreeHandRegion:
                    success = CaptureShape();
                    break;
                case JobLevel2.UploadFromClipboard:
                    success = LoadClipboardContent();
                    break;
            }

            if (tempImage != null)
            {
                Info.ImageSize = tempImage.Size;
            }

            if (success && Job3 != JobLevel3.ShortenURL)
            {
                if (Engine.ConfigUI.EnableImageSound)
                {
                    if (File.Exists(Engine.ConfigUI.SoundImagePath))
                        new System.Media.SoundPlayer(Engine.ConfigUI.SoundImagePath).Play();
                    else
                        new System.Media.SoundPlayer(Resources.Camera).Play();
                }
            }
            if (!success)
            {
                this.States.Add(TaskState.CancellationPending);
            }

            return success;
        }

        private void PrepareOutputs(DestSelector ucDestOptions)
        {
            if (!States.Contains(TaskState.Prepared) && !States.Contains(TaskState.CancellationPending))
            {
                Adapter.SaveMenuConfigToList<OutputEnum>(ucDestOptions.tsddbOutputs, WorkflowConfig.DestConfig.Outputs);
                Adapter.SaveMenuConfigToList<ClipboardContentEnum>(ucDestOptions.tsddbClipboardContent, TaskClipboardContent);
                Adapter.SaveMenuConfigToList<LinkFormatEnum>(ucDestOptions.tsddbLinkFormat, MyLinkFormat);
                Adapter.SaveMenuConfigToList<ImageUploaderType>(ucDestOptions.tsddbDestImage, WorkflowConfig.DestConfig.ImageUploaders);
                Adapter.SaveMenuConfigToList<TextUploaderType>(ucDestOptions.tsddbDestText, WorkflowConfig.DestConfig.TextUploaders);
                Adapter.SaveMenuConfigToList<FileUploaderType>(ucDestOptions.tsddbDestFile, WorkflowConfig.DestConfig.FileUploaders);
                Adapter.SaveMenuConfigToList<UrlShortenerType>(ucDestOptions.tsddbDestLink, MyLinkUploaders);

                States.Add(TaskState.Prepared);
            }
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

                if (Engine.ConfigUI != null && Engine.ConfigUI.ShowOutputsAsap)
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
                    string fn = WorkerTaskHelper.PrepareFilename(WorkflowConfig, tempImage, imageFormat, GetNameParser());
                    string imgfp = FileSystem.GetUniqueFilePath(WorkflowConfig, Engine.ImagesDir, fn);
                    UpdateLocalFilePath(imgfp);
                }

                SetOCR(tempImage);
            }

            return tempImage != null;
        }

        public void SetOCR(Image ocrImage)
        {
            if (TaskClipboardContent.Contains(ClipboardContentEnum.OCR) && string.IsNullOrEmpty(OCRText))
            {
                if (WorkflowConfig.DestConfig.Outputs.Contains(OutputEnum.LocalDisk))
                {
                    WriteImage(ocrImage);
                }

                OCRHelper ocr = null;
                if (File.Exists(Info.LocalFilePath))
                {
                    ocr = new OCRHelper(Info.LocalFilePath);
                }
                else if (ocrImage != null)
                {
                    string ocrfp = Path.Combine(Engine.zTempDir, FileSystem.GetUniqueFileName(WorkflowConfig, "ocr.png"));
                    FileInfo fi = FileSystem.WriteImage(ocrfp, ocrImage.SaveImage(WorkflowConfig, EImageFormat.PNG));
                    if (fi.Exists)
                    {
                        ocr = new OCRHelper(fi.FullName);
                        File.Delete(ocrfp);
                    }
                }

                if (ocr != null)
                {
                    this.OCRText = ocr.Text;

                    if (!string.IsNullOrEmpty(OCRText) && WorkflowConfig.DestConfig.Outputs.Contains(OutputEnum.LocalDisk))
                    {
                        OCRFilePath = Path.ChangeExtension(Info.LocalFilePath, ".txt");
                        FileSystem.WriteText(OCRFilePath, OCRText);
                    }
                }
            }
        }

        public void SetText(string text)
        {
            Job1 = JobLevel1.Text;
            tempText = text;

            string fptxt = FileSystem.GetUniqueFilePath(Engine.ConfigWorkflow, Engine.TextDir, new NameParser().Convert("%y.%mo.%d-%h.%mi.%s") + ".txt");
            UpdateLocalFilePath(fptxt);

            if (Directory.Exists(text))
            {
                Job3 = WorkerTask.JobLevel3.IndexFolder;

                IndexerAdapter settings = new IndexerAdapter();
                settings.LoadConfig(Engine.ConfigUI.IndexerConfig);
                Engine.ConfigUI.IndexerConfig.FolderList.Clear();
                string ext = ".log";
                if (WorkflowConfig.DestConfig.TextUploaders.Contains(TextUploaderType.FileUploader))
                {
                    ext = ".html";
                }
                Info.FileName = Path.GetFileName(tempText) + ext;
                settings.GetConfig().SetSingleIndexPath(Path.Combine(Engine.TextDir, Info.FileName));
                settings.GetConfig().FolderList.Add(tempText);

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
        /// If the user activated the "prompt for Info.FileName" option, then opens a dialog box.
        /// </summary>
        /// <param name="pattern">the base name</param>
        /// <returns>true if the screenshot should be saved, or false if the user canceled</returns>
        public DialogResult SetManualOutputs(string filePath)
        {
            DialogResult dlgResult = DialogResult.OK;
            if (Engine.ConfigUI.PromptForOutputs)
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
                string fp = Info.LocalFilePath;
                if (Engine.IsPortable)
                {
                    fp = Path.Combine(Application.StartupPath, fp);
                    UpdateLocalFilePath(fp);
                }
                if (File.Exists(fp))
                {
                    ur.LocalFilePath = fp;
                }
                if (!string.IsNullOrEmpty(ur.Host))
                {
                    UploadResults.Add(ur);
                }
                if (Engine.ConfigUI.ShowOutputsAsap)
                {
                    MyWorker.ReportProgress((int)ProgressType.ShowBalloonTip, this);
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
                if (string.IsNullOrEmpty(Info.LocalFilePath))
                {
                    Info.LocalFilePath = Engine.IsPortable ? Path.Combine(Application.StartupPath, fp) : fp;
                }
                else
                {
                    Info.LocalFilePath = Path.ChangeExtension(Info.LocalFilePath, Path.GetExtension(fp));
                }
                Info.FileName = Path.GetFileName(Info.LocalFilePath);

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

        /// <summary>
        /// Function to Capture Entire Screen
        /// </summary>
        public bool CaptureScreen()
        {
            if (tempImage == null)
            {
                Screenshot.DrawCursor = WorkflowConfig.DrawCursor;
                SetImage(Screenshot.CaptureFullscreen());
            }

            return tempImage != null;
        }

        private bool CaptureRectangle(Image imgSS)
        {
            return CaptureRegionOrWindow(imgSS, false);
        }

        /// <summary>
        /// Function to Capture Active Window
        /// </summary>
        private bool CaptureActiveWindow()
        {
            if (tempImage == null)
            {
                switch (WorkflowConfig.CaptureEngineMode2)
                {
                    case CaptureEngineType.DWM:
                        if (NativeMethods.IsDWMEnabled())
                        {
                            tempImage = Capture.CaptureWithDWM(WorkflowConfig);
                        }
                        else
                        {
                            StaticHelper.WriteLine("DWM is not found in the system.");
                            tempImage = Capture.CaptureWithGDI2(WorkflowConfig);
                        }
                        break;
                    default:    // GDI
                        tempImage = Capture.CaptureWithGDI2(WorkflowConfig);
                        break;
                }

                if (tempImage != null)
                {
                    if (WorkflowConfig.ActiveWindowShowCheckers)
                    {
                        tempImage = ImageEffects.DrawCheckers(tempImage);
                    }
                }

                SetImage(tempImage);
            }

            return tempImage != null;
        }

        public bool CaptureSelectedWindow()
        {
            NativeMethods.SetForegroundWindow(this.Info.Handle);
            Thread.Sleep(250);
            SetImage(Screenshot.CaptureWindow(this.Info.Handle));
            return tempImage != null;
        }

        public bool CaptureShape()
        {
            Screenshot.DrawCursor = Engine.ConfigWorkflow.DrawCursor;
            RegionCapturePreview rcp = new RegionCapturePreview(Engine.ConfigUI.SurfaceConfig);

            if (rcp.ShowDialog() == DialogResult.OK)
            {
                SetImage(rcp.Result);
            }

            return tempImage != null;
        }

        public bool CaptureRegionOrWindow()
        {
            if (!Engine.IsTakingScreenShot)
            {
                Engine.IsTakingScreenShot = true;

                bool windowMode = Job2 == WorkerTask.JobLevel2.CaptureSelectedWindow;

                if (Engine.ConfigUI == null) Engine.ConfigUI = new XMLSettings();

                try
                {
                    Screenshot.DrawCursor = Engine.ConfigWorkflow.DrawCursor;

                    using (Image imgSS = Screenshot.CaptureFullscreen())
                    {
                        if (Job2 == WorkerTask.JobLevel2.CaptureLastCroppedWindow && !Engine.ConfigUI.LastRegion.IsEmpty)
                        {
                            SetImage(CaptureHelpers.CropImage(imgSS, Engine.ConfigUI.LastRegion));
                        }

                        else if (Job2 == WorkerTask.JobLevel2.CaptureSelectedWindow)
                        {
                            CaptureWindow(imgSS);
                        }

                        else
                        {
                            switch (Engine.ConfigUI.CropEngineMode)
                            {
                                case CropEngineType.CropLite:
                                    using (CropLight crop = new CropLight(imgSS))
                                    {
                                        if (crop.ShowDialog() == DialogResult.OK)
                                        {
                                            SetImage(CaptureHelpers.CropImage(imgSS, crop.SelectionRectangle));
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
                                    surface.Config = Engine.ConfigUI.SurfaceConfig;
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
                    if (Engine.ConfigUI.CaptureEntireScreenOnError)
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

        private bool CaptureRegionOrWindow(Image imgSS, bool windowMode)
        {
            using (Crop c = new Crop(imgSS, windowMode))
            {
                if (c.ShowDialog() == DialogResult.OK)
                {
                    if (Job2 == WorkerTask.JobLevel2.CaptureRectRegion && !Engine.ConfigUI.LastRegion.IsEmpty)
                    {
                        return SetImage(CaptureHelpers.CropImage(imgSS, Engine.ConfigUI.LastRegion));
                    }
                    else if (windowMode && !Engine.ConfigUI.LastCapture.IsEmpty)
                    {
                        return SetImage(CaptureHelpers.CropImage(imgSS, Engine.ConfigUI.LastCapture));
                    }
                }
            }

            return false;
        }

        private bool CaptureWindow(Image imgSS)
        {
            return CaptureRegionOrWindow(imgSS, true);
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
            States.Add(TaskState.ImageProcessed);
            bool window = Job2 == JobLevel2.CaptureActiveWindow || Job2 == JobLevel2.CaptureSelectedWindow || Job2 == JobLevel2.CaptureEntireScreen;

            if (img != null)
            {
                if (!window)
                {
                    // Add Rounded corners
                    bool roundedShadowCorners = false;
                    if (Engine.ConfigUI.ImageAddRoundedCorners)
                    {
                        img = GraphicsMgr.RemoveCorners(img, null);
                        roundedShadowCorners = true;
                    }

                    // Add shadows
                    if (Engine.ConfigUI.ImageAddShadow)
                    {
                        img = GraphicsMgr.AddBorderShadow(img, roundedShadowCorners);
                    }
                }

                // Watermark
                ImageEffects effects = new ImageEffects(WorkflowConfig);
                img = effects.ApplySizeChanges(img);
                img = effects.ApplyScreenshotEffects(img);
                if (Job2 != WorkerTask.JobLevel2.UploadFromClipboard || !Engine.ConfigWorkflow.WatermarkExcludeClipboardUpload)
                {
                    img = new ImageEffects(WorkflowConfig).ApplyWatermark(img, GetNameParser(NameParserType.Watermark));
                }

                tempImage = img;
            }
        }

        public bool IsValidActionFile(Software app)
        {
            return Job1 == JobLevel1.File && app.TriggerForFiles && File.Exists(Info.LocalFilePath);
        }

        public bool IsValidActionText(Software app)
        {
            return Job1 == JobLevel1.Text && app.TriggerForText && !string.IsNullOrEmpty(tempText);
        }

        public bool IsValidActionImage(Software app)
        {
            return Job1 == JobLevel1.Image && app.TriggerForImages && !WasToTakeScreenshot ||
                   Job1 == JobLevel1.Image && app.TriggerForScreenshots && WasToTakeScreenshot;
        }

        public bool IsValidActionOCR(Software app)
        {
            return Job1 == JobLevel1.Image && app.TriggerForText && !string.IsNullOrEmpty(OCRText) && File.Exists(OCRFilePath);
        }

        /// <summary>
        /// Perform Actions after capturing image/text/file objects
        /// </summary>
        public void PerformActions()
        {
            foreach (Software app in Engine.ConfigUI.ActionsApps)
            {
                if (app.Enabled)
                {
                    if (IsValidActionImage(app) && app.Name == Engine.zImageAnnotator)
                    {
                        try
                        {
                            Greenshot.Helpers.Capture capture = new Greenshot.Helpers.Capture(tempImage);
                            capture.CaptureDetails.Filename = Info.LocalFilePath;
                            capture.CaptureDetails.Title = Path.GetFileNameWithoutExtension(capture.CaptureDetails.Filename);
                            capture.CaptureDetails.AddMetaData("file", capture.CaptureDetails.Filename);
                            capture.CaptureDetails.AddMetaData("source", "file");
                            Greenshot.Drawing.Surface surface = new Greenshot.Drawing.Surface(capture);
                            Greenshot.ImageEditorForm editor = new Greenshot.ImageEditorForm(surface, WorkflowConfig.DestConfig.Outputs.Contains(OutputEnum.LocalDisk)) { Icon = Resources.zss_main };
                            editor.SetImagePath(Info.LocalFilePath);
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
                    else if (IsValidActionImage(app) && app.Name == Engine.zImageEffects)
                    {
                        ImageEffectsGUI effects = new ImageEffectsGUI(tempImage);
                        effects.ShowDialog();
                        tempImage = effects.GetImageForExport();
                    }
                    else if (File.Exists(app.Path))
                    {
                        if (IsValidActionOCR(app))
                        {
                            app.OpenFile(OCRFilePath);
                            OCRText = File.ReadAllText(OCRFilePath);
                        }
                        else if (IsValidActionText(app))
                        {
                            app.OpenFile(Info.LocalFilePath);
                            tempText = File.ReadAllText(tempText);
                        }
                        else if (IsValidActionImage(app))
                        {
                            WriteImage(tempImage);
                            app.OpenFile(Info.LocalFilePath);
                        }
                        else if (IsValidActionFile(app))
                        {
                            app.OpenFile(Info.LocalFilePath);
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

            if (File.Exists(Info.LocalFilePath) || tempImage != null || !string.IsNullOrEmpty(tempText))
            {
                // Note: We need write text or image first
                if (WorkflowConfig.DestConfig.Outputs.Contains(OutputEnum.LocalDisk))
                {
                    switch (Job1)
                    {
                        case JobLevel1.Text:
                            FileSystem.WriteText(Info.LocalFilePath, tempText);
                            break;
                        default:
                            WriteImage(tempImage);
                            break;
                    }

                    UploadResult ur_local = new UploadResult()
                    {
                        Host = OutputEnum.LocalDisk.GetDescription(),
                        LocalFilePath = Info.LocalFilePath,
                    };
                    if (!WorkflowConfig.DestConfig.Outputs.Contains(OutputEnum.RemoteHost))
                    {
                        ur_local.URL = ur_local.GetLocalFilePathAsUri(Info.LocalFilePath);
                        AddUploadResult(ur_local);
                    }
                }

                if (WorkflowConfig.DestConfig.Outputs.Contains(OutputEnum.RemoteHost))
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

                if (WorkflowConfig.DestConfig.Outputs.Contains(OutputEnum.SharedFolder))
                {
                    UploadToSharedFolder();
                }

                if (WorkflowConfig.DestConfig.Outputs.Contains(OutputEnum.Email))
                {
                    SendEmail();
                }

                if (WorkflowConfig.DestConfig.Outputs.Contains(OutputEnum.Printer))
                {
                    Print();
                }

                if (WorkflowConfig.DestConfig.Outputs.Contains(OutputEnum.Clipboard) && WorkflowConfig.DestConfig.Outputs.Count == 1)
                {
                    SetClipboardContent();
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

            if (tempImage != null)
            {
                StaticHelper.WriteLine(new StackFrame(1).GetMethod().Name + " prepared data from image");
                EImageFormat imageFormat;
                data = WorkerTaskHelper.PrepareImage(WorkflowConfig, tempImage, out imageFormat, bTargetFileSize: true);
            }
            else if (File.Exists(Info.LocalFilePath))
            {
                StaticHelper.WriteLine(new StackFrame(1).GetMethod().Name + " prepared data from " + Info.LocalFilePath);
                data = PrepareData(Info.LocalFilePath);
            }
            else if (!string.IsNullOrEmpty(tempText))
            {
                StaticHelper.WriteLine(new StackFrame(1).GetMethod().Name + " prepared data from text");
                data = new MemoryStream(Encoding.UTF8.GetBytes(tempText));
            }

            SetFileSize(data.Length);

            return data;
        }

        private void SetFileSize(long sz)
        {
            if (sz > 1023)
            {
                Info.FileSize = string.Format("{0} KiB", (sz / 1024.0).ToString("0"));
            }
        }

        /// <summary>
        /// Writes MyImage object in a WorkerTask into a file
        /// </summary>
        /// <param name="t">WorkerTask</param>
        public void WriteImage(Image img)
        {
            if (WorkflowConfig.DestConfig.Outputs.Contains(OutputEnum.LocalDisk) && img != null &&
                !States.Contains(TaskState.ImageWritten))
            {
                // PrepareData instead of using Data
                FileInfo fi = FileSystem.WriteImage(Info.LocalFilePath, PrepareData());
                this.SetFileSize(fi.Length);
                States.Add(TaskState.ImageWritten);

                if (!File.Exists(Info.LocalFilePath))
                {
                    Errors.Add(string.Format("{0} does not exist", Info.LocalFilePath));
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
                            apng.AddFrame(new Bitmap(img), WorkflowConfig.ImageAnimatedFramesDelay * 1000, 1000);
                        }

                        apng.WriteApng(outputFilePath);
                        break;

                    default:
                        outputFilePath += ".gif";
                        AnimatedGifEncoder enc = new AnimatedGifEncoder();
                        enc.Start(outputFilePath);
                        enc.SetDelay(WorkflowConfig.ImageAnimatedFramesDelay * 1000);
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
            if (WorkflowConfig.DestConfig.Outputs.Contains(OutputEnum.RemoteHost))
            {
                if (Engine.ConfigUI != null && tempImage != null && Engine.ConfigUI.TinyPicSizeCheck && WorkflowConfig.DestConfig.ImageUploaders.Contains(ImageUploaderType.TINYPIC))
                {
                    if (tempImage.Width > 1600 || tempImage.Height > 1600)
                    {
                        StaticHelper.WriteLine("Changing from TinyPic to ImageShack due to large image size");
                        if (!WorkflowConfig.DestConfig.ImageUploaders.Contains(ImageUploaderType.IMAGESHACK))
                        {
                            WorkflowConfig.DestConfig.ImageUploaders.Add(ImageUploaderType.IMAGESHACK);
                            WorkflowConfig.DestConfig.ImageUploaders.Remove(ImageUploaderType.TINYPIC);
                        }
                    }
                }

                for (int i = 0; i < WorkflowConfig.DestConfig.ImageUploaders.Count; i++)
                {
                    UploadImage(WorkflowConfig.DestConfig.ImageUploaders[i], Data);
                }
            }

            if (Engine.ConfigUI != null && Engine.ConfigUI.ImageUploadRetryOnTimeout && UploadDuration > (int)Engine.ConfigUI.UploadDurationLimit)
            {
                if (!WorkflowConfig.DestConfig.ImageUploaders.Contains(ImageUploaderType.TINYPIC))
                {
                    WorkflowConfig.DestConfig.ImageUploaders.Add(ImageUploaderType.TINYPIC);
                }
                else if (!WorkflowConfig.DestConfig.ImageUploaders.Contains(ImageUploaderType.TINYPIC))
                {
                    WorkflowConfig.DestConfig.ImageUploaders.Add(ImageUploaderType.IMAGESHACK);
                }
            }
        }

        private void UploadImage(ImageUploaderType imageUploaderType, Stream data)
        {
            ImageUploader imageUploader = null;

            switch (imageUploaderType)
            {
                case ImageUploaderType.IMAGESHACK:
                    imageUploader = new ImageShackUploader(ZKeys.ImageShackKey, Engine.ConfigUploaders.ImageShackAccountType,
                        Engine.ConfigUploaders.ImageShackRegistrationCode)
                    {
                        IsPublic = Engine.ConfigUploaders.ImageShackShowImagesInPublic
                    };
                    break;
                case ImageUploaderType.TINYPIC:
                    imageUploader = new TinyPicUploader(ZKeys.TinyPicID, ZKeys.TinyPicKey, Engine.ConfigUploaders.TinyPicAccountType,
                        Engine.ConfigUploaders.TinyPicRegistrationCode);
                    break;
                case ImageUploaderType.IMGUR:
                    imageUploader = new Imgur(Engine.ConfigUploaders.ImgurAccountType, ZKeys.ImgurAnonymousKey, Engine.ConfigUploaders.ImgurOAuthInfo)
                    {
                        ThumbnailType = Engine.ConfigUploaders.ImgurThumbnailType
                    };
                    break;
                case ImageUploaderType.FLICKR:
                    imageUploader = new FlickrUploader(ZKeys.FlickrKey, ZKeys.FlickrSecret,
                        Engine.ConfigUploaders.FlickrAuthInfo, Engine.ConfigUploaders.FlickrSettings);
                    break;
                case ImageUploaderType.Photobucket:
                    imageUploader = new Photobucket(Engine.ConfigUploaders.PhotobucketOAuthInfo, Engine.ConfigUploaders.PhotobucketAccountInfo);
                    break;
                case ImageUploaderType.UPLOADSCREENSHOT:
                    imageUploader = new UploadScreenshot(ZKeys.UploadScreenshotKey);
                    break;
                case ImageUploaderType.MEDIAWIKI:
                    UploadToMediaWiki();
                    break;
                case ImageUploaderType.TWITPIC:
                    TwitPicOptions twitpicOpt = new TwitPicOptions();
                    twitpicOpt.Username = Engine.ConfigUploaders.TwitPicUsername;
                    twitpicOpt.Password = Engine.ConfigUploaders.TwitPicPassword;
                    // twitpicOpt.TwitPicUploadType = Engine.conf.TwitPicUploadMode;
                    twitpicOpt.TwitPicThumbnailMode = Engine.ConfigUploaders.TwitPicThumbnailMode;
                    twitpicOpt.ShowFull = Engine.ConfigUploaders.TwitPicShowFull;
                    imageUploader = new TwitPicUploader(twitpicOpt);
                    break;
                case ImageUploaderType.YFROG:
                    YfrogOptions yfrogOp = new YfrogOptions(ZKeys.ImageShackKey);
                    yfrogOp.Username = Engine.ConfigUploaders.YFrogUsername;
                    yfrogOp.Password = Engine.ConfigUploaders.YFrogPassword;
                    yfrogOp.Source = Application.ProductName;
                    // yfrogOp.UploadType = Engine.conf.YfrogUploadMode;
                    imageUploader = new YfrogUploader(yfrogOp);
                    break;
                case ImageUploaderType.TWITSNAPS:
                    imageUploader = new TwitSnapsUploader(ZKeys.TwitsnapsKey, Adapter.TwitterGetActiveAccount());
                    break;
                case ImageUploaderType.FileUploader:
                    foreach (FileUploaderType ft in WorkflowConfig.DestConfig.FileUploaders)
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
                    if (Engine.ConfigUI == null)
                    {
                        Engine.ConfigUI = new XMLSettings();
                    }

                    for (int i = 0; i <= (int)Engine.ConfigUI.ErrorRetryCount; i++)
                    {
                        UploadResult ur_remote_img = new UploadResult() { LocalFilePath = Info.LocalFilePath };
                        ur_remote_img = imageUploader.Upload(data, Info.FileName);
                        ur_remote_img.Host = imageUploaderType.GetDescription();
                        AddUploadResult(ur_remote_img);
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

            if (ShouldShortenURL(tempText))
            {
                // Need this for shortening URL using Clipboard Upload
                ShortenURL(tempText);
            }
            else if (WorkflowConfig.DestConfig.Outputs.Contains(OutputEnum.RemoteHost))
            {
                foreach (TextUploaderType textUploaderType in WorkflowConfig.DestConfig.TextUploaders)
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
                    textUploader = new PastebinUploader(ZKeys.PastebinKey, Engine.ConfigUploaders.PastebinSettings);
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

                if (!string.IsNullOrEmpty(tempText))
                {
                    url = textUploader.UploadText(tempText);
                }
                else
                {
                    url = textUploader.UploadTextFile(Info.LocalFilePath);
                }
                UploadResult ur_remote_text = new UploadResult()
                {
                    LocalFilePath = Info.LocalFilePath,
                    Host = textUploaderType.GetDescription(),
                    URL = url
                };
                AddUploadResult(ur_remote_text);
                Errors = textUploader.Errors;
            }
        }

        /// <summary>
        /// Funtion to FTP the Screenshot
        /// </summary>
        /// <returns>Retuns a List of Screenshots</returns>
        public UploadResult UploadToFTP(int FtpAccountId, Stream data)
        {
            UploadResult ur_remote_file_ftp = new UploadResult()
            {
                LocalFilePath = this.Info.LocalFilePath,
                Host = FileUploaderType.FTP.GetDescription()
            };

            try
            {
                MyWorker.ReportProgress((int)WorkerTask.ProgressType.UPDATE_PROGRESS_MAX, TaskbarProgressBarState.Indeterminate);

                if (Adapter.CheckFTPAccounts(this, FtpAccountId))
                {
                    FTPAccount acc = Engine.ConfigUploaders.FTPAccountList[FtpAccountId];
                    DestinationName = string.Format("FTP - {0}", acc.Name);
                    StaticHelper.WriteLine(string.Format("Uploading {0} to FTP: {1}", Info.FileName, acc.Host));

                    MyWorker.ReportProgress((int)WorkerTask.ProgressType.UPDATE_PROGRESS_MAX, TaskbarProgressBarState.Normal);
                    switch (acc.Protocol)
                    {
                        case FTPProtocol.SFTP:
                            SFTPUploader sftp = new SFTPUploader(acc);
                            if (!sftp.isInstantiated)
                            {
                                Errors.Add("An SFTP client couldn't be instantiated, not enough information.\nCould be a missing key file.");
                                return ur_remote_file_ftp;
                            }
                            sftp.ProgressChanged += new Uploader.ProgressEventHandler(UploadProgressChanged);
                            ur_remote_file_ftp.URL = File.Exists(Info.LocalFilePath) ? sftp.Upload(Info.LocalFilePath).URL : sftp.Upload(data, Info.FileName).URL;
                            ur_remote_file_ftp.ThumbnailURL = CreateThumbnail(ur_remote_file_ftp.URL, sftp);
                            break;
                        default:
                            FTPUploader fu = new FTPUploader(acc);
                            fu.ProgressChanged += new Uploader.ProgressEventHandler(UploadProgressChanged);
                            ur_remote_file_ftp.URL = File.Exists(Info.LocalFilePath) ? fu.Upload(Info.LocalFilePath).URL : fu.Upload(data, Info.FileName).URL;
                            ur_remote_file_ftp.ThumbnailURL = CreateThumbnail(ur_remote_file_ftp.URL, fu);
                            break;
                    }

                    if (!string.IsNullOrEmpty(ur_remote_file_ftp.URL))
                    {
                        AddUploadResult(ur_remote_file_ftp);
                    }
                }
            }
            catch (Exception ex)
            {
                StaticHelper.WriteException(ex, "Error while uploading to FTP Server");
                Errors.Add("FTP upload failed.\r\n" + ex.Message);
            }
            return ur_remote_file_ftp;
        }

        private string CreateThumbnail(string url, FTPUploader fu)
        {
            if (!string.IsNullOrEmpty(url))
            {
                if (CreateThumbnail())
                {
                    double thar = (double)Engine.ConfigUploaders.FTPThumbnailWidthLimit / (double)tempImage.Width;
                    using (Image img = GraphicsMgr.ChangeImageSize(tempImage, Engine.ConfigUploaders.FTPThumbnailWidthLimit,
                        (int)(thar * tempImage.Height)))
                    {
                        StringBuilder sb = new StringBuilder(Path.GetFileNameWithoutExtension(Info.LocalFilePath));
                        sb.Append(".th");
                        sb.Append(Path.GetExtension(Info.LocalFilePath));
                        string thPath = Path.Combine(Path.GetDirectoryName(Info.LocalFilePath), sb.ToString());
                        img.Save(thPath);
                        if (File.Exists(thPath))
                        {
                            string thumb = fu.Upload(thPath).URL;

                            if (!string.IsNullOrEmpty(thumb))
                            {
                                return thumb;
                            }
                        }
                    }
                }
                return null;
            }
            return null;
        }

        private string CreateThumbnail(string url, SFTPUploader fu)
        {
            if (!string.IsNullOrEmpty(url))
            {
                if (CreateThumbnail())
                {
                    double thar = (double)Engine.ConfigUploaders.FTPThumbnailWidthLimit / (double)tempImage.Width;
                    using (Image img = GraphicsMgr.ChangeImageSize(tempImage, Engine.ConfigUploaders.FTPThumbnailWidthLimit,
                        (int)(thar * tempImage.Height)))
                    {
                        StringBuilder sb = new StringBuilder(Path.GetFileNameWithoutExtension(Info.LocalFilePath));
                        sb.Append(".th");
                        sb.Append(Path.GetExtension(Info.LocalFilePath));
                        string thPath = Path.Combine(Path.GetDirectoryName(Info.LocalFilePath), sb.ToString());
                        img.Save(thPath);

                        if (File.Exists(thPath))
                        {
                            string thumb = fu.Upload(thPath).URL;

                            if (!string.IsNullOrEmpty(thumb))
                            {
                                return thumb;
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
                    if (Engine.ConfigUI.ShowFTPSettingsBeforeUploading)
                    {
                        UploadersConfigForm ucf = new UploadersConfigForm(Engine.ConfigUploaders, ZKeys.GetAPIKeys());
                        ucf.Icon = Resources.zss_main;
                        ucf.tcUploaders.SelectedTab = ucf.tpFileUploaders;
                        ucf.tcFileUploaders.SelectedTab = ucf.tpFTP;
                        ucf.ShowDialog();
                    }
                    switch (Job1)
                    {
                        case JobLevel1.Text:
                            UploadToFTP(Engine.ConfigUploaders.FTPSelectedText, data);
                            break;
                        case JobLevel1.Image:
                            UploadToFTP(Engine.ConfigUploaders.FTPSelectedImage, data);
                            break;
                        default:
                        case JobLevel1.File:
                            UploadToFTP(Engine.ConfigUploaders.FTPSelectedFile, data);
                            break;
                    }
                    break;
                case FileUploaderType.Minus:
                    fileUploader = new Minus(Engine.ConfigUploaders.MinusConfig, new OAuthInfo(ZKeys.MinusConsumerKey, ZKeys.MinusConsumerSecret));
                    break;
                case FileUploaderType.Dropbox:
                    string uploadPath = new NameParser { IsFolderPath = true }.Convert(Dropbox.TidyUploadPath(Engine.ConfigUploaders.DropboxUploadPath));
                    fileUploader = new Dropbox(Engine.ConfigUploaders.DropboxOAuthInfo, uploadPath, Engine.ConfigUploaders.DropboxAccountInfo);
                    break;
                case FileUploaderType.SendSpace:
                    fileUploader = new SendSpace(ZKeys.SendSpaceKey);
                    switch (Engine.ConfigUploaders.SendSpaceAccountType)
                    {
                        case AccountType.Anonymous:
                            SendSpaceManager.PrepareUploadInfo(ZKeys.SendSpaceKey);
                            break;
                        case AccountType.User:
                            SendSpaceManager.PrepareUploadInfo(ZKeys.SendSpaceKey, Engine.ConfigUploaders.SendSpaceUsername,
                                Engine.ConfigUploaders.SendSpacePassword);
                            break;
                    }
                    break;
                case FileUploaderType.RapidShare:
                    fileUploader = new RapidShare(Engine.ConfigUploaders.RapidShareUserAccountType, Engine.ConfigUploaders.RapidShareUsername,
                        Engine.ConfigUploaders.RapidSharePassword);
                    break;
                case FileUploaderType.CustomUploader:
                    fileUploader = new CustomUploader(Engine.ConfigUploaders.CustomUploadersList[Engine.ConfigUploaders.CustomUploaderSelected]);
                    break;
            }

            if (fileUploader != null)
            {
                MyWorker.ReportProgress((int)WorkerTask.ProgressType.UPDATE_PROGRESS_MAX, TaskbarProgressBarState.Indeterminate);
                DestinationName = fileUploaderType.GetDescription();
                StaticHelper.WriteLine("Initialized " + DestinationName);
                fileUploader.ProgressChanged += UploadProgressChanged;
                UploadResult ur_remote_file = fileUploader.Upload(data, Info.FileName);
                if (ur_remote_file != null)
                {
                    ur_remote_file.Host = fileUploaderType.GetDescription();
                    ur_remote_file.LocalFilePath = Info.LocalFilePath;
                    AddUploadResult(ur_remote_file);
                }
                Errors = fileUploader.Errors;
            }
        }

        public void UploadFile()
        {
            StaticHelper.WriteLine("Uploading File: " + Info.LocalFilePath);

            foreach (FileUploaderType fileUploaderType in WorkflowConfig.DestConfig.FileUploaders)
            {
                UploadFile(fileUploaderType, Data);
            }
        }

        public bool ShortenURL(UploadResult ur_shorturl, string fullUrl)
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
                    us = new GoogleURLShortener(Engine.ConfigUploaders.GoogleURLShortenerAccountType, ZKeys.GoogleApiKey,
                        Engine.ConfigUploaders.GoogleURLShortenerOAuthInfo);
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
                        ur_shorturl.Host = us.Host;
                        ur_shorturl.URL = fullUrl;
                        ur_shorturl.ShortenedURL = shortenUrl;
                        return true;
                    }
                }
            }

            return false;
        }

        public void SetClipboardContent()
        {
            UploadResult ur_clipboard = new UploadResult();
            ur_clipboard.Host = OutputEnum.Clipboard.GetDescription();
            if (File.Exists(Info.LocalFilePath) && TaskClipboardContent.Contains(ClipboardContentEnum.Local))
            {
                ur_clipboard.LocalFilePath = this.Info.LocalFilePath;
            }
            AddUploadResult(ur_clipboard);
        }

        public void Print()
        {
            if (WorkflowConfig.DestConfig.Outputs.Contains(OutputEnum.Printer))
            {
                if (tempImage != null)
                {
                    MyWorker.ReportProgress((int)ProgressType.PrintImage, (Image)tempImage.Clone());
                }
                else if (!string.IsNullOrEmpty(tempText))
                {
                    MyWorker.ReportProgress((int)ProgressType.PrintText, tempText);
                }
            }
        }

        public void SendEmail()
        {
            EmailForm emailForm = new EmailForm(Engine.ConfigUploaders.EmailRememberLastTo ? Engine.ConfigUploaders.EmailLastTo : string.Empty,
                Engine.ConfigUploaders.EmailDefaultSubject, Engine.ConfigUploaders.EmailDefaultBody);

            if (emailForm.ShowDialog() == DialogResult.OK)
            {
                if (Engine.ConfigUploaders.EmailRememberLastTo)
                {
                    Engine.ConfigUploaders.EmailLastTo = emailForm.ToEmail;
                }

                Email email = new Email
                {
                    SmtpServer = Engine.ConfigUploaders.EmailSmtpServer,
                    SmtpPort = Engine.ConfigUploaders.EmailSmtpPort,
                    FromEmail = Engine.ConfigUploaders.EmailFrom,
                    Password = Engine.ConfigUploaders.EmailPassword
                };

                Stream emailData = null;
                try
                {
                    emailData = Data;

                    if (emailData != null && emailData.Length > 0)
                    {
                        email.Send(emailForm.ToEmail, emailForm.Subject, emailForm.Body, emailData, Info.FileName);
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
            if (Engine.ConfigUploaders.LocalhostAccountList.HasValidIndex(Engine.ConfigUploaders.LocalhostSelected))
            {
                LocalhostAccount acc = Engine.ConfigUploaders.LocalhostAccountList[Engine.ConfigUploaders.LocalhostSelected];
                string fn = string.Empty;
                if (File.Exists(Info.LocalFilePath))
                {
                    fn = Path.GetFileName(Info.LocalFilePath);
                    string destFile = acc.GetLocalhostPath(fn);
                    string destDir = Path.GetDirectoryName(destFile);
                    if (!Directory.Exists(destDir))
                    {
                        Directory.CreateDirectory(destDir);
                    }
                    File.Copy(Info.LocalFilePath, destFile);
                    UpdateLocalFilePath(destFile);
                }
                else if (tempImage != null)
                {
                    EImageFormat imageFormat;
                    Data = WorkerTaskHelper.PrepareImage(WorkflowConfig, tempImage, out imageFormat);
                    fn = WorkerTaskHelper.PrepareFilename(WorkflowConfig, tempImage, imageFormat, GetNameParser());
                    string fp = acc.GetLocalhostPath(fn);
                    FileSystem.WriteImage(fp, Data);
                }
                else if (!string.IsNullOrEmpty(tempText))
                {
                    fn = new NameParser(NameParserType.EntireScreen).Convert(WorkflowConfig.EntireScreenPattern) + ".txt";
                    string destFile = acc.GetLocalhostPath(fn);
                    FileSystem.WriteText(destFile, tempText);
                }

                UploadResult ur = new UploadResult()
                {
                    Host = OutputEnum.SharedFolder.GetDescription(),
                    URL = acc.GetUriPath(fn),
                    LocalFilePath = Info.LocalFilePath
                };
                UploadResults.Add(ur);
            }
        }

        public bool UploadToMediaWiki()
        {
            string fullFilePath = Info.LocalFilePath;

            if (Engine.ConfigUploaders.MediaWikiAccountList.HasValidIndex(Engine.ConfigUploaders.MediaWikiAccountSelected) && File.Exists(fullFilePath))
            {
                MediaWikiAccount acc = Engine.ConfigUploaders.MediaWikiAccountList[Engine.ConfigUploaders.MediaWikiAccountSelected];
                System.Net.IWebProxy proxy = Adapter.CheckProxySettings().GetWebProxy;
                DestinationName = acc.Name;
                StaticHelper.WriteLine(string.Format("Uploading {0} to MediaWiki: {1}", Info.FileName, acc.Url));
                MediaWikiUploader uploader = new MediaWikiUploader(new MediaWikiOptions(acc, proxy));
                UploadResult ur_remote_img_mediawiki = uploader.UploadImage(Info.LocalFilePath);
                if (ur_remote_img_mediawiki != null)
                {
                    ur_remote_img_mediawiki.LocalFilePath = this.Info.LocalFilePath;
                    AddUploadResult(ur_remote_img_mediawiki);
                }
                return true;
            }
            return false;
        }

        private void FlashIcon()
        {
            for (int i = 0; i < (int)Engine.ConfigUI.FlashTrayCount; i++)
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
            if (Engine.ConfigUI.ShowTrayUploadProgress)
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

        public bool LoadClipboardContent()
        {
            bool succ = true;
            if (Clipboard.ContainsImage())
            {
                succ = SetImage(Clipboard.GetImage());
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

            return succ;
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
                bool bShortenUrlJob = Engine.ConfigUI.ShortenUrlUsingClipboardUpload && Job2 == JobLevel2.UploadFromClipboard && FileSystem.IsValidLink(tempText);
                bool bLongUrl = Engine.ConfigUI.ShortenUrlAfterUpload && url.Length > Engine.ConfigUI.ShortenUrlAfterUploadAfter;

                if (bShortenUrlJob || bLongUrl)
                {
                    StaticHelper.WriteLine(string.Format("URL Length: {0}; Shortening after {1}", url.Length.ToString(), Engine.ConfigUI.ShortenUrlAfterUploadAfter));
                }
                return Engine.ConfigUI.TwitterEnabled || bShortenUrlJob || bLongUrl ||
                Engine.ConfigUI.ConfLinkFormat.Contains((int)LinkFormatEnum.FULL_TINYURL);
            }

            return false;
        }

        public bool ShortenURL(string fullUrl)
        {
            UploadResult ur_shorturl = new UploadResult();
            bool success = ShortenURL(ur_shorturl, fullUrl);
            AddUploadResult(ur_shorturl);
            return success;
        }

        public bool JobIsImageToClipboard()
        {
            return WorkflowConfig.DestConfig.Outputs.Contains(OutputEnum.Clipboard) && TaskClipboardContent.Contains(ClipboardContentEnum.Data) && tempImage != null;
        }

        private bool CreateThumbnail()
        {
            return GraphicsMgr.IsValidImage(Info.LocalFilePath) && tempImage != null &&
                (Engine.ConfigUI.ConfLinkFormat.Contains((int)LinkFormatEnum.LINKED_THUMBNAIL) ||
                  Engine.ConfigUI.ConfLinkFormat.Contains((int)LinkFormatEnum.LINKED_THUMBNAIL_WIKI) ||
                  Engine.ConfigUI.ConfLinkFormat.Contains((int)LinkFormatEnum.LinkedThumbnailHtml) ||
                  Engine.ConfigUI.ConfLinkFormat.Contains((int)LinkFormatEnum.THUMBNAIL)) &&
                (!Engine.ConfigUploaders.FTPThumbnailCheckSize || (Engine.ConfigUploaders.FTPThumbnailCheckSize &&
                (tempImage.Width > Engine.ConfigUploaders.FTPThumbnailWidthLimit)));
        }

        public bool Canceled()
        {
            return !IsNotCanceled();
        }

        public bool IsNotCanceled()
        {
            return !States.Contains(TaskState.CancellationPending);
        }

        private void SetNotifyIconStatus(NotifyIcon ni, bool ready)
        {
            Icon ico = ready ? Resources.zss_tray : Resources.zss_busy;
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
                        sbMsg.Append(GetActiveUploadersDescription<FileUploaderType>(WorkflowConfig.DestConfig.FileUploaders));
                        break;
                }
                ni.Text = ready ? Engine.GetProductName() : sbMsg.ToString().Substring(0, Math.Min(sbMsg.Length, 63));
            }
        }

        #endregion Checks

        #region Descriptions

        public NameParser GetNameParser()
        {
            return GetNameParser(Job2 == JobLevel2.CaptureActiveWindow ? NameParserType.ActiveWindow : NameParserType.EntireScreen);
        }

        public NameParser GetNameParser(NameParserType parserType)
        {
            return new NameParser
            {
                Type = parserType,
                Picture = tempImage,
                AutoIncrementNumber = WorkflowConfig.AutoIncrement,
                WindowText = Info.WindowTitleText
            };
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
                        destName = GetActiveUploadersDescription<FileUploaderType>(WorkflowConfig.DestConfig.FileUploaders);
                        break;
                }
            }
            return destName;
        }

        public string GetDescription()
        {
            StringBuilder sb = new StringBuilder();

            if (!string.IsNullOrEmpty(Info.FileName))
            {
                sb.Append(Info.FileName);
            }

            if (tempImage != null)
            {
                sb.Append(string.Format(" ({0}x{1})", tempImage.Width, tempImage.Height));
                if (!string.IsNullOrEmpty(Info.FileSize))
                {
                    sb.Append(" " + Info.FileSize);
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
            foreach (OutputEnum ut in WorkflowConfig.DestConfig.Outputs)
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
            foreach (ImageUploaderType ut in WorkflowConfig.DestConfig.ImageUploaders)
            {
                sb.Append(ut.GetDescription());
                sb.Append(", ");
            }
            if (sb.Length < 3)
            {
                foreach (OutputEnum ut in WorkflowConfig.DestConfig.Outputs)
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
            return GetActiveUploadersDescription<TextUploaderType>(WorkflowConfig.DestConfig.TextUploaders);
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
            sbDebug.AppendLine(string.Format(" File Uploader: {0}", GetActiveUploadersDescription<FileUploaderType>(WorkflowConfig.DestConfig.FileUploaders)));
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

            hi.Filename = Info.FileName;
            hi.Filepath = Info.LocalFilePath;
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

    public class TaskInfo
    {
        public WorkerTask.JobLevel2 Job { get; set; }

        public IntPtr Handle { get; set; }

        public DestSelector DestConfig { get; set; }

        public NotifyIcon TrayIcon { get; set; }

        public string FileName { get; set; }

        public string FileSize { get; set; }

        private string mFilePath;

        public string LocalFilePath
        {
            get
            {
                return mFilePath;
            }
            set
            {
                mFilePath = value;
            }
        }

        public string ExistingFilePath
        {
            get
            {
                return mFilePath;
            }
            set
            {
                if (File.Exists(value))
                {
                    mFilePath = value;
                }
                else
                {
                    throw new Exception(string.Format("{0} does not exist.", value));
                }
            }
        }

        public Size ImageSize { get; set; }

        public string WindowTitleText { get; set; }
    }
}
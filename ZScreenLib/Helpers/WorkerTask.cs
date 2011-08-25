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
using GraphicsMgrLib;
using HelpersLib;
using HistoryLib;
using Microsoft.WindowsAPICodePack.Taskbar;
using RegionCapture;
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
using ZScreenLib.Shapes;
using ZSS.IndexersLib;
using ZUploader.HelperClasses;

namespace ZScreenLib
{
    public class WorkerTask
    {
        #region Enums

        public enum TaskStatus
        {
            Created,
            Prepared,
            Started,
            RetryPending,
            ThreadMode,
            ImageEdited,
            ImageWritten,
            CancellationPending,
            Finished
        }

        public enum JobLevel2
        {
            [Description("Entire Screen")]
            CaptureEntireScreen,
            [Description("Active Window")]
            CaptureActiveWindow,
            [Description("Selected Window")]
            CaptureSelectedWindow,
            [Description("Crop Shot")]
            CaptureRectRegion,
            [Description("Last Crop Shot")]
            CaptureLastCroppedWindow,
            [Description("Auto Capture")]
            AUTO_CAPTURE,
            [Description("Clipboard Upload")]
            UploadFromClipboard,
            [Description("Drag & Drop Window")]
            UploadFromDragDrop,
            [Description("Language Translator")]
            Translate,
            [Description("Screen Color Picker")]
            SCREEN_COLOR_PICKER,
            [Description("Upload Image")]
            UploadImage,
            [Description("Webpage Capture")]
            WEBPAGE_CAPTURE,
            [Description("Freehand Crop Shot")]
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
            IndexFolder
        }

        public enum ProgressType : int
        {
            COPY_TO_CLIPBOARD_IMAGE, // needed only for the feature CopyImageUntilURL
            FLASH_ICON,
            INCREMENT_PROGRESS,
            SET_ICON_BUSY,
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

        public BackgroundWorker MyWorker { get; set; }

        public bool WasToTakeScreenshot { get; set; }

        public JobLevel1 Job1 { get; private set; }  // Image, File, Text

        public JobLevel2 Job2 { get; private set; } // Entire Screen, Active Window, Selected Window, Crop Shot, etc.

        public JobLevel3 Job3 { get; private set; } // Shorten URL, Upload Text, Index Folder, etc.

        public List<string> Errors { get; set; }

        public bool IsError
        {
            get { return Errors != null && Errors.Count > 0; }
        }

        public List<TaskStatus> Status = new List<TaskStatus>();

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

        public int UniqueNumber { get; set; }

        public Image TempImage { get; private set; }

        public string TempText { get; private set; }

        public GoogleTranslateInfo TranslationInfo { get; private set; }

        public string FileName { get; private set; }

        public string LocalFilePath { get; private set; }

        private string DestinationName = string.Empty;

        public List<OutputEnum> TaskOutputs = new List<OutputEnum>();
        public List<ClipboardContentEnum> TaskClipboardContent = new List<ClipboardContentEnum>();
        public List<LinkFormatEnum> MyLinkFormat = new List<LinkFormatEnum>();
        public List<ImageUploaderType> MyImageUploaders = new List<ImageUploaderType>();
        public List<TextUploaderType> MyTextUploaders = new List<TextUploaderType>();
        public List<UrlShortenerType> MyLinkUploaders = new List<UrlShortenerType>();
        public List<FileUploaderType> MyFileUploaders = new List<FileUploaderType>();

        public List<UploadResult> UploadResults { get; private set; }

        #endregion Properties

        #region Constructors

        public WorkerTask()
        {
            UploadResults = new List<UploadResult>();
            Errors = new List<string>();
            Status.Add(TaskStatus.Created);
            MyWorker = new BackgroundWorker() { WorkerReportsProgress = true };
        }

        public WorkerTask(BackgroundWorker worker, JobLevel2 job, DestSelector ucDestOptions, string fp = "")
            : this()
        {
            if (!string.IsNullOrEmpty(fp))
            {
                UpdateLocalFilePath(fp);
            }

            MyWorker = worker;
            AssignJob(job);

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
                    success = CaptureRegion();
                    break;
            }

            if (!success)
            {
                this.Status.Add(TaskStatus.CancellationPending);
            }

            if (PrepareOutputs(ucDestOptions) == DialogResult.Cancel)
            {
                this.Status.Add(TaskStatus.CancellationPending);
            }
        }

        public void AssignJob(JobLevel2 job)
        {
            Job2 = job;

            switch (job)
            {
                case JobLevel2.UploadFromDragDrop:
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
        }

        private DialogResult PrepareOutputs(DestSelector ucDestOptions)
        {
            DialogResult dlgResult = DialogResult.OK;

            if (!Status.Contains(TaskStatus.Prepared) && !Status.Contains(TaskStatus.CancellationPending))
            {
                Adapter.SaveMenuConfigToList<OutputEnum>(ucDestOptions.tsddbOutputs, TaskOutputs);
                Adapter.SaveMenuConfigToList<ClipboardContentEnum>(ucDestOptions.tsddbClipboardContent, TaskClipboardContent);
                Adapter.SaveMenuConfigToList<LinkFormatEnum>(ucDestOptions.tsddbLinkFormat, MyLinkFormat);
                Adapter.SaveMenuConfigToList<ImageUploaderType>(ucDestOptions.tsddbDestImage, MyImageUploaders);
                Adapter.SaveMenuConfigToList<TextUploaderType>(ucDestOptions.tsddDestText, MyTextUploaders);
                Adapter.SaveMenuConfigToList<FileUploaderType>(ucDestOptions.tsddDestFile, MyFileUploaders);
                Adapter.SaveMenuConfigToList<UrlShortenerType>(ucDestOptions.tsddbDestLink, MyLinkUploaders);

                MyWorker.ReportProgress((int)WorkerTask.ProgressType.SET_ICON_BUSY, this);

                if (Engine.conf.PromptForOutputs)
                {
                    dlgResult = SetManualOutputs(LocalFilePath);
                }
                Status.Add(TaskStatus.Prepared);
            }

            if (dlgResult == DialogResult.OK)
            {
                WriteImage();
            }

            return dlgResult;
        }

        #endregion Constructors

        #region Populating Task

        public bool SetImage(Image img)
        {
            if (img != null)
            {
                Engine.MyLogger.WriteLine(string.Format("Setting Image {0}x{1} to WorkerTask", img.Width, img.Height));

                TempImage = img;
                EImageFormat imageFormat;
                WorkerTaskHelper.PrepareImage(TempImage, out imageFormat);

                string fn = WorkerTaskHelper.PrepareFilename(imageFormat, TempImage, GetPatternType());
                string imgfp = FileSystem.GetUniqueFilePath(Engine.ImagesDir, fn);
                UpdateLocalFilePath(imgfp);

                Job1 = JobLevel1.Image;
                if (Engine.conf != null && Engine.conf.ShowOutputsAsap)
                {
                    // IF (Bitmap)img.Clone() IS NOT USED THEN WE ARE GONNA GET CROSS THREAD OPERATION ERRORS! - McoreD
                    MyWorker.ReportProgress((int)WorkerTask.ProgressType.COPY_TO_CLIPBOARD_IMAGE, (Bitmap)img.Clone());
                }

                if (!Status.Contains(TaskStatus.ImageEdited) && Adapter.ActionsEnabled() && Job2 != WorkerTask.JobLevel2.UploadImage)
                {
                    Status.Add(TaskStatus.ImageEdited);
                    ProcessImage(img);
                    PerformActions();
                }
            }

            return TempImage != null;
        }

        public void SetImage(string fp)
        {
            SetImage(GraphicsMgr.GetImageSafely(fp));
        }

        public void SetText(string text)
        {
            Job1 = JobLevel1.Text;
            TempText = text;

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
                    if (!string.IsNullOrEmpty(FileName))
                    {
                        filePath = dialog.FilePath;
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
                ur.LocalFilePath = fp;
                if (!string.IsNullOrEmpty(fp) || !string.IsNullOrEmpty(ur.URL))
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
                LocalFilePath = fp;
                FileName = Path.GetFileName(fp);

                if (ZAppHelper.IsTextFile(fp))
                {
                    Job1 = JobLevel1.Text;
                }
                else if (ZAppHelper.IsImageFile(fp))
                {
                    Job1 = JobLevel1.Image;
                    IsImage = true;
                    if (TempImage == null && GraphicsMgr.IsValidImage(fp))
                    {
                        TempImage = FileSystem.ImageFromFile(fp);
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

        public bool CaptureRegionOrWindow()
        {
            if (!Engine.IsTakingScreenShot)
            {
                Engine.IsTakingScreenShot = true;

                bool windowMode = Job2 == WorkerTask.JobLevel2.CaptureSelectedWindow;

                if (Engine.conf == null) Engine.conf = new XMLSettings();

                try
                {
                    using (Image imgSS = Capture.CaptureScreen(Engine.conf.ShowCursor))
                    {
                        if (Job2 == WorkerTask.JobLevel2.CaptureLastCroppedWindow && !Engine.conf.LastRegion.IsEmpty)
                        {
                            SetImage(GraphicsMgr.CropImage(imgSS, Engine.conf.LastRegion));
                        }
                        else
                        {
                            if (Engine.conf.CropEngineMode == CropEngineType.Cropv2 && !windowMode)
                            {
                                using (Crop2 crop = new Crop2(imgSS))
                                {
                                    if (crop.ShowDialog() == DialogResult.OK)
                                    {
                                        SetImage(crop.GetCroppedScreenshot());
                                    }
                                }
                            }
                            else if (Engine.conf.CropEngineMode == CropEngineType.CropLite && !windowMode)
                            {
                                using (CropLight crop = new CropLight(imgSS))
                                {
                                    if (crop.ShowDialog() == DialogResult.OK)
                                    {
                                        SetImage(GraphicsMgr.CropImage(imgSS, crop.SelectionRectangle));
                                    }
                                }
                            }
                            else
                            {
                                using (Crop c = new Crop(imgSS, windowMode))
                                {
                                    if (c.ShowDialog() == DialogResult.OK)
                                    {
                                        if (Job2 == WorkerTask.JobLevel2.CaptureRectRegion && !Engine.conf.LastRegion.IsEmpty)
                                        {
                                            SetImage(GraphicsMgr.CropImage(imgSS, Engine.conf.LastRegion));
                                        }
                                        else if (windowMode && !Engine.conf.LastCapture.IsEmpty)
                                        {
                                            SetImage(GraphicsMgr.CropImage(imgSS, Engine.conf.LastCapture));
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Engine.MyLogger.WriteException(ex, "Error while capturing region");
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

            return TempImage != null;
        }

        public bool CaptureRegion()
        {
            RegionCapturePreview rcp = new RegionCapturePreview();

            if (rcp.ShowDialog() == DialogResult.OK)
            {
                SetImage(rcp.Result);
            }

            return TempImage != null;
        }

        public bool CaptureFreehandCrop()
        {
            using (FreehandCapture crop = new FreehandCapture())
            {
                if (crop.ShowDialog() == DialogResult.OK)
                {
                    using (Image ss = Capture.CaptureScreen(false))
                    {
                        SetImage(crop.GetScreenshot(ss));
                    }
                }
                else
                {
                    Status.Add(WorkerTask.TaskStatus.RetryPending);
                }
            }

            return TempImage != null;
        }

        /// <summary>
        /// Function to Capture Active Window
        /// </summary>
        public bool CaptureActiveWindow()
        {
            if (TempImage == null)
            {
                SetImage(Capture.CaptureActiveWindow());
            }
            return TempImage != null;
        }

        /// <summary>
        /// Function to Capture Entire Screen
        /// </summary>
        public bool CaptureScreen()
        {
            if (TempImage == null)
            {
                SetImage(Capture.CaptureScreen(Engine.conf != null && Engine.conf.ShowCursor));
            }

            return TempImage != null;
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

                TempImage = img;
            }
        }

        /// <summary>
        /// Perform Actions after capturing image/text/file objects
        /// </summary>
        public void PerformActions()
        {
            foreach (Software app in Engine.conf.ActionsList)
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
                                Greenshot.Helpers.Capture capture = new Greenshot.Helpers.Capture(TempImage);
                                capture.CaptureDetails.Filename = LocalFilePath;
                                capture.CaptureDetails.Title = Path.GetFileNameWithoutExtension(capture.CaptureDetails.Filename);
                                capture.CaptureDetails.AddMetaData("file", capture.CaptureDetails.Filename);
                                capture.CaptureDetails.AddMetaData("source", "file");
                                Greenshot.Drawing.Surface surface = new Greenshot.Drawing.Surface(capture);
                                Greenshot.ImageEditorForm editor = new Greenshot.ImageEditorForm(surface, TaskOutputs.Contains(OutputEnum.LocalDisk)) { Icon = Resources.zss_main };
                                editor.SetImagePath(LocalFilePath);
                                editor.Visible = false;
                                editor.ShowDialog();
                                if (!editor.surface.Modified)
                                {
                                    TempImage = editor.GetImageForExport();
                                }
                            }
                            catch (Exception ex)
                            {
                                Engine.MyLogger.WriteException(ex, "ImageEdit");
                            }
                        }
                        else if (File.Exists(app.Path))
                        {
                            app.OpenFile(LocalFilePath);
                        }
                    }
                }
            }
        }

        #endregion Edit Image

        #region Publish Data

        /// <summary>
        /// Writes MyImage object in a WorkerTask into a file
        /// </summary>
        /// <param name="t">WorkerTask</param>
        public void WriteImage()
        {
            if (TaskOutputs.Contains(OutputEnum.LocalDisk) && TempImage != null && !Status.Contains(TaskStatus.ImageWritten))
            {
                string fp = LocalFilePath;
                Image img = TempImage;
                fp = FileSystem.WriteImage(fp, img);
                Status.Add(TaskStatus.ImageWritten);

                UpdateLocalFilePath(fp);

                if (!File.Exists(LocalFilePath))
                {
                    Errors.Add(string.Format("{0} does not exist", LocalFilePath));
                }
            }
        }

        /// <summary>
        /// Function to edit Image (Screenshot or Picture) in an Image Editor and Upload
        /// </summary>
        public void PublishData()
        {
            if (File.Exists(LocalFilePath) || TempImage != null || !string.IsNullOrEmpty(TempText))
            {
                foreach (OutputEnum oe in TaskOutputs)
                {
                    PublishData(oe);
                }

                if (UploadResults.Count > 0)
                {
                    FlashIcon();
                }
            }
        }

        private void PublishData(OutputEnum oe)
        {
            switch (oe)
            {
                case OutputEnum.Printer:
                    Print();
                    break;
                case OutputEnum.RemoteHost:
                    if (Job1 == JobLevel1.File)
                    {
                        UploadFile();
                    }
                    else
                    {
                        UploadImage();
                    }
                    break;
                case OutputEnum.Email:
                    SendEmail();
                    break;
                case OutputEnum.SharedFolder:
                    UploadToSharedFolder();
                    break;
            }
        }

        public void UploadImage()
        {
            StartTime = DateTime.Now;

            if (TaskOutputs.Contains(OutputEnum.RemoteHost))
            {
                if (Engine.conf != null && Engine.conf.TinyPicSizeCheck && MyImageUploaders.Contains(ImageUploaderType.TINYPIC) && File.Exists(LocalFilePath))
                {
                    SizeF size = Image.FromFile(LocalFilePath).PhysicalDimension;
                    if (size.Width > 1600 || size.Height > 1600)
                    {
                        Engine.MyLogger.WriteLine("Changing from TinyPic to ImageShack due to large image size");
                        if (!MyImageUploaders.Contains(ImageUploaderType.IMAGESHACK))
                        {
                            MyImageUploaders.Add(ImageUploaderType.IMAGESHACK);
                            MyImageUploaders.Remove(ImageUploaderType.TINYPIC);
                        }
                    }
                }

                for (int i = 0; i < MyImageUploaders.Count; i++)
                {
                    UploadImage(MyImageUploaders[i], PrepareData());
                }
            }

            if (TaskClipboardContent.Contains(ClipboardContentEnum.Local))
            {
                AddUploadResult(new UploadResult()
                {
                    Host = ClipboardContentEnum.Local.GetDescription()
                });
            }

            EndTime = DateTime.Now;

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

        private Stream PrepareData()
        {
            Stream data = null;

            if (File.Exists(LocalFilePath))
            {
                data = new FileStream(LocalFilePath, FileMode.Open, FileAccess.Read, FileShare.Read);
            }
            else if (TempImage != null)
            {
                data = TempImage.SaveImage(Engine.conf.ImageFormat);
            }
            else if (!string.IsNullOrEmpty(TempText))
            {
                data = new MemoryStream(Encoding.UTF8.GetBytes(TempText));
            }

            return data;
        }

        private void UploadImage(ImageUploaderType imageUploaderType, Stream data)
        {
            ImageUploader imageUploader = null;

            switch (imageUploaderType)
            {
                case ImageUploaderType.IMAGESHACK:
                    imageUploader = new ImageShackUploader(ZKeys.ImageShackKey, Engine.MyUploadersConfig.ImageShackAccountType,
                        Engine.MyUploadersConfig.ImageShackRegistrationCode)
                    {
                        IsPublic = Engine.MyUploadersConfig.ImageShackShowImagesInPublic
                    };
                    break;
                case ImageUploaderType.TINYPIC:
                    imageUploader = new TinyPicUploader(ZKeys.TinyPicID, ZKeys.TinyPicKey, Engine.MyUploadersConfig.TinyPicAccountType,
                        Engine.MyUploadersConfig.TinyPicRegistrationCode);
                    break;
                case ImageUploaderType.IMGUR:
                    imageUploader = new Imgur(Engine.MyUploadersConfig.ImgurAccountType, ZKeys.ImgurAnonymousKey, Engine.MyUploadersConfig.ImgurOAuthInfo)
                    {
                        ThumbnailType = Engine.MyUploadersConfig.ImgurThumbnailType
                    };
                    break;
                case ImageUploaderType.FLICKR:
                    imageUploader = new FlickrUploader(ZKeys.FlickrKey, ZKeys.FlickrSecret,
                        Engine.MyUploadersConfig.FlickrAuthInfo, Engine.MyUploadersConfig.FlickrSettings);
                    break;
                case ImageUploaderType.UPLOADSCREENSHOT:
                    imageUploader = new UploadScreenshot(ZKeys.UploadScreenshotKey);
                    break;
                case ImageUploaderType.MEDIAWIKI:
                    UploadToMediaWiki();
                    break;
                case ImageUploaderType.TWITPIC:
                    TwitPicOptions twitpicOpt = new TwitPicOptions();
                    twitpicOpt.Username = Engine.MyUploadersConfig.TwitPicUsername;
                    twitpicOpt.Password = Engine.MyUploadersConfig.TwitPicPassword;
                    // twitpicOpt.TwitPicUploadType = Engine.conf.TwitPicUploadMode;
                    twitpicOpt.TwitPicThumbnailMode = Engine.MyUploadersConfig.TwitPicThumbnailMode;
                    twitpicOpt.ShowFull = Engine.MyUploadersConfig.TwitPicShowFull;
                    imageUploader = new TwitPicUploader(twitpicOpt);
                    break;
                case ImageUploaderType.YFROG:
                    YfrogOptions yfrogOp = new YfrogOptions(ZKeys.ImageShackKey);
                    yfrogOp.Username = Engine.MyUploadersConfig.YFrogUsername;
                    yfrogOp.Password = Engine.MyUploadersConfig.YFrogPassword;
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
                Engine.MyLogger.WriteLine("Initialized " + DestinationName);

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
                                string.Format("Retrying... Attempt {1}", imageUploaderType.GetDescription(), i));
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
            StartTime = DateTime.Now;

            MyWorker.ReportProgress((int)WorkerTask.ProgressType.UPDATE_PROGRESS_MAX, TaskbarProgressBarState.Indeterminate);

            if (ShouldShortenURL(TempText))
            {
                // Need this for shortening URL using Clipboard Upload
                ShortenURL(TempText);
            }
            else if (TaskOutputs.Contains(OutputEnum.RemoteHost))
            {
                foreach (TextUploaderType textUploaderType in MyTextUploaders)
                {
                    UploadText(textUploaderType);
                }
            }

            EndTime = DateTime.Now;
        }

        private void UploadText(TextUploaderType textUploaderType)
        {
            TextUploader textUploader = null;

            switch (textUploaderType)
            {
                case TextUploaderType.PASTEBIN:
                    textUploader = new PastebinUploader(ZKeys.PastebinKey, Engine.MyUploadersConfig.PastebinSettings);
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
                Engine.MyLogger.WriteLine("Uploading to " + DestinationName);

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

        public void UploadFile()
        {
            StartTime = DateTime.Now;

            Engine.MyLogger.WriteLine("Uploading File: " + LocalFilePath);

            foreach (FileUploaderType fileUploaderType in MyFileUploaders)
            {
                UploadFile(fileUploaderType, PrepareData());
            }

            EndTime = DateTime.Now;
        }

        private void UploadFile(FileUploaderType fileUploaderType, Stream data)
        {
            FileUploader fileUploader = null;

            switch (fileUploaderType)
            {
                case FileUploaderType.FTP:
                    if (Engine.conf.ShowFTPSettingsBeforeUploading)
                    {
                        UploadersConfigForm ucf = new UploadersConfigForm(Engine.MyUploadersConfig, ZKeys.GetAPIKeys());
                        ucf.Icon = Resources.zss_main;
                        ucf.tcUploaders.SelectedTab = ucf.tpFileUploaders;
                        ucf.tcFileUploaders.SelectedTab = ucf.tpFTP;
                        ucf.ShowDialog();
                    }
                    switch (Job1)
                    {
                        case JobLevel1.Text:
                            UploadToFTP(Engine.MyUploadersConfig.FTPSelectedText, data);
                            break;
                        case JobLevel1.Image:
                            UploadToFTP(Engine.MyUploadersConfig.FTPSelectedImage, data);
                            break;
                        default:
                        case JobLevel1.File:
                            UploadToFTP(Engine.MyUploadersConfig.FTPSelectedFile, data);
                            break;
                    }
                    break;
                case FileUploaderType.Dropbox:
                    string uploadPath = new NameParser { IsFolderPath = true }.Convert(Dropbox.TidyUploadPath(Engine.MyUploadersConfig.DropboxUploadPath));
                    fileUploader = new Dropbox(Engine.MyUploadersConfig.DropboxOAuthInfo, uploadPath, Engine.MyUploadersConfig.DropboxAccountInfo);
                    break;
                case FileUploaderType.SendSpace:
                    fileUploader = new SendSpace(ZKeys.SendSpaceKey);
                    switch (Engine.MyUploadersConfig.SendSpaceAccountType)
                    {
                        case AccountType.Anonymous:
                            SendSpaceManager.PrepareUploadInfo(ZKeys.SendSpaceKey);
                            break;
                        case AccountType.User:
                            SendSpaceManager.PrepareUploadInfo(ZKeys.SendSpaceKey, Engine.MyUploadersConfig.SendSpaceUsername,
                                Engine.MyUploadersConfig.SendSpacePassword);
                            break;
                    }
                    break;
                case FileUploaderType.RapidShare:
                    fileUploader = new RapidShare(Engine.MyUploadersConfig.RapidShareUserAccountType, Engine.MyUploadersConfig.RapidShareUsername,
                        Engine.MyUploadersConfig.RapidSharePassword);
                    break;
                case FileUploaderType.CustomUploader:
                    fileUploader = new CustomUploader(Engine.MyUploadersConfig.CustomUploadersList[Engine.MyUploadersConfig.CustomUploaderSelected]);
                    break;
            }

            if (fileUploader != null)
            {
                MyWorker.ReportProgress((int)WorkerTask.ProgressType.UPDATE_PROGRESS_MAX, TaskbarProgressBarState.Indeterminate);
                DestinationName = fileUploaderType.GetDescription();
                fileUploader.ProgressChanged += UploadProgressChanged;
                UploadResult ur = new UploadResult();
                ur = fileUploader.Upload(data, FileName);
                ur.Host = fileUploaderType.GetDescription();
                AddUploadResult(ur);
                Errors = fileUploader.Errors;
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
                    FTPAccount acc = Engine.MyUploadersConfig.FTPAccountList[FtpAccountId];
                    DestinationName = string.Format("FTP - {0}", acc.Name);
                    Engine.MyLogger.WriteLine(string.Format("Uploading {0} to FTP: {1}", FileName, acc.Host));

                    FTPUploader fu = new FTPUploader(acc);
                    fu.ProgressChanged += new Uploader.ProgressEventHandler(UploadProgressChanged);

                    MyWorker.ReportProgress((int)WorkerTask.ProgressType.UPDATE_PROGRESS_MAX, TaskbarProgressBarState.Normal);

                    string url = File.Exists(LocalFilePath) ? fu.Upload(LocalFilePath).URL : fu.Upload(data, FileName).URL;

                    if (!string.IsNullOrEmpty(url))
                    {
                        AddUploadResult(new UploadResult() { Host = FileUploaderType.FTP.GetDescription(), URL = url });

                        if (CreateThumbnail())
                        {
                            double thar = (double)Engine.MyUploadersConfig.FTPThumbnailWidthLimit / (double)TempImage.Width;
                            using (Image img = GraphicsMgr.ChangeImageSize(TempImage, Engine.MyUploadersConfig.FTPThumbnailWidthLimit,
                                (int)(thar * TempImage.Height)))
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
                                        ur = new UploadResult();
                                        ur.Host = FileUploaderType.FTP.GetDescription();
                                        ur.ThumbnailURL = thumb;
                                        AddUploadResult(ur);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Engine.MyLogger.WriteException(ex, "Error while uploading to FTP Server");
                Errors.Add("FTP upload failed.\r\n" + ex.Message);
            }

            return ur;
        }

        public void Print()
        {
            if (TaskOutputs.Contains(OutputEnum.Printer))
            {
                if (TempImage != null)
                {
                    MyWorker.ReportProgress((int)ProgressType.PrintImage, (Image)TempImage.Clone());
                }
                else if (!string.IsNullOrEmpty(TempText))
                {
                    MyWorker.ReportProgress((int)ProgressType.PrintText, TempText);
                }
            }
        }

        public void SendEmail()
        {
            EmailForm emailForm = new EmailForm(Engine.MyUploadersConfig.EmailRememberLastTo ? Engine.MyUploadersConfig.EmailLastTo : string.Empty,
                Engine.MyUploadersConfig.EmailDefaultSubject, Engine.MyUploadersConfig.EmailDefaultBody);

            if (emailForm.ShowDialog() == DialogResult.OK)
            {
                if (Engine.MyUploadersConfig.EmailRememberLastTo)
                {
                    Engine.MyUploadersConfig.EmailLastTo = emailForm.ToEmail;
                }

                Email email = new Email
                {
                    SmtpServer = Engine.MyUploadersConfig.EmailSmtpServer,
                    SmtpPort = Engine.MyUploadersConfig.EmailSmtpPort,
                    FromEmail = Engine.MyUploadersConfig.EmailFrom,
                    Password = Engine.MyUploadersConfig.EmailPassword
                };

                Stream emailData = null;
                try
                {
                    emailData = PrepareData();

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
            if (Engine.MyUploadersConfig.LocalhostAccountList.CheckSelected(Engine.MyUploadersConfig.LocalhostSelected))
            {
                LocalhostAccount acc = Engine.MyUploadersConfig.LocalhostAccountList[Engine.MyUploadersConfig.LocalhostSelected];
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
                else if (TempImage != null)
                {
                    EImageFormat imageFormat;
                    WorkerTaskHelper.PrepareImage(TempImage, out imageFormat);
                    fn = WorkerTaskHelper.PrepareFilename(imageFormat, TempImage, GetPatternType());
                    string fp = acc.GetLocalhostPath(fn);
                    FileSystem.WriteImage(fp, TempImage);
                }
                else if (!string.IsNullOrEmpty(TempText))
                {
                    fn = new NameParser(NameParserType.EntireScreen).Convert(Engine.conf.EntireScreenPattern) + ".txt";
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

            if (Engine.MyUploadersConfig.MediaWikiAccountList.CheckSelected(Engine.MyUploadersConfig.MediaWikiAccountSelected) && File.Exists(fullFilePath))
            {
                MediaWikiAccount acc = Engine.MyUploadersConfig.MediaWikiAccountList[Engine.MyUploadersConfig.MediaWikiAccountSelected];
                System.Net.IWebProxy proxy = Adapter.CheckProxySettings().GetWebProxy;
                DestinationName = acc.Name;
                Engine.MyLogger.WriteLine(string.Format("Uploading {0} to MediaWiki: {1}", FileName, acc.Url));
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
                UploadInfo uploadInfo = UploadManager.GetInfo(UniqueNumber);
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
                if (Engine.conf.ShortenUrlAfterUpload)
                {
                    Engine.MyLogger.WriteLine(string.Format("URL Length: {0}; Shortening after {1}", url.Length.ToString(), Engine.conf.ShortenUrlAfterUploadAfter));
                }
                return Engine.conf.TwitterEnabled ||
                    Engine.conf.ShortenUrlUsingClipboardUpload && Job2 == JobLevel2.UploadFromClipboard && FileSystem.IsValidLink(TempText) ||
                    Engine.conf.ShortenUrlAfterUpload && url.Length > Engine.conf.ShortenUrlAfterUploadAfter ||
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
                else if (MyLinkUploaders.Contains(UrlShortenerType.Google))
                {
                    us = new GoogleURLShortener(ZKeys.GoogleURLShortenerKey);
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
                        Engine.MyLogger.WriteLine(string.Format("Shortened URL: {0}", shortenUrl));
                        ur.ShortenedURL = shortenUrl;
                        return true;
                    }
                }
            }

            return false;
        }

        public bool JobIsImageToClipboard()
        {
            return TaskOutputs.Contains(OutputEnum.Clipboard) && TaskClipboardContent.Contains(ClipboardContentEnum.Data) && TempImage != null;
        }

        private bool CreateThumbnail()
        {
            return GraphicsMgr.IsValidImage(LocalFilePath) && TempImage != null &&
                (Engine.conf.ConfLinkFormat.Contains((int)LinkFormatEnum.LINKED_THUMBNAIL) ||
                  Engine.conf.ConfLinkFormat.Contains((int)LinkFormatEnum.LINKED_THUMBNAIL_WIKI) ||
                  Engine.conf.ConfLinkFormat.Contains((int)LinkFormatEnum.LinkedThumbnailHtml) ||
                  Engine.conf.ConfLinkFormat.Contains((int)LinkFormatEnum.THUMBNAIL)) &&
                (!Engine.MyUploadersConfig.FTPThumbnailCheckSize || (Engine.MyUploadersConfig.FTPThumbnailCheckSize &&
                (TempImage.Width > Engine.MyUploadersConfig.FTPThumbnailWidthLimit)));
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
            if (!string.IsNullOrEmpty(FileName))
            {
                return FileName;
            }
            else if (TempImage != null)
            {
                return string.Format("Image ({0}x{1})", TempImage.Width, TempImage.Height);
            }
            return Application.ProductName;
        }

        public string GetOutputsDescription()
        {
            StringBuilder sb = new StringBuilder();
            foreach (OutputEnum ut in TaskOutputs)
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
                foreach (OutputEnum ut in TaskOutputs)
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

        #endregion Helper Methods
    }
}
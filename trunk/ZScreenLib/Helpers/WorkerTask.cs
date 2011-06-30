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
using System.Drawing.Imaging;
using System.IO;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Crop;
using GraphicsMgrLib;
using HelpersLib;
using HistoryLib;
using Microsoft.WindowsAPICodePack.Taskbar;
using UploadersAPILib;
using UploadersLib;
using UploadersLib.FileUploaders;
using UploadersLib.HelperClasses;
using UploadersLib.ImageUploaders;
using UploadersLib.OtherServices;
using UploadersLib.TextUploaders;
using UploadersLib.URLShorteners;
using ZScreenLib.Properties;
using ZScreenLib.Shapes;
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
            LANGUAGE_TRANSLATOR,
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
            ShowTrayWarning,
            PrintText,
            PrintImage,
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

        public TaskStatus Status { get; set; }

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

        public byte[] TempFile { get; set; }

        public GoogleTranslateInfo TranslationInfo { get; private set; }

        public string FileName { get; set; }

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

        public bool IsTakingScreenShot;

        #region Constructors

        public WorkerTask()
        {
            this.UploadResults = new List<UploadResult>();
            this.Errors = new List<string>();
            this.Status = TaskStatus.Created;
            this.MyWorker = new BackgroundWorker() { WorkerReportsProgress = true };
        }

        public WorkerTask(BackgroundWorker worker)
            : this()
        {
            this.MyWorker = worker;
        }

        public void AssignJob(JobLevel2 job)
        {
            this.Job2 = job;

            switch (job)
            {
                case JobLevel2.UploadFromDragDrop:
                case JobLevel2.UploadFromClipboard:
                    Job1 = JobLevel1.File;
                    break;
                case JobLevel2.LANGUAGE_TRANSLATOR:
                    this.Job1 = JobLevel1.Text;
                    break;
                default:
                    Job1 = JobLevel1.Image;
                    break;
            }
        }

        /// <summary>
        /// Constructor taking Worker and Job
        /// </summary>
        /// <param name="worker"></param>
        /// <param name="job"></param>
        public WorkerTask(BackgroundWorker worker, JobLevel2 job)
            : this(worker)
        {
            this.AssignJob(job);
        }

        public void PrepareTask(DestSelector ucDestOptions)
        {
            if (this.Status != TaskStatus.Prepared)
            {
                Adapter.SaveMenuConfigToList<OutputEnum>(ucDestOptions.tsddbOutputs, TaskOutputs);
                Adapter.SaveMenuConfigToList<ClipboardContentEnum>(ucDestOptions.tsddbClipboardContent, TaskClipboardContent);
                Adapter.SaveMenuConfigToList<LinkFormatEnum>(ucDestOptions.tsddbLinkFormat, MyLinkFormat);
                Adapter.SaveMenuConfigToList<ImageUploaderType>(ucDestOptions.tsddbDestImage, MyImageUploaders);
                Adapter.SaveMenuConfigToList<TextUploaderType>(ucDestOptions.tsddDestText, MyTextUploaders);
                Adapter.SaveMenuConfigToList<FileUploaderType>(ucDestOptions.tsddDestFile, MyFileUploaders);
                Adapter.SaveMenuConfigToList<UrlShortenerType>(ucDestOptions.tsddbDestLink, MyLinkUploaders);
                this.Status = TaskStatus.Prepared;
            }
        }

        #endregion Constructors

        public bool CanStartWork()
        {
            bool can = this.WasToTakeScreenshot && this.MyImageUploaders.Count > 0;
            if (!this.WasToTakeScreenshot)
            {
                switch (this.Job1)
                {
                    case JobLevel1.Image:
                        can = MyImageUploaders.Count > 0;
                        break;
                    case JobLevel1.Text:
                        can = MyTextUploaders.Count > 0;
                        break;
                    case JobLevel1.File:
                        can = MyFileUploaders.Count > 0;
                        break;
                }
            }
            return can;
        }

        #region Populating Task

        public void SetImage(Image img)
        {
            if (img != null)
            {
                Engine.MyLogger.WriteLine(string.Format("Setting Image {0}x{1} to WorkerTask", img.Width, img.Height));
                this.TempImage = img;
                this.Job1 = JobLevel1.Image;
                if (Engine.conf != null && Engine.conf.CopyImageUntilURL)
                {
                    // IF (Bitmap)img.Clone() IS NOT USED THEN WE ARE GONNA GET CROSS THREAD OPERATION ERRORS! - McoreD
                    this.MyWorker.ReportProgress((int)WorkerTask.ProgressType.COPY_TO_CLIPBOARD_IMAGE, (Bitmap)img.Clone());
                }
            }
        }

        public void SetImage(string fp)
        {
            SetImage(GraphicsMgr.GetImageSafely(fp));
        }

        public void SetText(string text)
        {
            this.Job1 = JobLevel1.Text;
            this.TempText = text;

            if (Directory.Exists(text))
            {
                this.Job3 = WorkerTask.JobLevel3.IndexFolder;
            }
            else
            {
                this.Job3 = WorkerTask.JobLevel3.UploadText;
            }
        }

        /// <summary>
        /// Sets the file to save the image to.
        /// If the user activated the "prompt for filename" option, then opens a dialog box.
        /// </summary>
        /// <param name="fileName">the base name</param>
        /// <returns>true if the screenshot should be saved, or false if the user canceled</returns>
        public bool SetFilePathFromPattern(string fileName)
        {
            string dir = Engine.ImagesDir;
            string filePath = FileSystem.GetUniqueFilePath(Path.Combine(dir, fileName + "." + Engine.zImageFileFormat.Extension));

            if (Engine.conf.ManualNaming)
            {
                DestOptions dialog = new DestOptions(this)
                {
                    Title = "Browse for a file path...",
                    FilePath = filePath,
                    Icon = Resources.zss_main
                };
                NativeMethods.SetForegroundWindow(dialog.Handle);
                dialog.ShowDialog();
                if (dialog.DialogResult == DialogResult.OK)
                {
                    if (string.IsNullOrEmpty(this.FileName) || !this.LocalFilePath.Equals(dialog.FilePath))
                    {
                        this.UpdateLocalFilePath(dialog.FilePath);
                    }
                }
                else
                {
                    return false;
                }
            }

            StringBuilder sbPath = new StringBuilder();
            if (string.IsNullOrEmpty(this.FileName))
            {
                this.FileName = Path.GetFileNameWithoutExtension(filePath);
            }

            sbPath.Append(Path.Combine(Path.GetDirectoryName(filePath), this.FileName));
            sbPath.Append(Path.GetExtension(filePath));
            filePath = sbPath.ToString();
            // make sure this length is less than 256 char
            if (filePath.Length > 256)
            {
                int extraChar = filePath.Length - 256;
                string fn = Path.GetFileNameWithoutExtension(filePath);
                string fnn = fn.Substring(0, fn.Length - extraChar);
                filePath = Path.Combine(dir, fnn) + Path.GetExtension(filePath);
            }
            UpdateLocalFilePath(filePath);
            return true;
        }

        public void AddUploadResult(UploadResult ur)
        {
            if (ur != null && !ExistsUploadResult(ur))
            {
                string fp = this.LocalFilePath;
                if (Engine.IsPortable)
                {
                    fp = Path.Combine(Application.StartupPath, fp);
                    this.UpdateLocalFilePath(fp);
                }
                ur.LocalFilePath = fp;
                if (!string.IsNullOrEmpty(fp) || !string.IsNullOrEmpty(ur.URL) || TempImage != null)
                {
                    this.UploadResults.Add(ur);
                }
            }
        }

        private bool ExistsUploadResult(UploadResult ur2)
        {
            foreach (UploadResult ur1 in this.UploadResults)
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
            this.FileName = Path.GetFileName(fp);
            this.LocalFilePath = fp;

            if (ZAppHelper.IsTextFile(fp))
            {
                this.Job1 = JobLevel1.Text;
            }
            else if (ZAppHelper.IsImageFile(fp))
            {
                this.Job1 = JobLevel1.Image;
                this.IsImage = true;
                if (GraphicsMgr.IsValidImage(fp) && this.TempImage == null)
                {
                    this.TempImage = FileSystem.ImageFromFile(fp);
                }
            }
            else
            {
                this.Job1 = JobLevel1.File;
            }
        }

        #endregion Populating Task

        #region Capture

        public bool BwCaptureRegionOrWindow()
        {
            IsTakingScreenShot = true;
            bool success = false;
            bool windowMode = this.Job2 == WorkerTask.JobLevel2.CaptureSelectedWindow;

            if (Engine.conf == null) Engine.conf = new XMLSettings();

            try
            {
                using (Image imgSS = Capture.CaptureScreen(Engine.conf.ShowCursor))
                {
                    if (this.Job2 == WorkerTask.JobLevel2.CaptureLastCroppedWindow && !Engine.LastRegion.IsEmpty)
                    {
                        this.SetImage(GraphicsMgr.CropImage(imgSS, Engine.LastRegion));
                    }
                    else
                    {
                        if (Engine.conf.UseCropBeta && !windowMode)
                        {
                            using (Crop2 crop = new Crop2(imgSS))
                            {
                                if (crop.ShowDialog() == DialogResult.OK)
                                {
                                    this.SetImage(crop.GetCroppedScreenshot());
                                }
                            }
                        }
                        else if (Engine.conf.UseCropLight && !windowMode)
                        {
                            using (CropLight crop = new CropLight(imgSS))
                            {
                                if (crop.ShowDialog() == DialogResult.OK)
                                {
                                    this.SetImage(GraphicsMgr.CropImage(imgSS, crop.SelectionRectangle));
                                }
                            }
                        }
                        else
                        {
                            using (Crop c = new Crop(imgSS, windowMode))
                            {
                                if (c.ShowDialog() == DialogResult.OK)
                                {
                                    if (this.Job2 == WorkerTask.JobLevel2.CaptureRectRegion && !Engine.LastRegion.IsEmpty)
                                    {
                                        this.SetImage(GraphicsMgr.CropImage(imgSS, Engine.LastRegion));
                                    }
                                    else if (windowMode && !Engine.LastCapture.IsEmpty)
                                    {
                                        this.SetImage(GraphicsMgr.CropImage(imgSS, Engine.LastCapture));
                                    }
                                }
                            }
                        }
                    }
                }

                IsTakingScreenShot = false;

                if (this.TempImage != null)
                {
                    bool roundedShadowCorners = false;
                    if (windowMode && Engine.conf.SelectedWindowRoundedCorners || !windowMode && Engine.conf.CropShotRoundedCorners)
                    {
                        this.SetImage(GraphicsMgr.RemoveCorners(this.TempImage, null));
                        roundedShadowCorners = true;
                    }
                    if (windowMode && Engine.conf.SelectedWindowShadow || !windowMode && Engine.conf.CropShotShadow)
                    {
                        this.SetImage(GraphicsMgr.AddBorderShadow(this.TempImage, roundedShadowCorners));
                    }
                }
                success = true;
            }
            catch (Exception ex)
            {
                Engine.MyLogger.WriteException(ex, "Error while capturing region");
                this.Errors.Add(ex.Message);
                if (Engine.conf.CaptureEntireScreenOnError)
                {
                    CaptureScreen();
                }
            }
            finally
            {
                this.MyWorker.ReportProgress((int)WorkerTask.ProgressType.UpdateCropMode);
                IsTakingScreenShot = false;
            }
            return success;
        }

        public void BwCaptureFreehandCrop()
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
                    Status = WorkerTask.TaskStatus.RetryPending;
                }
            }
        }

        /// <summary>
        /// Function to Capture Active Window
        /// </summary>
        public void CaptureActiveWindow()
        {
            if (this.TempImage == null)
            {
                this.SetImage(Capture.CaptureActiveWindow());
            }
        }

        /// <summary>
        /// Function to Capture Entire Screen
        /// </summary>
        public void CaptureScreen()
        {
            if (this.TempImage == null)
            {
                this.SetImage(Capture.CaptureScreen(Engine.conf != null && Engine.conf.ShowCursor));
            }
        }

        #endregion Capture

        #region Google Translate

        public void SetTranslationInfo(GoogleTranslateInfo gti)
        {
            this.Job1 = JobLevel1.Text;
            this.TranslationInfo = gti;
        }

        #endregion Google Translate

        #region Actions

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
                        Job1 == JobLevel1.Image && app.TriggerForImages && !this.WasToTakeScreenshot ||
                        Job1 == JobLevel1.Image && app.TriggerForScreenshots && this.WasToTakeScreenshot ||
                        Job1 == JobLevel1.Text && app.TriggerForText)
                    {
                        if (app.Name == Engine.zImageAnnotator)
                        {
                            try
                            {
                                Greenshot.Configuration.AppConfig.ConfigPath = Path.Combine(Engine.SettingsDir, "ImageEditor.bin");
                                Greenshot.ImageEditorForm editor = new Greenshot.ImageEditorForm { Icon = Resources.zss_main };
                                editor.AutoSave = Engine.conf.ImageEditorAutoSave;
                                editor.MyWorker = MyWorker;
                                editor.SetImage(TempImage);
                                editor.SetImagePath(LocalFilePath);
                                editor.ShowDialog();
                                this.SetImage(editor.GetImage());
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

        #endregion Actions

        #region Publish Data

        /// <summary>
        /// Writes MyImage object in a WorkerTask into a file
        /// </summary>
        /// <param name="t">WorkerTask</param>
        public void WriteImage()
        {
            if (this.TaskOutputs.Contains(OutputEnum.LocalDisk) && File.Exists(this.LocalFilePath) && this.TempImage != null)
            {
                NameParserType type;
                string pattern = string.Empty;
                if (this.Job2 == WorkerTask.JobLevel2.CaptureActiveWindow)
                {
                    type = NameParserType.ActiveWindow;
                    pattern = Engine.conf.ActiveWindowPattern;
                }
                else
                {
                    type = NameParserType.EntireScreen;
                    pattern = Engine.conf.EntireScreenPattern;
                }

                using (NameParser parser = new NameParser(type) { AutoIncrementNumber = Engine.conf.AutoIncrement })
                {
                    if (this.SetFilePathFromPattern(parser.Convert(pattern)))
                    {
                        Engine.conf.AutoIncrement = parser.AutoIncrementNumber;

                        Image img = TempImage;
                        string fp = LocalFilePath;

                        img = ImageEffects.ApplySizeChanges(img);
                        img = ImageEffects.ApplyScreenshotEffects(img);
                        if (Job2 != WorkerTask.JobLevel2.UploadFromClipboard || !Engine.conf.WatermarkExcludeClipboardUpload)
                        {
                            img = ImageEffects.ApplyWatermark(img);
                        }

                        FileSystem.WriteImage(fp, img);

                        UpdateLocalFilePath(fp);

                        if (!File.Exists(this.LocalFilePath))
                        {
                            this.Errors.Add(string.Format("{0} does not exist", this.LocalFilePath));
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Function to edit Image (Screenshot or Picture) in an Image Editor and Upload
        /// </summary>
        /// <param name="task"></param>
        public void PublishData()
        {
            if (Job1 == JobLevel1.File)
            {
                UploadFile();
            }
            else
            {
                PublishImage();
            }

            if (TaskOutputs.Contains(OutputEnum.SharedFolder))
            {
                UploadToSharedFolder();
            }

            if (this.UploadResults.Count > 0)
            {
                FlashIcon();
            }
        }

        public void PublishImage()
        {
            if (TempImage != null && Adapter.ActionsEnabled() && Job2 != WorkerTask.JobLevel2.UploadImage)
            {
                PerformActions();
            }

            UploadImage();
        }

        public void UploadImageRemote()
        {
            if (Engine.conf != null && Engine.conf.TinyPicSizeCheck && this.MyImageUploaders.Contains(ImageUploaderType.TINYPIC) && File.Exists(this.LocalFilePath))
            {
                SizeF size = Image.FromFile(this.LocalFilePath).PhysicalDimension;
                if (size.Width > 1600 || size.Height > 1600)
                {
                    Engine.MyLogger.WriteLine("Changing from TinyPic to ImageShack due to large image size");
                    if (!this.MyImageUploaders.Contains(ImageUploaderType.IMAGESHACK))
                    {
                        this.MyImageUploaders.Add(ImageUploaderType.IMAGESHACK);
                        this.MyImageUploaders.Remove(ImageUploaderType.TINYPIC);
                    }
                }
            }

            List<Thread> uploaderThreads = new List<Thread>();

            if (MyImageUploaders.Contains(ImageUploaderType.IMAGESHACK))
            {
                uploaderThreads.Add(new Thread(() =>
               {
                   ImageShackUploader imageUploader = new ImageShackUploader(ZKeys.ImageShackKey, Engine.MyUploadersConfig.ImageShackAccountType,
                       Engine.MyUploadersConfig.ImageShackRegistrationCode)
                   {
                       IsPublic = Engine.MyUploadersConfig.ImageShackShowImagesInPublic
                   };
                   UploadImage(ImageUploaderType.IMAGESHACK, imageUploader);
               }));
            }

            if (MyImageUploaders.Contains(ImageUploaderType.TINYPIC))
            {
                uploaderThreads.Add(new Thread(() =>
               {
                   TinyPicUploader imageUploader = new TinyPicUploader(ZKeys.TinyPicID, ZKeys.TinyPicKey, Engine.MyUploadersConfig.TinyPicAccountType,
                       Engine.MyUploadersConfig.TinyPicRegistrationCode);
                   UploadImage(ImageUploaderType.TINYPIC, imageUploader);
               }));
            }

            if (MyImageUploaders.Contains(ImageUploaderType.IMGUR))
            {
                uploaderThreads.Add(new Thread(() =>
                {
                    Imgur imageUploader = new Imgur(Engine.MyUploadersConfig.ImgurAccountType, ZKeys.ImgurAnonymousKey, Engine.MyUploadersConfig.ImgurOAuthInfo);
                    UploadImage(ImageUploaderType.IMGUR, imageUploader);
                }));
            }

            if (MyImageUploaders.Contains(ImageUploaderType.FLICKR))
            {
                uploaderThreads.Add(new Thread(() =>
                {
                    FlickrUploader imageUploader = new FlickrUploader(ZKeys.FlickrKey, ZKeys.FlickrSecret,
                        Engine.MyUploadersConfig.FlickrAuthInfo, Engine.MyUploadersConfig.FlickrSettings);
                    UploadImage(ImageUploaderType.FLICKR, imageUploader);
                }));
            }

            if (MyImageUploaders.Contains(ImageUploaderType.UPLOADSCREENSHOT))
            {
                uploaderThreads.Add(new Thread(() =>
                {
                    UploadScreenshot imageUploader = new UploadScreenshot(ZKeys.UploadScreenshotKey);
                    UploadImage(ImageUploaderType.UPLOADSCREENSHOT, imageUploader);
                }));
            }

            if (MyImageUploaders.Contains(ImageUploaderType.MEDIAWIKI))
            {
                uploaderThreads.Add(new Thread(() =>
                {
                    UploadToMediaWiki();
                }));
            }

            if (MyImageUploaders.Contains(ImageUploaderType.TWITPIC))
            {
                uploaderThreads.Add(new Thread(() =>
                {
                    TwitPicOptions twitpicOpt = new TwitPicOptions();
                    twitpicOpt.Username = Engine.MyUploadersConfig.TwitPicUsername;
                    twitpicOpt.Password = Engine.MyUploadersConfig.TwitPicPassword;
                    // twitpicOpt.TwitPicUploadType = Engine.conf.TwitPicUploadMode;
                    twitpicOpt.TwitPicThumbnailMode = Engine.MyUploadersConfig.TwitPicThumbnailMode;
                    twitpicOpt.ShowFull = Engine.MyUploadersConfig.TwitPicShowFull;
                    UploadImage(ImageUploaderType.TWITPIC, new TwitPicUploader(twitpicOpt));
                }));
            }

            if (MyImageUploaders.Contains(ImageUploaderType.YFROG))
            {
                uploaderThreads.Add(new Thread(() =>
                {
                    YfrogOptions yfrogOp = new YfrogOptions(ZKeys.ImageShackKey);
                    yfrogOp.Username = Engine.MyUploadersConfig.YFrogUsername;
                    yfrogOp.Password = Engine.MyUploadersConfig.YFrogPassword;
                    yfrogOp.Source = Application.ProductName;
                    // yfrogOp.UploadType = Engine.conf.YfrogUploadMode;
                    UploadImage(ImageUploaderType.YFROG, new YfrogUploader(yfrogOp));
                }));
            }

            if (MyImageUploaders.Contains(ImageUploaderType.TWITSNAPS))
            {
                uploaderThreads.Add(new Thread(() =>
                {
                    UploadImage(ImageUploaderType.TWITSNAPS, new TwitSnapsUploader(ZKeys.TwitsnapsKey, Adapter.TwitterGetActiveAccount()));
                }));
            }

            if (MyImageUploaders.Contains(ImageUploaderType.FileUploader))
            {
                uploaderThreads.Add(new Thread(() =>
                {
                    UploadFile();
                }));
            }

            foreach (Thread t in uploaderThreads)
            {
                t.Start();
            }

            foreach (Thread t in uploaderThreads)
            {
                t.Join();
            }
        }

        public void UploadImage()
        {
            this.StartTime = DateTime.Now;

            if (TaskOutputs.Contains(OutputEnum.RemoteHost))
            {
                UploadImageRemote();
            }

            if (TaskClipboardContent.Contains(ClipboardContentEnum.Local))
            {
                this.AddUploadResult(new UploadResult()
                {
                    Host = ClipboardContentEnum.Local.GetDescription()
                });
            }

            if (TaskOutputs.Contains(OutputEnum.Printer))
            {
                if (this.TempImage != null)
                {
                    this.MyWorker.ReportProgress(101, (Image)this.TempImage.Clone());
                }
            }

            this.EndTime = DateTime.Now;

            if (Engine.conf != null && Engine.conf.ImageUploadRetryOnTimeout && this.UploadDuration > (int)Engine.conf.UploadDurationLimit)
            {
                if (!this.MyImageUploaders.Contains(ImageUploaderType.TINYPIC))
                {
                    this.MyImageUploaders.Add(ImageUploaderType.TINYPIC);
                }
                else if (!this.MyImageUploaders.Contains(ImageUploaderType.TINYPIC))
                {
                    this.MyImageUploaders.Add(ImageUploaderType.IMAGESHACK);
                }
            }
        }

        private void UploadImage(ImageUploaderType ut, ImageUploader uploader)
        {
            if (uploader != null)
            {
                uploader.ProgressChanged += (x) => UploadProgressChanged(x);
                this.DestinationName = this.GetActiveImageUploadersDescription();
                Engine.MyLogger.WriteLine("Initialized " + this.DestinationName);
                string fullFilePath = this.LocalFilePath;
                if (File.Exists(fullFilePath) || this.TempImage != null)
                {
                    if (Engine.conf == null)
                    {
                        Engine.conf = new XMLSettings();
                    }
                    for (int i = 0; i <= (int)Engine.conf.ErrorRetryCount; i++)
                    {
                        UploadResult ur = new UploadResult();
                        if (File.Exists(fullFilePath))
                        {
                            ur = uploader.Upload(fullFilePath);
                        }
                        else if (this.TempImage != null)
                        {
                            FileName = new NameParser(NameParserType.EntireScreen).Convert(Engine.conf.EntireScreenPattern) + ".png";
                            MemoryStream ms = new MemoryStream();
                            this.TempImage.Save(ms, ImageFormat.Png);
                            ur = uploader.Upload(ms, this.FileName);
                        }
                        ur.Host = ut.GetDescription();
                        this.AddUploadResult(ur);
                        this.Errors = uploader.Errors;

                        if (this.UploadResults.Count > 0 && string.IsNullOrEmpty(this.UploadResults[UploadResults.Count - 1].URL))
                        {
                            this.MyWorker.ReportProgress((int)ZScreenLib.WorkerTask.ProgressType.ShowTrayWarning, string.Format("Retrying... Attempt {1}", ut.GetDescription(), i));
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
            this.StartTime = DateTime.Now;
            this.MyWorker.ReportProgress((int)WorkerTask.ProgressType.UPDATE_PROGRESS_MAX, TaskbarProgressBarState.Indeterminate);

            if (this.ShouldShortenURL(this.TempText))
            {
                // Need this for shortening URL using Clipboard Upload http://imgur.com/DzBJQ.png
                this.ShortenURL(this.TempText);
            }
            else
            {
                if (MyTextUploaders.Contains(TextUploaderType.PASTEBIN))
                {
                    UploadText(TextUploaderType.PASTEBIN, new PastebinUploader(ZKeys.PastebinKey, Engine.MyUploadersConfig.PastebinSettings));
                }
                if (MyTextUploaders.Contains(TextUploaderType.PASTEBIN_CA))
                {
                    UploadText(TextUploaderType.PASTEBIN_CA, new PastebinCaUploader(ZKeys.PastebinCaKey));
                }
                if (MyTextUploaders.Contains(TextUploaderType.PASTE2))
                {
                    UploadText(TextUploaderType.PASTE2, new Paste2Uploader());
                }
                if (MyTextUploaders.Contains(TextUploaderType.SLEXY))
                {
                    UploadText(TextUploaderType.SLEXY, new SlexyUploader());
                }
                if (MyTextUploaders.Contains(TextUploaderType.FileUploader))
                {
                    UploadFile();
                }
                if (TaskOutputs.Contains(OutputEnum.Printer))
                {
                    if (!string.IsNullOrEmpty(TempText))
                    {
                        MyWorker.ReportProgress((int)ProgressType.PrintText, TempText);
                    }
                }
            }
            this.EndTime = DateTime.Now;
        }

        private void UploadText(TextUploaderType ut, TextUploader textUploader)
        {
            if (textUploader != null)
            {
                this.DestinationName = ut.GetDescription();
                Engine.MyLogger.WriteLine("Uploading to " + this.DestinationName);

                string url = string.Empty;

                if (!string.IsNullOrEmpty(this.TempText))
                {
                    url = textUploader.UploadText(this.TempText);
                }
                else
                {
                    url = textUploader.UploadTextFile(this.LocalFilePath);
                }

                this.AddUploadResult(new UploadResult() { Host = ut.GetDescription(), URL = url });
                this.Errors = textUploader.Errors;
            }
        }

        public void UploadFile()
        {
            this.StartTime = DateTime.Now;
            Engine.MyLogger.WriteLine("Uploading File: " + this.LocalFilePath);

            List<Thread> uploaderThreads = new List<Thread>();

            if (this.MyFileUploaders.Contains(FileUploaderType.FTP))
            {
                uploaderThreads.Add(new Thread(() =>
                {
                    switch (this.Job1)
                    {
                        case JobLevel1.Text:
                            UploadToFTP(Engine.MyUploadersConfig.FTPSelectedText);
                            break;
                        case JobLevel1.Image:
                            UploadToFTP(Engine.MyUploadersConfig.FTPSelectedImage);
                            break;
                        default:
                            UploadToFTP(Engine.MyUploadersConfig.FTPSelectedFile);
                            break;
                    }
                }));
            }

            if (MyFileUploaders.Contains(FileUploaderType.Dropbox))
            {
                uploaderThreads.Add(new Thread(() =>
                {
                    string uploadPath = new NameParser { IsFolderPath = true }.Convert(Dropbox.TidyUploadPath(Engine.MyUploadersConfig.DropboxUploadPath));
                    FileUploader fileHost = new Dropbox(Engine.MyUploadersConfig.DropboxOAuthInfo, uploadPath, Engine.MyUploadersConfig.DropboxAccountInfo);
                    UploadFile(FileUploaderType.Dropbox, fileHost);
                }));
            }

            if (MyFileUploaders.Contains(FileUploaderType.SendSpace))
            {
                uploaderThreads.Add(new Thread(() =>
                 {
                     FileUploader fileHost = new SendSpace(ZKeys.SendSpaceKey);
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
                     UploadFile(FileUploaderType.SendSpace, fileHost);
                 }));
            }

            if (MyFileUploaders.Contains(FileUploaderType.RapidShare))
            {
                uploaderThreads.Add(new Thread(() =>
                {
                    UploadFile(FileUploaderType.RapidShare,
                        new RapidShare(Engine.MyUploadersConfig.RapidShareUserAccountType, Engine.MyUploadersConfig.RapidShareUsername,
                          Engine.MyUploadersConfig.RapidSharePassword));
                }));
            }

            if (MyFileUploaders.Contains(FileUploaderType.ShareCX))
            {
                uploaderThreads.Add(new Thread(() =>
                {
                    UploadFile(FileUploaderType.ShareCX, new ShareCX());
                }));
            }

            if (MyFileUploaders.Contains(FileUploaderType.CustomUploader))
            {
                uploaderThreads.Add(new Thread(() =>
                 {
                     UploadFile(FileUploaderType.CustomUploader,
                         new CustomUploader(Engine.MyUploadersConfig.CustomUploadersList[Engine.MyUploadersConfig.CustomUploaderSelected]));
                 }));
            }

            foreach (Thread t in uploaderThreads)
            {
                t.Start();
            }

            foreach (Thread t in uploaderThreads)
            {
                t.Join();
            }

            this.EndTime = DateTime.Now;
        }

        private void UploadFile(FileUploaderType ut, FileUploader fileHost)
        {
            if (fileHost != null)
            {
                this.MyWorker.ReportProgress((int)WorkerTask.ProgressType.UPDATE_PROGRESS_MAX, TaskbarProgressBarState.Indeterminate);
                this.DestinationName = ut.GetDescription();
                fileHost.ProgressChanged += UploadProgressChanged;
                UploadResult ur = new UploadResult();
                if (File.Exists(LocalFilePath))
                {
                    ur = fileHost.Upload(this.LocalFilePath);
                }
                else if (this.TempImage != null)
                {
                    MemoryStream ms = new MemoryStream();
                    TempImage.Save(ms, ImageFormat.Png);
                    FileName = new NameParser(NameParserType.EntireScreen).Convert(Engine.conf.EntireScreenPattern) + ".png";
                    ur = fileHost.Upload(ms, FileName);
                }
                ur.Host = ut.GetDescription();
                this.AddUploadResult(ur);
                this.Errors = fileHost.Errors;
            }
        }

        /// <summary>
        /// Funtion to FTP the Screenshot
        /// </summary>
        /// <returns>Retuns a List of Screenshots</returns>
        public UploadResult UploadToFTP(int FtpAccountId)
        {
            UploadResult ur = null;

            try
            {
                this.MyWorker.ReportProgress((int)WorkerTask.ProgressType.UPDATE_PROGRESS_MAX, TaskbarProgressBarState.Indeterminate);

                if (Adapter.CheckFTPAccounts(this) && File.Exists(this.LocalFilePath))
                {
                    FTPAccount acc = Engine.MyUploadersConfig.FTPAccountList[FtpAccountId];
                    this.DestinationName = string.Format("FTP - {0}", acc.Name);
                    Engine.MyLogger.WriteLine(string.Format("Uploading {0} to FTP: {1}", this.FileName, acc.Host));

                    FTPUploader fu = new FTPUploader(acc);
                    fu.ProgressChanged += new Uploader.ProgressEventHandler(UploadProgressChanged);

                    this.MyWorker.ReportProgress((int)WorkerTask.ProgressType.UPDATE_PROGRESS_MAX, TaskbarProgressBarState.Normal);

                    string url = fu.Upload(this.LocalFilePath).URL;

                    if (!string.IsNullOrEmpty(url))
                    {
                        this.AddUploadResult(new UploadResult() { Host = FileUploaderType.FTP.GetDescription(), URL = url });

                        if (CreateThumbnail())
                        {
                            double thar = (double)Engine.MyUploadersConfig.FTPThumbnailWidthLimit / (double)this.TempImage.Width;
                            using (Image img = GraphicsMgr.ChangeImageSize(this.TempImage, Engine.MyUploadersConfig.FTPThumbnailWidthLimit,
                                (int)(thar * this.TempImage.Height)))
                            {
                                StringBuilder sb = new StringBuilder(Path.GetFileNameWithoutExtension(this.LocalFilePath));
                                sb.Append(".th");
                                sb.Append(Path.GetExtension(this.LocalFilePath));
                                string thPath = Path.Combine(Path.GetDirectoryName(this.LocalFilePath), sb.ToString());
                                img.Save(thPath);
                                if (File.Exists(thPath))
                                {
                                    string thumb = fu.Upload(thPath).URL;

                                    if (!string.IsNullOrEmpty(thumb))
                                    {
                                        ur = new UploadResult();
                                        ur.Host = FileUploaderType.FTP.GetDescription();
                                        ur.ThumbnailURL = thumb;
                                        this.UploadResults.Add(ur);
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
                this.Errors.Add("FTP upload failed.\r\n" + ex.Message);
            }

            return ur;
        }

        public void UploadToSharedFolder()
        {
            if (Engine.MyUploadersConfig.LocalhostAccountList.CheckSelected(Engine.MyUploadersConfig.LocalhostSelected))
            {
                LocalhostAccount acc = Engine.MyUploadersConfig.LocalhostAccountList[Engine.MyUploadersConfig.LocalhostSelected];
                string fn = string.Empty;
                if (File.Exists(this.LocalFilePath))
                {
                    fn = Path.GetFileName(this.LocalFilePath);
                    string destFile = acc.GetLocalhostPath(fn);
                    string destDir = Path.GetDirectoryName(destFile);
                    if (!Directory.Exists(destDir))
                    {
                        Directory.CreateDirectory(destDir);
                    }
                    File.Copy(this.LocalFilePath, destFile);
                    this.UpdateLocalFilePath(destFile);
                }
                else if (TempImage != null)
                {
                    fn = new NameParser(NameParserType.EntireScreen).Convert(Engine.conf.EntireScreenPattern) + ".png";
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
                    LocalFilePath = this.LocalFilePath
                };
                this.UploadResults.Add(ur);
            }
        }

        public bool UploadToMediaWiki()
        {
            string fullFilePath = this.LocalFilePath;

            if (Engine.MyUploadersConfig.MediaWikiAccountList.CheckSelected(Engine.MyUploadersConfig.MediaWikiAccountSelected) && File.Exists(fullFilePath))
            {
                MediaWikiAccount acc = Engine.MyUploadersConfig.MediaWikiAccountList[Engine.MyUploadersConfig.MediaWikiAccountSelected];
                System.Net.IWebProxy proxy = Adapter.CheckProxySettings().GetWebProxy;
                this.DestinationName = acc.Name;
                Engine.MyLogger.WriteLine(string.Format("Uploading {0} to MediaWiki: {1}", this.FileName, acc.Url));
                MediaWikiUploader uploader = new MediaWikiUploader(new MediaWikiOptions(acc, proxy));
                this.AddUploadResult(uploader.UploadImage(this.LocalFilePath));

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
                UploadInfo uploadInfo = UploadManager.GetInfo(this.UniqueNumber);
                if (uploadInfo != null)
                {
                    uploadInfo.UploadPercentage = (int)progress.Percentage;
                    this.MyWorker.ReportProgress((int)WorkerTask.ProgressType.CHANGE_TRAY_ICON_PROGRESS, progress);
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
            if (FileSystem.IsValidLink(url) && this.MyLinkUploaders.Count > 0)
            {
                if (Engine.conf.ShortenUrlAfterUpload)
                {
                    Engine.MyLogger.WriteLine(string.Format("URL Length: {0}; Shortening after {1}", url.Length.ToString(), Engine.conf.ShortenUrlAfterUploadAfter));
                }
                return Engine.conf.TwitterEnabled ||
                    Engine.conf.ShortenUrlUsingClipboardUpload && this.Job2 == JobLevel2.UploadFromClipboard && FileSystem.IsValidLink(TempText) ||
                    Engine.conf.ShortenUrlAfterUpload && url.Length > Engine.conf.ShortenUrlAfterUploadAfter ||
                    Engine.conf.ConfLinkFormat.Contains((int)LinkFormatEnum.FULL_TINYURL);
            }

            return false;
        }

        public bool ShortenURL(string fullUrl)
        {
            UploadResult ur = new UploadResult();
            bool success = this.ShortenURL(ur, fullUrl);
            this.AddUploadResult(ur);
            return success;
        }

        public bool ShortenURL(UploadResult ur, string fullUrl)
        {
            if (!string.IsNullOrEmpty(fullUrl))
            {
                this.Job3 = WorkerTask.JobLevel3.ShortenURL;
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
            return TaskOutputs.Contains(OutputEnum.Clipboard) && TaskClipboardContent.Contains(ClipboardContentEnum.Data) && this.TempImage != null;
        }

        private bool CreateThumbnail()
        {
            return GraphicsMgr.IsValidImage(this.LocalFilePath) && this.TempImage != null &&
                (Engine.conf.ConfLinkFormat.Contains((int)LinkFormatEnum.LINKED_THUMBNAIL) ||
                  Engine.conf.ConfLinkFormat.Contains((int)LinkFormatEnum.LINKED_THUMBNAIL_WIKI) ||
                  Engine.conf.ConfLinkFormat.Contains((int)LinkFormatEnum.LinkedThumbnailHtml) ||
                  Engine.conf.ConfLinkFormat.Contains((int)LinkFormatEnum.THUMBNAIL)) &&
                (!Engine.MyUploadersConfig.FTPThumbnailCheckSize || (Engine.MyUploadersConfig.FTPThumbnailCheckSize &&
                (this.TempImage.Width > Engine.MyUploadersConfig.FTPThumbnailWidthLimit)));
        }

        #endregion Checks

        #region Descriptions

        public string GetDestinationName()
        {
            string destName = this.DestinationName;
            if (string.IsNullOrEmpty(destName))
            {
                switch (Job1)
                {
                    case JobLevel1.Image:
                        destName = this.GetActiveImageUploadersDescription();
                        break;
                    case JobLevel1.Text:
                        switch (this.Job3)
                        {
                            case WorkerTask.JobLevel3.ShortenURL:
                                destName = this.GetActiveLinkUploadersDescription();
                                break;
                            default:
                                destName = this.GetActiveTextUploadersDescription();
                                break;
                        }
                        break;
                    case JobLevel1.File:
                        destName = this.GetActiveUploadersDescription<FileUploaderType>(this.MyFileUploaders);
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
            foreach (OutputEnum ut in this.TaskOutputs)
            {
                sb.Append(ut.GetDescription());
                sb.Append(", ");
            }
            sb.Remove(sb.Length - 2, 2);
            return sb.ToString();
        }

        public string GetActiveImageUploadersDescription()
        {
            StringBuilder sb = new StringBuilder();
            foreach (ImageUploaderType ut in this.MyImageUploaders)
            {
                sb.Append(ut.GetDescription());
                sb.Append(", ");
            }
            sb.Remove(sb.Length - 2, 2);
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
            this.MyWorker.RunWorkerAsync(this);
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
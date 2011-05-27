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
using ZUploader.HelperClasses;

namespace ZScreenLib
{
    public class WorkerTask
    {
        #region Enums

        public enum TaskStatus
        {
            RetryPending,
            Started,
            CancellationPending,
            Finished
        }

        public enum JobLevel2
        {
            [Description("Entire Screen")]
            TAKE_SCREENSHOT_SCREEN,
            [Description("Active Window")]
            TAKE_SCREENSHOT_WINDOW_ACTIVE,
            [Description("Selected Window")]
            TakeScreenshotWindowSelected,
            [Description("Crop Shot")]
            TakeScreenshotCropped,
            [Description("Last Crop Shot")]
            TAKE_SCREENSHOT_LAST_CROPPED,
            [Description("Auto Capture")]
            AUTO_CAPTURE,
            [Description("Clipboard Upload")]
            UploadFromClipboard,
            [Description("Drag & Drop Window")]
            PROCESS_DRAG_N_DROP,
            [Description("Language Translator")]
            LANGUAGE_TRANSLATOR,
            [Description("Screen Color Picker")]
            SCREEN_COLOR_PICKER,
            [Description("Upload Image")]
            UPLOAD_IMAGE,
            [Description("Custom Uploader Test")]
            CustomUploaderTest,
            [Description("Webpage Capture")]
            WEBPAGE_CAPTURE,
            [Description("Freehand Crop Shot")]
            FREEHAND_CROP_SHOT
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
            CHANGE_UPLOAD_DESTINATION,
            CHANGE_TRAY_ICON_PROGRESS,
            ShowTrayWarning
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
        public bool IsImage { get; set; }
        public int UniqueNumber { get; set; }

        public Image MyImage { get; private set; }
        public string MyText { get; private set; }
        public byte[] MyFile { get; set; }
        public GoogleTranslateInfo TranslationInfo { get; private set; }

        public string FileName { get; set; }
        public string LocalFilePath { get; private set; }
        public string RemoteFilePath
        {
            get
            {
                if (LinkManager != null)
                {
                    if (!string.IsNullOrEmpty(LinkManager.UploadResult.ShortenedURL))
                    {
                        return LinkManager.UploadResult.ShortenedURL;
                    }

                    return LinkManager.UploadResult.URL;
                }
                throw new Exception("Attempted to access RemoteFilePath when LinkManager is null. Check for task.LinkManager != null before accessing RemoteFilePath.");
            }
        }

        private string DestinationName = string.Empty;

        public ImageUploaderType MyImageUploader { get; set; }
        public ImageFileManager LinkManager { get; set; }
        public TextUploaderType MyTextUploader { get; set; }
        public UrlShortenerType MyUrlShortener { get; set; }
        public FileUploaderType MyFileUploader { get; set; }

        #endregion Properties

        #region Constructors

        private WorkerTask()
        {
            this.Errors = new List<string>();
            this.Status = TaskStatus.Started;
        }

        public WorkerTask(JobLevel2 job)
            : this()
        {
            this.MyWorker = new BackgroundWorker() { WorkerReportsProgress = true };
            this.Job2 = job;
        }

        /// <summary>
        /// Constructor taking Worker and Job
        /// </summary>
        /// <param name="worker"></param>
        /// <param name="job"></param>
        public WorkerTask(BackgroundWorker worker, JobLevel2 job)
            : this()
        {
            this.MyWorker = worker;
            this.Job2 = job;

            if (Job2 == WorkerTask.JobLevel2.CustomUploaderTest)
            {
                MyImageUploader = ImageUploaderType.FileUploader;
                MyFileUploader = FileUploaderType.CustomUploader;
            }
            else
            {
                MyImageUploader = (ImageUploaderType)Engine.conf.MyImageUploader;
                MyTextUploader = (TextUploaderType)Engine.conf.MyTextUploader;
                MyFileUploader = (FileUploaderType)Engine.conf.MyFileUploader;
                MyUrlShortener = (UrlShortenerType)Engine.conf.MyURLShortener;
            }

            if (this.Job2 == JobLevel2.LANGUAGE_TRANSLATOR)
            {
                this.Job1 = JobLevel1.Text;
            }
            else if (MyImageUploader == ImageUploaderType.FileUploader)
            {
                this.Job1 = JobLevel1.File;
            }
            else
            {
                this.Job1 = JobLevel1.Image;
            }
        }

        #endregion Constructors

        #region Populating Task

        public void SetImage(Image img)
        {
            Engine.MyLogger.WriteLine(string.Format("Setting Image {0}x{1} to WorkerTask", img.Width, img.Height));
            this.MyImage = img;
            this.Job1 = JobLevel1.Image;
            if (Engine.conf.CopyImageUntilURL)
            {
                // IF (Bitmap)img.Clone() IS NOT USED THEN WE ARE GONNA GET CROSS THREAD OPERATION ERRORS! - McoreD
                this.MyWorker.ReportProgress((int)WorkerTask.ProgressType.COPY_TO_CLIPBOARD_IMAGE, (Bitmap)img.Clone());
            }
        }

        public void SetImage(string fp)
        {
            SetImage(GraphicsMgr.GetImageSafely(fp));
        }

        public void SetText(string text)
        {
            this.Job1 = JobLevel1.Text;
            this.MyText = text;

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
                // NOTE: we cannot use SaveFileDialog because we are not in the main thread, and we cant also use SaveFileDialog
                // in the main thread because the file name has to be determined outside of the main thread so the main thrad is
                // ready for multiple requests

                //SaveFileDialog dlg = new SaveFileDialog();
                //if (dlg.ShowDialog() == DialogResult.OK)
                //{
                //    filePath = dlg.FileName;
                //}

                DestOptions dialog = new DestOptions(this)
                {
                    Title = "Specify a Screenshot Name...",
                    InputText = fileName,
                    Icon = Resources.zss_main
                };
                NativeMethods.SetForegroundWindow(dialog.Handle);
                dialog.ShowDialog();
                if (dialog.DialogResult == DialogResult.OK)
                {
                    if (string.IsNullOrEmpty(this.FileName) || !this.FileName.Equals(dialog.InputText))
                    {
                        this.FileName = HelpersLib.ZAppHelper.NormalizeString(dialog.InputText);
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

        public void UpdateRemoteFilePath(UploadResult ur)
        {
            if (this.LinkManager == null)
            {
                this.LinkManager = new ImageFileManager(ur.Source);
            }
            this.LinkManager.SetUploadResult(ur);
        }

        public void UpdateLocalFilePath(string fp)
        {
            this.LocalFilePath = fp;
            this.LinkManager = new ImageFileManager(fp);
            this.UpdateRemoteFilePath(new UploadResult()
            {
                URL = this.LinkManager.GetLocalFilePathAsUri(Engine.IsPortable ? Path.Combine(Application.StartupPath, this.LocalFilePath) : this.LocalFilePath)
            });
            this.IsImage = GraphicsMgr.IsValidImage(fp);
            this.FileName = Path.GetFileName(fp);

            if (GraphicsMgr.IsValidImage(fp) && this.MyImage == null)
            {
                this.MyImage = FileSystem.ImageFromFile(fp);
            }
        }

        #endregion Populating Task

        #region Capture

        /// <summary>
        /// Function to Capture Active Window
        /// </summary>
        public void CaptureActiveWindow()
        {
            if (this.MyImage == null)
            {
                this.SetImage(Capture.CaptureActiveWindow());
            }
        }

        /// <summary>
        /// Function to Capture Entire Screen
        /// </summary>
        public void CaptureScreen()
        {
            if (this.MyImage == null)
            {
                this.SetImage(Capture.CaptureScreen(Engine.conf.ShowCursor));
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
            if (File.Exists(LocalFilePath))
            {
                foreach (Software app in Engine.conf.ActionsList)
                {
                    if (app.Enabled)
                    {
                        if (app.Name == Engine.zImageAnnotator)
                        {
                            try
                            {
                                Greenshot.Configuration.AppConfig.ConfigPath = Path.Combine(Engine.SettingsDir, "ImageEditor.bin");
                                Greenshot.ImageEditorForm editor = new Greenshot.ImageEditorForm { Icon = Resources.zss_main };
                                editor.AutoSave = Engine.conf.ImageEditorAutoSave;
                                editor.MyWorker = MyWorker;
                                editor.SetImage(MyImage);
                                editor.SetImagePath(LocalFilePath);
                                editor.ShowDialog();
                            }
                            catch (Exception ex)
                            {
                                Engine.MyLogger.WriteException(ex, "ImageEdit");
                            }
                        }
                        else if (File.Exists(app.Path))
                        {
                            if (Job1 == JobLevel1.File && app.TriggerForFiles ||
                                Job1 == JobLevel1.Image && app.TriggerForImages ||
                                Job1 == JobLevel1.Text && app.TriggerForText)
                            {
                                app.OpenFile(LocalFilePath);
                            }
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
            if (!Engine.conf.MemoryMode && this.MyImage != null)
            {
                NameParserType type;
                string pattern = string.Empty;
                if (this.Job2 == WorkerTask.JobLevel2.TAKE_SCREENSHOT_WINDOW_ACTIVE)
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
                        FileSystem.WriteImage(this);
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
        }

        public void PublishImage()
        {
            if (MyImage != null && Adapter.ImageSoftwareEnabled() && Job2 != WorkerTask.JobLevel2.UPLOAD_IMAGE)
            {
                PerformActions();
            }

            if (MyImageUploader == ImageUploaderType.FileUploader)
            {
                UploadFile();
            }
            else
            {
                UploadImage();
            }
        }

        public void UploadImage()
        {
            this.StartTime = DateTime.Now;
            if (this.MyImageUploader != ImageUploaderType.CLIPBOARD)
            {
                Engine.MyLogger.WriteLine("Uploading Image: " + this.LocalFilePath);
            }

            ImageUploader imageUploader = null;

            if (Engine.conf.TinyPicSizeCheck && this.MyImageUploader == ImageUploaderType.TINYPIC && File.Exists(this.LocalFilePath))
            {
                SizeF size = Image.FromFile(this.LocalFilePath).PhysicalDimension;
                if (size.Width > 1600 || size.Height > 1600)
                {
                    Engine.MyLogger.WriteLine("Changing from TinyPic to ImageShack due to large image size");
                    this.MyImageUploader = ImageUploaderType.IMAGESHACK;
                }
            }

            switch (this.MyImageUploader)
            {
                case ImageUploaderType.FILE:
                    string fp = this.LocalFilePath;
                    if (Engine.IsPortable)
                    {
                        fp = Path.Combine(Application.StartupPath, fp);
                        this.UpdateLocalFilePath(fp);
                    }
                    break;
                case ImageUploaderType.IMAGESHACK:
                    imageUploader = new ImageShackUploader(ZKeys.ImageShackKey, Engine.MyUploadersConfig.ImageShackRegistrationCode)
                    {
                        Public = Engine.MyUploadersConfig.ImageShackShowImagesInPublic
                    };
                    break;
                case ImageUploaderType.TINYPIC:
                    imageUploader = new TinyPicUploader(ZKeys.TinyPicID, ZKeys.TinyPicKey, Engine.MyUploadersConfig.TinyPicRegistrationCode);
                    break;
                case ImageUploaderType.IMGUR:
                    imageUploader = new Imgur(Engine.MyUploadersConfig.ImgurAccountType, ZKeys.ImgurAnonymousKey, Engine.MyUploadersConfig.ImgurOAuthInfo);
                    break;
                case ImageUploaderType.FLICKR:
                    imageUploader = new FlickrUploader(ZKeys.FlickrKey, ZKeys.FlickrSecret,
                        Engine.MyUploadersConfig.FlickrAuthInfo, Engine.MyUploadersConfig.FlickrSettings);
                    break;
                case ImageUploaderType.UPLOADSCREENSHOT:
                    imageUploader = new UploadScreenshot(ZKeys.UploadScreenshotKey);
                    break;
                case ImageUploaderType.Localhost:
                    UploadLocalhost();
                    break;
                case ImageUploaderType.MEDIAWIKI:
                    UploadMediaWiki();
                    break;
                case ImageUploaderType.PRINTER:
                    if (this.MyImage != null)
                    {
                        this.MyWorker.ReportProgress(101, (Image)this.MyImage.Clone());
                    }
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
                case ImageUploaderType.TWITSNAPS:
                    imageUploader = new TwitSnapsUploader(ZKeys.TwitsnapsKey, Adapter.TwitterGetActiveAccount());
                    break;
                case ImageUploaderType.YFROG:
                    YfrogOptions yfrogOp = new YfrogOptions(ZKeys.ImageShackKey);
                    yfrogOp.Username = Engine.MyUploadersConfig.YFrogUsername;
                    yfrogOp.Password = Engine.MyUploadersConfig.YFrogPassword;
                    yfrogOp.Source = Application.ProductName;
                    // yfrogOp.UploadType = Engine.conf.YfrogUploadMode;
                    imageUploader = new YfrogUploader(yfrogOp);
                    break;
            }

            if (imageUploader != null)
            {
                imageUploader.ProgressChanged += (x) => UploadProgressChanged(x);
                this.DestinationName = this.MyImageUploader.GetDescription();
                Engine.MyLogger.WriteLine("Initialized " + this.DestinationName);
                string fullFilePath = this.LocalFilePath;
                if (File.Exists(fullFilePath) || this.MyImage != null)
                {
                    for (int i = 0; i <= (int)Engine.conf.ErrorRetryCount; i++)
                    {
                        if (File.Exists(fullFilePath))
                        {
                            this.UpdateRemoteFilePath(imageUploader.Upload(fullFilePath));
                        }
                        else if (this.MyImage != null && this.FileName != null)
                        {
                            this.UpdateRemoteFilePath(imageUploader.Upload(this.MyImage, this.FileName.ToString()));
                        }

                        this.Errors = imageUploader.Errors;

                        if (string.IsNullOrEmpty(this.LinkManager.UploadResult.URL))
                        {
                            this.MyWorker.ReportProgress((int)ZScreenLib.WorkerTask.ProgressType.ShowTrayWarning, string.Format("Retrying... Attempt {1}", this.MyImageUploader.GetDescription(), i));
                        }
                        else
                        {
                            break;
                        }
                    }
                }
            }

            this.EndTime = DateTime.Now;

            if (Engine.conf.ImageUploadRetryOnTimeout && this.UploadDuration > (int)Engine.conf.UploadDurationLimit)
            {
                if (this.MyImageUploader == ImageUploaderType.IMAGESHACK)
                {
                    Engine.conf.MyImageUploader = (int)ImageUploaderType.TINYPIC;
                }
                else if (this.MyImageUploader == ImageUploaderType.TINYPIC)
                {
                    Engine.conf.MyImageUploader = (int)ImageUploaderType.IMAGESHACK;
                }
                this.MyWorker.ReportProgress((int)WorkerTask.ProgressType.CHANGE_UPLOAD_DESTINATION);
            }

            if (this.LinkManager != null)
            {
                FlashIcon(this);
            }
        }

        public void UploadText()
        {
            this.StartTime = DateTime.Now;
            this.MyWorker.ReportProgress((int)WorkerTask.ProgressType.UPDATE_PROGRESS_MAX, TaskbarProgressBarState.Indeterminate);

            if (this.ShouldShortenURL(this.MyText))
            {
                // Need this for shortening URL using Clipboard Upload http://imgur.com/DzBJQ.png
                this.ShortenURL(this.MyText);
            }
            else
            {
                if (this.MyTextUploader == TextUploaderType.FileUploader)
                {
                    UploadFile();
                }
                else
                {
                    TextUploader textUploader = null;

                    switch (this.MyTextUploader)
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
                    }

                    if (textUploader != null)
                    {
                        this.DestinationName = this.MyTextUploader.GetDescription();
                        Engine.MyLogger.WriteLine("Uploading to " + this.DestinationName);

                        string url = string.Empty;

                        if (!string.IsNullOrEmpty(this.MyText))
                        {
                            url = textUploader.UploadText(this.MyText);
                        }
                        else
                        {
                            url = textUploader.UploadTextFile(this.LocalFilePath);
                        }

                        this.UpdateRemoteFilePath(new UploadResult() { URL = url });
                        this.Errors = textUploader.Errors;
                    }
                }
            }
            this.EndTime = DateTime.Now;
        }

        public void UploadFile()
        {
            this.StartTime = DateTime.Now;
            Engine.MyLogger.WriteLine("Uploading File: " + this.LocalFilePath);

            FileUploader fileHost = null;
            switch (this.MyFileUploader)
            {
                case FileUploaderType.FTP:
                    switch (this.Job1)
                    {
                        case JobLevel1.Text:
                            UploadFTP(Engine.MyUploadersConfig.FTPSelectedText);
                            break;
                        case JobLevel1.Image:
                            UploadFTP(Engine.MyUploadersConfig.FTPSelectedImage);
                            break;
                        default:
                            UploadFTP(Engine.MyUploadersConfig.FTPSelectedFile);
                            break;
                    }
                    break;
                case FileUploaderType.Dropbox:
                    string uploadPath = new NameParser { IsFolderPath = true }.Convert(Dropbox.TidyUploadPath(Engine.MyUploadersConfig.DropboxUploadPath));
                    fileHost = new Dropbox(Engine.MyUploadersConfig.DropboxOAuthInfo, uploadPath, Engine.MyUploadersConfig.DropboxAccountInfo);
                    break;
                case FileUploaderType.SendSpace:
                    fileHost = new SendSpace(ZKeys.SendSpaceKey);
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
                    fileHost = new RapidShare(new RapidShareOptions()
                    {
                        AccountType = Engine.MyUploadersConfig.RapidShareAccountType,
                        PremiumUsername = Engine.MyUploadersConfig.RapidSharePremiumUserName,
                        Password = Engine.MyUploadersConfig.RapidSharePassword,
                        CollectorsID = Engine.MyUploadersConfig.RapidShareCollectorsID
                    });
                    break;
                case FileUploaderType.ShareCX:
                    fileHost = new ShareCX();
                    break;
                case FileUploaderType.CustomUploader:
                    if (Engine.MyUploadersConfig.CustomUploadersList.CheckSelected(Engine.MyUploadersConfig.CustomUploaderSelected))
                    {
                        fileHost = new CustomUploader(Engine.MyUploadersConfig.CustomUploadersList[Engine.MyUploadersConfig.CustomUploaderSelected]);
                    }
                    break;
            }

            if (fileHost != null)
            {
                this.MyWorker.ReportProgress((int)WorkerTask.ProgressType.UPDATE_PROGRESS_MAX, TaskbarProgressBarState.Indeterminate);
                this.DestinationName = this.MyFileUploader.GetDescription();
                fileHost.ProgressChanged += UploadProgressChanged;
                UploadResult ur = fileHost.Upload(this.LocalFilePath);
                if (ur != null)
                {
                    this.UpdateRemoteFilePath(ur);
                }
                this.Errors = fileHost.Errors;
            }

            this.EndTime = DateTime.Now;
        }

        /// <summary>
        /// Funtion to FTP the Screenshot
        /// </summary>
        /// <returns>Retuns a List of Screenshots</returns>
        public bool UploadFTP(int FtpAccountId)
        {
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
                        this.UpdateRemoteFilePath(new UploadResult() { URL = url });

                        if (CreateThumbnail())
                        {
                            double thar = (double)Engine.MyUploadersConfig.FTPThumbnailWidthLimit / (double)this.MyImage.Width;
                            using (Image img = GraphicsMgr.ChangeImageSize(this.MyImage, Engine.MyUploadersConfig.FTPThumbnailWidthLimit,
                                (int)(thar * this.MyImage.Height)))
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
                                        this.LinkManager.UploadResult.ThumbnailURL = thumb;
                                    }
                                }
                            }
                        }

                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                Engine.MyLogger.WriteException(ex, "Error while uploading to FTP Server");
                this.Errors.Add("FTP upload failed.\r\n" + ex.Message);
            }

            return false;
        }

        public void UploadLocalhost()
        {
            if (Engine.MyUploadersConfig.LocalhostAccountList.CheckSelected(Engine.MyUploadersConfig.LocalhostSelected) && File.Exists(this.LocalFilePath))
            {
                LocalhostAccount acc = Engine.MyUploadersConfig.LocalhostAccountList[Engine.MyUploadersConfig.LocalhostSelected];
                string fn = Path.GetFileName(this.LocalFilePath);
                string destFile = acc.GetLocalhostPath(fn);
                string destDir = Path.GetDirectoryName(destFile);
                if (!Directory.Exists(destDir))
                {
                    Directory.CreateDirectory(destDir);
                }
                File.Move(this.LocalFilePath, destFile);
                this.UpdateLocalFilePath(destFile);
                this.LinkManager.UploadResult.URL = acc.GetUriPath(fn);
            }
        }

        public bool UploadMediaWiki()
        {
            string fullFilePath = this.LocalFilePath;

            if (Engine.MyUploadersConfig.MediaWikiAccountList.CheckSelected(Engine.MyUploadersConfig.MediaWikiAccountSelected) && File.Exists(fullFilePath))
            {
                MediaWikiAccount acc = Engine.MyUploadersConfig.MediaWikiAccountList[Engine.MyUploadersConfig.MediaWikiAccountSelected];
                System.Net.IWebProxy proxy = Adapter.CheckProxySettings().GetWebProxy;
                this.DestinationName = acc.Name;
                Engine.MyLogger.WriteLine(string.Format("Uploading {0} to MediaWiki: {1}", this.FileName, acc.Url));
                MediaWikiUploader uploader = new MediaWikiUploader(new MediaWikiOptions(acc, proxy));
                this.UpdateRemoteFilePath(uploader.UploadImage(this.LocalFilePath));
                // RemoteFilePath = acc.Url + "/index.php?title=File:" + (Path.GetFileName(LocalFilePath));

                return true;
            }
            return false;
        }

        private void FlashIcon(WorkerTask t)
        {
            for (int i = 0; i < (int)Engine.conf.FlashTrayCount; i++)
            {
                t.MyWorker.ReportProgress((int)WorkerTask.ProgressType.FLASH_ICON, Resources.zss_uploaded);
                Thread.Sleep(250);
                t.MyWorker.ReportProgress((int)WorkerTask.ProgressType.FLASH_ICON, Resources.zss_green);
                Thread.Sleep(250);
            }
            t.MyWorker.ReportProgress((int)WorkerTask.ProgressType.FLASH_ICON, Resources.zss_tray);
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

        #region Checks

        public bool JobIsImageToFile()
        {
            return Job1 == JobLevel1.Image && MyImageUploader == ImageUploaderType.FILE;
        }

        /// <summary>
        /// Function to test if the URL should or could shorten
        /// </summary>
        /// <param name="url">Long URL</param>
        /// <returns>true/false whether URL should or could shorten</returns>
        public bool ShouldShortenURL(string url)
        {
            if (FileSystem.IsValidLink(url) && this.MyUrlShortener != UrlShortenerType.NONE)
            {
                if (Engine.conf.ShortenUrlAfterUpload)
                {
                    Engine.MyLogger.WriteLine(string.Format("URL Length: {0}; Shortening after {1}", url.Length.ToString(), Engine.conf.ShortenUrlAfterUploadAfter));
                }
                return Engine.conf.TwitterEnabled ||
                    Engine.conf.ShortenUrlUsingClipboardUpload && this.Job2 == JobLevel2.UploadFromClipboard && FileSystem.IsValidLink(MyText) ||
                    Engine.conf.ShortenUrlAfterUpload && url.Length > Engine.conf.ShortenUrlAfterUploadAfter ||
                    Engine.conf.MyClipboardUriMode == (int)ClipboardUriType.FULL_TINYURL;
            }

            return false;
        }

        public bool ShortenURL(string fullUrl)
        {
            if (!string.IsNullOrEmpty(fullUrl))
            {
                this.Job3 = WorkerTask.JobLevel3.ShortenURL;
                URLShortener us = null;

                switch (MyUrlShortener)
                {
                    case UrlShortenerType.BITLY:
                        us = new BitlyURLShortener(ZKeys.BitlyLogin, ZKeys.BitlyKey);
                        break;
                    case UrlShortenerType.Google:
                        us = new GoogleURLShortener(ZKeys.GoogleURLShortenerKey);
                        break;
                    case UrlShortenerType.ISGD:
                        us = new IsgdURLShortener();
                        break;
                    case UrlShortenerType.Jmp:
                        us = new JmpURLShortener(ZKeys.BitlyLogin, ZKeys.BitlyKey);
                        break;
                    /*case UrlShortenerType.THREELY:
                        us = new ThreelyURLShortener(Engine.ThreelyKey);
                        break;*/
                    case UrlShortenerType.TINYURL:
                        us = new TinyURLShortener();
                        break;
                    case UrlShortenerType.TURL:
                        us = new TurlURLShortener();
                        break;
                }

                if (us != null)
                {
                    string shortenUrl = us.ShortenURL(fullUrl);

                    if (!string.IsNullOrEmpty(shortenUrl))
                    {
                        Engine.MyLogger.WriteLine(string.Format("Shortened URL: {0}", shortenUrl));
                        UpdateRemoteFilePath(new UploadResult() { URL = fullUrl, ShortenedURL = shortenUrl });
                        return true;
                    }
                }
            }

            return false;
        }

        public bool JobIsImageToClipboard()
        {
            return Job1 == JobLevel1.Image && MyImageUploader == ImageUploaderType.CLIPBOARD && this.MyImage != null;
        }

        private bool CreateThumbnail()
        {
            return GraphicsMgr.IsValidImage(this.LocalFilePath) && this.MyImage != null &&
                ((ClipboardUriType)Engine.conf.MyClipboardUriMode == ClipboardUriType.LINKED_THUMBNAIL ||
                 (ClipboardUriType)Engine.conf.MyClipboardUriMode == ClipboardUriType.LINKED_THUMBNAIL_WIKI ||
                 (ClipboardUriType)Engine.conf.MyClipboardUriMode == ClipboardUriType.LinkedThumbnailHtml ||
                 (ClipboardUriType)Engine.conf.MyClipboardUriMode == ClipboardUriType.THUMBNAIL) &&
                (!Engine.MyUploadersConfig.FTPThumbnailCheckSize || (Engine.MyUploadersConfig.FTPThumbnailCheckSize &&
                (this.MyImage.Width > Engine.MyUploadersConfig.FTPThumbnailWidthLimit)));
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
                        destName = this.MyImageUploader.GetDescription();
                        break;
                    case JobLevel1.Text:
                        switch (this.Job3)
                        {
                            case WorkerTask.JobLevel3.ShortenURL:
                                destName = this.MyUrlShortener.GetDescription();
                                break;
                            default:
                                destName = this.MyTextUploader.GetDescription();
                                break;
                        }
                        break;
                    case JobLevel1.File:
                        destName = this.MyFileUploader.GetDescription();
                        break;
                }
            }
            return destName;
        }

        public string GetDescription()
        {
            if (this.Job2 == JobLevel2.UploadFromClipboard && this.Job3 != JobLevel3.None)
            {
                return string.Format("{0}: {1} ({2})", this.Job2.GetDescription(), this.Job3.GetDescription(), this.GetDestinationName());
            }
            else
            {
                return string.Format("{0} ({1})", this.Job2.GetDescription(), this.GetDestinationName());
            }
        }

        public string ToErrorString()
        {
            return string.Join("\r\n", Errors.ToArray());
        }

        public override string ToString()
        {
            StringBuilder sbDebug = new StringBuilder();
            sbDebug.AppendLine(string.Format("Image Uploader: {0}", MyImageUploader));
            // sbDebug.AppendLine(string.Format(" Text Uploader: {0}", MyTextUploader));
            sbDebug.AppendLine(string.Format(" File Uploader: {0}", MyFileUploader.GetDescription()));
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

        public HistoryItem GenerateHistoryItem()
        {
            HistoryLib.HistoryItem hi = new HistoryLib.HistoryItem();
            hi.DateTimeUtc = this.EndTime;
            if (this.LinkManager != null)
            {
                hi.DeletionURL = this.LinkManager.UploadResult.DeletionURL;
                hi.ThumbnailURL = this.LinkManager.UploadResult.ThumbnailURL;
                hi.ShortenedURL = this.LinkManager.UploadResult.ShortenedURL;
                hi.URL = this.LinkManager.UploadResult.URL;
            }
            hi.Filename = this.FileName;
            hi.Filepath = this.LocalFilePath;
            hi.Host = this.GetDestinationName();
            hi.Type = this.Job1.GetDescription();

            return hi;
        }

        #endregion Helper Methods
    }
}
#region License Information (GPL v2)
/*
    ZScreen - A program that allows you to upload screenshots in one keystroke.
    Copyright (C) 2008-2009  Brandon Zimmerman

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
#endregion

using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Microsoft.WindowsAPICodePack.Taskbar;
using UploadersLib;
using UploadersLib.FileUploaders;
using UploadersLib.Helpers;
using UploadersLib.ImageUploaders;
using ZScreenLib.Properties;

namespace ZScreenLib
{
    public class TaskManager
    {
        private WorkerTask mTask;
        public static bool mSetHotkeys, mTakingScreenShot, bAutoScreenshotsOpened, bDropWindowOpened, bQuickActionsOpened, bQuickOptionsOpened;

        public TaskManager(ref WorkerTask task)
        {
            this.mTask = task;
        }

        #region Image Tasks Manager

        public void CaptureActiveWindow()
        {
            try
            {
                mTask.CaptureActiveWindow();
                WriteImage();
                PublishData();
            }
            catch (ArgumentOutOfRangeException aor)
            {
                mTask.Errors.Add("Invalid FTP Account Selection");
                FileSystem.AppendDebug(aor.ToString());
            }
            catch (Exception ex)
            {
                FileSystem.AppendDebug(ex.ToString());
                if (Engine.conf.CaptureEntireScreenOnError)
                {
                    CaptureRegionOrWindow();
                }
            }
        }

        public string CaptureRegionOrWindow()
        {
            mTakingScreenShot = true;
            string filePath = "";

            try
            {
                using (Image imgSS = User32.CaptureScreen(Engine.conf.ShowCursor))
                {
                    if (mTask.Job == WorkerTask.Jobs.TAKE_SCREENSHOT_LAST_CROPPED && !Engine.LastRegion.IsEmpty)
                    {
                        mTask.SetImage(GraphicsMgr.CropImage(imgSS, Engine.LastRegion));
                    }
                    else
                    {
                        using (Crop c = new Crop(imgSS, mTask.Job == WorkerTask.Jobs.TakeScreenshotWindowSelected))
                        {
                            if (c.ShowDialog() == DialogResult.OK)
                            {
                                if (mTask.Job == WorkerTask.Jobs.TakeScreenshotCropped && !Engine.LastRegion.IsEmpty)
                                {
                                    mTask.SetImage(GraphicsMgr.CropImage(imgSS, Engine.LastRegion));
                                }
                                else if (mTask.Job == WorkerTask.Jobs.TakeScreenshotWindowSelected && !Engine.LastCapture.IsEmpty)
                                {
                                    mTask.SetImage(GraphicsMgr.CropImage(imgSS, Engine.LastCapture));
                                }
                            }
                            else
                            {
                                mTask.Completed = true;
                            }
                        }
                    }
                }

                mTakingScreenShot = false;

                if (mTask.MyImage != null)
                {
                    WriteImage();
                    PublishData();
                }
            }
            catch (Exception ex)
            {
                FileSystem.AppendDebug(ex.ToString());
                mTask.Errors.Add(ex.Message);
                if (Engine.conf.CaptureEntireScreenOnError)
                {
                    CaptureScreen();
                }
            }
            finally
            {
                mTask.MyWorker.ReportProgress((int)WorkerTask.ProgressType.UpdateCropMode);
                mTakingScreenShot = false;
            }

            return filePath;
        }

        public void CaptureScreen()
        {
            mTask.CaptureScreen();
            WriteImage();
            PublishData();
        }

        /// <summary>
        /// Writes MyImage object in a WorkerTask into a file
        /// </summary>
        /// <param name="t">WorkerTask</param>
        public void WriteImage()
        {
            if (mTask.MyImage != null)
            {
                NameParserType type;
                if (mTask.Job == WorkerTask.Jobs.TAKE_SCREENSHOT_WINDOW_ACTIVE)
                {
                    type = NameParserType.ActiveWindow;
                }
                else
                {
                    type = NameParserType.EntireScreen;
                }

                mTask.SetFilePathFromPattern(NameParser.Convert(type));
                mTask.UpdateLocalFilePath(FileSystem.SaveImage(mTask.MyImage, mTask.LocalFilePath));
                if (!File.Exists(mTask.LocalFilePath))
                {
                    mTask.Errors.Add(string.Format("{0} does not exist", mTask.LocalFilePath));
                }
            }
        }

        /// <summary>
        /// Function to edit Image (Screenshot or Picture) in an Image Editor and Upload
        /// </summary>
        /// <param name="task"></param>
        public void PublishData()
        {
            if (Engine.conf.ShowAdvancedOptionsAfterCrop)
            {
                //TODO: Implement Dialog to choose action

                if (mTask.JobCategory == JobCategoryType.BINARY)
                {
                    UploadFile();
                }
                else
                {
                    PublishImage();
                }
            }
            else
            {
                if (mTask.JobCategory == JobCategoryType.BINARY)
                {
                    UploadFile();
                }
                else
                {
                    PublishImage();
                }
            }
        }

        #endregion

        public void PublishImage()
        {
            if (mTask.MyImage != null && Adapter.ImageSoftwareEnabled() && mTask.Job != WorkerTask.Jobs.UPLOAD_IMAGE)
            {
                ImageEdit();
            }

            if (Engine.conf.PreferFileUploaderForImages)
            {
                UploadFile();
            }
            else
            {
                UploadImage();
            }
        }

        public void UploadFile()
        {
            mTask.StartTime = DateTime.Now;
            FileSystem.AppendDebug("Uploading File: " + mTask.LocalFilePath);
            FileUploader fileHost = null;
            switch (mTask.MyFileUploader)
            {
                case FileUploaderType.FTP:
                    UploadFTP();
                    break;
                case FileUploaderType.SendSpace:
                    fileHost = new SendSpace();
                    switch (Engine.conf.SendSpaceAccountType)
                    {
                        case AcctType.Anonymous:
                            fileHost.Errors = SendSpaceManager.PrepareUploadInfo(null, null);
                            break;
                        case AcctType.User:
                            fileHost.Errors = SendSpaceManager.PrepareUploadInfo(Engine.conf.SendSpaceUserName, Engine.conf.SendSpacePassword);
                            break;
                    }
                    break;
                case FileUploaderType.RapidShare:
                    fileHost = new RapidShare(new RapidShareOptions()
                    {
                        AccountType = Engine.conf.RapidShareAccountType,
                        PremiumUsername = Engine.conf.RapidSharePremiumUserName,
                        Password = Engine.conf.RapidSharePassword,
                        CollectorsID = Engine.conf.RapidShareCollectorsID
                    });
                    break;
            }

            if (fileHost != null)
            {
                mTask.MyWorker.ReportProgress((int)WorkerTask.ProgressType.UPDATE_PROGRESS_MAX, TaskbarProgressBarState.Indeterminate);
                mTask.DestinationName = fileHost.Name;
                fileHost.ProgressChanged += UploadProgressChanged;
                string url = fileHost.Upload(mTask.LocalFilePath);
                mTask.Errors = fileHost.Errors;
                mTask.RemoteFilePath = url;
            }

            mTask.EndTime = DateTime.Now;
        }

        public void UploadImage()
        {
            mTask.StartTime = DateTime.Now;
            FileSystem.AppendDebug("Uploading Image: " + mTask.LocalFilePath);

            ImageUploader imageUploader = null;

            if (Engine.conf.TinyPicSizeCheck && mTask.MyImageUploader == ImageDestType.TINYPIC && File.Exists(mTask.LocalFilePath))
            {
                SizeF size = Image.FromFile(mTask.LocalFilePath).PhysicalDimension;
                if (size.Width > 1600 || size.Height > 1600)
                {
                    FileSystem.AppendDebug("Changing from TinyPic to ImageShack due to large image size");
                    mTask.MyImageUploader = ImageDestType.IMAGESHACK;
                }
            }

            switch (mTask.MyImageUploader)
            {
                case ImageDestType.CLIPBOARD:
                    mTask.MyWorker.ReportProgress((int)WorkerTask.ProgressType.COPY_TO_CLIPBOARD_IMAGE, mTask.LocalFilePath);
                    break;
                case ImageDestType.CUSTOM_UPLOADER:
                    if (Adapter.CheckList(Engine.conf.ImageUploadersList, Engine.conf.ImageUploaderSelected))
                    {
                        imageUploader = new CustomUploader(Engine.conf.ImageUploadersList[Engine.conf.ImageUploaderSelected]);
                    }
                    break;
                case ImageDestType.DEKIWIKI:
                    UploadDekiWiki();
                    break;
                case ImageDestType.FILE:
                    mTask.MyWorker.ReportProgress((int)WorkerTask.ProgressType.COPY_TO_CLIPBOARD_URL, mTask.LocalFilePath);
                    break;
                case ImageDestType.FLICKR:
                    imageUploader = new FlickrUploader(Engine.conf.FlickrAuthInfo, Engine.conf.FlickrSettings);
                    break;
                case ImageDestType.FTP:
                    UploadFTP();
                    break;
                case ImageDestType.IMAGEBAM:
                    ImageBamUploaderOptions imageBamOptions = new ImageBamUploaderOptions(Engine.conf.ImageBamApiKey, Engine.conf.ImageBamSecret,
                        Adapter.GetImageBamGalleryActive()) { NSFW = Engine.conf.ImageBamContentNSFW };
                    imageUploader = new ImageBamUploader(imageBamOptions);
                    break;
                case ImageDestType.IMAGEBIN:
                    imageUploader = new ImageBin();
                    break;
                case ImageDestType.IMAGESHACK:
                    imageUploader = new ImageShackUploader(Engine.IMAGESHACK_KEY, Engine.conf.ImageShackRegistrationCode, Engine.conf.UploadMode);
                    ((ImageShackUploader)imageUploader).Public = Engine.conf.ImageShackShowImagesInPublic;
                    break;
                case ImageDestType.PRINTER:
                    if (mTask.MyImage != null)
                    {
                        mTask.MyWorker.ReportProgress(101, (Image)mTask.MyImage.Clone());
                    }
                    break;
                case ImageDestType.TINYPIC:
                    imageUploader = new TinyPicUploader(Engine.TINYPIC_ID, Engine.TINYPIC_KEY, Engine.conf.UploadMode);
                    ((TinyPicUploader)imageUploader).Shuk = Engine.conf.TinyPicShuk;
                    break;
                case ImageDestType.TWITPIC:
                    TwitPicOptions twitpicOpt = new TwitPicOptions();
                    twitpicOpt.UserName = Adapter.TwitterGetActiveAcct().UserName;
                    twitpicOpt.Password = Adapter.TwitterGetActiveAcct().Password;
                    // twitpicOpt.TwitPicUploadType = Engine.conf.TwitPicUploadMode;
                    twitpicOpt.TwitPicThumbnailMode = Engine.conf.TwitPicThumbnailMode;
                    twitpicOpt.ShowFull = Engine.conf.TwitPicShowFull;
                    imageUploader = new TwitPicUploader(twitpicOpt);
                    break;
                case ImageDestType.TWITSNAPS:
                    TwitSnapsOptions twitsnapsOpt = new TwitSnapsOptions();
                    twitsnapsOpt.UserName = Adapter.TwitterGetActiveAcct().UserName;
                    twitsnapsOpt.Password = Adapter.TwitterGetActiveAcct().Password;
                    imageUploader = new TwitSnapsUploader(twitsnapsOpt);
                    break;
                case ImageDestType.YFROG:
                    YfrogOptions yfrogOp = new YfrogOptions(Engine.IMAGESHACK_KEY);
                    yfrogOp.UserName = Adapter.TwitterGetActiveAcct().UserName;
                    yfrogOp.Password = Adapter.TwitterGetActiveAcct().Password;
                    yfrogOp.Source = Application.ProductName;
                    // yfrogOp.UploadType = Engine.conf.YfrogUploadMode;
                    imageUploader = new YfrogUploader(yfrogOp);
                    break;
            }

            //imageUploader.ProgressChanged += new ImageUploader.ProgressEventHandler(UploadProgressChanged);

            if (imageUploader != null)
            {
                FileSystem.AppendDebug("Initialized " + imageUploader.Name);
                mTask.MyWorker.ReportProgress((int)WorkerTask.ProgressType.UPDATE_PROGRESS_MAX, TaskbarProgressBarState.Indeterminate);
                mTask.DestinationName = imageUploader.Name;
                string fullFilePath = mTask.LocalFilePath;
                if (File.Exists(fullFilePath) || mTask.MyImage != null)
                {
                    for (int i = 1; i <= (int)Engine.conf.ErrorRetryCount && (mTask.ImageManager == null ||
                        (mTask.ImageManager != null && mTask.ImageManager.ImageFileList.Count < 1)); i++)
                    {
                        if (File.Exists(fullFilePath))
                        {
                            mTask.ImageManager = imageUploader.UploadImage(fullFilePath);
                        }
                        else if (mTask.MyImage != null && mTask.FileName != null)
                        {
                            mTask.ImageManager = imageUploader.UploadImage(mTask.MyImage, mTask.FileName.ToString());
                        }
                        mTask.Errors = imageUploader.Errors;

                        if (mTask.ImageManager.ImageFileList.Count == 0)
                        {
                            mTask.MyWorker.ReportProgress((int)ZScreenLib.WorkerTask.ProgressType.ShowTrayWarning, string.Format("Retrying... Attempt {1}", mTask.MyImageUploader.GetDescription(), i));
                        }
                        else
                        {
                            break;
                        }
                    }
                }
            }

            this.SetRemoteFilePath();
            mTask.EndTime = DateTime.Now;

            if (Engine.conf.ImageUploadRetryOnTimeout && mTask.UploadDuration > (int)Engine.conf.UploadDurationLimit)
            {
                if (mTask.MyImageUploader == ImageDestType.IMAGESHACK)
                {
                    Engine.conf.ImageUploaderType = ImageDestType.TINYPIC;
                }
                else if (mTask.MyImageUploader == ImageDestType.TINYPIC)
                {
                    Engine.conf.ImageUploaderType = ImageDestType.IMAGESHACK;
                }
                mTask.MyWorker.ReportProgress((int)WorkerTask.ProgressType.CHANGE_UPLOAD_DESTINATION);
            }

            if (mTask.ImageManager != null)
            {
                FlashIcon(mTask);
            }
        }

        private void SetRemoteFilePath()
        {
            if (mTask.ImageManager != null)
            {
                string url = mTask.ImageManager.GetFullImageUrl();
                mTask.RemoteFilePath = url;
                FileSystem.AppendDebug("URL: " + mTask.RemoteFilePath);
            }
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
        }

        /// <summary>
        /// Funtion to FTP the Screenshot
        /// </summary>
        /// <returns>Retuns a List of Screenshots</returns>
        public bool UploadFTP()
        {
            try
            {
                mTask.MyWorker.ReportProgress((int)WorkerTask.ProgressType.UPDATE_PROGRESS_MAX, TaskbarProgressBarState.Indeterminate);

                if (Adapter.CheckFTPAccounts(ref mTask) && File.Exists(mTask.LocalFilePath))
                {
                    FTPAccount acc = Engine.conf.FTPAccountList[Engine.conf.FTPSelected];
                    mTask.DestinationName = string.Format("FTP - {0}", acc.Name);
                    FileSystem.AppendDebug(string.Format("Uploading {0} to FTP: {1}", mTask.FileName, acc.Host));

                    FTPUploader fu = new FTPUploader(acc);
                    fu.ProgressChanged += new Uploader.ProgressEventHandler(UploadProgressChanged);

                    mTask.MyWorker.ReportProgress((int)WorkerTask.ProgressType.UPDATE_PROGRESS_MAX, TaskbarProgressBarState.Normal);

                    string url = fu.Upload(mTask.LocalFilePath);

                    if (!string.IsNullOrEmpty(url))
                    {
                        mTask.RemoteFilePath = url;
                        mTask.ImageManager = new ImageFileManager();
                        mTask.ImageManager.Add(url, ImageFile.ImageType.FULLIMAGE);

                        if (IsThumbnail())
                        {
                            using (Image img = GraphicsMgr.ChangeImageSize(mTask.MyImage, Engine.conf.FTPThumbnailWidth, Engine.conf.FTPThumbnailHeight))
                            {
                                StringBuilder sb = new StringBuilder(Path.GetFileNameWithoutExtension(mTask.LocalFilePath));
                                sb.Append(".th");
                                sb.Append(Path.GetExtension(mTask.LocalFilePath));
                                string thPath = Path.Combine(Path.GetDirectoryName(mTask.LocalFilePath), sb.ToString());
                                img.Save(thPath);
                                if (File.Exists(thPath))
                                {
                                    string thumb = fu.Upload(thPath);

                                    if (!string.IsNullOrEmpty(thumb))
                                    {
                                        mTask.ImageManager.Add(thumb, ImageFile.ImageType.THUMBNAIL);
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
                Console.WriteLine(ex.ToString());
                mTask.Errors.Add("FTP upload failed.\r\n" + ex.Message);
            }

            return false;
        }

        private bool IsThumbnail()
        {
            return GraphicsMgr.IsValidImage(mTask.LocalFilePath) && mTask.MyImage != null &&
                (Engine.conf.ClipboardUriMode != ClipboardUriType.FULL || Engine.conf.FTPCreateThumbnail) &&
                (!Engine.conf.FTPThumbnailCheckSize || (Engine.conf.FTPThumbnailCheckSize && (mTask.MyImage.Width > Engine.conf.FTPThumbnailWidth ||
                mTask.MyImage.Height > Engine.conf.FTPThumbnailHeight)));
        }

        private void UploadProgressChanged(int progress)
        {
            if (Engine.conf.ShowTrayUploadProgress)
            {
                UploadInfo uploadInfo = UploadManager.GetInfo(mTask.UniqueNumber);
                if (uploadInfo != null)
                {
                    uploadInfo.UploadPercentage = (int)progress;
                    mTask.MyWorker.ReportProgress((int)WorkerTask.ProgressType.CHANGE_TRAY_ICON_PROGRESS, progress);
                }
            }
        }

        public bool UploadDekiWiki()
        {
            try
            {
                string fullFilePath = mTask.LocalFilePath;

                if (Adapter.CheckDekiWikiAccounts(ref mTask) && File.Exists(fullFilePath))
                {
                    DekiWikiAccount acc = Engine.conf.DekiWikiAccountList[Engine.conf.DekiWikiSelected];

                    System.Net.IWebProxy proxy = Adapter.CheckProxySettings().GetWebProxy;

                    if (DekiWiki.savePath == null || DekiWiki.savePath.Length == 0 || Engine.conf.DekiWikiForcePath == true)
                    {
                        DekiWikiPath diag = new DekiWikiPath(new DekiWikiOptions(acc, proxy));
                        diag.history = acc.History;
                        diag.ShowDialog();

                        if (diag.DialogResult != DialogResult.OK)
                        {
                            throw new Exception("User canceled the operation.");
                        }

                        DekiWiki.savePath = diag.path;
                    }

                    mTask.DestinationName = acc.Name;

                    FileSystem.AppendDebug(string.Format("Uploading {0} to Mindtouch: {1}", mTask.FileName, acc.Url));

                    DekiWikiUploader uploader = new DekiWikiUploader(new DekiWikiOptions(acc, proxy));
                    mTask.ImageManager = uploader.UploadImage(mTask.LocalFilePath);
                    mTask.RemoteFilePath = acc.getUriPath(Path.GetFileName(mTask.LocalFilePath));

                    DekiWiki connector = new DekiWiki(new DekiWikiOptions(acc, proxy));
                    connector.UpdateHistory();

                    return true;
                }
            }
            catch (Exception ex)
            {
                mTask.Errors.Add("Mindtouch upload failed.\r\n" + ex.Message);
            }

            return false;
        }

        public void UploadText()
        {
            mTask.StartTime = DateTime.Now;
            mTask.MyWorker.ReportProgress((int)WorkerTask.ProgressType.UPDATE_PROGRESS_MAX, TaskbarProgressBarState.Indeterminate);

            if (Engine.conf.PreferFileUploaderForText)
            {
                UploadFile();
            }
            else
            {
                TextUploader textUploader = (TextUploader)mTask.MyTextUploader;
                string url = "";
                if (mTask.MyText != null)
                {
                    url = textUploader.UploadText(mTask.MyText);
                }
                else
                {
                    url = textUploader.UploadTextFromFile(mTask.LocalFilePath);
                    mTask.MyText = TextInfo.FromFile(mTask.LocalFilePath);
                }
                mTask.RemoteFilePath = url;
                mTask.Errors = textUploader.Errors;
                mTask.EndTime = DateTime.Now;
            }
        }

        public void TextEdit()
        {
            if (File.Exists(mTask.LocalFilePath))
            {
                Process p = new Process();
                ProcessStartInfo psi = new ProcessStartInfo(Engine.conf.TextEditorActive.Path)
                {
                    Arguments = string.Format("{0}{1}{0}", "\"", mTask.LocalFilePath)
                };
                p.StartInfo = psi;
                p.Start();
                // Wait till user quits the ScreenshotEditApp
                p.WaitForExit();
            }
        }

        /// <summary>
        /// Edit Image in selected Image Editor
        /// </summary>
        public void ImageEdit()
        {
            if (File.Exists(mTask.LocalFilePath))
            {
                Process p = new Process();
                Software app = Engine.conf.ImageEditor;
                if (app != null)
                {
                    if (app.Name == Engine.ZSCREEN_IMAGE_EDITOR)
                    {
                        try
                        {
                            Greenshot.Configuration.AppConfig.ConfigPath = Path.Combine(Engine.SettingsDir, "ImageEditor.bin");
                            Greenshot.ImageEditorForm editor = new Greenshot.ImageEditorForm { Icon = Resources.zss_main };
                            editor.AutoSave = Engine.conf.ImageEditorAutoSave;
                            editor.MyWorker = mTask.MyWorker;
                            editor.SetImage(mTask.MyImage);
                            editor.SetImagePath(mTask.LocalFilePath);
                            editor.ShowDialog();
                        }
                        catch (Exception ex)
                        {
                            FileSystem.AppendDebug(ex.ToString());
                        }
                    }
                    else if (File.Exists(app.Path))
                    {
                        ProcessStartInfo psi = new ProcessStartInfo(app.Path)
                        {
                            Arguments = string.Format("{0}{1}{0}", "\"", mTask.LocalFilePath)
                        };
                        p.StartInfo = psi;
                        p.Start();
                        p.WaitForExit();
                    }
                }
            }
        }

        public override string ToString()
        {
            StringBuilder sbDebug = new StringBuilder();
            sbDebug.AppendLine(string.Format("Image Uploader: {0", mTask.MyImageUploader));
            return sbDebug.ToString();
        }
    }
}
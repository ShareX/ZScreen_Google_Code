using System;
using ZSS.Tasks;
using System.IO;
using System.Diagnostics;
using ZSS.TextUploader.Global;
using ZSS.ImageUploader;
using ZSS.Properties;
using System.Threading;
using System.Drawing;
using System.Windows.Forms;

namespace ZSS.Helpers
{
    public class TaskManager
    {
        private MainAppTask task;

        public TaskManager(ref MainAppTask task)
        {
            this.task = task;
        }

        public void UploadImage()
        {
            task.StartTime = DateTime.Now;
            HTTPUploader imageUploader = null;

            if (Program.conf.TinyPicSizeCheck && task.ImageDestCategory == ImageDestType.TINYPIC && File.Exists(task.LocalFilePath))
            {
                SizeF size = Image.FromFile(task.LocalFilePath).PhysicalDimension;
                if (size.Width > 1600 || size.Height > 1600)
                {
                    task.ImageDestCategory = ImageDestType.IMAGESHACK;
                }
            }

            switch (task.ImageDestCategory)
            {
                case ImageDestType.CLIPBOARD:
                    task.MyWorker.ReportProgress((int)MainAppTask.ProgressType.COPY_TO_CLIPBOARD_IMAGE, task.LocalFilePath);
                    break;
                case ImageDestType.CUSTOM_UPLOADER:
                    if (Program.conf.ImageUploadersList != null && Program.conf.ImageUploaderSelected != -1)
                    {
                        imageUploader = new CustomUploader(Program.conf.ImageUploadersList[Program.conf.ImageUploaderSelected]);
                    }
                    break;
                case ImageDestType.FTP:
                    UploadFtp();
                    break;
                case ImageDestType.DEKIWIKI:
                    UploadDekiWiki();
                    break;
                case ImageDestType.IMAGESHACK:
                    imageUploader = new ImageShackUploader(Program.IMAGESHACK_KEY, Program.conf.ImageShackRegistrationCode, Program.conf.UploadMode);
                    break;
                case ImageDestType.TINYPIC:
                    imageUploader = new TinyPicUploader(Program.TINYPIC_ID, Program.TINYPIC_KEY, Program.conf.UploadMode);
                    ((TinyPicUploader)imageUploader).Shuk = Program.conf.TinyPicShuk;
                    break;
            }

            if (imageUploader != null)
            {
                task.DestinationName = imageUploader.Name;
                string fullFilePath = task.LocalFilePath;
                if (File.Exists(fullFilePath) || task.MyImage != null)
                {
                    for (int i = 1; i <= (int)Program.conf.ErrorRetryCount &&
                        (task.ImageManager == null || (task.ImageManager != null && task.ImageManager.FileCount < 1)); i++)
                    {
                        if (File.Exists(fullFilePath))
                        {
                            task.ImageManager = imageUploader.UploadImage(fullFilePath);
                        }
                        else if (task.MyImage != null)
                        {
                            task.ImageManager = imageUploader.UploadImage(task.MyImage);
                        }
                        task.Errors = imageUploader.Errors;
                        if (Program.conf.ImageUploadRetry && (task.ImageDestCategory ==
                            ImageDestType.IMAGESHACK || task.ImageDestCategory == ImageDestType.TINYPIC))
                        {
                            break;
                        }
                    }

                    //Set remote path for Screenshots history
                    if (task.ImageManager != null) task.RemoteFilePath = task.ImageManager.GetFullImageUrl();
                }
            }

            task.EndTime = DateTime.Now;

            if (Program.conf.AutoChangeUploadDestination && task.UploadDuration > (int)Program.conf.UploadDurationLimit)
            {
                if (task.ImageDestCategory == ImageDestType.IMAGESHACK)
                {
                    Program.conf.ScreenshotDestMode = ImageDestType.TINYPIC;
                }
                else if (task.ImageDestCategory == ImageDestType.TINYPIC)
                {
                    Program.conf.ScreenshotDestMode = ImageDestType.IMAGESHACK;
                }
                task.MyWorker.ReportProgress((int)MainAppTask.ProgressType.UPDATE_UPLOAD_DESTINATION);
            }

            if (task.ImageManager != null)
            {
                FlashIcon(task);
            }
        }

        private void FlashIcon(MainAppTask t)
        {
            for (int i = 0; i < (int)Program.conf.FlashTrayCount; i++)
            {
                t.MyWorker.ReportProgress((int)MainAppTask.ProgressType.FLASH_ICON, Resources.zss_uploaded);
                Thread.Sleep(275);
                t.MyWorker.ReportProgress((int)MainAppTask.ProgressType.FLASH_ICON, Resources.zss_green);
                Thread.Sleep(275);
            }
        }

        public bool UploadDekiWiki()
        {
            try
            {
                string fullFilePath = task.LocalFilePath;

                if (Program.CheckDekiWikiAccounts(ref task) && File.Exists(fullFilePath))
                {
                    DekiWikiAccount acc = Program.conf.DekiWikiAccountList[Program.conf.DekiWikiSelected];

                    if (DekiWiki.savePath == null || DekiWiki.savePath.Length == 0 || Program.conf.DekiWikiForcePath == true)
                    {
                        ZSS.Forms.DekiWikiPath diag = new ZSS.Forms.DekiWikiPath(ref acc);
                        diag.history = acc.History;
                        diag.ShowDialog();

                        if (diag.DialogResult != DialogResult.OK)
                        {
                            throw new Exception("User canceled the operation.");
                        }

                        DekiWiki.savePath = diag.path;

                    }
                    task.DestinationName = acc.Name;

                    FileSystem.AppendDebug(string.Format("Uploading {0} to Mindtouch: {1}", task.FileName, acc.Url));

                    DekiWikiUploader uploader = new DekiWikiUploader(acc);
                    task.ImageManager = uploader.UploadImage(task.LocalFilePath);
                    task.RemoteFilePath = acc.getUriPath(Path.GetFileName(task.LocalFilePath));

                    DekiWiki connector = new DekiWiki(ref acc);
                    connector.UpdateHistory();

                    return true;
                }
            }
            catch (Exception ex)
            {
                task.Errors.Add("Mindtouch upload failed.\r\n" + ex.Message);
            }

            return false;
        }

        /// <summary>
        /// Funtion to FTP the Screenshot
        /// </summary>
        /// <returns>Retuns a List of Screenshots</returns>
        public bool UploadFtp()
        {
            try
            {
                string fullFilePath = task.LocalFilePath;

                if (Program.CheckFTPAccounts(ref task) && File.Exists(fullFilePath))
                {
                    FTPAccount acc = Program.conf.FTPAccountList[Program.conf.FTPselected];
                    task.DestinationName = acc.Name;

                    FileSystem.AppendDebug(string.Format("Uploading {0} to FTP: {1}", task.FileName, acc.Server));

                    FTPUploader fu = new FTPUploader(acc)
                    {
                        EnableThumbnail = (Program.conf.ClipboardUriMode != ClipboardUriType.FULL) ||
                        Program.conf.FTPCreateThumbnail,
                        WorkingDir = Program.CacheDir
                    };
                    task.ImageManager = fu.UploadImage(fullFilePath);
                    task.RemoteFilePath = acc.getUriPath(Path.GetFileName(task.LocalFilePath));
                    return true;
                }
            }
            catch (Exception ex)
            {
                task.Errors.Add("FTP upload failed.\r\n" + ex.Message);
            }

            return false;
        }

        public void UploadText()
        {
            task.StartTime = DateTime.Now;
            switch (task.TextDestCategory)
            {
                // WE ARE ONLY SUPPORTING TXT UPLOADING VIA FTP SO FAR, PASTE2 AND PASTEBIN SUPPORT WILL COME LATER

                case TextDestType.FTP:
                    UploadFtp();
                    break;
            }
            task.EndTime = DateTime.Now;
        }

        public void TextEdit()
        {
            if (File.Exists(task.LocalFilePath))
            {
                Process p = new Process();
                ProcessStartInfo psi = new ProcessStartInfo(Program.conf.TextEditorActive.Path)
                {
                    Arguments = string.Format("{0}{1}{0}", "\"", task.LocalFilePath)
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
            if (File.Exists(task.LocalFilePath))
            {
                Process p = new Process();
                ProcessStartInfo psi = new ProcessStartInfo(Program.conf.ImageSoftwareActive.Path)
                {
                    Arguments = string.Format("{0}{1}{0}", "\"", task.LocalFilePath)
                };
                p.StartInfo = psi;
                p.Start();
                // Wait till user quits the ScreenshotEditApp
                p.WaitForExit();
            }
        }
    }
}
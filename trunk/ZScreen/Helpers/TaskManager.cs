using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZSS.Tasks;
using System.IO;
using System.Diagnostics;
using ZSS.TextUploader.Global;
using ZSS.ImageUploader;
using ZSS.Properties;
using System.Threading;

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
                if (File.Exists(fullFilePath))
                {
                    for (int i = 1; i <= (int)Program.conf.ErrorRetryCount &&
                        (task.ImageManager == null || (task.ImageManager != null && task.ImageManager.FileCount < 1)); i++)
                    {
                        task.ImageManager = imageUploader.UploadImage(fullFilePath);
                        task.Errors = imageUploader.Errors;
                        if (Program.conf.ImageUploadRetry && (task.ImageDestCategory ==
                            ImageDestType.IMAGESHACK || task.ImageDestCategory == ImageDestType.TINYPIC))
                        {
                            break;
                        }
                    }

                    //Set remote path for Screenshots history
                    task.RemoteFilePath = task.ImageManager.GetFullImageUrl();
                }
            }

            task.EndTime = DateTime.Now;

            if (task.ImageManager != null)
            {
                FlashIcon(task);
            }
        }


        private void FlashIcon(MainAppTask task)
        {
            for (int i = 0; i < (int)Program.conf.FlashTrayCount; i++)
            {
                task.MyWorker.ReportProgress((int)MainAppTask.ProgressType.FLASH_ICON, Resources.zss_uploaded);
                Thread.Sleep(275);
                task.MyWorker.ReportProgress((int)MainAppTask.ProgressType.FLASH_ICON, Resources.zss_green);
                Thread.Sleep(275);
            }
        }

        /// <summary>
        /// Funtion to FTP the Screenshot
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="fullFilePath"></param>
        /// <returns>Retuns a List of Screenshots</returns>
        public bool UploadFtp()
        {
            try
            {
                string fullFilePath = task.LocalFilePath;

                if (Program.CheckFTPAccounts() && File.Exists(fullFilePath))
                {
                    FTPAccount acc = Program.conf.FTPAccountList[Program.conf.FTPselected];
                    task.DestinationName = acc.Name;

                    FileSystem.appendDebug(string.Format("Uploading {0} to FTP: {1}", task.FileName, acc.Server));

                    ImageUploader.FTPUploader fu = new ZSS.ImageUploader.FTPUploader(acc);
                    fu.EnableThumbnail = (Program.conf.ClipboardUriMode != ClipboardUriType.FULL) || Program.conf.FTPCreateThumbnail; // = true; // ideally this shold be true
                    fu.WorkingDir = Program.conf.CacheDir;
                    task.ImageManager = fu.UploadImage(fullFilePath);
                    task.RemoteFilePath = acc.getUriPath(Path.GetFileName(task.LocalFilePath));
                    return true;
                }
                else
                {
                    task.Errors.Add("FTP upload failed.");
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
            switch (task.TextDestCategory)
            {
                // WE ARE ONLY SUPPORTING TXT UPLOADING VIA FTP SO FAR, PASTE2 AND PASTEBIN SUPPORT WILL COME LATER

                case TextDestType.FTP:
                    UploadFtp();
                    break;
            }
        }

        public void TextEdit()
        {
            if (File.Exists(task.LocalFilePath))
            {
                Process p = new Process();
                ProcessStartInfo psi = new ProcessStartInfo(Program.conf.TextEditorActive.Path);
                psi.Arguments = string.Format("{0}{1}{0}", "\"", task.LocalFilePath);
                p.StartInfo = psi;
                p.Start();
                // Wait till user quits the ScreenshotEditApp
                p.WaitForExit();
            }
        }

        /// <summary>
        /// Edit Image in selected Image Editor
        /// </summary>
        /// <param name="task"></param>
        public void ImageEdit()
        {
            if (File.Exists(task.LocalFilePath))
            {
                Process p = new Process();
                ProcessStartInfo psi = new ProcessStartInfo(Program.conf.ImageSoftwareActive.Path);
                psi.Arguments = string.Format("{0}{1}{0}", "\"", task.LocalFilePath);
                p.StartInfo = psi;
                p.Start();
                // Wait till user quits the ScreenshotEditApp
                p.WaitForExit();
            }
        }
    }
}
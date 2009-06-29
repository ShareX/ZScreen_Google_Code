using System;
using ZSS.Tasks;
using System.IO;
using System.Diagnostics;
using ZSS.ImageUploaders;
using ZSS.Properties;
using System.Threading;
using System.Drawing;
using ZSS.TextUploadersLib;
using ZSS.Global;

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
                    UploadFtpImages();
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
                    string url = task.ImageManager.GetFullImageUrl();
                    if (task.MakeTinyURL)
                    {
                        url = OnlineTasks.ShortenURL(url);
                    }
                    if (task.ImageManager != null)
                    {
                        task.RemoteFilePath = url;
                    }
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

        /// <summary>
        /// Funtion to FTP the Screenshot
        /// </summary>
        /// <returns>Retuns a List of Screenshots</returns>
        public bool UploadFtpImages()
        {
            try
            {
                string fullFilePath = task.LocalFilePath;

                if (Program.CheckFTPAccounts(ref task) && File.Exists(fullFilePath))
                {
                    FTPAccount acc = Program.conf.FTPAccountList[Program.conf.FTPSelected];
                    task.DestinationName = acc.Name;

                    FileSystem.AppendDebug(string.Format("Uploading {0} to FTP: {1}", task.FileName, acc.Server));

                    ZSS.ImageUploaders.FTPUploader fu = new ZSS.ImageUploaders.FTPUploader(acc)
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

            TextUploader textUploader = (TextUploader)task.MyTextUploader;
            string url = textUploader.UploadTextFromFile(task.LocalFilePath);
            if (task.MakeTinyURL)
            {
                url = OnlineTasks.ShortenURL(url);
            }
            task.RemoteFilePath = url;

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
                Software app = Program.conf.ImageEditorActive;
                if (app != null && File.Exists(app.Path))
                {
                    if (app.Name == Program.ZSCREEN_EDITOR)
                    {
                        try
                        {
                            Greenshot.Configuration.AppConfig.ConfigPath = Path.Combine(Program.SettingsDir, "ImageEditor.bin");
                            Greenshot.ImageEditorForm editor = new Greenshot.ImageEditorForm { Icon = Resources.zss_main };
                            editor.AutoSave = Program.conf.ImageEditorAutoSave;
                            editor.MyWorker = task.MyWorker;
                            editor.SetImage(task.MyImage);
                            editor.SetImagePath(task.LocalFilePath);
                            editor.ShowDialog();
                        }
                        catch (Exception ex)
                        {
                            FileSystem.AppendDebug(ex.ToString());
                        }
                    }
                    else
                    {
                        ProcessStartInfo psi = new ProcessStartInfo(app.Path)
                        {
                            Arguments = string.Format("{0}{1}{0}", "\"", task.LocalFilePath)
                        };
                        p.StartInfo = psi;
                        p.Start();
                        p.WaitForExit();
                    }
                }
            }
        }
    }
}
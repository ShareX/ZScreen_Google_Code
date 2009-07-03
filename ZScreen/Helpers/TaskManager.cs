using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using ZSS.Global;
using ZSS.ImageUploaderLib;
using ZSS.Properties;
using ZSS.Tasks;
using ZSS.TextUploadersLib;
using System.ComponentModel;

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
            ImageUploader imageUploader = null;

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
                    ((ImageShackUploader)imageUploader).Public = Program.conf.ImageShackShowImagesInPublic;
                    break;
                case ImageDestType.TINYPIC:
                    imageUploader = new TinyPicUploader(Program.TINYPIC_ID, Program.TINYPIC_KEY, Program.conf.UploadMode);
                    ((TinyPicUploader)imageUploader).Shuk = Program.conf.TinyPicShuk;
                    break;
                case ImageDestType.TWITPIC:
                    imageUploader = new TwitPicUploader(Program.conf.TwitPicUserName, Program.conf.TwitPicPassword, Program.conf.TwiPicUploadMode);
                    break;
            }

            if (imageUploader != null)
            {
                task.DestinationName = imageUploader.Name;
                string fullFilePath = task.LocalFilePath;
                if (File.Exists(fullFilePath) || task.MyImage != null)
                {
                    for (int i = 1; i <= (int)Program.conf.ErrorRetryCount &&
                        (task.ImageManager == null || (task.ImageManager != null && task.ImageManager.ImageFileList.Count < 1)); i++)
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
                        url = Adapter.TryShortenURL(url);
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

                if (Adapter.CheckFTPAccounts(ref task) && File.Exists(fullFilePath))
                {
                    FTPAccount acc = Program.conf.FTPAccountList[Program.conf.FTPSelected];
                    task.DestinationName = acc.Name;

                    FileSystem.AppendDebug(string.Format("Uploading {0} to FTP: {1}", task.FileName, acc.Server));

                    ZSS.ImageUploaderLib.FTPUploader fu = new ZSS.ImageUploaderLib.FTPUploader(acc)
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

        public bool UploadDekiWiki()
        {
            try
            {
                string fullFilePath = task.LocalFilePath;

                if (Adapter.CheckDekiWikiAccounts(ref task) && File.Exists(fullFilePath))
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

        public void UploadText()
        {
            task.StartTime = DateTime.Now;

            TextUploader textUploader = (TextUploader)task.MyTextUploader;
            string url = textUploader.UploadTextFromFile(task.LocalFilePath);
            if (task.MakeTinyURL)
            {
                url = Adapter.TryShortenURL(url);
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
                Software app = Program.conf.ImageEditor;
                if (app != null && File.Exists(app.Path))
                {
                    if (app.Name == Program.ZSCREEN_IMAGE_EDITOR)
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
                            Console.WriteLine(ex.ToString());
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
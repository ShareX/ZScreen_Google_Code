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
using ZSS.TextUploaderLib;
using System.ComponentModel;

namespace ZSS.Helpers
{
    public class TaskManager
    {
        private MainAppTask mTask;

        public TaskManager(ref MainAppTask task)
        {
            this.mTask = task;
        }

        public void UploadImage()
        {
            mTask.StartTime = DateTime.Now;

            ImageUploader imageUploader = null;

            if (Program.conf.TinyPicSizeCheck && mTask.ImageDestCategory == ImageDestType.TINYPIC && File.Exists(mTask.LocalFilePath))
            {
                SizeF size = Image.FromFile(mTask.LocalFilePath).PhysicalDimension;
                if (size.Width > 1600 || size.Height > 1600)
                {
                    mTask.ImageDestCategory = ImageDestType.IMAGESHACK;
                }
            }

            switch (mTask.ImageDestCategory)
            {
                case ImageDestType.CLIPBOARD:
                    mTask.MyWorker.ReportProgress((int)MainAppTask.ProgressType.COPY_TO_CLIPBOARD_IMAGE, mTask.LocalFilePath);
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
                imageUploader.ProxySettings = Adapter.GetProxySettings();
                mTask.DestinationName = imageUploader.Name;
                string fullFilePath = mTask.LocalFilePath;
                if (File.Exists(fullFilePath) || mTask.MyImage != null)
                {
                    for (int i = 1; i <= (int)Program.conf.ErrorRetryCount &&
                        (mTask.ImageManager == null || (mTask.ImageManager != null && mTask.ImageManager.ImageFileList.Count < 1)); i++)
                    {
                        if (File.Exists(fullFilePath))
                        {
                            mTask.ImageManager = imageUploader.UploadImage(fullFilePath);
                        }
                        else if (mTask.MyImage != null)
                        {
                            mTask.ImageManager = imageUploader.UploadImage(mTask.MyImage);
                        }
                        mTask.Errors = imageUploader.Errors;
                        if (Program.conf.ImageUploadRetry && (mTask.ImageDestCategory ==
                            ImageDestType.IMAGESHACK || mTask.ImageDestCategory == ImageDestType.TINYPIC))
                        {
                            break;
                        }
                    }

                    //Set remote path for Screenshots history
                    string url = mTask.ImageManager.GetFullImageUrl();
                    if (mTask.MakeTinyURL)
                    {
                        url = Adapter.TryShortenURL(url);
                    }
                    if (mTask.ImageManager != null)
                    {
                        mTask.RemoteFilePath = url;
                        mTask.ImageManager.ImageFileList.Add(new ImageFile(url, ImageFile.ImageType.FULLIMAGE_TINYURL));
                    }
                }
            }

            mTask.EndTime = DateTime.Now;

            if (Program.conf.AutoChangeUploadDestination && mTask.UploadDuration > (int)Program.conf.UploadDurationLimit)
            {
                if (mTask.ImageDestCategory == ImageDestType.IMAGESHACK)
                {
                    Program.conf.ScreenshotDestMode = ImageDestType.TINYPIC;
                }
                else if (mTask.ImageDestCategory == ImageDestType.TINYPIC)
                {
                    Program.conf.ScreenshotDestMode = ImageDestType.IMAGESHACK;
                }
                mTask.MyWorker.ReportProgress((int)MainAppTask.ProgressType.UPDATE_UPLOAD_DESTINATION);
            }

            if (mTask.ImageManager != null)
            {
                FlashIcon(mTask);
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
        public bool UploadFtp()
        {
            try
            {
                string fullFilePath = mTask.LocalFilePath;

                if (Adapter.CheckFTPAccounts(ref mTask) && File.Exists(fullFilePath))
                {
                    FTPAccount acc = Program.conf.FTPAccountList[Program.conf.FTPSelected];
                    mTask.DestinationName = acc.Name;

                    FileSystem.AppendDebug(string.Format("Uploading {0} to FTP: {1}", mTask.FileName, acc.Server));

                    ZSS.ImageUploaderLib.FTPUploader fu = new ZSS.ImageUploaderLib.FTPUploader(acc)
                    {
                        EnableThumbnail = (Program.conf.ClipboardUriMode != ClipboardUriType.FULL) || Program.conf.FTPCreateThumbnail,
                        WorkingDir = Program.CacheDir
                    };
                    mTask.ImageManager = fu.UploadImage(fullFilePath);
                    mTask.RemoteFilePath = acc.GetUriPath(Path.GetFileName(mTask.LocalFilePath));
                    return true;
                }
            }
            catch (Exception ex)
            {
                mTask.Errors.Add("FTP upload failed.\r\n" + ex.Message);
            }

            return false;
        }

        public bool UploadDekiWiki()
        {
            try
            {
                string fullFilePath = mTask.LocalFilePath;

                if (Adapter.CheckDekiWikiAccounts(ref mTask) && File.Exists(fullFilePath))
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
                    mTask.DestinationName = acc.Name;

                    FileSystem.AppendDebug(string.Format("Uploading {0} to Mindtouch: {1}", mTask.FileName, acc.Url));

                    DekiWikiUploader uploader = new DekiWikiUploader(acc);
                    mTask.ImageManager = uploader.UploadImage(mTask.LocalFilePath);
                    mTask.RemoteFilePath = acc.getUriPath(Path.GetFileName(mTask.LocalFilePath));

                    DekiWiki connector = new DekiWiki(ref acc);
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

            TextUploader textUploader = (TextUploader)mTask.MyTextUploader;
            textUploader.ProxySettings = Adapter.GetProxySettings();
            string url = "";
            if (!string.IsNullOrEmpty(mTask.MyText))
            {
                url = textUploader.UploadText(mTask.MyText);
            }
            else
            {
                url = textUploader.UploadTextFromFile(mTask.LocalFilePath);
            }
            if (mTask.MakeTinyURL)
            {
                url = Adapter.TryShortenURL(url);
            }
            mTask.RemoteFilePath = url;

            mTask.EndTime = DateTime.Now;
        }

        public void TextEdit()
        {
            if (File.Exists(mTask.LocalFilePath))
            {
                Process p = new Process();
                ProcessStartInfo psi = new ProcessStartInfo(Program.conf.TextEditorActive.Path)
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
                Software app = Program.conf.ImageEditor;
                if (app != null)
                {
                    if (app.Name == Program.ZSCREEN_IMAGE_EDITOR)
                    {
                        try
                        {
                            Greenshot.Configuration.AppConfig.ConfigPath = Path.Combine(Program.SettingsDir, "ImageEditor.bin");
                            Greenshot.ImageEditorForm editor = new Greenshot.ImageEditorForm { Icon = Resources.zss_main };
                            editor.AutoSave = Program.conf.ImageEditorAutoSave;
                            editor.MyWorker = mTask.MyWorker;
                            editor.SetImage(mTask.MyImage);
                            editor.SetImagePath(mTask.LocalFilePath);
                            editor.ShowDialog();
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.ToString());
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
    }
}
#region License Information (GPL v2)
/*
    ZUploader - A program that allows you to upload images, text or files in your clipboard
    Copyright (C) 2010 ZScreen Developers

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
using System.ComponentModel;
using System.IO;
using UploadersLib;
using UploadersLib.FileUploaders;
using UploadersLib.Helpers;
using UploadersLib.ImageUploaders;
using UploadersLib.TextUploaders;

namespace ZUploader
{
    public class Task : IDisposable
    {
        public delegate void TaskEventHandler(UploadInfo info);

        public event TaskEventHandler UploadStarted;
        public event TaskEventHandler UploadProgressChanged;
        public event TaskEventHandler UploadCompleted;

        public UploadInfo Info { get; private set; }

        private Stream Data;
        private BackgroundWorker bw;

        #region Constructors

        private Task()
        {
            Info = new UploadInfo();
            Info.ID = UploadManager.GetID();
        }

        /// <summary>
        /// Create task using file path
        /// </summary>
        public Task(EDataType dataType, string filePath)
            : this()
        {
            Info.UploaderType = dataType;
            Info.FilePath = filePath;
            Data = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);
        }

        /// <summary>
        /// Create task using stream
        /// </summary>
        public Task(EDataType dataType, Stream stream, string fileName)
            : this()
        {
            Info.UploaderType = dataType;
            Info.FileName = fileName;
            Data = stream;
        }

        #endregion

        public void Start()
        {
            OnUploadStarted();

            ProxySettings proxy = new ProxySettings();
            if (!string.IsNullOrEmpty(Program.Settings.ProxySettings.Host))
            {
                proxy.ProxyConfig = ProxyConfigType.ManualProxy;
            }
            proxy.ProxyActive = Program.Settings.ProxySettings;
            Uploader.ProxySettings = proxy;

            bw = new BackgroundWorker();
            bw.WorkerReportsProgress = true;
            bw.DoWork += new DoWorkEventHandler(UploadThread);
            bw.ProgressChanged += new ProgressChangedEventHandler(bw_ProgressChanged);
            bw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bw_RunWorkerCompleted);
            bw.RunWorkerAsync();
        }

        private void UploadThread(object sender, DoWorkEventArgs e)
        {
            try
            {
                switch (Info.UploaderType)
                {
                    case EDataType.File:
                        Info.Result = UploadFile(Data, Info.FileName);
                        break;
                    case EDataType.Image:
                        Info.Result = UploadImage(Data, Info.FileName);
                        break;
                    case EDataType.Text:
                        Info.Result = UploadText(Data);
                        break;
                }
            }
            catch (Exception ex)
            {
                Info.Result.Errors.Add(ex.Message);
            }
        }

        public UploadResult UploadFile(Stream stream, string fileName)
        {
            FileUploader fileUploader = null;

            switch (UploadManager.FileUploader)
            {
                case FileUploaderType2.FTP:
                    fileUploader = new FTPUploader(Program.Settings.FTPAccount);
                    break;
                case FileUploaderType2.SendSpace:
                    fileUploader = new SendSpace();
                    SendSpaceManager.PrepareUploadInfo(null, null);
                    break;
                case FileUploaderType2.RapidShare:
                    fileUploader = new RapidShare();
                    break;
                case FileUploaderType2.FileBin:
                    fileUploader = new FileBin();
                    break;
                case FileUploaderType2.DropIO:
                    fileUploader = new DropIO();
                    break;
                case FileUploaderType2.ShareCX:
                    fileUploader = new ShareCX();
                    break;
                default:
                    break;
            }

            if (fileUploader != null)
            {
                fileUploader.ProgressChanged += (x) => bw.ReportProgress((int)x.Percentage, x);
                UploadResult ur = fileUploader.Upload(stream, fileName);
                ur.Errors = fileUploader.Errors;
                return ur;
            }

            return null;
        }

        public UploadResult UploadImage(Stream stream, string fileName)
        {
            ImageUploader imageUploader = null;

            switch (UploadManager.ImageUploader)
            {
                case ImageDestType2.IMAGESHACK:
                    imageUploader = new ImageShackUploader(Program.ImageShackKey, string.Empty);
                    break;
                case ImageDestType2.TINYPIC:
                    imageUploader = new TinyPicUploader(Program.TinyPicID, Program.TinyPicKey, string.Empty);
                    break;
                case ImageDestType2.IMAGEBIN:
                    imageUploader = new ImageBin();
                    break;
                case ImageDestType2.IMG1:
                    imageUploader = new Img1Uploader();
                    break;
                case ImageDestType2.IMGUR:
                    imageUploader = new Imgur(Program.ImgurKey);
                    break;
                case ImageDestType2.UPLOADSCREENSHOT:
                    imageUploader = new UploadScreenshot(Program.UploadScreenshotKey);
                    break;
                default:
                    break;
            }

            if (imageUploader != null)
            {
                imageUploader.ProgressChanged += (x) => bw.ReportProgress((int)x.Percentage, x);
                ImageFileManager ifm = imageUploader.UploadImage(stream, fileName);
                UploadResult ur = new UploadResult
                {
                    URL = ifm.GetFullImageUrl(),
                    ThumbnailURL = ifm.GetThumbnailUrl(),
                    DeletionURL = ifm.GetDeletionLink(),
                    Errors = imageUploader.Errors
                };
                return ur;
            }

            return null;
        }

        public UploadResult UploadText(Stream stream)
        {
            TextUploader textUploader = null;

            switch (UploadManager.TextUploader)
            {
                case TextDestType2.PASTE2:
                    textUploader = new Paste2Uploader();
                    break;
                case TextDestType2.PASTEBIN:
                    textUploader = new PastebinUploader();
                    break;
                case TextDestType2.PASTEBIN_CA:
                    textUploader = new PastebinCaUploader();
                    break;
                case TextDestType2.SLEXY:
                    textUploader = new SlexyUploader();
                    break;
                default:
                    break;
            }

            if (textUploader != null)
            {
                string text = new StreamReader(stream).ReadToEnd();
                string url = textUploader.UploadText(TextInfo.FromString(text));
                UploadResult ur = new UploadResult
                {
                    URL = url,
                    Errors = textUploader.Errors
                };
                return ur;
            }

            return null;
        }

        private void bw_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            ProgressManager progress = e.UserState as ProgressManager;
            if (progress != null)
            {
                Info.Progress = progress;
                OnUploadProgressChanged();
            }
        }

        private void bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            OnUploadCompleted();
            Dispose();
        }

        private void OnUploadStarted()
        {
            if (UploadStarted != null)
            {
                Info.Status = "Uploading";
                UploadStarted(Info);
            }
        }

        private void OnUploadProgressChanged()
        {
            if (UploadProgressChanged != null)
            {
                UploadProgressChanged(Info);
            }
        }

        private void OnUploadCompleted()
        {
            if (UploadCompleted != null)
            {
                Info.Status = "Completed";
                UploadCompleted(Info);
            }
        }

        public void Dispose()
        {
            if (Data != null)
            {
                Data.Dispose();
            }
        }
    }
}
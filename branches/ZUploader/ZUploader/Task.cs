using System.ComponentModel;
using System.Drawing;
using System.IO;
using UploadersLib;
using UploadersLib.Helpers;
using UploadersLib.ImageUploaders;
using System;

namespace ZUploader
{
    public class Task : IDisposable
    {
        public DataManager DataManager { get; private set; }

        public delegate void UploadStartedEventHandler(Task sender);
        public delegate void UploadCompletedEventHandler(Task sender, UploadCompletedEventArgs e);

        public class UploadCompletedEventArgs
        {
            public string URL { get; set; }
            public string Thumbnail { get; set; }
        }

        public event UploadStartedEventHandler UploadStarted;
        public event UploadCompletedEventHandler UploadCompleted;

        public int TaskID { get; private set; }

        #region Constructors

        public Task()
        {
            TaskID = UploadManager.GetID();
            DataManager = new DataManager();
        }

        public Task(EDataType dataType, Stream stream, string fileName)
            : this()
        {
            switch (DataManager.FileType = dataType)
            {
                case EDataType.Data:
                    DataManager.Data = Helpers.GetBytes(stream);
                    break;
                case EDataType.Image:
                    DataManager.Image = Image.FromStream(stream);
                    break;
                case EDataType.Text:
                    DataManager.Text = new StreamReader(stream).ReadToEnd();
                    break;
            }

            DataManager.FileName = fileName;
        }

        public Task(Image image, string fileName)
            : this()
        {
            DataManager.FileType = EDataType.Image;
            DataManager.Image = image;
            DataManager.FileName = fileName;
        }

        public Task(string text)
            : this()
        {
            DataManager.FileType = EDataType.Text;
            DataManager.Text = text;
        }

        #endregion

        public void Start()
        {
            OnUploadStarted();

            BackgroundWorker bw = new BackgroundWorker();
            bw.DoWork += new DoWorkEventHandler(UploadThread);
            bw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bw_RunWorkerCompleted);
            bw.RunWorkerAsync();
        }

        private void UploadThread(object sender, DoWorkEventArgs e)
        {
            switch (DataManager.FileType)
            {
                case EDataType.Data:
                    e.Result = Uploaders.UploadFile(DataManager.Data, DataManager.FileName);
                    break;
                case EDataType.Image:
                    e.Result = Uploaders.UploadImage(DataManager.Image, DataManager.FileName);
                    break;
                case EDataType.Text:
                    e.Result = Uploaders.UploadText(DataManager.Text);
                    break;
            }
        }

        private void bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            UploadCompletedEventArgs args = new UploadCompletedEventArgs();

            if (e.Result is ImageFileManager)
            {
                ImageFileManager ifm = (ImageFileManager)e.Result;
                args.URL = ifm.GetFullImageUrl();
                args.Thumbnail = ifm.GetThumbnailUrl();
            }
            else if (e.Result is string)
            {
                args.URL = (string)e.Result;
            }

            OnUploadCompleted(args);
        }

        private void OnUploadStarted()
        {
            if (UploadStarted != null)
            {
                UploadStarted(this);
            }
        }

        private void OnUploadCompleted(UploadCompletedEventArgs e)
        {
            if (UploadCompleted != null)
            {
                UploadCompleted(this, e);
            }
        }

        public void Dispose()
        {
            if (DataManager != null)
            {
                DataManager.Dispose();
            }
        }
    }
}
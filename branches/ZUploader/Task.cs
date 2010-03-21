using System.ComponentModel;
using System.Drawing;
using System.IO;
using UploadersLib;
using UploadersLib.Helpers;
using UploadersLib.ImageUploaders;

namespace ZUploader
{
    public class Task
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
                    DataManager.Data = stream;
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

        public Task(Image image)
            : this()
        {
            DataManager.FileType = EDataType.Image;
            DataManager.Image = image;
            DataManager.FileName = Helpers.GetRandomAlphanumeric(10);
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
                    break;
                case EDataType.Image:
                    e.Result = Uploaders.UploadImage(DataManager.Image, DataManager.FileName);
                    break;
                case EDataType.Text:
                    break;
            }
        }

        private void bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            UploadCompletedEventArgs args = new UploadCompletedEventArgs();

            ImageFileManager ifm = e.Result as ImageFileManager;

            if (ifm != null)
            {
                args.URL = ifm.GetFullImageUrl();
                args.Thumbnail = ifm.GetThumbnailUrl();
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
    }
}
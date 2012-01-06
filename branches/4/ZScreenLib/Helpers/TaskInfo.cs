using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using UploadersLib;

namespace ZScreenLib
{
    public class TaskInfo
    {
        public WorkerTask.JobLevel2 Job { get; set; }

        public IntPtr Handle { get; set; }

        public DestConfig DestConfig { get; set; }

        public NotifyIcon TrayIcon { get; set; }

        public string FileName
        {
            get
            {
                return Path.GetFileName(mFilePath);
            }
        }

        public string FileSize { get; set; }

        private string mFilePath;

        public string LocalFilePath
        {
            get
            {
                return mFilePath;
            }
            set
            {
                mFilePath = value;
            }
        }

        public string ExistingFilePath
        {
            get
            {
                return mFilePath;
            }
            set
            {
                if (File.Exists(value))
                {
                    mFilePath = value;
                }
                else
                {
                    throw new Exception(string.Format("{0} does not exist.", value));
                }
            }
        }

        public Size ImageSize { get; set; }

        public string WindowTitleText { get; set; }
    }
}
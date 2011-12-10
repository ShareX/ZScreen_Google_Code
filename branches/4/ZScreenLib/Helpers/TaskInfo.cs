using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Crop;
using Gif.Components;
using GraphicsMgrLib;
using HelpersLib;
using HistoryLib;
using ImageQueue;
using Microsoft.WindowsAPICodePack.Taskbar;
using ScreenCapture;
using UploadersLib;
using UploadersLib.FileUploaders;
using UploadersLib.GUI;
using UploadersLib.HelperClasses;
using UploadersLib.ImageUploaders;
using UploadersLib.OtherServices;
using UploadersLib.TextUploaders;
using UploadersLib.URLShorteners;
using ZScreenLib.Properties;
using ZSS.IndexersLib;
using ZUploader.HelperClasses;

namespace ZScreenLib
{
    public class TaskInfo
    {
        public WorkerTask.JobLevel2 Job { get; set; }

        public IntPtr Handle { get; set; }

        public DestConfig DestConfig { get; set; }

        public NotifyIcon TrayIcon { get; set; }

        public string FileName { get; set; }

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
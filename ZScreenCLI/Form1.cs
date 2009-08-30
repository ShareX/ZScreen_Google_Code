using System;
using System.ComponentModel;
using System.Windows.Forms;
using ZScreenLib;
using System.IO;
using System.Collections.Generic;

namespace ZScreenCLI
{
    public partial class Form1 : GenericMainWindow
    {
        public Form1()
        {
            InitializeComponent();
            ProcessArgs();
        }

        public void ProcessArgs()
        {
            string[] args = Environment.GetCommandLineArgs();
            WorkerTask task = null;

            if (args.Length > 1)
            {
                this.niTray.Icon = ResxMgr.BusyIcon;
                Engine.TurnOn(new ZScreenLib.Engine.EngineOptions { KeyboardHook = false, ShowConfigWizard = false });
                Engine.LoadSettingsLatest();

                try
                {
                    if (args[1].ToLower() == "crop_shot")
                    {
                        // Crop Shot
                        task = CropShot(WorkerTask.Jobs.TakeScreenshotCropped);
                    }
                    else if (args[1].ToLower() == "selected_window")
                    {
                        // Selected Window
                        task = CropShot(WorkerTask.Jobs.TakeScreenshotWindowSelected);
                    }
                    else if (args[1].ToLower() == "clipboard_upload")
                    {
                        // Clipboard Upload
                        ClipboardUpload();
                    }
                    else if (args[1].ToLower() == "upload" && args.Length > 2 && !string.IsNullOrEmpty(args[2]))
                    {
                        FileUpload(args[2]);
                    }
                }
                catch (Exception ex)
                {
                    Console.Write(ex.ToString());
                }

                this.niTray.Icon = ResxMgr.ReadyIcon;
            }
        }

        #region Command Line Arguments

        public WorkerTask CropShot(WorkerTask.Jobs job)
        {
            WorkerTask task = new Worker(this).CreateTask(job);
            task.MyImageUploader = Engine.conf.ScreenshotDestMode;
            new TaskManager(ref task).CaptureRegionOrWindow();
            new BalloonTipHelper(this.niTray, task).ShowBalloonTip();
            UploadManager.SetClipboardText(task, false);
            return task;
        }

        public void ClipboardUpload()
        {
            Worker cu = new Worker(this);
            cu.UploadUsingClipboard2();
        }

        public void FileUpload(string fp)
        {
            if (File.Exists(fp) || Directory.Exists(fp))
            {
                Worker fu = new Worker(this);
                fu.UploadUsingFileSystem(FileSystem.GetExplorerFileList(new string[] { fp }));
            }
        }

        #endregion

        #region Tray Events

        private void niTray_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void niTray_BalloonTipClosed(object sender, EventArgs e)
        {
            this.Close();
        }

        private void niTray_BalloonTipClicked(object sender, EventArgs e)
        {
            if (ZScreenLib.Engine.conf.BalloonTipOpenLink)
            {
                NotifyIcon ni = (NotifyIcon)sender;
                new BalloonTipHelper(ni).ClickBalloonTip();
            }
            this.Close();
        }

        #endregion
    }
}
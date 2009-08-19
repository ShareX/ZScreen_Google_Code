using System;
using System.Windows.Forms;
using ZScreenLib;

namespace ZScreenCLI
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            string[] args = Environment.GetCommandLineArgs();
            if (args.Length > 1)
            {
                ZScreenLib.Program.Load(false);
                WorkerTask task = null;
                // this.niTray.Icon = ResxMgr.BusyIcon;
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
                    }
                }
                catch (Exception ex)
                {
                    Console.Write(ex.ToString());
                }
                // this.niTray.Icon = ResxMgr.ReadyIcon;
                Application.Run(new Form1(task));
            }
        }

        public static WorkerTask CropShot(WorkerTask.Jobs job)
        {
            WorkerTask task = new WorkerTask(job);
            task.MyImageUploader = ZScreenLib.Program.conf.ScreenshotDestMode;
            new TaskManager(ref task).CaptureRegionOrWindow();
            //   new BalloonTipHelper(this.niTray, task).ShowBalloonTip();
            UploadManager.SetClipboardText(task, false);
            return task;
        }
    }
}
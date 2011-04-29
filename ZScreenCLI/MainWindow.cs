using System;
using System.IO;
using System.Windows.Forms;
using ZScreenLib;

namespace ZScreenCLI
{
    public partial class MainWindow : GenericMainWindow
    {
        private Worker mWorker = null;

        public MainWindow()
        {
            InitializeComponent();
            mWorker = new Worker(this);
        }

        public void ProcessArgs()
        {
            string[] args = Environment.GetCommandLineArgs();

            if (args.Length > 1)
            {
                this.niTray.Icon = ResxMgr.BusyIcon;
                FileSystem.AppendDebug("Command Line: " + Environment.CommandLine);
                try
                {
                    if (args[1].ToLower() == "crop_shot")
                    {
                        // Crop Shot
                        mWorker.StartBw_CropShot();
                    }
                    else if (args[1].ToLower() == "selected_window")
                    {
                        // Selected Window
                        mWorker.StartBw_SelectedWindow();
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
                    tmrClose.Enabled = true;
                }
                catch (Exception ex)
                {
                    Console.Write(ex.ToString());
                }

                this.niTray.Icon = ResxMgr.ReadyIcon;
            }
            else
            {
                this.Close();
            }
        }

        #region Command Line Arguments

        public void ClipboardUpload()
        {
            mWorker.StartBw_ClipboardUpload();
        }

        public void FileUpload(string fp)
        {
            if (File.Exists(fp) || Directory.Exists(fp))
            {
                mWorker.UploadUsingFileSystem(FileSystem.GetExplorerFileList(new string[] { fp }));
            }
        }

        #endregion Command Line Arguments

        #region Tray Events

        private void niTray_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void niTray_BalloonTipClosed(object sender, EventArgs e)
        {
            if (0 == Application.OpenForms.Count)
            {
                this.Close();
            }
        }

        private void niTray_BalloonTipClicked(object sender, EventArgs e)
        {
            if (Engine.conf.BalloonTipOpenLink)
            {
                NotifyIcon ni = (NotifyIcon)sender;
                new BalloonTipHelper(ni).ClickBalloonTip();
            }
            this.Close();
        }

        #endregion Tray Events

        private void MainWindow_Load(object sender, EventArgs e)
        {
            ProcessArgs();
        }

        private void tmrClose_Tick(object sender, EventArgs e)
        {
            Console.WriteLine("UploadInfoList: " + ClipboardManager.UploadInfoList.Count);
            Console.WriteLine("OpenForms: " + Application.OpenForms.Count);
            if (null != mWorker)
            {
                Console.WriteLine("WorkerIsBusy: " + mWorker.IsBusy);
            }

            if (0 == ClipboardManager.UploadInfoList.Count && 1 == Application.OpenForms.Count && null != mWorker && !mWorker.IsBusy)
            {
                this.Close();
            }
        }

        private void MainWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.niTray.Visible = false;
        }
    }
}
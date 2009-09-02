using System;
using System.ComponentModel;
using System.Windows.Forms;
using ZScreenLib;
using System.IO;
using System.Collections.Generic;

namespace ZScreenCLI
{
    public partial class MainWindow : GenericMainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
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
                        Worker cs = new Worker(this);
                        cs.StartBw_CropShot();
                    }
                    else if (args[1].ToLower() == "selected_window")
                    {
                        // Selected Window
                        Worker cs = new Worker(this);
                        cs.StartBw_SelectedWindow();
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
            Worker cu = new Worker(this);
            cu.StartBw_ClipboardUpload();
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
            if (Engine.conf.BalloonTipOpenLink)
            {
                NotifyIcon ni = (NotifyIcon)sender;
                new BalloonTipHelper(ni).ClickBalloonTip();
            }
            this.Close();
        }

        #endregion

        private void MainWindow_Load(object sender, EventArgs e)
        {
            ProcessArgs();
        }

        private void tmrClose_Tick(object sender, EventArgs e)
        {
            if (0 == UploadManager.UploadInfoList.Count && 0 == Application.OpenForms.Count)
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
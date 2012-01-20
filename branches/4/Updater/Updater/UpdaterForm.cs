#region License Information (GPL v2)

/*
    ZUploader - A program that allows you to upload images, texts or files
    Copyright (C) 2008-2012 ZScreen Developers

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

#endregion License Information (GPL v2)

using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.IO;
using System.Web;
using System.Windows.Forms;

namespace Updater
{
    public partial class UpdaterForm : Form
    {
        public string URL { get; set; }
        public string ProcessName { get; set; }
        public string ProcessPath { get; private set; }
        public string FileName { get; set; }
        public string SavePath { get; private set; }
        public bool RunAs { get; set; }

        private FileDownloader fileDownloader;
        private FileStream stream;
        private bool downloadStarted;
        private bool paused;

        private Rectangle fillRect, drawRect;
        private LinearGradientBrush backgroundBrush;

        public UpdaterForm()
        {
            InitializeComponent();
            fillRect = new Rectangle(0, 0, ClientSize.Width, ClientSize.Height);
            drawRect = new Rectangle(0, 0, fillRect.Width - 1, fillRect.Height - 1);
            backgroundBrush = new LinearGradientBrush(fillRect, Color.FromArgb(80, 80, 80), Color.FromArgb(50, 50, 50), LinearGradientMode.Vertical);

            ChangeStatus("Waiting.");
        }

        public UpdaterForm(string lUrl, string lProcessPath, bool lRunAs)
            : this()
        {
            URL = lUrl;
            ProcessPath = lProcessPath;
            RunAs = lRunAs;
            Debug.WriteLine("RunAs: " + RunAs);
            ProcessName = Path.GetFileNameWithoutExtension(lProcessPath);
            FileName = HttpUtility.UrlDecode(lUrl.Substring(lUrl.LastIndexOf('/') + 1));
            lblFilename.Text = "Filename: " + FileName;
        }

        private void UpdaterForm_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.FillRectangle(backgroundBrush, fillRect);
        }

        private void ChangeStatus(string status)
        {
            lblStatus.Text = "Status: " + status;
        }

        private void ChangeProgress()
        {
            pbProgress.Value = (int)Math.Round(fileDownloader.DownloadPercentage);
            lblProgress.Text = String.Format(CultureInfo.CurrentCulture,
                "Progress: {0:0.##}%\nDownload speed: {1:0.##} KiB/s\nFile size: {2:n0} / {3:n0} KiB",
                fileDownloader.DownloadPercentage, fileDownloader.DownloadSpeed / 1024, fileDownloader.DownloadedSize / 1024,
                fileDownloader.FileSize / 1024);
        }

        private void btnDownload_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(URL))
            {
                if (!downloadStarted)
                {
                    downloadStarted = true;
                    btnDownload.Text = "Pause";

                    SavePath = Path.Combine(Path.GetTempPath(), FileName);
                    stream = new FileStream(SavePath, FileMode.Create, FileAccess.Write, FileShare.Read);
                    fileDownloader = new FileDownloader(URL, stream);
                    fileDownloader.FileSizeReceived += (v1, v2) => ChangeProgress();
                    fileDownloader.DownloadStarted += (v1, v2) => ChangeStatus("Download started.");
                    fileDownloader.ProgressChanged += (v1, v2) => ChangeProgress();
                    fileDownloader.DownloadCompleted += new EventHandler(fileDownloader_DownloadCompleted);
                    fileDownloader.ExceptionThrowed += (v1, v2) => ChangeStatus("Exception: " + fileDownloader.LastException.Message);
                    fileDownloader.StartDownload();

                    ChangeStatus("Getting file size.");
                }
                else
                {
                    if (paused)
                    {
                        paused = false;
                        btnDownload.Text = "Pause";
                        ChangeStatus("Downloading.");
                        fileDownloader.ResumeDownload();
                    }
                    else
                    {
                        paused = true;
                        btnDownload.Text = "Resume";
                        ChangeStatus("Paused.");
                        fileDownloader.PauseDownload();
                    }
                }
            }
        }

        private void fileDownloader_DownloadCompleted(object sender, EventArgs e)
        {
            stream.Close();
            ChangeStatus("Download completed.");
            btnDownload.Text = "Completed.";
            btnDownload.Enabled = false;

            if (MessageBox.Show("Download completed. If " + ProcessName + " is open please close for the setup to install properly. " +
                "If you press Yes then Updater will automatically close " + ProcessName + " and open installer.",
                this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (!string.IsNullOrEmpty(ProcessName))
                {
                    foreach (Process process in Process.GetProcessesByName(ProcessName))
                    {
                        process.CloseMainWindow();
                    }
                    System.Threading.Thread.Sleep(4000);
                    foreach (Process process in Process.GetProcessesByName(ProcessName))
                    {
                        process.Kill();
                    }
                }
            }
            ProcessStartInfo psi = new ProcessStartInfo(SavePath);
            psi.Arguments = "/SILENT";
            if (RunAs)
            {
                psi.Verb = "runas";
            }
            psi.UseShellExecute = true;
            Process exe = Process.Start(psi);
            exe.EnableRaisingEvents = true;
            exe.Exited += new EventHandler(Installer_Exited);
        }

        private void Installer_Exited(object sender, EventArgs e)
        {
            if (File.Exists(ProcessPath))
            {
                Process.Start(ProcessPath);
            }
            Application.Exit();
        }

        private void openDownloadUrlToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(URL))
            {
                Process.Start(URL);
            }
        }

        private void copyDownloadUrlToClipboardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(URL))
            {
                Clipboard.SetText(URL);
            }
        }
    }
}
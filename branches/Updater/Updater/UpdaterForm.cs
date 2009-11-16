#region License Information (GPL v2)
/*
    ZScreen - A program that allows you to upload screenshots in one keystroke.
    Copyright (C) 2008-2009  Brandon Zimmerman

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
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.IO;
using System.Web;
using System.Windows.Forms;
using System.Diagnostics;

namespace Updater
{
    public partial class UpdaterForm : Form
    {
        public string URL { get; set; }
        public string ProcessName { get; set; }
        public string FileName { get; set; }
        public string SavePath { get; private set; }

        private FileDownloader fileDownloader;
        private FileStream stream;
        private bool downloadStarted;
        private bool paused;

        public UpdaterForm()
        {
            InitializeComponent();
            ChangeStatus("Waiting.");
        }

        public UpdaterForm(string url, string processName)
            : this()
        {
            URL = url;
            ProcessName = processName;
            FileName = HttpUtility.UrlDecode(url.Substring(url.LastIndexOf('/') + 1));
            lblFilename.Text = "Filename: " + FileName;
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
                        process.Close();
                    }
                }

                Process exe = Process.Start(SavePath);
                exe.EnableRaisingEvents = true;
                exe.Exited += new EventHandler(exe_Exited);
            }
        }

        private void exe_Exited(object sender, EventArgs e)
        {
            MessageBox.Show("Update success.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);

            if (File.Exists(SavePath))
            {
                File.Delete(SavePath);
            }

            Application.Exit();
        }

        private void UpdaterForm_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Rectangle rect = new Rectangle(0, 0, this.ClientSize.Width, this.ClientSize.Height);
            LinearGradientBrush brush = new LinearGradientBrush(rect, Color.Black, Color.FromArgb(50, 50, 50), LinearGradientMode.Vertical);
            brush.SetSigmaBellShape(0.20f);
            g.FillRectangle(brush, rect);
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
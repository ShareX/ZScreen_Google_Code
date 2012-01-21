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

namespace HelpersLib
{
    public partial class DownloaderForm : Form
    {
        public string URL { get; set; }

        public string FileName { get; set; }

        public string SavePath { get; private set; }

        public string Changelog { get; set; }

        public bool DownloadStarted { get; private set; }

        public bool DownloadCompleted { get; private set; }

        public bool InstallStarted { get; private set; }

        private FileDownloader fileDownloader;
        private FileStream stream;
        private Rectangle fillRect, drawRect;
        private LinearGradientBrush backgroundBrush;

        public DownloaderForm()
        {
            InitializeComponent();
            fillRect = new Rectangle(0, 0, ClientSize.Width, ClientSize.Height);
            drawRect = new Rectangle(0, 0, fillRect.Width - 1, fillRect.Height - 1);
            backgroundBrush = new LinearGradientBrush(fillRect, Color.FromArgb(80, 80, 80), Color.FromArgb(50, 50, 50), LinearGradientMode.Vertical);
            this.Size = new Size(496, 235);
            ChangeStatus("Waiting.");
        }

        public DownloaderForm(string url, string Changelog)
            : this()
        {
            URL = url;
            this.Changelog = Changelog;
            txtChangelog.Text = Changelog;
            FileName = HttpUtility.UrlDecode(URL.Substring(URL.LastIndexOf('/') + 1));
            lblFilename.Text = "Filename: " + FileName;
        }

        private void UpdaterForm_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.FillRectangle(backgroundBrush, fillRect);
        }

        private void DownloaderForm_Shown(object sender, EventArgs e)
        {
            StartDownload();
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

        private void StartDownload()
        {
            if (!string.IsNullOrEmpty(URL) && !DownloadStarted)
            {
                DownloadStarted = true;

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
        }

        private void fileDownloader_DownloadCompleted(object sender, EventArgs e)
        {
            DownloadCompleted = true;
            ChangeStatus("Download completed.");
            btnCancel.Text = "Install";
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

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (DownloadCompleted)
            {
                try
                {
                    ProcessStartInfo psi = new ProcessStartInfo(SavePath);
                    psi.Verb = "runas";
                    psi.UseShellExecute = true;
                    Process.Start(psi);
                    InstallStarted = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString(), "Updater", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            Close();
        }

        private void UpdaterForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (DownloadStarted && !DownloadCompleted)
            {
                fileDownloader.StopDownload();
            }
        }

        private void cbHistory_CheckedChanged(object sender, EventArgs e)
        {
            if (cbHistory.Checked)
            {
                this.Size = new Size(496, 449);
            }
            else
            {
                this.Size = new Size(496, 235);
            }
        }
    }
}
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

namespace Updater
{
    public partial class UpdaterForm : Form
    {
        public string URL { get; set; }
        public string ProcessName { get; set; }

        private FileDownloader fileDownloader;
        private MemoryStream stream;

        public UpdaterForm()
        {
            InitializeComponent();
            ChangeStatus("Waiting");
        }

        public UpdaterForm(string url, string processName)
            : this()
        {
            URL = url;
            ProcessName = processName;
            string filename = HttpUtility.UrlDecode(url.Substring(url.LastIndexOf('/') + 1));
            lblFilename.Text = "Filename: " + filename;

            stream = new MemoryStream();
            fileDownloader = new FileDownloader(url, stream);
            fileDownloader.FileSizeReceived += (v1, v2) => ChangeProgress();
            fileDownloader.DownloadStarted += (v1, v2) => ChangeStatus("Download started.");
            fileDownloader.ProgressChanged += (v1, v2) => ChangeProgress();
            fileDownloader.DownloadCompleted += (v1, v2) => ChangeStatus("Download completed.");
            fileDownloader.ExceptionThrowed += (v1, v2) => ChangeStatus("Exception: " + fileDownloader.LastException.Message);
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
            btnDownload.Enabled = false;
            fileDownloader.StartDownload();
            ChangeStatus("Getting file size.");
        }

        private void UpdaterForm_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Rectangle rect = new Rectangle(0, 0, this.ClientSize.Width, this.ClientSize.Height);
            LinearGradientBrush brush = new LinearGradientBrush(rect, Color.Black, Color.FromArgb(50, 50, 50), LinearGradientMode.Vertical);
            brush.SetSigmaBellShape(0.20f);
            g.FillRectangle(brush, rect);
        }
    }
}
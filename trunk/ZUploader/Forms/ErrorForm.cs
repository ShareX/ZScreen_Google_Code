#region License Information (GPL v2)

/*
    ZUploader - A program that allows you to upload images, texts or files
    Copyright (C) 2008-2011 ZScreen Developers

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
using System.IO;
using System.Windows.Forms;
using HelpersLib;

namespace ZUploader
{
    public partial class ErrorForm : Form
    {
        public Exception Exception { get; private set; }
        public string LogPath { get; private set; }
        public string BugReportPath { get; private set; }

        public ErrorForm(string title, Exception e, string logPath, string bugReportPath)
        {
            InitializeComponent();
            this.Text = title;
            Exception = e;
            LogPath = logPath;
            BugReportPath = bugReportPath;

            lblExceptionMessage.Text = Exception.Message;
            txtException.Text = Exception.ToString();
            btnOpenLogFile.Visible = !string.IsNullOrEmpty(LogPath) && File.Exists(LogPath);
            btnSendBugReport.Visible = !string.IsNullOrEmpty(BugReportPath);
        }

        private void btnCopyAll_Click(object sender, EventArgs e)
        {
            string text = txtException.Text;

            if (!string.IsNullOrEmpty(text))
            {
                Helpers.CopyTextSafely(text);
            }
        }

        private void btnOpenLogFile_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(LogPath) && File.Exists(LogPath))
            {
                Process.Start(LogPath);
            }
        }

        private void btnSendBugReport_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(BugReportPath))
            {
                Process.Start(BugReportPath);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ErrorForm_Shown(object sender, EventArgs e)
        {
            this.BringToFront();
            this.Activate();
        }

        private void ErrorForm_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Rectangle rect = new Rectangle(0, 0, this.ClientSize.Width, this.ClientSize.Height);
            using (LinearGradientBrush brush = new LinearGradientBrush(rect, Color.Black, Color.FromArgb(50, 50, 50), LinearGradientMode.Vertical))
            {
                brush.SetSigmaBellShape(0.25f);
                g.FillRectangle(brush, rect);
            }
        }
    }
}
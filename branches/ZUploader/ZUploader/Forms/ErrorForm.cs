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
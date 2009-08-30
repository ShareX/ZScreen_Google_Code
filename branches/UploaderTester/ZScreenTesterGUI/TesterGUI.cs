using System;
using System.Windows.Forms;
using UploadersLib;
using System.Linq;
using ZScreenLib;
using System.Drawing;
using System.ComponentModel;
using System.Collections.Generic;
using System.IO;
using System.Diagnostics;

namespace ZScreenTesterGUI
{
    public partial class TesterGUI : Form
    {
        List<ImageDestType> Uploaders = new List<ImageDestType>();

        public TesterGUI()
        {
            InitializeComponent();

            foreach (ImageDestType uploader in Enum.GetValues(typeof(UploadersLib.ImageDestType)))
            {
                switch (uploader)
                {
                    case ImageDestType.CLIPBOARD:
                    case ImageDestType.FILE:
                    case ImageDestType.PRINTER:
                    case ImageDestType.TWITSNAPS:
                        continue;
                }

                Uploaders.Add(uploader);
            }

            foreach (ImageDestType uploader in Uploaders)
            {
                lvUploaders.Items.Add(uploader.GetDescription()).SubItems.Add("");
            }

            if (!File.Exists(Tester.TestFile))
            {
                OpenFileDialog dlg = new OpenFileDialog();
                dlg.Title = "Browse for a test file...";

                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    Tester.TestFile = dlg.FileName;
                }
            }

            StartTest();
        }

        public void StartTest()
        {
            foreach (ListViewItem lvi in lvUploaders.Items)
            {
                lvi.SubItems[1].Text = "Waiting";
                lvi.BackColor = Color.LightYellow;
            }

            BackgroundWorker bw = new BackgroundWorker();
            bw.WorkerReportsProgress = true;
            bw.DoWork += new DoWorkEventHandler(bw_DoWork);
            bw.ProgressChanged += new ProgressChangedEventHandler(bw_ProgressChanged);
            bw.RunWorkerAsync(bw);
        }

        private void bw_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker bw = (BackgroundWorker)e.Argument;
            int index = 0;

            foreach (ImageDestType uploader in Uploaders)
            {
                WorkerTask task = new WorkerTask(WorkerTask.Jobs.UPLOAD_IMAGE);
                task.MyImageUploader = uploader;
                task.SetLocalFilePath(Tester.TestFile);
                new TaskManager(ref task).UploadImage();
                bw.ReportProgress(index++, task);
            }
        }

        private void bw_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            WorkerTask task = e.UserState as WorkerTask;

            lvUploaders.Items[e.ProgressPercentage].Tag = task;

            if (task != null && !string.IsNullOrEmpty(task.RemoteFilePath))
            {
                lvUploaders.Items[e.ProgressPercentage].BackColor = Color.LightGreen;
                lvUploaders.Items[e.ProgressPercentage].SubItems[1].Text = "Success: " + task.RemoteFilePath;
            }
            else
            {
                lvUploaders.Items[e.ProgressPercentage].BackColor = Color.LightCoral;
                lvUploaders.Items[e.ProgressPercentage].SubItems[1].Text = "Failed: " + task.ToErrorString();
            }
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            StartTest();
        }

        private void openURLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (lvUploaders.SelectedItems.Count > 0)
            {
                WorkerTask task = lvUploaders.SelectedItems[0].Tag as WorkerTask;

                if (task != null && !string.IsNullOrEmpty(task.RemoteFilePath))
                {
                    Process.Start(task.RemoteFilePath);
                }
            }
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (lvUploaders.SelectedItems.Count > 0)
            {
                List<string> urls = new List<string>();
                WorkerTask task;

                foreach (ListViewItem lvi in lvUploaders.SelectedItems)
                {
                    task = lvi.Tag as WorkerTask;
                    if (task != null && !string.IsNullOrEmpty(task.RemoteFilePath))
                    {
                        urls.Add(string.Format("{0}: {1}", task.MyImageUploader.GetDescription(), task.RemoteFilePath));
                    }
                }

                if (urls.Count > 0)
                {
                    Clipboard.SetText(string.Join("\r\n", urls.ToArray()));
                }
            }
        }
    }
}
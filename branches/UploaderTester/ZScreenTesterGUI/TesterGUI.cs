using System;
using System.Windows.Forms;
using UploadersLib;
using System.Linq;
using ZScreenLib;
using System.Drawing;
using System.ComponentModel;
using System.Collections.Generic;

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
                ListViewItem lvi = new ListViewItem(uploader.GetDescription());
                lvi.SubItems.Add("Waiting");
                lvi.BackColor = Color.LightYellow;
                lvi.Tag = uploader;
                lvUploaders.Items.Add(lvi);
            }

            StartTest();
        }

        public void StartTest()
        {
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
    }
}
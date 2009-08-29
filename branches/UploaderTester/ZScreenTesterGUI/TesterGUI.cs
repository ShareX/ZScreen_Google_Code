using System;
using System.Windows.Forms;
using UploadersLib;
using System.Linq;
using ZScreenLib;
using System.Drawing;
using System.ComponentModel;

namespace ZScreenTesterGUI
{
    public partial class TesterGUI : Form
    {
        public TesterGUI()
        {
            InitializeComponent();

            foreach (ImageDestType uploader in Enum.GetValues(typeof(UploadersLib.ImageDestType)))
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
            foreach (ImageDestType uploader in Enum.GetValues(typeof(UploadersLib.ImageDestType)))
            {
                WorkerTask task = new WorkerTask(WorkerTask.Jobs.UPLOAD_IMAGE);
                task.MyImageUploader = uploader;
                task.SetLocalFilePath(Tester.TestFile);
                if (uploader == ImageDestType.TWITSNAPS) continue;
                new TaskManager(ref task).UploadImage();
                bw.ReportProgress(index++, task.RemoteFilePath);
            }
        }

        private void bw_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            string url = e.UserState as string;
            if (string.IsNullOrEmpty(url))
            {
                lvUploaders.Items[e.ProgressPercentage].BackColor = Color.IndianRed;
                lvUploaders.Items[e.ProgressPercentage].SubItems[1].Text = "Failed";
            }
            else
            {
                lvUploaders.Items[e.ProgressPercentage].BackColor = Color.LightGreen;
                lvUploaders.Items[e.ProgressPercentage].SubItems[1].Text = "Success: " + url;
            }
        }
    }
}
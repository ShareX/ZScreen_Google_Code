using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using UploadersLib;
using ZScreenLib;
using UploadersLib.Helpers;

namespace ZScreenTesterGUI
{
    public partial class TesterGUI : Form
    {
        public enum UploaderType
        {
            None,
            ImageUploader,
            TextUploader,
            FileUploader,
            UrlShortener
        }

        public class UploaderInfo
        {
            public UploaderType UploaderType;
            public ImageDestType ImageUploader;
            public TextDestType TextUploader;
            public FileUploaderType FileUploader;
            public UrlShortenerType UrlShortener;
            public WorkerTask Task;
        }

        public UploaderInfo[] Uploaders;

        public TesterGUI()
        {
            InitializeComponent();

            MyConsole myConsole = new MyConsole();
            myConsole.ConsoleWriteLine += new MyConsole.ConsoleEventHandler(myConsole_ConsoleWriteLine);
            Console.SetOut(myConsole);

            ListViewItem lvi;

            foreach (ImageDestType uploader in Enum.GetValues(typeof(ImageDestType)))
            {
                switch (uploader)
                {
                    case ImageDestType.CLIPBOARD:
                    case ImageDestType.FILE:
                    case ImageDestType.PRINTER:
                    case ImageDestType.TWITSNAPS: // Not possible to upload without post Twitter
                        continue;
                }

                lvi = new ListViewItem(uploader.GetDescription());
                lvi.Tag = new UploaderInfo { UploaderType = UploaderType.ImageUploader, ImageUploader = uploader };
                lvUploaders.Items.Add(lvi);
            }

            foreach (TextDestType uploader in Enum.GetValues(typeof(TextDestType)))
            {
                lvi = new ListViewItem(uploader.GetDescription());
                lvi.Tag = new UploaderInfo { UploaderType = UploaderType.TextUploader, TextUploader = uploader };
                lvUploaders.Items.Add(lvi);
            }

            foreach (FileUploaderType uploader in Enum.GetValues(typeof(FileUploaderType)))
            {
                lvi = new ListViewItem(uploader.GetDescription());
                lvi.Tag = new UploaderInfo { UploaderType = UploaderType.FileUploader, FileUploader = uploader };
                lvUploaders.Items.Add(lvi);
            }

            foreach (UrlShortenerType uploader in Enum.GetValues(typeof(UrlShortenerType)))
            {
                lvi = new ListViewItem(uploader.GetDescription());
                lvi.Tag = new UploaderInfo { UploaderType = UploaderType.UrlShortener, UrlShortener = uploader };
                lvUploaders.Items.Add(lvi);
            }

            Uploaders = lvUploaders.Items.OfType<ListViewItem>().Select(x => x.Tag as UploaderInfo).ToArray();

            if (!File.Exists(Tester.TestFilePicture))
            {
                OpenFileDialog dlg = new OpenFileDialog();
                dlg.Title = "Browse for a test file...";

                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    Tester.TestFilePicture = dlg.FileName;
                }
            }
        }

        private void myConsole_ConsoleWriteLine(string value)
        {
            this.Invoke(new MethodInvoker(delegate
                {
                    txtConsole.AppendText(value);
                }));
        }

        public void StartTest()
        {
            foreach (ListViewItem lvi in lvUploaders.Items)
            {
                if (lvi.SubItems.Count < 2)
                {
                    lvi.SubItems.Add("");
                }

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

            for (int i = 0; i < Uploaders.Length; i++)
            {
                if (Uploaders[i] != null)
                {
                    WorkerTask task = new WorkerTask(WorkerTask.Jobs.UploadFromClipboard);

                    switch (Uploaders[i].UploaderType)
                    {
                        case UploaderType.ImageUploader:
                            task.MyImageUploader = Uploaders[i].ImageUploader;
                            task.SetLocalFilePath(Tester.TestFilePicture);
                            new TaskManager(ref task).UploadImage();
                            break;
                        case UploaderType.FileUploader:
                            task.MyFileUploader = Uploaders[i].FileUploader;
                            task.SetLocalFilePath(Tester.TestFileBinary);
                            new TaskManager(ref task).UploadFile();
                            break;
                        case UploaderType.TextUploader:
                            task.MyTextUploader = Adapter.FindTextUploader(Uploaders[i].TextUploader.GetDescription());
                            task.MyText = TextInfo.FromString(task.MyTextUploader.TesterString);
                            new TaskManager(ref task).UploadText();
                            break;
                        case UploaderType.UrlShortener:
                            task.MyTextUploader = Adapter.FindUrlShortener(Uploaders[i].UrlShortener.GetDescription());
                            task.MyText = TextInfo.FromString(task.MyTextUploader.TesterString);
                            new TaskManager(ref task).UploadText();
                            break;
                        default:
                            throw new Exception("Unknown uploader.");
                    }

                    Uploaders[i].Task = task;

                    bw.ReportProgress(i, Uploaders[i]);
                }
            }
        }

        private void bw_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            UploaderInfo uploader = e.UserState as UploaderInfo;

            lvUploaders.Items[e.ProgressPercentage].Tag = uploader;

            if (uploader != null && uploader.Task != null && !string.IsNullOrEmpty(uploader.Task.RemoteFilePath))
            {
                lvUploaders.Items[e.ProgressPercentage].BackColor = Color.LightGreen;
                lvUploaders.Items[e.ProgressPercentage].SubItems[1].Text = "Success: " + uploader.Task.RemoteFilePath;
            }
            else
            {
                lvUploaders.Items[e.ProgressPercentage].BackColor = Color.LightCoral;
                lvUploaders.Items[e.ProgressPercentage].SubItems[1].Text = "Failed: " + uploader.Task.ToErrorString();
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
                UploaderInfo uploader = lvUploaders.SelectedItems[0].Tag as UploaderInfo;

                if (uploader != null && uploader.Task != null && !string.IsNullOrEmpty(uploader.Task.RemoteFilePath))
                {
                    Process.Start(uploader.Task.RemoteFilePath);
                }
            }
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (lvUploaders.SelectedItems.Count > 0)
            {
                List<string> urls = new List<string>();
                UploaderInfo uploader;

                foreach (ListViewItem lvi in lvUploaders.SelectedItems)
                {
                    uploader = lvi.Tag as UploaderInfo;
                    if (uploader != null && uploader.Task != null && !string.IsNullOrEmpty(uploader.Task.RemoteFilePath))
                    {
                        urls.Add(string.Format("{0}: {1}", "TODO", uploader.Task.RemoteFilePath));
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
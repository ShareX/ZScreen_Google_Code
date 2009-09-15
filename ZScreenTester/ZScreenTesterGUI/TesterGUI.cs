using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using UploadersLib;
using UploadersLib.Helpers;
using ZScreenLib;

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
            public int Index;
        }

        public bool Testing
        {
            get { return isTesting; }
            set
            {
                isTesting = value;
                btnTestAll.Enabled = !value;
                btnTestSelected.Enabled = !value;
                testSelectedUploadersToolStripMenuItem.Enabled = !value;
            }
        }

        public string TestFileBinaryPath { get; set; }
        public string TestFilePicturePath { get; set; }
        public string TestFileTextPath { get; set; }

        private bool isTesting = false;

        public TesterGUI()
        {
            InitializeComponent();

            MyConsole myConsole = new MyConsole();
            myConsole.ConsoleWriteLine += new MyConsole.ConsoleEventHandler(myConsole_ConsoleWriteLine);
            Console.SetOut(myConsole);

            ListViewItem lvi;

            ListViewGroup imageUploadersGroup = new ListViewGroup("Image Uploaders", HorizontalAlignment.Left);
            ListViewGroup textUploadersGroup = new ListViewGroup("Text Uploaders", HorizontalAlignment.Left);
            ListViewGroup fileUploadersGroup = new ListViewGroup("File Uploaders", HorizontalAlignment.Left);
            ListViewGroup urlShortenersGroup = new ListViewGroup("URL Shorteners", HorizontalAlignment.Left);
            lvUploaders.Groups.AddRange(new[] { imageUploadersGroup, textUploadersGroup, fileUploadersGroup, urlShortenersGroup });

            foreach (ImageDestType uploader in Enum.GetValues(typeof(ImageDestType)))
            {
                switch (uploader)
                {
                    case ImageDestType.CLIPBOARD:
                    case ImageDestType.FILE:
                    case ImageDestType.PRINTER:
                    case ImageDestType.TWITSNAPS: // Not possible to upload without post Twitter
                    case ImageDestType.FileUploader: // We are going to test this in File Uploader tests
                        continue;
                }

                lvi = new ListViewItem(uploader.GetDescription());
                lvi.Tag = new UploaderInfo { UploaderType = UploaderType.ImageUploader, ImageUploader = uploader };
                lvi.Group = imageUploadersGroup;
                lvUploaders.Items.Add(lvi);
            }

            foreach (TextDestType uploader in Enum.GetValues(typeof(TextDestType)))
            {
                lvi = new ListViewItem(uploader.GetDescription());
                lvi.Tag = new UploaderInfo { UploaderType = UploaderType.TextUploader, TextUploader = uploader };
                lvi.Group = textUploadersGroup;
                lvUploaders.Items.Add(lvi);
            }

            foreach (FileUploaderType uploader in Enum.GetValues(typeof(FileUploaderType)))
            {
                lvi = new ListViewItem(uploader.GetDescription());
                lvi.Tag = new UploaderInfo { UploaderType = UploaderType.FileUploader, FileUploader = uploader };
                lvi.Group = fileUploadersGroup;
                lvUploaders.Items.Add(lvi);
            }

            foreach (UrlShortenerType uploader in Enum.GetValues(typeof(UrlShortenerType)))
            {
                lvi = new ListViewItem(uploader.GetDescription());
                lvi.Tag = new UploaderInfo { UploaderType = UploaderType.UrlShortener, UrlShortener = uploader };
                lvi.Group = urlShortenersGroup;
                lvUploaders.Items.Add(lvi);
            }

            PrepareListItems();
        }

        private void TesterGUI_Load(object sender, EventArgs e)
        {
            CheckPaths();
        }

        private void CheckPaths()
        {
            if (!File.Exists(this.TestFilePicturePath))
            {
                OpenFileDialog dlg = new OpenFileDialog();
                dlg.Filter = "Image Files (*.BMP;*.JPG;*.GIF;*.PNG)|*.BMP;*.JPG;*.GIF;*.PNG|All files (*.*)|*.*";
                dlg.Title = "Browse for a test image file...";

                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    this.TestFileBinaryPath = this.TestFilePicturePath = dlg.FileName;
                }
            }
        }

        private void PrepareListItems()
        {
            for (int i = 0; i < lvUploaders.Items.Count; i++)
            {
                ListViewItem lvi = lvUploaders.Items[i];

                if (lvi.SubItems.Count < 2)
                {
                    lvi.SubItems.Add("");
                }

                lvi.SubItems[1].Text = "Waiting";
                lvi.BackColor = Color.LightYellow;

                UploaderInfo uploadInfo = lvi.Tag as UploaderInfo;
                if (uploadInfo != null)
                {
                    uploadInfo.Index = i;
                }
            }
        }

        private void myConsole_ConsoleWriteLine(string value)
        {
            if (!this.IsDisposed)
            {
                this.Invoke(new MethodInvoker(delegate
                    {
                        txtConsole.AppendText(value);
                    }));
            }
        }

        public void StartTest(UploaderInfo[] uploaders)
        {
            Testing = true;

            BackgroundWorker bw = new BackgroundWorker();
            bw.WorkerReportsProgress = true;
            bw.DoWork += new DoWorkEventHandler(bw_DoWork);
            bw.ProgressChanged += new ProgressChangedEventHandler(bw_ProgressChanged);
            bw.RunWorkerCompleted += (x, x2) => Testing = false;
            bw.RunWorkerAsync(uploaders);
        }

        private void bw_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker bw = (BackgroundWorker)sender;
            UploaderInfo[] uploaders = (UploaderInfo[])e.Argument;

            foreach (UploaderInfo uploader in uploaders)
            {
                if (this.IsDisposed || !isTesting)
                {
                    break;
                }

                if (uploader != null)
                {
                    WorkerTask task = new WorkerTask(WorkerTask.Jobs.UploadFromClipboard);

                    switch (uploader.UploaderType)
                    {
                        case UploaderType.ImageUploader:
                            task.MyImageUploader = uploader.ImageUploader;
                            task.UpdateLocalFilePath(this.TestFilePicturePath);
                            new TaskManager(ref task).UploadImage();
                            break;
                        case UploaderType.FileUploader:
                            task.MyFileUploader = uploader.FileUploader;
                            task.UpdateLocalFilePath(this.TestFileBinaryPath);
                            new TaskManager(ref task).UploadFile();
                            break;
                        case UploaderType.TextUploader:
                            task.MyTextUploader = Adapter.FindTextUploader(uploader.TextUploader.GetDescription());
                            task.MyText = TextInfo.FromString(task.MyTextUploader.TesterString);
                            new TaskManager(ref task).UploadText();
                            break;
                        case UploaderType.UrlShortener:
                            task.MyTextUploader = Adapter.FindUrlShortener(uploader.UrlShortener.GetDescription());
                            task.MyText = TextInfo.FromString(task.MyTextUploader.TesterString);
                            new TaskManager(ref task).UploadText();
                            break;
                        default:
                            throw new Exception("Unknown uploader.");
                    }

                    uploader.Task = task;

                    bw.ReportProgress(uploader.Index, uploader);
                }
            }
        }

        private void bw_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (!this.IsDisposed)
            {
                UploaderInfo uploader = e.UserState as UploaderInfo;

                lvUploaders.Items[uploader.Index].Tag = uploader;

                if (uploader != null && uploader.Task != null && !string.IsNullOrEmpty(uploader.Task.RemoteFilePath))
                {
                    lvUploaders.Items[uploader.Index].BackColor = Color.LightGreen;
                    lvUploaders.Items[uploader.Index].SubItems[1].Text = "Success: " + uploader.Task.RemoteFilePath;
                }
                else
                {
                    lvUploaders.Items[uploader.Index].BackColor = Color.LightCoral;
                    lvUploaders.Items[uploader.Index].SubItems[1].Text = "Failed: " + uploader.Task.ToErrorString();
                }
            }
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
                        urls.Add(string.Format("{0}: {1}", lvi.Text, uploader.Task.RemoteFilePath));
                    }
                }

                if (urls.Count > 0)
                {
                    Clipboard.SetText(string.Join("\r\n", urls.ToArray()));
                }
            }
        }

        private void TesterGUI_FormClosing(object sender, FormClosingEventArgs e)
        {
            isTesting = false;
        }

        private void btnTestAll_Click(object sender, EventArgs e)
        {
            UploaderInfo[] uploaders = lvUploaders.Items.Cast<ListViewItem>().Select(x => x.Tag as UploaderInfo).ToArray();
            StartTest(uploaders);
        }

        private void btnTestSelected_Click(object sender, EventArgs e)
        {
            UploaderInfo[] uploaders = lvUploaders.SelectedItems.Cast<ListViewItem>().Select(x => x.Tag as UploaderInfo).ToArray();
            StartTest(uploaders);
        }
    }
}
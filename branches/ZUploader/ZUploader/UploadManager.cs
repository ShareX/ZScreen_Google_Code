using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using UploadersLib;
using ZUploader.Properties;
using System.Text;

namespace ZUploader
{
    public static class UploadManager
    {
        public static ImageDestType2 ImageUploader { get; set; }
        public static TextDestType2 TextUploader { get; set; }
        public static FileUploaderType2 FileUploader { get; set; }
        public static ListView ListViewControl { get; set; }

        private static List<Task> Tasks = new List<Task>();
        private static int ID;

        public static int GetID()
        {
            return ID++;
        }

        public static void Upload(string path)
        {
            Task task;
            string fileName = Path.GetFileName(path);

            using (Stream stream = File.Open(path, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                if (TextUploader != TextDestType2.FILE && Helpers.IsValidTextFile(path))
                {
                    task = new Task(EDataType.Text, stream, fileName);
                }
                else if (ImageUploader != ImageDestType2.FILE && Helpers.IsValidImageFile(stream))
                {
                    task = new Task(EDataType.Image, stream, fileName);
                }
                else
                {
                    task = new Task(EDataType.File, stream, fileName);
                }
            }

            StartUpload(task);
        }

        public static void Upload(string[] files)
        {
            foreach (string file in files)
            {
                if (!string.IsNullOrEmpty(file))
                {
                    if (File.Exists(file))
                    {
                        Upload(file);
                    }
                    else if (Directory.Exists(file))
                    {
                        string[] files2 = Directory.GetFiles(file, "*.*", SearchOption.AllDirectories);

                        Upload(files2);
                    }
                }
            }
        }

        public static void ClipboardUpload()
        {
            if (Clipboard.ContainsImage())
            {
                using (Image img = Clipboard.GetImage())
                using (MemoryStream stream = new MemoryStream())
                {
                    img.Save(stream, ImageFormat.Png);
                    Task task;
                    string fileName = Helpers.GetRandomAlphanumeric(10) + ".png";

                    if (ImageUploader == ImageDestType2.FILE)
                    {
                        task = new Task(EDataType.File, stream, fileName);
                    }
                    else
                    {
                        task = new Task(EDataType.Image, stream, fileName);
                    }

                    StartUpload(task);
                }
            }
            else if (Clipboard.ContainsText())
            {
                byte[] byteArray = Encoding.UTF8.GetBytes(Clipboard.GetText());
                using (MemoryStream stream = new MemoryStream(byteArray))
                {
                    Task task;
                    string fileName = Helpers.GetRandomAlphanumeric(10) + ".txt";

                    if (TextUploader == TextDestType2.FILE)
                    {
                        task = new Task(EDataType.File, stream, fileName);
                    }
                    else
                    {
                        task = new Task(EDataType.Text, stream, fileName);
                    }

                    StartUpload(task);
                }
            }
            else if (Clipboard.ContainsFileDropList())
            {
                string[] files = Clipboard.GetFileDropList().Cast<string>().ToArray();
                Upload(files);
            }
        }

        private static void StartUpload(Task task)
        {
            Tasks.Add(task);
            task.UploadStarted += new Task.UploadStartedEventHandler(task_UploadStarted);
            task.UploadCompleted += new Task.UploadCompletedEventHandler(task_UploadCompleted);
            task.Start();
        }

        private static void task_UploadStarted(Task sender)
        {
            if (ListViewControl != null)
            {
                ListViewItem lvi = new ListViewItem();
                lvi.Text = sender.TaskID.ToString();
                lvi.SubItems.Add("Upload started: " + sender.DataManager.FileType.ToString());
                lvi.SubItems.Add(string.Empty);
                ListViewControl.Items.Add(lvi);
            }
        }

        private static void task_UploadCompleted(Task sender, Task.UploadCompletedEventArgs e)
        {
            if (ListViewControl != null)
            {
                ListViewItem lvi = ListViewControl.Items[sender.TaskID];
                lvi.SubItems[1].Text = "Upload completed";
                lvi.SubItems[2].Text = e.URL;
                lvi.EnsureVisible();

                if (Settings.Default.ClipboardAutoCopy && !string.IsNullOrEmpty(e.URL))
                {
                    Clipboard.SetText(e.URL);
                }

                Tasks.Remove(sender);
                sender.Dispose();
            }
        }
    }
}
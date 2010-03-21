using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using UploadersLib;

namespace ZUploader
{
    public static class UploadManager
    {
        public static ImageDestType2 ImageUploader { get; set; }
        public static ListView ListViewControl { get; set; }

        private static List<Task> Tasks = new List<Task>();
        private static int ID;

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
                lvi.SubItems.Add("Upload started");
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
            }
        }

        public static int GetID()
        {
            return ID++;
        }

        public static void DoClipboardUpload()
        {
            if (Clipboard.ContainsImage())
            {
                StartUpload(new Task(Clipboard.GetImage()));
            }
            else if (Clipboard.ContainsText())
            {
                StartUpload(new Task(Clipboard.GetText()));
            }
            else if (Clipboard.ContainsFileDropList())
            {
                StringCollection files = Clipboard.GetFileDropList();

                foreach (string file in files)
                {
                    StartUpload(CheckFile(file));
                }
            }
        }

        private static Task CheckFile(string path)
        {
            Task task;
            Stream stream = File.Open(path, FileMode.Open, FileAccess.Read, FileShare.Read);
            string fileName = Path.GetFileNameWithoutExtension(path);

            if (Helpers.IsValidTextFile(path))
            {
                task = new Task(EDataType.Text, stream, fileName);
            }
            else if (Helpers.IsValidImageFile(stream))
            {
                task = new Task(EDataType.Image, stream, fileName);
            }
            else
            {
                task = new Task(EDataType.Data, stream, fileName);
            }

            return task;
        }
    }
}
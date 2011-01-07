#region License Information (GPL v2)

/*
    ZUploader - A program that allows you to upload images, text or files in your clipboard
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
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Media;
using System.Windows.Forms;
using HelpersLib;
using HelpersLib.Custom_Controls;
using HistoryLib;

namespace ZUploader
{
    public static class UploadManager
    {
        public static ImageDestination ImageUploader { get; set; }
        public static TextDestination TextUploader { get; set; }
        public static FileDestination FileUploader { get; set; }
        public static MyListView ListViewControl { get; set; }
        public static List<Task> Tasks { get; private set; }

        static UploadManager()
        {
            Tasks = new List<Task>();
        }

        public static void UploadFile(string path)
        {
            if (!string.IsNullOrEmpty(path))
            {
                if (File.Exists(path))
                {
                    EDataType type;

                    if (ImageUploader != ImageDestination.FILE && Helpers.IsImageFile(path))
                    {
                        type = EDataType.Image;
                    }
                    else if (TextUploader != TextDestination.FILE && Helpers.IsTextFile(path))
                    {
                        type = EDataType.Text;
                    }
                    else
                    {
                        type = EDataType.File;
                    }

                    Task task = Task.CreateFileUploaderTask(type, path);
                    StartUpload(task);
                }
                else if (Directory.Exists(path))
                {
                    string[] files = Directory.GetFiles(path, "*.*", SearchOption.AllDirectories);
                    UploadFile(files);
                }
            }
        }

        public static void UploadFile(string[] files)
        {
            if (files != null && files.Length > 0)
            {
                foreach (string file in files)
                {
                    UploadFile(file);
                }
            }
        }

        public static void UploadFile()
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    UploadFile(ofd.FileName);
                }
            }
        }

        public static void ClipboardUpload()
        {
            if (Clipboard.ContainsImage())
            {
                Image img = Clipboard.GetImage();
                UploadImage(img);
            }
            else if (Clipboard.ContainsFileDropList())
            {
                string[] files = Clipboard.GetFileDropList().Cast<string>().ToArray();
                UploadFile(files);
            }
            else if (Clipboard.ContainsText())
            {
                string text = Clipboard.GetText();
                UploadText(text);
            }
        }

        public static void DragDropUpload(IDataObject data)
        {
            if (data.GetDataPresent(DataFormats.FileDrop, false))
            {
                string[] files = data.GetData(DataFormats.FileDrop, false) as string[];
                UploadFile(files);
            }
            else if (data.GetDataPresent(DataFormats.Bitmap, false))
            {
                Image img = data.GetData(DataFormats.Bitmap, false) as Image;
                UploadImage(img);
            }
            else if (data.GetDataPresent(DataFormats.Text, false))
            {
                string text = data.GetData(DataFormats.Text, false) as string;
                UploadText(text);
            }
        }

        public static void UploadImage(Image img)
        {
            if (img != null)
            {
                EDataType type = ImageUploader == ImageDestination.FILE ? EDataType.File : EDataType.Image;
                Task task = Task.CreateImageUploaderTask(type, img);
                StartUpload(task);
            }
        }

        public static void UploadText(string text)
        {
            if (!string.IsNullOrEmpty(text))
            {
                EDataType type = TextUploader == TextDestination.FILE ? EDataType.File : EDataType.Text;
                Task task = Task.CreateTextUploaderTask(type, text);
                StartUpload(task);
            }
        }

        private static void StartUpload(Task task)
        {
            Tasks.Add(task);
            task.Info.ID = Tasks.Count - 1;
            task.UploadPreparing += new Task.TaskEventHandler(task_UploadPreparing);
            task.UploadStarted += new Task.TaskEventHandler(task_UploadStarted);
            task.UploadProgressChanged += new Task.TaskEventHandler(task_UploadProgressChanged);
            task.UploadCompleted += new Task.TaskEventHandler(task_UploadCompleted);
            CreateListViewItem(task.Info);
            StartTasks();
        }

        private static void StartTasks()
        {
            int workingTasksCount = Tasks.Count(x => x.IsWorking);
            Task[] inQueueTasks = Tasks.Where(x => x.Status == TaskStatus.InQueue).ToArray();

            if (inQueueTasks.Length > 0)
            {
                int len;

                if (Program.Settings.UploadLimit == 0)
                {
                    len = inQueueTasks.Length;
                }
                else
                {
                    len = (Program.Settings.UploadLimit - workingTasksCount).Between(0, inQueueTasks.Length);
                }

                for (int i = 0; i < len; i++)
                {
                    inQueueTasks[i].Start();
                }
            }
        }

        public static void StopUpload(int index)
        {
            if (Tasks.Count < index)
            {
                Tasks[index].Stop();
            }
        }

        private static void ChangeListViewItemStatus(UploadInfo info)
        {
            if (ListViewControl != null)
            {
                ListViewItem lvi = ListViewControl.Items[info.ID];
                lvi.SubItems[1].Text = info.Status;
            }
        }

        private static void CreateListViewItem(UploadInfo info)
        {
            if (ListViewControl != null)
            {
                Program.MyLogger.WriteLine("Upload in queue. ID: {0}, Job: {1}, Type: {2}, Host: {3}", info.ID, info.Job, info.UploaderType, info.UploaderHost);

                ListViewItem lvi = new ListViewItem();
                lvi.Text = info.FileName;
                lvi.SubItems.Add("In queue");
                lvi.SubItems.Add(string.Empty);
                lvi.SubItems.Add(string.Empty);
                lvi.SubItems.Add(string.Empty);
                lvi.SubItems.Add(string.Empty);
                lvi.SubItems.Add(info.UploaderType.ToString());
                lvi.SubItems.Add(info.UploaderHost);
                lvi.SubItems.Add(string.Empty);
                lvi.BackColor = info.ID % 2 == 0 ? Color.White : Color.WhiteSmoke;
                lvi.ImageIndex = 3;
                ListViewControl.Items.Add(lvi);
                lvi.EnsureVisible();
                ListViewControl.AutoResizeLastColumn();
            }
        }

        private static void task_UploadPreparing(UploadInfo info)
        {
            Program.MyLogger.WriteLine("Upload preparing. ID: {0}", info.ID);
            ChangeListViewItemStatus(info);
        }

        private static void task_UploadStarted(UploadInfo info)
        {
            string status = string.Format("Upload started. ID: {0}, Filename: {1}", info.ID, info.FileName);
            if (!string.IsNullOrEmpty(info.FilePath)) status += ", Filepath: " + info.FilePath;
            Program.MyLogger.WriteLine(status);

            ListViewItem lvi = ListViewControl.Items[info.ID];
            lvi.Text = info.FileName;
            lvi.SubItems[1].Text = info.Status;
            lvi.ImageIndex = 0;
        }

        private static void task_UploadProgressChanged(UploadInfo info)
        {
            if (ListViewControl != null)
            {
                ListViewItem lvi = ListViewControl.Items[info.ID];
                lvi.SubItems[2].Text = string.Format("{0:N0}%  {1:N0} kB / {2:N0} kB", info.Progress.Percentage,
                    info.Progress.Position / 1000, info.Progress.Length / 1000);
                lvi.SubItems[3].Text = string.Format("{0:N0} kB/s", info.Progress.Speed);
                lvi.SubItems[4].Text = string.Format("{0:00}:{1:00}", info.Progress.Elapsed.Minutes, info.Progress.Elapsed.Seconds);
                lvi.SubItems[5].Text = string.Format("{0:00}:{1:00}", info.Progress.Remaining.Minutes, info.Progress.Remaining.Seconds);
            }
        }

        private static void task_UploadCompleted(UploadInfo info)
        {
            try
            {
                if (ListViewControl != null && info != null && info.Result != null)
                {
                    ListViewItem lvi = ListViewControl.Items[info.ID];
                    lvi.Tag = info.Result;

                    if (info.Result.Errors != null && info.Result.Errors.Count > 0)
                    {
                        string errors = string.Join("\r\n\r\n", info.Result.Errors.ToArray());

                        Program.MyLogger.WriteLine("Upload failed. ID: {0}, Filename: {1}, Errors:\r\n{2}", info.ID, info.FileName, errors);

                        lvi.SubItems[1].Text = "Error";
                        lvi.SubItems[8].Text = string.Empty;
                        lvi.ImageIndex = 1;
                    }
                    else
                    {
                        Program.MyLogger.WriteLine("Upload completed. ID: {0}, Filename: {1}, URL: {2}, Duration: {3}ms", info.ID, info.FileName,
                            info.Result.URL, (int)info.UploadDuration.TotalMilliseconds);

                        lvi.SubItems[1].Text = info.Status;
                        lvi.SubItems[8].Text = info.Result.URL;
                        lvi.ImageIndex = 2;
                    }

                    lvi.EnsureVisible();

                    if (Program.Settings.ClipboardAutoCopy && !string.IsNullOrEmpty(info.Result.URL))
                    {
                        Helpers.CopyTextSafely(info.Result.URL);
                    }

                    if (Program.Settings.AutoPlaySound)
                    {
                        SystemSounds.Exclamation.Play();
                    }

                    if (Program.Settings.SaveHistory && !string.IsNullOrEmpty(info.Result.URL) && (info.Result.Errors == null || info.Result.Errors.Count == 0))
                    {
                        HistoryManager.AutomaticlyAddHistoryItemAsync(Program.HistoryFilePath, info.GetHistoryItem());
                    }
                }
            }
            finally
            {
                StartTasks();
            }
        }
    }
}
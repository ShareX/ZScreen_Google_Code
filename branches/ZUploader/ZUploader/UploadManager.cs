#region License Information (GPL v2)
/*
    ZUploader - A program that allows you to upload images, text or files in your clipboard
    Copyright (C) 2010 ZScreen Developers

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
#endregion

using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Media;
using System.Text;
using System.Windows.Forms;

namespace ZUploader
{
    public static class UploadManager
    {
        public static ImageDestType2 ImageUploader { get; set; }
        public static TextDestType2 TextUploader { get; set; }
        public static FileUploaderType2 FileUploader { get; set; }
        public static ListView ListViewControl { get; set; }

        private static int ID;

        public static int GetID()
        {
            return ID++;
        }

        public static void Upload(string filePath)
        {
            EDataType type;

            if (TextUploader != TextDestType2.FILE && Helpers.IsValidTextFile(filePath))
            {
                type = EDataType.Text;
            }
            else if (ImageUploader != ImageDestType2.FILE && Helpers.IsValidImageFile(filePath))
            {
                type = EDataType.Image;
            }
            else
            {
                type = EDataType.File;
            }

            Task task = new Task(type, filePath);
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
                {
                    MemoryStream stream = new MemoryStream();
                    img.SaveJPG100(stream);
                    string fileName = Helpers.GetRandomAlphanumeric(10) + ".jpg";
                    EDataType type = ImageUploader == ImageDestType2.FILE ? EDataType.File : EDataType.Image;
                    Task task = new Task(type, stream, fileName);
                    StartUpload(task);
                }
            }
            else if (Clipboard.ContainsText())
            {
                byte[] byteArray = Encoding.UTF8.GetBytes(Clipboard.GetText());
                MemoryStream stream = new MemoryStream(byteArray);
                string fileName = Helpers.GetRandomAlphanumeric(10) + ".txt";
                EDataType type = TextUploader == TextDestType2.FILE ? EDataType.File : EDataType.Text;
                Task task = new Task(type, stream, fileName);
                StartUpload(task);
            }
            else if (Clipboard.ContainsFileDropList())
            {
                string[] files = Clipboard.GetFileDropList().Cast<string>().ToArray();
                Upload(files);
            }
        }

        private static void StartUpload(Task task)
        {
            task.UploadStarted += new Task.UploadStartedEventHandler(task_UploadStarted);
            task.UploadProgressChanged += new Task.UploadProgressChangedEventHandler(task_UploadProgressChanged);
            task.UploadCompleted += new Task.UploadCompletedEventHandler(task_UploadCompleted);
            task.Start();
        }

        private static void task_UploadStarted(Task sender)
        {
            if (ListViewControl != null)
            {
                ListViewItem lvi = new ListViewItem();
                lvi.Text = sender.ID.ToString();
                lvi.SubItems.Add("Upload started: " + sender.DataManager.FileType.ToString());
                lvi.SubItems.Add(string.Empty);
                ListViewControl.Items.Add(lvi);
            }
        }

        private static void task_UploadProgressChanged(Task sender, int progress)
        {
            if (ListViewControl != null)
            {
                ListViewItem lvi = ListViewControl.Items[sender.ID];
                lvi.SubItems[1].Text = string.Format("Upload progress: {0}%", progress);
            }
        }

        private static void task_UploadCompleted(Task sender, UploadResult result)
        {
            if (ListViewControl != null)
            {
                ListViewItem lvi = ListViewControl.Items[sender.ID];
                lvi.Tag = result;

                if (result.Errors != null && result.Errors.Count > 0)
                {
                    lvi.SubItems[1].Text = "Error: " + result.Errors.Last();
                    lvi.SubItems[2].Text = string.Empty;
                }
                else
                {
                    lvi.SubItems[1].Text = "Upload completed";
                    lvi.SubItems[2].Text = result.URL;
                }

                lvi.EnsureVisible();

                if (Program.Settings.ClipboardAutoCopy && !string.IsNullOrEmpty(result.URL))
                {
                    Clipboard.SetText(result.URL);
                }

                if (Program.Settings.AutoPlaySound)
                {
                    SystemSounds.Exclamation.Play();
                }

                sender.Dispose();
            }
        }
    }
}
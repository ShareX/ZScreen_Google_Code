using System;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;
using UploadersLib;
using ZUploader.Properties;

namespace ZUploader
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            UpdateForm();
            UploadManager.ListViewControl = lvUploads;
            if (Settings.Default.FTPAccount == null)
            {
                Settings.Default.FTPAccount = new FTPAccount();
            }
            pgFTPAccount.SelectedObject = Settings.Default.FTPAccount;
            pgApp.SelectedObject = Settings.Default;
        }

        private void UpdateForm()
        {
            cbImageUploaderDestination.Items.AddRange(typeof(ImageDestType2).GetDescriptions());
            cbImageUploaderDestination.SelectedIndex = Settings.Default.SelectedImageUploaderDestination;
            cbTextUploaderDestination.Items.AddRange(typeof(TextDestType2).GetDescriptions());
            cbTextUploaderDestination.SelectedIndex = Settings.Default.SelectedTextUploaderDestination;
            cbFileUploaderDestination.Items.AddRange(typeof(FileUploaderType2).GetDescriptions());
            cbFileUploaderDestination.SelectedIndex = Settings.Default.SelectedFileUploaderDestination;
        }

        private void CopyURL()
        {
            if (lvUploads.SelectedItems.Count > 0)
            {
                string[] array = lvUploads.SelectedItems.Cast<ListViewItem>().Select(x => ((UploadResult)x.Tag).URL).ToArray();
                string urls = string.Join("\r\n", array);

                if (!string.IsNullOrEmpty(urls))
                {
                    Clipboard.SetText(urls);
                }
            }
        }

        private void CopyThumbnailURL()
        {
            if (lvUploads.SelectedItems.Count == 1)
            {
                UploadResult result = lvUploads.SelectedItems[0].Tag as UploadResult;

                if (result != null && !string.IsNullOrEmpty(result.ThumbnailURL))
                {
                    Clipboard.SetText(result.ThumbnailURL);
                }
            }
        }

        private void CopyDeletionURL()
        {
            if (lvUploads.SelectedItems.Count == 1)
            {
                UploadResult result = lvUploads.SelectedItems[0].Tag as UploadResult;

                if (result != null && !string.IsNullOrEmpty(result.DeletionURL))
                {
                    Clipboard.SetText(result.DeletionURL);
                }
            }
        }

        private void OpenURL()
        {
            if (lvUploads.SelectedItems.Count > 0)
            {
                string url = lvUploads.SelectedItems[0].SubItems[2].Text;

                if (!string.IsNullOrEmpty(url))
                {
                    Process.Start(url);
                }
            }
        }

        #region Form events

        private void btnClipboardUpload_Click(object sender, EventArgs e)
        {
            UploadManager.ClipboardUpload();
        }

        private void cbImageUploaderDestination_SelectedIndexChanged(object sender, EventArgs e)
        {
            Settings.Default.SelectedImageUploaderDestination = cbImageUploaderDestination.SelectedIndex;
            UploadManager.ImageUploader = (ImageDestType2)cbImageUploaderDestination.SelectedIndex;
        }

        private void cbTextUploaderDestination_SelectedIndexChanged(object sender, EventArgs e)
        {
            Settings.Default.SelectedTextUploaderDestination = cbTextUploaderDestination.SelectedIndex;
            UploadManager.TextUploader = (TextDestType2)cbTextUploaderDestination.SelectedIndex;
        }

        private void cbFileUploaderDestination_SelectedIndexChanged(object sender, EventArgs e)
        {
            Settings.Default.SelectedFileUploaderDestination = cbFileUploaderDestination.SelectedIndex;
            UploadManager.FileUploader = (FileUploaderType2)cbFileUploaderDestination.SelectedIndex;
        }

        private void btnCopy_Click(object sender, EventArgs e)
        {
            CopyURL();
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            OpenURL();
        }

        private void openURLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenURL();
        }

        private void copyURLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CopyURL();
        }

        private void copyThumbnailURLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CopyThumbnailURL();
        }

        private void copyDeletionURLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CopyDeletionURL();
        }

        private void lvUploads_DoubleClick(object sender, EventArgs e)
        {
            OpenURL();
        }

        private void lvUploads_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnCopy.Enabled = btnOpen.Enabled = copyURLToolStripMenuItem.Enabled =
                openURLToolStripMenuItem.Enabled = lvUploads.SelectedItems.Count > 0;

            copyThumbnailURLToolStripMenuItem.Enabled = copyDeletionURLToolStripMenuItem.Enabled = false;

            if (lvUploads.SelectedItems.Count == 1)
            {
                UploadResult result = lvUploads.SelectedItems[0].Tag as UploadResult;

                if (result != null)
                {
                    if (!string.IsNullOrEmpty(result.ThumbnailURL))
                    {
                        copyThumbnailURLToolStripMenuItem.Enabled = true;
                    }

                    if (!string.IsNullOrEmpty(result.DeletionURL))
                    {
                        copyDeletionURLToolStripMenuItem.Enabled = true;
                    }
                }
            }
        }

        private void MainForm_Resize(object sender, EventArgs e)
        {
            this.Refresh();
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Settings.Default.Save();
        }

        private void MainForm_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private void MainForm_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop, true);
            UploadManager.Upload(files);
        }

        #endregion
    }
}
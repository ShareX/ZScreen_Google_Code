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
            pgApp.SelectedObject = Settings.Default;
        }

        private void UpdateForm()
        {
            cbImageUploaderDestination.Items.AddRange(typeof(ImageDestType2).GetDescriptions());
            cbImageUploaderDestination.SelectedIndex = 0;
            cbTextUploaderDestination.Items.AddRange(typeof(TextDestType).GetDescriptions());
            cbTextUploaderDestination.SelectedIndex = 0;
            cbFileUploaderDestination.Items.AddRange(typeof(FileUploaderType2).GetDescriptions());
            cbFileUploaderDestination.SelectedIndex = 0;
        }

        private void CopyURL()
        {
            if (lvUploads.SelectedItems.Count > 0)
            {
                string[] array = lvUploads.SelectedItems.Cast<ListViewItem>().Select(x => x.SubItems[2].Text).ToArray();
                string urls = string.Join("\r\n", array);

                if (!string.IsNullOrEmpty(urls))
                {
                    Clipboard.SetText(urls);
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
            UploadManager.ImageUploader = (ImageDestType2)cbImageUploaderDestination.SelectedIndex;
        }

        private void cbTextUploaderDestination_SelectedIndexChanged(object sender, EventArgs e)
        {
            UploadManager.TextUploader = (TextDestType)cbTextUploaderDestination.SelectedIndex;
        }

        private void cbFileUploaderDestination_SelectedIndexChanged(object sender, EventArgs e)
        {
            UploadManager.FileUploader = (FileUploaderType2)cbFileUploaderDestination.SelectedIndex;
        }

        private void btnCopy_Click(object sender, EventArgs e)
        {
            CopyURL();
        }

        private void copyURLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CopyURL();
        }

        private void openURLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenURL();
        }

        private void lvUploads_DoubleClick(object sender, EventArgs e)
        {
            OpenURL();
        }

        private void lvUploads_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnCopy.Enabled = copyURLToolStripMenuItem.Enabled = openURLToolStripMenuItem.Enabled = lvUploads.SelectedItems.Count > 0;
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
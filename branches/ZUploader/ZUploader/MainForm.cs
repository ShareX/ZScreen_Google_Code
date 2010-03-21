using System;
using System.Windows.Forms;
using UploadersLib;

namespace ZUploader
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            UpdateForm();
            UploadManager.ListViewControl = lvUploads;
        }

        private void UpdateForm()
        {
            cbImageUploaderDestination.Items.AddRange(typeof(ImageDestType).GetDescriptions());
            cbImageUploaderDestination.SelectedIndex = 0;
            cbTextUploaderDestination.Items.AddRange(typeof(TextDestType).GetDescriptions());
            cbTextUploaderDestination.SelectedIndex = 0;
            cbFileUploaderDestination.Items.AddRange(typeof(FileUploaderType).GetDescriptions());
            cbFileUploaderDestination.SelectedIndex = 0;
        }

        private void btnClipboardUpload_Click(object sender, EventArgs e)
        {
            UploadManager.DoClipboardUpload();
        }

        private void cbImageUploaderDestination_SelectedIndexChanged(object sender, EventArgs e)
        {
            UploadManager.ImageUploader = (ImageDestType)cbImageUploaderDestination.SelectedIndex;
        }

        private void cbTextUploaderDestination_SelectedIndexChanged(object sender, EventArgs e)
        {
            UploadManager.TextUploader = (TextDestType)cbTextUploaderDestination.SelectedIndex;
        }

        private void cbFileUploaderDestination_SelectedIndexChanged(object sender, EventArgs e)
        {
            UploadManager.FileUploader = (FileUploaderType)cbFileUploaderDestination.SelectedIndex;
        }

        private void copyURLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (lvUploads.SelectedItems.Count > 0)
            {
                string url = lvUploads.SelectedItems[0].SubItems[2].Text;

                if (!string.IsNullOrEmpty(url))
                {
                    Clipboard.SetText(url);
                }
            }
        }
    }
}
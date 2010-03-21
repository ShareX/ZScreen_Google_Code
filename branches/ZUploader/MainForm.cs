using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using UploadersLib;

namespace ZUploader
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            cbImageUploaderDestination.Items.AddRange(typeof(ImageDestType2).GetDescriptions());
            cbImageUploaderDestination.SelectedIndex = 0;
            UploadManager.ListViewControl = lvUploads;
        }

        private void btnClipboardUpload_Click(object sender, EventArgs e)
        {
            UploadManager.DoClipboardUpload();
        }

        private void cbImageUploaderDestination_SelectedIndexChanged(object sender, EventArgs e)
        {
            UploadManager.ImageUploader = (ImageDestType2)cbImageUploaderDestination.SelectedIndex;
        }
    }
}
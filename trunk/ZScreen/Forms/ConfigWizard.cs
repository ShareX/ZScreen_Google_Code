using System;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;

namespace ZSS.Forms
{
    public partial class ConfigWizard : Form
    {

        public string RootFolder { get; private set; }
        public ImageDestType ImageDestinationType { get; private set; }

        public ConfigWizard(string rootDir)
        {
            InitializeComponent();
            txtRootFolder.Text = rootDir;
            this.RootFolder = rootDir;
            cboScreenshotDest.Items.AddRange(typeof(ImageDestType).GetDescriptions());
            cboScreenshotDest.SelectedIndex = (int)ImageDestType.IMAGESHACK;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnBrowseRootDir_Click(object sender, EventArgs e)
        {
            string oldDir = txtRootFolder.Text;
            FolderBrowserDialog dlg = new FolderBrowserDialog { ShowNewFolderButton = true };
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                txtRootFolder.Text = dlg.SelectedPath;
                RootFolder = txtRootFolder.Text;
            }
            FileSystem.MoveDirectory(oldDir, txtRootFolder.Text);
        }

        private void btnViewRootDir_Click(object sender, EventArgs e)
        {
            if (Directory.Exists(txtRootFolder.Text))
            {
                Process.Start(txtRootFolder.Text);
            }
        }

        private void cboScreenshotDest_SelectedIndexChanged(object sender, EventArgs e)
        {
            ImageDestinationType = (ImageDestType)cboScreenshotDest.SelectedIndex;
        }
    }
}
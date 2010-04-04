using System;
using System.Windows.Forms;

namespace ZUploader
{
    public partial class FTPSettingsForm : Form
    {
        public FTPSettingsForm()
        {
            InitializeComponent();
            pgFTPSettings.SelectedObject = Program.Settings.FTPAccount;
        }

        private void FTPSettingsForm_Load(object sender, EventArgs e)
        {
            this.BringToFront();
        }

        private void pgFTPSettings_SelectedObjectsChanged(object sender, EventArgs e)
        {
            pgFTPSettings.SelectedObject = Program.Settings.FTPAccount;
        }
    }
}
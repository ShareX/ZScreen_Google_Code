using System;
using System.IO;
using System.Windows.Forms;

namespace ZUploader
{
    public partial class SettingsForm : Form
    {
        private bool loaded;

        public SettingsForm()
        {
            InitializeComponent();
            LoadSettings();
        }

        private void SettingsForm_Load(object sender, EventArgs e)
        {
            this.BringToFront();
            loaded = true;
        }

        private void LoadSettings()
        {
            cbClipboardAutoCopy.Checked = Program.Settings.ClipboardAutoCopy;
            cbAutoPlaySound.Checked = Program.Settings.AutoPlaySound;
            cbShellContextMenu.Checked = ShellContextMenu.Check();

            cbHistorySave.Checked = Program.Settings.SaveHistory;
            cbUseCustomHistoryPath.Checked = Program.Settings.UseCustomHistoryPath;
            txtCustomHistoryPath.Text = Program.Settings.CustomHistoryPath;

            pgFTPSettings.SelectedObject = Program.Settings.FTPAccount;
            pgProxy.SelectedObject = Program.Settings.ProxySettings;
        }

        private void cbClipboardAutoCopy_CheckedChanged(object sender, EventArgs e)
        {
            Program.Settings.ClipboardAutoCopy = cbClipboardAutoCopy.Checked;
        }

        private void cbAutoPlaySound_CheckedChanged(object sender, EventArgs e)
        {
            Program.Settings.AutoPlaySound = cbAutoPlaySound.Checked;
        }

        private void cbShellContextMenu_CheckedChanged(object sender, EventArgs e)
        {
            if (loaded)
            {
                if (cbShellContextMenu.Checked)
                {
                    ShellContextMenu.Register();
                }
                else
                {
                    ShellContextMenu.Unregister();
                }
            }
        }

        private void cbHistorySave_CheckedChanged(object sender, EventArgs e)
        {
            Program.Settings.SaveHistory = cbHistorySave.Checked;
        }

        private void cbUseCustomHistoryPath_CheckedChanged(object sender, EventArgs e)
        {
            Program.Settings.UseCustomHistoryPath = cbUseCustomHistoryPath.Checked;
        }

        private void txtCustomHistoryPath_TextChanged(object sender, EventArgs e)
        {
            Program.Settings.CustomHistoryPath = txtCustomHistoryPath.Text;
        }

        private void btnBrowseCustomHistoryPath_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Title = "ZUploader - Custom history file path";

                try
                {
                    string text = txtCustomHistoryPath.Text;
                    if (!string.IsNullOrEmpty(text) && Directory.Exists(text = Path.GetDirectoryName(text)))
                    {
                        ofd.InitialDirectory = text;
                    }
                }
                finally
                {
                    if (string.IsNullOrEmpty(ofd.InitialDirectory))
                    {
                        ofd.InitialDirectory = Program.ZUploaderPersonalPath;
                    }
                }

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    txtCustomHistoryPath.Text = ofd.FileName;
                }
            }
        }

        private void pgFTPSettings_SelectedObjectsChanged(object sender, EventArgs e)
        {
            pgFTPSettings.SelectedObject = Program.Settings.FTPAccount;
        }
    }
}
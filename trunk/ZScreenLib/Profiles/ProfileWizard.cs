using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using UploadersLib;
using UploadersAPILib;

namespace ZScreenLib
{
    public partial class ProfileWizard : Form
    {
        public Profile Profile = null;

        public ProfileWizard(Profile profile)
            : this()
        {
            this.Profile = profile;
        }

        public ProfileWizard()
        {
            Profile = new Profile("New Profile");
            InitializeComponent();
        }

        private void ConfigGui()
        {
            this.Text = Application.ProductName + " - " + Profile.Name;
            txtName.Text = Profile.Name;

            if (cboTask.Items.Count == 0)
            {
                cboTask.Items.AddRange(typeof(WorkerTask.JobLevel2).GetDescriptions());
            }
            cboTask.SelectedIndex = (int)Profile.Job;

            chkClipboard.Checked = Profile.Outputs.Contains(OutputEnum.Clipboard);
            chkSaveFile.Checked = Profile.Outputs.Contains(OutputEnum.LocalDisk);
            chkUpload.Checked = Profile.Outputs.Contains(OutputEnum.RemoteHost);
            chkPrinter.Checked = Profile.Outputs.Contains(OutputEnum.Printer);
        }

        private void BeforeClose()
        {
            if (!string.IsNullOrEmpty(txtName.Text))
            {
                Profile.Name = txtName.Text;
            }

            Profile.Job = (WorkerTask.JobLevel2)cboTask.SelectedIndex;

            Profile.Outputs.Clear();
            if (chkClipboard.Checked)
            {
                Profile.Outputs.Add(OutputEnum.Clipboard);
            }
            if (chkSaveFile.Checked)
            {
                Profile.Outputs.Add(OutputEnum.LocalDisk);
            }
            if (chkUpload.Checked)
            {
                Profile.Outputs.Add(OutputEnum.RemoteHost);
            }
            if (chkPrinter.Checked)
            {
                Profile.Outputs.Add(OutputEnum.Printer);
            }

        }

        private void btnOutputsConfig_Click(object sender, EventArgs e)
        {
            UploadersConfigForm ocf = new UploadersConfigForm(Profile.OutputsConfig, ZKeys.GetAPIKeys());
            ocf.ShowDialog();
            Profile.OutputsConfig = ocf.Config;
        }

        private void ProfileWizard_Load(object sender, EventArgs e)
        {
            ConfigGui();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            Close();
        }

        private void ProfileWizard_FormClosing(object sender, FormClosingEventArgs e)
        {
            BeforeClose();
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            txtSaveFolder.Text = Adapter.GetDirPathUsingFolderBrowser("Browse for a folder to save files...");
        }
    }
}

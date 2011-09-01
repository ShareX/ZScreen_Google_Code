using System;
using System.Windows.Forms;
using UploadersAPILib;
using UploadersLib;

namespace ZScreenLib
{
    public partial class ProfileWizard : Form
    {
        public Workflow Workflow = null;

        public ProfileWizard(Workflow profile)
            : this()
        {
            this.Workflow = profile;
        }

        public ProfileWizard()
        {
            Workflow = new Workflow("New Profile");
            InitializeComponent();
        }

        private void ConfigGui()
        {
            this.Text = Workflow.Description + " - " + Application.ProductName;
            txtName.Text = Workflow.Description;

            if (cboTask.Items.Count == 0)
            {
                cboTask.Items.AddRange(typeof(WorkerTask.JobLevel2).GetDescriptions());
            }
            cboTask.SelectedIndex = (int)Workflow.Job;

            chkClipboard.Checked = Workflow.Outputs.Contains(OutputEnum.Clipboard);
            chkSaveFile.Checked = Workflow.Outputs.Contains(OutputEnum.LocalDisk);
            chkUpload.Checked = Workflow.Outputs.Contains(OutputEnum.RemoteHost);
            chkPrinter.Checked = Workflow.Outputs.Contains(OutputEnum.Printer);
        }

        private void BeforeClose()
        {
            if (!string.IsNullOrEmpty(txtName.Text))
            {
                Workflow.Description = txtName.Text;
            }

            Workflow.Job = (WorkerTask.JobLevel2)cboTask.SelectedIndex;

            Workflow.Outputs.Clear();
            if (chkClipboard.Checked)
            {
                Workflow.Outputs.Add(OutputEnum.Clipboard);
            }
            if (chkSaveFile.Checked)
            {
                Workflow.Outputs.Add(OutputEnum.LocalDisk);
            }
            if (chkUpload.Checked)
            {
                Workflow.Outputs.Add(OutputEnum.RemoteHost);
            }
            if (chkPrinter.Checked)
            {
                Workflow.Outputs.Add(OutputEnum.Printer);
            }
        }

        private void btnOutputsConfig_Click(object sender, EventArgs e)
        {
            UploadersConfigForm ocf = new UploadersConfigForm(Workflow.OutputsConfig, ZKeys.GetAPIKeys());
            ocf.ShowDialog();
            Workflow.OutputsConfig = ocf.Config;
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
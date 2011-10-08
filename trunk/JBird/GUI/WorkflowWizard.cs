using System;
using System.IO;
using System.Windows.Forms;
using UploadersAPILib;
using UploadersLib;
using HelpersLib.Hotkey;
using HelpersLib;
using JBirdGUI;

namespace ZScreenLib
{
    public partial class WorkflowWizard : HotkeyForm
    {
        public Workflow Workflow = null;

        public WorkflowWizard(string reason = "Create", Workflow profile = null)
        {
            InitializeComponent();
            if (profile == null) profile = new Workflow("New Workflow");
            this.Workflow = profile;
            this.Text = Application.ProductName + " - " + reason + " - " + Workflow.Description;
        }

        private void ConfigGui()
        {
            // Step 1
            txtName.Text = Workflow.Description;

            if (cboTask.Items.Count == 0)
            {
                cboTask.Items.AddRange(typeof(WorkerTask.JobLevel2).GetDescriptions());
            }
            cboTask.SelectedIndex = (int)Workflow.Job;

            HotkeyManager tempHotkeyMgr;
            Program.HotkeyMgrs.TryGetValue(this.Workflow.ID, out tempHotkeyMgr);
            if (tempHotkeyMgr != null)
            {
                hmcHotkeys.PrepareHotkeys(tempHotkeyMgr);
            }
            else
            {
                HotkeyManager hm = new HotkeyManager(Program.CoreUI, ZAppType.JBird);
                hm.AddHotkey(JBirdHotkey.Workflow, Workflow.Hotkey, Workflow.Start);
                hmcHotkeys.PrepareHotkeys(hm);
            }

            // Step 4

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
            UploadersConfigForm ocf = new UploadersConfigForm(Workflow.OutputsConfig, ZKeys.GetAPIKeys()) { Icon = this.Icon };
            ocf.ShowDialog();
            Workflow.OutputsConfig = ocf.Config;
        }

        private void WorkflowWizard_Load(object sender, EventArgs e)
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

        private void chkSaveFile_CheckedChanged(object sender, EventArgs e)
        {
            if (chkSaveFile.Checked && string.IsNullOrEmpty(txtSaveFolder.Text))
            {
                txtSaveFolder.Text = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyPictures), Application.ProductName);
            }
        }
    }
}
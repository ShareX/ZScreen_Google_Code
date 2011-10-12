﻿using System;
using System.IO;
using System.Windows.Forms;
using HelpersLib;
using HelpersLib.Hotkey;
using UploadersAPILib;
using UploadersLib;

namespace ZScreenLib
{
    public partial class WorkflowWizard : HotkeyForm
    {
        public WorkflowWizardGUIOptions GUI = new WorkflowWizardGUIOptions();
        public Workflow Config = null;

        public WorkflowWizard() { }

        public WorkflowWizard(string reason = "Create", Workflow workflow = null, WorkflowWizardGUIOptions gui = null)
        {
            InitializeComponent();
            Initialize(reason, workflow, gui);
        }

        protected void Initialize(string reason, Workflow workflow, WorkflowWizardGUIOptions gui)
        {
            if (workflow == null) workflow = new Workflow("New Workflow");
            this.Config = workflow;
            this.Text = Application.ProductName + " - " + reason + " - " + Config.Description;
            if (gui != null)
            {
                this.GUI = gui;
            }
        }

        private void WorkflowWizard_Load(object sender, EventArgs e)
        {
            ConfigGui();
        }

        private void ProfileWizard_FormClosing(object sender, FormClosingEventArgs e)
        {
            BeforeClose();
        }

        protected void ConfigGui()
        {
            // Hide/Show Tabs
            if (!GUI.ShowTabJob) this.tcMain.TabPages.Remove(tpJob);

            // Step 1
            txtName.Text = Config.Description;

            // Tasks
            chkPerformActions.Checked = Config.PerformActions;

            if (cboTask.Items.Count == 0)
            {
                cboTask.Items.AddRange(typeof(WorkerTask.JobLevel2).GetDescriptions());
            }
            cboTask.SelectedIndex = (int)Config.Job;

            // Step 4

            chkClipboard.Checked = Config.Outputs.Contains(OutputEnum.Clipboard);
            chkSaveFile.Checked = Config.Outputs.Contains(OutputEnum.LocalDisk);
            chkUpload.Checked = Config.Outputs.Contains(OutputEnum.RemoteHost);
            chkPrinter.Checked = Config.Outputs.Contains(OutputEnum.Printer);
        }

        private void BeforeClose()
        {
            // Description
            if (!string.IsNullOrEmpty(txtName.Text))
            {
                Config.Description = txtName.Text;
            }

            Config.Job = (WorkerTask.JobLevel2)cboTask.SelectedIndex;

            // Tasks
            Config.PerformActions = chkPerformActions.Checked;

            // Outputs
            Config.Outputs.Clear();
            if (chkClipboard.Checked)
            {
                Config.Outputs.Add(OutputEnum.Clipboard);
            }
            if (chkSaveFile.Checked)
            {
                Config.Outputs.Add(OutputEnum.LocalDisk);
            }
            if (chkUpload.Checked)
            {
                Config.Outputs.Add(OutputEnum.RemoteHost);
            }
            if (chkPrinter.Checked)
            {
                Config.Outputs.Add(OutputEnum.Printer);
            }
        }

        private void btnOutputsConfig_Click(object sender, EventArgs e)
        {
            UploadersConfigForm ocf = new UploadersConfigForm(Config.ConfigOutputs, ZKeys.GetAPIKeys()) { Icon = this.Icon };
            ocf.ShowDialog();
            Config.ConfigOutputs = ocf.Config;
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

        private void cboTask_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void chkTaskImageResize_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void chkTaskImageFileFormat_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void chkPerformActions_CheckedChanged(object sender, EventArgs e)
        {
            
        }
    }

    public class WorkflowWizardGUIOptions
    {
        public bool ShowTabJob { get; set; }
    }
}
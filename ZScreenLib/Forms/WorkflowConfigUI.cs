using System;
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

            tcMain.TabPages.Clear();
            if (gui != null)
            {
                this.GUI = gui;
            }
            else
            {
                tcMain.TabPages.Add(tpTasks);
            }
        }

        #region Config GUI

        protected void ConfigGui()
        {
            // Hide/Show Tabs
            if (GUI.ShowTabJob) this.tcMain.TabPages.Add(tpJob);
            if (GUI.ShowResizeTab) this.tcMain.TabPages.Add(tpImageResize);
            if (GUI.ShowQualityTab) this.tcMain.TabPages.Add(tpImageQuality);

            // Jobs
            ConfigGuiJobs();

            // Tasks
            ConfigGuiTasks();

            // Resize 
            ConfigGuiResize();

            // Quality
            ConfigGuiQuality();
        
            // Outputs
            ConfigGuiOutputs();
        }

        private void ConfigGuiJobs()
        {
            txtName.Text = Config.Description;
            if (cboTask.Items.Count == 0)
            {
                cboTask.Items.AddRange(typeof(WorkerTask.JobLevel2).GetDescriptions());
            }
            cboTask.SelectedIndex = (int)Config.Job;
        }

        private void ConfigGuiTasks()
        {
           chkPerformActions.Checked = Config.PerformActions;
        }

        private void ConfigGuiResize()
        {

        }

        private void ConfigGuiQuality()
        {
            if (cboFileFormat.Items.Count == 0)
            {
                cboFileFormat.Items.AddRange(typeof(EImageFormat).GetDescriptions());
                cboFileFormat.SelectedIndex = (int)Config.ImageFormat;
            }

            nudSwitchAfter.Value = Engine.Workflow.ImageSizeLimit;

            if (cboSwitchFormat.Items.Count == 0)
            {
                cboSwitchFormat.Items.AddRange(typeof(EImageFormat).GetDescriptions());
                cboSwitchFormat.SelectedIndex = (int)Config.ImageFormat2;
            }

            if (cboJpgQuality.Items.Count == 0)
            {
                cboJpgQuality.Items.AddRange(typeof(FreeImageJpegQualityType).GetDescriptions());
                cboJpgQuality.SelectedIndex = (int)Config.ImageJpegQuality;
            }

            if (cboJpgSubSampling.Items.Count == 0)
            {
                cboJpgSubSampling.Items.AddRange(typeof(FreeImageJpegSubSamplingType).GetDescriptions());
                cboJpgSubSampling.SelectedIndex = (int)Config.ImageJpegSubSampling;
            }

            cboGIFQuality.SelectedIndex = (int)Engine.Workflow.ImageGIFQuality;

            cboJpgQuality.Enabled = cboJpgSubSampling.Enabled = (EImageFormat)cboFileFormat.SelectedIndex == EImageFormat.JPEG ||
        (EImageFormat)cboSwitchFormat.SelectedIndex == EImageFormat.JPEG;

            cboGIFQuality.Enabled = (EImageFormat)cboFileFormat.SelectedIndex == EImageFormat.GIF ||
                (EImageFormat)cboSwitchFormat.SelectedIndex == EImageFormat.GIF;
        }

        private void ConfigGuiOutputs()
        {
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

        #endregion Config GUI

        #region Control Events

        private void WorkflowWizard_Load(object sender, EventArgs e)
        {
            ConfigGui();
        }

        private void WorkflowWizard_FormClosing(object sender, FormClosingEventArgs e)
        {
            BeforeClose();
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
            if (chkTaskImageResize.Checked)
            {
                tcMain.TabPages.Add(tpImageResize);
            }
            else
            {
                tcMain.TabPages.Remove(tpImageResize);
            }
        }

        private void chkTaskImageFileFormat_CheckedChanged(object sender, EventArgs e)
        {
            if (chkTaskImageFileFormat.Checked)
            {
                tcMain.TabPages.Add(tpImageQuality);
            }
            else
            {
                tcMain.TabPages.Remove(tpImageQuality);
            }
        }

        private void chkPerformActions_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void chkTaskOutputConfig_CheckedChanged(object sender, EventArgs e)
        {
            if (chkTaskOutputConfig.Checked)
            {
                tcMain.TabPages.Insert(tcMain.TabPages.Count, tpOutputs);
            }
            else
            {
                tcMain.TabPages.Remove(tpOutputs);
            }
        }

        private void cboFileFormat_SelectedIndexChanged(object sender, EventArgs e)
        {
            Config.ImageFormat = (EImageFormat)cboFileFormat.SelectedIndex;
        }

        private void nudSwitchAfter_ValueChanged(object sender, EventArgs e)
        {
            nudSwitchAfterValueChanged();
        }

        void nudSwitchAfter_LostFocus(object sender, System.EventArgs e)
        {
            nudSwitchAfterValueChanged();
        }

        private void nudSwitchAfterValueChanged()
        {
            Config.ImageSizeLimit = (int)nudSwitchAfter.Value;
            if ((int)nudSwitchAfter.Value == 0)
                cboSwitchFormat.Enabled = false;
            else
                cboSwitchFormat.Enabled = true;
        }

        private void cboSwitchFormat_SelectedIndexChanged(object sender, EventArgs e)
        {
            Config.ImageFormat2 = (EImageFormat)cboSwitchFormat.SelectedIndex;
        }

        private void cboJpgQuality_SelectedIndexChanged(object sender, EventArgs e)
        {
            Config.ImageJpegQuality = (FreeImageJpegQualityType)cboJpgQuality.SelectedIndex;
        }

        private void cboJpgSubSampling_SelectedIndexChanged(object sender, EventArgs e)
        {
            Config.ImageJpegSubSampling = (FreeImageJpegSubSamplingType)cboJpgSubSampling.SelectedIndex;
        }

        #endregion Control Events
    }

    public class WorkflowWizardGUIOptions
    {
        public bool ShowTabJob { get; set; }
        public bool ShowTasks { get; set; }
        public bool ShowResizeTab { get; set; }
        public bool ShowQualityTab { get; set; }
    }
}
using System;
using System.IO;
using System.Windows.Forms;
using HelpersLib;
using UploadersAPILib;
using UploadersLib;

namespace ZScreenLib
{
    public partial class WorkflowWizard : HotkeyForm
    {
        public WorkflowWizardGUIOptions GUI = new WorkflowWizardGUIOptions();
        public Workflow Config = null;
        public TaskInfo Info = new TaskInfo();

        public WorkflowWizard(TaskInfo info = null, Workflow workflow = null, WorkflowWizardGUIOptions gui = null)
        {
            InitializeComponent();
            Initialize(info, workflow, gui);
        }

        protected void Initialize(TaskInfo info, Workflow workflow, WorkflowWizardGUIOptions gui)
        {
            if (workflow == null) workflow = new Workflow("New Workflow");
            this.Config = workflow;

            this.Text = Application.ProductName + " - " + (info == null ? workflow.Description : info.Job.GetDescription());

            tcMain.TabPages.Clear();
            if (gui != null)
            {
                this.GUI = gui;
            }
            else
            {
                tcMain.TabPages.Add(tpTasks);
            }
            if (info != null)
            {
                this.Info = info;
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
            chkTaskImageAnnotate.Checked = Config.PerformActions;
            bool bDataType = ZAppHelper.IsImageFile(Info.LocalFilePath);
            chkTaskImageAnnotate.Visible = bDataType;
            chkTaskImageFileFormat.Visible = bDataType;
            chkTaskImageResize.Visible = bDataType;
        }

        private void ConfigGuiResize()
        {
            switch (Config.ImageSizeType)
            {
                case ImageSizeType.DEFAULT:
                    rbImageSizeDefault.Checked = true;
                    break;
                case ImageSizeType.FIXED:
                    rbImageSizeFixed.Checked = true;
                    break;
                case ImageSizeType.RATIO:
                    rbImageSizeRatio.Checked = true;
                    break;
            }

            nudImageSizeFixedWidth.Value = Config.ImageSizeFixedWidth;
            nudImageSizeFixedHeight.Value = Config.ImageSizeFixedHeight;
            nudImageSizeRatio.Value = (decimal)Config.ImageSizeRatioPercentage;
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

            foreach (FileUploaderType fut in Enum.GetValues(typeof(FileUploaderType)))
            {
                CheckBox chkUploader = new CheckBox()
                {
                    Text = fut.GetDescription(),
                    Checked = Config.FileUploaders.Contains(fut),
                    Tag = fut
                };
                if (Config.ConfigOutputs.IsActive(fut))
                {
                    flpFileUploaders.Controls.Add(chkUploader);
                }
            }

            if (ZAppHelper.IsImageFile(Info.LocalFilePath))
            {
                flpImageUploaders.Visible = true;
                flpTextUploaders.Visible = false;
                foreach (ImageUploaderType iut in Enum.GetValues(typeof(ImageUploaderType)))
                {
                    CheckBox chkUploader = new CheckBox()
                    {
                        Text = iut.GetDescription(),
                        Checked = Config.ImageUploaders.Contains(iut),
                        Tag = iut,
                    };
                    if (Config.ConfigOutputs.IsActive(iut))
                    {
                        flpImageUploaders.Controls.Add(chkUploader);
                    }
                }
            }
            else if (ZAppHelper.IsTextFile(Info.LocalFilePath))
            {
                flpTextUploaders.Visible = true;
                flpImageUploaders.Visible = false;
                foreach (TextUploaderType tut in Enum.GetValues(typeof(TextUploaderType)))
                {
                    CheckBox chkUploader = new CheckBox()
                    {
                        Text = tut.GetDescription(),
                        Checked = Config.TextUploaders.Contains(tut),
                        Tag = tut
                    };
                    if (Config.ConfigOutputs.IsActive(tut))
                    {
                        flpTextUploaders.Controls.Add(chkUploader);
                    }
                }
            }

            gbSaveToFile.Visible = chkSaveFile.Checked;
            txtFileNameWithoutExt.Text = Path.GetFileNameWithoutExtension(Info.LocalFilePath);
            txtSaveFolder.Text = Path.GetDirectoryName(Info.LocalFilePath);
        }

        #endregion Config GUI

        #region Helper Methods

        private void BeforeClose()
        {
            // Description
            if (!string.IsNullOrEmpty(txtName.Text))
            {
                Config.Description = txtName.Text;
            }

            Config.Job = (WorkerTask.JobLevel2)cboTask.SelectedIndex;

            // Tasks
            Config.PerformActions = chkTaskImageAnnotate.Checked;

            // Resize
            UpdateImageSize(bChangeConfig: true);

            // Outputs
            UpdateConfigOutputs();
        }

        private void UpdateImageSize(bool bChangeConfig = false)
        {
            double w2 = 0.0, h2 = 0.0, ratio = 0.0;

            if (rbImageSizeDefault.Checked)
            {
                if (bChangeConfig) Config.ImageSizeType = ImageSizeType.DEFAULT;
                ratio = 1.0;
                h2 = Info.ImageSize.Height;
            }
            else if (rbImageSizeFixed.Checked)
            {
                if (bChangeConfig) Config.ImageSizeType = ImageSizeType.FIXED;
                if (Info.ImageSize.Width > 0 && nudImageSizeFixedWidth.Value > 0)
                {
                    ratio = (double)nudImageSizeFixedWidth.Value / (double)Info.ImageSize.Width;
                    h2 = nudImageSizeFixedHeight.Value > 0 ? (double)nudImageSizeFixedHeight.Value : (double)Info.ImageSize.Height * ratio;
                }
            }
            else if (rbImageSizeRatio.Checked)
            {
                if (bChangeConfig) Config.ImageSizeType = ImageSizeType.RATIO;
                if (Info.ImageSize.Width > 0)
                {
                    ratio = (double)nudImageSizeRatio.Value / 100.0;
                    h2 = (double)Info.ImageSize.Height * ratio;
                }
            }

            if (ratio > 0.0)
            {
                w2 = (double)Info.ImageSize.Width * ratio;
                gbImageSize.Text = string.Format("Image Size - old: {0}x{1} - new: {2}x{3}",
                                  Info.ImageSize.Width, Info.ImageSize.Height, Math.Round(w2, 0), Math.Round(h2, 0));
            }
        }

        private void UpdateConfigOutputs()
        {
            Config.Outputs.Clear();
            if (chkClipboard.Checked) Config.Outputs.Add(OutputEnum.Clipboard);
            if (chkSaveFile.Checked) Config.Outputs.Add(OutputEnum.LocalDisk);
            if (chkUpload.Checked) Config.Outputs.Add(OutputEnum.RemoteHost);
            if (chkPrinter.Checked) Config.Outputs.Add(OutputEnum.Printer);

            Config.FileUploaders.Clear();
            foreach (CheckBox chk in flpFileUploaders.Controls)
            {
                FileUploaderType ut = (FileUploaderType)chk.Tag;
                if (chk.Checked) Config.FileUploaders.Add(ut);
            }

            Config.ImageUploaders.Clear();
            foreach (CheckBox chk in flpImageUploaders.Controls)
            {
                ImageUploaderType ut = (ImageUploaderType)chk.Tag;
                if (chk.Checked) Config.ImageUploaders.Add(ut);
            }

            Config.TextUploaders.Clear();
            foreach (CheckBox chk in flpTextUploaders.Controls)
            {
                TextUploaderType ut = (TextUploaderType)chk.Tag;
                if (chk.Checked) Config.TextUploaders.Add(ut);
            }

            if (!string.IsNullOrEmpty(txtFileNameWithoutExt.Text) && !string.IsNullOrEmpty(txtSaveFolder.Text))
            {
                string ext = Path.GetExtension(Info.LocalFilePath);
                Info.LocalFilePath = Path.Combine(txtSaveFolder.Text, txtFileNameWithoutExt.Text) + ext;
            }
        }

        #endregion Helper Methods

        #region Control Events

        private void WorkflowWizard_Load(object sender, EventArgs e)
        {
            ConfigGui();
        }

        private void chkTaskImageResize_CheckedChanged(object sender, EventArgs e)
        {
            if (chkTaskImageResize.Checked)
            {
                tcMain.TabPages.Insert(Math.Max(1, tcMain.TabPages.Count - 1), tpImageResize);
            }
            else
            {
                tcMain.TabPages.Remove(tpImageResize);
                Config.ImageSizeType = ImageSizeType.DEFAULT;
            }
        }

        private void cboTask_SelectedIndexChanged(object sender, EventArgs e)
        {
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
            BeforeClose();
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
                txtSaveFolder.Text = Path.GetDirectoryName(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyPictures), Application.ProductName));
            }
            gbSaveToFile.Visible = chkSaveFile.Checked;
        }

        private void chkTaskImageFileFormat_CheckedChanged(object sender, EventArgs e)
        {
            if (chkTaskImageFileFormat.Checked)
            {
                tcMain.TabPages.Insert(Math.Max(1, tcMain.TabPages.Count - 1), tpImageQuality);
            }
            else
            {
                tcMain.TabPages.Remove(tpImageQuality);
            }
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

        private void nudSwitchAfter_LostFocus(object sender, System.EventArgs e)
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

        private void rbImageSizeDefault_CheckedChanged(object sender, EventArgs e)
        {
            UpdateImageSize();
        }

        private void rbImageSizeFixed_CheckedChanged(object sender, EventArgs e)
        {
            UpdateImageSize();
        }

        private void rbImageSizeRatio_CheckedChanged(object sender, EventArgs e)
        {
            UpdateImageSize();
        }

        private void nudImageSizeRatio_ValueChanged(object sender, EventArgs e)
        {
            UpdateImageSize();
        }

        private void nudImageSizeFixedWidth_ValueChanged(object sender, EventArgs e)
        {
            UpdateImageSize();
        }

        private void nudImageSizeFixedHeight_ValueChanged(object sender, EventArgs e)
        {
            UpdateImageSize();
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
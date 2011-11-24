using System;
using System.IO;
using System.Windows.Forms;
using FreeImageNetLib;
using HelpersLib;
using UploadersAPILib;
using UploadersLib;
using ZScreenCoreLib;

namespace ZScreenLib
{
    public partial class WorkflowWizard : HotkeyForm
    {
        #region 0 Variables

        public Workflow Config = new Workflow();

        public WorkflowWizardGUIOptions GUI = new WorkflowWizardGUIOptions();

        protected WorkerTask Task = null;

        #endregion 0 Variables

        #region 1 Constructors

        public WorkflowWizard()
        {
            InitializeComponent();
        }

        public WorkflowWizard(WorkerTask info = null, WorkflowWizardGUIOptions gui = null)
            : this()
        {
            Initialize(info, gui);
        }

        #endregion 1 Constructors

        private void btnCopyImageClose_Click(object sender, EventArgs e)
        {
            Adapter.CopyImageToClipboard(this.Task.TempImage, Config.ClipboardForceBmp);
            btnCancel_Click(sender, e);
        }

        private void btnTaskAnnotate_Click(object sender, EventArgs e)
        {
            this.Task.PerformActions();
            pbImage.LoadImage(this.Task.TempImage);
        }

        private void chkTaskImageAnnotate_CheckedChanged(object sender, EventArgs e)
        {
        }

        #region Config GUI

        protected void ConfigGui()
        {
            // Hide/Show Tabs
            if (GUI.ShowTabJob) this.tcMain.TabPages.Add(tpJob);
            if (GUI.ShowResizeTab) this.tcMain.TabPages.Add(tpImageResize);
            if (GUI.ShowQualityTab) this.tcMain.TabPages.Add(tpImageQuality);

            if (Task != null && Task.TempImage != null)
            {
                pbImage.LoadImage(Task.TempImage);
            }

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
                cboTask.Items.AddRange(typeof(WorkerTask.JobLevel2).GetEnumDescriptions());
            }
            cboTask.SelectedIndex = (int)Config.Job;
        }

        private void ConfigGuiOutputs()
        {
            chkClipboard.Checked = Config.DestConfig.Outputs.Contains(OutputEnum.Clipboard);
            chkSaveFile.Checked = Config.DestConfig.Outputs.Contains(OutputEnum.LocalDisk);
            chkUpload.Checked = Config.DestConfig.Outputs.Contains(OutputEnum.RemoteHost);
            chkPrinter.Checked = Config.DestConfig.Outputs.Contains(OutputEnum.Printer);

            foreach (FileUploaderType fut in Enum.GetValues(typeof(FileUploaderType)))
            {
                CheckBox chkUploader = new CheckBox()
                {
                    Text = fut.GetDescription(),
                    Checked = Config.DestConfig.FileUploaders.Contains(fut),
                    Tag = fut
                };

                if (Engine.ConfigUploaders.IsActive(fut))
                {
                    flpFileUploaders.Controls.Add(chkUploader);
                }
            }

            if (Task != null && ZAppHelper.IsImageFile(Task.Info.LocalFilePath))
            {
                flpImageUploaders.Visible = true;
                flpTextUploaders.Visible = false;
                foreach (ImageUploaderType iut in Enum.GetValues(typeof(ImageUploaderType)))
                {
                    CheckBox chkUploader = new CheckBox()
                    {
                        Text = iut.GetDescription(),
                        Checked = Config.DestConfig.ImageUploaders.Contains(iut),
                        Tag = iut,
                    };
                    if (Engine.ConfigUploaders.IsActive(iut))
                    {
                        flpImageUploaders.Controls.Add(chkUploader);
                    }
                }
            }
            else if (Task != null && ZAppHelper.IsTextFile(Task.Info.LocalFilePath))
            {
                flpTextUploaders.Visible = true;
                flpImageUploaders.Visible = false;
                foreach (TextUploaderType tut in Enum.GetValues(typeof(TextUploaderType)))
                {
                    CheckBox chkUploader = new CheckBox()
                    {
                        Text = tut.GetDescription(),
                        Checked = Config.DestConfig.TextUploaders.Contains(tut),
                        Tag = tut
                    };
                    if (Engine.ConfigUploaders.IsActive(tut))
                    {
                        flpTextUploaders.Controls.Add(chkUploader);
                    }
                }
            }

            gbSaveToFile.Visible = chkSaveFile.Checked;
            if (Task != null)
            {
                txtFileNameWithoutExt.Text = Path.GetFileNameWithoutExtension(Task.Info.LocalFilePath);
                txtSaveFolder.Text = Path.GetDirectoryName(Task.Info.LocalFilePath);
            }
        }

        private void ConfigGuiQuality()
        {
            if (cboFileFormat.Items.Count == 0)
            {
                cboFileFormat.Items.AddRange(typeof(EImageFormat).GetEnumDescriptions());
                cboFileFormat.SelectedIndex = (int)Config.ImageFormat;
            }

            nudSwitchAfter.Value = Engine.ConfigOptions.ConfigImageEffects.ImageSizeLimit;

            if (cboSwitchFormat.Items.Count == 0)
            {
                cboSwitchFormat.Items.AddRange(typeof(EImageFormat).GetEnumDescriptions());
                cboSwitchFormat.SelectedIndex = (int)Config.ImageFormat2;
            }

            if (cboPngQuality.Items.Count == 0)
            {
                cboPngQuality.Items.AddRange(typeof(FreeImagePngQuality).GetEnumDescriptions());
                cboPngQuality.SelectedIndex = (int)Config.ImagePngCompression;
            }

            chkPngQualityInterlaced.Checked = Config.ImagePngInterlaced;

            if (cboJpgQuality.Items.Count == 0)
            {
                cboJpgQuality.Items.AddRange(typeof(FreeImageJpegQualityType).GetEnumDescriptions());
                cboJpgQuality.SelectedIndex = (int)Config.ImageJpegQuality;
            }

            if (cboJpgSubSampling.Items.Count == 0)
            {
                cboJpgSubSampling.Items.AddRange(typeof(FreeImageJpegSubSamplingType).GetEnumDescriptions());
                cboJpgSubSampling.SelectedIndex = (int)Config.ImageJpegSubSampling;
            }

            cboGIFQuality.SelectedIndex = (int)Config.ImageGIFQuality;

            if (cboTiffQuality.Items.Count == 0)
            {
                cboTiffQuality.Items.AddRange(typeof(FreeImageTiffQuality).GetEnumDescriptions());
                cboTiffQuality.SelectedIndex = (int)Config.ImageTiffCompression;
            }

            UpdateGuiQuality();
        }

        private void ConfigGuiResize()
        {
            switch (Engine.ConfigOptions.ConfigImageEffects.ImageSizeType)
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

            nudImageSizeFixedWidth.Value = Engine.ConfigOptions.ConfigImageEffects.ImageSizeFixedWidth;
            nudImageSizeFixedHeight.Value = Engine.ConfigOptions.ConfigImageEffects.ImageSizeFixedHeight;
            nudImageSizeRatio.Value = (decimal)Engine.ConfigOptions.ConfigImageEffects.ImageSizeRatioPercentage;
        }

        private void ConfigGuiTasks()
        {
            bool bIsImage = Task != null && ZAppHelper.IsImageFile(Task.Info.LocalFilePath);

            if (!bIsImage)
            {
                tcMain.TabPages.Remove(tpImagePreview);
            }
            else
            {
                tcMain.TabPages.Add(tpImagePreview);
                tcMain.SelectedTab = tpImagePreview;
            }

            btnTaskAnnotate.Visible = Task.Job1 == EDataType.Image;
            chkTaskImageFileFormat.Visible = bIsImage;
            chkTaskImageResize.Visible = bIsImage;
        }

        #endregion Config GUI

        #region Config GUI Enable/Disable

        private void UpdateGuiQuality()
        {
            cboSwitchFormat.Enabled = nudSwitchAfter.Value > 0;

            tcQuality.TabPages.Clear();
            EImageFormat userImageFormat = (EImageFormat)cboFileFormat.SelectedIndex;
            switch (userImageFormat)
            {
                case EImageFormat.PNG:
                    tcQuality.TabPages.Add(tpQualityPng);
                    break;
                case EImageFormat.JPEG:
                    tcQuality.TabPages.Add(tpQualityJpeg);
                    break;
                case EImageFormat.GIF:
                    tcQuality.TabPages.Add(tpQualityGif);
                    break;
                case EImageFormat.TIFF:
                    tcQuality.TabPages.Add(tpQualityTiff);
                    break;
            }

            EImageFormat userImageFormat2 = (EImageFormat)cboSwitchFormat.SelectedIndex;
            switch (userImageFormat2)
            {
                case EImageFormat.PNG:
                    if (!tcQuality.TabPages.Contains(tpQualityPng)) tcQuality.TabPages.Add(tpQualityPng);
                    break;
                case EImageFormat.JPEG:
                    if (!tcQuality.TabPages.Contains(tpQualityJpeg)) tcQuality.TabPages.Add(tpQualityJpeg);
                    break;
                case EImageFormat.GIF:
                    if (!tcQuality.TabPages.Contains(tpQualityGif)) tcQuality.TabPages.Add(tpQualityGif);
                    break;
                case EImageFormat.TIFF:
                    if (!tcQuality.TabPages.Contains(tpQualityTiff)) tcQuality.TabPages.Add(tpQualityTiff);
                    break;
            }

            cboJpgQuality.Enabled = cboJpgSubSampling.Enabled = ((EImageFormat)cboFileFormat.SelectedIndex == EImageFormat.JPEG ||
      (EImageFormat)cboSwitchFormat.SelectedIndex == EImageFormat.JPEG) && nudSwitchAfter.Value > 0;

            cboGIFQuality.Enabled = ((EImageFormat)cboFileFormat.SelectedIndex == EImageFormat.GIF ||
                (EImageFormat)cboSwitchFormat.SelectedIndex == EImageFormat.GIF) && nudSwitchAfter.Value > 0;
        }

        #endregion Config GUI Enable/Disable

        #region Control Events

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            txtSaveFolder.Text = Adapter.GetDirPathUsingFolderBrowser("Browse for a folder to save files...");
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            BeforeClose();
            Close();
        }

        private void btnOutputsConfig_Click(object sender, EventArgs e)
        {
            UploadersConfigForm ocf = new UploadersConfigForm(Engine.ConfigUploaders, ZKeys.GetAPIKeys()) { Icon = this.Icon };
            ocf.ShowDialog();
            Engine.ConfigUploaders = ocf.Config;
        }

        private void cboFileFormat_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateGuiQuality();
        }

        private void cboSwitchFormat_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateGuiQuality();
        }

        private void cboTask_SelectedIndexChanged(object sender, EventArgs e)
        {
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
                tcMain.TabPages.Insert(0, tpImageQuality);
                tcMain.SelectedTab = tpImageQuality;
            }
            else
            {
                tcMain.TabPages.Remove(tpImageQuality);
            }
        }

        private void chkTaskImageResize_CheckedChanged(object sender, EventArgs e)
        {
            if (chkTaskImageResize.Checked)
            {
                tcMain.TabPages.Insert(0, tpImageResize);
                tcMain.SelectedTab = tpImageResize;
            }
            else
            {
                tcMain.TabPages.Remove(tpImageResize);
                Engine.ConfigOptions.ConfigImageEffects.ImageSizeType = ImageSizeType.DEFAULT;
            }
        }

        private void chkTaskOutputConfig_CheckedChanged(object sender, EventArgs e)
        {
            if (chkTaskOutputConfig.Checked)
            {
                tcMain.TabPages.Insert(0, tpOutputs);
                tcMain.SelectedTab = tpOutputs;
            }
            else
            {
                tcMain.TabPages.Remove(tpOutputs);
            }
        }

        private void nudImageSizeFixedHeight_ValueChanged(object sender, EventArgs e)
        {
            UpdateImageSize();
        }

        private void nudImageSizeFixedWidth_ValueChanged(object sender, EventArgs e)
        {
            UpdateImageSize();
        }

        private void nudImageSizeRatio_ValueChanged(object sender, EventArgs e)
        {
            UpdateImageSize();
        }

        private void nudSwitchAfter_LostFocus(object sender, System.EventArgs e)
        {
            UpdateGuiQuality();
        }

        private void nudSwitchAfter_MouseUp(object sender, MouseEventArgs e)
        {
            ConfigGuiQuality();
        }

        private void nudSwitchAfter_ValueChanged(object sender, EventArgs e)
        {
            UpdateGuiQuality();
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

        private void WorkflowWizard_Load(object sender, EventArgs e)
        {
            ConfigGui();
        }

        #endregion Control Events

        #region Helper Methods

        private void BeforeClose()
        {
            if (DialogResult == System.Windows.Forms.DialogResult.Cancel) return;

            // Description
            if (!string.IsNullOrEmpty(txtName.Text))
            {
                Config.Description = txtName.Text;
            }

            Config.Job = (WorkerTask.JobLevel2)cboTask.SelectedIndex;

            // Tasks

            // Quality
            Config.ImageFormat = (EImageFormat)cboFileFormat.SelectedIndex;
            Engine.ConfigOptions.ConfigImageEffects.ImageSizeLimit = (int)nudSwitchAfter.Value;
            Config.ImageFormat2 = (EImageFormat)cboSwitchFormat.SelectedIndex;

            Config.ImagePngInterlaced = chkPngQualityInterlaced.Checked;
            Config.ImagePngCompression = (FreeImagePngQuality)cboPngQuality.SelectedIndex;

            Config.ImageJpegQuality = (FreeImageJpegQualityType)cboJpgQuality.SelectedIndex;
            Config.ImageJpegSubSampling = (FreeImageJpegSubSamplingType)cboJpgSubSampling.SelectedIndex;

            Config.ImageGIFQuality = (GIFQuality)cboGIFQuality.SelectedIndex;

            Config.ImageTiffCompression = (FreeImageTiffQuality)cboTiffQuality.SelectedIndex;

            // Resize
            UpdateImageSize(bChangeConfig: true);

            // Outputs
            UpdateConfigOutputs();
        }

        protected virtual void Initialize(WorkerTask task, WorkflowWizardGUIOptions gui)
        {
            if (task != null)
            {
                this.Config = task.WorkflowConfig;
                this.Text = Application.ProductName + " - Workflow - " + task.Info.Job.GetDescription();
            }
            else
            {
                this.Text = Application.ProductName;
            }

            tcMain.TabPages.Clear();
            if (gui != null)
            {
                gbTasks.Visible = false;
                this.MinimumSize = new System.Drawing.Size(this.Width - gbTasks.Width, this.Height);
                this.Width = this.MinimumSize.Width;
                this.GUI = gui;
            }
            else
            {
                chkTaskOutputConfig.Checked = true;
            }
            if (task != null)
            {
                this.Task = task;
            }
        }

        private void UpdateConfigOutputs()
        {
            Config.DestConfig.Outputs.Clear();
            if (chkClipboard.Checked) Config.DestConfig.Outputs.Add(OutputEnum.Clipboard);
            if (chkSaveFile.Checked) Config.DestConfig.Outputs.Add(OutputEnum.LocalDisk);
            if (chkUpload.Checked) Config.DestConfig.Outputs.Add(OutputEnum.RemoteHost);
            if (chkPrinter.Checked) Config.DestConfig.Outputs.Add(OutputEnum.Printer);

            Config.DestConfig.FileUploaders.Clear();
            foreach (CheckBox chk in flpFileUploaders.Controls)
            {
                FileUploaderType ut = (FileUploaderType)chk.Tag;
                if (chk.Checked) Config.DestConfig.FileUploaders.Add(ut);
            }

            Config.DestConfig.ImageUploaders.Clear();
            foreach (CheckBox chk in flpImageUploaders.Controls)
            {
                ImageUploaderType ut = (ImageUploaderType)chk.Tag;
                if (chk.Checked) Config.DestConfig.ImageUploaders.Add(ut);
            }

            Config.DestConfig.TextUploaders.Clear();
            foreach (CheckBox chk in flpTextUploaders.Controls)
            {
                TextUploaderType ut = (TextUploaderType)chk.Tag;
                if (chk.Checked) Config.DestConfig.TextUploaders.Add(ut);
            }

            if (!string.IsNullOrEmpty(txtFileNameWithoutExt.Text) && !string.IsNullOrEmpty(txtSaveFolder.Text))
            {
                string ext = Path.GetExtension(Task.Info.LocalFilePath);
                Task.Info.LocalFilePath = Path.Combine(txtSaveFolder.Text, txtFileNameWithoutExt.Text) + ext;
            }
        }

        private void UpdateImageSize(bool bChangeConfig = false)
        {
            if (Task == null) return;
            if (Task.Info.ImageSize.IsEmpty) return;

            double w2 = 0.0, h2 = 0.0, ratio = 0.0;

            if (rbImageSizeDefault.Checked)
            {
                if (bChangeConfig) Engine.ConfigOptions.ConfigImageEffects.ImageSizeType = ImageSizeType.DEFAULT;
                ratio = 1.0;
                h2 = Task.Info.ImageSize.Height;
            }
            else if (rbImageSizeFixed.Checked)
            {
                if (bChangeConfig) Engine.ConfigOptions.ConfigImageEffects.ImageSizeType = ImageSizeType.FIXED;
                if (Task.Info.ImageSize.Width > 0 && nudImageSizeFixedWidth.Value > 0)
                {
                    ratio = (double)nudImageSizeFixedWidth.Value / (double)Task.Info.ImageSize.Width;
                    h2 = nudImageSizeFixedHeight.Value > 0 ? (double)nudImageSizeFixedHeight.Value : (double)Task.Info.ImageSize.Height * ratio;
                }
            }
            else if (rbImageSizeRatio.Checked)
            {
                if (bChangeConfig) Engine.ConfigOptions.ConfigImageEffects.ImageSizeType = ImageSizeType.RATIO;
                if (Task.Info.ImageSize.Width > 0)
                {
                    ratio = (double)nudImageSizeRatio.Value / 100.0;
                    h2 = (double)Task.Info.ImageSize.Height * ratio;
                }
            }

            if (ratio > 0.0)
            {
                w2 = (double)Task.Info.ImageSize.Width * ratio;
                gbImageSize.Text = string.Format("Image Size - old: {0}x{1} - new: {2}x{3}",
                                  Task.Info.ImageSize.Width, Task.Info.ImageSize.Height, Math.Round(w2, 0), Math.Round(h2, 0));
            }
        }

        #endregion Helper Methods
    }

    public class WorkflowWizardGUIOptions
    {
        public bool ShowQualityTab { get; set; }

        public bool ShowResizeTab { get; set; }

        public bool ShowTabJob { get; set; }

        public bool ShowTasks { get; set; }
    }
}
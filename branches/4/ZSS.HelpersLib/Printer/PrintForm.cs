using System;
using System.Drawing;
using System.Windows.Forms;

namespace HelpersLib
{
    public partial class PrintForm : Form
    {
        private PrintHelper printHelper;
        private PrintSettings printSettings;

        public PrintForm(Image img, PrintSettings settings)
        {
            InitializeComponent();
            printHelper = new PrintHelper(img);
            printHelper.Settings = printSettings = settings;
            LoadSettings();
        }

        private void LoadSettings()
        {
            nudMargin.Value = printSettings.Margin;
            cbAutoRotate.Checked = printSettings.AutoRotateImage;
            cbAutoScale.Checked = printSettings.AutoScaleImage;
            cbAllowEnlarge.Checked = printSettings.AllowEnlargeImage;
            cbCenterImage.Checked = printSettings.CenterImage;

            cbAllowEnlarge.Enabled = printSettings.AutoScaleImage;
            cbCenterImage.Enabled = printSettings.AutoScaleImage;
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            printHelper.Print();
            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnShowPreview_Click(object sender, EventArgs e)
        {
            printHelper.ShowPreview();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void nudMargin_ValueChanged(object sender, EventArgs e)
        {
            printSettings.Margin = (int)nudMargin.Value;
        }

        private void cbAutoRotate_CheckedChanged(object sender, EventArgs e)
        {
            printSettings.AutoRotateImage = cbAutoRotate.Checked;
        }

        private void cbAutoScale_CheckedChanged(object sender, EventArgs e)
        {
            printSettings.AutoScaleImage = cbAutoScale.Checked;

            cbAllowEnlarge.Enabled = printSettings.AutoScaleImage;
            cbCenterImage.Enabled = printSettings.AutoScaleImage;
        }

        private void cbAllowEnlarge_CheckedChanged(object sender, EventArgs e)
        {
            printSettings.AllowEnlargeImage = cbAllowEnlarge.Checked;
        }

        private void cbCenterImage_CheckedChanged(object sender, EventArgs e)
        {
            printSettings.CenterImage = cbCenterImage.Checked;
        }
    }
}
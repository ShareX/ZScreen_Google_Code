using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ZSS.Colors
{
    public partial class ColorDialog : Form
    {
        MyColor myColor;

        public ColorDialog()
        {
            InitializeComponent();
        }

        private void UpdateControls(MyColor color)
        {
            lblPrimaryColor.BackColor = color;
            nudHue.Value = (decimal)Math.Round(color.HSB.Hue * 360);
            nudSaturation.Value = (decimal)Math.Round(color.HSB.Saturation * 100);
            nudBrightness.Value = (decimal)Math.Round(color.HSB.Brightness * 100);
            nudRed.Value = color.RGB.Red;
            nudGreen.Value = color.RGB.Green;
            nudBlue.Value = color.RGB.Blue;
            nudCyan.Value = (decimal)Math.Round(color.CMYK.Cyan * 100);
            nudMagenta.Value = (decimal)Math.Round(color.CMYK.Magenta * 100);
            nudYellow.Value = (decimal)Math.Round(color.CMYK.Yellow * 100);
            nudKey.Value = (decimal)Math.Round(color.CMYK.Key * 100);
            txtHex.Text = MyColors.ColorToHex(color);
            txtDecimal.Text = MyColors.ColorToDecimal(color).ToString();
        }

        #region Events

        private void colorPicker_ColorChanged(object sender, ColorEventArgs e)
        {
            myColor = e.Color;
            UpdateControls(myColor);
        }

        private void btnColorPicker_Click(object sender, EventArgs e)
        {
            if (colorTimer.Enabled)
            {
                colorTimer.Stop();
            }
            else
            {
                colorTimer.Start();
            }
        }

        private void colorTimer_Tick(object sender, EventArgs e)
        {
            colorPicker.Color = MyColors.GetPixelColor(MousePosition);
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void rbHue_CheckedChanged(object sender, EventArgs e)
        {
            if (rbHue.Checked) colorPicker.DrawStyle = DrawStyle.Hue;
        }

        private void rbSaturation_CheckedChanged(object sender, EventArgs e)
        {
            if (rbSaturation.Checked) colorPicker.DrawStyle = DrawStyle.Saturation;
        }

        private void rbBrightness_CheckedChanged(object sender, EventArgs e)
        {
            if (rbBrightness.Checked) colorPicker.DrawStyle = DrawStyle.Brightness;
        }

        private void rbRed_CheckedChanged(object sender, EventArgs e)
        {
            if (rbRed.Checked) colorPicker.DrawStyle = DrawStyle.Red;
        }

        private void rbGreen_CheckedChanged(object sender, EventArgs e)
        {
            if (rbGreen.Checked) colorPicker.DrawStyle = DrawStyle.Green;
        }

        private void rbBlue_CheckedChanged(object sender, EventArgs e)
        {
            if (rbBlue.Checked) colorPicker.DrawStyle = DrawStyle.Blue;
        }

        #endregion


    }
}
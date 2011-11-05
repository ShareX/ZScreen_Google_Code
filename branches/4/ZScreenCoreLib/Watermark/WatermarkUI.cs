using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GradientTester;
using HelpersLib;
using ZSS.ColorsLib;

namespace ZScreenCoreLib
{
    public partial class WatermarkUI : Form
    {
        #region 0 Properties

        private readonly ContextMenuStrip codesMenu = new ContextMenuStrip();
        private WatermarkConfig Config = new WatermarkConfig();

        #endregion 0 Properties

        #region 1 Helper Methods

        /// <summary>Get Image with Watermark</summary>
        public Image ApplyWatermark(Image img, NameParser parser = null)
        {
            if (parser == null)
            {
                parser = new NameParser(NameParserType.Watermark) { IsPreview = true, Picture = img };
            }
            return new WatermarkEffects(Config).ApplyWatermark(img, parser, Config.WatermarkMode);
        }

        private void CheckForCodes(object checkObject)
        {
            var textBox = (TextBox)checkObject;
            if (codesMenu.Items.Count > 0)
            {
                codesMenu.Show(textBox, new Point(textBox.Width + 1, 0));
            }
        }

        private void CodesMenuCloseEvent(object sender, MouseEventArgs e)
        {
            codesMenu.Close();
        }

        private void CodesMenuCloseEvents()
        {
            this.MouseClick += CodesMenuCloseEvent;
            foreach (Control cntrl in this.Controls)
            {
                if (cntrl.GetType() == typeof(GroupBox))
                {
                    cntrl.MouseClick += CodesMenuCloseEvent;
                }
            }
        }

        private void CreateCodesMenu()
        {
            var variables = Enum.GetValues(typeof(ReplacementVariables)).Cast<ReplacementVariables>().
                Select(
                    x =>
                    new
                    {
                        Name = ReplacementExtension.Prefix + Enum.GetName(typeof(ReplacementVariables), x),
                        Description = x.GetDescription()
                    });

            foreach (var variable in variables)
            {
                var tsi = new ToolStripMenuItem
                {
                    Text = string.Format("{0} - {1}", variable.Name, variable.Description),
                    Tag = variable.Name
                };
                tsi.Click += watermarkCodeMenu_Click;
                codesMenu.Items.Add(tsi);
            }

            CodesMenuCloseEvents();
        }

        private string FontToString()
        {
            return FontToString(Config.WatermarkFont, Config.WatermarkFontArgb);
        }

        private string FontToString(Font font, Color color)
        {
            return "Name: " + font.Name + " - Size: " + font.Size + " - Style: " + font.Style + " - Color: " +
                   color.R + "," + color.G + "," + color.B;
        }

        private void SelectColor(Control pb, ref XColor color)
        {
            var dColor = new DialogColor(pb.BackColor);
            if (dColor.ShowDialog() == DialogResult.OK)
            {
                pb.BackColor = dColor.Color;
                color = dColor.Color;
            }
        }

        private void TestWatermark()
        {
            using (Bitmap bmp = ZScreenCoreLib.Properties.Resources.main.Clone(new Rectangle(62, 33, 199, 140), PixelFormat.Format32bppArgb))
            {
                var bmp2 = new Bitmap(pbWatermarkShow.ClientRectangle.Width, pbWatermarkShow.ClientRectangle.Height);
                Graphics g = Graphics.FromImage(bmp2);
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.DrawImage(bmp,
                            new Rectangle(0, 0, pbWatermarkShow.ClientRectangle.Width,
                                          pbWatermarkShow.ClientRectangle.Height));
                pbWatermarkShow.Image = new WatermarkEffects(Config).ApplyWatermark(bmp2);
            }
        }

        private void watermarkCodeMenu_Click(object sender, EventArgs e)
        {
            var tsi = (ToolStripMenuItem)sender;
            int oldPos = txtWatermarkText.SelectionStart;
            string appendText;
            if (oldPos > 0 && txtWatermarkText.Text[txtWatermarkText.SelectionStart - 1] == ReplacementExtension.Prefix)
            {
                appendText = tsi.Tag.ToString().TrimStart('%');
                txtWatermarkText.Text =
                    txtWatermarkText.Text.Insert(txtWatermarkText.SelectionStart, appendText);
                txtWatermarkText.Select(oldPos + appendText.Length, 0);
            }
            else
            {
                appendText = tsi.Tag.ToString();
                txtWatermarkText.Text =
                    txtWatermarkText.Text.Insert(txtWatermarkText.SelectionStart, appendText);
                txtWatermarkText.Select(oldPos + appendText.Length, 0);
            }
        }

        #endregion 1 Helper Methods

        private void btnSelectGradient_Click(object sender, EventArgs e)
        {
            using (var gradient = new GradientMaker(Config.GradientMakerOptions))
            {
                gradient.Icon = Icon;
                if (gradient.ShowDialog() == DialogResult.OK)
                {
                    Config.GradientMakerOptions = gradient.Options;
                    TestWatermark();
                }
            }
        }

        private void btnWatermarkFont_Click(object sender, EventArgs e)
        {
            DialogResult result = ZScreenCoreHelper.ShowFontDialog(Config);
            if (result == DialogResult.OK)
            {
                pbWatermarkFontColor.BackColor = Config.WatermarkFontArgb;
                lblWatermarkFont.Text = FontToString();
                TestWatermark();
            }
        }

        private void btwWatermarkBrowseImage_Click(object sender, EventArgs e)
        {
            var fd = new OpenFileDialog
                         {
                             InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop)
                         };
            if (fd.ShowDialog() == DialogResult.OK)
            {
                txtWatermarkImageLocation.Text = fd.FileName;
            }
        }

        private void cboWatermarkType_SelectedIndexChanged(object sender, EventArgs e)
        {
            Config.WatermarkMode = (WatermarkType)cboWatermarkType.SelectedIndex;
            TestWatermark();
            tcWatermark.Enabled = Config.WatermarkMode != WatermarkType.NONE;
        }

        private void cbUseCustomGradient_CheckedChanged(object sender, EventArgs e)
        {
            Config.WatermarkUseCustomGradient = cboUseCustomGradient.Checked;
            gbGradientMakerBasic.Enabled = !cboUseCustomGradient.Checked;
            TestWatermark();
        }

        private void cbWatermarkAddReflection_CheckedChanged(object sender, EventArgs e)
        {
            Config.WatermarkAddReflection = cbWatermarkAddReflection.Checked;
            TestWatermark();
        }

        private void cbWatermarkAutoHide_CheckedChanged(object sender, EventArgs e)
        {
            Config.WatermarkAutoHide = cbWatermarkAutoHide.Checked;
            TestWatermark();
        }

        private void cbWatermarkGradientType_SelectedIndexChanged(object sender, EventArgs e)
        {
            Config.WatermarkGradientType = (LinearGradientMode)cbWatermarkGradientType.SelectedIndex;
            TestWatermark();
        }

        private void cbWatermarkPosition_SelectedIndexChanged(object sender, EventArgs e)
        {
            Config.WatermarkPositionMode = (WatermarkPositionType)chkWatermarkPosition.SelectedIndex;
            TestWatermark();
        }

        private void cbWatermarkUseBorder_CheckedChanged(object sender, EventArgs e)
        {
            Config.WatermarkUseBorder = cbWatermarkUseBorder.Checked;
            TestWatermark();
        }

        private void nudWatermarkBackTrans_ValueChanged(object sender, EventArgs e)
        {
            Config.WatermarkBackTrans = nudWatermarkBackTrans.Value;
            trackWatermarkBackgroundTrans.Value = (int)nudWatermarkBackTrans.Value;
        }

        private void nudWatermarkCornerRadius_ValueChanged(object sender, EventArgs e)
        {
            Config.WatermarkCornerRadius = nudWatermarkCornerRadius.Value;
            TestWatermark();
        }

        private void nudWatermarkFontTrans_ValueChanged(object sender, EventArgs e)
        {
            Config.WatermarkFontTrans = nudWatermarkFontTrans.Value;
            trackWatermarkFontTrans.Value = (int)nudWatermarkFontTrans.Value;
        }

        private void nudWatermarkImageScale_ValueChanged(object sender, EventArgs e)
        {
            Config.WatermarkImageScale = nudWatermarkImageScale.Value;
            TestWatermark();
        }

        private void nudWatermarkOffset_ValueChanged(object sender, EventArgs e)
        {
            Config.WatermarkOffset = nudWatermarkOffset.Value;
            TestWatermark();
        }

        private void pbWatermarkBorderColor_Click(object sender, EventArgs e)
        {
            SelectColor((PictureBox)sender, ref Config.WatermarkBorderArgb);
            TestWatermark();
        }

        private void pbWatermarkFontColor_Click(object sender, EventArgs e)
        {
            SelectColor((PictureBox)sender, ref Config.WatermarkFontArgb);
            lblWatermarkFont.Text = FontToString();
            TestWatermark();
        }

        private void pbWatermarkGradient1_Click(object sender, EventArgs e)
        {
            SelectColor((PictureBox)sender, ref Config.WatermarkGradient1Argb);
            TestWatermark();
        }

        private void pbWatermarkGradient2_Click(object sender, EventArgs e)
        {
            SelectColor((PictureBox)sender, ref Config.WatermarkGradient2Argb);
            TestWatermark();
        }

        private void trackWatermarkBackgroundTrans_Scroll(object sender, EventArgs e)
        {
            Config.WatermarkBackTrans = trackWatermarkBackgroundTrans.Value;
            nudWatermarkBackTrans.Value = Config.WatermarkBackTrans;
            TestWatermark();
        }

        private void trackWatermarkFontTrans_Scroll(object sender, EventArgs e)
        {
            Config.WatermarkFontTrans = trackWatermarkFontTrans.Value;
            nudWatermarkFontTrans.Value = Config.WatermarkFontTrans;
            TestWatermark();
        }

        private void txtWatermarkImageLocation_TextChanged(object sender, EventArgs e)
        {
            if (File.Exists(txtWatermarkImageLocation.Text))
            {
                Config.WatermarkImageLocation = txtWatermarkImageLocation.Text;
                TestWatermark();
            }
        }

        private void txtWatermarkText_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && codesMenu.Visible)
            {
                codesMenu.Close();
            }
        }

        private void txtWatermarkText_Leave(object sender, EventArgs e)
        {
            if (codesMenu.Visible)
            {
                codesMenu.Close();
            }
        }

        private void txtWatermarkText_MouseDown(object sender, MouseEventArgs e)
        {
            CheckForCodes(sender);
        }   

        private void txtWatermarkText_TextChanged(object sender, EventArgs e)
        {
            Config.WatermarkText = txtWatermarkText.Text;
            TestWatermark();
        }

        public WatermarkUI(WatermarkConfig cfg)
        {
            InitializeComponent();
            this.Config = cfg;
        }

        private void WatermarkUI_Load(object sender, EventArgs e)
        {
            this.Text = Application.ProductName + " - Watermark Configurator";

            if (cboWatermarkType.Items.Count == 0)
            {
                cboWatermarkType.Items.AddRange(typeof(WatermarkType).GetEnumDescriptions());
            }

            cboWatermarkType.SelectedIndex = (int)Config.WatermarkMode;
            if (chkWatermarkPosition.Items.Count == 0)
            {
                chkWatermarkPosition.Items.AddRange(typeof(WatermarkPositionType).GetEnumDescriptions());
            }

            chkWatermarkPosition.SelectedIndex = (int)Config.WatermarkPositionMode;
            nudWatermarkOffset.Value = Config.WatermarkOffset;
            cbWatermarkAddReflection.Checked = Config.WatermarkAddReflection;
            cbWatermarkAutoHide.Checked = Config.WatermarkAutoHide;

            txtWatermarkText.Text = Config.WatermarkText;
            pbWatermarkFontColor.BackColor = Config.WatermarkFontArgb;
            lblWatermarkFont.Text = FontToString();
            nudWatermarkFontTrans.Value = Config.WatermarkFontTrans;
            trackWatermarkFontTrans.Value = (int)Config.WatermarkFontTrans;
            nudWatermarkCornerRadius.Value = Config.WatermarkCornerRadius;
            pbWatermarkGradient1.BackColor = Config.WatermarkGradient1Argb;
            pbWatermarkGradient2.BackColor = Config.WatermarkGradient2Argb;
            pbWatermarkBorderColor.BackColor = Config.WatermarkBorderArgb;
            nudWatermarkBackTrans.Value = Config.WatermarkBackTrans;
            trackWatermarkBackgroundTrans.Value = (int)Config.WatermarkBackTrans;
            if (cbWatermarkGradientType.Items.Count == 0)
            {
                cbWatermarkGradientType.Items.AddRange(Enum.GetNames(typeof(LinearGradientMode)));
            }

            cbWatermarkGradientType.SelectedIndex = (int)Config.WatermarkGradientType;
            cboUseCustomGradient.Checked = Config.WatermarkUseCustomGradient;

            txtWatermarkImageLocation.Text = Config.WatermarkImageLocation;
            cbWatermarkUseBorder.Checked = Config.WatermarkUseBorder;
            nudWatermarkImageScale.Value = Config.WatermarkImageScale;

            CreateCodesMenu();
            TestWatermark();
        }
    }
}
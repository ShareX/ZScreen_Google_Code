using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using ZSS.ColorsLib;
using System.Globalization;

namespace GradientTester
{
    public partial class TestForm : Form
    {
        public TestForm()
        {
            InitializeComponent();
            TestColors();
        }

        private void TestColors()
        {
            txtStartPointX.Text = txtEndPointX.Text = "0.5";
            txtStartPointY.Text = "0";
            txtEndPointY.Text = "1";
            AddGradientStop("#FF3A6EB7", "0");
            AddGradientStop("#FF0D3A7A", "0.5");
            AddGradientStop("#FF041F47", "0.5");
            AddGradientStop("#FF233853", "1");

            /*
                <LinearGradientBrush StartPoint="0.5,0" EndPoint="0.5,1">
                    <GradientStop Color="#FF3A6EB7" Offset="0"/>
                    <GradientStop Color="#FF0D3A7A" Offset="0.5"/>
                    <GradientStop Color="#FF041F47" Offset="0.5"/>
                    <GradientStop Color="#FF233853" Offset="1"/>
                </LinearGradientBrush>
            */
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            UpdatePreview();
        }

        private void UpdatePreview()
        {
            List<GradientStop> gradient = new List<GradientStop>();
            foreach (string line in rtbCodes.Lines)
            {
                if (line.Contains('\t'))
                {
                    gradient.Add(ParseLine(line));
                }
            }
            PointF startPoint = new PointF(float.Parse(txtStartPointX.Text, CultureInfo.InvariantCulture),
                float.Parse(txtStartPointY.Text, CultureInfo.InvariantCulture));
            PointF endPoint = new PointF(float.Parse(txtEndPointX.Text, CultureInfo.InvariantCulture),
                float.Parse(txtEndPointY.Text, CultureInfo.InvariantCulture));
            LinearGradientBrush brush = CreateGradientBrush(pbPreview.ClientSize, startPoint, endPoint, gradient);

            Bitmap bmp = new Bitmap(pbPreview.ClientSize.Width, pbPreview.ClientSize.Height);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.FillRectangle(brush, 0, 0, pbPreview.ClientSize.Width, pbPreview.ClientSize.Height);
            }

            pbPreview.Image = bmp;
        }

        private GradientStop ParseLine(string line)
        {
            return new GradientStop(line.Substring(0, line.IndexOf('\t')), line.Remove(0, line.IndexOf('\t') + 1));
        }

        public LinearGradientBrush CreateGradientBrush(Size size, PointF startPoint, PointF endPoint, IEnumerable<GradientStop> gradient)
        {
            Point start = new Point((int)(size.Width * startPoint.X), (int)(size.Height * startPoint.Y));
            Point end = new Point((int)(size.Width * endPoint.X), (int)(size.Height * endPoint.Y));
            LinearGradientBrush brush = new LinearGradientBrush(start, end, Color.Black, Color.Black);
            gradient = gradient.OrderBy(x => x.Offset);
            ColorBlend blend = new ColorBlend();
            blend.Colors = gradient.Select(x => x.Color).ToArray();
            blend.Positions = gradient.Select(x => x.Offset).ToArray();
            brush.InterpolationColors = blend;
            return brush;
        }

        private void AddGradientStop(string color, string offset)
        {
            try
            {
                rtbCodes.SelectedText = string.Format("{0}\t{1}\n", color, offset);
                UpdatePreview();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        private void btnAddColor_Click(object sender, EventArgs e)
        {
            AddGradientStop(txtColor.Text, txtOffset.Text);
        }

        private void btnBrowseColor_Click(object sender, EventArgs e)
        {
            using (DialogColor colorPicker = new DialogColor())
            {
                if (!string.IsNullOrEmpty(txtColor.Text))
                {
                    colorPicker.SetCurrentColor(MyColors.ParseColor(txtColor.Text));
                }

                if (colorPicker.ShowDialog() == DialogResult.OK)
                {
                    Color color = colorPicker.Color;
                    txtColor.Text = string.Format("{0},{1},{2},{3}", color.A, color.R, color.G, color.B);
                }
            }
        }

        private void rtbCodes_SelectionChanged(object sender, EventArgs e)
        {
            int firstcharindex = rtbCodes.GetFirstCharIndexOfCurrentLine();
            int currentline = rtbCodes.GetLineFromCharIndex(firstcharindex);
            string line = rtbCodes.Lines[currentline];
            if (line.Contains('\t'))
            {
                txtColor.Text = line.Substring(0, line.IndexOf('\t'));
                txtOffset.Text = line.Remove(0, line.IndexOf('\t') + 1);
            }
        }
    }
}
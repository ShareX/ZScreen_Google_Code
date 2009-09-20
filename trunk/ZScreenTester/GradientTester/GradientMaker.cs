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
using System.Diagnostics;

namespace GradientTester
{
    public partial class GradientMaker : Form
    {
        public string BrushData;
        public PointF StartPoint;
        public PointF EndPoint;

        private bool isEditable;
        private bool isEditing;

        public GradientMaker()
        {
            InitializeComponent();
        }

        public GradientMaker(string brushData, PointF startPoint, PointF endPoint)
        {
            InitializeComponent();
            this.BrushData = brushData;
            this.StartPoint = startPoint;
            this.EndPoint = endPoint;
            txtStartPointX.Text = startPoint.X.ToString(CultureInfo.InvariantCulture);
            txtStartPointY.Text = startPoint.Y.ToString(CultureInfo.InvariantCulture);
            txtEndPointX.Text = endPoint.X.ToString(CultureInfo.InvariantCulture);
            txtEndPointY.Text = endPoint.Y.ToString(CultureInfo.InvariantCulture);
            rtbCodes.Text = brushData;
            UpdatePreview();
        }

        private void TestColors()
        {
            txtStartPointX.Text = txtEndPointX.Text = "0.5";
            txtStartPointY.Text = "0";
            txtEndPointY.Text = "1";
            rtbCodes.Text = "#FF3A6EB7\t0\n#FF0D3A7A\t0.5\n#FF041F47\t0.5\n#FF233853\t1";
            UpdatePreview();

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
            try
            {
                GradientStop[] gradient = ParseGradientData(rtbCodes.Text);
                if (gradient.Length > 1)
                {
                    PointF startPoint = new PointF(float.Parse(txtStartPointX.Text, CultureInfo.InvariantCulture),
                        float.Parse(txtStartPointY.Text, CultureInfo.InvariantCulture));
                    PointF endPoint = new PointF(float.Parse(txtEndPointX.Text, CultureInfo.InvariantCulture),
                        float.Parse(txtEndPointY.Text, CultureInfo.InvariantCulture));
                    using (LinearGradientBrush brush = CreateGradientBrush(pbPreview.ClientSize, startPoint, endPoint, gradient))
                    {
                        Bitmap bmp = new Bitmap(pbPreview.ClientSize.Width, pbPreview.ClientSize.Height);
                        using (Graphics g = Graphics.FromImage(bmp))
                        {
                            g.FillRectangle(brush, 0, 0, pbPreview.ClientSize.Width, pbPreview.ClientSize.Height);
                        }

                        pbPreview.Image = bmp;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        private static GradientStop[] ParseGradientData(string gradientData)
        {
            List<GradientStop> gradient = new List<GradientStop>();
            string[] lines = gradientData.Split('\n').Select(x => x.Trim()).ToArray();
            foreach (string line in lines)
            {
                if (line.Contains('\t'))
                {
                    gradient.Add(ParseLine(line));
                }
            }

            return gradient.ToArray();
        }

        private static GradientStop ParseLine(string line)
        {
            return new GradientStop(line.Substring(0, line.IndexOf('\t')), line.Remove(0, line.IndexOf('\t') + 1));
        }

        public static LinearGradientBrush CreateGradientBrush(Size size, PointF startPoint, PointF endPoint, IEnumerable<GradientStop> gradient)
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

        public static LinearGradientBrush CreateGradientBrush(Size size, PointF startPoint, PointF endPoint, string gradientData)
        {
            return CreateGradientBrush(size, startPoint, endPoint, ParseGradientData(gradientData));
        }

        private void AddGradientStop(string color, string offset)
        {
            rtbCodes.SelectedText = string.Format("{0}\t{1}", color, offset);
            UpdatePreview();
        }

        private void btnAddColor_Click(object sender, EventArgs e)
        {
            isEditing = true;
            if (isEditable)
            {
                int firstcharindex = rtbCodes.GetFirstCharIndexOfCurrentLine();
                int currentline = rtbCodes.GetLineFromCharIndex(firstcharindex);
                if (rtbCodes.Lines.Length > currentline)
                {
                    rtbCodes.SelectionStart = firstcharindex;
                    rtbCodes.SelectionLength = rtbCodes.Lines[currentline].Length;
                }
            }

            AddGradientStop(txtColor.Text, txtOffset.Text);
            isEditing = false;
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
            if (!isEditing)
            {
                isEditable = false;
                int firstcharindex = rtbCodes.GetFirstCharIndexOfCurrentLine();
                int currentline = rtbCodes.GetLineFromCharIndex(firstcharindex);
                if (rtbCodes.Lines.Length > currentline)
                {
                    string line = rtbCodes.Lines[currentline];
                    if (line.Contains('\t'))
                    {
                        txtColor.Text = line.Substring(0, line.IndexOf('\t'));
                        txtOffset.Text = line.Remove(0, line.IndexOf('\t') + 1);
                        isEditable = true;
                        UpdatePreview();
                    }
                }
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            PointF startPoint = new PointF(float.Parse(txtStartPointX.Text, CultureInfo.InvariantCulture),
                   float.Parse(txtStartPointY.Text, CultureInfo.InvariantCulture));
            PointF endPoint = new PointF(float.Parse(txtEndPointX.Text, CultureInfo.InvariantCulture),
                float.Parse(txtEndPointY.Text, CultureInfo.InvariantCulture));
            this.StartPoint = startPoint;
            this.EndPoint = endPoint;
            this.BrushData = rtbCodes.Text;

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void btnHelp_Click(object sender, EventArgs e)
        {
            Process.Start("http://code.google.com/p/zscreen/wiki/Watermark");
        }
    }
}
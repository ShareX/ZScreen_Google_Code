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
        public GradientMakerSettings Options;
        public BrushData mActiveBrushData;
        private bool isEditable;
        private bool isEditing;
        private string lastData;

        public GradientMaker()
        {
            InitializeComponent();
            cboGradientDirection.SelectedIndex = 0;
        }

        public GradientMaker(GradientMakerSettings options)
        {
            InitializeComponent();
            this.Options = options;
            foreach (BrushData bd in options.BrushDataList)
            {
                lbBrushData.Items.Add(bd);
            }
            this.mActiveBrushData = options.GetBrushDataActive();
            UpdateGUI(this.mActiveBrushData);
            UpdatePreview();
        }

        private void UpdateGUI(BrushData bd)
        {
            rtbCodes.Text = bd.Data;
            cboGradientDirection.SelectedIndex = (int)bd.Direction;
        }

        private void UpdatePreview()
        {
            try
            {
                using (LinearGradientBrush brush = CreateGradientBrush(pbPreview.ClientSize, this.mActiveBrushData))
                {
                    Bitmap bmp = new Bitmap(pbPreview.ClientSize.Width, pbPreview.ClientSize.Height);
                    using (Graphics g = Graphics.FromImage(bmp))
                    {
                        g.FillRectangle(brush, 0, 0, pbPreview.ClientSize.Width, pbPreview.ClientSize.Height);
                    }

                    pbPreview.Image = bmp;
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

        public static LinearGradientBrush CreateGradientBrush(Size size, BrushData gradientData)
        {
            IEnumerable<GradientStop> gradient = ParseGradientData(gradientData.Data);

            PointF startPoint = new PointF(0, 0);
            PointF endPoint = PointF.Empty;
            switch (gradientData.Direction)
            {
                case BrushData.GradientDirection.Horizontal:
                    endPoint = new PointF(1, 0);
                    break;
                case BrushData.GradientDirection.Vertical:
                    endPoint = new PointF(0, 1);
                    break;
            }

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

        #region Form events

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

            rtbCodes.SelectedText = string.Format("{0}\t{1}", txtColor.Text, txtOffset.Text);
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
                        if (rtbCodes.Text != lastData)
                        {
                            UpdatePreview();
                            lastData = rtbCodes.Text;
                        }
                    }
                }
            }
        }

        private void rtbCodes_TextChanged(object sender, EventArgs e)
        {
            this.mActiveBrushData.Data = rtbCodes.Text;
            UpdatePreview();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            UpdateBrushData();
            this.Options.BrushDataSelected = lbBrushData.SelectedIndex;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void UpdateBrushData()
        {
            this.mActiveBrushData = new BrushData(rtbCodes.Text, BrushData.GradientDirection.Vertical);
            this.mActiveBrushData.Name = txtName.Text;
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

        private void cboGradientDirection_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.mActiveBrushData.Direction = (BrushData.GradientDirection)cboGradientDirection.SelectedIndex;
            UpdatePreview();
        }

        #endregion

        private void GradientMaker_Load(object sender, EventArgs e)
        {

        }

        private void lbBrushData_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lbBrushData.SelectedIndex != -1)
            {
                this.Options.BrushDataSelected = lbBrushData.SelectedIndex;
                this.mActiveBrushData = (BrushData)lbBrushData.SelectedItem;
                UpdateGUI(this.mActiveBrushData);
                UpdatePreview();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            UpdateBrushData();
            lbBrushData.Items.Add(mActiveBrushData);
            this.Options.BrushDataList.Add(mActiveBrushData);
        }

        private void lbBrushData_KeyDown(object sender, KeyEventArgs e)
        {
            if (lbBrushData.SelectedIndex > -1)
            {
                if (e.KeyCode == Keys.Delete && lbBrushData.Items.Count > 1)
                {
                    int sel = lbBrushData.SelectedIndex;
                    lbBrushData.Items.RemoveAt(sel);
                    this.Options.BrushDataList.RemoveAt(sel);
                }
            }
        }
    }

    public class GradientMakerSettings
    {
        public List<BrushData> BrushDataList { get; set; }
        public int BrushDataSelected { get; set; }

        public GradientMakerSettings()
        {
            this.BrushDataList = new List<BrushData>();
            this.BrushDataList.Add(new BrushData());
            this.BrushDataSelected = 0;
        }

        public BrushData GetBrushDataActive()
        {
            return this.BrushDataList[this.BrushDataSelected];
        }
    }

    public class BrushData
    {
        public string Name { get; set; }
        public string Data { get; set; }
        public GradientDirection Direction { get; set; }

        public BrushData()
        {
            this.Name = "Gradient 1";
            this.Data = "255,68,120,194\t0\n255,13,58,122\t0.5\n255,6,36,78\t0.5\n255,12,76,159\t1";
            this.Direction = BrushData.GradientDirection.Vertical;
        }

        public BrushData(string data, GradientDirection direction)
        {
            this.Data = data;
            this.Direction = direction;
        }

        public enum GradientDirection
        {
            /// <summary>
            /// Specifies a gradient from left to right.
            /// </summary>
            Horizontal,
            /// <summary>
            /// Specifies a gradient from top to bottom.
            /// </summary>
            Vertical
        }

        public override string ToString()
        {
            return this.Name;
        }
    }
}
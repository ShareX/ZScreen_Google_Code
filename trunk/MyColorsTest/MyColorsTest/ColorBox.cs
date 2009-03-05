using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace MyColorsTest
{
    public class ColorBox : UserControl
    {
        #region Variables

        public MyColors.MyColor MyColor { get; set; }
        public MyColors.MyColor MyColor2 { get; set; }
        private Bitmap bmp;

        private eDrawStyle mDrawStyle;

        public eDrawStyle DrawStyle
        {
            get
            {
                return mDrawStyle;
            }
            set
            {
                mDrawStyle = value;
                Refresh();
            }
        }

        public enum eDrawStyle
        {
            Hue, Saturation, Brightness, Red, Green, Blue
        }

        public event EventHandler ColorChanged;

        private bool mouseDown;
        private Point oldMousePosition;

        #endregion

        public ColorBox()
        {
            InitializeComponent();
            this.MyColor = Color.Red;
            this.bmp = new Bitmap(this.Width, this.Height);
            this.DrawStyle = eDrawStyle.Hue;
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            this.Name = "ColorBox";
            this.Size = new System.Drawing.Size(255, 255);
            this.MouseDown += new MouseEventHandler(ColorBox_MouseDown);
            this.MouseMove += new MouseEventHandler(ColorBox_MouseMove);
            this.MouseUp += new MouseEventHandler(ColorBox_MouseUp);
            this.Paint += new PaintEventHandler(ColorBox_Paint);
        }

        #endregion

        #region Events

        private void ColorBox_MouseDown(object sender, MouseEventArgs e)
        {
            mouseDown = true;
            ColorBox_MouseMove(this, e);
        }

        private void ColorBox_MouseMove(object sender, MouseEventArgs e)
        {
            Point mousePosition = GetPoint(e.Location);
            if (mouseDown && (oldMousePosition == null || oldMousePosition != mousePosition))
            {
                MyColor2 = GetColor(mousePosition);
                oldMousePosition = mousePosition;
                if (ColorChanged != null) ColorChanged(this, e);
                Refresh();
            }
        }

        private void ColorBox_MouseUp(object sender, MouseEventArgs e)
        {
            mouseDown = false;
        }

        private void ColorBox_Paint(object sender, PaintEventArgs e)
        {
            if (!this.DesignMode)
            {
                if (!mouseDown) DrawColors();
                e.Graphics.DrawImage(bmp, this.ClientRectangle);
                DrawCrosshair(e.Graphics);
            }
        }

        #endregion

        #region Private Methods

        private void DrawCrosshair(Graphics g)
        {
            Point circlePoint = new Point(oldMousePosition.X - 5, oldMousePosition.Y - 5);

            int average = (MyColor2.RGB.Red + MyColor2.RGB.Green + MyColor2.RGB.Blue) / 3;
            int circ = 5 * 2;

            if (average > 175)
            {
                g.DrawEllipse(Pens.Black, new Rectangle(circlePoint, new Size(circ, circ)));
            }
            else
            {
                g.DrawEllipse(Pens.White, new Rectangle(circlePoint, new Size(circ, circ)));
            }
        }

        private void DrawColors()
        {
            switch (DrawStyle)
            {
                case eDrawStyle.Hue:
                    DrawHue();
                    break;
                case eDrawStyle.Saturation:
                    DrawSaturation();
                    break;
                case eDrawStyle.Brightness:
                    DrawBrightness();
                    break;
                case eDrawStyle.Red:
                    DrawRed();
                    break;
                case eDrawStyle.Green:
                    DrawGreen();
                    break;
                case eDrawStyle.Blue:
                    DrawBlue();
                    break;
            }
        }

        // Saturation = 0 -> 100
        // Brightness = 100 -> 0
        private void DrawHue()
        {
            Graphics g = Graphics.FromImage(bmp);
            MyColors.HSB start = new MyColors.HSB(MyColor.HSB.Hue, 1.0, 0.0);
            MyColors.HSB end = new MyColors.HSB(MyColor.HSB.Hue, 0.0, 0.0);

            for (int i = 0; i < this.Height; i++)
            {
                start.Brightness = end.Brightness = 1.0 - (double)i / this.Height;
                LinearGradientBrush brush = new LinearGradientBrush(new Rectangle(0, 0, this.Width, 1),
                    start.ToColor(), end.ToColor(), 0, false);
                g.FillRectangle(brush, new Rectangle(0, i, this.Width, 1));
            }
        }

        // Hue = 0 -> 360
        // Brightness = 100 -> 0
        private void DrawSaturation()
        {
            Graphics g = Graphics.FromImage(bmp);
            MyColors.HSB start = new MyColors.HSB(0.0, MyColor.HSB.Saturation, 1.0);
            MyColors.HSB end = new MyColors.HSB(0.0, MyColor.HSB.Saturation, 0.0);

            for (int i = 0; i < this.Width; i++)
            {
                start.Hue = end.Hue = (double)i / this.Height;
                LinearGradientBrush brush = new LinearGradientBrush(new Rectangle(0, 0, 1, this.Height),
                    start.ToColor(), end.ToColor(), 90, false);
                g.FillRectangle(brush, new Rectangle(i, 0, 1, this.Height));
            }
        }

        // Hue = 0 -> 360
        // Saturation = 100 -> 0
        private void DrawBrightness()
        {
            Graphics g = Graphics.FromImage(bmp);
            MyColors.HSB start = new MyColors.HSB(0.0, 1.0, MyColor.HSB.Brightness);
            MyColors.HSB end = new MyColors.HSB(0.0, 0.0, MyColor.HSB.Brightness);

            for (int i = 0; i < this.Width; i++)
            {
                start.Hue = end.Hue = (double)i / this.Height;
                LinearGradientBrush brush = new LinearGradientBrush(new Rectangle(0, 0, 1, this.Height),
                    start.ToColor(), end.ToColor(), 90, false);
                g.FillRectangle(brush, new Rectangle(i, 0, 1, this.Height));
            }
        }

        // Blue = 0 -> 255
        // Green = 255 -> 0
        private void DrawRed()
        {
            Graphics g = Graphics.FromImage(bmp);
            MyColors.RGB start = new MyColors.RGB(MyColor.RGB.Red, 0, 0);
            MyColors.RGB end = new MyColors.RGB(MyColor.RGB.Red, 0, 255);

            for (int i = 0; i < this.Height; i++)
            {
                start.Green = end.Green = Round(255 - (255 * (double)i / (this.Height)));
                LinearGradientBrush brush = new LinearGradientBrush(new Rectangle(0, 0, this.Width, 1),
                  start.ToColor(), end.ToColor(), 0, false);
                g.FillRectangle(brush, new Rectangle(0, i, this.Width, 1));
            }
        }

        // Blue = 0 -> 255
        // Red = 255 -> 0
        private void DrawGreen()
        {
            Graphics g = Graphics.FromImage(bmp);
            MyColors.RGB start = new MyColors.RGB(0, MyColor.RGB.Green, 0);
            MyColors.RGB end = new MyColors.RGB(0, MyColor.RGB.Green, 255);

            for (int i = 0; i < this.Height; i++)
            {
                start.Red = end.Red = Round(255 - (255 * (double)i / (this.Height)));
                LinearGradientBrush brush = new LinearGradientBrush(new Rectangle(0, 0, this.Width, 1),
                  start.ToColor(), end.ToColor(), 0, false);
                g.FillRectangle(brush, new Rectangle(0, i, this.Width, 1));
            }
        }

        // Red = 0 -> 255
        // Green = 255 -> 0
        private void DrawBlue()
        {
            Graphics g = Graphics.FromImage(bmp);
            MyColors.RGB start = new MyColors.RGB(0, 0, MyColor.RGB.Blue);
            MyColors.RGB end = new MyColors.RGB(255, 0, MyColor.RGB.Blue);

            for (int i = 0; i < this.Height; i++)
            {
                start.Green = end.Green = Round(255 - (255 * (double)i / (this.Height)));
                LinearGradientBrush brush = new LinearGradientBrush(new Rectangle(0, 0, this.Width, 1),
                  start.ToColor(), end.ToColor(), 0, false);
                g.FillRectangle(brush, new Rectangle(0, i, this.Width, 1));
            }
        }

        private MyColors.MyColor GetColor(Point point)
        {
            return GetColor(point.X, point.Y);
        }

        private MyColors.MyColor GetColor(int x, int y)
        {
            return new MyColors.MyColor(bmp.GetPixel(x, y));
        }

        //private MyColors.MyColor GetColor(int x, int y)
        //{
        //    switch (DrawStyle)
        //    {
        //        case eDrawStyle.Hue:
        //            return new MyColors.MyColor(new MyColors.HSB(MyColor.HSB.Hue,
        //                (double)x / this.Width, 1.0 - (double)y / this.Height));
        //        case eDrawStyle.Saturation:
        //            return new MyColors.MyColor(new MyColors.HSB((double)x / this.Width,
        //                MyColor.HSB.Saturation, 1.0 - (double)y / this.Height));
        //        case eDrawStyle.Brightness:
        //            return new MyColors.MyColor(new MyColors.HSB((double)x / this.Width, 1.0 - (double)y / this.Height, MyColor.HSB.Brightness));
        //        default:
        //            return MyColor;
        //        //case eDrawStyle.Red:
        //        //    _hsl = AdobeColors.RGB_to_HSL(Color.FromArgb(m_rgb.R, Round(255 * (1.0 - (double)y / (this.Height - 4))), Round(255 * (double)x / (this.Width - 4))));
        //        //    break;
        //        //case eDrawStyle.Green:
        //        //    _hsl = AdobeColors.RGB_to_HSL(Color.FromArgb(Round(255 * (1.0 - (double)y / (this.Height - 4))), m_rgb.G, Round(255 * (double)x / (this.Width - 4))));
        //        //    break;
        //        //case eDrawStyle.Blue:
        //        //    _hsl = AdobeColors.RGB_to_HSL(Color.FromArgb(Round(255 * (double)x / (this.Width - 4)), Round(255 * (1.0 - (double)y / (this.Height - 4))), m_rgb.B));
        //        //    break;
        //    }
        //}

        #endregion

        #region Private Helpers

        private Point GetPoint(Point point)
        {
            return new Point(GetBetween(point.X, 0, this.Width - 1), GetBetween(point.Y, 0, this.Height - 1));
        }

        private static int GetBetween(int value, int min, int max)
        {
            return Math.Max(Math.Min(value, max), min);
        }

        private static int Round(double val)
        {
            int ret_val = (int)val;

            int temp = (int)(val * 100);

            if ((temp % 100) >= 50)
                ret_val += 1;

            return ret_val;
        }

        #endregion
    }
}
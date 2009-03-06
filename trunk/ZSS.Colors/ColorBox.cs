using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace ZSS.Colors
{
    public class ColorBox : UserControl
    {
        #region Variables

        public MyColors.MyColor MyColor { get; set; }
        public MyColors.MyColor MyColor2 { get; set; }

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

        private Bitmap bmp;
        private int width;
        private int height;
        private eDrawStyle mDrawStyle;
        private bool mouseDown;
        private bool drawCrosshair;
        private Point oldMousePosition;

        #endregion

        public ColorBox()
        {
            InitializeComponent();
            this.width = this.ClientRectangle.Width;
            this.height = this.ClientRectangle.Height;
            this.bmp = new Bitmap(width, height);
            this.MyColor = Color.Red;
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
            this.SuspendLayout();
            // 
            // ColorBox
            // 
            this.Name = "ColorBox";
            this.Size = new System.Drawing.Size(255, 255);
            this.DoubleBuffered = true;
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.ColorBox_Paint);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.ColorBox_MouseMove);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ColorBox_MouseDown);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ColorBox_MouseUp);
            this.ClientSizeChanged += new System.EventHandler(this.ColorBox_ClientSizeChanged);
            this.ResumeLayout(false);

        }

        private void ColorBox_ClientSizeChanged(object sender, EventArgs e)
        {
            this.width = this.ClientRectangle.Width;
            this.height = this.ClientRectangle.Height;
            this.bmp = new Bitmap(width, height);
            DrawColors();
        }

        #endregion

        #region Events

        private void ColorBox_MouseDown(object sender, MouseEventArgs e)
        {
            if (!drawCrosshair) drawCrosshair = true;
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
                Refresh();
                if (ColorChanged != null) ColorChanged(this, e);
                //Console.WriteLine(width + "-" + this.ClientRectangle.Width + "-" + this.DisplayRectangle.Width);
            }
        }

        private void ColorBox_MouseUp(object sender, MouseEventArgs e)
        {
            mouseDown = false;
        }

        private void ColorBox_Paint(object sender, PaintEventArgs e)
        {
            if (!mouseDown) DrawColors();
            e.Graphics.DrawImage(bmp, this.ClientRectangle);
            if (drawCrosshair) DrawCrosshair(e.Graphics);
        }

        #endregion

        #region Private Methods

        private void DrawCrosshair(Graphics g)
        {
            g.DrawEllipse(new Pen(Color.Black), new Rectangle(new Point(oldMousePosition.X - 6, oldMousePosition.Y - 6),
                new Size(12, 12)));
            g.DrawEllipse(new Pen(Color.White), new Rectangle(new Point(oldMousePosition.X - 5, oldMousePosition.Y - 5),
                new Size(10, 10)));
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
            MyColors.HSB start = new MyColors.HSB(MyColor.HSB.Hue, 0.0, 0.0);
            MyColors.HSB end = new MyColors.HSB(MyColor.HSB.Hue, 1.0, 0.0);

            for (int i = 0; i < height; i++)
            {
                start.Brightness = end.Brightness = 1.0 - (double)i / height;
                LinearGradientBrush brush = new LinearGradientBrush(new Rectangle(0, 0, width, 1),
                    start.ToColor(), end.ToColor(), 0, false);
                g.FillRectangle(brush, new Rectangle(0, i, width, 1));
            }
        }

        // Hue = 0 -> 360
        // Brightness = 100 -> 0
        private void DrawSaturation()
        {
            Graphics g = Graphics.FromImage(bmp);
            MyColors.HSB start = new MyColors.HSB(0.0, MyColor.HSB.Saturation, 1.0);
            MyColors.HSB end = new MyColors.HSB(0.0, MyColor.HSB.Saturation, 0.0);

            for (int i = 0; i < width; i++)
            {
                start.Hue = end.Hue = (double)i / height;
                LinearGradientBrush brush = new LinearGradientBrush(new Rectangle(0, 0, 1, height),
                    start.ToColor(), end.ToColor(), 90, false);
                g.FillRectangle(brush, new Rectangle(i, 0, 1, height));
            }
        }

        // Hue = 0 -> 360
        // Saturation = 100 -> 0
        private void DrawBrightness()
        {
            Graphics g = Graphics.FromImage(bmp);
            MyColors.HSB start = new MyColors.HSB(0.0, 1.0, MyColor.HSB.Brightness);
            MyColors.HSB end = new MyColors.HSB(0.0, 0.0, MyColor.HSB.Brightness);

            for (int i = 0; i < width; i++)
            {
                start.Hue = end.Hue = (double)i / height;
                LinearGradientBrush brush = new LinearGradientBrush(new Rectangle(0, 0, 1, height),
                    start.ToColor(), end.ToColor(), 90, false);
                g.FillRectangle(brush, new Rectangle(i, 0, 1, height));
            }
        }

        // Blue = 0 -> 255
        // Green = 255 -> 0
        private void DrawRed()
        {
            Graphics g = Graphics.FromImage(bmp);
            MyColors.RGB start = new MyColors.RGB(MyColor.RGB.Red, 0, 0);
            MyColors.RGB end = new MyColors.RGB(MyColor.RGB.Red, 0, 255);

            for (int i = 0; i < height; i++)
            {
                start.Green = end.Green = Round(255 - (255 * (double)i / (height)));
                LinearGradientBrush brush = new LinearGradientBrush(new Rectangle(0, 0, width, 1),
                  start.ToColor(), end.ToColor(), 0, false);
                g.FillRectangle(brush, new Rectangle(0, i, width, 1));
            }
        }

        // Blue = 0 -> 255
        // Red = 255 -> 0
        private void DrawGreen()
        {
            Graphics g = Graphics.FromImage(bmp);
            MyColors.RGB start = new MyColors.RGB(0, MyColor.RGB.Green, 0);
            MyColors.RGB end = new MyColors.RGB(0, MyColor.RGB.Green, 255);

            for (int i = 0; i < height; i++)
            {
                start.Red = end.Red = Round(255 - (255 * (double)i / (height)));
                LinearGradientBrush brush = new LinearGradientBrush(new Rectangle(0, 0, width, 1),
                  start.ToColor(), end.ToColor(), 0, false);
                g.FillRectangle(brush, new Rectangle(0, i, width, 1));
            }
        }

        // Red = 0 -> 255
        // Green = 255 -> 0
        private void DrawBlue()
        {
            Graphics g = Graphics.FromImage(bmp);
            MyColors.RGB start = new MyColors.RGB(0, 0, MyColor.RGB.Blue);
            MyColors.RGB end = new MyColors.RGB(255, 0, MyColor.RGB.Blue);

            for (int i = 0; i < height; i++)
            {
                start.Green = end.Green = Round(255 - (255 * (double)i / (height)));
                LinearGradientBrush brush = new LinearGradientBrush(new Rectangle(0, 0, width, 1),
                  start.ToColor(), end.ToColor(), 0, false);
                g.FillRectangle(brush, new Rectangle(0, i, width, 1));
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

        #endregion

        #region Private Helpers

        private Point GetPoint(Point point)
        {
            return new Point(GetBetween(point.X, 0, width - 1), GetBetween(point.Y, 0, height - 1));
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
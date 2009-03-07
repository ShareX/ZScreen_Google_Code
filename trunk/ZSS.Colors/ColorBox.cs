using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace ZSS.Colors
{
    public class ColorBox : UserControl
    {
        public ColorBox()
        {
            InitializeComponent();
            this.width = this.ClientRectangle.Width;
            this.height = this.ClientRectangle.Height;
            this.bmp = new Bitmap(width, height);
            this.SetColor = Color.Red;
            this.GetColor = this.SetColor;
            this.DrawStyle = DrawStyle.Hue;
        }

        #region Variables

        public MyColors.MyColor SetColor
        {
            get
            {
                return mSetColor;
            }
            set
            {
                mSetColor = value;
                Refresh();
            }
        }

        public MyColors.MyColor GetColor { get; set; }

        public DrawStyle DrawStyle
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

        public event EventHandler ColorChanged;

        private Bitmap bmp;
        private int width;
        private int height;
        private MyColors.MyColor mSetColor;
        private DrawStyle mDrawStyle;
        private bool mouseDown;
        private bool drawCrosshair;
        private Point oldMousePosition;

        #endregion

        #region Component Designer generated code

        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.SuspendLayout();
            //components = new System.ComponentModel.Container();

            this.Name = "ColorBox";
            this.Size = new System.Drawing.Size(255, 255);
            this.DoubleBuffered = true;
            this.ClientSizeChanged += new System.EventHandler(this.ColorBox_ClientSizeChanged);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ColorBox_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.ColorBox_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ColorBox_MouseUp);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.ColorBox_Paint);

            this.ResumeLayout(false);
        }

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

        #endregion

        #region Events

        private void ColorBox_ClientSizeChanged(object sender, EventArgs e)
        {
            this.width = this.ClientRectangle.Width;
            this.height = this.ClientRectangle.Height;
            this.bmp = new Bitmap(width, height);
            DrawColors();
        }

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
                GetColor = GetPointColor(mousePosition);
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
            DrawEllipse(g, 6, Color.Black);
            DrawEllipse(g, 5, Color.White);
        }

        private void DrawEllipse(Graphics g, int size, Color color)
        {
            g.DrawEllipse(new Pen(color), new Rectangle(new Point(oldMousePosition.X - size, oldMousePosition.Y - size),
                new Size(size * 2, size * 2)));
        }

        private void DrawColors()
        {
            switch (DrawStyle)
            {
                case DrawStyle.Hue:
                    DrawHue();
                    break;
                case DrawStyle.Saturation:
                    DrawSaturation();
                    break;
                case DrawStyle.Brightness:
                    DrawBrightness();
                    break;
                case DrawStyle.Red:
                    DrawRed();
                    break;
                case DrawStyle.Green:
                    DrawGreen();
                    break;
                case DrawStyle.Blue:
                    DrawBlue();
                    break;
            }
        }

        // Saturation = 0 -> 100
        // Brightness = 100 -> 0
        private void DrawHue()
        {
            Graphics g = Graphics.FromImage(bmp);
            MyColors.HSB start = new MyColors.HSB(SetColor.HSB.Hue, 0.0, 0.0);
            MyColors.HSB end = new MyColors.HSB(SetColor.HSB.Hue, 1.0, 0.0);

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
            MyColors.HSB start = new MyColors.HSB(0.0, SetColor.HSB.Saturation, 1.0);
            MyColors.HSB end = new MyColors.HSB(0.0, SetColor.HSB.Saturation, 0.0);

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
            MyColors.HSB start = new MyColors.HSB(0.0, 1.0, SetColor.HSB.Brightness);
            MyColors.HSB end = new MyColors.HSB(0.0, 0.0, SetColor.HSB.Brightness);

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
            MyColors.RGB start = new MyColors.RGB(SetColor.RGB.Red, 0, 0);
            MyColors.RGB end = new MyColors.RGB(SetColor.RGB.Red, 0, 255);

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
            MyColors.RGB start = new MyColors.RGB(0, SetColor.RGB.Green, 0);
            MyColors.RGB end = new MyColors.RGB(0, SetColor.RGB.Green, 255);

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
            MyColors.RGB start = new MyColors.RGB(0, 0, SetColor.RGB.Blue);
            MyColors.RGB end = new MyColors.RGB(255, 0, SetColor.RGB.Blue);

            for (int i = 0; i < height; i++)
            {
                start.Green = end.Green = Round(255 - (255 * (double)i / (height)));
                LinearGradientBrush brush = new LinearGradientBrush(new Rectangle(0, 0, width, 1),
                  start.ToColor(), end.ToColor(), 0, false);
                g.FillRectangle(brush, new Rectangle(0, i, width, 1));
            }
        }

        #endregion

        #region Private Helpers

        private MyColors.MyColor GetPointColor(Point point)
        {
            return GetPointColor(point.X, point.Y);
        }

        private MyColors.MyColor GetPointColor(int x, int y)
        {
            return new MyColors.MyColor(bmp.GetPixel(x, y));
        }

        private Point GetPoint(Point point)
        {
            return new Point(GetBetween(point.X, 0, width - 1), GetBetween(point.Y, 0, height - 1));
        }

        private int GetBetween(int value, int min, int max)
        {
            return Math.Max(Math.Min(value, max), min);
        }

        private int Round(double val)
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
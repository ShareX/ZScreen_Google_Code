using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace ZSS.Colors
{
    public class ColorSlider : UserControl
    {
        public ColorSlider()
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

            this.Name = "ColorSlider";
            this.Size = new System.Drawing.Size(30, 255);
            this.DoubleBuffered = true;
            this.ClientSizeChanged += new System.EventHandler(this.ColorSlider_ClientSizeChanged);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ColorSlider_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.ColorSlider_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ColorSlider_MouseUp);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.ColorSlider_Paint);

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

        private void ColorSlider_ClientSizeChanged(object sender, EventArgs e)
        {
            this.width = this.ClientRectangle.Width;
            this.height = this.ClientRectangle.Height;
            this.bmp = new Bitmap(width, height);
            DrawColors();
        }

        private void ColorSlider_MouseDown(object sender, MouseEventArgs e)
        {
            if (!drawCrosshair) drawCrosshair = true;
            mouseDown = true;
            ColorSlider_MouseMove(this, e);
        }

        private void ColorSlider_MouseMove(object sender, MouseEventArgs e)
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

        private void ColorSlider_MouseUp(object sender, MouseEventArgs e)
        {
            mouseDown = false;
        }

        private void ColorSlider_Paint(object sender, PaintEventArgs e)
        {
            if (!mouseDown) DrawColors();
            e.Graphics.DrawImage(bmp, this.ClientRectangle);
            if (drawCrosshair) DrawCrosshair(e.Graphics);
        }

        #endregion

        #region Private Methods

        private void DrawCrosshair(Graphics g)
        {
            int rectOffset = 3;
            int rectSize = 4;
            g.DrawRectangle(new Pen(Color.Black), new Rectangle(rectOffset, oldMousePosition.Y - rectSize,
                width - rectOffset * 2, rectSize * 2 + 1));
            g.DrawRectangle(new Pen(Color.White), new Rectangle(rectOffset + 1, oldMousePosition.Y - rectSize + 1,
                width - rectOffset * 2 - 2, rectSize * 2 - 1));
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

        // Hue = 360 -> 0
        private void DrawHue()
        {
            Graphics g = Graphics.FromImage(bmp);
            MyColors.HSB color = new MyColors.HSB(0.0, 1.0, 1.0);

            for (int i = 0; i < height; i++)
            {
                color.Hue = 1.0 - (double)i / height;
                g.DrawLine(new Pen(color), 0, i, width, i);
            }
        }

        // Saturation = 100 -> 0
        private void DrawSaturation()
        {
            Graphics g = Graphics.FromImage(bmp);
            MyColors.HSB color = new MyColors.HSB(SetColor.HSB.Hue, 0.0, SetColor.HSB.Brightness);

            for (int i = 0; i < height; i++)
            {
                color.Saturation = 1.0 - (double)i / height;
                g.DrawLine(new Pen(color), 0, i, width, i);
            }
        }

        // Brightness = 100 -> 0
        private void DrawBrightness()
        {
            Graphics g = Graphics.FromImage(bmp);
            MyColors.HSB color = new MyColors.HSB(SetColor.HSB.Hue, SetColor.HSB.Saturation, 0.0);

            for (int i = 0; i < height; i++)
            {
                color.Brightness = 1.0 - (double)i / height;
                g.DrawLine(new Pen(color), 0, i, width, i);
            }
        }

        // Red = 255 -> 0
        private void DrawRed()
        {
            Graphics g = Graphics.FromImage(bmp);
            MyColors.RGB color = new MyColors.RGB(0, SetColor.RGB.Green, SetColor.RGB.Blue);

            for (int i = 0; i < height; i++)
            {
                color.Red = 255 - Round(255 * (double)i / height);
                g.DrawLine(new Pen(color), 0, i, width, i);
            }
        }

        // Green = 255 -> 0
        private void DrawGreen()
        {
            Graphics g = Graphics.FromImage(bmp);
            MyColors.RGB color = new MyColors.RGB(SetColor.RGB.Red, 0, SetColor.RGB.Blue);

            for (int i = 0; i < height; i++)
            {
                color.Green = 255 - Round(255 * (double)i / height);
                g.DrawLine(new Pen(color), 0, i, width, i);
            }
        }

        // Blue = 255 -> 0
        private void DrawBlue()
        {
            Graphics g = Graphics.FromImage(bmp);
            MyColors.RGB color = new MyColors.RGB(SetColor.RGB.Red, SetColor.RGB.Green, 0);

            for (int i = 0; i < height; i++)
            {
                color.Blue = 255 - Round(255 * (double)i / height);
                g.DrawLine(new Pen(color), 0, i, width, i);
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
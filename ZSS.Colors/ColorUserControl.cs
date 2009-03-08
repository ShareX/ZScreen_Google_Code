using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Diagnostics;

namespace ZSS.Colors
{
    public class ColorUserControl : UserControl
    {
        public event EventHandler ColorChanged;

        protected Bitmap bmp;
        protected int width;
        protected int height;
        protected DrawStyle mDrawStyle;
        protected MyColor mSetColor;
        protected bool mouseDown;
        protected bool drawCrosshair;
        protected Point oldMousePosition;

        public MyColor SetColor
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

        public MyColor GetColor { get; set; }

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

        #region Component Designer generated code

        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        protected virtual void Initialize()
        {
            this.SuspendLayout();

            this.DoubleBuffered = true;
            this.width = this.ClientRectangle.Width;
            this.height = this.ClientRectangle.Height;
            this.bmp = new Bitmap(width, height);
            this.SetColor = Color.Red;
            this.GetColor = this.SetColor;
            this.DrawStyle = DrawStyle.Hue;

            this.ClientSizeChanged += new System.EventHandler(this.EventClientSizeChanged);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.EventMouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.EventMouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.EventMouseUp);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.EventPaint);

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

        private void EventClientSizeChanged(object sender, EventArgs e)
        {
            this.width = this.ClientRectangle.Width;
            this.height = this.ClientRectangle.Height;
            this.bmp = new Bitmap(width, height);
            DrawColors();
        }

        private void EventMouseDown(object sender, MouseEventArgs e)
        {
            if (!drawCrosshair) drawCrosshair = true;
            mouseDown = true;
            EventMouseMove(this, e);
        }

        private void EventMouseMove(object sender, MouseEventArgs e)
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

        private void EventMouseUp(object sender, MouseEventArgs e)
        {
            mouseDown = false;
        }

        private void EventPaint(object sender, PaintEventArgs e)
        {
            if (!mouseDown) DrawColors();
            e.Graphics.DrawImage(bmp, this.ClientRectangle);
            if (drawCrosshair) DrawCrosshair(e.Graphics);
        }

        #endregion

        #region Protected Methods

        protected void DrawColors()
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

        #endregion

        #region Protected Helpers

        protected void DrawEllipse(Graphics g, int size, Color color)
        {
            g.DrawEllipse(new Pen(color), new Rectangle(new Point(oldMousePosition.X - size, oldMousePosition.Y - size),
                new Size(size * 2, size * 2)));
        }

        protected MyColor GetPointColor(Point point)
        {
            return GetPointColor(point.X, point.Y);
        }

        protected MyColor GetPointColor(int x, int y)
        {
            return new MyColor(bmp.GetPixel(x, y));
        }

        protected Point GetPoint(Point point)
        {
            return new Point(GetBetween(point.X, 0, width - 1), GetBetween(point.Y, 0, height - 1));
        }

        protected int GetBetween(int value, int min, int max)
        {
            return Math.Max(Math.Min(value, max), min);
        }

        protected int Round(double val)
        {
            int ret_val = (int)val;

            int temp = (int)(val * 100);

            if ((temp % 100) >= 50)
                ret_val += 1;

            return ret_val;
        }

        #endregion

        #region Protected Virtual Members

        protected virtual void DrawCrosshair(Graphics g) { }
        protected virtual void DrawHue() { }
        protected virtual void DrawSaturation() { }
        protected virtual void DrawBrightness() { }
        protected virtual void DrawRed() { }
        protected virtual void DrawGreen() { }
        protected virtual void DrawBlue() { }

        #endregion
    }
}
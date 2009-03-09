#region License Information (GPL v2)
/*
    ZScreen - A program that allows you to upload screenshots in one keystroke.
    Copyright (C) 2008-2009  Jaex (Berk)

    This program is free software; you can redistribute it and/or
    modify it under the terms of the GNU General Public License
    as published by the Free Software Foundation; either version 2
    of the License, or (at your option) any later version.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with this program; if not, write to the Free Software
    Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301, USA.
    
    Optionally you can also view the license at <http://www.gnu.org/licenses/>.
*/
#endregion

using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace ZSS.Colors
{
    public class ColorUserControl : UserControl
    {
        #region Variables

        public event ColorEventHandler ColorChanged;
        public bool drawCrosshair;

        protected Bitmap bmp;
        protected int width;
        protected int height;
        protected DrawStyle mDrawStyle;
        protected MyColor mSetColor;
        protected bool mouseDown;
        protected Point lastPos;
        protected Timer MouseMoveTimer;

        public MyColor SetColor
        {
            get
            {
                return mSetColor;
            }
            set
            {
                mSetColor = value;
                if (this is ColorBox)
                {
                    SetBoxMarker();
                }
                else
                {
                    SetSliderMarker();
                }
                Refresh();
                GetPointColor(lastPos);
                ThrowEvent(false);
            }
        }

        public DrawStyle DrawStyle
        {
            get
            {
                return mDrawStyle;
            }
            set
            {
                mDrawStyle = value;

                if (this is ColorBox)
                {
                    SetBoxMarker();
                }
                else
                {
                    SetSliderMarker();
                }
                Refresh();
                if (drawCrosshair)
                {
                    GetPointColor(lastPos);
                    ThrowEvent();
                }
            }
        }

        #endregion

        #region Component Designer generated code

        private System.ComponentModel.IContainer components = null;

        protected virtual void Initialize()
        {
            this.SuspendLayout();

            this.DoubleBuffered = true;
            this.width = this.ClientRectangle.Width;
            this.height = this.ClientRectangle.Height;
            this.bmp = new Bitmap(width, height);
            this.SetColor = Color.Red;
            this.DrawStyle = DrawStyle.Hue;

            MouseMoveTimer = new Timer();
            MouseMoveTimer.Interval = 10;
            MouseMoveTimer.Tick += new EventHandler(MouseMoveTimer_Tick);

            this.ClientSizeChanged += new System.EventHandler(this.EventClientSizeChanged);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.EventMouseDown);
            this.MouseEnter += new EventHandler(this.EventMouseEnter);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.EventMouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.EventMouseUp);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.EventPaint);

            this.ResumeLayout(false);
        }

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
            drawCrosshair = true;
            mouseDown = true;
            MouseMoveTimer.Start();
            //EventMouseMove(this, e);
        }

        private void EventMouseEnter(object sender, EventArgs e)
        {
            if (this is ColorBox)
            {
                this.Cursor = new Cursor(Helpers.GetImageResource("ZSS.Colors.Cursor.cur"));
            }
        }

        private void EventMouseMove(object sender, MouseEventArgs e)
        {

        }

        private void EventMouseUp(object sender, MouseEventArgs e)
        {
            mouseDown = false;
            MouseMoveTimer.Stop();
        }

        private void EventPaint(object sender, PaintEventArgs e)
        {
            if (!mouseDown) DrawColors();
            e.Graphics.DrawImage(bmp, this.ClientRectangle);
            if (drawCrosshair) DrawCrosshair(e.Graphics);
        }

        private void MouseMoveTimer_Tick(object sender, EventArgs e)
        {
            Point mousePosition = GetPoint(this.PointToClient(MousePosition));
            if (mouseDown && (lastPos == null || lastPos != mousePosition))
            {
                GetPointColor(mousePosition);
                ThrowEvent();
                Refresh();
                //Console.WriteLine(width + "-" + this.ClientRectangle.Width + "-" + this.DisplayRectangle.Width);
            }
        }

        #endregion

        #region Protected Methods

        protected void ThrowEvent()
        {
            ThrowEvent(true);
        }

        protected void ThrowEvent(bool updateControl)
        {
            if (ColorChanged != null)
            {
                ColorChanged(this, new ColorEventArgs(SetColor, DrawStyle, updateControl));
            }
        }

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

        protected void SetBoxMarker()
        {
            switch (DrawStyle)
            {
                case DrawStyle.Hue:
                    lastPos.X = Round(width * SetColor.HSB.Saturation);
                    lastPos.Y = Round(height * (1.0 - SetColor.HSB.Brightness));
                    break;
                case DrawStyle.Saturation:
                    lastPos.X = Round(width * SetColor.HSB.Hue);
                    lastPos.Y = Round(height * (1.0 - SetColor.HSB.Brightness));
                    break;
                case DrawStyle.Brightness:
                    lastPos.X = Round(width * SetColor.HSB.Hue);
                    lastPos.Y = Round(height * (1.0 - SetColor.HSB.Saturation));
                    break;
                case DrawStyle.Red:
                    lastPos.X = Round(width * (double)SetColor.RGB.Blue / 255);
                    lastPos.Y = Round(height * (1.0 - (double)SetColor.RGB.Green / 255));
                    break;
                case DrawStyle.Green:
                    lastPos.X = Round(width * (double)SetColor.RGB.Blue / 255);
                    lastPos.Y = Round(height * (1.0 - (double)SetColor.RGB.Red / 255));
                    break;
                case DrawStyle.Blue:
                    lastPos.X = Round(width * (double)SetColor.RGB.Red / 255);
                    lastPos.Y = Round(height * (1.0 - (double)SetColor.RGB.Green / 255));
                    break;
            }
            lastPos = GetPoint(lastPos);
        }

        protected void GetBoxColor()
        {
            switch (DrawStyle)
            {
                case DrawStyle.Hue:
                    mSetColor.HSB.Saturation = (double)lastPos.X / width;
                    mSetColor.HSB.Brightness = 1.0 - (double)lastPos.Y / height;
                    mSetColor.HSBUpdate();
                    break;
                case DrawStyle.Saturation:
                    mSetColor.HSB.Hue = (double)lastPos.X / width;
                    mSetColor.HSB.Brightness = 1.0 - (double)lastPos.Y / height;
                    mSetColor.HSBUpdate();
                    break;
                case DrawStyle.Brightness:
                    mSetColor.HSB.Hue = (double)lastPos.X / width;
                    mSetColor.HSB.Saturation = 1.0 - (double)lastPos.Y / height;
                    mSetColor.HSBUpdate();
                    break;
                case DrawStyle.Red:
                    mSetColor.RGB.Blue = Round(255 * (double)lastPos.X / width);
                    mSetColor.RGB.Green = Round(255 * (1.0 - (double)lastPos.Y / height));
                    mSetColor.RGBUpdate();
                    break;
                case DrawStyle.Green:
                    mSetColor.RGB.Blue = Round(255 * (double)lastPos.X / width);
                    mSetColor.RGB.Red = Round(255 * (1.0 - (double)lastPos.Y / height));
                    mSetColor.RGBUpdate();
                    break;
                case DrawStyle.Blue:
                    mSetColor.RGB.Red = Round(255 * (double)lastPos.X / width);
                    mSetColor.RGB.Green = Round(255 * (1.0 - (double)lastPos.Y / height));
                    mSetColor.RGBUpdate();
                    break;
            }
        }

        protected void SetSliderMarker()
        {
            switch (DrawStyle)
            {
                case DrawStyle.Hue:
                    lastPos.Y = height - Round(height * SetColor.HSB.Hue);
                    break;
                case DrawStyle.Saturation:
                    lastPos.Y = height - Round(height * SetColor.HSB.Saturation);
                    break;
                case DrawStyle.Brightness:
                    lastPos.Y = height - Round(height * SetColor.HSB.Brightness);
                    break;
                case DrawStyle.Red:
                    lastPos.Y = height - Round(height * (double)SetColor.RGB.Red / 255);
                    break;
                case DrawStyle.Green:
                    lastPos.Y = height - Round(height * (double)SetColor.RGB.Green / 255);
                    break;
                case DrawStyle.Blue:
                    lastPos.Y = height - Round(height * (double)SetColor.RGB.Blue / 255);
                    break;
            }
            lastPos = GetPoint(lastPos);
        }

        protected void GetSliderColor()
        {
            switch (DrawStyle)
            {
                case DrawStyle.Hue:
                    mSetColor.HSB.Hue = 1.0 - (double)lastPos.Y / height;
                    mSetColor.HSBUpdate();
                    break;
                case DrawStyle.Saturation:
                    mSetColor.HSB.Saturation = 1.0 - (double)lastPos.Y / height;
                    mSetColor.HSBUpdate();
                    break;
                case DrawStyle.Brightness:
                    mSetColor.HSB.Brightness = 1.0 - (double)lastPos.Y / height;
                    mSetColor.HSBUpdate();
                    break;
                case DrawStyle.Red:
                    mSetColor.RGB.Red = 255 - Round(255 * (double)lastPos.Y / height);
                    mSetColor.RGBUpdate();
                    break;
                case DrawStyle.Green:
                    mSetColor.RGB.Green = 255 - Round(255 * (double)lastPos.Y / height);
                    mSetColor.RGBUpdate();
                    break;
                case DrawStyle.Blue:
                    mSetColor.RGB.Blue = 255 - Round(255 * (double)lastPos.Y / height);
                    mSetColor.RGBUpdate();
                    break;
            }
        }

        #endregion

        #region Protected Helpers

        protected void DrawEllipse(Graphics g, int size, Color color)
        {
            g.DrawEllipse(new Pen(color), new Rectangle(new Point(lastPos.X - size, lastPos.Y - size),
                new Size(size * 2, size * 2)));
        }

        protected void GetPointColor(Point point)
        {
            lastPos = point;
            if (this is ColorBox)
            {
                GetBoxColor();
            }
            else
            {
                GetSliderColor();
            }
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
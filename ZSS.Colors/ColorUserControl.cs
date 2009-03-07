using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Diagnostics;

namespace ZSS.Colors
{
    public class ColorUserControl : UserControl, IColorUserControl
    {
        protected Bitmap bmp;
        protected int width;
        protected int height;
        protected DrawStyle mDrawStyle;
        protected MyColors.MyColor mSetColor;
        protected bool mouseDown;
        protected bool drawCrosshair;
        protected Point oldMousePosition;

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

        protected MyColors.MyColor GetPointColor(Point point)
        {
            return GetPointColor(point.X, point.Y);
        }

        protected MyColors.MyColor GetPointColor(int x, int y)
        {
            return new MyColors.MyColor(bmp.GetPixel(x, y));
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

        #region IColorUserControl Members

        public virtual void DrawHue() { ThrowMustOverrideError(new StackFrame(1, true)); }
        public virtual void DrawSaturation() { ThrowMustOverrideError(new StackFrame(1, true)); }
        public virtual void DrawBrightness() { ThrowMustOverrideError(new StackFrame(1, true)); }
        public virtual void DrawRed() { ThrowMustOverrideError(new StackFrame(1, true)); }
        public virtual void DrawGreen() { ThrowMustOverrideError(new StackFrame(1, true)); }
        public virtual void DrawBlue() { ThrowMustOverrideError(new StackFrame(1, true)); }

        private void ThrowMustOverrideError(StackFrame fr)
        {
            StackTrace st = new StackTrace(fr);
            throw new Exception(string.Format("need to override inherited virtual method {0}", fr.GetMethod().Name));
        }

        #endregion


    }
}

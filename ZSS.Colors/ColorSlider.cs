using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace ZSS.Colors
{
    public class ColorSlider : ColorUserControl
    {
        public ColorSlider()
        {
            Initialize();
        }

        #region Protected Override Methods

        protected override void Initialize()
        {
            this.Name = "ColorSlider";
            this.Size = new System.Drawing.Size(30, 255);
            base.Initialize();
        }

        protected override void DrawCrosshair(Graphics g)
        {
            int rectOffset = 3;
            int rectSize = 4;
            g.DrawRectangle(new Pen(Color.Black), new Rectangle(rectOffset, oldMousePosition.Y - rectSize,
                width - rectOffset * 2, rectSize * 2 + 1));
            g.DrawRectangle(new Pen(Color.White), new Rectangle(rectOffset + 1, oldMousePosition.Y - rectSize + 1,
                width - rectOffset * 2 - 2, rectSize * 2 - 1));
        }

        // Hue = 360 -> 0
        protected override void DrawHue()
        {
            Graphics g = Graphics.FromImage(bmp);
            HSB color = new HSB(0.0, 1.0, 1.0);

            for (int i = 0; i < height; i++)
            {
                color.Hue = 1.0 - (double)i / height;
                g.DrawLine(new Pen(color), 0, i, width, i);
            }
        }

        // Saturation = 100 -> 0
        protected override void DrawSaturation()
        {
            Graphics g = Graphics.FromImage(bmp);
            HSB color = new HSB(SetColor.HSB.Hue, 0.0, SetColor.HSB.Brightness);

            for (int i = 0; i < height; i++)
            {
                color.Saturation = 1.0 - (double)i / height;
                g.DrawLine(new Pen(color), 0, i, width, i);
            }
        }

        // Brightness = 100 -> 0
        protected override void DrawBrightness()
        {
            Graphics g = Graphics.FromImage(bmp);
            HSB color = new HSB(SetColor.HSB.Hue, SetColor.HSB.Saturation, 0.0);

            for (int i = 0; i < height; i++)
            {
                color.Brightness = 1.0 - (double)i / height;
                g.DrawLine(new Pen(color), 0, i, width, i);
            }
        }

        // Red = 255 -> 0
        protected override void DrawRed()
        {
            Graphics g = Graphics.FromImage(bmp);
            RGB color = new RGB(0, SetColor.RGB.Green, SetColor.RGB.Blue);

            for (int i = 0; i < height; i++)
            {
                color.Red = 255 - Round(255 * (double)i / height);
                g.DrawLine(new Pen(color), 0, i, width, i);
            }
        }

        // Green = 255 -> 0
        protected override void DrawGreen()
        {
            Graphics g = Graphics.FromImage(bmp);
            RGB color = new RGB(SetColor.RGB.Red, 0, SetColor.RGB.Blue);

            for (int i = 0; i < height; i++)
            {
                color.Green = 255 - Round(255 * (double)i / height);
                g.DrawLine(new Pen(color), 0, i, width, i);
            }
        }

        // Blue = 255 -> 0
        protected override void DrawBlue()
        {
            Graphics g = Graphics.FromImage(bmp);
            RGB color = new RGB(SetColor.RGB.Red, SetColor.RGB.Green, 0);

            for (int i = 0; i < height; i++)
            {
                color.Blue = 255 - Round(255 * (double)i / height);
                g.DrawLine(new Pen(color), 0, i, width, i);
            }
        }

        #endregion
    }
}
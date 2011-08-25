#region License Information (GPL v2)

/*
    ZUploader - A program that allows you to upload images, texts or files
    Copyright (C) 2008-2011 ZScreen Developers

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

#endregion License Information (GPL v2)

using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace RegionCapture
{
    public static class Helpers
    {
        public static Bitmap GetScreenshot()
        {
            Rectangle bounds = GetScreenBounds();

            Bitmap screenshot = new Bitmap(bounds.Width, bounds.Height, PixelFormat.Format32bppArgb);

            using (Graphics g = Graphics.FromImage(screenshot))
            {
                g.CopyFromScreen(bounds.Location, Point.Empty, bounds.Size, CopyPixelOperation.SourceCopy);
            }

            return screenshot;
        }

        public static Rectangle GetScreenBounds()
        {
            return SystemInformation.VirtualScreen;
        }

        public static Rectangle CreateRectangle(int x, int y, int x2, int y2)
        {
            int width = x2 - x + 1;
            int height = y2 - y + 1;

            if (width < 0)
            {
                x += width;
                width = -width;
            }

            if (height < 0)
            {
                y += height;
                height = -height;
            }

            return new Rectangle(x, y, width, height);
        }

        public static Rectangle CreateRectangle(Point pos, Point pos2)
        {
            return CreateRectangle(pos.X, pos.Y, pos2.X, pos2.Y);
        }

        public static Rectangle FixRectangle(int x, int y, int width, int height)
        {
            if (width < 0) x += width;
            if (height < 0) y += height;

            return new Rectangle(x, y, Math.Abs(width), Math.Abs(height));
        }

        public static Rectangle FixRectangle(Rectangle rect)
        {
            return FixRectangle(rect.X, rect.Y, rect.Width, rect.Height);
        }

        public static Image CropImage(Image img, Rectangle rect)
        {
            if (img != null && rect.Width > 0 && rect.Height > 0)
            {
                using (Bitmap bmp = new Bitmap(img))
                {
                    return bmp.Clone(rect, bmp.PixelFormat);
                }
            }

            return null;
        }

        public static Image CropImage(Image img, Rectangle rect, GraphicsPath gp)
        {
            if (img != null && rect.Width > 0 && rect.Height > 0 && gp != null)
            {
                Bitmap bmp = new Bitmap(rect.Width + 1, rect.Height + 1);

                using (Graphics g = Graphics.FromImage(bmp))
                {
                    g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    g.SmoothingMode = SmoothingMode.HighQuality;

                    using (Region region = new Region(gp))
                    {
                        g.Clip = region;
                        g.DrawImage(img, new Rectangle(0, 0, rect.Width, rect.Height), rect, GraphicsUnit.Pixel);
                    }
                }

                return bmp;
            }

            return null;
        }

        public static Image DrawBorder(Image img, GraphicsPath gp)
        {
            if (img != null && gp != null)
            {
                Bitmap bmp = new Bitmap(img);

                using (Graphics g = Graphics.FromImage(bmp))
                {
                    g.SmoothingMode = SmoothingMode.HighQuality;
                    g.DrawPath(Pens.Black, gp);
                }

                return bmp;
            }

            return null;
        }

        public static Image DrawBorder(Image img)
        {
            Bitmap bmp = new Bitmap(img);

            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.DrawRectangle(Pens.Black, 0, 0, img.Width - 1, img.Height - 1);
            }

            return bmp;
        }

        public static Image DrawCheckers(Image img)
        {
            Bitmap bmp = new Bitmap(img.Width, img.Height);

            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.SmoothingMode = SmoothingMode.HighQuality;

                Image checker = CreateCheckers(8, Color.LightGray, Color.White);
                Brush checkerBrush = new TextureBrush(checker, WrapMode.Tile);

                g.FillRectangle(checkerBrush, new Rectangle(0, 0, bmp.Width, bmp.Height));
                g.DrawImage(img, 0, 0);
            }

            return bmp;
        }

        public static Image CreateCheckers(int size, Color color1, Color color2)
        {
            Bitmap bmp = new Bitmap(size * 2, size * 2);

            using (Graphics g = Graphics.FromImage(bmp))
            using (Brush brush1 = new SolidBrush(color1))
            using (Brush brush2 = new SolidBrush(color2))
            {
                g.FillRectangle(brush1, 0, 0, size, size);
                g.FillRectangle(brush1, size, size, size, size);

                g.FillRectangle(brush2, size, 0, size, size);
                g.FillRectangle(brush2, 0, size, size, size);
            }

            return bmp;
        }

        public static void DrawTextWithShadow(Graphics g, string text, PointF position, Font font, Color textColor, Color shadowColor, int shadowOffset = 1)
        {
            Brush shadowBrush = new SolidBrush(shadowColor);
            g.DrawString(text, font, shadowBrush, position.X - shadowOffset, position.Y - shadowOffset);
            g.DrawString(text, font, shadowBrush, position.X + shadowOffset, position.Y - shadowOffset);
            g.DrawString(text, font, shadowBrush, position.X + shadowOffset, position.Y + shadowOffset);
            g.DrawString(text, font, shadowBrush, position.X - shadowOffset, position.Y + shadowOffset);

            Brush textBrush = new SolidBrush(textColor);
            g.DrawString(text, font, textBrush, position.X, position.Y);
        }
    }
}
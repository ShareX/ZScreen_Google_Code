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

        public static Rectangle CreateRectangle(Point pos1, Point pos2)
        {
            int x = pos1.X;
            int y = pos1.Y;
            int width = pos2.X - pos1.X + 1;
            int height = pos2.Y - pos1.Y + 1;

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

        public static Image CropImage(Image img, GraphicsPath gp)
        {
            if (img != null && gp != null)
            {
                RectangleF bounds = gp.GetBounds();

                if (bounds.Width > 0 && bounds.Height > 0)
                {
                    Rectangle rect = Rectangle.Round(bounds);
                    Bitmap bmp = new Bitmap(rect.Width, rect.Height);

                    using (Graphics g = Graphics.FromImage(bmp))
                    {
                        g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                        g.SmoothingMode = SmoothingMode.HighQuality;

                        using (Region region = new Region(gp))
                        {
                            g.Clip = region;
                            g.TranslateClip(-bounds.X, -bounds.Y);
                            g.DrawImage(img, new Rectangle(0, 0, rect.Width, rect.Height), rect, GraphicsUnit.Pixel);
                        }
                    }

                    return bmp;
                }
            }

            return null;
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
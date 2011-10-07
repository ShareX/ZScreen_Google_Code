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
using System.Windows.Forms;
using HelpersLib;

namespace HelpersLib
{
    public static class CaptureHelpers
    {
        public static Rectangle GetScreenBounds()
        {
            return SystemInformation.VirtualScreen;
        }

        public static Rectangle GetScreenBounds2()
        {
            Point topLeft = Point.Empty;
            Point bottomRight = Point.Empty;

            foreach (Screen screen in Screen.AllScreens)
            {
                if (screen.Bounds.X < topLeft.X) topLeft.X = screen.Bounds.X;
                if (screen.Bounds.Y < topLeft.Y) topLeft.Y = screen.Bounds.Y;
                if ((screen.Bounds.X + screen.Bounds.Width) > bottomRight.X) bottomRight.X = screen.Bounds.X + screen.Bounds.Width;
                if ((screen.Bounds.Y + screen.Bounds.Height) > bottomRight.Y) bottomRight.Y = screen.Bounds.Y + screen.Bounds.Height;
            }

            return new Rectangle(topLeft.X, topLeft.Y, bottomRight.X + Math.Abs(topLeft.X), bottomRight.Y + Math.Abs(topLeft.Y));
        }

        public static Rectangle GetScreenBounds3()
        {
            Point topLeft = Point.Empty;
            Point bottomRight = Point.Empty;

            foreach (Screen screen in Screen.AllScreens)
            {
                topLeft.X = Math.Min(topLeft.X, screen.Bounds.X);
                topLeft.Y = Math.Min(topLeft.Y, screen.Bounds.Y);
                bottomRight.X = Math.Max(bottomRight.X, screen.Bounds.Right);
                bottomRight.Y = Math.Max(bottomRight.Y, screen.Bounds.Bottom);
            }

            return new Rectangle(topLeft.X, topLeft.Y, bottomRight.X + Math.Abs(topLeft.X), bottomRight.Y + Math.Abs(topLeft.Y));
        }

        public static Rectangle GetScreenBounds4()
        {
            Rectangle rect = Rectangle.Empty;

            foreach (Screen screen in Screen.AllScreens)
            {
                rect = Rectangle.Union(rect, screen.Bounds);
            }

            return rect;
        }

        // For multi monitor
        public static Point FixScreenCoordinates(Point point)
        {
            int screenX = NativeMethods.GetSystemMetrics(SystemMetric.SM_XVIRTUALSCREEN);
            int screenY = NativeMethods.GetSystemMetrics(SystemMetric.SM_XVIRTUALSCREEN);
            return new Point(point.X - screenX, point.Y - screenY);
        }

        // For multi monitor
        public static Point GetZeroBasedMousePosition()
        {
            POINT pt = new POINT();
            NativeMethods.GetCursorPos(out pt);
            Point point = new Point(pt.X, pt.Y);
            return FixScreenCoordinates(point);
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

        public static Rectangle GetWindowRectangle(IntPtr handle)
        {
            Rectangle rect = Rectangle.Empty;

            if (NativeMethods.IsDWMEnabled())
            {
                NativeMethods.GetExtendedFrameBounds(handle, out rect);
            }

            if (rect.IsEmpty)
            {
                rect = NativeMethods.GetWindowRect(handle);
            }

            if (NativeMethods.IsZoomed(handle))
            {
                rect = NativeMethods.MaximizedWindowFix(handle, rect);
            }

            return rect;
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
                Bitmap bmp = new Bitmap(rect.Width, rect.Height);

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

        public static Bitmap ResizeImage(Image img, Rectangle rect, bool allowEnlarge = false, bool centerImage = true)
        {
            double ratio;
            int newWidth, newHeight, newX, newY;

            if (!allowEnlarge && img.Width <= rect.Width && img.Height <= rect.Height)
            {
                ratio = 1.0;
                newWidth = img.Width;
                newHeight = img.Height;
            }
            else
            {
                double ratioX = (double)rect.Width / (double)img.Width;
                double ratioY = (double)rect.Height / (double)img.Height;
                ratio = ratioX < ratioY ? ratioX : ratioY;
                newWidth = (int)(img.Width * ratio);
                newHeight = (int)(img.Height * ratio);
            }

            newX = rect.X;
            newY = rect.Y;

            if (centerImage)
            {
                newX += (int)((rect.Width - (img.Width * ratio)) / 2);
                newY += (int)((rect.Height - (img.Height * ratio)) / 2);
            }

            Bitmap bmp = new Bitmap(rect.Width, rect.Height);

            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.SmoothingMode = SmoothingMode.HighQuality;
                g.DrawImage(img, newX, newY, newWidth, newHeight);
            }

            return bmp;
        }

        public static Bitmap ResizeImage(Image img, int width, int height, bool allowEnlarge = false, bool centerImage = true)
        {
            return ResizeImage(img, new Rectangle(0, 0, width, height), allowEnlarge, centerImage);
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
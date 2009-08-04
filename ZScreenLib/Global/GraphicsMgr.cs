#region License Information (GPL v2)
/*
    ZScreen - A program that allows you to upload screenshots in one keystroke.
    Copyright (C) 2008-2009  Brandon Zimmerman

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
using System.Drawing.Drawing2D;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.IO;
using System.Drawing.Text;
using System.Runtime.InteropServices;

namespace ZScreenLib
{
    public enum FilterType
    {
        Brightness,
        Contrast,
        Saturation
    }

    public static class GraphicsMgr
    {
        /// <summary>
        /// Function to get a Rectangle of all the screens combined
        /// </summary>
        /// <returns></returns>
        public static Rectangle GetScreenBounds()
        {
            Point topLeft = new Point(0, 0);
            Point bottomRight = new Point(0, 0);
            foreach (Screen screen in Screen.AllScreens)
            {
                if (screen.Bounds.X < topLeft.X) topLeft.X = screen.Bounds.X;
                if (screen.Bounds.Y < topLeft.Y) topLeft.Y = screen.Bounds.Y;
                if ((screen.Bounds.X + screen.Bounds.Width) > bottomRight.X) bottomRight.X = screen.Bounds.X + screen.Bounds.Width;
                if ((screen.Bounds.Y + screen.Bounds.Height) > bottomRight.Y) bottomRight.Y = screen.Bounds.Y + screen.Bounds.Height;
            }
            return new Rectangle(topLeft.X, topLeft.Y, bottomRight.X + Math.Abs(topLeft.X), bottomRight.Y + Math.Abs(topLeft.Y));
        }

        /// <summary>
        /// Function to get Image without memory errors
        /// </summary>
        /// <param name="fp"></param>
        /// <returns></returns>
        public static Image GetImageSafely(string fp)
        {
            Bitmap bmp = null;
            try
            {
                using (Image img = Image.FromFile(fp))
                {
                    bmp = new Bitmap(img);
                }
            }
            catch (Exception ex)
            {
                FileSystem.AppendDebug(ex.ToString());
            }
            return bmp;
        }

        public static void SaveImageToMemoryStream(Image img, MemoryStream ms, ImageFormat format)
        {
            //image quality setting only works for JPEG

            if (format == ImageFormat.Jpeg)
            {
                EncoderParameter quality = new EncoderParameter(Encoder.Quality, (int)Program.conf.ImageQuality);
                ImageCodecInfo codec = GetEncoderInfo("image/jpeg");

                EncoderParameters encoderParams = new EncoderParameters(1);
                encoderParams.Param[0] = quality;

                img.Save(ms, codec, encoderParams);
            }
            else
            {
                try
                {
                    img.Save(ms, format);
                }
                catch (Exception ex)
                {
                    FileSystem.AppendDebug(ex.ToString());
                }
            }
        }

        private static ImageCodecInfo GetEncoderInfo(string mimeType)
        {
            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageEncoders();

            for (int x = 0; x < codecs.Length; x++)
                if (codecs[x].MimeType == mimeType)
                    return codecs[x];
            return null;
        }

        public static Rectangle GetRectangle(int x, int y, int width, int height, Size grid, bool gridToggle, ref Point point)
        {
            int oldX, oldY;
            if (width < 0)
            {
                x = x + width;
                width = -width;

                if (gridToggle && grid.Width > 0)
                {
                    width = GridPoint(width, grid.Width) - 1;
                    point.X = x + width;
                }
            }
            else if (gridToggle && grid.Width > 0)
            {
                oldX = x;
                x = ReverseGridPoint(x, width + 1, grid.Width);
                width -= x - oldX;
                point.X = x;
            }
            if (height < 0)
            {
                y = y + height;
                height = -height;
                if (gridToggle && grid.Height > 0)
                {
                    height = GridPoint(height, grid.Height) - 1;
                    point.Y = y + height;
                }
            }
            else if (gridToggle && grid.Height > 0)
            {
                oldY = y;
                y = ReverseGridPoint(y, height + 1, grid.Height);
                height -= y - oldY;
                point.Y = y;
            }
            return new Rectangle(x, y, width, height);
        }

        public static int GridPoint(int point, int grid)
        {
            if (point % grid > 0) point += grid - (point % grid);
            return point;
        }

        public static int ReverseGridPoint(int point, int size, int grid)
        {
            point -= grid - (size % grid);
            return point;
        }

        /// <summary>
        /// Determins if file is a valid image
        /// </summary>
        /// <param name="fp">File path of the Image</param>
        /// <returns></returns>
        public static bool IsValidImage(string path)
        {
            bool result = false;

            if (File.Exists(path))
            {
                IntPtr zero = IntPtr.Zero;

                if (GDI.GdipLoadImageFromFile(path, out zero) == 0 &&
                    GDI.GdipImageForceValidation(new HandleRef(null, zero)) == 0)
                {
                    result = true;
                }

                if (zero != IntPtr.Zero)
                {
                    GDI.IntGdipDisposeImage(new HandleRef(null, zero));
                }
            }

            return result;
        }

        public static Bitmap MagnifyingGlass(Bitmap bmp, Point point, int size, int power)
        {
            Bitmap newbmp = new Bitmap(size, size);
            Graphics g = Graphics.FromImage(newbmp);
            g.InterpolationMode = InterpolationMode.NearestNeighbor;
            Rectangle rect = new Rectangle(point.X - (size / power) / 2, point.Y - (size / power) / 2,
                size / power, size / power);
            g.DrawImage(bmp, new Rectangle(0, 0, size, size), rect, GraphicsUnit.Pixel);
            Pen crosshairPen = new Pen(Color.FromArgb(100, Color.Red), power);
            //g.DrawLine(crosshairPen, size / 2 - power * 2, size / 2 - power / 2, size / 2 - power, size / 2 - power / 2);
            //g.DrawLine(crosshairPen, size / 2, size / 2 - power / 2, size / 2 + power, size / 2 - power / 2);
            //g.DrawLine(crosshairPen, size / 2 - power / 2, size / 2 - power * 2, size / 2 - power / 2, size / 2 - power);
            //g.DrawLine(crosshairPen, size / 2 - power / 2, size / 2, size / 2 - power / 2, size / 2 + power);
            //Bitmap bmpcrosshair = new Bitmap(power * 3, power * 3);
            //Graphics g2 = Graphics.FromImage(bmpcrosshair);
            //Brush crosshairBrush = Brushes.Red;
            //g2.FillRectangle(crosshairBrush, 0, power, power, power);
            //g2.FillRectangle(crosshairBrush, power * 2, power, power, power);
            //g2.FillRectangle(crosshairBrush, power, 0, power, power);
            //g2.FillRectangle(crosshairBrush, power, power * 2, power, power);
            //g.DrawImage(bmpcrosshair, size / 2 - power * 2, size / 2 - power * 2);
            g.DrawLine(crosshairPen, 0, size / 2, size, size / 2);
            g.DrawLine(crosshairPen, size / 2, 0, size / 2, size - 2);
            g.DrawRectangle(new Pen(Brushes.Black), 0, 0, size - 1, size - 2);
            return newbmp;
        }

        public static Bitmap ChangeBrightness(Bitmap bmp, int value)
        {
            BitmapData bmpData = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
            int nOffset = bmpData.Stride - bmp.Width * 3;
            int bmpWidth = bmp.Width * 3;
            int nVal;

            unsafe
            {
                byte* p = (byte*)(void*)bmpData.Scan0;

                for (int y = 0; y < bmp.Height; ++y)
                {
                    for (int x = 0; x < bmpWidth; ++x)
                    {
                        nVal = p[0] + value;
                        if (nVal < 0) nVal = 0;
                        else if (nVal > 255) nVal = 255;

                        p[0] = (byte)nVal;
                        ++p;
                    }
                    p += nOffset;
                }
            }

            bmp.UnlockBits(bmpData);

            return bmp;
        }

        public static Image CropImage(Image img, Rectangle rect)
        {
            Image bmp = new Bitmap(rect.Width, rect.Height);
            Graphics g = Graphics.FromImage(bmp);
            g.CompositingQuality = CompositingQuality.HighQuality;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.SmoothingMode = SmoothingMode.HighQuality;
            g.DrawImage(img, new Rectangle(0, 0, rect.Width, rect.Height), rect, GraphicsUnit.Pixel);
            return bmp;
        }

        public static Image ChangeImageSize(Image img, float percentage)
        {
            int width = (int)(percentage / 100 * img.Width);
            int height = (int)(percentage / 100 * img.Height);
            return ChangeImageSize(img, width, height);
        }

        public static Image ChangeImageSize(Image img, int width, int height)
        {
            Image bmp = new Bitmap(width, height);
            Graphics g = Graphics.FromImage(bmp);
            g.CompositingQuality = CompositingQuality.HighQuality;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.SmoothingMode = SmoothingMode.HighQuality;
            g.DrawImage(img, new Rectangle(0, 0, bmp.Width, bmp.Height));
            return bmp;
        }

        public static Image DrawProgressIcon(int percentage)
        {
            if (percentage > 99) percentage = 99;
            Bitmap bmp = new Bitmap(16, 16);
            Graphics g = Graphics.FromImage(bmp);
            g.SmoothingMode = SmoothingMode.HighQuality;
            g.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;
            g.FillRectangle(Brushes.Black, 0, 0, 16, 16);
            int width = (int)(0.16 * percentage);
            if (width > 0)
            {
                Brush brush = new LinearGradientBrush(new Rectangle(0, 0, width, 16), Color.SteelBlue, Color.MediumBlue, LinearGradientMode.Vertical);
                g.FillRectangle(brush, 0, 0, width, 16);
            }
            StringFormat sf = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };
            g.DrawString(percentage.ToString(), new Font("Arial", 7, FontStyle.Bold), Brushes.White, bmp.Width / 2, bmp.Height / 2, sf);
            g.DrawRectangle(Pens.White, 0, 0, 15, 15);
            return bmp;
        }
    }
}
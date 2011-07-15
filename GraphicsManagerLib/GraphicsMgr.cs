#region License Information (GPL v2)

/*
    ZScreen - A program that allows you to upload screenshots in one keystroke.
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
using System.Drawing.Text;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using GraphicsMgrLib.Properties;
using HelpersLib;

namespace GraphicsMgrLib
{
    public enum FilterType
    {
        Brightness,
        Contrast,
        Saturation
    }

    public static class GraphicsMgr
    {
        public static Image CropImage(Image img, Rectangle rect)
        {
            Image bmp = new Bitmap(rect.Width, rect.Height);

            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.CompositingQuality = CompositingQuality.HighQuality;
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.DrawImage(img, new Rectangle(0, 0, rect.Width, rect.Height), rect, GraphicsUnit.Pixel);
            }

            return bmp;
        }

        public static Image AddCanvas(Image img, int size)
        {
            Image bmp = new Bitmap(img.Width + size * 2, img.Height + size * 2);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.CompositingQuality = CompositingQuality.HighQuality;
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.DrawImage(img, new Rectangle(size, size, img.Width, img.Height), new Rectangle(0, 0, img.Width, img.Height), GraphicsUnit.Pixel);
            }

            return bmp;
        }

        public static Bitmap RotateImage(Image img, float theta)
        {
            Matrix matrix = new Matrix();
            matrix.Translate(img.Width / -2, img.Height / -2, MatrixOrder.Append);
            matrix.RotateAt(theta, new Point(0, 0), MatrixOrder.Append);
            using (GraphicsPath gp = new GraphicsPath())
            {
                gp.AddPolygon(new Point[] { new Point(0, 0), new Point(img.Width, 0), new Point(0, img.Height) });
                gp.Transform(matrix);
                PointF[] pts = gp.PathPoints;

                Rectangle bbox = BoundingBox(img, matrix);
                Bitmap bmpDest = new Bitmap(bbox.Width, bbox.Height);

                using (Graphics gDest = Graphics.FromImage(bmpDest))
                {
                    Matrix mDest = new Matrix();
                    mDest.Translate(bmpDest.Width / 2, bmpDest.Height / 2, MatrixOrder.Append);
                    gDest.Transform = mDest;
                    gDest.CompositingQuality = CompositingQuality.HighQuality;
                    gDest.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    gDest.DrawImage(img, pts);
                    return bmpDest;
                }
            }
        }

        private static Rectangle BoundingBox(Image img, Matrix matrix)
        {
            GraphicsUnit gu = new GraphicsUnit();
            Rectangle rImg = Rectangle.Round(img.GetBounds(ref gu));

            Point topLeft = new Point(rImg.Left, rImg.Top);
            Point topRight = new Point(rImg.Right, rImg.Top);
            Point bottomRight = new Point(rImg.Right, rImg.Bottom);
            Point bottomLeft = new Point(rImg.Left, rImg.Bottom);
            Point[] points = new Point[] { topLeft, topRight, bottomRight, bottomLeft };
            GraphicsPath gp = new GraphicsPath(points,
                new byte[] { (byte)PathPointType.Start, (byte)PathPointType.Line, (byte)PathPointType.Line, (byte)PathPointType.Line });
            gp.Transform(matrix);
            return Rectangle.Round(gp.GetBounds());
        }

        /// <summary>
        /// Function to get a Rectangle of all the screens combined
        /// </summary>
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

        public static Rectangle GetScreenBounds2()
        {
            Point topLeft = new Point(int.MaxValue, int.MaxValue);
            Point bottomRight = new Point(int.MinValue, int.MinValue);

            foreach (Screen screen in Screen.AllScreens)
            {
                topLeft.X = Math.Min(topLeft.X, screen.Bounds.X);
                topLeft.Y = Math.Min(topLeft.Y, screen.Bounds.Y);
                bottomRight.X = Math.Max(bottomRight.X, screen.Bounds.Right);
                bottomRight.Y = Math.Max(bottomRight.Y, screen.Bounds.Bottom);
            }

            return new Rectangle(topLeft.X, topLeft.Y, bottomRight.X + Math.Abs(topLeft.X), bottomRight.Y + Math.Abs(topLeft.Y));
        }

        public static Rectangle GetScreenBounds3()
        {
            Rectangle rect = new Rectangle(int.MaxValue, int.MaxValue, int.MinValue, int.MinValue);

            foreach (Screen screen in Screen.AllScreens)
            {
                rect = Rectangle.Union(rect, screen.Bounds);
            }

            return rect;
        }

        public static Rectangle GetScreenBounds4()
        {
            return SystemInformation.VirtualScreen;
        }

        /// <summary>
        /// Function to get Image without memory errors
        /// </summary>
        public static Image GetImageSafely(string fp)
        {
            Bitmap bmp = null;
            if (IsValidImage(fp))
            {
                try
                {
                    using (Image img = Image.FromFile(fp))
                    {
                        bmp = new Bitmap(img);
                    }
                }
                catch (Exception)
                {
                    //  Engine.MyLogger.WriteLine("GetImageSafely", ex);
                }
            }
            return bmp;
        }

        public class SaveImageToMemoryStreamOptions
        {
            public Image MyImage { get; set; }

            public ImageFileFormat MyImageFileFormat { get; set; }

            public bool MakeJPGBackgroundWhite { get; set; }

            public GIFQuality GIFQuality { get; set; }

            public decimal JpgQuality { get; set; }

            public SaveImageToMemoryStreamOptions(Image img, ImageFileFormat iff)
            {
                this.MyImage = img;
                this.MyImageFileFormat = iff;
            }
        }

        private static ImageCodecInfo GetEncoderInfo(string mimeType)
        {
            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageEncoders();

            for (int x = 0; x < codecs.Length; x++)
            {
                if (codecs[x].MimeType == mimeType)
                {
                    return codecs[x];
                }
            }

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

        public static Image AddHighlighting(Bitmap bmp)
        {
            return AddHighlighting(bmp, Color.Yellow, true, 100);
        }

        public static Image AddHighlighting(Bitmap bmp, Color color, bool preserveTransparency, int opacity)
        {
            BitmapData bmpData = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);
            int length = bmpData.Stride * bmpData.Height;
            int r, g, b, a;

            unsafe
            {
                byte* p = (byte*)(void*)bmpData.Scan0;

                for (int i = 0; i < length; i += 4)
                {
                    r = i + 2; g = i + 1; b = i; a = i + 3;

                    p[r] = CalculateHighlighting(p[r], color.R, opacity);
                    p[g] = CalculateHighlighting(p[g], color.G, opacity);
                    p[b] = CalculateHighlighting(p[b], color.B, opacity);

                    if (!preserveTransparency)
                    {
                        p[a] = CalculateAlphaHighlighting(p[a], color.A, opacity);
                    }
                }
            }

            bmp.UnlockBits(bmpData);

            return bmp;
        }

        public static Image AddHighlighting2(Bitmap bmp, Color color, bool preserveTransparency, int opacity)
        {
            BitmapData bmpData = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);
            int r, g, b, a, length = bmpData.Stride * bmpData.Height;
            byte[] buffer = new byte[length];
            Marshal.Copy(bmpData.Scan0, buffer, 0, length);

            for (int i = 0; i < length; i += 4)
            {
                r = i + 2; g = i + 1; b = i; a = i + 3;

                buffer[r] = CalculateHighlighting(buffer[r], color.R, opacity);
                buffer[g] = CalculateHighlighting(buffer[g], color.G, opacity);
                buffer[b] = CalculateHighlighting(buffer[b], color.B, opacity);

                if (!preserveTransparency)
                {
                    buffer[a] = CalculateAlphaHighlighting(buffer[a], color.A, opacity);
                }
            }

            Marshal.Copy(buffer, 0, bmpData.Scan0, length);
            bmp.UnlockBits(bmpData);

            return bmp;
        }

        private static byte CalculateHighlighting(byte orginal, byte highlight, int opacity)
        {
            if (orginal < highlight)
            {
                return orginal;
            }

            if (opacity < 100)
            {
                return (byte)Math.Min((highlight + orginal * ((double)(100 - opacity) / 100)), 255);
            }

            return highlight;
        }

        private static byte CalculateAlphaHighlighting(byte orginal, byte highlight, int opacity)
        {
            if (orginal > highlight)
            {
                return orginal;
            }

            if (opacity < 100)
            {
                return (byte)Math.Min((orginal + highlight * ((double)opacity / 100)), 255);
            }

            return highlight;
        }

        public enum ColorType
        {
            B, G, R, A
        }

        public static int FindPosition(int x, int y, int stride, ColorType color)
        {
            return (stride * y) + (4 * x) + (int)color;
        }

        public static Rectangle GetCroppedArea(Bitmap bmp)
        {
            Rectangle rectangle = new Rectangle();

            int width = bmp.Size.Width;
            int height = bmp.Size.Height;

            Rectangle rect = new Rectangle(new Point(0, 0), bmp.Size);
            BitmapData bmpData = bmp.LockBits(rect, ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);

            try
            {
                IntPtr pImage = bmpData.Scan0;
                int bytes = bmpData.Stride * bmpData.Height;
                byte[] colorData = new byte[bytes];

                Marshal.Copy(pImage, colorData, 0, bytes);

                bool stop = false;

                // Find X
                for (int x = 0; x < width && !stop; x++)
                {
                    for (int y = 0; y < height && !stop; y++)
                    {
                        if (colorData[FindPosition(x, y, bmpData.Stride, ColorType.A)] > 0)
                        {
                            rectangle.X = x;
                            stop = true;
                        }
                    }
                }

                stop = false;

                // Find Y
                for (int y = 0; y < height && !stop; y++)
                {
                    for (int x = 0; x < width && !stop; x++)
                    {
                        if (colorData[FindPosition(x, y, bmpData.Stride, ColorType.A)] > 0)
                        {
                            rectangle.Y = y;
                            stop = true;
                        }
                    }
                }

                stop = false;

                // Find Width
                for (int x = bmp.Width - 1; x > rectangle.X && !stop; x--)
                {
                    for (int y = 0; y < height && !stop; y++)
                    {
                        if (colorData[FindPosition(x, y, bmpData.Stride, ColorType.A)] > 0)
                        {
                            rectangle.Width = x - rectangle.X + 1;
                            stop = true;
                        }
                    }
                }

                stop = false;

                // Find Height
                for (int y = bmp.Height - 1; y > rectangle.Y && !stop; y--)
                {
                    for (int x = 0; x < width && !stop; x++)
                    {
                        if (colorData[FindPosition(x, y, bmpData.Stride, ColorType.A)] > 0)
                        {
                            rectangle.Height = y - rectangle.Y + 1;
                            stop = true;
                        }
                    }
                }
            }
            finally
            {
                bmp.UnlockBits(bmpData);
            }

            return rectangle;
        }

        public static Image ChangeImageSize(Image img, int width, int height, bool preserveSize = false, bool autoScale = false)
        {
            int imageWidth = width, imageHeight = height;

            if (preserveSize)
            {
                imageWidth = Math.Min(img.Width, width);
                imageHeight = Math.Min(img.Height, height);
            }

            if (autoScale)
            {
                if (width > img.Width || height > img.Height)
                {
                    imageWidth = img.Width;
                    imageHeight = img.Height;
                }
                else
                {
                    if (width < 1)
                    {
                        imageWidth = (int)(((double)img.Width / img.Height) * height);
                    }

                    if (height < 1)
                    {
                        imageHeight = (int)(((double)img.Height / img.Width) * width);
                    }
                }
            }

            Image bmp = new Bitmap(imageWidth, imageHeight);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.CompositingQuality = CompositingQuality.HighQuality;
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.SmoothingMode = SmoothingMode.HighQuality;
                g.DrawImage(img, new Rectangle(0, 0, bmp.Width, bmp.Height));
            }

            return bmp;
        }

        public static Image ChangeImageSize(Image img, float percentage, bool preserveSize = false)
        {
            int width = (int)(percentage / 100 * img.Width);
            int height = (int)(percentage / 100 * img.Height);
            return ChangeImageSize(img, width, height, preserveSize);
        }

        public static Bitmap ResizeImage(Image img, int width, int height, bool allowEnlarge = false, bool centerImage = true)
        {
            return ResizeImage(img, new Rectangle(0, 0, width, height), allowEnlarge, centerImage);
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

        public static Image DrawProgressIcon(int percentage)
        {
            if (percentage > 99) percentage = 99;
            Bitmap bmp = new Bitmap(16, 16);
            using (Graphics g = Graphics.FromImage(bmp))
            {
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
                g.DrawString(percentage.ToString(), new XFont("Arial", 7, FontStyle.Bold), Brushes.White, bmp.Width / 2, bmp.Height / 2, sf);
                g.DrawRectangle(Pens.White, 0, 0, 15, 15);
            }
            return bmp;
        }

        /// <summary>
        /// Compute the original window image from the difference between the two given images
        /// </summary>
        /// <param name="whiteBGImage">the window with a white background</param>
        /// <param name="blackBGImage">the window with a black background</param>
        /// <returns>the original window image, with restored alpha channel</returns>
        public static Bitmap ComputeOriginal(Bitmap whiteBGImage, Bitmap blackBGImage)
        {
            int width = whiteBGImage.Size.Width;
            int height = whiteBGImage.Size.Height;

            Bitmap resultImage = new Bitmap(width, height, PixelFormat.Format32bppArgb);

            Rectangle rect = new Rectangle(new Point(0, 0), blackBGImage.Size);

            // Access the image data directly for faster image processing
            BitmapData blackImageData = blackBGImage.LockBits(rect, ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
            BitmapData whiteImageData = whiteBGImage.LockBits(rect, ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
            BitmapData resultImageData = resultImage.LockBits(rect, ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);

            try
            {
                IntPtr pBlackImage = blackImageData.Scan0;
                IntPtr pWhiteImage = whiteImageData.Scan0;
                IntPtr pResultImage = resultImageData.Scan0;

                int bytes = blackImageData.Stride * blackImageData.Height;
                byte[] blackBGImageRGB = new byte[bytes];
                byte[] whiteBGImageRGB = new byte[bytes];
                byte[] resultImageRGB = new byte[bytes];

                Marshal.Copy(pBlackImage, blackBGImageRGB, 0, bytes);
                Marshal.Copy(pWhiteImage, whiteBGImageRGB, 0, bytes);

                int offset = 0;

                int b0, g0, r0, b1, g1, r1, alphaR, alphaG, alphaB, resultR, resultG, resultB;

                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++)
                    {
                        // ARGB is in fact BGRA (little endian)
                        b0 = blackBGImageRGB[offset + 0];
                        g0 = blackBGImageRGB[offset + 1];
                        r0 = blackBGImageRGB[offset + 2];

                        b1 = whiteBGImageRGB[offset + 0];
                        g1 = whiteBGImageRGB[offset + 1];
                        r1 = whiteBGImageRGB[offset + 2];

                        alphaR = r0 - r1 + 255;
                        alphaG = g0 - g1 + 255;
                        alphaB = b0 - b1 + 255;

                        if (alphaG != 0)
                        {
                            resultR = r0 * 255 / alphaG;
                            resultG = g0 * 255 / alphaG;
                            resultB = b0 * 255 / alphaG;
                        }
                        else
                        {
                            // Could be any color since it is fully transparent.
                            resultR = 255;
                            resultG = 255;
                            resultB = 255;
                        }

                        resultImageRGB[offset + 3] = (byte)alphaR;
                        resultImageRGB[offset + 2] = (byte)resultR;
                        resultImageRGB[offset + 1] = (byte)resultG;
                        resultImageRGB[offset + 0] = (byte)resultB;

                        offset += 4;
                    }
                }

                Marshal.Copy(resultImageRGB, 0, pResultImage, bytes);
            }
            finally
            {
                blackBGImage.UnlockBits(blackImageData);
                whiteBGImage.UnlockBits(whiteImageData);
                resultImage.UnlockBits(resultImageData);
            }

            return resultImage;
        }

        /// <summary>
        /// Removes the pixels that are transparent in the given image from the
        /// region of the given graphics object
        /// </summary>
        public static void RemoveTransparentPixelsFromRegion(Bitmap image, Graphics g)
        {
            int width = image.Size.Width;
            int height = image.Size.Height;

            Rectangle rect = new Rectangle(new Point(0, 0), image.Size);

            // Access the image data directly for faster image processing
            BitmapData imageData = image.LockBits(rect, ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);

            IntPtr pImage = imageData.Scan0;

            int bytes = imageData.Stride * imageData.Height;
            byte[] imageRGB = new byte[bytes];

            Marshal.Copy(pImage, imageRGB, 0, bytes);

            int offset = 0;
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    int alpha = imageRGB[offset + 3];
                    if (alpha == 0)
                    {
                        // removes the completely transparent pixel from the graphics region
                        Region region = new Region(new Rectangle(x, y, 1, 1));
                        g.SetClip(region, CombineMode.Exclude);
                    }
                    offset += 4;
                }
            }

            image.UnlockBits(imageData);
        }

        public static Point ShadowOffset { get { return new Point(Resources.leftShadow.Width, Resources.topShadow.Height); } }

        public static Image AddBorderShadow(Image input, bool roundedShadowCorners)
        {
            Bitmap leftShadow = Resources.leftShadow;
            Bitmap rightShadow = Resources.rightShadow;
            Bitmap topShadow = Resources.topShadow;
            Bitmap bottomShadow = Resources.bottomShadow;
            Bitmap topLeftShadow = roundedShadowCorners ? Resources.topLeftShadow : Resources.topLeftShadowSquare;
            Bitmap topRightShadow = roundedShadowCorners ? Resources.topRightShadow : Resources.topRightShadowSquare;
            Bitmap bottomLeftShadow = Resources.bottomLeftShadow;
            Bitmap bottomRightShadow = Resources.bottomRightShadow;

            int leftMargin = leftShadow.Width;
            int rightMargin = rightShadow.Width;
            int topMargin = topShadow.Height;
            int bottomMargin = bottomShadow.Height;

            int width = input.Width;
            int height = input.Height;

            int resultWidth = leftMargin + width + rightMargin;
            int resultHeight = topMargin + height + bottomMargin;

            if (resultHeight - topRightShadow.Height - bottomRightShadow.Height <= 0
                || resultWidth - bottomLeftShadow.Width - bottomRightShadow.Width <= 0)
                return input;

            Bitmap bmpResult = new Bitmap(resultWidth, resultHeight, PixelFormat.Format32bppArgb);

            using (Graphics g = Graphics.FromImage(bmpResult))
            {
                g.DrawImage(topLeftShadow, 0, 0);
                g.DrawImage(topRightShadow, resultWidth - topRightShadow.Width, 0);
                g.DrawImage(bottomLeftShadow, 0, resultHeight - bottomLeftShadow.Height);
                g.DrawImage(bottomRightShadow, resultWidth - bottomRightShadow.Width, resultHeight - bottomRightShadow.Height);

                g.DrawShadow(leftShadow, 0, topLeftShadow.Height, leftShadow.Width, resultHeight - topLeftShadow.Height - bottomLeftShadow.Height);
                g.DrawShadow(rightShadow, resultWidth - rightShadow.Width, topRightShadow.Height,
                    rightShadow.Width, resultHeight - topRightShadow.Height - bottomRightShadow.Height);
                g.DrawShadow(topShadow, topLeftShadow.Width, 0, resultWidth - topLeftShadow.Width - topRightShadow.Width, topShadow.Height);
                g.DrawShadow(bottomShadow, bottomLeftShadow.Width, resultHeight - bottomShadow.Height,
                    resultWidth - bottomLeftShadow.Width - bottomRightShadow.Width, bottomShadow.Height);

                g.DrawImage(input, leftMargin, topMargin);
            }

            return bmpResult;
        }

        public static Bitmap MakeBackgroundTransparent(IntPtr hWnd, Image image)
        {
            Region region;
            if (GraphicsMgrNativeMethods.GetWindowRegion(hWnd, out region))
            {
                Bitmap result = new Bitmap(image.Width, image.Height);

                using (Graphics g = Graphics.FromImage(result))
                {
                    if (!region.IsEmpty(g))
                    {
                        RectangleF bounds = region.GetBounds(g);
                        g.Clip = region;
                        g.DrawImage(image, new RectangleF(new PointF(0, 0), bounds.Size), bounds, GraphicsUnit.Pixel);

                        return result;
                    }
                }
            }

            return (Bitmap)image;
        }

        /// <summary>
        /// Removes corners from windowImage, using redBGImage as a mask (red pixels are removed).
        /// </summary>
        /// <param name="windowImage">the image from which to remove the corners</param>
        /// <param name="redBGImage">the mask in which pixels to remove are marked in red. If null, corners will always be removed</param>
        /// <returns>a new image with corners removed</returns>
        public static Image RemoveCorners(Image windowImage, Bitmap redBGImage)
        {
            const int cornerSize = 5;
            if (windowImage.Width > cornerSize * 2 && windowImage.Height > cornerSize * 2)
            {
                Image result = new Bitmap(windowImage.Width, windowImage.Height, PixelFormat.Format32bppArgb);
                using (Graphics g = Graphics.FromImage(result))
                {
                    g.Clear(Color.Transparent);
                    // Remove the transparent pixels in the four corners
                    RemoveCorner(redBGImage, g, 0, 0, cornerSize, Corner.TopLeft);
                    RemoveCorner(redBGImage, g, windowImage.Width - cornerSize, 0, windowImage.Width, Corner.TopRight);
                    RemoveCorner(redBGImage, g, 0, windowImage.Height - cornerSize, cornerSize, Corner.BottomLeft);
                    RemoveCorner(redBGImage, g, windowImage.Width - cornerSize, windowImage.Height - cornerSize, windowImage.Width, Corner.BottomRight);
                    g.DrawImage(windowImage, 0, 0);
                }
                return result;
            }
            return windowImage;
        }

        public enum Corner { TopLeft, TopRight, BottomLeft, BottomRight };

        /// <summary>
        /// Removes a corner from the clipping region of the given graphics object.
        /// </summary>
        /// <param name="bmp">The bitmap with the form corners masked in red</param>
        private static void RemoveCorner(Bitmap bmp, Graphics g, int minx, int miny, int maxx, Corner corner)
        {
            int[] shape;
            if (corner == Corner.TopLeft || corner == Corner.TopRight)
            {
                shape = new int[5] { 5, 3, 2, 1, 1 };
            }
            else
            {
                shape = new int[5] { 1, 1, 2, 3, 5 };
            }

            int maxy = miny + 5;
            if (corner == Corner.TopLeft || corner == Corner.BottomLeft)
            {
                for (int y = miny; y < maxy; y++)
                {
                    for (int x = minx; x < minx + shape[y - miny]; x++)
                    {
                        RemoveCornerPixel(bmp, g, y, x);
                    }
                }
            }
            else
            {
                for (int y = miny; y < maxy; y++)
                {
                    for (int x = maxx - 1; x >= maxx - shape[y - miny]; x--)
                    {
                        RemoveCornerPixel(bmp, g, y, x);
                    }
                }
            }
        }

        /// <summary>
        /// Removes a pixel from the clipping region of the given graphics object, if
        /// the bitmap is red at the coordinates of the pixel, or if it is null.
        /// </summary>
        /// <param name="bmp">The bitmap with the form corners masked in red</param>
        private static void RemoveCornerPixel(Bitmap bmp, Graphics g, int y, int x)
        {
            bool remove;
            if (bmp != null)
            {
                Color color = bmp.GetPixel(x, y);
                // detect a shade of red (the color is darker because of the window's shadow)
                remove = (color.R > 0 && color.G == 0 && color.B == 0);
            }
            else
            {
                remove = true;
            }
            if (remove)
            {
                Region region = new Region(new Rectangle(x, y, 1, 1));
                g.SetClip(region, CombineMode.Exclude);
            }
        }
    }
}
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
                catch (Exception ex)
                {
                    FileSystem.AppendDebug("GetImageSafely", ex);
                }
            }
            return bmp;
        }

        public static void SaveImageToMemoryStream(Image img, MemoryStream ms, ImageFormat format)
        {
            //image quality setting only works for JPEG

            if (format == ImageFormat.Jpeg)
            {
                EncoderParameter quality = new EncoderParameter(Encoder.Quality, (int)Engine.conf.ImageQuality);
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
                    FileSystem.AppendDebug("Error at SaveImageToMemoryStream", ex);
                }
            }
        }

        private static ColorPalette GetColorPalette(uint nColors)
        {
            // Assume monochrome image.
            PixelFormat bitscolordepth = PixelFormat.Format1bppIndexed;
            ColorPalette palette;    // The Palette we are stealing
            Bitmap bitmap;     // The source of the stolen palette

            // Determine number of colors.
            if (nColors > 2)
                bitscolordepth = PixelFormat.Format4bppIndexed;
            if (nColors > 16)
                bitscolordepth = PixelFormat.Format8bppIndexed;

            // Make a new Bitmap object to get its Palette.
            bitmap = new Bitmap(1, 1, bitscolordepth);

            palette = bitmap.Palette;   // Grab the palette

            bitmap.Dispose();           // cleanup the source Bitmap		
            return palette;             // Send the palette back
        }

        public static void SaveGIFWithNewColorTable(Image img, string filePath, uint nColors, bool fTransparent)
        {
            // GIF codec supports 256 colors maximum, monochrome minimum.
            if (nColors > 256)
                nColors = 256;
            if (nColors < 2)
                nColors = 2;

            // Make a new 8-BPP indexed bitmap that is the same size as the source image.
            int Width = img.Width;
            int Height = img.Height;

            // Always use PixelFormat8bppIndexed because that is the color
            // table-based interface to the GIF codec.
            Bitmap bitmap = new Bitmap(Width,
                                    Height,
                                    PixelFormat.Format8bppIndexed);

            // Create a color palette big enough to hold the colors you want.
            ColorPalette pal = GetColorPalette(nColors);

            // Initialize a new color table with entries that are determined
            // by some optimal palette-finding algorithm; for demonstration 
            // purposes, use a grayscale.
            for (uint i = 0; i < nColors; i++)
            {
                uint Alpha = 0xFF;                      // Colors are opaque.
                uint Intensity = i * 0xFF / (nColors - 1);    // Even distribution. 

                // The GIF encoder makes the first entry in the palette
                // that has a ZERO alpha the transparent color in the GIF.
                // Pick the first one arbitrarily, for demonstration purposes.

                if (i == 0 && fTransparent) // Make this color index...
                    Alpha = 0;          // Transparent

                // Create a gray scale for demonstration purposes.
                // Otherwise, use your favorite color reduction algorithm
                // and an optimum palette for that algorithm generated here.
                // For example, a color histogram, or a median cut palette.
                pal.Entries[i] = Color.FromArgb((int)Alpha,
                                                (int)Intensity,
                                                (int)Intensity,
                                                (int)Intensity);
            }

            // Set the palette into the new Bitmap object.
            bitmap.Palette = pal;


            // Use GetPixel below to pull out the color data of Image.
            // Because GetPixel isn't defined on an Image, make a copy 
            // in a Bitmap instead. Make a new Bitmap that is the same size as the
            // image that you want to export. Or, try to
            // interpret the native pixel format of the image by using a LockBits
            // call. Use PixelFormat32BppARGB so you can wrap a Graphics  
            // around it.
            Bitmap BmpCopy = new Bitmap(Width,
                                    Height,
                                    PixelFormat.Format32bppArgb);
            {
                Graphics g = Graphics.FromImage(BmpCopy);

                g.PageUnit = GraphicsUnit.Pixel;

                // Transfer the Image to the Bitmap
                g.DrawImage(img, 0, 0, Width, Height);

                // g goes out of scope and is marked for garbage collection.
                // Force it, just to keep things clean.
                g.Dispose();
            }

            // Lock a rectangular portion of the bitmap for writing.
            BitmapData bitmapData;
            Rectangle rect = new Rectangle(0, 0, Width, Height);

            bitmapData = bitmap.LockBits(
                rect,
                ImageLockMode.WriteOnly,
                PixelFormat.Format8bppIndexed);

            // Write to the temporary buffer that is provided by LockBits.
            // Copy the pixels from the source image in this loop.
            // Because you want an index, convert RGB to the appropriate
            // palette index here.
            IntPtr pixels = bitmapData.Scan0;

            unsafe
            {
                // Get the pointer to the image bits.
                // This is the unsafe operation.
                byte* pBits;
                if (bitmapData.Stride > 0)
                    pBits = (byte*)pixels.ToPointer();
                else
                    // If the Stide is negative, Scan0 points to the last 
                    // scanline in the buffer. To normalize the loop, obtain
                    // a pointer to the front of the buffer that is located 
                    // (Height-1) scanlines previous.
                    pBits = (byte*)pixels.ToPointer() + bitmapData.Stride * (Height - 1);
                uint stride = (uint)Math.Abs(bitmapData.Stride);

                for (uint row = 0; row < Height; ++row)
                {
                    for (uint col = 0; col < Width; ++col)
                    {
                        // Map palette indexes for a gray scale.
                        // If you use some other technique to color convert,
                        // put your favorite color reduction algorithm here.
                        Color pixel;    // The source pixel.

                        // The destination pixel.
                        // The pointer to the color index byte of the
                        // destination; this real pointer causes this
                        // code to be considered unsafe.
                        byte* p8bppPixel = pBits + row * stride + col;

                        pixel = BmpCopy.GetPixel((int)col, (int)row);

                        // Use luminance/chrominance conversion to get grayscale.
                        // Basically, turn the image into black and white TV.
                        // Do not calculate Cr or Cb because you 
                        // discard the color anyway.
                        // Y = Red * 0.299 + Green * 0.587 + Blue * 0.114

                        // This expression is best as integer math for performance,
                        // however, because GetPixel listed earlier is the slowest 
                        // part of this loop, the expression is left as 
                        // floating point for clarity.

                        double luminance = (pixel.R * 0.299) +
                            (pixel.G * 0.587) +
                            (pixel.B * 0.114);

                        // Gray scale is an intensity map from black to white.
                        // Compute the index to the grayscale entry that
                        // approximates the luminance, and then round the index.
                        // Also, constrain the index choices by the number of
                        // colors to do, and then set that pixel's index to the 
                        // byte value.
                        *p8bppPixel = (byte)(luminance * (nColors - 1) / 255 + 0.5);

                    } /* end loop for col */
                } /* end loop for row */
            } /* end unsafe */

            // To commit the changes, unlock the portion of the bitmap.  
            bitmap.UnlockBits(bitmapData);

            bitmap.Save(filePath, ImageFormat.Gif);

            // Bitmap goes out of scope here and is also marked for
            // garbage collection.
            // Pal is referenced by bitmap and goes away.
            // BmpCopy goes out of scope here and is marked for garbage
            // collection. Force it, because it is probably quite large.
            // The same applies to bitmap.
            BmpCopy.Dispose();
            bitmap.Dispose();
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
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.CompositingQuality = CompositingQuality.HighQuality;
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.SmoothingMode = SmoothingMode.HighQuality;
                g.DrawImage(img, new Rectangle(0, 0, rect.Width, rect.Height), rect, GraphicsUnit.Pixel);
            }

            return bmp;
        }

        public static Image AutoCropImage(Bitmap bmp)
        {
            int x = 0, y = 0, width = bmp.Width, height = bmp.Height;

            for (int i = 0; i < bmp.Width; i++)
            {
                if (bmp.GetPixel(i, bmp.Height / 2).A > 0)
                {
                    x = i;
                    break;
                }
            }

            for (int i = 0; i < bmp.Height; i++)
            {
                if (bmp.GetPixel(bmp.Width / 2, i).A > 0)
                {
                    y = i;
                    break;
                }
            }

            for (int i = bmp.Width - 1; i > x; i--)
            {
                if (bmp.GetPixel(i, bmp.Height / 2).A > 0)
                {
                    width = i - x + 1;
                    break;
                }
            }

            for (int i = bmp.Height - 1; i > y; i--)
            {
                if (bmp.GetPixel(bmp.Width / 2, i).A > 0)
                {
                    height = i - y + 1;
                    break;
                }
            }

            return GraphicsMgr.CropImage(bmp, new Rectangle(x, y, width, height));
        }

        public static Image ChangeImageSize(Image img, int width, int height, bool preserveSize)
        {
            if (preserveSize)
            {
                width = Math.Min(img.Width, width);
                height = Math.Min(img.Height, height);
            }

            Image bmp = new Bitmap(width, height);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.CompositingQuality = CompositingQuality.HighQuality;
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.SmoothingMode = SmoothingMode.HighQuality;
                g.DrawImage(img, new Rectangle(0, 0, bmp.Width, bmp.Height));
            }

            return bmp;
        }

        public static Image ChangeImageSize(Image img, int width, int height)
        {
            return ChangeImageSize(img, width, height, false);
        }

        public static Image ChangeImageSize(Image img, Size size, bool preserveSize)
        {
            return ChangeImageSize(img, size.Width, size.Height, preserveSize);
        }

        public static Image ChangeImageSize(Image img, Size size)
        {
            return ChangeImageSize(img, size.Width, size.Height, false);
        }

        public static Image ChangeImageSize(Image img, float percentage)
        {
            int width = (int)(percentage / 100 * img.Width);
            int height = (int)(percentage / 100 * img.Height);
            return ChangeImageSize(img, width, height, false);
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
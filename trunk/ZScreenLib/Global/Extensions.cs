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
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace ZScreenLib
{
    public static class Extensions
    {
        public static int ToInt(this string str)
        {
            return Convert.ToInt32(str);
        }

        public static int Mid(this int number, int min, int max)
        {
            return Math.Min(Math.Max(number, min), max);
        }

        internal static string GetDescription(this Enum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());
            DescriptionAttribute[] attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);
            return (attributes.Length > 0) ? attributes[0].Description : value.ToString();
        }

        public static string[] GetDescriptions(this Type type)
        {
            string[] descriptions = new string[Enum.GetValues(type).Length];
            int i = 0;
            foreach (int value in Enum.GetValues(type))
            {
                descriptions[i++] = ((Enum)Enum.ToObject(type, value)).GetDescription();
            }
            return descriptions;
        }

        public static Point Intersect(this Point point, Rectangle rect)
        {
            if (point.X < rect.X)
            {
                point.X = rect.X;
            }
            else if (point.X > rect.Right)
            {
                point.X = rect.Right;
            }
            if (point.Y < rect.Y)
            {
                point.Y = rect.Y;
            }
            else if (point.Y > rect.Bottom)
            {
                point.Y = rect.Bottom;
            }
            return point;
        }

        public static string ToSpecialString(this Keys key)
        {
            string[] split = key.ToString().Split(new[] { ", " }, StringSplitOptions.None).Reverse().ToArray();
            return string.Join(" + ", split);
        }

        public static Rectangle AddMargin(this Rectangle rect, int offset)
        {
            rect.X -= offset;
            rect.Y -= offset;
            rect.Width += offset * 2;
            rect.Height += offset * 2;
            return rect;
        }

        /// <summary>
        /// Find out whether the two given bitmaps have the exact same image data.
        /// </summary>
        public static bool AreBitmapsEqual(this Bitmap first, Bitmap second)
        {
            if (first.Size != second.Size)
            {
                return false;
            }

            int width = first.Width;
            int height = first.Height;

            Rectangle rect = new Rectangle(new Point(0, 0), first.Size);

            // Access the image data directly for faster image processing
            BitmapData firstImageData = first.LockBits(rect, ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
            BitmapData secondImageData = second.LockBits(rect, ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);

            try
            {
                IntPtr pFirstImage = firstImageData.Scan0;
                IntPtr pSecondImage = secondImageData.Scan0;

                int bytes = firstImageData.Stride * firstImageData.Height;
                byte[] firstImageRGB = new byte[bytes];
                byte[] secondImageRGB = new byte[bytes];

                Marshal.Copy(pFirstImage, firstImageRGB, 0, bytes);
                Marshal.Copy(pSecondImage, secondImageRGB, 0, bytes);

                int offset = 0, b0, g0, r0, b1, g1, r1;

                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++)
                    {
                        b0 = firstImageRGB[offset + 0];
                        g0 = firstImageRGB[offset + 1];
                        r0 = firstImageRGB[offset + 2];

                        b1 = secondImageRGB[offset + 0];
                        g1 = secondImageRGB[offset + 1];
                        r1 = secondImageRGB[offset + 2];

                        if (r0 != r1 || g0 != g1 || b0 != b1)
                        {
                            return false;
                        }

                        offset += 4;
                    }
                }
            }
            finally
            {
                first.UnlockBits(firstImageData);
                second.UnlockBits(secondImageData);
            }

            return true;
        }

        public static void DrawShadow(this Graphics g, Bitmap shadowBitmap, int x, int y, int width, int height)
        {
            using (Brush brush = new TextureBrush(shadowBitmap))
            using (Bitmap bmpTemp = new Bitmap(width, height, PixelFormat.Format32bppArgb))
            using (Graphics gTemp = Graphics.FromImage(bmpTemp))
            {
                // Draw on a temp bitmap with (0,0) offset, because the texture starts at (0,0)
                gTemp.FillRectangle(brush, 0, 0, width, height);
                g.DrawImage(bmpTemp, x, y);
            }
        }

        public static Rectangle Merge(this Rectangle rect, Rectangle rect2)
        {
            if (rect.X > rect2.X)
            {
                rect.X = rect2.X;
                rect.Width += rect.X - rect2.X;
            }

            if (rect.Y > rect2.Y)
            {
                rect.Y = rect2.Y;
                rect.Height += rect.Y - rect2.Y;
            }

            if (rect.Right < rect2.Right)
            {
                rect.Width += rect2.Right - rect.Right;
            }

            if (rect.Bottom < rect2.Bottom)
            {
                rect.Height += rect2.Bottom - rect.Bottom;
            }

            return rect;
        }
    }
}
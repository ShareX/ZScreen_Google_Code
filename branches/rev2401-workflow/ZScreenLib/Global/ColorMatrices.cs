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

using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace ZScreenLib
{
    public static class ColorMatrices
    {
        //NTSC weights: 0.299, 0.587, 0.114
        public const float rw = 0.3086f;
        public const float gw = 0.6094f;
        public const float bw = 0.0820f;

        public static Bitmap ApplyColorMatrix(Bitmap img, ColorMatrix matrix)
        {
            using (Graphics g = Graphics.FromImage(img))
            {
                g.SmoothingMode = SmoothingMode.HighQuality;
                ImageAttributes imgattr = new ImageAttributes();
                imgattr.SetColorMatrix(matrix);
                g.DrawImage(img, new Rectangle(0, 0, img.Width, img.Height), 0, 0, img.Width, img.Height, GraphicsUnit.Pixel, imgattr);
            }

            return img;
        }

        public static ColorMatrix InverseFilter()
        {
            return new ColorMatrix(new[]{
                new float[] {-1, 0, 0, 0, 0},
                new float[] {0, -1, 0, 0, 0},
                new float[] {0, 0, -1, 0, 0},
                new float[] {0, 0, 0, 1, 0},
                new float[] {1, 1, 1, 0, 1}
            });
        }

        public static ColorMatrix BrightnessFilter(int percentage)
        {
            float perc = (float)percentage / 100;
            return new ColorMatrix(new[]{
                new float[] {1, 0, 0, 0, 0},
                new float[] {0, 1, 0, 0, 0},
                new float[] {0, 0, 1, 0, 0},
                new float[] {0, 0, 0, 1, 0},
                new float[] {perc, perc, perc, 0, 1}
            });
        }

        public static ColorMatrix ContrastFilter(int percentage)
        {
            float perc = 1 + (float)percentage / 100;
            return new ColorMatrix(new[]{
                new float[] {perc, 0, 0, 0, 0},
                new float[] {0, perc, 0, 0, 0},
                new float[] {0, 0, perc, 0, 0},
                new float[] {0, 0, 0, 1, 0},
                new float[] {0, 0, 0, 0, 1}
            });
        }

        public static ColorMatrix GrayscaleFilter()
        {
            return new ColorMatrix(new[]{
                new float[] {rw, rw, rw, 0, 0},
                new float[] {gw, gw, gw, 0, 0},
                new float[] {bw, bw, bw, 0, 0},
                new float[] {0, 0, 0, 1, 0},
                new float[] {0, 0, 0, 0, 1}
            });
        }

        public static ColorMatrix SaturationFilter(int percentage)
        {
            float s = 1 + (float)percentage / 100;
            return new ColorMatrix(new[]{
                new float[] {(1.0f - s) * rw + s, (1.0f - s) * rw, (1.0f - s) * rw, 0, 0},
                new float[] {(1.0f - s) * gw, (1.0f - s) * gw + s, (1.0f - s) * gw, 0, 0},
                new float[] {(1.0f - s) * bw, (1.0f - s) * bw, (1.0f - s) * bw + s, 0, 0},
                new float[] {0, 0, 0, 1, 0},
                new float[] {0, 0, 0, 0, 1}
            });
        }

        public static ColorMatrix ColorFilter(Color color)
        {
            return new ColorMatrix(new[]{
                new float[] {1, 0, 0, 0, 0},
                new float[] {0, 1, 0, 0, 0},
                new float[] {0, 0, 1, 0, 0},
                new float[] {0, 0, 0, 1, 0},
                new float[] {(float)color.R / 255, (float)color.G / 255, (float)color.B / 255, (float)color.A / 255, 1}
            });
        }
    }
}
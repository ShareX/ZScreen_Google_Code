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
using System.Runtime.InteropServices;

namespace ScreenCapture
{
    [StructLayout(LayoutKind.Explicit)]
    public struct ColorBgra
    {
        [FieldOffset(0)]
        public uint Bgra;

        [FieldOffset(0)]
        public byte Blue;
        [FieldOffset(1)]
        public byte Green;
        [FieldOffset(2)]
        public byte Red;
        [FieldOffset(3)]
        public byte Alpha;

        public const byte SizeOf = 4;

        public static bool operator ==(ColorBgra c1, ColorBgra c2)
        {
            return c1.Bgra == c2.Bgra;
        }

        public static bool operator !=(ColorBgra c1, ColorBgra c2)
        {
            return c1.Bgra != c2.Bgra;
        }

        public override bool Equals(object obj)
        {
            return obj != null && obj is ColorBgra && ((ColorBgra)obj).Bgra == Bgra;
        }

        public static ColorBgra FromBgra(uint bgra)
        {
            return new ColorBgra { Bgra = bgra };
        }

        public static ColorBgra FromBgra(byte b, byte g, byte r, byte a = 255)
        {
            return FromBgra(BgraToUInt32(b, g, r, a));
        }

        public static ColorBgra FromColor(Color color)
        {
            return FromBgra(color.B, color.G, color.R, color.A);
        }

        public static uint BgraToUInt32(byte b, byte g, byte r, byte a)
        {
            return (uint)b + ((uint)g << 8) + ((uint)r << 16) + ((uint)a << 24);
        }

        public static implicit operator ColorBgra(uint color)
        {
            return ColorBgra.FromBgra(color);
        }

        public Color ToColor()
        {
            return Color.FromArgb(Alpha, Red, Green, Blue);
        }

        public override string ToString()
        {
            return string.Format("B: {0}, G: {1}, R: {2}, A: {3}", Blue, Green, Red, Alpha);
        }
    }
}
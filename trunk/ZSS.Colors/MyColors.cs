#region License Information (GPL v2)
/*
    ZScreen - A program that allows you to upload screenshots in one keystroke.
    Copyright (C) 2008-2009  Jaex (Berk)

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
using System.Drawing;

namespace ZSS.Colors
{
    #region Public Structs

    public struct MyColor
    {
        public RGB RGB;
        public HSB HSB;
        public CMYK CMYK;

        public MyColor(Color color)
        {
            this.RGB = color;
            this.HSB = color;
            this.CMYK = color;
        }

        public static implicit operator MyColor(Color color)
        {
            return new MyColor(color);
        }

        public static implicit operator Color(MyColor color)
        {
            return color.RGB;
        }

        public static bool operator ==(MyColor left, MyColor right)
        {
            return (left.RGB == right.RGB) && (left.HSB == right.HSB) && (left.CMYK == right.CMYK);
        }

        public static bool operator !=(MyColor left, MyColor right)
        {
            return !(left == right);
        }

        public void RGBUpdate()
        {
            this.HSB = this.RGB;
            this.CMYK = this.RGB;
        }

        public void HSBUpdate()
        {
            this.RGB = this.HSB;
            this.CMYK = this.HSB;
        }

        public void CMYKUpdate()
        {
            this.RGB = this.CMYK;
            this.HSB = this.CMYK;
        }

        public override string ToString()
        {
            return String.Format("{0}\r\n{1}\r\n{2}", RGB.ToString(), HSB.ToString(), CMYK.ToString());
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }
    }

    public struct RGB
    {
        private int red;
        private int green;
        private int blue;

        public int Red
        {
            get { return red; }
            set { red = Helpers.CheckColor(value); }
        }

        public int Green
        {
            get { return green; }
            set { green = Helpers.CheckColor(value); }
        }

        public int Blue
        {
            get { return blue; }
            set { blue = Helpers.CheckColor(value); }
        }

        public RGB(int red, int green, int blue)
            : this()
        {
            this.Red = red;
            this.Green = green;
            this.Blue = blue;
        }

        public RGB(Color color)
        {
            this = new RGB(color.R, color.G, color.B);
        }

        public static implicit operator RGB(Color color)
        {
            return new RGB(color.R, color.G, color.B);
        }

        public static implicit operator Color(RGB color)
        {
            return color.ToColor();
        }

        public static implicit operator HSB(RGB color)
        {
            return color.ToHSB();
        }

        public static implicit operator CMYK(RGB color)
        {
            return color.ToCMYK();
        }

        public static bool operator ==(RGB left, RGB right)
        {
            return (left.Red == right.Red) && (left.Green == right.Green) && (left.Blue == right.Blue);
        }

        public static bool operator !=(RGB left, RGB right)
        {
            return !(left == right);
        }

        public override string ToString()
        {
            return String.Format("Red: {0}, Green: {1}, Blue: {2}", Red, Green, Blue);
        }

        public static Color ToColor(int red, int green, int blue)
        {
            return Color.FromArgb(red, green, blue);
        }

        public Color ToColor()
        {
            return ToColor(Red, Green, Blue);
        }

        public static HSB ToHSB(Color color)
        {
            HSB hsb = new HSB();

            int Max, Min, Diff, Sum;

            if (color.R > color.G) { Max = color.R; Min = color.G; }
            else { Max = color.G; Min = color.R; }
            if (color.B > Max) Max = color.B;
            else if (color.B < Min) Min = color.B;

            Diff = Max - Min;
            Sum = Max + Min;

            hsb.Brightness = (double)Max / 255;

            if (Max == 0) hsb.Saturation = 0;
            else hsb.Saturation = (double)Diff / Max;

            double q;
            if (Diff == 0) q = 0;
            else q = (double)60 / Diff;

            if (Max == color.R)
            {
                if (color.G < color.B) hsb.Hue = (double)(360 + q * (color.G - color.B)) / 360;
                else hsb.Hue = (double)(q * (color.G - color.B)) / 360;
            }
            else if (Max == color.G) hsb.Hue = (double)(120 + q * (color.B - color.R)) / 360;
            else if (Max == color.B) hsb.Hue = (double)(240 + q * (color.R - color.G)) / 360;
            else hsb.Hue = 0.0;

            return hsb;
        }

        public HSB ToHSB()
        {
            return ToHSB(this);
        }

        public static CMYK ToCMYK(Color color)
        {
            CMYK cmyk = new CMYK();
            double low = 1.0;

            cmyk.Cyan = (double)(255 - color.R) / 255;
            if (low > cmyk.Cyan)
                low = cmyk.Cyan;

            cmyk.Magenta = (double)(255 - color.G) / 255;
            if (low > cmyk.Magenta)
                low = cmyk.Magenta;

            cmyk.Yellow = (double)(255 - color.B) / 255;
            if (low > cmyk.Yellow)
                low = cmyk.Yellow;

            if (low > 0.0)
            {
                cmyk.Key = low;
            }

            return cmyk;
        }

        public CMYK ToCMYK()
        {
            return ToCMYK(this);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }
    }

    public struct HSB
    {
        private double hue;
        private double saturation;
        private double brightness;

        public double Hue
        {
            get { return hue; }
            set { hue = Helpers.CheckColor(value); }
        }

        public double Hue360
        {
            get { return hue * 360; }
            set { hue = Helpers.CheckColor(value / 360); }
        }

        public double Saturation
        {
            get { return saturation; }
            set { saturation = Helpers.CheckColor(value); }
        }

        public double Brightness
        {
            get { return brightness; }
            set { brightness = Helpers.CheckColor(value); }
        }

        public HSB(double hue, double saturation, double brightness)
            : this()
        {
            this.Hue = hue;
            this.Saturation = saturation;
            this.Brightness = brightness;
        }

        public HSB(Color color)
        {
            this = RGB.ToHSB(color);
        }

        public static implicit operator HSB(Color color)
        {
            return RGB.ToHSB(color);
        }

        public static implicit operator Color(HSB color)
        {
            return color.ToColor();
        }

        public static implicit operator RGB(HSB color)
        {
            return color.ToColor();
        }

        public static implicit operator CMYK(HSB color)
        {
            return color.ToColor();
        }

        public static bool operator ==(HSB left, HSB right)
        {
            return (left.Hue == right.Hue) && (left.Saturation == right.Saturation) && (left.Brightness == right.Brightness);
        }

        public static bool operator !=(HSB left, HSB right)
        {
            return !(left == right);
        }

        public override string ToString()
        {
            return String.Format("Hue: {0}, Saturation: {1}, Brightness: {2}", Helpers.Round(Hue * 360),
              Helpers.Round(Saturation * 100), Helpers.Round(Brightness * 100));
        }

        public static Color ToColor(HSB hsb)
        {
            int Max, Mid, Min;
            double q;

            Max = Helpers.Round(hsb.Brightness * 255);
            Min = Helpers.Round((1.0 - hsb.Saturation) * (hsb.Brightness / 1.0) * 255);
            q = (double)(Max - Min) / 255;

            if (hsb.Hue >= 0 && hsb.Hue <= (double)1 / 6)
            {
                Mid = Helpers.Round(((hsb.Hue - 0) * q) * 1530 + Min);
                return Color.FromArgb(Max, Mid, Min);
            }
            else if (hsb.Hue <= (double)1 / 3)
            {
                Mid = Helpers.Round(-((hsb.Hue - (double)1 / 6) * q) * 1530 + Max);
                return Color.FromArgb(Mid, Max, Min);
            }
            else if (hsb.Hue <= 0.5)
            {
                Mid = Helpers.Round(((hsb.Hue - (double)1 / 3) * q) * 1530 + Min);
                return Color.FromArgb(Min, Max, Mid);
            }
            else if (hsb.Hue <= (double)2 / 3)
            {
                Mid = Helpers.Round(-((hsb.Hue - 0.5) * q) * 1530 + Max);
                return Color.FromArgb(Min, Mid, Max);
            }
            else if (hsb.Hue <= (double)5 / 6)
            {
                Mid = Helpers.Round(((hsb.Hue - (double)2 / 3) * q) * 1530 + Min);
                return Color.FromArgb(Mid, Min, Max);
            }
            else if (hsb.Hue <= 1.0)
            {
                Mid = Helpers.Round(-((hsb.Hue - (double)5 / 6) * q) * 1530 + Max);
                return Color.FromArgb(Max, Min, Mid);
            }
            else
            {
                return Color.FromArgb(0, 0, 0);
            }
        }

        public static Color ToColor(double hue, double saturation, double brightness)
        {
            return ToColor(new HSB(hue, saturation, brightness));
        }

        public Color ToColor()
        {
            return ToColor(this);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }
    }

    public struct CMYK
    {
        private double cyan;
        private double magenta;
        private double yellow;
        private double key;

        public double Cyan
        {
            get { return cyan; }
            set { cyan = Helpers.CheckColor(value); }
        }

        public double Magenta
        {
            get { return magenta; }
            set { magenta = Helpers.CheckColor(value); }
        }

        public double Yellow
        {
            get { return yellow; }
            set { yellow = Helpers.CheckColor(value); }
        }

        public double Key
        {
            get { return key; }
            set { key = Helpers.CheckColor(value); }
        }

        public CMYK(double cyan, double magenta, double yellow, double key)
            : this()
        {
            this.Cyan = cyan;
            this.Magenta = magenta;
            this.Yellow = yellow;
            this.Key = key;
        }

        public CMYK(Color color)
        {
            this = RGB.ToCMYK(color);
        }

        public static implicit operator CMYK(Color color)
        {
            return RGB.ToCMYK(color);
        }

        public static implicit operator Color(CMYK color)
        {
            return color.ToColor();
        }

        public static implicit operator RGB(CMYK color)
        {
            return color.ToColor();
        }

        public static implicit operator HSB(CMYK color)
        {
            return color.ToColor();
        }

        public static bool operator ==(CMYK left, CMYK right)
        {
            return (left.Cyan == right.Cyan) && (left.Magenta == right.Magenta) && (left.Yellow == right.Yellow) &&
                (left.Key == right.Key);
        }

        public static bool operator !=(CMYK left, CMYK right)
        {
            return !(left == right);
        }

        public override string ToString()
        {
            return String.Format("Cyan: {0}, Magenta: {1}, Yellow: {2}, Key: {3}", Helpers.Round(Cyan * 100),
              Helpers.Round(Magenta * 100), Helpers.Round(Yellow * 100), Helpers.Round(Key * 100));
        }

        public static Color ToColor(CMYK cmyk)
        {
            int red = Helpers.Round(255 - (255 * cmyk.Cyan));
            int green = Helpers.Round(255 - (255 * cmyk.Magenta));
            int blue = Helpers.Round(255 - (255 * cmyk.Yellow));

            return Color.FromArgb(red, green, blue);
        }

        public static Color ToColor(double cyan, double magenta, double yellow, double key)
        {
            return ToColor(new CMYK(cyan, magenta, yellow, key));
        }

        public Color ToColor()
        {
            return ToColor(this);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }
    }

    #endregion

    public static class MyColors
    {
        public static string ColorToHex(Color color)
        {
            return string.Format("{0:X2}{1:X2}{2:X2}", color.R, color.G, color.B);
        }

        public static int ColorToDecimal(Color color)
        {
            return HexToDecimal(ColorToHex(color));
        }

        public static Color HexToColor(string hex)
        {
            string r, g, b;
            r = hex.Substring(0, 2);
            g = hex.Substring(2, 2);
            b = hex.Substring(4, 2);

            return Color.FromArgb(HexToDecimal(r), HexToDecimal(g), HexToDecimal(b));
        }

        public static int HexToDecimal(string hex)
        {
            //return int.Parse(hex, System.Globalization.NumberStyles.HexNumber);
            return Convert.ToInt32(hex, 16);
        }

        public static string DecimalToHex(int dec)
        {
            return dec.ToString("X6");
        }

        public static Color DecimalToColor(int dec)
        {
            return Color.FromArgb(dec & 0xFF, (dec & 0xff00) / 256, dec / 65536);
        }

        public static Color GetPixelColor(Point point)
        {
            Bitmap bmp = new Bitmap(1, 1);
            Graphics.FromImage(bmp).CopyFromScreen(point, new Point(0, 0), new Size(1, 1));
            return bmp.GetPixel(0, 0);
        }

        public static Color SetHue(Color c, double Hue)
        {
            HSB hsb = RGB.ToHSB(c);
            hsb.Hue = Hue;
            return hsb.ToColor();
        }

        public static Color ModifyHue(Color c, double Hue)
        {
            HSB hsb = RGB.ToHSB(c);
            hsb.Hue *= Hue;
            return hsb.ToColor();
        }

        public static Color SetSaturation(Color c, double Saturation)
        {
            HSB hsb = RGB.ToHSB(c);
            hsb.Saturation = Saturation;
            return hsb.ToColor();
        }

        public static Color ModifySaturation(Color c, double Saturation)
        {
            HSB hsb = RGB.ToHSB(c);
            hsb.Saturation *= Saturation;
            return hsb.ToColor();
        }

        public static Color SetBrightness(Color c, double brightness)
        {
            HSB hsb = RGB.ToHSB(c);
            hsb.Brightness = brightness;
            return hsb.ToColor();
        }

        public static Color ModifyBrightness(Color c, double brightness)
        {
            HSB hsb = RGB.ToHSB(c);
            hsb.Brightness *= brightness;
            return hsb.ToColor();
        }

        public static Color RandomColor()
        {
            Random rand = new Random();
            return Color.FromArgb(rand.Next(256), rand.Next(256), rand.Next(256));
        }
    }
}
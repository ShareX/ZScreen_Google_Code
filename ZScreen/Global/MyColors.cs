using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace ZSS
{
    public static class MyColors
    {
        #region Public Structs

        public struct MyColor
        {
            public Color Color;
            public RGB RGB;
            public HSB HSB;
            public CMYK CMYK;

            public MyColor(Color color)
            {
                this.Color = color;
                this.RGB = color;
                this.HSB = color;
                this.CMYK = color;
            }

            public static implicit operator MyColor(Color color)
            {
                return new MyColor(color);
            }

            public override string ToString()
            {
                return String.Format("RGB: {0}\r\nHSB: {1}\r\nCMYK: {2}",
                    RGB.ToString(), HSB.ToString(), CMYK.ToString());
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
                set { red = value; }
            }

            public int Green
            {
                get { return green; }
                set { green = value; }
            }

            public int Blue
            {
                get { return blue; }
                set { blue = value; }
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

            public override string ToString()
            {
                return String.Format("Red: {0}, Green: {1}, Blu: {2}", Red, Green, Blue);
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
        }

        public struct HSB
        {
            private double hue;
            private double saturation;
            private double brightness;

            public double Hue
            {
                get { return hue; }
                set { hue = CheckColor(value); }
            }

            public double Saturation
            {
                get { return saturation; }
                set { saturation = CheckColor(value); }
            }

            public double Brightness
            {
                get { return brightness; }
                set { brightness = CheckColor(value); }
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

            public override string ToString()
            {
                return String.Format("Hue: {0}, Saturation: {1}, Brightness: {2}", Round(Hue * 360),
                  Round(Saturation * 100), Round(Brightness * 100));
            }

            public static Color ToColor(HSB hsb)
            {
                int Max, Mid, Min;
                double q;

                Max = Round(hsb.Brightness * 255);
                Min = Round((1.0 - hsb.Saturation) * (hsb.Brightness / 1.0) * 255);
                q = (double)(Max - Min) / 255;

                if (hsb.Hue >= 0 && hsb.Hue <= (double)1 / 6)
                {
                    Mid = Round(((hsb.Hue - 0) * q) * 1530 + Min);
                    return Color.FromArgb(Max, Mid, Min);
                }
                else if (hsb.Hue <= (double)1 / 3)
                {
                    Mid = Round(-((hsb.Hue - (double)1 / 6) * q) * 1530 + Max);
                    return Color.FromArgb(Mid, Max, Min);
                }
                else if (hsb.Hue <= 0.5)
                {
                    Mid = Round(((hsb.Hue - (double)1 / 3) * q) * 1530 + Min);
                    return Color.FromArgb(Min, Max, Mid);
                }
                else if (hsb.Hue <= (double)2 / 3)
                {
                    Mid = Round(-((hsb.Hue - 0.5) * q) * 1530 + Max);
                    return Color.FromArgb(Min, Mid, Max);
                }
                else if (hsb.Hue <= (double)5 / 6)
                {
                    Mid = Round(((hsb.Hue - (double)2 / 3) * q) * 1530 + Min);
                    return Color.FromArgb(Mid, Min, Max);
                }
                else if (hsb.Hue <= 1.0)
                {
                    Mid = Round(-((hsb.Hue - (double)5 / 6) * q) * 1530 + Max);
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
                set { cyan = CheckColor(value); }
            }

            public double Magenta
            {
                get { return magenta; }
                set { magenta = CheckColor(value); }
            }

            public double Yellow
            {
                get { return yellow; }
                set { yellow = CheckColor(value); }
            }

            public double Key
            {
                get { return key; }
                set { key = CheckColor(value); }
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

            public override string ToString()
            {
                return String.Format("Cyan: {0}, Magenta: {1}, Yellow: {2}, Key: {3}", Round(Cyan * 100),
                  Round(Magenta * 100), Round(Yellow * 100), Round(Key * 100));
            }

            public static Color ToColor(CMYK cmyk)
            {
                int red = Round(255 - (255 * cmyk.Cyan));
                int green = Round(255 - (255 * cmyk.Magenta));
                int blue = Round(255 - (255 * cmyk.Yellow));

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
        }

        #endregion

        #region Public Static Methods

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

        #endregion

        #region Private Static Helpers

        private static double CheckColor(double number)
        {
            if (number > 1)
            {
                return 1;
            }
            else if (number < 0)
            {
                return 0;
            }
            else
            {
                return number;
            }
        }

        private static int Round(double val)
        {
            int ret_val = (int)val;

            int temp = (int)(val * 100);

            if ((temp % 100) >= 50)
                ret_val += 1;

            return ret_val;
        }

        private static string ToShortString(double number)
        {
            return number.ToString("0.####");
        }

        #endregion
    }
}
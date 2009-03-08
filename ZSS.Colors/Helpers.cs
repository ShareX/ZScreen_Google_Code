using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Reflection;
using System.IO;

namespace ZSS.Colors
{
    public delegate void ColorEventHandler(object sender, ColorEventArgs e);

    public class ColorEventArgs : EventArgs
    {
        public ColorEventArgs(MyColor color, DrawStyle drawStyle, bool updateControl)
        {
            this.Color = color;
            this.DrawStyle = drawStyle;
            this.UpdateControl = updateControl;
        }

        public MyColor Color;
        public DrawStyle DrawStyle;
        public bool UpdateControl;
    }

    public enum DrawStyle
    {
        Hue, Saturation, Brightness, Red, Green, Blue
    }

    public static class Helpers
    {
        public static double CheckColor(double number)
        {
            return GetBetween(number, 0, 1);
        }

        public static int CheckColor(int number)
        {
            return GetBetween(number, 0, 255);
        }

        public static int GetBetween(int number, int min, int max)
        {
            return Math.Max(Math.Min(number, max), min);
        }

        public static double GetBetween(double value, double min, double max)
        {
            return Math.Max(Math.Min(value, max), min);
        }

        public static int Round(double val)
        {
            int ret_val = (int)val;

            int temp = (int)(val * 100);

            if ((temp % 100) >= 50)
                ret_val += 1;

            return ret_val;
        }

        public static string ToShortString(double number)
        {
            return number.ToString("0.####");
        }

        public static Stream GetImageResource(string ResourceName)
        {
            Assembly asm = Assembly.GetExecutingAssembly();
            return asm.GetManifestResourceStream(ResourceName);
        }
    }
}
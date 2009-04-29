using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.ComponentModel;
using System.Drawing;

namespace ZSS
{
    public static class Extensions
    {
        public static int ToInt(this string str)
        {
            return Convert.ToInt32(str);
        }

        public static string GetDescription(this Enum value)
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
    }
}
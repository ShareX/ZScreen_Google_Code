using System;
using System.ComponentModel;
using System.Reflection;

namespace ZUploader
{
    public static class Enums
    {
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
    }
}
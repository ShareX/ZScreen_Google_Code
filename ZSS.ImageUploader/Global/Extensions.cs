using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace ZSS.ImageUploadersLib
{
    public static class Extensions
    {
        public static string ElementValue(this XElement xe, string name)
        {
            XElement xeItem = xe.Element(name);
            if (xeItem != null)
            {
                return xeItem.Value;
            }
            else
            {
                return "";
            }
        }

        public static string AttributeValue(this XElement xe, string name)
        {
            XAttribute xeItem = xe.Attribute(name);
            if (xeItem != null)
            {
                return xeItem.Value;
            }
            else
            {
                return "";
            }
        }

        public static string AttributeFirstValue(this XElement xe, params string[] names)
        {
            string value;
            foreach (string name in names)
            {
                value = xe.AttributeValue(name);
                if (!string.IsNullOrEmpty(value)) return value;
            }
            return "";
        }
    }
}
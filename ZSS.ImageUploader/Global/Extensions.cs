using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace ZSS.ImageUploaders
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
    }
}
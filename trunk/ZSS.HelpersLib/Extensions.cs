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
using System.IO;
using System.Reflection;
using System.Xml.Linq;

namespace HelpersLib
{
    public static class Extensions
    {
        public static string GetDescription(this Enum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());
            DescriptionAttribute[] attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);
            return (attributes.Length > 0) ? attributes[0].Description : value.ToString();
        }

        public static XElement GetElement(this XElement xe, params string[] elements)
        {
            XElement result = null;

            if (xe != null && elements != null && elements.Length > 0)
            {
                result = xe;

                foreach (string element in elements)
                {
                    result = result.Element(element);
                    if (result == null) break;
                }
            }

            return result;
        }

        public static XElement GetElement(this XDocument xd, params string[] elements)
        {
            if (xd != null && elements != null && elements.Length > 0)
            {
                XElement result = xd.Root;

                if (result.Name == elements[0])
                {
                    for (int i = 1; i < elements.Length; i++)
                    {
                        result = result.Element(elements[i]);
                        if (result == null) break;
                    }

                    return result;
                }
            }

            return null;
        }

        public static string GetElementValue(this XElement xe, string name)
        {
            if (xe != null)
            {
                XElement xeItem = xe.Element(name);
                if (xeItem != null)
                {
                    return xeItem.Value;
                }
            }

            return string.Empty;
        }

        public static string GetAttributeValue(this XElement xe, string name)
        {
            if (xe != null)
            {
                XAttribute xeItem = xe.Attribute(name);
                if (xeItem != null)
                {
                    return xeItem.Value;
                }
            }

            return string.Empty;
        }

        public static string GetAttributeFirstValue(this XElement xe, params string[] names)
        {
            string value;
            foreach (string name in names)
            {
                value = xe.GetAttributeValue(name);
                if (!string.IsNullOrEmpty(value))
                {
                    return value;
                }
            }

            return string.Empty;
        }

        public static void CopyStream(this Stream input, Stream output)
        {
            byte[] buffer = new byte[32768];
            int read;

            input.Position = 0;

            while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
            {
                output.Write(buffer, 0, read);
            }
        }

        public static byte[] GetBytes(this Image img)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                img.Save(ms, img.RawFormat);
                return Helpers.GetBytes(ms);
            }
        }

        public static ImageCodecInfo GetCodecInfo(this ImageFormat format)
        {
            foreach (ImageCodecInfo info in ImageCodecInfo.GetImageEncoders())
            {
                if (info.FormatID.Equals(format.Guid))
                {
                    return info;
                }
            }

            return null;
        }

        public static string GetMimeType(this ImageFormat format)
        {
            ImageCodecInfo codec = format.GetCodecInfo();

            if (codec != null) return codec.MimeType;

            return "image/unknown";
        }

        public static bool ReplaceFirst(this string text, string search, string replace, out string result)
        {
            int location = text.IndexOf(search);

            if (location < 0)
            {
                result = text;
                return false;
            }

            result = text.Remove(location, search.Length).Insert(location, replace);
            return true;
        }

        public static int Between(this int num, int min, int max)
        {
            return Math.Min(Math.Max(num, min), max);
        }
    }
}
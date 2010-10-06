#region License Information (GPL v2)

/*
    ZUploader - A program that allows you to upload images, text or files in your clipboard
    Copyright (C) 2010 ZScreen Developers

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
using System.Reflection;

namespace ZUploader
{
    public enum EDataType
    {
        File,
        Image,
        Text
    }

    public enum ImageDestType2
    {
        [Description("imageshack.us")]
        IMAGESHACK,
        [Description("tinypic.com")]
        TINYPIC,
        [Description("imagebin.ca")]
        IMAGEBIN,
        //[Description("img1.us")]
        //IMG1,
        [Description("imgur.com")]
        IMGUR,
        [Description("uploadscreenshot.com")]
        UPLOADSCREENSHOT,
        [Description("filez.muffinz.eu")]
        FilezImages,
        [Description("Use file uploader")]
        FILE
    }

    public enum TextDestType2
    {
        [Description("pastebin.com")]
        PASTEBIN,
        [Description("pastebin.ca")]
        PASTEBIN_CA,
        [Description("paste2.org")]
        PASTE2,
        [Description("slexy.org")]
        SLEXY,
        [Description("Use file uploader")]
        FILE
    }

    public enum FileUploaderType2
    {
        [Description("rapidshare.com")]
        RapidShare,
        [Description("sendspace.com")]
        SendSpace,
        [Description("filebin.ca")]
        FileBin,
        [Description("drop.io")]
        DropIO,
        [Description("share.cx")]
        ShareCX,
        [Description("filez.muffinz.eu")]
        FilezFiles,
        [Description("FTP Server")]
        FTP
    }

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
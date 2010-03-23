using System;
using System.ComponentModel;
using System.Reflection;

namespace ZUploader
{
    public enum ImageDestType2
    {
        [Description("ImageShack - www.imageshack.us")]
        IMAGESHACK,
        [Description("TinyPic - www.tinypic.com")]
        TINYPIC,
        [Description("ImageBin - www.imagebin.ca")]
        IMAGEBIN,
        [Description("Img1 - www.img1.us")]
        IMG1,
        [Description("Imgur - www.imgur.com")]
        IMGUR,
        [Description("Use File Uploader")]
        FILE
    }

    public enum TextDestType2
    {
        [Description("PasteBin - www.pastebin.com")]
        PASTEBIN,
        [Description("PasteBin - www.pastebin.ca")]
        PASTEBIN_CA,
        [Description("Paste2 - www.paste2.org")]
        PASTE2,
        [Description("Slexy 2.0 - www.slexy.org")]
        SLEXY,
        [Description("Use File Uploader")]
        FILE
        //[Description("Snipt - www.snipt.org")]
        //SNIPT
    }

    public enum FileUploaderType2
    {
        [Description("RapidShare - www.rapidshare.com")]
        RapidShare,
        [Description("SendSpace - www.sendspace.com")]
        SendSpace,
        [Description("FileBin - www.filebin.ca")]
        FileBin,
        [Description("FTP Server")]
        FTP
        //[Description("Drop.io - www.drop.io")]
        //DropIO
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
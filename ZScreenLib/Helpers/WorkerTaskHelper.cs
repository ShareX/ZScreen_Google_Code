using System.Drawing;
using System.IO;
using HelpersLib;

namespace ZScreenLib
{
    public static class WorkerTaskHelper
    {
        public static MemoryStream PrepareImage(Workflow profile, Image img, out EImageFormat imageFormat)
        {
            MemoryStream stream = img.SaveImage(profile, profile.ImageFormat);

            int sizeLimit = profile.ImageSizeLimit * 1024;
            if (profile.ImageFormat != profile.ImageFormat2 && sizeLimit > 0 && stream.Length > sizeLimit)
            {
                stream = img.SaveImage(profile, profile.ImageFormat2);
                imageFormat = profile.ImageFormat2;
            }
            else
            {
                imageFormat = profile.ImageFormat;
            }

            stream.Position = 0;

            return stream;
        }

        public static string PrepareFilename(Workflow profile, Image img, EImageFormat imageFormat, NameParserType patternType)
        {
            string ext = "png";

            switch (imageFormat)
            {
                case EImageFormat.PNG:
                    ext = "png";
                    break;
                case EImageFormat.JPEG:
                    ext = "jpg";
                    break;
                case EImageFormat.GIF:
                    ext = "gif";
                    break;
                case EImageFormat.BMP:
                    ext = "bmp";
                    break;
                case EImageFormat.TIFF:
                    ext = "tif";
                    break;
            }

            NameParser parser = new NameParser { Type = patternType, Picture = img, AutoIncrementNumber = profile.AutoIncrement };
            string pattern = profile.EntireScreenPattern;
            switch (patternType)
            {
                case NameParserType.ActiveWindow:
                    pattern = profile.ActiveWindowPattern;
                    break;
                default:
                    pattern = profile.EntireScreenPattern;
                    break;
            }
            string fn = parser.Convert(pattern);
            Engine.CoreConf.AutoIncrement = parser.AutoIncrementNumber; // issue 577; Engine.CoreConf.AutoIncrement has to be updated
            return string.Format("{0}.{1}", fn, ext);
        }
    }
}
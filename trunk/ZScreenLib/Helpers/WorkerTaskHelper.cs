using System.Drawing;
using System.IO;
using HelpersLib;

namespace ZScreenLib
{
    public static class WorkerTaskHelper
    {
        public static MemoryStream PrepareImage(Image img, out EImageFormat imageFormat)
        {
            MemoryStream stream = img.SaveImage(Engine.conf.ImageFormat);

            int sizeLimit = Engine.conf.ImageSizeLimit * 1024;
            if (Engine.conf.ImageFormat != Engine.conf.ImageFormat2 && sizeLimit > 0 && stream.Length > sizeLimit)
            {
                stream = img.SaveImage(Engine.conf.ImageFormat2);
                imageFormat = Engine.conf.ImageFormat2;
            }
            else
            {
                imageFormat = Engine.conf.ImageFormat;
            }

            stream.Position = 0;

            return stream;
        }

        public static string PrepareFilename(EImageFormat imageFormat, Image img, NameParserType patternType)
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

            NameParser parser = new NameParser { Type = patternType, Picture = img, AutoIncrementNumber = Engine.conf.AutoIncrement };
            string pattern = Engine.conf.EntireScreenPattern;
            switch (patternType)
            {
                case NameParserType.ActiveWindow:
                    pattern = Engine.conf.ActiveWindowPattern;
                    break;
                default:
                    pattern = Engine.conf.EntireScreenPattern;
                    break;
            }
            string fn = parser.Convert(pattern);
            Engine.conf.AutoIncrement = parser.AutoIncrementNumber;
            return string.Format("{0}.{1}", fn, ext);
        }
    }
}
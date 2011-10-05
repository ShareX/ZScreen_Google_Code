using System.Drawing;
using System.IO;
using System.Text;
using HelpersLib;

namespace ZScreenLib
{
    public static class WorkerTaskHelper
    {
        public static MemoryStream PrepareImage(Workflow wf, Image img, out EImageFormat imageFormat, bool targetFileSize = true)
        {
            MemoryStream stream = img.SaveImage(wf, wf.ImageFormat);

            long streamLength = stream.Length / 1024;
            int sizeLimit = wf.ImageSizeLimit * 1024;

            if (wf.ImageFormat != wf.ImageFormat2 && sizeLimit > 0 && stream.Length > sizeLimit)
            {
                stream = img.SaveImage(wf, wf.ImageFormat2);
                StaticHelper.WriteLine(ConvertImageString(streamLength, wf, stream));

                if (targetFileSize)
                {
                    while (stream.Length > sizeLimit && wf.ImageFormat2 == EImageFormat.JPEG)
                    {
                        if (wf.ImageJpegQuality == FreeImageJpegQualityType.JPEG_QUALITYBAD)
                        {
                            break;
                        }

                        wf.ImageJpegQuality = wf.ImageJpegQuality - 1;
                        stream = img.SaveImage(wf, EImageFormat.JPEG);
                        StaticHelper.WriteLine(ConvertImageString(streamLength, wf, stream));
                    }
                }

                imageFormat = wf.ImageFormat2;

            }
            else
            {
                imageFormat = wf.ImageFormat;
            }

            stream.Position = 0;

            return stream;
        }

        private static string ConvertImageString(long streamLengthPrevious, Workflow profile, Stream stream)
        {
            StringBuilder sbMsg = new StringBuilder();
            sbMsg.Append(string.Format("Converting {0} KiB {1} to {2} {3} KiB target {4} KiB",
                                                streamLengthPrevious,
                                                profile.ImageFormat.GetDescription(),
                                                stream.Length / 1024,
                                                profile.ImageFormat2.GetDescription(),
                                                profile.ImageSizeLimit));

            if (profile.ImageFormat2 == EImageFormat.JPEG)
            {
                sbMsg.Append(string.Format(" using setting {0}", profile.ImageJpegQuality.GetDescription()));
            }

            return sbMsg.ToString();
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
            Engine.Workflow.AutoIncrement = parser.AutoIncrementNumber; // issue 577; Engine.CoreConf.AutoIncrement has to be updated
            return string.Format("{0}.{1}", fn, ext);
        }
    }
}
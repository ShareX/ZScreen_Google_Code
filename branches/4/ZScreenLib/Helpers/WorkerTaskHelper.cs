using System.Drawing;
using System.IO;
using System.Text;
using FreeImageNetLib;
using HelpersLib;

namespace ZScreenLib
{
    public static class WorkerTaskHelper
    {
        public static MemoryStream PrepareImage(Workflow wf, Image img, out EImageFormat imageFormat,
            bool bConvert = true, bool bTargetFileSize = true)
        {
            imageFormat = wf.ImageFormat;
            MemoryStream stream = img.SaveImage(wf, wf.ImageFormat);

            if (bConvert)
            {
                long streamLength = stream.Length / 1024;
                int sizeLimit = wf.ConfigImageEffects.ImageSizeLimit * 1024;

                if (wf.ImageFormat != wf.ImageFormat2 && sizeLimit > 0 && stream.Length > sizeLimit)
                {
                    stream = img.SaveImage(wf, wf.ImageFormat2);

                    if (bTargetFileSize)
                    {
                        StaticHelper.WriteLine(ConvertImageString(streamLength, wf, stream));
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
            }

            stream.Position = 0;

            return stream;
        }

        private static string ConvertImageString(long streamLengthPrevious, Workflow workflow, Stream stream)
        {
            StringBuilder sbMsg = new StringBuilder();
            sbMsg.Append(string.Format("Converting {0} KiB {1} to {2} {3} KiB target {4} KiB",
                                                streamLengthPrevious,
                                                workflow.ImageFormat.GetDescription(),
                                                stream.Length / 1024,
                                                workflow.ImageFormat2.GetDescription(),
                                                workflow.ConfigImageEffects.ImageSizeLimit));

            if (workflow.ImageFormat2 == EImageFormat.JPEG)
            {
                sbMsg.Append(string.Format(" using setting {0}", workflow.ImageJpegQuality.GetDescription()));
            }

            return sbMsg.ToString();
        }

        public static string PrepareFilename(Workflow workflow, Image img, EImageFormat imageFormat, NameParser parser)
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

            string pattern = workflow.ConfigFileNaming.EntireScreenPattern;
            switch (parser.Type)
            {
                case NameParserType.ActiveWindow:
                    pattern = workflow.ConfigFileNaming.ActiveWindowPattern;
                    break;
                default:
                    pattern = workflow.ConfigFileNaming.EntireScreenPattern;
                    break;
            }
            string fn = parser.Convert(pattern);
            if (Engine.ConfigWorkflow != null)
            {
                Engine.ConfigWorkflow.ConfigFileNaming.AutoIncrement = parser.AutoIncrementNumber; // issue 577; Engine.Workflow.AutoIncrement has to be updated
            }

            string fileName = string.Format("{0}.{1}", fn, ext);

            return FileSystem.GetUniqueFileName(workflow, fileName);
        }
    }
}
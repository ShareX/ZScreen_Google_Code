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

            int sizeLimit = Engine.conf.ImageSizeLimit * 1000;
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
    }
}
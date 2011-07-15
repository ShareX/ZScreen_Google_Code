using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using HelpersLib;
using ImageQuantization;

namespace ZScreenLib
{
    public static class ImageExtensions
    {
        public static MemoryStream SaveImage(this Image img, EImageFormat imageFormat)
        {
            MemoryStream stream = new MemoryStream();

            switch (imageFormat)
            {
                case EImageFormat.PNG:
                    img.Save(stream, ImageFormat.Png);
                    break;
                case EImageFormat.JPEG:
                    img.SaveJPG(stream, Engine.conf.ImageJPEGQuality);
                    break;
                case EImageFormat.GIF:
                    img.SaveGIF(stream, Engine.conf.ImageGIFQuality);
                    break;
                case EImageFormat.BMP:
                    img.Save(stream, ImageFormat.Bmp);
                    break;
                case EImageFormat.TIFF:
                    img.Save(stream, ImageFormat.Tiff);
                    break;
            }

            return stream;
        }

        public static void SaveJPG(this Image img, Stream stream, int quality)
        {
            using (EncoderParameters encoderParameters = new EncoderParameters(1))
            {
                encoderParameters.Param[0] = new EncoderParameter(Encoder.Quality, quality);
                img.Save(stream, ImageFormat.Jpeg.GetCodecInfo(), encoderParameters);
            }
        }

        public static void SaveGIF(this Image img, Stream stream, GIFQuality quality)
        {
            if (quality == GIFQuality.Default)
            {
                img.Save(stream, ImageFormat.Gif);
            }
            else
            {
                Quantizer quantizer;
                switch (quality)
                {
                    case GIFQuality.Grayscale:
                        quantizer = new GrayscaleQuantizer();
                        break;
                    case GIFQuality.Bit4:
                        quantizer = new OctreeQuantizer(15, 4);
                        break;
                    case GIFQuality.Bit8:
                    default:
                        quantizer = new OctreeQuantizer(255, 4);
                        break;
                }

                using (Bitmap quantized = quantizer.Quantize(img))
                {
                    quantized.Save(stream, ImageFormat.Gif);
                }
            }
        }
    }
}
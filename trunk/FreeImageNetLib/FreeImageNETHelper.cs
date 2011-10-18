using System.Drawing;
using System.IO;
using FreeImageAPI;

namespace FreeImageNetLib
{
    public class FreeImageNETHelper
    {
        public static void SaveJpeg(Image img, Stream stream,
            FreeImageJpegQualityType freeImageJpegQualityType, FreeImageJpegSubSamplingType freeImageJpegSubSamplingType)
        {
            using (FreeImageAPI.FreeImageBitmap fib = new FreeImageAPI.FreeImageBitmap(img))
            {
                FREE_IMAGE_SAVE_FLAGS jpgQuality = FREE_IMAGE_SAVE_FLAGS.JPEG_QUALITYSUPERB;
                FREE_IMAGE_SAVE_FLAGS jpgSubSampling = FREE_IMAGE_SAVE_FLAGS.JPEG_SUBSAMPLING_444;

                switch (freeImageJpegQualityType)
                {
                    case FreeImageJpegQualityType.JPEG_QUALITYAVERAGE:
                        jpgQuality = FREE_IMAGE_SAVE_FLAGS.JPEG_QUALITYAVERAGE;
                        break;
                    case FreeImageJpegQualityType.JPEG_QUALITYBAD:
                        jpgQuality = FREE_IMAGE_SAVE_FLAGS.JPEG_QUALITYBAD;
                        break;
                    case FreeImageJpegQualityType.JPEG_QUALITYGOOD:
                        jpgQuality = FREE_IMAGE_SAVE_FLAGS.JPEG_QUALITYGOOD;
                        break;
                    case FreeImageJpegQualityType.JPEG_QUALITYNORMAL:
                        jpgQuality = FREE_IMAGE_SAVE_FLAGS.JPEG_QUALITYNORMAL;
                        break;
                    case FreeImageJpegQualityType.JPEG_QUALITYSUPERB:
                        jpgQuality = FREE_IMAGE_SAVE_FLAGS.JPEG_QUALITYSUPERB;
                        break;

                    case FreeImageJpegQualityType.JPEG_PROGRESSIVE_QUALITYAVERAGE:
                        jpgQuality = FREE_IMAGE_SAVE_FLAGS.JPEG_PROGRESSIVE | FREE_IMAGE_SAVE_FLAGS.JPEG_QUALITYAVERAGE;
                        break;
                    case FreeImageJpegQualityType.JPEG_PROGRESSIVE_QUALITYBAD:
                        jpgQuality = FREE_IMAGE_SAVE_FLAGS.JPEG_PROGRESSIVE | FREE_IMAGE_SAVE_FLAGS.JPEG_QUALITYBAD;
                        break;
                    case FreeImageJpegQualityType.JPEG_PROGRESSIVE_QUALITYGOOD:
                        jpgQuality = FREE_IMAGE_SAVE_FLAGS.JPEG_PROGRESSIVE | FREE_IMAGE_SAVE_FLAGS.JPEG_QUALITYGOOD;
                        break;
                    case FreeImageJpegQualityType.JPEG_PROGRESSIVE_QUALITYNORMAL:
                        jpgQuality = FREE_IMAGE_SAVE_FLAGS.JPEG_PROGRESSIVE | FREE_IMAGE_SAVE_FLAGS.JPEG_QUALITYNORMAL;
                        break;
                    case FreeImageJpegQualityType.JPEG_PROGRESSIVE_QUALITYSUPERB:
                        jpgQuality = FREE_IMAGE_SAVE_FLAGS.JPEG_PROGRESSIVE | FREE_IMAGE_SAVE_FLAGS.JPEG_QUALITYSUPERB;
                        break;
                }

                switch (freeImageJpegSubSamplingType)
                {
                    case FreeImageJpegSubSamplingType.JPEG_SUBSAMPLING_411:
                        jpgSubSampling = FREE_IMAGE_SAVE_FLAGS.JPEG_SUBSAMPLING_411;
                        break;
                    case FreeImageJpegSubSamplingType.JPEG_SUBSAMPLING_420:
                        jpgSubSampling = FREE_IMAGE_SAVE_FLAGS.JPEG_SUBSAMPLING_420;
                        break;
                    case FreeImageJpegSubSamplingType.JPEG_SUBSAMPLING_422:
                        jpgSubSampling = FREE_IMAGE_SAVE_FLAGS.JPEG_SUBSAMPLING_422;
                        break;
                    case FreeImageJpegSubSamplingType.JPEG_SUBSAMPLING_444:
                        jpgSubSampling = FREE_IMAGE_SAVE_FLAGS.JPEG_SUBSAMPLING_444;
                        break;
                }

                fib.Save(stream, FreeImageAPI.FREE_IMAGE_FORMAT.FIF_JPEG, jpgQuality | jpgSubSampling);
            }
        }

        public static void SaveGif(Image img, Stream stream)
        {
            using (FreeImageAPI.FreeImageBitmap fib = new FreeImageAPI.FreeImageBitmap(img))
            {
                fib.Save(stream, FreeImageAPI.FREE_IMAGE_FORMAT.FIF_GIF);
            }
        }

        public static void SavePng(Image img, Stream stream, FreeImagePngQuality freeImagePngQualityType, bool bInterlaced)
        {
            using (FreeImageAPI.FreeImageBitmap fib = new FreeImageAPI.FreeImageBitmap(img))
            {
                FREE_IMAGE_SAVE_FLAGS pngQuality = FREE_IMAGE_SAVE_FLAGS.PNG_Z_DEFAULT_COMPRESSION;

                switch (freeImagePngQualityType)
                {
                    case FreeImagePngQuality.PNG_Z_BEST_COMPRESSION:
                        pngQuality = FREE_IMAGE_SAVE_FLAGS.PNG_Z_BEST_COMPRESSION;
                        break;
                    case FreeImagePngQuality.PNG_Z_BEST_SPEED:
                        pngQuality = FREE_IMAGE_SAVE_FLAGS.PNG_Z_BEST_SPEED;

                        break;
                    case FreeImagePngQuality.PNG_Z_DEFAULT_COMPRESSION:
                        pngQuality = FREE_IMAGE_SAVE_FLAGS.PNG_Z_DEFAULT_COMPRESSION;
                        break;
                    case FreeImagePngQuality.PNG_Z_NO_COMPRESSION:
                        pngQuality = FREE_IMAGE_SAVE_FLAGS.PNG_Z_NO_COMPRESSION;
                        break;
                }

                if (bInterlaced)
                    pngQuality = FREE_IMAGE_SAVE_FLAGS.PNG_INTERLACED | pngQuality;

                fib.Save(stream, FreeImageAPI.FREE_IMAGE_FORMAT.FIF_PNG, pngQuality);
            }
        }
    }
}
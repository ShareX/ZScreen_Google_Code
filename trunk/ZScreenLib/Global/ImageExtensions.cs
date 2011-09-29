#region License Information (GPL v2)

/*
    ZUploader - A program that allows you to upload images, text or files in your clipboard
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

using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using FreeImageAPI;
using HelpersLib;
using ImageQuantization;

namespace ZScreenLib
{
    public static class ImageExtensions
    {
        public static MemoryStream SaveImage(this Image img, Workflow profile, EImageFormat imageFormat)
        {
            MemoryStream stream = new MemoryStream();

            switch (imageFormat)
            {
                case EImageFormat.PNG:
                    img.Save(stream, ImageFormat.Png);
                    break;
                case EImageFormat.JPEG:
                    img.SaveJPG(stream, profile, true);
                    break;
                case EImageFormat.GIF:
                    img.SaveGIF(stream, profile.ImageGIFQuality);
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

        public static void SaveJPG(this Image img, Stream stream, Workflow workflow, bool fillBackground)
        {
            if (fillBackground)
            {
                img = FillImageBackground(img, Color.White);
            }

            // Using FreeImage converter.
            using (FreeImageAPI.FreeImageBitmap fib = new FreeImageAPI.FreeImageBitmap(img))
            {
                FREE_IMAGE_SAVE_FLAGS jpgQuality = FREE_IMAGE_SAVE_FLAGS.JPEG_QUALITYSUPERB;
                FREE_IMAGE_SAVE_FLAGS jpgSubSampling = FREE_IMAGE_SAVE_FLAGS.JPEG_SUBSAMPLING_444;

                switch (workflow.ImageJpegQuality)
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
                }

                switch (workflow.ImageJpegSubSampling)
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

        public static Image FillImageBackground(Image img, Color color)
        {
            Bitmap bmp = new Bitmap(img.Width, img.Height);

            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.Clear(color);
                g.CompositingQuality = CompositingQuality.HighQuality;
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.SmoothingMode = SmoothingMode.HighQuality;
                g.DrawImageUnscaled(img, 0, 0);
            }

            return bmp;
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
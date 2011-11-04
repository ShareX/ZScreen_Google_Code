using System.Drawing.Imaging;
using HelpersLib;

namespace GraphicsMgrLib
{
    public abstract class ImageFileFormat
    {
        public abstract string Name { get; }

        public abstract string Description { get; }

        public abstract string Extension { get; }

        public abstract ImageFormat Format { get; }

        public abstract EImageFormat FormatType { get; }
    }

    public sealed class ImageFileFormatPng : ImageFileFormat
    {
        public override string Name
        {
            get { return "PNG"; }
        }

        public override string Description
        {
            get { return "Portable Network Graphics"; }
        }

        public override string Extension
        {
            get { return "png"; }
        }

        public override ImageFormat Format
        {
            get { return ImageFormat.Png; }
        }

        public override EImageFormat FormatType
        {
            get { return EImageFormat.PNG; }
        }
    }

    public sealed class ImageFileFormatJpg : ImageFileFormat
    {
        public override string Name
        {
            get { return "JPEG"; }
        }

        public override string Description
        {
            get { return "Joint Photographic Experts Group format"; }
        }

        public override string Extension
        {
            get { return "jpg"; }
        }

        public override ImageFormat Format
        {
            get { return ImageFormat.Jpeg; }
        }

        public override EImageFormat FormatType
        {
            get { return EImageFormat.JPEG; }
        }
    }

    public sealed class ImageFileFormatGif : ImageFileFormat
    {
        public override string Name
        {
            get { return "GIF"; }
        }

        public override string Description
        {
            get { return "Graphics Interchange Format"; }
        }

        public override string Extension
        {
            get { return "gif"; }
        }

        public override ImageFormat Format
        {
            get { return ImageFormat.Gif; }
        }

        public override EImageFormat FormatType
        {
            get { return EImageFormat.GIF; }
        }
    }

    public sealed class ImageFileFormatBmp : ImageFileFormat
    {
        public override string Name
        {
            get { return "BMP"; }
        }

        public override string Description
        {
            get { return "BMP file format"; }
        }

        public override string Extension
        {
            get { return "bmp"; }
        }

        public override ImageFormat Format
        {
            get { return ImageFormat.Bmp; }
        }

        public override EImageFormat FormatType
        {
            get { return EImageFormat.BMP; }
        }
    }

    public sealed class ImageFileFormatTif : ImageFileFormat
    {
        public override string Name
        {
            get { return "TIFF"; }
        }

        public override string Description
        {
            get { return "Tagged Image File Format"; }
        }

        public override string Extension
        {
            get { return "tif"; }
        }

        public override ImageFormat Format
        {
            get { return ImageFormat.Tiff; }
        }

        public override EImageFormat FormatType
        {
            get { return EImageFormat.TIFF; }
        }
    }
}
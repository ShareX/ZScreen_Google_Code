using System.ComponentModel;

namespace GraphicsMgrLib
{
    public enum GIFQuality
    {
        Default, Bit8, Bit4, Grayscale
    }

    public enum ImageFileFormatType
    {
        [Description("PNG")]
        Png,
        [Description("JPEG")]
        Jpg,
        [Description("GIF")]
        Gif,
        [Description("BMP")]
        Bmp,
        [Description("TIFF")]
        Tif
    }
}
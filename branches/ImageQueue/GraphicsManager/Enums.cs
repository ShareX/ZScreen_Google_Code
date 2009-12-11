using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace GraphicsMgrLib
{
    public enum GIFQuality
    {
        Grayscale,
        Bit4,
        Bit8
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

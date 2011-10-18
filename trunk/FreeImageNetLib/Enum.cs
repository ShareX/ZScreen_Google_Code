using System.ComponentModel;

namespace FreeImageNetLib
{
    public enum FreeImageJpegQualityType
    {
        [Description("Save with bad quality (10:1)")]
        JPEG_QUALITYBAD,
        [Description("Save with average quality (25:1)")]
        JPEG_QUALITYAVERAGE,
        [Description("Save with normal quality (50:1)")]
        JPEG_QUALITYNORMAL,
        [Description("Save with good quality (75:1)")]
        JPEG_QUALITYGOOD,
        [Description("Save with superb quality (100:1)")]
        JPEG_QUALITYSUPERB,
        [Description("Save as a progressive-JPEG with bad quality (10:1)")]
        JPEG_PROGRESSIVE_QUALITYBAD,
        [Description("Save as a progressive-JPEG with average quality (25:1)")]
        JPEG_PROGRESSIVE_QUALITYAVERAGE,
        [Description("Save as a progressive-JPEG with normal quality (50:1)")]
        JPEG_PROGRESSIVE_QUALITYNORMAL,
        [Description("Save as a progressive-JPEG with good quality (75:1)")]
        JPEG_PROGRESSIVE_QUALITYGOOD,
        [Description("Save as a progressive-JPEG with superb quality (100:1)")]
        JPEG_PROGRESSIVE_QUALITYSUPERB,
    }

    public enum FreeImageJpegSubSamplingType
    {
        [Description("Save with high 4x1 chroma subsampling (4:1:1)")]
        JPEG_SUBSAMPLING_411,
        [Description("Save with medium 2x2 medium chroma subsampling (4:2:0) - default value")]
        JPEG_SUBSAMPLING_420,
        [Description("Save with low 2x1 chroma subsampling (4:2:2)")]
        JPEG_SUBSAMPLING_422,
        [Description("Save with no chroma subsampling (4:4:4)")]
        JPEG_SUBSAMPLING_444,
    }

    public enum FreeImagePngQuality
    {
        [Description("Save using ZLib level 9 compression")]
        PNG_Z_BEST_COMPRESSION,
        [Description("Save using ZLib level 6 compression")]
        PNG_Z_DEFAULT_COMPRESSION,
        [Description("Save using ZLib level 1 compression")]
        PNG_Z_BEST_SPEED,
        [Description("Save without ZLib compression")]
        PNG_Z_NO_COMPRESSION,
    }

    public enum FreeImageTiffQuality
    {
        [Description("Save using ADOBE DEFLATE compression")]
        TIFF_ADOBE_DEFLATE,
        [Description("Save using DEFLATE compression (a.k.a. ZLib compression)")]
        TIFF_DEFLATE,
        [Description("Save using JPEG compression")]
        TIFF_JPEG,
        [Description("Save using LZW compression")]
        TIFF_LZW,
        [Description("Save using PACKBITS compression")]
        TIFF_PACKBITS,
        [Description("Save using CCITT Group 3 fax encoding")]
        TIFF_CCITTFAX3,
        [Description("Save using CCITT Group 4 fax encoding")]
        TIFF_CCITTFAX4,
        [Description("Save without any compression")]
        TIFF_NONE,
    }
}
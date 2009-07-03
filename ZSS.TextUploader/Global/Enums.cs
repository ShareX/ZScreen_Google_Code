using System.ComponentModel;
using System;

namespace ZSS.TextUploadersLib
{
    [Serializable]
    public enum TextDestType
    {
        [Description("FTP")]
        FTP,
        [Description("pastebin.com")]
        PASTEBIN,
        [Description("paste2.org")]
        PASTE2,
        [Description("slexy.org")]
        SLEXY,
    }

    [Serializable]
    public enum UrlShortenerType
    {
        [Description("3.ly")]
        THREELY,
        [Description("bit.ly")]
        BITLY,
        [Description("is.gd")]
        ISGD,
        [Description("kl.am")]
        KLAM,
        [Description("tinyurl.com")]
        TINYURL,
    }

    [Serializable]
    public enum Privacy
    {
        Public,
        Private
    }
}
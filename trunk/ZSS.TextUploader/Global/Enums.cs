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
        [Description("pastebin.ca")]
        PASTEBIN_CA,
        [Description("paste2.org")]
        PASTE2,
        [Description("slexy.org")]
        SLEXY,
    }

    [Serializable]
    public enum UrlShortenerType
    {
        [Description("tinyurl.com")]
        TINYURL,
        [Description("3.ly")]
        THREELY,
        [Description("kl.am")]
        KLAM,
        [Description("is.gd")]
        ISGD,
    }

    [Serializable]
    public enum Privacy
    {
        Public,
        Private
    }

}
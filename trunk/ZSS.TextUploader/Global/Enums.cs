using System.ComponentModel;

namespace ZSS.TextUploaders.Global
{
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

    public enum UrlShortenerType
    {
        [Description("tinyurl.com")]
        TINYURL,
        [Description("3.ly")]
        THREELY
    }
}
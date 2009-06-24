using System.ComponentModel;

namespace ZSS.TextUploaders.Global
{
    public enum TextDestType
    {
        [Description("FTP")]
        FTP,
        [Description("pastebin.com")]
        PASTEBIN,
        [Description("paste2.org")]
        PASTE2,
        [Description("slexy.org")]
        SLEXY
    }
}
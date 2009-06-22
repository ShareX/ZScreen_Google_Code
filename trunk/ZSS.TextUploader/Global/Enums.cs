using System.ComponentModel;

namespace ZSS.TextUploaders.Global
{
    public enum TextDestType
    {
        [Description("FTP")]
        FTP,
        [Description("pastebin.com")]
        PASTEBIN_COM,
        [Description("paste2.org")]
        PASTE2_ORG,
    }
}
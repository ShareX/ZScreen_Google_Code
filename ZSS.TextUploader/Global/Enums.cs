using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace ZSS.TextUploader.Global
{
    public enum TextDestType
    {
        [Description("FTP")]
        FTP,
        [Description("Paste2")]
        PASTE2,
        [Description("pastebin.ca")]
        PASTEBIN_CA
    }
}

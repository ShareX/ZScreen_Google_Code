﻿using System.ComponentModel;

namespace ZSS.TextUploader.Global
{
    public enum TextDestType
    {
        [Description("FTP")]
        FTP,
        [Description("pastebin.com")]
        PASTEBIN_COM,
        [Description("paste2.org")]
        PASTE2_ORG,
        [Description("pastebin.ca")]
        PASTEBIN_CA
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace ZSS.FileUploadersLib
{
    public enum FileUploaderType
    {
        [Description("RapidShare")]
        RapidShare,
        [Description("FTP")]
        Ftp,
    }

    public enum RapidShareAcctType
    {
        [Description("Anonymous")]
        Free,
        [Description("Collector's Account")]
        Collectors,
        [Description("Premium Account")]
        Premium
    }
}

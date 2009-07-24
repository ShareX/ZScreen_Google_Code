using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace ZSS.IndexersLib
{
    public enum InitializationMode
    {
        INTERVAL_BASED_SERVICE,
        INTERVAL_BASED_GUI,
        DATETIME_BASED_SERVICE,
        DATETIME_BASED_GUI,
        MANUAL
    }

    public enum IndexingMode
    {
        IN_EACH_DIRECTORY,
        IN_ONE_FOLDER_MERGED,
        IN_ONE_FOLDER_SEPERATE
    }

    public enum IndexingEngine
    {
        [Description("Tree Walk Utility")]
        TreeLib,
        [Description("Tree.NET")]
        TreeNetLib
    }
}

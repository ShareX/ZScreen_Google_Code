using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace ZSS.ImageEditorLib
{
    public enum ImageEditorSaveType
    {
        [Description("Save changes manually")]
        MANUAL_SAVE,
        [Description("Automatically save changes on exit")]
        AUTO_SAVE,
    }

}

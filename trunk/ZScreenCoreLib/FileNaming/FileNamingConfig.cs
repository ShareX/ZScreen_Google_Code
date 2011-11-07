using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using HelpersLib;

namespace ZScreenCoreLib
{
    public class FileNamingConfig
    {
        public string ActiveWindowPattern = "%t-%y-%mo-%d_%h.%mi.%s";

        [Category(ComponentModelStrings.FileNaming), DefaultValue(0), Description("Adjust the current Auto-Increment number.")]
        public int AutoIncrement { get; set; }

        public string EntireScreenPattern = "Screenshot-%y-%mo-%d_%h.%mi.%s";
        public int MaxNameLength = 100;

        [Category(ComponentModelStrings.FileNaming), DefaultValue(false), Description("Overwrite existing file without creating new files.")]
        public bool OverwriteFiles = false;

        public static void ApplyDefaultValues(object self)
        {
            foreach (PropertyDescriptor prop in TypeDescriptor.GetProperties(self))
            {
                DefaultValueAttribute attr = prop.Attributes[typeof(DefaultValueAttribute)] as DefaultValueAttribute;
                if (attr == null) continue;
                prop.SetValue(self, attr.Value);
            }
        }

        public FileNamingConfig()
        {
            ApplyDefaultValues(this);
        }
    }
}
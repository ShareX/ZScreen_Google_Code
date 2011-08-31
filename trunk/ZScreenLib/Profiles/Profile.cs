using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UploadersLib;
using HelpersLib;
using System.ComponentModel;

namespace ZScreenLib
{
    [Serializable]
    public class Profile
    {
        private Profile()
        {
            ApplyDefaultValues(this);
        }

        public Profile(string name)
            : this()
        {
            this.Name = name;
            this.Enabled = true;
            this.Outputs = new List<OutputEnum>();
            this.OutputsConfig = new UploadersConfig();
        }

        public static void ApplyDefaultValues(object self)
        {
            foreach (PropertyDescriptor prop in TypeDescriptor.GetProperties(self))
            {
                DefaultValueAttribute attr = prop.Attributes[typeof(DefaultValueAttribute)] as DefaultValueAttribute;
                if (attr == null) continue;
                prop.SetValue(self, attr.Value);
            }
        }

        public string Name { get; set; }
        public string Description { get; set; }

        public bool Enabled { get; set; }

        public string FileNamePattern { get; set; }
        public WorkerTask.JobLevel2 Job { get; set; }

        public List<OutputEnum> Outputs { get; set; }
        public UploadersConfig OutputsConfig { get; set; }

        // Image Settings
        public EImageFormat ImageFormat = EImageFormat.PNG;
        public int ImageJPEGQuality = 90;
        public GIFQuality ImageGIFQuality = GIFQuality.Default;
        public int ImageSizeLimit = 512;
        public EImageFormat ImageFormat2 = EImageFormat.JPEG;

        // Naming Conventions

        public string ActiveWindowPattern = "%t-%y-%mo-%d_%h.%mi.%s";
        public string EntireScreenPattern = "Screenshot-%y-%mo-%d_%h.%mi.%s";
        public string SaveFolderPattern = "%y-%mo";
        public int MaxNameLength = 100;

        [Category("File Naming"), DefaultValue(0), Description("Adjust the current Auto-Increment number.")]
        public int AutoIncrement { get; set; }

        [Category("File Naming"), DefaultValue(false), Description("Overwrite existing file without creating new files.")]
        public bool OverwriteFiles { get; set; }

        [Category("Clipboard"), DefaultValue(true), Description("Always overwrite the clipboard with the screenshot image or url.")]
        public bool ClipboardOverwrite { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using HelpersLib;
using UploadersLib;

namespace ZScreenLib
{
    [Serializable]
    public class Workflow
    {
        private Workflow()
        {
            ApplyDefaultValues(this);
        }

        public Workflow(string name)
            : this()
        {
            this.Description = name;
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

        public string Description { get; set; }

        public bool Enabled { get; set; }

        public WorkerTask.JobLevel2 Job { get; set; }

        #region Active Window

        public bool ActiveWindowPreferDWM = false;
        public bool ActiveWindowTryCaptureChildren = false;
        public bool ActiveWindowClearBackground = true;
        public bool ActiveWindowCleanTransparentCorners = true;
        public bool ActiveWindowIncludeShadows = true;
        public bool ActiveWindowShowCheckers = false;

        [Category("Screenshots / Active Window"), DefaultValue(false), Description("Freeze active window during capture. WARNING: Do not try this on a Windows process.")]
        public bool ActiveWindowGDIFreezeWindow { get; set; }

        #endregion Active Window

        public bool ShowCursor = false;

        // Image Settings
        public EImageFormat ImageFormat = EImageFormat.PNG;
        public int ImageJPEGQuality = 90;
        public GIFQuality ImageGIFQuality = GIFQuality.Default;
        public int ImageSizeLimit = 512;
        public EImageFormat ImageFormat2 = EImageFormat.JPEG;

        #region File Naming

        public string ActiveWindowPattern = "%t-%y-%mo-%d_%h.%mi.%s";
        public string EntireScreenPattern = "Screenshot-%y-%mo-%d_%h.%mi.%s";
        public string SaveFolderPattern = "%y-%mo";
        public int MaxNameLength = 100;

        [Category("File Naming"), DefaultValue(0), Description("Adjust the current Auto-Increment number.")]
        public int AutoIncrement { get; set; }

        [Category("File Naming"), DefaultValue(false), Description("Overwrite existing file without creating new files.")]
        public bool OverwriteFiles { get; set; }

        #endregion File Naming

        #region Outputs

        [Category("Clipboard"), DefaultValue(true), Description("Always overwrite the clipboard with the screenshot image or url.")]
        public bool ClipboardOverwrite { get; set; }

        public List<OutputEnum> Outputs { get; set; }
        public UploadersConfig OutputsConfig { get; set; }

        #endregion Outputs
    }
}
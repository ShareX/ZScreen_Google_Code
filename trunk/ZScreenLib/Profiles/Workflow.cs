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
        #region I/O Methods

        public bool Write(string filePath)
        {
            return SettingsHelper.Save<Workflow>(this, filePath, SerializationType.Xml);
        }

        public static Workflow Read(string filePath)
        {
            return SettingsHelper.Load<Workflow>(filePath, SerializationType.Xml);
        }

        #endregion I/O Methods

        public Workflow()
        {
            this.Description = "New Workflow";
            this.Enabled = true;
            this.Outputs = new List<OutputEnum>();
            this.OutputsConfig = new UploadersConfig();
            ApplyDefaultValues(this);
        }

        public Workflow(string name)
            : this()
        {
            this.Description = name;
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

        [Browsable(false)]
        public string Description { get; set; }

        [Browsable(false)]
        public bool Enabled { get; set; }

        [Browsable(false)]
        public WorkerTask.JobLevel2 Job { get; set; }

        #region Active Window

        public bool ActiveWindowPreferDWM = false;
        public bool ActiveWindowTryCaptureChildren = false;
        public bool ActiveWindowClearBackground = true;
        public bool ActiveWindowCleanTransparentCorners = true;
        public bool ActiveWindowIncludeShadows = true;
        public bool ActiveWindowShowCheckers = false;

        [Category(ComponentModelStrings.ScreenshotsActiveWindow), DefaultValue(false), Description("Freeze active window during capture. WARNING: Do not try this on a Windows process.")]
        public bool ActiveWindowGDIFreezeWindow { get; set; }

        #endregion Active Window

        public bool DrawCursor = false;

        // Image Settings
        public EImageFormat ImageFormat = EImageFormat.PNG;
        public FreeImageJpegQualityType ImageJpegQuality = FreeImageJpegQualityType.JPEG_QUALITYSUPERB;
        public FreeImageJpegSubSamplingType ImageJpegSubSampling = FreeImageJpegSubSamplingType.JPEG_SUBSAMPLING_444;
        public GIFQuality ImageGIFQuality = GIFQuality.Default;
        public int ImageSizeLimit = 512;
        public EImageFormat ImageFormat2 = EImageFormat.JPEG;

        [Category(ComponentModelStrings.InputsAnimatedImages), DefaultValue(AnimatedImageFormat.GIF), Description("Animated image type.")]
        public AnimatedImageFormat ImageFormatAnimated { get; set; }
        [Category(ComponentModelStrings.InputsAnimatedImages), DefaultValue(10), Description("Maximum number of frames per animated image. Images will be uploaded individually after this value.")]
        public int ImageAnimatedFramesMax { get; set; }
        [Category(ComponentModelStrings.InputsAnimatedImages), DefaultValue(1), Description("Delay in seconds between each frame of the animated image.")]
        public int ImageAnimatedFramesDelay { get; set; }

        #region File Naming

        public string ActiveWindowPattern = "%t-%y-%mo-%d_%h.%mi.%s";
        public string EntireScreenPattern = "Screenshot-%y-%mo-%d_%h.%mi.%s";
        public string SaveFolderPattern = "%y-%mo";
        public int MaxNameLength = 100;

        [Category(ComponentModelStrings.FileNaming), DefaultValue(0), Description("Adjust the current Auto-Increment number.")]
        public int AutoIncrement { get; set; }

        [Category(ComponentModelStrings.FileNaming), DefaultValue(false), Description("Overwrite existing file without creating new files.")]
        public bool OverwriteFiles = false;

        #endregion File Naming

        #region Outputs

        [Category(ComponentModelStrings.OutputsClipboard), DefaultValue(true), Description("Always overwrite the clipboard with the screenshot image or url.")]
        public bool ClipboardOverwrite { get; set; }

        [Browsable(false)]
        public List<OutputEnum> Outputs { get; set; }

        [Browsable(false)]
        public UploadersConfig OutputsConfig { get; set; }

        #endregion Outputs
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Drawing.Drawing2D;
using System.Xml.Serialization;
using FreeImageNetLib;
using GradientTester;
using GraphicsMgrLib;
using HelpersLib;
using HelpersLib.Hotkey;
using UploadersLib;
using ZScreenCoreLib;
using ZScreenLib.Helpers;

namespace ZScreenLib
{
    [Serializable]
    public class Workflow
    {
        #region 0 Properties

        private bool bPerformActions = false;

        public CaptureEngineType CaptureEngineMode2 = CaptureEngineType.GDI;

        [Browsable(false)]
        public string Description { get; set; }

        public bool DrawCursor = false;

        [Browsable(false)]
        public bool Enabled { get; set; }

        public HotkeySetting Hotkey = new HotkeySetting();

        [Browsable(false)]
        public string ID { get; set; }

        [Browsable(false)]
        public WorkerTask.JobLevel2 Job { get; set; }

        [Browsable(false)]
        public bool PerformActions
        {
            get
            {
                return bPerformActions ||
                 System.Windows.Forms.Control.IsKeyLocked(System.Windows.Forms.Keys.CapsLock) && ImageEditorOnKeyPress == EImageEditorOnKeyLock.CapsLock ||
                 System.Windows.Forms.Control.IsKeyLocked(System.Windows.Forms.Keys.NumLock) && ImageEditorOnKeyPress == EImageEditorOnKeyLock.NumLock ||
                 System.Windows.Forms.Control.IsKeyLocked(System.Windows.Forms.Keys.Scroll) && ImageEditorOnKeyPress == EImageEditorOnKeyLock.ScrollLock;
            }
            set
            {
                bPerformActions = value;
            }
        }

        #endregion 0 Properties

        #region 1 Constructors

        public Workflow()
        {
            this.ID = ZAppHelper.GetRandomAlphanumeric(12);
            this.Description = "New Workflow";
            this.Enabled = true;
            ApplyDefaultValues(this);
        }

        public Workflow(string name)
            : this()
        {
            this.Description = name;
        }

        #endregion 1 Constructors

        #region 1 Helper Methods

        public static void ApplyDefaultValues(object self)
        {
            foreach (PropertyDescriptor prop in TypeDescriptor.GetProperties(self))
            {
                DefaultValueAttribute attr = prop.Attributes[typeof(DefaultValueAttribute)] as DefaultValueAttribute;
                if (attr == null) continue;
                prop.SetValue(self, attr.Value);
            }
        }

        public void Start()
        {
        }

        #endregion 1 Helper Methods

        #region 1 I/O Methods

        public static Workflow Read(string filePath)
        {
            // Encrypt passwords
            return SettingsHelper.Load<Workflow>(filePath, SerializationType.Xml);
        }

        public bool Write(string filePath)
        {
            return SettingsHelper.Save(this, filePath, SerializationType.Xml);
        }

        #endregion 1 I/O Methods

        #region File Naming

        public FileNamingConfig ConfigFileNaming = new FileNamingConfig();
        public string SaveFolderPattern = "%y-%mo";

        #endregion File Naming

        #region Image Manipulation

        [XmlElement("ClipboardBackgroundColor"), BrowsableAttribute(false)]
        public XColor ClipboardBackgroundArgb
        {
            get
            {
                return ClipboardBackgroundColor;
            }
            set
            {
                ClipboardBackgroundColor = value;
            }
        }

        [XmlIgnore(), Category(ComponentModelStrings.OutputsClipboard), Description("Background color of images captured to clipboard.")]
        public Color ClipboardBackgroundColor { get; set; }

        public WatermarkConfig ConfigWatermark = new WatermarkConfig();

        // Image Editor
        [Category(ComponentModelStrings.Screenshots), DefaultValue(EImageEditorOnKeyLock.None), Description("Automatically start Image Editor on a key press.")]
        public EImageEditorOnKeyLock ImageEditorOnKeyPress { get; set; }

        // Image Settings

        public EImageFormat ImageFormat = EImageFormat.PNG;
        public EImageFormat ImageFormat2 = EImageFormat.JPEG;
        public GIFQuality ImageGIFQuality = GIFQuality.Default;
        public FreeImageJpegQualityType ImageJpegQuality = FreeImageJpegQualityType.JPEG_QUALITYSUPERB;
        public FreeImageJpegSubSamplingType ImageJpegSubSampling = FreeImageJpegSubSamplingType.JPEG_SUBSAMPLING_444;
        public FreeImagePngQuality ImagePngCompression = FreeImagePngQuality.PNG_Z_DEFAULT_COMPRESSION;
        public bool ImagePngInterlaced = false;
        public FreeImageTiffQuality ImageTiffCompression = FreeImageTiffQuality.TIFF_NONE;

        #endregion Image Manipulation

        #region Inputs / Active Window

        public bool ActiveWindowClearBackground = true;
        public XColor ActiveWindowDwmBackColor = Color.White;
        public bool ActiveWindowDwmUseCustomBackground = false;

        [Category(ComponentModelStrings.ScreenshotsActiveWindow), DefaultValue(false), Description("Freeze active window during capture. WARNING: Do not try this on a Windows process.")]
        public bool ActiveWindowGDIFreezeWindow { get; set; }

        public bool ActiveWindowIncludeShadows = true;
        public bool ActiveWindowShowCheckers = false;

        public bool ActiveWindowTryCaptureChildren = false;

        #endregion Inputs / Active Window

        #region Inputs / File Upload

        [Category(ComponentModelStrings.InputsAnimatedImages), DefaultValue(1), Description("Delay in seconds between each frame of the animated image.")]
        public int ImageAnimatedFramesDelay { get; set; }

        [Category(ComponentModelStrings.InputsAnimatedImages), DefaultValue(10), Description("Maximum number of frames per animated image. Images will be uploaded individually after this value.")]
        public int ImageAnimatedFramesMax { get; set; }

        // Inputs / Animated Images

        [Category(ComponentModelStrings.InputsAnimatedImages), DefaultValue(AnimatedImageFormat.GIF), Description("Animated image type.")]
        public AnimatedImageFormat ImageFormatAnimated { get; set; }

        #endregion Inputs / File Upload

        #region Outputs

        [Category(ComponentModelStrings.OutputsClipboard), DefaultValue(true), Description("Always overwrite the clipboard with the screenshot image or url.")]
        public bool ClipboardOverwrite { get; set; }

        [Category(ComponentModelStrings.OutputsClipboard), DefaultValue(false), Description("Extended compatibility for images copied to Clipboard")]
        public bool ClipboardForceBmp { get; set; }

        public DestConfig DestConfig = new DestConfig();

        #endregion Outputs

        #region Sound Settings

        [Category(ComponentModelStrings.SoundSettings), DefaultValue(false), Description("Enable sound when screenshot is taken.")]
        public bool EnableSoundTaskBegin { get; set; }

        [Category(ComponentModelStrings.SoundSettings), DefaultValue(false), Description("Enable custom sounds when upload completed.")]
        public bool EnableSoundTaskCompleted { get; set; }

        [Category(ComponentModelStrings.SoundSettings), Description("Location of .wav file.\nIf no sound is selected, a default camera click will play")]
        [EditorAttribute(typeof(SoundFileNameEditor), typeof(UITypeEditor))]
        public string SoundImagePath { get; set; }

        [Category(ComponentModelStrings.SoundSettings), Description("Location of .wav file.")]
        [EditorAttribute(typeof(SoundFileNameEditor), typeof(UITypeEditor))]
        public string SoundPath { get; set; }

        #endregion Sound Settings
    }
}
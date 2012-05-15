using System.ComponentModel;
using System.Drawing;
using System.Xml.Serialization;
using HelpersLib;
using HelpersLib.GraphicsHelper;

namespace ZScreenCoreLib
{
    public class ImageEffectsConfig
    {
        public static void ApplyDefaultValues(object self)
        {
            foreach (PropertyDescriptor prop in TypeDescriptor.GetProperties(self))
            {
                DefaultValueAttribute attr = prop.Attributes[typeof(DefaultValueAttribute)] as DefaultValueAttribute;
                if (attr == null) continue;
                prop.SetValue(self, attr.Value);
            }
        }

        public ImageEffectsConfig()
        {
            ApplyDefaultValues(this);
        }

        // Screenshots / Bevel

        [Category(ComponentModelStrings.ScreenshotsBevel), DefaultValue(false), Description("Add bevel effect to screenshots.")]
        public bool BevelEffect { get; set; }

        [Category(ComponentModelStrings.ScreenshotsBevel), DefaultValue(15), Description("Bevel effect size.")]
        public int BevelEffectOffset { get; set; }

        [Category(ComponentModelStrings.ScreenshotsBevel), DefaultValue(FilterType.Brightness), Description("Bevel effect filter type.")]
        public FilterType BevelFilterType { get; set; }

        //Screenshots / Border

        [Category(ComponentModelStrings.ScreenshotsBorder), DefaultValue(false), Description("Add border to screenshots.")]
        public bool BorderEffect { get; set; }

        [XmlElement("BorderEffectColor"), BrowsableAttribute(false)]
        public XColor BorderEffectArgb
        {
            get
            {
                return BorderEffectColor;
            }
            set
            {
                BorderEffectColor = value;
            }
        }

        #region Image Size

        public ImageSizeType ImageSizeType = ImageSizeType.DEFAULT;
        public int ImageSizeFixedHeight = 500;
        public int ImageSizeFixedWidth = 500;
        public int ImageSizeLimit = 512;
        public float ImageSizeRatioPercentage = 50.0f;

        #endregion Image Size

        [XmlIgnore(), Category(ComponentModelStrings.ScreenshotsBorder), Description("Border Color.")]
        public Color BorderEffectColor { get; set; }

        [Category(ComponentModelStrings.ScreenshotsBorder), DefaultValue(1), Description("Border size in px.")]
        public int BorderEffectSize { get; set; }

        // Screenshots / Reflection

        [Category(ComponentModelStrings.ScreenshotsReflection), DefaultValue(false), Description("Draw reflection bottom of screenshots.")]
        public bool DrawReflection { get; set; }

        [Category(ComponentModelStrings.ScreenshotsReflection), DefaultValue(0), Description("Reflection position will start: Screenshot height + Offset")]
        public int ReflectionOffset { get; set; }

        [Category(ComponentModelStrings.ScreenshotsReflection), DefaultValue(20), Description("Reflection height size relative to screenshot height.")]
        public int ReflectionPercentage { get; set; }

        [Category(ComponentModelStrings.ScreenshotsReflection), DefaultValue(true), Description("Adding skew to reflection from bottom left to bottom right.")]
        public bool ReflectionSkew { get; set; }

        [Category(ComponentModelStrings.ScreenshotsReflection), DefaultValue(25), Description("How much pixel skew left to right.")]
        public int ReflectionSkewSize { get; set; }

        [Category(ComponentModelStrings.ScreenshotsReflection), DefaultValue(255), Description("Reflection transparency start from this value to 0.")]
        public int ReflectionTransparency { get; set; }
    }

    public enum ImageSizeType
    {
        DEFAULT, FIXED, RATIO
    }
}
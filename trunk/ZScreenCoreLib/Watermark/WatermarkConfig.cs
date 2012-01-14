using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using GradientTester;
using HelpersLib;

namespace ZScreenCoreLib
{
    public class WatermarkConfig
    {
        public GradientMakerSettings GradientMakerOptions = new GradientMakerSettings();
        public bool WatermarkAddReflection = false;
        public bool WatermarkAutoHide = true;
        public decimal WatermarkBackTrans = 225;
        public XColor WatermarkBorderArgb = Color.Black;
        public decimal WatermarkCornerRadius = 4;

        [Category(ComponentModelStrings.InputsClipboard), DefaultValue(false), Description("Do not apply watermark during Clipboard Upload")]
        public bool WatermarkExcludeClipboardUpload { get; set; }

        public XFont WatermarkFont = new XFont("Arial", 8);
        public XColor WatermarkFontArgb = Color.White;
        public decimal WatermarkFontTrans = 255;
        public XColor WatermarkGradient1Argb = Color.FromArgb(85, 85, 85);
        public XColor WatermarkGradient2Argb = Color.Black;
        public LinearGradientMode WatermarkGradientType = LinearGradientMode.Vertical;

        public string WatermarkImageLocation = "";
        public decimal WatermarkImageScale = 100;
        public WatermarkType WatermarkMode = WatermarkType.NONE;
        public decimal WatermarkOffset = 5;
        public WatermarkPositionType WatermarkPositionMode = WatermarkPositionType.BOTTOM_RIGHT;

        public string WatermarkText = "%h:%mi";
        public bool WatermarkUseBorder = false;
        public bool WatermarkUseCustomGradient = false;
    }

    public enum WatermarkPositionType
    {
        [Description("Top - Left")]
        TOP_LEFT,
        [Description("Top - Center")]
        TOP,
        [Description("Top - Right")]
        TOP_RIGHT,
        [Description("Center - Left")]
        LEFT,
        [Description("Centered")]
        CENTER,
        [Description("Center - Right")]
        RIGHT,
        [Description("Bottom - Left")]
        BOTTOM_LEFT,
        [Description("Bottom - Center")]
        BOTTOM,
        [Description("Bottom - Right")]
        BOTTOM_RIGHT
    }

    public enum WatermarkType
    {
        [Description("None")]
        NONE,
        [Description("Text")]
        TEXT,
        [Description("Image")]
        IMAGE
    }
}
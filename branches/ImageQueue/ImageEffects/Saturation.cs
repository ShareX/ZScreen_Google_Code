using System.Drawing;
using Plugins;
using GraphicsManager;

namespace ImageAdjustment
{
    public class Saturation : IPluginItem
    {
        public override string Name { get { return "Saturation"; } }

        public override string Description { get { return "Saturation"; } }

        private float saturationValue;

        public float SaturationValue
        {
            get
            {
                return saturationValue;
            }
            set
            {
                saturationValue = value;
                OnPreviewTextChanged(saturationValue + "%");
            }
        }

        public override Image ApplyEffect(Image img)
        {
            return ColorMatrixMgr.ApplyColorMatrix(img, ColorMatrixMgr.Saturation(SaturationValue));
        }
    }
}
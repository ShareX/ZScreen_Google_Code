using System.Drawing;
using GraphicsMgrLib;
using Plugins;

namespace ImageAdjustment
{
    public class Hue : IPluginItem
    {
        public override string Name { get { return "Hue"; } }

        public override string Description { get { return "Hue"; } }

        private float angle;

        public float Angle
        {
            get
            {
                return angle;
            }
            set
            {
                angle = value;
                OnPreviewTextChanged(angle.ToString());
            }
        }

        public override Image ApplyEffect(Image img)
        {
            return ColorMatrixMgr.ApplyColorMatrix(img, ColorMatrixMgr.Hue(angle));
        }
    }
}
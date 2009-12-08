using System.Drawing;
using Plugins;

namespace ImageEffects
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
            return Helpers.ApplyColorMatrix(img, Helpers.Hue(angle));
        }
    }
}
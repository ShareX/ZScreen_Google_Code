using System.Drawing;
using Plugins;

namespace ImageEffects
{
    public class Brightness : IPluginItem
    {
        public override string Name { get { return "Brightness"; } }

        public override string Description { get { return "Brightness"; } }

        private float brightnessValue;

        public float BrightnessValue
        {
            get
            {
                return brightnessValue;
            }
            set
            {
                brightnessValue = value;
                OnPreviewTextChanged(brightnessValue + "%");
            }
        }

        public override Image ApplyEffect(Image img)
        {
            return Helpers.ApplyColorMatrix(img, Helpers.Brightness(BrightnessValue));
        }
    }
}
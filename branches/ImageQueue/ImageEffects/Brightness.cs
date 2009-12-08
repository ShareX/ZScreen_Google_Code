using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using Plugins;

namespace ImageEffects
{
    public class Brightness : IPluginItem
    {
        public override string Name { get { return "Brightness"; } }

        public override string Description { get { return "Brightness"; } }

        private int brightnessValue;

        public int BrightnessValue
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

        public override bool ApplyEffect(Image img)
        {
            Helpers.ApplyColorMatrix(img, Helpers.Brightness(BrightnessValue));
            return true;
        }
    }
}
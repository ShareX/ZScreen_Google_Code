using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Plugins;
using System.Drawing;

namespace ImageEffects
{
    public class Saturation : IPluginItem
    {
        public override string Name { get { return "Saturation"; } }

        public override string Description { get { return "Saturation"; } }

        private int saturationValue;

        public int SaturationValue
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

        public override bool ApplyEffect(Image img)
        {
            Helpers.ApplyColorMatrix(img, Helpers.Saturation(SaturationValue));
            return true;
        }
    }
}
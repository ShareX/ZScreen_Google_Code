using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using Plugins;

namespace ImageEffects
{
    public class Hue : IPluginItem
    {
        public override string Name { get { return "Hue"; } }

        public override string Description { get { return "Hue"; } }

        private int angle;

        public int Angle
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

        public override bool ApplyEffect(Image img)
        {
            Helpers.ApplyColorMatrix(img, Helpers.Hue(angle));
            return true;
        }
    }
}
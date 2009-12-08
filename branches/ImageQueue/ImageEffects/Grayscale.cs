using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using Plugins;

namespace ImageEffects
{
    public class Grayscale : IPluginItem
    {
        public override string Name { get { return "Grayscale"; } }

        public override string Description { get { return "Grayscale"; } }

        public override bool ApplyEffect(Image img)
        {
            Helpers.ApplyColorMatrix(img, Helpers.Grayscale());
            return true;
        }
    }
}
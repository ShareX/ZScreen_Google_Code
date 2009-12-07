using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using Plugins;

namespace ImageEffects
{
    public class Inverse : IPluginItem
    {
        public override string Name { get { return "Inverse"; } }

        public override string Description { get { return "Inverse"; } }

        public override bool ApplyEffect(Image img)
        {
            Helpers.ApplyColorMatrix(img, Helpers.InverseFilter());
            return true;
        }
    }
}
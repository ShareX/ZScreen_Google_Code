using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using Plugins;

namespace ImageEffects
{
    public class Contrast : IPluginItem
    {
        public override string Name { get { return "Contrast"; } }

        public override string Description { get { return "Contrast"; } }

        private int contrastValue;

        public int ContrastValue
        {
            get
            {
                return contrastValue;
            }
            set
            {
                contrastValue = value;
                OnPreviewTextChanged(contrastValue + "%");
            }
        }

        public override bool ApplyEffect(Image img)
        {
            Helpers.ApplyColorMatrix(img, Helpers.Contrast(ContrastValue));
            return true;
        }
    }
}
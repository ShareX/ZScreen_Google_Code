using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using Plugins;

namespace ImageEffects
{
    public class Colorize : IPluginItem
    {
        public override string Name { get { return "Colorize"; } }

        public override string Description { get { return "Colorize"; } }

        private Color color;

        public Color Color
        {
            get
            {
                return color;
            }
            set
            {
                color = value;
                OnPreviewTextChanged(color.ToString());
            }
        }

        private int percentage;

        public int Percentage
        {
            get
            {
                return percentage;
            }
            set
            {
                percentage = value;
                OnPreviewTextChanged(color.ToString());
            }
        }

        public override bool ApplyEffect(Image img)
        {
            Helpers.ApplyColorMatrix(img, Helpers.Colorize(color, percentage));
            return true;
        }
    }
}
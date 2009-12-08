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
                ChangePreviewText();
            }
        }

        private float percentage;

        public float Percentage
        {
            get
            {
                return percentage;
            }
            set
            {
                percentage = value;
                ChangePreviewText();
            }
        }

        private void ChangePreviewText()
        {
            OnPreviewTextChanged(string.Format("{0} {1}%", color.ToString(), percentage));
        }

        public override Image ApplyEffect(Image img)
        {
            return Helpers.ApplyColorMatrix(img, Helpers.Colorize(color, percentage));
        }
    }
}
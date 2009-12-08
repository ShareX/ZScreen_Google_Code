using System.Drawing;
using Plugins;
using GraphicsManager;

namespace ImageEffects
{
    public class Alpha : IPluginItem
    {
        public override string Name { get { return "Alpha"; } }

        public override string Description { get { return "Alpha"; } }

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

        private float addition;

        public float Addition
        {
            get
            {
                return addition;
            }
            set
            {
                addition = value;
                ChangePreviewText();
            }
        }

        private void ChangePreviewText()
        {
            OnPreviewTextChanged(string.Format("{0}% {1}", percentage, addition));
        }

        public override Image ApplyEffect(Image img)
        {
            return ColorMatrixMgr.ApplyColorMatrix(img, ColorMatrixMgr.Alpha(percentage, addition));
        }
    }
}
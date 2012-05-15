using System.Drawing;
using HelpersLib.GraphicsHelper;
using ImageEffects.IPlugin;

namespace ImageAdjustment
{
    public class Contrast : IPluginItem
    {
        public override string Name { get { return "Contrast"; } }

        public override string Description { get { return "Contrast"; } }

        private float contrastValue;

        public float ContrastValue
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

        public override Image ApplyEffect(Image img)
        {
            return ColorMatrixMgr.ApplyColorMatrix(img, ColorMatrixMgr.Contrast(ContrastValue));
        }
    }
}
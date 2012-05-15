using System.ComponentModel;
using System.Drawing;
using HelpersLib.GraphicsHelper;
using ImageEffects.IPlugin;

namespace ImageAdjustment
{
    public class Gamma : IPluginItem
    {
        public override string Name { get { return "Gamma"; } }

        public override string Description { get { return "Gamma"; } }

        private float gammaValue;

        [Description("Default value 0\nValue need to be between -100 to 10000")]
        public float GammaValue
        {
            get
            {
                return gammaValue;
            }
            set
            {
                gammaValue = value;
                OnPreviewTextChanged(gammaValue + "%");
            }
        }

        public override Image ApplyEffect(Image img)
        {
            return ColorMatrixMgr.ChangeGamma(img, gammaValue);
        }
    }
}
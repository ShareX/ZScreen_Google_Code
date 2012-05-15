using System.Drawing;
using HelpersLib.GraphicsHelper;
using ImageEffects.IPlugin;

namespace ImageAdjustment
{
    public class Grayscale : IPluginItem
    {
        public override string Name { get { return "Grayscale"; } }

        public override string Description { get { return "Grayscale"; } }

        public override Image ApplyEffect(Image img)
        {
            return ColorMatrixMgr.ApplyColorMatrix(img, ColorMatrixMgr.Grayscale());
        }
    }
}
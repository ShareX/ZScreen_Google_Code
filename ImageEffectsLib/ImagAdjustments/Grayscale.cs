using System.Drawing;
using Plugins;
using GraphicsMgrLib;

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
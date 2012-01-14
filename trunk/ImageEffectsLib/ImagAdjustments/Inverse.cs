using System.Drawing;
using GraphicsMgrLib;
using Plugins;

namespace ImageAdjustment
{
    public class Inverse : IPluginItem
    {
        public override string Name { get { return "Inverse"; } }

        public override string Description { get { return "Inverse"; } }

        public override Image ApplyEffect(Image img)
        {
            return ColorMatrixMgr.ApplyColorMatrix(img, ColorMatrixMgr.Inverse());
        }
    }
}
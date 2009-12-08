using System.Drawing;
using Plugins;

namespace ImageEffects
{
    public class Grayscale : IPluginItem
    {
        public override string Name { get { return "Grayscale"; } }

        public override string Description { get { return "Grayscale"; } }

        public override Image ApplyEffect(Image img)
        {
            return Helpers.ApplyColorMatrix(img, Helpers.Grayscale());
        }
    }
}
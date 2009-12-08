using System.Drawing;
using Plugins;

namespace ImageEffects
{
    public class Inverse : IPluginItem
    {
        public override string Name { get { return "Inverse"; } }

        public override string Description { get { return "Inverse"; } }

        public override Image ApplyEffect(Image img)
        {
            return Helpers.ApplyColorMatrix(img, Helpers.Inverse());
        }
    }
}
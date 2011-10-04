using System.Collections.Generic;
using Plugins;

namespace ImageAdjustment
{
    public class ImageEffectsPlugin : IPluginInterface
    {
        public List<IPluginItem> PluginItems { get; set; }

        public string Name
        {
            get
            {
                return "Image Adjustment";
            }
        }

        public string Description
        {
            get
            {
                return "Image Adjustment";
            }
        }

        public ImageEffectsPlugin()
        {
            PluginItems = new List<IPluginItem>();
            PluginItems.Add(new Alpha());
            PluginItems.Add(new Brightness());
            PluginItems.Add(new Colorize());
            PluginItems.Add(new Contrast());
            PluginItems.Add(new Gamma());
            PluginItems.Add(new Grayscale());
            PluginItems.Add(new Hue());
            PluginItems.Add(new Inverse());
            PluginItems.Add(new Saturation());
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Plugins;

namespace ImageEffects
{
    public class ImageEffectsPlugin : IPluginInterface
    {
        public List<IPluginItem> PluginItems { get; set; }

        public string Name
        {
            get
            {
                return "Image Effects";
            }
        }

        public string Description
        {
            get
            {
                return "Image Effects";
            }
        }

        public ImageEffectsPlugin()
        {
            PluginItems = new List<IPluginItem>();
            PluginItems.Add(new Brightness());
            PluginItems.Add(new Colorize());
            PluginItems.Add(new Contrast());
            PluginItems.Add(new Grayscale());
            PluginItems.Add(new Hue());
            PluginItems.Add(new Inverse());
            PluginItems.Add(new Saturation());
        }
    }
}
using System.Collections.Generic;
using Plugins;

namespace ImageManipulation
{
    public class ImageEffectsPlugin : IPluginInterface
    {
        public List<IPluginItem> PluginItems { get; set; }

        public string Name
        {
            get
            {
                return "Image Manipulation";
            }
        }

        public string Description
        {
            get
            {
                return "Image Manipulation";
            }
        }

        public ImageEffectsPlugin()
        {
            PluginItems = new List<IPluginItem>();
            PluginItems.Add(new Reflection());
            PluginItems.Add(new Resize());
            PluginItems.Add(new Rotate());
            PluginItems.Add(new Scale());
        }
    }
}
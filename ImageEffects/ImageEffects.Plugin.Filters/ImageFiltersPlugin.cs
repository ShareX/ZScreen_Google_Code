using System.Collections.Generic;
using ImageEffects.IPlugin;

namespace ImageFilters
{
    public class ImageFiltersPlugin : IPluginInterface
    {
        public List<IPluginItem> PluginItems { get; set; }

        public string Name
        {
            get
            {
                return "Image Filters";
            }
        }

        public string Description
        {
            get
            {
                return "Image Filters";
            }
        }

        public ImageFiltersPlugin()
        {
            PluginItems = new List<IPluginItem>();
            PluginItems.Add(new Blur());
            PluginItems.Add(new Shadow());
        }
    }
}
using System.Collections.Generic;

namespace ImageEffects.IPlugin
{
    public interface IPluginInterface
    {
        List<IPluginItem> PluginItems { get; set; }

        string Name { get; }
        string Description { get; }
    }
}
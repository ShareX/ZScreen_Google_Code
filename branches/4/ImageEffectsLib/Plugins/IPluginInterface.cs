using System.Collections.Generic;

namespace Plugins
{
    public interface IPluginInterface
    {
        List<IPluginItem> PluginItems { get; set; }

        string Name { get; }
        string Description { get; }
    }
}
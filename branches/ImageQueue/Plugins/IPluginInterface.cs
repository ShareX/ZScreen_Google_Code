using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Plugins
{
    public interface IPluginInterface
    {
        List<IPluginItem> PluginItems { get; set; }

        string Name { get; }
        string Description { get; }
    }
}
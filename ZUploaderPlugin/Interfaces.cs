using System.Windows.Forms;

namespace ZUploaderPlugin
{
    public interface IPluginHost
    {
        void AddPluginButton(ToolStripMenuItem tsmi);
    }

    public interface IPlugin
    {
        IPluginHost Host { get; set; }

        string Name { get; }
        string Description { get; }
        string Author { get; }
        string Version { get; }

        void Init();
    }
}
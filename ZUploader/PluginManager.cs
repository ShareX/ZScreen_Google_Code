using ExtensionManager;
using ZUploaderPlugin;

namespace ZUploader
{
    public class PluginManager
    {
        public string PluginFolderPath { get; private set; }

        private ExtensionManager<IPlugin, IPluginHost> manager;
        private IPluginHost host;

        public PluginManager(string pluginFolderPath, IPluginHost pluginHost)
        {
            PluginFolderPath = pluginFolderPath;
            manager = new ExtensionManager<IPlugin, IPluginHost>();
            manager.LoadDefaultFileExtensions();
            manager.AssemblyFailedLoading += new ExtensionManager<IPlugin, IPluginHost>.AssemblyFailedLoadingEventHandler(manager_AssemblyFailedLoading);
            manager.AssemblyLoading += new ExtensionManager<IPlugin, IPluginHost>.AssemblyLoadingEventHandler(manager_AssemblyLoading);
            manager.AssemblyLoaded += new ExtensionManager<IPlugin, IPluginHost>.AssemblyLoadedEventHandler(manager_AssemblyLoaded);
            host = pluginHost;
        }

        private void manager_AssemblyFailedLoading(object sender, AssemblyFailedLoadingEventArgs e)
        {
            Program.MyLogger.WriteLine("Plugin load failed: {0}\r\n{1}", e.Filename, e.ErrorMessage);
        }

        private void manager_AssemblyLoading(object sender, AssemblyLoadingEventArgs e)
        {
            Program.MyLogger.WriteLine("Plugin loading: " + e.Filename);
        }

        private void manager_AssemblyLoaded(object sender, AssemblyLoadedEventArgs e)
        {
            Program.MyLogger.WriteLine("Plugin loaded:" + e.Filename);
        }

        public void LoadPlugins()
        {
            manager.LoadExtensions(PluginFolderPath);

            foreach (Extension<IPlugin> extension in manager.Extensions)
            {
                IPlugin plugin = extension.Instance;
                plugin.Host = host;
                plugin.Init();
            }
        }
    }
}
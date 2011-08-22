using System;
using System.IO;
using System.Reflection;
using ExtensionManager;
using ZUploaderPluginBase;

namespace ZUploader
{
    public class PluginManager
    {
        public string PluginFolderPath { get; private set; }

        private ExtensionManager<ZUploaderPlugin, IPluginHost> manager;
        private IPluginHost host;

        public PluginManager(string pluginFolderPath, IPluginHost pluginHost)
        {
            PluginFolderPath = pluginFolderPath;

            AppDomain currentDomain = AppDomain.CurrentDomain;
            currentDomain.AssemblyResolve += new ResolveEventHandler(currentDomain_AssemblyResolve);

            manager = new ExtensionManager<ZUploaderPlugin, IPluginHost>();
            manager.LoadDefaultFileExtensions();
            manager.AssemblyFailedLoading += new ExtensionManager<ZUploaderPlugin, IPluginHost>.AssemblyFailedLoadingEventHandler(manager_AssemblyFailedLoading);
            manager.AssemblyLoading += new ExtensionManager<ZUploaderPlugin, IPluginHost>.AssemblyLoadingEventHandler(manager_AssemblyLoading);
            manager.AssemblyLoaded += new ExtensionManager<ZUploaderPlugin, IPluginHost>.AssemblyLoadedEventHandler(manager_AssemblyLoaded);
            host = pluginHost;
        }

        private Assembly currentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
        {
            string assemblyPath = Path.Combine(PluginFolderPath, args.Name.Substring(0, args.Name.IndexOf(",")) + ".dll");
            return Assembly.LoadFrom(assemblyPath);
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

            foreach (Extension<ZUploaderPlugin> extension in manager.Extensions)
            {
                ZUploaderPlugin plugin = extension.Instance;
                plugin.Host = host;
                plugin.Initialize();
            }
        }

        public void UnloadPlugins()
        {
            foreach (Extension<ZUploaderPlugin> extension in manager.Extensions)
            {
                ZUploaderPlugin plugin = extension.Instance;
                plugin.DeInitialize();
            }

            manager.Extensions.Clear();
        }
    }
}
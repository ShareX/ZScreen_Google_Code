namespace ZUploaderPlugin
{
    public interface IPlugin
    {
        string Name { get; }
        string Description { get; }
        string Author { get; }
        string Version { get; }

        IPluginHost Host { get; set; }

        void Init();
    }
}
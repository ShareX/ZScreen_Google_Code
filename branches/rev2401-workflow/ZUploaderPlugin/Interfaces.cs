namespace ZUploaderPlugin
{
    public interface IPluginHost
    {
        void TestPlugin(string text);
    }

    public interface IPlugin
    {
        IPluginHost Host { get; set; }

        string Name { get; }
        string Description { get; }
        string Author { get; }
        string Version { get; }

        void Test();
    }
}
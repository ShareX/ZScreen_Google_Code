using System.Windows.Forms;
using ZUploaderPlugin;

namespace ZUploaderPluginTest
{
    public class TestPlugin : IPlugin
    {
        public IPluginHost Host { get; set; }

        public string Name
        {
            get { return "ZUploader Test Plugin"; }
        }

        public string Description
        {
            get { return "For testing."; }
        }

        public string Author
        {
            get { return "Jaex"; }
        }

        public string Version
        {
            get { return "1.0.0.0"; }
        }

        public void Init()
        {
            ToolStripMenuItem tsmi = new ToolStripMenuItem();
            tsmi.Text = "Plugin test button!";
            tsmi.Click += (sender, e) => MessageBox.Show("It is working!!!");
            Host.AddPluginButton(tsmi);
        }
    }
}
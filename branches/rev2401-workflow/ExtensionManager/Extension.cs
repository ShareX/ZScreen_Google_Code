using System;
using System.Reflection;

namespace ExtensionManager
{
    public class Extension<ClientInterface>
    {
        public Extension()
        {
        }

        public Extension(string filename, ExtensionType extensionType, ClientInterface instance)
        {
            this.extensionType = extensionType;
            this.instance = instance;
            this.filename = filename;
        }

        private ExtensionType extensionType = ExtensionType.Unknown;
        private string filename = "";
        private SourceFileLanguage language = SourceFileLanguage.Unknown;
        private ClientInterface instance = default(ClientInterface);
        private Assembly instanceAssembly = default(Assembly);

        public ExtensionType ExtensionType
        {
            get { return extensionType; }
            set { extensionType = value; }
        }

        public string Filename
        {
            get { return filename; }
            set { filename = value; }
        }

        public SourceFileLanguage Language
        {
            get { return language; }
            set { language = value; }
        }

        public ClientInterface Instance
        {
            get { return instance; }
            set { instance = value; }
        }

        public Assembly InstanceAssembly
        {
            get { return instanceAssembly; }
            set { instanceAssembly = value; }
        }

        public Type GetType(string name)
        {
            return instanceAssembly.GetType(name, false, true);
        }
    }
}
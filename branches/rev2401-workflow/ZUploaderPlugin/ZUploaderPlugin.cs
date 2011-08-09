using System;

namespace ZUploaderPluginBase
{
    public abstract class ZUploaderPlugin
    {
        public abstract string Name { get; }
        public virtual Version Version { get { return new Version(1, 0); } }
        public virtual string Author { get { return ""; } }
        public virtual string Description { get { return ""; } }

        public virtual IPluginHost Host { get; set; }

        public abstract void Initialize();

        public virtual void DeInitialize() { }
    }
}
using System;

namespace ExtensionManager
{
    public class AssemblyLoadedEventArgs : EventArgs
    {
        public AssemblyLoadedEventArgs()
        {
        }

        public AssemblyLoadedEventArgs(string filename)
        {
            this.filename = filename;
        }

        private string filename = "";

        public string Filename
        {
            get { return filename; }
            set { filename = value; }
        }
    }
}
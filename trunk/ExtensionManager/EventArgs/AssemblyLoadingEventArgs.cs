using System;

namespace ExtensionManager
{
    public class AssemblyLoadingEventArgs : EventArgs
    {
        public AssemblyLoadingEventArgs()
        {
        }

        public AssemblyLoadingEventArgs(string filename)
        {
            this.filename = filename;
        }

        private bool cancel = false;
        private string filename = "";

        public bool Cancel
        {
            get { return cancel; }
            set { cancel = value; }
        }

        public string Filename
        {
            get { return filename; }
            set { filename = value; }
        }
    }
}
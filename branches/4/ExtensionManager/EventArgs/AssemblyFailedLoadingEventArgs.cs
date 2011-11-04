using System;
using System.CodeDom.Compiler;

namespace ExtensionManager
{
    public class AssemblyFailedLoadingEventArgs : EventArgs
    {
        public AssemblyFailedLoadingEventArgs()
        {
        }

        public AssemblyFailedLoadingEventArgs(string filename)
        {
            this.filename = filename;
        }

        private ExtensionType extensionType = ExtensionType.Unknown;
        private CompilerErrorCollection sourceFileCompilerErrors = new CompilerErrorCollection();
        private string errorMessage = "";
        private string filename = "";

        public ExtensionType ExtensionType
        {
            get { return extensionType; }
            set { extensionType = value; }
        }

        public CompilerErrorCollection SourceFileCompilerErrors
        {
            get { return sourceFileCompilerErrors; }
            set { sourceFileCompilerErrors = value; }
        }

        public string ErrorMessage
        {
            get { return errorMessage; }
            set { errorMessage = value; }
        }

        public string Filename
        {
            get { return filename; }
            set { filename = value; }
        }
    }
}
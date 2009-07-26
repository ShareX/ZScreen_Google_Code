//Copyright (c) Microsoft Corporation.  All rights reserved.

using System;
using System.IO;

namespace Microsoft.WindowsAPICodePack.Shell
{
    /// <summary>
    /// A file in the Shell Namespace
    /// </summary>
    public class ShellFile : ShellEntity, IDisposable
    {
        #region Internal Constructor

        internal ShellFile(string path)
        {
            // Get the absolute path
            string absPath = ShellHelper.GetAbsolutePath(path);

            // Make sure this is valid
            if (!File.Exists(absPath))
                throw new FileNotFoundException(string.Format("The given path does not exist ({0})", path));

            base.ParsingName = absPath;
            this.Path = absPath;
        }

        internal ShellFile(IShellItem2 shellItem)
        {
            nativeShellItem = shellItem;
        }

        #endregion

        #region Public Methods
        /// <summary>
        /// Constructs a new ShellFile object given a file path
        /// </summary>
        /// <param name="path">The file or folder path</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.String.Format(System.String,System.Object)", Justification = "We are not currently handling globalization or localization")]
        static public ShellFile FromFilePath(string path)
        {
            return new ShellFile(path);
        }

        #endregion

        #region Protected Properties

        /// <summary>
        /// Path for this file e.g. c:\Windows\file.txt,
        /// </summary>
        private string internalPath = null;

        #endregion

        #region Public Properties

        /// <summary>
        /// The path for this file
        /// </summary>
        virtual public string Path
        {
            get
            {
                if (internalPath == null && NativeShellItem != null)
                {
                    internalPath = ShellHelper.GetParsingName(NativeShellItem);
                }
                return internalPath;
            }
            protected set
            {
                this.internalPath = value;
            }
        }

        #endregion
        
        #region IDisposable Members

        void IDisposable.Dispose()
        {
            internalPath = null;

            base.Dispose();
        }

        #endregion
    }
}

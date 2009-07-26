// Copyright (c) Microsoft Corporation.  All rights reserved.

using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace Microsoft.WindowsAPICodePack.Shell
{
    /// <summary>
    /// Represents a Non FileSystem folder (e.g. My Computer, Control Panel)
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1710:IdentifiersShouldHaveCorrectSuffix", Justification = "This will complicate the class hierarchy and naming convention used in the Shell area")]
    public class NonFileSystemFolder : ShellFolder
    {
        #region Internal Constructors

        internal NonFileSystemFolder()
        {

        }

        internal NonFileSystemFolder(IShellItem2 shellItem)
        {
            nativeShellItem = shellItem;
        }

        #endregion

    }
}

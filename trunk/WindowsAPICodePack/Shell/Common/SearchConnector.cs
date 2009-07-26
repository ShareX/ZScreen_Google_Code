//Copyright (c) Microsoft Corporation.  All rights reserved.

using System;
using System.Collections.Generic;

namespace Microsoft.WindowsAPICodePack.Shell
{
    /// <summary>
    /// A Serch Connector folder in the Shell Namespace
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1710:IdentifiersShouldHaveCorrectSuffix", Justification = "This will complicate the class hierarchy and naming convention used in the Shell area")]
    public sealed class SearchConnector : SearchContainer
    {

        #region Private Constructor

        internal SearchConnector()
        {

        }

        internal SearchConnector(IShellItem2 shellItem)
        {
            nativeShellItem = shellItem;
        }

        #endregion
    }
}

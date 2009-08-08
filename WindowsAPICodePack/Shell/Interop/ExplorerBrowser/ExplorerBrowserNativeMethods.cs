//Copyright (c) Microsoft Corporation.  All rights reserved.

using System;
using System.Collections.Generic;
using System.Text;
using System.Security;
using System.Runtime.InteropServices;
using MS.WindowsAPICodePack.Internal;

namespace Microsoft.WindowsAPICodePack.Controls
{
    /// <summary>
    /// Internal class that contains interop declarations for 
    /// functions that are not benign and are performance critical. 
    /// </summary>
    [SuppressUnmanagedCodeSecurity]
    internal static class ExplorerBrowserNativeMethods
    {
        [DllImport( "SHLWAPI.DLL", CharSet = CharSet.Unicode, SetLastError = true )]
        internal static extern HRESULT IUnknown_SetSite(
            [In, MarshalAs( UnmanagedType.IUnknown )] object punk,
            [In, MarshalAs( UnmanagedType.IUnknown )] object punkSite );
    }
}

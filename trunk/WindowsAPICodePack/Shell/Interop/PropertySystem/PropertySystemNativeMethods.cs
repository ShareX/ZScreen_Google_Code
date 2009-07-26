//Copyright (c) Microsoft Corporation.  All rights reserved.

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Microsoft.WindowsAPICodePack;
using System.Runtime.InteropServices.ComTypes;

namespace Microsoft.WindowsAPICodePack.Shell
{
    internal static class PropertySystemNativeMethods
    {
        static PropertySystemNativeMethods()
        {
            // Hide default constructor
        }

        #region Property Definitions

        internal enum PROPDESC_RELATIVEDESCRIPTION_TYPE
        {
            PDRDT_GENERAL,
            PDRDT_DATE,
            PDRDT_SIZE,
            PDRDT_COUNT,
            PDRDT_REVISION,
            PDRDT_LENGTH,
            PDRDT_DURATION,
            PDRDT_SPEED,
            PDRDT_RATE,
            PDRDT_RATING,
            PDRDT_PRIORITY
        }

        #endregion

        #region Property System Helpers

        [DllImport("propsys.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        internal static extern int PSGetNameFromPropertyKey(
            ref PropertyKey propkey,
            [Out, MarshalAs(UnmanagedType.LPWStr)] out string ppszCanonicalName
        );

        [DllImport("propsys.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        internal static extern HRESULT PSGetPropertyDescription(
            ref PropertyKey propkey,
            ref Guid riid,
            [Out, MarshalAs(UnmanagedType.Interface)] out IPropertyDescription ppv
        );

        [DllImport("propsys.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        internal static extern int PSGetPropertyKeyFromName(
            [In, MarshalAs(UnmanagedType.LPWStr)] string pszCanonicalName,
            out PropertyKey propkey
        );

        #endregion
    }
}

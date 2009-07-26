//Copyright (c) Microsoft Corporation.  All rights reserved.

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Microsoft.WindowsAPICodePack.Shell
{
    /// <summary>
    /// Represents the base class for all types of Shell "containers". Any class deriving from this class
    /// can contain other ShellObjects (e.g. ShellFolder, FileSystemKnownFolder, ShellLibrary, etc)
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1710:IdentifiersShouldHaveCorrectSuffix", Justification = "This will complicate the class hierarchy and naming convention used in the Shell area")]
    public abstract class ShellContainer : ShellObject, IEnumerable<ShellObject>, IDisposable
    {

        #region Private Fields

        private IShellFolder desktopFolderEnumeration;
        private IShellFolder desktopFolderBinding;
        private IShellFolder nativeShellFolder;

        #endregion

        #region Internal Properties

        internal IShellFolder NativeShellFolder
        {
            get
            {
                if (nativeShellFolder == null)
                {
                    IntPtr pidl = ShellHelper.PidlFromShellItem(NativeShellItem);                    
                    Guid guid = new Guid(ShellIIDGuid.IShellFolder);

                    if (desktopFolderBinding == null)
                        ShellNativeMethods.SHGetDesktopFolder(out desktopFolderBinding);

                    HRESULT hr = desktopFolderBinding.BindToObject(pidl, IntPtr.Zero, ref guid, out nativeShellFolder);
                    if (CoreErrorHelper.Failed(hr))
                    {
                        string str = ShellHelper.GetParsingName(NativeShellItem);
                        if (str != null)
                        {
                            if (str == Environment.GetFolderPath(Environment.SpecialFolder.Desktop))
                            {
                                return null;
                            }
                            else
                            {
                                throw Marshal.GetExceptionForHR((int) hr);
                            }
                        }
                    }
                }

                return nativeShellFolder;
            }
        }

        #endregion

        #region Internal Constructor

        internal ShellContainer()
        {

        }

        internal ShellContainer(IShellItem2 shellItem):base(shellItem)
        {
            
        }

        #endregion

        #region Disposable Pattern

        /// <summary>
        /// Release resources
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing)
        {
            if (nativeShellFolder != null)
            {
                Marshal.ReleaseComObject(nativeShellFolder);
                nativeShellFolder = null;
            }

            base.Dispose(disposing);
        }

        /// <summary>
        /// Release resources
        /// </summary>
        ~ShellContainer()
        {
            Dispose(false);
        }

        #endregion

        #region IEnumerable<ShellObject> Members

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerator<ShellObject> GetEnumerator()
        {
            if (NativeShellFolder == null)
            {
                if (desktopFolderEnumeration == null)
                {
                    ShellNativeMethods.SHGetDesktopFolder(out desktopFolderEnumeration);
                }

                nativeShellFolder = desktopFolderEnumeration;
            }

            return new ShellFolderItems(NativeShellFolder);
        }

        #endregion

        #region IEnumerable Members

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return new ShellFolderItems(NativeShellFolder);
        }

        #endregion
    }
}

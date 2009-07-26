//Copyright (c) Microsoft Corporation.  All rights reserved.

using System;
using System.IO;
using System.Runtime.InteropServices;

namespace Microsoft.WindowsAPICodePack.Shell
{
    /// <summary>
    /// The base class for all Shell objects in Shell Namespace
    /// </summary>
    abstract public class ShellObject : IDisposable
    {

        #region Public Static Methods

        /// <summary>
        /// Creates a ShellObject subclass given a parsing name.
        /// For file system items, this method will only accept absolute paths.
        /// </summary>
        /// <param name="parsingName"></param>
        /// <returns>A newly constructed ShellObject object</returns>
        public static ShellObject FromParsingName(string parsingName)
        {
            return ShellObjectFactory.Create(parsingName);
        }

        #endregion

        #region Internal Fields

        /// <summary>
        /// Internal member to keep track of the native IShellItem2
        /// </summary>
        internal IShellItem2 nativeShellItem;

        #endregion

        internal ShellObject()
        {
            properties = new ShellProperties(this);
        }

        internal ShellObject(IShellItem2 shellItem)
        {
            nativeShellItem = shellItem;
            properties = new ShellProperties(this);
        }

        #region Protected Fields

        /// <summary>
        /// Parsing name for this Object e.g. c:\Windows\file.txt,
        /// or ::{Some Guid} 
        /// </summary>
        private string internalParsingName = null;

        /// <summary>
        /// A friendly name for this object that' suitable for display
        /// </summary>
        private string internalName = null;

        /// <summary>
        /// PID List (PIDL) for this object
        /// </summary>
        private IntPtr internalPIDL = IntPtr.Zero;

        #endregion

        #region Internal Properties

        /// <summary>
        /// Return the native ShellFolder object as newer IShellItem2
        /// </summary>
        /// <exception cref="System.Runtime.InteropServices.ExternalException">If the native object cannot be created.
        /// The ErrorCode member will contain the external error code.</exception>
        virtual internal IShellItem2 NativeShellItem2
        {
            get
            {
                if (nativeShellItem == null && ParsingName != null)
                {
                    Guid guid = new Guid(ShellIIDGuid.IShellItem2);
                    int retCode = ShellNativeMethods.SHCreateItemFromParsingName(ParsingName, IntPtr.Zero, ref guid, out nativeShellItem);

                    if (nativeShellItem == null || !CoreErrorHelper.Succeeded(retCode))
                    {
                        throw new ExternalException("Shell item could not be created.", Marshal.GetExceptionForHR(retCode));
                    }
                }
                return nativeShellItem;
            }
        }

        /// <summary>
        /// Return the native ShellFolder object
        /// </summary>
        virtual internal IShellItem NativeShellItem
        {
            get
            {
                return NativeShellItem2;
            }
        }

        /// <summary>
        /// Gets access to the native IPropertyStore (if one is already
        /// created for this item and still valid. This is usually done by the
        /// ShellPropertyWriter class. The reference will be set to null
        /// when the writer has been closed/commited).
        /// </summary>
        internal IPropertyStore NativePropertyStore
        {
            get;
            set;
        }

        #endregion

        #region Public Properties

        private ShellProperties properties = null;
        /// <summary>
        /// This object allows the manipulation of Shell properties of this Shell Item
        /// </summary>
        public ShellProperties Properties
        {
            get
            {
                return properties;
            }
        }

        /// <summary>
        /// The parsing name for this Shell Item
        /// </summary>
        virtual public string ParsingName
        {
            get
            {
                if (internalParsingName == null && nativeShellItem != null)
                {
                    internalParsingName = ShellHelper.GetParsingName(nativeShellItem);
                }
                return internalParsingName;
            }
            protected set
            {
                this.internalParsingName = value;
            }
        }

        /// <summary>
        /// The Normal Display for this Shell Item
        /// </summary>
        virtual public string Name
        {
            get
            {
                if (internalName == null && NativeShellItem != null)
                {
                    IntPtr pszString = IntPtr.Zero;
                    HRESULT hr = NativeShellItem.GetDisplayName(ShellNativeMethods.SIGDN.SIGDN_NORMALDISPLAY, out pszString);
                    if (hr == HRESULT.S_OK && pszString != IntPtr.Zero)
                    {
                        internalName = Marshal.PtrToStringAuto(pszString);
                    }
                }
                return internalName;
            }

            protected set
            {
                this.internalName = value;
            }
        }

        /// <summary>
        /// The PID List (PIDL) for this ShellItem
        /// </summary>
        virtual internal IntPtr PIDL
        {
            get
            {
                // Get teh PIDL for the ShellItem
                if (internalPIDL == IntPtr.Zero && NativeShellItem != null)
                    internalPIDL = ShellHelper.PidlFromShellItem(nativeShellItem);

                return internalPIDL;
            }
            set
            {
                this.internalPIDL = value;
            }
        }

        /// <summary>
        /// Override object.ToString()
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return this.Name;
        }

        /// <summary>
        /// Gets the display name of the ShellFolder object. DisplayNameType represents one of the 
        /// values that indicates how the name should look. 
        /// See <see cref="Microsoft.WindowsAPICodePack.Shell.DisplayNameType"/>for a list of possible values.
        /// </summary>
        /// <param name="displayNameType"></param>
        /// <returns></returns>
        virtual public string GetDisplayName(DisplayNameType displayNameType)
        {
            string returnValue = null;

            HRESULT hr = HRESULT.S_OK;

            if (NativeShellItem2 != null)
                hr = NativeShellItem2.GetDisplayName((ShellNativeMethods.SIGDN)displayNameType, out returnValue);

            if (hr != HRESULT.S_OK)
                throw new COMException("Can't get the display name", (int)hr);

            return returnValue;
        }

        /// <summary>
        /// Checks if this Shell Object is a link or shortcut
        /// </summary>
        public bool IsLink
        {
            get
            {
                try
                {
                    ShellNativeMethods.SFGAO sfgao;
                    NativeShellItem.GetAttributes(ShellNativeMethods.SFGAO.SFGAO_LINK, out sfgao);
                    return (sfgao & ShellNativeMethods.SFGAO.SFGAO_LINK) != 0;
                }
                catch (FileNotFoundException)
                {
                    return false;
                }
                catch (NullReferenceException)
                {
                    // NativeShellItem is null
                    return false;
                }
            }
        }

        /// <summary>
        /// Checks if this Shell Object is a link or shortcut
        /// </summary>
        public bool IsFileSystemObject
        {
            get
            {
                try
                {
                    ShellNativeMethods.SFGAO sfgao;
                    NativeShellItem.GetAttributes(ShellNativeMethods.SFGAO.SFGAO_FILESYSTEM, out sfgao);
                    return (sfgao & ShellNativeMethods.SFGAO.SFGAO_FILESYSTEM) != 0;
                }
                catch (FileNotFoundException)
                {
                    return false;
                }
                catch (NullReferenceException)
                {
                    // NativeShellItem is null
                    return false;
                }
            }
        }

        private ShellThumbnail thumbnail;
        /// <summary>
        /// Gets the thumbnail of the ShellObject.
        /// </summary>
        public ShellThumbnail Thumbnail
        {
            get
            {
                if (thumbnail == null)
                    thumbnail = new ShellThumbnail(this);

                return thumbnail;
            }
        }

        private ShellObject parentShellObject = null;
        /// <summary>
        /// Gets the parent ShellObject
        /// </summary>
        public ShellObject Parent
        {
            get
            {
                if (parentShellObject == null && nativeShellItem != null)
                {
                    IShellItem parentShellItem = null;

                    HRESULT hr = nativeShellItem.GetParent(out parentShellItem);

                    if (hr == HRESULT.S_OK && parentShellItem != null)
                        parentShellObject = ShellObjectFactory.Create(parentShellItem);
                    else
                        throw Marshal.GetExceptionForHR((int)hr);
                }

                return parentShellObject;
            }
        }


        #endregion

        #region IDisposable Members

        /// <summary>
        /// Release the native and managed objects
        /// </summary>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                internalName = null;
                internalParsingName = null;
                properties = null;
                thumbnail = null;
            }


            if (internalPIDL != IntPtr.Zero)
            {
                Marshal.Release(internalPIDL);
                internalPIDL = IntPtr.Zero;
            }

            if (nativeShellItem != null)
            {
                Marshal.ReleaseComObject(nativeShellItem);
                nativeShellItem = null;
            }

            if (NativePropertyStore != null)
            {
                Marshal.ReleaseComObject(NativePropertyStore);
                NativePropertyStore = null;
            }

        }

        /// <summary>
        /// Release the native objects
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// 
        /// </summary>
        ~ShellObject()
        {
            Dispose(false);
        }

        #endregion
    }
}

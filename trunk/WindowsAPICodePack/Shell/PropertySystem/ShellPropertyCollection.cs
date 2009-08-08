//Copyright (c) Microsoft Corporation.  All rights reserved.

using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using Microsoft.WindowsAPICodePack.Shell;
using MS.WindowsAPICodePack.Internal;

namespace Microsoft.WindowsAPICodePack.Shell.PropertySystem
{
    /// <summary>
    /// Creates a readonly collection of IProperty objects.
    /// </summary>
    public class ShellPropertyCollection : ReadOnlyCollection<IShellProperty>
    {
        #region Internal Constructor

        /// <summary>
        /// Creates a new Property collection given an IShellItem2 native interface
        /// </summary>
        /// <param name="parent">Parent ShellObject</param>
        internal ShellPropertyCollection(ShellObject parent)
            : base(new List<IShellProperty>())
        {
            ParentShellObject = parent;
            IPropertyStore nativePropertyStore = null;
            try
            {
                nativePropertyStore = CreateDefaultPropertyStore(ParentShellObject);
                AddProperties(nativePropertyStore);
            }
            finally
            {
                if (nativePropertyStore != null)
                {
                    Marshal.ReleaseComObject(nativePropertyStore);
                    nativePropertyStore = null;
                }
            }
        }

        #endregion

        #region Public Constructor

        /// <summary>
        /// Creates a new <c>ShellPropertyCollection</c> object with the specified file or folder path.
        /// </summary>
        /// <param name="path">The path to the file or folder.</param>
        public ShellPropertyCollection(string path) :
            this(ShellObjectFactory.Create(path))
        {
        }

        #endregion

        #region Private Methods

        private ShellObject ParentShellObject { get; set; }

        private void AddProperties(IPropertyStore nativePropertyStore)
        {
            uint propertyCount;
            PropertyKey propKey;

            // Populate the property collection
            nativePropertyStore.GetCount(out propertyCount);
            for (uint i = 0; i < propertyCount; i++)
            {
                nativePropertyStore.GetAt(i, out propKey);
                Items.Add(ParentShellObject.Properties.CreateTypedProperty(propKey));
            }
        }

        internal static IPropertyStore CreateDefaultPropertyStore(ShellObject shellObj)
        {
            IPropertyStore nativePropertyStore = null;

            Guid guid = new Guid(ShellIIDGuid.IPropertyStore);
            int hr = shellObj.NativeShellItem2.GetPropertyStore(
                   ShellNativeMethods.GETPROPERTYSTOREFLAGS.GPS_BESTEFFORT,
                   ref guid,
                   out nativePropertyStore);

            // throw on failure 
            if (nativePropertyStore == null || !CoreErrorHelper.Succeeded(hr))
            {
                Marshal.ThrowExceptionForHR(hr);
            }

            return nativePropertyStore;
        }




        #endregion

        #region Collection Public Methods

        /// <summary>
        /// Checks if a property with the given canonical name is available.
        /// </summary>
        /// <param name="canonicalName">The canonical name of the property.</param>
        /// <returns><B>True</B> if available, <B>false</B> otherwise.</returns>
        public bool Contains(string canonicalName)
        {
            if (string.IsNullOrEmpty(canonicalName))
            {
                throw new ArgumentException("Argument CanonicalName cannot be null or empty.", "canonicalName");
            }

            return Items.
                Where(p => p.CanonicalName == canonicalName).
                Count() > 0;

        }

        /// <summary>
        /// Checks if a property with the given property key is available.
        /// </summary>
        /// <param name="key">The property key.</param>
        /// <returns><B>True</B> if available, <B>false</B> otherwise.</returns>
        public bool Contains(PropertyKey key)
        {
            return Items.
                Where(p => p.PropertyKey == key).
                Count() > 0;
        }

        /// <summary>
        /// Gets the property associated with the supplied canonical name string.
        /// The canonical name property is case-sensitive.
        /// 
        /// </summary>
        /// <param name="canonicalName">The canonical name.</param>
        /// <returns>The property associated with the canonical name, if found.</returns>
        /// <exception cref="IndexOutOfRangeException">Throws IndexOutOfRangeException 
        /// if no matching property is found.</exception>
        public IShellProperty this[string canonicalName]
        {
            get
            {
                if (string.IsNullOrEmpty(canonicalName))
                {
                    throw new ArgumentException("Argument CanonicalName cannot be null or empty.", "canonicalName");
                }

                IShellProperty[] props = Items
                    .Where(p => p.CanonicalName == canonicalName).ToArray();

                if (props != null && props.Length > 0)
                    return props[0];

                throw new IndexOutOfRangeException("This CanonicalName is not a valid index.");
            }
        }

        /// <summary>
        /// Gets a property associated with the supplied property key.
        /// 
        /// </summary>
        /// <param name="key">The property key.</param>
        /// <returns>The property associated with the property key, if found.</returns>
        /// <exception cref="IndexOutOfRangeException">Throws IndexOutOfRangeException 
        /// if no matching property is found.</exception>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1043:UseIntegralOrStringArgumentForIndexers", Justification="We need the ability to get item from the collection using a property key")]
        public IShellProperty this[PropertyKey key]
        {
            get
            {
                IShellProperty[] props =
                    Items
                    .Where(p => p.PropertyKey == key)
                    .ToArray();

                if (props != null && props.Length > 0)
                    return props[0];

                throw new IndexOutOfRangeException("This PropertyKey is not a valid index.");
            }
        }

        #endregion

    }
}

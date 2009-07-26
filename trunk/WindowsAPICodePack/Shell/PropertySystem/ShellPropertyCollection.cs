//Copyright (c) Microsoft Corporation.  All rights reserved.

using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;

namespace Microsoft.WindowsAPICodePack.Shell
{
    /// <summary>
    /// A readonly collection of IProperty objects
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
        /// Creates a new Property Collection given a file or folder path
        /// </summary>
        /// <param name="path">The path to the file or folder</param>
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
                   ShellNativeMethods.GETPROPERTYSTOREFLAGS.GPS_DEFAULT,
                   ref guid,
                   out nativePropertyStore);

            // if we fail, try one more time with a more powerful flag!
            // This time, we won't catch any exceptions
            if (nativePropertyStore == null || !CoreErrorHelper.Succeeded(hr))
            {
                hr = shellObj.NativeShellItem2.GetPropertyStore(
                   ShellNativeMethods.GETPROPERTYSTOREFLAGS.GPS_BESTEFFORT,
                   ref guid,
                   out nativePropertyStore);

                // This time actually throw
                if (!CoreErrorHelper.Succeeded(hr))
                {
                    throw new ExternalException("Unable to obtain property store", Marshal.GetExceptionForHR(hr));
                }
            }

            return nativePropertyStore;
        }




        #endregion

        #region Collection Public Methods

        /// <summary>
        /// Checks if a property with the given Canaonical Name is available
        /// </summary>
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
        /// Checks if a property with the given Property Key is available
        /// </summary>
        public bool Contains(PropertyKey key)
        {
            return Items.
                Where(p => p.PropertyKey.FormatId == key.FormatId && p.PropertyKey.PropertyId == key.PropertyId).
                Count() > 0;
        }

        /// <summary>
        /// Returns the Property with the supplied canonicalName string
        /// Property Canonical name is Case Sensitive.
        /// Also gets a property that might not be in the current collection
        /// </summary>
        /// <param name="canonicalName">The canonical name</param>
        /// <returns>The Property having the canonical name, if found</returns>
        /// <exception cref="IndexOutOfRangeException">Throws IndexOutOfRangeException 
        /// if no matching Property is found</exception>
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
        /// Returns a Property that have the supplied PropertyKey
        /// Also gets a property that might not be in the current collection
        /// </summary>
        /// <param name="key">The property key</param>
        /// <returns>The Property having the PropertyKey, if found</returns>
        /// <exception cref="IndexOutOfRangeException">Throws IndexOutOfRangeException 
        /// if no matching Property is found</exception>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1043:UseIntegralOrStringArgumentForIndexers", Justification="We need the ability to get item from the collection using a property key")]
        public IShellProperty this[PropertyKey key]
        {
            get
            {
                IShellProperty[] props =
                    Items
                    .Where(p => p.PropertyKey.PropertyId == key.PropertyId && p.PropertyKey.FormatId == key.FormatId)
                    .ToArray();

                if (props != null && props.Length > 0)
                    return props[0];

                throw new IndexOutOfRangeException("This PropertyKey is not a valid index.");
            }
        }

        #endregion

    }
}

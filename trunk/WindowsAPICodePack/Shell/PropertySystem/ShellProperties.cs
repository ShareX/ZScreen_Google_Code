//Copyright (c) Microsoft Corporation.  All rights reserved.

using System.Runtime.InteropServices;
using System;
using System.Runtime.InteropServices.ComTypes;

namespace Microsoft.WindowsAPICodePack.Shell
{
    /// <summary>
    /// Partial class that implements helper methods for retrieving Shell properties
    /// using a canonical name, property key, or a strongly typed property. Also provides
    /// access to all the strongly typed system properties and default properties collection.
    /// </summary>
    public partial class ShellProperties
    {
        private ShellObject ParentShellObject { get; set; }
        private ShellPropertyCollection defaultPropertyCollection = null;

        internal ShellProperties(ShellObject parent)
        {
            ParentShellObject = parent;
        }

        /// <summary>
        /// Get a property that's available in the default property collection using 
        /// the given property key
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public IShellProperty GetProperty(PropertyKey key)
        {
            return CreateTypedProperty(key);
        }

        /// <summary>
        /// Get a property that's available in the default property collection using 
        /// the given canonical name
        /// </summary>
        /// <param name="canonicalName"></param>
        /// <returns></returns>
        public IShellProperty GetProperty(string canonicalName)
        {
            return CreateTypedProperty(canonicalName);
        }

        /// <summary>
        /// Get a strongly typed property that's available in the default property collection using 
        /// the given property key
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public ShellProperty<T> GetProperty<T>(PropertyKey key)
        {
            return CreateTypedProperty(key) as ShellProperty<T>;
        }

        /// <summary>
        /// Get a strongly typed property that's available in the default property collection using 
        /// the given canonical name
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="canonicalName"></param>
        /// <returns></returns>
        public ShellProperty<T> GetProperty<T>(string canonicalName)
        {
            return CreateTypedProperty(canonicalName) as ShellProperty<T>;
        }

        private PropertySystem propertySystem = null;
        /// <summary>
        /// Gets the accessor for all the system properties
        /// </summary>
        public PropertySystem System
        {
            get
            {
                if (propertySystem == null)
                    propertySystem = new PropertySystem(ParentShellObject);

                return propertySystem;
            }
        }

        /// <summary>
        /// Collection of all the default properties for this item.
        /// </summary>
        public ShellPropertyCollection DefaultPropertyCollection
        {
            get
            {
                if (defaultPropertyCollection == null)
                    defaultPropertyCollection = new ShellPropertyCollection(ParentShellObject);

                return defaultPropertyCollection;
            }
        }

        /// <summary>
        /// Returns the property writer for writing multiple properties.
        /// </summary>
        /// <returns></returns>
        /// <remarks>Use the Using pattern with the returned ShellPropertyWriter or
        /// manually call the Close method on the writer to commit the changes 
        /// and dispose the writer</remarks>
        public ShellPropertyWriter GetPropertyWriter()
        {
            return new ShellPropertyWriter(ParentShellObject);

        }

        internal IShellProperty CreateTypedProperty<T>(PropertyKey propKey)
        {
            ShellPropertyDescription desc = ShellPropertyDescriptionsCache.Cache.GetPropertyDescription(propKey);
            return (new ShellProperty<T>(propKey, desc, ParentShellObject));
        }

        internal IShellProperty CreateTypedProperty(PropertyKey propKey)
        {
            ShellPropertyDescription desc = ShellPropertyDescriptionsCache.Cache.GetPropertyDescription(propKey);

            switch (desc.VarEnumType)
            {
                case (VarEnum.VT_EMPTY):
                case (VarEnum.VT_NULL):
                    {
                        return (new ShellProperty<Object>(propKey, desc, ParentShellObject));
                    }
                case (VarEnum.VT_UI1):
                    {
                        return (new ShellProperty<Byte?>(propKey, desc, ParentShellObject));
                    }
                case (VarEnum.VT_I2):
                    {
                        return (new ShellProperty<Int16?>(propKey, desc, ParentShellObject));
                    }
                case (VarEnum.VT_UI2):
                    {
                        return (new ShellProperty<UInt16?>(propKey, desc, ParentShellObject));
                    }
                case (VarEnum.VT_I4):
                    {
                        return (new ShellProperty<Int32?>(propKey, desc, ParentShellObject));
                    }
                case (VarEnum.VT_UI4):
                    {
                        return (new ShellProperty<UInt32?>(propKey, desc, ParentShellObject));
                    }
                case (VarEnum.VT_I8):
                    {
                        return (new ShellProperty<Int64?>(propKey, desc, ParentShellObject));
                    }
                case (VarEnum.VT_UI8):
                    {
                        return (new ShellProperty<UInt64?>(propKey, desc, ParentShellObject));
                    }
                case (VarEnum.VT_R8):
                    {
                        return (new ShellProperty<Double?>(propKey, desc, ParentShellObject));
                    }
                case (VarEnum.VT_BOOL):
                    {
                        return (new ShellProperty<Boolean?>(propKey, desc, ParentShellObject));
                    }
                case (VarEnum.VT_FILETIME):
                    {
                        return (new ShellProperty<DateTime?>(propKey, desc, ParentShellObject));
                    }
                case (VarEnum.VT_CLSID):
                    {
                        return (new ShellProperty<IntPtr?>(propKey, desc, ParentShellObject));
                    }
                case (VarEnum.VT_CF):
                    {
                        return (new ShellProperty<IntPtr?>(propKey, desc, ParentShellObject));
                    }
                case (VarEnum.VT_BLOB):
                    {
                        return (new ShellProperty<Byte[]>(propKey, desc, ParentShellObject));
                    }
                case (VarEnum.VT_LPWSTR):
                    {
                        return (new ShellProperty<String>(propKey, desc, ParentShellObject));
                    }
                case (VarEnum.VT_UNKNOWN):
                    {
                        return (new ShellProperty<IntPtr?>(propKey, desc, ParentShellObject));
                    }
                case (VarEnum.VT_STREAM):
                    {
                        return (new ShellProperty<IStream>(propKey, desc, ParentShellObject));
                    }
                case (VarEnum.VT_VECTOR | VarEnum.VT_UI1):
                    {
                        return (new ShellProperty<Byte[]>(propKey, desc, ParentShellObject));
                    }
                case (VarEnum.VT_VECTOR | VarEnum.VT_I2):
                    {
                        return (new ShellProperty<Int16[]>(propKey, desc, ParentShellObject));
                    }
                case (VarEnum.VT_VECTOR | VarEnum.VT_UI2):
                    {
                        return (new ShellProperty<UInt16[]>(propKey, desc, ParentShellObject));
                    }
                case (VarEnum.VT_VECTOR | VarEnum.VT_I4):
                    {
                        return (new ShellProperty<Int32[]>(propKey, desc, ParentShellObject));
                    }
                case (VarEnum.VT_VECTOR | VarEnum.VT_UI4):
                    {
                        return (new ShellProperty<UInt32[]>(propKey, desc, ParentShellObject));
                    }
                case (VarEnum.VT_VECTOR | VarEnum.VT_I8):
                    {
                        return (new ShellProperty<Int64[]>(propKey, desc, ParentShellObject));
                    }
                case (VarEnum.VT_VECTOR | VarEnum.VT_UI8):
                    {
                        return (new ShellProperty<UInt64[]>(propKey, desc, ParentShellObject));
                    }
                case (VarEnum.VT_VECTOR | VarEnum.VT_R8):
                    {
                        return (new ShellProperty<Double[]>(propKey, desc, ParentShellObject));
                    }
                case (VarEnum.VT_VECTOR | VarEnum.VT_BOOL):
                    {
                        return (new ShellProperty<Boolean[]>(propKey, desc, ParentShellObject));
                    }
                case (VarEnum.VT_VECTOR | VarEnum.VT_FILETIME):
                    {
                        return (new ShellProperty<DateTime[]>(propKey, desc, ParentShellObject));
                    }
                case (VarEnum.VT_VECTOR | VarEnum.VT_CLSID):
                    {
                        return (new ShellProperty<IntPtr[]>(propKey, desc, ParentShellObject));
                    }
                case (VarEnum.VT_VECTOR | VarEnum.VT_CF):
                    {
                        return (new ShellProperty<IntPtr[]>(propKey, desc, ParentShellObject));
                    }
                case (VarEnum.VT_VECTOR | VarEnum.VT_LPWSTR):
                    {
                        return (new ShellProperty<String[]>(propKey, desc, ParentShellObject));
                    }
                default:
                    {
                        return (new ShellProperty<Object>(propKey, desc, ParentShellObject));
                    }
            }
        }

        internal IShellProperty CreateTypedProperty(string canonicalName)
        {
            // Otherwise, call the native PropertyStore method
            PropertyKey propKey;

            int result = PropertySystemNativeMethods.PSGetPropertyKeyFromName(canonicalName, out propKey);

            if (!CoreErrorHelper.Succeeded(result))
            {
                throw new ArgumentException(
                    "This CanonicalName is not valid",
                    Marshal.GetExceptionForHR(result));
            }
            else
            {
                return CreateTypedProperty(propKey);
            }
        }
    }
}
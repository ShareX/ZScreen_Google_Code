//Copyright (c) Microsoft Corporation.  All rights reserved.

using System;
using System.Runtime.InteropServices;

namespace Microsoft.WindowsAPICodePack.Shell
{
    /// <summary>
    /// Writer that supports setting multiple properties for a given ShellObject.
    /// </summary>
    public class ShellPropertyWriter : IDisposable
    {
        
        private ShellObject parentShellObject = null;

        // Reference to our writable PropertyStore
        internal IPropertyStore writablePropStore = null;

        /// <summary>
        /// The value was set but truncated in a string value or rounded if a numeric value.
        /// </summary>
        protected const int InPlaceStringTruncated = 0x00401A0;

        internal ShellPropertyWriter(ShellObject parent)
        {
            ParentShellObject = parent;

            // Open the property store for this shell object...
            Guid guid = new Guid(ShellIIDGuid.IPropertyStore);

            try
            {
                int hr = ParentShellObject.NativeShellItem2.GetPropertyStore(
                        ShellNativeMethods.GETPROPERTYSTOREFLAGS.GPS_READWRITE,
                        ref guid,
                        out writablePropStore);

                if (!CoreErrorHelper.Succeeded(hr))
                {
                    throw new ExternalException("Unable to get writable property store for this property.",
                        Marshal.GetExceptionForHR(hr));
                }
                else
                {
                    // If we succeed in creating a valid property store for this ShellObject,
                    // then set it on the parent shell object for others to use.
                    // Once this writer is closed/commited, we will set the 
                    if (ParentShellObject.NativePropertyStore == null)
                        ParentShellObject.NativePropertyStore = writablePropStore;
                }

            }
            catch (InvalidComObjectException e)
            {
                throw new ExternalException("Unable to get writable property store for this property.", e);
            }
            catch (InvalidCastException)
            {
                throw new ExternalException("Unable to get writable property store for this property.");
            }
        }

        /// <summary>
        /// Reference to parent ShellObject (associated with this writer)
        /// </summary>
        protected ShellObject ParentShellObject
        {
            get { return parentShellObject; }
            private set { parentShellObject = value; }
        }

        #region Private Methods

        /// <summary>
        /// Set the property value. 
        /// At this point we're sure every 
        /// property has a value, or for non Nullable types, it's not null
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private PropVariant SetValue(object value)
        {
            PropVariant propVar = new PropVariant();

            if (value == null)
            {
                propVar.Clear();
                return propVar;
            }

            if (value.GetType() == typeof(string))
            {
                //Strings require special consideration, because they cannot be empty as well
                if (String.IsNullOrEmpty(value as string) || String.IsNullOrEmpty((value as string).Trim()))
                    throw new ArgumentException("String argument cannot be null or empty.");
                propVar.SetString(value as string);
            }
            else if (value.GetType() == typeof(bool?))
            {
                propVar.SetBool((value as bool?).Value);
            }
            else if (value.GetType() == typeof(bool))
            {
                propVar.SetBool((bool)value);
            }
            else if (value.GetType() == typeof(byte?))
            {
                propVar.SetByte((value as byte?).Value);
            }
            else if (value.GetType() == typeof(byte))
            {
                propVar.SetByte((byte)value);
            }
            else if (value.GetType() == typeof(sbyte?))
            {
                propVar.SetSByte((value as sbyte?).Value);
            }
            else if (value.GetType() == typeof(sbyte))
            {
                propVar.SetSByte((sbyte)value);
            }
            else if (value.GetType() == typeof(short?))
            {
                propVar.SetShort((value as short?).Value);
            }
            else if (value.GetType() == typeof(short))
            {
                propVar.SetShort((short)value);
            }
            else if (value.GetType() == typeof(ushort?))
            {
                propVar.SetUShort((value as ushort?).Value);
            }
            else if (value.GetType() == typeof(ushort))
            {
                propVar.SetUShort((ushort)value);
            }
            else if (value.GetType() == typeof(int?))
            {
                propVar.SetInt((value as int?).Value);
            }
            else if (value.GetType() == typeof(int))
            {
                propVar.SetInt((int)value);
            }
            else if (value.GetType() == typeof(uint?))
            {
                propVar.SetUInt((value as uint?).Value);
            }
            else if (value.GetType() == typeof(uint))
            {
                propVar.SetUInt((uint)value);
            }
            else if (value.GetType() == typeof(long?))
            {
                propVar.SetLong((value as long?).Value);
            }
            else if (value.GetType() == typeof(long))
            {
                propVar.SetLong((long)value);
            }
            else if (value.GetType() == typeof(ulong?))
            {
                propVar.SetULong((value as ulong?).Value);
            }
            else if (value.GetType() == typeof(ulong))
            {
                propVar.SetULong((ulong)value);
            }
            else if (value.GetType() == typeof(double?))
            {
                propVar.SetDouble((value as double?).Value);
            }
            else if (value.GetType() == typeof(double))
            {
                propVar.SetDouble((double)value);
            }
            else if (value.GetType() == typeof(DateTime?))
            {
                propVar.SetDateTime((value as DateTime?).Value);
            }
            else if (value.GetType() == typeof(DateTime))
            {
                propVar.SetDateTime((DateTime)value);
            }
            else if (value.GetType() == typeof(string[]))
            {
                propVar.SetStringVector((value as string[]));
            }
            else if (value.GetType() == typeof(short[]))
            {
                propVar.SetShortVector((value as short[]));
            }
            else if (value.GetType() == typeof(ushort[]))
            {
                propVar.SetUShortVector((value as ushort[]));
            }
            else if (value.GetType() == typeof(int[]))
            {
                propVar.SetIntVector((value as int[]));
            }
            else if (value.GetType() == typeof(uint[]))
            {
                propVar.SetUIntVector((value as uint[]));
            }
            else if (value.GetType() == typeof(long[]))
            {
                propVar.SetLongVector((value as long[]));
            }
            else if (value.GetType() == typeof(ulong[]))
            {
                propVar.SetULongVector((value as ulong[]));
            }
            else if (value.GetType() == typeof(DateTime[]))
            {
                propVar.SetDateTimeVector((value as DateTime[]));
            }
            else if (value.GetType() == typeof(bool[]))
            {
                propVar.SetBoolVector((value as bool[]));
            }
            else
            {
                //Should not happen!
                throw new NotSupportedException("This Value type is not supported");
            }

            return propVar;
        }

        #endregion

        /// <summary>
        /// Writes the given property (using a PropertyKey and value)
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void WriteProperty(PropertyKey key, object value)
        {
            WriteProperty(key, value, true);
        }

        /// <summary>
        /// Writes the given property (using a PropertyKey and value). Set allowTruncatedValue
        /// parameter to true if the property allow truncation of the given value. Default is true.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="allowTruncatedValue"></param>
        /// <exception cref="System.InvalidOperationException">If the writable property store is already 
        /// closed.</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">If AllowTruncatedValue is set to false 
        /// and while setting the value on the property it had to be truncated in a string or rounded in 
        /// a numeric value.</exception>
        public void WriteProperty(PropertyKey key, object value, bool allowTruncatedValue)
        {
            if (writablePropStore == null)
                throw new InvalidOperationException("Writeable store has been closed.");

            PropVariant propVar = SetValue(value);
            int result = writablePropStore.SetValue(ref key, ref propVar);

            if (!allowTruncatedValue && (result == InPlaceStringTruncated))
            {
                // At this point we can't revert back the commit
                // so don't commit, close the property store and throw an exception
                // to let the user know.
                Marshal.ReleaseComObject(writablePropStore);
                writablePropStore = null;

                throw new ArgumentOutOfRangeException("value", "A value had to be truncated in a string or rounded if a numeric value. Set AllowTruncatedValue to true to prevent this exception.");
            }

            if (!CoreErrorHelper.Succeeded(result))
            {
                throw new ExternalException("Unable to set property.", Marshal.GetExceptionForHR(result));
            }
        }

        /// <summary>
        /// Writes the given property (using a canonical name and value)
        /// </summary>
        /// <param name="canonicalName"></param>
        /// <param name="value"></param>
        public void WriteProperty(string canonicalName, object value)
        {
            WriteProperty(canonicalName, value, true);
        }

        /// <summary>
        /// Writes the given property (using a canonical name and value). Set allowTruncatedValue
        /// parameter to true if the property allow truncation of the given value. Default is true.
        /// </summary>
        /// <param name="canonicalName"></param>
        /// <param name="value"></param>
        /// <param name="allowTruncatedValue"></param>
        /// <exception cref="System.ArgumentException">If the given canonical name is not valid</exception>
        public void WriteProperty(string canonicalName, object value, bool allowTruncatedValue)
        {
            // Get the PropertyKey using the canonicalName passed in
            PropertyKey propKey;

            int result = PropertySystemNativeMethods.PSGetPropertyKeyFromName(canonicalName, out propKey);

            if (!CoreErrorHelper.Succeeded(result))
            {
                throw new ArgumentException(
                    "The given CanonicalName is not valid.",
                    Marshal.GetExceptionForHR(result));
            }

            WriteProperty(propKey, value, allowTruncatedValue);
        }

        /// <summary>
        /// Writes the given property (using a IShellProperty and value)
        /// </summary>
        /// <param name="shellProperty"></param>
        /// <param name="value"></param>
        public void WriteProperty(IShellProperty shellProperty, object value)
        {
            WriteProperty(shellProperty, value, true);
        }

        /// <summary>
        /// Writes the given property (using a IShellProperty and value). Set allowTruncatedValue
        /// parameter to true if the property allow truncation of the given value. Default is true.
        /// </summary>
        /// <param name="shellProperty"></param>
        /// <param name="value"></param>
        /// <param name="allowTruncatedValue"></param>
        public void WriteProperty(IShellProperty shellProperty, object value, bool allowTruncatedValue)
        {
            WriteProperty(shellProperty.PropertyKey, value, allowTruncatedValue);
        }

        /// <summary>
        /// Writes the given property (using a strongly typed ShellProperty and value)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="shellProperty"></param>
        /// <param name="value"></param>
        public void WriteProperty<T>(ShellProperty<T> shellProperty, T value)
        {
            WriteProperty<T>(shellProperty, value, true);
        }
        /// <summary>
        /// Writes the given property (using a strongly typed ShellProperty and value). Set allowTruncatedValue
        /// parameter to true if the property allow truncation of the given value. Default is true.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="shellProperty"></param>
        /// <param name="value"></param>
        /// <param name="allowTruncatedValue"></param>
        public void WriteProperty<T>(ShellProperty<T> shellProperty, T value, bool allowTruncatedValue)
        {
            WriteProperty(shellProperty.PropertyKey, value, allowTruncatedValue);
        }
        
        #region IDisposable Members

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
        ~ShellPropertyWriter()
        {
            Dispose(false);
        }

        /// <summary>
        /// Release the native and managed objects
        /// </summary>
        protected virtual void Dispose(bool disposing)
        {
            Close();
        }
        
        /// <summary>
        /// Call this method to commit the writes (calls to WriteProperty method)
        /// and dispose off the writer.
        /// </summary>
        public void Close()
        {
            // Close the property writer (commit, etc)
            if (writablePropStore != null)
            {
                HRESULT hr = writablePropStore.Commit();

                Marshal.ReleaseComObject(writablePropStore);
                writablePropStore = null;
            }

            ParentShellObject.NativePropertyStore = null;
        }

        #endregion
    }
}

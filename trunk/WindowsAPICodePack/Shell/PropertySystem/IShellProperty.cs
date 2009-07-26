//Copyright (c) Microsoft Corporation.  All rights reserved.

//Copyright (c) Microsoft Corporation.  All rights reserved.

using System;
namespace Microsoft.WindowsAPICodePack.Shell
{
    /// <summary>
    /// Supports properties of a Shell Property
    /// </summary>
    public interface IShellProperty
    {
        /// <summary>
        /// The Property Key identifying this Property
        /// </summary>
        PropertyKey PropertyKey
        {
            get;
        }

        /// <summary>
        /// Gets a formatted, Unicode string representation of a property value
        /// </summary>
        /// <param name="format">One or more of the PropertyDescriptionFormat flags 
        /// that indicate the desired format</param>
        /// <returns>The formatted value as a string</returns>
        string FormatForDisplay(PropertyDescriptionFormat format);

        /// <summary>
        /// Get the property description object
        /// </summary>
        ShellPropertyDescription Description
        {
            get;
        }

        /// <summary>
        /// Gets the case-sensitive name by which 
        /// a property is known to the system, regardless of its localized name.
        /// </summary>
        string CanonicalName
        {
            get;
        }

        /// <summary>
        /// Return the value for this property using generic "Object" type.
        /// To obtain a specific type for this value, use the more type strong
        /// Property&lt;T&gt; class.
        /// Also, you can only set a value for this type using Property&lt;T&gt; 
        /// class
        /// </summary>
        object ValueAsObject
        {
            get;
        }

        /// <summary>
        /// Get the Value System.Type for this property
        /// </summary>
        Type ValueType
        {
            get;
        }

        /// <summary>
        /// Gets the image reference path and icon index associated with a property value 
        /// This is a Windows 7 only API
        /// </summary>
        IconReference IconReference 
        { 
            get; 
        }
    }
}

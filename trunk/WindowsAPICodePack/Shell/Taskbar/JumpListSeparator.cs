//Copyright (c) Microsoft Corporation.  All rights reserved.

using System;
using System.Runtime.InteropServices;

namespace Microsoft.WindowsAPICodePack.Shell.Taskbar
{
    /// <summary>
    /// Represents a separator in the user task list. The JumpListSeparator control
    /// can only be used in a user task list.
    /// </summary>
    public class JumpListSeparator : ShellLink, IJumpListTask
    {
        internal static PropertyKey PKEY_AppUserModel_IsDestListSeparator = new PropertyKey(new Guid("9F4C2855-9F79-4B39-A8D0-E1D42DE1D5F3"), 6);

        /// <summary>
        /// Initializes a new instance of a JumpListSeparator.
        /// </summary>
        public JumpListSeparator()
            : base("")
        {
            // Set the property IShellLinkW that specifies that this is a jumplist separator
            if (NativeShellLink != null)
            {
                IPropertyStore propertyStore = (IPropertyStore)NativeShellLink;
                PropVariant propVariant = new PropVariant();

                propVariant.SetBool(true);
                propertyStore.SetValue(ref PKEY_AppUserModel_IsDestListSeparator, ref propVariant);
                propVariant.Clear();

                propertyStore.Commit();
            }
        }


        #region IJumpListTask Members

        /// <summary>
        /// Returns an <b>IShellLinkW</b> representation of this object.
        /// </summary>
        /// <returns>An IShellLinkW object</returns>
        object IJumpListTask.GetShellRepresentation()
        {
            return NativeShellLink;
        }

        #endregion
    }
}

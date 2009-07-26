//Copyright (c) Microsoft Corporation.  All rights reserved.

using System;
using System.Runtime.InteropServices;

namespace Microsoft.WindowsAPICodePack.Shell.Taskbar
{
    /// <summary>
    /// Represents a jump list link object.
    /// </summary>
    public class JumpListLink : ShellLink, IJumpListTask, IJumpListItem
    {
        /// <summary>
        /// Initializes a new instance of a JumpListLink.
        /// </summary>
        public JumpListLink()
            : base("")
        {

        }

        /// <summary>
        /// Initializes a new instance of a JumpListLink with the specified path.
        /// </summary>
        /// <param name="path">The path to the item.</param>
        public JumpListLink(string path)
            : base(path)
        {

        }

        #region IJumpListTask Members

        /// <summary>
        /// Returns an <b>IShellLinkW</b> representation of this object.
        /// </summary>
        /// <returns>An IShellLinkW object</returns>
        object IJumpListTask.GetShellRepresentation()
        {
            return this.NativeShellLink;
        }

        #endregion

        #region IJumpListItem Members

        /// <summary>
        /// Gets or sets the target path for this JumpListLink object.
        /// </summary>
        public new string Path
        {
            get
            {
                return base.TargetLocation;
            }
            set
            {
                base.TargetLocation = value;
            }
        }

        /// <summary>
        /// Returns an <b>IShellLinkW</b> representation of this object.
        /// </summary>
        /// <returns>An IShellLinkW object</returns>
        object IJumpListItem.GetShellRepresentation()
        {
            return this.NativeShellLink;
        }

        #endregion
    }
}

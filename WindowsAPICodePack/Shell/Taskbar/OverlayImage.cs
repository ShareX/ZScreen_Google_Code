//Copyright (c) Microsoft Corporation.  All rights reserved.

using System.Drawing;
using System;

namespace Microsoft.WindowsAPICodePack.Shell.Taskbar
{
    /// <summary>
    /// Represents an overlay image on the taskbar button.
    /// </summary>
    public class OverlayImage
    {
        private Icon icon;
        /// <summary>
        /// Gets or sets the icon to use for this overlay.
        /// </summary>
        public Icon Icon
        {
            get
            {
                return icon;
            }
            set
            {
                if (Taskbar.OwnerHandle == IntPtr.Zero)
                    throw new InvalidOperationException("Taskbar Overlay Image cannot be set without an open Form.");

                icon = value;

                if (value == null)
                {
                    // Clear the overlay icon / text
                    Taskbar.TaskbarList.SetOverlayIcon(Taskbar.OwnerHandle, IntPtr.Zero, string.Empty);
                }
                else
                {
                    Taskbar.TaskbarList.SetOverlayIcon(Taskbar.OwnerHandle, value.Handle, Text);
                }
            }
        }

        private string text = string.Empty;
        /// <summary>
        /// Gets or sets the alternate text version of the information
        /// conveyed by the overlay, for accessiblity purposes.
        /// </summary>
        public string Text 
        {
            get
            {
                return text;
            }
            set
            {
                if (Taskbar.OwnerHandle == IntPtr.Zero)
                    throw new InvalidOperationException("Taskbar Overlay Image cannot be set without an open Form.");

                text = value;

                if (Icon == null)
                {
                    // Clear the overlay icon / text
                    Taskbar.TaskbarList.SetOverlayIcon(Taskbar.OwnerHandle, IntPtr.Zero, string.Empty);
                }
                else
                {
                    Taskbar.TaskbarList.SetOverlayIcon(Taskbar.OwnerHandle, Icon.Handle, value);
                }
            }
        }
    
        /// <summary>
        /// Creates a new OverlayImage using the specified icon and alt text.
        /// </summary>
        /// <param name="icon">An icon to use for the overlay.</param>
        /// <param name="text">The alternate text string for this overlay.</param>
        public OverlayImage(Icon icon, string text)
        {
            Icon = icon;
            Text = text;
        }
    }
}

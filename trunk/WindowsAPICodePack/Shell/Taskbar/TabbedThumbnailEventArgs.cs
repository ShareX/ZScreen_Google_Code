// Copyright (c) Microsoft Corporation.  All rights reserved.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace Microsoft.WindowsAPICodePack.Shell.Taskbar
{
    /// <summary>
    /// Event args for various Tabbed Thumbnail related events
    /// </summary>
    public class TabbedThumbnailEventArgs : EventArgs
    {
        /// <summary>
        /// Creates a Event Args for a specific tabbed thumbnail event.
        /// </summary>
        /// <param name="windowHandle">Window handle for the control/window related to the event</param>
        public TabbedThumbnailEventArgs(IntPtr windowHandle)
        {
            WindowHandle = windowHandle;
        }

        /// <summary>
        /// Creates a Event Args for a specific tabbed thumbnail event.
        /// </summary>
        /// <param name="wpfControl">WPF Control (UIElement) related to the event</param>
        public TabbedThumbnailEventArgs(UIElement wpfControl)
        {
            WindowHandle = IntPtr.Zero;
            WPFControl = wpfControl;
        }

        /// <summary>
        /// Gets the Window handle for the specific control/window that is related to this event.
        /// </summary>
        /// <remarks>For WPF Controls (UIElement) the WindowHandle will be IntPtr.Zero. Check the WPFControl property
        /// to get the specific control associated with this event.</remarks>
        public IntPtr WindowHandle
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the WPF Control (UIElement) that is related to this event.
        /// </summary>
        public UIElement WPFControl
        {
            get;
            private set;
        }
    }
}

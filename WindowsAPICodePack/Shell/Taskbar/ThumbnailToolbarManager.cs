//Copyright (c) Microsoft Corporation.  All rights reserved.

using System;
using System.Collections.Generic;
using System.Windows;

namespace Microsoft.WindowsAPICodePack.Shell.Taskbar
{
    /// <summary>
    /// 
    /// </summary>
    public class ThumbnailToolbarManager
    {

        internal ThumbnailToolbarManager()
        {
            // Hide the public constructor so users can't create an instance of this class.
        }

        /// <summary>
        /// Adds thumbnail toolbar for the specified window.
        /// </summary>
        /// <param name="windowHandle">Window handle for which the thumbnail toolbar buttons need to be added</param>
        /// <param name="buttons">Thumbnail buttons for the window's thumbnail toolbar</param>
        /// <exception cref="System.ArgumentException">If the number of buttons exceed the maximum allowed capacity (7).</exception>
        /// <exception cref="System.ArgumentException">If the Window Handle passed in invalid</exception>
        public void AddButtons(IntPtr windowHandle, params ThumbnailButton[] buttons)
        {
            if (windowHandle == IntPtr.Zero)
                throw new ArgumentException("Window handle cannot be empty", "windowHandle");
            if (buttons != null && buttons.Length == 0)
                throw new ArgumentException("Null or empty arrays are not allowed.", "buttons");
            if (buttons.Length > 7)
                throw new ArgumentException("Maximum number of buttons allowed is 7.", "buttons");

            // Add the buttons to our window manager, which will also create a proxy window
            TaskbarWindowManager.Instance.AddThumbnailButtons(windowHandle, buttons);
        }

        /// <summary>
        /// Adds thumbnail toolbar for the specified WPF Control.
        /// </summary>
        /// <param name="control">WPF Control for which the thumbnail toolbar buttons need to be added</param>
        /// <param name="buttons">Thumbnail buttons for the window's thumbnail toolbar</param>
        /// <exception cref="System.ArgumentException">If the number of buttons exceed the maximum allowed capacity (7).</exception>
        /// <exception cref="System.ArgumentNullException">If the control passed in null</exception>
        public void AddButtons(UIElement control, params ThumbnailButton[] buttons)
        {
            if (control == null)
                throw new ArgumentNullException("Control cannot be null", "control");
            if (buttons != null && buttons.Length == 0)
                throw new ArgumentException("Null or empty arrays are not allowed.", "buttons");
            if (buttons.Length > 7)
                throw new ArgumentException("Maximum number of buttons allowed is 7.", "buttons");

            // Add the buttons to our window manager, which will also create a proxy window
            TaskbarWindowManager.Instance.AddThumbnailButtons(control, buttons);
        }
    }
}

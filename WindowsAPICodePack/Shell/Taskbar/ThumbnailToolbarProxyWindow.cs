//Copyright (c) Microsoft Corporation.  All rights reserved.

using System;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Microsoft.WindowsAPICodePack.Shell.Taskbar
{
    internal class ThumbnailToolbarProxyWindow : NativeWindow
    {
        private ThumbnailButton[] thumbnailButtons;
        private IntPtr internalWindowHandle;

        internal System.Windows.UIElement WPFControl
        {
            get;
            set;
        }

        internal IntPtr WindowToTellTaskbarAbout
        {
            get 
            { 
                if(internalWindowHandle == IntPtr.Zero)
                    return this.Handle;
                else
                    return internalWindowHandle;
            }
        }

        internal TaskbarWindow TaskbarWindow
        {
            get;
            set;
        }

        internal ThumbnailToolbarProxyWindow(IntPtr windowHandle, ThumbnailButton[] buttons)
        {
            if (windowHandle == IntPtr.Zero)
                throw new ArgumentException("Window handle cannot be empty", "windowHandle");
            if (buttons != null && buttons.Length == 0)
                throw new ArgumentException("Null or empty arrays are not allowed.", "buttons");

            //
            internalWindowHandle = windowHandle;
            thumbnailButtons = buttons;

            // Set the window handle on the buttons (for future updates)
            Array.ForEach(thumbnailButtons, new Action<ThumbnailButton>(UpdateHandle));

            // Assign the window handle (coming from the user) to this native window
            // so we can intercept the window messages sent from the taskbar to this window.
            this.AssignHandle(windowHandle);
        }

        internal ThumbnailToolbarProxyWindow(System.Windows.UIElement wpfControl, ThumbnailButton[] buttons)
        {
            if (wpfControl == null)
                throw new ArgumentNullException("Control cannot be null", "wpfControl");
            if (buttons != null && buttons.Length == 0)
                throw new ArgumentException("Null or empty arrays are not allowed.", "buttons");

            //
            internalWindowHandle = IntPtr.Zero;
            WPFControl = wpfControl;
            thumbnailButtons = buttons;

            // Set the window handle on the buttons (for future updates)
            Array.ForEach(thumbnailButtons, new Action<ThumbnailButton>(UpdateHandle));
        }


        private void UpdateHandle(ThumbnailButton button)
        {
            button.WindowHandle = internalWindowHandle;
            button.AddedToTaskbar = false;
        }

        protected override void WndProc(ref Message m)
        {
            bool handled = false;

            handled = TaskbarWindowManager.Instance.DispatchMessage(ref m, this.TaskbarWindow);

            if (!handled)
                base.WndProc(ref m);
        }
    }
}

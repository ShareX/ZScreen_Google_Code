// Copyright (c) Microsoft Corporation.  All rights reserved.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Microsoft.WindowsAPICodePack.Shell.Taskbar
{
    internal class TaskbarWindow
    {
        internal TabbedThumbnailProxyWindow TabbedThumbnailProxyWindow
        {
            get;
            set;
        }

        internal ThumbnailToolbarProxyWindow ThumbnailToolbarProxyWindow
        {
            get;
            set;
        }

        internal bool EnableTabbedThumbnails
        {
            get;
            set;
        }

        internal bool EnableThumbnailToolbars
        {
            get;
            set;
        }

        internal IntPtr UserWindowHandle
        {
            get;
            set;
        }

        private TabbedThumbnailPreview tabbedThumbnailPreview = null;
        internal TabbedThumbnailPreview TabbedThumbnailPreview
        {
            get { return tabbedThumbnailPreview; }
            set
            {
                if (tabbedThumbnailPreview == null)
                {
                    TabbedThumbnailProxyWindow = new TabbedThumbnailProxyWindow(value);
                    tabbedThumbnailPreview = value;
                }
                else
                    throw new InvalidOperationException("Value is already set. It cannot be set more than once.");
            }
        }

        private ThumbnailButton[] thumbnailButtons;
        internal ThumbnailButton[] ThumbnailButtons
        {
            get { return thumbnailButtons; }
            set
            {
                thumbnailButtons = value;

                // Set the window handle on the buttons (for future updates)
                Array.ForEach(thumbnailButtons, new Action<ThumbnailButton>(UpdateHandle));
            }
        }

        private void UpdateHandle(ThumbnailButton button)
        {
            button.WindowHandle = WindowToTellTaskbarAbout;
            button.AddedToTaskbar = false;
        }

        internal IntPtr WindowToTellTaskbarAbout
        {
            get
            {
                if (EnableThumbnailToolbars && !EnableTabbedThumbnails && ThumbnailToolbarProxyWindow != null)
                    return ThumbnailToolbarProxyWindow.WindowToTellTaskbarAbout;
                else if (!EnableThumbnailToolbars && EnableTabbedThumbnails && TabbedThumbnailProxyWindow != null)
                    return TabbedThumbnailProxyWindow.WindowToTellTaskbarAbout;
                else if (EnableTabbedThumbnails && EnableThumbnailToolbars && TabbedThumbnailProxyWindow != null)
                    return TabbedThumbnailProxyWindow.WindowToTellTaskbarAbout;
                else
                    throw new InvalidOperationException();
            }
        }

        internal string Title
        {
            set
            {
                if (TabbedThumbnailProxyWindow != null)
                    TabbedThumbnailProxyWindow.Text = value;
                else
                    throw new InvalidOperationException();
            }
        }

        internal TaskbarWindow(IntPtr userWindowHandle, params ThumbnailButton[] buttons)
        {
            if (userWindowHandle == IntPtr.Zero)
                throw new ArgumentException("userWindowHandle");

            if (buttons == null || buttons.Length == 0)
                throw new ArgumentException("buttons");

            // Create our proxy window
            ThumbnailToolbarProxyWindow = new ThumbnailToolbarProxyWindow(userWindowHandle, buttons);
            ThumbnailToolbarProxyWindow.TaskbarWindow = this;

            // Set our current state
            EnableThumbnailToolbars = true;
            EnableTabbedThumbnails = false;

            //
            this.ThumbnailButtons = buttons;
            UserWindowHandle = userWindowHandle;
        }

        internal TaskbarWindow(System.Windows.UIElement wpfControl, params ThumbnailButton[] buttons)
        {
            if (wpfControl == null)
                throw new ArgumentNullException("wpfControl");

            if (buttons == null || buttons.Length == 0)
                throw new ArgumentException("buttons");

            // Create our proxy window
            ThumbnailToolbarProxyWindow = new ThumbnailToolbarProxyWindow(wpfControl, buttons);
            ThumbnailToolbarProxyWindow.TaskbarWindow = this;

            // Set our current state
            EnableThumbnailToolbars = true;
            EnableTabbedThumbnails = false;
            
            this.ThumbnailButtons = buttons;
            UserWindowHandle = IntPtr.Zero;
        }

        internal TaskbarWindow(TabbedThumbnailPreview preview)
        {
            if (preview == null)
                throw new ArgumentException("preview");

            // Create our proxy window
            TabbedThumbnailProxyWindow = new TabbedThumbnailProxyWindow(preview);

            // set our current state
            EnableThumbnailToolbars = false;
            EnableTabbedThumbnails = true;

            //
            UserWindowHandle = preview.WindowHandle;
            TabbedThumbnailPreview = preview;
            
        }

    }
}

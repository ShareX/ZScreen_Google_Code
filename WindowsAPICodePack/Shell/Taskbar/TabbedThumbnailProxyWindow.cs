// Copyright (c) Microsoft Corporation.  All rights reserved.

using System;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Windows;

namespace Microsoft.WindowsAPICodePack.Shell.Taskbar
{
    internal sealed class TabbedThumbnailProxyWindow : Form
    {
        private IntPtr proxyingFor;

        internal TabbedThumbnailProxyWindow(TabbedThumbnailPreview preview)
        {
            TabbedThumbnailPreview = preview;

            if (preview.WindowHandle != IntPtr.Zero)
            {
                proxyingFor = preview.WindowHandle;
                Size = new System.Drawing.Size(1, 1);

                // Try to get the window text so we can use it on the tabbed thumbnail as well
                StringBuilder text = new StringBuilder(256);
                TabbedThumbnailNativeMethods.GetWindowText(proxyingFor, text, text.Capacity);
                Text = text.ToString();
                
                // If we get a valid title from the GetWindowText method,
                // and also if the user hasn't set any title on the preview object,
                // then update the preview's title with what we get from GetWindowTitle
                if(!string.IsNullOrEmpty(Text) && string.IsNullOrEmpty(preview.Title))
                    preview.Title = Text;
            }
            else if (preview.WpfControl != null)
            {
                proxyingFor = IntPtr.Zero;
                WPFControl = preview.WpfControl;
                Size = new System.Drawing.Size(1, 1);
                // Since we can't get the text/caption for a UIElement, not setting this.Text here.

            }
        }

        internal TabbedThumbnailPreview TabbedThumbnailPreview
        {
            get;
            private set;
        }

        internal IntPtr RealWindow
        {
            get { return proxyingFor; }
        }

        internal UIElement WPFControl
        {
            get;
            private set;
        }

        internal IntPtr WindowToTellTaskbarAbout
        {
            get { return this.Handle; }
        }

        protected override void WndProc(ref Message m)
        {
            bool handled = false;

            handled = TaskbarWindowManager.Instance.DispatchMessage(ref m, this.TabbedThumbnailPreview.TaskbarWindow);

            if (!handled)
                base.WndProc(ref m);
        }
    }
}
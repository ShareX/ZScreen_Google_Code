//Copyright (c) Microsoft Corporation.  All rights reserved.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Interop;
using System.Drawing;
using System.Threading;

namespace Microsoft.WindowsAPICodePack.Shell.Taskbar
{
    /// <summary>
    /// Represents the main class for adding and removing tabbed thumbnails on the Taskbar
    /// for child windows and controls.
    /// </summary>
    public class TabbedThumbnail
    {
        /// <summary>
        /// Internal dictionary to keep track of the user's window handle and its 
        /// corresponding thumbnail preview objects.
        /// </summary>
        private IDictionary<IntPtr, TabbedThumbnailPreview> tabbedThumbnailList;
        private IDictionary<UIElement, TabbedThumbnailPreview> tabbedThumbnailListWPF; // list for WPF controls

        #region Public Events

        /// <summary>
        /// The event that occurs when a tab is closed on the taskbar thumbnail preview.
        /// </summary>
        public event EventHandler<TabbedThumbnailEventArgs> TabbedThumbnailClosed;

        /// <summary>
        /// The event that occurs when a tab is maximized via the taskbar thumbnail preview (context menu).
        /// </summary>
        public event EventHandler<TabbedThumbnailEventArgs> TabbedThumbnailMaximized;

        /// <summary>
        /// The event that occurs when a tab is minimized via the taskbar thumbnail preview (context menu).
        /// </summary>
        public event EventHandler<TabbedThumbnailEventArgs> TabbedThumbnailMinimized;

        /// <summary>
        /// The event that occurs when a tab is activated (clicked) on the taskbar thumbnail preview.
        /// </summary>
        public event EventHandler<TabbedThumbnailEventArgs> TabbedThumbnailActivated;

        /// <summary>
        /// The event that occurs when a thumbnail or peek bitmap is requested by the user.
        /// </summary>
        public event EventHandler<TabbedThumbnailEventArgs> TabbedThumbnailBitmapRequested;

        #endregion

        /// <summary>
        /// Internal constructor that creates a new dictionary for keeping track of the window handles
        /// and their corresponding thumbnail preview objects.
        /// </summary>
        internal TabbedThumbnail()
        {
            tabbedThumbnailList = new Dictionary<IntPtr, TabbedThumbnailPreview>();
            tabbedThumbnailListWPF = new Dictionary<UIElement, TabbedThumbnailPreview>();

            // Register for standard events from the window manager
            TaskbarWindowManager.Instance.TabbedThumbnailClosed += new EventHandler<TabbedThumbnailEventArgs>(windowManager_TabbedThumbnailClosed);
            TaskbarWindowManager.Instance.TabbedThumbnailActivated += new EventHandler<TabbedThumbnailEventArgs>(windowManager_TabbedThumbnailActivated);
            TaskbarWindowManager.Instance.TabbedThumbnailMaximized += new EventHandler<TabbedThumbnailEventArgs>(windowManager_TabbedThumbnailMaximized);
            TaskbarWindowManager.Instance.TabbedThumbnailMinimized += new EventHandler<TabbedThumbnailEventArgs>(windowManager_TabbedThumbnailMinimized);
            TaskbarWindowManager.Instance.TabbedThumbnailBitmapRequested += new EventHandler<TabbedThumbnailEventArgs>(windowManager_TabbedThumbnailBitmapRequested);
        }

        /// <summary>
        /// Adds a new tabbed thumbnail to the taskbar.
        /// </summary>
        /// <param name="preview">Thumbnail preview for a specific window handle or control. The preview
        /// object can be initialized with specific properties for the title, bitmap, and tooltip.</param>
        /// <exception cref="System.ArgumentException">If the tabbed thumbnail has already been added</exception>
        public void AddThumbnailPreview(TabbedThumbnailPreview preview)
        {
            if (preview.WindowHandle == IntPtr.Zero) // it's most likely a UI Element
            {
                if (tabbedThumbnailListWPF.ContainsKey(preview.WpfControl))
                    throw new ArgumentException("This preview has already been added");
            }
            else
            {
                // Regular control with a valid handle
                if (tabbedThumbnailList.ContainsKey(preview.WindowHandle))
                    throw new ArgumentException("This preview has already been added");
            }

            TaskbarWindowManager.Instance.AddTabbedThumbnail(preview);

            // Add the preview and window manager to our cache

            // Probably a UIElement control
            if (preview.WindowHandle == IntPtr.Zero)
                tabbedThumbnailListWPF.Add(preview.WpfControl, preview);
            else
                tabbedThumbnailList.Add(preview.WindowHandle, preview);

            InvalidateThumbnails();
        }

        #region Internal Event Handlers

        /// <summary>
        /// Event handler to recieve the event from the window manager and then forward it to the user
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void windowManager_TabbedThumbnailBitmapRequested(object sender, TabbedThumbnailEventArgs e)
        {
            // Foward the event to the user
            if (TabbedThumbnailBitmapRequested != null)
                TabbedThumbnailBitmapRequested(this, e);
        }

        /// <summary>
        /// Event handler to recieve the event from the window manager and then forward it to the user
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void windowManager_TabbedThumbnailMinimized(object sender, TabbedThumbnailEventArgs e)
        {
            if (TabbedThumbnailMinimized != null)
                TabbedThumbnailMinimized(this, e);
            else
            {
                // No one is listening to these events.
                // Forward the message to the main window
                TabbedThumbnailPreview preview = null;
                
                if (e.WindowHandle != IntPtr.Zero)
                    preview = GetThumbnailPreview(e.WindowHandle);
                else if (e.WPFControl != null)
                    preview = GetThumbnailPreview(e.WPFControl);

                if (preview != null)
                    CoreNativeMethods.SendMessage(preview.ParentWindowHandle, TabbedThumbnailNativeMethods.WM_SYSCOMMAND, new IntPtr(TabbedThumbnailNativeMethods.SC_MINIMIZE), IntPtr.Zero);
            }
        }

        /// <summary>
        /// Event handler to recieve the event from the window manager and then forward it to the user
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void windowManager_TabbedThumbnailMaximized(object sender, TabbedThumbnailEventArgs e)
        {
            if (TabbedThumbnailMaximized != null)
                TabbedThumbnailMaximized(this, e);
            else
            {
                // No one is listening to these events.
                // Forward the message to the main window
                TabbedThumbnailPreview preview = null;

                if (e.WindowHandle != IntPtr.Zero)
                    preview = GetThumbnailPreview(e.WindowHandle);
                else if (e.WPFControl != null)
                    preview = GetThumbnailPreview(e.WPFControl);

                if (preview != null)
                    CoreNativeMethods.SendMessage(preview.ParentWindowHandle, TabbedThumbnailNativeMethods.WM_SYSCOMMAND, new IntPtr(TabbedThumbnailNativeMethods.SC_MAXIMIZE), IntPtr.Zero);
            }
        }

        /// <summary>
        /// Event handler to recieve the event from the window manager and then forward it to the user
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void windowManager_TabbedThumbnailActivated(object sender, TabbedThumbnailEventArgs e)
        {
            if (TabbedThumbnailActivated != null)
                TabbedThumbnailActivated(this, e);
            else
            {
                // No one is listening to these events.
                // Forward the message to the main window
                TabbedThumbnailPreview preview = null;

                if (e.WindowHandle != IntPtr.Zero)
                    preview = GetThumbnailPreview(e.WindowHandle);
                else if (e.WPFControl != null)
                    preview = GetThumbnailPreview(e.WPFControl);

                if (preview != null)
                    CoreNativeMethods.SendMessage(
                        preview.ParentWindowHandle, 
                        TabbedThumbnailNativeMethods.WM_ACTIVATEAPP,   
                        new IntPtr(1),
                        new IntPtr(Thread.CurrentThread.GetHashCode()));
            }
        }

        /// <summary>
        /// Event handler to recieve the event from the window manager and then forward it to the user
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void windowManager_TabbedThumbnailClosed(object sender, TabbedThumbnailEventArgs e)
        {
            // Forward the event to the user
            if (TabbedThumbnailClosed != null)
                TabbedThumbnailClosed(this, e);
            else
            {
                // No one is listening to these events.
                // Forward the message to the main window
                TabbedThumbnailPreview preview = null;

                if (e.WindowHandle != IntPtr.Zero)
                    preview = GetThumbnailPreview(e.WindowHandle);
                else if (e.WPFControl != null)
                    preview = GetThumbnailPreview(e.WPFControl);

                if (preview != null)
                    CoreNativeMethods.SendMessage(preview.ParentWindowHandle, TabbedThumbnailNativeMethods.WM_NCDESTROY, IntPtr.Zero, IntPtr.Zero);
            }

            // Remove it from the internal list as well as the taskbar
            if (e.WindowHandle != IntPtr.Zero)
                RemoveThumbnailPreview(e.WindowHandle);
            else
                RemoveThumbnailPreview(e.WPFControl);
        }

        #endregion

        /// <summary>
        /// Gets the TabbedThumbnailPreview object associated with the given window handle
        /// </summary>
        /// <param name="windowHandle">Window handle for the control/window</param>
        /// <returns>TabbedThumbnailPreview associated with the given window handle</returns>
        public TabbedThumbnailPreview GetThumbnailPreview(IntPtr windowHandle)
        {
            if (windowHandle == IntPtr.Zero)
                throw new ArgumentException("Window handle is invalid", "windowHandle");

            if (tabbedThumbnailList.ContainsKey(windowHandle))
                return tabbedThumbnailList[windowHandle];
            else
                return null;
        }

        /// <summary>
        /// Gets the TabbedThumbnailPreview object associated with the given control
        /// </summary>
        /// <param name="control">Specific control for which the preview object is requested</param>
        /// <returns>TabbedThumbnailPreview associated with the given control</returns>
        public TabbedThumbnailPreview GetThumbnailPreview(Control control)
        {
            if (control == null)
                throw new ArgumentNullException("control");

            return GetThumbnailPreview(control.Handle);
        }

        /// <summary>
        /// Gets the TabbedThumbnailPreview object associated with the given WPF Window
        /// </summary>
        /// <param name="wpfControl">WPF Control (UIElement) for which the preview object is requested</param>
        /// <returns>TabbedThumbnailPreview associated with the given WPF Window</returns>
        public TabbedThumbnailPreview GetThumbnailPreview(UIElement wpfControl)
        {
            if (wpfControl == null)
                throw new ArgumentNullException("wpfControl");

            if (tabbedThumbnailListWPF.ContainsKey(wpfControl))
                return tabbedThumbnailListWPF[wpfControl];
            else
                return null;

        }

        /// <summary>
        /// Remove the tabbed thumbnail from the taskbar.
        /// </summary>
        /// <param name="preview">TabbedThumbnailPreview associated with the control/window that 
        /// is to be removed from the taskbar</param>
        public void RemoveThumbnailPreview(TabbedThumbnailPreview preview)
        {
            if (preview == null)
                throw new ArgumentNullException("preview");

            if (tabbedThumbnailList.ContainsKey(preview.WindowHandle))
                RemoveThumbnailPreview(preview.WindowHandle);
            else if (tabbedThumbnailListWPF.ContainsKey(preview.WpfControl))
                RemoveThumbnailPreview(preview.WpfControl);
        }

        /// <summary>
        /// Remove the tabbed thumbnail from the taskbar.
        /// </summary>
        /// <param name="windowHandle">TabbedThumbnailPreview associated with the window handle that 
        /// is to be removed from the taskbar</param>
        public void RemoveThumbnailPreview(IntPtr windowHandle)
        {
            if (tabbedThumbnailList.ContainsKey(windowHandle))
            {
                TaskbarWindowManager.Instance.UnregisterTab(tabbedThumbnailList[windowHandle].TaskbarWindow);

                tabbedThumbnailList.Remove(windowHandle);
            }
            else
                throw new ArgumentException("The given control has not been added to the taskbar.");
        }

        /// <summary>
        /// Remove the tabbed thumbnail from the taskbar.
        /// </summary>
        /// <param name="control">TabbedThumbnailPreview associated with the control that 
        /// is to be removed from the taskbar</param>
        public void RemoveThumbnailPreview(Control control)
        {
            if (control == null)
                throw new ArgumentNullException("control");

            IntPtr handle = control.Handle;

            RemoveThumbnailPreview(handle);
        }

        /// <summary>
        /// Remove the tabbed thumbnail from the taskbar.
        /// </summary>
        /// <param name="wpfControl">TabbedThumbnailPreview associated with the WPF Control (UIElement) that 
        /// is to be removed from the taskbar</param>
        public void RemoveThumbnailPreview(UIElement wpfControl)
        {
            if (wpfControl == null)
                throw new ArgumentNullException("wpfControl");

            if (tabbedThumbnailListWPF.ContainsKey(wpfControl))
            {
                TaskbarWindowManager.Instance.UnregisterTab(tabbedThumbnailListWPF[wpfControl].TaskbarWindow);

                tabbedThumbnailListWPF.Remove(wpfControl);
            }
            else
                throw new ArgumentException("The given control has not been added to the taskbar.");
        }

        /// <summary>
        /// Sets the given tabbed thumbnail preview object as being active on the taskbar tabbed thumbnails list.
        /// Call this method to keep the application and the taskbar in sync as to which window/control
        /// is currently active (or selected, in the case of tabbed application).
        /// </summary>
        /// <param name="preview">TabbedThumbnailPreview for the specific control/indow that is currently active in the application</param>
        /// <exception cref="System.ArgumentException">If the control/window is not yet added to the tabbed thumbnails list</exception>
        public void SetActiveTab(TabbedThumbnailPreview preview)
        {
            if (preview.WindowHandle != IntPtr.Zero)
            {
                if (tabbedThumbnailList.ContainsKey(preview.WindowHandle))
                    TaskbarWindowManager.Instance.SetActiveTab(tabbedThumbnailList[preview.WindowHandle].TaskbarWindow);
                else
                    throw new ArgumentException("The given preview has not been added to the taskbar.");
            }
            else if (preview.WpfControl != null)
            {
                if (tabbedThumbnailListWPF.ContainsKey(preview.WpfControl))
                    TaskbarWindowManager.Instance.SetActiveTab(tabbedThumbnailListWPF[preview.WpfControl].TaskbarWindow);
                else
                    throw new ArgumentException("The given preview has not been added to the taskbar.");
            }
        }

        /// <summary>
        /// Sets the given window handle as being active on the taskbar tabbed thumbnails list.
        /// Call this method to keep the application and the taskbar in sync as to which window/control
        /// is currently active (or selected, in the case of tabbed application).
        /// </summary>
        /// <param name="windowHandle">Window handle for the control/window that is currently active in the application</param>
        /// <exception cref="System.ArgumentException">If the control/window is not yet added to the tabbed thumbnails list</exception>
        public void SetActiveTab(IntPtr windowHandle)
        {
            if (tabbedThumbnailList.ContainsKey(windowHandle))
                TaskbarWindowManager.Instance.SetActiveTab(tabbedThumbnailList[windowHandle].TaskbarWindow);
            else
                throw new ArgumentException("The given control has not been added to the taskbar.");
        }

        /// <summary>
        /// Sets the given Control/Form window as being active on the taskbar tabbed thumbnails list.
        /// Call this method to keep the application and the taskbar in sync as to which window/control
        /// is currently active (or selected, in the case of tabbed application).
        /// </summary>
        /// <param name="control">Control/Form that is currently active in the application</param>
        /// <exception cref="System.ArgumentException">If the control/window is not yet added to the tabbed thumbnails list</exception>
        public void SetActiveTab(Control control)
        {
            if (control == null)
                throw new ArgumentNullException("control");

            SetActiveTab(control.Handle);
        }

        /// <summary>
        /// Sets the given WPF window as being active on the taskbar tabbed thumbnails list.
        /// Call this method to keep the application and the taskbar in sync as to which window/control
        /// is currently active (or selected, in the case of tabbed application).
        /// </summary>
        /// <param name="wpfControl">WPF control that is currently active in the application</param>
        /// <exception cref="System.ArgumentException">If the control/window is not yet added to the tabbed thumbnails list</exception>
        public void SetActiveTab(UIElement wpfControl)
        {
            if (wpfControl == null)
                throw new ArgumentNullException("wpfControl");

            if (tabbedThumbnailListWPF.ContainsKey(wpfControl))
                TaskbarWindowManager.Instance.SetActiveTab(tabbedThumbnailListWPF[wpfControl].TaskbarWindow);
            else
                throw new ArgumentException("The given control has not been added to the taskbar.");
        }

        /// <summary>
        /// Invalidates all the tabbed thumbnails. This will force the Desktop Window Manager
        /// to not use the cached thumbnail or preview or aero peek and request a new one next time.
        /// </summary>
        public void InvalidateThumbnails()
        {
            // Invalidate all the previews currently in our cache.
            // This will ensure we get updated bitmaps next time
            tabbedThumbnailList.Values.ToList<TabbedThumbnailPreview>().ForEach(thumbPreview => TaskbarWindowManager.Instance.InvalidatePreview(tabbedThumbnailList[thumbPreview.WindowHandle].TaskbarWindow));
            tabbedThumbnailListWPF.Values.ToList<TabbedThumbnailPreview>().ForEach(thumbPreview => TaskbarWindowManager.Instance.InvalidatePreview(tabbedThumbnailListWPF[thumbPreview.WpfControl].TaskbarWindow));

            tabbedThumbnailList.Values.ToList<TabbedThumbnailPreview>().ForEach(thumbPreview => thumbPreview.SetImage(IntPtr.Zero));
            tabbedThumbnailListWPF.Values.ToList<TabbedThumbnailPreview>().ForEach(thumbPreview => thumbPreview.SetImage(IntPtr.Zero));

        }

        /// <summary>
        /// Selects a portion of a window's client area to display as that window's thumbnail in the taskbar.
        /// </summary>
        /// <param name="windowHandle">The handle to a window represented in the taskbar. This has to be a top-level window.</param>
        /// <param name="clippingRect">Rectangle structure that specifies a selection within the window's client area,
        /// relative to the upper-left corner of that client area.</param>
        /// <remarks>To clear a clip that is already in place and return to the default display of the thumbnail, set
        /// the clippingRect parameter to an empty rectangle (Rectangle.Empty)</remarks>
        public void SetThumbnailClip(IntPtr windowHandle, Rectangle clippingRect)
        {
            CoreNativeMethods.RECT rect = new CoreNativeMethods.RECT();
            rect.left = clippingRect.Left;
            rect.top = clippingRect.Top;
            rect.right = clippingRect.Right;
            rect.bottom = clippingRect.Bottom;

            Taskbar.TaskbarList.SetThumbnailClip(windowHandle, ref rect);
        }
    }
}

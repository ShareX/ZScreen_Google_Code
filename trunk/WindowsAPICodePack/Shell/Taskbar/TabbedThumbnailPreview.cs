//Copyright (c) Microsoft Corporation.  All rights reserved.

using System;
using System.Drawing;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Runtime.InteropServices;

namespace Microsoft.WindowsAPICodePack.Shell.Taskbar
{
    /// <summary>
    /// Represents a tabbed thumbnail on the taskbar for a given window or a control.
    /// </summary>
    public class TabbedThumbnailPreview
    {
        #region Internal members

        internal IntPtr WindowHandle
        {
            get;
            set;
        }

        internal IntPtr ParentWindowHandle
        {
            get;
            set;
        }

        internal UIElement WpfControl
        {
            get;
            set;
        }

        internal Window WpfParentWindow
        {
            get;
            set;
        }

        internal TaskbarWindow TaskbarWindow
        {
            get;
            set;
        }

        private bool addedToTaskbar;
        internal bool AddedToTaskbar
        {
            get
            {
                return addedToTaskbar;
            }
            set
            {
                addedToTaskbar = value;

                // The user has updated the clipping region, so invalidate our existing preview
                if (TaskbarWindowManager.Instance != null && ClippingRectangle != Rectangle.Empty)
                    TaskbarWindowManager.Instance.InvalidatePreview(this.TaskbarWindow);
            }
        }

        internal bool RemovedFromTaskbar
        {
            get;
            set;
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Creates a new TabbedThumbnailPreview with the given window handle of the parent and
        /// a child control/window's handle (e.g. TabPage or Panel)
        /// </summary>
        /// <param name="parentWindowHandle">Window handle of the parent window. 
        /// This window has to be a top-level window and the handle cannot be null or IntPtr.Zero</param>
        /// <param name="windowHandle">Window handle of the child control or window for which a tabbed 
        /// thumbnail needs to be displayed</param>
        public TabbedThumbnailPreview(IntPtr parentWindowHandle, IntPtr windowHandle)
        {
            if (parentWindowHandle == IntPtr.Zero)
                throw new ArgumentException("Parent window handle cannot be zero.", "parentWindowHandle");
            if (windowHandle == IntPtr.Zero)
                throw new ArgumentException("Child control's window handle cannot be zero.", "windowHandle");

            WindowHandle = windowHandle;
            ParentWindowHandle = parentWindowHandle;
        }

        /// <summary>
        /// Creates a new TabbedThumbnailPreview with the given window handle of the parent and
        /// a child control (e.g. TabPage or Panel)
        /// </summary>
        /// <param name="parentWindowHandle">Window handle of the parent window. 
        /// This window has to be a top-level window and the handle cannot be null or IntPtr.Zero</param>
        /// <param name="control">Child control for which a tabbed thumbnail needs to be displayed</param>
        public TabbedThumbnailPreview(IntPtr parentWindowHandle, Control control)
        {
            if (parentWindowHandle == IntPtr.Zero)
                throw new ArgumentException("Parent window handle cannot be zero.", "parentWindowHandle");
            if (control == null)
                throw new ArgumentNullException("control");

            WindowHandle = control.Handle;
            ParentWindowHandle = parentWindowHandle;
        }

        /// <summary>
        /// Creates a new TabbedThumbnailPreview with the given window handle of the parent and
        /// a WPF child Window.
        /// </summary>
        /// <param name="parentWindow">Parent window for the UIElement control. 
        /// This window has to be a top-level window and the handle cannot be null</param>
        /// <param name="wpfControl">WPF Control (UIElement) for which a tabbed thumbnail needs to be displayed</param>
        public TabbedThumbnailPreview(Window parentWindow, UIElement wpfControl)
        {
            if (wpfControl == null)
                throw new ArgumentNullException("control");
            if (parentWindow == null)
                throw new ArgumentNullException("parentWindow");

            WindowHandle = IntPtr.Zero;
            WpfControl = wpfControl;
            WpfParentWindow = parentWindow;
            ParentWindowHandle = (new WindowInteropHelper(parentWindow)).Handle;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// This event is raised when the Title property changes.
        /// </summary>
        public event EventHandler TitleChanged;

        /// <summary>
        /// This event is raised when the Tooltip property changes.
        /// </summary>
        public event EventHandler TooltipChanged;

        private string title;
        /// <summary>
        /// Title for the window shown as the taskbar thumbnail.
        /// </summary>
        public string Title
        {
            get
            {
                return title;
            }
            set
            {
                if (value != title)
                {
                    title = value;

                    if (TitleChanged != null)
                        TitleChanged(this, EventArgs.Empty);
                }
            }
        }

        private string tooltip;
        /// <summary>
        /// Tooltip to be shown for this thumbnail on the taskbar. 
        /// By default this is full title of the window shown on the taskbar.
        /// </summary>
        public string Tooltip
        {
            get { return tooltip; }
            set
            {
                if (value != tooltip)
                {
                    tooltip = value;

                    if (TooltipChanged != null)
                        TooltipChanged(this, EventArgs.Empty);
                }
            }
        }

        private Rectangle clippingRectangle;
        /// <summary>
        /// Specifies that only a portion of the window's client area
        /// should be used in the window's thumbnail.
        /// </summary>
        public Rectangle ClippingRectangle
        {
            get { return clippingRectangle; }
            set
            {
                clippingRectangle = value;

                // The user has updated the clipping region, so invalidate our existing preview
                if (TaskbarWindowManager.Instance != null)
                    TaskbarWindowManager.Instance.InvalidatePreview(this.TaskbarWindow);
            }
        }

        internal IntPtr Bitmap
        {
            get;
            private set;
        }

        /// <summary>
        /// Override the thumbnail and peek bitmap. 
        /// By providing this bitmap manually, Thumbnail Window manager will provide the 
        /// Desktop Window Manager (DWM) this bitmap instead of rendering one automatically.
        /// Use this property to update the bitmap whenever the control is updated and the user
        /// needs to be shown a new thumbnail on the taskbar preview (or aero peek).
        /// </summary>
        /// <remarks>
        /// If the bitmap doesn't have the right dimensions, the DWM may scale it or not 
        /// render certain areas as appropriate - it is the user's responsibility
        /// to render a bitmap with the proper dimensions.
        /// </remarks>
        public void SetImage(Bitmap bitmap)
        {
            if (bitmap != null)
                SetImage(bitmap.GetHbitmap());
            else
                Bitmap = IntPtr.Zero;
        }

        /// <summary>
        /// Override the thumbnail and peek bitmap. 
        /// By providing this bitmap manually, Thumbnail Window manager will provide the 
        /// Desktop Window Manager (DWM) this bitmap instead of rendering one automatically.
        /// Use this property to update the bitmap whenever the control is updated and the user
        /// needs to be shown a new thumbnail on the taskbar preview (or aero peek).
        /// </summary>
        /// <remarks>
        /// If the bitmap doesn't have the right dimensions, the DWM may scale it or not 
        /// render certain areas as appropriate - it is the user's responsibility
        /// to render a bitmap with the proper dimensions.
        /// </remarks>
        public void SetImage(BitmapSource bitmapSource)
        {
            if (bitmapSource == null)
            {
                Bitmap = IntPtr.Zero;
                return;
            }

            int width = bitmapSource.PixelWidth;
            int height = bitmapSource.PixelHeight;
            int stride = width * ((bitmapSource.Format.BitsPerPixel + 7) / 8);

            byte[] bits = new byte[height * stride];

            bitmapSource.CopyPixels(bits, stride, 0);

            IntPtr ptr = Marshal.AllocHGlobal(bits.Length);
            System.Runtime.InteropServices.Marshal.Copy(bits, 0, ptr, bits.Length);

            Bitmap bmp = new Bitmap(
                        width,
                        height,
                        stride,
                        System.Drawing.Imaging.PixelFormat.Format32bppPArgb,
                        ptr);
   
            SetImage(bmp.GetHbitmap());
        }


        /// <summary>
        /// Override the thumbnail and peek bitmap. 
        /// By providing this bitmap manually, Thumbnail Window manager will provide the 
        /// Desktop Window Manager (DWM) this bitmap instead of rendering one automatically.
        /// Use this property to update the bitmap whenever the control is updated and the user
        /// needs to be shown a new thumbnail on the taskbar preview (or aero peek).
        /// </summary>
        /// <remarks>
        /// If the bitmap doesn't have the right dimensions, the DWM may scale it or not 
        /// render certain areas as appropriate - it is the user's responsibility
        /// to render a bitmap with the proper dimensions.
        /// </remarks>
        public void SetImage(IntPtr hBitmap)
        {
            // Before we set the bitmap, dispose the old bitmap
            if (Bitmap != IntPtr.Zero)
                ShellNativeMethods.DeleteObject(Bitmap);
            
            // Set the new bitmap
            Bitmap = hBitmap;

            // Let DWM know to invalidate its cached thumbnail/preview and ask us for a new one (i.e. the one
            // user just updated)
            if (TaskbarWindowManager.Instance != null)
                TaskbarWindowManager.Instance.InvalidatePreview(TaskbarWindow);
        }

        /// <summary>
        /// Specifies whether a standard window frame will be displayed
        /// around the bitmap.  If the bitmap represents a top-level window,
        /// you would probably set this flag to <b>true</b>.  If the bitmap
        /// represents a child window (or a frameless window), you would
        /// probably set this flag to <b>false</b>.
        /// </summary>
        public bool DisplayFrameAroundBitmap
        {
            get;
            set;
        }

        /// <summary>
        /// Invalidate any existing thumbnail preview. Calling this method
        /// will force DWM to request a new bitmap next time user previews the thumbnails
        /// or requests Aero peek preview.
        /// </summary>
        public void InvalidatePreview()
        {
            // invalidate the thumbnail bitmap
            if (TaskbarWindowManager.Instance != null)
            {
                TaskbarWindowManager.Instance.InvalidatePreview(TaskbarWindow);
                SetImage(IntPtr.Zero);
            }
        }

        #endregion

    }
}

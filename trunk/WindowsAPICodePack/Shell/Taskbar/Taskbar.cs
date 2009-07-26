//Copyright (c) Microsoft Corporation.  All rights reserved.

using System;
using System.Windows.Forms;
using System.Windows.Interop;

namespace Microsoft.WindowsAPICodePack.Shell.Taskbar
{
    /// <summary>
    /// Represents an instance of the Windows 7 taskbar
    /// </summary>
    public static class Taskbar
    {
        // Best practice recommends defining a private object to lock on
        private static Object syncLock = new Object();

        // Internal implemenation of ITaskbarList4 interface
        private static ITaskbarList4 taskbarList;
        internal static ITaskbarList4 TaskbarList
        {
            get
            {
                if (taskbarList == null)
                {
                    // Create a new instance of ITaskbarList3
                    lock (syncLock)
                    {
                        if (taskbarList == null)
                        {
                            taskbarList = (ITaskbarList4)new CTaskbarList();
                            taskbarList.HrInit();
                        }
                    }
                }

                return taskbarList;
            }
        }

        private static JumpList jumpList;
        /// <summary>
        /// Represents a taskbar's jumplist.
        /// </summary>
        public static JumpList JumpList
        {
            get
            {
                CoreHelpers.ThrowIfNotWin7();

                if (jumpList == null)
                {
                    lock (syncLock)
                    {
                        if (jumpList == null)
                        {
                            jumpList = new JumpList(AppId);
                        }
                    }
                }

                return jumpList;
            }
        }

        private static ProgressBar progressBar;
        /// <summary>
        /// Represents a taskbar button's progress bar feature.
        /// </summary>
        public static ProgressBar ProgressBar
        {
            get
            {
                CoreHelpers.ThrowIfNotWin7();

                if (progressBar == null)
                {
                    lock (syncLock)
                    {
                        if (progressBar == null)
                            progressBar = new ProgressBar();
                    }
                }

                return progressBar;
            }
        }

        private static MultipleViewProgressBar multipleViewProgressBar;
        /// <summary>
        /// Represents a taskbar button’s progress bar feature that is associated 
        /// with multiple windows. Only one progress bar will get displayed on the taskbar, 
        /// but states for different windows can be maintained.
        /// </summary>
        public static MultipleViewProgressBar MultipleViewProgressBar
        {
            get
            {
                CoreHelpers.ThrowIfNotWin7();

                if (multipleViewProgressBar == null)
                {
                    lock (syncLock)
                    {
                        if (multipleViewProgressBar == null)
                            multipleViewProgressBar = new MultipleViewProgressBar();
                    }
                }

                return multipleViewProgressBar;
            }
        }

        private static OverlayImage overlayImage;
        /// <summary>
        /// Gets or sets the OverlayImage that is used to render an overlay
        /// image on the button-right hand corner of this application's
        /// taskbar button.
        /// </summary>
        public static OverlayImage OverlayImage
        {
            get
            {
                CoreHelpers.ThrowIfNotWin7();

                // If OverlayImage is not yet initialized, create a default one
                if (overlayImage == null)
                    overlayImage = new OverlayImage(null, string.Empty);

                return overlayImage;
            }
            set
            {
                if (Taskbar.OwnerHandle == IntPtr.Zero)
                    throw new InvalidOperationException("Taskbar Overlay Image cannot be set without an open Form.");

                overlayImage = value;

                if (overlayImage == null || overlayImage.Icon == null)
                {
                    TaskbarList.SetOverlayIcon(OwnerHandle, IntPtr.Zero, string.Empty);
                }
                else
                {
                    TaskbarList.SetOverlayIcon(OwnerHandle, overlayImage.Icon.Handle, overlayImage.Text);
                }
            }
        }

        private static TabbedThumbnail tabbedThumbnail;
        /// <summary>
        /// Gets the Tabbed Thumbnail manager class for adding/updating
        /// tabbed thumbnail previews.
        /// </summary>
        public static TabbedThumbnail TabbedThumbnail
        {
            get
            {
                CoreHelpers.ThrowIfNotWin7();

                if (tabbedThumbnail == null)
                    tabbedThumbnail = new TabbedThumbnail();

                return tabbedThumbnail;
            }
        }

        private static ThumbnailToolbarManager thumbnailToolbarManager;
        /// <summary>
        /// Gets the Thumbnail toolbar manager class for adding/updating
        /// toolbar buttons.
        /// </summary>
        public static ThumbnailToolbarManager ThumbnailToolbars
        {
            get
            {
                CoreHelpers.ThrowIfNotWin7();

                if (thumbnailToolbarManager == null)
                    thumbnailToolbarManager = new ThumbnailToolbarManager();

                return thumbnailToolbarManager;
            }
        }

        /// <summary>
        /// Gets or sets the application user model id. Use this to explicitly
        /// set the application id when generating custom jump lists.
        /// </summary>
        public static string AppId
        {
            get
            {
                CoreHelpers.ThrowIfNotWin7();

                return GetCurrentProcessAppId();
            }
            set
            {
                SetCurrentProcessAppId(value);
            }
        }

        private static IntPtr ownerHandle;
        /// <summary>
        /// Sets the handle of the window whose taskbar button will be used
        /// to display progress.
        /// </summary>
        internal static IntPtr OwnerHandle
        {
            get
            {
                if (ownerHandle == IntPtr.Zero)
                {
                    if (Application.OpenForms.Count > 0)
                        ownerHandle = Application.OpenForms[0].Handle;
                    else if (System.Windows.Application.Current != null) // could be a WPF app
                        ownerHandle = new WindowInteropHelper(System.Windows.Application.Current.MainWindow).Handle;
                    else
                        throw new InvalidOperationException("A valid active Window is needed to update the Taskbar");
                }

                return ownerHandle;
            }
        }

        /// <summary>
        /// Sets the current process' explicit application user model id.
        /// </summary>
        /// <param name="appId">The application id.</param>
        private static void SetCurrentProcessAppId(string appId)
        {
            TaskbarNativeMethods.SetCurrentProcessExplicitAppUserModelID(appId);
        }

        /// <summary>
        /// Gets the current process' explicit application user model id.
        /// </summary>
        /// <returns>The app id or null if no app id has been defined.</returns>
        private static string GetCurrentProcessAppId()
        {
            string appId = string.Empty;
            TaskbarNativeMethods.GetCurrentProcessExplicitAppUserModelID(out appId);
            return appId;
        }
    }
}

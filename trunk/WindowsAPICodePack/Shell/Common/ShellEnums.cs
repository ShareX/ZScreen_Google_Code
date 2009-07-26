//Copyright (c) Microsoft Corporation.  All rights reserved.

using System;

namespace Microsoft.WindowsAPICodePack.Shell
{
    /// <summary>
    /// CommonFileDialog AddPlace locations
    /// </summary>
    public enum FileDialogAddPlaceLocation
    {
        /// <summary>
        /// At the bottom of the Favorites or Places list.
        /// </summary>
        Bottom = 0x00000000,

        /// <summary>
        /// At the top of the Favorites or Places list.
        /// </summary>
        Top = 0x00000001,
    }

    /// <summary>
    /// One of the values that indicates how the ShellObject DisplayName should look.
    /// </summary>
    public enum DisplayNameType : uint
    {
        /// <summary>
        /// Returns the display name relative to the desktop.
        /// </summary>
        Normal = 0x00000000,

        /// <summary>
        /// Returns the parsing name relative to the parent folder.
        /// </summary>
        RelativeToParent = 0x80018001,

        /// <summary>
        /// Returns the path relative to the parent folder in a 
        /// friendly format as displayed in an address bar.
        /// </summary>
        RelativeToParentAddressBar = 0x8007c001,

        /// <summary>
        /// Returns the parsing name relative to the desktop.
        /// </summary>
        RelativeToDesktop = 0x80028000,

        /// <summary>
        /// Returns the editing name relative to the parent folder.
        /// </summary>
        RelativeToParentEditing = 0x80031001,

        /// <summary>
        /// Returns the editing name relative to the desktop.
        /// </summary>
        RelativeToDesktopEditing = 0x8004c000,

        /// <summary>
        /// Returns the display name relative to the file system path.
        /// </summary>
        FileSystemPath = 0x80058000,

        /// <summary>
        /// Returns the display name relative to a URL.
        /// </summary>
        Url = 0x80068000,
    }
    /// <summary>
    /// Available Library folder types
    /// </summary>
    public enum LibraryFolderType
    {
        /// <summary>
        /// General Items
        /// </summary>
        Generic = 0,

        /// <summary>
        /// Documents
        /// </summary>
        Documents,

        /// <summary>
        /// Music
        /// </summary>
        Music,

        /// <summary>
        /// Pictures
        /// </summary>
        Pictures,

        /// <summary>
        /// Videos
        /// </summary>
        Videos
        
    }

    /// <summary>
    /// Flags controlling the appearance of a window
    /// </summary>
    public enum WindowShowCommand : uint
    {
        /// <summary>
        /// Hides the window and activates another window.
        /// </summary>
        Hide = 0,

        /// <summary>
        /// Activates and displays the window (including restoring
        /// it to its original size and position).
        /// </summary>
        Normal = 1,

        /// <summary>
        /// Minimizes the window.
        /// </summary>
        Minimized = 2,

        /// <summary>
        /// Maximizes the window.
        /// </summary>
        Maximized = 3,

        /// <summary>
        /// Similar to <see cref="Normal"/>, except that the window
        /// is not activated.
        /// </summary>
        ShowNoActivate = 4,

        /// <summary>
        /// Activates the window and displays it in its current size
        /// and position.
        /// </summary>
        Show = 5,

        /// <summary>
        /// Minimizes the window and activates the next top-level window.
        /// </summary>
        Minimize = 6,

        /// <summary>
        /// Minimizes the window and does not activate it.
        /// </summary>
        ShowMinimizedNoActivate = 7,

        /// <summary>
        /// Similar to <see cref="Normal"/>, except that the window is not
        /// activated.
        /// </summary>
        ShowNA = 8,

        /// <summary>
        /// Activates and displays the window, restoring it to its original
        /// size and position.
        /// </summary>
        Restore = 9,

        /// <summary>
        /// Sets the show state based on the initial value specified when
        /// the process was created.
        /// </summary>
        Default = 10,

        /// <summary>
        /// Minimizes a window, even if the thread owning the window is not
        /// responding.  Use this only to minimize windows from a different
        /// thread.
        /// </summary>
        ForceMinimize = 11
    }


}

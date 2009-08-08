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
        Default = 0x00000000,

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

    /// <summary>
    /// Provides a set of flags to be used with <see cref="Microsoft.WindowsAPICodePack.Shell.SearchCondition"/> 
    /// to indicate the operation in <see cref="Microsoft.WindowsAPICodePack.Shell.SearchConditionFactory"/> methods.
    /// </summary>
    public enum SearchConditionOperation
    {
        /// <summary>
        /// An implicit comparison between the value of the property and the value of the constant.
        /// </summary>
        Implicit = 0,

        /// <summary>
        /// The value of the property and the value of the constant must be equal.
        /// </summary>
        Equal = 1,

        /// <summary>
        /// The value of the property and the value of the constant must not be equal.
        /// </summary>
        NotEqual = 2,

        /// <summary>
        /// The value of the property must be less than the value of the constant.
        /// </summary>
        LessThan = 3,

        /// <summary>
        /// The value of the property must be greater than the value of the constant.
        /// </summary>
        GreaterThan = 4,

        /// <summary>
        /// The value of the property must be less than or equal to the value of the constant.
        /// </summary>
        LessThanOrEqual = 5,

        /// <summary>
        /// The value of the property must be greater than or equal to the value of the constant.
        /// </summary>
        GreaterThanOrEqual = 6,

        /// <summary>
        /// The value of the property must begin with the value of the constant.
        /// </summary>
        ValueStartsWith = 7,

        /// <summary>
        /// The value of the property must end with the value of the constant.
        /// </summary>
        ValueEndsWith = 8,

        /// <summary>
        /// The value of the property must contain the value of the constant.
        /// </summary>
        ValueContains = 9,

        /// <summary>
        /// The value of the property must not contain the value of the constant.
        /// </summary>
        ValueNotContains = 10,

        /// <summary>
        /// The value of the property must match the value of the constant, where '?' 
        /// matches any single character and '*' matches any sequence of characters.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "DOS")]
        DOSWildcards = 11,

        /// <summary>
        /// The value of the property must contain a word that is the value of the constant.
        /// </summary>
        WordEqual = 12,

        /// <summary>
        /// The value of the property must contain a word that begins with the value of the constant.
        /// </summary>
        WordStartsWith = 13,

        /// <summary>
        /// The application is free to interpret this in any suitable way.
        /// </summary>
        ApplicationSpecific = 14
    }

    /// <summary>
    /// Set of flags to be used with <see cref="Microsoft.WindowsAPICodePack.Shell.SearchConditionFactory"/>.
    /// </summary>
    public enum SearchConditionType
    {
        /// <summary>
        /// Indicates that the values of the subterms are combined by "AND".
        /// </summary>
        And = 0,

        /// <summary>
        /// Indicates that the values of the subterms are combined by "OR".
        /// </summary>
        Or = 1,

        /// <summary>
        /// Indicates a "NOT" comparison of subterms.
        /// </summary>
        Not = 2,

        /// <summary>
        /// Indicates that the node is a comparison between a property and a 
        /// constant value using a <see cref="Microsoft.WindowsAPICodePack.Shell.SearchConditionOperation"/>.
        /// </summary>
        Leaf = 3,
    }

    /// <summary>
    /// Used to describe the view mode.
    /// </summary>
    public enum FolderLogicalViewMode
    {
        /// <summary>
        /// The view is not specified.
        /// </summary>
        Unspecified = -1,

        /// <summary>
        /// The minimum valid enumeration value. Used for validation purposes only.
        /// </summary>
        First = 1,

        /// <summary>
        /// Details view.
        /// </summary>
        Details = 1,

        /// <summary>
        /// Tiles view.
        /// </summary>
        Tiles = 2,

        /// <summary>
        /// Icons view.
        /// </summary>
        Icons = 3,

        /// <summary>
        /// Windows 7 and later. List view.
        /// </summary>
        List = 4,

        /// <summary>
        /// Windows 7 and later. Content view.
        /// </summary>
        Content = 5,

        /// <summary>
        /// The maximum valid enumeration value. Used for validation purposes only.
        /// </summary>
        Last = 5
    }

    /// <summary>
    /// The direction in which the items are sorted.
    /// </summary>
    public enum SortDirection
    {
        /// <summary>
        /// The items are sorted in descending order. Whether the sort is alphabetical, numerical, 
        /// and so on, is determined by the data type of the column indicated in propkey.
        /// </summary>
        Descending = -1,

        /// <summary>
        /// The items are sorted in ascending order. Whether the sort is alphabetical, numerical, 
        /// and so on, is determined by the data type of the column indicated in propkey.
        /// </summary>
        Ascending = 1,
    }
}

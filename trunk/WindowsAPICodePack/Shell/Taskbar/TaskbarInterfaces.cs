//Copyright (c) Microsoft Corporation.  All rights reserved.

namespace Microsoft.WindowsAPICodePack.Shell.Taskbar
{
    /// <summary>
    /// Interface for jump list items
    /// </summary>
    public interface IJumpListItem
    {
        /// <summary>
        /// Gets or sets this item's path
        /// </summary>
        string Path { get; set; }

        /// <summary>
        /// Gets a shell representation of this object
        /// </summary>
        /// <returns>Shell representation of this object</returns>
        object GetShellRepresentation();
    }

    /// <summary>
    /// Interface for jump list tasks
    /// </summary>
    public interface IJumpListTask
    {
        /// <summary>
        /// Gets a shell representation of this object
        /// </summary>
        /// <returns>Shell representation of this object</returns>
        object GetShellRepresentation();
    }
}

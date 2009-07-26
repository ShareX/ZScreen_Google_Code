// Copyright (c) Microsoft Corporation.  All rights reserved.

using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.WindowsAPICodePack.Shell
{
    /// <summary>
    /// Represents the different retrieval options for the thumbnail/icon.
    /// Includes flags for extracting the thumbnail/icon from the file, 
    /// accessing the cache only, or from memory only.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1027:MarkEnumsWithFlags", Justification = "The members of this enum do not represent flags")]
    public enum ShellThumbnailRetrievalOptions
    {
        /// <summary>
        /// The default behavior is to load a thumbnail. If there is no thumbnail for the current ShellItem, it 
        /// retrieves the icon of the item. The thumbnail or icon is extracted if it is not currently cached.
        /// </summary>
        Default,

        /// <summary>
        /// Allows access to the disk, but only to retrieve a cached item. This returns a cached thumbnail if 
        /// it is available. If no cached thumbnail is available, it returns a cached per-instance icon but does 
        /// not extract a thumbnail or icon.
        /// </summary>
        CacheOnly = ShellNativeMethods.SIIGBF.SIIGBF_INCACHEONLY,

        /// <summary>
        /// Return only the item if it is in memory. Do not access the disk even if the item is cached. 
        /// Note that this only returns an already-cached icon and can fall back to a per-class icon if 
        /// an item has a per-instance icon that has not been cached yet. Retrieving a thumbnail, 
        /// even if it is cached, always requires the disk to be accessed, so this method should not be 
        /// called from the user interface (UI) thread without passing ShellThumbnailCacheOptions.MemoryOnly
        /// </summary>
        MemoryOnly = ShellNativeMethods.SIIGBF.SIIGBF_MEMORYONLY,
    }

    /// <summary>
    /// 
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1027:MarkEnumsWithFlags", Justification = "The members of this enum do not represent flags")]
    public enum ShellThumbnailFormatOptions
    {
        /// <summary>
        /// The default behavior is to load a thumbnail. If there is no thumbnail for the current Shell Item, 
        /// it retrieves an HBITMAP for the icon of the item.
        /// </summary>
        Default,

        /// <summary>
        /// Return only the thumbnail, never the icon. Note that not all items have thumbnails 
        /// so ShellThumbnailFormatOptions.ThumbnailOnly can fail in these cases.
        /// </summary>
        ThumbnailOnly = ShellNativeMethods.SIIGBF.SIIGBF_THUMBNAILONLY,
        
        /// <summary>
        /// Return only the icon, never the thumbnail.
        /// </summary>
        IconOnly = ShellNativeMethods.SIIGBF.SIIGBF_ICONONLY,
    }
}

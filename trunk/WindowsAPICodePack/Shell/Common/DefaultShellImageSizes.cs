// Copyright (c) Microsoft Corporation.  All rights reserved.

using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.WindowsAPICodePack.Shell
{
    /// <summary>
    /// Defines read-only properties for default shell icon sizes
    /// </summary>
    public static class DefaultIconSize
    {
        /// <summary>
        /// Gets the small size (read-only) property for a Shell Icon. 
        /// The small size is set to 16x16.
        /// </summary>
        public static readonly System.Windows.Size Small = new System.Windows.Size(16, 16);

        /// <summary>
        /// Gets the medium size (read-only) property for a Shell Icon. 
        /// The medium size is set to 32x32.
        /// </summary>
        public static readonly System.Windows.Size Medium = new System.Windows.Size(32, 32);

        /// <summary>
        /// Gets the large size (read-only) property for a Shell Icon. 
        /// The large size is set to 48x48.
        /// </summary>
        public static readonly System.Windows.Size Large = new System.Windows.Size(48, 48);

        /// <summary>
        /// Gets the extra-large size (read-only) property for a Shell Icon. 
        /// The extra-large size is set to 256x256.
        /// </summary>
        public static readonly System.Windows.Size ExtraLarge = new System.Windows.Size(256, 256);

        /// <summary>
        /// Maximum size for the Shell Icon (256x256)
        /// </summary>
        public static readonly System.Windows.Size Maximum = new System.Windows.Size(256, 256);

    }

    /// <summary>
    /// Defines read-only properties for default shell thumbnail sizes
    /// </summary>
    public static class DefaultThumbnailSize
    {
        /// <summary>
        /// Gets the small size (read-only) property for a Shell Thumbnail. 
        /// The small size is set to 32x32.
        /// </summary>
        public static readonly System.Windows.Size Small = new System.Windows.Size(32, 32);

        /// <summary>
        /// Gets the medium size (read-only) property for a Shell Thumbnail. 
        /// The medium size is set to 96x96.
        /// </summary>
        public static readonly System.Windows.Size Medium = new System.Windows.Size(96, 96);

        /// <summary>
        /// Gets the large size (read-only) property for a Shell Thumbnail. 
        /// The large size is set to 256x256.
        /// </summary>
        public static readonly System.Windows.Size Large = new System.Windows.Size(256, 256);

        /// <summary>
        /// Gets the extra-large size (read-only) property for a Shell Thumbnail. 
        /// The extra-large size is set to 1024x1024.
        /// </summary>
        public static readonly System.Windows.Size ExtraLarge = new System.Windows.Size(1024, 1024);

        /// <summary>
        /// Maximum size for the Shell Thumbnail (1024x1024)
        /// </summary>
        public static readonly System.Windows.Size Maximum = new System.Windows.Size(1024, 1024);
    }
}

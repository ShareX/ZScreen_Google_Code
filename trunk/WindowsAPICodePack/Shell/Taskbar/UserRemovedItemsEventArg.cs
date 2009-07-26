//Copyright (c) Microsoft Corporation.  All rights reserved.

using System;

namespace Microsoft.WindowsAPICodePack.Shell.Taskbar
{
    /// <summary>
    /// Event arguments for when the user is notified of items
    /// that have been removed from the taskbar destination list
    /// </summary>
    public class UserRemovedItemsEventArgs : EventArgs
    {
        readonly string[] removedItems;

        internal UserRemovedItemsEventArgs(string[] RemovedItems)
        {
            removedItems = RemovedItems;
        }

        /// <summary>
        /// The collection of removed items based on path.
        /// </summary>
        public string[] GetRemovedItems()
        {
            return removedItems;
        }
    }
}

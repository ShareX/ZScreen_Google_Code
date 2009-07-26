//Copyright (c) Microsoft Corporation.  All rights reserved.

using System;
using System.Collections.Specialized;
using System.Runtime.InteropServices;
using System.Text;

namespace Microsoft.WindowsAPICodePack.Shell.Taskbar
{
    /// <summary>
    /// Represents an instance of a Taskbar button jump list.
    /// </summary>
    public class JumpList
    {
        // Best practice recommends defining a private object to lock on
        private static Object syncLock = new Object();

        // Native implementation of destination list
        private ICustomDestinationList customDestinationList;

        #region Properties

        private CustomCategoryCollection customCategories;
        /// <summary>
        /// Returns the collection of custom categories that have been added to
        /// the Taskbar jump list.
        /// </summary>
        public CustomCategoryCollection CustomCategories
        {
            get
            {
                if (customCategories == null)
                {
                    // Make sure that we don't create multiple instances
                    // of this object
                    lock (syncLock)
                    {
                        if (customCategories == null)
                        {
                            customCategories = new CustomCategoryCollection();
                        }
                    }
                }

                return customCategories;
            }
        }

        private JumpListItemCollection<IJumpListTask> userTasks;
        /// <summary>
        /// Gets the collection of user tasks that have been added to the Taskbar jump
        /// list. User tasks can only consist of JumpListTask or
        /// JumpListSeparator objects.
        /// </summary>
        public JumpListItemCollection<IJumpListTask> UserTasks
        {
            get
            {
                if (userTasks == null)
                {
                    // Make sure that we don't create multiple instances
                    // of this object
                    lock (syncLock)
                    {
                        if (userTasks == null)
                        {
                            userTasks = new JumpListItemCollection<IJumpListTask>();
                        }
                    }

                }

                return userTasks;
            }
        }

        /// <summary>
        /// Gets the recommended number of items to add to the jump list.  
        /// </summary>
        /// <remarks>
        /// This number doesn’t 
        /// imply or suggest how many items will appear on the jump list.  
        /// This number should only be used for reference purposes since
        /// the actual number of slots in the jump list can change after the last
        /// refresh due to items being pinned or removed and resolution changes. 
        /// The jump list can increase in size accordingly.
        /// </remarks>
        public uint MaxSlotsInList
        {
            get 
            { 
                // Because we need the correct number for max slots, start a commit, get the max slots
                // and then abort. If we wait until the user calls RefreshTaskbarlist(), it will be too late.
                // The user needs to use this number before they update the jumplist.

                object removedItems;
                uint maxSlotsInList = 10; // default

                // Native call to start adding items to the taskbar destination list
                HRESULT hr = customDestinationList.BeginList(
                    out maxSlotsInList,
                    ref TaskbarNativeMethods.IID_IObjectArray,
                    out removedItems);

                if(CoreErrorHelper.Succeeded((int)hr))
                    customDestinationList.AbortList();

                return maxSlotsInList;
            }
        }

        /// <summary>
        /// Gets or sets the type of known categories to display.
        /// </summary>
        public KnownCategoryType KnownCategoryToDisplay { get; set; }

        private int knownCategoryOrdinalPosition = 0;
        /// <summary>
        /// Gets or sets the value for the known category location relative to the 
        /// custom category collection.
        /// </summary>
        public int KnownCategoryOrdinalPosition
        {
            get
            {
                return knownCategoryOrdinalPosition;
            }
            set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException("value", "Negative numbers are not allowed for the ordinal position.");

                knownCategoryOrdinalPosition = value;
            }

        }

        /// <summary>
        /// Gets or sets the application ID to use for this jump list.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "This is a public property that can be used by the application calling our library")]
        internal string AppID { get; set; }

        #endregion

        /// <summary>
        /// Creates a new instance of the JumpList class with the specified
        /// appId.
        /// </summary>
        /// <param name="appID">Application ID to use for this instace.</param>
        internal JumpList(string appID)
        {
            // Native implementation of destination list
            customDestinationList = (ICustomDestinationList)new CDestinationList();

            // Set application user model ID
            AppID = appID;
        }

        /// <summary>
        /// Reports document usage to the shell.
        /// </summary>
        /// <param name="destination">The full path of the file to report usage.</param>
        public void AddToRecent(string destination)
        {
            TaskbarNativeMethods.SHAddToRecentDocs(destination);
        }

        /// <summary>
        /// Commits the pending JumpList changes and refreshes the Taskbar.
        /// </summary>
        public void RefreshTaskbarList()
        {
            // Begins rendering on the taskbar destination list
            BeginList();

            // Add custom categories
            AppendCustomCategories();

            // Add user tasks
            AppendTaskList();

            // End rendering of the taskbar destination list
            customDestinationList.CommitList();
        }

        private void BeginList()
        {
            // Get list of removed items from native code
            object removedItems;
            uint maxSlotsInList = 10; // default

            // Native call to start adding items to the taskbar destination list
            HRESULT hr = customDestinationList.BeginList(
                out maxSlotsInList,
                ref TaskbarNativeMethods.IID_IObjectArray,
                out removedItems);

            if (!CoreErrorHelper.Succeeded((int)hr))
                Marshal.ThrowExceptionForHR((int)hr);

            // Process the deleted items
            string[] removedItemsArray = ProcessDeletedItems((IObjectArray)removedItems);

            // Raise the event if items were removed
            if (JumpListItemsRemoved != null && removedItemsArray != null && removedItemsArray.Length > 0)
                JumpListItemsRemoved(this, new UserRemovedItemsEventArgs(removedItemsArray));
        }

        /// <summary>
        /// Occurs when items are removed from the Taskbar's jump list since the last
        /// refresh. 
        /// </summary>
        /// <remarks>
        /// This event is not triggered
        /// immediately when a user removes an item from the jump list but rather
        /// when the application refreshes the task bar list directly.
        /// </remarks>
        public event EventHandler<UserRemovedItemsEventArgs> JumpListItemsRemoved = delegate { };

        /// <summary>
        /// Retrieves the current list of destinations that have been removed from the existing jump list by the user. 
        /// The removed destinations may become items on a custom jump list.
        /// </summary>
        /// <returns>A collection of items (filenames) removed from the existing jump list by the user.</returns>
        public string[] GetRemovedDestinations()
        {
            // Get list of removed items from native code
            object removedItems;

            customDestinationList.GetRemovedDestinations(ref TaskbarNativeMethods.IID_IObjectArray, out removedItems);

            return ProcessDeletedItems((IObjectArray)removedItems);
        }

        private string[] ProcessDeletedItems(IObjectArray removedItems)
        {
            uint count;
            removedItems.GetCount(out count);

            if (count == 0)
                return new string[] { };

            // String array passed to the user via the JumpListItemsRemoved
            // event
            string[] removedItemsArray = new string[count];

            // Process each removed item based on it's type
            for (uint i = 0; i < count; i++)
            {
                // Native call to retrieve objects from IObjectArray
                object item;
                removedItems.GetAt(i,
                    ref TaskbarNativeMethods.IID_IUnknown,
                    out item);

                // Process item
                if (item is IShellItem)
                {
                    removedItemsArray[i] = RemoveCustomCategoryItem((IShellItem)item);
                }
                else if (item is IShellLinkW)
                {
                    removedItemsArray[i] = RemoveCustomCategoryLink((IShellLinkW)item);
                }
            }

            return removedItemsArray;
        }

        private string RemoveCustomCategoryItem(IShellItem item)
        {
            string path = null;
            IntPtr pszString = IntPtr.Zero;
            HRESULT hr = item.GetDisplayName(ShellNativeMethods.SIGDN.SIGDN_FILESYSPATH, out pszString);
            if (hr == HRESULT.S_OK || pszString != IntPtr.Zero)
                path = Marshal.PtrToStringAuto(pszString);

            // Remove this item from each category
            foreach (CustomCategory category in this.CustomCategories)
                category.RemoveJumpListItem(path);

            return path;
        }


        private string RemoveCustomCategoryLink(IShellLinkW link)
        {
            StringBuilder sb = new StringBuilder(256);
            link.GetPath(sb, sb.Capacity, IntPtr.Zero, 2);

            string path = sb.ToString();

            // Remove this item from each category
            foreach (CustomCategory category in CustomCategories)
                category.RemoveJumpListItem(path);

            return path;
        }

        private void AppendCustomCategories()
        {
            // Initialize our current index in the custom categories list
            int currentIndex = 0;

            // Keep track whether we add the Known Categories to our list
            bool knownCategoriesAdded = false;

            // Append each category to list
            foreach (CustomCategory category in CustomCategories)
            {
                // If our current index is same as the KnownCategory OrdinalPosition,
                // append the Known Categories
                if (!knownCategoriesAdded && currentIndex == KnownCategoryOrdinalPosition)
                {
                    AppendKnownCategories();
                    knownCategoriesAdded = true;
                }

                // Don't process empty categories
                if (category.JumpListItems.Count == 0)
                    continue;

                IObjectCollection categoryContent =
                    (IObjectCollection)new CEnumerableObjectCollection();

                // Add each link's shell representation to the object array
                foreach (IJumpListItem link in category.JumpListItems)
                    categoryContent.AddObject(link.GetShellRepresentation());

                // Add current category to destination list
                HRESULT hr = customDestinationList.AppendCategory(
                    category.Name,
                    (IObjectArray)categoryContent);

                if(!CoreErrorHelper.Succeeded((int)hr))
                {
                    if ((uint)hr == 0x80040F03)
                        throw new InvalidOperationException("The file type is not registered with this application.");
                    else
                        Marshal.ThrowExceptionForHR((int)hr);
                }
                
                // Increase our current index
                currentIndex++;
            }

            // If the ordinal position was out of range, append the Known Categories
            // at the end
            if (!knownCategoriesAdded)
                AppendKnownCategories();
        }

        private void AppendTaskList()
        {
            if (UserTasks.Count == 0)
                return;

            IObjectCollection taskContent =
                    (IObjectCollection)new CEnumerableObjectCollection();

            // Add each task's shell representation to the object array
            foreach (IJumpListTask task in UserTasks)
                taskContent.AddObject(task.GetShellRepresentation());

            // Add tasks to the taskbar
            HRESULT hr = customDestinationList.AddUserTasks((IObjectArray)taskContent);

            if (!CoreErrorHelper.Succeeded((int)hr))
            {
                if ((uint)hr == 0x80040F03)
                    throw new InvalidOperationException("The file type is not registered with this application.");
                else
                    Marshal.ThrowExceptionForHR((int)hr);
            }
        }

        private void AppendKnownCategories()
        {
            if (KnownCategoryToDisplay == KnownCategoryType.Recent)
                customDestinationList.AppendKnownCategory(KNOWNDESTCATEGORY.KDC_RECENT);
            else if (KnownCategoryToDisplay == KnownCategoryType.Frequent)
                customDestinationList.AppendKnownCategory(KNOWNDESTCATEGORY.KDC_FREQUENT);
        }
    }
}

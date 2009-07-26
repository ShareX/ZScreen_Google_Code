// Copyright (c) Microsoft Corporation.  All rights reserved.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.IO;
using System.Diagnostics;

namespace Microsoft.WindowsAPICodePack.Shell
{
    internal static class ShellObjectFactory
    {
        /// <summary>
        /// Creates a ShellObject given a native IShellItem interface
        /// </summary>
        /// <param name="nativeShellItem"></param>
        /// <returns>A newly constructed ShellObject object</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1304:SpecifyCultureInfo", MessageId = "System.String.ToLower", Justification = "We are not currently handling globalization or localization")]
        internal static ShellObject Create(IShellItem nativeShellItem)
        {
            // Sanity check
            Debug.Assert(nativeShellItem != null, "nativeShellItem should not be null");

            // A lot of APIs need IShellItem2, so just keep a copy of it here
            IShellItem2 nativeShellItem2 = nativeShellItem as IShellItem2;

            // Get the System.ItemType property
            string itemType = ShellHelper.GetItemType(nativeShellItem2);

            if (!string.IsNullOrEmpty(itemType))
                itemType = itemType.ToLower();

            // Get the System.ParsingName property
            string parsingName = ShellHelper.GetParsingName(nativeShellItem2);

            // Get some IShellItem attributes
            ShellNativeMethods.SFGAO sfgao;
            nativeShellItem2.GetAttributes(ShellNativeMethods.SFGAO.SFGAO_FILESYSTEM | ShellNativeMethods.SFGAO.SFGAO_FOLDER, out sfgao);

            // Is this item a FileSystem item?
            bool isFileSystem = (sfgao & ShellNativeMethods.SFGAO.SFGAO_FILESYSTEM) != 0;

            // Is this item a Folder?
            bool isFolder = (sfgao & ShellNativeMethods.SFGAO.SFGAO_FOLDER) != 0;

            // Is this item a Link?
            bool isLink = (itemType == ".lnk"); // lowercase comparison

            // Is this a Library?
            ShellLibrary shellLibrary = null;
            bool isLibrary = (itemType == ".library-ms") && (shellLibrary = ShellLibrary.FromShellItem(nativeShellItem2, true)) != null;

            // Is this a Search Connector
            bool isSearchConnector = (itemType == ".searchconnector-ms"); // lowercase comparison

            // Is this a Saved Search (or Search Folder)
            bool isSearchFolder = (itemType == ".search-ms");  // lowercase comparison

            // PIDL for the IShellItem. If we do retrive this, save it so we can set it later
            IntPtr pidl = IntPtr.Zero;

            // For KnownFolders
            bool isKnownFolderVirtual = false;

            // Create the right type of ShellObject based on the above information 

            // 1. First check if this is a Shell Link
            if (isLink)
            {
                return new ShellLink(nativeShellItem2);
            }
            // 2. Check if this is a container or a single item (entity)
            else if (isFolder)
            {
                // 3. If this is a folder, check for types: Shell Library, Shell Folder or Search Container
                if (isLibrary)
                    return shellLibrary; // we already created this above while checking for Library
                else if (isSearchConnector)
                    return new SearchConnector(nativeShellItem2);
                else if (isSearchFolder)
                    return new SavedSearch(nativeShellItem2);
                else
                {
                    // 4. It's a ShellFolder
                    if (isFileSystem)
                    {
                        // 5. Is it a (File-System / Non-Virtual) Known Folder
                        if ((GetNativeKnownFolder(nativeShellItem2, out pidl, out isKnownFolderVirtual) != null) && !isKnownFolderVirtual)
                        {
                            FileSystemKnownFolder kf = new FileSystemKnownFolder(nativeShellItem2);
                            kf.PIDL = pidl;
                            return kf;
                        }
                        else
                            return new FileSystemFolder(nativeShellItem2);
                    }
                    else
                    {
                        // 5. Is it a (Non File-System / Virtual) Known Folder
                        if ((GetNativeKnownFolder(nativeShellItem2, out pidl, out isKnownFolderVirtual) != null) && isKnownFolderVirtual)
                        {
                            NonFileSystemKnownFolder kf = new NonFileSystemKnownFolder(nativeShellItem2);
                            kf.PIDL = pidl;
                            return kf;
                        }
                        else
                            return new NonFileSystemFolder(nativeShellItem2);
                    }

                }

            }
            else
            {
                // 6. If this is an entity (single item), check if its filesystem or not
                if (isFileSystem)
                    return new ShellFile(nativeShellItem2);
                else
                    return new NonFileSystemItem(nativeShellItem2);

            }
        }

        private static IKnownFolderNative GetNativeKnownFolder(IShellItem nativeShellItem, out IntPtr pidl, out bool isVirtual)
        {
            // Get teh PIDL for the ShellItem
            pidl = ShellHelper.PidlFromShellItem(nativeShellItem);

            if (pidl == IntPtr.Zero)
            {
                isVirtual = false;
                return null;
            }

            IKnownFolderNative knownFolderNative = KnownFolderHelper.FromPIDL(pidl);

            if (knownFolderNative != null)
            {
                // If we have a valid IKnownFolder, try to get it's category
                KnownFoldersSafeNativeMethods.NativeFolderDefinition nativeFolderDefinition;
                knownFolderNative.GetFolderDefinition(out nativeFolderDefinition);

                // Get the category type and see if it's virtual
                if (nativeFolderDefinition.category == FolderCategory.Virtual)
                    isVirtual = true;
                else
                    isVirtual = false;

                return knownFolderNative;
            }
            else
            {
                // KnownFolderHelper.FromPIDL could not create a valid KnownFolder from the given PIDL.
                // Return null to indicate the given IShellItem is not a KnownFolder. Also set our out parameter
                // to default value.
                isVirtual = false;
                return null;
            }
        }

        /// <summary>
        /// Creates a ShellObject given a parsing name
        /// </summary>
        /// <param name="parsingName"></param>
        /// <returns>A newly constructed ShellObject object</returns>
        internal static ShellObject Create(string parsingName)
        {
            if (string.IsNullOrEmpty(parsingName))
                throw new ArgumentNullException("parsingName");

            // Create a native shellitem from our path
            IShellItem2 nativeShellItem;
            Guid guid = new Guid(ShellIIDGuid.IShellItem2);
            int retCode = ShellNativeMethods.SHCreateItemFromParsingName(parsingName, IntPtr.Zero, ref guid, out nativeShellItem);

            if (CoreErrorHelper.Succeeded(retCode))
            {
                return ShellObjectFactory.Create(nativeShellItem);
            }
            else
            {
                throw new ExternalException("Unable to Create Shell Item.", Marshal.GetExceptionForHR(retCode));
            }
        }

        /// <summary>
        /// Constructs a new Shell object from IDList pointer
        /// </summary>
        /// <param name="idListPtr"></param>
        /// <returns></returns>
        internal static ShellObject Create(IntPtr idListPtr)
        {
            Guid guid = new Guid(ShellIIDGuid.IShellItem2);
            IShellItem2 nativeShellItem;
            int retCode = ShellNativeMethods.SHCreateItemFromIDList(idListPtr, ref guid, out nativeShellItem);
            if (CoreErrorHelper.Succeeded(retCode))
            {
                return ShellObjectFactory.Create(nativeShellItem);
            }
            else
            {
                // Since this is an internal method, return null instead of throwing an exception.
                // Let the caller know we weren't able to create a valid ShellObject with the given PIDL
                return null;
            }
        }

        /// <summary>
        /// Constructs a new Shell object from IDList pointer
        /// </summary>
        /// <param name="idListPtr"></param>
        /// <param name="parent"></param>
        /// <returns></returns>
        internal static ShellObject Create(IntPtr idListPtr, IShellFolder parent)
        {
            IShellItem nativeShellItem;

            int retCode = ShellNativeMethods.SHCreateShellItem(
                IntPtr.Zero,
                parent,
                idListPtr, out nativeShellItem);

            if (CoreErrorHelper.Succeeded(retCode))
            {
                return ShellObjectFactory.Create(nativeShellItem);
            }
            else
            {
                // Since this is an internal method, return null instead of throwing an exception.
                // Let the caller know we weren't able to create a valid ShellObject with the given PIDL
                return null;
            }
        }
    }
}

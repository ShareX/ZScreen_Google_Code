//Copyright (c) Microsoft Corporation.  All rights reserved.

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.InteropServices;
using System.IO;

namespace Microsoft.WindowsAPICodePack.Shell
{
    /// <summary>
    /// Provides access to the metadata for known folders. 
    /// This class contains properties to get the path of 
    /// standard known folders.
    /// </summary>
    public static class KnownFolders
    {
        /// <summary>
        /// Internal cache for all the KnownFolders we create.
        /// </summary>
        private static Dictionary<Guid, IKnownFolder> knownFoldersCache = new Dictionary<Guid, IKnownFolder>();
        
        /// <summary>
        /// Returns a strongly typed read only collection of all the registered Known Folders
        /// </summary>
        public static ICollection<IKnownFolder> All
        {
            get
            {
                return GetAllFolders();
            }
        }

        private static ReadOnlyCollection<IKnownFolder> GetAllFolders()
        {
            // Should this method be thread-safe?? (It'll take a while
            // to get a list of all the known folders, create the managed wrapper
            // and return the read-only collection.

            IList<IKnownFolder> foldersList = new List<IKnownFolder>();
            uint count;
            IntPtr folders;

            IKnownFolderManager knownFolderManager = (IKnownFolderManager)new KnownFolderManagerClass();
            knownFolderManager.GetFolderIds(out folders, out count);

            if (count > 0 && folders != IntPtr.Zero)
            {
                // Loop through all the KnownFolderID elements
                for (int i = 0; i < count; i++)
                {
                    // Read the current pointer
                    IntPtr current = new IntPtr(folders.ToInt32() + (Marshal.SizeOf(typeof(Guid)) * i));

                    // Convert to Guid
                    Guid knownFolderID = (Guid)Marshal.PtrToStructure(current, typeof(Guid));

                    IKnownFolder kf = null;

                    // Get the known folder
                    try
                    {
                        kf = KnownFolderHelper.FromKnownFolderId(knownFolderID);
                    }
                    catch(FileNotFoundException)
                    {
                        // Ignore, don't throw the exception to the user or return null. Just return
                        // them all the valid KnownFolders on the current system.
                    }
                    catch(DirectoryNotFoundException)
                    {
                        // Ignore, don't throw the exception to the user or return null. Just return
                        // them all the valid KnownFolders on the current system.
                    }
                    catch(ArgumentException)
                    {
                        // Ignore, don't throw the exception to the user or return null. Just return
                        // them all the valid KnownFolders on the current system.
                    }

                    // Add to our collection if it's not null (some folders might not exist on the system
                    // or we could have an exception that resulted in the null return from above method call
                    if(kf != null)
                        foldersList.Add(kf);
                }
            }

            Marshal.FreeCoTaskMem(folders);

            return new ReadOnlyCollection<IKnownFolder>(foldersList);
        }

        private static IKnownFolder GetKnownFolder(Guid guid)
        {
            // If we havn't already cached it, then create a new KnownFolder using the GUID
            // and cache it.
            if (!knownFoldersCache.ContainsKey(guid))
            {
                knownFoldersCache.Add(guid,
                    KnownFolderHelper.FromKnownFolderId(guid));
            }
            
            // Return the IKnownFolder from the cache
            return knownFoldersCache[guid];
        }

        #region Default Known Folders

        /// <summary>
        /// Gets the computer folder.
        /// </summary>
        /// <value>A <see cref="IKnownFolder"/> object.</value>
        public static IKnownFolder Computer
        {
            get
            {
                return GetKnownFolder(
                    FolderIdentifiers.Computer);
            }
        }

        /// <summary>
        /// Gets the Windows Vista Synchronization Manager folder.
        /// </summary>
        /// <value>A <see cref="IKnownFolder"/> object.</value>
        public static IKnownFolder Conflict
        {
            get
            {
                return GetKnownFolder(
                    FolderIdentifiers.Conflict);
            }
        }

        /// <summary>
        /// Gets the control panel.
        /// </summary>
        /// <value>A <see cref="IKnownFolder"/> object.</value>
        public static IKnownFolder ControlPanel
        {
            get
            {
                return GetKnownFolder(
                    FolderIdentifiers.ControlPanel);
            }
        }

        /// <summary>
        /// Gets the desktop folder.
        /// </summary>
        /// <value>A <see cref="IKnownFolder"/> object.</value>
        public static IKnownFolder Desktop
        {
            get
            {
                return GetKnownFolder(
                    FolderIdentifiers.Desktop);
            }
        }

        /// <summary>
        /// Gets the internet explorer folder.
        /// </summary>
        /// <value>A <see cref="IKnownFolder"/> object.</value>
        public static IKnownFolder Internet
        {
            get
            {
                return GetKnownFolder(
                    FolderIdentifiers.Internet);
            }
        }

        /// <summary>
        /// Gets the My Network Places folder.
        /// </summary>
        /// <value>A <see cref="IKnownFolder"/> object.</value>
        public static IKnownFolder Network
        {
            get
            {
                return GetKnownFolder(
                    FolderIdentifiers.Network);
            }
        }

        /// <summary>
        /// Gets the Printers and Faxes folder.
        /// </summary>
        /// <value>A <see cref="IKnownFolder"/> object.</value>
        public static IKnownFolder Printers
        {
            get
            {
                return GetKnownFolder(
                    FolderIdentifiers.Printers);
            }
        }

        /// <summary>
        /// Gets the Sync Center folder.
        /// </summary>
        /// <value>A <see cref="IKnownFolder"/> object.</value>
        public static IKnownFolder SyncManager
        {
            get
            {
                return GetKnownFolder(
                    FolderIdentifiers.SyncManager);
            }
        }

        /// <summary>
        /// Gets the network connections folder.
        /// </summary>
        /// <value>A <see cref="IKnownFolder"/> object.</value>
        public static IKnownFolder Connections
        {
            get
            {
                return GetKnownFolder(
                    FolderIdentifiers.Connections);
            }
        }

        /// <summary>
        /// Gets the Synch Setup folder.
        /// </summary>
        /// <value>A <see cref="IKnownFolder"/> object.</value>
        public static IKnownFolder SyncSetup
        {
            get
            {
                return GetKnownFolder(
                    FolderIdentifiers.SyncSetup);
            }
        }

        /// <summary>
        /// Gets the Sync Results folder.
        /// </summary>
        /// <value>A <see cref="IKnownFolder"/> object.</value>
        public static IKnownFolder SyncResults
        {
            get
            {
                return GetKnownFolder(
                    FolderIdentifiers.SyncResults);
            }
        }

        /// <summary>
        /// Gets the recycle bin.
        /// </summary>
        /// <value>A <see cref="IKnownFolder"/> object.</value>
        public static IKnownFolder RecycleBin
        {
            get
            {
                return GetKnownFolder(
                    FolderIdentifiers.RecycleBin);
            }
        }

        /// <summary>
        /// Gets the fonts folder.
        /// </summary>
        /// <value>A <see cref="IKnownFolder"/> object.</value>
        public static IKnownFolder Fonts
        {
            get
            {
                return GetKnownFolder(FolderIdentifiers.Fonts);
            }
        }

        /// <summary>
        /// Gets the startup folder.
        /// </summary>
        /// <value>A <see cref="IKnownFolder"/> object.</value>
        public static IKnownFolder Startup
        {
            get
            {
                return GetKnownFolder(FolderIdentifiers.Startup);
            }
        }

        /// <summary>
        /// Gets the programs folder.
        /// </summary>
        /// <value>A <see cref="IKnownFolder"/> object.</value>
        public static IKnownFolder Programs
        {
            get
            {
                return GetKnownFolder(FolderIdentifiers.Programs);
            }
        }

        /// <summary>
        /// Gets the per-user start menu folder.
        /// </summary>
        /// <value>A <see cref="IKnownFolder"/> object.</value>
        public static IKnownFolder StartMenu
        {
            get
            {
                return GetKnownFolder(FolderIdentifiers.StartMenu);
            }
        }

        /// <summary>
        /// Gets the per-user My Recent Documents folder.
        /// </summary>
        /// <value>A <see cref="IKnownFolder"/> object.</value>
        public static IKnownFolder Recent
        {
            get
            {
                return GetKnownFolder(FolderIdentifiers.Recent);
            }
        }

        /// <summary>
        /// Gets the per-user SendTo folder.
        /// </summary>
        /// <value>A <see cref="IKnownFolder"/> object.</value>
        public static IKnownFolder SendTo
        {
            get
            {
                return GetKnownFolder(FolderIdentifiers.SendTo);
            }
        }

        /// <summary>
        /// Gets the per-user documents folder.
        /// </summary>
        /// <value>A <see cref="IKnownFolder"/> object.</value>
        public static IKnownFolder Documents
        {
            get
            {
                return GetKnownFolder(FolderIdentifiers.Documents);
            }
        }

        /// <summary>
        /// Gets the per-user favorites folder.
        /// </summary>
        /// <value>A <see cref="IKnownFolder"/> object.</value>
        public static IKnownFolder Favorites
        {
            get
            {
                return GetKnownFolder(FolderIdentifiers.Favorites);
            }
        }

        /// <summary>
        /// Gets the network shortcuts folder.
        /// </summary>
        /// <value>A <see cref="IKnownFolder"/> object.</value>
        public static IKnownFolder NetHood
        {
            get
            {
                return GetKnownFolder(FolderIdentifiers.NetHood);
            }
        }

        /// <summary>
        /// Gets the Printer Shortcuts folder.
        /// </summary>
        /// <value>A <see cref="IKnownFolder"/> object.</value>
        public static IKnownFolder PrintHood
        {
            get
            {
                return GetKnownFolder(FolderIdentifiers.PrintHood);
            }
        }

        /// <summary>
        /// Gets the templates folder.
        /// </summary>
        /// <value>A <see cref="IKnownFolder"/> object.</value>
        public static IKnownFolder Templates
        {
            get
            {
                return GetKnownFolder(FolderIdentifiers.Templates);
            }
        }

        /// <summary>
        /// Gets the common startup folder.
        /// </summary>
        /// <value>A <see cref="IKnownFolder"/> object.</value>
        public static IKnownFolder CommonStartup
        {
            get
            {
                return GetKnownFolder(FolderIdentifiers.CommonStartup);
            }
        }

        /// <summary>
        /// Gets the common programs folder.
        /// </summary>
        /// <value>A <see cref="IKnownFolder"/> object.</value>
        public static IKnownFolder CommonPrograms
        {
            get
            {
                return GetKnownFolder(FolderIdentifiers.CommonPrograms);
            }
        }

        /// <summary>
        /// Gets the common start menu folder.
        /// </summary>
        /// <value>A <see cref="IKnownFolder"/> object.</value>
        public static IKnownFolder CommonStartMenu
        {
            get
            {
                return GetKnownFolder(FolderIdentifiers.CommonStartMenu);
            }
        }

        /// <summary>
        /// Gets the public desktop folder.
        /// </summary>
        /// <value>A <see cref="IKnownFolder"/> object.</value>
        public static IKnownFolder PublicDesktop
        {
            get
            {
                return GetKnownFolder(FolderIdentifiers.PublicDesktop);
            }
        }

        /// <summary>
        /// Gets the Application Data folder.
        /// </summary>
        /// <value>A <see cref="IKnownFolder"/> object.</value>
        public static IKnownFolder ProgramData
        {
            get
            {
                return GetKnownFolder(FolderIdentifiers.ProgramData);
            }
        }

        /// <summary>
        /// Gets the common templates folder.
        /// </summary>
        /// <value>A <see cref="IKnownFolder"/> object.</value>
        public static IKnownFolder CommonTemplates
        {
            get
            {
                return GetKnownFolder(FolderIdentifiers.CommonTemplates);
            }
        }

        /// <summary>
        /// Gets the public documents folder.
        /// </summary>
        /// <value>A <see cref="IKnownFolder"/> object.</value>
        public static IKnownFolder PublicDocuments
        {
            get
            {
                return GetKnownFolder(FolderIdentifiers.PublicDocuments);
            }
        }

        /// <summary>
        /// Gets the roaming application data folder.
        /// </summary>
        /// <value>A <see cref="IKnownFolder"/> object.</value>
        public static IKnownFolder RoamingAppData
        {
            get
            {
                return GetKnownFolder(FolderIdentifiers.RoamingAppData);
            }
        }

        /// <summary>
        /// Gets the per-user 
        /// application data folder.
        /// </summary>
        /// <value>A <see cref="IKnownFolder"/> object.</value>
        public static IKnownFolder LocalAppData
        {
            get
            {
                return GetKnownFolder(FolderIdentifiers.LocalAppData);
            }
        }

        /// <summary>
        /// Gets the LocalAppDataLow folder.
        /// </summary>
        /// <value>A <see cref="IKnownFolder"/> object.</value>
        public static IKnownFolder LocalAppDataLow
        {
            get
            {
                return GetKnownFolder(FolderIdentifiers.LocalAppDataLow);
            }
        }

        /// <summary>
        /// Gets the tempory internet files folder.
        /// </summary>
        /// <value>A <see cref="IKnownFolder"/> object.</value>
        public static IKnownFolder InternetCache
        {
            get
            {
                return GetKnownFolder(FolderIdentifiers.InternetCache);
            }
        }

        /// <summary>
        /// Gets the cookies folder.
        /// </summary>
        /// <value>A <see cref="IKnownFolder"/> object.</value>
        public static IKnownFolder Cookies
        {
            get
            {
                return GetKnownFolder(FolderIdentifiers.Cookies);
            }
        }

        /// <summary>
        /// Gets the history folder.
        /// </summary>
        /// <value>A <see cref="IKnownFolder"/> object.</value>
        public static IKnownFolder History
        {
            get
            {
                return GetKnownFolder(FolderIdentifiers.History);
            }
        }

        /// <summary>
        /// Gets the System32 folder.
        /// </summary>
        /// <value>A <see cref="IKnownFolder"/> object.</value>
        public static IKnownFolder System
        {
            get
            {
                return GetKnownFolder(FolderIdentifiers.System);
            }
        }

        /// <summary>
        /// Gets the System32 
        /// folder on x86 systems.
        /// </summary>
        /// <value>A <see cref="IKnownFolder"/> object.</value>
        public static IKnownFolder SystemX86
        {
            get
            {
                return GetKnownFolder(FolderIdentifiers.SystemX86);
            }
        }

        /// <summary>
        /// Gets the windows folder.
        /// </summary>
        /// <value>A <see cref="IKnownFolder"/> object.</value>
        public static IKnownFolder Windows
        {
            get
            {
                return GetKnownFolder(FolderIdentifiers.Windows);
            }
        }

        /// <summary>
        /// Gets the current user's root folder.
        /// </summary>
        /// <value>A <see cref="IKnownFolder"/> object.</value>
        public static IKnownFolder Profile
        {
            get
            {
                return GetKnownFolder(FolderIdentifiers.Profile);
            }
        }

        /// <summary>
        /// Gets the per-user My Pictures folder.
        /// </summary>
        /// <value>A <see cref="IKnownFolder"/> object.</value>
        public static IKnownFolder Pictures
        {
            get
            {
                return GetKnownFolder(FolderIdentifiers.Pictures);
            }
        }

        /// <summary>
        /// Gets the ProgramFiles folder on x86 systems.
        /// </summary>
        /// <value>A <see cref="IKnownFolder"/> object.</value>
        public static IKnownFolder ProgramFilesX86
        {
            get
            {
                return GetKnownFolder(FolderIdentifiers.ProgramFilesX86);
            }
        }

        /// <summary>
        /// Gets the common Program files folder on x86 systems.
        /// </summary>
        /// <value>A <see cref="IKnownFolder"/> object.</value>
        public static IKnownFolder ProgramFilesCommonX86
        {
            get
            {
                return GetKnownFolder(FolderIdentifiers.ProgramFilesCommonX86);
            }
        }

        /// <summary>
        /// Gets the ProgramsFiles folder on x64 systems.
        /// </summary>
        /// <value>A <see cref="IKnownFolder"/> object.</value>
        public static IKnownFolder ProgramFilesX64
        {
            get
            {
                return GetKnownFolder(FolderIdentifiers.ProgramFilesX64);
            }
        }

        /// <summary>
        /// Gets the common Program files folder on x64 systems.
        /// </summary>
        /// <value>A <see cref="IKnownFolder"/> object.</value>
        public static IKnownFolder ProgramFilesCommonX64
        {
            get
            {
                return GetKnownFolder(FolderIdentifiers.ProgramFilesCommonX64);
            }
        }

        /// <summary>
        /// Gets the program files folder.
        /// </summary>
        /// <value>A <see cref="IKnownFolder"/> object.</value>
        public static IKnownFolder ProgramFiles
        {
            get
            {
                return GetKnownFolder(FolderIdentifiers.ProgramFiles);
            }
        }

        /// <summary>
        /// Gets the common program files folder.
        /// </summary>
        /// <value>A <see cref="IKnownFolder"/> object.</value>
        public static IKnownFolder ProgramFilesCommon
        {
            get
            {
                return GetKnownFolder(FolderIdentifiers.ProgramFilesCommon);
            }
        }

        /// <summary>
        /// Gets the administrative tools folder.
        /// </summary>
        /// <value>A <see cref="IKnownFolder"/> object.</value>
        public static IKnownFolder AdminTools
        {
            get
            {
                return GetKnownFolder(FolderIdentifiers.AdminTools);
            }
        }

        /// <summary>
        /// Gets the common administrative tools folder.
        /// </summary>
        /// <value>A <see cref="IKnownFolder"/> object.</value>
        public static IKnownFolder CommonAdminTools
        {
            get
            {
                return GetKnownFolder(FolderIdentifiers.CommonAdminTools);
            }
        }

        /// <summary>
        /// Gets the per-user My Music folder.
        /// </summary>
        /// <value>A <see cref="IKnownFolder"/> object.</value>
        public static IKnownFolder Music
        {
            get
            {
                return GetKnownFolder(FolderIdentifiers.Music);
            }
        }

        /// <summary>
        /// Gets the videos folder.
        /// </summary>
        /// <value>A <see cref="IKnownFolder"/> object.</value>
        public static IKnownFolder Videos
        {
            get
            {
                return GetKnownFolder(FolderIdentifiers.Videos);
            }
        }

        /// <summary>
        /// Gets the Shared Pictures folder.
        /// </summary>
        /// <value>A <see cref="IKnownFolder"/> object.</value>
        public static IKnownFolder PublicPictures
        {
            get
            {
                return GetKnownFolder(FolderIdentifiers.PublicPictures);
            }
        }

        /// <summary>
        /// Gets the Shared Music folder.
        /// </summary>
        /// <value>A <see cref="IKnownFolder"/> object.</value>
        public static IKnownFolder PublicMusic
        {
            get
            {
                return GetKnownFolder(FolderIdentifiers.PublicMusic);
            }
        }

        /// <summary>
        /// Gets the Shared Videos folder.
        /// </summary>
        /// <value>A <see cref="IKnownFolder"/> object.</value>
        public static IKnownFolder PublicVideos
        {
            get
            {
                return GetKnownFolder(FolderIdentifiers.PublicVideos);
            }
        }

        /// <summary>
        /// Gets the Resources  folder.
        /// </summary>
        /// <value>A <see cref="IKnownFolder"/> object.</value>
        public static IKnownFolder ResourceDir
        {
            get
            {
                return GetKnownFolder(FolderIdentifiers.ResourceDir);
            }
        }

        /// <summary>
        /// Gets the localized resources directory folder.
        /// </summary>
        /// <value>A <see cref="IKnownFolder"/> object.</value>
        public static IKnownFolder LocalizedResourcesDir
        {
            get
            {
                return GetKnownFolder(FolderIdentifiers.LocalizedResourcesDir);
            }
        }

        /// <summary>
        /// Gets the common OEM links folder. 
        /// </summary>
        /// <value>A <see cref="IKnownFolder"/> object.</value>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "OEM", Justification = "This is following the native API")]
        public static IKnownFolder CommonOEMLinks
        {
            get
            {
                return GetKnownFolder(FolderIdentifiers.CommonOEMLinks);
            }
        }

        /// <summary>
        /// Gets the folder used to hold files that 
        /// are waiting to be copied onto CD.
        /// </summary>
        /// <value>A <see cref="IKnownFolder"/> object.</value>
        public static IKnownFolder CDBurning
        {
            get
            {
                return GetKnownFolder(FolderIdentifiers.CDBurning);
            }
        }

        /// <summary>
        /// Gets the Users folder.
        /// </summary>
        /// <value>A <see cref="IKnownFolder"/> object.</value>
        public static IKnownFolder UserProfiles
        {
            get
            {
                return GetKnownFolder(FolderIdentifiers.UserProfiles);
            }
        }

        /// <summary>
        /// Gets the playlists folder.
        /// </summary>
        /// <value>A <see cref="IKnownFolder"/> object.</value>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Playlists", Justification = "This is following the native API")]
        public static IKnownFolder Playlists
        {
            get
            {
                return GetKnownFolder(FolderIdentifiers.Playlists);
            }
        }

        /// <summary>
        /// Gets the sample playlists folder.
        /// </summary>
        /// <value>A <see cref="IKnownFolder"/> object.</value>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Playlists", Justification = "This is following the native API")]
        public static IKnownFolder SamplePlaylists
        {
            get
            {
                return GetKnownFolder(FolderIdentifiers.SamplePlaylists);
            }
        }

        /// <summary>
        /// Gets the music samples folder.
        /// </summary>
        /// <value>A <see cref="IKnownFolder"/> object.</value>
        public static IKnownFolder SampleMusic
        {
            get
            {
                return GetKnownFolder(FolderIdentifiers.SampleMusic);
            }
        }

        /// <summary>
        /// Gets the picture samples folder.
        /// </summary>
        /// <value>A <see cref="IKnownFolder"/> object.</value>
        public static IKnownFolder SamplePictures
        {
            get
            {
                return GetKnownFolder(FolderIdentifiers.SamplePictures);
            }
        }

        /// <summary>
        /// Gets the video samples folder.
        /// </summary>
        /// <value>A <see cref="IKnownFolder"/> object.</value>
        public static IKnownFolder SampleVideos
        {
            get
            {
                return GetKnownFolder(FolderIdentifiers.SampleVideos);
            }
        }

        /// <summary>
        /// Gets the Slide Shows folder.
        /// </summary>
        /// <value>A <see cref="IKnownFolder"/> object.</value>
        public static IKnownFolder PhotoAlbums
        {
            get
            {
                return GetKnownFolder(FolderIdentifiers.PhotoAlbums);
            }
        }

        /// <summary>
        /// Gets the public folder.
        /// </summary>
        /// <value>A <see cref="IKnownFolder"/> object.</value>
        public static IKnownFolder Public
        {
            get
            {
                return GetKnownFolder(FolderIdentifiers.Public);
            }
        }

        /// <summary>
        /// Gets the change/remove programs folder.
        /// </summary>
        /// <value>A <see cref="IKnownFolder"/> object.</value>
        public static IKnownFolder ChangeRemovePrograms
        {
            get
            {
                return GetKnownFolder(FolderIdentifiers.ChangeRemovePrograms);
            }
        }

        /// <summary>
        /// Gets the installed updates folder.
        /// </summary>
        /// <value>A <see cref="IKnownFolder"/> object.</value>
        public static IKnownFolder AppUpdates
        {
            get
            {
                return GetKnownFolder(FolderIdentifiers.AppUpdates);
            }
        }

        /// <summary>
        /// Gets the Programs and Features folder.
        /// </summary>
        /// <value>A <see cref="IKnownFolder"/> object.</value>
        public static IKnownFolder AddNewPrograms
        {
            get
            {
                return GetKnownFolder(FolderIdentifiers.AddNewPrograms);
            }
        }

        /// <summary>
        /// Gets the per-user downloads folder.
        /// </summary>
        /// <value>A <see cref="IKnownFolder"/> object.</value>
        public static IKnownFolder Downloads
        {
            get
            {
                return GetKnownFolder(FolderIdentifiers.Downloads);
            }
        }

        /// <summary>
        /// Gets the public downloads folder.
        /// </summary>
        /// <value>A <see cref="IKnownFolder"/> object.</value>
        public static IKnownFolder PublicDownloads
        {
            get
            {
                return GetKnownFolder(FolderIdentifiers.PublicDownloads);
            }
        }

        /// <summary>
        /// Gets the per-user saved searches folder.
        /// </summary>
        /// <value>A <see cref="IKnownFolder"/> object.</value>
        public static IKnownFolder SavedSearches
        {
            get
            {
                return GetKnownFolder(FolderIdentifiers.SavedSearches);
            }
        }

        /// <summary>
        /// Gets the per-user Quick Launch folder.
        /// </summary>
        /// <value>A <see cref="IKnownFolder"/> object.</value>
        public static IKnownFolder QuickLaunch
        {
            get
            {
                return GetKnownFolder(FolderIdentifiers.QuickLaunch);
            }
        }

        /// <summary>
        /// Gets the contacts folder.
        /// </summary>
        /// <value>A <see cref="IKnownFolder"/> object.</value>
        public static IKnownFolder Contacts
        {
            get
            {
                return GetKnownFolder(FolderIdentifiers.Contacts);
            }
        }

        /// <summary>
        /// Gets the per-user gadgets folder.
        /// </summary>
        /// <value>A <see cref="IKnownFolder"/> object.</value>
        public static IKnownFolder SidebarParts
        {
            get
            {
                return GetKnownFolder(FolderIdentifiers.SidebarParts);
            }
        }

        /// <summary>
        /// Gets the shared Gadgets folder.
        /// </summary>
        /// <value>A <see cref="IKnownFolder"/> object.</value>
        public static IKnownFolder SidebarDefaultParts
        {
            get
            {
                return GetKnownFolder(FolderIdentifiers.SidebarDefaultParts);
            }
        }

        /// <summary>
        /// Gets the tree properties folder.
        /// </summary>
        /// <value>A <see cref="IKnownFolder"/> object.</value>
        public static IKnownFolder TreeProperties
        {
            get
            {
                return GetKnownFolder(FolderIdentifiers.TreeProperties);
            }
        }

        /// <summary>
        /// Gets the public game tasks folder.
        /// </summary>
        /// <value>A <see cref="IKnownFolder"/> object.</value>
        public static IKnownFolder PublicGameTasks
        {
            get
            {
                return GetKnownFolder(FolderIdentifiers.PublicGameTasks);
            }
        }

        /// <summary>
        /// Gets the game explorer folder.
        /// </summary>
        /// <value>A <see cref="IKnownFolder"/> object.</value>
        public static IKnownFolder GameTasks
        {
            get
            {
                return GetKnownFolder(FolderIdentifiers.GameTasks);
            }
        }

        /// <summary>
        /// Gets the per-user saved games folder.
        /// </summary>
        /// <value>A <see cref="IKnownFolder"/> object.</value>
        public static IKnownFolder SavedGames
        {
            get
            {
                return GetKnownFolder(FolderIdentifiers.SavedGames);
            }
        }

        /// <summary>
        /// Gets the games folder.
        /// </summary>
        /// <value>A <see cref="IKnownFolder"/> object.</value>
        public static IKnownFolder Games
        {
            get
            {
                return GetKnownFolder(FolderIdentifiers.Games);
            }
        }

        /// <summary>
        /// Gets the recorded tv folder.
        /// </summary>
        /// <value>A <see cref="IKnownFolder"/> object.</value>
        /// <remarks>This folder is not used.</remarks>
        public static IKnownFolder RecordedTV
        {
            get
            {
                return GetKnownFolder(FolderIdentifiers.RecordedTV);
            }
        }

        /// <summary>
        /// Gets the Microsoft Office Outlook folder.
        /// </summary>
        /// <value>A <see cref="IKnownFolder"/> object.</value>
        public static IKnownFolder SearchMapi
        {
            get
            {
                return GetKnownFolder(FolderIdentifiers.SearchMapi);
            }
        }

        /// <summary>
        /// Gets the offline files folder.
        /// </summary>
        /// <value>A <see cref="IKnownFolder"/> object.</value>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Csc", Justification="This is following the native API")]
        public static IKnownFolder SearchCsc
        {
            get
            {
                return GetKnownFolder(FolderIdentifiers.SearchCsc);
            }
        }

        /// <summary>
        /// Gets the per-user links folder.
        /// </summary>
        /// <value>A <see cref="IKnownFolder"/> object.</value>
        public static IKnownFolder Links
        {
            get
            {
                return GetKnownFolder(FolderIdentifiers.Links);
            }
        }

        /// <summary>
        /// Gets the user files folder.
        /// </summary>
        /// <value>A <see cref="IKnownFolder"/> object.</value>
        public static IKnownFolder UsersFiles
        {
            get
            {
                return GetKnownFolder(FolderIdentifiers.UsersFiles);
            }
        }

        /// <summary>
        /// Gets the search results folder.
        /// </summary>
        /// <value>A <see cref="IKnownFolder"/> object.</value>
        public static IKnownFolder SearchHome
        {
            get
            {
                return GetKnownFolder(FolderIdentifiers.SearchHome);
            }
        }

        /// <summary>
        /// Gets the original images folder.
        /// </summary>
        /// <value>A <see cref="IKnownFolder"/> object.</value>
        public static IKnownFolder OriginalImages
        {
            get
            {
                return GetKnownFolder(FolderIdentifiers.OriginalImages);
            }
        }

        /// <summary>
        /// Gets the ProgramFiles folder
        /// </summary>
        public static IKnownFolder UserProgramFiles
        {
            get
            {
                CoreHelpers.ThrowIfNotWin7();
                return GetKnownFolder(FolderIdentifiers.UserProgramFiles);
            }
        }

        /// <summary>
        /// Gets the ProgramFiles\\Common folder
        /// </summary>
        public static IKnownFolder UserProgramFilesCommon
        {
            get
            {
                CoreHelpers.ThrowIfNotWin7();
                return GetKnownFolder(FolderIdentifiers.UserProgramFilesCommon);
            }
        }

        /// <summary>
        /// Gets the Ringtones folder
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Ringtones", Justification = "This is following the native API")]
        public static IKnownFolder Ringtones
        {
            get
            {
                CoreHelpers.ThrowIfNotWin7();
                return GetKnownFolder(FolderIdentifiers.Ringtones);
            }
        }

        /// <summary>
        /// Gets the Public Ringtones folder
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Ringtones", Justification = "This is following the native API")]
        public static IKnownFolder PublicRingtones
        {
            get
            {
                CoreHelpers.ThrowIfNotWin7();
                return GetKnownFolder(FolderIdentifiers.PublicRingtones);
            }
        }

        /// <summary>
        /// Gets the user's Libraries folder
        /// </summary>
        public static IKnownFolder UsersLibraries
        {
            get
            {
                CoreHelpers.ThrowIfNotWin7();
                return GetKnownFolder(FolderIdentifiers.UsersLibraries);
            }
        }

        /// <summary>
        /// Gets the Documents Library folder
        /// </summary>
        public static IKnownFolder DocumentsLibrary
        {
            get
            {
                CoreHelpers.ThrowIfNotWin7();
                return GetKnownFolder(FolderIdentifiers.DocumentsLibrary);
            }
        }

        /// <summary>
        /// Gets the Music Library folder
        /// </summary>
        public static IKnownFolder MusicLibrary
        {
            get
            {
                CoreHelpers.ThrowIfNotWin7();
                return GetKnownFolder(FolderIdentifiers.MusicLibrary);
            }
        }

        /// <summary>
        /// Gets the Pictures Library folder
        /// </summary>
        public static IKnownFolder PicturesLibrary
        {
            get
            {
                CoreHelpers.ThrowIfNotWin7();
                return GetKnownFolder(FolderIdentifiers.PicturesLibrary);
            }
        }

        /// <summary>
        /// Gets the Videos Library folder
        /// </summary>
        public static IKnownFolder VideosLibrary
        {
            get
            {
                CoreHelpers.ThrowIfNotWin7();
                return GetKnownFolder(FolderIdentifiers.VideosLibrary);
            }
        }

        /// <summary>
        /// Gets the RecordedTV Library folder
        /// </summary>
        public static IKnownFolder RecordedTVLibrary
        {
            get
            {
                CoreHelpers.ThrowIfNotWin7();
                return GetKnownFolder(FolderIdentifiers.RecordedTVLibrary);
            }
        }

        /// <summary>
        /// Gets the OtherUsers folder
        /// </summary>
        public static IKnownFolder OtherUsers
        {
            get
            {
                CoreHelpers.ThrowIfNotWin7();
                return GetKnownFolder(FolderIdentifiers.OtherUsers);
            }
        }

        /// <summary>
        /// Gets the DeviceMetadataStore folder
        /// </summary>
        public static IKnownFolder DeviceMetadataStore
        {
            get
            {
                CoreHelpers.ThrowIfNotWin7();
                return GetKnownFolder(FolderIdentifiers.DeviceMetadataStore);
            }
        }

        /// <summary>
        /// Gets the Libraries folder
        /// </summary>
        public static IKnownFolder Libraries
        {
            get
            {
                CoreHelpers.ThrowIfNotWin7();
                return GetKnownFolder(FolderIdentifiers.Libraries);
            }
        }

        /// <summary>
        /// Gets the UserPinned folder
        /// </summary>
        public static IKnownFolder UserPinned
        {
            get
            {
                CoreHelpers.ThrowIfNotWin7();
                return GetKnownFolder(FolderIdentifiers.UserPinned);
            }
        }

        /// <summary>
        /// Gets the ImplicitAppShortcuts folder
        /// </summary>
        public static IKnownFolder ImplicitAppShortcuts
        {
            get
            {
                CoreHelpers.ThrowIfNotWin7();
                return GetKnownFolder(FolderIdentifiers.ImplicitAppShortcuts);
            }
        }

        #endregion

    }
}
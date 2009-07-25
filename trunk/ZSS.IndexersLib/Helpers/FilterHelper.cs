using System.IO;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using ZSS.IndexersLib.Helpers;

namespace ZSS.IndexersLib
{
    public class FilterHelper
    {

        private IndexerAdapter mSettings;
        private string[] mBannedFilter;

        public FilterHelper(IndexerAdapter settings)
        {
            mSettings = settings;
            mBannedFilter = Regex.Split(mSettings.GetConfig().IgnoreFilesList, "\\" + mSettings.GetOptions().IgnoreFilesListDelimiter);
        }

        public List<TreeFile> GetFilesCollFiltered(TreeDir dir)
        {
            List<TreeFile> temp = new List<TreeFile>();

            foreach (TreeFile f in dir.GetFilesColl())
            {
                if (IsBannedFile(f.GetFilePath()) == false)
                {
                    temp.Add(f);
                }
            }
            return temp;
        }

        public bool isBannedFolder(TreeDir dir)
        {
            // Check if Option set to Enable Filtering
            if (mSettings.GetConfig().EnabledFiltering)
            {
                DirectoryInfo di = new DirectoryInfo(dir.DirectoryPath());
                FileAttributesEx dirAttrib = new FileAttributesEx(dir.DirectoryPath());

                string[] c = dir.DirectoryPath().Split(Path.DirectorySeparatorChar);
                // If Options says to filter protected OS folders 
                if (mSettings.GetConfig().HideProtectedOperatingSystemFilesFolders)
                {
                    return c[1].Length != 0 && dirAttrib.isReadOnlyDirectory() && mSettings.GetConfig().HideProtectedOperatingSystemFilesFolders;
                }

                // If Config says to filter Hidden Folders
                if (mSettings.GetConfig().IgnoreHiddenFolders)
                {
                    return dirAttrib.isHidden();
                }

                // If Config says to filter System Folders 
                if (mSettings.GetConfig().IgnoreSystemFolders)
                {
                    return dirAttrib.isSystem();
                }

                //war59312: If Config says to filter Empty Folders 
                if (mSettings.GetConfig().IgnoreEmptyFolders && dir.DirectorySize() == 0.0)
                {
                    return true;
                }
            }

            return false;
        }

        public bool IsBannedFile(string filePath)
        {

            // Check if Option set to Enable Filtering
            if (mSettings.GetConfig().EnabledFiltering)
            {

                // Establish an FileInfo, we need for the checks below
                FileAttributesEx fi = new FileAttributesEx(filePath);

                // If Options says to filter protected OS files 
                if (mSettings.GetConfig().HideProtectedOperatingSystemFilesFolders)
                {
                    return fi.isHiddenSystemFile();
                }

                // If Config says to filter Hidden Files
                if (mSettings.GetConfig().IgnoreHiddenFiles)
                {
                    return fi.isHidden();
                }

                // If Config says to filter System Files 
                if (mSettings.GetConfig().IgnoreSystemFiles)
                {
                    return fi.isSystem();
                }

                // If Config says to filter following files 
                if (mSettings.GetConfig().IgnoreFollowingFiles)
                {
                    foreach (string item in mBannedFilter)
                    {
                        if (Path.GetFileName(filePath).ToLower() == item.ToLower())
                        {
                            return true;
                        }
                        if (item.IndexOf("*.") != -1 && item.IndexOf(Path.GetExtension(filePath)) != -1)
                        {
                            return true;
                        }
                    }

                }
            }

            return false;
        }

    }

}

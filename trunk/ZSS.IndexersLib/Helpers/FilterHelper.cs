using System.IO;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace ZSS.IndexersLib
{
    public class FilterHelper
    {

        private IndexerAdapter mSettings;
        private string[] mBannedFilter;

        public FilterHelper(IndexerAdapter settings)
        {

            mSettings = settings;

            mBannedFilter = Regex.Split(mSettings.GetConfig().IgnoreFilesList, mSettings.GetOptions().IgnoreFilesListDelimiter);
        }

        public List<cFile> GetFilesCollFiltered(cDir dir)
        {

            List<cFile> temp = new List<cFile>();

            foreach (cFile f in dir.GetFilesColl())
            {
                if (IsBannedFile(f.GetFilePath()) == false)
                {
                    temp.Add(f);
                }
            }


            return temp;
        }

        public bool isBannedFolder(cDir dir)
        {

            // Check if Option set to Enable Filtering
            if (mSettings.GetConfig().EnabledFiltering)
            {

                DirectoryInfo di = new DirectoryInfo(dir.DirectoryPath());

                string[] c = dir.DirectoryPath().Split(Path.DirectorySeparatorChar);
                // If Options says to filter protected OS folders 
                if (mSettings.GetConfig().HideProtectedOperatingSystemFilesFolders)
                {
                    //MsgBox(di.FullName + " is " + di.Attributes.ToString())
                    bool banned = c[1].Length != 0 && ((File.GetAttributes(di.FullName) & FileAttributes.ReadOnly) == FileAttributes.ReadOnly);
                    banned = banned && ((File.GetAttributes(di.FullName) & FileAttributes.Directory) == FileAttributes.Directory);
                    banned = banned && mSettings.GetConfig().HideProtectedOperatingSystemFilesFolders;
                    return banned;
                }

                //// If Config says to filter Hidden Folders
                //if (mSettings.GetConfig().IgnoreHiddenFolders)
                //{
                //    if (di.Attributes == FileAttributes.Directory | FileAttributes.Hidden)
                //    {
                //        //MsgBox("Hidden Folder Check: " + di.FullName + " is " + di.Attributes.ToString())
                //        return true;
                //    }
                //}

                //// If Config says to filter System Folders 
                //if (mSettings.GetConfig().IgnoreSystemFolders)
                //{
                //    if (di.Attributes == FileAttributes.Directory + FileAttributes.System)
                //    {
                //        //MsgBox("System Folder Check: " + di.FullName + " is " + di.Attributes.ToString())
                //        return true;
                //    }
                //}

                //war59312 If Config says to filter Empty Folders 
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
                FileInfo fi = new FileInfo(filePath);

                //// If Options says to filter protected OS files 
                //if (mSettings.GetConfig().HideProtectedOperatingSystemFilesFolders)
                //{
                //    if ((fi.Attributes == FileAttributes.Archive + FileAttributes.Hidden + FileAttributes.System) | (fi.Attributes == FileAttributes.ReadOnly + FileAttributes.Hidden + FileAttributes.System))
                //    {
                //        //Console.WriteLine("HideProtectedOperatingSystemFilesFolders Check: HS when " + fi.FullName + " is " + fi.Attributes.ToString())
                //        return true;
                //    }
                //}

                //// If Config says to filter Hidden Files
                //if (mSettings.GetConfig().IgnoreHiddenFiles)
                //{
                //    if (fi.Attributes == FileAttributes.Archive + FileAttributes.Hidden)
                //    {
                //        return true;
                //    }
                //}

                //// If Config says to filter System Files 
                //if (mSettings.GetConfig().IgnoreSystemFiles)
                //{
                //    if (fi.Attributes == FileAttributes.Archive + FileAttributes.System)
                //    {
                //        return true;
                //    }
                //}

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

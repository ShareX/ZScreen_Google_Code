using System.IO;
using System.Windows.Forms;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace ZSS.IndexersLib
{
    public class TreeNetIndexer : Indexer
    {

        private IndexerAdapter mSettings;
        private FilterHelper mFilter;
        private bool mBooFirstDir = true;

        // 2.7.1.1 TreeGUI failed to index when Decimal Symbol set to a charactor other than dot
        string mDecimalSymbol = System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;

        public TreeNetIndexer(IndexerAdapter settings)
            : base(settings)
        {
            mSettings = new IndexerAdapter();
            mSettings = settings;
            mFilter = new FilterHelper(mSettings);
        }

        private TreeDir Analyze(string rootDirPath)
        {
            TreeDir dirRoot = new TreeDir(rootDirPath);
            dirRoot = GetFiles(dirRoot.DirectoryPath());
            if (mSettings.GetConfig().SortBySize)
            {
                dirRoot.GetSubDirColl().Sort(new TreeDirComparer());
                if (mSettings.GetConfig().SortBySizeMode == FileSortMode.Descending)
                {
                    dirRoot.GetSubDirColl().Reverse();
                }
            }
            return dirRoot;
        }

        private TreeDir GetFiles(string dirPath)
        {

            TreeDir dir = new TreeDir(dirPath);

            try
            {
                foreach (string filepath in Directory.GetFiles(dirPath))
                {
                    dir.SetFile(filepath, TreeDir.BinaryPrefix.Kibibytes);
                }
            }
            catch (System.UnauthorizedAccessException ex)
            {
                Console.WriteLine(ex.ToString());
            }

            try
            {
                foreach (string subDirPath in Directory.GetDirectories(dirPath))
                {

                    TreeDir subdir = new TreeDir(subDirPath);
                    subdir = GetFiles(subDirPath);
                    dir.AddDir(subdir);
                }
            }
            catch (System.UnauthorizedAccessException ex)
            {
                Console.WriteLine(ex.ToString());
            }

            return dir;
        }


        private string fGetDirPath(TreeDir dir)
        {
            string dirName = null;
            string[] c = dir.DirectoryPath().Split(Path.DirectorySeparatorChar);
            if (c[1].Length == 0)
            {
                // Root Drive
                dirName = dir.DirectoryPath();
            }
            else
            {
                if (mSettings.GetConfig().ShowFolderPath)
                {
                    dirName = dir.DirectoryPath();
                }
                else
                {
                    dirName = dir.DirectoryName();

                }
            }
            return dirName;
        }

        private string fGetDirSizeString(TreeDir dir)
        {

            string dirSize = null;

            if (mBooFirstDir)
            {
                dirSize = dir.DirectorySizeToString(TreeDir.BinaryPrefix.Gibibytes);
                if (decimal.Parse(Regex.Split(dirSize, "\\" + mDecimalSymbol)[0]) == 0)
                {
                    dirSize = dir.DirectorySizeToString(TreeDir.BinaryPrefix.Mebibytes);
                    if (decimal.Parse(Regex.Split(dirSize, " ")[0]) == 0)
                    {
                        dirSize = dir.DirectorySizeToString(TreeDir.BinaryPrefix.Kibibytes);
                    }
                }
            }
            else
            {
                dirSize = dir.DirectorySizeToString(TreeDir.BinaryPrefix.Mebibytes);
                if (decimal.Parse(Regex.Split(dirSize, "\\" + mDecimalSymbol)[0]) == 0)
                {
                    dirSize = dir.DirectorySizeToString(TreeDir.BinaryPrefix.Kibibytes);
                }
            }


            return dirSize;
        }

        private string fGetVirtualDirName(string filePath)
        {
            foreach (string line in mSettings.GetConfig().VirtualFolderList)
            {
                string[] spline = Regex.Split(line, "|");
                if (filePath.IndexOf(spline[0]) != -1)
                {
                    return filePath.Replace(spline[0], spline[1]);
                }
            }
            return filePath;
        }

        private bool fDivWrap(TreeDir dir)
        {

            bool y = false;
            y = (rootDir != dir.DirectoryPath()) && (dir.GetSubDirColl().Count > 0 | mSettings.GetConfig().ShowFileCount);


            return y;
        }

        //' DOMCOLLAPSE RULES
        //' If a folder has subfolders then wrap the folder with div
        //' If a folder has no files then don't have trigger
        //TODO war59312 - Hide folders from output in which all its files are ignored
        string rootDir = string.Empty;
        private TreeDir IndexToHtmlFile(TreeDir dir, StreamWriter where)
        {
            bool isNotIndexableDir = mFilter.isBannedFolder(dir);

            string dirName = fGetDirPath(dir);
            string dirSize = (string)fGetDirSizeString(dir);

            string dirTitle = null;

            if (mSettings.GetConfig().EnabledFiltering && mSettings.GetConfig().IgnoreEmptyFolders && dir.DirectorySize() == 0.0)
            {
                //war59312 - dont show empty folders
                dirTitle = "";
            }
            else
            {
                if (mSettings.GetConfig().ShowDirSize)
                {
                    dirTitle = HTMLHelper.GetValidXhtmlLine(string.Format("{0} [{1}]", dirName, dirSize));
                }
                else
                {
                    dirTitle = HTMLHelper.GetValidXhtmlLine(dirName);
                }
            }

            if (mBooFirstDir)
            {
                rootDir = dir.DirectoryPath();
                where.WriteLine("<h1>" + dirTitle + "</h1>");
                mBooFirstDir = false;
                mNumTabs = 1;
            }
            else
            {
                if (!isNotIndexableDir)
                {
                    if (mSettings.GetConfig().EnabledFiltering && mSettings.GetConfig().IgnoreEmptyFolders && dir.DirectorySize() == 0.0)
                    {
                        //war59312 - dont show empty folders
                    }
                    else
                    {
                        if (mSettings.GetConfig().ShowFolderPathOnStatusBar)
                        {
                            string hyperlinkDir = null;
                            if (mSettings.GetConfig().ShowVirtualFolders)
                            {
                                // Virtual Folders
                                hyperlinkDir = mSettings.GetConfig().ServerInfo + "/" + fGetVirtualDirName(dir.DirectoryPath()).Replace("\\", "/");
                            }
                            else
                            {
                                // Locally Browse
                                hyperlinkDir = "file://" + dir.DirectoryPath();
                            }
                            hyperlinkDir = "<a href=" + (char)34 + hyperlinkDir + (char)34 + ">" + dirTitle + "</a>";
                            where.WriteLine(GetHeadingOpen(dir) + hyperlinkDir + GetHeadingClose());
                        }
                        else
                        {
                            where.WriteLine(GetHeadingOpen(dir) + dirTitle + GetHeadingClose());
                        }
                    }
                }
            }

            if (!isNotIndexableDir)
            {
                if (mSettings.GetConfig().EnabledFiltering && mSettings.GetConfig().IgnoreEmptyFolders && dir.DirectorySize() == 0.0)
                {
                    //war59312 - dont show empty folders
                }
                else
                {
                    List<TreeFile> files = dir.GetFilesColl(mSettings);

                    if (fDivWrap(dir))
                    {
                        where.WriteLine(HTMLHelper.OpenDiv());
                    }

                    if (double.Parse(Regex.Split(dir.DirectorySizeToString(TreeDir.BinaryPrefix.Kibibytes), " ")[0]) > 0 | files.Count > 0)
                    {

                        if (mSettings.GetConfig().ShowFileCount)
                        {
                            if (files.Count > 0)
                            {
                                where.WriteLine(HTMLHelper.OpenPara("foldercount"));
                                where.WriteLine("Files Count: " + files.Count.ToString());
                                where.WriteLine(HTMLHelper.ClosePara());
                            }
                        }
                    }
                    else
                    {
                        //Note: 
                        // dir.GetFilesColl().Count = 0 DOESNT ALWAYS MEAN THAT 
                        // it is an empty directory because there can be subfolders 
                        // with files
                        //System.Windows.Forms.MessageBox.Show(dir.GetFilesColl().Count)
                        where.WriteLine(HTMLHelper.OpenPara(""));
                        where.WriteLine(mSettings.GetOptions().EmptyFolderMessage + HTMLHelper.AddBreak());
                        where.WriteLine(HTMLHelper.ClosePara());
                    }

                    if (files.Count > 0)
                    {
                        // Check if there is AT LEAST ONE valid file
                        bool booPrintList = false;
                        foreach (TreeFile fp in files)
                        {
                            if (!mFilter.IsBannedFile(fp.GetFilePath()))
                            {
                                booPrintList = true;
                                break; // TODO: might not be correct. Was : Exit For
                            }
                        }

                        if (mSettings.GetConfig().ShowFilesTreeNet)
                        {
                            if (booPrintList)
                            {
                                switch (mSettings.GetConfig().IndexListType)
                                {
                                    case XHTMLFileListMode.Bullets:
                                        where.WriteLine(HTMLHelper.OpenBulletedList());
                                        break;
                                    case XHTMLFileListMode.Numbered:
                                        where.WriteLine(HTMLHelper.OpenNumberedList());
                                        break;
                                }
                            }

                            if (mSettings.GetConfig().RevereFileOrder)
                            {
                                files.Reverse();
                            }

                            foreach (TreeFile f in files)
                            {
                                string lLine = null;

                                if (!mFilter.IsBannedFile(f.GetFilePath()))
                                {
                                    string strFilePath = null;

                                    if (mSettings.GetConfig().ShowFilePath)
                                    {
                                        if (mSettings.GetConfig().ShowVirtualFolders)
                                        {
                                            strFilePath = fGetVirtualDirName(f.GetFilePath());
                                        }
                                        else
                                        {
                                            strFilePath = f.GetFilePath();
                                        }
                                    }
                                    else
                                    {
                                        if (mSettings.GetConfig().HideExtension)
                                        {
                                            strFilePath = f.GetFileNameWithoutExtension();
                                        }
                                        else
                                        {
                                            strFilePath = f.GetFileName();
                                        }
                                    }

                                    if (mSettings.GetConfig().ShowFileSize)
                                    {
                                        string fileSize = f.GetSizeToString(TreeDir.BinaryPrefix.Mebibytes);
                                        if (double.Parse(Regex.Split(fileSize, "\\" + mDecimalSymbol)[0]) == 0)
                                        {
                                            fileSize = f.GetSizeToString(TreeDir.BinaryPrefix.Kibibytes);
                                        }
                                        lLine = HTMLHelper.GetValidXhtmlLine(strFilePath) + " " + HTMLHelper.GetSpan(string.Format(" [{0}]", fileSize), "filesize");
                                    }
                                    else
                                    {
                                        lLine = HTMLHelper.GetValidXhtmlLine(strFilePath);
                                    }

                                    if (mSettings.GetConfig().AudioInfo && fIsAudio(f.GetFileExtension().ToLower()) == true)
                                    {

                                        try
                                        {
                                            TagLib.File audioFile = TagLib.File.Create(f.GetFilePath());
                                            double fsize = f.GetSize(TreeDir.BinaryPrefix.Kibibits);
                                            double dura = audioFile.Properties.Duration.TotalSeconds;

                                            if (dura > 0)
                                            {
                                                Console.WriteLine(fsize / dura);
                                                lLine += HTMLHelper.GetSpan(string.Format(" [{0} kb/s]", (fsize / dura).ToString("0.00"), fGetHMS(audioFile.Properties.Duration.TotalSeconds)), "audioinfo");
                                                lLine += HTMLHelper.GetSpan(string.Format(" [{0}]", fGetHMS(audioFile.Properties.Duration.TotalSeconds)), "audiolength");
                                            }
                                        }
                                        catch (Exception ex)
                                        {
                                            Console.WriteLine(ex.ToString() + "\n" + f.GetFilePath());
                                        }
                                    }

                                    where.WriteLine("<li>" + lLine + "</li>");

                                }
                            }

                            if (booPrintList)
                            {
                                switch (mSettings.GetConfig().IndexListType)
                                {
                                    case XHTMLFileListMode.Bullets:
                                        where.WriteLine(HTMLHelper.CloseBulletedList());
                                        break;
                                    case XHTMLFileListMode.Numbered:
                                        where.WriteLine(HTMLHelper.CloseNumberedList());
                                        break;
                                }

                            }
                            // Show Files for TreeNet
                        }
                    }


                    mNumTabs += 1;

                    foreach (TreeDir d in dir.GetSubDirColl())
                    {

                        TreeDir sd = new TreeDir(d.DirectoryPath());

                        sd = IndexToHtmlFile(d, where);
                    }

                    if (fDivWrap(dir))
                    {
                        where.WriteLine(HTMLHelper.CloseDiv());
                    }

                    mNumTabs -= 1;

                }
            }

            return dir;
        }

        public bool fIsAudio(string ext)
        {
            string[] exts = { ".mp3", ".m4a", ".flac", ".wma" };

            foreach (string ex in exts)
            {
                if (ext.Equals(ex.ToLower()))
                {
                    return true;
                }
            }


            return false;
        }

        public string fGetHMS(double sec)
        {

            double[] hms = fGetDurationInHoursMS(sec);

            return string.Format("{0}:{1}:{2}", hms[0].ToString("00"), hms[1].ToString("00"), hms[2].ToString("00"));
        }

        public string fGetHMS2(double sec)
        {

            double[] hms = fGetDurationInHoursMS(sec);

            return string.Format("{0} Hours {1} Minutes {2} Seconds", hms[0], hms[1], hms[2]);
        }

        public double[] fGetDurationInHoursMS(double seconds)
        {

            double[] arrayHoursMinutesSeconds = new double[4];
            double SecondsLeft = seconds;
            int hours = 0;
            int minutes = 0;

            while (SecondsLeft >= 3600)
            {
                SecondsLeft -= 3600;
                hours += 1;
            }

            arrayHoursMinutesSeconds[0] = hours;

            while (SecondsLeft >= 60)
            {
                SecondsLeft -= 60;
                minutes += 1;
            }

            arrayHoursMinutesSeconds[1] = minutes;
            arrayHoursMinutesSeconds[2] = SecondsLeft;


            return arrayHoursMinutesSeconds;
        }

        private bool mBooFirstIndexFile = true;
        private bool mBooMoreFilesToCome = false;

        private void IndexRootFolderToHtml(string folderPath, StreamWriter sw, bool bAddFooter)
        {
            if (mBooFirstIndexFile)
            {
                sw.WriteLine(HTMLHelper.GetDocType());
                if (mSettings.GetConfig().CollapseFolders)
                {
                    sw.WriteLine(HTMLHelper.GetCollapseJs());
                    sw.WriteLine(HTMLHelper.GetCollapseCss());
                }
                sw.WriteLine(HTMLHelper.GetCssStyle(mSettings.GetConfig().CssFilePath));

                if (mBooMoreFilesToCome)
                {
                    sw.WriteLine(HTMLHelper.GetTitle(mSettings.GetConfig().MergedHtmlTitle));
                    mBooMoreFilesToCome = false;
                }
                else
                {
                    string[] c = folderPath.Split(Path.DirectorySeparatorChar);
                    if (c[1].Length == 0)
                    {
                        sw.WriteLine(HTMLHelper.GetTitle("Index for " + folderPath));
                    }
                    else
                    {
                        sw.WriteLine(HTMLHelper.GetTitle("Index for " + Path.GetFileName(folderPath)));
                    }
                }

                sw.WriteLine(HTMLHelper.CloseHead());
                sw.WriteLine(HTMLHelper.OpenBody());

                mBooFirstIndexFile = false;
            }

            TreeDir rootDir = new TreeDir(folderPath);

            rootDir = Analyze(rootDir.DirectoryPath());

            this.IndexToHtmlFile(rootDir, sw);

            if (bAddFooter)
            {
                sw.WriteLine(HTMLHelper.OpenDiv());
                sw.WriteLine("<hr>");
                sw.WriteLine(mSettings.GetFooterText(null, IndexingEngine.TreeNetLib, true));
                sw.WriteLine(HTMLHelper.CloseDiv());
                sw.WriteLine(HTMLHelper.CloseBody());
            }

            mBooFirstDir = true;
        }

        private void IndexFolderToTxt(string folderPath, StreamWriter sw, bool AddFooter)
        {
            if (Directory.Exists(folderPath))
            {
                // 2.7.1.6 TreeGUI crashed on Could not find a part of the path 

                TreeDir dir = new TreeDir(folderPath);
                dir = Analyze(dir.DirectoryPath());
                this.IndexToTxtFile(dir, sw);
                if (AddFooter)
                {
                    sw.WriteLine("____");
                    sw.WriteLine(mSettings.GetFooterText(null, IndexingEngine.TreeNetLib, false));
                }
            }
        }

        private TreeDir IndexToTxtFile(TreeDir dir, StreamWriter sw)
        {
            bool isNotIndexableDir = mFilter.isBannedFolder(dir);

            string dirSize = fGetDirSizeString(dir);
            string dirTitle = string.Format("{0} [{1}]", dir.DirectoryName(), dirSize);

            string strStars = "";
            char[] styleArray = mSettings.GetConfig().FolderHeadingStyle.ToCharArray();

            for (int i = 0; i <= dirTitle.Length - 1; i++)
            {
                strStars += styleArray[i % styleArray.Length];
            }

            sw.WriteLine(GetTabs() + strStars);
            sw.WriteLine(GetTabs() + dirTitle);
            sw.WriteLine(GetTabs() + strStars);

            if (double.Parse(Regex.Split(dir.DirectorySizeToString(TreeDir.BinaryPrefix.Kibibytes), " ")[0]) == 0)
            {
                sw.WriteLine(GetTabs() + mSettings.GetOptions().EmptyFolderMessage);
            }

            if (mSettings.GetConfig().ShowFilesTreeNet)
            {
                List<TreeFile> files = dir.GetFilesColl(mSettings);
                if (files.Count > 0)
                {
                    sw.WriteLine();
                }
                foreach (TreeFile fi in files)
                {
                    string fileDesc = GetTabs() + "  ";

                    if (mSettings.GetConfig().ShowFileSize)
                    {
                        string fileSize = fi.GetSizeToString(TreeDir.BinaryPrefix.Mebibytes);
                        if (double.Parse(Regex.Split(fileSize, "\\" + mDecimalSymbol)[0]) == 0)
                        {
                            fileSize = fi.GetSizeToString(TreeDir.BinaryPrefix.Kibibytes);
                        }
                        fileDesc += string.Format("{0} [{1}]", fi.GetFileName(), fileSize);
                    }
                    else
                    {
                        fileDesc += fi.GetFileName();
                    }

                    if (mSettings.GetConfig().AudioInfo && fIsAudio(fi.GetFileExtension().ToLower()) == true)
                    {
                        try
                        {
                            TagLib.File audioFile = TagLib.File.Create(fi.GetFilePath());
                            double fsize = fi.GetSize(TreeDir.BinaryPrefix.Kibibits);
                            double dura = audioFile.Properties.Duration.TotalSeconds;

                            if (dura > 0)
                            {
                                Console.WriteLine(fsize / dura);
                                fileDesc += string.Format(" [{0} Kibit/s]", (fsize / dura).ToString("0.00"), fGetHMS(audioFile.Properties.Duration.TotalSeconds));
                                fileDesc += string.Format(" [{0}]", fGetHMS(audioFile.Properties.Duration.TotalSeconds));
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.ToString() + "\n" + fi.GetFilePath());
                        }
                    }
                    sw.WriteLine(fileDesc);
                }
                sw.WriteLine();
            }

            mNumTabs += 1;

            foreach (TreeDir d in dir.GetSubDirColl())
            {

                TreeDir sd = new TreeDir(d.DirectoryPath());
                sd = IndexToTxtFile(d, sw);
            }
            mNumTabs -= 1;


            return dir;
        }

        int mNumTabs = 0;

        public override void IndexNow(IndexingMode IndexMode)
        {

            string fp = null;

            List<string> folderList = new List<string>();
            folderList = mSettings.GetConfig().FolderList;
            TreeNetIndexer treeNetLib = new TreeNetIndexer(mSettings);

            string ext = mSettings.GetConfig().IndexFileExt;

            switch (IndexMode)
            {
                case IndexingMode.IN_EACH_DIRECTORY:
                    IndexInEachDir(mSettings);
                    break;
                case IndexingMode.IN_ONE_FOLDER_MERGED:
                    if (mSettings.GetConfig().MergeFiles)
                    {
                        fp = mSettings.fGetIndexFilePath(-1, IndexingMode.IN_ONE_FOLDER_MERGED);

                        StreamWriter sw = new StreamWriter(fp, false);

                        if (mSettings.GetConfig().FolderList.Count > 1)
                        {
                            for (int i = 0; i <= mSettings.GetConfig().FolderList.Count - 2; i++)
                            {
                                string strDirPath = mSettings.GetConfig().FolderList[i];
                                TreeDir dir = new TreeDir(strDirPath);

                                this.CurrentDirMessage = "Indexing " + strDirPath;

                                if (ext.Contains(".html"))
                                {
                                    treeNetLib.mBooMoreFilesToCome = true;
                                    treeNetLib.IndexRootFolderToHtml(strDirPath, sw, false);
                                }
                                else
                                {
                                    treeNetLib.IndexFolderToTxt(strDirPath, sw, false);
                                }


                                this.Progress += 1;
                            }
                        }

                        TreeDir lastDir = new TreeDir(mSettings.GetConfig().FolderList[mSettings.GetConfig().FolderList.Count - 1]);
                        this.CurrentDirMessage = "Indexing " + lastDir.DirectoryPath();

                        if (ext.Contains(".html"))
                        {
                            treeNetLib.mBooFirstIndexFile = false || mSettings.GetConfig().FolderList.Count == 1;
                            treeNetLib.IndexRootFolderToHtml(lastDir.DirectoryPath(), sw, true);
                        }
                        else
                        {
                            treeNetLib.IndexFolderToTxt(lastDir.DirectoryPath(), sw, true);
                        }

                        sw.Close();
                        if (mSettings.GetConfig().ZipMergedFile)
                        {
                            mSettings.ZipAdminFile(fp, null);
                        }


                        this.Progress += 1;
                    }
                    break;
                case IndexingMode.IN_ONE_FOLDER_SEPERATE:

                    // DO NOT MERGE INDEX FILES 
                    if (!Directory.Exists(mSettings.GetConfig().OutputDir))
                    {
                        MessageBox.Show(string.Format("{0} does not exist." + Environment.NewLine + Environment.NewLine + "Please change the Output folder in Configuration." + Environment.NewLine + "The index file will be created in the same folder you chose to index.", mSettings.GetConfig().OutputDir), Application.ProductName, MessageBoxButtons.OK);
                    }


                    for (int i = 0; i <= mSettings.GetConfig().FolderList.Count - 1; i++)
                    {

                        string strDirPath = mSettings.GetConfig().FolderList[i];

                        string sDrive = Path.GetPathRoot(strDirPath).Substring(0, 1);
                        string sDirName = Path.GetFileName(strDirPath);
                        string sep = mSettings.GetOptions().IndividualIndexFileWordSeperator;

                        //where = mSettings.GetConfig().OutputDir + "\" + sDrive + sep + sDirName + sep + mSettings.GetConfig().GetIndexFileName
                        // New Behavior for getting where location
                        fp = mSettings.fGetIndexFilePath(i, IndexingMode.IN_ONE_FOLDER_SEPERATE);
                        mSettings.GetConfig().SetIndexPath(fp);
                        //MsgBox(where = mSettings.GetConfig().OutputDir + "\" + sDrive + sep + sDirName + sep + mSettings.GetConfig().GetIndexFileName)

                        StreamWriter sw = new StreamWriter(fp, false);

                        this.CurrentDirMessage = "Indexing " + strDirPath;

                        if (ext.Contains(".html"))
                        {
                            treeNetLib.mBooFirstIndexFile = true;
                            treeNetLib.IndexRootFolderToHtml(strDirPath, sw, true);
                        }
                        else
                        {
                            treeNetLib.IndexFolderToTxt(strDirPath, sw, true);
                        }

                        sw.Close();
                        if (mSettings.GetConfig().ZipFilesInOutputDir)
                        {
                            mSettings.ZipAdminFile(fp, null);
                        }

                        this.Progress += 1;
                    }


                    break;

            }
        }

        private void IndexInEachDir(IndexerAdapter myReader)
        {

            string where = null;
            List<string> folderList = new List<string>();
            folderList = myReader.GetConfig().FolderList;
            TreeNetIndexer treeNetLib = new TreeNetIndexer(myReader);

            string ext = myReader.GetConfig().IndexFileExt;

            for (int i = 0; i <= myReader.GetConfig().FolderList.Count - 1; i++)
            {

                string strDirPath = myReader.GetConfig().FolderList[i];
                // 2.5.1.1 Indexer halted if a configuration file had non-existent folders paths
                if (Directory.Exists(strDirPath))
                {

                    where = myReader.fGetIndexFilePath(i, IndexingMode.IN_EACH_DIRECTORY);
                    if (Directory.Exists(Path.GetDirectoryName(where)) == false)
                    {
                        Directory.CreateDirectory(Path.GetDirectoryName(where));
                    }

                    StreamWriter sw = new StreamWriter(where, false);

                    try
                    {
                        this.CurrentDirMessage = "Indexing " + strDirPath;

                        if (ext.Contains("html"))
                        {
                            //MessageBox.Show(myReader.GetConfig().mCssFilePath)
                            treeNetLib.mBooFirstIndexFile = true;
                            treeNetLib.IndexRootFolderToHtml(strDirPath, sw, mSettings.GetConfig().AddFooter);
                        }
                        else
                        {
                            treeNetLib.IndexFolderToTxt(strDirPath, sw, mSettings.GetConfig().AddFooter);
                        }


                        this.Progress += 1;
                    }
                    catch (System.UnauthorizedAccessException ex)
                    {
                        MessageBox.Show(ex.Message + "\n" + "Please Run TreeGUI As Administrator or Change Output Directory.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK);
                        return;
                    }
                    finally
                    {
                        sw.Close();
                    }

                    // Zip after sw is closed
                    if (myReader.GetConfig().ZipFilesInEachDir)
                    {
                        myReader.ZipAdminFile(where, null);
                    }
                }
            }
        }

        private string GetTabs()
        {
            string tabs = "";
            for (int i = 1; i <= mNumTabs; i++)
            {
                tabs += "\t";
            }
            return tabs;
        }

        private string GetHeadingOpen(TreeDir dir)
        {
            string cName = "trigger";
            List<TreeFile> files = dir.GetFilesColl(mSettings);

            if (mNumTabs > mSettings.GetConfig().FolderExpandLevel)
            {
                cName = "expanded";
            }
            else
            {
                cName = "trigger";
            }

            string tabs = string.Empty;

            if (mNumTabs < 7)
            {
                if (dir.GetSubDirColl().Count > 0 || files.Count > 0)
                {
                    tabs = string.Format("<h{0} class=\"{1}\">", mNumTabs.ToString(), cName);
                }
                else
                {
                    tabs = string.Format("<h{0}>", mNumTabs.ToString());
                }
            }
            else
            {
                if (dir.GetSubDirColl().Count > 0 || files.Count > 0)
                {
                    tabs = string.Format("<h6 class=\"{0}\">", cName);
                }
                else
                {
                    tabs = "<h6>";
                }
            }

            return tabs;
        }

        private string GetHeadingClose()
        {
            if (mNumTabs < 7)
            {
                return "</h" + mNumTabs.ToString() + ">";
            }
            else
            {
                return "</h6>";
            }
        }
    }
}
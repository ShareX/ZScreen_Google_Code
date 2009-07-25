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

        private cDir Analyze(string rootDirPath)
        {
            cDir dirRoot = new cDir(rootDirPath);
            dirRoot = GetFiles(dirRoot.DirectoryPath());
            if (mSettings.GetConfig().mSortBySize)
            {
                dirRoot.GetSubDirColl().Sort(new TreeDirComparer());
                if (1 == mSettings.GetConfig().mSortBySizeMode)
                {
                    dirRoot.GetSubDirColl().Reverse();
                }
            }
            return dirRoot;
        }

        private cDir GetFiles(string dirPath)
        {

            cDir dir = new cDir(dirPath);

            try
            {
                foreach (string filepath in Directory.GetFiles(dirPath))
                {
                    dir.SetFile(filepath, cDir.BinaryPrefix.Kibibytes);
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

                    cDir subdir = new cDir(subDirPath);
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


        private string fGetDirPath(cDir dir)
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

        private string fGetDirSizeString(cDir dir)
        {

            string dirSize = null;

            if (mBooFirstDir)
            {
                dirSize = dir.DirectorySizeToString(cDir.BinaryPrefix.Gibibytes);
                if (decimal.Parse(Regex.Split(dirSize, "\\" + mDecimalSymbol)[0]) == 0)
                {
                    dirSize = dir.DirectorySizeToString(cDir.BinaryPrefix.Mebibytes);
                    if (decimal.Parse(Regex.Split(dirSize, " ")[0]) == 0)
                    {
                        dirSize = dir.DirectorySizeToString(cDir.BinaryPrefix.Kibibytes);
                    }
                }
            }
            else
            {
                dirSize = dir.DirectorySizeToString(cDir.BinaryPrefix.Mebibytes);
                if (decimal.Parse(Regex.Split(dirSize, "\\" + mDecimalSymbol)[0]) == 0)
                {
                    dirSize = dir.DirectorySizeToString(cDir.BinaryPrefix.Kibibytes);
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

        private bool fDivWrap(cDir dir)
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
        private cDir IndexToHtmlFile(cDir dir, StreamWriter @where)
        {

            HtmlHelper html = new HtmlHelper();

            bool isNotIndexableDir = mFilter.isBannedFolder(dir);
            // (c(1).Length <> 0 And di.Attributes = FileAttributes.Hidden + FileAttributes.System + FileAttributes.Directory) And mSettings.GetOptions().mIgnoreHiddenFilesFolders
            //System.Windows.Forms.MessageBox.Show(isNotIndexableDir)

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
                    dirTitle = html.GetValidXhtmlLine(string.Format("{0} [{1}]", dirName, dirSize));
                }
                else
                {
                    dirTitle = html.GetValidXhtmlLine(dirName);

                }
            }

            if (mBooFirstDir)
            {
                rootDir = dir.DirectoryPath();
                @where.WriteLine("<h1>" + dirTitle + "</h1>");
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
                            @where.WriteLine(GetHeadingOpen(dir) + hyperlinkDir + GetHeadingClose());
                        }
                        else
                        {
                            @where.WriteLine(GetHeadingOpen(dir) + dirTitle + GetHeadingClose());

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
                    if (fDivWrap(dir))
                    {
                        where.WriteLine(html.OpenDiv());
                    }

                    if (double.Parse(Regex.Split(dir.DirectorySizeToString(cDir.BinaryPrefix.Kibibytes), " ")[0]) > 0 | dir.GetFilesColl().Count > 0)
                    {

                        if (mSettings.GetConfig().ShowFileCount)
                        {
                            int fc = mFilter.GetFilesCollFiltered(dir).Count;
                            if (fc > 0)
                            {
                                where.WriteLine(html.OpenPara("foldercount"));
                                where.WriteLine("Files Count: " + fc.ToString());
                                where.WriteLine(html.ClosePara());

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
                        where.WriteLine(html.OpenPara(""));
                        where.WriteLine(mSettings.GetOptions().EmptyFolderMessage + html.AddBreak());
                        where.WriteLine(html.ClosePara());
                    }

                    if (dir.GetFilesColl().Count > 0)
                    {

                        // Check if there is AT LEAST ONE valid file
                        bool booPrintList = false;
                        foreach (cFile fp in dir.GetFilesColl())
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
                                switch (mSettings.GetConfig().ListType)
                                {
                                    case IndexerConfig.eListType.Bullets:
                                        @where.WriteLine(html.OpenBulletedList());
                                        break;
                                    case IndexerConfig.eListType.Numbered:
                                        @where.WriteLine(html.OpenNumberedList());
                                        break;
                                }
                            }

                            if (mSettings.GetConfig().RevereFileOrder)
                            {
                                dir.GetFilesColl().Reverse();
                            }

                            foreach (cFile f in dir.GetFilesColl())
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
                                        string fileSize = f.GetSizeToString(cDir.BinaryPrefix.Mebibytes);
                                        if (double.Parse(Regex.Split(fileSize, "\\" + mDecimalSymbol)[0]) == 0)
                                        {
                                            fileSize = f.GetSizeToString(cDir.BinaryPrefix.Kibibytes);
                                        }
                                        lLine = html.GetValidXhtmlLine(strFilePath) + " " + html.GetSpan(string.Format(" [{0}]", fileSize), "filesize");
                                    }
                                    else
                                    {
                                        lLine = html.GetValidXhtmlLine(strFilePath);
                                    }

                                    if (mSettings.GetConfig().AudioInfo && fIsAudio(f.GetFileExtension().ToLower()) == true)
                                    {

                                        try
                                        {
                                            TagLib.File audioFile = TagLib.File.Create(f.GetFilePath());
                                            double fsize = f.GetSize(cDir.BinaryPrefix.Kibibits);
                                            double dura = audioFile.Properties.Duration.TotalSeconds;

                                            if (dura > 0)
                                            {
                                                Console.WriteLine(fsize / dura);
                                                lLine += html.GetSpan(string.Format(" [{0} kb/s]", (fsize / dura).ToString("0.00"), fGetHMS(audioFile.Properties.Duration.TotalSeconds)), "audioinfo");
                                                lLine += html.GetSpan(string.Format(" [{0}]", fGetHMS(audioFile.Properties.Duration.TotalSeconds)), "audiolength");
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
                                switch (mSettings.GetConfig().ListType)
                                {
                                    case IndexerConfig.eListType.Bullets:
                                        @where.WriteLine(html.CloseBulletedList());
                                        break;
                                    case IndexerConfig.eListType.Numbered:
                                        @where.WriteLine(html.CloseNumberedList());
                                        break;
                                }

                            }
                            // Show Files for TreeNet
                        }
                    }


                    mNumTabs += 1;

                    foreach (cDir d in dir.GetSubDirColl())
                    {

                        cDir sd = new cDir(d.DirectoryPath());

                        sd = IndexToHtmlFile(d, @where);
                    }

                    if (fDivWrap(dir))
                    {
                        where.WriteLine(html.CloseDiv());
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

        private void IndexRootFolderToHtml(string folderPath, StreamWriter sw, bool AddFooter)
        {

            HtmlHelper html = new HtmlHelper();

            if (mBooFirstIndexFile)
            {

                sw.WriteLine(html.GetDocType());
                if (mSettings.GetConfig().CollapseFolders)
                {
                    sw.WriteLine(html.GetCollapseJs());
                    sw.WriteLine(html.GetCollapseCss());
                }
                sw.WriteLine(html.GetCssStyle(mSettings.GetConfig().CssFilePath));

                if (mBooMoreFilesToCome)
                {
                    sw.WriteLine(html.GetTitle(mSettings.GetConfig().MergedHtmlTitle));
                    mBooMoreFilesToCome = false;
                }
                else
                {
                    string[] c = folderPath.Split(Path.DirectorySeparatorChar);
                    if (c[1].Length == 0)
                    {
                        sw.WriteLine(html.GetTitle("Index for " + folderPath));
                    }
                    else
                    {
                        sw.WriteLine(html.GetTitle("Index for " + Path.GetFileName(folderPath)));

                    }
                }

                sw.WriteLine(html.CloseHead());
                sw.WriteLine(html.OpenBody());

                mBooFirstIndexFile = false;
            }

            cDir rootDir = new cDir(folderPath);

            rootDir = Analyze(rootDir.DirectoryPath());

            this.IndexToHtmlFile(rootDir, sw);

            if (AddFooter)
            {
                sw.WriteLine(html.OpenDiv());
                sw.WriteLine("____" + html.AddBreak());
                sw.WriteLine(mSettings.getFooterText(null, IndexingEngine.TreeNetLib, true));
                sw.WriteLine(html.CloseDiv());
                sw.WriteLine(html.CloseBody());
            }


            mBooFirstDir = true;
        }

        private void IndexFolderToTxt(string folderPath, StreamWriter sw, bool AddFooter)
        {

            if (Directory.Exists(folderPath))
            {

                // 2.7.1.6 TreeGUI crashed on Could not find a part of the path 

                cDir dir = new cDir(folderPath);
                dir = Analyze(dir.DirectoryPath());
                this.IndexToTxtFile(dir, sw);
                if (AddFooter)
                {
                    sw.WriteLine("____");
                    sw.WriteLine(mSettings.getFooterText(null, IndexingEngine.TreeNetLib, false));

                }

            }
        }

        private cDir IndexToTxtFile(cDir dir, StreamWriter @where)
        {

            string dirSize = fGetDirSizeString(dir);

            string dirTitle = string.Format("{0} [{1}]", dir.DirectoryName(), dirSize);

            string strStars = "";
            char[] styleArray = mSettings.GetConfig().FolderHeadingStyle.ToCharArray();

            for (int i = 0; i <= dirTitle.Length - 1; i++)
            {
                strStars += styleArray[i % styleArray.Length];
            }

            @where.WriteLine("");
            @where.WriteLine(GetTabs() + strStars);
            @where.WriteLine(GetTabs() + dirTitle);
            @where.WriteLine(GetTabs() + strStars);

            if (double.Parse(Regex.Split(dir.DirectorySizeToString(cDir.BinaryPrefix.Kibibytes), " ")[0]) == 0)
            {
                where.WriteLine(GetTabs() + mSettings.GetOptions().EmptyFolderMessage);
            }

            if (mSettings.GetConfig().ShowFilesTreeNet)
            {

                foreach (cFile fp in dir.GetFilesColl())
                {

                    string fileDesc = GetTabs() + "  ";

                    if (mSettings.GetConfig().ShowFileSize)
                    {
                        string fileSize = fp.GetSizeToString(cDir.BinaryPrefix.Mebibytes);
                        if (double.Parse(Regex.Split(fileSize, "\\" + mDecimalSymbol)[0]) == 0)
                        {
                            fileSize = fp.GetSizeToString(cDir.BinaryPrefix.Kibibytes);
                        }
                        fileDesc += string.Format("{0} [{1}]", fp.GetFileName(), fileSize);
                    }
                    else
                    {
                        fileDesc += fp.GetFileName();
                    }

                    if (mSettings.GetConfig().AudioInfo && fIsAudio(fp.GetFileExtension().ToLower()) == true)
                    {

                        try
                        {
                            TagLib.File audioFile = TagLib.File.Create(fp.GetFilePath());
                            double fsize = fp.GetSize(cDir.BinaryPrefix.Kibibits);
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
                            Console.WriteLine(ex.ToString() + "\n" + fp.GetFilePath());

                        }
                    }



                    @where.WriteLine(fileDesc);

                }
            }

            mNumTabs += 1;

            foreach (cDir d in dir.GetSubDirColl())
            {

                cDir sd = new cDir(d.DirectoryPath());
                sd = IndexToTxtFile(d, @where);
            }
            mNumTabs -= 1;


            return dir;
        }

        int mNumTabs = 0;

        public override void IndexNow(IndexingMode IndexMode)
        {

            string where = null;

            List<string> folderList = new List<string>();
            folderList = mSettings.GetConfig().FolderList;
            TreeNetIndexer treeNetLib = new TreeNetIndexer(mSettings);

            string ext = mSettings.GetConfig().IndexFileExtension;

            switch (IndexMode)
            {

                case IndexingMode.IN_EACH_DIRECTORY:

                    IndexInEachDir(mSettings);

                    break;
                case IndexingMode.IN_ONE_FOLDER_MERGED:

                    if (mSettings.GetConfig().isMergeFiles)
                    {
                        where = mSettings.fGetIndexFilePath(-1, IndexingMode.IN_ONE_FOLDER_MERGED);

                        StreamWriter sw = new StreamWriter(where, false);

                        if (mSettings.GetConfig().FolderList.Count > 1)
                        {
                            for (int i = 0; i <= mSettings.GetConfig().FolderList.Count - 2; i++)
                            {
                                string strDirPath = mSettings.GetConfig().FolderList[i];
                                cDir dir = new cDir(strDirPath);

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

                        cDir lastDir = new cDir(mSettings.GetConfig().FolderList[mSettings.GetConfig().FolderList.Count - 1]);
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
                            mSettings.ZipAdminFile(where, null);
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
                        @where = mSettings.fGetIndexFilePath(i, IndexingMode.IN_ONE_FOLDER_SEPERATE);
                        //MsgBox(where = mSettings.GetConfig().OutputDir + "\" + sDrive + sep + sDirName + sep + mSettings.GetConfig().GetIndexFileName)

                        StreamWriter sw = new StreamWriter(@where, false);

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
                            mSettings.ZipAdminFile(where, null);
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

            string ext = myReader.GetConfig().IndexFileExtension;

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

                    StreamWriter sw = new StreamWriter(@where, false);

                    try
                    {
                        this.CurrentDirMessage = "Indexing " + strDirPath;

                        if (ext.Contains("html"))
                        {
                            //MessageBox.Show(myReader.GetConfig().mCssFilePath)
                            treeNetLib.mBooFirstIndexFile = true;
                            treeNetLib.IndexRootFolderToHtml(strDirPath, sw, true);
                        }
                        else
                        {
                            treeNetLib.IndexFolderToTxt(strDirPath, sw, true);
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

        private string GetHeadingOpen(cDir dir)
        {

            string cName = "trigger";
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
                if (dir.GetSubDirColl().Count > 0 || mFilter.GetFilesCollFiltered(dir).Count > 0)
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
                if (dir.GetSubDirColl().Count > 0 || mFilter.GetFilesCollFiltered(dir).Count > 0)
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
                string tabs = "</h" + mNumTabs.ToString() + ">";
                return tabs;
            }
            else
            {
                string tabs = "</h6>";
                return tabs;

            }
        }

    }


}


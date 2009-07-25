using System.Windows.Forms;
using System.Collections.Generic;
using System.IO;
using System;

namespace ZSS.IndexersLib
{
    [Serializable()]
    public class IndexerConfig
    {        
        //Default to Tree.NET
        public System.Threading.ThreadPriority ProcessPriority;

        // Main Form 
        private List<string> m_FolderList = new List<string>();

        private string m_IndexFileExtension = ".html";
        //Because default is Tree.NET
        private string m_IndexFileName = "index";

        // Config > Engine > Tree 
        private bool m_AddFilesTree = true;
        private bool m_Ascii = true;

        // Config > Output 
        private bool m_IndexFileInSameDir = true;
        public bool CreateIndividualFilesInOutputDir = true;

        // Engine > TreeNet 
        public bool ShowFilesTreeNet = true;
        public bool ShowFileSize = true;
        public bool ShowDirSize = true;
        public bool ShowFilePath { get; set; }
        public bool ShowFolderPath { get; set; }
        public bool HideExtension { get; set; }
        public bool ShowVirtualFolders { get; set; }
        public bool ShowFolderPathOnStatusBar { get; set; }

        public string FolderHeadingStyle = "*~";

        // Engine > TreeNet > XHTML Output

        private eListType mListType = eListType.Bullets;

        // Zip Operations
        public bool ZipAfterIndexed = false;
        public bool ZipAndDeleteFile = false;
        public bool ZipMergedFile = false;
        public bool ZipFilesInEachDir = true;
        public bool ZipFilesInOutputDir = false;

        // Filter

        public bool EnabledFiltering = true;
        public bool HideProtectedOperatingSystemFilesFolders = false;

        public bool IgnoreHiddenFiles = false;
        public bool IgnoreSystemFiles = false;
        public bool IgnoreFollowingFiles = true;
        const string mDefaultCssFileName = "Default.css";
        public string IgnoreFilesList = ".DS_Store|*.db|index.html|*.ini";
        public bool IgnoreHiddenFolders = false;
        public bool IgnoreSystemFolders = false;
        public bool IgnoreEmptyFolders = false;
        //war59312

        // Misc
        public string MergedHtmlTitle = "Site Index";
        public string ServerInfo = "ftp://127.0.0.1:21";

        public bool RevereFileOrder = false;
        public bool ShowFileCount = false;

        private List<string> mVirtualFolderList = new List<string>();

        public bool mSortBySize = false;
        public int mSortBySizeMode = 1;
        //  0 = Ascending, 1 = Descending

        public bool AudioQuickScan { get; set; }
        public bool AudioInfo = true;

        private int mFolderExpandLevel = 2;
        public int FolderExpandLevel
        {
            get { return mFolderExpandLevel; }
            set { mFolderExpandLevel = value; }
        }

        private bool mCollapseFolders = false;
        public bool CollapseFolders
        {
            get { return mCollapseFolders; }
            set { mCollapseFolders = value; }
        }

        private string mLogoPath;
        public string LogoPath
        {
            get { return mLogoPath; }
            set { mLogoPath = value; }
        }



        // 2.7.1.4 Default CSS file is read from Application startup path 
        // default.css when current file does not exit
        private string mCssFilePath = Application.StartupPath + mDefaultCssFileName;

        public IndexerConfig()
        {
            AudioQuickScan = true;
        }

        public string CssFileName
        {
            get { return mDefaultCssFileName; }
        }

        public string CssFilePath
        {

            get { return this.mCssFilePath; }

            set
            {
                if (File.Exists(value))
                {
                    this.mCssFilePath = value;
                }
                else
                {
                    mCssFilePath = Application.StartupPath + Path.DirectorySeparatorChar + mDefaultCssFileName;
                }
            }
        }



        public List<string> VirtualFolderList
        {
            get { return mVirtualFolderList; }
            set { mVirtualFolderList = value; }
        }


        public enum eListType
        {
            Bullets,
            Numbered
        }

        public eListType ListType
        {
            get { return mListType; }
            set { mListType = value; }
        }

        public string GetIndexFileName()
        {
            return this.m_IndexFileName + this.m_IndexFileExtension;
        }

        public string[] GetIndexFilePaths()
        {

            string[] paths = new string[FolderList.Count];

            for (int i = 0; i <= FolderList.Count - 1; i++)
            {
                paths[i] = FolderList[i] + Path.DirectorySeparatorChar + this.GetIndexFileName();
            }


            return paths;
        }

        public string GetIndexFilePath()
        {
            return this.OutputDir + Path.DirectorySeparatorChar + this.GetIndexFileName();
        }

        public List<string> FolderList
        {
            get { return m_FolderList; }
            set { this.m_FolderList = value; }
        }


        public string IndexFileName
        {
            get { return this.m_IndexFileName; }
            set { this.m_IndexFileName = value; }
        }

        public string IndexFileExtension
        {

            get { return this.m_IndexFileExtension; }
            set { m_IndexFileExtension = value; }
        }

        public bool isIndexFileInSameDir
        {
            get { return m_IndexFileInSameDir; }
            set { this.m_IndexFileInSameDir = value; }
        }

        private bool isIndexFileInOneDir { get; set; }

        public bool RemoveTreeBranches { get; set; }

        public bool isMergeFiles { get; set; }
        public bool isAscii
        {
            get { return this.m_Ascii; }
            set { m_Ascii = value; }
        }

        public bool isAddFiles
        {
            get { return this.m_AddFilesTree; }
            set { m_AddFilesTree = value; }
        }

        public string OutputDir { get; set; }

        public IndexingEngine IndexingEngineID { get; set; }

        public void SetSingleIndexPath(string fpath)
        {

            OutputDir = Path.GetDirectoryName(fpath);
            IndexFileName = Path.GetFileNameWithoutExtension(fpath);
            IndexFileExtension = Path.GetExtension(fpath);

            isMergeFiles = true;
        }
    }
}


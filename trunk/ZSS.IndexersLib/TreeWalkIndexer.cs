using System.IO;
using System;
using System.Diagnostics;

namespace ZSS.IndexersLib
{
    public class TreeWalkIndexer : Indexer
    {

        //* Indexes a Root Folder using tree.com

        private IndexerAdapter mSettings = new IndexerAdapter();
        private string m_CurrentIndexFilePath;
        private bool isFirstEntryToSingleFile = true;

        private IndexerAdapter GetReader()
        {
            return this.mSettings;
        }

        private void setCurrentIndexFilePath(string filePath)
        {
            this.m_CurrentIndexFilePath = filePath;
        }

        public TreeWalkIndexer(IndexerAdapter myReader)
            : base(myReader)
        {
            this.mSettings = myReader;
            if (this.mSettings.GetConfig().IndexFileExt.Contains("html"))
            {
                this.mSettings.GetConfig().IndexFileExt = ".txt";
            }
        }

        private string getAddFilesSwitch()
        {

            if (mSettings.GetConfig().TreeShowFiles)
            {
                return " /f";
            }

            return null;
        }


        private string getAsciiSwitch()
        {

            if (mSettings.GetConfig().TreeUseAscii == true)
            {
                return " /a";
            }

            return null;
        }

        private string getOutputSwitch(string folderPath, IndexingMode mode)
        {

            switch (mode)
            {

                case IndexingMode.IN_EACH_DIRECTORY:
                    this.setCurrentIndexFilePath(folderPath + Path.DirectorySeparatorChar + mSettings.GetConfig().GetIndexFileName());

                    return ">" + (char)34 + this.getCurrentIndexFilePath() + (char)34;
                case IndexingMode.IN_ONE_FOLDER_SEPERATE:

                    string strOutputFileName = null;
                    string sep = mSettings.GetOptions().IndividualIndexFileWordSeperator;
                    if (Path.GetFileName(folderPath).Length > 0)
                    {
                        strOutputFileName = Path.GetPathRoot(folderPath).Substring(0, 1) + sep + Path.GetFileName(folderPath) + sep + mSettings.GetConfig().GetIndexFileName();
                    }
                    else
                    {
                        strOutputFileName = Path.GetPathRoot(folderPath).Substring(0, 1) + sep + mSettings.GetConfig().GetIndexFileName();
                    }

                    this.setCurrentIndexFilePath(mSettings.GetConfig().OutputDir + Path.DirectorySeparatorChar + strOutputFileName);

                    return ">" + (char)34 + this.getCurrentIndexFilePath() + (char)34;
                case IndexingMode.IN_ONE_FOLDER_MERGED:                    
                    if (mSettings.GetConfig().MergeFiles)
                    {
                        this.setCurrentIndexFilePath(this.mSettings.GetConfig().GetIndexFilePath());
                        if (isFirstEntryToSingleFile == true)
                        {
                            isFirstEntryToSingleFile = false;
                            return ">" + (char)34 + this.getCurrentIndexFilePath() + (char)34;
                        }
                        else
                        {
                            return ">>" + (char)34 + this.getCurrentIndexFilePath() + (char)34;
                        }
                    }
                    break;
            }


            return null;
        }

        public override void IndexNow(IndexingMode mIndexMode)
        {
            TreeWalkIndexer tree = new TreeWalkIndexer(mSettings);
            bool isMergeFile = mSettings.GetConfig().MergeFiles;
            bool isRemoveBranches = mSettings.GetConfig().RemoveTreeBranches;

            for (int i = 0; i <= mSettings.GetConfig().FolderList.Count - 1; i++)
            {
                string TEMP_FILE = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\temp" + i.ToString() + ".bat";
                string CURRENT_DIR = mSettings.GetConfig().FolderList[i];
                string TREE_COMMAND = "%windir%\\system32\\tree.com " + tree.getSourceSwitch(CURRENT_DIR) + tree.getAsciiSwitch() + tree.getAddFilesSwitch() + tree.getOutputSwitch(CURRENT_DIR, mIndexMode);
                Console.WriteLine(TREE_COMMAND);
                using (StreamWriter sw = new StreamWriter(TEMP_FILE))
                {
                    sw.WriteLine(TREE_COMMAND);
                    //1.5.3.4 Didn't tag index files created in the same folder witout appending
                    if (mIndexMode == IndexingMode.IN_EACH_DIRECTORY | i == mSettings.GetConfig().FolderList.Count - 1 | (isMergeFile == false & mIndexMode == IndexingMode.IN_ONE_FOLDER_MERGED))
                    {
                        sw.WriteLine(mSettings.getBlankLine(tree.getCurrentIndexFilePath()));
                        sw.WriteLine(mSettings.GetFooterText(tree.getCurrentIndexFilePath(), IndexingEngine.TreeLib, false));
                    }
                    sw.WriteLine("DEL " + (char)34 + TEMP_FILE + (char)34);
                }
                Process proc = new Process();
                proc = mSettings.StartHiddenProcess(TEMP_FILE, true);

                if (isRemoveBranches)
                {
                    tree.removeTreeBranches(tree.getCurrentIndexFilePath());
                }

                if (mIndexMode == IndexingMode.IN_EACH_DIRECTORY | i == mSettings.GetConfig().FolderList.Count - 1 | (isMergeFile == false & mIndexMode == IndexingMode.IN_ONE_FOLDER_MERGED))
                {
                    if (mSettings.GetConfig().ZipFilesInEachDir)
                    {
                        mSettings.ZipAdminFile(tree.getCurrentIndexFilePath(), null);
                    }
                    if (mSettings.GetConfig().ZipMergedFile)
                    {
                        mSettings.ZipAdminFile(tree.getCurrentIndexFilePath(), null);
                    }
                }

                if (mIndexMode == IndexingMode.IN_ONE_FOLDER_SEPERATE)
                {
                    if (mSettings.GetConfig().ZipFilesInOutputDir)
                    {
                        //MsgBox(tree.getCurrentIndexFilePath())
                        mSettings.ZipAdminFile(tree.getCurrentIndexFilePath(), null);

                    }
                }

                if (proc.HasExited)
                {
                    this.Progress += 1;
                    this.CurrentDirMessage = "Indexed " + mSettings.GetConfig().FolderList[i];
                }

            }
        }


        private void removeTreeBranches(string filePath)
        {
            try
            {
                StreamReader sr = new StreamReader(filePath);
                string file = sr.ReadToEnd();
                file = file.Replace("---", " ");
                file = file.Replace(" +", "  ");
                file = file.Replace("+ ", "  ");
                file = file.Replace("|", "    ");
                // 4 spaces
                file = file.Replace(" \\", "  ");
                file = file.Replace("\\ ", "  ");
                sr.Close();
                StreamWriter sw = new StreamWriter(filePath);
                sw.WriteLine(file);
                sw.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            // Do Nothing
        }

        private string getSourceSwitch(string folderPath)
        {
            return (char)34 + folderPath + (char)34;
        }

        private string getCurrentIndexFilePath()
        {
            return this.m_CurrentIndexFilePath;
        }

    }


}


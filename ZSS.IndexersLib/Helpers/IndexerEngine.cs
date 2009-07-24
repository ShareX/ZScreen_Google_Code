using System.Windows.Forms;
using System.Collections.Generic;
using System.IO;
using System;

namespace ZSS.IndexersLib
{
    public class Indexer
    {

        private int mProgress = 0;
        private string mCurrentDirMsg = "Analyzing...";

        private IndexerAdapter mSettings = new IndexerAdapter();

        public Indexer(IndexerAdapter settings)
        {
            mSettings = settings;
        }

        public IndexerAdapter GetSettings()
        {
            return mSettings;
        }

        public int Progress
        {
            get { return mProgress; }
            set { mProgress = value; }
        }

        public string CurrentDirMessage
        {
            get { return mCurrentDirMsg; }
            protected set { mCurrentDirMsg = value; }
        }


        public virtual void IndexNow(IndexingMode mIndexMode)
        {

            // Does not seem to reach here 

            List<string> fixedFolderList = new List<string>();
            List<string> dneFolderList = new List<string>();

            foreach (string dirPath in mSettings.GetConfig().FolderList)
            {
                if (Directory.Exists(dirPath))
                {
                    fixedFolderList.Add(dirPath);
                }
                else
                {
                    dneFolderList.Add(dirPath);
                }
            }

            if (dneFolderList.Count > 0)
            {
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                foreach (string dp in dneFolderList)
                {
                    sb.AppendLine(dp);
                }
                MessageBox.Show("Following Index folders do not exist:" + Environment.NewLine + Environment.NewLine + sb.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


            // Overrides by Sub Classes

            mSettings.GetConfig().FolderList = fixedFolderList;
        }

    }

}

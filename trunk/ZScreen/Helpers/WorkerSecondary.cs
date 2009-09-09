#region License Information (GPL v2)
/*
    ZScreen - A program that allows you to upload screenshots in one keystroke.
    Copyright (C) 2008-2009  Brandon Zimmerman

    This program is free software; you can redistribute it and/or
    modify it under the terms of the GNU General Public License
    as published by the Free Software Foundation; either version 2
    of the License, or (at your option) any later version.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with this program; if not, write to the Free Software
    Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301, USA.
    
    Optionally you can also view the license at <http://www.gnu.org/licenses/>.
*/
#endregion

using System;
using System.ComponentModel;
using System.Windows.Forms;
using UploadersLib.TextServices;
using ZScreenGUI.Properties;
using ZScreenLib;
using ZSS;
using ZSS.UpdateCheckerLib;
using UploadersLib;

namespace ZScreenGUI
{
    /// <summary>
    /// Class responsible for all other tasks except for Image Uploading
    /// </summary>
    public class WorkerSecondary
    {
        private ZScreen mZScreen;

        public WorkerSecondary(ZScreen myZScreen)
        {
            this.mZScreen = myZScreen;
        }

        #region Check Updates

        public void CheckUpdates()
        {
            mZScreen.btnCheckUpdate.Enabled = false;
            mZScreen.lblUpdateInfo.Text = "Checking for Updates...";
            BackgroundWorker updateThread = new BackgroundWorker { WorkerReportsProgress = true };
            updateThread.DoWork += new DoWorkEventHandler(updateThread_DoWork);
            updateThread.ProgressChanged += new ProgressChangedEventHandler(updateThread_ProgressChanged);
            updateThread.RunWorkerCompleted += new RunWorkerCompletedEventHandler(updateThread_RunWorkerCompleted);
            updateThread.RunWorkerAsync(Application.ProductName);
        }

        private void updateThread_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            switch (e.ProgressPercentage)
            {
                case 1:
                    mZScreen.lblUpdateInfo.Text = (string)e.UserState;
                    break;
            }
        }

        private void updateThread_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = (BackgroundWorker)sender;
            NewVersionWindowOptions nvwo = new NewVersionWindowOptions { MyIcon = Resources.zss_main, MyImage = Resources.main };

            UpdateCheckerOptions uco = new UpdateCheckerOptions
            {
                CheckBeta = Engine.conf.CheckUpdatesBeta,
                MyNewVersionWindowOptions = nvwo
            };
            uco.ProxySettings = Adapter.CheckProxySettings().GetWebProxy;
            UpdateChecker updateChecker = new UpdateChecker((string)e.Argument, uco);
            worker.ReportProgress(1, updateChecker.StartCheckUpdate());
            updateChecker.ShowPrompt();
        }

        private void updateThread_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            mZScreen.btnCheckUpdate.Enabled = true;
        }

        #endregion

        #region Cache Cleaner Methods

        public void CleanCache()
        {
            BackgroundWorker bw = new BackgroundWorker();
            bw.DoWork += new System.ComponentModel.DoWorkEventHandler(BwCache_DoWork);
            bw.RunWorkerAsync();
        }

        private void BwCache_DoWork(object sender, DoWorkEventArgs e)
        {
            CacheCleanerTask t = new CacheCleanerTask(Engine.CacheDir, Engine.conf.ScreenshotCacheSize);
            t.CleanCache();
        }

        #endregion

        public void LoadHistoryItems()
        {
            mZScreen.cbHistorySave.Checked = Engine.conf.HistorySave;
            if (mZScreen.cbHistoryListFormat.Items.Count == 0)
            {
                mZScreen.cbHistoryListFormat.Items.AddRange(typeof(HistoryListFormat).GetDescriptions());
            }

            mZScreen.cbHistoryListFormat.SelectedIndex = (int)Engine.conf.HistoryListFormat;
            mZScreen.cbShowHistoryTooltip.Checked = Engine.conf.HistoryShowTooltips;
            mZScreen.cbHistoryAddSpace.Checked = Engine.conf.HistoryAddSpace;
            mZScreen.cbHistoryReverseList.Checked = Engine.conf.HistoryReverseList;

            BackgroundWorker bwHistoryReader = new BackgroundWorker();
            bwHistoryReader.DoWork += new DoWorkEventHandler(bwHistoryReader_DoWork);
            bwHistoryReader.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bwHistoryReader_RunWorkerCompleted);
            bwHistoryReader.RunWorkerAsync();
        }

        private void bwHistoryReader_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            HistoryManager history = (HistoryManager)e.Result;

            mZScreen.lbHistory.Items.Clear();

            for (int i = 0; i < history.HistoryItems.Count && i < Engine.conf.HistoryMaxNumber; i++)
            {
                mZScreen.lbHistory.Items.Add(history.HistoryItems[i]);
            }

            if (mZScreen.lbHistory.Items.Count > 0)
            {
                mZScreen.lbHistory.SelectedIndex = 0;
            }
        }

        private void bwHistoryReader_DoWork(object sender, DoWorkEventArgs e)
        {
            FileSystem.AppendDebug("Reading History file...");
            e.Result = HistoryManager.Read();
        }

        public void PerformOnlineTasks()
        {
            BackgroundWorker bwOnlineWorker = new BackgroundWorker();
            bwOnlineWorker.DoWork += new DoWorkEventHandler(bwOnlineTasks_DoWork);
            bwOnlineWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bwOnlineTasks_RunWorkerCompleted);
            bwOnlineWorker.RunWorkerAsync();
        }

        private void bwOnlineTasks_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
            	if (null != Uploader.ProxySettings) {
	                ZScreen.mGTranslator = new GoogleTranslate(Uploader.ProxySettings.GetWebProxy);   
	                Adapter.UpdateTinyPicShuk();            		
            	}
            }
            catch (Exception ex)
            {
                FileSystem.AppendDebug(ex.ToString());
            }
        }

        private void bwOnlineTasks_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (ZScreen.mGTranslator != null)
            {
                mZScreen.cbFromLanguage.Items.Clear();
                mZScreen.cbToLanguage.Items.Clear();
                foreach (GoogleTranslate.GTLanguage gtLang in ZScreen.mGTranslator.LanguageOptions.SourceLangList)
                {
                    mZScreen.cbFromLanguage.Items.Add(gtLang.Name);
                }

                foreach (GoogleTranslate.GTLanguage gtLang in ZScreen.mGTranslator.LanguageOptions.TargetLangList)
                {
                    mZScreen.cbToLanguage.Items.Add(gtLang.Name);
                }

                SelectLanguage(Engine.conf.FromLanguage, Engine.conf.ToLanguage, Engine.conf.HelpToLanguage);
                GoogleTranslate.GTLanguage secondLang = GoogleTranslate.FindLanguage(Engine.conf.ToLanguage2,
                    ZScreen.mGTranslator.LanguageOptions.TargetLangList);
                if (secondLang != null)
                {
                    mZScreen.btnTranslateTo1.Text = "To " + secondLang.Name;
                }

                if (mZScreen.cbFromLanguage.Items.Count > 0)
                {
                    mZScreen.cbFromLanguage.Enabled = true;
                }

                if (mZScreen.cbToLanguage.Items.Count > 0)
                {
                    mZScreen.cbToLanguage.Enabled = true;
                }
            }

            if (!string.IsNullOrEmpty(Engine.conf.TinyPicShuk) && Engine.conf.TinyPicShuk != mZScreen.txtTinyPicShuk.Text)
            {
                mZScreen.txtTinyPicShuk.Text = Engine.conf.TinyPicShuk;
            }
        }

        public void SelectLanguage(string srcLangValue, string targetLangValue, string helpTargetLangValue)
        {
            for (int i = 0; i < ZScreen.mGTranslator.LanguageOptions.SourceLangList.Count; i++)
            {
                if (ZScreen.mGTranslator.LanguageOptions.SourceLangList[i].Value == srcLangValue)
                {
                    if (mZScreen.cbFromLanguage.Items.Count > i)
                    {
                        mZScreen.cbFromLanguage.SelectedIndex = i;
                    }

                    break;
                }
            }

            for (int i = 0; i < ZScreen.mGTranslator.LanguageOptions.TargetLangList.Count; i++)
            {
                if (ZScreen.mGTranslator.LanguageOptions.TargetLangList[i].Value == targetLangValue)
                {
                    if (mZScreen.cbToLanguage.Items.Count > i)
                    {
                        mZScreen.cbToLanguage.SelectedIndex = i;
                    }
                }
            }
        }

        #region Test FTP Account asynchronously

        public void TestFTPAccountAsync(FTPAccount acc)
        {
            if (acc != null)
            {
                mZScreen.ucFTPAccounts.btnTest.Enabled = false;
                BackgroundWorker bw = new BackgroundWorker();
                bw.DoWork += new DoWorkEventHandler(bw_DoWorkTestFTPAccount);
                bw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bw_RunWorkerCompletedTestFTPAccount);
                bw.RunWorkerAsync(acc);
            }
        }

        private void bw_DoWorkTestFTPAccount(object sender, DoWorkEventArgs e)
        {
            Adapter.TestFTPAccount((FTPAccount)e.Argument);
        }

        private void bw_RunWorkerCompletedTestFTPAccount(object sender, RunWorkerCompletedEventArgs e)
        {
            mZScreen.ucFTPAccounts.btnTest.Enabled = true;
        }

        #endregion
    }
}
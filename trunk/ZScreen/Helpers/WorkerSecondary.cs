#region License Information (GPL v2)

/*
    ZScreen - A program that allows you to upload screenshots in one keystroke.
    Copyright (C) 2008-2011 ZScreen Developers

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

#endregion License Information (GPL v2)

using System;
using System.ComponentModel;
using System.Windows.Forms;
using UploadersLib;
using UploadersLib.OtherServices;
using ZScreenGUI.Properties;
using ZScreenLib;
using ZSS.UpdateCheckerLib;
using ZAPILib;

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
            updateThread.RunWorkerAsync();
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
            UpdateChecker updateChecker = new UpdateChecker(Engine.URL_UPDATE, Application.ProductName, new Version(Application.ProductVersion),
                Engine.conf.ReleaseChannel, Adapter.CheckProxySettings().GetWebProxy, nvwo);
            worker.ReportProgress(1, updateChecker.CheckUpdate());
            updateChecker.ShowPrompt();
        }

        private void updateThread_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            mZScreen.btnCheckUpdate.Enabled = true;
        }

        #endregion Check Updates

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

        #endregion Cache Cleaner Methods

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
                if (Engine.conf.GoogleLanguages == null || Engine.conf.GoogleLanguages.Count < 1)
                {
                    Engine.conf.GoogleLanguages = new GoogleTranslate(ZKeys.GoogleTranslateKey).GetLanguages();
                }

                if (Uploader.ProxySettings != null)
                {
                    Adapter.UpdateTinyPicShuk();
                }

                if (Adapter.CheckFTPAccounts())
                {
                    Adapter.TestFTPAccount(Adapter.GetFtpAcctActive(), true);
                }
            }
            catch (Exception ex)
            {
                FileSystem.AppendDebug("Error while performing Online Tasks", ex);
            }
        }

        private void bwOnlineTasks_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            FillLanguages();

            if (!string.IsNullOrEmpty(Engine.conf.TinyPicShuk) && Engine.conf.TinyPicShuk != mZScreen.txtTinyPicShuk.Text)
            {
                mZScreen.txtTinyPicShuk.Text = Engine.conf.TinyPicShuk;
            }
        }

        public void FillLanguages()
        {
            if (Engine.conf.GoogleLanguages != null && Engine.conf.GoogleLanguages.Count > 0)
            {
                mZScreen.cbFromLanguage.Items.Clear();
                mZScreen.cbToLanguage.Items.Clear();

                foreach (GoogleLanguage lang in Engine.conf.GoogleLanguages)
                {
                    mZScreen.cbFromLanguage.Items.Add(lang.Name);
                    mZScreen.cbToLanguage.Items.Add(lang.Name);
                }

                SelectLanguage(Engine.conf.GoogleSourceLanguage, Engine.conf.GoogleTargetLanguage, Engine.conf.GoogleTargetLanguage2);

                if (mZScreen.cbFromLanguage.Items.Count > 0)
                {
                    mZScreen.cbFromLanguage.Enabled = true;
                }

                if (mZScreen.cbToLanguage.Items.Count > 0)
                {
                    mZScreen.cbToLanguage.Enabled = true;
                }
            }
        }

        public void SelectLanguage(string sourceLanguage, string targetLanguage, string targetLanguage2)
        {
            for (int i = 0; i < Engine.conf.GoogleLanguages.Count; i++)
            {
                if (Engine.conf.GoogleLanguages[i].Language == sourceLanguage)
                {
                    if (mZScreen.cbFromLanguage.Items.Count > i)
                    {
                        mZScreen.cbFromLanguage.SelectedIndex = i;
                    }

                    break;
                }
            }

            for (int i = 0; i < Engine.conf.GoogleLanguages.Count; i++)
            {
                if (Engine.conf.GoogleLanguages[i].Language == targetLanguage)
                {
                    if (mZScreen.cbToLanguage.Items.Count > i)
                    {
                        mZScreen.cbToLanguage.SelectedIndex = i;
                    }

                    break;
                }
            }

            mZScreen.btnTranslateTo1.Text = "To " + GetLanguageName(targetLanguage2);
        }

        public string GetLanguageName(string language)
        {
            foreach (GoogleLanguage gl in Engine.conf.GoogleLanguages)
            {
                if (gl.Language == language) return gl.Name;
            }

            return string.Empty;
        }

        public int GetLanguageIndex(string language)
        {
            for (int i = 0; i < Engine.conf.GoogleLanguages.Count; i++)
            {
                if (Engine.conf.GoogleLanguages[i].Language == language) return i;
            }

            return -1;
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
            Adapter.TestFTPAccount((FTPAccount)e.Argument, false);
        }

        private void bw_RunWorkerCompletedTestFTPAccount(object sender, RunWorkerCompletedEventArgs e)
        {
            mZScreen.ucFTPAccounts.btnTest.Enabled = true;
        }

        #endregion Test FTP Account asynchronously
    }
}
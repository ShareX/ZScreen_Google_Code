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
using UploadersAPILib;
using UploadersLib;
using UploadersLib.OtherServices;
using ZScreenGUI.Properties;
using ZScreenLib;
using ZSS.UpdateCheckerLib;

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
                if (Engine.MyGTConfig.GoogleLanguages == null || Engine.MyGTConfig.GoogleLanguages.Count < 1)
                {
                    Engine.MyGTConfig.GoogleLanguages = new GoogleTranslate(ZKeys.GoogleTranslateKey).GetLanguages();
                }

                if (Uploader.ProxySettings != null)
                {
                    // TODO: Method to update TinyPic RegCode automatically
                }

                if (Adapter.CheckFTPAccounts())
                {
                    Adapter.TestFTPAccount(Adapter.GetFtpAcctActive(), true);
                }
            }
            catch (Exception ex)
            {
                Engine.MyLogger.WriteException("Error while performing Online Tasks", ex);
            }
        }

        private void bwOnlineTasks_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            FillLanguages();
        }

        public void FillLanguages()
        {
            if (Loader.MyGTGUI != null && Engine.MyGTConfig.GoogleLanguages != null && Engine.MyGTConfig.GoogleLanguages.Count > 0)
            {
                Loader.MyGTGUI.cbFromLanguage.Items.Clear();
                Loader.MyGTGUI.cbToLanguage.Items.Clear();

                foreach (GoogleLanguage lang in Engine.MyGTConfig.GoogleLanguages)
                {
                    Loader.MyGTGUI.cbFromLanguage.Items.Add(lang.Name);
                    Loader.MyGTGUI.cbToLanguage.Items.Add(lang.Name);
                }

                SelectLanguage(Engine.MyGTConfig.GoogleSourceLanguage, Engine.MyGTConfig.GoogleTargetLanguage, Engine.MyGTConfig.GoogleTargetLanguage2);

                if (Loader.MyGTGUI.cbFromLanguage.Items.Count > 0)
                {
                    Loader.MyGTGUI.cbFromLanguage.Enabled = true;
                }

                if (Loader.MyGTGUI.cbToLanguage.Items.Count > 0)
                {
                    Loader.MyGTGUI.cbToLanguage.Enabled = true;
                }
            }
        }

        public void SelectLanguage(string sourceLanguage, string targetLanguage, string targetLanguage2)
        {
            for (int i = 0; i < Engine.MyGTConfig.GoogleLanguages.Count; i++)
            {
                if (Engine.MyGTConfig.GoogleLanguages[i].Language == sourceLanguage)
                {
                    if (Loader.MyGTGUI.cbFromLanguage.Items.Count > i)
                    {
                        Loader.MyGTGUI.cbFromLanguage.SelectedIndex = i;
                    }

                    break;
                }
            }

            for (int i = 0; i < Engine.MyGTConfig.GoogleLanguages.Count; i++)
            {
                if (Engine.MyGTConfig.GoogleLanguages[i].Language == targetLanguage)
                {
                    if (Loader.MyGTGUI.cbToLanguage.Items.Count > i)
                    {
                        Loader.MyGTGUI.cbToLanguage.SelectedIndex = i;
                    }

                    break;
                }
            }

            Loader.MyGTGUI.btnTranslateTo1.Text = "To " + GetLanguageName(targetLanguage2);
        }

        public string GetLanguageName(string language)
        {
            foreach (GoogleLanguage gl in Engine.MyGTConfig.GoogleLanguages)
            {
                if (gl.Language == language) return gl.Name;
            }

            return string.Empty;
        }

        public int GetLanguageIndex(string language)
        {
            for (int i = 0; i < Engine.MyGTConfig.GoogleLanguages.Count; i++)
            {
                if (Engine.MyGTConfig.GoogleLanguages[i].Language == language) return i;
            }

            return -1;
        }
    }
}
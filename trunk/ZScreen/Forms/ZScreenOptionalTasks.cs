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
using HelpersLib;
using UploadersAPILib;
using UploadersLib;
using ZScreenGUI.Properties;
using ZScreenLib;
using ZSS.UpdateCheckerLib;

namespace ZScreenGUI
{
    /// <summary>
    /// Class responsible for all other tasks except for Image Uploading
    /// </summary>
    public partial class ZScreen : HotkeyForm
    {
        private Timer tmrTinyPicRegCodeUpdater = new Timer() { Interval = 3 * 3600 * 1000, Enabled = true };

        #region Check Updates

        public void CheckUpdates()
        {
            btnCheckUpdate.Enabled = false;
            lblUpdateInfo.Text = "Checking for Updates...";
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
                    lblUpdateInfo.Text = (string)e.UserState;
                    break;
            }
        }

        private void updateThread_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = (BackgroundWorker)sender;
            NewVersionWindowOptions nvwo = new NewVersionWindowOptions { MyIcon = Resources.zss_main, MyImage = Resources.main };
            UpdateChecker updateChecker = new UpdateChecker(ZLinks.URL_UPDATE, Application.ProductName,
                new Version(Adapter.AssemblyVersion),
                Engine.conf.ReleaseChannel, Adapter.CheckProxySettings().GetWebProxy, nvwo);

            updateChecker.CheckUpdate();

            string status;
            if (updateChecker.UpdateInfo.Status == UpdateStatus.UpdateCheckFailed)
            {
                status = "Update check failed";
            }
            else
            {
                status = updateChecker.UpdateInfo.ToString();
            }

            worker.ReportProgress(1, status);
            updateChecker.ShowPrompt();
        }

        private void updateThread_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            btnCheckUpdate.Enabled = true;
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

        #region Online Tasks

        public void PerformOnlineTasks()
        {
            BackgroundWorker bwOnlineWorker = new BackgroundWorker();
            bwOnlineWorker.DoWork += new DoWorkEventHandler(bwOnlineTasks_DoWork);
            bwOnlineWorker.RunWorkerAsync();

            tmrTinyPicRegCodeUpdater.Tick += new EventHandler(tmrTinyPicRegCodeUpdater_Tick);
        }

        private void tmrTinyPicRegCodeUpdater_Tick(object sender, EventArgs e)
        {
            Adapter.UpdateTinyPicRegCode();
        }

        private void bwOnlineTasks_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                Adapter.UpdateTinyPicRegCode();

                if (Adapter.CheckFTPAccounts())
                {
                    foreach (FTPAccount acc in Engine.Workflow.OutputsConfig.FTPAccountList)
                    {
                        Adapter.TestFTPAccount(acc, true);
                    }
                }
            }
            catch (Exception ex)
            {
                StaticHelper.WriteException(ex, "Error while performing Online Tasks");
            }
        }

        #endregion Online Tasks
    }
}
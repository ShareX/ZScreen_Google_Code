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
    public partial class ZScreen : ZScreenCoreUI
    {
        private Timer tmrTinyPicRegCodeUpdater = new Timer() { Interval = 3 * 3600 * 1000, Enabled = true };

        #region Cache Cleaner Methods

        public void CleanCache()
        {
            BackgroundWorker bw = new BackgroundWorker();
            bw.DoWork += new System.ComponentModel.DoWorkEventHandler(BwCache_DoWork);
            bw.RunWorkerAsync();
        }

        private void BwCache_DoWork(object sender, DoWorkEventArgs e)
        {
            CacheCleanerTask t = new CacheCleanerTask(Engine.CacheDir, Engine.ConfigUI.ScreenshotCacheSize);
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

                foreach (FTPAccount acc in Engine.ConfigUploaders.FTPAccountList2)
                {
                    Adapter.TestFTPAccount(acc, true);
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
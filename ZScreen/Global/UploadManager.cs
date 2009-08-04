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

using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using ZSS.ImageUploadersLib.Helpers;
using ZSS;

// Last working class that supports multiple screenshots histories:
// http://code.google.com/p/zscreen/source/browse/trunk/ZScreen/Global/ClipboardManager.cs?spec=svn550&r=550

namespace ZScreenLib
{
    /// <summary>
    /// Class reponsible for Adding or Retrieving Clipboard Text and Setting Text to Clipboard
    /// </summary>
    public static class UploadManager
    {
        public static List<UploadInfo> UploadInfos = new List<UploadInfo>();

        private static ImageFileManager ScreenshotsHistory = new ImageFileManager();
        private static MainAppTask MyTask;
        private static int UniqueNumber = 0;

        public static void AddTask(MainAppTask task)
        {
            MyTask = task;
            ScreenshotsHistory = task.ImageManager;
        }

        /// <summary>
        /// Function to be called when a new Worker thread starts
        /// </summary>
        public static int Queue()
        {
            int number = UniqueNumber++;
            UploadInfos.Add(new UploadInfo(number));
            return number;
        }

        public static void Clear()
        {
            UploadInfos.Clear();
        }

        /// <summary>
        /// Remove Last Screenshot from Clipboard Manager after setting to Clipboard
        /// </summary>
        public static bool Commit(int number)
        {
            UploadInfo find = GetInfo(number);
            if (find != null)
            {
                return UploadInfos.Remove(find);
            }
            return false;
        }

        public static UploadInfo GetInfo(int number)
        {
            return UploadInfos.Find(x => x.UniqueID == number);
        }

        public static ImageFileManager GetLastImageUpload()
        {
            return ScreenshotsHistory;
        }

        public static int GetAverageProgress()
        {
            return UploadInfos.Sum(x => x.UploadPercentage) / UploadInfos.Count;
        }

        /// <summary>
        /// Sets Clipboard text and returns the content
        /// </summary>
        /// <returns></returns>
        public static string SetClipboardText()
        {
            if (ScreenshotsHistory != null)
            {
                string url = ScreenshotsHistory.GetUrlByType(Program.conf.ClipboardUriMode).ToString().Trim();

                if (MyTask.MakeTinyURL) {
                	string tinyUrl = ScreenshotsHistory.GetUrlByType(ClipboardUriType.FULL_TINYURL);
                	if (!string.IsNullOrEmpty(tinyUrl)) {
                		url = tinyUrl.Trim();
                	}
                }
                	
                if (!string.IsNullOrEmpty(url))
                {
                    Clipboard.SetText(url);
                }

                return url;
            }

            return "";
        }
    }
}
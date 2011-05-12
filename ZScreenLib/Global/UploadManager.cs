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

using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using UploadersLib;
using UploadersLib.HelperClasses;
using ZScreenLib.Properties;
using System;

// Last working class that supports multiple screenshots histories:
// http://code.google.com/p/zscreen/source/browse/trunk/ZScreen/Global/ClipboardManager.cs?spec=svn550&r=550

namespace ZScreenLib
{
    /// <summary>
    /// Class reponsible for Adding or Retrieving Clipboard Text and Setting Text to Clipboard
    /// </summary>
    public static class UploadManager
    {
        public static List<UploadInfo> UploadInfoList = new List<UploadInfo>();
        public static ImageFileManager LinkManagerLast { get; set; }

        private static int UniqueNumber = 0;

        /// <summary>
        /// Function to be called when a new Worker thread starts
        /// </summary>
        public static int Queue()
        {
            int number = UniqueNumber++;
            UploadInfoList.Add(new UploadInfo(number));
            return number;
        }

        public static void Clear()
        {
            UploadInfoList.Clear();
        }

        /// <summary>
        /// Remove Last Screenshot from Clipboard Manager after setting to Clipboard
        /// </summary>
        public static bool Commit(int number)
        {
            UploadInfo find = GetInfo(number);
            if (find != null)
            {
                return UploadInfoList.Remove(find);
            }
            return false;
        }

        public static UploadInfo GetInfo(int number)
        {
            return UploadInfoList.Find(x => x.UniqueID == number);
        }

        public static int GetAverageProgress()
        {
            return UploadInfoList.Sum(x => x.UploadPercentage) / UploadInfoList.Count;
        }

        /// <summary>
        /// Sets Clipboard text and returns the content
        /// </summary>
        /// <returns></returns>
        public static void SetClipboard(WorkerTask task, bool showDialog)
        {
            string clipboardText = "";

            if (task.JobIsImageToClipboard())
            {
                Clipboard.SetImage(task.MyImage);
            }
            else if (task.WasImageToFile())
            {
                clipboardText = task.LocalFilePath;
            }
            else if (Engine.conf.ShowClipboardModeChooser || showDialog)
            {
                ClipboardOptions cmp = new ClipboardOptions(task);
                cmp.Icon = Resources.zss_main;
                if (showDialog) { cmp.ShowDialog(); } else { cmp.Show(); }
            }
            // If the user requests for the full image URL, preference is given for the Shortened URL is exists
            else if (task.Job1 == JobLevel1.Image && Engine.conf.MyClipboardUriMode == (int)ClipboardUriType.FULL)
            {
                if (task.Job3 == WorkerTask.JobLevel3.ShortenURL && !string.IsNullOrEmpty(task.LinkManager.UploadResult.ShortenedURL))
                {
                    clipboardText = task.LinkManager.UploadResult.ShortenedURL;
                }
                // If no shortened URL exists then default full URL will be used
                else
                {
                    clipboardText = task.RemoteFilePath;
                }
            }

            else
            {
                // From this point onwards app needs to respect all other Clipboard URL modes for Images
                if (task.LinkManager != null && task.Job1 == JobLevel1.Image)
                {
                    clipboardText = task.LinkManager.GetUrlByType((ClipboardUriType)Engine.conf.MyClipboardUriMode).ToString().Trim();
                }
                // Text and File catagories are still left to process. Exception for Google Translate
                else if (task.Job1 == JobLevel1.Text && task.Job2 == WorkerTask.JobLevel2.LANGUAGE_TRANSLATOR)
                {
                    if (task.TranslationInfo != null)
                    {
                        clipboardText = task.TranslationInfo.Result;
                    }
                }
                // Text and File catagories are still left to process. If shortened URL exists, preference is given to that
                else if (task.LinkManager != null && task.Job3 == WorkerTask.JobLevel3.ShortenURL && !string.IsNullOrEmpty(task.LinkManager.UploadResult.ShortenedURL))
                {
                    clipboardText = task.LinkManager.UploadResult.ShortenedURL;
                }
                // Otherwise full URL for Text or File is used
                else if (task.LinkManager != null)
                {
                    clipboardText = task.RemoteFilePath;
                }
            }

            if (!string.IsNullOrEmpty(clipboardText))
            {
                Engine.ClipboardUnhook();
                FileSystem.AppendDebug("Setting Clipboard with URL: " + clipboardText);
                clipboardText = FileSystem.GetBrowserFriendlyUrl(clipboardText);
                Clipboard.SetText(clipboardText); // auto                
                // optional deletion link
                if (task.LinkManager != null)
                {
                    string linkdel = task.LinkManager.UploadResult.DeletionURL;
                    if (!string.IsNullOrEmpty(linkdel))
                    {
                        FileSystem.AppendDebug("Deletion Link: " + linkdel);
                    }
                }

                Engine.zClipboardText = clipboardText;
                Engine.ClipboardHook(); // This is for Clipboard Monitoring - we resume monitoring the clipboard
            }
        }
    }
}
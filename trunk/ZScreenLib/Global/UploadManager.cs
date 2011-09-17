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
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using UploadersLib;
using UploadersLib.HelperClasses;
using ZScreenLib.Properties;

namespace ZScreenLib
{
    /// <summary>
    /// Class reponsible for Adding or Retrieving Clipboard Text and Setting Text to Clipboard
    /// </summary>
    public static class UploadManager
    {
        public static List<UploadInfo> UploadInfoList = new List<UploadInfo>();

        public static UploadResult UploadResultLast { get; set; }

        public static int CumulativePercentage { get; private set; }

        private static int UniqueNumber = 0;

        public static List<WorkerTask> Tasks { get; private set; }

        static UploadManager()
        {
            Tasks = new List<WorkerTask>();
        }

        /// <summary>
        /// Function to be called when a new Worker thread starts
        /// </summary>
        public static int Queue()
        {
            int number = UniqueNumber++;
            UploadInfoList.Add(new UploadInfo(number));
            return number;
        }

        public static void SetCumulativePercentatge(int perc)
        {
            CumulativePercentage = Math.Max(CumulativePercentage, perc);
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
        public static void ShowUploadResults(WorkerTask task, bool showDialog)
        {
            if (!task.MyWorkflow.ClipboardOverwrite && !Clipboard.ContainsFileDropList() && !Clipboard.ContainsImage() && !Clipboard.ContainsText() || task.MyWorkflow.ClipboardOverwrite)
            {
                if (task.UploadResults.Count > 0)
                {
                    if (task.TaskClipboardContent.Count > 1 || Engine.conf.ShowUploadResultsWindow || showDialog)
                    {
                        ClipboardOptions cmp = new ClipboardOptions(task);
                        cmp.Icon = Resources.zss_main;
                        if (showDialog) { cmp.ShowDialog(); } else { cmp.Show(); }
                    }
                }

                if (task.MyWorkflow.Outputs.Contains(OutputEnum.Clipboard))
                {
                    StringBuilder clipboardText = new StringBuilder();

                    if (task.JobIsImageToClipboard())
                    {
                        Adapter.CopyImageToClipboard(task.TempImage);
                    }
                    else if (task.TaskClipboardContent.Contains(ClipboardContentEnum.Local))
                    {
                        foreach (UploadResult ur in task.UploadResults)
                        {
                            if (Engine.conf.ConfLinkFormat.Count > 0)
                            {
                                clipboardText.AppendLine(ur.GetUrlByType((LinkFormatEnum)task.MyLinkFormat[0], ur.LocalFilePath));
                            }

                            if (!Engine.conf.ClipboardAppendMultipleLinks && clipboardText.Length > 0)
                            {
                                break;
                            }
                        }
                    }

                // If the user requests for the full image URL, preference is given for the Shortened URL is exists
                    else if (task.Job1 == JobLevel1.Image && task.MyLinkFormat.Contains((int)LinkFormatEnum.FULL))
                    {
                        if (task.Job3 == WorkerTask.JobLevel3.ShortenURL && !string.IsNullOrEmpty(task.UploadResults[0].ShortenedURL))
                        {
                            foreach (UploadResult ur in task.UploadResults)
                            {
                                if (!string.IsNullOrEmpty(ur.ShortenedURL))
                                {
                                    clipboardText.AppendLine(ur.ShortenedURL);
                                    if (!Engine.conf.ClipboardAppendMultipleLinks && clipboardText.Length > 0)
                                    {
                                        break;
                                    }
                                }
                            }
                        }
                        // If no shortened URL exists then default full URL will be used
                        else
                        {
                            foreach (UploadResult ur in task.UploadResults)
                            {
                                if (!string.IsNullOrEmpty(ur.URL))
                                {
                                    clipboardText.AppendLine(FileSystem.GetBrowserFriendlyUrl(ur.URL));
                                    if (!Engine.conf.ClipboardAppendMultipleLinks && clipboardText.Length > 0)
                                    {
                                        break;
                                    }
                                }
                            }
                            if (clipboardText.Length == 0 && task.TaskClipboardContent.Contains(ClipboardContentEnum.Local))
                            {
                                foreach (UploadResult ur in task.UploadResults)
                                {
                                    if (!string.IsNullOrEmpty(ur.LocalFilePath))
                                    {
                                        clipboardText.AppendLine(ur.LocalFilePath);
                                        if (!Engine.conf.ClipboardAppendMultipleLinks && clipboardText.Length > 0)
                                        {
                                            break;
                                        }
                                    }
                                }
                            }
                        }
                    }

                    else
                    {
                        // From this point onwards app needs to respect all other Clipboard URL modes for Images
                        if (task.UploadResults.Count > 0 && task.Job1 == JobLevel1.Image)
                        {
                            foreach (UploadResult ur in task.UploadResults)
                            {
                                if (task.MyLinkFormat.Count > 0)
                                {
                                    clipboardText.AppendLine(ur.GetUrlByType((LinkFormatEnum)task.MyLinkFormat[0], ur.URL));
                                }

                                if (!Engine.conf.ClipboardAppendMultipleLinks && clipboardText.Length > 0)
                                {
                                    break;
                                }
                            }
                        }
                        // Text and File catagories are still left to process. Exception for Google Translate
                        else if (task.Job1 == JobLevel1.Text && task.Job2 == WorkerTask.JobLevel2.Translate)
                        {
                            if (task.TranslationInfo != null)
                            {
                                clipboardText.AppendLine(task.TranslationInfo.Result);
                            }
                        }
                        // Text and File catagories are still left to process. If shortened URL exists, preference is given to that
                        else if (task.UploadResults.Count > 0 && task.Job3 == WorkerTask.JobLevel3.ShortenURL && !string.IsNullOrEmpty(task.UploadResults[0].ShortenedURL))
                        {
                            foreach (UploadResult ur in task.UploadResults)
                            {
                                if (!string.IsNullOrEmpty(ur.ShortenedURL))
                                {
                                    clipboardText.AppendLine(ur.ShortenedURL);
                                    break;
                                }
                            }
                        }
                        // Otherwise full URL for Text or File is used
                        else if (task.UploadResults.Count > 0)
                        {
                            clipboardText.AppendLine(FileSystem.GetBrowserFriendlyUrl(task.UploadResults[0].URL));
                        }
                    }

                    if (clipboardText.Length > 0)
                    {
                        string tempText = clipboardText.ToString().Trim();

                        if (Engine.conf.ClipboardShowFileSize && !string.IsNullOrEmpty(task.FileSize))
                        {
                            tempText += " " + task.FileSize;
                        }
                        if (!string.IsNullOrEmpty(tempText))
                        {
                            Engine.MyLogger.WriteLine("Setting Clipboard with URL: " + tempText);
                            Engine.zPreviousClipboardText = tempText;
                            Clipboard.SetText(tempText); // auto
                            // optional deletion link
                            if (task.UploadResults != null)
                            {
                                foreach (UploadResult ur in task.UploadResults)
                                {
                                    string linkdel = ur.DeletionURL;
                                    if (!string.IsNullOrEmpty(linkdel))
                                    {
                                        Engine.MyLogger.WriteLine("Deletion Link: " + linkdel);
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        public static void ResetCumulativePercentage()
        {
            CumulativePercentage = 0;
        }
    }
}
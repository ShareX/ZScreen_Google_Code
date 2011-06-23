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
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using UploadersLib;
using UploadersLib.HelperClasses;
using ZScreenLib.Properties;

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

        public static UploadResult UploadResultLast { get; set; }

        public static int CumulativePercentage { get; private set; }

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
        public static void SetClipboard(IntPtr handle, WorkerTask task, bool showDialog)
        {
            if (task.UploadResults.Count > 0)
            {
                if (task.MyClipboardContent.Count > 1 || Engine.conf.ShowUploadResultsWindow || showDialog)
                {
                    ClipboardOptions cmp = new ClipboardOptions(task);
                    cmp.Icon = Resources.zss_main;
                    if (showDialog) { cmp.ShowDialog(); } else { cmp.Show(); }
                }
            }

            string clipboardText = string.Empty;

            if (task.JobIsImageToClipboard())
            {
                MemoryStream ms = new MemoryStream();
                MemoryStream ms2 = new MemoryStream();
                Bitmap bmp = new Bitmap(task.TempImage);
                bmp.Save(ms, ImageFormat.Bmp);
                byte[] b = ms.GetBuffer();
                ms2.Write(b, 14, (int)ms.Length - 14);
                ms.Position = 0;
                DataObject dataObject = new DataObject();
                dataObject.SetData(DataFormats.Bitmap, bmp);
                dataObject.SetData(DataFormats.Dib, ms2);
                Clipboard.SetDataObject(dataObject, true, 3, 1000);
            }
            else if (task.MyClipboardContent.Contains(ClipboardContentEnum.Local))
            {
                foreach (UploadResult ur in task.UploadResults)
                {
                    if (Engine.conf.ConfLinkFormat.Count > 0)
                    {
                        clipboardText = ur.GetUrlByType((LinkFormatEnum)task.MyLinkFormat[0], ur.LocalFilePath);
                    }

                    if (!string.IsNullOrEmpty(clipboardText))
                    {
                        break;
                    }
                }
            }

        // If the user requests for the full image URL, preference is given for the Shortened URL is exists
            else if (task.Job1 == JobLevel1.Image && Engine.conf.ConfLinkFormat.Contains((int)LinkFormatEnum.FULL))
            {
                if (task.Job3 == WorkerTask.JobLevel3.ShortenURL && !string.IsNullOrEmpty(task.UploadResults[0].ShortenedURL))
                {
                    foreach (UploadResult ur in task.UploadResults)
                    {
                        if (!string.IsNullOrEmpty(ur.ShortenedURL))
                        {
                            clipboardText = ur.ShortenedURL;
                            break;
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
                            clipboardText = FileSystem.GetBrowserFriendlyUrl(ur.URL);
                            break;
                        }
                    }
                    if (string.IsNullOrEmpty(clipboardText) && task.MyClipboardContent.Contains(ClipboardContentEnum.Local))
                    {
                        foreach (UploadResult ur in task.UploadResults)
                        {
                            if (!string.IsNullOrEmpty(ur.LocalFilePath))
                            {
                                clipboardText = ur.LocalFilePath;
                                break;
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
                        if (Engine.conf.ConfLinkFormat.Count > 0)
                        {
                            clipboardText = ur.GetUrlByType((LinkFormatEnum)task.MyLinkFormat[0], ur.URL);
                        }

                        if (!string.IsNullOrEmpty(clipboardText))
                        {
                            break;
                        }
                    }
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
                else if (task.UploadResults.Count > 0 && task.Job3 == WorkerTask.JobLevel3.ShortenURL && !string.IsNullOrEmpty(task.UploadResults[0].ShortenedURL))
                {
                    foreach (UploadResult ur in task.UploadResults)
                    {
                        if (!string.IsNullOrEmpty(ur.ShortenedURL))
                        {
                            clipboardText = ur.ShortenedURL;
                            break;
                        }
                    }
                }
                // Otherwise full URL for Text or File is used
                else if (task.UploadResults.Count > 0)
                {
                    clipboardText = FileSystem.GetBrowserFriendlyUrl(task.UploadResults[0].URL);
                }
            }

            if (!string.IsNullOrEmpty(clipboardText))
            {
                Engine.ClipboardUnhook();
                Engine.MyLogger.WriteLine("Setting Clipboard with URL: " + clipboardText);
                Clipboard.SetText(clipboardText.ToString()); // auto
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

                Engine.zClipboardText = clipboardText.ToString();
                Engine.ClipboardHook(); // This is for Clipboard Monitoring - we resume monitoring the clipboard
            }
        }

        public static void ResetCumulativePercentage()
        {
            CumulativePercentage = 0;
        }
    }
}
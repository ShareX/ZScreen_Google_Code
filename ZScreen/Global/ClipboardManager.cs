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
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZSS.ImageUploader;
using ZSS.ImageUploader.Helpers;
using System.Windows.Forms;

namespace ZSS
{
    /// <summary>
    /// Class reponsible for Adding or Retrieving Clipboard Text and Setting Text to Clipboard
    /// </summary>
    public static class ClipboardManager
    {
        public static int Workers { get; private set; }
        private static List<ImageFileManager> ScreenshotsHistory = new List<ImageFileManager>();

        public static void AddScreenshotList(ImageFileManager screeshotsList)
        {
            ScreenshotsHistory.Add(screeshotsList);
        }

        /// <summary>
        /// Function to be called when a new Worker thread starts
        /// </summary>
        public static void Queue()
        {
            Workers++;
            Console.WriteLine("Clipboard Queued. Total: " + Workers);
        }

        public static void Clear()
        {
            ScreenshotsHistory.Clear();
            Workers = 0;
        }

        /// <summary>
        /// Remove Last Screenshot from Clipboard Manager after setting to Clipboard
        /// </summary>
        public static void Commit()
        {
            Workers--;
            if (Workers > 1)
            {
                ScreenshotsHistory.Remove(GetLastImageUpload());
            }
            Console.WriteLine("Clipboard Commited. Total: " + Workers);
        }

        public static ImageFileManager GetLastImageUpload()
        {
            if (ScreenshotsHistory.Count > 0)
            {
                return ScreenshotsHistory[ScreenshotsHistory.Count - 1];
            }

            return null;
        }

        private static string GetLastURL()
        {

            string url = "";
            if (ScreenshotsHistory.Count > 0)
            {
                ImageFileManager lastScreenshotList = GetLastImageUpload();
                if (lastScreenshotList != null)
                {
                    url = lastScreenshotList.URL;
                }
            }
            return url;
        }

        /// <summary>
        /// Returns a string representation of URLs per Screenshot List
        /// </summary>
        /// <param name="ifl"></param>
        /// <returns></returns>
        private static List<string> GetClipboardText(ImageFileManager ifl)
        {
            List<string> lCbLines = new List<string>();

            if (ifl != null)
            {
                switch (Program.conf.ClipboardUriMode)
                {
                    case ClipboardUriType.FULL:
                        lCbLines.Add(ifl.GetFullImageUrl());
                        break;
                    case ClipboardUriType.FULL_IMAGE_FORUMS:
                        lCbLines.Add(ifl.GetFullImageForumsUrl());
                        break;
                    case ClipboardUriType.LINKED_THUMBNAIL:
                        lCbLines.Add(ifl.GetLinkedThumbnailUrl());
                        break;
                    case ClipboardUriType.THUMBNAIL:
                        lCbLines.Add(ifl.GetThumbnailUrl());
                        break;
                }
            }

            return lCbLines;

        }

        public static void SetClipboardText()
        {
            try
            {
                List<List<string>> lCbLines = new List<List<string>>();

                if (Workers > 1)
                {
                    foreach (ImageFileManager list in ScreenshotsHistory)
                    {
                        lCbLines.Add(GetClipboardText(list));
                    }
                }
                else if (Workers == 1)
                {
                    lCbLines.Add(GetClipboardText(GetLastImageUpload()));
                }

                StringBuilder sbLines = new StringBuilder();
                foreach (List<string> list in lCbLines)
                {
                    foreach (string line in list)
                    {
                        sbLines.AppendLine(line);
                    }
                }

                string temp = sbLines.ToString();
                if (!string.IsNullOrEmpty(temp))
                {
                    Clipboard.Clear();
                    Clipboard.SetText(temp.Trim());
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

        }
    }
}
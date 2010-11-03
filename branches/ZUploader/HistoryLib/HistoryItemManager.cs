#region License Information (GPL v2)

/*
    ZUploader - A program that allows you to upload images, texts or files
    Copyright (C) 2010 ZScreen Developers

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

using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace HistoryLib
{
    public class HistoryItemManager
    {
        public bool IsURLExist { get; private set; }
        public bool IsThumbnailURLExist { get; private set; }
        public bool IsDeletionURLExist { get; private set; }
        public bool IsFilePathValid { get; private set; }
        public bool IsFileExist { get; private set; }
        public bool IsFolderExist { get; private set; }
        public bool IsImageFile { get; private set; }
        public bool IsTextFile { get; private set; }

        private HistoryItem hi;

        public HistoryItemManager(HistoryItem historyItem)
        {
            hi = historyItem;

            if (hi != null)
            {
                IsURLExist = !string.IsNullOrEmpty(hi.URL);
                IsThumbnailURLExist = !string.IsNullOrEmpty(hi.ThumbnailURL);
                IsDeletionURLExist = !string.IsNullOrEmpty(hi.DeletionURL);
                IsFilePathValid = !string.IsNullOrEmpty(hi.Filepath) && Path.HasExtension(hi.Filepath);
                IsFileExist = IsFilePathValid && File.Exists(hi.Filepath);
                IsFolderExist= IsFilePathValid && Directory.Exists(Path.GetDirectoryName(hi.Filepath));
                IsImageFile = IsFileExist && Helpers.IsImageFile(hi.Filepath);
                IsTextFile = IsFileExist && Helpers.IsTextFile(hi.Filepath);
            }
        }

        public void OpenURL()
        {
            if (hi != null && IsURLExist) Process.Start(hi.URL);
        }

        public void OpenThumbnailURL()
        {
            if (hi != null && IsThumbnailURLExist) Process.Start(hi.ThumbnailURL);
        }

        public void OpenDeletionURL()
        {
            if (hi != null && IsDeletionURLExist) Process.Start(hi.DeletionURL);
        }

        public void OpenFile()
        {
            if (hi != null && IsFileExist) Process.Start(hi.Filepath);
        }

        public void OpenFolder()
        {
            if (hi != null && IsFolderExist) Process.Start(Path.GetDirectoryName(hi.Filepath));
        }

        public void CopyURL()
        {
            if (hi != null && IsURLExist) Clipboard.SetText(hi.URL);
        }

        public void CopyThumbnailURL()
        {
            if (hi != null && IsThumbnailURLExist) Clipboard.SetText(hi.ThumbnailURL);
        }

        public void CopyDeletionURL()
        {
            if (hi != null && IsDeletionURLExist) Clipboard.SetText(hi.DeletionURL);
        }

        public void CopyFile()
        {
            if (hi != null && IsFileExist) Helpers.CopyFileToClipboard(hi.Filepath);
        }

        public void CopyImage()
        {
            if (hi != null && IsImageFile) Helpers.CopyImageToClipboard(hi.Filepath);
        }

        public void CopyText()
        {
            if (hi != null && IsTextFile) Helpers.CopyTextToClipboard(hi.Filepath);
        }

        public void CopyHTMLLink()
        {
            if (hi != null && IsURLExist) Clipboard.SetText(string.Format("<a href=\"{0}\">{0}</a>", hi.URL));
        }

        public void CopyHTMLImage()
        {
            if (hi != null && IsURLExist) Clipboard.SetText(string.Format("<img src=\"{0}\"/>", hi.URL));
        }

        public void CopyHTMLLinkedImage()
        {
            if (hi != null && IsURLExist && IsThumbnailURLExist)
            {
                Clipboard.SetText(string.Format("<a href=\"{0}\"><img src=\"{1}\"/></a>", hi.URL, hi.ThumbnailURL));
            }
        }

        public void CopyForumLink()
        {
            if (hi != null && IsURLExist) Clipboard.SetText(string.Format("[url]{0}[/url]", hi.URL));
        }

        public void CopyForumImage()
        {
            if (hi != null && IsURLExist) Clipboard.SetText(string.Format("[img]{0}[/img]", hi.URL));
        }

        public void CopyForumLinkedImage()
        {
            if (hi != null && IsURLExist && IsThumbnailURLExist)
            {
                Clipboard.SetText(string.Format("[url={0}][img]{1}[/img][/url]", hi.URL, hi.ThumbnailURL));
            }
        }

        public void CopyFilePath()
        {
            if (hi != null && IsFilePathValid) Clipboard.SetText(hi.Filepath);
        }

        public void CopyFileName()
        {
            if (hi != null && IsFilePathValid) Clipboard.SetText(Path.GetFileNameWithoutExtension(hi.Filepath));
        }

        public void CopyFileNameWithExtension()
        {
            if (hi != null && IsFilePathValid) Clipboard.SetText(Path.GetFileName(hi.Filepath));
        }

        public void CopyFolder()
        {
            if (hi != null && IsFilePathValid) Clipboard.SetText(Path.GetDirectoryName(hi.Filepath));
        }

        public void DeleteLocalFile()
        {
            if (hi != null && IsFileExist && MessageBox.Show("Do you want to delete this file?\n" + hi.Filepath,
                "Delete Local File", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                File.Delete(hi.Filepath);
            }
        }
    }
}
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
        public HistoryItem HistoryItem { get; private set; }

        public bool IsURLExist { get; private set; }
        public bool IsThumbnailURLExist { get; private set; }
        public bool IsDeletionURLExist { get; private set; }
        public bool IsImageURL { get; private set; }
        public bool IsTextURL { get; private set; }
        public bool IsFilePathValid { get; private set; }
        public bool IsFileExist { get; private set; }
        public bool IsFolderExist { get; private set; }
        public bool IsImageFile { get; private set; }
        public bool IsTextFile { get; private set; }

        public HistoryItemManager(HistoryItem historyItem)
        {
            HistoryItem = historyItem;

            if (HistoryItem != null)
            {
                IsURLExist = !string.IsNullOrEmpty(HistoryItem.URL);
                IsThumbnailURLExist = !string.IsNullOrEmpty(HistoryItem.ThumbnailURL);
                IsDeletionURLExist = !string.IsNullOrEmpty(HistoryItem.DeletionURL);
                IsImageURL = IsURLExist && Helpers.IsImageFile(HistoryItem.URL);
                IsTextURL = IsURLExist && Helpers.IsTextFile(HistoryItem.URL);
                IsFilePathValid = !string.IsNullOrEmpty(HistoryItem.Filepath) && Path.HasExtension(HistoryItem.Filepath);
                IsFileExist = IsFilePathValid && File.Exists(HistoryItem.Filepath);
                IsFolderExist= IsFilePathValid && Directory.Exists(Path.GetDirectoryName(HistoryItem.Filepath));
                IsImageFile = IsFileExist && Helpers.IsImageFile(HistoryItem.Filepath);
                IsTextFile = IsFileExist && Helpers.IsTextFile(HistoryItem.Filepath);
            }
        }

        public void OpenURL()
        {
            if (HistoryItem != null && IsURLExist) Process.Start(HistoryItem.URL);
        }

        public void OpenThumbnailURL()
        {
            if (HistoryItem != null && IsThumbnailURLExist) Process.Start(HistoryItem.ThumbnailURL);
        }

        public void OpenDeletionURL()
        {
            if (HistoryItem != null && IsDeletionURLExist) Process.Start(HistoryItem.DeletionURL);
        }

        public void OpenFile()
        {
            if (HistoryItem != null && IsFileExist) Process.Start(HistoryItem.Filepath);
        }

        public void OpenFolder()
        {
            if (HistoryItem != null && IsFolderExist) Process.Start(Path.GetDirectoryName(HistoryItem.Filepath));
        }

        public void CopyURL()
        {
            if (HistoryItem != null && IsURLExist) Clipboard.SetText(HistoryItem.URL);
        }

        public void CopyThumbnailURL()
        {
            if (HistoryItem != null && IsThumbnailURLExist) Clipboard.SetText(HistoryItem.ThumbnailURL);
        }

        public void CopyDeletionURL()
        {
            if (HistoryItem != null && IsDeletionURLExist) Clipboard.SetText(HistoryItem.DeletionURL);
        }

        public void CopyFile()
        {
            if (HistoryItem != null && IsFileExist) Helpers.CopyFileToClipboard(HistoryItem.Filepath);
        }

        public void CopyImage()
        {
            if (HistoryItem != null && IsImageFile) Helpers.CopyImageToClipboard(HistoryItem.Filepath);
        }

        public void CopyText()
        {
            if (HistoryItem != null && IsTextFile) Helpers.CopyTextToClipboard(HistoryItem.Filepath);
        }

        public void CopyHTMLLink()
        {
            if (HistoryItem != null && IsURLExist) Clipboard.SetText(string.Format("<a href=\"{0}\">{0}</a>", HistoryItem.URL));
        }

        public void CopyHTMLImage()
        {
            if (HistoryItem != null && IsImageURL) Clipboard.SetText(string.Format("<img src=\"{0}\"/>", HistoryItem.URL));
        }

        public void CopyHTMLLinkedImage()
        {
            if (HistoryItem != null && IsImageURL && IsThumbnailURLExist)
            {
                Clipboard.SetText(string.Format("<a href=\"{0}\"><img src=\"{1}\"/></a>", HistoryItem.URL, HistoryItem.ThumbnailURL));
            }
        }

        public void CopyForumLink()
        {
            if (HistoryItem != null && IsURLExist) Clipboard.SetText(string.Format("[url]{0}[/url]", HistoryItem.URL));
        }

        public void CopyForumImage()
        {
            if (HistoryItem != null && IsImageURL) Clipboard.SetText(string.Format("[img]{0}[/img]", HistoryItem.URL));
        }

        public void CopyForumLinkedImage()
        {
            if (HistoryItem != null && IsImageURL && IsThumbnailURLExist)
            {
                Clipboard.SetText(string.Format("[url={0}][img]{1}[/img][/url]", HistoryItem.URL, HistoryItem.ThumbnailURL));
            }
        }

        public void CopyFilePath()
        {
            if (HistoryItem != null && IsFilePathValid) Clipboard.SetText(HistoryItem.Filepath);
        }

        public void CopyFileName()
        {
            if (HistoryItem != null && IsFilePathValid) Clipboard.SetText(Path.GetFileNameWithoutExtension(HistoryItem.Filepath));
        }

        public void CopyFileNameWithExtension()
        {
            if (HistoryItem != null && IsFilePathValid) Clipboard.SetText(Path.GetFileName(HistoryItem.Filepath));
        }

        public void CopyFolder()
        {
            if (HistoryItem != null && IsFilePathValid) Clipboard.SetText(Path.GetDirectoryName(HistoryItem.Filepath));
        }

        public void DeleteLocalFile()
        {
            if (HistoryItem != null && IsFileExist && MessageBox.Show("Do you want to delete this file?\n" + HistoryItem.Filepath,
                "Delete Local File", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                File.Delete(HistoryItem.Filepath);
            }
        }

        public void MoreInfo()
        {
            new HistoryItemInfoForm(this).Show();
        }
    }
}
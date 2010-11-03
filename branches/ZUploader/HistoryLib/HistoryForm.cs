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

using System;
using System.IO;
using System.Windows.Forms;

namespace HistoryLib
{
    public partial class HistoryForm : Form
    {
        private HistoryManager history;
        private HistoryItem[] historyItems;

        public HistoryForm(string databasePath)
        {
            InitializeComponent();
            history = new HistoryManager(databasePath);
        }

        private void HistoryForm_Load(object sender, EventArgs e)
        {
            historyItems = history.GetHistoryItems();
            AddHistoryItems(historyItems);
        }

        private void HistoryForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (history != null) history.Dispose();
        }

        private void AddHistoryItems(HistoryItem[] historyItems)
        {
            lvHistory.SuspendLayout();

            foreach (HistoryItem hi in historyItems)
            {
                ListViewItem lvi = new ListViewItem(hi.DateTimeLocalString);
                lvi.SubItems.Add(hi.Filename);
                lvi.SubItems.Add(hi.Type);
                lvi.SubItems.Add(hi.Host);
                lvi.SubItems.Add(hi.URL);
                lvi.Tag = hi;
                lvHistory.Items.Add(lvi);
            }

            lvHistory.ResumeLayout(true);
        }

        private void cmsHistory_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel =  !UpdateHistoryMenu();
        }

        private bool UpdateHistoryMenu()
        {
            HistoryItem hi = GetSelectedHistoryItem();

            if (hi != null)
            {
                bool URLExist = !string.IsNullOrEmpty(hi.URL);
                bool thumbnailURLExist = !string.IsNullOrEmpty(hi.ThumbnailURL);
                bool deletionURLExist = !string.IsNullOrEmpty(hi.DeletionURL);
                bool filePathValid = !string.IsNullOrEmpty(hi.Filepath) && Path.HasExtension(hi.Filepath);
                bool fileExist =  filePathValid && File.Exists(hi.Filepath);
                bool folderExist = filePathValid && Directory.Exists(Path.GetDirectoryName(hi.Filepath));
                bool isImageFile = fileExist && Helpers.IsImageFile(hi.Filepath);
                bool isTextfile = fileExist && Helpers.IsTextFile(hi.Filepath);

                // Open
                tsmiOpenURL.Enabled = URLExist;
                tsmiOpenThumbnailURL.Enabled = thumbnailURLExist;
                tsmiOpenDeletionURL.Enabled = deletionURLExist;

                tsmiOpenFile.Enabled = fileExist;
                tsmiOpenFolder.Enabled = folderExist;

                // Copy
                tsmiCopyURL.Enabled = URLExist;
                tsmiCopyThumbnailURL.Enabled = thumbnailURLExist;
                tsmiCopyDeletionURL.Enabled = deletionURLExist;

                tsmiCopyFile.Enabled = fileExist;
                tsmiCopyImage.Enabled = isImageFile;
                tsmiCopyText.Enabled = isTextfile;

                tsmiCopyHTMLLink.Enabled = URLExist;
                tsmiCopyHTMLImage.Enabled = URLExist;
                tsmiCopyHTMLLinkedImage.Enabled = URLExist && thumbnailURLExist;

                tsmiCopyForumLink.Enabled = URLExist;
                tsmiCopyForumImage.Enabled = URLExist;
                tsmiCopyForumLinkedImage.Enabled = URLExist && thumbnailURLExist;

                tsmiCopyFilePath.Enabled = filePathValid;
                tsmiCopyFileName.Enabled = filePathValid;
                tsmiCopyFileNameWithExtension.Enabled = filePathValid;
                tsmiCopyFolder.Enabled = filePathValid;

                // Delete
                tsmiDeleteLocalFile.Enabled = fileExist;

                return true;
            }

            return false;
        }

        private HistoryItem GetSelectedHistoryItem()
        {
            if (lvHistory.SelectedItems.Count > 0)
            {
                return lvHistory.SelectedItems[0].Tag as HistoryItem;
            }

            return null;
        }
    }
}
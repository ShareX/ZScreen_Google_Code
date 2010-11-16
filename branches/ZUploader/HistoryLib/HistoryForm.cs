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
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace HistoryLib
{
    public partial class HistoryForm : Form
    {
        public string DatabasePath { get; private set; }

        private HistoryManager history;
        private HistoryItemManager him;
        private HistoryItem[] allHistoryItems;

        public HistoryForm(string databasePath, string title)
        {
            InitializeComponent();
            DatabasePath = databasePath;
            this.Text = title;
            cbFilenameFilterMethod.SelectedIndex = 0; // Contains
            cbFilenameFilterCulture.SelectedIndex = 1; // Invariant culture
            pbThumbnail.SizeMode = PictureBoxSizeMode.Zoom;
            pbThumbnail.LoadingImage = Helpers.LoadImageFromResources("Loading.gif");
        }

        private void HistoryForm_Shown(object sender, EventArgs e)
        {
            RefreshHistoryItems();
        }

        private void HistoryForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (history != null) history.Dispose();
        }

        private void RefreshHistoryItems()
        {
            if (history == null)
            {
                history = new HistoryManager(DatabasePath);
            }

            allHistoryItems = history.GetHistoryItems();
            AddHistoryItems(allHistoryItems);
        }

        private void btnApplyFilters_Click(object sender, EventArgs e)
        {
            ApplyFiltersAndAdd();
        }

        private void ApplyFiltersAndAdd()
        {
            if (allHistoryItems.Length > 0)
            {
                AddHistoryItems(ApplyFilters(allHistoryItems));
            }
        }

        private HistoryItem[] ApplyFilters(HistoryItem[] historyItems)
        {
            IEnumerable<HistoryItem> result = (IEnumerable<HistoryItem>)historyItems.Clone();

            string filenameFilter = txtFilenameFilter.Text;
            if (cbFilenameFilter.Checked && !string.IsNullOrEmpty(filenameFilter))
            {
                StringComparison rule = GetStringRule();

                if (cbFilenameFilterMethod.SelectedIndex == 0) // Contains
                {
                    result = result.Where(x => x.Filename.IndexOf(filenameFilter, rule) >= 0);
                }
                else if (cbFilenameFilterMethod.SelectedIndex == 1) // Starts with
                {
                    result = result.Where(x => x.Filename.StartsWith(filenameFilter, rule));
                }
                else if (cbFilenameFilterMethod.SelectedIndex == 2) // Exact match
                {
                    result = result.Where(x => x.Filename.Equals(filenameFilter, rule));
                }
            }

            if (cbDateFilter.Checked)
            {
                DateTime fromDate = dtpFilterFrom.Value.Date;
                DateTime toDate = dtpFilterTo.Value.Date;

                result = from hi in result
                         let date = hi.DateTimeUtc.ToLocalTime().Date
                         where date >= fromDate && date <= toDate
                         select hi;
            }

            return result.ToArray();
        }

        private StringComparison GetStringRule()
        {
            bool caseSensitive = cbFilenameFilterCase.Checked;

            switch (cbFilenameFilterCulture.SelectedIndex)
            {
                case 0:
                    return caseSensitive ? StringComparison.CurrentCulture : StringComparison.CurrentCultureIgnoreCase;
                case 1:
                    return caseSensitive ? StringComparison.InvariantCulture : StringComparison.InvariantCultureIgnoreCase;
                case 3:
                    return caseSensitive ? StringComparison.CurrentCulture : StringComparison.CurrentCultureIgnoreCase;
            }

            return StringComparison.InvariantCultureIgnoreCase;
        }

        private void AddHistoryItems(HistoryItem[] historyItems)
        {
            UpdateItemCount(historyItems);

            lvHistory.SuspendLayout();
            lvHistory.Items.Clear();

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

        private void UpdateItemCount(HistoryItem[] historyItems)
        {
            StringBuilder status = new StringBuilder();

            status.Append("Total: " + allHistoryItems.Length);

            if (allHistoryItems.Length > historyItems.Length)
            {
                status.Append(", Filtered: " + historyItems.Length);
            }

            var types = from hi in historyItems
                        group hi by hi.Type into t
                        let count = t.Count()
                        orderby t.Key
                        select string.Format(", {0}: {1}", t.Key, count);

            foreach (string type in types)
            {
                status.Append(type);
            }

            tsslStatus.Text = status.ToString();
        }

        private void cmsHistory_Opening(object sender, CancelEventArgs e)
        {
            e.Cancel =  !UpdateHistoryMenu();
        }

        private bool UpdateHistoryMenu()
        {
            HistoryItem hi = GetSelectedHistoryItem();

            if (hi != null)
            {
                him = new HistoryItemManager(hi);

                // Open
                tsmiOpenURL.Enabled = him.IsURLExist;
                tsmiOpenThumbnailURL.Enabled = him.IsThumbnailURLExist;
                tsmiOpenDeletionURL.Enabled = him.IsDeletionURLExist;

                tsmiOpenFile.Enabled = him.IsFileExist;
                tsmiOpenFolder.Enabled = him.IsFolderExist;

                // Copy
                tsmiCopyURL.Enabled = him.IsURLExist;
                tsmiCopyThumbnailURL.Enabled = him.IsThumbnailURLExist;
                tsmiCopyDeletionURL.Enabled = him.IsDeletionURLExist;

                tsmiCopyFile.Enabled = him.IsFileExist;
                tsmiCopyImage.Enabled = him.IsImageFile;
                tsmiCopyText.Enabled = him.IsTextFile;

                tsmiCopyHTMLLink.Enabled = him.IsURLExist;
                tsmiCopyHTMLImage.Enabled = him.IsURLExist;
                tsmiCopyHTMLLinkedImage.Enabled = him.IsURLExist && him.IsThumbnailURLExist;

                tsmiCopyForumLink.Enabled = him.IsURLExist;
                tsmiCopyForumImage.Enabled =  him.IsURLExist;
                tsmiCopyForumLinkedImage.Enabled =  him.IsURLExist && him.IsThumbnailURLExist;

                tsmiCopyFilePath.Enabled = him.IsFilePathValid;
                tsmiCopyFileName.Enabled =  him.IsFilePathValid;
                tsmiCopyFileNameWithExtension.Enabled = him.IsFilePathValid;
                tsmiCopyFolder.Enabled =  him.IsFilePathValid;

                // Delete
                tsmiDeleteLocalFile.Enabled = him.IsFileExist;

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

        private void RemoveSelectedHistoryItem()
        {
            if (lvHistory.SelectedItems.Count > 0)
            {
                ListViewItem lvi = lvHistory.SelectedItems[0];
                HistoryItem hi = lvi.Tag as HistoryItem;

                if (hi != null)
                {
                    history.RemoveHistoryItem(hi);
                    lvHistory.Items.Remove(lvi);
                }
            }
        }

        #region Right click menu events

        private void tsmiOpenURL_Click(object sender, EventArgs e)
        {
            him.OpenURL();
        }

        private void tsmiOpenThumbnailURL_Click(object sender, EventArgs e)
        {
            him.OpenThumbnailURL();
        }

        private void tsmiOpenDeletionURL_Click(object sender, EventArgs e)
        {
            him.OpenDeletionURL();
        }

        private void tsmiOpenFile_Click(object sender, EventArgs e)
        {
            him.OpenFile();
        }

        private void tsmiOpenFolder_Click(object sender, EventArgs e)
        {
            him.OpenFolder();
        }

        private void tsmiCopyURL_Click(object sender, EventArgs e)
        {
            him.CopyURL();
        }

        private void tsmiCopyThumbnailURL_Click(object sender, EventArgs e)
        {
            him.CopyThumbnailURL();
        }

        private void tsmiCopyDeletionURL_Click(object sender, EventArgs e)
        {
            him.CopyDeletionURL();
        }

        private void tsmiCopyFile_Click(object sender, EventArgs e)
        {
            him.CopyFile();
        }

        private void tsmiCopyImage_Click(object sender, EventArgs e)
        {
            him.CopyImage();
        }

        private void tsmiCopyText_Click(object sender, EventArgs e)
        {
            him.CopyText();
        }

        private void tsmiCopyHTMLLink_Click(object sender, EventArgs e)
        {
            him.CopyHTMLLink();
        }

        private void tsmiCopyHTMLImage_Click(object sender, EventArgs e)
        {
            him.CopyHTMLImage();
        }

        private void tsmiCopyHTMLLinkedImage_Click(object sender, EventArgs e)
        {
            him.CopyHTMLLinkedImage();
        }

        private void tsmiCopyForumLink_Click(object sender, EventArgs e)
        {
            him.CopyForumLink();
        }

        private void tsmiCopyForumImage_Click(object sender, EventArgs e)
        {
            him.CopyForumImage();
        }

        private void tsmiCopyForumLinkedImage_Click(object sender, EventArgs e)
        {
            him.CopyForumLinkedImage();
        }

        private void tsmiCopyFilePath_Click(object sender, EventArgs e)
        {
            him.CopyFilePath();
        }

        private void tsmiCopyFileName_Click(object sender, EventArgs e)
        {
            him.CopyFileName();
        }

        private void tsmiCopyFileNameWithExtension_Click(object sender, EventArgs e)
        {
            him.CopyFileNameWithExtension();
        }

        private void tsmiCopyFolder_Click(object sender, EventArgs e)
        {
            him.CopyFolder();
        }

        private void tsmiDeleteFromHistory_Click(object sender, EventArgs e)
        {
            RemoveSelectedHistoryItem();
        }

        private void tsmiDeleteLocalFile_Click(object sender, EventArgs e)
        {
            him.DeleteLocalFile();
        }

        private void tsmiDeleteFromHistoryAndLocalFile_Click(object sender, EventArgs e)
        {
            RemoveSelectedHistoryItem();
            him.DeleteLocalFile();
        }

        private void tsmiMoreInfo_Click(object sender, EventArgs e)
        {
            // TODO: More Info
        }

        private void tsmiRefresh_Click(object sender, EventArgs e)
        {
            RefreshHistoryItems();
        }

        #endregion Right click menu events

        private void lvHistory_SelectedIndexChanged(object sender, EventArgs e)
        {
            HistoryItem hi = GetSelectedHistoryItem();

            if (hi != null && hi.Type == "Image")
            {
                pbThumbnail.LoadImage(hi.Filepath, hi.URL);
            }
        }
    }
}
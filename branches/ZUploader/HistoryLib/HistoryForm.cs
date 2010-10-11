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
                ListViewItem lvi = new ListViewItem(hi.ID.ToString());
                lvi.SubItems.Add(hi.Filename);
                lvi.SubItems.Add(hi.Filepath);
                lvi.SubItems.Add(hi.DateTimeLocalString);
                lvi.SubItems.Add(hi.Type);
                lvi.SubItems.Add(hi.Host);
                lvi.SubItems.Add(hi.URL);
                lvi.SubItems.Add(hi.ThumbnailURL);
                lvi.SubItems.Add(hi.DeletionURL);
                lvHistory.Items.Add(lvi);
            }

            lvHistory.ResumeLayout(true);
        }
    }
}
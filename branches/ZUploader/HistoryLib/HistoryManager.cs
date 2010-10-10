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
using System.Data;
using System.IO;
using System.Reflection;

namespace HistoryLib
{
    public class HistoryManager
    {
        private const string EmptyDatabasePath = "HistoryLib.Database.History.db3";

        private SQLiteWrapper sqlite;

        public HistoryManager(string databasePath)
        {
            CreateDatabaseIfNotExist(databasePath);
            sqlite = new SQLiteWrapper(databasePath);
            sqlite.OpenDatabase();
        }

        private bool CreateDatabaseIfNotExist(string databasePath)
        {
            if (!File.Exists(databasePath))
            {
                using (Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(EmptyDatabasePath))
                {
                    return Helpers.WriteFile(stream, databasePath);
                }
            }

            return false;
        }

        public bool AddHistoryItem(HistoryItem historyItem)
        {
            bool result = false;

            if (!string.IsNullOrEmpty(historyItem.Filename) && historyItem.DateTimeUtc != DateTime.MinValue && !string.IsNullOrEmpty(historyItem.URL))
            {
                Dictionary<string, object> parameters = new Dictionary<string, object>
                {
                    {"ID", historyItem.ID},
                    {"Filename", historyItem.Filename},
                    {"Filepath", historyItem.Filepath},
                    {"DateTime", historyItem.DateTimeUtc},
                    {"Type", historyItem.Type},
                    {"Host", historyItem.Host},
                    {"URL", historyItem.URL},
                    {"ThumbnailURL", historyItem.ThumbnailURL},
                    {"DeletionURL", historyItem.DeletionURL}
                };

                result = sqlite.Insert("History", parameters) == 1;
            }

            return result;
        }

        public HistoryItem[] GetHistoryItems()
        {
            DataTable historyTable = sqlite.ExecuteQueryDataTable("SELECT * FROM History");

            HistoryItem[] historyItems = new HistoryItem[historyTable.Rows.Count];

            for (int i = 0; i < historyItems.Length; i++)
            {
                historyItems[i] = RowToHistoryItem(historyTable.Rows[i]);
            }

            return historyItems;
        }

        private HistoryItem RowToHistoryItem(DataRow row)
        {
            HistoryItem historyItem = null;

            if (row != null)
            {
                historyItem = new HistoryItem
                {
                    ID = (int)row["ID"],
                    Filename = (string)row["Filename"],
                    Filepath =(string)row["Filepath"],
                    DateTimeUtc = (DateTime)row["DateTime"],
                    Type =(string)row["Type"],
                    Host =(string)row["Host"],
                    URL = (string)row["URL"],
                    ThumbnailURL = (string)row["ThumbnailURL"],
                    DeletionURL = (string)row["DeletionURL"]
                };
            }

            return historyItem;
        }
    }
}
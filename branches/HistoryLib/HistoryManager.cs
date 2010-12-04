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
using System.Threading;
using HelpersLib;

namespace HistoryLib
{
    public class HistoryManager : IDisposable
    {
        private const string EmptyDatabasePath = "HistoryLib.Database.History.db3";

        private SQLiteWrapper sqlite;

        public HistoryManager(string databasePath)
        {
            CreateDatabaseIfNotExist(databasePath);
            sqlite = new SQLiteWrapper(databasePath, true);
            sqlite.UseLockProtection = true;
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

        public void Dispose()
        {
            sqlite.Dispose();
        }

        public bool AddHistoryItem(HistoryItem historyItem)
        {
            if (historyItem != null && !string.IsNullOrEmpty(historyItem.Filename) && historyItem.DateTimeUtc != DateTime.MinValue && !string.IsNullOrEmpty(historyItem.URL))
            {
                Dictionary<string, object> parameters = new Dictionary<string, object>
                {
                    {"Filename", historyItem.Filename},
                    {"Filepath", historyItem.Filepath},
                    {"DateTime", historyItem.DateTimeUtc},
                    {"Type", historyItem.Type},
                    {"Host", historyItem.Host},
                    {"URL", historyItem.URL},
                    {"ThumbnailURL", historyItem.ThumbnailURL},
                    {"DeletionURL", historyItem.DeletionURL}
                };

                return sqlite.Insert("History", parameters);
            }

            return false;
        }

        public HistoryItem[] GetHistoryItems()
        {
            DataTable historyTable = sqlite.SelectAll("History");

            HistoryItem[] historyItems = new HistoryItem[historyTable.Rows.Count];

            for (int i = 0; i < historyItems.Length; i++)
            {
                historyItems[i] = (HistoryItem)historyTable.Rows[i];
            }

            return historyItems;
        }

        public bool RemoveHistoryItem(HistoryItem historyItem)
        {
            return sqlite.Delete("History", "ID", historyItem.ID.ToString()) == 1;
        }

        public static void AutomaticlyAddHistoryItemAsync(string databasePath, HistoryItem historyItem)
        {
            ThreadStart thread = () =>
            {
                using (HistoryManager history = new HistoryManager(databasePath))
                {
                    history.AddHistoryItem(historyItem);
                }
            };

            new Thread(thread).Start();
        }
    }
}
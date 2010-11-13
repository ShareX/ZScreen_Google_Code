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
using System.Data.SQLite;
using System.Linq;
using System.Threading;

namespace HistoryLib
{
    public class SQLiteWrapper : IDisposable
    {
        public string DatabasePath { get; private set; }
        public bool InTransaction { get; private set; }
        public int LockCount { get; private set; }

        public bool UseLockProtection { get; set; }
        public int LockRetryCount { get; set; }
        public int LockRetryDelay { get; set; }

        private SQLiteConnection connection;
        private SQLiteTransaction transaction;

        public SQLiteWrapper(string databasePath)
        {
            DatabasePath = databasePath;
            UseLockProtection = false;
            LockRetryCount = 10;
            LockRetryDelay = 100;
        }

        public bool OpenDatabase()
        {
            if (string.IsNullOrEmpty(DatabasePath) || InTransaction)
            {
                return false;
            }

            if (connection == null || connection.State != ConnectionState.Open)
            {
                string connectionString = string.Format("Data Source={0}; Version=3; FailIfMissing=True;", DatabasePath);
                connection = new SQLiteConnection(connectionString);
                connection.DefaultTimeout = 0;
                connection.Open();
            }

            return true;
        }

        public void CloseDatabase()
        {
            if (connection != null && connection.State == ConnectionState.Open)
            {
                EndTransaction();
                connection.Close();
                connection.Dispose();
            }
        }

        public void Dispose()
        {
            CloseDatabase();
        }

        public void BeginTransaction()
        {
            if (!InTransaction)
            {
                InTransaction = true;

                Action action = () =>
                {
                    transaction = connection.BeginTransaction();
                };

                LockProtection(action);
            }
        }

        public void EndTransaction()
        {
            if (InTransaction)
            {
                Action action = () =>
                {
                    if (transaction != null && transaction.Connection != null)
                    {
                        transaction.Commit();
                        transaction.Dispose();
                    }
                };

                LockProtection(action);

                InTransaction = false;
            }
        }

        public int ExecuteNonQuery(string query)
        {
            using (SQLiteCommand command = CreateCommand(query))
            {
                return ExecuteNonQuery(command);
            }
        }

        public int ExecuteNonQuery(SQLiteCommand command)
        {
            int result = 0;

            Action action = () =>
            {
                result = command.ExecuteNonQuery();
            };

            LockProtection(action);

            return result;
        }

        public SQLiteDataReader ExecuteQueryDataReader(string query)
        {
            SQLiteDataReader dataReader = null;

            Action action = () =>
            {
                using (SQLiteCommand command = CreateCommand(query))
                {
                    dataReader = command.ExecuteReader();
                }
            };

            LockProtection(action);

            return dataReader;
        }

        public SQLiteDataAdapter ExecuteQueryDataAdapter(string query)
        {
            SQLiteDataAdapter dataAdapter = null;

            Action action = () =>
            {
                SQLiteCommand command = CreateCommand(query);
                dataAdapter = new SQLiteDataAdapter(command);
            };

            LockProtection(action);

            return dataAdapter;
        }

        /// <summary>Using ExecuteQueryDataAdapter</summary>
        public DataSet ExecuteQueryDataSet(string query)
        {
            DataSet dataSet = new DataSet();

            using (SQLiteDataAdapter dataAdapter = ExecuteQueryDataAdapter(query))
            {
                if (dataAdapter != null)
                {
                    dataAdapter.Fill(dataSet);
                }
            }

            return dataSet;
        }

        /// <summary>Using ExecuteQueryDataAdapter</summary>
        public DataTable ExecuteQueryDataTable(string query)
        {
            DataTable dataTable = new DataTable();

            using (SQLiteDataAdapter dataAdapter = ExecuteQueryDataAdapter(query))
            {
                if (dataAdapter != null)
                {
                    dataAdapter.Fill(dataTable);
                }
            }

            return dataTable;
        }

        /// <summary>Using ExecuteQueryDataReader</summary>
        public List<List<string>> ExecuteQueryListString(string query)
        {
            List<List<string>> list = new List<List<string>>();

            using (SQLiteDataReader dataReader = ExecuteQueryDataReader(query))
            {
                if (dataReader != null)
                {
                    while (dataReader.Read())
                    {
                        List<string> row = new List<string>();

                        for (int i = 0; i < dataReader.FieldCount; i++)
                        {
                            row.Add(dataReader[i].ToString());
                        }

                        list.Add(row);
                    }
                }
            }

            return list;
        }

        public bool Insert(string table, Dictionary<string, object> parameters)
        {
            string columnNames = string.Join(", ", parameters.Keys.ToArray());
            string columnValues = string.Join(", ", parameters.Keys.Select(x => "@" + x).ToArray());
            string query = string.Format("INSERT INTO {0} ({1}) VALUES ({2})", table, columnNames, columnValues);

            using (SQLiteCommand command = CreateCommand(query))
            {
                foreach (var parameter in parameters)
                {
                    command.Parameters.AddWithValue("@" + parameter.Key, parameter.Value ?? DBNull.Value);
                }

                return ExecuteNonQuery(command) == 1;
            }
        }

        public int Delete(string table, string whereName, string whereValue)
        {
            return ExecuteNonQuery(string.Format("DELETE FROM {0} WHERE {1}='{2}'", table, whereName, whereValue));
        }

        public DataTable SelectAll(string table)
        {
            return ExecuteQueryDataTable("SELECT * FROM " + table);
        }

        private SQLiteCommand CreateCommand(string query)
        {
            return new SQLiteCommand(query, connection) { CommandTimeout = 0, CommandType = CommandType.Text };
        }

        private void LockProtection(Action action)
        {
            if (!UseLockProtection)
            {
                action();
            }
            else
            {
                lock (this)
                {
                    for (int i = 0; i < LockRetryCount; i++)
                    {
                        try
                        {
                            action();
                            break;
                        }
                        catch (Exception e)
                        {
                            if (i + 1 < LockRetryCount)
                            {
                                Thread.Sleep(LockRetryDelay);
                                continue;
                            }

                            LockCount++;

                            throw new Exception("Database is locked.\nMessage: " + e.Message);
                        }
                    }
                }
            }
        }
    }
}
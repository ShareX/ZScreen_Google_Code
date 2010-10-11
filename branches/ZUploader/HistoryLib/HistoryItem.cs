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
using System.Data;

namespace HistoryLib
{
    public class HistoryItem
    {
        public long ID { get; set; }

        private string filename;
        public string Filename
        {
            get { return filename; }
            set
            {
                if (CheckValueLength(value, 260))
                {
                    filename = value;
                }
            }
        }

        private string filepath;
        public string Filepath
        {
            get { return filepath; }
            set
            {
                if (CheckValueLength(value, 260))
                {
                    filepath = value;
                }
            }
        }

        public DateTime DateTimeUtc { get; set; }

        public string DateTimeLocalString
        {
            get
            {
                DateTime time = DateTimeUtc.ToLocalTime();
                return string.Format("{0} {1}", time.ToShortDateString(), time.ToLongTimeString());
            }
        }

        private string type;
        public string Type
        {
            get { return type; }
            set
            {
                if (CheckValueLength(value, 5))
                {
                    type = value;
                }
            }
        }

        private string host;
        public string Host
        {
            get { return host; }
            set
            {
                if (CheckValueLength(value, 50))
                {
                    host = value;
                }
            }
        }

        private string url;
        public string URL
        {
            get { return url; }
            set
            {
                if (CheckValueLength(value, 1000))
                {
                    url = value;
                }
            }
        }

        private string thumbnailurl;
        public string ThumbnailURL
        {
            get { return thumbnailurl; }
            set
            {
                if (CheckValueLength(value, 1000))
                {
                    thumbnailurl = value;
                }
            }
        }

        private string deletionurl;
        public string DeletionURL
        {
            get { return deletionurl; }
            set
            {
                if (CheckValueLength(value, 1000))
                {
                    deletionurl = value;
                }
            }
        }

        private bool CheckValueLength(string value, int length)
        {
            if (value != null && value.Length > length)
            {
                string message = string.Format("Value length must be equal or smaller than {0}.\nFirst 25 chars of this value: {1}", length, value.Substring(0, 25));
                throw new Exception(message);
            }

            return true;
        }

        public static explicit operator HistoryItem(DataRow row)
        {
            HistoryItem historyItem = null;

            if (row != null)
            {
                historyItem = new HistoryItem
                {
                    ID = row.Field<long>("ID"),
                    Filename = row.Field<string>("Filename"),
                    Filepath = row.Field<string>("Filepath"),
                    DateTimeUtc = row.Field<DateTime>("DateTime"),
                    Type = row.Field<string>("Type"),
                    Host = row.Field<string>("Host"),
                    URL = row.Field<string>("URL"),
                    ThumbnailURL = row.Field<string>("ThumbnailURL"),
                    DeletionURL = row.Field<string>("DeletionURL")
                };
            }

            return historyItem;
        }
    }
}
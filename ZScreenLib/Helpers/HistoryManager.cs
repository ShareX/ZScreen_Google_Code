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
using System.IO;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace ZScreenLib
{
    [Serializable]
    public class HistoryManager
    {
        public List<HistoryItem> HistoryItems { get; set; }

        public HistoryManager()
        {
            this.HistoryItems = new List<HistoryItem>();
        }

        public HistoryManager(List<HistoryItem> list)
        {
            this.HistoryItems = list;
        }

        public bool Save()
        {
            FileSystem.AppendDebug("Saving history file: " + Engine.HistoryFile);
            return Save(Engine.HistoryFile);
        }

        public bool Save(string filePath)
        {
            try
            {
                if (!Directory.Exists(Path.GetDirectoryName(filePath)))
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(filePath));
                }

                XmlSerializer xs = new XmlSerializer(typeof(HistoryManager));
                using (FileStream fs = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.Read))
                {
                    xs.Serialize(fs, this);
                }

                return true;
            }
            catch (Exception e)
            {
                FileSystem.AppendDebug("Error while saving history", e);
                MessageBox.Show(e.Message);
            }

            return false;
        }

        public static HistoryManager Read()
        {
            FileSystem.AppendDebug("Reading history file: " + Engine.HistoryFile);
            return Read(Engine.HistoryFile);
        }

        public static HistoryManager Read(string filePath)
        {
            try
            {
                if (!Directory.Exists(Path.GetDirectoryName(filePath)))
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(filePath));
                }

                if (File.Exists(filePath))
                {
                    XmlSerializer xs = new XmlSerializer(typeof(HistoryManager));
                    using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                    {
                        return (HistoryManager)xs.Deserialize(fs);
                    }
                }
            }
            catch (Exception e)
            {
                FileSystem.AppendDebug("Error while reading history", e);
            }

            return new HistoryManager();
        }
    }
}
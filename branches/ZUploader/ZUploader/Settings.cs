#region License Information (GPL v2)
/*
    ZUploader - A program that allows you to upload images, text or files in your clipboard
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
#endregion

using System;
using System.Diagnostics;
using System.IO;
using System.Xml.Serialization;
using UploadersLib;

namespace ZUploader
{
    public class Settings
    {
        public bool ClipboardAutoCopy { get; set; }
        public int SelectedImageUploaderDestination { get; set; }
        public int SelectedTextUploaderDestination { get; set; }
        public int SelectedFileUploaderDestination { get; set; }
        public FTPAccount FTPAccount { get; set; }

        #region Functions

        public bool Save()
        {
            return Save(Program.SettingsFilePath);
        }

        public bool Save(string path)
        {
            if (!Directory.Exists(Path.GetDirectoryName(path)))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(path));
            }

            try
            {
                XmlSerializer xs = new XmlSerializer(typeof(Settings));
                using (FileStream fs = new FileStream(path, FileMode.Create))
                {
                    xs.Serialize(fs, this);
                    return true;
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.ToString());
            }

            return false;
        }

        public static Settings Load()
        {
            return Load(Program.SettingsFilePath);
        }

        public static Settings Load(string path)
        {
            if (!Directory.Exists(Path.GetDirectoryName(path)))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(path));
            }

            if (File.Exists(path))
            {
                try
                {
                    XmlSerializer xs = new XmlSerializer(typeof(Settings));
                    using (FileStream fs = new FileStream(path, FileMode.Open))
                    {
                        return xs.Deserialize(fs) as Settings;
                    }
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e.ToString());
                }
            }

            return new Settings();
        }

        #endregion
    }
}
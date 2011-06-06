#region License Information (GPL v2)

/*
    ZUploader - A program that allows you to upload images, text or files in your clipboard
    Copyright (C) 2008-2011 ZScreen Developers

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
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace HelpersLib
{
    public enum SerializationType
    {
        Binary, Xml
    }

    public static class SettingsHelper
    {
        public static bool Save<T>(T obj, string path, SerializationType type)
        {
            StaticHelper.WriteLine("Settings save started: " + path);

            try
            {
                lock (obj)
                {
                    if (!string.IsNullOrEmpty(path))
                    {
                        string directoryName = Path.GetDirectoryName(path);

                        if (!string.IsNullOrEmpty(directoryName) && !Directory.Exists(directoryName))
                        {
                            Directory.CreateDirectory(directoryName);
                        }

                        using (FileStream fs = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.Read))
                        {
                            switch (type)
                            {
                                case SerializationType.Binary:
                                    new BinaryFormatter().Serialize(fs, obj);
                                    break;
                                case SerializationType.Xml:
                                    new XmlSerializer(typeof(T)).Serialize(fs, obj);
                                    break;
                            }

                            return true;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                StaticHelper.WriteException(e);
            }
            finally
            {
                StaticHelper.WriteLine("Settings save finished: " + path);
            }

            return false;
        }

        public static T Load<T>(string path, SerializationType type, bool onErrorShowWarning = true) where T : new()
        {
            StaticHelper.WriteLine("Settings load started: " + path);

            try
            {
                if (!string.IsNullOrEmpty(path) && File.Exists(path))
                {
                    using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read))
                    {
                        switch (type)
                        {
                            case SerializationType.Binary:
                                return (T)new BinaryFormatter().Deserialize(fs);
                            case SerializationType.Xml:
                                return (T)new XmlSerializer(typeof(T)).Deserialize(fs);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                StaticHelper.WriteException(e);

                if (onErrorShowWarning)
                {
                    string text = string.Format("Settings path:\r\n{0}\r\n\r\nError:\r\n{1}", path, e.ToString());

                    MessageBox.Show(text, "Error when loading settings file", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    /*
                    if (MessageBox.Show(text, "Error when loading settings file", MessageBoxButtons.YesNo, MessageBoxIcon.Error) == DialogResult.Yes)
                    {
                        string filter;

                        switch (type)
                        {
                            case SerializationType.Binary:
                                filter = "Binary Settings(*.bin)|*.bin";
                                break;
                            default:
                            case SerializationType.Xml:
                                filter = "XML Settings(*.xml)|*.xml";
                                break;
                        }

                        using (OpenFileDialog dlg = new OpenFileDialog { Filter =  filter })
                        {
                            dlg.Title = "Load settings file from backup: " + Path.GetFileName(path);
                            dlg.InitialDirectory = Path.GetDirectoryName(path);

                            if (dlg.ShowDialog() == DialogResult.OK) // TODO: Not working in thread
                            {
                                return Load<T>(dlg.FileName, type);
                            }
                        }
                    }*/
                }
            }
            finally
            {
                StaticHelper.WriteLine("Settings load finished: " + path);
            }

            return new T();
        }
    }
}
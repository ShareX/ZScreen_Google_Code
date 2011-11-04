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
        public static bool Save<T>(T obj, string filePath, SerializationType type)
        {
            StaticHelper.WriteLine("Settings save started: " + filePath);

            bool isSuccess = false;

            try
            {
                lock (obj)
                {
                    if (!string.IsNullOrEmpty(filePath))
                    {
                        using (MemoryStream ms = new MemoryStream())
                        {
                            switch (type)
                            {
                                case SerializationType.Binary:
                                    new BinaryFormatter().Serialize(ms, obj);
                                    break;
                                case SerializationType.Xml:
                                    new XmlSerializer(typeof(T)).Serialize(ms, obj);
                                    break;
                            }

                            isSuccess = ms.WriteToFile(filePath);
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
                string message;

                if (isSuccess)
                {
                    message = "Settings save successful";
                }
                else
                {
                    message = "Settings save failed";
                }

                StaticHelper.WriteLine(string.Format("{0}: {1}", message, filePath));
            }

            return isSuccess;
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
                        if (fs.Length > 0)
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
            }
            catch (Exception e)
            {
                StaticHelper.WriteException(e);

                if (onErrorShowWarning)
                {
                    string text = string.Format("Settings path:\r\n{0}\r\n\r\nError:\r\n{1}", path, e.ToString());

                    MessageBox.Show(text, "Error when loading settings file", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
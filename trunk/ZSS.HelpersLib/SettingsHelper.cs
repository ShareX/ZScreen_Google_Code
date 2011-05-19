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
using System.Xml.Serialization;

namespace HelpersLib
{
    public enum SerializationType
    {
        Binary, Xml
    }

    public static class SettingsHelper
    {
        public static bool Save<T>(T obj, string path, SerializationType type, Logger logger)
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

                    try
                    {
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
                    catch (Exception e)
                    {
                       logger.WriteException(e);
                    }
                }
            }

            return false;
        }

        public static T Load<T>(string path, SerializationType type, Logger logger) where T : new()
        {
            if (!string.IsNullOrEmpty(path) && File.Exists(path))
            {
                try
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
                catch (Exception e)
                {
                    logger.WriteException(e);
                }
            }

            return new T();
        }
    }
}
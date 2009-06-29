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
using System.Xml;

namespace ZSS
{
    class FTPAccountReader
    {
        public List<FTPAccount> Accounts { get; private set; }

        /// <summary>
        /// Reads a base directory for user.config files
        /// Attempts to create a list of FTPAccounts
        /// </summary>
        /// <param name="dir"></param>
        public FTPAccountReader(string dir)
        {
            readConfigFiles(dir);
        }

        private void readConfigFiles(string dir)
        {
            this.Accounts = new List<FTPAccount>();

            string[] configFiles = Directory.GetFiles(dir, "*.config", SearchOption.AllDirectories);
            FTPAccount acc;
            int accIndex = -1;
            foreach (string fileLocation in configFiles)
            {
                XmlTextReader xtr = new XmlTextReader(fileLocation);
                while (xtr.Read())
                {
                    if (xtr.NodeType == XmlNodeType.Element)
                    {
                        if (!string.IsNullOrEmpty(xtr.Name))
                        {
                            if (xtr.HasAttributes)
                            {
                                xtr.MoveToNextAttribute();
                                string attrb = xtr.ReadInnerXml();
                                if (!string.IsNullOrEmpty(attrb))
                                {
                                    if (attrb.Equals("FTPserver"))
                                    {
                                        while (xtr.NodeType != XmlNodeType.Text)
                                        {
                                            xtr.Read();
                                        }
                                        // FileSystem.AppendDebug("value: " + xtr.Value);
                                        acc = new FTPAccount();
                                        accIndex++;
                                        Accounts.Add(acc);
                                        Accounts[accIndex].Name = "Default";
                                        Accounts[accIndex].Server = xtr.Value;

                                    }

                                    if (attrb.Equals("FTPpassword"))
                                    {
                                        while (xtr.NodeType != XmlNodeType.Text)
                                        {
                                            xtr.Read();
                                        }
                                        // FileSystem.AppendDebug("value: " + xtr.Value);
                                        Accounts[accIndex].Password = xtr.Value;

                                    }

                                    if (attrb.Equals("FTPusername"))
                                    {
                                        while (xtr.NodeType != XmlNodeType.Text)
                                        {
                                            xtr.Read();
                                        }
                                        // FileSystem.AppendDebug("value: " + xtr.Value);
                                        Accounts[accIndex].Username = xtr.Value;

                                    }

                                    if (attrb.Equals("FTPpath"))
                                    {
                                        while (xtr.NodeType != XmlNodeType.Text)
                                        {
                                            xtr.Read();
                                        }
                                        // FileSystem.AppendDebug("value: " + xtr.Value);
                                        Accounts[accIndex].Path = xtr.Value;

                                    }

                                    if (attrb.Equals("FTPport"))
                                    {
                                        while (xtr.NodeType != XmlNodeType.Text)
                                        {
                                            xtr.Read();
                                        }
                                        // FileSystem.AppendDebug("value: " + xtr.Value);
                                        int port = 21;
                                        Int32.TryParse(xtr.Value, out port);
                                        Accounts[accIndex].Port = port;
                                    }

                                    if (attrb.Equals("httppath"))
                                    {
                                        while (xtr.NodeType != XmlNodeType.Text)
                                        {
                                            xtr.Read();
                                        }
                                        // FileSystem.AppendDebug("value: " + xtr.Value);
                                        string path = xtr.Value;
                                        if (!path.ToLower().Equals("true") &&
                                            !path.ToLower().Equals("false"))
                                        {
                                            Accounts[accIndex].HttpPath = xtr.Value;
                                        }
                                    }
                                } // if first node is not empty
                            } // if first node as attributes
                        } // first node not empty
                    } // if node is an element
                } // reading xml
            } // for each config fileName
        }

        public FTPAccountReader()
        {
            string dir = Path.GetDirectoryName(Path.GetDirectoryName(FileSystem.GetConfigFilePath()));
            // FileSystem.AppendDebug(Path.GetDirectoryName(dir));
            readConfigFiles(dir);
            // validate ftpUpload accounts
            List<FTPAccount> temp = new List<FTPAccount>();
            temp.AddRange(this.Accounts);
            for (int i = 0; i < temp.Count - 1; i++)
            {
                FTPAccount acc = temp[i];

                if (string.IsNullOrEmpty(acc.Server) ||
                    string.IsNullOrEmpty(acc.Name) ||
                    string.IsNullOrEmpty(acc.Password) ||
                    string.IsNullOrEmpty(acc.Username) ||
                    0 == acc.Port)
                {
                    this.Accounts.RemoveAt(i);
                }
            }
        }
    }
}
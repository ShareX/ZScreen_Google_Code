#region License Information (GPL v2)

/*
    ZScreen - A program that allows you to upload screenshots in one keystroke.
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
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using HelpersLib;

namespace UploadersLib
{
    public class FTPAccountManager
    {
        public List<FTPAccount> FTPAccounts { get; set; }

        public FTPAccountManager()
        {
            this.FTPAccounts = new List<FTPAccount>();
        }

        public FTPAccountManager(List<FTPAccount> accounts)
        {
            this.FTPAccounts = accounts;
        }

        public void Save(string filePath)
        {
            try
            {
                using (FileStream fs = new FileStream(filePath, FileMode.Create))
                {
                    XmlSerializer xs = new XmlSerializer(typeof(FTPAccountManager));
                    xs.Serialize(fs, this);
                }
            }
            catch (Exception ex)
            {
                StaticHelper.WriteException(ex);
            }
        }

        public static FTPAccountManager Read(string filePath)
        {
            if (File.Exists(filePath))
            {
                try
                {
                    using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                    {
                        XmlSerializer xs = new XmlSerializer(typeof(FTPAccountManager));
                        FTPAccountManager set = xs.Deserialize(fs) as FTPAccountManager;
                        return set;
                    }
                }
                catch
                {
                    // just return blank settings
                }
            }

            return new FTPAccountManager();
        }
    }
}
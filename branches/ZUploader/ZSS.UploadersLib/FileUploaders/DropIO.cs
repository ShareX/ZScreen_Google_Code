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
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Xml;

namespace UploadersLib.FileUploaders
{
    public sealed class DropIO : FileUploader
    {
        private const string API_KEY = "6c65e2d2bfd858f7d0aa6509784f876483582eea";

        public string DropName { get; set; }
        public string DropComment { get; set; }
        public string DropDescription { get; set; }

        public override string Name
        {
            get { return "Drop.io"; }
        }

        public override string Upload(byte[] file, string fileName)
        {
            try
            {
                Dictionary<string, string> args = new Dictionary<string, string>();
                args.Add("version", "2.0");
                args.Add("api_key", API_KEY);
                args.Add("format", "xml");
                args.Add("drop_name", "ZScreen_" + GetRandomAlphanumeric(10));
                //args.Add("token", CreateToken());
                if (!string.IsNullOrEmpty(DropName)) args.Add("drop_name", DropName);
                if (!string.IsNullOrEmpty(DropComment)) args.Add("comment", DropComment);
                if (!string.IsNullOrEmpty(DropDescription)) args.Add("description", DropDescription);

                string response = UploadData(file, fileName, "http://assets.drop.io/upload", "file", args);

                if (!string.IsNullOrEmpty(response))
                {
                    return response;
                }
            }
            catch (Exception e)
            {
                Errors.Add(e.Message);
            }

            return null;
        }

        private string CreateToken()
        {
            Dictionary<string, string> args = new Dictionary<string, string>();
            args.Add("version", "2.0");
            args.Add("api_key", API_KEY);
            args.Add("format", "xml");
            if (!string.IsNullOrEmpty(DropName)) args.Add("drop_name", DropName);
            if (!string.IsNullOrEmpty(DropDescription)) args.Add("description", DropDescription);

            string response = GetResponse("http://api.drop.io/drops", args);

            XmlDocument xml = new XmlDocument();
            xml.LoadXml(response);
            XmlNode node = xml.SelectSingleNode("//drop/admin_token");
            if (node != null)
            {
                return node.InnerText;
            }

            return null;
        }
    }
}
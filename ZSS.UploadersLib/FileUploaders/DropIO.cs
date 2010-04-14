#region License Information (GPL v2)
/*
    ZScreen - A program that allows you to upload screenshots in one keystroke.
    Copyright (C) 2008-2010  Brandon Zimmerman

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
using System.Xml.Linq;
using UploadersLib.Helpers;

namespace UploadersLib.FileUploaders
{
    public sealed class DropIO : FileUploader
    {
        private const string API_KEY = "6c65e2d2bfd858f7d0aa6509784f876483582eea";

        public string DropName { get; set; }
        public string DropDescription { get; set; }

        public override string Name
        {
            get { return "Drop.io"; }
        }

        public class Asset
        {
            public string Name { get; set; }
            public string OriginalFilename { get; set; }
        }

        public class Drop
        {
            public string Name { get; set; }
            public string AdminToken { get; set; }
        }

        public override string Upload(Stream stream, string fileName)
        {
            try
            {
                DropName = "ZScreen_" + UploadHelpers.GetRandomAlphanumeric(10);
                DropDescription = string.Empty;
                Drop drop = CreateDrop(DropName, DropDescription, false, false, false);

                Dictionary<string, string> args = new Dictionary<string, string>();
                args.Add("version", "2.0");
                args.Add("api_key", API_KEY);
                args.Add("format", "xml");
                args.Add("token", drop.AdminToken);
                args.Add("drop_name", drop.Name);

                string response = UploadData(stream, fileName, "http://assets.drop.io/upload", "file", args);

                if (!string.IsNullOrEmpty(response))
                {
                    Asset asset = ParseAsset(response);
                    return string.Format("http://drop.io/{0}/asset/{1}", drop.Name, asset.Name);
                }
            }
            catch (Exception e)
            {
                Errors.Add(e.Message);
            }

            return null;
        }

        public Asset ParseAsset(string response)
        {
            XDocument doc = XDocument.Parse(response);
            XElement root = doc.Element("asset");
            if (root != null)
            {
                Asset asset = new Asset();
                asset.Name = root.ElementValue("name");
                asset.OriginalFilename = root.ElementValue("original-filename");
                return asset;
            }

            return null;
        }

        private Drop CreateDrop(string name, string description, bool guests_can_comment, bool guests_can_add, bool guests_can_delete)
        {
            Dictionary<string, string> args = new Dictionary<string, string>();
            args.Add("version", "2.0");
            args.Add("api_key", API_KEY);
            args.Add("format", "xml");
            // this is the name of the drop and will become part of the URL of the drop
            args.Add("name", name);
            // a plain text description of a drop
            args.Add("description", description);
            // determines whether guests can comment on assets
            args.Add("guests_can_comment", guests_can_comment.ToString());
            // determines whether guests can add assets
            args.Add("guests_can_add", guests_can_add.ToString());
            // determines whether guests can delete assets
            args.Add("guests_can_delete", guests_can_delete.ToString());

            string response = GetResponse("http://api.drop.io/drops", args);

            XDocument doc = XDocument.Parse(response);
            XElement root = doc.Element("drop");
            if (root != null)
            {
                Drop drop = new Drop();
                drop.Name = root.ElementValue("name");
                drop.AdminToken = root.ElementValue("admin_token");
                return drop;
            }

            return null;
        }
    }
}
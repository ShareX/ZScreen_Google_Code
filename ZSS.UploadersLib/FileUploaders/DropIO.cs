﻿#region License Information (GPL v2)

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
using System.Xml.Linq;
using HelpersLib;
using UploadersLib.HelperClasses;

namespace UploadersLib.FileUploaders
{
    public sealed class DropIO : FileUploader
    {
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

        private string APIKey;

        public DropIO(string apiKey)
        {
            APIKey = apiKey;
        }

        public override UploadResult Upload(Stream stream, string fileName)
        {
            UploadResult result = new UploadResult();

            try
            {
                DropName = "ZScreen_" + Helpers.GetRandomAlphanumeric(10);
                DropDescription = string.Empty;
                Drop drop = CreateDrop(DropName, DropDescription, false, false, false);

                Dictionary<string, string> args = new Dictionary<string, string>();
                args.Add("version", "2.0");
                args.Add("api_key", APIKey);
                args.Add("format", "xml");
                args.Add("token", drop.AdminToken);
                args.Add("drop_name", drop.Name);

                string response = UploadData(stream, "http://assets.drop.io/upload", fileName, "file", args);
                result.Source = response;

                if (!string.IsNullOrEmpty(response))
                {
                    Asset asset = ParseAsset(response);
                    result.URL = string.Format("http://drop.io/{0}/asset/{1}", drop.Name, asset.Name);
                }
            }
            catch (Exception e)
            {
                Errors.Add(e.Message);
            }

            return result;
        }

        public Asset ParseAsset(string response)
        {
            XDocument doc = XDocument.Parse(response);
            XElement root = doc.Element("asset");
            if (root != null)
            {
                Asset asset = new Asset();
                asset.Name = root.GetElementValue("name");
                asset.OriginalFilename = root.GetElementValue("original-filename");
                return asset;
            }

            return null;
        }

        private Drop CreateDrop(string name, string description, bool guests_can_comment, bool guests_can_add, bool guests_can_delete)
        {
            Dictionary<string, string> args = new Dictionary<string, string>();
            args.Add("version", "2.0");
            args.Add("api_key", APIKey);
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

            string response = SendPostRequest("http://api.drop.io/drops", args);

            XDocument doc = XDocument.Parse(response);
            XElement root = doc.Element("drop");
            if (root != null)
            {
                Drop drop = new Drop();
                drop.Name = root.GetElementValue("name");
                drop.AdminToken = root.GetElementValue("admin_token");
                return drop;
            }

            return null;
        }
    }
}
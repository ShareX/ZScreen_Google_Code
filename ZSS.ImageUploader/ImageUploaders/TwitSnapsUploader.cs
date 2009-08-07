﻿#region License Information (GPL v2)
/*
    ZScreen - A program that allows you to upload screenshots in one keystroke.
    Copyright (C) 2009  McoreD, Brandon Zimmerman

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
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Xml.Linq;
using ZSS.ImageUploadersLib;
using ZSS.ImageUploadersLib.Helpers;

namespace ZSS.ImageUploadersLib
{

    public class TwitSnapsOptions : ImageUploaderOptions { }

    public sealed class TwitSnapsUploader : ImageUploader
    {
        private TwitSnapsOptions Options;

        private const string UploadAndPostLink = "http://www.twitsnaps.com/api.php";

        public override string Name { get { return "TwitSnaps"; } }

        public TwitSnapsUploader(TwitSnapsOptions options)
        {
            this.Options = options;
        }

        public override ImageFileManager UploadImage(Image image)
        {
            TwitterMsg msgBox = new TwitterMsg("Update Twitter Status");
            msgBox.ShowDialog();
            return Upload(image, msgBox.Message);
        }

        private ImageFileManager Upload(Image image, string msg)
        {
            string url = string.Empty;

            Dictionary<string, string> arguments = new Dictionary<string, string>();

            arguments.Add("user_name", this.Options.Username);
            arguments.Add("password", this.Options.Password);

            if (!string.IsNullOrEmpty(msg))
            {
                arguments.Add("message", msg);
                url = UploadAndPostLink;
            }

            string source = PostImage(image, url, "file", arguments);

            return ParseResult(source);
        }

        private ImageFileManager ParseResult(string source)
        {

            ImageFileManager ifm = new ImageFileManager { Source = source };

            if (!string.IsNullOrEmpty(source))
            {
                XDocument xdoc = XDocument.Parse(source);
                XElement xele = xdoc.Element("rsp");

                if (xele != null)
                {
                    switch (xele.AttributeFirstValue("status", "stat"))
                    {
                        case "ok":
                            string userid, imageurl;                            
                            userid = xele.ElementValue("userid");                            
                            imageurl = xele.ElementValue("imageurl");
                            ifm.ImageFileList.Add(new ImageFile(imageurl, ImageFile.ImageType.FULLIMAGE));
                            break;
                        case "fail":
                            string code, msg;
                            code = xele.Element("errorcode").Value;
                            msg = xele.Element("errormsg").Value;
                            Errors.Add(msg);
                            break;
                    }
                }
            }

            return ifm;
        }
    }
}
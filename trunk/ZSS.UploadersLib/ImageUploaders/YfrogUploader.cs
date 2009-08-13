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
using UploadersLib;
using UploadersLib.Helpers;

namespace UploadersLib.ImageUploaders
{
    public enum YfrogThumbnailType
    {
        [Description("Mini Thumbnail")]
        Mini,
        [Description("Normal Thumbnail")]
        Thumb
    }

    public enum YfrogUploadType
    {
        [Description("Upload Image")]
        UPLOAD_IMAGE_ONLY,
        [Description("Upload Image and update Twitter Status")]
        UPLOAD_IMAGE_AND_TWITTER
    }

    public class YfrogOptions : ImageUploaderOptions
    {
        public string DeveloperKey { get; set; }
        public string Source { get; set; }
        public YfrogUploadType UploadType { get; set; }
        public bool ShowFull { get; set; }
        public YfrogThumbnailType ThumbnailMode { get; set; }

        public YfrogOptions(string devKey)
        {
            this.DeveloperKey = devKey;
        }
    }

    public sealed class YfrogUploader : ImageUploader
    {
        private YfrogOptions Options;

        private const string UploadLink = "http://yfrog.com/api/upload";
        private const string UploadAndPostLink = "http://yfrog.com/api/uploadAndPost";

        public override string Name { get { return "YFrog"; } }

        public YfrogUploader(YfrogOptions options)
        {
            this.Options = options;
        }

        public override ImageFileManager UploadImage(Image image, string fileName)
        {
            switch (this.Options.UploadType)
            {
                case YfrogUploadType.UPLOAD_IMAGE_ONLY:
                    return Upload(image, fileName, "");
                case YfrogUploadType.UPLOAD_IMAGE_AND_TWITTER:
                    TwitterMsg msgBox = new TwitterMsg("Update Twitter Status");
                    msgBox.ShowDialog();
                    return Upload(image, fileName, msgBox.Message);
            }
            return null;
        }

        private ImageFileManager Upload(Image image, string fileName, string msg)
        {
            string url;

            Dictionary<string, string> arguments = new Dictionary<string, string>();

            arguments.Add("username", this.Options.UserName);
            arguments.Add("password", this.Options.Password);

            if (!string.IsNullOrEmpty(msg))
            {
                arguments.Add("message", msg);
                url = UploadAndPostLink;
            }
            else
            {
                url = UploadLink;
            }
            if (!string.IsNullOrEmpty(this.Options.Source))
            {
                arguments.Add("source", this.Options.Source);
            }

            arguments.Add("key", this.Options.DeveloperKey);

            string source = UploadImage(image, fileName, url, "media", arguments);

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
                            string statusid, userid, mediaid, mediaurl;
                            statusid = xele.ElementValue("statusid");
                            userid = xele.ElementValue("userid");
                            mediaid = xele.ElementValue("mediaid");
                            mediaurl = xele.ElementValue("mediaurl");
                            if (this.Options.ShowFull) mediaurl = mediaurl + "/full";
                            ifm.ImageFileList.Add(new ImageFile(mediaurl, ImageFile.ImageType.FULLIMAGE));
                            ifm.ImageFileList.Add(new ImageFile(mediaurl + ".th.jpg", ImageFile.ImageType.THUMBNAIL));
                            break;
                        case "fail":
                            string code, msg;
                            code = xele.Element("err").Attribute("code").Value;
                            msg = xele.Element("err").Attribute("msg").Value;
                            Errors.Add(msg);
                            break;
                    }
                }
            }

            return ifm;
        }
    }
}
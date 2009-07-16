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
using System.Linq;
using System.Text;
using ZSS.ImageUploadersLib;
using ZSS.ImageUploadersLib.Helpers;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Net;
using System.Xml.Linq;

namespace ZSS.ImageUploadersLib
{
    public sealed class TwitPicUploader : ImageUploader
    {
        public enum ThumbnailType { Mini, Thumb }

        public string Username { get; set; }
        public string Password { get; set; }

        public TwitPicUploadType TwitPicUploadType { get; set; }
        public ThumbnailType TwitPicThumbnailType { get; set; }

        private const string UploadLink = "http://twitpic.com/api/upload";
        private const string UploadAndPostLink = "http://twitpic.com/api/uploadAndPost";

        public override string Name { get { return "TwitPic"; } }

        public TwitPicUploader(string username, string password, TwitPicUploadType uploadType)
        {
            Username = username;
            Password = password;
            TwitPicUploadType = uploadType;
        }

        public override ImageFileManager UploadImage(Image image)
        {
            switch (TwitPicUploadType)
            {
                case TwitPicUploadType.UPLOAD_IMAGE_ONLY:
                    return Upload(image, "");
                case TwitPicUploadType.UPLOAD_IMAGE_AND_TWITTER:
                    TwitterMsg msgBox = new TwitterMsg("Update Twitter Status");
                    msgBox.ShowDialog();
                    return Upload(image, msgBox.Message);
            }
            return null;
        }

        private ImageFileManager Upload(Image image, string msg)
        {
            Dictionary<string, string> arguments = new Dictionary<string, string>();
            arguments.Add("username", Username);
            arguments.Add("password", Password);
            if (!string.IsNullOrEmpty(msg))
            {
                arguments.Add("message", msg);
            }
            string url = (string.IsNullOrEmpty(msg) ? UploadLink : UploadAndPostLink);
            string source = PostImage2(image, url, "media", arguments);
            return ParseResult(source);
        }

        private ImageFileManager ParseResult(string source)
        {
            ImageFileManager ifm = new ImageFileManager { Source = source };

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
                        ifm.ImageFileList.Add(new ImageFile(mediaurl, ImageFile.ImageType.FULLIMAGE));
                        ifm.ImageFileList.Add(new ImageFile(string.Format("http://twitpic.com/show/{0}/{1}",
                            TwitPicThumbnailType.ToString().ToLowerInvariant(), mediaid), ImageFile.ImageType.THUMBNAIL));
                        break;
                    case "fail":
                        string code, msg;
                        code = xele.Element("err").Attribute("code").Value;
                        msg = xele.Element("err").Attribute("msg").Value;
                        Errors.Add(msg);
                        break;
                }
            }

            return ifm;
        }
    }
}
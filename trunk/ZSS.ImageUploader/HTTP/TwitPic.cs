using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZSS.ImageUploaders;
using ZSS.ImageUploaders.Helpers;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Net;
using System.Xml.Linq;

namespace ZSS.ImageUploaders
{
    public sealed class TwitPic : HTTPUploader
    {
        public string Username { get; set; }
        public string Password { get; set; }

        private const string UploadLink = "http://twitpic.com/api/upload";
        private const string UploadAndPostLink = "http://twitpic.com/api/uploadAndPost";

        public override string Name { get { return "TwitPic"; } }

        public TwitPic(string username, string password)
        {
            Username = username;
            Password = password;
        }

        protected override ImageFileManager UploadImage(Image image, ImageFormat format)
        {
            ImageFileManager ifm = new ImageFileManager();

            Dictionary<string, string> arguments = new Dictionary<string, string>();
            arguments.Add("username", Username);
            arguments.Add("password", Password);
            ifm.Source = PostImage(image, UploadLink, "media", arguments);

            XDocument xdoc = XDocument.Parse(ifm.Source);
            XElement xele = xdoc.Element("rsp");

            switch(xele.Attribute("stat").Value)
            {
                case "ok":
                    string mediaid, mediaurl;
                    mediaid = xele.Element("mediaid").Value;
                    mediaurl = xele.Element("mediaurl").Value;
                    ifm.ImageFileList.Add(new ImageFile(mediaurl, ImageFile.ImageType.FULLIMAGE));
                    break;
                case "fail":
                    string code, msg;
                    code = xele.Element("err").Attribute("code").Value;
                    msg = xele.Element("err").Attribute("msg").Value;
                    Errors.Add(msg);
                    break;
            }

            return ifm;
        }
    }
}
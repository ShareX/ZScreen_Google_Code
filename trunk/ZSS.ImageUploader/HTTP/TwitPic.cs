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
        public enum UploadType { Upload, UploadAndPost }
        public enum ThumbnailType { Mini, Thumb }

        public string Username { get; set; }
        public string Password { get; set; }
        public string Message { get; set; }
        public UploadType TwitPicUploadType { get; set; }
        public ThumbnailType TwitPicThumbnailType { get; set; }

        private const string UploadLink = "http://twitpic.com/api/upload";
        private const string UploadAndPostLink = "http://twitpic.com/api/uploadAndPost";

        public override string Name { get { return "TwitPic"; } }

        public TwitPic(string username, string password, UploadType uploadType)
        {
            Username = username;
            Password = password;
            TwitPicUploadType = uploadType;
        }

        public override ImageFileManager UploadImage(Image image)
        {
            switch (TwitPicUploadType)
            {
                case UploadType.Upload:
                    return Upload(image);
                case UploadType.UploadAndPost:
                    return UploadAndPost(image);
            }
            return null;
        }

        private ImageFileManager Upload(Image image)
        {
            Dictionary<string, string> arguments = new Dictionary<string, string>();
            arguments.Add("username", Username);
            arguments.Add("password", Password);
            string source = PostImage(image, UploadLink, "media", arguments);
            return ParseResult(source);
        }

        private ImageFileManager UploadAndPost(Image image)
        {
            Dictionary<string, string> arguments = new Dictionary<string, string>();
            arguments.Add("username", Username);
            arguments.Add("password", Password);
            arguments.Add("message", Message);
            string source = PostImage(image, UploadAndPostLink, "media", arguments);
            return ParseResult(source);
        }

        private ImageFileManager ParseResult(string source)
        {
            ImageFileManager ifm = new ImageFileManager { Source = source };

            XDocument xdoc = XDocument.Parse(source);
            XElement xele = xdoc.Element("rsp");

            switch (xele.Attribute("stat").Value)
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

            return ifm;
        }
    }
}
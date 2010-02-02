using System.Collections.Generic;
using System.Drawing;
using System.Xml.Linq;
using UploadersLib.Helpers;

namespace UploadersLib.ImageUploaders
{
    public sealed class Imgur : ImageUploader
    {
        private const string APIKEY = "63499468bcc5d2d6aee1439e50b4e61c";

        public override string Name
        {
            get { return "Imgur"; }
        }

        public override ImageFileManager UploadImage(Image image, string fileName)
        {
            Dictionary<string, string> arguments = new Dictionary<string, string>();
            arguments.Add("key", APIKEY);

            string response = UploadImage(image, fileName, "http://imgur.com/api/upload.xml", "image", arguments);

            return ParseResult(response);
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
                            string original_image = xele.ElementValue("original_image");
                            string large_thumbnail = xele.ElementValue("large_thumbnail");
                            string small_thumbnail = xele.ElementValue("small_thumbnail");
                            string imgur_page = xele.ElementValue("imgur_page");
                            string delete_page = xele.ElementValue("delete_page");
                            ifm.ImageFileList.Add(new ImageFile(original_image, ImageFile.ImageType.FULLIMAGE));
                            ifm.ImageFileList.Add(new ImageFile(large_thumbnail, ImageFile.ImageType.THUMBNAIL));
                            break;
                        case "fail":
                            string error_code = xele.ElementValue("error_code");
                            string error_msg = xele.ElementValue("error_msg");
                            Errors.Add(error_code + " - " + error_msg);
                            break;
                    }
                }
            }

            return ifm;
        }
    }
}
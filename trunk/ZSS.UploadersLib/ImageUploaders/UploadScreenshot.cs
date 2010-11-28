using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;
using HelpersLib;
using UploadersLib.HelperClasses;

namespace UploadersLib.ImageUploaders
{
    public class UploadScreenshot : ImageUploader
    {
        private string APIKey { get; set; }

        public UploadScreenshot(string key)
        {
            APIKey = key;
        }

        public override string Name
        {
            get { return "UploadScreenshot"; }
        }

        public override ImageFileManager UploadImage(Stream stream, string fileName)
        {
            Dictionary<string, string> arguments = new Dictionary<string, string>();
            arguments.Add("apiKey", APIKey);
            arguments.Add("xmlOutput", "1");
            //arguments.Add("testMode", "1");

            string response = UploadData(stream, fileName, "http://img1.uploadscreenshot.com/api-upload.php", "userfile", arguments);

            return ParseResult(response);
        }

        private ImageFileManager ParseResult(string source)
        {
            ImageFileManager ifm = new ImageFileManager { Source = source };

            if (!string.IsNullOrEmpty(source))
            {
                XDocument xdoc = XDocument.Parse(source);
                XElement xele = xdoc.Root.Element("upload");

                string error = xele.GetElementValue("errorCode");
                if (!string.IsNullOrEmpty(error))
                {
                    string errorMessage;

                    switch (error)
                    {
                        case "1":
                            errorMessage = "The MD5 sum that you provided did not match the MD5 sum that we calculated for the uploaded image file." +
                                " There may of been a network interruption during upload. Suggest that you try the upload again.";
                            break;
                        case "2":
                            errorMessage = "The apiKey that you provided does not exist or has been banned. Please contact us for more information.";
                            break;
                        case "3":
                            errorMessage = "The file that you provided was not a png or jpg.";
                            break;
                        case "4":
                            errorMessage = "The file that you provided was too large, currently the limit per file is 50MB.";
                            break;
                        case "99":
                        default:
                            errorMessage = "An unkown error occured, please contact the admin and include a copy of the file that you were trying to upload.";
                            break;
                    }

                    Errors.Add(errorMessage);
                }
                else
                {
                    ifm.Add(xele.GetElementValue("original"), LinkType.FULLIMAGE);
                    ifm.Add(xele.GetElementValue("small"), LinkType.THUMBNAIL);
                    ifm.Add(xele.GetElementValue("deleteurl"), LinkType.DELETION_LINK);
                }
            }

            return ifm;
        }
    }
}
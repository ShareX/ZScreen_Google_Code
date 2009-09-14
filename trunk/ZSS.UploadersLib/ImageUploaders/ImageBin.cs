using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UploadersLib.Helpers;
using System.Drawing;
using System.Text.RegularExpressions;

namespace UploadersLib.ImageUploaders
{
    public sealed class ImageBin : ImageUploader
    {
        public override string Name
        {
            get { return "ImageBin"; }
        }

        public override ImageFileManager UploadImage(Image image, string fileName)
        {
            ImageFileManager ifm = new ImageFileManager();

            Dictionary<string, string> arguments = new Dictionary<string, string>();
            arguments.Add("t", "file");
            arguments.Add("name", "ZScreen");
            arguments.Add("tags", "zscreen");
            arguments.Add("description", "test");
            arguments.Add("adult", "t");
            arguments.Add("sfile", "Upload");
            arguments.Add("url", string.Empty);

            string response = UploadImage(image, fileName, "http://imagebin.ca/upload.php", "f", arguments);
            ifm.Source = response;

            if (!string.IsNullOrEmpty(response))
            {
                Match match = Regex.Match(response, @"(?<=ca/view/).+(?=\.html'>)");
                if (match != null)
                {
                    //string url = string.Format("http://imagebin.ca/view/{0}.html", match.Value);
                    string url = "http://imagebin.ca/img/" + match.Value;
                    ifm.Add(url, ImageFile.ImageType.FULLIMAGE);
                }
            }

            return ifm;
        }
    }
}
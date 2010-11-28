using System.Collections.Generic;
using System.IO;
using UploadersLib.HelperClasses;

namespace UploadersLib.ImageUploaders
{
    public sealed class FilezImages : ImageUploader
    {
        public override string Name
        {
            get { return "Filez"; }
        }

        public override ImageFileManager UploadImage(Stream stream, string fileName)
        {
            Dictionary<string, string> args = new Dictionary<string, string>();
            args.Add("hideFile", "1");

            ImageFileManager ifm = new ImageFileManager();
            string response = UploadData(stream, fileName, "http://www.filez.muffinz.eu/api/upload/linkOnly", "file", args);
            ifm.Source = response;

            if (!string.IsNullOrEmpty(response))
            {
                if (response.EndsWith("/force"))
                {
                    response = response.Substring(0, response.Length - 6);
                }

                ifm.Add(response, LinkType.FULLIMAGE);
            }

            return ifm;
        }
    }
}
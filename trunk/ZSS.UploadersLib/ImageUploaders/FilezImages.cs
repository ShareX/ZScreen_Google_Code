using System.Collections.Generic;
using System.IO;
using UploadersLib.Helpers;

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
            args.Add("api", "ZUploader");  //does not work for some reason...

            ImageFileManager ifm = new ImageFileManager();
            string response = UploadData(stream, fileName, "http://www.filez.muffinz.eu/api/upload/linkOnly", "file", args);
            ifm.Source = response;

            if (!string.IsNullOrEmpty(response))
            {
                ifm.Add(response.TrimEnd('/', 'f', 'o', 'r', 'c', 'e'), LinkType.FULLIMAGE);
            }

            return ifm;
        }
    }
}
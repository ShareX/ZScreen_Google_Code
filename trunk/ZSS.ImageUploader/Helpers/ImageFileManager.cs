using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.IO;

namespace ZSS.ImageUploader.Helpers
{
    public class ImageFileManager
    {
        private List<ImageFile> ImageFileList = new List<ImageFile>();
        public string URL { get; set; }
        public int FileCount { get; set; }
        public string Source { get; set; }
        /// <summary>
        /// Local File Path of the Image if exists
        /// </summary>
        public string LocalFilePath { get; set; }

        public ImageFileManager() { }

        public ImageFileManager(List<ImageFile> list)
        {
            if (list != null && list.Count > 0)
            {
                this.ImageFileList = list;
                this.FileCount = list.Count;
                this.Source = list[0].Source;
                this.URL = this.GetFullImageUrl();
            }
        }

        /// <summary>
        /// Get an Image object of the Image File. Returns null if an error occurs.
        /// </summary>
        /// <returns></returns>
        public Image GetImage()
        {
            if (File.Exists(this.LocalFilePath))
            {
                Image temp = Image.FromFile(this.LocalFilePath);
                Bitmap bmp = new Bitmap(temp);
                temp.Dispose();
                return bmp;
            }

            return null;
        }

        public string GetUrlByType(ClipboardUriType type)
        {
            switch (type)
            {
                case ClipboardUriType.FULL:
                    return GetUrlByImageType(ImageFile.ImageType.FULLIMAGE);
                case ClipboardUriType.FULL_IMAGE_FORUMS:
                    return GetFullImageForumsUrl();
                case ClipboardUriType.LINKED_THUMBNAIL:
                    return GetLinkedThumbnailUrl();
                case ClipboardUriType.THUMBNAIL:
                    return GetThumbnailUrl();
            }

            return "";
        }

        private string GetUrlByImageType(ZSS.ImageUploader.ImageFile.ImageType type)
        {
            foreach (ImageFile imf in this.ImageFileList)
            {
                if (imf.Type == type)
                {
                    return imf.URI;
                }
            }
            return "";
        }

        public string GetThumbnailUrl()
        {
            foreach (ImageFile imf in this.ImageFileList)
            {
                if (imf.Type == ImageFile.ImageType.THUMBNAIL)
                {
                    return imf.URI;
                }
            }
            return "";
        }

        public string GetFullImageUrl()
        {
            foreach (ImageFile imf in this.ImageFileList)
            {
                if (imf.Type == ImageFile.ImageType.FULLIMAGE)
                {
                    return imf.URI;
                }
            }
            return "";
        }

        public string GetFullImageForumsUrl()
        {
            return string.Format("[IMG]{0}[/IMG]", this.GetFullImageUrl());
        }

        public string GetLinkedThumbnailUrl()
        {
            return string.Format("[URL={0}][IMG]{1}[/IMG][/URL]", GetFullImageUrl(), GetThumbnailUrl());
        }

        /// <summary>
        /// Return a file path of the Source saved as text or html
        /// </summary>
        /// <param name="dirPath"></param>
        /// <param name="sType"></param>
        /// <returns></returns>
        public string GetSource(string dirPath, SourceType sType)
        {
            string filePath = "";
            if (!string.IsNullOrEmpty(Source))
            {
                switch (sType)
                {
                    case SourceType.TEXT:
                        filePath = Path.Combine(dirPath, "LastSource.txt");
                        File.WriteAllText(filePath, Source);
                        break;
                    case SourceType.HTML:
                        filePath = Path.Combine(dirPath, "LastSource.html");
                        File.WriteAllText(filePath, Source);
                        break;
                    case SourceType.STRING:
                        filePath = Source;
                        break;
                }
            }
            return filePath;
        }

        public enum SourceType
        {
            TEXT,
            HTML,
            STRING
        }
    }
}
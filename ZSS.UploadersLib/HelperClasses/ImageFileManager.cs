#region License Information (GPL v2)

/*
    ZScreen - A program that allows you to upload screenshots in one keystroke.
    Copyright (C) 2008-2011 ZScreen Developers

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

#endregion License Information (GPL v2)

using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;

namespace UploadersLib.HelperClasses
{
    public class ImageFileManager
    {
        /// <summary>
        /// Local File Path of the Image if exists
        /// </summary>
        public string LocalFilePath { get; set; }

        public UploadResult UploadResult { get; set; }

        private ImageFileManager()
        {
            UploadResult = new UploadResult();
        }

        public ImageFileManager(string fp)
            : this()
        {
            this.LocalFilePath = fp;
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
                    return GetFullImageUrl();
                case ClipboardUriType.FULL_TINYURL:
                    return UploadResult.TinyURL;
                case ClipboardUriType.FULL_IMAGE_FORUMS:
                    return GetFullImageForumsUrl();
                case ClipboardUriType.FULL_IMAGE_HTML:
                    return GetFullImageHTML();
                case ClipboardUriType.FULL_IMAGE_WIKI:
                    return GetFullImageWiki();
                case ClipboardUriType.FULL_IMAGE_MEDIAWIKI:
                    return GetFullImageMediaWikiInnerLink();
                case ClipboardUriType.LINKED_THUMBNAIL:
                    return GetLinkedThumbnailForumUrl();
                case ClipboardUriType.LinkedThumbnailHtml:
                    return GetLinkedThumbnailHtmlUrl();
                case ClipboardUriType.LINKED_THUMBNAIL_WIKI:
                    return GetLinkedThumbnailWikiUrl();
                case ClipboardUriType.THUMBNAIL:
                    return UploadResult.ThumbnailURL;
                case ClipboardUriType.LocalFilePath:
                    return this.LocalFilePath;
                case ClipboardUriType.LocalFilePathUri:
                    return GetLocalFilePathAsUri();
            }

            return UploadResult.URL;
        }

        private string GetLinkedThumbnailHtmlUrl()
        {
            string url = GetFullImageUrl();
            string th = GetThumbnailUrl();
            if (!string.IsNullOrEmpty(url) && !string.IsNullOrEmpty(th))
            {
                return string.Format("<a target='_blank' href=\"{0}\"><img src=\"{1}\" border='0'/></a>", url, th);
            }
            return string.Empty;
        }

        /// <summary>
        /// Attempts to return a local file path URI and if fails (possible due to Portable mode) it will return the local file path
        /// </summary>
        /// <returns></returns>
        public string GetLocalFilePathAsUri()
        {
            string lp = string.Empty;
            try
            {
                lp = new Uri(this.LocalFilePath).AbsoluteUri;
            }
            catch
            {
                lp = this.LocalFilePath;
            }
            return lp;
        }

        public string GetLocalFilePathAsUri(string fp)
        {
            return new Uri(fp).AbsoluteUri;
        }

        public string GetThumbnailUrl()
        {
            return UploadResult.ThumbnailURL;
        }

        public string GetFullImageUrl()
        {
            return UploadResult.URL;
        }

        public string GetFullImageForumsUrl()
        {
            string url = this.GetFullImageUrl();
            if (!string.IsNullOrEmpty(url))
            {
                return string.Format("[IMG]{0}[/IMG]", url);
            }
            return string.Empty;
        }

        public string GetFullImageWiki()
        {
            string url = this.GetFullImageUrl();
            if (!string.IsNullOrEmpty(url))
            {
                return string.Format("[{0}]", url);
            }
            return string.Empty;
        }

        public string GetFullImageMediaWikiInnerLink()
        {
            string url = this.GetFullImageUrl();
            if (string.IsNullOrEmpty(url))
                return string.Empty;
            int index = url.IndexOf("Image:");
            if (index < 0)
                return string.Empty;
            string name = url.Substring(index + "Image:".Length);
            return string.Format("[[Image:{0}]]", name);
        }

        public string GetLinkedThumbnailForumUrl()
        {
            string full = GetFullImageUrl();
            string thumb = GetThumbnailUrl();
            if (!string.IsNullOrEmpty(full) && !string.IsNullOrEmpty(thumb))
            {
                return string.Format("[URL={0}][IMG]{1}[/IMG][/URL]", full, thumb);
            }
            return string.Empty;
        }

        public string GetLinkedThumbnailWikiUrl()
        {
            // [http://code.google.com/ http://code.google.com/images/code_sm.png]
            string full = GetFullImageUrl();
            string thumb = GetThumbnailUrl();
            if (!string.IsNullOrEmpty(full) && !string.IsNullOrEmpty(thumb))
            {
                return string.Format("[{0} {1}]", full, thumb);
            }
            return string.Empty;
        }

        public string GetFullImageHTML()
        {
            string url = GetFullImageUrl();
            if (!string.IsNullOrEmpty(url))
            {
                return string.Format("<img src=\"{0}\"/>", url);
            }
            return string.Empty;
        }

        public enum SourceType
        {
            TEXT,
            HTML,
            STRING
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
            if (!string.IsNullOrEmpty(UploadResult.Source))
            {
                switch (sType)
                {
                    case SourceType.TEXT:
                        filePath = Path.Combine(dirPath, "LastSource.txt");
                        File.WriteAllText(filePath, UploadResult.Source);
                        break;
                    case SourceType.HTML:
                        filePath = Path.Combine(dirPath, "LastSource.html");
                        File.WriteAllText(filePath, UploadResult.Source);
                        break;
                    case SourceType.STRING:
                        filePath = UploadResult.Source;
                        break;
                }
            }
            return filePath;
        }
    }
}
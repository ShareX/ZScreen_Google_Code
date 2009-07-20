﻿#region License Information (GPL v2)
/*
    ZScreen - A program that allows you to upload screenshots in one keystroke.
    Copyright (C) 2008-2009  Brandon Zimmerman

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

using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System;

namespace ZSS.ImageUploadersLib.Helpers
{
    public class ImageFileManager
    {
        public List<ImageFile> ImageFileList = new List<ImageFile>();
        public string URL { get; set; }
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
                case ClipboardUriType.FULL_TINYURL:
                    return GetUrlByImageType(ImageFile.ImageType.FULLIMAGE_TINYURL);
                case ClipboardUriType.FULL_IMAGE_FORUMS:
                    return GetFullImageForumsUrl();
                case ClipboardUriType.FULL_IMAGE_HTML:
                    return GetFullImageHTML();
                case ClipboardUriType.FULL_IMAGE_WIKI:
                    return GetFullImageWiki();
                case ClipboardUriType.LINKED_THUMBNAIL:
                    return GetLinkedThumbnailForumUrl();
                case ClipboardUriType.LINKED_THUMBNAIL_WIKI:
                    return GetLinkedThumbnailWikiUrl();
                case ClipboardUriType.THUMBNAIL:
                    return GetUrlByImageType(ImageFile.ImageType.THUMBNAIL);
            }

            return GetUrlByImageType(ImageFile.ImageType.FULLIMAGE);
        }

        private string GetUrlByImageType(ImageFile.ImageType type)
        {
            foreach (ImageFile imf in this.ImageFileList)
            {
                if (imf.Type == type)
                {
                    return imf.URI;
                }
            }
            return string.Empty;
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
            return string.Empty;
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
            return string.Empty;
        }

        public string GetFullImageForumsUrl()
        {
            return string.Format("[IMG]{0}[/IMG]", this.GetFullImageUrl());
        }

        public string GetFullImageWiki()
        {
            return string.Format("[{0}]", this.GetFullImageUrl());
        }

        public string GetLinkedThumbnailForumUrl()
        {
            return string.Format("[URL={0}][IMG]{1}[/IMG][/URL]", GetFullImageUrl(), GetThumbnailUrl());
        }

        public string GetLinkedThumbnailWikiUrl()
        {
            // [http://code.google.com/ http://code.google.com/images/code_sm.png]
            return string.Format("[{0} {1}]", GetFullImageUrl(), GetThumbnailUrl());
        }

        public string GetFullImageHTML()
        {
            return string.Format("<img src=\"{0}\"/>", this.GetFullImageUrl());
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
    }
}
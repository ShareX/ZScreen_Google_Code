#region License Information (GPL v2)
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

using System;
using System.Collections.Generic;
using System.Text;

namespace ZSS.ImageUploader
{
    public class ImageFile : IComparable<ImageFile>
    {
        private ImageType mType;
        private string mURI;

        /// <summary>
        /// Name of the Screenshot with extension
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Local file path of the Screenshot
        /// </summary>
        private string mFilePath;

        /// <summary>
        /// Size in Mebibytes (MiB) = 1024 KiB
        /// </summary>
        public decimal Size { get; private set; }
        public DateTime DateModified { get; set; }
        public string Source { get; set; }

        public enum ImageType
        {
            THUMBNAIL,
            FULLIMAGE
        }

        public ImageFile(string filePath)
        {
            this.mFilePath = filePath;
            this.Name = System.IO.Path.GetFileName(filePath);
            System.IO.FileInfo fi = new System.IO.FileInfo(filePath);
            this.DateModified = fi.LastWriteTime;
            this.Size = fi.Length / (decimal)(1024 * 1024);
        }

        public ImageFile(string URI, ImageType Type)
        {
            mURI = URI;
            this.Name = System.IO.Path.GetFileName(URI);
            mType = Type;
        }       

        public ImageType Type
        {
            get
            {
                return mType;
            }
        }

        public string URI
        {
            get
            {
                return mURI;
            }
        }

        public string LocalFilePath
        {
            get
            {
                return mFilePath;
            }
        }

        //public static ImageFile getThumbnailForum1ImageFile(string fullPath, string thPath)
        //{
        //    return new ImageFile(string.Format("[URL={0}][IMG]{1}[/IMG][/URL]", fullPath, thPath),
        //        ImageType.THUMBNAIL_FORUMS1);
        //}

        #region IComparable<ImageFile> Members

        int IComparable<ImageFile>.CompareTo(ImageFile other)
        {
            int returnValue = -1;
            if (this.DateModified > other.DateModified)
            {
                returnValue = 1;
            }
            else if (other.DateModified == this.DateModified)
            {
                returnValue = other.Name.CompareTo(this.Name);
            }

            return returnValue;
        }

        #endregion

    }
}
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

using System.Drawing;
using System.IO;
using ZSS.ImageUploadersLib.Helpers;
using ZSS.UploadersLib;

namespace ZSS.ImageUploadersLib
{
    public abstract class ImageUploader : Uploader
    {
    	public abstract string Name {get;}
    	/// API or Anonymous. Default: Anonymous
        /// </summary>
        protected UploadMode UploadMode { get; set; }		
        public event ProgressEventHandler ProgressChanged;
        public delegate void ProgressEventHandler(int progress);

        protected ImageUploader()
        {
            this.UploadMode = UploadMode.ANONYMOUS;
        }
        
        public abstract ImageFileManager UploadImage(Image image, string fileName);

        public ImageFileManager UploadImage(string filePath)
        {
            string fileName = Path.GetFileName(filePath);
            ImageFileManager ifm = UploadImage(Image.FromFile(filePath), fileName);
            ifm.LocalFilePath = filePath;
            return ifm;
        }

        public void ReportProgress(int progress)
        {
            if (ProgressChanged != null)
            {
                ProgressChanged(progress);
            }
        }
    }

    public abstract class ImageUploaderOptions
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
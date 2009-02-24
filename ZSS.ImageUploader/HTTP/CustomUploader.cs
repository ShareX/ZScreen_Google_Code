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
using System.Drawing;
using System.Drawing.Imaging;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;
using ZSS.ImageUploader.Helpers;

namespace ZSS.ImageUploader
{
    public sealed class CustomUploader : HTTPUploader
    {
        private ImageHostingService iHosting;

        public CustomUploader(ImageHostingService imageHosting)
        {
            this.iHosting = imageHosting;
        }

        public override string Name
        {
            get { return iHosting.Name; }
        }

        public override ImageFileManager UploadImage(Image image, ImageFormat format)
        {
            MemoryStream imgStream = new MemoryStream();
            image.Save(imgStream, format);
            image.Dispose();
            imgStream.Position = 0;
            bool oldValue = ServicePointManager.Expect100Continue;
            List<ImageFile> imageFiles = new List<ImageFile>();
            string imgSource = "";

            try
            {
                ServicePointManager.Expect100Continue = false;
                CookieContainer cookies = new CookieContainer();
                Dictionary<string, string> arguments = new Dictionary<string, string>();
                foreach (string[] args in iHosting.Arguments)
                {
                    arguments.Add(args[0], args[1]);
                }
                imgSource = PostImage(imgStream, iHosting.UploadURL, iHosting.FileForm, GetMimeType(format), arguments, cookies, "");
                if (imgSource != "")
                {
                    List<String> regexps = new List<string>();
                    foreach (string regexp in iHosting.RegexpList)
                    {
                        regexps.Add(Regex.Match(imgSource, regexp).Value);
                    }
                    iHosting.Regexps = regexps;
                    string fullimage = iHosting.ReturnLink(iHosting.Fullimage);
                    string thumbnail = iHosting.ReturnLink(iHosting.Thumbnail);
                    imageFiles.Add(new ImageFile(fullimage, ImageFile.ImageType.FULLIMAGE));
                    imageFiles.Add(new ImageFile(thumbnail, ImageFile.ImageType.THUMBNAIL));
                    
                }
            }
            catch (Exception ex)
            {
                this.Errors.Add(ex.Message);
            }
            finally
            {
                ServicePointManager.Expect100Continue = oldValue;
                imgStream.Dispose();
            }

            ImageFileManager ifm = new ImageFileManager(imageFiles);
            ifm.Source = imgSource;
            return ifm;
        }
    }
}
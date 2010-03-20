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
using System.Drawing;
using System.Drawing.Imaging;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;
using UploadersLib.Helpers;

namespace UploadersLib.ImageUploaders
{
    public sealed class CustomUploader : ImageUploader
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

        public override ImageFileManager UploadImage(Image image, string fileName)
        {
            ImageFileManager ifm = new ImageFileManager();
            bool oldValue = ServicePointManager.Expect100Continue;

            try
            {
                ServicePointManager.Expect100Continue = false;
                Dictionary<string, string> arguments = new Dictionary<string, string>();
                foreach (string[] args in iHosting.Arguments)
                {
                    arguments.Add(args[0], args[1]);
                }
                ifm.Source = UploadImage(image, fileName, iHosting.UploadURL, iHosting.FileForm, arguments);
                if (!string.IsNullOrEmpty(ifm.Source))
                {
                    List<String> regexps = new List<string>();
                    foreach (string regexp in iHosting.RegexpList)
                    {
                        regexps.Add(Regex.Match(ifm.Source, regexp).Value);
                    }
                    iHosting.Regexps = regexps;
                    string fullimage = iHosting.ReturnLink(iHosting.Fullimage);
                    string thumbnail = iHosting.ReturnLink(iHosting.Thumbnail);
                    if (!string.IsNullOrEmpty(fullimage))
                    {
                        ifm.ImageFileList.Add(new ImageFile(fullimage, LinkType.FULLIMAGE));
                    }
                    if (!string.IsNullOrEmpty(thumbnail))
                    {
                        ifm.ImageFileList.Add(new ImageFile(thumbnail, LinkType.THUMBNAIL));
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                this.Errors.Add(ex.Message);
            }
            finally
            {
                ServicePointManager.Expect100Continue = oldValue;
            }

            return ifm;
        }
    }
}
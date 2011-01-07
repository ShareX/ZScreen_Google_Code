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

using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using UploadersLib.HelperClasses;

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

        public override ImageFileManager UploadImage(Stream stream, string fileName)
        {
            ImageFileManager ifm = new ImageFileManager();

            Dictionary<string, string> arguments = new Dictionary<string, string>();
            foreach (string[] args in iHosting.Arguments)
            {
                arguments.Add(args[0], args[1]);
            }

            ifm.Source = UploadData(stream, iHosting.UploadURL, fileName, iHosting.FileForm, arguments);

            if (!string.IsNullOrEmpty(ifm.Source))
            {
                List<string> regexps = new List<string>();
                foreach (string regexp in iHosting.RegexpList)
                {
                    regexps.Add(Regex.Match(ifm.Source, regexp).Value);
                }
                iHosting.Regexps = regexps;

                string fullimage;
                if (!string.IsNullOrEmpty(iHosting.Fullimage))
                {
                    fullimage = iHosting.ReturnLink(iHosting.Fullimage);
                }
                else
                {
                    fullimage = ifm.Source;
                }

                string thumbnail = iHosting.ReturnLink(iHosting.Thumbnail);

                ifm.Add(fullimage, LinkType.FULLIMAGE);
                ifm.Add(thumbnail, LinkType.THUMBNAIL);
            }

            return ifm;
        }
    }
}
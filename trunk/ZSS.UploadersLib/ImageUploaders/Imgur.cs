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
using System.Xml.Linq;
using HelpersLib;
using UploadersLib.HelperClasses;

namespace UploadersLib.ImageUploaders
{
    public sealed class Imgur : ImageUploader
    {
        public override string Name
        {
            get { return "Imgur"; }
        }

        public string APIKey { get; private set; }

        public Imgur(string key)
        {
            APIKey = key;
        }

        public override ImageFileManager UploadImage(Stream stream, string fileName)
        {
            Dictionary<string, string> arguments = new Dictionary<string, string>();
            arguments.Add("key", APIKey);

            string response = UploadData(stream, "http://api.imgur.com/2/upload.xml", fileName, "image", arguments);

            return ParseResult(response);
        }

        private ImageFileManager ParseResult(string source)
        {
            ImageFileManager ifm = new ImageFileManager { Source = source };

            if (!string.IsNullOrEmpty(source))
            {
                XDocument xd = XDocument.Parse(source);
                XElement xe;

                if ((xe = xd.GetElement("upload", "links")) != null)
                {
                    ifm.Add(xe.GetElementValue("original"), LinkType.FULLIMAGE);
                    ifm.Add(xe.GetElementValue("large_thumbnail"), LinkType.THUMBNAIL);
                    //ifm.Add(xele.ElementValue("small_square"), LinkType.THUMBNAIL);
                    ifm.Add(xe.GetElementValue("delete_page"), LinkType.DELETION_LINK);
                }
                else if ((xe = xd.GetElement("error")) != null)
                {
                    Errors.Add("Imgur error message: " + xe.GetElementValue("message"));
                }
            }

            return ifm;
        }
    }
}
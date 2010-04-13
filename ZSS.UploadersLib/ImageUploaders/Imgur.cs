#region License Information (GPL v2)
/*
    ZScreen - A program that allows you to upload screenshots in one keystroke.
    Copyright (C) 2008-2010  Brandon Zimmerman

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
using System.IO;
using System.Xml.Linq;
using UploadersLib.Helpers;

namespace UploadersLib.ImageUploaders
{
    public sealed class Imgur : ImageUploader
    {
        private string APIKey { get; set; }

        public Imgur(string key)
        {
            APIKey = key;
        }

        public override string Name
        {
            get { return "Imgur"; }
        }

        public override ImageFileManager UploadImage(Stream stream, string fileName)
        {
            Dictionary<string, string> arguments = new Dictionary<string, string>();
            arguments.Add("key", APIKey);

            string response = UploadData(stream, fileName, "http://imgur.com/api/upload.xml", "image", arguments);

            return ParseResult(response);
        }

        private ImageFileManager ParseResult(string source)
        {
            ImageFileManager ifm = new ImageFileManager { Source = source };

            if (!string.IsNullOrEmpty(source))
            {
                XDocument xdoc = XDocument.Parse(source);
                XElement xele = xdoc.Element("rsp");

                if (xele != null)
                {
                    switch (xele.AttributeFirstValue("status", "stat"))
                    {
                        case "ok":
                            string original_image = xele.ElementValue("original_image");
                            string large_thumbnail = xele.ElementValue("large_thumbnail");
                            string small_thumbnail = xele.ElementValue("small_thumbnail");
                            string imgur_page = xele.ElementValue("imgur_page");
                            string delete_page = xele.ElementValue("delete_page");
                            ifm.ImageFileList.Add(new ImageFile(original_image, LinkType.FULLIMAGE));
                            ifm.ImageFileList.Add(new ImageFile(large_thumbnail, LinkType.THUMBNAIL));
                            ifm.ImageFileList.Add(new ImageFile(delete_page, LinkType.DELETION_LINK));
                            break;
                        case "fail":
                            string error_code = xele.ElementValue("error_code");
                            string error_msg = xele.ElementValue("error_msg");
                            Errors.Add(error_code + " - " + error_msg);
                            break;
                    }
                }
            }

            return ifm;
        }
    }
}
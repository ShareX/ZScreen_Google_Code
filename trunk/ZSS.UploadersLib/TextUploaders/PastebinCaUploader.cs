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
using UploadersLib.Helpers;

namespace UploadersLib.TextUploaders
{
    [Serializable]
    public sealed class PastebinCaUploader : TextUploader
    {
        public static readonly string Hostname = TextDestType.PASTEBIN_CA.GetDescription();
        public const string APIKey = "qrsjom2g8qDYcy8P+GeNnxSZowY89gKr";

        public override object Settings
        {
            get
            {
                return HostSettings;
            }
            set
            {
                HostSettings = (PastebinCaSettings)value;
            }
        }

        public PastebinCaSettings HostSettings = new PastebinCaSettings();

        public PastebinCaUploader()
        {
            HostSettings.URL = "http://pastebin.ca/quiet-paste.php";
        }

        public override string ToString()
        {
            return HostSettings.Name;
        }

        public override string TesterString
        {
            get { return "Testing " + Hostname; }
        }

        public override string UploadText(TextInfo text)
        {
            if (!string.IsNullOrEmpty(text.LocalString))
            {
                Dictionary<string, string> arguments = new Dictionary<string, string>();
                arguments.Add("api", APIKey);
                arguments.Add("content", text.LocalString);
                arguments.Add("description", HostSettings.Description);
                arguments.Add("expiry", HostSettings.ExpireTime);
                arguments.Add("name", HostSettings.Author);
                arguments.Add("s", "Submit Post");
                arguments.Add("type", HostSettings.TextFormat);

                return GetResponse(HostSettings.URL, arguments);
            }

            return string.Empty;
        }

        [Serializable]
        public class PastebinCaSettings : TextUploaderSettings
        {
            public override string Name { get; set; }
            public override string URL { get; set; }
            /// <summary>type</summary>
            public string TextFormat { get; set; }
            /// <summary>expiry</summary>
            public string ExpireTime { get; set; }
            /// <summary>description</summary>
            public string Description { get; set; }
            /// <summary>name</summary>
            public string Author { get; set; }

            public PastebinCaSettings()
            {
                Name = Hostname;
                TextFormat = "1";
                ExpireTime = "1 month";
                Description = string.Empty;
                Author = string.Empty;
            }
        }
    }
}
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
    public sealed class Paste2Uploader : TextUploader
    {
        public static readonly string Hostname = TextDestType.PASTE2.GetDescription();

        public override object Settings
        {
            get
            {
                return HostSettings;
            }
            set
            {
                HostSettings = (Paste2Settings)value;
            }
        }

        public Paste2Settings HostSettings = new Paste2Settings();

        public Paste2Uploader()
        {
            HostSettings.URL = "http://paste2.org/new-paste";
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
                arguments.Add("code", text.LocalString);
                arguments.Add("description", HostSettings.Description);
                arguments.Add("lang", HostSettings.TextFormat);
                arguments.Add("parent", "0");

                return GetRedirectionURL(HostSettings.URL, arguments);
            }

            return string.Empty;
        }

        [Serializable]
        public class Paste2Settings : TextUploaderSettings
        {
            public override string Name { get; set; }
            public override string URL { get; set; }
            /// <summary>lang</summary>
            public string TextFormat { get; set; }
            /// <summary>description</summary>
            public string Description { get; set; }

            public Paste2Settings()
            {
                Name = Hostname;
                TextFormat = "text";
                Description = string.Empty;
            }
        }
    }
}
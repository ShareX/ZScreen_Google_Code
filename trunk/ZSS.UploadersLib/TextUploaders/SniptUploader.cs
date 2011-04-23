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

using System;
using System.Collections.Generic;
using UploadersLib.HelperClasses;

namespace UploadersLib.TextUploaders
{
    [Serializable]
    public sealed class SniptUploader : TextUploader
    {
        public static readonly string Hostname = "TODO"; //TextDestType.SNIPT.GetDescription();

        public override object Settings
        {
            get
            {
                return HostSettings;
            }
            set
            {
                HostSettings = (SniptSettings)value;
            }
        }

        public SniptSettings HostSettings = new SniptSettings();

        public SniptUploader()
        {
            HostSettings.URL = "http://snipt.org/snip";
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
                arguments.Add("codeSnippet", text.LocalString);
                arguments.Add("codeSnippetTitle", HostSettings.SnippetTitle);
                arguments.Add("lang", HostSettings.TextFormat);
                arguments.Add("private", HostSettings.Visibility == Privacy.Private ? "1" : "0");
                arguments.Add("shownums", HostSettings.LineNumbers ? "1" : "0");
                arguments.Add("snipAction", "");
                arguments.Add("theme", HostSettings.Theme);

                return GetRedirectionURL(HostSettings.URL, arguments);
            }

            return string.Empty;
        }

        [Serializable]
        public class SniptSettings : TextUploaderSettings
        {
            public override string Name { get; set; }
            public override string URL { get; set; }
            /// <summary>lang</summary>
            public string TextFormat { get; set; }
            /// <summary>codeSnippetTitle</summary>
            public string SnippetTitle { get; set; }
            /// <summary>private</summary>
            public Privacy Visibility { get; set; }
            /// <summary>shownums</summary>
            public bool LineNumbers { get; set; }
            /// <summary>theme</summary>
            public string Theme { get; set; }

            public SniptSettings()
            {
                Name = Hostname;
                TextFormat = "text";
                SnippetTitle = string.Empty;
                Visibility = Privacy.Private;
                LineNumbers = true;
                Theme = "1";
            }
        }
    }
}
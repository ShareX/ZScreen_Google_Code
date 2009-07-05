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

using System.Collections.Generic;
using System;
using System.Linq;
using System.Net;
using System.Web;
using System.Text;
using System.IO;
using ZSS.TextUploaderLib.Helpers;
using System.Xml.Serialization;
using System.ComponentModel;

namespace ZSS.TextUploaderLib
{
    [Serializable]
    public sealed class SniptUploader : TextUploader
    {
        public const string Hostname = "snipt.org";

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

        public override string Name
        {
            get { return Hostname; }
        }

        public override string TesterString
        {
            get { return "Testing " + Hostname; }
        }

        public override string UploadText(string text)
        {
            if (!string.IsNullOrEmpty(text))
            {
                Dictionary<string, string> arguments = new Dictionary<string, string>();
                arguments.Add("codeSnippet", HttpUtility.UrlEncode(text));
                arguments.Add("codeSnippetTitle", HostSettings.SnippetTitle);
                arguments.Add("lang", HostSettings.Language);
                arguments.Add("private", HostSettings.Visibility == Privacy.Private ? "1" : "0");
                arguments.Add("shownums", HostSettings.LineNumbers ? "1" : "0");
                arguments.Add("snipAction", "");
                arguments.Add("theme", HostSettings.Theme);

                return GetResponse(HostSettings.URL, arguments);
            }

            return "";
        }

        [Serializable]
        public class SniptSettings
        {
            public string URL { get; set; }
            public string SnippetTitle { get; set; }
            public string Language { get; set; }
            public Privacy Visibility { get; set; }
            public bool LineNumbers { get; set; }
            public string Theme { get; set; }

            public SniptSettings()
            {
                Language = "text";
                Visibility = Privacy.Private;
                LineNumbers = true;
                Theme = "1";
            }
        }
    }
}
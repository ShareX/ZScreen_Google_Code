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
using System.Text;
using System.Web;
using System.Windows.Forms;
using System.Collections.Generic;
using System.ComponentModel;

namespace ZSS
{
    [Serializable()]
    public class DekiWikiAccount
    {
        [Category("MindTouch")]
        public string Name { get; set; }
        [Category("MindTouch")]
        public string Url { get; set; }
        [Category("MindTouch")]
        public string Username { get; set; }
        [Category("MindTouch"), PasswordPropertyText(true)]
        public string Password { get; set; }

        public List<DekiWikiHistory> History = new List<DekiWikiHistory>();

        public DekiWikiAccount() { }

        public DekiWikiAccount(string name)
        {
            this.Name = name;
        }

        public string getUriPath(string fileName)
        {
            // Create the Uri
            return Url + "/@api/deki/pages/=" + HttpUtility.UrlEncode(HttpUtility.UrlEncode(DekiWiki.savePath)) + "/files/=" + HttpUtility.UrlEncode(HttpUtility.UrlEncode(fileName));
        }

        public override string ToString()
        {
            return this.Name;
        }
    }
}
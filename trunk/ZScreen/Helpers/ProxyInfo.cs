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
using System.Linq;
using System.Text;
using System.Net;
using System.ComponentModel;

namespace ZSS.Helpers
{
    [Serializable]
    public class ProxyInfo
    {
        public string UserName { get; set; }
        [Category("Proxy"), PasswordPropertyText(true)]
        public string Password { get; set; }
        public string Domain { get; set; }
        public ProxyInfo() { }

        public ProxyInfo(string userName, string password, string domain)
        {
            this.UserName = userName;
            this.Password = password;
            this.Domain = domain;
        }

        public override string ToString()
        {
            return string.Format("{0} - {1}", this.UserName, this.Domain);
        }
    }
}
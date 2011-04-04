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
using System.Text;

namespace ZSS.UpdateCheckerLib
{
    public class UpdateInfo
    {
        public Version ApplicationVersion { get; set; }
        public Version LatestVersion { get; set; }
        public string URL { get; set; }
        public DateTime Date { get; set; }
        public string Summary { get; set; }

        public bool IsUpdateRequired
        {
            get { return LatestVersion != null && ApplicationVersion != null && LatestVersion.CompareTo(ApplicationVersion) > 0; }
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Current version: " + ApplicationVersion);
            sb.AppendLine("Latest version: " + (IsUpdateRequired ? LatestVersion : ApplicationVersion));
            return sb.ToString();
        }
    }
}
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
using System.Text;
using System.Diagnostics;

namespace ZSS.Screenshots
{
    /// <summary>
    /// This class is responsible for holding information of an image software 
    /// For example, MSPaint is an instance of this class
    /// </summary>
    class ScreenshotEditApp
    {
        /// <summary>
        /// Name of the Application as displyed in the WinSettings Image Software ListBox
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Location of the Application to execute
        /// </summary>
        public string Location { get; set; }

        /// <summary>
        /// Function to open a fileName using the Screenshot application
        /// </summary>
        /// <param name="fullFilePath">Screenshot fileName to open</param>
        /// <returns></returns>
        public bool openFile(string filePath)
        {
            bool success = false;
            Process p = new Process();
            ProcessStartInfo psi = new ProcessStartInfo(this.Location);
            psi.Arguments = filePath;
            p.StartInfo = psi;
            p.Start();
            success = true;
            return success;
        }
    }

}

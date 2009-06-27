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

#region Source code: Greenshot (GPL)
/*
    This file originated from the Greenshot project (GPL). It may or may not have been modified.
    Please do not contact Greenshot about errors with this code. Instead contact the creators of this program.
    URL: http://greenshot.sourceforge.net/
    Code (CVS): http://greenshot.cvs.sourceforge.net/viewvc/greenshot/
*/
#endregion

/*
 * Created by SharpDevelop.
 * User: jens
 * Date: 31.03.2007
 * Time: 19:39
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */

using System.Drawing.Imaging;
using System.Drawing;
using System.Windows.Forms;
using System;

namespace Greenshot.Configuration
{
    /// <summary>
    /// Greenshot's runtime configuration
    /// abstract, all properties are public and static
    /// </summary>
    public abstract class RuntimeConfig
    {
        public static string[] SupportedLanguages = { "en-US", "de-DE" };
        public static string[] SupportedImageFormats = { ImageFormat.Jpeg.ToString(), ImageFormat.Gif.ToString(), ImageFormat.Png.ToString(), ImageFormat.Bmp.ToString() };
        public static string BugTrackerUrl = "https://sourceforge.net/tracker/?func=postadd&group_id=191585&atid=937972&summary=%SUMMARY%&details=%DETAILS%";
        public static Rectangle LastCapturedRegion;
    }
}
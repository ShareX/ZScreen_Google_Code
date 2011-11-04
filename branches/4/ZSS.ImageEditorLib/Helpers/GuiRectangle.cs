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

#region Source code: Greenshot (GPL)

/*
    This file originated from the Greenshot project (GPL). It may or may not have been modified.
    Please do not contact Greenshot about errors with this code. Instead contact the creators of this program.
    URL: http://greenshot.sourceforge.net/
    Code (CVS): http://greenshot.cvs.sourceforge.net/viewvc/greenshot/
*/

#endregion Source code: Greenshot (GPL)

/*
 * Benutzer: thomas
 * Datum: 20.03.2007
 * Zeit: 21:54
 *
 */

using System.Drawing;

namespace Greenshot.Helpers
{
    public static class GuiRectangle
    {
        public static Rectangle GetGuiRectangle(int x, int y, int w, int h)
        {
            if (w < 0)
            {
                x += w;
                w = -w;
            }
            if (h < 0)
            {
                y += h;
                h = -h;
            }
            return new Rectangle(x, y, w, h);
        }
    }
}
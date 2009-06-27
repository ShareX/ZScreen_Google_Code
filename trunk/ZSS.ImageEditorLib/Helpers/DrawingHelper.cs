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
 * Erstellt mit SharpDevelop.
 * Benutzer: Administrator
 * Datum: 13.03.2008
 * Zeit: 23:34
 * 
 * Sie können diese Vorlage unter Extras > Optionen > Codeerstellung > Standardheader ändern.
 */

using System;

namespace Greenshot.Helpers
{
    /// <summary>
    /// Description of DrawingHelper.
    /// </summary>
    public static class DrawingHelper
    {
        /// <summary>
        /// borrowed from Sun JDK ;-)
        /// </summary>
        public static double CalculateLinePointDistance(double x1, double y1, double x2, double y2, double px, double py)
        {
            // Adjust vectors relative to x1,y1
            // x2,y2 becomes relative vector from x1,y1 to end of segment 
            x2 -= x1;
            y2 -= y1;
            // px,py becomes relative vector from x1,y1 to test point 
            px -= x1;
            py -= y1;
            double dotprod = px * x2 + py * y2;
            // dotprod is the length of the px,py vector 
            // projected on the x1,y1=>x2,y2 vector times the 
            // length of the x1,y1=>x2,y2 vector 
            double projlenSq = dotprod * dotprod / (x2 * x2 + y2 * y2);
            // Distance to line is now the length of the relative point 
            // vector minus the length of its projection onto the line 
            double lenSq = px * px + py * py - projlenSq;
            if (lenSq < 0)
            {
                lenSq = 0;
            }
            return Math.Sqrt(lenSq);
        }
    }
}
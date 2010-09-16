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

#endregion License Information (GPL v2)

#region Source code: Greenshot (GPL)

/*
    This file originated from the Greenshot project (GPL). It may or may not have been modified.
    Please do not contact Greenshot about errors with this code. Instead contact the creators of this program.
    URL: http://greenshot.sourceforge.net/
    Code (CVS): http://greenshot.cvs.sourceforge.net/viewvc/greenshot/
*/

#endregion Source code: Greenshot (GPL)

using System.Drawing;
using System.Drawing.Imaging;

namespace Greenshot.Helpers
{
    /// <summary>
    /// PropertyItemProvider is a helper class to provide instances of PropertyItem
    /// Be sure to have the PropertyItemProvider.resx too, since it contains the
    /// image we will take the PropertyItem from.
    /// </summary>
    public static class PropertyItemProvider
    {
        private static PropertyItem propertyItem;

        public static PropertyItem GetPropertyItem(int id, string value)
        {
            if (propertyItem == null)
            {
                System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PropertyItemProvider));
                Bitmap bmp = (Bitmap)resources.GetObject("propertyitemcontainer");
                propertyItem = bmp.GetPropertyItem(bmp.PropertyIdList[0]);
                propertyItem.Type = 2; // string
            }
            propertyItem.Id = id;
            System.Text.ASCIIEncoding encoding = new System.Text.ASCIIEncoding();
            propertyItem.Value = encoding.GetBytes(value + " ");
            propertyItem.Len = value.Length + 1;
            return propertyItem;
        }
    }
}
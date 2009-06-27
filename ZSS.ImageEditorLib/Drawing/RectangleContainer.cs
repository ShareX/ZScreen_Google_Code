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

using System;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.Serialization;
using Greenshot.Helpers;

namespace Greenshot.Drawing
{
    /// <summary>
    /// Description of RectangleContainer.
    /// </summary>
    [Serializable()]
    public class RectangleContainer : DrawableContainer
    {
        public RectangleContainer(Control parent)
            : base(parent)
        {
            supportedProperties.Add(DrawableContainer.Property.LINECOLOR);
            supportedProperties.Add(DrawableContainer.Property.FILLCOLOR);
            supportedProperties.Add(DrawableContainer.Property.THICKNESS);
        }

        #region serialization

        public RectangleContainer(SerializationInfo info, StreamingContext ctxt)
            : base(info, ctxt)
        {

        }

        public override void GetObjectData(SerializationInfo info, StreamingContext ctxt)
        {
            base.GetObjectData(info, ctxt);
        }

        #endregion

        public override void Draw(Graphics g, RenderMode rm)
        {
            Pen pen = new Pen(foreColor);
            pen.Width = thickness;
            Brush brush = new SolidBrush(backColor);
            Rectangle rect = GuiRectangle.GetGuiRectangle(this.Left, this.Top, this.Width, this.Height);
            g.FillRectangle(brush, rect);
            g.DrawRectangle(pen, rect);
        }
    }
}
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
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Greenshot.Helpers;

namespace Greenshot.Drawing
{
    [Serializable()]
    public class LineContainer : DrawableContainer
    {
        public bool HasStartPointArrowHead = false;
        public bool HasEndPointArrowHead = false;

        public LineContainer(Control parent)
            : base(parent)
        {
            supportedProperties.Add(DrawableContainer.Property.LINECOLOR);
            supportedProperties.Add(DrawableContainer.Property.ARROWHEADS);
            supportedProperties.Add(DrawableContainer.Property.THICKNESS);
            grippers[1].Enabled = grippers[2].Enabled = grippers[3].Enabled = grippers[5].Enabled = grippers[6].Enabled = grippers[7].Enabled = false;
        }

        #region Serialization

        public LineContainer(SerializationInfo info, StreamingContext ctxt)
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
            g.SmoothingMode = SmoothingMode.HighQuality;
            Pen pen = new Pen(foreColor) { Width = thickness };

            AdjustableArrowCap aac = new AdjustableArrowCap(4, 6);
            if (ArrowHeads == ArrowHeads.Start || ArrowHeads == ArrowHeads.Both) pen.CustomStartCap = aac;
            if (ArrowHeads == ArrowHeads.End || ArrowHeads == ArrowHeads.Both) pen.CustomEndCap = aac;

            g.DrawLine(pen, this.Left, this.Top, this.Left + this.Width, this.Top + this.Height);
        }

        public override bool ClickableAt(int x, int y)
        {
            if (!base.ClickableAt(x, y))
            {
                return false;
            }
            double distance = DrawingHelper.CalculateLinePointDistance(this.Left, this.Top, this.Left + this.Width, this.Top + this.Height, x, y);
            return distance < 5;
        }
    }
}
#region License Information (GPL v2)

/*
    ZUploader - A program that allows you to upload images, texts or files
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

using System.Drawing;
using System.Drawing.Drawing2D;

namespace RegionCapture
{
    public class RoundedRectangleRegionSurface : RectangleRegionSurface
    {
        public RoundedRectangleRegionSurface(Image backgroundImage)
            : base(backgroundImage)
        {
        }

        protected override void Draw(Graphics g)
        {
            if (Area != null && Area.Width > 0 && Area.Height > 0)
            {
                GraphicsPath graphicsPath = GraphicsEx.GetRoundedRectangle(new Rectangle(Area.X, Area.Y, Area.Width - 1, Area.Height - 1), 15);
                Region region = new Region(graphicsPath);
                g.ExcludeClip(region);
                g.FillRectangle(shadowBrush, 0, 0, Width, Height);
                g.ResetClip();
                g.DrawPath(borderPen, graphicsPath);
                //g.DrawRectangle(borderPen, Area.X, Area.Y, Area.Width - 1, Area.Height - 1);
            }
            else
            {
                g.FillRectangle(shadowBrush, 0, 0, Width, Height);
            }

            DrawObjects(g);
        }
    }
}
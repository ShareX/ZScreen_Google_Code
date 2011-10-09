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
using HelpersLib;

namespace ScreenCapture
{
    public class RectangleRegion : Surface
    {
        public RectangleRegion(Image backgroundImage = null)
            : base(backgroundImage)
        {
        }

        protected override void Draw(Graphics g)
        {
            if (AreaManager.Areas.Count > 0)
            {
                regionPath = new GraphicsPath();
                regionPath.FillMode = FillMode.Winding;

                foreach (Rectangle area in AreaManager.Areas)
                {
                    if (area.Width > 0 && area.Height > 0)
                    {
                        AddShapePath(regionPath, area);
                    }
                }

                using (Region region = new Region(regionPath))
                {
                    g.ExcludeClip(region);
                    g.FillRectangle(shadowBrush, 0, 0, Width, Height);
                    //DrawObjects(g);
                    g.ResetClip();
                }

                g.DrawPath(borderPen, regionPath);

                if (AreaManager.Areas.Count > 1)
                {
                    Rectangle totalArea = AreaManager.CombineAreas();
                    g.DrawRectangle(borderPen, totalArea.X, totalArea.Y, totalArea.Width - 1, totalArea.Height - 1);
                }
            }
            else
            {
                g.FillRectangle(shadowBrush, 0, 0, Width, Height);
            }
        }

        protected virtual void AddShapePath(GraphicsPath graphicsPath, Rectangle rect)
        {
            graphicsPath.AddRectangle(new Rectangle(rect.X, rect.Y, rect.Width - 1, rect.Height - 1));
        }
    }
}
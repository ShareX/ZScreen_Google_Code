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

using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace RegionCapture
{
    public class FreeHandRegion : Surface
    {
        private NodeObject lastNode;
        private List<Point> points;

        public FreeHandRegion(Image backgroundImage = null)
            : base(backgroundImage)
        {
            points = new List<Point>(128);
            regionPath = new GraphicsPath();

            lastNode = new NodeObject(borderPen, nodeBackgroundBrush);
            DrawableObjects.Add(lastNode);
        }

        protected override void Update()
        {
            base.Update();

            if (!IsAreaCreated && isMouseDown)
            {
                lastNode.Visible = true;
                lastNode.IsDragging = true;
                IsAreaCreated = true;
            }

            if (lastNode.Visible && lastNode.IsDragging && mousePosition != oldMousePosition)
            {
                points.Add(mousePosition);
                regionPath.AddLine(oldMousePosition, mousePosition);
                lastNode.Position = mousePosition;
            }

            if (points.Count > 2)
            {
                RectangleF rect = regionPath.GetBounds();
                Area = new Rectangle((int)rect.X, (int)rect.Y, (int)rect.Width + 1, (int)rect.Height + 1);
            }
        }

        protected override void OnRightClickCancel()
        {
            if (IsAreaCreated)
            {
                IsAreaCreated = false;
                area = Rectangle.Empty;
                regionPath.Reset();
                HideNodes();
                points.Clear();
            }
            else
            {
                Close(true);
            }
        }

        protected override void Draw(Graphics g)
        {
            if (points.Count > 2)
            {
                using (Region region = new Region(regionPath))
                {
                    g.ExcludeClip(region);
                    g.FillRectangle(shadowBrush, 0, 0, Width, Height);
                    g.ResetClip();
                }

                g.DrawPath(borderPen, regionPath);

                g.DrawRectangle(borderPen, Area.X, Area.Y, Area.Width - 1, Area.Height - 1);
            }
            else
            {
                g.FillRectangle(shadowBrush, 0, 0, Width, Height);
            }

            base.Draw(g);
        }
    }
}
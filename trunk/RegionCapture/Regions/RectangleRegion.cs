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

namespace RegionCapture
{
    public class RectangleRegion : Surface
    {
        protected NodeObject[] nodes;

        protected bool isNodesCreated;

        public RectangleRegion(Image backgroundImage)
            : base(backgroundImage)
        {
            nodes = new NodeObject[4];

            for (int i = 0; i < nodes.Length; i++)
            {
                nodes[i] = new NodeObject(borderPen, nodeBackgroundBrush);
                DrawableObjects.Add(nodes[i]);
            }
        }

        protected override void Update()
        {
            base.Update();

            // If first time click
            if (isMouseDown && !isNodesCreated)
            {
                for (int i = 0; i < nodes.Length; i++)
                {
                    nodes[i].Position = ClientMousePosition;
                    nodes[i].Visible = true;
                }

                // Right bottom
                nodes[2].IsHolding = true;

                isNodesCreated = true;
            }

            if (isNodesCreated)
            {
                if (nodes[0].IsHolding) // Left top
                {
                    nodes[0].Position = ClientMousePosition;
                    nodes[1].Position = new Point(nodes[1].Position.X, nodes[0].Position.Y);
                    nodes[3].Position = new Point(nodes[0].Position.X, nodes[3].Position.Y);
                }
                else if (nodes[1].IsHolding) // Right top
                {
                    nodes[1].Position = ClientMousePosition;
                    nodes[0].Position = new Point(nodes[0].Position.X, nodes[1].Position.Y);
                    nodes[2].Position = new Point(nodes[1].Position.X, nodes[2].Position.Y);
                }
                else if (nodes[2].IsHolding) // Right bottom
                {
                    nodes[2].Position = ClientMousePosition;
                    nodes[1].Position = new Point(nodes[2].Position.X, nodes[1].Position.Y);
                    nodes[3].Position = new Point(nodes[3].Position.X, nodes[2].Position.Y);
                }
                else if (nodes[3].IsHolding) // Left bottom
                {
                    nodes[3].Position = ClientMousePosition;
                    nodes[0].Position = new Point(nodes[3].Position.X, nodes[0].Position.Y);
                    nodes[2].Position = new Point(nodes[2].Position.X, nodes[3].Position.Y);
                }
            }
        }

        protected override void Draw(Graphics g)
        {
            if (Area.Width > 0 && Area.Height > 0)
            {
                g.ExcludeClip(Area);
                g.FillRectangle(shadowBrush, 0, 0, Width, Height);
                g.ResetClip();
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
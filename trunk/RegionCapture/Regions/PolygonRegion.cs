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
using System.Windows.Forms;

namespace RegionCapture
{
    public class PolygonRegion : Surface
    {
        private List<NodeObject> nodes;

        public PolygonRegion(Image backgroundImage)
            : base(backgroundImage)
        {
            nodes = new List<NodeObject>();

            MouseDown += new MouseEventHandler(PolygonRegionSurface_MouseDown);
        }

        private void PolygonRegionSurface_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                foreach (NodeObject node in DrawableObjects)
                {
                    if (node.IsMouseHover || node.IsHolding) return;
                }

                if (nodes.Count == 0)
                {
                    CreateNode();
                }

                CreateNode();
            }
            else if (e.Button == MouseButtons.Right)
            {
                foreach (NodeObject node in nodes)
                {
                    if (node.IsMouseHover)
                    {
                        nodes.Remove(node);
                        DrawableObjects.Remove(node);
                        return;
                    }
                }
            }
        }

        protected override void Update()
        {
            base.Update();

            for (int i = nodes.Count - 1; i >= 0; i--)
            {
                if (nodes[i].Visible && nodes[i].IsHolding)
                {
                    ActivateNode(nodes[i]);
                    break;
                }
            }
        }

        protected override void Draw(Graphics g)
        {
            regionPath = new GraphicsPath();

            for (int i = 0; i < nodes.Count - 1; i++)
            {
                regionPath.AddLine(nodes[i].Position, nodes[i + 1].Position);
            }

            if (nodes.Count > 2)
            {
                regionPath.CloseFigure();

                using (Region region = new Region(regionPath))
                {
                    g.ExcludeClip(region);
                    g.FillRectangle(shadowBrush, 0, 0, Width, Height);
                    g.ResetClip();
                }

                g.DrawRectangle(borderPen, Area.X, Area.Y, Area.Width - 1, Area.Height - 1);
            }
            else
            {
                g.FillRectangle(shadowBrush, 0, 0, Width, Height);
            }

            g.DrawPath(borderPen, regionPath);

            base.Draw(g);
        }

        private NodeObject CreateNode()
        {
            NodeObject newNode = new NodeObject(borderPen, nodeBackgroundBrush);
            ActivateNode(newNode);
            nodes.Add(newNode);
            DrawableObjects.Add(newNode);
            return newNode;
        }

        private void ActivateNode(NodeObject node)
        {
            node.Position = ClientMousePosition;
            node.Visible = true;
            node.IsHolding = true;
        }
    }
}
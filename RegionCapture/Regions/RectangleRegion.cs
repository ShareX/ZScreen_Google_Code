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
        protected NodeObject firstNode;
        protected NodeObject secondNode;

        public RectangleRegion(Image backgroundImage)
            : base(backgroundImage)
        {
            firstNode = new NodeObject(borderPen, nodeBackgroundBrush);
            secondNode = new NodeObject(borderPen, nodeBackgroundBrush);

            DrawableObjects.Add(firstNode);
            DrawableObjects.Add(secondNode);
        }

        protected override void Update()
        {
            base.Update();

            if (isMouseDown && (!firstNode.Visible || !secondNode.Visible))
            {
                ActivateNode(firstNode);
                ActivateNode(secondNode);
            }

            if (firstNode.Visible && secondNode.Visible)
            {
                if (firstNode.IsHolding)
                {
                    ActivateNode(firstNode);
                }
                else if (secondNode.IsHolding)
                {
                    ActivateNode(secondNode);
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

        private void ActivateNode(NodeObject node)
        {
            node.Position = ClientMousePosition;
            node.Visible = true;
            node.IsHolding = true;
        }
    }
}
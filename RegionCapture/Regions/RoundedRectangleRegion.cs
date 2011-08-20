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

using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace RegionCapture
{
    public class RoundedRectangleRegion : RectangleRegion
    {
        public float Radius { get; set; }

        private int radiusIncrease = 3;

        public RoundedRectangleRegion(Image backgroundImage)
            : base(backgroundImage)
        {
            Radius = 15;

            MouseWheel += new MouseEventHandler(RoundedRectangleRegionSurface_MouseWheel);
        }

        private void RoundedRectangleRegionSurface_MouseWheel(object sender, MouseEventArgs e)
        {
            if (e.Delta > 0)
            {
                Radius += radiusIncrease;
            }
            else if (e.Delta < 0)
            {
                Radius = Math.Max(0, Radius - radiusIncrease);
            }
        }

        protected override void Draw(Graphics g)
        {
            if (Area.Width > 0 && Area.Height > 0)
            {
                regionPath = new GraphicsPath();

                regionPath.AddRoundedRectangle(new Rectangle(Area.X, Area.Y, Area.Width - 1, Area.Height - 1), Radius);

                using (Region region = new Region(regionPath))
                {
                    g.ExcludeClip(region);
                    g.FillRectangle(shadowBrush, 0, 0, Width, Height);
                    g.ResetClip();
                }

                g.DrawPath(borderPen, regionPath);

                if (Radius >= 30)
                {
                    g.DrawRectangle(borderPen, Area.X, Area.Y, Area.Width - 1, Area.Height - 1);
                }
            }
            else
            {
                g.FillRectangle(shadowBrush, 0, 0, Width, Height);
            }

            DrawObjects(g);
        }
    }
}
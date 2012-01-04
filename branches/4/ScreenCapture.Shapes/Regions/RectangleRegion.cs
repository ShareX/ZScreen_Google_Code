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
        public AreaManager AreaManager { get; private set; }

        public RectangleRegion(Image backgroundImage = null)
            : base(backgroundImage)
        {
            AreaManager = new AreaManager(this);
            AreaManager.WindowCaptureMode = true;
            AreaManager.IncludeControls = true;
        }

        protected override void Update()
        {
            base.Update();
            AreaManager.Update();
        }

        protected override void Draw(Graphics g)
        {
            if (AreaManager.Areas.Count > 0 || !AreaManager.CurrentHoverArea.IsEmpty)
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

                if (!AreaManager.CurrentHoverArea.IsEmpty && !AreaManager.Areas.Contains(AreaManager.CurrentHoverArea))
                {
                    AddShapePath(regionPath, AreaManager.CurrentHoverArea);
                }

                using (Region region = new Region(regionPath))
                {
                    g.ExcludeClip(region);
                    g.FillRectangle(shadowBrush, 0, 0, Width, Height);
                    g.ResetClip();
                }

                g.DrawPath(borderPen, regionPath);

                if (!AreaManager.CurrentHoverArea.IsEmpty)
                {
                    GraphicsPath regionPathHover = new GraphicsPath();
                    AddShapePath(regionPathHover, AreaManager.CurrentHoverArea);

                    g.FillPath(lightBrush, regionPathHover);
                    //g.DrawRectangleProper(borderDotPen, AreaManager.CurrentHoverArea);
                    g.DrawPath(borderDotPen, regionPathHover);
                    g.DrawPath(borderDotPen2, regionPathHover);
                }

                if (!AreaManager.CurrentArea.IsEmpty)
                {
                    g.DrawRectangleProper(borderPen, AreaManager.CurrentArea);
                    g.ExcludeClip(AreaManager.CurrentArea);
                    DrawObjects(g);
                    g.ResetClip();
                }

                foreach (Rectangle area in AreaManager.Areas)
                {
                    if (area.Width > 0 && area.Height > 0)
                    {
                        g.Clip = new Region(area);

                        CaptureHelpers.DrawTextWithOutline(g, string.Format("X:{0}, Y:{1}, Width:{2}, Height:{3}", area.X, area.Y, area.Width, area.Height),
                            new PointF(area.X + 5, area.Y + 5), textFont, Color.White, Color.Black);
                    }
                }

                g.ResetClip();

                if (AreaManager.Areas.Count > 1)
                {
                    Rectangle totalArea = AreaManager.CombineAreas();
                    g.DrawCrossRectangle(borderPen, totalArea, 15);
                    CaptureHelpers.DrawTextWithOutline(g, string.Format("X:{0}, Y:{1}, Width:{2}, Height:{3}", totalArea.X, totalArea.Y,
                        totalArea.Width, totalArea.Height), new PointF(totalArea.X + 5, totalArea.Y - 20), textFont, Color.White, Color.Black);
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
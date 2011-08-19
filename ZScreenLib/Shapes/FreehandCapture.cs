#region License Information (GPL v2)

/*
    ZScreen - A program that allows you to upload screenshots in one keystroke.
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
using HelpersLib;

namespace ZScreenLib.Shapes
{
    public sealed class FreehandCapture : ShapeCapture
    {
        private bool leftDown;
        private Point lastPosition;
        private const string helpText = "Left click = Draw regions.\nRight click = Remove regions.\nEnter = Upload drawn regions.\nEscape = Cancel upload.";

        protected override void MouseDownEvent(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                leftDown = true;
                path.StartFigure();
                lastPosition = Cursor.Position;
            }
            else if (e.Button == MouseButtons.Right)
            {
                leftDown = false;
                path.Reset();
                Draw();
            }
        }

        protected override void MouseUpEvent(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                leftDown = false;
                path.CloseFigure();
                Draw();

                if (Engine.conf.FreehandCropAutoUpload)
                {
                    Exit(true);
                }
            }
            else if (Engine.conf.FreehandCropAutoClose && e.Button == MouseButtons.Right)
            {
                Exit(false);
            }
        }

        protected override void UpdateEvent(object sender, EventArgs e)
        {
            if (leftDown)
            {
                Point currentPosition = Cursor.Position;

                if (lastPosition != null && currentPosition != lastPosition)
                {
                    path.AddLine(lastPosition, currentPosition);
                    lastPosition = currentPosition;
                }

                Draw();
            }
        }

        protected override void DrawEvent(PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            if (path != null && path.PointCount > 0)
            {
                g.SmoothingMode = SmoothingMode.HighQuality;
                g.CompositingMode = CompositingMode.SourceCopy;
                g.FillPath(PathBrush, path);
                g.DrawPath(PathPen, path);

                DrawRectangleBorder(g);
            }

            DrawHelpText(g);
        }

        private void DrawHelpText(Graphics g)
        {
            if (Engine.conf.FreehandCropShowHelpText)
            {
                g.CompositingMode = CompositingMode.SourceOver;
                g.SmoothingMode = SmoothingMode.HighSpeed;

                using (Font helpTextFont = new XFont("Arial", 10))
                {
                    Size textSize = Size.Round(g.MeasureString(helpText, helpTextFont, 500, StringFormat.GenericTypographic));
                    Point textPos = PointToClient(new Point(this.Left + (this.Width / 2) - ((textSize.Width + 10) / 2), this.Top + 30));
                    Rectangle labelRect = new Rectangle(textPos, new Size(textSize.Width + 10, textSize.Height + 10));
                    using (GraphicsPath gPath = GraphicsEx.GetRoundedRectangle(labelRect, 7))
                    {
                        g.FillPath(new SolidBrush(Color.FromArgb(200, Color.White)), gPath);
                        g.DrawPath(Pens.Black, gPath);
                        g.DrawString(helpText, helpTextFont, Brushes.Black, new PointF(labelRect.X + 5, labelRect.Y + 5));
                    }
                }
            }
        }

        private void DrawRectangleBorder(Graphics g)
        {
            if (Engine.conf.FreehandCropShowRectangleBorder)
            {
                g.CompositingMode = CompositingMode.SourceOver;
                Rectangle rect = Rectangle.Round(path.GetBounds());
                g.DrawRectangle(BorderPen, rect);
                using (Font font = new Font(FontFamily.GenericSansSerif, 12, FontStyle.Bold))
                {
                    string text = string.Format("{0}x{1}", rect.Width, rect.Height);
                    Size textSize = Size.Round(g.MeasureString(text, font, 500, StringFormat.GenericDefault));
                    g.DrawString(text, font, Brushes.Black, new PointF(rect.Right - textSize.Width - 10, rect.Bottom - textSize.Height - 10));
                }
            }
        }
    }
}
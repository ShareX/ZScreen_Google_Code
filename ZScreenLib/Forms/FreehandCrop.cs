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

using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using GraphicsMgrLib;

namespace ZScreenLib.Forms
{
    public partial class FreehandCrop : LayeredForm
    {
        private Brush backBrush = new SolidBrush(Color.FromArgb(1, Color.White));
        private GraphicsPath path = new GraphicsPath();
        private Point lastPosition;
        private Bitmap bmp;
        private Pen pathPen = new Pen(Brushes.Red, 2);
        private Brush pathBrush = new SolidBrush(Color.FromArgb(50, Color.CornflowerBlue));
        private bool leftDown;
        private Timer timer = new Timer();

        public FreehandCrop()
        {
            Initialize();
        }

        private void Initialize()
        {
            this.ShowInTaskbar = false;
            this.TopMost = true;
            this.Bounds = GraphicsMgr.GetScreenBounds();
            bmp = new Bitmap(this.Width, this.Height);
            Draw();
            this.MouseDown += new MouseEventHandler(Crop_MouseDown);
            this.MouseUp += new MouseEventHandler(Crop_MouseUp);
            this.KeyDown += new KeyEventHandler(Crop_KeyDown);
            this.Shown += new EventHandler(FreehandCrop_Shown);
            path.FillMode = FillMode.Winding;
            path.StartFigure();
            timer.Interval = 50;
            timer.Tick += new EventHandler(timer_Tick);
            timer.Start();
        }

        private void FreehandCrop_Shown(object sender, EventArgs e)
        {
            NativeMethods.ActivateWindow(this.Handle);
        }

        private void CleanBackground(Graphics g)
        {
            g.Clear(Color.Transparent);
            g.FillRectangle(backBrush, new Rectangle(0, 0, bmp.Width, bmp.Height));
        }

        private void timer_Tick(object sender, EventArgs e)
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

        private void Crop_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape || e.KeyCode == Keys.Enter)
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void Crop_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                leftDown = true;
                path.Reset();
                path.StartFigure();
                lastPosition = Cursor.Position;
            }
        }

        private void Crop_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                path.CloseFigure();
                leftDown = false;
                Draw();
            }
            else if (e.Button == MouseButtons.Right)
            {
                this.DialogResult = DialogResult.Abort;
                this.Close();
            }
        }

        private void Draw()
        {
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.SmoothingMode = SmoothingMode.HighSpeed;
                CleanBackground(g);

                if (path != null)
                {
                    g.FillPath(pathBrush, path);
                    g.DrawPath(pathPen, path);
                }

                DrawBitmap(bmp);
            }
        }

        public Image GetScreenshot(Image fullscreenSS)
        {
            Rectangle rect = Rectangle.Round(path.GetBounds());

            Bitmap screenshot = new Bitmap(rect.Width, rect.Height);

            using (Graphics g = Graphics.FromImage(screenshot))
            {
                using (Matrix translateMatrix = new Matrix())
                {
                    translateMatrix.Translate(-path.GetBounds().X, -path.GetBounds().Y);
                    path.Transform(translateMatrix);
                }

                g.IntersectClip(new Region(path));

                g.CompositingQuality = CompositingQuality.HighQuality;
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.DrawImage(fullscreenSS, new Rectangle(0, 0, rect.Width, rect.Height), rect, GraphicsUnit.Pixel);
            }

            return screenshot;
        }
    }
}
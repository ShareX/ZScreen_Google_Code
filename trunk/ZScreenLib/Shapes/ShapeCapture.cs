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

#endregion License Information (GPL v2)

using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using GraphicsMgrLib;
using ZScreenLib.Forms;

namespace ZScreenLib.Shapes
{
    public abstract class ShapeCapture : LayeredForm
    {
        public Color BackgroundColor { get; set; }
        public Pen PathPen { get; set; }
        public Brush PathBrush { get; set; }
        public Pen BorderPen { get; set; }

        protected Bitmap surface;
        protected GraphicsPath path;

        private Timer updateTimer;

        public ShapeCapture()
        {
            Initialize();
        }

        private void Initialize()
        {
            this.ShowInTaskbar = false;
            this.Bounds = GraphicsMgr.GetScreenBounds();

            this.BackgroundColor = Color.FromArgb(50, Color.Black);
            this.PathPen = new Pen(Brushes.Red, 2) { DashStyle = DashStyle.Dash };
            this.PathBrush = new SolidBrush(Color.FromArgb(10, Color.White));
            this.BorderPen = new Pen(new SolidBrush(Color.FromArgb(175, Color.Black)), 2);

            this.surface = new Bitmap(this.Width, this.Height);
            this.path = new GraphicsPath(FillMode.Winding);
            path.StartFigure();

            this.KeyDown += new KeyEventHandler(KeyDownEvent);
            this.MouseDown += new MouseEventHandler(MouseDownEvent);
            this.MouseUp += new MouseEventHandler(MouseUpEvent);
            this.Shown += new EventHandler(FreehandCrop_Shown);

            updateTimer = new Timer { Interval = 20 };
            updateTimer.Tick += new EventHandler(UpdateEvent);
        }

        protected override void Dispose(bool disposing)
        {
            updateTimer.Dispose();
            surface.Dispose();
            path.Dispose();
            PathPen.Dispose();
            PathBrush.Dispose();
            BorderPen.Dispose();

            base.Dispose(disposing);
        }

        protected abstract void MouseDownEvent(object sender, MouseEventArgs e);

        protected abstract void MouseUpEvent(object sender, MouseEventArgs e);

        protected abstract void UpdateEvent(object sender, EventArgs e);

        protected abstract void DrawEvent(PaintEventArgs e);

        private void KeyDownEvent(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Space)
            {
                Exit(true);
            }
            else if (e.KeyCode == Keys.Escape)
            {
                Exit(false);
            }
        }

        private void FreehandCrop_Shown(object sender, EventArgs e)
        {
            NativeMethods.ActivateWindow(this.Handle);
            Draw();
            updateTimer.Start();
        }

        private void ClearBackground(Graphics g)
        {
            g.Clear(this.BackgroundColor);
        }

        protected void Exit(bool status)
        {
            if (status && path.PointCount > 0)
            {
                this.DialogResult = DialogResult.OK;
            }
            else
            {
                this.DialogResult = DialogResult.Abort;
            }

            this.Close();
        }

        protected void Draw()
        {
            using (Graphics g = Graphics.FromImage(surface))
            {
                ClearBackground(g);

                DrawEvent(new PaintEventArgs(g, new Rectangle(0, 0, surface.Width, surface.Height)));

                DrawBitmap(surface);
            }
        }

        public Image GetScreenshot(Image fullscreenSS)
        {
            Console.WriteLine(path.GetBounds().ToString());
            Rectangle rect = Rectangle.Round(path.GetBounds());
            rect.Location = NativeMethods.ConvertPoint(rect.Location);

            Bitmap screenshot = new Bitmap(rect.Width, rect.Height);

            using (Graphics g = Graphics.FromImage(screenshot))
            {
                using (Matrix translateMatrix = new Matrix())
                {
                    rect.Location = NativeMethods.ConvertPoint(rect.Location);
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
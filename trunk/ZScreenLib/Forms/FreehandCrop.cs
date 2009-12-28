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
        private Color backColor = Color.FromArgb(50, Color.Black);
        private GraphicsPath path = new GraphicsPath(FillMode.Alternate);
        private Point lastPosition;
        private Bitmap bmp;
        private Pen pathPen = new Pen(Brushes.Red, 2) { DashStyle = DashStyle.Dash };
        private Pen borderPen = new Pen(new SolidBrush(Color.FromArgb(175, Color.Black)), 2);
        private Brush pathBrush = new SolidBrush(Color.FromArgb(10, Color.White));
        private bool leftDown;
        private Timer timer = new Timer();

        private const string helpText = "Left click = Draw regions.\nRight click = Remove regions.\nEnter = Upload drawn regions.\nEscape = Cancel upload.";

        public FreehandCrop()
        {
            Initialize();
        }

        protected override void Dispose(bool disposing)
        {
            path.Dispose();
            bmp.Dispose();
            pathPen.Dispose();
            borderPen.Dispose();
            pathBrush.Dispose();
            timer.Dispose();

            base.Dispose(disposing);
        }

        private void Initialize()
        {
            this.ShowInTaskbar = false;
            this.Bounds = GraphicsMgr.GetScreenBounds();
            bmp = new Bitmap(this.Width, this.Height);
            Draw();
            this.MouseDown += new MouseEventHandler(Crop_MouseDown);
            this.MouseUp += new MouseEventHandler(Crop_MouseUp);
            this.KeyDown += new KeyEventHandler(Crop_KeyDown);
            this.Shown += new EventHandler(FreehandCrop_Shown);
            path.StartFigure();
            timer.Interval = 16;
            timer.Tick += new EventHandler(timer_Tick);
        }

        private void FreehandCrop_Shown(object sender, EventArgs e)
        {
            NativeMethods.ActivateWindow(this.Handle);
            timer.Start();
        }

        private void CleanBackground(Graphics g)
        {
            g.Clear(backColor);
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
            if (e.KeyCode == Keys.Enter)
            {
                Exit(true);
            }
            else if (e.KeyCode == Keys.Escape)
            {
                Exit(false);
            }
        }

        private void Crop_MouseDown(object sender, MouseEventArgs e)
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

        private void Crop_MouseUp(object sender, MouseEventArgs e)
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

        private void DrawHelpText(Graphics g)
        {
            if (Engine.conf.FreehandCropShowHelpText)
            {
                g.CompositingMode = CompositingMode.SourceOver;
                g.SmoothingMode = SmoothingMode.HighSpeed;

                using (Font helpTextFont = new Font("Arial", 10))
                {
                    Size textSize = Size.Round(g.MeasureString(helpText, helpTextFont, 500, StringFormat.GenericTypographic));
                    Point textPos = PointToClient(new Point(this.Left + (this.Width / 2) - ((textSize.Width + 10) / 2), this.Top + 30));
                    Rectangle labelRect = new Rectangle(textPos, new Size(textSize.Width + 10, textSize.Height + 10));
                    GraphicsPath gPath = RoundedRectangle.Create(labelRect, 7);
                    g.FillPath(new SolidBrush(Color.FromArgb(200, Color.White)), gPath);
                    g.DrawPath(Pens.Black, gPath);
                    g.DrawString(helpText, helpTextFont, Brushes.Black, new PointF(labelRect.X + 5, labelRect.Y + 5));
                }
            }
        }

        private void DrawRectangleBorder(Graphics g)
        {
            if (Engine.conf.FreehandCropShowRectangleBorder)
            {
                g.CompositingMode = CompositingMode.SourceOver;
                Rectangle rect = Rectangle.Round(path.GetBounds());
                g.DrawRectangle(borderPen, rect);
                using (Font font = new Font(FontFamily.GenericSansSerif, 12, FontStyle.Bold))
                {
                    string text = string.Format("{0}x{1}", rect.Width, rect.Height);
                    Size textSize = Size.Round(g.MeasureString(text, font, 500, StringFormat.GenericDefault));
                    g.DrawString(text, font, Brushes.Black, new PointF(rect.Right - textSize.Width - 10, rect.Bottom - textSize.Height - 10));
                }
            }
        }

        private void Exit(bool status)
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

        private void Draw()
        {
            using (Graphics g = Graphics.FromImage(bmp))
            {
                CleanBackground(g);

                if (path != null && path.PointCount > 0)
                {
                    g.SmoothingMode = SmoothingMode.HighQuality;
                    g.CompositingMode = CompositingMode.SourceCopy;
                    g.FillPath(pathBrush, path);
                    g.DrawPath(pathPen, path);

                    DrawRectangleBorder(g);
                }

                DrawHelpText(g);

                DrawBitmap(bmp);
            }
        }

        public Image GetScreenshot(Image fullscreenSS)
        {
            Rectangle rect = Rectangle.Round(path.GetBounds());
            rect.Location = NativeMethods.ConvertPoint(rect.Location);

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
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
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;
using HelpersLib;

namespace ScreenCapture
{
    public class Surface : Form
    {
        public Image SurfaceImage { get; protected set; }
        public SurfaceOptions Config { get; set; }
        public int FPS { get; private set; }

        protected List<DrawableObject> DrawableObjects { get; set; }

        private TextureBrush backgroundBrush;
        private Rectangle screenArea;
        protected Stopwatch timer;
        private int frameCount;

        protected GraphicsPath regionPath;
        protected Pen borderPen, borderDotPen, borderDotPen2;
        protected Brush shadowBrush, lightBrush, nodeBackgroundBrush;
        protected Font textFont;

        public Surface(Image backgroundImage = null)
        {
            screenArea = CaptureHelpers.GetScreenBounds();

            InitializeComponent();

            DrawableObjects = new List<DrawableObject>();

            if (backgroundImage != null)
            {
                LoadBackground(backgroundImage);
            }

            Config = new SurfaceOptions();

            timer = new Stopwatch();

            borderPen = new Pen(Color.DarkBlue);
            borderDotPen = new Pen(Color.Black, 1);
            borderDotPen.DashPattern = new float[] { 5, 5 };
            borderDotPen2 = new Pen(Color.White, 1);
            borderDotPen2.DashPattern = new float[] { 5, 5 };
            borderDotPen2.DashOffset = 5;
            shadowBrush = new SolidBrush(Color.FromArgb(75, Color.Black));
            lightBrush = new SolidBrush(Color.FromArgb(10, Color.Black));
            nodeBackgroundBrush = new SolidBrush(Color.White);
            textFont = new Font("Arial", 12, FontStyle.Bold);

            Shown += new EventHandler(Surface_Shown);
            KeyUp += new KeyEventHandler(Surface_KeyUp);
            MouseDoubleClick += new MouseEventHandler(Surface_MouseDoubleClick);
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            this.Bounds = screenArea;
            this.FormBorderStyle = FormBorderStyle.None;
            this.StartPosition = FormStartPosition.Manual;
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint, true);
            this.Text = "RegionCapture";
#if !DEBUG
            this.TopMost = true;
#endif
            this.ResumeLayout(false);
        }

        private void Surface_Shown(object sender, EventArgs e)
        {
            Activate();
        }

        private void Surface_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                Close(false);
            }
            else if (e.KeyCode == Keys.Space || e.KeyCode == Keys.Enter)
            {
                Close(true);
            }
        }

        private void Surface_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Close(true);
            }
        }

        public void LoadBackground(Image backgroundImage)
        {
            SurfaceImage = backgroundImage;
            backgroundBrush = new TextureBrush(backgroundImage);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
#if DEBUG
            if (!timer.IsRunning) timer.Start();
#endif

            Update();

            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.HighSpeed;
            g.FillRectangle(backgroundBrush, screenArea);

#if DEBUG
            g.DrawRectangleProper(Pens.Yellow, screenArea);
#endif

            Draw(g);

#if DEBUG
            CheckFPS();
            DrawInfo(g);
#endif

            Invalidate();
        }

        public virtual Image GetRegionImage()
        {
            if (regionPath != null)
            {
                Image img;

                Rectangle regionArea = Rectangle.Round(regionPath.GetBounds());
                regionArea.Width++;
                regionArea.Height++;
                Rectangle newRegionArea = Rectangle.Intersect(regionArea, screenArea);

                using (GraphicsPath gp = (GraphicsPath)regionPath.Clone())
                using (Matrix matrix = new Matrix())
                {
                    gp.CloseFigure();
                    matrix.Translate(-Math.Max(0, regionArea.X), -Math.Max(0, regionArea.Y));
                    gp.Transform(matrix);

                    img = CaptureHelpers.CropImage(SurfaceImage, newRegionArea, gp);

                    if (Config.DrawBorder)
                    {
                        img = CaptureHelpers.DrawOutline(img, gp);
                    }
                }

                if (Config.DrawChecker)
                {
                    img = CaptureHelpers.DrawCheckers(img);
                }

                return img;
            }

            return null;
        }

        public void Close(bool isOK)
        {
            if (isOK)
            {
                DialogResult = DialogResult.OK;
            }
            else
            {
                DialogResult = DialogResult.Cancel;
            }
        }

        protected virtual new void Update()
        {
            InputManager.Update();

            DrawableObject[] objects = DrawableObjects.OrderByDescending(x => x.Order).ToArray();

            if (objects.All(x => x.Visible && !x.IsDragging))
            {
                for (int i = 0; i < objects.Count(); i++)
                {
                    DrawableObject obj = objects[i];

                    obj.IsMouseHover = obj.Rectangle.Contains(InputManager.MousePosition);

                    if (obj.IsMouseHover)
                    {
                        for (int y = i + 1; y < objects.Count(); y++)
                        {
                            objects[y].IsMouseHover = false;
                        }

                        break;
                    }
                }

                foreach (DrawableObject obj in objects)
                {
                    if (obj.IsMouseHover && InputManager.IsMousePressed(MouseButtons.Left))
                    {
                        obj.IsDragging = true;
                        break;
                    }
                }
            }
            else
            {
                if (InputManager.IsMouseReleased(MouseButtons.Left))
                {
                    foreach (DrawableObject obj in objects)
                    {
                        obj.IsDragging = false;
                    }
                }
            }
        }

        protected virtual void Draw(Graphics g)
        {
            DrawObjects(g);
        }

        protected void DrawObjects(Graphics g)
        {
            foreach (DrawableObject drawObject in DrawableObjects)
            {
                if (drawObject.Visible)
                {
                    drawObject.Draw(g);
                }
            }
        }

        private void CheckFPS()
        {
            frameCount++;

            if (timer.ElapsedMilliseconds >= 1000)
            {
                FPS = frameCount;
                frameCount = 0;
                timer.Reset();
                timer.Start();
                EverySecond();
            }
        }

        protected virtual void EverySecond()
        {
        }

        private void DrawInfo(Graphics g)
        {
            string text = string.Format("FPS: {0}\nBounds: {1}", FPS, Bounds);

            SizeF textSize = g.MeasureString(text, textFont);

            int offset = 30;

            Rectangle primaryScreen = Screen.PrimaryScreen.Bounds;

            Point position = CaptureHelpers.FixScreenCoordinates(new Point(primaryScreen.X + (int)(primaryScreen.Width / 2 - textSize.Width / 2), primaryScreen.Y + offset - 1));
            Rectangle rect = new Rectangle(position, new Size((int)textSize.Width, (int)textSize.Height));

            if (rect.Contains(InputManager.MousePosition))
            {
                position = CaptureHelpers.FixScreenCoordinates(new Point(primaryScreen.X + (int)(primaryScreen.Width / 2 - textSize.Width / 2),
                    primaryScreen.Y + primaryScreen.Height - (int)textSize.Height - offset - 1));
            }

            CaptureHelpers.DrawTextWithOutline(g, text, position, textFont, Color.White, Color.Black);
        }

        protected Rectangle CalculateAreaFromNodes()
        {
            IEnumerable<NodeObject> nodes = DrawableObjects.OfType<NodeObject>().Where(x => x.Visible);

            if (nodes.Count() > 1)
            {
                int left, top, right, bottom;
                left = (int)nodes.Min(x => x.Position.X);
                top = (int)nodes.Min(x => x.Position.Y);
                right = (int)nodes.Max(x => x.Position.X);
                bottom = (int)nodes.Max(x => x.Position.Y);

                return CaptureHelpers.CreateRectangle(new Point(left, top), new Point(right, bottom));
            }

            return Rectangle.Empty;
        }

        public NodeObject MakeNode()
        {
            NodeObject node = new NodeObject(borderPen, nodeBackgroundBrush);
            DrawableObjects.Add(node);
            return node;
        }

        protected void ShowNodes()
        {
            foreach (NodeObject node in DrawableObjects.OfType<NodeObject>())
            {
                node.Visible = true;
            }
        }

        protected void HideNodes()
        {
            foreach (NodeObject node in DrawableObjects.OfType<NodeObject>())
            {
                node.Visible = false;
            }
        }

        private IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }

            if (backgroundBrush != null) backgroundBrush.Dispose();
            if (regionPath != null) regionPath.Dispose();
            if (borderPen != null) borderPen.Dispose();
            if (borderDotPen != null) borderDotPen.Dispose();
            if (borderDotPen2 != null) borderDotPen2.Dispose();
            if (shadowBrush != null) shadowBrush.Dispose();
            if (nodeBackgroundBrush != null) nodeBackgroundBrush.Dispose();
            if (textFont != null) textFont.Dispose();

            base.Dispose(disposing);
        }
    }
}
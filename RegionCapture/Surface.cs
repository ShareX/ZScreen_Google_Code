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
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;

namespace RegionCapture
{
    public class Surface : Form
    {
        public Image SurfaceImage { get; protected set; }

        protected Rectangle area;
        public Rectangle Area
        {
            get { return area; }
            protected set { area = value; }
        }

        protected bool AutoCalculateArea { get; set; } //TODO:?
        protected List<DrawableObject> DrawableObjects { get; set; }
        protected Point ClientMousePosition { get { return PointToClient(MousePosition); } }

        public int FPS { get; private set; }

        private TextureBrush backgroundBrush;
        private Stopwatch timer;
        private int frameCount;

        protected GraphicsPath regionPath;
        protected Pen borderPen;
        protected Brush shadowBrush, nodeBackgroundBrush;
        protected Font textFont;
        protected Point mousePosition, oldMousePosition;
        protected bool isMouseDown, oldIsMouseDown;

        public Surface(Image backgroundImage = null)
        {
            InitializeComponent();

            if (backgroundImage != null)
            {
                LoadBackground(backgroundImage);
            }

            DrawableObjects = new List<DrawableObject>();
            AutoCalculateArea = true;

            timer = new Stopwatch();

            borderPen = new Pen(Color.CornflowerBlue);
            shadowBrush = new SolidBrush(Color.FromArgb(75, Color.Black));
            nodeBackgroundBrush = new SolidBrush(Color.White);
            textFont = new Font("Arial", 18, FontStyle.Bold);

            MouseDown += new MouseEventHandler(Surface_MouseDown);
            MouseUp += new MouseEventHandler(Surface_MouseUp);
            KeyUp += new KeyEventHandler(Surface_KeyUp);
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
            AfterUpdate();

            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.HighSpeed;
            g.FillRectangle(backgroundBrush, Bounds);

#if DEBUG
            g.DrawRectangle(Pens.Yellow, Bounds.X, Bounds.Y, Bounds.Width - 1, Bounds.Height - 1);
#endif

            Draw(g);

#if DEBUG
            CheckFPS();
            DrawDebug(g);
#endif

            Invalidate();
        }

        public virtual Image GetRegionImage()
        {
            Image img = null;

            if (regionPath != null)
            {
                img = Helpers.CropImage(SurfaceImage, regionPath);
            }
            else
            {
                img = Helpers.CropImage(SurfaceImage, Area);
            }

            Debug.WriteLine("Image width: " + img.Width + ", height: " + img.Height);

            return img;
        }

        private void Surface_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isMouseDown = true;
            }
        }

        private void Surface_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isMouseDown = false;
            }
        }

        private void Surface_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                Close(true);
            }
            else if (e.KeyCode == Keys.Space || e.KeyCode == Keys.Enter)
            {
                Close(false);
            }
        }

        private void Close(bool isCancel = false)
        {
            if (isCancel)
            {
                DialogResult = DialogResult.Cancel;
            }
            else
            {
                DialogResult = DialogResult.OK;
            }
        }

        protected virtual new void Update()
        {
            mousePosition = ClientMousePosition;

            foreach (DrawableObject drawObject in DrawableObjects)
            {
                drawObject.IsMouseHover = drawObject.Rectangle.Contains(mousePosition);

                if (drawObject.IsMouseHover && !oldIsMouseDown && isMouseDown)
                {
                    drawObject.IsHolding = true;
                    break;
                }
                else if (oldIsMouseDown && !isMouseDown)
                {
                    drawObject.IsHolding = false;
                }
            }
        }

        protected virtual void AfterUpdate()
        {
            oldMousePosition = mousePosition;
            oldIsMouseDown = isMouseDown;
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

        private void DrawDebug(Graphics g)
        {
            string text = string.Format("FPS: {0}\nX: {1}, Y: {2}\nWidth: {3}, Height: {4}", FPS, Area.X, Area.Y, Area.Width, Area.Height);
            SizeF size = g.MeasureString(text, textFont);
            Helpers.DrawTextWithShadow(g, text, new PointF(Width / 2 - size.Width / 2, 30), textFont, Color.White, Color.Black, 1);
        }

        protected Rectangle CalculateAreaFromNodes()
        {
            IEnumerable<NodeObject> nodes = DrawableObjects.OfType<NodeObject>().Where(x => x.Visible);

            if (nodes.Count() > 1)
            {
                int left, top, right, bottom;
                left = nodes.Min(x => x.Position.X);
                top = nodes.Min(x => x.Position.Y);
                right = nodes.Max(x => x.Position.X);
                bottom = nodes.Max(x => x.Position.Y);

                return Helpers.CreateRectangle(new Point(left, top), new Point(right, bottom));
            }

            return Rectangle.Empty;
        }

        #region Windows Form Designer generated code

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
            if (shadowBrush != null) shadowBrush.Dispose();
            if (nodeBackgroundBrush != null) nodeBackgroundBrush.Dispose();
            if (textFont != null) textFont.Dispose();

            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            this.Bounds = Helpers.GetScreenBounds();
            this.FormBorderStyle = FormBorderStyle.None;
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint, true);
            this.Text = "RegionCapture";
#if !DEBUG
            this.TopMost = true;
#endif
            this.ResumeLayout(false);
        }

        #endregion Windows Form Designer generated code
    }
}
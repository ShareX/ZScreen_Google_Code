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
        public Rectangle Area { get; set; }

        public List<DrawableObject> DrawableObjects { get; private set; }

        public Point ClientMousePosition { get { return PointToClient(MousePosition); } }

        public bool AutoCalculateArea { get; protected set; }

        public int FPS { get; private set; }

        private TextureBrush backgroundBrush;
        private Stopwatch timer;
        private int frameCount;

        protected Pen borderPen;
        protected Brush shadowBrush, nodeBackgroundBrush;
        protected Font textFont;
        protected Point mousePosition, oldMousePosition;
        protected bool isMouseDown, oldIsMouseDown;

        public Surface(Image backgroundImage)
        {
            InitializeComponent();

            DrawableObjects = new List<DrawableObject>();
            AutoCalculateArea = true;

            backgroundBrush = new TextureBrush(backgroundImage);
            timer = new Stopwatch();

            borderPen = Pens.CornflowerBlue;
            shadowBrush = new SolidBrush(Color.FromArgb(75, Color.Black));
            nodeBackgroundBrush = Brushes.White;
            textFont = new Font("Arial", 22, FontStyle.Bold);

            MouseDown += new MouseEventHandler(Surface_MouseDown);
            MouseUp += new MouseEventHandler(Surface_MouseUp);
            KeyUp += new KeyEventHandler(Surface_KeyUp);
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
            if (e.KeyCode == Keys.Escape) Close();
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
                }
                else if (oldIsMouseDown && !isMouseDown)
                {
                    drawObject.IsHolding = false;
                }
            }
        }

        protected virtual void AfterUpdate()
        {
            if (AutoCalculateArea)
            {
                Area = CalculateAreaFromNodes();
            }

            oldMousePosition = mousePosition;
            oldIsMouseDown = isMouseDown;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (!timer.IsRunning) timer.Start();

            Update();
            AfterUpdate();

            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.HighSpeed;
            g.FillRectangle(backgroundBrush, Bounds);

            Draw(g);

            CheckFPS();

            Invalidate();
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
                Debug.WriteLine("FPS: " + FPS);
                frameCount = 0;
                timer.Reset();
                timer.Start();
            }
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
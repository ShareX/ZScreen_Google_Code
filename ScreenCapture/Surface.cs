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

namespace ScreenCapture
{
    public class Surface : Form
    {
        public Image SurfaceImage { get; protected set; }

        public bool DrawBorder { get; set; }
        public bool DrawChecker { get; set; }

        public int MinMoveSpeed { get; set; }
        public int MaxMoveSpeed { get; set; }

        protected Rectangle area;

        public Rectangle Area
        {
            get { return area; }
            protected set { area = value; }
        }

        public int FPS { get; private set; }

        protected bool IsAreaCreated { get; set; }

        protected List<DrawableObject> DrawableObjects { get; set; }
        protected Point ClientMousePosition { get { return PointToClient(MousePosition); } }

        private TextureBrush backgroundBrush;
        private Stopwatch timer;
        private int frameCount;

        protected GraphicsPath regionPath;
        protected Pen borderPen;
        protected Brush shadowBrush, lightBrush, nodeBackgroundBrush;
        protected Font textFont;
        protected Point mousePosition, oldMousePosition;
        protected bool isMouseDown, oldIsMouseDown;

        private bool isBottomRightMoving = true;

        public Surface(Image backgroundImage = null)
        {
            InitializeComponent();

            if (backgroundImage != null)
            {
                LoadBackground(backgroundImage);
            }

            MinMoveSpeed = 1;
            MaxMoveSpeed = 5;
            DrawableObjects = new List<DrawableObject>();

            timer = new Stopwatch();

            borderPen = new Pen(Color.CornflowerBlue);
            shadowBrush = new SolidBrush(Color.FromArgb(75, Color.Black));
            lightBrush = new SolidBrush(Color.FromArgb(10, Color.Black));
            nodeBackgroundBrush = new SolidBrush(Color.White);
            textFont = new Font("Arial", 18, FontStyle.Bold);

            MouseDoubleClick += new MouseEventHandler(Surface_MouseDoubleClick);
            MouseDown += new MouseEventHandler(Surface_MouseDown);
            MouseUp += new MouseEventHandler(Surface_MouseUp);
            KeyDown += new KeyEventHandler(Surface_KeyDown);
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
#endif

            DrawInfo(g);

            Invalidate();
        }

        public virtual Image GetRegionImage()
        {
            Image img = SurfaceImage;

            if (regionPath != null)
            {
                using (GraphicsPath gp = (GraphicsPath)regionPath.Clone())
                using (Matrix matrix = new Matrix())
                {
                    gp.CloseFigure();
                    RectangleF bounds = gp.GetBounds();
                    matrix.Translate(-bounds.X, -bounds.Y);
                    gp.Transform(matrix);

                    img = Helpers.CropImage(img, Rectangle.Round(bounds), gp);

                    if (DrawBorder)
                    {
                        img = Helpers.DrawBorder(img, gp);
                    }
                }

                if (DrawChecker)
                {
                    img = Helpers.DrawCheckers(img);
                }
            }
            else
            {
                img = Helpers.CropImage(img, Area);

                if (DrawBorder)
                {
                    img = Helpers.DrawBorder(img);
                }
            }

            Debug.WriteLine("Image width: " + img.Width + ", height: " + img.Height);

            return img;
        }

        public void MoveArea(int x, int y)
        {
            area.Offset(x, y);
        }

        public void ShrinkArea(int x, int y)
        {
            if (isBottomRightMoving)
            {
                area = new Rectangle(area.Left, area.Top, area.Width + x, area.Height + y);
            }
            else
            {
                area = new Rectangle(area.Left + x, area.Top + y, area.Width - x, area.Height - y);
            }
        }

        private void Surface_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Close(false);
            }
        }

        private void Surface_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isMouseDown = true;
            }
            else if (e.Button == MouseButtons.Right)
            {
                OnRightClickCancel();
            }
        }

        protected virtual void OnRightClickCancel()
        {
            if (IsAreaCreated)
            {
                IsAreaCreated = false;
                area = Rectangle.Empty;
                HideNodes();
            }
            else
            {
                Close(true);
            }
        }

        private void Surface_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isMouseDown = false;
            }
        }

        private void Surface_KeyDown(object sender, KeyEventArgs e)
        {
            int speed;

            if (e.Control)
            {
                speed = MaxMoveSpeed;
            }
            else
            {
                speed = MinMoveSpeed;
            }

            switch (e.KeyCode)
            {
                case Keys.Left:
                    if (e.Shift) { MoveArea(-speed, 0); } else { ShrinkArea(-speed, 0); }
                    break;
                case Keys.Right:
                    if (e.Shift) { MoveArea(speed, 0); } else { ShrinkArea(speed, 0); }
                    break;
                case Keys.Up:
                    if (e.Shift) { MoveArea(0, -speed); } else { ShrinkArea(0, -speed); }
                    break;
                case Keys.Down:
                    if (e.Shift) { MoveArea(0, speed); } else { ShrinkArea(0, speed); }
                    break;
                case Keys.Tab:
                    isBottomRightMoving = !isBottomRightMoving;
                    break;
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

        protected void Close(bool isCancel = false)
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

            DrawableObject[] objects = DrawableObjects.OrderByDescending(x => x.Order).ToArray();

            if (objects.All(x => !x.IsDragging))
            {
                for (int i = 0; i < objects.Count(); i++)
                {
                    DrawableObject obj = objects[i];

                    if (obj.IsMouseHover = obj.Rectangle.Contains(mousePosition))
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
                    if (obj.IsMouseHover && !oldIsMouseDown && isMouseDown)
                    {
                        obj.IsDragging = true;
                        break;
                    }
                }
            }
            else
            {
                if (oldIsMouseDown && !isMouseDown)
                {
                    foreach (DrawableObject obj in objects)
                    {
                        obj.IsDragging = false;
                    }
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

        private void DrawInfo(Graphics g)
        {
            string text = string.Format("X: {0}, Y: {1}\nWidth: {2}, Height: {3}", Area.X, Area.Y, Area.Width, Area.Height);

#if DEBUG
            text = string.Format("FPS: {0}\n{1}", FPS, text);
#endif

            SizeF size = g.MeasureString(text, textFont);

            int offset = 30;
            RectangleF rect = new RectangleF(Width / 2 - size.Width / 2, offset - 1, size.Width, size.Height);

            if (rect.Contains(mousePosition))
            {
                rect = new RectangleF(Width / 2 - size.Width / 2, Height - size.Height - offset - 1, size.Width, size.Height);
            }

            Helpers.DrawTextWithShadow(g, text, rect.Location, textFont, Color.White, Color.Black, 1);
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
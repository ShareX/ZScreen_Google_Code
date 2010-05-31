using System;
using System.Drawing;
using System.Windows.Forms;
using GraphicsMgrLib;

namespace Crop
{
    public class RegionManager : IDisposable
    {
        private Rectangle rectangle;
        public Rectangle Rectangle
        {
            get
            {
                return rectangle;
            }
            set
            {
                rectangle = value;
                rectangle.Intersect(Crop.Bounds);
            }
        }

        public bool IsMouseDown { get; private set; }
        public bool IsRectangleCreated { get; private set; }
        public bool IsRectangleSelected { get; private set; }
        public bool IsMoving { get; private set; }

        public Pen RectanglePen { get; set; }
        public Brush RectangleBrush { get; set; }
        public Font TextFont { get; set; }
        public Brush TextBrush { get; set; }
        public Brush TextShadowBrush { get; set; }
        public ResizeManager Resize { get; private set; }

        private Crop2 Crop;
        private Point positionOnClick;
        private Point currentPosition;

        public RegionManager(Crop2 crop)
        {
            Crop = crop;
            Crop.MouseDown += new MouseEventHandler(Crop_MouseDown);
            Crop.MouseUp += new MouseEventHandler(Crop_MouseUp);
            Crop.MouseMove += new MouseEventHandler(Crop_MouseMove);
            RectanglePen = new Pen(Color.Red, 1);
            RectangleBrush = new SolidBrush(Color.FromArgb(50, Color.CornflowerBlue));
            TextFont = new Font("Arial", 16);
            TextBrush = new SolidBrush(Color.Black);
            TextShadowBrush = new SolidBrush(Color.White);
            Resize = new ResizeManager(crop, this);
        }

        public void Dispose()
        {
            if (RectanglePen != null) RectanglePen.Dispose();
            if (RectangleBrush != null) RectangleBrush.Dispose();
            if (TextFont != null) TextFont.Dispose();
            if (TextBrush != null) TextBrush.Dispose();
            if (TextShadowBrush != null) TextShadowBrush.Dispose();
        }

        private void Crop_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (!IsMouseDown && !IsRectangleCreated && !IsRectangleSelected && !IsMoving)
                {
                    IsMouseDown = true;
                    IsRectangleCreated = true;
                    positionOnClick = e.Location;
                    Rectangle = new Rectangle(positionOnClick, new Size(25, 25));
                }
                else if (IsMoveable())
                {
                    IsMoving = true;
                    positionOnClick = e.Location;
                }
            }
            else if (e.Button == MouseButtons.Right)
            {
                if (IsRectangleSelected)
                {
                    DeselectRectangle();
                }
            }
        }

        private void Crop_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (!IsRectangleSelected)
                {
                    IsMouseDown = false;
                    SelectRectangle();
                }

                if (IsMoving)
                {
                    IsMoving = false;
                }
            }

            if (e.Button == MouseButtons.Right)
            {
                if (IsRectangleCreated)
                {
                    IsRectangleCreated = false;
                }
                else
                {
                    Crop.Close();
                }
            }
        }

        public bool IsMoveable()
        {
            if (IsRectangleCreated && IsRectangleSelected && Rectangle.Contains(GetMousePosition()))
            {
                Crop.Cursor = Cursors.Hand;
                return true;
            }

            Crop.Cursor = Cursors.Default;
            return false;
        }

        private void Crop_MouseMove(object sender, MouseEventArgs e)
        {
            IsMoveable();

            if (IsMoving)
            {
                Rectangle rect = Rectangle;
                rect.X += e.X - positionOnClick.X;
                rect.Y += e.Y - positionOnClick.Y;
                positionOnClick = e.Location;
                Rectangle = rect;
                Resize.Update();
            }

            if (IsMouseDown && IsRectangleCreated && !IsRectangleSelected)
            {
                currentPosition = GetMousePosition();
                Rectangle rect = new Rectangle(positionOnClick.X, positionOnClick.Y, currentPosition.X - positionOnClick.X, currentPosition.Y - positionOnClick.Y);
                rectangle = GraphicsMgr.FixRectangle(rect);
            }
        }

        private void SelectRectangle()
        {
            IsRectangleSelected = true;
            Resize.Update();
            Resize.Show();
        }

        private void DeselectRectangle()
        {
            IsRectangleSelected = false;
            Resize.Hide();
        }

        private Point GetMousePosition()
        {
            return Crop.PointToClient(Crop2.MousePosition);
        }

        public void Draw(Graphics g)
        {
            if (IsRectangleCreated)
            {
                g.FillRectangle(RectangleBrush, Rectangle);

                Rectangle rect = Rectangle;
                rect.Width--;
                rect.Height--;
                g.DrawRectangle(RectanglePen, rect);

                string info = string.Format("x:{0} y:{1}\n{2} x {3}", rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height);
                g.DrawString(info, TextFont, TextShadowBrush, rectangle.X + 6, rectangle.Y + 6);
                g.DrawString(info, TextFont, TextBrush, rectangle.X + 5, rectangle.Y + 5);
            }
        }
    }
}
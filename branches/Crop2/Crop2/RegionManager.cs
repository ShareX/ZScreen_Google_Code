using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using GraphicsMgrLib;
using System;

namespace Crop
{
    public class RegionManager
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
                rectangle = GraphicsMgr.GetRectangle(value);
                rectangle.Intersect(Crop.Bounds);
            }
        }

        public bool IsMouseDown { get; private set; }
        public bool IsRectangleCreated { get; private set; }
        public bool IsRectangleSelected { get; private set; }
        public bool IsMoving { get; private set; }
        public Pen RectanglePen { get; set; }
        public Brush RectangleBrush { get; set; }
        public ResizeManager Resize { get; private set; }

        private Crop Crop;
        private Point positionOnClick;
        private Point currentPosition;

        public RegionManager(Crop crop)
        {
            Crop = crop;
            Crop.MouseDown += new MouseEventHandler(Crop_MouseDown);
            Crop.MouseUp += new MouseEventHandler(Crop_MouseUp);
            Crop.MouseMove += new MouseEventHandler(Crop_MouseMove);
            RectanglePen = new Pen(Color.Red, 2);
            RectanglePen.Alignment = PenAlignment.Inset;
            RectangleBrush = new SolidBrush(Color.FromArgb(100, Color.CornflowerBlue));
            Resize = new ResizeManager(crop, this);
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
                    Rectangle = new Rectangle(positionOnClick, new Size(20, 20));
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
                Rectangle = new Rectangle(positionOnClick.X, positionOnClick.Y, currentPosition.X - positionOnClick.X + 1, currentPosition.Y - positionOnClick.Y + 1);
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
            return Crop.PointToClient(Crop.MousePosition);
        }

        public void Draw(Graphics g)
        {
            if (IsRectangleCreated)
            {
                g.FillRectangle(RectangleBrush, Rectangle);
                g.DrawRectangle(RectanglePen, Rectangle);
            }
        }
    }
}
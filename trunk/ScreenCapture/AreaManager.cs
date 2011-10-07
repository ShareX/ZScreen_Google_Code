using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using HelpersLib;

namespace ScreenCapture
{
    public class AreaManager
    {
        public bool IsMouseDown { get; private set; }
        public bool IsMoving { get; private set; }

        public List<Rectangle> Areas = new List<Rectangle>();
        public Rectangle CurrentArea;

        private Surface surface;
        private Point positionOnClick;
        private Point currentPosition;

        public AreaManager(Surface surface)
        {
            this.surface = surface;
            surface.MouseDown += new MouseEventHandler(Crop_MouseDown);
            surface.MouseUp += new MouseEventHandler(Crop_MouseUp);
            surface.MouseMove += new MouseEventHandler(Crop_MouseMove);
        }

        private void Crop_MouseDown(object sender, MouseEventArgs e)
        {
            Rectangle area = IsAreaIntersect();

            if (e.Button == MouseButtons.Left)
            {
                if (area != null)
                {
                    IsMoving = true;
                    positionOnClick = e.Location;
                    CurrentArea = area;
                }
                else if (!IsMouseDown)
                {
                    IsMouseDown = true;
                    Rectangle rect = new Rectangle(positionOnClick, new Size(25, 25));
                    Areas.Add(rect);
                    positionOnClick = e.Location;
                    CurrentArea = rect;
                }
            }
            else if (e.Button == MouseButtons.Right)
            {
                if (area != null)
                {
                    Areas.Remove(area);
                }
                else
                {
                    surface.Close();
                }
            }
        }

        private void Crop_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (CurrentArea != null)
                {
                    IsMouseDown = false;
                }

                if (IsMoving)
                {
                    IsMoving = false;
                }
            }
        }

        private void Crop_MouseMove(object sender, MouseEventArgs e)
        {
            IsAreaIntersect();

            if (IsMoving)
            {
                Rectangle rect = CurrentArea;
                rect.X += e.X - positionOnClick.X;
                rect.Y += e.Y - positionOnClick.Y;
                positionOnClick = e.Location;
                CurrentArea = rect;
            }

            if (IsMouseDown && CurrentArea != null /*&& !CurrentArea.Selected*/)
            {
                currentPosition = GetMousePosition();
                Rectangle rect = new Rectangle(positionOnClick.X, positionOnClick.Y, currentPosition.X - positionOnClick.X, currentPosition.Y - positionOnClick.Y);
                CurrentArea = CaptureHelpers.FixRectangle(rect);
            }
        }

        public Rectangle IsAreaIntersect()
        {
            for (int i = Areas.Count - 1; i >= 0; i--)
            {
                if (Areas[i].Contains(GetMousePosition()))
                {
                    return Areas[i];
                }
            }

            return Rectangle.Empty;
        }

        private Point GetMousePosition()
        {
            return CaptureHelpers.GetZeroBasedMousePosition();
        }

        public Region CombineAreas()
        {
            Region region = new Region();
            region.MakeEmpty();

            foreach (Rectangle area in Areas)
            {
                region.Union(area);
            }

            //region.Intersect(new Region(Crop.Bounds));

            return region;
        }
    }
}
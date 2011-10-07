using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using GraphicsMgrLib;
using HelpersLib;

namespace Crop
{
    public class AreaManager
    {
        public bool IsMouseDown { get; private set; }
        public bool IsMoving { get; private set; }

        public ResizeManager Resize { get; private set; }

        public List<Area> Areas = new List<Area>();
        public Area CurrentArea;

        private Crop2 Crop;
        private Point positionOnClick;
        private Point currentPosition;

        public AreaManager(Crop2 crop)
        {
            Crop = crop;
            Crop.MouseDown += new MouseEventHandler(Crop_MouseDown);
            Crop.MouseUp += new MouseEventHandler(Crop_MouseUp);
            Crop.MouseMove += new MouseEventHandler(Crop_MouseMove);

            Resize = new ResizeManager(crop, this);
        }

        private void Crop_MouseDown(object sender, MouseEventArgs e)
        {
            Area area = IsAreaIntersect();

            if (e.Button == MouseButtons.Left)
            {
                if (area != null)
                {
                    IsMoving = true;
                    positionOnClick = e.Location;
                    CurrentArea = area;
                    SelectArea();
                }
                else if (!IsMouseDown)
                {
                    IsMouseDown = true;
                    DeselectArea();
                    RectangleArea newArea = new RectangleArea();
                    Areas.Add(newArea);
                    positionOnClick = e.Location;
                    newArea.Rectangle = new Rectangle(positionOnClick, new Size(25, 25));
                    CurrentArea = newArea;
                }
            }
            else if (e.Button == MouseButtons.Right)
            {
                if (area != null)
                {
                    Areas.Remove(area);
                    DeselectArea();
                }
                else if (CurrentArea != null && CurrentArea.Selected)
                {
                    DeselectArea();
                }
                else
                {
                    Crop.Close(false);
                }
            }
        }

        private void Crop_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (CurrentArea != null && !CurrentArea.Selected)
                {
                    IsMouseDown = false;
                    SelectArea();
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
                Rectangle rect = CurrentArea.Rectangle;
                rect.X += e.X - positionOnClick.X;
                rect.Y += e.Y - positionOnClick.Y;
                positionOnClick = e.Location;
                CurrentArea.Rectangle = rect;
                Resize.Update();
            }

            if (IsMouseDown && CurrentArea != null && !CurrentArea.Selected)
            {
                currentPosition = GetMousePosition();
                Rectangle rect = new Rectangle(positionOnClick.X, positionOnClick.Y, currentPosition.X - positionOnClick.X, currentPosition.Y - positionOnClick.Y);
                CurrentArea.Rectangle = CaptureHelpers.FixRectangle(rect);
            }
        }

        public Area IsAreaIntersect()
        {
            for (int i = Areas.Count - 1; i >= 0; i--)
            {
                if (Areas[i].Rectangle.Contains(GetMousePosition()))
                {
                    Crop.Cursor = Cursors.Hand;
                    return Areas[i];
                }
            }

            Crop.Cursor = Cursors.Cross;
            return null;
        }

        private void SelectArea()
        {
            if (CurrentArea != null)
            {
                CurrentArea.Selected = true;
                Resize.Update();
                Resize.Show();
            }
        }

        private void DeselectArea()
        {
            if (CurrentArea != null)
            {
                CurrentArea.Selected = false;
                Resize.Hide();
                CurrentArea = null;
            }
        }

        private Point GetMousePosition()
        {
            return Crop.PointToClient(Crop2.MousePosition);
        }

        public void Draw(Graphics g)
        {
            foreach (Area area in Areas)
            {
                area.Draw(g);
            }
        }

        public Region CombineAreas()
        {
            Region region = new Region();
            region.MakeEmpty();

            foreach (Area area in Areas)
            {
                region.Union(area.Region);
            }

            region.Intersect(new Region(Crop.Bounds));

            return region;
        }
    }
}
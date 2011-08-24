using System.Drawing;
using System.Linq;

namespace RegionCapture
{
    public class DragableRegion : Surface
    {
        protected DrawableObject areaObject;

        public DragableRegion(Image backgroundImage = null)
            : base(backgroundImage)
        {
            areaObject = new DrawableObject { Order = -10 };
            DrawableObjects.Add(areaObject);
        }

        protected override void Update()
        {
            areaObject.Rectangle = area;

            base.Update();

            if (areaObject.IsDragging && DrawableObjects.OfType<NodeObject>().All(x => !x.IsDragging && !x.IsMouseHover))
            {
                int x = mousePosition.X - oldMousePosition.X;
                int y = mousePosition.Y - oldMousePosition.Y;

                area.Offset(x, y);
            }
        }
    }
}
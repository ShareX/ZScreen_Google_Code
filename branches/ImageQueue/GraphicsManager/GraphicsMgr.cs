using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing.Drawing2D;
using System.Drawing;

namespace GraphicsManager
{
    public static class GraphicsMgr
    {
        public static Image ChangeImageSize(Image img, int width, int height)
        {
            Image bmp = new Bitmap(width, height);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.CompositingQuality = CompositingQuality.HighQuality;
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.DrawImage(img, new Rectangle(0, 0, bmp.Width, bmp.Height));
            }

            return bmp;
        }

        public static Image ChangeImageSize(Image img, Size size)
        {
            return ChangeImageSize(img, size.Width, size.Height);
        }

        public static Image ChangeImageSize(Image img, float percentage)
        {
            int width = (int)(percentage / 100 * img.Width);
            int height = (int)(percentage / 100 * img.Height);
            return ChangeImageSize(img, width, height);
        }

        public static Image CropImage(Image img, Rectangle rect)
        {
            Image bmp = new Bitmap(rect.Width, rect.Height);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.CompositingQuality = CompositingQuality.HighQuality;
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.DrawImage(img, new Rectangle(0, 0, rect.Width, rect.Height), rect, GraphicsUnit.Pixel);
            }

            return bmp;
        }

        public static Bitmap RotateImage(Image img, float theta)
        {
            Matrix matrix = new Matrix();
            matrix.Translate(img.Width / -2, img.Height / -2, MatrixOrder.Append);
            matrix.RotateAt(theta, new Point(0, 0), MatrixOrder.Append);
            using (GraphicsPath gp = new GraphicsPath())
            {
                gp.AddPolygon(new Point[] { new Point(0, 0), new Point(img.Width, 0), new Point(0, img.Height) });
                gp.Transform(matrix);
                PointF[] pts = gp.PathPoints;

                Rectangle bbox = BoundingBox(img, matrix);
                Bitmap bmpDest = new Bitmap(bbox.Width, bbox.Height);

                using (Graphics gDest = Graphics.FromImage(bmpDest))
                {
                    Matrix mDest = new Matrix();
                    mDest.Translate(bmpDest.Width / 2, bmpDest.Height / 2, MatrixOrder.Append);
                    gDest.Transform = mDest;
                    gDest.CompositingQuality = CompositingQuality.HighQuality;
                    gDest.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    gDest.DrawImage(img, pts);
                    return bmpDest;
                }
            }
        }

        private static Rectangle BoundingBox(Image img, Matrix matrix)
        {
            GraphicsUnit gu = new GraphicsUnit();
            Rectangle rImg = Rectangle.Round(img.GetBounds(ref gu));

            Point topLeft = new Point(rImg.Left, rImg.Top);
            Point topRight = new Point(rImg.Right, rImg.Top);
            Point bottomRight = new Point(rImg.Right, rImg.Bottom);
            Point bottomLeft = new Point(rImg.Left, rImg.Bottom);
            Point[] points = new Point[] { topLeft, topRight, bottomRight, bottomLeft };
            GraphicsPath gp = new GraphicsPath(points,
                new byte[] { (byte)PathPointType.Start, (byte)PathPointType.Line, (byte)PathPointType.Line, (byte)PathPointType.Line });
            gp.Transform(matrix);
            return Rectangle.Round(gp.GetBounds());
        }


    }
}
using System;
using System.Diagnostics;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using RegionCapture;
using ZUploaderPluginBase;
using ZUploaderScreenshotPlugin.Properties;

namespace ZUploaderScreenshotPlugin
{
    public class ScreenshotPlugin : ZUploaderPlugin
    {
        public override string Name
        {
            get { return "Screenshot capture"; }
        }

        public override Version Version
        {
            get { return new Version(1, 0); }
        }

        public override string Author
        {
            get { return "Jaex"; }
        }

        public override string Description
        {
            get { return "Fullscreen capture, Crop capture"; }
        }

        public override void Initialize()
        {
            ToolStripMenuItem tsmiFullscreen = new ToolStripMenuItem();
            tsmiFullscreen.Text = "Fullscreen";
            tsmiFullscreen.Image = Resources.Fullscreen;
            tsmiFullscreen.Click += new EventHandler(tsmiFullscreen_Click);

            ToolStripMenuItem tsmiRectangle = new ToolStripMenuItem();
            tsmiRectangle.Text = "Rectangle";
            tsmiRectangle.Image = Resources.Rectangle;
            tsmiRectangle.Click += new EventHandler(tsmiRectangle_Click);

            ToolStripMenuItem tsmiRoundedRectangle = new ToolStripMenuItem();
            tsmiRoundedRectangle.Text = "Rounded Rectangle";
            tsmiRoundedRectangle.Image = Resources.RoundedRectangle;
            tsmiRoundedRectangle.Click += new EventHandler(tsmiRoundedRectangle_Click);

            ToolStripMenuItem tsmiEllipse = new ToolStripMenuItem();
            tsmiEllipse.Text = "Ellipse";
            tsmiEllipse.Image = Resources.Ellipse;
            tsmiEllipse.Click += new EventHandler(tsmiEllipse_Click);

            ToolStripMenuItem tsmiTriangle = new ToolStripMenuItem();
            tsmiTriangle.Text = "Triangle";
            tsmiTriangle.Image = Resources.Triangle;
            tsmiTriangle.Click += new EventHandler(tsmiTriangle_Click);

            ToolStripMenuItem tsmiDiamond = new ToolStripMenuItem();
            tsmiDiamond.Text = "Diamond";
            tsmiDiamond.Image = Resources.Diamond;
            tsmiDiamond.Click += new EventHandler(tsmiDiamond_Click);

            ToolStripMenuItem tsmiPolygon = new ToolStripMenuItem();
            tsmiPolygon.Text = "Polygon";
            tsmiPolygon.Image = Resources.Polygon;
            tsmiPolygon.Click += new EventHandler(tsmiPolygon_Click);

            ToolStripMenuItem tsmiFreeHand = new ToolStripMenuItem();
            tsmiFreeHand.Text = "FreeHand";
            tsmiFreeHand.Image = Resources.FreeHand;
            tsmiFreeHand.Click += new EventHandler(tsmiFreeHand_Click);

            Host.AddPluginButton(tsmiFullscreen);
            Host.AddPluginButton(tsmiRectangle);
            Host.AddPluginButton(tsmiRoundedRectangle);
            Host.AddPluginButton(tsmiEllipse);
            Host.AddPluginButton(tsmiTriangle);
            Host.AddPluginButton(tsmiDiamond);
            Host.AddPluginButton(tsmiPolygon);
            Host.AddPluginButton(tsmiFreeHand);
        }

        private void BeforeCapture()
        {
            Host.Hide();
            Thread.Sleep(250);
        }

        private void AfterCapture(Image img = null)
        {
            Host.Show();

            if (img != null)
            {
                Host.UploadImage(img);
            }
        }

        private void CaptureRegion(Surface surface)
        {
            BeforeCapture();

            try
            {
                Image screenshot = Helpers.GetScreenshot();

                surface.LoadBackground(screenshot);

                if (surface.ShowDialog() == DialogResult.OK)
                {
                    Image img = surface.GetRegionImage();
                    AfterCapture(img);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }
            finally
            {
                AfterCapture();
            }
        }

        private void tsmiFullscreen_Click(object sender, EventArgs e)
        {
            BeforeCapture();

            try
            {
                Image screenshot = Helpers.GetScreenshot();
                AfterCapture(screenshot);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }
            finally
            {
                AfterCapture();
            }
        }

        private void tsmiRectangle_Click(object sender, EventArgs e)
        {
            CaptureRegion(new RectangleRegion());
        }

        private void tsmiRoundedRectangle_Click(object sender, EventArgs e)
        {
            CaptureRegion(new RoundedRectangleRegion());
        }

        private void tsmiEllipse_Click(object sender, EventArgs e)
        {
            CaptureRegion(new EllipseRegion());
        }

        private void tsmiTriangle_Click(object sender, EventArgs e)
        {
            CaptureRegion(new TriangleRegion());
        }

        private void tsmiDiamond_Click(object sender, EventArgs e)
        {
            CaptureRegion(new DiamondRegion());
        }

        private void tsmiPolygon_Click(object sender, EventArgs e)
        {
            CaptureRegion(new PolygonRegion());
        }

        private void tsmiFreeHand_Click(object sender, EventArgs e)
        {
            CaptureRegion(new FreeHandRegion());
        }
    }
}
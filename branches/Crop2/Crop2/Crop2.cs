using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using GraphicsMgrLib;

namespace Crop
{
    public class Crop2 : Form
    {
        public Image Screenshot { get; private set; }
        public AreaManager Manager { get; private set; }

        public Crop2(Image screenshot)
        {
            InitializeComponent();
            Screenshot = screenshot;
            Manager = new AreaManager(this);
            Timer drawTimer = new Timer();
            drawTimer.Interval = 10;
            drawTimer.Tick += new EventHandler(drawTimer_Tick);
            drawTimer.Start();
        }

        protected override void Dispose(bool disposing)
        {
            if (Screenshot != null) Screenshot.Dispose();

            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            this.Bounds = GraphicsMgr.GetScreenBounds();
            this.CausesValidation = false;
            this.ControlBox = true;
            this.Cursor = Cursors.Cross;
            this.DoubleBuffered = true;
            this.FormBorderStyle = FormBorderStyle.None;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Crop";
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint, true);
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            //this.TopMost = true;
            this.ResumeLayout(false);

            this.KeyDown += new KeyEventHandler(Crop2_KeyDown);
        }

        private void Crop2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                Close(false);
            }
            else if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Space)
            {
                Close(true);
            }
        }

        public void Close(bool result)
        {
            if (result)
            {
                DialogResult = DialogResult.OK;
            }
            else
            {
                DialogResult = DialogResult.Abort;
            }

            this.Close();
        }

        private void drawTimer_Tick(object sender, EventArgs e)
        {
            this.Refresh();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.HighSpeed;
            DrawScreenshot(g);
            Manager.Draw(g);
        }

        public Image GetCroppedScreenshot()
        {
            using (Graphics g = Graphics.FromImage(Screenshot))
            {
                Region region = Manager.CombineAreas();

                if (!region.IsEmpty(g))
                {
                    RectangleF rect = region.GetBounds(g);
                    Bitmap bmp = new Bitmap((int)rect.Width, (int)rect.Height);

                    using (Graphics g2 = Graphics.FromImage(bmp))
                    {
                        g2.Clear(Color.Transparent);

                        using (Matrix translateMatrix = new Matrix())
                        {
                            translateMatrix.Translate(-rect.X, -rect.Y);
                            region.Transform(translateMatrix);
                        }

                        g2.IntersectClip(region);

                        g2.CompositingQuality = CompositingQuality.HighQuality;
                        g2.InterpolationMode = InterpolationMode.HighQualityBicubic;
                        g2.DrawImage(Screenshot, new Rectangle(0, 0, bmp.Width, bmp.Height), rect, GraphicsUnit.Pixel);

                        return bmp;
                    }
                }
            }

            return null;
        }

        private void DrawScreenshot(Graphics g)
        {
            g.DrawImage(Screenshot, 0, 0, Screenshot.Width, Screenshot.Height);
        }
    }
}
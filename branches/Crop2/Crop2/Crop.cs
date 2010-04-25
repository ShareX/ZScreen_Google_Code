using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using GraphicsMgrLib;

namespace Crop
{
    public class Crop : Form
    {
        public Image Screenshot { get; private set; }
        public RegionManager CropRegion { get; private set; }

        public Crop()
        {
            InitializeComponent();
            Screenshot = ZScreenLib.Capture.CaptureScreen(true);
            CropRegion = new RegionManager(this);
            Timer drawTimer = new Timer();
            drawTimer.Interval = 10;
            drawTimer.Tick += new EventHandler(drawTimer_Tick);
            drawTimer.Start();
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            this.Bounds = GraphicsMgr.GetScreenBounds();
            this.CausesValidation = false;
            this.ControlBox = true;
            this.Cursor = Cursors.Default;
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
            CropRegion.Draw(g);
        }

        private void DrawScreenshot(Graphics g)
        {
            g.DrawImage(Screenshot, 0, 0, Screenshot.Width, Screenshot.Height);
        }
    }
}
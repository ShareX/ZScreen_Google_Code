using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using GraphicsMgrLib;

namespace ZScreenLib
{
    public class CropLight : Form
    {
        private Timer timer;
        private TextureBrush backgroundBrush;
        private Pen rectanglePen;
        private Point positionOnClick, positionCurrent, positionOld;
        private bool isMouseDown;

        public Rectangle SelectionRectangle { get; private set; }

        public CropLight(Image backgroundImage)
        {
            InitializeComponent();

            backgroundBrush = new TextureBrush(backgroundImage);
            rectanglePen = new Pen(Color.Red);

            timer = new Timer { Interval = 10 };
            timer.Tick += new EventHandler(timer_Tick);
            timer.Start();
        }

        private void SimpleCrop_Shown(object sender, EventArgs e)
        {
            BringToFront();
            Activate();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            if (isMouseDown)
            {
                positionOld = positionCurrent;
                positionCurrent = PointToClient(Cursor.Position);

                if (positionCurrent != positionOld)
                {
                    SelectionRectangle = GraphicsMgr.FixRectangle(positionOnClick.X, positionOnClick.Y,
                        positionCurrent.X - positionOnClick.X + 1, positionCurrent.Y - positionOnClick.Y + 1);
                    Refresh();
                }
            }
        }

        private void Crop_KeyDown(object sender, KeyEventArgs e)
        {
            Close();
        }

        private void Crop_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                positionOnClick = e.Location;
                isMouseDown = true;
            }
            else
            {
                if (isMouseDown)
                {
                    isMouseDown = false;
                    Refresh();
                }
                else
                {
                    Close();
                }
            }
        }

        private void Crop_MouseUp(object sender, MouseEventArgs e)
        {
            if (isMouseDown && e.Button == MouseButtons.Left)
            {
                if (SelectionRectangle.Width > 0 && SelectionRectangle.Height > 0)
                {
                    DialogResult = DialogResult.OK;
                }

                Close();
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.InterpolationMode = InterpolationMode.NearestNeighbor;
            g.SmoothingMode = SmoothingMode.HighSpeed;
            g.FillRectangle(backgroundBrush, Bounds);

            if (isMouseDown)
            {
                g.DrawRectangle(rectanglePen, SelectionRectangle.X, SelectionRectangle.Y, SelectionRectangle.Width - 1, SelectionRectangle.Height - 1);
            }
        }

        #region Windows Form Designer generated code

        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }

            if (backgroundBrush != null) backgroundBrush.Dispose();
            if (rectanglePen != null) rectanglePen.Dispose();

            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            this.Name = "Crop";
            this.Text = "Crop";
            this.FormBorderStyle = FormBorderStyle.None;
            this.Bounds = GraphicsMgr.GetScreenBounds();
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint, true);
            this.Cursor = Cursors.Cross;
            this.TopMost = true;
            this.KeyDown += new KeyEventHandler(this.Crop_KeyDown);
            this.MouseDown += new MouseEventHandler(this.Crop_MouseDown);
            this.MouseUp += new MouseEventHandler(this.Crop_MouseUp);
            this.Shown += new EventHandler(SimpleCrop_Shown);
            this.ResumeLayout(false);
        }

        #endregion Windows Form Designer generated code
    }
}
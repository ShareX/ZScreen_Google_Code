using System;
using System.Drawing;
using System.Windows.Forms;

namespace ZSS.Forms
{
    public partial class ShowScreenshot : Form
    {
        public ShowScreenshot()
        {
            this.BackColor = Color.Black;
            this.Cursor = Cursors.Hand;
            this.DoubleBuffered = true;
            this.FormBorderStyle = FormBorderStyle.None;
            InitializeComponent();
            this.Bounds = GraphicsMgr.GetScreenBounds();
        }

        private void ShowScreenshot_Load(object sender, EventArgs e)
        {
            if ((this.Bounds.Width > this.BackgroundImage.Width) && (this.Bounds.Height > this.BackgroundImage.Height))
            {
                this.BackgroundImageLayout = ImageLayout.Center;
            }
            else
            {
                this.BackgroundImageLayout = ImageLayout.Zoom;
            }
        }

        private void ShowScreenshot_MouseDown(object sender, MouseEventArgs e)
        {
            this.Close();
        }

        private void ShowScreenshot_Shown(object sender, EventArgs e)
        {
            User32.SetForegroundWindow(this.Handle);
            User32.SetActiveWindow(this.Handle);
        }

        private void ShowScreenshot_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }
    }
}
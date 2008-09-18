#region License Information (GPL v2)
/*
    ZScreen - A program that allows you to upload screenshots in one keystroke.
    Copyright (C) 2008  Brandon Zimmerman

    This program is free software; you can redistribute it and/or
    modify it under the terms of the GNU General Public License
    as published by the Free Software Foundation; either version 2
    of the License, or (at your option) any later version.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with this program; if not, write to the Free Software
    Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301, USA.
    
    Optionally you can also view the license at <http://www.gnu.org/licenses/>.
*/
#endregion

using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace ZSS
{
    partial class Crop : Form
    {
        //new
        [DllImport("user32.dll")]
        static extern System.IntPtr WindowFromPoint(int xPoint, int yPoint);

        [DllImport("GDI32.dll")]
        private static extern int GetDeviceCaps(int hdc, int nIndex);

        [DllImport("user32.dll")]
        private static extern bool GetWindowRect(
            System.IntPtr hWnd,
            out Rect windowRect);

        [StructLayout(LayoutKind.Sequential)]
        private struct Rect
        {
            public int left;
            public int top;
            public int right;
            public int bottom;
        }


        private bool mMouseDown = false;
        private Image mBgImage;

        // Keep track of the location of the mouse
        private Point mMouseLocation, mMouseLocationOnClick;

        private Rectangle mToCrop;

        private Rect mRect = new Rect();

        private Pen mPen = new Pen(Color.Red, 2f);

        private Graphics mGraphics;

        public Crop(Image img)
        {
            InitializeComponent();

            SetupCropper();

            mBgImage = img;
            mToCrop.X = -1;
        }

        /// <summary>
        /// Set up the main form as a full screen cropper.
        /// </summary>
        private void SetupCropper()
        {
            mGraphics = this.CreateGraphics();

            // Use double buffering to improve drawing performance
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint, true);
            //Bounds = Screen.PrimaryScreen.Bounds;
            WindowState = FormWindowState.Maximized;
            ShowInTaskbar = false;
            DoubleBuffered = true;
            
        }

        private void Crop_MouseMove(object sender, MouseEventArgs e)
        {
            mMouseLocation = MousePosition;
            
            if (mMouseDown)
            {
                if (mMouseLocation.X < mMouseLocationOnClick.X && mMouseLocation.Y < mMouseLocationOnClick.Y)
                {
                    cancelAndRestart();
                    return;
                }

                if (mMouseLocation.X < mMouseLocationOnClick.X)
                    mMouseLocation.X = mMouseLocationOnClick.X;
                if (mMouseLocation.Y < mMouseLocationOnClick.Y)
                    mMouseLocation.Y = mMouseLocationOnClick.Y;



                mToCrop = new Rectangle(mMouseLocationOnClick.X, mMouseLocationOnClick.Y, mMouseLocation.X - mMouseLocationOnClick.X, mMouseLocation.Y - mMouseLocationOnClick.Y);
                Refresh();
            }

        }

        private void Crop_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Clicks == 1)
            {
                if (e.Button == MouseButtons.Left)
                {
                    Refresh();
                }
                
            }
        }

        private void Crop_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && e.Clicks == 1)
            {
                mMouseLocationOnClick = MousePosition;
                mMouseDown = true;
            }
            if (e.Button == MouseButtons.Right)
            {
                if (mMouseDown)
                {
                    cancelAndRestart();
                }
                else
                {
                    returnNullAndExit();
                }
            }
        }

        private void Crop_MouseUp(object sender, MouseEventArgs e)
        {
            mMouseDown = false;

            //use rectangle and crop, save, and exit

            if (mToCrop != null && mToCrop.X >= 0 && mToCrop.Width > 0 && mToCrop.Height > 0)
            {
                returnImageAndExit();
            }
            else
            {
                Refresh();
            } 
        }

        private void Crop_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (mMouseDown == false)
            {
                if (e.KeyChar == (int)Keys.Space)
                {
                    mToCrop = new Rectangle(0, 0, Size.Width, Size.Height);
                    returnImageAndExit();
                }
                if (e.KeyChar == (int)Keys.Escape)
                {
                    returnNullAndExit();
                }
            }
            if (mMouseDown == true && (e.KeyChar == (int)Keys.Escape || e.KeyChar == (int)Keys.Space))
            {
                cancelAndRestart();
                drawWindow();
            }
        }

        //new
        private void drawWindow()
        {
            Refresh();

            //Graphics g = this.CreateGraphics();


            IntPtr dow = WindowFromPoint(mMouseLocation.X, mMouseLocation.Y);
            
            GetWindowRect(dow, out mRect);

            mToCrop = new Rectangle(mRect.left, mRect.top, mRect.right - mRect.left, mRect.bottom - mRect.top);
            mGraphics.SmoothingMode = SmoothingMode.HighSpeed;
            mGraphics.DrawRectangle(mPen, mToCrop);
        }

        private void returnImageAndExit()
        {
            Program.returnedCroppedImage = cropImage(mBgImage, mToCrop);

            mBgImage.Dispose();

            Close();
        }

        private void cancelAndRestart()
        {
            Refresh();
            mMouseDown = false;
            mToCrop.X = -1;
        }

        private void returnNullAndExit()
        {
            mBgImage.Dispose();

            Program.returnedCroppedImage = null;
            
            //fixes right click menus from displaying in external programs after close
            System.Threading.Thread.Sleep(150);

            Close();
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            // Draw the current background image stretched to fill the full screen

            e.Graphics.DrawImage(mBgImage, 0, 0, Size.Width, Size.Height);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (mMouseDown)
            {
                e.Graphics.SmoothingMode = SmoothingMode.HighSpeed;
                e.Graphics.DrawRectangle(mPen, mToCrop);
            }
        }

        public Image cropImage(Image img, Rectangle rect)
        {
            Image Cropped = new Bitmap(rect.Width, rect.Height);
            Graphics e = Graphics.FromImage(Cropped);
            e.CompositingQuality = CompositingQuality.HighQuality;
            e.SmoothingMode = SmoothingMode.HighQuality;
            e.InterpolationMode = InterpolationMode.HighQualityBicubic;
            e.DrawImage(img, new Rectangle(0, 0, rect.Width, rect.Height), rect, GraphicsUnit.Pixel);
            return Cropped;
        }
    }
}

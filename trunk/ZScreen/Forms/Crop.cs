#region License Information (GPL v2)
/*
    ZScreen - A program that allows you to upload screenshots in one keystroke.
    Copyright (C) 2008-2009  Brandon Zimmerman

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
using System.Diagnostics;

namespace ZSS
{
    partial class Crop : Form
    {
        private bool mMouseDown = false;
        private Image mBgImage;
        private Point mousePos, mousePosOnClick, oldMousePos;
        private Rectangle mToCrop;
        private Pen mPen = new Pen(XMLSettings.DeserializeColor(Program.conf.CropBorderColor), Program.conf.CropBorderSize);
        private Graphics mGraphics;
        private Bitmap bmpBgImage;
        private Pen labelBorderPen = new Pen(Color.Black);
        private Pen crosshairPen = new Pen(Color.Red);
        private Pen crosshairPen2 = new Pen(Color.FromArgb(150, Color.Gray));
        private string strMouseUp = "Mouse Left Down: Create crop region\nMouse Right Down & Escape: Cancel Screenshot\nSpace: Capture Entire Screen";
        private string strMouseDown = "Mouse Left Up: Capture Screenshot\nMouse Right Down & Escape & Space: Cancel crop region";
        private Timer timer = new Timer();

        public Crop(Image img)
        {
            mBgImage = new Bitmap(img);
            bmpBgImage = new Bitmap(mBgImage);
            InitializeComponent();
            this.Bounds = MyGraphics.GetScreenBounds();
            mGraphics = this.CreateGraphics();
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint, true);
            ShowInTaskbar = false;
            DoubleBuffered = true;
            Cursor.Hide();
            mousePos = MousePosition;
            timer.Interval = 10;
            timer.Tick += new EventHandler(timer_Tick);
        }

        private void Crop_Shown(object sender, EventArgs e)
        {
            System.Threading.Thread.Sleep(100);
            User32.SetForegroundWindow(this.Handle.ToInt32());
            User32.SetActiveWindow(this.Handle.ToInt32());
            timer.Start();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            mousePos = MousePosition;
            if (oldMousePos == null || oldMousePos != mousePos)
            {
                oldMousePos = mousePos;
                if (mMouseDown)
                {
                    mToCrop = MyGraphics.GetRectangle(mousePos.X + this.Left, mousePos.Y + this.Top,
                        mousePosOnClick.X - mousePos.X, mousePosOnClick.Y - mousePos.Y);
                    //if ((mousePos.X < mousePosOnClick.X) && (mousePos.Y < mousePosOnClick.Y))
                    //    mToCrop = new Rectangle(mousePos.X, mousePos.Y, mousePosOnClick.X - mousePos.X, mousePosOnClick.Y - mousePos.Y);
                    //else if ((mousePos.X > mousePosOnClick.X) && (mousePos.Y < mousePosOnClick.Y))
                    //    mToCrop = new Rectangle(mousePosOnClick.X, mousePos.Y, mousePos.X - mousePosOnClick.X, mousePosOnClick.Y - mousePos.Y);
                    //else if ((mousePos.X > mousePosOnClick.X) && (mousePos.Y > mousePosOnClick.Y))
                    //    mToCrop = new Rectangle(mousePosOnClick.X, mousePosOnClick.Y, mousePos.X - mousePosOnClick.X, mousePos.Y - mousePosOnClick.Y);
                    //else if ((mousePos.X < mousePosOnClick.X) && (mousePos.Y > mousePosOnClick.Y))
                    //    mToCrop = new Rectangle(mousePos.X, mousePosOnClick.Y, mousePosOnClick.X - mousePos.X, mousePos.Y - mousePosOnClick.Y);
                }
                Refresh();
            }
        }

        private void ShowFormSize(string methodName)
        {
            Console.WriteLine(string.Format("{2} (Form Size): {0}x{1}", this.Size.Width, this.Size.Height, methodName));
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.HighSpeed;
            e.Graphics.DrawImage(mBgImage, 0, 0, mBgImage.Width, mBgImage.Height);
            if (Program.conf.CropStyle == 2)
                e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(75, Color.White)), new Rectangle(0, 0, mBgImage.Width, mBgImage.Height));
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.HighSpeed;

            g.DrawLine(crosshairPen2, new Point(0, mousePos.Y), new Point(mBgImage.Width, mousePos.Y));
            g.DrawLine(crosshairPen2, new Point(mousePos.X, 0), new Point(mousePos.X, mBgImage.Height));
            if (mMouseDown)
            {
                if (Program.conf.CropStyle == 1)
                    g.FillRectangle(new SolidBrush(Color.FromArgb(75, Color.White)), mToCrop);
                if (Program.conf.CropStyle == 2)
                    if (mToCrop.Width > 0 && mToCrop.Height > 0) g.DrawImage(bmpBgImage.Clone(mToCrop, bmpBgImage.PixelFormat), mToCrop);
                DrawInstructor(strMouseDown, g);
                if (Program.conf.CropBorderSize > 0.9f)
                    g.DrawRectangle(mPen, mToCrop);
                if (Program.conf.RegionRectangleInfo)
                {
                    Font posFont = new Font(FontFamily.GenericSansSerif, 8);
                    string posText = "X: " + mToCrop.X + " px, Y: " + mToCrop.Y + " px\nWidth: " + mToCrop.Width + " px, Height: " + mToCrop.Height + " px";
                    Size textSize = TextRenderer.MeasureText(posText, posFont);
                    Rectangle labelRect = new Rectangle(mToCrop.Left + 5, mToCrop.Bottom - textSize.Height - 15, textSize.Width + 10, textSize.Height + 10);
                    GraphicsPath gPath = MyGraphics.RoundedRectangle(labelRect, 7);
                    g.FillPath(new LinearGradientBrush(new Point(labelRect.X, labelRect.Y), new Point(labelRect.X + labelRect.Width, labelRect.Y),
                        Color.Black, Color.FromArgb(150, Color.Black)), gPath);
                    g.DrawPath(labelBorderPen, gPath);
                    g.DrawString(posText, posFont, new SolidBrush(Color.White), mToCrop.Left + 10, mToCrop.Bottom - textSize.Height - 10);
                }
            }
            else
            {
                DrawInstructor(strMouseUp, g);
                if (Program.conf.RegionRectangleInfo)
                {
                    Font posFont = new Font(FontFamily.GenericSansSerif, 8);
                    string posText = "X: " + mousePos.X + " px, Y: " + mousePos.Y + " px";
                    Rectangle labelRect = new Rectangle(mousePos.X + 15, mousePos.Y + 15,
                        TextRenderer.MeasureText(posText, posFont).Width + 10, TextRenderer.MeasureText(posText, posFont).Height + 10);
                    GraphicsPath gPath = MyGraphics.RoundedRectangle(labelRect, 7);
                    g.FillPath(new LinearGradientBrush(new Point(labelRect.X, labelRect.Y), new Point(labelRect.X + labelRect.Width, labelRect.Y),
                    Color.Black, Color.FromArgb(150, Color.Black)), gPath);
                    g.DrawPath(labelBorderPen, gPath);
                    g.DrawString(posText, posFont, new SolidBrush(Color.White), labelRect.X + 5, labelRect.Y + 5);
                }
            }
            g.DrawLine(crosshairPen, new Point(mousePos.X - 10, mousePos.Y), new Point(mousePos.X + 10, mousePos.Y));
            g.DrawLine(crosshairPen, new Point(mousePos.X, mousePos.Y - 10), new Point(mousePos.X, mousePos.Y + 10));
        }

        private void DrawInstructor(string drawText, Graphics g)
        {
            if (Program.conf.RegionHotkeyInfo)
            {
                Font posFont = new Font(FontFamily.GenericSansSerif, 8);
                Size textSize = TextRenderer.MeasureText(drawText, posFont);
                Rectangle labelRect = new Rectangle((this.Width / 2) - ((textSize.Width + 10) / 2), 30, textSize.Width + 30, textSize.Height + 10);
                GraphicsPath gPath = MyGraphics.RoundedRectangle(labelRect, 7);
                g.FillPath(new LinearGradientBrush(new Point(labelRect.X, labelRect.Y), new Point(labelRect.X + labelRect.Width, labelRect.Y),
                Color.White, Color.FromArgb(150, Color.White)), gPath);
                g.DrawPath(labelBorderPen, gPath);
                g.DrawString(drawText, posFont, new SolidBrush(Color.Black), labelRect.X + 5, labelRect.Y + 5);
            }
        }

        private void Crop_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                mousePosOnClick = MousePosition;
                mToCrop = new Rectangle(mousePosOnClick, new Size(0, 0));
                mMouseDown = true;
                Refresh();
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
            if (mMouseDown)
            {
                mMouseDown = false;
                if (mToCrop != null && mToCrop.X >= 0 && mToCrop.Width > 0 && mToCrop.Height > 0)
                {
                    returnImageAndExit();
                }
                else
                {
                    Refresh();
                }
            }
        }

        private void Crop_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (mMouseDown == false)
            {
                if (e.KeyChar == (int)Keys.Space)
                {
                    mToCrop = new Rectangle(0, 0, mBgImage.Width, mBgImage.Height);
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
            }
        }

        private void cancelAndRestart()
        {
            mMouseDown = false;
            Refresh();
        }

        private void returnImageAndExit()
        {
            Program.mLastRegion = mToCrop;
            this.DialogResult = DialogResult.OK;
            Close();
        }

        private void returnNullAndExit()
        {
            //fixes right click menus from displaying in external programs after close
            System.Threading.Thread.Sleep(150);
            this.DialogResult = DialogResult.Cancel;
            Close();
        }

        private void Crop_FormClosed(object sender, FormClosedEventArgs e)
        {
            DisposeImages();
            Cursor.Show();
        }

        private void DisposeImages()
        {
            mBgImage.Dispose();
            bmpBgImage.Dispose();
        }
    }
}
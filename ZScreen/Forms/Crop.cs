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
using System.Collections;
using System.Collections.Generic;

/*
 * Update: 20080401 (Isaac) Fixing multiple screen handling
 */
namespace ZSS
{
    partial class Crop : Form
    {
        private bool mMouseDown = false;
        private Image mBgImage;
        private Point mousePos, mousePosOnClick, oldMousePos;
        private Rectangle cropRegion;
        private Rectangle rectRegion;

        private Rectangle CropRegion
        {
            get
            {
                return cropRegion;
            }
            set
            {
                cropRegion = value;
                rectRegion.Location = cropRegion.Location;
                rectRegion.Size = new Size(cropRegion.Width + 1, cropRegion.Height + 1);
            }
        }

        private IntPtr mHandle;
        private Pen CropPen = new Pen(XMLSettings.DeserializeColor(Program.conf.CropBorderColor),
            (Single)Program.conf.CropBorderSize);
        private Pen SelectedWindowPen = new Pen(XMLSettings.DeserializeColor(Program.conf.SelectedWindowBorderColor),
            (Single)Program.conf.SelectedWindowBorderSize);
        private Graphics mGraphics;
        private Bitmap bmpBgImage;
        private Pen labelBorderPen = new Pen(Color.Black);
        private Pen crosshairPen = new Pen(XMLSettings.DeserializeColor(Program.conf.CropCrosshairColor));
        private Pen crosshairPen2 = new Pen(Color.FromArgb(150, Color.Gray));
        private string strMouseUp = "Mouse Left Down: Create crop region\nMouse Right Down & Escape: Cancel Screenshot\nSpace: Capture Entire Screen\nTab: Toggle Crop Grid mode";
        private string strMouseDown = "Mouse Left Up: Capture Screenshot\nMouse Right Down & Escape & Space: Cancel crop region\nTab: Toggle Crop Grid mode";
        private Queue windows = new Queue();
        private Timer timer = new Timer();
        private Timer windowCheck = new Timer();
        private CropOptions Options { get; set; }
        private bool forceCheck = false;
        private Rectangle rectIntersect;
        private DynamicCrosshair crosshair;

        public Crop(CropOptions options)
        {
            this.Options = options;
            mBgImage = new Bitmap(this.Options.MyImage);
            bmpBgImage = new Bitmap(mBgImage);
            InitializeComponent();
            this.Bounds = MyGraphics.GetScreenBounds();
            mGraphics = this.CreateGraphics();
            //This should not be used anymore since we will normalize points to client's coordinate
            //rectIntersect.Location = this.Bounds.Location;
            rectIntersect.Size = new Size(this.Bounds.Width - 1, this.Bounds.Height - 1);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint, true);
            mousePos = this.PointToClient(MousePosition);
            timer.Interval = 10;
            timer.Tick += new EventHandler(timer_Tick);
            windowCheck.Interval = 250;
            windowCheck.Tick += new EventHandler(windowCheck_Tick);
            crosshair = new DynamicCrosshair();

            if (this.Options.SelectedWindowMode)
            {
                User32.EnumWindowsProc ewp = new User32.EnumWindowsProc(EvalWindow);
                User32.EnumWindows(ewp, 0);
            }
            else
            {
                Cursor.Hide();
            }
        }

        private void Crop_Shown(object sender, EventArgs e)
        {
            this.TopMost = true;
            timer.Start();
            windowCheck.Start();
        }

        private void windowCheck_Tick(object sender, EventArgs e)
        {
            if (User32.GetForegroundWindow() != this.Handle)
            {
                User32.ActivateWindow(this.Handle);
            }
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            mousePos = this.PointToClient(MousePosition);
            if (Program.conf.CropDynamicCrosshair) forceCheck = true;
            if (oldMousePos == null || oldMousePos != mousePos || forceCheck)
            {
                oldMousePos = mousePos;
                forceCheck = false;
                if (this.Options.SelectedWindowMode)
                {
                    IEnumerator enumerator = windows.GetEnumerator();
                    while (enumerator.MoveNext())
                    {
                        KeyValuePair<IntPtr, Rectangle> kv = (KeyValuePair<IntPtr, Rectangle>)enumerator.Current;
                        if (kv.Value.Contains(Cursor.Position))
                        {
                            mHandle = kv.Key;
                            CropRegion = new Rectangle(this.PointToClient(kv.Value.Location), kv.Value.Size);

                            break;
                        }
                    }
                }
                else
                {
                    if (mMouseDown)
                    {
                        CropRegion = MyGraphics.GetRectangle(mousePos.X, mousePos.Y,
                            mousePosOnClick.X - mousePos.X, mousePosOnClick.Y - mousePos.Y, Program.conf.CropGridSize,
                            Program.conf.CropGridToggle, ref mousePos);
                        /*
                                                CropRegion = MyGraphics.GetRectangle(mousePos.X + this.Left, mousePos.Y + this.Top,
                                                    mousePosOnClick.X - mousePos.X, mousePosOnClick.Y - mousePos.Y, Program.conf.CropGridSize,
                                                    Program.conf.CropGridToggle, ref mousePos);
                        */
                        CropRegion = Rectangle.Intersect(CropRegion, rectIntersect);
                    }
                }
                Refresh();
            }
        }

        private bool EvalWindow(IntPtr hWnd, int lParam)
        {
            if (!User32.IsWindowVisible(hWnd)) return true;
            if (this.Handle == hWnd) return false;

            Rectangle rect = User32.GetWindowRectangle(hWnd);
            rect.Intersect(this.Bounds);
            windows.Enqueue(new KeyValuePair<IntPtr, Rectangle>(hWnd, rect));

            return true;
        }

        private void ShowFormSize(string methodName)
        {
            Console.WriteLine(string.Format("{2} (Form Size): {0}x{1}", this.Size.Width, this.Size.Height, methodName));
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.HighSpeed;
            e.Graphics.DrawImage(mBgImage, 0, 0, mBgImage.Width, mBgImage.Height);
            if ((this.Options.SelectedWindowMode && Program.conf.SelectedWindowRegionStyle == 2) || (!this.Options.SelectedWindowMode && Program.conf.CropRegionStyle == 2))
                e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(100, Color.White)), new Rectangle(0, 0, mBgImage.Width, mBgImage.Height));
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.HighSpeed;

            if ((this.Options.SelectedWindowMode && Program.conf.SelectedWindowRegionStyle == 1) ||
                (!this.Options.SelectedWindowMode && Program.conf.CropRegionStyle == 1 && mMouseDown))
            {
                g.FillRectangle(new SolidBrush(Color.FromArgb(75, Color.White)), CropRegion);
            }
            else if (((this.Options.SelectedWindowMode && Program.conf.SelectedWindowRegionStyle == 2) ||
                (!this.Options.SelectedWindowMode && Program.conf.CropRegionStyle == 2 && mMouseDown)) &&
                CropRegion.Width > 0 && CropRegion.Height > 0)
            {
                g.DrawImage(bmpBgImage, CropRegion, CropRegion, GraphicsUnit.Pixel);
            }

            if (this.Options.SelectedWindowMode)
            {
                if (Program.conf.SelectedWindowBorderSize != 0)
                {
                    g.DrawRectangle(SelectedWindowPen, CropRegion);
                }
                if (Program.conf.SelectedWindowRectangleInfo)
                {
                    DrawTooltip("X: " + CropRegion.X + " px, Y: " + CropRegion.Y + " px\nWidth: " + CropRegion.Width +
                        " px, Height: " + CropRegion.Height + " px", new Point(15, 15), g);
                }
            }
            else
            {
                if (Program.conf.CropShowBigCross)
                {
                    g.DrawLine(crosshairPen2, new Point(0, mousePos.Y), new Point(mBgImage.Width, mousePos.Y));
                    g.DrawLine(crosshairPen2, new Point(mousePos.X, 0), new Point(mousePos.X, mBgImage.Height));
                }
                if (mMouseDown)
                {
                    if (Program.conf.CropShowGrids && Program.conf.CropGridToggle)
                    {
                        DrawGrids(g);
                    }
                    DrawInstructor(strMouseDown, g);
                    if (Program.conf.CropBorderSize != 0)
                    {
                        g.DrawRectangle(CropPen, CropRegion);
                    }
                    if (Program.conf.CropRegionRectangleInfo)
                    {
                        DrawTooltip("X: " + CropRegion.X + " px, Y: " + CropRegion.Y + " px\nWidth: " +
                            rectRegion.Width + " px, Height: " + rectRegion.Height + " px", new Point(15, 15), g);
                    }
                    g.DrawLine(crosshairPen, new Point(mousePosOnClick.X - 10, mousePosOnClick.Y), new Point(mousePosOnClick.X + 10, mousePosOnClick.Y));
                    g.DrawLine(crosshairPen, new Point(mousePosOnClick.X, mousePosOnClick.Y - 10), new Point(mousePosOnClick.X, mousePosOnClick.Y + 10));
                }
                else
                {
                    DrawInstructor(strMouseUp, g);
                    if (Program.conf.CropRegionRectangleInfo)
                    {
                        Point p = this.PointToClient(mousePos);
                        DrawTooltip("X: " + mousePos.X + " px, Y: " + mousePos.Y + " px X: " + p.X + " px, Y: " + p.Y + " px", new Point(15, 15), g);
                    }
                }
                crosshair.Draw(g, mousePos);
            }
        }

        private void DrawTooltip(string text, Point offset, Graphics g)
        {
            Font font = new Font(FontFamily.GenericSansSerif, 8);
            Point mPos = this.PointToClient(MousePosition);
            Rectangle labelRect = new Rectangle(new Point(mPos.X + offset.X, mPos.Y + offset.Y),
                new Size(TextRenderer.MeasureText(text, font).Width + 10, TextRenderer.MeasureText(text, font).Height + 10));
            if (labelRect.Right > this.Bounds.Right - 5) labelRect.X = mPos.X - offset.X - labelRect.Width;
            if (labelRect.Bottom > this.Bounds.Bottom - 5) labelRect.Y = mPos.Y - offset.Y - labelRect.Height;
            GraphicsPath gPath = MyGraphics.RoundedRectangle(labelRect, 7);
            g.FillPath(new LinearGradientBrush(new Point(labelRect.X, labelRect.Y), new Point(labelRect.X + labelRect.Width, labelRect.Y),
            Color.Black, Color.FromArgb(150, Color.Black)), gPath);
            g.DrawPath(labelBorderPen, gPath);
            g.DrawString(text, font, new SolidBrush(Color.White), labelRect.X + 5, labelRect.Y + 5);
        }

        private void DrawGrids(Graphics g)
        {
            if (Program.conf.CropGridSize.Width >= 10)
            {
                for (int x = 0; x <= (CropRegion.Width / Program.conf.CropGridSize.Width); x++)
                {
                    g.DrawLine(crosshairPen2,
                        new Point(CropRegion.X + (Program.conf.CropGridSize.Width * x), CropRegion.Y),
                        new Point(CropRegion.X + (Program.conf.CropGridSize.Width * x), CropRegion.Y + CropRegion.Height));
                }
            }
            if (Program.conf.CropGridSize.Height >= 10)
            {
                for (int y = 0; y <= (CropRegion.Height / Program.conf.CropGridSize.Height); y++)
                {
                    g.DrawLine(crosshairPen2,
                        new Point(CropRegion.X, CropRegion.Y + (Program.conf.CropGridSize.Height * y)),
                        new Point(CropRegion.X + CropRegion.Width, CropRegion.Y + (Program.conf.CropGridSize.Height * y)));
                }
            }
        }

        private void DrawInstructor(string drawText, Graphics g)
        {
            if (Program.conf.CropRegionHotkeyInfo)
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
                if (this.Options.SelectedWindowMode)
                {
                    //if (Program.conf.SelectedWindowFront)
                    //{
                    //    User32.ActivateWindow(mHandle);
                    //}
                    returnImageAndExit();
                }
                else
                {
                    mousePosOnClick = this.PointToClient(MousePosition);
                    CropRegion = new Rectangle(mousePosOnClick, new Size(0, 0));
                    mMouseDown = true;
                    Refresh();
                }
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
            if (!this.Options.SelectedWindowMode && mMouseDown)
            {
                mMouseDown = false;
                if (CropRegion != null && CropRegion.Width > 0 && CropRegion.Height > 0)
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
                    CropRegion = new Rectangle(0, 0, mBgImage.Width, mBgImage.Height);
                    returnImageAndExit();
                }
                if (e.KeyChar == (int)Keys.Escape)
                {
                    returnNullAndExit();
                }
            }
            if (mMouseDown && (e.KeyChar == (int)Keys.Escape || e.KeyChar == (int)Keys.Space))
            {
                cancelAndRestart();
            }
            if (e.KeyChar == (int)Keys.Tab && !this.Options.SelectedWindowMode)
            {
                Program.conf.CropGridToggle = !Program.conf.CropGridToggle;
                Program.conf.Save();
                forceCheck = true;
            }
        }

        private void cancelAndRestart()
        {
            mMouseDown = false;
            Refresh();
        }

        private void returnImageAndExit()
        {
            if (this.Options.SelectedWindowMode)
            {
                Program.LastCapture = CropRegion;
            }
            else
            {
                Program.LastRegion = rectRegion;
            }
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

        private void Crop_FormClosing(object sender, FormClosingEventArgs e)
        {
            timer.Stop();
            windowCheck.Stop();
            if (!this.Options.SelectedWindowMode) Cursor.Show();
        }

        private void Crop_FormClosed(object sender, FormClosedEventArgs e)
        {
            DisposeImages();
        }

        private void DisposeImages()
        {
            mBgImage.Dispose();
            bmpBgImage.Dispose();
        }
    }

    /// <summary>
    /// Options class for Crop
    /// </summary>
    public class CropOptions
    {
        public bool SelectedWindowMode { get; set; }
        public Image MyImage { get; set; }
    }

    public class DynamicCrosshair
    {
        private int Interval;
        private int Step;
        private int CurrentStep;
        private int MinSize = 1;
        private int MaxSize;
        private int MaxWidth;
        private Stopwatch Timer = new Stopwatch();
        private long LastTime = 0;
        private int CurrentSize;
        private int NormalSize;
        private int LineCount;
        private int LineSize;
        private Color CrosshairColor = XMLSettings.DeserializeColor(Program.conf.CropCrosshairColor);

        public DynamicCrosshair()
        {
            Interval = Program.conf.CropInterval;
            Step = Program.conf.CropStep;
            LineCount = Program.conf.CrosshairLineCount;
            LineSize = Program.conf.CrosshairLineSize;
            CurrentStep = Step;
            MaxSize = LineSize;
            MaxWidth = MaxSize * LineCount;
            NormalSize = MinSize + ((MaxSize - MinSize) / 2);
            CurrentSize = NormalSize;
            Timer.Start();
        }

        public void Draw(Graphics g, Point mousePos)
        {
            g.SmoothingMode = SmoothingMode.HighQuality;
            if (Program.conf.CropDynamicCrosshair)
            {
                if (Timer.ElapsedMilliseconds - LastTime >= Interval)
                {
                    CurrentSize += CurrentStep;
                    if (CurrentSize > MaxSize)
                    {
                        CurrentStep = -Step;
                        CurrentSize += CurrentStep;
                    }
                    else if (CurrentSize < MinSize)
                    {
                        //CurrentStep = Step;
                        CurrentSize = MaxSize;
                    }
                    LastTime = Timer.ElapsedMilliseconds;
                }
            }
            else
            {
                CurrentSize = NormalSize;
            }
            if (Program.conf.CropGridToggle)
            {
                for (int i = 0; i < LineCount; i++)
                {
                    g.DrawRectangle(new Pen(Color.FromArgb((255 / LineCount) * (i + 1), CrosshairColor)),
                        mousePos.X - (CurrentSize + (i * LineSize)) / 2,
                        mousePos.Y - (CurrentSize + (i * LineSize)) / 2,
                        (CurrentSize + (i * LineSize)), (CurrentSize + (i * LineSize)));
                }
            }
            else
            {
                for (int i = 0; i < LineCount; i++)
                {
                    g.DrawEllipse(new Pen(Color.FromArgb((255 / LineCount) * (i + 1), CrosshairColor)),
                        mousePos.X - (CurrentSize + (i * LineSize)) / 2,
                        mousePos.Y - (CurrentSize + (i * LineSize)) / 2,
                        (CurrentSize + (i * LineSize)), (CurrentSize + (i * LineSize)));
                }
                g.DrawEllipse(new Pen(Color.FromArgb(50, CrosshairColor)), mousePos.X - MaxWidth / 2, mousePos.Y - MaxWidth / 2,
                    MaxWidth, MaxWidth);
            }
            g.DrawLine(new Pen(CrosshairColor), new Point(mousePos.X - (MaxWidth - (MaxSize - CurrentSize)) / 2, mousePos.Y),
                new Point(mousePos.X + (MaxWidth - (MaxSize - CurrentSize)) / 2, mousePos.Y));
            g.DrawLine(new Pen(CrosshairColor), new Point(mousePos.X, mousePos.Y - (MaxWidth - (MaxSize - CurrentSize)) / 2),
                new Point(mousePos.X, mousePos.Y + (MaxWidth - (MaxSize - CurrentSize)) / 2));
        }
    }
}
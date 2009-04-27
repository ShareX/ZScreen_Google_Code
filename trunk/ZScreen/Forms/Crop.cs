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

// Update: 20080401 (Isaac) Fixing multiple screen handling

using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using ZSS.Colors;
using System.Drawing.Imaging;

namespace ZSS
{
    partial class Crop : Form
    {
        private bool mMouseDown;
        private Image mBgImage;
        private Point mousePos, mousePosOnClick, oldMousePos;
        private Point screenMousePos;
        private Rectangle screenBound;
        private Rectangle clientBound;
        private Rectangle cropRegion;
        private Rectangle rectRegion;
        private Bitmap bmpBgImage;
        private Pen labelBorderPen = new Pen(Color.Black);
        private Pen crosshairPen = new Pen(XMLSettings.DeserializeColor(Program.conf.CropCrosshairColor));
        private Pen crosshairPen2 = new Pen(Color.FromArgb(150, Color.Gray));
        private string strMouseUp = "Mouse Left Down: Create crop region" +
            "\nMouse Right Down & Escape: Cancel Screenshot\nSpace: Capture Entire Screen\nTab: Toggle Crop Grid mode";
        private string strMouseDown = "Mouse Left Up: Capture Screenshot" +
            "\nMouse Right Down & Escape & Space: Cancel crop region\nTab: Toggle Crop Grid mode";
        private Queue windows = new Queue();
        private Timer timer = new Timer();
        private Timer windowCheck = new Timer();
        private bool selectedWindowMode;
        private bool forceCheck;
        private Rectangle rectIntersect;
        private DynamicCrosshair crosshair;
        private DynamicRectangle myRectangle;

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

        public Crop(Image myImage, bool windowMode)
        {
            selectedWindowMode = windowMode;
            mBgImage = new Bitmap(myImage);
            bmpBgImage = new Bitmap(mBgImage);
            InitializeComponent();
            Bounds = MyGraphics.GetScreenBounds();
            //This should not be used anymore since we will normalize points to client's coordinate
            //rectIntersect.Location = this.Bounds.Location;
            rectIntersect.Size = new Size(Bounds.Width - 1, Bounds.Height - 1);
            SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint, true);

            CalculateBoundaryFromMousePosition();

            timer.Interval = 10;
            timer.Tick += new EventHandler(TimerTick);
            windowCheck.Interval = 250;
            windowCheck.Tick += new EventHandler(WindowCheckTick);
            crosshair = new DynamicCrosshair();

            if (selectedWindowMode)
            {
                myRectangle = new DynamicRectangle(CaptureType.SELECTED_WINDOW);
                User32.EnumWindowsProc ewp = new User32.EnumWindowsProc(EvalWindow);
                User32.EnumWindows(ewp, 0);
            }
            else
            {
                myRectangle = new DynamicRectangle(CaptureType.CROP);
                Cursor.Hide();
            }

            Graphics g = Graphics.FromImage(mBgImage);
            g.SmoothingMode = SmoothingMode.HighQuality;

            if ((selectedWindowMode && Program.conf.SelectedWindowRegionStyles == RegionStyles.BACKGROUND_REGION_TRANSPARENT) ||
                (!selectedWindowMode && Program.conf.CropRegionStyles == RegionStyles.BACKGROUND_REGION_TRANSPARENT))
            { //If Background Region Transparent
                g.FillRectangle(new SolidBrush(Color.FromArgb(100, Color.White)),
                    new Rectangle(0, 0, mBgImage.Width, mBgImage.Height));
            }
            else if ((selectedWindowMode && Program.conf.SelectedWindowRegionStyles == RegionStyles.BACKGROUND_REGION_GRAYSCALE) ||
                (!selectedWindowMode && Program.conf.CropRegionStyles == RegionStyles.BACKGROUND_REGION_GRAYSCALE))
            { //If Background Region Grayscale
                ImageAttributes imgattr = new ImageAttributes();
                imgattr.SetColorMatrix(MyGraphics.GrayscaleFilter());
                g.DrawImage(mBgImage, new Rectangle(0, 0, mBgImage.Width, mBgImage.Height), 0, 0,
                    mBgImage.Width, mBgImage.Height, GraphicsUnit.Pixel, imgattr);
            }
        }

        private void Crop_Shown(object sender, EventArgs e)
        {
            TopMost = true;
            windowCheck.Start();
            timer.Start();
        }

        private void WindowCheckTick(object sender, EventArgs e)
        {
            if (User32.GetForegroundWindow() != Handle)
            {
                User32.ActivateWindow(Handle);
            }
        }

        private void TimerTick(object sender, EventArgs e)
        {
            CalculateBoundaryFromMousePosition();

            if (Program.conf.CropDynamicCrosshair) forceCheck = true;
            if (oldMousePos != mousePos || forceCheck)
            {
                oldMousePos = mousePos;
                forceCheck = false;
                if (selectedWindowMode)
                {
                    IEnumerator enumerator = windows.GetEnumerator();
                    while (enumerator.MoveNext())
                    {
                        KeyValuePair<IntPtr, Rectangle> kv = (KeyValuePair<IntPtr, Rectangle>)enumerator.Current;
                        if (kv.Value.Contains(Cursor.Position))
                        {
                            CropRegion = new Rectangle(PointToClient(kv.Value.Location), kv.Value.Size);
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
                        CropRegion = Rectangle.Intersect(CropRegion, rectIntersect);
                        mousePos = mousePos.Intersect(rectIntersect);
                    }
                }
                Refresh();
            }
        }

        private bool EvalWindow(IntPtr hWnd, int lParam)
        {
            if (!User32.IsWindowVisible(hWnd)) return true;
            if (Handle == hWnd) return false;

            Rectangle rect = User32.GetWindowRectangle(hWnd);
            rect.Intersect(Bounds);
            windows.Enqueue(new KeyValuePair<IntPtr, Rectangle>(hWnd, rect));

            return true;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.HighSpeed;
            g.DrawImage(mBgImage, 0, 0, mBgImage.Width, mBgImage.Height); //Draw background

            if ((selectedWindowMode && Program.conf.SelectedWindowRegionStyles == RegionStyles.REGION_TRANSPARENT) ||
                (!selectedWindowMode && Program.conf.CropRegionStyles == RegionStyles.REGION_TRANSPARENT && mMouseDown))
            { //If Region Transparent
                g.FillRectangle(new SolidBrush(Color.FromArgb(75, Color.White)), CropRegion);
            }
            else if (((selectedWindowMode &&
                (Program.conf.SelectedWindowRegionStyles == RegionStyles.BACKGROUND_REGION_TRANSPARENT ||
                Program.conf.SelectedWindowRegionStyles == RegionStyles.BACKGROUND_REGION_GRAYSCALE)) ||
                (!selectedWindowMode && (Program.conf.CropRegionStyles == RegionStyles.BACKGROUND_REGION_TRANSPARENT ||
                Program.conf.CropRegionStyles == RegionStyles.BACKGROUND_REGION_GRAYSCALE) &&
                mMouseDown)) && CropRegion.Width > 0 && CropRegion.Height > 0)
            { //If Background Region Transparent or Background Region Grayscale
                g.DrawImage(bmpBgImage, CropRegion, CropRegion, GraphicsUnit.Pixel);
            }

            if (selectedWindowMode)
            {
                if (Program.conf.SelectedWindowAddBorder)
                {
                    IEnumerator enumerator = windows.GetEnumerator();
                    while (enumerator.MoveNext())
                    {
                        KeyValuePair<IntPtr, Rectangle> kv = (KeyValuePair<IntPtr, Rectangle>)enumerator.Current;
                        g.DrawRectangle(new Pen(Brushes.Red), new Rectangle(PointToClient(kv.Value.Location), kv.Value.Size));
                    }
                }
                myRectangle.DrawRectangle(g, CropRegion);
                if (Program.conf.SelectedWindowRectangleInfo)
                {
                    DrawTooltip("X: " + CropRegion.X + " px, Y: " + CropRegion.Y + " px\nWidth: " + CropRegion.Width +
                        " px, Height: " + CropRegion.Height + " px", new Point(20, 20), g);
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
                    myRectangle.DrawRectangle(g, CropRegion);
                    if (Program.conf.CropRegionRectangleInfo)
                    {
                        DrawTooltip("X: " + CropRegion.X + " px, Y: " + CropRegion.Y + " px\nWidth: " +
                            rectRegion.Width + " px, Height: " + rectRegion.Height + " px", new Point(20, 20), g);
                    }
                    g.DrawLine(crosshairPen, new Point(mousePosOnClick.X - 10, mousePosOnClick.Y),
                        new Point(mousePosOnClick.X + 10, mousePosOnClick.Y));
                    g.DrawLine(crosshairPen, new Point(mousePosOnClick.X, mousePosOnClick.Y - 10),
                        new Point(mousePosOnClick.X, mousePosOnClick.Y + 10));
                }
                else
                {
                    DrawInstructor(strMouseUp, g);
                    if (Program.conf.CropRegionRectangleInfo)
                    {
                        DrawTooltip("X: " + mousePos.X + " px, Y: " + mousePos.Y + " px", new Point(20, 20), g);
                    }
                }
                crosshair.Draw(g, mousePos);
            }
        }

        private void DrawTooltip(string text, Point offset, Graphics g)
        {
            Font font = new Font(FontFamily.GenericSansSerif, 8);
            Point mPos = mousePos;
            Rectangle labelRect = new Rectangle(new Point(mPos.X + offset.X, mPos.Y + offset.Y),
                new Size(TextRenderer.MeasureText(text, font).Width + 10, TextRenderer.MeasureText(text, font).Height + 10));
            if (labelRect.Right > clientBound.Right - 5) labelRect.X = mPos.X - offset.X - labelRect.Width;
            if (labelRect.Bottom > clientBound.Bottom - 5) labelRect.Y = mPos.Y - offset.Y - labelRect.Height;
            GraphicsPath gPath = MyGraphics.RoundedRectangle(labelRect, 7);
            g.FillPath(new LinearGradientBrush(new Point(labelRect.X, labelRect.Y),
                new Point(labelRect.X + labelRect.Width, labelRect.Y), Color.Black, Color.FromArgb(150, Color.Black)), gPath);
            g.DrawPath(labelBorderPen, gPath);
            g.DrawString(text, font, new SolidBrush(Color.White), labelRect.X + 5, labelRect.Y + 5);
            if (!selectedWindowMode && Program.conf.CropShowMagnifyingGlass)
            {
                g.DrawImage(MyGraphics.MagnifyingGlass((Bitmap)bmpBgImage, mousePos, 100, 5), labelRect.X,
                labelRect.Y - labelRect.Height - 100 - offset.Y);
            }
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
                Point textPos = PointToClient(new Point(screenBound.Left +
                    (screenBound.Width / 2) - ((textSize.Width + 10) / 2), screenBound.Top + 30));
                Rectangle labelRect = new Rectangle(textPos, new Size(textSize.Width + 30, textSize.Height + 10));
                GraphicsPath gPath = MyGraphics.RoundedRectangle(labelRect, 7);
                g.FillPath(new LinearGradientBrush(new Point(labelRect.X, labelRect.Y), new Point(labelRect.X +
                    labelRect.Width, labelRect.Y), Color.White, Color.FromArgb(150, Color.White)), gPath);
                g.DrawPath(labelBorderPen, gPath);
                g.DrawString(drawText, posFont, new SolidBrush(Color.Black), labelRect.X + 5, labelRect.Y + 5);
            }
        }

        private void CalculateBoundaryFromMousePosition()
        {
            mousePos = PointToClient(MousePosition);
            screenMousePos = PointToScreen(mousePos);
            screenBound = Screen.GetBounds(screenMousePos);
            clientBound = new Rectangle(PointToClient(screenBound.Location), screenBound.Size);
        }

        private void Crop_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (selectedWindowMode)
                {
                    //if (Program.conf.SelectedWindowFront)
                    //{
                    //    User32.ActivateWindow(mHandle);
                    //}
                    ReturnImageAndExit();
                }
                else
                {
                    mousePosOnClick = PointToClient(MousePosition);
                    CropRegion = new Rectangle(mousePosOnClick, new Size(0, 0));
                    mMouseDown = true;
                    Refresh();
                }
            }
            if (e.Button == MouseButtons.Right)
            {
                if (mMouseDown)
                {
                    CancelAndRestart();
                }
                else
                {
                    ReturnNullAndExit();
                }
            }
        }

        private void Crop_MouseUp(object sender, MouseEventArgs e)
        {
            if (!selectedWindowMode && mMouseDown)
            {
                mMouseDown = false;
                if (CropRegion.Width > 0 && CropRegion.Height > 0)
                {
                    ReturnImageAndExit();
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
                    ReturnImageAndExit();
                }
                if (e.KeyChar == (int)Keys.Escape)
                {
                    ReturnNullAndExit();
                }
            }
            if (mMouseDown && (e.KeyChar == (int)Keys.Escape || e.KeyChar == (int)Keys.Space))
            {
                CancelAndRestart();
            }
            if (e.KeyChar == (int)Keys.Tab && !selectedWindowMode)
            {
                Program.conf.CropGridToggle = !Program.conf.CropGridToggle;
                Program.conf.Save();
                forceCheck = true;
            }
        }

        private void CancelAndRestart()
        {
            mMouseDown = false;
            Refresh();
        }

        private void ReturnImageAndExit()
        {
            if (selectedWindowMode)
            {
                Program.LastCapture = CropRegion;
            }
            else
            {
                Program.LastRegion = rectRegion;
            }
            DialogResult = DialogResult.OK;
            Close();
        }

        private void ReturnNullAndExit()
        {
            System.Threading.Thread.Sleep(150); //fixes right click menus from displaying in external programs after close
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void Crop_FormClosing(object sender, FormClosingEventArgs e)
        {
            timer.Stop();
            windowCheck.Stop();
            if (!selectedWindowMode) Cursor.Show();
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

    public class DynamicRectangle
    {
        private HSB color;
        private float size;
        private bool ruler;
        private Rectangle region;
        private int colorDiff;
        private double colorHue;
        private double colorHueMin;
        private double colorHueMax;
        private int step;
        private int currentStep;
        private Stopwatch timer;
        private long lastTime;
        private int interval;
        private bool changeColor;

        private double ColorHue
        {
            get { return colorHue; }
            set
            {
                colorHue = value;
                if (colorHue > 360)
                {
                    color.Hue360 = colorHue - 360;
                }
                else if (colorHue < 0)
                {
                    color.Hue360 = 360 + colorHue;
                }
                else
                {
                    color.Hue360 = colorHue;
                }
            }
        }

        public DynamicRectangle(CaptureType ct)
        {
            if (ct == CaptureType.CROP)
            {
                color = XMLSettings.DeserializeColor(Program.conf.CropBorderColor);
                size = (float)Program.conf.CropBorderSize;
                ruler = Program.conf.CropShowRuler;
                changeColor = Program.conf.CropDynamicBorderColor;
                interval = (int)Program.conf.CropRegionInterval;
                step = (int)Program.conf.CropRegionStep;
                colorDiff = (int)Program.conf.CropHueRange;
            }
            else if (ct == CaptureType.SELECTED_WINDOW)
            {
                color = XMLSettings.DeserializeColor(Program.conf.SelectedWindowBorderColor);
                size = (float)Program.conf.SelectedWindowBorderSize;
                ruler = Program.conf.SelectedWindowRuler;
                changeColor = Program.conf.SelectedWindowDynamicBorderColor;
                interval = (int)Program.conf.SelectedWindowRegionInterval;
                step = (int)Program.conf.SelectedWindowRegionStep;
                colorDiff = (int)Program.conf.SelectedWindowHueRange;
            }
            colorHue = color.Hue * 360;
            colorHueMin = color.Hue * 360 - colorDiff;
            colorHueMax = color.Hue * 360 + colorDiff;
            currentStep = step;
            timer = new Stopwatch();
            timer.Start();
        }

        public void DrawRectangle(Graphics g, Rectangle rect)
        {
            region = rect;
            if (size > 0)
            {
                if (changeColor && timer.ElapsedMilliseconds - lastTime >= interval)
                {
                    FindNewColor();
                    lastTime = timer.ElapsedMilliseconds;
                }
                g.DrawRectangle(new Pen(color, size), region);
                if (ruler)
                {
                    DrawRuler(g, 5, 10);
                    DrawRuler(g, 20, 100);
                }
            }
        }

        private void DrawRuler(Graphics g, int rulerSize, int rulerWidth)
        {
            Pen pen = new Pen(color);
            if (region.Width >= rulerSize && region.Height >= rulerSize)
            {
                for (int x = 1; x <= region.Width / rulerWidth; x++)
                {
                    g.DrawLine(pen, new Point(region.X + x * rulerWidth, region.Y),
                        new Point(region.X + x * rulerWidth, region.Y + rulerSize));
                    g.DrawLine(pen, new Point(region.X + x * rulerWidth, region.Bottom),
                        new Point(region.X + x * rulerWidth, region.Bottom - rulerSize));
                }
                for (int y = 1; y <= region.Height / rulerWidth; y++)
                {
                    g.DrawLine(pen, new Point(region.X, region.Y + y * rulerWidth),
                        new Point(region.X + rulerSize, region.Y + y * rulerWidth));
                    g.DrawLine(pen, new Point(region.Right, region.Y + y * rulerWidth),
                           new Point(region.Right - rulerSize, region.Y + y * rulerWidth));
                }
            }
        }

        private void FindNewColor()
        {
            if (ColorHue + currentStep > colorHueMax)
            {
                currentStep = -step;
            }
            else if (ColorHue + currentStep < colorHueMin)
            {
                currentStep = step;
            }
            ColorHue += currentStep;
            //FileSystem.AppendDebug(colorHue + " " + colorHueMin + " " + colorHueMax + " " + (double)currentStep);
        }
    }

    public class DynamicCrosshair
    {
        private int interval = Program.conf.CropInterval;
        private int step = Program.conf.CropStep;
        private int currentStep;
        private int minSize = 1;
        private int maxSize;
        private int maxWidth;
        private Stopwatch timer = new Stopwatch();
        private long lastTime;
        private int currentSize;
        private int normalSize;
        private int lineCount = Program.conf.CrosshairLineCount;
        private int lineSize = Program.conf.CrosshairLineSize;
        private Color crosshairColor = XMLSettings.DeserializeColor(Program.conf.CropCrosshairColor);

        public DynamicCrosshair()
        {
            currentStep = -step;
            maxSize = lineSize;
            maxWidth = maxSize * lineCount;
            normalSize = minSize + ((maxSize - minSize) / 2);
            currentSize = normalSize;
            timer.Start();
        }

        public void Draw(Graphics g, Point mousePos)
        {
            g.SmoothingMode = SmoothingMode.HighQuality;
            if (Program.conf.CropDynamicCrosshair)
            {
                if (timer.ElapsedMilliseconds - lastTime >= interval)
                {
                    currentSize += currentStep;
                    if (currentSize > maxSize)
                    {
                        currentStep = -step;
                        currentSize += currentStep;
                    }
                    else if (currentSize < minSize)
                    {
                        //currentStep = step;
                        currentSize = maxSize;
                    }
                    lastTime = timer.ElapsedMilliseconds;
                }
            }
            else
            {
                currentSize = normalSize;
            }
            if (Program.conf.CropGridToggle)
            {
                for (int i = 0; i < lineCount; i++)
                {
                    g.DrawRectangle(new Pen(Color.FromArgb((255 / lineCount) * (i + 1), crosshairColor)),
                        mousePos.X - (currentSize + (i * lineSize)) / 2,
                        mousePos.Y - (currentSize + (i * lineSize)) / 2,
                        (currentSize + (i * lineSize)), (currentSize + (i * lineSize)));
                }
                g.DrawRectangle(new Pen(Color.FromArgb(75, crosshairColor)), mousePos.X - maxWidth / 2,
                    mousePos.Y - maxWidth / 2, maxWidth, maxWidth);
            }
            else
            {
                for (int i = 0; i < lineCount; i++)
                {
                    g.DrawEllipse(new Pen(Color.FromArgb((255 / lineCount) * (i + 1), crosshairColor)),
                        mousePos.X - (currentSize + (i * lineSize)) / 2,
                        mousePos.Y - (currentSize + (i * lineSize)) / 2,
                        (currentSize + (i * lineSize)), (currentSize + (i * lineSize)));
                }
                g.DrawEllipse(new Pen(Color.FromArgb(50, crosshairColor)), mousePos.X - maxWidth / 2,
                    mousePos.Y - maxWidth / 2, maxWidth, maxWidth);
            }
            g.DrawLine(new Pen(crosshairColor), new Point(mousePos.X - (maxWidth - (maxSize - currentSize)) / 2, mousePos.Y),
                new Point(mousePos.X + (maxWidth - (maxSize - currentSize)) / 2, mousePos.Y));
            g.DrawLine(new Pen(crosshairColor), new Point(mousePos.X, mousePos.Y - (maxWidth - (maxSize - currentSize)) / 2),
                new Point(mousePos.X, mousePos.Y + (maxWidth - (maxSize - currentSize)) / 2));
        }
    }
}
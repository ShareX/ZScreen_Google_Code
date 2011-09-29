#region License Information (GPL v2)

/*
    ZScreen - A program that allows you to upload screenshots in one keystroke.
    Copyright (C) 2008-2011 ZScreen Developers

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

#endregion License Information (GPL v2)

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Windows.Forms;
using GraphicsMgrLib;
using ZSS.ColorsLib;

namespace ZScreenLib
{
    public class Crop : Form
    {
        #region Private Variables

        private bool mouseDown, selectedWindowMode, forceCheck, captureObjects, dragging;
        private Bitmap bmpClean, bmpBackground, bmpRegion;
        private TextureBrush brushClean, brushBackground, brushRegion;
        private Point mousePos, mousePosOnClick, oldMousePos, screenMousePos;
        private Rectangle screenBound, clientBound, cropRegion, rectRegion, rectIntersect;
        private Pen labelBorderPen = new Pen(Color.Black);
        private Pen crosshairPen = new Pen(Engine.conf.CropCrosshairArgb);
        private Pen crosshairPen2 = new Pen(Color.FromArgb(150, Color.Gray));
        private string strMouseUp = "Mouse Left Down: Create crop region" +
            "\nMouse Right Down & Escape: Cancel screenshot\nSpace: Capture entire screen\nTab: Toggle crop grid mode";
        private string strMouseDown = "Mouse Left Up: Capture screenshot" +
            "\nMouse Right Down & Escape & Space: Cancel crop region\nTab: Toggle crop grid mode\n" +
            "Arrow Keys: Re-position crop region (Hold shift to move faster)";
        private Queue windows = new Queue();
        private Timer timer = new Timer { Interval = 10 };
        private Timer windowCheck = new Timer { Interval = 250 };
        private DynamicCrosshair crosshair = new DynamicCrosshair();
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

        #endregion Private Variables

        public Point CursorPos { get; set; }

        /// <summary>
        /// Crop shot or Selected Window captures
        /// </summary>
        /// <param name="myImage">Fullscreen image</param>
        /// <param name="windowMode">True = Selected window, False = Crop shot</param>
        public Crop(Image myImage, bool windowMode)
        {
            InitializeComponent();
            selectedWindowMode = windowMode;
            bmpClean = new Bitmap(myImage);
            bmpBackground = new Bitmap(bmpClean);
            bmpRegion = new Bitmap(bmpClean);
            Bounds = GraphicsMgr.GetScreenBounds();
            this.CursorPos = this.PointToClient(Cursor.Position);
            rectIntersect.Size = new Size(Bounds.Width - 1, Bounds.Height - 1);
            SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint, true);
            CalculateBoundaryFromMousePosition();
            timer.Tick += new EventHandler(TimerTick);
            windowCheck.Tick += new EventHandler(WindowCheckTick);

            if (selectedWindowMode)
            {
                captureObjects = Engine.conf.SelectedWindowCaptureObjects;
                myRectangle = new DynamicRectangle(CaptureType.SELECTED_WINDOW);
                NativeMethods.EnumWindowsProc ewp = new NativeMethods.EnumWindowsProc(EvalWindow);
                NativeMethods.EnumWindows(ewp, IntPtr.Zero);
            }
            else
            {
                myRectangle = new DynamicRectangle(CaptureType.CROP);

                if (Engine.conf.UseHardwareCursor)
                {
                    Cursor = Cursors.Cross;
                }
                else
                {
                    Cursor.Hide();
                }
            }

            using (Graphics gBackground = Graphics.FromImage(bmpBackground))
            using (Graphics gRegion = Graphics.FromImage(bmpRegion))
            {
                gBackground.SmoothingMode = SmoothingMode.HighQuality;
                gRegion.SmoothingMode = SmoothingMode.HighQuality;

                if ((selectedWindowMode && Engine.conf.SelectedWindowRegionStyles == RegionStyles.REGION_TRANSPARENT) ||
                (!selectedWindowMode && Engine.conf.CropRegionStyles == RegionStyles.REGION_TRANSPARENT))
                { // If Region Transparent
                    gRegion.FillRectangle(new SolidBrush(Color.FromArgb(Engine.conf.RegionTransparentValue, Color.White)),
                        new Rectangle(0, 0, bmpRegion.Width, bmpRegion.Height));
                }
                else if ((selectedWindowMode && Engine.conf.SelectedWindowRegionStyles == RegionStyles.REGION_BRIGHTNESS) ||
                (!selectedWindowMode && Engine.conf.CropRegionStyles == RegionStyles.REGION_BRIGHTNESS))
                { // If Region Brightness
                    ImageAttributes imgattr = new ImageAttributes();
                    imgattr.SetColorMatrix(ColorMatrices.BrightnessFilter(Engine.conf.RegionBrightnessValue));
                    gRegion.DrawImage(bmpClean, new Rectangle(0, 0, bmpRegion.Width, bmpRegion.Height), 0, 0,
                        bmpRegion.Width, bmpRegion.Height, GraphicsUnit.Pixel, imgattr);
                }
                else if ((selectedWindowMode && Engine.conf.SelectedWindowRegionStyles == RegionStyles.BACKGROUND_REGION_TRANSPARENT) ||
                (!selectedWindowMode && Engine.conf.CropRegionStyles == RegionStyles.BACKGROUND_REGION_TRANSPARENT))
                { // If Background Region Transparent
                    gBackground.FillRectangle(new SolidBrush(Color.FromArgb(Engine.conf.BackgroundRegionTransparentValue, Color.White)),
                        new Rectangle(0, 0, bmpBackground.Width, bmpBackground.Height));
                }
                else if ((selectedWindowMode && Engine.conf.SelectedWindowRegionStyles == RegionStyles.BACKGROUND_REGION_BRIGHTNESS) ||
                (!selectedWindowMode && Engine.conf.CropRegionStyles == RegionStyles.BACKGROUND_REGION_BRIGHTNESS))
                { // If Background Region Brightness
                    ImageAttributes imgattr = new ImageAttributes();
                    imgattr.SetColorMatrix(ColorMatrices.BrightnessFilter(Engine.conf.BackgroundRegionBrightnessValue));
                    gBackground.DrawImage(bmpClean, new Rectangle(0, 0, bmpBackground.Width, bmpBackground.Height), 0, 0,
                        bmpBackground.Width, bmpBackground.Height, GraphicsUnit.Pixel, imgattr);
                }
                else if ((selectedWindowMode && Engine.conf.SelectedWindowRegionStyles == RegionStyles.BACKGROUND_REGION_GRAYSCALE) ||
                (!selectedWindowMode && Engine.conf.CropRegionStyles == RegionStyles.BACKGROUND_REGION_GRAYSCALE))
                { // If Background Region Grayscale
                    ImageAttributes imgattr = new ImageAttributes();
                    imgattr.SetColorMatrix(ColorMatrices.GrayscaleFilter());
                    gBackground.DrawImage(bmpClean, new Rectangle(0, 0, bmpBackground.Width, bmpBackground.Height), 0, 0,
                        bmpBackground.Width, bmpBackground.Height, GraphicsUnit.Pixel, imgattr);
                }
            }

            brushClean = new TextureBrush(bmpClean);
            brushBackground = new TextureBrush(bmpBackground);
            brushRegion = new TextureBrush(bmpRegion);
        }

        private void Crop_Shown(object sender, EventArgs e)
        {
            TopMost = true;
            windowCheck.Start();
            timer.Start();
        }

        private void WindowCheckTick(object sender, EventArgs e)
        {
            if (NativeMethods.GetForegroundWindow() != Handle)
            {
                NativeMethods.ActivateWindow(Handle);
            }
        }

        private bool IsDragging(Point point)
        {
            int checkSize = 0;

            if (selectedWindowMode) checkSize = 15;

            return mouseDown && (point.X > mousePosOnClick.X + checkSize || point.Y > mousePosOnClick.Y + checkSize ||
                point.X < mousePosOnClick.X - checkSize || point.Y < mousePosOnClick.Y - checkSize);
        }

        private void TimerTick(object sender, EventArgs e)
        {
            CalculateBoundaryFromMousePosition();

            if (Engine.conf.CropDynamicCrosshair) forceCheck = true;

            if (oldMousePos != mousePos || forceCheck)
            {
                oldMousePos = mousePos;
                forceCheck = false;

                dragging = IsDragging(mousePos);

                if (selectedWindowMode)
                {
                    if (!dragging)
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
                }

                if (mouseDown && dragging)
                {
                    CropRegion = GraphicsMgr.GetRectangle(mousePos.X, mousePos.Y,
                        mousePosOnClick.X - mousePos.X, mousePosOnClick.Y - mousePos.Y, Engine.conf.CropGridSize,
                        Engine.conf.CropGridToggle, ref mousePos);
                    CropRegion = Rectangle.Intersect(CropRegion, rectIntersect);
                    mousePos = mousePos.Intersect(rectIntersect);
                }

                Refresh();
            }
        }

        private bool EvalWindow(IntPtr hWnd, IntPtr lParam)
        {
            if (!NativeMethods.IsWindowVisible(hWnd)) return true;
            if (Handle == hWnd) return false;

            foreach (KeyValuePair<IntPtr, Rectangle> pair in windows)
            {
                if (pair.Key == hWnd)
                {
                    return true;
                }
            }

            if (captureObjects)
            {
                NativeMethods.EnumWindowsProc ewp = new NativeMethods.EnumWindowsProc(EvalWindow);
                NativeMethods.EnumChildWindows(hWnd, ewp, IntPtr.Zero);
            }

            Rectangle rect = NativeMethods.GetWindowRectangle(hWnd);
            rect.Intersect(Bounds);
            windows.Enqueue(new KeyValuePair<IntPtr, Rectangle>(hWnd, rect));

            return true;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.InterpolationMode = InterpolationMode.NearestNeighbor;
            g.SmoothingMode = SmoothingMode.HighSpeed;

            // Draw background
            g.FillRectangle(brushBackground, 0, 0, bmpBackground.Width, bmpBackground.Height);

            // Draw region
            if ((selectedWindowMode && (Engine.conf.SelectedWindowRegionStyles == RegionStyles.REGION_TRANSPARENT ||
                Engine.conf.SelectedWindowRegionStyles == RegionStyles.REGION_BRIGHTNESS)) ||
                (!selectedWindowMode && (Engine.conf.CropRegionStyles == RegionStyles.REGION_TRANSPARENT ||
                Engine.conf.CropRegionStyles == RegionStyles.REGION_BRIGHTNESS) && mouseDown))
            { // If Region Transparent or Region Brightness
                g.FillRectangle(brushRegion, CropRegion);
            }
            else if (((selectedWindowMode &&
                (Engine.conf.SelectedWindowRegionStyles == RegionStyles.BACKGROUND_REGION_TRANSPARENT ||
                Engine.conf.SelectedWindowRegionStyles == RegionStyles.BACKGROUND_REGION_GRAYSCALE ||
                Engine.conf.SelectedWindowRegionStyles == RegionStyles.BACKGROUND_REGION_BRIGHTNESS)) ||
                (!selectedWindowMode && (Engine.conf.CropRegionStyles == RegionStyles.BACKGROUND_REGION_TRANSPARENT ||
                Engine.conf.CropRegionStyles == RegionStyles.BACKGROUND_REGION_GRAYSCALE ||
                Engine.conf.CropRegionStyles == RegionStyles.BACKGROUND_REGION_BRIGHTNESS) && mouseDown))
                && CropRegion.Width > 0 && CropRegion.Height > 0)
            { // If Background Region Transparent or Background Region Grayscale or Background Region Brightness
                g.FillRectangle(brushClean, CropRegion);
            }

            if (selectedWindowMode)
            {
                myRectangle.DrawRectangle(g, CropRegion);

                if (Engine.conf.SelectedWindowRectangleInfo)
                {
                    DrawTooltip("X: " + CropRegion.X + " px, Y: " + CropRegion.Y + " px\nWidth: " + CropRegion.Width +
                        " px, Height: " + CropRegion.Height + " px", new Point(20, 20), g);
                }
            }
            else
            {
                if (Engine.conf.CropShowBigCross)
                {
                    g.DrawLine(crosshairPen2, new Point(0, mousePos.Y), new Point(bmpBackground.Width, mousePos.Y));
                    g.DrawLine(crosshairPen2, new Point(mousePos.X, 0), new Point(mousePos.X, bmpBackground.Height));
                }

                if (mouseDown)
                {
                    if (Engine.conf.CropShowGrids && Engine.conf.CropGridToggle)
                    {
                        DrawGrids(g);
                    }

                    DrawInstructor(strMouseDown, g);
                    myRectangle.DrawRectangle(g, CropRegion);

                    if (Engine.conf.CropRegionRectangleInfo)
                    {
                        DrawTooltip("X: " + CropRegion.X + " px, Y: " + CropRegion.Y + " px\nWidth: " +
                            rectRegion.Width + " px, Height: " + rectRegion.Height + " px", new Point(20, 20), g);
                    }

                    g.DrawLine(crosshairPen, new Point(mousePosOnClick.X - 10, mousePosOnClick.Y), new Point(mousePosOnClick.X + 10, mousePosOnClick.Y));
                    g.DrawLine(crosshairPen, new Point(mousePosOnClick.X, mousePosOnClick.Y - 10), new Point(mousePosOnClick.X, mousePosOnClick.Y + 10));
                }
                else
                {
                    DrawInstructor(strMouseUp, g);

                    if (Engine.conf.CropRegionRectangleInfo)
                    {
                        DrawTooltip("X: " + mousePos.X + " px, Y: " + mousePos.Y + " px", new Point(20, 20), g);
                    }
                }

                if (!Engine.conf.UseHardwareCursor)
                {
                    crosshair.Draw(g, mousePos);
                }
            }
        }

        private void DrawTooltip(string text, Point offset, Graphics g)
        {
            g.SmoothingMode = SmoothingMode.HighQuality;
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAliasGridFit;
            Font font = new Font(FontFamily.GenericSansSerif, 8);
            Point mPos = mousePos;
            Rectangle labelRect = new Rectangle(new Point(mPos.X + offset.X, mPos.Y + offset.Y),
                new Size(TextRenderer.MeasureText(text, font).Width + 10, TextRenderer.MeasureText(text, font).Height + 10));
            if (labelRect.Right > clientBound.Right - 5) labelRect.X = mPos.X - offset.X - labelRect.Width;
            if (labelRect.Bottom > clientBound.Bottom - 5) labelRect.Y = mPos.Y - offset.Y - labelRect.Height;
            using (GraphicsPath gPath = GraphicsEx.GetRoundedRectangle(labelRect, 6))
            {
                g.FillPath(new LinearGradientBrush(new Point(labelRect.X, labelRect.Y),
                    new Point(labelRect.X + labelRect.Width, labelRect.Y), Color.FromArgb(200, Color.Black), Color.FromArgb(100, Color.Black)), gPath);
                g.DrawPath(labelBorderPen, gPath);
            }
            g.DrawString(text, font, new SolidBrush(Color.White), labelRect.X + 5, labelRect.Y + 5);
            if ((!selectedWindowMode || (selectedWindowMode && dragging)) && Engine.conf.CropShowMagnifyingGlass)
            {
                int posY = labelRect.Y - offset.Y * 2 - 100;
                if (posY < 5) posY = labelRect.Y + labelRect.Height + 10;
                g.DrawImage(GraphicsMgr.MagnifyingGlass((Bitmap)bmpClean, mousePos, 100, 5), labelRect.X, posY);
            }
        }

        private void DrawGrids(Graphics g)
        {
            if (Engine.conf.CropGridSize.Width >= 10)
            {
                for (int x = 0; x <= (CropRegion.Width / Engine.conf.CropGridSize.Width); x++)
                {
                    g.DrawLine(crosshairPen2,
                        new Point(CropRegion.X + (Engine.conf.CropGridSize.Width * x), CropRegion.Y),
                        new Point(CropRegion.X + (Engine.conf.CropGridSize.Width * x), CropRegion.Y + CropRegion.Height));
                }
            }
            if (Engine.conf.CropGridSize.Height >= 10)
            {
                for (int y = 0; y <= (CropRegion.Height / Engine.conf.CropGridSize.Height); y++)
                {
                    g.DrawLine(crosshairPen2,
                        new Point(CropRegion.X, CropRegion.Y + (Engine.conf.CropGridSize.Height * y)),
                        new Point(CropRegion.X + CropRegion.Width, CropRegion.Y + (Engine.conf.CropGridSize.Height * y)));
                }
            }
        }

        private void DrawInstructor(string drawText, Graphics g)
        {
            if (Engine.conf.CropRegionHotkeyInfo)
            {
                Font posFont = new Font(FontFamily.GenericSansSerif, 8);
                Size textSize = TextRenderer.MeasureText(drawText, posFont);

                Point textPos = PointToClient(new Point(screenBound.Left +
                    (screenBound.Width / 2) - ((textSize.Width + 10) / 2), screenBound.Top + 30));

                Rectangle labelRect = new Rectangle(textPos, new Size(textSize.Width + 30, textSize.Height + 10));

                if (PointIntersectsRectangle(mousePos, labelRect))
                {
                    textPos = PointToClient(new Point(screenBound.Left +
                    (screenBound.Width / 2) - ((textSize.Width + 10) / 2), screenBound.Bottom - textSize.Height - 30));
                    labelRect = new Rectangle(textPos, new Size(textSize.Width + 30, textSize.Height + 10));
                }

                using (GraphicsPath gPath = GraphicsEx.GetRoundedRectangle(labelRect, 7))
                {
                    g.FillPath(new LinearGradientBrush(new Point(labelRect.X, labelRect.Y), new Point(labelRect.X +
                    labelRect.Width, labelRect.Y), Color.White, Color.FromArgb(150, Color.White)), gPath);
                    g.DrawPath(labelBorderPen, gPath);
                }
                g.DrawString(drawText, posFont, new SolidBrush(Color.Black), labelRect.X + 5, labelRect.Y + 5);
            }
        }

        private bool PointIntersectsRectangle(Point pt, Rectangle rect)
        {
            return pt.X <= rect.Right && pt.X >= rect.Left && pt.Y >= rect.Top && pt.Y <= rect.Bottom;
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
                mousePosOnClick = PointToClient(MousePosition);
                CropRegion = new Rectangle(mousePosOnClick, new Size(0, 0));
                mouseDown = true;
                if (!selectedWindowMode) Refresh();
            }
            else if (e.Button == MouseButtons.Right)
            {
                if (mouseDown)
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
            if (mouseDown && e.Button == MouseButtons.Left)
            {
                if (CropRegion.Width > 0 && CropRegion.Height > 0)
                {
                    ReturnImageAndExit();
                }
                else
                {
                    CancelAndRestart();
                }
            }
        }

        private void Crop_KeyDown(object sender, KeyEventArgs e)
        {
            if (mouseDown)
            {
                if (e.KeyCode == Keys.Escape || e.KeyCode == Keys.Space)
                {
                    CancelAndRestart();
                }
            }
            else
            {
                if (e.KeyCode == Keys.Space)
                {
                    CropRegion = new Rectangle(0, 0, bmpBackground.Width, bmpBackground.Height);
                    ReturnImageAndExit();
                }
                if (e.KeyCode == Keys.Escape)
                {
                    ReturnNullAndExit();
                }
            }

            if (e.KeyCode == Keys.Tab && !selectedWindowMode)
            {
                Engine.conf.CropGridToggle = !Engine.conf.CropGridToggle;
                forceCheck = true;
            }

            int move = e.Shift ? 5 : 1;

            if (e.KeyCode == Keys.Left)
            {
                Cursor.Position = new Point(Cursor.Position.X - move, Cursor.Position.Y);
            }
            else if (e.KeyCode == Keys.Right)
            {
                Cursor.Position = new Point(Cursor.Position.X + move, Cursor.Position.Y);
            }
            else if (e.KeyCode == Keys.Up)
            {
                Cursor.Position = new Point(Cursor.Position.X, Cursor.Position.Y - move);
            }
            else if (e.KeyCode == Keys.Down)
            {
                Cursor.Position = new Point(Cursor.Position.X, Cursor.Position.Y + move);
            }
        }

        private void CancelAndRestart()
        {
            mouseDown = dragging = false;
            this.Refresh();
        }

        private void ReturnImageAndExit()
        {
            if (selectedWindowMode)
            {
                if (dragging)
                {
                    Engine.conf.LastCapture = rectRegion;
                }
                else
                {
                    Engine.conf.LastCapture = CropRegion;
                }
            }
            else
            {
                Engine.conf.LastRegion = rectRegion;
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
            bmpClean.Dispose();
            bmpBackground.Dispose();
            bmpRegion.Dispose();
            brushClean.Dispose();
            brushBackground.Dispose();
            brushRegion.Dispose();
            timer.Dispose();
            windowCheck.Dispose();
        }

        #region Windows Form Designer generated code

        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            DisposeImages();
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            //
            // Crop
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.CausesValidation = false;
            this.ClientSize = new System.Drawing.Size(400, 400);
            this.ControlBox = false;
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Crop";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.TopMost = true;
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Crop_MouseUp);
            this.Shown += new System.EventHandler(this.Crop_Shown);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Crop_FormClosed);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Crop_MouseDown);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Crop_FormClosing);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Crop_KeyDown);
            this.ResumeLayout(false);
        }

        #endregion Windows Form Designer generated code
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
                color = (Color)Engine.conf.CropBorderArgb;
                size = (float)Engine.conf.CropBorderSize;
                ruler = Engine.conf.CropShowRuler;
                changeColor = Engine.conf.CropDynamicBorderColor;
                interval = (int)Engine.conf.CropRegionInterval;
                step = (int)Engine.conf.CropRegionStep;
                colorDiff = (int)Engine.conf.CropHueRange;
            }
            else if (ct == CaptureType.SELECTED_WINDOW)
            {
                color = (Color)Engine.conf.SelectedWindowBorderArgb;
                size = (float)Engine.conf.SelectedWindowBorderSize;
                ruler = Engine.conf.SelectedWindowRuler;
                changeColor = Engine.conf.SelectedWindowDynamicBorderColor;
                interval = (int)Engine.conf.SelectedWindowRegionInterval;
                step = (int)Engine.conf.SelectedWindowRegionStep;
                colorDiff = (int)Engine.conf.SelectedWindowHueRange;
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
                    DrawRuler(g, 20, 75);
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
        }
    }

    public class DynamicCrosshair
    {
        private int interval = Engine.conf.CropInterval;
        private int step = Engine.conf.CropStep;
        private int currentStep;
        private int minSize = 1;
        private int maxSize;
        private int maxWidth;
        private Stopwatch timer = new Stopwatch();
        private long lastTime;
        private int currentSize;
        private int normalSize;
        private int lineCount = Engine.conf.CrosshairLineCount;
        private int lineSize = Engine.conf.CrosshairLineSize;
        private Color crosshairColor = Engine.conf.CropCrosshairArgb;

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
            if (Engine.conf.CropDynamicCrosshair)
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
            if (Engine.conf.CropGridToggle)
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
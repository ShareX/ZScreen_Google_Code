#region License Information (GPL v2)

/*
    ZUploader - A program that allows you to upload images, texts or files
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
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using HelpersLib;
using ScreenCapture;

namespace ZUploader
{
    partial class MainForm
    {
        public ScreenshotDestination Destination { get; set; }

        private delegate Image ScreenCaptureDelegate();

        private void InitHotkeys()
        {
            RegisterHotkey(Keys.Control | Keys.PageUp, UploadManager.ClipboardUpload);

            RegisterHotkey(Keys.Control | Keys.Shift | Keys.PageUp, UploadManager.UploadFile);

            tsmiFullscreen.ShortcutKeyDisplayString = "PrintScreen";
            RegisterHotkey(Keys.PrintScreen, () => CaptureScreen(false));

            tsmiRectangle.ShortcutKeyDisplayString = "Ctrl + PrintScreen";
            RegisterHotkey(Keys.Control | Keys.PrintScreen, () => CaptureRegion(new RectangleRegion(), false));

            tsmiRoundedRectangle.ShortcutKeyDisplayString = "Ctrl + Shift + R";
            RegisterHotkey(Keys.Control | Keys.Shift | Keys.R, () => CaptureRegion(new RoundedRectangleRegion(), false));

            tsmiEllipse.ShortcutKeyDisplayString = "Ctrl + Shift + E";
            RegisterHotkey(Keys.Control | Keys.Shift | Keys.E, () => CaptureRegion(new EllipseRegion(), false));

            tsmiTriangle.ShortcutKeyDisplayString = "Ctrl + Shift + T";
            RegisterHotkey(Keys.Control | Keys.Shift | Keys.T, () => CaptureRegion(new TriangleRegion(), false));

            tsmiDiamond.ShortcutKeyDisplayString = "Ctrl + Shift + D";
            RegisterHotkey(Keys.Control | Keys.Shift | Keys.D, () => CaptureRegion(new DiamondRegion(), false));

            tsmiPolygon.ShortcutKeyDisplayString = "Ctrl + Shift + P";
            RegisterHotkey(Keys.Control | Keys.Shift | Keys.P, () => CaptureRegion(new PolygonRegion(), false));

            tsmiFreeHand.ShortcutKeyDisplayString = "Shift + PrintScreen";
            RegisterHotkey(Keys.Shift | Keys.PrintScreen, () => CaptureRegion(new FreeHandRegion(), false));

            RegisterHotkey(Keys.Alt | Keys.PrintScreen, () => CaptureActiveWindow(false));
        }

        private new void Capture(ScreenCaptureDelegate capture, bool autoHideForm = true)
        {
            if (autoHideForm)
            {
                Hide();
                Thread.Sleep(250);
            }

            Image img = null;

            try
            {
                img = capture();
            }
            catch (Exception ex)
            {
                StaticHelper.WriteException(ex);
            }
            finally
            {
                if (autoHideForm)
                {
                    Show();
                }

                if (img != null)
                {
                    switch (Destination)
                    {
                        default:
                        case ScreenshotDestination.Upload:
                            UploadManager.UploadImage(img);
                            break;
                        case ScreenshotDestination.Clipboard:
                            Clipboard.SetImage(img);
                            break;
                    }
                }
            }
        }

        private void CaptureScreen(bool autoHideForm = true)
        {
            Capture(Helpers.GetScreenshot, autoHideForm);
        }

        private void CaptureActiveWindow(bool autoHideForm = true)
        {
            Capture(Helpers.GetActiveWindowScreenshot, autoHideForm);
        }

        private void CaptureRegion(Surface surface, bool autoHideForm = true)
        {
            Capture(() =>
            {
                Image img = null, screenshot = Helpers.GetScreenshot();

                surface.LoadBackground(screenshot);

                if (surface.ShowDialog() == DialogResult.OK)
                {
                    img = surface.GetRegionImage();
                }

                return img;
            }, autoHideForm);
        }

        private void tsmiFullscreen_Click(object sender, EventArgs e)
        {
            CaptureScreen();
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
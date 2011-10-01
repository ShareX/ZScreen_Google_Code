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
using System.Media;
using System.Threading;
using System.Windows.Forms;
using HelpersLib;
using HelpersLib.Hotkey;
using ScreenCapture;

namespace ZUploader
{
    partial class MainForm
    {
        public ScreenshotDestination CaptureDestination { get; set; }

        private delegate Image ScreenCaptureDelegate();

        private void InitHotkeys()
        {
            HotkeyManager hotkeyManager = new HotkeyManager(Program.mainForm);
            hotkeyManager.AddHotkey(ZUploaderHotkey.ClipboardUpload, Program.Settings.HotkeyClipboardUpload, UploadManager.ClipboardUpload);
            hotkeyManager.AddHotkey(ZUploaderHotkey.FileUpload, Program.Settings.HotkeyFileUpload, UploadManager.UploadFile);
            hotkeyManager.AddHotkey(ZUploaderHotkey.PrintScreen, Program.Settings.HotkeyPrintScreen, () => CaptureScreen(false), tsmiFullscreen);
            hotkeyManager.AddHotkey(ZUploaderHotkey.ActiveWindow, Program.Settings.HotkeyActiveWindow, () => CaptureActiveWindow(false));
            hotkeyManager.AddHotkey(ZUploaderHotkey.RectangleRegion, Program.Settings.HotkeyRectangleRegion,
                () => CaptureRegion(new RectangleRegion(), false), tsmiRectangle);
            hotkeyManager.AddHotkey(ZUploaderHotkey.RoundedRectangleRegion, Program.Settings.HotkeyRoundedRectangleRegion,
                () => CaptureRegion(new RoundedRectangleRegion(), false), tsmiRoundedRectangle);
            hotkeyManager.AddHotkey(ZUploaderHotkey.EllipseRegion, Program.Settings.HotkeyEllipseRegion,
                () => CaptureRegion(new EllipseRegion(), false), tsmiEllipse);
            hotkeyManager.AddHotkey(ZUploaderHotkey.TriangleRegion, Program.Settings.HotkeyTriangleRegion,
                () => CaptureRegion(new TriangleRegion(), false), tsmiTriangle);
            hotkeyManager.AddHotkey(ZUploaderHotkey.DiamondRegion, Program.Settings.HotkeyDiamondRegion,
                () => CaptureRegion(new DiamondRegion(), false), tsmiDiamond);
            hotkeyManager.AddHotkey(ZUploaderHotkey.PolygonRegion, Program.Settings.HotkeyPolygonRegion,
                () => CaptureRegion(new PolygonRegion(), false), tsmiPolygon);
            hotkeyManager.AddHotkey(ZUploaderHotkey.FreeHandRegion, Program.Settings.HotkeyFreeHandRegion,
                () => CaptureRegion(new FreeHandRegion(), false), tsmiFreeHand);
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
                    switch (CaptureDestination)
                    {
                        default:
                        case ScreenshotDestination.Upload:
                            UploadManager.UploadImage(img);
                            break;
                        case ScreenshotDestination.Clipboard:
                            Clipboard.SetImage(img);

                            if (Program.Settings.AutoPlaySound)
                            {
                                SystemSounds.Exclamation.Play();
                            }
                            break;
                    }
                }
            }
        }

        private void CaptureScreen(bool autoHideForm = true)
        {
            Capture(Screenshot.GetFullscreen, autoHideForm);
        }

        private void CaptureActiveWindow(bool autoHideForm = true)
        {
            Capture(Screenshot.GetActiveWindow, autoHideForm);
        }

        private void CaptureRegion(Surface surface, bool autoHideForm = true)
        {
            Capture(() =>
            {
                Image img = null, screenshot = Screenshot.GetFullscreen();

                surface.Config = Program.Settings.SurfaceOptions;
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
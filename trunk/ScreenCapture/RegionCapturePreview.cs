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
using System.Windows.Forms;

namespace ScreenCapture
{
    public partial class RegionCapturePreview : Form
    {
        public Image Result { get; private set; }

        private Bitmap screenshot;
        private Surface surface;
        public SurfaceOptions SurfaceConfig { get; set; }

        public RegionCapturePreview(SurfaceOptions surfaceConfig)
        {
            InitializeComponent();
            screenshot = Helpers.GetScreenshot();

            SurfaceConfig = surfaceConfig;

            cbDrawBorder.Checked = surfaceConfig.DrawBorder;
            cbDrawChecker.Checked = surfaceConfig.DrawChecker;
            cbQuickCrop.Checked = surfaceConfig.QuickCrop;
        }

        private void CaptureRegion()
        {
            pbResult.Image = null;

            if (SurfaceConfig != null)
            {
                surface.Config = SurfaceConfig;
            }
            else
            {
                surface.Config = new SurfaceOptions()
                {
                    DrawBorder = cbDrawBorder.Checked,
                    DrawChecker = cbDrawChecker.Checked,
                    QuickCrop = cbQuickCrop.Checked,
                    MinMoveSpeed = 1,
                    MaxMoveSpeed = 5
                };
            }

            if (surface is RectangleRegion)
            {
                RectangleRegion rectangle = (RectangleRegion)surface;
                if (rectangle.Config.IsFixedSize = cbIsFixedSize.Checked)
                {
                    rectangle.Config.FixedSize = new Size((int)nudFixedWidth.Value, (int)nudFixedHeight.Value);
                }
            }

            if (surface.ShowDialog() == DialogResult.OK)
            {
                Result = surface.GetRegionImage();
                pbResult.Image = Result;
                Text = "RegionCapture: " + Result.Width + "x" + Result.Height;
            }
        }

        private void tsbFullscreen_Click(object sender, EventArgs e)
        {
            Result = screenshot;
            pbResult.Image = Result;
        }

        private void tsbRectangle_Click(object sender, EventArgs e)
        {
            surface = new RectangleRegion(screenshot);
            CaptureRegion();
        }

        private void tsbRoundedRectangle_Click(object sender, EventArgs e)
        {
            surface = new RoundedRectangleRegion(screenshot);
            CaptureRegion();
        }

        private void tsbEllipse_Click(object sender, EventArgs e)
        {
            surface = new EllipseRegion(screenshot);
            CaptureRegion();
        }

        private void tsbTriangle_Click(object sender, EventArgs e)
        {
            surface = new TriangleRegion(screenshot);
            CaptureRegion();
        }

        private void tsbDiamond_Click(object sender, EventArgs e)
        {
            surface = new DiamondRegion(screenshot);
            CaptureRegion();
        }

        private void tsbPolygon_Click(object sender, EventArgs e)
        {
            surface = new PolygonRegion(screenshot);
            CaptureRegion();
        }

        private void tsbFreeHand_Click(object sender, EventArgs e)
        {
            surface = new FreeHandRegion(screenshot);
            CaptureRegion();
        }

        private void RegionCapturePreview_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (pbResult.Image != null)
            {
                DialogResult = DialogResult.OK;
            }
            else
            {
                DialogResult = DialogResult.Cancel;
            }
        }

        private void cbDrawBorder_CheckedChanged(object sender, EventArgs e)
        {
            SurfaceConfig.DrawBorder = cbDrawBorder.Checked;
        }

        private void cbDrawChecker_CheckedChanged(object sender, EventArgs e)
        {
            SurfaceConfig.DrawChecker = cbDrawChecker.Checked;
        }

        private void cbIsFixedSize_CheckedChanged(object sender, EventArgs e)
        {
            SurfaceConfig.IsFixedSize = cbIsFixedSize.Checked;
        }

        private void nudFixedWidth_ValueChanged(object sender, EventArgs e)
        {
            SurfaceConfig.FixedSize = new Size((int)nudFixedWidth.Value, SurfaceConfig.FixedSize.Height);
        }

        private void nudFixedHeight_ValueChanged(object sender, EventArgs e)
        {
            SurfaceConfig.FixedSize = new Size(SurfaceConfig.FixedSize.Width, (int)nudFixedHeight.Value);
        }

        private void cbQuickCrop_CheckedChanged(object sender, EventArgs e)
        {
            SurfaceConfig.QuickCrop = cbQuickCrop.Checked;
        }
    }
}
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

namespace RegionCapture
{
    public partial class RegionCapturePreview : Form
    {
        public Image Result { get; private set; }

        private Bitmap screenshot;
        private Surface surface;

        public RegionCapturePreview()
        {
            InitializeComponent();
            screenshot = Helpers.GetScreenshot();
        }

        private void CaptureRegion()
        {
            pbResult.Image = null;

            surface.DrawBorder = cbDrawBorder.Checked;
            surface.DrawChecker = cbDrawChecker.Checked;

            if (surface is RectangleRegion)
            {
                RectangleRegion rectangle = (RectangleRegion)surface;
                if (rectangle.IsFixedSize = cbIsFixedSize.Checked)
                {
                    rectangle.FixedSize = new Size((int)nudFixedWidth.Value, (int)nudFixedHeight.Value);
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
            pbResult.Image = screenshot;
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
            if (Result != null)
            {
                DialogResult = DialogResult.OK;
            }
            else
            {
                DialogResult = DialogResult.Cancel;
            }
        }
    }
}
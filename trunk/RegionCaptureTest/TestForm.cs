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
using RegionCapture;

namespace RegionCaptureTest
{
    public partial class TestForm : Form
    {
        private Bitmap screenshot;
        private Image result;
        private Surface surface;

        public TestForm()
        {
            InitializeComponent();
            screenshot = Helpers.GetScreenshot();
        }

        private Image CreateCheckers(int size, Color color1, Color color2)
        {
            Bitmap bmp = new Bitmap(size * 2, size * 2);

            using (Graphics g = Graphics.FromImage(bmp))
            using (Brush brush1 = new SolidBrush(color1))
            using (Brush brush2 = new SolidBrush(color2))
            {
                g.FillRectangle(brush1, 0, 0, size, size);
                g.FillRectangle(brush1, size, size, size, size);

                g.FillRectangle(brush2, size, 0, size, size);
                g.FillRectangle(brush2, 0, size, size, size);
            }

            return bmp;
        }

        private void CaptureRegion()
        {
            pbResult.Image = null;

            if (surface.ShowDialog() == DialogResult.OK)
            {
                result = surface.GetRegionImage();
                pbResult.Image = result;
                Text = "RegionCapture: " + result.Width + "x" + result.Height;
            }
        }

        private void tsbFreeHand_Click(object sender, EventArgs e)
        {
            surface = new FreeHandRegion(screenshot);
            CaptureRegion();
        }

        private void tsbEllipse_Click(object sender, EventArgs e)
        {
            surface = new EllipseRegion(screenshot);
            CaptureRegion();
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

        private void tsbTriangle_Click(object sender, EventArgs e)
        {
            surface = new TriangleRegion(screenshot);
            CaptureRegion();
        }

        private void tsbPolygon_Click(object sender, EventArgs e)
        {
            surface = new PolygonRegion(screenshot);
            CaptureRegion();
        }

        private void cbShowChecker_CheckedChanged(object sender, EventArgs e)
        {
            if (cbShowChecker.Checked)
            {
                pbResult.BackgroundImage = CreateCheckers(8, Color.LightGray, Color.White);
            }
            else
            {
                pbResult.BackgroundImage = null;
            }
        }
    }
}
﻿#region License Information (GPL v2)

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
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using HelpersLib;

namespace ScreenCapture
{
    public class RoundedRectangleRegion : RectangleRegion
    {
        public float Radius { get; set; }

        public int RadiusIncrement { get; set; }

        public RoundedRectangleRegion(Image backgroundImage = null)
            : base(backgroundImage)
        {
            Radius = 25;
            RadiusIncrement = 3;

            MouseWheel += new MouseEventHandler(RoundedRectangleRegion_MouseWheel);
        }

        private void RoundedRectangleRegion_MouseWheel(object sender, MouseEventArgs e)
        {
            if (e.Delta > 0)
            {
                Radius += RadiusIncrement;
            }
            else if (e.Delta < 0)
            {
                Radius = Math.Max(0, Radius - RadiusIncrement);
            }
        }

        protected override void AddShapePath(GraphicsPath graphicsPath, Rectangle rect)
        {
            graphicsPath.AddRoundedRectangle(new Rectangle(rect.X, rect.Y, rect.Width - 1, rect.Height - 1), Radius);
        }
    }
}
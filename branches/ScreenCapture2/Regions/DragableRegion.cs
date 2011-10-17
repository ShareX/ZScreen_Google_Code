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

using System.Drawing;
using System.Linq;

namespace ScreenCapture
{
    public class DragableRegion : Surface
    {
        protected DrawableObject areaObject;

        public DragableRegion(Image backgroundImage = null)
            : base(backgroundImage)
        {
            areaObject = new DrawableObject { Order = -10 };
            DrawableObjects.Add(areaObject);
        }

        protected override void Update()
        {
            areaObject.Rectangle = CurrentArea;

            base.Update();

            if (areaObject.IsDragging && DrawableObjects.OfType<NodeObject>().All(x => !x.IsDragging && !x.IsMouseHover))
            {
                //AreaManager.MoveCurrentArea(mousePosition.X - oldMousePosition.X, mousePosition.Y - oldMousePosition.Y);
            }
        }
    }
}
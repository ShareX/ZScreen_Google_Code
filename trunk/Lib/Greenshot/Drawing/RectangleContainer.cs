/*
 * Greenshot - a free and open source screenshot tool
 * Copyright (C) 2007-2011  Thomas Braun, Jens Klingen, Robin Krom
 * 
 * For more information see: http://getgreenshot.org/
 * The Greenshot project is hosted on Sourceforge: http://sourceforge.net/projects/greenshot/
 * 
 * This program is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 1 of the License, or
 * (at your option) any later version.
 * 
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 * 
 * You should have received a copy of the GNU General Public License
 * along with this program.  If not, see <http://www.gnu.org/licenses/>.
 */
using System;
using System.Drawing;
using System.Runtime.Serialization;
using System.Windows.Forms;

using Greenshot.Configuration;
using Greenshot.Drawing.Fields;
using Greenshot.Helpers;
using Greenshot.Plugin.Drawing;

namespace Greenshot.Drawing {
	/// <summary>
	/// Represents a rectangular shape on the Surface
	/// </summary>
	[Serializable()] 
	public class RectangleContainer : DrawableContainer {
		private static readonly log4net.ILog LOG = log4net.LogManager.GetLogger(typeof(RectangleContainer));

		public RectangleContainer(Surface parent) : base(parent) {
			AddField(GetType(), FieldType.LINE_THICKNESS, 2);
			AddField(GetType(), FieldType.LINE_COLOR, Color.Red);
			AddField(GetType(), FieldType.FILL_COLOR, Color.Transparent);
			AddField(GetType(), FieldType.SHADOW, false);
		}
		
		
		public override void Draw(Graphics g, RenderMode rm) {
			int lineThickness = GetFieldValueAsInt(FieldType.LINE_THICKNESS);
			Color lineColor = GetFieldValueAsColor(FieldType.LINE_COLOR);
			Color fillColor = GetFieldValueAsColor(FieldType.FILL_COLOR);
			bool shadow = GetFieldValueAsBool(FieldType.SHADOW);
			bool lineVisible = (lineThickness > 0 && Colors.IsVisible(lineColor));
			if (shadow && (lineVisible || Colors.IsVisible(fillColor))) {
				//draw shadow first
				int basealpha = 100;
				int alpha = basealpha;
				int steps = 5;
				int currentStep = lineVisible ? 1 : 0;
				while (currentStep <= steps) {
					using (Pen shadowPen = new Pen(Color.FromArgb(alpha, 100, 100, 100))) {
						shadowPen.Width = lineVisible ? lineThickness : 1;
						Rectangle shadowRect = GuiRectangle.GetGuiRectangle(
							this.Left + currentStep,
							this.Top + currentStep,
							this.Width,
							this.Height);
						g.DrawRectangle(shadowPen, shadowRect);
						currentStep++;
						alpha = alpha - (basealpha / steps);
					}
				}
			}
			
			Rectangle rect = GuiRectangle.GetGuiRectangle(this.Left, this.Top, this.Width, this.Height);
			
			using (Brush brush = new SolidBrush(fillColor)) {
				g.FillRectangle(brush, rect);
			}
			
			if (lineThickness > 0) {
				using (Pen pen = new Pen(lineColor)) {
					pen.Width = lineThickness;
					g.DrawRectangle(pen, rect);
				}
			}
		}
		
	}
}

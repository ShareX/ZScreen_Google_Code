#region License Information (GPL v2)

/*
    ZUploader - A program that allows you to upload images, texts or files
    Copyright (C) 2008-2012 ZScreen Developers

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
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Updater
{
    public class MyButton : Control
    {
        public override string Text
        {
            get
            {
                return text;
            }
            set
            {
                if (value != null && text != value)
                {
                    text = value;

                    Invalidate();
                }
            }
        }

        private string text;

        public MyButton()
        {
            InitializeComponent();
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);

            Graphics g = pe.Graphics;

            DrawBackground(g);

            if (!string.IsNullOrEmpty(Text))
            {
                DrawText(g);
            }
        }

        private void DrawBackground(Graphics g)
        {
            using (LinearGradientBrush backgroundBrush = new LinearGradientBrush(new Rectangle(2, 2, ClientSize.Width - 4, ClientSize.Height - 4),
                Color.FromArgb(105, 105, 105), Color.FromArgb(65, 65, 65), LinearGradientMode.Vertical))
            {
                g.FillRectangle(backgroundBrush, new Rectangle(2, 2, ClientSize.Width - 4, ClientSize.Height - 4));
            }

            using (LinearGradientBrush innerBorderBrush = new LinearGradientBrush(new Rectangle(1, 1, ClientSize.Width - 2, ClientSize.Height - 2),
                Color.FromArgb(125, 125, 125), Color.FromArgb(75, 75, 75), LinearGradientMode.Vertical))
            using (Pen innerBorderPen = new Pen(innerBorderBrush))
            {
                g.DrawRectangle(innerBorderPen, new Rectangle(1, 1, ClientSize.Width - 3, ClientSize.Height - 3));
            }

            using (Pen borderPen = new Pen(Color.FromArgb(30, 30, 30)))
            {
                g.DrawRectangle(borderPen, new Rectangle(0, 0, ClientSize.Width - 1, ClientSize.Height - 1));
            }
        }

        private void DrawText(Graphics g)
        {
            using (Font textFont = new Font("Arial", 12))
            {
                Size textSize = TextRenderer.MeasureText(Text, textFont);
                Point textPosition = new Point(ClientSize.Width / 2 - textSize.Width / 2, ClientSize.Height / 2 - textSize.Height / 2);
                Point textShadowPosition = new Point(textPosition.X, textPosition.Y + 1);

                TextRenderer.DrawText(g, Text, textFont, textShadowPosition, Color.Black);
                TextRenderer.DrawText(g, Text, textFont, textPosition, Color.White);
            }
        }

        #region Component Designer generated code

        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
        }

        #endregion Component Designer generated code
    }
}
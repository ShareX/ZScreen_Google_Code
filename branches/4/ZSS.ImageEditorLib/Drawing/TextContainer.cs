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

#region Source code: Greenshot (GPL)

/*
    This file originated from the Greenshot project (GPL). It may or may not have been modified.
    Please do not contact Greenshot about errors with this code. Instead contact the creators of this program.
    URL: http://greenshot.sourceforge.net/
    Code (CVS): http://greenshot.cvs.sourceforge.net/viewvc/greenshot/
*/

#endregion Source code: Greenshot (GPL)

using System;
using System.Drawing;
using System.Drawing.Text;
using System.Runtime.Serialization;
using System.Windows.Forms;
using Greenshot.Helpers;

namespace Greenshot.Drawing
{
    [Serializable()]
    public class TextContainer : DrawableContainer
    {
        public TextContainer(Control parent)
            : base(parent)
        {
            supportedProperties.Add(DrawableContainer.Property.LINECOLOR);
        }

        #region Serialization

        public TextContainer(SerializationInfo info, StreamingContext ctxt)
            : base(info, ctxt)
        {
            childLabel = new Label();
            childLabel.Text = (string)info.GetValue("Text", typeof(string));
            childLabel.Font = (Font)info.GetValue("Font", typeof(Font));
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext ctxt)
        {
            base.GetObjectData(info, ctxt);
            info.AddValue("Text", childLabel.Text);
            info.AddValue("Font", childLabel.Font);
        }

        #endregion Serialization

        public override Color ForeColor
        {
            set { foreColor = childLabel.ForeColor = value; }
            get { return foreColor; }
        }

        public string Text
        {
            set { childLabel.Text = value; }
            get { return childLabel.Text; }
        }

        public override bool InitContent()
        {
            return ShowTextInput(true);
        }

        public override void OnDoubleClick()
        {
            ShowTextInput(false);
        }

        private bool ShowTextInput(bool isNew)
        {
            TextInputForm textInput = new TextInputForm();
            if (!isNew)
            {
                textInput.UpdateFromLabel(childLabel);
            }
            textInput.InputText.ForeColor = childLabel.ForeColor;
            if (textInput.ShowDialog(parent) == DialogResult.OK && textInput.InputText.Text.Length > 0)
            {
                childLabel.Text = textInput.InputText.Text;
                childLabel.Font = textInput.InputText.Font;
                ForeColor = textInput.InputText.ForeColor;
                parent.Invalidate();
                return true;
            }
            return false;
        }

        public override void Draw(Graphics g, RenderMode rm)
        {
            g.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;
            Rectangle rect = GuiRectangle.GetGuiRectangle(this.Left, this.Top, this.Width, this.Height);
            if (Selected && rm.Equals(RenderMode.EDIT)) DrawSelectionBorder(g, rect);
            Brush fontBrush = new SolidBrush(foreColor);
            g.DrawString(childLabel.Text, childLabel.Font, fontBrush, rect);
        }
    }
}
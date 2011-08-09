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

/*
 * Erstellt mit SharpDevelop.
 * Benutzer: thomas
 * Datum: 30.03.2007
 * Zeit: 19:53
 *
 * Sie k�nnen diese Vorlage unter Extras > Optionen > Codeerstellung > Standardheader �ndern.
 */

using System;
using System.Drawing;
using System.Windows.Forms;
using Greenshot.Configuration;

namespace Greenshot
{
    public partial class TextInputForm : Form
    {
        public TextInputForm()
        {
            //
            // The InitializeComponent() call is required for Windows Forms designer support.
            //
            InitializeComponent();

            FontFamily[] fontFamilies = FontFamily.Families;
            for (int i = 0; i < fontFamilies.Length; i++)
            {
                FontFamily family = fontFamilies[i];
                if (family.IsStyleAvailable(FontStyle.Regular))
                {
                    int n = comboFonts.Items.Add(family.Name);
                    if (InputText.Font.FontFamily.Equals(family))
                    {
                        comboFonts.SelectedIndex = n;
                    }
                }
            }
            int number;
            for (int i = 0; i <= 15; i++)
            {
                number = (i + 1) * 3 + 5;
                comboFontSize.Items.Add(number.ToString());
                if (InputText.Font.Size == (float)number)
                {
                    comboFontSize.SelectedIndex = i;
                }
            }
            btnBold.Checked = InputText.Font.Bold;
            btnItalic.Checked = InputText.Font.Italic;
            btnUnderline.Checked = InputText.Font.Underline;

            Font font = AppConfig.GetInstance().Editor_Font;
            if (font != null)
            {
                updateFromFont(font);
            }
        }

        private void BtnCancelClick(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            InputText.Text = "";
            this.Hide();
        }

        private void BtnOkClick(object sender, EventArgs e)
        {
            AppConfig.GetInstance().Editor_Font = InputText.Font;
            AppConfig.GetInstance().Save();
            this.DialogResult = DialogResult.OK;
            this.Hide();
        }

        private void setFontStyle()
        {
            FontStyle style = FontStyle.Regular;
            if (btnBold.Checked) style |= FontStyle.Bold;
            if (btnItalic.Checked) style |= FontStyle.Italic;
            if (btnUnderline.Checked) style |= FontStyle.Underline;
            if (InputText.Font.FontFamily.IsStyleAvailable(style))
            {
                InputText.Font = new Font(InputText.Font, style);
            }
        }

        public void UpdateFromLabel(Label label)
        {
            updateFromFont(label.Font);
            InputText.ForeColor = label.ForeColor;
            InputText.Text = label.Text;
        }

        private void updateFromFont(Font font)
        {
            comboFonts.Text = font.Name;
            comboFontSize.Text = font.Size.ToString();
            btnBold.Checked = font.Bold;
            btnItalic.Checked = font.Italic;
            btnUnderline.Checked = font.Underline;
            InputText.Font = font;
        }

        private void BtnBoldClick(object sender, EventArgs e)
        {
            setFontStyle();
        }

        private void BtnItalicClick(object sender, EventArgs e)
        {
            setFontStyle();
        }

        private void BtnUnderlineClick(object sender, EventArgs e)
        {
            setFontStyle();
        }

        private void ComboFontsSelectedIndexChanged(object sender, EventArgs e)
        {
            InputText.Font = new Font(comboFonts.SelectedItem.ToString(), InputText.Font.Size);
            setFontStyle();
        }

        private void ComboFontSizeTextChanged(object sender, EventArgs e)
        {
            int result = 0;
            if (int.TryParse(comboFontSize.Text, out result))
            {
                InputText.Font = new Font(InputText.Font.FontFamily, int.Parse(comboFontSize.Text));
                setFontStyle();
            }
        }

        private void BtnColorClick(object sender, System.EventArgs e)
        {
            ColorDialog colorDialog = ColorDialog.GetInstance();
            colorDialog.ShowDialog();
            if (colorDialog.DialogResult != DialogResult.Cancel)
            {
                InputText.ForeColor = colorDialog.Color;
            }
        }

        private void InputTextKeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.Modifiers.Equals(Keys.Control) && e.KeyCode == Keys.Return)
            {
                this.BtnOkClick(sender, e);
            }
        }
    }
}
#region License Information (GPL v2)

/*
    ZScreen - A program that allows you to upload screenshots in one keystroke.
    Copyright (C) 2008-2009  Brandon Zimmerman

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
 * Benutzer: jens
 * Datum: 11.02.2008
 * Zeit: 22:53
 *
 * Sie können diese Vorlage unter Extras > Optionen > Codeerstellung > Standardheader ändern.
 */

using System;
using System.Windows.Forms;
using Greenshot.Configuration;

namespace Greenshot.Forms
{
    /// <summary>
    /// Description of PrintOptionsDialog.
    /// </summary>
    public partial class PrintOptionsDialog : Form
    {
        AppConfig conf;

        public bool AllowPrintCenter, AllowPrintEnlarge, AllowPrintRotate, AllowPrintShrink;

        public PrintOptionsDialog()
        {
            //
            // The InitializeComponent() call is required for Windows Forms designer support.
            //
            InitializeComponent();

            //
            // TODO: Add constructor code after the InitializeComponent() call.
            //

            conf = AppConfig.GetInstance();

            this.AllowPrintCenter = this.checkboxAllowCenter.Checked = (bool)conf.Output_Print_Center;
            this.AllowPrintEnlarge = this.checkboxAllowEnlarge.Checked = (bool)conf.Output_Print_AllowEnlarge;
            this.AllowPrintRotate = this.checkboxAllowRotate.Checked = (bool)conf.Output_Print_AllowRotate;
            this.AllowPrintShrink = this.checkboxAllowShrink.Checked = (bool)conf.Output_Print_AllowShrink;
            this.checkbox_dontaskagain.Checked = false;
        }

        private void Button_okClick(object sender, EventArgs e)
        {
            this.AllowPrintCenter = this.checkboxAllowCenter.Checked;
            this.AllowPrintEnlarge = this.checkboxAllowEnlarge.Checked;
            this.AllowPrintRotate = this.checkboxAllowRotate.Checked;
            this.AllowPrintShrink = this.checkboxAllowShrink.Checked;
            if (this.checkbox_dontaskagain.Checked)
            {
                conf.Output_Print_Center = (bool?)this.AllowPrintCenter;
                conf.Output_Print_AllowEnlarge = (bool?)this.AllowPrintEnlarge;
                conf.Output_Print_AllowRotate = (bool?)this.AllowPrintRotate;
                conf.Output_Print_AllowShrink = (bool?)this.AllowPrintShrink;
                conf.Output_Print_PromptOptions = false;
                conf.Save();
            }
        }
    }
}
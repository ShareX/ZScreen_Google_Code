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
#endregion

#region Source code: Greenshot (GPL)
/*
    This file originated from the Greenshot project (GPL). It may or may not have been modified.
    Please do not contact Greenshot about errors with this code. Instead contact the creators of this program.
    URL: http://greenshot.sourceforge.net/
    Code (CVS): http://greenshot.cvs.sourceforge.net/viewvc/greenshot/
*/
#endregion

/*
 * Erstellt mit SharpDevelop.
 * Benutzer: thomas
 * Datum: 21.04.2007
 * Zeit: 17:05
 * 
 * Sie können diese Vorlage unter Extras > Optionen > Codeerstellung > Standardheader ändern.
 */

using System;
using System.Drawing;
using System.Windows.Forms;
using System.Reflection;

namespace Greenshot
{
	/// <summary>
	/// Description of AboutForm.
	/// </summary>
	public partial class AboutForm : Form
	{	
		public AboutForm()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			Version v = Assembly.GetExecutingAssembly().GetName().Version;
			lblTitle.Text = "Greenshot " + v.Major + "." + v.Minor + "." + v.Build.ToString("000");	
		}

        private void AboutForm_Load(object sender, EventArgs e)
        {
            this.Text = "About Image Editor";
        }
	}
}
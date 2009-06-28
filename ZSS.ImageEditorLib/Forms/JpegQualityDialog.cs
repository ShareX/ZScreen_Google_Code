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
 * Created by SharpDevelop.
 * User: jens
 * Date: 19.07.2007
 * Time: 21:37
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */

using System;
using System.Drawing;
using System.Windows.Forms;
using Greenshot.Configuration;

namespace Greenshot
{
    /// <summary>
    /// Description of JpegQualityDialog.
    /// </summary>
    public partial class JpegQualityDialog : Form
    {
        AppConfig conf;
        public int Quality = 0;
        public JpegQualityDialog()
        {
            //
            // The InitializeComponent() call is required for Windows Forms designer support.
            //
            InitializeComponent();

            conf = AppConfig.GetInstance();

            this.trackBarJpegQuality.Value = conf.Output_File_JpegQuality;
            this.textBoxJpegQuality.Text = conf.Output_File_JpegQuality.ToString();

        }

        private void Button_okClick(object sender, System.EventArgs e)
        {
            Quality = this.trackBarJpegQuality.Value;
            if (this.checkbox_dontaskagain.Checked)
            {
                conf.Output_File_JpegQuality = Quality;
                conf.Output_File_PromptJpegQuality = false;
                conf.Save();
            }

        }

        private void TrackBarJpegQualityScroll(object sender, System.EventArgs e)
        {
            textBoxJpegQuality.Text = trackBarJpegQuality.Value.ToString();
        }
    }
}
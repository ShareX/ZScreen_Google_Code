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

using System;
using System.Drawing;
using System.Windows.Forms;
using System.Net;
using System.Diagnostics;

namespace ZSS.UpdateCheckerLib
{
    public partial class NewVersionWindow : Form
    {
        [System.Runtime.InteropServices.DllImport("user32")]
        private static extern bool SetForegroundWindow(int hWnd);
        private NewVersionWindowOptions Options { get; set; }

        public NewVersionWindow(NewVersionWindowOptions options)
        {
            InitializeComponent();
            this.Options = options;

            if (this.Options.MyIcon != null)
                this.Icon = this.Options.MyIcon;
            if (this.Options.MyImage != null)
                this.pbApp.Image = this.Options.MyImage;

            this.lblVer.Text = this.Options.Question;
            if (!string.IsNullOrEmpty(this.Options.VersionHistory) &&
                 this.Options.VersionHistory.StartsWith("http://" + this.Options.ProjectName + ".googlecode.com/"))
            {
                WebClient wClient = new WebClient();
                string versionHistory = wClient.DownloadString(this.Options.VersionHistory);
                if (!string.IsNullOrEmpty(versionHistory))
                {
                    this.txtVer.Text = versionHistory;
                }
            }
            else
            {
                this.txtVer.Text = this.Options.VersionHistory;
            }
            SetForegroundWindow(this.Handle.ToInt32());
        }

        private void btnYes_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Yes;
            this.Close();
        }

        private void btnNo_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.No;
            this.Close();
        }
        
        void TxtVerLinkClicked(object sender, LinkClickedEventArgs e)
        {
        	Process.Start(e.LinkText);
        }
    }

    public class NewVersionWindowOptions
    {
        public Icon MyIcon { get; set; }
        public Image MyImage { get; set; }
        public string VersionHistory { get; set; }
        public string Question { get; set; }
        public string ProjectName { get; set; }
    }
}
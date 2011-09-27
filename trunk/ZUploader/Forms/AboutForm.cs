#region License Information (GPL v2)

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
using System.Reflection;
using System.Windows.Forms;
using HelpersLib;
using UploadersAPILib;
using UploadersLib;
using ZSS.UpdateCheckerLib;

namespace ZUploader
{
    public partial class AboutForm : Form
    {
        public AboutForm()
        {
            InitializeComponent();
            Text = Program.Title;
            lblProductName.Text = Program.Title;
            lblCopyright.Text = AssemblyCopyright;

            UpdateChecker updateChecker = new UpdateChecker(ZLinks.URL_UPDATE, Application.ProductName, new Version(Application.ProductVersion),
                ReleaseChannelType.Stable, Uploader.ProxySettings.GetWebProxy);
            updateChecker.AutoDownloadSummary = false;
            uclUpdate.CheckUpdate(updateChecker);
        }

        private void AboutForm_Shown(object sender, EventArgs e)
        {
            this.BringToFront();
            this.Activate();
        }

        public string AssemblyCopyright
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false);
                if (attributes.Length == 0)
                {
                    return string.Empty;
                }
                return ((AssemblyCopyrightAttribute)attributes[0]).Copyright;
            }
        }

        private void lblZScreen_Click(object sender, EventArgs e)
        {
            StaticHelper.LoadBrowser(ZLinks.URL_WEBSITE);
        }

        private void lblBugs_Click(object sender, EventArgs e)
        {
            StaticHelper.LoadBrowser(ZLinks.URL_ISSUES);
        }

        private void pbBerkURL_Click(object sender, EventArgs e)
        {
            StaticHelper.LoadBrowser(ZLinks.URL_BERK);
        }

        private void pbMikeURL_Click(object sender, EventArgs e)
        {
            StaticHelper.LoadBrowser(ZLinks.URL_MIKE);
        }

        private void pbBrandonURL_Click(object sender, EventArgs e)
        {
            StaticHelper.LoadBrowser(ZLinks.URL_BRANDON);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
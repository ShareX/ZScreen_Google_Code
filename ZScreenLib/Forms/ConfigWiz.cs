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
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using ZSS;
using UploadersLib;

namespace ZScreenLib
{
    public partial class ConfigWizard : Form
    {
        public string RootFolder { get; private set; }
        public ImageDestType ImageDestinationType { get; private set; }

        public ConfigWizard(string rootDir)
        {
            InitializeComponent();
            txtRootFolder.Text = rootDir;
            this.RootFolder = rootDir;
            cboScreenshotDest.Items.AddRange(typeof(ImageDestType).GetDescriptions());
            cboScreenshotDest.SelectedIndex = (int)ImageDestType.IMAGESHACK;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnBrowseRootDir_Click(object sender, EventArgs e)
        {
            string oldDir = txtRootFolder.Text;
            FolderBrowserDialog dlg = new FolderBrowserDialog { ShowNewFolderButton = true };
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                txtRootFolder.Text = dlg.SelectedPath;
                RootFolder = txtRootFolder.Text;
            }
            FileSystem.MoveDirectory(oldDir, txtRootFolder.Text);
        }

        private void btnViewRootDir_Click(object sender, EventArgs e)
        {
            if (Directory.Exists(txtRootFolder.Text))
            {
                Process.Start(txtRootFolder.Text);
            }
        }

        private void cboScreenshotDest_SelectedIndexChanged(object sender, EventArgs e)
        {
            ImageDestinationType = (ImageDestType)cboScreenshotDest.SelectedIndex;
        }
    }
}
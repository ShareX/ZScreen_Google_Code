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

using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Windows.Forms;
using UploadersLib;

namespace ZScreenLib
{
    public partial class ConfigWizard : DavuxLib.Controls.GlassForm
    {
        public bool PreferSystemFolders { get; private set; }
        public string RootFolder { get; private set; }
        public ImageUploaderType ImageDestinationType { get; private set; }
        public FileUploaderType FileUploaderType { get; private set; }
        public TextUploaderType MyTextUploaderType { get; private set; }
        public UrlShortenerType MyUrlShortenerType { get; private set; }
        private string DefaultRootFolder;

        public ConfigWizard(string rootDir)
        {
            InitializeComponent();
            this.Text = string.Format("ZScreen {0} - Configuration Wizard", Application.ProductVersion);
            DefaultRootFolder = rootDir;
            txtRootFolder.Text = chkPreferSystemFolders.Checked ? Engine.zRoamingAppDataFolder : rootDir;
            this.RootFolder = rootDir;

            ucDestOptions.cboImageUploaders.Items.AddRange(typeof(ImageUploaderType).GetDescriptions());
            ucDestOptions.cboImageUploaders.SelectedIndex = (int)ImageUploaderType.CLIPBOARD;

            ucDestOptions.cboFileUploaders.Items.AddRange(typeof(FileUploaderType).GetDescriptions());
            ucDestOptions.cboFileUploaders.SelectedIndex = (int)FileUploaderType.SendSpace;

            ucDestOptions.cboTextUploaders.Items.AddRange(typeof(TextUploaderType).GetDescriptions());
            ucDestOptions.cboTextUploaders.SelectedIndex = (int)TextUploaderType.PASTE2;

            ucDestOptions.cboURLShorteners.Items.AddRange(typeof(UrlShortenerType).GetDescriptions());
            ucDestOptions.cboURLShorteners.SelectedIndex = (int)UrlShortenerType.Google;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            PreferSystemFolders = chkPreferSystemFolders.Checked;
            RootFolder = txtRootFolder.Text;

            FileUploaderType = (FileUploaderType)ucDestOptions.cboFileUploaders.SelectedIndex;
            ImageDestinationType = (ImageUploaderType)ucDestOptions.cboImageUploaders.SelectedIndex;
            MyTextUploaderType = (TextUploaderType)ucDestOptions.cboTextUploaders.SelectedIndex;
            MyUrlShortenerType = (UrlShortenerType)ucDestOptions.cboURLShorteners.SelectedIndex;

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnBrowseRootDir_Click(object sender, EventArgs e)
        {
            string oldDir = txtRootFolder.Text;
            string newDir = Adapter.GetDirPathUsingFolderBrowser("Configure Root directory...");
            if (!string.IsNullOrEmpty(newDir))
            {
                txtRootFolder.Text = newDir;
                FileSystem.MoveDirectory(oldDir, txtRootFolder.Text);
            }
        }

        private void btnViewRootDir_Click(object sender, EventArgs e)
        {
            if (Directory.Exists(txtRootFolder.Text))
            {
                Process.Start(txtRootFolder.Text);
            }
        }

        private void chkPreferSystemFolders_CheckedChanged(object sender, EventArgs e)
        {
            PreferSystemFolders = chkPreferSystemFolders.Checked;
            txtRootFolder.Text = PreferSystemFolders ? Engine.zRoamingAppDataFolder : DefaultRootFolder;
            gbRoot.Enabled = !PreferSystemFolders;
        }

        private void ConfigWizard_Load(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(string.Format("If enabled {0} will create the data folders at the following locations:", Application.ProductName));
            sb.AppendLine();
            sb.AppendLine(string.Format("Settings:\t{0}\\{1}\\Settings", Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), Application.ProductName));
            sb.AppendLine(string.Format("Images:\t{0}\\{1}", Environment.GetFolderPath(Environment.SpecialFolder.MyPictures), Application.ProductName));
            sb.AppendLine(string.Format("Text:\t{0}\\{1}", Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), Application.ProductName));
            sb.AppendLine(string.Format("Logs:\t{0}\\{1}\\Logs", Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), Application.ProductName));
            ttApp.SetToolTip(chkPreferSystemFolders, sb.ToString());
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void ConfigWizard_Shown(object sender, EventArgs e)
        {
            chkPreferSystemFolders.Checked = true;
        }
    }
}
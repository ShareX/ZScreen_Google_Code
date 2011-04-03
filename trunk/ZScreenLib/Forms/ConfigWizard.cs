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
using System.Collections.Generic;

namespace ZScreenLib
{
    public partial class ConfigWizard : DavuxLib.Controls.GlassForm
    {
        public bool PreferSystemFolders { get; private set; }
        public string RootFolder { get; private set; }
        public ImageDestType ImageDestinationType { get; private set; }
        public FileUploaderType FileUploaderType { get; private set; }
        public int TextUploaderType { get; private set; }
        public int UrlShortenerType { get; private set; }

        public ConfigWizard(string rootDir)
        {
            InitializeComponent();
            this.Text = string.Format("ZScreen {0} - Configuration Wizard", Application.ProductVersion);
            txtRootFolder.Text = rootDir;
            this.RootFolder = rootDir;

            ucDestOptions.cboImageUploaders.Items.AddRange(typeof(ImageDestType).GetDescriptions());
            ucDestOptions.cboImageUploaders.SelectedIndex = (int)ImageDestType.CLIPBOARD;

            ucDestOptions.cboFileUploaders.Items.AddRange(typeof(FileUploaderType).GetDescriptions());
            ucDestOptions.cboFileUploaders.SelectedIndex = (int)FileUploaderType.SendSpace;

            foreach (TextDestType etu in Enum.GetValues(typeof(TextDestType)))
            {
                TextUploader tu = Adapter.FindTextUploader(etu.GetDescription());
                if (null != tu)
                {
                    ucDestOptions.cboTextUploaders.Items.Add(tu);
                }
            }
            ucDestOptions.cboTextUploaders.SelectedIndex = 0;

            foreach (UrlShortenerType etu in Enum.GetValues(typeof(UrlShortenerType)))
            {
                TextUploader tu = Adapter.FindUrlShortener(etu.GetDescription());
                if (null != tu)
                {
                    ucDestOptions.cboURLShorteners.Items.Add(tu);
                }
            }
            ucDestOptions.cboURLShorteners.SelectedIndex = 0;

        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            FileUploaderType = (FileUploaderType)ucDestOptions.cboFileUploaders.SelectedIndex;
            ImageDestinationType = (ImageDestType)ucDestOptions.cboImageUploaders.SelectedIndex;
            TextUploaderType = ucDestOptions.cboTextUploaders.SelectedIndex;
            UrlShortenerType = ucDestOptions.cboURLShorteners.SelectedIndex;

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
                RootFolder = txtRootFolder.Text;
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
            gbRoot.Enabled = !chkPreferSystemFolders.Checked;
            this.PreferSystemFolders = chkPreferSystemFolders.Checked;
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
    }
}
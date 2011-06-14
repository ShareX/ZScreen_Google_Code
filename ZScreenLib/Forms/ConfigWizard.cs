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
using System.Collections.Generic;
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
        public List<int> MyImageDestinationTypes = new List<int>();
        public FileUploaderType FileUploaderType { get; private set; }
        public List<int> MyTextDestinationTypes = new List<int>();
        public UrlShortenerType MyUrlShortenerType { get; private set; }
        private string DefaultRootFolder;

        public ConfigWizard(string rootDir)
        {
            InitializeComponent();
            this.Text = string.Format("ZScreen {0} - Configuration Wizard", Application.ProductVersion);
            DefaultRootFolder = rootDir;
            txtRootFolder.Text = chkPreferSystemFolders.Checked ? Engine.zRoamingAppDataFolder : rootDir;
            this.RootFolder = rootDir;

            if (ucDestOptions.tsddDestImage.DropDownItems.Count == 0)
            {
                foreach (ImageUploaderType t in Enum.GetValues(typeof(ImageUploaderType)))
                {
                    ToolStripMenuItem tsmi = new ToolStripMenuItem(t.GetDescription());
                    tsmi.Tag = t;
                    ucDestOptions.tsddDestImage.DropDownItems.Add(tsmi);
                }
            }

            ucDestOptions.cboFileUploaders.Items.AddRange(typeof(FileUploaderType).GetDescriptions());
            ucDestOptions.cboFileUploaders.SelectedIndex = (int)FileUploaderType.SendSpace;

            if (ucDestOptions.tsddDestText.DropDownItems.Count == 0)
            {
                foreach (TextUploaderType t in Enum.GetValues(typeof(TextUploaderType)))
                {
                    ToolStripMenuItem tsmi = new ToolStripMenuItem(t.GetDescription());
                    tsmi.Tag = t;
                    ucDestOptions.tsddDestText.DropDownItems.Add(tsmi);
                }
            }

            ucDestOptions.cboURLShorteners.Items.AddRange(typeof(UrlShortenerType).GetDescriptions());
            ucDestOptions.cboURLShorteners.SelectedIndex = (int)UrlShortenerType.Google;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            PreferSystemFolders = chkPreferSystemFolders.Checked;
            RootFolder = txtRootFolder.Text;

            FileUploaderType = (FileUploaderType)ucDestOptions.cboFileUploaders.SelectedIndex;

            foreach (ToolStripMenuItem tsmi in ucDestOptions.tsddDestImage.DropDownItems)
            {
                if (tsmi.Checked)
                {
                    MyImageDestinationTypes.Add((int)tsmi.Tag);
                }
            }

            foreach (ToolStripMenuItem tsmi in ucDestOptions.tsddDestText.DropDownItems)
            {
                if (tsmi.Checked)
                {
                    MyTextDestinationTypes.Add((int)tsmi.Tag);
                }
            }

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
            sb.AppendLine(string.Format("Images:\t\t{0}\\{1}", Environment.GetFolderPath(Environment.SpecialFolder.MyPictures), Application.ProductName));
            sb.AppendLine(string.Format("Text:\t\t{0}\\{1}", Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), Application.ProductName));
            sb.AppendLine(string.Format("Logs:\t\t{0}\\{1}\\Logs", Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), Application.ProductName));
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

        private void ucDestOptions_Load(object sender, EventArgs e)
        {
        }
    }
}
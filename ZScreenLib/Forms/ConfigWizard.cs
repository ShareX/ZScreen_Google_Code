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

        public List<int> MyClipboardContent = new List<int>();
        public List<int> MyLinkFormat = new List<int>();
        public List<int> MyImageUploaders = new List<int>();
        public List<int> MyFileUploaders = new List<int>();
        public List<int> MyTextUploaders = new List<int>();
        public List<int> MyLinkUploaders = new List<int>();
        private string DefaultRootFolder;

        public ConfigWizard(string rootDir)
        {
            InitializeComponent();
            this.Text = string.Format("ZScreen {0} - Configuration Wizard", Application.ProductVersion);
            DefaultRootFolder = rootDir;
            txtRootFolder.Text = chkPreferSystemFolders.Checked ? Engine.zRoamingAppDataFolder : rootDir;
            this.RootFolder = rootDir;

            DestSelectorHelper dsh = new DestSelectorHelper(ucDestOptions);
            MyClipboardContent.Add((int)ClipboardContentEnum.Data);
            MyLinkFormat.Add((int)LinkFormatEnum.FULL);
            MyImageUploaders.Add((int)ImageUploaderType.IMGUR);
            MyTextUploaders.Add((int)TextUploaderType.PASTE2);
            MyFileUploaders.Add((int)FileUploaderType.SendSpace);
            MyLinkUploaders.Add((int)UrlShortenerType.Google);

            dsh.AddEnumClipboardContentWithRuntimeSettings(MyClipboardContent);
            dsh.AddEnumLinkFormatWithRuntimeSettings(MyLinkFormat);
            dsh.AddEnumDestImageToMenuWithRuntimeSettings(MyImageUploaders);
            dsh.AddEnumDestTextToMenuWithRuntimeSettings(MyTextUploaders);
            dsh.AddEnumDestFileToMenuWithRuntimeSettings(MyFileUploaders);
            dsh.AddEnumDestLinkToMenuWithRuntimeSettings(MyLinkUploaders);
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            PreferSystemFolders = chkPreferSystemFolders.Checked;
            RootFolder = txtRootFolder.Text;

            Adapter.SaveMenuConfigToList(ucDestOptions.tsddbClipboardContent, MyClipboardContent);
            Adapter.SaveMenuConfigToList(ucDestOptions.tsddbLinkFormat, MyLinkFormat);
            Adapter.SaveMenuConfigToList(ucDestOptions.tsddDestFile, MyFileUploaders);
            Adapter.SaveMenuConfigToList(ucDestOptions.tsddbDestImage, MyImageUploaders);
            Adapter.SaveMenuConfigToList(ucDestOptions.tsddDestText, MyTextUploaders);
            Adapter.SaveMenuConfigToList(ucDestOptions.tsddbDestLink, MyLinkUploaders);

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
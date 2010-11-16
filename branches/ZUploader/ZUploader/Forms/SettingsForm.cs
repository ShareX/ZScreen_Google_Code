#region License Information (GPL v2)

/*
    ZUploader - A program that allows you to upload images, texts or files
    Copyright (C) 2010 ZScreen Developers

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
using System.IO;
using System.Windows.Forms;

namespace ZUploader
{
    public partial class SettingsForm : Form
    {
        private bool loaded;

        public SettingsForm()
        {
            InitializeComponent();
            LoadSettings();
        }

        private void SettingsForm_Load(object sender, EventArgs e)
        {
            this.BringToFront();
            loaded = true;
        }

        private void LoadSettings()
        {
            cbClipboardAutoCopy.Checked = Program.Settings.ClipboardAutoCopy;
            cbAutoPlaySound.Checked = Program.Settings.AutoPlaySound;
            cbShellContextMenu.Checked = ShellContextMenu.Check();

            cbImageFormat.SelectedIndex = (int)Program.Settings.ImageFormat;
            nudImageJPEGQuality.Value = Program.Settings.ImageJPEGQuality;
            cbImageGIFQuality.SelectedIndex = (int)Program.Settings.ImageGIFQuality;
            nudUseImageFormat2After.Value = Program.Settings.ImageSizeLimit;
            cbImageFormat2.SelectedIndex = (int)Program.Settings.ImageFormat2;

            cbHistorySave.Checked = Program.Settings.SaveHistory;
            cbUseCustomHistoryPath.Checked = Program.Settings.UseCustomHistoryPath;
            txtCustomHistoryPath.Text = Program.Settings.CustomHistoryPath;

            pgFTPSettings.SelectedObject = Program.Settings.FTPAccount;
            pgProxy.SelectedObject = Program.Settings.ProxySettings;
        }

        #region General

        private void cbClipboardAutoCopy_CheckedChanged(object sender, EventArgs e)
        {
            Program.Settings.ClipboardAutoCopy = cbClipboardAutoCopy.Checked;
        }

        private void cbAutoPlaySound_CheckedChanged(object sender, EventArgs e)
        {
            Program.Settings.AutoPlaySound = cbAutoPlaySound.Checked;
        }

        private void cbShellContextMenu_CheckedChanged(object sender, EventArgs e)
        {
            if (loaded)
            {
                if (cbShellContextMenu.Checked)
                {
                    ShellContextMenu.Register();
                }
                else
                {
                    ShellContextMenu.Unregister();
                }
            }
        }

        #endregion General

        #region Image

        private void cbImageFormat_SelectedIndexChanged(object sender, EventArgs e)
        {
            Program.Settings.ImageFormat = (EImageFormat)cbImageFormat.SelectedIndex;
        }

        private void nudImageJPEGQuality_ValueChanged(object sender, EventArgs e)
        {
            Program.Settings.ImageJPEGQuality = (int)nudImageJPEGQuality.Value;
        }

        private void cbImageGIFQuality_SelectedIndexChanged(object sender, EventArgs e)
        {
            Program.Settings.ImageGIFQuality = (GIFQuality)cbImageGIFQuality.SelectedIndex;
        }

        private void btnImageGIFQualityHint_Click(object sender, EventArgs e)
        {
            // TODO: GIF Quality Hint
        }

        private void nudUseImageFormat2After_ValueChanged(object sender, EventArgs e)
        {
            Program.Settings.ImageSizeLimit = (int)nudUseImageFormat2After.Value;
        }

        private void cbImageFormat2_SelectedIndexChanged(object sender, EventArgs e)
        {
            Program.Settings.ImageFormat2 = (EImageFormat)cbImageFormat2.SelectedIndex;
        }

        #endregion Image

        #region History

        private void cbHistorySave_CheckedChanged(object sender, EventArgs e)
        {
            Program.Settings.SaveHistory = cbHistorySave.Checked;
        }

        private void cbUseCustomHistoryPath_CheckedChanged(object sender, EventArgs e)
        {
            Program.Settings.UseCustomHistoryPath = cbUseCustomHistoryPath.Checked;
        }

        private void txtCustomHistoryPath_TextChanged(object sender, EventArgs e)
        {
            Program.Settings.CustomHistoryPath = txtCustomHistoryPath.Text;
        }

        private void btnBrowseCustomHistoryPath_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Title = "ZUploader - Custom history file path";

                try
                {
                    string text = txtCustomHistoryPath.Text;
                    if (!string.IsNullOrEmpty(text) && Directory.Exists(text = Path.GetDirectoryName(text)))
                    {
                        ofd.InitialDirectory = text;
                    }
                }
                finally
                {
                    if (string.IsNullOrEmpty(ofd.InitialDirectory))
                    {
                        ofd.InitialDirectory = Program.ZUploaderPersonalPath;
                    }
                }

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    txtCustomHistoryPath.Text = ofd.FileName;
                }
            }
        }

        #endregion History

        #region FTP

        private void pgFTPSettings_SelectedObjectsChanged(object sender, EventArgs e)
        {
            pgFTPSettings.SelectedObject = Program.Settings.FTPAccount;
        }

        #endregion FTP
    }
}
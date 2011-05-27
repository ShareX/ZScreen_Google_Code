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
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using HelpersLib;

namespace ZUploader
{
    public partial class SettingsForm : Form
    {
        private const int MaxBufferSizePower = 12;

        private bool loaded;
        private ContextMenuStrip codesMenu;

        public SettingsForm()
        {
            InitializeComponent();
            LoadSettings();
            loaded = true;
        }

        private void LoadSettings()
        {
            // General
            cbClipboardAutoCopy.Checked = Program.Settings.ClipboardAutoCopy;
            cbAutoPlaySound.Checked = Program.Settings.AutoPlaySound;
            cbURLShortenAfterUpload.Checked = Program.Settings.URLShortenAfterUpload;
            cbShellContextMenu.Checked = ShellContextMenu.Check();

            // Upload
            cbUseCustomUploadersConfigPath.Checked = Program.Settings.UseCustomUploadersConfigPath;
            txtCustomUploadersConfigPath.Text = Program.Settings.CustomUploadersConfigPath;
            nudUploadLimit.Value = Program.Settings.UploadLimit;

            for (int i = 0; i < MaxBufferSizePower; i++)
            {
                cbBufferSize.Items.Add(Math.Pow(2, i).ToString("N0"));
            }

            cbBufferSize.SelectedIndex = Program.Settings.BufferSizePower.Between(0, MaxBufferSizePower);

            // Image
            cbImageFormat.SelectedIndex = (int)Program.Settings.ImageFormat;
            nudImageJPEGQuality.Value = Program.Settings.ImageJPEGQuality;
            cbImageGIFQuality.SelectedIndex = (int)Program.Settings.ImageGIFQuality;
            nudUseImageFormat2After.Value = Program.Settings.ImageSizeLimit;
            cbImageFormat2.SelectedIndex = (int)Program.Settings.ImageFormat2;

            // Clipboard upload
            txtNameFormatPattern.Text = Program.Settings.NameFormatPattern;
            CreateCodesMenu();

            // History
            cbHistorySave.Checked = Program.Settings.SaveHistory;
            cbUseCustomHistoryPath.Checked = Program.Settings.UseCustomHistoryPath;
            txtCustomHistoryPath.Text = Program.Settings.CustomHistoryPath;
            nudHistoryMaxItemCount.Value = Program.Settings.HistoryMaxItemCount;

            // Proxy
            pgProxy.SelectedObject = Program.Settings.ProxySettings;

            // Debug
            txtDebugLog.Text = Program.MyLogger.Messages.ToString();
            txtDebugLog.ScrollToCaret();
        }

        private void SettingsForm_Shown(object sender, EventArgs e)
        {
            this.BringToFront();
            this.Activate();
        }

        private void SettingsForm_Resize(object sender, EventArgs e)
        {
            this.Refresh();
        }

        private void CreateCodesMenu()
        {
            codesMenu = new ContextMenuStrip
            {
                Font = new Font("Lucida Console", 8),
                Opacity = 0.8,
                ShowImageMargin = false
            };

            var variables = Enum.GetValues(typeof(ReplacementVariables)).Cast<ReplacementVariables>().
                Select(x => new
                {
                    Name = ReplacementExtension.Prefix + Enum.GetName(typeof(ReplacementVariables), x),
                    Description = x.GetDescription(),
                    Enum = x
                });

            foreach (var variable in variables)
            {
                switch (variable.Enum)
                {
                    case ReplacementVariables.t:
                    case ReplacementVariables.i:
                    case ReplacementVariables.n:
                        continue;
                }

                ToolStripMenuItem tsi = new ToolStripMenuItem { Text = string.Format("{0} - {1}", variable.Name, variable.Description), Tag = variable.Name };
                tsi.Click += (sender, e) => txtNameFormatPattern.AppendText(((ToolStripMenuItem)sender).Tag.ToString());
                codesMenu.Items.Add(tsi);
            }
        }

        private bool ChooseFolder(string title, TextBox tb)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Title = title;

                try
                {
                    string path = tb.Text;

                    if (!string.IsNullOrEmpty(path))
                    {
                        path = Path.GetDirectoryName(path);

                        if (Directory.Exists(path))
                        {
                            ofd.InitialDirectory = path;
                        }
                    }
                }
                finally
                {
                    if (string.IsNullOrEmpty(ofd.InitialDirectory))
                    {
                        ofd.InitialDirectory = Program.PersonalPath;
                    }
                }

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    tb.Text = ofd.FileName;
                    return true;
                }
            }

            return false;
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

        private void cbURLShortenAfterUpload_CheckedChanged(object sender, EventArgs e)
        {
            Program.Settings.URLShortenAfterUpload = cbURLShortenAfterUpload.Checked;
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

        private void btnOpenZUploaderPath_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Program.PersonalPath) && Directory.Exists(Program.PersonalPath))
            {
                Process.Start(Program.PersonalPath);
            }
        }

        #endregion General

        #region Upload

        private void cbUseCustomUploadersConfigPath_CheckedChanged(object sender, EventArgs e)
        {
            Program.Settings.UseCustomUploadersConfigPath = cbUseCustomUploadersConfigPath.Checked;
        }

        private void txtCustomUploadersConfigPath_TextChanged(object sender, EventArgs e)
        {
            Program.Settings.CustomUploadersConfigPath = txtCustomUploadersConfigPath.Text;
        }

        private void btnBrowseCustomUploadersConfigPath_Click(object sender, EventArgs e)
        {
            ChooseFolder("ZUploader - Choose uploaders config file path", txtCustomUploadersConfigPath);
        }

        private void btnLoadUploadersConfig_Click(object sender, EventArgs e)
        {
            Program.LoadUploadersConfig();
        }

        private void nudUploadLimit_ValueChanged(object sender, EventArgs e)
        {
            Program.Settings.UploadLimit = (int)nudUploadLimit.Value;
        }

        private void cbBufferSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            Program.Settings.BufferSizePower = cbBufferSize.SelectedIndex;
            string bufferSize = (Math.Pow(2, Program.Settings.BufferSizePower) * 1024 / 1000).ToString("#,0.###");
            lblBufferSizeInfo.Text = string.Format("x {0} KiB = {1} KiB", 1.024, bufferSize);
        }

        #endregion Upload

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

        private void nudUseImageFormat2After_ValueChanged(object sender, EventArgs e)
        {
            Program.Settings.ImageSizeLimit = (int)nudUseImageFormat2After.Value;
        }

        private void cbImageFormat2_SelectedIndexChanged(object sender, EventArgs e)
        {
            Program.Settings.ImageFormat2 = (EImageFormat)cbImageFormat2.SelectedIndex;
        }

        #endregion Image

        #region Clipboard upload

        private void txtNameFormatPattern_TextChanged(object sender, EventArgs e)
        {
            Program.Settings.NameFormatPattern = txtNameFormatPattern.Text;
            lblNameFormatPatternPreview.Text = "Preview: " + new NameParser().Convert(Program.Settings.NameFormatPattern);
        }

        private void btnNameFormatPatternHelp_Click(object sender, EventArgs e)
        {
            codesMenu.Show(btnNameFormatPatternHelp, new Point(btnNameFormatPatternHelp.Width + 1, 0));
        }

        #endregion Clipboard upload

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
            ChooseFolder("ZUploader - Choose history file path", txtCustomHistoryPath);
        }

        private void nudHistoryMaxItemCount_ValueChanged(object sender, EventArgs e)
        {
            Program.Settings.HistoryMaxItemCount = (int)nudHistoryMaxItemCount.Value;
        }

        #endregion History
    }
}
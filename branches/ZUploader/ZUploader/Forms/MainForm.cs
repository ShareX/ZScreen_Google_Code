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
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;
using HistoryLib;
using UploadersLib.Helpers;

namespace ZUploader
{
    public partial class MainForm : Form
    {
        public bool IsReady { get; private set; }

        private string commandLinePath;

        public MainForm(string path)
        {
            InitializeComponent();
            commandLinePath = path;
            InitControls();
            UpdateControls();
        }

        private void InitControls()
        {
            this.Text = string.Format("{0} {1} {2}", Application.ProductName, Application.ProductVersion, "Beta");

            foreach (string imageUploader in typeof(ImageDestType2).GetDescriptions())
            {
                tsddbImageUploaders.DropDownItems.Add(new ToolStripMenuItem(imageUploader));
            }
            tsddbImageUploaders.DropDownItemClicked += new ToolStripItemClickedEventHandler(tsddbImageUploaders_DropDownItemClicked);

            foreach (string fileUploader in typeof(FileUploaderType2).GetDescriptions())
            {
                tsddbFileUploaders.DropDownItems.Add(new ToolStripMenuItem(fileUploader));
            }
            tsddbFileUploaders.DropDownItemClicked += new ToolStripItemClickedEventHandler(tsddbFileUploaders_DropDownItemClicked);

            foreach (string textUploader in typeof(TextDestType2).GetDescriptions())
            {
                tsddbTextUploaders.DropDownItems.Add(new ToolStripMenuItem(textUploader));
            }
            tsddbTextUploaders.DropDownItemClicked += new ToolStripItemClickedEventHandler(tsddbTextUploaders_DropDownItemClicked);

            ImageList il = new ImageList();
            il.ColorDepth = ColorDepth.Depth32Bit;
            il.Images.Add(ZUploader.Properties.Resources.navigation_090_button);
            il.Images.Add(ZUploader.Properties.Resources.cross_button);
            il.Images.Add(ZUploader.Properties.Resources.tick_button);
            lvUploads.SmallImageList = il;

            UploadManager.ListViewControl = lvUploads;
        }

        private void LoadSettings()
        {
            ((ToolStripMenuItem)tsddbImageUploaders.DropDownItems[Program.Settings.SelectedImageUploaderDestination]).Checked = true;
            UploadManager.ImageUploader = (ImageDestType2)Program.Settings.SelectedImageUploaderDestination;

            ((ToolStripMenuItem)tsddbFileUploaders.DropDownItems[Program.Settings.SelectedFileUploaderDestination]).Checked = true;
            UploadManager.FileUploader = (FileUploaderType2)Program.Settings.SelectedFileUploaderDestination;

            ((ToolStripMenuItem)tsddbTextUploaders.DropDownItems[Program.Settings.SelectedTextUploaderDestination]).Checked = true;
            UploadManager.TextUploader = (TextDestType2)Program.Settings.SelectedTextUploaderDestination;
        }

        private UploadResult GetCurrentUploadResult()
        {
            UploadResult result = null;

            if (lvUploads.SelectedItems.Count > 0)
            {
                result = lvUploads.SelectedItems[0].Tag as UploadResult;
            }

            return result;
        }

        private void OpenURL()
        {
            UploadResult result = GetCurrentUploadResult();

            if (result != null && !string.IsNullOrEmpty(result.URL))
            {
                Process.Start(result.URL);
            }
        }

        private void CopyURL()
        {
            if (lvUploads.SelectedItems.Count > 0)
            {
                string[] array = lvUploads.SelectedItems.Cast<ListViewItem>().Select(x => ((UploadResult)x.Tag).URL).ToArray();
                string urls = string.Join("\r\n", array);

                if (!string.IsNullOrEmpty(urls))
                {
                    Clipboard.SetText(urls);
                }
            }
        }

        private void CopyThumbnailURL()
        {
            UploadResult result = GetCurrentUploadResult();

            if (result != null && !string.IsNullOrEmpty(result.ThumbnailURL))
            {
                Clipboard.SetText(result.ThumbnailURL);
            }
        }

        private void CopyDeletionURL()
        {
            UploadResult result = GetCurrentUploadResult();

            if (result != null && !string.IsNullOrEmpty(result.DeletionURL))
            {
                Clipboard.SetText(result.DeletionURL);
            }
        }

        private string GetErrors()
        {
            string errors = string.Empty;
            UploadResult result = GetCurrentUploadResult();

            if (result != null && result.Errors != null && result.Errors.Count > 0)
            {
                errors = string.Join("\r\n", result.Errors.ToArray());
            }

            return errors;
        }

        private void ShowErrors()
        {
            string errors = GetErrors();

            if (!string.IsNullOrEmpty(errors))
            {
                MessageBox.Show(errors, "ZUploader - Errors", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void CopyErrors()
        {
            string errors = GetErrors();

            if (!string.IsNullOrEmpty(errors))
            {
                Clipboard.SetText(errors);
            }
        }

        private void UpdateControls()
        {
            tsbCopy.Enabled = tsbOpen.Enabled = copyURLToolStripMenuItem.Visible = openURLToolStripMenuItem.Visible =
                copyThumbnailURLToolStripMenuItem.Visible = copyDeletionURLToolStripMenuItem.Visible = showErrorsToolStripMenuItem.Visible =
                copyErrorsToolStripMenuItem.Visible = uploadFileToolStripMenuItem.Visible = stopUploadToolStripMenuItem.Visible = false;

            if (lvUploads.SelectedItems.Count > 0)
            {
                UploadResult result = lvUploads.SelectedItems[0].Tag as UploadResult;

                if (result != null)
                {
                    if (!string.IsNullOrEmpty(result.URL))
                    {
                        tsbCopy.Enabled = tsbOpen.Enabled = copyURLToolStripMenuItem.Visible = openURLToolStripMenuItem.Visible = true;
                    }

                    if (!string.IsNullOrEmpty(result.ThumbnailURL))
                    {
                        copyThumbnailURLToolStripMenuItem.Visible = true;
                    }

                    if (!string.IsNullOrEmpty(result.DeletionURL))
                    {
                        copyDeletionURLToolStripMenuItem.Visible = true;
                    }

                    if (result.Errors != null && result.Errors.Count > 0)
                    {
                        showErrorsToolStripMenuItem.Visible = true;
                        copyErrorsToolStripMenuItem.Visible = true;
                    }
                }

                int index = lvUploads.SelectedIndices[0];
                stopUploadToolStripMenuItem.Visible = UploadManager.Tasks[index].IsUploading;
            }
            else
            {
                uploadFileToolStripMenuItem.Visible = true;
            }
        }

        #region Form events

        private void MainForm_Load(object sender, EventArgs e)
        {
            LoadSettings();
            DebugTimer.WriteLine("MainForm_Load. Settings loaded.");
        }

        private void MainForm_Shown(object sender, EventArgs e)
        {
            DebugTimer.WriteLine("MainForm_Shown. Startup time: {0}ms", Program.StartTimer.ElapsedMilliseconds.ToString());

            UploadManager.Upload(commandLinePath);
            IsReady = true;
        }

        private void MainForm_Resize(object sender, EventArgs e)
        {
            this.Refresh();
        }

        private void MainForm_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private void MainForm_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop, true);
            UploadManager.Upload(files);
        }

        private void tsbClipboardUpload_Click(object sender, EventArgs e)
        {
            UploadManager.ClipboardUpload();
        }

        private void tsbFileUpload_Click(object sender, EventArgs e)
        {
            UploadManager.Upload();
        }

        private void tsbCopy_Click(object sender, EventArgs e)
        {
            CopyURL();
        }

        private void tsbOpen_Click(object sender, EventArgs e)
        {
            OpenURL();
        }

        private void tsbHistory_Click(object sender, EventArgs e)
        {
            new HistoryForm(Program.HistoryFilePath, "ZUploader - History").Show();
        }

        private void tsbSettings_Click(object sender, EventArgs e)
        {
            new SettingsForm().Show();
        }

        private void tsbAbout_Click(object sender, EventArgs e)
        {
            new AboutForm().Show();
        }

        private void lvUploads_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateControls();
        }

        private void cmsUploads_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            UpdateControls();
        }

        private void openURLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenURL();
        }

        private void copyURLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CopyURL();
        }

        private void copyThumbnailURLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CopyThumbnailURL();
        }

        private void copyDeletionURLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CopyDeletionURL();
        }

        private void showErrorsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowErrors();
        }

        private void copyErrorsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CopyErrors();
        }

        private void uploadFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UploadManager.Upload();
        }

        private void stopUploadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (lvUploads.SelectedIndices.Count > 0)
            {
                foreach (int index in lvUploads.SelectedIndices)
                {
                    UploadManager.Tasks[index].Stop();
                }
            }
        }

        private void lvUploads_DoubleClick(object sender, EventArgs e)
        {
            OpenURL();
        }

        private void tsddbImageUploaders_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            for (int i = 0; i < tsddbImageUploaders.DropDownItems.Count; i++)
            {
                ToolStripMenuItem tsmi = (ToolStripMenuItem)tsddbImageUploaders.DropDownItems[i];
                if (tsmi.Checked = tsmi == e.ClickedItem)
                {
                    Program.Settings.SelectedImageUploaderDestination = i;
                    UploadManager.ImageUploader = (ImageDestType2)i;
                }
            }
        }

        private void tsddbFileUploaders_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            for (int i = 0; i < tsddbFileUploaders.DropDownItems.Count; i++)
            {
                ToolStripMenuItem tsmi = (ToolStripMenuItem)tsddbFileUploaders.DropDownItems[i];
                if (tsmi.Checked = tsmi == e.ClickedItem)
                {
                    Program.Settings.SelectedFileUploaderDestination = i;
                    UploadManager.FileUploader = (FileUploaderType2)i;
                }
            }
        }

        private void tsddbTextUploaders_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            for (int i = 0; i < tsddbTextUploaders.DropDownItems.Count; i++)
            {
                ToolStripMenuItem tsmi = (ToolStripMenuItem)tsddbTextUploaders.DropDownItems[i];
                if (tsmi.Checked = tsmi == e.ClickedItem)
                {
                    Program.Settings.SelectedTextUploaderDestination = i;
                    UploadManager.TextUploader = (TextDestType2)i;
                }
            }
        }

        #endregion Form events
    }
}
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
using System.Linq;
using System.Windows.Forms;
using HelpersLib;
using HistoryLib;
using UploadersLib;
using UploadersLib.HelperClasses;
using ZUploaderPlugin;

namespace ZUploader
{
    public partial class MainForm : HotkeyForm, IPluginHost
    {
        public bool IsReady { get; private set; }

        private PluginManager pluginManager;

        public MainForm()
        {
            InitControls();
            UpdateControls();
        }

        private void AfterLoadJobs()
        {
            LoadSettings();
            //LoadPlugins();
        }

        private void AfterShownJobs()
        {
            UseCommandLineArg(Program.CommandLineArg);
            IsReady = true;
        }

        public void TestPlugin(string text)
        {
            Debug.WriteLine(text);
        }

        private void InitControls()
        {
            InitializeComponent();

            this.Text = Program.Title;

            foreach (string imageUploader in Helpers.GetEnumDescriptions<ImageDestination>())
            {
                tsddbImageUploaders.DropDownItems.Add(new ToolStripMenuItem(imageUploader));
            }
            tsddbImageUploaders.DropDownItemClicked += new ToolStripItemClickedEventHandler(tsddbImageUploaders_DropDownItemClicked);

            foreach (string fileUploader in Helpers.GetEnumDescriptions<FileUploaderType>())
            {
                tsddbFileUploaders.DropDownItems.Add(new ToolStripMenuItem(fileUploader));
            }
            tsddbFileUploaders.DropDownItemClicked += new ToolStripItemClickedEventHandler(tsddbFileUploaders_DropDownItemClicked);

            foreach (string textUploader in Helpers.GetEnumDescriptions<TextUploaderType>())
            {
                tsddbTextUploaders.DropDownItems.Add(new ToolStripMenuItem(textUploader));
            }
            tsddbTextUploaders.DropDownItemClicked += new ToolStripItemClickedEventHandler(tsddbTextUploaders_DropDownItemClicked);

            foreach (string urlShortener in Helpers.GetEnumDescriptions<UrlShortenerType>())
            {
                tsddbURLShorteners.DropDownItems.Add(new ToolStripMenuItem(urlShortener));
            }
            tsddbURLShorteners.DropDownItemClicked += new ToolStripItemClickedEventHandler(tsddbURLShorteners_DropDownItemClicked);

            ImageList il = new ImageList();
            il.ColorDepth = ColorDepth.Depth32Bit;
            il.Images.Add(Properties.Resources.navigation_090_button);
            il.Images.Add(Properties.Resources.cross_button);
            il.Images.Add(Properties.Resources.tick_button);
            il.Images.Add(Properties.Resources.navigation_000_button);
            lvUploads.SmallImageList = il;
            lvUploads.FillLastColumn();

            UploadManager.ListViewControl = lvUploads;

            HotkeyPress += new HotkeyEventHandler(MainForm_HotkeyPress);
        }

        private void LoadSettings()
        {
            if (Helpers.GetEnumLength<ImageDestination>() <= Program.Settings.SelectedImageUploaderDestination)
            {
                Program.Settings.SelectedImageUploaderDestination = 0;
            }

            ((ToolStripMenuItem)tsddbImageUploaders.DropDownItems[Program.Settings.SelectedImageUploaderDestination]).Checked = true;
            UploadManager.ImageUploader = (ImageDestination)Program.Settings.SelectedImageUploaderDestination;

            if (Helpers.GetEnumLength<FileUploaderType>() <= Program.Settings.SelectedFileUploaderDestination)
            {
                Program.Settings.SelectedFileUploaderDestination = 0;
            }

            ((ToolStripMenuItem)tsddbFileUploaders.DropDownItems[Program.Settings.SelectedFileUploaderDestination]).Checked = true;
            UploadManager.FileUploader = (FileUploaderType)Program.Settings.SelectedFileUploaderDestination;

            if (Helpers.GetEnumLength<TextUploaderType>() <= Program.Settings.SelectedTextUploaderDestination)
            {
                Program.Settings.SelectedTextUploaderDestination = 0;
            }

            ((ToolStripMenuItem)tsddbTextUploaders.DropDownItems[Program.Settings.SelectedTextUploaderDestination]).Checked = true;
            UploadManager.TextUploader = (TextUploaderType)Program.Settings.SelectedTextUploaderDestination;

            if (Helpers.GetEnumLength<UrlShortenerType>() <= Program.Settings.SelectedURLShortenerDestination)
            {
                Program.Settings.SelectedURLShortenerDestination = 0;
            }

            ((ToolStripMenuItem)tsddbURLShorteners.DropDownItems[Program.Settings.SelectedURLShortenerDestination]).Checked = true;
            UploadManager.URLShortener = (UrlShortenerType)Program.Settings.SelectedURLShortenerDestination;

            RegisterHotkey(Program.Settings.HotkeyClipboardUpload);
        }

        private void LoadPlugins()
        {
            pluginManager = new PluginManager(Program.PluginFolderPath, this);
            pluginManager.LoadPlugins();
        }

        private void UpdateControls()
        {
            tsbCopy.Enabled = tsbOpen.Enabled = copyURLToolStripMenuItem.Visible = openURLToolStripMenuItem.Visible =
                copyShortenedURLToolStripMenuItem.Visible = copyThumbnailURLToolStripMenuItem.Visible = copyDeletionURLToolStripMenuItem.Visible =
                showErrorsToolStripMenuItem.Visible = copyErrorsToolStripMenuItem.Visible = showResponseToolStripMenuItem.Visible =
                uploadFileToolStripMenuItem.Visible = stopUploadToolStripMenuItem.Visible = false;

            int itemsCount = lvUploads.SelectedItems.Count;

            if (itemsCount > 0)
            {
                UploadResult result = lvUploads.SelectedItems[0].Tag as UploadResult;

                if (result != null)
                {
                    if (!string.IsNullOrEmpty(result.URL))
                    {
                        tsbCopy.Enabled = tsbOpen.Enabled = copyURLToolStripMenuItem.Visible = openURLToolStripMenuItem.Visible = true;

                        if (itemsCount > 1)
                        {
                            copyURLToolStripMenuItem.Text = string.Format("Copy URLs ({0})", itemsCount);
                        }
                        else
                        {
                            copyURLToolStripMenuItem.Text = "Copy URL";
                        }
                    }

                    if (!string.IsNullOrEmpty(result.ThumbnailURL))
                    {
                        copyThumbnailURLToolStripMenuItem.Visible = true;
                    }

                    if (!string.IsNullOrEmpty(result.DeletionURL))
                    {
                        copyDeletionURLToolStripMenuItem.Visible = true;
                    }

                    if (!string.IsNullOrEmpty(result.ShortenedURL))
                    {
                        copyShortenedURLToolStripMenuItem.Visible = true;
                    }

                    if (result.IsError)
                    {
                        showErrorsToolStripMenuItem.Visible = true;
                        copyErrorsToolStripMenuItem.Visible = true;
                    }

                    if (!string.IsNullOrEmpty(result.Source))
                    {
                        showResponseToolStripMenuItem.Visible = true;
                    }
                }

                int index = lvUploads.SelectedIndices[0];
                stopUploadToolStripMenuItem.Visible = UploadManager.Tasks[index].Status != TaskStatus.Completed;
            }
            else
            {
                uploadFileToolStripMenuItem.Visible = true;
            }
        }

        public void UseCommandLineArg(string arg)
        {
            if (!string.IsNullOrEmpty(arg))
            {
                arg = arg.Trim();

                if (arg.Equals("-clipboardupload", StringComparison.InvariantCultureIgnoreCase))
                {
                    UploadManager.ClipboardUpload();
                }
                else
                {
                    UploadManager.UploadFile(arg);
                }
            }
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
                string[] array = lvUploads.SelectedItems.Cast<ListViewItem>().Select(x => x.Tag as UploadResult).
                    Where(x => x != null && !string.IsNullOrEmpty(x.URL)).Select(x => x.URL).ToArray();

                if (array != null && array.Length > 0)
                {
                    string urls = string.Join("\r\n", array);

                    if (!string.IsNullOrEmpty(urls))
                    {
                        Helpers.CopyTextSafely(urls);
                    }
                }
            }
        }

        private void CopyShortenedURL()
        {
            UploadResult result = GetCurrentUploadResult();

            if (result != null && !string.IsNullOrEmpty(result.ShortenedURL))
            {
                Helpers.CopyTextSafely(result.ShortenedURL);
            }
        }

        private void CopyThumbnailURL()
        {
            UploadResult result = GetCurrentUploadResult();

            if (result != null && !string.IsNullOrEmpty(result.ThumbnailURL))
            {
                Helpers.CopyTextSafely(result.ThumbnailURL);
            }
        }

        private void CopyDeletionURL()
        {
            UploadResult result = GetCurrentUploadResult();

            if (result != null && !string.IsNullOrEmpty(result.DeletionURL))
            {
                Helpers.CopyTextSafely(result.DeletionURL);
            }
        }

        private string GetErrors()
        {
            string errors = string.Empty;
            UploadResult result = GetCurrentUploadResult();

            if (result != null && result.IsError)
            {
                errors = string.Join("\r\n\r\n", result.Errors.ToArray());
            }

            return errors;
        }

        private void ShowErrors()
        {
            string errors = GetErrors();

            if (!string.IsNullOrEmpty(errors))
            {
                new ErrorForm("ZUploader - Errors", "Upload errors:", errors, Program.LogFilePath, Program.URL_ISSUES).ShowDialog();
            }
        }

        private void CopyErrors()
        {
            string errors = GetErrors();

            if (!string.IsNullOrEmpty(errors))
            {
                Helpers.CopyTextSafely(errors);
            }
        }

        private void ShowResponse()
        {
            UploadResult result = GetCurrentUploadResult();

            if (result != null && !string.IsNullOrEmpty(result.Source))
            {
                ResponseForm form = new ResponseForm(result.Source);
                form.Icon = this.Icon;
                form.Show();
            }
        }

        #region Form events

        private void MainForm_Load(object sender, EventArgs e)
        {
            Program.MyLogger.WriteLine("MainForm_Load");
            AfterLoadJobs();
        }

        private void MainForm_Shown(object sender, EventArgs e)
        {
            Program.MyLogger.WriteLine("MainForm_Shown. Startup time: {0}ms", Program.StartTimer.ElapsedMilliseconds);
            AfterShownJobs();
        }

        private void MainForm_Resize(object sender, EventArgs e)
        {
            this.Refresh();
        }

        private void MainForm_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop, false) ||
                e.Data.GetDataPresent(DataFormats.Bitmap, false) ||
                e.Data.GetDataPresent(DataFormats.Text, false))
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
            UploadManager.DragDropUpload(e.Data);
        }

        private void MainForm_HotkeyPress(KeyEventArgs e)
        {
            if (e.KeyData == Program.Settings.HotkeyClipboardUpload)
            {
                UploadManager.ClipboardUpload();
            }
        }

        private void tsbClipboardUpload_Click(object sender, EventArgs e)
        {
            if (Program.Settings.ShowClipboardContentViewer)
            {
                using (ClipboardContentViewer ccv = new ClipboardContentViewer())
                {
                    if (ccv.ShowDialog() == DialogResult.OK && !ccv.IsClipboardEmpty)
                    {
                        UploadManager.ClipboardUpload();
                    }

                    Program.Settings.ShowClipboardContentViewer = !ccv.DontShowThisWindow;
                }
            }
            else
            {
                UploadManager.ClipboardUpload();
            }
        }

        private void tsbFileUpload_Click(object sender, EventArgs e)
        {
            UploadManager.UploadFile();
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
            new HistoryForm(Program.HistoryFilePath, Program.Settings.HistoryMaxItemCount, "ZUploader - History").Show();
        }

        private void tsbSettings_Click(object sender, EventArgs e)
        {
            SettingsForm form = new SettingsForm();
            form.Icon = this.Icon;
            form.Show();
        }

        private void tsbAbout_Click(object sender, EventArgs e)
        {
            AboutForm form = new AboutForm();
            form.Icon = this.Icon;
            form.Show();
        }

        private void lvUploads_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateControls();
        }

        private void lvUploads_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                UpdateControls();
                cmsUploads.Show(lvUploads, e.X + 1, e.Y + 1);
            }
        }

        private void openURLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenURL();
        }

        private void copyURLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CopyURL();
        }

        private void copyShortenedURLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CopyShortenedURL();
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

        private void showResponseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowResponse();
        }

        private void uploadFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UploadManager.UploadFile();
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
                    UploadManager.ImageUploader = (ImageDestination)i;
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
                    UploadManager.FileUploader = (FileUploaderType)i;
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
                    UploadManager.TextUploader = (TextUploaderType)i;
                }
            }
        }

        private void tsddbURLShorteners_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            for (int i = 0; i < tsddbURLShorteners.DropDownItems.Count; i++)
            {
                ToolStripMenuItem tsmi = (ToolStripMenuItem)tsddbURLShorteners.DropDownItems[i];
                if (tsmi.Checked = tsmi == e.ClickedItem)
                {
                    Program.Settings.SelectedURLShortenerDestination = i;
                    UploadManager.URLShortener = (UrlShortenerType)i;
                }
            }
        }

        #endregion Form events

        private void tsddbUploadersConfig_Click(object sender, EventArgs e)
        {
            UploadersConfigForm ucf = new UploadersConfigForm(Program.Settings.UploadersConfig);
            ucf.Show();
        }
    }
}
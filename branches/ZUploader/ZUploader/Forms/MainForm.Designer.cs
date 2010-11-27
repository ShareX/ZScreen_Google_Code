#region License Information (GPL v2)

/*
    ZUploader - A program that allows you to upload images, text or files in your clipboard
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

namespace ZUploader
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.cmsUploads = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.openURLToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyURLToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyThumbnailURLToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyDeletionURLToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showErrorsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyErrorsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.uploadFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stopUploadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsMain = new System.Windows.Forms.ToolStrip();
            this.tsbClipboardUpload = new System.Windows.Forms.ToolStripButton();
            this.tsbFileUpload = new System.Windows.Forms.ToolStripButton();
            this.tss1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbCopy = new System.Windows.Forms.ToolStripButton();
            this.tsbOpen = new System.Windows.Forms.ToolStripButton();
            this.tss2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsddbImageUploaders = new System.Windows.Forms.ToolStripDropDownButton();
            this.tsddbFileUploaders = new System.Windows.Forms.ToolStripDropDownButton();
            this.tsddbTextUploaders = new System.Windows.Forms.ToolStripDropDownButton();
            this.tss3 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbHistory = new System.Windows.Forms.ToolStripButton();
            this.tsbSettings = new System.Windows.Forms.ToolStripButton();
            this.tsbAbout = new System.Windows.Forms.ToolStripButton();
            this.tscMain = new System.Windows.Forms.ToolStripContainer();
            this.lvUploads = new HistoryLib.Custom_Controls.MyListView();
            this.chFilename = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chStatus = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chProgress = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chSpeed = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chElapsed = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chRemaining = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chUploaderType = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chHost = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chURL = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.showResponseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cmsUploads.SuspendLayout();
            this.tsMain.SuspendLayout();
            this.tscMain.ContentPanel.SuspendLayout();
            this.tscMain.TopToolStripPanel.SuspendLayout();
            this.tscMain.SuspendLayout();
            this.SuspendLayout();
            //
            // cmsUploads
            //
            this.cmsUploads.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openURLToolStripMenuItem,
            this.copyURLToolStripMenuItem,
            this.copyThumbnailURLToolStripMenuItem,
            this.copyDeletionURLToolStripMenuItem,
            this.showErrorsToolStripMenuItem,
            this.copyErrorsToolStripMenuItem,
            this.showResponseToolStripMenuItem,
            this.uploadFileToolStripMenuItem,
            this.stopUploadToolStripMenuItem});
            this.cmsUploads.Name = "cmsUploads";
            this.cmsUploads.Size = new System.Drawing.Size(188, 224);
            this.cmsUploads.Opening += new System.ComponentModel.CancelEventHandler(this.cmsUploads_Opening);
            //
            // openURLToolStripMenuItem
            //
            this.openURLToolStripMenuItem.Name = "openURLToolStripMenuItem";
            this.openURLToolStripMenuItem.Size = new System.Drawing.Size(187, 22);
            this.openURLToolStripMenuItem.Text = "Open URL";
            this.openURLToolStripMenuItem.Click += new System.EventHandler(this.openURLToolStripMenuItem_Click);
            //
            // copyURLToolStripMenuItem
            //
            this.copyURLToolStripMenuItem.Name = "copyURLToolStripMenuItem";
            this.copyURLToolStripMenuItem.Size = new System.Drawing.Size(187, 22);
            this.copyURLToolStripMenuItem.Text = "Copy URL";
            this.copyURLToolStripMenuItem.Click += new System.EventHandler(this.copyURLToolStripMenuItem_Click);
            //
            // copyThumbnailURLToolStripMenuItem
            //
            this.copyThumbnailURLToolStripMenuItem.Name = "copyThumbnailURLToolStripMenuItem";
            this.copyThumbnailURLToolStripMenuItem.Size = new System.Drawing.Size(187, 22);
            this.copyThumbnailURLToolStripMenuItem.Text = "Copy Thumbnail URL";
            this.copyThumbnailURLToolStripMenuItem.Click += new System.EventHandler(this.copyThumbnailURLToolStripMenuItem_Click);
            //
            // copyDeletionURLToolStripMenuItem
            //
            this.copyDeletionURLToolStripMenuItem.Name = "copyDeletionURLToolStripMenuItem";
            this.copyDeletionURLToolStripMenuItem.Size = new System.Drawing.Size(187, 22);
            this.copyDeletionURLToolStripMenuItem.Text = "Copy Deletion URL";
            this.copyDeletionURLToolStripMenuItem.Click += new System.EventHandler(this.copyDeletionURLToolStripMenuItem_Click);
            //
            // showErrorsToolStripMenuItem
            //
            this.showErrorsToolStripMenuItem.Name = "showErrorsToolStripMenuItem";
            this.showErrorsToolStripMenuItem.Size = new System.Drawing.Size(187, 22);
            this.showErrorsToolStripMenuItem.Text = "Show Errors";
            this.showErrorsToolStripMenuItem.Click += new System.EventHandler(this.showErrorsToolStripMenuItem_Click);
            //
            // copyErrorsToolStripMenuItem
            //
            this.copyErrorsToolStripMenuItem.Name = "copyErrorsToolStripMenuItem";
            this.copyErrorsToolStripMenuItem.Size = new System.Drawing.Size(187, 22);
            this.copyErrorsToolStripMenuItem.Text = "Copy Errors";
            this.copyErrorsToolStripMenuItem.Click += new System.EventHandler(this.copyErrorsToolStripMenuItem_Click);
            //
            // uploadFileToolStripMenuItem
            //
            this.uploadFileToolStripMenuItem.Name = "uploadFileToolStripMenuItem";
            this.uploadFileToolStripMenuItem.Size = new System.Drawing.Size(187, 22);
            this.uploadFileToolStripMenuItem.Text = "Upload file...";
            this.uploadFileToolStripMenuItem.Click += new System.EventHandler(this.uploadFileToolStripMenuItem_Click);
            //
            // stopUploadToolStripMenuItem
            //
            this.stopUploadToolStripMenuItem.Name = "stopUploadToolStripMenuItem";
            this.stopUploadToolStripMenuItem.Size = new System.Drawing.Size(187, 22);
            this.stopUploadToolStripMenuItem.Text = "Stop upload";
            this.stopUploadToolStripMenuItem.Click += new System.EventHandler(this.stopUploadToolStripMenuItem_Click);
            //
            // tsMain
            //
            this.tsMain.Dock = System.Windows.Forms.DockStyle.None;
            this.tsMain.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.tsMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbClipboardUpload,
            this.tsbFileUpload,
            this.tss1,
            this.tsbCopy,
            this.tsbOpen,
            this.tss2,
            this.tsddbImageUploaders,
            this.tsddbFileUploaders,
            this.tsddbTextUploaders,
            this.tss3,
            this.tsbHistory,
            this.tsbSettings,
            this.tsbAbout});
            this.tsMain.Location = new System.Drawing.Point(3, 0);
            this.tsMain.Name = "tsMain";
            this.tsMain.Padding = new System.Windows.Forms.Padding(4, 6, 4, 4);
            this.tsMain.ShowItemToolTips = false;
            this.tsMain.Size = new System.Drawing.Size(931, 33);
            this.tsMain.TabIndex = 87;
            this.tsMain.Text = "toolStrip1";
            //
            // tsbClipboardUpload
            //
            this.tsbClipboardUpload.Image = global::ZUploader.Properties.Resources.clipboard__plus;
            this.tsbClipboardUpload.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbClipboardUpload.Name = "tsbClipboardUpload";
            this.tsbClipboardUpload.Size = new System.Drawing.Size(119, 20);
            this.tsbClipboardUpload.Text = "Clipboard upload";
            this.tsbClipboardUpload.Click += new System.EventHandler(this.tsbClipboardUpload_Click);
            //
            // tsbFileUpload
            //
            this.tsbFileUpload.Image = global::ZUploader.Properties.Resources.folder__plus;
            this.tsbFileUpload.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbFileUpload.Name = "tsbFileUpload";
            this.tsbFileUpload.Size = new System.Drawing.Size(94, 20);
            this.tsbFileUpload.Text = "File upload...";
            this.tsbFileUpload.Click += new System.EventHandler(this.tsbFileUpload_Click);
            //
            // tss1
            //
            this.tss1.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.tss1.Name = "tss1";
            this.tss1.Size = new System.Drawing.Size(6, 23);
            //
            // tsbCopy
            //
            this.tsbCopy.Image = global::ZUploader.Properties.Resources.document_copy;
            this.tsbCopy.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbCopy.Name = "tsbCopy";
            this.tsbCopy.Size = new System.Drawing.Size(55, 20);
            this.tsbCopy.Text = "Copy";
            this.tsbCopy.Click += new System.EventHandler(this.tsbCopy_Click);
            //
            // tsbOpen
            //
            this.tsbOpen.Image = global::ZUploader.Properties.Resources.document__arrow;
            this.tsbOpen.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbOpen.Name = "tsbOpen";
            this.tsbOpen.Size = new System.Drawing.Size(56, 20);
            this.tsbOpen.Text = "Open";
            this.tsbOpen.Click += new System.EventHandler(this.tsbOpen_Click);
            //
            // tss2
            //
            this.tss2.Margin = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.tss2.Name = "tss2";
            this.tss2.Size = new System.Drawing.Size(6, 23);
            //
            // tsddbImageUploaders
            //
            this.tsddbImageUploaders.Image = global::ZUploader.Properties.Resources.image__plus;
            this.tsddbImageUploaders.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsddbImageUploaders.Name = "tsddbImageUploaders";
            this.tsddbImageUploaders.Size = new System.Drawing.Size(124, 20);
            this.tsddbImageUploaders.Text = "Image uploaders";
            //
            // tsddbFileUploaders
            //
            this.tsddbFileUploaders.Image = global::ZUploader.Properties.Resources.application__plus;
            this.tsddbFileUploaders.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsddbFileUploaders.Name = "tsddbFileUploaders";
            this.tsddbFileUploaders.Size = new System.Drawing.Size(109, 20);
            this.tsddbFileUploaders.Text = "File uploaders";
            //
            // tsddbTextUploaders
            //
            this.tsddbTextUploaders.Image = global::ZUploader.Properties.Resources.document__plus;
            this.tsddbTextUploaders.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsddbTextUploaders.Name = "tsddbTextUploaders";
            this.tsddbTextUploaders.Size = new System.Drawing.Size(113, 20);
            this.tsddbTextUploaders.Text = "Text uploaders";
            //
            // tss3
            //
            this.tss3.Name = "tss3";
            this.tss3.Size = new System.Drawing.Size(6, 23);
            //
            // tsbHistory
            //
            this.tsbHistory.Image = global::ZUploader.Properties.Resources.address_book_blue;
            this.tsbHistory.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbHistory.Name = "tsbHistory";
            this.tsbHistory.Size = new System.Drawing.Size(74, 20);
            this.tsbHistory.Text = "History...";
            this.tsbHistory.Click += new System.EventHandler(this.tsbHistory_Click);
            //
            // tsbSettings
            //
            this.tsbSettings.Image = global::ZUploader.Properties.Resources.application_form;
            this.tsbSettings.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbSettings.Name = "tsbSettings";
            this.tsbSettings.Size = new System.Drawing.Size(78, 20);
            this.tsbSettings.Text = "Settings...";
            this.tsbSettings.Click += new System.EventHandler(this.tsbSettings_Click);
            //
            // tsbAbout
            //
            this.tsbAbout.Image = global::ZUploader.Properties.Resources.information;
            this.tsbAbout.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbAbout.Name = "tsbAbout";
            this.tsbAbout.Size = new System.Drawing.Size(69, 20);
            this.tsbAbout.Text = "About...";
            this.tsbAbout.Click += new System.EventHandler(this.tsbAbout_Click);
            //
            // tscMain
            //
            this.tscMain.BottomToolStripPanelVisible = false;
            //
            // tscMain.ContentPanel
            //
            this.tscMain.ContentPanel.Controls.Add(this.lvUploads);
            this.tscMain.ContentPanel.Padding = new System.Windows.Forms.Padding(3);
            this.tscMain.ContentPanel.Size = new System.Drawing.Size(954, 329);
            this.tscMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tscMain.LeftToolStripPanelVisible = false;
            this.tscMain.Location = new System.Drawing.Point(0, 0);
            this.tscMain.Name = "tscMain";
            this.tscMain.RightToolStripPanelVisible = false;
            this.tscMain.Size = new System.Drawing.Size(954, 362);
            this.tscMain.TabIndex = 88;
            this.tscMain.Text = "toolStripContainer1";
            //
            // tscMain.TopToolStripPanel
            //
            this.tscMain.TopToolStripPanel.Controls.Add(this.tsMain);
            //
            // lvUploads
            //
            this.lvUploads.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chFilename,
            this.chStatus,
            this.chProgress,
            this.chSpeed,
            this.chElapsed,
            this.chRemaining,
            this.chUploaderType,
            this.chHost,
            this.chURL});
            this.lvUploads.ContextMenuStrip = this.cmsUploads;
            this.lvUploads.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvUploads.FullRowSelect = true;
            this.lvUploads.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lvUploads.HideSelection = false;
            this.lvUploads.Location = new System.Drawing.Point(3, 3);
            this.lvUploads.Name = "lvUploads";
            this.lvUploads.ShowItemToolTips = true;
            this.lvUploads.Size = new System.Drawing.Size(948, 323);
            this.lvUploads.TabIndex = 3;
            this.lvUploads.UseCompatibleStateImageBehavior = false;
            this.lvUploads.View = System.Windows.Forms.View.Details;
            this.lvUploads.SelectedIndexChanged += new System.EventHandler(this.lvUploads_SelectedIndexChanged);
            this.lvUploads.DoubleClick += new System.EventHandler(this.lvUploads_DoubleClick);
            //
            // chFilename
            //
            this.chFilename.Text = "Filename";
            this.chFilename.Width = 150;
            //
            // chStatus
            //
            this.chStatus.Text = "Status";
            this.chStatus.Width = 75;
            //
            // chProgress
            //
            this.chProgress.Text = "Progress";
            this.chProgress.Width = 149;
            //
            // chSpeed
            //
            this.chSpeed.Text = "Speed";
            this.chSpeed.Width = 65;
            //
            // chElapsed
            //
            this.chElapsed.Text = "Elapsed";
            this.chElapsed.Width = 50;
            //
            // chRemaining
            //
            this.chRemaining.Text = "Remaining";
            this.chRemaining.Width = 50;
            //
            // chUploaderType
            //
            this.chUploaderType.Text = "Type";
            this.chUploaderType.Width = 50;
            //
            // chHost
            //
            this.chHost.Text = "Host";
            this.chHost.Width = 100;
            //
            // chURL
            //
            this.chURL.Text = "URL";
            this.chURL.Width = 225;
            //
            // showResponseToolStripMenuItem
            //
            this.showResponseToolStripMenuItem.Name = "showResponseToolStripMenuItem";
            this.showResponseToolStripMenuItem.Size = new System.Drawing.Size(187, 22);
            this.showResponseToolStripMenuItem.Text = "Show Response";
            this.showResponseToolStripMenuItem.Click += new System.EventHandler(this.showResponseToolStripMenuItem_Click);
            //
            // MainForm
            //
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(954, 362);
            this.Controls.Add(this.tscMain);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(900, 400);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ZUploader";
            this.Shown += new System.EventHandler(this.MainForm_Shown);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.MainForm_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.MainForm_DragEnter);
            this.Resize += new System.EventHandler(this.MainForm_Resize);
            this.cmsUploads.ResumeLayout(false);
            this.tsMain.ResumeLayout(false);
            this.tsMain.PerformLayout();
            this.tscMain.ContentPanel.ResumeLayout(false);
            this.tscMain.TopToolStripPanel.ResumeLayout(false);
            this.tscMain.TopToolStripPanel.PerformLayout();
            this.tscMain.ResumeLayout(false);
            this.tscMain.PerformLayout();
            this.ResumeLayout(false);
        }

        #endregion Windows Form Designer generated code

        private HistoryLib.Custom_Controls.MyListView lvUploads;
        private System.Windows.Forms.ColumnHeader chStatus;
        private System.Windows.Forms.ColumnHeader chURL;
        private System.Windows.Forms.ContextMenuStrip cmsUploads;
        private System.Windows.Forms.ToolStripMenuItem copyURLToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openURLToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyThumbnailURLToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyDeletionURLToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyErrorsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem uploadFileToolStripMenuItem;
        private System.Windows.Forms.ColumnHeader chFilename;
        private System.Windows.Forms.ColumnHeader chProgress;
        private System.Windows.Forms.ColumnHeader chHost;
        private System.Windows.Forms.ColumnHeader chUploaderType;
        private System.Windows.Forms.ToolStrip tsMain;
        private System.Windows.Forms.ToolStripButton tsbClipboardUpload;
        private System.Windows.Forms.ToolStripButton tsbFileUpload;
        private System.Windows.Forms.ToolStripButton tsbCopy;
        private System.Windows.Forms.ToolStripButton tsbOpen;
        private System.Windows.Forms.ToolStripButton tsbSettings;
        private System.Windows.Forms.ToolStripButton tsbAbout;
        private System.Windows.Forms.ToolStripDropDownButton tsddbImageUploaders;
        private System.Windows.Forms.ToolStripDropDownButton tsddbFileUploaders;
        private System.Windows.Forms.ToolStripSeparator tss1;
        private System.Windows.Forms.ToolStripDropDownButton tsddbTextUploaders;
        private System.Windows.Forms.ToolStripSeparator tss2;
        private System.Windows.Forms.ToolStripContainer tscMain;
        private System.Windows.Forms.ColumnHeader chSpeed;
        private System.Windows.Forms.ColumnHeader chRemaining;
        private System.Windows.Forms.ToolStripMenuItem stopUploadToolStripMenuItem;
        private System.Windows.Forms.ColumnHeader chElapsed;
        private System.Windows.Forms.ToolStripButton tsbHistory;
        private System.Windows.Forms.ToolStripMenuItem showErrorsToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator tss3;
        private System.Windows.Forms.ToolStripMenuItem showResponseToolStripMenuItem;
    }
}
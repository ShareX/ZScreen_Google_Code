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
#endregion

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
            this.lvUploads = new System.Windows.Forms.ListView();
            this.chID = new System.Windows.Forms.ColumnHeader();
            this.chStatus = new System.Windows.Forms.ColumnHeader();
            this.chFilePath = new System.Windows.Forms.ColumnHeader();
            this.chProgress = new System.Windows.Forms.ColumnHeader();
            this.chUploaderType = new System.Windows.Forms.ColumnHeader();
            this.chHost = new System.Windows.Forms.ColumnHeader();
            this.chURL = new System.Windows.Forms.ColumnHeader();
            this.cmsUploads = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.openURLToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyURLToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyThumbnailURLToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyDeletionURLToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyErrorsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.uploadFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsMain = new System.Windows.Forms.ToolStrip();
            this.tsbClipboardUpload = new System.Windows.Forms.ToolStripButton();
            this.tsbFileUpload = new System.Windows.Forms.ToolStripButton();
            this.tsbCopy = new System.Windows.Forms.ToolStripButton();
            this.tsbOpen = new System.Windows.Forms.ToolStripButton();
            this.tsbSettings = new System.Windows.Forms.ToolStripButton();
            this.tsbAbout = new System.Windows.Forms.ToolStripButton();
            this.tss1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsddbImageUploaders = new System.Windows.Forms.ToolStripDropDownButton();
            this.tsddbFileUploaders = new System.Windows.Forms.ToolStripDropDownButton();
            this.tsddbTextUploaders = new System.Windows.Forms.ToolStripDropDownButton();
            this.cmsUploads.SuspendLayout();
            this.tsMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // lvUploads
            // 
            this.lvUploads.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lvUploads.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chID,
            this.chStatus,
            this.chFilePath,
            this.chProgress,
            this.chUploaderType,
            this.chHost,
            this.chURL});
            this.lvUploads.ContextMenuStrip = this.cmsUploads;
            this.lvUploads.FullRowSelect = true;
            this.lvUploads.GridLines = true;
            this.lvUploads.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lvUploads.HideSelection = false;
            this.lvUploads.Location = new System.Drawing.Point(8, 35);
            this.lvUploads.Name = "lvUploads";
            this.lvUploads.Size = new System.Drawing.Size(937, 369);
            this.lvUploads.TabIndex = 3;
            this.lvUploads.UseCompatibleStateImageBehavior = false;
            this.lvUploads.View = System.Windows.Forms.View.Details;
            this.lvUploads.SelectedIndexChanged += new System.EventHandler(this.lvUploads_SelectedIndexChanged);
            this.lvUploads.DoubleClick += new System.EventHandler(this.lvUploads_DoubleClick);
            // 
            // chID
            // 
            this.chID.Text = "ID";
            this.chID.Width = 25;
            // 
            // chStatus
            // 
            this.chStatus.Text = "Status";
            this.chStatus.Width = 100;
            // 
            // chFilePath
            // 
            this.chFilePath.Text = "File path";
            this.chFilePath.Width = 150;
            // 
            // chProgress
            // 
            this.chProgress.Text = "Progress";
            this.chProgress.Width = 130;
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
            this.chURL.Width = 377;
            // 
            // cmsUploads
            // 
            this.cmsUploads.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openURLToolStripMenuItem,
            this.copyURLToolStripMenuItem,
            this.copyThumbnailURLToolStripMenuItem,
            this.copyDeletionURLToolStripMenuItem,
            this.copyErrorsToolStripMenuItem,
            this.uploadFileToolStripMenuItem});
            this.cmsUploads.Name = "cmsUploads";
            this.cmsUploads.Size = new System.Drawing.Size(188, 136);
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
            // tsMain
            // 
            this.tsMain.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.tsMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbClipboardUpload,
            this.tsbFileUpload,
            this.tsbCopy,
            this.tsbOpen,
            this.tsbSettings,
            this.tsbAbout,
            this.tss1,
            this.tsddbImageUploaders,
            this.tsddbFileUploaders,
            this.tsddbTextUploaders});
            this.tsMain.Location = new System.Drawing.Point(0, 0);
            this.tsMain.Name = "tsMain";
            this.tsMain.Padding = new System.Windows.Forms.Padding(7, 5, 0, 3);
            this.tsMain.Size = new System.Drawing.Size(954, 31);
            this.tsMain.TabIndex = 87;
            this.tsMain.Text = "toolStrip1";
            // 
            // tsbClipboardUpload
            // 
            this.tsbClipboardUpload.Image = ((System.Drawing.Image)(resources.GetObject("tsbClipboardUpload.Image")));
            this.tsbClipboardUpload.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbClipboardUpload.Name = "tsbClipboardUpload";
            this.tsbClipboardUpload.Size = new System.Drawing.Size(119, 20);
            this.tsbClipboardUpload.Text = "Clipboard upload";
            this.tsbClipboardUpload.Click += new System.EventHandler(this.tsbClipboardUpload_Click);
            // 
            // tsbFileUpload
            // 
            this.tsbFileUpload.Image = ((System.Drawing.Image)(resources.GetObject("tsbFileUpload.Image")));
            this.tsbFileUpload.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbFileUpload.Name = "tsbFileUpload";
            this.tsbFileUpload.Size = new System.Drawing.Size(94, 20);
            this.tsbFileUpload.Text = "File upload...";
            this.tsbFileUpload.Click += new System.EventHandler(this.tsbFileUpload_Click);
            // 
            // tsbCopy
            // 
            this.tsbCopy.Image = ((System.Drawing.Image)(resources.GetObject("tsbCopy.Image")));
            this.tsbCopy.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbCopy.Name = "tsbCopy";
            this.tsbCopy.Size = new System.Drawing.Size(55, 20);
            this.tsbCopy.Text = "Copy";
            this.tsbCopy.Click += new System.EventHandler(this.tsbCopy_Click);
            // 
            // tsbOpen
            // 
            this.tsbOpen.Image = ((System.Drawing.Image)(resources.GetObject("tsbOpen.Image")));
            this.tsbOpen.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbOpen.Name = "tsbOpen";
            this.tsbOpen.Size = new System.Drawing.Size(56, 20);
            this.tsbOpen.Text = "Open";
            this.tsbOpen.Click += new System.EventHandler(this.tsbOpen_Click);
            // 
            // tsbSettings
            // 
            this.tsbSettings.Image = ((System.Drawing.Image)(resources.GetObject("tsbSettings.Image")));
            this.tsbSettings.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbSettings.Name = "tsbSettings";
            this.tsbSettings.Size = new System.Drawing.Size(78, 20);
            this.tsbSettings.Text = "Settings...";
            this.tsbSettings.Click += new System.EventHandler(this.tsbSettings_Click);
            // 
            // tsbAbout
            // 
            this.tsbAbout.Image = ((System.Drawing.Image)(resources.GetObject("tsbAbout.Image")));
            this.tsbAbout.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbAbout.Name = "tsbAbout";
            this.tsbAbout.Size = new System.Drawing.Size(60, 20);
            this.tsbAbout.Text = "About";
            this.tsbAbout.Click += new System.EventHandler(this.tsbAbout_Click);
            // 
            // tss1
            // 
            this.tss1.Name = "tss1";
            this.tss1.Size = new System.Drawing.Size(6, 23);
            // 
            // tsddbImageUploaders
            // 
            this.tsddbImageUploaders.Image = ((System.Drawing.Image)(resources.GetObject("tsddbImageUploaders.Image")));
            this.tsddbImageUploaders.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsddbImageUploaders.Name = "tsddbImageUploaders";
            this.tsddbImageUploaders.Size = new System.Drawing.Size(124, 20);
            this.tsddbImageUploaders.Text = "Image uploaders";
            // 
            // tsddbFileUploaders
            // 
            this.tsddbFileUploaders.Image = ((System.Drawing.Image)(resources.GetObject("tsddbFileUploaders.Image")));
            this.tsddbFileUploaders.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsddbFileUploaders.Name = "tsddbFileUploaders";
            this.tsddbFileUploaders.Size = new System.Drawing.Size(109, 20);
            this.tsddbFileUploaders.Text = "File uploaders";
            // 
            // tsddbTextUploaders
            // 
            this.tsddbTextUploaders.Image = ((System.Drawing.Image)(resources.GetObject("tsddbTextUploaders.Image")));
            this.tsddbTextUploaders.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsddbTextUploaders.Name = "tsddbTextUploaders";
            this.tsddbTextUploaders.Size = new System.Drawing.Size(113, 20);
            this.tsddbTextUploaders.Text = "Text uploaders";
            // 
            // MainForm
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(954, 412);
            this.Controls.Add(this.lvUploads);
            this.Controls.Add(this.tsMain);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(970, 450);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ZUploader";
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.MainForm_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.MainForm_DragEnter);
            this.Resize += new System.EventHandler(this.MainForm_Resize);
            this.cmsUploads.ResumeLayout(false);
            this.tsMain.ResumeLayout(false);
            this.tsMain.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView lvUploads;
        private System.Windows.Forms.ColumnHeader chID;
        private System.Windows.Forms.ColumnHeader chStatus;
        private System.Windows.Forms.ColumnHeader chURL;
        private System.Windows.Forms.ContextMenuStrip cmsUploads;
        private System.Windows.Forms.ToolStripMenuItem copyURLToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openURLToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyThumbnailURLToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyDeletionURLToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyErrorsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem uploadFileToolStripMenuItem;
        private System.Windows.Forms.ColumnHeader chFilePath;
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
    }
}


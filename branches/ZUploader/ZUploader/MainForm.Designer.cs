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
            this.lblImageUploaderDestination = new System.Windows.Forms.Label();
            this.cbImageUploaderDestination = new System.Windows.Forms.ComboBox();
            this.btnClipboardUpload = new System.Windows.Forms.Button();
            this.lvUploads = new System.Windows.Forms.ListView();
            this.chID = new System.Windows.Forms.ColumnHeader();
            this.chStatus = new System.Windows.Forms.ColumnHeader();
            this.chFilePath = new System.Windows.Forms.ColumnHeader();
            this.chProgress = new System.Windows.Forms.ColumnHeader();
            this.chUploaderType = new System.Windows.Forms.ColumnHeader();
            this.chUploaderName = new System.Windows.Forms.ColumnHeader();
            this.chURL = new System.Windows.Forms.ColumnHeader();
            this.cmsUploads = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.openURLToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyURLToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyThumbnailURLToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyDeletionURLToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyErrorsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.uploadFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lblTextUploaderDestination = new System.Windows.Forms.Label();
            this.cbTextUploaderDestination = new System.Windows.Forms.ComboBox();
            this.lblFileUploaderDestination = new System.Windows.Forms.Label();
            this.cbFileUploaderDestination = new System.Windows.Forms.ComboBox();
            this.btnCopy = new System.Windows.Forms.Button();
            this.cbAutoPlaySound = new System.Windows.Forms.CheckBox();
            this.cbClipboardAutoCopy = new System.Windows.Forms.CheckBox();
            this.llblBugReports = new System.Windows.Forms.LinkLabel();
            this.llWebsite = new System.Windows.Forms.LinkLabel();
            this.btnOpen = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnOpenFTPSettings = new System.Windows.Forms.Button();
            this.cmsUploads.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblImageUploaderDestination
            // 
            this.lblImageUploaderDestination.AutoSize = true;
            this.lblImageUploaderDestination.Location = new System.Drawing.Point(8, 12);
            this.lblImageUploaderDestination.Name = "lblImageUploaderDestination";
            this.lblImageUploaderDestination.Size = new System.Drawing.Size(90, 13);
            this.lblImageUploaderDestination.TabIndex = 0;
            this.lblImageUploaderDestination.Text = "Image Uploaders:";
            // 
            // cbImageUploaderDestination
            // 
            this.cbImageUploaderDestination.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbImageUploaderDestination.FormattingEnabled = true;
            this.cbImageUploaderDestination.Location = new System.Drawing.Point(104, 8);
            this.cbImageUploaderDestination.Name = "cbImageUploaderDestination";
            this.cbImageUploaderDestination.Size = new System.Drawing.Size(256, 21);
            this.cbImageUploaderDestination.TabIndex = 1;
            this.cbImageUploaderDestination.SelectedIndexChanged += new System.EventHandler(this.cbImageUploaderDestination_SelectedIndexChanged);
            // 
            // btnClipboardUpload
            // 
            this.btnClipboardUpload.Location = new System.Drawing.Point(368, 8);
            this.btnClipboardUpload.Name = "btnClipboardUpload";
            this.btnClipboardUpload.Size = new System.Drawing.Size(112, 40);
            this.btnClipboardUpload.TabIndex = 2;
            this.btnClipboardUpload.Text = "Clipboard Upload";
            this.btnClipboardUpload.UseVisualStyleBackColor = true;
            this.btnClipboardUpload.Click += new System.EventHandler(this.btnClipboardUpload_Click);
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
            this.chUploaderName,
            this.chURL});
            this.lvUploads.ContextMenuStrip = this.cmsUploads;
            this.lvUploads.FullRowSelect = true;
            this.lvUploads.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lvUploads.HideSelection = false;
            this.lvUploads.Location = new System.Drawing.Point(8, 88);
            this.lvUploads.Name = "lvUploads";
            this.lvUploads.Size = new System.Drawing.Size(936, 312);
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
            this.chStatus.Width = 125;
            // 
            // chFilePath
            // 
            this.chFilePath.Text = "File path";
            this.chFilePath.Width = 150;
            // 
            // chProgress
            // 
            this.chProgress.Text = "Progress";
            this.chProgress.Width = 125;
            // 
            // chUploaderType
            // 
            this.chUploaderType.Text = "Uploader type";
            this.chUploaderType.Width = 80;
            // 
            // chUploaderName
            // 
            this.chUploaderName.Text = "Uploader name";
            this.chUploaderName.Width = 125;
            // 
            // chURL
            // 
            this.chURL.Text = "URL";
            this.chURL.Width = 300;
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
            // lblTextUploaderDestination
            // 
            this.lblTextUploaderDestination.AutoSize = true;
            this.lblTextUploaderDestination.Location = new System.Drawing.Point(8, 36);
            this.lblTextUploaderDestination.Name = "lblTextUploaderDestination";
            this.lblTextUploaderDestination.Size = new System.Drawing.Size(82, 13);
            this.lblTextUploaderDestination.TabIndex = 4;
            this.lblTextUploaderDestination.Text = "Text Uploaders:";
            // 
            // cbTextUploaderDestination
            // 
            this.cbTextUploaderDestination.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbTextUploaderDestination.FormattingEnabled = true;
            this.cbTextUploaderDestination.Location = new System.Drawing.Point(104, 32);
            this.cbTextUploaderDestination.Name = "cbTextUploaderDestination";
            this.cbTextUploaderDestination.Size = new System.Drawing.Size(256, 21);
            this.cbTextUploaderDestination.TabIndex = 1;
            this.cbTextUploaderDestination.SelectedIndexChanged += new System.EventHandler(this.cbTextUploaderDestination_SelectedIndexChanged);
            // 
            // lblFileUploaderDestination
            // 
            this.lblFileUploaderDestination.AutoSize = true;
            this.lblFileUploaderDestination.Location = new System.Drawing.Point(8, 60);
            this.lblFileUploaderDestination.Name = "lblFileUploaderDestination";
            this.lblFileUploaderDestination.Size = new System.Drawing.Size(77, 13);
            this.lblFileUploaderDestination.TabIndex = 5;
            this.lblFileUploaderDestination.Text = "File Uploaders:";
            // 
            // cbFileUploaderDestination
            // 
            this.cbFileUploaderDestination.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbFileUploaderDestination.FormattingEnabled = true;
            this.cbFileUploaderDestination.Location = new System.Drawing.Point(104, 56);
            this.cbFileUploaderDestination.Name = "cbFileUploaderDestination";
            this.cbFileUploaderDestination.Size = new System.Drawing.Size(256, 21);
            this.cbFileUploaderDestination.TabIndex = 1;
            this.cbFileUploaderDestination.SelectedIndexChanged += new System.EventHandler(this.cbFileUploaderDestination_SelectedIndexChanged);
            // 
            // btnCopy
            // 
            this.btnCopy.Enabled = false;
            this.btnCopy.Location = new System.Drawing.Point(368, 48);
            this.btnCopy.Name = "btnCopy";
            this.btnCopy.Size = new System.Drawing.Size(56, 32);
            this.btnCopy.TabIndex = 6;
            this.btnCopy.Text = "Copy";
            this.btnCopy.UseVisualStyleBackColor = true;
            this.btnCopy.Click += new System.EventHandler(this.btnCopy_Click);
            // 
            // cbAutoPlaySound
            // 
            this.cbAutoPlaySound.AutoSize = true;
            this.cbAutoPlaySound.Location = new System.Drawing.Point(11, 43);
            this.cbAutoPlaySound.Name = "cbAutoPlaySound";
            this.cbAutoPlaySound.Size = new System.Drawing.Size(199, 17);
            this.cbAutoPlaySound.TabIndex = 1;
            this.cbAutoPlaySound.Text = "Play sound after upload is completed";
            this.cbAutoPlaySound.UseVisualStyleBackColor = true;
            this.cbAutoPlaySound.CheckedChanged += new System.EventHandler(this.cbAutoPlaySound_CheckedChanged);
            // 
            // cbClipboardAutoCopy
            // 
            this.cbClipboardAutoCopy.AutoSize = true;
            this.cbClipboardAutoCopy.Location = new System.Drawing.Point(11, 19);
            this.cbClipboardAutoCopy.Name = "cbClipboardAutoCopy";
            this.cbClipboardAutoCopy.Size = new System.Drawing.Size(254, 17);
            this.cbClipboardAutoCopy.TabIndex = 0;
            this.cbClipboardAutoCopy.Text = "Copy URL to clipboard after upload is completed";
            this.cbClipboardAutoCopy.UseVisualStyleBackColor = true;
            this.cbClipboardAutoCopy.CheckedChanged += new System.EventHandler(this.cbClipboardAutoCopy_CheckedChanged);
            // 
            // llblBugReports
            // 
            this.llblBugReports.AutoSize = true;
            this.llblBugReports.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline;
            this.llblBugReports.Location = new System.Drawing.Point(792, 64);
            this.llblBugReports.Name = "llblBugReports";
            this.llblBugReports.Size = new System.Drawing.Size(100, 13);
            this.llblBugReports.TabIndex = 84;
            this.llblBugReports.TabStop = true;
            this.llblBugReports.Text = "Bugs/Suggestions?";
            this.llblBugReports.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llblBugReports_LinkClicked);
            // 
            // llWebsite
            // 
            this.llWebsite.AutoSize = true;
            this.llWebsite.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline;
            this.llWebsite.Location = new System.Drawing.Point(824, 48);
            this.llWebsite.Name = "llWebsite";
            this.llWebsite.Size = new System.Drawing.Size(66, 13);
            this.llWebsite.TabIndex = 83;
            this.llWebsite.TabStop = true;
            this.llWebsite.Text = "ZScreen.net";
            this.llWebsite.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llWebsite_LinkClicked);
            // 
            // btnOpen
            // 
            this.btnOpen.Enabled = false;
            this.btnOpen.Location = new System.Drawing.Point(424, 48);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(56, 32);
            this.btnOpen.TabIndex = 8;
            this.btnOpen.Text = "Open";
            this.btnOpen.UseVisualStyleBackColor = true;
            this.btnOpen.Click += new System.EventHandler(this.btnOpen_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cbClipboardAutoCopy);
            this.groupBox1.Controls.Add(this.cbAutoPlaySound);
            this.groupBox1.Location = new System.Drawing.Point(488, 8);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(288, 72);
            this.groupBox1.TabIndex = 85;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Settings";
            // 
            // btnOpenFTPSettings
            // 
            this.btnOpenFTPSettings.Location = new System.Drawing.Point(784, 8);
            this.btnOpenFTPSettings.Name = "btnOpenFTPSettings";
            this.btnOpenFTPSettings.Size = new System.Drawing.Size(112, 32);
            this.btnOpenFTPSettings.TabIndex = 86;
            this.btnOpenFTPSettings.Text = "Open FTP Settings";
            this.btnOpenFTPSettings.UseVisualStyleBackColor = true;
            this.btnOpenFTPSettings.Click += new System.EventHandler(this.btnOpenFTPSettings_Click);
            // 
            // MainForm
            // 
            this.AcceptButton = this.btnClipboardUpload;
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(951, 407);
            this.Controls.Add(this.lvUploads);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.llblBugReports);
            this.Controls.Add(this.btnOpenFTPSettings);
            this.Controls.Add(this.llWebsite);
            this.Controls.Add(this.lblTextUploaderDestination);
            this.Controls.Add(this.lblFileUploaderDestination);
            this.Controls.Add(this.btnOpen);
            this.Controls.Add(this.btnCopy);
            this.Controls.Add(this.btnClipboardUpload);
            this.Controls.Add(this.cbImageUploaderDestination);
            this.Controls.Add(this.lblImageUploaderDestination);
            this.Controls.Add(this.cbFileUploaderDestination);
            this.Controls.Add(this.cbTextUploaderDestination);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ZUploader";
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.MainForm_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.MainForm_DragEnter);
            this.Resize += new System.EventHandler(this.MainForm_Resize);
            this.cmsUploads.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblImageUploaderDestination;
        private System.Windows.Forms.ComboBox cbImageUploaderDestination;
        private System.Windows.Forms.Button btnClipboardUpload;
        private System.Windows.Forms.ListView lvUploads;
        private System.Windows.Forms.ColumnHeader chID;
        private System.Windows.Forms.ColumnHeader chStatus;
        private System.Windows.Forms.ColumnHeader chURL;
        private System.Windows.Forms.Label lblTextUploaderDestination;
        private System.Windows.Forms.ComboBox cbTextUploaderDestination;
        private System.Windows.Forms.Label lblFileUploaderDestination;
        private System.Windows.Forms.ComboBox cbFileUploaderDestination;
        private System.Windows.Forms.ContextMenuStrip cmsUploads;
        private System.Windows.Forms.ToolStripMenuItem copyURLToolStripMenuItem;
        private System.Windows.Forms.Button btnCopy;
        private System.Windows.Forms.ToolStripMenuItem openURLToolStripMenuItem;
        private System.Windows.Forms.Button btnOpen;
        private System.Windows.Forms.ToolStripMenuItem copyThumbnailURLToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyDeletionURLToolStripMenuItem;
        internal System.Windows.Forms.LinkLabel llblBugReports;
        internal System.Windows.Forms.LinkLabel llWebsite;
        private System.Windows.Forms.CheckBox cbClipboardAutoCopy;
        private System.Windows.Forms.ToolStripMenuItem copyErrorsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem uploadFileToolStripMenuItem;
        private System.Windows.Forms.CheckBox cbAutoPlaySound;
        private System.Windows.Forms.ColumnHeader chFilePath;
        private System.Windows.Forms.ColumnHeader chProgress;
        private System.Windows.Forms.ColumnHeader chUploaderName;
        private System.Windows.Forms.ColumnHeader chUploaderType;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnOpenFTPSettings;
    }
}


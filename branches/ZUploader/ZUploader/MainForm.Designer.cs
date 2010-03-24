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
            this.lblImageUploaderDestination = new System.Windows.Forms.Label();
            this.cbImageUploaderDestination = new System.Windows.Forms.ComboBox();
            this.btnClipboardUpload = new System.Windows.Forms.Button();
            this.lvUploads = new System.Windows.Forms.ListView();
            this.chID = new System.Windows.Forms.ColumnHeader();
            this.chStatus = new System.Windows.Forms.ColumnHeader();
            this.chURL = new System.Windows.Forms.ColumnHeader();
            this.cmsUploads = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.copyURLToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openURLToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lblTextUploaderDestination = new System.Windows.Forms.Label();
            this.cbTextUploaderDestination = new System.Windows.Forms.ComboBox();
            this.lblFileUploaderDestination = new System.Windows.Forms.Label();
            this.cbFileUploaderDestination = new System.Windows.Forms.ComboBox();
            this.btnCopy = new System.Windows.Forms.Button();
            this.tcApp = new System.Windows.Forms.TabControl();
            this.tpHistory = new System.Windows.Forms.TabPage();
            this.tpFTP = new System.Windows.Forms.TabPage();
            this.pgFTPAccount = new System.Windows.Forms.PropertyGrid();
            this.tpOptions = new System.Windows.Forms.TabPage();
            this.pgApp = new System.Windows.Forms.PropertyGrid();
            this.btnOpen = new System.Windows.Forms.Button();
            this.cmsUploads.SuspendLayout();
            this.tcApp.SuspendLayout();
            this.tpHistory.SuspendLayout();
            this.tpFTP.SuspendLayout();
            this.tpOptions.SuspendLayout();
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
            this.lvUploads.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chID,
            this.chStatus,
            this.chURL});
            this.lvUploads.ContextMenuStrip = this.cmsUploads;
            this.lvUploads.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvUploads.FullRowSelect = true;
            this.lvUploads.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lvUploads.HideSelection = false;
            this.lvUploads.Location = new System.Drawing.Point(3, 3);
            this.lvUploads.Name = "lvUploads";
            this.lvUploads.Size = new System.Drawing.Size(458, 240);
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
            // chURL
            // 
            this.chURL.Text = "URL";
            this.chURL.Width = 300;
            // 
            // cmsUploads
            // 
            this.cmsUploads.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.copyURLToolStripMenuItem,
            this.openURLToolStripMenuItem});
            this.cmsUploads.Name = "cmsUploads";
            this.cmsUploads.Size = new System.Drawing.Size(128, 48);
            // 
            // copyURLToolStripMenuItem
            // 
            this.copyURLToolStripMenuItem.Name = "copyURLToolStripMenuItem";
            this.copyURLToolStripMenuItem.Size = new System.Drawing.Size(127, 22);
            this.copyURLToolStripMenuItem.Text = "Copy URL";
            this.copyURLToolStripMenuItem.Click += new System.EventHandler(this.copyURLToolStripMenuItem_Click);
            // 
            // openURLToolStripMenuItem
            // 
            this.openURLToolStripMenuItem.Name = "openURLToolStripMenuItem";
            this.openURLToolStripMenuItem.Size = new System.Drawing.Size(127, 22);
            this.openURLToolStripMenuItem.Text = "Open URL";
            this.openURLToolStripMenuItem.Click += new System.EventHandler(this.openURLToolStripMenuItem_Click);
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
            // tcApp
            // 
            this.tcApp.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tcApp.Controls.Add(this.tpHistory);
            this.tcApp.Controls.Add(this.tpFTP);
            this.tcApp.Controls.Add(this.tpOptions);
            this.tcApp.Location = new System.Drawing.Point(8, 88);
            this.tcApp.Name = "tcApp";
            this.tcApp.SelectedIndex = 0;
            this.tcApp.Size = new System.Drawing.Size(472, 272);
            this.tcApp.TabIndex = 7;
            // 
            // tpHistory
            // 
            this.tpHistory.Controls.Add(this.lvUploads);
            this.tpHistory.Location = new System.Drawing.Point(4, 22);
            this.tpHistory.Name = "tpHistory";
            this.tpHistory.Padding = new System.Windows.Forms.Padding(3);
            this.tpHistory.Size = new System.Drawing.Size(464, 246);
            this.tpHistory.TabIndex = 0;
            this.tpHistory.Text = "History";
            this.tpHistory.UseVisualStyleBackColor = true;
            // 
            // tpFTP
            // 
            this.tpFTP.Controls.Add(this.pgFTPAccount);
            this.tpFTP.Location = new System.Drawing.Point(4, 22);
            this.tpFTP.Name = "tpFTP";
            this.tpFTP.Padding = new System.Windows.Forms.Padding(3);
            this.tpFTP.Size = new System.Drawing.Size(464, 246);
            this.tpFTP.TabIndex = 2;
            this.tpFTP.Text = "FTP Account";
            this.tpFTP.UseVisualStyleBackColor = true;
            // 
            // pgFTPAccount
            // 
            this.pgFTPAccount.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pgFTPAccount.Location = new System.Drawing.Point(3, 3);
            this.pgFTPAccount.Name = "pgFTPAccount";
            this.pgFTPAccount.PropertySort = System.Windows.Forms.PropertySort.NoSort;
            this.pgFTPAccount.Size = new System.Drawing.Size(458, 240);
            this.pgFTPAccount.TabIndex = 0;
            this.pgFTPAccount.ToolbarVisible = false;
            // 
            // tpOptions
            // 
            this.tpOptions.Controls.Add(this.pgApp);
            this.tpOptions.Location = new System.Drawing.Point(4, 22);
            this.tpOptions.Name = "tpOptions";
            this.tpOptions.Padding = new System.Windows.Forms.Padding(3);
            this.tpOptions.Size = new System.Drawing.Size(464, 246);
            this.tpOptions.TabIndex = 1;
            this.tpOptions.Text = "Options";
            this.tpOptions.UseVisualStyleBackColor = true;
            // 
            // pgApp
            // 
            this.pgApp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pgApp.HelpVisible = false;
            this.pgApp.Location = new System.Drawing.Point(3, 3);
            this.pgApp.Name = "pgApp";
            this.pgApp.PropertySort = System.Windows.Forms.PropertySort.NoSort;
            this.pgApp.Size = new System.Drawing.Size(458, 240);
            this.pgApp.TabIndex = 0;
            this.pgApp.ToolbarVisible = false;
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
            // MainForm
            // 
            this.AcceptButton = this.btnClipboardUpload;
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(488, 367);
            this.Controls.Add(this.btnOpen);
            this.Controls.Add(this.btnCopy);
            this.Controls.Add(this.lblTextUploaderDestination);
            this.Controls.Add(this.btnClipboardUpload);
            this.Controls.Add(this.tcApp);
            this.Controls.Add(this.lblFileUploaderDestination);
            this.Controls.Add(this.cbFileUploaderDestination);
            this.Controls.Add(this.cbTextUploaderDestination);
            this.Controls.Add(this.cbImageUploaderDestination);
            this.Controls.Add(this.lblImageUploaderDestination);
            this.DoubleBuffered = true;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ZUploader";
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.MainForm_DragDrop);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.MainForm_DragEnter);
            this.Resize += new System.EventHandler(this.MainForm_Resize);
            this.cmsUploads.ResumeLayout(false);
            this.tcApp.ResumeLayout(false);
            this.tpHistory.ResumeLayout(false);
            this.tpFTP.ResumeLayout(false);
            this.tpOptions.ResumeLayout(false);
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
        private System.Windows.Forms.TabControl tcApp;
        private System.Windows.Forms.TabPage tpHistory;
        private System.Windows.Forms.TabPage tpOptions;
        private System.Windows.Forms.PropertyGrid pgApp;
        private System.Windows.Forms.ToolStripMenuItem openURLToolStripMenuItem;
        private System.Windows.Forms.Button btnOpen;
        private System.Windows.Forms.TabPage tpFTP;
        private System.Windows.Forms.PropertyGrid pgFTPAccount;
    }
}


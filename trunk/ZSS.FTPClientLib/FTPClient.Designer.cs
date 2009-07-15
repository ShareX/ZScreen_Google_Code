namespace ZSS.FTPClientLib
{
    partial class FTPClient
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
            this.cmsRightClickMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.downloadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.renameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.createDirectoryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.txtRename = new System.Windows.Forms.TextBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tcFTP = new System.Windows.Forms.TabControl();
            this.tpMainTab = new System.Windows.Forms.TabPage();
            this.tpConsole = new System.Windows.Forms.TabPage();
            this.txtConsole = new System.Windows.Forms.TextBox();
            this.cbDirectoryList = new System.Windows.Forms.ComboBox();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.refreshToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lvFTPList = new ZSS.FTPClientLib.ListViewEx();
            this.chFilename = new System.Windows.Forms.ColumnHeader();
            this.chFilesize = new System.Windows.Forms.ColumnHeader();
            this.chFiletype = new System.Windows.Forms.ColumnHeader();
            this.chLastModified = new System.Windows.Forms.ColumnHeader();
            this.chPermissions = new System.Windows.Forms.ColumnHeader();
            this.chOwnerGroup = new System.Windows.Forms.ColumnHeader();
            this.copyURLsToClipboardToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cmsRightClickMenu.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tcFTP.SuspendLayout();
            this.tpMainTab.SuspendLayout();
            this.tpConsole.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmsRightClickMenu
            // 
            this.cmsRightClickMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.downloadToolStripMenuItem,
            this.renameToolStripMenuItem,
            this.deleteToolStripMenuItem,
            this.toolStripSeparator1,
            this.refreshToolStripMenuItem,
            this.createDirectoryToolStripMenuItem,
            this.copyURLsToClipboardToolStripMenuItem});
            this.cmsRightClickMenu.Name = "cmsRightClickMenu";
            this.cmsRightClickMenu.Size = new System.Drawing.Size(207, 164);
            // 
            // downloadToolStripMenuItem
            // 
            this.downloadToolStripMenuItem.Name = "downloadToolStripMenuItem";
            this.downloadToolStripMenuItem.Size = new System.Drawing.Size(206, 22);
            this.downloadToolStripMenuItem.Text = "Download";
            this.downloadToolStripMenuItem.Click += new System.EventHandler(this.downloadToolStripMenuItem_Click);
            // 
            // renameToolStripMenuItem
            // 
            this.renameToolStripMenuItem.Name = "renameToolStripMenuItem";
            this.renameToolStripMenuItem.Size = new System.Drawing.Size(206, 22);
            this.renameToolStripMenuItem.Text = "Rename";
            this.renameToolStripMenuItem.Click += new System.EventHandler(this.renameToolStripMenuItem_Click);
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(206, 22);
            this.deleteToolStripMenuItem.Text = "Delete";
            this.deleteToolStripMenuItem.Click += new System.EventHandler(this.deleteToolStripMenuItem_Click);
            // 
            // createDirectoryToolStripMenuItem
            // 
            this.createDirectoryToolStripMenuItem.Name = "createDirectoryToolStripMenuItem";
            this.createDirectoryToolStripMenuItem.Size = new System.Drawing.Size(206, 22);
            this.createDirectoryToolStripMenuItem.Text = "Create directory";
            this.createDirectoryToolStripMenuItem.Click += new System.EventHandler(this.createDirectoryToolStripMenuItem_Click);
            // 
            // txtRename
            // 
            this.txtRename.Location = new System.Drawing.Point(8, 8);
            this.txtRename.Name = "txtRename";
            this.txtRename.Size = new System.Drawing.Size(100, 20);
            this.txtRename.TabIndex = 1;
            this.txtRename.Visible = false;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(3, 3);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.cbDirectoryList);
            this.splitContainer1.Panel1MinSize = 20;
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.lvFTPList);
            this.splitContainer1.Size = new System.Drawing.Size(952, 557);
            this.splitContainer1.SplitterDistance = 23;
            this.splitContainer1.TabIndex = 2;
            // 
            // tcFTP
            // 
            this.tcFTP.Controls.Add(this.tpMainTab);
            this.tcFTP.Controls.Add(this.tpConsole);
            this.tcFTP.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcFTP.Location = new System.Drawing.Point(3, 3);
            this.tcFTP.Name = "tcFTP";
            this.tcFTP.SelectedIndex = 0;
            this.tcFTP.Size = new System.Drawing.Size(966, 589);
            this.tcFTP.TabIndex = 3;
            // 
            // tpMainTab
            // 
            this.tpMainTab.Controls.Add(this.splitContainer1);
            this.tpMainTab.Location = new System.Drawing.Point(4, 22);
            this.tpMainTab.Name = "tpMainTab";
            this.tpMainTab.Padding = new System.Windows.Forms.Padding(3);
            this.tpMainTab.Size = new System.Drawing.Size(958, 563);
            this.tpMainTab.TabIndex = 0;
            this.tpMainTab.Text = "FTP Client";
            this.tpMainTab.UseVisualStyleBackColor = true;
            // 
            // tpConsole
            // 
            this.tpConsole.Controls.Add(this.txtConsole);
            this.tpConsole.Location = new System.Drawing.Point(4, 22);
            this.tpConsole.Name = "tpConsole";
            this.tpConsole.Padding = new System.Windows.Forms.Padding(3);
            this.tpConsole.Size = new System.Drawing.Size(958, 563);
            this.tpConsole.TabIndex = 1;
            this.tpConsole.Text = "Console";
            this.tpConsole.UseVisualStyleBackColor = true;
            // 
            // txtConsole
            // 
            this.txtConsole.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtConsole.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtConsole.Location = new System.Drawing.Point(3, 3);
            this.txtConsole.Multiline = true;
            this.txtConsole.Name = "txtConsole";
            this.txtConsole.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtConsole.Size = new System.Drawing.Size(952, 557);
            this.txtConsole.TabIndex = 0;
            // 
            // cbDirectoryList
            // 
            this.cbDirectoryList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cbDirectoryList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbDirectoryList.FormattingEnabled = true;
            this.cbDirectoryList.Location = new System.Drawing.Point(0, 0);
            this.cbDirectoryList.Name = "cbDirectoryList";
            this.cbDirectoryList.Size = new System.Drawing.Size(952, 21);
            this.cbDirectoryList.TabIndex = 3;
            this.cbDirectoryList.SelectedIndexChanged += new System.EventHandler(this.cbDirectoryList_SelectedIndexChanged);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(203, 6);
            // 
            // refreshToolStripMenuItem
            // 
            this.refreshToolStripMenuItem.Name = "refreshToolStripMenuItem";
            this.refreshToolStripMenuItem.Size = new System.Drawing.Size(206, 22);
            this.refreshToolStripMenuItem.Text = "Refresh";
            this.refreshToolStripMenuItem.Click += new System.EventHandler(this.refreshToolStripMenuItem_Click);
            // 
            // lvFTPList
            // 
            this.lvFTPList.AllowColumnReorder = true;
            this.lvFTPList.AllowDrop = true;
            this.lvFTPList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chFilename,
            this.chFilesize,
            this.chFiletype,
            this.chLastModified,
            this.chPermissions,
            this.chOwnerGroup});
            this.lvFTPList.ContextMenuStrip = this.cmsRightClickMenu;
            this.lvFTPList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvFTPList.DoubleClickActivation = false;
            this.lvFTPList.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lvFTPList.FullRowSelect = true;
            this.lvFTPList.GridLines = true;
            this.lvFTPList.HideSelection = false;
            this.lvFTPList.Location = new System.Drawing.Point(0, 0);
            this.lvFTPList.Name = "lvFTPList";
            this.lvFTPList.Size = new System.Drawing.Size(952, 530);
            this.lvFTPList.TabIndex = 0;
            this.lvFTPList.UseCompatibleStateImageBehavior = false;
            this.lvFTPList.View = System.Windows.Forms.View.Details;
            this.lvFTPList.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lvFTPList_MouseDoubleClick);
            this.lvFTPList.SelectedIndexChanged += new System.EventHandler(this.lvFTPList_SelectedIndexChanged);
            this.lvFTPList.DragDrop += new System.Windows.Forms.DragEventHandler(this.lvFTPList_DragDrop);
            this.lvFTPList.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.lvFTPList_ItemDrag);
            this.lvFTPList.DragOver += new System.Windows.Forms.DragEventHandler(this.lvFTPList_DragOver);
            // 
            // chFilename
            // 
            this.chFilename.Text = "Filename";
            this.chFilename.Width = 300;
            // 
            // chFilesize
            // 
            this.chFilesize.Text = "Filesize";
            this.chFilesize.Width = 100;
            // 
            // chFiletype
            // 
            this.chFiletype.Text = "Filetype";
            this.chFiletype.Width = 167;
            // 
            // chLastModified
            // 
            this.chLastModified.Text = "Last modified";
            this.chLastModified.Width = 150;
            // 
            // chPermissions
            // 
            this.chPermissions.Text = "Permissions";
            this.chPermissions.Width = 100;
            // 
            // chOwnerGroup
            // 
            this.chOwnerGroup.Text = "Owner/Group";
            this.chOwnerGroup.Width = 100;
            // 
            // copyURLsToClipboardToolStripMenuItem
            // 
            this.copyURLsToClipboardToolStripMenuItem.Name = "copyURLsToClipboardToolStripMenuItem";
            this.copyURLsToClipboardToolStripMenuItem.Size = new System.Drawing.Size(206, 22);
            this.copyURLsToClipboardToolStripMenuItem.Text = "Copy URL(s) to clipboard";
            this.copyURLsToClipboardToolStripMenuItem.Click += new System.EventHandler(this.copyURLsToClipboardToolStripMenuItem_Click);
            // 
            // TestForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(972, 595);
            this.Controls.Add(this.tcFTP);
            this.Controls.Add(this.txtRename);
            this.Name = "TestForm";
            this.Padding = new System.Windows.Forms.Padding(3);
            this.Text = "FTP Test";
            this.cmsRightClickMenu.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.tcFTP.ResumeLayout(false);
            this.tpMainTab.ResumeLayout(false);
            this.tpConsole.ResumeLayout(false);
            this.tpConsole.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ListViewEx lvFTPList;
        private System.Windows.Forms.ColumnHeader chFilename;
        private System.Windows.Forms.ColumnHeader chFilesize;
        private System.Windows.Forms.ColumnHeader chFiletype;
        private System.Windows.Forms.ColumnHeader chLastModified;
        private System.Windows.Forms.ColumnHeader chPermissions;
        private System.Windows.Forms.ColumnHeader chOwnerGroup;
        private System.Windows.Forms.ContextMenuStrip cmsRightClickMenu;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem renameToolStripMenuItem;
        private System.Windows.Forms.TextBox txtRename;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ToolStripMenuItem downloadToolStripMenuItem;
        private System.Windows.Forms.TabControl tcFTP;
        private System.Windows.Forms.TabPage tpMainTab;
        private System.Windows.Forms.TabPage tpConsole;
        private System.Windows.Forms.TextBox txtConsole;
        private System.Windows.Forms.ToolStripMenuItem createDirectoryToolStripMenuItem;
        private System.Windows.Forms.ComboBox cbDirectoryList;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem refreshToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyURLsToClipboardToolStripMenuItem;
    }
}
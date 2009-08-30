namespace ZSS.FTPClientLib
{
    partial class FTPClient2
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
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.refreshToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.createDirectoryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyURLsToClipboardToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.txtRename = new System.Windows.Forms.TextBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.cbDirectoryList = new System.Windows.Forms.ComboBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.tcFTP = new System.Windows.Forms.TabControl();
            this.tpMainTab = new System.Windows.Forms.TabPage();
            this.tpConsole = new System.Windows.Forms.TabPage();
            this.txtConsole = new System.Windows.Forms.TextBox();
            this.openURLToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lvFTPList = new ZSS.FTPClientLib.ListViewEx();
            this.chFilename = new System.Windows.Forms.ColumnHeader();
            this.chFilesize = new System.Windows.Forms.ColumnHeader();
            this.chFiletype = new System.Windows.Forms.ColumnHeader();
            this.chLastModified = new System.Windows.Forms.ColumnHeader();
            this.chPermissions = new System.Windows.Forms.ColumnHeader();
            this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.cmsRightClickMenu.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tcFTP.SuspendLayout();
            this.tpMainTab.SuspendLayout();
            this.tpConsole.SuspendLayout();
            this.toolStripContainer1.BottomToolStripPanel.SuspendLayout();
            this.toolStripContainer1.ContentPanel.SuspendLayout();
            this.toolStripContainer1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
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
            this.openURLToolStripMenuItem,
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
            // createDirectoryToolStripMenuItem
            // 
            this.createDirectoryToolStripMenuItem.Name = "createDirectoryToolStripMenuItem";
            this.createDirectoryToolStripMenuItem.Size = new System.Drawing.Size(206, 22);
            this.createDirectoryToolStripMenuItem.Text = "Create directory";
            this.createDirectoryToolStripMenuItem.Click += new System.EventHandler(this.createDirectoryToolStripMenuItem_Click);
            // 
            // copyURLsToClipboardToolStripMenuItem
            // 
            this.copyURLsToClipboardToolStripMenuItem.Name = "copyURLsToClipboardToolStripMenuItem";
            this.copyURLsToClipboardToolStripMenuItem.Size = new System.Drawing.Size(206, 22);
            this.copyURLsToClipboardToolStripMenuItem.Text = "Copy URL(s) to clipboard";
            this.copyURLsToClipboardToolStripMenuItem.Click += new System.EventHandler(this.copyURLsToClipboardToolStripMenuItem_Click);
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
            this.splitContainer1.Panel2.Controls.Add(this.panel1);
            this.splitContainer1.Panel2.Controls.Add(this.toolStripContainer1);
            this.splitContainer1.Size = new System.Drawing.Size(952, 557);
            this.splitContainer1.SplitterDistance = 23;
            this.splitContainer1.TabIndex = 2;
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
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.progressBar1);
            this.panel1.Location = new System.Drawing.Point(380, 208);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(5);
            this.panel1.Size = new System.Drawing.Size(192, 64);
            this.panel1.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(5, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(178, 26);
            this.label1.TabIndex = 1;
            this.label1.Text = "Connecting to FTP server...";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // progressBar1
            // 
            this.progressBar1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.progressBar1.Location = new System.Drawing.Point(5, 31);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(178, 24);
            this.progressBar1.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.progressBar1.TabIndex = 0;
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
            this.txtConsole.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtConsole.Location = new System.Drawing.Point(3, 3);
            this.txtConsole.Multiline = true;
            this.txtConsole.Name = "txtConsole";
            this.txtConsole.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtConsole.Size = new System.Drawing.Size(952, 557);
            this.txtConsole.TabIndex = 0;
            // 
            // openURLToolStripMenuItem
            // 
            this.openURLToolStripMenuItem.Name = "openURLToolStripMenuItem";
            this.openURLToolStripMenuItem.Size = new System.Drawing.Size(206, 22);
            this.openURLToolStripMenuItem.Text = "Open URL";
            this.openURLToolStripMenuItem.Click += new System.EventHandler(this.openURLToolStripMenuItem_Click);
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
            this.chPermissions});
            this.lvFTPList.ContextMenuStrip = this.cmsRightClickMenu;
            this.lvFTPList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvFTPList.DoubleClickActivation = false;
            this.lvFTPList.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lvFTPList.FullRowSelect = true;
            this.lvFTPList.GridLines = true;
            this.lvFTPList.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lvFTPList.HideSelection = false;
            this.lvFTPList.Location = new System.Drawing.Point(0, 0);
            this.lvFTPList.Name = "lvFTPList";
            this.lvFTPList.Size = new System.Drawing.Size(952, 508);
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
            this.chFilename.Width = 350;
            // 
            // chFilesize
            // 
            this.chFilesize.Text = "Filesize";
            this.chFilesize.Width = 100;
            // 
            // chFiletype
            // 
            this.chFiletype.Text = "Filetype";
            this.chFiletype.Width = 200;
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
            // toolStripContainer1
            // 
            // 
            // toolStripContainer1.BottomToolStripPanel
            // 
            this.toolStripContainer1.BottomToolStripPanel.Controls.Add(this.statusStrip1);
            // 
            // toolStripContainer1.ContentPanel
            // 
            this.toolStripContainer1.ContentPanel.Controls.Add(this.lvFTPList);
            this.toolStripContainer1.ContentPanel.Size = new System.Drawing.Size(952, 508);
            this.toolStripContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStripContainer1.Location = new System.Drawing.Point(0, 0);
            this.toolStripContainer1.Name = "toolStripContainer1";
            this.toolStripContainer1.Size = new System.Drawing.Size(952, 530);
            this.toolStripContainer1.TabIndex = 2;
            this.toolStripContainer1.Text = "toolStripContainer1";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 0);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.statusStrip1.Size = new System.Drawing.Size(952, 22);
            this.statusStrip1.SizingGrip = false;
            this.statusStrip1.TabIndex = 0;
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(38, 17);
            this.toolStripStatusLabel1.Text = "status";
            // 
            // FTPClient2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(972, 595);
            this.Controls.Add(this.tcFTP);
            this.Controls.Add(this.txtRename);
            this.Name = "FTPClient2";
            this.Padding = new System.Windows.Forms.Padding(3);
            this.Text = "FTP Client";
            this.Resize += new System.EventHandler(this.FTPClient_Resize);
            this.cmsRightClickMenu.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.tcFTP.ResumeLayout(false);
            this.tpMainTab.ResumeLayout(false);
            this.tpConsole.ResumeLayout(false);
            this.tpConsole.PerformLayout();
            this.toolStripContainer1.BottomToolStripPanel.ResumeLayout(false);
            this.toolStripContainer1.BottomToolStripPanel.PerformLayout();
            this.toolStripContainer1.ContentPanel.ResumeLayout(false);
            this.toolStripContainer1.ResumeLayout(false);
            this.toolStripContainer1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
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
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.ToolStripMenuItem openURLToolStripMenuItem;
        private System.Windows.Forms.ToolStripContainer toolStripContainer1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
    }
}
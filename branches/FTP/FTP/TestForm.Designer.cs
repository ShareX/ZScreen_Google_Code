namespace FTPTest
{
    partial class TestForm
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
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.renameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.txtRename = new System.Windows.Forms.TextBox();
            this.txtCurrentDirectory = new System.Windows.Forms.TextBox();
            this.btnNavigateBack = new System.Windows.Forms.Button();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.downloadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lvFTPList = new FTPTest.ListViewEx();
            this.chFilename = new System.Windows.Forms.ColumnHeader();
            this.chFilesize = new System.Windows.Forms.ColumnHeader();
            this.chFiletype = new System.Windows.Forms.ColumnHeader();
            this.chLastModified = new System.Windows.Forms.ColumnHeader();
            this.chPermissions = new System.Windows.Forms.ColumnHeader();
            this.chOwnerGroup = new System.Windows.Forms.ColumnHeader();
            this.tcFTP = new System.Windows.Forms.TabControl();
            this.tpMainTab = new System.Windows.Forms.TabPage();
            this.tpConsole = new System.Windows.Forms.TabPage();
            this.txtConsole = new System.Windows.Forms.TextBox();
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
            this.deleteToolStripMenuItem});
            this.cmsRightClickMenu.Name = "cmsRightClickMenu";
            this.cmsRightClickMenu.Size = new System.Drawing.Size(129, 70);
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(128, 22);
            this.deleteToolStripMenuItem.Text = "Delete";
            this.deleteToolStripMenuItem.Click += new System.EventHandler(this.deleteToolStripMenuItem_Click);
            // 
            // renameToolStripMenuItem
            // 
            this.renameToolStripMenuItem.Name = "renameToolStripMenuItem";
            this.renameToolStripMenuItem.Size = new System.Drawing.Size(128, 22);
            this.renameToolStripMenuItem.Text = "Rename";
            this.renameToolStripMenuItem.Click += new System.EventHandler(this.renameToolStripMenuItem_Click);
            // 
            // txtRename
            // 
            this.txtRename.Location = new System.Drawing.Point(8, 8);
            this.txtRename.Name = "txtRename";
            this.txtRename.Size = new System.Drawing.Size(100, 20);
            this.txtRename.TabIndex = 1;
            this.txtRename.Visible = false;
            // 
            // txtCurrentDirectory
            // 
            this.txtCurrentDirectory.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCurrentDirectory.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCurrentDirectory.Location = new System.Drawing.Point(88, 8);
            this.txtCurrentDirectory.Name = "txtCurrentDirectory";
            this.txtCurrentDirectory.ReadOnly = true;
            this.txtCurrentDirectory.Size = new System.Drawing.Size(858, 21);
            this.txtCurrentDirectory.TabIndex = 0;
            // 
            // btnNavigateBack
            // 
            this.btnNavigateBack.Location = new System.Drawing.Point(8, 8);
            this.btnNavigateBack.Name = "btnNavigateBack";
            this.btnNavigateBack.Size = new System.Drawing.Size(71, 22);
            this.btnNavigateBack.TabIndex = 1;
            this.btnNavigateBack.Text = "Back";
            this.btnNavigateBack.UseVisualStyleBackColor = true;
            this.btnNavigateBack.Click += new System.EventHandler(this.btnNavigateBack_Click);
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
            this.splitContainer1.Panel1.Controls.Add(this.txtCurrentDirectory);
            this.splitContainer1.Panel1.Controls.Add(this.btnNavigateBack);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.lvFTPList);
            this.splitContainer1.Size = new System.Drawing.Size(958, 563);
            this.splitContainer1.SplitterDistance = 39;
            this.splitContainer1.TabIndex = 2;
            // 
            // downloadToolStripMenuItem
            // 
            this.downloadToolStripMenuItem.Name = "downloadToolStripMenuItem";
            this.downloadToolStripMenuItem.Size = new System.Drawing.Size(128, 22);
            this.downloadToolStripMenuItem.Text = "Download";
            this.downloadToolStripMenuItem.Click += new System.EventHandler(this.downloadToolStripMenuItem_Click);
            // 
            // lvFTPList
            // 
            this.lvFTPList.AllowColumnReorder = true;
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
            this.lvFTPList.Size = new System.Drawing.Size(958, 520);
            this.lvFTPList.TabIndex = 0;
            this.lvFTPList.UseCompatibleStateImageBehavior = false;
            this.lvFTPList.View = System.Windows.Forms.View.Details;
            this.lvFTPList.DoubleClick += new System.EventHandler(this.lvFTPList_DoubleClick);
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
            this.chFiletype.Width = 100;
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
            // tcFTP
            // 
            this.tcFTP.Controls.Add(this.tpMainTab);
            this.tcFTP.Controls.Add(this.tpConsole);
            this.tcFTP.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcFTP.Location = new System.Drawing.Point(0, 0);
            this.tcFTP.Name = "tcFTP";
            this.tcFTP.SelectedIndex = 0;
            this.tcFTP.Size = new System.Drawing.Size(972, 595);
            this.tcFTP.TabIndex = 3;
            // 
            // tpMainTab
            // 
            this.tpMainTab.Controls.Add(this.splitContainer1);
            this.tpMainTab.Location = new System.Drawing.Point(4, 22);
            this.tpMainTab.Name = "tpMainTab";
            this.tpMainTab.Padding = new System.Windows.Forms.Padding(3);
            this.tpMainTab.Size = new System.Drawing.Size(964, 569);
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
            this.tpConsole.Size = new System.Drawing.Size(964, 569);
            this.tpConsole.TabIndex = 1;
            this.tpConsole.Text = "Console";
            this.tpConsole.UseVisualStyleBackColor = true;
            // 
            // txtConsole
            // 
            this.txtConsole.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtConsole.Location = new System.Drawing.Point(3, 3);
            this.txtConsole.Multiline = true;
            this.txtConsole.Name = "txtConsole";
            this.txtConsole.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtConsole.Size = new System.Drawing.Size(958, 563);
            this.txtConsole.TabIndex = 0;
            // 
            // TestForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(972, 595);
            this.Controls.Add(this.tcFTP);
            this.Controls.Add(this.txtRename);
            this.Name = "TestForm";
            this.Text = "FTP Test";
            this.cmsRightClickMenu.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
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
        private System.Windows.Forms.TextBox txtCurrentDirectory;
        private System.Windows.Forms.Button btnNavigateBack;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ToolStripMenuItem downloadToolStripMenuItem;
        private System.Windows.Forms.TabControl tcFTP;
        private System.Windows.Forms.TabPage tpMainTab;
        private System.Windows.Forms.TabPage tpConsole;
        private System.Windows.Forms.TextBox txtConsole;
    }
}
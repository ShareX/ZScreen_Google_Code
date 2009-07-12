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
            this.lvFTPList = new FTPTest.ListViewEx();
            this.chFilename = new System.Windows.Forms.ColumnHeader();
            this.chFilesize = new System.Windows.Forms.ColumnHeader();
            this.chFiletype = new System.Windows.Forms.ColumnHeader();
            this.chLastModified = new System.Windows.Forms.ColumnHeader();
            this.chPermissions = new System.Windows.Forms.ColumnHeader();
            this.chOwnerGroup = new System.Windows.Forms.ColumnHeader();
            this.cmsRightClickMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmsRightClickMenu
            // 
            this.cmsRightClickMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.deleteToolStripMenuItem,
            this.renameToolStripMenuItem});
            this.cmsRightClickMenu.Name = "cmsRightClickMenu";
            this.cmsRightClickMenu.Size = new System.Drawing.Size(153, 70);
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.deleteToolStripMenuItem.Text = "Delete";
            this.deleteToolStripMenuItem.Click += new System.EventHandler(this.deleteToolStripMenuItem_Click);
            // 
            // renameToolStripMenuItem
            // 
            this.renameToolStripMenuItem.Name = "renameToolStripMenuItem";
            this.renameToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.renameToolStripMenuItem.Text = "Rename";
            this.renameToolStripMenuItem.Click += new System.EventHandler(this.renameToolStripMenuItem_Click);
            // 
            // txtRename
            // 
            this.txtRename.Location = new System.Drawing.Point(16, 32);
            this.txtRename.Name = "txtRename";
            this.txtRename.Size = new System.Drawing.Size(100, 20);
            this.txtRename.TabIndex = 1;
            this.txtRename.Visible = false;
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
            this.lvFTPList.Size = new System.Drawing.Size(972, 595);
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
            // TestForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(972, 595);
            this.Controls.Add(this.lvFTPList);
            this.Controls.Add(this.txtRename);
            this.Name = "TestForm";
            this.Text = "FTP Test";
            this.cmsRightClickMenu.ResumeLayout(false);
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
    }
}
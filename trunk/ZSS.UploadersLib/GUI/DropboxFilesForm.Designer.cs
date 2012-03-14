namespace UploadersLib.Forms
{
    partial class DropboxFilesForm
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
            this.lvDropboxFiles = new HelpersLib.MyListView();
            this.chFilename = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chSize = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chModified = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tsMenu = new System.Windows.Forms.ToolStrip();
            this.tsbSelectFolder = new System.Windows.Forms.ToolStripButton();
            this.scMainSplit = new System.Windows.Forms.SplitContainer();
            this.tsMenu.SuspendLayout();
            this.scMainSplit.Panel1.SuspendLayout();
            this.scMainSplit.Panel2.SuspendLayout();
            this.scMainSplit.SuspendLayout();
            this.SuspendLayout();
            // 
            // lvDropboxFiles
            // 
            this.lvDropboxFiles.AutoFillColumn = true;
            this.lvDropboxFiles.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chFilename,
            this.chSize,
            this.chModified});
            this.lvDropboxFiles.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvDropboxFiles.FullRowSelect = true;
            this.lvDropboxFiles.GridLines = true;
            this.lvDropboxFiles.Location = new System.Drawing.Point(0, 0);
            this.lvDropboxFiles.Name = "lvDropboxFiles";
            this.lvDropboxFiles.Size = new System.Drawing.Size(559, 460);
            this.lvDropboxFiles.TabIndex = 0;
            this.lvDropboxFiles.UseCompatibleStateImageBehavior = false;
            this.lvDropboxFiles.View = System.Windows.Forms.View.Details;
            this.lvDropboxFiles.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lvDropboxFiles_MouseDoubleClick);
            // 
            // chFilename
            // 
            this.chFilename.Text = "File Name";
            this.chFilename.Width = 275;
            // 
            // chSize
            // 
            this.chSize.Text = "Size";
            this.chSize.Width = 80;
            // 
            // chModified
            // 
            this.chModified.Text = "Modified";
            this.chModified.Width = 200;
            // 
            // tsMenu
            // 
            this.tsMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbSelectFolder});
            this.tsMenu.Location = new System.Drawing.Point(3, 3);
            this.tsMenu.Margin = new System.Windows.Forms.Padding(2);
            this.tsMenu.Name = "tsMenu";
            this.tsMenu.Size = new System.Drawing.Size(553, 25);
            this.tsMenu.TabIndex = 1;
            this.tsMenu.Text = "toolStrip1";
            // 
            // tsbSelectFolder
            // 
            this.tsbSelectFolder.Image = global::UploadersLib.Properties.Resources.folder;
            this.tsbSelectFolder.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbSelectFolder.Name = "tsbSelectFolder";
            this.tsbSelectFolder.Size = new System.Drawing.Size(160, 22);
            this.tsbSelectFolder.Text = "Select current folder path";
            this.tsbSelectFolder.Click += new System.EventHandler(this.tsbSelectFolder_Click);
            // 
            // scMainSplit
            // 
            this.scMainSplit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scMainSplit.IsSplitterFixed = true;
            this.scMainSplit.Location = new System.Drawing.Point(0, 0);
            this.scMainSplit.Name = "scMainSplit";
            this.scMainSplit.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // scMainSplit.Panel1
            // 
            this.scMainSplit.Panel1.Controls.Add(this.tsMenu);
            this.scMainSplit.Panel1.Padding = new System.Windows.Forms.Padding(3);
            // 
            // scMainSplit.Panel2
            // 
            this.scMainSplit.Panel2.Controls.Add(this.lvDropboxFiles);
            this.scMainSplit.Size = new System.Drawing.Size(559, 491);
            this.scMainSplit.SplitterDistance = 30;
            this.scMainSplit.SplitterWidth = 1;
            this.scMainSplit.TabIndex = 2;
            // 
            // DropboxFilesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(559, 491);
            this.Controls.Add(this.scMainSplit);
            this.Name = "DropboxFilesForm";
            this.ShowIcon = false;
            this.Text = "Dropbox";
            this.tsMenu.ResumeLayout(false);
            this.tsMenu.PerformLayout();
            this.scMainSplit.Panel1.ResumeLayout(false);
            this.scMainSplit.Panel1.PerformLayout();
            this.scMainSplit.Panel2.ResumeLayout(false);
            this.scMainSplit.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private HelpersLib.MyListView lvDropboxFiles;
        private System.Windows.Forms.ColumnHeader chFilename;
        private System.Windows.Forms.ColumnHeader chSize;
        private System.Windows.Forms.ColumnHeader chModified;
        private System.Windows.Forms.ToolStrip tsMenu;
        private System.Windows.Forms.ToolStripButton tsbSelectFolder;
        private System.Windows.Forms.SplitContainer scMainSplit;
    }
}
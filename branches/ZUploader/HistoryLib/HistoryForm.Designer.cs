namespace HistoryLib
{
    partial class HistoryForm
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
            this.cmsHistory = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiCopy = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiCopyURL = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiCopyThumbnailURL = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiCopyDeletionURL = new System.Windows.Forms.ToolStripMenuItem();
            this.tssCopy2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiCopyHTMLLink = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiCopyHTMLImage = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiCopyHTMLLinkedImage = new System.Windows.Forms.ToolStripMenuItem();
            this.tssCopy3 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiCopyForumLink = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiCopyForumImage = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiCopyForumLinkedImage = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiCopyFile = new System.Windows.Forms.ToolStripMenuItem();
            this.tssCopy1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiCopyImage = new System.Windows.Forms.ToolStripMenuItem();
            this.tssCopy4 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiCopyFilePath = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiCopyFileName = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiCopyFileNameWithExtension = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiCopyFolder = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiCopyText = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiDeleteFromHistory = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiDeleteLocalFile = new System.Windows.Forms.ToolStripMenuItem();
            this.lvHistory = new HistoryLib.ListViewNF();
            this.chDateTime = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chFilename = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chType = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chHost = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chURL = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tsmiOpenURL = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiOpenThumbnailURL = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiOpenDeletionURL = new System.Windows.Forms.ToolStripMenuItem();
            this.tssOpen1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiOpenFile = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiOpenFolder = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiRefresh = new System.Windows.Forms.ToolStripMenuItem();
            this.tssHistory1 = new System.Windows.Forms.ToolStripSeparator();
            this.cmsHistory.SuspendLayout();
            this.SuspendLayout();
            //
            // cmsHistory
            //
            this.cmsHistory.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiOpen,
            this.tsmiCopy,
            this.tsmiDelete,
            this.tssHistory1,
            this.tsmiRefresh});
            this.cmsHistory.Name = "cmsHistory";
            this.cmsHistory.ShowImageMargin = false;
            this.cmsHistory.Size = new System.Drawing.Size(128, 120);
            this.cmsHistory.Opening += new System.ComponentModel.CancelEventHandler(this.cmsHistory_Opening);
            //
            // tsmiOpen
            //
            this.tsmiOpen.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiOpenURL,
            this.tsmiOpenThumbnailURL,
            this.tsmiOpenDeletionURL,
            this.tssOpen1,
            this.tsmiOpenFile,
            this.tsmiOpenFolder});
            this.tsmiOpen.Name = "tsmiOpen";
            this.tsmiOpen.Size = new System.Drawing.Size(127, 22);
            this.tsmiOpen.Text = "Open";
            //
            // tsmiCopy
            //
            this.tsmiCopy.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiCopyURL,
            this.tsmiCopyThumbnailURL,
            this.tsmiCopyDeletionURL,
            this.tssCopy1,
            this.tsmiCopyFile,
            this.tsmiCopyImage,
            this.tsmiCopyText,
            this.tssCopy2,
            this.tsmiCopyHTMLLink,
            this.tsmiCopyHTMLImage,
            this.tsmiCopyHTMLLinkedImage,
            this.tssCopy3,
            this.tsmiCopyForumLink,
            this.tsmiCopyForumImage,
            this.tsmiCopyForumLinkedImage,
            this.tssCopy4,
            this.tsmiCopyFilePath,
            this.tsmiCopyFileName,
            this.tsmiCopyFileNameWithExtension,
            this.tsmiCopyFolder});
            this.tsmiCopy.Name = "tsmiCopy";
            this.tsmiCopy.Size = new System.Drawing.Size(127, 22);
            this.tsmiCopy.Text = "Copy";
            //
            // tsmiCopyURL
            //
            this.tsmiCopyURL.Name = "tsmiCopyURL";
            this.tsmiCopyURL.Size = new System.Drawing.Size(233, 22);
            this.tsmiCopyURL.Text = "URL";
            //
            // tsmiCopyThumbnailURL
            //
            this.tsmiCopyThumbnailURL.Name = "tsmiCopyThumbnailURL";
            this.tsmiCopyThumbnailURL.Size = new System.Drawing.Size(233, 22);
            this.tsmiCopyThumbnailURL.Text = "Thumbnail URL";
            //
            // tsmiCopyDeletionURL
            //
            this.tsmiCopyDeletionURL.Name = "tsmiCopyDeletionURL";
            this.tsmiCopyDeletionURL.Size = new System.Drawing.Size(233, 22);
            this.tsmiCopyDeletionURL.Text = "Deletion URL";
            //
            // tssCopy2
            //
            this.tssCopy2.Name = "tssCopy2";
            this.tssCopy2.Size = new System.Drawing.Size(230, 6);
            //
            // tsmiCopyHTMLLink
            //
            this.tsmiCopyHTMLLink.Name = "tsmiCopyHTMLLink";
            this.tsmiCopyHTMLLink.Size = new System.Drawing.Size(233, 22);
            this.tsmiCopyHTMLLink.Text = "HTML link";
            //
            // tsmiCopyHTMLImage
            //
            this.tsmiCopyHTMLImage.Name = "tsmiCopyHTMLImage";
            this.tsmiCopyHTMLImage.Size = new System.Drawing.Size(233, 22);
            this.tsmiCopyHTMLImage.Text = "HTML image";
            //
            // tsmiCopyHTMLLinkedImage
            //
            this.tsmiCopyHTMLLinkedImage.Name = "tsmiCopyHTMLLinkedImage";
            this.tsmiCopyHTMLLinkedImage.Size = new System.Drawing.Size(233, 22);
            this.tsmiCopyHTMLLinkedImage.Text = "HTML linked image";
            //
            // tssCopy3
            //
            this.tssCopy3.Name = "tssCopy3";
            this.tssCopy3.Size = new System.Drawing.Size(230, 6);
            //
            // tsmiCopyForumLink
            //
            this.tsmiCopyForumLink.Name = "tsmiCopyForumLink";
            this.tsmiCopyForumLink.Size = new System.Drawing.Size(233, 22);
            this.tsmiCopyForumLink.Text = "Forum (BBCode) link";
            //
            // tsmiCopyForumImage
            //
            this.tsmiCopyForumImage.Name = "tsmiCopyForumImage";
            this.tsmiCopyForumImage.Size = new System.Drawing.Size(233, 22);
            this.tsmiCopyForumImage.Text = "Forum (BBCode) image";
            //
            // tsmiCopyForumLinkedImage
            //
            this.tsmiCopyForumLinkedImage.Name = "tsmiCopyForumLinkedImage";
            this.tsmiCopyForumLinkedImage.Size = new System.Drawing.Size(233, 22);
            this.tsmiCopyForumLinkedImage.Text = "Forum (BBCode) linked image";
            //
            // tsmiCopyFile
            //
            this.tsmiCopyFile.Name = "tsmiCopyFile";
            this.tsmiCopyFile.Size = new System.Drawing.Size(233, 22);
            this.tsmiCopyFile.Text = "File";
            //
            // tssCopy1
            //
            this.tssCopy1.Name = "tssCopy1";
            this.tssCopy1.Size = new System.Drawing.Size(230, 6);
            //
            // tsmiCopyImage
            //
            this.tsmiCopyImage.Name = "tsmiCopyImage";
            this.tsmiCopyImage.Size = new System.Drawing.Size(233, 22);
            this.tsmiCopyImage.Text = "Image (Bitmap)";
            //
            // tssCopy4
            //
            this.tssCopy4.Name = "tssCopy4";
            this.tssCopy4.Size = new System.Drawing.Size(230, 6);
            //
            // tsmiCopyFilePath
            //
            this.tsmiCopyFilePath.Name = "tsmiCopyFilePath";
            this.tsmiCopyFilePath.Size = new System.Drawing.Size(233, 22);
            this.tsmiCopyFilePath.Text = "File path";
            //
            // tsmiCopyFileName
            //
            this.tsmiCopyFileName.Name = "tsmiCopyFileName";
            this.tsmiCopyFileName.Size = new System.Drawing.Size(233, 22);
            this.tsmiCopyFileName.Text = "File name";
            //
            // tsmiCopyFileNameWithExtension
            //
            this.tsmiCopyFileNameWithExtension.Name = "tsmiCopyFileNameWithExtension";
            this.tsmiCopyFileNameWithExtension.Size = new System.Drawing.Size(233, 22);
            this.tsmiCopyFileNameWithExtension.Text = "File name with extension";
            //
            // tsmiCopyFolder
            //
            this.tsmiCopyFolder.Name = "tsmiCopyFolder";
            this.tsmiCopyFolder.Size = new System.Drawing.Size(233, 22);
            this.tsmiCopyFolder.Text = "Folder";
            //
            // tsmiCopyText
            //
            this.tsmiCopyText.Name = "tsmiCopyText";
            this.tsmiCopyText.Size = new System.Drawing.Size(233, 22);
            this.tsmiCopyText.Text = "Text";
            //
            // tsmiDelete
            //
            this.tsmiDelete.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiDeleteFromHistory,
            this.tsmiDeleteLocalFile});
            this.tsmiDelete.Name = "tsmiDelete";
            this.tsmiDelete.Size = new System.Drawing.Size(106, 22);
            this.tsmiDelete.Text = "Delete";
            //
            // tsmiDeleteFromHistory
            //
            this.tsmiDeleteFromHistory.Name = "tsmiDeleteFromHistory";
            this.tsmiDeleteFromHistory.Size = new System.Drawing.Size(152, 22);
            this.tsmiDeleteFromHistory.Text = "From history...";
            //
            // tsmiDeleteLocalFile
            //
            this.tsmiDeleteLocalFile.Name = "tsmiDeleteLocalFile";
            this.tsmiDeleteLocalFile.Size = new System.Drawing.Size(152, 22);
            this.tsmiDeleteLocalFile.Text = "Local file...";
            //
            // lvHistory
            //
            this.lvHistory.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chDateTime,
            this.chFilename,
            this.chType,
            this.chHost,
            this.chURL});
            this.lvHistory.ContextMenuStrip = this.cmsHistory;
            this.lvHistory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvHistory.FullRowSelect = true;
            this.lvHistory.HideSelection = false;
            this.lvHistory.Location = new System.Drawing.Point(0, 0);
            this.lvHistory.Name = "lvHistory";
            this.lvHistory.Size = new System.Drawing.Size(825, 332);
            this.lvHistory.TabIndex = 0;
            this.lvHistory.UseCompatibleStateImageBehavior = false;
            this.lvHistory.View = System.Windows.Forms.View.Details;
            //
            // chDateTime
            //
            this.chDateTime.Text = "Date & time";
            this.chDateTime.Width = 122;
            //
            // chFilename
            //
            this.chFilename.Text = "Filename";
            this.chFilename.Width = 172;
            //
            // chType
            //
            this.chType.Text = "Type";
            this.chType.Width = 56;
            //
            // chHost
            //
            this.chHost.Text = "Host";
            this.chHost.Width = 95;
            //
            // chURL
            //
            this.chURL.Text = "URL";
            this.chURL.Width = 351;
            //
            // tsmiOpenURL
            //
            this.tsmiOpenURL.Name = "tsmiOpenURL";
            this.tsmiOpenURL.Size = new System.Drawing.Size(156, 22);
            this.tsmiOpenURL.Text = "URL";
            //
            // tsmiOpenThumbnailURL
            //
            this.tsmiOpenThumbnailURL.Name = "tsmiOpenThumbnailURL";
            this.tsmiOpenThumbnailURL.Size = new System.Drawing.Size(156, 22);
            this.tsmiOpenThumbnailURL.Text = "Thumbnail URL";
            //
            // tsmiOpenDeletionURL
            //
            this.tsmiOpenDeletionURL.Name = "tsmiOpenDeletionURL";
            this.tsmiOpenDeletionURL.Size = new System.Drawing.Size(156, 22);
            this.tsmiOpenDeletionURL.Text = "Deletion URL";
            //
            // tssOpen1
            //
            this.tssOpen1.Name = "tssOpen1";
            this.tssOpen1.Size = new System.Drawing.Size(153, 6);
            //
            // tsmiOpenFile
            //
            this.tsmiOpenFile.Name = "tsmiOpenFile";
            this.tsmiOpenFile.Size = new System.Drawing.Size(156, 22);
            this.tsmiOpenFile.Text = "File";
            //
            // tsmiOpenFolder
            //
            this.tsmiOpenFolder.Name = "tsmiOpenFolder";
            this.tsmiOpenFolder.Size = new System.Drawing.Size(156, 22);
            this.tsmiOpenFolder.Text = "Folder";
            //
            // tsmiRefresh
            //
            this.tsmiRefresh.Name = "tsmiRefresh";
            this.tsmiRefresh.Size = new System.Drawing.Size(127, 22);
            this.tsmiRefresh.Text = "Refresh list";
            //
            // tssHistory1
            //
            this.tssHistory1.Name = "tssHistory1";
            this.tssHistory1.Size = new System.Drawing.Size(124, 6);
            //
            // HistoryForm
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(825, 332);
            this.Controls.Add(this.lvHistory);
            this.Name = "HistoryForm";
            this.Text = "HistoryFormTest";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.HistoryForm_FormClosed);
            this.Load += new System.EventHandler(this.HistoryForm_Load);
            this.cmsHistory.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        #endregion Windows Form Designer generated code

        private ListViewNF lvHistory;
        private System.Windows.Forms.ColumnHeader chFilename;
        private System.Windows.Forms.ColumnHeader chDateTime;
        private System.Windows.Forms.ColumnHeader chType;
        private System.Windows.Forms.ColumnHeader chHost;
        private System.Windows.Forms.ColumnHeader chURL;
        private System.Windows.Forms.ContextMenuStrip cmsHistory;
        private System.Windows.Forms.ToolStripMenuItem tsmiOpen;
        private System.Windows.Forms.ToolStripMenuItem tsmiCopy;
        private System.Windows.Forms.ToolStripMenuItem tsmiCopyURL;
        private System.Windows.Forms.ToolStripMenuItem tsmiCopyThumbnailURL;
        private System.Windows.Forms.ToolStripMenuItem tsmiCopyDeletionURL;
        private System.Windows.Forms.ToolStripSeparator tssCopy1;
        private System.Windows.Forms.ToolStripMenuItem tsmiCopyFile;
        private System.Windows.Forms.ToolStripMenuItem tsmiCopyImage;
        private System.Windows.Forms.ToolStripSeparator tssCopy2;
        private System.Windows.Forms.ToolStripMenuItem tsmiCopyHTMLLink;
        private System.Windows.Forms.ToolStripMenuItem tsmiCopyHTMLImage;
        private System.Windows.Forms.ToolStripMenuItem tsmiCopyHTMLLinkedImage;
        private System.Windows.Forms.ToolStripSeparator tssCopy3;
        private System.Windows.Forms.ToolStripMenuItem tsmiCopyForumLink;
        private System.Windows.Forms.ToolStripMenuItem tsmiCopyForumImage;
        private System.Windows.Forms.ToolStripMenuItem tsmiCopyForumLinkedImage;
        private System.Windows.Forms.ToolStripSeparator tssCopy4;
        private System.Windows.Forms.ToolStripMenuItem tsmiCopyFilePath;
        private System.Windows.Forms.ToolStripMenuItem tsmiCopyFileName;
        private System.Windows.Forms.ToolStripMenuItem tsmiCopyFileNameWithExtension;
        private System.Windows.Forms.ToolStripMenuItem tsmiCopyFolder;
        private System.Windows.Forms.ToolStripMenuItem tsmiCopyText;
        private System.Windows.Forms.ToolStripMenuItem tsmiDelete;
        private System.Windows.Forms.ToolStripMenuItem tsmiDeleteFromHistory;
        private System.Windows.Forms.ToolStripMenuItem tsmiDeleteLocalFile;
        private System.Windows.Forms.ToolStripMenuItem tsmiOpenURL;
        private System.Windows.Forms.ToolStripMenuItem tsmiOpenThumbnailURL;
        private System.Windows.Forms.ToolStripMenuItem tsmiOpenDeletionURL;
        private System.Windows.Forms.ToolStripSeparator tssOpen1;
        private System.Windows.Forms.ToolStripMenuItem tsmiOpenFile;
        private System.Windows.Forms.ToolStripMenuItem tsmiOpenFolder;
        private System.Windows.Forms.ToolStripSeparator tssHistory1;
        private System.Windows.Forms.ToolStripMenuItem tsmiRefresh;
    }
}
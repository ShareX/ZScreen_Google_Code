namespace ZSS
{
    partial class ViewRemote
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ViewRemote));
            this.pbViewer = new System.Windows.Forms.PictureBox();
            this.lbFiles = new System.Windows.Forms.ListBox();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnCopyToClip = new System.Windows.Forms.Button();
            this.cbReverse = new System.Windows.Forms.CheckBox();
            this.cbAddSpace = new System.Windows.Forms.CheckBox();
            this.pnlControls = new System.Windows.Forms.Panel();
            this.btnSave = new System.Windows.Forms.Button();
            this.folderBrowseDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.pnlViewer = new System.Windows.Forms.Panel();
            this.ssViewer = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.sBar = new System.Windows.Forms.ToolStripStatusLabel();
            this.pBar = new System.Windows.Forms.ToolStripProgressBar();
            this.bwRemoteViewer = new System.ComponentModel.BackgroundWorker();
            this.tmrFetchFile = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.pbViewer)).BeginInit();
            this.pnlControls.SuspendLayout();
            this.pnlViewer.SuspendLayout();
            this.ssViewer.SuspendLayout();
            this.SuspendLayout();
            // 
            // pbViewer
            // 
            this.pbViewer.AccessibleDescription = null;
            this.pbViewer.AccessibleName = null;
            resources.ApplyResources(this.pbViewer, "pbViewer");
            this.pbViewer.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.pbViewer.BackgroundImage = null;
            this.pbViewer.Font = null;
            this.pbViewer.ImageLocation = null;
            this.pbViewer.Name = "pbViewer";
            this.pbViewer.TabStop = false;
            this.pbViewer.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pbViewer_MouseMove);
            this.pbViewer.Click += new System.EventHandler(this.pbViewer_Click);
            this.pbViewer.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pbViewer_MouseDown);
            // 
            // lbFiles
            // 
            this.lbFiles.AccessibleDescription = null;
            this.lbFiles.AccessibleName = null;
            resources.ApplyResources(this.lbFiles, "lbFiles");
            this.lbFiles.BackgroundImage = null;
            this.lbFiles.Font = null;
            this.lbFiles.FormattingEnabled = true;
            this.lbFiles.Name = "lbFiles";
            this.lbFiles.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lbFiles.SelectedIndexChanged += new System.EventHandler(this.lbFiles_SelectedIndexChanged);
            // 
            // btnDelete
            // 
            this.btnDelete.AccessibleDescription = null;
            this.btnDelete.AccessibleName = null;
            resources.ApplyResources(this.btnDelete, "btnDelete");
            this.btnDelete.BackgroundImage = null;
            this.btnDelete.Font = null;
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnCopyToClip
            // 
            this.btnCopyToClip.AccessibleDescription = null;
            this.btnCopyToClip.AccessibleName = null;
            resources.ApplyResources(this.btnCopyToClip, "btnCopyToClip");
            this.btnCopyToClip.BackgroundImage = null;
            this.btnCopyToClip.Font = null;
            this.btnCopyToClip.Name = "btnCopyToClip";
            this.btnCopyToClip.UseVisualStyleBackColor = true;
            this.btnCopyToClip.Click += new System.EventHandler(this.btnCopyToClip_Click);
            // 
            // cbReverse
            // 
            this.cbReverse.AccessibleDescription = null;
            this.cbReverse.AccessibleName = null;
            resources.ApplyResources(this.cbReverse, "cbReverse");
            this.cbReverse.BackgroundImage = null;
            this.cbReverse.Name = "cbReverse";
            this.cbReverse.UseVisualStyleBackColor = true;
            // 
            // cbAddSpace
            // 
            this.cbAddSpace.AccessibleDescription = null;
            this.cbAddSpace.AccessibleName = null;
            resources.ApplyResources(this.cbAddSpace, "cbAddSpace");
            this.cbAddSpace.BackgroundImage = null;
            this.cbAddSpace.Name = "cbAddSpace";
            this.cbAddSpace.UseVisualStyleBackColor = true;
            // 
            // pnlControls
            // 
            this.pnlControls.AccessibleDescription = null;
            this.pnlControls.AccessibleName = null;
            resources.ApplyResources(this.pnlControls, "pnlControls");
            this.pnlControls.BackgroundImage = null;
            this.pnlControls.Controls.Add(this.btnSave);
            this.pnlControls.Controls.Add(this.cbAddSpace);
            this.pnlControls.Controls.Add(this.cbReverse);
            this.pnlControls.Controls.Add(this.btnDelete);
            this.pnlControls.Controls.Add(this.btnCopyToClip);
            this.pnlControls.Font = null;
            this.pnlControls.Name = "pnlControls";
            // 
            // btnSave
            // 
            this.btnSave.AccessibleDescription = null;
            this.btnSave.AccessibleName = null;
            resources.ApplyResources(this.btnSave, "btnSave");
            this.btnSave.BackgroundImage = null;
            this.btnSave.Font = null;
            this.btnSave.Name = "btnSave";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // folderBrowseDialog
            // 
            resources.ApplyResources(this.folderBrowseDialog, "folderBrowseDialog");
            // 
            // pnlViewer
            // 
            this.pnlViewer.AccessibleDescription = null;
            this.pnlViewer.AccessibleName = null;
            resources.ApplyResources(this.pnlViewer, "pnlViewer");
            this.pnlViewer.BackgroundImage = null;
            this.pnlViewer.Controls.Add(this.ssViewer);
            this.pnlViewer.Controls.Add(this.pbViewer);
            this.pnlViewer.Font = null;
            this.pnlViewer.Name = "pnlViewer";
            // 
            // ssViewer
            // 
            this.ssViewer.AccessibleDescription = null;
            this.ssViewer.AccessibleName = null;
            resources.ApplyResources(this.ssViewer, "ssViewer");
            this.ssViewer.BackgroundImage = null;
            this.ssViewer.Font = null;
            this.ssViewer.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.sBar,
            this.pBar});
            this.ssViewer.Name = "ssViewer";
            this.ssViewer.SizingGrip = false;
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.AccessibleDescription = null;
            this.toolStripStatusLabel1.AccessibleName = null;
            resources.ApplyResources(this.toolStripStatusLabel1, "toolStripStatusLabel1");
            this.toolStripStatusLabel1.BackgroundImage = null;
            this.toolStripStatusLabel1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripStatusLabel1.Image = global::ZSS.Properties.Resources.info;
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            // 
            // sBar
            // 
            this.sBar.AccessibleDescription = null;
            this.sBar.AccessibleName = null;
            resources.ApplyResources(this.sBar, "sBar");
            this.sBar.BackgroundImage = null;
            this.sBar.Name = "sBar";
            this.sBar.Overflow = System.Windows.Forms.ToolStripItemOverflow.Never;
            this.sBar.Spring = true;
            this.sBar.Click += new System.EventHandler(this.sBar_Click);
            // 
            // pBar
            // 
            this.pBar.AccessibleDescription = null;
            this.pBar.AccessibleName = null;
            resources.ApplyResources(this.pBar, "pBar");
            this.pBar.Name = "pBar";
            this.pBar.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            // 
            // bwRemoteViewer
            // 
            this.bwRemoteViewer.WorkerReportsProgress = true;
            this.bwRemoteViewer.WorkerSupportsCancellation = true;
            this.bwRemoteViewer.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bwRemoteViewer_DoWork);
            this.bwRemoteViewer.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bwRemoteViewer_RunWorkerCompleted);
            this.bwRemoteViewer.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.bwRemoteViewer_ProgressChanged);
            // 
            // tmrFetchFile
            // 
            this.tmrFetchFile.Enabled = true;
            this.tmrFetchFile.Tick += new System.EventHandler(this.tmrFetchFile_Tick);
            // 
            // ViewRemote
            // 
            this.AccessibleDescription = null;
            this.AccessibleName = null;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = null;
            this.Controls.Add(this.pnlViewer);
            this.Controls.Add(this.pnlControls);
            this.Controls.Add(this.lbFiles);
            this.Font = null;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "ViewRemote";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Shown += new System.EventHandler(this.ViewRemote_Shown);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ViewRemote_FormClosing);
            this.Resize += new System.EventHandler(this.ViewRemote_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.pbViewer)).EndInit();
            this.pnlControls.ResumeLayout(false);
            this.pnlControls.PerformLayout();
            this.pnlViewer.ResumeLayout(false);
            this.pnlViewer.PerformLayout();
            this.ssViewer.ResumeLayout(false);
            this.ssViewer.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pbViewer;
        private System.Windows.Forms.ListBox lbFiles;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnCopyToClip;
        private System.Windows.Forms.CheckBox cbReverse;
        private System.Windows.Forms.CheckBox cbAddSpace;
        private System.Windows.Forms.Panel pnlControls;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.FolderBrowserDialog folderBrowseDialog;
        private System.Windows.Forms.Panel pnlViewer;
        private System.ComponentModel.BackgroundWorker bwRemoteViewer;
        private System.Windows.Forms.StatusStrip ssViewer;
        private System.Windows.Forms.ToolStripStatusLabel sBar;
        private System.Windows.Forms.ToolStripProgressBar pBar;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.Timer tmrFetchFile;
    }
}
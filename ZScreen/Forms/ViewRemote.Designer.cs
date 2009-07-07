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
            this.txtViewer = new System.Windows.Forms.TextBox();
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
            this.pbViewer.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.pbViewer.Location = new System.Drawing.Point(0, 0);
            this.pbViewer.Margin = new System.Windows.Forms.Padding(0);
            this.pbViewer.Name = "pbViewer";
            this.pbViewer.Size = new System.Drawing.Size(525, 560);
            this.pbViewer.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pbViewer.TabIndex = 0;
            this.pbViewer.TabStop = false;
            this.pbViewer.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pbViewer_MouseMove);
            this.pbViewer.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pbViewer_MouseDown);
            // 
            // lbFiles
            // 
            this.lbFiles.FormattingEnabled = true;
            this.lbFiles.Location = new System.Drawing.Point(531, 129);
            this.lbFiles.Name = "lbFiles";
            this.lbFiles.ScrollAlwaysVisible = true;
            this.lbFiles.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lbFiles.Size = new System.Drawing.Size(245, 420);
            this.lbFiles.TabIndex = 1;
            this.lbFiles.SelectedIndexChanged += new System.EventHandler(this.lbFiles_SelectedIndexChanged);
            // 
            // btnDelete
            // 
            this.btnDelete.Enabled = false;
            this.btnDelete.Location = new System.Drawing.Point(5, 99);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(235, 24);
            this.btnDelete.TabIndex = 2;
            this.btnDelete.Text = "Delete files";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnCopyToClip
            // 
            this.btnCopyToClip.Enabled = false;
            this.btnCopyToClip.Location = new System.Drawing.Point(5, 45);
            this.btnCopyToClip.Name = "btnCopyToClip";
            this.btnCopyToClip.Size = new System.Drawing.Size(235, 24);
            this.btnCopyToClip.TabIndex = 3;
            this.btnCopyToClip.Text = "Copy URL to clipboard";
            this.btnCopyToClip.UseVisualStyleBackColor = true;
            this.btnCopyToClip.Click += new System.EventHandler(this.btnCopyToClip_Click);
            // 
            // cbReverse
            // 
            this.cbReverse.AutoSize = true;
            this.cbReverse.Location = new System.Drawing.Point(7, 25);
            this.cbReverse.Margin = new System.Windows.Forms.Padding(0);
            this.cbReverse.Name = "cbReverse";
            this.cbReverse.Size = new System.Drawing.Size(81, 17);
            this.cbReverse.TabIndex = 36;
            this.cbReverse.Text = "Reverse list";
            this.cbReverse.UseVisualStyleBackColor = true;
            // 
            // cbAddSpace
            // 
            this.cbAddSpace.AutoSize = true;
            this.cbAddSpace.Location = new System.Drawing.Point(7, 7);
            this.cbAddSpace.Margin = new System.Windows.Forms.Padding(0);
            this.cbAddSpace.Name = "cbAddSpace";
            this.cbAddSpace.Size = new System.Drawing.Size(110, 17);
            this.cbAddSpace.TabIndex = 35;
            this.cbAddSpace.Text = "Add space before";
            this.cbAddSpace.UseVisualStyleBackColor = true;
            // 
            // pnlControls
            // 
            this.pnlControls.Controls.Add(this.btnSave);
            this.pnlControls.Controls.Add(this.cbAddSpace);
            this.pnlControls.Controls.Add(this.cbReverse);
            this.pnlControls.Controls.Add(this.btnDelete);
            this.pnlControls.Controls.Add(this.btnCopyToClip);
            this.pnlControls.Location = new System.Drawing.Point(530, 0);
            this.pnlControls.Margin = new System.Windows.Forms.Padding(0);
            this.pnlControls.Name = "pnlControls";
            this.pnlControls.Size = new System.Drawing.Size(249, 126);
            this.pnlControls.TabIndex = 37;
            // 
            // btnSave
            // 
            this.btnSave.Enabled = false;
            this.btnSave.Location = new System.Drawing.Point(5, 72);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(235, 24);
            this.btnSave.TabIndex = 37;
            this.btnSave.Text = "Save files...";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // pnlViewer
            // 
            this.pnlViewer.Controls.Add(this.txtViewer);
            this.pnlViewer.Controls.Add(this.ssViewer);
            this.pnlViewer.Controls.Add(this.pbViewer);
            this.pnlViewer.Location = new System.Drawing.Point(0, 0);
            this.pnlViewer.Name = "pnlViewer";
            this.pnlViewer.Size = new System.Drawing.Size(525, 560);
            this.pnlViewer.TabIndex = 38;
            // 
            // txtViewer
            // 
            this.txtViewer.BackColor = System.Drawing.SystemColors.Info;
            this.txtViewer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtViewer.Location = new System.Drawing.Point(0, 0);
            this.txtViewer.Multiline = true;
            this.txtViewer.Name = "txtViewer";
            this.txtViewer.Size = new System.Drawing.Size(525, 538);
            this.txtViewer.TabIndex = 2;
            // 
            // ssViewer
            // 
            this.ssViewer.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.sBar,
            this.pBar});
            this.ssViewer.Location = new System.Drawing.Point(0, 538);
            this.ssViewer.Name = "ssViewer";
            this.ssViewer.Size = new System.Drawing.Size(525, 22);
            this.ssViewer.SizingGrip = false;
            this.ssViewer.TabIndex = 1;
            this.ssViewer.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripStatusLabel1.Image = global::ZSS.Properties.Resources.info;
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(16, 17);
            this.toolStripStatusLabel1.Text = "Image";
            // 
            // sBar
            // 
            this.sBar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.sBar.Name = "sBar";
            this.sBar.Overflow = System.Windows.Forms.ToolStripItemOverflow.Never;
            this.sBar.Size = new System.Drawing.Size(392, 17);
            this.sBar.Spring = true;
            this.sBar.Text = "Ready.";
            this.sBar.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pBar
            // 
            this.pBar.Name = "pBar";
            this.pBar.Size = new System.Drawing.Size(100, 16);
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
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(782, 558);
            this.Controls.Add(this.pnlViewer);
            this.Controls.Add(this.pnlControls);
            this.Controls.Add(this.lbFiles);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ViewRemote";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "View Remote Directory";
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
        private System.Windows.Forms.TextBox txtViewer;
    }
}
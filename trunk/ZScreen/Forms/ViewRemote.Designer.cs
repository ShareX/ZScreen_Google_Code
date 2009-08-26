namespace ZScreenLib
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ViewRemote));
            this.lbFiles = new System.Windows.Forms.ListBox();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnCopyToClip = new System.Windows.Forms.Button();
            this.cbReverse = new System.Windows.Forms.CheckBox();
            this.cbAddSpace = new System.Windows.Forms.CheckBox();
            this.pnlControls = new System.Windows.Forms.Panel();
            this.btnSave = new System.Windows.Forms.Button();
            this.folderBrowseDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.bwRemoteViewer = new System.ComponentModel.BackgroundWorker();
            this.pbViewer = new System.Windows.Forms.PictureBox();
            this.ssViewer = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.sBar = new System.Windows.Forms.ToolStripStatusLabel();
            this.pBar = new System.Windows.Forms.ToolStripProgressBar();
            this.txtViewer = new System.Windows.Forms.TextBox();
            this.pnlViewer = new System.Windows.Forms.Panel();
            this.ViewRemotePanel = new System.Windows.Forms.TableLayoutPanel();
            this.ControlsPanel = new System.Windows.Forms.TableLayoutPanel();
            this.pnlControls.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbViewer)).BeginInit();
            this.ssViewer.SuspendLayout();
            this.pnlViewer.SuspendLayout();
            this.ViewRemotePanel.SuspendLayout();
            this.ControlsPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbFiles
            // 
            this.lbFiles.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbFiles.FormattingEnabled = true;
            this.lbFiles.Location = new System.Drawing.Point(3, 141);
            this.lbFiles.Name = "lbFiles";
            this.lbFiles.ScrollAlwaysVisible = true;
            this.lbFiles.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lbFiles.Size = new System.Drawing.Size(245, 407);
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
            this.pnlControls.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlControls.Location = new System.Drawing.Point(0, 0);
            this.pnlControls.Margin = new System.Windows.Forms.Padding(0);
            this.pnlControls.Name = "pnlControls";
            this.pnlControls.Size = new System.Drawing.Size(251, 138);
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
            // bwRemoteViewer
            // 
            this.bwRemoteViewer.WorkerReportsProgress = true;
            this.bwRemoteViewer.WorkerSupportsCancellation = true;
            this.bwRemoteViewer.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bwRemoteViewer_DoWork);
            this.bwRemoteViewer.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bwRemoteViewer_RunWorkerCompleted);
            this.bwRemoteViewer.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.bwRemoteViewer_ProgressChanged);
            // 
            // pbViewer
            // 
            this.pbViewer.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.pbViewer.Location = new System.Drawing.Point(0, 0);
            this.pbViewer.Margin = new System.Windows.Forms.Padding(0);
            this.pbViewer.Name = "pbViewer";
            this.pbViewer.Size = new System.Drawing.Size(519, 552);
            this.pbViewer.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pbViewer.TabIndex = 0;
            this.pbViewer.TabStop = false;
            this.pbViewer.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pbViewer_MouseMove);
            this.pbViewer.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pbViewer_MouseDown);
            // 
            // ssViewer
            // 
            this.ssViewer.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.sBar,
            this.pBar});
            this.ssViewer.Location = new System.Drawing.Point(0, 530);
            this.ssViewer.Name = "ssViewer";
            this.ssViewer.Size = new System.Drawing.Size(519, 22);
            this.ssViewer.SizingGrip = false;
            this.ssViewer.TabIndex = 1;
            this.ssViewer.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripStatusLabel1.Image = global::ZScreenGUI.Properties.Resources.info;
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(16, 17);
            this.toolStripStatusLabel1.Text = "Image";
            // 
            // sBar
            // 
            this.sBar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.sBar.Name = "sBar";
            this.sBar.Overflow = System.Windows.Forms.ToolStripItemOverflow.Never;
            this.sBar.Size = new System.Drawing.Size(386, 17);
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
            // txtViewer
            // 
            this.txtViewer.BackColor = System.Drawing.SystemColors.Info;
            this.txtViewer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtViewer.Location = new System.Drawing.Point(0, 0);
            this.txtViewer.Multiline = true;
            this.txtViewer.Name = "txtViewer";
            this.txtViewer.Size = new System.Drawing.Size(519, 530);
            this.txtViewer.TabIndex = 2;
            // 
            // pnlViewer
            // 
            this.pnlViewer.Controls.Add(this.txtViewer);
            this.pnlViewer.Controls.Add(this.ssViewer);
            this.pnlViewer.Controls.Add(this.pbViewer);
            this.pnlViewer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlViewer.Location = new System.Drawing.Point(3, 3);
            this.pnlViewer.Name = "pnlViewer";
            this.pnlViewer.Size = new System.Drawing.Size(519, 552);
            this.pnlViewer.TabIndex = 38;
            // 
            // ViewRemotePanel
            // 
            this.ViewRemotePanel.ColumnCount = 2;
            this.ViewRemotePanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 67.13555F));
            this.ViewRemotePanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 32.86445F));
            this.ViewRemotePanel.Controls.Add(this.pnlViewer, 0, 0);
            this.ViewRemotePanel.Controls.Add(this.ControlsPanel, 1, 0);
            this.ViewRemotePanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ViewRemotePanel.Location = new System.Drawing.Point(0, 0);
            this.ViewRemotePanel.Name = "ViewRemotePanel";
            this.ViewRemotePanel.RowCount = 1;
            this.ViewRemotePanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.ViewRemotePanel.Size = new System.Drawing.Size(782, 558);
            this.ViewRemotePanel.TabIndex = 39;
            // 
            // ControlsPanel
            // 
            this.ControlsPanel.ColumnCount = 1;
            this.ControlsPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.ControlsPanel.Controls.Add(this.pnlControls, 0, 0);
            this.ControlsPanel.Controls.Add(this.lbFiles, 0, 1);
            this.ControlsPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ControlsPanel.Location = new System.Drawing.Point(528, 3);
            this.ControlsPanel.Name = "ControlsPanel";
            this.ControlsPanel.RowCount = 2;
            this.ControlsPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.ControlsPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 75F));
            this.ControlsPanel.Size = new System.Drawing.Size(251, 552);
            this.ControlsPanel.TabIndex = 39;
            // 
            // ViewRemote
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(782, 558);
            this.Controls.Add(this.ViewRemotePanel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ViewRemote";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.Text = "View Remote Directory";
            this.Shown += new System.EventHandler(this.ViewRemote_Shown);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ViewRemote_FormClosing);
            this.pnlControls.ResumeLayout(false);
            this.pnlControls.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbViewer)).EndInit();
            this.ssViewer.ResumeLayout(false);
            this.ssViewer.PerformLayout();
            this.pnlViewer.ResumeLayout(false);
            this.pnlViewer.PerformLayout();
            this.ViewRemotePanel.ResumeLayout(false);
            this.ControlsPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox lbFiles;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnCopyToClip;
        private System.Windows.Forms.CheckBox cbReverse;
        private System.Windows.Forms.CheckBox cbAddSpace;
        private System.Windows.Forms.Panel pnlControls;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.FolderBrowserDialog folderBrowseDialog;
        private System.ComponentModel.BackgroundWorker bwRemoteViewer;
        private System.Windows.Forms.PictureBox pbViewer;
        private System.Windows.Forms.StatusStrip ssViewer;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel sBar;
        private System.Windows.Forms.ToolStripProgressBar pBar;
        private System.Windows.Forms.TextBox txtViewer;
        private System.Windows.Forms.Panel pnlViewer;
        private System.Windows.Forms.TableLayoutPanel ViewRemotePanel;
        private System.Windows.Forms.TableLayoutPanel ControlsPanel;
    }
}
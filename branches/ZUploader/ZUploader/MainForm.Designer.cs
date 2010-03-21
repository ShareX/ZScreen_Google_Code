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
            this.label1 = new System.Windows.Forms.Label();
            this.cbImageUploaderDestination = new System.Windows.Forms.ComboBox();
            this.btnClipboardUpload = new System.Windows.Forms.Button();
            this.lvUploads = new System.Windows.Forms.ListView();
            this.chID = new System.Windows.Forms.ColumnHeader();
            this.chStatus = new System.Windows.Forms.ColumnHeader();
            this.chURL = new System.Windows.Forms.ColumnHeader();
            this.cmsUploads = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.copyURLToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label2 = new System.Windows.Forms.Label();
            this.cbTextUploaderDestination = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cbFileUploaderDestination = new System.Windows.Forms.ComboBox();
            this.btnCopy = new System.Windows.Forms.Button();
            this.tcApp = new System.Windows.Forms.TabControl();
            this.tpHistory = new System.Windows.Forms.TabPage();
            this.tpOptions = new System.Windows.Forms.TabPage();
            this.pgApp = new System.Windows.Forms.PropertyGrid();
            this.cmsUploads.SuspendLayout();
            this.tcApp.SuspendLayout();
            this.tpHistory.SuspendLayout();
            this.tpOptions.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(90, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Image Uploaders:";
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
            this.btnClipboardUpload.Size = new System.Drawing.Size(112, 32);
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
            // 
            // chID
            // 
            this.chID.Text = "ID";
            this.chID.Width = 25;
            // 
            // chStatus
            // 
            this.chStatus.Text = "Status";
            this.chStatus.Width = 100;
            // 
            // chURL
            // 
            this.chURL.Text = "URL";
            this.chURL.Width = 300;
            // 
            // cmsUploads
            // 
            this.cmsUploads.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.copyURLToolStripMenuItem});
            this.cmsUploads.Name = "cmsUploads";
            this.cmsUploads.Size = new System.Drawing.Size(122, 26);
            // 
            // copyURLToolStripMenuItem
            // 
            this.copyURLToolStripMenuItem.Name = "copyURLToolStripMenuItem";
            this.copyURLToolStripMenuItem.Size = new System.Drawing.Size(121, 22);
            this.copyURLToolStripMenuItem.Text = "Copy URL";
            this.copyURLToolStripMenuItem.Click += new System.EventHandler(this.copyURLToolStripMenuItem_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 36);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(82, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Text Uploaders:";
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
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 60);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "File Uploaders:";
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
            this.btnCopy.Size = new System.Drawing.Size(112, 29);
            this.btnCopy.TabIndex = 6;
            this.btnCopy.Text = "Copy Link";
            this.btnCopy.UseVisualStyleBackColor = true;
            this.btnCopy.Click += new System.EventHandler(this.btnCopy_Click);
            // 
            // tcApp
            // 
            this.tcApp.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tcApp.Controls.Add(this.tpHistory);
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
            // MainForm
            // 
            this.AcceptButton = this.btnClipboardUpload;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(492, 366);
            this.Controls.Add(this.btnCopy);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnClipboardUpload);
            this.Controls.Add(this.tcApp);
            this.Controls.Add(this.cbFileUploaderDestination);
            this.Controls.Add(this.cbTextUploaderDestination);
            this.Controls.Add(this.cbImageUploaderDestination);
            this.Controls.Add(this.label1);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ZUploader";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            this.cmsUploads.ResumeLayout(false);
            this.tcApp.ResumeLayout(false);
            this.tpHistory.ResumeLayout(false);
            this.tpOptions.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbImageUploaderDestination;
        private System.Windows.Forms.Button btnClipboardUpload;
        private System.Windows.Forms.ListView lvUploads;
        private System.Windows.Forms.ColumnHeader chID;
        private System.Windows.Forms.ColumnHeader chStatus;
        private System.Windows.Forms.ColumnHeader chURL;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbTextUploaderDestination;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbFileUploaderDestination;
        private System.Windows.Forms.ContextMenuStrip cmsUploads;
        private System.Windows.Forms.ToolStripMenuItem copyURLToolStripMenuItem;
        private System.Windows.Forms.Button btnCopy;
        private System.Windows.Forms.TabControl tcApp;
        private System.Windows.Forms.TabPage tpHistory;
        private System.Windows.Forms.TabPage tpOptions;
        private System.Windows.Forms.PropertyGrid pgApp;
    }
}


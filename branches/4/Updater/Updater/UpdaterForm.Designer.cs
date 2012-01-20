namespace Updater
{
    partial class UpdaterForm
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
            this.openDownloadUrlToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyDownloadUrlToClipboardToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.lblFilename = new Updater.MyLabel();
            this.btnCancel = new Updater.MyButton();
            this.pbProgress = new Updater.MyProgressBar();
            this.lblStatus = new Updater.MyLabel();
            this.lblProgress = new Updater.MyLabel();
            this.cmsRightClickMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // cmsRightClickMenu
            // 
            this.cmsRightClickMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openDownloadUrlToolStripMenuItem,
            this.copyDownloadUrlToClipboardToolStripMenuItem});
            this.cmsRightClickMenu.Name = "cmsRightClickMenu";
            this.cmsRightClickMenu.Size = new System.Drawing.Size(243, 48);
            // 
            // openDownloadUrlToolStripMenuItem
            // 
            this.openDownloadUrlToolStripMenuItem.Name = "openDownloadUrlToolStripMenuItem";
            this.openDownloadUrlToolStripMenuItem.Size = new System.Drawing.Size(242, 22);
            this.openDownloadUrlToolStripMenuItem.Text = "Open download url";
            this.openDownloadUrlToolStripMenuItem.Click += new System.EventHandler(this.openDownloadUrlToolStripMenuItem_Click);
            // 
            // copyDownloadUrlToClipboardToolStripMenuItem
            // 
            this.copyDownloadUrlToClipboardToolStripMenuItem.Name = "copyDownloadUrlToClipboardToolStripMenuItem";
            this.copyDownloadUrlToClipboardToolStripMenuItem.Size = new System.Drawing.Size(242, 22);
            this.copyDownloadUrlToClipboardToolStripMenuItem.Text = "Copy download url to clipboard";
            this.copyDownloadUrlToClipboardToolStripMenuItem.Click += new System.EventHandler(this.copyDownloadUrlToClipboardToolStripMenuItem_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.Image = global::Updater.Properties.Resources.update;
            this.pictureBox1.Location = new System.Drawing.Point(352, 8);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(128, 128);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // lblFilename
            // 
            this.lblFilename.BackColor = System.Drawing.Color.Transparent;
            this.lblFilename.ForeColor = System.Drawing.Color.White;
            this.lblFilename.Location = new System.Drawing.Point(8, 8);
            this.lblFilename.Name = "lblFilename";
            this.lblFilename.Size = new System.Drawing.Size(328, 24);
            this.lblFilename.TabIndex = 10;
            this.lblFilename.Text = "Filename:";
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.Black;
            this.btnCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancel.ForeColor = System.Drawing.Color.White;
            this.btnCancel.Location = new System.Drawing.Point(352, 144);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(128, 32);
            this.btnCancel.TabIndex = 9;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // pbProgress
            // 
            this.pbProgress.Location = new System.Drawing.Point(8, 144);
            this.pbProgress.Maximum = 100;
            this.pbProgress.Minimum = 0;
            this.pbProgress.Name = "pbProgress";
            this.pbProgress.Size = new System.Drawing.Size(336, 32);
            this.pbProgress.TabIndex = 8;
            this.pbProgress.Value = 75;
            // 
            // lblStatus
            // 
            this.lblStatus.BackColor = System.Drawing.Color.Transparent;
            this.lblStatus.ForeColor = System.Drawing.Color.White;
            this.lblStatus.Location = new System.Drawing.Point(8, 40);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(328, 24);
            this.lblStatus.TabIndex = 11;
            this.lblStatus.Text = "Status:";
            // 
            // lblProgress
            // 
            this.lblProgress.BackColor = System.Drawing.Color.Transparent;
            this.lblProgress.ForeColor = System.Drawing.Color.White;
            this.lblProgress.Location = new System.Drawing.Point(8, 72);
            this.lblProgress.Name = "lblProgress";
            this.lblProgress.Size = new System.Drawing.Size(328, 24);
            this.lblProgress.TabIndex = 12;
            this.lblProgress.Text = "Progress:";
            // 
            // UpdaterForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(70)))), ((int)(((byte)(70)))));
            this.ClientSize = new System.Drawing.Size(490, 185);
            this.ContextMenuStrip = this.cmsRightClickMenu;
            this.Controls.Add(this.lblProgress);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.lblFilename);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.pbProgress);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "UpdaterForm";
            this.Text = "Update Manager";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.UpdaterForm_FormClosing);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.UpdaterForm_Paint);
            this.cmsRightClickMenu.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.ContextMenuStrip cmsRightClickMenu;
        private System.Windows.Forms.ToolStripMenuItem openDownloadUrlToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyDownloadUrlToClipboardToolStripMenuItem;
        private MyProgressBar pbProgress;
        private MyButton btnCancel;
        private MyLabel lblFilename;
        private MyLabel lblStatus;
        private MyLabel lblProgress;
    }
}
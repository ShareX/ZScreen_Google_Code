namespace ZUploader
{
    partial class SettingsForm
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
        	this.pgFTPSettings = new System.Windows.Forms.PropertyGrid();
        	this.cbClipboardAutoCopy = new System.Windows.Forms.CheckBox();
        	this.cbAutoPlaySound = new System.Windows.Forms.CheckBox();
        	this.tcSettings = new System.Windows.Forms.TabControl();
        	this.tpFtp = new System.Windows.Forms.TabPage();
        	this.tpProxy = new System.Windows.Forms.TabPage();
        	this.pgProxy = new System.Windows.Forms.PropertyGrid();
        	this.tcSettings.SuspendLayout();
        	this.tpFtp.SuspendLayout();
        	this.tpProxy.SuspendLayout();
        	this.SuspendLayout();
        	// 
        	// pgFTPSettings
        	// 
        	this.pgFTPSettings.Dock = System.Windows.Forms.DockStyle.Fill;
        	this.pgFTPSettings.Location = new System.Drawing.Point(3, 3);
        	this.pgFTPSettings.Name = "pgFTPSettings";
        	this.pgFTPSettings.PropertySort = System.Windows.Forms.PropertySort.NoSort;
        	this.pgFTPSettings.Size = new System.Drawing.Size(506, 256);
        	this.pgFTPSettings.TabIndex = 0;
        	this.pgFTPSettings.ToolbarVisible = false;
        	this.pgFTPSettings.SelectedObjectsChanged += new System.EventHandler(this.pgFTPSettings_SelectedObjectsChanged);
        	// 
        	// cbClipboardAutoCopy
        	// 
        	this.cbClipboardAutoCopy.AutoSize = true;
        	this.cbClipboardAutoCopy.Location = new System.Drawing.Point(16, 16);
        	this.cbClipboardAutoCopy.Name = "cbClipboardAutoCopy";
        	this.cbClipboardAutoCopy.Size = new System.Drawing.Size(254, 17);
        	this.cbClipboardAutoCopy.TabIndex = 2;
        	this.cbClipboardAutoCopy.Text = "Copy URL to clipboard after upload is completed";
        	this.cbClipboardAutoCopy.UseVisualStyleBackColor = true;
        	this.cbClipboardAutoCopy.CheckedChanged += new System.EventHandler(this.cbClipboardAutoCopy_CheckedChanged);
        	// 
        	// cbAutoPlaySound
        	// 
        	this.cbAutoPlaySound.AutoSize = true;
        	this.cbAutoPlaySound.Location = new System.Drawing.Point(16, 40);
        	this.cbAutoPlaySound.Name = "cbAutoPlaySound";
        	this.cbAutoPlaySound.Size = new System.Drawing.Size(199, 17);
        	this.cbAutoPlaySound.TabIndex = 3;
        	this.cbAutoPlaySound.Text = "Play sound after upload is completed";
        	this.cbAutoPlaySound.UseVisualStyleBackColor = true;
        	this.cbAutoPlaySound.CheckedChanged += new System.EventHandler(this.cbAutoPlaySound_CheckedChanged);
        	// 
        	// tcSettings
        	// 
        	this.tcSettings.Controls.Add(this.tpFtp);
        	this.tcSettings.Controls.Add(this.tpProxy);
        	this.tcSettings.Location = new System.Drawing.Point(16, 72);
        	this.tcSettings.Name = "tcSettings";
        	this.tcSettings.SelectedIndex = 0;
        	this.tcSettings.Size = new System.Drawing.Size(520, 288);
        	this.tcSettings.TabIndex = 4;
        	// 
        	// tpFtp
        	// 
        	this.tpFtp.Controls.Add(this.pgFTPSettings);
        	this.tpFtp.Location = new System.Drawing.Point(4, 22);
        	this.tpFtp.Name = "tpFtp";
        	this.tpFtp.Padding = new System.Windows.Forms.Padding(3);
        	this.tpFtp.Size = new System.Drawing.Size(512, 262);
        	this.tpFtp.TabIndex = 0;
        	this.tpFtp.Text = "FTP";
        	this.tpFtp.UseVisualStyleBackColor = true;
        	// 
        	// tpProxy
        	// 
        	this.tpProxy.Controls.Add(this.pgProxy);
        	this.tpProxy.Location = new System.Drawing.Point(4, 22);
        	this.tpProxy.Name = "tpProxy";
        	this.tpProxy.Padding = new System.Windows.Forms.Padding(3);
        	this.tpProxy.Size = new System.Drawing.Size(512, 262);
        	this.tpProxy.TabIndex = 1;
        	this.tpProxy.Text = "Proxy";
        	this.tpProxy.UseVisualStyleBackColor = true;
        	// 
        	// pgProxy
        	// 
        	this.pgProxy.Dock = System.Windows.Forms.DockStyle.Fill;
        	this.pgProxy.Location = new System.Drawing.Point(3, 3);
        	this.pgProxy.Name = "pgProxy";
        	this.pgProxy.PropertySort = System.Windows.Forms.PropertySort.NoSort;
        	this.pgProxy.Size = new System.Drawing.Size(506, 256);
        	this.pgProxy.TabIndex = 1;
        	this.pgProxy.ToolbarVisible = false;
        	// 
        	// SettingsForm
        	// 
        	this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
        	this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        	this.ClientSize = new System.Drawing.Size(551, 373);
        	this.Controls.Add(this.tcSettings);
        	this.Controls.Add(this.cbClipboardAutoCopy);
        	this.Controls.Add(this.cbAutoPlaySound);
        	this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
        	this.MaximizeBox = false;
        	this.Name = "SettingsForm";
        	this.Text = "ZUploader - Settings";
        	this.Load += new System.EventHandler(this.FTPSettingsForm_Load);
        	this.tcSettings.ResumeLayout(false);
        	this.tpFtp.ResumeLayout(false);
        	this.tpProxy.ResumeLayout(false);
        	this.ResumeLayout(false);
        	this.PerformLayout();
        }
        private System.Windows.Forms.TabControl tcSettings;
        private System.Windows.Forms.PropertyGrid pgProxy;
        private System.Windows.Forms.TabPage tpProxy;
        private System.Windows.Forms.TabPage tpFtp;

        #endregion

        private System.Windows.Forms.PropertyGrid pgFTPSettings;
        private System.Windows.Forms.CheckBox cbClipboardAutoCopy;
        private System.Windows.Forms.CheckBox cbAutoPlaySound;
    }
}
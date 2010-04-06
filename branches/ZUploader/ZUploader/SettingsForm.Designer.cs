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
            this.lblFTPSettings = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // pgFTPSettings
            // 
            this.pgFTPSettings.Location = new System.Drawing.Point(16, 88);
            this.pgFTPSettings.Name = "pgFTPSettings";
            this.pgFTPSettings.PropertySort = System.Windows.Forms.PropertySort.NoSort;
            this.pgFTPSettings.Size = new System.Drawing.Size(520, 240);
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
            // lblFTPSettings
            // 
            this.lblFTPSettings.AutoSize = true;
            this.lblFTPSettings.Location = new System.Drawing.Point(16, 72);
            this.lblFTPSettings.Name = "lblFTPSettings";
            this.lblFTPSettings.Size = new System.Drawing.Size(112, 13);
            this.lblFTPSettings.TabIndex = 4;
            this.lblFTPSettings.Text = "FTP Account settings:";
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(548, 342);
            this.Controls.Add(this.lblFTPSettings);
            this.Controls.Add(this.cbClipboardAutoCopy);
            this.Controls.Add(this.cbAutoPlaySound);
            this.Controls.Add(this.pgFTPSettings);
            this.Name = "SettingsForm";
            this.Text = "ZUploader - FTP Settings";
            this.Load += new System.EventHandler(this.FTPSettingsForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PropertyGrid pgFTPSettings;
        private System.Windows.Forms.CheckBox cbClipboardAutoCopy;
        private System.Windows.Forms.CheckBox cbAutoPlaySound;
        private System.Windows.Forms.Label lblFTPSettings;
    }
}
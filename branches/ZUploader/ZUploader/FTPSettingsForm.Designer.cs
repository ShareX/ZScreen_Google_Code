namespace ZUploader
{
    partial class FTPSettingsForm
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
            this.SuspendLayout();
            // 
            // pgFTPSettings
            // 
            this.pgFTPSettings.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pgFTPSettings.Location = new System.Drawing.Point(0, 0);
            this.pgFTPSettings.Name = "pgFTPSettings";
            this.pgFTPSettings.PropertySort = System.Windows.Forms.PropertySort.NoSort;
            this.pgFTPSettings.Size = new System.Drawing.Size(548, 342);
            this.pgFTPSettings.TabIndex = 0;
            this.pgFTPSettings.ToolbarVisible = false;
            this.pgFTPSettings.SelectedObjectsChanged += new System.EventHandler(this.pgFTPSettings_SelectedObjectsChanged);
            // 
            // FTPSettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(548, 342);
            this.Controls.Add(this.pgFTPSettings);
            this.Name = "FTPSettingsForm";
            this.Text = "ZUploader - FTP Settings";
            this.Load += new System.EventHandler(this.FTPSettingsForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PropertyGrid pgFTPSettings;
    }
}
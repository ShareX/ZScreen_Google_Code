namespace UploadersLib
{
    partial class ImageUploadersConfig
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
            this.tcMain = new System.Windows.Forms.TabControl();
            this.tpFTP = new System.Windows.Forms.TabPage();
            this.btnFtpHelp = new System.Windows.Forms.Button();
            this.btnFTPOpenClient = new System.Windows.Forms.Button();
            this.gbFTPSettings = new System.Windows.Forms.GroupBox();
            this.lblFtpFiles = new System.Windows.Forms.Label();
            this.lblFtpText = new System.Windows.Forms.Label();
            this.lblFtpImages = new System.Windows.Forms.Label();
            this.cboFtpFiles = new System.Windows.Forms.ComboBox();
            this.cboFtpText = new System.Windows.Forms.ComboBox();
            this.cboFtpImages = new System.Windows.Forms.ComboBox();
            this.cbFTPThumbnailCheckSize = new System.Windows.Forms.CheckBox();
            this.lblFTPThumbWidth = new System.Windows.Forms.Label();
            this.txtFTPThumbWidth = new System.Windows.Forms.TextBox();
            this.panelFTP = new System.Windows.Forms.Panel();
            this.tlpFTP = new System.Windows.Forms.TableLayoutPanel();
            this.ucFTPAccounts = new UploadersLib.AccountsControl();
            this.tpDropbox = new System.Windows.Forms.TabPage();
            this.tcMain.SuspendLayout();
            this.tpFTP.SuspendLayout();
            this.gbFTPSettings.SuspendLayout();
            this.panelFTP.SuspendLayout();
            this.tlpFTP.SuspendLayout();
            this.SuspendLayout();
            // 
            // tcMain
            // 
            this.tcMain.Controls.Add(this.tpFTP);
            this.tcMain.Controls.Add(this.tpDropbox);
            this.tcMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcMain.Location = new System.Drawing.Point(0, 0);
            this.tcMain.Name = "tcMain";
            this.tcMain.SelectedIndex = 0;
            this.tcMain.Size = new System.Drawing.Size(817, 466);
            this.tcMain.TabIndex = 0;
            // 
            // tpFTP
            // 
            this.tpFTP.BackColor = System.Drawing.SystemColors.Window;
            this.tpFTP.Controls.Add(this.tlpFTP);
            this.tpFTP.Location = new System.Drawing.Point(4, 22);
            this.tpFTP.Name = "tpFTP";
            this.tpFTP.Padding = new System.Windows.Forms.Padding(3);
            this.tpFTP.Size = new System.Drawing.Size(809, 440);
            this.tpFTP.TabIndex = 6;
            this.tpFTP.Text = "FTP";
            // 
            // btnFtpHelp
            // 
            this.btnFtpHelp.Location = new System.Drawing.Point(227, 8);
            this.btnFtpHelp.Name = "btnFtpHelp";
            this.btnFtpHelp.Size = new System.Drawing.Size(64, 24);
            this.btnFtpHelp.TabIndex = 75;
            this.btnFtpHelp.Text = "&Help...";
            this.btnFtpHelp.UseVisualStyleBackColor = true;
            // 
            // btnFTPOpenClient
            // 
            this.btnFTPOpenClient.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnFTPOpenClient.Location = new System.Drawing.Point(546, 8);
            this.btnFTPOpenClient.Name = "btnFTPOpenClient";
            this.btnFTPOpenClient.Size = new System.Drawing.Size(128, 24);
            this.btnFTPOpenClient.TabIndex = 116;
            this.btnFTPOpenClient.Text = "Open FTP &Client...";
            this.btnFTPOpenClient.UseVisualStyleBackColor = true;
            // 
            // gbFTPSettings
            // 
            this.gbFTPSettings.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gbFTPSettings.Controls.Add(this.lblFtpFiles);
            this.gbFTPSettings.Controls.Add(this.lblFtpText);
            this.gbFTPSettings.Controls.Add(this.lblFtpImages);
            this.gbFTPSettings.Controls.Add(this.cboFtpFiles);
            this.gbFTPSettings.Controls.Add(this.cboFtpText);
            this.gbFTPSettings.Controls.Add(this.cboFtpImages);
            this.gbFTPSettings.Controls.Add(this.cbFTPThumbnailCheckSize);
            this.gbFTPSettings.Controls.Add(this.lblFTPThumbWidth);
            this.gbFTPSettings.Controls.Add(this.txtFTPThumbWidth);
            this.gbFTPSettings.Location = new System.Drawing.Point(3, 337);
            this.gbFTPSettings.Name = "gbFTPSettings";
            this.gbFTPSettings.Size = new System.Drawing.Size(797, 94);
            this.gbFTPSettings.TabIndex = 115;
            this.gbFTPSettings.TabStop = false;
            this.gbFTPSettings.Text = "FTP Settings";
            // 
            // lblFtpFiles
            // 
            this.lblFtpFiles.AutoSize = true;
            this.lblFtpFiles.Location = new System.Drawing.Point(432, 70);
            this.lblFtpFiles.Name = "lblFtpFiles";
            this.lblFtpFiles.Size = new System.Drawing.Size(28, 13);
            this.lblFtpFiles.TabIndex = 136;
            this.lblFtpFiles.Text = "Files";
            // 
            // lblFtpText
            // 
            this.lblFtpText.AutoSize = true;
            this.lblFtpText.Location = new System.Drawing.Point(432, 44);
            this.lblFtpText.Name = "lblFtpText";
            this.lblFtpText.Size = new System.Drawing.Size(28, 13);
            this.lblFtpText.TabIndex = 135;
            this.lblFtpText.Text = "Text";
            // 
            // lblFtpImages
            // 
            this.lblFtpImages.AutoSize = true;
            this.lblFtpImages.Location = new System.Drawing.Point(419, 19);
            this.lblFtpImages.Name = "lblFtpImages";
            this.lblFtpImages.Size = new System.Drawing.Size(41, 13);
            this.lblFtpImages.TabIndex = 134;
            this.lblFtpImages.Text = "Images";
            // 
            // cboFtpFiles
            // 
            this.cboFtpFiles.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboFtpFiles.FormattingEnabled = true;
            this.cboFtpFiles.Location = new System.Drawing.Point(472, 64);
            this.cboFtpFiles.Name = "cboFtpFiles";
            this.cboFtpFiles.Size = new System.Drawing.Size(272, 21);
            this.cboFtpFiles.TabIndex = 133;
            // 
            // cboFtpText
            // 
            this.cboFtpText.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboFtpText.FormattingEnabled = true;
            this.cboFtpText.Location = new System.Drawing.Point(472, 40);
            this.cboFtpText.Name = "cboFtpText";
            this.cboFtpText.Size = new System.Drawing.Size(272, 21);
            this.cboFtpText.TabIndex = 132;
            // 
            // cboFtpImages
            // 
            this.cboFtpImages.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboFtpImages.FormattingEnabled = true;
            this.cboFtpImages.Location = new System.Drawing.Point(472, 16);
            this.cboFtpImages.Name = "cboFtpImages";
            this.cboFtpImages.Size = new System.Drawing.Size(272, 21);
            this.cboFtpImages.TabIndex = 117;
            // 
            // cbFTPThumbnailCheckSize
            // 
            this.cbFTPThumbnailCheckSize.AutoSize = true;
            this.cbFTPThumbnailCheckSize.Location = new System.Drawing.Point(16, 48);
            this.cbFTPThumbnailCheckSize.Name = "cbFTPThumbnailCheckSize";
            this.cbFTPThumbnailCheckSize.Size = new System.Drawing.Size(331, 17);
            this.cbFTPThumbnailCheckSize.TabIndex = 131;
            this.cbFTPThumbnailCheckSize.Text = "If image size smaller than thumbnail size then not make thumbnail";
            this.cbFTPThumbnailCheckSize.UseVisualStyleBackColor = true;
            // 
            // lblFTPThumbWidth
            // 
            this.lblFTPThumbWidth.AutoSize = true;
            this.lblFTPThumbWidth.Location = new System.Drawing.Point(16, 25);
            this.lblFTPThumbWidth.Name = "lblFTPThumbWidth";
            this.lblFTPThumbWidth.Size = new System.Drawing.Size(107, 13);
            this.lblFTPThumbWidth.TabIndex = 129;
            this.lblFTPThumbWidth.Text = "Thumbnail width (px):";
            // 
            // txtFTPThumbWidth
            // 
            this.txtFTPThumbWidth.Location = new System.Drawing.Point(128, 22);
            this.txtFTPThumbWidth.Name = "txtFTPThumbWidth";
            this.txtFTPThumbWidth.Size = new System.Drawing.Size(40, 20);
            this.txtFTPThumbWidth.TabIndex = 127;
            this.txtFTPThumbWidth.Text = "2500";
            this.txtFTPThumbWidth.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // panelFTP
            // 
            this.panelFTP.Controls.Add(this.btnFtpHelp);
            this.panelFTP.Controls.Add(this.btnFTPOpenClient);
            this.panelFTP.Controls.Add(this.ucFTPAccounts);
            this.panelFTP.Location = new System.Drawing.Point(3, 3);
            this.panelFTP.Name = "panelFTP";
            this.panelFTP.Size = new System.Drawing.Size(705, 319);
            this.panelFTP.TabIndex = 117;
            // 
            // tlpFTP
            // 
            this.tlpFTP.ColumnCount = 1;
            this.tlpFTP.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpFTP.Controls.Add(this.panelFTP, 0, 0);
            this.tlpFTP.Controls.Add(this.gbFTPSettings, 0, 1);
            this.tlpFTP.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpFTP.Location = new System.Drawing.Point(3, 3);
            this.tlpFTP.Name = "tlpFTP";
            this.tlpFTP.RowCount = 2;
            this.tlpFTP.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 75F));
            this.tlpFTP.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tlpFTP.Size = new System.Drawing.Size(803, 434);
            this.tlpFTP.TabIndex = 0;
            // 
            // ucFTPAccounts
            // 
            this.ucFTPAccounts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucFTPAccounts.Location = new System.Drawing.Point(0, 0);
            this.ucFTPAccounts.Margin = new System.Windows.Forms.Padding(4);
            this.ucFTPAccounts.Name = "ucFTPAccounts";
            this.ucFTPAccounts.Size = new System.Drawing.Size(705, 319);
            this.ucFTPAccounts.TabIndex = 0;
            // 
            // tpDropbox
            // 
            this.tpDropbox.Location = new System.Drawing.Point(4, 22);
            this.tpDropbox.Name = "tpDropbox";
            this.tpDropbox.Size = new System.Drawing.Size(809, 440);
            this.tpDropbox.TabIndex = 7;
            this.tpDropbox.Text = "Dropbox";
            this.tpDropbox.UseVisualStyleBackColor = true;
            // 
            // ImageUploadersConfig
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(817, 466);
            this.Controls.Add(this.tcMain);
            this.Name = "ImageUploadersConfig";
            this.Text = "Image Uploaders";
            this.tcMain.ResumeLayout(false);
            this.tpFTP.ResumeLayout(false);
            this.gbFTPSettings.ResumeLayout(false);
            this.gbFTPSettings.PerformLayout();
            this.panelFTP.ResumeLayout(false);
            this.tlpFTP.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tcMain;
        internal System.Windows.Forms.TabPage tpFTP;
        private System.Windows.Forms.Button btnFtpHelp;
        private System.Windows.Forms.Button btnFTPOpenClient;
        internal AccountsControl ucFTPAccounts;
        internal System.Windows.Forms.GroupBox gbFTPSettings;
        private System.Windows.Forms.Label lblFtpFiles;
        private System.Windows.Forms.Label lblFtpText;
        private System.Windows.Forms.Label lblFtpImages;
        private System.Windows.Forms.ComboBox cboFtpFiles;
        private System.Windows.Forms.ComboBox cboFtpText;
        private System.Windows.Forms.ComboBox cboFtpImages;
        private System.Windows.Forms.CheckBox cbFTPThumbnailCheckSize;
        private System.Windows.Forms.Label lblFTPThumbWidth;
        private System.Windows.Forms.TextBox txtFTPThumbWidth;
        private System.Windows.Forms.Panel panelFTP;
        private System.Windows.Forms.TableLayoutPanel tlpFTP;
        private System.Windows.Forms.TabPage tpDropbox;
    }
}
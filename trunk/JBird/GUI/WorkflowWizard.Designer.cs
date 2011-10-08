namespace ZScreenLib
{
    partial class WorkflowWizard
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
            this.tpAccessibility = new System.Windows.Forms.TabPage();
            this.gbTask = new System.Windows.Forms.GroupBox();
            this.cboTask = new System.Windows.Forms.ComboBox();
            this.gbName = new System.Windows.Forms.GroupBox();
            this.txtName = new System.Windows.Forms.TextBox();
            this.chkUseHotkey = new System.Windows.Forms.CheckBox();
            this.tpCapture = new System.Windows.Forms.TabPage();
            this.tpEditing = new System.Windows.Forms.TabPage();
            this.tpOutputs = new System.Windows.Forms.TabPage();
            this.gbRemoteLocations = new System.Windows.Forms.GroupBox();
            this.chkSendspace = new System.Windows.Forms.CheckBox();
            this.chkUploadFTP = new System.Windows.Forms.CheckBox();
            this.chkUploadDropbox = new System.Windows.Forms.CheckBox();
            this.gbSaveToFile = new System.Windows.Forms.GroupBox();
            this.txtSaveFolder = new System.Windows.Forms.TextBox();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.chkSaveFile = new System.Windows.Forms.CheckBox();
            this.btnOutputsConfig = new System.Windows.Forms.Button();
            this.chkPrinter = new System.Windows.Forms.CheckBox();
            this.chkUpload = new System.Windows.Forms.CheckBox();
            this.chkClipboard = new System.Windows.Forms.CheckBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.hmcHotkeys = new HelpersLib.Hotkey.HotkeyManagerControl();
            this.tcMain.SuspendLayout();
            this.tpAccessibility.SuspendLayout();
            this.gbTask.SuspendLayout();
            this.gbName.SuspendLayout();
            this.tpOutputs.SuspendLayout();
            this.gbRemoteLocations.SuspendLayout();
            this.gbSaveToFile.SuspendLayout();
            this.SuspendLayout();
            // 
            // tcMain
            // 
            this.tcMain.Controls.Add(this.tpAccessibility);
            this.tcMain.Controls.Add(this.tpCapture);
            this.tcMain.Controls.Add(this.tpEditing);
            this.tcMain.Controls.Add(this.tpOutputs);
            this.tcMain.Location = new System.Drawing.Point(8, 8);
            this.tcMain.Name = "tcMain";
            this.tcMain.SelectedIndex = 0;
            this.tcMain.Size = new System.Drawing.Size(624, 344);
            this.tcMain.TabIndex = 0;
            // 
            // tpAccessibility
            // 
            this.tpAccessibility.Controls.Add(this.hmcHotkeys);
            this.tpAccessibility.Controls.Add(this.gbTask);
            this.tpAccessibility.Controls.Add(this.gbName);
            this.tpAccessibility.Controls.Add(this.chkUseHotkey);
            this.tpAccessibility.Location = new System.Drawing.Point(4, 22);
            this.tpAccessibility.Name = "tpAccessibility";
            this.tpAccessibility.Padding = new System.Windows.Forms.Padding(3);
            this.tpAccessibility.Size = new System.Drawing.Size(616, 318);
            this.tpAccessibility.TabIndex = 0;
            this.tpAccessibility.Text = "Step 1 Job";
            this.tpAccessibility.UseVisualStyleBackColor = true;
            // 
            // gbTask
            // 
            this.gbTask.Controls.Add(this.cboTask);
            this.gbTask.Location = new System.Drawing.Point(8, 72);
            this.gbTask.Name = "gbTask";
            this.gbTask.Size = new System.Drawing.Size(584, 56);
            this.gbTask.TabIndex = 10;
            this.gbTask.TabStop = false;
            this.gbTask.Text = "Task";
            // 
            // cboTask
            // 
            this.cboTask.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTask.FormattingEnabled = true;
            this.cboTask.Location = new System.Drawing.Point(8, 24);
            this.cboTask.Name = "cboTask";
            this.cboTask.Size = new System.Drawing.Size(360, 21);
            this.cboTask.TabIndex = 0;
            // 
            // gbName
            // 
            this.gbName.Controls.Add(this.txtName);
            this.gbName.Location = new System.Drawing.Point(8, 8);
            this.gbName.Name = "gbName";
            this.gbName.Size = new System.Drawing.Size(584, 56);
            this.gbName.TabIndex = 9;
            this.gbName.TabStop = false;
            this.gbName.Text = "Description";
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(8, 24);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(560, 20);
            this.txtName.TabIndex = 7;
            // 
            // chkUseHotkey
            // 
            this.chkUseHotkey.AutoSize = true;
            this.chkUseHotkey.Location = new System.Drawing.Point(16, 136);
            this.chkUseHotkey.Name = "chkUseHotkey";
            this.chkUseHotkey.Size = new System.Drawing.Size(183, 17);
            this.chkUseHotkey.TabIndex = 7;
            this.chkUseHotkey.Text = "Enable a hotkey to run this profile";
            this.chkUseHotkey.UseVisualStyleBackColor = true;
            // 
            // tpCapture
            // 
            this.tpCapture.Location = new System.Drawing.Point(4, 22);
            this.tpCapture.Name = "tpCapture";
            this.tpCapture.Size = new System.Drawing.Size(616, 318);
            this.tpCapture.TabIndex = 5;
            this.tpCapture.Text = "Step 2 Capture";
            this.tpCapture.UseVisualStyleBackColor = true;
            // 
            // tpEditing
            // 
            this.tpEditing.Location = new System.Drawing.Point(4, 22);
            this.tpEditing.Name = "tpEditing";
            this.tpEditing.Padding = new System.Windows.Forms.Padding(3);
            this.tpEditing.Size = new System.Drawing.Size(616, 318);
            this.tpEditing.TabIndex = 4;
            this.tpEditing.Text = "Step 3 Editing";
            this.tpEditing.UseVisualStyleBackColor = true;
            // 
            // tpOutputs
            // 
            this.tpOutputs.Controls.Add(this.gbRemoteLocations);
            this.tpOutputs.Controls.Add(this.gbSaveToFile);
            this.tpOutputs.Controls.Add(this.chkSaveFile);
            this.tpOutputs.Controls.Add(this.btnOutputsConfig);
            this.tpOutputs.Controls.Add(this.chkPrinter);
            this.tpOutputs.Controls.Add(this.chkUpload);
            this.tpOutputs.Controls.Add(this.chkClipboard);
            this.tpOutputs.Location = new System.Drawing.Point(4, 22);
            this.tpOutputs.Name = "tpOutputs";
            this.tpOutputs.Padding = new System.Windows.Forms.Padding(3);
            this.tpOutputs.Size = new System.Drawing.Size(616, 318);
            this.tpOutputs.TabIndex = 2;
            this.tpOutputs.Text = "Step 4 Outputs";
            this.tpOutputs.UseVisualStyleBackColor = true;
            // 
            // gbRemoteLocations
            // 
            this.gbRemoteLocations.Controls.Add(this.chkSendspace);
            this.gbRemoteLocations.Controls.Add(this.chkUploadFTP);
            this.gbRemoteLocations.Controls.Add(this.chkUploadDropbox);
            this.gbRemoteLocations.Location = new System.Drawing.Point(8, 168);
            this.gbRemoteLocations.Name = "gbRemoteLocations";
            this.gbRemoteLocations.Size = new System.Drawing.Size(576, 48);
            this.gbRemoteLocations.TabIndex = 6;
            this.gbRemoteLocations.TabStop = false;
            this.gbRemoteLocations.Text = "Upload to Remote Locations";
            // 
            // chkSendspace
            // 
            this.chkSendspace.Location = new System.Drawing.Point(272, 16);
            this.chkSendspace.Name = "chkSendspace";
            this.chkSendspace.Size = new System.Drawing.Size(104, 24);
            this.chkSendspace.TabIndex = 6;
            this.chkSendspace.Text = "Sendspace";
            this.chkSendspace.UseVisualStyleBackColor = true;
            // 
            // chkUploadFTP
            // 
            this.chkUploadFTP.Location = new System.Drawing.Point(144, 16);
            this.chkUploadFTP.Name = "chkUploadFTP";
            this.chkUploadFTP.Size = new System.Drawing.Size(104, 24);
            this.chkUploadFTP.TabIndex = 5;
            this.chkUploadFTP.Text = "FTP Server";
            this.chkUploadFTP.UseVisualStyleBackColor = true;
            // 
            // chkUploadDropbox
            // 
            this.chkUploadDropbox.Location = new System.Drawing.Point(16, 16);
            this.chkUploadDropbox.Name = "chkUploadDropbox";
            this.chkUploadDropbox.Size = new System.Drawing.Size(104, 24);
            this.chkUploadDropbox.TabIndex = 4;
            this.chkUploadDropbox.Text = "Dropbox";
            this.chkUploadDropbox.UseVisualStyleBackColor = true;
            // 
            // gbSaveToFile
            // 
            this.gbSaveToFile.Controls.Add(this.txtSaveFolder);
            this.gbSaveToFile.Controls.Add(this.btnBrowse);
            this.gbSaveToFile.Location = new System.Drawing.Point(8, 96);
            this.gbSaveToFile.Name = "gbSaveToFile";
            this.gbSaveToFile.Size = new System.Drawing.Size(576, 64);
            this.gbSaveToFile.TabIndex = 5;
            this.gbSaveToFile.TabStop = false;
            this.gbSaveToFile.Text = "When taking a screenshot, save the file to a preconfigured location";
            // 
            // txtSaveFolder
            // 
            this.txtSaveFolder.Location = new System.Drawing.Point(16, 24);
            this.txtSaveFolder.Name = "txtSaveFolder";
            this.txtSaveFolder.Size = new System.Drawing.Size(456, 20);
            this.txtSaveFolder.TabIndex = 7;
            // 
            // btnBrowse
            // 
            this.btnBrowse.Location = new System.Drawing.Point(480, 20);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(80, 24);
            this.btnBrowse.TabIndex = 6;
            this.btnBrowse.Text = "&Browse...";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // chkSaveFile
            // 
            this.chkSaveFile.Location = new System.Drawing.Point(8, 64);
            this.chkSaveFile.Name = "chkSaveFile";
            this.chkSaveFile.Size = new System.Drawing.Size(184, 24);
            this.chkSaveFile.TabIndex = 4;
            this.chkSaveFile.Text = "Save to file";
            this.chkSaveFile.UseVisualStyleBackColor = true;
            this.chkSaveFile.CheckedChanged += new System.EventHandler(this.chkSaveFile_CheckedChanged);
            // 
            // btnOutputsConfig
            // 
            this.btnOutputsConfig.Location = new System.Drawing.Point(8, 8);
            this.btnOutputsConfig.Name = "btnOutputsConfig";
            this.btnOutputsConfig.Size = new System.Drawing.Size(336, 24);
            this.btnOutputsConfig.TabIndex = 0;
            this.btnOutputsConfig.Text = "Outputs Configuration... ( Dropbox, FTP, SendSpace etc. )";
            this.btnOutputsConfig.UseVisualStyleBackColor = true;
            this.btnOutputsConfig.Click += new System.EventHandler(this.btnOutputsConfig_Click);
            // 
            // chkPrinter
            // 
            this.chkPrinter.Location = new System.Drawing.Point(256, 64);
            this.chkPrinter.Name = "chkPrinter";
            this.chkPrinter.Size = new System.Drawing.Size(184, 24);
            this.chkPrinter.TabIndex = 3;
            this.chkPrinter.Text = "Send to Printer";
            this.chkPrinter.UseVisualStyleBackColor = true;
            // 
            // chkUpload
            // 
            this.chkUpload.Location = new System.Drawing.Point(256, 40);
            this.chkUpload.Name = "chkUpload";
            this.chkUpload.Size = new System.Drawing.Size(184, 24);
            this.chkUpload.TabIndex = 2;
            this.chkUpload.Text = "Upload to Remote Locations";
            this.chkUpload.UseVisualStyleBackColor = true;
            // 
            // chkClipboard
            // 
            this.chkClipboard.Location = new System.Drawing.Point(8, 40);
            this.chkClipboard.Name = "chkClipboard";
            this.chkClipboard.Size = new System.Drawing.Size(184, 24);
            this.chkClipboard.TabIndex = 1;
            this.chkClipboard.Text = "Copy Image or Text to Clipboard";
            this.chkClipboard.UseVisualStyleBackColor = true;
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(440, 360);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(104, 24);
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = "&Save && Close";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(552, 360);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(72, 24);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // hotkeyManager
            // 
            this.hmcHotkeys.Location = new System.Drawing.Point(16, 168);
            this.hmcHotkeys.Name = "hotkeyManager";
            this.hmcHotkeys.Size = new System.Drawing.Size(576, 48);
            this.hmcHotkeys.TabIndex = 11;
            // 
            // WorkflowWizard
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(656, 394);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.tcMain);
            this.MinimumSize = new System.Drawing.Size(664, 420);
            this.Name = "WorkflowWizard";
            this.Text = "ProfileWizard";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ProfileWizard_FormClosing);
            this.Load += new System.EventHandler(this.WorkflowWizard_Load);
            this.tcMain.ResumeLayout(false);
            this.tpAccessibility.ResumeLayout(false);
            this.tpAccessibility.PerformLayout();
            this.gbTask.ResumeLayout(false);
            this.gbName.ResumeLayout(false);
            this.gbName.PerformLayout();
            this.tpOutputs.ResumeLayout(false);
            this.gbRemoteLocations.ResumeLayout(false);
            this.gbSaveToFile.ResumeLayout(false);
            this.gbSaveToFile.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tcMain;
        private System.Windows.Forms.TabPage tpAccessibility;
        private System.Windows.Forms.ComboBox cboTask;
        private System.Windows.Forms.TabPage tpOutputs;
        private System.Windows.Forms.TabPage tpEditing;
        private System.Windows.Forms.CheckBox chkPrinter;
        private System.Windows.Forms.CheckBox chkUpload;
        private System.Windows.Forms.CheckBox chkClipboard;
        private System.Windows.Forms.Button btnOutputsConfig;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.CheckBox chkUseHotkey;
        private System.Windows.Forms.CheckBox chkSaveFile;
        private System.Windows.Forms.GroupBox gbName;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.GroupBox gbSaveToFile;
        private System.Windows.Forms.TextBox txtSaveFolder;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.GroupBox gbTask;
        private System.Windows.Forms.GroupBox gbRemoteLocations;
        private System.Windows.Forms.CheckBox chkSendspace;
        private System.Windows.Forms.CheckBox chkUploadFTP;
        private System.Windows.Forms.CheckBox chkUploadDropbox;
        private System.Windows.Forms.TabPage tpCapture;
        private HelpersLib.Hotkey.HotkeyManagerControl hmcHotkeys;
    }
}
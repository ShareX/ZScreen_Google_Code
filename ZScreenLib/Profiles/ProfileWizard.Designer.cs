namespace ZScreenLib
{
    partial class ProfileWizard
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
            this.gbName = new System.Windows.Forms.GroupBox();
            this.txtName = new System.Windows.Forms.TextBox();
            this.gbHotey = new System.Windows.Forms.GroupBox();
            this.txtHotkey = new System.Windows.Forms.TextBox();
            this.cbAlt = new System.Windows.Forms.CheckBox();
            this.cbShift = new System.Windows.Forms.CheckBox();
            this.cbControl = new System.Windows.Forms.CheckBox();
            this.chkUseHotkey = new System.Windows.Forms.CheckBox();
            this.cboTask = new System.Windows.Forms.ComboBox();
            this.tpEditing = new System.Windows.Forms.TabPage();
            this.tpOutputs = new System.Windows.Forms.TabPage();
            this.gbSaveFolder = new System.Windows.Forms.GroupBox();
            this.txtSaveFolder = new System.Windows.Forms.TextBox();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.chkSaveFile = new System.Windows.Forms.CheckBox();
            this.chkPrinter = new System.Windows.Forms.CheckBox();
            this.chkUpload = new System.Windows.Forms.CheckBox();
            this.chkClipboard = new System.Windows.Forms.CheckBox();
            this.btnOutputsConfig = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.gbTask = new System.Windows.Forms.GroupBox();
            this.tcMain.SuspendLayout();
            this.tpAccessibility.SuspendLayout();
            this.gbName.SuspendLayout();
            this.gbHotey.SuspendLayout();
            this.tpOutputs.SuspendLayout();
            this.gbSaveFolder.SuspendLayout();
            this.gbTask.SuspendLayout();
            this.SuspendLayout();
            // 
            // tcMain
            // 
            this.tcMain.Controls.Add(this.tpAccessibility);
            this.tcMain.Controls.Add(this.tpEditing);
            this.tcMain.Controls.Add(this.tpOutputs);
            this.tcMain.Location = new System.Drawing.Point(8, 8);
            this.tcMain.Name = "tcMain";
            this.tcMain.SelectedIndex = 0;
            this.tcMain.Size = new System.Drawing.Size(624, 328);
            this.tcMain.TabIndex = 0;
            // 
            // tpAccessibility
            // 
            this.tpAccessibility.Controls.Add(this.gbTask);
            this.tpAccessibility.Controls.Add(this.gbName);
            this.tpAccessibility.Controls.Add(this.gbHotey);
            this.tpAccessibility.Controls.Add(this.chkUseHotkey);
            this.tpAccessibility.Location = new System.Drawing.Point(4, 22);
            this.tpAccessibility.Name = "tpAccessibility";
            this.tpAccessibility.Padding = new System.Windows.Forms.Padding(3);
            this.tpAccessibility.Size = new System.Drawing.Size(616, 302);
            this.tpAccessibility.TabIndex = 0;
            this.tpAccessibility.Text = "Accessibility";
            this.tpAccessibility.UseVisualStyleBackColor = true;
            // 
            // gbName
            // 
            this.gbName.Controls.Add(this.txtName);
            this.gbName.Location = new System.Drawing.Point(8, 8);
            this.gbName.Name = "gbName";
            this.gbName.Size = new System.Drawing.Size(584, 56);
            this.gbName.TabIndex = 9;
            this.gbName.TabStop = false;
            this.gbName.Text = "Name";
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(8, 24);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(560, 20);
            this.txtName.TabIndex = 7;
            // 
            // gbHotey
            // 
            this.gbHotey.Controls.Add(this.txtHotkey);
            this.gbHotey.Controls.Add(this.cbAlt);
            this.gbHotey.Controls.Add(this.cbShift);
            this.gbHotey.Controls.Add(this.cbControl);
            this.gbHotey.Location = new System.Drawing.Point(8, 160);
            this.gbHotey.Name = "gbHotey";
            this.gbHotey.Size = new System.Drawing.Size(288, 56);
            this.gbHotey.TabIndex = 8;
            this.gbHotey.TabStop = false;
            this.gbHotey.Text = "Hotkey Configuration";
            // 
            // txtHotkey
            // 
            this.txtHotkey.Location = new System.Drawing.Point(168, 24);
            this.txtHotkey.Name = "txtHotkey";
            this.txtHotkey.Size = new System.Drawing.Size(100, 20);
            this.txtHotkey.TabIndex = 6;
            // 
            // cbAlt
            // 
            this.cbAlt.AutoSize = true;
            this.cbAlt.Location = new System.Drawing.Point(124, 24);
            this.cbAlt.Name = "cbAlt";
            this.cbAlt.Size = new System.Drawing.Size(38, 17);
            this.cbAlt.TabIndex = 5;
            this.cbAlt.Text = "Alt";
            this.cbAlt.UseVisualStyleBackColor = true;
            // 
            // cbShift
            // 
            this.cbShift.AutoSize = true;
            this.cbShift.Location = new System.Drawing.Point(71, 24);
            this.cbShift.Name = "cbShift";
            this.cbShift.Size = new System.Drawing.Size(47, 17);
            this.cbShift.TabIndex = 4;
            this.cbShift.Text = "Shift";
            this.cbShift.UseVisualStyleBackColor = true;
            // 
            // cbControl
            // 
            this.cbControl.AutoSize = true;
            this.cbControl.Location = new System.Drawing.Point(24, 24);
            this.cbControl.Name = "cbControl";
            this.cbControl.Size = new System.Drawing.Size(41, 17);
            this.cbControl.TabIndex = 3;
            this.cbControl.Text = "Ctrl";
            this.cbControl.UseVisualStyleBackColor = true;
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
            // cboTask
            // 
            this.cboTask.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTask.FormattingEnabled = true;
            this.cboTask.Location = new System.Drawing.Point(8, 24);
            this.cboTask.Name = "cboTask";
            this.cboTask.Size = new System.Drawing.Size(360, 21);
            this.cboTask.TabIndex = 0;
            // 
            // tpEditing
            // 
            this.tpEditing.Location = new System.Drawing.Point(4, 22);
            this.tpEditing.Name = "tpEditing";
            this.tpEditing.Padding = new System.Windows.Forms.Padding(3);
            this.tpEditing.Size = new System.Drawing.Size(616, 302);
            this.tpEditing.TabIndex = 4;
            this.tpEditing.Text = "Editing";
            this.tpEditing.UseVisualStyleBackColor = true;
            // 
            // tpOutputs
            // 
            this.tpOutputs.Controls.Add(this.gbSaveFolder);
            this.tpOutputs.Controls.Add(this.chkSaveFile);
            this.tpOutputs.Controls.Add(this.chkPrinter);
            this.tpOutputs.Controls.Add(this.chkUpload);
            this.tpOutputs.Controls.Add(this.chkClipboard);
            this.tpOutputs.Controls.Add(this.btnOutputsConfig);
            this.tpOutputs.Location = new System.Drawing.Point(4, 22);
            this.tpOutputs.Name = "tpOutputs";
            this.tpOutputs.Padding = new System.Windows.Forms.Padding(3);
            this.tpOutputs.Size = new System.Drawing.Size(616, 302);
            this.tpOutputs.TabIndex = 2;
            this.tpOutputs.Text = "Outputs";
            this.tpOutputs.UseVisualStyleBackColor = true;
            // 
            // gbSaveFolder
            // 
            this.gbSaveFolder.Controls.Add(this.txtSaveFolder);
            this.gbSaveFolder.Controls.Add(this.btnBrowse);
            this.gbSaveFolder.Location = new System.Drawing.Point(24, 56);
            this.gbSaveFolder.Name = "gbSaveFolder";
            this.gbSaveFolder.Size = new System.Drawing.Size(576, 64);
            this.gbSaveFolder.TabIndex = 5;
            this.gbSaveFolder.TabStop = false;
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
            this.chkSaveFile.Location = new System.Drawing.Point(8, 32);
            this.chkSaveFile.Name = "chkSaveFile";
            this.chkSaveFile.Size = new System.Drawing.Size(184, 24);
            this.chkSaveFile.TabIndex = 4;
            this.chkSaveFile.Text = "Save to file";
            this.chkSaveFile.UseVisualStyleBackColor = true;
            // 
            // chkPrinter
            // 
            this.chkPrinter.Location = new System.Drawing.Point(8, 152);
            this.chkPrinter.Name = "chkPrinter";
            this.chkPrinter.Size = new System.Drawing.Size(184, 24);
            this.chkPrinter.TabIndex = 3;
            this.chkPrinter.Text = "Send to Printer";
            this.chkPrinter.UseVisualStyleBackColor = true;
            // 
            // chkUpload
            // 
            this.chkUpload.Location = new System.Drawing.Point(8, 128);
            this.chkUpload.Name = "chkUpload";
            this.chkUpload.Size = new System.Drawing.Size(184, 24);
            this.chkUpload.TabIndex = 2;
            this.chkUpload.Text = "Upload to a Remote Location";
            this.chkUpload.UseVisualStyleBackColor = true;
            // 
            // chkClipboard
            // 
            this.chkClipboard.Location = new System.Drawing.Point(8, 8);
            this.chkClipboard.Name = "chkClipboard";
            this.chkClipboard.Size = new System.Drawing.Size(184, 24);
            this.chkClipboard.TabIndex = 1;
            this.chkClipboard.Text = "Copy Image or Text to Clipboard";
            this.chkClipboard.UseVisualStyleBackColor = true;
            // 
            // btnOutputsConfig
            // 
            this.btnOutputsConfig.Location = new System.Drawing.Point(8, 264);
            this.btnOutputsConfig.Name = "btnOutputsConfig";
            this.btnOutputsConfig.Size = new System.Drawing.Size(144, 24);
            this.btnOutputsConfig.TabIndex = 0;
            this.btnOutputsConfig.Text = "Outputs Configuration...";
            this.btnOutputsConfig.UseVisualStyleBackColor = true;
            this.btnOutputsConfig.Click += new System.EventHandler(this.btnOutputsConfig_Click);
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(472, 344);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(72, 24);
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = "&OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(552, 344);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(72, 24);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
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
            // ProfileWizard
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(656, 378);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.tcMain);
            this.Name = "ProfileWizard";
            this.Text = "ProfileWizard";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ProfileWizard_FormClosing);
            this.Load += new System.EventHandler(this.ProfileWizard_Load);
            this.tcMain.ResumeLayout(false);
            this.tpAccessibility.ResumeLayout(false);
            this.tpAccessibility.PerformLayout();
            this.gbName.ResumeLayout(false);
            this.gbName.PerformLayout();
            this.gbHotey.ResumeLayout(false);
            this.gbHotey.PerformLayout();
            this.tpOutputs.ResumeLayout(false);
            this.gbSaveFolder.ResumeLayout(false);
            this.gbSaveFolder.PerformLayout();
            this.gbTask.ResumeLayout(false);
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
        private System.Windows.Forms.GroupBox gbHotey;
        private System.Windows.Forms.TextBox txtHotkey;
        private System.Windows.Forms.CheckBox cbAlt;
        private System.Windows.Forms.CheckBox cbShift;
        private System.Windows.Forms.CheckBox cbControl;
        private System.Windows.Forms.CheckBox chkUseHotkey;
        private System.Windows.Forms.CheckBox chkSaveFile;
        private System.Windows.Forms.GroupBox gbName;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.GroupBox gbSaveFolder;
        private System.Windows.Forms.TextBox txtSaveFolder;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.GroupBox gbTask;
    }
}
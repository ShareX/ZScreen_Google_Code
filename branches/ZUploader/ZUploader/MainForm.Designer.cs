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
            this.label1 = new System.Windows.Forms.Label();
            this.cbImageUploaderDestination = new System.Windows.Forms.ComboBox();
            this.btnClipboardUpload = new System.Windows.Forms.Button();
            this.lvUploads = new System.Windows.Forms.ListView();
            this.chID = new System.Windows.Forms.ColumnHeader();
            this.chStatus = new System.Windows.Forms.ColumnHeader();
            this.chURL = new System.Windows.Forms.ColumnHeader();
            this.label2 = new System.Windows.Forms.Label();
            this.cbTextUploaderDestination = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cbFileUploaderDestination = new System.Windows.Forms.ComboBox();
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
            this.cbImageUploaderDestination.Size = new System.Drawing.Size(224, 21);
            this.cbImageUploaderDestination.TabIndex = 1;
            this.cbImageUploaderDestination.SelectedIndexChanged += new System.EventHandler(this.cbImageUploaderDestination_SelectedIndexChanged);
            // 
            // btnClipboardUpload
            // 
            this.btnClipboardUpload.Location = new System.Drawing.Point(336, 8);
            this.btnClipboardUpload.Name = "btnClipboardUpload";
            this.btnClipboardUpload.Size = new System.Drawing.Size(112, 72);
            this.btnClipboardUpload.TabIndex = 2;
            this.btnClipboardUpload.Text = "Clipboard upload";
            this.btnClipboardUpload.UseVisualStyleBackColor = true;
            this.btnClipboardUpload.Click += new System.EventHandler(this.btnClipboardUpload_Click);
            // 
            // lvUploads
            // 
            this.lvUploads.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chID,
            this.chStatus,
            this.chURL});
            this.lvUploads.Location = new System.Drawing.Point(8, 88);
            this.lvUploads.Name = "lvUploads";
            this.lvUploads.Size = new System.Drawing.Size(440, 216);
            this.lvUploads.TabIndex = 3;
            this.lvUploads.UseCompatibleStateImageBehavior = false;
            this.lvUploads.View = System.Windows.Forms.View.Details;
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
            this.cbTextUploaderDestination.Size = new System.Drawing.Size(224, 21);
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
            this.cbFileUploaderDestination.Size = new System.Drawing.Size(224, 21);
            this.cbFileUploaderDestination.TabIndex = 1;
            this.cbFileUploaderDestination.SelectedIndexChanged += new System.EventHandler(this.cbFileUploaderDestination_SelectedIndexChanged);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(457, 311);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lvUploads);
            this.Controls.Add(this.btnClipboardUpload);
            this.Controls.Add(this.cbFileUploaderDestination);
            this.Controls.Add(this.cbTextUploaderDestination);
            this.Controls.Add(this.cbImageUploaderDestination);
            this.Controls.Add(this.label1);
            this.Name = "MainForm";
            this.Text = "ZUploader";
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
    }
}


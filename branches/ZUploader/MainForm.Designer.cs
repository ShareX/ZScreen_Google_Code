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
            this.cbImageUploaderDestination.Size = new System.Drawing.Size(272, 21);
            this.cbImageUploaderDestination.TabIndex = 1;
            this.cbImageUploaderDestination.SelectedIndexChanged += new System.EventHandler(this.cbImageUploaderDestination_SelectedIndexChanged);
            // 
            // btnClipboardUpload
            // 
            this.btnClipboardUpload.Location = new System.Drawing.Point(8, 40);
            this.btnClipboardUpload.Name = "btnClipboardUpload";
            this.btnClipboardUpload.Size = new System.Drawing.Size(368, 23);
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
            this.lvUploads.Location = new System.Drawing.Point(8, 72);
            this.lvUploads.Name = "lvUploads";
            this.lvUploads.Size = new System.Drawing.Size(368, 200);
            this.lvUploads.TabIndex = 3;
            this.lvUploads.UseCompatibleStateImageBehavior = false;
            this.lvUploads.View = System.Windows.Forms.View.Details;
            // 
            // chID
            // 
            this.chID.Text = "ID";
            // 
            // chStatus
            // 
            this.chStatus.Text = "Status";
            this.chStatus.Width = 100;
            // 
            // chURL
            // 
            this.chURL.Text = "URL";
            this.chURL.Width = 200;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(385, 284);
            this.Controls.Add(this.lvUploads);
            this.Controls.Add(this.btnClipboardUpload);
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
    }
}


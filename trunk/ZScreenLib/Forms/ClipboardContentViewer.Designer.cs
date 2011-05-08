namespace ZScreenLib
{
    partial class ClipboardContentViewer
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
            this.lblQuestion = new System.Windows.Forms.Label();
            this.pbClipboard = new System.Windows.Forms.PictureBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.txtClipboard = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.pbClipboard)).BeginInit();
            this.SuspendLayout();
            // 
            // lblQuestion
            // 
            this.lblQuestion.BackColor = System.Drawing.Color.Orange;
            this.lblQuestion.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblQuestion.Location = new System.Drawing.Point(0, 0);
            this.lblQuestion.Name = "lblQuestion";
            this.lblQuestion.Size = new System.Drawing.Size(384, 24);
            this.lblQuestion.TabIndex = 0;
            this.lblQuestion.Text = "Your clipboard contains the following:";
            this.lblQuestion.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pbClipboard
            // 
            this.pbClipboard.Location = new System.Drawing.Point(64, 53);
            this.pbClipboard.Name = "pbClipboard";
            this.pbClipboard.Size = new System.Drawing.Size(256, 256);
            this.pbClipboard.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbClipboard.TabIndex = 1;
            this.pbClipboard.TabStop = false;
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(216, 328);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 2;
            this.btnOK.Text = "&OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(296, 328);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // txtClipboard
            // 
            this.txtClipboard.Location = new System.Drawing.Point(64, 53);
            this.txtClipboard.Multiline = true;
            this.txtClipboard.Name = "txtClipboard";
            this.txtClipboard.ReadOnly = true;
            this.txtClipboard.Size = new System.Drawing.Size(256, 256);
            this.txtClipboard.TabIndex = 4;
            // 
            // ClipboardContentViewer
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 362);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.lblQuestion);
            this.Controls.Add(this.txtClipboard);
            this.Controls.Add(this.pbClipboard);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ClipboardContentViewer";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Are you sure you want to upload?";
            this.Load += new System.EventHandler(this.ClipboardContentViewer_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pbClipboard)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblQuestion;
        private System.Windows.Forms.PictureBox pbClipboard;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        public System.Windows.Forms.TextBox txtClipboard;
    }
}
namespace ZSS.UpdateCheckerLib
{
    partial class NewVersionWindow
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
            this.btnYes = new System.Windows.Forms.Button();
            this.btnNo = new System.Windows.Forms.Button();
            this.txtVer = new System.Windows.Forms.TextBox();
            this.lblVer = new System.Windows.Forms.Label();
            this.pbApp = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pbApp)).BeginInit();
            this.SuspendLayout();
            // 
            // btnYes
            // 
            this.btnYes.Location = new System.Drawing.Point(312, 80);
            this.btnYes.Name = "btnYes";
            this.btnYes.Size = new System.Drawing.Size(83, 24);
            this.btnYes.TabIndex = 0;
            this.btnYes.Text = "&Yes";
            this.btnYes.UseVisualStyleBackColor = true;
            this.btnYes.Click += new System.EventHandler(this.btnYes_Click);
            // 
            // btnNo
            // 
            this.btnNo.Location = new System.Drawing.Point(400, 80);
            this.btnNo.Name = "btnNo";
            this.btnNo.Size = new System.Drawing.Size(83, 24);
            this.btnNo.TabIndex = 1;
            this.btnNo.Text = "&No";
            this.btnNo.UseVisualStyleBackColor = true;
            this.btnNo.Click += new System.EventHandler(this.btnNo_Click);
            // 
            // txtVer
            // 
            this.txtVer.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtVer.Location = new System.Drawing.Point(8, 112);
            this.txtVer.Multiline = true;
            this.txtVer.Name = "txtVer";
            this.txtVer.ReadOnly = true;
            this.txtVer.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtVer.Size = new System.Drawing.Size(476, 184);
            this.txtVer.TabIndex = 2;
            this.txtVer.TabStop = false;
            this.txtVer.WordWrap = false;
            // 
            // lblVer
            // 
            this.lblVer.BackColor = System.Drawing.Color.Transparent;
            this.lblVer.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblVer.Location = new System.Drawing.Point(136, 8);
            this.lblVer.Name = "lblVer";
            this.lblVer.Size = new System.Drawing.Size(344, 100);
            this.lblVer.TabIndex = 3;
            this.lblVer.Text = "New Version is available";
            // 
            // pbApp
            // 
            this.pbApp.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbApp.Location = new System.Drawing.Point(8, 8);
            this.pbApp.Name = "pbApp";
            this.pbApp.Size = new System.Drawing.Size(116, 100);
            this.pbApp.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbApp.TabIndex = 4;
            this.pbApp.TabStop = false;
            // 
            // NewVersionWindow
            // 
            this.AcceptButton = this.btnYes;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(493, 305);
            this.Controls.Add(this.pbApp);
            this.Controls.Add(this.txtVer);
            this.Controls.Add(this.btnNo);
            this.Controls.Add(this.btnYes);
            this.Controls.Add(this.lblVer);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "NewVersionWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "A New Version is Available";
            ((System.ComponentModel.ISupportInitialize)(this.pbApp)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnYes;
        private System.Windows.Forms.Button btnNo;
        private System.Windows.Forms.TextBox txtVer;
        private System.Windows.Forms.Label lblVer;
        private System.Windows.Forms.PictureBox pbApp;
    }
}
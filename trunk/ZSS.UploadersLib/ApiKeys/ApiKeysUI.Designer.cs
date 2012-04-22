namespace UploadersLib
{
    partial class ApiKeysUI
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
            this.pgAppConfig = new System.Windows.Forms.PropertyGrid();
            this.btnReset = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // pgAppConfig
            // 
            this.pgAppConfig.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pgAppConfig.Location = new System.Drawing.Point(0, 0);
            this.pgAppConfig.Name = "pgAppConfig";
            this.pgAppConfig.Size = new System.Drawing.Size(568, 366);
            this.pgAppConfig.TabIndex = 0;
            // 
            // btnReset
            // 
            this.btnReset.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnReset.Location = new System.Drawing.Point(488, 336);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(74, 22);
            this.btnReset.TabIndex = 1;
            this.btnReset.Text = "&Reset";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // ApiKeysUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(568, 366);
            this.Controls.Add(this.btnReset);
            this.Controls.Add(this.pgAppConfig);
            this.MinimumSize = new System.Drawing.Size(576, 400);
            this.Name = "ApiKeysUI";
            this.Text = "ApiKeysUI";
            this.Load += new System.EventHandler(this.ApiKeysUI_Load);
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.PropertyGrid pgAppConfig;
        private System.Windows.Forms.Button btnReset;
    }
}
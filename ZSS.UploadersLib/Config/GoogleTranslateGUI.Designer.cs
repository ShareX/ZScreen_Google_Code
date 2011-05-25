namespace UploadersLib
{
    partial class GoogleTranslateGUI
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
            this.cbLanguageAutoDetect = new System.Windows.Forms.CheckBox();
            this.txtAutoTranslate = new System.Windows.Forms.TextBox();
            this.cbAutoTranslate = new System.Windows.Forms.CheckBox();
            this.btnTranslateTo1 = new System.Windows.Forms.Button();
            this.txtTranslateResult = new System.Windows.Forms.TextBox();
            this.txtLanguages = new System.Windows.Forms.TextBox();
            this.btnTranslate = new System.Windows.Forms.Button();
            this.txtTranslateText = new System.Windows.Forms.TextBox();
            this.lblToLanguage = new System.Windows.Forms.Label();
            this.lblFromLanguage = new System.Windows.Forms.Label();
            this.cbToLanguage = new System.Windows.Forms.ComboBox();
            this.cbFromLanguage = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // cbLanguageAutoDetect
            // 
            this.cbLanguageAutoDetect.AutoSize = true;
            this.cbLanguageAutoDetect.Location = new System.Drawing.Point(224, 18);
            this.cbLanguageAutoDetect.Name = "cbLanguageAutoDetect";
            this.cbLanguageAutoDetect.Size = new System.Drawing.Size(128, 17);
            this.cbLanguageAutoDetect.TabIndex = 25;
            this.cbLanguageAutoDetect.Text = "Auto detect language";
            this.cbLanguageAutoDetect.UseVisualStyleBackColor = true;
            this.cbLanguageAutoDetect.CheckedChanged += new System.EventHandler(this.cbLanguageAutoDetect_CheckedChanged);
            // 
            // txtAutoTranslate
            // 
            this.txtAutoTranslate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtAutoTranslate.Location = new System.Drawing.Point(440, 254);
            this.txtAutoTranslate.Name = "txtAutoTranslate";
            this.txtAutoTranslate.Size = new System.Drawing.Size(56, 20);
            this.txtAutoTranslate.TabIndex = 24;
            this.txtAutoTranslate.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // cbAutoTranslate
            // 
            this.cbAutoTranslate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cbAutoTranslate.AutoSize = true;
            this.cbAutoTranslate.Location = new System.Drawing.Point(24, 256);
            this.cbAutoTranslate.Name = "cbAutoTranslate";
            this.cbAutoTranslate.Size = new System.Drawing.Size(416, 17);
            this.cbAutoTranslate.TabIndex = 23;
            this.cbAutoTranslate.Text = "Automatically translate text instead of uploading text if the text length is smal" +
                "ler than";
            this.cbAutoTranslate.UseVisualStyleBackColor = true;
            this.cbAutoTranslate.CheckedChanged += new System.EventHandler(this.cbAutoTranslate_CheckedChanged);
            // 
            // btnTranslateTo1
            // 
            this.btnTranslateTo1.AllowDrop = true;
            this.btnTranslateTo1.Location = new System.Drawing.Point(216, 208);
            this.btnTranslateTo1.Name = "btnTranslateTo1";
            this.btnTranslateTo1.Size = new System.Drawing.Size(136, 24);
            this.btnTranslateTo1.TabIndex = 22;
            this.btnTranslateTo1.Text = "???";
            this.btnTranslateTo1.UseVisualStyleBackColor = true;
            this.btnTranslateTo1.Click += new System.EventHandler(this.btnTranslateTo1_Click);
            this.btnTranslateTo1.DragDrop += new System.Windows.Forms.DragEventHandler(this.btnTranslateTo1_DragDrop);
            this.btnTranslateTo1.DragEnter += new System.Windows.Forms.DragEventHandler(this.btnTranslateTo1_DragEnter);
            // 
            // txtTranslateResult
            // 
            this.txtTranslateResult.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.txtTranslateResult.Location = new System.Drawing.Point(360, 104);
            this.txtTranslateResult.Multiline = true;
            this.txtTranslateResult.Name = "txtTranslateResult";
            this.txtTranslateResult.ReadOnly = true;
            this.txtTranslateResult.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtTranslateResult.Size = new System.Drawing.Size(336, 96);
            this.txtTranslateResult.TabIndex = 21;
            // 
            // txtLanguages
            // 
            this.txtLanguages.Location = new System.Drawing.Point(360, 80);
            this.txtLanguages.Name = "txtLanguages";
            this.txtLanguages.ReadOnly = true;
            this.txtLanguages.Size = new System.Drawing.Size(336, 20);
            this.txtLanguages.TabIndex = 20;
            // 
            // btnTranslate
            // 
            this.btnTranslate.Location = new System.Drawing.Point(16, 208);
            this.btnTranslate.Name = "btnTranslate";
            this.btnTranslate.Size = new System.Drawing.Size(192, 24);
            this.btnTranslate.TabIndex = 18;
            this.btnTranslate.Text = "Translate ( Ctrl + Enter )";
            this.btnTranslate.UseVisualStyleBackColor = true;
            this.btnTranslate.Click += new System.EventHandler(this.btnTranslate_Click);
            this.btnTranslate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.btnTranslate_KeyDown);
            // 
            // txtTranslateText
            // 
            this.txtTranslateText.Location = new System.Drawing.Point(16, 80);
            this.txtTranslateText.Multiline = true;
            this.txtTranslateText.Name = "txtTranslateText";
            this.txtTranslateText.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtTranslateText.Size = new System.Drawing.Size(336, 120);
            this.txtTranslateText.TabIndex = 16;
            this.txtTranslateText.TextChanged += new System.EventHandler(this.txtTranslateText_TextChanged);
            this.txtTranslateText.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtTranslateText_KeyDown);
            // 
            // lblToLanguage
            // 
            this.lblToLanguage.Cursor = System.Windows.Forms.Cursors.SizeAll;
            this.lblToLanguage.Location = new System.Drawing.Point(16, 40);
            this.lblToLanguage.Name = "lblToLanguage";
            this.lblToLanguage.Size = new System.Drawing.Size(48, 32);
            this.lblToLanguage.TabIndex = 19;
            this.lblToLanguage.Text = "Target:";
            this.lblToLanguage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblToLanguage.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lblToLanguage_MouseDown);
            // 
            // lblFromLanguage
            // 
            this.lblFromLanguage.Location = new System.Drawing.Point(16, 10);
            this.lblFromLanguage.Name = "lblFromLanguage";
            this.lblFromLanguage.Size = new System.Drawing.Size(48, 32);
            this.lblFromLanguage.TabIndex = 17;
            this.lblFromLanguage.Text = "Source:";
            this.lblFromLanguage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cbToLanguage
            // 
            this.cbToLanguage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbToLanguage.Enabled = false;
            this.cbToLanguage.FormattingEnabled = true;
            this.cbToLanguage.Location = new System.Drawing.Point(72, 48);
            this.cbToLanguage.MaxDropDownItems = 20;
            this.cbToLanguage.Name = "cbToLanguage";
            this.cbToLanguage.Size = new System.Drawing.Size(144, 21);
            this.cbToLanguage.TabIndex = 15;
            this.cbToLanguage.SelectedIndexChanged += new System.EventHandler(this.cbToLanguage_SelectedIndexChanged);
            // 
            // cbFromLanguage
            // 
            this.cbFromLanguage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbFromLanguage.Enabled = false;
            this.cbFromLanguage.FormattingEnabled = true;
            this.cbFromLanguage.Location = new System.Drawing.Point(72, 16);
            this.cbFromLanguage.MaxDropDownItems = 20;
            this.cbFromLanguage.Name = "cbFromLanguage";
            this.cbFromLanguage.Size = new System.Drawing.Size(144, 21);
            this.cbFromLanguage.TabIndex = 14;
            this.cbFromLanguage.SelectedIndexChanged += new System.EventHandler(this.cbFromLanguage_SelectedIndexChanged);
            // 
            // GoogleTranslateGUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(713, 295);
            this.Controls.Add(this.cbLanguageAutoDetect);
            this.Controls.Add(this.txtAutoTranslate);
            this.Controls.Add(this.cbAutoTranslate);
            this.Controls.Add(this.btnTranslateTo1);
            this.Controls.Add(this.txtTranslateResult);
            this.Controls.Add(this.txtLanguages);
            this.Controls.Add(this.btnTranslate);
            this.Controls.Add(this.txtTranslateText);
            this.Controls.Add(this.lblToLanguage);
            this.Controls.Add(this.lblFromLanguage);
            this.Controls.Add(this.cbToLanguage);
            this.Controls.Add(this.cbFromLanguage);
            this.Name = "GoogleTranslateGUI";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Google Translate GUI";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox cbLanguageAutoDetect;
        public System.Windows.Forms.TextBox txtAutoTranslate;
        public System.Windows.Forms.CheckBox cbAutoTranslate;
        public System.Windows.Forms.Button btnTranslateTo1;
        public System.Windows.Forms.TextBox txtTranslateResult;
        public System.Windows.Forms.TextBox txtLanguages;
        public System.Windows.Forms.Button btnTranslate;
        public System.Windows.Forms.TextBox txtTranslateText;
        public System.Windows.Forms.Label lblToLanguage;
        public System.Windows.Forms.Label lblFromLanguage;
        public System.Windows.Forms.ComboBox cbToLanguage;
        public System.Windows.Forms.ComboBox cbFromLanguage;
    }
}
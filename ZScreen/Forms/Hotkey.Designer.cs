namespace ZSS
{
    partial class Hotkey
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.cboKey = new System.Windows.Forms.ComboBox();
            this.cbMod1 = new System.Windows.Forms.CheckBox();
            this.cbMod2 = new System.Windows.Forms.CheckBox();
            this.cboMod2 = new System.Windows.Forms.ComboBox();
            this.cboMod1 = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // cboKey
            // 
            this.cboKey.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboKey.FormattingEnabled = true;
            this.cboKey.Location = new System.Drawing.Point(3, 57);
            this.cboKey.Name = "cboKey";
            this.cboKey.Size = new System.Drawing.Size(121, 21);
            this.cboKey.TabIndex = 65;
            // 
            // cbMod1
            // 
            this.cbMod1.AutoSize = true;
            this.cbMod1.Location = new System.Drawing.Point(130, 6);
            this.cbMod1.Name = "cbMod1";
            this.cbMod1.Size = new System.Drawing.Size(15, 14);
            this.cbMod1.TabIndex = 66;
            this.cbMod1.UseVisualStyleBackColor = true;
            this.cbMod1.CheckedChanged += new System.EventHandler(this.cbMod1_CheckedChanged);
            // 
            // cbMod2
            // 
            this.cbMod2.AutoSize = true;
            this.cbMod2.Location = new System.Drawing.Point(130, 33);
            this.cbMod2.Name = "cbMod2";
            this.cbMod2.Size = new System.Drawing.Size(15, 14);
            this.cbMod2.TabIndex = 67;
            this.cbMod2.UseVisualStyleBackColor = true;
            this.cbMod2.CheckedChanged += new System.EventHandler(this.cbMod2_CheckedChanged);
            // 
            // cboMod2
            // 
            this.cboMod2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboMod2.Enabled = false;
            this.cboMod2.FormattingEnabled = true;
            this.cboMod2.Location = new System.Drawing.Point(3, 30);
            this.cboMod2.Name = "cboMod2";
            this.cboMod2.Size = new System.Drawing.Size(121, 21);
            this.cboMod2.TabIndex = 68;
            // 
            // cboMod1
            // 
            this.cboMod1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboMod1.Enabled = false;
            this.cboMod1.FormattingEnabled = true;
            this.cboMod1.Location = new System.Drawing.Point(3, 3);
            this.cboMod1.Name = "cboMod1";
            this.cboMod1.Size = new System.Drawing.Size(121, 21);
            this.cboMod1.TabIndex = 69;
            // 
            // Hotkey
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.cboMod1);
            this.Controls.Add(this.cboMod2);
            this.Controls.Add(this.cbMod2);
            this.Controls.Add(this.cbMod1);
            this.Controls.Add(this.cboKey);
            this.Name = "Hotkey";
            this.Size = new System.Drawing.Size(149, 84);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cboKey;
        private System.Windows.Forms.CheckBox cbMod1;
        private System.Windows.Forms.CheckBox cbMod2;
        private System.Windows.Forms.ComboBox cboMod2;
        private System.Windows.Forms.ComboBox cboMod1;
    }
}

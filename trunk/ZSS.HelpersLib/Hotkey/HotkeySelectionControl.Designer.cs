namespace HelpersLib.Hotkey
{
    partial class HotkeySelectionControl
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
            this.lblHotkeyDescription = new System.Windows.Forms.Label();
            this.btnSetHotkey = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblHotkeyDescription
            // 
            this.lblHotkeyDescription.AutoSize = true;
            this.lblHotkeyDescription.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblHotkeyDescription.Location = new System.Drawing.Point(8, 8);
            this.lblHotkeyDescription.Name = "lblHotkeyDescription";
            this.lblHotkeyDescription.Size = new System.Drawing.Size(76, 16);
            this.lblHotkeyDescription.TabIndex = 0;
            this.lblHotkeyDescription.Text = "Description";
            // 
            // btnSetHotkey
            // 
            this.btnSetHotkey.Location = new System.Drawing.Point(296, 5);
            this.btnSetHotkey.Name = "btnSetHotkey";
            this.btnSetHotkey.Size = new System.Drawing.Size(195, 23);
            this.btnSetHotkey.TabIndex = 1;
            this.btnSetHotkey.Text = "Ctrl + Shift + Alt + Print Screen";
            this.btnSetHotkey.UseVisualStyleBackColor = true;
            this.btnSetHotkey.Click += new System.EventHandler(this.btnSetHotkey_Click);
            // 
            // HotkeySelectionControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnSetHotkey);
            this.Controls.Add(this.lblHotkeyDescription);
            this.Name = "HotkeySelectionControl";
            this.Size = new System.Drawing.Size(500, 35);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblHotkeyDescription;
        private System.Windows.Forms.Button btnSetHotkey;
    }
}

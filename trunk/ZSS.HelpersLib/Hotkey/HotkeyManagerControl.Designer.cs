namespace HelpersLib.Hotkey
{
    partial class HotkeyManagerControl
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
            this.flpHotkeys = new System.Windows.Forms.FlowLayoutPanel();
            this.SuspendLayout();
            // 
            // flpHotkeys
            // 
            this.flpHotkeys.AutoScroll = true;
            this.flpHotkeys.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpHotkeys.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flpHotkeys.Location = new System.Drawing.Point(0, 0);
            this.flpHotkeys.Name = "flpHotkeys";
            this.flpHotkeys.Padding = new System.Windows.Forms.Padding(5);
            this.flpHotkeys.Size = new System.Drawing.Size(447, 256);
            this.flpHotkeys.TabIndex = 0;
            this.flpHotkeys.WrapContents = false;
            // 
            // HotkeyManagerControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.flpHotkeys);
            this.Name = "HotkeyManagerControl";
            this.Size = new System.Drawing.Size(447, 256);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel flpHotkeys;
    }
}

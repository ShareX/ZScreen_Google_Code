namespace Test
{
    partial class Form1
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
            this.glowLabel1 = new DavuxLib.Controls.GlowLabel();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // glowLabel1
            // 
            this.glowLabel1.AutoSize = true;
            this.glowLabel1.BackColor = System.Drawing.SystemColors.Control;
            this.glowLabel1.Enabled = false;
            this.glowLabel1.Location = new System.Drawing.Point(195, 9);
            this.glowLabel1.Name = "glowLabel1";
            this.glowLabel1.Size = new System.Drawing.Size(69, 17);
            this.glowLabel1.TabIndex = 0;
            this.glowLabel1.Text = "Aero Text";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(379, 9);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "Aero Button";
            this.button1.UseCompatibleTextRendering = true;
            this.button1.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(950, 669);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.glowLabel1);
            this.DoubleBuffered = true;
            this.GlassArea = new System.Windows.Forms.Padding(0, 40, 0, 0);
            this.Name = "Form1";
            this.NCAPainting = true;
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DavuxLib.Controls.GlowLabel glowLabel1;
        private System.Windows.Forms.Button button1;
    }
}


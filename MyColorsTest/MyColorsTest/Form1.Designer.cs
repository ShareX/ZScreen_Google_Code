using ZSS.Colors;

namespace MyColorsTest
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
            this.txtColorBox = new System.Windows.Forms.TextBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.txtColorSlider = new System.Windows.Forms.TextBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.colorSlider1 = new ZSS.Colors.ColorSlider();
            this.colorBox1 = new ZSS.Colors.ColorBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // txtColorBox
            // 
            this.txtColorBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtColorBox.Location = new System.Drawing.Point(8, 304);
            this.txtColorBox.Multiline = true;
            this.txtColorBox.Name = "txtColorBox";
            this.txtColorBox.Size = new System.Drawing.Size(248, 64);
            this.txtColorBox.TabIndex = 1;
            this.txtColorBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(8, 272);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(288, 21);
            this.comboBox1.TabIndex = 2;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // txtColorSlider
            // 
            this.txtColorSlider.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtColorSlider.Location = new System.Drawing.Point(8, 376);
            this.txtColorSlider.Multiline = true;
            this.txtColorSlider.Name = "txtColorSlider";
            this.txtColorSlider.Size = new System.Drawing.Size(248, 64);
            this.txtColorSlider.TabIndex = 5;
            this.txtColorSlider.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox1.Location = new System.Drawing.Point(256, 304);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(32, 64);
            this.pictureBox1.TabIndex = 8;
            this.pictureBox1.TabStop = false;
            // 
            // colorSlider1
            // 
            this.colorSlider1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.colorSlider1.DrawStyle = ZSS.Colors.DrawStyle.Hue;
            this.colorSlider1.Location = new System.Drawing.Point(264, 8);
            this.colorSlider1.Name = "colorSlider1";
            this.colorSlider1.Size = new System.Drawing.Size(30, 255);
            this.colorSlider1.TabIndex = 7;
            // 
            // colorBox1
            // 
            this.colorBox1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.colorBox1.DrawStyle = ZSS.Colors.DrawStyle.Hue;
            this.colorBox1.Location = new System.Drawing.Point(8, 8);
            this.colorBox1.Name = "colorBox1";
            this.colorBox1.Size = new System.Drawing.Size(255, 255);
            this.colorBox1.TabIndex = 6;
            // 
            // pictureBox2
            // 
            this.pictureBox2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox2.Location = new System.Drawing.Point(256, 376);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(32, 64);
            this.pictureBox2.TabIndex = 9;
            this.pictureBox2.TabStop = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gainsboro;
            this.ClientSize = new System.Drawing.Size(301, 451);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.colorSlider1);
            this.Controls.Add(this.colorBox1);
            this.Controls.Add(this.txtColorSlider);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.txtColorBox);
            this.Name = "Form1";
            this.Text = "MyColorsTest";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtColorBox;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.TextBox txtColorSlider;
        private ColorBox colorBox1;
        private ColorSlider colorSlider1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
    }
}
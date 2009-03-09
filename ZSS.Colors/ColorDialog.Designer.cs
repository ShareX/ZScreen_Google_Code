namespace ZSS.Colors
{
    partial class ColorDialog
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
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.lblOld = new System.Windows.Forms.Label();
            this.lblNew = new System.Windows.Forms.Label();
            this.txtHex = new System.Windows.Forms.TextBox();
            this.lblHex = new System.Windows.Forms.Label();
            this.nudKey = new System.Windows.Forms.NumericUpDown();
            this.nudYellow = new System.Windows.Forms.NumericUpDown();
            this.nudMagenta = new System.Windows.Forms.NumericUpDown();
            this.nudCyan = new System.Windows.Forms.NumericUpDown();
            this.lblKey = new System.Windows.Forms.Label();
            this.lblYellow = new System.Windows.Forms.Label();
            this.lblMagenta = new System.Windows.Forms.Label();
            this.lblCyan = new System.Windows.Forms.Label();
            this.lblHue = new System.Windows.Forms.Label();
            this.lblBrightness = new System.Windows.Forms.Label();
            this.lblSaturation = new System.Windows.Forms.Label();
            this.nudBlue = new System.Windows.Forms.NumericUpDown();
            this.nudGreen = new System.Windows.Forms.NumericUpDown();
            this.nudRed = new System.Windows.Forms.NumericUpDown();
            this.nudBrightness = new System.Windows.Forms.NumericUpDown();
            this.nudSaturation = new System.Windows.Forms.NumericUpDown();
            this.nudHue = new System.Windows.Forms.NumericUpDown();
            this.rbBlue = new System.Windows.Forms.RadioButton();
            this.rbGreen = new System.Windows.Forms.RadioButton();
            this.rbRed = new System.Windows.Forms.RadioButton();
            this.rbBrightness = new System.Windows.Forms.RadioButton();
            this.rbSaturation = new System.Windows.Forms.RadioButton();
            this.rbHue = new System.Windows.Forms.RadioButton();
            this.lblSecondaryColor = new System.Windows.Forms.Label();
            this.lblPrimaryColor = new System.Windows.Forms.Label();
            this.colorPicker = new ZSS.Colors.ColorPicker();
            ((System.ComponentModel.ISupportInitialize)(this.nudKey)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudYellow)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMagenta)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudCyan)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudBlue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudGreen)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudRed)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudBrightness)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudSaturation)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudHue)).BeginInit();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(568, 232);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 65;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(488, 232);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 64;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            // 
            // lblOld
            // 
            this.lblOld.AutoSize = true;
            this.lblOld.Location = new System.Drawing.Point(304, 240);
            this.lblOld.Name = "lblOld";
            this.lblOld.Size = new System.Drawing.Size(26, 13);
            this.lblOld.TabIndex = 63;
            this.lblOld.Text = "Old:";
            // 
            // lblNew
            // 
            this.lblNew.AutoSize = true;
            this.lblNew.Location = new System.Drawing.Point(304, 216);
            this.lblNew.Name = "lblNew";
            this.lblNew.Size = new System.Drawing.Size(32, 13);
            this.lblNew.TabIndex = 62;
            this.lblNew.Text = "New:";
            // 
            // txtHex
            // 
            this.txtHex.Location = new System.Drawing.Point(536, 144);
            this.txtHex.MaxLength = 7;
            this.txtHex.Name = "txtHex";
            this.txtHex.Size = new System.Drawing.Size(56, 20);
            this.txtHex.TabIndex = 61;
            this.txtHex.Text = "#FFFFFF";
            this.txtHex.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lblHex
            // 
            this.lblHex.AutoSize = true;
            this.lblHex.Location = new System.Drawing.Point(480, 148);
            this.lblHex.Name = "lblHex";
            this.lblHex.Size = new System.Drawing.Size(29, 13);
            this.lblHex.TabIndex = 60;
            this.lblHex.Text = "Hex:";
            // 
            // nudKey
            // 
            this.nudKey.Location = new System.Drawing.Point(536, 112);
            this.nudKey.Name = "nudKey";
            this.nudKey.Size = new System.Drawing.Size(48, 20);
            this.nudKey.TabIndex = 59;
            this.nudKey.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.nudKey.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // nudYellow
            // 
            this.nudYellow.Location = new System.Drawing.Point(536, 80);
            this.nudYellow.Name = "nudYellow";
            this.nudYellow.Size = new System.Drawing.Size(48, 20);
            this.nudYellow.TabIndex = 58;
            this.nudYellow.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.nudYellow.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // nudMagenta
            // 
            this.nudMagenta.Location = new System.Drawing.Point(536, 48);
            this.nudMagenta.Name = "nudMagenta";
            this.nudMagenta.Size = new System.Drawing.Size(48, 20);
            this.nudMagenta.TabIndex = 57;
            this.nudMagenta.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.nudMagenta.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // nudCyan
            // 
            this.nudCyan.Location = new System.Drawing.Point(536, 16);
            this.nudCyan.Name = "nudCyan";
            this.nudCyan.Size = new System.Drawing.Size(48, 20);
            this.nudCyan.TabIndex = 56;
            this.nudCyan.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.nudCyan.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // lblKey
            // 
            this.lblKey.AutoSize = true;
            this.lblKey.Location = new System.Drawing.Point(480, 116);
            this.lblKey.Name = "lblKey";
            this.lblKey.Size = new System.Drawing.Size(28, 13);
            this.lblKey.TabIndex = 55;
            this.lblKey.Text = "Key:";
            // 
            // lblYellow
            // 
            this.lblYellow.AutoSize = true;
            this.lblYellow.Location = new System.Drawing.Point(480, 84);
            this.lblYellow.Name = "lblYellow";
            this.lblYellow.Size = new System.Drawing.Size(41, 13);
            this.lblYellow.TabIndex = 54;
            this.lblYellow.Text = "Yellow:";
            // 
            // lblMagenta
            // 
            this.lblMagenta.AutoSize = true;
            this.lblMagenta.Location = new System.Drawing.Point(480, 52);
            this.lblMagenta.Name = "lblMagenta";
            this.lblMagenta.Size = new System.Drawing.Size(52, 13);
            this.lblMagenta.TabIndex = 53;
            this.lblMagenta.Text = "Magenta:";
            // 
            // lblCyan
            // 
            this.lblCyan.AutoSize = true;
            this.lblCyan.Location = new System.Drawing.Point(480, 20);
            this.lblCyan.Name = "lblCyan";
            this.lblCyan.Size = new System.Drawing.Size(34, 13);
            this.lblCyan.TabIndex = 52;
            this.lblCyan.Text = "Cyan:";
            // 
            // lblHue
            // 
            this.lblHue.AutoSize = true;
            this.lblHue.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblHue.Location = new System.Drawing.Point(437, 19);
            this.lblHue.Name = "lblHue";
            this.lblHue.Size = new System.Drawing.Size(13, 13);
            this.lblHue.TabIndex = 51;
            this.lblHue.Text = "°";
            // 
            // lblBrightness
            // 
            this.lblBrightness.AutoSize = true;
            this.lblBrightness.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblBrightness.Location = new System.Drawing.Point(437, 83);
            this.lblBrightness.Name = "lblBrightness";
            this.lblBrightness.Size = new System.Drawing.Size(19, 13);
            this.lblBrightness.TabIndex = 50;
            this.lblBrightness.Text = "%";
            // 
            // lblSaturation
            // 
            this.lblSaturation.AutoSize = true;
            this.lblSaturation.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblSaturation.Location = new System.Drawing.Point(437, 51);
            this.lblSaturation.Name = "lblSaturation";
            this.lblSaturation.Size = new System.Drawing.Size(19, 13);
            this.lblSaturation.TabIndex = 49;
            this.lblSaturation.Text = "%";
            // 
            // nudBlue
            // 
            this.nudBlue.Location = new System.Drawing.Point(384, 176);
            this.nudBlue.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.nudBlue.Name = "nudBlue";
            this.nudBlue.Size = new System.Drawing.Size(48, 20);
            this.nudBlue.TabIndex = 48;
            this.nudBlue.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.nudBlue.Value = new decimal(new int[] {
            255,
            0,
            0,
            0});
            // 
            // nudGreen
            // 
            this.nudGreen.Location = new System.Drawing.Point(384, 144);
            this.nudGreen.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.nudGreen.Name = "nudGreen";
            this.nudGreen.Size = new System.Drawing.Size(48, 20);
            this.nudGreen.TabIndex = 47;
            this.nudGreen.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.nudGreen.Value = new decimal(new int[] {
            255,
            0,
            0,
            0});
            // 
            // nudRed
            // 
            this.nudRed.Location = new System.Drawing.Point(384, 112);
            this.nudRed.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.nudRed.Name = "nudRed";
            this.nudRed.Size = new System.Drawing.Size(48, 20);
            this.nudRed.TabIndex = 46;
            this.nudRed.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.nudRed.Value = new decimal(new int[] {
            255,
            0,
            0,
            0});
            // 
            // nudBrightness
            // 
            this.nudBrightness.Location = new System.Drawing.Point(384, 80);
            this.nudBrightness.Name = "nudBrightness";
            this.nudBrightness.Size = new System.Drawing.Size(48, 20);
            this.nudBrightness.TabIndex = 45;
            this.nudBrightness.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.nudBrightness.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // nudSaturation
            // 
            this.nudSaturation.Location = new System.Drawing.Point(384, 48);
            this.nudSaturation.Name = "nudSaturation";
            this.nudSaturation.Size = new System.Drawing.Size(48, 20);
            this.nudSaturation.TabIndex = 44;
            this.nudSaturation.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.nudSaturation.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // nudHue
            // 
            this.nudHue.Location = new System.Drawing.Point(384, 16);
            this.nudHue.Maximum = new decimal(new int[] {
            360,
            0,
            0,
            0});
            this.nudHue.Name = "nudHue";
            this.nudHue.Size = new System.Drawing.Size(48, 20);
            this.nudHue.TabIndex = 43;
            this.nudHue.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.nudHue.Value = new decimal(new int[] {
            360,
            0,
            0,
            0});
            // 
            // rbBlue
            // 
            this.rbBlue.AutoSize = true;
            this.rbBlue.Location = new System.Drawing.Point(304, 176);
            this.rbBlue.Name = "rbBlue";
            this.rbBlue.Size = new System.Drawing.Size(49, 17);
            this.rbBlue.TabIndex = 42;
            this.rbBlue.TabStop = true;
            this.rbBlue.Text = "Blue:";
            this.rbBlue.UseVisualStyleBackColor = true;
            // 
            // rbGreen
            // 
            this.rbGreen.AutoSize = true;
            this.rbGreen.Location = new System.Drawing.Point(304, 144);
            this.rbGreen.Name = "rbGreen";
            this.rbGreen.Size = new System.Drawing.Size(57, 17);
            this.rbGreen.TabIndex = 41;
            this.rbGreen.TabStop = true;
            this.rbGreen.Text = "Green:";
            this.rbGreen.UseVisualStyleBackColor = true;
            // 
            // rbRed
            // 
            this.rbRed.AutoSize = true;
            this.rbRed.Location = new System.Drawing.Point(304, 112);
            this.rbRed.Name = "rbRed";
            this.rbRed.Size = new System.Drawing.Size(48, 17);
            this.rbRed.TabIndex = 40;
            this.rbRed.TabStop = true;
            this.rbRed.Text = "Red:";
            this.rbRed.UseVisualStyleBackColor = true;
            // 
            // rbBrightness
            // 
            this.rbBrightness.AutoSize = true;
            this.rbBrightness.Location = new System.Drawing.Point(304, 80);
            this.rbBrightness.Name = "rbBrightness";
            this.rbBrightness.Size = new System.Drawing.Size(77, 17);
            this.rbBrightness.TabIndex = 39;
            this.rbBrightness.TabStop = true;
            this.rbBrightness.Text = "Brightness:";
            this.rbBrightness.UseVisualStyleBackColor = true;
            // 
            // rbSaturation
            // 
            this.rbSaturation.AutoSize = true;
            this.rbSaturation.Location = new System.Drawing.Point(304, 48);
            this.rbSaturation.Name = "rbSaturation";
            this.rbSaturation.Size = new System.Drawing.Size(76, 17);
            this.rbSaturation.TabIndex = 38;
            this.rbSaturation.TabStop = true;
            this.rbSaturation.Text = "Saturation:";
            this.rbSaturation.UseVisualStyleBackColor = true;
            // 
            // rbHue
            // 
            this.rbHue.AutoSize = true;
            this.rbHue.Location = new System.Drawing.Point(304, 16);
            this.rbHue.Name = "rbHue";
            this.rbHue.Size = new System.Drawing.Size(48, 17);
            this.rbHue.TabIndex = 37;
            this.rbHue.TabStop = true;
            this.rbHue.Text = "Hue:";
            this.rbHue.UseVisualStyleBackColor = true;
            // 
            // lblSecondaryColor
            // 
            this.lblSecondaryColor.BackColor = System.Drawing.Color.RoyalBlue;
            this.lblSecondaryColor.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblSecondaryColor.Location = new System.Drawing.Point(344, 232);
            this.lblSecondaryColor.Name = "lblSecondaryColor";
            this.lblSecondaryColor.Size = new System.Drawing.Size(64, 24);
            this.lblSecondaryColor.TabIndex = 36;
            // 
            // lblPrimaryColor
            // 
            this.lblPrimaryColor.BackColor = System.Drawing.Color.Red;
            this.lblPrimaryColor.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblPrimaryColor.Location = new System.Drawing.Point(344, 208);
            this.lblPrimaryColor.Name = "lblPrimaryColor";
            this.lblPrimaryColor.Size = new System.Drawing.Size(64, 24);
            this.lblPrimaryColor.TabIndex = 35;
            // 
            // colorPicker
            // 
            this.colorPicker.DrawStyle = ZSS.Colors.DrawStyle.Hue;
            this.colorPicker.Location = new System.Drawing.Point(8, 8);
            this.colorPicker.Name = "colorPicker";
            this.colorPicker.Size = new System.Drawing.Size(285, 255);
            this.colorPicker.TabIndex = 66;
            // 
            // ColorDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(658, 272);
            this.Controls.Add(this.colorPicker);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.lblOld);
            this.Controls.Add(this.lblNew);
            this.Controls.Add(this.txtHex);
            this.Controls.Add(this.lblHex);
            this.Controls.Add(this.nudKey);
            this.Controls.Add(this.nudYellow);
            this.Controls.Add(this.nudMagenta);
            this.Controls.Add(this.nudCyan);
            this.Controls.Add(this.lblKey);
            this.Controls.Add(this.lblYellow);
            this.Controls.Add(this.lblMagenta);
            this.Controls.Add(this.lblCyan);
            this.Controls.Add(this.lblHue);
            this.Controls.Add(this.lblBrightness);
            this.Controls.Add(this.lblSaturation);
            this.Controls.Add(this.nudBlue);
            this.Controls.Add(this.nudGreen);
            this.Controls.Add(this.nudRed);
            this.Controls.Add(this.nudBrightness);
            this.Controls.Add(this.nudSaturation);
            this.Controls.Add(this.nudHue);
            this.Controls.Add(this.rbBlue);
            this.Controls.Add(this.rbGreen);
            this.Controls.Add(this.rbRed);
            this.Controls.Add(this.rbBrightness);
            this.Controls.Add(this.rbSaturation);
            this.Controls.Add(this.rbHue);
            this.Controls.Add(this.lblSecondaryColor);
            this.Controls.Add(this.lblPrimaryColor);
            this.Name = "ColorDialog";
            this.Text = "ColorDialog";
            ((System.ComponentModel.ISupportInitialize)(this.nudKey)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudYellow)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMagenta)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudCyan)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudBlue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudGreen)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudRed)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudBrightness)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudSaturation)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudHue)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Label lblOld;
        private System.Windows.Forms.Label lblNew;
        private System.Windows.Forms.TextBox txtHex;
        private System.Windows.Forms.Label lblHex;
        private System.Windows.Forms.NumericUpDown nudKey;
        private System.Windows.Forms.NumericUpDown nudYellow;
        private System.Windows.Forms.NumericUpDown nudMagenta;
        private System.Windows.Forms.NumericUpDown nudCyan;
        private System.Windows.Forms.Label lblKey;
        private System.Windows.Forms.Label lblYellow;
        private System.Windows.Forms.Label lblMagenta;
        private System.Windows.Forms.Label lblCyan;
        private System.Windows.Forms.Label lblHue;
        private System.Windows.Forms.Label lblBrightness;
        private System.Windows.Forms.Label lblSaturation;
        private System.Windows.Forms.NumericUpDown nudBlue;
        private System.Windows.Forms.NumericUpDown nudGreen;
        private System.Windows.Forms.NumericUpDown nudRed;
        private System.Windows.Forms.NumericUpDown nudBrightness;
        private System.Windows.Forms.NumericUpDown nudSaturation;
        private System.Windows.Forms.NumericUpDown nudHue;
        private System.Windows.Forms.RadioButton rbBlue;
        private System.Windows.Forms.RadioButton rbGreen;
        private System.Windows.Forms.RadioButton rbRed;
        private System.Windows.Forms.RadioButton rbBrightness;
        private System.Windows.Forms.RadioButton rbSaturation;
        private System.Windows.Forms.RadioButton rbHue;
        private System.Windows.Forms.Label lblSecondaryColor;
        private System.Windows.Forms.Label lblPrimaryColor;
        private ColorPicker colorPicker;
    }
}
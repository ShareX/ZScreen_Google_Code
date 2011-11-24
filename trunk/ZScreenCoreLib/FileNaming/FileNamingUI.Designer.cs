namespace ZScreenCoreLib
{
    partial class FileNamingUI
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
            this.chkOverwriteFiles = new System.Windows.Forms.CheckBox();
            this.lblMaxNameLength = new System.Windows.Forms.Label();
            this.nudMaxNameLength = new System.Windows.Forms.NumericUpDown();
            this.btnResetIncrement = new System.Windows.Forms.Button();
            this.gbOthersNaming = new System.Windows.Forms.GroupBox();
            this.lblEntireScreenPreview = new System.Windows.Forms.Label();
            this.txtEntireScreen = new System.Windows.Forms.TextBox();
            this.gbCodeTitle = new System.Windows.Forms.GroupBox();
            this.btnCodesI = new System.Windows.Forms.Button();
            this.btnCodesPm = new System.Windows.Forms.Button();
            this.btnCodesS = new System.Windows.Forms.Button();
            this.btnCodesMi = new System.Windows.Forms.Button();
            this.btnCodesH = new System.Windows.Forms.Button();
            this.btnCodesY = new System.Windows.Forms.Button();
            this.btnCodesD = new System.Windows.Forms.Button();
            this.btnCodesMo = new System.Windows.Forms.Button();
            this.btnCodesT = new System.Windows.Forms.Button();
            this.lblCodeI = new System.Windows.Forms.Label();
            this.lblCodeT = new System.Windows.Forms.Label();
            this.lblCodeMo = new System.Windows.Forms.Label();
            this.lblCodePm = new System.Windows.Forms.Label();
            this.lblCodeD = new System.Windows.Forms.Label();
            this.lblCodeS = new System.Windows.Forms.Label();
            this.lblCodeMi = new System.Windows.Forms.Label();
            this.lblCodeY = new System.Windows.Forms.Label();
            this.lblCodeH = new System.Windows.Forms.Label();
            this.gbActiveWindowNaming = new System.Windows.Forms.GroupBox();
            this.lblActiveWindowPreview = new System.Windows.Forms.Label();
            this.txtActiveWindow = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.nudMaxNameLength)).BeginInit();
            this.gbOthersNaming.SuspendLayout();
            this.gbCodeTitle.SuspendLayout();
            this.gbActiveWindowNaming.SuspendLayout();
            this.SuspendLayout();
            // 
            // chkOverwriteFiles
            // 
            this.chkOverwriteFiles.AutoSize = true;
            this.chkOverwriteFiles.Location = new System.Drawing.Point(248, 224);
            this.chkOverwriteFiles.Name = "chkOverwriteFiles";
            this.chkOverwriteFiles.Size = new System.Drawing.Size(95, 17);
            this.chkOverwriteFiles.TabIndex = 5;
            this.chkOverwriteFiles.Text = "Overwrite Files";
            this.chkOverwriteFiles.UseVisualStyleBackColor = true;
            this.chkOverwriteFiles.CheckedChanged += new System.EventHandler(this.chkOverwriteFiles_CheckedChanged);
            // 
            // lblMaxNameLength
            // 
            this.lblMaxNameLength.AutoSize = true;
            this.lblMaxNameLength.Location = new System.Drawing.Point(248, 192);
            this.lblMaxNameLength.Name = "lblMaxNameLength";
            this.lblMaxNameLength.Size = new System.Drawing.Size(179, 13);
            this.lblMaxNameLength.TabIndex = 3;
            this.lblMaxNameLength.Text = "Maximum name length (0 = No limit) :";
            // 
            // nudMaxNameLength
            // 
            this.nudMaxNameLength.Location = new System.Drawing.Point(432, 189);
            this.nudMaxNameLength.Name = "nudMaxNameLength";
            this.nudMaxNameLength.Size = new System.Drawing.Size(56, 20);
            this.nudMaxNameLength.TabIndex = 4;
            this.nudMaxNameLength.ValueChanged += new System.EventHandler(this.nudMaxNameLength_ValueChanged);
            // 
            // btnResetIncrement
            // 
            this.btnResetIncrement.Location = new System.Drawing.Point(8, 280);
            this.btnResetIncrement.Name = "btnResetIncrement";
            this.btnResetIncrement.Size = new System.Drawing.Size(184, 23);
            this.btnResetIncrement.TabIndex = 6;
            this.btnResetIncrement.Text = "Reset Auto-Increment Number";
            this.btnResetIncrement.UseVisualStyleBackColor = true;
            this.btnResetIncrement.Click += new System.EventHandler(this.btnResetIncrement_Click);
            // 
            // gbOthersNaming
            // 
            this.gbOthersNaming.Controls.Add(this.lblEntireScreenPreview);
            this.gbOthersNaming.Controls.Add(this.txtEntireScreen);
            this.gbOthersNaming.Location = new System.Drawing.Point(240, 96);
            this.gbOthersNaming.Name = "gbOthersNaming";
            this.gbOthersNaming.Size = new System.Drawing.Size(362, 80);
            this.gbOthersNaming.TabIndex = 2;
            this.gbOthersNaming.TabStop = false;
            this.gbOthersNaming.Text = "Other Capture Types";
            // 
            // lblEntireScreenPreview
            // 
            this.lblEntireScreenPreview.AutoSize = true;
            this.lblEntireScreenPreview.Location = new System.Drawing.Point(16, 56);
            this.lblEntireScreenPreview.Name = "lblEntireScreenPreview";
            this.lblEntireScreenPreview.Size = new System.Drawing.Size(112, 13);
            this.lblEntireScreenPreview.TabIndex = 1;
            this.lblEntireScreenPreview.Text = "Entire Screen Preview";
            // 
            // txtEntireScreen
            // 
            this.txtEntireScreen.Location = new System.Drawing.Point(16, 24);
            this.txtEntireScreen.Name = "txtEntireScreen";
            this.txtEntireScreen.Size = new System.Drawing.Size(330, 20);
            this.txtEntireScreen.TabIndex = 0;
            this.txtEntireScreen.TextChanged += new System.EventHandler(this.txtEntireScreen_TextChanged);
            this.txtEntireScreen.Leave += new System.EventHandler(this.txtEntireScreen_Leave);
            // 
            // gbCodeTitle
            // 
            this.gbCodeTitle.BackColor = System.Drawing.Color.Transparent;
            this.gbCodeTitle.Controls.Add(this.btnCodesI);
            this.gbCodeTitle.Controls.Add(this.btnCodesPm);
            this.gbCodeTitle.Controls.Add(this.btnCodesS);
            this.gbCodeTitle.Controls.Add(this.btnCodesMi);
            this.gbCodeTitle.Controls.Add(this.btnCodesH);
            this.gbCodeTitle.Controls.Add(this.btnCodesY);
            this.gbCodeTitle.Controls.Add(this.btnCodesD);
            this.gbCodeTitle.Controls.Add(this.btnCodesMo);
            this.gbCodeTitle.Controls.Add(this.btnCodesT);
            this.gbCodeTitle.Controls.Add(this.lblCodeI);
            this.gbCodeTitle.Controls.Add(this.lblCodeT);
            this.gbCodeTitle.Controls.Add(this.lblCodeMo);
            this.gbCodeTitle.Controls.Add(this.lblCodePm);
            this.gbCodeTitle.Controls.Add(this.lblCodeD);
            this.gbCodeTitle.Controls.Add(this.lblCodeS);
            this.gbCodeTitle.Controls.Add(this.lblCodeMi);
            this.gbCodeTitle.Controls.Add(this.lblCodeY);
            this.gbCodeTitle.Controls.Add(this.lblCodeH);
            this.gbCodeTitle.Location = new System.Drawing.Point(8, 8);
            this.gbCodeTitle.Name = "gbCodeTitle";
            this.gbCodeTitle.Size = new System.Drawing.Size(224, 264);
            this.gbCodeTitle.TabIndex = 0;
            this.gbCodeTitle.TabStop = false;
            this.gbCodeTitle.Text = "Environment Variables";
            // 
            // btnCodesI
            // 
            this.btnCodesI.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCodesI.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnCodesI.Location = new System.Drawing.Point(59, 201);
            this.btnCodesI.Margin = new System.Windows.Forms.Padding(2);
            this.btnCodesI.Name = "btnCodesI";
            this.btnCodesI.Size = new System.Drawing.Size(152, 22);
            this.btnCodesI.TabIndex = 15;
            this.btnCodesI.TabStop = false;
            this.btnCodesI.Text = "Auto-Increment";
            this.btnCodesI.UseVisualStyleBackColor = true;
            this.btnCodesI.Click += new System.EventHandler(this.btnCodes_Click);
            // 
            // btnCodesPm
            // 
            this.btnCodesPm.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCodesPm.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnCodesPm.Location = new System.Drawing.Point(59, 228);
            this.btnCodesPm.Margin = new System.Windows.Forms.Padding(2);
            this.btnCodesPm.Name = "btnCodesPm";
            this.btnCodesPm.Size = new System.Drawing.Size(152, 22);
            this.btnCodesPm.TabIndex = 17;
            this.btnCodesPm.TabStop = false;
            this.btnCodesPm.Text = "Gets AM/PM";
            this.btnCodesPm.UseVisualStyleBackColor = true;
            this.btnCodesPm.Click += new System.EventHandler(this.btnCodes_Click);
            // 
            // btnCodesS
            // 
            this.btnCodesS.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCodesS.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnCodesS.Location = new System.Drawing.Point(59, 174);
            this.btnCodesS.Margin = new System.Windows.Forms.Padding(2);
            this.btnCodesS.Name = "btnCodesS";
            this.btnCodesS.Size = new System.Drawing.Size(152, 22);
            this.btnCodesS.TabIndex = 13;
            this.btnCodesS.TabStop = false;
            this.btnCodesS.Text = "Gets the current second";
            this.btnCodesS.UseVisualStyleBackColor = true;
            this.btnCodesS.Click += new System.EventHandler(this.btnCodes_Click);
            // 
            // btnCodesMi
            // 
            this.btnCodesMi.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCodesMi.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnCodesMi.Location = new System.Drawing.Point(59, 148);
            this.btnCodesMi.Margin = new System.Windows.Forms.Padding(2);
            this.btnCodesMi.Name = "btnCodesMi";
            this.btnCodesMi.Size = new System.Drawing.Size(152, 22);
            this.btnCodesMi.TabIndex = 11;
            this.btnCodesMi.TabStop = false;
            this.btnCodesMi.Text = "Gets the current minute";
            this.btnCodesMi.UseVisualStyleBackColor = true;
            this.btnCodesMi.Click += new System.EventHandler(this.btnCodes_Click);
            // 
            // btnCodesH
            // 
            this.btnCodesH.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCodesH.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnCodesH.Location = new System.Drawing.Point(59, 122);
            this.btnCodesH.Margin = new System.Windows.Forms.Padding(2);
            this.btnCodesH.Name = "btnCodesH";
            this.btnCodesH.Size = new System.Drawing.Size(152, 22);
            this.btnCodesH.TabIndex = 9;
            this.btnCodesH.TabStop = false;
            this.btnCodesH.Text = "Gets the current hour";
            this.btnCodesH.UseVisualStyleBackColor = true;
            this.btnCodesH.Click += new System.EventHandler(this.btnCodes_Click);
            // 
            // btnCodesY
            // 
            this.btnCodesY.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCodesY.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnCodesY.Location = new System.Drawing.Point(59, 96);
            this.btnCodesY.Margin = new System.Windows.Forms.Padding(2);
            this.btnCodesY.Name = "btnCodesY";
            this.btnCodesY.Size = new System.Drawing.Size(152, 22);
            this.btnCodesY.TabIndex = 7;
            this.btnCodesY.TabStop = false;
            this.btnCodesY.Text = "Gets the current year";
            this.btnCodesY.UseVisualStyleBackColor = true;
            this.btnCodesY.Click += new System.EventHandler(this.btnCodes_Click);
            // 
            // btnCodesD
            // 
            this.btnCodesD.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCodesD.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnCodesD.Location = new System.Drawing.Point(59, 70);
            this.btnCodesD.Margin = new System.Windows.Forms.Padding(2);
            this.btnCodesD.Name = "btnCodesD";
            this.btnCodesD.Size = new System.Drawing.Size(152, 22);
            this.btnCodesD.TabIndex = 5;
            this.btnCodesD.TabStop = false;
            this.btnCodesD.Text = "Gets the current day";
            this.btnCodesD.UseVisualStyleBackColor = true;
            this.btnCodesD.Click += new System.EventHandler(this.btnCodes_Click);
            // 
            // btnCodesMo
            // 
            this.btnCodesMo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCodesMo.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnCodesMo.Location = new System.Drawing.Point(59, 44);
            this.btnCodesMo.Margin = new System.Windows.Forms.Padding(2);
            this.btnCodesMo.Name = "btnCodesMo";
            this.btnCodesMo.Size = new System.Drawing.Size(152, 22);
            this.btnCodesMo.TabIndex = 3;
            this.btnCodesMo.TabStop = false;
            this.btnCodesMo.Text = "Gets the current month";
            this.btnCodesMo.UseVisualStyleBackColor = true;
            this.btnCodesMo.Click += new System.EventHandler(this.btnCodes_Click);
            // 
            // btnCodesT
            // 
            this.btnCodesT.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCodesT.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnCodesT.Location = new System.Drawing.Point(59, 19);
            this.btnCodesT.Margin = new System.Windows.Forms.Padding(2);
            this.btnCodesT.Name = "btnCodesT";
            this.btnCodesT.Size = new System.Drawing.Size(152, 22);
            this.btnCodesT.TabIndex = 1;
            this.btnCodesT.TabStop = false;
            this.btnCodesT.Text = "Title of Active Window";
            this.btnCodesT.UseVisualStyleBackColor = true;
            this.btnCodesT.Click += new System.EventHandler(this.btnCodes_Click);
            // 
            // lblCodeI
            // 
            this.lblCodeI.AutoSize = true;
            this.lblCodeI.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblCodeI.Location = new System.Drawing.Point(20, 205);
            this.lblCodeI.Name = "lblCodeI";
            this.lblCodeI.Size = new System.Drawing.Size(17, 13);
            this.lblCodeI.TabIndex = 14;
            this.lblCodeI.Text = "%i";
            // 
            // lblCodeT
            // 
            this.lblCodeT.AutoSize = true;
            this.lblCodeT.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblCodeT.Location = new System.Drawing.Point(20, 22);
            this.lblCodeT.Name = "lblCodeT";
            this.lblCodeT.Size = new System.Drawing.Size(18, 13);
            this.lblCodeT.TabIndex = 0;
            this.lblCodeT.Text = "%t";
            // 
            // lblCodeMo
            // 
            this.lblCodeMo.AutoSize = true;
            this.lblCodeMo.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblCodeMo.Location = new System.Drawing.Point(20, 48);
            this.lblCodeMo.Name = "lblCodeMo";
            this.lblCodeMo.Size = new System.Drawing.Size(29, 13);
            this.lblCodeMo.TabIndex = 2;
            this.lblCodeMo.Text = "%mo";
            // 
            // lblCodePm
            // 
            this.lblCodePm.AutoSize = true;
            this.lblCodePm.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblCodePm.Location = new System.Drawing.Point(20, 231);
            this.lblCodePm.Name = "lblCodePm";
            this.lblCodePm.Size = new System.Drawing.Size(29, 13);
            this.lblCodePm.TabIndex = 16;
            this.lblCodePm.Text = "%pm";
            // 
            // lblCodeD
            // 
            this.lblCodeD.AutoSize = true;
            this.lblCodeD.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblCodeD.Location = new System.Drawing.Point(20, 73);
            this.lblCodeD.Name = "lblCodeD";
            this.lblCodeD.Size = new System.Drawing.Size(21, 13);
            this.lblCodeD.TabIndex = 4;
            this.lblCodeD.Text = "%d";
            // 
            // lblCodeS
            // 
            this.lblCodeS.AutoSize = true;
            this.lblCodeS.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblCodeS.Location = new System.Drawing.Point(20, 177);
            this.lblCodeS.Name = "lblCodeS";
            this.lblCodeS.Size = new System.Drawing.Size(20, 13);
            this.lblCodeS.TabIndex = 12;
            this.lblCodeS.Text = "%s";
            // 
            // lblCodeMi
            // 
            this.lblCodeMi.AutoSize = true;
            this.lblCodeMi.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblCodeMi.Location = new System.Drawing.Point(20, 152);
            this.lblCodeMi.Name = "lblCodeMi";
            this.lblCodeMi.Size = new System.Drawing.Size(25, 13);
            this.lblCodeMi.TabIndex = 10;
            this.lblCodeMi.Text = "%mi";
            // 
            // lblCodeY
            // 
            this.lblCodeY.AutoSize = true;
            this.lblCodeY.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblCodeY.Location = new System.Drawing.Point(20, 99);
            this.lblCodeY.Name = "lblCodeY";
            this.lblCodeY.Size = new System.Drawing.Size(20, 13);
            this.lblCodeY.TabIndex = 6;
            this.lblCodeY.Text = "%y";
            // 
            // lblCodeH
            // 
            this.lblCodeH.AutoSize = true;
            this.lblCodeH.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblCodeH.Location = new System.Drawing.Point(20, 126);
            this.lblCodeH.Name = "lblCodeH";
            this.lblCodeH.Size = new System.Drawing.Size(21, 13);
            this.lblCodeH.TabIndex = 8;
            this.lblCodeH.Text = "%h";
            // 
            // gbActiveWindowNaming
            // 
            this.gbActiveWindowNaming.BackColor = System.Drawing.Color.Transparent;
            this.gbActiveWindowNaming.Controls.Add(this.lblActiveWindowPreview);
            this.gbActiveWindowNaming.Controls.Add(this.txtActiveWindow);
            this.gbActiveWindowNaming.Location = new System.Drawing.Point(240, 8);
            this.gbActiveWindowNaming.Name = "gbActiveWindowNaming";
            this.gbActiveWindowNaming.Size = new System.Drawing.Size(362, 80);
            this.gbActiveWindowNaming.TabIndex = 1;
            this.gbActiveWindowNaming.TabStop = false;
            this.gbActiveWindowNaming.Text = "Active Window";
            // 
            // lblActiveWindowPreview
            // 
            this.lblActiveWindowPreview.AutoSize = true;
            this.lblActiveWindowPreview.Location = new System.Drawing.Point(16, 56);
            this.lblActiveWindowPreview.Name = "lblActiveWindowPreview";
            this.lblActiveWindowPreview.Size = new System.Drawing.Size(120, 13);
            this.lblActiveWindowPreview.TabIndex = 1;
            this.lblActiveWindowPreview.Text = "Active Window Preview";
            // 
            // txtActiveWindow
            // 
            this.txtActiveWindow.Location = new System.Drawing.Point(16, 24);
            this.txtActiveWindow.Name = "txtActiveWindow";
            this.txtActiveWindow.Size = new System.Drawing.Size(330, 20);
            this.txtActiveWindow.TabIndex = 0;
            this.txtActiveWindow.TextChanged += new System.EventHandler(this.txtActiveWindow_TextChanged);
            this.txtActiveWindow.Leave += new System.EventHandler(this.txtActiveWindow_Leave);
            // 
            // FileNamingUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(624, 314);
            this.Controls.Add(this.chkOverwriteFiles);
            this.Controls.Add(this.lblMaxNameLength);
            this.Controls.Add(this.nudMaxNameLength);
            this.Controls.Add(this.btnResetIncrement);
            this.Controls.Add(this.gbOthersNaming);
            this.Controls.Add(this.gbCodeTitle);
            this.Controls.Add(this.gbActiveWindowNaming);
            this.MinimumSize = new System.Drawing.Size(640, 352);
            this.Name = "FileNamingUI";
            this.Text = "FileNamingUI";
            this.Load += new System.EventHandler(this.FileNamingUI_Load);
            ((System.ComponentModel.ISupportInitialize)(this.nudMaxNameLength)).EndInit();
            this.gbOthersNaming.ResumeLayout(false);
            this.gbOthersNaming.PerformLayout();
            this.gbCodeTitle.ResumeLayout(false);
            this.gbCodeTitle.PerformLayout();
            this.gbActiveWindowNaming.ResumeLayout(false);
            this.gbActiveWindowNaming.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox chkOverwriteFiles;
        private System.Windows.Forms.Label lblMaxNameLength;
        private System.Windows.Forms.NumericUpDown nudMaxNameLength;
        internal System.Windows.Forms.Button btnResetIncrement;
        internal System.Windows.Forms.GroupBox gbOthersNaming;
        internal System.Windows.Forms.Label lblEntireScreenPreview;
        internal System.Windows.Forms.TextBox txtEntireScreen;
        internal System.Windows.Forms.GroupBox gbCodeTitle;
        internal System.Windows.Forms.Button btnCodesI;
        internal System.Windows.Forms.Button btnCodesPm;
        internal System.Windows.Forms.Button btnCodesS;
        internal System.Windows.Forms.Button btnCodesMi;
        internal System.Windows.Forms.Button btnCodesH;
        internal System.Windows.Forms.Button btnCodesY;
        internal System.Windows.Forms.Button btnCodesD;
        internal System.Windows.Forms.Button btnCodesMo;
        internal System.Windows.Forms.Button btnCodesT;
        internal System.Windows.Forms.Label lblCodeI;
        internal System.Windows.Forms.Label lblCodeT;
        internal System.Windows.Forms.Label lblCodeMo;
        internal System.Windows.Forms.Label lblCodePm;
        internal System.Windows.Forms.Label lblCodeD;
        internal System.Windows.Forms.Label lblCodeS;
        internal System.Windows.Forms.Label lblCodeMi;
        internal System.Windows.Forms.Label lblCodeY;
        internal System.Windows.Forms.Label lblCodeH;
        internal System.Windows.Forms.GroupBox gbActiveWindowNaming;
        internal System.Windows.Forms.Label lblActiveWindowPreview;
        internal System.Windows.Forms.TextBox txtActiveWindow;
    }
}
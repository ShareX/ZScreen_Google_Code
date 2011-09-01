namespace JBirdGUI
{
    partial class JBirdMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(JBirdMain));
            this.btnCaptureCropShot = new System.Windows.Forms.Button();
            this.btnCaptureSelectedWindow = new System.Windows.Forms.Button();
            this.btnCaptureScreen = new System.Windows.Forms.Button();
            this.btnUploadClipboard = new System.Windows.Forms.Button();
            this.btnUploadFiles = new System.Windows.Forms.Button();
            this.btnCaptureShape = new System.Windows.Forms.Button();
            this.btnCaptureActiveWindow = new System.Windows.Forms.Button();
            this.btnCropShotLast = new System.Windows.Forms.Button();
            this.btnGoogleTranslateOpen = new System.Windows.Forms.Button();
            this.btnScreenColorPicker = new System.Windows.Forms.Button();
            this.btnAutoCapture = new System.Windows.Forms.Button();
            this.btnWorkflows = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnCaptureCropShot
            // 
            this.btnCaptureCropShot.Image = ((System.Drawing.Image)(resources.GetObject("btnCaptureCropShot.Image")));
            this.btnCaptureCropShot.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnCaptureCropShot.Location = new System.Drawing.Point(112, 16);
            this.btnCaptureCropShot.Name = "btnCaptureCropShot";
            this.btnCaptureCropShot.Size = new System.Drawing.Size(80, 80);
            this.btnCaptureCropShot.TabIndex = 1;
            this.btnCaptureCropShot.Text = "Capture &Cropped Area";
            this.btnCaptureCropShot.UseVisualStyleBackColor = true;
            // 
            // btnCaptureSelectedWindow
            // 
            this.btnCaptureSelectedWindow.Image = ((System.Drawing.Image)(resources.GetObject("btnCaptureSelectedWindow.Image")));
            this.btnCaptureSelectedWindow.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnCaptureSelectedWindow.Location = new System.Drawing.Point(16, 208);
            this.btnCaptureSelectedWindow.Name = "btnCaptureSelectedWindow";
            this.btnCaptureSelectedWindow.Size = new System.Drawing.Size(80, 80);
            this.btnCaptureSelectedWindow.TabIndex = 2;
            this.btnCaptureSelectedWindow.Text = "Capture &Window";
            this.btnCaptureSelectedWindow.UseVisualStyleBackColor = true;
            // 
            // btnCaptureScreen
            // 
            this.btnCaptureScreen.Image = ((System.Drawing.Image)(resources.GetObject("btnCaptureScreen.Image")));
            this.btnCaptureScreen.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnCaptureScreen.Location = new System.Drawing.Point(16, 16);
            this.btnCaptureScreen.Name = "btnCaptureScreen";
            this.btnCaptureScreen.Size = new System.Drawing.Size(80, 80);
            this.btnCaptureScreen.TabIndex = 3;
            this.btnCaptureScreen.Text = "Capture &Entire Screen";
            this.btnCaptureScreen.UseVisualStyleBackColor = true;
            this.btnCaptureScreen.Click += new System.EventHandler(this.btnCaptureScreen_Click);
            // 
            // btnUploadClipboard
            // 
            this.btnUploadClipboard.Image = ((System.Drawing.Image)(resources.GetObject("btnUploadClipboard.Image")));
            this.btnUploadClipboard.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnUploadClipboard.Location = new System.Drawing.Point(208, 16);
            this.btnUploadClipboard.Name = "btnUploadClipboard";
            this.btnUploadClipboard.Size = new System.Drawing.Size(80, 80);
            this.btnUploadClipboard.TabIndex = 5;
            this.btnUploadClipboard.Text = "Upload &Clipboard Content";
            this.btnUploadClipboard.UseVisualStyleBackColor = true;
            // 
            // btnUploadFiles
            // 
            this.btnUploadFiles.Image = ((System.Drawing.Image)(resources.GetObject("btnUploadFiles.Image")));
            this.btnUploadFiles.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnUploadFiles.Location = new System.Drawing.Point(208, 112);
            this.btnUploadFiles.Name = "btnUploadFiles";
            this.btnUploadFiles.Size = new System.Drawing.Size(80, 80);
            this.btnUploadFiles.TabIndex = 6;
            this.btnUploadFiles.Text = "Upload &Files";
            this.btnUploadFiles.UseVisualStyleBackColor = true;
            // 
            // btnCaptureShape
            // 
            this.btnCaptureShape.Image = ((System.Drawing.Image)(resources.GetObject("btnCaptureShape.Image")));
            this.btnCaptureShape.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnCaptureShape.Location = new System.Drawing.Point(112, 208);
            this.btnCaptureShape.Name = "btnCaptureShape";
            this.btnCaptureShape.Size = new System.Drawing.Size(80, 80);
            this.btnCaptureShape.TabIndex = 8;
            this.btnCaptureShape.Text = "Capture &Shape";
            this.btnCaptureShape.UseVisualStyleBackColor = true;
            // 
            // btnCaptureActiveWindow
            // 
            this.btnCaptureActiveWindow.Image = ((System.Drawing.Image)(resources.GetObject("btnCaptureActiveWindow.Image")));
            this.btnCaptureActiveWindow.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnCaptureActiveWindow.Location = new System.Drawing.Point(16, 112);
            this.btnCaptureActiveWindow.Name = "btnCaptureActiveWindow";
            this.btnCaptureActiveWindow.Size = new System.Drawing.Size(80, 80);
            this.btnCaptureActiveWindow.TabIndex = 9;
            this.btnCaptureActiveWindow.Text = "Capture &Active Window";
            this.btnCaptureActiveWindow.UseVisualStyleBackColor = true;
            this.btnCaptureActiveWindow.Click += new System.EventHandler(this.btnCaptureActiveWindow_Click);
            // 
            // btnCropShotLast
            // 
            this.btnCropShotLast.Image = ((System.Drawing.Image)(resources.GetObject("btnCropShotLast.Image")));
            this.btnCropShotLast.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnCropShotLast.Location = new System.Drawing.Point(112, 112);
            this.btnCropShotLast.Name = "btnCropShotLast";
            this.btnCropShotLast.Size = new System.Drawing.Size(80, 80);
            this.btnCropShotLast.TabIndex = 10;
            this.btnCropShotLast.Text = "Capture &Last Cropped Area";
            this.btnCropShotLast.UseVisualStyleBackColor = true;
            // 
            // btnGoogleTranslateOpen
            // 
            this.btnGoogleTranslateOpen.Image = ((System.Drawing.Image)(resources.GetObject("btnGoogleTranslateOpen.Image")));
            this.btnGoogleTranslateOpen.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnGoogleTranslateOpen.Location = new System.Drawing.Point(304, 112);
            this.btnGoogleTranslateOpen.Name = "btnGoogleTranslateOpen";
            this.btnGoogleTranslateOpen.Size = new System.Drawing.Size(80, 80);
            this.btnGoogleTranslateOpen.TabIndex = 11;
            this.btnGoogleTranslateOpen.Text = "Google &Translator";
            this.btnGoogleTranslateOpen.UseVisualStyleBackColor = true;
            // 
            // btnScreenColorPicker
            // 
            this.btnScreenColorPicker.Image = ((System.Drawing.Image)(resources.GetObject("btnScreenColorPicker.Image")));
            this.btnScreenColorPicker.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnScreenColorPicker.Location = new System.Drawing.Point(304, 16);
            this.btnScreenColorPicker.Name = "btnScreenColorPicker";
            this.btnScreenColorPicker.Size = new System.Drawing.Size(80, 80);
            this.btnScreenColorPicker.TabIndex = 12;
            this.btnScreenColorPicker.Text = "Screen Color Picker";
            this.btnScreenColorPicker.UseVisualStyleBackColor = true;
            // 
            // btnAutoCapture
            // 
            this.btnAutoCapture.Image = ((System.Drawing.Image)(resources.GetObject("btnAutoCapture.Image")));
            this.btnAutoCapture.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnAutoCapture.Location = new System.Drawing.Point(208, 208);
            this.btnAutoCapture.Name = "btnAutoCapture";
            this.btnAutoCapture.Size = new System.Drawing.Size(80, 80);
            this.btnAutoCapture.TabIndex = 13;
            this.btnAutoCapture.Text = "Auto Capture";
            this.btnAutoCapture.UseVisualStyleBackColor = true;
            // 
            // btnWorkflows
            // 
            this.btnWorkflows.Image = global::JBirdGUI.Properties.Resources.wrench;
            this.btnWorkflows.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnWorkflows.Location = new System.Drawing.Point(304, 208);
            this.btnWorkflows.Name = "btnWorkflows";
            this.btnWorkflows.Size = new System.Drawing.Size(80, 80);
            this.btnWorkflows.TabIndex = 14;
            this.btnWorkflows.Text = "Workflows";
            this.btnWorkflows.UseVisualStyleBackColor = true;
            this.btnWorkflows.Click += new System.EventHandler(this.btnWorkflows_Click);
            // 
            // JBirdMain
            // 
            this.AcceptButton = this.btnWorkflows;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(402, 312);
            this.Controls.Add(this.btnWorkflows);
            this.Controls.Add(this.btnScreenColorPicker);
            this.Controls.Add(this.btnGoogleTranslateOpen);
            this.Controls.Add(this.btnAutoCapture);
            this.Controls.Add(this.btnCropShotLast);
            this.Controls.Add(this.btnCaptureActiveWindow);
            this.Controls.Add(this.btnCaptureShape);
            this.Controls.Add(this.btnCaptureScreen);
            this.Controls.Add(this.btnUploadFiles);
            this.Controls.Add(this.btnUploadClipboard);
            this.Controls.Add(this.btnCaptureSelectedWindow);
            this.Controls.Add(this.btnCaptureCropShot);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(408, 344);
            this.MinimumSize = new System.Drawing.Size(408, 344);
            this.Name = "JBirdMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "JBird";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.JBirdMain_FormClosing);
            this.Load += new System.EventHandler(this.JBirdMain_Load);
            this.Shown += new System.EventHandler(this.JBirdMain_Shown);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnCaptureCropShot;
        private System.Windows.Forms.Button btnCaptureSelectedWindow;
        private System.Windows.Forms.Button btnCaptureScreen;
        private System.Windows.Forms.Button btnUploadClipboard;
        private System.Windows.Forms.Button btnUploadFiles;
        private System.Windows.Forms.Button btnCaptureShape;
        private System.Windows.Forms.Button btnCaptureActiveWindow;
        private System.Windows.Forms.Button btnCropShotLast;
        private System.Windows.Forms.Button btnGoogleTranslateOpen;
        private System.Windows.Forms.Button btnScreenColorPicker;
        private System.Windows.Forms.Button btnAutoCapture;
        private System.Windows.Forms.Button btnWorkflows;
    }
}


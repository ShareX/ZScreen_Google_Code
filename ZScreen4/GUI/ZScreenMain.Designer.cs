namespace ZScreen4
{
    partial class ZScreenMain
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ZScreenMain));
            this.niApp = new System.Windows.Forms.NotifyIcon(this.components);
            this.cmsApp = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiDestImages = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiDestFiles = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiDestText = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiExit = new System.Windows.Forms.ToolStripMenuItem();
            this.btnCaptureCropShot = new System.Windows.Forms.Button();
            this.btnCaptureSelectedWindow = new System.Windows.Forms.Button();
            this.btnCaptureScreen = new System.Windows.Forms.Button();
            this.btnHotkeys = new System.Windows.Forms.Button();
            this.btnUploadClipboard = new System.Windows.Forms.Button();
            this.btnUploadFiles = new System.Windows.Forms.Button();
            this.btnDestinations = new System.Windows.Forms.Button();
            this.btnCropShotFreehand = new System.Windows.Forms.Button();
            this.btnCaptureActiveWindow = new System.Windows.Forms.Button();
            this.btnCropShotLast = new System.Windows.Forms.Button();
            this.btnGoogleTranslateOpen = new System.Windows.Forms.Button();
            this.btnScreenColorPicker = new System.Windows.Forms.Button();
            this.btnAutoCapture = new System.Windows.Forms.Button();
            this.cmsApp.SuspendLayout();
            this.SuspendLayout();
            // 
            // niApp
            // 
            this.niApp.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.niApp.ContextMenuStrip = this.cmsApp;
            this.niApp.Icon = ((System.Drawing.Icon)(resources.GetObject("niApp.Icon")));
            this.niApp.Text = "ZScreen";
            this.niApp.Visible = true;
            // 
            // cmsApp
            // 
            this.cmsApp.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiDestImages,
            this.tsmiDestFiles,
            this.tsmiDestText,
            this.toolStripSeparator1,
            this.tsmiExit});
            this.cmsApp.Name = "cmsApp";
            this.cmsApp.Size = new System.Drawing.Size(154, 98);
            // 
            // tsmiDestImages
            // 
            this.tsmiDestImages.Name = "tsmiDestImages";
            this.tsmiDestImages.Size = new System.Drawing.Size(153, 22);
            this.tsmiDestImages.Text = "Send image to";
            // 
            // tsmiDestFiles
            // 
            this.tsmiDestFiles.Name = "tsmiDestFiles";
            this.tsmiDestFiles.Size = new System.Drawing.Size(153, 22);
            this.tsmiDestFiles.Text = "Send file to";
            // 
            // tsmiDestText
            // 
            this.tsmiDestText.Name = "tsmiDestText";
            this.tsmiDestText.Size = new System.Drawing.Size(153, 22);
            this.tsmiDestText.Text = "Send text to";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(150, 6);
            // 
            // tsmiExit
            // 
            this.tsmiExit.Name = "tsmiExit";
            this.tsmiExit.Size = new System.Drawing.Size(153, 22);
            this.tsmiExit.Text = "E&xit";
            this.tsmiExit.Click += new System.EventHandler(this.tsmiExit_Click);
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
            this.btnCaptureSelectedWindow.Text = "Capture &Selected Window";
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
            // btnHotkeys
            // 
            this.btnHotkeys.Location = new System.Drawing.Point(16, 408);
            this.btnHotkeys.Name = "btnHotkeys";
            this.btnHotkeys.Size = new System.Drawing.Size(80, 24);
            this.btnHotkeys.TabIndex = 4;
            this.btnHotkeys.Text = "Hotkeys...";
            this.btnHotkeys.UseVisualStyleBackColor = true;
            // 
            // btnUploadClipboard
            // 
            this.btnUploadClipboard.Image = ((System.Drawing.Image)(resources.GetObject("btnUploadClipboard.Image")));
            this.btnUploadClipboard.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnUploadClipboard.Location = new System.Drawing.Point(208, 112);
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
            this.btnUploadFiles.Location = new System.Drawing.Point(208, 208);
            this.btnUploadFiles.Name = "btnUploadFiles";
            this.btnUploadFiles.Size = new System.Drawing.Size(80, 80);
            this.btnUploadFiles.TabIndex = 6;
            this.btnUploadFiles.Text = "Upload &Files";
            this.btnUploadFiles.UseVisualStyleBackColor = true;
            // 
            // btnDestinations
            // 
            this.btnDestinations.Location = new System.Drawing.Point(112, 408);
            this.btnDestinations.Name = "btnDestinations";
            this.btnDestinations.Size = new System.Drawing.Size(80, 24);
            this.btnDestinations.TabIndex = 7;
            this.btnDestinations.Text = "Destinations..";
            this.btnDestinations.UseVisualStyleBackColor = true;
            this.btnDestinations.Click += new System.EventHandler(this.btnDestinations_Click);
            // 
            // btnCropShotFreehand
            // 
            this.btnCropShotFreehand.Image = ((System.Drawing.Image)(resources.GetObject("btnCropShotFreehand.Image")));
            this.btnCropShotFreehand.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnCropShotFreehand.Location = new System.Drawing.Point(112, 208);
            this.btnCropShotFreehand.Name = "btnCropShotFreehand";
            this.btnCropShotFreehand.Size = new System.Drawing.Size(80, 80);
            this.btnCropShotFreehand.TabIndex = 8;
            this.btnCropShotFreehand.Text = "Capture &Freehand";
            this.btnCropShotFreehand.UseVisualStyleBackColor = true;
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
            this.btnAutoCapture.Location = new System.Drawing.Point(208, 16);
            this.btnAutoCapture.Name = "btnAutoCapture";
            this.btnAutoCapture.Size = new System.Drawing.Size(80, 80);
            this.btnAutoCapture.TabIndex = 13;
            this.btnAutoCapture.Text = "Auto Capture";
            this.btnAutoCapture.UseVisualStyleBackColor = true;
            // 
            // ZScreenMain
            // 
            this.AcceptButton = this.btnCaptureScreen;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(400, 446);
            this.Controls.Add(this.btnAutoCapture);
            this.Controls.Add(this.btnScreenColorPicker);
            this.Controls.Add(this.btnGoogleTranslateOpen);
            this.Controls.Add(this.btnCropShotLast);
            this.Controls.Add(this.btnCaptureActiveWindow);
            this.Controls.Add(this.btnCropShotFreehand);
            this.Controls.Add(this.btnDestinations);
            this.Controls.Add(this.btnUploadFiles);
            this.Controls.Add(this.btnUploadClipboard);
            this.Controls.Add(this.btnHotkeys);
            this.Controls.Add(this.btnCaptureScreen);
            this.Controls.Add(this.btnCaptureSelectedWindow);
            this.Controls.Add(this.btnCaptureCropShot);
            this.Name = "ZScreenMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ZScreen";
            this.Load += new System.EventHandler(this.ZScreenMain_Load);
            this.cmsApp.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.NotifyIcon niApp;
        private System.Windows.Forms.ContextMenuStrip cmsApp;
        private System.Windows.Forms.ToolStripMenuItem tsmiDestImages;
        private System.Windows.Forms.ToolStripMenuItem tsmiDestFiles;
        private System.Windows.Forms.ToolStripMenuItem tsmiDestText;
        private System.Windows.Forms.ToolStripMenuItem tsmiExit;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.Button btnCaptureCropShot;
        private System.Windows.Forms.Button btnCaptureSelectedWindow;
        private System.Windows.Forms.Button btnCaptureScreen;
        private System.Windows.Forms.Button btnHotkeys;
        private System.Windows.Forms.Button btnUploadClipboard;
        private System.Windows.Forms.Button btnUploadFiles;
        private System.Windows.Forms.Button btnDestinations;
        private System.Windows.Forms.Button btnCropShotFreehand;
        private System.Windows.Forms.Button btnCaptureActiveWindow;
        private System.Windows.Forms.Button btnCropShotLast;
        private System.Windows.Forms.Button btnGoogleTranslateOpen;
        private System.Windows.Forms.Button btnScreenColorPicker;
        private System.Windows.Forms.Button btnAutoCapture;
    }
}


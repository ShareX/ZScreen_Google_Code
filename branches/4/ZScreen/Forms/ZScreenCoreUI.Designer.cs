using HelpersLib;
namespace ZScreenGUI
{
    partial class ZScreenCoreUI : HotkeyForm
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
            this.tsCoreMainTab = new System.Windows.Forms.ToolStrip();
            this.tsbFullscreenCapture = new System.Windows.Forms.ToolStripButton();
            this.tsbActiveWindow = new System.Windows.Forms.ToolStripButton();
            this.tsbSelectedWindow = new System.Windows.Forms.ToolStripButton();
            this.tsddbCoreSelectedWindow = new System.Windows.Forms.ToolStripDropDownButton();
            this.tsbCropShot = new System.Windows.Forms.ToolStripButton();
            this.tsbLastCropShot = new System.Windows.Forms.ToolStripButton();
            this.tsbFreehandCropShot = new System.Windows.Forms.ToolStripButton();
            this.tsbAutoCapture = new System.Windows.Forms.ToolStripButton();
            this.tssMaintoolbar1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbFileUpload = new System.Windows.Forms.ToolStripButton();
            this.tsbClipboardUpload = new System.Windows.Forms.ToolStripButton();
            this.tsbDragDropWindow = new System.Windows.Forms.ToolStripButton();
            this.tsbLanguageTranslator = new System.Windows.Forms.ToolStripButton();
            this.tsbScreenColorPicker = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbOpenHistory = new System.Windows.Forms.ToolStripButton();
            this.tsbImageDirectory = new System.Windows.Forms.ToolStripButton();
            this.tsbAbout = new System.Windows.Forms.ToolStripButton();
            this.tsbDonate = new System.Windows.Forms.ToolStripLabel();
            this.tsCoreMainTab.SuspendLayout();
            this.SuspendLayout();
            // 
            // tsCoreMainTab
            // 
            this.tsCoreMainTab.BackColor = System.Drawing.Color.White;
            this.tsCoreMainTab.CanOverflow = false;
            this.tsCoreMainTab.Dock = System.Windows.Forms.DockStyle.Right;
            this.tsCoreMainTab.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.tsCoreMainTab.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbFullscreenCapture,
            this.tsbActiveWindow,
            this.tsbSelectedWindow,
            this.tsddbCoreSelectedWindow,
            this.tsbCropShot,
            this.tsbLastCropShot,
            this.tsbFreehandCropShot,
            this.tsbAutoCapture,
            this.tssMaintoolbar1,
            this.tsbFileUpload,
            this.tsbClipboardUpload,
            this.tsbDragDropWindow,
            this.tsbLanguageTranslator,
            this.tsbScreenColorPicker,
            this.toolStripSeparator8,
            this.tsbOpenHistory,
            this.tsbImageDirectory,
            this.tsbAbout,
            this.tsbDonate});
            this.tsCoreMainTab.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.VerticalStackWithOverflow;
            this.tsCoreMainTab.Location = new System.Drawing.Point(206, 0);
            this.tsCoreMainTab.Name = "tsCoreMainTab";
            this.tsCoreMainTab.Size = new System.Drawing.Size(158, 430);
            this.tsCoreMainTab.TabIndex = 0;
            // 
            // tsbFullscreenCapture
            // 
            this.tsbFullscreenCapture.Image = global::ZScreenGUI.Properties.Resources.monitor;
            this.tsbFullscreenCapture.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.tsbFullscreenCapture.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbFullscreenCapture.Name = "tsbFullscreenCapture";
            this.tsbFullscreenCapture.Size = new System.Drawing.Size(155, 20);
            this.tsbFullscreenCapture.Text = "Capture Fullscreen";
            this.tsbFullscreenCapture.Click += new System.EventHandler(this.tsbFullscreenCapture_Click);
            // 
            // tsbActiveWindow
            // 
            this.tsbActiveWindow.Image = global::ZScreenGUI.Properties.Resources.application_go;
            this.tsbActiveWindow.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.tsbActiveWindow.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbActiveWindow.Name = "tsbActiveWindow";
            this.tsbActiveWindow.Size = new System.Drawing.Size(155, 20);
            this.tsbActiveWindow.Text = "Active Window (3 sec)";
            this.tsbActiveWindow.ToolTipText = "Active Window will capture after 3 seconds";
            this.tsbActiveWindow.Click += new System.EventHandler(this.tsbActiveWindow_Click);
            // 
            // tsbSelectedWindow
            // 
            this.tsbSelectedWindow.Image = global::ZScreenGUI.Properties.Resources.application_side_boxes;
            this.tsbSelectedWindow.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.tsbSelectedWindow.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbSelectedWindow.Name = "tsbSelectedWindow";
            this.tsbSelectedWindow.Size = new System.Drawing.Size(155, 20);
            this.tsbSelectedWindow.Text = "Capture Controls...";
            this.tsbSelectedWindow.Click += new System.EventHandler(this.tsbSelectedWindow_Click);
            // 
            // tsddbCoreSelectedWindow
            // 
            this.tsddbCoreSelectedWindow.Image = global::ZScreenGUI.Properties.Resources.application_double;
            this.tsddbCoreSelectedWindow.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.tsddbCoreSelectedWindow.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsddbCoreSelectedWindow.Name = "tsddbCoreSelectedWindow";
            this.tsddbCoreSelectedWindow.Size = new System.Drawing.Size(155, 20);
            this.tsddbCoreSelectedWindow.Text = "Capture Window...";
            this.tsddbCoreSelectedWindow.DropDownOpening += new System.EventHandler(this.tsddbSelectedWindow_DropDownOpening);
            // 
            // tsbCropShot
            // 
            this.tsbCropShot.Image = global::ZScreenGUI.Properties.Resources.shape_square;
            this.tsbCropShot.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.tsbCropShot.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbCropShot.Name = "tsbCropShot";
            this.tsbCropShot.Size = new System.Drawing.Size(155, 20);
            this.tsbCropShot.Text = "Capture Rectangle...";
            this.tsbCropShot.Click += new System.EventHandler(this.tsbCropShot_Click);
            // 
            // tsbLastCropShot
            // 
            this.tsbLastCropShot.Image = global::ZScreenGUI.Properties.Resources.shape_square_go;
            this.tsbLastCropShot.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.tsbLastCropShot.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbLastCropShot.Name = "tsbLastCropShot";
            this.tsbLastCropShot.Size = new System.Drawing.Size(155, 20);
            this.tsbLastCropShot.Text = "Capture Last Rectangle...";
            this.tsbLastCropShot.Click += new System.EventHandler(this.tsbLastCropShot_Click);
            // 
            // tsbFreehandCropShot
            // 
            this.tsbFreehandCropShot.Image = global::ZScreenGUI.Properties.Resources.shape_square_edit;
            this.tsbFreehandCropShot.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.tsbFreehandCropShot.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbFreehandCropShot.Name = "tsbFreehandCropShot";
            this.tsbFreehandCropShot.Size = new System.Drawing.Size(155, 20);
            this.tsbFreehandCropShot.Text = "Capture Shape...";
            this.tsbFreehandCropShot.Click += new System.EventHandler(this.tsbFreehandCropShot_Click);
            // 
            // tsbAutoCapture
            // 
            this.tsbAutoCapture.Image = global::ZScreenGUI.Properties.Resources.images_stack;
            this.tsbAutoCapture.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.tsbAutoCapture.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbAutoCapture.Name = "tsbAutoCapture";
            this.tsbAutoCapture.Size = new System.Drawing.Size(155, 20);
            this.tsbAutoCapture.Text = "Auto Capture...";
            this.tsbAutoCapture.Click += new System.EventHandler(this.tsbAutoCapture_Click);
            // 
            // tssMaintoolbar1
            // 
            this.tssMaintoolbar1.Name = "tssMaintoolbar1";
            this.tssMaintoolbar1.Size = new System.Drawing.Size(155, 6);
            // 
            // tsbFileUpload
            // 
            this.tsbFileUpload.Image = global::ZScreenGUI.Properties.Resources.drive_network;
            this.tsbFileUpload.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.tsbFileUpload.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbFileUpload.Name = "tsbFileUpload";
            this.tsbFileUpload.Size = new System.Drawing.Size(155, 20);
            this.tsbFileUpload.Text = "File Upload...";
            this.tsbFileUpload.Click += new System.EventHandler(this.tsbFileUpload_Click);
            // 
            // tsbClipboardUpload
            // 
            this.tsbClipboardUpload.Image = global::ZScreenGUI.Properties.Resources.images;
            this.tsbClipboardUpload.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.tsbClipboardUpload.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbClipboardUpload.Name = "tsbClipboardUpload";
            this.tsbClipboardUpload.Size = new System.Drawing.Size(155, 20);
            this.tsbClipboardUpload.Text = "Clipboard Upload...";
            this.tsbClipboardUpload.Click += new System.EventHandler(this.tsbClipboardUpload_Click);
            // 
            // tsbDragDropWindow
            // 
            this.tsbDragDropWindow.Image = global::ZScreenGUI.Properties.Resources.shape_move_backwards;
            this.tsbDragDropWindow.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.tsbDragDropWindow.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbDragDropWindow.Name = "tsbDragDropWindow";
            this.tsbDragDropWindow.Size = new System.Drawing.Size(155, 20);
            this.tsbDragDropWindow.Text = "Drag && Drop Window...";
            this.tsbDragDropWindow.Click += new System.EventHandler(this.tsbDragDropWindow_Click);
            // 
            // tsbLanguageTranslator
            // 
            this.tsbLanguageTranslator.Image = global::ZScreenGUI.Properties.Resources.comments;
            this.tsbLanguageTranslator.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.tsbLanguageTranslator.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbLanguageTranslator.Name = "tsbLanguageTranslator";
            this.tsbLanguageTranslator.Size = new System.Drawing.Size(155, 20);
            this.tsbLanguageTranslator.Text = "Google Translate...";
            this.tsbLanguageTranslator.Click += new System.EventHandler(this.tsbLanguageTranslator_Click);
            // 
            // tsbScreenColorPicker
            // 
            this.tsbScreenColorPicker.Image = global::ZScreenGUI.Properties.Resources.color_wheel;
            this.tsbScreenColorPicker.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.tsbScreenColorPicker.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbScreenColorPicker.Name = "tsbScreenColorPicker";
            this.tsbScreenColorPicker.Size = new System.Drawing.Size(155, 20);
            this.tsbScreenColorPicker.Text = "Screen Color Picker...";
            this.tsbScreenColorPicker.Click += new System.EventHandler(this.tsbScreenColorPicker_Click);
            // 
            // toolStripSeparator8
            // 
            this.toolStripSeparator8.Name = "toolStripSeparator8";
            this.toolStripSeparator8.Size = new System.Drawing.Size(155, 6);
            // 
            // tsbOpenHistory
            // 
            this.tsbOpenHistory.Image = global::ZScreenGUI.Properties.Resources.pictures;
            this.tsbOpenHistory.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.tsbOpenHistory.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbOpenHistory.Name = "tsbOpenHistory";
            this.tsbOpenHistory.Size = new System.Drawing.Size(155, 20);
            this.tsbOpenHistory.Text = "History...";
            this.tsbOpenHistory.Click += new System.EventHandler(this.tsbOpenHistory_Click);
            // 
            // tsbImageDirectory
            // 
            this.tsbImageDirectory.Image = global::ZScreenGUI.Properties.Resources.folder_picture;
            this.tsbImageDirectory.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.tsbImageDirectory.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbImageDirectory.Name = "tsbImageDirectory";
            this.tsbImageDirectory.Size = new System.Drawing.Size(155, 20);
            this.tsbImageDirectory.Text = "Images Directory...";
            this.tsbImageDirectory.Click += new System.EventHandler(this.tsbImageDirectory_Click);
            // 
            // tsbAbout
            // 
            this.tsbAbout.Image = global::ZScreenGUI.Properties.Resources.information;
            this.tsbAbout.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.tsbAbout.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbAbout.Name = "tsbAbout";
            this.tsbAbout.Size = new System.Drawing.Size(155, 20);
            this.tsbAbout.Text = "About...";
            this.tsbAbout.Click += new System.EventHandler(this.tsbAbout_Click);
            // 
            // tsbDonate
            // 
            this.tsbDonate.AutoSize = false;
            this.tsbDonate.BackgroundImage = global::ZScreenGUI.Properties.Resources.paypal;
            this.tsbDonate.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.tsbDonate.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.None;
            this.tsbDonate.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbDonate.ImageTransparentColor = System.Drawing.Color.Transparent;
            this.tsbDonate.Margin = new System.Windows.Forms.Padding(0, 8, 0, 0);
            this.tsbDonate.Name = "tsbDonate";
            this.tsbDonate.Size = new System.Drawing.Size(100, 35);
            this.tsbDonate.Text = "Donate";
            this.tsbDonate.ToolTipText = "Thanks!";
            this.tsbDonate.Click += new System.EventHandler(this.tsbDonate_Click);
            // 
            // ZScreenCoreUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(364, 430);
            this.Controls.Add(this.tsCoreMainTab);
            this.Name = "ZScreenCoreUI";
            this.Text = "ZScreenCoreUI";
            this.tsCoreMainTab.ResumeLayout(false);
            this.tsCoreMainTab.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStripButton tsbFullscreenCapture;
        private System.Windows.Forms.ToolStripButton tsbSelectedWindow;
        private System.Windows.Forms.ToolStripButton tsbCropShot;
        private System.Windows.Forms.ToolStripButton tsbLastCropShot;
        private System.Windows.Forms.ToolStripButton tsbFreehandCropShot;
        private System.Windows.Forms.ToolStripButton tsbAutoCapture;
        private System.Windows.Forms.ToolStripSeparator tssMaintoolbar1;
        private System.Windows.Forms.ToolStripButton tsbFileUpload;
        private System.Windows.Forms.ToolStripButton tsbClipboardUpload;
        private System.Windows.Forms.ToolStripButton tsbDragDropWindow;
        private System.Windows.Forms.ToolStripButton tsbLanguageTranslator;
        private System.Windows.Forms.ToolStripButton tsbScreenColorPicker;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator8;
        private System.Windows.Forms.ToolStripButton tsbOpenHistory;
        private System.Windows.Forms.ToolStripButton tsbImageDirectory;
        private System.Windows.Forms.ToolStripButton tsbAbout;
        private System.Windows.Forms.ToolStripLabel tsbDonate;
        protected System.Windows.Forms.ToolStrip tsCoreMainTab;
        public System.Windows.Forms.ToolStripDropDownButton tsddbCoreSelectedWindow;
        public System.Windows.Forms.ToolStripButton tsbActiveWindow;
    }
}
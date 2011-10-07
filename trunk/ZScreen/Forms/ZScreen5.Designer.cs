namespace ZScreenGUI
{
    partial class ZScreenSnap
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
            this.tsMainTab = new System.Windows.Forms.ToolStrip();
            this.tsbFullscreenCapture = new System.Windows.Forms.ToolStripButton();
            this.tsbActiveWindow = new System.Windows.Forms.ToolStripButton();
            this.tsbSelectedWindow = new System.Windows.Forms.ToolStripButton();
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
            this.tlpMain = new System.Windows.Forms.TableLayoutPanel();
            this.pbPreview = new System.Windows.Forms.PictureBox();
            this.flpTasks = new System.Windows.Forms.FlowLayoutPanel();
            this.btnTasksActions = new System.Windows.Forms.Button();
            this.btnTasksEffects = new System.Windows.Forms.Button();
            this.tsMainTab.SuspendLayout();
            this.tlpMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbPreview)).BeginInit();
            this.flpTasks.SuspendLayout();
            this.SuspendLayout();
            // 
            // tsMainTab
            // 
            this.tsMainTab.BackColor = System.Drawing.Color.White;
            this.tsMainTab.CanOverflow = false;
            this.tsMainTab.Dock = System.Windows.Forms.DockStyle.None;
            this.tsMainTab.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.tsMainTab.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbFullscreenCapture,
            this.tsbActiveWindow,
            this.tsbSelectedWindow,
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
            this.tsMainTab.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.VerticalStackWithOverflow;
            this.tsMainTab.Location = new System.Drawing.Point(686, 0);
            this.tsMainTab.Name = "tsMainTab";
            this.tsMainTab.Size = new System.Drawing.Size(158, 379);
            this.tsMainTab.TabIndex = 127;
            // 
            // tsbFullscreenCapture
            // 
            this.tsbFullscreenCapture.Image = global::ZScreenGUI.Properties.Resources.monitor;
            this.tsbFullscreenCapture.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.tsbFullscreenCapture.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbFullscreenCapture.Name = "tsbFullscreenCapture";
            this.tsbFullscreenCapture.Size = new System.Drawing.Size(156, 20);
            this.tsbFullscreenCapture.Text = "Capture Fullscreen";
            // 
            // tsbActiveWindow
            // 
            this.tsbActiveWindow.Image = global::ZScreenGUI.Properties.Resources.application_go;
            this.tsbActiveWindow.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.tsbActiveWindow.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbActiveWindow.Name = "tsbActiveWindow";
            this.tsbActiveWindow.Size = new System.Drawing.Size(156, 20);
            this.tsbActiveWindow.Text = "Active Window (3 sec)";
            this.tsbActiveWindow.ToolTipText = "Active Window will capture after 3 seconds";
            // 
            // tsbSelectedWindow
            // 
            this.tsbSelectedWindow.Image = global::ZScreenGUI.Properties.Resources.application_double;
            this.tsbSelectedWindow.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.tsbSelectedWindow.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbSelectedWindow.Name = "tsbSelectedWindow";
            this.tsbSelectedWindow.Size = new System.Drawing.Size(156, 20);
            this.tsbSelectedWindow.Text = "Capture Window...";
            // 
            // tsbCropShot
            // 
            this.tsbCropShot.Image = global::ZScreenGUI.Properties.Resources.shape_square;
            this.tsbCropShot.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.tsbCropShot.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbCropShot.Name = "tsbCropShot";
            this.tsbCropShot.Size = new System.Drawing.Size(156, 20);
            this.tsbCropShot.Text = "Capture Rectangle...";
            // 
            // tsbLastCropShot
            // 
            this.tsbLastCropShot.Image = global::ZScreenGUI.Properties.Resources.shape_square_go;
            this.tsbLastCropShot.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.tsbLastCropShot.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbLastCropShot.Name = "tsbLastCropShot";
            this.tsbLastCropShot.Size = new System.Drawing.Size(156, 20);
            this.tsbLastCropShot.Text = "Capture Last Rectangle...";
            // 
            // tsbFreehandCropShot
            // 
            this.tsbFreehandCropShot.Image = global::ZScreenGUI.Properties.Resources.shape_square_edit;
            this.tsbFreehandCropShot.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.tsbFreehandCropShot.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbFreehandCropShot.Name = "tsbFreehandCropShot";
            this.tsbFreehandCropShot.Size = new System.Drawing.Size(156, 20);
            this.tsbFreehandCropShot.Text = "Capture Shape...";
            // 
            // tsbAutoCapture
            // 
            this.tsbAutoCapture.Image = global::ZScreenGUI.Properties.Resources.images_stack;
            this.tsbAutoCapture.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.tsbAutoCapture.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbAutoCapture.Name = "tsbAutoCapture";
            this.tsbAutoCapture.Size = new System.Drawing.Size(156, 20);
            this.tsbAutoCapture.Text = "Auto Capture...";
            // 
            // tssMaintoolbar1
            // 
            this.tssMaintoolbar1.Name = "tssMaintoolbar1";
            this.tssMaintoolbar1.Size = new System.Drawing.Size(156, 6);
            // 
            // tsbFileUpload
            // 
            this.tsbFileUpload.Image = global::ZScreenGUI.Properties.Resources.drive_network;
            this.tsbFileUpload.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.tsbFileUpload.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbFileUpload.Name = "tsbFileUpload";
            this.tsbFileUpload.Size = new System.Drawing.Size(156, 20);
            this.tsbFileUpload.Text = "File Upload...";
            // 
            // tsbClipboardUpload
            // 
            this.tsbClipboardUpload.Image = global::ZScreenGUI.Properties.Resources.images;
            this.tsbClipboardUpload.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.tsbClipboardUpload.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbClipboardUpload.Name = "tsbClipboardUpload";
            this.tsbClipboardUpload.Size = new System.Drawing.Size(156, 20);
            this.tsbClipboardUpload.Text = "Clipboard Upload...";
            // 
            // tsbDragDropWindow
            // 
            this.tsbDragDropWindow.Image = global::ZScreenGUI.Properties.Resources.shape_move_backwards;
            this.tsbDragDropWindow.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.tsbDragDropWindow.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbDragDropWindow.Name = "tsbDragDropWindow";
            this.tsbDragDropWindow.Size = new System.Drawing.Size(156, 20);
            this.tsbDragDropWindow.Text = "Drag && Drop Window...";
            // 
            // tsbLanguageTranslator
            // 
            this.tsbLanguageTranslator.Image = global::ZScreenGUI.Properties.Resources.comments;
            this.tsbLanguageTranslator.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.tsbLanguageTranslator.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbLanguageTranslator.Name = "tsbLanguageTranslator";
            this.tsbLanguageTranslator.Size = new System.Drawing.Size(156, 20);
            this.tsbLanguageTranslator.Text = "Google Translate...";
            this.tsbLanguageTranslator.Visible = false;
            // 
            // tsbScreenColorPicker
            // 
            this.tsbScreenColorPicker.Image = global::ZScreenGUI.Properties.Resources.color_wheel;
            this.tsbScreenColorPicker.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.tsbScreenColorPicker.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbScreenColorPicker.Name = "tsbScreenColorPicker";
            this.tsbScreenColorPicker.Size = new System.Drawing.Size(156, 20);
            this.tsbScreenColorPicker.Text = "Screen Color Picker...";
            // 
            // toolStripSeparator8
            // 
            this.toolStripSeparator8.Name = "toolStripSeparator8";
            this.toolStripSeparator8.Size = new System.Drawing.Size(156, 6);
            // 
            // tsbOpenHistory
            // 
            this.tsbOpenHistory.Image = global::ZScreenGUI.Properties.Resources.pictures;
            this.tsbOpenHistory.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.tsbOpenHistory.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbOpenHistory.Name = "tsbOpenHistory";
            this.tsbOpenHistory.Size = new System.Drawing.Size(156, 20);
            this.tsbOpenHistory.Text = "History...";
            // 
            // tsbImageDirectory
            // 
            this.tsbImageDirectory.Image = global::ZScreenGUI.Properties.Resources.folder_picture;
            this.tsbImageDirectory.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.tsbImageDirectory.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbImageDirectory.Name = "tsbImageDirectory";
            this.tsbImageDirectory.Size = new System.Drawing.Size(156, 20);
            this.tsbImageDirectory.Text = "Images Directory...";
            // 
            // tsbAbout
            // 
            this.tsbAbout.Image = global::ZScreenGUI.Properties.Resources.information;
            this.tsbAbout.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.tsbAbout.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbAbout.Name = "tsbAbout";
            this.tsbAbout.Size = new System.Drawing.Size(156, 20);
            this.tsbAbout.Text = "About...";
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
            // 
            // tlpMain
            // 
            this.tlpMain.ColumnCount = 3;
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 158F));
            this.tlpMain.Controls.Add(this.tsMainTab, 2, 0);
            this.tlpMain.Controls.Add(this.pbPreview, 1, 0);
            this.tlpMain.Controls.Add(this.flpTasks, 0, 0);
            this.tlpMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpMain.Location = new System.Drawing.Point(0, 0);
            this.tlpMain.Name = "tlpMain";
            this.tlpMain.RowCount = 1;
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMain.Size = new System.Drawing.Size(844, 500);
            this.tlpMain.TabIndex = 128;
            // 
            // pbPreview
            // 
            this.pbPreview.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pbPreview.Location = new System.Drawing.Point(103, 3);
            this.pbPreview.Name = "pbPreview";
            this.pbPreview.Size = new System.Drawing.Size(580, 494);
            this.pbPreview.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbPreview.TabIndex = 128;
            this.pbPreview.TabStop = false;
            // 
            // flpTasks
            // 
            this.flpTasks.Controls.Add(this.btnTasksActions);
            this.flpTasks.Controls.Add(this.btnTasksEffects);
            this.flpTasks.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpTasks.Location = new System.Drawing.Point(3, 3);
            this.flpTasks.Name = "flpTasks";
            this.flpTasks.Size = new System.Drawing.Size(94, 494);
            this.flpTasks.TabIndex = 129;
            // 
            // btnTasksActions
            // 
            this.btnTasksActions.Location = new System.Drawing.Point(3, 3);
            this.btnTasksActions.Name = "btnTasksActions";
            this.btnTasksActions.Size = new System.Drawing.Size(75, 23);
            this.btnTasksActions.TabIndex = 0;
            this.btnTasksActions.Text = "Run Actions";
            this.btnTasksActions.UseVisualStyleBackColor = true;
            // 
            // btnTasksEffects
            // 
            this.btnTasksEffects.Location = new System.Drawing.Point(3, 32);
            this.btnTasksEffects.Name = "btnTasksEffects";
            this.btnTasksEffects.Size = new System.Drawing.Size(75, 23);
            this.btnTasksEffects.TabIndex = 1;
            this.btnTasksEffects.Text = "Effects";
            this.btnTasksEffects.UseVisualStyleBackColor = true;
            // 
            // ZScreenSnap
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(844, 500);
            this.Controls.Add(this.tlpMain);
            this.Name = "ZScreenSnap";
            this.Text = "ZScreenSnap";
            this.tsMainTab.ResumeLayout(false);
            this.tsMainTab.PerformLayout();
            this.tlpMain.ResumeLayout(false);
            this.tlpMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbPreview)).EndInit();
            this.flpTasks.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolStrip tsMainTab;
        private System.Windows.Forms.ToolStripButton tsbFullscreenCapture;
        private System.Windows.Forms.ToolStripButton tsbActiveWindow;
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
        private System.Windows.Forms.TableLayoutPanel tlpMain;
        private System.Windows.Forms.PictureBox pbPreview;
        private System.Windows.Forms.FlowLayoutPanel flpTasks;
        private System.Windows.Forms.Button btnTasksActions;
        private System.Windows.Forms.Button btnTasksEffects;
    }
}
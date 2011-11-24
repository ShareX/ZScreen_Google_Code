/*
 * Greenshot - a free and open source screenshot tool
 * Copyright (C) 2007-2011  Thomas Braun, Jens Klingen, Robin Krom
 * 
 * For more information see: http://getgreenshot.org/
 * The Greenshot project is hosted on Sourceforge: http://sourceforge.net/projects/greenshot/
 * 
 * This program is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 1 of the License, or
 * (at your option) any later version.
 * 
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 * 
 * You should have received a copy of the GNU General Public License
 * along with this program.  If not, see <http://www.gnu.org/licenses/>.
 */
namespace Greenshot {
	partial class SettingsForm : System.Windows.Forms.Form {
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		
		/// <summary>
		/// Disposes resources used by the form.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing) {
				if (components != null) {
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}
		
		/// <summary>
		/// This method is required for Windows Forms designer support.
		/// Do not change the method contents inside the source code editor. The Forms designer might
		/// not be able to load this method if it was changed manually.
		/// </summary>
		private void InitializeComponent()
		{
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingsForm));
            this.settings_cancel = new System.Windows.Forms.Button();
            this.settings_okay = new System.Windows.Forms.Button();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.tabcontrol = new System.Windows.Forms.TabControl();
            this.tab_capture = new System.Windows.Forms.TabPage();
            this.groupbox_editor = new System.Windows.Forms.GroupBox();
            this.checkbox_editor_match_capture_size = new System.Windows.Forms.CheckBox();
            this.groupbox_iecapture = new System.Windows.Forms.GroupBox();
            this.checkbox_ie_capture = new System.Windows.Forms.CheckBox();
            this.groupbox_windowscapture = new System.Windows.Forms.GroupBox();
            this.colorButton_window_background = new Greenshot.Controls.ColorButton();
            this.label_window_capture_mode = new System.Windows.Forms.Label();
            this.checkbox_capture_windows_interactive = new System.Windows.Forms.CheckBox();
            this.combobox_window_capture_mode = new System.Windows.Forms.ComboBox();
            this.groupbox_capture = new System.Windows.Forms.GroupBox();
            this.checkbox_playsound = new System.Windows.Forms.CheckBox();
            this.checkbox_capture_mousepointer = new System.Windows.Forms.CheckBox();
            this.numericUpDownWaitTime = new System.Windows.Forms.NumericUpDown();
            this.label_waittime = new System.Windows.Forms.Label();
            this.tab_printer = new System.Windows.Forms.TabPage();
            this.groupbox_printoptions = new System.Windows.Forms.GroupBox();
            this.checkboxPrintInverted = new System.Windows.Forms.CheckBox();
            this.checkbox_alwaysshowprintoptionsdialog = new System.Windows.Forms.CheckBox();
            this.checkboxTimestamp = new System.Windows.Forms.CheckBox();
            this.checkboxAllowCenter = new System.Windows.Forms.CheckBox();
            this.checkboxAllowRotate = new System.Windows.Forms.CheckBox();
            this.checkboxAllowEnlarge = new System.Windows.Forms.CheckBox();
            this.checkboxAllowShrink = new System.Windows.Forms.CheckBox();
            this.tab_plugins = new System.Windows.Forms.TabPage();
            this.groupbox_plugins = new System.Windows.Forms.GroupBox();
            this.listview_plugins = new System.Windows.Forms.ListView();
            this.button_pluginconfigure = new System.Windows.Forms.Button();
            this.tabcontrol.SuspendLayout();
            this.tab_capture.SuspendLayout();
            this.groupbox_editor.SuspendLayout();
            this.groupbox_iecapture.SuspendLayout();
            this.groupbox_windowscapture.SuspendLayout();
            this.groupbox_capture.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownWaitTime)).BeginInit();
            this.tab_printer.SuspendLayout();
            this.groupbox_printoptions.SuspendLayout();
            this.tab_plugins.SuspendLayout();
            this.groupbox_plugins.SuspendLayout();
            this.SuspendLayout();
            // 
            // settings_cancel
            // 
            this.settings_cancel.Location = new System.Drawing.Point(367, 366);
            this.settings_cancel.Name = "settings_cancel";
            this.settings_cancel.Size = new System.Drawing.Size(75, 23);
            this.settings_cancel.TabIndex = 2;
            this.settings_cancel.Text = "Cancel";
            this.settings_cancel.UseVisualStyleBackColor = true;
            this.settings_cancel.Click += new System.EventHandler(this.Settings_cancelClick);
            // 
            // settings_okay
            // 
            this.settings_okay.Location = new System.Drawing.Point(286, 366);
            this.settings_okay.Name = "settings_okay";
            this.settings_okay.Size = new System.Drawing.Size(75, 23);
            this.settings_okay.TabIndex = 1;
            this.settings_okay.Text = "OK";
            this.settings_okay.UseVisualStyleBackColor = true;
            this.settings_okay.Click += new System.EventHandler(this.Settings_okayClick);
            // 
            // tabcontrol
            // 
            this.tabcontrol.Controls.Add(this.tab_capture);
            this.tabcontrol.Controls.Add(this.tab_printer);
            this.tabcontrol.Location = new System.Drawing.Point(12, 13);
            this.tabcontrol.Name = "tabcontrol";
            this.tabcontrol.SelectedIndex = 0;
            this.tabcontrol.Size = new System.Drawing.Size(431, 346);
            this.tabcontrol.TabIndex = 0;
            // 
            // tab_capture
            // 
            this.tab_capture.Controls.Add(this.groupbox_editor);
            this.tab_capture.Controls.Add(this.groupbox_iecapture);
            this.tab_capture.Controls.Add(this.groupbox_windowscapture);
            this.tab_capture.Controls.Add(this.groupbox_capture);
            this.tab_capture.Location = new System.Drawing.Point(4, 22);
            this.tab_capture.Name = "tab_capture";
            this.tab_capture.Size = new System.Drawing.Size(423, 320);
            this.tab_capture.TabIndex = 0;
            this.tab_capture.Text = "Capture";
            this.tab_capture.UseVisualStyleBackColor = true;
            // 
            // groupbox_editor
            // 
            this.groupbox_editor.Controls.Add(this.checkbox_editor_match_capture_size);
            this.groupbox_editor.Location = new System.Drawing.Point(4, 260);
            this.groupbox_editor.Name = "groupbox_editor";
            this.groupbox_editor.Size = new System.Drawing.Size(416, 50);
            this.groupbox_editor.TabIndex = 3;
            this.groupbox_editor.TabStop = false;
            this.groupbox_editor.Text = "Editor";
            // 
            // checkbox_editor_match_capture_size
            // 
            this.checkbox_editor_match_capture_size.Location = new System.Drawing.Point(6, 19);
            this.checkbox_editor_match_capture_size.Name = "checkbox_editor_match_capture_size";
            this.checkbox_editor_match_capture_size.Size = new System.Drawing.Size(397, 24);
            this.checkbox_editor_match_capture_size.TabIndex = 0;
            this.checkbox_editor_match_capture_size.Text = "Match capture size";
            this.checkbox_editor_match_capture_size.UseVisualStyleBackColor = true;
            // 
            // groupbox_iecapture
            // 
            this.groupbox_iecapture.Controls.Add(this.checkbox_ie_capture);
            this.groupbox_iecapture.Location = new System.Drawing.Point(4, 204);
            this.groupbox_iecapture.Name = "groupbox_iecapture";
            this.groupbox_iecapture.Size = new System.Drawing.Size(416, 50);
            this.groupbox_iecapture.TabIndex = 2;
            this.groupbox_iecapture.TabStop = false;
            this.groupbox_iecapture.Text = "IE Capture settings";
            // 
            // checkbox_ie_capture
            // 
            this.checkbox_ie_capture.Location = new System.Drawing.Point(6, 19);
            this.checkbox_ie_capture.Name = "checkbox_ie_capture";
            this.checkbox_ie_capture.Size = new System.Drawing.Size(213, 24);
            this.checkbox_ie_capture.TabIndex = 0;
            this.checkbox_ie_capture.Text = "IE capture";
            this.checkbox_ie_capture.UseVisualStyleBackColor = true;
            // 
            // groupbox_windowscapture
            // 
            this.groupbox_windowscapture.Controls.Add(this.colorButton_window_background);
            this.groupbox_windowscapture.Controls.Add(this.label_window_capture_mode);
            this.groupbox_windowscapture.Controls.Add(this.checkbox_capture_windows_interactive);
            this.groupbox_windowscapture.Controls.Add(this.combobox_window_capture_mode);
            this.groupbox_windowscapture.Location = new System.Drawing.Point(4, 117);
            this.groupbox_windowscapture.Name = "groupbox_windowscapture";
            this.groupbox_windowscapture.Size = new System.Drawing.Size(416, 80);
            this.groupbox_windowscapture.TabIndex = 1;
            this.groupbox_windowscapture.TabStop = false;
            this.groupbox_windowscapture.Text = "Window capture settings";
            // 
            // colorButton_window_background
            // 
            this.colorButton_window_background.AutoSize = true;
            this.colorButton_window_background.Image = ((System.Drawing.Image)(resources.GetObject("colorButton_window_background.Image")));
            this.colorButton_window_background.Location = new System.Drawing.Point(374, 37);
            this.colorButton_window_background.Name = "colorButton_window_background";
            this.colorButton_window_background.SelectedColor = System.Drawing.Color.White;
            this.colorButton_window_background.Size = new System.Drawing.Size(29, 30);
            this.colorButton_window_background.TabIndex = 2;
            this.colorButton_window_background.UseVisualStyleBackColor = true;
            // 
            // label_window_capture_mode
            // 
            this.label_window_capture_mode.Location = new System.Drawing.Point(6, 46);
            this.label_window_capture_mode.Name = "label_window_capture_mode";
            this.label_window_capture_mode.Size = new System.Drawing.Size(205, 23);
            this.label_window_capture_mode.TabIndex = 3;
            this.label_window_capture_mode.Text = "Window capture mode";
            // 
            // checkbox_capture_windows_interactive
            // 
            this.checkbox_capture_windows_interactive.Location = new System.Drawing.Point(9, 19);
            this.checkbox_capture_windows_interactive.Name = "checkbox_capture_windows_interactive";
            this.checkbox_capture_windows_interactive.Size = new System.Drawing.Size(394, 18);
            this.checkbox_capture_windows_interactive.TabIndex = 0;
            this.checkbox_capture_windows_interactive.Text = "Interactiv window capture";
            this.checkbox_capture_windows_interactive.UseVisualStyleBackColor = true;
            // 
            // combobox_window_capture_mode
            // 
            this.combobox_window_capture_mode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.combobox_window_capture_mode.FormattingEnabled = true;
            this.combobox_window_capture_mode.Location = new System.Drawing.Point(217, 43);
            this.combobox_window_capture_mode.MaxDropDownItems = 15;
            this.combobox_window_capture_mode.Name = "combobox_window_capture_mode";
            this.combobox_window_capture_mode.Size = new System.Drawing.Size(151, 21);
            this.combobox_window_capture_mode.TabIndex = 1;
            this.combobox_window_capture_mode.SelectedIndexChanged += new System.EventHandler(this.Combobox_window_capture_modeSelectedIndexChanged);
            // 
            // groupbox_capture
            // 
            this.groupbox_capture.Controls.Add(this.checkbox_playsound);
            this.groupbox_capture.Controls.Add(this.checkbox_capture_mousepointer);
            this.groupbox_capture.Controls.Add(this.numericUpDownWaitTime);
            this.groupbox_capture.Controls.Add(this.label_waittime);
            this.groupbox_capture.Location = new System.Drawing.Point(4, 4);
            this.groupbox_capture.Name = "groupbox_capture";
            this.groupbox_capture.Size = new System.Drawing.Size(416, 106);
            this.groupbox_capture.TabIndex = 0;
            this.groupbox_capture.TabStop = false;
            this.groupbox_capture.Text = "General Capture settings";
            // 
            // checkbox_playsound
            // 
            this.checkbox_playsound.Location = new System.Drawing.Point(11, 39);
            this.checkbox_playsound.Name = "checkbox_playsound";
            this.checkbox_playsound.Size = new System.Drawing.Size(399, 24);
            this.checkbox_playsound.TabIndex = 1;
            this.checkbox_playsound.Text = "Play camera sound";
            this.checkbox_playsound.UseVisualStyleBackColor = true;
            // 
            // checkbox_capture_mousepointer
            // 
            this.checkbox_capture_mousepointer.Location = new System.Drawing.Point(11, 19);
            this.checkbox_capture_mousepointer.Name = "checkbox_capture_mousepointer";
            this.checkbox_capture_mousepointer.Size = new System.Drawing.Size(394, 24);
            this.checkbox_capture_mousepointer.TabIndex = 0;
            this.checkbox_capture_mousepointer.Text = "Capture mousepointer";
            this.checkbox_capture_mousepointer.UseVisualStyleBackColor = true;
            // 
            // numericUpDownWaitTime
            // 
            this.numericUpDownWaitTime.Increment = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.numericUpDownWaitTime.Location = new System.Drawing.Point(11, 69);
            this.numericUpDownWaitTime.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numericUpDownWaitTime.Name = "numericUpDownWaitTime";
            this.numericUpDownWaitTime.Size = new System.Drawing.Size(57, 20);
            this.numericUpDownWaitTime.TabIndex = 2;
            this.numericUpDownWaitTime.ThousandsSeparator = true;
            // 
            // label_waittime
            // 
            this.label_waittime.Location = new System.Drawing.Point(74, 71);
            this.label_waittime.Name = "label_waittime";
            this.label_waittime.Size = new System.Drawing.Size(331, 16);
            this.label_waittime.TabIndex = 3;
            this.label_waittime.Text = "Wait before capture (ms)";
            // 
            // tab_printer
            // 
            this.tab_printer.Controls.Add(this.groupbox_printoptions);
            this.tab_printer.Location = new System.Drawing.Point(4, 22);
            this.tab_printer.Name = "tab_printer";
            this.tab_printer.Padding = new System.Windows.Forms.Padding(3);
            this.tab_printer.Size = new System.Drawing.Size(423, 320);
            this.tab_printer.TabIndex = 1;
            this.tab_printer.Text = "Printer";
            this.tab_printer.UseVisualStyleBackColor = true;
            // 
            // groupbox_printoptions
            // 
            this.groupbox_printoptions.Controls.Add(this.checkboxPrintInverted);
            this.groupbox_printoptions.Controls.Add(this.checkbox_alwaysshowprintoptionsdialog);
            this.groupbox_printoptions.Controls.Add(this.checkboxTimestamp);
            this.groupbox_printoptions.Controls.Add(this.checkboxAllowCenter);
            this.groupbox_printoptions.Controls.Add(this.checkboxAllowRotate);
            this.groupbox_printoptions.Controls.Add(this.checkboxAllowEnlarge);
            this.groupbox_printoptions.Controls.Add(this.checkboxAllowShrink);
            this.groupbox_printoptions.Location = new System.Drawing.Point(2, 6);
            this.groupbox_printoptions.Name = "groupbox_printoptions";
            this.groupbox_printoptions.Size = new System.Drawing.Size(412, 227);
            this.groupbox_printoptions.TabIndex = 0;
            this.groupbox_printoptions.TabStop = false;
            this.groupbox_printoptions.Text = "Print options";
            // 
            // checkboxPrintInverted
            // 
            this.checkboxPrintInverted.CheckAlign = System.Drawing.ContentAlignment.TopLeft;
            this.checkboxPrintInverted.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.checkboxPrintInverted.Location = new System.Drawing.Point(12, 144);
            this.checkboxPrintInverted.Name = "checkboxPrintInverted";
            this.checkboxPrintInverted.Size = new System.Drawing.Size(394, 16);
            this.checkboxPrintInverted.TabIndex = 5;
            this.checkboxPrintInverted.Text = "Print inverted";
            this.checkboxPrintInverted.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.checkboxPrintInverted.UseVisualStyleBackColor = true;
            // 
            // checkbox_alwaysshowprintoptionsdialog
            // 
            this.checkbox_alwaysshowprintoptionsdialog.Location = new System.Drawing.Point(12, 167);
            this.checkbox_alwaysshowprintoptionsdialog.Name = "checkbox_alwaysshowprintoptionsdialog";
            this.checkbox_alwaysshowprintoptionsdialog.Size = new System.Drawing.Size(394, 19);
            this.checkbox_alwaysshowprintoptionsdialog.TabIndex = 6;
            this.checkbox_alwaysshowprintoptionsdialog.Text = "Show print options dialog every time an image is printed";
            this.checkbox_alwaysshowprintoptionsdialog.UseVisualStyleBackColor = true;
            // 
            // checkboxTimestamp
            // 
            this.checkboxTimestamp.CheckAlign = System.Drawing.ContentAlignment.TopLeft;
            this.checkboxTimestamp.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.checkboxTimestamp.Location = new System.Drawing.Point(12, 121);
            this.checkboxTimestamp.Name = "checkboxTimestamp";
            this.checkboxTimestamp.Size = new System.Drawing.Size(394, 16);
            this.checkboxTimestamp.TabIndex = 4;
            this.checkboxTimestamp.Text = "Print date / time at bottom of page";
            this.checkboxTimestamp.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.checkboxTimestamp.UseVisualStyleBackColor = true;
            // 
            // checkboxAllowCenter
            // 
            this.checkboxAllowCenter.CheckAlign = System.Drawing.ContentAlignment.TopLeft;
            this.checkboxAllowCenter.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.checkboxAllowCenter.Location = new System.Drawing.Point(12, 96);
            this.checkboxAllowCenter.Name = "checkboxAllowCenter";
            this.checkboxAllowCenter.Size = new System.Drawing.Size(394, 18);
            this.checkboxAllowCenter.TabIndex = 3;
            this.checkboxAllowCenter.Text = "Center printout on page";
            this.checkboxAllowCenter.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.checkboxAllowCenter.UseVisualStyleBackColor = true;
            // 
            // checkboxAllowRotate
            // 
            this.checkboxAllowRotate.CheckAlign = System.Drawing.ContentAlignment.TopLeft;
            this.checkboxAllowRotate.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.checkboxAllowRotate.Location = new System.Drawing.Point(12, 72);
            this.checkboxAllowRotate.Name = "checkboxAllowRotate";
            this.checkboxAllowRotate.Size = new System.Drawing.Size(394, 17);
            this.checkboxAllowRotate.TabIndex = 2;
            this.checkboxAllowRotate.Text = "Rotate printouts to page orientation.";
            this.checkboxAllowRotate.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.checkboxAllowRotate.UseVisualStyleBackColor = true;
            // 
            // checkboxAllowEnlarge
            // 
            this.checkboxAllowEnlarge.CheckAlign = System.Drawing.ContentAlignment.TopLeft;
            this.checkboxAllowEnlarge.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.checkboxAllowEnlarge.Location = new System.Drawing.Point(12, 47);
            this.checkboxAllowEnlarge.Name = "checkboxAllowEnlarge";
            this.checkboxAllowEnlarge.Size = new System.Drawing.Size(394, 19);
            this.checkboxAllowEnlarge.TabIndex = 1;
            this.checkboxAllowEnlarge.Text = "Enlarge small printouts to paper size.";
            this.checkboxAllowEnlarge.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.checkboxAllowEnlarge.UseVisualStyleBackColor = true;
            // 
            // checkboxAllowShrink
            // 
            this.checkboxAllowShrink.CheckAlign = System.Drawing.ContentAlignment.TopLeft;
            this.checkboxAllowShrink.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.checkboxAllowShrink.Location = new System.Drawing.Point(12, 22);
            this.checkboxAllowShrink.Name = "checkboxAllowShrink";
            this.checkboxAllowShrink.Size = new System.Drawing.Size(394, 17);
            this.checkboxAllowShrink.TabIndex = 0;
            this.checkboxAllowShrink.Text = "Shrink large printouts to paper size.";
            this.checkboxAllowShrink.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.checkboxAllowShrink.UseVisualStyleBackColor = true;
            // 
            // tab_plugins
            // 
            this.tab_plugins.Controls.Add(this.groupbox_plugins);
            this.tab_plugins.Location = new System.Drawing.Point(4, 22);
            this.tab_plugins.Name = "tab_plugins";
            this.tab_plugins.Size = new System.Drawing.Size(423, 320);
            this.tab_plugins.TabIndex = 2;
            this.tab_plugins.Text = "Plugins";
            this.tab_plugins.UseVisualStyleBackColor = true;
            // 
            // groupbox_plugins
            // 
            this.groupbox_plugins.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupbox_plugins.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.groupbox_plugins.Controls.Add(this.listview_plugins);
            this.groupbox_plugins.Controls.Add(this.button_pluginconfigure);
            this.groupbox_plugins.Location = new System.Drawing.Point(0, 0);
            this.groupbox_plugins.Name = "groupbox_plugins";
            this.groupbox_plugins.Size = new System.Drawing.Size(423, 314);
            this.groupbox_plugins.TabIndex = 0;
            this.groupbox_plugins.TabStop = false;
            this.groupbox_plugins.Text = "Plugin settings";
            // 
            // listview_plugins
            // 
            this.listview_plugins.Dock = System.Windows.Forms.DockStyle.Top;
            this.listview_plugins.FullRowSelect = true;
            this.listview_plugins.Location = new System.Drawing.Point(3, 16);
            this.listview_plugins.Name = "listview_plugins";
            this.listview_plugins.Size = new System.Drawing.Size(417, 263);
            this.listview_plugins.TabIndex = 2;
            this.listview_plugins.UseCompatibleStateImageBehavior = false;
            this.listview_plugins.View = System.Windows.Forms.View.Details;
            this.listview_plugins.SelectedIndexChanged += new System.EventHandler(this.Listview_pluginsSelectedIndexChanged);
            this.listview_plugins.Click += new System.EventHandler(this.Listview_pluginsSelectedIndexChanged);
            // 
            // button_pluginconfigure
            // 
            this.button_pluginconfigure.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button_pluginconfigure.AutoSize = true;
            this.button_pluginconfigure.Enabled = false;
            this.button_pluginconfigure.Location = new System.Drawing.Point(6, 285);
            this.button_pluginconfigure.Name = "button_pluginconfigure";
            this.button_pluginconfigure.Size = new System.Drawing.Size(75, 23);
            this.button_pluginconfigure.TabIndex = 1;
            this.button_pluginconfigure.Text = "Configure";
            this.button_pluginconfigure.UseVisualStyleBackColor = true;
            this.button_pluginconfigure.Click += new System.EventHandler(this.Button_pluginconfigureClick);
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(451, 396);
            this.Controls.Add(this.tabcontrol);
            this.Controls.Add(this.settings_okay);
            this.Controls.Add(this.settings_cancel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SettingsForm";
            this.Text = "SettingsForm";
            this.tabcontrol.ResumeLayout(false);
            this.tab_capture.ResumeLayout(false);
            this.groupbox_editor.ResumeLayout(false);
            this.groupbox_iecapture.ResumeLayout(false);
            this.groupbox_windowscapture.ResumeLayout(false);
            this.groupbox_windowscapture.PerformLayout();
            this.groupbox_capture.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownWaitTime)).EndInit();
            this.tab_printer.ResumeLayout(false);
            this.groupbox_printoptions.ResumeLayout(false);
            this.tab_plugins.ResumeLayout(false);
            this.groupbox_plugins.ResumeLayout(false);
            this.groupbox_plugins.PerformLayout();
            this.ResumeLayout(false);

		}
		private System.Windows.Forms.GroupBox groupbox_editor;
        private System.Windows.Forms.CheckBox checkbox_editor_match_capture_size;
		private System.Windows.Forms.CheckBox checkboxPrintInverted;
		private Greenshot.Controls.ColorButton colorButton_window_background;
		private System.Windows.Forms.Label label_window_capture_mode;
		private System.Windows.Forms.CheckBox checkbox_ie_capture;
		private System.Windows.Forms.GroupBox groupbox_capture;
		private System.Windows.Forms.GroupBox groupbox_windowscapture;
		private System.Windows.Forms.GroupBox groupbox_iecapture;
		private System.Windows.Forms.TabPage tab_capture;
		private System.Windows.Forms.ComboBox combobox_window_capture_mode;
		private System.Windows.Forms.NumericUpDown numericUpDownWaitTime;
        private System.Windows.Forms.Label label_waittime;
		private System.Windows.Forms.CheckBox checkbox_capture_windows_interactive;
		private System.Windows.Forms.CheckBox checkbox_capture_mousepointer;
		private System.Windows.Forms.TabPage tab_printer;
		private System.Windows.Forms.ListView listview_plugins;
		private System.Windows.Forms.Button button_pluginconfigure;
		private System.Windows.Forms.GroupBox groupbox_plugins;
		private System.Windows.Forms.TabPage tab_plugins;
        private System.Windows.Forms.CheckBox checkboxTimestamp;
		private System.Windows.Forms.CheckBox checkboxAllowShrink;
		private System.Windows.Forms.CheckBox checkboxAllowEnlarge;
		private System.Windows.Forms.CheckBox checkboxAllowRotate;
		private System.Windows.Forms.CheckBox checkboxAllowCenter;
		private System.Windows.Forms.CheckBox checkbox_alwaysshowprintoptionsdialog;
        private System.Windows.Forms.GroupBox groupbox_printoptions;
        private System.Windows.Forms.TabControl tabcontrol;
        private System.Windows.Forms.CheckBox checkbox_playsound;
		private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
		private System.Windows.Forms.Button settings_cancel;
        private System.Windows.Forms.Button settings_okay;
	}
}

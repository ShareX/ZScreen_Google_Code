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

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Threading;
using System.Windows.Forms;

using Greenshot.Configuration;
using Greenshot.Helpers;
using Greenshot.Helpers.OfficeInterop;
using Greenshot.Plugin;
using Greenshot.UnmanagedHelpers;
using GreenshotPlugin.Controls;
using GreenshotPlugin.Core;
using IniFile;

namespace Greenshot
{
    /// <summary>
    /// Description of SettingsForm.
    /// </summary>
    public partial class SettingsForm : Form
    {
        private static CoreConfiguration coreConfiguration = IniConfig.GetIniSection<CoreConfiguration>();
        private static EditorConfiguration editorConfiguration = IniConfig.GetIniSection<EditorConfiguration>();
        ILanguage lang;
        private static log4net.ILog LOG = log4net.LogManager.GetLogger(typeof(SettingsForm));
        private ToolTip toolTip;

        public SettingsForm()
        {
            InitializeComponent();

            lang = Language.GetInstance();
            // Force re-loading of languages
            lang.Load();

            toolTip = new ToolTip();
            AddPluginTab();

            this.combobox_window_capture_mode.Items.AddRange(new object[] { WindowCaptureMode.Auto, WindowCaptureMode.Screen, WindowCaptureMode.GDI });
            if (DWM.isDWMEnabled())
            {
                this.combobox_window_capture_mode.Items.Add(WindowCaptureMode.Aero);
                this.combobox_window_capture_mode.Items.Add(WindowCaptureMode.AeroTransparent);
            }
            UpdateUI();
            DisplaySettings();
        }

        private void AddPluginTab()
        {
            if (PluginHelper.instance.HasPlugins())
            {
                this.tabcontrol.TabPages.Add(tab_plugins);
                // Draw the Plugin listview
                listview_plugins.BeginUpdate();
                listview_plugins.Items.Clear();
                listview_plugins.Columns.Clear();
                string[] columns = { "Name", "Version", "DLL Path" };
                foreach (string column in columns)
                {
                    listview_plugins.Columns.Add(column);
                }
                PluginHelper.instance.FillListview(this.listview_plugins);
                // Maximize Column size!
                for (int i = 0; i < listview_plugins.Columns.Count; i++)
                {
                    listview_plugins.AutoResizeColumn(i, ColumnHeaderAutoResizeStyle.ColumnContent);
                    int width = listview_plugins.Columns[i].Width;
                    listview_plugins.AutoResizeColumn(i, ColumnHeaderAutoResizeStyle.HeaderSize);
                    if (width > listview_plugins.Columns[i].Width)
                    {
                        listview_plugins.Columns[i].Width = width;
                    }
                }
                listview_plugins.EndUpdate();
                listview_plugins.Refresh();

                // Disable the configure button, it will be enabled when a plugin is selected AND isConfigurable
                button_pluginconfigure.Enabled = false;
            }
        }

        private void BtnPatternHelpClick(object sender, EventArgs e)
        {
            MessageBox.Show(lang.GetString(LangKey.settings_message_filenamepattern), lang.GetString(LangKey.settings_filenamepattern));
        }

        private void Button_pluginconfigureClick(object sender, EventArgs e)
        {
            PluginHelper.instance.ConfigureSelectedItem(listview_plugins);
        }

        private void Combobox_window_capture_modeSelectedIndexChanged(object sender, EventArgs e)
        {
            int windowsVersion = Environment.OSVersion.Version.Major;
            string modeText = combobox_window_capture_mode.Text;
            string dwmMode = WindowCaptureMode.Aero.ToString();
            string autoMode = WindowCaptureMode.Auto.ToString();
            if (modeText.Equals(dwmMode, StringComparison.CurrentCultureIgnoreCase)
                || (modeText.Equals(autoMode, StringComparison.CurrentCultureIgnoreCase) && windowsVersion >= 6))
            {
                colorButton_window_background.Visible = true;
            }
            else
            {
                colorButton_window_background.Visible = false;
            }
        }

        private void DisplaySettings()
        {
            colorButton_window_background.SelectedColor = coreConfiguration.DWMBackgroundColor;

            if (!DWM.isDWMEnabled())
            {
                // Remove DWM from configuration, as DWM is disabled!
                if (coreConfiguration.WindowCaptureMode == WindowCaptureMode.Aero || coreConfiguration.WindowCaptureMode == WindowCaptureMode.AeroTransparent)
                {
                    coreConfiguration.WindowCaptureMode = WindowCaptureMode.GDI;
                }
            }
            combobox_window_capture_mode.SelectedItem = coreConfiguration.WindowCaptureMode;

            checkbox_playsound.Checked = coreConfiguration.PlayCameraSound;

            checkboxPrintInverted.Checked = coreConfiguration.OutputPrintInverted;
            checkboxAllowCenter.Checked = coreConfiguration.OutputPrintCenter;
            checkboxAllowEnlarge.Checked = coreConfiguration.OutputPrintAllowEnlarge;
            checkboxAllowRotate.Checked = coreConfiguration.OutputPrintAllowRotate;
            checkboxAllowShrink.Checked = coreConfiguration.OutputPrintAllowShrink;
            checkboxTimestamp.Checked = coreConfiguration.OutputPrintTimestamp;
            checkbox_alwaysshowprintoptionsdialog.Checked = coreConfiguration.OutputPrintPromptOptions;
            checkbox_capture_mousepointer.Checked = coreConfiguration.CaptureMousepointer;
            checkbox_capture_windows_interactive.Checked = coreConfiguration.CaptureWindowsInteractive;

            numericUpDownWaitTime.Value = coreConfiguration.CaptureDelay >= 0 ? coreConfiguration.CaptureDelay : 0;
        }

        private void Listview_pluginsSelectedIndexChanged(object sender, EventArgs e)
        {
            button_pluginconfigure.Enabled = PluginHelper.instance.isSelectedItemConfigurable(listview_plugins);
        }

        private void SaveSettings()
        {
            coreConfiguration.WindowCaptureMode = (WindowCaptureMode)combobox_window_capture_mode.SelectedItem;
            coreConfiguration.PlayCameraSound = checkbox_playsound.Checked;

            List<Destination> destinations = new List<Destination>();
            coreConfiguration.OutputDestinations = destinations;

            coreConfiguration.OutputPrintInverted = checkboxPrintInverted.Checked;
            coreConfiguration.OutputPrintCenter = checkboxAllowCenter.Checked;
            coreConfiguration.OutputPrintAllowEnlarge = checkboxAllowEnlarge.Checked;
            coreConfiguration.OutputPrintAllowRotate = checkboxAllowRotate.Checked;
            coreConfiguration.OutputPrintAllowShrink = checkboxAllowShrink.Checked;
            coreConfiguration.OutputPrintTimestamp = checkboxTimestamp.Checked;
            coreConfiguration.OutputPrintPromptOptions = checkbox_alwaysshowprintoptionsdialog.Checked;
            coreConfiguration.CaptureMousepointer = checkbox_capture_mousepointer.Checked;
            coreConfiguration.CaptureWindowsInteractive = checkbox_capture_windows_interactive.Checked;
            coreConfiguration.CaptureDelay = (int)numericUpDownWaitTime.Value;
            coreConfiguration.DWMBackgroundColor = colorButton_window_background.SelectedColor;

            IniConfig.Save();

            // Make sure the current language & settings are reflected in the Main-context menu
            MainForm.instance.UpdateUI();
        }

        private void Settings_cancelClick(object sender, System.EventArgs e)
        {
            this.Close();
        }

        private void settings_okay_Click(object sender, EventArgs e)
        {
            SaveSettings();
            this.Close();
        }

        private void UpdateUI()
        {
            this.Text = lang.GetString(LangKey.settings_title);

            this.tab_printer.Text = lang.GetString(LangKey.settings_printer);
            this.tab_capture.Text = lang.GetString(LangKey.settings_capture);
            this.tab_plugins.Text = lang.GetString(LangKey.settings_plugins);

            this.groupbox_windowscapture.Text = lang.GetString(LangKey.settings_windowscapture);
            this.label_window_capture_mode.Text = lang.GetString(LangKey.settings_window_capture_mode);

            this.groupbox_capture.Text = lang.GetString(LangKey.settings_capture);
            this.checkbox_capture_mousepointer.Text = lang.GetString(LangKey.settings_capture_mousepointer);
            this.checkbox_capture_windows_interactive.Text = lang.GetString(LangKey.settings_capture_windows_interactive);
            this.label_waittime.Text = lang.GetString(LangKey.settings_waittime);

            this.checkbox_playsound.Text = lang.GetString(LangKey.settings_playsound);

            this.groupbox_printoptions.Text = lang.GetString(LangKey.settings_printoptions);
            this.checkboxAllowCenter.Text = lang.GetString(LangKey.printoptions_allowcenter);
            this.checkboxAllowEnlarge.Text = lang.GetString(LangKey.printoptions_allowenlarge);
            this.checkboxAllowRotate.Text = lang.GetString(LangKey.printoptions_allowrotate);
            this.checkboxAllowShrink.Text = lang.GetString(LangKey.printoptions_allowshrink);
            this.checkboxTimestamp.Text = lang.GetString(LangKey.printoptions_timestamp);
            this.checkboxPrintInverted.Text = lang.GetString(LangKey.printoptions_inverted);
            this.checkbox_alwaysshowprintoptionsdialog.Text = lang.GetString(LangKey.settings_alwaysshowprintoptionsdialog);
        }
    }
}
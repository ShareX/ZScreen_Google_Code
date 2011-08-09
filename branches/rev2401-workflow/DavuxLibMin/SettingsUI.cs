/*
 * Author: David Amenta
 * Release Date: 12/12/2009
 * License: Free for any use.
 */

using System.Windows.Forms;

namespace DavuxLib
{
    public class SettingsUI
    {
        // this allows the caller to mutate the Tag that describes the config.
        // example:  Use %|Value to refer to the name of an object in the current view.
        // the caller will be asked for each setting to mutate and return the value.
        public delegate string SettingStringFound(string settingString);
        public event SettingStringFound OnSettingFound;

        /// <summary>
        /// Load the control values from the Settings object.
        /// </summary>
        /// <param name="Controls">Collection of controls to load settings for.</param>
        public void LoadControlData(Control.ControlCollection Controls)
        {
            foreach (Control c in Controls)
            {
                ParseControl(c, false);
            }
        }

        /// <summary>
        /// Save the state of supported controls with a valid Tag.
        /// </summary>
        /// <param name="Controls">Collection of controls to save settings.</param>
        public void SaveControlData(Control.ControlCollection Controls)
        {
            foreach (Control c in Controls)
            {
                ParseControl(c, true);
            }
            Settings.Save();
        }

        private void ControlFound(Control c, bool Saving)
        {
            string tag = c.Tag as string;

            if (!string.IsNullOrEmpty(tag))
            {
                if (OnSettingFound != null)
                {
                    tag = OnSettingFound.Invoke(tag);
                }
                // TextBox configuration.  (Text)
                if (c as TextBox != null)
                {
                    TextBox t = c as TextBox;
                    if (Saving)
                    {
                        Settings.Set(tag, t.Text);
                    }
                    else
                    {
                        t.Text = Settings.Get(tag, "", "", false);
                    }
                }
                // TrackBar configuration.  (Value)
                if (c as TrackBar != null)
                {
                    TrackBar t = c as TrackBar;
                    if (Saving)
                    {
                        Settings.Set(tag, t.Value);
                    }
                    else
                    {
                        t.Value = Settings.Get(tag, t.Value);
                    }
                }
                // CheckBox configuration.  (Checked)
                if (c as CheckBox != null)
                {
                    CheckBox t = c as CheckBox;
                    if (Saving)
                    {
                        Settings.Set(tag, t.Checked);
                    }
                    else
                    {
                        t.Checked = Settings.Get(tag, false);
                    }
                }

                // Add new controls here.
            }
        }

        private void ParseControl(Control c, bool saving)
        {
            if (c.Tag != null)
            {
                ControlFound(c, saving);
            }

            foreach (Control cx in c.Controls)
            {
                ParseControl(cx, saving);
            }
        }
    }
}
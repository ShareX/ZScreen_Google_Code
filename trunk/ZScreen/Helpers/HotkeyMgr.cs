using System.Windows.Forms;
using ZScreenLib;

namespace ZScreenGUI
{
    public class HotkeyMgr
    {
        public static int mHKSelectedRow = -1;

        public DataGridView dgvHotkeys { get; set; }
        public Label lblHotkeyStatus { get; set; }

        public string[] HotkeyNames = new string[]
        {
            "Entire Screen",
            "Active Window",
            "Crop Shot",
            "Selected Window",
            "Clipboard Upload",
            "Freehand Crop Shot",
            "Last Crop Shot",
            "Auto Capture",
            "Actions Toolbar",
            "Quick Options",
            "Drop Window",
            "Language Translator",
            "Screen Color Picker",
            "Twitter Client"
        };

        public HotkeyMgr(ref DataGridView dgv, ref Label info)
        {
            this.dgvHotkeys = dgv;
            this.lblHotkeyStatus = info;
        }

        public void UpdateHotkeysDGV()
        {
            UpdateHotkeysDGV(false);
        }

        private void UpdateHotkeysDGV(bool resetKeys)
        {
            dgvHotkeys.Rows.Clear();

            foreach (string name in HotkeyNames)
            {
                AddHotkey(name, resetKeys);
            }

            dgvHotkeys.Refresh();
        }

        public void ResetHotkeys()
        {
            int index = 0;
            foreach (string descr in HotkeyNames)
            {
                object dfltHotkey = Engine.conf.GetFieldValue("DefaultHotkey" + descr.Replace(" ", string.Empty));
                SetHotkey(index++, (Keys)dfltHotkey);
            }
            UpdateHotkeysDGV(true);
        }

        private void AddHotkey(string descr, bool resetKeys)
        {
            object dfltHotkey = Engine.conf.GetFieldValue("DefaultHotkey" + descr.Replace(" ", string.Empty));

            if (!resetKeys)
            {
                object userHotKey = Engine.conf.GetFieldValue("Hotkey" + descr.Replace(" ", string.Empty));
                if (userHotKey != null && userHotKey.GetType() == typeof(Keys))
                {
                    dgvHotkeys.Rows.Add(descr, ((Keys)userHotKey).ToSpecialString(), ((Keys)dfltHotkey).ToSpecialString());
                }
            }
            else
            {
                dgvHotkeys.Rows.Add(descr, ((Keys)dfltHotkey).ToSpecialString(), ((Keys)dfltHotkey).ToSpecialString());
            }
        }

        /// <summary>
        /// Sets the Hotkey of the Active Cell of the DataGridView
        /// </summary>
        /// <param name="key"></param>
        public void SetHotkey(Keys key)
        {
            SetHotkey(mHKSelectedRow, key);
        }

        public void SetHotkey(int row, Keys key)
        {
            dgvHotkeys.Rows[row].Cells[1].Value = key.ToSpecialString();
            lblHotkeyStatus.Text = dgvHotkeys.Rows[row].Cells[0].Value + " Hotkey set to: " + key.ToSpecialString() + ". Press enter when done setting all desired Hotkeys.";
            SaveHotkey(dgvHotkeys.Rows[row].Cells[0].Value.ToString(), key);
        }

        public static bool SaveHotkey(string name, Keys key)
        {
            return Engine.conf.SetFieldValue("Hotkey" + name.Replace(" ", string.Empty), key);
        }
    }
}
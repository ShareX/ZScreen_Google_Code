using System;
using System.Windows.Forms;
using HelpersLib;
using ZScreenLib;

namespace ZScreenGUI
{
    public class HotkeyMgr
    {
        public static int mHKSelectedRow = -1;

        public DataGridView dgvHotkeys { get; set; }

        public Label lblHotkeyStatus { get; set; }

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

            foreach (HotkeyTask hk in Enum.GetValues(typeof(HotkeyTask)))
            {
                AddHotkey(hk, resetKeys);
            }

            dgvHotkeys.Refresh();
        }

        public void ResetHotkeys()
        {
            int index = 0;
            foreach (HotkeyTask hk in Enum.GetValues(typeof(HotkeyTask)))
            {
                object dfltHotkey = Engine.conf.GetFieldValue("DefaultHotkey" + hk.ToString().Replace(" ", string.Empty));
                SetHotkey(index++, (Keys)dfltHotkey);
            }
            UpdateHotkeysDGV(true);
        }

        private void AddHotkey(HotkeyTask hotkeyEnum, bool resetKeys)
        {
            object dfltHotkey = Engine.conf.GetFieldValue("DefaultHotkey" + hotkeyEnum.ToString());

            if (!resetKeys)
            {
                object userHotKey = Engine.conf.GetFieldValue("Hotkey" + hotkeyEnum.ToString());
                if (userHotKey != null && userHotKey.GetType() == typeof(Keys))
                {
                    dgvHotkeys.Rows.Add(hotkeyEnum.GetDescription(), ((Keys)userHotKey).ToSpecialString(), ((Keys)dfltHotkey).ToSpecialString());
                }
            }
            else
            {
                dgvHotkeys.Rows.Add(hotkeyEnum, ((Keys)dfltHotkey).ToSpecialString(), ((Keys)dfltHotkey).ToSpecialString());
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
            // TODO: Unregister old hotkey
            dgvHotkeys.Rows[row].Cells[1].Value = key.ToSpecialString();
            // TODO: Register new key
            lblHotkeyStatus.Text = dgvHotkeys.Rows[row].Cells[0].Value + " Hotkey set to: " + key.ToSpecialString() + ". Press enter when done setting all desired Hotkeys.";
            SaveHotkey(dgvHotkeys.Rows[row].Cells[0].Value.ToString(), key);
        }

        public static bool SaveHotkey(string name, Keys key)
        {
            return Engine.conf.SetFieldValue("Hotkey" + name.Replace(" ", string.Empty), key);
        }
    }
}
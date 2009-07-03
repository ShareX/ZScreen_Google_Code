using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ZSS.UserControls
{
    public partial class AccountsControl : UserControl
    {
        public AccountsControl()
        {
            InitializeComponent();
        }

        public virtual bool RemoveItem(int sel)
        {
            if (sel != -1)
            {
                this.AccountsList.Items.RemoveAt(sel);
                if (this.AccountsList.Items.Count > 0)
                {
                    this.AccountsList.SelectedIndex = (sel > 0) ? (sel - 1) : 0;
                }
                return true;
            }
            return false;
        }

        private void SettingsGrid_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            if (this.AccountsList.SelectedIndex > -1)
            {
                this.AccountsList.Items[this.AccountsList.SelectedIndex] = SettingsGrid.SelectedObject;
            }
        }
    }
}
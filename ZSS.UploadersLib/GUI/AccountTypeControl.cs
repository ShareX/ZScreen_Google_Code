using System;
using System.ComponentModel;
using System.Windows.Forms;
using HelpersLib;

namespace UploadersLib.GUI
{
    [DefaultEvent("AccountTypeChanged")]
    public partial class AccountTypeControl : UserControl
    {
        public delegate void AccountTypeChangedEventHandler(AccountType accountType);
        public event AccountTypeChangedEventHandler AccountTypeChanged;

        public AccountType SelectedAccountType
        {
            get
            {
                return (AccountType)cbAccountType.SelectedIndex.Between(0, 1);
            }
            set
            {
                cbAccountType.SelectedIndex = (int)value;
            }
        }

        public AccountTypeControl()
        {
            InitializeComponent();
            cbAccountType.SelectedIndexChanged += new EventHandler(cbAccountType_SelectedIndexChanged);
        }

        private void cbAccountType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (AccountTypeChanged != null)
            {
                AccountTypeChanged(SelectedAccountType);
            }
        }
    }
}
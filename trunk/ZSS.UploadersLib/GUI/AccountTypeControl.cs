using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace UploadersLib.GUI
{
    [DefaultEvent("AccountTypeChanged")]
    public partial class AccountTypeControl : UserControl
    {
        public event EventHandler AccountTypeChanged;

        public AccountType SelectedAccountType
        {
            get
            {
                return (AccountType)cbAccountType.SelectedIndex;
            }
            set
            {
                cbAccountType.SelectedIndex = (int)value;
            }
        }

        public AccountTypeControl()
        {
            InitializeComponent();
            cbAccountType.SelectedIndexChanged += AccountTypeChanged;
        }
    }
}
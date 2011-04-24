using System;
using System.Windows.Forms;

namespace ZUploader
{
    public partial class InputBox : Form
    {
        public string InputText { get; private set; }

        public InputBox()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            InputText = tbInput.Text;
            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
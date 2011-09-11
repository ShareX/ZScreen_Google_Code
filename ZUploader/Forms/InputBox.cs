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

        public static string GetInputText()
        {
            using (InputBox form = new InputBox())
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    return form.InputText;
                }

                return null;
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            InputText = tbInput.Text;
            DialogResult = DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
    }
}
using System;
using System.Windows.Forms;

namespace UploadersLib.GUI
{
    public partial class EmailForm : Form
    {
        public string ToEmail { get; private set; }
        public string Subject { get; private set; }
        public string Body { get; private set; }

        public EmailForm()
        {
            InitializeComponent();
        }

        public EmailForm(string toEmail, string subject, string body)
            : this()
        {
            txtToEmail.Text = toEmail;
            txtSubject.Text = subject;
            txtMessage.Text = body;
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            ToEmail = txtToEmail.Text;
            Subject = txtSubject.Text;
            Body = txtMessage.Text;
            DialogResult = DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
    }
}
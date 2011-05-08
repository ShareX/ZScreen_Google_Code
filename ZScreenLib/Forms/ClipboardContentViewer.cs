using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ZScreenLib
{
    public partial class ClipboardContentViewer : Form
    {
        public ClipboardContentViewer()
        {
            InitializeComponent();
        }

        private void ClipboardContentViewer_Load(object sender, EventArgs e)
        {
            if (Clipboard.ContainsImage())
            {
                pbClipboard.Image = Clipboard.GetImage();
                pbClipboard.Visible = true;
                txtClipboard.Visible = false;
            }
            else if (Clipboard.ContainsText())
            {
                txtClipboard.Text = Clipboard.GetText();
                pbClipboard.Visible = false;
                txtClipboard.Visible = true;
            }
            else
            {
                lblQuestion.Text = "Your clipboard contains a file.";
                pbClipboard.Visible = false;
                txtClipboard.Visible = false;
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }
    }
}

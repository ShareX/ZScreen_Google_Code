using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace JBirdGUI
{
    public partial class JBirdToolbar : JBirdCoreUI
    {
        public JBirdToolbar()
        {
            InitializeComponent();
        }

        private void tsbCaptureScreen_Click(object sender, EventArgs e)
        {
            base.btnCaptureScreen_Click(sender, e);
        }

        private void tsbCaptureActiveWindow_Click(object sender, EventArgs e)
        {
            base.btnCaptureActiveWindow_Click(sender, e);
        }

        private void tsbCaptureWindow_Click(object sender, EventArgs e)
        {

        }

        private void tsbExit_Click(object sender, EventArgs e)
        {
            base.Close();
        }
    }
}

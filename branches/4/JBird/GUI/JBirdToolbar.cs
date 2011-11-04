using System;

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
using System;
using System.Windows.Forms;

namespace JBirdGUI
{
    public partial class JBirdMain : JBirdCoreUI
    {
        public JBirdMain()
        {
            InitializeComponent();
        }

        private void JBirdMain_Load(object sender, EventArgs e)
        {
            base.JBirdCoreUI_Load(sender, e);
        }

        private void JBirdMain_Shown(object sender, EventArgs e)
        {
            base.JBirdCoreUI_Shown(sender, e);
        }

        private void JBirdMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            base.JBirdCoreUI_FormClosing(sender, e);
        }

        protected override void btnCaptureScreen_Click(object sender, EventArgs e)
        {
            base.btnCaptureScreen_Click(sender, e);
        }

        protected override void btnCaptureActiveWindow_Click(object sender, EventArgs e)
        {
            base.btnCaptureActiveWindow_Click(sender, e);
        }

        protected override void btnWorkflows_Click(object sender, EventArgs e)
        {
            base.btnWorkflows_Click(sender, e);
        }
    }
}
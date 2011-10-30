using System;
using System.Windows.Forms;

namespace ZScreenGUI
{
    public partial class ZScreenOptionsCoreUI : Form
    {
        public ZScreenOptionsCoreUI()
        {
            InitializeComponent();
        }

        private void ZScreenOptions_Load(object sender, EventArgs e)
        {
            if (tvOptions.Nodes.Count == tcMain.TabPages.Count)
            {
                for (int i = 0; i < tvOptions.Nodes.Count; i++)
                {
                    tcMain.TabPages[i].Text = tvOptions.Nodes[i].Text;
                }
            }
        }
    }
}
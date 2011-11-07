using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace HelpersLib
{
    public partial class OptionsCoreUI : Form
    {
        List<TabPage> TabPages = new List<TabPage>();

        public OptionsCoreUI()
        {
            InitializeComponent();
        }

        private void ZScreenOptionsCoreUI_Load(object sender, EventArgs e)
        {
            if (tvOptions.Nodes.Count == tcMain.TabPages.Count)
            {
                for (int i = 0; i < tvOptions.Nodes.Count; i++)
                {
                    tcMain.TabPages[i].Text = tvOptions.Nodes[i].Text;
                    TabPages.Add(tcMain.TabPages[i]);
                }
                tvOptions.NodeMouseClick += new TreeNodeMouseClickEventHandler(tvOptions_NodeMouseClick);
            }
        }

        private void tvOptions_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            int id = e.Node.Index;
            tcMain.TabPages.Clear();
            tcMain.TabPages.Add(TabPages[id]);
        }
    }
}
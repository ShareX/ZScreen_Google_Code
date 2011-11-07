using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using ZScreenLib;

namespace ZScreenGUI
{
    public partial class ZScreenOptionsUI : Form
    {
        private void AddNodesToList(TreeNodeCollection nodes)
        {
            for (int i = 0; i < nodes.Count; i++)
            {
                Nodes.Add(nodes[i]);
                if (nodes[i].Nodes.Count > 0)
                {
                    AddNodesToList(nodes[i].Nodes);
                }
            }
        }

        List<TreeNode> Nodes = new List<TreeNode>();
        List<TabPage> TabPages = new List<TabPage>();

        private void tvOptions_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            int id = e.Node.Index;
            tcMain.TabPages.Clear();
            var tabPage = TabPages.Where(x => x.Text == e.Node.Text);
            if (tabPage.Count() > 0)
            {
                tcMain.TabPages.Add(tabPage.First<TabPage>());
            }
        }

        private XMLSettings Config = null;

        private void ZScreenOptionsCoreUI_Load(object sender, EventArgs e)
        {
            this.Text = Application.ProductName + " - Options";

            tvOptions.ExpandAll();

            AddNodesToList(tvOptions.Nodes);

            for (int i = 0; i < Nodes.Count; i++)
            {
                TabPages.Add(tcMain.TabPages[i]);
                TabPages[i].Text = Nodes[i].Text;
            }

            tvOptions.NodeMouseClick += new TreeNodeMouseClickEventHandler(tvOptions_NodeMouseClick);

            tcMain.TabPages.Clear();
            tcMain.TabPages.Add(TabPages[0]);

            tvOptions_NodeMouseClick(sender, new TreeNodeMouseClickEventArgs(tvOptions.Nodes[0], System.Windows.Forms.MouseButtons.Left, 1, Cursor.Position.X, Cursor.Position.Y));
        }

        public ZScreenOptionsUI(XMLSettings config)
        {
            InitializeComponent();

            this.Config = config;

            pgIndexer.SelectedObject = config.IndexerConfig;
        }

        private void ZScreenOptionsUI_Resize(object sender, EventArgs e)
        {
            this.Refresh();
        }

        private void ZScreenOptionsUI_Shown(object sender, EventArgs e)
        {
        }
    }
}
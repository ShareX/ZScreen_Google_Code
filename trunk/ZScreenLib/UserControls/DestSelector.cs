using System.Windows.Forms;
using UploadersAPILib;
using UploadersLib;
using System;

namespace ZScreenLib
{
    public partial class DestSelector : UserControl
    {
        public DestSelector()
        {
            InitializeComponent();
        }

        private void LoadDest<T>(ToolStripDropDownButton tsddb)
        {
            if (tsddb.DropDownItems.Count == 0)
            {
                foreach (Enum t in Enum.GetValues(typeof(T)))
                {
                    ToolStripMenuItem tsmi = new ToolStripMenuItem(((Enum)t).GetDescription());
                    tsmi.Tag = t;
                }
            }
        }

        private void tsbDestConfig_Click(object sender, System.EventArgs e)
        {
            new UploadersConfigForm(Engine.MyUploadersConfig, ZKeys.GetAPIKeys()).Show();
        }

        private void tsddDestLinks_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            for (int i = 0; i < tsddDestLink.DropDownItems.Count; i++)
            {
                ToolStripMenuItem tsmi = (ToolStripMenuItem)tsddDestLink.DropDownItems[i];
                tsmi.Checked = tsmi == e.ClickedItem && !((ToolStripMenuItem)e.ClickedItem).Checked;
            }
        }
    }
}
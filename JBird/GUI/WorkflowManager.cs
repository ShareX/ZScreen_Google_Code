using System;
using System.Collections.Generic;
using System.Windows.Forms;
using HelpersLib;
using JBirdGUI;

namespace ZScreenLib
{
    public partial class WorkflowManager : Form
    {
        private List<Workflow> Workflows = null;
        private bool GuiReady = false;

        public WorkflowManager(List<Workflow> profiles)
        {
            InitializeComponent();
            Workflows = profiles;
        }

        private void WorkflowManager_Load(object sender, EventArgs e)
        {
            this.Text = Application.ProductName + " - Workflow Manager";
            WorkflowsGuiRefresh();
        }

        public void WorkflowsGuiRefresh()
        {
            lvWorkflows.Items.Clear();
            int count = 0;
            foreach (Workflow wf in Workflows)
            {
                lvWorkflows.Items.Add(WorkFlowToListViewItem(count++, wf));
            }
            Program.HotkeysUpdate();
        }

        private ListViewItem WorkFlowToListViewItem(int id, Workflow wf)
        {
            ListViewItem lvi = new ListViewItem();
            lvi.Checked = wf.Enabled;
            lvi.Text = wf.Description;
            lvi.SubItems.Add(wf.Job.GetDescription());
            lvi.SubItems.Add(wf.Hotkey.ToString());
            lvi.Tag = id;
            return lvi;
        }

        private void btnProfileCreate_Click(object sender, EventArgs e)
        {
            WorkflowWizard pw = new WorkflowWizard("Create") { Icon = this.Icon };

            if (pw.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Workflows.Add(pw.Workflow);
                WorkflowsGuiRefresh();
            }
        }

        private void btnProfileEdit_Click(object sender, EventArgs e)
        {
            if (lvWorkflows.SelectedItems.Count > 0)
            {
                ListViewItem lvi = lvWorkflows.SelectedItems[0];
                WorkflowEdit(lvi);
            }
        }

        private void lvProfiles_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (lvWorkflows.SelectedItems.Count > 0)
            {
                ListViewItem lvi = lvWorkflows.GetItemAt(e.X, e.Y);
                WorkflowEdit(lvi);
            }
        }

        private void WorkflowEdit(ListViewItem lvi)
        {
            Workflow wf = Workflows[(int)lvi.Tag];
            WorkflowWizard pw = new WorkflowWizard("Edit", wf) { Icon = this.Icon };
            if (pw.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                lvi.Tag = pw.Workflow;
                WorkflowsGuiRefresh();
            }
        }

        private void btnProfileDelete_Click(object sender, EventArgs e)
        {
            if (lvWorkflows.SelectedItems.Count > 0)
            {
                ListViewItem lvi = lvWorkflows.SelectedItems[0];
                Workflows.RemoveAt((int)lvi.Tag);
                lvWorkflows.Items.Remove(lvi);
            }
        }

        private void btnProfileDuplicate_Click(object sender, EventArgs e)
        {
            if (lvWorkflows.SelectedItems.Count > 0)
            {
                ListViewItem lvi = lvWorkflows.SelectedItems[0];
                Workflow wf = Workflows[(int)lvi.Tag];
                IClone cm = new CloneManager();
                Workflow wf2 = cm.Clone<Workflow>(wf);
                wf2.Description = wf.Description + " - Copy";
                Workflows.Add(wf2);
                WorkflowsGuiRefresh();
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void lvWorkflows_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            if (GuiReady)
            {
                ListViewItem lvi = e.Item;
                Workflows[(int)lvi.Tag].Enabled = lvi.Checked;
            }
        }

        private void ProfileManager_Shown(object sender, EventArgs e)
        {
            GuiReady = true;
        }
    }
}
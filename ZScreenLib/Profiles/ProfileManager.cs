using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ZScreenLib
{
    public partial class ProfileManager : Form
    {
        private List<Workflow> Profiles = null;

        public ProfileManager(List<Workflow> profiles)
        {
            InitializeComponent();
            Profiles = profiles;
        }

        private void ProfileManager_Load(object sender, EventArgs e)
        {
            this.Text = Application.ProductName + " - Workflows";
            ProfilesGuiRefresh();
        }

        public void ProfilesGuiRefresh()
        {
            lvProfiles.Items.Clear();
            foreach (Workflow p in Profiles)
            {
                ListViewItem lvi = new ListViewItem();
                lvi.Text = p.Description;
                lvi.SubItems.Add(p.Job.GetDescription());
                lvi.SubItems.Add(string.Empty);
                lvi.SubItems.Add(p.Enabled.ToString());
                lvi.Tag = p;
                lvProfiles.Items.Add(lvi);
            }
        }

        private void btnProfileCreate_Click(object sender, EventArgs e)
        {
            ProfileWizard pw = new ProfileWizard() { Icon = this.Icon };
            if (pw.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Profiles.Add(pw.Workflow);
                ProfilesGuiRefresh();
            }
        }

        private void btnProfileEdit_Click(object sender, EventArgs e)
        {
            if (lvProfiles.SelectedItems.Count > 0)
            {
                ListViewItem lvi = lvProfiles.SelectedItems[0];
                ProfileEdit(lvi);
            }
        }

        private void lvProfiles_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (lvProfiles.SelectedItems.Count > 0)
            {
                ListViewItem lvi = lvProfiles.GetItemAt(e.X, e.Y);
                ProfileEdit(lvi);
            }
        }

        private void ProfileEdit(ListViewItem lvi)
        {
            Workflow p = lvi.Tag as Workflow;
            ProfileWizard pw = new ProfileWizard(p) { Icon = this.Icon };
            if (pw.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                lvi.Tag = pw.Workflow;
                ProfilesGuiRefresh();
            }
        }
    }
}
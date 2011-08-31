using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HelpersLib;

namespace ZScreenLib
{
    public partial class ProfileManager : Form
    {
        private List<Profile> Profiles = null;

        public ProfileManager(List<Profile> profiles)
        {
            InitializeComponent();
            Profiles = profiles;
        }

        private void ProfileManager_Load(object sender, EventArgs e)
        {
            this.Text = Application.ProductName + " - Profiles";
            ProfilesGuiRefresh();
        }

        public void ProfilesGuiRefresh()
        {
            lvProfiles.Items.Clear();
            foreach (Profile p in Profiles)
            {
                ListViewItem lvi = new ListViewItem();
                lvi.Text = p.Name;
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
                Profiles.Add(pw.Profile);
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
            Profile p = lvi.Tag as Profile;
            ProfileWizard pw = new ProfileWizard(p) { Icon = this.Icon };
            if (pw.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                lvi.Tag = pw.Profile;
                ProfilesGuiRefresh();
            }
        }
    }
}

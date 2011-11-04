using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HelpersLib;
using HelpersLib.Properties;

namespace ZScreenLib
{
    public partial class ActionsUI : Form
    {
        private ActionsConfig Config;

        public ActionsUI(ActionsConfig config)
        {
            InitializeComponent();
            Config = config;
        }

        private void ActionsUI_Load(object sender, EventArgs e)
        {
            lbSoftware.Items.Clear();

            foreach (Software app in Config.ActionsApps)
            {
                if (!String.IsNullOrEmpty(app.Name))
                {
                    lbSoftware.Items.Add(app.Name, app.Enabled);
                }
            }

            int i;
            if (Config.ImageEditor != null && (i = lbSoftware.Items.IndexOf(Config.ImageEditor.Name)) != -1)
            {
                lbSoftware.SelectedIndex = i;
            }
            else if (lbSoftware.Items.Count > 0)
            {
                lbSoftware.SelectedIndex = 0;
            }
        }

        private void AddImageSoftwareToList(Software temp)
        {
            if (temp != null)
            {
                Config.ActionsApps.Add(temp);
                lbSoftware.Items.Add(temp);
                lbSoftware.SelectedIndex = lbSoftware.Items.Count - 1;
            }
        }

        /// <summary>
        /// Browse for an applicatoin
        /// </summary>
        /// <returns>Software</returns>
        private Software BrowseApplication()
        {
            Software temp = null;

            using (var dlg = new OpenFileDialog())
            {
                dlg.Filter = StaticHelper.FILTER_EXE_FILES;

                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    temp = new Software { Name = Path.GetFileNameWithoutExtension(dlg.FileName), Path = dlg.FileName };
                }
            }

            return temp;
        }

        private void btnAddImageSoftware_Click(object sender, EventArgs e)
        {
            Software temp = BrowseApplication();
            if (temp != null)
            {
                AddImageSoftwareToList(temp);
            }
        }

        private void btnDeleteImageSoftware_Click(object sender, EventArgs e)
        {
            int sel = lbSoftware.SelectedIndex;

            if (sel != -1)
            {
                Config.ActionsApps.RemoveAt(sel);

                lbSoftware.Items.RemoveAt(sel);

                if (lbSoftware.Items.Count > 0)
                {
                    lbSoftware.SelectedIndex = (sel > 0) ? (sel - 1) : 0;
                }
            }
        }

        /// <summary>
        /// Searches for an Image Software in settings and returns it
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        private Software GetImageSoftware(string name)
        {
            foreach (Software app in Config.ActionsApps)
            {
                if (app != null && app.Name != null)
                {
                    if (app.Name.Equals(name))
                    {
                        return app;
                    }
                }
            }

            return null;
        }

        private void lbSoftware_SelectedIndexChanged(object sender, EventArgs e)
        {
            ShowImageEditorsSettings();
        }

        private void lbSoftwareItemCheck(object sender, ItemCheckEventArgs e)
        {
            UpdateGuiEditors(sender);
        }

        private void pgEditorsImage_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            Software temp = Config.ActionsApps[lbSoftware.SelectedIndex];
            lbSoftware.Items[lbSoftware.SelectedIndex] = temp;
            Config.ActionsApps[lbSoftware.SelectedIndex] = temp;
        }

        private void SetActiveImageSoftware()
        {
            Config.ImageEditor = Config.ActionsApps[lbSoftware.SelectedIndex];
        }

        private void ShowImageEditorsSettings()
        {
            if (lbSoftware.SelectedItem != null)
            {
                Software app = GetImageSoftware(lbSoftware.SelectedItem.ToString());
                if (app != null)
                {
                    Config.ActionsApps[lbSoftware.SelectedIndex].Enabled =
                        lbSoftware.GetItemChecked(lbSoftware.SelectedIndex);
                    pgEditorsImage.SelectedObject = app;
                    pgEditorsImage.Enabled = !app.Protected;
                    btnActionsRemove.Enabled = !app.Protected;
                    SetActiveImageSoftware();
                }
            }
        }

        private void UpdateGuiEditors(object sender)
        {
            if (sender.GetType() == lbSoftware.GetType())
            {
                // the checked state needs to be inversed for some weird reason to get it working properly
                if (Config.ActionsApps.HasValidIndex(lbSoftware.SelectedIndex))
                {
                    Config.ActionsApps[lbSoftware.SelectedIndex].Enabled =
                        !lbSoftware.GetItemChecked(lbSoftware.SelectedIndex);
                }
            }
            else if (sender.GetType() == typeof(ToolStripMenuItem))
            {
                var tsm = sender as ToolStripMenuItem;
                var sel = (int)tsm.Tag;
                if (Config.ActionsApps.HasValidIndex(sel))
                {
                    Config.ActionsApps[sel].Enabled = tsm.Checked;
                    lbSoftware.SetItemChecked(lbSoftware.SelectedIndex, tsm.Checked);
                }
            }
        }
    }
}
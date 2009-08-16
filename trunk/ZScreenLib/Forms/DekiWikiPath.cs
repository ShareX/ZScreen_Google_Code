#region License Information (GPL v2)
/*
    ZScreen - A program that allows you to upload screenshots in one keystroke.
    Copyright (C) 2008-2009  Brandon Zimmerman

    This program is free software; you can redistribute it and/or
    modify it under the terms of the GNU General Public License
    as published by the Free Software Foundation; either version 2
    of the License, or (at your option) any later version.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with this program; if not, write to the Free Software
    Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301, USA.
    
    Optionally you can also view the license at <http://www.gnu.org/licenses/>.
*/
#endregion

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Net;
using ZSS;
using UploadersLib.Helpers;

namespace ZScreenLib
{
    public partial class DekiWikiPath : Form
    {
        public DekiWikiOptions Options { get; set; }
        public string path { set; get; }
        private string currentPath;
        private DekiWiki connector;

        public List<DekiWikiHistory> history = new List<DekiWikiHistory>();

        public DekiWikiPath(DekiWikiOptions options)
        {
            InitializeComponent();

            this.Options = options;

            connector = new DekiWiki(options);

            lblAccount.Text = this.Options.Account.Name;

            try
            {
                connector.Login();
            }
            catch
            {
                throw new Exception("Could not log in to DekiWiki.");
            }
        }

        private void SetRootHome()
        {
            // Set the path to the site's root
            currentPath = "";

            // Reset the tree
            PopulateTree(currentPath);

            // Reset the path
            lblDekiWikiPath.Text = "";
        }

        private void SetRootUser()
        {
            // Set the path to the user's root
            currentPath = "User:" + this.Options.Account.Username;

            // Reset the tree
            PopulateTree(currentPath);

            // Reset the path
            lblDekiWikiPath.Text = currentPath;
        }

        private void UpdatePathText()
        {
            // Get the current node
            TreeNode node = tvDekiWikiPath.SelectedNode;

            // Make sure we have a selected node
            if (node == null)
            {
                lblDekiWikiPath.Text = "";
                return;
            }

            // Make sure there's a tag (there won't be for history)
            if (node.Tag == null)
            {
                return;
            }

            // Get the page from the node tag
            DekiWiki.Page page = (DekiWiki.Page)node.Tag;

            // Update the path
            lblDekiWikiPath.Text = page.path;
        }

        private TreeNode PopulateNode(DekiWiki.Page parent)
        {
            // Create the node
            TreeNode node = new TreeNode(parent.name);

            // Tag the node with the appropriate page object
            node.Tag = parent;

            // Recurse through each child node
            foreach (DekiWiki.Page page in parent.children)
            {
                node.Nodes.Add(PopulateNode(page));
            }

            // Return the node
            return node;
        }

        private void PopulateTree(string path)
        {
            // Set to not paint
            tvDekiWikiPath.BeginUpdate();

            // Clear any nodes
            tvDekiWikiPath.Nodes.Clear();

            // Get the pages structure
            List<DekiWiki.Page> pages = connector.getPathInfo(path);

            // Recurse through the nodes
            foreach (DekiWiki.Page page in pages)
            {
                // Just a note, this should only happen once
                tvDekiWikiPath.Nodes.Add(PopulateNode(page));
            }

            TreeNode historyNode = new TreeNode("History");

            history.Reverse();
            foreach (DekiWikiHistory item in history)
            {
                DekiWiki.Page page;

                try
                {
                    page = connector.getPageInfo(item.Path);
                }
                catch
                {
                    continue;
                }

                TreeNode node = new TreeNode(page.name);

                node.Tag = page;

                historyNode.Nodes.Add(node);
            }
            history.Reverse();

            tvDekiWikiPath.Nodes.Add(historyNode);

            // Let the app paint now
            tvDekiWikiPath.EndUpdate();
        }

        private void btnDekiWikiSave_Click(object sender, EventArgs e)
        {
            path = lblDekiWikiPath.Text;
            DialogResult = DialogResult.OK;
            Hide();
            Close();
        }

        private void btnDekiWikiCancel_Click(object sender, EventArgs e)
        {
            path = "";
            DialogResult = DialogResult.Cancel;
            Hide();
            Close();
        }

        private void btnDekiWikiHome_Click(object sender, EventArgs e)
        {
            SetRootHome();
        }

        private void btnDekiWikiUser_Click(object sender, EventArgs e)
        {
            SetRootUser();
        }

        private void tvDekiWikiPath_AfterSelect(object sender, TreeViewEventArgs e)
        {
            UpdatePathText();
        }

        private void DekiWikiPath_Load(object sender, EventArgs e)
        {
            SetRootUser();
        }
    }
}
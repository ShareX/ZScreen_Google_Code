using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ZSS;
using IconHelper;
using FTPTest.Properties;

namespace FTPTest
{
    public partial class TestForm : Form
    {
        public FTP FTPClient;
        private string currentDirectory;

        public TestForm()
        {
            InitializeComponent();
            lvFTPList.SubItemEndEditing += new SubItemEndEditingEventHandler(lvFTPList_SubItemEndEditing);

            if (string.IsNullOrEmpty(Settings.Default.Server) || string.IsNullOrEmpty(Settings.Default.UserName) ||
                string.IsNullOrEmpty(Settings.Default.Password))
            {
                new LoginDialog().ShowDialog();
            }

            FTPAccount FTPAcc = new FTPAccount("FTP Test")
            {
                Server = Settings.Default.Server,
                Username = Settings.Default.UserName,
                Password = Settings.Default.Password
            };

            FTPClient = new FTP(FTPAcc);

            LoadDirectory("");
        }

        private void lvFTPList_SubItemEndEditing(object sender, SubItemEndEditingEventArgs e)
        {
            if (lvFTPList.SelectedItems.Count > 0 && !e.Cancel)
            {
                FTP.FTPLineResult file = (FTP.FTPLineResult)lvFTPList.SelectedItems[0].Tag;
                FTPClient.Rename(file.Name, e.DisplayText);
                file.Name = e.DisplayText;
            }
        }

        private void LoadDirectory(string path)
        {
            currentDirectory = path;
            FTPClient.Account.Path = currentDirectory;

            FTP.FTPLineResult[] list = FTPClient.ListDirectoryDetails();
            list = list.OrderBy(x => !x.IsDirectory).ThenBy(x => x.Name).ToArray();
            //list = (from x in list orderby !x.IsDirectory, x.Name select x).ToArray();

            lvFTPList.Items.Clear();
            lvFTPList.SmallImageList = new ImageList { ColorDepth = ColorDepth.Depth32Bit };

            foreach (FTP.FTPLineResult file in list)
            {
                ListViewItem lvi = new ListViewItem(file.Name);
                lvi.SubItems.Add(file.SizeString);
                lvi.SubItems.Add(IconReader.GetDisplayName(file.Name, file.IsDirectory));
                lvi.SubItems.Add(file.TimeInfo ? file.DateTime.ToString() : file.DateTime.ToShortDateString());
                lvi.SubItems.Add(file.Permissions);
                lvi.SubItems.Add(file.Owner + " " + file.Group);
                lvi.Tag = file;

                string ext;
                if (file.IsDirectory)
                {
                    ext = "Directory";
                }
                else if (file.Name.Contains('.'))
                {
                    ext = file.Name.Remove(0, file.Name.LastIndexOf('.'));
                }
                else
                {
                    ext = "File";
                }

                if (!lvFTPList.SmallImageList.Images.Keys.Contains(ext))
                {
                    Icon icon;
                    if (file.IsDirectory)
                    {
                        icon = IconReader.GetFolderIcon(IconReader.IconSize.Small, IconReader.FolderType.Closed);
                    }
                    else
                    {
                        icon = IconReader.GetFileIcon(ext, IconReader.IconSize.Small, false);
                    }

                    if (icon != null)
                    {
                        lvFTPList.SmallImageList.Images.Add(ext, icon.ToBitmap());
                    }
                }

                if (lvFTPList.SmallImageList.Images.Keys.Contains(ext))
                {
                    lvi.ImageKey = ext;
                }

                lvFTPList.Items.Add(lvi);
            }
        }

        private void lvFTPList_DoubleClick(object sender, EventArgs e)
        {
            if (lvFTPList.SelectedItems.Count > 0)
            {
                FTP.FTPLineResult file = (FTP.FTPLineResult)lvFTPList.SelectedItems[0].Tag;
                string path = FTP.CombineURL(currentDirectory, file.Name);
                if (file.IsDirectory)
                {
                    LoadDirectory(path);
                }
                else
                {
                    //FTPClient.DownloadFile(path, "mycomputer");
                }
            }
        }

        private void renameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (lvFTPList.SelectedItems.Count > 0)
            {
                lvFTPList.StartEditing(txtRename, lvFTPList.SelectedItems[0], 0);
                int offset = 23;
                txtRename.Left += offset;
                txtRename.Width -= offset;
            }
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (lvFTPList.SelectedItems.Count > 0)
            {
                ListViewItem lvi = lvFTPList.SelectedItems[0];
                FTP.FTPLineResult file = (FTP.FTPLineResult)lvi.Tag;
                FTPClient.DeleteFile(file.Name);
                lvFTPList.Items.Remove(lvi);
            }
        }
    }
}
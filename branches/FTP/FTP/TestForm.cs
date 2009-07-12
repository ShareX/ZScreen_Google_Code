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

namespace FTPTest
{
    public partial class TestForm : Form
    {
        public FTP FTPClient;
        private string currentDirectory;

        public TestForm()
        {
            InitializeComponent();

            FTPAccount FTPAcc = new FTPAccount("FTP Test") { Server = "", Username = "", Password = "" };
            FTPClient = new FTP(FTPAcc);

            LoadDirectory("");
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
    }
}
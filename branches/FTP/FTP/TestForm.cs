using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using FTPTest.Properties;
using IconHelper;
using ZSS;

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
            FTPClient.FTPOutput += x => txtConsole.AppendText(x + "\r\n");

            currentDirectory = FTPClient.FTPAddress;
            RefreshDirectory();
        }

        #region Methods

        private void RefreshDirectory()
        {
            LoadDirectory(currentDirectory);
        }

        private void LoadDirectory(string path)
        {
            currentDirectory = path;
            FTPClient.Account.Path = currentDirectory;
            btnNavigateBack.Enabled = currentDirectory != FTPClient.FTPAddress;
            txtCurrentDirectory.Text = " " + currentDirectory;

            FTPLineResult[] list = FTPClient.ListDirectoryDetails(currentDirectory);
            list = list.OrderBy(x => !x.IsDirectory).ThenBy(x => x.Name).ToArray();
            //list = (from x in list orderby !x.IsDirectory, x.Name select x).ToArray();

            lvFTPList.Items.Clear();
            lvFTPList.SmallImageList = new ImageList { ColorDepth = ColorDepth.Depth32Bit };

            foreach (FTPLineResult file in list)
            {
                if (file.IsDirectory && (file.Name == "." || file.Name == "..")) continue;

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

        private void FTPDownload(bool openDirectory)
        {
            if (lvFTPList.SelectedItems.Count > 0)
            {
                FTPLineResult file = lvFTPList.SelectedItems[0].Tag as FTPLineResult;
                string path = FTPHelpers.CombineURL(currentDirectory, file.Name);
                if (file.IsDirectory)
                {
                    if (openDirectory)
                    {
                        LoadDirectory(path);
                    }
                }
                else if (!string.IsNullOrEmpty(file.Name))
                {
                    SaveFileDialog sfd = new SaveFileDialog();
                    sfd.FileName = file.Name;
                    sfd.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                    if (sfd.ShowDialog() == DialogResult.OK)
                    {
                        FTPClient.DownloadFile(path, sfd.FileName);
                    }
                }
            }
        }

        private void FTPRename()
        {
            if (lvFTPList.SelectedItems.Count > 0)
            {
                lvFTPList.StartEditing(txtRename, lvFTPList.SelectedItems[0], 0);
                int offset = 23;
                txtRename.Left += offset;
                txtRename.Width -= offset;
            }
        }

        private void lvFTPList_SubItemEndEditing(object sender, SubItemEndEditingEventArgs e)
        {
            if (lvFTPList.SelectedItems.Count > 0 && !e.Cancel)
            {
                FTPLineResult file = (FTPLineResult)lvFTPList.SelectedItems[0].Tag;
                if (file.Name != e.DisplayText)
                {
                    string path = FTPHelpers.CombineURL(currentDirectory, file.Name);
                    FTPClient.Rename(path, e.DisplayText);
                    file.Name = e.DisplayText;
                }
            }
        }

        private void FTPDelete()
        {
            if (lvFTPList.SelectedItems.Count > 0)
            {
                ListViewItem lvi = lvFTPList.SelectedItems[0];
                FTPLineResult file = lvi.Tag as FTPLineResult;
                if (file != null && !string.IsNullOrEmpty(file.Name))
                {
                    string path = FTPHelpers.CombineURL(currentDirectory, file.Name);
                    if (file.IsDirectory)
                    {
                        FTPClient.RemoveDirectoryFull(path);
                    }
                    else
                    {
                        FTPClient.DeleteFile(path);
                    }
                    lvFTPList.Items.Remove(lvi);
                }
            }
        }

        private void FTPCreateDirectory()
        {
            InputBox ib = new InputBox { Text = "Create directory", Question = "Please enter the name of the directory which should be created:" };
            ib.ShowDialog();
            this.BringToFront();
            if (ib.DialogResult == DialogResult.OK)
            {
                FTPClient.MakeDirectory(FTPHelpers.CombineURL(currentDirectory, ib.InputText));
                RefreshDirectory();
            }
        }

        #endregion

        #region Form events

        private void lvFTPList_DoubleClick(object sender, EventArgs e)
        {
            FTPDownload(true);
        }

        private void downloadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FTPDownload(false);
        }

        private void renameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FTPRename();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FTPDelete();
        }

        private void createDirectoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FTPCreateDirectory();
        }

        private void btnNavigateBack_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(currentDirectory) && currentDirectory.Contains('/'))
            {
                LoadDirectory(currentDirectory.Substring(0, currentDirectory.LastIndexOf('/')));
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            RefreshDirectory();
        }

        #endregion
    }
}
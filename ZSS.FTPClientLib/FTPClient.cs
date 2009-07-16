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
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using IconHelper;
using ZSS;
using System.Collections.Generic;
using System.IO;
using System.Collections;

namespace ZSS.FTPClientLib
{
    public partial class FTPClient : Form
    {
        public FTP FTPAdapter;
        private string currentDirectory;
        private ListViewItem tempSelected;

        public FTPClient(FTPAccount FTPAcc)
        {
            InitializeComponent();
            lvFTPList.SubItemEndEditing += new SubItemEndEditingEventHandler(lvFTPList_SubItemEndEditing);

            /*if (string.IsNullOrEmpty(Settings.Default.Server) || string.IsNullOrEmpty(Settings.Default.UserName) ||
                string.IsNullOrEmpty(Settings.Default.Password))
            {
                new LoginDialog().ShowDialog();
            }*/

            FTPAdapter = new FTP(FTPAcc);
            FTPAdapter.FTPOutput += x => txtConsole.AppendText(x + "\r\n");

            RefreshDirectory();
        }

        #region Methods

        private void RefreshDirectory()
        {
            if (string.IsNullOrEmpty(currentDirectory))
            {
                currentDirectory = FTPAdapter.Account.FTPAddress;
            }

            LoadDirectory(currentDirectory);
        }

        private void FillDirectories(string path)
        {
            path = path.Remove(0, FTPAdapter.Account.FTPAddress.Length);
            List<string> paths = FTPHelpers.GetPaths(path);
            paths.Insert(0, "/");

            cbDirectoryList.Items.Clear();
            foreach (string directory in paths)
            {
                cbDirectoryList.Items.Add(directory);
            }

            if (cbDirectoryList.Items.Count > 0)
            {
                cbDirectoryList.SelectedIndex = cbDirectoryList.Items.Count - 1;
            }
        }

        private void LoadDirectory(string path)
        {
            currentDirectory = path;
            FillDirectories(currentDirectory);

            List<FTPLineResult> list = FTPAdapter.ListDirectoryDetails(currentDirectory);
            list = list.OrderBy(x => !x.IsDirectory).ThenBy(x => x.Name).ToList();
            //list = (from x in list orderby !x.IsDirectory, x.Name select x).ToArray();

            if (path != FTPAdapter.Account.FTPAddress)
            {
                list.Insert(0, new FTPLineResult { Name = ".", IsDirectory = true, IsSpecial = true });
                list.Insert(1, new FTPLineResult { Name = "..", IsDirectory = true, IsSpecial = true });
            }

            lvFTPList.Items.Clear();
            lvFTPList.SmallImageList = new ImageList { ColorDepth = ColorDepth.Depth32Bit };

            foreach (FTPLineResult file in list)
            {
                if (file.IsDirectory && (file.Name == "." || file.Name == "..") && !file.IsSpecial) continue;

                ListViewItem lvi = new ListViewItem(file.Name);

                if (!file.IsSpecial)
                {
                    lvi.SubItems.Add(file.SizeString);
                    lvi.SubItems.Add(IconReader.GetDisplayName(file.Name, file.IsDirectory));
                    lvi.SubItems.Add(file.TimeInfo ? file.DateTime.ToString() : file.DateTime.ToShortDateString());
                    lvi.SubItems.Add(file.Permissions);
                    lvi.SubItems.Add(file.Owner + " " + file.Group);
                }

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
                FTPLineResult checkDirectory = lvFTPList.SelectedItems[0].Tag as FTPLineResult;
                if (openDirectory)
                {
                    if (checkDirectory != null)
                    {
                        if (checkDirectory.IsDirectory)
                        {
                            if (checkDirectory.IsSpecial)
                            {
                                if (checkDirectory.Name == ".")
                                {
                                    LoadDirectory(FTPAdapter.Account.FTPAddress);
                                }
                                else if (checkDirectory.Name == "..")
                                {
                                    FTPNavigateBack();
                                }
                            }
                            else
                            {
                                string loadPath = FTPHelpers.CombineURL(currentDirectory, checkDirectory.Name);
                                LoadDirectory(loadPath);
                            }
                            return;
                        }
                    }
                }

                if (!checkDirectory.IsDirectory && !checkDirectory.IsSpecial)
                {
                    FolderBrowserDialog fbd = new FolderBrowserDialog();
                    fbd.RootFolder = Environment.SpecialFolder.Desktop;

                    if (fbd.ShowDialog() == DialogResult.OK && !string.IsNullOrEmpty(fbd.SelectedPath))
                    {
                        string path;
                        foreach (ListViewItem lvi in lvFTPList.SelectedItems)
                        {
                            FTPLineResult file = lvi.Tag as FTPLineResult;
                            path = FTPHelpers.CombineURL(currentDirectory, file.Name);

                            if (!file.IsDirectory && !string.IsNullOrEmpty(file.Name))
                            {
                                FTPAdapter.DownloadFile(path, FTPHelpers.CombineURL(fbd.SelectedPath, file.Name));
                            }
                        }
                    }
                }
            }
        }

        private void FTPRename()
        {
            if (lvFTPList.SelectedItems.Count > 0)
            {
                FTPLineResult file = (FTPLineResult)lvFTPList.SelectedItems[0].Tag;
                if (!file.IsSpecial)
                {
                    lvFTPList.StartEditing(txtRename, lvFTPList.SelectedItems[0], 0);
                    int offset = 23;
                    txtRename.Left += offset;
                    txtRename.Width -= offset;
                }
            }
        }

        private void lvFTPList_SubItemEndEditing(object sender, SubItemEndEditingEventArgs e)
        {
            if (lvFTPList.SelectedItems.Count > 0 && !e.Cancel && !string.IsNullOrEmpty(e.DisplayText))
            {
                FTPLineResult file = (FTPLineResult)lvFTPList.SelectedItems[0].Tag;
                if (file.Name != e.DisplayText)
                {
                    string path = FTPHelpers.CombineURL(currentDirectory, file.Name);
                    FTPAdapter.Rename(path, e.DisplayText);
                    file.Name = e.DisplayText;
                }
            }
        }

        private void FTPDelete()
        {
            foreach (ListViewItem lvi in lvFTPList.SelectedItems)
            {
                FTPLineResult file = lvi.Tag as FTPLineResult;
                if (file != null && !string.IsNullOrEmpty(file.Name) && !file.IsSpecial)
                {
                    string path = FTPHelpers.CombineURL(currentDirectory, file.Name);
                    if (file.IsDirectory)
                    {
                        FTPAdapter.RemoveDirectory(path);
                    }
                    else
                    {
                        FTPAdapter.DeleteFile(path);
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
                FTPAdapter.MakeDirectory(FTPHelpers.CombineURL(currentDirectory, ib.InputText));
                RefreshDirectory();
            }
        }

        private void FTPUploadFiles(string uploadDirectory, string[] files)
        {
            string filename;
            foreach (string file in files)
            {
                filename = Path.GetFileName(file);
                if (filename.Contains('.'))
                {
                    FTPAdapter.UploadFile(file, FTPHelpers.CombineURL(uploadDirectory, filename));
                }
                else
                {
                    List<string> filesList = new List<string>();
                    filesList.AddRange(Directory.GetFiles(file));
                    filesList.AddRange(Directory.GetDirectories(file));
                    string path = FTPHelpers.CombineURL(uploadDirectory, filename);
                    FTPAdapter.MakeDirectory(path);
                    FTPUploadFiles(path, filesList.ToArray());
                }
            }
        }

        private void FTPNavigateBack()
        {
            if (!string.IsNullOrEmpty(currentDirectory) && currentDirectory.Contains('/'))
            {
                LoadDirectory(currentDirectory.Substring(0, currentDirectory.LastIndexOf('/')));
            }
        }

        #endregion

        #region Form events

        private void lvFTPList_MouseDoubleClick(object sender, MouseEventArgs e)
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

        private void lvFTPList_ItemDrag(object sender, ItemDragEventArgs e)
        {
            if (lvFTPList.SelectedItems.Count > 0)
            {
                List<string> filenames = new List<string>();
                foreach (ListViewItem lvi in lvFTPList.SelectedItems)
                {
                    FTPLineResult file = lvi.Tag as FTPLineResult;
                    if (file != null && !string.IsNullOrEmpty(file.Name) && !file.IsSpecial)
                    {
                        filenames.Add(file.Name);
                    }
                }
                if (filenames.Count > 0)
                {
                    lvFTPList.DoDragDrop(filenames.ToArray(), DragDropEffects.Move);
                }
            }
        }

        private void lvFTPList_DragOver(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(string[])))
            {
                Point point = lvFTPList.PointToClient(new Point(e.X, e.Y));
                ListViewItem lvi = lvFTPList.GetItemAt(point.X, point.Y);
                if (lvi != null && e.AllowedEffect == DragDropEffects.Move)
                {
                    if (tempSelected != null && tempSelected != lvi)
                    {
                        tempSelected.Selected = false;
                    }
                    FTPLineResult file = lvi.Tag as FTPLineResult;
                    if (file != null && file.IsDirectory)
                    {
                        lvi.Selected = true;
                        tempSelected = lvi;
                        e.Effect = DragDropEffects.Move;
                    }
                    else
                    {
                        e.Effect = DragDropEffects.None;
                    }
                }
            }
            else if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Move;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private void lvFTPList_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(string[])))
            {
                Point point = lvFTPList.PointToClient(new Point(e.X, e.Y));
                ListViewItem lvi = lvFTPList.GetItemAt(point.X, point.Y);
                if (lvi != null && e.AllowedEffect == DragDropEffects.Move)
                {
                    if (tempSelected != null && tempSelected != lvi)
                    {
                        tempSelected.Selected = false;
                    }
                    FTPLineResult file = lvi.Tag as FTPLineResult;
                    if (file != null && file.IsDirectory)
                    {
                        string[] filenames = e.Data.GetData(typeof(string[])) as string[];
                        if (filenames != null)
                        {
                            int renameCount = 0;
                            foreach (string filename in filenames)
                            {
                                if (file.Name != filename)
                                {
                                    string path = FTPHelpers.CombineURL(currentDirectory, filename);
                                    string movePath = "";
                                    if (file.IsSpecial)
                                    {
                                        if (file.Name == ".")
                                        {
                                            movePath = FTPHelpers.AddSlash(filename, FTPHelpers.SlashType.Prefix, 2);
                                        }
                                        else if (file.Name == "..")
                                        {
                                            movePath = FTPHelpers.AddSlash(filename, FTPHelpers.SlashType.Prefix);
                                        }
                                    }
                                    else
                                    {
                                        movePath = FTPHelpers.CombineURL(file.Name, filename);
                                    }
                                    if (!string.IsNullOrEmpty(movePath))
                                    {
                                        FTPAdapter.Rename(path, movePath);
                                        renameCount++;
                                    }
                                }
                            }
                            if (renameCount > 0)
                            {
                                RefreshDirectory();
                            }
                        }
                    }
                }
                tempSelected = null;
            }
            else if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = e.Data.GetData(DataFormats.FileDrop) as string[];
                if (files != null)
                {
                    FTPUploadFiles(currentDirectory, files);
                    RefreshDirectory();
                }
            }
        }

        private void lvFTPList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvFTPList.SelectedItems.Count > 0)
            {
                FTPLineResult file = lvFTPList.SelectedItems[0].Tag as FTPLineResult;

                downloadToolStripMenuItem.Enabled = !file.IsDirectory && !file.IsSpecial;
                renameToolStripMenuItem.Enabled = deleteToolStripMenuItem.Enabled =
                    copyURLsToClipboardToolStripMenuItem.Enabled = !file.IsSpecial;
                //refreshToolStripMenuItem.Enabled
                //createDirectoryToolStripMenuItem.Enabled
            }
        }

        private void refreshToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RefreshDirectory();
        }

        private void cbDirectoryList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbDirectoryList.Items.Count > 0)
            {
                string path = cbDirectoryList.SelectedItem.ToString().Remove(0, 1);
                path = FTPHelpers.CombineURL(FTPAdapter.Account.FTPAddress, path);
                if (currentDirectory != path)
                {
                    LoadDirectory(path);
                }
            }
        }

        private void copyURLsToClipboardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string path;
            List<string> list = new List<string>();

            foreach (ListViewItem lvi in lvFTPList.SelectedItems)
            {
                FTPLineResult file = lvi.Tag as FTPLineResult;
                if (!file.IsSpecial)
                {
                    path = currentDirectory.Remove(0, FTPAdapter.Account.FTPAddress.Length);
                    path = FTPAdapter.Account.GetUriPath(FTPHelpers.CombineURL(path, file.Name), true);
                    list.Add(path);
                }
            }

            string clipboard = string.Join("\r\n", list.ToArray());

            if (!string.IsNullOrEmpty(clipboard))
            {
                Clipboard.SetText(clipboard);
            }
        }

        private void FTPClient_Resize(object sender, EventArgs e)
        {
            this.Refresh();
        }

        #endregion
    }
}
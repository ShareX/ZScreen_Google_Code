using System.IO;
using System.Linq;
using System.Windows.Forms;
using HelpersLib;
using UploadersLib.FileUploaders;
using UploadersLib.HelperClasses;

namespace UploadersLib.Forms
{
    public partial class DropboxFilesForm : Form
    {
        private Dropbox dropbox;
        private ImageListManager ilm;

        public DropboxFilesForm(OAuthInfo oauth, string path = null)
        {
            InitializeComponent();
            dropbox = new Dropbox(oauth);
            ilm = new ImageListManager(lvDropboxFiles);

            if (path != null)
            {
                Shown += (sender, e) => OpenDirectory(path);
            }
        }

        public void OpenDirectory(string path)
        {
            lvDropboxFiles.Items.Clear();

            DropboxDirectoryInfo directory = null;

            AsyncHelper.AsyncJob(() =>
            {
                directory  = dropbox.GetFilesList(path);
            },
            () =>
            {
                if (directory != null)
                {
                    lvDropboxFiles.Tag = directory;

                    ListViewItem lvi = GetParentFolder(directory.Path);

                    if (lvi != null)
                    {
                        lvDropboxFiles.Items.Add(lvi);
                    }

                    foreach (DropboxContentInfo content in directory.Contents.OrderBy(x => !x.Is_dir))
                    {
                        string filename = Path.GetFileName(content.Path);
                        lvi = new ListViewItem(filename);
                        lvi.SubItems.Add(content.Size);
                        lvi.SubItems.Add(content.Modified);
                        lvi.ImageKey = ilm.AddImage(content.Icon);
                        lvi.Tag = content;
                        lvDropboxFiles.Items.Add(lvi);
                    }

                    Text = "Dropbox - Path: " + ZAppHelper.CombineURL(directory.Root, directory.Path);
                }
                else
                {
                    MessageBox.Show("Path not exist: " + path, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            });
        }

        public ListViewItem GetParentFolder(string currentPath)
        {
            if (!string.IsNullOrEmpty(currentPath))
            {
                string parentFolder = currentPath.Remove(currentPath.LastIndexOf('/'));

                DropboxContentInfo content = new DropboxContentInfo() { Icon = "folder", Is_dir = true, Path = parentFolder };

                ListViewItem lvi = new ListViewItem("Parent folder");
                lvi.ImageKey = ilm.AddImage(content.Icon);
                lvi.Tag = content;
                return lvi;
            }

            return null;
        }

        private void lvDropboxFiles_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (lvDropboxFiles.SelectedItems.Count > 0)
            {
                DropboxContentInfo content = lvDropboxFiles.SelectedItems[0].Tag as DropboxContentInfo;

                if (content != null && content.Is_dir)
                {
                    OpenDirectory(content.Path);
                }
            }
        }
    }
}
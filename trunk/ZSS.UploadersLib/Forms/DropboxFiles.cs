using System.IO;
using System.Windows.Forms;
using UploadersLib.FileUploaders;
using UploadersLib.HelperClasses;

namespace UploadersLib.Forms
{
    public partial class DropboxFiles : Form
    {
        private Dropbox dropbox;

        public DropboxFiles(OAuthInfo oauth)
        {
            InitializeComponent();
            dropbox = new Dropbox(oauth);
            ImageList il = new ImageList() { ColorDepth = ColorDepth.Depth32Bit };
            il.Images.Add("folder", Properties.Resources.folder);
            il.Images.Add("page_white", Properties.Resources.page_white);
            lvDropboxFiles.SmallImageList = il;
        }

        public bool OpenDirectory(string path)
        {
            bool result = false;

            Cursor.Current = Cursors.WaitCursor;

            DropboxDirectoryInfo directory = dropbox.GetFilesList(path);

            lvDropboxFiles.Items.Clear();

            if (directory != null)
            {
                foreach (DropboxContentInfo content in directory.Contents)
                {
                    string filename = Path.GetFileName(content.Path);
                    ListViewItem lvi = new ListViewItem(filename);
                    lvi.SubItems.Add(content.Size);
                    lvi.SubItems.Add(content.Modified);
                    lvi.Tag = content;

                    if (content.Is_dir)
                    {
                        lvi.ImageKey = "folder";
                    }
                    else
                    {
                        lvi.ImageKey = "page_white";
                    }

                    lvDropboxFiles.Items.Add(lvi);
                }

                result = true;
            }
            else
            {
                MessageBox.Show("Path not exist: " + path, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            Cursor.Current = Cursors.Default;

            return result;
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
using System.IO;
using System.Linq;
using System.Windows.Forms;
using UploadersLib.FileUploaders;
using UploadersLib.HelperClasses;

namespace UploadersLib.Forms
{
    public partial class DropboxFilesForm : Form
    {
        private Dropbox dropbox;
        private ImageListManager ilm;

        public DropboxFilesForm(OAuthInfo oauth)
        {
            InitializeComponent();
            dropbox = new Dropbox(oauth);
            ilm = new ImageListManager(lvDropboxFiles);
        }

        public bool OpenDirectory(string path)
        {
            bool result = false;

            Cursor.Current = Cursors.WaitCursor;

            DropboxDirectoryInfo directory = dropbox.GetFilesList(path);

            lvDropboxFiles.Items.Clear();

            if (directory != null)
            {
                foreach (DropboxContentInfo content in directory.Contents.OrderBy(x => !x.Is_dir))
                {
                    string filename = Path.GetFileName(content.Path);
                    ListViewItem lvi = new ListViewItem(filename);
                    lvi.SubItems.Add(content.Size);
                    lvi.SubItems.Add(content.Modified);
                    lvi.ImageKey = ilm.AddImage(content.Icon);
                    lvi.Tag = content;
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
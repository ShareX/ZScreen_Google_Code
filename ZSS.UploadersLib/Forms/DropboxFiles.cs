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
        }

        public void OpenDirectory(string path)
        {
            DropboxDirectoryInfo directory = dropbox.GetFilesList(path);

            lvDropboxFiles.Items.Clear();

            foreach (DropboxContentInfo content in directory.Contents)
            {
                string filename = Path.GetFileName(content.Path);
                ListViewItem lvi = new ListViewItem(filename);
                lvi.SubItems.Add(content.Size);
                lvi.SubItems.Add(content.Modified);
                lvi.Tag = content;
                lvDropboxFiles.Items.Add(lvi);
            }
        }

        private void lvDropboxFiles_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (lvDropboxFiles.SelectedItems.Count > 0)
            {
                DropboxContentInfo content = lvDropboxFiles.SelectedItems[0].Tag as DropboxContentInfo;

                if (content != null)
                {
                    OpenDirectory(content.Path);
                }
            }
        }
    }
}
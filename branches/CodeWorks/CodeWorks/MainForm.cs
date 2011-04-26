using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using CodeWorks.Properties;

namespace CodeWorks
{
    public partial class MainForm : Form
    {
        private string[] ignoredFolders = new string[] { "bin", "obj", "Properties" };
        private string[] allowedFileExtensions = new string[] { ".cs" };
        private string[] ignoredFileNames = new string[] { ".designer.cs" };

        private string searchText;

        public MainForm()
        {
            InitializeComponent();
            searchText = Resources.SearchText;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            lvResults.Items.Clear();
            SearchFolder(txtFolderPath.Text);
            lblFileCount.Text = lvResults.Items.Count + " files.";
        }

        private void SearchFolder(string path)
        {
            if (IsValidFolder(path))
            {
                foreach (string folder in Directory.GetDirectories(path))
                {
                    SearchFolder(folder);
                }

                foreach (string file in Directory.GetFiles(path))
                {
                    CheckFile(file);
                }
            }
        }

        private bool IsValidFolder(string path)
        {
            return !string.IsNullOrEmpty(path) && Directory.Exists(path) && !ignoredFolders.Contains(Path.GetFileName(path));
        }

        private bool IsValidFile(string path)
        {
            return !string.IsNullOrEmpty(path) && File.Exists(path) &&
                Path.HasExtension(path) && allowedFileExtensions.Contains(Path.GetExtension(path)) &&
                ignoredFileNames.All(x => !Path.GetFileName(path).ToLowerInvariant().EndsWith(x));
        }

        private bool CheckFile(string path)
        {
            if (IsValidFile(path))
            {
                TextInfo info = new TextInfo(path);

                string check = CheckLicense(info.CurrentText);
                // result = RemoveDuplicateLines(result);

                if (!string.IsNullOrEmpty(check))
                {
                    info.IsDifferent = true;
                    info.NewText = check;
                    lvResults.Items.Add(path).Tag = info;
                    return true;
                }
            }

            return false;
        }

        private string CheckLicense(string text)
        {
            if (!text.StartsWith(searchText))
            {
                return text.Insert(0, searchText + "\r\n\r\n");
            }

            return null;
        }

        private string RemoveDuplicateLines(string text)
        {
            return Regex.Replace(text, @"(\r\n\s*){3,}", "\r\n\r\n", RegexOptions.Singleline);
        }

        private void lvResults_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvResults.SelectedItems.Count > 0)
            {
                TextInfo info = lvResults.SelectedItems[0].Tag as TextInfo;

                if (info != null)
                {
                    tbDefaultText.Text = info.CurrentText;
                    tbNewText.Text = info.NewText;
                }
            }
        }

        private void btnAddLicense_Click(object sender, EventArgs e)
        {
            if (lvResults.SelectedItems.Count > 0)
            {
                TextInfo info = lvResults.SelectedItems[0].Tag as TextInfo;

                if (info != null)
                {
                    if (File.Exists(info.FilePath))
                    {
                        File.WriteAllText(info.FilePath, info.NewText);
                    }

                    lvResults.Items.Remove(lvResults.SelectedItems[0]);
                }
            }
        }

        private void btnAddLicenseAll_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem lvi in lvResults.Items)
            {
                TextInfo info = lvi.Tag as TextInfo;

                if (info != null)
                {
                    if (File.Exists(info.FilePath))
                    {
                        File.WriteAllText(info.FilePath, info.NewText);
                    }
                }
            }

            lvResults.Items.Clear();
        }

        private void MainForm_Resize(object sender, EventArgs e)
        {
            Refresh();
        }
    }
}
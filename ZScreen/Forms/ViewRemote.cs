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
using System.IO;
using System.Windows.Forms;
using ZSS;
using ZSS.Properties;

namespace ZScreenLib
{
    public partial class ViewRemote : Form
    {
        private System.Resources.ResourceManager mRes = Resources.ResourceManager;

        private FTPAdapter mFTP;

        private int mOldX = 0, mOldY = 0;

        private FTPAccount mAcc;

        public ViewRemote()
        {
            InitializeComponent();
            this.Icon = Resources.zss_main;
        }

        private List<string> FetchList()
        {
            List<string> result = new List<string>();

            try
            {
                string[] splode, str = mFTP.ListDirectory(FTPHelpers.CombineURL(mAcc.FTPAddress, mAcc.Path));
                string goodFile;

                bwRemoteViewer.ReportProgress((int)RemoteViewerTask.ProgressType.UPDATE_PROGRESS_MAX, str.Length);
                for (int x = 0; x < str.Length; x++)
                {
                    splode = str[x].Split(' ');
                    goodFile = splode[splode.Length - 1];
                    if (goodFile.Length > 2 && goodFile.Contains("."))
                    {
                        result.Add(goodFile);
                        bwRemoteViewer.ReportProgress((int)RemoteViewerTask.ProgressType.ADD_FILE_TO_LISTBOX, goodFile);
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "ZScreen FTP");
            }

            return result;
        }

        private void btnCopyToClip_Click(object sender, EventArgs e)
        {
            string str = "";

            List<string> items = new List<string>();

            foreach (string obj in lbFiles.SelectedItems)
            {
                items.Add(mAcc.GetUriPath(FTPHelpers.GetFileName(obj)) + Environment.NewLine);
            }

            if (cbReverse.Checked) items.Reverse();
            str = string.Join(Environment.NewLine, items.ToArray());
            if (cbAddSpace.Checked) str = str.Insert(0, Environment.NewLine);

            if (!string.IsNullOrEmpty(str))
            {
                Clipboard.SetText(str);
            }
        }

        private void btnDelete_Click(object sender, System.EventArgs e)
        {
            try
            {
                List<string> list = new List<string>();

                foreach (string item in lbFiles.SelectedItems)
                {
                    list.Add(item);
                }

                foreach (string obj in list)
                {
                    if (!string.IsNullOrEmpty(obj))
                    {
                        mFTP.DeleteFile(FTPHelpers.CombineURL(mAcc.FTPAddress, mAcc.Path, obj));
                        lbFiles.Items.Remove(obj);
                    }
                }
            }
            catch (Exception ex)
            {
                FileSystem.AppendDebug(ex.ToString());
                MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            if (lbFiles.Items.Count > 0) lbFiles.SelectedIndex = 0;
        }

        private void ViewRemote_FormClosing(object sender, System.Windows.Forms.FormClosingEventArgs e)
        {
            pbViewer.ImageLocation = "";      
        }

        private void btnSave_Click(object sender, System.EventArgs e)
        {
            string dir;

            folderBrowseDialog.SelectedPath = Program.ImagesDir;
            folderBrowseDialog.ShowNewFolderButton = true;

            if (folderBrowseDialog.ShowDialog() == DialogResult.OK && !string.IsNullOrEmpty(dir = folderBrowseDialog.SelectedPath))
            {
                try
                {
                    foreach (string str in lbFiles.SelectedItems)
                    {
                        mFTP.DownloadFile(FTPHelpers.CombineURL(mAcc.FTPAddress, mAcc.Path, str), Path.Combine(dir, Path.GetFileName(str)));
                    }
                }
                catch
                {
                    MessageBox.Show("Save Failed");
                }
            }
        }

        private void lbFiles_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            if (lbFiles.SelectedItems.Count == 1 && (string)lbFiles.SelectedItem != "")
            {
                string fp = (string)lbFiles.SelectedItem;

                if (!bwRemoteViewer.IsBusy)
                {
                    RemoteViewerTask rvt = new RemoteViewerTask(RemoteViewerTask.Jobs.VIEW_FILE);
                    rvt.RemoteFile = fp;
                    bwRemoteViewer.RunWorkerAsync(rvt);
                }

                if (FileSystem.IsValidImageFile(fp))
                {
                    pbViewer.Left = 0;
                    pbViewer.Top = 0;
                }
            }
        }

        private void pbViewer_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                mOldX = e.X;
                mOldY = e.Y;
            }
        }

        private void pbViewer_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                pbViewer.Left += (e.X - mOldX);
                pbViewer.Top += (e.Y - mOldY);
            }
        }

        private void sUpdateGuiControls()
        {
            foreach (System.Windows.Forms.Control ctl in this.pnlControls.Controls)
            {
                if (ctl is System.Windows.Forms.Button)
                {
                    ctl.Enabled = !bwRemoteViewer.IsBusy;
                }
            }
        }

        private void sBwFetchlist()
        {
            if (Program.conf.FTPAccountList != null)
                mAcc = Program.conf.FTPAccountList[Program.conf.FTPSelected];
            bwRemoteViewer.ReportProgress((int)RemoteViewerTask.ProgressType.UPDATE_STATUS_BAR_TEXT,
                string.Format("Fetching files from {0}", mAcc.Name));

            if (mAcc != null && !string.IsNullOrEmpty(mAcc.Server))
            {
                FTPOptions fopt = new FTPOptions();
                fopt.Account = mAcc;
                fopt.ProxySettings = Adapter.GetProxySettings();
                mFTP = new FTPAdapter(fopt);
                List<string> files = FetchList();
                if (files.Count > 0)
                {
                    sBwViewFile(files[0]);
                }
            }
        }

        private void sBwViewFile(string file)
        {
            string localfile = FileSystem.GetTempFilePath(file);
            if (!File.Exists(localfile))
            {
                bwRemoteViewer.ReportProgress((int)RemoteViewerTask.ProgressType.FETCHING_FILE, file);

                try
                {
                    //mFTP.ChangeDir(mAcc.Path);
                    string directory = Path.GetDirectoryName(localfile);
                    if (!Directory.Exists(directory))
                    {
                        Directory.CreateDirectory(directory);
                    }
                    mFTP.DownloadFile(FTPHelpers.CombineURL(mAcc.FTPAddress, mAcc.Path, file), localfile);
                }
                catch (System.Exception ex)
                {
                    FileSystem.AppendDebug(ex.Message);
                    // bwRemoteViewer.ReportProgress((int)RemoteViewerTask.ProgressType.VIEWING_FILE, "");
                }
            }

            // toDelete.Add(localfile);
            bwRemoteViewer.ReportProgress((int)RemoteViewerTask.ProgressType.VIEWING_FILE, localfile);
        }

        private void bwRemoteViewer_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            RemoteViewerTask rvt = (RemoteViewerTask)e.Argument;

            switch (rvt.Job)
            {
                case RemoteViewerTask.Jobs.FETCH_LIST:
                    sBwFetchlist();
                    break;
                case RemoteViewerTask.Jobs.VIEW_FILE:
                    sBwViewFile(rvt.RemoteFile);
                    break;
            }

            e.Result = rvt;
        }

        private void bwRemoteViewer_ProgressChanged(object sender, System.ComponentModel.ProgressChangedEventArgs e)
        {
            sUpdateGuiControls();
            pBar.Visible = true;

            RemoteViewerTask.ProgressType progress = (RemoteViewerTask.ProgressType)e.ProgressPercentage;
            string fp = string.Empty;
            switch (progress)
            {
                case RemoteViewerTask.ProgressType.ADD_FILE_TO_LISTBOX:
                    fp = (string)e.UserState;
                    lbFiles.Items.Add(fp);
                    if (lbFiles.SelectedIndex < 0)
                    {
                        lbFiles.SelectedIndex = 0;
                    }
                    pBar.Style = ProgressBarStyle.Continuous;
                    pBar.Increment(1);
                    break;

                case RemoteViewerTask.ProgressType.ADDING_FILES_TO_LISTBOX:
                    List<string> result = (List<string>)e.UserState;
                    lbFiles.Items.AddRange(result.ToArray());
                    //show first image on list on startup
                    if (lbFiles.Items.Count > 0)
                    {
                        lbFiles.SelectedIndex = 0;
                    }
                    break;

                case RemoteViewerTask.ProgressType.FETCHING_FILE:
                    fp = (string)e.UserState;
                    sBar.Text = string.Format("Fetching file {0} from {1}...", fp, mAcc.Name);
                    break;

                case RemoteViewerTask.ProgressType.INCREMENT_PROGRESS:
                    pBar.Style = ProgressBarStyle.Continuous;
                    pBar.Increment((int)e.UserState);
                    break;

                case RemoteViewerTask.ProgressType.UPDATE_PROGRESS_MAX:
                    int max = (int)e.UserState;
                    pBar.Style = ProgressBarStyle.Continuous;
                    pBar.Value = 0;
                    pBar.Maximum = max;
                    break;

                case RemoteViewerTask.ProgressType.UPDATE_STATUS_BAR_TEXT:
                    sBar.Text = (string)e.UserState;
                    break;

                case RemoteViewerTask.ProgressType.VIEWING_FILE:
                    fp = (string)e.UserState;
                    FileSystem.AppendDebug(string.Format("Viewing file: {0}", fp));
                    if (FileSystem.IsValidImageFile(fp))
                    {
                        pbViewer.Visible = true;
                        txtViewer.Visible = false;
                        pbViewer.ImageLocation = fp;
                    }
                    else if (FileSystem.IsValidTextFile(fp))
                    {
                        pbViewer.Visible = false;
                        txtViewer.Visible = true;
                        txtViewer.Text = File.ReadAllText(fp);
                    }
                    break;
            }
        }

        private void bwRemoteViewer_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            RemoteViewerTask rvt = (RemoteViewerTask)e.Result;

            switch (rvt.Job)
            {
                case RemoteViewerTask.Jobs.FETCH_LIST:
                    sBar.Text = string.Format("Ready. Loaded {0} files.", lbFiles.Items.Count);
                    break;
                case RemoteViewerTask.Jobs.VIEW_FILE:
                    sBar.Text = string.Format("Showing {0}.", rvt.RemoteFile);
                    break;
            }

            pBar.ProgressBar.Style = ProgressBarStyle.Continuous;

            sUpdateGuiControls();
            pBar.Value = 0;
            pBar.Visible = false;
        }

        private void ViewRemote_Shown(object sender, System.EventArgs e)
        {
            RemoteViewerTask rvt = new RemoteViewerTask(RemoteViewerTask.Jobs.FETCH_LIST);
            bwRemoteViewer.RunWorkerAsync(rvt);
        }
    }
}
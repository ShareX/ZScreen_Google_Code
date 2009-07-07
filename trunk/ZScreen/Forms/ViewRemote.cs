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
using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;
using ZSS.Properties;
using ZSS.Tasks;
using System.IO;

namespace ZSS
{
    public partial class ViewRemote : System.Windows.Forms.Form
    {
        private System.Resources.ResourceManager mRes = Properties.Resources.ResourceManager;

        private int mFrmWidth, mFrmHeight;
        private int mLbWidth, mLbHeight, mLbX, mLbY;
        private int mConWidth, mConHeight, mConX, mConY;
        private int mPbWidth, mPbHeight;

        private FTP mFTP/* = new FTP()*/;

        // private List<string> toDelete = new List<string>();

        private int mOldX = 0, mOldY = 0;

        private FTPAccount mAcc;

        long mBytesTotal = 0;
        long mBytesDownloaded = 0;

        public ViewRemote()
        {
            InitializeComponent();
            this.Icon = Properties.Resources.zss_main;
            setInitialHeightWidthVariables();

        }

        private void setInitialHeightWidthVariables()
        {
            mFrmWidth = Width;
            mFrmHeight = Height;
            mLbWidth = lbFiles.Width;
            mLbHeight = lbFiles.Height;
            mLbX = lbFiles.Location.X;
            mLbY = lbFiles.Location.Y;
            mConWidth = pnlControls.Width;
            mConHeight = pnlControls.Height;
            mConX = pnlControls.Location.X;
            mConY = pnlControls.Location.Y;
            mPbWidth = pbViewer.Width;
            mPbHeight = pbViewer.Height;
        }

        private void ViewRemote_Resize(object sender, System.EventArgs e)
        {
            if (WindowState == System.Windows.Forms.FormWindowState.Normal)
            {
                //Width = frmWidth;
                //Height = frmHeight;

                //width shrink
                pnlControls.Location = new System.Drawing.Point(mConX + mFrmWidth - Width, pnlControls.Location.Y);
                lbFiles.Location = new System.Drawing.Point(mLbX + mFrmWidth - Width, lbFiles.Location.Y);
                pnlViewer.Width = mPbWidth + mFrmWidth - Width;

                //height shrink
                lbFiles.Height = mLbHeight + mFrmHeight - Height;
                pnlViewer.Height = mPbHeight + mFrmHeight - Height;
            }

            //width grow
            if (Width - mFrmWidth > 0)
            {

                pnlControls.Location = new System.Drawing.Point(mConX + Width - mFrmWidth, pnlControls.Location.Y);
                lbFiles.Location = new System.Drawing.Point(mLbX + Width - mFrmWidth, lbFiles.Location.Y);
                pnlViewer.Width = mPbWidth + Width - mFrmWidth;
            }

            //Height grow
            if (Height - mFrmHeight > 0)
            {
                lbFiles.Height = mLbHeight + Height - mFrmHeight;
                pnlViewer.Height = mPbHeight + Height - mFrmHeight;
            }
        }

       private List<string> fetchList()
        {
            List<string> result = new List<string>();

            try
            {
                // ff.ChangeDir(acc.Path);

                string[] str = mFTP.ListDirectory();//(System.String[])mFTP.ListFiles().ToArray(typeof(System.String);
                string[] splode;

                string s;
                string goodFile;

                bwRemoteViewer.ReportProgress((int)RemoteViewerTask.ProgressType.UPDATE_PROGRESS_MAX, str.Length);
                for (int x = 0; x < str.Length; x++)
                {
                    s = str[x];
                    splode = s.Split(' ');
                    goodFile = splode[splode.Length - 1];
                    result.Add(goodFile);
                    bwRemoteViewer.ReportProgress((int)RemoteViewerTask.ProgressType.ADD_FILE_TO_LISTBOX, goodFile);
                    //if (checkFileTypes(goodFile))
                    //{
                        
                    //}
                }

            }
            catch (System.Exception e)
            {
                System.Windows.Forms.MessageBox.Show(e.Message, "ZScreen FTP");
            }

            return result;
        }



        private void btnCopyToClip_Click(object sender, System.EventArgs e)
        {
            string str = "";

            List<string> items = new List<string>();

            foreach (string obj in lbFiles.SelectedItems)
                items.Add(obj);

            if (cbReverse.Checked)
                items.Reverse();

            foreach (object obj in items)
            {
                str += mAcc.GetUriPath((string)obj) + System.Environment.NewLine;
            }



            if (str != "")
            {
                if (cbAddSpace.Checked)
                    str = str.Insert(0, System.Environment.NewLine);
                str = str.TrimEnd(System.Environment.NewLine.ToCharArray()); ;

                //Set clipboard data
                System.Windows.Forms.Clipboard.SetDataObject(str);
            }
        }

        private void btnDelete_Click(object sender, System.EventArgs e)
        {
            try
            {
                //mFTP.ChangeDir(mAcc.Path);

                System.Collections.ArrayList items = new System.Collections.ArrayList();

                foreach (object obj in lbFiles.SelectedItems)
                    items.Add(obj);

                foreach (object obj in items)
                {
                    if ((string)obj != "")
                    {
                        mFTP.DeleteFile(mAcc.Path + "/" + (string)obj);
                        lbFiles.Items.Remove(obj);
                    }
                }
                //mFTP.Disconnect();
            }
            catch (Exception ex)
            {
                FileSystem.AppendDebug(ex.ToString());
                MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            if (lbFiles.Items.Count > 0)
                lbFiles.SelectedIndex = 0;
        }

        private void ViewRemote_FormClosing(object sender, System.Windows.Forms.FormClosingEventArgs e)
        {
            pbViewer.ImageLocation = "";
            FileSystem.WriteDebugFile();
        }

        private void btnSave_Click(object sender, System.EventArgs e)
        {
            string dir;

            //choose directory / create directory popup
            //remember old directory and display it
            folderBrowseDialog.SelectedPath = Program.ImagesDir;
            folderBrowseDialog.ShowNewFolderButton = true;
            folderBrowseDialog.ShowDialog();

            if (folderBrowseDialog.SelectedPath != "")
            {
                //Program.set.path = folderBrowseDialog.SelectedPath;
                dir = folderBrowseDialog.SelectedPath;
                try
                {
                    //mFTP.ChangeDir(mAcc.Path);
                    foreach (string str in lbFiles.SelectedItems)
                    {
                        mFTP.DownloadFile(str, dir + "\\" + str);

                        //while (mFTP.DoDownload() > 0) ;
                    }
                    //mFTP.Disconnect();
                }
                catch (System.Exception)
                {
                    System.Windows.Forms.MessageBox.Show("Save Failed");
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

        private void pbViewer_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
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

            if (mAcc != null && !string.IsNullOrEmpty(mAcc.Server ))
            {
                //connect();
                mFTP = new FTP(ref mAcc);
                //test code
                //mFTP.timeout = 30000;
                //mFTP.Connect();
                //end of test code
                List<string> files = fetchList();
                if (files.Count > 0)
                {
                    sBwViewFile(files[0]);
                }
                //mFTP.Disconnect();
            }
        }

        private void sBwViewFile(string file)
        {
            string localfile = FileSystem.GetTempFilePath(file);
            if (!System.IO.File.Exists(localfile))
            {
                bwRemoteViewer.ReportProgress((int)RemoteViewerTask.ProgressType.FETCHING_FILE, file);

                try
                {
                    //mFTP.ChangeDir(mAcc.Path);
                    mFTP.DownloadFile(file, localfile);

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
                    else if (FileSystem.IsValidText(fp))
                    {
                        pbViewer.Visible = false ;
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

        private void pbViewer_Click(object sender, System.EventArgs e)
        {

        }

        private void ViewRemote_Shown(object sender, System.EventArgs e)
        {
            RemoteViewerTask rvt = new RemoteViewerTask(RemoteViewerTask.Jobs.FETCH_LIST);
            bwRemoteViewer.RunWorkerAsync(rvt);

        }

        private void tmrSecond_Tick(object sender, System.EventArgs e)
        {
            // sUpdateGuiControls();
        }

        private void sBar_Click(object sender, System.EventArgs e)
        {

        }

        private void tmrFetchFile_Tick(object sender, System.EventArgs e)
        {
            if (bwRemoteViewer.IsBusy)
            {
                if (mBytesDownloaded > 0 && mBytesTotal > 0)
                {
                    if (!(pBar.Value == 0 && mBytesTotal == mBytesDownloaded && mBytesDownloaded < mBytesTotal))
                    {
                        pBar.Maximum = (int)mBytesTotal;
                        pBar.Value = (int)mBytesDownloaded;
                        // System.FileSystem.AppendDebug(string.Format("{0}/{1}", pBar.Value, pBar.Maximum));
                    }
                }
            }

        }

    }
}
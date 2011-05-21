#region License Information (GPL v2)

/*
    ZScreen - A program that allows you to upload screenshots in one keystroke.
    Copyright (C) 2008-2011 ZScreen Developers

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

#endregion License Information (GPL v2)

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;
using UploadersLib.FileUploaders;
using UploadersLib.HelperClasses;
using UploadersLib.ImageUploaders;

namespace UploadersLib
{
    public partial class UploadersConfigForm : Form
    {
        public UploadersConfig Config { get; private set; }
        public UploadersAPIKeys APIKeys { get; private set; }

        public UploadersConfigForm(UploadersConfig uploadersConfig, UploadersAPIKeys uploadersAPIKeys)
        {
            InitializeComponent();
            LoadTabIcons();
            CreateUserControlEvents();
            LoadSettings(uploadersConfig);
            APIKeys = uploadersAPIKeys;
        }

        #region Image uploaders

        #region ImageShack

        private void txtImageShackRegistrationCode_TextChanged(object sender, EventArgs e)
        {
            Config.ImageShackRegistrationCode = txtImageShackRegistrationCode.Text;
        }

        private void txtImageShackUsername_TextChanged(object sender, EventArgs e)
        {
            Config.ImageShackUsername = txtImageShackUsername.Text;
        }

        private void btnImageShackOpenRegistrationCode_Click(object sender, EventArgs e)
        {
            Process.Start("http://profile.imageshack.us/prefs/");
        }

        private void btnImageShackOpenPublicProfile_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Config.ImageShackUsername))
            {
                Process.Start("http://profile.imageshack.us/user/" + Config.ImageShackUsername);
            }
            else
            {
                txtImageShackUsername.Focus();
            }
        }

        private void btnImageShackOpenMyImages_Click(object sender, EventArgs e)
        {
            Process.Start("http://my.imageshack.us/v_images.php");
        }

        #endregion ImageShack

        #region TinyPic

        private void txtTinyPicUsername_TextChanged(object sender, EventArgs e)
        {
            if (Config.TinyPicRememberUserPass)
            {
                Config.TinyPicUsername = txtTinyPicUsername.Text;
            }
        }

        private void txtTinyPicPassword_TextChanged(object sender, EventArgs e)
        {
            if (Config.TinyPicRememberUserPass)
            {
                Config.TinyPicPassword = txtTinyPicPassword.Text;
            }
        }

        private void btnTinyPicLogin_Click(object sender, EventArgs e)
        {
            string username = txtTinyPicUsername.Text;
            string password = txtTinyPicPassword.Text;

            if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password))
            {
                try
                {
                    TinyPicUploader tpu = new TinyPicUploader(APIKeys.TinyPicID, APIKeys.TinyPicKey, txtTinyPicRegistrationCode.Text);
                    string registrationCode = tpu.UserAuth(username, password);

                    if (!string.IsNullOrEmpty(registrationCode))
                    {
                        Config.TinyPicRegistrationCode = registrationCode;
                        txtTinyPicRegistrationCode.Text = registrationCode;
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.ToString());
                }
            }
        }

        private void cbTinyPicRememberUsernamePassword_CheckedChanged(object sender, EventArgs e)
        {
            Config.TinyPicRememberUserPass = cbTinyPicRememberUsernamePassword.Checked;

            if (Config.TinyPicRememberUserPass)
            {
                Config.TinyPicUsername = txtTinyPicUsername.Text;
                Config.TinyPicPassword = txtTinyPicPassword.Text;
            }
            else
            {
                Config.TinyPicUsername = string.Empty;
                Config.TinyPicPassword = string.Empty;
            }
        }

        private void btnTinyPicOpenMyImages_Click(object sender, EventArgs e)
        {
            Process.Start("http://tinypic.com/yourstuff.php");
        }

        #endregion TinyPic

        #region Imgur

        private void cbImgurUseUserAccount_CheckedChanged(object sender, EventArgs e)
        {
            if (cbImgurUseUserAccount.Checked)
            {
                Config.ImgurAccountType = AccountType.User;
            }
            else
            {
                Config.ImgurAccountType = AccountType.Anonymous;
            }
        }

        private void btnImgurOpenAuthorizePage_Click(object sender, EventArgs e)
        {
            ImgurAuthOpen();
        }

        private void btnImgurEnterVerificationCode_Click(object sender, EventArgs e)
        {
            ImgurAuthComplete();
        }

        #endregion Imgur

        #region Flickr

        private void btnFlickrOpenAuthorize_Click(object sender, EventArgs e)
        {
            FlickrAuthOpen();
        }

        private void btnFlickrCompleteAuth_Click(object sender, EventArgs e)
        {
            FlickrAuthComplete();
        }

        private void btnFlickrCheckToken_Click(object sender, EventArgs e)
        {
            FlickrCheckToken();
        }

        private void btnFlickrOpenImages_Click(object sender, EventArgs e)
        {
            FlickrOpenImages();
        }

        #endregion Flickr

        #region TwitPic

        private void cboTwitPicThumbnailMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            Config.TwitPicThumbnailMode = (TwitPicThumbnailType)cboTwitPicThumbnailMode.SelectedIndex;
        }

        private void chkTwitPicShowFull_CheckedChanged(object sender, EventArgs e)
        {
            Config.TwitPicShowFull = chkTwitPicShowFull.Checked;
        }

        #endregion TwitPic

        #endregion Image uploaders

        #region File uploaders

        #region Dropbox

        private void pbDropboxLogo_Click(object sender, EventArgs e)
        {
            Process.Start("https://www.dropbox.com");
        }

        private void btnDropboxRegister_Click(object sender, EventArgs e)
        {
            Process.Start("https://www.dropbox.com/register");
        }

        private void btnDropboxShowFiles_Click(object sender, EventArgs e)
        {
            DropboxOpenFiles();
        }

        private void btnDropboxAuthOpen_Click(object sender, EventArgs e)
        {
            DropboxAuthOpen();
        }

        private void btnDropboxAuthComplete_Click(object sender, EventArgs e)
        {
            DropboxAuthComplete();
        }

        private void txtDropboxPath_TextChanged(object sender, EventArgs e)
        {
            Config.DropboxUploadPath = txtDropboxPath.Text;
            UpdateDropboxStatus();
        }

        #endregion Dropbox

        #region FTP

        private void cboFtpImages_SelectedIndexChanged(object sender, EventArgs e)
        {
            Config.FTPSelectedImage = cboFtpImages.SelectedIndex;
        }

        private void cboFtpText_SelectedIndexChanged(object sender, EventArgs e)
        {
            Config.FTPSelectedText = cboFtpText.SelectedIndex;
        }

        private void cboFtpFiles_SelectedIndexChanged(object sender, EventArgs e)
        {
            Config.FTPSelectedFile = cboFtpFiles.SelectedIndex;
        }

        private void btnFtpHelp_Click(object sender, EventArgs e)
        {
            Process.Start("http://code.google.com/p/zscreen/wiki/FTPAccounts");
        }

        private void btnFTPImport_Click(object sender, EventArgs e)
        {
            FTPAccountsImport();
        }

        private void btnFTPExport_Click(object sender, EventArgs e)
        {
            FTPAccountsExport();
        }

        private void chkFTPThumbnailCheckSize_CheckedChanged(object sender, EventArgs e)
        {
            Config.FTPThumbnailCheckSize = chkFTPThumbnailCheckSize.Checked;
        }

        private void txtFTPThumbWidth_TextChanged(object sender, EventArgs e)
        {
            int width;
            if (int.TryParse(txtFTPThumbWidth.Text, out width))
            {
                Config.FTPThumbnailWidthLimit = width;
            }
        }

        #endregion FTP

        #region RapidShare

        private void cboRapidShareAcctType_SelectedIndexChanged(object sender, EventArgs e)
        {
            Config.RapidShareAccountType = (RapidShareAcctType)cboRapidShareAcctType.SelectedIndex;
            txtRapidSharePremiumUserName.Enabled = Config.RapidShareAccountType == RapidShareAcctType.Premium;
            txtRapidShareCollectorID.Enabled = Config.RapidShareAccountType != RapidShareAcctType.Free && !txtRapidSharePremiumUserName.Enabled;
            txtRapidSharePassword.Enabled = Config.RapidShareAccountType != RapidShareAcctType.Free;
        }

        private void txtRapidShareCollectorID_TextChanged(object sender, EventArgs e)
        {
            Config.RapidShareCollectorsID = txtRapidShareCollectorID.Text;
        }

        private void txtRapidSharePremiumUserName_TextChanged(object sender, EventArgs e)
        {
            Config.RapidSharePremiumUserName = txtRapidSharePremiumUserName.Text;
        }

        private void txtRapidSharePassword_TextChanged(object sender, EventArgs e)
        {
            Config.RapidSharePassword = txtRapidSharePassword.Text;
        }

        #endregion RapidShare

        #region SendSpace

        private void btnSendSpaceRegister_Click(object sender, EventArgs e)
        {
            using (UserPassBox upb = SendSpaceRegister())
            {
                if (upb.Success)
                {
                    txtSendSpaceUserName.Text = upb.UserName;
                    txtSendSpacePassword.Text = upb.Password;
                    cboSendSpaceAcctType.SelectedIndex = (int)AccountType.User;
                }
            }
        }

        private void cboSendSpaceAcctType_SelectedIndexChanged(object sender, EventArgs e)
        {
            Config.SendSpaceAccountType = (AccountType)cboSendSpaceAcctType.SelectedIndex;
            txtSendSpacePassword.Enabled = Config.SendSpaceAccountType == AccountType.User;
            txtSendSpaceUserName.Enabled = Config.SendSpaceAccountType == AccountType.User;
        }

        private void txtSendSpaceUserName_TextChanged(object sender, EventArgs e)
        {
            Config.SendSpaceUsername = txtSendSpaceUserName.Text;
        }

        private void txtSendSpacePassword_TextChanged(object sender, EventArgs e)
        {
            Config.SendSpacePassword = txtSendSpacePassword.Text;
        }

        #endregion SendSpace

        #region MindTouch Deki Wiki

        private void chkDekiWikiForcePath_CheckedChanged(object sender, EventArgs e)
        {
            Config.DekiWikiForcePath = chkDekiWikiForcePath.Checked;
        }

        #endregion MindTouch Deki Wiki

        #endregion File uploaders

        #region Text uploaders

        #region Pastebin

        private void btnPastebinLogin_Click(object sender, EventArgs e)
        {
            PastebinLogin();
        }

        #endregion Pastebin

        #endregion Text uploaders

        #region Other Services

        private void btnTwitterLogin_Click(object sender, EventArgs e)
        {
            TwitterLogin();
        }

        #endregion Other Services

        #region Custom uploader

        private void btnCustomUploaderAdd_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtCustomUploaderName.Text))
            {
                CustomUploaderInfo iUploader = GetCustomUploaderFromFields();
                Config.CustomUploadersList.Add(iUploader);
                lbCustomUploaderList.Items.Add(iUploader.Name);
                lbCustomUploaderList.SelectedIndex = lbCustomUploaderList.Items.Count - 1;
            }
        }

        private void btnCustomUploaderRemove_Click(object sender, EventArgs e)
        {
            if (lbCustomUploaderList.SelectedIndex > -1)
            {
                int selected = lbCustomUploaderList.SelectedIndex;
                Config.CustomUploadersList.RemoveAt(selected);
                lbCustomUploaderList.Items.RemoveAt(selected);
                LoadCustomUploader(new CustomUploaderInfo());
            }
        }

        private void btnCustomUploaderUpdate_Click(object sender, EventArgs e)
        {
            UpdateCustomUploader();
        }

        private void lbCustomUploaderList_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = lbCustomUploaderList.SelectedIndex;

            if (index > -1)
            {
                LoadCustomUploader(Config.CustomUploadersList[index]);
                Config.CustomUploaderSelected = index;
            }
        }

        private void btnCustomUploaderRegexpAdd_Click(object sender, EventArgs e)
        {
            string regexp = txtCustomUploaderRegexp.Text;

            if (!string.IsNullOrEmpty(regexp))
            {
                if (regexp.StartsWith("!tag"))
                {
                    lvCustomUploaderRegexps.Items.Add(String.Format("(?<={0}>).*(?=</{0})",
                        regexp.Substring(4, regexp.Length - 4).Trim()));
                }
                else
                {
                    lvCustomUploaderRegexps.Items.Add(regexp);
                }

                txtCustomUploaderRegexp.Text = string.Empty;
                txtCustomUploaderRegexp.Focus();
            }
        }

        private void btnCustomUploaderRegexpRemove_Click(object sender, EventArgs e)
        {
            if (lvCustomUploaderRegexps.SelectedItems.Count > 0)
            {
                lvCustomUploaderRegexps.SelectedItems[0].Remove();
            }
        }

        private void btnCustomUploaderRegexpEdit_Click(object sender, EventArgs e)
        {
            string regexp = txtCustomUploaderRegexp.Text;

            if (lvCustomUploaderRegexps.SelectedItems.Count > 0 && !string.IsNullOrEmpty(regexp))
            {
                lvCustomUploaderRegexps.SelectedItems[0].Text = regexp;
            }
        }

        private void btnCustomUploaderArgAdd_Click(object sender, EventArgs e)
        {
            string name = txtCustomUploaderArgName.Text;
            string value = txtCustomUploaderArgValue.Text;

            if (!string.IsNullOrEmpty(name))
            {
                lvCustomUploaderArguments.Items.Add(name).SubItems.Add(value);
                txtCustomUploaderArgName.Text = string.Empty;
                txtCustomUploaderArgValue.Text = string.Empty;
                txtCustomUploaderArgName.Focus();
            }
        }

        private void btnCustomUploaderArgRemove_Click(object sender, EventArgs e)
        {
            if (lvCustomUploaderArguments.SelectedItems.Count > 0)
            {
                lvCustomUploaderArguments.SelectedItems[0].Remove();
            }
        }

        private void btnCustomUploaderArgEdit_Click(object sender, EventArgs e)
        {
            string name = txtCustomUploaderArgName.Text;
            string value = txtCustomUploaderArgValue.Text;

            if (lvCustomUploaderArguments.SelectedItems.Count > 0 && !string.IsNullOrEmpty(name))
            {
                lvCustomUploaderArguments.SelectedItems[0].Text = name;
                lvCustomUploaderArguments.SelectedItems[0].SubItems[1].Text = value;
            }
        }

        private void btnCustomUploaderImport_Click(object sender, EventArgs e)
        {
            if (Config.CustomUploadersList == null)
            {
                Config.CustomUploadersList = new List<CustomUploaderInfo>();
            }

            using (OpenFileDialog dlg = new OpenFileDialog { Filter = "ZScreen Image Uploaders(*.zihs)|*.zihs" })
            {
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    ImportImageUploaders(dlg.FileName);
                }
            }
        }

        private void btnCustomUploaderExport_Click(object sender, EventArgs e)
        {
            if (Config.CustomUploadersList != null)
            {
                using (SaveFileDialog dlg = new SaveFileDialog
                {
                    FileName = string.Format("{0}-{1}-uploaders", Application.ProductName, DateTime.Now.ToString("yyyyMMdd")),
                    Filter = "ZScreen Image Uploaders(*.zihs)|*.zihs"
                })
                {
                    if (dlg.ShowDialog() == DialogResult.OK)
                    {
                        CustomUploaderManager ihsm = new CustomUploaderManager
                        {
                            ImageHostingServices = Config.CustomUploadersList
                        };

                        ihsm.Save(dlg.FileName);
                    }
                }
            }
        }

        private void btnCustomUploaderClear_Click(object sender, EventArgs e)
        {
            LoadCustomUploader(new CustomUploaderInfo());
        }

        private void btnCustomUploaderTest_Click(object sender, EventArgs e)
        {
            UpdateCustomUploader();

            if (lbCustomUploaderList.SelectedIndex > -1)
            {
                btnCustomUploaderTest.Enabled = false;

                // TODO: Loader.Worker.StartWorkerScreenshots(WorkerTask.JobLevel2.CustomUploaderTest);
            }
        }

        private void txtCustomUploaderLog_LinkClicked(object sender, LinkClickedEventArgs e)
        {
            Process.Start(e.LinkText);
        }

        #endregion Custom uploader
    }
}
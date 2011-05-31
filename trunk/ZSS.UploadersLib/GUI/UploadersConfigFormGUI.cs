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
using HelpersLib;
using UploadersLib.HelperClasses;
using UploadersLib.ImageUploaders;
using UploadersLib.OtherServices;
using UploadersLib.Properties;

namespace UploadersLib
{
    public partial class UploadersConfigForm : Form
    {
        private void ControlSettings()
        {
            ImageList imageUploadersImageList = new ImageList();
            imageUploadersImageList.ColorDepth = ColorDepth.Depth32Bit;
            imageUploadersImageList.Images.Add("ImageShack", Resources.ImageShack);
            imageUploadersImageList.Images.Add("TinyPic", Resources.TinyPic);
            imageUploadersImageList.Images.Add("Imgur", Resources.Imgur);
            imageUploadersImageList.Images.Add("Flickr", Resources.Flickr);
            imageUploadersImageList.Images.Add("TwitPic", Resources.TwitPic);
            imageUploadersImageList.Images.Add("TwitSnaps", Resources.TwitSnaps);
            imageUploadersImageList.Images.Add("YFrog", Resources.YFrog);
            imageUploadersImageList.Images.Add("MediaWiki", Resources.MediaWiki);
            tcImageUploaders.ImageList = imageUploadersImageList;

            ImageList fileUploadersImageList = new ImageList();
            fileUploadersImageList.ColorDepth = ColorDepth.Depth32Bit;
            fileUploadersImageList.Images.Add("Dropbox", Resources.Dropbox);
            fileUploadersImageList.Images.Add("RapidShare", Resources.RapidShare);
            fileUploadersImageList.Images.Add("SendSpace", Resources.SendSpace);
            tcFileUploaders.ImageList = fileUploadersImageList;

            ImageList textUploadersImageList = new ImageList();
            textUploadersImageList.ColorDepth = ColorDepth.Depth32Bit;
            textUploadersImageList.Images.Add("Pastebin", Resources.Pastebin);
            tcTextUploaders.ImageList = textUploadersImageList;

            ImageList urlShortenersImageList = new ImageList();
            urlShortenersImageList.ColorDepth = ColorDepth.Depth32Bit;
            tcURLShorteners.ImageList = urlShortenersImageList;

            ImageList otherServicesImageList = new ImageList();
            otherServicesImageList.ColorDepth = ColorDepth.Depth32Bit;
            otherServicesImageList.Images.Add("Twitter", Resources.Twitter);
            tcOtherServices.ImageList = otherServicesImageList;

            tpImageShack.ImageKey = "ImageShack";
            tpTinyPic.ImageKey = "TinyPic";
            tpImgur.ImageKey = "Imgur";
            tpFlickr.ImageKey = "Flickr";
            tpTwitPic.ImageKey = "TwitPic";
            tpTwitSnaps.ImageKey = "TwitSnaps";
            tpYFrog.ImageKey = "YFrog";
            tpMediaWiki.ImageKey = "MediaWiki";
            tpDropbox.ImageKey = "Dropbox";
            tpRapidShare.ImageKey = "RapidShare";
            tpSendSpace.ImageKey = "SendSpace";
            tpPastebin.ImageKey = "Pastebin";
            tpTwitter.ImageKey = "Twitter";
        }

        public void LoadSettings(UploadersConfig uploadersConfig)
        {
            Config = uploadersConfig;

            #region Image uploaders

            // ImageShack

            atcImageShackAccountType.SelectedAccountType = Config.ImageShackAccountType;
            txtImageShackRegistrationCode.Text = Config.ImageShackRegistrationCode;
            txtImageShackUsername.Text = Config.ImageShackUsername;
            cbImageShackIsPublic.Checked = Config.ImageShackShowImagesInPublic;

            // TinyPic

            atcTinyPicAccountType.SelectedAccountType = Config.TinyPicAccountType;
            txtTinyPicUsername.Text = Config.TinyPicUsername;
            txtTinyPicPassword.Text = Config.TinyPicPassword;
            cbTinyPicRememberUsernamePassword.Checked = Config.TinyPicRememberUserPass;
            txtTinyPicRegistrationCode.Text = Config.TinyPicRegistrationCode;

            // Imgur

            atcImgurAccountType.SelectedAccountType = Config.ImgurAccountType;

            if (OAuthInfo.CheckOAuth(Config.ImgurOAuthInfo))
            {
                lblImgurAccountStatus.Text = "Login successful: " + Config.ImgurOAuthInfo.UserToken;
            }

            // Flickr

            pgFlickrAuthInfo.SelectedObject = Config.FlickrAuthInfo;
            pgFlickrSettings.SelectedObject = Config.FlickrSettings;

            // TwitPic

            txtTwitPicUsername.Text = Config.TwitPicUsername;
            txtTwitPicPassword.Text = Config.TwitPicPassword;
            chkTwitPicShowFull.Checked = Config.TwitPicShowFull;

            if (cboTwitPicThumbnailMode.Items.Count == 0)
            {
                cboTwitPicThumbnailMode.Items.AddRange(typeof(TwitPicThumbnailType).GetEnumDescriptions());
            }

            cboTwitPicThumbnailMode.SelectedIndex = (int)Config.TwitPicThumbnailMode;

            // YFrog

            txtYFrogUsername.Text = Config.YFrogUsername;
            txtYFrogPassword.Text = Config.YFrogPassword;

            // MediaWiki

            if (Config.MediaWikiAccountList == null || Config.MediaWikiAccountList.Count == 0)
            {
                MediaWikiSetup(new List<MediaWikiAccount>());
            }
            else
            {
                MediaWikiSetup(Config.MediaWikiAccountList);
                if (ucMediaWikiAccounts.AccountsList.Items.Count > 0)
                {
                    ucMediaWikiAccounts.AccountsList.SelectedIndex = Config.MediaWikiAccountSelected;
                }
            }

            #endregion Image uploaders

            #region Text uploaders

            // Pastebin

            pgPastebinSettings.SelectedObject = Config.PastebinSettings;

            #endregion Text uploaders

            #region File uploaders

            // Dropbox

            txtDropboxPath.Text = Config.DropboxUploadPath;
            UpdateDropboxStatus();

            // FTP

            if (Config.FTPAccountList == null || Config.FTPAccountList.Count == 0)
            {
                FTPSetup(new List<FTPAccount>());
            }
            else
            {
                FTPSetup(Config.FTPAccountList);
                if (ucFTPAccounts.AccountsList.Items.Count > 0)
                {
                    ucFTPAccounts.AccountsList.SelectedIndex = 0;
                }
            }

            txtFTPThumbWidth.Text = Config.FTPThumbnailWidthLimit.ToString();
            chkFTPThumbnailCheckSize.Checked = Config.FTPThumbnailCheckSize;

            // RapidShare

            atcRapidShareAccountType.SelectedAccountType = Config.RapidShareUserAccountType;
            txtRapidShareUsername.Text = Config.RapidShareUsername;
            txtRapidSharePassword.Text = Config.RapidSharePassword;

            // SendSpace

            atcSendSpaceAccountType.SelectedAccountType = Config.SendSpaceAccountType;
            txtSendSpacePassword.Text = Config.SendSpacePassword;
            txtSendSpaceUserName.Text = Config.SendSpaceUsername;

            // Localhost

            if (Config.LocalhostAccountList == null || Config.LocalhostAccountList.Count == 0)
            {
                LocalhostAccountsSetup(new List<LocalhostAccount>());
            }
            else
            {
                LocalhostAccountsSetup(Config.LocalhostAccountList);
                if (ucLocalhostAccounts.AccountsList.Items.Count > 0)
                {
                    ucLocalhostAccounts.AccountsList.SelectedIndex = Config.LocalhostSelected;
                }
            }

            // Custom uploaders

            lbCustomUploaderList.Items.Clear();

            if (Config.CustomUploadersList == null)
            {
                Config.CustomUploadersList = new List<CustomUploaderInfo>();
                LoadCustomUploader(new CustomUploaderInfo());
            }
            else
            {
                List<CustomUploaderInfo> iUploaders = Config.CustomUploadersList;
                foreach (CustomUploaderInfo iUploader in iUploaders)
                {
                    lbCustomUploaderList.Items.Add(iUploader.Name);
                }

                if (lbCustomUploaderList.Items.Count > 0)
                {
                    lbCustomUploaderList.SelectedIndex = Config.CustomUploaderSelected;
                }

                if (lbCustomUploaderList.SelectedIndex > -1)
                {
                    LoadCustomUploader(Config.CustomUploadersList[lbCustomUploaderList.SelectedIndex]);
                }
            }

            #endregion File uploaders

            #region Other Services

            ucTwitterAccounts.AccountsList.Items.Clear();

            foreach (OAuthInfo acc in Config.TwitterOAuthInfoList)
            {
                ucTwitterAccounts.AccountsList.Items.Add(acc);
            }

            if (ucTwitterAccounts.AccountsList.Items.Count > 0)
            {
                ucTwitterAccounts.AccountsList.SelectedIndex = Config.TwitterSelectedAccount;
            }

            #endregion Other Services
        }

        private void CreateUserControlEvents()
        {
            // MediaWiki

            ucMediaWikiAccounts.btnAdd.Click += new EventHandler(MediawikiAccountAddButton_Click);
            ucMediaWikiAccounts.btnRemove.Click += new EventHandler(MediawikiAccountRemoveButton_Click);
            ucMediaWikiAccounts.btnTest.Click += new EventHandler(MediawikiAccountTestButton_Click);
            ucMediaWikiAccounts.AccountsList.SelectedIndexChanged += new EventHandler(MediaWikiAccountsList_SelectedIndexChanged);

            // FTP

            ucFTPAccounts.btnAdd.Click += new EventHandler(FTPAccountAddButton_Click);
            ucFTPAccounts.btnRemove.Click += new EventHandler(FTPAccountRemoveButton_Click);
            ucFTPAccounts.btnTest.Click += new EventHandler(FTPAccountTestButton_Click);
            ucFTPAccounts.btnClone.Visible = true;
            ucFTPAccounts.btnClone.Click += new EventHandler(FTPAccountCloneButton_Click);
            ucFTPAccounts.AccountsList.SelectedIndexChanged += new EventHandler(FTPAccountsList_SelectedIndexChanged);
            ucFTPAccounts.SettingsGrid.PropertyValueChanged += new PropertyValueChangedEventHandler(FtpAccountSettingsGrid_PropertyValueChanged);

            // Localhost

            ucLocalhostAccounts.btnAdd.Click += new EventHandler(LocalhostAccountAddButton_Click);
            ucLocalhostAccounts.btnRemove.Click += new EventHandler(LocalhostAccountRemoveButton_Click);
            ucLocalhostAccounts.btnTest.Visible = false;
            ucLocalhostAccounts.AccountsList.SelectedIndexChanged += new EventHandler(LocalhostAccountsList_SelectedIndexChanged);

            // Twitter

            ucTwitterAccounts.btnAdd.Text = "Add";
            ucTwitterAccounts.btnAdd.Click += new EventHandler(TwitterAccountAddButton_Click);
            ucTwitterAccounts.btnRemove.Click += new EventHandler(TwitterAccountRemoveButton_Click);
            ucTwitterAccounts.btnTest.Text = "Authorize";
            ucTwitterAccounts.btnTest.Click += new EventHandler(TwitterAccountAuthButton_Click);
            ucTwitterAccounts.SettingsGrid.PropertySort = PropertySort.NoSort;
            ucTwitterAccounts.AccountsList.SelectedIndexChanged += new EventHandler(TwitterAccountList_SelectedIndexChanged);
        }

        #region MediaWiki

        private void MediawikiAccountAddButton_Click(object sender, EventArgs e)
        {
            MediaWikiAccount acc = new MediaWikiAccount("New Account");
            Config.MediaWikiAccountList.Add(acc);
            ucMediaWikiAccounts.AccountsList.Items.Add(acc);
            ucMediaWikiAccounts.AccountsList.SelectedIndex = ucMediaWikiAccounts.AccountsList.Items.Count - 1;
        }

        private void MediawikiAccountRemoveButton_Click(object sender, EventArgs e)
        {
            int sel = ucMediaWikiAccounts.AccountsList.SelectedIndex;
            if (ucMediaWikiAccounts.RemoveItem(sel))
            {
                Config.MediaWikiAccountList.RemoveAt(sel);
            }
        }

        private void MediaWikiAccountsList_SelectedIndexChanged(object sender, EventArgs e)
        {
            int sel = ucMediaWikiAccounts.AccountsList.SelectedIndex;
            Config.MediaWikiAccountSelected = sel;
            if (Config.MediaWikiAccountList != null && sel != -1 && sel < Config.MediaWikiAccountList.Count && Config.MediaWikiAccountList[sel] != null)
            {
                MediaWikiAccount acc = Config.MediaWikiAccountList[sel];
                ucMediaWikiAccounts.SettingsGrid.SelectedObject = acc;
            }
        }

        private void MediawikiAccountTestButton_Click(object sender, EventArgs e)
        {
            string text = ucMediaWikiAccounts.btnTest.Text;
            ucMediaWikiAccounts.btnTest.Text = "Testing...";
            ucMediaWikiAccounts.btnTest.Enabled = false;
            MediaWikiAccount acc = GetSelectedMediaWiki();
            if (acc != null)
            {
                TestMediaWikiAccount(acc,
                    // callback for success
                    delegate()
                    {
                        // invoke on UI thread
                        Invoke((Action)delegate()
                        {
                            ucMediaWikiAccounts.btnTest.Enabled = true;
                            ucMediaWikiAccounts.btnTest.Text = text;
                            MessageBox.Show("Login successful!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        });
                    },
                    // callback for failure
                    delegate(string message)
                    {
                        // invoke on UI thread
                        Invoke((Action)delegate()
                        {
                            ucMediaWikiAccounts.btnTest.Enabled = true;
                            ucMediaWikiAccounts.btnTest.Text = text;
                            MessageBox.Show(message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        });
                    });
            }
        }

        #endregion MediaWiki

        #region Localhost

        private void LocalhostAccountAddButton_Click(object sender, EventArgs e)
        {
            LocalhostAccount acc = new LocalhostAccount("New Account");
            Config.LocalhostAccountList.Add(acc);
            ucLocalhostAccounts.AccountsList.Items.Add(acc);
            ucLocalhostAccounts.AccountsList.SelectedIndex = ucLocalhostAccounts.AccountsList.Items.Count - 1;
        }

        private void LocalhostAccountRemoveButton_Click(object sender, EventArgs e)
        {
            int sel = ucLocalhostAccounts.AccountsList.SelectedIndex;
            if (ucLocalhostAccounts.RemoveItem(sel))
            {
                Config.LocalhostAccountList.RemoveAt(sel);
            }
        }

        private void LocalhostAccountsList_SelectedIndexChanged(object sender, EventArgs e)
        {
            int sel = ucLocalhostAccounts.AccountsList.SelectedIndex;
            Config.LocalhostSelected = sel;
            if (Config.LocalhostAccountList.CheckSelected(sel))
            {
                LocalhostAccount acc = Config.LocalhostAccountList[sel];
                ucLocalhostAccounts.SettingsGrid.SelectedObject = acc;
            }
        }

        private void LocalhostAccountsSetup(IEnumerable<LocalhostAccount> accs)
        {
            if (accs != null)
            {
                ucLocalhostAccounts.AccountsList.Items.Clear();
                Config.LocalhostAccountList = new List<LocalhostAccount>();
                Config.LocalhostAccountList.AddRange(accs);
                foreach (LocalhostAccount acc in Config.LocalhostAccountList)
                {
                    ucLocalhostAccounts.AccountsList.Items.Add(acc);
                }
            }
        }

        #endregion Localhost

        #region FTP

        private void FTPSetup(IEnumerable<FTPAccount> accs)
        {
            if (accs != null)
            {
                int selFtpList = ucFTPAccounts.AccountsList.SelectedIndex;

                ucFTPAccounts.AccountsList.Items.Clear();
                cboFtpImages.Items.Clear();
                cboFtpText.Items.Clear();
                cboFtpFiles.Items.Clear();

                Config.FTPAccountList = new List<FTPAccount>();
                Config.FTPAccountList.AddRange(accs);

                foreach (FTPAccount acc in Config.FTPAccountList)
                {
                    ucFTPAccounts.AccountsList.Items.Add(acc);
                    cboFtpImages.Items.Add(acc);
                    cboFtpText.Items.Add(acc);
                    cboFtpFiles.Items.Add(acc);
                }

                if (ucFTPAccounts.AccountsList.Items.Count > 0)
                {
                    ucFTPAccounts.AccountsList.SelectedIndex = selFtpList.Between(0, ucFTPAccounts.AccountsList.Items.Count - 1);
                    cboFtpImages.SelectedIndex = Config.FTPSelectedImage.Between(0, ucFTPAccounts.AccountsList.Items.Count - 1);
                    cboFtpText.SelectedIndex = Config.FTPSelectedText.Between(0, ucFTPAccounts.AccountsList.Items.Count - 1);
                    cboFtpFiles.SelectedIndex = Config.FTPSelectedFile.Between(0, ucFTPAccounts.AccountsList.Items.Count - 1);
                }
            }
        }

        private void FTPAccountAddButton_Click(object sender, EventArgs e)
        {
            FTPAccount acc = new FTPAccount("New Account");
            Config.FTPAccountList.Add(acc);
            ucFTPAccounts.AccountsList.Items.Add(acc);
            ucFTPAccounts.AccountsList.SelectedIndex = ucFTPAccounts.AccountsList.Items.Count - 1;
            FTPSetup(Config.FTPAccountList);
        }

        private void FTPAccountRemoveButton_Click(object sender, EventArgs e)
        {
            int sel = ucFTPAccounts.AccountsList.SelectedIndex;
            if (ucFTPAccounts.RemoveItem(sel))
            {
                Config.FTPAccountList.RemoveAt(sel);
            }
            FTPSetup(Config.FTPAccountList);
        }

        private void FTPAccountTestButton_Click(object sender, EventArgs e)
        {
            if (CheckFTPAccounts())
            {
                TestFTPAccount((FTPAccount)Config.FTPAccountList[ucFTPAccounts.AccountsList.SelectedIndex], false);
            }
        }

        private void FTPAccountCloneButton_Click(object sender, EventArgs e)
        {
            FTPAccount src = ucFTPAccounts.AccountsList.Items[ucFTPAccounts.AccountsList.SelectedIndex] as FTPAccount;
            Config.FTPAccountList.Add(src.Clone());
            ucFTPAccounts.AccountsList.SelectedIndex = ucFTPAccounts.AccountsList.Items.Count - 1;
            FTPSetup(Config.FTPAccountList);
        }

        private void FTPAccountsList_SelectedIndexChanged(object sender, EventArgs e)
        {
            int sel = ucFTPAccounts.AccountsList.SelectedIndex;

            if (Config.FTPAccountList.CheckSelected(sel))
            {
                FTPAccount acc = Config.FTPAccountList[sel];
                ucFTPAccounts.SettingsGrid.SelectedObject = acc;
            }
        }

        private void FtpAccountSettingsGrid_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            FTPSetup(Config.FTPAccountList);
        }

        #endregion FTP

        #region Twitter

        private void TwitterAccountAuthButton_Click(object sender, EventArgs e)
        {
            if (CheckTwitterAccounts())
            {
                OAuthInfo acc = TwitterGetActiveAccount();
                Twitter twitter = new Twitter(acc);
                string url = twitter.GetAuthorizationURL();

                if (!string.IsNullOrEmpty(url))
                {
                    Config.TwitterOAuthInfoList[Config.TwitterSelectedAccount] = acc;
                    Process.Start(url);
                    ucTwitterAccounts.SettingsGrid.SelectedObject = acc;
                }
            }
        }

        private void TwitterAccountRemoveButton_Click(object sender, EventArgs e)
        {
            int sel = ucTwitterAccounts.AccountsList.SelectedIndex;
            if (ucTwitterAccounts.RemoveItem(sel))
            {
                Config.TwitterOAuthInfoList.RemoveAt(sel);
            }
        }

        private void TwitterAccountList_SelectedIndexChanged(object sender, EventArgs e)
        {
            int sel = ucTwitterAccounts.AccountsList.SelectedIndex;
            Config.TwitterSelectedAccount = sel;

            if (CheckTwitterAccounts())
            {
                OAuthInfo acc = Config.TwitterOAuthInfoList[sel];
                ucTwitterAccounts.SettingsGrid.SelectedObject = acc;
            }
        }

        private void TwitterAccountAddButton_Click(object sender, EventArgs e)
        {
            OAuthInfo acc = new OAuthInfo(APIKeys.TwitterConsumerKey, APIKeys.TwitterConsumerSecret);
            Config.TwitterOAuthInfoList.Add(acc);
            ucTwitterAccounts.AccountsList.Items.Add(acc);
            ucTwitterAccounts.AccountsList.SelectedIndex = ucTwitterAccounts.AccountsList.Items.Count - 1;
            if (CheckTwitterAccounts())
            {
                ucTwitterAccounts.SettingsGrid.SelectedObject = acc;
            }
        }

        #endregion Twitter
    }
}
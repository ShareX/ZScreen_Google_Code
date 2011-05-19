using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using UploadersLib.Properties;
using UploadersLib.HelperClasses;
using UploadersLib.FileUploaders;

namespace UploadersLib
{
    public partial class UploadersConfigForm : Form
    {
        private void LoadTabIcons()
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

        private void CreateUserControlEvents()
        {
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

        }

        #region Localhost UserControl Events

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
                if (Config.LocalhostAccountList.Count > 0)
                {
                    ucLocalhostAccounts.SettingsGrid.SelectedObject = Config.LocalhostAccountList[sel.Between(0, sel - 1)];
                }
                else
                {
                    ucLocalhostAccounts.SettingsGrid.SelectedObject = null;
                }
            }
        }

        private void LocalhostAccountsList_SelectedIndexChanged(object sender, EventArgs e)
        {
            int sel = ucLocalhostAccounts.AccountsList.SelectedIndex;
            Config.LocalhostSelected = sel;
            if (StaticHelper.CheckList(Config.LocalhostAccountList, sel))
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

        #endregion

        #region FTP UserControl Events

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
                if (Config.FTPAccountList.Count > 0)
                {
                    ucFTPAccounts.SettingsGrid.SelectedObject = Config.FTPAccountList[sel.Between(0, sel - 1)];
                }
                else
                {
                    ucFTPAccounts.SettingsGrid.SelectedObject = null;
                }
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

            if (StaticHelper.CheckList(Config.FTPAccountList, sel))
            {
                FTPAccount acc = Config.FTPAccountList[sel];
                ucFTPAccounts.SettingsGrid.SelectedObject = acc;
            }
        }

        private void FtpAccountSettingsGrid_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            FTPSetup(Config.FTPAccountList);
        }

        #endregion FTP UserControl Events

        public void LoadSettings(UploadersConfig uploadersConfig)
        {
            Config = uploadersConfig;

            #region Image uploaders

            // ImageShack

            txtImageShackRegistrationCode.Text = Config.ImageShackRegistrationCode;
            txtImageShackUsername.Text = Config.ImageShackUsername;
            cbImageShackIsPublic.Checked = Config.ImageShackShowImagesInPublic;

            // TinyPic

            txtTinyPicUsername.Text = Config.TinyPicUsername;
            txtTinyPicPassword.Text = Config.TinyPicPassword;
            cbTinyPicRememberUsernamePassword.Checked = Config.TinyPicRememberUserPass;
            txtTinyPicRegistrationCode.Text = Config.TinyPicRegistrationCode;

            // Imgur

            cbImgurUseUserAccount.Checked = Config.ImgurAccountType == AccountType.User;

            if (OAuthInfo.CheckOAuth(Config.ImgurOAuthInfo))
            {
                lblImgurAccountStatus.Text = "Login successful: " + Config.ImgurOAuthInfo.UserToken;
            }

            #endregion Image uploaders

            #region Text uploaders

            // Pastebin

            pgPastebinSettings.SelectedObject = Config.PastebinSettings;

            #endregion

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

            if (cboRapidShareAcctType.Items.Count == 0)
            {
                cboRapidShareAcctType.Items.AddRange(typeof(RapidShareAcctType).GetDescriptions());
            }

            cboRapidShareAcctType.SelectedIndex = (int)Config.RapidShareAccountType;
            txtRapidShareCollectorID.Text = Config.RapidShareCollectorsID;
            txtRapidSharePassword.Text = Config.RapidSharePassword;
            txtRapidSharePremiumUserName.Text = Config.RapidSharePremiumUserName;

            // SendSpace

            if (cboSendSpaceAcctType.Items.Count == 0)
            {
                cboSendSpaceAcctType.Items.AddRange(typeof(AccountType).GetDescriptions());
            }

            cboSendSpaceAcctType.SelectedIndex = (int)Config.SendSpaceAccountType;
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

            #endregion File uploaders
        }

    }
}

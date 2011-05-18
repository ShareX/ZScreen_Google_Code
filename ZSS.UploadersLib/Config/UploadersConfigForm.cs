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
using System.Diagnostics;
using System.Windows.Forms;
using UploadersLib.HelperClasses;
using UploadersLib.ImageUploaders;
using UploadersLib.Properties;

namespace UploadersLib.Config
{
    public partial class UploadersConfigForm : Form
    {
        public UploadersConfig Config { get; private set; }
        public UploadersAPIKeys APIKeys { get; private set; }

        public UploadersConfigForm(UploadersConfig uploadersConfig, UploadersAPIKeys uploadersAPIKeys)
        {
            InitializeComponent();
            LoadTabIcons();
            APIKeys = uploadersAPIKeys;
            LoadSettings(uploadersConfig);
        }

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

            #region File uploaders

            // Dropbox

            txtDropboxPath.Text = Config.DropboxUploadPath;
            UpdateDropboxStatus();

            #endregion File uploaders
        }

        #region Events

        #region Image uploaders

        // ImageShack

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

        // TinyPic

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

        // Imgur

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

        #endregion Image uploaders

        #region File uploaders

        // Dropbox

        private void pbDropboxLogo_Click(object sender, EventArgs e)
        {
            Process.Start("https://www.dropbox.com");
        }

        private void btnDropboxRegister_Click(object sender, EventArgs e)
        {
            Process.Start("https://www.dropbox.com/register");
        }

        private void btnDropboxAuthOpen_Click(object sender, EventArgs e)
        {
            DropboxAuthOpen();
        }

        private void btnDropboxAuthComplete_Click(object sender, EventArgs e)
        {
            DropboxAuthComplete();
        }

        #endregion File uploaders

        #endregion Events
    }
}
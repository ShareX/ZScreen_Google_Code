using System;
using System.Diagnostics;
using System.Windows.Forms;
using UploadersLib.HelperClasses;
using UploadersLib.ImageUploaders;
using UploadersLib.Properties;

namespace UploadersLib
{
    public partial class UploadersConfigForm : Form
    {
        public UploadersConfig Config { get; private set; }

        public UploadersConfigForm(UploadersConfig uploadersConfig)
        {
            InitializeComponent();
            LoadTabIcons();
            LoadSettings(uploadersConfig);
        }

        private void LoadTabIcons()
        {
            ImageList tabImageList = new ImageList();
            tabImageList.ColorDepth = ColorDepth.Depth32Bit;
            tabImageList.Images.Add("ImageShackIcon", Resources.ImageShackIcon);
            tabImageList.Images.Add("TinyPicIcon", Resources.TinyPicIcon);
            tabImageList.Images.Add("ImgurIcon", Resources.ImgurIcon);
            tabImageList.Images.Add("FlickrIcon", Resources.FlickrIcon);
            tabImageList.Images.Add("DropboxIcon", Resources.DropboxIcon);
            tcImageUploaders.ImageList = tabImageList;
            tpImageShack.ImageKey = "ImageShackIcon";
            tpTinyPic.ImageKey = "TinyPicIcon";
            tpImgur.ImageKey = "ImgurIcon";
            tpFlickr.ImageKey = "FlickrIcon";
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
                lblImgurAccountStatus.Text = "Login success, User token: " + Config.ImgurOAuthInfo.UserToken;
            }

            #endregion Image uploaders
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
            Process.Start("http://my.imageshack.us/registration/");
        }

        private void btnImageShackOpenPublicProfile_Click(object sender, EventArgs e)
        {
            Process.Start("http://profile.imageshack.us/user/" + Config.ImageShackUsername);
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
                    // TODO: API KEYS
                    TinyPicUploader tpu = new TinyPicUploader("", "");
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
            try
            {
                // TODO: API KEYS
                OAuthInfo oauth = new OAuthInfo("", "");

                string url = new Imgur(oauth).GetAuthorizationURL();

                if (!string.IsNullOrEmpty(url))
                {
                    Config.ImgurOAuthInfo = oauth;
                    Process.Start(url);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnImgurEnterVerificationCode_Click(object sender, EventArgs e)
        {
            try
            {
                string verification = txtImgurVerificationCode.Text;

                if (!string.IsNullOrEmpty(verification) && Config.ImgurOAuthInfo != null &&
                    !string.IsNullOrEmpty(Config.ImgurOAuthInfo.AuthToken) && !string.IsNullOrEmpty(Config.ImgurOAuthInfo.AuthSecret))
                {
                    bool result = new Imgur(Config.ImgurOAuthInfo).GetAccessToken(verification);

                    if (result)
                    {
                        lblImgurAccountStatus.Text = "Login success, User token: " + Config.ImgurOAuthInfo.UserToken;
                        MessageBox.Show("Login success.", "ZScreen", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        lblImgurAccountStatus.Text = "Login failed.";
                        MessageBox.Show("Login failed.", "ZScreen", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion Image uploaders

        #endregion Events
    }
}
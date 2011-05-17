using System;
using System.Diagnostics;
using System.Text;
using System.Windows.Forms;
using HelpersLib;
using UploadersLib.FileUploaders;
using UploadersLib.HelperClasses;
using UploadersLib.ImageUploaders;

namespace UploadersLib.Config
{
    public partial class UploadersConfigForm : Form
    {
        // Imgur

        public void ImgurAuthOpen()
        {
            try
            {
                OAuthInfo oauth = new OAuthInfo(APIKeys.ImgurConsumerKey, APIKeys.ImgurConsumerSecret);

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

        public void ImgurAuthComplete()
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
                        lblImgurAccountStatus.Text = "Login success: " + Config.ImgurOAuthInfo.UserToken;
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

        // Dropbox

        public void DropboxAuthOpen()
        {
            try
            {
                OAuthInfo oauth = new OAuthInfo(APIKeys.DropboxConsumerKey, APIKeys.DropboxConsumerSecret);

                string url = new Dropbox(oauth).GetAuthorizationURL();

                if (!string.IsNullOrEmpty(url))
                {
                    Config.DropboxOAuthInfo = oauth;
                    Process.Start(url);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void DropboxAuthComplete()
        {
            if (Config.DropboxOAuthInfo != null && !string.IsNullOrEmpty(Config.DropboxOAuthInfo.AuthToken) &&
                !string.IsNullOrEmpty(Config.DropboxOAuthInfo.AuthSecret))
            {
                Dropbox dropbox = new Dropbox(Config.DropboxOAuthInfo);
                bool result = dropbox.GetAccessToken();

                if (result)
                {
                    DropboxAccountInfo account = dropbox.GetAccountInfo();

                    if (account != null)
                    {
                        Config.DropboxEmail = account.Email;
                        Config.DropboxName = account.Display_name;
                        Config.DropboxUserID = account.Uid.ToString();
                        Config.DropboxUploadPath = txtDropboxPath.Text;
                        UpdateDropboxStatus();
                        MessageBox.Show("Login successful.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    else
                    {
                        MessageBox.Show("GetAccountInfo failed.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Login failed.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("You must give access to ZScreen from Authorize page first.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            Config.DropboxOAuthInfo = null;
            UpdateDropboxStatus();
        }

        private void UpdateDropboxStatus()
        {
            if (Config.DropboxOAuthInfo != null && !string.IsNullOrEmpty(Config.DropboxOAuthInfo.UserToken) &&
                !string.IsNullOrEmpty(Config.DropboxOAuthInfo.UserSecret))
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendLine("Login status: Success");
                sb.AppendLine("Email: " + Config.DropboxEmail);
                sb.AppendLine("Name: " + Config.DropboxName);
                sb.AppendLine("User ID: " + Config.DropboxUserID);
                string uploadPath = new NameParser { IsFolderPath = true }.Convert(Dropbox.TidyUploadPath(Config.DropboxUploadPath));
                if (!string.IsNullOrEmpty(uploadPath))
                {
                    sb.AppendLine("Upload path: " + uploadPath);
                    sb.AppendLine("Download path: " + Dropbox.GetDropboxURL(Config.DropboxUserID, uploadPath, "{Filename}"));
                }
                lblDropboxStatus.Text = sb.ToString();
            }
            else
            {
                lblDropboxStatus.Text = "Login status: Authorize required";
            }
        }
    }
}
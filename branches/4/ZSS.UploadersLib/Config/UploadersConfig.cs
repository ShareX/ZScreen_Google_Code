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
using System.ComponentModel;
using HelpersLib;
using UploadersLib.FileUploaders;
using UploadersLib.HelperClasses;
using UploadersLib.ImageUploaders;
using UploadersLib.TextUploaders;

namespace UploadersLib
{
    [Serializable]
    public class UploadersConfig
    {
        #region Constructor

        public UploadersConfig()
        {
            ApplyDefaultValues(this);
        }

        #endregion Constructor

        #region General

        [Category(ComponentModelStrings.AppPathsUploaders), ReadOnly(true), Description("File Path of the Uploaders Configuration")]
        public string FilePath { get; set; }

        [Category(ComponentModelStrings.AppPasswords), DefaultValue(false), Description("Encrypt passwords using AES")]
        public bool PasswordsSecureUsingEncryption { get; set; }

        [Browsable(false), Category(ComponentModelStrings.AppPasswords), DefaultValue(EncryptionStrength.High), Description("Strength can be Low = 128, Medium = 192, or High = 256")]
        public EncryptionStrength PasswordsEncryptionStrength2 { get; set; }

        #endregion General

        #region Image uploaders

        // ImageShack

        public AccountType ImageShackAccountType = AccountType.Anonymous;
        public string ImageShackRegistrationCode = string.Empty;
        public string ImageShackUsername = string.Empty;
        public bool ImageShackShowImagesInPublic = false;

        // TinyPic

        public AccountType TinyPicAccountType = AccountType.Anonymous;
        public string TinyPicRegistrationCode = string.Empty;
        public string TinyPicUsername = string.Empty;
        public string TinyPicPassword = string.Empty;
        public bool TinyPicRememberUserPass = false;

        // Imgur

        public AccountType ImgurAccountType = AccountType.Anonymous;
        public ImgurThumbnailType ImgurThumbnailType = ImgurThumbnailType.Large_Thumbnail;
        public OAuthInfo ImgurOAuthInfo = null;

        // Flickr

        public FlickrAuthInfo FlickrAuthInfo = new FlickrAuthInfo();
        public FlickrSettings FlickrSettings = new FlickrSettings();

        // Photobucket

        public OAuthInfo PhotobucketOAuthInfo = null;
        public PhotobucketAccountInfo PhotobucketAccountInfo = null;

        // TwitPic

        public string TwitPicUsername = string.Empty;
        public string TwitPicPassword = string.Empty;
        public bool TwitPicShowFull = true;
        public TwitPicThumbnailType TwitPicThumbnailMode = TwitPicThumbnailType.Thumb;

        // YFrog

        public string YFrogUsername = string.Empty;
        public string YFrogPassword = string.Empty;

        // MediaWiki

        public List<MediaWikiAccount> MediaWikiAccountList = new List<MediaWikiAccount>();
        public int MediaWikiAccountSelected = 0;

        #endregion Image uploaders

        #region File uploaders

        // FTP

        public List<FTPAccount> FTPAccountList2 = new List<FTPAccount>();
        public int FTPSelectedImage = 0;
        public int FTPSelectedText = 0;
        public int FTPSelectedFile = 0;
        public int FTPThumbnailWidthLimit = 150;
        // If image size smaller than thumbnail size then not make thumbnail
        public bool FTPThumbnailCheckSize = true;

        // Email

        public string EmailSmtpServer = "smtp.gmail.com";
        public int EmailSmtpPort = 587;
        public string EmailFrom = "...@gmail.com";
        public string EmailPassword = string.Empty;
        public bool EmailRememberLastTo = true;
        public bool EmailConfirmSend = true;
        public string EmailLastTo = string.Empty;
        public string EmailDefaultSubject = "Sending Email from ZScreen";
        public string EmailDefaultBody = "Screenshot is attached.";

        // Dropbox

        public OAuthInfo DropboxOAuthInfo = null;
        public string DropboxUploadPath = "Public/ZScreen/%y-%mo";
        public DropboxAccountInfo DropboxAccountInfo = null;

        // Minus

        public MinusOptions MinusConfig = new MinusOptions();

        // RapidShare

        public string RapidShareUsername = string.Empty;
        public string RapidSharePassword = string.Empty;

        // SendSpace

        public AccountType SendSpaceAccountType = AccountType.Anonymous;
        public string SendSpaceUsername = string.Empty;
        public string SendSpacePassword = string.Empty;

        // Custom Uploaders

        public List<CustomUploaderInfo> CustomUploadersList = new List<CustomUploaderInfo>();
        public int CustomUploaderSelected = 0;

        #endregion File uploaders

        #region Text uploaders

        // Pastebin

        public PastebinSettings PastebinSettings = new PastebinSettings();

        #endregion Text uploaders

        #region URL shorteners

        public AccountType GoogleURLShortenerAccountType = AccountType.Anonymous;
        public OAuthInfo GoogleURLShortenerOAuthInfo = null;

        #endregion URL shorteners

        #region Other services

        // Twitter

        public List<OAuthInfo> TwitterOAuthInfoList = new List<OAuthInfo>();
        public int TwitterSelectedAccount = 0;
        public TwitterClientSettings TwitterClientConfig = new TwitterClientSettings();
        public bool TwitterEnabled = false;

        #endregion Other services

        #region Other destinations

        // Localhost

        public List<LocalhostAccount> LocalhostAccountList = new List<LocalhostAccount>();
        public int LocalhostSelectedImages = 0;
        public int LocalhostSelectedText = 0;
        public int LocalhostSelectedFiles = 0;

        #endregion Other destinations

        #region Helper Methods

        public static void ApplyDefaultValues(object self)
        {
            foreach (PropertyDescriptor prop in TypeDescriptor.GetProperties(self))
            {
                DefaultValueAttribute attr = prop.Attributes[typeof(DefaultValueAttribute)] as DefaultValueAttribute;
                if (attr == null) continue;
                prop.SetValue(self, attr.Value);
            }
        }

        public bool IsActive(FileUploaderType ut)
        {
            switch (ut)
            {
                case FileUploaderType.Dropbox:
                    return DropboxOAuthInfo != null && !string.IsNullOrEmpty(DropboxOAuthInfo.UserToken) && !string.IsNullOrEmpty(DropboxOAuthInfo.UserSecret);
                case FileUploaderType.RapidShare:
                    return !string.IsNullOrEmpty(RapidShareUsername) && !string.IsNullOrEmpty(RapidSharePassword);
                case FileUploaderType.SendSpace:
                    return SendSpaceAccountType == AccountType.Anonymous || (!string.IsNullOrEmpty(SendSpaceUsername) && !string.IsNullOrEmpty(SendSpacePassword));
                case FileUploaderType.Minus:
                    return MinusConfig != null && MinusConfig.MinusUser != null;
                case FileUploaderType.CustomUploader:
                    return CustomUploadersList != null && CustomUploadersList.Count > 0;
                case FileUploaderType.FTP:
                    return FTPAccountList2 != null && FTPAccountList2.Count > 0;
            }

            return false;
        }

        public void CryptPasswords(bool bEncrypt)
        {
            StaticHelper.WriteLine((bEncrypt ? "Encrypting " : "Decrypting") + " passwords.");

            CryptKeys crypt = new CryptKeys() { KeySize = this.PasswordsEncryptionStrength2 };

            this.TinyPicPassword =
                bEncrypt ? crypt.Encrypt(this.TinyPicPassword) :
            crypt.Decrypt(this.TinyPicPassword);

            this.RapidSharePassword =
                bEncrypt ? crypt.Encrypt(this.RapidSharePassword) :
            crypt.Decrypt(this.RapidSharePassword);

            this.SendSpacePassword = bEncrypt ? crypt.Encrypt(this.SendSpacePassword) :
            crypt.Decrypt(this.SendSpacePassword);

            foreach (FTPAccount acc in this.FTPAccountList2)
            {
                acc.Password = bEncrypt ? crypt.Encrypt(acc.Password) : crypt.Decrypt(acc.Password);
                acc.Passphrase = bEncrypt ? crypt.Encrypt(acc.Passphrase) : crypt.Decrypt(acc.Passphrase);
            }

            foreach (LocalhostAccount acc in this.LocalhostAccountList)
            {
                acc.Password = bEncrypt ? crypt.Encrypt(acc.Password) : crypt.Decrypt(acc.Password);
            }

            this.TwitPicPassword = bEncrypt ? crypt.Encrypt(this.TwitPicPassword) :
                crypt.Decrypt(this.TwitPicPassword);

            this.EmailPassword = bEncrypt ? crypt.Encrypt(this.EmailPassword) :
            crypt.Decrypt(this.EmailPassword);
        }

        public bool IsActive(TextUploaderType ut)
        {
            switch (ut)
            {
                case TextUploaderType.FileUploader:
                    foreach (FileUploaderType fu in Enum.GetValues(typeof(FileUploaderType)))
                    {
                        if (IsActive(fu)) return true;
                    }
                    return false;
                default:
                    return true;
            }
        }

        public bool IsActive(ImageUploaderType ut)
        {
            switch (ut)
            {
                case ImageUploaderType.FLICKR:
                    return !string.IsNullOrEmpty(FlickrAuthInfo.Token);
                case ImageUploaderType.IMAGESHACK:
                    return ImageShackAccountType == AccountType.Anonymous || !string.IsNullOrEmpty(ImageShackRegistrationCode);
                case ImageUploaderType.TINYPIC:
                    return TinyPicAccountType == AccountType.Anonymous || !string.IsNullOrEmpty(TinyPicRegistrationCode);
                case ImageUploaderType.IMGUR:
                    return ImgurOAuthInfo != null && !string.IsNullOrEmpty(ImgurOAuthInfo.UserToken) && !string.IsNullOrEmpty(ImgurOAuthInfo.UserSecret);
                case ImageUploaderType.MEDIAWIKI:
                    return MediaWikiAccountList.Count > 0;
                case ImageUploaderType.Photobucket:
                    return PhotobucketAccountInfo != null && PhotobucketOAuthInfo != null;
                case ImageUploaderType.TWITPIC:
                    return !string.IsNullOrEmpty(TwitPicPassword);
                case ImageUploaderType.TWITSNAPS:
                    return TwitterOAuthInfoList.Count > 0;
                case ImageUploaderType.UPLOADSCREENSHOT:
                    return true;
                case ImageUploaderType.YFROG:
                    return !string.IsNullOrEmpty(YFrogPassword);
                case ImageUploaderType.FileUploader:
                    foreach (FileUploaderType fu in Enum.GetValues(typeof(FileUploaderType)))
                    {
                        if (IsActive(fu)) return true;
                    }
                    return false;
            }

            return false;
        }

        public int GetFtpIndex(EDataType dataType)
        {
            switch (dataType)
            {
                case EDataType.Image:
                    return FTPSelectedImage;
                case EDataType.Text:
                    return FTPSelectedText;
                default:
                    return FTPSelectedFile;
            }
        }

        #endregion Helper Methods

        #region I/O Methods

        public bool Write(string filePath)
        {
            bool succ = true;

            // encrypt before interim save
            if (this.PasswordsSecureUsingEncryption)
            {
                this.CryptPasswords(bEncrypt: true);
            }

            succ = SettingsHelper.Save(this, filePath, SerializationType.Xml);

            // decrypt after interim save
            if (this.PasswordsSecureUsingEncryption)
            {
                this.CryptPasswords(bEncrypt: false);
            }

            return succ;
        }

        public static UploadersConfig Read(string filePath)
        {
            UploadersConfig uc = SettingsHelper.Load<UploadersConfig>(filePath, SerializationType.Xml);
            uc.FilePath = filePath;
            return uc;
        }

        #endregion I/O Methods
    }
}
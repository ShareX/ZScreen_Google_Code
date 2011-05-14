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

using System.Collections.Generic;
using UploadersLib.FileUploaders;
using UploadersLib.HelperClasses;
using UploadersLib.ImageUploaders;
using UploadersLib.TextUploaders;

namespace UploadersLib
{
    public class UploadersConfig
    {
        #region Image uploaders

        // ImageShack

        public string ImageShackRegistrationCode = string.Empty;
        public string ImageShackUsername = string.Empty;
        public bool ImageShackShowImagesInPublic = false;

        // TinyPic

        public string TinyPicShuk = string.Empty;
        public string TinyPicUsername = string.Empty;
        public string TinyPicPassword = string.Empty;
        public bool TinyPicRememberUserPass = false;

        // Imgur

        public OAuthInfo ImgurOAuthInfo = null;
        public AccountType ImgurAccountType = AccountType.Anonymous;

        // Flickr

        public FlickrAuthInfo FlickrAuthInfo = new FlickrAuthInfo();
        public FlickrSettings FlickrSettings = new FlickrSettings();

        // TwitPic

        public bool TwitPicShowFull = true;
        public TwitPicThumbnailType TwitPicThumbnailMode = TwitPicThumbnailType.Thumb;

        // MediaWiki

        public List<MediaWikiAccount> MediaWikiAccountList = new List<MediaWikiAccount>();
        public int MediaWikiAccountSelected = 0;

        // DekiWiki

        public List<DekiWikiAccount> DekiWikiAccountList = new List<DekiWikiAccount>();
        public int DekiWikiSelected = 0;
        public bool DekiWikiForcePath = false;

        #endregion Image uploaders

        #region File uploaders

        // FTP

        public List<FTPAccount> FTPAccountList = new List<FTPAccount>();
        public int FTPSelectedImage = 0;
        public int FTPSelectedText = 0;
        public int FTPSelectedFile = 0;
        public int FTPThumbnailWidthLimit = 150;
        // If image size smaller than thumbnail size then not make thumbnail
        public bool FTPThumbnailCheckSize = true;

        // Dropbox

        public OAuthInfo DropboxOAuthInfo = null;
        public string DropboxUploadPath = "Public/ZScreen/%y-%mo";
        public string DropboxEmail = string.Empty;
        public string DropboxName = string.Empty;
        public string DropboxUserID = string.Empty;

        // RapidShare

        public string RapidSharePremiumUserName = string.Empty;
        public string RapidShareCollectorsID = string.Empty;
        public string RapidSharePassword = string.Empty;
        public RapidShareAcctType RapidShareAccountType = RapidShareAcctType.Free;

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
        public int LocalhostSelected = 0;

        #endregion Other destinations
    }
}
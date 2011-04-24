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

using System.ComponentModel;

namespace UploadersLib
{
    public enum HttpMethod
    {
        GET, POST
    }

    public enum Proxy
    {
        [Description("HTTP Proxy")]
        HTTP,
        [Description("SOCKS v4 Proxy")]
        SOCKS4,
        [Description("SOCKS v4a Proxy")]
        SOCKS4a,
        [Description("SOCKS v5 Proxy")]
        SOCKS5
    }

    public enum Protocol
    {
        [Description("http://")]
        Http,
        [Description("https://")]
        Https,
        [Description("file://")]
        File
    }

    public enum LinkType
    {
        FULLIMAGE,
        THUMBNAIL,
        DELETION_LINK,
        FULLIMAGE_TINYURL
    }

    public enum URLType
    {
        URL,
        ThumbnailURL,
        DeletionURL
    }

    #region Image Uploaders

    public enum ImageDestType
    {
        [Description("FTP Server")]
        FTP,
        [Description("File Hosting Service")]
        FileUploader,
        [Description("Localhost")]
        Localhost,
        [Description("Clipboard")]
        CLIPBOARD,
        [Description("File")]
        FILE,
        [Description("ImageShack - imageshack.us")]
        IMAGESHACK,
        [Description("TinyPic - tinypic.com")]
        TINYPIC,
        [Description("Flickr - flickr.com")]
        FLICKR,
        //[Description("ImageBin - imagebin.ca")]
        //IMAGEBIN,
        //[Description("Img1 - img1.us")]
        //IMG1,
        [Description("ImageBam - imagebam.com")]
        IMAGEBAM,
        [Description("Imgur - imgur.com")]
        IMGUR,
        [Description("Uploadscreenshot - uploadscreenshot.com")]
        UPLOADSCREENSHOT,
        [Description("TwitPic - twitpic.com")]
        TWITPIC,
        [Description("TwitSnaps - twitsnaps.com")]
        TWITSNAPS,
        [Description("yFrog - yfrog.com")]
        YFROG,
        [Description("MindTouch Deki Wiki")]
        DEKIWIKI,
        [Description("MediaWiki")]
        MEDIAWIKI,
        [Description("Printer")]
        PRINTER
    }

    public enum UploadMode
    {
        [Description("User")]
        API,
        [Description("Anonymous")]
        ANONYMOUS
    }

    public enum ClipboardUriType
    {
        [Description("Full Image")]
        FULL,
        [Description("Full Image for Forums")]
        FULL_IMAGE_FORUMS,
        [Description("Full Image as HTML")]
        FULL_IMAGE_HTML,
        [Description("Full Image for Wiki")]
        FULL_IMAGE_WIKI,
        [Description("Full Image Link for MediaWiki")]
        FULL_IMAGE_MEDIAWIKI,
        [Description("Full Image for Twitter")]
        FULL_TINYURL,
        [Description("Linked Thumbnail for Forums")]
        LINKED_THUMBNAIL,
        [Description("Linked Thumbnail as HTML")]
        LinkedThumbnailHtml,
        [Description("Linked Thumbnail for Wiki")]
        LINKED_THUMBNAIL_WIKI,
        [Description("Thumbnail")]
        THUMBNAIL,
        [Description("Local File as URI")]
        LocalFilePathUri,
        [Description("Local File")]
        LocalFilePath
    }

    #endregion Image Uploaders

    #region Text Uploaders & URL Shorteners

    public enum TextDestination
    {
        [Description("pastebin.com")]
        PASTEBIN,
        [Description("pastebin.ca")]
        PASTEBIN_CA,
        [Description("paste2.org")]
        PASTE2,
        [Description("slexy.org")]
        SLEXY,
        [Description("Use file uploader")]
        FILE
    }

    public enum UrlShortenerType
    {
        [Description("Google - www.goo.gl")]
        Google,
        [Description("bit.ly - www.bit.ly")]
        BITLY,
        [Description("Threely - www.3.ly")]
        THREELY,
        [Description("j.mp - www.j.mp")]
        Jmp,
        [Description("is.gd - www.is.gd")]
        ISGD,
        [Description("TinyURL - www.tinyurl.com")]
        TINYURL,
        [Description("TURL - www.turl.ca")]
        TURL
        //[Description("kl.am - www.kl.am")]
        //KLAM
    }

    public enum Privacy
    {
        Public,
        Private
    }

    #endregion Text Uploaders & URL Shorteners

    #region File Uploaders

    public enum FileUploaderType
    {
        [Description("FTP Server")]
        FTP,
        [Description("RapidShare - www.rapidshare.com")]
        RapidShare,
        [Description("SendSpace - www.sendspace.com")]
        SendSpace,
        [Description("Dropbox - www.dropbox.com")]
        Dropbox,
        //[Description("FileBin - www.filebin.ca")]
        //FileBin,
        //[Description("Drop.io - www.drop.io")]
        //DropIO,
        [Description("ShareCX - www.share.cx")]
        ShareCX,
        [Description("Filez - www.filez.muffinz.eu")]
        FilezFiles,
        [Description("Custom Uploader")]
        CUSTOM_UPLOADER
    }

    public enum RapidShareAcctType
    {
        [Description("Anonymous")]
        Free,
        [Description("Collector's Account")]
        Collectors,
        [Description("Premium Account")]
        Premium
    }

    public enum AcctType
    {
        [Description("Anonymous")]
        Anonymous,
        [Description("User")]
        User
    }

    #endregion File Uploaders
}
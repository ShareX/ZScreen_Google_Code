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

    public enum ResponseType
    {
        Text, RedirectionURL
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
        URL,
        ThumbnailURL,
        DeletionLink,
        FULLIMAGE_TINYURL
    }

    public enum URLType
    {
        URL, ThumbnailURL, DeletionURL
    }

    public enum Privacy
    {
        Public, Private
    }

    public enum AccountType
    {
        [Description("Anonymous")]
        Anonymous,
        [Description("User")]
        User
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

    public enum ImageUploaderType
    {
        [Description("Clipboard - Copy Bitmap")]
        CLIPBOARD,
        [Description("File - Copy local file path")]
        FILE,
        [Description("Localhost")]
        Localhost,
        [Description("Printer")]
        PRINTER,
        [Description("ImageShack - imageshack.us")]
        IMAGESHACK,
        [Description("TinyPic - tinypic.com")]
        TINYPIC,
        [Description("Imgur - imgur.com")]
        IMGUR,
        [Description("Flickr - flickr.com")]
        FLICKR,
        [Description("Uploadscreenshot - uploadscreenshot.com")]
        UPLOADSCREENSHOT,
        [Description("TwitPic - twitpic.com")]
        TWITPIC,
        [Description("TwitSnaps - twitsnaps.com")]
        TWITSNAPS,
        [Description("yfrog - yfrog.com")]
        YFROG,
        [Description("MindTouch Deki Wiki")]
        DEKIWIKI,
        [Description("MediaWiki")]
        MEDIAWIKI,
        [Description("Use file uploader")]
        FileUploader
    }

    public enum TextUploaderType
    {
        [Description("Pastebin - pastebin.com")]
        PASTEBIN,
        [Description("Pastebin - pastebin.ca")]
        PASTEBIN_CA,
        [Description("Paste2 - paste2.org")]
        PASTE2,
        [Description("Slexy 2.0 - slexy.org")]
        SLEXY,
        [Description("Use file uploader")]
        FileUploader
    }

    public enum FileUploaderType
    {
        [Description("RapidShare - rapidshare.com")]
        RapidShare,
        [Description("SendSpace - sendspace.com")]
        SendSpace,
        [Description("Dropbox - dropbox.com")]
        Dropbox,
        [Description("FileSonic - filesonic.com")]
        FileSonic,
        [Description("ShareCX - share.cx")]
        ShareCX,
        [Description("Use custom uploader")]
        CustomUploader,
        [Description("FTP Server")]
        FTP
    }

    public enum UrlShortenerType
    {
        [Description("Google - www.goo.gl")]
        Google,
        [Description("bit.ly - www.bit.ly")]
        BITLY,
        //[Description("Threely - www.3.ly")]
        //THREELY,
        [Description("j.mp - www.j.mp")]
        Jmp,
        [Description("is.gd - www.is.gd")]
        ISGD,
        [Description("TinyURL - www.tinyurl.com")]
        TINYURL,
        [Description("TURL - www.turl.ca")]
        TURL
    }
}
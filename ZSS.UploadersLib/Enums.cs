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

    public enum OutputTypeEnum
    {
        [Description("Capture")]
        Bitmap,
        [Description("Local file path")]
        Local,
        [Description("Uploaded URL")]
        Remote,
    }

    public enum LinkFormatEnum
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
        [Description("Local File path")]
        LocalFilePath,
        [Description("Local File path as URI")]
        LocalFilePathUri,
    }

    public enum ImageUploaderType
    {
        [Description("Shared folder")]
        SharedFolder,
        [Description("imageshack.us")]
        IMAGESHACK,
        [Description("tinypic.com")]
        TINYPIC,
        [Description("imgur.com")]
        IMGUR,
        [Description("flickr.com")]
        FLICKR,
        [Description("uploadscreenshot.com")]
        UPLOADSCREENSHOT,
        [Description("twitpic.com")]
        TWITPIC,
        [Description("twitsnaps.com")]
        TWITSNAPS,
        [Description("yfrog.com")]
        YFROG,
        [Description("MediaWiki")]
        MEDIAWIKI,
        [Description("File Uploader")]
        FileUploader,
        [Description("Printer")]
        Printer
    }

    public enum TextUploaderType
    {
        [Description("pastebin.com")]
        PASTEBIN,
        [Description("pastebin.ca")]
        PASTEBIN_CA,
        [Description("paste2.org")]
        PASTE2,
        [Description("slexy.org")]
        SLEXY,
        [Description("File Uploader")]
        FileUploader,
    }

    public enum FileUploaderType
    {
        [Description("rapidshare.com")]
        RapidShare,
        [Description("sendspace.com")]
        SendSpace,
        [Description("dropbox.com")]
        Dropbox,
        [Description("share.cx")]
        ShareCX,
        [Description("Custom Uploader")]
        CustomUploader,
        [Description("FTP Server")]
        FTP
    }

    public enum UrlShortenerType
    {
        [Description("goo.gl")]
        Google,
        [Description("bit.ly")]
        BITLY,
        //[Description("Threely - www.3.ly")]
        //THREELY,
        [Description("j.mp")]
        Jmp,
        [Description("is.gd")]
        ISGD,
        [Description("tinyurl.com")]
        TINYURL,
        [Description("turl.ca")]
        TURL
    }
}
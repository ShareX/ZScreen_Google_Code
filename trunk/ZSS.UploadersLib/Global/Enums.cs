#region License Information (GPL v2)
/*
    ZScreen - A program that allows you to upload screenshots in one keystroke.
    Copyright (C) 2008-2009  Brandon Zimmerman

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
#endregion

using System;
using System.ComponentModel;

namespace UploadersLib
{
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

    #region Image Uploaders

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
        [Description("Thumbnail")]
        THUMBNAIL,
        [Description("Linked Thumbnail for Forums")]
        LINKED_THUMBNAIL,
        [Description("Linked Thumbnail for Wiki")]
        LINKED_THUMBNAIL_WIKI,
        [Description("Full Image for Twitter")]
        FULL_TINYURL
    }

    public enum ImageDestType
    {
        [Description("FTP Server")]
        FTP,
        [Description("File Hosting Service")]
        FileUploader,
        [Description("Clipboard")]
        CLIPBOARD,
        [Description("File")]
        FILE,
        [Description("ImageShack - www.imageshack.us")]
        IMAGESHACK,
        [Description("TinyPic - www.tinypic.com")]
        TINYPIC,
        [Description("Flickr - www.flickr.com")]
        FLICKR,
        [Description("ImageBin - www.imagebin.ca")]
        IMAGEBIN,
        [Description("ImageBam - www.imagebam.com")]
        IMAGEBAM,
        [Description("TwitPic - www.twitpic.com")]
        TWITPIC,
        [Description("TwitSnaps - www.twitsnaps.com")]
        TWITSNAPS,
        [Description("yFrog - www.yfrog.com")]
        YFROG,
        [Description("Custom Uploader")]
        CUSTOM_UPLOADER,
        [Description("MindTouch Deki Wiki")]
        DEKIWIKI,
        [Description("Printer")]
        PRINTER
    }

    #endregion

    #region Text Uploaders & URL Shorteners

    [Serializable]
    public enum TextDestType
    {
        [Description("PasteBin - www.pastebin.com")]
        PASTEBIN,
        [Description("PasteBin - www.pastebin.ca")]
        PASTEBIN_CA,
        [Description("Paste2 - www.paste2.org")]
        PASTE2,
        [Description("Slexy 2.0 - www.slexy.org")]
        SLEXY
        //[Description("Snipt - www.snipt.org")]
        //SNIPT
    }

    [Serializable]
    public enum UrlShortenerType
    {
        [Description("Threely - www.3.ly")]
        THREELY,
        [Description("bit.ly - www.bit.ly")]
        BITLY,
        [Description("j.mp - www.j.mp")]
        Jmp,
        [Description("is.gd - www.is.gd")]
        ISGD,
        [Description("kl.am - www.kl.am")]
        KLAM,
        //[Description("Ow.ly - www.ow.ly")]
        //OWLY,
        [Description("TinyURL - www.tinyurl.com")]
        TINYURL,
        [Description("TURL - www.turl.ca")]
        TURL
    }

    [Serializable]
    public enum Privacy
    {
        Public,
        Private
    }

    #endregion

    #region File Uploaders

    public enum FileUploaderType
    {
        [Description("FTP Server")]
        FTP,
        [Description("RapidShare - www.rapidshare.com")]
        RapidShare,
        [Description("SendSpace - www.sendspace.com")]
        SendSpace,
        [Description("FileBin - www.filebin.ca")]
        FileBin
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

    #endregion
}
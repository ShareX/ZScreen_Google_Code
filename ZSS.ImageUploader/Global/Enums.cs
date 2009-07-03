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

using System.ComponentModel;

namespace ZSS
{
    public enum UploadMode
    {
        [Description("User")]
        API,
        [Description("Anonymous")]
        ANONYMOUS
    }

    public enum TwitPicUploadType
    {
        [Description("Upload Image")]
        UPLOAD_IMAGE_ONLY,
        [Description("Upload Image and update Twitter Status")]
        UPLOAD_IMAGE_AND_TWITTER
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
        LINKED_THUMBNAIL_WIKI
    }

    public enum ImageDestType
    {
        [Description("Clipboard")]
        CLIPBOARD,
        [Description("File")]
        FILE,
        [Description("FTP")]
        FTP,
        [Description("TinyPic")]
        TINYPIC,
        [Description("ImageShack")]
        IMAGESHACK,
        [Description("TwitPic")]
        TWITPIC,
        [Description("Custom Uploader")]
        CUSTOM_UPLOADER,
        [Description("MindTouch Deki Wiki")]
        DEKIWIKI,
    }
}
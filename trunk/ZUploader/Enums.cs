#region License Information (GPL v2)

/*
    ZUploader - A program that allows you to upload images, text or files in your clipboard
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

namespace ZUploader
{
    public enum ImageDestination
    {
        [Description("imageshack.us")]
        ImageShack,
        [Description("tinypic.com")]
        TinyPic,
        [Description("imgur.com")]
        Imgur,
        [Description("flickr.com")]
        Flickr,
        [Description("uploadscreenshot.com")]
        UploadScreenshot,
        [Description("File Uploader")]
        FileUploader
    }

    public enum TextDestination
    {
        [Description("pastebin.com")]
        Pastebin,
        [Description("pastebin.ca")]
        PastebinCA,
        [Description("paste2.org")]
        Paste2,
        [Description("slexy.org")]
        Slexy,
        [Description("File Uploader")]
        FileUploader
    }

    public enum FileDestination
    {
        [Description("rapidshare.com")]
        RapidShare,
        [Description("sendspace.com")]
        SendSpace,
        [Description("dropbox.com")]
        Dropbox,
        //[Description("min.us")]
        //Minus,
        [Description("Custom Uploader")]
        CustomUploader,
        [Description("FTP Server")]
        FTP
    }

    public enum EDataType
    {
        File, Image, Text
    }

    public enum EImageFormat
    {
        PNG, JPEG, GIF, BMP, TIFF
    }

    public enum GIFQuality
    {
        Default, Bit8, Bit4, Grayscale
    }

    public enum TaskJob
    {
        DataUpload, FileUpload, ImageUpload, TextUpload
    }
}
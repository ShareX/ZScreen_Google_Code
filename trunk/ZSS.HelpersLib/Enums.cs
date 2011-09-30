#region License Information (GPL v2)

/*
    ZUploader - A program that allows you to upload images, texts or files
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
using System.ComponentModel;

namespace HelpersLib
{
    // http://en.wikipedia.org/wiki/List_of_file_formats

    public enum ImageFileExtensions
    {
        [Description("Joint Photographic Experts Group")]
        jpg, jpeg,
        [Description("Portable Network Graphic")]
        png,
        [Description("CompuServe's Graphics Interchange Format")]
        gif,
        [Description("Microsoft Windows Bitmap formatted image")]
        bmp,
        [Description("File format used for icons in Microsoft Windows")]
        ico,
        [Description("Tagged Image File Format")]
        tif, tiff
    }

    public enum TextFileExtensions
    {
        [Description("ASCII or Unicode plaintext")]
        txt,
        [Description("ASCII or extended ASCII text file")]
        nfo,
        [Description("C source")]
        c,
        [Description("C++ source")]
        cpp, cc, cxx,
        [Description("C/C++ header file")]
        h,
        [Description("C++ header file")]
        hpp, hxx,
        [Description("C# source")]
        cs,
        [Description("Visual Basic.NET source")]
        vb,
        [Description("HyperText Markup Language")]
        html, htm,
        [Description("eXtensible HyperText Markup Language")]
        xhtml, xht,
        [Description("eXtensible Markup Language")]
        xml,
        [Description("Cascading Style Sheets")]
        css,
        [Description("JavaScript and JScript")]
        js,
        [Description("Hypertext Preprocessor")]
        php,
        [Description("Batch file")]
        bat,
        [Description("Java source")]
        java,
        [Description("Lua")]
        lua,
        [Description("Python source")]
        py,
        [Description("Perl")]
        pl,
        [Description("Visual Studio solution")]
        sln
    }

    public enum EDataType
    {
        File, Image, Text
    }

    public enum GIFQuality
    {
        Default, Bit8, Bit4, Grayscale
    }

    public enum EImageFormat
    {
        PNG, JPEG, GIF, BMP, TIFF
    }

    public enum TaskStatus
    {
        InQueue, Preparing, Uploading, URLShortening, Completed, Stopped
    }

    public enum TaskProgress
    {
        ReportStarted, ReportProgress
    }

    public enum FreeImageJpegQualityType
    {
        [Description("Save with bad quality (10:1)")]
        JPEG_QUALITYBAD,
        [Description("Save with average quality (25:1)")]
        JPEG_QUALITYAVERAGE,
        [Description("Save with normal quality (50:1)")]
        JPEG_QUALITYNORMAL,
        [Description("Save with good quality (75:1)")]
        JPEG_QUALITYGOOD,
        [Description("Save with superb quality (100:1)")]
        JPEG_QUALITYSUPERB,
    }

    public enum FreeImageJpegSubSamplingType
    {
        [Description("Save with high 4x1 chroma subsampling (4:1:1)")]
        JPEG_SUBSAMPLING_411,
        [Description("Save with medium 2x2 medium chroma subsampling (4:2:0) - default value")]
        JPEG_SUBSAMPLING_420,
        [Description("Save with low 2x1 chroma subsampling (4:2:2)")]
        JPEG_SUBSAMPLING_422,
        [Description("Save with no chroma subsampling (4:4:4)")]
        JPEG_SUBSAMPLING_444,
    }

    public enum WindowButtonAction
    {
        [Description("Minimize to Tray")]
        MinimizeToTray,
        [Description("Minimize to Taskbar")]
        MinimizeToTaskbar,
        [Description("Exit Application")]
        ExitApplication,
        [Description("Do Nothing")]
        Nothing
    }

    public enum HotkeyTask
    {
        [Description("Capture Entire Screen")]
        EntireScreen,
        [Description("Capture Active Window")]
        ActiveWindow,
        [Description("Capture Rectangular Region")]
        CropShot,
        [Description("Capture Last Rectangular Region")]
        LastCropShot,
        [Description("Capture Selected Window")]
        SelectedWindow,
        [Description("Capture Shape")]
        FreehandCropShot,
        [Description("Clipboard Upload")]
        ClipboardUpload,
        [Description("Auto Capture")]
        AutoCapture,
        [Description("Drop Window")]
        DropWindow,
        /*
        [Description("Google Translate")]
        LanguageTranslator,
        */
        [Description("Color Picker")]
        ScreenColorPicker,
        [Description("Twitter Client")]
        TwitterClient
    }
}
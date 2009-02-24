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
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Drawing;
using ZSS.ImageUploader.Helpers;
using System.IO;

namespace ZSS.Tasks
{
    public class MainAppTask
    {
        #region "Enums"
        public enum Jobs
        {
            [Description("Custom Uploader Test")]
            CUSTOM_UPLOADER_TEST,
            [Description("Active Window")]
            TAKE_SCREENSHOT_WINDOW,
            [Description("Cropped Window")]
            TAKE_SCREENSHOT_CROPPED,
            [Description("Cropped Window")]
            TAKE_SCREENSHOT_LAST_CROPPED,
            [Description("Fullscreen")]
            TAKE_SCREENSHOT_SCREEN,
            [Description("Drag and Drop")]
            PROCESS_DRAG_N_DROP,
            [Description("Image from Clipboard")]
            IMAGEUPLOAD_FROM_CLIPBOARD,
            [Description("Language Translator")]
            LANGUAGE_TRANSLATOR
        }

        public enum ProgressType
        {
            ADD_FILE_TO_LISTBOX,
            COPY_TO_CLIPBOARD_URL,
            COPY_TO_CLIPBOARD_IMAGE,
            FLASH_ICON,
            INCREMENT_PROGRESS,
            SET_ICON_BUSY,
            UPDATE_STATUS_BAR_TEXT,
            UPDATE_PROGRESS_MAX,
            UPDATE_TRAY_TITLE
        }
        #endregion

        #region "Common Properties for All Categories"
        public BackgroundWorker MyWorker { get; private set; }
        public JobCategoryType JobCategory { get; set; }
        public Jobs Job { get; private set; }
        /// <summary>
        /// List of Errors the Worker had during its operation
        /// </summary>
        public List<string> Errors { get; set; }
        #endregion

        #region "Properties for Categories: Pictures and Screenshots"
        /// <summary>
        /// Image object: Screenshot captured using User32 or Picture by User
        /// </summary>
        public Image MyImage { get; private set; }
        /// <summary>
        /// Name of the Image
        /// </summary>                
        public StringBuilder ImageName { get; set; }
        /// <summary>
        /// Local file path of the Image: Picture or Screenshot
        /// </summary>
        public string ImageLocalPath { get; private set; }
        /// <summary>
        /// URL of the Image: Picture or Screenshot
        /// </summary>
        public string ImageRemotePath { get; set; }
        /// <summary>
        /// FTP Account Name, TinyPic, ImageShack, xs.to
        /// </summary>
        public string ImageDestinationName { get; set; }
        /// <summary>
        /// FTP, HTTP: TinyPic, ImageShack
        /// </summary>
        public ImageDestType ImageDestCategory { get; set; }
        /// <summary>
        /// Pictures List to access Local file path, URL
        /// </summary>
        public ImageFileManager ImageManager { get; set; }
        #endregion

        #region "Properties for Category: Text"
        public GoogleTranslate.TranslationInfo TranslationInfo { get; set; }
        #endregion

        /// <summary>
        /// Constructor taking Worker and Job
        /// </summary>
        /// <param name="worker"></param>
        /// <param name="job"></param>
        public MainAppTask(BackgroundWorker worker, Jobs job)
        {
            this.MyWorker = worker;
            this.Job = job;
            this.Errors = new List<string>();
        }

        public void SetImage(Image img)
        {
            this.MyImage = img;
        }

        public bool SafeToUpload()
        {
            bool safe = ((this.Job == Jobs.PROCESS_DRAG_N_DROP || this.Job == Jobs.IMAGEUPLOAD_FROM_CLIPBOARD)
                && this.ImageDestCategory == ImageDestType.FTP) || this.MyImage != null;
            if (!safe)
            {
                this.Errors.Add("Not a valid image.");
            }
            return safe;
        }

        public void SetImage(string fp)
        {
            this.MyImage = MyGraphics.GetImageSafely(fp);
        }

        public void SetLocalFilePath(string fp)
        {
            this.ImageLocalPath = fp;
            this.ImageName = new StringBuilder(Path.GetFileName(fp));
        }

        #region "Functions"
        /// <summary>
        /// Function to Capture Active Window
        /// </summary>
        public void CaptureActiveWindow()
        {
            if (this.MyImage == null)
            {
                this.MyImage = User32.GrabWindow((IntPtr)User32.GetWindowHandle(), Program.conf.ShowCursor);
            }
        }

        /// <summary>
        /// Function to Capture Entire Screen
        /// </summary>
        public void CaptureScreen()
        {
            if (this.MyImage == null)
            {
                this.MyImage = User32.CaptureScreen(Program.conf.ShowCursor);
            }
        }
        #endregion

    }
}
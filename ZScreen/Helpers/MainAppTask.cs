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
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Text;
using ZSS.ImageUploaders.Helpers;
using ZSS.TextUploadersLib;

namespace ZSS.Tasks
{
    public class MainAppTask
    {
        #region "Enums"

        public enum Jobs
        {
            [Description("Entire Screen")]
            TAKE_SCREENSHOT_SCREEN,
            [Description("Active Window")]
            TAKE_SCREENSHOT_WINDOW_ACTIVE,
            [Description("Selected Window")]
            TAKE_SCREENSHOT_WINDOW_SELECTED,
            [Description("Crop Shot")]
            TAKE_SCREENSHOT_CROPPED,
            [Description("Last Crop Shot")]
            TAKE_SCREENSHOT_LAST_CROPPED,
            [Description("Auto Capture")]
            AUTO_CAPTURE,
            [Description("Clipboard Upload")]
            UPLOAD_FROM_CLIPBOARD,
            [Description("Drag & Drop Window")]
            PROCESS_DRAG_N_DROP,
            [Description("Language Translator")]
            LANGUAGE_TRANSLATOR,
            [Description("Screen Color Picker")]
            SCREEN_COLOR_PICKER,
            [Description("Upload Image")]
            UPLOAD_IMAGE,
            [Description("Custom Uploader Test")]
            CUSTOM_UPLOADER_TEST
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
            UPDATE_TRAY_TITLE,
            UPDATE_CROP_MODE,
            UPDATE_UPLOAD_DESTINATION
        }

        #endregion

        #region "Common Properties for All Categories"

        public BackgroundWorker MyWorker { get; private set; }
        public JobCategoryType JobCategory { get; set; }
        /// <summary>
        /// Entire Screen, Active Window, Selected Window, Crop Shot...
        /// </summary>
        public Jobs Job { get; private set; }
        /// <summary>
        /// List of Errors the Worker had during its operation
        /// </summary>
        public List<string> Errors { get; set; }
        public bool Retry { get; set; }
        public DateTime StartTime { get; set; }
        private DateTime endTime;
        public DateTime EndTime
        {
            get
            {
                return endTime;
            }
            set
            {
                endTime = value;
                UploadDuration = (int)Math.Round((endTime - StartTime).TotalMilliseconds);
            }
        }
        public int UploadDuration { get; set; }
        public bool IsImage { get; set; }

        #endregion

        #region "Properties for Categories: Pictures and Screenshots"

        /// <summary>
        /// Image object: Screenshot captured using User32 or Picture by User
        /// </summary>
        public Image MyImage { get; private set; }
        /// <summary>
        /// Name of the Image
        /// </summary>                
        public StringBuilder FileName { get; set; }
        private string localFilePath;
        /// <summary>
        /// Local file path of the Image: Picture or Screenshot or Text file
        /// </summary>
        public string LocalFilePath
        {
            get
            {
                return localFilePath;
            }
            private set
            {
                localFilePath = value;
                CheckIsValidImage();
            }
        }
        /// <summary>
        /// Option to convert Remote File Path to a tiny URL
        /// </summary>
        public bool MakeTinyURL { get; set; }
        /// <summary>
        /// URL of the Image: Picture or Screenshot, or Text file
        /// </summary>
        public string RemoteFilePath { get; set; }
        /// <summary>
        /// FTP Account Name, TinyPic, ImageShack
        /// </summary>
        public string DestinationName { get; set; }
        /// <summary>
        /// Clipboard, Custom Uploader, File, FTP, ImageShack, TinyPic
        /// </summary>
        public ImageDestType ImageDestCategory { get; set; }
        /// <summary>
        /// Pictures List to access Local file path, URL
        /// </summary>
        public ImageFileManager ImageManager { get; set; }

        #endregion

        #region "Properties for Category: Text"
        /// <summary>
        /// String object: Text captured from Clipboard
        /// </summary>
        public string MyText { get; set; }
        public GoogleTranslate.TranslationInfo TranslationInfo { get; set; }
        /// <summary>
        /// MyTextUploader Object
        /// </summary>
        public TextUploader MyTextUploader { get; set; }
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
            if (Program.conf.CopyImageUntilURL)
            {
                // IF (Bitmap)img.Clone() IS NOT USED THEN WE ARE GONNA GET CROSS THREAD OPERATION ERRORS! - McoreD
                this.MyWorker.ReportProgress((int)MainAppTask.ProgressType.COPY_TO_CLIPBOARD_IMAGE, (Bitmap)img.Clone());
            }
        }

        public bool SafeToUpload()
        {
            bool safe = ((this.Job == Jobs.PROCESS_DRAG_N_DROP || this.Job == Jobs.UPLOAD_FROM_CLIPBOARD)
                && this.ImageDestCategory == ImageDestType.FTP) || this.MyImage != null;
            if (!safe)
            {
                this.Errors.Add("Not a valid image.");
            }
            return safe;
        }

        public void SetImage(string fp)
        {
            this.MyImage = GraphicsMgr.GetImageSafely(fp);
        }

        public void SetLocalFilePath(string fp)
        {
            this.LocalFilePath = fp;
            this.FileName = new StringBuilder(Path.GetFileName(fp));
        }

        /// <summary>
        /// Check for valid image and update task.Errors with the error message
        /// </summary>
        /// <returns></returns>
        public bool IsValidImage()
        {
            if (!IsImage)
            {
                Errors.Add("Unsupported image.");
            }
            return IsImage;
        }

        private void CheckIsValidImage()
        {
            try
            {
                Image.FromFile(LocalFilePath).Dispose();
                IsImage = true;
            }
            catch
            {
                IsImage = false;
            }
        }


        #region "Functions"

        /// <summary>
        /// Function to Capture Active Window
        /// </summary>
        public void CaptureActiveWindow()
        {
            if (this.MyImage == null)
            {
                this.SetImage(User32.GrabWindow(User32.GetWindowHandle(), Program.conf.ShowCursor));
            }
        }

        /// <summary>
        /// Function to Capture Entire Screen
        /// </summary>
        public void CaptureScreen()
        {
            if (this.MyImage == null)
            {
                this.SetImage(User32.CaptureScreen(Program.conf.ShowCursor));
            }
        }

        public void RunWorker()
        {
            this.MyWorker.RunWorkerAsync(this);
        }

        #endregion
    }
}
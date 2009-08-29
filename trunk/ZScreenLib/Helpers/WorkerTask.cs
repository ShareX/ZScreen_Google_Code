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
using System.Runtime.InteropServices;
using ZSS;
using UploadersLib;
using UploadersLib.Helpers;
using UploadersLib.TextServices;
using System.Windows.Forms;
using ZScreenLib.Properties;
using System.Diagnostics;

namespace ZScreenLib
{
    public class WorkerTask
    {
        #region "Enums"

        public enum Jobs
        {
            [Description("Entire Screen")]
            TAKE_SCREENSHOT_SCREEN,
            [Description("Active Window")]
            TAKE_SCREENSHOT_WINDOW_ACTIVE,
            [Description("Selected Window")]
            TakeScreenshotWindowSelected,
            [Description("Crop Shot")]
            TakeScreenshotCropped,
            [Description("Last Crop Shot")]
            TAKE_SCREENSHOT_LAST_CROPPED,
            [Description("Auto Capture")]
            AUTO_CAPTURE,
            [Description("Clipboard Upload")]
            UploadFromClipboard,
            [Description("Drag & Drop Window")]
            PROCESS_DRAG_N_DROP,
            [Description("Language Translator")]
            LANGUAGE_TRANSLATOR,
            [Description("Screen Color Picker")]
            SCREEN_COLOR_PICKER,
            [Description("Upload Image")]
            UPLOAD_IMAGE,
            [Description("Custom Uploader Test")]
            CustomUploaderTest,
            [Description("Webpage Capture")]
            WEBPAGE_CAPTURE
        }

        public enum ProgressType : int
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
            UpdateCropMode,
            CHANGE_UPLOAD_DESTINATION,
            CHANGE_TRAY_ICON_PROGRESS,
            ShowTrayWarning
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
        private DateTime mEndTime;
        public DateTime EndTime
        {
            get
            {
                return mEndTime;
            }
            set
            {
                mEndTime = value;
                UploadDuration = (int)Math.Round((mEndTime - StartTime).TotalMilliseconds);
            }
        }
        public int UploadDuration { get; set; }
        public bool IsImage { get; set; }
        public int UniqueNumber { get; set; }

        #endregion

        #region "Properties for Categories: Pictures and Screenshots"

        /// <summary>
        /// Image object: Screenshot captured using User32 or Picture by User
        /// </summary>
        public Image MyImage { get; private set; }
        /// <summary>
        /// Name of the Image
        /// </summary>                
        public string FileName { get; set; }
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
                this.IsImage = GraphicsMgr.IsValidImage(localFilePath);
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
        /// Tiny URL of RemoteFilePath
        /// </summary>
        //public string TinyURL { get; set; }
        ///// <summary>
        ///// FTP Account Name, TinyPic, ImageShack
        ///// </summary>
        public string DestinationName = "File";
        /// <summary>
        /// Clipboard, Custom Uploader, File, FTP, ImageShack, TinyPic
        /// </summary>
        public ImageDestType MyImageUploader { get; set; }
        /// <summary>
        /// Pictures List to access Local file path, URL
        /// </summary>
        public ImageFileManager ImageManager { get; set; }

        #endregion

        public XMLSettings Settings { get; set; }

        #region "Properties for Category: Text"
        /// <summary>
        /// String object: Text captured from Clipboard
        /// </summary>
        public TextInfo MyText { get; set; }
        public GoogleTranslate.TranslationInfo TranslationInfo { get; set; }
        /// <summary>
        /// MyTextUploader
        /// </summary>
        public TextUploader MyTextUploader { get; set; }
        #endregion

        #region "Properties for Category: Binary"
        public FileUploaderType MyFileUploader { get; set; }
        public byte[] MyFile { get; set; }
        #endregion

        private WorkerTask()
        {
            this.Errors = new List<string>();
            this.Settings = Engine.conf;
        }

        public WorkerTask(Jobs job)
            : this()
        {
            this.MyWorker = new BackgroundWorker() { WorkerReportsProgress = true };
            this.Job = job;
        }

        /// <summary>
        /// Constructor taking Worker and Job
        /// </summary>
        /// <param name="worker"></param>
        /// <param name="job"></param>
        public WorkerTask(BackgroundWorker worker, Jobs job)
            : this()
        {
            this.MyWorker = worker;
            this.Job = job;
        }

        public void SetImage(Image img)
        {
            this.MyImage = img;
            if (Engine.conf.CopyImageUntilURL)
            {
                // IF (Bitmap)img.Clone() IS NOT USED THEN WE ARE GONNA GET CROSS THREAD OPERATION ERRORS! - McoreD
                this.MyWorker.ReportProgress((int)WorkerTask.ProgressType.COPY_TO_CLIPBOARD_IMAGE, (Bitmap)img.Clone());
            }
        }

        public bool SafeToUpload()
        {
            bool safe = ((this.Job == Jobs.PROCESS_DRAG_N_DROP || this.Job == Jobs.UploadFromClipboard)
                && this.MyImageUploader == ImageDestType.FTP) || this.MyImage != null;
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

        public void SetFilePath(string fileName)
        {
            string filePath = FileSystem.GetUniqueFilePath(Path.Combine(Engine.ImagesDir, fileName + "." + Engine.zImageFileTypes[Engine.conf.FileFormat]));

            if (Engine.conf.ManualNaming)
            {
                DestOptions ib = new DestOptions(this)
                {
                    Title = "Specify a Screenshot Name...",
                    InputText = this.FileName,
                    Icon = Resources.zss_main
                };
                ib.ShowDialog();
                if (ib.DialogResult == DialogResult.OK)
                {
                    StringBuilder sb = new StringBuilder(ib.InputText);
                    sb = NameParser.Normalize(sb);
                    if (string.IsNullOrEmpty(this.FileName) || !this.FileName.Equals(ib.InputText))
                    {
                        this.FileName = sb.ToString();
                    }
                }
            }

            StringBuilder sbPath = new StringBuilder();
            if (string.IsNullOrEmpty(this.FileName))
            {
                this.FileName = Path.GetFileNameWithoutExtension(filePath);
            }
            sbPath.Append(Path.Combine(Path.GetDirectoryName(filePath), this.FileName));
            sbPath.Append(Path.GetExtension(filePath));
            filePath = sbPath.ToString();

            this.LocalFilePath = filePath;
            this.FileName = Path.GetFileName(filePath);

        }

        public void SetLocalFilePath(string fp)
        {
            this.LocalFilePath = fp;
            this.FileName = Path.GetFileName(fp);
            if (FileSystem.IsValidImage(fp) && this.MyImage == null)
            {
                this.MyImage = FileSystem.ImageFromFile(fp);
            }
            Debug.Assert(File.Exists(fp), "File does not exist");
        }

        public string GetDescription()
        {
            return string.Format("{0} ({1})", this.Job.GetDescription(), this.DestinationName);
        }

        #region "Functions"

        /// <summary>
        /// Function to Capture Active Window
        /// </summary>
        public void CaptureActiveWindow()
        {
            if (this.MyImage == null)
            {
                this.SetImage(User32.CaptureWindow(User32.GetWindowHandle(), Engine.conf.ShowCursor));
            }
        }

        /// <summary>
        /// Function to Capture Entire Screen
        /// </summary>
        public void CaptureScreen()
        {
            if (this.MyImage == null)
            {
                this.SetImage(User32.CaptureScreen(Engine.conf.ShowCursor));
            }
        }

        public void RunWorker()
        {
            this.MyWorker.RunWorkerAsync(this);
        }

        #endregion

        public override string ToString()
        {
            StringBuilder sbDebug = new StringBuilder();
            sbDebug.AppendLine(string.Format("Image Uploader: {0}", MyImageUploader));
            // sbDebug.AppendLine(string.Format(" Text Uploader: {0}", MyTextUploader));
            sbDebug.AppendLine(string.Format(" File Uploader: {0}", MyFileUploader.GetDescription()));
            return sbDebug.ToString();
        }
    }
}
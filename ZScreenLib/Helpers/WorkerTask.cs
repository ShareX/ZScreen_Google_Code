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
using System.Windows.Forms;
using UploadersLib;
using UploadersLib.Helpers;
using UploadersLib.TextServices;
using ZScreenLib.Helpers;
using ZScreenLib.Properties;
using HelpersLib;
using GraphicsMgrLib;

namespace ZScreenLib
{
    public class WorkerTask
    {
        #region Enums

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
            WEBPAGE_CAPTURE,
            [Description("Freehand Crop Shot")]
            FREEHAND_CROP_SHOT
        }

        public enum ProgressType : int
        {
            ADD_FILE_TO_LISTBOX,
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

        #region Common Properties for All Categories

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
        public bool RetryPending { get; set; }
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

        #region Properties for Categories: Pictures and Screenshots

        /// <summary>
        /// Image object: Screenshot captured using User32 or Picture by User
        /// </summary>
        public Image MyImage { get; private set; }
        /// <summary>
        /// Name of the Image
        /// </summary>                
        public string FileName { get; set; }
        /// <summary>
        /// Local file path of the Image: Picture or Screenshot or Text file
        /// </summary>
        public string LocalFilePath { get; private set; }
        /// <summary>
        /// Option to convert Remote File Path to a tiny URL
        /// </summary>
        public bool MakeTinyURL { get; set; }
        /// <summary>
        /// URL of the Image: Picture or Screenshot, or Text file
        /// </summary>
        private string urlRemote = string.Empty;
        public string RemoteFilePath
        {
            get
            {
                return urlRemote;
            }
            set
            {
                if (!string.IsNullOrEmpty(value))
                    urlRemote = value;
            }
        }

        /*/// <summary>
        /// Tiny URL of RemoteFilePath
        /// </summary>
        public string TinyURL { get; set; }*/

        /// <summary>
        /// FTP Account Name, TinyPic, ImageShack
        /// </summary>
        public string DestinationName = string.Empty;
        /// <summary>
        /// Clipboard, Custom Uploader, File, FTP, ImageShack, TinyPic
        /// </summary>
        public ImageDestType MyImageUploader { get; set; }
        /// <summary>
        /// Pictures List to access Local file path, URL
        /// </summary>
        public ImageFileManager LinkManager { get; set; }

        #endregion

        #region Properties for Category: Text

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

        #region Properties for Category: Binary

        public FileUploaderType MyFileUploader { get; set; }
        public byte[] MyFile { get; set; }

        #endregion

        private WorkerTask()
        {
            this.Errors = new List<string>();
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
            FileSystem.AppendDebug(string.Format("Setting Image {0}x{1} to WorkerTask", img.Width, img.Height));
            this.MyImage = img;
            if (Engine.conf.CopyImageUntilURL)
            {
                // IF (Bitmap)img.Clone() IS NOT USED THEN WE ARE GONNA GET CROSS THREAD OPERATION ERRORS! - McoreD
                this.MyWorker.ReportProgress((int)WorkerTask.ProgressType.COPY_TO_CLIPBOARD_IMAGE, (Bitmap)img.Clone());
            }
        }

        public void SetImage(string fp)
        {
            this.MyImage = GraphicsMgr.GetImageSafely(fp);
        }

        /// <summary>
        /// Sets the file to save the image to. 
        /// If the user activated the "prompt for filename" option, then opens a dialog box.
        /// </summary>
        /// <param name="fileName">the base name</param>
        /// <returns>true if the screenshot should be saved, or false if the user canceled</returns>
        public bool SetFilePathFromPattern(string fileName)
        {
            string dir = Engine.ImagesDir;
            string filePath = FileSystem.GetUniqueFilePath(Path.Combine(dir, fileName + "." + Engine.zImageFileFormat.Extension));

            if (Engine.conf.ManualNaming)
            {
                // NOTE: we cannot use SaveFileDialog because we are not in the main thread, and we cant also use SaveFileDialog
                // in the main thread because the file name has to be determined outside of the main thread so the main thrad is 
                // ready for multiple requests

                //SaveFileDialog dlg = new SaveFileDialog();
                //if (dlg.ShowDialog() == DialogResult.OK)
                //{
                //    filePath = dlg.FileName;
                //}

                DestOptions dialog = new DestOptions(this)
                {
                    Title = "Specify a Screenshot Name...",
                    InputText = fileName,
                    Icon = Resources.zss_main
                };
                NativeMethods.SetForegroundWindow(dialog.Handle);
                dialog.ShowDialog();
                if (dialog.DialogResult == DialogResult.OK)
                {
                    StringBuilder sb = new StringBuilder(dialog.InputText);
                    sb = NameParser.Normalize(new NameParser.NormalizeOptions(sb) { ConvertSpace = true });
                    if (string.IsNullOrEmpty(this.FileName) || !this.FileName.Equals(dialog.InputText))
                    {
                        this.FileName = sb.ToString();
                    }
                }
                else
                {
                    return false;
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
            // make sure this length is less than 256 char
            if (filePath.Length > 256)
            {
                int extraChar = filePath.Length - 256;
                string fn = Path.GetFileNameWithoutExtension(filePath);
                string fnn = fn.Substring(0, fn.Length - extraChar);
                filePath = Path.Combine(dir, fnn) + Path.GetExtension(filePath);
            }
            UpdateLocalFilePath(filePath);
            return true;
        }

        public void UpdateLocalFilePath(string fp)
        {
            this.LocalFilePath = fp;
            this.LinkManager = new ImageFileManager(fp);
            this.RemoteFilePath = this.LinkManager.GetLocalFilePathAsUri();
            this.IsImage = GraphicsMgr.IsValidImage(fp);
            this.FileName = Path.GetFileName(fp);

            if (GraphicsMgr.IsValidImage(fp) && this.MyImage == null)
            {
                this.MyImage = FileSystem.ImageFromFile(fp);
            }
        }

        public string GetDestinationName()
        {
            string destName = this.DestinationName;
            if (string.IsNullOrEmpty(destName))
            {
                switch (JobCategory)
                {
                    case JobCategoryType.PICTURES:
                    case JobCategoryType.SCREENSHOTS:
                        destName = this.MyImageUploader.GetDescription();
                        break;
                    case JobCategoryType.TEXT:
                        destName = this.MyTextUploader.ToString();
                        break;
                    case JobCategoryType.BINARY:
                        destName = this.MyFileUploader.GetDescription();
                        break;
                }
            }
            return destName;
        }

        public string GetDescription()
        {
            return string.Format("{0} ({1})", this.Job.GetDescription(), this.GetDestinationName());
        }

        /// <summary>
        /// Function to Capture Active Window
        /// </summary>
        public void CaptureActiveWindow()
        {
            if (this.MyImage == null)
            {
                this.SetImage(Capture.CaptureActiveWindow());
            }
        }

        /// <summary>
        /// Function to Capture Entire Screen
        /// </summary>
        public void CaptureScreen()
        {
            if (this.MyImage == null)
            {
                this.SetImage(Capture.CaptureScreen(Engine.conf.ShowCursor));
            }
        }

        public void RunWorker()
        {
            this.MyWorker.RunWorkerAsync(this);
        }

        public override string ToString()
        {
            StringBuilder sbDebug = new StringBuilder();
            sbDebug.AppendLine(string.Format("Image Uploader: {0}", MyImageUploader));
            // sbDebug.AppendLine(string.Format(" Text Uploader: {0}", MyTextUploader));
            sbDebug.AppendLine(string.Format(" File Uploader: {0}", MyFileUploader.GetDescription()));
            return sbDebug.ToString();
        }

        public string ToErrorString()
        {
            return string.Join("\r\n", Errors.ToArray());
        }
    }
}
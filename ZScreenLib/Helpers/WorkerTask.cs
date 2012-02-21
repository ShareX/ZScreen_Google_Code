using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Media;
using System.Net;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Crop;
using Gif.Components;
using GraphicsMgrLib;
using HelpersLib;
using HistoryLib;
using ImageQueue;
using Microsoft.WindowsAPICodePack.Taskbar;
using ScreenCapture;
using SharpApng;
using UploadersLib;
using UploadersLib.FileUploaders;
using UploadersLib.GUI;
using UploadersLib.HelperClasses;
using UploadersLib.ImageUploaders;
using UploadersLib.OtherServices;
using UploadersLib.TextUploaders;
using UploadersLib.URLShorteners;
using ZScreenCoreLib;
using ZScreenLib.Properties;
using ZSS.IndexersLib;
using ZUploader.HelperClasses;

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

namespace ZScreenLib
{
    public class WorkerTask : IDisposable
    {
        #region 0 Enums

        #region JobLevel2 enum

        public enum JobLevel2
        {
            [Description("Capture Entire Screen")]
            CaptureEntireScreen,
            [Description("Capture Active Monitor")]
            CaptureActiveMonitor,
            [Description("Capture Active Window")]
            CaptureActiveWindow,
            [Description("Capture Window")]
            CaptureSelectedWindow,
            [Description("Capture Window from a List")]
            CaptureSelectedWindowFromList,
            [Description("Capture Rectangular Region")]
            CaptureRectRegion,
            [Description("Capture Rectangular Region to Clipboard")]
            CaptureRectRegionClipboard,
            [Description("Capture Previous Rectangular Region")]
            CaptureLastCroppedWindow,
            [Description("Auto Capture")]
            AutoCapture,
            [Description("Clipboard Upload")]
            UploadFromClipboard,
            [Description("Upload from Explorer")]
            UploadFromExplorer,
            [Description("Language Translator")]
            Translate,
            [Description("Screen Color Picker")]
            ScreenColorPicker,
            [Description("Upload Image")]
            UploadImage,
            [Description("Webpage Capture")]
            WebpageCapture,
            [Description("Capture Shape")]
            CaptureFreeHandRegion
        }

        #endregion JobLevel2 enum

        #region JobLevel3 enum

        public enum JobLevel3
        {
            [Description("None")]
            None,
            [Description("Upload Text")]
            UploadText,
            [Description("Shorten URL")]
            ShortenURL,
            [Description("Index Folder")]
            IndexFolder,
            [Description("Created Animated Image")]
            CreateAnimatedImage
        }

        #endregion JobLevel3 enum

        #region ProgressType enum

        public enum ProgressType
        {
            CopyToClipboardImage, // needed only for the feature CopyImageUntilURL
            FlashIcon,
            IncrementProgress,
            UpdateStatusBarText,
            UpdateProgressMax,
            UpdateTrayTitle,
            UpdateCropMode,
            ChangeTrayIconProgress,
            ShowBalloonTip,
            ShowTrayWarning,
            PrintText,
            PrintImage
        }

        #endregion ProgressType enum

        #region TaskState enum

        public enum TaskState
        {
            Created,
            Prepared,
            Started,
            RetryPending,
            ThreadMode,
            ImageProcessed,
            ImageEdited,
            ImageWritten,
            CancellationPending,
            Finished
        }

        #endregion TaskState enum

        #endregion 0 Enums

        #region 0 Events

        public event TaskEventHandler UploadCompleted;
        public event TaskEventHandler UploadPreparing;
        public event TaskEventHandler UploadProgressChanged2;
        public event TaskEventHandler UploadStarted;

        #endregion 0 Events

        #region 0 Properties

        private Stream Data;
        private string DestinationName = string.Empty;

        public DateTime EndTime
        {
            get { return mEndTime; }
            set
            {
                mEndTime = value;
                UploadDuration = (int)Math.Round((mEndTime - StartTime).TotalMilliseconds);
            }
        }

        public List<string> Errors { get; set; }

        public int Id { get; set; }

        public TaskInfo Info { get; private set; }

        public bool IsError
        {
            get { return Errors != null && Errors.Count > 0; }
        }

        public bool IsImage { get; private set; }

        public bool IsStopped { get; private set; }

        public bool IsWorking
        {
            get { return Status == TaskStatus.Preparing || Status == TaskStatus.Uploading; }
        }

        public EDataType Job1 { get; private set; } // Image, File, Text

        public JobLevel2 Job2 { get; private set; } // Entire Screen, Active Window, Selected Window, Crop Shot, etc.

        public JobLevel3 Job3 { get; private set; } // Shorten URL, Upload Text, Index Folder, etc.

        private DateTime mEndTime;

        public BackgroundWorker MyWorker { get; set; }

        public string OCRFilePath { get; private set; }

        public string OCRText { get; private set; }

        public ProgressManager Progress { get; set; }

        public UploadResult Result
        {
            get { return GetResult(); }
        }

        public DateTime StartTime { get; private set; }

        public List<TaskState> States = new List<TaskState>();

        public TaskStatus Status { get; private set; }

        public Image TempImage { get; private set; }

        public List<Image> tempImages;

        public string TempText { get; private set; }

        public GoogleTranslateInfo TranslationInfo { get; private set; }

        /// <summary>
        /// Duration in milliseconds
        /// </summary>
        public int UploadDuration { get; set; }

        public List<UploadResult> UploadResults { get; private set; }

        public bool WasToTakeScreenshot { get; set; }

        public Workflow WorkflowConfig { get; private set; }

        #endregion 0 Properties

        #region 1 Constructors

        private void PrepareOutputs(DestConfig ucDestOptions)
        {
            if (!States.Contains(TaskState.Prepared) && !States.Contains(TaskState.CancellationPending))
            {
                WorkflowConfig.DestConfig = ucDestOptions;
                States.Add(TaskState.Prepared);
            }
        }

        public bool StartWork(JobLevel2 job)
        {
            DebugHelper.WriteLine(WorkflowConfig.DestConfig.ToString());

            Job2 = job;

            SetNotifyIconStatus(Info.TrayIcon, ready: false);

            Info.WindowTitleText = NativeMethods.GetForegroundWindowText();
#if DEBUG
            DebugHelper.WriteLine("Retrieved Window Title at " + new StackFrame().GetMethod().Name);
#endif
            bool success = true;

            switch (Job2)
            {
                case JobLevel2.CaptureActiveWindow:
                    success = CaptureActiveWindow();
                    break;
                case JobLevel2.CaptureEntireScreen:
                    success = CaptureScreen();
                    break;
                case JobLevel2.CaptureActiveMonitor:
                    success = CaptureActiveMonitor();
                    break;
                case JobLevel2.CaptureSelectedWindow:
                case JobLevel2.CaptureRectRegion:
                case JobLevel2.CaptureRectRegionClipboard:
                case JobLevel2.CaptureLastCroppedWindow:
                    success = CaptureRegionOrWindow();
                    break;
                case JobLevel2.CaptureSelectedWindowFromList:
                    success = CaptureSelectedWindow();
                    break;
                case JobLevel2.CaptureFreeHandRegion:
                    success = CaptureShape();
                    break;
                case JobLevel2.UploadFromClipboard:
                    success = LoadClipboardContent();
                    break;
            }

            switch (job)
            {
                case JobLevel2.UploadFromExplorer:
                    Job1 = EDataType.File;
                    break;
                case JobLevel2.UploadFromClipboard:
                    if (FileSystem.IsValidLink(TempText))
                    {
                        Job1 = EDataType.URL;
                    }
                    break;
                case JobLevel2.Translate:
                    Job1 = EDataType.Text;
                    break;
                default:
                    Job1 = EDataType.Image;
                    break;
            }

            if (TempImage != null)
            {
                Info.ImageSize = TempImage.Size;
            }

            if (success && Job3 != JobLevel3.ShortenURL && Job2 != JobLevel2.UploadFromExplorer && WorkflowConfig.EnableSoundTaskBegin)
            {
                if (File.Exists(WorkflowConfig.SoundImagePath))
                    new SoundPlayer(WorkflowConfig.SoundImagePath).Play();
                else
                {
                    string soundPath = Path.Combine(Application.StartupPath, "Camera.wav");
                    if (File.Exists(soundPath))
                    {
                        new SoundPlayer(soundPath).Play();
                    }
                    else
                    {
                        new SoundPlayer(Resources.Camera).Play();
                    }
                }
            }
            if (!success)
            {
                States.Add(TaskState.CancellationPending);
            }

            return success;
        }

        public WorkerTask(Workflow wf, bool cloneWorkflow = true)
        {
            Info = new TaskInfo();
            UploadResults = new List<UploadResult>();
            Errors = new List<string>();
            States.Add(TaskState.Created);
            MyWorker = new BackgroundWorker { WorkerReportsProgress = true };

            if (cloneWorkflow)
            {
                IClone cm = new CloneManager();
                WorkflowConfig = cm.Clone(wf);
            }
            else
            {
                WorkflowConfig = wf;
            }
        }

        public WorkerTask(BackgroundWorker worker, Workflow wf)
            : this(wf)
        {
            MyWorker = worker;

            if (wf.DestConfig.Outputs.Contains(OutputEnum.Clipboard) &&
                WorkflowConfig.DestConfig.TaskClipboardContent.Count == 0)
            {
                WorkflowConfig.DestConfig.TaskClipboardContent.Add(ClipboardContentEnum.Data);
            }

            StartWork(wf.Job);
        }

        public WorkerTask(BackgroundWorker worker, TaskInfo info)
            : this(Engine.ConfigWorkflow)
        {
            Info = info;

            if (!string.IsNullOrEmpty(info.ExistingFilePath))
            {
                UpdateLocalFilePath(info.ExistingFilePath);
            }

            MyWorker = worker;
            PrepareOutputs(info.DestConfig); // step 1

            DialogResult result = StartWork(info.Job) ? DialogResult.OK : DialogResult.Cancel; // step 2

            if (result == DialogResult.OK && Engine.ConfigUI.PromptForOutputs) // step 3
            {
                var wfw = new WorkflowWizard(this) { Icon = Resources.zss_tray };
                result = wfw.ShowDialog();
            }

            if (Job1 == EDataType.Image && result == DialogResult.OK)
            {
                if (!States.Contains(TaskState.ImageProcessed))
                {
                    ProcessImage(TempImage);
                }
            }

            if (result == DialogResult.Cancel)
            {
                States.Add(TaskState.CancellationPending);
            }

            if (States.Contains(TaskState.CancellationPending))
            {
                SetNotifyIconStatus(Info.TrayIcon, ready: true);
            }
        }

        #endregion 1 Constructors

        #region Capture

        /// <summary>
        /// Function to Capture Active Window
        /// </summary>
        private bool CaptureActiveWindow()
        {
            if (TempImage == null)
            {
                switch (WorkflowConfig.CaptureEngineMode2)
                {
                    case CaptureEngineType.DWM:
                        if (NativeMethods.IsDWMEnabled())
                        {
                            TempImage = Capture.CaptureWithDWM(WorkflowConfig, NativeMethods.GetForegroundWindow());
                        }
                        else
                        {
                            DebugHelper.WriteLine("DWM is not found in the system.");
                            TempImage = Capture.CaptureWithGDI2(WorkflowConfig);
                        }
                        break;
                    default: // GDI
                        TempImage = Capture.CaptureWithGDI2(WorkflowConfig);
                        break;
                }

                if (TempImage != null)
                {
                    if (WorkflowConfig.ActiveWindowShowCheckers)
                    {
                        TempImage = ImageEffects.DrawCheckers(TempImage);
                    }
                }

                SetImage(TempImage);
            }

            return TempImage != null;
        }

        private bool CaptureRectangle(Image imgSS)
        {
            return CaptureRegionOrWindow(imgSS, false);
        }

        public bool CaptureRegionOrWindow()
        {
            if (!Engine.IsTakingScreenShot)
            {
                Engine.IsTakingScreenShot = true;

                bool windowMode = Job2 == JobLevel2.CaptureSelectedWindow;

                if (Engine.ConfigUI == null) Engine.ConfigUI = new XMLSettings();

                try
                {
                    Screenshot.DrawCursor = Engine.ConfigWorkflow.DrawCursor;

                    using (Image imgSS = Screenshot.CaptureFullscreen())
                    {
                        if (Job2 == JobLevel2.CaptureLastCroppedWindow && !Engine.ConfigUI.LastRegion.IsEmpty)
                        {
                            SetImage(CaptureHelpers.CropImage(imgSS, Engine.ConfigUI.LastRegion));
                        }

                        else if (Job2 == JobLevel2.CaptureSelectedWindow)
                        {
                            CaptureWindow(imgSS);
                        }

                        else
                        {
                            switch (Engine.ConfigUI.CropEngineMode)
                            {
                                case CropEngineType.CropLite:
                                    using (var crop = new CropLight(imgSS))
                                    {
                                        if (crop.ShowDialog() == DialogResult.OK)
                                        {
                                            SetImage(CaptureHelpers.CropImage(imgSS, crop.SelectionRectangle));
                                        }
                                    }
                                    break;
                                case CropEngineType.Cropv1:
                                    CaptureRectangle(imgSS);
                                    break;
                                case CropEngineType.Cropv2:
                                    using (var crop = new Crop2(imgSS))
                                    {
                                        if (crop.ShowDialog() == DialogResult.OK)
                                        {
                                            SetImage(crop.GetCroppedScreenshot());
                                        }
                                    }
                                    break;
                                case CropEngineType.Cropv3:
                                    Surface surface = new RectangleRegion(imgSS);
                                    surface.Config = Engine.ConfigUI.SurfaceConfig;
                                    if (surface.ShowDialog() == DialogResult.OK)
                                    {
                                        SetImage(surface.GetRegionImage());
                                    }
                                    break;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    DebugHelper.WriteException(ex, "Error while capturing region");
                    Errors.Add(ex.Message);
                    if (Engine.ConfigOptions.CaptureEntireScreenOnError)
                    {
                        CaptureScreen();
                    }
                }
                finally
                {
                    MyWorker.ReportProgress((int)ProgressType.UpdateCropMode);
                    Engine.IsTakingScreenShot = false;
                }
            }

            return TempImage != null;
        }

        private bool CaptureRegionOrWindow(Image imgSS, bool windowMode)
        {
            using (var c = new Crop(imgSS, windowMode))
            {
                if (c.ShowDialog() == DialogResult.OK)
                {
                    if ((Job2 == JobLevel2.CaptureRectRegion || Job2 == JobLevel2.CaptureRectRegionClipboard)
                        && !Engine.ConfigUI.LastRegion.IsEmpty)
                    {
                        return SetImage(CaptureHelpers.CropImage(imgSS, Engine.ConfigUI.LastRegion));
                    }
                    else if (windowMode && !Engine.ConfigUI.LastCapture.IsEmpty)
                    {
                        return SetImage(CaptureHelpers.CropImage(imgSS, Engine.ConfigUI.LastCapture));
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// Function to Capture Entire Screen
        /// </summary>
        public bool CaptureScreen()
        {
            if (TempImage == null)
            {
                Screenshot.DrawCursor = WorkflowConfig.DrawCursor;
                SetImage(Screenshot.CaptureFullscreen());
            }

            return TempImage != null;
        }

        public bool CaptureActiveMonitor()
        {
            SetImage(Screenshot.CaptureActiveMonitor());
            return TempImage != null;
        }

        public bool CaptureSelectedWindow()
        {
            NativeMethods.SetForegroundWindow(Info.Handle);
            Thread.Sleep(250);

            switch (WorkflowConfig.CaptureEngineMode2)
            {
                case CaptureEngineType.DWM:
                    SetImage(Capture.CaptureWithDWM(WorkflowConfig, Info.Handle));
                    break;
                default:
                    SetImage(Screenshot.CaptureWindow(Info.Handle));
                    break;
            }

            return TempImage != null;
        }

        public bool CaptureShape()
        {
            Screenshot.DrawCursor = Engine.ConfigWorkflow.DrawCursor;
            var rcp = new RegionCapturePreview(Engine.ConfigUI.SurfaceConfig);

            if (rcp.ShowDialog() == DialogResult.OK)
            {
                SetImage(rcp.Result);
            }

            return TempImage != null;
        }

        private bool CaptureWindow(Image imgSS)
        {
            return CaptureRegionOrWindow(imgSS, true);
        }

        #endregion Capture

        #region Checks

        public bool Canceled()
        {
            return !IsNotCanceled();
        }

        private bool CreateThumbnail()
        {
            return GraphicsMgr.IsValidImage(Info.LocalFilePath) && TempImage != null &&
                   (Engine.ConfigUI.ConfLinkFormat.Contains((int)LinkFormatEnum.LINKED_THUMBNAIL) ||
                    Engine.ConfigUI.ConfLinkFormat.Contains((int)LinkFormatEnum.LINKED_THUMBNAIL_WIKI) ||
                    Engine.ConfigUI.ConfLinkFormat.Contains((int)LinkFormatEnum.LinkedThumbnailHtml) ||
                    Engine.ConfigUI.ConfLinkFormat.Contains((int)LinkFormatEnum.THUMBNAIL)) &&
                   (!Engine.ConfigUploaders.FTPThumbnailCheckSize || (Engine.ConfigUploaders.FTPThumbnailCheckSize &&
                                                                      (TempImage.Width >
                                                                       Engine.ConfigUploaders.FTPThumbnailWidthLimit)));
        }

        public bool IsNotCanceled()
        {
            return !States.Contains(TaskState.CancellationPending);
        }

        public bool JobIsImageToClipboard()
        {
            return WorkflowConfig.DestConfig.Outputs.Contains(OutputEnum.Clipboard) &&
                   WorkflowConfig.DestConfig.TaskClipboardContent.Contains(ClipboardContentEnum.Data) &&
                   TempImage != null;
        }

        private void SetNotifyIconStatus(NotifyIcon ni, bool ready)
        {
            Icon ico = ready ? Resources.zss_tray : Resources.zss_busy;
            if (ni != null && ico != null)
            {
                ni.Icon = ico;
                // Text length must be less than 64 characters long
                var sbMsg = new StringBuilder();
                sbMsg.Append(Job2.GetDescription());
                sbMsg.Append(" to ");
                switch (Job1)
                {
                    case EDataType.Image:
                        sbMsg.Append(WorkflowConfig.DestConfig.ToStringImageUploaders());
                        break;
                    case EDataType.Text:
                        if (Job3 == JobLevel3.ShortenURL)
                        {
                            sbMsg.Append(WorkflowConfig.DestConfig.ToStringLinkUploaders());
                        }
                        else
                        {
                            sbMsg.Append(WorkflowConfig.DestConfig.ToStringTextUploaders());
                        }
                        break;
                    case EDataType.File:
                        sbMsg.Append(WorkflowConfig.DestConfig.ToStringFileUploaders());
                        break;
                }
                ni.Text = ready ? Engine.GetProductName() : sbMsg.ToString().Substring(0, Math.Min(sbMsg.Length, 63));
            }
        }

        public bool ShortenURL(string fullUrl)
        {
            var ur_shorturl = new UploadResult();
            bool success = ShortenURL(ur_shorturl, fullUrl);
            AddUploadResult(ur_shorturl);
            return success;
        }

        /// <summary>
        /// Function to test if the URL should or could shorten
        /// </summary>
        /// <param name="url">Long URL</param>
        /// <returns>true/false whether URL should or could shorten</returns>
        public bool ShouldShortenURL(string url)
        {
            if (FileSystem.IsValidLink(url) && WorkflowConfig.DestConfig.LinkUploaders.Count > 0)
            {
                bool bShortenUrlJob = Engine.ConfigUI.ShortenUrlUsingClipboardUpload &&
                                      Job2 == JobLevel2.UploadFromClipboard && FileSystem.IsValidLink(TempText);
                bool bLongUrl = Engine.ConfigUI.ShortenUrlAfterUpload &&
                                url.Length > Engine.ConfigUI.ShortenUrlAfterUploadAfter;

                if (bShortenUrlJob || bLongUrl)
                {
                    DebugHelper.WriteLine(string.Format("URL Length: {0}; Shortening after {1}", url.Length.ToString(),
                                                         Engine.ConfigUI.ShortenUrlAfterUploadAfter));
                }
                return Engine.ConfigOptions.TwitterEnabled || bShortenUrlJob || bLongUrl ||
                       Engine.ConfigUI.ConfLinkFormat.Contains((int)LinkFormatEnum.FULL_TINYURL);
            }

            return false;
        }

        #endregion Checks

        #region Delegates

        public delegate void TaskEventHandler(WorkerTask wt);

        #endregion Delegates

        #region Descriptions

        public string GetDescription()
        {
            var sb = new StringBuilder();

            if (!string.IsNullOrEmpty(Info.FileName))
            {
                sb.Append(Info.FileName);
            }

            if (TempImage != null)
            {
                sb.Append(string.Format(" ({0}x{1})", TempImage.Width, TempImage.Height));
                if (!string.IsNullOrEmpty(Info.FileSize))
                {
                    sb.Append(" " + Info.FileSize);
                }
            }

            if (sb.Length == 0)
            {
                sb.Append(Application.ProductName);
            }

            return sb.ToString();
        }

        public string GetDestinationName()
        {
            string destName = DestinationName;
            if (string.IsNullOrEmpty(destName))
            {
                switch (Job1)
                {
                    case EDataType.Image:
                        destName = WorkflowConfig.DestConfig.ToStringImageUploaders();
                        break;
                    case EDataType.Text:
                        switch (Job3)
                        {
                            case JobLevel3.ShortenURL:
                                destName = WorkflowConfig.DestConfig.ToStringLinkUploaders();
                                break;
                            default:
                                destName = WorkflowConfig.DestConfig.ToStringTextUploaders();
                                break;
                        }
                        break;
                    case EDataType.File:
                        destName = WorkflowConfig.DestConfig.ToStringFileUploaders();
                        break;
                }
            }
            return destName;
        }

        public NameParser GetNameParser()
        {
            return
                GetNameParser(Job2 == JobLevel2.CaptureActiveWindow
                                  ? NameParserType.ActiveWindow
                                  : NameParserType.EntireScreen);
        }

        public NameParser GetNameParser(NameParserType parserType)
        {
            return new NameParser
                       {
                           Type = parserType,
                           Picture = TempImage,
                           AutoIncrementNumber = WorkflowConfig.ConfigFileNaming.AutoIncrement,
                           WindowText = Info.WindowTitleText
                       };
        }

        public string ToErrorString()
        {
            return string.Join("\r\n", Errors.ToArray());
        }

        public override string ToString()
        {
            var sbDebug = new StringBuilder();
            sbDebug.AppendLine(string.Format("Image Uploaders: {0}", WorkflowConfig.DestConfig.ToStringImageUploaders()));
            sbDebug.AppendLine(string.Format("  File Uploader: {0}", WorkflowConfig.DestConfig.ToStringFileUploaders()));
            return sbDebug.ToString();
        }

        #endregion Descriptions

        #region Edit Image

        public bool IsValidActionFile(Software app)
        {
            return Job1 == EDataType.File && app.TriggerForFiles && File.Exists(Info.LocalFilePath);
        }

        public bool IsValidActionImage(Software app)
        {
            return Job1 == EDataType.Image && app.TriggerForImages && !WasToTakeScreenshot ||
                   Job1 == EDataType.Image && app.TriggerForScreenshots && WasToTakeScreenshot;
        }

        public bool IsValidActionOCR(Software app)
        {
            return Job1 == EDataType.Image && app.TriggerForText && !string.IsNullOrEmpty(OCRText) &&
                   File.Exists(OCRFilePath);
        }

        public bool IsValidActionText(Software app)
        {
            return Job1 == EDataType.Text && app.TriggerForText && !string.IsNullOrEmpty(TempText);
        }

        /// <summary>
        /// Perform Actions after capturing image/text/file objects
        /// </summary>
        public void PerformActions()
        {
            foreach (Software app in Engine.ConfigUI.ConfigActions.ActionsApps)
            {
                if (app.Enabled)
                {
                    if (IsValidActionImage(app) && app.Name == Engine.zImageAnnotator)
                    {
                        try
                        {
                            // Compatibility fixes
                            string APPLICATIONDATA_LANGUAGE_PATH =
                                Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                                             @"Greenshot\Languages\");
                            if (!Directory.Exists(APPLICATIONDATA_LANGUAGE_PATH))
                                Directory.CreateDirectory(APPLICATIONDATA_LANGUAGE_PATH);
                            Greenshot.MainForm.Start(new string[0]);

                            var capture = new Greenshot.Helpers.Capture(TempImage);
                            capture.CaptureDetails.Filename = Info.LocalFilePath;
                            capture.CaptureDetails.Title =
                                Path.GetFileNameWithoutExtension(capture.CaptureDetails.Filename);
                            capture.CaptureDetails.AddMetaData("file", capture.CaptureDetails.Filename);
                            capture.CaptureDetails.AddMetaData("source", "file");

                            var surface = new Greenshot.Drawing.Surface(capture);
                            var editor = new Greenshot.ImageEditorForm(surface,
                                                             WorkflowConfig.DestConfig.Outputs.Contains(
                                                                 OutputEnum.LocalDisk)) { Icon = Resources.zss_tray };
                            editor.SetImagePath(Info.LocalFilePath);
                            editor.Visible = false;
                            editor.ShowDialog();
                            TempImage = editor.GetImageForExport();
                        }
                        catch (Exception ex)
                        {
                            DebugHelper.WriteException(ex, "ImageEdit");
                        }
                    }
                    else if (IsValidActionImage(app) && app.Name == Engine.zImageEffects)
                    {
                        var effects = new ImageEffectsGUI(TempImage);
                        effects.ShowDialog();
                        TempImage = effects.GetImageForExport();
                    }
                    else if (File.Exists(app.Path))
                    {
                        if (IsValidActionOCR(app))
                        {
                            app.OpenFile(OCRFilePath);
                            OCRText = File.ReadAllText(OCRFilePath);
                        }
                        else if (IsValidActionText(app))
                        {
                            app.OpenFile(Info.LocalFilePath);
                            TempText = File.ReadAllText(TempText);
                        }
                        else if (IsValidActionImage(app))
                        {
                            WriteImage(TempImage);
                            app.OpenFile(Info.LocalFilePath);
                        }
                        else if (IsValidActionFile(app))
                        {
                            app.OpenFile(Info.LocalFilePath);
                        }
                    }
                    DebugHelper.WriteLine(string.Format("Performed Actions using {0}.", app.Name));
                }
            }
        }

        private void ProcessImage(Image img)
        {
            States.Add(TaskState.ImageProcessed);
            bool window = Job2 == JobLevel2.CaptureActiveWindow || Job2 == JobLevel2.CaptureSelectedWindow ||
                          Job2 == JobLevel2.CaptureEntireScreen;

            if (img != null)
            {
                if (!window)
                {
                    // Add Rounded corners
                    bool roundedShadowCorners = false;
                    if (Engine.ConfigUI.ImageAddRoundedCorners)
                    {
                        img = GraphicsMgr.RemoveCorners(img, null);
                        roundedShadowCorners = true;
                    }

                    // Add shadows
                    if (Engine.ConfigUI.ImageAddShadow)
                    {
                        img = GraphicsMgr.AddBorderShadow(img, roundedShadowCorners);
                    }
                }

                // Effects
                var effects = new ImageEffects(Engine.ConfigOptions.ConfigImageEffects);
                img = effects.ApplySizeChanges(img);
                img = effects.ApplyScreenshotEffects(img);

                // Watermark
                if (Job2 != JobLevel2.UploadFromClipboard || !Engine.ConfigWorkflow.ConfigWatermark.WatermarkExcludeClipboardUpload)
                {
                    img = new WatermarkEffects(WorkflowConfig.ConfigWatermark).ApplyWatermark(img, GetNameParser(NameParserType.Watermark));
                }

                TempImage = img;
            }
        }

        #endregion Edit Image

        #region Google Translate

        public void SetTranslationInfo(GoogleTranslateInfo gti)
        {
            Job1 = EDataType.Text;
            TranslationInfo = gti;
        }

        #endregion Google Translate

        #region Helper Methods

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        /// <filterpriority>2</filterpriority>
        public void Dispose()
        {
            if (TempImage != null) TempImage.Dispose();
            if (Data != null) Data.Dispose();
            if (MyWorker != null) MyWorker.Dispose();
        }

        public HistoryItem GenerateHistoryItem()
        {
            return GenerateHistoryItem(Result);
        }

        public HistoryItem GenerateHistoryItem(UploadResult ur)
        {
            var hi = new HistoryItem
                         {
                             DateTimeUtc = EndTime,
                             DeletionURL = ur.DeletionURL,
                             ThumbnailURL = ur.ThumbnailURL,
                             ShortenedURL = ur.ShortenedURL,
                             URL = ur.URL,
                             Filename = Info.FileName,
                             Filepath = Info.LocalFilePath,
                             Host = ur.Host,
                             Type = Job1.GetDescription()
                         };

            return hi;
        }

        public UploadResult GetResult()
        {
            return UploadResults.FirstOrDefault(ur => !string.IsNullOrEmpty(ur.URL));
        }

        /// <summary>
        /// Runs BwApp_DoWork
        /// </summary>
        public void RunWorker()
        {
            Info.WindowTitleText = NativeMethods.GetForegroundWindowText();

            // PerformActions should happen in main thread
            if (this.Job1 == EDataType.Image && this.TempImage != null && !this.States.Contains(WorkerTask.TaskState.ImageEdited))
            {
                this.States.Add(WorkerTask.TaskState.ImageEdited);
                if (this.WorkflowConfig.PerformActions && this.Job2 != WorkerTask.JobLevel2.UploadImage)
                {
                    this.PerformActions();
                }
            }

            // Any code before the line below will run in main thread
            MyWorker.RunWorkerAsync(this);
        }

        #endregion Helper Methods

        private void MyWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void MyWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            switch ((TaskProgress)e.ProgressPercentage)
            {
                case TaskProgress.ReportStarted:
                    OnUploadStarted();
                    break;
                case TaskProgress.ReportProgress:
                    var progress = e.UserState as ProgressManager;
                    if (progress != null)
                    {
                        Progress = progress;
                        // OnUploadProgressChanged2();
                    }
                    break;
            }
        }

        private void MyWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            // OnUploadCompleted();
        }

        #region Populating Task

        public void AddUploadResult(UploadResult ur)
        {
            if (ur != null && !ExistsUploadResult(ur))
            {
                string fp = Info.LocalFilePath;
                if (Engine.IsPortable)
                {
                    fp = Path.Combine(Application.StartupPath, fp);
                    UpdateLocalFilePath(fp);
                }
                if (File.Exists(fp))
                {
                    ur.LocalFilePath = fp;
                }
                if (!string.IsNullOrEmpty(ur.Host))
                {
                    UploadResults.Add(ur);
                }
                if (Engine.ConfigUI.ShowOutputsAsap)
                {
                    MyWorker.ReportProgress((int)ProgressType.ShowBalloonTip, this);
                }
            }
        }

        private bool ExistsUploadResult(UploadResult ur2)
        {
            return UploadResults.Any(ur1 => ur2.Host == ur1.Host);
        }

        public bool SetImage(string savePath)
        {
            return SetImage(GraphicsMgr.GetImageSafely(savePath), savePath);
        }

        public bool SetImage(Image img, string savePath = "")
        {
            if (img != null)
            {
                TempImage = img;
                Job1 = EDataType.Image;

                DebugHelper.WriteLine(string.Format("Setting Image {0}x{1} to WorkerTask", img.Width, img.Height));

                if (Engine.ConfigUI != null && Engine.ConfigUI.ShowOutputsAsap)
                {
                    // IF (Bitmap)img.Clone() IS NOT USED THEN WE ARE GONNA GET CROSS THREAD OPERATION ERRORS! - McoreD
                    MyWorker.ReportProgress((int)ProgressType.CopyToClipboardImage, img.Clone());
                }

                // UpdateLocalFilePath needs to happen before Image is processed
                EImageFormat imageFormat;
                if (!string.IsNullOrEmpty(savePath))
                {
                    UpdateLocalFilePath(savePath);
                    Data = PrepareDataFromFile(savePath);
                }
                else
                {
                    // Prepare data so that we have the correct file extension for Image Editor
                    Data = WorkerTaskHelper.PrepareImage(WorkflowConfig, Engine.ConfigOptions, TempImage, out imageFormat,
                                                         bTargetFileSize: false);
                    string fn = WorkerTaskHelper.PrepareFilename(WorkflowConfig, TempImage, imageFormat, GetNameParser());
                    string imgfp = FileSystem.GetUniqueFilePath(WorkflowConfig, Engine.ImagesDir, fn);
                    UpdateLocalFilePath(imgfp);
                }

                SetOCR(TempImage);
            }

            return TempImage != null;
        }

        public void SetImages(List<Image> tempImages)
        {
            Job3 = JobLevel3.CreateAnimatedImage;
            this.tempImages = tempImages;
        }

        /// <summary>
        /// Sets the file to save the image to.
        /// If the user activated the "prompt for Info.FileName" option, then opens a dialog box.
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns>true if the screenshot should be saved, or false if the user canceled</returns>
        public DialogResult SetManualOutputs(string filePath)
        {
            DialogResult dlgResult = DialogResult.OK;
            if (Engine.ConfigUI.PromptForOutputs)
            {
                var dialog = new DestOptions(this)
                                 {
                                     Title = "Configure Outputs...",
                                     FilePath = filePath,
                                     Icon = Resources.zss_main
                                 };
                NativeMethods.SetForegroundWindow(dialog.Handle);
                dlgResult = dialog.ShowDialog();
                if (dlgResult == DialogResult.OK)
                {
                    if (!string.IsNullOrEmpty(dialog.FilePath))
                    {
                        UpdateLocalFilePath(dialog.FilePath);
                    }
                }
            }
            return dlgResult;
        }

        public void SetOCR(Image ocrImage)
        {
            if (WorkflowConfig.DestConfig.TaskClipboardContent.Contains(ClipboardContentEnum.OCR) &&
                string.IsNullOrEmpty(OCRText))
            {
                if (WorkflowConfig.DestConfig.Outputs.Contains(OutputEnum.LocalDisk))
                {
                    WriteImage(ocrImage);
                }

                OCRHelper ocr = null;
                if (File.Exists(Info.LocalFilePath))
                {
                    ocr = new OCRHelper(Info.LocalFilePath);
                }
                else if (ocrImage != null)
                {
                    string ocrfp = Path.Combine(Engine.zTempDir, FileSystem.GetUniqueFileName(WorkflowConfig, "ocr.png"));
                    FileInfo fi = FileSystem.WriteImage(ocrfp, ocrImage.SaveImage(WorkflowConfig, EImageFormat.PNG));
                    if (fi.Exists)
                    {
                        ocr = new OCRHelper(fi.FullName);
                        File.Delete(ocrfp);
                    }
                }

                if (ocr != null)
                {
                    OCRText = ocr.Text;

                    if (!string.IsNullOrEmpty(OCRText) &&
                        WorkflowConfig.DestConfig.Outputs.Contains(OutputEnum.LocalDisk))
                    {
                        OCRFilePath = Path.ChangeExtension(Info.LocalFilePath, ".txt");
                        FileSystem.WriteText(OCRFilePath, OCRText);
                    }
                }
            }
        }

        public void SetText(string text)
        {
            Job1 = EDataType.Text;
            TempText = text;

            string ext = ".log";
            if (Directory.Exists(text) && WorkflowConfig.DestConfig.TextUploaders.Contains(TextDestination.FileUploader))
            {
                ext = ".html";
            }
            string fptxt = FileSystem.GetUniqueFilePath(Engine.ConfigWorkflow, Engine.TextDir,
                                                        new NameParser().Convert("%y.%mo.%d-%h.%mi.%s") + ext);
            UpdateLocalFilePath(fptxt);

            if (Directory.Exists(text))
            {
                var settings = new IndexerAdapter();
                settings.LoadConfig(Engine.ConfigOptions.IndexerConfig);
                Engine.ConfigOptions.IndexerConfig.FolderList.Clear();

                settings.GetConfig().SetSingleIndexPath(Path.Combine(Engine.TextDir, Info.FileName));
                settings.GetConfig().FolderList.Add(TempText);

                Indexer indexer = null;
                switch (settings.GetConfig().IndexingEngineType)
                {
                    case IndexingEngine.TreeLib:
                        indexer = new TreeWalkIndexer(settings);
                        break;
                    case IndexingEngine.TreeNetLib:
                        indexer = new TreeNetIndexer(settings);
                        break;
                }

                if (indexer != null)
                {
                    Job3 = JobLevel3.IndexFolder;
                    TempText = indexer.IndexNow(IndexingMode.IN_ONE_FOLDER_MERGED, false);
                    UpdateLocalFilePath(settings.GetConfig().GetIndexFilePath());
                }
            }
            else if (FileSystem.IsValidLink(text))
            {
                Job3 = JobLevel3.ShortenURL;
            }
            else
            {
                Job3 = JobLevel3.UploadText;
            }
        }

        public void UpdateLocalFilePath(string fp)
        {
            if (!string.IsNullOrEmpty(fp))
            {
                if (string.IsNullOrEmpty(Info.LocalFilePath))
                {
                    Info.LocalFilePath = Engine.IsPortable ? Path.Combine(Application.StartupPath, fp) : fp;
                }
                else
                {
                    Info.LocalFilePath = Path.ChangeExtension(Info.LocalFilePath, Path.GetExtension(fp));
                }

                if (ZAppHelper.IsTextFile(fp))
                {
                    Job1 = EDataType.Text;
                }
                else if (ZAppHelper.IsImageFile(fp))
                {
                    Job1 = EDataType.Image;
                    IsImage = true;
                    if (TempImage == null && GraphicsMgr.IsValidImage(fp) && Job3 != JobLevel3.CreateAnimatedImage)
                    {
                        TempImage = FileSystem.ImageFromFile(fp); // todo: check if required
                    }
                }
                else
                {
                    Job1 = EDataType.File;
                }
            }
        }

        #endregion Populating Task

        #region Publish Data

        private string CreateThumbnail(string url, FTPUploader fu)
        {
            if (!string.IsNullOrEmpty(url))
            {
                if (CreateThumbnail())
                {
                    double thar = Engine.ConfigUploaders.FTPThumbnailWidthLimit / (double)TempImage.Width;
                    using (
                        Image img = GraphicsMgr.ChangeImageSize(TempImage, Engine.ConfigUploaders.FTPThumbnailWidthLimit,
                                                                (int)(thar * TempImage.Height)))
                    {
                        var sb = new StringBuilder(Path.GetFileNameWithoutExtension(Info.LocalFilePath));
                        sb.Append(".th");
                        sb.Append(Path.GetExtension(Info.LocalFilePath));
                        Debug.Assert(Info.LocalFilePath != null, "Info.LocalFilePath != null");
                        string thPath = Path.Combine(Path.GetDirectoryName(Info.LocalFilePath), sb.ToString());
                        img.Save(thPath);
                        if (File.Exists(thPath))
                        {
                            string thumb = fu.Upload(thPath).URL;

                            if (!string.IsNullOrEmpty(thumb))
                            {
                                return thumb;
                            }
                        }
                    }
                }
                return null;
            }
            return null;
        }

        private string CreateThumbnail(string url, SFTPUploader fu)
        {
            if (!string.IsNullOrEmpty(url))
            {
                if (CreateThumbnail())
                {
                    double thar = Engine.ConfigUploaders.FTPThumbnailWidthLimit / (double)TempImage.Width;
                    using (
                        Image img = GraphicsMgr.ChangeImageSize(TempImage, Engine.ConfigUploaders.FTPThumbnailWidthLimit,
                                                                (int)(thar * TempImage.Height)))
                    {
                        var sb = new StringBuilder(Path.GetFileNameWithoutExtension(Info.LocalFilePath));
                        sb.Append(".th");
                        sb.Append(Path.GetExtension(Info.LocalFilePath));
                        string thPath = Path.Combine(Path.GetDirectoryName(Info.LocalFilePath), sb.ToString());
                        img.Save(thPath);

                        if (File.Exists(thPath))
                        {
                            string thumb = fu.Upload(thPath).URL;

                            if (!string.IsNullOrEmpty(thumb))
                            {
                                return thumb;
                            }
                        }
                    }
                }
                return null;
            }
            return null;
        }

        private void FlashIcon()
        {
            for (int i = 0; i < (int)Engine.ConfigOptions.FlashTrayCount; i++)
            {
                MyWorker.ReportProgress((int)ProgressType.FlashIcon, Resources.zss_uploaded);
                Thread.Sleep(250);
                MyWorker.ReportProgress((int)ProgressType.FlashIcon, Resources.zss_green);
                Thread.Sleep(250);
            }
            MyWorker.ReportProgress((int)ProgressType.FlashIcon, Resources.zss_tray);
        }

        private Stream PrepareDataFromFile(string fp)
        {
            Stream data = null;
            using (var fs = new FileStream(fp, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                data = new MemoryStream();
                fs.CopyStreamTo(data);
            }
            return data;
        }

        private Stream PrepareDataFromImage(Image img)
        {
            Stream data = null;
            DebugHelper.WriteLine(new StackFrame(1).GetMethod().Name + " prepared data from image");
            EImageFormat imageFormat;
            data = WorkerTaskHelper.PrepareImage(WorkflowConfig, Engine.ConfigOptions, img, out imageFormat, bTargetFileSize: true);
            return data;
        }

        private Stream PrepareData()
        {
            Stream data = null;

            if (Job3 == JobLevel3.CreateAnimatedImage)
            {
                WriteImageAnimated();
            }

            if (File.Exists(Info.LocalFilePath)) // priority 1: filepath before image
            {
                DebugHelper.WriteLine(new StackFrame(1).GetMethod().Name + " prepared data from " + Info.LocalFilePath);
                data = PrepareDataFromFile(Info.LocalFilePath);
            }
            else if (TempImage != null)
            {
                data = PrepareDataFromImage(TempImage);
            }
            else if (!string.IsNullOrEmpty(TempText))
            {
                DebugHelper.WriteLine(new StackFrame(1).GetMethod().Name + " prepared data from text");
                data = new MemoryStream(Encoding.UTF8.GetBytes(TempText));
            }

            SetFileSize(data.Length);

            return data;
        }

        public void Print()
        {
            if (WorkflowConfig.DestConfig.Outputs.Contains(OutputEnum.Printer))
            {
                if (TempImage != null)
                {
                    MyWorker.ReportProgress((int)ProgressType.PrintImage, TempImage.Clone());
                }
                else if (!string.IsNullOrEmpty(TempText))
                {
                    MyWorker.ReportProgress((int)ProgressType.PrintText, TempText);
                }
            }
        }

        /// <summary>
        /// Beginining of the background worker tasks
        /// </summary>
        public void PublishData()
        {
            StartTime = DateTime.Now;

            Data = PrepareData();

            if (File.Exists(Info.LocalFilePath) || TempImage != null || !string.IsNullOrEmpty(TempText))
            {
                // Note: We need write text or image first
                if (WorkflowConfig.DestConfig.Outputs.Contains(OutputEnum.LocalDisk))
                {
                    switch (Job1)
                    {
                        case EDataType.Text:
                        case EDataType.URL:
                            WriteText(TempText);
                            break;
                        default:
                            WriteImage(TempImage);
                            break;
                    }

                    var ur_local = new UploadResult
                                       {
                                           Host = OutputEnum.LocalDisk.GetDescription(),
                                           LocalFilePath = Info.LocalFilePath,
                                       };
                    if (!WorkflowConfig.DestConfig.Outputs.Contains(OutputEnum.RemoteHost))
                    {
                        ur_local.URL = ur_local.GetLocalFilePathAsUri(Info.LocalFilePath);
                        AddUploadResult(ur_local);
                    }
                }

                if (WorkflowConfig.DestConfig.Outputs.Contains(OutputEnum.RemoteHost))
                {
                    switch (Job1)
                    {
                        case EDataType.Image:
                            UploadImage();
                            break;

                        case EDataType.Text:
                        case EDataType.URL:
                            switch (Job2)
                            {
                                case JobLevel2.Translate:
                                    SetTranslationInfo(
                                        new GoogleTranslate(Engine.ConfigUI.ApiKeys.GoogleApiKey).TranslateText(TranslationInfo));
                                    SetText(TranslationInfo.Result);
                                    break;
                                default:
                                    UploadText();
                                    break;
                            }
                            break;

                        case EDataType.File:
                            UploadFile();
                            break;
                    }
                }

                if (WorkflowConfig.DestConfig.Outputs.Contains(OutputEnum.SharedFolder))
                {
                    switch (Job1)
                    {
                        case EDataType.File:
                            UploadToSharedFolder(Engine.ConfigUploaders.LocalhostSelectedFiles);
                            break;
                        case EDataType.Image:
                            UploadToSharedFolder(Engine.ConfigUploaders.LocalhostSelectedImages);
                            break;
                        case EDataType.Text:
                        case EDataType.URL:
                            UploadToSharedFolder(Engine.ConfigUploaders.LocalhostSelectedText);
                            break;
                    }
                }

                if (WorkflowConfig.DestConfig.Outputs.Contains(OutputEnum.Email))
                {
                    SendEmail();
                }

                if (WorkflowConfig.DestConfig.Outputs.Contains(OutputEnum.Printer))
                {
                    Print();
                }

                if (WorkflowConfig.DestConfig.Outputs.Contains(OutputEnum.Clipboard) &&
                    WorkflowConfig.DestConfig.Outputs.Count == 1)
                {
                    SetClipboardContent();
                }

                if (UploadResults.Count > 0)
                {
                    FlashIcon();
                }
            }

            EndTime = DateTime.Now;
        }

        public void SendEmail()
        {
            var emailForm =
                new EmailForm(
                    Engine.ConfigUploaders.EmailRememberLastTo ? Engine.ConfigUploaders.EmailLastTo : string.Empty,
                    Engine.ConfigUploaders.EmailDefaultSubject, Engine.ConfigUploaders.EmailDefaultBody);

            if (emailForm.ShowDialog() == DialogResult.OK)
            {
                if (Engine.ConfigUploaders.EmailRememberLastTo)
                {
                    Engine.ConfigUploaders.EmailLastTo = emailForm.ToEmail;
                }

                var email = new Email
                                {
                                    SmtpServer = Engine.ConfigUploaders.EmailSmtpServer,
                                    SmtpPort = Engine.ConfigUploaders.EmailSmtpPort,
                                    FromEmail = Engine.ConfigUploaders.EmailFrom,
                                    Password = Engine.ConfigUploaders.EmailPassword
                                };

                Stream emailData = null;
                try
                {
                    emailData = Data;

                    if (emailData != null && emailData.Length > 0)
                    {
                        email.Send(emailForm.ToEmail, emailForm.Subject, emailForm.Body, emailData, Info.FileName);
                    }
                }
                finally
                {
                    if (emailData != null) emailData.Dispose();
                }
            }
        }

        public void SetClipboardContent()
        {
            var ur_clipboard = new UploadResult();
            ur_clipboard.Host = OutputEnum.Clipboard.GetDescription();
            if (File.Exists(Info.LocalFilePath) &&
                WorkflowConfig.DestConfig.TaskClipboardContent.Contains(ClipboardContentEnum.Local))
            {
                ur_clipboard.LocalFilePath = Info.LocalFilePath;
            }
            AddUploadResult(ur_clipboard);
        }

        private void SetFileSize(long sz)
        {
            var dsz = sz > 1023 ? (sz / 1024.0) : (double)sz;
            var strsz = dsz.ToString("N0", CultureInfo.CurrentCulture.NumberFormat);
            Info.FileSize = string.Format("{0} {1}", strsz, sz > 1023 ? "KiB" : "B");
        }

        public bool ShortenURL(UploadResult urShorturl, string fullUrl)
        {
            if (!string.IsNullOrEmpty(fullUrl))
            {
                Job3 = JobLevel3.ShortenURL;
                URLShortener us = null;

                if (WorkflowConfig.DestConfig.LinkUploaders.Contains(UrlShortenerType.BITLY))
                {
                    us = new BitlyURLShortener(Engine.ConfigUI.ApiKeys.BitlyLogin, Engine.ConfigUI.ApiKeys.BitlyKey);
                }
                else if (WorkflowConfig.DestConfig.LinkUploaders.Contains(UrlShortenerType.Google))
                {
                    us = new GoogleURLShortener(Engine.ConfigUploaders.GoogleURLShortenerAccountType, Engine.ConfigUI.ApiKeys.GoogleApiKey, Engine.ConfigUploaders.GoogleURLShortenerOAuthInfo);
                }
                else if (WorkflowConfig.DestConfig.LinkUploaders.Contains(UrlShortenerType.ISGD))
                {
                    us = new IsgdURLShortener();
                }
                else if (WorkflowConfig.DestConfig.LinkUploaders.Contains(UrlShortenerType.Jmp))
                {
                    us = new JmpURLShortener(Engine.ConfigUI.ApiKeys.BitlyLogin, Engine.ConfigUI.ApiKeys.BitlyKey);
                }
                else if (WorkflowConfig.DestConfig.LinkUploaders.Contains(UrlShortenerType.TINYURL))
                {
                    us = new TinyURLShortener();
                }
                else if (WorkflowConfig.DestConfig.LinkUploaders.Contains(UrlShortenerType.TURL))
                {
                    us = new TurlURLShortener();
                }

                if (us != null)
                {
                    string shortenUrl = us.ShortenURL(fullUrl);

                    if (!string.IsNullOrEmpty(shortenUrl))
                    {
                        DebugHelper.WriteLine(string.Format("Shortened URL: {0}", shortenUrl));
                        urShorturl.Host = us.Host;
                        urShorturl.URL = fullUrl;
                        urShorturl.ShortenedURL = shortenUrl;
                        return true;
                    }
                }
            }

            return false;
        }

        private void UploadFile(FileDestination fileUploaderType, Stream data)
        {
            FileUploader fileUploader = null;

            switch (fileUploaderType)
            {
                case FileDestination.FTP:
                    if (Engine.ConfigUI.ShowFTPSettingsBeforeUploading)
                    {
                        var ucf = new UploadersConfigForm(Engine.ConfigUploaders, Engine.ConfigUI.ApiKeys);
                        ucf.Icon = Resources.zss_main;
                        ucf.tcUploaders.SelectedTab = ucf.tpFileUploaders;
                        ucf.tcFileUploaders.SelectedTab = ucf.tpFTP;
                        ucf.ShowDialog();
                    }
                    switch (Job1)
                    {
                        case EDataType.Text:
                            UploadToFTP(Engine.ConfigUploaders.FTPSelectedText, data);
                            break;
                        case EDataType.Image:
                            UploadToFTP(Engine.ConfigUploaders.FTPSelectedImage, data);
                            break;
                        default:
                        case EDataType.File:
                            UploadToFTP(Engine.ConfigUploaders.FTPSelectedFile, data);
                            break;
                    }
                    break;
                case FileDestination.Minus:
                    fileUploader = new Minus(Engine.ConfigUploaders.MinusConfig, new OAuthInfo(Engine.ConfigUI.ApiKeys.MinusConsumerKey, Engine.ConfigUI.ApiKeys.MinusConsumerSecret));
                    break;
                case FileDestination.Dropbox:
                    string uploadPath = new NameParser { IsFolderPath = true }.Convert(Dropbox.TidyUploadPath(Engine.ConfigUploaders.DropboxUploadPath));
                    fileUploader = new Dropbox(Engine.ConfigUploaders.DropboxOAuthInfo, uploadPath, Engine.ConfigUploaders.DropboxAccountInfo);
                    break;
                case FileDestination.Box:
                    fileUploader = new Box(ZKeys.BoxKey)
                     {
                         AuthToken = Engine.ConfigUploaders.BoxAuthToken,
                         FolderID = Engine.ConfigUploaders.BoxFolderID,
                         Share = Engine.ConfigUploaders.BoxShare
                     };
                    break;
                case FileDestination.SendSpace:
                    fileUploader = new SendSpace(Engine.ConfigUI.ApiKeys.SendSpaceKey);
                    switch (Engine.ConfigUploaders.SendSpaceAccountType)
                    {
                        case AccountType.Anonymous:
                            SendSpaceManager.PrepareUploadInfo(Engine.ConfigUI.ApiKeys.SendSpaceKey);
                            break;
                        case AccountType.User:
                            SendSpaceManager.PrepareUploadInfo(Engine.ConfigUI.ApiKeys.SendSpaceKey, Engine.ConfigUploaders.SendSpaceUsername, Engine.ConfigUploaders.SendSpacePassword);
                            break;
                    }
                    break;
                case FileDestination.RapidShare:
                    fileUploader = new RapidShare(Engine.ConfigUploaders.RapidShareUsername, Engine.ConfigUploaders.RapidSharePassword,
                        Engine.ConfigUploaders.RapidShareFolderID);
                    break;
                case FileDestination.CustomUploader:
                    fileUploader = new CustomUploader(Engine.ConfigUploaders.CustomUploadersList[Engine.ConfigUploaders.CustomUploaderSelected]);
                    break;
            }

            if (fileUploader != null)
            {
                MyWorker.ReportProgress((int)ProgressType.UpdateProgressMax, TaskbarProgressBarState.Indeterminate);
                DestinationName = fileUploaderType.GetDescription();
                DebugHelper.WriteLine("Initialized " + DestinationName);
                fileUploader.ProgressChanged += UploadProgressChanged;
                UploadResult ur_remote_file = fileUploader.Upload(data, Info.FileName);
                if (ur_remote_file != null)
                {
                    ur_remote_file.Host = fileUploaderType.GetDescription();
                    ur_remote_file.LocalFilePath = Info.LocalFilePath;
                    AddUploadResult(ur_remote_file);
                }
                Errors = fileUploader.Errors;
            }
        }

        public void UploadFile()
        {
            DebugHelper.WriteLine("Uploading File: " + Info.LocalFilePath);

            foreach (FileDestination fileUploaderType in WorkflowConfig.DestConfig.FileUploaders)
            {
                UploadFile(fileUploaderType, Data);
            }
        }

        public void UploadImage()
        {
            if (WorkflowConfig.DestConfig.Outputs.Contains(OutputEnum.RemoteHost))
            {
                if (Engine.ConfigUI != null && TempImage != null && Engine.ConfigUI.TinyPicSizeCheck &&
                    WorkflowConfig.DestConfig.ImageUploaders.Contains(ImageDestination.TinyPic))
                {
                    if (TempImage.Width > 1600 || TempImage.Height > 1600)
                    {
                        DebugHelper.WriteLine("Changing from TinyPic to ImageShack due to large image size");
                        if (!WorkflowConfig.DestConfig.ImageUploaders.Contains(ImageDestination.ImageShack))
                        {
                            WorkflowConfig.DestConfig.ImageUploaders.Add(ImageDestination.ImageShack);
                            WorkflowConfig.DestConfig.ImageUploaders.Remove(ImageDestination.TinyPic);
                        }
                    }
                }

                foreach (ImageDestination t in WorkflowConfig.DestConfig.ImageUploaders)
                {
                    UploadImage(t, Data);
                }
            }

            if (Engine.ConfigUI != null && Engine.ConfigUI.ImageUploadRetryOnTimeout &&
                UploadDuration > Engine.ConfigUI.UploadDurationLimit)
            {
                if (!WorkflowConfig.DestConfig.ImageUploaders.Contains(ImageDestination.TinyPic))
                {
                    WorkflowConfig.DestConfig.ImageUploaders.Add(ImageDestination.TinyPic);
                }
                else if (!WorkflowConfig.DestConfig.ImageUploaders.Contains(ImageDestination.TinyPic))
                {
                    WorkflowConfig.DestConfig.ImageUploaders.Add(ImageDestination.ImageShack);
                }
            }
        }

        private void UploadImage(ImageDestination imageUploaderType, Stream data)
        {
            ImageUploader imageUploader = null;

            switch (imageUploaderType)
            {
                case ImageDestination.ImageShack:
                    imageUploader = new ImageShackUploader(Engine.ConfigUI.ApiKeys.ImageShackKey,
                                                           Engine.ConfigUploaders.ImageShackAccountType,
                                                           Engine.ConfigUploaders.ImageShackRegistrationCode)
                                        {
                                            IsPublic = Engine.ConfigUploaders.ImageShackShowImagesInPublic
                                        };
                    break;
                case ImageDestination.TinyPic:
                    imageUploader = new TinyPicUploader(Engine.ConfigUI.ApiKeys.TinyPicID, Engine.ConfigUI.ApiKeys.TinyPicKey,
                                                        Engine.ConfigUploaders.TinyPicAccountType,
                                                        Engine.ConfigUploaders.TinyPicRegistrationCode);
                    break;
                case ImageDestination.Imgur:
                    imageUploader = new Imgur(Engine.ConfigUploaders.ImgurAccountType, Engine.ConfigUI.ApiKeys.ImgurAnonymousKey,
                                              Engine.ConfigUploaders.ImgurOAuthInfo)
                                        {
                                            ThumbnailType = Engine.ConfigUploaders.ImgurThumbnailType
                                        };
                    break;
                case ImageDestination.Flickr:
                    imageUploader = new FlickrUploader(Engine.ConfigUI.ApiKeys.FlickrKey, Engine.ConfigUI.ApiKeys.FlickrSecret,
                                                       Engine.ConfigUploaders.FlickrAuthInfo,
                                                       Engine.ConfigUploaders.FlickrSettings);
                    break;
                case ImageDestination.Photobucket:
                    imageUploader = new Photobucket(Engine.ConfigUploaders.PhotobucketOAuthInfo,
                                                    Engine.ConfigUploaders.PhotobucketAccountInfo);
                    break;
                case ImageDestination.UploadScreenshot:
                    imageUploader = new UploadScreenshot(Engine.ConfigUI.ApiKeys.UploadScreenshotKey);
                    break;
                case ImageDestination.Twitpic:
                    var twitpicOpt = new TwitPicOptions();
                    twitpicOpt.Username = Engine.ConfigUploaders.TwitPicUsername;
                    twitpicOpt.Password = Engine.ConfigUploaders.TwitPicPassword;
                    // twitpicOpt.TwitPicUploadType = Engine.conf.TwitPicUploadMode;
                    twitpicOpt.TwitPicThumbnailMode = Engine.ConfigUploaders.TwitPicThumbnailMode;
                    twitpicOpt.ShowFull = Engine.ConfigUploaders.TwitPicShowFull;
                    imageUploader = new TwitPicUploader(twitpicOpt);
                    break;
                case ImageDestination.Twitsnaps:
                    imageUploader = new TwitSnapsUploader(Engine.ConfigUI.ApiKeys.TwitsnapsKey, Adapter.TwitterGetActiveAccount());
                    break;
                case ImageDestination.yFrog:
                    var yfrogOp = new YfrogOptions(Engine.ConfigUI.ApiKeys.ImageShackKey);
                    yfrogOp.Username = Engine.ConfigUploaders.YFrogUsername;
                    yfrogOp.Password = Engine.ConfigUploaders.YFrogPassword;
                    yfrogOp.Source = Application.ProductName;
                    // yfrogOp.UploadType = Engine.conf.YfrogUploadMode;
                    imageUploader = new YfrogUploader(yfrogOp);
                    break;
                /*case ImageDestination.MediaWiki:
                    UploadToMediaWiki();
                    break;*/
                case ImageDestination.FileUploader:
                    foreach (FileDestination ft in WorkflowConfig.DestConfig.FileUploaders)
                    {
                        UploadFile(ft, data);
                    }
                    break;
            }

            if (imageUploader != null)
            {
                imageUploader.ProgressChanged += (x) => UploadProgressChanged(x);
                DestinationName = WorkflowConfig.DestConfig.ToStringImageUploaders();
                DebugHelper.WriteLine("Initialized " + DestinationName);

                if (data != null)
                {
                    if (Engine.ConfigUI == null)
                    {
                        Engine.ConfigUI = new XMLSettings();
                    }

                    for (int i = 0; i <= Engine.ConfigUI.ErrorRetryCount; i++)
                    {
                        var ur_remote_img = new UploadResult { LocalFilePath = Info.LocalFilePath };
                        ur_remote_img = imageUploader.Upload(data, Info.FileName);
                        ur_remote_img.Host = imageUploaderType.GetDescription();
                        AddUploadResult(ur_remote_img);
                        Errors = imageUploader.Errors;

                        if (UploadResults.Count > 0 && string.IsNullOrEmpty(UploadResults[UploadResults.Count - 1].URL))
                        {
                            MyWorker.ReportProgress((int)ProgressType.ShowTrayWarning,
                                                    string.Format("Retrying... Attempt {1}",
                                                                  imageUploaderType.GetDescription(), i + 1));
                        }
                        else
                        {
                            break;
                        }
                    }
                }
            }
        }

        private void UploadProgressChanged(ProgressManager progress)
        {
            if (Engine.ConfigUI.ShowTrayUploadProgress)
            {
                UploadInfo uploadInfo = UploadManager.GetInfo(Id);
                if (uploadInfo != null)
                {
                    uploadInfo.UploadPercentage = (int)progress.Percentage;
                    MyWorker.ReportProgress((int)ProgressType.ChangeTrayIconProgress, progress);
                }
            }
        }

        public void UploadText()
        {
            MyWorker.ReportProgress((int)ProgressType.UpdateProgressMax, TaskbarProgressBarState.Indeterminate);

            if (ShouldShortenURL(TempText))
            {
                // Need this for shortening URL using Clipboard Upload
                ShortenURL(TempText);
            }
            else if (WorkflowConfig.DestConfig.Outputs.Contains(OutputEnum.RemoteHost))
            {
                foreach (TextDestination textUploaderType in WorkflowConfig.DestConfig.TextUploaders)
                {
                    UploadText(textUploaderType);
                }
            }
        }

        private void UploadText(TextDestination textUploaderType)
        {
            TextUploader textUploader = null;

            switch (textUploaderType)
            {
                case TextDestination.Pastebin:
                    textUploader = new PastebinUploader(Engine.ConfigUI.ApiKeys.PastebinKey, Engine.ConfigUploaders.PastebinSettings);
                    break;
                case TextDestination.PastebinCA:
                    textUploader = new PastebinCaUploader(Engine.ConfigUI.ApiKeys.PastebinCaKey);
                    break;
                case TextDestination.Paste2:
                    textUploader = new Paste2Uploader();
                    break;
                case TextDestination.Slexy:
                    textUploader = new SlexyUploader();
                    break;
                case TextDestination.FileUploader:
                    UploadFile();
                    break;
            }

            if (textUploader != null)
            {
                DestinationName = textUploaderType.GetDescription();
                DebugHelper.WriteLine("Uploading to " + DestinationName);

                string url = string.Empty;

                if (!string.IsNullOrEmpty(TempText))
                {
                    url = textUploader.UploadText(TempText);
                }
                else
                {
                    url = textUploader.UploadTextFile(Info.LocalFilePath);
                }
                var ur_remote_text = new UploadResult
                                         {
                                             LocalFilePath = Info.LocalFilePath,
                                             Host = textUploaderType.GetDescription(),
                                             URL = url
                                         };
                AddUploadResult(ur_remote_text);
                Errors = textUploader.Errors;
            }
        }

        /// <summary>
        /// Funtion to FTP the Screenshot
        /// </summary>
        /// <returns>Retuns a List of Screenshots</returns>
        public UploadResult UploadToFTP(int FtpAccountId, Stream data)
        {
            var ur_remote_file_ftp = new UploadResult
                                         {
                                             LocalFilePath = Info.LocalFilePath,
                                             Host = FileDestination.FTP.GetDescription()
                                         };

            try
            {
                MyWorker.ReportProgress((int)ProgressType.UpdateProgressMax, TaskbarProgressBarState.Indeterminate);

                if (Adapter.CheckFTPAccounts(this, FtpAccountId))
                {
                    FTPAccount acc = Engine.ConfigUploaders.FTPAccountList2[FtpAccountId];
                    DestinationName = string.Format("FTP - {0}", acc.Name);
                    DebugHelper.WriteLine(string.Format("Uploading {0} to FTP: {1}", Info.FileName, acc.Host));

                    MyWorker.ReportProgress((int)ProgressType.UpdateProgressMax, TaskbarProgressBarState.Normal);
                    switch (acc.Protocol)
                    {
                        case FTPProtocol.SFTP:
                            var sftp = new SFTPUploader(acc);
                            if (!sftp.isInstantiated)
                            {
                                Errors.Add(
                                    "An SFTP client couldn't be instantiated, not enough information.\nCould be a missing key file.");
                                return ur_remote_file_ftp;
                            }
                            sftp.ProgressChanged += UploadProgressChanged;
                            ur_remote_file_ftp.URL = File.Exists(Info.LocalFilePath)
                                                         ? sftp.Upload(Info.LocalFilePath).URL
                                                         : sftp.Upload(data, Info.FileName).URL;
                            ur_remote_file_ftp.ThumbnailURL = CreateThumbnail(ur_remote_file_ftp.URL, sftp);
                            break;
                        default:
                            var fu = new FTPUploader(acc);
                            fu.ProgressChanged += UploadProgressChanged;
                            ur_remote_file_ftp.URL = File.Exists(Info.LocalFilePath)
                                                         ? fu.Upload(Info.LocalFilePath).URL
                                                         : fu.Upload(data, Info.FileName).URL;
                            ur_remote_file_ftp.ThumbnailURL = CreateThumbnail(ur_remote_file_ftp.URL, fu);
                            break;
                    }

                    if (!string.IsNullOrEmpty(ur_remote_file_ftp.URL))
                    {
                        AddUploadResult(ur_remote_file_ftp);
                    }
                }
            }
            catch (Exception ex)
            {
                DebugHelper.WriteException(ex, "Error while uploading to FTP Server");
                Errors.Add("FTP upload failed.\r\n" + ex.Message);
            }
            return ur_remote_file_ftp;
        }

        public bool UploadToMediaWiki()
        {
            string fullFilePath = Info.LocalFilePath;

            if (
                Engine.ConfigUploaders.MediaWikiAccountList.HasValidIndex(
                    Engine.ConfigUploaders.MediaWikiAccountSelected) && File.Exists(fullFilePath))
            {
                MediaWikiAccount acc =
                    Engine.ConfigUploaders.MediaWikiAccountList[Engine.ConfigUploaders.MediaWikiAccountSelected];
                IWebProxy proxy = Adapter.CheckProxySettings().GetWebProxy;
                DestinationName = acc.Name;
                DebugHelper.WriteLine(string.Format("Uploading {0} to MediaWiki: {1}", Info.FileName, acc.Url));
                var uploader = new MediaWikiUploader(new MediaWikiOptions(acc, proxy));
                UploadResult ur_remote_img_mediawiki = uploader.UploadImage(Info.LocalFilePath);
                if (ur_remote_img_mediawiki != null)
                {
                    ur_remote_img_mediawiki.LocalFilePath = Info.LocalFilePath;
                    AddUploadResult(ur_remote_img_mediawiki);
                }
                return true;
            }
            return false;
        }

        public void UploadToSharedFolder(int id)
        {
            if (Engine.ConfigUploaders.LocalhostAccountList.HasValidIndex(id))
            {
                LocalhostAccount acc = Engine.ConfigUploaders.LocalhostAccountList[id];
                string fn = string.Empty;
                if (File.Exists(Info.LocalFilePath))
                {
                    fn = Path.GetFileName(Info.LocalFilePath);
                    string destFile = acc.GetLocalhostPath(fn);
                    string destDir = Path.GetDirectoryName(destFile);
                    if (!Directory.Exists(destDir))
                    {
                        Directory.CreateDirectory(destDir);
                    }
                    File.Copy(Info.LocalFilePath, destFile);
                    UpdateLocalFilePath(destFile);
                }
                else if (TempImage != null)
                {
                    EImageFormat imageFormat;
                    Data = WorkerTaskHelper.PrepareImage(WorkflowConfig, Engine.ConfigOptions, TempImage, out imageFormat);
                    fn = WorkerTaskHelper.PrepareFilename(WorkflowConfig, TempImage, imageFormat, GetNameParser());
                    string fp = acc.GetLocalhostPath(fn);
                    FileSystem.WriteImage(fp, Data);
                }
                else if (!string.IsNullOrEmpty(TempText))
                {
                    fn = new NameParser(NameParserType.EntireScreen).Convert(WorkflowConfig.ConfigFileNaming.EntireScreenPattern) +
                         ".txt";
                    string destFile = acc.GetLocalhostPath(fn);
                    FileSystem.WriteText(destFile, TempText);
                }

                var ur = new UploadResult
                             {
                                 Host = OutputEnum.SharedFolder.GetDescription(),
                                 URL = acc.GetUriPath(fn),
                                 LocalFilePath = Info.LocalFilePath
                             };
                UploadResults.Add(ur);
            }
        }

        /// <summary>
        /// Writes MyImage object in a WorkerTask into a file
        /// </summary>
        /// <param name="t">WorkerTask</param>
        public void WriteImage(Image img)
        {
            if (WorkflowConfig.DestConfig.Outputs.Contains(OutputEnum.LocalDisk) && img != null &&
                !States.Contains(TaskState.ImageWritten))
            {
                // PrepareData instead of using Data
                FileInfo fi = FileSystem.WriteImage(Info.LocalFilePath, PrepareDataFromImage(img));
                SetFileSize(fi.Length);
                States.Add(TaskState.ImageWritten);

                if (!File.Exists(Info.LocalFilePath))
                {
                    Errors.Add(string.Format("{0} does not exist", Info.LocalFilePath));
                }
            }
        }

        private void WriteImageAnimated()
        {
            if (tempImages != null && tempImages.Count > 0)
            {
                String outputFilePath = FileSystem.GetUniqueFilePath(WorkflowConfig, Engine.ImagesDir,
                                                                     new NameParser(NameParserType.EntireScreen).Convert
                                                                         (WorkflowConfig.ConfigFileNaming.EntireScreenPattern));

                switch (WorkflowConfig.ImageFormatAnimated)
                {
                    case AnimatedImageFormat.PNG:
                        outputFilePath += ".png";
                        var apng = new Apng();
                        foreach (Image img in tempImages)
                        {
                            apng.AddFrame(new Bitmap(img), WorkflowConfig.ImageAnimatedFramesDelay * 1000, 1000);
                        }

                        apng.WriteApng(outputFilePath);
                        break;

                    default:
                        outputFilePath += ".gif";
                        var enc = new AnimatedGifEncoder();
                        enc.Start(outputFilePath);
                        enc.SetDelay(WorkflowConfig.ImageAnimatedFramesDelay * 1000);
                        enc.SetRepeat(0);
                        foreach (Image img in tempImages)
                        {
                            enc.AddFrame(img);
                        }
                        enc.Finish();
                        break;
                }

                UpdateLocalFilePath(outputFilePath);
                DebugHelper.WriteLine("Wrote animated image: " + outputFilePath);
                tempImages.Clear();
            }
        }

        public string WriteImageAs()
        {
            SaveFileDialog sfd = new SaveFileDialog();
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                return FileSystem.WriteImage(sfd.FileName, PrepareData()).FullName;
            }
            return string.Empty;
        }

        private void WriteText(string text)
        {
            if (!string.IsNullOrEmpty(text) && !string.IsNullOrEmpty(Info.LocalFilePath))
            {
                FileSystem.WriteText(Info.LocalFilePath, text);
            }
        }

        #endregion Publish Data

        public void Start()
        {
            if (Status == TaskStatus.InQueue && !IsStopped)
            {
                OnUploadPreparing();

                //  UploadManager.UpdateProxySettings();

                MyWorker = new BackgroundWorker();
                MyWorker.WorkerReportsProgress = true;
                MyWorker.DoWork += MyWorker_DoWork;
                MyWorker.ProgressChanged += MyWorker_ProgressChanged;
                MyWorker.RunWorkerCompleted += MyWorker_RunWorkerCompleted;
                MyWorker.RunWorkerAsync();
            }
        }

        #region Task Events

        private void OnUploadCompleted()
        {
            Status = TaskStatus.Completed;

            if (!IsStopped)
            {
                Status = TaskStatus.Completed;
            }
            else
            {
                Status = TaskStatus.Stopped;
            }

            if (UploadCompleted != null)
            {
                UploadCompleted(this);
            }

            Dispose();
        }

        private void OnUploadPreparing()
        {
            Status = TaskStatus.Preparing;

            switch (Job1)
            {
                case EDataType.Image:
                case EDataType.Text:
                    Status = TaskStatus.Preparing;
                    break;
                default:
                    Status = TaskStatus.InQueue;
                    break;
            }

            if (UploadPreparing != null)
            {
                UploadPreparing(this);
            }
        }

        private void OnUploadProgressChanged()
        {
            if (UploadProgressChanged2 != null)
            {
                UploadProgressChanged2(this);
            }
        }

        private void OnUploadStarted()
        {
            if (UploadStarted != null)
            {
                UploadStarted(this);
            }
        }

        #endregion Task Events

        #region Upload Methods

        public bool LoadClipboardContent()
        {
            bool succ = true;
            if (Clipboard.ContainsImage())
            {
                succ = SetImage(Clipboard.GetImage());
            }
            else if (Clipboard.ContainsText())
            {
                Engine.zPreviousSetClipboardText = Clipboard.GetText();
                SetText(Engine.zPreviousSetClipboardText);
            }
            else if (Clipboard.ContainsFileDropList())
            {
                List<string> listFiles = FileSystem.GetExplorerFileList(Clipboard.GetFileDropList());
                if (listFiles.Count > 0)
                {
                    UpdateLocalFilePath(listFiles[0]);
                }
                Job1 = EDataType.File;
            }

            return succ;
        }

        #endregion Upload Methods
    }
}
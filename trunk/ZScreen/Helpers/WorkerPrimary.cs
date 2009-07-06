using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZSS.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Drawing;
using System.ComponentModel;
using System.IO;
using ZSS.Properties;
using System.Drawing.Printing;
using Greenshot.Helpers;
using ZSS.TextUploaderLib;
using ZSS.Forms;
using ZSS.ColorsLib;
using ZSS.Global;
using System.Runtime.InteropServices;

namespace ZSS.Helpers
{
    public class WorkerPrimary
    {
        private ZScreen mZScreen;

        internal bool mSetHotkeys;
        internal int mHKSelectedRow = -1;
        private const int mWM_KEYDOWN = 0x0100, mWM_SYSKEYDOWN = 0x0104;
        public IntPtr KeyboardHookHandle = (IntPtr)1; //Used for the keyboard hook

        internal bool mTakingScreenShot;
        internal bool bAutoScreenshotsOpened;
        internal bool bDropWindowOpened;
        internal bool bQuickActionsOpened;
        private bool bQuickOptionsOpened;

        public WorkerPrimary(ZScreen myZScreen)
        {
            this.mZScreen = myZScreen;
        }

        private BackgroundWorker CreateWorker()
        {
            BackgroundWorker bwApp = new BackgroundWorker { WorkerReportsProgress = true };
            bwApp.DoWork += new System.ComponentModel.DoWorkEventHandler(BwApp_DoWork);
            bwApp.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(BwApp_ProgressChanged);
            bwApp.RunWorkerCompleted += new RunWorkerCompletedEventHandler(BwApp_RunWorkerCompleted);
            return bwApp;
        }

        #region "Background Worker"

        private void BwApp_DoWork(object sender, DoWorkEventArgs e)
        {
            MainAppTask task = (MainAppTask)e.Argument;
            task.MyWorker.ReportProgress((int)MainAppTask.ProgressType.SET_ICON_BUSY, task);
            ClipboardManager.Queue();

            if (Program.conf.PromptForUpload && task.ImageDestCategory != ImageDestType.CLIPBOARD &
                task.ImageDestCategory != ImageDestType.FILE &&
                (task.Job == MainAppTask.Jobs.TAKE_SCREENSHOT_SCREEN ||
                task.Job == MainAppTask.Jobs.TAKE_SCREENSHOT_WINDOW_ACTIVE) &&
                MessageBox.Show("Do you really want to upload to " + task.ImageDestCategory.GetDescription() + "?",
                Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
            {
                e.Result = task;
                return;
            }

            if (task.JobCategory == JobCategoryType.SCREENSHOTS)
            {
                if (Program.conf.ScreenshotDelayTime != 0)
                {
                    Thread.Sleep((int)(Program.conf.ScreenshotDelayTime));
                }
            }

            FileSystem.AppendDebug(".");
            FileSystem.AppendDebug(string.Format("Job started: {0}", task.Job));

            switch (task.JobCategory)
            {
                case JobCategoryType.PICTURES:
                    PublishImage(ref task);
                    break;
                case JobCategoryType.SCREENSHOTS:
                    switch (task.Job)
                    {
                        case MainAppTask.Jobs.TAKE_SCREENSHOT_SCREEN:
                            CaptureScreen(ref task);
                            break;
                        case MainAppTask.Jobs.TAKE_SCREENSHOT_WINDOW_SELECTED:
                        case MainAppTask.Jobs.TAKE_SCREENSHOT_CROPPED:
                        case MainAppTask.Jobs.TAKE_SCREENSHOT_LAST_CROPPED:
                            CaptureRegionOrWindow(ref task);
                            break;
                        case MainAppTask.Jobs.CUSTOM_UPLOADER_TEST:
                        case MainAppTask.Jobs.TAKE_SCREENSHOT_WINDOW_ACTIVE:
                            CaptureActiveWindow(ref task);
                            break;
                        case MainAppTask.Jobs.UPLOAD_IMAGE:
                            PublishImage(ref task);
                            break;
                    }
                    break;
                case JobCategoryType.TEXT:
                    switch (task.Job)
                    {
                        case MainAppTask.Jobs.UPLOAD_FROM_CLIPBOARD:
                            PublishText(ref task);
                            break;
                        case MainAppTask.Jobs.LANGUAGE_TRANSLATOR:
                            LanguageTranslator(ref task);
                            break;
                    }
                    break;
                case JobCategoryType.BINARY:
                    switch (task.Job)
                    {
                        case MainAppTask.Jobs.UPLOAD_FROM_CLIPBOARD:
                            if (Program.conf.AutoSwitchFTP)
                            {
                                task.ImageDestCategory = ImageDestType.FTP;
                                PublishBinary(ref task);
                            }
                            break;
                    }
                    break;
            }

            if (!string.IsNullOrEmpty(task.LocalFilePath) && File.Exists(task.LocalFilePath))
            {
                if (Program.conf.AddFailedScreenshot ||
                    (!Program.conf.AddFailedScreenshot && task.Errors.Count == 0 ||
                    task.JobCategory == JobCategoryType.TEXT))
                {
                    task.MyWorker.ReportProgress((int)MainAppTask.ProgressType.ADD_FILE_TO_LISTBOX, new HistoryItem(task));
                }
            }

            e.Result = task;
        }

        private void BwApp_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            switch ((MainAppTask.ProgressType)e.ProgressPercentage)
            {
                case (MainAppTask.ProgressType)101:
                    PrintHelper ph = new PrintHelper(e.UserState as Image);
                    PrinterSettings ps = ph.PrintWithDialog();
                    break;
                case (MainAppTask.ProgressType)102:
                    try
                    {
                        ImageOutput.PrepareClipboardObject();
                        ImageOutput.CopyToClipboard(e.UserState as Image);
                    }
                    catch (Exception ex)
                    {
                        FileSystem.AppendDebug(ex.Message);
                    }
                    break;
                case (MainAppTask.ProgressType)103:
                    ImageOutput.SaveWithDialog(e.UserState as Image);
                    break;
                case MainAppTask.ProgressType.ADD_FILE_TO_LISTBOX:
                    AddHistoryItem((HistoryItem)e.UserState);
                    break;
                case MainAppTask.ProgressType.COPY_TO_CLIPBOARD_IMAGE:
                    if (e.UserState.GetType() == typeof(string))
                    {
                        string f = e.UserState.ToString();
                        if (File.Exists(f))
                        {
                            SaveImageToClipboard(f);
                            FileSystem.AppendDebug(string.Format("Saved {0} as an Image to Clipboard...", f));
                        }
                    }
                    else if (e.UserState.GetType() == typeof(Bitmap))
                    {
                        try
                        {
                            Clipboard.SetImage((Image)e.UserState);
                        }
                        catch (Exception ex)
                        {
                            // Sometimes there are 'Clipboard Set did not succeed' errors
                            FileSystem.AppendDebug(ex.Message);
                        }
                    }
                    break;
                case MainAppTask.ProgressType.FLASH_ICON:
                    mZScreen.niTray.Icon = (Icon)e.UserState;
                    break;
                case MainAppTask.ProgressType.SET_ICON_BUSY:
                    MainAppTask task = (MainAppTask)e.UserState;
                    mZScreen.niTray.Text = mZScreen.Text + " - " + task.Job.GetDescription();
                    mZScreen.niTray.Icon = Resources.zss_busy;
                    break;
                case MainAppTask.ProgressType.UPDATE_CROP_MODE:
                    mZScreen.cboCropGridMode.Checked = Program.conf.CropGridToggle;
                    break;
                case MainAppTask.ProgressType.UPDATE_UPLOAD_DESTINATION:
                    mZScreen.cboImagesDest.SelectedIndex = (int)Program.conf.ScreenshotDestMode;
                    break;
            }
        }

        private void BwApp_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                MainAppTask task = (MainAppTask)e.Result;

                FileSystem.AppendDebug(string.Format("Job completed: {0}", task.Job));

                if (!RetryUpload(task))
                {
                    switch (task.JobCategory)
                    {
                        case JobCategoryType.BINARY:
                            switch (task.Job)
                            {
                                case MainAppTask.Jobs.UPLOAD_FROM_CLIPBOARD:
                                    if (!string.IsNullOrEmpty(task.RemoteFilePath))
                                    {
                                        Clipboard.SetText(task.RemoteFilePath);
                                    }
                                    break;
                            }
                            break;
                        case JobCategoryType.TEXT:
                            switch (task.Job)
                            {
                                case MainAppTask.Jobs.LANGUAGE_TRANSLATOR:
                                    this.mZScreen.txtTranslateText.Text = task.TranslationInfo.SourceText;
                                    this.mZScreen.txtTranslateResult.Text = task.TranslationInfo.Result.TranslatedText;
                                    this.mZScreen.txtLanguages.Text = task.TranslationInfo.Result.TranslationType;
                                    this.mZScreen.txtDictionary.Text = task.TranslationInfo.Result.Dictionary;
                                    if (Program.conf.ClipboardTranslate)
                                    {
                                        Clipboard.SetText(task.TranslationInfo.Result.TranslatedText);
                                    }
                                    this.mZScreen.btnTranslate.Enabled = true;
                                    this.mZScreen.btnTranslateTo1.Enabled = true;
                                    break;
                                case MainAppTask.Jobs.UPLOAD_FROM_CLIPBOARD:
                                    if (!string.IsNullOrEmpty(task.RemoteFilePath))
                                    {
                                        Clipboard.SetText(task.RemoteFilePath);
                                    }
                                    break;
                            }
                            break;
                        case JobCategoryType.SCREENSHOTS:
                            switch (task.Job)
                            {
                                case MainAppTask.Jobs.CUSTOM_UPLOADER_TEST:
                                    if (task.ImageManager != null && task.ImageManager.ImageFileList.Count > 0)
                                    {
                                        if (task.ImageManager.GetFullImageUrl() != "")
                                        {
                                            this.mZScreen.txtUploadersLog.AppendText(task.DestinationName + " full image: " +
                                                task.ImageManager.GetFullImageUrl() + "\r\n");
                                        }
                                        if (task.ImageManager.GetThumbnailUrl() != "")
                                        {
                                            this.mZScreen.txtUploadersLog.AppendText(task.DestinationName + " thumbnail: " +
                                                task.ImageManager.GetThumbnailUrl() + "\r\n");
                                        }
                                    }
                                    this.mZScreen.btnUploadersTest.Enabled = true;
                                    break;
                            }
                            if (task.ImageDestCategory != ImageDestType.FILE)
                            {
                                if (Program.conf.DeleteLocal)
                                {
                                    if (File.Exists(task.LocalFilePath))
                                    {
                                        File.Delete(task.LocalFilePath);
                                    }
                                }
                            }
                            break;
                    }

                    if (task.JobCategory == JobCategoryType.SCREENSHOTS || task.JobCategory == JobCategoryType.PICTURES)
                    {
                        ClipboardManager.AddTask(task);
                        ClipboardManager.SetClipboardText();
                    }

                    if (task.ImageManager != null && !string.IsNullOrEmpty(task.ImageManager.Source))
                    {
                        this.mZScreen.btnOpenSourceText.Enabled = true;
                        this.mZScreen.btnOpenSourceBrowser.Enabled = true;
                        this.mZScreen.btnOpenSourceString.Enabled = true;
                    }

                    this.mZScreen.niTray.Text = this.mZScreen.Text;
                    if (ClipboardManager.Workers > 1)
                    {
                        this.mZScreen.niTray.Icon = Resources.zss_busy;
                    }
                    else
                    {
                        this.mZScreen.niTray.Icon = Resources.zss_tray;
                    }

                    if (task.Job == MainAppTask.Jobs.LANGUAGE_TRANSLATOR || File.Exists(task.LocalFilePath) || !string.IsNullOrEmpty(task.RemoteFilePath))
                    {
                        if (Program.conf.CompleteSound)
                        {
                            System.Media.SystemSounds.Exclamation.Play();
                        }
                        if (Program.conf.ShowBalloonTip)
                        {
                            new BalloonTipHelper(this.mZScreen.niTray, task).ShowBalloonTip();
                        }
                    }

                    if (task.Errors.Count > 0)
                    {
                        FileSystem.AppendDebug(task.Errors[task.Errors.Count - 1]);
                    }
                }

                if (task.MyImage != null) task.MyImage.Dispose(); // For fix memory leak
            }
            catch (Exception ex)
            {
                FileSystem.AppendDebug(ex.ToString());
            }
            finally
            {
                ClipboardManager.Commit();
            }
        }

        #endregion

        internal MainAppTask CreateTask(MainAppTask.Jobs job)
        {
            BackgroundWorker bwApp = CreateWorker();
            MainAppTask task = new MainAppTask(bwApp, job);
            if (task.Job != MainAppTask.Jobs.CUSTOM_UPLOADER_TEST)
            {
                task.ImageDestCategory = Program.conf.ScreenshotDestMode;
            }
            else
            {
                task.ImageDestCategory = ImageDestType.CUSTOM_UPLOADER;
            }
            return task;
        }

        public MainAppTask GetWorkerText(MainAppTask.Jobs job)
        {
            return GetWorkerText(job, "");
        }

        /// <summary>
        /// Worker for Text: Paste2, Pastebin
        /// </summary>
        /// <returns></returns>
        public MainAppTask GetWorkerText(MainAppTask.Jobs job, string localFilePath)
        {
            MainAppTask t = CreateTask(job);
            t.JobCategory = JobCategoryType.TEXT;
            // t.MakeTinyURL = Program.MakeTinyURL();
            t.MyTextUploader = (TextUploader)mZScreen.ucTextUploaders.MyCollection.SelectedItem;
            if (!string.IsNullOrEmpty(localFilePath))
            {
                t.SetLocalFilePath(localFilePath);
            }

            switch (job)
            {
                case MainAppTask.Jobs.LANGUAGE_TRANSLATOR:
                    mZScreen.btnTranslate.Enabled = false;
                    t.TranslationInfo = new GoogleTranslate.TranslationInfo(mZScreen.txtTranslateText.Text, Program.mGTranslator.LanguageOptions.SourceLangList[mZScreen.cbFromLanguage.SelectedIndex],
                        Program.mGTranslator.LanguageOptions.TargetLangList[mZScreen.cbToLanguage.SelectedIndex]);
                    if (t.TranslationInfo.IsEmpty())
                    {
                        mZScreen.btnTranslate.Enabled = true;
                    }
                    break;
            }
            return t;
        }

        public void HistoryRetryUpload(HistoryItem hi)
        {
            if (hi != null && File.Exists(hi.LocalPath))
            {
                MainAppTask task = CreateTask(MainAppTask.Jobs.UPLOAD_IMAGE);
                task.JobCategory = hi.JobCategory;
                task.SetImage(hi.LocalPath);
                task.SetLocalFilePath(hi.LocalPath);
                task.ImageDestCategory = hi.ImageDestCategory;
                task.MyWorker.RunWorkerAsync(task);
            }
        }

        internal void EventJobs(object sender, MainAppTask.Jobs jobs)
        {
            switch (jobs)
            {
                case MainAppTask.Jobs.TAKE_SCREENSHOT_SCREEN:
                    StartBW_EntireScreen();
                    break;
                case MainAppTask.Jobs.TAKE_SCREENSHOT_WINDOW_ACTIVE:
                    StartBW_ActiveWindow();
                    break;
                case MainAppTask.Jobs.TAKE_SCREENSHOT_WINDOW_SELECTED:
                    StartBW_SelectedWindow();
                    break;
                case MainAppTask.Jobs.TAKE_SCREENSHOT_CROPPED:
                    StartBW_CropShot();
                    break;
                case MainAppTask.Jobs.TAKE_SCREENSHOT_LAST_CROPPED:
                    StartBW_LastCropShot();
                    break;
                case MainAppTask.Jobs.AUTO_CAPTURE:
                    ShowAutoCapture();
                    break;
                case MainAppTask.Jobs.UPLOAD_FROM_CLIPBOARD:
                    UploadUsingClipboard();
                    break;
                case MainAppTask.Jobs.PROCESS_DRAG_N_DROP:
                    ShowDropWindow();
                    break;
                case MainAppTask.Jobs.LANGUAGE_TRANSLATOR:
                    StartWorkerTranslator();
                    break;
                case MainAppTask.Jobs.SCREEN_COLOR_PICKER:
                    ScreenColorPicker();
                    break;
            }
        }


        #region "Capture Method"

        private void CaptureActiveWindow(ref MainAppTask task)
        {
            try
            {
                task.CaptureActiveWindow();
                WriteImage(task);
                PublishImage(ref task);
            }
            catch (ArgumentOutOfRangeException aor)
            {
                task.Errors.Add("Invalid FTP Account Selection");
                FileSystem.AppendDebug(aor.ToString());
            }
            catch (Exception ex)
            {
                FileSystem.AppendDebug(ex.ToString());
                if (Program.conf.CaptureEntireScreenOnError)
                {
                    CaptureRegionOrWindow(ref task);
                }
            }
        }

        private string CaptureRegionOrWindow(ref MainAppTask task)
        {
            string filePath = "";
            Image imgSS = null;

            try
            {
                mTakingScreenShot = true;
                imgSS = User32.CaptureScreen(Program.conf.ShowCursor);

                if (task.Job == MainAppTask.Jobs.TAKE_SCREENSHOT_LAST_CROPPED && !Program.LastRegion.IsEmpty)
                {
                    task.SetImage(GraphicsMgr.CropImage(imgSS, Program.LastRegion));
                }
                else
                {
                    Crop c = new Crop((Bitmap)imgSS, task.Job == MainAppTask.Jobs.TAKE_SCREENSHOT_WINDOW_SELECTED);
                    if (c.ShowDialog() == DialogResult.OK)
                    {
                        if (task.Job == MainAppTask.Jobs.TAKE_SCREENSHOT_CROPPED && !Program.LastRegion.IsEmpty)
                        {
                            task.SetImage(GraphicsMgr.CropImage(imgSS, Program.LastRegion));
                        }
                        else if (task.Job == MainAppTask.Jobs.TAKE_SCREENSHOT_WINDOW_SELECTED && !Program.LastCapture.IsEmpty)
                        {
                            task.SetImage(GraphicsMgr.CropImage(imgSS, Program.LastCapture));
                        }
                    }
                }

                mTakingScreenShot = false;
                if (task.MyImage != null)
                {
                    WriteImage(task);
                    PublishImage(ref task);
                }
            }
            catch (Exception ex)
            {
                FileSystem.AppendDebug(ex.ToString());
                task.Errors.Add(ex.Message);
                if (Program.conf.CaptureEntireScreenOnError)
                {
                    CaptureScreen(ref task);
                }
            }
            finally
            {
                task.MyWorker.ReportProgress((int)MainAppTask.ProgressType.UPDATE_CROP_MODE);
                mTakingScreenShot = false;
                if (imgSS != null) imgSS.Dispose();
            }

            return filePath;
        }

        private void CaptureScreen(ref MainAppTask task)
        {
            task.CaptureScreen();
            WriteImage(task);
            PublishImage(ref task);
        }

        private void SaveImageToClipboard(string fullFile)
        {
            if (File.Exists(fullFile))
            {
                Image img = Image.FromFile(fullFile);
                Clipboard.SetImage(img);

                img.Dispose();

            }
        }

        #endregion

        public void LanguageTranslator(ref MainAppTask t)
        {
            t.TranslationInfo.Result = Program.mGTranslator.TranslateText(t.TranslationInfo);
        }

        public void StartWorkerTranslator()
        {
            if (Clipboard.ContainsText())
            {
                StartBW_LanguageTranslator(new GoogleTranslate.TranslationInfo(Clipboard.GetText(),
                    GoogleTranslate.FindLanguage(Program.conf.FromLanguage, Program.mGTranslator.LanguageOptions.SourceLangList),
                    GoogleTranslate.FindLanguage(Program.conf.ToLanguage, Program.mGTranslator.LanguageOptions.TargetLangList)));
            }
        }

        /// <summary>
        /// Worker for Images: Drag n Drop, Image from Clipboard, Custom Uploader
        /// </summary>
        /// <param name="job">Job Type</param>
        /// <param name="localFilePath">Local file path of the image</param>
        private void StartWorkerPictures(MainAppTask.Jobs job, string localFilePath)
        {
            MainAppTask t = CreateTask(job);
            t.JobCategory = JobCategoryType.PICTURES;
            t.MakeTinyURL = Adapter.MakeTinyURL();
            t.SetImage(localFilePath);
            t.SetLocalFilePath(localFilePath);
            t.MyWorker.RunWorkerAsync(t);
        }

        /// <summary>
        /// Worker for Screenshots: Active Window, Crop, Entire Screen
        /// </summary>
        /// <param name="job">Job Type</param>
        public void StartWorkerScreenshots(MainAppTask.Jobs job)
        {
            MainAppTask t = CreateTask(job);
            t.JobCategory = JobCategoryType.SCREENSHOTS;
            t.MakeTinyURL = Adapter.MakeTinyURL();
            t.MyWorker.RunWorkerAsync(t);
        }

        /// <summary>
        /// Worker for Binary: Drag n Drop, Clipboard Upload files from Explorer
        /// </summary>
        /// <param name="job">Job Type</param>
        /// <param name="localFilePath">Local file path of the file</param>
        private void StartWorkerBinary(MainAppTask.Jobs job, string localFilePath)
        {
            MainAppTask t = CreateTask(job);
            t.JobCategory = JobCategoryType.BINARY;
            t.MakeTinyURL = Adapter.MakeTinyURL();
            t.SetLocalFilePath(localFilePath);
            t.MyWorker.RunWorkerAsync(t);
        }

        private void PublishBinary(ref MainAppTask task)
        {
            task.StartTime = DateTime.Now;
            TaskManager tm = new TaskManager(ref task);
            tm.UploadFtp();
            task.EndTime = DateTime.Now;
        }

        /// <summary>
        /// Function to edit Image (Screenshot or Picture) in an Image Editor and Upload
        /// </summary>
        /// <param name="task"></param>
        private void PublishImage(ref MainAppTask task)
        {
            TaskManager tm = new TaskManager(ref task);

            if (task.MyImage != null && Adapter.ImageSoftwareEnabled() && task.Job != MainAppTask.Jobs.UPLOAD_IMAGE)
            {
                tm.ImageEdit();
            }

            if (task.SafeToUpload())
            {
                FileSystem.AppendDebug("File for HDD: " + task.LocalFilePath);
                tm.UploadImage();
            }
        }

        /// <summary>
        /// Function to edit Text in a Text Editor and Upload
        /// </summary>
        /// <param name="task"></param>
        private void PublishText(ref MainAppTask task)
        {
            TaskManager tm = new TaskManager(ref task);
            tm.UploadText();
        }

        private void ScreenshotUsingDragDrop(string fp)
        {
            StartWorkerPictures(MainAppTask.Jobs.PROCESS_DRAG_N_DROP, fp);
        }


        private void ScreenshotUsingDragDrop(string[] paths)
        {
            foreach (string filePath in FileSystem.GetExplorerFileList(paths))
            {
                File.Copy(filePath, FileSystem.GetUniqueFilePath(Path.Combine(
                    Program.ImagesDir, Path.GetFileName(filePath))), true);
                ScreenshotUsingDragDrop(filePath);
            }
        }

        public void ScreenshotUsingHotkeys(object sender, KeyEventArgs e)
        {
            if (mSetHotkeys)
            {
                if (e.KeyData == Keys.Enter)
                {
                    QuitSettingHotkeys();
                }
                else if (e.KeyData == Keys.Escape)
                {
                    mZScreen.dgvHotkeys.Rows[mHKSelectedRow].Cells[1].Value = Keys.None;
                    SetHotkey(mHKSelectedRow, Keys.None);
                }
                else
                {
                    mZScreen.dgvHotkeys.Rows[mHKSelectedRow].Cells[1].Value = e.KeyData.ToSpecialString();
                    SetHotkey(mHKSelectedRow, e.KeyData);
                }
            }
            else
            {
                e.Handled = CheckHotkeys(e.KeyData);
            }
        }

        private bool CheckHotkeys(Keys key)
        {
            if (Program.conf.HotkeyEntireScreen == key) //Entire Screen
            {
                StartBW_EntireScreen();
                return true;
            }
            if (Program.conf.HotkeyActiveWindow == key) //Active Window
            {
                StartBW_ActiveWindow();
                return true;
            }
            if (Program.conf.HotkeySelectedWindow == key) //Selected Window
            {
                StartBW_SelectedWindow();
                return true;
            }
            if (Program.conf.HotkeyCropShot == key) //Crop Shot
            {
                StartBW_CropShot();
                return true;
            }
            if (Program.conf.HotkeyLastCropShot == key) //Last Crop Shot
            {
                StartBW_LastCropShot();
                return true;
            }
            if (Program.conf.HotkeyAutoCapture == key) //Auto Capture
            {
                ShowAutoCapture();
                return true;
            }
            if (Program.conf.HotkeyClipboardUpload == key) //Clipboard Upload
            {
                UploadUsingClipboard();
                return true;
            }
            if (Program.conf.HotkeyDropWindow == key) //Drag & Drop Window
            {
                ShowDropWindow();
                return true;
            }
            if (Program.conf.HotkeyActionsToolbar == key) //Actions Toolbar
            {
                ShowActionsToolbar(true);
                return true;
            }
            if (Program.conf.HotkeyQuickOptions == key) //Quick Options
            {
                ShowQuickOptions();
                return true;
            }
            if (Program.conf.HotkeyLanguageTranslator == key) //Language Translator
            {
                Program.Worker.StartWorkerTranslator();
                return true;
            }
            if (Program.conf.HotkeyScreenColorPicker == key) //Screen Color Picker
            {
                ScreenColorPicker();
                return true;
            }
            return false;
        }

        public void QuitSettingHotkeys()
        {
            if (mSetHotkeys)
            {
                mSetHotkeys = false;

                mZScreen.lblHotkeyStatus.Text = mZScreen.dgvHotkeys.Rows[mHKSelectedRow].Cells[0].Value + " Hotkey Updated.";

                //mZScreen.lblHotkeyStatus.Text = "Aborted Hotkey selection. Click on a Hotkey to set.";

                mHKSelectedRow = -1;
            }
        }

        private void SetHotkey(int row, Keys key)
        {
            SetHotkey(mZScreen.dgvHotkeys.Rows[mHKSelectedRow].Cells[0].Value.ToString(), key);

            mZScreen.lblHotkeyStatus.Text = mZScreen.dgvHotkeys.Rows[mHKSelectedRow].Cells[0].Value + " Hotkey set to: " + key.ToSpecialString() +
                ". Press enter when done setting all desired Hotkeys.";
        }

        private bool SetHotkey(string name, Keys key)
        {
            return Program.conf.SetFieldValue("Hotkey" + name.Replace(" ", ""), key);
        }

        public void UploadUsingClipboard()
        {
            if (Clipboard.ContainsText() && Program.conf.AutoTranslate && Clipboard.GetText().Length <= Program.conf.AutoTranslateLength)
            {
                StartWorkerTranslator();
            }
            else
            {
                List<MainAppTask> textWorkers = new List<MainAppTask>();

                if (Clipboard.ContainsImage())
                {
                    Image cImage = Clipboard.GetImage();
                    string fp = FileSystem.GetFilePath(NameParser.Convert(new NameParserInfo(NameParserType.EntireScreen)), false);
                    fp = FileSystem.SaveImage(cImage, fp);
                    StartWorkerPictures(MainAppTask.Jobs.UPLOAD_FROM_CLIPBOARD, fp);
                }
                else if (Clipboard.ContainsText())
                {
                    MainAppTask temp = GetWorkerText(MainAppTask.Jobs.UPLOAD_FROM_CLIPBOARD);
                    string fp = FileSystem.GetUniqueFilePath(Path.Combine(Program.TextDir,
                        NameParser.Convert(new NameParserInfo("%y.%mo.%d-%h.%mi.%s")) + ".txt"));
                    Adapter.WriteTextToFile(Clipboard.GetText(), fp);
                    temp.SetLocalFilePath(fp);
                    temp.MyText = Clipboard.GetText();
                    textWorkers.Add(temp);
                }
                else if (Clipboard.ContainsFileDropList())
                {
                    List<string> strListFilePath = new List<string>();

                    foreach (string fp in FileSystem.GetExplorerFileList(Clipboard.GetFileDropList()))
                    {
                        try
                        {
                            if (GraphicsMgr.IsValidImage(fp))
                            {
                                string cbFilePath = FileSystem.GetUniqueFilePath(Path.Combine(Program.ImagesDir, Path.GetFileName(fp)));
                                File.Copy(fp, cbFilePath, true);
                                strListFilePath.Add(cbFilePath);
                            }
                            else
                            {
                                strListFilePath.Add(fp); // yes we use the orignal file path
                            }
                        }
                        catch (Exception ex)
                        {
                            FileSystem.AppendDebug(ex.ToString());
                        }
                    }

                    foreach (string fp in strListFilePath)
                    {
                        if (FileSystem.IsValidImageFile(fp))
                        {
                            StartWorkerPictures(MainAppTask.Jobs.UPLOAD_FROM_CLIPBOARD, fp);
                        }
                        else if (FileSystem.IsValidTextFile(fp))
                        {
                            MainAppTask temp = GetWorkerText(MainAppTask.Jobs.UPLOAD_FROM_CLIPBOARD);
                            temp.SetLocalFilePath(fp);
                            using (StreamReader sr = new StreamReader(fp))
                            {
                                temp.MyText = sr.ReadToEnd();
                            }
                            textWorkers.Add(temp);
                        }
                        else
                        {
                            StartWorkerBinary(MainAppTask.Jobs.UPLOAD_FROM_CLIPBOARD, fp);
                        }
                    }
                }

                foreach (MainAppTask temp in textWorkers)
                {
                    if (FileSystem.IsValidLink(temp.MyText))
                    {
                        if (Program.conf.UrlShortenerActive != null)
                        {
                            temp.MyTextUploader = Program.conf.UrlShortenerActive;
                            temp.RunWorker();
                        }
                    }
                    else
                    {
                        if (Program.conf.TextUploaderActive != null)
                        {
                            temp.RunWorker();
                        }
                    }
                }
            }
        }

        private string GetFilePath(MainAppTask.Jobs job)
        {
            switch (job)
            {
                case MainAppTask.Jobs.TAKE_SCREENSHOT_CROPPED:
                case MainAppTask.Jobs.TAKE_SCREENSHOT_LAST_CROPPED:
                case MainAppTask.Jobs.TAKE_SCREENSHOT_SCREEN:
                    return FileSystem.GetFilePath(NameParser.Convert(new NameParserInfo(NameParserType.EntireScreen)), Program.conf.ManualNaming);
                case MainAppTask.Jobs.TAKE_SCREENSHOT_WINDOW_ACTIVE:
                    return FileSystem.GetFilePath(NameParser.Convert(new NameParserInfo(NameParserType.ActiveWindow)), Program.conf.ManualNaming);
            }
            throw new Exception("Unsupported Job for getting File Path.");
        }

        private bool RetryUpload(MainAppTask t)
        {
            if (Program.conf.ImageUploadRetry && t.IsImage && t.Errors.Count > 0 && !t.Retry &&
                (t.ImageDestCategory == ImageDestType.IMAGESHACK || t.ImageDestCategory == ImageDestType.TINYPIC))
            {
                MainAppTask task = CreateTask(MainAppTask.Jobs.UPLOAD_IMAGE);
                task.JobCategory = t.JobCategory;
                task.SetImage(t.LocalFilePath);
                task.SetLocalFilePath(t.LocalFilePath);
                if (t.ImageDestCategory == ImageDestType.IMAGESHACK)
                {
                    task.ImageDestCategory = ImageDestType.TINYPIC;
                }
                else
                {
                    task.ImageDestCategory = ImageDestType.IMAGESHACK;
                }
                task.Retry = true;
                task.MyWorker.RunWorkerAsync(task);
                return true;
            }
            return false;
        }

        private void WriteImage(MainAppTask t)
        {
            if (t.MyImage != null)
            {
                switch (t.Job)
                {
                    case MainAppTask.Jobs.TAKE_SCREENSHOT_CROPPED:
                        t.SetLocalFilePath(this.GetFilePath(MainAppTask.Jobs.TAKE_SCREENSHOT_CROPPED));
                        break;
                    case MainAppTask.Jobs.TAKE_SCREENSHOT_LAST_CROPPED:
                        t.SetLocalFilePath(this.GetFilePath(MainAppTask.Jobs.TAKE_SCREENSHOT_LAST_CROPPED));
                        break;
                    case MainAppTask.Jobs.TAKE_SCREENSHOT_SCREEN:
                        t.SetLocalFilePath(this.GetFilePath(MainAppTask.Jobs.TAKE_SCREENSHOT_SCREEN));
                        break;
                    case MainAppTask.Jobs.TAKE_SCREENSHOT_WINDOW_ACTIVE:
                    case MainAppTask.Jobs.TAKE_SCREENSHOT_WINDOW_SELECTED:
                    case MainAppTask.Jobs.CUSTOM_UPLOADER_TEST:
                        t.SetLocalFilePath(this.GetFilePath(MainAppTask.Jobs.TAKE_SCREENSHOT_WINDOW_ACTIVE));
                        break;
                }
                // Update LocalFilePath again, due to possible PNG to JPG changes
                t.SetLocalFilePath(FileSystem.SaveImage(t.MyImage, t.LocalFilePath));
            }
        }

        internal void AddHistoryItem(HistoryItem hi)
        {
            mZScreen.lbHistory.Items.Insert(0, hi);
            CheckHistoryItems();
            SaveHistoryItems();
            if (mZScreen.lbHistory.Items.Count > 0)
            {
                mZScreen.lbHistory.ClearSelected();
                mZScreen.lbHistory.SelectedIndex = 0;
            }
        }

        #region "Start Workers"

        public void StartBW_LanguageTranslator(GoogleTranslate.TranslationInfo translationInfo)
        {
            if (mZScreen.cbFromLanguage.Items.Count > 0 && mZScreen.cbToLanguage.Items.Count > 0 && !translationInfo.IsEmpty())
            {
                MainAppTask t = CreateTask(MainAppTask.Jobs.LANGUAGE_TRANSLATOR);
                t.JobCategory = JobCategoryType.TEXT;
                mZScreen.btnTranslate.Enabled = false;
                mZScreen.btnTranslateTo1.Enabled = false;
                t.TranslationInfo = translationInfo;
                t.MyWorker.RunWorkerAsync(t);
            }
        }

        public void StartBW_EntireScreen()
        {
            StartWorkerScreenshots(MainAppTask.Jobs.TAKE_SCREENSHOT_SCREEN);
        }

        public void StartBW_ActiveWindow()
        {
            StartWorkerScreenshots(MainAppTask.Jobs.TAKE_SCREENSHOT_WINDOW_ACTIVE);
        }

        public void StartBW_SelectedWindow()
        {
            if (!Program.Worker.mTakingScreenShot)
            {
                StartWorkerScreenshots(MainAppTask.Jobs.TAKE_SCREENSHOT_WINDOW_SELECTED);
            }
        }

        public void StartBW_CropShot()
        {
            if (!Program.Worker.mTakingScreenShot)
            {
                StartWorkerScreenshots(MainAppTask.Jobs.TAKE_SCREENSHOT_CROPPED);
            }
        }

        public void StartBW_LastCropShot()
        {
            StartWorkerScreenshots(MainAppTask.Jobs.TAKE_SCREENSHOT_LAST_CROPPED);
        }

        #region Quick Actions

        /// <summary>
        /// Show Actions Toolbar
        /// </summary>
        /// <param name="manual">If user clicks from Tray Menu then Manual is set to true.</param>
        public void ShowActionsToolbar(bool manual)
        {
            if (!bQuickActionsOpened)
            {
                bQuickActionsOpened = true;
                ToolbarWindow actionsToolbar = new ToolbarWindow { Icon = Resources.zss_main };
                actionsToolbar.Location = Program.conf.ActionToolbarLocation;
                actionsToolbar.EventJob += new JobsEventHandler(EventJobs);
                actionsToolbar.FormClosed += new FormClosedEventHandler(quickActions_FormClosed);
                actionsToolbar.Show();
                if (manual)
                {
                    actionsToolbar.Show();
                    Rectangle taskbar = User32.GetTaskbarRectangle();
                    actionsToolbar.Location = new Point(SystemInformation.PrimaryMonitorSize.Width - actionsToolbar.Width - 100,
                        SystemInformation.PrimaryMonitorSize.Height - taskbar.Height - actionsToolbar.Height - 10);
                }
            }
        }

        private void quickActions_FormClosed(object sender, FormClosedEventArgs e)
        {
            bQuickActionsOpened = false;
        }

        #endregion

        #region "Show Quick Actions"

        public void ShowQuickOptions()
        {
            if (!bQuickOptionsOpened)
            {
                bQuickOptionsOpened = true;
                QuickOptions quickOptions = new QuickOptions { Icon = Resources.zss_main };
                quickOptions.FormClosed += new FormClosedEventHandler(QuickOptionsFormClosed);
                quickOptions.ApplySettings += new EventHandler(QuickOptionsApplySettings);
                quickOptions.Show();
                Rectangle taskbar = User32.GetTaskbarRectangle();
                quickOptions.Location = new Point(SystemInformation.PrimaryMonitorSize.Width - quickOptions.Width - 100,
                    SystemInformation.PrimaryMonitorSize.Height - taskbar.Height - quickOptions.Height - 10);
            }
        }

        private void QuickOptionsApplySettings(object sender, EventArgs e)
        {
            mZScreen.cboImagesDest.SelectedIndex = (int)Program.conf.ScreenshotDestMode;
            mZScreen.cboClipboardTextMode.SelectedIndex = (int)Program.conf.ClipboardUriMode;
        }

        private void QuickOptionsFormClosed(object sender, FormClosedEventArgs e)
        {
            bQuickOptionsOpened = false;
        }

        #endregion

        public void ShowAutoCapture()
        {
            if (!bAutoScreenshotsOpened)
            {
                bAutoScreenshotsOpened = true;
                AutoCapture autoScreenshots = new AutoCapture { Icon = Resources.zss_main };
                autoScreenshots.EventJob += new JobsEventHandler(EventJobs);
                autoScreenshots.FormClosed += new FormClosedEventHandler(autoScreenshots_FormClosed);
                autoScreenshots.Show();
            }
        }

        private void autoScreenshots_FormClosed(object sender, FormClosedEventArgs e)
        {
            bAutoScreenshotsOpened = false;
        }

        public void ShowDropWindow()
        {
            if (!bDropWindowOpened)
            {
                bDropWindowOpened = true;
                DropWindow dw = new DropWindow();
                dw.Result += new StringsEventHandler(dw_Result);
                dw.FormClosed += new FormClosedEventHandler(dw_FormClosed);
                dw.Show();
                Rectangle taskbar = User32.GetTaskbarRectangle();
                if (Program.conf.LastDropBoxPosition == Point.Empty)
                {
                    dw.Location = new Point(SystemInformation.PrimaryMonitorSize.Width - dw.Width - 100,
                        SystemInformation.PrimaryMonitorSize.Height - taskbar.Height - dw.Height - 10);
                }
                else
                {
                    dw.Location = Program.conf.LastDropBoxPosition;
                }
            }
        }

        private void dw_Result(object sender, string[] strings)
        {
            if (strings != null) ScreenshotUsingDragDrop(strings);
        }

        private void dw_FormClosed(object sender, FormClosedEventArgs e)
        {
            bDropWindowOpened = false;
        }

        public void ScreenColorPicker()
        {
            DialogColor dialogColor = new DialogColor { ScreenPicker = true };
            dialogColor.Show();
        }

        #endregion

        #region "Translate"

        public void TranslateTo1()
        {

            if (Program.conf.ToLanguage2 == "?")
            {
                mZScreen.lblToLanguage.BorderStyle = BorderStyle.FixedSingle;
                MessageBox.Show("Drag n drop 'To:' label to this button for be able to set button language.", mZScreen.Text,
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                mZScreen.lblToLanguage.BorderStyle = BorderStyle.None;
            }
            else
            {
                StartBW_LanguageTranslator(new GoogleTranslate.TranslationInfo(mZScreen.txtTranslateText.Text,
                     GoogleTranslate.FindLanguage(Program.conf.FromLanguage, Program.mGTranslator.LanguageOptions.SourceLangList),
                     GoogleTranslate.FindLanguage(Program.conf.ToLanguage2, Program.mGTranslator.LanguageOptions.TargetLangList)));
            }
        }



        #endregion

        #region "ZScreen GUI"

        public void CheckHistoryItems()
        {
            if (mZScreen.lbHistory.Items.Count > Program.conf.HistoryMaxNumber)
            {
                for (int i = mZScreen.lbHistory.Items.Count - 1; i >= Program.conf.HistoryMaxNumber; i--)
                {
                    mZScreen.lbHistory.Items.RemoveAt(i);
                }
            }
            UpdateGuiControlsHistory();
        }

        public void SaveHistoryItems()
        {
            if (Program.conf.HistorySave)
            {
                List<HistoryItem> historyItems = new List<HistoryItem>();
                foreach (HistoryItem item in mZScreen.lbHistory.Items)
                {
                    historyItems.Add(item);
                }
                HistoryManager hm = new HistoryManager(historyItems);
                hm.Save();
            }
        }

        public void UpdateGuiControlsHistory()
        {
            mZScreen.tpHistoryList.Text = string.Format("History List ({0}/{1})", mZScreen.lbHistory.Items.Count, Program.conf.HistoryMaxNumber);
        }

        #endregion
    }
}

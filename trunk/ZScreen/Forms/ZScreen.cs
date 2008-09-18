#region License Information (GPL v2)
/*
    ZScreen - A program that allows you to upload screenshots in one keystroke.
    Copyright (C) 2008  Brandon Zimmerman

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
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;
using System.Collections;
using System.IO;
using System.Runtime.InteropServices;
using Microsoft.Win32;
using System.Diagnostics;
using System.Threading;
using ZSS.Properties;
using ZSS.Tasks;
using System.ComponentModel;
using System.Collections.Generic;
using ZSS.ImageUploader;

namespace ZSS
{
    public partial class ZScreen : Form
    {
        public static string[] mFileTypes = { "png", "jpg", "gif", "bmp", "tif", "emf", "wmf", "ico" };

        public static ImageFormat[] mImageFormats = { ImageFormat.Png, ImageFormat.Jpeg, ImageFormat.Gif, ImageFormat.Bmp, ImageFormat.Tiff, ImageFormat.Emf, ImageFormat.Wmf, ImageFormat.Icon };

        public bool mMinimized = true;


        private System.Resources.ResourceManager mResourceMan = new System.Resources.ResourceManager("ZSS.ZScreen",
                                    System.Reflection.Assembly.GetExecutingAssembly());

        //private static Settings set = Settings.Default;

        private const string mLangRus = "АаБбВвГгДдЕеЁёЖжЗзИиЙйКкЛлМмНнОоПпРрСсТтУуФфХхЦцЧчШшЩщЪъЫыЬьЭэЮюЯя";

        private static string[] mReplacementVars = { "%t", "%mo", "%d", "%y", "%h", "%mi", "%s", "%i", "%pm" };


        private TabPage mLastTab = null;
        // private string mFileName = Program.set.activeWindow;

        private bool mClose = false;
        private bool mAllowUpdateUI = false;
        private bool mDoingNameUpdate = false;
        private bool mTakingScreenShot = false;

        // Used for appending Screenshot URL when taking more than on Screenshot in a short time
        private uint mNumWorkers = 0;
        private StringBuilder mClipboardURLs = new StringBuilder();

        //Used for Click-to-insert Codes (Naming Conventions)
        private string mHadFocus;
        private int mHadFocusAt;

        //Used for the keyboard hook
        public IntPtr m_hID = (IntPtr)1;

        private const int mWM_KEYDOWN = 0x0100;
        private const int mWM_SYSKEYDOWN = 0x0104;

        //Structures

        //Needed for GetWindowRect
        [StructLayout(LayoutKind.Sequential)]
        private struct RECT
        {
            public int left;
            public int top;
            public int right;
            public int bottom;
        }


        public class HistoryItem
        {
            public StringBuilder File;
            public string Url;

            public HistoryItem(StringBuilder file, string url)
            {
                File = file;
                Url = url;
            }

            public override string ToString()
            {
                return File.ToString();
            }
        }

        //Imported Functions


        //Used for the keyboard hook

        [DllImport("user32", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);

        //Used for getting the active Window

        [DllImport("user32")]
        private static extern IntPtr GetWindowDC(IntPtr hWnd);

        [DllImport("user32")]
        private static extern IntPtr GetWindowRect(IntPtr hWnd, ref RECT rect);

        [DllImport("user32")]
        public static extern int GetForegroundWindow();

        [DllImport("user32")]
        public static extern int GetWindowTextW(int hWnd, [MarshalAs(UnmanagedType.LPWStr)] StringBuilder text, int count);



        //End of Imported Functions


        private static string getWindowLabel()
        {
            const int numOfChars = 256;
            int handle = -1;
            StringBuilder sb = new StringBuilder(numOfChars);

            handle = GetForegroundWindow();

            if (GetWindowTextW(handle, sb, numOfChars) > 0)
                return sb.ToString();
            else
                return "";
        }
        private static int getWindowHandle()
        {
            const int numOfChars = 256;
            int handle = -1;
            StringBuilder sb = new StringBuilder(numOfChars);

            handle = GetForegroundWindow();

            if (GetWindowTextW(handle, sb, numOfChars) > 0)
                return handle;
            else
                return -1;
        }

        /// <summary>
        /// Function get 
        /// </summary>
        /// <param name="fName"></param>
        /// <returns></returns>
        private string getScreenshotName(string fName)
        {
            if (Program.conf.ManualNaming)
            {
                Forms.InputBox ib = new ZSS.Forms.InputBox(Resources.ScreenshotName, fName);
                ib.Icon = Properties.Resources.zss_new;
                //ib.BringToFront();
                ib.ShowDialog();
                if (ib.DialogResult == DialogResult.OK)
                {
                    StringBuilder sb = new StringBuilder(ib.Answer);
                    normalize(ref sb);
                    if (!fName.Equals(ib.Answer))
                        fName = string.Format("{0}-{1}", sb.ToString(), DateTime.Now.ToString("yyyyMMddHHmmss"));
                }
                // Thread.Sleep(500);
            }

            return fName;
        }

        public IntPtr keyboardHookCallback(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode >= 0 && !mTakingScreenShot && (wParam == (IntPtr)mWM_KEYDOWN || wParam == (IntPtr)mWM_SYSKEYDOWN))
            {
                if (checkKeys(Program.conf.HKActiveWindow, lParam))
                {
                    //Active window
                    BackgroundWorker bwApp = new BackgroundWorker();
                    bwApp.WorkerReportsProgress = true;
                    bwApp.DoWork += new System.ComponentModel.DoWorkEventHandler(bwApp_DoWork);
                    bwApp.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(bwApp_ProgressChanged);
                    bwApp.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bwApp_RunWorkerCompleted);
                    MainAppTask t = new MainAppTask(bwApp, MainAppTask.Jobs.TAKE_SCREENSHOT_WINDOW);
                    t.ScreenshotName = new StringBuilder(Program.conf.activeWindow);
                    bwApp.RunWorkerAsync(t);
                    //Thread thr = new Thread(new ThreadStart(/*Program.zss.*/activeWindow));
                    //thr.SetApartmentState(ApartmentState.STA);
                    //thr.Start();
                    return m_hID;
                }

                else if (checkKeys(Program.conf.HKCropShot, lParam))
                {

                    //Cropshot
                    BackgroundWorker bwApp = new BackgroundWorker();
                    bwApp.WorkerReportsProgress = true;
                    bwApp.DoWork += new System.ComponentModel.DoWorkEventHandler(bwApp_DoWork);
                    bwApp.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(bwApp_ProgressChanged);
                    bwApp.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bwApp_RunWorkerCompleted);
                    MainAppTask t = new MainAppTask(bwApp, MainAppTask.Jobs.TAKE_SCREENSHOT_CROPPED);
                    t.ScreenshotName = new StringBuilder(Program.conf.entireScreen);
                    bwApp.RunWorkerAsync(t);
                    //Thread thr = new Thread(new ThreadStart(/*Program.zss.*/cropShot));
                    //thr.SetApartmentState(ApartmentState.STA);
                    //thr.Start();
                    return m_hID;
                }

                else if (checkKeys(Program.conf.HKEntireScreen, lParam))
                {
                    //Entire Screen
                    BackgroundWorker bwApp = new BackgroundWorker();
                    bwApp.WorkerReportsProgress = true;
                    bwApp.DoWork += new System.ComponentModel.DoWorkEventHandler(bwApp_DoWork);
                    bwApp.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(bwApp_ProgressChanged);
                    bwApp.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bwApp_RunWorkerCompleted);
                    MainAppTask t = new MainAppTask(bwApp, MainAppTask.Jobs.TAKE_SCREENSHOT_SCREEN);
                    t.ScreenshotName = new StringBuilder(Program.conf.entireScreen);
                    bwApp.RunWorkerAsync(t);
                    //Thread thr = new Thread(new ThreadStart(/*Program.zss.*/entireScreen));
                    //thr.SetApartmentState(ApartmentState.STA);
                    //thr.Start();
                    return m_hID;
                }
            }
            return CallNextHookEx(m_hID, nCode, wParam, lParam);

        }

        private bool checkKeys(HKcombo hkc, IntPtr lParam)
        {
            if (hkc.Mods == null) //0 mods
            {
                if ((Keys)Marshal.ReadInt32(lParam) == hkc.Key)
                    return true;
            }
            else // if(hkc.Mods.Length > 0)
            {
                if (hkc.Mods.Length == 1)
                {
                    if (Control.ModifierKeys == hkc.Mods[0] && (Keys)Marshal.ReadInt32(lParam) == hkc.Key)
                        return true;
                }
                else //if (hkc.Mods.Length == 2)
                {
                    if (Control.ModifierKeys == (hkc.Mods[0] | hkc.Mods[1]) && (Keys)Marshal.ReadInt32(lParam) == hkc.Key)
                        return true;
                }
            }

            return false;
        }

        void bwApp_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            ZSS.Tasks.MainAppTask t = (ZSS.Tasks.MainAppTask)e.Result;
            switch (t.Job)
            {
                case MainAppTask.Jobs.TAKE_SCREENSHOT_WINDOW:
                case MainAppTask.Jobs.TAKE_SCREENSHOT_CROPPED:
                case MainAppTask.Jobs.TAKE_SCREENSHOT_SCREEN:
                    FileSystem.appendDebug(string.Format("Job completed: {0}", t.Job.ToString()));
                    if (File.Exists(t.ScreenshotLocalPath))
                    {
                        if (Program.conf.DeleteLocal)
                            File.Delete(t.ScreenshotLocalPath);

                        setClipboardText(t.ScreenshotList);
                    }
                    break;
            }

            niTray.Icon = Resources.zss_new;

            mNumWorkers--;
            FileSystem.writeDebugFile();
        }

        private void setClipboardText(List<ImageFile> ifl)
        {
            if (mNumWorkers == 1)
                mClipboardURLs = new StringBuilder();

            if (ifl != null)
            {
                foreach (ImageFile imgf in ifl)
                {
                    switch (Program.conf.ClipboardUriMode)
                    {
                        case ClipboardUriType.FULL:
                            if (imgf.Type == ImageFile.ImageType.FULLIMAGE)
                            {
                                mClipboardURLs.Append(imgf.URI);
                                mClipboardURLs.Append(Environment.NewLine);
                            }
                            break;
                        case ClipboardUriType.THUMBNAIL_FORUMS1:
                            if (imgf.Type == ImageFile.ImageType.THUMBNAIL_FORUMS1)
                            {
                                mClipboardURLs.Append(imgf.URI);
                                mClipboardURLs.Append(Environment.NewLine);
                            }
                            break;
                        case ClipboardUriType.ALL:
                            mClipboardURLs.Append(imgf.URI);
                            mClipboardURLs.Append(Environment.NewLine);
                            break;
                    }

                }
                Clipboard.SetDataObject(mClipboardURLs.ToString().Trim(), true, 3, 1000);
            }
        }

        private void setClipboardText(string url)
        {
            try
            {
                FileSystem.appendDebug(string.Format("Copied URL: {0} to Clipboard...", url));
                if (mNumWorkers > 1)
                {
                    mClipboardURLs.Append(url);
                    mClipboardURLs.Append("\n");
                }
                else
                {
                    mClipboardURLs = new StringBuilder();
                    mClipboardURLs.Append(url);
                    mClipboardURLs.Append("\n");
                }

                Clipboard.SetDataObject(mClipboardURLs.ToString().Trim(), true);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        void bwApp_ProgressChanged(object sender, System.ComponentModel.ProgressChangedEventArgs e)
        {
            ZSS.Tasks.MainAppTask.ProgressType p = (ZSS.Tasks.MainAppTask.ProgressType)e.ProgressPercentage;
            switch (p)
            {
                case MainAppTask.ProgressType.ADD_FILE_TO_LISTBOX:
                    lbHistory.Items.Insert(0, (HistoryItem)e.UserState);
                    break;

                case MainAppTask.ProgressType.COPY_TO_CLIPBOARD_IMAGE:
                    string f = e.UserState.ToString();
                    if (File.Exists(f))
                    {
                        saveImageToClipboard(f);
                        FileSystem.appendDebug(string.Format("Saved {0} as an Image to Clipboard...", f));
                    }
                    break;

                case ZSS.Tasks.MainAppTask.ProgressType.COPY_TO_CLIPBOARD_URL:
                    string url = e.UserState.ToString();
                    setClipboardText(url);
                    break;

                case MainAppTask.ProgressType.FLASH_ICON:
                    niTray.Icon = (Icon)e.UserState;
                    break;

                case MainAppTask.ProgressType.SET_ICON_BUSY:
                    niTray.Icon = Properties.Resources.zss_busy;
                    break;
            }

        }

        void bwApp_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {

            ZSS.Tasks.MainAppTask t = (ZSS.Tasks.MainAppTask)e.Argument;
            t.myWorker.ReportProgress((int)MainAppTask.ProgressType.SET_ICON_BUSY);

            FileSystem.appendDebug(".");
            FileSystem.appendDebug(string.Format("Job started: {0}", t.Job.ToString()));

            switch (t.Job)
            {
                case MainAppTask.Jobs.TAKE_SCREENSHOT_CROPPED:
                    mNumWorkers++;
                    captureCrop(t);
                    break;
                case MainAppTask.Jobs.TAKE_SCREENSHOT_SCREEN:
                    mNumWorkers++;
                    captureScreen(t);
                    break;
                case MainAppTask.Jobs.TAKE_SCREENSHOT_WINDOW:
                    mNumWorkers++;
                    captureActiveWindow(ref t);
                    break;

            }

            e.Result = t;

            //throw new NotImplementedException();
        }

        private void imageSoftwareAndOrFTP(ref MainAppTask task)
        {
            List<ImageFile> ifl = new List<ImageFile>();
            Console.WriteLine("File for FTP: " + task.ScreenshotName.ToString());
            Console.WriteLine("File for HDD: " + task.ScreenshotLocalPath);

            if (Program.conf.ISenabled)
            {
                //if (Program.conf.ISpath != "")
                ImageSoftware(ref task);
            }
            else
            {
                UploadScreenshot(ref task);
            }

        }

        /// <summary>
        /// Funtion to FTP the Screenshot
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="fullFilePath"></param>
        /// <returns>Retuns a List of Screenshots</returns>
        private bool uploadFtp(ref MainAppTask task)
        {
            bool succ = true;
            string httpPath = "";
            string fullFilePath = task.ScreenshotLocalPath;

            if (File.Exists(fullFilePath))
            {
                //there may be bugs from this line
                FTPAccount acc = Program.conf.FTPAccountList[Program.conf.FTPselected];

                httpPath = acc.getUriPath(Path.GetFileName(task.ScreenshotLocalPath));
                task.ScreenshotRemotePath = httpPath;
                task.myWorker.ReportProgress((int)Tasks.MainAppTask.ProgressType.COPY_TO_CLIPBOARD_URL, httpPath);

                FileSystem.appendDebug(string.Format("Uploading {0} to FTP: {1}", task.ScreenshotName, acc.Server));

                // ftpUploadFile(ref acc, task.ScreenshotName, fullFilePath);
                ImageUploader.FTPUploader fu = new ZSS.ImageUploader.FTPUploader(acc);
                fu.EnableThumbnail = Program.conf.EnableThumbnail;
                fu.WorkingDir = Program.conf.CacheDir;
                task.ScreenshotList = fu.UploadImage(fullFilePath);
            }

            return succ;
        }

        private bool uploadImageShack(ref MainAppTask task)
        {
            bool succ = true;
            string fullFilePath = task.ScreenshotLocalPath;
            if (File.Exists(fullFilePath))
            {
                ImageShackUploader su = new ImageShackUploader();
                task.ScreenshotList = su.UploadImage(fullFilePath);

                //Set remote path for Screenshots history
                foreach (ImageFile imf in task.ScreenshotList)
                {
                    if (imf.Type == ImageFile.ImageType.FULLIMAGE)
                    {
                        task.ScreenshotRemotePath = imf.URI;
                        break;
                    }
                }
            }

            return succ;

        }

        private void saveImageToClipboard(string fullFile)
        {
            if (File.Exists(fullFile))
            {
                Image img = Image.FromFile(fullFile);
                Clipboard.SetImage(img);

                img.Dispose();

                if (Program.conf.DeleteLocal)
                    File.Delete(fullFile);
            }
        }

        private void ImageSoftware(ref MainAppTask task)
        {
            // List<ImageFile> ifl = new List<ImageFile>();

            if (File.Exists(task.ScreenshotLocalPath))
            {
                //Thread thred = new Thread(new ParameterizedThreadStart(ISthread(task)));
                //Process proc = Process.Start("\"" + @Program.set.ISpath + "\"", "\"" + @fullFilePath + "\"");
                Process p = new Process();
                ProcessStartInfo psi = new ProcessStartInfo(Program.conf.ImageSoftwareActive.Path);
                psi.Arguments = string.Format("{0}{1}{0}", "\"", task.ScreenshotLocalPath);
                p.StartInfo = psi;
                p.Start();
                // Wait till user quits the ScreenshotEditApp
                p.WaitForExit();

                // upload to ftpUpload or save to clipboard
                UploadScreenshot(ref task);

            }
        }

        private void UploadScreenshot(ref MainAppTask task)
        {
            switch (Program.conf.ScreenshotDestMode)
            {
                case ScreenshotDestType.CLIPBOARD:
                    task.myWorker.ReportProgress((int)MainAppTask.ProgressType.COPY_TO_CLIPBOARD_IMAGE,
                                                    task.ScreenshotLocalPath);
                    break;
                case ScreenshotDestType.FTP:
                    uploadFtp(ref task);
                    break;
                case ScreenshotDestType.IMAGESHACK:
                    uploadImageShack(ref task);
                    break;
            }

            if (Program.conf.ScreenshotDestMode != ScreenshotDestType.FILE)
            {
                if (File.Exists(task.ScreenshotLocalPath))
                {
                    //add URL to history
                    task.myWorker.ReportProgress((int)Tasks.MainAppTask.ProgressType.ADD_FILE_TO_LISTBOX, new HistoryItem(task.ScreenshotName, task.ScreenshotRemotePath));


                    //flash Icon after done
                    for (int i = 0; i < (int)Program.conf.FlashTrayCount; i++)
                    {
                        task.myWorker.ReportProgress((int)MainAppTask.ProgressType.FLASH_ICON, Resources.zss_uploaded);
                        Thread.Sleep(250);
                        task.myWorker.ReportProgress((int)MainAppTask.ProgressType.FLASH_ICON, Resources.zss_new);
                        Thread.Sleep(250);
                    }
                }
            }

            task.myWorker.ReportProgress((int)MainAppTask.ProgressType.FLASH_ICON, Resources.zss_new);
        }

        private string getFileName(StringBuilder file)
        {
            string filePath = Program.conf.path + "\\" + file + "." + mFileTypes[Program.conf.FileFormat];

            if (Program.conf.ManualNaming)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(Path.Combine(Path.GetDirectoryName(filePath), getScreenshotName(Path.GetFileNameWithoutExtension(filePath))));
                sb.Append(Path.GetExtension(filePath));
                filePath = sb.ToString();
            }

            return filePath;
        }

        private void captureActiveWindow(ref MainAppTask task)
        {
            string filePath = "";
            try
            {
                string time = DateTime.Now.ToShortTimeString();

                IntPtr wind = (IntPtr)getWindowHandle();

                Image img = grabWindow(wind);

                StringBuilder file = task.ScreenshotName;
                convertAW(ref file);

                filePath = saveImage(img, getFileName(file));

                file = new StringBuilder(Path.GetFileName(filePath));

                task.ScreenshotName = file;
                task.ScreenshotLocalPath = filePath;
                imageSoftwareAndOrFTP(ref task);
            }
            catch
            {
                captureCrop(task);
            }

        }
        /*
        private string captureCrop(MainAppTask task)
        {
            string filePath = "";

            try
            {
                mTakingScreenShot = true;

                StringBuilder file = task.ScreenshotName;
                convertES(ref file);

                string tmpFullFile = Program.conf.path + "\\" + file + "." + mFileTypes[Program.conf.FileFormat];
                filePath = grabScreen(tmpFullFile, mImageFormats[Program.conf.FileFormat], Program.conf.ManualNaming);

                Crop c = new Crop(filePath);
                c.ShowDialog();

                filePath = convertIfOverSize(filePath, mFileTypes[Program.conf.SwitchFormat], false);
                file = new StringBuilder(Path.GetFileName(filePath));

                mTakingScreenShot = false;
                task.ScreenshotName = file;
                task.ScreenshotLocalPath = filePath;
                imageSoftwareAndOrFTP(ref task);
            }
            catch
            {
                captureScreen(task);
            }

            return filePath;
        }
        */

        private string captureCrop(MainAppTask task)
        {
            string filePath = "";

            try
            {
                mTakingScreenShot = true;

                Image img = grabScreen();

                Crop c = new Crop(img);
                c.ShowDialog();

                img = Program.returnedCroppedImage;

                StringBuilder file = task.ScreenshotName;

                if (img != null)
                {
                    convertES(ref file);

                    filePath = saveImage(img, getFileName(file));
                    file = new StringBuilder(Path.GetFileName(filePath));
                }

                mTakingScreenShot = false;

                task.ScreenshotName = file;
                task.ScreenshotLocalPath = filePath;
                imageSoftwareAndOrFTP(ref task);
            }
            catch
            {
                captureScreen(task);
            }

            return filePath;
        }

        /*
        private string captureScreen(MainAppTask task)
        {
            StringBuilder file = task.ScreenshotName;
            convertES(ref file);

            string filePath = grabScreen(Program.conf.path + "\\" + file + "." + mFileTypes[Program.conf.FileFormat], mImageFormats[Program.conf.FileFormat], Program.conf.ManualNaming);
            filePath = convertIfOverSize(filePath, mFileTypes[Program.conf.SwitchFormat], false);

            file = new StringBuilder(Path.GetFileName(filePath));

            task.ScreenshotName = file;
            task.ScreenshotLocalPath = filePath;
            imageSoftwareAndOrFTP(ref task);

            return filePath;
        }
        */

        private string captureScreen(MainAppTask task)
        {
            Image img = grabScreen();

            StringBuilder file = task.ScreenshotName;
            convertES(ref file);

            string filePath = saveImage(img, getFileName(file));

            file = new StringBuilder(Path.GetFileName(filePath));

            task.ScreenshotName = file;
            task.ScreenshotLocalPath = filePath;
            imageSoftwareAndOrFTP(ref task);

            return filePath;
        }

        /*
        /// <summary>
        /// Function to return a new captured window if the fileName size is over the limit
        /// </summary>
        /// <param name="fullFilePath">Screenshot fileName path of the original format</param>
        /// <param name="newExt">Extension that the fileName format should switch to</param>
        /// <param name="activewindow"></param>
        /// <returns>Returns new fileName path of the screenshot</returns>
        private string convertIfOverSize(string filePath, string newExt, bool activewindow)
        {
            string oldExt = Path.GetExtension(filePath); // oldExt.Insert(0, ".");
            long size = Program.conf.SwitchAfter * 1024;
            newExt = newExt.Insert(0, ".");

            if (File.Exists(filePath) && size != 0)
            {
                FileStream fi = File.Open(filePath, FileMode.Open);
                long len = fi.Length;
                fi.Close();

                if (len > size)
                {
                    File.Delete(filePath);
                    filePath = Path.ChangeExtension(filePath, newExt);
                    if (activewindow)
                    {
                        filePath = grabWindow((IntPtr)getWindowHandle(), filePath, mImageFormats[Program.conf.SwitchFormat], false);
                    }
                    else
                    {
                        filePath = grabScreen(filePath, mImageFormats[Program.conf.SwitchFormat], false);
                    }
                }
            }

            return filePath;


        }
        */

        /// <summary>
        /// Function to return the file path of a captured image. ImageFormat is based on length of the image.
        /// </summary>
        /// <param name="img">The actual image</param>
        /// <param name="filePath">The path to where the image will be saved</param>
        /// <returns>Returns the file path to a screenshot</returns>
        private string saveImage(Image img, string filePath)
        {
            long size = Program.conf.SwitchAfter * 1024;

            MemoryStream ms = new MemoryStream();
            saveImageToMemoryStream(img, ms, mImageFormats[Program.conf.FileFormat]);

            long len = ms.Length;

            if (len > size && size != 0)
            {
                ms = new MemoryStream();
                saveImageToMemoryStream(img, ms, mImageFormats[Program.conf.SwitchFormat]);

                filePath = Path.ChangeExtension(filePath, mFileTypes[Program.conf.SwitchFormat]);
            }

            if (!Directory.Exists(Path.GetDirectoryName(filePath)))
                Directory.CreateDirectory(Path.GetDirectoryName(filePath));

            FileStream fi = File.Create(filePath);

            ms.WriteTo(fi);

            fi.Close();

            ms.Dispose();
            fi.Dispose();

            return filePath;
        }


        /*
        public string saveImage(Bitmap bmp, string filePath, ImageFormat format)
        {
            //image quality setting only works for JPEG

            if (format == ImageFormat.Jpeg)
            {
                EncoderParameter quality = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, Program.conf.ImageQuality);
                ImageCodecInfo codec = getEncoderInfo("image/jpeg");

                EncoderParameters encoderParams = new EncoderParameters(1);
                encoderParams.Param[0] = quality;

                bmp.Save(filePath, codec, encoderParams);
            }
            else
            {
                bmp.Save(filePath, format);
            }

            return filePath;
        }
        */

        public void saveImageToMemoryStream(Image img, MemoryStream ms, ImageFormat format)
        {
            //image quality setting only works for JPEG

            if (format == ImageFormat.Jpeg)
            {
                EncoderParameter quality = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, Program.conf.ImageQuality);
                ImageCodecInfo codec = getEncoderInfo("image/jpeg");

                EncoderParameters encoderParams = new EncoderParameters(1);
                encoderParams.Param[0] = quality;

                img.Save(ms, codec, encoderParams);
            }
            else
            {
                img.Save(ms, format);
            }
        }

        private ImageCodecInfo getEncoderInfo(string mimeType)
        {
            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageEncoders();

            for (int x = 0; x < codecs.Length; x++)
                if (codecs[x].MimeType == mimeType)
                    return codecs[x];
            return null;
        }
        /*
        private string grabScreen(string filePath, ImageFormat format, bool prompt)
        {
            Bitmap bmp = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height, PixelFormat.Format32bppArgb);

            Graphics gfx = Graphics.FromImage(bmp);
            gfx.CopyFromScreen(Screen.PrimaryScreen.Bounds.X, Screen.PrimaryScreen.Bounds.Y, 0, 0, Screen.PrimaryScreen.Bounds.Size, CopyPixelOperation.SourceCopy);

            //Cursors.Default.Draw(gfx, new Rectangle(Cursor.Position.X, Cursor.Position.Y, Cursor.Size.Width, Cursor.Size.Height));

            if (prompt)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(Path.Combine(Path.GetDirectoryName(filePath), getScreenshotName(Path.GetFileNameWithoutExtension(filePath))));
                sb.Append(Path.GetExtension(filePath));
                filePath = sb.ToString();

            }

            saveImage(bmp, filePath, format);

            gfx.Dispose();
            bmp.Dispose();

            return filePath;
        }
        */

        private Image grabScreen()
        {
            Image img = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height, PixelFormat.Format32bppArgb);

            Graphics gfx = Graphics.FromImage(img);
            gfx.CopyFromScreen(Screen.PrimaryScreen.Bounds.X, Screen.PrimaryScreen.Bounds.Y, 0, 0, Screen.PrimaryScreen.Bounds.Size, CopyPixelOperation.SourceCopy);

            //Cursors.Default.Draw(gfx, new Rectangle(Cursor.Position.X, Cursor.Position.Y, Cursor.Size.Width, Cursor.Size.Height));

            return img;
        }

        /*
        private string grabWindow(IntPtr handle, string filePath, ImageFormat format, bool prompt)
        {
            IntPtr hdcSrc = GetWindowDC(handle);
            RECT windowRect = new RECT();
            GetWindowRect(handle, ref windowRect);
            int width = windowRect.right - windowRect.left;
            int height = windowRect.bottom - windowRect.top;

            Bitmap bmp = new Bitmap(width, height, PixelFormat.Format32bppArgb);
            Graphics gfx = Graphics.FromImage(bmp);
            gfx.CopyFromScreen(windowRect.left, windowRect.top, 0, 0, new Size(width, height), CopyPixelOperation.SourceCopy);

            if (prompt)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(Path.Combine(Path.GetDirectoryName(filePath), getScreenshotName(Path.GetFileNameWithoutExtension(filePath))));
                sb.Append(Path.GetExtension(filePath));
                filePath = sb.ToString();
            }
            saveImage(bmp, filePath, format);

            gfx.Dispose();
            bmp.Dispose();

            return filePath;
        }
        */

        private Image grabWindow(IntPtr handle)
        {
            IntPtr hdcSrc = GetWindowDC(handle);
            RECT windowRect = new RECT();
            GetWindowRect(handle, ref windowRect);
            int width = windowRect.right - windowRect.left;
            int height = windowRect.bottom - windowRect.top;

            Image img = new Bitmap(width, height, PixelFormat.Format32bppArgb);
            Graphics gfx = Graphics.FromImage(img);
            gfx.CopyFromScreen(windowRect.left, windowRect.top, 0, 0, new Size(width, height), CopyPixelOperation.SourceCopy);

            return img;
        }

        private string fixNum(int i)
        {
            string temp = i.ToString();
            if (i < 10)
                temp = temp.Insert(0, "0");
            return temp;
        }

        private void normalize(ref StringBuilder sb)
        {
            StringBuilder temp = new StringBuilder("");

            foreach (char c in sb.ToString())
            {
                if (char.IsLetterOrDigit(c) || c == '.' || c == '-' || mLangRus.IndexOf(c) != -1)
                    temp.Append(c);
                if (c == ' ')
                    temp.Append('_');
            }

            sb = temp;
        }



        private void cleanCache()
        {
            System.ComponentModel.BackgroundWorker bw = new System.ComponentModel.BackgroundWorker();
            bw.DoWork += new System.ComponentModel.DoWorkEventHandler(bwCache_DoWork);
            bw.RunWorkerAsync();

        }

        private void bwCache_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            ZSS.Tasks.CacheCleanerTask t = new ZSS.Tasks.CacheCleanerTask(Program.conf.CacheDir, Program.conf.ScreenshotCacheSize);

        }

        public ZScreen()
        {
            InitializeComponent();

            // Set Icon
            this.Icon = Properties.Resources.zss_new;
            
            // Set Name
            lblLogo.Text = string.Format("{0} v{1}", Application.ProductName, Application.ProductVersion);

            // Update GUI Controls
            setupScreen();

            //checkTsmIS();

            cleanCache();

            //show settings if never ran before
            if (Program.conf.RunOnce == false)
            {
                mMinimized = false;
                Show();
                WindowState = FormWindowState.Normal;
                this.Activate();
                this.BringToFront();
                //Form.ActiveForm.BringToFront();

                lblFirstRun.Visible = true;

                Program.conf.activeWindow = Properties.Resources.activeWindowDefault;
                Program.conf.entireScreen = Properties.Resources.entireScreenDefault;

                Program.conf.RunOnce = true;
                // Program.set.Save(); // could crash on upgrade
            }
            else
            {
                Hide();
            }

        }

        private void ZScreen_Resize(object sender, EventArgs e)
        {
            if (FormWindowState.Minimized == WindowState)
            {
                Hide();
                mMinimized = true;
            }

        }

        private void ni_DoubleClick(object sender, EventArgs e)
        {
            mMinimized = false;
            Show();
            WindowState = FormWindowState.Normal;
            this.Activate();
            Form.ActiveForm.BringToFront();

        }

        private void ZScreen_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!mClose && e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                Hide();
            }
            Program.conf.Save();
            FileSystem.appendDebug("Closed " + Application.ProductName + "\n");
            FileSystem.writeDebugFile();
        }

        private void exitZScreenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            mClose = true;
            Close();
        }

        private void btnUpdateFTP_Click(object sender, EventArgs e)
        {
            //tsmFTP.Checked = Program.conf.ScreenshotDestMode == ScreenshotDestType.FTP;
            //saveToUpdate(true);
            updateFTP();
        }

        private FTPAccount getFTPAccountFromFields()
        {
            int port = 21;
            try { port = Int32.Parse(txtServerPort.Text); }
            catch { }

            trimFTPControls();

            FTPAccount acc = new FTPAccount();
            acc.Name = txtName.Text;
            acc.Server = txtServer.Text;
            acc.Port = port;
            acc.Username = txtUsername.Text;
            acc.Password = txtPassword.Text;
            if (!txtPath.Text.StartsWith("/"))
                txtPath.Text = string.Concat("/", txtPath.Text);
            acc.Path = txtPath.Text;
            acc.HttpPath = txtHttpPath.Text;
            acc.IsActive = rbFTPActive.Checked;

            return acc;
        }

        private bool validateFTP()
        {
            Control[] controls = { txtServer, txtUsername, txtPassword, txtPath };

            foreach (Control c in controls)
            {
                if (String.IsNullOrEmpty(c.Text))
                    return false;
            }

            return true;
        }

        private void updateFTP()
        {
            if (validateFTP() //txtServer.Text != "" && txtUsername.Text != "" && txtPassword.Text != "" && txtPath.Text != ""
                && lbFTPAccounts.SelectedIndices.Count == 1 && lbFTPAccounts.SelectedIndex != -1)
            {
                //unset error message (if applicable)
                //lblError1.Visible = false;
                txtErrorFTP.Text = Properties.Resources.FTPupdated;

                FTPAccount acc = getFTPAccountFromFields();

                if (Program.conf.FTPAccountList != null)
                {
                    Program.conf.FTPAccountList[lbFTPAccounts.SelectedIndex] = acc; //use selected index instead of 0
                }
                else
                {
                    Program.conf.FTPAccountList = new List<FTPAccount>();
                    Program.conf.FTPAccountList.Add(acc);
                }

                lbFTPAccounts.Items[lbFTPAccounts.SelectedIndex] = acc.Name;

                rewriteFTPRightClickMenu();
            }
            else
            {
                //Set error message
                mMinimized = false;
                Show();
                //lblError1.Visible = true;
                txtErrorFTP.Text = Properties.Resources.FTPnotUpdated;
                //tsmFTP.Checked = false;
            }
        }

        private void rewriteISRightClickMenu()
        {
            if (Program.conf.ImageSoftwareList != null)
            {
                tsmImageSoftware.DropDownDirection = ToolStripDropDownDirection.Right;

                tsmImageSoftware.DropDownItems.Clear();

                List<ImageSoftware> imgs = Program.conf.ImageSoftwareList;

                ToolStripMenuItem tsm;

                //tsm.TextDirection = ToolStripTextDirection.Horizontal;
                tsmImageSoftware.DropDownDirection = ToolStripDropDownDirection.Right;

                tsm = new ToolStripMenuItem();
                tsm.Text = Properties.Resources.Disabled;
                tsm.CheckOnClick = true;
                tsm.Click += new EventHandler(disableImageSoftware_Click);

                tsmImageSoftware.DropDownItems.Add(tsm);

                tsmImageSoftware.DropDownItems.Add(new ToolStripSeparator());

                for (int x = 0; x < imgs.Count; x++)
                {
                    tsm = new ToolStripMenuItem();
                    //tsm.Tag = x;
                    tsm.CheckOnClick = true;

                    //add event handler
                    tsm.Click += new EventHandler(rightClickISItem_Click);

                    tsm.Text = imgs[x].Name;
                    tsmImageSoftware.DropDownItems.Add(tsm);

                }

                //check the active ftpUpload account

                if (Program.conf.ISenabled)
                    checkCorrectISRightClickMenu(Program.conf.ImageSoftwareActive.Name);
                else
                    checkCorrectISRightClickMenu(tsmImageSoftware.DropDownItems[0].Text);

                tsmImageSoftware.DropDownDirection = ToolStripDropDownDirection.Right;

                //show drop down menu in the correct place if menu is selected
                if (tsmImageSoftware.Selected == true)
                {
                    tsmImageSoftware.DropDown.Hide();
                    tsmImageSoftware.DropDown.Show();
                }
            }
        }

        private void disableImageSoftware_Click(object sender, EventArgs e)
        {
            //cbRunImageSoftware.Checked = false;

            //select "Disabled"
            lbImageSoftware.SelectedIndex = 0;

            checkCorrectISRightClickMenu(tsmImageSoftware.DropDownItems[0].Text); //disabled
            //rewriteISRightClickMenu();
        }

        private void rightClickISItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem tsm = (ToolStripMenuItem)sender;

            Program.conf.ImageSoftwareActive = Program.GetImageSoftware(tsm.Text); //Program.conf.ImageSoftwareList[(int)tsm.Tag];

            if (lbImageSoftware.Items.IndexOf(tsm.Text) >= 0)
                lbImageSoftware.SelectedItem = tsm.Text;

            //Turn on Image Software
            //cbRunImageSoftware.Checked = true;

            //rewriteISRightClickMenu();
        }

        private void checkCorrectISRightClickMenu(string txt)
        {
            ToolStripMenuItem tsm;

            for (int x = 0; x < tsmImageSoftware.DropDownItems.Count; x++)
            {
                //if (tsmImageSoftware.DropDownItems[x].GetType() == typeof(ToolStripMenuItem))
                if (tsmImageSoftware.DropDownItems[x] is ToolStripMenuItem)
                {
                    tsm = (ToolStripMenuItem)tsmImageSoftware.DropDownItems[x];

                    if (tsm.Text == txt)
                    {
                        tsm.CheckState = CheckState.Checked;
                    }
                    else
                    {
                        tsm.CheckState = CheckState.Unchecked;
                    }
                }
            }
        }

        private void rewriteFTPRightClickMenu()
        {
            if (Program.conf.FTPAccountList != null)
            {
                tsmDestFTP.DropDownDirection = ToolStripDropDownDirection.Right;

                tsmDestFTP.DropDownItems.Clear();

                List<FTPAccount> accs = Program.conf.FTPAccountList;

                ToolStripMenuItem tsm;

                //tsm.TextDirection = ToolStripTextDirection.Horizontal;
                tsmDestFTP.DropDownDirection = ToolStripDropDownDirection.Right;


                for (int x = 0; x < accs.Count; x++)
                {
                    tsm = new ToolStripMenuItem();
                    tsm.Tag = x;
                    tsm.CheckOnClick = true;

                    //add event handler
                    tsm.Click += new EventHandler(rightClickFTPItem_Click);

                    tsm.Text = accs[x].Name;
                    tsmDestFTP.DropDownItems.Add(tsm);

                }

                //check the active ftpUpload account

                checkCorrectFTPAccountRightClickMenu(Program.conf.FTPselected);

                tsmDestFTP.DropDownDirection = ToolStripDropDownDirection.Right;

                //show drop down menu in the correct place if menu is selected
                if (tsmDestFTP.Selected == true)
                {
                    tsmDestFTP.DropDown.Hide();
                    tsmDestFTP.DropDown.Show();
                }
            }
        }

        private void rightClickFTPItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem tsm = (ToolStripMenuItem)sender;

            //Program.conf.FTPselected = (int)tsm.Tag;

            lbFTPAccounts.SelectedIndex = (int)tsm.Tag;

            //rewriteFTPRightClickMenu();
        }

        private void checkCorrectFTPAccountRightClickMenu(int index)
        {
            ToolStripMenuItem tsm;

            for (int x = 0; x < tsmDestFTP.DropDownItems.Count; x++)
            {
                tsm = (ToolStripMenuItem)tsmDestFTP.DropDownItems[x];

                if (index == x)
                {
                    tsm.CheckState = CheckState.Checked;
                }
                else
                {
                    tsm.CheckState = CheckState.Unchecked;
                }
            }
        }

        private void btnUpdateHotKeys_Click(object sender, EventArgs e)
        {
            //validate hotkeys
            hkAW.validateHK();
            hkCS.validateHK();
            hkES.validateHK();

            //set hotkey setting
            Program.conf.HKActiveWindow = hkAW.getHotkey();
            Program.conf.HKCropShot = hkCS.getHotkey();
            Program.conf.HKEntireScreen = hkES.getHotkey();
        }

        private bool checkStartWithWindows()
        {
            RegistryKey regkey = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run");
            if ((string)regkey.GetValue(Application.ProductName, "null", RegistryValueOptions.None) != "null")
            {
                Registry.CurrentUser.Flush();
                return true;
            }
            Registry.CurrentUser.Flush();
            return false;
        }

        private void sShowLicense()
        {
            string lic = FileSystem.getTextFromFile(Path.Combine(Application.StartupPath, "Resources\\license.txt"));
            lic = lic != string.Empty ? lic : FileSystem.getText("license.txt");
            if (lic != string.Empty)
            {
                frmTextViewer v = new frmTextViewer(string.Format("{0} - {1}",
                    Application.ProductName, "License"), lic);
                v.Icon = this.Icon;
                v.ShowDialog();
            }

        }

        private void sShowVersionHistory()
        {
            string h = FileSystem.getTextFromFile(Path.Combine(Application.StartupPath, "Resources\\VersionHistory.txt"));
            if (h == string.Empty)
            {
                h = FileSystem.getText("VersionHistory.txt");
            }
            if (h != string.Empty)
            {
                frmTextViewer v = new frmTextViewer(string.Format("{0} - {1}",
                    Application.ProductName, "Version History"), h);
                v.Icon = this.Icon;
                v.ShowDialog();
            }
        }

        private void setupScreen()
        {
            //////////////////////////////
            // Configure Settings
            /////////////////////////////
            // Configure FTP Accounts List
            if (Program.conf.FTPAccountList == null)
            {
                Program.conf.FTPAccountList = new List<FTPAccount>();
            }
            if (Program.conf.FTPAccountList.Count == 0)
            {
                Program.conf.FTPAccountList.Add(new FTPAccount(Resources.NewAccount));
            }
            // Configure ImageSoftware List
            if (Program.conf.ImageSoftwareList == null)
            {
                Program.conf.ImageSoftwareList = new List<ImageSoftware>();
            }
            if (Program.conf.ImageSoftwareActive == null)
            {
                Program.conf.ImageSoftwareActive = new ImageSoftware();
                Program.conf.ImageSoftwareActive.Name = "MS Paint";
                Program.conf.ImageSoftwareActive.Path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.System), "mspaint.exe");
            }
            if (Program.conf.ImageSoftwareList.Count == 0)
            {
                Program.conf.ImageSoftwareList.Add(Program.conf.ImageSoftwareActive);
            }

            ///////////////////////////////////
            // Main Tab
            //////////////////////////////////
            //add languages to combobox
            cmbLanguage.Items.AddRange(Program.languages);
            //select language (doesn't update UI because allowUpdateUI = false)            
            cmbLanguage.SelectedIndex = Array.IndexOf(Program.langCode, Program.conf.Culture);
            //allows the UI to be updated after the combobox is set
            mAllowUpdateUI = true;
            //Depending on the language sets or removes PM
            setPMvisible();
            //cbRunImageSoftware.Checked = Program.conf.ISenabled;

            ///////////////////////////////////
            // FTP Settings
            ///////////////////////////////////
            //load up FTP accounts
            if (Program.conf.FTPAccountList != null)
            {
                List<FTPAccount> accs = Program.conf.FTPAccountList;

                lbFTPAccounts.Items.Clear();
                for (int x = 0; x < accs.Count; x++)
                {
                    lbFTPAccounts.Items.Add(accs[x].Name);
                }
            }
            //selects the ftpUpload account used last time... or the default ftpUpload account
            if (lbFTPAccounts.Items.Count > 0)
                lbFTPAccounts.SelectedIndex = Program.conf.FTPselected;

            //Display selected FTP account
            int sel = lbFTPAccounts.SelectedIndex;
            FTPAccount acc;
            if (Program.conf.FTPAccountList != null &&
                Program.conf.FTPAccountList.Count >= sel && sel != -1 &&
                Program.conf.FTPAccountList[sel] != null)
            {
                acc = Program.conf.FTPAccountList[sel];
                txtName.Text = acc.Name;
                txtServer.Text = acc.Server;
                txtServerPort.Text = acc.Port.ToString();
                txtUsername.Text = acc.Username;
                txtPassword.Text = acc.Password;
                txtPath.Text = acc.Path;
                txtHttpPath.Text = acc.HttpPath;
                rbFTPActive.Checked = acc.IsActive;
                rbFTPPassive.Checked = !acc.IsActive;
                cbDeleteLocal.Checked = Program.conf.DeleteLocal;
            }

            ///////////////////////////////////
            // Image Software Settings
            ///////////////////////////////////
            //Add "Disabled" to the top of the Image Software List
            lbImageSoftware.Items.Add(Properties.Resources.Disabled);

            foreach (ImageSoftware app in Program.conf.ImageSoftwareList)
            {
                lbImageSoftware.Items.Add(app.Name);
            }

            int i;
            if (Program.conf.ISenabled)
            {
                if ((i = lbImageSoftware.Items.IndexOf(Program.conf.ImageSoftwareActive.Name)) != -1)
                    lbImageSoftware.SelectedIndex = i;
            }
            else
            {
                //Set to disabled
                lbImageSoftware.SelectedIndex = 0;
            }
            txtImageSoftwarePath.Enabled = false;
            //cbRunImageSoftware.Checked = Program.conf.ISenabled;


            ///////////////////////////////////
            // Main/File Settings
            ///////////////////////////////////  
            chkEnableThumbnail.Checked = Program.conf.EnableThumbnail;
            cboClipboardTextMode.SelectedIndex = (int)Program.conf.ClipboardUriMode;
            //updateClipboardTextTrayMenu();
            cboScreenshotDest.SelectedIndex = (int)Program.conf.ScreenshotDestMode;

            // 
            //check start with windows
            cbStartWin.Checked = checkStartWithWindows();

            ///////////////////////////////////
            // localFilePath Settings
            ///////////////////////////////////
            gbAutoFileName.Enabled = !chkManualNaming.Checked;
            gbCodeTitle.Enabled = !chkManualNaming.Checked;

            if (Program.conf.path != null && Program.conf.path != "")
            {
                //display the previous fileName settings
                txtFileDirectory.Enabled = true;
                txtFileDirectory.Text = Program.conf.path;
                txtFileDirectory.Enabled = false;
            }
            else //go ahead and use the default directory
            {

                try
                {
                    string strFileDirectory = System.Environment.GetFolderPath(Environment.SpecialFolder.MyPictures) + "\\" + Application.ProductName + " Images";

                    if (!Directory.Exists(strFileDirectory))
                        Directory.CreateDirectory(strFileDirectory);

                    Program.conf.path = strFileDirectory;
                    txtFileDirectory.Enabled = true;
                    txtFileDirectory.Text = strFileDirectory;
                    txtFileDirectory.Enabled = false;
                    //Program.conf.Save();

                }
                catch
                { }
            }



            //file settings
            txtActiveWindow.Text = Program.conf.activeWindow;
            txtEntireScreen.Text = Program.conf.entireScreen;

            cmbFileFormat.Items.AddRange(mFileTypes);
            cmbSwitchFormat.Items.AddRange(mFileTypes);

            cmbFileFormat.SelectedIndex = Program.conf.FileFormat;
            txtSwitchAfter.Text = Program.conf.SwitchAfter.ToString();
            cmbSwitchFormat.SelectedIndex = Program.conf.SwitchFormat;
            txtImageQuality.Text = Program.conf.ImageQuality.ToString();
            chkManualNaming.Checked = Program.conf.ManualNaming;

            //tsmSaveToClip.Checked = Program.conf.ScreenshotDestMode == ScreenshotDestType.CLIPBOARD;
            tsmPromptFileName.Checked = Program.conf.ManualNaming;

            ///////////////////////////////////
            // Advanced Settings
            ///////////////////////////////////
            // configure files and folders
            if (string.IsNullOrEmpty(Program.conf.CacheDir))
            {
                string dir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), Application.ProductName);
                if (!Directory.Exists(dir))
                    Directory.CreateDirectory(dir);
                Program.conf.CacheDir = dir;
            }
            txtCacheDir.Text = Program.conf.CacheDir;
            //advanced
            txtCacheDir.Text = Program.conf.CacheDir;
            nudCacheSize.Value = Program.conf.ScreenshotCacheSize;
            nudFlashIconCount.Value = Program.conf.FlashTrayCount;

            hkAW.updateHotkey(Program.conf.HKActiveWindow);
            hkCS.updateHotkey(Program.conf.HKCropShot);
            hkES.updateHotkey(Program.conf.HKEntireScreen);


        }

        private void updateClipboardTextTrayMenu()
        {

            foreach (ToolStripMenuItem tsmi in tsmCbCopy.DropDownItems)
            {
                tsmi.Checked = false;
            }

            switch (Program.conf.ClipboardUriMode)
            {
                case ClipboardUriType.ALL:
                    tsmCbAll.Checked = true; ;
                    break;
                case ClipboardUriType.FULL:
                    tsmCbFullImage.Checked = true; ;
                    break;
                case ClipboardUriType.THUMBNAIL_FORUMS1:
                    tsmCbThumb.Checked = true; ;
                    break;
            }

        }

        private void btnMinimize_Click(object sender, EventArgs e)
        {
            Hide();
        }

        private void convertAW(ref StringBuilder sb)
        {
            //default AW
            //%t-%mo.%d.%y-%h.%mi.%s%pm

            //auto-increment
            if (sb.ToString().Contains("%i"))
            {
                if (Program.conf.awincrement != 0)
                    Program.conf.awincrement += 1;
                else
                    Program.conf.awincrement = 1;
                //Program.conf.Save();
            }

            DateTime dt = DateTime.Now;

            if (sb.ToString().Contains(mReplacementVars[8]))
            {
                sb = sb.Replace(mReplacementVars[4], !Properties.Resources.PMvisible ? dt.Hour.ToString() : (dt.Hour == 0 ? "12" : ((dt.Hour > 12 ? fixNum(dt.Hour - 12) : fixNum(dt.Hour)))));
            }
            else
            {
                sb = sb.Replace(mReplacementVars[4], fixNum(dt.Hour));
            }

            sb = sb.Replace(mReplacementVars[0], getWindowLabel())
                      .Replace(mReplacementVars[1], fixNum(dt.Month))
                      .Replace(mReplacementVars[2], fixNum(dt.Day))
                      .Replace(mReplacementVars[3], dt.Year.ToString())
                      .Replace(mReplacementVars[5], fixNum(dt.Minute))
                      .Replace(mReplacementVars[6], fixNum(dt.Second))
                      .Replace(mReplacementVars[7], addZeroes(Program.conf.awincrement, 4))
                      .Replace(mReplacementVars[8], (dt.Hour >= 12 ? "PM" : "AM"));

            //normalize the entire thing, allow only characters and digits
            //spaces become underscores, prevents possible problems
            normalize(ref sb);

        }

        private void convertES(ref StringBuilder sb)
        {
            //default ES
            //SS-%mo.%d.%y-%h.%mi.%s%pm

            //auto-increment
            if (sb.ToString().Contains("%i"))
            {
                if (Program.conf.esincrement != 0)
                    Program.conf.esincrement += 1;
                else
                    Program.conf.esincrement = 1;
                //Program.conf.Save();
            }

            DateTime dt = DateTime.Now;

            if (sb.ToString().Contains(mReplacementVars[8]))
            {
                sb = sb.Replace(mReplacementVars[4], !Properties.Resources.PMvisible ? dt.Hour.ToString() : (dt.Hour == 0 ? "12" : ((dt.Hour > 12 ? fixNum(dt.Hour - 12) : fixNum(dt.Hour)))));
            }
            else
            {
                sb = sb.Replace(mReplacementVars[4], fixNum(dt.Hour));
            }

            sb = sb.Replace(mReplacementVars[0], "")
                      .Replace(mReplacementVars[1], fixNum(dt.Month))
                      .Replace(mReplacementVars[2], fixNum(dt.Day))
                      .Replace(mReplacementVars[3], dt.Year.ToString())
                      .Replace(mReplacementVars[5], fixNum(dt.Minute))
                      .Replace(mReplacementVars[6], fixNum(dt.Second))
                      .Replace(mReplacementVars[7], addZeroes(Program.conf.esincrement, 4))
                      .Replace(mReplacementVars[8], (dt.Hour >= 12 ? "PM" : "AM"));



            //normalize the entire thing, allow only characters, digits and periods
            //spaces become underscores, prevents possible problems
            normalize(ref sb);
        }

        private string addZeroes(int i, int digits)
        {
            string str = "";

            int len = i.ToString().Length;

            int k = 0;

            if (len > 0 && digits > 0)
            {
                while (k < (digits - len))
                {
                    str += "0";
                    k++;
                }

                str += i;
            }

            return str;
        }

        private void btnBrowseDirectory_Click(object sender, EventArgs e)
        {
            browseDirectory();
        }

        private void browseDirectory()
        {
            //get old directory
            string old = txtFileDirectory.Text;

            txtFileDirectory.Enabled = true;

            //choose directory / create directory popup
            //remember old directory and display it
            FolderBrowserDialog dlg = new FolderBrowserDialog();
            dlg.SelectedPath = txtFileDirectory.Text;
            dlg.ShowNewFolderButton = true;
            dlg.ShowDialog();

            if (dlg.SelectedPath != "")
            {
                Program.conf.path = dlg.SelectedPath;
                txtFileDirectory.Text = dlg.SelectedPath;
            }
            else
            {
                Program.conf.path = old;
                txtFileDirectory.Text = old;
            }
            txtFileDirectory.Enabled = false;
        }

        private void btnResetActiveWindow_Click(object sender, EventArgs e)
        {
            Program.conf.activeWindow = Properties.Resources.activeWindowDefault;
            txtActiveWindow.Text = Program.conf.activeWindow;
        }

        private void btnResetEntireScreen_Click(object sender, EventArgs e)
        {
            Program.conf.entireScreen = Properties.Resources.entireScreenDefault;
            txtEntireScreen.Text = Program.conf.entireScreen;
        }

        private void btnCopyToClipboard_Click(object sender, EventArgs e)
        {
            string str = "";

            ArrayList items = new ArrayList();

            foreach (int i in lbHistory.SelectedIndices)
            {
                items.Add(((HistoryItem)lbHistory.Items[i]).Url);
            }

            if (cbReverse.Checked)
                items.Reverse();

            foreach (string url in items)
            {
                str += url + System.Environment.NewLine;
            }



            if (str != "")
            {
                if (cbAddSpace.Checked)
                    str = str.Insert(0, System.Environment.NewLine);
                str = str.TrimEnd(System.Environment.NewLine.ToCharArray()); ;

                //Set clipboard data
                Clipboard.SetDataObject(str);
            }
        }

        private void tsmDir_Click(object sender, EventArgs e)
        {
            browseDirectory();
        }

        private void btnUpdateImageSoftware_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtImageSoftwareName.Text) &&
                Program.GetImageSoftware(txtImageSoftwareName.Text) == null &&
                !string.IsNullOrEmpty(txtImageSoftwarePath.Text))
            {
                int sel;
                bool isActiveImageSoftware = false;

                if ((sel = lbImageSoftware.SelectedIndex) > 0)
                {
                    if (Program.conf.ImageSoftwareActive.Name == (string)lbImageSoftware.SelectedItem)
                        isActiveImageSoftware = true;
                    ImageSoftware temp = new ImageSoftware();
                    temp.Name = txtImageSoftwareName.Text;
                    temp.Path = txtImageSoftwarePath.Text;
                    Program.conf.ImageSoftwareList[sel - 1] = temp;
                    lbImageSoftware.Items[sel] = temp.Name;

                    if (isActiveImageSoftware)
                    {
                        if (Program.conf.ISenabled)
                        {
                            Program.conf.ImageSoftwareActive = temp;
                            checkCorrectISRightClickMenu(temp.Name);
                        }
                    }
                }
            }

            rewriteISRightClickMenu();
        }

        private void btnBrowseImageSoftware_Click(object sender, EventArgs e)
        {
            browseImageSoftware();
        }

        private void browseImageSoftware()
        {
            //get old path
            string old = txtImageSoftwarePath.Text;

            txtImageSoftwarePath.Enabled = true;

            //choose path / create path popup
            //remember old path and display it

            OpenFileDialog dlg = new OpenFileDialog();
            if (txtImageSoftwarePath.Text != "")
                dlg.FileName = txtImageSoftwarePath.Text;

            dlg.Filter = "Executable files (*.exe)|*.exe";

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                txtImageSoftwarePath.Text = dlg.FileName;
                if (String.IsNullOrEmpty(txtImageSoftwareName.Text))
                {
                    txtImageSoftwareName.Text =
                        Path.GetFileNameWithoutExtension(dlg.FileName);
                }
            }

            txtImageSoftwarePath.Enabled = false;


        }

        private void tsmSettings_Click(object sender, EventArgs e)
        {
            mLastTab = tpMain;
            bringUpMenu();
        }

        private void btnTestConnection_Click(object sender, EventArgs e)
        {
            txtErrorFTP.Text = /*"Testing...";*/ Properties.Resources.FTPtest;
            txtErrorFTP.Update();
            trimFTPControls();
            int port = 21;

            Int32.TryParse(txtServerPort.Text, out port);

            try
            {

                FTP ff = new FTP();
                ff.server = txtServer.Text;
                ff.port = port;
                ff.user = txtUsername.Text;
                ff.pass = txtPassword.Text;
                ff.PassiveMode = rbFTPPassive.Checked;

                ff.Connect();
                ff.ChangeDir(txtPath.Text);

                //removed binary mode

                ff.Disconnect();

                // cbFTPEnabled.Checked = true;
                txtErrorFTP.Text = /*"Success";*/ Properties.Resources.FTPsuccess;
            }
            catch (Exception t)
            {
                txtErrorFTP.Text = Program.replaceErrorMessages(t.Message);
            }

        }

        private void trimFTPControls()
        {
            TextBox[] arr = { txtServer, txtPath, txtHttpPath };
            foreach (TextBox tb in arr)
            {
                if (tb.Text != "/")
                {
                    tb.Text = tb.Text.TrimEnd("/".ToCharArray()).TrimEnd("\\".ToCharArray());
                    tb.Update();
                }
            }
        }

        private void tsmViewDirectory_Click(object sender, EventArgs e)
        {
            Process proc = new Process();
            proc.StartInfo.FileName = "explorer";
            proc.StartInfo.Arguments = Program.conf.path;

            proc.Start();
        }

        private void sShowHelp()
        {
            String pHelpApp = Path.Combine(Environment.GetEnvironmentVariable("windir"), "hh.exe");
            String pHelpFile = Path.Combine(Application.StartupPath, "docs.chm");

            if (File.Exists(pHelpApp) && File.Exists(pHelpFile))
            {
                Process proc = new Process();
                proc.StartInfo.FileName = Path.Combine(Environment.GetEnvironmentVariable("windir"), "hh.exe");

                proc.StartInfo.Arguments = pHelpFile;
                proc.Start();

                if (mLastTab == null)
                    tabControl.SelectedTab = tpMain;
                else
                    tabControl.SelectedTab = mLastTab;

                tabControl.Focus();
            }
            else
            {
                Process.Start("http://www.brandonz.net/projects/zscreen/documentation.html");
            }
        }

        private void tpDocumentation_Enter(object sender, EventArgs e)
        {
            sShowHelp();

        }

        private void tsmViewRemote_Click(object sender, EventArgs e)
        {
            ViewRemote vr = new ViewRemote();
            vr.ShowDialog(this);
        }

        private void registerCode(string code)
        {
            if (mHadFocus == "aw")
            {
                txtActiveWindow.Text = txtActiveWindow.Text.Insert(mHadFocusAt, code);
                txtActiveWindow.Focus();
                txtActiveWindow.Select(mHadFocusAt + code.Length, 0);
            }
            if (mHadFocus == "es")
            {
                txtEntireScreen.Text = txtEntireScreen.Text.Insert(mHadFocusAt, code);
                txtEntireScreen.Focus();
                txtEntireScreen.Select(mHadFocusAt + code.Length, 0);
            }
        }

        private void txtActiveWindow_Leave(object sender, EventArgs e)
        {
            mHadFocusAt = txtActiveWindow.SelectionStart;
            mHadFocus = "aw";
        }

        private void txtEntireScreen_Leave(object sender, EventArgs e)
        {
            mHadFocusAt = txtEntireScreen.SelectionStart;
            mHadFocus = "es";
        }

        private void btnCodesT_Click(object sender, EventArgs e)
        {
            registerCode("%t");
        }

        private void btnCodesMo_Click(object sender, EventArgs e)
        {
            registerCode("%mo");
        }

        private void btnCodesD_Click(object sender, EventArgs e)
        {
            registerCode("%d");
        }

        private void btnCodesY_Click(object sender, EventArgs e)
        {
            registerCode("%y");
        }

        private void btnCodesH_Click(object sender, EventArgs e)
        {
            registerCode("%h");
        }

        private void btnCodesMi_Click(object sender, EventArgs e)
        {
            registerCode("%mi");
        }

        private void btnCodesS_Click(object sender, EventArgs e)
        {
            registerCode("%s");
        }

        private void btnCodesPm_Click(object sender, EventArgs e)
        {
            registerCode("%pm");
        }

        private void btnCodesI_Click(object sender, EventArgs e)
        {
            registerCode("%i");
        }

        private void bringUpMenu()
        {
            mMinimized = false;
            Show();
            WindowState = FormWindowState.Normal;
            this.Activate();
            Form.ActiveForm.BringToFront();
        }

        private void tsm_Click(object sender, EventArgs e)
        {
            mLastTab = tpFTP;

            string val = "tsmMain";

            switch (((ToolStripMenuItem)sender).Name)
            {
                case "tsmHotkeys":
                    val = "tpHotkeys";
                    break;
                case "tsmFTPSettings":
                    val = "tpFTP";
                    break;
                case "tsmImageSoftwareSettings":
                    val = "tpImageSoftware";
                    break;
                case "tsmFileSettings":
                    val = "tpFile";
                    break;
                case "tsmHistory":
                    val = "tpHistory";
                    break;
                case "tsmAdvanced":
                    val = "tpAdvanced";
                    break;
            }

            tabControl.SelectTab(val);

            bringUpMenu();

            tabControl.Focus();
        }

        private void setLastTab(object sender, EventArgs e)
        {
            mLastTab = (TabPage)sender;
        }

        private void cmbLanguage_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (mAllowUpdateUI && cmbLanguage.SelectedIndex != -1)
            {
                Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.CreateSpecificCulture(Program.langCode[cmbLanguage.SelectedIndex]);
                Thread.CurrentThread.CurrentUICulture = Thread.CurrentThread.CurrentCulture;

                Program.conf.Culture = Program.langCode[cmbLanguage.SelectedIndex];
                //Program.conf.Save();

                updateUI();

            }
        }

        private void updateUI()
        {
            //manual changes
            if (lbImageSoftware.Items.Count > 0) //It must contain one object
                lbImageSoftware.Items[0] = Properties.Resources.Disabled;

            if (tsmImageSoftware.DropDownItems.Count > 0) //It also should contain at least one object
            {
                tsmImageSoftware.DropDownItems[0].Text = Properties.Resources.Disabled;
            }

            //automatic changes

            recurseControls(Form.ActiveForm.Controls);

            foreach (ToolStripItem t in cmTray.Items)
            {
                if (t is ToolStripMenuItem)
                {
                    ToolStripMenuItem tm = (ToolStripMenuItem)t;

                    string txt = mResourceMan.GetString(tm.Name + ".Text");

                    if (txt != null)
                        tm.Text = txt;

                    foreach (ToolStripItem ti in tm.DropDownItems)
                    {
                        if (ti is ToolStripMenuItem)
                        {
                            ToolStripMenuItem tmi = (ToolStripMenuItem)ti;
                            txt = mResourceMan.GetString(tmi.Name + ".Text");

                            if (txt != null)
                                tmi.Text = txt;
                        }
                    }
                }
            }

            setPMvisible();
            setNamingConventions();

        }


        private void setPMvisible()
        {
            if (Properties.Resources.PMvisible)
            {
                lblCodePm.Visible = true;
                btnCodesPm.Visible = true;
            }
            else
            {
                lblCodePm.Visible = false;
                btnCodesPm.Visible = false;
            }
        }

        private void setNamingConventions()
        {
            Program.conf.activeWindow = Properties.Resources.activeWindowDefault;
            Program.conf.entireScreen = Properties.Resources.entireScreenDefault;

            //Program.conf.Save();

            txtActiveWindow.Text = Program.conf.activeWindow;
            txtEntireScreen.Text = Program.conf.entireScreen;
        }

        private void recurseControls(System.Windows.Forms.Control.ControlCollection cc)
        {
            foreach (Control c in cc)
            {
                changeText(c);
            }
        }

        private void changeText(Control c)
        {
            string txt = mResourceMan.GetString(c.Name + ".Text");

            object obj = null;

            if (txt != null)
                c.Text = txt;

            if ((obj = mResourceMan.GetObject(c.Name + ".Location")) != null)
                c.Location = (Point)obj;

            if ((obj = mResourceMan.GetObject(c.Name + ".Size")) != null)
                c.Size = (Size)obj;

            if ((obj = mResourceMan.GetObject(c.Name + ".Visible")) != null && c.Name != "pnlFirstRun") //keep displaying first run
                c.Visible = (bool)obj;

            if (c is ComboBox && (obj = mResourceMan.GetString(c.Name + ".Items")) != null)
            {
                ComboBox cb = (ComboBox)c;

                //Change first item
                cb.Items[0] = obj;

                int x = 0;

                while ((obj = mResourceMan.GetString(c.Name + ".Items" + ++x)) != null)
                {
                    if (x <= cb.Items.Count)
                        cb.Items[x] = obj;
                }
            }

            if (c.Controls.Count > 0)
            {
                recurseControls(c.Controls);
            }
        }

        private void btnAddAccount_Click(object sender, EventArgs e)
        {
            if (validateFTP())
            {
                FTPAccount acc = getFTPAccountFromFields();

                Program.conf.FTPAccountList.Add(acc);

                lbFTPAccounts.Items.Add(acc.Name);

                lbFTPAccounts.SelectedIndex = lbFTPAccounts.Items.Count - 1;
            }
            else
            {
                txtErrorFTP.Text = Resources.FTPnotUpdated; //change to FTP Account not added?
            }

        }

        private void btnCloneAccount_Click(object sender, EventArgs e)
        {
            FTPAccount tmp = new FTPAccount();

            int src = lbFTPAccounts.SelectedIndex;

            if (Program.conf.FTPAccountList != null && Program.conf.FTPAccountList.Count > 0 && lbFTPAccounts.SelectedIndices.Count == 1 && src != -1)
            {
                int len = Program.conf.FTPAccountList.Count;

                tmp = Program.conf.FTPAccountList[src];

                //mDoingNameUpdate = true;
                lbFTPAccounts.Items.Add(tmp.Name);

                txtServer.Text = tmp.Server;
                txtServerPort.Text = tmp.Port.ToString();
                txtUsername.Text = tmp.Username;
                txtPassword.Text = tmp.Password;
                txtPath.Text = tmp.Path;
                txtHttpPath.Text = tmp.HttpPath;
                rbFTPActive.Checked = tmp.IsActive;
                rbFTPPassive.Checked = !tmp.IsActive;

                Program.conf.FTPAccountList.Add(tmp);

                lbFTPAccounts.SelectedIndex = lbFTPAccounts.Items.Count - 1;
                txtName.Text = tmp.Name;
                txtName.Focus();
                txtName.SelectAll();
                //mDoingNameUpdate = false;




                //Program.conf.Save();

                //rewriteFTPRightClickMenu();
            }
        }

        private void addInNewFTPAccount()
        {
            FTPAccount acc = new FTPAccount();
            acc.Name = Resources.NewAccount;

            acc.Port = 21;
            acc.Path = "/";
            acc.HttpPath = "%/";

            txtServerPort.Text = acc.Port.ToString();
            txtPath.Text = acc.Path;
            txtHttpPath.Text = acc.HttpPath;

            txtServer.Text = "";
            txtUsername.Text = "";
            txtPassword.Text = "";

            Program.conf.FTPAccountList.Add(acc);

            lbFTPAccounts.Items.Add(Resources.NewAccount);

            lbFTPAccounts.SelectedIndex = lbFTPAccounts.Items.Count - 1;
            txtName.Text = Resources.NewAccount;
            txtName.Focus();
            txtName.SelectAll();

            rbFTPActive.Checked = false;
            rbFTPPassive.Checked = true;
        }

        private void btnDeleteFTP_Click(object sender, EventArgs e)
        {
            int sel = lbFTPAccounts.SelectedIndex;

            //   if (sel != -1 && sel != 0)
            if (sel != -1)
            {
                Program.conf.FTPAccountList.RemoveAt(sel);

                lbFTPAccounts.Items.RemoveAt(sel);

                //if the selected item is deleted change it back to default
                //if (Program.conf.FTPselected == sel)
                //{
                //    lbFTPAccounts.SelectedIndex = 0;
                //}
                //else
                //{
                //lbFTPAccounts.SelectedIndex = lbFTPAccounts.Items.Count - 1;
                //}
                if (lbFTPAccounts.Items.Count > 0)
                {
                    lbFTPAccounts.SelectedIndex = (sel > 0) ? (sel - 1) : 0;
                }
                else
                {
                    //No FTP Accounts
                    //btnAddAccount.PerformClick();

                    addInNewFTPAccount();
                }

                //Program.conf.FTPselected = lbFTPAccounts.SelectedIndex;

                //Program.conf.Save();

                //rewriteFTPRightClickMenu();
            }


        }

        private void lbFTPAccounts_SelectedIndexChanged(object sender, EventArgs e)
        {
            int sel = lbFTPAccounts.SelectedIndex;

            if (!mDoingNameUpdate && Program.conf.FTPAccountList != null && sel != -1 && sel < Program.conf.FTPAccountList.Count && Program.conf.FTPAccountList[sel] != null)
            {
                FTPAccount tmp = Program.conf.FTPAccountList[sel];

                txtName.Text = tmp.Name;
                txtServer.Text = tmp.Server;
                txtServerPort.Text = tmp.Port.ToString();
                txtUsername.Text = tmp.Username;
                txtPassword.Text = tmp.Password;
                txtPath.Text = tmp.Path;
                txtHttpPath.Text = tmp.HttpPath;
                rbFTPActive.Checked = tmp.IsActive;
                rbFTPPassive.Checked = !tmp.IsActive;

                Program.conf.FTPselected = lbFTPAccounts.SelectedIndex;

                rewriteFTPRightClickMenu();
            }
        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {
            /*
            if (lbFTPAccounts.SelectedIndex != -1)
            {
                mDoingNameUpdate = true;
                lbFTPAccounts.Items[lbFTPAccounts.SelectedIndex] = txtName.Text;
                mDoingNameUpdate = false;
            }
            */
        }

        private void pbLogo_Click(object sender, EventArgs e)
        {
            sShowHelp();
        }

        private void tsmHelp_Click(object sender, EventArgs e)
        {
            sShowHelp();
        }

        private void tsmLicense_Click(object sender, EventArgs e)
        {
            sShowLicense();
        }

        private void tsmVersionHistory_Click(object sender, EventArgs e)
        {
            sShowVersionHistory();
        }

        private void sShowAbout()
        {
            ZSS.Forms.AboutBox ab = new ZSS.Forms.AboutBox();
            ab.ShowDialog();
        }

        private void tsmAbout_Click(object sender, EventArgs e)
        {
            sShowAbout();
        }

        private void tsmDocumentation_Click(object sender, EventArgs e)
        {
            sShowHelp();
        }

        private void btnBrowseConfig_Click(object sender, EventArgs e)
        {
            Process.Start(Path.GetDirectoryName(Program.XMLSettingsFile));
        }

        private void tsmLic_Click(object sender, EventArgs e)
        {
            sShowLicense();
        }

        private void chkManualNaming_CheckedChanged(object sender, EventArgs e)
        {
            sUpdatePromptFileNameCheck();
        }


        private void sUpdatePromptFileNameCheck()
        {
            gbAutoFileName.Enabled = !chkManualNaming.Checked;
            gbCodeTitle.Enabled = !chkManualNaming.Checked;
            tsmPromptFileName.Checked = chkManualNaming.Checked;

            Program.conf.ManualNaming = chkManualNaming.Checked;
            //Program.conf.Save();
        }


        private void tsmPromptFileName_Click(object sender, EventArgs e)
        {
            chkManualNaming.Checked = !tsmPromptFileName.Checked;
        }

        private void ZScreen_Load(object sender, EventArgs e)
        {
            // nothing
        }

        private void sShowDebug()
        {
            if (File.Exists(FileSystem.DebugFilePath))
            {
                Process.Start(FileSystem.DebugFilePath);
            }
        }

        private void lbHistory_DoubleClick(object sender, EventArgs e)
        {
            if (lbHistory.SelectedIndex > -1)
            {
                Process.Start(((HistoryItem)lbHistory.SelectedItem).Url);
            }
        }


        private void tsmAboutMain_Click(object sender, EventArgs e)
        {
            sShowAbout();
        }

        private void cbStartWin_CheckedChanged(object sender, EventArgs e)
        {
            RegistryKey regkey = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run", true);

            if (cbStartWin.Checked)
            {
                regkey.SetValue(Application.ProductName, Application.ExecutablePath, RegistryValueKind.String);
            }
            else
            {
                regkey.DeleteValue(Application.ProductName, false);
            }

            Registry.CurrentUser.Flush();
        }

        private void sExportAccounts()
        {
            if (Program.conf.FTPAccountList != null)
            {
                SaveFileDialog dlg = new SaveFileDialog();
                dlg.FileName = string.Format("{0}-{1}-accounts", Application.ProductName, DateTime.Now.ToString("yyyyMMdd"));
                dlg.Filter = Program.FILTER_ACCOUNTS;
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    FileSystem.msWriteObjectToFileBF(Program.conf.FTPAccountList, dlg.FileName);
                }
            }
        }

        private void btnExportAccounts_Click(object sender, EventArgs e)
        {
            sExportAccounts();
        }

        private void btnAccsImport_Click(object sender, EventArgs e)
        {
            if (Program.conf.FTPAccountList == null)
                Program.conf.FTPAccountList = new List<FTPAccount>();

            List<FTPAccount> tmp = new List<FTPAccount>();
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = Program.FILTER_ACCOUNTS;
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                tmp = (List<FTPAccount>)FileSystem.mfReadObjectFromFileBF(dlg.FileName);
            }
            if (tmp != null)
            {
                Program.conf.FTPAccountList.AddRange(tmp);
                foreach (FTPAccount acc in tmp)
                {
                    lbFTPAccounts.Items.Add(acc.Name);
                }
            }
        }

        private void nudFlashIconCount_ValueChanged(object sender, EventArgs e)
        {
            Program.conf.FlashTrayCount = nudFlashIconCount.Value;
        }

        private void nudCacheSize_ValueChanged(object sender, System.EventArgs e)
        {
            Program.conf.ScreenshotCacheSize = nudCacheSize.Value;
        }

        private void txtCacheDir_TextChanged(object sender, System.EventArgs e)
        {
            Program.conf.CacheDir = txtCacheDir.Text;
        }

        private void btnSettingsExport_Click(object sender, EventArgs e)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Filter = Program.FILTER_SETTINGS;

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                Program.conf.Save(dlg.FileName);
            }
        }

        private void btnSettingsImport_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = Program.FILTER_SETTINGS;
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                XMLSettings temp = XMLSettings.Read(dlg.FileName);
                if (temp.RunOnce == true)
                {
                    Program.conf = temp;
                    setupScreen();
                }
            }
        }



        private void sAddImageSoftwareToList()
        {
            if (!string.IsNullOrEmpty(txtImageSoftwareName.Text))
            {
                if (Program.GetImageSoftware(txtImageSoftwareName.Text) == null)
                {
                    if (!string.IsNullOrEmpty(txtImageSoftwarePath.Text))
                    {
                        ImageSoftware temp = new ImageSoftware();
                        temp.Name = txtImageSoftwareName.Text;
                        temp.Path = txtImageSoftwarePath.Text;
                        Program.conf.ImageSoftwareList.Add(temp);
                        lbImageSoftware.Items.Add(temp.Name);

                        lbImageSoftware.SelectedItem = temp.Name;

                        //rewriteISRightClickMenu();
                    }
                }
            }
        }

        private void btnAddImageSoftware_Click(object sender, EventArgs e)
        {
            sAddImageSoftwareToList();
        }

        private void btnDeleteImageSoftware_Click(object sender, EventArgs e)
        {
            int wasActiveSetLower = -1;

            if (lbImageSoftware.SelectedIndex > 0)
            {
                //If Active then set one step lower in the list
                if (Program.conf.ImageSoftwareActive.Name == lbImageSoftware.SelectedItem.ToString())
                {
                    wasActiveSetLower = lbImageSoftware.SelectedIndex - 1;
                }

                ImageSoftware temp = Program.GetImageSoftware(lbImageSoftware.SelectedItem.ToString());
                if (temp != null)
                {
                    Program.conf.ImageSoftwareList.Remove(temp);
                    lbImageSoftware.Items.Remove(lbImageSoftware.SelectedItem.ToString());
                }

                if (wasActiveSetLower != -1)
                {
                    lbImageSoftware.SelectedIndex = wasActiveSetLower;
                }

                rewriteISRightClickMenu();
            }
        }

        private void chkEnableThumbnail_CheckedChanged(object sender, EventArgs e)
        {
            Program.conf.EnableThumbnail = chkEnableThumbnail.Checked;
        }

        private void cboScreenshotDest_SelectedIndexChanged(object sender, EventArgs e)
        {
            ScreenshotDestType sdt = (ScreenshotDestType)cboScreenshotDest.SelectedIndex;
            Program.conf.ScreenshotDestMode = sdt;

            switch (sdt)
            {
                case ScreenshotDestType.CLIPBOARD:
                    checkSendToMenu(tsmDestClipboard);
                    break;
                case ScreenshotDestType.FILE:
                    checkSendToMenu(tsmDestFile);
                    break;
                case ScreenshotDestType.FTP:
                    checkSendToMenu(tsmDestFTP);
                    break;
                case ScreenshotDestType.IMAGESHACK:
                    checkSendToMenu(tsmDestImageShack);
                    break;
            }
        }

        private void checkSendToMenu(ToolStripMenuItem item)
        {
            checkToolStripMenuItem(tsmSendTo, item);
        }

        private void checkToolStripMenuItem(ToolStripMenuItem parent, ToolStripMenuItem item)
        {
            foreach (ToolStripMenuItem tsmi in parent.DropDownItems)
            {
                if (tsmi == item)
                    tsmi.Checked = true;
                else
                    tsmi.Checked = false;
            }

            tsmCbCopy.Enabled = cboScreenshotDest.SelectedIndex != (int)ScreenshotDestType.CLIPBOARD &&
                                cboScreenshotDest.SelectedIndex != (int)ScreenshotDestType.FILE;
        }

        private void tsmDestClipboard_Click(object sender, EventArgs e)
        {
            cboScreenshotDest.SelectedIndex = (int)ScreenshotDestType.CLIPBOARD;
        }

        private void tsmDestFile_Click(object sender, EventArgs e)
        {
            cboScreenshotDest.SelectedIndex = (int)ScreenshotDestType.FILE;
        }

        private void tsmDestFTP_Click(object sender, EventArgs e)
        {
            cboScreenshotDest.SelectedIndex = (int)ScreenshotDestType.FTP;
        }

        private void tsmDestImageShack_Click(object sender, EventArgs e)
        {
            cboScreenshotDest.SelectedIndex = (int)ScreenshotDestType.IMAGESHACK;
        }

        private void sSetActiveImageSoftware()
        {
            int sel;

            if ((sel = lbImageSoftware.SelectedIndex) > 0)
            {
                Program.conf.ISenabled = true;

                Program.conf.ImageSoftwareActive = Program.conf.ImageSoftwareList[sel - 1];
                rewriteISRightClickMenu();
                //checkCorrectISRightClickMenu(Program.conf.ImageSoftwareActive.Name);
                //cbRunImageSoftware.Checked = true;

            }
        }

        private void lbImageSoftware_SelectedIndexChanged(object sender, EventArgs e)
        {
            int sel = lbImageSoftware.SelectedIndex;

            bool b = sel > 0;

            if (sel == 0)
            {
                txtImageSoftwareName.Text = "";
                txtImageSoftwarePath.Text = "";

                Program.conf.ISenabled = false;
                rewriteISRightClickMenu();
            }
            else if (b)
            {
                ImageSoftware temp = Program.GetImageSoftware(lbImageSoftware.SelectedItem.ToString());
                if (temp != null)
                {
                    txtImageSoftwareName.Text = temp.Name;
                    txtImageSoftwarePath.Text = temp.Path;
                }

                sSetActiveImageSoftware();
            }

            btnUpdateImageSoftware.Enabled = b;
            btnDeleteImageSoftware.Enabled = b;
        }

        private void cboClipboardTextMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            Program.conf.ClipboardUriMode = (ClipboardUriType)cboClipboardTextMode.SelectedIndex;
            updateClipboardTextTrayMenu();
        }

        private void tsmCbAll_Click(object sender, EventArgs e)
        {
            tsmCbAll.Checked = !tsmCbAll.Checked;
            Program.conf.ClipboardUriMode = ClipboardUriType.ALL;
            cboClipboardTextMode.SelectedIndex = (int)Program.conf.ClipboardUriMode;
        }

        private void tsmCbFullImage_Click(object sender, EventArgs e)
        {
            tsmCbFullImage.Checked = !tsmCbFullImage.Checked;
            Program.conf.ClipboardUriMode = ClipboardUriType.FULL;
            cboClipboardTextMode.SelectedIndex = (int)Program.conf.ClipboardUriMode;
        }

        private void tsmCbThumb_Click(object sender, EventArgs e)
        {
            tsmCbThumb.Checked = !tsmCbThumb.Checked;
            Program.conf.ClipboardUriMode = ClipboardUriType.THUMBNAIL_FORUMS1;
            cboClipboardTextMode.SelectedIndex = (int)Program.conf.ClipboardUriMode;
        }
        
        private void txtFileDirectory_TextChanged(object sender, EventArgs e)
        {
            Program.conf.path = txtFileDirectory.Text;
        }

        private void cbDeleteLocal_CheckedChanged(object sender, EventArgs e)
        {
            Program.conf.DeleteLocal = cbDeleteLocal.Checked;
        }

        private void txtActiveWindow_TextChanged(object sender, EventArgs e)
        {
            Program.conf.activeWindow = txtActiveWindow.Text;
        }

        private void txtEntireScreen_TextChanged(object sender, EventArgs e)
        {
            Program.conf.entireScreen = txtEntireScreen.Text;
        }

        private void cmbFileFormat_SelectedIndexChanged(object sender, EventArgs e)
        {
            Program.conf.FileFormat = cmbFileFormat.SelectedIndex;
        }

        private void txtImageQuality_ValueChanged(object sender, EventArgs e)
        {
            long quality = 100L;

            try { quality = long.Parse(txtImageQuality.Text); }
            catch { }

            Program.conf.ImageQuality = quality;
        }

        private void txtSwitchAfter_TextChanged(object sender, EventArgs e)
        {
            int switchAfter = 350;

            try { switchAfter = Int32.Parse(txtSwitchAfter.Text); }
            catch { }

            Program.conf.SwitchAfter = switchAfter;
        }

        private void cmbSwitchFormat_SelectedIndexChanged(object sender, EventArgs e)
        {
            Program.conf.SwitchFormat = cmbSwitchFormat.SelectedIndex;
        }
    }
}
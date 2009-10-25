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
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using UploadersLib;
using UploadersLib.Helpers;
using UploadersLib.TextServices;
using ZScreenLib.Properties;

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
        public ImageFileManager ImageManager { get; set; }

        #endregion

        public XMLSettings Settings { get; set; }

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

        public void SetFilePathFromPattern(string fileName)
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

            UpdateLocalFilePath(filePath);
        }

        public void UpdateLocalFilePath(string fp)
        {
            this.LocalFilePath = fp;
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

        #region Functions

        /// <summary>
        /// Function to Capture Active Window
        /// </summary>
        public void CaptureActiveWindow()
        {
            if (this.MyImage == null)
            {
                IntPtr handle = User32.GetWindowHandle();
                Rectangle windowRect = User32.GetWindowRectangle(handle);
                Form form = null;

                Bitmap windowImage = null;

                if (Engine.conf.SelectedWindowCleanBackground)
                {
                    // create form behind the window to remove the dirty Aero background
                    form = new Form();

                    form.BackColor = Color.Black;
                    form.FormBorderStyle = FormBorderStyle.None;
                    form.Show();
                    activateWindow(handle);
                    form.Refresh();
                    User32.SetWindowPos(form.Handle, handle, windowRect.X, windowRect.Y, windowRect.Width, windowRect.Height, 0);
                    Application.DoEvents();

                    // capture the window with a black background
                    Bitmap blackBGImage = User32.CaptureWindow(handle, Engine.conf.ShowCursor) as Bitmap;
                    //blackBGImage.Save(@"c:\users\nicolas\documents\blackBGImage.png");
                   
                    form.BackColor = Color.White;
                    form.Refresh();
                    activateWindow(handle);
                    Application.DoEvents();

                    // capture the window again with a white background this time
                    Bitmap whiteBGImage = User32.CaptureWindow(handle, Engine.conf.ShowCursor) as Bitmap;
                    //whiteBGImage.Save(@"c:\users\nicolas\documents\whiteBGImage.png");

                    // compute the real window image by difference between the two previous images
                    windowImage = computeOriginal(whiteBGImage, blackBGImage);
                }
                else
                {
                    windowImage = User32.CaptureWindow(handle, Engine.conf.ShowCursor) as Bitmap;
                }

                if (Engine.conf.SelectedWindowCleanTransparentCorners)
                {
                    if (form == null)
                    {
                        form = new Form();
                        form.BackColor = Color.White;
                        form.FormBorderStyle = FormBorderStyle.None;
                        form.Show();
                        User32.ActivateWindow(handle);
                    }

                    // paints red corners behind the form, so that they can be recognized and removed
                    form.Paint += new PaintEventHandler(formPaintRedCorners);
                    form.Refresh();
                    User32.SetWindowPos(form.Handle, handle, windowRect.X, windowRect.Y, windowRect.Width, windowRect.Height, 0);
                    Application.DoEvents();
                    Bitmap redCornersImage = User32.CaptureWindow(handle, false) as Bitmap;

                    Image result = new Bitmap(windowImage.Width, windowImage.Height, PixelFormat.Format32bppArgb);
                    Graphics g = Graphics.FromImage(result);
                    g.Clear(Color.Transparent);

                    // remove the transparent pixels in the four corners
                    RemoveCorner(redCornersImage, g, 0, 0, 5, Corner.TopLeft);
                    RemoveCorner(redCornersImage, g, windowImage.Width - 5, 0, windowImage.Width, Corner.TopRight);
                    RemoveCorner(redCornersImage, g, 0, windowImage.Height - 5, 5, Corner.BottomLeft);
                    RemoveCorner(redCornersImage, g, windowImage.Width - 5, windowImage.Height - 5, windowImage.Width, Corner.BottomRight);
                    g.DrawImage(windowImage, 0, 0);

                    g.Dispose();
                    this.SetImage(result);
                }
                else
                {
                    this.SetImage(windowImage);
                }

                if (form != null)
                {
                    form.Close();
                }
            }
        }

        private static void activateWindow(IntPtr handle)
        {
            User32.ActivateWindow(handle);
            int count = 500;
            while (User32.GetForegroundWindow() != handle && count > 0)
            {
                Thread.Sleep(10);
                count--;
            }
        }

        /// <summary>
        /// compute the original window image from the difference between the two given images
        /// </summary>
        /// <param name="whiteBGImage">the window with a white background</param>
        /// <param name="blackBGImage">the window with a black background</param>
        /// <returns>the original window image, with restored alpha channel</returns>
        private Bitmap computeOriginal(Bitmap whiteBGImage, Bitmap blackBGImage)
        {
            int width = whiteBGImage.Size.Width;
            int height = whiteBGImage.Size.Height;

            Bitmap resultImage = new Bitmap(width, height, PixelFormat.Format32bppArgb);

            var rect = new Rectangle(new Point(0, 0), blackBGImage.Size);

            // access the image data directly for faster image processing
            BitmapData blackImageData = blackBGImage.LockBits(rect, ImageLockMode.ReadOnly,
                PixelFormat.Format32bppArgb);
            BitmapData whiteImageData = whiteBGImage.LockBits(rect, ImageLockMode.ReadOnly,
                PixelFormat.Format32bppArgb);
            BitmapData resultImageData = resultImage.LockBits(rect, ImageLockMode.ReadOnly,
                PixelFormat.Format32bppArgb);

            IntPtr pBlackImage = blackImageData.Scan0;
            IntPtr pWhiteImage = whiteImageData.Scan0;
            IntPtr pResultImage = resultImageData.Scan0;

            int bytes = blackImageData.Stride * blackImageData.Height;
            byte[] blackBGImageRGB = new byte[bytes];
            byte[] whiteBGImageRGB = new byte[bytes];
            byte[] resultImageRGB = new byte[bytes];

            Marshal.Copy(pBlackImage, blackBGImageRGB, 0, bytes);
            Marshal.Copy(pWhiteImage, whiteBGImageRGB, 0, bytes);

            int offset = 0;
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    // ARGB is in fact BGRA (little endian)
                    int r0 = blackBGImageRGB[offset + 2];
                    int g0 = blackBGImageRGB[offset + 1];
                    int b0 = blackBGImageRGB[offset + 0];
                    int r1 = whiteBGImageRGB[offset + 2];
                    int alpha = r0 - r1 + 255;

                    int resultR, resultG, resultB;
                    if (alpha != 0)
                    {
                        resultR = r0 * 255 / alpha;
                        resultG = g0 * 255 / alpha;
                        resultB = b0 * 255 / alpha;
                    }
                    else
                    {
                        resultR = 0;
                        resultG = 0;
                        resultB = 0;
                    }

                    resultImageRGB[offset + 3] = (byte)alpha;
                    resultImageRGB[offset + 2] = (byte)resultR;
                    resultImageRGB[offset + 1] = (byte)resultG;
                    resultImageRGB[offset + 0] = (byte)resultB;

                    offset += 4;
                }
            }

            Marshal.Copy(resultImageRGB, 0, pResultImage, bytes);

            blackBGImage.UnlockBits(blackImageData);
            whiteBGImage.UnlockBits(whiteImageData);
            resultImage.UnlockBits(resultImageData);

            return resultImage;
        }

        #region Clean window corners

        /// <summary>
        /// Paints 5 pixel wide red corners behind the form from which the event originates.
        /// </summary>
        void formPaintRedCorners(object sender, PaintEventArgs e)
        {
            const int cornerSize = 5;
            int width = (sender as Form).Width;
            int height = (sender as Form).Height;
            e.Graphics.FillRectangle(Brushes.Red, 0, 0, cornerSize, cornerSize);
            e.Graphics.FillRectangle(Brushes.Red, width - 5, 0, cornerSize, cornerSize);
            e.Graphics.FillRectangle(Brushes.Red, 0, height - 5, cornerSize, cornerSize);
            e.Graphics.FillRectangle(Brushes.Red, width - 5, height - 5, cornerSize, cornerSize);
        }

        enum Corner { TopLeft, TopRight, BottomLeft, BottomRight };

        /// <summary>
        /// Removes a corner from the clipping region of the given graphics object.
        /// </summary>
        /// <param name="bmp">The bitmap with the form corners masked in red</param>
        private void RemoveCorner(Bitmap bmp, Graphics g, int minx, int miny, int maxx, Corner corner)
        {
            int[] shape;
            if (corner == Corner.TopLeft || corner == Corner.TopRight)
            {
                shape = new int[5] { 5, 3, 2, 1, 1 };
            }
            else
            {
                shape = new int[5] { 1, 1, 2, 3, 5 };
            }

            int maxy = miny + 5;
            if (corner == Corner.TopLeft || corner == Corner.BottomLeft)
            {
                for (int y = miny; y < maxy; y++)
                {
                    for (int x = minx; x < minx + shape[y - miny]; x++)
                    {
                        RemoveCornerPixel(bmp, g, y, x);
                    }
                }
            }
            else
            {
                for (int y = miny; y < maxy; y++)
                {
                    for (int x = maxx - 1; x >= maxx - shape[y - miny]; x--)
                    {
                        RemoveCornerPixel(bmp, g, y, x);
                    }
                }
            }
        }

        /// <summary>
        /// Removes a pixel from the clipping region of the given graphics object, if
        /// the bitmap is red at the coordinates of the pixel.
        /// </summary>
        /// <param name="bmp">The bitmap with the form corners masked in red</param>
        private static void RemoveCornerPixel(Bitmap bmp, Graphics g, int y, int x)
        {
            var color = bmp.GetPixel(x, y);
            // detect a shade of red (the color is darker because of the window's shadow)
            if (color.R > 64 && color.G == 0 && color.B == 0)
            {
                Region region = new Region(new Rectangle(x, y, 1, 1));
                g.SetClip(region, System.Drawing.Drawing2D.CombineMode.Exclude);
            }
        }
        #endregion

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

        public string ToErrorString()
        {
            return string.Join("\r\n", Errors.ToArray());
        }
    }
}
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

using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using Crop;
using GraphicsMgrLib;
using ZScreenLib.Shapes;

namespace ZScreenLib
{
    public class TaskManager
    {
        public static bool mSetHotkeys, mTakingScreenShot, bAutoScreenshotsOpened, bDropWindowOpened, bQuickActionsOpened, bQuickOptionsOpened;
        private WorkerTask mTask;

        public TaskManager(WorkerTask task)
        {
            mTask = task;
        }

        #region Image Tasks Manager

        public void CaptureActiveWindow()
        {
            try
            {
                mTask.CaptureActiveWindow();
                mTask.WriteImage();
                mTask.PublishData();
            }
            catch (ArgumentOutOfRangeException aor)
            {
                mTask.Errors.Add("Invalid FTP Account Selection");
                Engine.MyLogger.WriteLine(aor.ToString());
            }
            catch (Exception ex)
            {
                Engine.MyLogger.WriteException(ex, "Error while capturing active window");
                if (Engine.conf.CaptureEntireScreenOnError)
                {
                    CaptureRegionOrWindow();
                }
            }
        }

        public string CaptureRegionOrWindow()
        {
            mTakingScreenShot = true;
            string filePath = string.Empty;

            bool windowMode = mTask.Job2 == WorkerTask.JobLevel2.TakeScreenshotWindowSelected;

            try
            {
                using (Image imgSS = Capture.CaptureScreen(Engine.conf.ShowCursor))
                {
                    if (mTask.Job2 == WorkerTask.JobLevel2.TAKE_SCREENSHOT_LAST_CROPPED && !Engine.LastRegion.IsEmpty)
                    {
                        mTask.SetImage(GraphicsMgr.CropImage(imgSS, Engine.LastRegion));
                    }
                    else
                    {
                        if (Engine.conf.UseCropBeta && !windowMode)
                        {
                            using (Crop2 crop = new Crop2(imgSS))
                            {
                                if (crop.ShowDialog() == DialogResult.OK)
                                {
                                    mTask.SetImage(crop.GetCroppedScreenshot());
                                }
                            }
                        }
                        else if (Engine.conf.UseCropLight && !windowMode)
                        {
                            using (CropLight crop = new CropLight(imgSS))
                            {
                                if (crop.ShowDialog() == DialogResult.OK)
                                {
                                    mTask.SetImage(GraphicsMgr.CropImage(imgSS, crop.SelectionRectangle));
                                }
                            }
                        }
                        else
                        {
                            using (Crop c = new Crop(imgSS, windowMode))
                            {
                                if (c.ShowDialog() == DialogResult.OK)
                                {
                                    if (mTask.Job2 == WorkerTask.JobLevel2.TakeScreenshotCropped && !Engine.LastRegion.IsEmpty)
                                    {
                                        mTask.SetImage(GraphicsMgr.CropImage(imgSS, Engine.LastRegion));
                                    }
                                    else if (windowMode && !Engine.LastCapture.IsEmpty)
                                    {
                                        mTask.SetImage(GraphicsMgr.CropImage(imgSS, Engine.LastCapture));
                                    }
                                }
                            }
                        }
                    }
                }

                mTakingScreenShot = false;

                if (mTask.MyImage != null)
                {
                    bool roundedShadowCorners = false;
                    if (windowMode && Engine.conf.SelectedWindowRoundedCorners || !windowMode && Engine.conf.CropShotRoundedCorners)
                    {
                        mTask.SetImage(GraphicsMgr.RemoveCorners(mTask.MyImage, null));
                        roundedShadowCorners = true;
                    }
                    if (windowMode && Engine.conf.SelectedWindowShadow || !windowMode && Engine.conf.CropShotShadow)
                    {
                        mTask.SetImage(GraphicsMgr.AddBorderShadow(mTask.MyImage, roundedShadowCorners));
                    }

                    mTask.WriteImage();
                    mTask.PublishData();
                }
            }
            catch (Exception ex)
            {
                Engine.MyLogger.WriteException(ex, "Error while capturing region");
                mTask.Errors.Add(ex.Message);
                if (Engine.conf.CaptureEntireScreenOnError)
                {
                    CaptureScreen();
                }
            }
            finally
            {
                mTask.MyWorker.ReportProgress((int)WorkerTask.ProgressType.UpdateCropMode);
                mTakingScreenShot = false;
            }

            return filePath;
        }

        public void CaptureScreen()
        {
            mTask.CaptureScreen();
            mTask.WriteImage();
            mTask.PublishData();
        }

        public void CaptureFreehandCrop()
        {
            using (FreehandCapture crop = new FreehandCapture())
            {
                if (crop.ShowDialog() == DialogResult.OK)
                {
                    using (Image ss = Capture.CaptureScreen(false))
                    {
                        mTask.SetImage(crop.GetScreenshot(ss));
                    }
                }
                else
                {
                    mTask.Status = WorkerTask.TaskStatus.RetryPending;
                }
            }

            if (mTask.MyImage != null)
            {
                mTask.WriteImage();
                mTask.PublishData();
            }
        }

        #endregion Image Tasks Manager

        public void TextEdit()
        {
            if (File.Exists(mTask.LocalFilePath))
            {
                Process p = new Process();
                ProcessStartInfo psi = new ProcessStartInfo(Engine.conf.TextEditorActive.Path)
                {
                    Arguments = string.Format("{0}{1}{0}", "\"", mTask.LocalFilePath)
                };
                p.StartInfo = psi;
                p.Start();
                // Wait till user quits the ScreenshotEditApp
                p.WaitForExit();
            }
        }

        public override string ToString()
        {
            StringBuilder sbDebug = new StringBuilder();
            sbDebug.AppendLine(string.Format("Image Uploader: {0", mTask.MyImageUploader));
            return sbDebug.ToString();
        }
    }
}
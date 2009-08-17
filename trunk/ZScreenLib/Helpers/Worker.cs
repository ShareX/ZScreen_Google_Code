using System;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;

namespace ZScreenLib
{
   public class Worker
    {
       internal bool mSetHotkeys, mTakingScreenShot, bAutoScreenshotsOpened, bDropWindowOpened, bQuickActionsOpened, bQuickOptionsOpened;

       public string CaptureRegionOrWindow(ref WorkerTask task)
       {
           mTakingScreenShot = true;
           string filePath = "";

           try
           {
               using (Image imgSS = User32.CaptureScreen(Program.conf.ShowCursor))
               {
                   if (task.Job == WorkerTask.Jobs.TAKE_SCREENSHOT_LAST_CROPPED && !Program.LastRegion.IsEmpty)
                   {
                       task.SetImage(GraphicsMgr.CropImage(imgSS, Program.LastRegion));
                   }
                   else
                   {
                       using (Crop c = new Crop(imgSS, task.Job == WorkerTask.Jobs.TakeScreenshotWindowSelected))
                       {
                           if (c.ShowDialog() == DialogResult.OK)
                           {
                               if (task.Job == WorkerTask.Jobs.TakeScreenshotCropped && !Program.LastRegion.IsEmpty)
                               {
                                   task.SetImage(GraphicsMgr.CropImage(imgSS, Program.LastRegion));
                               }
                               else if (task.Job == WorkerTask.Jobs.TakeScreenshotWindowSelected && !Program.LastCapture.IsEmpty)
                               {
                                   task.SetImage(GraphicsMgr.CropImage(imgSS, Program.LastCapture));
                               }
                           }
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
               task.MyWorker.ReportProgress((int)WorkerTask.ProgressType.UpdateCropMode);
               mTakingScreenShot = false;
           }

           return filePath;
       }

       /// <summary>
       /// Method to Capture Screen
       /// </summary>
       /// <param name="task"></param>
       public void CaptureScreen(ref WorkerTask task)
       {
           task.CaptureScreen();
           WriteImage(task);
           PublishImage(ref task);
       }

       /// <summary>
       /// Method to Write Image to File
       /// </summary>
       /// <param name="t"></param>
       public void WriteImage(WorkerTask t)
       {
           if (t.MyImage != null)
           {
               NameParserType type;
               if (t.Job == WorkerTask.Jobs.TAKE_SCREENSHOT_WINDOW_ACTIVE)
               {
                   type = NameParserType.ActiveWindow;
               }
               else
               {
                   type = NameParserType.EntireScreen;
               }

               string filePath = FileSystem.GetFilePath(NameParser.Convert(type), Program.conf.ManualNaming);

               t.SetLocalFilePath(FileSystem.SaveImage(t.MyImage, filePath));
           }
       }

       /// <summary>
       /// Function to edit Image (Screenshot or Picture) in an Image Editor and Upload
       /// </summary>
       /// <param name="task"></param>
       private void PublishImage(ref WorkerTask task)
       {
           TaskManager tm = new TaskManager(ref task);

           if (task.MyImage != null && Adapter.ImageSoftwareEnabled() && task.Job != WorkerTask.Jobs.UPLOAD_IMAGE)
           {
               tm.ImageEdit();
           }

           if (task.SafeToUpload())
           {
               FileSystem.AppendDebug("File for HDD: " + task.LocalFilePath);
               tm.UploadImage();
           }
       }

    }
}

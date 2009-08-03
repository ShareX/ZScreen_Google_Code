using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZScreenLib.Global;
using System.Drawing;
using System.Windows.Forms;
using ZScreenLib.Forms;

namespace ZScreenLib.Helpers
{
   public class Worker
    {

       internal bool mSetHotkeys, mTakingScreenShot, bAutoScreenshotsOpened, bDropWindowOpened, bQuickActionsOpened, bQuickOptionsOpened;

       public string CaptureRegionOrWindow(ref MainAppTask task)
       {
           mTakingScreenShot = true;
           string filePath = "";

           try
           {
               using (Image imgSS = User32.CaptureScreen(Program.conf.ShowCursor))
               {
                   if (task.Job == MainAppTask.Jobs.TAKE_SCREENSHOT_LAST_CROPPED && !Program.LastRegion.IsEmpty)
                   {
                       task.SetImage(GraphicsMgr.CropImage(imgSS, Program.LastRegion));
                   }
                   else
                   {
                       using (Crop c = new Crop(imgSS, task.Job == MainAppTask.Jobs.TAKE_SCREENSHOT_WINDOW_SELECTED))
                       {
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
           }

           return filePath;
       }

       /// <summary>
       /// Method to Capture Screen
       /// </summary>
       /// <param name="task"></param>
       public void CaptureScreen(ref MainAppTask task)
       {
           task.CaptureScreen();
           WriteImage(task);
           PublishImage(ref task);
       }

       /// <summary>
       /// Method to Write Image to File
       /// </summary>
       /// <param name="t"></param>
       public void WriteImage(MainAppTask t)
       {
           if (t.MyImage != null)
           {
               NameParserType type;
               if (t.Job == MainAppTask.Jobs.TAKE_SCREENSHOT_WINDOW_ACTIVE)
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

    }
}

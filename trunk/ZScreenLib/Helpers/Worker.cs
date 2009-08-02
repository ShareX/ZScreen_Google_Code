using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZScreenLib.Global;

namespace ZScreenLib.Helpers
{
   public class Worker
    {
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

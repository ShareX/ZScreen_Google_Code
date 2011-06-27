using System;
using System.Collections.Generic;
using System.IO;
using HelpersLib;
using NDesk.Options;
using UploadersLib;
using UploadersLib.HelperClasses;
using ZScreenLib;

namespace ZScreenCLI
{
    public class Program
    {
        private static void Main(string[] args)
        {
            bool bShowHelp = false;
            bool bFileUpload = false;
            bool bClipboardUpload = false;
            bool bCropShot = false;
            bool bSelectedWindow = false;
            bool bScreen = false;

            List<int> listOutputTypes = new List<int>();
            int clipboardContent = (int)ClipboardContentEnum.Remote;
            List<int> listImageHosts = new List<int>();
            List<int> listTextHosts = new List<int>();
            List<int> listFileHosts = new List<int>();
            List<string> listPaths = new List<string>();

            Engine.MyUploadersConfig = UploadersConfig.Load(@"C:\Users\Mike\AppData\Roaming\ZScreen\Settings\UploadersConfig.xml");

            var p = new OptionSet()
            {
                { "o|outputs=", "Outputs\n" + "this must be an integer.",
                    (int v) => listImageHosts.Add(v) },
                { "ih|image_host=", "Image uploader type.\n" + "this must be an integer.",
                    (int v) => listImageHosts.Add(v) },
                { "th|text_host", "Text uploader type",
                    (int v) => listTextHosts.Add(v) },
                { "fh|file_host", "File uploader type",
                    (int v) => listFileHosts.Add(v) },
                { "h|help",  "show this message and exit",
                    v => bShowHelp = v != null },
                { "cu|clipboard_upload",  "Upload clipboard content",
                    v => bClipboardUpload = v != null },
                { "sw|selected_window",  "Capture selected window",
                    v => bSelectedWindow = v != null },
                { "cs|crop_shot",  "Capture rectangular region",
                    v => bCropShot = v != null },
                { "s|screen",  "Capture entire screen",
                    v => bScreen = v != null },
                { "u|upload=", "File path of the file to upload.",
                    v => {
                            if (File.Exists(v)) listPaths.Add (v);
                            else if (Directory.Exists(v)) listPaths.Add(v);
                            if (listPaths.Count>0) bFileUpload = true;
                         }
                },
            };

            List<string> extra;
            try
            {
                extra = p.Parse(args);
            }
            catch (OptionException e)
            {
                Console.Write(string.Format("{0}: ", "ZScreenCLI"));
                Console.WriteLine(e.Message);
                Console.WriteLine("Try 'zscreencli.exe --help' for more information.");
                return;
            }

            if (listOutputTypes.Count == 0)
            {
                // if user forgets to list output then copy to clipboard
                listOutputTypes.Add((int)OutputEnum.Clipboard);
            }

            if (bShowHelp)
            {
                ShowHelp(p);
                return;
            }

            if (bFileUpload && listPaths.Count > 0)
            {
                List<string> listFiles = new List<string>();
                foreach (string fdp in listPaths)
                {
                    if (File.Exists(fdp))
                    {
                        listFiles.Add(fdp);
                    }
                    else if (Directory.Exists(fdp))
                    {
                        listFiles.AddRange(Directory.GetFiles(fdp, "*.*", SearchOption.AllDirectories));
                    }
                }
                foreach (string fp in listFiles)
                {
                    WorkerTask fuTask = new WorkerTask();
                    fuTask.MyOutputs.Add(OutputEnum.Clipboard);
                    fuTask.MyClipboardContent.Add((ClipboardContentEnum)clipboardContent);
                    fuTask.MyImageUploaders.Add(ImageUploaderType.IMAGESHACK);
                    fuTask.AssignJob(WorkerTask.JobLevel2.UploadFromClipboard);
                    fuTask.UpdateLocalFilePath(fp);
                    fuTask.PublishData();
                    foreach (UploadResult ur in fuTask.UploadResults)
                    {
                        Console.WriteLine(ur.URL);
                    }
                    Console.ReadLine();
                }
            }

            string message;
            if (extra.Count > 0)
            {
                message = string.Join(" ", extra.ToArray());
            }
            else
            {
                message = "Hello {0}!";
            }

            foreach (string name in listPaths)
            {
                for (int i = 0; i < listImageHosts.Count; ++i)
                    Console.WriteLine(message, name);
            }
        }

        private static void ShowHelp(OptionSet p)
        {
            Console.WriteLine("Usage: greet [OPTIONS]+ message");
            Console.WriteLine("Greet a list of individuals with an optional message.");
            Console.WriteLine("If no message is specified, a generic greeting is used.");
            Console.WriteLine();
            Console.WriteLine("Options:");
            p.WriteOptionDescriptions(Console.Out);
            Console.WriteLine();
            Console.WriteLine("Image hosts:\n");
            foreach (ImageUploaderType ut in Enum.GetValues(typeof(ImageUploaderType)))
            {
                Console.WriteLine(string.Format("{0}: {1}", (int)ut, ut.GetDescription()));
            }
        }
    }
}
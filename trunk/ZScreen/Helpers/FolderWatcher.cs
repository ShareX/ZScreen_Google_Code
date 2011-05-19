using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Threading;
using ZScreenLib;

namespace ZScreenGUI
{
    public class FolderWatcher
    {
        private FileSystemWatcher watcher = new FileSystemWatcher();

        public string FolderPath { get; set; }

        public FolderWatcher(ISynchronizeInvoke synchronizingObject)
        {
            this.watcher.SynchronizingObject = synchronizingObject;
            this.watcher.Created += new FileSystemEventHandler(Watcher_Created);
        }

        public FolderWatcher(string p, ISynchronizeInvoke synchronizingObject)
            : this(synchronizingObject)
        {
            this.FolderPath = p;
            this.watcher = new FileSystemWatcher(p);
        }

        public void StartWatching()
        {
            if (Directory.Exists(FolderPath))
            {
                watcher.Path = this.FolderPath;
                this.watcher.EnableRaisingEvents = true;
            }
        }

        public void StopWatching()
        {
            this.watcher.EnableRaisingEvents = false;
        }

        [STAThread]
        private void Watcher_Created(object sender, FileSystemEventArgs e)
        {
            string filePath = e.FullPath;
            int retry = 5;
            while (retry > 0)
            {
                try
                {
                    using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                    {
                        // check if the file is complete
                    }
                    Engine.MyLogger.WriteLine(string.Format("Created {0}", filePath));
                    Loader.Worker.UploadUsingFileSystem(new List<string>() { filePath });
                    break;
                }
                catch
                {
                    if (--retry == 0)
                    {
                        Engine.MyLogger.WriteLine("Unable to open file '" + filePath + "'");
                    }
                    Thread.Sleep(500);
                }
            }
        }
    }
}
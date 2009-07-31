using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows;
using System.Collections.Specialized;

namespace ZSS.Helpers
{
    public class FolderWatcher
    {
        public string FolderPath { get; set; }
        private FileSystemWatcher Watcher = new FileSystemWatcher();

        public FolderWatcher()
        {
            this.Watcher.Created += new FileSystemEventHandler(Watcher_Created);
        }

        public FolderWatcher(string p)
            : this()
        {
            this.FolderPath = p;
            this.Watcher = new FileSystemWatcher(p);            
        }

        public void StartWatching()
        {            
            if (Directory.Exists(FolderPath))
            {
                Watcher.Path = this.FolderPath;
                this.Watcher.EnableRaisingEvents = true;
            }
        }

        public void StopWatching()
        {
            this.Watcher.EnableRaisingEvents = false;
        }


        void Watcher_Created(object sender, FileSystemEventArgs e)
        {
            FileSystem.AppendDebug(string.Format("Added {0}", e.FullPath));
            Program.Worker.UploadUsingFileSystem(new List<string>() { e.FullPath });
        }

    }
}

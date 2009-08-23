using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows;
using System.Collections.Specialized;
using System.ComponentModel;
using ZSS;
using ZScreenLib;

namespace ZScreenGUI
{
    public class FolderWatcher
    {
        public string FolderPath { get; set; }
        private FileSystemWatcher Watcher = new FileSystemWatcher();

        public FolderWatcher(ISynchronizeInvoke SynchronizingObject)
        {
            this.Watcher.SynchronizingObject = SynchronizingObject;
            this.Watcher.Created += new FileSystemEventHandler(Watcher_Created);
        }

        public FolderWatcher(string p, ISynchronizeInvoke SynchronizingObject)
            : this(SynchronizingObject)
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

        [STAThread]
        void Watcher_Created(object sender, FileSystemEventArgs e)
        {
            FileSystem.AppendDebug(string.Format("Added {0}", e.FullPath));
            if (Loader.Worker.UploadUsingFileSystem(new List<string>() { e.FullPath }))
            {
                try
                {
                    File.Delete(e.FullPath);
                }
                catch (Exception ex)
                {
                    FileSystem.AppendDebug(ex);
                }
            }
        }

    }
}

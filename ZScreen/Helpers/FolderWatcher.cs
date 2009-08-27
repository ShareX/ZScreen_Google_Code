using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using ZScreenLib;
using ZSS;

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
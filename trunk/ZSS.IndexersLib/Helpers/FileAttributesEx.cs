using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace ZSS.IndexersLib.Helpers
{
    public class FileAttributesEx
    {
        public string FilePath { get; set; }

        public FileAttributesEx() { }

        public FileAttributesEx(string path)
        {
            FilePath = path;
        }

        public bool isReadOnlyDirectory()
        {
            return isDirectory() && isReadOnly();
        }

        public bool isDirectory()
        {
            return (File.GetAttributes(FilePath) & FileAttributes.Directory) == FileAttributes.Directory;
        }

        public bool isReadOnly()
        {
            return (File.GetAttributes(FilePath) & FileAttributes.ReadOnly) == FileAttributes.ReadOnly;
        }

        public bool isHidden()
        {
            return (File.GetAttributes(FilePath) & FileAttributes.Hidden) == FileAttributes.Hidden;
        }

        public bool isArchive()
        {
            return (File.GetAttributes(FilePath) & FileAttributes.Archive) == FileAttributes.Archive;
        }

        public bool isSystem()
        {
            return (File.GetAttributes(FilePath) & FileAttributes.System) == FileAttributes.System;
        }

        public bool isHiddenSystemFile()
        {
            return isHidden() && isSystem() && isArchive();
        }

        public void AddSystem()
        {
            File.SetAttributes(FilePath, File.GetAttributes(FilePath) | FileAttributes.System);
        }

        public void AddReadOnly()
        {
            File.SetAttributes(FilePath, File.GetAttributes(FilePath) | FileAttributes.ReadOnly);
        }

        public void AddHidden()
        {
            File.SetAttributes(FilePath, File.GetAttributes(FilePath) | FileAttributes.Hidden);
        }

        public void AddArchive()
        {
            File.SetAttributes(FilePath, File.GetAttributes(FilePath) | FileAttributes.Archive);
        }

        public void Clear()
        {
            File.SetAttributes(FilePath, File.GetAttributes(FilePath)
            & ~(FileAttributes.Archive | FileAttributes.ReadOnly | FileAttributes.System | FileAttributes.Hidden));
        }
    }
}

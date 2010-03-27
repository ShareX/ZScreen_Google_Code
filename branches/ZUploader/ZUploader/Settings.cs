using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using UploadersLib;
using System.ComponentModel;

namespace ZUploader
{
    public class Settings
    {
        public bool ClipboardAutoCopy { get; set; }
        public int SelectedImageUploaderDestination { get; set; }
        public int SelectedTextUploaderDestination { get; set; }
        public int SelectedFileUploaderDestination { get; set; }
        public FTPAccount FTPAccount { get; set; }

        #region Functions

        public bool Save()
        {
            return Save(Program.SettingsFilePath);
        }

        public bool Save(string path)
        {
            if (!Directory.Exists(Path.GetDirectoryName(path)))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(path));
            }

            try
            {
                XmlSerializer xs = new XmlSerializer(typeof(Settings));
                using (FileStream fs = new FileStream(path, FileMode.Create))
                {
                    xs.Serialize(fs, this);
                    return true;
                }
            }
            catch (Exception e)
            {

            }

            return false;
        }

        public static Settings Load()
        {
            return Load(Program.SettingsFilePath);
        }

        public static Settings Load(string path)
        {
            if (!Directory.Exists(Path.GetDirectoryName(path)))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(path));
            }

            if (File.Exists(path))
            {
                try
                {
                    XmlSerializer xs = new XmlSerializer(typeof(Settings));
                    using (FileStream fs = new FileStream(path, FileMode.Open))
                    {
                        return xs.Deserialize(fs) as Settings;
                    }
                }
                catch (Exception e)
                {

                }
            }

            return new Settings();
        }

        #endregion
    }
}
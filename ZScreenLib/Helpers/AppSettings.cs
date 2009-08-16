using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml.Serialization;
using System.Threading;

namespace ZScreenLib
{
    [Serializable]
    public class AppSettings
    {
        private static string AppSettingsFile = Path.Combine(Program.LocalAppDataFolder, "AppSettings.xml");

        public string RootDir { get; set; }
        public string XMLSettingsFile { get; set; }

        public AppSettings()
        {
            //RootDir = Program.DefaultRootAppFolder;
        }

        public static AppSettings Read()
        {
            return Read(AppSettingsFile);
        }

        public string GetSettingsFilePath()
        {
            return Path.Combine(Program.SettingsDir, XMLSettings.XMLFileName);
        }

        public static AppSettings Read(string filePath)
        {
            if (!Directory.Exists(Path.GetDirectoryName(filePath)))
                Directory.CreateDirectory(Path.GetDirectoryName(filePath));

            if (File.Exists(filePath))
            {
                try
                {
                    XmlSerializer xs = new XmlSerializer(typeof(AppSettings));
                    using (FileStream fs = new FileStream(filePath, FileMode.Open))
                    {
                        return xs.Deserialize(fs) as AppSettings;
                    }
                }
                catch (Exception ex)
                {
                    FileSystem.AppendDebug(ex.ToString());
                }
            }

            return new AppSettings();
        }

        public void Write()
        {
            new Thread(SaveThread).Start(AppSettingsFile);
        }

        public void SaveThread(object filePath)
        {
            lock (this)
            {
                Write((string)filePath);
            }
        }

        public void Write(string filePath)
        {
            try
            {
                if (!Directory.Exists(Path.GetDirectoryName(filePath)))
                    Directory.CreateDirectory(Path.GetDirectoryName(filePath));

                XmlSerializer xs = new XmlSerializer(typeof(AppSettings));
                using (FileStream fs = new FileStream(filePath, FileMode.Create))
                {
                    xs.Serialize(fs, this);
                }
            }
            catch (Exception ex)
            {
                FileSystem.AppendDebug(ex.ToString());
            }
        }
    }
}
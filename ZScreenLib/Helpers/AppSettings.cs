using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml.Serialization;
using System.Threading;
using UploadersLib;
using System.ComponentModel;

namespace ZScreenLib
{
    [Serializable]
    public class AppSettings
    {
        public readonly static string AppSettingsFile = Path.Combine(Engine.zLocalAppDataFolder, "AppSettings.xml");

        public string RootDir { get; set; }
        public string XMLSettingsFile = Path.Combine(Engine.zLocalAppDataFolder, XMLSettings.XMLFileName);
        [Category("Options / General"), DefaultValue(ImageDestType.CLIPBOARD), Description("Image Destination")]
        public ImageDestType ImageUploader { get; set; }
        [Category("Options / General"), DefaultValue(false), Description("Prefer System Folders for all the data created by ZScreen")]
        public bool PreferSystemFolders { get; set; }

        public static void ApplyDefaultValues(object self)
        {
            foreach (PropertyDescriptor prop in TypeDescriptor.GetProperties(self))
            {
                DefaultValueAttribute attr = prop.Attributes[typeof(DefaultValueAttribute)] as DefaultValueAttribute;
                if (attr == null) continue;
                prop.SetValue(self, attr.Value);
            }
        }

        public AppSettings()
        {
            ApplyDefaultValues(this);
        }

        public static AppSettings Read()
        {
            return Read(AppSettingsFile);
        }

        public string GetSettingsFilePath()
        {
            return Path.Combine(Engine.SettingsDir, XMLSettings.XMLFileName);
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
                    FileSystem.AppendDebug("Error while reading appSettings", ex);
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
                FileSystem.AppendDebug("Error while writing appSettings", ex);
            }
        }
    }
}
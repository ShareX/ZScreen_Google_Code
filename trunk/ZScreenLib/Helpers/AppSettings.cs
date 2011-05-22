using System;
using System.ComponentModel;
using System.IO;
using System.Threading;
using System.Xml.Serialization;
using UploadersLib;

namespace ZScreenLib
{
    public class AppSettings
    {
        public readonly static string AppSettingsFile = Path.Combine(Engine.zLocalAppDataFolder, "AppSettings.xml");

        public string RootDir { get; set; }
        public string XMLSettingsFile = Path.Combine(Engine.zLocalAppDataFolder, XMLSettings.SettingsFileName);

        public int ImageUploader; // default value is from ConfigWizard
        public int FileUploader; // default value is from ConfigWizard
        public int TextUploader; // default value is from ConfigWizard
        public int UrlShortener; // default value is from ConfigWizard

        [Category("Options / General"), Description("Prefer System Folders for all the data created by ZScreen")]
        public bool PreferSystemFolders { get; set; }  // default value is from ConfigWizard

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
            return Path.Combine(Engine.SettingsDir, XMLSettings.SettingsFileName);
        }

        public static AppSettings Read(string filePath)
        {
            if (!Engine.Portable && !Directory.Exists(Path.GetDirectoryName(filePath)))
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
                    Engine.MyLogger.WriteException(ex, "Error while reading appSettings");
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
                Engine.MyLogger.WriteException(ex, "Error while writing appSettings");
            }
        }
    }
}
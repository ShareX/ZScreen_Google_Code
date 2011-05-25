using System;
using System.ComponentModel;
using System.IO;
using System.Threading;
using System.Xml.Serialization;
using UploadersLib;
using System.Windows.Forms.Design;
using System.Drawing.Design;

namespace ZScreenLib
{
    public class AppSettings
    {
        public readonly static string AppSettingsFile = Path.Combine(Engine.zLocalAppDataFolder, "AppSettings.xml");

        public string RootDir { get; set; }
        public string XMLSettingsFile { get; set; }

        public int ImageUploader; // default value is from ConfigWizard
        public int FileUploader; // default value is from ConfigWizard
        public int TextUploader; // default value is from ConfigWizard
        public int UrlShortener; // default value is from ConfigWizard

        [Category("Options / General"), Description("Prefer System Folders for all the data created by ZScreen")]
        public bool PreferSystemFolders { get; set; }  // default value is from ConfigWizard

        [Category("Options / Paths"), Description("Directory where custom history file will be saved.")]
        [EditorAttribute(typeof(FolderNameEditor), typeof(UITypeEditor))]
        public string CustomHistoryDir { get; set; }

        [Category("Options / Paths"), Description("Directory where custom uploaders config file will be saved.")]
        [EditorAttribute(typeof(FolderNameEditor), typeof(UITypeEditor))]
        public string CustomUploadersConfigDir { get; set; }

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

        public void SaveThread(object filePath)
        {
            lock (this)
            {
                Write((string)filePath);
            }
        }

        public void Write()
        {
            if (!Engine.Portable) // DONT UPDATE FOR PORTABLE MODE
            {
                new Thread(SaveThread).Start(AppSettingsFile);
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
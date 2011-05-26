using System.ComponentModel;
using System.Drawing.Design;
using System.IO;
using System.Windows.Forms.Design;
using HelpersLib;

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
            return SettingsHelper.Load<AppSettings>(filePath, SerializationType.Xml);
        }

        public bool Write()
        {
            return Write(AppSettingsFile);
        }

        public bool Write(string filePath)
        {
            return SettingsHelper.Save(this, filePath, SerializationType.Xml);
        }
    }
}
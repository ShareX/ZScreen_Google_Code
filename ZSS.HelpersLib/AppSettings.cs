using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.IO;
using System.Windows.Forms.Design;

namespace HelpersLib
{
    public class AppSettings
    {
        public readonly static string AppSettingsFile = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), @"ZScreen\AppSettings.xml");

        [Browsable(false), EditorAttribute(typeof(FolderNameEditor), typeof(UITypeEditor)), Description("Relocate Root folder location")]
        public string RootDir { get; set; }

        [ReadOnly(true)]
        public string XMLSettingsFile { get; set; }

        [Category("Options / General"), DefaultValue(true), Description("Prefer System Folders for all the data created by ZScreen")]
        public bool PreferSystemFolders { get; set; }  // default value is from ConfigWizard

        [Category("Options / Paths"), DefaultValue(false), Description("Use a customised History path.")]
        public bool UseHistoryCustomPath { get; set; }

        [Category("Options / Paths"), Description("Path where history file will be saved.")]
        [EditorAttribute(typeof(XmlFileNameEditor), typeof(UITypeEditor))]
        public string UploadersHistoryCustomPath { get; set; }

        [Category("Options / Paths"), DefaultValue(false), Description("Use a customised Uploaders Configuration path.")]
        public bool UseUploadersConfigCustomPath { get; set; }

        [Category("Options / Paths"), Description("Path where uploaders config file will be saved.")]
        [EditorAttribute(typeof(XmlFileNameEditor), typeof(UITypeEditor))]
        public string UploadersConfigCustomPath { get; set; }

        public int ImageUploader; // default value is from ConfigWizard
        public int FileUploader;  // default value is from ConfigWizard
        public int TextUploader;  // default value is from ConfigWizard
        public int UrlShortener;  // default value is from ConfigWizard

        public AppSettings()
        {
            ApplyDefaultValues(this);
        }

        public static void ApplyDefaultValues(object self)
        {
            foreach (PropertyDescriptor prop in TypeDescriptor.GetProperties(self))
            {
                DefaultValueAttribute attr = prop.Attributes[typeof(DefaultValueAttribute)] as DefaultValueAttribute;
                if (attr == null) continue;
                prop.SetValue(self, attr.Value);
            }
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
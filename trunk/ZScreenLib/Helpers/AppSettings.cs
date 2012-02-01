using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.IO;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using HelpersLib;

namespace ZScreenLib
{
    public class AppSettings
    {
        public static string AppSettingsFile
        {
            get
            {
                string dir = Engine.IsPortable ? Path.Combine(Application.StartupPath, Engine.PortableRootFolder) : Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), Application.ProductName);
                return Path.Combine(dir, "AppSettings.xml");
            }
        }

        [Browsable(false), EditorAttribute(typeof(FolderNameEditor), typeof(UITypeEditor)), Description("Relocate Root folder location")]
        public string RootDir { get; set; }

        [Category(ComponentModelStrings.AppPaths), ReadOnly(true)]
        public string XMLSettingsPath { get; set; }

        [Category(ComponentModelStrings.AppPaths), ReadOnly(true)]
        public string WorkflowConfigPath { get; set; }

        [Category(ComponentModelStrings.AppPaths), ReadOnly(true)]
        public string UploadersConfigPath { get; set; }

        [Category(ComponentModelStrings.AppPaths), DefaultValue(false), Description("Prefer System Folders for all the data created by ZScreen")]
        public bool PreferSystemFolders { get; set; }  // default value is from ConfigWizard

        [Category(ComponentModelStrings.Outputs), DefaultValue(true), Description("Support uploading to multiple destinations.")]
        public bool SupportMultipleDestinations { get; set; }

        [Category(ComponentModelStrings.AppPathsHistory), DefaultValue(false), Description("Use a customised History path.")]
        public bool UseHistoryCustomPath { get; set; }

        [Category(ComponentModelStrings.AppPathsHistory), Description("Path where history file will be saved.")]
        [EditorAttribute(typeof(XmlFileNameEditor), typeof(UITypeEditor))]
        public string HistoryCustomPath { get; set; }

        [Category(ComponentModelStrings.AppPathsWorkflow), DefaultValue(false), Description("Use a customised Workflow Configuration path.")]
        public bool UseWorkflowConfigCustomPath { get; set; }

        [Category(ComponentModelStrings.AppPathsUploaders), DefaultValue(false), Description("Use a customised Uploaders Configuration path.")]
        public bool UseUploadersConfigCustomPath { get; set; }

        [Category(ComponentModelStrings.AppPathsWorkflow), Description("Path where workflow config file will be saved.")]
        [EditorAttribute(typeof(XmlFileNameEditor), typeof(UITypeEditor))]
        public string WorkflowConfigCustomPath { get; set; }

        [Category(ComponentModelStrings.AppPathsUploaders), Description("Path where uploaders config file will be saved.")]
        [EditorAttribute(typeof(XmlFileNameEditor), typeof(UITypeEditor))]
        public string UploadersConfigCustomPath { get; set; }

        public List<int> AppOutputs = new List<int>();
        public List<int> ClipboardContent = new List<int>();
        public List<int> ImageUploaders = new List<int>(); // default value is from ConfigWizard
        public List<int> FileUploaders = new List<int>();  // default value is from ConfigWizard
        public List<int> TextUploaders = new List<int>();  // default value is from ConfigWizard
        public List<int> LinkUploaders = new List<int>();  // default value is from ConfigWizard

        #region Program Window

        public bool ShowMainWindow = false;

        [Category(ComponentModelStrings.App), DefaultValue(true), Description("Show application title in the taskbar.")]
        public bool ShowInTaskbar { get; set; }

        public bool Windows7TaskbarIntegration = true;

        public FormWindowState WindowState = FormWindowState.Normal;
        [Category(ComponentModelStrings.App), DefaultValue(true), Description("Remember Main Window size and position.")]
        public bool SaveFormSizePosition { get; set; }
        public Size WindowSize = Size.Empty;
        public Point WindowLocation = Point.Empty;

        public WindowButtonAction WindowButtonActionClose = WindowButtonAction.MinimizeToTray;
        public WindowButtonAction WindowButtonActionMinimize = WindowButtonAction.MinimizeToTaskbar;

        #endregion Program Window

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
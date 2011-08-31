using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HelpersLib;
using System.Windows.Forms;
using System.IO;
using System.ComponentModel;

namespace JBirdGUI
{
    public class Settings
    {
        public readonly static string SettingsFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), Application.ProductName + @"\Settings.xml");

        #region I/O Methods

        public void Backup()
        {
            string dirPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), Application.ProductName);
            if (!Directory.Exists(dirPath)) Directory.CreateDirectory(dirPath);
            File.Copy(SettingsFilePath, Path.Combine(dirPath, Path.GetFileName(SettingsFilePath)));
        }

        public static Settings Read(string filePath)
        {
            return SettingsHelper.Load<Settings>(filePath, SerializationType.Xml);
        }

        public bool Write()
        {
            return SettingsHelper.Save<Settings>(this, SettingsFilePath, SerializationType.Xml);
        }

        public static Settings Read()
        {
            return SettingsHelper.Load<Settings>(SettingsFilePath, SerializationType.Xml);
        }

        #endregion I/O Methods

        [Category("Options"), DefaultValue(false), Description("Toggle to backup settings to Documents folder")]
        public bool BackupSettings { get; set; }

    }
}

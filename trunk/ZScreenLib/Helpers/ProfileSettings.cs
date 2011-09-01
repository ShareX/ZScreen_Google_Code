using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;
using HelpersLib;

namespace ZScreenLib
{
    [Serializable]
    public class ProfileSettings
    {
        [Category("Options"), Description("List of Profiles")]
        public List<Workflow> Profiles = new List<Workflow>();

        public readonly static string SettingsFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), Application.ProductName + @"\Profiles1.xml");

        #region I/O Methods

        public void Backup()
        {
            string dirPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), Application.ProductName);
            if (!Directory.Exists(dirPath)) Directory.CreateDirectory(dirPath);
            File.Copy(SettingsFilePath, Path.Combine(dirPath, Path.GetFileName(SettingsFilePath)));
        }

        public static ProfileSettings Read(string filePath)
        {
            return SettingsHelper.Load<ProfileSettings>(filePath, SerializationType.Xml);
        }

        public static ProfileSettings Read()
        {
            return SettingsHelper.Load<ProfileSettings>(SettingsFilePath, SerializationType.Xml);
        }

        public bool Write(string filePath)
        {
            return SettingsHelper.Save<ProfileSettings>(this, filePath, SerializationType.Xml);
        }

        public bool Write()
        {
            return SettingsHelper.Save<ProfileSettings>(this, SettingsFilePath, SerializationType.Xml);
        }

        #endregion I/O Methods
    }
}
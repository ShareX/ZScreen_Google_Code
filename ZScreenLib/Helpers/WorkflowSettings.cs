using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;
using HelpersLib;

namespace ZScreenLib
{
    [Serializable]
    public class WorkflowConfig
    {
        [Category("Options"), Description("List of Profiles")]
        public List<Workflow> Workflows98 = new List<Workflow>();

        public readonly static string SettingsFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), Application.ProductName + @"\Profiles1.xml");

        #region I/O Methods

        public void Backup()
        {
            string dirPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), Application.ProductName);
            if (!Directory.Exists(dirPath)) Directory.CreateDirectory(dirPath);
            File.Copy(SettingsFilePath, Path.Combine(dirPath, Path.GetFileName(SettingsFilePath)));
        }

        public static WorkflowConfig Read(string filePath)
        {
            return SettingsHelper.Load<WorkflowConfig>(filePath, SerializationType.Xml);
        }

        public static WorkflowConfig Read()
        {
            return SettingsHelper.Load<WorkflowConfig>(SettingsFilePath, SerializationType.Xml);
        }

        public bool Write(string filePath)
        {
            return SettingsHelper.Save<WorkflowConfig>(this, filePath, SerializationType.Xml);
        }

        public bool Write()
        {
            return SettingsHelper.Save<WorkflowConfig>(this, SettingsFilePath, SerializationType.Xml);
        }

        #endregion I/O Methods
    }
}
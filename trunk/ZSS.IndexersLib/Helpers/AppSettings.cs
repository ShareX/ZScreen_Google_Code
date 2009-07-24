using System.ComponentModel;
using System;
using System.Xml;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace ZSS.IndexersLib
{
    [Serializable()]
    public class AppSettings
    {
        private string m_TasksFolderPath;

        private bool m_EnableScheduleInGuiMode { get; set; }
        private bool m_AlwaysOnTop = false;
        public bool MaximizedWindow { get; set; }
        public bool RememberWindowSize { get; set; }
        //war59312
        public bool RememberWindowLocation { get; set; }
        //war59312

        // General Configuration > Appearance
        private bool m_MinimizeToTray = true;
        private bool m_TrayIcon = true;
        private bool m_CloseToTray;
        private bool m_TrayOnLoad;

        private bool m_StartupItem;
        private bool m_RunTasksInGui;

        // General Configuration > Schedule 
        private int m_IndexingInterval = 30;

        public readonly string IgnoreFilesListDelimiter = "|";

        public bool OnMonday { get; set; }
        public bool OnTuesday { get; set; }
        public bool OnWednesday { get; set; }
        public bool OnThursday { get; set; }
        public bool OnFriday { get; set; }
        public bool OnSaturday = true;
        public bool OnSunday = true;

        public string ScheduleTime = "03:00:00";
        [XmlIgnore()]
        public bool IsScheduledForToday;

        [XmlElement("EmptyFolderMessage")]
        public string EmptyFolderMessage = "Folder is Empty.";

        public string IndividualIndexFileWordSeperator = "-";

        // Misc

        public bool ForceSave = false;
        public bool OpenDefaultConfig = false;

        private string mDefaultConfigFilePath;

        private bool mTimeInUTC = true;

        private List<string> mRecentFiles = new List<string>();

        public List<string> RecentFiles
        {
            get { return mRecentFiles; }
            set { mRecentFiles = value; }
        }

        public bool IndexedTimeInUTC
        {
            get { return mTimeInUTC; }
            set { mTimeInUTC = value; }
        }


        public string DefaultConfigFilePath
        {
            get { return mDefaultConfigFilePath; }
            set { mDefaultConfigFilePath = value; }
        }


        public string ServiceName
        {
            get { return "McoreIndexer"; }
        }

        public bool IsIndexAccordingToTime { get; set; }
        public bool IsIndexingIntervalEnabled { get; set; }

        public bool TrayIconIsEnabled
        {
            get { return m_TrayIcon; }
            set { m_TrayIcon = value; }
        }

        public bool TrayOnLoad
        {
            get { return m_TrayOnLoad & TrayIconIsEnabled; }
            set { m_TrayOnLoad = value; }
        }

        public bool CloseToTray
        {
            get { return m_CloseToTray & TrayIconIsEnabled; }
            set { m_CloseToTray = value; }
        }

        public bool MinimizeToTray
        {
            get { return m_MinimizeToTray & TrayIconIsEnabled; }
            set { m_MinimizeToTray = value; }
        }

        public int IndexingInterval
        {
            get { return this.m_IndexingInterval; }
            set { m_IndexingInterval = value; }
        }

        public bool RunTasksInGUI
        {
            get { return this.m_RunTasksInGui; }
            set { m_RunTasksInGui = value; }
        }

        public bool StartupItem
        {
            get { return this.m_StartupItem; }
            set { this.m_StartupItem = value; }
        }

        public bool AlwaysOnTop
        {
            get { return m_AlwaysOnTop; }
            set { m_AlwaysOnTop = value; }
        }
        public string TasksFolderPath
        {
            get { return this.m_TasksFolderPath; }
            set { m_TasksFolderPath = value; }
        }
    }


}

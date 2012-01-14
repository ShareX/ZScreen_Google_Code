using System.ComponentModel;
using HelpersLib;
using ZScreenCoreLib;
using ZSS.IndexersLib;

namespace ZScreenLib
{
    public class ZScreenOptions
    {
        #region 1 I/O Methods

        public static ZScreenOptions Read(string filePath)
        {
            // Encrypt passwords
            return SettingsHelper.Load<ZScreenOptions>(filePath, SerializationType.Xml);
        }

        public bool Write(string filePath)
        {
            return SettingsHelper.Save(this, filePath, SerializationType.Xml);
        }

        #endregion 1 I/O Methods

        #region Application

        public bool AutoSaveSettings = true;

        #endregion Application

        #region Capture

        public bool CaptureEntireScreenOnError = false;
        public IndexerConfig IndexerConfig = new IndexerConfig();

        #endregion Capture

        #region Effects

        public ImageEffectsConfig ConfigImageEffects = new ImageEffectsConfig();

        #endregion Effects

        #region History

        public int HistoryMaxNumber = 100;
        public bool HistorySave = true;

        #endregion History

        #region Other methods

        public static void ApplyDefaultValues(object self)
        {
            foreach (PropertyDescriptor prop in TypeDescriptor.GetProperties(self))
            {
                DefaultValueAttribute attr = prop.Attributes[typeof(DefaultValueAttribute)] as DefaultValueAttribute;
                if (attr == null) continue;
                prop.SetValue(self, attr.Value);
            }
        }

        #endregion Other methods

        #region Workflow - after completing a task

        public bool BalloonTipOpenLink = true;
        public bool CompleteSound = true;
        public bool DeleteLocal = false;
        public decimal FlashTrayCount = 2;

        public bool ShowBalloonTip = true;
        public bool ShowUploadDuration = true;
        public bool TwitterEnabled = false;

        #endregion Workflow - after completing a task
    }
}
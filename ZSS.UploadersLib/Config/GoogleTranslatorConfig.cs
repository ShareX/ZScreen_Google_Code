using System.Collections.Generic;
using HelpersLib;
using UploadersLib.OtherServices;

namespace UploadersLib
{
    public class GoogleTranslatorConfig
    {
        #region I/O Methods

        public bool Write(string filePath)
        {
            return SettingsHelper.Save<GoogleTranslatorConfig>(this, filePath, SerializationType.Xml);
        }

        public static GoogleTranslatorConfig Read(string filePath)
        {
            return SettingsHelper.Load<GoogleTranslatorConfig>(filePath, SerializationType.Xml);
        }

        #endregion I/O Methods

        public List<GoogleLanguage> GoogleLanguages;
        public string GoogleSourceLanguage = "en";
        public bool GoogleAutoDetectSource = true;
        public string GoogleTargetLanguage = "en";
        public string GoogleTargetLanguage2 = "?";
        public bool AutoTranslate = false;
        public int AutoTranslateLength = 20;
    }
}
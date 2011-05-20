using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HelpersLib;
using System.IO;
using UploadersLib.OtherServices;

namespace UploadersLib
{
    public class GoogleTranslatorConfig
    {
        #region I/O Methods

        public bool Write(string filePath)
        {
            return SettingsHelper.Save<GoogleTranslatorConfig>(this, filePath, SerializationType.Xml, StaticHelper.MyLogger);
        }

        public static GoogleTranslatorConfig Read(string filePath)
        {
            if (!string.IsNullOrEmpty(filePath) && File.Exists(filePath))
            {
                return HelpersLib.SettingsHelper.Load<GoogleTranslatorConfig>(filePath, HelpersLib.SerializationType.Xml, StaticHelper.MyLogger);
            }

            return new GoogleTranslatorConfig();
        }

        #endregion

        public List<GoogleLanguage> GoogleLanguages;
        public string GoogleSourceLanguage = "en";
        public bool GoogleAutoDetectSource = true;
        public string GoogleTargetLanguage = "en";
        public string GoogleTargetLanguage2 = "?";
        public bool AutoTranslate = false;
        public int AutoTranslateLength = 20;
    }
}

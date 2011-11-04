/*
 * Author: David Amenta
 * Release Date: 12/12/2009
 * License: Free for any use.
 */

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;

namespace DavuxLib
{
    public class Settings
    {
        public delegate void SettingChanged(ConfValue c);

        /// <summary>
        /// Occurs ecah time a setting value is changed.
        /// </summary>
        public static event SettingChanged OnSettingChanged;

        /// <summary>
        /// True if the settings file will be placed in the Local Application Data directory.
        /// </summary>
        public static bool SaveToAppData { get; set; }

        /// <summary>
        /// Directory or Filename where the settings will be stored.
        /// </summary>
        public static string SaveFileName { get; set; }

        internal static List<ConfValue> Dict = new List<ConfValue>();

        /// <summary>
        /// Root location where SaveFileName will be created.
        /// </summary>
        public static string SaveLocation
        {
            get
            {
                if (SaveToAppData)
                {
                    return Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + System.IO.Path.DirectorySeparatorChar + SaveFileName;
                }
                else
                {
                    return System.IO.Directory.GetCurrentDirectory() + System.IO.Path.DirectorySeparatorChar + SaveFileName;
                }
            }
        }

        static Settings()
        {
            SaveToAppData = true;
            SaveFileName = "settings.xml";
        }

        public static bool Load()
        {
            bool ret = true;
            try
            {
                Dict.AddRange(XSerializer.Load<List<ConfValue>>(SaveLocation));
            }
            catch (Exception ex)
            {
                Trace.WriteLine("Error Loading Settings: " + ex.Message);
                ret = false;
            }
            return ret;
        }

        public static void Save()
        {
            try
            {
                XSerializer.Save<List<ConfValue>>(SaveLocation, Dict);
            }
            catch (Exception ex)
            {
                Trace.WriteLine("Error Saving Settings: " + ex.Message);
            }
        }

        #region Get methods for each data type.

        public static Rectangle Get(string key, Rectangle sDefault)
        {
            string r = Get(key, null);
            if (r != null)
            {
                if (r.Contains('|'))
                {
                    try
                    {
                        string[] parts = r.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);

                        if (parts.Length == 4)
                        {
                            return new Rectangle(int.Parse(parts[0]), int.Parse(parts[1]), int.Parse(parts[2]), int.Parse(parts[3]));
                        }
                    }
                    catch (Exception) { }
                }
            }
            return sDefault;
        }

        public static bool Get(string key, bool sDefault)
        {
            return Get(key, sDefault ? "YES" : "NO", null, true) == "YES";
        }

        public static int Get(string key, int sDefault)
        {
            try
            {
                return int.Parse(Get(key, sDefault.ToString(), null, true));
            }
            catch
            {
                return sDefault;
            }
        }

        public static string Get(string key, string sDefault)
        {
            return Get(key, sDefault, sDefault, true);
        }

        public static string Get(string key, string sDefault, string error, bool create)
        {
            for (int i = 0; i < Dict.Count; i++)
            {
                ConfValue KV = Dict[i];
                //Trace.WriteLine(KV.Key + " <- " + key);
                if (KV.Key.Trim().ToLower() == key.Trim().ToLower())
                {
                    //T//race.WriteLineIf(string.IsNullOrEmpty(KV.Value), "Empty Value");
                    return KV.Value;
                }
            }
            //Trace.WriteLine("Setting Not found");
            if (create)
            {
                ConfValue c = new ConfValue(key, sDefault);
                Dict.Add(c);

                if (OnSettingChanged != null)
                {
                    OnSettingChanged.Invoke(c);
                }

                if (key == "#Settings|Save On Modification" || Settings.Get("#Settings|Save On Modification", true))
                {
                    Save();
                }
                return sDefault;
            }
            return error;
        }

        #endregion Get methods for each data type.

        #region Set methods for each data type (Match Get!)

        public static void Set(string key, Rectangle value)
        {
            Set(key, value.X + "|" + value.Y + "|" + value.Width + "|" + value.Height);
        }

        public static void Set(string key, bool value)
        {
            Set(key, value ? "YES" : "NO");
        }

        public static void Set(string key, int value)
        {
            Set(key, value.ToString());
        }

        public static void Set(string key, string value)
        {
            for (int i = 0; i < Dict.Count; i++)
            {
                ConfValue KV = Dict[i];

                if (KV.Key == key)
                {
                    KV.Value = value;

                    if (key == "#Settings|Save On Modification" || Settings.Get("#Settings|Save On Modification", true))
                    {
                        Save();
                    }
                    if (OnSettingChanged != null)
                    {
                        OnSettingChanged.Invoke(KV);
                    }
                    return;
                }
            }

            ConfValue c = new ConfValue(key, value);
            Dict.Add(c);

            if (OnSettingChanged != null)
            {
                OnSettingChanged.Invoke(c);
            }

            if (key == "#Settings|Save On Modification" || Settings.Get("#Settings|Save On Modification", true))
            {
                Save();
            }
        }

        #endregion Set methods for each data type (Match Get!)
    }

    public class ConfValue
    {
        public ConfValue(string key, string value)
        {
            Key = key; Value = value;
        }

        public ConfValue() { }

        public string Key { get; set; }
        public string Value { get; set; }

        public override string ToString()
        {
            return "<CV " + Key + ": " + Value + ">";
        }
    }
}
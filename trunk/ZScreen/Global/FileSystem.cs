#region License Information (GPL v2)
/*
    ZScreen - A program that allows you to upload screenshots in one keystroke.
    Copyright (C) 2008  Brandon Zimmerman

    This program is free software; you can redistribute it and/or
    modify it under the terms of the GNU General Public License
    as published by the Free Software Foundation; either version 2
    of the License, or (at your option) any later version.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with this program; if not, write to the Free Software
    Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301, USA.
    
    Optionally you can also view the license at <http://www.gnu.org/licenses/>.
*/
#endregion

using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Configuration;
using System.Windows.Forms;

namespace ZSS
{
    static class FileSystem
    {

        private static StringBuilder mDebug = new StringBuilder();
        private static string mFilePathDebug = Path.Combine(Program.LocalAppDataFolder, string.Format("{0}-{1}-debug.txt",
        Application.ProductName, DateTime.Now.ToString("yyyyMMdd")));

        public static string DebugFilePath
        {
            get
            { return mFilePathDebug; }
            private set { ;}
        }

        /// <summary>
        /// Function to read the contents of an embedded resource text fileName
        /// </summary>
        /// <param name="name">Embedded Text fileName name</param>
        /// <returns>Text read from the embedded Text fileName</returns>
        public static string getText(string name)
        {
            System.Reflection.Assembly oAsm = System.Reflection.Assembly.GetExecutingAssembly();
            String rName = string.Empty;
            String text = string.Empty;

            for (int i = 0; i < oAsm.GetManifestResourceNames().Length - 1; i++)
            {
                if (oAsm.GetManifestResourceNames()[i].ToString().Contains(name))
                {
                    rName = oAsm.GetManifestResourceNames()[i].ToString();
                    break;
                }
            }
            if (!string.IsNullOrEmpty(rName))
            {
                Stream oStrm = oAsm.GetManifestResourceStream(rName);
                // read contents of embedded fileName
                StreamReader oRdr = new StreamReader(oStrm);
                text = oRdr.ReadToEnd();
            }

            return text;

        }

        public static string getTextFromFile(string filePath)
        {
            string s = "";
            if (File.Exists(filePath))
            {
                using (StreamReader sr = new StreamReader(filePath))
                {
                    s = sr.ReadToEnd();
                }
            }
            return s;
        }

        public static string getConfigFilePath()
        {

            System.Configuration.Configuration config =
                ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.PerUserRoamingAndLocal);
            return config.FilePath;

        }

        public static string getTempFilePath(string fileName)
        {

            string dir = Program.conf.CacheDir;
            if (string.IsNullOrEmpty(dir))
                dir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), Application.ProductName);
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);
            return Path.Combine(dir, fileName);

        }

        public static void appendDebug(string msg)
        {
            mDebug.AppendLine(DateTime.Now.Ticks.ToString() + " " + msg);
        }

        public static void writeDebugFile()
        {
            if (mDebug.Length > 0)
            {
                using (StreamWriter sw = new StreamWriter(mFilePathDebug, true))
                {
                    sw.WriteLine(mDebug.ToString());
                    mDebug = new StringBuilder();
                }
            }
        }

        public static void msWriteObjectToFileXML(object obj, string filePath)
        {

            using (FileStream fs = new FileStream(filePath, FileMode.Create))
            {
                System.Xml.Serialization.XmlSerializer xs =
                    new System.Xml.Serialization.XmlSerializer(obj.GetType());
                xs.Serialize(fs, obj);
            }

        }

        public static object mfReadObjectFromFileXML(string filePath)
        {

            object myObject = new object();

            try
            {
                using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                {
                    System.Xml.Serialization.XmlSerializer xs =
                        new System.Xml.Serialization.XmlSerializer(myObject.GetType());
                    myObject = xs.Deserialize(fs) as object;
                    fs.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return myObject;

        }

        public static bool msWriteObjectToFileBF(object obj, string filePath)
        {

            bool suc = true;
            try
            {
                FileStream fs = new FileStream(filePath, FileMode.Create);
                System.Runtime.Serialization.Formatters.Binary.BinaryFormatter bf =
                    new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                bf.Serialize(fs, obj);
                fs.Close();
            }
            catch (Exception ex)
            {
                suc = false;
                Console.WriteLine(ex.Message);
            }

            return suc;

        }


        public static object mfReadObjectFromFileBF(string filePath)
        {

            object myObject = null;

            if (File.Exists(filePath))
            {

                FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read);

                try
                {
                    System.Runtime.Serialization.Formatters.Binary.BinaryFormatter bf =
                        new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                    myObject = (object)bf.Deserialize(fs);
                    return myObject;
                }
                catch (Exception ex)
                {
                    fs.Close();
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    fs.Close();
                }

            }

            return myObject;

        }


    }
}

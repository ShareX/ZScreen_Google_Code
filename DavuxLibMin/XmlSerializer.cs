/*
 * Author: David Amenta
 * Release Date: 12/12/2009
 * License: Free for any use.
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml.Serialization;

namespace DavuxLib
{
    public class XSerializer
    {
        /// <summary>
        /// Load a class from a serialized XML file.
        /// </summary>
        /// <param name="filename">full path or path relative to the XML file</param>
        /// <param name="t">type of the class that is being retrieved (Use typeof(ClassName))</param>
        /// <returns>A populated version of the class, or null on failure</returns>
        /// <exception cref="Exception">Can throw several exceptions for IO and serialization loading</exception>
        public static T Load<T>(string filename)
        {
            T ob = default(T);
            using (Stream s = File.Open(filename, FileMode.Open))
            {
                StreamReader sr = new StreamReader(s);
                ob = DeserializeObject<T>(sr.ReadToEnd());
                s.Close();
            }
            return ob;
        }

        /// <summary>
        /// Save an instance of a class to an XML file
        /// </summary>
        /// <param name="filename">Full or relative path to the file</param>
        /// <param name="cls">Class to serialize and save.</param>
        /// <param name="t">Type of the class (use: typeof(ClassName)</param>
        /// <returns>True on success, False on failure</returns>
        public static void Save<T>(string filename, T cls)
        {
            using (Stream s = File.Open(filename, FileMode.Create))
            {
                using (StreamWriter sw = new StreamWriter(s))
                {
                    sw.Write(SerializeObject<T>(cls));
                    sw.Close();
                    s.Close();
                    return;
                }
            }
        }


        /// <summary>
        /// Serialize the object into an XML format
        /// </summary>
        /// <typeparam name="T">Type of object to serialize</typeparam>
        /// <param name="pObject">the object to serialize</param>
        /// <returns>a string representing the XML version of the object</returns>
        public static String SerializeObject<T>(T pObject)
        {
            MemoryStream memoryStream = new MemoryStream();
            UTF8Encoding encoding = new UTF8Encoding();

            XmlSerializer xs = new XmlSerializer(typeof(T));
            System.Xml.XmlTextWriter xmlTextWriter = new System.Xml.XmlTextWriter(memoryStream, Encoding.UTF8);
            xs.Serialize(xmlTextWriter, (object)pObject);
            memoryStream = (MemoryStream)xmlTextWriter.BaseStream;
            return encoding.GetString(memoryStream.ToArray());
        }

        /// <summary>
        /// Deserialize the object back into the object from an XML string
        /// </summary>
        /// <typeparam name="T">Type of the object to restore</typeparam>
        /// <param name="pXmlizedString">The string that represents the object in XML</param>
        /// <returns>A new instance of the restored object</returns>
        public static T DeserializeObject<T>(String pXmlizedString)
        {
            UTF8Encoding encoding = new UTF8Encoding();
            XmlSerializer xs = new XmlSerializer(typeof(T));
            MemoryStream memoryStream = new MemoryStream(encoding.GetBytes(pXmlizedString));
            System.Xml.XmlTextWriter xmlTextWriter = new System.Xml.XmlTextWriter(memoryStream, Encoding.UTF8);
            return (T)xs.Deserialize(memoryStream);
        }
    }
}


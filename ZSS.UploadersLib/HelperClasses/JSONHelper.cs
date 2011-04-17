using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;

namespace UploadersLib.HelperClasses
{
    public static class JSONHelper
    {
        public static T JSONToObject<T>(string json)
        {
            if (!string.IsNullOrEmpty(json))
            {
                try
                {
                    DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(T));
                    using (MemoryStream stream = new MemoryStream(Encoding.UTF8.GetBytes(json)))
                    {
                        return (T)serializer.ReadObject(stream);
                    }
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e.ToString());
                }
            }

            return default(T);
        }
    }
}
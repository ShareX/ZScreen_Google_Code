using System.IO;
using System.Xml.Serialization;

namespace HelpersLib
{
    public class CloneManager : IClone
    {
        /// <summary>
        /// Clones the specified instance.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="instance">The instance.</param>
        /// <returns>A new instance of an object.</returns>
        T IClone.Clone<T>(T instance)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            MemoryStream stream = new MemoryStream();
            serializer.Serialize(stream, instance);
            stream.Seek(0, SeekOrigin.Begin);
            return serializer.Deserialize(stream) as T;
        }
    }
}